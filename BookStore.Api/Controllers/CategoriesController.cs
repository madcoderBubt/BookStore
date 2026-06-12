using BookStore.Domain.Entities;
using BookStore.Domain.Interfaces;
using BookStore.Shared.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoriesController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        public IActionResult GetCategories()
        {
            var categories = _categoryRepository.Categories.Select(c => new CategoryDto
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description
            }).ToList();

            return Ok(categories);
        }

        [HttpPost]
        [Authorize]
        public IActionResult CreateCategory([FromBody] CategoryDto categoryDto)
        {
            var category = new Category
            {
                Name = categoryDto.Name,
                Description = categoryDto.Description
            };
            _categoryRepository.AddEdit(category);
            return Ok(categoryDto);
        }

        [HttpPut("{id}")]
        [Authorize]
        public IActionResult UpdateCategory(int id, [FromBody] CategoryDto categoryDto)
        {
            var category = _categoryRepository.Categories.FirstOrDefault(c => c.Id == id);
            if (category == null) return NotFound();

            category.Name = categoryDto.Name;
            category.Description = categoryDto.Description;
            _categoryRepository.AddEdit(category);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult DeleteCategory(int id)
        {
            var category = _categoryRepository.Categories.FirstOrDefault(c => c.Id == id);
            if (category == null) return NotFound();

            _categoryRepository.Delete(id);
            return NoContent();
        }
    }
}
