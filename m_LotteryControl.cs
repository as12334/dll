using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using User.Web.WebBase;

public class m_LotteryControl : UserControl
{
    private string _lottery_type = "";
    private string _player_type = "";
    protected DataTable lotteryTable = null;
    protected string zodiacData = FileCacheHelper.get_YearLianArray();

    public string get_lm_group_set()
    {
        MemberPageBase parent = this.Parent as MemberPageBase;
        if (parent != null)
        {
            return parent.LmGroup();
        }
        return "";
    }

    protected string GetGameNameByID(string Lid)
    {
        MemberPageBase parent = this.Parent as MemberPageBase;
        return parent.GetGameNameByID(Lid);
    }

    public bool IsOpenLotteryForControl(int id)
    {
        DataRow[] source = this.lotteryTable.Select(string.Format(" id={0} ", id));
        return ((source != null) && (source.Count<DataRow>() > 0));
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        this.lotteryTable = (this.Parent as MemberPageBase).GetLotteryList();
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

    public string player_type
    {
        get
        {
            return this._player_type;
        }
        set
        {
            if (value == null)
            {
                this._player_type = "";
            }
            else
            {
                this._player_type = value;
            }
            base.Session["LotteryTypeByPhone"] = this.lottery_type;
        }
    }
}

