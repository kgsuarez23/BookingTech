namespace Api.Manager.Domain.Records
{
    public record RoleRecord
    {
        public int RoleID { get; set; }
        public string RoleName { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }
}
