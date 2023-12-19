namespace Miniature.Models
{
    public class Shortener
    {
    }

    public class UrlShortnerDTO
    {
        public string Url { get; set; }
    }

    public class UrlIdCombination
    {
        public long Id { get; set; }

        public string Url { get; set; }
    }
}
