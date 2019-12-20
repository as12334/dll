namespace Agent.Web.NewsManage
{
    using Agent.Web.WebBase;
    using LotterySystem.Common;
    using LotterySystem.Model;
    using System;
    using System.Data;
    using System.Web;

    public class news_list : MemberPageBase
    {
        protected string CurUrl = "";
        protected int dataCount;
        protected string[] FiledName = new string[0];
        protected string[] FiledValue = new string[0];
        protected DataTable newsTable;
        protected string page = "1";
        protected int pageCount;
        protected int pageSize = 20;
        protected DataTable userDT;

        protected void Page_Load(object sender, EventArgs e)
        {
            string str = this.Session["user_name"].ToString();
            agent_userinfo_session sessionInfo = this.Session[str + "lottery_session_user_info"] as agent_userinfo_session;
            if (!sessionInfo.get_u_type().Trim().Equals("zj"))
            {
                base.Response.Redirect("../MessagePage.aspx?code=u100014&url=&issuccess=1&isback=0");
            }
            if (((this.Session["child_user_name"] != null) && sessionInfo.get_u_type().Trim().Equals("zj")) && !RuleJudge.ChildOperateValid(sessionInfo, "po_3_4", HttpContext.Current))
            {
                base.Response.Redirect("../MessagePage.aspx?code=u100014&url=&issuccess=1&isback=0");
                base.Response.End();
            }
            this.page = LSRequest.qq("page");
            if (string.IsNullOrEmpty(this.page))
            {
                this.page = "1";
            }
            if (int.Parse(this.page) < 1)
            {
                this.page = "1";
            }
            string str2 = LSRequest.qq("isdel");
            if (!string.IsNullOrEmpty(str2) && str2.Equals("1"))
            {
                string str3 = LSRequest.qq("adid");
                if (!string.IsNullOrEmpty(str3))
                {
                    bool flag2 = CallBLL.cz_ad_bll.DeleteAd(Convert.ToInt32(str3));
                    string url = "news_list.aspx?page=" + this.page;
                    if (!flag2)
                    {
                        base.Response.Write(base.ShowDialogBox("刪除站內消息失敗！", url, 400));
                        base.Response.End();
                    }
                    else
                    {
                        base.Response.Write(base.ShowDialogBox("刪除站內消息成功！", url, 0));
                        base.Response.End();
                    }
                }
            }
            int num = base.get_current_master_id();
            DataSet set = CallBLL.cz_ad_bll.GetAdByPage(sessionInfo.get_u_type(), num, Convert.ToInt32(this.page) - 1, this.pageSize, ref this.pageCount, ref this.dataCount);
            if (((set != null) && (set.Tables.Count > 0)) && (set.Tables[0].Rows.Count > 0))
            {
                this.newsTable = set.Tables[0];
            }
            this.CurUrl = string.Format("news_list.aspx?page={0}", this.page);
        }
    }
}

