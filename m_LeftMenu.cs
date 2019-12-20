using System;
using System.Web.UI;
using User.Web.WebBase;

public class m_LeftMenu : UserControl
{
    protected string rund = "";
    protected string sltType = "";

    protected string CodeValidate()
    {
        string str = string.Empty;
        Random random = new Random();
        for (int i = 0; i < 4; i++)
        {
            int num = random.Next();
            str = str + ((char) (0x30 + ((ushort) (num % 10)))).ToString();
        }
        return (DateTime.Now.ToString("MMddhhmmss") + str);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        this.rund = this.CodeValidate();
        this.sltType = ((this.Parent as TemplateControl) as MemberPageBase_Mobile).LotteryTypeSave();
    }
}

