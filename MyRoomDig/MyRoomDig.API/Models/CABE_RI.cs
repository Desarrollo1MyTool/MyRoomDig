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
    
    public partial class CABE_RI
    {
        public int ID_RI { get; set; }
        public Nullable<int> ID_CENTROC { get; set; }
        public Nullable<System.DateTime> FECHA_SOLICITUD { get; set; }
        public string USUARIO { get; set; }
        public string TIEMPOPARA { get; set; }
        public Nullable<int> ID_TIEMPO_RI { get; set; }
        public Nullable<System.DateTime> FECHA_REVISION { get; set; }
        public Nullable<int> ID_ESTADOS_RI { get; set; }
        public string OBSERVACION { get; set; }
        public Nullable<System.DateTime> FECHA_APROBA { get; set; }
    }
}
