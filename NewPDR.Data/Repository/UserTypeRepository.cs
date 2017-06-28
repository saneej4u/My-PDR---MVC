using NewPDR.Data.Infrastructure;
using NewPDR.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewPDR.Data.Repository
{

    public class UserTypeRepository : RepositoryBase<UserType>, IUserTypeRepository
    {
        public UserTypeRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

    }

    public interface IUserTypeRepository : IRepository<UserType>
    {

    }

}
