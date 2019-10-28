using System;
namespace Facturacion.Entidades
{
    public class SerieBe
    {
        public int IdUsuario { get; set; }
        public string Loguin { get; set; }
        public string  Nombres { get; set; }
        public string  Apellidos { get; set; }
        public string  Correo { get; set; }
        public string  Clave { get; set; }
        public int UsuarioCrea { get; set; }
        public int? UsuarioModifica { get; set; }
        public int? UsuarioElimina { get; set; }
        public DateTime  FechaCrea { get; set; }
        public DateTime?  FechaModifica { get; set; }
        public DateTime?  FechaElimina { get; set; }
        public bool  EsEliminacion { get; set; }
        public int? EmpresaId { get; set; }
        public int? PerfilId { get; set; }
    }
}
