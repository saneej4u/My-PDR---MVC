using NewPDR.Data.Infrastructure;
using NewPDR.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewPDR.Data.Repository
{
    class RatingTypeRepository : RepositoryBase<RatingType>, IRatingTypeRepository
    {
        public RatingTypeRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
    public interface IRatingTypeRepository : IRepository<RatingType>
    {

    }
}
