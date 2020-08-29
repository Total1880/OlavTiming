using OlavTiming.Models;
using OlavTiming.Repositories;
using OlavTiming.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OlavTiming.Services
{
    public class UserTaskService : IUserTaskService
    {
        private readonly IRepository<UserTask> _userTaskRepository;
        private UserTask _userTask;
        private Timeframe _runningTimeFrame;

        public UserTaskService(IRepository<UserTask> userTaskRepository)
        {
            _userTaskRepository = userTaskRepository;
        }

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

        public IList<UserTask> Create(IList<UserTask> userTaskList)
        {
            foreach (var userTask in userTaskList)
            {
                if (userTask.Id <= 0)
                {
                    userTask.Id = userTaskList.Max(u => u.Id) + 1;
                }
            }
            return _userTaskRepository.Create(userTaskList);
        }

        public IList<UserTask> Get()
        {
            return _userTaskRepository.Get();
        }

        private void SortTimeFrame()
        {
            _userTask.Timeframes.OrderBy(t => t.Start);
        }
    }
}
