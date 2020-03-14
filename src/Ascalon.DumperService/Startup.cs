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
using DostaLab.Cqrs.Validating;
using Ascalon.DumperService.SreamService;
using Ascalon.DumperService.Kafka;
using Ascalon.DumperService.Infrastructure;
using Ascalon.DumperService.Kafka.Services;

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

            services.Configure<KafkaProducerOptions>(Configuration.GetSection("KafkaProducerOptions"));

            services.AddHttpClient();

            services.AddCommonValidating();

            services.AddMediatR(Assembly.GetAssembly(typeof(PostDumperHandler)));

            services.AddKafkaProducer();

            services.AddSingleton<IStreamService, StreamService>();

            services.ConfigurePostDumper();

            services.Configure<PostDumperConsumerServiceOptions>(
                Configuration.GetSection(nameof(PostDumperConsumerServiceOptions)));

            services.Configure<KafkaProducerOptions>(Configuration.GetSection(nameof(KafkaProducerOptions)));

            services.AddHostedService<PostDumperConsumerService>();
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
