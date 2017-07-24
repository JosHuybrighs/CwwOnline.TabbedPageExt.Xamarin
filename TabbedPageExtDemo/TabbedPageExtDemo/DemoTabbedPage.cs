using System;
using TabbedPageExt;
using Xamarin.Forms;

namespace TabbedPageExtDemo
{
    public class DemoTabbedPage: TabbedPageExt.TabbedPageExt
    {
        TabPage _tabPage3;
        TabPage _tabPage6;

        public DemoTabbedPage(bool useModeMorePage = true)
            : base(Device.RuntimePlatform == Device.Android ? IconColor.White : IconColor.Black)
        {
            MorePageTitle = "More";
            MorePagesMode = useModeMorePage ? MorePagesAccessMode.MorePage : MorePagesAccessMode.MorePopupMenu;
//            MaxTabs = 3;
//            MorePopupMenuMargin = new Thickness(0,0,0,0);
//            MorePopupMenuBackgroundColor = Color.FromHex("#e0e0e0");
//            MorePopupMenuItemSeperatorColor = Color.Gray;
//            MorePopupMenuItemTextColor = Color.FromHex("#303030");
//            MorePopupMenuItemSelectedBackgroundColor = Color.Teal;
//            MorePopupMenuItemSeperatorVisibility = SeparatorVisibility.Default;

            for (int i = 1; i < 9; ++i)
            {
                this.TabPages.Add(NewTabPage(i));
            }
        }

        TabPage NewTabPage(int pageNr)
        {
            if (pageNr == 1)
            {
                return new TabPage($"Tab: {pageNr}", "person_white.png", MorePagesMode == MorePagesAccessMode.MorePage ? "person_black.png" : "person_white.png",
                    // Page creation method: pass page type and a viewmodel
                    typeof(DemoContentPage), new DemoContentPageViewModel());
            }
            if (pageNr == 2)
            {
                return new TabPage($"Tab: {pageNr}", "person_white.png", MorePagesMode == MorePagesAccessMode.MorePage ? "person_black.png" : "person_white.png",
                    // Page creation method: pass page type and a viewmodel
                    typeof(HelloContentPage));
            }
            if (pageNr == 3)
            {
                return new TabPage($"Tab: {pageNr}", "person_white.png", MorePagesMode == MorePagesAccessMode.MorePage ? "person_black.png" : "person_white.png",
                    // Page creation method: pass a Func<> delegate method to create the page. Page is defined in XAML.
                    () =>
                    {
                        return (DemoContentPage)Activator.CreateInstance(typeof(DemoContentPage), new DemoContentPageViewModel());
                    });
            }
            if (pageNr == 7)
            {
                return new TabPage($"Tab: {pageNr}", "person_white.png", MorePagesMode == MorePagesAccessMode.MorePage ? "person_black.png" : "person_white.png",
                    // Page creation method: pass a Func<> delegate method to create the page. Page is defined using code.
                    () =>
                    {
                        var stackLayout = new StackLayout() { Margin = new Thickness(12,12,12,0) };
                        stackLayout.Children.Add(new Label() { Text = "This is a demo TabPage hosted by TabbedPageExt.", FontSize = 20 });
                        stackLayout.Children.Add(new Label() { Text = $"The page lets you remove tab page 6.", Margin = new Thickness(0, 12, 0, 0) });
                        if (_tabPage6 != null)
                        {
                            var button = new Button() { Text = "Remove tab page" };
                            button.Clicked += (s, e) =>
                            {
                                this.TabPages.Remove(_tabPage6);
                                button.IsEnabled = false;
                                _tabPage6 = null;
                            };
                            stackLayout.Children.Add(button);
                        }
                        else
                        {
                            stackLayout.Children.Add(new Label() { Text = "The tab page has already been removed.", TextColor = Color.Blue });
                        }
                        var contentPage = new ContentPage
                        {
                            Title = $"Tab: {pageNr}",
                            Content = stackLayout,
                            Icon = new FileImageSource() { File = "person_black.png" }
                        };
                        return contentPage;
                    });
            }
            if (pageNr == 8)
            {
                return new TabPage($"Tab: {pageNr}", "person_white.png", MorePagesMode == MorePagesAccessMode.MorePage ? "person_black.png" : "person_white.png",
                    // Page creation method: pass a Func<> delegate method to create the page. Page is defined using code.
                    () =>
                    {
                        var stackLayout = new StackLayout() { Margin = new Thickness(12,12,12,0) };
                        stackLayout.Children.Add(new Label() { Text = "This is a demo TabPage hosted by TabbedPageExt.", FontSize = 20 });
                        stackLayout.Children.Add(new Label() { Text = $"This page lets you remove tab page 3.", Margin = new Thickness(0, 12, 0, 0) });
                        if (_tabPage3 != null)
                        {
                            var button = new Button() { Text = "Remove tab page" };
                            button.Clicked += (s, e) =>
                            {
                                this.TabPages.Remove(_tabPage3);
                                button.IsEnabled = false;
                                _tabPage3 = null;
                            };
                            stackLayout.Children.Add(button);
                        }
                        else
                        {
                            stackLayout.Children.Add(new Label() { Text = "The tab page has already been removed.", TextColor = Color.Blue });
                        }
                        var contentPage = new ContentPage
                        {
                            Title = $"Tab: {pageNr}",
                            Content = stackLayout,
                            Icon = new FileImageSource() { File = "person_black.png" }
                        };
                        return contentPage;
                    });
            }
            var tabPage = new TabPage($"Tab: {pageNr}", "person_white.png", MorePagesMode == MorePagesAccessMode.MorePage ? "person_black.png" : "person_white.png",
                // Page creation method: pass a Func<> delegate method to create the page. Page is defined using code.
                () =>
                {
                    var stackLayout = new StackLayout() { Margin = new Thickness(12,12,12,0) };
                    stackLayout.Children.Add(new Label() { Text = "This is a demo TabPage hosted by TabbedPageExt.", FontSize = 20 });
                    stackLayout.Children.Add(new Label() { Text = "The page only has a 'Tap' button that opens a new 'details' page on the navigation stack.", Margin = new Thickness(0, 12, 0, 0) });
                    var button = new Button() { Text = "Tap" };
                    button.Clicked += (s, e) => Navigation.PushAsync(new ContentPage() { Title = $"Detail: {pageNr}" });
                    stackLayout.Children.Add(button);
                    var contentPage = new ContentPage
                    {
                        Title = $"Tab: {pageNr}",
                        Content = stackLayout,
                        Icon = new FileImageSource() { File = "person_black.png" }
                    };
                    return contentPage;
                });
            if (pageNr == 3) _tabPage3 = tabPage;
            if (pageNr == 6) _tabPage6 = tabPage;
            return tabPage;
        }
    }
}
