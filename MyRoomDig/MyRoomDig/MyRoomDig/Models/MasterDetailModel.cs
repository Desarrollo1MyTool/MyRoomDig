namespace MyRoomDig.Models
{
    using System;
    using Xamarin.Forms;
    public class MasterDetailModel
    {
        public int IdOpcion { get; set; }
        public string ItemMenu { get; set; }
        public ImageSource Icon { get; set; }
        public Type TargetType { get; set; }
    }
}
