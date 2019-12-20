namespace Agent.Web.Account
{
    using Agent.Web.WebBase;
    using LotterySystem.Common;
    using LotterySystem.Model;
    using System;
    using System.Data;

    public class hy_list : MemberPageBase
    {
        protected int dataCount;
        protected string[] FiledName = new string[] { "lid", "ml", "state", "flag", "namesort", "timesort", "keyword", "uid", "ut", "zstype", "sortName", "sortType" };
        protected string[] FiledValue;
        protected string flag = "0";
        protected bool isOpenKC_M = true;
        protected bool isOpenSIX_M = true;
        protected string kc_usable_credit_sum = "0";
        protected string keyword = "";
        protected string lid = "";
        protected string login_u_type = "";
        protected string lottrty_kc = "";
        protected string lottrty_six = "";
        protected string ml = "";
        protected string namesort = "0";
        protected string page = "1";
        protected int pageCount;
        protected int pageSize = 20;
        protected string six_usable_credit_sum = "0";
        protected string sortNameState = "0";
        protected string sortTypeState = "0";
        protected string state = "0";
        protected string timesort = "0";
        protected string uid = "";
        protected string url = "";
        protected DataTable userDT;
        private agent_userinfo_session userModel;
        protected string ut = "";
        protected string zstype = "";

        protected string GetUtypeTxt(string su_type)
        {
            string str = "一般會員";
            string str2 = su_type;
            if (str2 == null)
            {
                return str;
            }
            if (!(str2 == "fgs"))
            {
                if (str2 != "gd")
                {
                    if (str2 != "zd")
                    {
                        return str;
                    }
                    return "直屬總代";
                }
            }
            else
            {
                return "直屬分公司";
            }
            return "直屬股東";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string str = this.Session["user_name"].ToString();
            this.login_u_type = this.Session["user_type"].ToString();
            this.userModel = this.Session[str + "lottery_session_user_info"] as agent_userinfo_session;
            if (((!this.userModel.get_u_type().Trim().Equals("zj") && !this.userModel.get_u_type().Trim().Equals("fgs")) && (!this.userModel.get_u_type().Trim().Equals("gd") && !this.userModel.get_u_type().Trim().Equals("zd"))) && !this.userModel.get_u_type().Trim().Equals("dl"))
            {
                base.Response.Redirect("/MessagePage.aspx?code=u100035&url=&issuccess=1&isback=0");
            }
            DataTable lotteryList = base.GetLotteryList();
            this.isOpenSIX_M = base.GetMasterLotteryIsOpen(lotteryList, 1);
            this.isOpenKC_M = base.GetMasterLotteryIsOpen(lotteryList, 2);
            DataTable table2 = lotteryList.DefaultView.ToTable(true, new string[] { "master_id" });
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
            this.zstype = LSRequest.qq("zstype");
            if (this.userModel.get_u_type().Equals("dl") && string.IsNullOrEmpty(this.ut))
            {
                this.ut = "dl";
                this.uid = this.userModel.get_u_id();
            }
            if (this.userModel.get_u_type().Equals("zd") && string.IsNullOrEmpty(this.ut))
            {
                this.ut = "zd";
                this.uid = this.userModel.get_u_id();
            }
            if (this.userModel.get_u_type().Equals("gd") && string.IsNullOrEmpty(this.ut))
            {
                this.ut = "gd";
                this.uid = this.userModel.get_u_id();
            }
            if (this.userModel.get_u_type().Equals("fgs") && string.IsNullOrEmpty(this.ut))
            {
                this.ut = "fgs";
                this.uid = this.userModel.get_u_id();
            }
            if (!this.userModel.get_u_type().Equals("zj"))
            {
                cz_users userInfoByUID = CallBLL.cz_users_bll.GetUserInfoByUID(this.uid);
                if (!base.IsUpperLowerLevels(userInfoByUID.get_u_name(), this.userModel.get_u_type(), this.userModel.get_u_name()))
                {
                    base.Response.Redirect("/MessagePage.aspx?code=u100057&url=&issuccess=1&isback=0");
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
            this.url = string.Format("?lid={0}&ml={1}&state={2}&flag={3}&keyword={4}&sortName={5}&sortType={6}&page={7}&uid={8}&ut={9}&zstype={10}", new object[] { this.lid, this.ml, this.state, this.flag, this.keyword, this.namesort, this.timesort, this.page, this.uid, this.ut, this.zstype });
            if (LSRequest.qq("searchHidden").Equals("search"))
            {
                this.ml = LSRequest.qq("ml");
                this.state = LSRequest.qq("userState");
                this.flag = LSRequest.qq("userFlag");
                this.keyword = LSRequest.qq("searchkey");
                this.page = LSRequest.qq("page");
                this.namesort = LSRequest.qq("sortName");
                this.timesort = LSRequest.qq("sortType");
                this.url = string.Format("?lid={0}&ml={1}&state={2}&flag={3}&keyword={4}&sortName={5}&sortType={6}&page={7}&uid={8}&ut={9}&zstype={10}", new object[] { this.lid, this.ml, this.state, this.flag, this.keyword, this.namesort, this.timesort, 1, this.uid, this.ut, this.zstype });
                base.Response.Redirect(string.Format("hy_list.aspx{0}", this.url));
            }
            if (int.Parse(this.page) < 1)
            {
                this.page = "1";
            }
            if (FileCacheHelper.get_RedisStatOnline().Equals(1) || FileCacheHelper.get_RedisStatOnline().Equals(2))
            {
                this.userDT = CallBLL.cz_users_bll.GetUserHYList(this.state, "hy", this.flag, this.keyword, Convert.ToInt32(this.page) - 1, this.pageSize, ref this.pageCount, ref this.dataCount, this.namesort, this.timesort, this.uid, this.ut, this.zstype, "redis");
            }
            else
            {
                this.userDT = CallBLL.cz_users_bll.GetUserHYList(this.state, "hy", this.flag, this.keyword, Convert.ToInt32(this.page) - 1, this.pageSize, ref this.pageCount, ref this.dataCount, this.namesort, this.timesort, this.uid, this.ut, this.zstype);
            }
            this.FiledValue = new string[] { this.lid, this.ml, this.state, this.flag, this.namesort, this.timesort, this.keyword, this.uid, this.ut, this.zstype, this.sortNameState, this.sortTypeState };
            DataTable table3 = CallBLL.cz_users_bll.GetUserHYCreditSum(this.state, "hy", this.uid, this.ut, this.zstype);
            if (table3 != null)
            {
                this.six_usable_credit_sum = Utils.GetMathRound(Convert.ToDouble(table3.Rows[0]["six_usable_credit"].ToString()), 0);
                this.kc_usable_credit_sum = Utils.GetMathRound(Convert.ToDouble(table3.Rows[0]["kc_usable_credit"].ToString()), 0);
            }
        }
    }
}

