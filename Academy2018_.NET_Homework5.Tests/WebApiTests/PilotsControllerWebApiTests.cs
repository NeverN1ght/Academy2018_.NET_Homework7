using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using Academy2018_.NET_Homework5.Infrastructure.Database;
using Academy2018_.NET_Homework5.Infrastructure.Models;
using Academy2018_.NET_Homework5.Shared.DTOs;
using Newtonsoft.Json;
using NUnit.Framework;
using OpenQA.Selenium;

namespace Academy2018_.NET_Homework5.Tests.WebApiTests
{
    [TestFixture]
    public class PilotsControllerWebApiTests
    {
        private AirportContext _context;
        private readonly string _baseUrl = "http://localhost:52898/api/pilots/";

        private void GetMethod(out HttpWebResponse response, string uri)
        {
            HttpWebRequest httpWebRequest = WebRequest.CreateHttp(uri);
            httpWebRequest.Method = "GET";
            httpWebRequest.ContentType = "application/json";
            response = httpWebRequest.GetResponse() as HttpWebResponse;
        }

        private void PostMethod(object entity, out HttpWebResponse response, string uri)
        {
            HttpWebRequest httpRequest = WebRequest.CreateHttp(uri);
            httpRequest.Method = "POST";
            httpRequest.ContentType = "application/json";
            var streamWriter = new StreamWriter(httpRequest.GetRequestStream());
            streamWriter.Write(JsonConvert.SerializeObject(entity));
            streamWriter.Close();
            response = httpRequest.GetResponse() as HttpWebResponse;
        }

        private void PutMethod(object entity, out HttpWebResponse response, string uri)
        {
            HttpWebRequest httpRequest = WebRequest.CreateHttp(uri);
            httpRequest.Method = "PUT";
            httpRequest.ContentType = "application/json";
            var streamWriter = new StreamWriter(httpRequest.GetRequestStream());
            streamWriter.Write(JsonConvert.SerializeObject(entity));
            streamWriter.Close();
            response = httpRequest.GetResponse() as HttpWebResponse;
        }

        private void DeleteMethod(out HttpWebResponse response, string uri)
        {
            HttpWebRequest httpRequest = WebRequest.CreateHttp(uri);
            httpRequest.Method = "DELETE";
            httpRequest.ContentType = "application/json";
            response = httpRequest.GetResponse() as HttpWebResponse;
        }

