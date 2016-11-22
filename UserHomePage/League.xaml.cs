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
using knatteligan.Services;

namespace UserHomePage
{
    /// <summary>
    /// Interaction logic for League.xaml
    /// </summary>
    public partial class League : Window
    {
        private readonly TeamService _teamService;
        public League()
        {
            InitializeComponent();
            _teamService = new TeamService();
            TeamList.ItemsSource = _teamService.GetAllTeams();
        }

        private void ManageLeague_Clicked(object sender, MouseButtonEventArgs e)
        {
            throw new Exception();
        }
        private void searchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            throw new Exception();
        }

    }
}
