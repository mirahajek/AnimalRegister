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
    public enum FinanceCategory
    {
        Feeding,
        Buildings,
        Equipment,
        Animals,
        Plant,
        Machine,
        Pond,
        Other
    }

    /// <summary>
    /// Typ transakce VÝDAJE - 0  / PŘÍJMY - 1 / OSTATNI - 2
    /// </summary>
    public enum FinanceTypeRecord
    {
        Income,
        Costs,
        Other

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
        /// Kategorie pro finance - česky
        /// </summary>
        public static string[] FinanceCategory_Czech = { "Krmení", "Stavby", "Vybavení", "Zvířata", "Rostliny", "Stroje", "Rybník", "Ostatní" };

        /// <summary>
        /// Kolekce prasat v chovu
        /// </summary>
        public List<Pig> Pigs { get; private set; }

        /// <summary>
        /// Kolekce chovných prasnic
        /// </summary>
        public List<Saw> Saws { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public List<FinanceRecord> FinanceRecords { get; private set; }

        // Cesty pro uložení do souboru na disk C - appData - složka AnimalRegister
        private string pathBase;
        private string pathId;
        private string pathPigs;
        private string pathSaws;
        private string pathFinance;


        /// <summary>
        /// Konstruktor
        /// </summary>
        public Admin()
        {
            // Takto ziskam pocet prvku v ENUM
            int a = Enum.GetValues(typeof(FinanceCategory)).Length;

            Pigs = new List<Pig>();
            Saws = new List<Saw>();
            FinanceRecords = new List<FinanceRecord>();

            try
            {
                pathBase = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "AnimalRegister");
                if (!Directory.Exists(pathBase))
                    Directory.CreateDirectory(pathBase);

                pathId = Path.Combine(pathBase, "IDs");
                pathPigs = Path.Combine(pathBase, "Pigs");
                pathSaws = Path.Combine(pathBase, "Saws");
                pathFinance = Path.Combine(pathBase, "Finance");
            }
            catch 
            {
                MessageBox.Show("Nepodařilo se načíst složku pro získání dat aplikace", "Pozor", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            LoadIDs();
            LoadPigSaws();
            LoadFinance();
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
                UpdateSetPage(graphicRecords,null);
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
                UpdateSetPage(graphicRecords,null);
                return graphicRecords;
            }
            else
                return null;
        }

        /// <summary>
        /// Metoda vytvoří grafickou podobu všech transakcí uložených v kolekci
        /// </summary>
        /// <returns>Kolekce grafických financí</returns>
        public List<FinanceGraphicRecord> ConstructGraphicFinance()
        {
            if (FinanceRecords.Count > 0)
            {
                List<FinanceGraphicRecord> graphicRecords = new List<FinanceGraphicRecord>();
                var query = FinanceRecords.OrderByDescending(a => a.Date);
                foreach (FinanceRecord rec in query)
                {
                    graphicRecords.Add(new FinanceGraphicRecord(rec));
                }
                // Nastavení strany pro každý jednotlivý záznam
                UpdateSetPage(null,graphicRecords);
                return graphicRecords;
            }
            else
                return null;
        }

        /// <summary>
        /// Metoda, která každé grafické podobě zvířete přidá stránku na které bude vykreslen
        /// </summary>
        /// <param name="graphicAnimals">Kolekce grafických zvířat, které se budou číslovat</param>
        private void UpdateSetPage(List<GraphicPigSawRecord> graphicAnimals, List<FinanceGraphicRecord> graphicFinance)
        {
            // Přidání na canvas grafického vyjádření zvířete
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
            // Přidání na plátno grafické podoby transakcí
            else if (graphicAnimals == null && graphicFinance != null)
            {
                int[] pageGraphicRecord_income = new int[5];
                int[] pageGraphicRecord_costs = new int[5];
                int i = 0;
                int j = 0;
                foreach (FinanceGraphicRecord rec in graphicFinance)
                {
                    // Osm záznamy na stranu - jedná se příjmy
                    if (pageGraphicRecord_income[i] < 8 && rec.FinanceRecord.TypeRecord == FinanceTypeRecord.Income)
                    {
                        pageGraphicRecord_income[i]++;
                        rec.Page = i;

                        if (pageGraphicRecord_income[i] % 8 == 0)
                            i++;
                    }

                    // Osm záznamů na stranu - jedná se o výdaje
                    else if (pageGraphicRecord_costs[j] < 8 && rec.FinanceRecord.TypeRecord == FinanceTypeRecord.Costs)
                    {
                        pageGraphicRecord_costs[j]++;
                        rec.Page = j;

                        if (pageGraphicRecord_costs[j] % 8 == 0)
                            j++;
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
                    Saw mother = null;
                    if(motherId != -1)
                        mother = Saws[motherId];
   
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
                    Saw mother = null;
                    if (motherId != -1)
                        mother = Saws[motherId];
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

        #region

        /// <summary>
        /// Metoda pro přidání nebo úpravu transakce
        /// </summary>
        /// <param name="operation">0 - nový záznam * 1 - úprava stávajícího</param>
        /// <param name="date">Datum transakce</param>
        /// <param name="name">Název transakce * MAX 50 znaků</param>
        /// <param name="price">Částka transakce</param>
        /// <param name="description">Popis transakce</param>
        /// <param name="typeFinance">Příjmy / výdaje</param>
        /// <param name="category">Pořadí kategorie transakce</param>
        /// <param name="animalId">Pořadí zvířete, kterého se transakce týká</param>
        /// <param name="editRecord">Záznam pro úpravu</param>
        public void AddEditFinanceRecord(byte operation, DateTime date, string name, int price, string description, FinanceTypeRecord typeFinance, FinanceCategory category
            , int animalId, FinanceRecord editRecord)
        {
            if(operation == 0)
            {
                FinanceRecords.Add(new FinanceRecord(price, name, date, description, typeFinance, category, animalId));
            }
            else if(operation == 1)
            {
                editRecord.Date = date;
                editRecord.Name = name;
                editRecord.Price = price;
                editRecord.Description = description;

                editRecord.TypeRecord = typeFinance;
                editRecord.Category = category;
                editRecord.RelativeAnimalId = animalId;
            }

            SaveFinance();
            SaveIDs();
        }

        /// <summary>
        /// Metoda pro odebrání transakce
        /// </summary>
        /// <param name="removeRecord">Záznam k odebrání</param>
        public void RemoveFinanceRecord(FinanceRecord removeRecord)
        {
            FinanceRecords.Remove(removeRecord);

            SaveFinance();
            SaveIDs();
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
            SaveFinance();
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
                writter.WriteLine(FinanceRecord.Id);
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
        /// Uložení prasat a prasnic na C- appData
        /// </summary>
        private void SaveFinance()
        {
            XmlSerializer xml = new XmlSerializer(FinanceRecords.GetType());
            // Transakce
            using (StreamWriter writter = new StreamWriter(pathFinance))
            {
                xml.Serialize(writter, FinanceRecords);
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
                int.TryParse(reader.ReadLine(), out FinanceRecord.Id);
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

        /// <summary>
        /// Načtení dat o PRASNICE a Ostatní prasata
        /// </summary>
        private void LoadFinance()
        {
            XmlSerializer xml = new XmlSerializer(FinanceRecords.GetType());
            // Finance
            if (File.Exists(pathFinance))
            {
                using (StreamReader reader = new StreamReader(pathFinance))
                {
                    FinanceRecords = (List<FinanceRecord>)xml.Deserialize(reader);
                }
            }
            else
                FinanceRecords = new List<FinanceRecord>();
        }

        #endregion

    }
}
