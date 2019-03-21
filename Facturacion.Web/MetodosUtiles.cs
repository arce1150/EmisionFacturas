using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Facturacion.Entidades;
namespace Facturacion.Web
{
    public class MetodosUtiles
    {
        public static UsuarioBe ObtenerUsuario()
        {
            var user = HttpContext.Current.Session["LoginUsuario"];
            if (user == null) return null;

            return (UsuarioBe)user;
        }
         
        public static void AgregarUsuariosSession(UsuarioBe usuario)
        {
            HttpContext.Current.Session.Add("LoginUsuario", usuario);
        } 
    }
}