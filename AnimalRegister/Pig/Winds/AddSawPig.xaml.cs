﻿using System;
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
    /// CodeBehind pro okno pro přidání / úpravu záznamu prasete
    /// </summary>
    public partial class AddSawPig : Window
    {
        /// <summary>
        /// Validátor aplikace - kontroluje vstupní data a volá metody ze správce
        /// </summary>
        private Validator validator;
        /// <summary>
        /// View model pro přidání / editaci záznamu prasete
        /// </summary>
        private VM_PigSaw viewModel;

        private bool correctClose;

        /// <summary>
        /// Konstruktor pro úpravu prasete / prasnice
        /// </summary>
        /// <param name="validator">Validátor aplikace - konstroluje vstupní data a volá metody ze správce</param>
        /// <param name="viewModel">View model pro přidání / editaci záznamu prasete</param>
        public AddSawPig(Validator validator, VM_PigSaw viewModel)
        {
            InitializeComponent();
            // Zdroj dat pro ComboBoxy
            pigTypeComboBox.ItemsSource = viewModel.PigType;
            motherComboBox.ItemsSource = viewModel.Mothers;
            // Nastavení počátku ComboBoxů, tak aby bylo vybráno ostatní, jelikož se bude častěji přidávat prase 
            // do chovu než prasnice
            pigTypeComboBox.SelectedIndex = 1;
            motherComboBox.SelectedIndex = -1;
            sexComboBox.SelectedIndex = -1;
            this.validator = validator;
            this.viewModel = viewModel;
            //Schování tlačítek při přidání nového zvířete, aby nebylo možné kliknout na porod a veterinu
            birthButton.Visibility = Visibility.Hidden;
            veterinaryButton.Visibility = Visibility.Hidden;

            // Úprava stávajícího prasete
            if (viewModel.EditRecordFlag)
            {
                pigTypeComboBox.SelectedIndex = viewModel.SelectType;            
                motherComboBox.SelectedIndex = viewModel.SelectMother;
                sexComboBox.SelectedIndex = viewModel.SelectSex;
                
                DataContext = viewModel;
                // Skrytí tlačítka pro porody u ostatních prasat
                if (viewModel.SelectType == 1)
                {
                    birthButton.Visibility = Visibility.Hidden;
                    veterinaryButton.Visibility = Visibility.Visible;
                }
                else
                {
                    birthButton.Visibility = Visibility.Visible;
                    veterinaryButton.Visibility = Visibility.Visible;
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
            if(!viewModel.EditRecordFlag)
            {
                try
                {
                    validator.AddEditSawPig(0, pigTypeComboBox.SelectedIndex, motherComboBox.SelectedIndex, bornTextBox.Text, registerNumberTextBox.Text,
                        nameTextBox.Text, descriptionTextBox.Text,sexComboBox.SelectedIndex);
                    correctClose = true;
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
                    correctClose = true;
                    Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Pozor", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            
        }

        /// <summary>
        /// Odebrání vybraného záznamu - tlačítko Odeber
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                validator.RemoveSawPig();
                correctClose = true;
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
            // Prasnice - schová se možnost zadat matku a pohlaví, protože to není relevatní v tomto případě
            if(pigTypeComboBox.SelectedIndex == 0 && viewModel != null)
            {
                // Skrytí tlačítka porodů u nově vytvářené prasnice
                if(!viewModel.EditRecordFlag)
                    birthButton.Visibility = Visibility.Hidden;
                // Zobrazení tlačítka porodu při editaci prasnice
                else
                    birthButton.Visibility = Visibility.Visible;
                // Skrytí výběru matky - u chovné prasnice nelze zadat matku, jelikož se neočekává, že bude z chovu
                motherDescTextBlock.Visibility = Visibility.Hidden;
                motherComboBox.Visibility = Visibility.Hidden;
                motherComboBox.SelectedIndex = -1;
                // Skrytí výběru pohlaví, jelikož u prasnice bude neměnné
                sexDescTextBlock.Visibility = Visibility.Hidden;
                sexComboBox.Visibility = Visibility.Hidden;
                sexComboBox.SelectedIndex = -1;
            }
            // Ostatní prase, zde je již důležité uchovat informaci o pohlaví a matce
            else if(viewModel != null)
            {
                if (!viewModel.EditRecordFlag)
                    birthButton.Visibility = Visibility.Hidden;
                else
                    birthButton.Visibility = Visibility.Visible;
                // Zobrazení výběru matky a pohlaví
                motherDescTextBlock.Visibility = Visibility.Visible;
                motherComboBox.Visibility = Visibility.Visible;

                sexDescTextBlock.Visibility = Visibility.Visible;
                sexComboBox.Visibility = Visibility.Visible;
            }
        }

        /// <summary>
        /// Zobrazení okna veterinárních záznamů - tlačítko Veterina
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void VeterinaryButton_Click(object sender, RoutedEventArgs e)
        {
            VeterinaryWindow window = new VeterinaryWindow(validator);
            window.Show();
        }

        /// <summary>
        /// Zobrazení okna porodů - tlačítko Porod
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BirthButton_Click(object sender, RoutedEventArgs e)
        {
            BirthWindow window = new BirthWindow(validator, validator.DefineVM_Birth());
            window.Show();
        }

        /// <summary>
        /// Obsluha, která informuje o zavírání okna
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Ochrana před nechtěným uzavřením okna a ztrátou dat

            // Okno uzavřeno pomocí křížku
            if (!correctClose)
            {
                // Dotaz uživatele, zda si opravdu přeje uzavřít okno bez uložení
                MessageBoxResult result = MessageBox.Show("Opravdu si přejete uzavřít okno bez uložení ? ", "Pozor", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if(result == MessageBoxResult.Yes)
                {
                    try
                    {
                        validator.ConstructGraphicPigSawList(true, false, false);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Pozor", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    e.Cancel = true;
                }
            }
            else
                e.Cancel = false;
        }
    }
}
