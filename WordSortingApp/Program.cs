using Microsoft.Extensions.DependencyInjection;
using System;
using WordSortingApp.Services;

namespace WordSortingApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            IServiceCollection serviceCollection = new ServiceCollection()
                .AddTransient<TextCollectingService>()
                .AddTransient<App>();

            ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
            serviceProvider.GetService<App>().Run();
        }
    }
}
