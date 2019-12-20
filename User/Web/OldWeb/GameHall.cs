namespace User.Web.OldWeb
{
    using System;
    using System.Data;
    using System.Web.UI.HtmlControls;
    using User.Web.WebBase;

    public class GameHall : MemberPageBase
    {
        protected string currentTime = "";
        protected HtmlGenericControl DDD;
        protected DataTable DT = null;
        protected string txt_endtime = "";
        protected string txt_state = "";
        protected string u_type = "";
        protected string url = "";

        protected string GetStateString(string state)
        {
            string str = "";
            if (state.Equals("0"))
            {
                str = string.Format("<img src=\"{0}\">", "/images/lottery_state_0.gif");
            }
            else if (state.Equals("1"))
            {
                str = string.Format("<img src=\"{0}\">", "/images/lottery_state_1.gif");
            }
            if (state.Equals("2"))
            {
                str = string.Format("<img src=\"{0}\">", "/images/lottery_state_2.gif");
            }
            return str;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.DT = base.GetLotteryList();
            this.currentTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        protected void SetData(string lid)
        {
            DataTable currentPhase;
            this.url = string.Format("/{0}/index.aspx?lid={1}&path={2}", base.GetGameFolderByID(lid), lid, base.GetGameFolderByID(lid));
            this.txt_state = "";
            this.txt_endtime = "";
            int num = 100;
            if (lid.Equals(num.ToString()))
            {
                currentPhase = CallBLL.cz_phase_six_bll.GetCurrentPhase("1");
                if ((currentPhase != null) || (currentPhase.Rows.Count > 0))
                {
                    DateTime now = DateTime.Now;
                    string s = currentPhase.Rows[0]["sn_stop_date"].ToString();
                    if (now < DateTime.Parse(s))
                    {
                        this.txt_state = "2";
                        this.txt_endtime = Convert.ToDateTime(s).ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    else
                    {
                        this.txt_state = "0";
                        this.txt_endtime = "-";
                    }
                }
            }
            else
            {
                currentPhase = null;
                switch (int.Parse(lid))
                {
                    case 0:
                        currentPhase = CallBLL.cz_phase_kl10_bll.IsPhaseClose();
                        break;

                    case 1:
                        currentPhase = CallBLL.cz_phase_cqsc_bll.IsPhaseClose();
                        break;

                    case 2:
                        currentPhase = CallBLL.cz_phase_pk10_bll.IsPhaseClose();
                        break;

                    case 3:
                        currentPhase = CallBLL.cz_phase_xync_bll.IsPhaseClose();
                        break;

                    case 4:
                        currentPhase = CallBLL.cz_phase_jsk3_bll.IsPhaseClose();
                        break;

                    case 5:
                        currentPhase = CallBLL.cz_phase_kl8_bll.IsPhaseClose();
                        break;

                    case 6:
                        currentPhase = CallBLL.cz_phase_k8sc_bll.IsPhaseClose();
                        break;

                    case 7:
                        currentPhase = CallBLL.cz_phase_pcdd_bll.IsPhaseClose();
                        break;

                    case 8:
                        currentPhase = CallBLL.cz_phase_pkbjl_bll.IsPhaseClose();
                        break;

                    case 9:
                        currentPhase = CallBLL.cz_phase_xyft5_bll.IsPhaseClose();
                        break;

                    case 10:
                        currentPhase = CallBLL.cz_phase_jscar_bll.IsPhaseClose();
                        break;

                    case 11:
                        currentPhase = CallBLL.cz_phase_speed5_bll.IsPhaseClose();
                        break;

                    case 12:
                        currentPhase = CallBLL.cz_phase_jspk10_bll.IsPhaseClose();
                        break;

                    case 13:
                        currentPhase = CallBLL.cz_phase_jscqsc_bll.IsPhaseClose();
                        break;

                    case 14:
                        currentPhase = CallBLL.cz_phase_jssfc_bll.IsPhaseClose();
                        break;

                    case 15:
                        currentPhase = CallBLL.cz_phase_jsft2_bll.IsPhaseClose();
                        break;

                    case 0x10:
                        currentPhase = CallBLL.cz_phase_car168_bll.IsPhaseClose();
                        break;

                    case 0x11:
                        currentPhase = CallBLL.cz_phase_ssc168_bll.IsPhaseClose();
                        break;

                    case 0x12:
                        currentPhase = CallBLL.cz_phase_vrcar_bll.IsPhaseClose();
                        break;

                    case 0x13:
                        currentPhase = CallBLL.cz_phase_vrssc_bll.IsPhaseClose();
                        break;

                    case 20:
                        currentPhase = CallBLL.cz_phase_xyftoa_bll.IsPhaseClose();
                        break;

                    case 0x15:
                        currentPhase = CallBLL.cz_phase_xyftsg_bll.IsPhaseClose();
                        break;

                    case 0x16:
                        currentPhase = CallBLL.cz_phase_happycar_bll.IsPhaseClose();
                        break;
                }
                if ((currentPhase != null) && (currentPhase.Rows.Count > 0))
                {
                    string str2 = currentPhase.Rows[0]["isopen"].ToString();
                    string str3 = currentPhase.Rows[0]["openning"].ToString();
                    string str4 = currentPhase.Rows[0]["opendate"].ToString();
                    string str5 = currentPhase.Rows[0]["endtime"].ToString();
                    DateTime time2 = Convert.ToDateTime(this.DT.Select(string.Format(" id={0} ", lid))[0]["begintime"].ToString());
                    if (str2.Equals("0"))
                    {
                        this.txt_state = "0";
                        DateTime time3 = DateTime.Now;
                        string introduced14 = time3.ToString("yyyy-MM-dd");
                        if (introduced14 == time3.AddHours(7.0).ToString("yyyy-MM-dd"))
                        {
                            this.txt_endtime = time3.ToString("yyyy-MM-dd ") + time2.ToString("HH:mm:ss");
                        }
                        else
                        {
                            this.txt_endtime = time3.AddDays(1.0).ToString("yyyy-MM-dd ") + time2.ToString("HH:mm:ss");
                        }
                    }
                    else
                    {
                        if (str3.Equals("n"))
                        {
                            this.txt_state = "1";
                        }
                        else
                        {
                            this.txt_state = "2";
                        }
                        this.txt_endtime = Convert.ToDateTime(str5).ToString("HH:mm:ss");
                    }
                }
            }
        }
    }
}

