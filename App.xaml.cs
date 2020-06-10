using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace _PAIN__WPF___Tetris
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public MVVM.IWindowService WindowService { get; } = new MVVM.WindowService();
        private Logic.GameLogic Game { get; } = new Logic.GameLogic();

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            ViewModels.TetrisViewModel tetrisViewModel = new ViewModels.TetrisViewModel(Game);

            WindowService.Show(tetrisViewModel);
        }
    }
}
