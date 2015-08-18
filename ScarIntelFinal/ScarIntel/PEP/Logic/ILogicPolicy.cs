using System.Collections.Generic;

namespace Logic
{
    public interface ILogicPolicy
    {
        User GetUser(string user, string password);
        IEnumerable<User> GetAllUsers();


        IEnumerable<Role> GetAllRoles();
        Role GetRole(string role);


        IEnumerable<Permission> GetAllPermissions();
        Permission GetOnePermission(string method, string resource);


        IEnumerable<UserAssingnment> GetAllUserAssingnments();
        UserAssingnment GetOneUserAssingnment(string user);

        IEnumerable<PermissionAssigment> GetAllPermissionAssignments();
        PermissionAssigment GetOnePermissonAssigment(string roleName);
    }
}