using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OWRM_Work_Routine_Manager.Models;

namespace OWRM_Work_Routine_Manager.Controllers
{
    public class NotasController : Controller
    {
        private OWRModels db = new OWRModels();
        private HttpCookie cookie;
        private int id_usuario;

        [Authorize(Users = "ADM,USU")]
        public ActionResult Index()
        {
            //IEnumerable<NotasViewModel> notasRegistradas = ListarNotas();
            ViewBag.cores = ListarCores();
            return View(ListarNotas());
        }

        [HttpGet]
        public JsonResult ListarNotas(int IdNota)
        {
            cookie = Request.Cookies["Usuario"];
            id_usuario = int.Parse(cookie.Value);

            var query =
                (from T in db.NOTA
                 where T.ID_NOTA == IdNota
                 && T.ID_USUARIO == id_usuario
                 select new
                 {
                     T.ID_NOTA,
                     T.DESCRICAO,
                     T.TITULO,
                     T.COR

                 }).First();

            return Json(query, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AddNota(string titulo, string descricao, string cor)
        {
            cookie = Request.Cookies["Usuario"];
            id_usuario = int.Parse(cookie.Value);

            DateTime data = DateTime.Now;
            NOTA nota = new NOTA();
            string retorno = "";

            nota.TITULO = titulo;
            nota.DESCRICAO = descricao;
            nota.DATA = data;
            nota.COR = cor;
            nota.ID_USUARIO = id_usuario;

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

            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult ExcluirNota(int idNota)
        {
            cookie = Request.Cookies["Usuario"];
            id_usuario = int.Parse(cookie.Value);

            NOTA item = db.NOTA.First(i => i.ID_NOTA == idNota && i.ID_USUARIO == id_usuario);
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
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SalvarNota(int idnota, string titulo, string descricao, string cor)
        {
            cookie = Request.Cookies["Usuario"];
            id_usuario = int.Parse(cookie.Value);

            NOTA item = db.NOTA.First(i => i.ID_NOTA == idnota && i.ID_USUARIO == id_usuario);
            item.DESCRICAO = descricao;
            item.TITULO = titulo;
            item.COR = cor;
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
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        private IEnumerable<NotasViewModel> ListarNotas()
        {
            cookie = Request.Cookies["Usuario"];
            id_usuario = int.Parse(cookie.Value);

            List<NotasViewModel> query =
                (from N in db.NOTA
                 where N.ID_USUARIO == id_usuario
                 select new NotasViewModel()
                 {
                     DESCRICAO = N.DESCRICAO,
                     COR = N.COR,
                     DATA = N.DATA,
                     TITULO = N.TITULO,
                     ID_NOTA = N.ID_NOTA
                 }).ToList();

            return query;
        }

        private List<string> ListarCores()
        {
            List<string> cores = new List<string>();
            cores.Add("#f3ef7e");
            cores.Add("#3495eb");
            cores.Add("#d44444");
            cores.Add("#46c768");
            cores.Add("#ffa600");
            cores.Add("#ff00fb");
            cores.Add("#1ab5db");

            return cores;
        }
    }
}