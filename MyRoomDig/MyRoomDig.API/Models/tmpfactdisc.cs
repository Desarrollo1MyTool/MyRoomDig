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
    
    public partial class tmpfactdisc
    {
        public int maquina { get; set; }
        public int factura { get; set; }
        public Nullable<System.DateTime> fecha { get; set; }
        public string ident { get; set; }
        public string cliente { get; set; }
        public Nullable<decimal> ServBase { get; set; }
        public Nullable<decimal> ServIva { get; set; }
        public Nullable<decimal> FoodBase { get; set; }
        public Nullable<decimal> FoodIva { get; set; }
        public Nullable<decimal> DrinkBase { get; set; }
        public Nullable<decimal> DrinkIva { get; set; }
        public Nullable<decimal> OtroBase { get; set; }
        public Nullable<decimal> OtroIva { get; set; }
        public Nullable<decimal> FactBase { get; set; }
        public Nullable<decimal> FactIva { get; set; }
        public Nullable<decimal> total { get; set; }
        public Nullable<decimal> Pagos { get; set; }
        public Nullable<decimal> cartera { get; set; }
        public string estado { get; set; }
        public Nullable<decimal> PagosEfect { get; set; }
        public Nullable<decimal> PagosDebit { get; set; }
        public Nullable<decimal> PagosCredit { get; set; }
        public Nullable<decimal> PagosOtros { get; set; }
        public Nullable<decimal> DESCUENTO { get; set; }
    }
}
