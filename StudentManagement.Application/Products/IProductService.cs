using StudentManagement.Data.Models;
using StudentManagement.Data.ViewModels.ProductDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Application.Products
{
    public interface IProductService
    {
        Task<List<ProductVm>> GetAll(string? keyword);
        Task<ProductVm> GetById(int id);
        Task<Product> Create(CreateProductRequest request);
        Task<int> Update(UpdateProductRequest request);
        Task<int> Delete(int ProductId);

        Task<int> CategoryAssign(int id, CategoryAssignRequest request);
    }
}
