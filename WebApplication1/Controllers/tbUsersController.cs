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
    
    public class tbUsersController : ApiController
    {
        private ReefKineticsEntities db = new ReefKineticsEntities();

        [AllowAnonymous]
        public IHttpActionResult Login(WebApplication1.Models.User user)
        {
            //check the password and the user name

           if (UserExists(user.UserName,user.Password))
            {
                var res = WebApplication1.Models.User.GenerateJwtToken(user.UserName);

                return Ok(res);
               }
            else
            {
                return Ok(false);
            }


           
        }

            // GET: api/tbUsers
            public IHttpActionResult GetUsers()
        {

            var userModel = db.tbUsers .ToList();
            List<User> inst = new List<User>();
            foreach (var item in userModel)
            {
                User User = new User();
                User.FullName  = item.FullName ;
                User.Email = item.Email;
                User.UserName  = item.UserName ;
                User.Password = item.Password;
                User.Address   = item.Address  ;
                User.AddDate  = item.AddDate  ;
                inst.Add(User);

            }
            return Ok(inst);
        }

       

       
        [ResponseType(typeof(void))]
        public IHttpActionResult PuttbInstructor(int id, tbUser  tbUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tbUser .UserId )
            {
                return BadRequest();
            }

            db.Entry(tbUser ).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tbUserExists(id))
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

        
        [ResponseType(typeof(tbUser ))]
        public IHttpActionResult PosttUser(tbUser  tbUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tbUsers .Add(tbUser );

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (tbUserExists(tbUser.UserId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = tbUser .UserId  }, tbUser );
        }

        
        [ResponseType(typeof(tbUser ))]
        public IHttpActionResult DeletetbInstructor(int id)
        {
            tbUser  tbUser  = db.tbUsers .Find(id);
            if (tbUser == null)
            {
                return NotFound();
            }

            db.tbUsers .Remove(tbUser);
            db.SaveChanges();

            return Ok(tbUser);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tbUserExists(int  id)
        {
            return db.tbUsers .Count(e => e.UserId  == id) > 0;
        }

        private bool UserExists(string UserName, string Password)
        {
            return db.tbUsers.Count(e => e.UserName == UserName && e.Password==Password) > 0;
        }

    }
}