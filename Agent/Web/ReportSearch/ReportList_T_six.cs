namespace Agent.Web.ReportSearch
{
    using Agent.Web.WebBase;
    using LotterySystem.Common;
    using LotterySystem.Model;
    using System;

    public class ReportList_T_six : MemberPageBase
    {
        protected string backUrl = "";
        protected agent_userinfo_session uModel;

        protected string GetReportOpenDate()
        {
            return CallBLL.cz_system_set_six_bll.GetSystemSet(100).get_ev_18();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.uModel = this.Session[this.Session["user_name"] + "lottery_session_user_info"] as agent_userinfo_session;
            base.Permission_Aspx_ZJ(this.uModel, "po_4_2");
            base.Permission_Aspx_DL(this.uModel, "po_7_2");
            this.backUrl = base.Server.UrlEncode("/ReportSearch/Report.aspx?lid=" + 100);
            LSRequest.AddReportCookies();
        }
    }
}

