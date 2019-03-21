using System;
namespace Facturacion.ServicioWindows.Entidades
{
    public class PadronSunatBe
    {
        public int IdPadron { get; set; }
        [ModelMapAttribute()]
        public string Ruc { get; set; }
        [ModelMapAttribute()]
        public string RazonSocial { get; set; }
        [ModelMapAttribute()]
        public string EstadoContribuyente { get; set; }
        [ModelMapAttribute()]
        public string CondicionDomicilio { get; set; }
        [ModelMapAttribute()]
        public string Ubigeo { get; set; }
        [ModelMapAttribute()]
        public string TipoDeVia { get; set; }
        [ModelMapAttribute()]
        public string NombreVia { get; set; }
        [ModelMapAttribute()]
        public string CodigoZona { get; set; }
        [ModelMapAttribute()]
        public string TipoZona { get; set; }
        [ModelMapAttribute()]
        public string Numero { get; set; }
        [ModelMapAttribute()]
        public string Interior { get; set; }
        [ModelMapAttribute()]
        public string Lote { get; set; }
        [ModelMapAttribute()]
        public string Departamento { get; set; }
        [ModelMapAttribute()]
        public string Manzana { get; set; }
        [ModelMapAttribute()]
        public string Kilometro { get; set; }
    }
}
