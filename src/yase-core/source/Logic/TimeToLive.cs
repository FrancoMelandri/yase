using System.Security.Cryptography;
using System.Text;

namespace yase_core.Logic 
{
    public interface ITimeToLive
    {
        long Get(int expiration);
        long Now();
    }

    class TimeToLive : ITimeToLive 
    {
        public long Get(int expiration)
        {
            return  System.DateTime.Now.Ticks + System.TimeSpan.TicksPerMinute * expiration;
        }

        public long Now()
        {
            return  System.DateTime.Now.Ticks;
        }
    }
}
