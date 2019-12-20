using LotterySystem.Common;
using System;
using User.Web.WebBase;

public class m_PCDD_tm : MemberPageBase_Mobile
{
    protected string lottery_type = "";
    protected string player_type = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        base.Response.Expires = 0;
        base.IsUserLoginByMobile();
        base.UsePageCache();
        this.lottery_type = LSRequest.qq("lottery_type");
        if (string.IsNullOrEmpty(this.lottery_type))
        {
            this.lottery_type = 7.ToString();
        }
        this.player_type = LSRequest.qq("player_type");
        if (string.IsNullOrEmpty(this.player_type))
        {
            this.player_type = "tm";
        }
    }
}

