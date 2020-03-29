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
    public class GraphicPigSawRecord
    {
        private Rectangle top;
        private Rectangle down;
        private Image leftImage;
        private TextBlock name;
        private TextBlock registerNumber;
        private TextBlock born;
        private TextBlock age;

        public Pig Animal { get; private set; }
        public int Page { get; set; }

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
