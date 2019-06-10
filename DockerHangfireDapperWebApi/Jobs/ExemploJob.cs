using Docker.Jobs.Core;
using Docker.Services;
using System;

namespace Docker.Jobs
{
    public interface IExemploJob : IHangfireJob { }

    public class ExemploJob : IExemploJob
    {
        private readonly IExemploService exemploService;

        public ExemploJob(IExemploService exemploService)
        {
            this.exemploService = exemploService;
        }

        public void Perform() =>
            Console.WriteLine(exemploService.ToString());
    }

}
