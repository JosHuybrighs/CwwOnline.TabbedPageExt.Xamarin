using UIKit;
using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;
using TabbedPageExtDemo.iOS;
using TabbedPageExt;

[assembly: ExportRenderer(typeof(PopupMenuItemViewCell), typeof(PopupMenuItemViewCellRenderer))]
namespace TabbedPageExtDemo.iOS
{
    class PopupMenuItemViewCellRenderer : ViewCellRenderer
    {
        public override UITableViewCell GetCell(Cell item, UITableViewCell reusableCell, UITableView tv)
        {
            var cell = base.GetCell(item, reusableCell, tv);
            var view = item as PopupMenuItemViewCell;
            cell.SelectedBackgroundView = new UIView
            {
                BackgroundColor = view.SelectedBackgroundColor.ToUIColor(),
            };

            return cell;
        }

    }
}
