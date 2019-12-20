using LotterySystem.Model;
using System;
using System.Data;
using User.Web.WebBase;

public class m_Report_JeuWeek_SIX : MemberPageBase_Mobile
{
    protected DataTable kcLastWeekDT = null;
    protected DataTable kcThisWeekDT = null;
    protected DataTable sixLastWeekDT = null;
    protected DataTable sixThisWeekDT = null;
    protected cz_userinfo_session uModel;
    protected string userName = null;

    protected string DisplayString(string masterID)
    {
        return "display:;";
    }

    protected string getweek(DateTime ss)
    {
        string str = ss.DayOfWeek.ToString();
        switch (str)
        {
            case "Monday":
                return "一";

            case "Tuesday":
                return "二";

            case "Wednesday":
                return "三";

            case "Thursday":
                return "四";

            case "Friday":
                return "五";

            case "Saturday":
                return "六";

            case "Sunday":
                return "日";
        }
        return "";
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        base.Response.Expires = 0;
        base.IsUserLoginByMobile();
        this.userName = this.Session["user_name"].ToString();
        this.uModel = base.GetUserModelInfo;
    }
}

