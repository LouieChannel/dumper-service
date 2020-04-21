using FluentValidation;

namespace Ascalon.DumperService.Features.Dumpers.PostDumper
{
    public class PostDumperValidator : AbstractValidator<PostDumperCommand>
    {
        public PostDumperValidator()
        {
            RuleFor(c => c.IpAddress).NotNull().NotEmpty();

            RuleFor(c => c.Array).NotNull();
        }
    }
}
