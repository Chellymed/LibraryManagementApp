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
    public class BookService : IBookService
    {
        private readonly IBookRepository _repository;
        private readonly IMapper _mapper;
        public BookService(IBookRepository repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<BookModel> Create(BookModel model)
        {
            var mappedEntity = _mapper.Map<Book>(model);
            if (mappedEntity == null)
                throw new ApplicationException($"Entity could not be mapped.");

            var newEntity = await _repository.AddAsync(mappedEntity);

            var newMappedEntity = _mapper.Map<BookModel>(newEntity);
            return newMappedEntity;
        }

        public async Task Delete(BookModel model)
        {
            var deletedEntity = await _repository.GetByIdAsync(model.Id);
            if (deletedEntity == null)
                   throw new ApplicationException($"Entity could not be loaded.");

                await _repository.DeleteAsync(deletedEntity);
        }

        public async Task<IEnumerable<BookModel>> GetAll()
        {
            var entityList = await _repository.GetAllAsync();
            var mapped = _mapper.Map<IEnumerable<BookModel>>(entityList);
            return mapped;
        }

        public async Task<BookModel> GetById(string id)
        {
            var entity = await _repository.GetByIdAsync(id);
            var mapped = _mapper.Map<BookModel>(entity);
            return mapped;
        }

        public async Task Update(BookModel model)
        {
            var editEntity = await _repository.GetByIdAsync(model.Id);
            if (editEntity == null)
                throw new ApplicationException($"Entity could not be loaded.");

            _mapper.Map(model, editEntity);

            await _repository.UpdateAsync(editEntity);
        }
    }
}
