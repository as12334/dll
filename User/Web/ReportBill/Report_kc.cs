namespace User.Web.ReportBill
{
    using LotterySystem.Common;
    using LotterySystem.Model;
    using System;
    using System.Data;
    using System.Web;
    using User.Web.WebBase;

    public class Report_kc : MemberPageBase
    {
        protected string beginDate = "";
        protected int dataCount = 0;
        protected DataTable dataTable = null;
        protected string[] FiledName = new string[] { "findDate", "lid", "op", "isback" };
        protected string[] FiledValue = null;
        protected string isback = "0";
        protected string lotteryID = "";
        protected string op = "0";
        protected int page = 1;
        protected int pageCount = 0;
        protected int pageSize = 10;
        protected string PrevUrl = "";
        protected string u_drawback = "";
        protected string userName = "";
        protected cz_users users = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.beginDate = LSRequest.qq("findDate");
            string str = LSRequest.qq("lid");
            if (!Utils.ValidLotteryID(str, true))
            {
                base.Response.End();
            }
            this.PrevUrl = CookieHelper.GetCookie("cookiesreportprev");
            this.op = LSRequest.qq("op");
            this.isback = LSRequest.qq("isback");
            if (!string.IsNullOrEmpty(base.q("page")))
            {
                this.page = Convert.ToInt32(base.q("page"));
            }
            if (this.page < 1)
            {
                this.page = 1;
            }
            this.FiledValue = new string[] { this.beginDate, str, this.op, this.isback };
            this.userName = HttpContext.Current.Session["user_name"].ToString();
            this.users = CallBLL.cz_users_bll.UserLogin(this.userName);
            string str3 = this.users.get_su_type().ToString().Trim();
            if (str3 != null)
            {
                if (!(str3 == "dl"))
                {
                    if (str3 == "zd")
                    {
                        this.u_drawback = "dl_drawback";
                    }
                    else if (str3 == "gd")
                    {
                        this.u_drawback = "zd_drawback";
                    }
                    else if (str3 == "fgs")
                    {
                        this.u_drawback = "gd_drawback";
                    }
                }
                else
                {
                    this.u_drawback = "hy_drawback";
                }
            }
            string str2 = "cz_betold_kc";
            if (DateTime.Now.AddHours((double) -int.Parse(base.get_BetTime_KC())).ToString("yyyy-MM-dd") == this.beginDate)
            {
                str2 = "cz_bet_kc";
            }
            DateTime time2 = Convert.ToDateTime(this.beginDate);
            DateTime now = DateTime.Now;
            now = time2.AddDays(1.0);
            if (str2.Equals("cz_betold_kc"))
            {
                this.dataTable = CallBLL.cz_betold_kc_bll.GetReportByPageList(this.userName, this.page - 1, this.pageSize, ref this.pageCount, ref this.dataCount, this.beginDate + " 07:00:00", now.ToString("yyyy-MM-dd") + " 07:00:00", str).Tables[0];
            }
            else
            {
                this.dataTable = CallBLL.cz_bet_kc_bll.GetReportByPageList(this.userName, this.page - 1, this.pageSize, ref this.pageCount, ref this.dataCount, this.beginDate + " 07:00:00", now.ToString("yyyy-MM-dd") + " 07:00:00", str).Tables[0];
            }
        }
    }
}

