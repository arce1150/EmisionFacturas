using System;
using System.Collections.Generic; 
namespace Facturacion.Utilitarios
{
    public class jPagination<T>
    {
        public int total { get; set; }
        public List<T> rows { get; set; }
    }
}
