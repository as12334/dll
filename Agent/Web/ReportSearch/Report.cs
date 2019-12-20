namespace Agent.Web.ReportSearch
{
    using Agent.Web.WebBase;
    using LotterySystem.Common;
    using LotterySystem.Model;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class Report : MemberPageBase
    {
        protected string beginTime = "";
        protected DateTime endMonth;
        protected string endTime = "";
        protected DateTime endWeek;
        protected string jsonLottery = "";
        protected string kc_list = "<option value='' selected=selected>全部</option>";
        protected DateTime lastendMonth;
        protected DateTime lastendWeek;
        protected DateTime laststartMonth;
        protected DateTime laststartWeek;
        protected DataTable lotteryDT;
        protected string lotteryId = "";
        protected string minDateStr = "";
        protected string minDateString = "";
        protected string perName_1 = "po";
        protected string perName_2 = "po";
        protected string six_list = "";
        protected string sixOpenPhase = "0";
        protected DateTime startMonth;
        protected DateTime startWeek;
        protected DateTime yesterday;

        protected void GetReportOpenDate()
        {
            string s = CallBLL.cz_system_set_six_bll.GetSystemSet(100).get_ev_18();
            string reportOpenDate = CallBLL.cz_system_set_kc_ex_bll.GetReportOpenDate();
            int num = 100;
            if (this.lotteryId.Equals(num.ToString()))
            {
                this.minDateStr = DateTime.Parse(s).ToString("yyyy-MM-dd");
            }
            else
            {
                this.minDateStr = DateTime.Parse(reportOpenDate).ToString("yyyy-MM-dd");
            }
            this.minDateString = string.Format("{0},{1}", s, reportOpenDate);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            agent_userinfo_session _session = this.Session[this.Session["user_name"] + "lottery_session_user_info"] as agent_userinfo_session;
            if (_session.get_u_type().Equals("zj") && (_session.get_users_child_session() != null))
            {
                this.perName_1 = "";
                this.perName_2 = "";
                if (_session.get_users_child_session().get_permissions_name().IndexOf("po_4_1") > -1)
                {
                    this.perName_1 = "po_4_1";
                }
                if (_session.get_users_child_session().get_permissions_name().IndexOf("po_4_2") > -1)
                {
                    this.perName_2 = "po_4_2";
                }
                if (string.IsNullOrEmpty(this.perName_1) && string.IsNullOrEmpty(this.perName_2))
                {
                    base.Response.Redirect("/MessagePage.aspx?code=u100057&url=&issuccess=1&isback=0");
                }
            }
            if (!_session.get_u_type().Equals("zj") && (_session.get_users_child_session() != null))
            {
                this.perName_1 = "";
                this.perName_2 = "";
                if (_session.get_users_child_session().get_permissions_name().IndexOf("po_7_1") > -1)
                {
                    this.perName_1 = "po_7_1";
                }
                if (_session.get_users_child_session().get_permissions_name().IndexOf("po_7_2") > -1)
                {
                    this.perName_2 = "po_7_2";
                }
                if (string.IsNullOrEmpty(this.perName_1) && string.IsNullOrEmpty(this.perName_2))
                {
                    base.Response.Redirect("/MessagePage.aspx?code=u100057&url=&issuccess=1&isback=0");
                }
            }
            this.lotteryId = LSRequest.qq("lid");
            this.lotteryDT = base.GetLotteryList();
            this.SetDate();
            this.beginTime = DateTime.Now.AddHours(-7.0).ToString("yyyy-MM-dd");
            this.endTime = DateTime.Now.AddHours(-7.0).ToString("yyyy-MM-dd");
            if (CallBLL.cz_phase_six_bll.IsOpenPhase())
            {
                this.sixOpenPhase = "1";
            }
            Dictionary<string, Dictionary<string, string>> dictionary = new Dictionary<string, Dictionary<string, string>>();
            DataRow[] rowArray = this.lotteryDT.Select(string.Format(" master_id={0} ", 1));
            DataRow[] rowArray2 = this.lotteryDT.Select(string.Format(" master_id={0} ", 2));
            if ((rowArray != null) && (rowArray.Length > 0))
            {
                string str = "";
                Dictionary<string, string> dictionary2 = new Dictionary<string, string>();
                foreach (DataRow row in rowArray)
                {
                    dictionary2.Add(row["id"].ToString(), row["lottery_name"].ToString());
                    str = row["name"].ToString();
                    this.six_list = this.six_list + string.Format("<option value='{0}' {1}>{2}</option>", row["id"].ToString(), "selected=selected", row["lottery_name"].ToString());
                }
                dictionary.Add(1.ToString() + "," + str, dictionary2);
            }
            if ((rowArray2 != null) && (rowArray2.Length > 0))
            {
                string str2 = "";
                Dictionary<string, string> dictionary3 = new Dictionary<string, string>();
                foreach (DataRow row2 in rowArray2)
                {
                    dictionary3.Add(row2["id"].ToString(), row2["lottery_name"].ToString());
                    str2 = row2["name"].ToString();
                    this.kc_list = this.kc_list + string.Format("<option value='{0}'>{1}</option>", row2["id"].ToString(), row2["lottery_name"].ToString());
                }
                dictionary.Add(2.ToString() + "," + str2, dictionary3);
            }
            this.jsonLottery = JsonHandle.ObjectToJson(dictionary);
            this.GetReportOpenDate();
        }

        protected void SetDate()
        {
            DateTime time = DateTime.Now.AddHours(-7.0);
            time.ToString("yyyy-MM-dd");
            time.ToString("yyyy-MM-dd");
            int num = Convert.ToInt32(time.DayOfWeek.ToString("d"));
            if (num == 0)
            {
                num = 7;
            }
            this.yesterday = time.AddDays(-1.0);
            this.startWeek = time.AddDays((double) (1 - num));
            this.endWeek = this.startWeek.AddDays(6.0);
            this.lastendWeek = this.startWeek.AddDays(-1.0);
            this.laststartWeek = this.lastendWeek.AddDays(-6.0);
            this.startMonth = time.AddDays((double) (1 - time.Day));
            this.endMonth = this.startMonth.AddMonths(1).AddDays(-1.0);
            this.lastendMonth = this.startMonth.AddDays(-1.0);
            this.laststartMonth = this.startMonth.AddMonths(-1);
        }
    }
}

