using LotterySystem.Common;
using System;
using User.Web.WebBase;

public class m_ClosedLottery : MemberPageBase_Mobile
{
    protected string lottery_type = "";
    protected string player_type = "";
    protected string title = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        base.IsUserLoginByMobile();
        this.lottery_type = LSRequest.qq("lottery_type");
        this.player_type = LSRequest.qq("player_type");
        if (string.IsNullOrEmpty(this.lottery_type))
        {
            this.lottery_type = "0";
        }
        switch (int.Parse(this.lottery_type))
        {
            case 0:
                this.title = base.GetGameNameByID(0.ToString());
                break;

            case 1:
                this.title = base.GetGameNameByID(1.ToString());
                break;

            case 2:
                this.title = base.GetGameNameByID(2.ToString());
                break;

            case 3:
                this.title = base.GetGameNameByID(3.ToString());
                break;

            case 4:
                this.title = base.GetGameNameByID(4.ToString());
                break;

            case 5:
                this.title = base.GetGameNameByID(5.ToString());
                break;

            case 6:
                this.title = base.GetGameNameByID(6.ToString());
                break;

            case 7:
                this.title = base.GetGameNameByID(7.ToString());
                break;

            case 8:
                this.title = base.GetGameNameByID(8.ToString());
                break;

            case 9:
                this.title = base.GetGameNameByID(9.ToString());
                break;

            case 10:
                this.title = base.GetGameNameByID(10.ToString());
                break;

            case 11:
                this.title = base.GetGameNameByID(11.ToString());
                break;

            case 12:
                this.title = base.GetGameNameByID(12.ToString());
                break;

            case 13:
                this.title = base.GetGameNameByID(13.ToString());
                break;

            case 14:
                this.title = base.GetGameNameByID(14.ToString());
                break;

            case 15:
                this.title = base.GetGameNameByID(15.ToString());
                break;

            case 0x10:
                this.title = base.GetGameNameByID(0x10.ToString());
                break;

            case 0x11:
                this.title = base.GetGameNameByID(0x11.ToString());
                break;

            case 0x12:
                this.title = base.GetGameNameByID(0x12.ToString());
                break;

            case 0x13:
                this.title = base.GetGameNameByID(0x13.ToString());
                break;

            case 20:
                this.title = base.GetGameNameByID(20.ToString());
                break;

            case 0x15:
                this.title = base.GetGameNameByID(0x15.ToString());
                break;

            case 100:
                this.title = base.GetGameNameByID(100.ToString());
                break;

            default:
                base.Response.End();
                break;
        }
    }
}

