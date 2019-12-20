namespace User.Web
{
    using LotterySystem.Common;
    using LotterySystem.WebPageBase;
    using System;
    using User.Web.LoginUserControl.Login1;
    using User.Web.LoginUserControl.Login2;
    using User.Web.LoginUserControl.Login3;
    using User.Web.LoginUserControl.Login4;
    using User.Web.LoginUserControl.Login5;
    using User.Web.LoginUserControl.Login6;
    using User.Web.LoginUserControl.Login7;
    using User.Web.LoginUserControl.Login8;
    using User.Web.LoginUserControl.Login9;

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
        }
    }
}

