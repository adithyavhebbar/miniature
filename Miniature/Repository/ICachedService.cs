namespace Miniature.Repository
{
    public interface ICachedService
    {
        public Task<long> Increment();

        public Task Set(string key, string value);

        public Task Set(long key, string value);

        public Task<string> Get(string key);

        public Task<string> Get(long key);

    }
}
