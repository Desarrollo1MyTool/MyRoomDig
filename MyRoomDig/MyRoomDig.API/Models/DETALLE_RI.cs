//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MyRoomDig.API.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class DETALLE_RI
    {
        public int ID_CABE_RI { get; set; }
        public int ITEM { get; set; }
        public Nullable<decimal> CANT_REQ { get; set; }
        public Nullable<int> ID_PRODUCTO_REQ { get; set; }
        public Nullable<int> ID_PRIORIDAD_RI { get; set; }
        public string DESCRIP { get; set; }
        public Nullable<int> ID_PROVEEDOR { get; set; }
        public Nullable<decimal> CANT_AUTOR { get; set; }
        public Nullable<int> ID_PRODUCTO_AUTOR { get; set; }
        public string DESCRIP_AUTOR { get; set; }
        public Nullable<int> ID_TRASLADO { get; set; }
        public Nullable<int> ID_CABE_OC { get; set; }
        public Nullable<decimal> CANT_TRASLA { get; set; }
        public Nullable<decimal> CANT_COMPRA { get; set; }
        public Nullable<System.DateTime> FEC_LIMITE { get; set; }
    }
}
