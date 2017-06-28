using NewPDR.Data.Infrastructure;
using NewPDR.Data.Repository;
using NewPDR.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewPDR.Core.Common;
using System.Configuration;

namespace NewPDR.Service
{

    public enum CurrentPDRStatuses
    {
        NotStarted = 1,
        Started = 2,
        Completed = 3
    }

    public enum Current_YearStatus
    {
        MidYear = 1,
        FullYear = 2,
    }

    public enum WhoAmI
    {
        Manager = 1,
        LineManager = 2,
        Colleague = 3
    }

    public interface IPDRService
    {
        PDReview GetPDReviewById(int pdrId);
        IEnumerable<PDReview> GetAllPDReviews();
        IEnumerable<PDReview> GetAllPDReviewsByUserid(string userId);
        IEnumerable<PDReview> GetPDReviewByReviewPeriod(DateTime reviewPeriod);
        IEnumerable<Objective> GetAllObjectivesByPDR(int pdrId);
        Objective GetSingleObjectivesByID(int objectiveId);
        IEnumerable<SuccessFactor> GetAllSuccessFactorByPDR(int pdrId);
        IEnumerable<PersonalDevelopmentPlan> GetAllPersonalDevelopmentPlanByPDR(int pdrId);
        SuccessFactor GetSingleSuccessFactorByID(int objectiveId);
        PersonalDevelopmentPlan GetSinglePersonalDevelopmentPlanByID(int pdpId);
        IEnumerable<Rating> GetAllRatings(int userType, int ratingTypeId);
        Rating GetSingleRatings(int ratingId);

        IEnumerable<DevelopmentCategory> GetAllDevelopmentCategory(int userType);

        bool IsManagerSigned(PDReview pdr);

        bool IsColleagueSigned(PDReview pdr);

        //void CreatePDR(ApplicationUser user, UserType userType, DateTime reviewPeriod);
        void CreatePDR(ApplicationUser user, ApplicationUser reviewUser, DateTime? reviewPeriod = null);

        void UpdatePDR(PDReview pdr);
        void UpdateObjective(Objective objective);
        void UpdateSuccessFactor(SuccessFactor successFactor);
        void UpdatePersonalDevelopmentPlan(PersonalDevelopmentPlan personalDevelopmentPlan);

        void SavePDR();

        PDRStatus GetPDRStatus(int statusId);
        IEnumerable<PDRStatus> GetAllPDRStatus();

        string PDRCurrentStatusName(PDReview pdr);
        CurrentPDRStatuses CurrentPDRStatus(PDReview pdr, Current_YearStatus midOrFull);

        Current_YearStatus MidOrFull(PDReview pdr);

        void OpenMidYearPDR(int revieweeID);
        void CompleteMidYearPDR(PDReview pdr);

        void OpenFullYearPDR(int revieweeID);
        void CompleteFullYearPDR(PDReview pdr);

        //Lock and Unlock PDR
        void LockPDR(ApplicationUser user, PDReview pdr);
        void UnLockPDR(ApplicationUser user, PDReview pdr);
        void ForceUnLockPDR(ApplicationUser user, PDReview pdr);
        bool IsLocked(ApplicationUser user, PDReview pdr);
        void RequestForUnLock(ApplicationUser loggedInUser, PDReview pdr);
        void ResponseForUnLock(ApplicationUser loggedInUser, PDReview pdr, string response);
        bool IsLockedByCurrentUser(ApplicationUser loggedInUser, PDReview pdr);
        bool IsAnyAccessRequest(ApplicationUser loggedInUser, PDReview pdr);
        bool IsAnyAccessResponse(ApplicationUser loggedInUser, PDReview pdr);
        void AcceptUnLockRequest(ApplicationUser loggedInUser, ApplicationUser requestedUser, PDReview pdr);
        void DenyUnLockRequest(ApplicationUser loggedInUser, PDReview pdr);


        bool DisableWholePDRDetailsView(ApplicationUser loggedInUser, PDReview selectedPDR);
        WhoAmI WhoAamI(ApplicationUser appUser, PDReview pdr);

