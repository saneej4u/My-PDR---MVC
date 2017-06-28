using NewPDR.Data.Infrastructure;
using NewPDR.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewPDR.Data.Repository
{

    class ToDoListRepository : RepositoryBase<ToDoList>, IToDoListRepository
    {
        public ToDoListRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
    public interface IToDoListRepository : IRepository<ToDoList>
    {

    }
}
