namespace User.Web
{
    using LotterySystem.Common;
    using System;
    using User.Web.WebBase;

    public class LogAjaxException : MemberPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string str = "";
            str = this.Session["user_name"].ToString();
            string str3 = LSRequest.qq("url");
            string str4 = LSRequest.qq("data").ToString().Replace("&quot；", "\"");
            string str5 = LSRequest.qq("action");
            string str6 = LSRequest.qq("error");
            FileCacheHelper.LogAjaxError("AjaxParseException =>" + str + "|" + str5 + "|" + str6 + "|" + str4, "user");
        }
    }
}

