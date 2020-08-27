using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using OlavTiming.Models;
using OlavTiming.Services.Interfaces;
using System;
using System.Linq;

namespace OlavTiming.ViewModels
{
    public class RunningTaskViewModel : ViewModelBase
    {
        private readonly IUserTaskService _userTaskService;
        private RelayCommand _newTaskCommand;
        private UserTask _currentUserTask;
        private string _userTaskName;
        private DateTime _start;

        public RelayCommand NewTaskCommand => _newTaskCommand ??= new RelayCommand(NewTask);

        public UserTask CurrentUserTask
        {
            get => _currentUserTask;
            set
            {
                _currentUserTask = value;
                RaisePropertyChanged();
            }
        }

        public string UserTaskName
        {
            get => _userTaskName;
            set
            {
                _userTaskName = value;
                RaisePropertyChanged();
            }
        }

        public DateTime Start
        {
            get => _start;
            set
            {
                _start = value;
                RaisePropertyChanged();
            }
        }

        public RunningTaskViewModel(IUserTaskService userTaskService)
        {
            _userTaskService = userTaskService;
        }

        private void NewTask()
        {
            CurrentUserTask = _userTaskService.Start(UserTaskName);
            Start = CurrentUserTask.Timeframes[0].Start;
        }
    }
}
