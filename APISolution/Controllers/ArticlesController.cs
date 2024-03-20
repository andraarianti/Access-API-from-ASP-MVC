using APISolution.BLL.DTOs;
using APISolution.BLL.Interfaces;
using APISolution.Domain;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APISolution.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ArticlesController : ControllerBase
	{

		private IArticleBLL _articleBLL;
		private IMapper _mapper;
		public ArticlesController(IArticleBLL articleBLL, IMapper mapper)
		{
			_articleBLL = articleBLL;
			_mapper = mapper;
		}

		// GET: api/<ArticlesController>
		[HttpGet]
		public async Task<IEnumerable<ArticleDTO>> GetAll()
		{
			var articles = await _articleBLL.GetArticleWithCategory();
			return articles;
		}

		//GET api/<ArticlesController>/5
		[HttpGet("{id}")]
		public async Task<ArticleDTO> GetArticleById(int id)
		{
			return await _articleBLL.GetArticleById(id);
		}

		[HttpGet("category/{categoryId}")]
		public async Task<IEnumerable<ArticleDTO>> GetArticleByCategory(int categoryId)
		{
			return await _articleBLL.GetArticleByCategory(categoryId);
		}

		[HttpGet("paging/{categoryId}/{pageNumber}/{pageSize}")]
		public async Task<IEnumerable<ArticleDTO>> GetWithPaging(int categoryId, int pageNumber, int pageSize)
		{
			return await _articleBLL.GetWithPaging(categoryId, pageNumber, pageSize);
		}

		[HttpGet("count")]
		public async Task<int> GetCountArticles()
		{
			return await _articleBLL.GetCountArticles();
		}

		//POST api/<ArticlesController>
		[HttpPost]
		public async Task<IActionResult> Post(ArticleCreateDTO article)
		{

			if (article == null)
			{
				return BadRequest();
			}

			var result = await _articleBLL.Insert(article);

			if (result != null)
			{
				return Ok(result);
			}
			else
			{
				return StatusCode(500, "Gagal menyimpan artikel baru.");
			}
		}

		// PUT api/<ArticlesController>/5
		[HttpPut("{id}")]
		public async Task<ActionResult<ArticleDTO>> Put(int id, [FromBody] ArticleUpdateDTO article)
		{
			var getArticle = await _articleBLL.GetArticleById(id);
			if (getArticle == null)
			{
				return NotFound($"Data Article dengan id {id} tidak ditemukan");
			}
			var result = await _articleBLL.Update(id, article);

			if (result != null)
			{
				return Ok(result);
			}
			else
			{
				return StatusCode(500, "Gagal mengupdate artikel.");
			}
		}

		// DELETE api/<ArticlesController>/5
		[HttpDelete("{id}")]
		public async Task<ActionResult<bool>> Delete(int id)
		{
			var result = await _articleBLL.Delete(id);
			if (!result)
			{
				return NotFound(); // Entitas tidak ditemukan, kembalikan respons 404 Not Found
			}
			return Ok(result);
		}
	}
}
