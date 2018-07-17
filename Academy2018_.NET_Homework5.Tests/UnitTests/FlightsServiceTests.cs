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
    public class FlightsServiceTests
    {
        private IMapper _mapper;
        private AbstractValidator<Flight> _validator;
        private IRepository<Flight> _repository;
        private IService<FlightDto> _service;

        [SetUp]
        public void Init()
        {
            _mapper = new Mapper(
                new MapperConfiguration(cfg => cfg.CreateMap<FlightDto, Flight>()));
            _validator = new FlightValidator();

            _repository = A.Fake<IRepository<Flight>>();
            const int EXISTED_ID = 3;
            A.CallTo(() => _repository.Create(A<Flight>._)).Returns("YM-222");
            A.CallTo(() => _repository.IsExist(EXISTED_ID)).Returns(true);

            _service = new FlightsService(_repository, _mapper, _validator);
        }

        [Test]
        public void Add_When_flightModel_is_valid_Then_return_created_model_id()
        {
            var validDto = new FlightDto
            {
                ArrivalTime = new DateTime(2018, 07, 16, 20, 21, 0),
                DeparturePoint = "Lvil, Ukraine",
                DestinationPoint = "New York, USA",
                Number = "YM-2341",
                Tickets = new List<Ticket>
                {
                    new Ticket
                    {
                        FlightNumber = "YM-2341",
                        Price = 2000
                    }
                }
            };

            var result = _service.Add(validDto);

            Assert.AreEqual(result, "YM-222");
        }

        [Test]
        public void Add_When_flightModel_is_not_valid_Then_throw_ValidationException()
        {
            var notValidDto = new FlightDto
            {
                ArrivalTime = new DateTime(2018, 07, 16, 20, 21, 0),
                DeparturePoint = "",
                DestinationPoint = "New York, USA",
                Number = "YM-2341",
                Tickets = new List<Ticket>
                {
                    new Ticket
                    {
                        FlightNumber = "YM-2341",
                        Price = 2000
                    }
                }
            };

            Assert.Throws<ValidationException>(() => _service.Add(notValidDto));
        }

        [Test]
        public void Add_When_flightDto_is_null_Then_throw_NullBodyException()
        {
            FlightDto nullDto = null;

            Assert.Throws<NullBodyException>(() => _service.Add(nullDto));
        }

        [Test]
        public void Update_When_flightDto_is_null_Then_throw_NullBodyException()
        {
            FlightDto nullDto = null;
            int id = 1;

            Assert.Throws<NullBodyException>(() => _service.Update(id, nullDto));
        }

        [Test]
        public void Update_When_id_is_not_exist_Then_throw_NotExistException()
        {
            var validDto = new FlightDto
            {
                ArrivalTime = new DateTime(2018, 07, 16, 20, 21, 0),
                DeparturePoint = "Lvil, Ukraine",
                DestinationPoint = "New York, USA",
                Number = "YM-2341",
                Tickets = new List<Ticket>
                {
                    new Ticket
                    {
                        FlightNumber = "YM-2341",
                        Price = 2000
                    }
                }
            };
            int notExistId = 2;

            Assert.Throws<NotExistException>(() => _service.Update(notExistId, validDto));
        }

        [Test]
        public void Update_When_flightModel_is_valid_and_id_is_exist_Then_call_Update_method()
        {
            var validDto = new FlightDto
            {
                ArrivalTime = new DateTime(2018, 07, 16, 20, 21, 0),
                DeparturePoint = "Lvil, Ukraine",
                DestinationPoint = "New York, USA",
                Number = "YM-2341",
                Tickets = new List<Ticket>
                {
                    new Ticket
                    {
                        FlightNumber = "YM-2341",
                        Price = 2000
                    }
                }
            };
            int existId = 3;

            _service.Update(existId, validDto);

            A.CallTo(() => _repository.Update(A<int>._, A<Flight>._)).MustHaveHappened();
        }

        [Test]
        public void Update_When_flightModel_is_not_valid_and_id_is_exist_Then_throw_ValidationException()
        {
            var notValidDto = new FlightDto
            {
                ArrivalTime = new DateTime(2018, 07, 16, 20, 21, 0),
                DeparturePoint = "Lvil, Ukraine",
                DestinationPoint = "New York, USA",
                Number = "",
                Tickets = new List<Ticket>
                {
                    new Ticket
                    {
                        FlightNumber = "YM-2341",
                        Price = 2000
                    }
                }
            };
            int existId = 3;

            Assert.Throws<ValidationException>(() => _service.Update(existId, notValidDto));
        }
    }
}
