using HackerNewsApi.Filter;
using HackerNewsApi.Interfaces;
using HackerNewsApi.Repository;
using HackerNewsApi.Service;
using Microsoft.Extensions.DependencyInjection;

namespace HackerNewsApi
{
    public static class DependancyInjection
    {
        public static IServiceCollection AddAppDI(this IServiceCollection services, IConfiguration configuration)
        {
            var externalApiUrl = configuration.GetValue<string>("ApplicationSettings:ExternalApiUrl");

            services.AddMemoryCache()
            .AddTransient<IHackerNewsService, HackerNewsService>()
            .AddTransient<IHackerNewsRepository, HackerNewsRepository>();


            services.AddHttpClient("ExternalApi",option =>
            {
                option.BaseAddress = new Uri(externalApiUrl);
            });

            return services;
        }
    }
}
