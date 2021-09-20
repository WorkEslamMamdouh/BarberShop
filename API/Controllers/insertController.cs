using API.Models;
using BLL.Services.insert_Table;
using DAL.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using API.Controllers;
using WebUl.API.Tools;
using System.Web.Http.Cors;

namespace API.Controllers
{
    [EnableCorsAttribute("*", "*", "*")]
    public class insertController : BaseController
    {
        private readonly Iinsert_TableServices insert_TableServices;
    
        public insertController(Iinsert_TableServices _insert_TableServices)
        {
            this.insert_TableServices = _insert_TableServices;
            
        }

        [HttpGet, AllowAnonymous]
        public IHttpActionResult GetAll()
        {
            if (ModelState.IsValid)
            {
                var nationality = insert_TableServices.GetAll().ToList();
                //GetAll(x => x.ID == ID)

                return Ok(new BaseResponse(nationality));
            }
            return BadRequest(ModelState);
        }

        [HttpGet, AllowAnonymous]
        public IHttpActionResult GetById(int id)
        {
            if (ModelState.IsValid)
            {
                var nationality = insert_TableServices.GetById(id);

                return Ok(new BaseResponse(nationality));
            }
            return BadRequest(ModelState);
        }


        [HttpPost, AllowAnonymous]
        public IHttpActionResult Insert([FromBody]insert_Table_Result Nation)
        {
            if (ModelState.IsValid )
            {
                try
                {
                    var Nationality = insert_TableServices.Insert(Nation);
                    return Ok(new BaseResponse(Nationality));
                }
                catch (Exception ex)
                {
                    return Ok(new BaseResponse(HttpStatusCode.ExpectationFailed, ex.Message));
                }
            }
            return BadRequest(ModelState);
        }
        [HttpGet, AllowAnonymous]
        public IHttpActionResult Delete(int ID)
        {
            if (ModelState.IsValid )
            {
                try
                {
                    insert_TableServices.Delete(ID);
                    return Ok(new BaseResponse());
                }
                catch (Exception ex)
                {
                    return Ok(new BaseResponse(0, "Error"));
                }

            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        [HttpPost, AllowAnonymous]
        public IHttpActionResult Update([FromBody]insert_Table_Result Nation)
        {
            if (ModelState.IsValid )
            {
                try
                {
                    var Nationality = insert_TableServices.Update(Nation);
                    return Ok(new BaseResponse(Nationality));
                }
                catch (Exception ex)
                {
                    return Ok(new BaseResponse(HttpStatusCode.ExpectationFailed, ex.Message));
                }
            }
            return BadRequest(ModelState);
        }



        //***************asmaa********************//
        [HttpPost, AllowAnonymous]
        public IHttpActionResult UpdateLst(List<insert_Table_Result> insert_Table_Result)
        {
            try
            {
                insert_TableServices.UpdateList(insert_Table_Result);
                return Ok(new BaseResponse());
            }
            catch (Exception ex)
            {
                return Ok(new BaseResponse(HttpStatusCode.ExpectationFailed, ex.Message));
            }
        }

    }
}
