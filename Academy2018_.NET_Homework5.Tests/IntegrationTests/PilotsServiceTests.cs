using System;
using System.Collections.Generic;
using System.Linq;
using Academy2018_.NET_Homework5.Core.Abstractions;
using Academy2018_.NET_Homework5.Core.Services;
using Academy2018_.NET_Homework5.Core.Validation;
using Academy2018_.NET_Homework5.Infrastructure.Abstractions;
using Academy2018_.NET_Homework5.Infrastructure.Database;
using Academy2018_.NET_Homework5.Infrastructure.Models;
using Academy2018_.NET_Homework5.Infrastructure.Repositories;
using Academy2018_.NET_Homework5.Shared.DTOs;
using Academy2018_.NET_Homework5.Shared.Exceptions;
using AutoMapper;
using FluentValidation;
using NUnit.Framework;

namespace Academy2018_.NET_Homework5.Tests.IntegrationTests
{
    /// <summary>
    /// Если запускать тесты по одному, то всё работает нормально,
    /// но если все вместе, то первый заходит нормально, а после него все фейлятся.
    /// Я думал, что проблема в SetUp/TearDown, но так и не смог разобраться.
    /// </summary>
    [TestFixture]
    public class PilotsServiceTests
    {
        private IMapper _mapper;
        private AbstractValidator<Pilot> _validator;
        private AirportContext _context;
        private IRepository<Pilot> _repository;
        private IService<PilotDto> _service;

        public PilotsServiceTests()
        {
            _mapper = new Mapper(
                new MapperConfiguration(
                    cfg => {
                        cfg.CreateMap<PilotDto, Pilot>();
                        cfg.CreateMap<Pilot, PilotDto>();
                    }));
            _validator = new PilotValidator();
            _context = new AirportContext();
            _repository = new PilotsRepository(_context);

            _service = new PilotsService(
                _repository,
                _mapper,
                _validator);
        }

        [SetUp]
        public void Init()
        {         
            // deleting database if it exist
            _context.Database.EnsureDeleted();

            // creating database if not exist
            _context.Database.EnsureCreated();

            // test data
            var testData = new Pilot[]
            {
                new Pilot
                {
                    FirstName = "Petro",
                    LastName = "Bolom",
                    Birthdate = new DateTime(1978, 11, 12),
                    Experience = 5
                },
                new Pilot
                {
                    FirstName = "Mihail",
                    LastName = "Solomko",
                    Birthdate = new DateTime(1967, 1, 21),
                    Experience = 10
                },
                new Pilot
                {
                    FirstName = "Vlad",
                    LastName = "Mars",
                    Birthdate = new DateTime(1969, 10, 22),
                    Experience = 8
                }
            };

            // adding test data
            _context.Pilots.AddRange(testData);
            _context.SaveChanges();
        }

        [TearDown]
        public void Clear()
        {
            // clear database after test
            _context.Database.EnsureDeleted();
        }

        [Test]
        public void GetAll_When_is_called_Then_return_data()
        {
            var result = _service.GetAll()
                .ToList();


            Assert.IsNotEmpty(result);

            Assert.IsTrue(result[0].Id > 0);
            Assert.AreEqual(result[0].FirstName, "Petro");
            Assert.AreEqual(result[0].LastName, "Bolom");
            Assert.AreEqual(result[0].Experience, 5);

            Assert.IsTrue(result[1].Id > 0);
            Assert.AreEqual(result[1].FirstName, "Mihail");
            Assert.AreEqual(result[1].LastName, "Solomko");
            Assert.AreEqual(result[1].Experience, 10);

            Assert.IsTrue(result[2].Id > 0);
            Assert.AreEqual(result[2].FirstName, "Vlad");
            Assert.AreEqual(result[2].LastName, "Mars");
            Assert.AreEqual(result[2].Experience, 8);
        }

