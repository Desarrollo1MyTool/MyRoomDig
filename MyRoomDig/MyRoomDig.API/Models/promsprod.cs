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
    
    public partial class promsprod
    {
        public int id_proms { get; set; }
        public int id_productos { get; set; }
        public int id_detmedidas { get; set; }
        public Nullable<int> cantidad { get; set; }
    
        public virtual detmedida detmedida { get; set; }
        public virtual producto producto { get; set; }
        public virtual prom prom { get; set; }
    }
}
