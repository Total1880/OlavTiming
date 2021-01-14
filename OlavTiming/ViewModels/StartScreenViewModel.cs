using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using OlavTiming.Messages.WindowOpener;
using System;

namespace OlavTiming.ViewModels
{
    public class StartScreenViewModel : ViewModelBase
    {
        private RelayCommand _newTaskCommand;
        private RelayCommand _overviewTaskCommand;

        public RelayCommand NewTaskCommand => _newTaskCommand ??= new RelayCommand(NewTask);
        public RelayCommand OverviewTaskCommand => _overviewTaskCommand ??= new RelayCommand(OverviewTask);

        private void NewTask()
        {
            MessengerInstance.Send(new OpenRunningTaskPageMessage());
        }

        private void OverviewTask()
        {
            MessengerInstance.Send(new OpenOverviewTaskPageMessage());

        }
    }
}
