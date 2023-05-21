using System.Net;
using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System.Net.Http;

namespace Pdfless.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPdfless(this IServiceCollection services, IConfiguration configuration, IWebProxy proxy = null)
        {
            var pdflessOptions = new PdflessOptions();
            configuration.GetSection(PdflessOptions.PdflessSettingsKey).Bind(pdflessOptions);
            services.Configure<PdflessOptions>(options => configuration.GetSection(PdflessOptions.PdflessSettingsKey).Bind(options) );

            services.AddTransient<PdflessHttpMessageHandler>();
            services.AddPdflessHttpClient<IPdflessClient, PdflessClient>(pdflessOptions, proxy);
            return services;
        }

        private static IServiceCollection AddPdflessHttpClient<I, T>(this IServiceCollection services,
          PdflessOptions options, IWebProxy proxy = null) where I : class
                                              where T : class, I
        {
            services.AddHttpClient<I, T>(client =>
             {
                 client.BaseAddress = new Uri(options.Host);
             })
             .AddHttpMessageHandler<PdflessHttpMessageHandler>()
             .ConfigurePrimaryHttpMessageHandler(() => proxy != null ? 
                CreateHttpClientHandlerWithWebProxy(proxy) : 
                CreateHttpClientHandlerWithDefaultProxyCredentials()
             );

            return services;
        }

        private static HttpClientHandler CreateHttpClientHandlerWithWebProxy(IWebProxy proxy) 
        {
            return new HttpClientHandler()
            {
                Proxy = proxy,
                PreAuthenticate = true,
                UseDefaultCredentials = false,
            };
        }

        private static HttpClientHandler CreateHttpClientHandlerWithDefaultProxyCredentials() 
        {
            return new HttpClientHandler()
            {
                DefaultProxyCredentials = CredentialCache.DefaultCredentials
            };
        }
    }
}