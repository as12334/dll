namespace Agent.Web
{
    using Agent.Web.WebBase;
    using LotterySystem.Model;
    using System;
    using System.Data;
    using System.Web.UI.HtmlControls;

    public class GameHall : MemberPageBase
    {
        protected string currentTime = "";
        protected HtmlGenericControl DDD;
        protected DataTable DT;
        protected string skin = "";
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
            string str = this.Session["user_name"].ToString();
            agent_userinfo_session _session = this.Session[str + "lottery_session_user_info"] as agent_userinfo_session;
            _session.get_u_name().Trim();
            this.skin = _session.get_u_skin();
            this.DT = base.GetLotteryList();
            this.currentTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        protected void SetData(string lid)
        {
            this.url = base.GetLotteryLogoImgUrl(lid);
            this.txt_state = "";
            this.txt_endtime = "";
            int num = 100;
            if (lid.Equals(num.ToString()))
            {
                DataTable currentPhase = CallBLL.cz_phase_six_bll.GetCurrentPhase("1");
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
                DataTable table2 = null;
                switch (int.Parse(lid))
                {
                    case 0:
                        table2 = CallBLL.cz_phase_kl10_bll.IsPhaseClose();
                        break;

                    case 1:
                        table2 = CallBLL.cz_phase_cqsc_bll.IsPhaseClose();
                        break;

                    case 2:
                        table2 = CallBLL.cz_phase_pk10_bll.IsPhaseClose();
                        break;

                    case 3:
                        table2 = CallBLL.cz_phase_xync_bll.IsPhaseClose();
                        break;

                    case 4:
                        table2 = CallBLL.cz_phase_jsk3_bll.IsPhaseClose();
                        break;

                    case 5:
                        table2 = CallBLL.cz_phase_kl8_bll.IsPhaseClose();
                        break;

                    case 6:
                        table2 = CallBLL.cz_phase_k8sc_bll.IsPhaseClose();
                        break;

                    case 7:
                        table2 = CallBLL.cz_phase_pcdd_bll.IsPhaseClose();
                        break;

                    case 8:
                        table2 = CallBLL.cz_phase_pkbjl_bll.IsPhaseClose();
                        break;

                    case 9:
                        table2 = CallBLL.cz_phase_xyft5_bll.IsPhaseClose();
                        break;

                    case 12:
                        table2 = CallBLL.cz_phase_jspk10_bll.IsPhaseClose();
                        break;

                    case 13:
                        table2 = CallBLL.cz_phase_jscqsc_bll.IsPhaseClose();
                        break;

                    case 14:
                        table2 = CallBLL.cz_phase_jssfc_bll.IsPhaseClose();
                        break;

                    case 15:
                        table2 = CallBLL.cz_phase_jsft2_bll.IsPhaseClose();
                        break;
                }
                if ((table2 != null) && (table2.Rows.Count > 0))
                {
                    string str2 = table2.Rows[0]["isopen"].ToString();
                    string str3 = table2.Rows[0]["openning"].ToString();
                    table2.Rows[0]["opendate"].ToString();
                    string str4 = table2.Rows[0]["endtime"].ToString();
                    DateTime time2 = Convert.ToDateTime(this.DT.Select(string.Format(" id={0} ", lid))[0]["begintime"].ToString());
                    if (str2.Equals("0"))
                    {
                        this.txt_state = "0";
                        DateTime time3 = DateTime.Now;
                        string introduced16 = time3.ToString("yyyy-MM-dd");
                        if (introduced16 == time3.AddHours(7.0).ToString("yyyy-MM-dd"))
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
                        this.txt_endtime = Convert.ToDateTime(str4).ToString("HH:mm:ss");
                    }
                }
            }
        }
    }
}

