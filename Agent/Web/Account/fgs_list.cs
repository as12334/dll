namespace Agent.Web.Account
{
    using Agent.Web.WebBase;
    using LotterySystem.Common;
    using LotterySystem.Model;
    using System;
    using System.Data;

    public class fgs_list : MemberPageBase
    {
        protected int dataCount;
        protected string[] FiledName = new string[] { "lid", "ml", "state", "flag", "namesort", "timesort", "keyword", "sortName", "sortType" };
        protected string[] FiledValue;
        protected string flag = "0";
        protected string keyword = "";
        protected string lid = "";
        protected string lottrty_kc = "";
        protected string lottrty_six = "";
        protected string ml = "";
        protected string namesort = "0";
        protected string page = "1";
        protected int pageCount;
        protected int pageSize = 20;
        protected string sortNameState = "0";
        protected string sortTypeState = "0";
        protected string state = "0";
        protected string timesort = "0";
        protected string url = "";
        protected DataTable userDT;

        protected void GetUserUid(string u_name, ref string fgsguid, ref string gdguid, ref string zdguid, ref string dlguid, ref string hyguid)
        {
            DataTable uIDByUpUserName = CallBLL.cz_users_bll.GetUIDByUpUserName("fgs", u_name);
            if (uIDByUpUserName != null)
            {
                for (int i = 0; i < uIDByUpUserName.Rows.Count; i++)
                {
                    if (uIDByUpUserName.Rows[i]["u_type"].ToString().Equals("fgs"))
                    {
                        fgsguid = uIDByUpUserName.Rows[i]["u_id"].ToString();
                    }
                    if (uIDByUpUserName.Rows[i]["u_type"].ToString().Equals("gd"))
                    {
                        gdguid = uIDByUpUserName.Rows[i]["u_id"].ToString();
                    }
                    if (uIDByUpUserName.Rows[i]["u_type"].ToString().Equals("zd"))
                    {
                        zdguid = uIDByUpUserName.Rows[i]["u_id"].ToString();
                    }
                    if (uIDByUpUserName.Rows[i]["u_type"].ToString().Equals("dl"))
                    {
                        dlguid = uIDByUpUserName.Rows[i]["u_id"].ToString();
                    }
                    if (uIDByUpUserName.Rows[i]["u_type"].ToString().Equals("hy"))
                    {
                        hyguid = uIDByUpUserName.Rows[i]["u_id"].ToString();
                    }
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string str = this.Session["user_name"].ToString();
            agent_userinfo_session model = this.Session[str + "lottery_session_user_info"] as agent_userinfo_session;
            if (!model.get_u_type().Trim().Equals("zj"))
            {
                base.Response.Redirect("../MessagePage.aspx?code=u100035&url=&issuccess=1&isback=0");
                base.Response.End();
            }
            base.Permission_Aspx_ZJ(model, "po_2_1");
            DataTable table2 = base.GetLotteryList().DefaultView.ToTable(true, new string[] { "master_id" });
            for (int i = 0; i < table2.Rows.Count; i++)
            {
                if (Convert.ToInt32(table2.Rows[i][0]).Equals(1))
                {
                    this.lottrty_six = table2.Rows[i][0].ToString();
                }
                else if (Convert.ToInt32(table2.Rows[i][0]).Equals(2))
                {
                    this.lottrty_kc = table2.Rows[i][0].ToString();
                }
            }
            this.lid = LSRequest.qq("lid");
            this.ml = LSRequest.qq("ml");
            this.state = LSRequest.qq("state");
            this.sortNameState = LSRequest.qq("sortName");
            this.sortTypeState = LSRequest.qq("sortType");
            if (string.IsNullOrEmpty(this.sortNameState))
            {
                this.sortNameState = "0";
            }
            if (string.IsNullOrEmpty(this.sortTypeState))
            {
                this.sortTypeState = "0";
            }
            this.namesort = this.sortNameState;
            this.timesort = this.sortTypeState;
            if (string.IsNullOrEmpty(this.state))
            {
                this.state = "0";
            }
            this.flag = LSRequest.qq("flag");
            if (string.IsNullOrEmpty(this.flag))
            {
                this.flag = "0";
            }
            this.keyword = LSRequest.qq("keyword");
            this.page = LSRequest.qq("page");
            if (string.IsNullOrEmpty(this.page))
            {
                this.page = "1";
            }
            this.url = string.Format("?lid={0}&ml={1}&state={2}&flag={3}&keyword={4}&sortName={5}&sortType={6}&page={7}", new object[] { this.lid, this.ml, this.state, this.flag, this.keyword, this.namesort, this.timesort, this.page });
            if (LSRequest.qq("searchHidden").Equals("search"))
            {
                this.ml = LSRequest.qq("ml");
                this.state = LSRequest.qq("userState");
                this.flag = LSRequest.qq("userFlag");
                this.keyword = LSRequest.qq("searchkey");
                this.page = LSRequest.qq("page");
                this.namesort = LSRequest.qq("sortName");
                this.timesort = LSRequest.qq("sortType");
                this.url = string.Format("?lid={0}&ml={1}&state={2}&flag={3}&keyword={4}&sortName={5}&sortType={6}&page={7}", new object[] { this.lid, this.ml, this.state, this.flag, this.keyword, this.namesort, this.timesort, this.page });
                base.Response.Redirect(string.Format("fgs_list.aspx?ml={0}&state={1}&flag={2}&keyword={3}&page=1&sortName={4}&sortType={5}", new object[] { this.ml, this.state, this.flag, this.keyword, this.sortNameState, this.sortTypeState }));
            }
            if (int.Parse(this.page) < 1)
            {
                this.page = "1";
            }
            if (FileCacheHelper.get_RedisStatOnline().Equals(1) || FileCacheHelper.get_RedisStatOnline().Equals(2))
            {
                this.userDT = CallBLL.cz_users_bll.GetUserFGSList(this.state, "fgs", this.flag, this.keyword, Convert.ToInt32(this.page) - 1, this.pageSize, ref this.pageCount, ref this.dataCount, this.namesort, this.timesort, "redis");
            }
            else
            {
                this.userDT = CallBLL.cz_users_bll.GetUserFGSList(this.state, "fgs", this.flag, this.keyword, Convert.ToInt32(this.page) - 1, this.pageSize, ref this.pageCount, ref this.dataCount, this.namesort, this.timesort);
            }
            this.FiledValue = new string[] { this.lid, this.ml, this.state, this.flag, this.namesort, this.timesort, this.keyword, this.sortNameState, this.sortTypeState };
        }
    }
}

