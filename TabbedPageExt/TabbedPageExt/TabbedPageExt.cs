using Xamarin.Forms;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;

namespace TabbedPageExt
{
    /// <summary>
    /// A TabbedPage supporting UI virtualization and an iOS-like more tab page across all platforms. 
    /// </summary>
    public class TabbedPageExt : TabbedPage
    {
        /// <summary>
        /// An enum defining how excessive pages can be revealed
        /// </summary>
        public enum MorePagesAccessMode
        {
            /// <summary>
            /// Excessive pages are hidden in a MorePage view (as in iOS)
            /// </summary>
            MorePage,
            /// <summary>
            /// Excessive pages are hidden in a DropDown list available as a More toolbar icon (as in android)
            /// </summary>
            MorePopupMenu
        }

        /// <summary>
        /// An enum to choose between a white or a black More toolbar icon on Android.
        /// </summary>
        public enum IconColor
        {
            /// <summary>
            /// White toolbar icon on android.
            /// </summary>
            White,
            /// <summary>
            /// Black toolbar icon on android.
            /// </summary>
            Black
        }

        /// <summary>
        /// List holding the tab pages of TabbedPageExt.
        /// </summary>
        public ObservableCollection<TabPage> TabPages { get; private set; }

        int _maxTabs = 4;
        /// <summary>
        /// The maximum number of tabs to be shown on the TabbedPage. Default = 4.
        /// </summary>
        public int MaxTabs
        {
            get { return _maxTabs; }
            set { _maxTabs = value; }
        }

        /// <summary>
        /// Defines the MorePagesAccessMode.
        /// Default = MorePage.
        /// </summary>
        public MorePagesAccessMode MorePagesMode { get; set; }

        /// <summary>
        /// Set this property to True when you don't want the first hidden tab page to be shown in case a
        /// visible page is removed.
        /// Default = false.
        /// </summary>
        public bool DontMovePages { get; set; }

        /// <summary>
        /// A string for the "More" text presented on the More tab (iOS) or the More toolbar icon (other platforms). 
        /// </summary>
        public string MorePageTitle { get; set; }

        /// <summary>
        /// Icon string for the More toolbar icon. When set it overrides the default icon provided by the plugin.
        /// </summary>
        public string MoreToolbarIcon { get; set; }

        public Thickness MorePopupMenuMargin { get; set; }

        /// <summary>
        /// Background color of the 'More' popup menu
        /// </summary>
        public Color MorePopupMenuBackgroundColor { get; set; }

        /// <summary>
        /// Text color of the text in the 'More' popup menu
        /// </summary>
        public Color MorePopupMenuItemTextColor { get; set; }

        /// <summary>
        /// Indicates if menuitems have a seperator or not
        /// </summary>
        public SeparatorVisibility MorePopupMenuItemSeperatorVisibility { get; set; }

        /// <summary>
        /// Color of menu seperator lines in a 'More' popup menu
        /// </summary>
        public Color MorePopupMenuItemSeperatorColor { get; set; }

        /// <summary>
        /// Background color of a selected item in the 'More' popup menu
        /// </summary>
        public Color MorePopupMenuItemSelectedBackgroundColor { get; set; }

        public double MorePopupMenuItemHeigth { get; set; }

        ObservableCollection<MorePageItem> _morePopupMenuItems = new ObservableCollection<MorePageItem>();
        public ObservableCollection<MorePageItem> MorePopupMenuItems
        {
            get { return _morePopupMenuItems; }
        }


        MorePage _morePage;
        ToolbarItem _moreToolbarItem;
        TabPage _activeTabPage;

        /// <summary>
        /// Constructor for TabbedPageExt. You can pass an optional IconColor value specifying whether
        /// the 'more' toolbar icon is black or white. On iOS it comes as any other iOS icon in the toolbar (e.g. blue).
        /// </summary>
        /// <param name="androidMoreIconColor">Color of the More toolbar icon for Android</param>
        /// <param name="winMoreIconColor">Color of the More toolbar icon for UWP</param>
        public TabbedPageExt(IconColor moreIconColor = IconColor.White)
        {
            double topPadding = Device.RuntimePlatform == Device.iOS ? 44 : 56;
            MorePopupMenuMargin = new Thickness(0, topPadding, 8, 0);
            MorePopupMenuItemHeigth = (Device.RuntimePlatform == Device.iOS) ? 44 : (Device.RuntimePlatform == Device.Android) ? 45 : 33;
            MoreToolbarIcon = moreIconColor == IconColor.White ? "tabbedpageext_more_vert_white.png" : "tabbedpageext_more_vert_black.png";
            MorePopupMenuBackgroundColor = Color.FromHex("#303030");
            MorePopupMenuItemTextColor = Color.White;
            MorePopupMenuItemSeperatorColor = Color.FromHex("#9C969C");
            MorePopupMenuItemSelectedBackgroundColor = Color.FromHex("#707070");

            TabPages = new ObservableCollection<TabPage>();
            TabPages.CollectionChanged += TabPages_CollectionChanged;
        }

