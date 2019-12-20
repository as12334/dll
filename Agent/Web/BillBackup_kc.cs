namespace Agent.Web
{
    using Agent.Web.WebBase;
    using LotterySystem.Common;
    using LotterySystem.Model;
    using System;
    using System.Data;
    using System.IO;
    using System.Text;
    using System.Web.UI.HtmlControls;

    public class BillBackup_kc : MemberPageBase
    {
        private string drawback = "";
        protected HtmlForm form1;
        private string levelName = "";
        protected string lotteryId = "";
        protected string lotteryName = "";
        private string lotteryText = "";
        private DateTime now = DateTime.Now;
        protected string phase = "";
        protected string phase_id = "";
        private DataTable phaseTable;
        protected DataTable table;
        protected string u_type = "";
        private agent_userinfo_session userModel;
        private string userName = "";

        private void Create()
        {
            DataTable table = CallBLL.cz_bet_kc_bll.GetBillListBackup(this.u_type, this.userName, this.levelName, this.drawback, this.phase_id, this.lotteryId);
            if (table != null)
            {
                StringWriter writer = new StringWriter();
                int num = 8;
                if (this.lotteryId.Equals(num.ToString()))
                {
                    writer.WriteLine("期號,註單號,時間,會員,成数,項目,明細,金額,賠率,桌號,一般/免傭,");
                }
                else
                {
                    writer.WriteLine("期號,註單號,時間,會員,成数,項目,明細,金額,賠率,");
                }
                foreach (DataRow row in table.Rows)
                {
                    string str = "";
                    if (row["isDelete"].ToString().Equals("1"))
                    {
                        str = "單已注銷";
                    }
                    int num2 = 8;
                    if (this.lotteryId.Equals(num2.ToString()))
                    {
                        string[] strArray = new string[0x17];
                        strArray[0] = row["phase"].ToString().Trim();
                        strArray[1] = ",";
                        strArray[2] = row["order_num"].ToString().Trim();
                        strArray[3] = "\t,";
                        strArray[4] = row["bet_time"].ToString().Trim();
                        strArray[5] = ",";
                        strArray[6] = row["u_name"].ToString().Trim();
                        strArray[7] = ",";
                        strArray[8] = row[this.u_type + "_rate"].ToString().Trim();
                        strArray[9] = ",";
                        strArray[10] = row["play_name"].ToString().Trim();
                        strArray[11] = ",";
                        strArray[12] = row["bet_val"].ToString().Trim();
                        strArray[13] = ",";
                        strArray[14] = row["amount"].ToString().Trim();
                        strArray[15] = ",";
                        strArray[0x10] = this.u_type.Equals("zj") ? row["odds_zj"].ToString().Trim() : row["odds"].ToString().Trim();
                        strArray[0x11] = ",第";
                        strArray[0x12] = row["table_type"].ToString().Trim();
                        strArray[0x13] = "桌,";
                        int num3 = 0;
                        strArray[20] = row["play_type"].ToString().Trim().Equals(num3.ToString()) ? "免傭" : "一般";
                        strArray[0x15] = ",";
                        strArray[0x16] = str;
                        writer.WriteLine(string.Concat(strArray));
                    }
                    else
                    {
                        writer.WriteLine(row["phase"].ToString().Trim() + "," + row["order_num"].ToString().Trim() + "\t," + row["bet_time"].ToString().Trim() + "," + row["u_name"].ToString().Trim() + "," + row[this.u_type + "_rate"].ToString().Trim() + "," + row["play_name"].ToString().Trim() + "," + row["bet_val"].ToString().Trim() + "," + row["amount"].ToString().Trim() + "," + (this.u_type.Equals("zj") ? row["odds_zj"].ToString().Trim() : row["odds"].ToString().Trim()) + "," + str);
                    }
                }
                writer.Close();
                base.Response.AddHeader("Content-Disposition", "attachment; filename=" + this.lotteryText + "_" + this.phase + ".csv");
                base.Response.ContentType = "application/ms-excel";
                base.Response.ContentEncoding = Encoding.GetEncoding("GB2312");
                base.Response.Write(writer);
                base.Response.End();
            }
            else
            {
                base.Response.Redirect("/MessagePage.aspx?code=u100053&url=&issuccess=1&isback=0");
                base.Response.End();
            }
        }

        private void GetNoPhaseSWF()
        {
            string gameFolderFileByID = base.GetGameFolderFileByID(this.lotteryId);
            string url = string.Format("/noopen.aspx?lid={0}&path={1}", this.lotteryId, base.Server.UrlEncode(gameFolderFileByID));
            base.Response.Redirect(url, true);
        }

        private string GetOnOpenSWF()
        {
            base.Response.Redirect("/MessagePage.aspx?code=u100052&url=&issuccess=1&isback=0");
            base.Response.End();
            return "";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.userName = this.Session["user_name"].ToString();
            this.userModel = this.Session[this.userName + "lottery_session_user_info"] as agent_userinfo_session;
            base.Permission_Aspx_ZJ(this.userModel, "po_1_1");
            base.Permission_Aspx_DL(this.userModel, "po_5_1");
            this.u_type = this.userModel.get_u_type().Trim();
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
                    this.lotteryText = base.GetGameNameByID(0.ToString());
                    break;

                case 1:
                    if (CallBLL.cz_phase_cqsc_bll.GetPhaseCountByQueryDate(str).Equals(0))
                    {
                        this.GetNoPhaseSWF();
                    }
                    this.phaseTable = CallBLL.cz_phase_cqsc_bll.IsOpenPhaseByTime(this.now);
                    this.lotteryText = base.GetGameNameByID(1.ToString());
                    break;

                case 2:
                    if (CallBLL.cz_phase_pk10_bll.GetPhaseCountByQueryDate(str).Equals(0))
                    {
                        this.GetNoPhaseSWF();
                    }
                    this.phaseTable = CallBLL.cz_phase_pk10_bll.IsOpenPhaseByTime(this.now);
                    this.lotteryText = base.GetGameNameByID(2.ToString());
                    break;

                case 3:
                    if (CallBLL.cz_phase_xync_bll.GetPhaseCountByQueryDate(str).Equals(0))
                    {
                        this.GetNoPhaseSWF();
                    }
                    this.phaseTable = CallBLL.cz_phase_xync_bll.IsOpenPhaseByTime(this.now);
                    this.lotteryText = base.GetGameNameByID(3.ToString());
                    break;

                case 4:
                    if (CallBLL.cz_phase_jsk3_bll.GetPhaseCountByQueryDate(str).Equals(0))
                    {
                        this.GetNoPhaseSWF();
                    }
                    this.phaseTable = CallBLL.cz_phase_jsk3_bll.IsOpenPhaseByTime(this.now);
                    this.lotteryText = base.GetGameNameByID(4.ToString());
                    break;

                case 5:
                    if (CallBLL.cz_phase_kl8_bll.GetPhaseCountByQueryDate(str).Equals(0))
                    {
                        this.GetNoPhaseSWF();
                    }
                    this.phaseTable = CallBLL.cz_phase_kl8_bll.IsOpenPhaseByTime(this.now);
                    this.lotteryText = base.GetGameNameByID(5.ToString());
                    break;

                case 6:
                    if (CallBLL.cz_phase_k8sc_bll.GetPhaseCountByQueryDate(str).Equals(0))
                    {
                        this.GetNoPhaseSWF();
                    }
                    this.phaseTable = CallBLL.cz_phase_k8sc_bll.IsOpenPhaseByTime(this.now);
                    this.lotteryText = base.GetGameNameByID(6.ToString());
                    break;

                case 7:
                    if (CallBLL.cz_phase_pcdd_bll.GetPhaseCountByQueryDate(str).Equals(0))
                    {
                        this.GetNoPhaseSWF();
                    }
                    this.phaseTable = CallBLL.cz_phase_pcdd_bll.IsOpenPhaseByTime(this.now);
                    this.lotteryText = base.GetGameNameByID(7.ToString());
                    break;

                case 8:
                    if (CallBLL.cz_phase_pkbjl_bll.GetPhaseCountByQueryDate(str).Equals(0))
                    {
                        this.GetNoPhaseSWF();
                    }
                    this.phaseTable = CallBLL.cz_phase_pkbjl_bll.IsOpenPhaseByTime(this.now);
                    this.lotteryText = base.GetGameNameByID(8.ToString());
                    break;

                case 9:
                    if (CallBLL.cz_phase_xyft5_bll.GetPhaseCountByQueryDate(str).Equals(0))
                    {
                        this.GetNoPhaseSWF();
                    }
                    this.phaseTable = CallBLL.cz_phase_xyft5_bll.IsOpenPhaseByTime(this.now);
                    this.lotteryText = base.GetGameNameByID(9.ToString());
                    break;

                case 10:
                    if (CallBLL.cz_phase_jscar_bll.GetPhaseCountByQueryDate(str).Equals(0))
                    {
                        this.GetNoPhaseSWF();
                    }
                    this.phaseTable = CallBLL.cz_phase_jscar_bll.IsOpenPhaseByTime(this.now);
                    this.lotteryText = base.GetGameNameByID(10.ToString());
                    break;

                case 11:
                    if (CallBLL.cz_phase_speed5_bll.GetPhaseCountByQueryDate(str).Equals(0))
                    {
                        this.GetNoPhaseSWF();
                    }
                    this.phaseTable = CallBLL.cz_phase_speed5_bll.IsOpenPhaseByTime(this.now);
                    this.lotteryText = base.GetGameNameByID(11.ToString());
                    break;

                case 12:
                    if (CallBLL.cz_phase_jspk10_bll.GetPhaseCountByQueryDate(str).Equals(0))
                    {
                        this.GetNoPhaseSWF();
                    }
                    this.phaseTable = CallBLL.cz_phase_jspk10_bll.IsOpenPhaseByTime(this.now);
                    this.lotteryText = base.GetGameNameByID(12.ToString());
                    break;

                case 13:
                    if (CallBLL.cz_phase_jscqsc_bll.GetPhaseCountByQueryDate(str).Equals(0))
                    {
                        this.GetNoPhaseSWF();
                    }
                    this.phaseTable = CallBLL.cz_phase_jscqsc_bll.IsOpenPhaseByTime(this.now);
                    this.lotteryText = base.GetGameNameByID(13.ToString());
                    break;

                case 14:
                    if (CallBLL.cz_phase_jssfc_bll.GetPhaseCountByQueryDate(str).Equals(0))
                    {
                        this.GetNoPhaseSWF();
                    }
                    this.phaseTable = CallBLL.cz_phase_jssfc_bll.IsOpenPhaseByTime(this.now);
                    this.lotteryText = base.GetGameNameByID(14.ToString());
                    break;

                case 15:
                    if (CallBLL.cz_phase_jsft2_bll.GetPhaseCountByQueryDate(str).Equals(0))
                    {
                        this.GetNoPhaseSWF();
                    }
                    this.phaseTable = CallBLL.cz_phase_jsft2_bll.IsOpenPhaseByTime(this.now);
                    this.lotteryText = base.GetGameNameByID(15.ToString());
                    break;

                case 0x10:
                    if (CallBLL.cz_phase_car168_bll.GetPhaseCountByQueryDate(str).Equals(0))
                    {
                        this.GetNoPhaseSWF();
                    }
                    this.phaseTable = CallBLL.cz_phase_car168_bll.IsOpenPhaseByTime(this.now);
                    this.lotteryText = base.GetGameNameByID(0x10.ToString());
                    break;

                case 0x11:
                    if (CallBLL.cz_phase_ssc168_bll.GetPhaseCountByQueryDate(str).Equals(0))
                    {
                        this.GetNoPhaseSWF();
                    }
                    this.phaseTable = CallBLL.cz_phase_ssc168_bll.IsOpenPhaseByTime(this.now);
                    this.lotteryText = base.GetGameNameByID(0x11.ToString());
                    break;

                case 0x12:
                    if (CallBLL.cz_phase_vrcar_bll.GetPhaseCountByQueryDate(str).Equals(0))
                    {
                        this.GetNoPhaseSWF();
                    }
                    this.phaseTable = CallBLL.cz_phase_vrcar_bll.IsOpenPhaseByTime(this.now);
                    this.lotteryText = base.GetGameNameByID(0x12.ToString());
                    break;

                case 0x13:
                    if (CallBLL.cz_phase_vrssc_bll.GetPhaseCountByQueryDate(str).Equals(0))
                    {
                        this.GetNoPhaseSWF();
                    }
                    this.phaseTable = CallBLL.cz_phase_vrssc_bll.IsOpenPhaseByTime(this.now);
                    this.lotteryText = base.GetGameNameByID(0x13.ToString());
                    break;

                case 20:
                    if (CallBLL.cz_phase_xyftoa_bll.GetPhaseCountByQueryDate(str).Equals(0))
                    {
                        this.GetNoPhaseSWF();
                    }
                    this.phaseTable = CallBLL.cz_phase_xyftoa_bll.IsOpenPhaseByTime(this.now);
                    this.lotteryText = base.GetGameNameByID(20.ToString());
                    break;

                case 0x15:
                    if (CallBLL.cz_phase_xyftsg_bll.GetPhaseCountByQueryDate(str).Equals(0))
                    {
                        this.GetNoPhaseSWF();
                    }
                    this.phaseTable = CallBLL.cz_phase_xyftsg_bll.IsOpenPhaseByTime(this.now);
                    this.lotteryText = base.GetGameNameByID(0x15.ToString());
                    break;

                case 0x16:
                    if (CallBLL.cz_phase_happycar_bll.GetPhaseCountByQueryDate(str).Equals(0))
                    {
                        this.GetNoPhaseSWF();
                    }
                    this.phaseTable = CallBLL.cz_phase_happycar_bll.IsOpenPhaseByTime(this.now);
                    this.lotteryText = base.GetGameNameByID(0x16.ToString());
                    break;
            }
            if (this.phaseTable == null)
            {
                base.Response.Write(this.GetOnOpenSWF());
                base.Response.End();
            }
            else if (Convert.ToDateTime(this.phaseTable.Rows[0]["stop_date"].ToString().Trim()) > this.now)
            {
                base.Response.Write(this.GetOnOpenSWF());
                base.Response.End();
            }
            this.phase_id = this.phaseTable.Rows[0]["p_id"].ToString().Trim();
            this.phase = this.phaseTable.Rows[0]["phase"].ToString().Trim();
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
            this.Create();
        }
    }
}

