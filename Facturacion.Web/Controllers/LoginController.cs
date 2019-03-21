using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Facturacion.Entidades;
using Facturacion.Servicios.Interfaces;
namespace Facturacion.Web.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            var user = MetodosUtiles.ObtenerUsuario();
            if (user != null)
            {
                MetodosUtiles.AgregarUsuariosSession(user);
                return RedirectToAction("Index", "Home");
            } 
            return View();
        }
        public JsonResult ValidarUsuarioSistema(string u, string pw)
        {
            UsuarioBe usuario = new UsuarioBe();
            ErrorBe ObjError = new ErrorBe();
            if (!string.IsNullOrEmpty(pw))
                pw = new Utilitarios.Cryptography().Encriptar(pw);
            usuario = ServiceManager<UsuarioSvc>.Provider.ValidarUsuario(u, pw);
            if (usuario == null)
            {
                ObjError.Mensaje = "Usuario o Contraseña Inválidos";
                ObjError.Error = 1;
            }
            else {
                ObjError.Mensaje = "Usuario Válido";
                ObjError.Error = 0; 
                MetodosUtiles.AgregarUsuariosSession(usuario);
            }
            JsonResult json = Json(new { d = ObjError }, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }
        public ActionResult Salir() {
            Session.Clear();
            return RedirectToAction("Index", "Login"); 
        }
    }
}