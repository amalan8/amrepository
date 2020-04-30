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
using System.Text.RegularExpressions;

namespace Homework04
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
          
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {


                string patternToFind = @"^(\d{5}(-\d{4})?|[A-Z]\d[A-Z] ?\d[A-Z]\d)$";
                string enteredZip = txtBox_ZipCode.Text;



                Regex zipValidation = new Regex(patternToFind);


                if (zipValidation.IsMatch(enteredZip))
                {
                     btnSubmit.IsEnabled = true;
                }
                else
                {
                    btnSubmit.IsEnabled = false;
                }

        }
    }
    
}
