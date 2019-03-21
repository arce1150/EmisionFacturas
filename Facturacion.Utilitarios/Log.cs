using System;
using System.Configuration;
using System.IO;
using System.Reflection;
namespace Facturacion.Utilitarios
{
    public class Log
    {
        public static void GrabarLog(string mensaje, string detalle)
        {
            string ruta = ConfigurationManager.AppSettings["Errores.Log"].ToString();
            LogBe log = new LogBe();
            log.FechaLog = DateTime.Now;
            log.MensajeLog = mensaje;
            log.DetalleLog = detalle;
            PropertyInfo[] propiedades = log.GetType().GetProperties();
            string archivoLog = string.Format("Facturacion.Log_{0}{1}{2}.txt", DateTime.Now.Year.ToString(), DateTime.Now.Month.ToString("00"), DateTime.Now.Day.ToString("00"));
            string rutaArchivo = string.Concat(ruta, archivoLog);
            using (StreamWriter sw = new StreamWriter(rutaArchivo, true))
            {
                foreach (var item in propiedades)
                {
                    sw.WriteLine("{0}={1}", item.Name, item.GetValue(log, null));
                }
                sw.WriteLine(new string('-', 200));
            }
        }
    }
}
