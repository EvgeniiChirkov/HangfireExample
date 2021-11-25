using System;
using System.Linq.Expressions;

namespace Hangfire.Example.Tasks
{
    public interface ITaskExecutor
    {
        public string Start(Expression<Action> methodCall);
    }
    
    public class HangfireTaskExecutor : ITaskExecutor
    {
        public string Start(Expression<Action> methodCall)
        {
            return BackgroundJob.Enqueue(methodCall);
        }
    }
}