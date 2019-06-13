namespace MyRoomDig.Domain
{
    using System.Collections.Generic;

    public class KeyValue
    {
        public int id { get; set; }
        public int idAux { get; set; }
        public string name { get; set; }
        public string nameAux { get; set; }
        public bool? valDefecto { get; set; }
        public byte[] Imagen { get; set; }
    }
}
