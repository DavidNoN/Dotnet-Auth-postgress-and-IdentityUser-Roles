using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace UserApi.Entities
{
    public class RoleEntity : IdentityRole<Guid>
    {
        public ICollection<UserRoleEntity> UserRoles { get; set; }
    }
}