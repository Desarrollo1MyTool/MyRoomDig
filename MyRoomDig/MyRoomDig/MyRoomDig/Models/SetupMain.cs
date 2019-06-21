namespace MyRoomDig.Models
{
    using SQLite;
    public class SetupMain
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string NameOption { get; set; }
        public string Icon { get; set; }
        public int IdOption { get; set; }
        public bool IsMain { get; set; }
        public bool IsVisible { get; set; }
    }
}
