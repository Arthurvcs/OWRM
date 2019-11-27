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
using System.Web.Http.Results;
using OWRM_Work_Routine_Manager.Models;
using WebServiceRestful.Models;

namespace WebServiceRestful.Controllers
{
    
    [Route("Nota")]
    public class NOTAsController : ApiController
    {
        private OWRModels db = new OWRModels();

        // GET: api/NOTAs
        public IQueryable<NOTA> GetNOTA()
        {
            return db.NOTA;
        }

        // GET: api/NOTAs/5
        [ResponseType(typeof(NOTA))]
        public IHttpActionResult GetNOTA(int id)
        {
            NOTA nOTA = db.NOTA.Find(id);
            if (nOTA == null)
            {
                return NotFound();
            }

            return Ok(nOTA);
        }

        // PUT: api/NOTAs/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutNOTA(int id, NOTA nOTA)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != nOTA.ID_NOTA)
            {
                return BadRequest();
            }

            db.Entry(nOTA).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NOTAExists(id))
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

        // POST: api/NOTAs
        [ResponseType(typeof(NOTA))]
        public IHttpActionResult PostNOTA(NOTA nOTA)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.NOTA.Add(nOTA);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = nOTA.ID_NOTA }, nOTA);
        }

        // DELETE: api/NOTAs/5
        [ResponseType(typeof(NOTA))]
        public IHttpActionResult DeleteNOTA(int id)
        {
            NOTA nOTA = db.NOTA.Find(id);
            if (nOTA == null)
            {
                return NotFound();
            }

            db.NOTA.Remove(nOTA);
            db.SaveChanges();

            return Ok(nOTA);

        }

        [HttpGet, Route("ListarNotas")]
        public IHttpActionResult ListarNotas()
        {
            List<NotasViewModel> query =
                (from N in db.NOTA
                 select new NotasViewModel()
                 {
                     DESCRICAO = N.DESCRICAO,
                     COR = N.COR,
                     DATA = N.DATA,
                     TITULO = N.TITULO,
                     ID_NOTA = N.ID_NOTA
                 }).ToList();

            return Json(query);
        }

        [HttpPost, Route("ListarNotas")]
        public IHttpActionResult ListarNotas([FromBody] ParamsNota paramsNota)
        {

            var query =
                (from T in db.NOTA
                 where T.ID_NOTA == paramsNota.idNota
                 && T.ID_USUARIO == paramsNota.u
                 select new
                 {
                     T.ID_NOTA,
                     T.DESCRICAO,
                     T.TITULO,
                     T.COR

                 }).First();

            return Json(query);
        }

        [HttpPost,Route("AddNota")]
        public IHttpActionResult AddNota([FromBody] ParamsNota paramsNota)
        {
            DateTime data = DateTime.Now;
            NOTA nota = new NOTA();
            string retorno = "";

            nota.TITULO = paramsNota.titulo;
            nota.DESCRICAO = paramsNota.descricao;
            nota.DATA = data;
            nota.COR = paramsNota.cor;
            nota.ID_USUARIO = paramsNota.u;

            try
            {
                db.NOTA.Add(nota);
                db.SaveChanges();
                retorno = "Nota cadastrada com sucesso !";
            }
            catch (Exception)
            {
                retorno = "Ops, algo deu errado ao salvar sua nota !";
            }

            return Json(retorno);
        }
        [HttpPost, Route("ExcluirNota")]
        public IHttpActionResult ExcluirNota([FromBody] ParamsNota paramsNota)
        {
            NOTA item = db.NOTA.First(i => i.ID_NOTA == paramsNota.idNota && i.ID_USUARIO == paramsNota.u);
            string retorno = "";

            try
            {
                db.NOTA.Remove(item);
                db.SaveChanges();
                retorno = "Nota excluida com sucesso !";

            }
            catch (Exception)
            {
                retorno = "Ops, algo deu errado ao excluir sua nota !";
            }
            return Json(retorno);
        }

        [HttpPost,Route("SalvarNota")]
        public IHttpActionResult SalvarNota([FromBody] ParamsNota paramsNota)
        {
            NOTA item = db.NOTA.First(i => i.ID_NOTA == paramsNota.idNota && i.ID_USUARIO == paramsNota.u);
            item.DESCRICAO = paramsNota.descricao;
            item.TITULO = paramsNota.titulo;
            item.COR = paramsNota.cor;
            string retorno = "";

            try
            {
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
                retorno = "Nota alterada com sucesso !";

            }
            catch (Exception)
            {
                retorno = "Ops, algo deu errado ao salvar sua nota!";
            }
            return Json(retorno);
        }

        [HttpGet,Route("ListarCores")]
        public IHttpActionResult ListarCores()
        {
            List<string> cores = new List<string>();
            cores.Add("#f3ef7e");
            cores.Add("#3495eb");
            cores.Add("#d44444");
            cores.Add("#46c768");
            cores.Add("#ffa600");
            cores.Add("#ff00fb");
            cores.Add("#1ab5db");

            return Json(cores);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool NOTAExists(int id)
        {
            return db.NOTA.Count(e => e.ID_NOTA == id) > 0;
        }
    }
}