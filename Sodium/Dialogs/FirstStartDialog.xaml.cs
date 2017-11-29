using System.Windows;
using Newtonsoft.Json;
using Sodium.Helpers;

namespace Sodium.Dialogs
{
    public partial class FirstStartDialog : Window
    {
        public FirstStartDialog() {
            InitializeComponent();

            rtb.AppendText(JsonConvert.SerializeObject(PasswordHelper.Protect(PasswordHelper.ConvertToSecureString("test"))));
        }
    }
}
