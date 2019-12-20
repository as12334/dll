namespace Agent.Web
{
    using Agent.Web.WebBase;
    using LotterySystem.Common;
    using LotterySystem.Model;
    using System;
    using System.Collections;
    using System.Data;

    public class ViewAutoSaleLog : MemberPageBase
    {
        protected string cloneName = "";
        protected int dataCount;
        protected DataTable dataTable;
        protected string[] FiledName = new string[] { "uid", "lid" };
        protected string[] FiledValue = new string[0];
        protected bool isAll = true;
        protected bool isChild;
        protected bool isCloneUser;
        protected DataTable lotteryDT;
        protected string lotteryId = "";
        protected string page = "1";
        protected int pageCount;
        protected int pageSize = 20;
        protected string u_id = "";
        protected string u_name = "";
        protected string u_type = "";
        protected string url = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            string str = this.Session["user_name"].ToString();
            agent_userinfo_session model = this.Session[str + "lottery_session_user_info"] as agent_userinfo_session;
            base.Permission_Aspx_DL(model, "po_5_2");
            this.u_type = model.get_u_type().Trim();
            if (this.Session["child_user_name"] != null)
            {
                this.isCloneUser = true;
                this.cloneName = this.Session["child_user_name"].ToString();
            }
            if (model.get_u_type().Trim().Equals("zj"))
            {
                base.Response.Redirect("../MessagePage.aspx?code=u100014&url=&issuccess=1&isback=0");
            }
            this.u_id = LSRequest.qq("uid");
            if (string.IsNullOrEmpty(this.u_id))
            {
                this.u_id = model.get_u_id();
            }
            if (string.IsNullOrEmpty(this.u_id))
            {
                base.Response.End();
            }
            this.u_name = CallBLL.cz_users_bll.GetUserNameByUid(this.u_id, ref this.isChild);
            if (string.IsNullOrEmpty(this.u_name))
            {
                base.Response.Redirect("/MessagePage.aspx?code=u100014&url=&issuccess=1&isback=0");
                base.Response.End();
            }
            if ((this.u_id != model.get_u_id()) && !base.IsUpperLowerLevels(this.u_name, model.get_u_type(), model.get_u_name()))
            {
                base.Response.Redirect("/MessagePage.aspx?code=u100014&url=&issuccess=1&isback=0");
                base.Response.End();
            }
            this.lotteryId = LSRequest.qq("lid");
            if (string.IsNullOrEmpty(this.lotteryId))
            {
                this.lotteryId = "";
            }
            this.lotteryDT = base.GetLotteryList();
            base.IsLotteryExistForSysLog(this.lotteryId, "u100032", "1", "");
            string lotteryId = "";
            ArrayList list = new ArrayList();
            if (this.lotteryId == "")
            {
                foreach (DataRow row in this.lotteryDT.Rows)
                {
                    list.Add(row["id"].ToString());
                }
                lotteryId = string.Join(",", list.ToArray());
            }
            else
            {
                lotteryId = this.lotteryId;
            }
            this.page = LSRequest.qq("page");
            if (string.IsNullOrEmpty(this.page))
            {
                this.page = "1";
            }
            if (int.Parse(this.page) < 1)
            {
                this.page = "1";
            }
            this.dataTable = CallBLL.cz_autosale_log_bll.get_log_table(Convert.ToInt32(this.page) - 1, this.pageSize, ref this.pageCount, ref this.dataCount, this.u_name.Trim(), lotteryId, ref this.isAll);
            this.FiledValue = new string[] { this.u_id, this.lotteryId };
        }
    }
}

