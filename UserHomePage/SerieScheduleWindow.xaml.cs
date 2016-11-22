using knatteligan.Domain.Entities;
using knatteligan.Helpers;
using knatteligan.Services;
using KnatteliganWPF;
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

namespace UserHomePage
{
    /// <summary>
    /// Interaction logic for SerieScheduleWindow.xaml
    /// </summary>
    public partial class SerieScheduleWindow : Window
    {
        public SerializableDictionary<int, MatchWeek> GameWeeks { get; set; }
        MatchService _matchRepositoryService;
        public SerieScheduleWindow()
        {
            _matchRepositoryService = new MatchService();
            InitializeComponent();
            DataContext = this;
        }
        private void CurrentMatchWeekMatches_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

            var listItem = sender as ListBox;
            var match = (Match)listItem.SelectedItems[0];
            var matchProtocol = new MatchProtocol(match);
            matchProtocol.Show();
        }

        private void listView_Click(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {

        }

        private void DatePicker_OnSelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
