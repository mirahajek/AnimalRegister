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
 
            validator.ConstructGraphicPigSawList(true,false,false);
        }

        private void OverviewButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void FinanceButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            validator.SaveAll();
            Close();
        }

        private void AddFinanceButton_Click(object sender, RoutedEventArgs e)
        {
          
        }

        private void AddPigButton_Click(object sender, RoutedEventArgs e)
        {
            
            AddSawPig window = new AddSawPig(validator, validator.DefineVM_PigSaw(null));
            
            window.Show();
        }

        private void Window_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            try
            {
                bool rotateUpflag = false;
                // Delta vrací hodnotu rotace
                //      - Kolečkem dolů je hodnota -
                //      - Kolečkem nahoru je hodnota +
                if (e.Delta > 0)
                    rotateUpflag = true;

                validator.ConstructGraphicPigSawList(false, true, rotateUpflag);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Pozor", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
