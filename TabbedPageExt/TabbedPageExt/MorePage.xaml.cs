using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TabbedPageExt
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MorePage : ContentPage
    {
        public ObservableCollection<MorePageItem> MorePageItems { get; private set; }

        public MorePage(string title)
        {
            Title = title;
            MorePageItems = new ObservableCollection<MorePageItem>();
            InitializeComponent();
            ChildrenView.ItemsSource = MorePageItems;
            ChildrenView.ItemSelected += ChildrenView_ItemSelected;
        }

        private void ChildrenView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            MorePageItem item = e.SelectedItem as MorePageItem;
            if (item != null)
            {
                // Tell PageItem to execute the registered Action (typically: open page). 
                item.OnClicked();
            }
            ChildrenView.SelectedItem = null;
        }
    }
}