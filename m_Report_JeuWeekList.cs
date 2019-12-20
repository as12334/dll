using LotterySystem.Common;
using LotterySystem.Model;
using System;
using System.Data;
using System.Web;
using User.Web.WebBase;

public class m_Report_JeuWeekList : MemberPageBase_Mobile
{
    protected string count_order = "0";
    protected string cssdisplay = "";
    protected int dataCount = 0;
    protected DataTable dataTable = null;
    protected string dataTitle = "";
    protected string findDate = "";
    protected string lid = "";
    protected int page = 1;
    protected int pageCount = 0;
    protected int pageSize = 10;
    protected string sum_money = "0";
    protected string u_drawback = "";
    protected cz_userinfo_session uModel = null;
    protected string userName = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        base.Response.Buffer = true;
        base.Response.ExpiresAbsolute = DateTime.Now.AddSeconds(-1.0);
        base.Response.Expires = 0;
        base.Response.CacheControl = "no-cache";
        base.Response.AddHeader("Pragma", "No-Cache");
        base.IsUserLoginByMobile();
        this.findDate = LSRequest.qq("findDate");
        this.lid = LSRequest.qq("lid");
        this.dataTitle = "(快)注單明細 - 日期:" + this.findDate;
        if (!Utils.ValidLotteryID(this.lid, true))
        {
            base.Response.End();
        }
        this.userName = HttpContext.Current.Session["user_name"].ToString();
        string str2 = base.GetUserModelInfo.get_su_type().ToString().Trim();
        if (str2 != null)
        {
            if (!(str2 == "dl"))
            {
                if (str2 == "zd")
                {
                    this.u_drawback = "dl_drawback";
                }
                else if (str2 == "gd")
                {
                    this.u_drawback = "zd_drawback";
                }
                else if (str2 == "fgs")
                {
                    this.u_drawback = "gd_drawback";
                }
            }
            else
            {
                this.u_drawback = "hy_drawback";
            }
        }
        string str = "cz_betold_kc";
        if (DateTime.Now.AddHours((double) -int.Parse(base.get_BetTime_KC())).ToString("yyyy-MM-dd") == this.findDate)
        {
            str = "cz_bet_kc";
        }
        DateTime time2 = Convert.ToDateTime(this.findDate);
        DateTime now = DateTime.Now;
        now = time2.AddDays(1.0);
        if (str.Equals("cz_betold_kc"))
        {
            this.dataTable = CallBLL.cz_betold_kc_bll.GetReportByPageList(this.userName, this.page - 1, this.pageSize, ref this.pageCount, ref this.dataCount, this.findDate + " 07:00:00", now.ToString("yyyy-MM-dd") + " 07:00:00", this.lid).Tables[0];
        }
        else
        {
            this.dataTable = CallBLL.cz_bet_kc_bll.GetReportByPageList(this.userName, this.page - 1, this.pageSize, ref this.pageCount, ref this.dataCount, this.findDate + " 07:00:00", now.ToString("yyyy-MM-dd") + " 07:00:00", this.lid).Tables[0];
        }
        if (this.dataCount == 0)
        {
            this.cssdisplay = "display:none;";
        }
        else
        {
            this.cssdisplay = "display:block;";
        }
        DataTable table = CallBLL.cz_bet_kc_bll.Phone_GetBetByUserName(this.userName);
        if (table.Rows.Count > 0)
        {
            this.count_order = table.Rows[0][0].ToString();
            this.sum_money = table.Rows[0][1].ToString();
        }
        if (string.IsNullOrEmpty(this.sum_money))
        {
            this.sum_money = "0";
        }
    }
}

