using PhoneBook.Extensions.MongoDB;
using PhoneBook.ReportCommon.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhoneBook.ReportCommon.Services
{
    public interface IReportRepository: IBaseRepository<ReportModel>
    {

    }
    public class ReportRepository : BaseRepository<ReportModel>, IReportRepository
    {
        public ReportRepository(MongoDBConnectionSetting mongoDBConnectionSetting) : base(mongoDBConnectionSetting)
        {
        }
    }
}
