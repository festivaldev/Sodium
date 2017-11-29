using System.IO;
using System.Windows;
using Sodium.Dialogs;

namespace Sodium.Windows
{
    public partial class MainWindow : Window
    {
        public MainWindow() {
            InitializeComponent();

            this.Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e) {
            if (!File.Exists(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "settings.json"))) {
                var dialog = new FirstStartDialog { Owner = this };
                dialog.ShowDialog();
            }
        }
    }
}
