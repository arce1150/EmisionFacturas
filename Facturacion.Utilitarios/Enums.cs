using System.ComponentModel;

namespace Facturacion.Utilitarios
{ 
    public enum CatalogosSunat : int
    {
        [Description("Tipo de Operación")]
        TIPOOPERACION_CATALOGO51 = 51,
        [Description("Tipos de Documentos de Identidad")]
        TIPODOCUMENTOIDENTIDAD_CATALOGO6 = 6,
        [Description("Tipo de Monedas")]
        TIPOMONEDA_CATALOGO2 = 2,
        [Description("Unidad de Medida Comercial")]
        UNIDADMEDIDA_CATALOGO3 =3,
        [Description("Tipos de Tributos")]
        TIPOSDETRIBUTO_CATALOGO5=5,
        [Description("Tipos de Sistema de Cálculo del ISC")]
        TIPOCALCULOISC_CATALOGO8 =8,
        [Description("Tipo de Afectación del IGV")]
        TIPOAFECTACIONIGV_CATALOGO7 = 7,
        [Description("Tipo de Producto:Bien, servicio")]
        TIPOPRODUCTO_CATALOGO100 = 100
    }
    public enum TipoComprobante : int {
        Factura=1,
        Boleta=3,
        FacturaId= 235,
        ValorDNI= 128,
        ValorRUC= 130
    }   
}
