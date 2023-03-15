using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.ModelConfiguration;

namespace WebApplication1.Models
{
    public class RobotType
    {
        public int   RtypeId { get; set; }

        public int? ModelId{ get; set; }
      
        
        public string RtypeName { get; set; }

        public string RtypeDescription { get; set; }

        


    }
}