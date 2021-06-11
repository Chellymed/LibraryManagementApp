using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Models
{
    public class AuthorModel : ModelBase
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
    }
}
