namespace User.Web
{
    using LotterySystem.Common;
    using System;
    using System.Data;
    using User.Web.WebBase;

    public class AdShow : MemberPageBase
    {
        protected int dataCount = 0;
        protected DataTable DT = null;
        protected string[] FiledName = new string[0];
        protected string[] FiledValue = null;
        protected string page = "1";
        protected int pageCount = 0;
        protected int pageSize = 5;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.page = LSRequest.qq("page");
            if (string.IsNullOrEmpty(this.page))
            {
                this.page = "1";
            }
            int num = base.get_current_master_id();
            DataSet set = CallBLL.cz_ad_bll.GetAdByPage("hy", num, Convert.ToInt32(this.page) - 1, this.pageSize, ref this.pageCount, ref this.dataCount);
            if (((set != null) && (set.Tables.Count > 0)) && (set.Tables[0].Rows.Count > 0))
            {
                this.DT = set.Tables[0];
            }
        }
    }
}

