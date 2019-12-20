namespace User.Web.OldWeb
{
    using LotterySystem.Common;
    using System;
    using System.Data;
    using User.Web.WebBase;

    public class Rule : MemberPageBase
    {
        protected DataTable lotteryDT = null;
        protected string lotteryID = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            this.lotteryID = LSRequest.qq("id");
            this.lotteryDT = base.GetLotteryList();
        }
    }
}

