using API.Models;
using BLL.Services.Home;
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
using System.Data.SqlClient;
using System.Data.Entity;
using WebUl.DAL.Repository;
using Newtonsoft.Json;

namespace API.Controllers
{
    [EnableCorsAttribute("*", "*", "*")]
    public class HomeController : BaseController
    {

        insert_Table_Result context = new insert_Table_Result();

        //insert_Table_Result insert_Table = new insert_Table_Result();

        //private insert_Table_Result db = new insert_Table_Result();

        protected SamahEntities db = UnitOfWork.context(BuildConnectionString());

        private readonly IHomeServices HomeService;

        public HomeController(IHomeServices _HomeService)
        {
            this.HomeService = _HomeService;

        }

        [HttpGet, AllowAnonymous]
        public IHttpActionResult GetAll(string TR_Type)
        {
            if (ModelState.IsValid)
            {
                var nationality = HomeService.GetAll(x => x.Type == TR_Type).ToList();
                //GetAll(x => x.ID == ID)

                return Ok(new BaseResponse(nationality));
            }
            return BadRequest(ModelState);
        }

        [HttpGet, AllowAnonymous]
        public IHttpActionResult PROC_insert_Table(string Name, string Phone, string Type, string Message , string TR_Type)
        {
            if (ModelState.IsValid)
            {
                try
                {
                     db.Database.ExecuteSqlCommand("insert_Table  N'" + Name + "',N'" + Phone + "',N'" + Type + "',N'" + Message + "',N'" + TR_Type + "'");
                    //var companies = db.GFun_Companies(userCode).ToList();

                    return Ok(new BaseResponse(100));


                }
                catch (Exception ex)
                {
                    return Ok(new BaseResponse(HttpStatusCode.ExpectationFailed, ex.Message));
                }
            }
            return BadRequest(ModelState);
        }


        [HttpGet, AllowAnonymous]
        public IHttpActionResult UpdateTable_Tim_work(int Cheak, int ID)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Database.ExecuteSqlCommand("update [dbo].[Table_Tim_work] set [Cheak] = "+ Cheak + " where  [ID] = "+ ID + "");
                    //var companies = db.GFun_Companies(userCode).ToList();

