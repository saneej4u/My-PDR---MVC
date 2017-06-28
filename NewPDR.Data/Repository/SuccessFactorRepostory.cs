using NewPDR.Data.Infrastructure;
using NewPDR.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewPDR.Data.Repository
{
    class SuccessFactorRepository: RepositoryBase<SuccessFactor>, ISuccessFactorRepository
    {
        public SuccessFactorRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
    public interface ISuccessFactorRepository : IRepository<SuccessFactor>
    {

    }
}
