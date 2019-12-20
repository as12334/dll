namespace Agent.Web
{
    using Agent.Web.WebBase;
    using LotterySystem.Common;
    using LotterySystem.Model;
    using LotterySystem.WebPageBase;
    using System;
    using System.Configuration;
    using System.Data;

    public class Index : MemberPageBase
    {
        protected string ajaxErrorLogSwitch = "";
        protected string browserCode = "";
        protected bool isChildSytem;
        protected DataTable lotteryDT;
        protected string masterids = "";
        protected string navString = "";
        protected string negative_sale = "";
        protected string online_type = "";
        protected string saleuser = "";
        protected string skin = "";
        protected string sysName = PageBase.get_GetLottorySystemName();
        protected agent_userinfo_session uModel;
        protected string url = "";
        protected string userName = "";
        protected string zodiacData = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            this.ajaxErrorLogSwitch = FileCacheHelper.get_AjaxErrorLogSwitch();
            string str = ConfigurationManager.AppSettings["CloseIndexRefresh"];
            if ((str != "true") && PageBase.IsNeedPopBrower())
            {
                this.Session.Abandon();
                base.Response.Write("<script>top.location.href='/'</script>");
                base.Response.End();
            }
            this.lotteryDT = base.GetLotteryList();
            this.navString = base.GetNav();
            DataRow row = this.lotteryDT.Rows[0];
            this.zodiacData = base.get_YearLianArray();
            this.uModel = this.Session[this.Session["user_name"] + "lottery_session_user_info"] as agent_userinfo_session;
            this.skin = this.uModel.get_u_skin();
            this.online_type = this.uModel.get_u_type().Trim();
            if (CallBLL.cz_admin_subsystem_bll.GetModel().get_flag().Equals(1))
            {
                this.isChildSytem = true;
            }
            this.saleuser = this.saleuser + "{";
            this.saleuser = this.saleuser + "\"saleuser\": {";
            if (this.uModel.get_u_type().Trim().Equals("zj"))
            {
                DataTable saleSetUser = CallBLL.cz_saleset_six_bll.GetSaleSetUser();
                if (saleSetUser != null)
                {
                    for (int i = 0; i < saleSetUser.Rows.Count; i++)
                    {
                        if (i == 0)
                        {
                            this.saleuser = this.saleuser + string.Format("\"{0}\": {1}", saleSetUser.Rows[i]["u_name"], saleSetUser.Rows[i]["flag"]);
                        }
                        else
                        {
                            this.saleuser = this.saleuser + string.Format(",\"{0}\": {1}", saleSetUser.Rows[i]["u_name"], saleSetUser.Rows[i]["flag"]);
                        }
                    }
                }
                this.negative_sale = this.uModel.get_negative_sale();
            }
            this.saleuser = this.saleuser + "}}";
            int num3 = 1;
            if (this.Session["user_state"].ToString().Equals(num3.ToString()))
            {
                this.url = string.Format("/Report.aspx", new object[0]);
            }
            int num4 = 1;
            if (row["master_id"].ToString().Equals(num4.ToString()))
            {
                this.url = "";
            }
            this.masterids = base.GetLotteryMasterID(this.lotteryDT);
            if (str != "true")
            {
                this.browserCode = Utils.Number(4);
                PageBase.SetBrowerFlag(this.browserCode);
            }
        }
    }
}

