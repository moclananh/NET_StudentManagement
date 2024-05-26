using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Application;
using StudentManagement.Data;
using StudentManagement.Data.Models;
using StudentManagement.Data.ViewModels.CategoryDTO;
using StudentManagement.Data.ViewModels.ProductDTO;

namespace StudentManagement.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoriesController(IUnitOfWork unitOfWork)
        {
           _unitOfWork = unitOfWork;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll([FromQuery] string keyword)
        {
            var item = await _unitOfWork.CategoryService.GetAll(keyword);
            return Ok(item);
        }

        [HttpGet("{categoryId}")]
        public async Task<IActionResult> GetById(int categoryId)
        {
            var cat = await _unitOfWork.CategoryService.GetById(categoryId);
            if (cat == null)
                return BadRequest("Cannot find product");
            return Ok(cat);
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCategoryRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _unitOfWork.CategoryService.Create(request);
            if (result == null)
                return BadRequest();
            return Ok(result);
        }

        [HttpPut("{categoryId}")]
        public async Task<IActionResult> Update([FromRoute] int categoryId, [FromBody] UpdateCategoryRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            request.Id = categoryId;
            var affectedResult = await _unitOfWork.CategoryService.Update(request);
            if (affectedResult == 0)
                return BadRequest();
            return Ok();
        }

        [HttpDelete("{categoryId}")]
        public async Task<IActionResult> Delete(int categoryId)
        {
            var affectedResult = await _unitOfWork.CategoryService.Delete(categoryId);
            if (affectedResult == 0)
                return BadRequest();
            return Ok();
        }

    }
}
