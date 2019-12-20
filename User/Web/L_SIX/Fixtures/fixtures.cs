namespace User.Web.L_SIX.Fixtures
{
    using System;
    using User.Web.WebBase;

    public class fixtures : MemberPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            base.Response.Expires = 0;
            base.Response.CacheControl = "no-cache";
        }
    }
}

