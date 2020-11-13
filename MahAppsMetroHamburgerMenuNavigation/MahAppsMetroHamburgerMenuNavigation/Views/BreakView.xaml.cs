using System;
using System.Windows.Controls;

namespace MahAppsMetroHamburgerMenuNavigation.Views
{
    /// <summary>
    /// Interaction logic for BreakView.xaml
    /// </summary>
    public partial class BreakView : UserControl
    {
        public BreakView()
        {
            Console.WriteLine("Creating BreakView");
            Unloaded += BreakView_Unloaded;
            InitializeComponent();
        }

        private void BreakView_Unloaded(object sender, System.Windows.RoutedEventArgs e)
        {
            Console.WriteLine("Unloading BreakView");
        }
    }
}
