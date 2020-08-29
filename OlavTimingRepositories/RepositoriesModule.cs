using Autofac;

namespace OlavTiming.Repositories
{
    public class RepositoriesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UserTaskRepository>().AsImplementedInterfaces();
        }
    }
}
