using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Timers;

namespace Facturacion.ServicioWindows
{
    public partial class ServicioWindows: ServiceBase
    {
        System.Timers.Timer _Temporizador = new System.Timers.Timer();
        bool _sincronizacionNormal = true;
        protected override void OnStart(string[] args)
        {
            _Temporizador.Elapsed += new System.Timers.ElapsedEventHandler(OnElapsedTime);
            _Temporizador.Enabled = true;
            _Temporizador.AutoReset = false;
        }

        protected override void OnStop()
        {
            _Temporizador.Enabled = false;
            _Temporizador.Stop();
        }
        private void OnElapsedTime(object sender, ElapsedEventArgs e)
        {
            try
            {
                int horaActual = DateTime.Now.Hour;
                _Temporizador.Interval = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["RunInterval"]) * 60000;
                _Temporizador.Start();
                if (horaActual == int.Parse(System.Configuration.ConfigurationManager.AppSettings.Get("HoraConvencional"))
                    && _sincronizacionNormal == true)
                {
                    EjecutarServicio();
                    _sincronizacionNormal = false;
                }
            }
            catch (Exception ex)
            {
            }
        }
        public void EjecutarServicio()
        {
            //var registro = new RegistroPadron();
            //registro.ProcesarPadron();
            string RutaFichero = System.Configuration.ConfigurationManager.AppSettings["RutaFicheroPadron"];
            string Path =string.Concat(RutaFichero, "padron_reducido_ruc.txt");
            ReadFileInBatch(Path);
        }
        internal static void ReadFileInBatch(string Path)
        {
            string RutaFichero = System.Configuration.ConfigurationManager.AppSettings["RutaFicheroPadron"];
            int TotalRows = File.ReadLines(Path).Count();  
            int Limit = 10000;
            System.Data.DataTable dt = null;
            string Logs = string.Empty;
            for (int Offset = 0; Offset < TotalRows; Offset += Limit)
            {
                // Print Logs
                //Logs = string.Format("Processing :: Rows {0} of Total {1} :: Offset {2} : Limit : {3}",
                //    (Offset + Limit) < TotalRows ? Offset + Limit : TotalRows,
                //    TotalRows, Offset, Limit
                //); 
                //Console.WriteLine(Logs);
                dt = Path.FileToTable(heading: true, delimiter: '|', offset: Offset, limit: Limit);
                 
                //dt.TableToFile(string.Concat(RutaFichero, @"output.txt"));
                ConvertFileToTable.InsertarPadron(dt);
            }
        }
    }
}
