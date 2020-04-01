using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalRegister.Pig.Logic
{
    public class VM_PigSaw
    {
        /// <summary>
        /// Zobrazovaná instance prasete
        /// </summary>
        private Pig ViewPig;
        /// <summary>
        /// Zobrazovaná instance Prasnice
        /// </summary>
        private Saw ViewSaw;

        /// <summary>
        /// Konstruktor View modelu
        /// </summary>
        /// <param name="pig">Zobrazované prase</param>
        /// <param name="saw">Zobrazovaná prasnice</param>
        /// <param name="mothers">Pole jmen prasnic uložených v aplikaci</param>
        public VM_PigSaw(Pig pig, Saw saw, List<string> mothers)
        {
            ViewPig = pig;
            ViewSaw = saw;

            Mothers = mothers;
        }

        /// <summary>
        /// Pole jmen prasnic v systému
        /// </summary>
        public List<string> Mothers { get; private set; }

        /// <summary>
        /// Typ - Prasnice / Ostatní prase
        /// </summary>
        public string[] PigType
        {
            get
            {
                return Enum.GetNames(typeof(TypePig_Czech));
            }
        }

        /// <summary>
        /// Datum narození prasete
        /// </summary>
        public string DateBorn
        {
            get
            {
                if(ViewPig != null)
                {
                    return ViewPig.Born.ToShortDateString();
                }
                else
                {
                    return ViewSaw.Born.ToShortDateString();
                    
                }
            }
        }

        /// <summary>
        /// Registrační číslo prasete
        /// </summary>
        public string RegisterNumber
        {
            get
            {
                if (ViewPig != null)
                {
                    return ViewPig.RegisterNumber;
                }
                else
                {
                    return ViewSaw.RegisterNumber;

                }
            }
        }

        /// <summary>
        /// Pojmenování prasete
        /// </summary>
        public string Name
        {
            get
            {
                if (ViewPig != null)
                {
                    return ViewPig.Name;
                }
                else
                {
                    return ViewSaw.Name;

                }
            }
        }

        /// <summary>
        /// Detailnější popis k praseti
        /// </summary>
        public string Description
        {
            get
            {
                if (ViewPig != null)
                {
                    return ViewPig.Description;
                }
                else
                {
                    return ViewSaw.Description;

                }
            }
        }
    }
}
