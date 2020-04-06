using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interakční logika pro BirthWindow.xaml
    /// </summary>
    public partial class BirthWindow : Window
    {
        /// <summary>
        /// View model pro bindování dat a comboBoxů
        /// </summary>
        private VM_Birth viewModel;

        /// <summary>
        /// 
        /// </summary>
        private Validator validator;

        /// <summary>
        /// Upravovaný záznam o porodu
        /// </summary>
        private Birth editRecord;

        /// <summary>
        /// Základní konstruktor
        /// </summary>
        public BirthWindow(Validator validator, VM_Birth viewModel)
        {
            InitializeComponent();
            this.viewModel = viewModel;
            this.validator = validator;
            if (!viewModel.EditRecord)
                birthSelectComboBox.DataContext = viewModel.SawBirth;
        }

        /// <summary>
        /// Uživatel vybral konkrétní porod - zobrazí se data ve všech polích
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BirthSelectComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Podmínka pro první spuštění, kdy ještě není vybráná žádná instance
            if (birthSelectComboBox.SelectedItem != null)
            {
                editRecord = (Birth)birthSelectComboBox.SelectedItem;
                viewModel.ChangeRecord(editRecord);

                DataContext = viewModel;
                birthSelectComboBox.DataContext = viewModel.SawBirth;
                // Obnovení dat v textBoxech a comboBoxu - nefungovalo mi PropertyChanged
                dateRecessedTextBox.GetBindingExpression(TextBox.TextProperty).UpdateTarget();
                datePregnancyCheckTextBox.GetBindingExpression(TextBox.TextProperty).UpdateTarget();
                pregnancyCheckComboBox.GetBindingExpression(ComboBox.SelectedIndexProperty).UpdateTarget();
                
                bornPlanTextBox.GetBindingExpression(TextBox.TextProperty).UpdateTarget();
                bornRealTextBox.GetBindingExpression(TextBox.TextProperty).UpdateTarget();
                liveTextBox.GetBindingExpression(TextBox.TextProperty).UpdateTarget();
                deadTextBox.GetBindingExpression(TextBox.TextProperty).UpdateTarget();
                rearedTextBox.GetBindingExpression(TextBox.TextProperty).UpdateTarget();
            }
        }

        /// <summary>
        /// Uložení všech dat na disk a uzavření OKNA - tlačítko Ulož
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Nový záznam
                if(editRecord == null)
                {
                    validator.AddEditBirth(0, dateRecessedTextBox.Text, liveTextBox.Text, deadTextBox.Text, rearedTextBox.Text, bornRealTextBox.Text
                    , pregnancyCheckComboBox.SelectedIndex, null);
                }
                // Úprava stávajícího
                else
                {
                    validator.AddEditBirth(1, dateRecessedTextBox.Text, liveTextBox.Text, deadTextBox.Text, rearedTextBox.Text, bornRealTextBox.Text
                    , pregnancyCheckComboBox.SelectedIndex,editRecord);
                }
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Pozor", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Odebrání vybraného záznamu o porodu - tlačítko ODEBER
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
