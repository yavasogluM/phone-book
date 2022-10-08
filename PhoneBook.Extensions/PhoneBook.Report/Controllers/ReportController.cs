using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        [HttpGet("test")]
        public async Task<List<ReportModel>> GetTest()
        {
            await _reportService.InsertReport(new ReportModel
            {
                Durum = ReportType.Preparing,
                RaporDetay = new ReportDetail { KisiSayisi = 2, Konum = "ankara", TelefonNumarasiSayisi = 5 },
                TalepTarihi = System.DateTime.Now,
                UUID = Guid.NewGuid().ToString()
            });
            var list = await _reportService.GetAll();
            return list;
        }
    }
}
