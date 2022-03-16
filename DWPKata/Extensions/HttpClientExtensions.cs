using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Net.Http.Headers;

namespace DWPKata.Extensions
{
    public static class HttpClientExtensions
    {
        public static IServiceCollection AddHttpClients(this IServiceCollection services, IConfiguration config)
        {
            var httpClients = config.GetSection("AppSettings:HttpClients");

            foreach (var httpClient in httpClients.GetChildren())
            {
                var key = httpClient.GetValue<string>("Key");
                var baseAddress = httpClient.GetValue<string>("BaseAddress");

                services.AddHttpClient(key, client =>
                {
                    client.BaseAddress = new System.Uri(baseAddress);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
                });
            }

            return services;
        }
    }
}