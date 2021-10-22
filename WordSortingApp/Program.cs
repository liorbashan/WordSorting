using Microsoft.Extensions.DependencyInjection;
using System;
using WordSortingApp.Interfaces;
using WordSortingApp.Services;

namespace WordSortingApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            IServiceCollection serviceCollection = new ServiceCollection()
                .AddTransient<InputCollectingService>()
                .AddTransient<UrlReaderService>()
                .AddTransient<FileReaderService>()
                .AddTransient<App>();

            ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
            serviceProvider.GetService<App>().Run();
        }
    }
}
