using System.Collections.Generic;
using System.Threading.Tasks;
using Academy2018_.NET_Homework5.Core.Abstractions;
using Academy2018_.NET_Homework5.Infrastructure.Abstractions;
using Academy2018_.NET_Homework5.Shared.Exceptions;
using AutoMapper;
using FluentValidation;

namespace Academy2018_.NET_Homework5.Core.Services.Basic
{
    public abstract class BasicService<TEntity, TDto> : IService<TDto>
        where TEntity: class
        where TDto: class
    {
        private readonly IRepository<TEntity> _repository;
        private readonly IMapper _mapper;
        private readonly AbstractValidator<TEntity> _validator;

        protected BasicService(
            IRepository<TEntity> repository,
            IMapper mapper,
            AbstractValidator<TEntity> validator)
        {
            _repository = repository;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<IEnumerable<TDto>> GetAllAsync()
        {
            return _mapper.Map<IEnumerable<TEntity>, IEnumerable<TDto>>(
                await _repository.GetAsync());
        }

        public async Task<TDto> GetByIdAsync(object id)
        {
            var response = _mapper.Map<TEntity, TDto>(
                await _repository.GetAsync(id));

            if (response == null)
            {
                throw new NotExistException();
            }

            return response;
        }

        public async Task<object> AddAsync(TDto dto)
        {
            if (dto == null)
            {
                throw new NullBodyException();
            }

            var model = _mapper.Map<TDto, TEntity>(dto);
            var validationResult = await _validator.ValidateAsync(model);

            if (validationResult.IsValid)
            {
                return await _repository.CreateAsync(model);
            }
            else
            {
                throw new ValidationException(validationResult.Errors);
            }
        }

        public async Task UpdateAsync(object id, TDto dto)
        {
            if (dto == null)
            {
                throw new NullBodyException();
            }

            if (await _repository.IsExistAsync(id))
            {
                var model = _mapper.Map<TDto, TEntity>(dto);
                var validationResult = await _validator.ValidateAsync(model);

                if (validationResult.IsValid)
                {
                    await _repository.UpdateAsync(id, model);

                    await _repository.SaveChangesAsync();
                }
                else
                {
                    throw new ValidationException(validationResult.Errors);
                }
            }
            else
            {
                throw new NotExistException();
            }
        }

        public async Task DeleteAsync(object id)
        {
            if (await _repository.IsExistAsync(id))
            {
                await _repository.DeleteAsync(id);

                await _repository.SaveChangesAsync();
            }
            else
            {
                throw new NotExistException();
            }
        }
    }
}
