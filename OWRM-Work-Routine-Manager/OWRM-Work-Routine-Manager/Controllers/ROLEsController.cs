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
using OWRM_Work_Routine_Manager.Models;

namespace OWRM_Work_Routine_Manager.Controllers
{
    public class ROLEsController : ApiController
    {
        private OWRModels db = new OWRModels();

        // GET: api/ROLEs
        public IQueryable<ROLE> GetROLE()
        {
            return db.ROLE;
        }

        // GET: api/ROLEs/5
        [ResponseType(typeof(ROLE))]
        public IHttpActionResult GetROLE(int id)
        {
            ROLE rOLE = db.ROLE.Find(id);
            if (rOLE == null)
            {
                return NotFound();
            }

            return Ok(rOLE);
        }

        // PUT: api/ROLEs/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutROLE(int id, ROLE rOLE)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != rOLE.ID_ROLE)
            {
                return BadRequest();
            }

            db.Entry(rOLE).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ROLEExists(id))
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

        // POST: api/ROLEs
        [ResponseType(typeof(ROLE))]
        public IHttpActionResult PostROLE(ROLE rOLE)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ROLE.Add(rOLE);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = rOLE.ID_ROLE }, rOLE);
        }

        // DELETE: api/ROLEs/5
        [ResponseType(typeof(ROLE))]
        public IHttpActionResult DeleteROLE(int id)
        {
            ROLE rOLE = db.ROLE.Find(id);
            if (rOLE == null)
            {
                return NotFound();
            }

            db.ROLE.Remove(rOLE);
            db.SaveChanges();

            return Ok(rOLE);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ROLEExists(int id)
        {
            return db.ROLE.Count(e => e.ID_ROLE == id) > 0;
        }
    }
}