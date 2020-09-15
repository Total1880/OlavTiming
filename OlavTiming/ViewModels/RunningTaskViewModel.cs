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
        private RelayCommand _manualEndTaskCommand;
        private UserTask _currentUserTask;
        private string _userTaskName;
        private DateTime _start;
        private DateTime _end;
        private DateTime _manualEnd;
        private bool _startButtonEnabled;
        private bool _pauseButtonEnabled;
        private bool _endButtonEnabled;
        private Visibility _manualEndEnabled;
        private Visibility _pauseLabel;
        private ObservableCollection<UserTask> _allTasks;

        public RelayCommand NewTaskCommand => _newTaskCommand ??= new RelayCommand(NewTask);
        public RelayCommand PauseTaskCommand => _pauseTaskCommand ??= new RelayCommand(PauseTask);
        public RelayCommand EndTaskCommand => _endTaskCommand ??= new RelayCommand(EndTask);
        public RelayCommand ManualEndTaskCommand => _manualEndTaskCommand ??= new RelayCommand(ManualEndTask);

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

        public DateTime ManualEnd
        {
            get => _manualEnd;
            set
            {
                _manualEnd = value;
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

        public Visibility ManualEndShow
        {
            get => _manualEndEnabled;
            set
            {
                _manualEndEnabled = value;
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
            ManualEndShow = Visibility.Hidden;
            AllTasks = new ObservableCollection<UserTask>(_userTaskService.Get());
            CheckUserTasks();
            ManualEnd = DateTime.Now;
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

            ResetView();
        }

        private void ManualEndTask()
        {
            if (CurrentUserTask.Timeframes.Max(u => u.Start) < ManualEnd)
            {
                CurrentUserTask = _userTaskService.End(ManualEnd);

                ResetView();
            }
            else
            {
                MessageBox.Show("The end time needs to be later than " + CurrentUserTask.Timeframes.Max(u => u.Start));
            }
        }

        private void ResetView()
        {
            UpdateView(true, false, false);
            AllTasks = new ObservableCollection<UserTask>(_userTaskService.Create(AllTasks));
            UserTaskName = string.Empty;
            Start = DateTime.MinValue;
            End = DateTime.MinValue;
            ManualEndShow = Visibility.Hidden;
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

        private void CheckUserTasks()
        {
            if (AllTasks.Any(u => u.End == DateTime.MinValue))
            {
                CurrentUserTask = AllTasks.Where(u => u.End == DateTime.MinValue).FirstOrDefault();
                
                var result = MessageBox.Show("Your last task has not ended. Do you want to continue " + CurrentUserTask.Name + "? If no, you must enter a time manually.", "Last task did not complete", MessageBoxButton.YesNo);
                UpdateView(false, true, true);
                UserTaskName = CurrentUserTask.Name;

                if (result == MessageBoxResult.No)
                {
                    ManualEndShow = Visibility.Visible;
                }
            }
        }
    }
}
