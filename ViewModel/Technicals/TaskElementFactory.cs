using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.Technicals
{
    public class TaskElementFactory : IFactory<TaskElement>
    {
        private IFactory<object> _metadataFactory;

        public TaskElementFactory(IFactory<object> metadataFactory)
        {
            ArgumentNullException.ThrowIfNull(metadataFactory, nameof(metadataFactory));
            _metadataFactory = metadataFactory;
        }

        public TaskElement Create() => new TaskElement(_metadataFactory.Create());
    }
}
