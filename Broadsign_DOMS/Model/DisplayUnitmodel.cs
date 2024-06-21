using Broadsign_DOMS.Service;
using Broadsign_DOMS.Service;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Broadsign_DOMS.Model
{
    public class DisplayUnitModel : IBroadsignAPIModel
    {
        
        public string Address { get; set; }
        public int Bmb_host_id { get; set; }
        public int Display_unit_type_id { get; set; }
        public int Domain_id { get; set; }
        public bool Enforce_day_parts { get; set; }
        public bool Enforce_screen_controls { get; set; }
        public bool Export_enabled { get; set; }
        public int Export_first_enabled_by_user_id { get; set; }
        public string Export_first_enabled_tm { get; set; }
        public int Export_retired_by_user_id { get; set; }
        public string NewName { get; set; }
        public string Export_retired_on_tm { get; set; }
        public string External_id { get; set; }
        public string Geolocation { get; set; }
        public int Host_screen_count { get; set; }
        public string Timezone { get; set; }
        public int Virtual_host_screen_count { get; set; }
        public string Virtual_id { get; set; }
        public string Zipcode { get; set; }
        public bool Active { get; set; }
        public int Container_id { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public int Parent_id { get; set; }
        public Domain AssignedDomain { get; set; }


        //public static void UpdateDisplayUnits(Domain domain, ObservableCollection<object> displayUnits)
        //{
        //    if(displayUnits == null)
        //    {
        //        MessageBox.Show("Please select or upload display units before updating");
        //    }

        //    var path = "/display_unit/v12";

        //    foreach(DisplayUnitModel du in displayUnits)
        //    {
        //        var requestBody = JsonConvert.SerializeObject(new {
        //            active= du.Active,
        //            address= du.Address,
        //            bmb_host_id= du.Bmb_host_id,
        //            container_id= du.Container_id,
        //            display_unit_type_id= du.Display_unit_type_id,
        //            domain_id= du.Domain_id,
        //            enforce_day_parts= du.Enforce_day_parts,
        //            enforce_screen_controls= du.Enforce_day_parts,
        //            export_enabled= du.Enforce_screen_controls,
        //            external_id= du.External_id,
        //            geolocation = du.Geolocation,
        //            id= du.Id,
        //            name = du.NewName,
        //            timezone = du.Timezone,
        //            virtual_host_screen_count= du.Virtual_host_screen_count,
        //            virtual_id= du.Virtual_id,
        //            zipcode = du.Zipcode
        //        });
        //        Request.SendRequest(path, domain.Token, RestSharp.Method.PUT, requestBody);
        //        du.Name = du.NewName;
        //        du.NewName = "";
        //    }
        //}
    }
}
