using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalRegister.Pig.Logic
{
    /// <summary>
    /// Kategorie pro finance
    /// </summary>
    enum Category
    {
        Feeding,
        Buildings,
        Equipment,
        Animals,
        Plant,
    }

    /// <summary>
    /// Enum pro výběr, zda se jedná o Prasnici nebo ostatní prase k chovu
    /// </summary>
    enum TypePig
    {
        Saw,
        OtherPig
    }

    /// <summary>
    /// České názvy možností prasate
    /// </summary>
    enum TypePig_Czech
    {
        Prasnice,
        Ostatni
    }

    /// <summary>
    /// Třída správce, kde bude veškerá logika
    /// </summary>
    public class Admin
    {
        /// <summary>
        /// Kolekce prasat v chovu
        /// </summary>
        public List<Pig> Pigs { get; private set; }

        /// <summary>
        /// Kolekce chovných prasnic
        /// </summary>
        public List<Saw> Saws { get; private set; }
        /// <summary>
        /// Konstruktor
        /// </summary>
        public Admin()
        {
            // Takto ziskam pocet prvku v ENUM
            int a = Enum.GetValues(typeof(Category)).Length;
            Veterinary vet = new Veterinary();
            Pigs = new List<Pig>();
            Saws = new List<Saw>();

            Saws.Add(new Saw(DateTime.Now, "Jsm sd pokusna svine 123", "Baaruska","Pokus"));
            Saws.Add(new Saw(DateTime.Now, "Jsm sd pokusna svine 123", "Baaruska", "Pokus"));
            Saws.Add(new Saw(DateTime.Now, "Jsm sd pokusna svine 123", "Baaruska", "Pokus"));
            Saws.Add(new Saw(DateTime.Now, "Jsm sd pokusna svine 123", "Baaruska", "Pokus"));


            Pigs.Add(new Pig(DateTime.Now, "Ahj 123 456 555", false, null));
            Pigs.Add(new Pig(DateTime.Now, "Ahj 123 456 555", false, null));
            Pigs.Add(new Pig(DateTime.Now, "Ahj 123 456 555", false, null));
            Pigs.Add(new Pig(DateTime.Now, "Ahj 123 456 555", false, null));
        }



        #region Graphic methods

        /// <summary>
        /// Vytvoří grafickou podobu chovných prasnic
        /// </summary>
        /// <returns></returns>
        public List<GraphicPigSawRecord> ConstructGraphicSawList()
        {
            List<GraphicPigSawRecord> graphicRecords = new List<GraphicPigSawRecord>();
            foreach (Saw saw in Saws)
            {
                graphicRecords.Add(new GraphicPigSawRecord(saw));
            }
            // Nastavení strany pro každý jednotlivý záznam
            UpdateSetPage(graphicRecords);
            return graphicRecords;
        }

        /// <summary>
        /// Vytvoří grafickou podobu ostatních prasat
        /// </summary>
        /// <returns></returns>
        public List<GraphicPigSawRecord> ConstructGraphicPigList()
        {
            List<GraphicPigSawRecord> graphicRecords = new List<GraphicPigSawRecord>();
            foreach (Pig pig in Pigs)
            {
                graphicRecords.Add(new GraphicPigSawRecord(pig));
            }
            // Nastavení strany pro každý jednotlivý záznam
            UpdateSetPage(graphicRecords);
            return graphicRecords;
        }

        /// <summary>
        /// Metoda, která každé grafické podobě zvířete přidá stránku na které bude vykreslen
        /// </summary>
        /// <param name="graphicAnimals">Kolekce grafických zvířat, které se budou číslovat</param>
        private void UpdateSetPage(List<GraphicPigSawRecord> graphicAnimals)
        {
            if (graphicAnimals != null)
            {
                int[] pageGraphicRecord = new int[5];
                int i = 0;
                foreach (GraphicPigSawRecord animal in graphicAnimals)
                {
                    // Čtyři záznamy na stranu
                    if (pageGraphicRecord[i] < 4)
                    {
                        pageGraphicRecord[i]++;
                        animal.Page = i;

                        if (pageGraphicRecord[i] % 4 == 0)
                            i++;
                    }
                }

            }
        }

        #endregion

        #region Add/Edit/Remove Pig or Saw

        /// <summary>
        /// Přidání nového kusu prasete pro výkrm apod.
        /// </summary>
        /// <param name="choice">0 - Nové zvíře (zákl. údaje), 1 - Nové zvíře (kompletní údaje), 2 - Úprava stávajícího zvířete</param>
        /// <param name="born">Datum narození</param>
        /// <param name="registerNumber">Evidenční číslo dle chovatele</param>
        /// <param name="sex">Pohlaví</param>
        /// <param name="mother">Matka</param>
        /// <param name="name">Pojmenování zvířete</param>
        /// <param name="description">Popis</param>
        public void AddEditPig(byte choice, DateTime born, string registerNumber, bool sex, Saw mother, string name, string description)
        {
            if (choice == 0)
                Pigs.Add(new Pig(born, registerNumber, sex, mother));
            else if (choice == 1)
            {

            }
        }

        /// <summary>
        /// Přidání nového kusu prasete pro výkrm apod.
        /// </summary>
        /// <param name="choice">0 - Nové zvíře (zákl. údaje), 1 - Nové zvíře (kompletní údaje), 2 - Úprava stávajícího zvířete</param>
        /// <param name="born">Datum narození</param>
        /// <param name="registerNumber">Evidenční číslo dle chovatele</param>
        /// <param name="name">Pojmenování zvířete</param>
        /// <param name="description">Popis</param>
        public void AddEditSaw(byte choice, DateTime born, string registerNumber, string name, string description)
        {
            if (choice == 0)
                Saws.Add(new Saw(born, registerNumber));
            else if (choice == 1)
            {

            }
        }



        #endregion


        #region Save/ Load



        #endregion

    }
}
