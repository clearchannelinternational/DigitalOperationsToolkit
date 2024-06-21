using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Broadsign_DOMS.Model
{
    public class ContainerScopeModel : IBroadsignAPIModel
    {

        public bool Can_see_above { get; set; }
        public int Scope_container_group_id { get;  set; }
        public int Scope_container_id { get; set; }
        public string Scope_resource_type { get; set; }
        public bool Active { get; set; }
        public int Container_id { get; set; }
        public int Domain_id { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public int Parent_id { get; set; }
        public Domain AssignedDomain { get; set; }

        //private static dynamic _getContainerScopes(string token, int scope_id = 0)
        //{
        //    Requests.SendRequest("/container_scope/v1", token, RestSharp.Method.GET);
        //    return JsonConvert.DeserializeObject(Requests.Response.Content);
        //}
        //public static async Task GeneratContainerScopes(Domain domain)
        //{
        //    await Task.Delay(1);
        //    dynamic scopes = _getContainerScopes(domain.Token);

        //    if (scopes != null)
        //    {
        //        foreach (var container_scope in scopes["container_scope"])
        //        {
        //            if (container_scope.active == true)
        //            {
        //                CommonResources.Container_Scopes.Add(new ContainerScopeModel
        //                {
        //                    Active = container_scope.active,
        //                    Can_see_above = container_scope.can_see_above,
        //                    Domain_id = container_scope.domain_id,
        //                    Id = container_scope.id,
        //                    Parent_id = container_scope.parent_id,
        //                    Scope_container_group_id = container_scope.scope_container_group_id,
        //                    Scope_container_id = container_scope.scope_container_id,
        //                    Scope_resource_type = container_scope.scope_resource_type,
        //                    AssignedDomain = domain
        //                });
        //            }
        //        }
        //    }
        //}
    }
}
