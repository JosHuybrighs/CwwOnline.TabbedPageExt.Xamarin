using Android.Content;
using Android.Views;
using Android.Graphics.Drawables;
using System.ComponentModel;
using Xamarin.Forms;
using TabbedPageExt;
using Xamarin.Forms.Platform.Android;
using TabbedPageExt.Android;

[assembly: ExportRenderer(typeof(PopupMenuItemViewCell), typeof(PopupMenuItemViewCellRenderer))]
namespace TabbedPageExt.Android
{
    public class PopupMenuItemViewCellRenderer : ViewCellRenderer
    {

        private global::Android.Views.View _cellCore;
        private Drawable _unselectedBackground;
        private bool _selected;

        protected override global::Android.Views.View GetCellCore(Cell item,
                                                                  global::Android.Views.View convertView,
                                                                  ViewGroup parent,
                                                                  Context context)
        {
            _cellCore = base.GetCellCore(item, convertView, parent, context);

            _selected = false;
            _unselectedBackground = _cellCore.Background;

            return _cellCore;
        }

        protected override void OnCellPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            base.OnCellPropertyChanged(sender, args);

            if (args.PropertyName == "IsSelected")
            {
                _selected = !_selected;

                if (_selected)
                {
                    var extendedViewCell = sender as PopupMenuItemViewCell;
                    _cellCore.SetBackgroundColor(extendedViewCell.SelectedBackgroundColor.ToAndroid());
                }
                else
                {
                    _cellCore.SetBackground(_unselectedBackground);
                }
            }
        }
    }

}