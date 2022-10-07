using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using PhoneBook.Extensions.MongoDB;
using PhoneBook.ReportCommon.Services;
using PhoneBook.ReportWorker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBook.ReportWorker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    IConfiguration Configuration = hostContext.Configuration;
                    ApiUrlModel apiUrlModel = Configuration.GetSection("ApiUrl").Get<ApiUrlModel>();
                    services.AddSingleton(apiUrlModel);
                    services.AddHostedService<Worker>();
                });
    }
}
