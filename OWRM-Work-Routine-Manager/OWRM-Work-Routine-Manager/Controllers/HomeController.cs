using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using OWRM_Work_Routine_Manager.Models;


namespace OWRM_Work_Routine_Manager.Controllers
{
    public class HomeController : Controller
    {

        [Authorize]
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Login()
        {
            return View();
        }

        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            Response.Cookies["Usuario"].Expires = DateTime.Now.AddDays(-1);

            return RedirectToAction("Login", "Home");
        }

        [HttpPost]
        public ActionResult Login(USUARIO model, string returnUrl)
        {
            OWRModels db = new OWRModels();
            var dataItem = db.USUARIO.Where(x => x.LOGIN == model.LOGIN && x.SENHA == model.SENHA).FirstOrDefault();
            if (dataItem != null)
            {
                HttpCookie cookie = new HttpCookie("Usuario");
                cookie.Value = dataItem.ID_USUARIO.ToString();
                DateTime dtNow = DateTime.Now;

                TimeSpan tsMinute = new TimeSpan(0, 0, 120, 0);

                cookie.Expires = dtNow + tsMinute;
                Response.Cookies.Add(cookie);

                ViewBag.nome = dataItem.NOME;

                var role = (from R in db.ROLE
                            where R.ID_ROLE == dataItem.ID_ROLE
                            select R.DESCRICAO).FirstOrDefault();

                FormsAuthentication.SetAuthCookie(role.ToString(), false);
                if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/") && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    FormsAuthentication.SetAuthCookie(role.ToString(), false);
                    return RedirectToAction("Index");
                }
            }
            else
            {
                ModelState.AddModelError("", "Usuário ou senha inválidos");
                return View();
            }
        }
        [Authorize(Users = "ADM,USU")]
        public ActionResult Sobre()
        {
            return View();
        }
        [Authorize(Users = "ADM,USU")]
        public ActionResult NoData()
        {
            return View();
        }

    }
}