using CarApp.Maui.ViewModel;

namespace CarApp.Maui
{
    public partial class MainPage : ContentPage
    {
        public MainPage(CarListViewModel carListViewModel)
        {
            InitializeComponent();
            BindingContext = carListViewModel;
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {

        }
    }

}
