using Broadsign_DOMS.Resource;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Windows.Devices.Display.Core;

namespace Broadsign_DOMS.Model
{
    public class PlayerModel : BroadsignAPIModel
    {

        public int Config_profile_bag_id { get; set; }
        public string Custom_unique_id { get; set; }
        public string Db_pickup_tm_utc { get; set; }
        public int Discovery_status { get; set; }
        public int Display_unit_id { get; set; }
        public int Domain_id { get; set; }
        public string Geolocation { get; set; }
        public string NewName { get; set; }
        public int Nscreens { get; set; }
        public string Primary_mac_address { get; set; }
        public string Public_key_fingerprint { get; set; }
        public string Remote_clear_db_tm_utc { get; set; }
        public string Remote_reboot_tm_utc { get; set; }
        public string Secondary_mac_address { get; set; }
        public int Volume { get; set; }

        public static List<PlayerModel> GetPlayers(string token, int id = 0)
        {
            string path = "/host/v17";
            Requests.SendRequest(path, token, Method.GET);

            var root = JsonConvert.DeserializeObject<Dictionary<string, object>>(Requests.Response.Content);
            return JsonConvert.DeserializeObject<List<PlayerModel>>(root["host"].ToString());
        }
        public static void UpdatePlayers(Domain token, ObservableCollection<object> players = null)
        {
            if (players == null)
            {
                MessageBox.Show("No players select or found");
                return;
            }
            string path = "/host/v17";
            foreach (PlayerModel p in players)
            {

                var requestBody = JsonConvert.SerializeObject(new
                {
                    config_profile_bag_id = p.Config_profile_bag_id,
                    container_id = p.Container_id,
                    db_pickup_tm_utc = p.Db_pickup_tm_utc,
                    discovery_status = p.Discovery_status,
                    display_unit_id = p.Display_unit_id,
                    domain_id = p.Domain_id,
                    geolocation = p.Geolocation,
                    id = p.Id,
                    name = $"{p.NewName}",
                    nscreens = p.Nscreens,
                    public_key_fingerprint = p.Public_key_fingerprint,
                    remote_clear_db_tm_utc = p.Remote_clear_db_tm_utc,
                    remote_reboot_tm_utc = p.Remote_reboot_tm_utc,

                    volume = p.Volume
                });


                Requests.SendRequest(path,
                                     t: p.AssignedDomain.Token,
                                     Method.PUT,
                                     requestBody); ;
                p.Name = p.NewName;
                p.NewName = "";
            }






        }

        public static async Task GeneratePlayers(Domain domain)
        {
            //using a task delay in order to not overload the processor
            await Task.Delay(1);
            try
            {

                List<PlayerModel> players = GetPlayers(domain.Token);
                players
                    .ForEach(player =>
                    {
                    
                        player.AssignedDomain = domain;
                        CommonResources.Players.Add(player);  
                    
                    });
            


            }catch(Exception ex)    
            {

            }         
        }
    }
}
