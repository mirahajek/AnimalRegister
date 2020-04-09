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
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Instance validátoru, který kontroluje data a upozorňuje uživatele
        /// </summary>
        private Validator validator;

        private bool financeDataFlag;

        /// <summary>
        /// Základní konstruktor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            validator = new Validator();
            validator.DefineCanvas(pigCanvas, sawCanvas);

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
            try
            {
                validator.ConstructGraphicFinance(true,false,false);
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

                if (!financeDataFlag)
                    validator.ConstructGraphicPigSawList(false, true, rotateUpflag);
                else
                    validator.ConstructGraphicFinance(false, true, rotateUpflag);
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
            FinanceStatisticWindow window = new FinanceStatisticWindow(validator);
            window.Show();
        }
    }
}
