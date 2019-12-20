using LotterySystem.Model;
using System;
using System.Data;
using System.Linq;
using System.Web;
using User.Web.WebBase;

public class m_show_ad : MemberPageBase_Mobile
{
    protected string adContent = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        base.Response.Expires = 0;
        base.IsUserLoginByMobile();
        int num = base.get_current_master_id();
        string str = num.ToString();
        if (num == 0)
        {
            str = "0,1,2";
        }
        else if (num == 1)
        {
            str = "0,1";
        }
        else
        {
            str = "0,2";
        }
        string str2 = HttpContext.Current.Session["user_name"].ToString();
        cz_userinfo_session getUserModelInfo = base.GetUserModelInfo;
        DataTable table = null;
        table = base.get_ad_cache(str);
        if (table != null)
        {
            DataRow[] source = base.get_ad_select(ref table, getUserModelInfo.get_u_type(), 1);
            if ((source.Count<DataRow>() > 0) && (source[0]["allow_show"].ToString() == "0"))
            {
                this.adContent = source[0]["ad_content"].ToString();
            }
        }
        if (this.adContent == "")
        {
            base.Response.Redirect("Main.aspx");
        }
    }
}

