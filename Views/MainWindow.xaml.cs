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
        private ViewModels.ViewModelMainWindow vmmw;
        public MainWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            vmmw = new ViewModels.ViewModelMainWindow();
            DataContext = vmmw;


            InitKeyDownEvents();
        }

        private void InitKeyDownEvents()
        {
            this.KeyDown += (sender, e) =>
            {
                vmmw.KeyDownEvents(e);
            };
        }
    }

}
