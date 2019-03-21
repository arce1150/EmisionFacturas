using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using Facturacion.Servicios.Interfaces;
using Facturacion.Entidades;
using Facturacion.Utilitarios;

namespace Facturacion.Servicios.Business
{
    public class ProductoProvider : ProductoSvc
    {
        private string _conexion = Utilitarios.Settings.conexion;
        public override int InsertarProducto(ProductoBe p)
        {
            int FilasAfectadas = 0;
            using (SqlConnection conexion = new SqlConnection(_conexion))
            {

                try
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("USP_PRODUCTO_INSERT", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter() { ParameterName = "@IdProducto", SqlDbType = SqlDbType.Int, Value = (object)p.IdProducto ?? DBNull.Value, Direction = ParameterDirection.Input, IsNullable = false });
                    cmd.Parameters.Add(new SqlParameter() { ParameterName = "@CodigoSunat", SqlDbType = SqlDbType.VarChar, Value = (object)p.CodigoSunat ?? DBNull.Value, Direction = ParameterDirection.Input, IsNullable = false });
                    cmd.Parameters.Add(new SqlParameter() { ParameterName = "@CodigoBarras", SqlDbType = SqlDbType.VarChar, Value = (object)p.CodigoBarras ?? DBNull.Value, Direction = ParameterDirection.Input, IsNullable = false });
                    cmd.Parameters.Add(new SqlParameter() { ParameterName = "@IdUnidad", SqlDbType = SqlDbType.Char, Value = (object)p.IdUnidad ?? DBNull.Value, Direction = ParameterDirection.Input, IsNullable = false });
                    cmd.Parameters.Add(new SqlParameter() { ParameterName = "@IdAfectacion", SqlDbType = SqlDbType.Char, Value = (object)p.IdAfectacion ?? DBNull.Value, Direction = ParameterDirection.Input, IsNullable = false });
                    cmd.Parameters.Add(new SqlParameter() { ParameterName = "@IdCategoria", SqlDbType = SqlDbType.Int, Value = (object)p.IdCategoria ?? DBNull.Value, Direction = ParameterDirection.Input, IsNullable = false });
                    cmd.Parameters.Add(new SqlParameter() { ParameterName = "@IdTipo", SqlDbType = SqlDbType.Int, Value = (object)p.IdTipo ?? DBNull.Value, Direction = ParameterDirection.Input, IsNullable = false });
                    cmd.Parameters.Add(new SqlParameter() { ParameterName = "@IdMoneda", SqlDbType = SqlDbType.Int, Value = (object)p.IdMoneda ?? DBNull.Value, Direction = ParameterDirection.Input, IsNullable = false });
                    cmd.Parameters.Add(new SqlParameter() { ParameterName = "@Nombre", SqlDbType = SqlDbType.VarChar, Value = (object)p.Nombre ?? DBNull.Value, Direction = ParameterDirection.Input, IsNullable = false });
                    cmd.Parameters.Add(new SqlParameter() { ParameterName = "@Stock", SqlDbType = SqlDbType.Int, Value = (object)p.Stock ?? DBNull.Value, Direction = ParameterDirection.Input, IsNullable = false });
                    cmd.Parameters.Add(new SqlParameter() { ParameterName = "@PrecioCompra", SqlDbType = SqlDbType.Decimal, Value = (object)p.PrecioCompra ?? DBNull.Value, Direction = ParameterDirection.Input, IsNullable = false });
                    cmd.Parameters.Add(new SqlParameter() { ParameterName = "@PrecioVenta", SqlDbType = SqlDbType.Decimal, Value = (object)p.PrecioVenta ?? DBNull.Value, Direction = ParameterDirection.Input, IsNullable = false });
                    cmd.Parameters.Add(new SqlParameter() { ParameterName = "@UsuarioCrea", SqlDbType = SqlDbType.Int, Value = (object)p.UsuarioCrea ?? DBNull.Value, Direction = ParameterDirection.Input, IsNullable = false });
                    cmd.Parameters.Add(new SqlParameter() { ParameterName = "@UsuarioModifica", SqlDbType = SqlDbType.Int, Value = (object)p.UsuarioModifica ?? DBNull.Value, Direction = ParameterDirection.Input, IsNullable = false });
                     
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
        public override List<ProductoBe> PaginarProducto(string Filtro, int PageSize, int PageNumber, out int TotalRegistros)
        {
            List<ProductoBe> Lista = null;
            using (SqlConnection conexion = new SqlConnection(_conexion))
            {
                SqlCommand cmd = new SqlCommand("USP_PRODUCTO_PAGINACION", conexion);
                try
                {
                    conexion.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter() { ParameterName = "@Filtro", SqlDbType = SqlDbType.VarChar, Value = (object)Filtro ?? DBNull.Value, Direction = ParameterDirection.Input, IsNullable = false });
                    cmd.Parameters.Add(new SqlParameter() { ParameterName = "@PageSize", SqlDbType = SqlDbType.Int, Value = (object)PageSize ?? DBNull.Value, Direction = ParameterDirection.Input, IsNullable = false });
                    cmd.Parameters.Add(new SqlParameter() { ParameterName = "@PageNumber", SqlDbType = SqlDbType.Int, Value = (object)PageNumber ?? DBNull.Value, Direction = ParameterDirection.Input, IsNullable = false });
                    SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    if (dr != null)
                    {
                        int pIdProducto = dr.GetOrdinal("IdProducto");
                        int pCodigoProducto = dr.GetOrdinal("CodigoProducto");
                        int pCodigoSunat = dr.GetOrdinal("CodigoSunat");
                        int pCodigoBarras = dr.GetOrdinal("CodigoBarras");
                        int pUnidadStr = dr.GetOrdinal("UnidadStr");
                        int pAfectoStr = dr.GetOrdinal("AfectoStr");
                        int pIdCategoria = dr.GetOrdinal("IdCategoria");
                        int pBienStr = dr.GetOrdinal("BienStr");
                        int pMonedaStr = dr.GetOrdinal("MonedaStr");
                        int pNombre = dr.GetOrdinal("Nombre");
                        int pStock = dr.GetOrdinal("Stock");
                        int pPrecioCompra = dr.GetOrdinal("PrecioCompra");
                        int pPrecioVenta = dr.GetOrdinal("PrecioVenta"); 
                        Lista = new List<ProductoBe>();
                        ProductoBe p = null;
                        while (dr.Read())
                        {
                            p = new ProductoBe();
                            p.IdProducto = dr.GetValueInt32(pIdProducto);
                            p.CodigoProducto = dr.GetValueString(pCodigoProducto);
                            p.CodigoSunat = dr.GetValueString(pCodigoSunat);
                            p.CodigoBarras = dr.GetValueString(pCodigoBarras);
                            p.UnidadStr = dr.GetValueString(pUnidadStr);
                            p.AfectoStr = dr.GetValueString(pAfectoStr);
                            p.IdCategoria = dr.GetValueInt32(pIdCategoria);
                            p.BienStr = dr.GetValueString(pBienStr);
                            p.MonedaStr = dr.GetValueString(pMonedaStr);
                            p.Nombre = dr.GetValueString(pNombre);
                            p.Stock = dr.GetValueInt32(pStock);
                            p.PrecioCompra = dr.GetValueDecimal(pPrecioCompra);
                            p.PrecioVenta = dr.GetValueDecimal(pPrecioVenta); 
                            Lista.Add(p);
                        }
                        dr.Close();
                    }
                    TotalRegistros = (Lista == null || Lista.Count == 0) ? 0 : Lista[0].TotalRegistros;
                    return Lista;
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
        public override ProductoBe ProductoPorCodigo(int IdProducto)
        {
            ProductoBe p = null;
            using (SqlConnection conexion = new SqlConnection(_conexion))
            {
                SqlCommand cmd = new SqlCommand("USP_PRODUCTO_POR_CODIGO", conexion);
                try
                {
                    conexion.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter() { ParameterName = "@IdProducto", SqlDbType = SqlDbType.Int, Value = (object)IdProducto ?? DBNull.Value, Direction = ParameterDirection.Input, IsNullable = false });
                    SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    if (dr != null)
                    {
                        int pIdProducto = dr.GetOrdinal("IdProducto");
                        int pCodigoProducto = dr.GetOrdinal("CodigoProducto");
                        int pCodigoSunat = dr.GetOrdinal("CodigoSunat");
                        int pCodigoBarras = dr.GetOrdinal("CodigoBarras");
                        int pIdUnidad = dr.GetOrdinal("IdUnidad");
                        int pIdAfectacion = dr.GetOrdinal("IdAfectacion");
                        int pIdCategoria = dr.GetOrdinal("IdCategoria");
                        int pIdTipo = dr.GetOrdinal("IdTipo");
                        int pIdMoneda = dr.GetOrdinal("IdMoneda");
                        int pNombre = dr.GetOrdinal("Nombre");
                        int pStock = dr.GetOrdinal("Stock");
                        int pPrecioCompra = dr.GetOrdinal("PrecioCompra");
                        int pPrecioVenta = dr.GetOrdinal("PrecioVenta"); 
                        if (dr.Read())
                        {
                            p = new ProductoBe();
                            p.IdProducto = dr.GetValueInt32(pIdProducto);
                            p.CodigoProducto = dr.GetValueString(pCodigoProducto);
                            p.CodigoSunat = dr.GetValueString(pCodigoSunat);
                            p.CodigoBarras = dr.GetValueString(pCodigoBarras);
                            p.IdUnidad = dr.GetValueString(pIdUnidad);
                            p.IdAfectacion = dr.GetValueString(pIdAfectacion);
                            p.IdCategoria = dr.GetValueInt32(pIdCategoria);
                            p.IdTipo = dr.GetValueInt32(pIdTipo);
                            p.IdMoneda = dr.GetValueInt32(pIdMoneda);
                            p.Nombre = dr.GetValueString(pNombre);
                            p.Stock = dr.GetValueInt32(pStock);
                            p.PrecioCompra = dr.GetValueDecimal(pPrecioCompra);
                            p.PrecioVenta = dr.GetValueDecimal(pPrecioVenta); 
                        }
                        dr.Close();
                    } 
                    return p;
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
        public override string ListarCategorias()
        {
            string resultado = string.Empty;
            using (SqlConnection conexion = new SqlConnection(_conexion))
            {
                SqlCommand cmd = new SqlCommand("USP_CATEGORIA_LISTA", conexion);
                try
                {
                    conexion.Open();
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
        public override string PaginarProductoV2()
        {
            string resultado = string.Empty;
            using (SqlConnection conexion = new SqlConnection(_conexion))
            { 
                try
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("USP_PRODUCTO_PAGINACION", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    //cmd.Parameters.Add(new SqlParameter() { ParameterName = "@IdProducto", SqlDbType = SqlDbType.Int, Value = (object)IdProducto ?? DBNull.Value, Direction = ParameterDirection.Input, IsNullable = false });
                    object data = cmd.ExecuteScalar();
                    if (data != null) resultado = data.ToString();
                    //var Fila = resultado.Split('#')[1];
                    //var Columna = Fila.Split('|');
                    //var UltimaColumna =int.Parse(Columna[Columna.Length -1]);
                    //TotalRegistros =string.IsNullOrEmpty(resultado)? 0: UltimaColumna;
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
