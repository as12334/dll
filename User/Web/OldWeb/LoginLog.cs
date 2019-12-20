namespace User.Web.OldWeb
{
    using LotterySystem.Model;
    using System;
    using System.Data;
    using User.Web.WebBase;

    public class LoginLog : MemberPageBase
    {
        protected DataTable DT = null;
        protected string skin = "";
        protected string u_type = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            cz_userinfo_session _session = new cz_userinfo_session();
            _session = this.Session[this.Session["user_name"].ToString() + "lottery_session_user_info"] as cz_userinfo_session;
            string str = _session.get_u_name().Trim();
            this.skin = _session.get_u_skin();
            DataSet newList = CallBLL.cz_login_log_bll.GetNewList(str, 50);
            if ((newList != null) && (newList.Tables.Count > 0))
            {
                this.DT = newList.Tables[0];
            }
        }
    }
}

