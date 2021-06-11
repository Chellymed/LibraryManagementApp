using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;


namespace Core.Entities
{
    [Table("Books")]
    public class Book : EntityBase
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string BookTypeId { get; set; }
        public BookType BookType { get; set; }
        //[Required()]
        public string AuthorId { get; set; }
        public Author Author { get; set; }
    }
}
