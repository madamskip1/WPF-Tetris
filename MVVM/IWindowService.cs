using System;
using System.Collections.Generic;
using System.Text;

namespace _PAIN__WPF___Tetris.MVVM
{
    public interface IWindowService
    {
        void Show(IViewModel viewModel);
        void ShowDialog(IViewModel viewModel);
    }
}
