namespace User.Web.Handler
{
    using System;
    using System.Web;
    using System.Web.SessionState;
    using User.Web.WebBase;

    public class BaseHandler : MemberPageBase, IHttpHandler, IRequiresSessionState
    {
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}

