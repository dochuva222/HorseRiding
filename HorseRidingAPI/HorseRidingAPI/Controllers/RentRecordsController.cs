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
    public class RentRecordsController : ApiController
    {
        private db_a743eb_horseridingEntities db = new db_a743eb_horseridingEntities();

        // GET: api/RentRecords
        public IQueryable<RentRecord> GetAllRentRecord()
        {
            return db.RentRecord;
        }

        [Route("api/ActualRentRecords")]
        public IHttpActionResult GetActualUserRentRecord()
        {
            var currentDate = DateTime.Now.Date;
            return Ok(db.RentRecord.Where(r => r.RentDate >= currentDate).ToList());
        }

        [Route("api/RentTrainerIsFree")]
        public IHttpActionResult GetIsRentRecordTrainerIsFree(int id, long date, long time)
        {
            var currentDate = new DateTime(date).Date;
            var currentTime = new TimeSpan(time);
            var rent = db.RentRecord.FirstOrDefault(r => r.RentDate == currentDate && r.RentTime == currentTime && r.TrainerId == id);
            if (rent == null)
                return Ok(true);
            else
                return Ok(false);
        }

        // GET: api/RentRecords/5
        [ResponseType(typeof(List<RentRecord>))]
        public IHttpActionResult GetUserRentRecord(int userId)
        {
            return Ok(db.RentRecord.Where(r => r.UserId == userId).ToList());
        }

        [ResponseType(typeof(RentRecord))]
        public IHttpActionResult GetCurrentDateRentRecord(int userId, long date)
        {
            var currentDate = new DateTime(date).Date;
            return Ok(db.RentRecord.FirstOrDefault(r => r.UserId == userId && r.RentDate == currentDate));
        }

        [ResponseType(typeof(RentRecord))]
        public IHttpActionResult GetCurrentDateRentRecord(long date, long time)
        {
            var currentDate = new DateTime(date).Date;
            var currentTime = new TimeSpan(time);
            return Ok(db.RentRecord.FirstOrDefault(r => r.RentDate == currentDate && r.RentTime == currentTime));
        }

        // PUT: api/RentRecords/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutRentRecord(int id, RentRecord rentRecord)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != rentRecord.Id)
            {
                return BadRequest();
            }

            db.Entry(rentRecord).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RentRecordExists(id))
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

        // POST: api/RentRecords
        [ResponseType(typeof(RentRecord))]
        public IHttpActionResult PostRentRecord(RentRecord rentRecord)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.RentRecord.Add(rentRecord);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = rentRecord.Id }, rentRecord);
        }

        // DELETE: api/RentRecords/5
        [ResponseType(typeof(RentRecord))]
        public IHttpActionResult DeleteRentRecord(int id)
        {
            RentRecord rentRecord = db.RentRecord.Find(id);
            if (rentRecord == null)
            {
                return NotFound();
            }

            db.RentRecord.Remove(rentRecord);
            db.SaveChanges();

            return Ok(rentRecord);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RentRecordExists(int id)
        {
            return db.RentRecord.Count(e => e.Id == id) > 0;
        }
    }
}