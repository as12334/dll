namespace Agent.Web.Account
{
    using Agent.Web.WebBase;
    using LotterySystem.Common;
    using LotterySystem.Model;
    using System;
    using System.Data;
    using System.Diagnostics;

    public class ViewOnlineUser : MemberPageBase
    {
        protected string cloneName = "";
        protected int dataCount;
        protected int days = 30;
        protected string[] FiledName = new string[] { "q_date" };
        protected string[] FiledValue = new string[0];
        protected bool isCloneUser;
        protected string lottrty_kc = "yes";
        protected string lottrty_six = "yes";
        protected DataTable onlineTable;
        protected string page = "1";
        protected int pageCount;
        protected int pageSize = 20;
        protected string skin = "";
        protected string tabState_kc = "";
        protected string tabState_six = "on";
        protected DataTable topTable;
        protected string u_type = "";
        protected string url = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            string str = this.Session["user_name"].ToString();
            agent_userinfo_session _session = this.Session[str + "lottery_session_user_info"] as agent_userinfo_session;
            this.u_type = _session.get_u_type().Trim();
            if (!_session.get_u_type().Trim().Equals("zj"))
            {
                base.Response.End();
            }
            if (this.Session["child_user_name"] != null)
            {
                this.isCloneUser = true;
                this.cloneName = this.Session["child_user_name"].ToString();
            }
            this.page = LSRequest.qq("page");
            if (string.IsNullOrEmpty(this.page))
            {
                this.page = "1";
            }
            else if (int.Parse(this.page) < 1)
            {
                this.page = "1";
            }
            string str2 = LSRequest.qq("q_date");
            if (string.IsNullOrEmpty(str2))
            {
                str2 = DateTime.Now.ToString();
            }
            else
            {
                str2 = Utils.StampToDateTime(str2).ToString();
            }
            DateTime now = DateTime.Now;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            if (FileCacheHelper.get_RedisStatOnline().Equals(1))
            {
                this.onlineTable = base.GetOnlineUserByPage(int.Parse(this.page) - 1, this.pageSize, ref this.pageCount, ref this.dataCount, Convert.ToDateTime(str2), "redis");
            }
            else if (FileCacheHelper.get_RedisStatOnline().Equals(2))
            {
                this.onlineTable = base.GetOnlineUserByPageStack(int.Parse(this.page) - 1, this.pageSize, ref this.pageCount, ref this.dataCount, Convert.ToDateTime(str2), "redis");
            }
            else
            {
                this.onlineTable = CallBLL.cz_stat_online_bll.GetOnlineUserByPage(int.Parse(this.page) - 1, this.pageSize, ref this.pageCount, ref this.dataCount, Convert.ToDateTime(str2));
            }
            stopwatch.Stop();
            DateTime time2 = DateTime.Now;
            TimeSpan span = new TimeSpan();
            span = (TimeSpan) (time2 - now);
            int seconds = span.Seconds;
            DateTime time3 = DateTime.Now;
            Stopwatch stopwatch2 = new Stopwatch();
            stopwatch2.Start();
            if (FileCacheHelper.get_RedisStatOnline().Equals(1))
            {
                this.topTable = base.get_top_online(this.days, "redis");
            }
            else if (FileCacheHelper.get_RedisStatOnline().Equals(2))
            {
                this.topTable = base.get_top_onlineStack(this.days, "redis");
            }
            else
            {
                this.topTable = base.get_top_online(this.days);
            }
            stopwatch2.Stop();
            DateTime time4 = DateTime.Now;
            TimeSpan span2 = new TimeSpan();
            span2 = (TimeSpan) (time4 - time3);
            int num6 = span2.Seconds;
            this.FiledValue = new string[] { Utils.DateTimeToStamp(Convert.ToDateTime(str2)).ToString() };
        }
    }
}

