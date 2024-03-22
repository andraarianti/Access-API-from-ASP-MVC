using APISolution.BLL.DTOs;
using APISolution.Domain;
using AutoMapper;

namespace APISolution.BLL.Profiles
{
	public class MapperProfile : Profile
	{
		public MapperProfile()
		{
			CreateMap<Category, CategoryDTO>().ReverseMap();
			CreateMap<CategoryCreateDTO, Category>();
			CreateMap<CategoryUpdateDTO, Category>();

			CreateMap<Article, ArticleDTO>().ReverseMap();
			CreateMap<ArticleCreateDTO, Article>();
			CreateMap<ArticleUpdateDTO, Article>();

			CreateMap<User, UserDTO>().ReverseMap();
			CreateMap<UserCreateDTO, User>();
			CreateMap<LoginDTO, User>().ReverseMap();

			CreateMap<Role, RoleDTO>().ReverseMap();
			CreateMap<RoleCreateDTO, Role>();
		}

	}
}
