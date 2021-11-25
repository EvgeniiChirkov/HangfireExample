using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace Hangfire.Example.Tasks
{
    public static class TaskInformant
    {
        public static readonly Dictionary<JobType, IJob> Jobs = new();
        public static void FillTasks(IServiceCollection serviceCollection)
        {
            var assembly = typeof(TaskInformant).Assembly;

            var taskTypes = assembly.GetExportedTypes()
                .Where(x => typeof(IJob).IsAssignableFrom(x) && !x.IsInterface);

            foreach (var taskType in taskTypes)
            {
                var task = serviceCollection.BuildServiceProvider().GetService(taskType) as IJob;
                Jobs.Add(task.JobType, task);
            }
        }
    }
}