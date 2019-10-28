using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
<<<<<<< HEAD
using Facturacion.Entidades;
=======
>>>>>>> 1d36f34b2da68da34bccc406f3d97fbddfd4d44e
using Facturacion.Servicios.Interfaces;
namespace Facturacion.Web.Controllers
{
    public class VentasController : Base
    {
        private string _ListaSerie;
<<<<<<< HEAD
        private List<ParametroSistemaBe> _Parametros;
        public VentasController()
        {
            UsuarioBe usuario = new UsuarioBe();
            usuario=MetodosUtiles.ObtenerUsuario();
            if (usuario == null)
            {
                RedirectToAction("Index", "Login");
            }
            else {
                _ListaSerie = ServiceManager<FacturaSvc>.Provider.ListarSerie(usuario.IdUsuario);
                _Parametros = new List<ParametroSistemaBe>();
                _Parametros=ServiceManager<ParametroSistemaSvc>.Provider.ListarParametros()
                    .Where(x=>x.Grupo==(int)Utilitarios.CatalogosSunat.TIPODOCUMENTOIDENTIDAD_CATALOGO6).ToList();
            }
            
=======
        public VentasController()
        {
            _ListaSerie=ServiceManager<FacturaSvc>.Provider.ListarSerie();
>>>>>>> 1d36f34b2da68da34bccc406f3d97fbddfd4d44e
        }
        // GET: Ventas
        public ActionResult Index()
        {
<<<<<<< HEAD

            ViewBag.ListaSerie = _ListaSerie;
            ViewBag.ListaParametro = _Parametros;
=======
            ViewBag.ListaSerie = _ListaSerie;
>>>>>>> 1d36f34b2da68da34bccc406f3d97fbddfd4d44e
            return View();
        }
        public async  System.Threading.Tasks.Task<JsonResult> ConsultarNro ( string tipo,string nro ) {
            string url = string.Empty;
            string urlApi = Utilitarios.Settings.ApiUrl;
            string token = Utilitarios.Settings.ApiToken;
            if (tipo == "ruc") url = string.Format("{0}/{1}/{2}?token={3}", urlApi, "ruc", nro, token) ;
            else if(tipo == "dni") url = string.Format("{0}/{1}/{2}?token={3}", urlApi, "dni", nro, token);
            var resultado = "";
            using (var client = new System.Net.Http.HttpClient()) {
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                System.Net.Http.HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode) {
                    var data = await response.Content.ReadAsStringAsync();
                    //var table =Json.JsonConvert.DeserializeObject<System.Data.DataTable>(data);
                    //JavascriptSerializer.
                    resultado = data;
                }
            }
            JsonResult json = Json(new { d = resultado }, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }
    }
}