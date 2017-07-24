using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Extensions;
using System.Collections.ObjectModel;
using Xamarin.Forms.Xaml;
using Xamarin.Forms;

namespace TabbedPageExt
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MorePopupMenuPage : PopupPage
    {
        public static readonly BindableProperty MenuBackgroundColorProperty = BindableProperty.Create("MenuBackgroundColor", typeof(Color), typeof(MorePopupMenuPage), Color.FromHex("#303030"), propertyChanged: OnMenuBackgroundColorChanged);
        public Color MenuBackgroundColor
        {
            get { return (Color)GetValue(MenuBackgroundColorProperty); }
            set { SetValue(MenuBackgroundColorProperty, value); }
        }
        private static void OnMenuBackgroundColorChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var page = bindable as MorePopupMenuPage;
            page.MenuItemsListView.BackgroundColor = (Color)newvalue;
        }

        public static readonly BindableProperty MenuItemTextColorProperty = BindableProperty.Create("MenuItemTextColor", typeof(Color), typeof(MorePopupMenuPage), Color.White);
        public Color MenuItemTextColor
        {
            get { return (Color)GetValue(MenuItemTextColorProperty); }
            set { SetValue(MenuItemTextColorProperty, value); }
        }

        public static readonly BindableProperty MenuItemSeperatorColorProperty = BindableProperty.Create("MenuItemSeperatorColor", typeof(Color), typeof(MorePopupMenuPage), Color.FromHex("#9C969C"), propertyChanged: OnSeperatorColorChanged);
        public Color MenuItemSeperatorColor
        {
            get { return (Color)GetValue(MenuItemSeperatorColorProperty); }
            set { SetValue(MenuItemSeperatorColorProperty, value); }
        }
        private static void OnSeperatorColorChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var page = bindable as MorePopupMenuPage;
            page.MenuItemsListView.SeparatorColor = (Color)newvalue;
        }

        public static readonly BindableProperty MenuItemSeperatorVisibilityProperty = BindableProperty.Create("MenuItemSeperatorVisibility", typeof(SeparatorVisibility), typeof(MorePopupMenuPage), SeparatorVisibility.Default, propertyChanged: OnSeperatorVisibilityChanged);
        public SeparatorVisibility MenuItemSeperatorVisibility
        {
            get { return (SeparatorVisibility)GetValue(MenuItemSeperatorVisibilityProperty); }
            set { SetValue(MenuItemSeperatorVisibilityProperty, value); }
        }
        private static void OnSeperatorVisibilityChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var page = bindable as MorePopupMenuPage;
            page.MenuItemsListView.SeparatorVisibility = (SeparatorVisibility)newvalue;
        }


        public static readonly BindableProperty MenuItemSelectedBackgroundColorProperty = BindableProperty.Create("MenuItemSelectedBackgroundColor", typeof(Color), typeof(MorePopupMenuPage), Color.FromHex("#707070"));
        public Color MenuItemSelectedBackgroundColor
        {
            get { return (Color)GetValue(MenuItemSelectedBackgroundColorProperty); }
            set { SetValue(MenuItemSelectedBackgroundColorProperty, value); }
        }

        public MorePopupMenuPage(ObservableCollection<MorePageItem> menuItems,
                                 Thickness margin,
                                 double menuItemHeight)
        {
            InitializeComponent();
            this.ContentStackLayout.Padding = margin;
            this.MenuItemsListView.ItemsSource = menuItems;
            this.MenuItemsListView.ItemSelected += MenuItemsListView_ItemSelected;
            this.MenuItemsListView.HeightRequest = menuItemHeight * menuItems.Count;
            this.MenuItemsListView.SeparatorVisibility = MenuItemSeperatorVisibility;
            this.MenuItemsListView.SeparatorColor = MenuItemSeperatorColor;
            this.MenuItemsListView.BackgroundColor = MenuBackgroundColor;
        }

        private async void MenuItemsListView_ItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            // Close popup
            await Navigation.PopPopupAsync();
            // Execute action
            var item = e.SelectedItem as MorePageItem;
            item.OnClicked();
        }
    }
}