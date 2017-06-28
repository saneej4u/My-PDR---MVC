using NewPDR.Data.Infrastructure;
using NewPDR.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewPDR.Data.Repository
{
    public class ObjectiveRepository : RepositoryBase<Objective>, IObjectiveRepository
    {
        public ObjectiveRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
    public interface IObjectiveRepository : IRepository<Objective>
    {

    }
}
