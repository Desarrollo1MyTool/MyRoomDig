namespace MyRoomDig.Views
{
    using MyRoomDig.ViewModels;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MasterDetailPage : Xamarin.Forms.MasterDetailPage
    {
		public MasterDetailPage ()
		{
			InitializeComponent();
            App.MasterDetailPage = this;
            Detail = new NavigationPage(MainViewModel.GetInstance().MainDetailPage);
        }
	}
}