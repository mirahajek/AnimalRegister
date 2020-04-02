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
    public enum Category
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
    public enum TypePig
    {
        Saw,
        OtherPig
    }

    /// <summary>
    /// České názvy možností prasate
    /// </summary>
    public enum TypePig_Czech
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

            Saws.Add(new Saw(DateTime.Now, "Jsem pokusna svine 1", "Maruska","Nema nohu"));
            Saws.Add(new Saw(DateTime.Now, "Jsem pokusna svine 2", "Baruska", "Ma o nohu naivc"));
            Saws.Add(new Saw(DateTime.Now, "Jsem pokusna svine 3", "Haluska", "Mozna neexistuje"));
            Saws.Add(new Saw(DateTime.Now, "Jsem pokusna svine 4", "Kovadluska", "Pokus"));


            Pigs.Add(new Pig(DateTime.Now, "Prsatako 1", false, Saws[0]));
            Pigs.Add(new Pig(DateTime.Now, "ID pras 2", false, Saws[1]));
            Pigs.Add(new Pig(DateTime.Now, "Id pras 3", false, Saws[2]));
            Pigs.Add(new Pig(DateTime.Now, "ID pras 4", false, Saws[3]));
        }



        #region Graphic methods

        /// <summary>
        /// Vytvoří grafickou podobu chovných prasnic
        /// </summary>
        /// <returns></returns>
        public List<GraphicPigSawRecord> ConstructGraphicSawList()
        {
            if (Saws.Count > 0)
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
            else
                return null;
        }

        /// <summary>
        /// Vytvoří grafickou podobu ostatních prasat
        /// </summary>
        /// <returns></returns>
        public List<GraphicPigSawRecord> ConstructGraphicPigList()
        {
            if (Pigs.Count > 0)
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
            else
                return null;
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
        /// Přidání / úprava prasnice nebo prasete
        /// </summary>
        /// <param name="operation">0 - Nové prase, 1 - úprava stávajcího</param>
        /// <param name="typePig">Prasnice / ostatní prase</param>
        /// <param name="motherId">Id v ComboBoxu pro výběr matky</param>
        /// <param name="dateBorn">Datum narození prasete</param>
        /// <param name="registerNumber">Registrační číslo prasete</param>
        /// <param name="name">Pojmenování prasete</param>
        /// <param name="description">Podrobný popis prasete</param>
        public void AddEditSawPig(byte operation, TypePig type, int motherId, DateTime dateBorn, string registerNumber, string name, string description, Pig editPig)
        {
            // Nové zvíře
            if(operation == 0)
            {
                Saw mother = Saws[motherId];
                // Prasnice
                if (type == TypePig.Saw)
                {
                    Saws.Add(new Saw(dateBorn, registerNumber, name, description));
                }
                // Ostatní
                else
                {
                    Pigs.Add(new Pig(dateBorn, registerNumber, true, mother, name, description));
                }
            }
            // Úprava stávajícího
            else if(operation == 1)
            {
                // Prasnice
                if (editPig is Saw)
                { 
                    // Změna parametrů konkrétní PRASNICE
                    Saw editSaw = editPig as Saw;
                    editSaw.Name = name;
                    editSaw.RegisterNumber = registerNumber;
                    editSaw.Born = dateBorn;
                    editSaw.Description = description;
                }
                // Ostatní
                else
                {
                    Saw mother = Saws[motherId];

                    editPig.Name = name;
                    editPig.RegisterNumber = registerNumber;
                    editPig.Born = dateBorn;
                    editPig.Description = description;
                    editPig.Mother = mother;
                }
            }
        }




        #endregion


        #region Save/ Load



        #endregion

    }
}
