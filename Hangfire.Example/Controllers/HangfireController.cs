using Hangfire.Example.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Hangfire.Example.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HangfireController : ControllerBase
    {
        private readonly IJobManager _jobManager;

        public HangfireController(IJobManager jobManager)
        {
            _jobManager = jobManager;
        }

        [HttpPost("sample-job")]
        public ActionResult Sync([FromBody]SampleJobParameter parameter)
        {
            return Ok(_jobManager.TaskExecute(JobType.SampleJob, parameter));
        }
    }
}