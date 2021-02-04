using OlavTiming.Models;
using System;
using System.Collections.Generic;

namespace OlavTiming.Services.Interfaces
{
    public interface IUserTaskService
    {
        UserTask Start(string name);
        UserTask Pause();
        UserTask End();
        UserTask End(DateTime dateTime);
        IList<UserTask> Create(IList<UserTask> userTask);
        IList<UserTask> Get();
        IList<UserTask> Get(DateTime date);
        IList<DateTime> GetFiles();
    }
}
