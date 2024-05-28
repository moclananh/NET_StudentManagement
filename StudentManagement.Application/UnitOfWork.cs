using StudentManagement.Application.Categories;
using StudentManagement.Application.EmailService;
using StudentManagement.Application.Products;
using StudentManagement.Application.Roles;
using StudentManagement.Application.Users;

namespace StudentManagement.Application
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IUserService _userService;
        private readonly IEmailService _emailService;
        private readonly IRoleService _roleService;

        public UnitOfWork(
            IProductService productService,
            ICategoryService categoryService,
            IUserService userService,
            IEmailService emailService,
            IRoleService roleService)
        {
            // _dbContext = dbContext;
            _productService = productService;
            _categoryService = categoryService;
            _userService = userService;
            _emailService = emailService;
            _roleService = roleService;
        }
        public IProductService ProductService => _productService;
        public ICategoryService CategoryService => _categoryService;
        public IUserService UserService => _userService;
        public IEmailService EmailService => _emailService;
        public IRoleService RoleService => _roleService;

    }
}
