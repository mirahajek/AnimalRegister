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
    /// Okno pro zobrazení statistických parametrů financí - přehled příjmů/výdajů v každém měsíci, výběr v kategoriích apod
    /// </summary>
    public partial class FinanceStatisticWindow : Window
    {
        /// <summary>
        /// Instance validátoru, který slouží pro veškerou validaci dat
        /// </summary>
        private log.Validator validator;

        /// <summary>
        /// Flag - při prvním spuštění
        /// </summary>
        private bool firstStartFlag;
        /// <summary>
        /// Kolekce obsahující názvy kategoriíí + volba všechny na konci
        /// </summary>
        private List<string> categoryNames;

        /// <summary>
        /// Vybraná instance prasete
        /// </summary>
        private log.Pig selectPig;

        /// <summary>
        /// Základní konstruktor
        /// </summary>
        /// <param name="validator">Instance validátoru, který slouží pro veškerou validaci dat</param>
        public FinanceStatisticWindow(log.Validator validator)
        {
            firstStartFlag = true;
            this.validator = validator;
            InitializeComponent();
            // Získání názvu kategorií a přidání volby Všechny na konec
            categoryNames = new List<string>();
            categoryNames.AddRange(log.Admin.FinanceCategory_Czech);
            categoryNames.Add("Všechny");
            // Nastavení kontextu a vybrané položky comboBoxů
            categoryComboBox.DataContext = categoryNames;
            categoryComboBox.SelectedIndex = categoryNames.Count - 1;
            yearComboBox.SelectedIndex = DateTime.Today.Year - 2018;
            validator.CalculateStatisticData(statisticCanvas, ((ComboBoxItem)yearComboBox.SelectedItem).Content.ToString(), categoryNames.Count - 1, null);
            // Nastavení viditelnosti u comboBoxu pro výběr konkrétního zvířete
            animalComboBox.Visibility = Visibility.Hidden;
            animalTitle.Visibility = Visibility.Hidden;
            // Naplnění comboBoxu pro zvířata, kde uživatel vybírá konkrétní zvíře 
            animalComboBox.DataContext = validator.Define_PigsList();
            firstStartFlag = false;
        }


        /// <summary>
        /// Změna vybrané hodnoty v comboBoxu pro výběr roku
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void YearComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!firstStartFlag)
            {
                if (selectPig == null)
                    validator.CalculateStatisticData(statisticCanvas, ((ComboBoxItem)yearComboBox.SelectedItem).Content.ToString(), categoryComboBox.SelectedIndex, null);
                else
                    validator.CalculateStatisticData(statisticCanvas, ((ComboBoxItem)yearComboBox.SelectedItem).Content.ToString(), categoryComboBox.SelectedIndex, 
                        selectPig);

            }
        }

        /// <summary>
        /// Změna vybrané hodnoty v comboBoxu pro výběr kategorie
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CategoryComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Uživatel vybral kategorii zvíře, umožní se mu vybrat i konkrétní zvíře, pokud chce
            if (categoryComboBox.SelectedIndex == 3)
            {
                animalComboBox.SelectedIndex = -1;
                animalComboBox.Visibility = Visibility.Visible;
                animalTitle.Visibility = Visibility.Visible;
            }
            else
            {
                selectPig = null;
                animalComboBox.Visibility = Visibility.Hidden;
                animalTitle.Visibility = Visibility.Hidden;
            }

            if (!firstStartFlag)
            {
                if (selectPig == null)
                    validator.CalculateStatisticData(statisticCanvas, ((ComboBoxItem)yearComboBox.SelectedItem).Content.ToString(), categoryComboBox.SelectedIndex, null);
            }
        }

        /// <summary>
        /// Změna vybrané hodnoty v comboBoxu pro výběr konkrétního zvířete
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AnimalComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if((log.Pig)animalComboBox.SelectedItem != null)
            {
                selectPig = (log.Pig)animalComboBox.SelectedItem;
                validator.CalculateStatisticData(statisticCanvas, ((ComboBoxItem)yearComboBox.SelectedItem).Content.ToString(), categoryComboBox.SelectedIndex, selectPig);
            }
        }
    }
}
