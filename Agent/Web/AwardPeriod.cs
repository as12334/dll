namespace Agent.Web
{
    using Agent.Web.WebBase;
    using LotterySystem.Common;
    using LotterySystem.Model;
    using System;
    using System.Data;

    public class AwardPeriod : MemberPageBase
    {
        protected string beginTime = "";
        protected int dataCount;
        protected DataTable dataTable;
        protected DateTime endMonth;
        protected string endTime = "";
        protected DateTime endWeek;
        protected string[] FiledName = new string[] { "lid", "begintime", "endtime", "issearch", "maxpid", "tabletype" };
        protected string[] FiledValue = new string[0];
        private string issearch = "0";
        protected DateTime lastendMonth;
        protected DateTime lastendWeek;
        protected DateTime laststartMonth;
        protected DateTime laststartWeek;
        protected DataTable lotteryDT;
        protected string lotteryId = "";
        protected string lottrty_kc = "";
        protected string lottrty_six = "";
        private string maxpid = "";
        protected string numTable = "";
        protected string page = "1";
        protected int pageCount;
        protected int pageSize = 20;
        private string search = "";
        protected string SixMaxPID = "";
        protected string skin = "";
        protected DateTime startMonth;
        protected DateTime startWeek;
        protected string tabState_kc = "";
        protected string tabState_six = "";
        protected string url = "";
        protected DateTime yesterday;

        protected void Page_Load(object sender, EventArgs e)
        {
            string str = this.Session["user_name"].ToString();
            agent_userinfo_session model = this.Session[str + "lottery_session_user_info"] as agent_userinfo_session;
            this.skin = model.get_u_skin();
            if (!model.get_u_type().Equals("zj".ToString()))
            {
                base.Response.Redirect("/MessagePage.aspx?code=u100057&url=&issuccess=1&isback=0");
                base.Response.End();
            }
            base.Permission_Aspx_ZJ(model, "po_3_1");
            this.lotteryId = LSRequest.qq("lid");
            this.maxpid = LSRequest.qq("maxpid");
            this.numTable = base.q("tabletype");
            if (string.IsNullOrEmpty(this.numTable))
            {
                string cookie = CookieHelper.GetCookie("PKBJL_TableType_Record_Current_Agent");
                if (string.IsNullOrEmpty(cookie))
                {
                    cookie = "p1";
                }
                this.numTable = Utils.GetPKBJL_NumTable(cookie);
            }
            this.lotteryDT = base.GetLotteryList();
            base.IsLotteryExist(this.lotteryId, "u100032", "1", "");
            this.page = LSRequest.qq("page");
            if (string.IsNullOrEmpty(this.page))
            {
                this.page = "1";
            }
            if (int.Parse(this.page) < 1)
            {
                this.page = "1";
            }
            if (string.IsNullOrEmpty(LSRequest.qq("issearch")))
            {
                this.issearch = "0";
            }
            else
            {
                this.issearch = LSRequest.qq("issearch");
            }
            this.beginTime = LSRequest.qq("begintime");
            this.endTime = LSRequest.qq("endtime");
            this.search = LSRequest.qq("searchHidden");
            if (this.search.Equals("search"))
            {
                this.issearch = "1";
                this.maxpid = "";
                this.lotteryId = LSRequest.qq("lid");
                this.beginTime = LSRequest.qq("txtbegintime");
                this.endTime = LSRequest.qq("txtendtime");
                this.url = string.Format("lid={0}&begintime={1}&endtime={2}&issearch={3}&tabletype={4}", new object[] { this.lotteryId, this.beginTime, this.endTime, this.issearch, this.maxpid, this.numTable });
                string url = string.Format("/LotteryPeriod/AwardPeriod.aspx?{0}", this.url);
                base.Response.Write(base.LocationHref(url));
                base.Response.End();
            }
            this.SetDate();
            switch (Convert.ToInt32(this.lotteryId))
            {
                case 0:
                {
                    DataSet set2 = CallBLL.cz_phase_kl10_bll.GetPhaseByPage(Convert.ToInt32(this.page) - 1, this.pageSize, ref this.pageCount, ref this.dataCount, this.beginTime, this.endTime, this.issearch, this.maxpid);
                    if (((set2 != null) && (set2.Tables.Count > 0)) && (set2.Tables[0].Rows.Count > 0))
                    {
                        this.dataTable = set2.Tables[0];
                        if (string.IsNullOrEmpty(this.maxpid))
                        {
                            this.maxpid = this.dataTable.Rows[0]["p_id"].ToString();
                        }
                    }
                    this.FiledValue = new string[] { this.lotteryId, this.beginTime, this.endTime, this.issearch, this.maxpid, this.numTable };
                    return;
                }
                case 1:
                {
                    DataSet set3 = CallBLL.cz_phase_cqsc_bll.GetPhaseByPage(Convert.ToInt32(this.page) - 1, this.pageSize, ref this.pageCount, ref this.dataCount, this.beginTime, this.endTime, this.issearch, this.maxpid);
                    if (((set3 != null) && (set3.Tables.Count > 0)) && (set3.Tables[0].Rows.Count > 0))
                    {
                        this.dataTable = set3.Tables[0];
                        if (string.IsNullOrEmpty(this.maxpid))
                        {
                            this.maxpid = this.dataTable.Rows[0]["p_id"].ToString();
                        }
                    }
                    this.FiledValue = new string[] { this.lotteryId, this.beginTime, this.endTime, this.issearch, this.maxpid, this.numTable };
                    return;
                }
                case 2:
                {
                    DataSet set4 = CallBLL.cz_phase_pk10_bll.GetPhaseByPage(Convert.ToInt32(this.page) - 1, this.pageSize, ref this.pageCount, ref this.dataCount, this.beginTime, this.endTime, this.issearch, this.maxpid);
                    if (((set4 != null) && (set4.Tables.Count > 0)) && (set4.Tables[0].Rows.Count > 0))
                    {
                        this.dataTable = set4.Tables[0];
                        if (string.IsNullOrEmpty(this.maxpid))
                        {
                            this.maxpid = this.dataTable.Rows[0]["p_id"].ToString();
                        }
                    }
                    this.FiledValue = new string[] { this.lotteryId, this.beginTime, this.endTime, this.issearch, this.maxpid, this.numTable };
                    return;
                }
                case 3:
                {
                    DataSet set5 = CallBLL.cz_phase_xync_bll.GetPhaseByPage(Convert.ToInt32(this.page) - 1, this.pageSize, ref this.pageCount, ref this.dataCount, this.beginTime, this.endTime, this.issearch, this.maxpid);
                    if (((set5 != null) && (set5.Tables.Count > 0)) && (set5.Tables[0].Rows.Count > 0))
                    {
                        this.dataTable = set5.Tables[0];
                        if (string.IsNullOrEmpty(this.maxpid))
                        {
                            this.maxpid = this.dataTable.Rows[0]["p_id"].ToString();
                        }
                    }
                    this.FiledValue = new string[] { this.lotteryId, this.beginTime, this.endTime, this.issearch, this.maxpid, this.numTable };
                    return;
                }
                case 4:
                {
                    DataSet set6 = CallBLL.cz_phase_jsk3_bll.GetPhaseByPage(Convert.ToInt32(this.page) - 1, this.pageSize, ref this.pageCount, ref this.dataCount, this.beginTime, this.endTime, this.issearch, this.maxpid);
                    if (((set6 != null) && (set6.Tables.Count > 0)) && (set6.Tables[0].Rows.Count > 0))
                    {
                        this.dataTable = set6.Tables[0];
                        if (string.IsNullOrEmpty(this.maxpid))
                        {
                            this.maxpid = this.dataTable.Rows[0]["p_id"].ToString();
                        }
                    }
                    this.FiledValue = new string[] { this.lotteryId, this.beginTime, this.endTime, this.issearch, this.maxpid, this.numTable };
                    return;
                }
                case 5:
                {
                    DataSet set7 = CallBLL.cz_phase_kl8_bll.GetPhaseByPage(Convert.ToInt32(this.page) - 1, this.pageSize, ref this.pageCount, ref this.dataCount, this.beginTime, this.endTime, this.issearch, this.maxpid);
                    if (((set7 != null) && (set7.Tables.Count > 0)) && (set7.Tables[0].Rows.Count > 0))
                    {
                        this.dataTable = set7.Tables[0];
                        if (string.IsNullOrEmpty(this.maxpid))
                        {
                            this.maxpid = this.dataTable.Rows[0]["p_id"].ToString();
                        }
                    }
                    this.FiledValue = new string[] { this.lotteryId, this.beginTime, this.endTime, this.issearch, this.maxpid, this.numTable };
                    return;
                }
                case 6:
                {
                    DataSet set8 = CallBLL.cz_phase_k8sc_bll.GetPhaseByPage(Convert.ToInt32(this.page) - 1, this.pageSize, ref this.pageCount, ref this.dataCount, this.beginTime, this.endTime, this.issearch, this.maxpid);
                    if (((set8 != null) && (set8.Tables.Count > 0)) && (set8.Tables[0].Rows.Count > 0))
                    {
                        this.dataTable = set8.Tables[0];
                        if (string.IsNullOrEmpty(this.maxpid))
                        {
                            this.maxpid = this.dataTable.Rows[0]["p_id"].ToString();
                        }
                    }
                    this.FiledValue = new string[] { this.lotteryId, this.beginTime, this.endTime, this.issearch, this.maxpid, this.numTable };
                    return;
                }
                case 7:
                {
                    DataSet set9 = CallBLL.cz_phase_pcdd_bll.GetPhaseByPage(Convert.ToInt32(this.page) - 1, this.pageSize, ref this.pageCount, ref this.dataCount, this.beginTime, this.endTime, this.issearch, this.maxpid);
                    if (((set9 != null) && (set9.Tables.Count > 0)) && (set9.Tables[0].Rows.Count > 0))
                    {
                        this.dataTable = set9.Tables[0];
                        if (string.IsNullOrEmpty(this.maxpid))
                        {
                            this.maxpid = this.dataTable.Rows[0]["p_id"].ToString();
                        }
                    }
                    this.FiledValue = new string[] { this.lotteryId, this.beginTime, this.endTime, this.issearch, this.maxpid, this.numTable };
                    return;
                }
                case 8:
                {
                    DataSet set11 = CallBLL.cz_phase_pkbjl_bll.GetPhaseByPage(Convert.ToInt32(this.page) - 1, this.pageSize, ref this.pageCount, ref this.dataCount, this.beginTime, this.endTime, this.issearch, this.maxpid, this.numTable);
                    if (((set11 != null) && (set11.Tables.Count > 0)) && (set11.Tables[0].Rows.Count > 0))
                    {
                        this.dataTable = set11.Tables[0];
                        if (string.IsNullOrEmpty(this.maxpid))
                        {
                            this.maxpid = this.dataTable.Rows[0]["p_id"].ToString();
                        }
                    }
                    this.FiledValue = new string[] { this.lotteryId, this.beginTime, this.endTime, this.issearch, this.maxpid, this.numTable };
                    return;
                }
                case 9:
                {
                    DataSet set10 = CallBLL.cz_phase_xyft5_bll.GetPhaseByPage(Convert.ToInt32(this.page) - 1, this.pageSize, ref this.pageCount, ref this.dataCount, this.beginTime, this.endTime, this.issearch, this.maxpid);
                    if (((set10 != null) && (set10.Tables.Count > 0)) && (set10.Tables[0].Rows.Count > 0))
                    {
                        this.dataTable = set10.Tables[0];
                        if (string.IsNullOrEmpty(this.maxpid))
                        {
                            this.maxpid = this.dataTable.Rows[0]["p_id"].ToString();
                        }
                    }
                    this.FiledValue = new string[] { this.lotteryId, this.beginTime, this.endTime, this.issearch, this.maxpid, this.numTable };
                    return;
                }
                case 10:
                {
                    DataSet set12 = CallBLL.cz_phase_jscar_bll.GetPhaseByPage(Convert.ToInt32(this.page) - 1, this.pageSize, ref this.pageCount, ref this.dataCount, this.beginTime, this.endTime, this.issearch, this.maxpid);
                    if (((set12 != null) && (set12.Tables.Count > 0)) && (set12.Tables[0].Rows.Count > 0))
                    {
                        this.dataTable = set12.Tables[0];
                        if (string.IsNullOrEmpty(this.maxpid))
                        {
                            this.maxpid = this.dataTable.Rows[0]["p_id"].ToString();
                        }
                    }
                    this.FiledValue = new string[] { this.lotteryId, this.beginTime, this.endTime, this.issearch, this.maxpid, this.numTable };
                    return;
                }
                case 11:
                {
                    DataSet set13 = CallBLL.cz_phase_speed5_bll.GetPhaseByPage(Convert.ToInt32(this.page) - 1, this.pageSize, ref this.pageCount, ref this.dataCount, this.beginTime, this.endTime, this.issearch, this.maxpid);
                    if (((set13 != null) && (set13.Tables.Count > 0)) && (set13.Tables[0].Rows.Count > 0))
                    {
                        this.dataTable = set13.Tables[0];
                        if (string.IsNullOrEmpty(this.maxpid))
                        {
                            this.maxpid = this.dataTable.Rows[0]["p_id"].ToString();
                        }
                    }
                    this.FiledValue = new string[] { this.lotteryId, this.beginTime, this.endTime, this.issearch, this.maxpid, this.numTable };
                    return;
                }
                case 12:
                {
                    DataSet set15 = CallBLL.cz_phase_jspk10_bll.GetPhaseByPage(Convert.ToInt32(this.page) - 1, this.pageSize, ref this.pageCount, ref this.dataCount, this.beginTime, this.endTime, this.issearch, this.maxpid);
                    if (((set15 != null) && (set15.Tables.Count > 0)) && (set15.Tables[0].Rows.Count > 0))
                    {
                        this.dataTable = set15.Tables[0];
                        if (string.IsNullOrEmpty(this.maxpid))
                        {
                            this.maxpid = this.dataTable.Rows[0]["p_id"].ToString();
                        }
                    }
                    this.FiledValue = new string[] { this.lotteryId, this.beginTime, this.endTime, this.issearch, this.maxpid, this.numTable };
                    return;
                }
                case 13:
                {
                    DataSet set14 = CallBLL.cz_phase_jscqsc_bll.GetPhaseByPage(Convert.ToInt32(this.page) - 1, this.pageSize, ref this.pageCount, ref this.dataCount, this.beginTime, this.endTime, this.issearch, this.maxpid);
                    if (((set14 != null) && (set14.Tables.Count > 0)) && (set14.Tables[0].Rows.Count > 0))
                    {
                        this.dataTable = set14.Tables[0];
                        if (string.IsNullOrEmpty(this.maxpid))
                        {
                            this.maxpid = this.dataTable.Rows[0]["p_id"].ToString();
                        }
                    }
                    this.FiledValue = new string[] { this.lotteryId, this.beginTime, this.endTime, this.issearch, this.maxpid, this.numTable };
                    return;
                }
                case 14:
                {
                    DataSet set16 = CallBLL.cz_phase_jssfc_bll.GetPhaseByPage(Convert.ToInt32(this.page) - 1, this.pageSize, ref this.pageCount, ref this.dataCount, this.beginTime, this.endTime, this.issearch, this.maxpid);
                    if (((set16 != null) && (set16.Tables.Count > 0)) && (set16.Tables[0].Rows.Count > 0))
                    {
                        this.dataTable = set16.Tables[0];
                        if (string.IsNullOrEmpty(this.maxpid))
                        {
                            this.maxpid = this.dataTable.Rows[0]["p_id"].ToString();
                        }
                    }
                    this.FiledValue = new string[] { this.lotteryId, this.beginTime, this.endTime, this.issearch, this.maxpid, this.numTable };
                    return;
                }
                case 15:
                {
                    DataSet set17 = CallBLL.cz_phase_jsft2_bll.GetPhaseByPage(Convert.ToInt32(this.page) - 1, this.pageSize, ref this.pageCount, ref this.dataCount, this.beginTime, this.endTime, this.issearch, this.maxpid);
                    if (((set17 != null) && (set17.Tables.Count > 0)) && (set17.Tables[0].Rows.Count > 0))
                    {
                        this.dataTable = set17.Tables[0];
                        if (string.IsNullOrEmpty(this.maxpid))
                        {
                            this.maxpid = this.dataTable.Rows[0]["p_id"].ToString();
                        }
                    }
                    this.FiledValue = new string[] { this.lotteryId, this.beginTime, this.endTime, this.issearch, this.maxpid, this.numTable };
                    return;
                }
                case 0x10:
                {
                    DataSet set18 = CallBLL.cz_phase_car168_bll.GetPhaseByPage(Convert.ToInt32(this.page) - 1, this.pageSize, ref this.pageCount, ref this.dataCount, this.beginTime, this.endTime, this.issearch, this.maxpid);
                    if (((set18 != null) && (set18.Tables.Count > 0)) && (set18.Tables[0].Rows.Count > 0))
                    {
                        this.dataTable = set18.Tables[0];
                        if (string.IsNullOrEmpty(this.maxpid))
                        {
                            this.maxpid = this.dataTable.Rows[0]["p_id"].ToString();
                        }
                    }
                    this.FiledValue = new string[] { this.lotteryId, this.beginTime, this.endTime, this.issearch, this.maxpid, this.numTable };
                    return;
                }
                case 0x11:
                {
                    DataSet set19 = CallBLL.cz_phase_ssc168_bll.GetPhaseByPage(Convert.ToInt32(this.page) - 1, this.pageSize, ref this.pageCount, ref this.dataCount, this.beginTime, this.endTime, this.issearch, this.maxpid);
                    if (((set19 != null) && (set19.Tables.Count > 0)) && (set19.Tables[0].Rows.Count > 0))
                    {
                        this.dataTable = set19.Tables[0];
                        if (string.IsNullOrEmpty(this.maxpid))
                        {
                            this.maxpid = this.dataTable.Rows[0]["p_id"].ToString();
                        }
                    }
                    this.FiledValue = new string[] { this.lotteryId, this.beginTime, this.endTime, this.issearch, this.maxpid, this.numTable };
                    return;
                }
                case 0x12:
                {
                    DataSet set20 = CallBLL.cz_phase_vrcar_bll.GetPhaseByPage(Convert.ToInt32(this.page) - 1, this.pageSize, ref this.pageCount, ref this.dataCount, this.beginTime, this.endTime, this.issearch, this.maxpid);
                    if (((set20 != null) && (set20.Tables.Count > 0)) && (set20.Tables[0].Rows.Count > 0))
                    {
                        this.dataTable = set20.Tables[0];
                        if (string.IsNullOrEmpty(this.maxpid))
                        {
                            this.maxpid = this.dataTable.Rows[0]["p_id"].ToString();
                        }
                    }
                    this.FiledValue = new string[] { this.lotteryId, this.beginTime, this.endTime, this.issearch, this.maxpid, this.numTable };
                    return;
                }
                case 0x13:
                {
                    DataSet set21 = CallBLL.cz_phase_vrssc_bll.GetPhaseByPage(Convert.ToInt32(this.page) - 1, this.pageSize, ref this.pageCount, ref this.dataCount, this.beginTime, this.endTime, this.issearch, this.maxpid);
                    if (((set21 != null) && (set21.Tables.Count > 0)) && (set21.Tables[0].Rows.Count > 0))
                    {
                        this.dataTable = set21.Tables[0];
                        if (string.IsNullOrEmpty(this.maxpid))
                        {
                            this.maxpid = this.dataTable.Rows[0]["p_id"].ToString();
                        }
                    }
                    this.FiledValue = new string[] { this.lotteryId, this.beginTime, this.endTime, this.issearch, this.maxpid, this.numTable };
                    return;
                }
                case 20:
                {
                    DataSet set22 = CallBLL.cz_phase_xyftoa_bll.GetPhaseByPage(Convert.ToInt32(this.page) - 1, this.pageSize, ref this.pageCount, ref this.dataCount, this.beginTime, this.endTime, this.issearch, this.maxpid);
                    if (((set22 != null) && (set22.Tables.Count > 0)) && (set22.Tables[0].Rows.Count > 0))
                    {
                        this.dataTable = set22.Tables[0];
                        if (string.IsNullOrEmpty(this.maxpid))
                        {
                            this.maxpid = this.dataTable.Rows[0]["p_id"].ToString();
                        }
                    }
                    this.FiledValue = new string[] { this.lotteryId, this.beginTime, this.endTime, this.issearch, this.maxpid, this.numTable };
                    return;
                }
                case 0x15:
                {
                    DataSet set23 = CallBLL.cz_phase_xyftsg_bll.GetPhaseByPage(Convert.ToInt32(this.page) - 1, this.pageSize, ref this.pageCount, ref this.dataCount, this.beginTime, this.endTime, this.issearch, this.maxpid);
                    if (((set23 != null) && (set23.Tables.Count > 0)) && (set23.Tables[0].Rows.Count > 0))
                    {
                        this.dataTable = set23.Tables[0];
                        if (string.IsNullOrEmpty(this.maxpid))
                        {
                            this.maxpid = this.dataTable.Rows[0]["p_id"].ToString();
                        }
                    }
                    this.FiledValue = new string[] { this.lotteryId, this.beginTime, this.endTime, this.issearch, this.maxpid, this.numTable };
                    return;
                }
                case 0x16:
                {
                    DataSet set24 = CallBLL.cz_phase_happycar_bll.GetPhaseByPage(Convert.ToInt32(this.page) - 1, this.pageSize, ref this.pageCount, ref this.dataCount, this.beginTime, this.endTime, this.issearch, this.maxpid);
                    if (((set24 != null) && (set24.Tables.Count > 0)) && (set24.Tables[0].Rows.Count > 0))
                    {
                        this.dataTable = set24.Tables[0];
                        if (string.IsNullOrEmpty(this.maxpid))
                        {
                            this.maxpid = this.dataTable.Rows[0]["p_id"].ToString();
                        }
                    }
                    this.FiledValue = new string[] { this.lotteryId, this.beginTime, this.endTime, this.issearch, this.maxpid, this.numTable };
                    return;
                }
                case 100:
                {
                    DataSet set = CallBLL.cz_phase_six_bll.GetPhaseByPage(Convert.ToInt32(this.page) - 1, this.pageSize, ref this.pageCount, ref this.dataCount, this.beginTime, this.endTime, this.issearch, this.maxpid);
                    if (((set != null) && (set.Tables.Count > 0)) && (set.Tables[0].Rows.Count > 0))
                    {
                        this.dataTable = set.Tables[0];
                        if (string.IsNullOrEmpty(this.maxpid))
                        {
                            this.maxpid = this.dataTable.Rows[0]["p_id"].ToString();
                        }
                        this.SixMaxPID = CallBLL.cz_phase_six_bll.GetCurrentPhase().get_p_id().ToString();
                    }
                    this.FiledValue = new string[] { this.lotteryId, this.beginTime, this.endTime, this.issearch, this.maxpid, this.numTable };
                    return;
                }
            }
        }

        protected void SetDate()
        {
            string str = "";
            DateTime now = DateTime.Now;
            now.ToString("yyyy-MM-dd");
            str = now.ToString("yyyy-MM-dd");
            int num = Convert.ToInt32(now.DayOfWeek.ToString("d"));
            if (num == 0)
            {
                num = 7;
            }
            this.yesterday = now.AddDays(-1.0);
            this.startWeek = now.AddDays((double) (1 - num));
            this.endWeek = this.startWeek.AddDays(6.0);
            this.lastendWeek = this.startWeek.AddDays(-1.0);
            this.laststartWeek = this.lastendWeek.AddDays(-6.0);
            this.startMonth = now.AddDays((double) (1 - now.Day));
            this.endMonth = this.startMonth.AddMonths(1).AddDays(-1.0);
            this.lastendMonth = this.startMonth.AddDays(-1.0);
            this.laststartMonth = this.startMonth.AddMonths(-1);
            if (string.IsNullOrEmpty(this.beginTime))
            {
                this.beginTime = now.AddDays(-15.0).ToString("yyyy-MM-dd");
            }
            if (string.IsNullOrEmpty(this.endTime))
            {
                this.endTime = str;
            }
        }
    }
}

