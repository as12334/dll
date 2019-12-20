using LotterySystem.Common;
using System;
using User.Web.WebBase;

public class m_PKBJL_p3 : MemberPageBase_Mobile
{
    protected string lottery_type = "";
    protected string player_type = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        base.Response.Expires = 0;
        base.IsUserLoginByMobile();
        base.UsePageCache();
        this.lottery_type = base.qq("lottery_type");
        if (string.IsNullOrEmpty(this.lottery_type))
        {
            this.lottery_type = 8.ToString();
        }
        this.player_type = base.qq("player_type");
        this.player_type = "p3";
        CookieHelper.SetCookie("PKBJL_TableType_Record_Current_User", this.player_type);
    }
}

