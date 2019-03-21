using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Reflection;
using Facturacion.ServicioWindows.Entidades;
namespace Facturacion.ServicioWindows.AccesoDatos
{
    public class PadronSunatDa
    {
        public int InsertarPadronSunat(SqlConnection con, List<PadronSunatBe> padron)
        {
            int filasAfectadas = 0;
            SqlBulkCopy bulkCopy = null;
            try
            {
                SqlCommand cmd = new SqlCommand("USP_ELIMNAR_PADRON_RUC", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("@Anio", "valor a eliminar");
                cmd.ExecuteNonQuery();

                bulkCopy = new SqlBulkCopy(string.Empty);
                System.Data.DataTable dt = SqlBullCopyMaping.MapModel(padron, "PadronSunat");
                using (bulkCopy = new SqlBulkCopy(con))
                {
                    bulkCopy.BatchSize = 500;
                    bulkCopy.BulkCopyTimeout = 0;
                    bulkCopy.DestinationTableName = "[version2].[PadronSunat]";
                    bulkCopy.ColumnMappings.Add("Ruc", "Ruc");
                    bulkCopy.ColumnMappings.Add("RazonSocial", "RazonSocial");
                    bulkCopy.ColumnMappings.Add("EstadoContribuyente", "EstadoContribuyente");
                    bulkCopy.ColumnMappings.Add("CondicionDomicilio", "CondicionDomicilio");
                    bulkCopy.ColumnMappings.Add("Ubigeo", "Ubigeo");
                    bulkCopy.ColumnMappings.Add("TipoDeVia", "TipoDeVia");
                    bulkCopy.ColumnMappings.Add("NombreDeVia", "NombreDeVia");
                    bulkCopy.ColumnMappings.Add("NroDirecion", "NroDirecion");
                    bulkCopy.WriteToServer(dt);
                }
                return filasAfectadas = 1;
            }
            catch (Exception ex)
            {
                var errorMessage = GetBulkCopyColumnException(ex, bulkCopy);
                throw new Exception(errorMessage, ex);
            }
        }
        protected string GetBulkCopyColumnException(Exception ex, SqlBulkCopy bulkcopy)
        {
            string message = string.Empty;
            if (ex.Message.Contains("Received an invalid column length from the bcp client for colid"))
            {
                string pattern = @"\d+";
                System.Text.RegularExpressions.Match match = System.Text.RegularExpressions.Regex.Match(ex.Message.ToString(), pattern);
                var index = Convert.ToInt32(match.Value) - 1;

                FieldInfo fi = typeof(SqlBulkCopy).GetField("_sortedColumnMappings", BindingFlags.NonPublic | BindingFlags.Instance);
                var sortedColumns = fi.GetValue(bulkcopy);
                var items = (Object[])sortedColumns.GetType().GetField("_items", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(sortedColumns);

                FieldInfo itemdata = items[index].GetType().GetField("_metadata", BindingFlags.NonPublic | BindingFlags.Instance);
                var metadata = itemdata.GetValue(items[index]);
                var column = metadata.GetType().GetField("column", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).GetValue(metadata);
                var length = metadata.GetType().GetField("length", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).GetValue(metadata);
                message = (String.Format("Column: {0} contains data with a length greater than: {1}", column, length));
            }
            return message;
        }
    }
}
