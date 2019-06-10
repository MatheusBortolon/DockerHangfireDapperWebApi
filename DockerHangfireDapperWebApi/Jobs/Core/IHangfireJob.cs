namespace Docker.Jobs.Core
{
    public interface IHangfireJob
    {
        void Perform();
    }
}
