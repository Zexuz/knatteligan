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
using knatteligan.Repositories;
using knatteligan.Services;

namespace UserHomePage
{
    /// <summary>
    /// Interaction logic for PlayerStats.xaml
    /// </summary>
    public partial class PlayerStats : Window
    {
        private readonly PersonService _personService;
        public PlayerStats()
        {
            InitializeComponent();
            _personService = new PersonService();
            DataGrid.ItemsSource = _personService.GetAllPlayers();
        }

        private void DataGrid_OnAutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            
        }
    }
}
