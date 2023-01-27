using Microsoft.AspNetCore.Builder;
using SignalR_API.SubscribeTableDependencies;

namespace SignalR_API.MiddlewareExtensions
{
    public static class ApplicationBuilderExtension
    {
        public static void UseSqlTableDependency<T>(this IApplicationBuilder applicationBuilder, string connectionString)
        {
            var serviceProvider = applicationBuilder.ApplicationServices;
            var service = serviceProvider.GetService<SubscribeProductTableDependency>();
            service.SubscribeTableDependency(connectionString);
        }
    }
}