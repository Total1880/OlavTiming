using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using OlavTiming.Messages.WindowOpener;
using System;

namespace OlavTiming.ViewModels
{
    public class StartScreenViewModel : ViewModelBase
    {
        private RelayCommand _newTaskCommand;

        public RelayCommand NewTaskCommand => _newTaskCommand ??= new RelayCommand(NewTask);

        private void NewTask()
        {
            MessengerInstance.Send(new OpenRunningTaskPageMessage());
        }
    }
}
