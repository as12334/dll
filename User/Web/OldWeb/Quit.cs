namespace User.Web.OldWeb
{
    using LotterySystem.Common;
    using System;
    using User.Web.WebBase;

    public class Quit : MemberPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string str = this.Session["user_name"].ToString();
            if (FileCacheHelper.get_RedisStatOnline().Equals(0))
            {
                MemberPageBase.update_online_user(str);
            }
            this.Session.Abandon();
            base.Response.Write(" <SCRIPT type=\"text/javascript\">top.location.href = '/';</script>");
            base.Response.End();
        }
    }
}

