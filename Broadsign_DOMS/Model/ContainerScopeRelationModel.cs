
using Broadsign_DOMS.Service;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Broadsign_DOMS.Model
{
    public class ContainerScopeRelationModel : IBroadsignAPIModel
    {

        public int User_id { get; set; }
        public bool Active { get; set ; }
        public int Container_id { get ; set ; }
        public int Domain_id { get ; set ; }
        public int Id { get ; set ; }
        public string Name { get ; set ; }
        public int Parent_id { get ; set ; }
        public Domain AssignedDomain { get ; set ; }

        //private static dynamic _getScopingRelation(string token)
        //{
        //    string path = "/container_scope_relationship/v1";
        //    Request.SendRequest(path, token, RestSharp.Method.GET);
        //    return JsonConvert.DeserializeObject(Request.Response.Content);
        //}

        //public static async Task GenerateScopingRelations(Domain domain)
        //{
        //    await Task.Delay(1);
        //    dynamic relation_users_containers = _getScopingRelation(domain.Token);

        //    if (relation_users_containers != null)
        //    {
        //        foreach (var ugsRelation in relation_users_containers["container_scope_relationship"])
        //        {
        //            if (ugsRelation.active == true)
        //            {
        //                CommonResources.Container_Scope_Relations.Add(new ContainerScopeRelationModel
        //                {
        //                    Active = ugsRelation.active,
        //                    Domain_id = ugsRelation.domain_id,
        //                    Id = ugsRelation.id,
        //                    Parent_id = ugsRelation.parent_id,
        //                    User_id = ugsRelation.user_id,
        //                    AssignedDomain = domain
        //                });
        //            }
        //        }

        //    }
        //}
    }
}
