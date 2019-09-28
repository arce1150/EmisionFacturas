using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;

namespace Facturacion.ServicioWindows
{
    public static class ConvertFileToTable
    {
        private static string _conexion = System.Configuration.ConfigurationManager.ConnectionStrings["ConexionPadron"].ConnectionString;
        public static DataTable FileToTable(this string path, bool heading = true, char delimiter = '\t')
        {
            var table = new DataTable();
            string headerLine = File.ReadLines(path).FirstOrDefault(); // Read the first row for headings
            string[] headers = headerLine.Split(delimiter);
            int skip = 1;
            int num = 1;
            foreach (string header in headers)
            {
                if (heading)
                    table.Columns.Add(header);
                else
                {
                    table.Columns.Add("Field" + num); // Create fields header if heading is false
                    num++;
                    skip = 0; // Don't skip the first row if heading is false
                }
            }
            foreach (string line in File.ReadLines(path).Skip(skip))
            {
                if (!string.IsNullOrEmpty(line))
                {
                    table.Rows.Add(line.Split(delimiter));
                }
            }
            return table;
        }
        public static DataTable FileToTable(this string path, bool heading = true, char delimiter = '\t', int offset = 0, int limit = 100000)
        {
            DataTable table = new DataTable();
            string headerLine = File.ReadLines(path).FirstOrDefault(); // Read the first row for headings
            string[] headers = headerLine.Split(delimiter);
            int skip = 1;
            int num = 1;
            if (headers.Count() > 0)
            {
                string Columnas = "RUC|RazonSocial|EstadoContribuyente|CondicionDomicilio|Ubigeo|TipoDeVia|NombreDeVia|CodigoDeZona|";
                Columnas += "TipoDeZona|Numero|Interior|Lote|Departamento|Manzana|Kilometro|FechaCreacion";
                headers = Columnas.Split('|');
            }
            foreach (string item in headers)
            {
                if (heading)
                {
                    table.Columns.Add(item.Trim());
                }
                else
                {
                    table.Columns.Add("Field" + num); // Create fields header if heading is false
                    num++;
                    skip = 0; // Don't skip the first row if heading is false
                }
            }
            string[] Fila = null;
            string[] Temporal = null;
            int NroColumnas = table.Columns.Count;
            string FilaRead = string.Empty;
            foreach (string linea in File.ReadLines(path).Skip(skip + offset).Take(limit))
            {
                if (!string.IsNullOrEmpty(linea))
                {
                    FilaRead = linea + DateTime.Now.ToString("yyyy/MM/dd") + "|";
                    Fila = FilaRead.Split(delimiter);
                    if (Fila.Length > NroColumnas)
                    {
                        Temporal = new string[NroColumnas];
                        Array.Copy(Fila, Temporal, NroColumnas);
                        Fila = new string[NroColumnas];
                        Array.Copy(Temporal, Fila, NroColumnas);
                    }
                    table.Rows.Add(Fila);
                }
            }
            return table;
        }
        public static void TableToFile(this DataTable table, string path, bool append = true)
        {
            StringBuilder stringBuilder = new StringBuilder();
            if (!File.Exists(path) || !append)
                stringBuilder.AppendLine(string.Join("\t", table.Columns.Cast<DataColumn>().Select(arg => arg.ColumnName)));

            using (StreamWriter sw = new StreamWriter(path, append))
            {
                foreach (DataRow dataRow in table.Rows)
                    stringBuilder.AppendLine(string.Join("\t", dataRow.ItemArray.Select(arg => arg.ToString())));
                sw.Write(stringBuilder.ToString());
            }
        }
        public static void InsertarPadron(DataTable dt)
        {
            string tblTemp = string.Empty;
            string mergeSql = string.Empty;
            tblTemp = "CREATE TABLE #TmpPadron(";
            tblTemp += "[IdPadron][int] IDENTITY(1, 1) NOT NULL PRIMARY KEY,";
            tblTemp += "[RUC] [char](11) NOT NULL, ";
            tblTemp += "[RazonSocial] [varchar](200) NULL,";
            tblTemp += "[EstadoContribuyente] [varchar](50) NULL,";
            tblTemp += "[CondicionDomicilio] [varchar](50) NULL,";
            tblTemp += "[Ubigeo] [varchar](10) NULL,";
            tblTemp += "[TipoDeVia] [varchar](100) NULL,";
            tblTemp += "[NombreDeVia] [varchar](100) NULL,";
            tblTemp += "[CodigoDeZona] [varchar](50) NULL,";
            tblTemp += "[TipoDeZona] [varchar](100) NULL,";
            tblTemp += "[Numero] [varchar](50) NULL,";
            tblTemp += "[Interior] [varchar](50) NULL,";
            tblTemp += "[Lote] [varchar](50) NULL,";
            tblTemp += "[Departamento] [varchar](50) NULL,";
            tblTemp += "[Manzana] [varchar](50) NULL,";
            tblTemp += "[Kilometro] [varchar](50) NULL,";
            tblTemp += "[FechaCreacion] [datetime] NOT NULL);";
            try
            {
                SqlBulkCopy bulkCopy = null;
                bulkCopy = new SqlBulkCopy(string.Empty);
                using (SqlConnection con = new SqlConnection(_conexion))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(tblTemp, con);
                    cmd.ExecuteNonQuery();
                    using (bulkCopy = new SqlBulkCopy(con))
                    {
                        bulkCopy.BatchSize = 500;
                        bulkCopy.BulkCopyTimeout = 0;
                        bulkCopy.DestinationTableName = "#TmpPadron";
                        bulkCopy.ColumnMappings.Add("RUC", "RUC");
                        bulkCopy.ColumnMappings.Add("RazonSocial", "RazonSocial");
                        bulkCopy.ColumnMappings.Add("EstadoContribuyente", "EstadoContribuyente");
                        bulkCopy.ColumnMappings.Add("CondicionDomicilio", "CondicionDomicilio");
                        bulkCopy.ColumnMappings.Add("Ubigeo", "Ubigeo");
                        bulkCopy.ColumnMappings.Add("TipoDeVia", "TipoDeVia");
                        bulkCopy.ColumnMappings.Add("NombreDeVia", "NombreDeVia");
                        bulkCopy.ColumnMappings.Add("CodigoDeZona", "CodigoDeZona");
                        bulkCopy.ColumnMappings.Add("TipoDeZona", "TipoDeZona");
                        bulkCopy.ColumnMappings.Add("Numero", "Numero");
                        bulkCopy.ColumnMappings.Add("Interior", "Interior");
                        bulkCopy.ColumnMappings.Add("Lote", "Lote");
                        bulkCopy.ColumnMappings.Add("Departamento", "Departamento");
                        bulkCopy.ColumnMappings.Add("Manzana", "Manzana");
                        bulkCopy.ColumnMappings.Add("Kilometro", "Kilometro");
                        bulkCopy.ColumnMappings.Add("FechaCreacion", "FechaCreacion");
                        bulkCopy.WriteToServer(dt);
                    }
                    //mergeSql = "MERGE INTO PadronSunat AS Target ";
                    //mergeSql += "USING #TmpPadron AS Source ";
                    //mergeSql += "on ";
                    //mergeSql += "Target.RUC=Source.RUC and Source.RUC IS NOT NULL ";
                    //mergeSql += "WHEN MATCHED THEN ";
                    mergeSql += "UPDATE SET Target.RazonSocial=Source.RazonSocial, ";
                    mergeSql += "Target.EstadoContribuyente=Source.EstadoContribuyente,";
                    mergeSql += "Target.CondicionDomicilio=Source.CondicionDomicilio,";
                    mergeSql += "Target.Ubigeo=Source.Ubigeo,";
                    mergeSql += "Target.TipoDeVia=Source.TipoDeVia,";
                    mergeSql += "Target.NombreDeVia=Source.NombreDeVia,";
                    mergeSql += "Target.CodigoDeZona=Source.CodigoDeZona,";
                    mergeSql += "Target.TipoDeZona=Source.TipoDeZona,";
                    mergeSql += "Target.Numero=Source.Numero,";
                    mergeSql += "Target.Interior=Source.Interior,";
                    mergeSql += "Target.Lote=Source.Lote,";
                    mergeSql += "Target.Departamento=Source.Departamento,";
                    mergeSql += "Target.Manzana=Source.Manzana,";
                    mergeSql += "Target.Kilometro=Source.Kilometro,";
                    mergeSql += "Target.FechaCreacion=Source.FechaCreacion ";

                    //mergeSql += "WHEN NOT MATCHED THEN ";
                    //mergeSql += "INSERT (RazonSocial,EstadoContribuyente,CondicionDomicilio,Ubigeo,TipoDeVia,NombreDeVia,";
                    //mergeSql += "CodigoDeZona,TipoDeZona,Numero,Interior,Lote,Departamento,Manzana,Kilometro,FechaCreacion)";
                    //mergeSql += "VALUES (Source.RazonSocial,Source.EstadoContribuyente,Source.CondicionDomicilio,Source.Ubigeo,";
                    //mergeSql += "Source.TipoDeVia,Source.NombreDeVia,Source.CodigoDeZona,Source.TipoDeZona,Source.Numero,";
                    //mergeSql += "Source.Interior,Source.Lote,Source.Departamento,Source.Manzana,Source.Kilometro,Source.FechaCreacion);";
                    //cmd.CommandText = mergeSql;
                    //cmd.ExecuteNonQuery();
                    cmd.CommandText = "drop table #TmpPadron";
                    cmd.ExecuteNonQuery();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
