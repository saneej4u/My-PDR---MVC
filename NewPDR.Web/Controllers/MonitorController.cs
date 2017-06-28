using Microsoft.AspNet.Identity;
using NewPDR.Model;
using NewPDR.Service;
using NewPDR.Web.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewPDR.Web.Controllers
{
    [Authorize]
    public class MonitorController : Controller
    {


        #region " Property "

        private IUserService _UserService;
        private IUserTypeService _UserTypeService;
        private IPDRService _PDRService;
        private UserManager<ApplicationUser> _UserManager;

        private IHRProPersonnelRecordService _HRProPersonnelRecordService;


        private ApplicationUser LoggenInUser
        {
            get
            {
                return _UserService.GetUsers().ToList().FirstOrDefault(x => x.UserName == (HttpContext.User.Identity.Name));
            }
        }

        #endregion

        public MonitorController(IUserService userService, IUserTypeService userTypeService, IPDRService pdrS, UserManager<ApplicationUser> userManager, IHRProPersonnelRecordService ihrpro)
        {
            this._UserService = userService;
            this._UserTypeService = userTypeService;
            this._PDRService = pdrS;
            this._UserManager = userManager;
            this._HRProPersonnelRecordService = ihrpro;
        }

        // GET: Monitor
        public ActionResult Index(string userId)
        {

            var user = _UserService.GetUser(userId);
            var model = new ChartViewModel();

            if (user != null)
            {
                List<PDReview> pdrs = _PDRService.GetAllPDReviews().Where(x => x.ReviewerUserId == user.Id).ToList();

                List<MidYearChartViewModel> midYearChartViewModel = new List<MidYearChartViewModel>();
                List<FullYearChartViewModel> fullYearChartViewModel = new List<FullYearChartViewModel>();

                if (pdrs != null)
                {
                    foreach (var pdr in pdrs)
                    {

                        var midObjR = pdr.MidyearObjectiveOverallScore.HasValue ? pdr.MidyearObjectiveOverallScore.Value : 0;
                        var midSfR = pdr.MidYearSuccessFactorOverallScore.HasValue ? pdr.MidYearSuccessFactorOverallScore.Value : 0;
                        var midOvR = pdr.MidYearSuccessFactorOverallScore.HasValue ? pdr.MidYearSuccessFactorOverallScore.Value : 0;


                        MidYearChartViewModel midCVM = new MidYearChartViewModel
                        {
                            ChartTitle = string.Format("{0} Mid year PDR ratings", pdr.ReviewPeriod.Year),
                            Name = "MidYearChart",
                            UserId = user.Id,
                            xValues = new List<string> { " Objectives ", " Success Factor ", " Overall " },
                            yValues = new List<int> { midObjR, midSfR, midOvR },
                            IsChartValueZero = ((midObjR == 0) && (midSfR == 0) && (midOvR == 0)) || (pdr.MidYearStatus != 3)
                        };

                        midYearChartViewModel.Add(midCVM);


                        var fullObjR = pdr.FullYearObjectiveOverallScore.HasValue ? pdr.FullYearObjectiveOverallScore.Value : 0;
                        var fullSfR = pdr.FullYearSuccessFactorOverallScore.HasValue ? pdr.FullYearSuccessFactorOverallScore.Value : 0;
                        var fullOvR = pdr.FullYearOverallScore.HasValue ? pdr.FullYearOverallScore.Value : 0;


                        FullYearChartViewModel fullCVM = new FullYearChartViewModel
                        {
                            ChartTitle = string.Format("{0} Full year PDR ratings", pdr.ReviewPeriod.Year),
                            Name = "FullYearChart",
                            UserId = user.Id,
                            xValues = new List<string> { " Objectives ", " Success Factor ", " Overall " },
                            yValues = new List<int> { fullObjR, fullSfR, fullOvR },
                            IsChartValueZeroFullYear = ((fullObjR == 0) && (fullSfR == 0) && (fullOvR == 0)) || (pdr.FullYearStatus != 3)
                        };

                        fullYearChartViewModel.Add(fullCVM);

                    };

                    model.UserId = user.Id;
                    model.UserName = user.UserName;
                    model.MidYearChartViewModels = midYearChartViewModel;
                    model.FullYearChartViewModels = fullYearChartViewModel;
                }
            }

            return View(model);
        }

        public PartialViewResult SiteNavigation()
        {

            var model = new SideNavigationViewModel();

            model.MyPDRs = _PDRService.GetAllPDReviews().Where(x => x.ReviewerUserId == LoggenInUser.Id).ToList();


            return PartialView("_SideNavigationPartial", model);
        }
    }
}