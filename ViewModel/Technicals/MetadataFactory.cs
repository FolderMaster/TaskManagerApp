using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.Technicals
{
    public class MetadataFactory : IFactory<object>
    {
        public MetadataFactory() { }

        public object Create() => new Metadata() { Name = "Task" };
    }
}
