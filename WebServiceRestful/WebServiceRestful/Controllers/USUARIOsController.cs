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
   
    [Route("Usuario")]
    public class USUARIOsController : ApiController
    {
        private OWRModels db = new OWRModels();

        // GET: api/USUARIOs
        public IQueryable<USUARIO> GetUSUARIO()
        {
            return db.USUARIO;
        }

        // GET: api/USUARIOs/5
        [ResponseType(typeof(USUARIO))]
        public IHttpActionResult GetUSUARIO(int id)
        {
            USUARIO uSUARIO = db.USUARIO.Find(id);
            if (uSUARIO == null)
            {
                return NotFound();
            }

            return Ok(uSUARIO);
        }

        // PUT: api/USUARIOs/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUSUARIO(int id, USUARIO uSUARIO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != uSUARIO.ID_USUARIO)
            {
                return BadRequest();
            }

            db.Entry(uSUARIO).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!USUARIOExists(id))
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

        // POST: api/USUARIOs
        [ResponseType(typeof(USUARIO))]
        public IHttpActionResult PostUSUARIO(USUARIO uSUARIO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.USUARIO.Add(uSUARIO);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = uSUARIO.ID_USUARIO }, uSUARIO);
        }

        // DELETE: api/USUARIOs/5
        [ResponseType(typeof(USUARIO))]
        public IHttpActionResult DeleteUSUARIO(int id)
        {
            USUARIO uSUARIO = db.USUARIO.Find(id);
            if (uSUARIO == null)
            {
                return NotFound();
            }

            db.USUARIO.Remove(uSUARIO);
            db.SaveChanges();

            return Ok(uSUARIO);
        }

        [HttpPost,Route("AddUsuario")]
        public IHttpActionResult AddUsuario([FromBody] ParamsUsuario paramsUsuario)
        {
            int id_role = 2;

            USUARIO usu = new USUARIO();
            string retorno = "";

            usu.NOME = paramsUsuario.nome;
            usu.LOGIN = paramsUsuario.login;
            usu.SENHA = paramsUsuario.senha;
            usu.MATRICULA = paramsUsuario.matricula;
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

            return Json(retorno);
        }

        [HttpGet,Route("ListarUsuarios")]
        public IHttpActionResult ListarUsuarios()
        {
            var query =
                (from U in db.USUARIO
                 join R in db.ROLE on U.ID_ROLE equals R.ID_ROLE
                 select new UsuarioViewModel
                 {
                     NOME = U.NOME,
                     LOGIN = U.LOGIN,
                     ID_USUARIO = U.ID_USUARIO,
                     MATRICULA = U.MATRICULA,
                     ROLE = R.DESCRICAO

                 }).ToList();

            return Json(query);
        }

        [HttpPost,Route("DeletarUsuario")]
        public IHttpActionResult DeletarUsuario([FromBody] ParamsUsuario paramsUsuario)
        {
            USUARIO item = db.USUARIO.First(i => i.ID_USUARIO == paramsUsuario.idUsuario);
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
            return Json(retorno);
        }

        [HttpPost,Route("EditarUsuario")]
        public IHttpActionResult EditarUsuario([FromBody] ParamsUsuario paramsUsuario)
        {
            int id_role = 2;

            USUARIO item = db.USUARIO.First(i => i.ID_USUARIO == paramsUsuario.idUsuario);
            item.NOME = paramsUsuario.nome;
            item.LOGIN = paramsUsuario.login;
            if (!string.IsNullOrEmpty(paramsUsuario.senha))
            {
                item.SENHA = paramsUsuario.senha;
            }
            item.MATRICULA = paramsUsuario.matricula;
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
            return Json(retorno);
        }

        [HttpGet,Route("GetUsuario")]
        public IHttpActionResult GetUsuario(int id_usuario)
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

        private bool USUARIOExists(int id)
        {
            return db.USUARIO.Count(e => e.ID_USUARIO == id) > 0;
        }
    }
}