using LotterySystem.Model;
using System;
using System.Data;
using User.Web.WebBase;

public class m_Report_JeuWJSAjaxSix : MemberPageBase_Mobile
{
    protected int dataCount = 0;
    protected string[] FiledName = new string[0];
    protected string[] FiledValue = null;
    protected int page = 1;
    protected int pageCount = 0;
    protected int pageSize = 10;
    protected DataTable sixDataTable = null;
    protected cz_userinfo_session uModel;
    protected string userName = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        base.Response.Expires = 0;
        base.IsUserLoginByMobileForAjax();
        this.userName = this.Session["user_name"].ToString();
        this.uModel = base.GetUserModelInfo;
        if (!string.IsNullOrEmpty(base.q("page")))
        {
            this.page = Convert.ToInt32(base.q("page"));
        }
        this.FiledValue = new string[0];
        this.sixDataTable = CallBLL.cz_bet_six_bll.GetBetByPageList(this.userName, this.page - 1, this.pageSize, ref this.pageCount, ref this.dataCount).Tables[0];
        if (this.page > this.pageCount)
        {
            this.sixDataTable = null;
        }
    }
}

