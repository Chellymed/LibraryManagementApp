using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.Entities
{
    [Table("BookTypes")]
    public class BookType : EntityBase
    {
        public string Designation { get; set; }
        public List<Book> Books { get; set; } = new List<Book>();
    }
}
