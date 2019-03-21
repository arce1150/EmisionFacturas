using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Facturacion.Entidades;
using Facturacion.Servicios.Interfaces;
namespace Facturacion.Web.Controllers
{
    public class ProductoController : Base
    {
        private List<ParametroSistemaBe> _Parametro = null;
        public ProductoController()
        {
            _Parametro = new List<ParametroSistemaBe>();
            _Parametro = ServiceManager<ParametroSistemaSvc>.Provider.ListarParametros();
        }
        // GET: Producto
        public ActionResult Index()
        {
            return View();
        }
        public string ListarCategoria()
        {
            var Lista = ServiceManager<ProductoSvc>.Provider.ListarCategorias();
            return Lista;
        }
        public JsonResult ListarParametros()
        {
            var Lista = _Parametro;
            JsonResult json = Json(new { d = Lista }, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }
        public JsonResult InsertarProducto(ProductoBe p)
        {
            int FilasAfectadas = 0;
            UsuarioBe usuario = new UsuarioBe();
            usuario = MetodosUtiles.ObtenerUsuario();
            if (p.IdProducto == 0)
                p.UsuarioCrea = usuario.IdUsuario;
            else p.UsuarioModifica = usuario.IdUsuario;

            FilasAfectadas = ServiceManager<ProductoSvc>.Provider.InsertarProducto(p);
            JsonResult json = Json(new { d = FilasAfectadas }, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }
        public string PaginarProducto()
        { 
            var Lista = ServiceManager<ProductoSvc>.Provider.PaginarProductoV2();
            return Lista;
        }
    }
}