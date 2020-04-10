using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalRegister.Pig.Logic
{
    /// <summary>
    /// ViewModel slouží pro přidání / editaci záznamu prasete - pro okno AddSawPig
    /// </summary>
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
        public bool EditRecordFlag { get; private set; }

        /// <summary>
        /// Konstruktor View modelu - zobrazení vytvořeného zvířete
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
            EditRecordFlag = true;
        }

        /// <summary>
        /// Konstruktor pro přidání nového prasete
        /// </summary>
        /// <param name="mothers"></param>
        public VM_PigSaw(List<string> mothers)
        {
            Mothers = mothers;
            EditRecordFlag = false;
            
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
                // Ostatní prase v chovu
                if (ViewPig != null)
                {
                    return 1;
                }
                // Chovná prasnice
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
                // Ostatní prase v chovu
                if (ViewPig != null)
                {
                    // Ostatní zvíře je prasnice
                    if (ViewPig.Sex == Sex.Saw)
                        return 0;
                    // Ostatní zvíře je kanec
                    else
                        return 1;
                }
                // Chovná prasnice
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
                // Ostatní prase v chovu
                if (ViewPig != null)
                {
                    return ViewPig.Born.ToShortDateString();
                }
                // Chovná prasnice
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
                // Ostatní prase v chovu
                if (ViewPig != null)
                {
                    return ViewPig.RegisterNumber;
                }
                // Chovná prasnice
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
                // Ostatní prase v chovu
                if (ViewPig != null)
                {
                    return ViewPig.Name;
                }
                // Chovná prasnice
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
                // Ostatní prase v chovu
                if (ViewPig != null)
                {
                    return ViewPig.Description;
                }
                // Chovná prasnice
                else
                {
                    return ViewSaw.Description;

                }
            }
        }
    }
}
