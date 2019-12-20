using LotterySystem.Model;
using System;
using System.Data;
using User.Web.WebBase;

public class m_touzhu_1 : MemberPageBase_Mobile
{
    protected string caizhong = "";
    protected string data_str = "";
    protected string jiangqi = "";
    protected string jiangqi_str = "";
    protected string pk_kind = "";
    protected DataTable plDT = null;
    protected string r_url = "";
    protected int s_count = 0;
    protected int s_momey = 0;
    protected string txtMomey = "";
    protected cz_userinfo_session uModel = null;
    protected string wanfa = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        base.Response.Expires = 0;
        base.IsUserLoginByMobile();
        this.caizhong = base.qq("caizhong");
        this.wanfa = base.qq("wanfa");
        this.jiangqi = base.qq("jiangqi");
        this.txtMomey = base.qq("txtMomey");
        this.data_str = base.qq("data");
        if ((((this.caizhong == "") || (this.wanfa == "")) || ((this.jiangqi == "") || (this.data_str == ""))) || (this.txtMomey == ""))
        {
            base.Response.End();
        }
        this.r_url = "CQSC_" + this.wanfa + ".aspx?lottery_type=" + this.caizhong + "&player_type=" + this.wanfa;
        DataTable table = CallBLL.cz_phase_cqsc_bll.GetIsClosedByTime(int.Parse(this.jiangqi)).Tables[0];
        if ((table != null) && (table.Rows.Count > 0))
        {
            if (table.Rows[0]["is_closed"].ToString().Trim() == "1")
            {
                base.Response.Write("<script>alert('該獎期已經截止下注！');window.location=\"" + this.r_url + "\";</script>");
                base.Response.End();
            }
            else
            {
                this.jiangqi_str = table.Rows[0]["phase"].ToString();
            }
        }
        else
        {
            base.Response.End();
        }
        this.uModel = base.GetUserModelInfo;
        string str = this.Session["user_name"].ToString();
        DataTable table2 = CallBLL.cz_users_bll.GetUserRate(str, 2).Tables[0];
        if (table2 == null)
        {
            base.Response.End();
        }
        this.pk_kind = table2.Rows[0]["kc_kind"].ToString().Trim();
        string[] strArray = this.data_str.Split(new char[] { '|' });
        this.s_count = strArray.Length;
        this.s_momey = int.Parse(this.txtMomey) * strArray.Length;
        string str2 = "";
        foreach (string str3 in strArray)
        {
            string[] strArray2 = str3.Split(new char[] { ',' })[0].Split(new char[] { '_' });
            str2 = str2 + strArray2[3].ToString().Trim() + ",";
        }
        str2 = str2.Substring(0, str2.Length - 1);
        DataSet playOddsByID = CallBLL.cz_odds_cqsc_bll.GetPlayOddsByID(str2);
        this.plDT = playOddsByID.Tables[0];
        if ((this.plDT == null) || (this.plDT.Rows.Count == 0))
        {
            base.Response.End();
        }
    }
}

