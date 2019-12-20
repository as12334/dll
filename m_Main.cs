using System;
using System.Data;
using User.Web.WebBase;

public class m_Main : MemberPageBase_Mobile
{
    protected DataTable lotteryTable = null;
    protected string rund = "";
    protected string sltType = "";
    protected string vrCARUrl = "";
    protected string vrSSCUrl = "";

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
        base.Response.Buffer = true;
        base.Response.ExpiresAbsolute = DateTime.Now.AddSeconds(-1.0);
        base.Response.Expires = 0;
        base.Response.CacheControl = "no-cache";
        base.Response.AddHeader("Pragma", "No-Cache");
        if (!string.IsNullOrEmpty(this.Session["modifypassword"]))
        {
            this.Session.Abandon();
            base.Response.Write("<script>top.location.href='/m'</script>");
            base.Response.End();
        }
        base.IsUserLoginByMobile();
        this.rund = this.CodeValidate();
        this.sltType = base.LotteryTypeSave();
        this.lotteryTable = base.GetLotteryList();
        foreach (DataRow row in this.lotteryTable.Rows)
        {
            string str = row["id"].ToString();
            int num = 0x12;
            if (str.Equals(num.ToString()))
            {
                this.vrCARUrl = string.Format("VRCAR_lm.aspx?lottery_type={0}&player_type=lm&r={1}", 0x12, this.rund);
            }
            num = 0x13;
            if (str.Equals(num.ToString()))
            {
                this.vrSSCUrl = string.Format("VRSSC_lm.aspx?lottery_type={0}&player_type=lm&r={1}", 0x13, this.rund);
            }
        }
    }
}

