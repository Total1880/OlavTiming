using Autofac;

namespace OlavTiming.Services
{
    public class ServicesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UserTaskService>().AsImplementedInterfaces();
        }
    }
}
