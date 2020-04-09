using AnimalRegister.Pig.Winds;
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
    /// Třída starající se o vykreslení na plátna
    /// </summary>
    public class Graphic
    {
        /// <summary>
        /// Plátno pro vykreslení ostatních prasat
        /// </summary>
        private Canvas canvasPig;
        /// <summary>
        /// Plátno pro vykreslení prasnic
        /// </summary>
        private Canvas canvasSaw;
        /// <summary>
        /// Aktuální strana výpisu Prasnic, ostatních a transakcí - úvodní strana
        /// </summary>
        private int recordsActualPage;


        /// <summary>
        /// Základní konstruktor
        /// </summary>
        public Graphic()
        {
            recordsActualPage = 0;
        }

        /// <summary>
        /// Nastavení pláten pro vykreslování
        /// </summary>
        /// <param name="canvasPig"></param>
        /// <param name="canvasSaw"></param>
        public void DefineCanvas(Canvas canvasPig, Canvas canvasSaw)
        {
            this.canvasPig = canvasPig;
            this.canvasSaw = canvasSaw;
        }


        /// <summary>
        /// Vykreslí na canvasy jak Prasnice, Ostatní prasata i Finance
        /// </summary>
        /// <param name="operation">0 - zvířata * 1 - finance</param>
        /// <param name="first">Jedná se o první stranu</param>
        /// <param name="rotate">Uživatel orotoval kolečko</param>
        /// <param name="rotateUp">Uživatel otočil kolečkem nahoru</param>
        /// <param name="graphicSaw">Kolekce grafických záznamů Prasnic</param>
        /// <param name="graphicPig">Kolekce grafických záznamů Prasat</param>
        /// <param name="graphicFinance">Kolekce grafických financí</param>
        public void ConstructGraphicPigSawFinanceList(int operation ,bool first, bool rotate, bool rotateUp, List<GraphicPigSawRecord> graphicSaw, 
            List<GraphicPigSawRecord> graphicPig, List<FinanceGraphicRecord> graphicFinance)
        {
            // Jedná se o první stranu
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

            // Smazání pláten
            canvasSaw.Children.Clear();
            canvasPig.Children.Clear();

            // Pozadí na bílou barvu
            canvasSaw.Background = Brushes.White;
            canvasPig.Background = Brushes.White;
            // Prasata
            if (operation == 0)
            {
                // Souřadnice od vrchu pro všechny prvky grafického záznamu
                int[] top =
                {
                140,170,
                142,175,200,225
                };
                // Souřadnice od leva pro všechny prvky grafického záznamu
                int[] left =
                {
                10,10,
                15,30,30,30
                };
                int a = 0;
                int sawTop = 0;
                int pigTop = 0;

                DefineHead(0);
                // Vykreslení prasnic a ostatních
                List<GraphicPigSawRecord> pigColection = graphicSaw;
                pigColection.AddRange(graphicPig);

                foreach (GraphicPigSawRecord rec in pigColection)
                {
                    if (rec.Page == recordsActualPage)
                    {
                        List<object> elements = rec.ReturnAllAtributs();
                        foreach (object obj in elements)
                        {
                            if (rec.Animal is Saw)
                                CanvasPositionAddObject(obj, canvasSaw, left[a], top[a] + sawTop * 120, 0, 0); 
                            else
                                CanvasPositionAddObject(obj, canvasPig, left[a], top[a] + pigTop * 120, 0, 0);

                            a++;
                        }
                        a = 0;

                        if (rec.Animal is Saw)
                            sawTop++;
                        else
                            pigTop++;
                    }
                }
            }
            // Přidání finančních transakcí na plátno
            else if(operation == 1)
            {
                DefineHead(1);
                // Souřadnice od vrchu pro všechny prvky grafického záznamu
                int[] top =
                {
                110,135,
                112,140,112,140
                };
                // Souřadnice od leva pro všechny prvky grafického záznamu
                int[] left =
                {
                10,10,
                15,15,300, 300
                };
                // Vykresleni financi
                int incomeInc = 0;
                int costsInc = 0;
                foreach (FinanceGraphicRecord rec in graphicFinance)
                {
                    List<object> elements = rec.ReturnAllAtributs();
                    // Příjmy
                    if (rec.Page == recordsActualPage)
                    {
                        for (int i = 0; i < elements.Count; i++)
                        {
                            if(rec.FinanceRecord.TypeRecord == FinanceTypeRecord.Income)
                                CanvasPositionAddObject(elements[i], canvasSaw, left[i], top[i] + incomeInc * 65, 0, 0);
                            else if(rec.FinanceRecord.TypeRecord == FinanceTypeRecord.Costs)
                                CanvasPositionAddObject(elements[i], canvasPig, left[i], top[i] + costsInc * 65, 0, 0);
                        }
                        if (rec.FinanceRecord.TypeRecord == FinanceTypeRecord.Income)
                            incomeInc++;
                        else
                            costsInc++;
                    }
                }
            }
            
        }

        /// <summary>
        /// Metoda pro definice hlavičky záznamu
        /// </summary>
        /// <param name="type">0 - prasata, 1 - finance</param>
        private void DefineHead(byte type)
        {
            int height = 120;
            string[] titles = { "Prasnice", "Ostatní" };
            if (type == 1)
            {
                height = 100;
                titles[0] = "Příjmy";
                titles[1] = "Výdaje";
            }
                

            Rectangle rect = new Rectangle
            {
                Height = height,
                Width = 400,
                Fill = new SolidColorBrush(Color.FromArgb(255, 71, 75, 101))
            };

            TextBlock text = new TextBlock
            {
                Text = titles[0],
                Width = 400,
                Foreground = Brushes.White,
                FontWeight = FontWeights.Bold,
                FontSize = 35
            };

            Rectangle rectRight = new Rectangle
            {
                Height = height,
                Width = 400,
                Fill = new SolidColorBrush(Color.FromArgb(255, 71, 75, 101))
            };

            TextBlock textRight = new TextBlock
            {
                Text = titles[1],
                Width = 400,
                Foreground = Brushes.White,
                FontWeight = FontWeights.Bold,
                FontSize = 35
            };

            CanvasPositionAddObject(rect, canvasSaw, 0, 0, 0, 0);
            CanvasPositionAddObject(text, canvasSaw, 20, 35, 0, 0);

            CanvasPositionAddObject(rectRight, canvasPig, 0, 0, 0, 0);
            CanvasPositionAddObject(textRight, canvasPig, 20, 35, 0, 0);


        }

        /// <summary>
        /// Metoda pro zarovnání objektu na plátno, vyžaduje plátno, element a čtyři směry, kterými jej zarovná. Zkracuje přidání objektu na canvas
        /// </summary>
        /// <param name="element">Objekt, který se má přidat na plátno</param>
        /// <param name="canvas">Plátno </param>
        /// <param name="setLeft">Pozice od leva</param>
        /// <param name="setTop">Pozice od vrcholu</param>
        /// <param name="setRight">Pozice od prava</param>
        /// <param name="setBottom">Pozice od spodu</param>
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
