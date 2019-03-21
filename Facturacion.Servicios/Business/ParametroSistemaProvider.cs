using System;
using System.Collections.Generic; 
using Facturacion.Servicios.Interfaces;
using System.Data;
using System.Data.SqlClient;
using Facturacion.Entidades;
using Facturacion.Utilitarios;

namespace Facturacion.Servicios.Business
{
    public class ParametroSistemaProvider : ParametroSistemaSvc
    {
        private string _conexion = Settings.conexion;
        public override List<ParametroSistemaBe> ListarParametros()
        {
            List<ParametroSistemaBe> Lista = null;
            using (SqlConnection conexion = new SqlConnection(_conexion))
            {
                SqlCommand cmd = new SqlCommand("USP_PARAMETRO_LISTA", conexion);
                try
                {
                    conexion.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    if (dr != null)
                    {
                        int pIdParametro = dr.GetOrdinal("IdParametro");
                        int pCodigoParametro = dr.GetOrdinal("CodigoParametro");
                        int pDescripcion = dr.GetOrdinal("Descripcion"); 
                        int pGrupo = dr.GetOrdinal("Grupo");
                        int pNombreGrupo = dr.GetOrdinal("NombreGrupo");
                        int pTituloGrupo = dr.GetOrdinal("TituloGrupo"); 

                        Lista = new List<ParametroSistemaBe>();
                        ParametroSistemaBe parametro = null;
                        while (dr.Read())
                        {
                            parametro = new ParametroSistemaBe();
                            parametro.IdParametro = dr.GetValueInt32(pIdParametro);
                            parametro.CodigoParametro = dr.GetValueString(pCodigoParametro);
                            parametro.Descripcion = dr.GetValueString(pDescripcion); 
                            parametro.Grupo = dr.GetValueInt16(pGrupo);
                            parametro.NombreGrupo = dr.GetValueString(pNombreGrupo);
                            parametro.TituloGrupo = dr.GetValueString(pTituloGrupo);  
                            Lista.Add(parametro);
                        }
                        dr.Close();
                    }
                    return Lista;
                }
                catch (SqlException ex)
                {
                    Log.GrabarLog(ex.Message, ex.StackTrace);
                    throw ex;
                }
                catch (Exception ex1) {
                    Log.GrabarLog(ex1.Message, ex1.StackTrace);
                    throw ex1;
                }

            }
        }
    }
}
