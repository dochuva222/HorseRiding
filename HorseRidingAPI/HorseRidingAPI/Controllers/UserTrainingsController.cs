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
    public class UserTrainingsController : ApiController
    {
        private db_a743eb_horseridingEntities db = new db_a743eb_horseridingEntities();

        // GET: api/UserTrainings
        public IQueryable<UserTraining> GetUserTraining()
        {
            return db.UserTraining;
        }

        [Route("api/ActualTrainings")]
        public IHttpActionResult GetActualTrainings()
        {
            var currentDate = DateTime.Now.Date;
            return Ok(db.UserTraining.Where(r => r.TrainingDate >= currentDate).ToList());
        }

        // GET: api/UserTrainings/5
        [ResponseType(typeof(List<UserTraining>))]
        public IHttpActionResult GetUserTraining(int userId)
        {
            return Ok(db.UserTraining.Where(t => t.UserId == userId).ToList());
        }

        [ResponseType(typeof(UserTraining))]
        public IHttpActionResult GetUserTraining(int userId, long date)
        {
            var currentDate = new DateTime(date).Date;
            return Ok(db.UserTraining.FirstOrDefault(t => t.UserId == userId && t.TrainingDate == currentDate));
        }

        [ResponseType(typeof(UserTraining))]
        public IHttpActionResult GetUserTraining(long date, long time)
        {
            var currentDate = new DateTime(date).Date;
            var currentTime = new TimeSpan(time);
            return Ok(db.UserTraining.FirstOrDefault(t => t.TrainingDate  == currentDate && t.TrainingTime == currentTime));
        }

        // PUT: api/UserTrainings/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUserTraining(int id, UserTraining userTraining)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != userTraining.Id)
            {
                return BadRequest();
            }

            db.Entry(userTraining).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserTrainingExists(id))
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

        // POST: api/UserTrainings
        [ResponseType(typeof(UserTraining))]
        public IHttpActionResult PostUserTraining(UserTraining userTraining)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.UserTraining.Add(userTraining);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = userTraining.Id }, userTraining);
        }

        // DELETE: api/UserTrainings/5
        [ResponseType(typeof(UserTraining))]
        public IHttpActionResult DeleteUserTraining(int id)
        {
            UserTraining userTraining = db.UserTraining.Find(id);
            if (userTraining == null)
            {
                return NotFound();
            }

            db.UserTraining.Remove(userTraining);
            db.SaveChanges();

            return Ok(userTraining);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserTrainingExists(int id)
        {
            return db.UserTraining.Count(e => e.Id == id) > 0;
        }
    }
}