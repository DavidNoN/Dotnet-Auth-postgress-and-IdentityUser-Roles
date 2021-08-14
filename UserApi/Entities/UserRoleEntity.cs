using System;
using Microsoft.AspNetCore.Identity;

namespace UserApi.Entities
{
    public class UserRoleEntity : IdentityUserRole<Guid>
    {
        public UserEntity User { get; set; }

        public RoleEntity Role { get; set; }
        
    }
}