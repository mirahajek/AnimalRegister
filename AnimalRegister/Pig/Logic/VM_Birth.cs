using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace AnimalRegister.Pig.Logic
{
    /// <summary>
    /// Třída sloužící jako viewModel pro okno BirthWindow. Obsahuje všechny potřebné vlastnosti pro vytvoření, editaci záznamu
    /// </summary>
    public class VM_Birth 
    {
        /// <summary>
        /// Instance prasnice, které se dané porody týkají. Využito pro získání dat o celkovém počtu narozených, mrtvých a odchovaných selatech
        /// </summary>
        private Saw saw;

        /// <summary>
        /// Základní konstruktor - vyžaduje kolekci záznamů PRASNICE
        /// </summary>
        /// <param name="sawsBirth">Porodní záznamy prasnice</param>
        /// <param name="saw">Instance prasnice</param>
        public VM_Birth(List<Birth> sawsBirth, Saw saw)
        {
            SawBirth = sawsBirth;
            SelectBirth = null;
            EditRecordFlag = false;
            this.saw = saw;
            // 
            if (saw != null)
            {
                TotalLive = saw.TotalLive.ToString();
                TotalDeath = saw.TotalDeath.ToString();
                TotalReared = saw.TotalReared.ToString();
            }
        }

        /// <summary>
        /// Změna výběru - uživatel vybral nějaký porod, nebo změnil
        /// </summary>
        /// <param name="record"></param>
        public void ChangeRecord(Birth record)
        {
            EditRecordFlag = true;
            SelectBirth = record;
        }

        /// <summary>
        /// Počet živých selat při porodu CELKEM
        /// </summary>
        public string TotalLive { get; }

        /// <summary>
        /// Počet mrtvých selat při porodu CELKEM
        /// </summary>
        public string TotalDeath { get; }

        /// <summary>
        /// Počet odchovaných selat CELKEM
        /// </summary>
        public string TotalReared { get; }



        /// <summary>
        /// Úprava - TRUE * Nový - FALSE
        /// </summary>
        public bool EditRecordFlag { get; private set; }

        /// <summary>
        /// Kolekce porodů prasnice
        /// </summary>
        public List<Birth> SawBirth { get; private set; }

        /// <summary>
        /// Vybraný záznam o porodu
        /// </summary>
        public Birth SelectBirth { get; private set; }

        /// <summary>
        /// Datum kontroly březosti
        /// </summary>
        public string DatePregnancyCheck
        {
            get
            {
                return SelectBirth.DatePregnancyCheck.ToShortDateString();
            }
        }

        /// <summary>
        /// Kontola březosti - provedena TRUE / FALSE
        /// </summary>
        public int PregnancyCheck
        {
            get
            {
                if (SelectBirth.PregnancyCheck)
                    return 1;
                else
                    return 0;
            }
        }

        /// <summary>
        /// Datum zapuštění
        /// </summary>
        public string DateRecessed
        {
            get
            {
                return SelectBirth.DateRecessed.ToShortDateString();
            }
        }

        /// <summary>
        /// Plánované datum porodu
        /// </summary>
        public string DateBirth_plan
        {
            get
            {
                return SelectBirth.DateBirth_plan.ToShortDateString();
            }
        }

        /// <summary>
        /// Skutečné datum porodu
        /// </summary>
        public string DateBirth_real
        {
            get
            {
                return SelectBirth.DateBirth_real.ToString();
            }
        }

        /// <summary>
        /// Živých selat při porodu
        /// </summary>
        public string Live
        {
            get
            {
                return SelectBirth.Live.ToString();
            }
        }

        /// <summary>
        /// Mrtvých selat při porodu
        /// </summary>
        public string Death
        {
            get
            {
                return SelectBirth.Death.ToString();
            }
        }

        /// <summary>
        /// Odchovaných selat po odstavu
        /// </summary>
        public string Reared
        {
            get
            {
                return SelectBirth.Reared.ToString();
            }
        }
    }
}
