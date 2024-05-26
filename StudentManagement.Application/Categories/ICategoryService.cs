using StudentManagement.Data.Models;
using StudentManagement.Data.ViewModels.CategoryDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Application.Categories
{
    public interface ICategoryService
    {
        Task<List<Category>> GetAll(string? keyword);
        Task<Category> GetById(int id);
        Task<Category> Create(CreateCategoryRequest request);
        Task<int> Update(UpdateCategoryRequest request);
        Task<int> Delete(int categoryId);
        //Task<PagedResult<CategoryVm>> GetAllPaging(GetCategoryPagingRequest request);
    }

}
