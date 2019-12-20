namespace Agent.Web
{
    using Agent.Web.WebBase;
    using LotterySystem.Common;
    using LotterySystem.Model;
    using System;
    using System.Data;

    public class CreditInfo : MemberPageBase
    {
        protected DataTable car168_DT;
        protected DataTable cqsc_DT;
        protected DataTable happycar_DT;
        protected DataTable jscar_DT;
        protected DataTable jscqsc_DT;
        protected DataTable jsft2_DT;
        protected DataTable jsk3_DT;
        protected DataTable jspk10_DT;
        protected DataTable jssfc_DT;
        protected DataTable k8sc_DT;
        protected double kc_credit;
        protected string kc_iscash = "0";
        protected int kc_m_id;
        protected double kc_usable_credit;
        protected DataTable kl10_DT;
        protected DataTable kl8_DT;
        protected DataTable lotteryDT;
        protected string lotteryID = "";
        protected DataTable pcdd_DT;
        protected string pk_kc_a = "";
        protected string pk_kc_b = "";
        protected string pk_kc_c = "";
        protected string pk_six_a = "";
        protected string pk_six_b = "";
        protected string pk_six_c = "";
        protected DataTable pk10_DT;
        protected DataTable pkbjl_DT;
        protected double six_credit;
        protected DataTable six_DT;
        protected string six_iscash = "0";
        protected int six_m_id;
        protected double six_usable_credit;
        protected string skin = "";
        protected DataTable speed5_DT;
        protected DataTable ssc168_DT;
        protected DataTable vrcar_DT;
        protected DataTable vrssc_DT;
        protected DataTable xyft5_DT;
        protected DataTable xyftoa_DT;
        protected DataTable xyftsg_DT;
        protected DataTable xync_DT;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.lotteryDT = base.GetLotteryList();
            this.lotteryID = LSRequest.qq("lid");
            string str = this.Session["user_name"].ToString();
            agent_userinfo_session _session = this.Session[str + "lottery_session_user_info"] as agent_userinfo_session;
            this.skin = _session.get_u_skin();
            if (_session.get_u_type().Equals("zj"))
            {
                base.Response.Redirect("/MessagePage.aspx?code=u100057&url=&issuccess=1&isback=0");
                base.Response.End();
            }
            DataSet userCredit = CallBLL.cz_users_bll.GetUserCredit(this.Session["user_name"].ToString());
            if (((userCredit != null) && (userCredit.Tables.Count > 0)) && (userCredit.Tables[0].Rows.Count > 0))
            {
                this.six_credit = Convert.ToDouble(userCredit.Tables[0].Rows[0]["six_credit"]);
                this.six_usable_credit = Convert.ToDouble(userCredit.Tables[0].Rows[0]["six_usable_credit"]);
                this.six_iscash = userCredit.Tables[0].Rows[0]["six_iscash"].ToString();
                this.kc_credit = Convert.ToDouble(userCredit.Tables[0].Rows[0]["kc_credit"]);
                this.kc_usable_credit = Convert.ToDouble(userCredit.Tables[0].Rows[0]["kc_usable_credit"]);
                this.kc_iscash = userCredit.Tables[0].Rows[0]["kc_iscash"].ToString();
            }
            if (_session.get_six_kind().ToLower() != "0")
            {
                if (_session.get_six_kind().ToLower() != "a")
                {
                    this.pk_six_a = "display:none;";
                }
                if (_session.get_six_kind().ToLower() != "b")
                {
                    this.pk_six_b = "display:none;";
                }
                if (_session.get_six_kind().ToLower() != "c")
                {
                    this.pk_six_c = "display:none;";
                }
            }
            else
            {
                this.pk_six_a = "display:;";
                this.pk_six_b = "display:;";
                this.pk_six_c = "display:;";
            }
            if (_session.get_kc_kind().ToLower() != "0")
            {
                if (_session.get_kc_kind().ToLower() != "a")
                {
                    this.pk_kc_a = "display:none;";
                }
                if (_session.get_kc_kind().ToLower() != "b")
                {
                    this.pk_kc_b = "display:none;";
                }
                if (_session.get_kc_kind().ToLower() != "c")
                {
                    this.pk_kc_c = "display:none;";
                }
            }
            else
            {
                this.pk_kc_a = "display:;";
                this.pk_kc_b = "display:;";
                this.pk_kc_c = "display:;";
            }
            foreach (DataRow row in this.lotteryDT.Rows)
            {
                int num = Convert.ToInt32(row["id"].ToString());
                int num2 = Convert.ToInt32(row["master_id"].ToString());
                if (num2.Equals(1))
                {
                    this.six_m_id = num2;
                }
                if (num2.Equals(2))
                {
                    this.kc_m_id = num2;
                }
                if (num.Equals(0))
                {
                    DataSet playByName = CallBLL.cz_play_kl10_bll.GetPlayByName(this.Session["user_name"].ToString());
                    if (((playByName != null) && (playByName.Tables.Count > 0)) && (playByName.Tables[0].Rows.Count > 0))
                    {
                        this.kl10_DT = playByName.Tables[0];
                    }
                }
                if (num.Equals(1))
                {
                    DataSet set3 = CallBLL.cz_play_cqsc_bll.GetPlayByName(this.Session["user_name"].ToString());
                    if (((set3 != null) && (set3.Tables.Count > 0)) && (set3.Tables[0].Rows.Count > 0))
                    {
                        this.cqsc_DT = set3.Tables[0];
                    }
                }
                if (num.Equals(2))
                {
                    DataSet set4 = CallBLL.cz_play_pk10_bll.GetPlayByName(this.Session["user_name"].ToString());
                    if (((set4 != null) && (set4.Tables.Count > 0)) && (set4.Tables[0].Rows.Count > 0))
                    {
                        this.pk10_DT = set4.Tables[0];
                    }
                }
                if (num.Equals(3))
                {
                    DataSet set5 = CallBLL.cz_play_xync_bll.GetPlayByName(this.Session["user_name"].ToString());
                    if (((set5 != null) && (set5.Tables.Count > 0)) && (set5.Tables[0].Rows.Count > 0))
                    {
                        this.xync_DT = set5.Tables[0];
                    }
                }
                if (num.Equals(4))
                {
                    DataSet set6 = CallBLL.cz_play_jsk3_bll.GetPlayByName(this.Session["user_name"].ToString());
                    if (((set6 != null) && (set6.Tables.Count > 0)) && (set6.Tables[0].Rows.Count > 0))
                    {
                        this.jsk3_DT = set6.Tables[0];
                    }
                }
                if (num.Equals(5))
                {
                    DataSet set7 = CallBLL.cz_play_kl8_bll.GetPlayByName(this.Session["user_name"].ToString());
                    if (((set7 != null) && (set7.Tables.Count > 0)) && (set7.Tables[0].Rows.Count > 0))
                    {
                        this.kl8_DT = set7.Tables[0];
                    }
                }
                if (num.Equals(6))
                {
                    DataSet set8 = CallBLL.cz_play_k8sc_bll.GetPlayByName(this.Session["user_name"].ToString());
                    if (((set8 != null) && (set8.Tables.Count > 0)) && (set8.Tables[0].Rows.Count > 0))
                    {
                        this.k8sc_DT = set8.Tables[0];
                    }
                }
                if (num.Equals(7))
                {
                    DataSet set9 = CallBLL.cz_play_pcdd_bll.GetPlayByName(this.Session["user_name"].ToString());
                    if (((set9 != null) && (set9.Tables.Count > 0)) && (set9.Tables[0].Rows.Count > 0))
                    {
                        this.pcdd_DT = set9.Tables[0];
                    }
                }
                if (num.Equals(100))
                {
                    DataSet set10 = null;
                    if (!FileCacheHelper.get_IsShowLM_B())
                    {
                        set10 = CallBLL.cz_play_six_bll.GetPlayByName(this.Session["user_name"].ToString(), "91060,91061,91062,91063,91064,91065");
                    }
                    else
                    {
                        set10 = CallBLL.cz_play_six_bll.GetPlayByName(this.Session["user_name"].ToString());
                    }
                    if (((set10 != null) && (set10.Tables.Count > 0)) && (set10.Tables[0].Rows.Count > 0))
                    {
                        this.six_DT = set10.Tables[0];
                    }
                }
                if (num.Equals(9))
                {
                    DataSet set11 = CallBLL.cz_play_xyft5_bll.GetPlayByName(this.Session["user_name"].ToString());
                    if (((set11 != null) && (set11.Tables.Count > 0)) && (set11.Tables[0].Rows.Count > 0))
                    {
                        this.xyft5_DT = set11.Tables[0];
                    }
                }
                if (num.Equals(8))
                {
                    DataSet set12 = CallBLL.cz_play_pkbjl_bll.GetPlayByName(this.Session["user_name"].ToString());
                    if (((set12 != null) && (set12.Tables.Count > 0)) && (set12.Tables[0].Rows.Count > 0))
                    {
                        this.pkbjl_DT = set12.Tables[0];
                    }
                }
                if (num.Equals(10))
                {
                    DataSet set13 = CallBLL.cz_play_jscar_bll.GetPlayByName(this.Session["user_name"].ToString());
                    if (((set13 != null) && (set13.Tables.Count > 0)) && (set13.Tables[0].Rows.Count > 0))
                    {
                        this.jscar_DT = set13.Tables[0];
                    }
                }
                if (num.Equals(11))
                {
                    DataSet set14 = CallBLL.cz_play_speed5_bll.GetPlayByName(this.Session["user_name"].ToString());
                    if (((set14 != null) && (set14.Tables.Count > 0)) && (set14.Tables[0].Rows.Count > 0))
                    {
                        this.speed5_DT = set14.Tables[0];
                    }
                }
                if (num.Equals(13))
                {
                    DataSet set15 = CallBLL.cz_play_jscqsc_bll.GetPlayByName(this.Session["user_name"].ToString());
                    if (((set15 != null) && (set15.Tables.Count > 0)) && (set15.Tables[0].Rows.Count > 0))
                    {
                        this.jscqsc_DT = set15.Tables[0];
                    }
                }
                if (num.Equals(12))
                {
                    DataSet set16 = CallBLL.cz_play_jspk10_bll.GetPlayByName(this.Session["user_name"].ToString());
                    if (((set16 != null) && (set16.Tables.Count > 0)) && (set16.Tables[0].Rows.Count > 0))
                    {
                        this.jspk10_DT = set16.Tables[0];
                    }
                }
                if (num.Equals(14))
                {
                    DataSet set17 = CallBLL.cz_play_jssfc_bll.GetPlayByName(this.Session["user_name"].ToString());
                    if (((set17 != null) && (set17.Tables.Count > 0)) && (set17.Tables[0].Rows.Count > 0))
                    {
                        this.jssfc_DT = set17.Tables[0];
                    }
                }
                if (num.Equals(15))
                {
                    DataSet set18 = CallBLL.cz_play_jsft2_bll.GetPlayByName(this.Session["user_name"].ToString());
                    if (((set18 != null) && (set18.Tables.Count > 0)) && (set18.Tables[0].Rows.Count > 0))
                    {
                        this.jsft2_DT = set18.Tables[0];
                    }
                }
                if (num.Equals(0x10))
                {
                    DataSet set19 = CallBLL.cz_play_car168_bll.GetPlayByName(this.Session["user_name"].ToString());
                    if (((set19 != null) && (set19.Tables.Count > 0)) && (set19.Tables[0].Rows.Count > 0))
                    {
                        this.car168_DT = set19.Tables[0];
                    }
                }
                if (num.Equals(0x11))
                {
                    DataSet set20 = CallBLL.cz_play_ssc168_bll.GetPlayByName(this.Session["user_name"].ToString());
                    if (((set20 != null) && (set20.Tables.Count > 0)) && (set20.Tables[0].Rows.Count > 0))
                    {
                        this.ssc168_DT = set20.Tables[0];
                    }
                }
                if (num.Equals(0x12))
                {
                    DataSet set21 = CallBLL.cz_play_vrcar_bll.GetPlayByName(this.Session["user_name"].ToString());
                    if (((set21 != null) && (set21.Tables.Count > 0)) && (set21.Tables[0].Rows.Count > 0))
                    {
                        this.vrcar_DT = set21.Tables[0];
                    }
                }
                if (num.Equals(0x13))
                {
                    DataSet set22 = CallBLL.cz_play_vrssc_bll.GetPlayByName(this.Session["user_name"].ToString());
                    if (((set22 != null) && (set22.Tables.Count > 0)) && (set22.Tables[0].Rows.Count > 0))
                    {
                        this.vrssc_DT = set22.Tables[0];
                    }
                }
                if (num.Equals(20))
                {
                    DataSet set23 = CallBLL.cz_play_xyftoa_bll.GetPlayByName(this.Session["user_name"].ToString());
                    if (((set23 != null) && (set23.Tables.Count > 0)) && (set23.Tables[0].Rows.Count > 0))
                    {
                        this.xyftoa_DT = set23.Tables[0];
                    }
                }
                if (num.Equals(0x15))
                {
                    DataSet set24 = CallBLL.cz_play_xyftsg_bll.GetPlayByName(this.Session["user_name"].ToString());
                    if (((set24 != null) && (set24.Tables.Count > 0)) && (set24.Tables[0].Rows.Count > 0))
                    {
                        this.xyftsg_DT = set24.Tables[0];
                    }
                }
                if (num.Equals(0x16))
                {
                    DataSet set25 = CallBLL.cz_play_happycar_bll.GetPlayByName(this.Session["user_name"].ToString());
                    if (((set25 != null) && (set25.Tables.Count > 0)) && (set25.Tables[0].Rows.Count > 0))
                    {
                        this.happycar_DT = set25.Tables[0];
                    }
                }
            }
        }
    }
}

