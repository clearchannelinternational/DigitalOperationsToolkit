
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Windows;

namespace Broadsign_DOMS.Model
{
    public class ContainerModel : IBroadsignAPIModel
    {

        public int Group_id { get; set; }
        public string Parent_resource_type { get; set; }
        public bool Active { get; set; }
        public int Container_id { get; set; }
        public int Domain_id { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public int Parent_id { get; set; }
        public Domain AssignedDomain { get; set; }


        //    public static async Task GenerateContainers(Domain domain)
        //    {
        //        await Task.Delay(1);

        //        dynamic containers = ContainerModel._getContainers(domain.Token);
        //        if (containers != null)
        //        {
        //            foreach (var container in containers["container"])
        //            {
        //                //show message loading resource for country ...
        //                if (container.active == true)
        //                {
        //                    CommonResources.Containers.Add(new ContainerModel
        //                    {
        //                        Active = container.active,
        //                        Container_id = container.container_id,
        //                        Domain_id = container.domain_id,
        //                        Group_id = container.group_id,
        //                        Id = container.id,
        //                        Name = container.name,
        //                        Parent_id = container.parent_id,
        //                        Parent_resource_type = container.parent_resource_type,
        //                        AssignedDomain = domain

        //                    });
        //                }
        //            }
        //        }
        //    }
    }
}
