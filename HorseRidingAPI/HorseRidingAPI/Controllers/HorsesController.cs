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
    public class HorsesController : ApiController
    {
        private db_a743eb_horseridingEntities db = new db_a743eb_horseridingEntities();

        public IQueryable<Horse> GetHorses()
        {
            return db.Horse;
        }

        [Route("api/Horses/{id}")]
        [HttpGet]
        public IHttpActionResult GetHorseById(int id)
        {
            return Ok(db.Horse.FirstOrDefault(r => r.Id == id));
        }

        public IHttpActionResult GetHorses(int trainingTypeId)
        {
            return Ok(db.Horse.Where(r => r.TrainingTypeId == trainingTypeId).ToList());
        }

        public IHttpActionResult GetHorseIsFree(int horseId, long date, long time)
        {
            bool isFree = true;
            var currentDate = new DateTime(date).Date;
            var currentTime = new TimeSpan(time);
            var training = db.UserTraining.Where(t => t.TrainingDate == currentDate && t.HorseId == horseId).FirstOrDefault(t => t.TrainingTime == currentTime);
            if (training != null)
                isFree = false;
            return Ok(isFree);
        }

        [Route("api/PutHorses/{id}")]
        [ResponseType(typeof(void))]
        [HttpPut]
        public IHttpActionResult PutHorse(int id, Horse horse)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != horse.Id)
            {
                return BadRequest();
            }

            db.Entry(horse).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HorseExists(id))
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
        [Route("api/PostHorses")]
        [HttpPost]
        public IHttpActionResult PostHorse(Horse horse)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Horse.Add(horse);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = horse.Id }, horse);
        }

        // DELETE: api/Trainers/5
        [Route("api/DeleteHorse/{id}")]
        [HttpDelete]
        public IHttpActionResult DeleteHorse(int id)
        {
            Horse horse = db.Horse.Find(id);
            if (horse == null)
            {
                return NotFound();
            }

            db.Horse.Remove(horse);
            db.SaveChanges();

            return Ok(horse);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool HorseExists(int id)
        {
            return db.Horse.Count(e => e.Id == id) > 0;
        }
    }
}