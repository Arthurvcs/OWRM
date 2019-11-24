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
    public class PONTOsController : ApiController
    {
        private OWRModels db = new OWRModels();

        // GET: api/PONTOs
        public IQueryable<PONTO> GetPONTO()
        {
            return db.PONTO;
        }

        // GET: api/PONTOs/5
        [ResponseType(typeof(PONTO))]
        public IHttpActionResult GetPONTO(int id)
        {
            PONTO pONTO = db.PONTO.Find(id);
            if (pONTO == null)
            {
                return NotFound();
            }

            return Ok(pONTO);
        }

        // PUT: api/PONTOs/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPONTO(int id, PONTO pONTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pONTO.ID_PONTO)
            {
                return BadRequest();
            }

            db.Entry(pONTO).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PONTOExists(id))
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

        // POST: api/PONTOs
        [ResponseType(typeof(PONTO))]
        public IHttpActionResult PostPONTO(PONTO pONTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PONTO.Add(pONTO);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = pONTO.ID_PONTO }, pONTO);
        }

        // DELETE: api/PONTOs/5
        [ResponseType(typeof(PONTO))]
        public IHttpActionResult DeletePONTO(int id)
        {
            PONTO pONTO = db.PONTO.Find(id);
            if (pONTO == null)
            {
                return NotFound();
            }

            db.PONTO.Remove(pONTO);
            db.SaveChanges();

            return Ok(pONTO);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PONTOExists(int id)
        {
            return db.PONTO.Count(e => e.ID_PONTO == id) > 0;
        }
    }
}