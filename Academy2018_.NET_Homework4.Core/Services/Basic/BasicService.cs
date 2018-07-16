using System.Collections.Generic;
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

        public IEnumerable<TDto> GetAll()
        {
            return _mapper.Map<IEnumerable<TEntity>, IEnumerable<TDto>>(
                _repository.Get());
        }

        public TDto GetById(object id)
        {
            var response = _mapper.Map<TEntity, TDto>(
                _repository.Get(id));

            if (response == null)
            {
                throw new NotExistException();
            }

            return response;
        }

        public object Add(TDto dto)
        {
            if (dto == null)
            {
                throw new NullBodyException();
            }

            var model = _mapper.Map<TDto, TEntity>(dto);
            var validationResult = _validator.Validate(model);

            if (validationResult.IsValid)
            {
                return _repository.Create(model);
            }
            else
            {
                throw new ValidationException(validationResult.Errors);
            }
        }

        public void Update(object id, TDto dto)
        {
            if (dto == null)
            {
                throw new NullBodyException();
            }

            if (_repository.IsExist(id))
            {
                var model = _mapper.Map<TDto, TEntity>(dto);
                var validationResult = _validator.Validate(model);

                if (validationResult.IsValid)
                {
                    _repository.Update(id, model);

                    _repository.SaveChanges();
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

        public void Delete(object id)
        {
            if (_repository.IsExist(id))
            {
                _repository.Delete(id);

                _repository.SaveChanges();
            }
            else
            {
                throw new NotExistException();
            }
        }
    }
}
