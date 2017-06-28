using NewPDR.Data.Infrastructure;
using NewPDR.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewPDR.Data.Repository
{
    class SuccessFactorTypeRepository : RepositoryBase<SuccessFactorType>, ISuccessFactorTypeRepository
    {
        public SuccessFactorTypeRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
    public interface ISuccessFactorTypeRepository : IRepository<SuccessFactorType>
    {

    }
}