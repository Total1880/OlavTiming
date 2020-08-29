using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using OlavTiming.Models;
using OlavTiming.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;

namespace OlavTiming.ViewModels
{
    public class RunningTaskViewModel : ViewModelBase
    {
        private readonly IUserTaskService _userTaskService;
        private RelayCommand _newTaskCommand;
        private RelayCommand _pauseTaskCommand;
        private RelayCommand _endTaskCommand;
        private UserTask _currentUserTask;
        private string _userTaskName;
        private DateTime _start;
        private DateTime _end;
        private bool _startButtonEnabled;
        private bool _pauseButtonEnabled;
        private bool _endButtonEnabled;
        private Visibility _pauseLabel;
        private ObservableCollection<UserTask> _allTasks;

        public RelayCommand NewTaskCommand => _newTaskCommand ??= new RelayCommand(NewTask);
        public RelayCommand PauseTaskCommand => _pauseTaskCommand ??= new RelayCommand(PauseTask);
        public RelayCommand EndTaskCommand => _endTaskCommand ??= new RelayCommand(EndTask);

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

        public DateTime End
        {
            get => _end;
            set
            {
                _end = value;
                RaisePropertyChanged();
            }
        }

        public bool StartButtonEnabled
        {
            get => _startButtonEnabled;
            set
            {
                _startButtonEnabled = value;
                RaisePropertyChanged();
            }
        }

        public bool PauseButtonEnabled
        {
            get => _pauseButtonEnabled;
            set
            {
                _pauseButtonEnabled = value;
                RaisePropertyChanged();
            }
        }

        public bool EndButtonEnabled
        {
            get => _endButtonEnabled;
            set
            {
                _endButtonEnabled = value;
                RaisePropertyChanged();
            }
        }

        public Visibility PauseLabel
        {
            get => _pauseLabel;
            set
            {
                _pauseLabel = value;
                RaisePropertyChanged();
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

        public RunningTaskViewModel(IUserTaskService userTaskService)
        {
            _userTaskService = userTaskService;
            StartButtonEnabled = true;
            PauseButtonEnabled = false;
            EndButtonEnabled = false;
            PauseLabel = Visibility.Collapsed;
            AllTasks = new ObservableCollection<UserTask>(_userTaskService.Get());
        }

        private void NewTask()
        {
            CurrentUserTask = _userTaskService.Start(UserTaskName);
            UpdateView(false, true, true);
            AllTasks.Add(CurrentUserTask);
            _userTaskService.Create(AllTasks);
        }

        private void PauseTask()
        {
            CurrentUserTask = _userTaskService.Pause();
            if (CurrentUserTask.Timeframes[CurrentUserTask.Timeframes.Count() - 1].End == DateTime.MinValue)
            {
                PauseLabel = Visibility.Collapsed;
            }
            else
            {
                PauseLabel = Visibility.Visible;
            }
            _userTaskService.Create(AllTasks);
        }

        private void EndTask()
        {
            CurrentUserTask = _userTaskService.End();
            UpdateView(true, false, false);
            AllTasks = new ObservableCollection<UserTask>(_userTaskService.Create(AllTasks));
        }

        private void UpdateView(bool start, bool pause, bool end)
        {
            if (CurrentUserTask.Timeframes.Any())
            {
                Start = CurrentUserTask.Timeframes[0].Start;
                End = CurrentUserTask.Timeframes[CurrentUserTask.Timeframes.Count - 1].End;
                StartButtonEnabled = start;
                PauseButtonEnabled = pause;
                EndButtonEnabled = end;
                PauseLabel = Visibility.Collapsed;
            }
        }
    }
}
