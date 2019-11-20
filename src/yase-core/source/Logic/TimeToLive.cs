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
            => System.DateTime.Now.Ticks + System.TimeSpan.TicksPerMinute * expiration;

        public long Now()
            => System.DateTime.Now.Ticks;
    }
}
