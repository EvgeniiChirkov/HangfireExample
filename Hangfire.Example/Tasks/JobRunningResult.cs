using System;

namespace Hangfire.Example.Tasks
{
    public class JobRunningResult
    {
        public JobRunningResult(string jobIdentifier, DateTime runningAt)
        {
            JobIdentifier = jobIdentifier;
            RunningAt = runningAt;
        }

        public string JobIdentifier { get; set; }
        public DateTime RunningAt { get; set; }
    }
}