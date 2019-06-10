using Docker.SimpleDAL;
using System.Linq;

namespace Docker.Services
{
    public class ExemploService : IExemploService
    {
        private readonly IDapperCommands _dapperCommands;

        public ExemploService(IDapperCommands dapperCommands) =>
            _dapperCommands = dapperCommands;

        public int GetQuantidadeJobs() =>
            _dapperCommands.Query<int>("select count(1) from HangFire.Job").FirstOrDefault();

    }
}
