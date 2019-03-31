using System;
using yase_core.Models;

namespace yase_core.Logic
{
    public interface IStorageServiceWrapper
    {
        Option<ShortUrl> Get(string tiny);
    }

    class StorageServiceWrapper : IStorageServiceWrapper
    {
        public Option<ShortUrl> Get(string tiny)
        {
            return new ShortUrl().ToOption();
        }
    }
}