using Application.Models;
using AutoMapper;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<EntityBase, ModelBase>();
            CreateMap<ModelBase, EntityBase>().ForMember(opt => opt.CreatedBy, prop => prop.Ignore())
                                                    .ForMember(opt => opt.CreatedOn, prop => prop.Ignore())
                                                    .ForMember(opt => opt.LastChangeBy, prop => prop.Ignore())
                                                    .ForMember(opt => opt.LastChangeOn, prop => prop.Ignore());
            CreateMap<BookModel, Book>().IncludeBase<ModelBase, EntityBase>().ForMember(prop => prop.Id, opt => opt.Ignore());
            CreateMap<Book, BookModel>().IncludeBase<EntityBase, ModelBase>();
            CreateMap<BookTypeModel, BookType>().IncludeBase<ModelBase, EntityBase>().ForMember(prop => prop.Id, opt => opt.Ignore());
            CreateMap<BookType, BookTypeModel>().IncludeBase<EntityBase, ModelBase>();
            CreateMap<AuthorModel, Author>().IncludeBase<ModelBase, EntityBase>().ForMember(prop => prop.Id, opt => opt.Ignore());
            CreateMap<Author, AuthorModel>().IncludeBase<EntityBase, ModelBase>();
        }
    }
}
