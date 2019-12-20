using System;
using User.Web.WebBase;

public class m_XYFTSG_3456 : MemberPageBase_Mobile
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
            this.lottery_type = 0x15.ToString();
        }
        this.player_type = base.qq("player_type");
        if (string.IsNullOrEmpty(this.player_type))
        {
            this.player_type = "3456";
        }
    }
}

