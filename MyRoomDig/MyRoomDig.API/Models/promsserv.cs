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
    
    public partial class promsserv
    {
        public int id { get; set; }
        public string name { get; set; }
        public Nullable<int> id_proms { get; set; }
        public Nullable<int> id_servicios { get; set; }
        public Nullable<int> id_tipos { get; set; }
        public Nullable<int> id_suites { get; set; }
        public Nullable<bool> producto { get; set; }
        public Nullable<decimal> dscto { get; set; }
        public Nullable<int> cant { get; set; }
        public Nullable<int> prodcant { get; set; }
        public Nullable<int> medcant { get; set; }
        public Nullable<bool> temporal { get; set; }
    }
}
