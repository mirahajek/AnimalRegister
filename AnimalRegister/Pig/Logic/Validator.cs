using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using AnimalRegister.Pig.Winds;

namespace AnimalRegister.Pig.Logic
{
    public class Validator
    {
        /// <summary>
        /// Instance správce aplikace
        /// </summary>
        private Admin admin;

        private Graphic graphic;

        private Pig editPig; 
        /// <summary>
        /// Nastavení canvasů pro vykreslení informací - plátno pro ostatní prasat a plátno pro prasnice
        /// </summary>
        /// <param name="canvasPig">Plátno pro ostatní prasata</param>
        /// <param name="canvasSaw">Plátno pro prasnice</param>
        public void DefineCanvas(Canvas canvasPig, Canvas canvasSaw)
        {
            if (canvasPig != null && canvasSaw != null)
                graphic.DefineCanvas(canvasPig, canvasSaw);
            else
                throw new ArgumentException("Nepodařilo se načíst plátno pro vykreslení informací. Prosím restartujte aplikaci.");
        }

        /// <summary>
        /// Základní konstruktor
        /// </summary>
        public Validator()
        {
            
            admin = new Admin();
            graphic = new Graphic();

            AddEditBirth(0, "1.1.2020", "", "", "", "", 0, null, null);
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


        /// <summary>
        /// Vykreslí na canvasy jak Prasnice, tak i Ostatní prasata
        /// </summary>
        /// <param name="first">Jedná se o první stranu</param>
        /// <param name="rotate">Uživatel orotoval kolečko</param>
        /// <param name="rotateUp">Uživatel otočil kolečkem nahoru</param>
        public void ConstructGraphicPigSawList(bool first, bool rotate, bool rotateUp)
        {
            // Kolekce grafických záznamů, které budou vykresleni na plátno
            List<GraphicPigSawRecord> graphicSaw = admin.ConstructGraphicSawList();
            List<GraphicPigSawRecord> graphicPig = admin.ConstructGraphicPigList();
            // Přidá obsluhu události při kliknutí na záznam
            foreach (GraphicPigSawRecord rec in graphicSaw)
            {
                rec.RecordClick += GraphicRecordClick;
            }
            // Přidá obsluhu události při kliknutí na záznam
            foreach(GraphicPigSawRecord rec in graphicPig)
            {
                rec.RecordClick += GraphicRecordClick;
            }
            // Zavolá metodu třídy graphic, která přidá všechny záznamy na plátna
            graphic.ConstructGraphicPigSawList(first, rotate, rotateUp, graphicSaw, graphicPig);
        }

        #region Add/Edit/Remove PigSaw
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

            Sex sex_help = Sex.Saw;
            if (typePig == 1)
            {
                // Ošetření výběru pohlaví
                if (sex == -1)
                    throw new ArgumentException("Nevybral jsi pohlaví zvířete");
                else
                    sex_help = (Sex)sex;
            }

            // Nové zvíře
            if (operation == 0)
            {
                admin.AddEditSawPig(0, type, motherId, born, registerNumber, name, description,null, sex_help);
            }
            // Úprava stávajícího
            else if (operation == 1 && editPig != null)
            {
                admin.AddEditSawPig(1, type, motherId, born, registerNumber, name, description,editPig, sex_help);
            }
            else
                throw new ArgumentException("Nelze provést zadanou operaci. Lze pouze 0 nebo 1 (Nový záznam / úprava)");

            ConstructGraphicPigSawList(true, false, false);
        }

        /// <summary>
        /// Odebrání záznamu zvířete
        /// </summary>
        /// <param name="removedPig">Odebíráné zvíře</param>
        public void RemoveSawPig()
        {
            if (editPig != null)
                admin.RemoveSawPig(editPig);
            else
                throw new ArgumentException("Musíš nejprve vybrat prase, které chceš odebrat!");

            ConstructGraphicPigSawList(true,false,false);

        }

        #endregion

        #region Add/Edit/Remove BirthRecord

        /// <summary>
        /// Metoda pro přidání nebo úpravu záznamu porodu
        /// </summary>
        /// <param name="operation">0 - nový záznam, 1 - úprava stávajícího</param>
        /// <param name="dateRecessed">Datum zapuštění</param>
        /// <param name="live">Počet živých selat při porodu</param>
        /// <param name="death">Počet mrtvých selat při porodu</param>
        /// <param name="reared">Počet odchovaných selat při odstavu</param>
        /// <param name="dateBirthReal">Skutečné datum porodu</param>
        /// <param name="pregnancyCheck">Kontrola březosti</param>
        /// <param name="editRecord">Upravovaný záznam</param>
        public void AddEditBirth(byte operation, string dateRecessed, string live, string death, string reared, string dateBirthReal,
            int pregnancyCheck, Birth editRecord, Pig relationalPig)
        {
            // Pomocné proměnné
            bool pregnancyCheck_help = false;
            DateTime dateRecessed_help = DateTime.Now;

            // Datum porodu skutečné - kontrola pouze až uživatel něco zadá
            if (!DateTime.TryParse(dateBirthReal, out DateTime dateBirthReal_help) && dateBirthReal != "")
                throw new ArgumentException("Zadal jsi datum porodu, ve špatném formátu. Má vypadat jako 12.10.2020");

            // Datum zapuštění - povinný parametr
            if (dateRecessed == "")
                throw new ArgumentException("Nezadal jsi datum zapuštění!");
            else if (!DateTime.TryParse(dateRecessed, out dateRecessed_help))
                throw new ArgumentException("Zadal jsi datum zapuštění ve špatném formátu. Má vypadat jako 12.10.2020");

            // Ošetření situace, kdy uživatel něco zadal, ale nepodařilo se to převést - nepovinné parametry, proto jsou zkoumány až když je zapsáno
            if (!int.TryParse(live, out int live_help) && live != "")
                throw new ArgumentException("Nezadal jsi číslo do počtu živých selat! Zkus to znovu");

            if (!int.TryParse(death, out int death_help) && death != "")
                throw new ArgumentException("Nezadal jsi číslo do počtu mrtvých selat! Zkus to znovu");

            if (!int.TryParse(reared, out int reared_help) && reared != "")
                throw new ArgumentException("Nezadal jsi číslo do počtu odchovaných selat! Zkus to znovu");
            //Kontrola březosti
            if(pregnancyCheck == 1)
                pregnancyCheck_help = true;
            

            if (operation == 0 && relationalPig != null)
            {
                admin.AddEditBirth(0, dateRecessed_help, live_help, death_help, reared_help, dateBirthReal_help, pregnancyCheck_help, null, relationalPig);
            }
            else if(operation == 1 && relationalPig != null && editRecord != null)
            {

            }

        }

        #endregion

        /// <summary>
        /// Uloží všechna data aplikace
        /// </summary>
        public void SaveAll()
        {
            admin.SaveAll();
        }
    }
}
