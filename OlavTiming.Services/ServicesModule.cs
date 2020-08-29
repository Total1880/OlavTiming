using Autofac;
using OlavTiming.Repositories;

namespace OlavTiming.Services
{
    public class ServicesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule<RepositoriesModule>();

            builder.RegisterType<UserTaskService>().AsImplementedInterfaces();
        }
    }
}
