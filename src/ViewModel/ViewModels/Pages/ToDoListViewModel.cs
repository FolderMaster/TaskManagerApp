﻿using ReactiveUI.SourceGenerators;
using ReactiveUI;

using Model;

using ViewModel.Technicals;
using ViewModel.AppStates;

namespace ViewModel.ViewModels.Pages
{
    public partial class ToDoListViewModel : PageViewModel
    {
        private readonly AppState _appState;

        [Reactive]
        private IEnumerable<ToDoListElement>? _toDoList;

        public ToDoListViewModel(AppState appStateManager)
        {
            _appState = appStateManager;

            Metadata = _appState.Services.ResourceService.GetResource("ToDoListPageMetadata");
            _appState.ItemSessionChanged += AppStateManager_ItemSessionChanged;
        }

        [ReactiveCommand]
        public void Update()
        {
            if (_appState.Session.Tasks == null)
            {
                return;
            }
            var tasks = TaskHelper.GetTaskElements(_appState.Session.Tasks);
            var uncompletedTasks = tasks.Where(t => !TaskHelper.IsTaskCompleted(t));
            ToDoList = uncompletedTasks.OrderBy(t => t.Difficult).
                OrderBy(t => t.Priority).OrderBy(t => t.PlannedTime - t.SpentTime).Select
                (t => new ToDoListElement(t, t.Deadline + (t.PlannedTime - t.SpentTime) <
                DateTime.Now, t.Deadline < DateTime.Now));
        }

        private void AppStateManager_ItemSessionChanged(object? sender, object e) =>
            UpdateCommand.Execute();
    }
}