using LotterySystem.Common;
using System;
using User.Web.WebBase;

public class m_Quit : MemberPageBase_Mobile
{
    protected void Page_Load(object sender, EventArgs e)
    {
        base.Response.Expires = 0;
        base.IsUserLoginByMobile();
        string str = this.Session["user_name"].ToString();
        if (FileCacheHelper.get_RedisStatOnline().Equals(0))
        {
            MemberPageBase.update_online_user(str);
        }
        this.Session.Abandon();
        base.Response.Write("<SCRIPT type=\"text/javascript\">top.location.href = \"/m\";</script>");
        base.Response.End();
    }
}

