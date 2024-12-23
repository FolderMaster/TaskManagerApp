﻿using Model.Interfaces;
using ViewModel.Implementations.AppStates.Sessions.Database.Domains;
using ViewModel.Interfaces.DataManagers.Generals;

namespace ViewModel.Implementations.DataManagers.Factories
{
    public class TaskCompositeFactory : IFactory<ITaskComposite>
    {
        private IFactory<object> _metadataFactory;

        public TaskCompositeFactory(IFactory<object> metadataFactory)
        {
            ArgumentNullException.ThrowIfNull(metadataFactory, nameof(metadataFactory));
            _metadataFactory = metadataFactory;
        }

        public ITaskComposite Create()
        {
            var result = new TaskCompositeDomain()
            {
                Metadata = _metadataFactory.Create(),
                Entity = new()
                {
                    Task = new()
                }
            };
            result.Entity.Task.TaskComposite = result.Entity;
            return result;
        }
    }
}
