using Logic;

namespace PEP
{
    public class PdpUserProvider
    {
        public static bool IsValidUser(string username, string password)
        {
            return PolicyDecisionPoint.GetUser(username, password) != null;
        }

        public static bool IsAutorized(string user, string method, string resource)
        {
            return PolicyDecisionPoint.IsAcesssible(user, method, resource);
        }
    }
}