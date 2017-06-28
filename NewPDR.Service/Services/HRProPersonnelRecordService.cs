using NewPDR.Data.Repository;
using NewPDR.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewPDR.Service
{
    public interface IHRProPersonnelRecordService
    {

         HRProPersonnelRecord GetSingle(string emailId);

         IEnumerable<HRProPersonnelRecord> GetAll();
    }

    public class HRProPersonnelRecordService :IHRProPersonnelRecordService
    {
        private readonly IHRProPersonnelRecordRepository _HRProRepository;

        public HRProPersonnelRecordService(IHRProPersonnelRecordRepository hrProRepository)
        {
            this._HRProRepository = hrProRepository;
        }

        public HRProPersonnelRecord GetSingle(string emailId)
        {
           return GetAll().Where(x => x.EmailId == emailId).FirstOrDefault();
        }

        public IEnumerable<HRProPersonnelRecord> GetAll()
        {
           return _HRProRepository.GetAll();
        }
    }
}
