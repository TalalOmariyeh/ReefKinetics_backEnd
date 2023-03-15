using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebApplication1;
using WebApplication1.Middleware;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [CustomAuthorize]
    public class tbModelsController : ApiController
    {
        private ReefKineticsEntities db = new ReefKineticsEntities();

      
        public IHttpActionResult GettbModels()
        {
            {
                var a = db.tbModels .ToList();
                List<Model> inst = new List<Model>();
                foreach (var item in a)
                {
                    Model Model = new Model();
                    Model.ModelName  = item.ModelName ;
                    Model.ModelDescription  = item.ModelDescription ;
                   
                    inst.Add(Model);

                }
                return Ok(inst);
            }
        }

   

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        
    }
}