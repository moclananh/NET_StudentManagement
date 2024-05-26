using StudentManagement.Data.Models;
using StudentManagement.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Data.ViewModels.CategoryDTO;

namespace StudentManagement.Application.Categories
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext _context;

        public CategoryService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Category> Create(CreateCategoryRequest request)
        {
            var category = new Category()
            {
                Name = request.Name,
                CreatedDate = DateTime.Now,
                Status = Data.Models.Enums.Status.Active
            };

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<int> Update(UpdateCategoryRequest request)
        {
            {
                var category = await _context.Categories.FindAsync(request.Id);

                if (category == null) throw new Exception("cannot find this category");
                category.Name = request.Name;
                category.Status = request.Status;
                category.ModifiedDate = DateTime.Now;
                return await _context.SaveChangesAsync();
            }
        }

        public async Task<int> Delete(int categoryId)
        {
            var category = await _context.Categories.FindAsync(categoryId);
            if (category == null) throw new Exception("cannot find this category");

            _context.Categories.Remove(category);
            return await _context.SaveChangesAsync();
        }

        public async Task<List<Category>> GetAll(string? keyword)
        {
            var query = from c in _context.Categories
                        select new { c };
            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(x => x.c.Name.Contains(keyword));
            }

            return await query.Select(x => new Category()
            {
                Id = x.c.Id,
                Name = x.c.Name,
                CreatedDate = DateTime.Now,
                Status = x.c.Status,
                ModifiedDate = DateTime.Now
            }).ToListAsync();
        }

        public async Task<Category> GetById(int id)
        {
            var query = from c in _context.Categories
                        where c.Id == id
                        select new { c };
            return await query.Select(x => new Category()
            {

                Id = x.c.Id,
                Name = x.c.Name,
                CreatedDate = DateTime.Now,
                Status = x.c.Status,
                ModifiedDate = DateTime.Now
            }).FirstOrDefaultAsync();
        }

    }
}
