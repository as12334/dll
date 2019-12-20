namespace Agent.Web
{
    using Agent.Web.WebBase;
    using LotterySystem.Model;
    using System;
    using System.Data;

    public class LoginLog : MemberPageBase
    {
        protected DataTable DT;
        protected string u_type = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            string str = this.Session["user_name"].ToString();
            agent_userinfo_session _session = this.Session[str + "lottery_session_user_info"] as agent_userinfo_session;
            this.u_type = _session.get_u_type().Trim();
            DataSet newList = CallBLL.cz_login_log_bll.GetNewList((this.Session["child_user_name"] != null) ? this.Session["child_user_name"].ToString() : this.Session["user_name"].ToString(), 50);
            if ((newList != null) && (newList.Tables.Count > 0))
            {
                this.DT = newList.Tables[0];
            }
        }
    }
}

