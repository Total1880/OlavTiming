using GalaSoft.MvvmLight;
using OlavTiming.Models;
using OlavTiming.Services.Interfaces;
using System;
using System.Collections.ObjectModel;

namespace OlavTiming.ViewModels
{
    public class OverviewTasksViewModel : ViewModelBase
    {
        private readonly IUserTaskService _userTaskService;
        private DateTime _selectedDate;
        private ObservableCollection<UserTask> _allTasks;

        public DateTime SelectedDate
        {
            get => _selectedDate;
            set
            {
                _selectedDate = value;
                RaisePropertyChanged();
                GetTasks();
            }
        }

        public ObservableCollection<UserTask> AllTasks
        {
            get => _allTasks;
            set
            {
                _allTasks = value;
                RaisePropertyChanged();
            }
        }

        public OverviewTasksViewModel(IUserTaskService userTaskService)
        {
            _userTaskService = userTaskService;
            SelectedDate = DateTime.Today;
        }

        private void GetTasks()
        {
            AllTasks = new ObservableCollection<UserTask>(_userTaskService.Get(SelectedDate));
        }
    }
}
