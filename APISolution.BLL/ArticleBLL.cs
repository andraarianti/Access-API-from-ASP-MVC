using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APISolution.BLL.DTOs;
using APISolution.BLL.Interfaces;
using APISolution.Data;
using APISolution.Domain;
using AutoMapper;

namespace APISolution.BLL
{
	public class ArticleBLL : IArticleBLL
	{
		private readonly IArticleData _articleData;
		private readonly IMapper _mapper;
		public ArticleBLL(IArticleData articleData, IMapper mapper)
		{
			_articleData = articleData;
			_mapper = mapper;
		}
		public async Task<bool> Delete(int id)
		{
			await _articleData.Delete(id);
			return true;
		}

		public async Task<IEnumerable<ArticleDTO>> GetArticleByCategory(int categoryId)
		{
			var articles = await _articleData.GetArticleByCategory(categoryId);
			var articlesDTO = _mapper.Map<IEnumerable<ArticleDTO>>(articles);
			return articlesDTO;
		}

		public async Task<ArticleDTO> GetArticleById(int id)
		{
			var articles = await _articleData.GetById(id);
			var articlesDTO = _mapper.Map<ArticleDTO>(articles);
			return articlesDTO;
		}

		public async Task<IEnumerable<ArticleDTO>> GetArticleWithCategory()
		{
			var articles = await _articleData.GetArticleWithCategory();
			var articlesDTO = _mapper.Map<IEnumerable<ArticleDTO>>(articles);
			return articlesDTO;
		}

		public async Task<int> GetCountArticles()
		{
			var articles = await _articleData.GetCountArticles();
			return articles;
		}

		public async Task<IEnumerable<ArticleDTO>> GetWithPaging(int categoryId, int pageNumber, int pageSize)
		{
			var articles = await _articleData.GetWithPaging(categoryId, pageNumber, pageSize);
			var articlesDTO = _mapper.Map<IEnumerable<ArticleDTO>>(articles);
			return articlesDTO;
		}

		public async Task<ArticleDTO> Insert(ArticleCreateDTO article)
		{
			//harus di map terlebih dahulu
			var insertedArticle = await _articleData.Insert(_mapper.Map<Article>(article));
			var insertedArticleDTO = _mapper.Map<ArticleDTO>(insertedArticle);
			return insertedArticleDTO;
		}

		public Task<int> InsertWithIdentity(ArticleCreateDTO article)
		{
			throw new NotImplementedException();
		}

		public async Task<ArticleDTO> Update(int id, ArticleUpdateDTO article)
		{
			//mapping data yang akan diubah 
			var update = await _articleData.Update(id, _mapper.Map<Article>(article));
			var articles = _mapper.Map<ArticleDTO>(update);
			return articles;
		}
	}
}
