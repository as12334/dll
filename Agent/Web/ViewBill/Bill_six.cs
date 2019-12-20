namespace Agent.Web.ViewBill
{
    using Agent.Web.WebBase;
    using LotterySystem.Common;
    using LotterySystem.Model;
    using System;
    using System.Data;

    public class Bill_six : MemberPageBase
    {
        protected string atz = "1";
        protected int dataCount;
        protected DataTable dataTable;
        protected string[] FiledName = new string[] { "phaseid", "oddsid", "playid", "number", "isab", "atz" };
        protected string[] FiledValue;
        protected string isab = "";
        protected int lb_step;
        protected string lid = "";
        protected string number = "";
        protected string oddsid = "";
        protected string oddsidab = "";
        protected string page = "1";
        protected int pageCount;
        protected int pageSize = 10;
        protected string phaseid = "";
        protected string playid = "";
        protected agent_userinfo_session uModel;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.uModel = this.Session[this.Session["user_name"] + "lottery_session_user_info"] as agent_userinfo_session;
            base.Permission_Aspx_ZJ(this.uModel, "po_1_1");
            base.Permission_Aspx_DL(this.uModel, "po_5_1");
            string str3 = this.uModel.get_u_type();
            if (str3 != null)
            {
                if (!(str3 == "zj"))
                {
                    if (str3 == "fgs")
                    {
                        this.lb_step = 4;
                    }
                    else if (str3 == "gd")
                    {
                        this.lb_step = 3;
                    }
                    else if (str3 == "zd")
                    {
                        this.lb_step = 2;
                    }
                    else if (str3 == "dl")
                    {
                        this.lb_step = 1;
                    }
                }
                else
                {
                    this.lb_step = 5;
                }
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
            this.phaseid = LSRequest.qq("phaseid");
            this.oddsid = LSRequest.qq("oddsid");
            this.playid = LSRequest.qq("playid");
            this.lid = LSRequest.qq("lid");
            this.number = LSRequest.qq("number");
            this.isab = LSRequest.qq("isab");
            this.atz = LSRequest.qq("atz");
            if (string.IsNullOrEmpty(this.atz))
            {
                this.atz = "1";
            }
            if (!string.IsNullOrEmpty(this.number) && (int.Parse(this.number) < 10))
            {
                this.number = "0" + int.Parse(this.number);
            }
            string str = this.uModel.get_u_type();
            string str2 = this.uModel.get_u_name();
            if (this.uModel.get_u_type().Equals("fgs") && ((this.uModel.get_allow_view_report() == 1) && int.Parse(this.atz).Equals(2)))
            {
                str = "zj";
                str2 = this.uModel.get_zjname();
                this.lb_step = 5;
            }
            this.dataTable = CallBLL.cz_bet_six_bll.GetBetViewBillPageList(str, str2, this.playid, this.oddsid, int.Parse(this.phaseid), Convert.ToInt32(this.page) - 1, this.pageSize, ref this.pageCount, ref this.dataCount, this.number, this.isab);
            this.FiledValue = new string[] { this.phaseid, this.oddsid, this.playid, this.number, this.isab, this.atz };
        }
    }
}

