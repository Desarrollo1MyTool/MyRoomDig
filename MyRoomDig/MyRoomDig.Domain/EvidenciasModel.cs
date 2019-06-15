namespace MyRoomDig.Domain
{
    using System;
    public class EvidenciasModel
    {
        public System.Guid id { get; set; }
        public int idSerialNum { get; set; }
        public int? numIdenti { get; set; }
        public int? typeDoc { get; set; }
        public string name { get; set; }
        public string idClient { get; set; }
        public string codeTipoEvid { get; set; }
        public string nameClient { get; set; }
        public string codeCarpeta { get; set; }
        public byte[] evidencia1 { get; set; }
        public string descripcion { get; set; }
        public string fileName { get; set; }
        public Nullable<System.DateTime> fecha { get; set; }
        public string usuario { get; set; }
    }
}
