using System;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.ServiceProcess;
using System.Threading;

namespace Facturacion.ServicioWindows
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            //ServiceBase[] ServicesToRun;
            //ServicesToRun = new ServiceBase[]
            //{
            //    new Service1()
            //};
            //ServiceBase.Run(ServicesToRun);
            Thread.CurrentThread.CurrentCulture = new CultureInfo("es-PE");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("es-PE");
            if (ConfigurationManager.AppSettings.Get("Modo") == "Debug")
            {
                new ServicioWindows().EjecutarServicio();
            }
            else
            {
                var servicesToRun = new ServiceBase[]{
                        new ServicioWindows()
                };
                ServiceBase.Run(servicesToRun);
            }
        }
    }
}
