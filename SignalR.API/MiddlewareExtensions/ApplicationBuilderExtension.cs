using SignalR.API.SubscribeTableDependencies;

namespace SignalR.API.MiddlewareExtensions
{
    public static class ApplicationBuilderExtension
    {
        public static void UseSqlTableDependency(this IApplicationBuilder applicationBuilder, string connectionString)
        {            
            var serviceProvider = applicationBuilder.ApplicationServices;
            var service = serviceProvider.GetService<SubscribeProductTableDependency>();
            service.SubscribeTableDependency(connectionString);
        }

        // public static void UseSqlTableDependency<T>(this IApplicationBuilder applicationBuilder, string connectionString)
        //     where T : ISubscribeTableDependency
        // {
        //     var serviceProvider = applicationBuilder.ApplicationServices;
        //     var service = serviceProvider.GetService<T>();
        //     service.SubscribeTableDependency(connectionString);
        // }
    }
}