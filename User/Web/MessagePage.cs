namespace User.Web
{
    using LotterySystem.Common;
    using LotterySystem.WebPageBase;
    using System;
    using User.Web.WebBase;

    public class MessagePage : MemberPageBase
    {
        protected string cssClass = "";
        protected string isback = "1";
        protected string isSuccess = "";
        protected string msgCode = "";
        protected string msgText = "";
        protected string url = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            this.msgCode = LSRequest.qq("code");
            this.isSuccess = LSRequest.qq("issuccess");
            this.url = base.Server.UrlDecode(base.Request["url"]);
            this.isback = LSRequest.qq("isback");
            if (string.IsNullOrEmpty(this.isback))
            {
                this.isback = "1";
            }
            this.msgText = PageBase.GetMessageByCache(this.msgCode, "MessageHint");
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

