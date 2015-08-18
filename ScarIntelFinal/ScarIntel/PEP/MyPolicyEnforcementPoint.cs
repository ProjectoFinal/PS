using System.Web;
using System.Web.Mvc;
using Logic;

namespace PEP
{
    public class MyPolicyEnforcementPoint : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            string user = httpContext.User.Identity.Name;
            string method = httpContext.Request.HttpMethod;
            string resource = httpContext.Request.Url.AbsoluteUri;

            return PolicyDecisionPoint.IsAcesssible(user, method, resource);
        }
    }
}