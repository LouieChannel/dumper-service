using Ascalon.MefiatR.Validator.Fluent;
using Microsoft.Extensions.DependencyInjection;

namespace Ascalon.DumperService.Features.Dumpers.PostDumper
{
    public static class Extensions
    {
        public static void ConfigurePostDumper(this IServiceCollection services)
        {
            services.AddFluentValidatingPreProcessor<PostDumperCommand, PostDumperValidator>();
        }
    }
}
