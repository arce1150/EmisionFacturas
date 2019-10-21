using System;
using System.Data.SqlClient;
using System.Data;
using Facturacion.Entidades;
using Facturacion.Servicios.Interfaces;
using Facturacion.Utilitarios;
using System.Collections.Generic;

namespace Facturacion.Servicios.Business
{
    public class UsuarioProvider : UsuarioSvc
    {
        private string _conexion = Utilitarios.Settings.conexion;
        public override int InsertarUsuario(UsuarioBe u)
        {
            int FilasAfectadas = 0;
            using (SqlConnection conexion = new SqlConnection(_conexion))
            {

                try
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("USP_USUARIO_INSERT", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter() { ParameterName = "@IdUsuario", SqlDbType = SqlDbType.Int, Value = (object)u.IdUsuario ?? DBNull.Value, Direction = ParameterDirection.Input, IsNullable = false });
                    cmd.Parameters.Add(new SqlParameter() { ParameterName = "@Loguin", SqlDbType = SqlDbType.VarChar, Value = (object)u.Loguin ?? DBNull.Value, Direction = ParameterDirection.Input, IsNullable = false });
                    cmd.Parameters.Add(new SqlParameter() { ParameterName = "@Nombres", SqlDbType = SqlDbType.VarChar, Value = (object)u.Nombres ?? DBNull.Value, Direction = ParameterDirection.Input, IsNullable = false });
                    cmd.Parameters.Add(new SqlParameter() { ParameterName = "@Apellidos", SqlDbType = SqlDbType.VarChar, Value = (object)u.Apellidos ?? DBNull.Value, Direction = ParameterDirection.Input, IsNullable = false });
                    cmd.Parameters.Add(new SqlParameter() { ParameterName = "@Correo", SqlDbType = SqlDbType.VarChar, Value = (object)u.Correo ?? DBNull.Value, Direction = ParameterDirection.Input, IsNullable = false });
                    cmd.Parameters.Add(new SqlParameter() { ParameterName = "@Clave", SqlDbType = SqlDbType.VarChar, Value = (object)u.Clave ?? DBNull.Value, Direction = ParameterDirection.Input, IsNullable = false });
                    cmd.Parameters.Add(new SqlParameter() { ParameterName = "@UsuarioCrea", SqlDbType = SqlDbType.Int, Value = (object)u.UsuarioCrea ?? DBNull.Value, Direction = ParameterDirection.Input, IsNullable = false });
                     
                    FilasAfectadas = cmd.ExecuteNonQuery();
                    return FilasAfectadas;
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
        public override UsuarioBe ValidarUsuario(string u, string pw)
        {
            UsuarioBe usuario = null;
            using (SqlConnection conexion = new SqlConnection(_conexion))
            {

                try
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("USP_USUARIO_VALIDACION", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter() { ParameterName = "@Loguin", SqlDbType = SqlDbType.VarChar, Value = (object)u ?? DBNull.Value, Direction = ParameterDirection.Input, IsNullable = false });
                    cmd.Parameters.Add(new SqlParameter() { ParameterName = "@Clave", SqlDbType = SqlDbType.VarChar, Value = (object)pw ?? DBNull.Value, Direction = ParameterDirection.Input, IsNullable = false });
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr != null) { 
                        int pIdUsuario = dr.GetOrdinal("IdUsuario");
                        int pLoguin = dr.GetOrdinal("Loguin");
                        int pNombres = dr.GetOrdinal("Nombres");
                        int pApellidos = dr.GetOrdinal("Apellidos");
                        int pCorreo = dr.GetOrdinal("Correo");
                        int pClave = dr.GetOrdinal("Clave");
                        int pPerfilId = dr.GetOrdinal("PerfilId");
                        int pPerfilStr = dr.GetOrdinal("PerfilStr");
                        int pEmpresaId = dr.GetOrdinal("EmpresaId");
                        int pRazonSocial = dr.GetOrdinal("RazonSocial");
                        int pRuc = dr.GetOrdinal("Ruc");
                        if (dr.Read()) {
                            usuario = new UsuarioBe();
                            usuario.IdUsuario = dr.GetValueInt32(pIdUsuario);
                            usuario.Loguin = dr.GetValueString(pLoguin);
                            usuario.Nombres = dr.GetValueString(pNombres);
                            usuario.Apellidos = dr.GetValueString(pApellidos);
                            usuario.Correo = dr.GetValueString(pCorreo);
                            usuario.Clave = dr.GetValueString(pClave);
                            usuario.PerfilId = dr.GetValueInt32(pPerfilId);
                            usuario.PerfilStr = dr.GetValueString(pPerfilStr);
                            usuario.EmpresaId = dr.GetValueInt32(pEmpresaId);
                            usuario.RazonSocial = dr.GetValueString(pRazonSocial);
                            usuario.Ruc=dr.GetValueString(pRuc);
                            if (dr.NextResult()) {
                                int pMenuId = dr.GetOrdinal("MenuId");
                                int pNombre = dr.GetOrdinal("Nombre");
                                int pUrl = dr.GetOrdinal("Url");
                                int pOrden = dr.GetOrdinal("Orden");
                                int pPerfiles = dr.GetOrdinal("Perfiles");
                                int pMenuPadre = dr.GetOrdinal("MenuPadre");
                                MenuBe menu = null;
                                List<MenuBe> ListaMenu = new List<MenuBe>();
                                while (dr.Read()) {
                                    menu = new MenuBe();
                                    menu.MenuId= dr.GetValueInt32(pMenuId);
                                    menu.Nombre = dr.GetValueString(pNombre);
                                    menu.Url = dr.GetValueString(pUrl);
                                    menu.Orden = dr.GetValueInt16(pOrden);
                                    menu.Perfiles = dr.GetValueString(pPerfiles);
                                    menu.MenuPadre = dr.GetValueInt32(pMenuPadre);
                                    ListaMenu.Add(menu);
                                }
                                usuario.ListMenus = ListaMenu;
                            }
                            dr.Close();
                        }
                        
                    }
                    return usuario;
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
