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
    
    public partial class taxi
    {
        public int Id_taxi { get; set; }
        public Nullable<System.DateTime> fecha { get; set; }
        public string Id_Placa { get; set; }
        public Nullable<int> Id_Mesas { get; set; }
        public Nullable<int> maquina { get; set; }
        public string Nombre { get; set; }
        public string Usuario { get; set; }
        public string Admin { get; set; }
        public Nullable<int> Id_detestados { get; set; }
        public Nullable<int> Id_Pedidos { get; set; }
        public Nullable<decimal> monto { get; set; }
        public Nullable<int> ID { get; set; }
        public string CODEPAGO { get; set; }
        public Nullable<decimal> PAGO { get; set; }
        public string ESTADO_PAGO { get; set; }
        public Nullable<System.DateTime> FechaPago { get; set; }
        public Nullable<int> NumVisita { get; set; }
        public Nullable<bool> CAshDispenser { get; set; }
        public Nullable<int> TOTALVISITAS { get; set; }
        public Nullable<System.DateTime> fechaVence { get; set; }
    }
}
