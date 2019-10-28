using System;
using System.Collections.Generic;
using Facturacion.Entidades;
namespace Facturacion.Servicios.Interfaces
{
    public abstract partial class FacturaSvc : ServiceProvider
    {
        public abstract string ListarSerie(int usuarioId);
    }
}
