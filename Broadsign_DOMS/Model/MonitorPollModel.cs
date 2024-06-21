using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Broadsign_DOMS.Model
{
    public class MonitorPollModel : IBroadsignAPIModel
    {
        public int Client_resource_id { get; set; }
        
        public int Domain_id { get; set; }
        public int Monitor_status { get; set; }
        public string Poll_last_utc { get; set; }
        public string Poll_next_expected_utc { get; set; }
        public string Private_ip { get; set; }
        public string Product_version { get; set; }
        public string Public_ip { get; set; }
        public bool Active { get; set; }
        public int Container_id { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public int Parent_id { get; set; }
        public Domain AssignedDomain { get; set; }

        //public static dynamic GetMonitorPoll(string t, int host = 0)
        //{
        //    string path = "/monitor_poll/v2";
        //    Requests.SendRequest(path, t, RestSharp.Method.GET);
        //    return JsonConvert.DeserializeObject(Requests.Response.Content);
        //}

    }
}
