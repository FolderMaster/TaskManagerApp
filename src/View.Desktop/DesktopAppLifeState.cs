using System;
using ViewModel.Interfaces.AppStates;

namespace View.Desktop
{
    public class DesktopAppLifeState : IAppLifeState
    {
        public event EventHandler AppClosing;

        public DesktopAppLifeState()
        {
            AppDomain.CurrentDomain.ProcessExit += CurrentDomain_ProcessExit;
        }

        private void CurrentDomain_ProcessExit(object? sender, EventArgs e) =>
            AppClosing?.Invoke(this, e);
    }
}
