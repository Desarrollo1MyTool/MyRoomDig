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
    
    public partial class cuestionario_new
    {
        public int id { get; set; }
        public Nullable<int> id_pregunta { get; set; }
        public int respuesta { get; set; }
        public Nullable<System.DateTime> fecha { get; set; }
        public string usuario { get; set; }
        public Nullable<int> idHabitacion { get; set; }
    }
}
