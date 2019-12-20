namespace Agent.Web
{
    using Agent.Web.LoginUserControl.Login1;
    using Agent.Web.LoginUserControl.Login2;
    using Agent.Web.LoginUserControl.Login3;
    using Agent.Web.LoginUserControl.Login4;
    using Agent.Web.LoginUserControl.Login5;
    using Agent.Web.LoginUserControl.Login6;
    using Agent.Web.LoginUserControl.Login7;
    using Agent.Web.LoginUserControl.Login8;
    using Agent.Web.LoginUserControl.Login9;
    using LotterySystem.Common;
    using LotterySystem.WebPageBase;
    using System;

    public class _Default : PageBase
    {
        protected string codeFlag = "false";
        protected LoginControl LoginControl1;
        protected LoginControl LoginControl2;
        protected LoginControl LoginControl3;
        protected LoginControl LoginControl4;
        protected LoginControl LoginControl5;
        protected LoginControl LoginControl6;
        protected LoginControl LoginControl7;
        protected LoginControl LoginControl8;
        protected LoginControl LoginControl9;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (int.Parse(FileCacheHelper.get_GetLockedPasswordCount()) == 0)
            {
                this.codeFlag = "true";
            }
            else
            {
                this.codeFlag = "false";
            }
            this.Session.Abandon();
            LSRequest.RemoveReportCookies();
        }
    }
}

