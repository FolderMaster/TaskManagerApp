using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.ViewModels.Pages;

public class PageViewModel : ViewModelBase
{
    public object Metadata { get; private set; }

    public PageViewModel(object metadata) => Metadata = metadata;

    public PageViewModel() : this("Default metadata") { }
}
