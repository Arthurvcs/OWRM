using OWRM_Work_Routine_Manager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OWRM_Work_Routine_Manager.Controllers
{
    public class ControlePontoController : Controller
    {
        private OWRModels db = new OWRModels();
        private HttpCookie cookie;
        private int id_usuario;

        [Authorize(Users = "ADM,USU")]
        public ActionResult Index()
        {
            return View(ListarPontos());
        }

        [HttpPost]
        public ActionResult FIltrado(DateTime? datepickerIni, DateTime? datepickerFim)
        {
            cookie = Request.Cookies["Usuario"];
            id_usuario = int.Parse(cookie.Value);

            IEnumerable<PontoViewModel> query =
              (from P in db.PONTO
               where (P.PONTO_ENTRADA >= datepickerIni && P.PONTO_ENTRADA <= datepickerFim) && (P.PONTO_SAIDA >= datepickerIni && P.PONTO_SAIDA <= datepickerFim)
               && P.ID_USUARIO == id_usuario
               select new PontoViewModel()
               {
                   PONTO_ENTRADA = P.PONTO_ENTRADA,
                   PONTO_ENTRADA_INTERVALO = P.PONTO_ENTRADA_INTERVALO,
                   PONTO_SAIDA_INTERVALO = P.PONTO_SAIDA_INTERVALO,
                   PONTO_SAIDA = P.PONTO_SAIDA,
               }).ToList();

            return View("Index", query);
        }

        private IEnumerable<PontoViewModel> ListarPontos()
        {
            cookie = Request.Cookies["Usuario"];
            id_usuario = int.Parse(cookie.Value);

            List<PontoViewModel> query =
                (from P in db.PONTO
                 where P.ID_USUARIO == id_usuario
                 select new PontoViewModel()
                 {
                     PONTO_ENTRADA = P.PONTO_ENTRADA,
                     PONTO_ENTRADA_INTERVALO = P.PONTO_ENTRADA_INTERVALO,
                     PONTO_SAIDA_INTERVALO = P.PONTO_SAIDA_INTERVALO,
                     PONTO_SAIDA = P.PONTO_SAIDA,
                 }).ToList();

            return query;
        }
    }
}