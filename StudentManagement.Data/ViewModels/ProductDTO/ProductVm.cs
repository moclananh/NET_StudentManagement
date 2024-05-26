using StudentManagement.Data.Models;
using StudentManagement.Data.Models.Enums;
using StudentManagement.Data.ViewModels.CategoryDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Data.ViewModels.ProductDTO
{
    public class ProductVm
    {
        public int Id { get; set; }
        public Status Status { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        public List<CategoryVm> Categories { get; set; } = null;
    }
}
