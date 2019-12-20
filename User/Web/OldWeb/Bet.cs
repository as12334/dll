namespace User.Web.OldWeb
{
    using LotterySystem.Common;
    using System;
    using System.Data;
    using System.Web;
    using User.Web.WebBase;

    public class Bet : MemberPageBase
    {
        protected string clsOff = "";
        protected string clsOn = "";
        protected int dataCount = 0;
        protected string[] FiledName = new string[] { "masterId" };
        protected string[] FiledValue = null;
        protected string kcCls = "";
        protected DataTable kcDataTable = null;
        protected string kcStyle = "";
        protected string masterId = "";
        protected int page = 1;
        protected int pageCount = 0;
        protected int pageSize = 10;
        protected string sixCls = "";
        protected DataTable sixDataTable = null;
        protected string sixStyle = "";
        protected string userName = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            int num;
            this.masterId = base.q("masterId");
            if (string.IsNullOrEmpty(this.masterId))
            {
                num = 100;
                if (LSRequest.qq("id").Equals(num.ToString()))
                {
                    num = 1;
                    this.masterId = num.ToString();
                }
                else
                {
                    this.masterId = 2.ToString();
                }
            }
            if (!string.IsNullOrEmpty(base.q("page")))
            {
                this.page = Convert.ToInt32(base.q("page"));
            }
            this.FiledValue = new string[] { this.masterId };
            this.userName = HttpContext.Current.Session["user_name"].ToString();
            num = 1;
            if (this.masterId.Equals(num.ToString()))
            {
                this.sixDataTable = CallBLL.cz_bet_six_bll.GetBetByPageList(this.userName, this.page - 1, this.pageSize, ref this.pageCount, ref this.dataCount).Tables[0];
            }
            else
            {
                this.kcDataTable = CallBLL.cz_bet_kc_bll.GetBetByPageList(this.userName, this.page - 1, this.pageSize, ref this.pageCount, ref this.dataCount).Tables[0];
            }
        }
    }
}

