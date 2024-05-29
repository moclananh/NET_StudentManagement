using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Application;
using StudentManagement.Data;
using StudentManagement.Data.Models;
using StudentManagement.Data.ViewModels.ProductDTO;

namespace StudentManagement.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("GetAll")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll([FromQuery] string keyword)
        {
            var item = await _unitOfWork.ProductService.GetAll(keyword);
            return Ok(item);
        }

        [HttpGet("GetById/{productId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(int productId)
        {
            var product = await _unitOfWork.ProductService.GetById(productId);
            if (product == null)
                return BadRequest("Cannot find product");
            return Ok(product);
        }


        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] CreateProductRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _unitOfWork.ProductService.Create(request);
            if (result == null)
                return BadRequest();
            return Ok(result);
        }

        [HttpPut("Update/{productId}")]
        public async Task<IActionResult> Update([FromRoute] int productId, [FromBody] UpdateProductRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            request.ProductId = productId;
            var affectedResult = await _unitOfWork.ProductService.Update(request);
            if (affectedResult == 0)
                return BadRequest();
            return Ok();
        }

        [HttpDelete("Delete/{productId}")]
        public async Task<IActionResult> Delete(int productId)
        {
            var affectedResult = await _unitOfWork.ProductService.Delete(productId);
            if (affectedResult == 0)
                return BadRequest();
            return Ok();
        }

        [HttpPut("Assign/{id}/categories")]
        public async Task<IActionResult> CategoryAssign(int id, [FromBody] CategoryAssignRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _unitOfWork.ProductService.CategoryAssign(id, request);
          
            return Ok();
        }
    }
}
