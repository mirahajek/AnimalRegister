using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalRegister.Pig.Logic
{
    /// <summary>
    /// Porodní záznam prasnice
    /// </summary>
    public class Birth
    {
        public static int ID = 0;
        /// <summary>
        /// Identifikátor porodu
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Živých selat při porodu
        /// </summary>
        public int Live { get; set; }
        /// <summary>
        /// Mrtvých selat při porodu
        /// </summary>
        public int Death { get; set; }
        /// <summary>
        /// Odchovaných selat
        /// </summary>
        public int Reared { get; set; }
        /// <summary>
        /// Skutečné datum porodu
        /// </summary>
        public DateTime? DateBirth_real { get; set; }
        /// <summary>
        /// Plánované datum porodu
        /// </summary>
        public DateTime DateBirth_plan { get; set; }
        private DateTime dateRecessed;
        /// <summary>
        /// Datum zapuštění prasnice
        /// </summary>
        public DateTime DateRecessed
        {
            get
            {
                return dateRecessed;
            }

            set
            {
                dateRecessed = value.Date;
                DateBirth_plan = value.AddDays(115);
                DatePregnancyCheck = value.AddDays(40);
            }
        }
        /// <summary>
        /// Datum testu březosti
        /// </summary>
        public DateTime DatePregnancyCheck { get; set; }
        /// <summary>
        /// Test březosti proveden - Ano / Ne
        /// </summary>
        public bool PregnancyCheck { get; set; }

        /// <summary>
        /// Kontruktor pro ukládání do XML
        /// </summary>
        public Birth()
        {

        }

        /// <summary>
        /// Základní kontruktor při vytvoření záznamu
        /// </summary>
        /// <param name="dateRecessed">Datum zapuštění</param>
        public Birth(DateTime dateRecessed)
        {
            Id = ID;
            ID++;

            DateRecessed = dateRecessed;
        }

        /// <summary>
        /// Kompletní kontruktor pro editaci záznamu
        /// </summary>
        /// <param name="dateRecessed">Datum zapuštění</param>
        /// <param name="live">Živých selat</param>
        /// <param name="death">Mrtvých selat</param>
        /// <param name="reared">Odchovaných selat</param>
        /// <param name="birthProcess">Průběh porodu</param>
        /// <param name="dateBirth_real">Skutečné datum porodu</param>
        /// <param name="pregnancyCheck">Kontrola březosti</param>
        public Birth(DateTime dateRecessed, int live, int death, int reared, DateTime dateBirth_real, bool pregnancyCheck)
        {
            Id = ID;
            ID++;

            DateRecessed = dateRecessed;
            Live = live;
            Death = death;
            Reared = reared;
            DateBirth_real = dateBirth_real;
            PregnancyCheck = pregnancyCheck;
        }

        /// <summary>
        /// Přetížená metoda pro výpis, využíváno pro výpis do comboBoxů
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return DateRecessed.ToShortDateString();
        }
    }
}
