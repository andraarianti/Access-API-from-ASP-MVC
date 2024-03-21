using APISolution.BLL.DTOs;
using APISolution.Domain;

namespace SampleMVC.Services
{
    public interface ICategoryServices
    {
        Task<IEnumerable<CategoryDTO>> GetAll();
        Task<CategoryDTO> GetById(int id);
        Task<int> GetCount();
        Task<IEnumerable<CategoryDTO>> GetByPage(int page, int pageSize);
        Task<CategoryDTO> Create(CategoryCreateDTO category);
        Task Update(int id, CategoryUpdateDTO category);
        Task Delete(int id);
    }
}
