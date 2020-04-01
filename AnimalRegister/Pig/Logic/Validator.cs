﻿using System;
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
        /// <summary>
        /// Plátno pro vykreslení ostatních prasat
        /// </summary>
        private Canvas canvasPig;
        /// <summary>
        /// Plátno pro vykreslení prasnic
        /// </summary>
        private Canvas canvasSaw;
        /// <summary>
        /// Instance správce aplikace
        /// </summary>
        private Admin admin;
        /// <summary>
        /// Aktuální strana výpisu prasat a prasnic - úvodní strana
        /// </summary>
        private int recordsActualPage;

        /// <summary>
        /// Nastavení canvasů pro vykreslení informací - plátno pro ostatní prasat a plátno pro prasnice
        /// </summary>
        /// <param name="canvasPig">Plátno pro ostatní prasata</param>
        /// <param name="canvasSaw">Plátno pro prasnice</param>
        public void DefineCanvas(Canvas canvasPig, Canvas canvasSaw)
        {
            if (canvasPig != null)
                this.canvasPig = canvasPig;
            else
                throw new ArgumentException("Nepodařilo se načíst plátno pro vykreslení informací. Prosím restartujte aplikaci.");

            if (canvasSaw != null)
                this.canvasSaw = canvasSaw;
            else
                throw new ArgumentException("Nepodařilo se načíst plátno pro vykreslení informací. Prosím restartujte aplikaci.");
        }

        public Validator()
        {
            recordsActualPage = 0;
            admin = new Admin();
        }

        public VM_PigSaw DefineVM_PigSaw()
        {
            List<string> mothersName = new List<string>();
            foreach(Saw saw in admin.Saws)
            {
                mothersName.Add(saw.Name);
            }

            return new VM_PigSaw(null, null, mothersName);
        }

        /// <summary>
        /// Přidání / úprava prasnice nebo prasete
        /// </summary>
        /// <param name="typePig">Prasnice / ostatní prase</param>
        /// <param name="motherId">Id v ComboBoxu pro výběr matky</param>
        /// <param name="dateBorn">Datum narození prasete</param>
        /// <param name="registerNumber">Registrační číslo prasete</param>
        /// <param name="name">Pojmenování prasete</param>
        /// <param name="description">Podrobný popis prasete</param>
        public void AddEditSawPig(int typePig, int motherId, string dateBorn, string registerNumber, string name, string description)
        {

        }


        /// <summary>
        /// Vloží záznamy prasnice na plátno a zarovná je
        /// </summary>
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
