using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PhoneBook.Extensions.MongoDB;
using PhoneBook.ReportCommon.Models;
using PhoneBook.ReportCommon.Services;
using PhoneBook.ReportWorker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PhoneBook.ReportWorker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly ApiUrlModel _apiUrlModel;
        public Worker(ILogger<Worker> logger,
            ApiUrlModel apiUrlModel)
        {
            _logger = logger;
            _apiUrlModel = apiUrlModel;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var extension = new Extensions.HttpExtension();

                var result = extension.GetResponse<List<HttpResponseReportModel>, object>(new Extensions.Models.HttpRequest<object> { RequestUrl = $"{_apiUrlModel.ReportServiceUrl}api/report", RequestType = Extensions.Models.HttpRequestType.GET });

                _logger.LogInformation($"Tamamlanan: {result.ResponseObject.Count(x => x.Durum == ReportType.Completed)} - Devam Eden: {result.ResponseObject.Count(x => x.Durum == ReportType.Preparing)} ");

                //TODO: added sending httprequest to contact service for get users with contact
                //TODO: added rabbitmq sending pipe

                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
