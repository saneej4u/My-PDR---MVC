using Microsoft.AspNet.Identity;
using NewPDR.Model;
using NewPDR.Service;
using NewPDR.Service.Services;
using NewPDR.Web.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewPDR.Web.Controllers
{
    [Authorize]
    public class ProfileController : Controller
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

        public ProfileController(IUserService userService, IUserTypeService userTypeService, IPDRService pdrS, UserManager<ApplicationUser> userManager, IHRProPersonnelRecordService ihrpro, IToDoListService todo, IActivityService actser)
        {
            this._UserService = userService;
            this._UserTypeService = userTypeService;
            this._PDRService = pdrS;
            this._UserManager = userManager;
            this._HRProPersonnelRecordService = ihrpro;
            this._IToDoListService = todo;
            this._IActivityService = actser;
        }


        // GET: Profile
        public ActionResult Index()
        {
            if (LoggenInUser != null)
            {
                TitleViewModel model = new TitleViewModel();
                model.AllUsers = new List<ApplicationUser>();
                model.AllUsers.Add(LoggenInUser);

                var hrProcolleagues = _HRProPersonnelRecordService.GetAll().Where(x => x.LineManagerEmailId == LoggenInUser.UserName).ToList();

                if (hrProcolleagues != null)
                {
                    var pdrDbColleages = _UserService.GetAllUsers(hrProcolleagues.Select(x => x.EmailId).ToList()).ToList();

                    if (pdrDbColleages.Any())
                        model.AllUsers.AddRange(pdrDbColleages);
                }


                return View(model);
            }
            return View();

        }

        public ActionResult ProfileDetails(string emailId)
        {

            return View();
        }
    }
}