                    return Ok(new BaseResponse(100));


                }
                catch (Exception ex)
                {
                    return Ok(new BaseResponse(HttpStatusCode.ExpectationFailed, ex.Message));
                }
            }
            return BadRequest(ModelState);
        }


        [HttpGet, AllowAnonymous]
        public IHttpActionResult insert_Table_Tim_work(string Name, int Cheak)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var companies = db.Database.SqlQuery<insert_tw_Result>("insert_tw '" + Name + "',"+ Cheak + "").ToList();
                    //var companies = db.GFun_Companies(userCode).ToList();

                    return Ok(new BaseResponse(companies));


                }
                catch (Exception ex)
                {
                    return Ok(new BaseResponse(HttpStatusCode.ExpectationFailed, ex.Message));
                }
            }
            return BadRequest(ModelState);
        }

        [HttpGet, AllowAnonymous]
        public IHttpActionResult GetAllTable_Tim_work()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var companies = db.Database.SqlQuery<Table_Tim_work>("select * from [dbo].[Table_Tim_work] ").ToList();
                    //var companies = db.GFun_Companies(userCode).ToList();

                    return Ok(new BaseResponse(companies));


                }
                catch (Exception ex)
                {
                    return Ok(new BaseResponse(HttpStatusCode.ExpectationFailed, ex.Message));
                }
            }
            return BadRequest(ModelState);
        }

        [HttpGet, AllowAnonymous]
        public IHttpActionResult select_Table_Tim_work( )
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var companies = db.Database.SqlQuery<select_Table_Tim_work_Result>("select_Table_Tim_work ").ToList();
                    //var companies = db.GFun_Companies(userCode).ToList();

                    return Ok(new BaseResponse(companies));


                }
                catch (Exception ex)
                {
                    return Ok(new BaseResponse(HttpStatusCode.ExpectationFailed, ex.Message));
                }
            }
            return BadRequest(ModelState);
        }

        [HttpGet, AllowAnonymous]
        public IHttpActionResult closeDay()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Database.ExecuteSqlCommand("closeDay"); 
                    return Ok(new BaseResponse(100));
                     
                }
                catch (Exception ex)
                {
                    return Ok(new BaseResponse(HttpStatusCode.ExpectationFailed, ex.Message));
                }
            }
            return BadRequest(ModelState);
        }

        [HttpGet, AllowAnonymous]
        public IHttpActionResult cheakcloseDay()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var companies = db.Database.SqlQuery<int>("select LastNum from Table_LastNum where [Type] ='3'").ToList();
                    return Ok(new BaseResponse(companies));

                }
                catch (Exception ex)
                {
                    return Ok(new BaseResponse(HttpStatusCode.ExpectationFailed, ex.Message));
                }
            }
            return BadRequest(ModelState);
        }

        [HttpGet, AllowAnonymous]
        public IHttpActionResult PROC_Delete_Rows(int ID)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var companies = db.Database.SqlQuery<Delete_Rows_Result>("Delete_Rows " + ID + "").ToList();
                   
                    return Ok(new BaseResponse(companies));


                }
                catch (Exception ex)
                {
                    return Ok(new BaseResponse(HttpStatusCode.ExpectationFailed, ex.Message));
                }
            }
            return BadRequest(ModelState);
        }
        [HttpGet, AllowAnonymous]
        public IHttpActionResult PROC_Enter_Customer(int ID, string TR_Type)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var companies = db.Database.SqlQuery<Enter_Customer_Result>("Enter_Customer " + ID + ",'" + TR_Type + "'").ToList();

                    return Ok(new BaseResponse(companies));


                }
                catch (Exception ex)
                {
                    return Ok(new BaseResponse(HttpStatusCode.ExpectationFailed, ex.Message));
                }
            }
            return BadRequest(ModelState);
        }

        protected IEnumerable<T> Get<T>(string SqlStatement)
        {
            //var companiesList = new List<Table_Hagz>();
            //foreach (var company in companies)
            //{
            //    var comp = new Table_Hagz();
            //    comp.ID = company.ID;
            //    comp.Num= company.Num;
            //    comp.Name= SecuritySystem.Decrypt(company.Name);
            //    comp.Phone= SecuritySystem.Decrypt(company.Phone);
            //    comp.Type = SecuritySystem.Decrypt(company.Type);
            //    comp.Message= SecuritySystem.Decrypt(company.Message);
            //    comp.cheak = Convert.ToBoolean(company.cheak);
            //    comp.StatusFlag = SecuritySystem.Decrypt(company.StatusFlag);
            //    companiesList.Add(comp);
            //};
            //return Ok(companiesList);



            //var SqlStatment = "insert_Table  '" + Name + "','" + Phone + "','" + Type + "','" + Message + "'";

            ////string result = this.ExecuteScalar(SqlStatment);

            //var result = this.Get<object>(SqlStatment);

            //return Ok(new BaseResponse(companiesList));   
            //SqlParameter[] Param = new SqlParameter[] {
            //       new SqlParameter("@Name",Name),
            //       new SqlParameter("@Phone",Phone),
            //       new SqlParameter("@Type",Type),
            //       new SqlParameter("@Message",Message)

            //};
            //db.Database.SqlQuery<string>("Select name from sys.tables").ToList();
            //var data = db.Database.SqlQuery<string>("insert_Table  '"+ Name + "','" + Phone + "','" + Type + "','" + Message + "'").ToList();

            string connectionString = db.Database.Connection.ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = SqlStatement;
                    connection.Open();
                    Table_Hagz table = new Table_Hagz();
                    //table.Load(command.ExecuteReader());
                    connection.Close();
                    command.Dispose();
                    connection.Dispose();

                    var result = JsonConvert.DeserializeObject<IEnumerable<T>>(JsonConvert.SerializeObject(table));
                    return result;
                }
            }

        }

        public string ExecuteScalar(string SqlStatement)
        {
            string connectionString = db.Database.Connection.ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = SqlStatement;
                    connection.Open();

                    string result = string.Empty;

                    result = command.ExecuteScalar().ToString();
                    connection.Close();
                    command.Dispose();
                    connection.Dispose();


                    return result;
                }
            }

        }

        [HttpGet, AllowAnonymous]
        public IHttpActionResult GetById(int id)
        {
            if (ModelState.IsValid)
            {
                var nationality = HomeService.GetById(id);

                return Ok(new BaseResponse(nationality));
            }
            return BadRequest(ModelState);
        }


        [HttpPost, AllowAnonymous]
        public IHttpActionResult Insert([FromBody]Table_Hagz Nation)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var Nationality = HomeService.Insert(Nation);
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
            if (ModelState.IsValid)
            {
                try
                {
                    HomeService.Delete(ID);
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
        public IHttpActionResult Update([FromBody]Table_Hagz Nation)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var Nationality = HomeService.Update(Nation);
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
        public IHttpActionResult UpdateLst(List<Table_Hagz> Table_Hagz)
        {
            try
            {
                HomeService.UpdateList(Table_Hagz);
                return Ok(new BaseResponse());
            }
            catch (Exception ex)
            {
                return Ok(new BaseResponse(HttpStatusCode.ExpectationFailed, ex.Message));
            }
        }

    }
}
