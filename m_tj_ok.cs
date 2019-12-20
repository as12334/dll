using System;
using User.Web.WebBase;

public class m_tj_ok : MemberPageBase_Mobile
{
    protected string errorStr = "";
    protected string lottery_type = "";
    protected string player_type = "";
    protected string r_url = "";
    protected string successStr = "";
    protected string title = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        base.IsUserLoginByMobile();
        this.lottery_type = base.qq("lottery_type");
        this.player_type = base.qq("player_type");
        string str = base.qq("game_type");
        this.successStr = this.Session["successStrBetPhone"];
        this.errorStr = this.Session["errorStrBetPhone"];
        if (!string.IsNullOrEmpty(this.successStr))
        {
            this.successStr = base.Server.UrlDecode(this.successStr);
        }
        if (!string.IsNullOrEmpty(this.errorStr))
        {
            this.errorStr = base.Server.UrlDecode(this.errorStr);
        }
        if (string.IsNullOrEmpty(this.lottery_type))
        {
            this.lottery_type = 0.ToString();
        }
        string str2 = this.player_type;
        switch (int.Parse(this.lottery_type))
        {
            case 0:
                this.title = base.GetGameNameByID(0.ToString());
                this.r_url = "KL10_";
                break;

            case 1:
                this.title = base.GetGameNameByID(1.ToString());
                this.r_url = "CQSC_";
                break;

            case 2:
                this.title = base.GetGameNameByID(2.ToString());
                this.r_url = "PK10_";
                break;

            case 3:
                this.title = base.GetGameNameByID(3.ToString());
                this.r_url = "XYNC_";
                break;

            case 4:
                this.title = base.GetGameNameByID(4.ToString());
                this.r_url = "JSK3";
                break;

            case 5:
                this.title = base.GetGameNameByID(5.ToString());
                this.r_url = "KL8_";
                break;

            case 6:
                this.title = base.GetGameNameByID(6.ToString());
                this.r_url = "K8SC_";
                break;

            case 7:
                this.title = base.GetGameNameByID(7.ToString());
                this.r_url = "PCDD_";
                break;

            case 8:
                this.title = base.GetGameNameByID(8.ToString());
                this.r_url = "PKBJL_";
                break;

            case 9:
                this.title = base.GetGameNameByID(9.ToString());
                this.r_url = "XYFT5_";
                break;

            case 10:
                this.title = base.GetGameNameByID(10.ToString());
                this.r_url = "JSCAR_";
                break;

            case 11:
                this.title = base.GetGameNameByID(11.ToString());
                this.r_url = "SPEED5_";
                break;

            case 12:
                this.title = base.GetGameNameByID(12.ToString());
                this.r_url = "JSPK10_";
                break;

            case 13:
                this.title = base.GetGameNameByID(13.ToString());
                this.r_url = "JSCQSC_";
                break;

            case 14:
                this.title = base.GetGameNameByID(14.ToString());
                this.r_url = "JSSFC_";
                break;

            case 15:
                this.title = base.GetGameNameByID(15.ToString());
                this.r_url = "JSFT2_";
                break;

            case 0x10:
                this.title = base.GetGameNameByID(0x10.ToString());
                this.r_url = "CAR168_";
                break;

            case 0x11:
                this.title = base.GetGameNameByID(0x11.ToString());
                this.r_url = "SSC168_";
                break;

            case 0x12:
                this.title = base.GetGameNameByID(0x12.ToString());
                this.r_url = "VRCAR_";
                break;

            case 0x13:
                this.title = base.GetGameNameByID(0x13.ToString());
                this.r_url = "VRSSC_";
                break;

            case 20:
                this.title = base.GetGameNameByID(20.ToString());
                this.r_url = "XYFTOA_";
                break;

            case 0x15:
                this.title = base.GetGameNameByID(0x15.ToString());
                this.r_url = "XYFTSG_";
                break;

            case 0x16:
                this.title = base.GetGameNameByID(0x16.ToString());
                this.r_url = "HAPPYCAR_";
                break;

            case 100:
                this.title = base.GetGameNameByID(100.ToString());
                this.r_url = "SIX_";
                break;

            default:
                base.Response.End();
                break;
        }
        if (int.Parse(this.lottery_type).Equals(100))
        {
            this.r_url = this.r_url + str + ".aspx?lottery_type=" + this.lottery_type + "&player_type=" + str2;
        }
        else
        {
            this.r_url = this.r_url + str2 + ".aspx?lottery_type=" + this.lottery_type + "&player_type=" + str2;
        }
    }
}

