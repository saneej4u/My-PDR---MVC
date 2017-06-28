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
    public class ColleagueController : Controller
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

        public ColleagueController(IUserService userService, IUserTypeService userTypeService, IPDRService pdrS, UserManager<ApplicationUser> userManager, IHRProPersonnelRecordService ihrpro)
        {
            this._UserService = userService;
            this._UserTypeService = userTypeService;
            this._PDRService = pdrS;
            this._UserManager = userManager;
            this._HRProPersonnelRecordService = ihrpro;
        }


        // GET: Colleague
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ShowColleagues()
        {
            List<ColleaguesViewModel> model = PopulateColleaguesViewModel(LoggenInUser);

            return View( model);
        }

        private List<ColleaguesViewModel> PopulateColleaguesViewModel(ApplicationUser user)
        {
            var colleaguesViewModel = new List<ColleaguesViewModel>();
            var hrProcolleagues = _HRProPersonnelRecordService.GetAll().Where(x => x.LineManagerEmailId == user.UserName).ToList();
            var pdrDbColleages = _UserService.GetAllUsers(hrProcolleagues.Select(x => x.EmailId).ToList()).ToList();

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
    }
}