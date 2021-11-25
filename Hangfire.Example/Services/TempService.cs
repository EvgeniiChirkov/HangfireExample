using System;
using System.Threading;
using System.Threading.Tasks;
using Hangfire.Example.Configuration;
using Microsoft.Extensions.Options;

namespace Hangfire.Example.Services
{
    public interface ISampleService
    {
        Task DateTimeToConsole(DateTime dateTime);
    }
    
    public class SampleService : ISampleService
    {
        private readonly ServiceSettings _serviceSettings;

        public SampleService(IOptions<ServiceSettings> options)
        {
            _serviceSettings = options.Value;
        }
        public async Task DateTimeToConsole(DateTime dateTime)
        {
            Console.WriteLine($"Start - {DateTime.Now}");
            Thread.Sleep(10);
            throw new Exception();
            Console.WriteLine($"{_serviceSettings.SampleSettings} - END - {DateTime.Now}");
        }
    }
}