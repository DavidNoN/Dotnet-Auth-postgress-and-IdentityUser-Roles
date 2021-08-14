using System.ComponentModel.DataAnnotations;

namespace UserApi.Dtos
{
    public record RegisterUserDto
    {
        [Required]
        [MinLength(4)]
        public string UserNameComplete { get; set; }
        public string UserIdReal { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string DateOfBirth { get; set; }
        [Required]
        [MinLength(5)]
        public string UserName { get; set; }
        [Required] public bool? EmailConfirmed { get; set; }
        public string Gender { get; set; }
        [Required]
        [MinLength(7)]
        public string PhoneNumber { get; set; }
        [Required]
        public bool? PhoneNumberConfirmed { get; set; }
        [MinLength(5)]
        public string UserAddress { get; set; }
        public string UserExtraAddress { get; set; }
        public string SecurityStamp { get; set; }
        [Required]
        [StringLength(12, MinimumLength = 4)]
        public string Password { get; set; }
    }
}