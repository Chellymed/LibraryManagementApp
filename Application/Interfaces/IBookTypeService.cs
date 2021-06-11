using Application.Models;
using Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IBookTypeService
    {
        Task<IEnumerable<BookTypeModel>> GetAll();
        Task<BookTypeModel> GetById(string id);
        Task<BookTypeModel> Create(BookTypeModel model);
        Task Update(BookTypeModel model);
        Task Delete(BookTypeModel model);
    }
}
