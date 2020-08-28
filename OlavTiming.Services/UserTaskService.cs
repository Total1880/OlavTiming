using OlavTiming.Models;
using OlavTiming.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OlavTiming.Services
{
    public class UserTaskService : IUserTaskService
    {
        private UserTask _userTask;
        private Timeframe _runningTimeFrame;

        public UserTask End()
        {
            SortTimeFrame();

            if (_userTask.Timeframes[_userTask.Timeframes.Count - 1].End == DateTime.MinValue)
            {
                _userTask.Timeframes[_userTask.Timeframes.Count - 1].End = DateTime.Now;
            }

            return _userTask;
        }

        public UserTask Pause()
        {
            SortTimeFrame();

            if (_userTask.Timeframes[_userTask.Timeframes.Count - 1].End == DateTime.MinValue)
            {
                _userTask.Timeframes[_userTask.Timeframes.Count - 1].End = DateTime.Now;
            }
            else
            {
                _runningTimeFrame = new Timeframe { Start = DateTime.Now };
                _userTask.Timeframes.Add(_runningTimeFrame);
            }

            return _userTask;
        }

        public UserTask Start(string name)
        {
            _userTask = new UserTask { Name = name };
            _runningTimeFrame = new Timeframe { Start = DateTime.Now };
            _userTask.Timeframes = new List<Timeframe> { _runningTimeFrame };
            SortTimeFrame();

            return _userTask;
        }

        private void SortTimeFrame()
        {
            _userTask.Timeframes.OrderBy(t => t.Start);
        }
    }
}
