using NewPDR.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewPDR.Web.ActionFilters
{
    public class UnLockActionFilter : ActionFilterAttribute
    {
    

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            //var _PDRService = DependencyResolver.Current.GetService<IPDRService>();
            //var _UserService = DependencyResolver.Current.GetService<IUserService>();
            //var loggedInUser = _UserService.GetUsers().ToList().FirstOrDefault(x => x.UserName == (HttpContext.Current.User.Identity.Name));
            //var loggedInUsersPDR = _PDRService.GetAllPDReviews().Where(x => x.ReviewerUserId == loggedInUser.Id).ToList();

            ////Unlock my PDR
            //foreach (var pdr in loggedInUsersPDR)
            //{
            //    _PDRService.UnLockPDR(loggedInUser, pdr);
            //}

          
        }
    }
}