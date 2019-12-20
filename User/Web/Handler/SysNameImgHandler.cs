namespace User.Web.Handler
{
    using System;
    using System.Web;
    using System.Web.SessionState;

    public class SysNameImgHandler : IHttpHandler, IRequiresSessionState
    {
        public void ProcessRequest(HttpContext hc)
        {
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}

