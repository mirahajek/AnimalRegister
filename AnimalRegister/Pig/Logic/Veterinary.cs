using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalRegister.Pig.Logic
{
    public class Veterinary
    {
        public static int ID = 0;
        /// <summary>
        /// Jedinečný identifikátor veterinárního záznamu
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Datum veterinárního záznamu
        /// </summary>
        public DateTime Date { get; set; }
        /// <summary>
        /// Zaplacená částka
        /// </summary>
        public int Price { get; set; }
        /// <summary>
        /// Účel záznamu
        /// </summary>
        public string Purpose { get; set; }
        /// <summary>
        /// Podané léčivo
        /// </summary>
        public string Drugs { get; set; }
        /// <summary>
        /// Prováděné úkony na praseti
        /// </summary>
        public string Tasks { get; set; }

        /// <summary>
        /// Prázdný konstruktor pro XML serializer
        /// </summary>
        public Veterinary()
        {
            
        }

        /// <summary>
        /// Kompletní konstruktor 
        /// </summary>
        /// <param name="date">Datum</param>
        /// <param name="price">Částka</param>
        /// <param name="purpose">Účel</param>
        /// <param name="tasks">Úkony</param>
        /// <param name="drugs">Léčivo</param>
        public Veterinary(DateTime date, int price, string purpose, string tasks,string drugs)
        {
            Id = ID;
            ID++;

            Date = date;
            Price = price;
            Purpose = purpose;
            Tasks = tasks;
            Drugs = drugs;
        }

        /// <summary>
        /// Přetížený výpis
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Date.ToShortDateString();
        }
    }
}
