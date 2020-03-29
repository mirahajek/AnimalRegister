using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace AnimalRegister.Pig.Logic
{
    public class Validator
    {
        private Canvas canvasPig;
        private Canvas canvasSaw;
        private Admin admin;
        private int recordsActualPage;

        public void DefineCanvas(Canvas canvasPig, Canvas canvasSaw)
        {
            if (canvasPig != null)
                this.canvasPig = canvasPig;
            else
                throw new ArgumentException("");

            if (canvasSaw != null)
                this.canvasSaw = canvasSaw;
            else
                throw new ArgumentException("");
        }

        public Validator()
        {
            recordsActualPage = 0;
            admin = new Admin();
        }

        public void ConstructGraphicSawList()
        {
            List<GraphicPigSawRecord> graphic = admin.ConstructGraphicSawList();

            int[] top =
            {
                140,170,
                142,175,200,225
            };

            int[] left =
            {
                10,10,
                15,30,30,30
            };
            int a = 0;
            int b = 0;
            foreach(GraphicPigSawRecord rec in graphic)
            {
                List<object> elements = rec.ReturnAllAtributs();
                foreach (object obj in elements)
                {
                    CanvasPositionAddObject(obj, canvasSaw, left[a], top[a] + b * 120, 0, 0);
                    a++;
                }
                a = 0;
                b++;
                
            }
            
        }

        public void ConstructGraphicRecords(bool first, bool rotate, bool rotateUp)
        {
            // Jedná se o první stranu denních záznamů
            //      - uložení hodnot z ComboBoxů -- rok a měsíc
            if (first)
            {
                recordsActualPage = 0;
            }
            // Přetečení - první strana a rotace nahoru -- poslední strana, tedy pátá
            if (recordsActualPage == 0 && rotate && rotateUp)
            {
                recordsActualPage = 4;
            }
            // Rotace kolečkem nahoru -- snížení aktuální strany
            else if (rotate && rotateUp)
            {
                recordsActualPage--;
            }

            // Aktuální strana 5 - a rotace dolů -- přetečení na první stranu, tedy Id - 0
            if (recordsActualPage == 4 && rotate && !rotateUp)
                recordsActualPage = 0;
            else if (rotate && !rotateUp)
            {
                recordsActualPage++;
            }
        }

        private void CanvasPositionAddObject(object element, Canvas canvas, int setLeft, int setTop, int setRight, int setBottom)
        {
            // Jedná se o obdelník
            if (element is Rectangle)
            {
                canvas.Children.Add(element as Rectangle);
                Canvas.SetLeft(element as Rectangle, setLeft);
                Canvas.SetTop(element as Rectangle, setTop);
                Canvas.SetRight(element as Rectangle, setRight);
                Canvas.SetBottom(element as Rectangle, setBottom);
            }
            // Jedná se o TextBlock  
            else if (element is TextBlock)
            {
                canvas.Children.Add(element as TextBlock);
                Canvas.SetLeft(element as TextBlock, setLeft);
                Canvas.SetTop(element as TextBlock, setTop);
                Canvas.SetRight(element as TextBlock, setRight);
                Canvas.SetBottom(element as TextBlock, setBottom);
            }
            // Jedná se o tlačítko
            else if (element is Button)
            {
                canvas.Children.Add(element as Button);
                Canvas.SetLeft(element as Button, setLeft);
                Canvas.SetTop(element as Button, setTop);
                Canvas.SetRight(element as Button, setRight);
                Canvas.SetBottom(element as Button, setBottom);
            }
        }
    }
}
