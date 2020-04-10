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
    /// Třída reprezentující grafickou podobu jednoho záznamu o praseti
    /// </summary>
    public class GraphicPigSawRecord
    {
        /// <summary>
        /// Událost informující o kliknutí na grafický záznam
        /// </summary>
        public event EventHandler RecordClick;
        /// <summary>
        /// Horní tmavý obdelník
        /// </summary>
        private Rectangle top;
        /// <summary>
        /// Spodní světlý obdelník
        /// </summary>
        private Rectangle down;
        /// <summary>
        /// Název prasete - nahoře vlevo
        /// </summary>
        private TextBlock name;
        /// <summary>
        /// Registrační číslo prasete - první na světlém bloku
        /// </summary>
        private TextBlock registerNumber;
        /// <summary>
        /// Datum narození prasete - druhý na světlém bloku
        /// </summary>
        private TextBlock born;
        /// <summary>
        /// Věk ve dnech - třetí údaj na světlém bloku
        /// </summary>
        private TextBlock age;
        /// <summary>
        /// Instance zvířete, kterého se daná grafická podoba týká
        /// </summary>
        public Pig Animal { get; private set; }
        /// <summary>
        /// Strana na které bude záznam vykreslen
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// Základní konstruktor - vytvoří vše potřebné pro tuto třídu
        /// </summary>
        /// <param name="animal">Instance zvířete, které se bude zobrazovat</param>
        public GraphicPigSawRecord(Pig animal)
        {
            Animal = animal;

            top = new Rectangle
            {
                Width = 380,
                Height = 30,
                RadiusX = 2,
                RadiusY = 2,
                Fill = new SolidColorBrush(Color.FromArgb(255,111,121,182)),
                StrokeThickness = 1,
                Stroke = Brushes.Gray
            };

            down = new Rectangle
            {
                Width = 380,
                Height = 80,
                RadiusX = 2,
                RadiusY = 2,
                Fill = new SolidColorBrush(Color.FromArgb(255,239,241,255)),
                StrokeThickness = 1,
                Stroke = Brushes.Gray
            };

            name = new TextBlock
            {
                FontSize = 17,
                FontWeight = FontWeights.DemiBold,
                Foreground = Brushes.Black,
                Text = Animal.Name

            };

            registerNumber = new TextBlock
            {
                FontSize = 14,
                FontWeight = FontWeights.DemiBold,
                Foreground = Brushes.Black,
                Text = "Evidenční číslo:\t\t" + Animal.RegisterNumber

            };

            born = new TextBlock
            {
                FontSize = 14,
                FontWeight = FontWeights.DemiBold,
                Foreground = Brushes.Black,
                Text = "Datum narození:\t\t" + Animal.Born.ToShortDateString()

            };

            age = new TextBlock
            {
                FontSize = 14,
                FontWeight = FontWeights.DemiBold,
                Foreground = Brushes.Black,
                Text = "Věk:\t\t\t" + Animal.Age.ToString() + " dni"

            };
            // Kliknutí na spodní obdelník
            down.MouseDown += Down_MouseDown;
        }

        /// <summary>
        /// Obsluha události kliknutí na spodní obdelník, vyvolá se událost celé třídy, kterou odchytne správce
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Down_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            RecordClick(Animal, EventArgs.Empty);
        }

        /// <summary>
        /// Přidá všechny vlastnosti třídy do jedné kolekce, tak aby bylo možné objekty přidat na CANVS
        /// </summary>
        /// <returns>Kolekci všech vlastností této třídy 6 prvků -- 0 a 1 -> Obdelníky - Top a Down ** 2 - 5 -> Text - Jméno, Ev. číslo, Narození a Věk</returns>
        public List<object> ReturnAllAtributs()
        {
            List<object> result = new List<object>
            {
                top,
                down,
                name,
                registerNumber,
                born,
                age
            };

            return result;
        }
    }
}
