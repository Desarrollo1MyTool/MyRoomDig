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
    
    public partial class ctrmovi
    {
        public int id { get; set; }
        public Nullable<int> id_pedidos { get; set; }
        public Nullable<int> id_mecanismos { get; set; }
        public Nullable<System.DateTime> fecha_ini { get; set; }
        public Nullable<System.DateTime> fecha_fin { get; set; }
        public Nullable<int> minutos { get; set; }
        public Nullable<bool> prendido { get; set; }
        public Nullable<int> id_mensaje { get; set; }
    }
}
