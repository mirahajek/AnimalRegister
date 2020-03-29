using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalRegister.Pig.Logic
{
    public class Saw : Pig
    {
        /// <summary>
        /// Počet živých selat při porodu CELKEM
        /// </summary>
        public int TotalLive { get; private set; }
        /// <summary>
        /// Počet mrtvých selat při porodu CELKEM
        /// </summary>
        public int TotalDeath { get; private set; }
        /// <summary>
        /// Počet odchovaných selat CELKEM
        /// </summary>
        public int TotalReared { get; private set; }
        private List<Birth> birthRecords;
        /// <summary>
        /// Kolekce porodních záznamů
        /// </summary>
        public List<Birth> BirthRecords
        {
            get
            {
                return birthRecords;
            }
            set
            {
                birthRecords = value;
                TotalLive = 0; TotalDeath = 0; TotalReared = 0;
                foreach(Birth rec in birthRecords)
                {
                    TotalLive += rec.Live;
                    TotalDeath += rec.Death;
                    TotalReared += rec.Reared;
                }

            }
        }

        /// <summary>
        /// Konstruktor pro ukládání Xml
        /// </summary>
        public Saw() : base()
        {

        }

        /// <summary>
        /// Základní kontruktor s povinnými údaji
        /// </summary>
        /// <param name="born">Datum narození</param>
        /// <param name="registerNumber">Registrační číslo prasnice</param>
        /// <param name="name">Pojmenování prasnice</param>
        public Saw(DateTime born, string registerNumber) : base(born, registerNumber, false, null)
        {
            Id = ID;
            ID++;

            Born = born;
            RegisterNumber = registerNumber;
        }


        /// <summary>
        /// Kompletní kontruktor
        /// </summary>
        /// <param name="born">Datum narození</param>
        /// <param name="registerNumber">Registrační číslo prasnice</param>
        /// <param name="name">Pojmenování prasnice</param>
        /// <param name="desc">Doplňující popis k prasnici</param>
        public Saw(DateTime born, string registerNumber, string name, string desc) : base(born, registerNumber, false, null,name,desc)
        {
            Id = ID;
            ID++;

            Born = born;
            RegisterNumber = registerNumber;
            Name = name;
            Description = desc;
        }
    }
}
