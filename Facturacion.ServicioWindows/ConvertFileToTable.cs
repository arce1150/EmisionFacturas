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
            if (headers.Count() > 0) {
                string Columnas = "RUC|RazonSocial|EstadoContribuyente|CondicionDomicilio|Ubigeo|TipoDeVia|NombreDeVia|CodigoDeZona|";
                Columnas +="TipoDeZona|Numero|Interior|Lote|Departamento|Manzana|Kilometro|FechaCreacion";
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
                    FilaRead = linea + DateTime.Now.ToString("yyyy/MM/dd")+"|"; 
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
        public static void InsertarPadron(DataTable dt) {
            try {
                SqlBulkCopy bulkCopy = null;
                bulkCopy = new SqlBulkCopy(string.Empty); 
                using (SqlConnection con = new SqlConnection(_conexion)) {
                    con.Open();
                    using (bulkCopy = new SqlBulkCopy(con))
                    {
                        bulkCopy.BatchSize = 500;
                        bulkCopy.BulkCopyTimeout = 0;
                        bulkCopy.DestinationTableName = "PadronSunat";
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
                }
                
            }
            catch (Exception ex) {

            }
             
        }
    }
}
