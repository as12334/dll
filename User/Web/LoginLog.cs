namespace User.Web
{
    using System;
    using System.Data;
    using System.Web;
    using User.Web.WebBase;

    public class LoginLog : MemberPageBase
    {
        protected DataTable DT = null;
        protected string u_type = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            this.u_type = "hy";
            string str = HttpContext.Current.Session["user_name"].ToString();
            DataSet newList = CallBLL.cz_login_log_bll.GetNewList(str, 50);
            if ((newList != null) && (newList.Tables.Count > 0))
            {
                this.DT = newList.Tables[0];
            }
        }
    }
}

