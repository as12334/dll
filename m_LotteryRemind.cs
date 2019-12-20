using System;
using System.Web.UI;
using User.Web.WebBase;

public class m_LotteryRemind : UserControl
{
    private string _lottery_type = "";
    protected MemberPageBase memberPageBase = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        this.memberPageBase = this.Parent as MemberPageBase;
    }

    public string lottery_type
    {
        get
        {
            return this._lottery_type;
        }
        set
        {
            this._lottery_type = value;
        }
    }
}

