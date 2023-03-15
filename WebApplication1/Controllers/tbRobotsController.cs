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
    public class tbRobotsController : ApiController
    {
        private ReefKineticsEntities db = new ReefKineticsEntities();

       
        public IHttpActionResult GettbRobots()
        {

            var a = db.tbRobots.Include(p => p.tbUser).Include(x => x.tbRobotType.tbModel).ToList();
            List<Robot > inst = new List<Robot >();
            foreach (var item in a)
            {
                Robot  Robots = new Robot ();
                Robots.RobotId = item.RobotId;
                Robots.RobotName  = item.RobotName  ;
                Robots.RtypeId  = item.RtypeID  ;
                Robots.UserId   = item.UserId ;
                if (item.tbUser != null)
                {
                    Robots.userFullName = item.tbUser.FullName;
                    Robots.userEmail = item.tbUser.Email;
                }
                   else
                {
                    Robots.userFullName = null;
                    Robots.userEmail = null;
                }


                if (item.tbRobotType != null)
                {
                    Robots.RTypeName = item.tbRobotType.RtypeName;
                    Robots.ModelName = item.tbRobotType.tbModel.ModelName;
                }
                    

                else
                {

                    Robots.RTypeName = null;
                    Robots.ModelName = null;
                }

                inst.Add(Robots);

            }
            return Ok(inst);
        }


        public object GettbRobotAfterpost(tbRobot robot)
        {

            var a = db.tbRobots.Include(p => p.tbUser).Include(x => x.tbRobotType.tbModel).Where(x => x.RobotId == robot.RobotId).ToList();
            List<Robot> inst = new List<Robot>();
            foreach (var item in a)
            {
                Robot Robots = new Robot();
                Robots.RobotId = item.RobotId;
                Robots.RobotName = item.RobotName;
                Robots.RtypeId = item.RtypeID;
                Robots.UserId = item.UserId;
                if (item.tbUser != null)
                {
                    Robots.userFullName = item.tbUser.FullName;
                    Robots.userEmail = item.tbUser.Email;
                }
                else
                {
                    Robots.userFullName = null;
                    Robots.userEmail = null;
                }


                if (item.tbRobotType != null)
                {
                    Robots.RTypeName = item.tbRobotType.RtypeName;
                    Robots.ModelName = item.tbRobotType.tbModel.ModelName;
                }


                else
                {

                    Robots.RTypeName = null;
                    Robots.ModelName = null;
                }

                inst.Add(Robots);

            }
            return inst[0];
        }


        [ResponseType(typeof(tbUser ))]
        public IHttpActionResult GettbUser()
        {
            var a = db.tbUsers .ToList();
            List<User > inst = new List<User >();
            foreach (var item in a)
            {
                User   users = new User  ();
                users.FullName  = item.FullName ;

                users.UserId = item.UserId;
                inst.Add(users);

            }
            return Ok(inst);
        }

        [ResponseType(typeof(tbRobotType ))]
        public IHttpActionResult GetRobotType()
        {
            var a = db.tbRobotTypes .ToList();
            List<RobotType > inst = new List<RobotType >();
            foreach (var item in a)
            {
                RobotType  robotType = new RobotType();
                robotType.RtypeName = item.RtypeName;

                robotType.RtypeId  = item.RTypeId;
                inst.Add(robotType);

            }
            return Ok(inst);
        }

        [ResponseType(typeof(void))]
        [HttpPut]
        public IHttpActionResult PuttbRobot(tbRobot  tbRobot)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            

            db.Entry(tbRobot ).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                
                    throw;
                
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        
        [ResponseType(typeof(tbRobot ))]
        [HttpPost]
        public IHttpActionResult PosttbRobot(tbRobot tbRobot)
        {
           


            if (tbRobot.UserId == 0)
                tbRobot.UserId = null;
            if (tbRobot.RtypeID == 0)
                tbRobot.RtypeID = null;


            db.tbRobots .Add(tbRobot );

         

            try
            {
                db.SaveChanges();
                



            }
            catch (DbUpdateException)
            {
                if (tbStudentExists(tbRobot .RobotId ))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = tbRobot .RobotId  }, GettbRobotAfterpost(tbRobot));
        }

        // DELETE: api/tbStudents/5
        [ResponseType(typeof(tbRobot ))]
        [HttpDelete]
        public IHttpActionResult DeletetbStudent(int id)
        {
            tbRobot tbRobot  = db.tbRobots .Find(id);
            if (tbRobot  == null)
            {
                return NotFound();
            }

            db.tbRobots .Remove(tbRobot);
            db.SaveChanges();

            return Ok(tbRobot );
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        
        private bool tbStudentExists(int id)
        {
            return db.tbRobots .Count(e => e.RobotId  == id) > 0;
        }

    
    }
}