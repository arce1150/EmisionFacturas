using System; 
namespace Facturacion.Entidades
{
    public class UsuarioBe:EntidadBase
    {
        public int IdUsuario { get; set; }
        public string Loguin { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Correo { get; set; }
        public string Clave { get; set; } 

    }
}
