using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Facturacion.Entidades;
using Facturacion.Servicios.Interfaces;

namespace Facturacion.Web.Controllers
{
    public class HomeController : Base
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        } 
    }
}