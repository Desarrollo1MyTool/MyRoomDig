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
    
    public partial class prom
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public prom()
        {
            this.promsclases = new HashSet<promsclase>();
            this.promsgrupoes = new HashSet<promsgrupo>();
            this.promsprods = new HashSet<promsprod>();
            this.promsseccs = new HashSet<promssecc>();
            this.promstimes = new HashSet<promstime>();
        }
    
        public int id { get; set; }
        public string name { get; set; }
        public Nullable<int> tipo { get; set; }
        public Nullable<bool> activo { get; set; }
        public Nullable<int> nivel { get; set; }
        public Nullable<int> canttodo { get; set; }
        public Nullable<bool> dia { get; set; }
        public Nullable<bool> hora { get; set; }
        public Nullable<bool> producto { get; set; }
        public Nullable<bool> clase { get; set; }
        public Nullable<bool> grupo { get; set; }
        public Nullable<bool> seccion { get; set; }
        public Nullable<bool> Nuevo { get; set; }
        public Nullable<int> cantidad { get; set; }
        public Nullable<decimal> dscto { get; set; }
        public Nullable<bool> todo { get; set; }
        public Nullable<bool> pendiente { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<promsclase> promsclases { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<promsgrupo> promsgrupoes { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<promsprod> promsprods { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<promssecc> promsseccs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<promstime> promstimes { get; set; }
    }
}
