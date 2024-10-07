using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI.SourceGenerators;

using Model;

namespace ViewModel.ViewModels.Modals
{
    public partial class AddViewModel : ModalViewModel
    {
        public ITask Value { get; set; }

        public AddViewModel()
        {

        }

        [ReactiveCommand]
        private void Ok() =>
            _taskSource?.SetResult(Value);

        [ReactiveCommand]
        private void Cancel() =>
            _taskSource?.SetResult(null);
    }
}
