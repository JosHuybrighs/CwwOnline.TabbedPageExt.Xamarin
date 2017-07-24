using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TabbedPageExtDemo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DemoContentPage : ContentPage
    {
        public DemoContentPage()
        {
            InitializeComponent();
        }

        public DemoContentPage(object viewModel)
        {
            this.BindingContext = viewModel;
            InitializeComponent();
        }
    }
}