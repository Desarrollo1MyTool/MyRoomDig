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
    
    public partial class reservas_huesped
    {
        public int Id_reservas { get; set; }
        public int Id_empresas { get; set; }
        public string Id_clientes { get; set; }
        public Nullable<int> Id_suites { get; set; }
        public Nullable<int> Id_mesas { get; set; }
        public Nullable<System.DateTime> Fec_entrada { get; set; }
        public Nullable<System.DateTime> Fec_salida { get; set; }
        public string Nombre { get; set; }
        public string Empresa { get; set; }
    }
}
