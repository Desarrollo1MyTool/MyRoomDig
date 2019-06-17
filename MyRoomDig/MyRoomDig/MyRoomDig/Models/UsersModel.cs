namespace MyRoomDig.Models
{
    using SQLite.Net.Attributes;
    public class UsersModel
    {
        [PrimaryKey]
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
    }
}
