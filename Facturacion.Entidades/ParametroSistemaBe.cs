using System; 
namespace Facturacion.Entidades
{
    public class ParametroSistemaBe:EntidadBase
    {
        public int IdParametro { get; set; }
        public string CodigoParametro { get; set; }
        public string Descripcion { get; set; }
        public Int16? Valor { get; set; }
        public Int16? Grupo { get; set; }
        public string NombreGrupo { get; set; }
        public string TituloGrupo { get; set; }
        public int? PadreId { get; set; }
    }
}
