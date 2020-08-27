using Autofac;
using OlavTiming.Services;
using OlavTiming.ViewModels;

namespace OlavTiming
{
    public class ViewModelLocator
    {
        private readonly IContainer _container;

        public ViewModelLocator()
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule<ServicesModule>();

            builder.RegisterType<StartScreenViewModel>().SingleInstance();
            builder.RegisterType<RunningTaskViewModel>().SingleInstance();

            _container = builder.Build();
        }

        public StartScreenViewModel StartScreen => _container.Resolve<StartScreenViewModel>();
        public RunningTaskViewModel RunningTask => _container.Resolve<RunningTaskViewModel>();
    }
}
