using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APISolution.Domain;
using Microsoft.EntityFrameworkCore;

namespace APISolution.Data
{
	public class CategoryData : ICategoryData
	{
		private readonly AppDbContext _context;
		public CategoryData(AppDbContext context)
		{
			_context = context;
		}

		public Task<bool> Delete(int id)
		{
			var categories = _context.Categories.Where(c => c.CategoryId == id).FirstOrDefault();
			_context.Categories.Remove(categories);
			_context.SaveChanges();
			return Task.FromResult(true);
		}

		public async Task<IEnumerable<Category>> GetAll()
		{
			var categories = await _context.Categories.OrderBy(c => c.CategoryName).ToListAsync();
			return categories;
		}

		public async Task<Category> GetById(int id)
		{
			var categories = await _context.Categories.Where(c => c.CategoryId == id).FirstOrDefaultAsync();
			return categories;
		}

		public async Task<IEnumerable<Category>> GetByName(string name)
		{
			var categories = await _context.Categories.Where(c => c.CategoryName.Contains(name)).ToListAsync();
			return categories;
		}

		public async Task<int> GetCountCategories(string name)
		{
			var categories = await _context.Categories.Where(c => c.CategoryName.Contains(name)).CountAsync();
			return categories;
		}

		public async Task<IEnumerable<Category>> GetWithPaging(int pageNumber, int pageSize, string name = "")
		{
			var categories = await _context.Categories.Where(c => c.CategoryName.Contains(name))
				.Skip((pageNumber - 1) * pageSize)
				.Take(pageSize)
				.ToListAsync();
			return categories;
		}

		public async Task<Category> Insert(Category entity)
		{
			var category = new Category
			{
				CategoryName = entity.CategoryName
			};
			await _context.Categories.AddAsync(category);
			await _context.SaveChangesAsync();
			return category;
		}

		public async Task<int> InsertWithIdentity(Category category)
		{
			var categories = await _context.Categories.AddAsync(category);
			await _context.SaveChangesAsync();
			return categories.Entity.CategoryId;
		}

		public async Task<Category> Update(int id, Category entity)
		{
			var categories = await _context.Categories.Where(c => c.CategoryId == id).FirstOrDefaultAsync();
			categories.CategoryName = entity.CategoryName;
			await _context.SaveChangesAsync();
			return categories;

		}
	}
}
