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
    
    public partial class detfactura
    {
        public int tipo { get; set; }
        public string prefijo { get; set; }
        public int id_facturas { get; set; }
        public int id_items { get; set; }
        public int CONSEC { get; set; }
        public Nullable<bool> abono { get; set; }
        public int id_frmpagos { get; set; }
        public Nullable<int> id_bancos { get; set; }
        public Nullable<decimal> pago { get; set; }
        public Nullable<decimal> valor { get; set; }
        public string documento { get; set; }
        public string autoriza { get; set; }
        public string noid { get; set; }
        public string nota { get; set; }
        public Nullable<int> cliente { get; set; }
        public Nullable<decimal> propina { get; set; }
        public string usuario { get; set; }
        public Nullable<int> maquina { get; set; }
        public Nullable<decimal> monto_iva { get; set; }
        public Nullable<System.DateTime> FEC_GRABA { get; set; }
        public Nullable<int> IdRecCaja { get; set; }
        public Nullable<int> id_clase_pago { get; set; }
        public Nullable<int> ID_DETESTADOS { get; set; }
        public string USUARIO_CRUZA { get; set; }
        public Nullable<System.DateTime> FEC_CRUZA { get; set; }
        public string USUARIO_ANULA { get; set; }
        public Nullable<System.DateTime> FEC_ANULA { get; set; }
        public string codeRecupera { get; set; }
        public Nullable<int> IdApp { get; set; }
        public Nullable<System.DateTime> fecTransac { get; set; }
    
        public virtual banco banco { get; set; }
        public virtual frmpago frmpago { get; set; }
    }
}
