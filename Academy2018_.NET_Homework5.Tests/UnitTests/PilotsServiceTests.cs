using System;
using Academy2018_.NET_Homework5.Core.Abstractions;
using Academy2018_.NET_Homework5.Core.Services;
using Academy2018_.NET_Homework5.Core.Validation;
using Academy2018_.NET_Homework5.Infrastructure.Abstractions;
using Academy2018_.NET_Homework5.Infrastructure.Models;
using Academy2018_.NET_Homework5.Shared.DTOs;
using Academy2018_.NET_Homework5.Shared.Exceptions;
using AutoMapper;
using NUnit.Framework;
using FakeItEasy;
using FluentValidation;

namespace Academy2018_.NET_Homework5.Tests.UnitTests
{
    [TestFixture]
    public class PilotsServiceTests
    {
        private IMapper _mapper;
        private AbstractValidator<Pilot> _validator;
        private IRepository<Pilot> _repository;
        private IService<PilotDto> _service;

        [SetUp]
        public void Init()
        {
            _mapper = new Mapper(
                new MapperConfiguration(cfg => cfg.CreateMap<PilotDto, Pilot>()));
            _validator = new PilotValidator();

            _repository = A.Fake<IRepository<Pilot>>();
            const int EXISTED_ID = 3;
            A.CallTo(() => _repository.Create(A<Pilot>._)).Returns(1);
            A.CallTo(() => _repository.IsExist(EXISTED_ID)).Returns(true);

            _service = new PilotsService(_repository, _mapper, _validator);
        }

        [Test]
        public void Add_When_pilotModel_is_valid_Then_return_created_model_id()
        {
            var validDto = new PilotDto {
                FirstName = "Petro",
                LastName = "Boroda",
                Birthdate = new DateTime(1989, 10, 12),
                Experience = 4
            };

            var result = _service.Add(validDto);
           
            Assert.AreEqual(result, 1);
        }

        [Test]
        public void Add_When_pilotModel_is_not_valid_Then_throw_ValidationException()
        {
            var notValidDto = new PilotDto
            {
                FirstName = "P",
                LastName = "B",
                Birthdate = new DateTime(2030, 10, 12),
                Experience = -2
            };

            Assert.Throws<ValidationException>(() => _service.Add(notValidDto));
        }

        [Test]
        public void Add_When_pilotDto_is_null_Then_throw_NullBodyException()
        {
            PilotDto nullDto = null;

            Assert.Throws<NullBodyException>(() => _service.Add(nullDto));
        }

        [Test]
        public void Update_When_pilotDto_is_null_Then_throw_NullBodyException()
        {
            PilotDto nullDto = null;
            int id = 1;

            Assert.Throws<NullBodyException>(() => _service.Update(id, nullDto));
        }

        [Test]
        public void Update_When_id_is_not_exist_Then_throw_NotExistException()
        {
            var validDto = new PilotDto
            {
                FirstName = "Petro",
                LastName = "Boroda",
                Birthdate = new DateTime(1989, 10, 12),
                Experience = 4
            };
            int notExistId = 2;

            Assert.Throws<NotExistException>(() => _service.Update(notExistId, validDto));
        }

        [Test]
        public void Update_When_pilotModel_is_valid_and_id_is_exist_Then_call_Update_method()
        {
            var validDto = new PilotDto
            {
                FirstName = "Petro",
                LastName = "Boroda",
                Birthdate = new DateTime(1989, 10, 12),
                Experience = 4
            };
            int existId = 3;

            _service.Update(existId, validDto);

            A.CallTo(() => _repository.Update(A<int>._, A<Pilot>._)).MustHaveHappened();
        }

        [Test]
        public void Update_When_pilotModel_is_not_valid_and_id_is_exist_Then_throw_ValidationException()
        {
            var notValidDto = new PilotDto
            {
                FirstName = "P",
                LastName = "B",
                Birthdate = new DateTime(2030, 10, 12),
                Experience = -2
            };
            int existId = 3;

            Assert.Throws<ValidationException>(() => _service.Update(existId, notValidDto));
        }
    }
}
