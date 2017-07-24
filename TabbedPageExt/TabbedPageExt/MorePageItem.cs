using System;
using Xamarin.Forms;

namespace TabbedPageExt
{
    /// <summary>
    /// A class that encapsulates all the data and methods that are necessary to hold a TabPage on a MorePage.
    /// </summary>
    public class MorePageItem : BindableObject
    {
        /// <summary>
        /// BindableProperty for the Text property
        /// </summary>
        public static readonly BindableProperty TextProperty = BindableProperty.Create("Text", typeof(string), typeof(MorePageItem), null);
        /// <summary>
        /// A string holding the tabpage item's text
        /// </summary>
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        /// <summary>
        /// BindableProperty for the Icon property
        /// </summary>
        public static readonly BindableProperty IconProperty = BindableProperty.Create("Icon", typeof(FileImageSource), typeof(MorePageItem), default(FileImageSource));
        /// <summary>
        /// A FileImageSource defining the tabpage icon
        /// </summary>
        public FileImageSource Icon
        {
            get { return (FileImageSource)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        /// <summary>
        /// BindableProperty for the MoreListIcon property
        /// </summary>
        public static readonly BindableProperty MoreListIconProperty = BindableProperty.Create("MoreListIcon", typeof(FileImageSource), typeof(MorePageItem), default(FileImageSource));
        /// <summary>
        /// A FileImageSource defining the tabpage icon
        /// </summary>
        public FileImageSource MoreListIcon
        {
            get { return (FileImageSource)GetValue(MoreListIconProperty); }
            set { SetValue(MoreListIconProperty, value); }
        }

        internal static readonly BindableProperty IsEnabledProperty = BindableProperty.Create("IsEnabled", typeof(bool), typeof(MorePageItem), true);
        /// <summary>
        /// Property defining whether the item is enabled or not. 
        /// </summary>
        internal bool IsEnabled
        {
            get { return (bool)GetValue(IsEnabledProperty); }
            set { SetValue(IsEnabledProperty, value); }
        }

        /// <summary>
        /// A property to obtain the ImageSource for the right bracket icon
        /// </summary>
        public ImageSource RightBracketImage
        {
            get { return ImageSource.FromResource("TabbedPageExt.Resources.angle_bracket_right@2x.png"); }
        }

        /// <summary>
        /// Property to keep the item's TabPage
        /// </summary>
        public TabPage TabPage { get; private set; }

        /// <summary>
        /// The event handler to be called when the item is clicked
        /// </summary>
        public event EventHandler Clicked;

        /// <summary>
        /// MorePageItem constructor expecting a TabPage and an Action delegate function
        /// </summary>
        /// <param name="tabPage">The TabPage object to be assigned to the item.</param>
        /// <param name="activated">The Action delegate to be called when the item is clicked.</param>
        public MorePageItem(TabPage tabPage, Action activated)
        {
            TabPage = tabPage;
            Text = tabPage.Title;
            Icon = tabPage.Icon;
            MoreListIcon = (tabPage.MoreListIcon == default(FileImageSource)) ? tabPage.Icon : tabPage.MoreListIcon;
            Clicked += (s, e) => activated();
        }

        /// <summary>
        /// Method to be called from within the UI to indicate that the item is clicked. 
        /// </summary>
        public void OnClicked()
        {
            Clicked?.Invoke(this, EventArgs.Empty);
        }
    }
}
