using LotterySystem.Common;
using LotterySystem.Model;
using System;
using System.Data;
using User.Web.WebBase;

public class m_Report_JeuWeekListAjax : MemberPageBase_Mobile
{
    protected int dataCount = 0;
    protected DataTable dataTable = null;
    protected string[] FiledName = new string[0];
    protected string[] FiledValue = null;
    protected string findDate = "";
    protected string lid = "";
    protected int page = 1;
    protected int pageCount = 0;
    protected int pageSize = 10;
    protected string u_drawback = "";
    protected cz_userinfo_session uModel = null;
    protected string userName = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        base.Response.Expires = 0;
        base.IsUserLoginByMobileForAjax();
        this.userName = this.Session["user_name"].ToString();
        this.uModel = base.GetUserModelInfo;
        this.findDate = LSRequest.qq("findDate");
        this.lid = LSRequest.qq("lid");
        if (!Utils.ValidLotteryID(this.lid, true))
        {
            base.Response.End();
        }
        if (!string.IsNullOrEmpty(base.q("page")))
        {
            this.page = Convert.ToInt32(base.q("page"));
        }
        this.FiledValue = new string[0];
        string str2 = this.uModel.get_su_type().ToString().Trim();
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
        if (this.page > this.pageCount)
        {
            this.dataTable = null;
        }
    }
}

