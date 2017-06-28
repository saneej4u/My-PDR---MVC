using Microsoft.AspNet.Identity;
using NewPDR.Model;
using NewPDR.Service;
using NewPDR.Web.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NewPDR.Web.Extensions;
using NewPDR.Service.Services;
using System.IO;
using Rotativa.Core;
using Rotativa.MVC;
using NewPDR.Web.ActionFilters;
using System.Configuration;


namespace NewPDR.Web.Controllers
{
    [Authorize]
    public class PDReviewController : Controller
    {

        #region " Property "

        private IUserService _UserService;
        private IUserTypeService _UserTypeService;
        private IPDRService _PDRService;
        private UserManager<ApplicationUser> _UserManager;

        private IHRProPersonnelRecordService _HRProPersonnelRecordService;

        private IToDoListService _IToDoListService;

        private IActivityService _IActivityService;

        private ApplicationUser LoggenInUser
        {
            get
            {
                return _UserService.GetUsers().ToList().FirstOrDefault(x => x.UserName == (HttpContext.User.Identity.Name));
            }
        }

        #endregion

        public PDReviewController(IUserService userService, IUserTypeService userTypeService, IPDRService pdrS, UserManager<ApplicationUser> userManager, IHRProPersonnelRecordService ihrpro, IToDoListService todo, IActivityService actser)
        {
            this._UserService = userService;
            this._UserTypeService = userTypeService;
            this._PDRService = pdrS;
            this._UserManager = userManager;
            this._HRProPersonnelRecordService = ihrpro;
            this._IToDoListService = todo;
            this._IActivityService = actser;
        }

        // GET: PDReview
        public ActionResult Index()
        {
            var loggenInUser = LoggenInUser;

            if (loggenInUser != null)
            {
                //Unlock my PDR 
                UnLockMyPDR(loggenInUser, null);

                // Unlock my Colleagues PDR
                UnLockColleaguesPDRs(loggenInUser, null);

                var myTeam = _UserService.GetTeamMembers(LoggenInUser);
                if (myTeam != null && myTeam .Count()> 0 )
                {
                    ViewBag.IsManagerView = true;
                }
                else
                {
                    ViewBag.IsManagerView = false;
                }



            }
            return View();
        }

        public ActionResult ShowAllPDR()
        {
            var loggenInUser = LoggenInUser;
            if (loggenInUser != null)
            {

                var model = new DashboardViewModel();

                //My PDR ViewModel
                var loggedInUsersPDRViewModel = new List<PDReviewViewModel>();

                var loggedInUsersPDR = _PDRService.GetAllPDReviews().Where(x => x.ReviewerUserId == loggenInUser.Id).ToList();

                var getAllPDRStatus = _PDRService.GetAllPDRStatus();
                var getAllUserType = _UserTypeService.GetUserTypes();

                loggedInUsersPDRViewModel = loggedInUsersPDR.Select(x => new PDReviewViewModel
                {
                    ApplicationUser = x.ApplicationUser,
                    ColleagueOverallComments = x.ColleagueOverallComments,
                    DateTimeCreated = x.DateTimeCreated,
                    ID = x.ID,
                    ReviewPeriod = x.ReviewPeriod,
                    UserType = getAllUserType.FirstOrDefault(ut => ut.ID == x.UserTypeId),
                    FullYearStatus = getAllPDRStatus.FirstOrDefault(s => s.ID == x.FullYearStatus) != null ? getAllPDRStatus.FirstOrDefault(s => s.ID == x.FullYearStatus).Name : "",
                    MidYearStatus = getAllPDRStatus.FirstOrDefault(s => s.ID == x.MidYearStatus) != null ? getAllPDRStatus.FirstOrDefault(s => s.ID == x.MidYearStatus).Name : "",
                    MidYearStatusButton = getAllPDRStatus.FirstOrDefault(s => s.ID == x.FullYearStatus) != null ? getAllPDRStatus.FirstOrDefault(s => s.ID == x.FullYearStatus).Name == "Not Started" ? "Open" : "Complete" : "",
                    FullYearStatusButton = getAllPDRStatus.FirstOrDefault(s => s.ID == x.FullYearStatus) != null ? getAllPDRStatus.FirstOrDefault(s => s.ID == x.FullYearStatus).Name == "Not Started" ? "Open" : "Complete" : "",
                    IsLocked = x.IsLocked,
                    LockedAt = x.LockedAt,
                    LockEndTime = x.LockEndTime,
                    LockedBy = x.LockedBy,
                    IsDisableWholeView = _PDRService.DisableWholePDRDetailsView(loggenInUser, x)
                }).ToList();

                //Colleage View Model 
                var colleaguesViewModel = PopulateColleaguesViewModel(loggenInUser);

                // Add view model to dashboard view model
                model.PDReviewViewModels = loggedInUsersPDRViewModel;
                model.Colleagues = colleaguesViewModel;
                model.ApplicationUserId = loggenInUser.Id;

                //Unlock my PDR
                UnLockMyPDR(loggenInUser, loggedInUsersPDR);

                // Unlock my Colleagues PDR
                List<PDReview> colleaguesPDRs = model.Colleagues.SelectMany(x => x.PDReviews).ToList();
                UnLockColleaguesPDRs(loggenInUser, colleaguesPDRs);

                return View(model);
            }

            return View();
        }

        public ActionResult ShowAllPDRToDownload()
        {
            var loggenInUser = LoggenInUser;

            if (loggenInUser != null)
            {
                var model = new DashboardViewModel();

                //My PDR ViewModel
                var loggedInUsersPDRViewModel = new List<PDReviewViewModel>();

                var loggedInUsersPDR = _PDRService.GetAllPDReviews().Where(x => x.ReviewerUserId == loggenInUser.Id).ToList();

                var getAllPDRStatus = _PDRService.GetAllPDRStatus();
                var getAllUserType = _UserTypeService.GetUserTypes();

                loggedInUsersPDRViewModel = loggedInUsersPDR.Select(x => new PDReviewViewModel
                {
                    ApplicationUser = x.ApplicationUser,
                    ColleagueOverallComments = x.ColleagueOverallComments,
                    DateTimeCreated = x.DateTimeCreated,
                    ID = x.ID,
                    ReviewPeriod = x.ReviewPeriod,
                    UserType = getAllUserType.FirstOrDefault(ut => ut.ID == x.UserTypeId),
                    FullYearStatus = getAllPDRStatus.FirstOrDefault(s => s.ID == x.FullYearStatus) != null ? getAllPDRStatus.FirstOrDefault(s => s.ID == x.FullYearStatus).Name : "",
                    MidYearStatus = getAllPDRStatus.FirstOrDefault(s => s.ID == x.MidYearStatus) != null ? getAllPDRStatus.FirstOrDefault(s => s.ID == x.MidYearStatus).Name : "",
                    MidYearStatusButton = getAllPDRStatus.FirstOrDefault(s => s.ID == x.FullYearStatus) != null ? getAllPDRStatus.FirstOrDefault(s => s.ID == x.FullYearStatus).Name == "Not Started" ? "Open" : "Complete" : "",
                    FullYearStatusButton = getAllPDRStatus.FirstOrDefault(s => s.ID == x.FullYearStatus) != null ? getAllPDRStatus.FirstOrDefault(s => s.ID == x.FullYearStatus).Name == "Not Started" ? "Open" : "Complete" : "",
                    IsLocked = x.IsLocked,
                    LockedAt = x.LockedAt,
                    LockEndTime = x.LockEndTime,
                    LockedBy = x.LockedBy,
                    IsDisableWholeView = _PDRService.DisableWholePDRDetailsView(loggenInUser, x)
                }).ToList();

                //Colleage View Model 
                var colleaguesViewModel = PopulateColleaguesViewModel(loggenInUser);

                // Add view model to dashboard view model
                model.PDReviewViewModels = loggedInUsersPDRViewModel;
                model.Colleagues = colleaguesViewModel;
                model.ApplicationUserId = loggenInUser.Id;
                return View(model);

            }

            return View();
        }

