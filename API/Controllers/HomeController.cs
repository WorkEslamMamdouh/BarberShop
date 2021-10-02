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
using API.Models.CustomModel;

namespace API.Controllers
{
    [EnableCorsAttribute("*", "*", "*")]
    public class HomeController : BaseController
    {

        public class GetStatus
        {
            public int TrNo { get; set; }
            public string StatusName { get; set; }

        }

        insert_Table_Result context = new insert_Table_Result();

        //insert_Table_Result insert_Table = new insert_Table_Result();

        //private insert_Table_Result db = new insert_Table_Result();

        protected SamahEntities db = UnitOfWork.context(BuildConnectionString());

        private readonly IHomeServices HomeService;

        public HomeController(IHomeServices _HomeService)
        {
            this.HomeService = _HomeService;

        }
         
        [HttpPost, AllowAnonymous]
        public IHttpActionResult Insert_SessionStorage ([FromBody]SessionStorage Session)
        {
            if (ModelState.IsValid)
            {
                int Check_ID_Device = 0;

                 Check_ID_Device = db.Database.SqlQuery<int>("select COUNT([ID_Device]) from [dbo].[SessionStorage] Where [ID_Device] = '" + Session.ID_Device + "'").FirstOrDefault();

                if (Check_ID_Device == 0)
                {
                    string qerye = "insert into [dbo].[SessionStorage] values("+ Session.BranchCode+ ",'" + Session.ID_Device + "','" + Session.Name + "','" + Session.Phone + "','" + Session.TR_Type + "'," + Session.page + "," + Session.TurnNumber + "," + Session.ServiceId + "," + Session.Id_Cust + ")";
                    db.Database.ExecuteSqlCommand(qerye);
                }
                else
                { 
                    db.Database.ExecuteSqlCommand("update [dbo].[SessionStorage] set BranchCode = "+ Session.BranchCode + ",[Name] ='" + Session.Name + "' , [Phone] = '" + Session.Phone + "', [page] = 2  where ID_Device = '" + Session.ID_Device + "'");
                }
                 
                return Ok(new BaseResponse(100));
            }
            return BadRequest(ModelState);
        }
         
        [HttpGet, AllowAnonymous]
        public IHttpActionResult Get_Uesr_Session(string ID_Device)
        {
            if (ModelState.IsValid)
            {
                var SessionStorage = db.Database.SqlQuery<SessionStorage>("select * from SessionStorage Where [ID_Device] = N'" + ID_Device + "'").ToList();

                if (SessionStorage.Count > 0)
                {
                    if (SessionStorage[0].page == null)
                    {
                        SessionStorage[0].page = 2;
                    }
                }
             

                return Ok(new BaseResponse(SessionStorage));
            }
            return BadRequest(ModelState);
        }

