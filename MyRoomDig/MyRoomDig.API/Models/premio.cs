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
    
    public partial class premio
    {
        public int id { get; set; }
        public string descripcion { get; set; }
        public Nullable<decimal> costo { get; set; }
        public Nullable<int> idProveedor { get; set; }
        public int puntosRequeridos { get; set; }
        public Nullable<bool> activo { get; set; }
        public Nullable<bool> bono { get; set; }
        public Nullable<decimal> valorBono { get; set; }
        public byte[] imagen { get; set; }
    }
}
