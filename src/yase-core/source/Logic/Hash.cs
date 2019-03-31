using System.Security.Cryptography;
using System.Text;

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
            using (MD5 md5Hash = MD5.Create()) 
            {
                var data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(source));
                var sBuilder = new StringBuilder();
                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }
                return sBuilder.ToString().Substring(0, length);
            }
        }
    }
}