        [ChildActionOnly]
        public ActionResult ShowTitle()
        {
            var loggedInUser = LoggenInUser;
            if (loggedInUser != null)
            {
                TitleViewModel model = new TitleViewModel();
                model.AllPDRs = new List<PDReview>();

                var allPDRs = _PDRService.GetAllPDReviews().ToList();

                var myLatesPDR = allPDRs.Where(x => x.ReviewerUserId == loggedInUser.Id && x.ReviewPeriod.Year == DateTime.Today.Year).FirstOrDefault();

                if (myLatesPDR != null)
                {
                    model.MyPDR = myLatesPDR;
                    model.AllPDRs.Add(myLatesPDR);
                }

                var hrProcolleagues = _HRProPersonnelRecordService.GetAll().Where(x => x.LineManagerEmailId == loggedInUser.UserName).ToList();
                var pdrDbColleages = _UserService.GetAllUsers(hrProcolleagues.Select(x => x.EmailId).ToList()).ToList();

                var colleaguesLatestPDR = new List<PDReview>();

                foreach (var pdrDbCol in pdrDbColleages)
                {
                    var colPdr = allPDRs.Where(x => x.ReviewerUserId == pdrDbCol.Id && x.ReviewPeriod.Year == DateTime.Today.Year).FirstOrDefault();

                    if (colPdr != null)
                        colleaguesLatestPDR.Add(colPdr);
                }


                if (colleaguesLatestPDR.Any())
                {
                    model.ColleaguesPDR = colleaguesLatestPDR;
                    model.AllPDRs.AddRange(colleaguesLatestPDR);
                }


                return PartialView("_ColleagueInfoTile", model);
            }
            return View();

        }

        public PartialViewResult CreateReviewerPDR(string reviewerUserId)
        {

            var user = LoggenInUser;
            var reviewUser = _UserService.GetUser(reviewerUserId);

            if (user != null && reviewUser != null)
            {

                var errors = _PDRService.CanCreatePDR(reviewUser);

                ModelState.AddModelErrors(errors);


                if (ModelState.IsValid)
                {
                    _PDRService.CreatePDR(user, reviewUser);
                    _IActivityService.InsertActivity(user, string.Format("PDR created for {0}.", reviewUser.UserName));
                    ViewBag.IsCreated = true;
                }
                else
                {
                    var errorMesssage = errors.FirstOrDefault() != null ? errors.FirstOrDefault().Message : "Failed to create PDR";
                    ViewBag.IsCreated = errorMesssage;
                }


            }

            var colleaguesViewModel = PopulateColleaguesViewModel(user);

            return PartialView("_ColleaugePDRListPartial", colleaguesViewModel);
        }

        public ActionResult ShowPDRDetails(int revieweeID, DateTime reviewPeriod, bool refresh = false)
        {
            PDReview selectedPDR = _PDRService.GetPDReviewById(revieweeID);

            if (selectedPDR == null) { return null; }

            var loggenInUser = LoggenInUser;

            UpdateLineManager(selectedPDR);

            //Lock PDR
            _PDRService.LockPDR(loggenInUser, selectedPDR);


            WhoAmI whoAmI = _PDRService.WhoAamI(loggenInUser, selectedPDR);

            CurrentPDRStatuses midYearStatus = _PDRService.CurrentPDRStatus(selectedPDR, Current_YearStatus.MidYear);
            CurrentPDRStatuses fullYearStatus = _PDRService.CurrentPDRStatus(selectedPDR, Current_YearStatus.FullYear);

            //Populate Objectives, Success factors, SignOf and PDR Info
            var objectivesModel = PopulateObjectives(selectedPDR, whoAmI, loggenInUser, midYearStatus, fullYearStatus);
            var successFactorModel = PopulateSuccessFactors(selectedPDR, whoAmI, loggenInUser, midYearStatus, fullYearStatus);
            var pdpModel = PopulatePDP(selectedPDR, whoAmI, loggenInUser);
            var pDReviewModel = PopulatePDReviewModel(selectedPDR, whoAmI, loggenInUser, midYearStatus, fullYearStatus);



            var a = selectedPDR.ObjectiveOverallRatingId.HasValue ? selectedPDR.ObjectiveOverallRatingId.Value : 0;
            var b = selectedPDR.SuccessFactorOverallRatingId.HasValue ? selectedPDR.SuccessFactorOverallRatingId.Value : 0;
            var c = selectedPDR.OverallRatingId.HasValue ? selectedPDR.OverallRatingId.Value : 0;

            var ratingsList = _PDRService.GetAllRatings(loggenInUser.UserTypeId, 3).ToList();
            var objOverallratintText = ratingsList.FirstOrDefault(z => z.ID == a);
            var successOverallratintText = ratingsList.FirstOrDefault(z => z.ID == b);
            var annualOverallratintText = ratingsList.FirstOrDefault(z => z.ID == c);

            int lockTimeOut;
            if (!int.TryParse(ConfigurationManager.AppSettings["LockTimeOut"], out lockTimeOut))
            {
                lockTimeOut = 20;
            }

            //TODO: Populate Success Factors
            //TODO: Populate PDP
            var model = new PDRDetailsViewModel
            {
                PDRId = revieweeID,
                ReviewPeriod = reviewPeriod,
                Objectives = objectivesModel,
                SuccessFactors = successFactorModel,
                PDPlans = pdpModel,
                PDReviewViewModel = pDReviewModel,
                IsDisableWholeView = _PDRService.DisableWholePDRDetailsView(loggenInUser, selectedPDR),
                IsLockedByCurrentUser = _PDRService.IsLockedByCurrentUser(loggenInUser, selectedPDR),
                WhoAmI = whoAmI,
                CurrentPDRStatus = _PDRService.PDRCurrentStatusName(selectedPDR),
                ObjectiveOverallRatings = ratingsList.ToSelectListItems((selectedPDR.ObjectiveOverallRatingId.HasValue ? selectedPDR.ObjectiveOverallRatingId.Value : 0)),
                SuccessFactorOverallRatings = ratingsList.ToSelectListItems((selectedPDR.SuccessFactorOverallRatingId.HasValue ? selectedPDR.SuccessFactorOverallRatingId.Value : 0)),
                OverallRatings = ratingsList.ToSelectListItems(selectedPDR.OverallRatingId.HasValue ? selectedPDR.OverallRatingId.Value : 0),
                AccessRequest = selectedPDR.AccessRequest,
                AccessResponse = selectedPDR.AccessResponse,
                AcessRequestedUserId = selectedPDR.AcessRequestedUserId,
                LockedBy = selectedPDR.LockedBy,
                LockedAt = selectedPDR.LockedAt.HasValue ? selectedPDR.LockedAt.Value : DateTime.Today,
                EndLocked = selectedPDR.LockEndTime.HasValue ? selectedPDR.LockEndTime.Value : DateTime.Today.AddMinutes(lockTimeOut),
                IsAnyAccessRequest = _PDRService.IsAnyAccessRequest(loggenInUser, selectedPDR),
                IsAnyAccessResponse = _PDRService.IsAnyAccessResponse(loggenInUser, selectedPDR),
                SelectedObjectiveOverallRatingId = objOverallratintText != null ? objOverallratintText.Name : "Not calculated yet.",
                SelectedSuccessFactorOverallRatingId = successOverallratintText != null ? successOverallratintText.Name : "Not calculated yet.",
                SelectedOverallRatingId = annualOverallratintText != null ? annualOverallratintText.Name : "Not calculated yet.",
                IsReadyToCloseMidYear = (!_PDRService.CanCompleteMidYearPDR(selectedPDR).Any()),
                IsReadyToCloseFullYear = (!_PDRService.CanCompleteFullYearPDR(selectedPDR).Any())

            };

            if (refresh)
            {
                if (selectedPDR.LockedBy.ToLower().Equals(loggenInUser.UserName.ToLower()))
                {
                    selectedPDR.LockedAt = DateTime.UtcNow;
                    selectedPDR.LockEndTime = DateTime.UtcNow.AddMinutes(lockTimeOut);
                    _PDRService.UpdatePDR(selectedPDR);
                }
            }

            ViewBag.Is_Locked_Me = (loggenInUser.UserName.ToLower().Equals(selectedPDR.LockedBy.ToLower()));

            ViewBag.Lock_EndTime = selectedPDR.LockEndTime.Value.ToString("dd-MM-yyyy h:mm:ss tt");

            return View(model);
        }

