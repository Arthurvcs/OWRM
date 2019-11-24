using OWRM_Work_Routine_Manager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace OWRM_Work_Routine_Manager.Controllers
{
    public class TarefaController : Controller
    {
        private OWRModels db = new OWRModels();
        private HttpCookie cookie;
        private int id_usuario;

        [Authorize(Users = "ADM,USU")]
        public ActionResult Index()
        {
            IEnumerable<TarefasViewModel> tarefasRegistradas = ListarTarefas().OrderBy(m => m.DATA_INICIO);
            return View(tarefasRegistradas);
        }
        [HttpGet]
        public JsonResult ListarTarefas(int id_tarefa)
        {
            cookie = Request.Cookies["Usuario"];
            id_usuario = int.Parse(cookie.Value);

            var query =
                (from T in db.TAREFA
                 where T.ID_TAREFA == id_tarefa
                 && T.ID_USUARIO == id_usuario
                 select new
                 {
                     T.ID_TAREFA,
                     T.DESCRICAO,
                     T.TITULO

                 }).First();

            return Json(query, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AddTarefa(string titulo, string descricao)
        {
            DateTime data = DateTime.Now;
            TAREFA tarefa = new TAREFA();
            string retorno = "";

            cookie = Request.Cookies["Usuario"];
            id_usuario = int.Parse(cookie.Value);

            tarefa.DESCRICAO = descricao;
            tarefa.TITULO = titulo;
            tarefa.ID_USUARIO = id_usuario;

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

            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult IniciarTarefa(int idTarefa)
        {
            cookie = Request.Cookies["Usuario"];
            id_usuario = int.Parse(cookie.Value);

            TAREFA item = db.TAREFA.First(i => i.ID_TAREFA == idTarefa && i.ID_USUARIO == id_usuario);
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
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult SalvarTarefa(int idTarefa, string titulo, string descricao)
        {
            cookie = Request.Cookies["Usuario"];
            id_usuario = int.Parse(cookie.Value); 

            TAREFA item = db.TAREFA.First(i => i.ID_TAREFA == idTarefa && i.ID_USUARIO == id_usuario);
            item.DESCRICAO = descricao;
            item.TITULO = titulo;
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
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult PararTarefa(int idTarefa)
        {
            cookie = Request.Cookies["Usuario"];
            id_usuario = int.Parse(cookie.Value);

            TAREFA item = db.TAREFA.First(i => i.ID_TAREFA == idTarefa && i.ID_USUARIO == id_usuario);
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
            return Json(retorno, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public ActionResult ExcluirTarefa(int idTarefa)
        {
            cookie = Request.Cookies["Usuario"];
            id_usuario = int.Parse(cookie.Value);

            TAREFA item = db.TAREFA.First(i => i.ID_TAREFA == idTarefa && i.ID_USUARIO == id_usuario);
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
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        private IEnumerable<TarefasViewModel> ListarTarefas()
        {
            cookie = Request.Cookies["Usuario"];
            id_usuario = int.Parse(cookie.Value);

            List<TarefasViewModel> query =
                (from T in db.TAREFA
                 where T.ID_USUARIO == id_usuario
                 select new TarefasViewModel()
                 {
                     DATA_INICIO = T.DATA_INICIO,
                     DATA_FIM = T.DATA_FIM,
                     ID_TAREFA = T.ID_TAREFA,
                     DESCRICAO = T.DESCRICAO,
                     TITULO = T.TITULO

                 }).ToList();

            return query;
        }
    }
}