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
    
    public partial class mesa
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public mesa()
        {
            this.olvidos = new HashSet<olvido>();
        }
    
        public int id { get; set; }
        public string name { get; set; }
        public Nullable<int> id_orden { get; set; }
        public Nullable<int> id_orden222 { get; set; }
        public Nullable<int> bajar { get; set; }
        public Nullable<int> derecha { get; set; }
        public Nullable<int> casilla { get; set; }
        public Nullable<int> salto { get; set; }
        public Nullable<int> id_zona { get; set; }
        public Nullable<int> id_suites { get; set; }
        public Nullable<int> id_estado { get; set; }
        public Nullable<int> id_pedido { get; set; }
        public Nullable<int> id_pedido_ult { get; set; }
        public Nullable<int> id_tempo { get; set; }
        public Nullable<int> id_mesero { get; set; }
        public string descrip { get; set; }
        public string ubica { get; set; }
        public Nullable<int> minimo { get; set; }
        public Nullable<int> maximo { get; set; }
        public Nullable<bool> cuenta { get; set; }
        public Nullable<bool> ver { get; set; }
        public Nullable<int> id_forma { get; set; }
        public Nullable<int> id_forma_ant { get; set; }
        public Nullable<int> mensaje { get; set; }
        public Nullable<bool> temporal { get; set; }
        public Nullable<int> id_bodega { get; set; }
        public Nullable<bool> rotar { get; set; }
        public Nullable<bool> limpia { get; set; }
        public Nullable<bool> recorda { get; set; }
        public Nullable<bool> factura { get; set; }
        public Nullable<bool> marcar { get; set; }
        public Nullable<bool> taxi { get; set; }
        public string admon { get; set; }
        public Nullable<System.DateTime> fecmarcar { get; set; }
        public Nullable<System.DateTime> fectaxi { get; set; }
        public Nullable<System.DateTime> fecfactura { get; set; }
        public Nullable<System.DateTime> fecadmon { get; set; }
        public Nullable<bool> BloAseo { get; set; }
        public Nullable<System.DateTime> FecBloAseo { get; set; }
        public Nullable<System.DateTime> FecAbierta { get; set; }
        public Nullable<int> Id_Aseo { get; set; }
        public Nullable<bool> Agua { get; set; }
        public Nullable<bool> Luz { get; set; }
        public Nullable<bool> Desague { get; set; }
        public Nullable<int> id_camarera { get; set; }
        public Nullable<int> id_alertas { get; set; }
        public Nullable<int> Id_CtrlCama { get; set; }
        public string VIDEO { get; set; }
        public Nullable<int> DESAG { get; set; }
        public Nullable<bool> EVENTOS { get; set; }
        public Nullable<int> adultos { get; set; }
        public Nullable<int> niños { get; set; }
        public Nullable<int> Bebes { get; set; }
        public Nullable<int> APP { get; set; }
        public Nullable<bool> NEGADOR { get; set; }
        public Nullable<int> timeGaraje { get; set; }
        public Nullable<bool> habIn { get; set; }
        public Nullable<bool> habOut { get; set; }
        public Nullable<int> maxAdulAdic { get; set; }
        public Nullable<int> maxNinoAdic { get; set; }
        public Nullable<bool> Complem { get; set; }
        public Nullable<bool> HouseUse { get; set; }
        public Nullable<int> paxOferta { get; set; }
        public Nullable<System.DateTime> fecAvizaAudit { get; set; }
        public string dotacion { get; set; }
        public Nullable<int> idMto { get; set; }
        public Nullable<bool> negadorAux { get; set; }
        public Nullable<int> id_prv { get; set; }
        public Nullable<System.DateTime> finFechaPulsaChapa { get; set; }
        public string kRoomHost { get; set; }
        public string kLinHost { get; set; }
        public string kCode { get; set; }
        public Nullable<bool> supervisor { get; set; }
        public Nullable<bool> decoracion { get; set; }
        public Nullable<bool> superIn { get; set; }
        public Nullable<bool> grupo { get; set; }
        public Nullable<bool> siBaucher { get; set; }
        public string codeReserva { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<olvido> olvidos { get; set; }
        public virtual mesero mesero { get; set; }
        public virtual suite suite { get; set; }
    }
}
