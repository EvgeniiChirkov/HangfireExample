using System;
using System.Threading.Tasks;
using Hangfire.Example.Services;

namespace Hangfire.Example.Tasks
{
    public interface IJob
    {
        public bool IsSingle { get; }
        public JobType JobType { get; }
        [AutomaticRetry(Attempts = 0)]
        Task Start<T>(T parameter);
    }

    public class SampleJob : IJob
    {
        private readonly ISampleService _sampleService;

        public SampleJob(ISampleService sampleService)
        {
            _sampleService = sampleService;
        }

        public bool IsSingle => true;
        public JobType JobType => JobType.SampleJob;

        public async Task Start<T>(T parameter)
        {
            if (parameter is not SampleJobParameter taskParameter)
                return;

            await _sampleService.DateTimeToConsole(taskParameter.DateTime);
        }
    }
    public class SampleJobParameter
    {
        public DateTime DateTime { get; set; }
    }
}