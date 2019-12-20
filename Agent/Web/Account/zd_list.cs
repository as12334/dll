namespace Agent.Web.Account
{
    using Agent.Web.WebBase;
    using LotterySystem.Common;
    using LotterySystem.Model;
    using System;
    using System.Data;

    public class zd_list : MemberPageBase
    {
        protected int dataCount;
        protected string[] FiledName = new string[] { "lid", "ml", "state", "flag", "namesort", "timesort", "keyword", "uid", "ut", "sortName", "sortType" };
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
        protected string uid = "";
        protected string url = "";
        protected DataTable userDT;
        protected string ut = "";

        protected void GetCount(string u_name, ref string zdcount, ref string dlcount, ref string hycount)
        {
            foreach (DataRow row in CallBLL.cz_rate_kc_bll.GetUserCount(u_name, "zd").Rows)
            {
                string str = row[0].ToString();
                if (str != null)
                {
                    if (!(str == "zd"))
                    {
                        if (str == "dl")
                        {
                            goto Label_0074;
                        }
                        if (str == "hy")
                        {
                            goto Label_0084;
                        }
                    }
                    else
                    {
                        zdcount = row[1].ToString();
                    }
                }
                continue;
            Label_0074:
                dlcount = row[1].ToString();
                continue;
            Label_0084:
                hycount = row[1].ToString();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string str = this.Session["user_name"].ToString();
            agent_userinfo_session model = this.Session[str + "lottery_session_user_info"] as agent_userinfo_session;
            if ((!model.get_u_type().Trim().Equals("fgs") && !model.get_u_type().Trim().Equals("zj")) && !model.get_u_type().Trim().Equals("gd"))
            {
                base.Response.Redirect("../MessagePage.aspx?code=u100035&url=&issuccess=1&isback=0");
                base.Response.End();
            }
            base.Permission_Aspx_ZJ(model, "po_2_1");
            base.Permission_Aspx_DL(model, "po_6_1");
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
            this.uid = LSRequest.qq("uid");
            this.ut = LSRequest.qq("ut");
            if (model.get_u_type().Equals("gd") && string.IsNullOrEmpty(this.ut))
            {
                this.ut = "gd";
                this.uid = model.get_u_id();
            }
            if (model.get_u_type().Equals("fgs") && string.IsNullOrEmpty(this.ut))
            {
                this.ut = "fgs";
                this.uid = model.get_u_id();
            }
            if (!model.get_u_type().Equals("zj"))
            {
                cz_users userInfoByUID = CallBLL.cz_users_bll.GetUserInfoByUID(this.uid);
                if (!base.IsUpperLowerLevels(userInfoByUID.get_u_name(), model.get_u_type(), model.get_u_name()))
                {
                    base.Response.Redirect("../MessagePage.aspx?code=u100057&url=&issuccess=1&isback=0");
                    base.Response.End();
                }
            }
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
            this.url = string.Format("?lid={0}&ml={1}&state={2}&flag={3}&keyword={4}&sortName={5}&sortType={6}&page={7}&uid={8}&ut={9}", new object[] { this.lid, this.ml, this.state, this.flag, this.keyword, this.namesort, this.timesort, this.page, this.uid, this.ut });
            if (LSRequest.qq("searchHidden").Equals("search"))
            {
                this.ml = LSRequest.qq("ml");
                this.state = LSRequest.qq("userState");
                this.flag = LSRequest.qq("userFlag");
                this.keyword = LSRequest.qq("searchkey");
                this.page = LSRequest.qq("page");
                this.namesort = LSRequest.qq("sortName");
                this.timesort = LSRequest.qq("sortType");
                this.url = string.Format("?lid={0}&ml={1}&state={2}&flag={3}&keyword={4}&sortName={5}&sortType={6}&page={7}&uid={8}&ut={9}", new object[] { this.lid, this.ml, this.state, this.flag, this.keyword, this.namesort, this.timesort, 1, this.uid, this.ut });
                base.Response.Redirect(string.Format("zd_list.aspx{0}", this.url));
            }
            if (int.Parse(this.page) < 1)
            {
                this.page = "1";
            }
            if (FileCacheHelper.get_RedisStatOnline().Equals(1) || FileCacheHelper.get_RedisStatOnline().Equals(2))
            {
                this.userDT = CallBLL.cz_users_bll.GetUserZDList(this.state, "zd", this.flag, this.keyword, Convert.ToInt32(this.page) - 1, this.pageSize, ref this.pageCount, ref this.dataCount, this.namesort, this.timesort, this.uid, this.ut, "redis");
            }
            else
            {
                this.userDT = CallBLL.cz_users_bll.GetUserZDList(this.state, "zd", this.flag, this.keyword, Convert.ToInt32(this.page) - 1, this.pageSize, ref this.pageCount, ref this.dataCount, this.namesort, this.timesort, this.uid, this.ut);
            }
            this.FiledValue = new string[] { this.lid, this.ml, this.state, this.flag, this.namesort, this.timesort, this.keyword, this.uid, this.ut, this.sortNameState, this.sortTypeState };
        }
    }
}

