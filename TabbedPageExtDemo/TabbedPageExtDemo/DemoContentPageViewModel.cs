using System.Windows.Input;

namespace TabbedPageExtDemo
{
    public class DemoContentPageViewModel
    {
        TRelayCommand<object> _sendMessageCmd;
        public ICommand SendMessageCmd
        {
            get
            {
                if (_sendMessageCmd == null)
                {
                    _sendMessageCmd = new TRelayCommand<object>(
                        async (o) =>
                        {
                            // Give prompt
                            await App.Current.MainPage.DisplayAlert("Send message", "Send messages is not supported in the demo.", "Cancel");
                        });
                }
                return _sendMessageCmd;
            }
        }
    }
}
