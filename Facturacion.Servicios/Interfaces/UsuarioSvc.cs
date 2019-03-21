using System;
using System.Collections.Generic;
using Facturacion.Entidades;
namespace Facturacion.Servicios.Interfaces
{
    public abstract class UsuarioSvc: ServiceProvider
    {
        public abstract UsuarioBe ValidarUsuario(string u, string pw);
        public abstract int InsertarUsuario(UsuarioBe u);
    }
}
