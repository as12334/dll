namespace Agent.Web.ManageZJProfit
{
    using Agent.Web.WebBase;
    using LotterySystem.Common;
    using LotterySystem.Model;
    using System;
    using System.Data;
    using System.Web;

    public class Manage_ZJ_Profit : MemberPageBase
    {
        protected string CurUrl = "/ManageZJProfit/Manage_ZJ_Profit.aspx";
        protected DataTable dataTable;
        protected agent_userinfo_session userModel;

        protected void Page_Load(object sender, EventArgs e)
        {
            string str = this.Session["user_name"].ToString();
            this.userModel = this.Session[str + "lottery_session_user_info"] as agent_userinfo_session;
            if (!this.userModel.get_u_type().Trim().Equals("zj"))
            {
                base.Response.Redirect("../MessagePage.aspx?code=u100014&url=&issuccess=1&isback=0");
            }
            if (((this.Session["child_user_name"] != null) && this.userModel.get_u_type().Trim().Equals("zj")) && !RuleJudge.ChildOperateValid(this.userModel, "po_3_4", HttpContext.Current))
            {
                base.Response.Redirect("../MessagePage.aspx?code=u100014&url=&issuccess=1&isback=0");
                base.Response.End();
            }
            if (!FileCacheHelper.get_ManageZJProfit().Equals("1"))
            {
                base.Response.Redirect("../MessagePage.aspx?code=u100014&url=&issuccess=1&isback=0");
                base.Response.End();
            }
            if ((this.userModel.get_users_child_session() != null) && !this.userModel.get_users_child_session().get_is_admin().Equals(1))
            {
                base.Response.Redirect("../MessagePage.aspx?code=u100014&url=&issuccess=1&isback=0");
                base.Response.End();
            }
            this.dataTable = CallBLL.cz_zj_profit_set_bll.GetProfit();
        }
    }
}

