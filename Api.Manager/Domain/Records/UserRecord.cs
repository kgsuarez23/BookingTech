namespace Api.Manager.Domain.Records
{
    public record UserRecord
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime RegistrationDate { get; set; }
        public bool IsActive { get; set; }
    }
}
