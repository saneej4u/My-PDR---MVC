using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewPDR.Model;
using NewPDR.Data.Repository;
using NewPDR.Data.Infrastructure;

namespace NewPDR.Service.Services
{

    public interface IPDRConfigService
    {
        UserType GetSingleUserType(int id);
        ObjectiveType GetSingleObjectiveType(int id);
        SuccessFactorType GetSingleSuccessFactorType(int id);
        DevelopmentCategory GetSingleDevelopmentCategory(int id);
        RatingType GetSingleRatingType(int id);
        Rating GetSingleRating(int id);


        IEnumerable<UserType> GetAllUserType();
        IEnumerable<ObjectiveType> GetAllObjectiveType();
        IEnumerable<SuccessFactorType> GetAllSuccessFactorType();
        IEnumerable<DevelopmentCategory> GetAllDevelopmentCategory();
        IEnumerable<RatingType> GetALLRatingType();
        IEnumerable<Rating> GetALLRating();

        void UpdateObjectiveType(ObjectiveType objectiveType);
        void UpdateSuccessFactorType(SuccessFactorType successFactorType);
        void UpdateDevelopmentCategory(DevelopmentCategory developmentCategory);

        void DeleteObjectiveType(ObjectiveType objectiveType);
        void DeleteSuccessFactorType(SuccessFactorType successFactorType);
        void DeleteDevelopmentCategory(DevelopmentCategory developmentCategory);


        void SaveConfig();

    }

    public class PDRConfigService: IPDRConfigService
    {

        private readonly IUserRepository _UserRepository;
        private readonly IPDReviewRepository _PDRReviewRepository;
        private readonly IObjectiveRepository _ObjectiveRepository;
        private readonly IObjectiveTypeRepository _ObjectiveTyperepository;

        private readonly ISuccessFactorRepository _SuccessFactorRepository;
        private readonly ISuccessFactorTypeRepository _SuccessFactorTypeRepository;

        private readonly IPDRStatusRepository _PDRStatusRepository;

        private readonly IRatingRepository _Ratingrepository;
        private readonly IRatingTypeRepository _RatingTypeRepository;

        private readonly IPersonalDevelopmentPlanRepository _IPersonalDevelopmentPlanRepository;
        private readonly IDevelopmentCategoryRepository _IDevelopmentCategoryRepository;

        private readonly IHRProPersonnelRecordRepository _IHRProPersonnelRecordRepository;

        private readonly IUserTypeRepository _IUserTypeRepository;

        private readonly IUnitOfWork unitOfWork;



        public PDRConfigService(IUserRepository userRepository, IPDReviewRepository pdReview, IObjectiveRepository objective, IObjectiveTypeRepository objectiveType, ISuccessFactorRepository sucfact, ISuccessFactorTypeRepository sucfacType, IPDRStatusRepository pdrSt, IRatingTypeRepository ratTypeerep, IRatingRepository rateRep, IPersonalDevelopmentPlanRepository pdpRep, IDevelopmentCategoryRepository devcat, IHRProPersonnelRecordRepository hrPro, IUserTypeRepository userType,IUnitOfWork unitOfWork)
        {
            this._UserRepository = userRepository;
            this._PDRReviewRepository = pdReview;
            this._ObjectiveRepository = objective;
            this._ObjectiveTyperepository = objectiveType;
            this.unitOfWork = unitOfWork;
            this._PDRStatusRepository = pdrSt;
            this._Ratingrepository = rateRep;
            this._RatingTypeRepository = ratTypeerep;
            this._SuccessFactorRepository = sucfact;
            this._SuccessFactorTypeRepository = sucfacType;
            this._IPersonalDevelopmentPlanRepository = pdpRep;
            this._IDevelopmentCategoryRepository = devcat;
            this._IHRProPersonnelRecordRepository = hrPro;
            this._IUserTypeRepository = userType;
        }


        public UserType GetSingleUserType(int id)
        {
            return GetAllUserType().FirstOrDefault(x => x.ID == id);
        }

        public ObjectiveType GetSingleObjectiveType(int id)
        {
            return GetAllObjectiveType().FirstOrDefault(x => x.ID == id);
        }

        public SuccessFactorType GetSingleSuccessFactorType(int id)
        {
            return GetAllSuccessFactorType().FirstOrDefault(x => x.ID == id);
        }

        public DevelopmentCategory GetSingleDevelopmentCategory(int id)
        {
            return GetAllDevelopmentCategory().FirstOrDefault(x => x.ID == id);
        }

        public RatingType GetSingleRatingType(int id)
        {
            return GetALLRatingType().FirstOrDefault(x => x.ID == id);
        }

        public Rating GetSingleRating(int id)
        {
            return GetALLRating().FirstOrDefault(x => x.ID == id);
        }

        public IEnumerable<UserType> GetAllUserType()
        {
            return _IUserTypeRepository.GetAll();
        }

        public IEnumerable<ObjectiveType> GetAllObjectiveType()
        {
            return _ObjectiveTyperepository.GetAll();
        }

        public IEnumerable<SuccessFactorType> GetAllSuccessFactorType()
        {
            return _SuccessFactorTypeRepository.GetAll();
        }

        public IEnumerable<DevelopmentCategory> GetAllDevelopmentCategory()
        {
            return _IDevelopmentCategoryRepository.GetAll();
        }

        public IEnumerable<RatingType> GetALLRatingType()
        {
            return _RatingTypeRepository.GetAll();
        }

        public IEnumerable<Rating> GetALLRating()
        {
            return _Ratingrepository.GetAll();
        }

        public void UpdateObjectiveType(ObjectiveType objectiveType)
        {
            throw new NotImplementedException();
        }

        public void UpdateSuccessFactorType(SuccessFactorType successFactorType)
        {
            throw new NotImplementedException();
        }

        public void UpdateDevelopmentCategory(DevelopmentCategory developmentCategory)
        {
            throw new NotImplementedException();
        }

        public void DeleteObjectiveType(ObjectiveType objectiveType)
        {
            throw new NotImplementedException();
        }

        public void DeleteSuccessFactorType(SuccessFactorType successFactorType)
        {
            throw new NotImplementedException();
        }

        public void DeleteDevelopmentCategory(DevelopmentCategory developmentCategory)
        {
            throw new NotImplementedException();
        }

        public void SaveConfig()
        {
            throw new NotImplementedException();
        }
    }
}
