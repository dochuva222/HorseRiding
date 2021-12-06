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
    public class TrainersController : ApiController
    {
        private db_a743eb_horseridingEntities db = new db_a743eb_horseridingEntities();

        [HttpGet]
        public IQueryable<Trainer> GetTrainers()
        {
            return db.Trainer;
        }

        [HttpGet]
        [Route("api/Trainers/{id}")]
        [ResponseType(typeof(Trainer))]
        public IHttpActionResult GetTrainerById(int id)
        {
            return Ok(db.Trainer.FirstOrDefault(r => r.Id == id));
        }

        [HttpGet]
        [ResponseType(typeof(Trainer))]
        public IHttpActionResult GetTrainerByPhone(string phone)
        {
            return Ok(db.Trainer.FirstOrDefault(u => u.Phone == phone));
        }

        [HttpGet]
        [ResponseType(typeof(List<Trainer>))]
        public IHttpActionResult GetTrainersOfCurrentTrainingType(int trainingTypeId)
        {
            return Ok(db.Trainer.Where(r => r.TrainingTypeId == trainingTypeId).ToList());
        }

        public IHttpActionResult GetTrainerIsFree(int trainerId, long date, long time)
        {
            bool isFree = true;
            var currentDate = new DateTime(date).Date;
            var currentTime = new TimeSpan(time);
            var training = db.UserTraining.Where(t => t.TrainingDate == currentDate && t.HorseId == trainerId).FirstOrDefault(t => t.TrainingTime == currentTime);
            if (training != null)
                isFree = false;
            return Ok(isFree);
        }


        // PUT: api/Trainers/5
        [Route("api/PutTrainers/{id}")]
        [ResponseType(typeof(void))]
        [HttpPut]
        public IHttpActionResult PutTrainer(int id, Trainer trainer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != trainer.Id)
            {
                return BadRequest();
            }

            db.Entry(trainer).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TrainerExists(id))
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

        // POST: api/Trainers
        [ResponseType(typeof(Trainer))]
        [HttpPost]
        public IHttpActionResult PostTrainer(Trainer trainer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Trainer.Add(trainer);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = trainer.Id }, trainer);
        }

        // DELETE: api/Trainers/5
        [Route("api/DeleteTrainer/{id}")]
        [ResponseType(typeof(Trainer))]
        [HttpDelete]
        public IHttpActionResult DeleteTrainer(int id)
        {
            Trainer trainer = db.Trainer.Find(id);
            if (trainer == null)
            {
                return NotFound();
            }

            db.Trainer.Remove(trainer);
            db.SaveChanges();

            return Ok(trainer);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TrainerExists(int id)
        {
            return db.Trainer.Count(e => e.Id == id) > 0;
        }
    }
}