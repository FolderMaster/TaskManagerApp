using System;

using ViewModel.Interfaces.AppStates;

namespace View.Desktop
{
    public class DesktopAppLifeState : IAppLifeState
    {
        public event EventHandler AppDeactivated;

        public DesktopAppLifeState()
        {
            AppDomain.CurrentDomain.ProcessExit += CurrentDomain_ProcessExit;
        }

        private void CurrentDomain_ProcessExit(object? sender, EventArgs e) =>
            AppDeactivated?.Invoke(this, e);
    }
}
