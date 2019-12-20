namespace User.Web.OldWeb
{
    using System;
    using User.Web.WebBase;

    public class Left_six : MemberPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsOpenBigLottery(1))
            {
                base.Response.End();
            }
        }
    }
}

