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
    public class TAREFAsController : ApiController
    {
        private OWRModels db = new OWRModels();

        // GET: api/TAREFAs
        public IQueryable<TAREFA> GetTAREFA()
        {
            return db.TAREFA;
        }

        // GET: api/TAREFAs/5
        [ResponseType(typeof(TAREFA))]
        public IHttpActionResult GetTAREFA(int id)
        {
            TAREFA tAREFA = db.TAREFA.Find(id);
            if (tAREFA == null)
            {
                return NotFound();
            }

            return Ok(tAREFA);
        }

        // PUT: api/TAREFAs/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTAREFA(int id, TAREFA tAREFA)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tAREFA.ID_TAREFA)
            {
                return BadRequest();
            }

            db.Entry(tAREFA).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TAREFAExists(id))
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

        // POST: api/TAREFAs
        [ResponseType(typeof(TAREFA))]
        public IHttpActionResult PostTAREFA(TAREFA tAREFA)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.TAREFA.Add(tAREFA);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tAREFA.ID_TAREFA }, tAREFA);
        }

        // DELETE: api/TAREFAs/5
        [ResponseType(typeof(TAREFA))]
        public IHttpActionResult DeleteTAREFA(int id)
        {
            TAREFA tAREFA = db.TAREFA.Find(id);
            if (tAREFA == null)
            {
                return NotFound();
            }

            db.TAREFA.Remove(tAREFA);
            db.SaveChanges();

            return Ok(tAREFA);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TAREFAExists(int id)
        {
            return db.TAREFA.Count(e => e.ID_TAREFA == id) > 0;
        }
    }
}