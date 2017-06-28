using NewPDR.Data.Infrastructure;
using NewPDR.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewPDR.Data.Repository
{
    class HRProPersonnelRecordRepository : RepositoryBase<HRProPersonnelRecord>, IHRProPersonnelRecordRepository
    {
        public HRProPersonnelRecordRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
        public override HRProPersonnelRecord GetById(string id)
        {
            return GetAll().FirstOrDefault(x => x.EmailId == id);
        }
    }
    public interface IHRProPersonnelRecordRepository : IRepository<HRProPersonnelRecord>
    {

    }
}
