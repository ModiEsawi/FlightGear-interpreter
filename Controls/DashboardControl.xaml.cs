using proj12.Model;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace proj12.View.Controls
{
    /// <summary>
    /// Interaction logic for DashboardControl.xaml
    /// </summary>
    public partial class DashboardControl : UserControl
    {
        private ISimulatorModel model;
        private DashboardVM dvm;
        public DashboardControl(ISimulatorModel model)
        {
            InitializeComponent();
            this.model = model;
            dvm = new DashboardVM(model);
            this.DataContext = dvm;
        }

        //public void setModel(ISimulatorModel model)
        //{
        //    this.model = model;
        //    dvm = new DashboardVM(model);
        //    this.DataContext = dvm;
        //}
    }
}
