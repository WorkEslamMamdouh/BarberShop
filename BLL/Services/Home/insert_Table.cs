using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Domain
{
    class insert_Table
    {

      
            public int ID { get; set; }
            public Nullable<int> Num { get; set; }
            public string Name { get; set; }
            public string Phone { get; set; }
            public string Type { get; set; }
            public string Message { get; set; }
            public Nullable<bool> cheak { get; set; }
            public string StatusFlag { get; set; }
       
    }
}
