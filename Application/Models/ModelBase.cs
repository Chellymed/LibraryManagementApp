using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Models
{
    public abstract class ModelBase
    {
        public string Id { get; set; }
        public string CreatedBy { get; set; }
        public string LastChangeBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? LastChangeOn { get; set; }
        public string Comment { get; set; }
    }
}
