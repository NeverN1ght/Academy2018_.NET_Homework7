using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Academy2018_.NET_Homework5.Infrastructure.Abstractions;
using Academy2018_.NET_Homework5.Infrastructure.Models;
using Academy2018_.NET_Homework5.Infrastructure.Repositories;
using Academy2018_.NET_Homework5.Shared.DTOs;
using Academy2018_.NET_Homework5.Shared.DTOs.Json;
using Academy2018_.NET_Homework5.Shared.Exceptions;
using AutoMapper;
using FluentValidation;
using Newtonsoft.Json;

namespace Academy2018_.NET_Homework5.Core.Services.Data
{
    public class CrewsLoadService
    {
        private readonly CrewsRepository _repository;
        private readonly IMapper _mapper;
        private readonly AbstractValidator<Crew> _validator;

        public CrewsLoadService(
            CrewsRepository repository,
            IMapper mapper,
            AbstractValidator<Crew> validator)
        {
            _repository = repository;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<List<JsonCrewDto>> GetDataAsync(string uri)
        {
            HttpWebRequest httpWebRequest = WebRequest.CreateHttp(uri);
            httpWebRequest.Method = "GET";
            httpWebRequest.ContentType = "application/json";
            var response = await httpWebRequest.GetResponseAsync() as HttpWebResponse;
            var stream = response.GetResponseStream();
            var reader = new StreamReader(stream);
            var result = JsonConvert.DeserializeObject<List<JsonCrewDto>>(
                await reader.ReadToEndAsync());

            return result;
        }

        public async Task SaveToDbAsync(List<JsonCrewDto> data)
        {
            if (data == null || data.Count == 0)
            {
                throw new NullBodyException();
            }

            var dataToAdd = data.Select(d => d).Take(10).ToList();
            var models = _mapper.Map<List<JsonCrewDto>, List<Crew>>(dataToAdd);

            foreach (var model in models)
            {
                await _validator.ValidateAndThrowAsync(model);
            }

            await _repository.AddRangeAsync(models);
        }

        public async Task WriteLogAsync(List<JsonCrewDto> data)
        {

        }
    }
}
