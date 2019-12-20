namespace Agent.Web.Account
{
    using Agent.Web.WebBase;
    using LotterySystem.Common;
    using LotterySystem.Model;
    using System;
    using System.Data;

    public class credit_flow_list : MemberPageBase
    {
        protected bool bol_masterid;
        protected int dataCount;
        protected DataTable dataTable;
        protected cz_users edit_model;
        protected string[] FiledName = new string[] { "uid", "mlid", "flag" };
        protected string[] FiledValue = new string[0];
        protected string flag = "";
        protected string flagDS = "";
        protected DataTable lotteryDT;
        protected string mlid = "";
        protected string page = "1";
        protected int pageCount;
        protected int pageSize = 20;
        protected string uid = "";
        protected agent_userinfo_session userModel;

        private void InitData()
        {
            this.dataTable = CallBLL.cz_credit_flow_bll.GetCreditListByUName(this.uid, int.Parse(this.mlid), int.Parse(this.page) - 1, this.pageSize, ref this.pageCount, ref this.dataCount, this.flagDS);
            this.FiledValue = new string[] { this.uid, this.mlid, this.flag };
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string str = this.Session["user_name"].ToString();
            this.userModel = this.Session[str + "lottery_session_user_info"] as agent_userinfo_session;
            this.lotteryDT = base.GetLotteryList();
            if (base.GetLotteryMasterID(this.lotteryDT).Split(new char[] { ',' }).Length > 1)
            {
                this.bol_masterid = true;
            }
            this.uid = LSRequest.qq("uid");
            this.mlid = LSRequest.qq("mlid");
            this.flag = LSRequest.qq("flag");
            this.flagDS = this.flag;
            if (this.flag.Equals("0"))
            {
                this.flagDS = "0,1";
            }
            else if (this.flag.Equals("2"))
            {
                this.flagDS = "2,3";
            }
            else if (this.flag.Equals("4"))
            {
                this.flagDS = "4";
            }
            else
            {
                this.flagDS = "";
            }
            this.edit_model = CallBLL.cz_users_bll.GetUserInfoByUID(this.uid);
            if (!this.edit_model.get_kc_iscash().Equals(int.Parse("1")))
            {
                base.Response.End();
            }
            base.Permission_Aspx_ZJ(this.userModel, "po_2_1");
            base.Permission_Aspx_DL(this.userModel, "po_6_1");
            if (!base.IsUpperLowerLevels(this.edit_model.get_u_name(), this.userModel.get_u_type(), this.userModel.get_u_name()))
            {
                base.Response.Redirect("/MessagePage.aspx?code=u100057&url=&issuccess=1&isback=0&isopen=1");
                base.Response.End();
            }
            this.page = LSRequest.qq("page");
            if (string.IsNullOrEmpty(this.page))
            {
                this.page = "1";
            }
            else if (int.Parse(this.page) < 1)
            {
                this.page = "1";
            }
            this.InitData();
        }
    }
}

