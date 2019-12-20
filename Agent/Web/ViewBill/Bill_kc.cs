namespace Agent.Web.ViewBill
{
    using Agent.Web.WebBase;
    using LotterySystem.Common;
    using LotterySystem.Model;
    using System;
    using System.Data;

    public class Bill_kc : MemberPageBase
    {
        protected string atz = "1";
        protected int dataCount;
        protected DataTable dataTable;
        protected string[] FiledName = new string[] { "phaseid", "oddsid", "playid", "lid", "atz" };
        protected string[] FiledValue;
        protected int lb_step;
        protected string lid = "";
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
            string str6 = this.uModel.get_u_type();
            if (str6 != null)
            {
                if (!(str6 == "zj"))
                {
                    if (str6 == "fgs")
                    {
                        this.lb_step = 4;
                    }
                    else if (str6 == "gd")
                    {
                        this.lb_step = 3;
                    }
                    else if (str6 == "zd")
                    {
                        this.lb_step = 2;
                    }
                    else if (str6 == "dl")
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
            this.atz = LSRequest.qq("atz");
            string str = "";
            string str2 = "";
            int num = 8;
            if (this.lid.Equals(num.ToString()))
            {
                str = Utils.GetPKBJL_NumTable(LSRequest.qq("playpage"), null);
                if (string.IsNullOrEmpty(str))
                {
                    base.Response.End();
                    return;
                }
                str2 = LSRequest.qq("playtype");
                if (!string.IsNullOrEmpty(str2))
                {
                    int num2 = 0;
                    if (str2 != num2.ToString())
                    {
                        int num3 = 1;
                        if (str2 != num3.ToString())
                        {
                            int num4 = 2;
                            if (str2 != num4.ToString())
                            {
                                base.Response.End();
                                return;
                            }
                        }
                    }
                }
            }
            if (string.IsNullOrEmpty(this.atz))
            {
                this.atz = "1";
            }
            string str4 = this.uModel.get_u_type();
            string str5 = this.uModel.get_u_name();
            if (this.uModel.get_u_type().Equals("fgs") && ((this.uModel.get_allow_view_report() == 1) && int.Parse(this.atz).Equals(2)))
            {
                str4 = "zj";
                str5 = this.uModel.get_zjname();
                this.lb_step = 5;
            }
            this.dataTable = CallBLL.cz_bet_kc_bll.GetBetViewBillPageList(str4, str5, this.playid, this.oddsid, int.Parse(this.phaseid), Convert.ToInt32(this.page) - 1, this.pageSize, ref this.pageCount, ref this.dataCount, int.Parse(this.lid), str, str2);
            this.FiledValue = new string[] { this.phaseid, this.oddsid, this.playid, this.lid, this.atz };
        }
    }
}

