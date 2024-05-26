using StudentManagement.Application.Categories;
using StudentManagement.Application.Products;
using StudentManagement.Application.Users;

namespace StudentManagement.Application
{
    public class UnitOfWork : IUnitOfWork
    {
        //  private readonly ApplicationDBContext _dbContext;
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IUserService _userService;

        public UnitOfWork(
            IProductService productService,
            ICategoryService categoryService,
            IUserService userService)
        {
            // _dbContext = dbContext;
            _productService = productService;
            _categoryService = categoryService;
            _userService = userService;
        }
        public IProductService ProductService => _productService;
        public ICategoryService CategoryService => _categoryService;
        public IUserService UserService => _userService;

    }
}
