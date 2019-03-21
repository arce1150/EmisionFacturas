using System;
using System.Collections.Generic;
 
namespace Facturacion.ServicioWindows
{
    public class LogicaPadron
    {
        readonly string _conexion = System.Configuration.ConfigurationManager.ConnectionStrings["Facturacion.Conexion"].ConnectionString;
    }
}
