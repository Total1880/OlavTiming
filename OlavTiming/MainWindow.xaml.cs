using GalaSoft.MvvmLight.Messaging;
using OlavTiming.Messages.WindowOpener;
using OlavTiming.Pages;
using System;
using System.Windows;

namespace OlavTiming
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private StartScreenPage _startScreenPage;
        private RunningTaskPage _runningTaskPage;
        private OverviewTasksPage _overviewTasksPage;

        public StartScreenPage StartScreenPage => _startScreenPage ??= new StartScreenPage();
        public RunningTaskPage RunningTaskPage => _runningTaskPage ??= new RunningTaskPage();
        public OverviewTasksPage OverviewTasksPage => _overviewTasksPage ??= new OverviewTasksPage();

        public MainWindow()
        {
            InitializeComponent();
            MainWindowFrame.NavigationService.Navigate(StartScreenPage);
            Messenger.Default.Register<OpenRunningTaskPageMessage>(this, OpenNewTask);
            Messenger.Default.Register<OpenOverviewTaskPageMessage>(this, OpenOverviewTasks);
        }

        private void OpenNewTask(OpenRunningTaskPageMessage obj)
        {
            MainWindowFrame.NavigationService.Navigate(RunningTaskPage);
        }

        private void OpenOverviewTasks(OpenOverviewTaskPageMessage obj)
        {
            MainWindowFrame.NavigationService.Navigate(OverviewTasksPage);
        }
    }
}
