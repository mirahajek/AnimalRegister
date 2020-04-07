using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalRegister.Pig.Logic
{
    /// <summary>
    /// View model pro Nový / úpravu finančního záznamu
    /// </summary>
    public class VM_Finance
    {
        /// <summary>
        /// Zobrazovaný finanční záznam
        /// </summary>
        private FinanceRecord record;
        /// <summary>
        /// Informuje o tom, zda je viewModel připraven pro nový záznam nebo pro úpravu stávajícího
        /// </summary>
        public bool NewRecordFlag { get; }

        /// <summary>
        /// Základní konstruktor - nový záznam model
        /// </summary>
        /// <param name="pigColection">Kolekce všech prasat pro comboBox</param>
        public VM_Finance(List<Pig> pigColection)
        {
            record = null;
            Pigs = pigColection;
            NewRecordFlag = true;
        }

        /// <summary>
        /// Rozšířený konstruktor - umožňuje bindovat data pro úpravu záznamu
        /// </summary>
        /// <param name="pigColection">Kolekce všech prasat pro comboBox</param>
        /// <param name="selectType">Vybraný typ transakce - příjem / výdaj</param>
        /// <param name="selectCategory">Vybraná kategorie</param>
        /// <param name="selectPig">Vybrané konkrétní zvíře</param>
        /// <param name="editRecord">Upravovaný záznam</param>
        public VM_Finance(List<Pig> pigColection, int selectType, int selectCategory, int selectPig, FinanceRecord editRecord)
        {
            Pigs = pigColection;
            SelectType = selectType;
            SelectCategory = selectCategory;
            SelectPig = selectPig;
            record = editRecord;
            NewRecordFlag = false;
        }

        /// <summary>
        /// Datum transakce
        /// </summary>
        public string Date
        {
            get
            {
                return record.Date.ToShortDateString();
            }
        }

        /// <summary>
        /// Název transakce
        /// </summary>
        public string Name
        {
            get
            {
                return record.Name;
            }
        }

        /// <summary>
        /// Částka transakce
        /// </summary>
        public string Price
        {
            get
            {
                return record.Price.ToString();
            }
        }

        /// <summary>
        /// Popis transakce
        /// </summary>
        public string Description
        {
            get
            {
                return record.Description;
            }
        }

        /// <summary>
        /// Vybraný typ transakce - příjem / výdaj
        /// </summary>
        public int SelectType {get;}
        /// <summary>
        /// Kolekce prasat pro comboBox
        /// </summary>
        public List<Pig> Pigs { get; }
        /// <summary>
        /// Vybrané zvíře
        /// </summary>
        public int SelectPig { get; }

        /// <summary>
        /// Seznam jmen kategorií pro comboBox
        /// </summary>
        public string[] CategoryNames
        {
            get
            {
                return Admin.FinanceCategory_Czech;
            }
        }

        /// <summary>
        /// Vybraná kategorie
        /// </summary>
        public int SelectCategory { get; }



    }
}
