using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Facturacion.Web.Controllers
{
    public class VentasController : Base
    {
        // GET: Ventas
        public ActionResult Index()
        {
            return View();
        }
    }
}