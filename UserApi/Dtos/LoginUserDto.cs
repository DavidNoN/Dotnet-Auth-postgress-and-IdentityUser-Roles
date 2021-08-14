using System.ComponentModel.DataAnnotations;

namespace UserApi.Dtos
{
    public record LoginUserDto
    {
        [Required]
        [MinLength(4)]
        public string UserNameEmailPhone { get; set; }
        
        [Required]
        [StringLength(12, MinimumLength = 4)]
        public string Password { get; set; }
    }
}