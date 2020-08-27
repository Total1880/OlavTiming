using OlavTiming.Models;
using OlavTiming.Services.Interfaces;
using System;
using System.Collections.Generic;

namespace OlavTiming.Services
{
    public class UserTaskService : IUserTaskService
    {
        private UserTask _userTask;
        private Timeframe _runningTimeFrame;

        public UserTask End()
        {
            throw new NotImplementedException();
        }

        public UserTask Pause()
        {
            throw new NotImplementedException();
        }

        public UserTask Start(string name)
        {
            _userTask = new UserTask { Name = name };
            _runningTimeFrame = new Timeframe { Start = DateTime.Now };
            _userTask.Timeframes = new List<Timeframe> { _runningTimeFrame };

            return _userTask;
        }
    }
}
