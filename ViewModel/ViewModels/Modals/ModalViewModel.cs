using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI.SourceGenerators;

namespace ViewModel.ViewModels.Modals
{
    public partial class ModalViewModel : ViewModelBase
    {
        protected TaskCompletionSource<object?>? _taskSource;

        [Reactive]
        private bool _isInvoked;

        public async Task<object?> Invoke()
        {
            if (IsInvoked)
            {
                throw new InvalidOperationException();
            }
            _taskSource = new();
            IsInvoked = true;
            var result = await _taskSource.Task;
            IsInvoked = false;
            return result;
        }
    }
}
