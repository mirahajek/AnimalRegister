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
using log = AnimalRegister.Pig.Logic;

namespace AnimalRegister.Pig.Winds
{
    /// <summary>
    /// Interakční logika pro AddFinanceWindow.xaml
    /// </summary>
    public partial class AddFinanceWindow : Window
    {
        private log.Validator validator;

        private log.Pig relativePig;

        public AddFinanceWindow(log.Validator validator)
        {
            InitializeComponent();
            this.validator = validator;  
        }


        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                
                validator.AddEditFinanceRecord(0,dateTextBox.Text,nameTextBox.Text,priceTextBox.Text,descriptionTextBox.Text,typeComboBox.SelectedIndex,categoryComboBox.SelectedIndex,
                    relativePig,null);


                Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Chyba", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            
        }

        private void CategoryComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (categoryComboBox.SelectedItem != null)
                relativePig = ()categoryComboBox.SelectedItem;
        }

        private void AnimalComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(animalComboBox.SelectedItem != null)
                relativePig = (log.Pig)animalComboBox.SelectedItem;
        }
    }
}
