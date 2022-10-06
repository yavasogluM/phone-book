using PhoneBook.ReportCommon.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhoneBook.ReportCommon.Services
{
    public interface IReportService
    {
        List<ReportModel> GetAll();
    }
    public class ReportService : IReportService
    {
        public List<ReportModel> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
