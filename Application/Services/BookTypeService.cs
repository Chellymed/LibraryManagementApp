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
    public class BookTypeService : IBookTypeService
    {
        private readonly IBookTypeRepository _repository;
        private readonly IMapper _mapper;
        public BookTypeService(IBookTypeRepository repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<BookTypeModel> Create(BookTypeModel model)
        {
            var mappedEntity = _mapper.Map<BookType>(model);
            if (mappedEntity == null)
                throw new ApplicationException($"Entity could not be mapped.");

            var newEntity = await _repository.AddAsync(mappedEntity);

            var newMappedEntity = _mapper.Map<BookTypeModel>(newEntity);
            return newMappedEntity;
        }

        public async Task Delete(BookTypeModel model)
        {
            var deletedEntity = await _repository.GetByIdAsync(model.Id);
            if (deletedEntity == null)
                throw new ApplicationException($"Entity could not be loaded.");

            await _repository.DeleteAsync(deletedEntity);
        }

        public async Task<IEnumerable<BookTypeModel>> GetAll()
        {
            var entityList = await _repository.GetAllAsync();
            var mapped = _mapper.Map<IEnumerable<BookTypeModel>>(entityList);
            return mapped;
        }

        public async Task<BookTypeModel> GetById(string id)
        {
            var entity = await _repository.GetByIdAsync(id);
            var mapped = _mapper.Map<BookTypeModel>(entity);
            return mapped;
        }

        public async Task Update(BookTypeModel model)
        {
            var editEntity = await _repository.GetByIdAsync(model.Id);
            if (editEntity == null)
                throw new ApplicationException($"Entity could not be loaded.");

            _mapper.Map(model, editEntity);

            await _repository.UpdateAsync(editEntity);
        }
    }
}

