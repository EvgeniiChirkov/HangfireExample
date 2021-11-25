using System;

namespace Hangfire.Example.Tasks
{
    public class JobInfo
    {
        public string Id { get; set; }
        public JobType Type { get; set; }
        public DateTime StartedAt { get; set; }
    }
}