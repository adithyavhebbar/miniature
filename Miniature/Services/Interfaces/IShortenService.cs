namespace Miniature.Services.Interfaces
{
    public interface IShortenService
    {
        public Task<string> Shorten(string Url);

        public Task<string> Expand(string Url);
    }
}
