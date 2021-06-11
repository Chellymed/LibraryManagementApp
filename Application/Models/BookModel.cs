using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.Models
{
    public class BookModel : ModelBase
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string BookTypeId { get; set; }
        public BookTypeModel BookType { get; set; }
      //  [Required()]
        public string AuthorId { get; set; }
        public AuthorModel Author { get; set; }

    }
}
