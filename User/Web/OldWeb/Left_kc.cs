namespace User.Web.OldWeb
{
    using System;
    using User.Web.WebBase;

    public class Left_kc : MemberPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsOpenBigLottery(2))
            {
                base.Response.End();
            }
        }
    }
}

