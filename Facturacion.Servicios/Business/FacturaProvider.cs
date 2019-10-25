using System; 
using System.Data;
using System.Data.SqlClient;
using Facturacion.Servicios.Interfaces;
using Facturacion.Utilitarios;

namespace Facturacion.Servicios.Business
{
    public class FacturaProvider : FacturaSvc
    {
        private string _conexion = Utilitarios.Settings.conexion;
        public override string ListarSerie()
        {
            string resultado = string.Empty;
            using (SqlConnection conexion = new SqlConnection(_conexion))
            {
                try
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("USP_SERIE_LISTADO", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    object data = cmd.ExecuteScalar();
                    if (data != null) resultado = data.ToString();
                    return resultado;
                }
                catch (SqlException ex)
                {
                    Log.GrabarLog(ex.Message, ex.StackTrace);
                    throw ex;
                }
                catch (Exception ex1)
                {
                    Log.GrabarLog(ex1.Message, ex1.StackTrace);
                    throw ex1;
                }
            } 
        }
    }
}
