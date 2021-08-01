using System.IO;
using Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application.IntegrationTests
{
    public class ServicesFixture
    {
        public readonly IConfigurationRoot Configuration;
        public readonly ServiceProvider ServiceProvider;

        public ServicesFixture()
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(path: "appSettings.Test.json", optional: false, reloadOnChange: true)
                .Build();

            ServiceProvider = new ServiceCollection()
                .AddLogging()
                .AddInfrastucture(Configuration)
                .BuildServiceProvider();
        }
    }
}