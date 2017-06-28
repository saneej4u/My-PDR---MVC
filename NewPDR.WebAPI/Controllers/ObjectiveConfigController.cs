using Microsoft.AspNet.Identity;
using NewPDR.Model;
using NewPDR.Service;
using NewPDR.Service.Services;
using NewPDR.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace NewPDR.WebAPI.Controllers
{
    [RoutePrefix("api/objectivetypeconfig")]
    public class ObjectiveConfigController : ApiController
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

        public ObjectiveConfigController(IUserService userService, IUserTypeService userTypeService, IPDRConfigService pdrS, IHRProPersonnelRecordService ihrpro, IToDoListService todo, IActivityService actser)
        {
            this._UserService = userService;
            this._UserTypeService = userTypeService;
            this._IPDRConfigService = pdrS;
            //this._UserManager = userManager;
            this._HRProPersonnelRecordService = ihrpro;
            this._IToDoListService = todo;
            this._IActivityService = actser;
        }

        [Route("objectivetypes/{id}")]
        public IHttpActionResult GetSingleObjectiveType(int id)
        {
            try
            {
                var obtype = _IPDRConfigService.GetSingleObjectiveType(id);

                if (obtype == null)
                {
                    return NotFound();
                }

                return Ok(obtype);
            }
            catch (Exception)
            {
                return InternalServerError();
            }

        }

        [Route("objectivetypes/all")]
        public IEnumerable<ObjectiveType> GetAllObjectiveTypes()
        {
            return _IPDRConfigService.GetAllObjectiveType();
        }

        [Route("objectivetypes/all/usertype/{userTypeId}")]
        public IEnumerable<ObjectiveType> GetAllObjectiveTypesByUserType(int userTypeId)
        {
            return _IPDRConfigService.GetAllObjectiveType().Where(x => x.UserTypeId == userTypeId);
        }


        //Create
        public IHttpActionResult PostObjectiveType(ObjectiveType objtype)
        {
            try
            {

                if (objtype == null)
                {
                    return BadRequest();
                }

                //Replace with create
                _IPDRConfigService.UpdateObjectiveType(objtype);

                return Created("", objtype);

            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }


        //Update
        public IHttpActionResult PutObjectiveType(int id , ObjectiveType objtype)
        {
            try
            {
                if(objtype == null)
                {
                    return BadRequest();
                }

                _IPDRConfigService.UpdateObjectiveType(objtype);
                return Ok(objtype);

            }
            catch(Exception)
            {
                return InternalServerError();
            }
        }

        //Delete
        public IHttpActionResult DeleteObjectiveType(int id)
        {
            try
            {
                var result = _IPDRConfigService.GetSingleObjectiveType(id);

                if (result != null)
                {
                    _IPDRConfigService.DeleteObjectiveType(result);

                    return StatusCode(HttpStatusCode.NoContent);
                }

                return BadRequest();
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }


    }
}
