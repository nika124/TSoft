namespace TSoft.Models
{
    public class Roles
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int AppObjectGroupId { get; set; }
        public int PermissionType { get; set; }
        public bool Status { get; set; }

    }
}
