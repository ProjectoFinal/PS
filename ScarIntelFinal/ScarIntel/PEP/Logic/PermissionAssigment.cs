using System.Collections.Generic;

namespace Logic
{
    public class PermissionAssigment
    {
        public PermissionAssigment(Role role, List<Permission> permission)
        {
            Permissions = permission;
            Role = role;
        }

        public Role Role { get; private set; }
        public List<Permission> Permissions { get; private set; }
    }
}