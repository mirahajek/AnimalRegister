using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace AnimalRegister.Pig.Logic
{
    public class VM_Birth 
    {
        /// <summary>
        /// Základní konstruktor - vyžaduje kolekci záznamů PRASNICE
        /// </summary>
        /// <param name="sawsBirth"></param>
        public VM_Birth(List<Birth> sawsBirth)
        {
            SawBirth = sawsBirth;
            SelectBirth = null;
            EditRecord = false;
        }

        /// <summary>
        /// Změna výběru - uživatel vybral nějaký porod, nebo změnil
        /// </summary>
        /// <param name="record"></param>
        public void ChangeRecord(Birth record)
        {
            EditRecord = true;
            SelectBirth = record;
        }

        /// <summary>
        /// Úprava - TRUE * Nový - FALSE
        /// </summary>
        public bool EditRecord { get; private set; }

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
        public string DateRecessed { get; private set; }
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
