using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PrtgAPI;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace PRTGAuto.Models
{
    public class PRTGData
    {
        public List<Probe> Probes { get; set; }
        public ObservableCollection<Group> Groups { get; set; }
        public PrtgClient client = PRTGConnection.Client;
        public PRTGData()
        {
            Probes = client.GetProbes();
        }
    }
}
