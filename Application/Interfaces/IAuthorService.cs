using Application.Models;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IAuthorService
    {
        Task<IEnumerable<AuthorModel>> GetAll();
        Task<AuthorModel> GetById(string id);
        Task<AuthorModel> Create(AuthorModel model);
        Task Update(AuthorModel model);
        Task Delete(AuthorModel model);
    }
}
