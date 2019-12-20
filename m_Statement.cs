using System;
using User.Web.WebBase;

public class m_Statement : MemberPageBase_Mobile
{
    protected void Page_Load(object sender, EventArgs e)
    {
        base.Response.Expires = 0;
        base.IsUserLoginByMobile();
        base.UsePageCache();
    }
}

