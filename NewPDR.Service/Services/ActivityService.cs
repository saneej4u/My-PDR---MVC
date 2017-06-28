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

    public interface IActivityService
    {

        IEnumerable<Activity> GetAllActivityUserId(string userId);
        Activity GetActivityById(int taskId);

        void InsertActivity(ApplicationUser user, string activity);
        void Save();
        void Delete(Activity activity);

    }

    public class ActivityService :IActivityService
    {
        private readonly IActivityRepository _ActivityRepository;

        private readonly IUnitOfWork unitOfWork;

        public ActivityService(IUnitOfWork unitOfWork, IActivityRepository actRep)
        {
            this.unitOfWork = unitOfWork;
            this._ActivityRepository = actRep;
        }



        public IEnumerable<Activity> GetAllActivityUserId(string userId)
        {
            return _ActivityRepository.GetAll().Where(x => x.ApplicationUserId == userId);
        }

        public Activity GetActivityById(int taskId)
        {
            return _ActivityRepository.GetById(taskId);
        }

        public void InsertActivity(ApplicationUser user, string activity)
        {
            //Create PDR

            var activities = GetAllActivityUserId(user.Id);

            if(activities.Count() >= 5)
            {
                var firstAct = activities.OrderBy(x => x.DateTimeCreated).FirstOrDefault();
                if(firstAct!=null)
                {
                    _ActivityRepository.Delete(firstAct);
                    Save();
                }
            }
            Activity act = new Activity();
            act.ApplicationUser = user;
            act.ApplicationUserId = user.Id;
            act.Name = activity;
            _ActivityRepository.Add(act);
            Save();
        }


        public void Delete(Activity activity)
        {
            _ActivityRepository.Delete(activity);
            Save();
        }

        public void Save()
        {
            unitOfWork.Commit();
        }
    }
}
