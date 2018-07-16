using Academy2018_.NET_Homework5.Core.Services.Basic;
using Academy2018_.NET_Homework5.Infrastructure.Abstractions;
using Academy2018_.NET_Homework5.Infrastructure.Models;
using Academy2018_.NET_Homework5.Shared.DTOs;
using AutoMapper;
using FluentValidation;

namespace Academy2018_.NET_Homework5.Core.Services
{
    public class StewardessesService: BasicService<Stewardesse, StewardesseDto>
    {
        public StewardessesService(
            IRepository<Stewardesse> repository,
            IMapper mapper,
            AbstractValidator<Stewardesse> validator) : base(repository, mapper, validator)
        {
        }
    }
}

