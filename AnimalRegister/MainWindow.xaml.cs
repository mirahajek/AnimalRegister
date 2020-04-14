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
    /// CodeBehind pro hlavní okno obrazovky, sloužící pro vykreslení záznamů prasat nebo financí
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Instance validátoru, který kontroluje data a upozorňuje uživatele
        /// </summary>
        private Validator validator;

        /// <summary>
        /// Flag vyjadřující, že jsou zobrazována data pro finance ANO / NE - při rotaci kolečkem, aby při skok na další stranu byla druhá strana financí a ne PRASAT a opačně
        /// </summary>
        private bool financeDataFlag;

        /// <summary>
        /// Flag informuje o prvním nastavení comboBoxu pro rok a kategorii, aby při nastavování se nevolali metody z VALIDATORU, protože nastane comboBox_SelectionChange
        /// </summary>
        private bool firstStartFlag; 

        /// <summary>
        /// Kolekce pro názvy kategorií - získaná ze statické vlastnosti správce + přidána volba VŠECHNY na konec
        /// </summary>
        private List<string> categoryNames;

        /// <summary>
        /// Základní konstruktor
        /// </summary>
        public MainWindow()
        {

            InitializeComponent();

            firstStartFlag = true;
            ComboBoxsVisible(false);
            // Získání názvu kategorií a přidání volby Všechny na konec
            categoryNames = new List<string>();
            categoryNames.AddRange(Admin.FinanceCategory_Czech);
            categoryNames.Add("Všechny");
            // Nastavení kontextu a vybrané položky comboBoxů
            categoryComboBox.DataContext = categoryNames;
            categoryComboBox.SelectedIndex = categoryNames.Count - 1;
            yearComboBox.SelectedIndex = DateTime.Today.Year - 2018;
            firstStartFlag = false;

            validator = new Validator();
            validator.DefineCanvas(pigCanvas, sawCanvas);
            // Vykreslení prasat a flag na prasata
            financeDataFlag = false;
            validator.ConstructGraphicPigSawList(true,false,false);

        }

        /// <summary>
        /// Zobrazí základní přehled, tedy výpis záznamů prasat a prasnic na 5 stran. Začíná na první straně - tlačítko Přehled
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OverviewButton_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxsVisible(false);
            // Vykreslení záznamů prasat
            validator.ConstructGraphicPigSawList(true, false, false);
            financeDataFlag = false;
        }

        /// <summary>
        /// Zobrazení všech finančních záznamů - tlačítko Finance
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FinanceButton_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxsVisible(true);
            try
            {
                // Vykreslení záznamů financí a nastavení flagu, aby při rotaci kolečkem proběhlo vykreslení další strany také pro finance
                validator.ConstructGraphicFinance(true,false,false, false, yearComboBox.SelectedIndex, categoryComboBox.SelectedIndex);
                financeDataFlag = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Pozor", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Uložení všech dat a uzavření okna - tlačítko Ulož
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            validator.SaveAll();
            Close();
        }

        /// <summary>
        /// Přidání nového finančního záznamu - tlačítko Přidej
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddFinanceButton_Click(object sender, RoutedEventArgs e)
        {
            financeDataFlag = true;
            ComboBoxsVisible(true);
            // Okno pro přidání záznamu financí
            AddFinanceWindow window = new AddFinanceWindow(validator, validator.DefineVM_Finance(true));
            window.Show();
        }

        /// <summary>
        /// Přidání nového prasete - tlačítko Přidej
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddPigButton_Click(object sender, RoutedEventArgs e)
        {
            financeDataFlag = false;
            ComboBoxsVisible(false);
            // Okno pro přidání záznamu prasete
            AddSawPig window = new AddSawPig(validator, validator.DefineVM_PigSaw(true));
            window.Show();
        }

        /// <summary>
        /// Rotace kolečka - možnost rotovat až na 5 stran. Při přetečení na první straně se skočí na poslední apod na poslední straně
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                // Vykreslení záznamů financí - další strana
                if (!financeDataFlag)
                    validator.ConstructGraphicPigSawList(false, true, rotateUpflag);
                else
                    validator.ConstructGraphicFinance(false, true, rotateUpflag, false, yearComboBox.SelectedIndex, categoryComboBox.SelectedIndex);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Pozor", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Otevře okno finanční statistiky, kde je vidět rořní přehled - tlačítko Statistika
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StatisticButton_Click(object sender, RoutedEventArgs e)
        {
            if(!financeDataFlag)
                ComboBoxsVisible(false);

            FinanceStatisticWindow window = new FinanceStatisticWindow(validator);
            window.Show();
        }

        /// <summary>
        /// Změna výběru v comboBoxu pro rok
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void YearComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!firstStartFlag && financeDataFlag)
            {
                try
                {
                    validator.ConstructGraphicFinance(true, false, false, false,yearComboBox.SelectedIndex, categoryComboBox.SelectedIndex);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Pozor", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            

        }

        /// <summary>
        /// Změna výběru v comboBoxu pro kategorii
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CategoryComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!firstStartFlag && financeDataFlag)
            {
                try
                {
                    validator.ConstructGraphicFinance(true, false, false, false,yearComboBox.SelectedIndex, categoryComboBox.SelectedIndex);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Pozor", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        /// <summary>
        /// Zviditelnění comboBoxů pro rok a kategorii + jejich popis
        /// </summary>
        /// <param name="visibility"></param>
        private void ComboBoxsVisible(bool visibility)
        {
            if (visibility)
            {
                categoryComboBox.Visibility = Visibility.Visible;
                yearComboBox.Visibility = Visibility.Visible;
                categoryTitleTextBlock.Visibility = Visibility.Visible;
                yearTitleTextBlock.Visibility = Visibility.Visible;
            }
            else
            {
                categoryComboBox.Visibility = Visibility.Hidden;
                yearComboBox.Visibility = Visibility.Hidden;
                categoryTitleTextBlock.Visibility = Visibility.Hidden;
                yearTitleTextBlock.Visibility = Visibility.Hidden;
            }
        }
    }
}
