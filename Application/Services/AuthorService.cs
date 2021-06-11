using Application.Interfaces;
using Application.Models;
using AutoMapper;
using Core.Entities;
using Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
  
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _repository;
        private readonly IMapper _mapper;
        public AuthorService(IAuthorRepository repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<AuthorModel> Create(AuthorModel model)
        {
            var mappedEntity = _mapper.Map<Author>(model);
            if (mappedEntity == null)
                throw new ApplicationException($"Entity could not be mapped.");

            var newEntity = await _repository.AddAsync(mappedEntity);

            var newMappedEntity = _mapper.Map<AuthorModel>(newEntity);
            return newMappedEntity;
        }

        public async Task Delete(AuthorModel model)
        {
            var deletedEntity = await _repository.GetByIdAsync(model.Id);
            if (deletedEntity == null)
                throw new ApplicationException($"Entity could not be loaded.");

            await _repository.DeleteAsync(deletedEntity);
        }

        public async Task<IEnumerable<AuthorModel>> GetAll()
        {
            var entityList = await _repository.GetAllAsync();
            var mapped = _mapper.Map<IEnumerable<AuthorModel>>(entityList);
            return mapped;
        }

        public async Task<AuthorModel> GetById(string id)
        {
            var entity = await _repository.GetByIdAsync(id);
            var mapped = _mapper.Map<AuthorModel>(entity);
            return mapped;
        }

        public async Task Update(AuthorModel model)
        {
            var editEntity = await _repository.GetByIdAsync(model.Id);
            if (editEntity == null)
                throw new ApplicationException($"Entity could not be loaded.");

            _mapper.Map(model, editEntity);

            await _repository.UpdateAsync(editEntity);
        }
    }
}
