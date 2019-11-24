using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OWRM_Work_Routine_Manager.Models;
using System.Web.Mvc;
using System.Data.Entity;
using OWRM_Work_Routine_Manager.Models.ViewModels;

namespace OWRM_Work_Routine_Manager
{
    public class UsuarioController : Controller
    {
        // GET: Usuario
        private OWRModels db = new OWRModels(); //Criando uma váriavel de acesso as entidades

        [Authorize(Users = "ADM")]
        public ActionResult CadastroUsuario()
        {
            return View(ListarUsuarios());

        }

        [HttpPost]
        public ActionResult AddUsuario(string nome, string login,string senha, string matricula, int id_role = 2)
        {
            USUARIO usu = new USUARIO();
            string retorno = "";

            usu.NOME = nome;
            usu.LOGIN = login;
            usu.SENHA = senha;
            usu.MATRICULA = matricula;
            usu.ID_ROLE = id_role;

            try
            {
                db.USUARIO.Add(usu);
                db.SaveChanges();
                retorno = "Usuário cadastrado com sucesso !";
            }
            catch (Exception)
            {
                retorno = "Ops, algo deu errado ao registrar um novo usuário";
            }

            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public IEnumerable<UsuarioViewModel> ListarUsuarios()
        {
            var query =
                (from U in db.USUARIO
                 join R in db.ROLE on U.ID_ROLE equals R.ID_ROLE
                 select new UsuarioViewModel
                 {
                    NOME = U.NOME,
                    LOGIN= U.LOGIN,                 
                    ID_USUARIO = U.ID_USUARIO,
                    MATRICULA = U.MATRICULA,
                    ROLE = R.DESCRICAO

                 }).ToList();

            return query;
        }

        [HttpPost]
        public ActionResult DeletarUsuario(int idUsuario)
        {
            USUARIO item = db.USUARIO.First(i => i.ID_USUARIO == idUsuario);
            string retorno = "";

            try
            {
                db.USUARIO.Remove(item);
                db.SaveChanges();
                retorno = "Usuário excluido da base com sucesso !";

            }
            catch (Exception)
            {
                retorno = "Ops, algo deu errado ao deletar o usuário !";
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult EditarUsuario(int Id_usuario, string nome, string login, string senha, string matricula, int id_role = 2)
        {
            USUARIO item = db.USUARIO.First(i => i.ID_USUARIO == Id_usuario);
            item.NOME = nome;
            item.LOGIN = login;
            if(!string.IsNullOrEmpty(senha))
            {
                item.SENHA = senha;
            }
            item.MATRICULA = matricula;
            item.ID_ROLE = id_role;
            string retorno = "";

            try
            {
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
                retorno = "Usuário atualizado com sucesso !";

            }
            catch (Exception)
            {
                retorno = "Ops, algo deu errado ao editar os dados do usuário!";
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetUsuario(int id_usuario)
        {
            var query =
                (from U in db.USUARIO
                 where U.ID_USUARIO == id_usuario
                 select new
                 {
                     U.NOME,
                     U.LOGIN,
                     U.MATRICULA,

                 }).First();

            return Json(query, JsonRequestBehavior.AllowGet);
        }
    }
}