using System.Text;
using System.Text.Json;
using APISolution.BLL.DTOs;

namespace SampleMVC.Services
{
    public class CategoryServices : ICategoryServices
	{
		private readonly HttpClient _client;
		private readonly IConfiguration _configuration;
		private ILogger<CategoryServices> _logger;

        public CategoryServices(HttpClient client, IConfiguration configuration, ILogger<CategoryServices> logger)
        {
            _client = client;
			_configuration = configuration;
			_logger = logger;
        }

		private string GetBaseUrl()
		{
			return _configuration["BaseUrl"] + "Categories";
		}

		public async Task<CategoryDTO> Create(CategoryCreateDTO category)
		{
			var json = JsonSerializer.Serialize(category);
			var data = new StringContent(json, Encoding.UTF8, "application/json");
			var httpResponse = await _client.PostAsync(GetBaseUrl(), data);

			if (!httpResponse.IsSuccessStatusCode)
			{
				throw new Exception("cannot create category");
			}

			var content = await httpResponse.Content.ReadAsStringAsync();
			var newCategory = JsonSerializer.Deserialize<CategoryDTO>(content, new JsonSerializerOptions
			{
				PropertyNameCaseInsensitive = true
			});
			return newCategory;
		}

		public async Task Delete(int id)
		{
			var httpResponse = await _client.DeleteAsync(GetBaseUrl() + "/" + id);
			if (!httpResponse.IsSuccessStatusCode)
			{
				throw new Exception("cannot delete category");
			}
		}

		public async Task<IEnumerable<CategoryDTO>> GetAll()
		{
			_logger.LogInformation(GetBaseUrl());
			var httpResponse = await _client.GetAsync(GetBaseUrl());

			if (!httpResponse.IsSuccessStatusCode)
			{
				throw new Exception("cannot retrieve category");
			}
			var content = await httpResponse.Content.ReadAsStringAsync();
			var categories = JsonSerializer.Deserialize<IEnumerable<CategoryDTO>>(content, new JsonSerializerOptions
			{
				PropertyNameCaseInsensitive = true
			});

			return categories;
		}

		public async Task<CategoryDTO> GetById(int id)
		{
            _logger.LogInformation(GetBaseUrl());
            var httpResponse = await _client.GetAsync(GetBaseUrl() + "/" + id);

            if (!httpResponse.IsSuccessStatusCode)
            {	
				throw new Exception("cannot retrieve category");
            }

			var content = await httpResponse.Content.ReadAsStringAsync();
			var category = JsonSerializer.Deserialize<CategoryDTO>(content, new JsonSerializerOptions
			{
				PropertyNameCaseInsensitive = true
			});
			return category;
        }

		public async Task Update(int id, CategoryUpdateDTO category)
		{
			var json = JsonSerializer.Serialize(category);
			var data = new StringContent(json, Encoding.UTF8, "application/json");
			var httpResponse = await _client.PutAsync(GetBaseUrl() + "/", data);
			if (!httpResponse.IsSuccessStatusCode)
			{
				throw new Exception("cannot update category");
			}
		}

		public Task<int> GetCount()
		{
			var httpResponse = _client.GetAsync(GetBaseUrl() + "/Count");
			if (!httpResponse.Result.IsSuccessStatusCode)
			{
				throw new Exception("cannot retrieve category");
			}
			var content = httpResponse.Result.Content.ReadAsStringAsync();
			var count = JsonSerializer.Deserialize<int>(content.Result, new JsonSerializerOptions
			{
				PropertyNameCaseInsensitive = true
			});
			return Task.FromResult(count);
		}

		public async Task<IEnumerable<CategoryDTO>> GetByPage(int page, int pageSize)
		{
			var httpResponse = await _client.GetAsync(GetBaseUrl() + "/Paging/" + page + "/" + pageSize);
			if (!httpResponse.IsSuccessStatusCode)
			{
				throw new Exception("cannot retrieve category");
			}

			var content = await httpResponse.Content.ReadAsStringAsync();
			var categories = JsonSerializer.Deserialize<IEnumerable<CategoryDTO>>(content, new JsonSerializerOptions
			{
				PropertyNameCaseInsensitive = true
			});

			return categories;
		}
	}
}
