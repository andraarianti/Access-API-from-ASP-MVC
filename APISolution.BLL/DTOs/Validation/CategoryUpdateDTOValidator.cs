using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace APISolution.BLL.DTOs.Validation
{
	public class CategoryUpdateDTOValidator : AbstractValidator<CategoryUpdateDTO>
	{
		public CategoryUpdateDTOValidator()
		{
			RuleFor(x => x.CategoryName).NotEmpty().WithMessage("Category Name harus diisi");
		}
	}
}
