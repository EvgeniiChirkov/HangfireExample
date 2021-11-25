using System;
using System.Collections.Generic;

namespace Hangfire.Example.Tasks
{
    public interface IHangfireJobMemory
    {
        void AddJob(string jobId, JobType jobType, DateTime startedAt);
        void RemoveJob(string jobId);
    }
    
    public class HangfireJobMemory : IHangfireJobMemory
    {
        private readonly Dictionary<string, JobInfo> _jobs = new();

        public void AddJob(string jobId, JobType jobType, DateTime startedAt)
        {
            _jobs.Add(jobId, new JobInfo()
            {
                Id = jobId,
                Type = jobType,
                StartedAt = startedAt
            });
        }

        public void RemoveJob(string jobId)
        {
            _jobs.Remove(jobId);
        }
    }
}