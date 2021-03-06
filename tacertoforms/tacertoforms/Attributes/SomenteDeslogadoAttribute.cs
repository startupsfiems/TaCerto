using System.Web.Mvc;
using System.Web.Mvc.Filters;
using System.Web.Routing;

namespace TaCertoForms.Attributes {
    public class SomenteDeslogadoAttribute : ActionFilterAttribute, IAuthenticationFilter {
        public void OnAuthentication(AuthenticationContext filterContext) {
            if(filterContext.HttpContext.Session["Logado"] != null && filterContext.HttpContext.Request.FilePath != "/Login/LogOff" && (bool)filterContext.HttpContext.Session["Logado"])
                filterContext.Result = new HttpUnauthorizedResult();
        }

        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext) {
            if(filterContext.Result == null || filterContext.Result is HttpUnauthorizedResult) {
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary {
                        { "controller", "Home" },
                        { "action", "Index" }
                    }
                );
            }
        }
    }
}