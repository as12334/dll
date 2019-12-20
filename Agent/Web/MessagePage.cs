namespace Agent.Web
{
    using Agent.Web.WebBase;
    using LotterySystem.Common;
    using LotterySystem.Model;
    using LotterySystem.WebPageBase;
    using System;

    public class MessagePage : MemberPageBase
    {
        protected string cssClass = "";
        protected string isback = "1";
        protected string isSuccess = "";
        protected string msgCode = "";
        protected string msgText = "";
        protected string openwindow = "";
        protected string skin = "";
        protected string url = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            this.skin = (this.Session[this.Session["user_name"].ToString() + "lottery_session_user_info"] as agent_userinfo_session).get_u_skin();
            this.msgCode = LSRequest.qq("code");
            this.isSuccess = LSRequest.qq("issuccess");
            this.url = base.Server.UrlDecode(base.Request["url"]);
            this.isback = LSRequest.qq("isback");
            string str = LSRequest.qq("isopen");
            string str2 = LSRequest.qq("txt");
            if (!string.IsNullOrEmpty(str) && str.Equals("1"))
            {
                this.openwindow = " id=\"opw\"";
            }
            if (string.IsNullOrEmpty(this.isback))
            {
                this.isback = "1";
            }
            if (!string.IsNullOrEmpty(str2))
            {
                this.msgText = string.Format(PageBase.GetMessageByCache(this.msgCode, "MessageHint"), str2);
            }
            else
            {
                this.msgText = PageBase.GetMessageByCache(this.msgCode, "MessageHint");
            }
            if (this.isSuccess.Equals("0"))
            {
                this.cssClass = "right";
            }
            else if (this.isSuccess.Equals("1"))
            {
                this.cssClass = "wrong";
            }
            else
            {
                this.cssClass = "tip";
            }
        }
    }
}

