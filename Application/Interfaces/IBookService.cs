using Application.Models;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IBookService 
    {
        Task<IEnumerable<BookModel>> GetAll();
        Task<BookModel> GetById(string id);
        Task<BookModel> Create(BookModel model);
        Task Update(BookModel model);
        Task Delete(BookModel model);
    }
}
