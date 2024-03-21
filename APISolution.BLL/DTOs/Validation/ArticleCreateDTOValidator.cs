using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace APISolution.BLL.DTOs.Validation
{
	public class ArticleCreateDTOValidator : AbstractValidator<ArticleCreateDTO>
	{
		public ArticleCreateDTOValidator()
		{
			RuleFor(x => x.Title).NotEmpty().WithMessage("Title is required");
			RuleFor(x => x.Details).NotEmpty().WithMessage("Details is required");
		}
	}
}
