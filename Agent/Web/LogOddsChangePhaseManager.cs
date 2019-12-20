namespace Agent.Web
{
    using Agent.Web.WebBase;
    using LotterySystem.Common;
    using LotterySystem.Model;
    using System;
    using System.Data;

    public class LogOddsChangePhaseManager : MemberPageBase
    {
        protected string cloneName = "";
        protected int dataCount;
        protected DataTable dataTable;
        protected string[] FiledName = new string[] { "lid", "phaseNum" };
        protected string[] FiledValue = new string[0];
        protected bool isCloneUser;
        protected DataTable lotteryDT;
        protected string lotteryId = "";
        protected string page = "1";
        protected int pageCount;
        protected int pageSize = 20;
        protected string phaseNum = "";
        protected DataTable phaseNumTable;
        protected string u_type = "";
        protected string url = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            string str = this.Session["user_name"].ToString();
            agent_userinfo_session _session = this.Session[str + "lottery_session_user_info"] as agent_userinfo_session;
            this.u_type = _session.get_u_type().Trim();
            if (!_session.get_u_type().Trim().Equals("zj"))
            {
                base.Response.Redirect("/MessagePage.aspx?code=u100014&url=&issuccess=1&isback=0");
            }
            if (this.Session["child_user_name"] != null)
            {
                this.isCloneUser = true;
                this.cloneName = this.Session["child_user_name"].ToString();
            }
            this.lotteryId = LSRequest.qq("lid");
            if (string.IsNullOrEmpty(this.lotteryId))
            {
                base.Response.Redirect("/MessagePage.aspx?code=u100008&url=&issuccess=1&isback=0");
            }
            this.phaseNum = LSRequest.qq("phaseNum");
            if (string.IsNullOrEmpty(this.phaseNum))
            {
                base.Response.Redirect("/MessagePage.aspx?code=u100008&url=&issuccess=1&isback=0");
            }
            this.lotteryDT = base.GetLotteryList();
            base.IsLotteryExistForSysLog(this.lotteryId, "u100032", "1", "");
            this.page = LSRequest.qq("page");
            if (string.IsNullOrEmpty(this.page))
            {
                this.page = "1";
            }
            if (int.Parse(this.page) < 1)
            {
                this.page = "1";
            }
            DataSet set = CallBLL.cz_system_log_bll.GetSOptLogByPage(Convert.ToInt32(this.page) - 1, this.pageSize, ref this.pageCount, ref this.dataCount, (this.lotteryId == "") ? "" : base.GetGameNameByID(this.lotteryId), this.phaseNum);
            this.dataTable = set.Tables[0];
            this.FiledValue = new string[] { this.lotteryId, this.phaseNum };
        }
    }
}

