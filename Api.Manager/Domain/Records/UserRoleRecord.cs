namespace Api.Manager.Domain.Records
{
    public record UserRoleRecord
    {
        public int UserID { get; set; }
        public int RoleID { get; set; }
        public string RoleName { get; set; }
        public bool IsActive { get; set; }
    }
}
