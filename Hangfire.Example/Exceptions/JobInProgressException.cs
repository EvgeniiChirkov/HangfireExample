using System;
using Hangfire.Example.Tasks;

namespace Hangfire.Example.Exceptions
{
    public class JobInProgressException : Exception
    {
        public JobInProgressException(JobType jobType)
        {
            Message = $"The job '{jobType}' is already in progress";
        }

        public override string Message { get; }
    }
}