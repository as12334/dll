using System;
using System.Runtime.CompilerServices;
using System.Web.UI;

public class m_BetDialog : UserControl
{
    private string _lottery_type = "";

    protected void Page_Load(object sender, EventArgs e)
    {
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

    public string numTable { get; set; }

    public string playType { get; set; }
}

