using NewPDR.Data.Infrastructure;
using NewPDR.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewPDR.Data.Repository
{

    public class ObjectiveTypeRepository : RepositoryBase<ObjectiveType>, IObjectiveTypeRepository
    {
        public ObjectiveTypeRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
    public interface IObjectiveTypeRepository : IRepository<ObjectiveType>
    {

    }
}
