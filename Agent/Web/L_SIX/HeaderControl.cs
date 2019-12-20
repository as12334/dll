namespace Agent.Web.L_SIX
{
    using LotterySystem.Model;
    using System;
    using System.Text;
    using System.Web.UI;

    public class HeaderControl : UserControl
    {
        protected agent_userinfo_session userModel;

        protected string AgentSIXKind()
        {
            StringBuilder builder = new StringBuilder();
            string str = base.Session["user_name"].ToString();
            agent_userinfo_session _session = base.Session[str + "lottery_session_user_info"] as agent_userinfo_session;
            if (string.IsNullOrEmpty(_session.get_six_kind()) || _session.get_six_kind().ToString().Equals("0"))
            {
                builder.Append("<option value=\"\" selected>全部</option>");
                builder.Append("<option value=\"A\">A盤</option>");
                builder.Append("<option value=\"B\">B盤</option>");
                builder.Append("<option value=\"C\">C盤</option>");
            }
            else
            {
                builder.Append(string.Format("<option value=\"{0}\">{1}盤</option>", _session.get_six_kind().ToUpper(), _session.get_six_kind().ToUpper()));
            }
            return builder.ToString();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string str = base.Session["user_name"].ToString();
            this.userModel = base.Session[str + "lottery_session_user_info"] as agent_userinfo_session;
        }
    }
}

