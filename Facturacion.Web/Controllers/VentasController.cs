using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Facturacion.Servicios.Interfaces;
namespace Facturacion.Web.Controllers
{
    public class VentasController : Base
    {
        private string _ListaSerie;
        public VentasController()
        {
            _ListaSerie=ServiceManager<FacturaSvc>.Provider.ListarSerie();
        }
        // GET: Ventas
        public ActionResult Index()
        {
            ViewBag.ListaSerie = _ListaSerie;
            return View();
        }
    }
}