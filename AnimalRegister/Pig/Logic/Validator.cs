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

        /// <summary>
        /// Instance třídy pro vykreslení na canvas
        /// </summary>
        private Graphic graphic;

        /// <summary>
        /// Instance prasete, na které uživatel kliknul na hlavní stránce
        /// </summary>
        private Pig editPig;

        /// <summary>
        /// Instance finančního záznamu na který uživatel kliknul na hlavní stránce
        /// </summary>
        private FinanceRecord editFinanceRecord;

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
        }

        #region VM logic
        /// <summary>
        /// Metoda pro vytvoření View modelu Prase / Prasnice, pro jejich úpravu 
        /// </summary>
        /// <returns>View model</returns>
        /// <param name="newPigFlag">Informace o tom, zda se jedná o nové prase nebo se upravuje stávající</param>
        public VM_PigSaw DefineVM_PigSaw(bool newPigFlag)
        {
            if (newPigFlag)
            {
                // Smazání informací o upravovaném praseti, protože uživatel si přeje přidat nové zvíře
                editPig = null;
            }
            // Kolekce obsahující jména všech prasnic v chovu
            List<string> mothersName = new List<string>();
            // Získání jmen všech prasnic v chovu
            foreach(Saw saw in admin.Saws)
            {
                mothersName.Add(saw.Name);
            }
            
            // Jedná se o úpravu stávajícího záznamu
            if(editPig != null)
            {
                // Pokud se jedná o Prasnici, vytvoří se View model pro ni
                if (editPig is Saw)
                    return new VM_PigSaw(null, editPig as Saw, mothersName, -1);
                else
                {
                    // Nalezení id matky v kolekci PRasnic
                    int selectMotherId = -1;
                    if (editPig.Mother != null)
                        selectMotherId = admin.Saws.FindIndex(a => a.Id == editPig.Mother.Id);

                    return new VM_PigSaw(editPig, null, mothersName, selectMotherId);
                }
            }
            else
                return new VM_PigSaw(mothersName);   
        }

        /// <summary>
        /// Metoda pro vytvoření View modelu Prase / Prasnice, pro jejich úpravu 
        /// </summary>
        /// <returns>View model</returns>
        public VM_Birth DefineVM_Birth()
        {
            List<Birth> sawBirth = new List<Birth>();
            if (editPig is Saw)
            {
                Saw mother = (Saw)editPig;
                List<Birth> records = new List<Birth>();

                var query = mother.BirthRecords.OrderByDescending(a => a.DateRecessed);

                foreach(Birth rec in query)
                {
                    records.Add(rec);
                }
                return new VM_Birth(records);
            }
            else
                throw new ArgumentException("Nelze zobrazit porody pro vybrané zvíře.");
        }

        /// <summary>
        /// Metoda pro vytvoření View modelu finance
        /// </summary>
        /// <returns>View model</returns>
        public VM_Finance DefineVM_Finance(bool newFinanceRecord)
        {
            List<Pig> pigs = new List<Pig>();
            pigs.AddRange(admin.Saws);
            pigs.AddRange(admin.Pigs);
            // Úprava stávajícího záznamu
            if (!newFinanceRecord && editFinanceRecord != null)
            {
                // Nalezení zvířete v kolekci, a uložení jeho pořadí - aby bylo možné v ComboBoxu zobrazit odpovídající řádek
                int pigIndex = pigs.FindIndex(a => a.Id == editFinanceRecord.RelativeAnimalId);
                return new VM_Finance(pigs, (int)editFinanceRecord.TypeRecord, (int)editFinanceRecord.Category, pigIndex, editFinanceRecord);
            }
            // View model pro nový záznam - tedy data pro comboBoxy pouze
            else
            {
                editFinanceRecord = null;
                return new VM_Finance(pigs);
            }
        }

        /// <summary>
        /// Vrátí kolekci veterinárních záznamů pro konkrétní zvíře
        /// </summary>
        /// <returns>Kolekce veterinárních záznamů - data pro ComboBox</returns>
        public List<Veterinary> Define_VeterinaryContext()
        {
            if (editPig != null)
            {
                List<Veterinary> records = new List<Veterinary>();
                var query = editPig.VeterinaryRecords.OrderByDescending(a => a.Date);
                foreach(Veterinary rec in query)
                {
                    records.Add(rec);
                }

                return records;
            }
            else
                return new List<Veterinary>();
        }

        /// <summary>
        /// Metoda pro získání všech prasnic + ostatních prasat, aby bylo možné je zobrazit v ComboBoxu finance - statistika
        /// </summary>
        /// <returns></returns>
        public List<Pig> Define_PigsList()
        {
            List<Pig> pigs = new List<Pig>();
            pigs.AddRange(admin.Saws);
            pigs.AddRange(admin.Pigs);

            return pigs;
        }
        #endregion


        public void CalculateStatisticData(Canvas statCanvas, string year, int categoryId, Pig relativeAnimal)
        {
            // Názvy textBlocků v okně statistiky, do kterých se budou vpisovat data o finančních přehledech
            string[] statisticTextBlockNames =
            {
                // Příjmy prvních 6 měsíců
                "januaryPlusTextBlock","februaryPlusTextBlock","marchPlusTextBlock","aprilPlusTextBlock","mayPlusTextBlock","junePlusTextBlock",
                // Příjmy druhých 6 měsíců
                "julyPlusTextBlock","augustPlusTextBlock","septemberPlusTextBlock","octoberPlusTextBlock","novemberPlusTextBlock","decemberPlusTextBlock",
                // Výdaje prvních 6 měsíců
                "januaryMinusTextBlock","februaryMinusTextBlock","marchMinusTextBlock","aprilMinusTextBlock","mayMinusTextBlock","juneMinusTextBlock",
                // Výdaje druhých 6 měsíců
                "julyMinusTextBlock","augustMinusTextBlock","septemberMinusTextBlock","octoberMinusTextBlock","novemberMinusTextBlock","decemberMinusTextBlock",
                // Celkové příjmy a výdaje
                "sumPlusTextBlock","sumMinusTextBlock"
            };
            // Uživatel má vybranou nějakou kategorii
            if (categoryId == -1)
                throw new ArgumentException("Nevybral jsi kategorii, pokud nechceš nějakou konkrétní zvol VŠECHNY");
            // Ošetření že jde rok převést na číslo
            if (!int.TryParse(year, out int year_help))
                throw new ArgumentException("Nepodařilo se převést vámi zadaný rok! Prosím opakujte výběr");
            // Kolekce kam se uloží data pro 12 příjmů, 12 výdajů a suma příjmu a výdajů -- celkem 26 položek
            List<int> result;

            // Uživatel nezměnil comboBox pro kategorii - volba ROK, KATEGORIE (všechny)
            // -------------------------------------------------------------------------
            // Rovná se sumě, protože k této kolekci se přidává ve VIEW ještě volba VŠECHNY vždy na konec této kolekce
            if (categoryId == Admin.FinanceCategory_Czech.Count())
            {
                result = admin.CalculateStatisticData(0, year_help, null, null);
            }
            // Uživatel vybral nějakou kategorii, ale ne zvíře - volba ROK, KATEGORIE
            else if (categoryId != 3)
            {
                result = admin.CalculateStatisticData(1, year_help, (FinanceCategory)categoryId, null);
            }
            // uživatel vybral určité zvíře - volba ROK, KATEGORIE a KONKRÉTNÍ ZVÍŘE
            else if(categoryId == 3 && relativeAnimal != null)
            {
                result = admin.CalculateStatisticData(2, year_help, (FinanceCategory)categoryId, relativeAnimal.Id);
            }
            // uživatel vybral kategorii zvířat, ale NE konkrétní zvíře
            else if (categoryId == 3 && relativeAnimal == null)
            {
                result = admin.CalculateStatisticData(1, year_help, (FinanceCategory)categoryId, null);
            }
            // Něco se pokazilo při výběru
            else
            {
                throw new ArgumentException("Něco se nepodařilo při výpočtu parametrů. Zkus výběr prosím znovu");
            }
            // Vykreslení na CANVAS - pouze se změní hodnoty textBlocků 
            for (int i = 0; i < 26; i++)
            {
                TextBlock text = (TextBlock)statCanvas.FindName(statisticTextBlockNames[i]);
                text.Text = result[i].ToString() + "\tKč";
            }
        }

        /// <summary>
        /// Kliknutí na grafický záznam - prasata a prasnice
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GraphicRecordClick(object sender, EventArgs e)
        {
            editPig = sender as Pig;
            AddSawPig window = new AddSawPig(this, DefineVM_PigSaw(false));
            window.Show();
        }

        /// <summary>
        /// Kliknutí na grafický záznam - finance
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GraphicFinanceRecordClick(object sender, EventArgs e)
        {
            editFinanceRecord = sender as FinanceRecord;
            AddFinanceWindow window = new AddFinanceWindow(this, DefineVM_Finance(false));
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
            graphic.ConstructGraphicPigSawFinanceList(0,first, rotate, rotateUp, graphicSaw, graphicPig,null);
        }


        /// <summary>
        /// Vykreslí na canvas finanční transakce
        /// </summary>
        /// <param name="first">Jedná se o první stranu</param>
        /// <param name="rotate">Uživatel rotoval kolečkem</param>
        /// <param name="rotateUp">Rotoval nahoru</param>
        public void ConstructGraphicFinance(bool first, bool rotate, bool rotateUp)
        {
            // Kolekce grafických záznamů, které budou vykresleni na plátno
            List<FinanceGraphicRecord> graphicFinance = admin.ConstructGraphicFinance();
            // Přidá obsluhu události při kliknutí na záznam
            foreach (FinanceGraphicRecord rec in graphicFinance)
            {
                rec.GraphicRecordClick += GraphicFinanceRecordClick;
            }

            // Zavolá metodu třídy graphic, která přidá všechny záznamy na plátna
            graphic.ConstructGraphicPigSawFinanceList(1,first, rotate, rotateUp, null, null, graphicFinance);
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
                if (!(editPig is Saw) && type == TypePig.Saw)
                    throw new ArgumentException("Nemůžeš změnit ostatní na chovnou prasnici. Vymaž daný záznam a vytvoř nové zvíře.");
                else if(editPig is Saw && type == TypePig.OtherPig)
                    throw new ArgumentException("Nemůžeš změnit chovnou prasnici na ostatní. Vymaž daný záznam a vytvoř nové zvíře.");

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
            int pregnancyCheck, Birth editRecord)
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

            // Vztažné prase je definováno - uživatel kliknul na seznam a k existujícímu praseti chce přidat záznam
            if(editPig != null)
            {
                // Jedná se o prasnici
                if (editPig is Saw)
                {
                    // Přidání nového záznamu
                    if (operation == 0)
                    {
                        admin.AddEditBirth(0, dateRecessed_help, live_help, death_help, reared_help, dateBirthReal_help, pregnancyCheck_help, null, editPig);
                    }
                    // Úprava stávajícího
                    else if (operation == 1 && editRecord != null)
                    {
                        admin.AddEditBirth(1, dateRecessed_help, live_help, death_help, reared_help, dateBirthReal_help, pregnancyCheck_help, editRecord, editPig);
                    }
                }
                else
                    throw new ArgumentException("Upravované zvíře není PRASNICE, proto nelze přidat porod"); 
            }
            else
                throw new ArgumentException("Nemůžeš přidat porod pro neexistující prace. Nejprve prase vytvoř!");

        }

        /// <summary>
        /// Odebrání záznamu o porodu u nějaké prasnice
        /// </summary>
        /// <param name="record">Odebíráný záznam o porodu</param>
        public void RemoveBirth(Birth record)
        {
            if (record != null && editPig is Saw && editPig != null)
            {
                admin.RemoveBirth(record, (Saw)editPig);
            }
            else if (record == null)
                throw new ArgumentException("Nejprve musíš vybrat záznam, který chceš odebrat!");
            else
                throw new ArgumentException("Něco se nepodařilo. Záznam nebyl odebrán. Zkuste to prosím znovu.");
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
        public void AddEditVeterinary(int operation, string date, string price, string purpose, string drugs, string tasks, Veterinary record)
        {
            // Ošetření datumu
            if (!DateTime.TryParse(date, out DateTime date_help) && date != "")
                throw new ArgumentException("Zadal jsi datum ve špatném formátu. Má vypadat jako 12.10.2020"); 
            else if (date == "")
                throw new ArgumentException("Nezadal jsi žádné datum veterinárního úkonu. Povinný parametr - označen *");
            // Ošetření částky
            if (!int.TryParse(price, out int price_help) && price != "")
                throw new ArgumentException("Zadanou částku nelze převést na číslo. Zkus ji zadat znovu");
            else if (price == "")
                throw new ArgumentException("Nezadal jsi žádnou částku");
            
            // Ošetření účelu návštěvy
            if (purpose == "")
                throw new ArgumentException("Nezadal jsi žádný účel návštěvy veterináře");

            // Nový záznam
            if (operation == 0 && editPig != null)
            {
                admin.AddEditVeterinary(0, date_help, price_help, purpose, drugs, tasks, editPig, null);
            }
            // Úprava stávajícího
            else if (operation == 1 && record != null && editPig != null)
            {
                admin.AddEditVeterinary(1, date_help, price_help, purpose, drugs, tasks, editPig, record);
            }
            else
                throw new ArgumentException("Něco se nepodařilo, omlouváme se za problémy. Zkuste aplikaci restartovat.");
        }

        /// <summary>
        /// Metoda pro odebrání veterinárního záznamu
        /// </summary>
        /// <param name="record">Odebíraný veterinární záznam</param>
        public void RemoveVeterinary(Veterinary record)
        {
            if (record != null && editPig != null)
            {
                admin.RemoveVeterinary(record, editPig);
            }
            else if (editPig == null)
                throw new ArgumentException("Něco se pokazilo. Zkuste to prosím znovu");
            else
                throw new ArgumentException("Nevybral jsi žádný záznam, který by bylo možné smazat.");
        }

        #endregion


        #region Add/Edit/Remove FinanceRecord

        /// <summary>
        /// Metoda pro přidání nebo úpravu transakce
        /// </summary>
        /// <param name="operation">0 - nový záznam * 1 - úprava stávajícího</param>
        /// <param name="date">Datum transakce</param>
        /// <param name="name">Název transakce * MAX 50 znaků</param>
        /// <param name="price">Částka transakce</param>
        /// <param name="description">Popis transakce</param>
        /// <param name="typeFinance">Příjmy / výdaje</param>
        /// <param name="categoryId">Pořadí kategorie transakce</param>
        /// <param name="animal">Pořadí zvířete, kterého se transakce týká</param>
        /// <param name="editRecord">Záznam pro úpravu</param>
        public void AddEditFinanceRecord(byte operation, string date, string name, string price, string description, int typeFinance, int categoryId, Pig animal)
        {
            // Ošetření datumu - povinný údaj
            if (!DateTime.TryParse(date, out DateTime date_help) && date != "")
                throw new ArgumentException("Zadal jsi datum ve špatném formátu. Má vypadat jako 12.10.2020");
            else if(date == "")
                throw new ArgumentException("Nezadal jsi žádné datum transakce");
            // Ošetření názvu transakce - povinný údaj
            if (name.Count() > 38 && name != "")
                throw new ArgumentException("Zadaný název transakce přesahuje 38 znaků. Prosím zkať jej");
            else if (name == "")
                throw new ArgumentException("Nezadal jsi název transakce");
            // Ošetření částky transakce - povinný údaj
            if (!int.TryParse(price, out int price_help) && price != "")
                throw new ArgumentException("Zadal jsi hodnotu transakce ve špatném formátu. Povoleno je pouze celé číslo");
            else if(price == "")
                throw new ArgumentException("Nezadal jsi žádnou hodnotu transakce");
            // Ošetření typu transakce - povinný údaj
            if(typeFinance == -1)
                throw new ArgumentException("Nevybral jsi typ transakce - příjem nebo výdaj");
            // Ošetření kategorie transakce - povinný údaj
            if(categoryId == -1)
                throw new ArgumentException("Nevybral jsi žádnou kategorii. Naprav to a nějakou si zvol.");

            int animalId = -1;
            // Uživatel si zvolil vztažné zvíře a současně kategorie je 3 - zvíře * pokud není kategorie zvíře, tak se uloží - 1, tedy nevybráno do zvířete
            if (animal != null && categoryId == 3)
                animalId = animal.Id;

            // Nový záznam
            if (operation == 0)
            {
                admin.AddEditFinanceRecord(0, date_help, name, price_help, description, (FinanceTypeRecord)typeFinance, (FinanceCategory)categoryId, animalId, null);
            }
            // Úprava stávajícího záznamu
            else if(operation == 1)
            {
                admin.AddEditFinanceRecord(1, date_help, name, price_help, description, (FinanceTypeRecord)typeFinance, (FinanceCategory)categoryId, animalId, editFinanceRecord);
            }

            ConstructGraphicFinance(true, false, false);
        }


        /// <summary>
        /// Metoda pro odebrání transakce
        /// </summary>
        public void RemoveFinanceRecord()
        {
            if (editFinanceRecord != null)
                admin.RemoveFinanceRecord(editFinanceRecord);
            else
                throw new ArgumentException("Nevybral jsi žádný záznam, který lze vymazat.");

            ConstructGraphicFinance(true, false, false);
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
