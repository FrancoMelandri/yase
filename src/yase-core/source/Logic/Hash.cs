
namespace yase_core.Logic 
{
    public interface IHash
    {
        string Get(string source, int length);
    }

    class Hash : IHash 
    {
        public string Get(string source, int length)
        {
            return "";
        }
    }
}