        /// <summary>
        /// Invoked when a new TabPage becomes active.
        /// </summary>
        protected override void OnCurrentPageChanged()
        {
            base.OnCurrentPageChanged();

            if (_activeTabPage != null)
            {
                _activeTabPage.Hide();
                _activeTabPage = null;
            }
            if (this.CurrentPage != null)
            {
                if (Device.RuntimePlatform == Device.iOS)
                {
                    this.Title = this.CurrentPage.Title;
                }
                _activeTabPage = this.CurrentPage as TabPage;
                if (_activeTabPage != null)
                {
                    _activeTabPage.Show(_moreToolbarItem);
                }
            }
        }

        void SetMoreToolbarItem(ToolbarItem toolbarItem)
        {
            _moreToolbarItem = toolbarItem;
            if (_activeTabPage != null)
            {
                _activeTabPage.ToolbarItems.Add(_moreToolbarItem);
            }
        }

        void ResetMoreToolbarItem()
        {
            _moreToolbarItem = null;
        }

        private void TabPages_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    {
                        foreach (var item in e.NewItems)
                        {
                            TabPage tabPage = item as TabPage;
                            if (TabPages.Count < MaxTabs + 1)
                            {
                                this.Children.Add(tabPage);
                            }
                            else
                            {
                                // Can't show the page
                                // Add page as a child to MorePage
                                MorePageItem pageItem = new MorePageItem(tabPage,
                                    () =>
                                    {
                                        ContentPage newPage = tabPage.CreateContentPageInstance();
                                        Navigation.PushAsync(newPage);
                                    });
                                // Check if it must be presented in 'MorePage' or in a dropdown list
                                if (MorePagesMode == MorePagesAccessMode.MorePage)
                                {
                                    // Must be shown under 'More' page list.
                                    if (TabPages.Count == MaxTabs + 1)
                                    {
                                        // This is the first page to be added.
                                        // Add a MorePage tabPage in case of iOS and a MorePage toolbaricon otherwise
                                        string moreTitle = string.IsNullOrEmpty(MorePageTitle) ? "More" : MorePageTitle;
                                        _morePage = new MorePage(moreTitle);
                                        if (Device.RuntimePlatform == Device.iOS)
                                        {
                                            this.Children.Add(_morePage);
                                        }
                                        else
                                        {
                                            var moreToolbarItem = new ToolbarItem(moreTitle, MoreToolbarIcon,
                                                () =>
                                                {
                                                    Navigation.PushAsync(_morePage);
                                                }, Device.RuntimePlatform == Device.Android ? ToolbarItemOrder.Primary : ToolbarItemOrder.Primary, 100);
                                            SetMoreToolbarItem(moreToolbarItem);
                                        }
                                    }
                                    _morePage.MorePageItems.Add(pageItem);
                                }
                                else
                                {
                                    // Must be shown in the 'More' popupmenu list.
                                    if (TabPages.Count == MaxTabs + 1)
                                    {
                                        // This is the first page to be added.
                                        // Add a 'More' popupmenu toolbaricon
                                        string moreTitle = string.IsNullOrEmpty(MorePageTitle) ? "More" : MorePageTitle;
                                        var moreToolbarItem = new ToolbarItem(moreTitle, MoreToolbarIcon,
                                            async () =>
                                            {
                                                // Show PopupMenu
                                                if (_activeTabPage != null &&
                                                    _activeTabPage.ContentBaseLayout != null)
                                                {
                                                    try
                                                    {
                                                        var menuPopup = new MorePopupMenuPage(MorePopupMenuItems,
                                                                                              MorePopupMenuMargin,
                                                                                              MorePopupMenuItemHeigth)
                                                        {
                                                            MenuItemSelectedBackgroundColor = MorePopupMenuItemSelectedBackgroundColor,
                                                            MenuItemTextColor = MorePopupMenuItemTextColor,
                                                            MenuItemSeperatorColor = MorePopupMenuItemSeperatorColor,
                                                            MenuItemSeperatorVisibility = MorePopupMenuItemSeperatorVisibility,
                                                            MenuBackgroundColor = MorePopupMenuBackgroundColor,
                                                        };
                                                        await Navigation.PushPopupAsync(menuPopup);
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        throw new Exception("Please install nuget package rg.plugins.popup");
                                                    }
                                                }

                                            }, ToolbarItemOrder.Primary, 100);
                                        SetMoreToolbarItem(moreToolbarItem);
                                    }
                                    MorePopupMenuItems.Add(pageItem);
                                }
                            }
                        }
                        break;
                    }
                case NotifyCollectionChangedAction.Remove:
                    int pageStartIdx = e.OldStartingIndex;
                    foreach (var item in e.OldItems)
                    {
                        TabPage tabPage = item as TabPage;
                        // Check if removed page was visible
                        if (this.Children.Contains(tabPage))
                        {
                            // Yes
                            this.Children.Remove(tabPage);
                            int mTabs = (Device.RuntimePlatform == Device.iOS) ? MaxTabs + 1 : MaxTabs;
                            if (!DontMovePages &&
                                this.Children.Count < mTabs)
                            {
                                // We can bring the first hidden page to the main view.
                                // Check if this page is under "MorePage" or under "MorePopupMenu"
                                if (MorePagesMode == MorePagesAccessMode.MorePage)
                                {
                                    // Page is under 'More'.
                                    // Remove it under "More"
                                    _morePage.MorePageItems.RemoveAt(0);
                                    // Check if there are still other pages under "More"
                                    if (_morePage.MorePageItems.Count == 0)
                                    {
                                        // No: Remove MorePage tabPage in case of iOS and the MorePage toolbar item otherwise
                                        if (Device.RuntimePlatform == Device.iOS)
                                        {
                                            this.Children.Remove(_morePage);
                                            _morePage = null;
                                        }
                                        else
                                        {
                                            ResetMoreToolbarItem();
                                        }
                                    }
                                }
                                else
                                {
                                    // First hidden page is in MorePopupMenu
                                    MorePopupMenuItems.RemoveAt(0);
                                    // Check if there are still other pages under "More"
                                    if (MorePopupMenuItems.Count == 0)
                                    {
                                        // No: Remove the MorePopupMenu toolbar item
                                        ResetMoreToolbarItem();
                                    }
                                }
                                // Bring in the hidden tab page
                                var hiddenPage = this.TabPages[MaxTabs - 1];
                                int insertIndex = (Device.RuntimePlatform == Device.iOS) ? MaxTabs - 1 : MaxTabs;
                                this.Children.Insert(insertIndex, hiddenPage);
                            }
                        }
                        else if (_morePage != null)
                        {
                            // No, removed page was under "MorePage" 
                            var itemPage = _morePage.MorePageItems.FirstOrDefault(i => i.TabPage == tabPage);
                            if (itemPage != null)
                            {
                                _morePage.MorePageItems.Remove(itemPage);
                            }
                            if (TabPages.Count == MaxTabs + 1)
                            {
                                if (Device.RuntimePlatform == Device.iOS)
                                {
                                    this.Children.Remove(_morePage);
                                    _morePage = null;
                                }
                                else
                                {
                                    ResetMoreToolbarItem();
                                }
                            }
                        }
                        else
                        {
                            // No, removed page was under MorePopupMenu
                            var entry = MorePopupMenuItems.FirstOrDefault(p => p.TabPage == tabPage);
                            if (entry != null)
                            {
                                MorePopupMenuItems.Remove(entry);
                                // Remove MorePopupMenu toolbar item if there are no pages anymore in MorePopupMenu
                                if (MorePopupMenuItems.Count == 0)
                                {
                                    ResetMoreToolbarItem();
                                }
                            }
                        }
                    }
                    break;
                case NotifyCollectionChangedAction.Reset:
                    this.Children.Clear();
//                    this.ToolbarItems.Clear();
                    this.MorePopupMenuItems.Clear();
                    if (_morePage != null)
                    {
                        _morePage.MorePageItems.Clear();
                    }
                    _moreToolbarItem = null;
                    _morePage = null;
                    break;
            }
        }
    }
}
