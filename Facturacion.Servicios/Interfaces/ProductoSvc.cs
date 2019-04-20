using System;
using System.Collections.Generic;
using Facturacion.Entidades;
namespace Facturacion.Servicios.Interfaces
{
    public abstract partial class ProductoSvc:ServiceProvider
    {
        public abstract int InsertarProducto(ProductoBe p);
        public abstract ProductoBe ProductoPorCodigo(int IdProducto);
        public abstract List<ProductoBe> PaginarProducto(string Filtro,int PageSize,int PageNumber, out int TotalRegistros);
        public abstract string PaginarProductoV2();
        public abstract string ListarCategorias();
        public abstract int EliminarProducto(int IdProducto,int UsuarioElimina);
    }
}
