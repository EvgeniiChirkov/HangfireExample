using System;
using Hangfire.Example.Services;

namespace Hangfire.Example.Tasks
{
    public interface IJobManager
    {
        JobRunningResult TaskExecute<T>(JobType jobType, T parameter);
    }

    public class JobManager : IJobManager
    {
        private readonly ITaskExecutor _taskExecutor;
        private readonly IHangfireJobMemory _hangfireJobMemory;

        public JobManager(ITaskExecutor taskExecutor, IHangfireJobMemory hangfireJobMemory)
        {
            _taskExecutor = taskExecutor;
            _hangfireJobMemory = hangfireJobMemory;
        }

        public JobRunningResult TaskExecute<T>(JobType jobType, T parameter)
        {
            var task = TaskInformant.Jobs[jobType];
            // TODO if (task.IsSingle && _hangfireJobMemory.IsRunning(jobType))
            //  TODO   throw new JobInProgressException(jobType);

            //Task.Run(async () => await task.Start(parameter))
             //   .ContinueWith(t => { });

            _taskExecutor.Start(() => task.Start(parameter));

            return new JobRunningResult("jobIdentifier", DateTime.Now);
        }
    }

  
}