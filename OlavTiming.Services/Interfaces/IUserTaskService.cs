using OlavTiming.Models;
using System.Collections.Generic;

namespace OlavTiming.Services.Interfaces
{
    public interface IUserTaskService
    {
        UserTask Start(string name);
        UserTask Pause();
        UserTask End();
        IList<UserTask> Create(IList<UserTask> userTask);
        IList<UserTask> Get();
    }
}
