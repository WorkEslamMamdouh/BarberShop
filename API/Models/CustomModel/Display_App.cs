using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL.Domain;
using API.Models;
using static API.Controllers.HomeController;

namespace API.Models.CustomModel
{
    
    public class Display_App
    {
        public List<Table_Hagz> Table_Hagz { get; set; }
        public GetStatus GetSts { get; set; }
    }



}