        [Test]
        public void GetById_When_id_is_exist_Then_return_entity()
        {
            var existId = _context.Pilots
                .SingleOrDefault(p => p.FirstName == "Petro").Id;

            var result = _service.GetById(existId);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Id, existId);
            Assert.AreEqual(result.FirstName, "Petro");
            Assert.AreEqual(result.LastName, "Bolom");
            Assert.AreEqual(result.Experience, 5);
        }

        [Test]
        public void GetById_When_id_is_not_exist_Then_throw_NotExistException()
        {
            var notExistId = 98465;

            Assert.Throws<NotExistException>(() => _service.GetById(notExistId));
        }

        [Test]
        public void Add_When_pilotDto_is_null_Then_throw_NullBodyException()
        {
            PilotDto nullDto = null;

            Assert.Throws<NullBodyException>(() => _service.Add(nullDto));
        }

        [Test]
        public void Add_When_pilotDto_is_not_valid_Then_throw_ValidationException()
        {
            var notValidDto = new PilotDto
            {
                FirstName = "S",
                LastName = "W",
                Birthdate = new DateTime(2030, 10, 12),
                Experience = -2
            };

            Assert.Throws<ValidationException>(() => _service.Add(notValidDto));
        }

        [Test]
        public void Add_When_pilotDto_is_valid_Then_return_created_model_id()
        {
            var validDto = new PilotDto
            {
                FirstName = "Sanya",
                LastName = "White",
                Birthdate = new DateTime(1989, 10, 12),
                Experience = 2
            };

            var resultedId = _service.Add(validDto);
            var createdId = _context.Pilots
                .FirstOrDefault(p => p.FirstName == "Sanya").Id;

            Assert.AreEqual(resultedId, createdId);
        }

        [Test]
        public void Update_When_id_is_not_exist_Then_throw_NotExistException()
        {
            var notExistId = 98465;
            var validDto = new PilotDto
            {
                FirstName = "Sanya",
                LastName = "White",
                Birthdate = new DateTime(1989, 10, 12),
                Experience = 2
            };

            Assert.Throws<NotExistException>(() => _service.Update(notExistId, validDto));
        }

        [Test]
        public void Update_When_pilotDto_is_null_Then_throw_NullBodyException()
        {
            PilotDto nullDto = null;
            var existId = _context.Pilots
                .FirstOrDefault(p => p.FirstName == "Petro").Id;

            Assert.Throws<NullBodyException>(() => _service.Update(existId, nullDto));
        }

        [Test]
        public void Update_When_pilotDto_is_not_valid_Then_throw_ValidationException()
        {
            var notValidDto = new PilotDto
            {
                FirstName = "S",
                LastName = "W",
                Birthdate = new DateTime(2030, 10, 12),
                Experience = -2
            };
            var existId = _context.Pilots
                .FirstOrDefault(p => p.FirstName == "Petro").Id;

            Assert.Throws<ValidationException>(() => _service.Update(existId, notValidDto));
        }

        [Test]
        public void Update_When_pilotDto_is_valid_Then_update()
        {
            var validDto = new PilotDto
            {
                FirstName = "Ivan", //updated field
                LastName = "Bolom",
                Birthdate = new DateTime(1978, 11, 12),
                Experience = 5
            };
            var existedPilot = _context.Pilots
                .FirstOrDefault(p => p.FirstName == "Petro");

            _service.Update(existedPilot.Id, validDto);

            Assert.AreEqual(existedPilot.FirstName, "Ivan");
        }

        [Test]
        public void Delete_When_id_is_not_exist_Then_throw_NotExistException()
        {
            var notExistId = 98465;

            Assert.Throws<NotExistException>(() => _service.Delete(notExistId));
        }

        [Test]
        public void Delete_When_id_is_exist_Then_delete()
        {
            var existId = _context.Pilots
                .FirstOrDefault(p => p.FirstName == "Petro").Id;

            _service.Delete(existId);

            Assert.IsNull(_context.Pilots.Find(existId));
        }
    }
}
