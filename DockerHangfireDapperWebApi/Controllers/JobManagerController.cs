using Docker.Services;
using Microsoft.AspNetCore.Mvc;

namespace Docker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExemploController : ControllerBase
    {
        private readonly IExemploService exemploService;

        public ExemploController(IExemploService exemploService)
        {
            this.exemploService = exemploService;
        }

        [HttpGet]
        public int Get() =>
            exemploService.GetQuantidadeJobs();
            
    }
}