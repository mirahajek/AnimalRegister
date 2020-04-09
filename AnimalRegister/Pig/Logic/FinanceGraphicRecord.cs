using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace AnimalRegister.Pig.Logic
{
    /// <summary>
    /// Třída reprezentující grafickou podobu jedné transakce
    /// </summary>
    public class FinanceGraphicRecord
    {
        /// <summary>
        /// Událost třídy FinanceGraphicRecord, která se vyvolá při kliknutí na RectangleDown myší. Předá jako sender tuto třídu
        /// </summary>
        public event EventHandler GraphicRecordClick;

        /// <summary>
        /// Strana záznamu
        /// </summary>
        public int Page { get; set; }
        /// <summary>
        /// Částka transakce
        /// </summary>
        public TextBlock Price { get; private set; }
        /// <summary>
        /// Datum transakce
        /// </summary>
        public TextBlock Date { get; private set; }
        /// <summary>
        /// Název transakce
        /// </summary>
        public TextBlock Title { get; private set; }
        /// <summary>
        /// Název kategorie, dané transakce
        /// </summary>
        public TextBlock CategoryName { get; private set; }
        /// <summary>
        /// Horní modrý obdelník
        /// </summary>
        public Rectangle RectangleTop { get; private set; }
        /// <summary>
        /// Spodní bílý obdelník
        /// </summary>
        public Rectangle RectangleDown { get; private set; }
        /// <summary>
        /// Vztažný finanční záznam
        /// </summary>
        public FinanceRecord FinanceRecord { get; set; }

        /// <summary>
        /// Základní konstruktor - vytvoří vše potřebné pro vykreslení
        /// </summary>
        /// <param name="record"></param>
        public FinanceGraphicRecord(FinanceRecord record)
        {
            // Částka
            Price = new TextBlock
            {
                FontSize = 16,
                FontWeight = FontWeights.Bold,
                Foreground = Brushes.Black,
                Text = record.Price.ToString() + "\tKč"

            };
            // Datum
            Date = new TextBlock
            {
                FontSize = 15,
                FontWeight = FontWeights.Bold,
                Foreground = Brushes.Black,
                Text = record.Date.ToShortDateString()

            };
            // Název kategorie
            CategoryName = new TextBlock
            {
                FontSize = 14,
                FontWeight = FontWeights.DemiBold,
                Foreground = Brushes.Black,
                Text = Admin.FinanceCategory_Czech[(int)record.Category]

            };
            // Název transakce
            Title = new TextBlock
            {
                FontSize = 14,
                FontWeight = FontWeights.DemiBold,
                Foreground = Brushes.Black,
                Text = record.Name

            };
            // HOrní obdelník
            RectangleTop = new Rectangle
            {
                Width = 380,
                Height = 25,
                RadiusX = 3,
                RadiusY = 3,
                Fill = new SolidColorBrush(Color.FromArgb(255, 111, 121, 182))
            };

            RectangleDown = new Rectangle
            {
                Width = 380,
                Height = 25,
                RadiusX = 5,
                RadiusY = 5,
                Fill = new SolidColorBrush(Color.FromArgb(160, 0, 255, 0)), //Brushes.White,
                StrokeThickness = 1,
                Stroke = Brushes.Gray
            };

            if (record.TypeRecord == FinanceTypeRecord.Costs)
                RectangleDown.Fill = new SolidColorBrush(Color.FromArgb(160, 255, 0, 0));//(255, 255, 108, 108));
            // Přiřazení záznamu a přidání obsluhy kliknutí na spodní obdelník, název kategorie a název transakce
            FinanceRecord = record;
            RectangleDown.MouseDown += RectangleDown_MouseDown;
            Title.MouseDown += RectangleDown_MouseDown;
            CategoryName.MouseDown += RectangleDown_MouseDown;
        }

        /// <summary>
        /// Obsluha události kliknutí na obdelník, vyvolá se událost celé této třídy, tedy FinanceGraphicRecord
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RectangleDown_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            GraphicRecordClick(FinanceRecord, EventArgs.Empty);
        }

        /// <summary>
        /// Vrátí všechny části grafického záznamu
        /// </summary>
        /// <returns></returns>
        public List<object> ReturnAllAtributs()
        {
            List<object> recordParts = new List<object>();
            recordParts.Add(RectangleTop);
            recordParts.Add(RectangleDown);
            recordParts.Add(Price);
            recordParts.Add(Title);
            recordParts.Add(Date);
            recordParts.Add(CategoryName);

            return recordParts;
        }
    }
}
