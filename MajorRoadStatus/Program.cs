using System;
using System.Net.Http;
using MajorRoadStatusSystem;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace MajorRoadStatus
{
    class Program
    {
        static int Main(string[] args) => CommandLineApplication.Execute<Program>(args);

        [Argument(0, "road name for which status is required")]
        private string RoadName { get; }

        private int OnExecute()
        {            
            ServiceProvider serviceProvider = ConfigureServices();
            return serviceProvider.GetService<ICheckMajorRoadStatus>().GetMajorRoadStatus(RoadName);
        }

        public ServiceProvider ConfigureServices()
        {

            IConfiguration configuration = new ConfigurationBuilder().              
              AddJsonFile("AppSettings.json", optional: false, reloadOnChange: true).
              Build();

            var services = new ServiceCollection();

            services.Configure<MajorRoadStatusSystemConfig>(configuration.GetSection("MajorRoadStatusSystem"));            
            services.AddSingleton<HttpClient>();
            services.AddTransient<IHttpRequestBuilder, HttpRequestBuilder>();
            services.AddTransient<IMajorRoadStatus, MajorRoadStatusSystem.MajorRoadStatus>();
            services.AddTransient<ICheckMajorRoadStatus, CheckMajorRoadStatus>();

            return services.BuildServiceProvider();
        }
    }
}
