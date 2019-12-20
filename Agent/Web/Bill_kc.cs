namespace Agent.Web
{
    using Agent.Web.WebBase;
    using LotterySystem.Common;
    using LotterySystem.Model;
    using System;
    using System.Data;

    public class Bill_kc : MemberPageBase
    {
        private string drawback = "";
        private string levelName = "";
        protected string lotteryId = "";
        protected string lotteryName = "";
        private DateTime now = DateTime.Now;
        protected string phase = "";
        protected string phase_id = "";
        private DataTable phaseTable;
        protected DataTable playTable;
        protected string skin = "";
        protected DataTable table;
        protected string u_type = "";
        private agent_userinfo_session userModel;
        private string userName = "";

        private void GetNoPhaseSWF()
        {
            string gameFolderFileByID = base.GetGameFolderFileByID(this.lotteryId);
            string url = string.Format("/noopen.aspx?lid={0}&path={1}", this.lotteryId, base.Server.UrlEncode(gameFolderFileByID));
            base.Response.Redirect(url, true);
        }

        private string GetSWF()
        {
            base.Response.Redirect("/MessagePage.aspx?code=u100052&url=&issuccess=1&isback=0");
            base.Response.End();
            return "";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.userName = this.Session["user_name"].ToString();
            this.userModel = this.Session[this.userName + "lottery_session_user_info"] as agent_userinfo_session;
            this.skin = this.userModel.get_u_skin();
            this.u_type = this.userModel.get_u_type().Trim();
            base.Permission_Aspx_ZJ(this.userModel, "po_1_1");
            base.Permission_Aspx_DL(this.userModel, "po_5_1");
            this.lotteryId = LSRequest.qq("lid");
            this.lotteryName = base.GetGameNameByID(this.lotteryId);
            string str = this.now.ToString("yyyy-MM-dd") + " 00:00:00";
            switch (Convert.ToInt32(this.lotteryId))
            {
                case 0:
                    if (CallBLL.cz_phase_kl10_bll.GetPhaseCountByQueryDate(str).Equals(0))
                    {
                        this.GetNoPhaseSWF();
                    }
                    this.phaseTable = CallBLL.cz_phase_kl10_bll.IsOpenPhaseByTime(this.now);
                    break;

                case 1:
                    if (CallBLL.cz_phase_cqsc_bll.GetPhaseCountByQueryDate(str).Equals(0))
                    {
                        this.GetNoPhaseSWF();
                    }
                    this.phaseTable = CallBLL.cz_phase_cqsc_bll.IsOpenPhaseByTime(this.now);
                    break;

                case 2:
                    if (CallBLL.cz_phase_pk10_bll.GetPhaseCountByQueryDate(str).Equals(0))
                    {
                        this.GetNoPhaseSWF();
                    }
                    this.phaseTable = CallBLL.cz_phase_pk10_bll.IsOpenPhaseByTime(this.now);
                    break;

                case 3:
                    if (CallBLL.cz_phase_xync_bll.GetPhaseCountByQueryDate(str).Equals(0))
                    {
                        this.GetNoPhaseSWF();
                    }
                    this.phaseTable = CallBLL.cz_phase_xync_bll.IsOpenPhaseByTime(this.now);
                    break;

                case 4:
                    if (CallBLL.cz_phase_jsk3_bll.GetPhaseCountByQueryDate(str).Equals(0))
                    {
                        this.GetNoPhaseSWF();
                    }
                    this.phaseTable = CallBLL.cz_phase_jsk3_bll.IsOpenPhaseByTime(this.now);
                    break;

                case 5:
                    if (CallBLL.cz_phase_kl8_bll.GetPhaseCountByQueryDate(str).Equals(0))
                    {
                        this.GetNoPhaseSWF();
                    }
                    this.phaseTable = CallBLL.cz_phase_kl8_bll.IsOpenPhaseByTime(this.now);
                    break;

                case 6:
                    if (CallBLL.cz_phase_k8sc_bll.GetPhaseCountByQueryDate(str).Equals(0))
                    {
                        this.GetNoPhaseSWF();
                    }
                    this.phaseTable = CallBLL.cz_phase_k8sc_bll.IsOpenPhaseByTime(this.now);
                    break;

                case 7:
                    if (CallBLL.cz_phase_pcdd_bll.GetPhaseCountByQueryDate(str).Equals(0))
                    {
                        this.GetNoPhaseSWF();
                    }
                    this.phaseTable = CallBLL.cz_phase_pcdd_bll.IsOpenPhaseByTime(this.now);
                    break;

                case 8:
                    if (CallBLL.cz_phase_pkbjl_bll.GetPhaseCountByQueryDate(str).Equals(0))
                    {
                        this.GetNoPhaseSWF();
                    }
                    this.phaseTable = CallBLL.cz_phase_pkbjl_bll.IsOpenPhaseByTime(this.now);
                    break;

                case 9:
                    if (CallBLL.cz_phase_xyft5_bll.GetPhaseCountByQueryDate(str).Equals(0))
                    {
                        this.GetNoPhaseSWF();
                    }
                    this.phaseTable = CallBLL.cz_phase_xyft5_bll.IsOpenPhaseByTime(this.now);
                    break;

                case 10:
                    if (CallBLL.cz_phase_jscar_bll.GetPhaseCountByQueryDate(str).Equals(0))
                    {
                        this.GetNoPhaseSWF();
                    }
                    this.phaseTable = CallBLL.cz_phase_jscar_bll.IsOpenPhaseByTime(this.now);
                    break;

                case 11:
                    if (CallBLL.cz_phase_speed5_bll.GetPhaseCountByQueryDate(str).Equals(0))
                    {
                        this.GetNoPhaseSWF();
                    }
                    this.phaseTable = CallBLL.cz_phase_speed5_bll.IsOpenPhaseByTime(this.now);
                    break;

                case 12:
                    if (CallBLL.cz_phase_jspk10_bll.GetPhaseCountByQueryDate(str).Equals(0))
                    {
                        this.GetNoPhaseSWF();
                    }
                    this.phaseTable = CallBLL.cz_phase_jspk10_bll.IsOpenPhaseByTime(this.now);
                    break;

                case 13:
                    if (CallBLL.cz_phase_jscqsc_bll.GetPhaseCountByQueryDate(str).Equals(0))
                    {
                        this.GetNoPhaseSWF();
                    }
                    this.phaseTable = CallBLL.cz_phase_jscqsc_bll.IsOpenPhaseByTime(this.now);
                    break;

                case 14:
                    if (CallBLL.cz_phase_jssfc_bll.GetPhaseCountByQueryDate(str).Equals(0))
                    {
                        this.GetNoPhaseSWF();
                    }
                    this.phaseTable = CallBLL.cz_phase_jssfc_bll.IsOpenPhaseByTime(this.now);
                    break;

                case 15:
                    if (CallBLL.cz_phase_jsft2_bll.GetPhaseCountByQueryDate(str).Equals(0))
                    {
                        this.GetNoPhaseSWF();
                    }
                    this.phaseTable = CallBLL.cz_phase_jsft2_bll.IsOpenPhaseByTime(this.now);
                    break;

                case 0x10:
                    if (CallBLL.cz_phase_car168_bll.GetPhaseCountByQueryDate(str).Equals(0))
                    {
                        this.GetNoPhaseSWF();
                    }
                    this.phaseTable = CallBLL.cz_phase_car168_bll.IsOpenPhaseByTime(this.now);
                    break;

                case 0x11:
                    if (CallBLL.cz_phase_ssc168_bll.GetPhaseCountByQueryDate(str).Equals(0))
                    {
                        this.GetNoPhaseSWF();
                    }
                    this.phaseTable = CallBLL.cz_phase_ssc168_bll.IsOpenPhaseByTime(this.now);
                    break;

                case 0x12:
                    if (CallBLL.cz_phase_vrcar_bll.GetPhaseCountByQueryDate(str).Equals(0))
                    {
                        this.GetNoPhaseSWF();
                    }
                    this.phaseTable = CallBLL.cz_phase_vrcar_bll.IsOpenPhaseByTime(this.now);
                    break;

                case 0x13:
                    if (CallBLL.cz_phase_vrssc_bll.GetPhaseCountByQueryDate(str).Equals(0))
                    {
                        this.GetNoPhaseSWF();
                    }
                    this.phaseTable = CallBLL.cz_phase_vrssc_bll.IsOpenPhaseByTime(this.now);
                    break;

                case 20:
                    if (CallBLL.cz_phase_xyftoa_bll.GetPhaseCountByQueryDate(str).Equals(0))
                    {
                        this.GetNoPhaseSWF();
                    }
                    this.phaseTable = CallBLL.cz_phase_xyftoa_bll.IsOpenPhaseByTime(this.now);
                    break;

                case 0x15:
                    if (CallBLL.cz_phase_xyftsg_bll.GetPhaseCountByQueryDate(str).Equals(0))
                    {
                        this.GetNoPhaseSWF();
                    }
                    this.phaseTable = CallBLL.cz_phase_xyftsg_bll.IsOpenPhaseByTime(this.now);
                    break;

                case 0x16:
                    if (CallBLL.cz_phase_happycar_bll.GetPhaseCountByQueryDate(str).Equals(0))
                    {
                        this.GetNoPhaseSWF();
                    }
                    this.phaseTable = CallBLL.cz_phase_happycar_bll.IsOpenPhaseByTime(this.now);
                    break;
            }
            if (this.phaseTable == null)
            {
                base.Response.Write(this.GetSWF());
                base.Response.End();
            }
            else if (Convert.ToDateTime(this.phaseTable.Rows[0]["stop_date"].ToString().Trim()) > this.now)
            {
                base.Response.Write(this.GetSWF());
                base.Response.End();
            }
            this.phase_id = this.phaseTable.Rows[0]["p_id"].ToString().Trim();
            this.phase = this.phaseTable.Rows[0]["phase"].ToString().Trim();
            LSRequest.qq("hdnCreate");
            string str2 = this.u_type;
            if (str2 != null)
            {
                if (!(str2 == "zj"))
                {
                    if (str2 == "fgs")
                    {
                        this.levelName = "fgs_name";
                        this.drawback = "gd_drawback";
                    }
                    else if (str2 == "gd")
                    {
                        this.levelName = "gd_name";
                        this.drawback = "zd_drawback";
                    }
                    else if (str2 == "zd")
                    {
                        this.levelName = "zd_name";
                        this.drawback = "dl_drawback";
                    }
                    else if (str2 == "dl")
                    {
                        this.levelName = "dl_name";
                        this.drawback = "hy_drawback";
                    }
                }
                else
                {
                    this.drawback = "fgs_drawback";
                }
            }
            this.playTable = CallBLL.cz_bet_kc_bll.GetBetByLotteryID(this.phase_id, this.lotteryId, this.u_type, this.userName, this.levelName);
        }
    }
}

