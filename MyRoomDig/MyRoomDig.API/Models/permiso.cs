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
    
    public partial class permiso
    {
        public int id { get; set; }
        public string usuario { get; set; }
        public string name { get; set; }
        public Nullable<int> nivel { get; set; }
        public Nullable<bool> temporal { get; set; }
    }
}
