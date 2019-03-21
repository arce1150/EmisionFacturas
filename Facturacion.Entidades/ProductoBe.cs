using System;
namespace Facturacion.Entidades
{
    public class ProductoBe : EntidadBase
    {
        public int IdProducto { get; set; }
        public string CodigoProducto { get; set; }
        public string CodigoSunat { get; set; }
        public string CodigoBarras { get; set; }
        public string IdUnidad { get; set; }
        public string IdAfectacion { get; set; }
        public int IdCategoria { get; set; }
        public int IdTipo { get; set; }
        public int IdMoneda { get; set; }
        public string Nombre { get; set; }
        public int Stock { get; set; }
        public decimal? PrecioCompra { get; set; }
        public decimal? PrecioVenta { get; set; }
        public string UnidadStr { get; set; }
        public string AfectoStr { get; set; }
        public string BienStr { get; set; }
        public string MonedaStr { get; set; }
        public string CategoriaStr { get; set; }
        public int TotalRegistros { get; set; }

    }
}
