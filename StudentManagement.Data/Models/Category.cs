using StudentManagement.Data.Models.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Data.Models
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public List<ProductInCategory> ProductInCategories { get; set; }
    }
}
