using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using OWRM_Work_Routine_Manager.Models;

namespace WebServiceRestful.Controllers
{
    [Route("ControlePonto")]
    public class ControlePontoController : ApiController
    {
        private OWRModels db = new OWRModels();

        public class ParamsPonto
        {
            public DateTime? dataIni { get; set; }
            public DateTime? dataFim { get; set; }
        }

        // GET: api/PONTOs
        public IQueryable<PONTO> GetPONTO()
        {
            return db.PONTO;
        }

        [HttpPost, Route("IndexFiltro")]
        public IHttpActionResult IndexFiltro([FromBody] ParamsPonto paramsPonto )
        { 

            IEnumerable<PontoViewModel> query =
              (from P in db.PONTO
               where P.PONTO_ENTRADA == paramsPonto.dataIni && P.PONTO_SAIDA == paramsPonto.dataFim
               select new PontoViewModel()
               {
                   PONTO_ENTRADA = P.PONTO_ENTRADA,
                   PONTO_ENTRADA_INTERVALO = P.PONTO_ENTRADA_INTERVALO,
                   PONTO_SAIDA_INTERVALO = P.PONTO_SAIDA_INTERVALO,
                   PONTO_SAIDA = P.PONTO_SAIDA,
               }).ToList();

            return Json(query);
        }

        [HttpGet,Route("ListarPontos")]
        public IHttpActionResult ListarPontos()
        {
            List<PontoViewModel> query =
                (from P in db.PONTO
                 select new PontoViewModel()
                 {
                     PONTO_ENTRADA = P.PONTO_ENTRADA,
                     PONTO_ENTRADA_INTERVALO = P.PONTO_ENTRADA_INTERVALO,
                     PONTO_SAIDA_INTERVALO = P.PONTO_SAIDA_INTERVALO,
                     PONTO_SAIDA = P.PONTO_SAIDA,
                 }).ToList();

            return Json(query);
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

        [HttpPost, Route("ListarPontosFiltro")]
        public IHttpActionResult ListarPontosFiltro([FromBody] ParamsPonto paramsPonto)
        {


            DateTime dataInicio = Convert.ToDateTime(paramsPonto.dataIni);
            DateTime dataFinal = Convert.ToDateTime(paramsPonto.dataFim);


            IEnumerable < PontoViewModel > query =
                (from P in db.PONTO
                 where P.PONTO_ENTRADA == dataInicio && P.PONTO_SAIDA == dataFinal
                 select new PontoViewModel()
                 {
                     PONTO_ENTRADA = P.PONTO_ENTRADA,
                     PONTO_ENTRADA_INTERVALO = P.PONTO_ENTRADA_INTERVALO,
                     PONTO_SAIDA_INTERVALO = P.PONTO_SAIDA_INTERVALO,
                     PONTO_SAIDA = P.PONTO_SAIDA,
                 }).ToList();

            return Json(query);
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