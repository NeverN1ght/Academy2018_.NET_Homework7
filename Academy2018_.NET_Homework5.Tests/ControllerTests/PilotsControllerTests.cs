using System;
using System.Collections.Generic;
using Academy2018_.NET_Homework5.API.Controllers;
using Academy2018_.NET_Homework5.Core.Abstractions;
using Academy2018_.NET_Homework5.Shared.DTOs;
using Academy2018_.NET_Homework5.Shared.Exceptions;
using NUnit.Framework;
using FakeItEasy;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Academy2018_.NET_Homework5.Tests.ControllerTests
{
    [TestFixture]
    public class PilotsControllerTests
    {
        [Test]
        public void Get_When_id_called_Then_return_status_code_200()
        {
            var service = A.Fake<IService<PilotDto>>();
            A.CallTo(() => service.GetAll()).Returns(new List<PilotDto>());
            var controller = new PilotsController(service);

            var result = controller.Get() as ObjectResult;
         
            Assert.AreEqual(200, result.StatusCode);
        }

        [Test]
        public void Post_When_dto_is_valid_Then_return_status_code_201()
        {
            var service = A.Fake<IService<PilotDto>>();
            var validDto = new PilotDto
            {
                FirstName = "Sanya",
                LastName = "Alexanrovich",
                Birthdate = new DateTime(1989, 12, 19),
                Experience = 10
            };
            A.CallTo(() => service.Add(validDto)).Returns(1);
            var controller = new PilotsController(service);
            var dto = validDto;

            var result = controller.Post(dto) as ObjectResult;

            Assert.AreEqual(201, result.StatusCode);
        }

        [Test]
        public void Post_When_dto_is_not_valid_Then_return_status_code_400()
        {
            var service = A.Fake<IService<PilotDto>>();
            var notValidDto = new PilotDto
            {
                FirstName = "",
                LastName = "Alexanrovich",
                Birthdate = new DateTime(2020, 12, 19),
                Experience = -2
            };
            A.CallTo(() => service.Add(notValidDto)).Throws(new ValidationException(""));
            var controller = new PilotsController(service);
            var dto = notValidDto;

            var result = controller.Post(dto) as StatusCodeResult;

            Assert.AreEqual(400, result.StatusCode);
        }

        [Test]
        public void Post_When_dto_is_null_Then_return_status_code_400()
        {
            var service = A.Fake<IService<PilotDto>>();
            A.CallTo(() => service.Add(A<PilotDto>.That.IsNull())).Throws(new NullBodyException());
            var controller = new PilotsController(service);
            PilotDto dto = null;

            var result = controller.Post(dto) as StatusCodeResult;

            Assert.AreEqual(400, result.StatusCode);
        }

        [Test]
        public void Put_When_dto_is_valid_and_id_is_exist_Then_return_status_code_200()
        {
            var service = A.Fake<IService<PilotDto>>();
            var validDto = new PilotDto
            {
                FirstName = "Sanya",
                LastName = "Alexanrovich",
                Birthdate = new DateTime(1989, 12, 19),
                Experience = 10
            };
            var existId = 1;
            A.CallTo(() => service.Update(existId, validDto));
            var controller = new PilotsController(service);
            var dto = validDto;
            var id = existId;

            var result = controller.Put(id, dto) as StatusCodeResult;

            Assert.AreEqual(200, result.StatusCode);
        }

        [Test]
        public void Put_When_id_is_not_exist_Then_return_status_code_404()
        {
            var service = A.Fake<IService<PilotDto>>();
            var notExistId = 8485;
            A.CallTo(() => service.Update(notExistId, A<PilotDto>._)).Throws(new NotExistException());
            var controller = new PilotsController(service);
            var dto = new PilotDto();
            var id = notExistId;

            var result = controller.Put(id, dto) as StatusCodeResult;

            Assert.AreEqual(404, result.StatusCode);
        }

        [Test]
        public void Delete_When_id_is_exist_Then_return_status_code_204()
        {
            var service = A.Fake<IService<PilotDto>>();
            var existId = 1;
            A.CallTo(() => service.Delete(existId));
            var controller = new PilotsController(service);
            var id = existId;

            var result = controller.Delete(id) as StatusCodeResult;

            Assert.AreEqual(204, result.StatusCode);
        }

        [Test]
        public void Delete_When_id_is_not_exist_Then_return_status_code_404()
        {
            var service = A.Fake<IService<PilotDto>>();
            var notExistId = 8485;
            A.CallTo(() => service.Delete(notExistId)).Throws(new NotExistException());
            var controller = new PilotsController(service);
            var id = notExistId;

            var result = controller.Delete(id) as StatusCodeResult;

            Assert.AreEqual(404, result.StatusCode);
        }
    }
}
