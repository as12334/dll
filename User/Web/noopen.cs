namespace User.Web
{
    using LotterySystem.Common;
    using LotterySystem.Model;
    using System;
    using System.Data;
    using System.Web;
    using User.Web.WebBase;

    public class noopen : MemberPageBase
    {
        protected string v_beginTime = "";
        protected string v_currentTime = "";
        protected string v_lid = "";
        protected string v_lotteryName = "";
        protected string v_phase = "";
        protected string v_url = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            string str = HttpContext.Current.Session["user_name"].ToString();
            DataTable lotteryList = base.GetLotteryList();
            if (!string.IsNullOrEmpty(LSRequest.qq("lid")))
            {
                this.v_lid = LSRequest.qq("lid");
                DataTable currentPhase = null;
                switch (Convert.ToInt32(this.v_lid))
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

                    case 100:
                        currentPhase = CallBLL.cz_phase_six_bll.GetCurrentPhase("1");
                        break;
                }
                DataRow[] rowArray = lotteryList.Select(string.Format("id={0}", Convert.ToInt32(this.v_lid)));
                int num2 = 100;
                if (this.v_lid.Equals(num2.ToString()))
                {
                    cz_phase_six _six = CallBLL.cz_phase_six_bll.GetCurrentPhase();
                    this.v_url = string.Format("/{0}/index.aspx?lid={1}", "L_SIX", 100);
                    this.v_phase = _six.get_phase();
                }
                else if (currentPhase.Rows[0]["isopen"].ToString().Equals("0"))
                {
                    DateTime now = DateTime.Now;
                    DateTime time2 = DateTime.Now;
                    string introduced12 = now.ToString("yyyy-MM-dd");
                    if (introduced12 == now.AddHours(7.0).ToString("yyyy-MM-dd"))
                    {
                        time2 = Convert.ToDateTime(now.ToString("yyyy-MM-dd") + " " + rowArray[0]["begintime"].ToString());
                    }
                    else
                    {
                        time2 = Convert.ToDateTime(now.AddDays(1.0).ToString("yyyy-MM-dd") + " " + rowArray[0]["begintime"].ToString());
                    }
                    this.v_currentTime = now.ToString("yyyy-MM-dd HH:mm:ss");
                    this.v_beginTime = time2.ToString("yyyy-MM-dd HH:mm:ss");
                    string str2 = "";
                    switch (Convert.ToInt32(this.v_lid))
                    {
                        case 0:
                        case 2:
                        case 3:
                        case 8:
                        case 9:
                        case 10:
                        case 12:
                        case 14:
                        case 15:
                        case 0x10:
                        case 0x12:
                        case 20:
                        case 0x15:
                        case 0x16:
                            if (string.IsNullOrEmpty(str2))
                            {
                                str2 = string.Format("/{0}/index.aspx?lid={1}", rowArray[0]["dir_name"].ToString(), rowArray[0]["id"].ToString());
                            }
                            break;

                        case 1:
                        case 4:
                        case 6:
                        case 7:
                        case 11:
                        case 13:
                        case 0x11:
                        case 0x13:
                            str2 = string.Format("/{0}/index.aspx?lid={1}", rowArray[0]["dir_name"].ToString(), rowArray[0]["id"].ToString());
                            break;

                        case 5:
                            str2 = string.Format("/{0}/index.aspx?lid={1}", rowArray[0]["dir_name"].ToString(), rowArray[0]["id"].ToString());
                            break;
                    }
                    this.v_url = str2;
                }
            }
        }
    }
}

