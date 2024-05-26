using StudentManagement.Application.Categories;
using StudentManagement.Application.Products;
using StudentManagement.Application.Users;

namespace StudentManagement.Application
{
    public interface IUnitOfWork
    {
        public IProductService ProductService { get; }
        public ICategoryService CategoryService { get; }
        public IUserService UserService { get; }

    }
}
