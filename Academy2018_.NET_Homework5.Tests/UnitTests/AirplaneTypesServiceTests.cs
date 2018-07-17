using System;
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
    public class AirplaneTypesServiceTests
    {
        private IMapper _mapper;
        private AbstractValidator<AirplaneType> _validator;
        private IRepository<AirplaneType> _repository;
        private IService<AirplaneTypeDto> _service;

        [SetUp]
        public void Init()
        {
            _mapper = new Mapper(
                new MapperConfiguration(cfg => cfg.CreateMap<AirplaneTypeDto, AirplaneType>()));
            _validator = new AirplaneTypeValidator();

            _repository = A.Fake<IRepository<AirplaneType>>();
            const int EXISTED_ID = 3;
            A.CallTo(() => _repository.Create(A<AirplaneType>._)).Returns(1);
            A.CallTo(() => _repository.IsExist(EXISTED_ID)).Returns(true);

            _service = new AirplaneTypesService(_repository, _mapper, _validator);
        }

        [Test]
        public void Add_When_airplaneTypeModel_is_valid_Then_return_created_model_id()
        {
            var validDto = new AirplaneTypeDto
            { 
                AirplaneModel = "YZ-222",
                CarryingCapacity = 14000,
                SeatsCount = 987             
            };

            var result = _service.Add(validDto);

            Assert.AreEqual(result, 1);
        }

        [Test]
        public void Add_When_airplaneTypeModel_is_not_valid_Then_throw_ValidationException()
        {
            var notValidDto = new AirplaneTypeDto
            {
                AirplaneModel = "Y",
                CarryingCapacity = -200,
                SeatsCount = 987
            };

            Assert.Throws<ValidationException>(() => _service.Add(notValidDto));
        }

        [Test]
        public void Add_When_airplaneTypeDto_is_null_Then_throw_NullBodyException()
        {
            AirplaneTypeDto nullDto = null;

            Assert.Throws<NullBodyException>(() => _service.Add(nullDto));
        }

        [Test]
        public void Update_When_airplaneTypeDto_is_null_Then_throw_NullBodyException()
        {
            AirplaneTypeDto nullDto = null;
            int id = 1;

            Assert.Throws<NullBodyException>(() => _service.Update(id, nullDto));
        }

        [Test]
        public void Update_When_id_is_not_exist_Then_throw_NotExistException()
        {
            var validDto = new AirplaneTypeDto
            {
                AirplaneModel = "YZ-222",
                CarryingCapacity = 14000,
                SeatsCount = 987
            };
            int notExistId = 2;

            Assert.Throws<NotExistException>(() => _service.Update(notExistId, validDto));
        }

        [Test]
        public void Update_When_airplaneTypeModel_is_valid_and_id_is_exist_Then_call_Update_method()
        {
            var validDto = new AirplaneTypeDto
            {
                AirplaneModel = "YZ-222",
                CarryingCapacity = 14000,
                SeatsCount = 987
            };
            int existId = 3;

            _service.Update(existId, validDto);

            A.CallTo(() => _repository.Update(A<int>._, A<AirplaneType>._)).MustHaveHappened();
        }

        [Test]
        public void Update_When_airplaneTypeModel_is_not_valid_and_id_is_exist_Then_throw_ValidationException()
        {
            var notValidDto = new AirplaneTypeDto
            {
                AirplaneModel = "Y",
                CarryingCapacity = -200,
                SeatsCount = 987
            };
            int existId = 3;

            Assert.Throws<ValidationException>(() => _service.Update(existId, notValidDto));
        }
    }
}
