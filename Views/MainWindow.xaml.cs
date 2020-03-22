using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace _PAIN__WPF___Tetris
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ViewModels.ViewModelMainWindow viewModelMainWindow;

        public MainWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            viewModelMainWindow = new ViewModels.ViewModelMainWindow(this);

            

            InitKeyDownEvents();
        }

        public void SetMainInfoText(string text)
        {
            MainInfo.Text = text;
        }

        public void SetInfoVisibility(bool visibility)
        {
            InfoGrid.Visibility = (visibility ? Visibility.Visible : Visibility.Hidden);
        }

        private void InitKeyDownEvents()
        {
            this.KeyDown += (sender, e) =>
            {
                viewModelMainWindow.KeyDownEvents(e);
            };
        }
    }

}
