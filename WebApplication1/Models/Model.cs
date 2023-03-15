using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.ModelConfiguration;

namespace WebApplication1.Models
{
    public class Model
    {

        public int ModelId { get; set; }

        
        public string ModelName { get; set; }
        public string ModelDescription { get; set; }
        

    }
}