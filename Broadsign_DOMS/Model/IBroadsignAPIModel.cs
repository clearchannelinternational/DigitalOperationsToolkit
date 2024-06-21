namespace Broadsign_DOMS.Model
{
    public interface IBroadsignAPIModel
    {
        bool Active { get; set; }
        int Container_id { get; set; }
        int Domain_id { get; set; }
        int Id { get; set; }
        string Name { get; set; }
        int Parent_id { get; set; }

        Domain AssignedDomain { get; set; }
    }
}