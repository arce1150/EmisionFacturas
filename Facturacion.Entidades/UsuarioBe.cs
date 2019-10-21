using System;
using System.Collections.Generic;

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
        public int? PerfilId { get; set; }
        public string PerfilStr { get; set; }
        public int? EmpresaId { get; set; }
        public string RazonSocial { get; set; }
        public string Ruc { get; set; }
        public List<MenuBe> ListMenus { get; set; }
    }
}
