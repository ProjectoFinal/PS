using System.Collections.Generic;

namespace Logic
{
    public class UserAssingnment
    {
        public readonly List<Role> roles;
        public readonly User user;

        public UserAssingnment(User u, List<Role> rls)
        {
            roles = rls;
            user = u;
        }
    }
}