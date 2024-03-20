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
	public class CategoryBLL : ICategoryBLL
	{
		private readonly ICategoryData _categoryData;
		private readonly IMapper _mapper;

        public CategoryBLL(ICategoryData categoryData, IMapper mapper)
        {
            _categoryData = categoryData;
			_mapper = mapper;
        }

        public async Task<bool> Delete(int id)
		{
			var categories = await _categoryData.GetById(id);
			if (categories == null)
			{
				return false;
			}
			await _categoryData.Delete(id);
			return true;
		}

		public async Task<IEnumerable<CategoryDTO>> GetAll()
		{
			var categories = await _categoryData.GetAll();
			var categoriesDTO = _mapper.Map<IEnumerable<CategoryDTO>>(categories);
			return categoriesDTO;
		}

		public async Task<CategoryDTO> GetById(int id)
		{
			var categories = await _categoryData.GetById(id);
			var categoriesDTO = _mapper.Map<CategoryDTO>(categories);
			return categoriesDTO;
		}

		public async Task<IEnumerable<CategoryDTO>> GetByName(string name)
		{
			var categories = await _categoryData.GetByName(name);
			var categoriesDTO = _mapper.Map<IEnumerable<CategoryDTO>>(categories);
			return categoriesDTO;
		}

		public async Task<int> GetCountCategories(string name)
		{
			var categories = await _categoryData.GetCountCategories(name);
			return categories;
		}

		public async Task<IEnumerable<CategoryDTO>> GetWithPaging(int pageNumber, int pageSize, string name)
		{
			var categories = await _categoryData.GetWithPaging(pageNumber, pageSize, name);
			var categoriesDTO = _mapper.Map<IEnumerable<CategoryDTO>>(categories);
			return categoriesDTO;
		}

		public async Task<CategoryDTO> Insert(CategoryCreateDTO entity)
		{
			var categories = await _categoryData.Insert(_mapper.Map<Category>(entity));
			var categoriesDTO = _mapper.Map<CategoryDTO>(categories);
			return categoriesDTO;
		}

		public async Task<CategoryDTO> Update(CategoryUpdateDTO entity)
		{
			var categories = await _categoryData.Update(entity.CategoryID, _mapper.Map<Category>(entity));
			var categoriesDTO = _mapper.Map<CategoryDTO>(categories);
			return categoriesDTO;
		}
	}
}
