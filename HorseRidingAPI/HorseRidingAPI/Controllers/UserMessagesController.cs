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
using HorseRidingAPI.Models;

namespace HorseRidingAPI.Controllers
{
    public class UserMessagesController : ApiController
    {
        private db_a743eb_horseridingEntities db = new db_a743eb_horseridingEntities();

        // GET: api/UserMessages
        public IQueryable<UserMessage> GetUserMessage()
        {
            return db.UserMessage;
        }

        [Route("api/UserMessagesByUserId/{id}")]
        public IHttpActionResult GetUserMessage(int id)
        {
            return Ok(db.UserMessage.Where(u => u.UserId == id).ToList());
        }

        // PUT: api/UserMessages/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUserMessage(int id, UserMessage userMessage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != userMessage.Id)
            {
                return BadRequest();
            }

            db.Entry(userMessage).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserMessageExists(id))
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

        // POST: api/UserMessages
        [ResponseType(typeof(UserMessage))]
        public IHttpActionResult PostUserMessage(UserMessage userMessage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.UserMessage.Add(userMessage);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = userMessage.Id }, userMessage);
        }

        // DELETE: api/UserMessages/5
        [ResponseType(typeof(UserMessage))]
        public IHttpActionResult DeleteUserMessage(int id)
        {
            UserMessage userMessage = db.UserMessage.Find(id);
            if (userMessage == null)
            {
                return NotFound();
            }

            db.UserMessage.Remove(userMessage);
            db.SaveChanges();

            return Ok(userMessage);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserMessageExists(int id)
        {
            return db.UserMessage.Count(e => e.Id == id) > 0;
        }
    }
}