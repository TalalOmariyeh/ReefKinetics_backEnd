//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplication1
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbRobot
    {
        public int RobotId { get; set; }
        public string RobotName { get; set; }
        public Nullable<int> UserId { get; set; }
        public Nullable<int> RtypeID { get; set; }
    
        public virtual tbRobotType tbRobotType { get; set; }
        public virtual tbUser tbUser { get; set; }
    }
}