        private void UpdateLineManager(PDReview selectedPDR)
        {
            var reviewer = _HRProPersonnelRecordService.GetSingle(selectedPDR.ReviewerEmailId);
            selectedPDR.LineManagerEmailId = reviewer.LineManagerEmailId;
            _PDRService.UpdatePDR(selectedPDR);
        }

        [HttpPost]
        public ActionResult ShowPDRDetails(List<ObjectiveViewModel> model)
        {
            if (model != null)
            {
                var objective = _PDRService.GetSingleObjectivesByID(model[0].ID);

                if (objective != null)
                {
                    WhoAmI whoAmI = _PDRService.WhoAamI(LoggenInUser, objective.PDReview);

                    //ModelState.AddModelError("[0].Description", "Some Description errors");

                    if (ModelState.IsValid)
                    {
                        objective.Description = model[0].Description;

                        if (whoAmI == WhoAmI.Colleague)
                        {
                            objective.ColleagueComments = model[0].ColleagueComments;
                            objective.ColleagueEvidence = model[0].ColleagueEvidence;
                        }

                        if (whoAmI == WhoAmI.LineManager)
                        {
                            objective.ManagerComments = model[0].ManagerComments;
                            objective.ManagerEvidence = model[0].ManagerEvidence;

                            if (_PDRService.MidOrFull(objective.PDReview) == Current_YearStatus.MidYear)
                                objective.MidYearRating = Convert.ToInt16(model[0].SelectedMidYearRating);

                            if (_PDRService.MidOrFull(objective.PDReview) == Current_YearStatus.FullYear)
                                objective.FullYearRating = Convert.ToInt16(model[0].SelectedFullYearRating);
                        }
                        _PDRService.UpdateObjective(objective);
                    }


                    var ratingsList = _PDRService.GetAllRatings(LoggenInUser.UserTypeId, 1).ToList();
                    var midRatings = ratingsList.ToSelectListItems(Convert.ToInt16(model[0].SelectedMidYearRating));
                    var fullRatings = ratingsList.ToSelectListItems(Convert.ToInt16(model[0].SelectedFullYearRating));

                    var midratingsText = midRatings.FirstOrDefault(z => z.Value == objective.MidYearRating.ToString());
                    var fullratingsText = fullRatings.FirstOrDefault(z => z.Value == objective.FullYearRating.ToString());

                    CurrentPDRStatuses midYearStatus = _PDRService.CurrentPDRStatus(objective.PDReview, Current_YearStatus.MidYear);
                    CurrentPDRStatuses fullYearStatus = _PDRService.CurrentPDRStatus(objective.PDReview, Current_YearStatus.FullYear);

                    model[0].PDReview = objective.PDReview;
                    model[0].ObjectiveType = objective.ObjectiveType;
                    model[0].ID = objective.ID;
                    model[0].Description = objective.Description;
                    model[0].ColleagueComments = objective.ColleagueComments;
                    model[0].ManagerComments = objective.ManagerComments;
                    model[0].ColleagueEvidence = objective.ColleagueEvidence;
                    model[0].ManagerEvidence = objective.ManagerEvidence;
                    //model[0].ObjectiveIndex = ind;
                    model[0].WhoAmI = whoAmI;
                    model[0].MidYearRatings = midRatings;
                    model[0].FullYearRatings = fullRatings;
                    model[0].MidYearStatus = midYearStatus;
                    model[0].FullYearStatus = fullYearStatus;
                    model[0].SelectedMidYearRating = midratingsText != null ? midratingsText.Text : "Not calculated yet.";
                    model[0].SelectedFullYearRating = fullratingsText != null ? fullratingsText.Text : "Not calculated yet.";
                }
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_ObjectivesPartial", model);
            }


            return View(model);
        }

        [HttpPost]
        public ActionResult SaveSuccessFactors(List<SuccessFactorViewModel> model)
        {
            if (model != null)
            {
                var successFactor = _PDRService.GetSingleSuccessFactorByID(model[0].ID);

                if (successFactor != null)
                {
                    WhoAmI whoAmI = _PDRService.WhoAamI(LoggenInUser, successFactor.PDReview);

                    successFactor.Description = model[0].Description;
                    successFactor.Improvements = model[0].Improvements;
                    successFactor.Strengths = model[0].Strengths;

                    if (whoAmI == WhoAmI.Colleague)
                    {
                        successFactor.ColleagueComments = model[0].ColleagueComments;
                    }

                    if (whoAmI == WhoAmI.LineManager)
                    {
                        successFactor.ManagerComments = model[0].ManagerComments;

                        if (_PDRService.MidOrFull(successFactor.PDReview) == Current_YearStatus.MidYear)
                            successFactor.MidYearRating = Convert.ToInt16(model[0].SelectedMidYearRating);

                        if (_PDRService.MidOrFull(successFactor.PDReview) == Current_YearStatus.FullYear)
                            successFactor.FullYearRating = Convert.ToInt16(model[0].SelectedFullYearRating);

                    }

                    _PDRService.UpdateSuccessFactor(successFactor);

                    var ratingsList = _PDRService.GetAllRatings(LoggenInUser.UserTypeId, 2).ToList();
                    var midRatings = ratingsList.ToSelectListItems(Convert.ToInt16(model[0].SelectedMidYearRating));
                    var fullRatings = ratingsList.ToSelectListItems(Convert.ToInt16(model[0].SelectedFullYearRating));

                    var midratingsText = midRatings.FirstOrDefault(z => z.Value == successFactor.MidYearRating.ToString());
                    var fullratingsText = fullRatings.FirstOrDefault(z => z.Value == successFactor.FullYearRating.ToString());


                    CurrentPDRStatuses midYearStatus = _PDRService.CurrentPDRStatus(successFactor.PDReview, Current_YearStatus.MidYear);
                    CurrentPDRStatuses fullYearStatus = _PDRService.CurrentPDRStatus(successFactor.PDReview, Current_YearStatus.FullYear);

                    model[0].PDReview = successFactor.PDReview;
                    model[0].SuccessFactorType = successFactor.SuccessFactorType;
                    model[0].ID = successFactor.ID;
                    model[0].Description = successFactor.Description;
                    model[0].ColleagueComments = successFactor.ColleagueComments;
                    model[0].ManagerComments = successFactor.ManagerComments;
                    model[0].Improvements = successFactor.Improvements;
                    model[0].Strengths = successFactor.Strengths;
                    model[0].WhoAmI = whoAmI;
                    model[0].MidYearRatings = midRatings;
                    model[0].FullYearRatings = fullRatings;
                    model[0].MidYearStatus = midYearStatus;
                    model[0].FullYearStatus = fullYearStatus;
                    model[0].SelectedMidYearRating = midratingsText != null ? midratingsText.Text : "Not calculated yet.";
                    model[0].SelectedFullYearRating = fullratingsText != null ? fullratingsText.Text : "Not calculated yet.";
                }
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_SuccessFactorPartial", model);
            }


            return View(model);
        }