        [SetUp]
        public void Init()
        {
            _context = new AirportContext();

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
        public void Get_When_requested_Then_return_response_with_status_code_200()
        {
            GetMethod(out HttpWebResponse response, _baseUrl);
            var stream = response.GetResponseStream();
            var reader = new StreamReader(stream);
            var result = JsonConvert
                .DeserializeObject<IEnumerable<PilotDto>>(
                    reader.ReadToEnd()).ToList();

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

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public void Get_When_requested_with_existed_id_Then_return_response_with_status_code_200()
        {
            var existedId = _context.Pilots
                .FirstOrDefault(p => p.FirstName == "Mihail").Id;      

            GetMethod(out HttpWebResponse response, _baseUrl + existedId);
            var stream = response.GetResponseStream();
            var reader = new StreamReader(stream);
            var result = JsonConvert.DeserializeObject<PilotDto>(
                reader.ReadToEnd());

            Assert.IsNotNull(result);

            Assert.AreEqual(result.Id, existedId);
            Assert.AreEqual(result.FirstName, "Mihail");
            Assert.AreEqual(result.LastName, "Solomko");
            Assert.AreEqual(result.Experience, 10);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public void Post_When_requested_with_valid_body_Then_return_response_with_created_object_with_status_code_201()
        {
            var validPilotDto = new PilotDto
            {
                FirstName = "Oleg",
                LastName = "Nikolenko",
                Birthdate = new DateTime(1987, 2, 8),
                Experience = 4
            };

            PostMethod(validPilotDto, out HttpWebResponse response, _baseUrl);
            var stream = response.GetResponseStream();
            var reader = new StreamReader(stream);
            var result = JsonConvert.DeserializeObject<PilotDto>(reader.ReadToEnd());

            Assert.IsNotNull(result);

            Assert.AreEqual(result.FirstName, "Oleg");
            Assert.AreEqual(result.LastName, "Nikolenko");
            Assert.AreEqual(result.Experience, 4);

            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
        }

        [Test]
        public void Post_When_requested_with_not_valid_body_Then_return_response_with_status_code_400()
        {
            var notValidPilotDto = new PilotDto
            {
                FirstName = "s",
                LastName = "Nikolenko",
                Birthdate = new DateTime(1987, 2, 8),
                Experience = -2
            };

            HttpWebResponse result = new HttpWebResponse();
            try
            {
                PostMethod(notValidPilotDto, out HttpWebResponse response, _baseUrl);
            }
            catch (WebException ex)
            {
                result = ex.Response as HttpWebResponse;
            }           

            Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [Test]
        public void Delete_When_requested_with_existed_id_Then_return_response_with_status_code_204()
        {
            var existedId = _context.Pilots
                .FirstOrDefault(p => p.FirstName == "Mihail").Id;

            DeleteMethod(out HttpWebResponse response, _baseUrl + existedId);

            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Test]
        public void Put_When_requested_with_not_existed_id_Then_return_response_with_status_code_404()
        {
            var validPilotDto = new PilotDto
            {
                FirstName = "Mihail",
                LastName = "Solomko",
                Birthdate = new DateTime(1967, 1, 21),
                Experience = 10
            };
            var notExistedId = 968;

            HttpWebResponse result = new HttpWebResponse();
            try
            {
                PutMethod(validPilotDto, out HttpWebResponse response, _baseUrl + notExistedId);
            }
            catch (WebException ex)
            {
                result = ex.Response as HttpWebResponse;
            }

            Assert.AreEqual(HttpStatusCode.NotFound, result.StatusCode);
        }

        [Test]
        public void Put_When_requested_with_existed_id_Then_return_response_with_status_code_200()
        {
            var validPilotDto = new PilotDto
            {
                FirstName = "Mihail",
                LastName = "Solomko",
                Birthdate = new DateTime(1967, 1, 21),
                Experience = 10
            };
            var existedId = _context.Pilots
                .FirstOrDefault(p => p.FirstName == "Mihail").Id;

            PutMethod(validPilotDto, out HttpWebResponse response, _baseUrl + existedId);          

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public void Put_When_requested_with_not_valid_body_Then_return_response_with_status_code_400()
        {
            var notValidPilotDto = new PilotDto
            {
                FirstName = "M",
                LastName = "Solomko",
                Birthdate = new DateTime(2020, 1, 21),
                Experience = -3
            };
            var existedId = _context.Pilots
                .FirstOrDefault(p => p.FirstName == "Mihail").Id;

            HttpWebResponse result = new HttpWebResponse();
            try
            {
                PutMethod(notValidPilotDto, out HttpWebResponse response, _baseUrl + existedId);
            }
            catch (WebException ex)
            {
                result = ex.Response as HttpWebResponse;
            }

            Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [Test]
        public void Put_When_requested_with_valid_body_Then_return_response_with_status_code_200()
        {
            var validPilotDto = new PilotDto
            {
                FirstName = "Mihail",
                LastName = "Solomko",
                Birthdate = new DateTime(1967, 1, 21),
                Experience = 10
            };
            var existedId = _context.Pilots
                .FirstOrDefault(p => p.FirstName == "Mihail").Id;

            PutMethod(validPilotDto, out HttpWebResponse response, _baseUrl + existedId);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public void Delete_When_requested_with_not_existed_id_Then_return_response_with_status_code_404()
        {
            var notExistedId = 968;

            HttpWebResponse result = new HttpWebResponse();
            try
            {
                DeleteMethod(out HttpWebResponse response, _baseUrl + notExistedId);
            }
            catch (WebException ex)
            {
                result = ex.Response as HttpWebResponse;
            }

            Assert.AreEqual(HttpStatusCode.NotFound, result.StatusCode);
        }
    }
}
