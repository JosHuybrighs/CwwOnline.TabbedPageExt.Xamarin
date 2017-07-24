# CwwOnline.TabbedPageExt.Xamarin

A TabbedPage for Xamarin Forms which supports:
* **UI virtualization**: Tab pages can either directly be created (before they are selected) or they can be created at the moment the tab is selected.
* **Crossplatform 'More' view**: For all platforms (android, iOS, uwp, ..) a 'more' tabbar button (iOS) or toolbar button (android, uwp) is automatically inserted popping up a iOS-like "More" listview to select one of the 'hidden' pages.
As an option it also possible to configure the tabbed page to show a popup menu when touching the 'more' toolbar button (ios, android, uwp).
* **Navigation support**: Possibility to push the TabbedPage itself on the navigation stack. 

Example view:

#### Mode 1: 'More' page list (as in iOS)
<div style="width: 100%; overflow: hidden;">
<div style="float: left; margin-right: 20px">
<h4>iOS</h4>
<img src="![](doc/tabbedpage_ios_moreview.PNG)" />
</div>
<div>
<h4>Android</h4>
<img src="![](doc/tabbedpage_ios_moreview.PNG)" />
</div>
</div>

#### Mode 2: 'More' popup menu (as in android)




## Installation

Still to do..

## Usage

Still to do..
```csharp
public class DemoTabbedPage: TabbedPageExt.TabbedPageExt
{
  public MyTabbedPage()
      : base(Device.RuntimePlatform == Device.Android ? IconColor.White : IconColor.Black)
   {
      var tabPage1 = new TabPage("Tab: 1",
                                 "tab1_icon_white.png", "tab1_icon_white.png",
                                 typeof(DemoContentPage), new DemoContentPageViewModel());
      this.TabPages.Add(tabPage1);
   }
}
```