namespace Agent.Web.L_SIX
{
    using Agent.Web.WebBase;
    using LotterySystem.Model;
    using System;

    public class NewBet_six : MemberPageBase
    {
        protected int max_bet_id;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Session["user_type"].ToString().Equals("zj"))
            {
                base.Response.Redirect("/MessagePage.aspx?code=u100057&url=&issuccess=1&isback=0");
                base.Response.End();
            }
            agent_userinfo_session model = this.Session[this.Session["user_name"] + "lottery_session_user_info"] as agent_userinfo_session;
            base.Permission_Aspx_ZJ(model, "po_1_1");
        }
    }
}

