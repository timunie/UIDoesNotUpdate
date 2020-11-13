using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using MahApps.Metro.Controls;
using MahAppsMetroHamburgerMenuNavigation.ViewModels;
using MahAppsMetroHamburgerMenuNavigation.Views;
using MenuItem = MahAppsMetroHamburgerMenuNavigation.ViewModels.MenuItem;

namespace MahAppsMetroHamburgerMenuNavigation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public static Navigation.NavigationServiceEx NavigationServiceEx { get; } = new Navigation.NavigationServiceEx();

        public MainWindow()
        {
            this.InitializeComponent();
            DataContext = new ShellViewModel();
            NavigationServiceEx.Navigated += this.NavigationServiceEx_OnNavigated;
            this.HamburgerMenuControl.Content = NavigationServiceEx.Frame;

            // Navigate to the home page.
            this.Loaded += (sender, args) => NavigationServiceEx.Navigate(new MainViewModel());
        }

        private void HamburgerMenuControl_OnItemInvoked(object sender, HamburgerMenuItemInvokedEventArgs e)
        {
            if (e.InvokedItem is MenuItem menuItem && menuItem.IsNavigation)
            {
                if (menuItem.NavigationTarget is AboutViewModel)
                {
                    var view = new AboutView();
                    view.ShowDialog();
                    SelectMenuItem(((Frame)HamburgerMenuControl.Content).Content);
                }
                else
                {
                    NavigationServiceEx.Navigate(menuItem.NavigationTarget);
                }
            }
        }

        private void NavigationServiceEx_OnNavigated(object sender, NavigationEventArgs e)
        {
            if (e == null)
            {
                return;
            }

            SelectMenuItem(e.Content);

            // update back button
            this.GoBackButton.Visibility = NavigationServiceEx.Frame.CanGoBack ? Visibility.Visible : Visibility.Collapsed;
        }

        private void SelectMenuItem(object content)
        {
            this.HamburgerMenuControl.SelectedItem = this.HamburgerMenuControl
                                                         .Items
                                                         .OfType<MenuItem>()
                                                         .FirstOrDefault(x => x.NavigationTarget == content);

            this.HamburgerMenuControl.SelectedOptionsItem = this.HamburgerMenuControl
                                                                .OptionsItems
                                                                .OfType<MenuItem>()
                                                                .FirstOrDefault(x => x.NavigationTarget == content);
        }

        private void GoBack_OnClick(object sender, RoutedEventArgs e)
        {
            NavigationServiceEx.Frame.GoBack();
        }
    }
}