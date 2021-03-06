﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalRegister.Pig.Logic
{
    /// <summary>
    /// Třída reprezentující chovnou prasnici - odděděna od prasete
    /// </summary>
    public class Saw : Pig
    {
        /// <summary>
        /// Kolekce porodních záznamů
        /// </summary>
        public List<Birth> BirthRecords { get; set; }

        private int totalLive;
        /// <summary>
        /// Počet živých selat při porodu CELKEM
        /// </summary>
        public int TotalLive
        {
            get
            {
                // Výpočet celkového počtu narozených selat
                totalLive = 0;
                foreach (Birth rec in BirthRecords)
                {
                    totalLive += rec.Live;
                }

                return totalLive;
            }
            set
            {
                totalLive = value;
            }
        }

        private int totalDeath;
        /// <summary>
        /// Počet mrtvých selat při porodu CELKEM
        /// </summary>
        public int TotalDeath
        {
            get
            {
                // Výpočet celkového počtu mrtvých selat při porodu
                totalDeath = 0;
                foreach (Birth rec in BirthRecords)
                {
                    totalDeath += rec.Death;
                }
                
                return totalDeath;
            }
            set
            {
                totalDeath = value;
            }
        }


        private int totalReared;
        /// <summary>
        /// Počet odchovaných selat CELKEM
        /// </summary>
        public int TotalReared
        {
            get
            {
                // Výpočet celkového počtu odchovaných selat při odstavu
                totalReared = 0;

                foreach (Birth rec in BirthRecords)
                {
                    totalReared += rec.Reared;
                }

                return totalReared;
            }
            set
            {
                totalReared = value;
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
        public Saw(DateTime born, string registerNumber) : base(born, registerNumber, Sex.Saw, null)
        {
            Born = born;
            RegisterNumber = registerNumber;

            BirthRecords = new List<Birth>();
        }


        /// <summary>
        /// Kompletní kontruktor
        /// </summary>
        /// <param name="born">Datum narození</param>
        /// <param name="registerNumber">Registrační číslo prasnice</param>
        /// <param name="name">Pojmenování prasnice</param>
        /// <param name="desc">Doplňující popis k prasnici</param>
        public Saw(DateTime born, string registerNumber, string name, string desc) : base(born, registerNumber, Sex.Saw, null,name,desc)
        {
            Born = born;
            RegisterNumber = registerNumber;
            Name = name;
            Description = desc;

            BirthRecords = new List<Birth>();
        }

        /// <summary>
        /// Přetížená metoda pro výpis. Používaná v ComboBoxech
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return RegisterNumber + " | " + Name;
        }
    }
}
