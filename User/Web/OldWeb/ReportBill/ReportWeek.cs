namespace User.Web.OldWeb.ReportBill
{
    using LotterySystem.Common;
    using LotterySystem.Model;
    using System;
    using System.Data;
    using System.Web;
    using User.Web.WebBase;

    public class ReportWeek : MemberPageBase
    {
        protected string beginDate = "";
        protected string clsOff = "";
        protected string clsOn = "";
        protected string kcCls = "";
        protected DataTable kcDataTable = null;
        protected string kcStyle = "";
        protected string masterId = "";
        protected string op = "0";
        protected string sixCls = "";
        protected DataTable sixDataTable = null;
        protected string sixStyle = "";
        protected cz_userinfo_session uModel;
        protected string userName = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.masterId = base.q("masterId");
            this.op = LSRequest.qq("op");
            DataSet list = CallBLL.cz_lottery_bll.GetList();
            string lotteryMasterID = base.GetLotteryMasterID(list.Tables[0]);
            if (string.IsNullOrEmpty(this.masterId))
            {
                this.masterId = lotteryMasterID.Split(new char[] { ',' })[0];
            }
            this.beginDate = LSRequest.qq("beginDate");
            this.userName = HttpContext.Current.Session["user_name"].ToString();
            this.uModel = this.Session[this.userName + "lottery_session_user_info"] as cz_userinfo_session;
            int num = 1;
            if (this.masterId.Equals(num.ToString()))
            {
                this.sixDataTable = CallBLL.cz_betold_six_bll.GetReportByWeek(this.userName, this.uModel.get_su_type(), this.beginDate);
            }
            else
            {
                this.kcDataTable = CallBLL.cz_betold_kc_bll.GetReportByWeek(this.userName, this.uModel.get_su_type(), this.beginDate, base.get_BetTime_KC());
            }
            CookieHelper.SetCookie("cookiesreportprev", "reportweek.aspx");
        }
    }
}

