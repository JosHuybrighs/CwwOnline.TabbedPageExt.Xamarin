using System;
using Xamarin.Forms;

namespace TabbedPageExtDemo
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
//            GC.Collect();
            base.OnAppearing();
        }

        private void Button_Clicked_ModeMore(object sender, EventArgs e)
        {
            var navigationPage = App.Current.MainPage as NavigationPage;
            var tabbedPage = new DemoTabbedPage();
            navigationPage.PushAsync(tabbedPage);
        }

        private void Button_Clicked_ModeToolbar(object sender, EventArgs e)
        {
            var navigationPage = App.Current.MainPage as NavigationPage;
            var tabbedPage = new DemoTabbedPage(false);
            navigationPage.PushAsync(tabbedPage);
        }
    }
}
