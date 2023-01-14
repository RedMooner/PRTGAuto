using PrtgAPI;
using System.Collections.Generic;
using System.Windows.Documents;

namespace PRTGAuto.Models
{
    public static class PRTGConnection
    {
        public static PrtgClient Client;
        public static bool Connect(string url, string username, string password)
        {
            try
            {
                Client = new PrtgClient(url, username, password);
                //LoadProbes();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
