namespace UserApi.Dtos
{
    public record TokenInfoDto
    {
        public string NameId { get; set; }
        public string UniqueName { get; set; }
        public string GivenName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string PhoneNumber { get; set; }
    }
}