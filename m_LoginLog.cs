using LotterySystem.Model;
using System;
using System.Data;
using User.Web.WebBase;

public class m_LoginLog : MemberPageBase_Mobile
{
    protected DataTable dtable = null;
    protected DataTable table_lastweek = null;
    protected DataTable table_week = null;
    protected string u_type = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        base.Response.Expires = 0;
        base.IsUserLoginByMobile();
        string str = this.Session["user_name"].ToString();
        cz_userinfo_session getUserModelInfo = base.GetUserModelInfo;
        this.u_type = getUserModelInfo.get_u_type().Trim();
        DataSet newList = CallBLL.cz_login_log_bll.GetNewList(str, 50);
        if ((newList != null) && (newList.Tables.Count > 0))
        {
            this.dtable = newList.Tables[0];
        }
    }
}

