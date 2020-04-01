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
using AnimalRegister.Pig.Logic;

namespace AnimalRegister.Pig.Winds
{
    /// <summary>
    /// Interakční logika pro AddSawPig.xaml
    /// </summary>
    public partial class AddSawPig : Window
    {
        private Validator validator;

        private VM_PigSaw viewModel;

        /// <summary>
        /// Konstruktor pro úpravu prasete / prasnice
        /// </summary>
        /// <param name="validator"></param>
        /// <param name="viewModel"></param>
        public AddSawPig(Validator validator, VM_PigSaw viewModel)
        {
            InitializeComponent();
            pigTypeComboBox.ItemsSource = viewModel.PigType;
            motherComboBox.ItemsSource = viewModel.Mothers;
            this.validator = validator;
            this.viewModel = viewModel;

            if (viewModel.State)
            {
                pigTypeComboBox.SelectedIndex = viewModel.SelectType;            
                motherComboBox.SelectedIndex = viewModel.SelectMother;
                
                DataContext = viewModel;
                // Skrytí comboBoxu pro výběr matky, pokud se jedná o prasnici
                if (viewModel.SelectType == 0)
                {
                    motherComboBox.Opacity = 0.5;
                }
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {

            Close();
        }
    }
}
