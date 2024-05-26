using StudentManagement.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Data.Models.BaseEntities
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public Status Status { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set;}
    }
}
