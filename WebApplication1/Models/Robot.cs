using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Robot
    {
        public int RobotId { get; set; }
        public String RobotName { get; set; }
        public int? UserId { get; set; }
        public int? RtypeId { get; set; }

        public string userFullName { get; set; }

        public String RTypeName { get; set; }

        public String ModelName { get; set; }

        public string userEmail { get; set; }





    }
}