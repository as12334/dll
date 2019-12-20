namespace Agent.Web.ReportSearch
{
    using Agent.Web.WebBase;
    using LotterySystem.Common;
    using LotterySystem.Model;
    using System;
    using System.Data;
    using System.Linq;

    public class LM_RankShow_six : MemberPageBase
    {
        protected string atz = "1";
        protected int dataCount;
        protected string[] FiledName = new string[] { "items", "ispay", "atz", "ow", "islike", "pk", "oddsid" };
        protected string[] FiledValue;
        protected string groupString = "";
        protected bool isDoubleOdds;
        protected string isLike = "";
        protected string isPayment = "";
        protected string item_num = "";
        protected string kind = "";
        protected string masterId = 1.ToString();
        protected string oddsid = "";
        protected string OutWindow = "0";
        protected string page = "1";
        protected int pageCount;
        protected int pageSize = 20;
        protected DataTable table;
        protected agent_userinfo_session uModel;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.uModel = this.Session[this.Session["user_name"] + "lottery_session_user_info"] as agent_userinfo_session;
            string str = base.Request.UrlReferrer.LocalPath.ToLower();
            if (this.uModel.get_u_type().Equals("zj"))
            {
                if (str.IndexOf("billsearchlist") > -1)
                {
                    base.Permission_Aspx_ZJ(this.uModel, "po_1_1");
                }
                if (str.IndexOf("bill_six") > -1)
                {
                    base.Permission_Aspx_ZJ(this.uModel, "po_1_1");
                }
                if (str.IndexOf("newbet_six") > -1)
                {
                    base.Permission_Aspx_ZJ(this.uModel, "po_3_5");
                }
                if (str.IndexOf("reportdetail_b_six") > -1)
                {
                    base.Permission_Aspx_ZJ(this.uModel, "po_4_1");
                }
                if (str.IndexOf("reportdetail_t_six") > -1)
                {
                    base.Permission_Aspx_ZJ(this.uModel, "po_4_2");
                }
            }
            else
            {
                if ((str.IndexOf("billsearchlist") > -1) || (str.IndexOf("newbet_six") > -1))
                {
                    base.Response.Redirect("/MessagePage.aspx?code=u100057&url=&issuccess=1&isback=0&isopen=1");
                    base.Response.End();
                }
                if (str.IndexOf("bill_six") > -1)
                {
                    base.Permission_Aspx_DL(this.uModel, "po_5_1");
                }
                if (str.IndexOf("reportdetail_b_six") > -1)
                {
                    base.Permission_Aspx_ZJ(this.uModel, "po_7_1");
                }
                if (str.IndexOf("reportdetail_t_six") > -1)
                {
                    base.Permission_Aspx_ZJ(this.uModel, "po_7_2");
                }
            }
            this.kind = LSRequest.qq("pk");
            this.isPayment = LSRequest.qq("ispay");
            this.item_num = LSRequest.qq("items");
            this.OutWindow = LSRequest.qq("ow");
            this.atz = LSRequest.qq("atz");
            this.oddsid = LSRequest.qq("oddsid");
            string str2 = "0";
            if (string.IsNullOrEmpty(this.atz))
            {
                this.atz = "1";
            }
            else if (this.atz.Equals("2"))
            {
                this.atz = "1";
                str2 = "1";
            }
            this.isLike = LSRequest.qq("islike");
            if (string.IsNullOrEmpty(this.isLike))
            {
                this.isLike = "0";
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
            string str3 = this.uModel.get_zjname();
            string str4 = this.uModel.get_u_type();
            int num = CallBLL.cz_phase_six_bll.GetCurrentPhase().get_p_id();
            this.table = CallBLL.cz_bet_six_bll.GetBetLMRankShow(this.item_num, this.isLike, this.oddsid, Convert.ToInt32(this.page) - 1, this.pageSize, ref this.pageCount, ref this.dataCount, str4, this.uModel.get_u_name(), int.Parse(this.atz), num, this.kind, str2, str3);
            this.FiledValue = new string[] { this.item_num, this.isPayment, this.atz, this.OutWindow, this.isLike, this.kind, this.oddsid };
        }

        private string px(string p_str)
        {
            string[] array = p_str.Split(new char[] { ',' });
            Array.Sort<string>(array);
            return string.Join(",", array.ToArray<string>());
        }
    }
}

