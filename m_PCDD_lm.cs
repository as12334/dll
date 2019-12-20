using LotterySystem.Common;
using System;
using User.Web.WebBase;

public class m_PCDD_lm : MemberPageBase_Mobile
{
    protected string lottery_type = "";
    protected string player_type = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        base.Response.Expires = 0;
        base.Response.End();
        base.IsUserLoginByMobile();
        base.UsePageCache();
        this.lottery_type = LSRequest.qq("lottery_type");
        this.player_type = LSRequest.qq("player_type");
        if (string.IsNullOrEmpty(this.lottery_type))
        {
            this.lottery_type = 7.ToString();
        }
        if (string.IsNullOrEmpty(this.player_type))
        {
            this.player_type = "lm";
        }
    }
}

