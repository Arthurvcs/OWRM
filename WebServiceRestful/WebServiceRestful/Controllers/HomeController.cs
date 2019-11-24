using OWRM_Work_Routine_Manager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Cors;
using System.Web.Mvc;
using System.Web.Security;

namespace WebServiceRestful.Controllers
{
   
    public class HomeController : Controller
    {

        [HttpPost]
        public ActionResult Login(USUARIO model, string returnUrl)
        {
            OWRModels db = new OWRModels();
            var dataItem = db.USUARIO.Where(x => x.LOGIN == model.LOGIN && x.SENHA == model.SENHA).FirstOrDefault();
            if (dataItem != null)
            {
                FormsAuthentication.SetAuthCookie(dataItem.LOGIN, false);
                if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                         && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    FormsAuthentication.SetAuthCookie(dataItem.NOME.ToString(), false);
                    return RedirectToAction("Index");

                }
            }
            else
            {
                ModelState.AddModelError("", "Usuário ou senha inválidos");
                return View();
            }
        }
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }
    }
}
