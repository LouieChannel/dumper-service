using FluentValidation;

namespace Ascalon.DumperService.Features.Dumpers.PostDumper
{
    public class PostDumperValidator : AbstractValidator<PostDumperCommand>
    {
        public PostDumperValidator()
        {
            RuleFor(c => c.Id).NotNull().NotEmpty();

            RuleFor(c => c.Gfx).NotNull().NotEmpty();

            RuleFor(c => c.Gfy).NotNull().NotEmpty();

            RuleFor(c => c.Gfz).NotNull().NotEmpty();

            RuleFor(c => c.Wx).NotNull().NotEmpty();

            RuleFor(c => c.Wy).NotNull().NotEmpty();

            RuleFor(c => c.Wz).NotNull().NotEmpty();

            RuleFor(c => c.Speed).NotNull().NotEmpty();
        }
    }
}
