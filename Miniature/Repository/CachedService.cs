using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;

namespace Miniature.Repository
{
    public class CachedService : ICachedService
    {
        private readonly string _idKey;

        private ConnectionMultiplexer _redis;

        private IDatabase _db;

        private readonly IConfiguration _configuration;

#pragma warning disable CS8618
        public CachedService(IConfiguration configuration)
        {
            _configuration = configuration;
            _idKey = "counter";
            ConfigureRedis();
            SetIdKeyValue();
        }

#pragma warning disable CS8618 
        public CachedService(IConfiguration configuration, string idKey)
        {
            _configuration = configuration;
            _idKey = idKey;
            ConfigureRedis();
            SetIdKeyValue();
        }

        private void ConfigureRedis()
        {
            string? connection = _configuration.GetValue<string>("RedisConnString");
            var config = new ConfigurationOptions()
            {
                EndPoints = { connection ?? string.Empty },
                AbortOnConnectFail = false,
            };
            Console.WriteLine($"Connection String={connection}");
            _redis = ConnectionMultiplexer.Connect(config);
            _db = _redis.GetDatabase();
        }

        private void SetIdKeyValue()
        {
            _db.StringIncrement(_idKey, 10000000);
        }

        public async Task<long> Increment()
        {
            long value = await _db.StringIncrementAsync(_idKey);
            return value;
        }

        public async Task Set(string Key, string Value)
        {
            await _db.StringSetAsync(Key, Value);
        }

        public async Task Set(long key, string Value)
        {
            await _db.StringSetAsync(key.ToString(), Value);
        }

        public async Task<string> Get(string key)
        {
            string? val = await _db.StringGetAsync(key);
            return val ?? string.Empty;
        }

        public async Task<string> Get(long key)
        {
            string? val = await _db.StringGetAsync(key.ToString());
            return val ?? string.Empty;
        }
    }
}
