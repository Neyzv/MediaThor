using Microsoft.Extensions.DependencyInjection;

namespace MediaThor.Extensions
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add static services of the MediaThor lib.
        /// </summary>
        /// <param name="services">The app service collection.</param>
        /// <returns></returns>
        public static IServiceCollection AddMediathorServices(this IServiceCollection services) =>
            services
                .AddSingleton<IPipelineBuilder, PipelineBuilder>()
                .AddTransient<IMediator, MediaThor>();
    }
}