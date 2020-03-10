using FluentValidation;

namespace Ascalon.DumperService.Features.Dumpers.PostDumper
{
    public class PostDumperValidator : AbstractValidator<PostDumperCommand>
    {
        public PostDumperValidator()
        {
            RuleFor(c => c.Id).NotNull();
            RuleFor(c => c.Id).NotEmpty();

            RuleFor(c => c.Gfx).NotNull();
            RuleFor(c => c.Gfx).NotEmpty();

            RuleFor(c => c.Gfy).NotNull();
            RuleFor(c => c.Gfy).NotEmpty();

            RuleFor(c => c.Gfz).NotNull();
            RuleFor(c => c.Gfz).NotEmpty();

            RuleFor(c => c.Wx).NotNull();
            RuleFor(c => c.Wx).NotEmpty();

            RuleFor(c => c.Wy).NotNull();
            RuleFor(c => c.Wy).NotEmpty();

            RuleFor(c => c.Wz).NotNull();
            RuleFor(c => c.Wz).NotEmpty();

            RuleFor(c => c.Speed).NotNull();
            RuleFor(c => c.Speed).NotEmpty();
        }
    }
}
