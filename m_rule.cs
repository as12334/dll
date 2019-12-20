using System;
using System.Data;
using User.Web.WebBase;

public class m_rule : MemberPageBase_Mobile
{
    protected DataTable lotteryDT = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        base.Response.Expires = 0;
        base.IsUserLoginByMobile();
        this.lotteryDT = base.GetLotteryList();
    }
}

