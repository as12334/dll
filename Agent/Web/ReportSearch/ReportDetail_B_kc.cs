namespace Agent.Web.ReportSearch
{
    using Agent.Web.WebBase;
    using LotterySystem.Common;
    using LotterySystem.Model;
    using System;

    public class ReportDetail_B_kc : MemberPageBase
    {
        protected string lid = "";
        protected string oddsid = "";
        protected string oddsidab = "";
        protected string phaseid = "";
        protected string playid = "";
        protected agent_userinfo_session uModel;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.uModel = this.Session[this.Session["user_name"] + "lottery_session_user_info"] as agent_userinfo_session;
            base.Permission_Aspx_ZJ(this.uModel, "po_4_1");
            base.Permission_Aspx_DL(this.uModel, "po_7_1");
            string str = LSRequest.qq("userid");
            if ((!string.IsNullOrEmpty(str) && !this.uModel.get_u_type().Equals("fgs")) && !this.uModel.get_allow_view_report().Equals(1))
            {
                cz_users userInfoByUID = CallBLL.cz_users_bll.GetUserInfoByUID(str);
                if (!base.IsUpperLowerLevels(userInfoByUID.get_u_name(), this.uModel.get_u_type(), this.uModel.get_u_name()))
                {
                    base.Response.Redirect("../MessagePage.aspx?code=u100057&url=&issuccess=1&isback=0&isopen=1");
                    base.Response.End();
                }
            }
        }
    }
}

