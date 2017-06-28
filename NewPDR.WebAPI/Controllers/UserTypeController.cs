using NewPDR.Model;
using NewPDR.Service;
using NewPDR.Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace NewPDR.WebAPI.Controllers
{
     [RoutePrefix("api/usertypeconfig")]
    public class UserTypeController : ApiController
    {

        #region " Property "

        private IUserService _UserService;
        private IUserTypeService _UserTypeService;
        private IPDRConfigService _IPDRConfigService;
        //private UserManager<ApplicationUser> _UserManager;

        private IHRProPersonnelRecordService _HRProPersonnelRecordService;

        private IToDoListService _IToDoListService;

        private IActivityService _IActivityService;



        #endregion

        public UserTypeController(IUserService userService, IUserTypeService userTypeService, IPDRConfigService pdrS, IHRProPersonnelRecordService ihrpro, IToDoListService todo, IActivityService actser)
        {
            this._UserService = userService;
            this._UserTypeService = userTypeService;
            this._IPDRConfigService = pdrS;
            //this._UserManager = userManager;
            this._HRProPersonnelRecordService = ihrpro;
            this._IToDoListService = todo;
            this._IActivityService = actser;
        }

        [Route("usertype/all")]
        public IEnumerable<UserType> GetAllUserType()
        {
            return _IPDRConfigService.GetAllUserType();
        }

    }
}
