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
    
    public partial class tmpconsumo
    {
        public int maquina { get; set; }
        public int habitacion { get; set; }
        public string placa { get; set; }
        public Nullable<decimal> servicio { get; set; }
        public Nullable<decimal> consumo { get; set; }
        public string cortesia { get; set; }
        public Nullable<decimal> total { get; set; }
        public Nullable<decimal> pendiente { get; set; }
        public Nullable<decimal> facturado { get; set; }
        public Nullable<int> controles { get; set; }
        public Nullable<System.DateTime> Entrada { get; set; }
        public Nullable<System.DateTime> Salida { get; set; }
        public Nullable<decimal> Abonos { get; set; }
        public Nullable<decimal> Saldo { get; set; }
        public Nullable<decimal> adultos { get; set; }
        public Nullable<decimal> ninos { get; set; }
        public Nullable<decimal> bebes { get; set; }
        public Nullable<decimal> servpend { get; set; }
        public Nullable<decimal> conspend { get; set; }
        public Nullable<int> ID_PEDIDO { get; set; }
        public Nullable<int> cantAlim { get; set; }
    }
}
