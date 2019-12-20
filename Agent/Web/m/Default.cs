namespace Agent.Web.m
{
    using LotterySystem.Common;
    using LotterySystem.WebPageBase;
    using System;

    public class Default : PageBase
    {
        protected string codeFlag = "false";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (int.Parse(FileCacheHelper.get_GetLockedPasswordCount()) == 0)
            {
                this.codeFlag = "true";
                this.Session["lottery_session_img_code_display"] = 1;
            }
            else
            {
                this.codeFlag = "false";
            }
            this.Session.Abandon();
        }
    }
}

