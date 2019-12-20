namespace Agent.Web
{
    using Agent.Web.WebBase;
    using LotterySystem.Common;
    using System;

    public class LogAjaxException : MemberPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string str = "";
            string str2 = this.Session["user_name"].ToString();
            object obj1 = this.Session[str2 + "lottery_session_user_info"];
            if (this.Session["child_user_name"] != null)
            {
                str = str2 + "-" + this.Session["child_user_name"].ToString();
            }
            else
            {
                str = str2;
            }
            LSRequest.qq("url");
            string str3 = LSRequest.qq("data").ToString().Replace("&quot；", "\"");
            string str4 = LSRequest.qq("action");
            string str5 = LSRequest.qq("error");
            FileCacheHelper.LogAjaxError("AjaxParseException =>" + str + "|" + str4 + "|" + str5 + "|" + str3, "agent");
        }
    }
}

