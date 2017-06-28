using NewPDR.Data.Infrastructure;
using NewPDR.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewPDR.Data.Repository
{
    class DevelopmentCategoryRepository : RepositoryBase<DevelopmentCategory>, IDevelopmentCategoryRepository
    {
        public DevelopmentCategoryRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
    public interface IDevelopmentCategoryRepository : IRepository<DevelopmentCategory>
    {

    }
}