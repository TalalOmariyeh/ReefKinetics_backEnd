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
    public class tbRobotTypeController : ApiController
    {
        private ReefKineticsEntities db = new ReefKineticsEntities();

        
        public IQueryable<tbRobotType > GettbRobotTypes()
        {
            return db.tbRobotTypes ;
        }

        
        [ResponseType(typeof(void))]
        public IHttpActionResult PuttbQuiz(int id, tbRobotType tbRobotType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tbRobotType.RTypeId )
            {
                return BadRequest();
            }

            db.Entry(tbRobotType).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tbRobotTypeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        
        [ResponseType(typeof(tbRobotType))]
        public IHttpActionResult PosttbQuiz(tbRobotType tbRobotType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tbRobotTypes .Add(tbRobotType);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (tbRobotTypeExists(tbRobotType.RTypeId ))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = tbRobotType.RTypeId  }, tbRobotType);
        }

        
        [ResponseType(typeof(tbRobotType))]
        public IHttpActionResult DeletetbQuiz(int id)
        {
            tbRobotType tbRobotType = db.tbRobotTypes .Find(id);
            if (tbRobotType == null)
            {
                return NotFound();
            }

            db.tbRobotTypes .Remove(tbRobotType);
            db.SaveChanges();

            return Ok(tbRobotType);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tbRobotTypeExists(int id)
        {
            return db.tbRobotTypes .Count(e => e.RTypeId  == id) > 0;
        }
    }
}