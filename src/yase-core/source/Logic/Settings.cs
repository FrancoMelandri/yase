namespace yase_core.Logic
{
    public interface ISettings
    {
        string BaseUrl { get; }
        int Length { get; }
        int ttl { get; }
    }

    class Settings : ISettings
    {
        public string BaseUrl { get; set; } 
        public int Length { get; set; }
        public int ttl { get; set; }
    }
}
