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

    public interface IUserTypeService
    {
        UserType GetUserType(int id);
        IEnumerable<UserType> GetUserTypes();
      
    }

    class UserTypeService :IUserTypeService
    {
        private readonly IUserTypeRepository userRepository;
        private readonly IUnitOfWork unitOfWork;

        public UserTypeService(IUserTypeRepository userRepository, IUnitOfWork unitOfWork)
        {
            this.userRepository = userRepository;
            this.unitOfWork = unitOfWork;
        }

        public UserType GetUserType(int id)
        {
            return userRepository.Get(u => u.ID == id);
        }

        public IEnumerable<UserType> GetUserTypes()
        {
            return userRepository.GetAll();
        }
    }
}
