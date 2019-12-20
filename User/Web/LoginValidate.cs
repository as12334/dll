namespace User.Web
{
    using System;
    using System.Data;
    using System.Linq;
    using System.Web;
    using User.Web.WebBase;

    public class LoginValidate : MemberPageBase
    {
        protected string adContent = "";
        protected string sysName = PageBase.get_GetLottorySystemName();

        protected void Page_Load(object sender, EventArgs e)
        {
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
            DataTable table = null;
            table = base.get_ad_cache(str);
            if (table != null)
            {
                DataRow[] source = base.get_ad_select(ref table, base.GetUserModelInfo.get_u_type(), 1);
                if ((source.Count<DataRow>() > 0) && (source[0]["allow_show"].ToString() == "0"))
                {
                    this.adContent = source[0]["ad_content"].ToString();
                }
            }
        }
    }
}