        // Bussiness rules and validation
        IEnumerable<ValidationResult> CanSaveOverallObjectiveRatings(PDReview pdr);
        IEnumerable<ValidationResult> CanSaveOverallSuccessFactorRatings(PDReview pdr);
        IEnumerable<ValidationResult> CanSaveOverallAnnualRatings(PDReview pdr);
        IEnumerable<ValidationResult> CanSaveObjective(Objective objective);
        IEnumerable<ValidationResult> CanSaveSuccessFactor(SuccessFactor successFactor);
        IEnumerable<ValidationResult> CanSavePersonalDevelopmentPlan(PersonalDevelopmentPlan personalDevelopmentPlan);

        IEnumerable<ValidationResult> CanCompleteMidYearPDR(PDReview pdr);
        IEnumerable<ValidationResult> CanCompleteFullYearPDR(PDReview pdr);

        IEnumerable<ValidationResult> CanCreatePDR(ApplicationUser reviewUser);

        Rating CalculateObjectiveOverAllRating(PDReview pdr);
        Rating CalculateSuccessFactorOverAllRating(PDReview pdr);
        Rating CalculateAnnualOverAllRating(PDReview pdr, int objfrom, int successfactorfrom);

    }



    public class PDRService : IPDRService
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


        private readonly IUnitOfWork unitOfWork;


