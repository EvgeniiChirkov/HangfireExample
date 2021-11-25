using Hangfire.Example.Services;
using Hangfire.Server;

namespace Hangfire.Example.Tasks
{
    public interface IHangFireFilter : IServerFilter
    {
        
    }
    
    public class HangFireFilter: IHangFireFilter
        {
            private readonly IHangfireJobMemory _hangfireJobMemory;

            public HangFireFilter(IHangfireJobMemory hangfireJobMemory)
            {
                _hangfireJobMemory = hangfireJobMemory;
            }

            public void OnPerforming(PerformingContext filterContext)
            {
                var type = filterContext.BackgroundJob.Job.Method.DeclaringType;
                var jobId = filterContext.BackgroundJob.Id;
                // TODO _hangfireJobMemory.Add(jobId, ...);
            }

            public void OnPerformed(PerformedContext filterContext)
            {
                var jobId = filterContext.BackgroundJob.Id;
                _hangfireJobMemory.RemoveJob(jobId);
            }
        }
}