using PhoneBook.ReportCommon.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook.ReportCommon.Services
{
    public interface IReportService
    {
        Task<List<ReportModel>> GetAll();
        Task<List<ReportModel>> InsertReport(ReportModel request);
    }
    public class ReportService : IReportService
    {
        private readonly Extensions.MongoDB.MongoDBConnectionSetting _mongoDBConnectionSetting;
        private readonly IReportRepository _reportRepository;
        public ReportService(Extensions.MongoDB.MongoDBConnectionSetting mongoDBConnectionSetting,
            IReportRepository reportRepository)
        {
            _mongoDBConnectionSetting = mongoDBConnectionSetting;
            _reportRepository = reportRepository;
        }

        public async Task<List<ReportModel>> GetAll() => await _reportRepository.GetListAsync();

        public async Task<List<ReportModel>> InsertReport(ReportModel request)
        {
            await _reportRepository.InsertItemAsync(request);
            return await _reportRepository.GetListAsync();
        }
    }
}