        [HttpGet, AllowAnonymous] //done
        public IHttpActionResult GetBranch()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var Branch = db.Database.SqlQuery<G_Branch>("select * from [dbo].[G_Branch]").ToList();
                    return Ok(new BaseResponse(Branch));

                }
                catch (Exception ex)
                {
                    return Ok(new BaseResponse(HttpStatusCode.ExpectationFailed, ex.Message));
                }
            }
            return BadRequest(ModelState);
        }


        [HttpGet, AllowAnonymous] // done
        public IHttpActionResult GetAll(string TR_Type , int BranchCode)
        {
            if (ModelState.IsValid)
            {
                var nationality = HomeService.GetAll(x => x.Type == TR_Type && x.BranchCode == BranchCode).ToList();
                //GetAll(x => x.ID == ID)

                return Ok(new BaseResponse(nationality));
            }
            return BadRequest(ModelState);
        }

        [HttpGet, AllowAnonymous]
        public IHttpActionResult GetAll_App(string TR_Type, int ID , string ID_Device , int BranchCode)
        {
            if (ModelState.IsValid)
            {
                Display_App Display = new Display_App();

                GetStatus GSta = new GetStatus();

                var Corse = HomeService.GetAll(x => x.Type == TR_Type && x.cheak == true && x.BranchCode == BranchCode).ToList();

                int TrNo = db.Database.SqlQuery<int>(" select Num from Table_Hagz where ID = " + ID + "").FirstOrDefault();

                int StatusName = db.Database.SqlQuery<int>(" Cheack_Num_Home_App  '" + TR_Type + "' , " + ID + " , "+ BranchCode + "").FirstOrDefault();


                Display.Table_Hagz = Corse;
                GSta.TrNo = TrNo;

                string Name;
                if (StatusName == -1)
                {
                    Name = "يمكنك الدخول";
                }
                else if (StatusName == -2)
                {
                    Name = "لقد بدائت الحلاقة نتمني لكم وقت طيب";
                }
                else if (StatusName == -3)
                {
                    Name = "الحجز الخاص بك غير موجود او تم الانتهتء من الخدمة الرجاء الحجز مره اخري";
                    db.Database.ExecuteSqlCommand("update [dbo].[SessionStorage] set [page] = 2  where ID_Device = '" + ID_Device + "'");
                }
                else
                {
                    Name = StatusName.ToString();
                }
                GSta.StatusName = Name;

                Display.GetSts = GSta;


                return Ok(new BaseResponse(Display));
            }
            return BadRequest(ModelState);
        }

        [HttpGet, AllowAnonymous]
        public IHttpActionResult insert_Table_on_App(string Name, string Phone, string Type, string Message, string TR_Type , string ID_Device ,int BranchCode)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var companies = db.Database.SqlQuery<Table_Hagz>("insert_Table_on_App  N'" + Name + "',N'" + Phone + "',N'" + Type + "',N'" + Message + "',N'" + TR_Type + "' , "+ BranchCode + "").ToList();
                    //var companies = db.GFun_Companies(userCode).ToList();
                    db.Database.ExecuteSqlCommand("update [dbo].[SessionStorage] set [page] = 5  , TurnNumber ="+ companies[0].Num + " , Id_Cust="+ companies[0].ID+ ",TR_Type="+ TR_Type + " where ID_Device = '" + ID_Device + "'");

                    return Ok(new BaseResponse(companies));


                }
                catch (Exception ex)
                {
                    return Ok(new BaseResponse(HttpStatusCode.ExpectationFailed, ex.Message));
                }
            }
            return BadRequest(ModelState);
        }

        [HttpGet, AllowAnonymous] //done
        public IHttpActionResult PROC_insert_Table(string Name, string Phone, string Type, string Message, string TR_Type, int BranchCode)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Database.ExecuteSqlCommand("insert_Table  N'" + Name + "',N'" + Phone + "',N'" + Type + "',N'" + Message + "',N'" + TR_Type + "' , "+ BranchCode + "");
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
                    db.Database.ExecuteSqlCommand("update [dbo].[Table_Tim_work] set [Cheak] = " + Cheak + " where  [ID] = " + ID + "");
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
         
        [HttpGet, AllowAnonymous] //Done
        public IHttpActionResult insert_Table_Tim_work(string Name, int Cheak, int BranchCode)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var companies = db.Database.SqlQuery<insert_tw_Result>("insert_tw '" + Name + "'," + Cheak + ","+ BranchCode + "").ToList();
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

        [HttpGet, AllowAnonymous] //Done
        public IHttpActionResult GetAllEmb_InApp(int TR_Type ,string ID_Device , int BranchCode)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var companies = db.Database.SqlQuery<Table_Tim_work>("select * from [dbo].[Table_Tim_work] where  BranchCode = "+ BranchCode + " ").ToList();
                    //var companies = db.GFun_Companies(userCode).ToList();

                    db.Database.ExecuteSqlCommand("update [dbo].[SessionStorage] set [page] = 3 , [TR_Type] = "+ TR_Type + "  where ID_Device = '" + ID_Device + "'");


                    return Ok(new BaseResponse(companies));


                }
                catch (Exception ex)
                {
                    return Ok(new BaseResponse(HttpStatusCode.ExpectationFailed, ex.Message));
                }
            }
            return BadRequest(ModelState);
        }

        [HttpGet, AllowAnonymous] //---done
        public IHttpActionResult GetAllTable_Tim_work(int BranchCode)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var companies = db.Database.SqlQuery<Table_Tim_work>("select * from [dbo].[Table_Tim_work] where  BranchCode = "+ BranchCode + "").ToList();
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

        [HttpGet, AllowAnonymous] //---done
        public IHttpActionResult select_Table_Tim_work(int BranchCode)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var companies = db.Database.SqlQuery<select_Table_Tim_work_Result>("select_Table_Tim_work "+ BranchCode + "").ToList();
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
        public IHttpActionResult closeDay(int BranchCode)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Database.ExecuteSqlCommand("closeDay "+ BranchCode + "");
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
        public IHttpActionResult Cheack_Num_Confirm(string TrType , string ID_Device ,int BranchCode)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var companies = db.Database.SqlQuery<int>("Cheack_Num_Confirm " + TrType + " , "+ BranchCode + "").ToList();

                    db.Database.ExecuteSqlCommand("update [dbo].[SessionStorage] set [page] = 4 , TR_Type ="+ TrType + " where ID_Device = '" + ID_Device + "'");


                    return Ok(new BaseResponse(companies));

                }
                catch (Exception ex)
                {
                    return Ok(new BaseResponse(HttpStatusCode.ExpectationFailed, ex.Message));
                }
            }
            return BadRequest(ModelState);
        }



        [HttpGet, AllowAnonymous] //done
        public IHttpActionResult cheakcloseDay(int BranchCode)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var companies = db.Database.SqlQuery<int>("select LastNum from Table_LastNum where [Type] ='3' and BranchCode ="+ BranchCode + "").ToList();
                    return Ok(new BaseResponse(companies));

                }
                catch (Exception ex)
                {
                    return Ok(new BaseResponse(HttpStatusCode.ExpectationFailed, ex.Message));
                }
            }
            return BadRequest(ModelState);
        }

        [HttpGet, AllowAnonymous] //done
        public IHttpActionResult Delete_Cut(int ID, string ID_Device , int BranchCode)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Database.ExecuteSqlCommand("Delete_Rows " + ID + " , "+ BranchCode + "");  
                    db.Database.ExecuteSqlCommand("update [dbo].[SessionStorage] set [page] = 2 , [TR_Type] = null ,Id_Cust =null where ID_Device = '" + ID_Device + "'");
                     
                    return Ok(new BaseResponse(100));


                }
                catch (Exception ex)
                {
                    return Ok(new BaseResponse(HttpStatusCode.ExpectationFailed, ex.Message));
                }
            }
            return BadRequest(ModelState);
        }

        [HttpGet, AllowAnonymous] //done
        public IHttpActionResult PROC_Delete_Rows(int ID , int BranchCode)
        {
            if (ModelState.IsValid)
            {
                try
                {
                      db.Database.ExecuteSqlCommand("Delete_Rows " + ID + ","+ BranchCode + "");
                     

                    return Ok(new BaseResponse(100));


                }
                catch (Exception ex)
                {
                    return Ok(new BaseResponse(HttpStatusCode.ExpectationFailed, ex.Message));
                }
            }
            return BadRequest(ModelState);
        }

        [HttpGet, AllowAnonymous]//done
        public IHttpActionResult PROC_Enter_Customer(int ID, string TR_Type , int BranchCode )
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var companies = db.Database.SqlQuery<Enter_Customer_Result>("Enter_Customer " + ID + ",'" + TR_Type + "' , "+ BranchCode + "").ToList();

                    return Ok(new BaseResponse(companies));


                }
                catch (Exception ex)
                {
                    return Ok(new BaseResponse(HttpStatusCode.ExpectationFailed, ex.Message));
                }
            }
            return BadRequest(ModelState);
        }
         
    }
}
