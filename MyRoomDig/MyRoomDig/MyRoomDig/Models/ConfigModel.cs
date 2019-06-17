namespace MyRoomDig.Models
{
    using SQLite.Net.Attributes;
    public class ConfigModel
    {
        [PrimaryKey]
        public int Id { get; set; }
        public string HostIP { get; set; }
        public int HostPort { get; set; }
        public string Maquina { get; set; }
    }
}
