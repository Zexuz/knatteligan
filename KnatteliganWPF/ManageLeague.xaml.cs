using System.Windows;

namespace KnatteliganWPF
{
    /// <summary>
    /// Interaction logic for ManageLeague.xaml
    /// </summary>
    public partial class ManageLeague : Window
    {
        public ManageLeague()
        {
            InitializeComponent();
        }
        private void MainWindow_Clicked(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
