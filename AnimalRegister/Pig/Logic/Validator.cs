using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;
using AnimalRegister.Pig.Winds;

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


        private Pig editPig; 
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

        /// <summary>
        /// Základní konstruktor
        /// </summary>
        public Validator()
        {
            recordsActualPage = 0;
            admin = new Admin();
        }

        /// <summary>
        /// Metoda pro vytvoření View modelu Prase / Prasnice, pro jejich úpravu 
        /// </summary>
        /// <returns>View model</returns>
        public VM_PigSaw DefineVM_PigSaw(Pig pig)
        {
            List<string> mothersName = new List<string>();
            // Získání jmen všech prasnic v chovu
            foreach(Saw saw in admin.Saws)
            {
                mothersName.Add(saw.Name);
            }
            
            // Jedná se o úpravu stávajícího záznamu
            if(pig != null)
            {
                // Pokud se jedná o Prasnici, vytvoří se View model pro ni
                if (pig is Saw)
                    return new VM_PigSaw(null, pig as Saw, mothersName, -1);
                else
                {
                    // Nalezení id matky v kolekci PRasnic
                    int selectMotherId = admin.Saws.FindIndex(a => a.Id == pig.Mother.Id);
                    return new VM_PigSaw(pig, null, mothersName, selectMotherId);
                }
            }

            return new VM_PigSaw(mothersName);   
        }

        /// <summary>
        /// Přidání / úprava prasnice nebo prasete
        /// </summary>
        /// <param name="operation">0 - Nové prase, 1 - úprava stávajcího</param>
        /// <param name="typePig">Prasnice / ostatní prase</param>
        /// <param name="motherId">Id v ComboBoxu pro výběr matky</param>
        /// <param name="dateBorn">Datum narození prasete</param>
        /// <param name="registerNumber">Registrační číslo prasete</param>
        /// <param name="name">Pojmenování prasete</param>
        /// <param name="description">Podrobný popis prasete</param>
        public void AddEditSawPig(byte operation ,int typePig, int motherId, string dateBorn, string registerNumber, string name, string description, int sex)
        {
            //Pomocné proměnné
            DateTime born = DateTime.Now;
            TypePig type = (TypePig)typePig;
            // Uživatel nevybral prasnici ani ostatní
            if (typePig == -1)
                throw new ArgumentException("Nevybral jsi zda se jedná o prasnici / ostatní prase");
            // Uživatel vybral matku pro prasnici
            if (typePig == 0 && motherId != -1)
                throw new ArgumentException("Nemůžeš vybrat matku pro chovnou PRASNICI");

            // Ošetření datumu
            if (!DateTime.TryParse(dateBorn, out born))
                throw new ArgumentException("Zadal jsi datum ve špatném formátu. Má vypadat jako 12.10.2020");
            else if (dateBorn == "")
                throw new ArgumentException("Nezadal jsi datum narození");
            else if (born > DateTime.Now.AddDays(10))
                throw new ArgumentException("Zadal jsi datum do budoucnosti více jak 10 dní");
            // Ošetření evidenčního čísla
            if (registerNumber == "")
                throw new ArgumentException("Nezadal jsi žádné registrační číslo prasete");
            //
            Sex sex_help = Sex.Saw;
            if (sex == -1)
                throw new ArgumentException("Nevybral jsi pohlaví zvířete");
            else
                sex_help = (Sex)sex;

            // Nové zvíře
            if (operation == 0)
            {
                admin.AddEditSawPig(0, type, motherId, born, registerNumber, name, description,null, sex_help);
            }
            // Úprava stávajícího
            else if (operation == 1 && editPig != null)
            {
                admin.AddEditSawPig(1, type, motherId, born, registerNumber, name, description,editPig, sex_help);
                ConstructGraphicPigSawList();
            }
            else
                throw new ArgumentException("Nelze provést zadanou operaci. Lze pouze 0 nebo 1 (Nový záznam / úprava)");
        }

        /// <summary>
        /// Vykreslí na canvasy jak Prasnice, tak i Ostatní prasata
        /// </summary>
        /// <returns></returns>
        public void ConstructGraphicPigSawList()
        {
            // Kolekce grafických záznam pro PRASE i PRASNICE
            List<GraphicPigSawRecord> graphicSaw = admin.ConstructGraphicSawList();
            List<GraphicPigSawRecord> graphicPig = admin.ConstructGraphicPigList();
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
            int b = 0;

            // Vykreslení prasnic
            foreach (GraphicPigSawRecord rec in graphicSaw)
            {
                rec.RecordClick += GraphicRecordClick;
                List<object> elements = rec.ReturnAllAtributs();
                foreach (object obj in elements)
                {
                    CanvasPositionAddObject(obj, canvasSaw, left[a], top[a] + b * 120, 0, 0);
                    a++;
                }
                a = 0;
                b++;
            }

            a = 0; b = 0;
            // Vykreslení prasat
            foreach (GraphicPigSawRecord rec in graphicPig)
            {
                rec.RecordClick += GraphicRecordClick;
                List<object> elements = rec.ReturnAllAtributs();
                foreach (object obj in elements)
                {
                    CanvasPositionAddObject(obj, canvasPig, left[a], top[a] + b * 120, 0, 0);
                    a++;
                }
                a = 0;
                b++;
            }
        }

        /// <summary>
        /// Kliknutí na grafický záznam
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GraphicRecordClick(object sender, EventArgs e)
        {
            AddSawPig window = new AddSawPig(this, DefineVM_PigSaw(sender as Pig));
            editPig = sender as Pig;
            window.Show();
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
