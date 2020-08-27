using OlavTiming.Models;

namespace OlavTiming.Services.Interfaces
{
    public interface IUserTaskService
    {
        UserTask Start(string name);
        UserTask Pause();
        UserTask End();
    }
}
