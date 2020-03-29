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
using AnimalRegister.Pig.Logic;
using AnimalRegister.Pig.Winds;

namespace AnimalRegister
{
    /// <summary>
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Validator validator; 

        public MainWindow()
        {
            InitializeComponent();
            validator = new Validator();
            validator.DefineCanvas(pigCanvas, sawCanvas);
            validator.ConstructGraphicSawList();
        }

        private void OverviewButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void FinanceButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void AddFinanceButton_Click(object sender, RoutedEventArgs e)
        {
          
        }

        private void AddPigButton_Click(object sender, RoutedEventArgs e)
        {
            AddSawPig window = new AddSawPig(validator);
            window.Show();
        }

        private void AddSawButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
