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
    public class DeparturesServiceTests
    {
        private IMapper _mapper;
        private AbstractValidator<Departure> _validator;
        private IRepository<Departure> _repository;
        private IService<DepartureDto> _service;

        [SetUp]
        public void Init()
        {
            _mapper = new Mapper(
                new MapperConfiguration(cfg => cfg.CreateMap<DepartureDto, Departure>()));
            _validator = new DepartureValidator();

            _repository = A.Fake<IRepository<Departure>>();
            const int EXISTED_ID = 3;
            A.CallTo(() => _repository.Create(A<Departure>._)).Returns(1);
            A.CallTo(() => _repository.IsExist(EXISTED_ID)).Returns(true);

            _service = new DeparturesService(_repository, _mapper, _validator);
        }

        [Test]
        public void Add_When_departureModel_is_valid_Then_return_created_model_id()
        {
            var validDto = new DepartureDto
            {
                Airplane = new Airplane
                {
                    Name = "Airflot",
                    ExploitationTerm = TimeSpan.FromDays(1000),
                    ReleaseDate = new DateTime(1970, 5, 21),
                    Type = new AirplaneType
                    {
                        AirplaneModel = "YZ-222",
                        CarryingCapacity = 14000,
                        SeatsCount = 987
                    }
                },
                Crew = new Crew
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
                },
                DepartureTime = new DateTime(2018, 07, 16, 20, 21, 0),
                FlightNumber = "YM-2343"
            };

            var result = _service.Add(validDto);

            Assert.AreEqual(result, 1);
        }

        [Test]
        public void Add_When_departureModel_is_not_valid_Then_throw_ValidationException()
        {
            var notValidDto = new DepartureDto
            {
                Airplane = new Airplane
                {
                    Name = "Airflot",
                    ExploitationTerm = TimeSpan.FromDays(1000),
                    ReleaseDate = new DateTime(1970, 5, 21),
                    Type = new AirplaneType
                    {
                        AirplaneModel = "YZ-222",
                        CarryingCapacity = 14000,
                        SeatsCount = 987
                    }
                },
                Crew = new Crew
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
                },
                DepartureTime = new DateTime(2020, 07, 16, 20, 21, 0),
                FlightNumber = ""
            };

            Assert.Throws<ValidationException>(() => _service.Add(notValidDto));
        }

        [Test]
        public void Add_When_departureDto_is_null_Then_throw_NullBodyException()
        {
            DepartureDto nullDto = null;

            Assert.Throws<NullBodyException>(() => _service.Add(nullDto));
        }

        [Test]
        public void Update_When_departureDto_is_null_Then_throw_NullBodyException()
        {
            DepartureDto nullDto = null;
            int id = 1;

            Assert.Throws<NullBodyException>(() => _service.Update(id, nullDto));
        }

        [Test]
        public void Update_When_id_is_not_exist_Then_throw_NotExistException()
        {
            var validDto = new DepartureDto
            {
                Airplane = new Airplane
                {
                    Name = "Airflot",
                    ExploitationTerm = TimeSpan.FromDays(1000),
                    ReleaseDate = new DateTime(1970, 5, 21),
                    Type = new AirplaneType
                    {
                        AirplaneModel = "YZ-222",
                        CarryingCapacity = 14000,
                        SeatsCount = 987
                    }
                },
                Crew = new Crew
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
                },
                DepartureTime = new DateTime(2020, 07, 16, 20, 21, 0),
                FlightNumber = "YM-2343"
            };
            int notExistId = 2;

            Assert.Throws<NotExistException>(() => _service.Update(notExistId, validDto));
        }

        [Test]
        public void Update_When_departureModel_is_valid_and_id_is_exist_Then_call_Update_method()
        {
            var validDto = new DepartureDto
            {
                Airplane = new Airplane
                {
                    Name = "Airflot",
                    ExploitationTerm = TimeSpan.FromDays(1000),
                    ReleaseDate = new DateTime(1970, 5, 21),
                    Type = new AirplaneType
                    {
                        AirplaneModel = "YZ-222",
                        CarryingCapacity = 14000,
                        SeatsCount = 987
                    }
                },
                Crew = new Crew
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
                },
                DepartureTime = new DateTime(2018, 07, 16, 20, 21, 0),
                FlightNumber = "YM-2343"
            };
            int existId = 3;

            _service.Update(existId, validDto);

            A.CallTo(() => _repository.Update(A<int>._, A<Departure>._)).MustHaveHappened();
        }

        [Test]
        public void Update_When_departureModel_is_not_valid_and_id_is_exist_Then_throw_ValidationException()
        {
            var notValidDto = new DepartureDto()
            {
                Airplane = new Airplane
                {
                    Name = "Airflot",
                    ExploitationTerm = TimeSpan.FromDays(1000),
                    ReleaseDate = new DateTime(1970, 5, 21),
                    Type = new AirplaneType
                    {
                        AirplaneModel = "YZ-222",
                        CarryingCapacity = 14000,
                        SeatsCount = 987
                    }
                },
                Crew = new Crew
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
                },
                DepartureTime = new DateTime(2018, 07, 16, 20, 21, 0),
                FlightNumber = ""
            };
            int existId = 3;

            Assert.Throws<ValidationException>(() => _service.Update(existId, notValidDto));
        }
    }
}
