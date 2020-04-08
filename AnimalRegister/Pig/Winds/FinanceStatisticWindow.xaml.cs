using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using AnimalRegister.Pig.Logic;

namespace AnimalRegister.Pig.Winds
{
    /// <summary>
    /// Interakční logika pro FinanceStatisticWindow.xaml
    /// </summary>
    public partial class FinanceStatisticWindow : Window
    {
        private Validator validator;

        public FinanceStatisticWindow(Validator validator)
        {
            InitializeComponent();
            TextBlock text = (TextBlock)statisticCanvas.FindName("xxTextBlock");
        }
    }
}
