using Miniature.Models;
using Miniature.Repository;
using Miniature.Services.Interfaces;
using System.Text;

namespace Miniature.Services
{
    public class ShortenerService : IShortenService
    {
        private readonly IDConverter IDConverter;

        private readonly ICachedService _cacheService;

        private readonly string _basePath = string.Empty;
        private readonly string _httpScheme = string.Empty;
        public ShortenerService(ICachedService cachedService) 
        {
            IDConverter = IDConverter.GetInstance();
            _cacheService = cachedService;
            _basePath = Environment.GetEnvironmentVariable("BasePath");
            _httpScheme = Environment.GetEnvironmentVariable("Scheme");
        }
        public async Task<string> Expand(string shortenedValue)
        {
            int len = shortenedValue.Length;
            char[] chars = shortenedValue.ToCharArray();

            long finalValue = 0;

            for (int i = 0; i < len; i++) 
            {
                var indexTable = IDConverter.GetCharToIndexTable();
                char c = chars[i];

                if (indexTable.ContainsKey(c))
                {
                    long remainder = indexTable.GetValueOrDefault(c);

                    finalValue = (long) (finalValue + (remainder * Math.Pow(62, len - i - 1)));
                }
            }
            string originalUrl = await _cacheService.Get(finalValue);
            return originalUrl;
        }

        public async Task<string> Shorten(string Url)
        {
            long id = await _cacheService.Increment();
            string shortenedUrl = await CreateUniqueId(id);

            await _cacheService.Set(id, Url);
            return $"{_httpScheme}://{_basePath}/{shortenedUrl}";
        }

        private async Task<string> CreateUniqueId(long id)
        {
            List<int> base62Id = ConvertBase10ToBase62(id);

            StringBuilder uniqueUrlId = new();
            List<char> indexToCharTable = IDConverter.GetIndexToCharTable();


            foreach (int digit in base62Id)
            {
                uniqueUrlId.Append(indexToCharTable[digit]);
            }

            return uniqueUrlId.ToString();
        }

        private List<int> ConvertBase10ToBase62(long id)
        {
            List<int> list = new List<int>();

            while(id > 0)
            {
                var rem = (int) (id % 62);

                list.Add(rem);

                id /= 62;
            }

            list.Reverse();

            return list;
        }
    }
}
