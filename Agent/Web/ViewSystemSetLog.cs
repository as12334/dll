namespace Agent.Web
{
    using Agent.Web.WebBase;
    using LotterySystem.Model;
    using System;
    using System.Data;

    public class ViewSystemSetLog : MemberPageBase
    {
        protected string cloneName = "";
        protected int dataCount;
        protected DataTable dataTable;
        protected string[] FiledName = new string[] { "uid" };
        protected string[] FiledValue = new string[0];
        protected bool isAll = true;
        protected bool isCloneUser;
        protected string login_name = "";
        protected DataTable lotteryDT;
        protected string master_name = "";
        protected string page = "1";
        protected int pageCount;
        protected int pageSize = 20;
        protected string u_id = "";
        protected string u_name = "";
        protected string u_type = "";
        protected string url = "";
        protected string zj_name = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            string str = this.Session["user_name"].ToString();
            this.login_name = str;
            this.master_name = str;
            agent_userinfo_session _session = this.Session[str + "lottery_session_user_info"] as agent_userinfo_session;
            this.u_type = _session.get_u_type().Trim();
            this.zj_name = _session.get_zjname().Trim();
            if (this.Session["child_user_name"] != null)
            {
                this.isCloneUser = true;
                this.cloneName = this.Session["child_user_name"].ToString();
                this.login_name = this.cloneName;
            }
            if (!this.u_type.Equals("zj"))
            {
                base.Response.End();
            }
            this.dataTable = CallBLL.cz_system_set_log_bll.get_lottery_set_log();
        }
    }
}

