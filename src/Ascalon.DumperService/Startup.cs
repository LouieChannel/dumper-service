using System.Text.Json;
using System.Globalization;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ascalon.DumperService.Features.Dumpers.PostDumper;
using MediatR;
using System.Reflection;
using DostaLab.Cqrs.Logging;
using DostaLab.Cqrs.Validating;
using Ascalon.DumperService.SreamService;

namespace Ascalon.DumperService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
                options.JsonSerializerOptions.IgnoreNullValues = true;
            });

            services.AddLogging();
            services.AddCommonLogging();

            services.AddHttpClient();

            services.AddCommonValidating();

            services.AddMediatR(Assembly.GetAssembly(typeof(PostDumperHandler)));

            services.AddSingleton<IStreamService, StreamService>();

            services.ConfigurePostDumper();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("ru-RU");
            CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("ru-RU");

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
