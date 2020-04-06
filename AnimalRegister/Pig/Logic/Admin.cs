using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Serialization;

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
    /// Pohlaví prase ** 0 - Prasnice a 1 - Kanec
    /// </summary>
    public enum Sex
    {
        Saw,
        Boar
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

        private string pathBase;
        private string pathId;
        private string pathPigs;
        private string pathSaws;
        /// <summary>
        /// Konstruktor
        /// </summary>
        public Admin()
        {
            // Takto ziskam pocet prvku v ENUM
            int a = Enum.GetValues(typeof(Category)).Length;

            Pigs = new List<Pig>();
            Saws = new List<Saw>();

            /*
            Saws.Add(new Saw(DateTime.Now, "Jsem pokusna svine 1", "Maruska","Nema nohu"));
            Saws.Add(new Saw(DateTime.Now, "Jsem pokusna svine 2", "Baruska", "Ma o nohu naivc"));
            Saws.Add(new Saw(DateTime.Now, "Jsem pokusna svine 3", "Haluska", "Mozna neexistuje"));
            Saws.Add(new Saw(DateTime.Now, "Jsem pokusna svine 4", "Kovadluska", "Pokus"));


            Pigs.Add(new Pig(DateTime.Now, "Prsatako 1", Sex.Boar, Saws[0]));
            Pigs.Add(new Pig(DateTime.Now, "ID pras 2", Sex.Boar, Saws[1]));
            Pigs.Add(new Pig(DateTime.Now, "Id pras 3", Sex.Saw, Saws[2]));
            Pigs.Add(new Pig(DateTime.Now, "ID pras 4", Sex.Saw, Saws[3]));
            */

            try
            {
                pathBase = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "AnimalRegister");
                if (!Directory.Exists(pathBase))
                    Directory.CreateDirectory(pathBase);

                pathId = Path.Combine(pathBase, "IDs");
                pathPigs = Path.Combine(pathBase, "Pigs");
                pathSaws = Path.Combine(pathBase, "Saws");
            }
            catch 
            {
                MessageBox.Show("Nepodařilo se načíst složku pro získání dat aplikace", "Pozor", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            LoadIDs();
            LoadPigSaws();
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
        /// <param name="type">Prasnice / ostatní prase</param>
        /// <param name="motherId">Id v ComboBoxu pro výběr matky</param>
        /// <param name="dateBorn">Datum narození prasete</param>
        /// <param name="registerNumber">Registrační číslo prasete</param>
        /// <param name="name">Pojmenování prasete</param>
        /// <param name="description">Podrobný popis prasete</param>
        /// <param name="editPig">Upravované prase</param>
        /// <param name="sex">Pohlaví 0 - Prasnice, 1 - Kanec</param>
        public void AddEditSawPig(byte operation, TypePig type, int motherId, DateTime dateBorn, string registerNumber, string name, string description, Pig editPig, Sex sex)
        {
            // Nové zvíře
            if(operation == 0)
            {
                // Prasnice
                if (type == TypePig.Saw)
                {
                    Saws.Add(new Saw(dateBorn, registerNumber, name, description));
                }
                // Ostatní
                else
                {
                    Saw mother = Saws[motherId];
                    Pigs.Add(new Pig(dateBorn, registerNumber,sex, mother, name, description));
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
                    // Vyhledání konkrétní matky v seznamu PRASNIC
                    Saw mother = Saws[motherId];
                    // Úprava hodnot zadaného prasate
                    editPig.Name = name;
                    editPig.RegisterNumber = registerNumber;
                    editPig.Born = dateBorn;
                    editPig.Description = description;
                    editPig.Mother = mother;
                }
            }

            SaveIDs();
            SavePigSaws();
        }

        /// <summary>
        /// Odebrání záznamu zvířete
        /// </summary>
        /// <param name="removedPig">Odebíráné zvíře</param>
        public void RemoveSawPig(Pig removedPig)
        {
            if (removedPig != null)
            {
                if(removedPig is Saw)
                {
                    Saw removedSaw = (Saw)removedPig;
                    // Vyhledání prasnice z kolekce, které odpovídá ID
                    var query = (from s in Saws
                                 where (s.Id == removedSaw.Id)
                                 select s).First();
                    // Odebrání prasnice z kolekce
                    Saw saw = query;
                    if (saw != null)
                    {
                        Saws.Remove(saw);
                    }
                }
                else
                {
                    // Vyhledání prasete z kolekce, které odpovídá ID
                    var query = (from p in Pigs
                                 where (p.Id == removedPig.Id)
                                 select p).First();
                    // Odebrání prasete z kolekce
                    Pig pig = query;
                    if (pig != null)
                    {
                        Pigs.Remove(pig);
                    }
                }
            }
            // Uložení ID a prasat
            SaveIDs();
            SavePigSaws();
        }
        #endregion

        #region Add/Edit/Remove BirthRecords

        /// <summary>
        /// Metoda pro přidání nebo úpravu záznamu porodu
        /// </summary>
        /// <param name="operation">0 - nový záznam, 1 - úprava stávajícího</param>
        /// <param name="dateRecessed">Datum zapuštění</param>
        /// <param name="live">Počet živých selat při porodu</param>
        /// <param name="death">Počet mrtvých selat při porodu</param>
        /// <param name="reared">Počet odchovaných selat při odstavu</param>
        /// <param name="dateBirthReal">Skutečné datum porodu</param>
        /// <param name="pregnancyCheck">Kontrola březosti - Ano/Ne</param>
        /// <param name="editRecord">Upravovaný záznam</param>
        public void AddEditBirth(byte operation, DateTime dateRecessed, int live, int death, int reared, DateTime dateBirthReal, bool pregnancyCheck, Birth editRecord
            ,Pig relationalPig)
        {
            Saw saw = (Saw)relationalPig;
            if (operation == 0)
                saw.BirthRecords.Add(new Birth(dateRecessed, live, death, reared, dateBirthReal, pregnancyCheck));
            else
            {
                editRecord.DateRecessed = dateRecessed;
                editRecord.Live = live;
                editRecord.Death = death;
                editRecord.Reared = reared;
                editRecord.DateBirth_real = dateBirthReal;
                editRecord.PregnancyCheck = pregnancyCheck;
            }

            SaveAll();
        }

        /// <summary>
        /// Odebrání záznamu o porodu u nějaké prasnice
        /// </summary>
        /// <param name="record">Odebíráný záznam o porodu</param>
        /// <param name="saw">Vztažná prasnice, která obsahuje zadaný záznam</param>
        public void RemoveBirth(Birth record, Saw saw)
        {
            saw.BirthRecords.Remove(record);

            SaveAll();
        }

        #endregion

        #region Add/Edit/Remove Veterinary
        /// <summary>
        /// Metoda pro přidání / úpravu veterinárního záznamu
        /// </summary>
        /// <param name="operation">0 - nový, 1 - úprava</param>
        /// <param name="date">Datum návštěvy veterináře</param>
        /// <param name="price">Částka zaplacená za ošetření</param>
        /// <param name="purpose">Účel návstěvy veterináře</param>
        /// <param name="drugs">Podané léčivo</param>
        /// <param name="tasks">Provedené úkony a další poznámky k záznamu</param>
        /// <param name="editPig">Vztažné prase</param>
        public void AddEditVeterinary(int operation, DateTime date, int price, string purpose, string drugs, string tasks, Pig editPig, Veterinary record)
        {
            if(operation == 0)
            {
                editPig.VeterinaryRecords.Add(new Veterinary(date, price, purpose, tasks, drugs));
            }
            else if (operation == 1)
            {
                record.Date = date;
                record.Price = price;
                record.Purpose = purpose;
                record.Drugs = drugs;
                record.Tasks = tasks;
            }

            SaveAll();
        }

        /// <summary>
        /// Metoda pro odebrání veterinárního záznamu
        /// </summary>
        /// <param name="record">Odebíraný veterinární záznam</param>
        public void RemoveVeterinary(Veterinary record, Pig pig)
        {
            pig.VeterinaryRecords.Remove(record);

            SaveAll();
        }


        #endregion

        #region Save/ Load

        /// <summary>
        /// Uloží všechna data aplikace
        /// </summary>
        public void SaveAll()
        {
            SaveIDs();
            SavePigSaws();
        }

        /// <summary>
        /// Uložení ID - prasat, porodů a veterinárních záznamů
        /// </summary>
        private void SaveIDs()
        {
            using(StreamWriter writter = new StreamWriter(pathId))
            {
                writter.WriteLine(Pig.ID);
                writter.WriteLine(Birth.ID);
                writter.WriteLine(Veterinary.ID);
            }
        }

        /// <summary>
        /// Uložení prasat a prasnic na C- appData
        /// </summary>
        private void SavePigSaws()
        {
            // Serializéry pro prasata a prasnice
            XmlSerializer xml_pig = new XmlSerializer(Pigs.GetType());
            XmlSerializer xml_saw = new XmlSerializer(Saws.GetType());
            // Prasnice
            using (StreamWriter writter = new StreamWriter(pathSaws))
            {
                xml_saw.Serialize(writter, Saws);
            }
            // Ostatní
            using (StreamWriter writter = new StreamWriter(pathPigs))
            {
                xml_pig.Serialize(writter, Pigs);
            }
        }

        /// <summary>
        /// Načtení IDs - Prase, porody a veterina
        /// </summary>
        private void LoadIDs()
        {
            using (StreamReader reader = new StreamReader(pathId))
            {
                int.TryParse(reader.ReadLine(),out Pig.ID);
                int.TryParse(reader.ReadLine(), out Birth.ID);
                int.TryParse(reader.ReadLine(), out Veterinary.ID);
            }
        }

        /// <summary>
        /// Načtení dat o PRASNICE a Ostatní prasata
        /// </summary>
        private void LoadPigSaws()
        {
            XmlSerializer xml_pig = new XmlSerializer(Pigs.GetType());
            XmlSerializer xml_saw = new XmlSerializer(Saws.GetType());
            // Prasnice
            if (File.Exists(pathSaws))
            {
                using (StreamReader reader = new StreamReader(pathSaws))
                {
                    Saws = (List<Saw>)xml_saw.Deserialize(reader);
                }
            }
            else
                Saws = new List<Saw>();

            // Ostatní prasata
            if (File.Exists(pathPigs))
            {
                using (StreamReader reader = new StreamReader(pathPigs))
                {
                    Pigs = (List<Pig>)xml_pig.Deserialize(reader);
                }
            }
            else
                Pigs = new List<Pig>();

        }

        #endregion

    }
}
