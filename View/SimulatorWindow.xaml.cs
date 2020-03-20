using proj12.Model;
using proj12.View.Controls;
using proj12.ViewModel;
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

namespace proj12.View
{

    public partial class SimulatorWindow : Window
    {
        ViewModel.ViewModel  vm;
        DashboardControl db;


        [Obsolete]
        public SimulatorWindow()
        {
            InitializeComponent();
            var model = new SimulatorModel();
            vm = new ViewModel.ViewModel(model);
            this.DataContext = vm;
            db = new DashboardControl(model);
            Grid.SetColumn(db, 1);
            Grid.SetRow(db, 2);
            mainGrid.Children.Add(db);
            model.StartServer();



        }

        [Obsolete]
        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            SettingsWindow settingsWindw = new SettingsWindow();
            settingsWindw.ShowDialog();
        }
    }
}
