using StudentManagement.Application.Products;
using StudentManagement.Data.Models;
using StudentManagement.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Data.ViewModels.ProductDTO;
using StudentManagement.Data.ViewModels.CategoryDTO;
using StudentManagement.Data.ViewModels.Commons;
using AutoMapper;

namespace StudentManagement.Application.Products
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public ProductService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }

        public async Task<Product> Create(CreateProductRequest request)
        {
            var Product = new Product()
            {
                CreatedDate = DateTime.Now,
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                Status = Data.Models.Enums.Status.Active,

            };

            _context.Products.Add(Product);
            await _context.SaveChangesAsync();
            return Product;
        }

        public async Task<int> Update(UpdateProductRequest request)
        {
            {
                var Product = await _context.Products.FindAsync(request.ProductId);

                if (Product == null) throw new Exception("cannot find this Product");

                Product.Name = request.Name;
                Product.Description = request.Description;
                Product.Price = request.Price;
                Product.ModifiedDate = DateTime.Now;
                Product.Status = request.Status;
                return await _context.SaveChangesAsync();
            }
        }

        public async Task<int> Delete(int ProductId)
        {
            var Product = await _context.Products.FindAsync(ProductId);
            if (Product == null) throw new Exception("cannot find this Product");

            _context.Products.Remove(Product);
            return await _context.SaveChangesAsync();
        }

        public async Task<List<ProductVm>> GetAll(string? keyword)
        {
            /* var query = from p in _context.Products
                         select new { p };


             if (!string.IsNullOrEmpty(keyword))
             {
                 query = query.Where(x => x.p.Name.Contains(keyword));
             }

             return await query.Select(x => new ProductVm()
             {
                 Id = x.p.Id,
                 Status = x.p.Status,
                 CreatedDate = x.p.CreatedDate,
                 ModifiedDate = x.p.ModifiedDate,
                 Name = x.p.Name,
                 Description = x.p.Description,
                 Price = x.p.Price,

             }).ToListAsync();*/

            var query = await _context.Products.AsNoTracking().ToListAsync();

            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(x => x.Name.Contains(keyword)).ToList();
            }
            return _mapper.Map<List<ProductVm>>(query);
        }

        public async Task<ProductVm> GetById(int id)
        {
            /*  var product = await _context.Products.FindAsync(id);
              var categories = await (from c in _context.Categories
                                      join pic in _context.ProductInCategories on c.Id equals pic.CategoryId
                                      where pic.ProductId == id
                                      select new CategoryVm
                                      {
                                          Id = c.Id,
                                          Name = c.Name,
                                      }).AsNoTracking().AsQueryable().ToListAsync(); // phai co asNoTracking de ignore vong lap vo hang;

              var productViewModel = new ProductVm()
              {
                  Id = product.Id,
                  Status = product.Status,
                  CreatedDate = product.CreatedDate,
                  ModifiedDate = product.ModifiedDate,
                  Name = product.Name,
                  Description = product.Description,
                  Price = product.Price,
                  Categories = categories,
              };
              return productViewModel;
           */
            var product = await _context.Products
                .Include(p => p.ProductInCategories)
                .ThenInclude(pic => pic.Category)
                .AsNoTracking() // Optional: Prevents tracking for read-only queries
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
                return null;

            return _mapper.Map<ProductVm>(product);

        }

        public async Task<int> CategoryAssign(int id, CategoryAssignRequest request)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                throw new Exception("Id not exist!");
            }

            List<SelectItem> mang = new List<SelectItem>();

            foreach (var item in request.Categories)
            {
                mang.Add(item);
                // Check if the product with the same ID is already in the distinctProducts list
            }

            foreach (var category in mang)
            {
                var productInCategory = await _context.ProductInCategories
                    .FirstOrDefaultAsync(x => x.CategoryId == int.Parse(category.Id)
                    && x.ProductId == id);
                if (productInCategory != null && category.Selected == false)
                {
                    _context.ProductInCategories.Remove(productInCategory);
                }
                else if (productInCategory == null && category.Selected)
                {
                    await _context.ProductInCategories.AddAsync(new ProductInCategory()
                    {
                        CategoryId = int.Parse(category.Id),
                        ProductId = id
                    });
                }
            }

            return await _context.SaveChangesAsync();
        }
    }
}
