using NewPDR.Data.Infrastructure;
using NewPDR.Data.Repository;
using NewPDR.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewPDR.Service
{

    public interface IUserService
    {
        ApplicationUser GetUser(string userId);
        IEnumerable<ApplicationUser> GetUsers();
        IEnumerable<ApplicationUser> GetUsers(string username);
        ApplicationUser GetUserProfile(string userid);
        ApplicationUser GetUsersByEmail(string email);
        IEnumerable<ApplicationUser> GetAllUsers(List<string> emailIds);
        IEnumerable<ApplicationUser> GetUserByUserId(IEnumerable<string> userid);
        IEnumerable<ApplicationUser> SearchUser(string searchString);
        IEnumerable<ApplicationUser> GetTeamMembers(ApplicationUser me);

        void UpdateUser(ApplicationUser user);
        void SaveUser();
        void EditUser(string id, string firstname, string lastname, string email);


        void SaveImageURL(string userId, string imageUrl);
    }

    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IHRProPersonnelRecordRepository _HRProPersonnelRecordRepository;


        public UserService(IUserRepository userRepository, IHRProPersonnelRecordRepository hrProService, IUnitOfWork unitOfWork)
        {
            this.userRepository = userRepository;
            this.unitOfWork = unitOfWork;
            this._HRProPersonnelRecordRepository = hrProService;
        }
        public ApplicationUser GetUserProfile(string userid)
        {
            var userprofile = userRepository.Get(u => u.Id == userid);
            return userprofile;
        }


        #region IUserService Members

        public ApplicationUser GetUser(string userId)
        {
            return userRepository.Get(u => u.Id == userId);
        }

        public IEnumerable<ApplicationUser> GetUsers()
        {
            var users = userRepository.GetAll();
            return users;
        }
        public ApplicationUser GetUsersByEmail(string email)
        {
            var users = userRepository.Get(u => u.UserName.Contains(email));
            return users;

        }
        public IEnumerable<ApplicationUser> GetUsers(string username)
        {
            var users = userRepository.GetMany(u => u.Email.Contains(username)).OrderBy(u => u.Email).ToList();

            return users;
        }

        public void SaveImageURL(string userId, string imageUrl)
        {
            var user = GetUser(userId);
            user.ProfilePicUrl = imageUrl;
            UpdateUser(user);
        }
        public void EditUser(string id, string firstname, string lastname, string email)
        {
            var user = GetUser(id);
            user.Email = email;
            UpdateUser(user);
        }
        public void UpdateUser(ApplicationUser user)
        {
            userRepository.Update(user);
            SaveUser();
        }

        public IEnumerable<ApplicationUser> SearchUser(string searchString)
        {
            var users = userRepository.GetMany(u => u.UserName.Contains(searchString) || u.Email.Contains(searchString)).OrderBy(u => u.Email);
            return users;
        }

        public void SaveUser()
        {
            unitOfWork.Commit();
        }

        public IEnumerable<ApplicationUser> GetUserByUserId(IEnumerable<string> userid)
        {
            List<ApplicationUser> users = new List<ApplicationUser> { };
            foreach (string item in userid)
            {
                var Users = userRepository.GetById(item);
                users.Add(Users);

            }
            return users;
        }

        public IEnumerable<ApplicationUser> GetAllUsers(List<string> emailIds)
        {

            List<ApplicationUser> users = new List<ApplicationUser> { };
            foreach (string userName in emailIds)
            {
                var Users = userRepository.GetAll().Where(x => x.UserName == userName).FirstOrDefault();

                if (Users != null)
                    users.Add(Users);

            }
            return users;
        }

        public IEnumerable<ApplicationUser> GetTeamMembers(ApplicationUser me)
        {
            var hrProcolleagues = _HRProPersonnelRecordRepository.GetAll().Where(x => x.LineManagerEmailId == me.UserName).ToList();
            if (hrProcolleagues != null)
            {
                return GetAllUsers(hrProcolleagues.Select(x => x.EmailId).ToList()).ToList();
            }

            return null;
        }

        #endregion
    }
}
