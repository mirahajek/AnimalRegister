﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalRegister.Pig.Logic
{
    /// <summary>
    /// Třída repezentující jeden finanční záznam - transakci
    /// </summary>
    public class FinanceRecord
    {
        public static int Id = 0;

        /// <summary>
        /// Jedinečný identifikátor každé transakce
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// Utracená částka
        /// </summary>
        public int Price { get; set; }
        /// <summary>
        /// Název transakce
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Detailnější popis transakce
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Příjem / výdaj
        /// </summary>
        public FinanceTypeRecord TypeRecord { get; set; }
        /// <summary>
        /// Druh finance, tedy kategorie (jídlo, bydlení atd.)
        /// </summary>
        public FinanceCategory Category { get; set; }
        /// <summary>
        /// Datum uskutečnění transakce
        /// </summary>
        public DateTime Date { get; set; }
        /// <summary>
        /// Vztažné zvíře záznamu
        /// </summary>
        public int RelativeAnimalId { get; set; }


        /// <summary>
        /// Kompletní konstruktor pro zadání všech parametrů
        /// </summary>
        /// <param name="price">Utracená / získaná částka</param>
        /// <param name="name">Název transakce</param>
        /// <param name="date">Datum uskutečnění transakce</param>
        /// <param name="place">Místo uskutečněné transakce (obchod, město apod)</param>
        /// <param name="description">Detailní popis k transakci</param>
        /// <param name="type">Výdaj / příjem</param>
        /// <param name="category">Kategorie výdaje</param>
        public FinanceRecord(int price, string name, DateTime date, string description, FinanceTypeRecord type, FinanceCategory category, int relativeAnimalId)
        {
            Price = price;
            Name = name;
            Date = date;
            Description = description;
            TypeRecord = type;
            Category = category;
            RelativeAnimalId = relativeAnimalId;

            ID = Id;
            Id++;
        }

        /// <summary>
        /// Prázdný konstruktor pro ukládání souboru
        /// </summary>
        public FinanceRecord()
        {

        }

    }
}
