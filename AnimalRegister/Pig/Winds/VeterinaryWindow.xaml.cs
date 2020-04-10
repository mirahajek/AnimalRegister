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
    /// CodeBehind pro okno pro přidání / úpravu veterinárního záznamu záznamu
    /// </summary>
    public partial class VeterinaryWindow : Window
    {
        /// <summary>
        /// Instance validátoru pro práci s daty aplikace
        /// </summary>
        private Validator validator;

        /// <summary>
        /// Upravovaný veterinární záznam
        /// </summary>
        private Veterinary editRecord;

        /// <summary>
        /// Základní konstruktor
        /// </summary>
        /// <param name="validator">Validátor aplikace</param>
        public VeterinaryWindow(Validator validator)
        {
            InitializeComponent();
            this.validator = validator;
            veterinarySelectComboBox.DataContext = validator.Define_VeterinaryContext();
        }

        /// <summary>
        /// Uložení zadaných hodnot * uložení nového záznamu, nebo úprava stávajícího - tlačítko Ulož
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
                    validator.AddEditVeterinary(0, dateTextBox.Text, priceTextBox.Text, purposeTextBox.Text, drugsTextBox.Text, tasksTextBox.Text, null);
                }
                // Úprava stávajícího záznamu
                else
                {
                    validator.AddEditVeterinary(1, dateTextBox.Text, priceTextBox.Text, purposeTextBox.Text, drugsTextBox.Text, tasksTextBox.Text, editRecord);
                }

                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Pozor", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Odebrání vybrného záznamu - tlačítko Odeber
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                validator.RemoveVeterinary(editRecord);

                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Pozor", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Změna vybrané položky v comboBoxu veterinárních záznamů
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void VeterinarySelectComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(veterinarySelectComboBox.SelectedItem != null)
            {
                editRecord = (Veterinary)veterinarySelectComboBox.SelectedItem;
            }
        }
    }
}
