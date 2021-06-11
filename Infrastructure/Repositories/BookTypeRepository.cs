using Core.Entities;
using Core.Repositories;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repositories
{
    public class BookTypeRepository : Repository<BookType> , IBookTypeRepository
    {
        public BookTypeRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
