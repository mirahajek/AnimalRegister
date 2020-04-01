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

        public AddSawPig(Validator validator, VM_PigSaw viewModel)
        {
            InitializeComponent();
            pigTypeComboBox.ItemsSource = viewModel.PigType;
            motherComboBox.ItemsSource = viewModel.Mothers;
            this.validator = validator;
            this.viewModel = viewModel;

            DataContext = viewModel;

        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {

            Close();
        }
    }
}
