namespace User.Web.OldWeb.ReportBill
{
    using LotterySystem.Common;
    using LotterySystem.Model;
    using System;
    using System.Data;
    using System.Web;
    using User.Web.WebBase;

    public class Report_six : MemberPageBase
    {
        protected string beginDate = "";
        protected int dataCount = 0;
        protected DataTable dataTable = null;
        protected string[] FiledName = new string[] { "findDate", "op", "isback" };
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
            if (!Utils.ValidLotteryID(LSRequest.qq("lid"), true))
            {
                base.Response.End();
            }
            this.PrevUrl = CookieHelper.GetCookie("cookiesreportprev");
            this.beginDate = LSRequest.qq("findDate");
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
            this.FiledValue = new string[] { this.beginDate, this.op, this.isback };
            this.userName = HttpContext.Current.Session["user_name"].ToString();
            this.users = CallBLL.cz_users_bll.UserLogin(this.userName);
            string str2 = this.users.get_su_type().ToString().Trim();
            if (str2 != null)
            {
                if (!(str2 == "dl"))
                {
                    if (str2 == "zd")
                    {
                        this.u_drawback = "dl_drawback";
                    }
                    else if (str2 == "gd")
                    {
                        this.u_drawback = "zd_drawback";
                    }
                    else if (str2 == "fgs")
                    {
                        this.u_drawback = "gd_drawback";
                    }
                }
                else
                {
                    this.u_drawback = "hy_drawback";
                }
            }
            this.dataTable = CallBLL.cz_betold_six_bll.GetReportByPageList(this.userName, this.page - 1, this.pageSize, ref this.pageCount, ref this.dataCount, this.beginDate + " 08:00:00", this.beginDate + " 23:59:59").Tables[0];
        }
    }
}

