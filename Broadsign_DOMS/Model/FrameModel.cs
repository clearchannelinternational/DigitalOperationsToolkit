

using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;

namespace Broadsign_DOMS.Model
{
    public class FrameModel : IBroadsignAPIModel
    {
        
        public int Domain_id { get; set; }
        public int Geometry_type { get; set; }
        
        public int Interactivity_timeout { get; set; }
        public int Interactivity_trigger_id { get; set; }
        public int Loop_policy_id { get; set; }
       
        public string NewName { get; set; }
        public int Screen_no { get; set; }
        public int Height{ get; set; }
        public int Width{ get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }
        public bool Active { get; set; }
        public int Container_id { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public int Parent_id { get; set; }
        public Domain AssignedDomain { get; set; }



        //public static void UpdateRename(Domain domain, ObservableCollection<object> frames = null)
        //{
        //    //check if frames contains objects
        //    if (frames == null)
        //    {
        //        MessageBox.Show("please select or upload frames before updating");
        //        return;
        //    }

        //    string path = "/skin/v7";

        //    foreach(FrameModel f in frames)
        //    {
        //        var requestBody = JsonConvert.SerializeObject(new
        //        {
        //            active= f.Active,
        //            domain_id= f.Domain_id,
        //            geometry_type = f.Geometry_type,
        //            height = f.Height,
        //            id = f.Id,
        //            interactivity_timeout = f.Interactivity_timeout,
        //            interactivity_trigger_id = f.Interactivity_trigger_id,
        //            loop_policy_id = f.Loop_policy_id,
        //            name = f.NewName,
        //            parent_id= f.Parent_id,
        //            screen_no= f.Screen_no,
        //            width= f.Width,
        //            x= f.X,
        //            y= f.Y,
        //            z= f.Z

        //        });

        //        Requests.SendRequest(path, domain.Token, RestSharp.Method.PUT, requestBody);
        //        f.Name = f.NewName;
        //        f.NewName = "";
        //    };
        //}
    }
}
