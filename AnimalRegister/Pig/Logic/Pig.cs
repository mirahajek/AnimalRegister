using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalRegister.Pig.Logic
{
    /// <summary>
    /// Třída reprezentující prase
    /// </summary>
    public class Pig
    {
        public static int ID = 0;
        /// <summary>
        /// Vnitřní identifikátor prase
        /// </summary>
        public int Id { get;  set; }
        /// <summary>
        /// Pojmenování prasete
        /// </summary>
        public string Name { get;  set; }
        /// <summary>
        /// Datum narození
        /// </summary>
        public DateTime Born { get;  set; }
        /// <summary>
        /// Registrační číslo prasete - dle chovatele
        /// </summary>
        public string RegisterNumber { get;  set; }
        /// <summary>
        /// Popis
        /// </summary>
        public string Description { get;  set; }
        /// <summary>
        /// Prasnice
        /// </summary>
        public Saw Mother { get; set; }
        /// <summary>
        /// Pohlaví * True - muž, False - žena
        /// </summary>
        public Sex Sex { get; set; }
        /// <summary>
        /// Kolekce veterinárních záznamů kusu
        /// </summary>
        public List<Veterinary> VeterinaryRecords { get; set; }
        /// <summary>
        /// Věk prasete ve dnech
        /// </summary>
        public int Age
        {
            get
            {
                return (DateTime.Now - Born).Days;
            }
        }

        /// <summary>
        /// Základní konstruktor - povinné údaje
        /// </summary>
        /// <param name="born">Datum narození</param>
        /// <param name="registerNumber">Registrační číslo</param>
        /// <param name="sex">Pohlaví * True - muž * False - žena</param>
        /// <param name="mother">Matka - prasnice</param>
        public Pig(DateTime born, string registerNumber, Sex sex, Saw mother)
        {
            Id = ID;
            ID++;

            Born = born;
            RegisterNumber = registerNumber;
            Sex = sex;
            Mother = mother;

            VeterinaryRecords = new List<Veterinary>();
        }

        /// <summary>
        /// Základní konstruktor - povinné údaje
        /// </summary>
        /// <param name="born">Datum narození</param>
        /// <param name="registerNumber">Registrační číslo</param>
        /// <param name="sex">Pohlaví * 0 - žena  * 1 - muž</param>
        /// <param name="mother">Matka - prasnice</param>
        /// <param name="despription">Doplňující popis ke zvířeti</param>
        /// <param name="name">Pojmenování zvířete</param>
        public Pig(DateTime born, string registerNumber, Sex sex, Saw mother, string name, string despription)
        {
            Id = ID;
            ID++;

            Born = born;
            RegisterNumber = registerNumber;
            Sex = sex;
            Mother = mother;
            Name = name;
            Description = despription;

            VeterinaryRecords = new List<Veterinary>();
        }

        /// <summary>
        /// Prázdný konstruktor pro XML serializer
        /// </summary>
        public Pig()
        {

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
