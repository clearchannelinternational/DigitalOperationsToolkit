using Broadsign_DOMS.Service;
using RestSharp;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Windows;
using System.Threading.Tasks;

using System.Diagnostics;
using System.Collections.ObjectModel;
using System.Linq;
using System;

namespace Broadsign_DOMS.Model
{
    public class UserModel : IBroadsignAPIModel
    {

   
        #region Properties
        
        public bool Allow_auth_token { get; set; }
        
        public string Email { get; set; }
        public object Has_auth_token { get; set; }
        public ObservableCollection<GroupModel> Groups { get; set; }
        public ObservableCollection<ContainerScopeRelationModel> Group_ids { get; set; }
        public ObservableCollection<ContainerScopeModel> ScopingRelation { get; set; }
        public string Passwd { get; set; }
        public string Pending_single_sign_on_email { get; set; }
        public string Public_key_fingerprint { get; set; }
        public int Single_sign_on_id { get; set; }
        public string Username { get; set; }
        public bool Active { get ; set ; }
        public int Container_id { get ; set ; }
        public int Domain_id { get ; set ; }
        public int Id { get ; set ; }
        public string Name { get ; set ; }
        public int Parent_id { get ; set ; }
        public Domain AssignedDomain { get ; set ; }


        #endregion


        //public static void AddUsers(Domain domain,UserModel user = null, List<UserModel> users = null)
        //{
        //    if(user == null && users == null)
        //    {
        //        MessageBox.Show("Please add at least one user as argument");
        //        return;
        //    };
        //    string path = "/user/v13/add";
        //    dynamic requestBody = @"{ " +
        //        "\"container_id\":" + user.Container_id + ", " +
        //        "\"domain_id\":" + user.Domain_id + ", " +
        //        "\"name\": \"" + user.Name + "\", " +
        //        "\"passwd\": \"\", " +
        //        "\"sub_elements\": { " +
        //            "\"container_scope\": [ " +
        //                "{ \"can_see_above\": true, " +
        //                "\"scope_container_group_id\": 0, " +
        //                "\"scope_container_id\": 0 } ], " +
        //        "\"group\": " +
        //            "[ { \"id\":" + user.Groups[0].Id + "} ] }, " +
        //        "\"username\": \"" + user.Username + "\"}";
        //    dynamic requestBodyDeserialized = new
        //    {
        //        container_id = user.Container_id,
        //        domain_id = user.Domain_id,
        //        name = user.Name,
        //        passwd = "",
        //        sub_elements = user.Container_id,
        //    };
        //    Request.SendRequest(path, domain.Token, Method.POST, requestBody);
        //    MessageBox.Show(Request.Response.ResponseStatus.ToString());
        //}
        //#endregion

    }
}
