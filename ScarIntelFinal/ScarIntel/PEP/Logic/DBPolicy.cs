using System.Collections.Generic;
using DataAcess;
using Logic;

namespace PDP.Logic
{
    public class DbPolicy : ILogicPolicy
    {
        private static readonly IDal<Permission> pdal = new PermissionDal();
        private static readonly IDal<PermissionAssigment> padal = new PermissionAssigmentDal();
        private static readonly IDal<Role> rdal = new RoleDal();
        private static readonly IDal<UserAssingnment> uadal = new UserAssigmentDal();
        private static readonly IDal<User> udal = new UserDal();


        public User GetUser(string user, string pw)
        {
            return udal.Get(user, pw);
        }

        public IEnumerable<User> GetAllUsers()
        {
            return udal.GetAll();
        }


        public IEnumerable<Role> GetAllRoles()
        {
            return rdal.GetAll();
        }

        public Role GetRole(string roleName)
        {
            return rdal.Get(roleName);
        }


        public IEnumerable<Permission> GetAllPermissions()
        {
            return pdal.GetAll();
        }

        public Permission GetOnePermission(string method, string resource)
        {
            return pdal.Get(method, resource);
        }


        public IEnumerable<UserAssingnment> GetAllUserAssingnments()
        {
            return uadal.GetAll();
        }

        public UserAssingnment GetOneUserAssingnment(string user)
        {
            return uadal.Get(user);
        }


        public IEnumerable<PermissionAssigment> GetAllPermissionAssignments()
        {
            return padal.GetAll();
        }

        public PermissionAssigment GetOnePermissonAssigment(string roleName)
        {
            return padal.Get(roleName);
        }
    }
}