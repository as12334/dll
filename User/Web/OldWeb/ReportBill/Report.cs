namespace User.Web.OldWeb.ReportBill
{
    using LotterySystem.Common;
    using LotterySystem.Model;
    using System;
    using System.Data;
    using System.Web;
    using User.Web.WebBase;

    public class Report : MemberPageBase
    {
        protected DataTable kcLastWeekDT = null;
        protected DataTable kcThisWeekDT = null;
        protected string masterId = "";
        protected string[] masterIDSplit = null;
        protected DataTable sixLastWeekDT = null;
        protected DataTable sixThisWeekDT = null;
        protected cz_userinfo_session uModel;
        protected string userName = "";

        protected string DisplayString(string masterID)
        {
            for (int i = 0; i < this.masterIDSplit.Length; i++)
            {
                if (this.masterIDSplit[i].Equals(masterID))
                {
                    return "display:;";
                }
            }
            return "display:none;";
        }

        protected string getweek(DateTime ss)
        {
            string str = ss.DayOfWeek.ToString();
            switch (str)
            {
                case "Monday":
                    return "星期一";

                case "Tuesday":
                    return "星期二";

                case "Wednesday":
                    return "星期三";

                case "Thursday":
                    return "星期四";

                case "Friday":
                    return "星期五";

                case "Saturday":
                    return "星期六";

                case "Sunday":
                    return "星期日";
            }
            return "";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.userName = HttpContext.Current.Session["user_name"].ToString();
            this.uModel = this.Session[this.userName + "lottery_session_user_info"] as cz_userinfo_session;
            DataTable lotteryList = base.GetLotteryList();
            this.masterIDSplit = base.GetLotteryMasterID(lotteryList).Split(new char[] { ',' });
            CookieHelper.RemoveCookie("cookiesreportprev");
            string str = LSRequest.qq("id");
            if (string.IsNullOrEmpty(str))
            {
                this.masterId = this.masterIDSplit[0];
            }
            else
            {
                int num = 100;
                if (str.Equals(num.ToString()))
                {
                    this.masterId = "1";
                }
                else
                {
                    this.masterId = "2";
                }
            }
        }
    }
}

