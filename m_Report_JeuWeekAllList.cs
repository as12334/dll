using LotterySystem.Common;
using LotterySystem.Model;
using System;
using System.Data;
using System.Web;
using User.Web.WebBase;

public class m_Report_JeuWeekAllList : MemberPageBase_Mobile
{
    protected string dataTitle = "";
    protected string findDate = "";
    protected DataTable kcDataTable = null;
    protected cz_userinfo_session uModel = null;
    protected string userName = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        base.Response.Expires = 0;
        base.IsUserLoginByMobile();
        this.findDate = LSRequest.qq("findDate");
        this.dataTitle = "(快)分類明細 - 日期:" + this.findDate;
        this.userName = HttpContext.Current.Session["user_name"].ToString();
        this.kcDataTable = CallBLL.cz_betold_kc_bll.GetReportByWeek(this.userName, base.GetUserModelInfo.get_su_type(), this.findDate, base.get_BetTime_KC());
    }
}

