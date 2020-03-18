using System;
using System.Collections.Generic;
using System.Configuration;
using System.Windows;

namespace proj12.View
{

    public partial class SettingsWindow : Window
    {
        [Obsolete]
        public SettingsWindow()
        {
            InitializeComponent();
            SimIPtextBox.Text = ConfigurationSettings.AppSettings.Get("Simulator IP");
            SimPortTextBox.Text = ConfigurationSettings.AppSettings.Get("Simulator Port");
            HostPortTextBox.Text= ConfigurationSettings.AppSettings.Get("Host Port");
        }

        [Obsolete]
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {

            if (SimIPtextBox.Text == "" || SimPortTextBox.Text == "" || HostPortTextBox.Text == "")
            {
                MessageBox.Show("fields is Missing", "Confirmation");
            }
            else
            {
                ConfigurationSettings.AppSettings.Set("Simulator IP", SimIPtextBox.Text);
                ConfigurationSettings.AppSettings.Set("Simulator Port", SimPortTextBox.Text);
                ConfigurationSettings.AppSettings.Set("Host Port", HostPortTextBox.Text);
                this.Close();
            }

        }
        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            SimIPtextBox.Text = "";
            SimPortTextBox.Text = "";
            HostPortTextBox.Text = "";
        }
    }
    
}
