namespace MyRoomDig.Domain
{
    using System;
    using Xamarin.Forms;
    public class EvidenciasModel
    {
        public System.Guid id { get; set; }
        public bool holder { get; set; }
        public int idSerialNum { get; set; }
        public int idTercero { get; set; }
        public int? typeDoc { get; set; }
        public string sigla { get; set; }
        public string firstName { get; set; }
        public string secondName { get; set; }
        public string firstLastName { get; set; }
        public string secondLastName { get; set; }
        public string identifClient { get; set; }
        public string codeTipoEvid { get; set; }
        public string nameClient { get; set; }
        public string codeCarpeta { get; set; }
        public byte[] evidencia1 { get; set; }
        public string descripcion { get; set; }
        public string fileName { get; set; }
        public Nullable<System.DateTime> fecha { get; set; }
        public string usuario { get; set; }
        public ImageSource image { get; set; }
    }
}
