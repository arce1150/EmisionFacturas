using System;
using System.Collections.Generic;
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
        }
    }
}
