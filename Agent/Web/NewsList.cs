namespace Agent.Web
{
    using Agent.Web.WebBase;
    using LotterySystem.Common;
    using LotterySystem.Model;
    using System;
    using System.Data;

    public class NewsList : MemberPageBase
    {
        protected int dataCount;
        protected DataTable DT;
        protected string[] FiledName = new string[0];
        protected string[] FiledValue;
        protected int page = 1;
        protected int pageCount;
        protected int pageSize = 20;
        protected string skin = "";
        protected string sysName = PageBase.get_GetLottorySystemName();

        protected void Page_Load(object sender, EventArgs e)
        {
            string str = this.Session["user_name"].ToString();
            this.skin = (this.Session[str + "lottery_session_user_info"] as agent_userinfo_session).get_u_skin();
            if (!string.IsNullOrEmpty(LSRequest.qq("page")))
            {
                this.page = Convert.ToInt32(LSRequest.qq("page"));
            }
            if (this.page < 1)
            {
                this.page = 1;
            }
            int num = base.get_current_master_id();
            DataSet set = CallBLL.cz_ad_bll.GetAdByPage(this.Session["user_type"].ToString(), num, this.page - 1, this.pageSize, ref this.pageCount, ref this.dataCount);
            if (((set != null) && (set.Tables.Count > 0)) && (set.Tables[0].Rows.Count > 0))
            {
                this.DT = set.Tables[0];
            }
        }
    }
}

