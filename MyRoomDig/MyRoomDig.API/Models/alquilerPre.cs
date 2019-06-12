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
    
    public partial class alquilerPre
    {
        public int id_temps { get; set; }
        public int id_servicios { get; set; }
        public int id_tipos { get; set; }
        public int id_suites { get; set; }
        public decimal tarifa { get; set; }
        public Nullable<int> persona { get; set; }
        public Nullable<short> maximo { get; set; }
        public Nullable<short> tiempo { get; set; }
        public Nullable<short> vl_iva { get; set; }
        public Nullable<bool> limites { get; set; }
        public Nullable<System.DateTime> hora_desde_1 { get; set; }
        public Nullable<System.DateTime> hora_hasta_1 { get; set; }
        public Nullable<System.DateTime> hora_desde_2 { get; set; }
        public Nullable<System.DateTime> hora_hasta_2 { get; set; }
        public Nullable<int> holgura { get; set; }
        public Nullable<bool> minima { get; set; }
        public Nullable<System.DateTime> salida { get; set; }
        public Nullable<bool> clave { get; set; }
        public Nullable<int> id_seradic { get; set; }
        public Nullable<int> id_medadic { get; set; }
        public Nullable<int> ctadic { get; set; }
        public Nullable<bool> noacumula { get; set; }
        public Nullable<bool> rotar { get; set; }
        public Nullable<int> id_tipiva { get; set; }
        public Nullable<int> id_prodexced { get; set; }
        public Nullable<int> id_mediexced { get; set; }
        public Nullable<int> ct_cantexced { get; set; }
        public Nullable<int> excedente { get; set; }
        public Nullable<decimal> seguro { get; set; }
        public Nullable<decimal> costo { get; set; }
        public Nullable<decimal> item1 { get; set; }
        public Nullable<decimal> item2 { get; set; }
        public Nullable<decimal> item3 { get; set; }
        public Nullable<int> ID_FIDELIZACION { get; set; }
        public Nullable<int> ID { get; set; }
        public Nullable<bool> rangos { get; set; }
        public Nullable<bool> personasTarifas { get; set; }
        public Nullable<bool> alimentacion { get; set; }
        public Nullable<bool> detalleProductos { get; set; }
        public Nullable<int> ID_PLANTILLAPUC { get; set; }
        public Nullable<int> ID_PLANTILLAALIM { get; set; }
        public Nullable<int> id_plan { get; set; }
        public Nullable<int> id_acomodacion { get; set; }
        public Nullable<int> avizar { get; set; }
    }
}
