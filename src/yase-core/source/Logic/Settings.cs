using System;

namespace yase_core.Logic 
{
    public interface ISettings
    {
        string BaseUrl { get; }
        int Length { get; }
    }

    class Settings : ISettings
    {
        public string BaseUrl { get; set; } 
        public int Length { get; set; }
    }
}
