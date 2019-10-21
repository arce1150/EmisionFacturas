using System;
namespace Facturacion.Entidades
{
    public class MenuBe
    {
        public int MenuId { get; set; }
        public string Nombre { get; set; }
        public string Url { get; set; }
        public Int16 Orden { get; set; }
        public string Perfiles { get; set; }
        public int? MenuPadre { get; set; }
        public int UsuarioCrea { get; set; }
    }
}
