
using Bookstore.Application.DTO;
using FluentValidation;

namespace Bookstore.Application.Validators
{
    public class CreateBookValidator : AbstractValidator<CreateBookDto>
    {
        public CreateBookValidator()
        {
            RuleFor(x => x.Title) .NotEmpty().WithMessage("标题不能为空") .MaximumLength(200);

            RuleFor(x => x.Author) .NotEmpty().WithMessage("作者不能为空") .MaximumLength(100);

            RuleFor(x => x.Price) .GreaterThan(0).WithMessage("价格必须大于0");

            RuleFor(x => x.Category).NotEmpty().WithMessage("类别不能为空") .MaximumLength(100);
        }
    }
}
