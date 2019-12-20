namespace Agent.Web.Account
{
    using Agent.Web.WebBase;
    using LotterySystem.Common;
    using LotterySystem.Model;
    using System;
    using System.Data;

    public class child_list : MemberPageBase
    {
        protected DataTable childTable;
        protected string CurUrl = "";
        protected int dataCount;
        protected string[] FiledName = new string[0];
        protected string[] FiledValue = new string[0];
        protected string page = "1";
        protected int pageCount;
        protected int pageSize = 20;
        protected DataTable userDT;

        protected void Page_Load(object sender, EventArgs e)
        {
            string str = this.Session["user_name"].ToString();
            agent_userinfo_session model = this.Session[str + "lottery_session_user_info"] as agent_userinfo_session;
            if (((!model.get_u_type().Trim().Equals("zj") && !model.get_u_type().Trim().Equals("fgs")) && (!model.get_u_type().Trim().Equals("gd") && !model.get_u_type().Trim().Equals("zd"))) && !model.get_u_type().Trim().Equals("dl"))
            {
                base.Response.Redirect("../MessagePage.aspx?code=u100035&url=&issuccess=1&isback=0&isopen=1");
                base.Response.End();
            }
            if (this.Session["child_user_name"] != null)
            {
                base.Response.Redirect("../MessagePage.aspx?code=u100014&url=&issuccess=1&isback=0&isopen=1");
                base.Response.End();
            }
            base.Permission_Aspx_ZJ(model, "po_2_1");
            base.Permission_Aspx_DL(model, "po_6_1");
            base.GetLotteryList();
            this.page = LSRequest.qq("page");
            if (string.IsNullOrEmpty(this.page))
            {
                this.page = "1";
            }
            if (int.Parse(this.page) < 1)
            {
                this.page = "1";
            }
            if (FileCacheHelper.get_RedisStatOnline().Equals(1) || FileCacheHelper.get_RedisStatOnline().Equals(2))
            {
                this.childTable = CallBLL.cz_users_child_bll.GetChildList(str, Convert.ToInt32(this.page) - 1, this.pageSize, ref this.pageCount, ref this.dataCount, "redis");
            }
            else
            {
                this.childTable = CallBLL.cz_users_child_bll.GetChildList(str, Convert.ToInt32(this.page) - 1, this.pageSize, ref this.pageCount, ref this.dataCount);
            }
            this.CurUrl = string.Format("?page={0}", this.page);
        }
    }
}

