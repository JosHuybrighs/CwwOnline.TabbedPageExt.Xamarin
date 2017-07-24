using System;
using Xamarin.Forms;

namespace TabbedPageExt
{
    public class TabPage : ContentPage, IDisposable
    {
        public static readonly BindableProperty MoreListIconProperty = BindableProperty.Create("MoreListIcon", typeof(FileImageSource), typeof(TabPage), default(FileImageSource));
        public FileImageSource MoreListIcon
        {
            get { return (FileImageSource)GetValue(MoreListIconProperty); }
            set { SetValue(MoreListIconProperty, value); }
        }


        Type _viewPageType;
        object _viewPageViewModel;
        ContentPage _contentPage;

        public Func<ContentPage> CreateContentPage { get; private set; }

        public RelativeLayout ContentBaseLayout { get; private set; }

        /// <summary>
        /// Construct TabPage with a title and icon, for a given page type and page viewmodel 
        /// </summary>
        /// <param name="title">The title of the TabPage.</param>
        /// <param name="pageIcon">A string defining the resource/asset filename of the page icon.</param>
        /// <param name="moreListIcon">A string defining the resource/asset filename of the icon that must be
        /// shown when the page is listed in the 'more' page list or popup menu.</param>
        /// <param name="pageType">Type of content page that must be created when the TabPage is shown.</param>
        /// <param name="viewModel">An optional object to be assigned to the content page BindingContext.</param>
        public TabPage(string title, string pageIcon, string moreListIcon, Type pageType, object viewModel = null)
        {
            Initialize(title, pageIcon, moreListIcon);
            _viewPageType = pageType;
            _viewPageViewModel = viewModel;
        }

        /// <summary>
        /// Construct TabPage with a title and icon. The content page that must be shown when the TabPage is activated
        /// is created by means of a 'createContentPage function passed as parameter.  
        /// </summary>
        /// <param name="title">The title of the TabPage.</param>
        /// <param name="pageIcon">A string defining the resource/asset filename of the page icon.</param>
        /// <param name="moreListIcon">A string defining the resource/asset filename of the icon that must be
        /// <param name="createContentPage">A function delegate that must be called to create the TabPage's content page.</param>
        public TabPage(string title, string pageIcon, string moreListIcon, Func<ContentPage> createContentPage)
        {
            Initialize(title, pageIcon, moreListIcon);
            CreateContentPage = createContentPage;
        }

        /// <summary>
        /// Show TabPage
        /// </summary>
        public void Show(ToolbarItem moreToolbarItem)
        {
            if (_viewPageType == null)
            {
                _contentPage = CreateContentPage();
            }
            else
            {
                Page newPage = null;
                if (_viewPageViewModel != null)
                {
                    newPage = (Page)Activator.CreateInstance(_viewPageType, _viewPageViewModel);
                }
                else
                {
                    newPage = (Page)Activator.CreateInstance(_viewPageType);
                }
                _contentPage = newPage as ContentPage;
            }
            if (_contentPage != null)
            {
                this.BindingContext = _contentPage.BindingContext;
                foreach (var item in _contentPage.ToolbarItems)
                {
                    this.ToolbarItems.Add(item);
                }
                if (moreToolbarItem != null)
                {
                    this.ToolbarItems.Add(moreToolbarItem);
                }
                ContentBaseLayout = new RelativeLayout()
                {
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    BackgroundColor = Color.Transparent,
                    Margin = new Thickness(0),
                    Padding = new Thickness(0)
                };
                ContentBaseLayout.Children.Add(_contentPage.Content,
                    Constraint.RelativeToParent((parent) => {
                    return parent.X;
                }), Constraint.RelativeToParent((parent) => {
                    return parent.Y;
                }), Constraint.RelativeToParent((parent) => {
                    return parent.Width;
                }), Constraint.RelativeToParent((parent) => {
                    return parent.Height;
                }));
                this.Content = ContentBaseLayout;
            }
        }

        /// <summary>
        /// Hide TabPage
        /// </summary>
        public void Hide()
        {
            if (_contentPage != null)
            {
                /*
                foreach (var item in _contentPage.ToolbarItems)
                {
                    this.ToolbarItems.Remove(item);
                }
                if (moreToolbarItem != null)
                {
                    this.ToolbarItems.Remove(moreToolbarItem);
                }
                */
                this.ToolbarItems.Clear();
                _contentPage = null;
                ContentBaseLayout.Children.Clear();
                this.Content = null;
            }

        }

        public void AddToolbarItem(ToolbarItem moreToolbarItem)
        {

        }

        public void Initialize(string title, string pageIcon, string moreListIcon)
        {
            Title = title;
            Icon = pageIcon;
            MoreListIcon = moreListIcon;
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                    if (_contentPage != null)
                    {
                        this.Hide();
                    }
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~TabPage() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion

    }
}
