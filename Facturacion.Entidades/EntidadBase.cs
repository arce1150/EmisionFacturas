using System;
 
namespace Facturacion.Entidades
{
    public class EntidadBase
    {
        public int UsuarioCrea { get; set; }
        public int? UsuarioModifica { get; set; }
        public int? UsuarioElimina { get; set; } 
        public DateTime FechaCrea { get; set; }
        public DateTime? FechaModifica { get; set; }
        public DateTime? FechaEliminacion { get; set; } 
        public bool FlagEliminacion { get; set; }
    }
}
