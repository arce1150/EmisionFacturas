using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
namespace Facturacion.Utilitarios
{
    public class JSON
    {
        public static string SerializarLista<T> ( List<T> lista, List<string> campos = null )
        {
            StringBuilder sb = new StringBuilder("[");
            int nFilas = lista.Count;
            PropertyInfo[] oPropiedades = lista[0].GetType().GetProperties();
            List<string> propiedades = null;
            if (campos != null && campos.Count > 0)
            {
                propiedades = new List<string>();
                foreach (PropertyInfo oPropiedad in oPropiedades)
                {
                    propiedades.Add(oPropiedad.Name);
                }
            }
            int nCols;
            string tipo;
            string propiedad = "";
            int pos = -1;
            object oValorCampo = null;
            for (int i = 0; i < nFilas; i++)
            {
                oPropiedades = lista[i].GetType().GetProperties();
                sb.Append("{");
                if (campos == null)
                {
                    nCols = oPropiedades.Length;
                    for (int j = 0; j < nCols; j++)
                    {
                        propiedad = oPropiedades[j].Name;
                        tipo = oPropiedades[j].PropertyType.ToString().ToLower();
                        oValorCampo = oPropiedades[j].GetValue(lista[i], null);
                        switch (tipo)
                        {
                            case "system.int32":
                            case "system.int16":
                            case "system.decimal":
                            case "system.double":
                            case "system.float": 
                                oValorCampo = oValorCampo == null ? "\"\"" : oValorCampo.ToString();
                                break;
                            case "system.datetime":
                            case "system.nullable`1[system.datetime]":
                                oValorCampo = oValorCampo == null ? "\"\"" : string.Format("\"{0}\"", oValorCampo.ToString().Replace("/","-")) ;
                                break;
                            case "system.boolean":
                                oValorCampo = oValorCampo == null ? "\"\"" : oValorCampo.ToString().ToLower();
                                break;
                            default:
                                oValorCampo = oValorCampo == null ? "\"\"" : oValorCampo.ToString();
                                break;
                        } 
                        sb.Append(String.Format("\"{0}\":{1}{2}{1}", propiedad, (tipo.Contains("string") ? "\"" : ""), oValorCampo));
                        if (j < nCols - 1) sb.Append(",");
                    }
                }
                else
                {
                    nCols = campos.Count;
                    for (int j = 0; j < nCols; j++)
                    {
                        pos = propiedades.IndexOf(campos[j]);
                        if (pos > -1)
                        {
                            propiedad = campos[j];
                            tipo = lista[i].GetType().GetProperty(propiedad).PropertyType.ToString().ToLower();
                            sb.Append(String.Format("\"{0}\":{1}{2}{1}", propiedad, (tipo.Contains("string") ? "\"" : ""),
                                lista[i].GetType().GetProperty(propiedad).GetValue(lista[i], null).ToString()));
                            if (j < nCols - 1) sb.Append(",");
                        }
                    }
                }
                sb.Append("}");
                if (i < nFilas - 1) sb.Append(",");
            }
            sb.Append("]");
            return sb.ToString();
        }
    }
}
