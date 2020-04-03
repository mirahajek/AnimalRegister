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
            // Zdroj dat pro ComboBoxy
            pigTypeComboBox.ItemsSource = viewModel.PigType;
            motherComboBox.ItemsSource = viewModel.Mothers;
            // Nastavení počítku ComboBoxů, tak aby bylo vybráno ostatní, jelikož se bude častěji přidávat prase 
            // do chovu než prasnice
            pigTypeComboBox.SelectedIndex = 1;
            motherComboBox.SelectedIndex = -1;
            sexComboBox.SelectedIndex = -1;
            this.validator = validator;
            this.viewModel = viewModel;
            // Úprava stávajícího prasete
            if (viewModel.State)
            {
                pigTypeComboBox.SelectedIndex = viewModel.SelectType;            
                motherComboBox.SelectedIndex = viewModel.SelectMother;
                sexComboBox.SelectedIndex = viewModel.SelectSex;
                
                DataContext = viewModel;
                // Skrytí comboBoxu pro výběr matky, pokud se jedná o prasnici
                if (viewModel.SelectType == 0)
                {
                    motherComboBox.Opacity = 0.5;
                }
            }
        }

        /// <summary>
        /// Uložení zadaných dat do okna - tlačítko ULOŽ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // Nové zvíře
            if(!viewModel.State)
            {
                try
                {
                    validator.AddEditSawPig(0, pigTypeComboBox.SelectedIndex, motherComboBox.SelectedIndex, bornTextBox.Text, registerNumberTextBox.Text,
                        nameTextBox.Text, descriptionTextBox.Text,sexComboBox.SelectedIndex);
                    Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Pozor", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            // Úprava stávajícího
            else
            {
                try
                {
                    validator.AddEditSawPig(1, pigTypeComboBox.SelectedIndex, motherComboBox.SelectedIndex, bornTextBox.Text, registerNumberTextBox.Text,
                        nameTextBox.Text, descriptionTextBox.Text, sexComboBox.SelectedIndex);
                    Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Pozor", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                validator.RemoveSawPig();
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Pozor", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            
        }

        /// <summary>
        /// Změna výběru v ComboBoxu typu zvířete - Chovná prasnice / ostatní prasata
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PigTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(pigTypeComboBox.SelectedIndex == 0)
            {
                motherDescTextBlock.Visibility = Visibility.Hidden;
                motherComboBox.Visibility = Visibility.Hidden;

                sexDescTextBlock.Visibility = Visibility.Hidden;
                sexComboBox.Visibility = Visibility.Hidden;
            }
            else
            {
                motherDescTextBlock.Visibility = Visibility.Visible;
                motherComboBox.Visibility = Visibility.Visible;

                sexDescTextBlock.Visibility = Visibility.Visible;
                sexComboBox.Visibility = Visibility.Visible;
            }
        }

        private void VeterinaryButton_Click(object sender, RoutedEventArgs e)
        {
            VeterinaryWindow window = new VeterinaryWindow();
            window.Show();
        }

        private void BirthButton_Click(object sender, RoutedEventArgs e)
        {
            BirthWindow window = new BirthWindow();
            window.Show();


        }
    }
}
