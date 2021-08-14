using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace UserApi.Entities
{
    public class UserEntity : IdentityUser<Guid>
    {
        public string UserNameComplete { get; set; } = string.Empty;
        public string UserIdReal { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string UserAddress { get; set; } = string.Empty;
        public string UserExtraAddress { get; set; } = string.Empty;
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime LastActive { get; set; } = DateTime.Now;
        public string Gender { get; set; } = string.Empty;
        public PhotoEntity UserPhoto { get; set; } 
        public ICollection<UserRoleEntity> UserRoles { get; set; }
        
    }
}