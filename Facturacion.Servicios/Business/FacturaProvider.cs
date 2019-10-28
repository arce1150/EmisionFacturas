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
<<<<<<< HEAD
        public override string ListarSerie(int usuarioId)
=======
        public override string ListarSerie()
>>>>>>> 1d36f34b2da68da34bccc406f3d97fbddfd4d44e
        {
            string resultado = string.Empty;
            using (SqlConnection conexion = new SqlConnection(_conexion))
            {
                try
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("USP_SERIE_LISTADO", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
<<<<<<< HEAD
                    cmd.Parameters.Add(new SqlParameter() { ParameterName = "@IdUsuario", SqlDbType = SqlDbType.VarChar, Value = (object)usuarioId ?? DBNull.Value, Direction = ParameterDirection.Input, IsNullable = false });
=======
>>>>>>> 1d36f34b2da68da34bccc406f3d97fbddfd4d44e
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
