namespace MyRoomDig.Models
{
    using SQLite;
    public class SetupApp
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int IdMaquina { get; set; }
        public int IdApp { get; set; }
        public int IdLugar { get; set; }
        public string IntranetOrder { get; set; }
    }
}
