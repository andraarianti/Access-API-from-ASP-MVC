using APISolution.BLL.DTOs;
using APISolution.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APISolution.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CategoriesController : ControllerBase
	{
		private ICategoryBLL _categoryBLL;
		public CategoriesController(ICategoryBLL categoryBLL)
		{
			_categoryBLL = categoryBLL;
		}

		// GET: api/Categories
		[HttpGet]
		public async Task<IEnumerable<CategoryDTO>> GetAll()
		{
			var results = await _categoryBLL.GetAll();
			return results;
		}

		//GET api/Categories/5
		[HttpGet("{id}")]
		public async Task<CategoryDTO> GetById(int id)
		{
			return await _categoryBLL.GetById(id);
		}

		[HttpGet("Name/{name}")]
		public async Task<IEnumerable<CategoryDTO>> GetByName(string name)
		{
			return await _categoryBLL.GetByName(name);
		}

		[HttpGet("Count/{name}")]
		public async Task<int> GetCountCategories(string name)
		{
			return await _categoryBLL.GetCountCategories(name);
		}

		[HttpGet("Paging/{pageNumber}/{pageSize}/{name}")]
		public async Task<IEnumerable<CategoryDTO>> GetWithPaging(int pageNumber, int pageSize, string name)
		{
			return await _categoryBLL.GetWithPaging(pageNumber, pageSize, name);
		}

		// POST api/<Categories_Controller>
		[HttpPost]
		public async Task<CategoryDTO> Post(CategoryCreateDTO category)
		{
			return await _categoryBLL.Insert(category);
		}

		[HttpPut]
		public async Task<CategoryDTO> Put(CategoryUpdateDTO category)
		{
			return await _categoryBLL.Update(category);
		}

		// DELETE api/<Categories_Controller>/5
		[HttpDelete("{id}")]
		public async Task<bool> Delete(int id)
		{
			var result = await _categoryBLL.Delete(id);
			if(result)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
	}
}
