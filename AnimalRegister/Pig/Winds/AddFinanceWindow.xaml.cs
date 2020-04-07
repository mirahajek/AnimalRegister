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
        /// <summary>
        /// Instance validátoru pro přístup k logice programu
        /// </summary>
        private log.Validator validator;
        /// <summary>
        /// Instance prasete, kterého se týká daný finanční záznam - pokud byl zadán
        /// </summary>
        private log.Pig relativePig;
        /// <summary>
        /// View model pro bindování dat
        /// </summary>
        private log.VM_Finance viewModel;

        /// <summary>
        /// Základní konstruktor - pro přidání nového záznamu
        /// </summary>
        /// <param name="validator">Validátor aplikace</param>
        /// <param name="viewModel">View model pro bindování dat</param>
        public AddFinanceWindow(log.Validator validator, log.VM_Finance viewModel)
        {
            InitializeComponent();
            // Nastavení atributů
            this.validator = validator;
            this.viewModel = viewModel;
            // Schování comboBoxu a popisu pro váběr zvířete  a tlačítka odeber, protože se jedná o nový záznam
            animalTitleTextBlock.Visibility = Visibility.Hidden;
            animalComboBox.Visibility = Visibility.Hidden;
            removeButton.Visibility = Visibility.Hidden;
            // Nastavení kontextu pro comboBoxy, aby zobrazovali seznam prasat a 
            categoryComboBox.DataContext = viewModel.CategoryNames;
            animalComboBox.DataContext = viewModel.Pigs;
            
            if (!viewModel.NewRecordFlag)
            {
                
                DataContext = viewModel;
                categoryComboBox.SelectedIndex = viewModel.SelectCategory;
                typeComboBox.SelectedIndex = viewModel.SelectType;
                animalComboBox.SelectedIndex = viewModel.SelectPig;
                removeButton.Visibility = Visibility.Visible;
            }
        }

        /// <summary>
        /// Rozšířený konstruktor - pro úpravu stávajícího záznamu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                validator.RemoveFinanceRecord();

                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Chyba", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        /// <summary>
        /// Uložení všech dat zadaných do okna - tlačítko Ulož
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(viewModel.NewRecordFlag)
                {
                    validator.AddEditFinanceRecord(0, dateTextBox.Text, nameTextBox.Text, priceTextBox.Text, descriptionTextBox.Text, typeComboBox.SelectedIndex, categoryComboBox.SelectedIndex,
                    relativePig);
                }
                else
                {
                    validator.AddEditFinanceRecord(1, dateTextBox.Text, nameTextBox.Text, priceTextBox.Text, descriptionTextBox.Text, typeComboBox.SelectedIndex, categoryComboBox.SelectedIndex,
                    relativePig);
                }

                Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Chyba", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            
        }

        /// <summary>
        /// Změna v comboBoxu pro výběr konkrétního zvířete
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AnimalComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(animalComboBox.SelectedItem != null) 
                relativePig = (log.Pig)animalComboBox.SelectedItem;
        }

        /// <summary>
        /// Změna v comboBoxu pro výběr kategorie transakce
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CategoryComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Uživatel vybral zvíře - zobrazí se mu další comboBox, kde může vybrat nějaké konkrétní ve svém chovu
            if(categoryComboBox.SelectedIndex == 3)
            {
                animalTitleTextBlock.Visibility = Visibility.Visible;
                animalComboBox.Visibility = Visibility.Visible;
            }
            // Uživatel nevybral zvíře - schová se mu varianta pro konkrétní zvíře
            else
            {
                animalTitleTextBlock.Visibility = Visibility.Hidden;
                animalComboBox.Visibility = Visibility.Hidden;
            }
        }
    }
}
