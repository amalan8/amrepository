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

namespace HelloWorld
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //this.WindowState = WindowState.Maximized;
            this.uxSubmit.IsEnabled = false;
        }

        private void submitEnabledCheck()
        {
            uxSubmit.IsEnabled = ((uxName.Text != "") && (uxPassword.Text != ""));
        }
        private void uxSubmit_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Submitting password:" + uxPassword.Text);
        }

        private void uxName_changed(object sender, TextChangedEventArgs e)
        {
            submitEnabledCheck();
        }

        private void uxPassword_changed(object sender, TextChangedEventArgs e)
        {

            submitEnabledCheck();
        }
    }
}
