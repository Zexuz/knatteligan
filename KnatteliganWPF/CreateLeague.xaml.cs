using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace KnatteliganWPF
{
    /// <summary>
    /// Interaction logic for CreateLeague.xaml
    /// </summary>
    public partial class CreateLeague : Window
    {
        public CreateLeague()
        {
            InitializeComponent();
        }
        private void AddTeam_Clicked(object sender, RoutedEventArgs e)
        {
            var addTeam = new AddTeam();
            var addTeamResult = addTeam.ShowDialog();
           
        }
        private void CloseCommandHandler_Clicked(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
