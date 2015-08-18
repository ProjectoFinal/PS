using PDP.Logic;

namespace Logic
{
    public class PolicyDecisionPoint
    {
        private static readonly ILogicPolicy _policy = new DbPolicy();

        public static bool IsAcesssible(string username, string action, string resource)
        {
            if (username == null || action == null || resource == null)
                return false;
            UserAssingnment ua = _policy.GetOneUserAssingnment(username);
            foreach (Role role in ua.roles)
            {
                PermissionAssigment pa = _policy.GetOnePermissonAssigment(role.Name);
                foreach (Permission permission in pa.Permissions)
                {
                    if (permission != null)
                    {
                        if (permission.Method.Equals(action) && permission.Resource.Equals(resource))
                            return true;
                    }
                }
            }
            return false;
        }

        public static User GetUser(string username, string password)
        {
            return _policy.GetUser(username, password);
        }
    }
}