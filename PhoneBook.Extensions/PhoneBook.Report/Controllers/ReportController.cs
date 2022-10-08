using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhoneBook.ContactCommon.Models;
using PhoneBook.ReportCommon.Models;
using PhoneBook.ReportCommon.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhoneBook.Report.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;
        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet]
        public async Task<List<ReportModel>> Get() => await _reportService.GetAll();

        [HttpPost(template:"get-by-location")]
        public async Task GetByLocation([FromBody]string location)
        {
            //TODO: create new rabbitmq pipe for this request
        }
    }
}