        public PDRService(IUserRepository userRepository, IPDReviewRepository pdReview, IObjectiveRepository objective, IObjectiveTypeRepository objectiveType, ISuccessFactorRepository sucfact, ISuccessFactorTypeRepository sucfacType, IPDRStatusRepository pdrSt, IRatingTypeRepository ratTypeerep, IRatingRepository rateRep, IPersonalDevelopmentPlanRepository pdpRep, IDevelopmentCategoryRepository devcat, IHRProPersonnelRecordRepository hrPro, IUnitOfWork unitOfWork)
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
        }


        public PDReview GetPDReviewById(int pdrId)
        {
            return GetAllPDReviews().Where(x => x.ID == pdrId).FirstOrDefault();
        }

        public IEnumerable<PDReview> GetAllPDReviews()
        {
            return _PDRReviewRepository.GetAll();
        }

        public IEnumerable<PDReview> GetAllPDReviewsByUserid(string userId)
        {
            return GetAllPDReviews().Where(x => x.ApplicationUserId == userId);
        }

        public IEnumerable<PDReview> GetPDReviewByReviewPeriod(DateTime reviewPeriod)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Objective> GetAllObjectivesByPDR(int pdrId)
        {
            return _ObjectiveRepository.GetAll().Where(x => x.PDReviewId == pdrId);
        }

        public Objective GetSingleObjectivesByID(int objectiveId)
        {
            return _ObjectiveRepository.GetById(objectiveId);
        }

        public IEnumerable<SuccessFactor> GetAllSuccessFactorByPDR(int pdrId)
        {
            return _SuccessFactorRepository.GetAll().Where(x => x.PDReviewId == pdrId);
        }

        public IEnumerable<PersonalDevelopmentPlan> GetAllPersonalDevelopmentPlanByPDR(int pdrId)
        {
            return _IPersonalDevelopmentPlanRepository.GetAll().Where(x => x.PDReviewId == pdrId);
        }

        public SuccessFactor GetSingleSuccessFactorByID(int objectiveId)
        {
            return _SuccessFactorRepository.GetById(objectiveId);
        }

        public PersonalDevelopmentPlan GetSinglePersonalDevelopmentPlanByID(int pdpId)
        {
            return _IPersonalDevelopmentPlanRepository.GetById(pdpId);
        }

        //public void CreatePDR(ApplicationUser user, UserType userType, DateTime reviewPeriod)
        //{
        //    //Create PDR
        //    PDReview pdr = new PDReview();
        //    {
        //        pdr.UserTypeId = userType.ID;
        //        pdr.ApplicationUserId = user.Id;
        //        pdr.ReviewPeriod = reviewPeriod;
        //        pdr.MidYearStatus = 1;
        //        pdr.FullYearStatus = 1;
        //        pdr.LockedAt = DateTime.Now;
        //        pdr.LockedBy = user.Email;
        //    }

        //    _PDRReviewRepository.Add(pdr);
        //    SavePDR();


        //    if (pdr != null)
        //    {
        //        // Create Objective
        //        var objectivesTypes = _ObjectiveTyperepository.GetAll().Where(x => x.UserTypeId == userType.ID);

        //        foreach (var objectiveType in objectivesTypes)
        //        {
        //            var objective = new Objective();
        //            objective.ObjectiveTypeId = objectiveType.ID;
        //            objective.PDReviewId = pdr.ID;
        //            _ObjectiveRepository.Add(objective);
        //        }

        //        var successFactorTypes = _SuccessFactorTypeRepository.GetAll().Where(x => x.UserTypeId == userType.ID);

        //        foreach (var successFactorType in successFactorTypes)
        //        {
        //            var successFactor = new SuccessFactor();
        //            successFactor.SuccessFactorTypeId = successFactorType.ID;
        //            successFactor.PDReviewId = pdr.ID;
        //            _SuccessFactorRepository.Add(successFactor);
        //        }


        //        SavePDR();
        //    }

        //}

        public void CreatePDR(ApplicationUser user, ApplicationUser reviewUser, DateTime? reviewPeriod = null)
        {

            var rPeriod = reviewPeriod.HasValue ? reviewPeriod.Value : GenerateNextAvailableReviewPeriod(reviewUser);

            if (!IsThisPDRAllReadyExist(reviewUser, rPeriod))
            {
                //Create PDR
                PDReview pdr = new PDReview();
                {
                    pdr.UserTypeId = reviewUser.UserTypeId;
                    pdr.ApplicationUserId = user.Id;
                    pdr.ReviewPeriod = rPeriod;
                    pdr.MidYearStatus = 1;
                    pdr.FullYearStatus = 1;
                    pdr.ReviewerEmailId = reviewUser.UserName;
                    pdr.ReviewerUserId = reviewUser.Id;
                }

                _PDRReviewRepository.Add(pdr);
                SavePDR();


                if (pdr != null)
                {
                    // Create Objective
                    var objectivesTypes = _ObjectiveTyperepository.GetAll().Where(x => x.UserTypeId == reviewUser.UserTypeId);

                    foreach (var objectiveType in objectivesTypes)
                    {
                        var objective = new Objective();
                        objective.ObjectiveTypeId = objectiveType.ID;
                        objective.PDReviewId = pdr.ID;
                        _ObjectiveRepository.Add(objective);
                    }

                    var successFactorTypes = _SuccessFactorTypeRepository.GetAll().Where(x => x.UserTypeId == reviewUser.UserTypeId);

                    foreach (var successFactorType in successFactorTypes)
                    {
                        var successFactor = new SuccessFactor();
                        successFactor.SuccessFactorTypeId = successFactorType.ID;
                        successFactor.PDReviewId = pdr.ID;
                        _SuccessFactorRepository.Add(successFactor);
                    }

                    var develpmentCategories = _IDevelopmentCategoryRepository.GetAll().Where(x => x.UserTypeId == reviewUser.UserTypeId);

                    foreach (var develpmentCategory in develpmentCategories)
                    {
                        var pdPlan = new PersonalDevelopmentPlan();
                        pdPlan.PDReviewId = pdr.ID;
                        _IPersonalDevelopmentPlanRepository.Add(pdPlan);
                    }

                    SavePDR();
                }
            }


        }

        public PDRStatus GetPDRStatus(int statusId)
        {
            return _PDRStatusRepository.GetById(statusId);

        }

        public IEnumerable<PDRStatus> GetAllPDRStatus()
        {
            return _PDRStatusRepository.GetAll();
        }

        public void UpdatePDR(PDReview pdr)
        {
            _PDRReviewRepository.Update(pdr);
            SavePDR();
        }

        public void OpenMidYearPDR(int revieweeID)
        {
            var pdr = GetPDReviewById(revieweeID);
            pdr.IsMidYear = true;
            pdr.IsFullYear = false;
            pdr.MidYearStatus = 2;
            UpdatePDR(pdr);
        }

        public void CompleteMidYearPDR(PDReview pdr)
        {
            if (pdr != null)
            {
                Rating objR = CalculateObjectiveOverAllRating(pdr);
                Rating sfR = CalculateSuccessFactorOverAllRating(pdr);
              

                pdr.ObjectiveOverallRatingId = objR.ID;
                pdr.SuccessFactorOverallRatingId = sfR.ID;
               

                pdr.MidyearObjectiveOverallScore = objR.ScoreFrom;
                pdr.MidYearSuccessFactorOverallScore = sfR.ScoreFrom;
               

                Rating ovR = CalculateAnnualOverAllRating(pdr,objR.ScoreFrom, sfR.ScoreFrom); 
                pdr.OverallRatingId = ovR.ID; 
                pdr.MidYearOverallScore = ovR.ScoreFrom;

                pdr.IsMidYear = false;
                pdr.IsFullYear = false;

                pdr.MidYearStatus = 3;

                UpdatePDR(pdr);

          

            }
        }

        public void OpenFullYearPDR(int revieweeID)
        {
            var pdr = GetPDReviewById(revieweeID);
            pdr.IsMidYear = false;
            pdr.IsFullYear = true;
            pdr.FullYearStatus = 2;
            UpdatePDR(pdr);
        }

        public void CompleteFullYearPDR(PDReview pdr)
        {
            if (pdr != null)
            {
                Rating objR = CalculateObjectiveOverAllRating(pdr);
                Rating sfR = CalculateSuccessFactorOverAllRating(pdr);
                

                pdr.ObjectiveOverallRatingId = objR.ID;
                pdr.SuccessFactorOverallRatingId = sfR.ID;


                pdr.FullYearObjectiveOverallScore = objR.ScoreFrom;
                pdr.FullYearSuccessFactorOverallScore = sfR.ScoreFrom;

                Rating ovR = CalculateAnnualOverAllRating(pdr, objR.ScoreFrom, sfR.ScoreFrom);
                pdr.OverallRatingId = ovR.ID;
                pdr.FullYearOverallScore = ovR.ScoreFrom;

                pdr.IsMidYear = false;
                pdr.IsFullYear = false;
                pdr.FullYearStatus = 3;
                UpdatePDR(pdr);
            }
        }


        public CurrentPDRStatuses CurrentPDRStatus(PDReview pdr, Current_YearStatus midOrFull)
        {
            switch (midOrFull)
            {
                case Current_YearStatus.MidYear:
                    return (CurrentPDRStatuses)GetPDRStatus(pdr.MidYearStatus).ID;

                case Current_YearStatus.FullYear:
                    return (CurrentPDRStatuses)GetPDRStatus(pdr.FullYearStatus).ID;
                default:
                    return (CurrentPDRStatuses)GetPDRStatus(pdr.MidYearStatus).ID;

            }
        }

        public Current_YearStatus MidOrFull(PDReview pdr)
        {
            if (pdr.IsMidYear)
            {
                return Current_YearStatus.MidYear;
            }
            else if (pdr.IsFullYear)
            {
                return Current_YearStatus.FullYear;
            }

            return Current_YearStatus.MidYear;

        }

        public string PDRCurrentStatusName(PDReview pdr)
        {

            CurrentPDRStatuses midYearStatus = CurrentPDRStatus(pdr, Current_YearStatus.MidYear);
            CurrentPDRStatuses fullYearStatus = CurrentPDRStatus(pdr, Current_YearStatus.FullYear);

            if (fullYearStatus == CurrentPDRStatuses.Completed)
                return String.Format("You are viewing {0}'s full year PDR for year {0}, Full year PDR completed.", pdr.ReviewPeriod.Year, pdr.ReviewerEmailId);
            else if (fullYearStatus == CurrentPDRStatuses.Started)
                return String.Format("You are viewing {1}'s full year PDR for year {0}, Full year PDR is in Progress.", pdr.ReviewPeriod.Year, pdr.ReviewerEmailId);
            else if (midYearStatus == CurrentPDRStatuses.Completed)
                return String.Format("You are viewing {1}'s mid year PDR for year {0}, Mid year PDR completed.", pdr.ReviewPeriod.Year, pdr.ReviewerEmailId);
            else if (midYearStatus == CurrentPDRStatuses.Started)
                return String.Format("You are viewing {1}'s mid year PDR for year {0}, Mid year PDR is in Progress.", pdr.ReviewPeriod.Year, pdr.ReviewerEmailId);
            else
                return String.Format("You are viewing {1}'s full year PDR for year {0}, Mid year PDR {2} & Full year PDR {3}. .", pdr.ReviewPeriod.Year, pdr.ReviewerEmailId, CurrentPDRStatus(pdr, Current_YearStatus.MidYear), CurrentPDRStatus(pdr, Current_YearStatus.FullYear));

        }

        public bool DisableWholePDRDetailsView(ApplicationUser loggedInUser, PDReview selectedPDR)
        {
            if (!IsLockedByCurrentUser(loggedInUser, selectedPDR))
                return true;

            if (selectedPDR.FullYearStatus == 2 || selectedPDR.MidYearStatus == 2)
                return false;

            return true;
        }

        public void UpdateObjective(Objective objective)
        {
            _ObjectiveRepository.Update(objective);
            SavePDR();
        }

        public void UpdateSuccessFactor(SuccessFactor successFactor)
        {
            _SuccessFactorRepository.Update(successFactor);
            SavePDR();
        }

        public void UpdatePersonalDevelopmentPlan(PersonalDevelopmentPlan personalDevelopmentPlan)
        {
            _IPersonalDevelopmentPlanRepository.Update(personalDevelopmentPlan);
            SavePDR();
        }


        public IEnumerable<Rating> GetAllRatings(int userType, int ratingTypeId)
        {
            var ratings =  _Ratingrepository.GetAll().Where(x => x.UserTypeId == userType && x.RatingTypeId == ratingTypeId).ToList();
            if (ratings.Count() > 0)
            {
                return ratings;
            }
            else
            {
              return  _Ratingrepository.GetAll().Where(x => x.UserTypeId == 1 && x.RatingTypeId == 1).ToList();
            }
        }

        public Rating GetSingleRatings(int ratingId)
        {
            return _Ratingrepository.GetById(ratingId);
        }

        public IEnumerable<DevelopmentCategory> GetAllDevelopmentCategory(int userType)
        {
            return _IDevelopmentCategoryRepository.GetAll().Where(x => x.UserTypeId == userType).ToList();
        }

        public WhoAmI WhoAamI(ApplicationUser appUser, PDReview pdr)
        {
            if (appUser.Id == pdr.ReviewerUserId)
            {
                return WhoAmI.Colleague;
            }
            else if(appUser.UserName.ToLower().Equals(pdr.LineManagerEmailId.ToLower()))
            {
                return WhoAmI.LineManager;
            }

            return WhoAmI.Manager;
        }


        public bool IsManagerSigned(PDReview pdr)
        {

            return (!string.IsNullOrEmpty(pdr.ManagerSigned)) && pdr.ManagerSignedDate.HasValue;
        }

        public bool IsColleagueSigned(PDReview pdr)
        {

            return (!string.IsNullOrEmpty(pdr.ColleagueSigned)) && pdr.ColleagueSignedDate.HasValue;
        }

        public void LockPDR(ApplicationUser loggedInUser, PDReview pdr)
        {
            if (!IsLocked(loggedInUser, pdr))
            {
                int lockTimeOut;
                if (!int.TryParse(ConfigurationManager.AppSettings["LockTimeOut"], out lockTimeOut))
                {
                    lockTimeOut = 20;
                }

                pdr.IsLocked = true;
                pdr.LockedAt = DateTime.UtcNow;
                pdr.LockEndTime = DateTime.UtcNow.AddMinutes(lockTimeOut);
                pdr.LockedBy = loggedInUser.UserName;
                pdr.AccessRequest = string.Empty;
                pdr.AcessRequestedUserId = string.Empty;
                UpdatePDR(pdr);
            }

        }


        public void AcceptUnLockRequest(ApplicationUser loggedInUser, ApplicationUser requestedUser, PDReview pdr)
        {
            UnLockPDR(loggedInUser, pdr);
            LockPDR(requestedUser, pdr);
            pdr.AccessResponse = requestedUser.Id;
            UpdatePDR(pdr);
        }

        public void DenyUnLockRequest(ApplicationUser loggedInUser, PDReview pdr)
        {
            int lockTimeOut;
            if (!int.TryParse(ConfigurationManager.AppSettings["LockTimeOut"], out lockTimeOut))
            {
                lockTimeOut = 20;
            }

            pdr.IsLocked = true;
            pdr.LockedAt = DateTime.UtcNow;
            pdr.LockEndTime = DateTime.UtcNow.AddMinutes(lockTimeOut);
            pdr.LockedBy = loggedInUser.UserName;
            pdr.AccessResponse = pdr.AcessRequestedUserId;
            pdr.AccessRequest = string.Empty;
            pdr.AcessRequestedUserId = string.Empty;

            UpdatePDR(pdr);
        }

        public void UnLockPDR(ApplicationUser loggedInUser, PDReview pdr)
        {
            if (loggedInUser.UserName.Equals(pdr.LockedBy))
            {
                pdr.IsLocked = false;
                pdr.LockedAt = null;
                pdr.LockEndTime = null;
                pdr.LockedBy = string.Empty;
                pdr.AccessRequest = string.Empty;
                pdr.AcessRequestedUserId = string.Empty;
                UpdatePDR(pdr);
            }
        }

        public void ForceUnLockPDR(ApplicationUser loggedInUser, PDReview pdr)
        {
            if (loggedInUser.UserName.Equals(pdr.LockedBy))
            {
                pdr.IsLocked = false;
                pdr.LockedAt = null;
                pdr.LockEndTime = null;
                pdr.LockedBy = string.Empty;
                pdr.AccessRequest = string.Empty;
                pdr.AcessRequestedUserId = string.Empty;
                UpdatePDR(pdr);
            }
        }

        public bool IsLocked(ApplicationUser loggedInUser, PDReview pdr)
        {
            return pdr.IsLocked && (pdr.LockEndTime >= DateTime.UtcNow);
        }

        public bool IsLockedByCurrentUser(ApplicationUser loggedInUser, PDReview pdr)
        {
            return pdr.IsLocked && (loggedInUser.UserName == pdr.LockedBy);
        }

        public void RequestForUnLock(ApplicationUser loggedInUser, PDReview pdr)
        {
            if (pdr.IsLocked)
            {
                pdr.AccessRequest = "Please can I access. ";
                pdr.AcessRequestedUserId = loggedInUser.Id;
                UpdatePDR(pdr);

            }

        }

        public void ResponseForUnLock(ApplicationUser loggedInUser, PDReview pdr, string response)
        {
            if (pdr.IsLocked && (pdr.LockedBy.Equals(loggedInUser.UserName)))
            {
                pdr.AccessResponse = response;
                UpdatePDR(pdr);

            }

        }

        public bool IsAnyAccessRequest(ApplicationUser loggedInUser, PDReview pdr)
        {
            if (pdr.LockedBy.Equals(loggedInUser.UserName))
                return !string.IsNullOrEmpty(pdr.AccessRequest);

            return false;
        }

        public bool IsAnyAccessResponse(ApplicationUser loggedInUser, PDReview pdr)
        {
            if (pdr.LockedBy.ToLower() != loggedInUser.UserName.ToLower())
                return !string.IsNullOrEmpty(pdr.AccessResponse);

            return false;
        }

        public Rating CalculateObjectiveOverAllRating(PDReview pdr)
        {
            if (MidOrFull(pdr) == Current_YearStatus.MidYear)
            {
                //makle sure all the mid year is saved
                var getAvgMidyearRating = pdr.Objectives.Average(x => GetSingleRatings(x.MidYearRating).ScoreFrom);
                var midRating = GetAllRatings(pdr.UserTypeId, 3).FirstOrDefault(x => x.ScoreTo >= getAvgMidyearRating && x.ScoreFrom <= getAvgMidyearRating);
                if (midRating != null)
                {
                    return midRating;
                }
            }
            else if (MidOrFull(pdr) == Current_YearStatus.FullYear)
            {
                var getAvgMidyearRating = pdr.Objectives.Average(x => GetSingleRatings(x.MidYearRating).ScoreFrom);
                var getAvgFullYearRating = pdr.Objectives.Average(x => GetSingleRatings(x.FullYearRating).ScoreFrom);

                var full = (getAvgMidyearRating + getAvgFullYearRating) / 2;

                var fullRating = GetAllRatings(pdr.UserTypeId, 3).FirstOrDefault(x => x.ScoreTo >= full && x.ScoreFrom <= full);
                if (fullRating != null)
                {
                    return fullRating;
                }
            }

            return GetSingleRatings(1);

        }

        public Rating CalculateSuccessFactorOverAllRating(PDReview pdr)
        {
            if (MidOrFull(pdr) == Current_YearStatus.MidYear)
            {
                //makle sure all the mid year is saved
                var getAvgMidyearRating = pdr.SuccessFactors.Average(x => GetSingleRatings(x.MidYearRating).ScoreFrom);
                var midRating = GetAllRatings(pdr.UserTypeId, 3).FirstOrDefault(x => x.ScoreTo >= getAvgMidyearRating && x.ScoreFrom <= getAvgMidyearRating);
                if (midRating != null)
                {
                    return midRating;
                }
            }
            else if (MidOrFull(pdr) == Current_YearStatus.FullYear)
            {
                var getAvgMidyearRating = pdr.SuccessFactors.Average(x => GetSingleRatings(x.MidYearRating).ScoreFrom);
                var getAvgFullYearRating = pdr.SuccessFactors.Average(x => GetSingleRatings(x.FullYearRating).ScoreFrom);

                var full = (getAvgMidyearRating + getAvgFullYearRating) / 2;

                var fullRating = GetAllRatings(pdr.UserTypeId, 3).FirstOrDefault(x => x.ScoreTo >= full && x.ScoreFrom <= full);
                if (fullRating != null)
                {
                    return fullRating;
                }
            }

            return GetSingleRatings(1);

        }

        public Rating CalculateAnnualOverAllRating(PDReview pdr, int objfrom, int successfactorfrom)
        {
            //makle sure all the mid year is saved
            //var objOverallRating = GetSingleRatings(pdr.ObjectiveOverallRatingId.Value).ScoreFrom;
            //var sfOverallRating = GetSingleRatings(pdr.SuccessFactorOverallRatingId.Value).ScoreFrom;
            
            var getAvgAnnualRating = (objfrom + successfactorfrom) / 2;

            var annualRating = GetAllRatings(pdr.UserTypeId,3).FirstOrDefault(x => x.ScoreTo >= getAvgAnnualRating && x.ScoreFrom <= getAvgAnnualRating);
            if (annualRating != null)
            {
                return annualRating;
            }
            return GetSingleRatings(1);
        }

        public void SavePDR()
        {
            unitOfWork.Commit();
        }

        #region " Validation and Bussiness rule "

        public IEnumerable<ValidationResult> CanSaveOverallObjectiveRatings(PDReview pdr)
        {
            //Mid year is in Progress
            if (pdr.MidYearStatus == 2)
            {
                if (pdr.Objectives.Any(x => x.MidYearRating == 0))
                {
                    yield return new ValidationResult("SelectedObjectiveOverallRatingId", "Make sure mid year rating is completed before calculate overall objective.");
                }
            }

            //Full year is in Progress
            if (pdr.FullYearStatus == 2)
            {
                if (pdr.Objectives.Any(x => x.FullYearRating == 0))
                {
                    yield return new ValidationResult("SelectedObjectiveOverallRatingId", "Make sure full year rating is complete/save before saving overall objective.");
                }
            }
        }

        public IEnumerable<ValidationResult> CanSaveOverallSuccessFactorRatings(PDReview pdr)
        {
            //Mid year is in Progress
            if (pdr.MidYearStatus == 2)
            {
                if (pdr.SuccessFactors.Any(x => x.MidYearRating == 0))
                {
                    yield return new ValidationResult("SelectedSuccessFactorOverallRatingId", "Please complete ratings first.");
                }
            }

            //Full year is in Progress
            if (pdr.FullYearStatus == 2)
            {
                if (pdr.SuccessFactors.Any(x => x.FullYearRating == 0))
                {
                    yield return new ValidationResult("SelectedSuccessFactorOverallRatingId", "Please complete ratings first.");
                }
            }
        }

        public IEnumerable<ValidationResult> CanSaveOverallAnnualRatings(PDReview pdr)
        {
            //Mid year is in Progress
            if (pdr.MidYearStatus == 2)
            {
                if (pdr.Objectives.Any(x => x.MidYearRating == 0))
                {
                    yield return new ValidationResult("SelectedOverallRatingId", "Make sure mid year rating is complete/save before saving overall objective.");
                }

                if (pdr.SuccessFactors.Any(x => x.MidYearRating == 0))
                {
                    yield return new ValidationResult("SelectedOverallRatingId", "Please complete ratings first.");
                }
            }

            //Full year is in Progress
            if (pdr.FullYearStatus == 2)
            {
                if (pdr.Objectives.Any(x => x.FullYearRating == 0))
                {
                    yield return new ValidationResult("SelectedOverallRatingId", "Make sure full year rating is complete/save before saving overall objective.");
                }

                if (pdr.SuccessFactors.Any(x => x.FullYearRating == 0))
                {
                    yield return new ValidationResult("SelectedOverallRatingId", "Please complete ratings first.");
                }
            }
        }

        public IEnumerable<ValidationResult> CanCompleteMidYearPDR(PDReview pdr)
        {
            if (pdr.MidYearStatus == 2)
            {
                if (pdr.Objectives.Any(x => x.MidYearRating == 0))
                {
                    yield return new ValidationResult("PDRListErrors", "At the moment cannot complete, make sure you complete all the objective mid year ratings.");
                }

                if (pdr.SuccessFactors.Any(x => x.MidYearRating == 0))
                {
                    yield return new ValidationResult("PDRListErrors", "At the moment cannot complete, make sure you complete all the success factor mid year ratings.");
                }
            }
        }

        public IEnumerable<ValidationResult> CanCompleteFullYearPDR(PDReview pdr)
        {
            if (pdr.FullYearStatus == 2)
            {
                if (pdr.Objectives.Any(x => x.FullYearRating == 0))
                {
                    yield return new ValidationResult("PDRListErrors", "At the moment cannot complete, make sure you complete all the mid year ratings.");
                }

                if (pdr.SuccessFactors.Any(x => x.FullYearRating == 0))
                {
                    yield return new ValidationResult("PDRListErrors", "At the moment cannot complete, make sure you complete all the success factor mid year ratings.");
                }
            }
        }


        public IEnumerable<ValidationResult> CanSaveObjective(Objective objective)
        {
            yield return new ValidationResult("SelectedObjectiveOverallRatingId", "Make sure mid year rating is complete/save before saving overall objective.");
        }

        public IEnumerable<ValidationResult> CanSaveSuccessFactor(SuccessFactor successFactor)
        {
            yield return new ValidationResult("SelectedObjectiveOverallRatingId", "Make sure mid year rating is complete/save before saving overall objective.");
        }

        public IEnumerable<ValidationResult> CanSavePersonalDevelopmentPlan(PersonalDevelopmentPlan personalDevelopmentPlan)
        {
            yield return new ValidationResult("SelectedObjectiveOverallRatingId", "Make sure mid year rating is complete/save before saving overall objective.");
        }

        public IEnumerable<ValidationResult> CanCreatePDR(ApplicationUser reviewUser)
        {

            var geLatestPDR = _PDRReviewRepository.GetAll().Where(x => x.ReviewerUserId == reviewUser.Id).OrderByDescending(d => d.ReviewPeriod).FirstOrDefault();

            if (geLatestPDR != null)
            {

                if (geLatestPDR.ReviewPeriod.Year >= DateTime.Today.AddYears(2).Year)
                {
                    yield return new ValidationResult("PDRColleagueListErrors", string.Format("Failed to create PDR for {0}, not able to create PDR for selected year.", reviewUser.UserName));
                }
            }


        }

        private DateTime GenerateNextAvailableReviewPeriod(ApplicationUser reviewUser)
        {
            var geLatestPDR = _PDRReviewRepository.GetAll().Where(x => x.ReviewerUserId == reviewUser.Id).OrderByDescending(d => d.ReviewPeriod).FirstOrDefault();

            if (geLatestPDR != null)
            {
                return geLatestPDR.ReviewPeriod.AddYears(1);
            }
            return DateTime.Today;
        }

        private bool IsThisPDRAllReadyExist(ApplicationUser reviewUser, DateTime reviewPeriod)
        {

            return _PDRReviewRepository.GetAll().Where(x => x.ReviewerUserId == reviewUser.Id && x.ReviewPeriod.Year == reviewPeriod.Year).Any();

        }

        #endregion



    }
}
