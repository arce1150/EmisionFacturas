using System;
using System.Collections.Generic;
using Facturacion.Entidades;
namespace Facturacion.Servicios.Interfaces
{
    public abstract partial class ParametroSistemaSvc: ServiceProvider
    {
        public abstract List<ParametroSistemaBe> ListarParametros();
    }
}
