using NewPDR.Data.Infrastructure;
using NewPDR.Data.Repository;
using NewPDR.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewPDR.Service.Services
{

    public interface IToDoListService
    {

        IEnumerable<ToDoList> GetAllTaskUserId(string userId);
        ToDoList GetTaskById(int taskId);

        void InsertToDoList(ApplicationUser user, string task);
        void UpdateTask(ToDoList ToDoList);
        void Save();
        void Delete(ToDoList task);

    }

    public class ToDoListService : IToDoListService
    {

        private readonly IUnitOfWork unitOfWork;
        private readonly IToDoListRepository _ToDoListRepository;

        public ToDoListService(IUnitOfWork unitOfWork, IToDoListRepository todoRep)
        {
            this.unitOfWork = unitOfWork;
            this._ToDoListRepository = todoRep;
        }

        public IEnumerable<ToDoList> GetAllTaskUserId(string userId)
        {
            return _ToDoListRepository.GetAll().Where(x => x.ApplicationUserId == userId);
        }

        public ToDoList GetTaskById(int taskId)
        {
            return _ToDoListRepository.GetById(taskId);
        }

        public void UpdateTask(ToDoList toDoList)
        {
            _ToDoListRepository.Update(toDoList);
            Save();
        }

        public void InsertToDoList(ApplicationUser user, string task)
        {
            //Create PDR
            ToDoList pdr = new ToDoList();
            pdr.ApplicationUser = user;
            pdr.ApplicationUserId = user.Id;
            pdr.Task = task;
            pdr.IsCompleted = false;
            _ToDoListRepository.Add(pdr);
            Save();
        }

        public void Delete(ToDoList task)
        {
            _ToDoListRepository.Delete(task);
            Save();


        }

        public void Save()
        {
            unitOfWork.Commit();
        }
    }
}
