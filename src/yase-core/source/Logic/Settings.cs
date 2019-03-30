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
        public Settings (string baseUrl, int length)
        {
            BaseUrl = baseUrl;
            Length = length;
        }

        public string BaseUrl { get; } 
        public int Length { get; }
    }

    public class SettingsBuilder
    {
        private string _baseUrl;
        private int _length;

        public SettingsBuilder WithBase(string baseUrl) 
        {
            _baseUrl = baseUrl;
            return  this;
        } 

        public SettingsBuilder WithLength(int length) 
        {
            _length = length;
            return  this;
        }

        public ISettings Build()
        {
            return new Settings(_baseUrl, _length);
        }
    }
}
