using LotterySystem.Model;
using System;
using System.Data;
using User.Web.WebBase;

public class m_Report_JeuWJS : MemberPageBase_Mobile
{
    protected string count_order = "0";
    protected string cssdisplay = "";
    protected int dataCount = 0;
    protected string[] FiledName = new string[0];
    protected string[] FiledValue = null;
    protected DataTable kcDataTable = null;
    protected int page = 1;
    protected int pageCount = 0;
    protected int pageSize = 10;
    protected string sltType = "";
    protected string sum_money = "0";
    protected cz_userinfo_session uModel;
    protected string userName = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        base.Response.Expires = 0;
        base.Response.CacheControl = "no-cache";
        base.Response.AddHeader("Pragma", "No-Cache");
        base.IsUserLoginByMobile();
        this.userName = this.Session["user_name"].ToString();
        this.uModel = base.GetUserModelInfo;
        if (!string.IsNullOrEmpty(base.q("page")))
        {
            this.page = Convert.ToInt32(base.q("page"));
        }
        this.FiledValue = new string[0];
        this.kcDataTable = CallBLL.cz_bet_kc_bll.GetBetByPageList(this.userName, this.page - 1, this.pageSize, ref this.pageCount, ref this.dataCount).Tables[0];
        if (this.dataCount == 0)
        {
            this.cssdisplay = "display:none;";
        }
        else
        {
            this.cssdisplay = "display:block;";
        }
        DataTable table = CallBLL.cz_bet_kc_bll.Phone_GetBetByUName(this.userName);
        if (table.Rows.Count > 0)
        {
            int num = 0;
            double num2 = 0.0;
            foreach (DataRow row in table.Rows)
            {
                if (row[2].ToString().Equals("0"))
                {
                    num += int.Parse(row[0].ToString());
                    num2 += double.Parse(row[1].ToString());
                }
            }
            this.count_order = num.ToString();
            this.sum_money = num2.ToString();
        }
        if (string.IsNullOrEmpty(this.sum_money))
        {
            this.sum_money = "0";
        }
    }
}