        [HttpPost]
        public ActionResult SavePersonalDevelopmentPlan(List<PDPViewModel> model)
        {
            if (model != null)
            {
                var pdpModel = _PDRService.GetSinglePersonalDevelopmentPlanByID(model[0].ID);

                if (pdpModel != null)
                {
                    pdpModel.DevelopmentNeed = model[0].DevelopmentNeed;
                    pdpModel.DevelopmentCatId = Convert.ToInt16(model[0].SelectedDevelopmentCategory);
                    pdpModel.TimeScale = model[0].TimeScale;
                    pdpModel.Solution = model[0].Solution;
                    pdpModel.CostAndResource = model[0].CostAndResource;

                    _PDRService.UpdatePersonalDevelopmentPlan(pdpModel);

                    var devCategories = _PDRService.GetAllDevelopmentCategory(LoggenInUser.UserTypeId).ToList();

                    model[0].PDReview = pdpModel.PDReview;
                    model[0].DevelopmentNeed = pdpModel.DevelopmentNeed;
                    model[0].ID = pdpModel.ID;
                    model[0].Solution = pdpModel.Solution;
                    model[0].CostAndResource = pdpModel.CostAndResource;
                    model[0].TimeScale = pdpModel.TimeScale;
                    model[0].SelectedDevelopmentCategory = pdpModel.DevelopmentCatId.ToString();
                    model[0].DevelopmentCategorys = devCategories.ToSelectListItems(pdpModel.DevelopmentCatId);
                    model[0].PDPIndexIndex = 0;
                }
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_PersonalDevelopmentPlanPartial", model);
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult SaveSignOff(PDReviewViewModel model)
        {

            if (model != null)
            {
                var pdr = _PDRService.GetPDReviewById(model.ID);

                if (pdr != null)
                {
                    WhoAmI whoAmI = _PDRService.WhoAamI(LoggenInUser, pdr);

                    if (whoAmI == WhoAmI.Colleague)
                    {
                        pdr.ColleagueOverallComments = model.ColleagueOverallComments;
                    }


                    if (whoAmI == WhoAmI.LineManager)
                    {
                        pdr.ManagerOverallComments = model.ManagerOverallComments;
                    }

                    model.WhoAmI = whoAmI;

                    if (ModelState.IsValid)
                    {
                        _PDRService.UpdatePDR(pdr);
                    }

                }


                if (Request.IsAjaxRequest())
                {
                    return PartialView("_SignoffAndSummaryPartial", model);
                }
            }

            return View(model);
        }

        public ActionResult OpenMidYearPDR(int revieweeID, DateTime reviewPeriod)
        {

            _PDRService.OpenMidYearPDR(revieweeID);
            return RedirectToAction("ShowPDRDetails", new { revieweeID = revieweeID, reviewPeriod = reviewPeriod.ToString("MM-dd-yyyy") });
        }

        public ActionResult OpenFullYearPDR(int revieweeID, DateTime reviewPeriod)
        {
            _PDRService.OpenFullYearPDR(revieweeID);
            return RedirectToAction("ShowPDRDetails", new { revieweeID = revieweeID, reviewPeriod = reviewPeriod.ToString("MM-dd-yyyy") });
        }

        public ActionResult CloseMidYearPDR(int revieweeID, DateTime reviewPeriod)
        {
            //Look for validation before complete
            var pdr = _PDRService.GetPDReviewById(revieweeID);
            _PDRService.CompleteMidYearPDR(pdr);
            return RedirectToAction("ShowPDRDetails", new { revieweeID = revieweeID, reviewPeriod = reviewPeriod.ToString("MM-dd-yyyy") });

        }

        public ActionResult CloseFullYearPDR(int revieweeID, DateTime reviewPeriod)
        {
            //Look for validation before complete
            var pdr = _PDRService.GetPDReviewById(revieweeID);
            _PDRService.CompleteFullYearPDR(pdr);
            return RedirectToAction("ShowPDRDetails", new { revieweeID = revieweeID, reviewPeriod = reviewPeriod.ToString("MM-dd-yyyy") });
        }

        public PartialViewResult MidYearChartPartial(string chartType)
        {
            var user = LoggenInUser;

            MidYearChartViewModel midYearChartViewModel = new MidYearChartViewModel();

            if (user != null)
            {
                PDReview pdr = _PDRService.GetAllPDReviews().Where(x => x.ReviewerUserId == user.Id && x.MidYearStatus == 3).OrderByDescending(d => d.ReviewPeriod).FirstOrDefault();
                if (pdr != null)
                {
                    var model = new ChartViewModel();

                    var midObjR = pdr.MidyearObjectiveOverallScore.HasValue ? pdr.MidyearObjectiveOverallScore.Value : 0;
                    var midSfR = pdr.MidYearSuccessFactorOverallScore.HasValue ? pdr.MidYearSuccessFactorOverallScore.Value : 0;
                    var midOvR = pdr.MidYearSuccessFactorOverallScore.HasValue ? pdr.MidYearSuccessFactorOverallScore.Value : 0;


                    midYearChartViewModel = new MidYearChartViewModel
                    {
                        ChartTitle = string.Format("{0} Mid year PDR ratings", pdr.ReviewPeriod.Year),
                        Name = "MidYearChart",
                        UserId = user.Id,
                        xValues = new List<string> { " Objectives ", " Success Factor ", " Overall " },
                        yValues = new List<int> { midObjR, midSfR, midOvR },
                        IsChartValueZero = ((midObjR == 0) && (midSfR == 0) && (midOvR == 0)) || (pdr.MidYearStatus != 3),
                        ChartType = chartType
                    };

                    return PartialView("_MidChartSharedPartial", midYearChartViewModel);
                }
            }

            return PartialView("_MidChartSharedPartial", null);

        }

        public PartialViewResult CompleteMidYearPDR(int revieweeID, DateTime reviewPeriod)
        {
            var pdr = _PDRService.GetPDReviewById(revieweeID);

            var errors = _PDRService.CanCompleteMidYearPDR(pdr);

            ModelState.AddModelErrors(errors);

            if (ModelState.IsValid)
            {
                _PDRService.CompleteMidYearPDR(pdr);
            }

            //Populate Model to show
            var user = LoggenInUser;
            var pdrModel = _PDRService.GetAllPDReviews().Where(x => x.ReviewerUserId == user.Id).ToList();
            var model = new List<PDReviewViewModel>();
            model = pdrModel.Select(x => new PDReviewViewModel
            {
                ApplicationUser = x.ApplicationUser,
                ColleagueOverallComments = x.ColleagueOverallComments,
                DateTimeCreated = x.DateTimeCreated,
                ID = x.ID,
                ReviewPeriod = x.ReviewPeriod,
                UserType = _UserTypeService.GetUserType(x.UserTypeId),
                FullYearStatus = _PDRService.GetPDRStatus(x.FullYearStatus).Name,
                MidYearStatus = _PDRService.GetPDRStatus(x.MidYearStatus).Name,
                MidYearStatusButton = _PDRService.GetPDRStatus(x.MidYearStatus).Name == "Not Started" ? "Open" : "Complete",
                FullYearStatusButton = _PDRService.GetPDRStatus(x.FullYearStatus).Name == "Not Started" ? "Open" : "Complete"
            }).ToList();

            return PartialView("_PDRListPartial", model);
        }

        public PartialViewResult CompleteFullYearPDR(int revieweeID, DateTime reviewPeriod)
        {
            var pdr = _PDRService.GetPDReviewById(revieweeID);

            _PDRService.CompleteFullYearPDR(pdr);

            //Populate Model to show

            var user = LoggenInUser;

            var pdrModel = _PDRService.GetAllPDReviews().Where(x => x.ReviewerUserId == user.Id).ToList();

            var model = new List<PDReviewViewModel>();

            model = pdrModel.Select(x => new PDReviewViewModel
            {
                ApplicationUser = x.ApplicationUser,
                ColleagueOverallComments = x.ColleagueOverallComments,
                DateTimeCreated = x.DateTimeCreated,
                ID = x.ID,
                ReviewPeriod = x.ReviewPeriod,
                UserType = _UserTypeService.GetUserType(x.UserTypeId),
                FullYearStatus = _PDRService.GetPDRStatus(x.FullYearStatus).Name,
                MidYearStatus = _PDRService.GetPDRStatus(x.MidYearStatus).Name,
                MidYearStatusButton = _PDRService.GetPDRStatus(x.MidYearStatus).Name == "Not Started" ? "Open" : "Complete",
                FullYearStatusButton = _PDRService.GetPDRStatus(x.FullYearStatus).Name == "Not Started" ? "Open" : "Complete"
            }).ToList();

            return PartialView("_PDRListPartial", model);
        }

        [HttpGet]
        public ActionResult SaveOverallObjectiveDDL(PDRDetailsViewModel model)
        {
            Rating rating = null;
            var pdr = _PDRService.GetPDReviewById(model.PDRId);
            var errors = _PDRService.CanSaveOverallObjectiveRatings(pdr);
            ModelState.AddModelErrors(errors);
            if (ModelState.IsValid)
            {
                if (pdr != null)
                {
                    rating = _PDRService.CalculateObjectiveOverAllRating(pdr);
                    pdr.ObjectiveOverallRatingId = rating.ID;
                    _PDRService.UpdatePDR(pdr);
                }
            }
            else
            {
                if (pdr != null)
                {
                    pdr.ObjectiveOverallRatingId = 0;
                    _PDRService.UpdatePDR(pdr);
                }
            }

            model.SelectedObjectiveOverallRatingId = rating != null ? rating.Name : "Not calculated yet.";

            var overallObjectiveRatings = RenderRazorViewToString(this.ControllerContext, "_OverallObjectiveRatings", model);

            return Json(new { OverallObjectiveRatings = overallObjectiveRatings, IsModelValid = ModelState.IsValid }, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public ActionResult SaveOverallSuccessFactorDDL(PDRDetailsViewModel model)
        {
            Rating rating = null;
            var pdr = _PDRService.GetPDReviewById(model.PDRId);
            var errors = _PDRService.CanSaveOverallSuccessFactorRatings(pdr);
            ModelState.AddModelErrors(errors);
            if (ModelState.IsValid)
            {
                if (pdr != null)
                {
                    rating = _PDRService.CalculateSuccessFactorOverAllRating(pdr);
                    pdr.SuccessFactorOverallRatingId = rating.ID;
                    _PDRService.UpdatePDR(pdr);
                }
            }
            else
            {
                if (pdr != null)
                {
                    pdr.SuccessFactorOverallRatingId = 0;
                    _PDRService.UpdatePDR(pdr);
                }
            }

            model.SelectedSuccessFactorOverallRatingId = rating != null ? rating.Name : "Not calculated yet.";
            model.WhoAmI = _PDRService.WhoAamI(LoggenInUser, pdr);

            var overallSuccessFactorRatings = RenderRazorViewToString(this.ControllerContext, "_OverallSuccessFactorRatings", model);

            return Json(new { OverallSuccessFactorRatings = overallSuccessFactorRatings, IsModelValid = ModelState.IsValid }, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public ActionResult SaveOverallAnnualRatingsDDL(PDRDetailsViewModel model)
        {
            Rating rating = null;
            var pdr = _PDRService.GetPDReviewById(model.PDRId);
            var errors = _PDRService.CanSaveOverallAnnualRatings(pdr);
            ModelState.AddModelErrors(errors);

            if (ModelState.IsValid)
            {
                if (pdr != null)
                {
                    rating = _PDRService.CalculateAnnualOverAllRating(pdr,1,1);
                    pdr.OverallRatingId = rating.ID;
                    _PDRService.UpdatePDR(pdr);
                }
            }
            else
            {
                if (pdr != null)
                {
                    pdr.OverallRatingId = 0;
                    _PDRService.UpdatePDR(pdr);
                }
            }

            model.SelectedOverallRatingId = rating != null ? rating.Name : "Not calculated yet.";
            model.WhoAmI = _PDRService.WhoAamI(LoggenInUser, pdr);

            var overallAnnualRatings = RenderRazorViewToString(this.ControllerContext, "_OverallAnnualRatings", model);

            return Json(new { OverallAnnualRatings = overallAnnualRatings, IsModelValid = ModelState.IsValid }, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public PartialViewResult ColleagueSignature(int pdrId, bool checkBoxValue)
        {
            var pdr = _PDRService.GetPDReviewById(pdrId);
            if (pdr != null)
            {
                if (checkBoxValue)
                {
                    pdr.ColleagueSigned = string.Empty;
                    pdr.ColleagueSignedDate = null; ;
                }
                else
                {
                    if (!string.IsNullOrEmpty(pdr.ColleagueOverallComments))
                    {
                        pdr.ColleagueSigned = pdr.ReviewerEmailId;
                        pdr.ColleagueSignedDate = DateTime.Today.Date;
                    }
                    else
                    {
                        ModelState.AddModelError("ColleagueOverallComments", "Collegue comments cannot be empty.");
                    }

                }

                _PDRService.UpdatePDR(pdr);
            }
            var loggenInUser = LoggenInUser;

            WhoAmI whoAmI = _PDRService.WhoAamI(loggenInUser, pdr);
            //Populate partial view SignOf and PDR Info

            CurrentPDRStatuses midYearStatus = _PDRService.CurrentPDRStatus(pdr, Current_YearStatus.MidYear);
            CurrentPDRStatuses fullYearStatus = _PDRService.CurrentPDRStatus(pdr, Current_YearStatus.FullYear);

            var model = PopulatePDReviewModel(pdr, whoAmI, loggenInUser, midYearStatus, fullYearStatus);

            return PartialView("_ColleagueSignPartial", model);
        }

        [HttpGet]
        public PartialViewResult ManagerSignature(int pdrId, bool checkBoxValue)
        {
            var pdr = _PDRService.GetPDReviewById(pdrId);
            if (pdr != null)
            {
                if (checkBoxValue)
                {
                    pdr.ManagerSigned = string.Empty;
                    pdr.ManagerSignedDate = null; ;
                }
                else
                {
                    if (!string.IsNullOrEmpty(pdr.ManagerOverallComments))
                    {
                        pdr.ManagerSigned = pdr.ApplicationUser.UserName;
                        pdr.ManagerSignedDate = DateTime.Today.Date;
                    }
                    else
                    {
                        ModelState.AddModelError("ManagerOverallComments", "Manager comments cannot be empty.");
                    }

                }


                _PDRService.UpdatePDR(pdr);
            }
            var loggenInUser = LoggenInUser;

            WhoAmI whoAmI = _PDRService.WhoAamI(loggenInUser, pdr);
            //Populate partial view SignOf and PDR Info
            CurrentPDRStatuses midYearStatus = _PDRService.CurrentPDRStatus(pdr, Current_YearStatus.MidYear);
            CurrentPDRStatuses fullYearStatus = _PDRService.CurrentPDRStatus(pdr, Current_YearStatus.FullYear);
            var model = PopulatePDReviewModel(pdr, whoAmI, loggenInUser, midYearStatus, fullYearStatus);

            return PartialView("_ManagerSignPartial", model);
        }

        public PartialViewResult SideNavigation()
        {
            return PartialView("_SideNavigationPartial");
        }

        public PartialViewResult LastFiveActivitiesPartialView()
        {


            var activities = _IActivityService.GetAllActivityUserId(LoggenInUser.Id).OrderByDescending(x => x.DateTimeCreated).Take(5);

            var model = new List<ActivityViewModel>();

            model = activities.Select(x => new ActivityViewModel { Name = x.Name }).ToList();

            return PartialView("_LastFiveActivitiesPartialView", model);
        }

        [HttpPost]
        public ActionResult SaveToDoList(string task)
        {
            var loggenInUser = LoggenInUser;

            List<ToDoList> getAllTaskForLoggedUser = _IToDoListService.GetAllTaskUserId(loggenInUser.Id).ToList();
            bool isMoreThanFive = false;
            bool isemptyTask = false;



            if (!string.IsNullOrEmpty(task))
            {
                if (getAllTaskForLoggedUser.Count() < 5)
                {
                    _IToDoListService.InsertToDoList(loggenInUser, task);
                    isMoreThanFive = false;
                }
                else
                {
                    isMoreThanFive = true;
                }
                isemptyTask = false;
            }
            else
            {
                isemptyTask = true;
            }

            List<ToDoList> getAllTaskForLoggedUser1 = _IToDoListService.GetAllTaskUserId(loggenInUser.Id).ToList();

            IEnumerable<ToDoListViewModel> model = new List<ToDoListViewModel>();
            model = getAllTaskForLoggedUser1.Select(x => new ToDoListViewModel
            {
                ToDoApplicationUserId = x.ApplicationUserId,
                ID = x.ID,
                DateTimeCreated = x.DateTimeCreated,
                IsCompleted = x.IsCompleted,
                Task = x.Task

            });

            var toDoPartialView = RenderRazorViewToString(this.ControllerContext, "_ToDoPartialView", model);

            return Json(new { toDoPartialView, isMoreThanFive, isemptyTask }, JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult ToDoList()
        {
            IEnumerable<ToDoListViewModel> model = PopulateToDoList();

            return PartialView("_ToDoPartialView", model);
        }

        public PartialViewResult UpdateTaskStatus(int task, bool statusValue)
        {

            ToDoList todo = _IToDoListService.GetTaskById(task);

            if (todo != null)
            {
                todo.IsCompleted = statusValue;

                _IToDoListService.UpdateTask(todo);
            }


            IEnumerable<ToDoListViewModel> model = new List<ToDoListViewModel>();
            model = _IToDoListService.GetAllTaskUserId(LoggenInUser.Id).Select(x => new ToDoListViewModel
            {
                ToDoApplicationUserId = x.ApplicationUserId,
                ID = x.ID,
                DateTimeCreated = x.DateTimeCreated,
                IsCompleted = x.IsCompleted,
                Task = x.Task

            });

            return PartialView("_ToDoPartialView", model);
        }

        [HttpPost]
        public JsonResult UnLockRequest(int pdrId)
        {
            var pdr = _PDRService.GetPDReviewById(pdrId);
            var loggenInUser = LoggenInUser;
            if (pdr != null)
            {
                _PDRService.RequestForUnLock(loggenInUser, pdr);

            }

            int lockTimeOut;
            if (!int.TryParse(ConfigurationManager.AppSettings["LockTimeOut"], out lockTimeOut))
            {
                lockTimeOut = 20;
            }


            PDRDetailsViewModel model = new PDRDetailsViewModel();
            model.LockedBy = pdr.LockedBy;
            model.LockedAt = pdr.LockedAt.HasValue ? pdr.LockedAt.Value : DateTime.Today;
            model.EndLocked = pdr.LockEndTime.HasValue ? pdr.LockEndTime.Value : DateTime.Today.AddMinutes(lockTimeOut);
            model.PDRId = pdr.ID;
            model.AccessRequest = pdr.AccessRequest;
            model.AcessRequestedUserId = pdr.AcessRequestedUserId;
            model.IsAnyAccessRequest = !_PDRService.IsAnyAccessRequest(loggenInUser, pdr);
            model.IsLockedByCurrentUser = !_PDRService.IsLockedByCurrentUser(loggenInUser, pdr);
            var unLockRequestAccessOrDeny = RenderRazorViewToString(this.ControllerContext, "_UnLockRequestAccessOrDeny", model);

            var requesUnlockPartialView = RenderRazorViewToString(this.ControllerContext, "_RequesUnlockPartialView", model);
            string sentTo = model.LockedBy;
            return Json(new { unLockRequestAccessOrDeny, requesUnlockPartialView, sentTo }, JsonRequestBehavior.AllowGet);

        }

        public ActionResult UnLockRequestAccepted(int pdrId)
        {
            var pdr = _PDRService.GetPDReviewById(pdrId);

            if (pdr != null)
            {
                var requestedUser = _UserService.GetUser(pdr.AcessRequestedUserId);
                var loggenInUser = LoggenInUser;

                if (loggenInUser != null && requestedUser != null)
                    _PDRService.AcceptUnLockRequest(loggenInUser, requestedUser, pdr);

            }

            return RedirectToAction("ShowPDRDetails", new { revieweeID = pdr.ID, reviewPeriod = pdr.ReviewPeriod.ToString("MM-dd-yyyy") });
        }

        public ActionResult UnLockRequestDenied(int pdrId)
        {
            var pdr = _PDRService.GetPDReviewById(pdrId);
            if (pdr != null)
            {
                var loggenInUser = LoggenInUser;

                if (loggenInUser != null)
                    _PDRService.DenyUnLockRequest(loggenInUser, pdr);
            }
            return RedirectToAction("ShowPDRDetails", new { revieweeID = pdr.ID, reviewPeriod = pdr.ReviewPeriod.ToString("MM-dd-yyyy") });
        }

        public ActionResult DownloadPDR(int revieweeID, DateTime reviewPeriod)
        {

            var selectedPDR = _PDRService.GetPDReviewById(revieweeID);
            var loggenInUser = LoggenInUser;

            WhoAmI whoAmI = _PDRService.WhoAamI(loggenInUser, selectedPDR);

            CurrentPDRStatuses midYearStatus = _PDRService.CurrentPDRStatus(selectedPDR, Current_YearStatus.MidYear);
            CurrentPDRStatuses fullYearStatus = _PDRService.CurrentPDRStatus(selectedPDR, Current_YearStatus.FullYear);

            //Populate Objectives, Success factors
            var objectivesModel = PopulateObjectives(selectedPDR, whoAmI, loggenInUser, midYearStatus, fullYearStatus);
            var successFactorModel = PopulateSuccessFactors(selectedPDR, whoAmI, loggenInUser, midYearStatus, fullYearStatus);
            var pdpModel = PopulateSuccessFactors(selectedPDR, whoAmI, loggenInUser, midYearStatus, fullYearStatus);

            //Populate partial view SignOf and PDR Info
            var pDReviewModel = PopulatePDReviewModel(selectedPDR, whoAmI, loggenInUser, midYearStatus, fullYearStatus);

            var ratingsList = _PDRService.GetAllRatings(loggenInUser.UserTypeId, 1).ToList();


            //TODO: Populate Success Factors
            //TODO: Populate PDP
            var model = new PDRDetailsViewModel
            {
                PDRId = revieweeID,
                ReviewPeriod = reviewPeriod,
                Objectives = objectivesModel,
                SuccessFactors = successFactorModel,
                PDPlans = null,
                PDReviewViewModel = pDReviewModel,
                IsDisableWholeView = _PDRService.DisableWholePDRDetailsView(loggenInUser, selectedPDR),
                IsLockedByCurrentUser = _PDRService.IsLockedByCurrentUser(loggenInUser, selectedPDR),
                WhoAmI = whoAmI,
                CurrentPDRStatus = _PDRService.PDRCurrentStatusName(selectedPDR),
                ObjectiveOverallRatings = ratingsList.ToSelectListItems((selectedPDR.ObjectiveOverallRatingId.HasValue ? selectedPDR.ObjectiveOverallRatingId.Value : 0)),
                SuccessFactorOverallRatings = ratingsList.ToSelectListItems((selectedPDR.SuccessFactorOverallRatingId.HasValue ? selectedPDR.SuccessFactorOverallRatingId.Value : 0)),
                OverallRatings = ratingsList.ToSelectListItems(selectedPDR.OverallRatingId.HasValue ? selectedPDR.OverallRatingId.Value : 0),
                AccessRequest = selectedPDR.AccessRequest,
                AccessResponse = selectedPDR.AccessResponse,
                AcessRequestedUserId = selectedPDR.AcessRequestedUserId,
                LockedBy = selectedPDR.LockedBy,
                LockedAt = selectedPDR.LockedAt.HasValue ? selectedPDR.LockedAt.Value : DateTime.Today,
                EndLocked = selectedPDR.LockEndTime.HasValue ? selectedPDR.LockEndTime.Value : DateTime.Today.AddMinutes(2),
                IsAnyAccessRequest = _PDRService.IsAnyAccessRequest(loggenInUser, selectedPDR),
                IsAnyAccessResponse = _PDRService.IsAnyAccessResponse(loggenInUser, selectedPDR)
            };

            var fileName = string.Format("{1}'s_PDR_ For_ Year_{0}.pdf", model.ReviewPeriod.Year, model.PDReviewViewModel.ReviewUserEmail);

            return new Rotativa.MVC.ViewAsPdf("DownloadPDR", model) { FileName = fileName };

        }

        public PartialViewResult DeleteTask(int taskId)
        {
            var task = _IToDoListService.GetTaskById(taskId);

            if (task != null)
            {
                _IToDoListService.Delete(task);
            }

            IEnumerable<ToDoListViewModel> model = PopulateToDoList();

            return PartialView("_ToDoPartialView", model);

        }

        public ActionResult CreateBulkPDR()
        {
            var model = PopulateColleaguesViewModel(LoggenInUser);

            return View(model);
        }


        public PartialViewResult CreateTeamBulkPDR(DateTime reviewPeriod)
        {
            var loggenInUser = LoggenInUser;

            var teamMembers = _UserService.GetTeamMembers(loggenInUser);

            foreach (var teamMember in teamMembers)
            {
                var errors = _PDRService.CanCreatePDR(teamMember);

                _PDRService.CreatePDR(loggenInUser, teamMember, reviewPeriod);
            }
            var model = PopulateColleaguesViewModel(loggenInUser);

            return PartialView("_ColleaugePDRListPartial", model);
        }


        //public ActionResult CreateTeamMemberPDR(string reviewerUserId)
        //{
        //    var user = LoggenInUser;
        //    var reviewUser = _UserService.GetUser(reviewerUserId);

        //    if (user != null && reviewUser != null)
        //    {

        //        var errors = _PDRService.CanCreatePDR(reviewUser);

        //        ModelState.AddModelErrors(errors);

        //        if (ModelState.IsValid)
        //        {
        //            _PDRService.CreatePDR(user, reviewUser);

        //            _IActivityService.InsertActivity(user, string.Format("PDR created for {0}.", reviewUser.UserName));
        //        }


        //    }

        //    var colleaguesViewModel = PopulateColleaguesViewModel(user);

        //    return PartialView("_ColleaugePDRListPartial", colleaguesViewModel);

        //}

        public ActionResult Monitor()
        {
            var model = new List<ColleaguesViewModel>();

            var loggenInUser = LoggenInUser;
            List<ApplicationUser> colUsers = _UserService.GetTeamMembers(loggenInUser).ToList();

            colUsers.Add(loggenInUser);

            var allUsers = colUsers.Select(x => new ColleaguesViewModel
            {
                ColleagueUser = x,
                FirstName = x.UserName,
                Surname = x.UserName,
                UserFrom = x.UserType.Name,
                UserNameOrEmail = x.UserName
            });

            model.AddRange(allUsers);


            return View(model);
        }

        #region " Helper Methods "

        private List<ColleaguesViewModel> PopulateColleaguesViewModel(ApplicationUser user)
        {
            var colleaguesViewModel = new List<ColleaguesViewModel>();
            var hrProcolleagues = _HRProPersonnelRecordService.GetAll().Where(x => x.LineManagerEmailId == user.UserName).ToList();
            var pdrDbColleages = _UserService.GetTeamMembers(LoggenInUser);

            colleaguesViewModel = pdrDbColleages.Select(x => new ColleaguesViewModel
            {
                ColleagueUser = x,
                UserNameOrEmail = x.UserName,
                FirstName = hrProcolleagues.FirstOrDefault(c => c.EmailId == x.UserName).ForeName,
                Surname = hrProcolleagues.FirstOrDefault(c => c.EmailId == x.UserName).Surname,
                UserFrom = x.UserType.Name,
                PDReviews = _PDRService.GetAllPDReviews().Where(c => c.ReviewerEmailId == x.UserName).ToList() // x.PDReviews.Select(c=>c.ReviewPeriod.Year).ToList()
            }).ToList();
            return colleaguesViewModel;
        }

        private List<OthersPDRViewModel> PopulateOthersPDRViewModel(ApplicationUser user)
        {
            var colleaguesViewModel = new List<OthersPDRViewModel>();

            var hrProcolleagues = _HRProPersonnelRecordService.GetAll().Where(x => x.LineManagerEmailId == user.UserName).ToList();

            if (hrProcolleagues != null)
            {
                var pdrDbColleages = _UserService.GetAllUsers(hrProcolleagues.Select(x => x.EmailId).ToList()).ToList();

                if (pdrDbColleages.Any())
                {
                    colleaguesViewModel = pdrDbColleages.Select(x => new OthersPDRViewModel
                                  {
                                      ColleagueUser = x,
                                      UserNameOrEmail = x.UserName,
                                      FirstName = hrProcolleagues.FirstOrDefault(c => c.EmailId == x.UserName) != null ? hrProcolleagues.FirstOrDefault(c => c.EmailId == x.UserName).ForeName : "",
                                      Surname = hrProcolleagues.FirstOrDefault(c => c.EmailId == x.UserName) != null ? hrProcolleagues.FirstOrDefault(c => c.EmailId == x.UserName).Surname : "",
                                      UserFrom = x.UserType.Name,
                                      PDReviews = _PDRService.GetAllPDReviews().Where(c => c.ReviewerEmailId == x.UserName).ToList() // x.PDReviews.Select(c=>c.ReviewPeriod.Year).ToList()
                                  }).ToList();
                }

            }

            return colleaguesViewModel;
        }

        private PDReviewViewModel PopulatePDReviewModel(PDReview selectedPDR, WhoAmI whoAmI, ApplicationUser loggenInUser, CurrentPDRStatuses midYearStatus, CurrentPDRStatuses fullYearStatus)
        {
            return new PDReviewViewModel
            {
                ID = selectedPDR.ID,
                ReviewPeriod = selectedPDR.ReviewPeriod,
                DateTimeCreated = selectedPDR.DateTimeCreated,
                ApplicationUser = selectedPDR.ApplicationUser,
                UserType = selectedPDR.UserType,
                FullYearStatusE = fullYearStatus,
                MidYearStatusE = midYearStatus,
                ColleagueOverallComments = selectedPDR.ColleagueOverallComments,
                ColleagueSigned = selectedPDR.ColleagueSigned,
                ColleagueSignedDate = selectedPDR.ColleagueSignedDate,
                ManagerOverallComments = selectedPDR.ManagerOverallComments,
                ManagerSigned = selectedPDR.ManagerSigned,
                ManagerSignedDate = selectedPDR.ManagerSignedDate,
                WhoAmI = whoAmI,
                ReviewUserEmail = selectedPDR.ReviewerEmailId,
                LineMangerEmailId = selectedPDR.LineManagerEmailId,
                ManagerEmailId = "Manager email",
                IsManagerSigned = _PDRService.IsManagerSigned(selectedPDR),
                IsColleagueSigned = _PDRService.IsColleagueSigned(selectedPDR),
                IsLocked = selectedPDR.IsLocked,
                LockedAt = selectedPDR.LockedAt,
                LockEndTime = selectedPDR.LockEndTime,
                LockedBy = selectedPDR.LockedBy,
                IsDisableWholeView = _PDRService.DisableWholePDRDetailsView(loggenInUser, selectedPDR)

            };
        }

        private List<ObjectiveViewModel> PopulateObjectives(PDReview selectedPDR, WhoAmI whoAmI, ApplicationUser loggenInUser, CurrentPDRStatuses midYearStatus, CurrentPDRStatuses fullYearStatus)
        {
            var objectives = _PDRService.GetAllObjectivesByPDR(selectedPDR.ID).ToList();

            var objectivesModel = new List<ObjectiveViewModel>();
            int ind = 0;


            var ratingsList = _PDRService.GetAllRatings(loggenInUser.UserTypeId, 1).ToList();


            foreach (var data in objectives)
            {
                var midratings = ratingsList.ToSelectListItems(data.MidYearRating);
                var fullratings = ratingsList.ToSelectListItems(data.FullYearRating);

                var midratingsText = midratings.FirstOrDefault(z => z.Value == data.MidYearRating.ToString());
                var fullratingsText = fullratings.FirstOrDefault(z => z.Value == data.FullYearRating.ToString());

                var result = new ObjectiveViewModel
                {

                    PDReview = selectedPDR,
                    ObjectiveType = data.ObjectiveType,
                    ID = data.ID,
                    Description = data.Description,
                    ColleagueComments = data.ColleagueComments,
                    ManagerComments = data.ManagerComments,
                    ColleagueEvidence = data.ColleagueEvidence,
                    ManagerEvidence = data.ManagerEvidence,
                    SuccessMessage = "",
                    ObjectiveIndex = ind,
                    MidYearRatings = midratings,
                    FullYearRatings = fullratings,
                    WhoAmI = whoAmI,
                    MidYearStatus = midYearStatus,
                    FullYearStatus = fullYearStatus,
                    SelectedMidYearRating = midratingsText != null ? midratingsText.Text : "Not calculated yet.",
                    SelectedFullYearRating = fullratingsText != null ? fullratingsText.Text : "Not calculated yet.",
                    IsDisableWholeView = _PDRService.DisableWholePDRDetailsView(loggenInUser, selectedPDR)
                };
                ind = ind + 1;
                objectivesModel.Add(result);
            }

            return objectivesModel;
        }

        private List<SuccessFactorViewModel> PopulateSuccessFactors(PDReview selectedPDR, WhoAmI whoAmI, ApplicationUser loggenInUser, CurrentPDRStatuses midYearStatus, CurrentPDRStatuses fullYearStatus)
        {
            var successFactors = _PDRService.GetAllSuccessFactorByPDR(selectedPDR.ID).ToList();

            var successFactorViewModel = new List<SuccessFactorViewModel>();
            int ind = 0;

            var ratingsList = _PDRService.GetAllRatings(loggenInUser.UserTypeId, 2).ToList();

            foreach (var data in successFactors)
            {
                var midratings = ratingsList.ToSelectListItems(data.MidYearRating);
                var fullratings = ratingsList.ToSelectListItems(data.FullYearRating);

                var midratingsText = midratings.FirstOrDefault(z => z.Value == data.MidYearRating.ToString());
                var fullratingsText = fullratings.FirstOrDefault(z => z.Value == data.FullYearRating.ToString());

                var result = new SuccessFactorViewModel
                {

                    PDReview = selectedPDR,
                    SuccessFactorType = data.SuccessFactorType,
                    ID = data.ID,
                    Description = data.Description,
                    ColleagueComments = data.ColleagueComments,
                    ManagerComments = data.ManagerComments,
                    Improvements = data.Improvements,
                    Strengths = data.Strengths,
                    SuccessFactorIndex = ind,
                    MidYearRatings = midratings,
                    FullYearRatings = fullratings,
                    WhoAmI = whoAmI,
                    MidYearStatus = midYearStatus,
                    FullYearStatus = fullYearStatus,
                    SelectedMidYearRating = midratingsText != null ? midratingsText.Text : "Not calculated yet.",
                    SelectedFullYearRating = fullratingsText != null ? fullratingsText.Text : "Not calculated yet.",
                    IsDisableWholeView = _PDRService.DisableWholePDRDetailsView(loggenInUser, selectedPDR)
                };
                ind = ind + 1;
                successFactorViewModel.Add(result);
            }

            return successFactorViewModel;
        }

        private List<PDPViewModel> PopulatePDP(PDReview selectedPDR, WhoAmI whoAmI, ApplicationUser loggenInUser)
        {
            var pdPlans = _PDRService.GetAllPersonalDevelopmentPlanByPDR(selectedPDR.ID).ToList();

            var devCategories = _PDRService.GetAllDevelopmentCategory(loggenInUser.UserTypeId).ToList();

            var pDPViewModel = new List<PDPViewModel>();
            int ind = 0;

            foreach (var data in pdPlans)
            {

                var catId = data.DevelopmentCatId;
                var categories = devCategories.ToSelectListItems(catId);

                var result = new PDPViewModel
                {
                    PDReview = selectedPDR,
                    DevelopmentNeed = data.DevelopmentNeed,
                    ID = data.ID,
                    Solution = data.Solution,
                    CostAndResource = data.CostAndResource,
                    TimeScale = data.TimeScale,
                    SelectedDevelopmentCategory = data.DevelopmentCatId.ToString(),
                    DevelopmentCategorys = categories,
                    WhoAmI = whoAmI,
                    PDPIndexIndex = 0,
                    IsDisableWholeView = _PDRService.DisableWholePDRDetailsView(loggenInUser, selectedPDR)
                };
                ind = ind + 1;
                pDPViewModel.Add(result);
            }

            return pDPViewModel;
        }

        private IEnumerable<ToDoListViewModel> PopulateToDoList()
        {
            IEnumerable<ToDoListViewModel> model = new List<ToDoListViewModel>();
            model = _IToDoListService.GetAllTaskUserId(LoggenInUser.Id).Select(x => new ToDoListViewModel
            {
                ToDoApplicationUserId = x.ApplicationUserId,
                ID = x.ID,
                DateTimeCreated = x.DateTimeCreated,
                IsCompleted = x.IsCompleted,
                Task = x.Task

            });
            return model;
        }

        private void UnLockColleaguesPDRs(ApplicationUser loggenInUser, List<PDReview> colleaguesPDRs)
        {
            if (colleaguesPDRs == null)
            {
                var colleauges = _UserService.GetTeamMembers(loggenInUser).Select(x => x.UserName);

                colleaguesPDRs = _PDRService.GetAllPDReviews().Where(x => colleauges.Contains(x.ReviewerEmailId)).ToList();
            }

            foreach (PDReview pdr in colleaguesPDRs)
            {
                _PDRService.UnLockPDR(loggenInUser, pdr);
            }
        }

        private void UnLockMyPDR(ApplicationUser loggenInUser, List<PDReview> loggedInUsersPDR)
        {
            if (loggedInUsersPDR == null)
                loggedInUsersPDR = _PDRService.GetAllPDReviews().Where(x => x.ReviewerUserId == loggenInUser.Id).ToList();

            foreach (var pdr in loggedInUsersPDR)
            {
                _PDRService.UnLockPDR(loggenInUser, pdr);
            }
        }

        public static String RenderRazorViewToString(ControllerContext controllerContext, String viewName, Object model)
        {
            controllerContext.Controller.ViewData.Model = model;

            using (var sw = new StringWriter())
            {
                var ViewResult = ViewEngines.Engines.FindPartialView(controllerContext, viewName);
                var ViewContext = new ViewContext(controllerContext, ViewResult.View, controllerContext.Controller.ViewData, controllerContext.Controller.TempData, sw);
                ViewResult.View.Render(ViewContext, sw);
                ViewResult.ViewEngine.ReleaseView(controllerContext, ViewResult.View);
                return sw.GetStringBuilder().ToString();
            }
        }

        #endregion


    }
}