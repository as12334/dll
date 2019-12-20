namespace Agent.Web.ReportSearch
{
    using Agent.Web.WebBase;
    using LotterySystem.BLL;
    using LotterySystem.Common;
    using LotterySystem.Model;
    using System;
    using System.Web;

    public class ReportList_B_kc : MemberPageBase
    {
        protected HttpCookie cookies;
        protected cz_users g_UserModel;
        protected string uid = "";
        protected agent_userinfo_session uModel;

        protected string GetReportOpenDate()
        {
            return new cz_system_set_kc_exBLL().GetReportOpenDate();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.uModel = this.Session[this.Session["user_name"] + "lottery_session_user_info"] as agent_userinfo_session;
            base.Permission_Aspx_ZJ(this.uModel, "po_4_1");
            base.Permission_Aspx_DL(this.uModel, "po_7_1");
            this.uid = LSRequest.qq("uid");
            if (!string.IsNullOrEmpty(this.uid))
            {
                this.g_UserModel = CallBLL.cz_users_bll.GetUserInfoByUID(this.uid);
                if ((!this.uModel.get_u_type().Equals("fgs") && !this.uModel.get_allow_view_report().Equals(1)) && !base.IsUpperLowerLevels(this.g_UserModel.get_u_name(), this.uModel.get_u_type(), this.uModel.get_u_name()))
                {
                    base.Response.Redirect("../MessagePage.aspx?code=u100057&url=&issuccess=1&isback=0&isopen=0");
                    base.Response.End();
                }
            }
            LSRequest.AddReportCookies();
        }
    }
}

