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
    
    public partial class plantilla_inven
    {
        public int id { get; set; }
        public string name { get; set; }
        public Nullable<int> id_bodegas { get; set; }
        public Nullable<bool> negativos { get; set; }
        public Nullable<int> id_permisos { get; set; }
        public Nullable<int> id_PlantillaSource { get; set; }
        public Nullable<int> id_BodegaSource { get; set; }
        public Nullable<int> id_imprime { get; set; }
        public Nullable<int> id_copia { get; set; }
    }
}
