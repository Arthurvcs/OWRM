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
using WebServiceRestful.Models;

namespace WebServiceRestful.Controllers
{
    [Route("Tarefa")]
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

        [HttpPost,Route("AddTarefa")]
        public IHttpActionResult AddTarefa([FromBody] ParamsTarefa paramsTarefa )
        {

            DateTime data = DateTime.Now;
            TAREFA tarefa = new TAREFA();
            string retorno = "";

            tarefa.DESCRICAO = paramsTarefa.descricao;
            tarefa.TITULO = paramsTarefa.titulo;

            try
            {
                db.TAREFA.Add(tarefa);
                db.SaveChanges();
                retorno = "Tarefa cadastrada com sucesso !";
            }
            catch (Exception)
            {
                retorno = "Ops, algo deu errado ao salvar sua tarefa !";
            }

            return Json(retorno);
        }

        [HttpPost, Route("IniciarTarefa")]
        public IHttpActionResult IniciarTarefa([FromBody] ParamsTarefa paramsTarefa)
        {

            TAREFA item = db.TAREFA.First(i => i.ID_TAREFA == paramsTarefa.idTarefa);
            item.DATA_INICIO = DateTime.Now;
            string retorno = "";

            try
            {
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
                retorno = "Tarefa iniciada cadastrada com sucesso !";

            }
            catch (Exception)
            {
                retorno = "Ops, algo deu errado ao iniciar sua tarefa !";
            }
            return Json(retorno);
        }

        [HttpPost,Route("SalvarTarefa")]
        public IHttpActionResult SalvarTarefa( [FromBody] ParamsTarefa paramsTarefa)
        {

            TAREFA item = db.TAREFA.First(i => i.ID_TAREFA == paramsTarefa.idTarefa);
            item.DESCRICAO = paramsTarefa.descricao;
            item.TITULO = paramsTarefa.titulo;
            string retorno = "";

            try
            {
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
                retorno = "Tarefa alterada com sucesso !";

            }
            catch (Exception)
            {
                retorno = "Ops, algo deu errado ao iniciar sua tarefa !";
            }
            return Json(retorno);
        }

        [HttpPost, Route("PararTarefa")]
        public IHttpActionResult PararTarefa([FromBody] ParamsTarefa paramsTarefa)
        {
            TAREFA item = db.TAREFA.First(i => i.ID_TAREFA == paramsTarefa.idTarefa);
            item.DATA_FIM = DateTime.Now;
            string retorno = "";

            try
            {
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
                retorno = "Tarefa finalizada com sucesso !";

            }
            catch (Exception)
            {
                retorno = "Ops, algo deu errado ao finzalizar sua tarefa !";
            }
            return Json(retorno);

        }

        [HttpPost, Route("ExcluirTarefa")]
        public IHttpActionResult ExcluirTarefa([FromBody] ParamsTarefa paramsTarefa)
        {
            TAREFA item = db.TAREFA.First(i => i.ID_TAREFA == paramsTarefa.idTarefa);
            string retorno = "";

            try
            {
                db.TAREFA.Remove(item);
                db.SaveChanges();
                retorno = "Tarefa excluida com sucesso !";

            }
            catch (Exception)
            {
                retorno = "Ops, algo deu errado ao excluir sua tarefa !";
            }
            return Json(retorno);
        }

        [HttpGet]
        public IHttpActionResult ListarTarefas()
        {
            List<TarefasViewModel> query =
                (from T in db.TAREFA
                 select new TarefasViewModel()
                 {
                     DATA_INICIO = T.DATA_INICIO,
                     DATA_FIM = T.DATA_FIM,
                     ID_TAREFA = T.ID_TAREFA,
                     DESCRICAO = T.DESCRICAO,
                     TITULO = T.TITULO

                 }).ToList();

            return Json(query);
        }

        [HttpGet]
        public IHttpActionResult ListarTarefas(int id_tarefa)
        {
            var query =
                (from T in db.TAREFA
                 where T.ID_TAREFA == id_tarefa
                 select new
                 {
                     T.ID_TAREFA,
                     T.DESCRICAO,
                     T.TITULO

                 }).First();

            return Json(query);
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