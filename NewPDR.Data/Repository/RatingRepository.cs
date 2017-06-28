using NewPDR.Data.Infrastructure;
using NewPDR.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewPDR.Data.Repository
{
    class RatingRepository : RepositoryBase<Rating>, IRatingRepository
    {
        public RatingRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
    public interface IRatingRepository : IRepository<Rating>
    {

    }
}
