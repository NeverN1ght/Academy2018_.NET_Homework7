using System;
using System.Collections.Generic;
using Academy2018_.NET_Homework5.Core.Abstractions;
using Academy2018_.NET_Homework5.Core.Services;
using Academy2018_.NET_Homework5.Core.Validation;
using Academy2018_.NET_Homework5.Infrastructure.Abstractions;
using Academy2018_.NET_Homework5.Infrastructure.Models;
using Academy2018_.NET_Homework5.Shared.DTOs;
using Academy2018_.NET_Homework5.Shared.Exceptions;
using AutoMapper;
using FakeItEasy;
using FluentValidation;
using NUnit.Framework;

namespace Academy2018_.NET_Homework5.Tests.UnitTests
{
    [TestFixture]
    public class CrewsServiceTests
    {
        private IMapper _mapper;
        private AbstractValidator<Crew> _validator;
        private IRepository<Crew> _repository;
        private IService<CrewDto> _service;

        [SetUp]
        public void Init()
        {
            _mapper = new Mapper(
                new MapperConfiguration(cfg => cfg.CreateMap<CrewDto, Crew>()));
            _validator = new CrewValidator();

            _repository = A.Fake<IRepository<Crew>>();
            const int EXISTED_ID = 3;
            A.CallTo(() => _repository.Create(A<Crew>._)).Returns(1);
            A.CallTo(() => _repository.IsExist(EXISTED_ID)).Returns(true);

            _service = new CrewsService(_repository, _mapper, _validator);
        }

        [Test]
        public void Add_When_crewModel_is_valid_Then_return_created_model_id()
        {
            var validDto = new CrewDto
            {
                Pilot = new Pilot {
                    FirstName = "Petro",
                    LastName = "Boroda",
                    Birthdate = new DateTime(1989, 10, 12),
                    Experience = 4
                },
                Stewardesses = new List<Stewardesse> {
                    new Stewardesse {
                        FirstName = "Maria",
                        LastName = "Alexandrovna",
                        Birthdate = new DateTime(1990, 3, 1)
                    }
                }
            };

            var result = _service.Add(validDto);

            Assert.AreEqual(result, 1);
        }

        [Test]
        public void Add_When_crewDto_is_null_Then_throw_NullBodyException()
        {
            CrewDto nullDto = null;

            Assert.Throws<NullBodyException>(() => _service.Add(nullDto));
        }

        [Test]
        public void Update_When_crewDto_is_null_Then_throw_NullBodyException()
        {
            CrewDto nullDto = null;
            int id = 1;

            Assert.Throws<NullBodyException>(() => _service.Update(id, nullDto));
        }

        [Test]
        public void Update_When_id_is_not_exist_Then_throw_NotExistException()
        {
            var validDto = new CrewDto()
            {
                Pilot = new Pilot
                {
                    FirstName = "Petro",
                    LastName = "Boroda",
                    Birthdate = new DateTime(1989, 10, 12),
                    Experience = 4
                },
                Stewardesses = new List<Stewardesse> {
                    new Stewardesse {
                        FirstName = "Maria",
                        LastName = "Alexandrovna",
                        Birthdate = new DateTime(1990, 3, 1)
                    }
                }
            };
            int notExistId = 2;

            Assert.Throws<NotExistException>(() => _service.Update(notExistId, validDto));
        }

        [Test]
        public void Update_When_crewModel_is_valid_and_id_is_exist_Then_call_Update_method()
        {
            var validDto = new CrewDto
            {
                Pilot = new Pilot
                {
                    FirstName = "Petro",
                    LastName = "Boroda",
                    Birthdate = new DateTime(1989, 10, 12),
                    Experience = 4
                },
                Stewardesses = new List<Stewardesse> {
                    new Stewardesse {
                        FirstName = "Maria",
                        LastName = "Alexandrovna",
                        Birthdate = new DateTime(1990, 3, 1)
                    }
                }
            };
            int existId = 3;

            _service.Update(existId, validDto);

            A.CallTo(() => _repository.Update(A<int>._, A<Crew>._)).MustHaveHappened();
        }
    }
}
