using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APISolution.Domain;
using Microsoft.EntityFrameworkCore;

namespace APISolution.Data
{
	public class ArticleData : IArticleData
	{
		private readonly AppDbContext _context;

        public ArticleData(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Delete(int id)
		{
			var article = await _context.Categories.FirstOrDefaultAsync(c => c.CategoryId == id);
			if (article == null)
			{
				return false; // Entitas tidak ditemukan, kembalikan false atau respons yang sesuai
			}
			_context.Categories.Remove(article);
			await _context.SaveChangesAsync();
			return true;
		}

		public async Task<IEnumerable<Article>> GetAll()
		{
			var articles = await _context.Articles.OrderBy(c => c.Title).ToListAsync();
			return articles;
		}

		public async Task<IEnumerable<Article>> GetArticleByCategory(int categoryId)
		{
			var articles = await _context.Articles.Where(c => c.CategoryId == categoryId).ToListAsync();
			return articles;
		}

		public async Task<IEnumerable<Article>> GetArticleWithCategory()
		{
			var articles = await _context.Articles.Include(c => c.Category).ToListAsync();
			return articles;
		}

		public async Task<Article> GetById(int id)
		{
			var articles = await _context.Articles.Where(c => c.ArticleId == id).FirstOrDefaultAsync();
			return articles;
		}

		public async Task<int> GetCountArticles()
		{
			var categories = await _context.Articles.CountAsync();
			return categories;
		}

		public async Task<IEnumerable<Article>> GetWithPaging(int categoryId, int pageNumber, int pageSize)
		{
			var articles = await _context.Articles.Where(c => c.CategoryId == categoryId)
				.Skip((pageNumber - 1) * pageSize)
				.Take(pageSize)
				.ToListAsync();
			return articles;
		}

		public async Task<Article> Insert(Article entity)
		{
			var article = new Article
			{
				CategoryId = entity.CategoryId,
				Title = entity.Title,
				Details = entity.Details,
				IsApproved = entity.IsApproved,
				Pic = entity.Pic
			};
			await _context.Articles.AddAsync(article);
			await _context.SaveChangesAsync();
			return article;
		}

		public async Task<Task> InsertArticleWithCategory(Article article)
		{
			try
			{
				_context.Categories.Add(article.Category);
				_context.Articles.Add(article);
				await _context.SaveChangesAsync();
				return Task.CompletedTask;
			}
			catch (Exception ex)
			{

				throw new Exception(ex.Message);
			}
		}

		public Task<int> InsertWithIdentity(Article article)
		{
			throw new NotImplementedException();
		}

		public async Task<Article> Update(int id, Article entity)
		{
			var article = await GetById(id);
			if (article != null)
			{
				article.CategoryId = entity.CategoryId;
				article.Title = entity.Title;
				article.Details = entity.Details;
				article.PublishDate = entity.PublishDate;
				article.IsApproved = entity.IsApproved;
				article.Pic = entity.Pic;

				await _context.SaveChangesAsync();

				return article;
			}

			return null;
		}
	}
}
