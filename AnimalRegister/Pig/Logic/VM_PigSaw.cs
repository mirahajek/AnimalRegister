using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        /// True - uprava * False - novy
        /// </summary>
        public bool State { get; private set; }

        /// <summary>
        /// Konstruktor View modelu
        /// </summary>
        /// <param name="pig">Zobrazované prase</param>
        /// <param name="saw">Zobrazovaná prasnice</param>
        /// <param name="mothers">Pole jmen prasnic uložených v aplikaci</param>
        public VM_PigSaw(Pig pig, Saw saw, List<string> mothers, int selectMotherId)
        {
            ViewPig = pig;
            ViewSaw = saw;

            Mothers = mothers;
            SelectMother = selectMotherId;
            State = true;
        }


        public VM_PigSaw(List<string> mothers)
        {
            Mothers = mothers;
            State = false;
            
        }

        /// <summary>
        /// Pole jmen prasnic v systému ** data pro ComboBox
        /// </summary>
        public List<string> Mothers { get; private set; }

        /// <summary>
        /// Typ - Prasnice / Ostatní prase ** data pro ComboBox
        /// </summary>
        public string[] PigType
        {
            get
            {
                return Enum.GetNames(typeof(TypePig_Czech));
            }
        }

        /// <summary>
        /// Id vybrané prasnice - pozice v ComboBoxu
        /// </summary>
        public int SelectMother { get; private set; }

        /// <summary>
        /// Id vybraného typu prasete ** 0 - Prasnice / 1 - Ostatní
        /// </summary>
        public int SelectType
        {
            get
            {
                // Zobrazované zvíře je ostatní PRASE
                if (ViewPig != null)
                {
                    return 1;
                }
                // Prasnice
                else
                {
                    return 0;
                }
            }
        }

        /// <summary>
        /// Vybrané pohlaví zvířete
        /// </summary>
        public int SelectSex
        {
            get
            {
                if (ViewPig != null)
                {
                    // Ostatní zvíře je prasnice
                    if (ViewPig.Sex == Sex.Saw)
                        return 0;
                    // Ostatní zvíře je kanec
                    else
                        return 1;
                }
                else
                {
                    return 0;
                }
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
