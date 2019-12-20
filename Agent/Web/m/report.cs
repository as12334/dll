namespace Agent.Web.M
{
    using Agent.Web.WebBase;
    using LotterySystem.BLL;
    using LotterySystem.Common;
    using LotterySystem.DBUtility;
    using LotterySystem.Model;
    using LotterySystem.WebPageBase;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Text;
    using System.Web;

    public class report : MemberPageBase_Mobile
    {
        private int pageSize = 5;

        private void getPlayName(HttpContext context, ref string strResult)
        {
            base.checkLoginByHandler(0);
            ReturnResult result = new ReturnResult();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            result.set_success(200);
            int num = Convert.ToInt32(LSRequest.qq("lid"));
            string str2 = DateTime.Now.AddHours((double) -int.Parse(base.get_BetTime_KC())).ToString("yyyy-MM-dd");
            DataTable phaseByQueryDate = null;
            DataTable play = null;
            switch (num)
            {
                case 0:
                    phaseByQueryDate = CallBLL.cz_phase_kl10_bll.GetPhaseByQueryDate(str2);
                    play = CallBLL.cz_play_kl10_bll.GetPlay();
                    break;

                case 1:
                    phaseByQueryDate = CallBLL.cz_phase_cqsc_bll.GetPhaseByQueryDate(str2);
                    play = CallBLL.cz_play_cqsc_bll.GetPlay();
                    break;

                case 2:
                    phaseByQueryDate = CallBLL.cz_phase_pk10_bll.GetPhaseByQueryDate(str2);
                    play = CallBLL.cz_play_pk10_bll.GetPlay();
                    break;

                case 3:
                    phaseByQueryDate = CallBLL.cz_phase_xync_bll.GetPhaseByQueryDate(str2);
                    play = CallBLL.cz_play_xync_bll.GetPlay();
                    break;

                case 4:
                    phaseByQueryDate = CallBLL.cz_phase_jsk3_bll.GetPhaseByQueryDate(str2);
                    play = CallBLL.cz_play_jsk3_bll.GetPlay();
                    break;

                case 5:
                    phaseByQueryDate = CallBLL.cz_phase_kl8_bll.GetPhaseByQueryDate(str2);
                    play = CallBLL.cz_play_kl8_bll.GetPlay();
                    break;

                case 6:
                    phaseByQueryDate = CallBLL.cz_phase_k8sc_bll.GetPhaseByQueryDate(str2);
                    play = CallBLL.cz_play_k8sc_bll.GetPlay();
                    break;

                case 7:
                    phaseByQueryDate = CallBLL.cz_phase_pcdd_bll.GetPhaseByQueryDate(str2);
                    play = CallBLL.cz_play_pcdd_bll.GetPlay();
                    break;

                case 8:
                    phaseByQueryDate = CallBLL.cz_phase_pkbjl_bll.GetPhaseByQueryDate(str2);
                    play = CallBLL.cz_play_pkbjl_bll.GetPlay();
                    break;

                case 9:
                    phaseByQueryDate = CallBLL.cz_phase_xyft5_bll.GetPhaseByQueryDate(str2);
                    play = CallBLL.cz_play_xyft5_bll.GetPlay();
                    break;

                case 100:
                    phaseByQueryDate = CallBLL.cz_phase_six_bll.GetCurrentPhase("20");
                    play = CallBLL.cz_play_six_bll.GetPlay();
                    break;

                default:
                    strResult = base.ObjectToJson(result);
                    return;
            }
            StringBuilder builder = new StringBuilder();
            StringBuilder builder2 = new StringBuilder();
            if (phaseByQueryDate != null)
            {
                if (num.Equals(8))
                {
                    Dictionary<string, string> source = new Dictionary<string, string>();
                    for (int i = 0; i < phaseByQueryDate.Rows.Count; i++)
                    {
                        string str3 = phaseByQueryDate.Rows[i]["p_id"].ToString();
                        string key = phaseByQueryDate.Rows[i]["phase"].ToString();
                        if (!source.ContainsKey(key))
                        {
                            source.Add(key, str3);
                        }
                    }
                    for (int j = 0; j < source.Count; j++)
                    {
                        if (j == 0)
                        {
                            builder.Append(source.ElementAt<KeyValuePair<string, string>>(j).Value + "," + source.ElementAt<KeyValuePair<string, string>>(j).Key + " 期");
                        }
                        else
                        {
                            builder.Append("|" + source.ElementAt<KeyValuePair<string, string>>(j).Value + "," + source.ElementAt<KeyValuePair<string, string>>(j).Key + " 期");
                        }
                    }
                }
                else
                {
                    for (int k = 0; k < phaseByQueryDate.Rows.Count; k++)
                    {
                        if (k == 0)
                        {
                            builder.Append(phaseByQueryDate.Rows[k]["p_id"].ToString().Trim() + "," + phaseByQueryDate.Rows[k]["phase"].ToString().Trim() + " 期");
                        }
                        else
                        {
                            builder.Append("|" + phaseByQueryDate.Rows[k]["p_id"].ToString().Trim() + "," + phaseByQueryDate.Rows[k]["phase"].ToString().Trim() + " 期");
                        }
                    }
                }
                dictionary.Add("phaseoption", builder.ToString());
            }
            if (play != null)
            {
                for (int m = 0; m < play.Rows.Count; m++)
                {
                    if (m == 0)
                    {
                        builder2.Append(play.Rows[m]["play_id"].ToString().Trim() + "," + play.Rows[m]["play_name"].ToString().Trim());
                    }
                    else
                    {
                        builder2.Append("|" + play.Rows[m]["play_id"].ToString().Trim() + "," + play.Rows[m]["play_name"].ToString().Trim());
                    }
                }
                dictionary.Add("playoption", builder2.ToString());
            }
            if (num.Equals(100))
            {
                if (CallBLL.cz_phase_six_bll.IsOpenPhase())
                {
                    dictionary.Add("t_Balance", "0");
                }
                else
                {
                    dictionary.Add("t_Balance", "1");
                }
            }
            else
            {
                dictionary.Add("t_Balance", "1");
            }
            result.set_data(dictionary);
            strResult = JsonHandle.ObjectToJson(result);
        }

        private void getQueryDate(HttpContext context, ref string strResult)
        {
            base.checkLoginByHandler(0);
            ReturnResult result = new ReturnResult();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            result.set_success(200);
            DateTime time = DateTime.Now.AddHours(-7.0);
            int num = Convert.ToInt32(time.DayOfWeek.ToString("d"));
            if (num == 0)
            {
                num = 7;
            }
            DateTime time2 = time.AddDays(-1.0);
            DateTime time3 = time.AddDays((double) (1 - num));
            DateTime time4 = time3.AddDays(6.0);
            DateTime time5 = time3.AddDays(-1.0);
            DateTime time6 = time5.AddDays(-6.0);
            DateTime time7 = time.AddDays((double) (1 - time.Day));
            DateTime time8 = time7.AddMonths(1).AddDays(-1.0);
            DateTime time9 = time7.AddDays(-1.0);
            DateTime time10 = time7.AddMonths(-1);
            string str = time.ToString("yyyy-MM-dd") + "|" + time.ToString("yyyy-MM-dd");
            dictionary.Add("today", str);
            str = time2.ToString("yyyy-MM-dd") + "|" + time2.ToString("yyyy-MM-dd");
            dictionary.Add("yesterday", str);
            str = time3.ToString("yyyy-MM-dd") + "|" + time4.ToString("yyyy-MM-dd");
            dictionary.Add("tweek", str);
            str = time6.ToString("yyyy-MM-dd") + "|" + time5.ToString("yyyy-MM-dd");
            dictionary.Add("lastweek", str);
            str = time7.ToString("yyyy-MM-dd") + "|" + time8.ToString("yyyy-MM-dd");
            dictionary.Add("tmonth", str);
            str = time10.ToString("yyyy-MM-dd") + "|" + time9.ToString("yyyy-MM-dd");
            dictionary.Add("lastmonth", str);
            result.set_data(dictionary);
            strResult = base.ObjectToJson(result);
        }

        protected string GetReportOpenDateKc()
        {
            return new cz_system_set_kc_exBLL().GetReportOpenDate();
        }

        private string GetReportOpenDateSix()
        {
            return CallBLL.cz_system_set_six_bll.GetSystemSet(100).get_ev_18();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            base.Response.Expires = 0;
            base.Response.CacheControl = "no-cache";
            string str = LSRequest.qq("action").Trim();
            string strResult = "";
            string str7 = str;
            if (str7 != null)
            {
                if (!(str7 == "getPlayName"))
                {
                    if (str7 == "getQueryDate")
                    {
                        this.getQueryDate(HttpContext.Current, ref strResult);
                    }
                    else if (str7 == "SearchDataSix")
                    {
                        if (base.q("ReportType").ToUpper().Equals("B"))
                        {
                            this.ReportDataSix_B();
                        }
                        else
                        {
                            this.ReportDataSix_T();
                        }
                    }
                    else if (str7 == "SearchDetailSix")
                    {
                        if (base.q("ReportType").ToUpper().Equals("B"))
                        {
                            this.ReportDetailSix_B();
                        }
                        else
                        {
                            this.ReportDetailSix_T();
                        }
                    }
                    else if (str7 == "SearchDataKc")
                    {
                        if (base.q("ReportType").ToUpper().Equals("B"))
                        {
                            this.ReportDataKc_B();
                        }
                        else
                        {
                            this.ReportDataKc_T();
                        }
                    }
                    else if (str7 == "SearchDetailKc")
                    {
                        if (base.q("ReportType").ToUpper().Equals("B"))
                        {
                            this.ReportDetailKc_B();
                        }
                        else
                        {
                            this.ReportDetailKc_T();
                        }
                    }
                }
                else
                {
                    this.getPlayName(HttpContext.Current, ref strResult);
                }
            }
            base.OutJson(strResult);
        }

        public void ReportDataKc_B()
        {
            agent_userinfo_session _session = this.Session[this.Session["user_name"] + "lottery_session_user_info"] as agent_userinfo_session;
            cz_users userInfoByUID = null;
            string str = LSRequest.qq("uid");
            if (!string.IsNullOrEmpty(str))
            {
                userInfoByUID = CallBLL.cz_users_bll.GetUserInfoByUID(str);
                if ((!_session.get_u_type().Equals("fgs") && !_session.get_allow_view_report().Equals(1)) && !base.IsUpperLowerLevels(userInfoByUID.get_u_name(), _session.get_u_type(), _session.get_u_name()))
                {
                    base.noRightOptMsg(PageBase.GetMessageByCache("u100057", "MessageHint"));
                    return;
                }
            }
            if ((_session.get_u_type().ToLower().Equals("zj") && (_session.get_users_child_session() != null)) && (_session.get_users_child_session().get_permissions_name().IndexOf("po_4_1") < 0))
            {
                base.noRightOptMsg(PageBase.GetMessageByCache("u100057", "MessageHint"));
            }
            else if ((!_session.get_u_type().ToLower().Equals("zj") && (_session.get_users_child_session() != null)) && (_session.get_users_child_session().get_permissions_name().IndexOf("po_7_1") < 0))
            {
                base.noRightOptMsg(PageBase.GetMessageByCache("u100057", "MessageHint"));
            }
            else
            {
                List<SqlParameter> list = new List<SqlParameter>();
                SqlParameter item = new SqlParameter();
                DateTime now = DateTime.Now;
                int num = int.Parse(now.Hour.ToString());
                int num2 = int.Parse(now.Minute.ToString());
                if (((num == 6) && (num2 >= 20)) && (num2 < 0x2d))
                {
                    base.noRightOptMsg(PageBase.GetMessageByCache("u100064", "MessageHint"));
                }
                else
                {
                    string str2 = "";
                    string str3 = "";
                    string str4 = "";
                    string str6 = "";
                    string str7 = base.q("t_LT");
                    if (str7.Equals("-1"))
                    {
                        str7 = "";
                    }
                    string str8 = base.q("gID");
                    string str9 = base.q("t_FT");
                    string str10 = base.q("t_LID");
                    if (str10.Equals("0"))
                    {
                        str10 = "";
                    }
                    string str11 = base.q("BeginDate");
                    string str12 = base.q("EndDate");
                    string str13 = base.q("t_Balance");
                    double num3 = 0.0;
                    string str14 = "0";
                    string str15 = "";
                    if (_session.get_u_type() == "fgs")
                    {
                        str14 = _session.get_allow_view_report().ToString();
                        str15 = _session.get_sup_name();
                    }
                    if (userInfoByUID == null)
                    {
                        str2 = _session.get_u_type();
                        if (str2 == "fgs")
                        {
                            if (str14 == "1")
                            {
                                str4 = str15;
                                str2 = "zj";
                            }
                            else
                            {
                                str4 = _session.get_u_name();
                            }
                        }
                        else
                        {
                            str4 = _session.get_u_name();
                        }
                    }
                    else
                    {
                        str4 = userInfoByUID.get_u_name();
                        str2 = userInfoByUID.get_u_type();
                    }
                    if ((str14 != "1") && !base.IsUpperLowerLevels(str4, _session.get_u_type(), _session.get_u_name()))
                    {
                        base.noRightOptMsg(PageBase.GetMessageByCache("u100057", "MessageHint"));
                    }
                    else
                    {
                        string str16 = "";
                        string str17 = "";
                        string str18 = "";
                        string str19 = "";
                        string str20 = "";
                        string str21 = "";
                        string str33 = str2;
                        if (str33 != null)
                        {
                            if (!(str33 == "zj"))
                            {
                                if (str33 == "fgs")
                                {
                                    str3 = "gd_name";
                                    str18 = "gd_drawback";
                                    str19 = "fgs_drawback";
                                    str17 = "fgs_name";
                                    str20 = "(100-(gd_rate+zd_rate+dl_rate))/100";
                                    str21 = "(100-(fgs_rate+gd_rate+zd_rate+dl_rate))/100";
                                    str16 = "gd";
                                }
                                else if (str33 == "gd")
                                {
                                    str3 = "zd_name";
                                    str18 = "zd_drawback";
                                    str19 = "gd_drawback";
                                    str17 = "gd_name";
                                    str20 = "(100-(zd_rate+dl_rate))/100";
                                    str21 = "(100-(gd_rate+zd_rate+dl_rate))/100";
                                    str16 = "zd";
                                }
                                else if (str33 == "zd")
                                {
                                    str3 = "dl_name";
                                    str18 = "dl_drawback";
                                    str19 = "zd_drawback";
                                    str17 = "zd_name";
                                    str20 = "(100-dl_rate)/100";
                                    str21 = "(100-(zd_rate+dl_rate))/100";
                                    str16 = "dl";
                                }
                                else if (str33 == "dl")
                                {
                                    str3 = "u_name";
                                    str18 = "hy_drawback";
                                    str19 = "dl_drawback";
                                    str17 = "dl_name";
                                    str20 = "1";
                                    str21 = "(100-dl_rate)/100";
                                    str16 = "hy";
                                }
                            }
                            else
                            {
                                str3 = "fgs_name";
                                str18 = "fgs_drawback";
                                str19 = "zj_drawback";
                                str20 = "(100-(fgs_rate+gd_rate+zd_rate+dl_rate))/100";
                                str21 = "0";
                            }
                        }
                        if (str17 != "")
                        {
                            str17 = " and " + str17 + "='" + str4 + "'";
                        }
                        str11 = base.q("BeginDate");
                        str12 = base.q("EndDate");
                        DateTime time2 = DateTime.Now;
                        DateTime time3 = Convert.ToDateTime("2015-07-01");
                        Convert.ToDateTime(time2.ToString("yyyy-MM-dd"));
                        DateTime time4 = time3;
                        DateTime time5 = Convert.ToDateTime(str11);
                        DateTime time6 = Convert.ToDateTime(str12);
                        if (!str9.Equals("0") && !string.IsNullOrEmpty(this.GetReportOpenDateKc()))
                        {
                            DateTime time7 = Convert.ToDateTime(this.GetReportOpenDateKc());
                            if (time5 < time7)
                            {
                                str11 = time7.ToString("yyyy-MM-dd");
                                base.noRightOptMsg(string.Format(PageBase.GetMessageByCache("u100065", "MessageHint"), str11));
                                return;
                            }
                        }
                        if (time4 > time5)
                        {
                            str11 = time4.ToString("yyyy-MM-dd");
                            base.noRightOptMsg(string.Format(PageBase.GetMessageByCache("u100066", "MessageHint"), str11));
                        }
                        else
                        {
                            if (time4 > time6)
                            {
                                str12 = time4.ToString("yyyy-MM-dd");
                            }
                            string str22 = "";
                            string str23 = DateTime.Now.AddHours((double) -int.Parse(base.get_BetTime_KC())).ToString("yyyy-MM-dd");
                            str22 = "cz_bet_kc   with(NOLOCK) ";
                            if ((str23 == str11) && (str23 == str12))
                            {
                                str22 = "cz_bet_kc   with(NOLOCK)  ";
                            }
                            else
                            {
                                str22 = "cz_betold_kc   with(NOLOCK)  ";
                            }
                            string str24 = "";
                            if (str7 != "")
                            {
                                str24 = str24 + " and lottery_type=@lottery_type ";
                                item = new SqlParameter("@lottery_type", SqlDbType.NVarChar) {
                                    Value = str7
                                };
                                list.Add(item);
                            }
                            if (str8 != "0")
                            {
                                str24 = str24 + " and play_id=@play_id  ";
                                item = new SqlParameter("@play_id", SqlDbType.NVarChar) {
                                    Value = str8
                                };
                                list.Add(item);
                            }
                            if (str13 == "0")
                            {
                                str24 = str24 + " and is_payment=0 ";
                            }
                            else
                            {
                                str24 = str24 + " and is_payment=1 ";
                            }
                            if (str9 == "0")
                            {
                                if (str10 != "")
                                {
                                    str24 = str24 + " and phase_id=@phase_id  ";
                                    item = new SqlParameter("@phase_id", SqlDbType.NVarChar) {
                                        Value = str10
                                    };
                                    list.Add(item);
                                }
                                if (str9.Equals("0") && base.q("isQS").Equals("0"))
                                {
                                    base.noRightOptMsg("無當天查詢的獎期!");
                                    return;
                                }
                            }
                            else
                            {
                                DateTime time9 = Convert.ToDateTime(str12);
                                DateTime time10 = DateTime.Now;
                                time10 = time9.AddDays(1.0);
                                str24 = str24 + " and bet_time between @BeginDate and @EndDatejtdate ";
                                item = new SqlParameter("@BeginDate", SqlDbType.NVarChar) {
                                    Value = str11 + " 06:30:00"
                                };
                                list.Add(item);
                                item = new SqlParameter("@EndDatejtdate", SqlDbType.NVarChar) {
                                    Value = time10.ToString("yyyy-MM-dd") + " 06:30:00"
                                };
                                list.Add(item);
                            }
                            string str25 = "";
                            string str26 = "";
                            string str27 = "profit";
                            if (str2 != "zj")
                            {
                                if (str2 == "fgs")
                                {
                                    str27 = "profit_zj";
                                }
                                str25 = "select count(bet_id) as bs,sum(" + str21 + "* (CASE WHEN (isTie=1) THEN 0 ELSE amount END) ) uphl,sum(CASE WHEN sale_type=1 THEN 0 ELSE (CASE WHEN (isTie=1) THEN 0 ELSE amount END)  END)as hyzt,  ISNULL(sum(CASE WHEN  sale_type=1 THEN 0 ELSE profit END),'0') as hyjg , sum((CASE WHEN (isTie=1) THEN 0 ELSE amount END) *" + str2 + "_rate/100)as szzt,  ISNULL(sum(profit*" + str2 + "_rate/100),'0')as szjg,ISNULL(sum(" + str27 + "*" + str21 + "),'0')as upjg ,ISNULL(sum(profit*" + str20 + "),0) as ysze,ISNULL(abs(sum((CASE WHEN (isTie=1) THEN 0 ELSE amount END) * " + str20 + " * (CASE WHEN is_payment=0 THEN 0 ELSE " + str18 + "/100 END))) ,'0') as ysds,ISNULL(abs(sum((CASE WHEN (isTie=1) THEN 0 ELSE amount END) * " + str21 + " * (CASE WHEN is_payment=0 THEN 0 ELSE " + str19 + "/100 END))) ,'0') as mygetds,ISNULL(abs(sum((CASE WHEN (isTie=1) THEN 0 ELSE amount END) * " + str21 + " * (CASE WHEN is_payment=0 THEN 0 ELSE " + str18 + "/100 END))) ,'0') as mypayds,ISNULL(abs(sum((CASE WHEN (isTie=1) THEN 0 ELSE amount END) * " + str2 + "_rate/100  * (CASE WHEN is_payment=0 THEN 0 ELSE " + str18 + "/100 END))) ,'0') as szds,max(m_type) m_type," + str3 + " from  ( select * from  " + str22 + "  where   isDelete=0 " + str24 + str17 + " and u_name!='" + str4 + "'" + str6 + " ) as bet group by " + str3;
                                str26 = "select ISNULL(sum((CASE WHEN (isTie=1) THEN 0 ELSE amount END) *" + str2 + "_rate/100),'0') as szzt from  " + str22 + "  where  isDelete=0 " + str24 + str17 + " and u_name!='" + str4 + "'" + str6;
                            }
                            else
                            {
                                str25 = "select count(bet_id) as bs,sum(" + str21 + "* (CASE WHEN (isTie=1) THEN 0 ELSE amount END) ) uphl,sum(CASE WHEN sale_type=1 THEN 0 ELSE (CASE WHEN (isTie=1) THEN 0 ELSE amount END)  END)as hyzt,  ISNULL(sum(CASE WHEN  sale_type=1 THEN 0 ELSE profit END),'0') as hyjg , sum((CASE WHEN (isTie=1) THEN 0 ELSE amount END) *" + str2 + "_rate/100)as szzt,  ISNULL(sum(profit_zj*" + str2 + "_rate/100),'0')as szjg,ISNULL(sum(profit_zj*" + str21 + "),'0')as upjg ,ISNULL(sum(profit_zj*" + str20 + "),0) as ysze,ISNULL(abs(sum((CASE WHEN (isTie=1) THEN 0 ELSE amount END) * " + str20 + " * (CASE WHEN is_payment=0 THEN 0 ELSE " + str18 + "/100 END))) ,'0') as ysds,ISNULL(abs(sum((CASE WHEN (isTie=1) THEN 0 ELSE amount END) * " + str2 + "_rate/100 * (CASE WHEN is_payment=0 THEN 0 ELSE " + str19 + "/100 END))) ,'0') as mygetds,ISNULL(abs(sum((CASE WHEN (isTie=1) THEN 0 ELSE amount END) * " + str2 + "_rate/100 * (CASE WHEN is_payment=0 THEN 0 ELSE " + str18 + "/100 END))) ,'0') as mypayds,ISNULL(abs(sum((CASE WHEN (isTie=1) THEN 0 ELSE amount END) * " + str2 + "_rate/100  * (CASE WHEN is_payment=0 THEN 0 ELSE " + str18 + "/100 END))) ,'0') as szds,max(m_type) m_type," + str3 + " from  ( select * from   " + str22 + "  where  isDelete=0 " + str24 + str17 + " and fgs_name!='" + str4 + "'" + str6 + " ) as bet group by " + str3;
                                str26 = "select ISNULL(sum((CASE WHEN (isTie=1) THEN 0 ELSE amount END) *" + str2 + "_rate/100),'0') as szzt,ISNULL(sum(profit_zj*" + str2 + "_rate/100),'0')as szjg,ISNULL(abs(sum((CASE WHEN (isTie=1) THEN 0 ELSE amount END) * zj_rate/100 * (CASE WHEN is_payment=0 THEN 0 ELSE fgs_drawback/100 END))) ,'0') as ysds  from  " + str22 + "  where  isDelete=0 " + str24 + str17 + " and fgs_name!='" + str4 + "'" + str6;
                            }
                            if (str25 != "")
                            {
                                if (str2 == "zj")
                                {
                                    str25 = "select  a.*,cz_users.u_nicker a_t_name,cz_users.u_id,ISNULL(cz_users.u_type,'') a_m_type  from cz_users inner join (" + str25 + ") a on a." + str3 + "=cz_users.u_name  order by a_t_name";
                                }
                                else
                                {
                                    str25 = "select  a.*,cz_users.u_nicker a_t_name,cz_users.u_id,ISNULL(cz_users.u_type,'') a_m_type  from cz_users inner join (" + str25 + ") a on a." + str3 + "=cz_users.u_name  order by " + str3;
                                }
                            }
                            DateTime time11 = DateTime.Now;
                            if (str12.Trim() == "")
                            {
                                str11 = time11.ToString("yyyy-MM-dd");
                                str12 = time11.ToString("yyyy-MM-dd");
                            }
                            int num4 = Convert.ToInt32(time11.DayOfWeek.ToString("d"));
                            if (num4 == 0)
                            {
                                num4 = 7;
                            }
                            DateTime time12 = time11.AddDays((double) (1 - num4));
                            time12.AddDays(6.0);
                            time12.AddDays(-1.0).AddDays(-6.0);
                            DateTime time14 = time11.AddDays((double) (1 - time11.Day));
                            time14.AddMonths(1).AddDays(-1.0);
                            time14.AddDays(-1.0);
                            time14.AddMonths(-1);
                            if (str2 == "zj")
                            {
                                double num5 = 0.0;
                                double num6 = 0.0;
                                double num7 = 0.0;
                                double num8 = 0.0;
                                double num9 = 0.0;
                                double num10 = 0.0;
                                double num11 = 0.0;
                                double num12 = 0.0;
                                double num13 = 0.0;
                                double num14 = 0.0;
                                double num15 = 0.0;
                                double num16 = 0.0;
                                int num17 = 0;
                                double num18 = 0.0;
                                double num19 = 0.0;
                                double num20 = 0.0;
                                double num21 = 0.0;
                                double num22 = 0.0;
                                double num23 = 0.0;
                                double num24 = 0.0;
                                double num25 = 0.0;
                                double num26 = 0.0;
                                double num27 = 0.0;
                                int num28 = 0;
                                DataTable table = DbHelperSQL.Query(str26, list.ToArray()).Tables[0];
                                double num29 = Math.Round(double.Parse(table.Rows[0][0].ToString()), 1);
                                Math.Round((double) (double.Parse(table.Rows[0][1].ToString()) + double.Parse(table.Rows[0][2].ToString())), 1);
                                table = DbHelperSQL.Query(str25, list.ToArray()).Tables[0];
                                num5 = 0.0;
                                num6 = 0.0;
                                num7 = 0.0;
                                num8 = 0.0;
                                num9 = 0.0;
                                num10 = 0.0;
                                num11 = 0.0;
                                num12 = 0.0;
                                num13 = 0.0;
                                num14 = 0.0;
                                num15 = 0.0;
                                num16 = 0.0;
                                num17 = 0;
                                num18 = 0.0;
                                num19 = 0.0;
                                num20 = 0.0;
                                num21 = 0.0;
                                num22 = 0.0;
                                num23 = 0.0;
                                num24 = 0.0;
                                num25 = 0.0;
                                num26 = 0.0;
                                num27 = 0.0;
                                num28 = 0;
                                Dictionary<string, object> data = new Dictionary<string, object>();
                                Dictionary<string, List<Dictionary<string, string>>> dictionary2 = new Dictionary<string, List<Dictionary<string, string>>>();
                                List<Dictionary<string, string>> list2 = new List<Dictionary<string, string>>();
                                List<Dictionary<string, string>> list3 = new List<Dictionary<string, string>>();
                                Dictionary<string, List<Dictionary<string, string>>> dictionary3 = new Dictionary<string, List<Dictionary<string, string>>>();
                                List<Dictionary<string, string>> list4 = new List<Dictionary<string, string>>();
                                foreach (DataRow row in table.Rows)
                                {
                                    Dictionary<string, string> dictionary4 = new Dictionary<string, string>();
                                    num17 = int.Parse(row["bs"].ToString());
                                    num5 = Math.Round(double.Parse(row["hyzt"].ToString()), 1);
                                    num6 = Math.Round(double.Parse(row["hyjg"].ToString()), 1);
                                    num11 = Math.Round(double.Parse(row["szzt"].ToString()), 1);
                                    num12 = Math.Round(double.Parse(row["szjg"].ToString()), 1);
                                    if (num5 > 0.0)
                                    {
                                        num10 = Math.Round((double) ((num11 / num5) * 100.0), 2);
                                    }
                                    else
                                    {
                                        num10 = 0.0;
                                    }
                                    num7 = Math.Round(double.Parse(row["ysze"].ToString()), 1);
                                    num8 = Math.Round(double.Parse(row["ysds"].ToString()), 1);
                                    num9 = Math.Round(double.Parse(row["szds"].ToString()), 1);
                                    num13 = Math.Round(double.Parse(row["mygetds"].ToString()), 1);
                                    num14 = Math.Round(double.Parse(row["mypayds"].ToString()), 1);
                                    if (num11 > 0.0)
                                    {
                                        num27 = Math.Round((double) ((num11 / num29) * 100.0), 2);
                                    }
                                    else
                                    {
                                        num27 = 0.0;
                                    }
                                    if (str2 == "dl")
                                    {
                                        dictionary4.Add("uname", row[str3].ToString());
                                    }
                                    else
                                    {
                                        dictionary4.Add("uname", row[str3].ToString());
                                        string str28 = string.Format("action=SearchDataKc&uid={0}&m_type=fgs&gID={1}&t_FT={2}&t_LID={3}&BeginDate={4}&EndDate={5}&t_Balance={6}&t_LT={7}&ReportType=B", new object[] { row["u_id"].ToString(), str8, base.q("t_FT"), str10, str11, str12, str13, str7 });
                                        dictionary4.Add("uname_href", str28);
                                    }
                                    dictionary4.Add("nicker", row["a_t_name"].ToString());
                                    dictionary4.Add("bs", num17.ToString());
                                    dictionary4.Add("yxje", string.Format("{0:F1}", num5));
                                    dictionary4.Add("hysy", string.Format("{0:F1}", num6));
                                    dictionary4.Add("ysxx", string.Format("{0:F1}", num7 + num8));
                                    dictionary4.Add("zc", num10.ToString() + "%");
                                    dictionary4.Add("szze", string.Format("{0:F1}", num11));
                                    dictionary4.Add("zhb", num27 + "%");
                                    dictionary4.Add("szsy", string.Format("{0:F1}", num12));
                                    dictionary4.Add("szts", string.Format("{0:F1}", num9));
                                    dictionary4.Add("szjg", string.Format("{0:F1}", num12 + num9));
                                    list2.Add(dictionary4);
                                    num28++;
                                    num18 += num5;
                                    num19 += num6;
                                    num20 += num7;
                                    num21 += num8;
                                    num22 += num9;
                                    num26 += num17;
                                    num24 += num11;
                                    num25 += num12;
                                    num15 += num13;
                                    num16 += num14;
                                }
                                if (num18 > 0.0)
                                {
                                    num23 = Math.Round((double) ((num24 / num18) * 100.0), 2);
                                }
                                else
                                {
                                    num23 = 0.0;
                                }
                                Dictionary<string, string> dictionary5 = new Dictionary<string, string>();
                                dictionary5.Add("uname", "合計：");
                                dictionary5.Add("nicker", "");
                                dictionary5.Add("bs", num26.ToString());
                                dictionary5.Add("yxje", string.Format("{0:F1}", num18));
                                dictionary5.Add("hysy", string.Format("{0:F1}", num19));
                                dictionary5.Add("ysxx", string.Format("{0:F1}", num20 + num21));
                                dictionary5.Add("zc", string.Format("{0:F1}", num23) + "%");
                                dictionary5.Add("szze", string.Format("{0:F1}", num24));
                                dictionary5.Add("zhb", "");
                                dictionary5.Add("szsy", string.Format("{0:F1}", num25));
                                dictionary5.Add("szts", string.Format("{0:F1}", num22));
                                dictionary5.Add("szjg", string.Format("{0:F1}", num25 + num22));
                                list3.Add(dictionary5);
                                dictionary2.Add("listreport", list2);
                                dictionary2.Add("listcount", list3);
                                data.Add("listdata", dictionary2);
                                num3 = num25 + num22;
                                double num30 = 0.0;
                                double num31 = 0.0;
                                Dictionary<string, string> dictionary6 = new Dictionary<string, string>();
                                dictionary6.Add("zcjg", string.Format("{0:F1}", num3));
                                dictionary6.Add("dkbhhjg", string.Format("{0:F1}", (num25 + -num30) + (num22 + -num31)));
                                list4.Add(dictionary6);
                                dictionary3.Add("listresult", list4);
                                data.Add("listdataresult", dictionary3);
                                data.Add("memberId", "fgs");
                                base.successOptMsg("交收報表查詢", data);
                            }
                            else
                            {
                                double num32 = 0.0;
                                double num33 = 0.0;
                                double num34 = 0.0;
                                double num35 = 0.0;
                                double num36 = 0.0;
                                double num37 = 0.0;
                                double num38 = 0.0;
                                double num39 = 0.0;
                                double num40 = 0.0;
                                double num41 = 0.0;
                                double num42 = 0.0;
                                int num43 = 0;
                                double num44 = 0.0;
                                double num45 = 0.0;
                                double num46 = 0.0;
                                double num47 = 0.0;
                                double num48 = 0.0;
                                double num49 = 0.0;
                                double num50 = 0.0;
                                double num51 = 0.0;
                                double num52 = 0.0;
                                double num53 = 0.0;
                                double num54 = 0.0;
                                double num55 = 0.0;
                                double num56 = 0.0;
                                double num57 = 0.0;
                                double num58 = 0.0;
                                double num59 = 0.0;
                                int num60 = 0;
                                double num61 = 0.0;
                                double num62 = 0.0;
                                DataTable table2 = DbHelperSQL.Query(str26, list.ToArray()).Tables[0];
                                double num63 = Math.Round(double.Parse(table2.Rows[0][0].ToString()), 1);
                                table2 = DbHelperSQL.Query(str25, list.ToArray()).Tables[0];
                                num32 = 0.0;
                                num33 = 0.0;
                                num34 = 0.0;
                                num35 = 0.0;
                                num36 = 0.0;
                                num37 = 0.0;
                                num38 = 0.0;
                                num39 = 0.0;
                                num40 = 0.0;
                                num41 = 0.0;
                                num42 = 0.0;
                                num43 = 0;
                                num44 = 0.0;
                                num45 = 0.0;
                                num46 = 0.0;
                                num47 = 0.0;
                                num48 = 0.0;
                                num49 = 0.0;
                                num50 = 0.0;
                                num51 = 0.0;
                                num52 = 0.0;
                                num53 = 0.0;
                                num54 = 0.0;
                                num55 = 0.0;
                                num56 = 0.0;
                                num57 = 0.0;
                                num58 = 0.0;
                                num59 = 0.0;
                                num60 = 0;
                                num61 = 0.0;
                                Dictionary<string, object> dictionary7 = new Dictionary<string, object>();
                                Dictionary<string, List<Dictionary<string, string>>> dictionary8 = new Dictionary<string, List<Dictionary<string, string>>>();
                                List<Dictionary<string, string>> list5 = new List<Dictionary<string, string>>();
                                List<Dictionary<string, string>> list6 = new List<Dictionary<string, string>>();
                                Dictionary<string, List<Dictionary<string, string>>> dictionary9 = new Dictionary<string, List<Dictionary<string, string>>>();
                                List<Dictionary<string, string>> list7 = new List<Dictionary<string, string>>();
                                new List<Dictionary<string, string>>();
                                new Dictionary<string, List<Dictionary<string, string>>>();
                                new List<Dictionary<string, string>>();
                                Dictionary<string, List<Dictionary<string, string>>> dictionary10 = new Dictionary<string, List<Dictionary<string, string>>>();
                                List<Dictionary<string, string>> list8 = new List<Dictionary<string, string>>();
                                foreach (DataRow row2 in table2.Rows)
                                {
                                    Dictionary<string, string> dictionary11 = new Dictionary<string, string>();
                                    num43 = int.Parse(row2["bs"].ToString());
                                    num32 = Math.Round(double.Parse(row2["hyzt"].ToString()), 1);
                                    num33 = Math.Round(double.Parse(row2["hyjg"].ToString()), 1);
                                    num38 = Math.Round(double.Parse(row2["szzt"].ToString()), 1);
                                    num39 = Math.Round(double.Parse(row2["szjg"].ToString()), 1);
                                    if (num32 > 0.0)
                                    {
                                        num37 = Math.Round((double) ((num38 / num32) * 100.0), 2);
                                    }
                                    else
                                    {
                                        num37 = 0.0;
                                    }
                                    num34 = Math.Round(double.Parse(row2["ysze"].ToString()), 1);
                                    num35 = Math.Round(double.Parse(row2["ysds"].ToString()), 1);
                                    num36 = Math.Round(double.Parse(row2["szds"].ToString()), 1);
                                    num40 = Math.Round(double.Parse(row2["mygetds"].ToString()), 1);
                                    num41 = Math.Round(double.Parse(row2["mypayds"].ToString()), 1);
                                    num42 = Math.Round(double.Parse(row2["upjg"].ToString()), 1);
                                    if (num38 > 0.0)
                                    {
                                        num59 = Math.Round((double) ((num38 / num63) * 100.0), 2);
                                    }
                                    else
                                    {
                                        num59 = 0.0;
                                    }
                                    string str29 = "";
                                    if ((str2 == "dl") && (row2["a_m_type"].ToString().Trim() == "hy"))
                                    {
                                        dictionary11.Add("uname", row2[str3].ToString());
                                    }
                                    else if ((((str2 == "zd") || (str2 == "gd")) || (str2 == "fgs")) && (row2["a_m_type"].ToString().Trim() == "hy"))
                                    {
                                        str29 = "zs";
                                    }
                                    else
                                    {
                                        dictionary11.Add("uname", row2[str3].ToString());
                                        string str30 = string.Format("action=SearchDataKc&uid={0}&m_type={1}&gID={2}&t_FT={3}&t_LID={4}&BeginDate={5}&EndDate={6}&t_Balance={7}&t_LT={8}&ReportType=B", new object[] { row2["u_id"], str16, str8, base.q("t_FT"), str10, str11, str12, str13, str7 });
                                        dictionary11.Add("uname_href", str30);
                                    }
                                    dictionary11.Add("hytype", str29);
                                    dictionary11.Add("nicker", row2["a_t_name"].ToString());
                                    dictionary11.Add("bs", num43.ToString());
                                    if ((str2 == "dl") || (row2["a_m_type"].ToString().Trim() == "hy"))
                                    {
                                        dictionary11.Add("yxje", string.Format("{0:F1}", num32));
                                        string str31 = string.Format("action=SearchDetailKc&userid={0}&gID={1}&m_type={2}&t_FT={3}&t_LID={4}&BeginDate={5}&EndDate={6}&t_Balance={7}&t_LT={8}&ReportType=B", new object[] { row2["u_id"], str8, str16, str9, str10, str11, str12, str13, str7 });
                                        dictionary11.Add("yxje_href", str31);
                                    }
                                    else
                                    {
                                        dictionary11.Add("yxje", string.Format("{0:F1}", num32));
                                    }
                                    dictionary11.Add("hysy", num33.ToString());
                                    dictionary11.Add("ysxx", string.Format("{0:F1}", num34 + num35));
                                    dictionary11.Add("zc", num37.ToString() + "%");
                                    dictionary11.Add("szze", string.Format("{0:F1}", num38));
                                    dictionary11.Add("zhb", num59.ToString() + "%");
                                    dictionary11.Add("szsy", string.Format("{0:F1}", num39));
                                    dictionary11.Add("szts", string.Format("{0:F1}", num36));
                                    dictionary11.Add("szjg", string.Format("{0:F1}", num39 + num36));
                                    dictionary11.Add("zqts", string.Format("{0:F1}", num41 - num40));
                                    if (str2 == "fgs")
                                    {
                                        dictionary11.Add("zqpl", string.Format("{0:F1}", (num34 - num39) - num42));
                                    }
                                    dictionary11.Add("sjjg", string.Format("{0:F1}", ((num39 + num36) + (num41 - num40)) + ((num34 - num39) - num42)));
                                    dictionary11.Add("gxsj", Math.Round(double.Parse(row2["uphl"].ToString()), 1).ToString());
                                    dictionary11.Add("yfsj", string.Format("{0:F1}", (num34 + num35) - (((num39 + num36) + (num41 - num40)) + ((num34 - num39) - num42))));
                                    list5.Add(dictionary11);
                                    num60++;
                                    num44 += num32;
                                    num45 += num33;
                                    num46 += num34;
                                    num47 += num35;
                                    num48 += num36;
                                    num53 += num40;
                                    num54 += num41;
                                    num52 += num43;
                                    num50 += num38;
                                    num51 += num39;
                                    num49 += num37;
                                    num55 += num42;
                                    num61 += double.Parse(row2["uphl"].ToString());
                                }
                                if (num44 > 0.0)
                                {
                                    num49 = Math.Round((double) ((num50 / num44) * 100.0), 2);
                                }
                                else
                                {
                                    num49 = 0.0;
                                }
                                Dictionary<string, string> dictionary12 = new Dictionary<string, string>();
                                dictionary12.Add("uname", "合計：");
                                dictionary12.Add("nicker", "");
                                dictionary12.Add("bs", num52.ToString());
                                dictionary12.Add("yxje", string.Format("{0:F1}", num44));
                                dictionary12.Add("hysy", string.Format("{0:F1}", num45));
                                dictionary12.Add("ysxx", string.Format("{0:F1}", num46 + num47));
                                dictionary12.Add("zc", num49.ToString() + "%");
                                dictionary12.Add("szze", string.Format("{0:F1}", num50));
                                dictionary12.Add("zhb", "");
                                dictionary12.Add("szsy", string.Format("{0:F1}", num51));
                                dictionary12.Add("szts", string.Format("{0:F1}", num48));
                                dictionary12.Add("szjg", string.Format("{0:F1}", num51 + num48));
                                dictionary12.Add("zqts", string.Format("{0:F1}", num54 - num53));
                                if (str2 == "fgs")
                                {
                                    dictionary12.Add("zqpl", string.Format("{0:F1}", (num46 - num51) - num55));
                                }
                                dictionary12.Add("sjjg", string.Format("{0:F1}", ((num51 + num48) + (num54 - num53)) + ((num46 - num51) - num55)));
                                dictionary12.Add("gxsj", string.Format("{0:F1}", num61));
                                dictionary12.Add("yfsj", string.Format("{0:F1}", (num46 + num47) - (((num51 + num48) + (num54 - num53)) + ((num46 - num51) - num55))));
                                list6.Add(dictionary12);
                                dictionary8.Add("listreport", list5);
                                dictionary8.Add("listcount", list6);
                                dictionary7.Add("listdata", dictionary8);
                                table2 = DbHelperSQL.Query("select count(bet_id) as bs,sum((CASE WHEN (isTie=1) THEN 0 ELSE amount END) ) zt, sum(" + str27 + ") result ,ISNULL(abs(sum((CASE WHEN (isTie=1) THEN 0 ELSE amount END)  * (CASE WHEN is_payment=0 THEN 0 ELSE " + str2 + "_drawback/100 END))) ,'0') as ysds from  " + str22 + "  where isDelete=0 " + str24 + " and  u_name='" + str4 + "' " + str6, list.ToArray()).Tables[0];
                                num62 = 0.0;
                                if (((table2.Rows.Count > 0) && (int.Parse(table2.Rows[0]["bs"].ToString().Trim()) > 0)) && (int.Parse(table2.Rows[0]["bs"].ToString()) > 0))
                                {
                                    num56 = Math.Round(double.Parse("-" + table2.Rows[0]["zt"].ToString()), 1);
                                    num57 = Math.Round(double.Parse(table2.Rows[0]["result"].ToString()), 1);
                                    num58 = Math.Round(double.Parse(table2.Rows[0]["ysds"].ToString()), 1);
                                    num62 += double.Parse(table2.Rows[0]["bs"].ToString());
                                }
                                Dictionary<string, string> dictionary13 = new Dictionary<string, string>();
                                dictionary13.Add("zcjg", string.Format("{0:F1}", num51 + num48));
                                dictionary13.Add("zqts", string.Format("{0:F1}", num54 - num53));
                                if (str2.Equals("fgs"))
                                {
                                    dictionary13.Add("zqpl", string.Format("{0:F1}", (num46 - num51) - num55));
                                }
                                dictionary13.Add("dkbhjzshjg", string.Format("{0:F1}", (((num51 + -num57) + (num48 - num58)) + (num54 - num53)) + ((num46 - num51) - num55)));
                                dictionary13.Add("yfsj", string.Format("{0:F1}", (num55 + num57) + (num53 + num58)));
                                list8.Add(dictionary13);
                                dictionary10.Add("listresult", list8);
                                dictionary7.Add("listdataresult", dictionary10);
                                if (num62 > 0.0)
                                {
                                    Dictionary<string, string> dictionary14 = new Dictionary<string, string>();
                                    dictionary14.Add("bs", num62.ToString());
                                    dictionary14.Add("bhje", string.Format("{0:F1}", num56));
                                    string str32 = string.Format("userid={0}&uID={1}&gID={2}&m_type={3}&t_FT={4}&t_LID={5}&BeginDate={6}&EndDate={7}&t_Balance={8}&t_LT={9}", new object[] { (userInfoByUID != null) ? userInfoByUID.get_u_id() : _session.get_u_id(), str4, str8, str16, base.q("t_FT"), str10, str11, str12, str13, str7 });
                                    dictionary14.Add("bhje_herf", str32);
                                    dictionary14.Add("bhsy", string.Format("{0:F1}", num57));
                                    dictionary14.Add("ts", string.Format("{0:F1}", num58));
                                    dictionary14.Add("tshjg", string.Format("{0:F1}", num57 + num58));
                                    list7.Add(dictionary14);
                                }
                                dictionary9.Add("listreportbh", list7);
                                dictionary7.Add("listdatabh", dictionary9);
                                dictionary7.Add("memberId", str16);
                                base.successOptMsg("分類報表查詢", dictionary7);
                            }
                        }
                    }
                }
            }
        }

        public void ReportDataKc_T()
        {
            agent_userinfo_session _session = this.Session[this.Session["user_name"] + "lottery_session_user_info"] as agent_userinfo_session;
            cz_users userInfoByUID = null;
            string str = LSRequest.qq("uid");
            if (!string.IsNullOrEmpty(str))
            {
                userInfoByUID = CallBLL.cz_users_bll.GetUserInfoByUID(str);
                if ((!_session.get_u_type().Equals("fgs") && !_session.get_allow_view_report().Equals(1)) && !base.IsUpperLowerLevels(userInfoByUID.get_u_name(), _session.get_u_type(), _session.get_u_name()))
                {
                    base.noRightOptMsg(PageBase.GetMessageByCache("u100057", "MessageHint"));
                    return;
                }
            }
            if ((_session.get_u_type().ToLower().Equals("zj") && (_session.get_users_child_session() != null)) && (_session.get_users_child_session().get_permissions_name().IndexOf("po_4_2") < 0))
            {
                base.noRightOptMsg(PageBase.GetMessageByCache("u100057", "MessageHint"));
            }
            else if ((!_session.get_u_type().ToLower().Equals("zj") && (_session.get_users_child_session() != null)) && (_session.get_users_child_session().get_permissions_name().IndexOf("po_7_2") < 0))
            {
                base.noRightOptMsg(PageBase.GetMessageByCache("u100057", "MessageHint"));
            }
            else
            {
                List<SqlParameter> list = new List<SqlParameter>();
                SqlParameter item = new SqlParameter();
                DateTime now = DateTime.Now;
                int num = int.Parse(now.Hour.ToString());
                int num2 = int.Parse(now.Minute.ToString());
                if (((num == 6) && (num2 >= 20)) && (num2 < 0x2d))
                {
                    base.noRightOptMsg(PageBase.GetMessageByCache("u100064", "MessageHint"));
                }
                else
                {
                    string str2 = "";
                    string str3 = "";
                    string str4 = "";
                    string str5 = base.q("t_LT");
                    if (str5.Equals("-1"))
                    {
                        str5 = "";
                    }
                    string str6 = base.q("gID");
                    string str7 = base.q("t_FT");
                    string str8 = base.q("t_LID");
                    if (str8.Equals("0"))
                    {
                        str8 = "";
                    }
                    string str9 = base.q("BeginDate");
                    string str10 = base.q("EndDate");
                    string str11 = base.q("t_Balance");
                    string str12 = "";
                    string str13 = "";
                    string str14 = "0";
                    string str15 = "";
                    if (!str7.Equals("0"))
                    {
                        DateTime time2 = Convert.ToDateTime(str9);
                        if (!string.IsNullOrEmpty(this.GetReportOpenDateKc()))
                        {
                            DateTime time3 = Convert.ToDateTime(this.GetReportOpenDateKc());
                            if (time2 < time3)
                            {
                                str9 = time3.ToString("yyyy-MM-dd");
                                base.noRightOptMsg(string.Format(PageBase.GetMessageByCache("u100065", "MessageHint"), str9));
                                return;
                            }
                        }
                    }
                    if (_session.get_u_type() == "fgs")
                    {
                        str14 = _session.get_allow_view_report().ToString();
                        str15 = _session.get_sup_name();
                    }
                    if (userInfoByUID == null)
                    {
                        str2 = _session.get_u_type();
                        if (str2 == "fgs")
                        {
                            if (str14 == "1")
                            {
                                str3 = str15;
                                str2 = "zj";
                            }
                            else
                            {
                                str3 = _session.get_u_name();
                            }
                        }
                        else
                        {
                            str3 = _session.get_u_name();
                        }
                    }
                    else
                    {
                        str3 = userInfoByUID.get_u_name();
                        str2 = userInfoByUID.get_u_type();
                    }
                    if ((str14 != "1") && !base.IsUpperLowerLevels(str3, _session.get_u_type(), _session.get_u_name()))
                    {
                        base.noRightOptMsg(PageBase.GetMessageByCache("u100057", "MessageHint"));
                    }
                    else
                    {
                        string str16 = "";
                        string str17 = "";
                        string str18 = "";
                        string str19 = "";
                        string str20 = "";
                        string str21 = "";
                        string str31 = str2;
                        if (str31 != null)
                        {
                            if (!(str31 == "zj"))
                            {
                                if (str31 == "fgs")
                                {
                                    str18 = "gd_drawback";
                                    str19 = "fgs_drawback";
                                    str17 = "fgs_name";
                                    str20 = "(100-(gd_rate+zd_rate+dl_rate))/100";
                                    str21 = "(100-(fgs_rate+gd_rate+zd_rate+dl_rate))/100";
                                    str16 = "gd";
                                }
                                else if (str31 == "gd")
                                {
                                    str18 = "zd_drawback";
                                    str19 = "gd_drawback";
                                    str17 = "gd_name";
                                    str20 = "(100-(zd_rate+dl_rate))/100";
                                    str21 = "(100-(gd_rate+zd_rate+dl_rate))/100";
                                    str16 = "zd";
                                    string text1 = " and gd_name='" + _session.get_u_name() + "'";
                                }
                                else if (str31 == "zd")
                                {
                                    str18 = "dl_drawback";
                                    str19 = "zd_drawback";
                                    str17 = "zd_name";
                                    str20 = "(100-dl_rate)/100";
                                    str21 = "(100-(zd_rate+dl_rate))/100";
                                    str16 = "dl";
                                    string text2 = " and zd_name='" + _session.get_u_name() + "'";
                                }
                                else if (str31 == "dl")
                                {
                                    str18 = "hy_drawback";
                                    str19 = "dl_drawback";
                                    str17 = "dl_name";
                                    str20 = "1";
                                    str21 = "(100-dl_rate)/100";
                                    str16 = "hy";
                                    string text3 = " and dl_name='" + _session.get_u_name() + "'";
                                }
                            }
                            else
                            {
                                str18 = "fgs_drawback";
                                str19 = "zj_drawback";
                                str20 = "(100-(fgs_rate+gd_rate+zd_rate+dl_rate))/100";
                                str21 = "0";
                            }
                        }
                        if (str17 != "")
                        {
                            str17 = " and " + str17 + "='" + str3 + "'";
                        }
                        Convert.ToDateTime(DateTime.Now.ToString());
                        string str22 = "";
                        string str23 = "";
                        string str24 = DateTime.Now.AddHours(-7.0).ToString("yyyy-MM-dd");
                        if ((str24 == str9) && (str24 == str10))
                        {
                            str23 = "cz_bet_kc  with(NOLOCK) ";
                        }
                        else
                        {
                            str23 = " cz_betold_kc  with(NOLOCK) ";
                        }
                        if (str5 != "")
                        {
                            str12 = str12 + " and lottery_type=@lottery_type  ";
                            item = new SqlParameter("@lottery_type", SqlDbType.NVarChar) {
                                Value = str5
                            };
                            list.Add(item);
                        }
                        if (str6 != "0")
                        {
                            str12 = str12 + " and play_id=@play_id  ";
                            item = new SqlParameter("@play_id", SqlDbType.NVarChar) {
                                Value = str6
                            };
                            list.Add(item);
                        }
                        if (str11 == "0")
                        {
                            str12 = str12 + " and is_payment=0 ";
                        }
                        else
                        {
                            str12 = str12 + " and is_payment=1 ";
                        }
                        if (str7 == "0")
                        {
                            str12 = str12 + " and phase_id=@phase_id  ";
                            item = new SqlParameter("@phase_id", SqlDbType.NVarChar) {
                                Value = str8
                            };
                            list.Add(item);
                        }
                        else
                        {
                            DateTime time5 = Convert.ToDateTime(str10);
                            DateTime time6 = DateTime.Now;
                            time6 = time5.AddDays(1.0);
                            str12 = str12 + " and bet_time between   @BeginDate and @Enddatejtdate  ";
                            item = new SqlParameter("@BeginDate", SqlDbType.NVarChar) {
                                Value = str9 + " 06:30:00"
                            };
                            list.Add(item);
                            item = new SqlParameter("@Enddatejtdate", SqlDbType.NVarChar) {
                                Value = time6.ToString("yyyy-MM-dd") + " 06:30:00"
                            };
                            list.Add(item);
                        }
                        string str25 = "";
                        string str26 = "profit";
                        if (str2 != "zj")
                        {
                            if (str2 == "fgs")
                            {
                                str26 = "profit_zj";
                            }
                            str25 = "select lottery_type,count(bet_id) as bs,sum(" + str21 + "* (CASE WHEN (isTie=1) THEN 0 ELSE amount END)) uphl,sum(CASE WHEN sale_type=1 THEN 0 ELSE amount END)as hyzt,  ISNULL(sum(CASE WHEN sale_type=1 THEN 0 ELSE profit END),'0') as hyjg , sum((CASE WHEN (isTie=1) THEN 0 ELSE amount END)*" + str2 + "_rate/100)as szzt,  ISNULL(sum(profit*" + str2 + "_rate/100),'0')as szjg,ISNULL(sum(" + str26 + "*" + str21 + "),'0')as upjg ,ISNULL(sum(profit*" + str20 + "),0) as ysze,ISNULL(abs(sum((CASE WHEN (isTie=1) THEN 0 ELSE amount END)* " + str20 + " * (CASE WHEN is_payment=0 THEN 0 ELSE " + str18 + "/100 END))) ,'0') as ysds,ISNULL(abs(sum((CASE WHEN (isTie=1) THEN 0 ELSE amount END)* " + str21 + " * (CASE WHEN is_payment=0 THEN 0 ELSE " + str19 + "/100 END))) ,'0') as mygetds,ISNULL(abs(sum((CASE WHEN (isTie=1) THEN 0 ELSE amount END)* " + str21 + " * (CASE WHEN is_payment=0 THEN 0 ELSE " + str18 + "/100 END))) ,'0') as mypayds,ISNULL(abs(sum((CASE WHEN (isTie=1) THEN 0 ELSE amount END)* " + str2 + "_rate/100  * (CASE WHEN is_payment=0 THEN 0 ELSE " + str18 + "/100 END))) ,'0') as szds,play_id,play_name from  ( select * from   " + str23 + "  where  isDelete=0  " + str12 + " " + str17 + " and u_name!='" + str3 + "'" + str4 + " ) as bet group by lottery_type,play_id,play_name  order by lottery_type asc,play_id asc";
                            str22 = "select ISNULL(sum((CASE WHEN (isTie=1) THEN 0 ELSE amount END)*" + str2 + "_rate/100),'0') as szzt from  " + str23 + "  where  isDelete=0  " + str12 + " " + str17 + " and u_name!='" + str3 + "'" + str4;
                        }
                        else
                        {
                            str25 = "select lottery_type,count(bet_id) as bs,sum(" + str21 + "* (CASE WHEN (isTie=1) THEN 0 ELSE amount END)) uphl,sum(CASE WHEN sale_type=1 THEN 0 ELSE amount END)as hyzt,  ISNULL(sum(CASE WHEN sale_type=1 THEN 0 ELSE profit_zj END),'0') as hyjg , sum((CASE WHEN (isTie=1) THEN 0 ELSE amount END)*" + str2 + "_rate/100)as szzt,  ISNULL(sum(profit_zj*" + str2 + "_rate/100),'0')as szjg,ISNULL(sum(profit_zj*" + str21 + "),'0')as upjg ,ISNULL(sum(profit_zj*" + str20 + "),0) as ysze,ISNULL(abs(sum((CASE WHEN (isTie=1) THEN 0 ELSE amount END)* " + str20 + " * (CASE WHEN is_payment=0 THEN 0 ELSE " + str18 + "/100 END))) ,'0') as ysds,ISNULL(abs(sum((CASE WHEN (isTie=1) THEN 0 ELSE amount END)* " + str21 + " * (CASE WHEN is_payment=0 THEN 0 ELSE " + str19 + "/100 END))) ,'0') as mygetds,ISNULL(abs(sum((CASE WHEN (isTie=1) THEN 0 ELSE amount END)* " + str21 + " * (CASE WHEN is_payment=0 THEN 0 ELSE " + str18 + "/100 END))) ,'0') as mypayds,ISNULL(abs(sum((CASE WHEN (isTie=1) THEN 0 ELSE amount END)* " + str2 + "_rate/100  * (CASE WHEN is_payment=0 THEN 0 ELSE " + str18 + "/100 END))) ,'0') as szds,play_id,play_name from  ( select * from  " + str23 + "  where  isDelete=0  " + str12 + " " + str17 + " and fgs_name!='" + str3 + "'" + str4 + " ) as bet group by lottery_type,play_id,play_name order by lottery_type asc,play_id asc";
                            str22 = "select ISNULL(sum((CASE WHEN (isTie=1) THEN 0 ELSE amount END)*" + str2 + "_rate/100),'0') as szzt from  " + str23 + "  where  isDelete=0  " + str12 + " " + str17 + " and fgs_name!='" + str3 + "'" + str4;
                        }
                        DataTable table = DbHelperSQL.Query(str22, list.ToArray()).Tables[0];
                        double num3 = Math.Round(double.Parse(table.Rows[0][0].ToString()), 1);
                        table = DbHelperSQL.Query(str25, list.ToArray()).Tables[0];
                        DateTime time7 = DateTime.Now;
                        if (str10.Trim() == "")
                        {
                            str9 = time7.ToString("yyyy-MM-dd");
                            str10 = time7.ToString("yyyy-MM-dd");
                        }
                        if (str2 == "zj")
                        {
                            double num4 = 0.0;
                            double num5 = 0.0;
                            double num6 = 0.0;
                            double num7 = 0.0;
                            double num8 = 0.0;
                            double num9 = 0.0;
                            double num10 = 0.0;
                            double num11 = 0.0;
                            int num12 = 0;
                            double num13 = 0.0;
                            double num14 = 0.0;
                            double num15 = 0.0;
                            double num16 = 0.0;
                            double num17 = 0.0;
                            double num18 = 0.0;
                            double num19 = 0.0;
                            double num20 = 0.0;
                            double num21 = 0.0;
                            double num22 = 0.0;
                            double num23 = 0.0;
                            double num24 = 0.0;
                            int num25 = 0;
                            Dictionary<string, object> data = new Dictionary<string, object>();
                            Dictionary<string, List<Dictionary<string, string>>> dictionary2 = new Dictionary<string, List<Dictionary<string, string>>>();
                            List<Dictionary<string, string>> list2 = new List<Dictionary<string, string>>();
                            List<Dictionary<string, string>> list3 = new List<Dictionary<string, string>>();
                            Dictionary<string, List<Dictionary<string, string>>> dictionary3 = new Dictionary<string, List<Dictionary<string, string>>>();
                            List<Dictionary<string, string>> list4 = new List<Dictionary<string, string>>();
                            foreach (DataRow row in table.Rows)
                            {
                                Dictionary<string, string> dictionary4 = new Dictionary<string, string>();
                                num12 = int.Parse(row["bs"].ToString());
                                num4 = Math.Round(double.Parse(row["hyzt"].ToString()), 1);
                                num5 = Math.Round(double.Parse(row["hyjg"].ToString()), 1);
                                num10 = Math.Round(double.Parse(row["szzt"].ToString()), 1);
                                num11 = Math.Round(double.Parse(row["szjg"].ToString()), 1);
                                if (num4 > 0.0)
                                {
                                    num9 = Math.Round((double) ((num10 / num4) * 100.0), 3);
                                }
                                else
                                {
                                    num9 = 0.0;
                                }
                                num6 = Math.Round(double.Parse(row["ysze"].ToString()), 1);
                                num7 = Math.Round(double.Parse(row["ysds"].ToString()), 1);
                                num8 = Math.Round(double.Parse(row["szds"].ToString()), 1);
                                if ((num10 > 0.0) && (num3 > 0.0))
                                {
                                    num24 = Math.Round((double) ((num10 / num3) * 100.0), 2);
                                }
                                else
                                {
                                    num24 = 0.0;
                                }
                                dictionary4.Add("xzlx", "『" + base.GetGameNameByID(row["lottery_type"].ToString()) + "』" + row["play_name"].ToString());
                                dictionary4.Add("bs", num12.ToString());
                                dictionary4.Add("zxze", num4.ToString());
                                string str27 = string.Format("action=SearchDetailKc&q_type={0}&gID={1}&t_LT=1&t_FT={2}&t_LID={3}&BeginDate={4}&EndDate={5}&ReportType=T", new object[] { row["lottery_type"].ToString(), row["play_id"].ToString(), str7, str13, str9, str10 });
                                dictionary4.Add("zxze_href", str27);
                                dictionary4.Add("hysy", string.Format("{0:F1}", num5));
                                dictionary4.Add("ysxx", string.Format("{0:F1}", num6 + num7));
                                dictionary4.Add("zc", num9.ToString() + "%");
                                dictionary4.Add("szze", string.Format("{0:F1}", num10));
                                dictionary4.Add("zhb", num24 + "%");
                                dictionary4.Add("szsy", string.Format("{0:F1}", num11));
                                dictionary4.Add("szts", string.Format("{0:F1}", num8));
                                dictionary4.Add("szjg", string.Format("{0:F1}", num11 + num8));
                                list2.Add(dictionary4);
                                num25++;
                                num13 += num4;
                                num14 += num5;
                                num15 += num6;
                                num16 += num7;
                                num17 += num8;
                                num21 += num12;
                                num19 += num10;
                                num20 += num11;
                            }
                            if (num13 > 0.0)
                            {
                                num18 = Math.Round((double) ((num19 / num13) * 100.0), 3);
                            }
                            else
                            {
                                num18 = 0.0;
                            }
                            Dictionary<string, string> dictionary5 = new Dictionary<string, string>();
                            dictionary5.Add("xzlx", "合計：");
                            dictionary5.Add("bs", num21.ToString());
                            dictionary5.Add("zxze", Math.Round(num13, 1).ToString());
                            dictionary5.Add("hysy", Math.Round(num14, 1).ToString());
                            dictionary5.Add("ysxx", Math.Round((double) (num15 + num16), 1).ToString());
                            dictionary5.Add("zc", num18.ToString() + "%");
                            dictionary5.Add("szze", Math.Round(num19, 1).ToString());
                            dictionary5.Add("zhb", "");
                            dictionary5.Add("szsy", Math.Round(num20, 1).ToString());
                            dictionary5.Add("szts", Math.Round(num17, 1).ToString());
                            dictionary5.Add("szjg", Math.Round((double) (num20 + num17), 1).ToString());
                            list3.Add(dictionary5);
                            dictionary2.Add("listreport", list2);
                            dictionary2.Add("listcount", list3);
                            data.Add("listdata", dictionary2);
                            Dictionary<string, string> dictionary6 = new Dictionary<string, string>();
                            dictionary6.Add("zcjg", string.Format("{0:F1}", num20 + num17));
                            dictionary6.Add("dkbhhjg", string.Format("{0:F1}", (num20 + num17) - (num22 + num23)));
                            list4.Add(dictionary6);
                            dictionary3.Add("listresult", list4);
                            data.Add("listdataresult", dictionary3);
                            data.Add("memberId", "fgs");
                            base.successOptMsg("分類報表查詢", data);
                        }
                        else
                        {
                            double num26 = 0.0;
                            double num27 = 0.0;
                            double num28 = 0.0;
                            double num29 = 0.0;
                            double num30 = 0.0;
                            double num31 = 0.0;
                            double num32 = 0.0;
                            double num33 = 0.0;
                            double num34 = 0.0;
                            double num35 = 0.0;
                            double num36 = 0.0;
                            int num37 = 0;
                            double num38 = 0.0;
                            double num39 = 0.0;
                            double num40 = 0.0;
                            double num41 = 0.0;
                            double num42 = 0.0;
                            double num43 = 0.0;
                            double num44 = 0.0;
                            double num45 = 0.0;
                            double num46 = 0.0;
                            double num47 = 0.0;
                            double num48 = 0.0;
                            double num49 = 0.0;
                            double num50 = 0.0;
                            double num51 = 0.0;
                            double num52 = 0.0;
                            double num53 = 0.0;
                            int num54 = 0;
                            Dictionary<string, object> dictionary7 = new Dictionary<string, object>();
                            Dictionary<string, List<Dictionary<string, string>>> dictionary8 = new Dictionary<string, List<Dictionary<string, string>>>();
                            List<Dictionary<string, string>> list5 = new List<Dictionary<string, string>>();
                            List<Dictionary<string, string>> list6 = new List<Dictionary<string, string>>();
                            Dictionary<string, List<Dictionary<string, string>>> dictionary9 = new Dictionary<string, List<Dictionary<string, string>>>();
                            List<Dictionary<string, string>> list7 = new List<Dictionary<string, string>>();
                            List<Dictionary<string, string>> list8 = new List<Dictionary<string, string>>();
                            new Dictionary<string, List<Dictionary<string, string>>>();
                            new List<Dictionary<string, string>>();
                            Dictionary<string, List<Dictionary<string, string>>> dictionary10 = new Dictionary<string, List<Dictionary<string, string>>>();
                            List<Dictionary<string, string>> list9 = new List<Dictionary<string, string>>();
                            foreach (DataRow row2 in table.Rows)
                            {
                                Dictionary<string, string> dictionary11 = new Dictionary<string, string>();
                                num37 = int.Parse(row2["bs"].ToString());
                                num26 = Math.Round(double.Parse(row2["hyzt"].ToString()), 1);
                                num27 = Math.Round(double.Parse(row2["hyjg"].ToString()), 1);
                                num32 = Math.Round(double.Parse(row2["szzt"].ToString()), 1);
                                num33 = Math.Round(double.Parse(row2["szjg"].ToString()), 1);
                                if (num26 > 0.0)
                                {
                                    num31 = Math.Round((double) ((num32 / num26) * 100.0), 3);
                                }
                                else
                                {
                                    num31 = 0.0;
                                }
                                num28 = Math.Round(double.Parse(row2["ysze"].ToString()), 1);
                                num29 = Math.Round(double.Parse(row2["ysds"].ToString()), 1);
                                num30 = Math.Round(double.Parse(row2["szds"].ToString()), 1);
                                num34 = Math.Round(double.Parse(row2["mygetds"].ToString()), 1);
                                num35 = Math.Round(double.Parse(row2["mypayds"].ToString()), 1);
                                num36 = Math.Round(double.Parse(row2["upjg"].ToString()), 1);
                                if (num32 > 0.0)
                                {
                                    num53 = Math.Round((double) ((num32 / num3) * 100.0), 2);
                                }
                                else
                                {
                                    num53 = 0.0;
                                }
                                dictionary11.Add("xzlx", "『" + base.GetGameNameByID(row2["lottery_type"].ToString()) + "』" + row2["play_name"].ToString());
                                dictionary11.Add("bs", num37.ToString());
                                dictionary11.Add("xzje", num26.ToString());
                                string str28 = string.Format("action=SearchDetailKc&q_type={0}&UP_1_ID={1}&gID={2}&m_type={3}&t_LT=1&t_FT={4}&t_LID={5}&BeginDate={6}&EndDate={7}&ReportType=T", new object[] { row2["lottery_type"].ToString(), str3, row2["play_id"].ToString(), str2, str7, str13, str9, str10 });
                                dictionary11.Add("xzje_href", str28);
                                dictionary11.Add("hysy", string.Format("{0:F1}", num27).ToString());
                                dictionary11.Add("ysze", string.Format("{0:F1}", num28 + num29));
                                dictionary11.Add("zc", num31.ToString() + "%");
                                dictionary11.Add("szze", string.Format("{0:F1}", num32));
                                dictionary11.Add("zhb", num53.ToString() + "%");
                                dictionary11.Add("szsy", Math.Round(num33, 1).ToString());
                                dictionary11.Add("szts", Math.Round(num30, 1).ToString());
                                dictionary11.Add("szjg", Math.Round((double) (num33 + num30), 1).ToString());
                                dictionary11.Add("zqts", Math.Round((double) (num35 - num34), 1).ToString());
                                dictionary11.Add("zshjg", Math.Round((double) ((num33 + num30) + (num34 - num35)), 1).ToString());
                                dictionary11.Add("yfsj", Math.Round((double) ((num28 + num29) - ((num33 + num30) + (num35 - num34))), 1).ToString());
                                list5.Add(dictionary11);
                                num54++;
                                num38 += num26;
                                num39 += num27;
                                num40 += num28;
                                num41 += num29;
                                num42 += num30;
                                num47 += num34;
                                num48 += num35;
                                num46 += num37;
                                num44 += num32;
                                num45 += num33;
                                num43 += num31;
                                num49 += num36;
                            }
                            if (num38 > 0.0)
                            {
                                num43 = Math.Round((double) ((num44 / num38) * 100.0), 3);
                            }
                            else
                            {
                                num43 = 0.0;
                            }
                            Dictionary<string, string> dictionary12 = new Dictionary<string, string>();
                            dictionary12.Add("xzlx", "合計：");
                            dictionary12.Add("bs", num46.ToString());
                            dictionary12.Add("xzje", Math.Round(num38, 1).ToString());
                            dictionary12.Add("hysy", Math.Round(num39, 1).ToString());
                            dictionary12.Add("ysze", Math.Round((double) (num40 + num41), 1).ToString());
                            dictionary12.Add("zc", num43.ToString() + "%");
                            dictionary12.Add("szze", Math.Round(num44, 1).ToString());
                            dictionary12.Add("zhb", "");
                            dictionary12.Add("szsy", Math.Round(num45, 1).ToString());
                            dictionary12.Add("szts", Math.Round(num42, 1).ToString());
                            dictionary12.Add("szjg", Math.Round((double) (num45 + num42), 1).ToString());
                            dictionary12.Add("zqts", Math.Round((double) (num48 - num47), 1).ToString());
                            dictionary12.Add("zshjg", Math.Round((double) ((num45 + num42) + (num47 - num48)), 1).ToString());
                            dictionary12.Add("yfsj", Math.Round((double) ((num40 + num41) - ((num45 + num42) + (num48 - num47))), 1).ToString());
                            list6.Add(dictionary12);
                            dictionary8.Add("listreport", list5);
                            dictionary8.Add("listcount", list6);
                            dictionary7.Add("listdata", dictionary8);
                            double num55 = 0.0;
                            double num56 = 0.0;
                            double num57 = 0.0;
                            double num58 = 0.0;
                            table = DbHelperSQL.Query("select lottery_type,play_id,play_name, count(bet_id) as bs,sum((CASE WHEN (isTie=1) THEN 0 ELSE amount END)) zt, sum(" + str26 + ") result ,ISNULL(abs(sum((CASE WHEN (isTie=1) THEN 0 ELSE amount END) * (CASE WHEN is_payment=0 THEN 0 ELSE " + str2 + "_drawback/100 END))) ,'0') as ysds from  " + str23 + "  where  isDelete=0  " + str12 + "  and  u_name='" + str3 + "'  group by lottery_type,play_id,play_name  order by lottery_type asc,play_id asc ", list.ToArray()).Tables[0];
                            Dictionary<string, string> dictionary13 = new Dictionary<string, string>();
                            if ((table.Rows.Count > 0) && (int.Parse(table.Rows[0]["bs"].ToString().Trim()) > 0))
                            {
                                foreach (DataRow row3 in table.Rows)
                                {
                                    Dictionary<string, string> dictionary14 = new Dictionary<string, string>();
                                    num50 = Math.Round(double.Parse("-" + row3["zt"].ToString()), 1);
                                    num51 = Math.Round(double.Parse(row3["result"].ToString()), 1);
                                    num52 = Math.Round(double.Parse(row3["ysds"].ToString()), 1);
                                    num55 += double.Parse(row3["bs"].ToString());
                                    num56 += num50;
                                    num57 += num51;
                                    num58 += num52;
                                    dictionary14.Add("bhlb", "『" + base.GetGameNameByID(row3["lottery_type"].ToString()) + "』" + row3["play_name"].ToString());
                                    dictionary14.Add("bs", row3["bs"].ToString());
                                    string str29 = string.Format("action=SearchDetailKc&q_type={0}&uID={1}&gID={2}&t_LT=1&m_type={3}&t_FT={4}&t_LID={5}&BeginDate={6}&EndDate={7}&FactSize=fill&isbh=1&ReportType=B", new object[] { row3["lottery_type"].ToString(), str3, row3["play_id"].ToString(), str16, str7, str13, str9, str10 });
                                    dictionary14.Add("bs_href", str29);
                                    dictionary14.Add("bhje", num50.ToString());
                                    string str30 = string.Format("action=SearchDetailKc&q_type={0}&uID={1}&gID={2}&t_LT=1&m_type={3}&t_FT={4}&t_LID={5}&BeginDate={6}&EndDate={7}&FactSize=my&isbh=1&ReportType=B", new object[] { row3["lottery_type"].ToString(), str3, row3["play_id"].ToString(), str16, str7, str13, str9, str10 });
                                    dictionary14.Add("bhje_href", str30);
                                    dictionary14.Add("zhb", "100%");
                                    dictionary14.Add("bhsy", Math.Round(num51, 1).ToString());
                                    dictionary14.Add("ts", Math.Round(num52, 1).ToString());
                                    dictionary14.Add("tshjg", Math.Round((double) (num51 + num52), 1).ToString());
                                    list7.Add(dictionary14);
                                }
                                Dictionary<string, string> dictionary15 = new Dictionary<string, string>();
                                dictionary15.Add("bhlb", "合計：");
                                dictionary15.Add("bs", Math.Round(num55, 1).ToString());
                                dictionary15.Add("bhje", Math.Round(num56, 1).ToString());
                                dictionary15.Add("zhb", "100%");
                                dictionary15.Add("bhsy", Math.Round(num57, 1).ToString());
                                dictionary15.Add("ts", num58.ToString());
                                dictionary15.Add("tshjg", Math.Round((double) (num57 + num58), 2).ToString());
                                list8.Add(dictionary15);
                                dictionary9.Add("listreportbh", list7);
                                dictionary9.Add("listcountbh", list8);
                                dictionary7.Add("listdatabh", dictionary9);
                                dictionary13.Add("szze", Math.Round((double) (num44 + num56), 1).ToString());
                                dictionary13.Add("syjg", Math.Round((double) (num45 + -num57), 1).ToString());
                                dictionary13.Add("ts", Math.Round((double) (num42 - num58), 1).ToString());
                                dictionary13.Add("tshjg", Math.Round((double) ((num45 + -num57) + (num42 - num58)), 1).ToString());
                                dictionary13.Add("zqts", Math.Round((double) (num48 - num47), 1).ToString());
                                dictionary13.Add("zshjg", Math.Round((double) (((num45 + -num57) + (num42 - num58)) + (num48 - num47)), 1).ToString());
                            }
                            dictionary13.Add("zcjg", string.Format("{0:F1}", num45 + num42));
                            dictionary13.Add("yfsj", string.Format("{0:F1}", (num49 + num57) + (num47 + num58)));
                            list9.Add(dictionary13);
                            dictionary10.Add("listresult", list9);
                            dictionary7.Add("listdataresult", dictionary10);
                            dictionary7.Add("memberId", str16);
                            base.successOptMsg("分類報表查詢", dictionary7);
                        }
                    }
                }
            }
        }

        public void ReportDataSix_B()
        {
            agent_userinfo_session _session = this.Session[this.Session["user_name"] + "lottery_session_user_info"] as agent_userinfo_session;
            if ((_session.get_u_type().ToLower().Equals("zj") && (_session.get_users_child_session() != null)) && (_session.get_users_child_session().get_permissions_name().IndexOf("po_4_1") < 0))
            {
                base.noRightOptMsg(PageBase.GetMessageByCache("u100057", "MessageHint"));
            }
            else if ((!_session.get_u_type().ToLower().Equals("zj") && (_session.get_users_child_session() != null)) && (_session.get_users_child_session().get_permissions_name().IndexOf("po_7_1") < 0))
            {
                base.noRightOptMsg(PageBase.GetMessageByCache("u100057", "MessageHint"));
            }
            else
            {
                SqlParameter[] parameterArray3;
                List<SqlParameter> list = new List<SqlParameter>();
                SqlParameter item = new SqlParameter();
                string str2 = "";
                string str3 = "select is_closed,p_id from cz_phase_six where is_opendata=0";
                DataTable table = DbHelperSQL.Query(str3, null).Tables[0];
                string str4 = base.q("gID");
                string str5 = base.q("t_FT");
                string str6 = base.q("t_LID");
                string str7 = base.q("BeginDate");
                string str8 = base.q("EndDate");
                string str9 = base.q("t_Balance");
                if (str5.Trim() == "0")
                {
                    string str10 = "select phase from cz_phase_six where p_id=@cxqs";
                    parameterArray3 = new SqlParameter[1];
                    SqlParameter parameter2 = new SqlParameter("@cxqs", SqlDbType.NVarChar) {
                        Value = str6
                    };
                    parameterArray3[0] = parameter2;
                    SqlParameter[] parameterArray = parameterArray3;
                    DataTable table2 = DbHelperSQL.Query(str10, parameterArray).Tables[0];
                    table2.Rows[0][0].ToString();
                }
                if (table.Rows.Count > 0)
                {
                    if ((table.Rows[0][0].ToString().Trim() == "1") && (_session.get_u_type() != "zj"))
                    {
                        base.noRightOptMsg(PageBase.GetMessageByCache("u100067", "MessageHint"));
                        return;
                    }
                    if (str5.Trim() == "0")
                    {
                        if (string.IsNullOrEmpty(str6))
                        {
                            base.Response.End();
                        }
                        if (str6.Trim() != table.Rows[0]["p_id"].ToString().Trim())
                        {
                            base.noRightOptMsg(PageBase.GetMessageByCache("u100068", "MessageHint"));
                            return;
                        }
                    }
                    else
                    {
                        string str11 = DateTime.Now.ToString("yyyy-MM-dd");
                        if (str7 != str11)
                        {
                            base.noRightOptMsg(PageBase.GetMessageByCache("u100068", "MessageHint"));
                            return;
                        }
                    }
                    str2 = " cz_bet_six with(NOLOCK) ";
                }
                else
                {
                    str2 = " cz_betold_six with(NOLOCK) ";
                }
                string str12 = "";
                string str13 = "";
                string str14 = "";
                string str15 = "";
                double num = 0.0;
                string str16 = "0";
                string str17 = "";
                if (_session.get_u_type() == "fgs")
                {
                    str16 = _session.get_allow_view_report().ToString();
                    str17 = _session.get_sup_name();
                }
                if (base.Request["a_name"] == null)
                {
                    str12 = _session.get_u_type();
                    if (str12 == "fgs")
                    {
                        if (str16 == "1")
                        {
                            str14 = str17;
                            str12 = "zj";
                        }
                        else
                        {
                            str14 = _session.get_u_name();
                        }
                    }
                    else
                    {
                        str14 = _session.get_u_name();
                    }
                }
                else
                {
                    str14 = base.q("a_name");
                    str12 = base.q("a_type");
                    string str18 = "select u_type from cz_users where u_name=@a_name ";
                    parameterArray3 = new SqlParameter[1];
                    SqlParameter parameter3 = new SqlParameter("@a_name", SqlDbType.NVarChar) {
                        Value = base.q("a_name")
                    };
                    parameterArray3[0] = parameter3;
                    SqlParameter[] parameterArray2 = parameterArray3;
                    DataTable table3 = DbHelperSQL.Query(str18, parameterArray2).Tables[0];
                    str12 = table3.Rows[0][0].ToString().Trim();
                }
                if ((str16 != "1") && !base.IsUpperLowerLevels(str14, _session.get_u_type(), _session.get_u_name()))
                {
                    base.noRightOptMsg(PageBase.GetMessageByCache("u100057", "MessageHint"));
                }
                else
                {
                    if (str4 != "0")
                    {
                        str15 = " and play_id=@play_id ";
                        item = new SqlParameter("@play_id", SqlDbType.NVarChar) {
                            Value = str4
                        };
                        list.Add(item);
                    }
                    if (str9 != "")
                    {
                        str15 = str15 + " and is_payment=@is_payment  ";
                        item = new SqlParameter("@is_payment", SqlDbType.NVarChar) {
                            Value = str9
                        };
                        list.Add(item);
                    }
                    string str19 = "";
                    string str20 = "";
                    string str21 = "";
                    string str22 = "";
                    string str23 = "";
                    string str24 = "";
                    string str40 = str12;
                    if (str40 != null)
                    {
                        if (!(str40 == "zj"))
                        {
                            if (str40 == "fgs")
                            {
                                str13 = "gd_name";
                                str21 = "gd_drawback";
                                str22 = "fgs_drawback";
                                str20 = "fgs_name";
                                str23 = "(100-(gd_rate+zd_rate+dl_rate))/100";
                                str24 = "(100-(fgs_rate+gd_rate+zd_rate+dl_rate))/100";
                                str19 = "gd";
                            }
                            else if (str40 == "gd")
                            {
                                str13 = "zd_name";
                                str21 = "zd_drawback";
                                str22 = "gd_drawback";
                                str20 = "gd_name";
                                str23 = "(100-(zd_rate+dl_rate))/100";
                                str24 = "(100-(gd_rate+zd_rate+dl_rate))/100";
                                str19 = "zd";
                            }
                            else if (str40 == "zd")
                            {
                                str13 = "dl_name";
                                str21 = "dl_drawback";
                                str22 = "zd_drawback";
                                str20 = "zd_name";
                                str23 = "(100-dl_rate)/100";
                                str24 = "(100-(zd_rate+dl_rate))/100";
                                str19 = "dl";
                            }
                            else if (str40 == "dl")
                            {
                                str13 = "u_name";
                                str21 = "hy_drawback";
                                str22 = "dl_drawback";
                                str20 = "dl_name";
                                str23 = "1";
                                str24 = "(100-dl_rate)/100";
                                str19 = "hy";
                            }
                        }
                        else
                        {
                            str13 = "fgs_name";
                            str21 = "fgs_drawback";
                            str22 = "zj_drawback";
                            str23 = "(100-(fgs_rate+gd_rate+zd_rate+dl_rate))/100";
                            str24 = "0";
                        }
                    }
                    if (str20 != "")
                    {
                        str20 = " and " + str20 + "='" + str14 + "'";
                    }
                    string str25 = "select top 1 * from cz_phase_six order by  p_id desc";
                    DataTable table4 = DbHelperSQL.Query(str25, null).Tables[0];
                    table4.Rows[0]["p_id"].ToString().Trim();
                    DateTime time3 = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd")).AddDays(-68.0);
                    DateTime time4 = Convert.ToDateTime(str7);
                    DateTime time5 = Convert.ToDateTime(str8);
                    if (!str5.Equals("0") && !string.IsNullOrEmpty(this.GetReportOpenDateSix()))
                    {
                        DateTime time6 = Convert.ToDateTime(this.GetReportOpenDateSix());
                        if (time4 < time6)
                        {
                            str7 = time6.ToString("yyyy-MM-dd");
                            base.noRightOptMsg(string.Format(PageBase.GetMessageByCache("u100066", "MessageHint"), str7));
                            return;
                        }
                    }
                    if (time3 > time4)
                    {
                        str7 = time3.ToString("yyyy-MM-dd");
                        base.noRightOptMsg(string.Format(PageBase.GetMessageByCache("u100066", "MessageHint"), str7));
                    }
                    else
                    {
                        if (time3 > time5)
                        {
                            str8 = time3.ToString("yyyy-MM-dd");
                        }
                        string str26 = str8;
                        string str27 = " ";
                        if (str5.Trim() == "0")
                        {
                            if (string.IsNullOrEmpty(str6))
                            {
                                base.Response.End();
                            }
                            else
                            {
                                str27 = " and phase_id=@phase_id  ";
                                item = new SqlParameter("@phase_id", SqlDbType.NVarChar) {
                                    Value = base.q("t_LID")
                                };
                                list.Add(item);
                            }
                        }
                        else
                        {
                            str27 = " and bet_time between @BeginDate and @CqDate  ";
                            item = new SqlParameter("@BeginDate", SqlDbType.NVarChar) {
                                Value = str7 + " 00:00:00"
                            };
                            list.Add(item);
                            item = new SqlParameter("@CqDate", SqlDbType.NVarChar) {
                                Value = str26 + " 23:59:59"
                            };
                            list.Add(item);
                        }
                        string str28 = "";
                        string str29 = "";
                        string str30 = "profit";
                        if (str12 != "zj")
                        {
                            if (str12 == "fgs")
                            {
                                str30 = "profit_zj";
                            }
                            str28 = "select count(bet_id) as bs,sum(" + str24 + "* (CASE WHEN (isTie=1) THEN 0 ELSE amount END) ) uphl,sum(CASE WHEN sale_type=1 THEN 0 ELSE (CASE WHEN (isTie=1) THEN 0 ELSE amount END)  END)as hyzt,  ISNULL(sum(CASE WHEN  sale_type=1 THEN 0 ELSE profit END),'0') as hyjg , sum((CASE WHEN (isTie=1) THEN 0 ELSE amount END) *" + str12 + "_rate/100)as szzt,  ISNULL(sum(profit*" + str12 + "_rate/100),'0')as szjg,ISNULL(sum(" + str30 + "*" + str24 + "),'0')as upjg ,ISNULL(sum(profit*" + str23 + "),0) as ysze,ISNULL(abs(sum((CASE WHEN (isTie=1) THEN 0 ELSE amount END) * " + str23 + " * (CASE WHEN is_payment=0 THEN 0 ELSE " + str21 + "/100 END))) ,'0') as ysds,ISNULL(abs(sum((CASE WHEN (isTie=1) THEN 0 ELSE amount END) * " + str24 + " * (CASE WHEN is_payment=0 THEN 0 ELSE " + str22 + "/100 END))) ,'0') as mygetds,ISNULL(abs(sum((CASE WHEN (isTie=1) THEN 0 ELSE amount END) * " + str24 + " * (CASE WHEN is_payment=0 THEN 0 ELSE " + str21 + "/100 END))) ,'0') as mypayds,ISNULL(abs(sum((CASE WHEN (isTie=1) THEN 0 ELSE amount END) * " + str12 + "_rate/100  * (CASE WHEN is_payment=0 THEN 0 ELSE " + str21 + "/100 END))) ,'0') as szds,ISNULL(abs(sum((CASE WHEN (isTie=1) THEN 0 ELSE amount END) * " + str12 + "_rate/100  * (CASE WHEN is_payment=0 THEN 0 ELSE " + str21 + "/100 END))) ,'0') as sz_payds,max(m_type) m_type," + str13 + " from  ( select * from  " + str2 + "  where   isDelete=0 " + str27 + str20 + " and u_name!='" + str14 + "'" + str15 + " ) as bet group by " + str13;
                            str29 = "select ISNULL(sum((CASE WHEN (isTie=1) THEN 0 ELSE amount END) *" + str12 + "_rate/100),'0') as szzt from  " + str2 + "  where  isDelete=0 " + str27 + str20 + " and u_name!='" + str14 + "'" + str15;
                        }
                        else
                        {
                            str28 = "select count(bet_id) as bs,sum(" + str24 + "* (CASE WHEN (isTie=1) THEN 0 ELSE amount END) ) uphl,sum(CASE WHEN sale_type=1 THEN 0 ELSE (CASE WHEN (isTie=1) THEN 0 ELSE amount END)  END)as hyzt,  ISNULL(sum(CASE WHEN  sale_type=1 THEN 0 ELSE profit END),'0') as hyjg , sum((CASE WHEN (isTie=1) THEN 0 ELSE amount END) *" + str12 + "_rate/100)as szzt,  ISNULL(sum(profit_zj*" + str12 + "_rate/100),'0')as szjg,ISNULL(sum(profit_zj*" + str24 + "),'0')as upjg ,ISNULL(sum(profit_zj*" + str23 + "),0) as ysze,ISNULL(abs(sum((CASE WHEN (isTie=1) THEN 0 ELSE amount END) * " + str23 + " * (CASE WHEN is_payment=0 THEN 0 ELSE " + str21 + "/100 END))) ,'0') as ysds,ISNULL(abs(sum((CASE WHEN (isTie=1) THEN 0 ELSE amount END) * " + str12 + "_rate/100 * (CASE WHEN is_payment=0 THEN 0 ELSE " + str22 + "/100 END))) ,'0') as mygetds,ISNULL(abs(sum((CASE WHEN (isTie=1) THEN 0 ELSE amount END) * " + str12 + "_rate/100 * (CASE WHEN is_payment=0 THEN 0 ELSE " + str21 + "/100 END))) ,'0') as mypayds,ISNULL(abs(sum((CASE WHEN (isTie=1) THEN 0 ELSE amount END) * " + str12 + "_rate/100  * (CASE WHEN is_payment=0 THEN 0 ELSE " + str21 + "/100 END))) ,'0') as szds,max(m_type) m_type," + str13 + " from  ( select * from   " + str2 + "  where  isDelete=0 " + str27 + str20 + " and fgs_name!='" + str14 + "'" + str15 + " ) as bet group by " + str13;
                            str29 = "select ISNULL(sum((CASE WHEN (isTie=1) THEN 0 ELSE amount END) *" + str12 + "_rate/100),'0') as szzt,ISNULL(sum(profit_zj*" + str12 + "_rate/100),'0')as szjg,ISNULL(abs(sum((CASE WHEN (isTie=1) THEN 0 ELSE amount END) * zj_rate/100 * (CASE WHEN is_payment=0 THEN 0 ELSE fgs_drawback/100 END))) ,'0') as ysds  from  " + str2 + "  where  isDelete=0 " + str27 + str20 + " and fgs_name!='" + str14 + "'" + str15;
                        }
                        if (str28 != "")
                        {
                            str28 = "select  a.*,cz_users.u_nicker a_t_name,ISNULL(cz_users.u_type,'') u_type  from cz_users inner join (" + str28 + ") a on a." + str13 + "=cz_users.u_name  order by " + str13;
                        }
                        if (str12 == "zj")
                        {
                            double num2 = 0.0;
                            double num3 = 0.0;
                            double num4 = 0.0;
                            double num5 = 0.0;
                            double num6 = 0.0;
                            double num7 = 0.0;
                            double num8 = 0.0;
                            double num9 = 0.0;
                            double num10 = 0.0;
                            double num11 = 0.0;
                            double num12 = 0.0;
                            double num13 = 0.0;
                            int num14 = 0;
                            double num15 = 0.0;
                            double num16 = 0.0;
                            double num17 = 0.0;
                            double num18 = 0.0;
                            double num19 = 0.0;
                            double num20 = 0.0;
                            double num21 = 0.0;
                            double num22 = 0.0;
                            double num23 = 0.0;
                            double num24 = 0.0;
                            double num25 = 0.0;
                            double num26 = 0.0;
                            double num27 = 0.0;
                            int num28 = 0;
                            DataTable table5 = DbHelperSQL.Query(str29, list.ToArray()).Tables[0];
                            double num29 = Math.Round(double.Parse(table5.Rows[0][0].ToString()), 1);
                            Math.Round((double) (double.Parse(table5.Rows[0][1].ToString()) + double.Parse(table5.Rows[0][2].ToString())), 1);
                            table5 = DbHelperSQL.Query(str28, list.ToArray()).Tables[0];
                            num2 = 0.0;
                            num3 = 0.0;
                            num4 = 0.0;
                            num5 = 0.0;
                            num6 = 0.0;
                            num7 = 0.0;
                            num8 = 0.0;
                            num9 = 0.0;
                            num10 = 0.0;
                            num11 = 0.0;
                            num12 = 0.0;
                            num13 = 0.0;
                            num14 = 0;
                            num15 = 0.0;
                            num16 = 0.0;
                            num17 = 0.0;
                            num18 = 0.0;
                            num19 = 0.0;
                            num20 = 0.0;
                            num21 = 0.0;
                            num22 = 0.0;
                            num23 = 0.0;
                            num24 = 0.0;
                            num25 = 0.0;
                            num26 = 0.0;
                            num27 = 0.0;
                            num28 = 0;
                            Dictionary<string, object> data = new Dictionary<string, object>();
                            Dictionary<string, List<Dictionary<string, string>>> dictionary2 = new Dictionary<string, List<Dictionary<string, string>>>();
                            List<Dictionary<string, string>> list2 = new List<Dictionary<string, string>>();
                            List<Dictionary<string, string>> list3 = new List<Dictionary<string, string>>();
                            Dictionary<string, List<Dictionary<string, string>>> dictionary3 = new Dictionary<string, List<Dictionary<string, string>>>();
                            List<Dictionary<string, string>> list4 = new List<Dictionary<string, string>>();
                            List<Dictionary<string, string>> list5 = new List<Dictionary<string, string>>();
                            Dictionary<string, List<Dictionary<string, string>>> dictionary4 = new Dictionary<string, List<Dictionary<string, string>>>();
                            List<Dictionary<string, string>> list6 = new List<Dictionary<string, string>>();
                            new Dictionary<string, List<Dictionary<string, string>>>();
                            new List<Dictionary<string, string>>();
                            foreach (DataRow row in table5.Rows)
                            {
                                num14 = int.Parse(row["bs"].ToString());
                                num2 = Math.Round(double.Parse(row["hyzt"].ToString()), 1);
                                num3 = Math.Round(double.Parse(row["hyjg"].ToString()), 1);
                                num8 = Math.Round(double.Parse(row["szzt"].ToString()), 1);
                                num9 = Math.Round(double.Parse(row["szjg"].ToString()), 1);
                                if (num2 > 0.0)
                                {
                                    num7 = Math.Round((double) ((num8 / num2) * 100.0), 2);
                                }
                                else
                                {
                                    num7 = 0.0;
                                }
                                num4 = Math.Round(double.Parse(row["ysze"].ToString()), 1);
                                num5 = Math.Round(double.Parse(row["ysds"].ToString()), 1);
                                num6 = Math.Round(double.Parse(row["szds"].ToString()), 1);
                                num10 = Math.Round(double.Parse(row["mygetds"].ToString()), 1);
                                num11 = Math.Round(double.Parse(row["mypayds"].ToString()), 1);
                                if ((num8 > 0.0) && (num29 > 0.0))
                                {
                                    num27 = Math.Round((double) ((num8 / num29) * 100.0), 2);
                                }
                                else
                                {
                                    num27 = 0.0;
                                }
                                Dictionary<string, string> dictionary5 = new Dictionary<string, string>();
                                if (str12 == "dl")
                                {
                                    dictionary5.Add("uname", row[str13].ToString());
                                }
                                else
                                {
                                    string str31 = "";
                                    if (base.q("t_FT") == "0")
                                    {
                                        str31 = base.q("t_LID");
                                    }
                                    string str32 = string.Format("action=SearchDataSix&t_TYPE=1&a_name={0}&m_type=fgs&gID={1}&t_FT={2}&t_LID={3}&BeginDate={4}&EndDate={5}&t_Balance={6}&ReportType=B", new object[] { row[str13].ToString(), str4, base.q("t_FT"), str31, str7, str8, str9 });
                                    dictionary5.Add("uname", row[str13].ToString());
                                    dictionary5.Add("uname_href", str32);
                                }
                                dictionary5.Add("nicker", row["a_t_name"].ToString());
                                dictionary5.Add("bs", num14.ToString());
                                dictionary5.Add("yxje", string.Format("{0:F1}", num2));
                                string str33 = string.Format("action=SearchDetailSix&UP_1_ID={0}&t_LT=1&m_type=fgs&t_FT={1}&gID={2}&t_LID={3}&BeginDate={4}&EndDate={5}&t_Balance={6}&ReportType=B", new object[] { row[str13].ToString(), base.q("t_FT"), str4, (base.q("t_FT") == "0") ? base.q("t_LID") : "", str7, str8, str9 });
                                dictionary5.Add("yxje_href", str33);
                                dictionary5.Add("hysy", string.Format("{0:F1}", num3));
                                dictionary5.Add("ysxx", string.Format("{0:F1}", num4 + num5));
                                dictionary5.Add("zc", num7.ToString() + "%");
                                dictionary5.Add("szze", string.Format("{0:F1}", num8));
                                dictionary5.Add("zhb", num27 + "%");
                                dictionary5.Add("szsy", string.Format("{0:F1}", num9));
                                dictionary5.Add("szts", string.Format("{0:F1}", num6));
                                dictionary5.Add("szjg", string.Format("{0:F1}", num9 + num6));
                                list2.Add(dictionary5);
                                num28++;
                                num15 += num2;
                                num16 += num3;
                                num17 += num4;
                                num18 += num5;
                                num19 += num6;
                                num23 += num14;
                                num21 += num8;
                                num22 += num9;
                                num12 += num10;
                                num13 += num11;
                            }
                            if (num15 > 0.0)
                            {
                                num20 = Math.Round((double) ((num21 / num15) * 100.0), 2);
                            }
                            else
                            {
                                num20 = 0.0;
                            }
                            Dictionary<string, string> dictionary6 = new Dictionary<string, string>();
                            dictionary6.Add("uname", "合計：");
                            dictionary6.Add("nicker", "");
                            dictionary6.Add("bs", num23.ToString());
                            dictionary6.Add("yxje", string.Format("{0:F1}", num15));
                            dictionary6.Add("hysy", string.Format("{0:F1}", num16));
                            dictionary6.Add("ysxx", string.Format("{0:F1}", num17 + num18));
                            dictionary6.Add("zc", string.Format("{0:F1}", num20));
                            dictionary6.Add("szze", string.Format("{0:F1}", num21));
                            dictionary6.Add("szsy", string.Format("{0:F1}", num22));
                            dictionary6.Add("szts", string.Format("{0:F1}", num19));
                            dictionary6.Add("szjg", string.Format("{0:F1}", num22 + num19));
                            dictionary6.Add("zhb", "");
                            list3.Add(dictionary6);
                            dictionary2.Add("listreport", list2);
                            dictionary2.Add("listcount", list3);
                            data.Add("listdata", dictionary2);
                            num = num22 + num19;
                            double num30 = 0.0;
                            double num31 = 0.0;
                            double num32 = 0.0;
                            double num33 = 0.0;
                            table5 = DbHelperSQL.Query("select  a1.*,cz_saleset_six.u_name,cz_saleset_six.u_nicker from  (select u_name, count(bet_id) as bs,sum((CASE WHEN (isTie=1) THEN 0 ELSE amount END) ) zt, sum(profit) result ,ISNULL(abs(sum((CASE WHEN (isTie=1) THEN 0 ELSE amount END)  * (CASE WHEN is_payment=0 THEN 0 ELSE " + str12 + "_drawback/100 END))) ,'0') as ysds from  " + str2 + "  where  isDelete=0 " + str27 + " and  fgs_name='" + str14 + "'" + str15 + " group by u_name) a1 left join cz_saleset_six on a1.u_name=cz_saleset_six.u_name", list.ToArray()).Tables[0];
                            Dictionary<string, string> dictionary7 = new Dictionary<string, string>();
                            if ((table5.Rows.Count > 0) && (int.Parse(table5.Rows[0]["bs"].ToString().Trim()) > 0))
                            {
                                if (_session.get_u_type().Equals("fgs"))
                                {
                                    _session.get_allow_view_report().ToString().Equals("1");
                                }
                                num23 = 0.0;
                                foreach (DataRow row2 in table5.Rows)
                                {
                                    Dictionary<string, string> dictionary8 = new Dictionary<string, string>();
                                    num24 = Math.Round(double.Parse("-" + row2["zt"].ToString()), 1);
                                    num25 = Math.Round(double.Parse(row2["result"].ToString()), 1);
                                    num26 = Math.Round(double.Parse(row2["ysds"].ToString()), 1);
                                    num23 += int.Parse(row2["bs"].ToString());
                                    num31 += num24;
                                    num30 += num23;
                                    num32 += num25;
                                    num33 += num26;
                                    if (!_session.get_u_type().Equals("zj"))
                                    {
                                        str14 = "";
                                    }
                                    dictionary8.Add("uname", row2["u_name"].ToString());
                                    dictionary8.Add("nicker", row2["u_nicker"].ToString());
                                    dictionary8.Add("bs", row2["bs"].ToString());
                                    string str34 = string.Format("action=SearchDetailSix&uID={0}&UP_1_ID={1}&m_type=fgs&t_FT={2}&gID={3}&t_LID={4}&BeginDate={5}&EndDate={6}&t_Balance={7}&zjbh=1&ReportType=B", new object[] { str14, row2["u_name"].ToString(), base.q("t_FT"), str4, (base.q("t_FT") == "0") ? base.q("t_LID") : "", str7, str8, str9 });
                                    dictionary8.Add("bs_href", str34);
                                    dictionary8.Add("bhje", string.Format("{0:F1}", num24));
                                    string str35 = string.Format("action=SearchDetailSix&uID={0}&UP_1_ID={1}&m_type=fgs&t_FT={2}&gID={3}&t_LID={4}&BeginDate={5}&EndDate={6}&t_Balance={7}&zjbh=1&ReportType=B", new object[] { str14, row2["u_name"].ToString(), base.q("t_FT"), str4, (base.q("t_FT") == "0") ? base.q("t_LID") : "", str7, str8, str9 });
                                    dictionary8.Add("bhje_href", string.Format("{0:F1}", str35));
                                    dictionary8.Add("zbl", "100%");
                                    dictionary8.Add("bhsy", string.Format("{0:F1}", num25));
                                    dictionary8.Add("ts", string.Format("{0:F1}", num26));
                                    dictionary8.Add("tshjg", string.Format("{0:F1}", num25 + num26));
                                    list4.Add(dictionary8);
                                }
                                Dictionary<string, string> dictionary9 = new Dictionary<string, string>();
                                dictionary9.Add("uname", "合計:");
                                dictionary9.Add("nicker", "");
                                dictionary9.Add("bs", string.Format("{0:F1}", num30));
                                dictionary9.Add("bhje", string.Format("{0:F1}", num31));
                                dictionary9.Add("zbl", "100%");
                                dictionary9.Add("bhsy", string.Format("{0:F1}", num32));
                                dictionary9.Add("ts", string.Format("{0:F1}", num33));
                                dictionary9.Add("tshjg", string.Format("{0:F1}", num32 + num33));
                                list5.Add(dictionary9);
                                dictionary3.Add("listreportbh", list4);
                                dictionary3.Add("listcountbh", list5);
                                data.Add("listdatabh", dictionary3);
                                if (_session.get_u_type().Equals("fgs"))
                                {
                                    _session.get_allow_view_report().ToString().Equals("1");
                                }
                                dictionary7.Add("szze", string.Format("{0:F1}", num21 + num31));
                                dictionary7.Add("syjg", string.Format("{0:F1}", num22 + -num32));
                                dictionary7.Add("ts", string.Format("{0:F1}", num19 + -num33));
                                dictionary7.Add("tshjg", string.Format("{0:F1}", (num22 + -num32) + (num19 + -num33)));
                            }
                            dictionary7.Add("zcjg", string.Format("{0:F1}", num));
                            dictionary7.Add("dkbhhjg", string.Format("{0:F1}", (num22 + -num32) + (num19 + -num33)));
                            list6.Add(dictionary7);
                            dictionary4.Add("listresult", list6);
                            data.Add("listdataresult", dictionary4);
                            data.Add("memberId", "fgs");
                            base.successOptMsg("交收報表查詢", data);
                        }
                        else
                        {
                            double num34 = 0.0;
                            double num35 = 0.0;
                            double num36 = 0.0;
                            double num37 = 0.0;
                            double num38 = 0.0;
                            double num39 = 0.0;
                            double num40 = 0.0;
                            double num41 = 0.0;
                            double num42 = 0.0;
                            double num43 = 0.0;
                            double num44 = 0.0;
                            double num45 = 0.0;
                            int num46 = 0;
                            double num47 = 0.0;
                            double num48 = 0.0;
                            double num49 = 0.0;
                            double num50 = 0.0;
                            double num51 = 0.0;
                            double num52 = 0.0;
                            double num53 = 0.0;
                            double num54 = 0.0;
                            double num55 = 0.0;
                            double num56 = 0.0;
                            double num57 = 0.0;
                            double num58 = 0.0;
                            double num59 = 0.0;
                            double num60 = 0.0;
                            double num61 = 0.0;
                            double num62 = 0.0;
                            double num63 = 0.0;
                            int num64 = 0;
                            double num65 = 0.0;
                            double num66 = 0.0;
                            DataTable table6 = DbHelperSQL.Query(str29, list.ToArray()).Tables[0];
                            double num67 = Math.Round(double.Parse(table6.Rows[0][0].ToString()), 1);
                            table6 = DbHelperSQL.Query(str28, list.ToArray()).Tables[0];
                            num34 = 0.0;
                            num35 = 0.0;
                            num36 = 0.0;
                            num37 = 0.0;
                            num38 = 0.0;
                            num39 = 0.0;
                            num40 = 0.0;
                            num41 = 0.0;
                            num42 = 0.0;
                            num43 = 0.0;
                            num44 = 0.0;
                            num45 = 0.0;
                            num46 = 0;
                            num47 = 0.0;
                            num48 = 0.0;
                            num49 = 0.0;
                            num50 = 0.0;
                            num51 = 0.0;
                            num52 = 0.0;
                            num53 = 0.0;
                            num54 = 0.0;
                            num55 = 0.0;
                            num56 = 0.0;
                            num57 = 0.0;
                            num58 = 0.0;
                            num59 = 0.0;
                            num60 = 0.0;
                            num61 = 0.0;
                            num62 = 0.0;
                            num63 = 0.0;
                            num64 = 0;
                            num65 = 0.0;
                            Dictionary<string, object> dictionary10 = new Dictionary<string, object>();
                            Dictionary<string, List<Dictionary<string, string>>> dictionary11 = new Dictionary<string, List<Dictionary<string, string>>>();
                            List<Dictionary<string, string>> list7 = new List<Dictionary<string, string>>();
                            List<Dictionary<string, string>> list8 = new List<Dictionary<string, string>>();
                            Dictionary<string, List<Dictionary<string, string>>> dictionary12 = new Dictionary<string, List<Dictionary<string, string>>>();
                            List<Dictionary<string, string>> list9 = new List<Dictionary<string, string>>();
                            List<Dictionary<string, string>> list10 = new List<Dictionary<string, string>>();
                            new Dictionary<string, List<Dictionary<string, string>>>();
                            new List<Dictionary<string, string>>();
                            Dictionary<string, List<Dictionary<string, string>>> dictionary13 = new Dictionary<string, List<Dictionary<string, string>>>();
                            List<Dictionary<string, string>> list11 = new List<Dictionary<string, string>>();
                            foreach (DataRow row3 in table6.Rows)
                            {
                                num46 = int.Parse(row3["bs"].ToString());
                                num34 = Math.Round(double.Parse(row3["hyzt"].ToString()), 1);
                                num35 = Math.Round(double.Parse(row3["hyjg"].ToString()), 1);
                                num41 = Math.Round(double.Parse(row3["szzt"].ToString()), 1);
                                num42 = Math.Round(double.Parse(row3["szjg"].ToString()), 1);
                                if (num34 > 0.0)
                                {
                                    num40 = Math.Round((double) ((num41 / num34) * 100.0), 2);
                                }
                                else
                                {
                                    num40 = 0.0;
                                }
                                num36 = Math.Round(double.Parse(row3["ysze"].ToString()), 1);
                                num37 = Math.Round(double.Parse(row3["ysds"].ToString()), 1);
                                num38 = Math.Round(double.Parse(row3["szds"].ToString()), 1);
                                num39 = Math.Round(double.Parse(row3["sz_payds"].ToString()), 1);
                                num43 = Math.Round(double.Parse(row3["mygetds"].ToString()), 1);
                                num44 = Math.Round(double.Parse(row3["mypayds"].ToString()), 1);
                                num45 = Math.Round(double.Parse(row3["upjg"].ToString()), 1);
                                Dictionary<string, string> dictionary14 = new Dictionary<string, string>();
                                if ((num41 > 0.0) && (num67 > 0.0))
                                {
                                    num63 = Math.Round((double) ((num41 / num67) * 100.0), 2);
                                }
                                else
                                {
                                    num63 = 0.0;
                                }
                                dictionary14.Add("uname", row3[str13].ToString());
                                string str36 = "";
                                if ((str12 != "dl") || (row3["u_type"].ToString().Trim() != "hy"))
                                {
                                    if ((((str12 == "zd") || (str12 == "gd")) || (str12 == "fgs")) && (row3["u_type"].ToString().Trim() == "hy"))
                                    {
                                        str36 = "zs";
                                    }
                                    else
                                    {
                                        string str37 = string.Format("action=SearchDataSix&t_TYPE=1&a_name={0}&m_type={1}&gID={2}&t_FT={3}&t_LID={4}&BeginDate={5}&EndDate={6}&t_Balance={7}&ReportType=B", new object[] { row3[str13].ToString(), str19, str4, base.q("t_FT"), (base.q("t_FT") == "0") ? base.q("t_LID") : "", str7, str8, str9 });
                                        dictionary14.Add("uname_href", str37);
                                    }
                                }
                                dictionary14.Add("hytype", str36);
                                dictionary14.Add("nicker", row3["a_t_name"].ToString());
                                dictionary14.Add("bs", num46.ToString());
                                dictionary14.Add("yxje", string.Format("{0:F1}", num34));
                                string str38 = string.Format("action=SearchDetailSix&UP_1_ID={0}&t_LT=1&m_type={1}&t_FT={2}&gID={3}&t_LID={4}&BeginDate={5}&EndDate={6}&t_Balance={7}&ReportType=B", new object[] { row3[str13].ToString(), str19, base.q("t_FT"), str4, (base.q("t_FT") == "0") ? base.q("t_LID") : "", str7, str8, str9 });
                                dictionary14.Add("yxje_href", str38);
                                dictionary14.Add("hysy", num35.ToString());
                                dictionary14.Add("ysxx", string.Format("{0:F1}", num36 + num37));
                                dictionary14.Add("zc", num40.ToString() + "%");
                                dictionary14.Add("szze", string.Format("{0:F1}", num41));
                                dictionary14.Add("zhb", num63.ToString() + "%");
                                dictionary14.Add("szsy", string.Format("{0:F1}", num42));
                                dictionary14.Add("szts", string.Format("{0:F1}", num38));
                                dictionary14.Add("szjg", string.Format("{0:F1}", num42 + num38));
                                dictionary14.Add("zqts", string.Format("{0:F1}", (num44 - num43) + (num39 - num38)));
                                if (str12 == "fgs")
                                {
                                    dictionary14.Add("zqpl", string.Format("{0:F1}", (num36 - num42) - num45));
                                }
                                dictionary14.Add("sjjg", string.Format("{0:F1}", ((num42 + num38) + ((num44 - num43) + (num39 - num38))) + ((num36 - num42) - num45)));
                                dictionary14.Add("gxsj", Math.Round(double.Parse(row3["uphl"].ToString()), 1).ToString());
                                dictionary14.Add("yfsj", string.Format("{0:F1}", (num36 + num37) - (((num42 + num38) + ((num44 - num43) + (num39 - num38))) + ((num36 - num42) - num45))));
                                list7.Add(dictionary14);
                                num64++;
                                num47 += num34;
                                num48 += num35;
                                num49 += num36;
                                num50 += num37;
                                num51 += num38;
                                num52 += num39;
                                num57 += num43;
                                num58 += num44;
                                num56 += num46;
                                num54 += num41;
                                num55 += num42;
                                num53 += num40;
                                num59 += num45;
                                num65 += double.Parse(row3["uphl"].ToString());
                            }
                            if (num47 > 0.0)
                            {
                                num53 = Math.Round((double) ((num54 / num47) * 100.0), 2);
                            }
                            else
                            {
                                num53 = 0.0;
                            }
                            Dictionary<string, string> dictionary15 = new Dictionary<string, string>();
                            dictionary15.Add("uname", "合計：");
                            dictionary15.Add("nicker", "");
                            dictionary15.Add("bs", num56.ToString());
                            dictionary15.Add("yxje", string.Format("{0:F1}", num47));
                            dictionary15.Add("hysy", string.Format("{0:F1}", num48));
                            dictionary15.Add("ysxx", string.Format("{0:F1}", num49 + num50));
                            dictionary15.Add("zc", num53.ToString());
                            dictionary15.Add("szze", string.Format("{0:F1}", num54));
                            dictionary15.Add("zhb", "");
                            dictionary15.Add("szsy", string.Format("{0:F1}", num55));
                            dictionary15.Add("szts", string.Format("{0:F1}", num51));
                            dictionary15.Add("szjg", string.Format("{0:F1}", num55 + num51));
                            dictionary15.Add("zqts", string.Format("{0:F1}", (num58 - num57) + (num52 - num51)));
                            if (str12 == "fgs")
                            {
                                dictionary15.Add("zqpl", string.Format("{0:F1}", (num49 - num55) - num59));
                            }
                            dictionary15.Add("sjjg", string.Format("{0:F1}", ((num55 + num51) + ((num58 - num57) + (num52 - num51))) + ((num49 - num55) - num59)));
                            dictionary15.Add("gxsj", string.Format("{0:F1}", num65));
                            dictionary15.Add("yfsj", string.Format("{0:F1}", (num49 + num50) - (((num55 + num51) + ((num58 - num57) + (num52 - num51))) + ((num49 - num55) - num59))));
                            list8.Add(dictionary15);
                            dictionary11.Add("listreport", list7);
                            dictionary11.Add("listcount", list8);
                            dictionary10.Add("listdata", dictionary11);
                            table6 = DbHelperSQL.Query("select count(bet_id) as bs,sum((CASE WHEN (isTie=1) THEN 0 ELSE amount END) ) zt, sum(" + str30 + ") result ,ISNULL(abs(sum((CASE WHEN (isTie=1) THEN 0 ELSE amount END)  * (CASE WHEN is_payment=0 THEN 0 ELSE " + str12 + "_drawback/100 END))) ,'0') as ysds from  " + str2 + "  where  isDelete=0  " + str27 + " and  u_name='" + str14 + "' " + str15, list.ToArray()).Tables[0];
                            num66 = 0.0;
                            if (((table6.Rows.Count > 0) && (int.Parse(table6.Rows[0]["bs"].ToString().Trim()) > 0)) && (int.Parse(table6.Rows[0]["bs"].ToString()) > 0))
                            {
                                num60 = Math.Round(double.Parse("-" + table6.Rows[0]["zt"].ToString()), 1);
                                num61 = Math.Round(double.Parse(table6.Rows[0]["result"].ToString()), 1);
                                num62 = Math.Round(double.Parse(table6.Rows[0]["ysds"].ToString()), 1);
                                num66 += double.Parse(table6.Rows[0]["bs"].ToString());
                            }
                            Dictionary<string, string> dictionary16 = new Dictionary<string, string>();
                            if (num66 > 0.0)
                            {
                                dictionary16.Add("bs", num66.ToString());
                                dictionary16.Add("bhje", string.Format("{0:F1}", num60));
                                string str39 = string.Format("action=SearchDetailSix&uID={0}&t_LT=1&gID={1}&m_type={2}&t_FT={3}&t_LID={4}&BeginDate={5}&EndDate={6}&t_Balance={7}&ReportType=B", new object[] { str14, str4, str19, base.q("t_FT"), (base.q("t_FT") == "0") ? base.q("t_LID") : "", str7, str8, str9 });
                                dictionary16.Add("bhje_href", str39);
                                dictionary16.Add("bhsy", string.Format("{0:F1}", num61));
                                dictionary16.Add("ts", string.Format("{0:F1}", num62));
                                dictionary16.Add("tshjg", string.Format("{0:F1}", num61 + num62));
                                list9.Add(dictionary16);
                            }
                            dictionary12.Add("listreportbh", list9);
                            dictionary12.Add("listcountbh", list10);
                            dictionary10.Add("listdatabh", dictionary12);
                            Dictionary<string, string> dictionary17 = new Dictionary<string, string>();
                            dictionary17.Add("zcjg", string.Format("{0:F1}", num55 + num51));
                            dictionary17.Add("zqts", string.Format("{0:F1}", (num58 - num57) + (num52 - num51)));
                            dictionary17.Add("dkbhjzshjg", string.Format("{0:F1}", ((num55 + -num61) + (num51 - num62)) + ((num58 - num57) + (num52 - num51))));
                            dictionary17.Add("dkbhhzcjg", string.Format("{0:F1}", (num55 + -num61) + (num51 - num62)));
                            dictionary17.Add("yfsj", string.Format("{0:F1}", (num59 + num61) + (num57 + num62)));
                            list11.Add(dictionary17);
                            dictionary13.Add("listresult", list11);
                            dictionary10.Add("listdataresult", dictionary13);
                            dictionary10.Add("memberId", str19);
                            base.successOptMsg("交收報表查詢", dictionary10);
                        }
                    }
                }
            }
        }

        public void ReportDataSix_T()
        {
            agent_userinfo_session _session = this.Session[this.Session["user_name"] + "lottery_session_user_info"] as agent_userinfo_session;
            if ((_session.get_u_type().ToLower().Equals("zj") && (_session.get_users_child_session() != null)) && (_session.get_users_child_session().get_permissions_name().IndexOf("po_4_2") < 0))
            {
                base.noRightOptMsg(PageBase.GetMessageByCache("u100057", "MessageHint"));
            }
            else if ((!_session.get_u_type().ToLower().Equals("zj") && (_session.get_users_child_session() != null)) && (_session.get_users_child_session().get_permissions_name().IndexOf("po_7_2") < 0))
            {
                base.noRightOptMsg(PageBase.GetMessageByCache("u100057", "MessageHint"));
            }
            else
            {
                SqlParameter[] parameterArray3;
                List<SqlParameter> list = new List<SqlParameter>();
                SqlParameter item = new SqlParameter();
                string str = "";
                string str2 = "";
                string str3 = "select is_closed,p_id from cz_phase_six where is_opendata=0";
                DataTable table = DbHelperSQL.Query(str3, null).Tables[0];
                string str4 = base.q("gID");
                string str5 = base.q("t_FT");
                string str6 = base.q("t_LID");
                string str7 = base.q("BeginDate");
                string str8 = base.q("EndDate");
                string str9 = base.q("t_Balance");
                if (!str5.Equals("0"))
                {
                    DateTime time = Convert.ToDateTime(str7);
                    if (!string.IsNullOrEmpty(this.GetReportOpenDateSix()))
                    {
                        DateTime time2 = Convert.ToDateTime(this.GetReportOpenDateSix());
                        if (time < time2)
                        {
                            str7 = time2.ToString("yyyy-MM-dd");
                            base.noRightOptMsg(string.Format(PageBase.GetMessageByCache("u100065", "MessageHint"), str7));
                            return;
                        }
                    }
                }
                if (str5.Trim() == "0")
                {
                    string str10 = "select phase from cz_phase_six where p_id=@cxqs";
                    parameterArray3 = new SqlParameter[1];
                    SqlParameter parameter2 = new SqlParameter("@cxqs", SqlDbType.NVarChar) {
                        Value = str6
                    };
                    parameterArray3[0] = parameter2;
                    SqlParameter[] parameterArray = parameterArray3;
                    DataTable table2 = DbHelperSQL.Query(str10, parameterArray).Tables[0];
                    table2.Rows[0][0].ToString();
                }
                if (table.Rows.Count > 0)
                {
                    if ((table.Rows[0][0].ToString().Trim() == "1") && (_session.get_u_type() != "zj"))
                    {
                        base.noRightOptMsg(string.Format(PageBase.GetMessageByCache("u100067", "MessageHint"), new object[0]));
                        return;
                    }
                    if (str5.Trim() == "0")
                    {
                        if (string.IsNullOrEmpty(str6))
                        {
                            base.Response.End();
                        }
                        if (str6.Trim() != table.Rows[0]["p_id"].ToString().Trim())
                        {
                            base.noRightOptMsg(string.Format(PageBase.GetMessageByCache("u100068", "MessageHint"), new object[0]));
                            return;
                        }
                    }
                    else
                    {
                        string str11 = DateTime.Now.ToString("yyyy-MM-dd");
                        if (str7 != str11)
                        {
                            base.noRightOptMsg(string.Format(PageBase.GetMessageByCache("u100068", "MessageHint"), new object[0]));
                            return;
                        }
                    }
                    str2 = " cz_bet_six  with(NOLOCK)  ";
                }
                else
                {
                    str2 = " cz_betold_six  with(NOLOCK)  ";
                }
                string str12 = "";
                string str13 = "";
                string str14 = "";
                base.q("t_LID");
                string str15 = "";
                string str16 = "";
                string str17 = "";
                string str18 = "0";
                string str19 = "";
                if (_session.get_u_type() == "fgs")
                {
                    str18 = _session.get_allow_view_report().ToString();
                    str19 = _session.get_sup_name();
                }
                if (base.Request["a_name"] == null)
                {
                    str12 = _session.get_u_type();
                    if (str12 == "fgs")
                    {
                        if (str18 == "1")
                        {
                            str13 = str19;
                            str12 = "zj";
                        }
                        else
                        {
                            str13 = _session.get_u_name();
                        }
                    }
                    else
                    {
                        str13 = _session.get_u_name();
                    }
                }
                else
                {
                    str13 = base.q("a_name");
                    str12 = base.q("a_type");
                    string str20 = "select u_type from agent where u_name=@u_namecxqs   ";
                    parameterArray3 = new SqlParameter[1];
                    SqlParameter parameter3 = new SqlParameter("@u_namecxqs", SqlDbType.NVarChar) {
                        Value = base.q("a_name")
                    };
                    parameterArray3[0] = parameter3;
                    SqlParameter[] parameterArray2 = parameterArray3;
                    DataTable table3 = DbHelperSQL.Query(str20, parameterArray2).Tables[0];
                    str12 = table3.Rows[0][0].ToString().Trim();
                }
                if ((str18 != "1") && !base.IsUpperLowerLevels(str13, _session.get_u_type(), _session.get_u_name()))
                {
                    base.noRightOptMsg(string.Format(PageBase.GetMessageByCache("u100057", "MessageHint"), new object[0]));
                }
                else
                {
                    if (str4 != "0")
                    {
                        str14 = " and play_id=@play_id  ";
                        item = new SqlParameter("@play_id", SqlDbType.NVarChar) {
                            Value = str4
                        };
                        list.Add(item);
                    }
                    if (str9 != "")
                    {
                        str14 = str14 + " and is_payment=@is_payment   ";
                        item = new SqlParameter("@is_payment", SqlDbType.NVarChar) {
                            Value = str9
                        };
                        list.Add(item);
                    }
                    string str21 = "";
                    string str22 = "";
                    string str23 = "";
                    string str24 = "";
                    string str25 = "";
                    string str26 = "";
                    string str35 = str12;
                    if (str35 != null)
                    {
                        if (!(str35 == "zj"))
                        {
                            if (str35 == "fgs")
                            {
                                str23 = "gd_drawback";
                                str24 = "fgs_drawback";
                                str22 = "fgs_name";
                                str25 = "(100-(gd_rate+zd_rate+dl_rate))/100";
                                str26 = "(100-(fgs_rate+gd_rate+zd_rate+dl_rate))/100";
                                str21 = "gd";
                            }
                            else if (str35 == "gd")
                            {
                                str23 = "zd_drawback";
                                str24 = "gd_drawback";
                                str22 = "gd_name";
                                str25 = "(100-(zd_rate+dl_rate))/100";
                                str26 = "(100-(gd_rate+zd_rate+dl_rate))/100";
                                str21 = "zd";
                            }
                            else if (str35 == "zd")
                            {
                                str23 = "dl_drawback";
                                str24 = "zd_drawback";
                                str22 = "zd_name";
                                str25 = "(100-dl_rate)/100";
                                str26 = "(100-(zd_rate+dl_rate))/100";
                                str21 = "dl";
                            }
                            else if (str35 == "dl")
                            {
                                str23 = "hy_drawback";
                                str24 = "dl_drawback";
                                str22 = "dl_name";
                                str25 = "1";
                                str26 = "(100-dl_rate)/100";
                                str21 = "hy";
                            }
                        }
                        else
                        {
                            str23 = "fgs_drawback";
                            str24 = "zj_drawback";
                            str25 = "(100-(fgs_rate+gd_rate+zd_rate+dl_rate))/100";
                            str26 = "0";
                        }
                    }
                    if (str22 != "")
                    {
                        str22 = " and " + str22 + "='" + str13 + "'";
                    }
                    Convert.ToDateTime(DateTime.Now.ToString());
                    string str27 = "";
                    if (str5.Trim() == "0")
                    {
                        if (string.IsNullOrEmpty(str6))
                        {
                            base.Response.End();
                        }
                        else
                        {
                            str15 = base.q("t_LID");
                        }
                        str16 = "  phase_id=@phase_id  ";
                        item = new SqlParameter("@phase_id", SqlDbType.NVarChar) {
                            Value = str15
                        };
                        list.Add(item);
                        str17 = str15;
                    }
                    else
                    {
                        str15 = "  bet_time between  @BeginDate and @EndDate  ";
                        item = new SqlParameter("@BeginDate", SqlDbType.NVarChar) {
                            Value = str7 + " 00:00:00"
                        };
                        list.Add(item);
                        item = new SqlParameter("@EndDate", SqlDbType.NVarChar) {
                            Value = str8 + " 23:59:59"
                        };
                        list.Add(item);
                        str16 = str15;
                        str17 = "";
                    }
                    string str28 = "profit";
                    if (str12 != "zj")
                    {
                        if (str12 == "fgs")
                        {
                            str28 = "profit_zj";
                        }
                        str = "select count(bet_id) as bs,sum(" + str26 + "* (CASE WHEN (isTie=1) THEN 0 ELSE amount END)) uphl,sum(CASE WHEN sale_type=1 THEN 0 ELSE amount END)as hyzt,  ISNULL(sum(CASE WHEN sale_type=1 THEN 0 ELSE profit END),'0') as hyjg , sum((CASE WHEN (isTie=1) THEN 0 ELSE amount END)*" + str12 + "_rate/100) as szzt,  ISNULL(sum(profit*" + str12 + "_rate/100),'0')as szjg,ISNULL(sum(" + str28 + "*" + str26 + "),'0')as upjg ,ISNULL(sum(profit*" + str25 + "),0) as ysze,ISNULL(abs(sum((CASE WHEN (isTie=1) THEN 0 ELSE amount END)* " + str25 + " * (CASE WHEN profit=0 THEN 0 ELSE " + str23 + "/100 END))) ,'0') as ysds,ISNULL(abs(sum((CASE WHEN (isTie=1) THEN 0 ELSE amount END)* " + str26 + " * (CASE WHEN profit=0 THEN 0 ELSE " + str24 + "/100 END))) ,'0') as mygetds,ISNULL(abs(sum((CASE WHEN (isTie=1) THEN 0 ELSE amount END)* " + str26 + " * (CASE WHEN profit=0 THEN 0 ELSE " + str23 + "/100 END))) ,'0') as mypayds,ISNULL(abs(sum((CASE WHEN (isTie=1) THEN 0 ELSE amount END)* " + str12 + "_rate/100  * (CASE WHEN profit=0 THEN 0 ELSE " + str23 + "/100 END))) ,'0') as szds,play_id from  ( select * from   " + str2 + "  where " + str16 + " " + str22 + " and u_name!='" + str13 + "' and  isDelete=0 " + str14 + " ) as bet group by play_id ";
                        str27 = "select ISNULL(sum((CASE WHEN (isTie=1) THEN 0 ELSE amount END)*" + str12 + "_rate/100),'0') as szzt from  " + str2 + "  where " + str16 + " " + str22 + " and u_name!='" + str13 + "' and  isDelete=0 " + str14;
                    }
                    else
                    {
                        str = "select count(bet_id) as bs,sum(" + str26 + "* (CASE WHEN (isTie=1) THEN 0 ELSE amount END)) uphl,sum(CASE WHEN sale_type=1 THEN 0 ELSE amount END)as hyzt,  ISNULL(sum(CASE WHEN sale_type=1 THEN 0 ELSE profit_zj END),'0') as hyjg , sum((CASE WHEN (isTie=1) THEN 0 ELSE amount END)*" + str12 + "_rate/100)as szzt,  ISNULL(sum(profit_zj*" + str12 + "_rate/100),'0')as szjg,ISNULL(sum(profit_zj*" + str26 + "),'0')as upjg ,ISNULL(sum(profit_zj*" + str25 + "),0) as ysze,ISNULL(abs(sum((CASE WHEN (isTie=1) THEN 0 ELSE amount END)* " + str25 + " * (CASE WHEN is_payment=0 THEN 0 ELSE " + str23 + "/100 END))) ,'0') as ysds,ISNULL(abs(sum((CASE WHEN (isTie=1) THEN 0 ELSE amount END)* " + str26 + " * (CASE WHEN is_payment=0 THEN 0 ELSE " + str24 + "/100 END))) ,'0') as mygetds,ISNULL(abs(sum((CASE WHEN (isTie=1) THEN 0 ELSE amount END)* " + str26 + " * (CASE WHEN is_payment=0 THEN 0 ELSE " + str23 + "/100 END))) ,'0') as mypayds,ISNULL(abs(sum((CASE WHEN (isTie=1) THEN 0 ELSE amount END)* " + str12 + "_rate/100  * (CASE WHEN is_payment=0 THEN 0 ELSE " + str23 + "/100 END))) ,'0') as szds,play_id from  ( select * from  " + str2 + "  where" + str16 + " " + str22 + " and fgs_name!='" + str13 + "' and  isDelete=0 " + str14 + " ) as bet group by play_id ";
                        str27 = "select ISNULL(sum((CASE WHEN (isTie=1) THEN 0 ELSE amount END)*" + str12 + "_rate/100),'0') as szzt from  " + str2 + "  where " + str16 + " " + str22 + " and fgs_name!='" + str13 + "' and  isDelete=0 " + str14;
                    }
                    str = "select  a.*,cz_play_six.play_name from (" + str + ") a  inner join cz_play_six  on a.play_id=cz_play_six.play_id  order by cz_play_six.sort";
                    DataTable table4 = DbHelperSQL.Query(str27, list.ToArray()).Tables[0];
                    double num = Math.Round(double.Parse(table4.Rows[0][0].ToString()), 1);
                    table4 = DbHelperSQL.Query(str, list.ToArray()).Tables[0];
                    DateTime now = DateTime.Now;
                    if (str8.Trim() == "")
                    {
                        str7 = now.ToString("yyyy-MM-dd");
                        str8 = now.ToString("yyyy-MM-dd");
                    }
                    if (str12 == "zj")
                    {
                        double num2 = 0.0;
                        double num3 = 0.0;
                        double num4 = 0.0;
                        double num5 = 0.0;
                        double num6 = 0.0;
                        double num7 = 0.0;
                        double num8 = 0.0;
                        double num9 = 0.0;
                        int num10 = 0;
                        double num11 = 0.0;
                        double num12 = 0.0;
                        double num13 = 0.0;
                        double num14 = 0.0;
                        double num15 = 0.0;
                        double num16 = 0.0;
                        double num17 = 0.0;
                        double num18 = 0.0;
                        double num19 = 0.0;
                        double num20 = 0.0;
                        double num21 = 0.0;
                        double num22 = 0.0;
                        double num23 = 0.0;
                        int num24 = 0;
                        Dictionary<string, object> data = new Dictionary<string, object>();
                        Dictionary<string, List<Dictionary<string, string>>> dictionary2 = new Dictionary<string, List<Dictionary<string, string>>>();
                        List<Dictionary<string, string>> list2 = new List<Dictionary<string, string>>();
                        List<Dictionary<string, string>> list3 = new List<Dictionary<string, string>>();
                        Dictionary<string, List<Dictionary<string, string>>> dictionary3 = new Dictionary<string, List<Dictionary<string, string>>>();
                        List<Dictionary<string, string>> list4 = new List<Dictionary<string, string>>();
                        List<Dictionary<string, string>> list5 = new List<Dictionary<string, string>>();
                        Dictionary<string, List<Dictionary<string, string>>> dictionary4 = new Dictionary<string, List<Dictionary<string, string>>>();
                        List<Dictionary<string, string>> list6 = new List<Dictionary<string, string>>();
                        foreach (DataRow row in table4.Rows)
                        {
                            num10 = int.Parse(row["bs"].ToString());
                            num2 = Math.Round(double.Parse(row["hyzt"].ToString()), 1);
                            num3 = Math.Round(double.Parse(row["hyjg"].ToString()), 1);
                            num8 = Math.Round(double.Parse(row["szzt"].ToString()), 1);
                            num9 = Math.Round(double.Parse(row["szjg"].ToString()), 1);
                            if (num2 > 0.0)
                            {
                                num7 = Math.Round((double) ((num8 / num2) * 100.0), 3);
                            }
                            else
                            {
                                num7 = 0.0;
                            }
                            num4 = Math.Round(double.Parse(row["ysze"].ToString()), 1);
                            num5 = Math.Round(double.Parse(row["ysds"].ToString()), 1);
                            num6 = Math.Round(double.Parse(row["szds"].ToString()), 1);
                            if ((num8 > 0.0) && (num > 0.0))
                            {
                                num23 = Math.Round((double) ((num8 / num) * 100.0), 2);
                            }
                            else
                            {
                                num23 = 0.0;
                            }
                            Dictionary<string, string> dictionary5 = new Dictionary<string, string>();
                            int num62 = 100;
                            dictionary5.Add("xzlx", string.Format("『" + base.GetGameNameByID(num62.ToString()) + " " + row["play_name"].ToString(), new object[0]));
                            dictionary5.Add("bs", num10.ToString());
                            dictionary5.Add("zxze", num2.ToString());
                            string str29 = string.Format("action=SearchDetailSix&gID={0}&t_LT=1&t_FT={1}&t_LID={2}&BeginDate={3}&EndDate={4}&ReportType=T", new object[] { row["play_id"].ToString(), str5, str17, str7, str8 });
                            dictionary5.Add("zxze_href", str29);
                            dictionary5.Add("hysy", string.Format("{0:F1}", num3));
                            dictionary5.Add("ysxx", string.Format("{0:F1}", num4 + num5));
                            dictionary5.Add("zc", num7.ToString());
                            dictionary5.Add("szze", string.Format("{0:F1}", num8));
                            dictionary5.Add("zhb", num23.ToString() + "%");
                            dictionary5.Add("szsy", string.Format("{0:F1}", num9));
                            dictionary5.Add("szts", string.Format("{0:F1}", num6));
                            dictionary5.Add("szjg", string.Format("{0:F1}", num9 + num6));
                            list2.Add(dictionary5);
                            num24++;
                            num11 += num2;
                            num12 += num3;
                            num13 += num4;
                            num14 += num5;
                            num15 += num6;
                            num19 += num10;
                            num17 += num8;
                            num18 += num9;
                        }
                        if (num11 > 0.0)
                        {
                            num16 = Math.Round((double) ((num17 / num11) * 100.0), 3);
                        }
                        else
                        {
                            num16 = 0.0;
                        }
                        Dictionary<string, string> dictionary6 = new Dictionary<string, string>();
                        dictionary6.Add("xzlx", "合計：");
                        dictionary6.Add("bs", num19.ToString());
                        dictionary6.Add("zxze", Math.Round(num11, 1).ToString());
                        dictionary6.Add("hysy", Math.Round(num12, 1).ToString());
                        dictionary6.Add("ysxx", Math.Round((double) (num13 + num14), 1).ToString());
                        dictionary6.Add("zc", num16.ToString());
                        dictionary6.Add("szze", Math.Round(num17, 1).ToString());
                        dictionary6.Add("zhb", "");
                        dictionary6.Add("szsy", Math.Round(num18, 1).ToString());
                        dictionary6.Add("szts", Math.Round(num15, 1).ToString());
                        dictionary6.Add("szjg", Math.Round((double) (num18 + num15), 1).ToString());
                        list3.Add(dictionary6);
                        dictionary2.Add("listreport", list2);
                        dictionary2.Add("listcount", list3);
                        data.Add("listdata", dictionary2);
                        double num25 = 0.0;
                        double num26 = 0.0;
                        double num27 = 0.0;
                        double num28 = 0.0;
                        table4 = DbHelperSQL.Query("select play_id,play_name, count(bet_id) as bs,sum((CASE WHEN (isTie=1) THEN 0 ELSE amount END)) zt, sum(profit) result ,ISNULL(abs(sum((CASE WHEN (isTie=1) THEN 0 ELSE amount END) * (CASE WHEN profit=0 THEN 0 ELSE " + str12 + "_drawback/100 END))) ,'0') as ysds from  " + str2 + "  where  " + str16 + "  and  fgs_name='" + str13 + "'" + str14 + " group by play_id,play_name  order by play_id asc", list.ToArray()).Tables[0];
                        if ((table4.Rows.Count > 0) && (int.Parse(table4.Rows[0]["bs"].ToString().Trim()) > 0))
                        {
                            if (_session.get_u_type().Equals("fgs"))
                            {
                                _session.get_allow_view_report().ToString().Equals("1");
                            }
                            foreach (DataRow row2 in table4.Rows)
                            {
                                Dictionary<string, string> dictionary7 = new Dictionary<string, string>();
                                num20 = Math.Round(double.Parse("-" + row2["zt"].ToString()), 1);
                                num21 = Math.Round(double.Parse(row2["result"].ToString()), 1);
                                num22 = Math.Round(double.Parse(row2["ysds"].ToString()), 1);
                                num25 += double.Parse(row2["bs"].ToString());
                                num26 += num20;
                                num27 += num21;
                                num28 += num22;
                                if (!_session.get_u_type().Equals("zj"))
                                {
                                    str13 = "";
                                }
                                dictionary7.Add("bhlb", row2["play_name"].ToString());
                                dictionary7.Add("bs", row2["bs"].ToString());
                                string str30 = string.Format("action=SearchDetailSix&uID={0}&gID={1}&t_LT=1&m_type=fgs&t_FT={2}&t_LID={3}&BeginDate={4}&EndDate={5}&FactSize=fill&zjbh=1&ReportType=T", new object[] { str13, row2["play_id"].ToString(), str5, str17, str7, str8 });
                                dictionary7.Add("bs_href", str30);
                                dictionary7.Add("bhje", num20.ToString());
                                string str31 = string.Format("action=SearchDetailSix&uID={0}&gID={1}&t_LT=1&m_type=fgs&t_FT={2}&t_LID={3}&BeginDate={4}&EndDate={5}&FactSize=my&zjbh=1&ReportType=T", new object[] { str13, row2["play_id"].ToString(), str5, str17, str7, str8 });
                                dictionary7.Add("bhje_href", str31);
                                dictionary7.Add("zbl", "100%");
                                dictionary7.Add("bhsy", Math.Round(num21, 1).ToString());
                                dictionary7.Add("ts", Math.Round(num22, 1).ToString());
                                dictionary7.Add("tshjg", Math.Round((double) (num21 + num22), 1).ToString());
                                list4.Add(dictionary7);
                            }
                            Dictionary<string, string> dictionary8 = new Dictionary<string, string>();
                            dictionary8.Add("bhlx", "合計：");
                            dictionary8.Add("bs", Math.Round(num25, 1).ToString());
                            dictionary8.Add("bhje", Math.Round(num26, 1).ToString());
                            dictionary8.Add("zbl", "100%");
                            dictionary8.Add("bhsy", Math.Round(num27, 1).ToString());
                            dictionary8.Add("ts", num28.ToString());
                            dictionary8.Add("tshjg", Math.Round((double) (num27 + num28), 2).ToString());
                            list5.Add(dictionary8);
                            dictionary3.Add("listreportbh", list4);
                            dictionary3.Add("listcountbh", list5);
                            data.Add("listdatabh", dictionary3);
                        }
                        Dictionary<string, string> dictionary9 = new Dictionary<string, string>();
                        dictionary9.Add("zcjg", string.Format("{0:F1}", num18 + num15));
                        dictionary9.Add("dkbhhjg", string.Format("{0:F1}", (num18 + -num27) + (num15 + -num28)));
                        list6.Add(dictionary9);
                        dictionary4.Add("listresult", list6);
                        data.Add("listdataresult", dictionary4);
                        data.Add("memberId", "fgs");
                        base.successOptMsg("分類報表查詢", data);
                    }
                    else
                    {
                        double num29 = 0.0;
                        double num30 = 0.0;
                        double num31 = 0.0;
                        double num32 = 0.0;
                        double num33 = 0.0;
                        double num34 = 0.0;
                        double num35 = 0.0;
                        double num36 = 0.0;
                        double num37 = 0.0;
                        double num38 = 0.0;
                        double num39 = 0.0;
                        int num40 = 0;
                        double num41 = 0.0;
                        double num42 = 0.0;
                        double num43 = 0.0;
                        double num44 = 0.0;
                        double num45 = 0.0;
                        double num46 = 0.0;
                        double num47 = 0.0;
                        double num48 = 0.0;
                        double num49 = 0.0;
                        double num50 = 0.0;
                        double num51 = 0.0;
                        double num52 = 0.0;
                        double num53 = 0.0;
                        double num54 = 0.0;
                        double num55 = 0.0;
                        double num56 = 0.0;
                        int num57 = 0;
                        Dictionary<string, object> dictionary10 = new Dictionary<string, object>();
                        Dictionary<string, List<Dictionary<string, string>>> dictionary11 = new Dictionary<string, List<Dictionary<string, string>>>();
                        List<Dictionary<string, string>> list7 = new List<Dictionary<string, string>>();
                        List<Dictionary<string, string>> list8 = new List<Dictionary<string, string>>();
                        Dictionary<string, List<Dictionary<string, string>>> dictionary12 = new Dictionary<string, List<Dictionary<string, string>>>();
                        List<Dictionary<string, string>> list9 = new List<Dictionary<string, string>>();
                        List<Dictionary<string, string>> list10 = new List<Dictionary<string, string>>();
                        Dictionary<string, List<Dictionary<string, string>>> dictionary13 = new Dictionary<string, List<Dictionary<string, string>>>();
                        List<Dictionary<string, string>> list11 = new List<Dictionary<string, string>>();
                        new Dictionary<string, List<Dictionary<string, string>>>();
                        new List<Dictionary<string, string>>();
                        foreach (DataRow row3 in table4.Rows)
                        {
                            num40 = int.Parse(row3["bs"].ToString());
                            num29 = Math.Round(double.Parse(row3["hyzt"].ToString()), 1);
                            num30 = Math.Round(double.Parse(row3["hyjg"].ToString()), 1);
                            num35 = Math.Round(double.Parse(row3["szzt"].ToString()), 1);
                            num36 = Math.Round(double.Parse(row3["szjg"].ToString()), 1);
                            if (num29 > 0.0)
                            {
                                num34 = Math.Round((double) ((num35 / num29) * 100.0), 3);
                            }
                            else
                            {
                                num34 = 0.0;
                            }
                            num31 = Math.Round(double.Parse(row3["ysze"].ToString()), 1);
                            num32 = Math.Round(double.Parse(row3["ysds"].ToString()), 1);
                            num33 = Math.Round(double.Parse(row3["szds"].ToString()), 1);
                            num37 = Math.Round(double.Parse(row3["mygetds"].ToString()), 1);
                            num38 = Math.Round(double.Parse(row3["mypayds"].ToString()), 1);
                            num39 = Math.Round(double.Parse(row3["upjg"].ToString()), 1);
                            if (num35 > 0.0)
                            {
                                num56 = Math.Round((double) ((num35 / num) * 100.0), 2);
                            }
                            else
                            {
                                num56 = 0.0;
                            }
                            Dictionary<string, string> dictionary14 = new Dictionary<string, string>();
                            dictionary14.Add("xzlx", row3["play_name"].ToString());
                            dictionary14.Add("bs", num40.ToString());
                            dictionary14.Add("xzje", num29.ToString());
                            string str32 = string.Format("action=SearchDetailSix&UP_1_ID={0}&gID={1}&m_type={2}&t_LT=1&t_FT={3}&t_LID={4}&BeginDate={5}&EndDate={6}&ReportType=T", new object[] { str13, row3["play_id"].ToString(), str12, str5, str17, str7, str8 });
                            dictionary14.Add("xzje_href", str32);
                            dictionary14.Add("hysy", string.Format("{0:F1}", num30));
                            dictionary14.Add("ysze", string.Format("{0:F1}", num31 + num32));
                            dictionary14.Add("zc", num34.ToString() + "%");
                            dictionary14.Add("szze", string.Format("{0:F1}", num35));
                            dictionary14.Add("hb", num56.ToString() + "%");
                            dictionary14.Add("szsy", Math.Round(num36, 1).ToString());
                            dictionary14.Add("szts", Math.Round(num33, 1).ToString());
                            dictionary14.Add("szjg", Math.Round((double) (num36 + num33), 1).ToString());
                            dictionary14.Add("zqts", Math.Round((double) (num38 - num37), 1).ToString());
                            dictionary14.Add("zshjg", Math.Round((double) ((num36 + num33) + (num37 - num38)), 1).ToString());
                            dictionary14.Add("yfsj", Math.Round((double) ((num31 + num32) - ((num36 + num33) + (num38 - num37))), 1).ToString());
                            list7.Add(dictionary14);
                            num57++;
                            num41 += num29;
                            num42 += num30;
                            num43 += num31;
                            num44 += num32;
                            num45 += num33;
                            num50 += num37;
                            num51 += num38;
                            num49 += num40;
                            num47 += num35;
                            num48 += num36;
                            num46 += num34;
                            num52 += num39;
                        }
                        if (num41 > 0.0)
                        {
                            num46 = Math.Round((double) ((num47 / num41) * 100.0), 3);
                        }
                        else
                        {
                            num46 = 0.0;
                        }
                        Dictionary<string, string> dictionary15 = new Dictionary<string, string>();
                        dictionary15.Add("xzlx", "合計：");
                        dictionary15.Add("bs", num49.ToString());
                        dictionary15.Add("zxze", Math.Round(num41, 1).ToString());
                        dictionary15.Add("hysy", Math.Round(num42, 1).ToString());
                        dictionary15.Add("ysze", Math.Round((double) (num43 + num44), 1).ToString());
                        dictionary15.Add("zc", num46.ToString());
                        dictionary15.Add("szze", Math.Round(num47, 1).ToString());
                        dictionary15.Add("hb", "");
                        dictionary15.Add("szsy", Math.Round(num48, 1).ToString());
                        dictionary15.Add("szts", Math.Round(num45, 1).ToString());
                        dictionary15.Add("szjg", Math.Round((double) (num48 + num45), 1).ToString());
                        dictionary15.Add("zqts", Math.Round((double) (num51 - num50), 1).ToString());
                        dictionary15.Add("zshjg", Math.Round((double) ((num48 + num45) + (num50 - num51)), 1).ToString());
                        dictionary15.Add("yfsj", Math.Round((double) ((num43 + num44) - ((num48 + num45) + (num51 - num50))), 1).ToString());
                        list8.Add(dictionary15);
                        dictionary11.Add("listreport", list7);
                        dictionary11.Add("listcount", list8);
                        dictionary10.Add("listdata", dictionary11);
                        double num58 = 0.0;
                        double num59 = 0.0;
                        double num60 = 0.0;
                        double num61 = 0.0;
                        table4 = DbHelperSQL.Query("select play_id,play_name, count(bet_id) as bs,sum((CASE WHEN (isTie=1) THEN 0 ELSE amount END)) zt, sum(" + str28 + ") result ,ISNULL(abs(sum((CASE WHEN (isTie=1) THEN 0 ELSE amount END) * (CASE WHEN profit=0 THEN 0 ELSE " + str12 + "_drawback/100 END))) ,'0') as ysds from  " + str2 + "  where  " + str16 + "  and  u_name='" + str13 + "'  group by  play_id,play_name  order by play_id,play_name asc ", list.ToArray()).Tables[0];
                        Dictionary<string, string> dictionary16 = new Dictionary<string, string>();
                        if ((table4.Rows.Count > 0) && (int.Parse(table4.Rows[0]["bs"].ToString().Trim()) > 0))
                        {
                            foreach (DataRow row4 in table4.Rows)
                            {
                                num53 = Math.Round(double.Parse("-" + row4["zt"].ToString()), 1);
                                num54 = Math.Round(double.Parse(row4["result"].ToString()), 1);
                                num55 = Math.Round(double.Parse(row4["ysds"].ToString()), 1);
                                num58 += double.Parse(row4["bs"].ToString());
                                num59 += num53;
                                num60 += num54;
                                num61 += num55;
                                Dictionary<string, string> dictionary17 = new Dictionary<string, string>();
                                dictionary17.Add("bhlb", row4["play_name"].ToString());
                                dictionary17.Add("bs", row4["bs"].ToString());
                                string str33 = string.Format("action=SearchDetailSix&uID={0}&gID={1}&t_LT=1&m_type={2}&t_FT={3}&t_LID={4}&BeginDate={5}&EndDate={6}&FactSize=fill&ReportType=T", new object[] { str13, row4["play_id"].ToString(), str21, str5, str17, str7, str8 });
                                dictionary17.Add("bs_href", str33);
                                dictionary17.Add("bhje", num53.ToString());
                                string str34 = string.Format("action=SearchDetailSix&uID={0}&gID={1}&t_LT=1&m_type={2}&t_FT={3}&t_LID={4}&BeginDate={5}&EndDate={6}&FactSize=my&ReportType=T", new object[] { str13, row4["play_id"].ToString(), str21, str5, str17, str7, str8 });
                                dictionary17.Add("bhje_href", str34);
                                dictionary17.Add("zbl", "100%");
                                dictionary17.Add("bhsy", Math.Round(num54, 1).ToString());
                                dictionary17.Add("ts", Math.Round(num55, 1).ToString());
                                dictionary17.Add("tshjg", Math.Round((double) (num54 + num55), 1).ToString());
                                list9.Add(dictionary17);
                            }
                            Dictionary<string, string> dictionary18 = new Dictionary<string, string>();
                            dictionary18.Add("bhlx", "合計：");
                            dictionary18.Add("bs", Math.Round(num58, 1).ToString());
                            dictionary18.Add("bhje", Math.Round(num59, 1).ToString());
                            dictionary18.Add("zbl", "100%");
                            dictionary18.Add("bhsy", Math.Round(num60, 1).ToString());
                            dictionary18.Add("ts", num61.ToString());
                            dictionary18.Add("tshjg", Math.Round((double) (num60 + num61), 2).ToString());
                            list10.Add(dictionary18);
                            dictionary12.Add("listreportbh", list9);
                            dictionary12.Add("listcountbh", list10);
                            dictionary10.Add("listdatabh", dictionary12);
                            dictionary16.Add("szze", Math.Round((double) (num47 + num59), 1).ToString());
                            dictionary16.Add("syjg", Math.Round((double) (num48 + -num60), 1).ToString());
                            dictionary16.Add("ts", Math.Round((double) (num45 - num61), 1).ToString());
                            dictionary16.Add("tshjg", Math.Round((double) ((num48 + -num60) + (num45 - num61)), 1).ToString());
                            dictionary16.Add("zqts", Math.Round((double) (num51 - num50), 1).ToString());
                            dictionary16.Add("zshjg", Math.Round((double) (((num48 + -num60) + (num45 - num61)) + (num51 - num50)), 1).ToString());
                        }
                        dictionary16.Add("zcjg", string.Format("{0:F1}", num48 + num45).ToString());
                        dictionary16.Add("yfsj", string.Format("{0:F1}", (num52 + num60) + (num50 + num61)).ToString());
                        list11.Add(dictionary16);
                        dictionary13.Add("listresult", list11);
                        dictionary10.Add("listdataresult", dictionary13);
                        dictionary10.Add("memberId", str21);
                        base.successOptMsg("分類報表查詢", dictionary10);
                    }
                }
            }
        }

        public void ReportDetailKc_B()
        {
            agent_userinfo_session _session = this.Session[this.Session["user_name"] + "lottery_session_user_info"] as agent_userinfo_session;
            if ((_session.get_u_type().ToLower().Equals("zj") && (_session.get_users_child_session() != null)) && (_session.get_users_child_session().get_permissions_name().IndexOf("po_4_1") < 0))
            {
                base.noRightOptMsg(PageBase.GetMessageByCache("u100057", "MessageHint"));
            }
            else if ((!_session.get_u_type().ToLower().Equals("zj") && (_session.get_users_child_session() != null)) && (_session.get_users_child_session().get_permissions_name().IndexOf("po_7_1") < 0))
            {
                base.noRightOptMsg(PageBase.GetMessageByCache("u100057", "MessageHint"));
            }
            else
            {
                string fTable = "cz_bet_kc  with(NOLOCK) ";
                string str2 = LSRequest.qq("t_LT");
                if (str2.Equals("-1"))
                {
                    str2 = "";
                }
                string str3 = "";
                string str4 = "";
                string str5 = "";
                string str6 = "";
                string str7 = "";
                string str8 = LSRequest.qq("t_FT");
                LSRequest.qq("q_type");
                string str9 = LSRequest.qq("t_Balance");
                string str10 = LSRequest.qq("BeginDate");
                string str11 = LSRequest.qq("EndDate");
                string str12 = LSRequest.qq("t_LID");
                string str13 = LSRequest.qq("userid");
                string str14 = " 1=1 ";
                List<SqlParameter> list = new List<SqlParameter>();
                SqlParameter item = new SqlParameter();
                if (str9 == "0")
                {
                    str14 = str14 + " and is_payment=0";
                }
                else
                {
                    str14 = str14 + " and is_payment=1";
                }
                if (str8 == "0")
                {
                    if (str12 != "")
                    {
                        str14 = str14 + " and phase_id = @phase_id ";
                        SqlParameter parameter2 = new SqlParameter("@phase_id", SqlDbType.NVarChar) {
                            Value = str12
                        };
                        list.Add(parameter2);
                    }
                }
                else
                {
                    DateTime time = Convert.ToDateTime(str11);
                    DateTime now = DateTime.Now;
                    now = time.AddDays(1.0);
                    str14 = str14 + " and bet_time between  @BeginDate and  @jtdate ";
                    item = new SqlParameter("@BeginDate", SqlDbType.NVarChar) {
                        Value = str10 + " 06:30:00"
                    };
                    list.Add(item);
                    item = new SqlParameter("@jtdate", SqlDbType.NVarChar) {
                        Value = now.ToString("yyyy-MM-dd") + " 06:30:00"
                    };
                    list.Add(item);
                    string str15 = DateTime.Now.AddHours(-7.0).ToString("yyyy-MM-dd");
                    if ((str15 == LSRequest.qq("BeginDate")) && (str15 == LSRequest.qq("EndDate")))
                    {
                        fTable = "cz_bet_kc  with(NOLOCK) ";
                    }
                    else
                    {
                        fTable = "cz_betold_kc  with(NOLOCK) ";
                    }
                }
                LSRequest.qq("mID");
                str3 = LSRequest.qq("UP_ID");
                str4 = LSRequest.qq("FactSize");
                if (str3 == "my")
                {
                    str3 = _session.get_u_name();
                }
                else
                {
                    str3 = LSRequest.qq("UP_ID");
                }
                str6 = LSRequest.qq("uID");
                cz_users userInfoByUID = null;
                if (string.IsNullOrEmpty(str13))
                {
                    str5 = _session.get_u_name();
                }
                else
                {
                    if (userInfoByUID == null)
                    {
                        userInfoByUID = CallBLL.cz_users_bll.GetUserInfoByUID(str13);
                    }
                    str5 = userInfoByUID.get_u_name();
                }
                string str16 = "0";
                if (_session.get_u_type() == "fgs")
                {
                    str16 = _session.get_allow_view_report().ToString();
                }
                if ((str16 != "1") && !base.IsUpperLowerLevels(userInfoByUID.get_u_name(), _session.get_u_type(), _session.get_u_name()))
                {
                    base.noRightOptMsg(PageBase.GetMessageByCache("u100057", "MessageHint"));
                }
                else
                {
                    if (str4 == "my")
                    {
                        str4 = "zj_rate";
                    }
                    string str17 = "";
                    string str18 = "";
                    string str19 = "";
                    string str20 = "";
                    int num = 0;
                    string str32 = _session.get_u_type();
                    if (str32 != null)
                    {
                        if (!(str32 == "zj"))
                        {
                            if (str32 == "fgs")
                            {
                                str19 = "fgs";
                                str20 = "gd";
                                num = 4;
                            }
                            else if (str32 == "gd")
                            {
                                str19 = "gd";
                                str20 = "zd";
                                num = 3;
                            }
                            else if (str32 == "zd")
                            {
                                str19 = "zd";
                                str20 = "dl";
                                num = 2;
                            }
                            else if (str32 == "dl")
                            {
                                str19 = "dl";
                                str20 = "hy";
                                num = 1;
                            }
                        }
                        else
                        {
                            str19 = "zj";
                            str20 = "fgs";
                            num = 5;
                        }
                    }
                    str17 = "hy";
                    if (str6.Trim() != "")
                    {
                        if (str17 == "fgs")
                        {
                            str18 = " and fgs_name= @fgs_name ";
                            SqlParameter parameter3 = new SqlParameter("@fgs_name", SqlDbType.NVarChar) {
                                Value = str6
                            };
                            list.Add(parameter3);
                        }
                        else
                        {
                            str18 = " and u_name= @u_name  ";
                            SqlParameter parameter4 = new SqlParameter("@u_name", SqlDbType.NVarChar) {
                                Value = str6
                            };
                            list.Add(parameter4);
                        }
                    }
                    else if (str17 == "hy")
                    {
                        str18 = " and  u_name= @u_name  ";
                        SqlParameter parameter5 = new SqlParameter("@u_name", SqlDbType.NVarChar) {
                            Value = str5
                        };
                        list.Add(parameter5);
                    }
                    else if (str17 == "zj")
                    {
                        str18 = "";
                    }
                    else
                    {
                        str18 = " and  " + str17 + "_name = @m_type_name ";
                        SqlParameter parameter6 = new SqlParameter("@m_type_name", SqlDbType.NVarChar) {
                            Value = str5
                        };
                        list.Add(parameter6);
                    }
                    base.q("gID");
                    if (base.q("gID") != "0")
                    {
                        str18 = str18 + " and play_id=@play_id  ";
                        item = new SqlParameter("@play_id", SqlDbType.NVarChar) {
                            Value = LSRequest.qq("gID")
                        };
                        list.Add(item);
                    }
                    if (str9 != "")
                    {
                        str18 = str18 + " and is_payment=@is_payment ";
                        item = new SqlParameter("@is_payment", SqlDbType.NVarChar) {
                            Value = str9
                        };
                        list.Add(item);
                    }
                    if (str2 != "")
                    {
                        str18 = str18 + " and lottery_type=@lottery_type  ";
                        item = new SqlParameter("@lottery_type", SqlDbType.NVarChar) {
                            Value = str2
                        };
                        list.Add(item);
                    }
                    int num2 = Convert.ToInt32("0" + base.q("page"));
                    int num3 = 0;
                    int num4 = 1;
                    num2 = (num2 == 0) ? 1 : num2;
                    string str21 = "";
                    str21 = " and  " + str14 + " " + str18 + str7;
                    num3 = Convert.ToInt32(base.GetValueByKey("count(bet_id)", fTable, "bet_id>0" + str21, list.ToArray()));
                    num4 = ((num3 % this.pageSize) == 0) ? (num3 / this.pageSize) : ((num3 / this.pageSize) + 1);
                    num2 = (num2 > num4) ? num4 : num2;
                    if (num2 <= 0)
                    {
                        num2 = 1;
                    }
                    str21 = " " + str14 + " " + str18 + str7;
                    StringBuilder builder = new StringBuilder();
                    builder.AppendFormat(" select {0} from {1} where {2} order by bet_id desc offset {3} rows fetch next {4} rows only  ", new object[] { " * ", fTable, str21, (((num2 - 1) * this.pageSize) < 0) ? 0 : ((num2 - 1) * this.pageSize), this.pageSize });
                    DataTable table = DbHelperSQL.Query(builder.ToString(), list.ToArray()).Tables[0];
                    if (table.Rows.Count == 0)
                    {
                        if (num2 <= 1)
                        {
                            base.noRightOptMsg("無符合條件的數據！");
                        }
                        else
                        {
                            base.noRightOptMsg("無數據加載！");
                        }
                    }
                    else
                    {
                        double num5 = 0.0;
                        int num6 = 0;
                        string str22 = "";
                        string str23 = "";
                        double num7 = 0.0;
                        Dictionary<string, object> data = new Dictionary<string, object>();
                        Dictionary<string, List<Dictionary<string, string>>> dictionary2 = new Dictionary<string, List<Dictionary<string, string>>>();
                        List<Dictionary<string, string>> list2 = new List<Dictionary<string, string>>();
                        foreach (DataRow row in table.Rows)
                        {
                            Dictionary<string, string> dictionary3 = new Dictionary<string, string>();
                            int num1 = num6 % 2;
                            DateTime time4 = Convert.ToDateTime(row["bet_time"].ToString());
                            if (row["u_name"].ToString().Trim() == _session.get_u_name())
                            {
                                num7 = -double.Parse(row["amount"].ToString());
                            }
                            else
                            {
                                num7 = double.Parse(row["amount"].ToString());
                            }
                            if (_session.get_u_type() == "zj")
                            {
                                if (row["fgs_name"].ToString().Trim() == _session.get_u_name())
                                {
                                    num7 = -double.Parse(row["amount"].ToString());
                                }
                                else
                                {
                                    num7 = double.Parse(row["amount"].ToString());
                                }
                            }
                            (row["isDelete"]).Equals("1");
                            if (!string.IsNullOrEmpty(row["unit_cnt"].ToString()) && (int.Parse(row["unit_cnt"].ToString()) > 1))
                            {
                                base.GroupShowHrefString(2, row["order_num"].ToString(), row["is_payment"].ToString(), "1", "1");
                            }
                            row["is_payment"].ToString().Equals("1");
                            string str25 = "";
                            int num8 = 8;
                            if (row["lottery_type"].ToString().Equals(num8.ToString()))
                            {
                                str25 = string.Format("  [第{0}桌 {1}]", row["table_type"].ToString(), Utils.GetPKBJLPlaytypeColorTxt(row["play_type"].ToString()));
                            }
                            dictionary3.Add("zdhmsj", row["order_num"].ToString().Trim() + "#<br>" + time4.ToString("yy-MM-dd HH:mm:ss"));
                            dictionary3.Add("xzlx", base.GetGameNameByID(row["lottery_type"].ToString().Trim()) + str25 + "<br>" + row["phase"].ToString() + "期");
                            dictionary3.Add("hy", row["u_name"].ToString().Trim() + "<br>" + row["kind"].ToString() + "盤");
                            str22 = "";
                            if ((((row["odds_id"].ToString().Trim() == "329") || (row["odds_id"].ToString().Trim() == "330")) || ((row["odds_id"].ToString().Trim() == "331") || (row["odds_id"].ToString().Trim() == "1181"))) || (((row["odds_id"].ToString().Trim() == "1200") || (row["odds_id"].ToString().Trim() == "1201")) || ((row["odds_id"].ToString().Trim() == "1202") || (row["odds_id"].ToString().Trim() == "1203"))))
                            {
                                switch (row["odds_id"].ToString().Trim())
                                {
                                    case "327":
                                    case "328":
                                    case "329":
                                    case "330":
                                    case "331":
                                    case "1181":
                                    case "1200":
                                    case "1201":
                                    case "1202":
                                    case "1203":
                                        str22 = row["play_name"].ToString() + " @ " + row["odds"].ToString();
                                        break;
                                }
                                if (int.Parse(row["unit_cnt"].ToString()) > 0)
                                {
                                    num5 = double.Parse(row["amount"].ToString()) / double.Parse(row["unit_cnt"].ToString());
                                }
                                if (((int.Parse(row["unit_cnt"].ToString()) > 0) && ((str32 = row["lm_type"].ToString().Trim()) != null)) && (str32 == "0"))
                                {
                                    if (double.Parse(row["unit_cnt"].ToString()) > 1.0)
                                    {
                                        str22 = str22 + "<br>復式【" + row["unit_cnt"].ToString() + " 組】 ";
                                    }
                                    str22 = str22 + "<br>" + row["bet_val"].ToString().Replace(',', '、');
                                }
                                str23 = " ￥" + num5.ToString() + "\x00d7" + row["unit_cnt"].ToString() + "<br>" + num7.ToString();
                            }
                            else
                            {
                                str32 = str22;
                                str22 = str32 + row["play_name"].ToString() + "『" + row["bet_val"].ToString() + "』@" + row["odds"].ToString();
                                str23 = num7.ToString();
                            }
                            if ((row["sale_type"]).Equals("1"))
                            {
                                str22 = str22 + "<br><補>";
                            }
                            dictionary3.Add("xzmx", str22);
                            dictionary3.Add("hyxz", str23);
                            if (row["is_payment"].ToString().Trim() != "1")
                            {
                                dictionary3.Add("hysy", "『未派彩』");
                            }
                            else
                            {
                                dictionary3.Add("hysy", Math.Round(double.Parse(row["profit"].ToString()), 2).ToString());
                            }
                            string str26 = "";
                            string str27 = "";
                            string str28 = "";
                            string str29 = "";
                            string str30 = "";
                            if (num >= 1)
                            {
                                str26 = row["dl_name"].ToString() + "<br>" + row["dl_rate"].ToString() + "<br>" + row["hy_drawback"].ToString();
                                dictionary3.Add("dl_name", str26);
                            }
                            if (num >= 2)
                            {
                                str27 = row["zd_name"].ToString() + "<br>" + row["zd_rate"].ToString() + "<br>" + row["dl_drawback"].ToString();
                                dictionary3.Add("zd_name", str27);
                            }
                            if (num >= 3)
                            {
                                str28 = row["gd_name"].ToString() + "<br>" + row["gd_rate"].ToString() + "<br>" + row["zd_drawback"].ToString();
                                dictionary3.Add("gd_name", str28);
                            }
                            if (num >= 4)
                            {
                                str29 = row["fgs_name"].ToString() + "<br>" + row["fgs_rate"].ToString() + "<br>" + row["gd_drawback"].ToString();
                                dictionary3.Add("fgs_name", str29);
                            }
                            if (num >= 5)
                            {
                                str30 = _session.get_u_name() + "<br>" + row["zj_rate"].ToString() + "<br>" + row["fgs_drawback"].ToString();
                                dictionary3.Add("zj_name", str30);
                            }
                            if (row["is_payment"].ToString().Trim() != "1")
                            {
                                dictionary3.Add("ndjg", "『未派彩』");
                            }
                            else
                            {
                                string str31 = Math.Round((double) ((double.Parse(row["profit"].ToString()) * double.Parse(row[str19 + "_rate"].ToString())) / 100.0), 2).ToString() + "<br>";
                                if (double.Parse(row["profit"].ToString()) == 0.0)
                                {
                                    str31 = str31 + "0";
                                }
                                else
                                {
                                    str31 = str31 + Math.Round((double) ((((num7 * double.Parse(row[str19 + "_rate"].ToString())) / 100.0) * double.Parse(row[str20 + "_drawback"].ToString())) / 100.0), 2);
                                }
                                dictionary3.Add("ndjg", str31);
                            }
                            list2.Add(dictionary3);
                        }
                        dictionary2.Add("listdetail", list2);
                        data.Add("listdata", dictionary2);
                        base.successOptMsg("交收報表明細", data);
                    }
                }
            }
        }

        public void ReportDetailKc_T()
        {
            agent_userinfo_session _session = this.Session[this.Session["user_name"] + "lottery_session_user_info"] as agent_userinfo_session;
            if ((_session.get_u_type().ToLower().Equals("zj") && (_session.get_users_child_session() != null)) && (_session.get_users_child_session().get_permissions_name().IndexOf("po_4_2") < 0))
            {
                base.noRightOptMsg(PageBase.GetMessageByCache("u100057", "MessageHint"));
            }
            else if ((!_session.get_u_type().ToLower().Equals("zj") && (_session.get_users_child_session() != null)) && (_session.get_users_child_session().get_permissions_name().IndexOf("po_7_2") < 0))
            {
                base.noRightOptMsg(PageBase.GetMessageByCache("u100057", "MessageHint"));
            }
            else
            {
                List<SqlParameter> list = new List<SqlParameter>();
                SqlParameter item = new SqlParameter();
                string fTable = "cz_bet_kc with(NOLOCK) ";
                string str2 = "";
                string str3 = "";
                string str4 = "";
                string str5 = "";
                string str6 = "";
                string str7 = "";
                LSRequest.qq("t_FT");
                string str8 = LSRequest.qq("q_type");
                string str9 = LSRequest.qq("t_Balance");
                string str10 = "0";
                if (LSRequest.qq("isbh") == "1")
                {
                    str10 = "1";
                }
                if (LSRequest.qq("t_FT") == "LID")
                {
                    string str11 = "";
                    SqlParameter[] collection = Utils.GetParams(LSRequest.qq("t_LID"), ref str11, SqlDbType.Int);
                    str2 = string.Format(" phase_id in({0}) and  lottery_type=@lottery_type ", str11);
                    list.AddRange(collection);
                    item = new SqlParameter("@lottery_type", SqlDbType.NVarChar) {
                        Value = LSRequest.qq("q_type")
                    };
                    list.Add(item);
                }
                else
                {
                    DateTime time = Convert.ToDateTime(LSRequest.qq("EndDate"));
                    DateTime now = DateTime.Now;
                    string str12 = time.AddDays(1.0).ToString("yyyy-MM-dd");
                    str2 = " bet_time between  @BeginDate and @CqDate  ";
                    item = new SqlParameter("@BeginDate", SqlDbType.NVarChar) {
                        Value = LSRequest.qq("BeginDate") + " 06:30:00"
                    };
                    list.Add(item);
                    item = new SqlParameter("@CqDate", SqlDbType.NVarChar) {
                        Value = str12 + " 06:30:00"
                    };
                    list.Add(item);
                    string str13 = DateTime.Now.AddHours((double) -int.Parse(base.get_BetTime_KC())).ToString("yyyy-MM-dd");
                    if ((str13 == LSRequest.qq("BeginDate")) && (str13 == LSRequest.qq("EndDate")))
                    {
                        fTable = "cz_bet_kc  with(NOLOCK) ";
                    }
                    else
                    {
                        fTable = "cz_betold_kc  with(NOLOCK) ";
                    }
                }
                LSRequest.qq("mID");
                str3 = LSRequest.qq("UP_ID");
                str4 = LSRequest.qq("FactSize");
                if (str3 == "my")
                {
                    str3 = _session.get_u_name();
                }
                else
                {
                    str3 = LSRequest.qq("UP_ID");
                }
                str6 = LSRequest.qq("uID");
                if (base.Request["UP_1_ID"] == null)
                {
                    str5 = _session.get_u_name();
                }
                else
                {
                    str5 = LSRequest.qq("UP_1_ID");
                }
                if (!base.IsUpperLowerLevels(str5, _session.get_u_type(), _session.get_u_name()))
                {
                    base.Response.Redirect("../MessagePage.aspx?code=u100057&url=&issuccess=1&isback=0&isopen=0");
                    base.Response.End();
                }
                if (str4 == "my")
                {
                    str4 = "zj_zc";
                }
                string str14 = "";
                string str15 = "";
                string str16 = "";
                string str17 = "";
                int num = 0;
                bool flag = false;
                if (((base.Request["a_name"] == null) && (_session.get_u_type() == "fgs")) && (_session.get_allow_view_report().ToString() == "1"))
                {
                    flag = true;
                }
                string str28 = _session.get_u_type();
                if (str28 != null)
                {
                    if (!(str28 == "zj"))
                    {
                        if (str28 == "fgs")
                        {
                            str16 = "fgs";
                            str17 = "gd";
                            num = 4;
                            string text1 = " and fgs_name='" + _session.get_u_name() + "'";
                        }
                        else if (str28 == "gd")
                        {
                            str16 = "gd";
                            str17 = "zd";
                            num = 3;
                            string text2 = " and gd_name='" + _session.get_u_name() + "'";
                        }
                        else if (str28 == "zd")
                        {
                            str16 = "zd";
                            str17 = "dl";
                            num = 2;
                            string text3 = " and zd_name='" + _session.get_u_name() + "'";
                        }
                        else if (str28 == "dl")
                        {
                            str16 = "dl";
                            str17 = "hy";
                            num = 1;
                            string text4 = " and dl_name='" + _session.get_u_name() + "'";
                        }
                    }
                    else
                    {
                        str16 = "zj";
                        str17 = "fgs";
                        num = 5;
                    }
                }
                if (LSRequest.qq("m_type") != "")
                {
                    str14 = LSRequest.qq("m_type");
                }
                else
                {
                    str14 = _session.get_u_type();
                }
                if (((str14 == "fgs") && (str4 != "")) && (LSRequest.qq("gID") == ""))
                {
                    str7 = " and u_name=@u_name  ";
                    item = new SqlParameter("@u_name", SqlDbType.NVarChar) {
                        Value = LSRequest.qq("UP_1_ID")
                    };
                    list.Add(item);
                }
                if (str6.Trim() != "")
                {
                    if (str14 == "fgs")
                    {
                        if (!flag)
                        {
                            str15 = " and fgs_name= @fgs_name_uid ";
                            item = new SqlParameter("@fgs_name_uid", SqlDbType.NVarChar) {
                                Value = str6
                            };
                            list.Add(item);
                        }
                    }
                    else
                    {
                        str15 = " and u_name=@u_name_uid  ";
                        item = new SqlParameter("@u_name_uid", SqlDbType.NVarChar) {
                            Value = str6
                        };
                        list.Add(item);
                    }
                }
                else if (str14 == "hy")
                {
                    str15 = " and  u_name=@u_name_UP_1_ID  ";
                    item = new SqlParameter("@u_name_UP_1_ID", SqlDbType.NVarChar) {
                        Value = str5
                    };
                    list.Add(item);
                }
                else if (str14 == "zj")
                {
                    str15 = "";
                }
                else if (!flag)
                {
                    str15 = " and " + str14 + "_name= @m_type_name ";
                    item = new SqlParameter("@m_type_name", SqlDbType.NVarChar) {
                        Value = str5
                    };
                    list.Add(item);
                }
                if (str10 != "1")
                {
                    str15 = str15 + " and u_name!=@u_name_xx  ";
                    item = new SqlParameter("@u_name_xx", SqlDbType.NVarChar) {
                        Value = LSRequest.qq("UP_1_ID")
                    };
                    list.Add(item);
                }
                LSRequest.qq("gID");
                if (LSRequest.qq("gID") != "")
                {
                    str15 = str15 + " and play_id=@play_id_xx  ";
                    item = new SqlParameter("@play_id_xx", SqlDbType.NVarChar) {
                        Value = LSRequest.qq("gID")
                    };
                    list.Add(item);
                }
                if (str9 != "")
                {
                    str15 = str15 + " and is_payment=@is_payment  ";
                    item = new SqlParameter("@is_payment", SqlDbType.NVarChar) {
                        Value = str9
                    };
                    list.Add(item);
                }
                int num2 = Convert.ToInt32("0" + LSRequest.qq("page"));
                int num3 = 0;
                int num4 = 1;
                num2 = (num2 == 0) ? 1 : num2;
                string str18 = "";
                str18 = " " + str2 + " and lottery_type=@lottery_type_xx  " + str15 + str7;
                item = new SqlParameter("@lottery_type_xx", SqlDbType.NVarChar) {
                    Value = str8
                };
                list.Add(item);
                num3 = Convert.ToInt32(base.GetValueByKey("count(bet_id)", fTable, "bet_id>0 and " + str18, list.ToArray()));
                num4 = ((num3 % this.pageSize) == 0) ? (num3 / this.pageSize) : ((num3 / this.pageSize) + 1);
                num2 = (num2 > num4) ? num4 : num2;
                if (num2 <= 0)
                {
                    num2 = 1;
                }
                StringBuilder builder = new StringBuilder();
                builder.AppendFormat(" select {0} from {1} where {2} order by bet_id desc offset {3} rows fetch next {4} rows only  ", new object[] { " * ", fTable, str18, (((num2 - 1) * this.pageSize) < 0) ? 0 : ((num2 - 1) * this.pageSize), this.pageSize });
                DataTable table = DbHelperSQL.Query(builder.ToString(), list.ToArray()).Tables[0];
                if (table.Rows.Count == 0)
                {
                    if (num2 <= 1)
                    {
                        base.noRightOptMsg("無符合條件的數據！");
                    }
                    else
                    {
                        base.noRightOptMsg("無數據加載！");
                    }
                }
                else
                {
                    double num5 = 0.0;
                    int num6 = 0;
                    string str19 = "";
                    double num7 = 0.0;
                    Dictionary<string, object> data = new Dictionary<string, object>();
                    Dictionary<string, List<Dictionary<string, string>>> dictionary2 = new Dictionary<string, List<Dictionary<string, string>>>();
                    List<Dictionary<string, string>> list2 = new List<Dictionary<string, string>>();
                    foreach (DataRow row in table.Rows)
                    {
                        Dictionary<string, string> dictionary3 = new Dictionary<string, string>();
                        int num1 = num6 % 2;
                        DateTime time4 = Convert.ToDateTime(row["bet_time"].ToString());
                        if (row["u_name"].ToString().Trim() == _session.get_u_name())
                        {
                            num7 = -double.Parse(row["amount"].ToString());
                        }
                        else
                        {
                            num7 = double.Parse(row["amount"].ToString());
                        }
                        if (_session.get_u_type() == "zj")
                        {
                            if (row["fgs_name"].ToString().Trim() == _session.get_u_name())
                            {
                                num7 = -double.Parse(row["amount"].ToString());
                            }
                            else
                            {
                                num7 = double.Parse(row["amount"].ToString());
                            }
                        }
                        (row["isDelete"]).Equals("1");
                        if (!string.IsNullOrEmpty(row["unit_cnt"].ToString()) && (int.Parse(row["unit_cnt"].ToString()) > 1))
                        {
                            base.GroupShowHrefString(2, row["order_num"].ToString(), row["is_payment"].ToString(), "1", "1");
                        }
                        string str21 = "";
                        int num8 = 8;
                        if (row["lottery_type"].ToString().Equals(num8.ToString()))
                        {
                            str21 = string.Format("  [第{0}桌 {1}]", row["table_type"].ToString(), Utils.GetPKBJLPlaytypeColorTxt(row["play_type"].ToString()));
                        }
                        dictionary3.Add("zdhmsj", row["order_num"].ToString().Trim() + "#<br>" + time4.ToString("yy-MM-dd HH:mm:ss"));
                        dictionary3.Add("xzlx", base.GetGameNameByID(row["lottery_type"].ToString()) + str21 + "<br>" + row["phase"].ToString() + "期");
                        dictionary3.Add("hy", row["u_name"].ToString().Trim() + "<br>" + row["kind"].ToString() + "盤");
                        str19 = "";
                        if ((((row["odds_id"].ToString().Trim() == "329") || (row["odds_id"].ToString().Trim() == "330")) || ((row["odds_id"].ToString().Trim() == "331") || (row["odds_id"].ToString().Trim() == "1181"))) || (((row["odds_id"].ToString().Trim() == "1200") || (row["odds_id"].ToString().Trim() == "1201")) || ((row["odds_id"].ToString().Trim() == "1202") || (row["odds_id"].ToString().Trim() == "1203"))))
                        {
                            string str30;
                            switch (row["odds_id"].ToString().Trim())
                            {
                                case "327":
                                case "328":
                                case "329":
                                case "330":
                                case "331":
                                case "1181":
                                case "1200":
                                case "1201":
                                case "1202":
                                case "1203":
                                    str19 = row["play_name"].ToString() + " @ " + row["odds"].ToString();
                                    break;
                            }
                            if (int.Parse(row["unit_cnt"].ToString()) > 0)
                            {
                                num5 = double.Parse(row["amount"].ToString()) / double.Parse(row["unit_cnt"].ToString());
                            }
                            if (((int.Parse(row["unit_cnt"].ToString()) > 0) && ((str30 = row["lm_type"].ToString().Trim()) != null)) && (str30 == "0"))
                            {
                                if (double.Parse(row["unit_cnt"].ToString()) > 1.0)
                                {
                                    str19 = str19 + "<br>復式【" + row["unit_cnt"].ToString() + " 組】 ";
                                }
                                str19 = str19 + "<br>" + row["bet_val"].ToString().Replace(',', '、');
                            }
                            string text5 = " ￥" + num5.ToString() + "\x00d7" + row["unit_cnt"].ToString() + "<br>" + num7.ToString();
                        }
                        else
                        {
                            str28 = str19;
                            str19 = str28 + row["play_name"].ToString() + "『" + row["bet_val"].ToString() + "』@" + row["odds"].ToString();
                            num7.ToString();
                        }
                        if ((row["sale_type"]).Equals("1"))
                        {
                            str19 = str19 + "<br><補>";
                        }
                        dictionary3.Add("xzmx", str19);
                        if (row["is_payment"].ToString().Trim() != "1")
                        {
                            dictionary3.Add("hysy", "『未派彩』");
                        }
                        else
                        {
                            dictionary3.Add("hysy", Math.Round(double.Parse(row["profit"].ToString()), 2).ToString());
                        }
                        string str22 = "";
                        string str23 = "";
                        string str24 = "";
                        string str25 = "";
                        string str26 = "";
                        if (num >= 1)
                        {
                            str22 = row["dl_name"].ToString() + "<br>" + row["dl_rate"].ToString() + "<br>" + row["hy_drawback"].ToString();
                            dictionary3.Add("dl_name", str22);
                        }
                        if (num >= 2)
                        {
                            str23 = row["zd_name"].ToString() + "<br>" + row["zd_rate"].ToString() + "<br>" + row["dl_drawback"].ToString();
                            dictionary3.Add("zd_name", str23);
                        }
                        if (num >= 3)
                        {
                            str24 = row["gd_name"].ToString() + "<br>" + row["gd_rate"].ToString() + "<br>" + row["zd_drawback"].ToString();
                            dictionary3.Add("gd_name", str24);
                        }
                        if (num >= 4)
                        {
                            str25 = row["fgs_name"].ToString() + "<br>" + row["fgs_rate"].ToString() + "<br>" + row["gd_drawback"].ToString();
                            dictionary3.Add("fgs_name", str25);
                        }
                        if (num >= 5)
                        {
                            str26 = _session.get_u_name() + "<br>" + row["zj_rate"].ToString() + "<br>" + row["fgs_drawback"].ToString();
                            dictionary3.Add("zj_name", str26);
                        }
                        if (row["is_payment"].ToString().Trim() != "1")
                        {
                            dictionary3.Add("ndjg", "『未派彩』");
                        }
                        else
                        {
                            string str27 = Math.Round((double) ((double.Parse(row["profit"].ToString()) * double.Parse(row[str16 + "_rate"].ToString())) / 100.0), 2).ToString() + "<br>";
                            if (double.Parse(row["profit"].ToString()) == 0.0)
                            {
                                str27 = str27 + "0";
                            }
                            else
                            {
                                str27 = str27 + Math.Round((double) ((((num7 * double.Parse(row[str16 + "_rate"].ToString())) / 100.0) * double.Parse(row[str17 + "_drawback"].ToString())) / 100.0), 2);
                            }
                            dictionary3.Add("ndjg", str27);
                        }
                        list2.Add(dictionary3);
                    }
                    dictionary2.Add("listdetail", list2);
                    data.Add("listdata", dictionary2);
                    base.successOptMsg("分類報表明細", data);
                }
            }
        }

        public void ReportDetailSix_B()
        {
            agent_userinfo_session _session = this.Session[this.Session["user_name"] + "lottery_session_user_info"] as agent_userinfo_session;
            if ((_session.get_u_type().ToLower().Equals("zj") && (_session.get_users_child_session() != null)) && (_session.get_users_child_session().get_permissions_name().IndexOf("po_4_1") < 0))
            {
                base.noRightOptMsg(PageBase.GetMessageByCache("u100057", "MessageHint"));
            }
            else if ((!_session.get_u_type().ToLower().Equals("zj") && (_session.get_users_child_session() != null)) && (_session.get_users_child_session().get_permissions_name().IndexOf("po_7_1") < 0))
            {
                base.noRightOptMsg(PageBase.GetMessageByCache("u100057", "MessageHint"));
            }
            else
            {
                List<SqlParameter> list = new List<SqlParameter>();
                SqlParameter item = new SqlParameter();
                string fTable = "cz_bet_six with(NOLOCK) ";
                string str2 = "select is_closed from cz_phase_six where is_opendata=0";
                DataTable table = DbHelperSQL.Query(str2, null).Tables[0];
                if (table.Rows.Count > 0)
                {
                    if ((table.Rows[0][0].ToString().Trim() == "1") && (_session.get_u_type() != "zj"))
                    {
                        base.noRightOptMsg(PageBase.GetMessageByCache("u100067", "MessageHint"));
                        return;
                    }
                    fTable = "cz_bet_six  with(NOLOCK) ";
                    DateTime time = Convert.ToDateTime(base.q("BeginDate"));
                    DateTime time2 = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
                    if (time < time2)
                    {
                        base.noRightOptMsg(PageBase.GetMessageByCache("u100069", "MessageHint"));
                        return;
                    }
                }
                else
                {
                    fTable = " cz_betold_six  with(NOLOCK) ";
                }
                string str3 = "";
                string str4 = "";
                string str5 = "";
                string str6 = "";
                string str7 = "";
                string str8 = "";
                base.q("t_FT");
                base.q("q_type");
                string str9 = base.q("t_Balance");
                if (base.q("t_FT") == "0")
                {
                    str3 = " phase_id=@phase_id  ";
                    item = new SqlParameter("@phase_id", SqlDbType.NVarChar) {
                        Value = base.q("t_LID")
                    };
                    list.Add(item);
                }
                else
                {
                    DateTime time3 = Convert.ToDateTime(base.q("EndDate"));
                    DateTime now = DateTime.Now;
                    string str10 = time3.ToString("yyyy-MM-dd");
                    str3 = " bet_time between  @BeginDate  and  @CqDate ";
                    item = new SqlParameter("@BeginDate", SqlDbType.NVarChar) {
                        Value = base.q("BeginDate") + " 00:00:00"
                    };
                    list.Add(item);
                    item = new SqlParameter("@CqDate", SqlDbType.NVarChar) {
                        Value = str10 + " 23:59:59"
                    };
                    list.Add(item);
                }
                base.q("mID");
                str4 = base.q("UP_ID");
                str5 = base.q("FactSize");
                if (str4 == "my")
                {
                    str4 = _session.get_u_name();
                }
                else
                {
                    str4 = base.q("UP_ID");
                }
                str7 = base.q("uID");
                if (base.Request["UP_1_ID"] == null)
                {
                    str6 = _session.get_u_name();
                }
                else
                {
                    str6 = base.q("UP_1_ID");
                }
                string str11 = "0";
                if (_session.get_u_type() == "fgs")
                {
                    str11 = _session.get_allow_view_report().ToString();
                }
                if (str11 != "1")
                {
                    if (!base.IsUpperLowerLevels(str6, _session.get_u_type(), _session.get_u_name()))
                    {
                        base.noRightOptMsg(PageBase.GetMessageByCache("u100057", "MessageHint"));
                        return;
                    }
                    if (!string.IsNullOrEmpty(str7) && !base.IsUpperLowerLevels(str7, _session.get_u_type(), _session.get_u_name()))
                    {
                        base.noRightOptMsg(PageBase.GetMessageByCache("u100057", "MessageHint"));
                        return;
                    }
                }
                if (str5 == "my")
                {
                    str5 = "zj_rate";
                }
                string str12 = "";
                string str13 = "";
                string str14 = "";
                string str15 = "";
                int num = 0;
                string str33 = _session.get_u_type();
                if (str33 != null)
                {
                    if (!(str33 == "zj"))
                    {
                        if (str33 == "fgs")
                        {
                            str14 = "fgs";
                            str15 = "gd";
                            num = 4;
                        }
                        else if (str33 == "gd")
                        {
                            str14 = "gd";
                            str15 = "zd";
                            num = 3;
                        }
                        else if (str33 == "zd")
                        {
                            str14 = "zd";
                            str15 = "dl";
                            num = 2;
                        }
                        else if (str33 == "dl")
                        {
                            str14 = "dl";
                            str15 = "hy";
                            num = 1;
                        }
                    }
                    else
                    {
                        str14 = "zj";
                        str15 = "fgs";
                        num = 5;
                    }
                }
                string str16 = "0";
                cz_users userInfoByUName = CallBLL.cz_users_bll.GetUserInfoByUName(str6);
                if (userInfoByUName == null)
                {
                    if (!_session.get_u_type().Equals("zj"))
                    {
                        if (!_session.get_u_type().Equals("fgs") || !_session.get_allow_view_report().ToString().Equals("1"))
                        {
                            base.noRightOptMsg(PageBase.GetMessageByCache("u100008", "MessageHint"));
                            return;
                        }
                        str16 = "2";
                    }
                    else
                    {
                        str12 = "fgs";
                    }
                }
                else
                {
                    str12 = userInfoByUName.get_u_type();
                }
                string str17 = base.q("zjbh");
                if ((string.IsNullOrEmpty(str7.Trim()) && str17.Equals("1")) && (_session.get_u_type().Equals("fgs") && _session.get_allow_view_report().ToString().Equals("1")))
                {
                    str7 = _session.get_zjname();
                }
                if (str7.Trim() != "")
                {
                    if (((str12 == "fgs") && _session.get_u_type().Equals("zj")) || ((_session.get_u_type().Equals("fgs") && _session.get_allow_view_report().ToString().Equals("1")) && str16.Equals("2")))
                    {
                        str13 = " and fgs_name= @fgs_name ";
                        SqlParameter parameter2 = new SqlParameter("@fgs_name", SqlDbType.NVarChar) {
                            Value = str7
                        };
                        list.Add(parameter2);
                    }
                    else
                    {
                        str13 = " and u_name=@u_name ";
                        item = new SqlParameter("@u_name", SqlDbType.NVarChar) {
                            Value = str7
                        };
                        list.Add(item);
                    }
                }
                else if (str12 == "hy")
                {
                    str13 = " and  u_name= @u_name  ";
                    SqlParameter parameter3 = new SqlParameter("@u_name", SqlDbType.NVarChar) {
                        Value = str6
                    };
                    list.Add(parameter3);
                }
                else if (str12 == "zj")
                {
                    str13 = "";
                }
                else
                {
                    str13 = " and  " + str12 + "_name = @m_type_name ";
                    SqlParameter parameter4 = new SqlParameter("@m_type_name", SqlDbType.NVarChar) {
                        Value = str6
                    };
                    list.Add(parameter4);
                }
                base.q("gID");
                if (base.q("gID") != "0")
                {
                    str13 = str13 + " and play_id=@play_id  ";
                    item = new SqlParameter("@play_id", SqlDbType.NVarChar) {
                        Value = LSRequest.qq("gID")
                    };
                    list.Add(item);
                }
                bool flag1 = str9 != "";
                int num2 = Convert.ToInt32("0" + base.q("page"));
                int num3 = 0;
                int num4 = 1;
                num2 = (num2 == 0) ? 1 : num2;
                string str18 = "";
                str18 = " and  " + str3 + " " + str13 + str8;
                num3 = Convert.ToInt32(base.GetValueByKey("count(bet_id)", fTable, "bet_id>0" + str18, list.ToArray()));
                num4 = ((num3 % this.pageSize) == 0) ? (num3 / this.pageSize) : ((num3 / this.pageSize) + 1);
                num2 = (num2 > num4) ? num4 : num2;
                if (num2 <= 0)
                {
                    num2 = 1;
                }
                str18 = " " + str3 + " " + str13 + str8;
                StringBuilder builder = new StringBuilder();
                builder.AppendFormat(" select {0} from {1} where {2} order by bet_id desc offset {3} rows fetch next {4} rows only ", new object[] { " * ", fTable, str18, (((num2 - 1) * this.pageSize) < 0) ? 0 : ((num2 - 1) * this.pageSize), this.pageSize });
                DataTable table2 = DbHelperSQL.Query(builder.ToString(), list.ToArray()).Tables[0];
                if (table2.Rows.Count == 0)
                {
                    if (num2 <= 1)
                    {
                        base.noRightOptMsg("無符合條件的數據！");
                    }
                    else
                    {
                        base.noRightOptMsg("無數據加載！");
                    }
                }
                else
                {
                    int num5 = 0;
                    double num6 = 0.0;
                    Dictionary<string, object> data = new Dictionary<string, object>();
                    Dictionary<string, List<Dictionary<string, string>>> dictionary2 = new Dictionary<string, List<Dictionary<string, string>>>();
                    List<Dictionary<string, string>> list2 = new List<Dictionary<string, string>>();
                    foreach (DataRow row in table2.Rows)
                    {
                        Dictionary<string, string> dictionary3 = new Dictionary<string, string>();
                        string str19 = row["odds_id"].ToString();
                        int num1 = num5 % 2;
                        DateTime time5 = Convert.ToDateTime(row["bet_time"].ToString());
                        if (row["u_name"].ToString().Trim() == _session.get_u_name())
                        {
                            num6 = -double.Parse(row["amount"].ToString());
                        }
                        else
                        {
                            num6 = double.Parse(row["amount"].ToString());
                        }
                        if (_session.get_u_type() == "zj")
                        {
                            if (row["fgs_name"].ToString().Trim() == _session.get_u_name())
                            {
                                num6 = -double.Parse(row["amount"].ToString());
                            }
                            else
                            {
                                num6 = double.Parse(row["amount"].ToString());
                            }
                        }
                        if (_session.get_u_type().Equals("fgs") && ((_session.get_allow_view_report() == 1) && str16.Equals("2")))
                        {
                            if (row["fgs_name"].ToString().Trim() == _session.get_zjname())
                            {
                                num6 = -double.Parse(row["amount"].ToString());
                            }
                            else
                            {
                                num6 = double.Parse(row["amount"].ToString());
                            }
                        }
                        (row["isDelete"]).Equals("1");
                        if (!string.IsNullOrEmpty(row["unit_cnt"].ToString()) && (int.Parse(row["unit_cnt"].ToString()) > 1))
                        {
                            base.GroupShowHrefString(1, row["order_num"].ToString(), row["is_payment"].ToString(), "1", "1");
                        }
                        row["is_payment"].ToString().Equals("1");
                        dictionary3.Add("zdhmsj", row["order_num"].ToString().Trim() + "#<br />" + time5.ToString("yy-MM-dd HH:mm:ss"));
                        int num9 = 100;
                        dictionary3.Add("xzlx", base.GetGameNameByID(num9.ToString()) + "<br />" + row["phase"].ToString() + "期");
                        dictionary3.Add("hy", row["u_name"].ToString().Trim() + "<br />" + row["kind"].ToString().Trim() + "盤");
                        string str21 = "";
                        if ("92565".Equals(str19))
                        {
                            str21 = string.Format("<span  class='blue'>{0}【{1}】</span>@<b class='red'>{2}</b><br />{3}", new object[] { row["play_name"].ToString(), "中", row["odds"].ToString(), FunctionSix.GetSXLNameBySXid(row["bet_val"].ToString()) });
                        }
                        else if (("92566".Equals(str19) || "92567".Equals(str19)) || ("92568".Equals(str19) || "92636".Equals(str19)))
                        {
                            string str22 = row["lm_type"].ToString();
                            if (str22.Equals("0"))
                            {
                                str21 = (string.Format("<span  class='blue'>{0}</span>@<b class='red'>{1}</b>", row["play_name"].ToString(), row["odds"].ToString().Split(new char[] { ',' })[0]) + string.Format("<br /><span  class='blue'>{0}</span>@<b class='red'>{1}</b>", "連" + base.get_YearLian(), row["odds"].ToString().Split(new char[] { ',' })[1]) + string.Format("<br />【{0}組】", row["unit_cnt"].ToString())) + string.Format("<br />{0}", FunctionSix.GetSXLNameBySXid(row["bet_val"].ToString())) + FunctionSix.GetSXLWT(row["odds_id"].ToString(), row["odds"].ToString(), row["bet_wt"].ToString(), base.get_YearLianID());
                            }
                            else if (str22.Equals("1"))
                            {
                                str21 = (string.Format("<span  class='blue'>{0}</span>@<b class='red'>{1}</b>", row["play_name"].ToString(), row["odds"].ToString().Split(new char[] { ',' })[0]) + string.Format("<br /><span  class='blue'>{0}</span>@<b class='red'>{1}</b>", "連" + base.get_YearLian(), row["odds"].ToString().Split(new char[] { ',' })[1]) + string.Format("<br />膽拖【{0}組】", row["unit_cnt"].ToString())) + string.Format("<br />{0}<br />拖<br />{1}", FunctionSix.GetSXLNameBySXid(row["bet_val"].ToString().Split(new char[] { '|' })[0]), FunctionSix.GetSXLNameBySXid(row["bet_val"].ToString().Split(new char[] { '|' })[1])) + FunctionSix.GetSXLWT(row["odds_id"].ToString(), row["odds"].ToString(), row["bet_wt"].ToString(), base.get_YearLianID());
                            }
                        }
                        else if (("92569".Equals(str19) || "92570".Equals(str19)) || ("92571".Equals(str19) || "92637".Equals(str19)))
                        {
                            string str23 = row["lm_type"].ToString();
                            if (str23.Equals("0"))
                            {
                                str21 = (string.Format("<span  class='blue'>{0}</span>@<b class='red'>{1}</b>", row["play_name"].ToString(), row["odds"].ToString().Split(new char[] { ',' })[0]) + string.Format("<br /><span  class='blue'>{0}</span>@<b class='red'>{1}</b>", "連0", row["odds"].ToString().Split(new char[] { ',' })[1]) + string.Format("<br />【{0}組】", row["unit_cnt"].ToString())) + string.Format("<br />{0}", FunctionSix.GetWSLNameBySXid(row["bet_val"].ToString())) + FunctionSix.GetWSLWT(row["odds_id"].ToString(), row["odds"].ToString(), row["bet_wt"].ToString());
                            }
                            else if (str23.Equals("1"))
                            {
                                str21 = (string.Format("<span  class='blue'>{0}</span>@<b class='red'>{1}</b>", row["play_name"].ToString(), row["odds"].ToString().Split(new char[] { ',' })[0]) + string.Format("<br /><span  class='blue'>{0}</span>@<b class='red'>{1}</b>", "連0", row["odds"].ToString().Split(new char[] { ',' })[1]) + string.Format("<br />膽拖【{0}組】", row["unit_cnt"].ToString())) + string.Format("<br />{0}<br />拖<br />{1}", FunctionSix.GetWSLNameBySXid(row["bet_val"].ToString().Split(new char[] { '|' })[0]), FunctionSix.GetWSLNameBySXid(row["bet_val"].ToString().Split(new char[] { '|' })[1])) + FunctionSix.GetWSLWT(row["odds_id"].ToString(), row["odds"].ToString(), row["bet_wt"].ToString());
                            }
                        }
                        else if ((("92572,92588,92589,92590,92591,92592".IndexOf(str19) > -1) || ("92285,92286,92287,92288,92289,92575".IndexOf(str19) > -1)) || ("92638,92639,92640,92641,92642,92643".IndexOf(str19) > -1))
                        {
                            string str24 = row["lm_type"].ToString();
                            if (str24.Equals("0"))
                            {
                                if (str19.Equals("92286") || str19.Equals("92639"))
                                {
                                    str21 = string.Format("<span  class='blue'>{0}</span>@<b class='red'>{1}</b>", row["play_name"].ToString(), row["odds"].ToString().Split(new char[] { ',' })[0]) + string.Format("<br /><span  class='blue'>{0}</span>@<b class='red'>{1}</b>", "中三", row["odds"].ToString().Split(new char[] { ',' })[1]);
                                }
                                else if (str19.Equals("92288") || str19.Equals("92641"))
                                {
                                    str21 = string.Format("<span  class='blue'>{0}</span>@<b class='red'>{1}</b>", row["play_name"].ToString(), row["odds"].ToString().Split(new char[] { ',' })[0]) + string.Format("<br /><span  class='blue'>{0}</span>@<b class='red'>{1}</b>", "中二", row["odds"].ToString().Split(new char[] { ',' })[1]);
                                }
                                else
                                {
                                    str21 = string.Format("<span  class='blue'>{0}</span>@<b class='red'>{1}</b>", row["play_name"].ToString(), row["odds"].ToString());
                                }
                                str21 = (str21 + string.Format("<br />【{0}組】", row["unit_cnt"].ToString())) + string.Format("<br />{0}", row["bet_val"].ToString()) + FunctionSix.GetLMWT(row["odds_id"].ToString(), row["odds"].ToString(), row["bet_wt"].ToString());
                            }
                            else if (str24.Equals("1"))
                            {
                                if (str19.Equals("92286") || str19.Equals("92639"))
                                {
                                    str21 = string.Format("<span  class='blue'>{0}</span>@<b class='red'>{1}</b>", row["play_name"].ToString(), row["odds"].ToString().Split(new char[] { ',' })[0]) + string.Format("<br /><span  class='blue'>{0}</span>@<b class='red'>{1}</b>", "中三", row["odds"].ToString().Split(new char[] { ',' })[1]);
                                }
                                else if (str19.Equals("92288") || str19.Equals("92641"))
                                {
                                    str21 = string.Format("<span  class='blue'>{0}</span>@<b class='red'>{1}</b>", row["play_name"].ToString(), row["odds"].ToString().Split(new char[] { ',' })[0]) + string.Format("<br /><span  class='blue'>{0}</span>@<b class='red'>{1}</b>", "中二", row["odds"].ToString().Split(new char[] { ',' })[1]);
                                }
                                else
                                {
                                    str21 = string.Format("<span  class='blue'>{0}</span>@<b class='red'>{1}</b>", row["play_name"].ToString(), row["odds"].ToString());
                                }
                                str21 = (str21 + string.Format("<br />膽拖【{0}組】", row["unit_cnt"].ToString())) + string.Format("<br />{0}<br />拖<br />{1}", row["bet_val"].ToString().Split(new char[] { '|' })[0], row["bet_val"].ToString().Split(new char[] { '|' })[1]) + FunctionSix.GetLMWT(row["odds_id"].ToString(), row["odds"].ToString(), row["bet_wt"].ToString());
                            }
                            else if (str24.Equals("2"))
                            {
                                if (str19.Equals("92286") || str19.Equals("92639"))
                                {
                                    str21 = string.Format("<span  class='blue'>{0}</span>@<b class='red'>{1}</b>", row["play_name"].ToString(), row["odds"].ToString().Split(new char[] { ',' })[0]) + string.Format("<br /><span  class='blue'>{0}</span>@<b class='red'>{1}</b>", "中三", row["odds"].ToString().Split(new char[] { ',' })[1]);
                                }
                                else if (str19.Equals("92288") || str19.Equals("92641"))
                                {
                                    str21 = string.Format("<span  class='blue'>{0}</span>@<b class='red'>{1}</b>", row["play_name"].ToString(), row["odds"].ToString().Split(new char[] { ',' })[0]) + string.Format("<br /><span  class='blue'>{0}</span>@<b class='red'>{1}</b>", "中二", row["odds"].ToString().Split(new char[] { ',' })[1]);
                                }
                                else
                                {
                                    str21 = string.Format("<span  class='blue'>{0}</span>@<b class='red'>{1}</b>", row["play_name"].ToString(), row["odds"].ToString());
                                }
                                str21 = (str21 + string.Format("<br />生肖對碰【{0}組】", row["unit_cnt"].ToString())) + string.Format("<br />{0}<br />對碰<br />{1}", row["bet_val"].ToString().Split(new char[] { '|' })[0], row["bet_val"].ToString().Split(new char[] { '|' })[1]) + FunctionSix.GetLMWT(row["odds_id"].ToString(), row["odds"].ToString(), row["bet_wt"].ToString());
                            }
                            else if (str24.Equals("3"))
                            {
                                if (str19.Equals("92286") || str19.Equals("92639"))
                                {
                                    str21 = string.Format("<span  class='blue'>{0}</span>@<b class='red'>{1}</b>", row["play_name"].ToString(), row["odds"].ToString().Split(new char[] { ',' })[0]) + string.Format("<br /><span  class='blue'>{0}</span>@<b class='red'>{1}</b>", "中三", row["odds"].ToString().Split(new char[] { ',' })[1]);
                                }
                                else if (str19.Equals("92288") || str19.Equals("92641"))
                                {
                                    str21 = string.Format("<span  class='blue'>{0}</span>@<b class='red'>{1}</b>", row["play_name"].ToString(), row["odds"].ToString().Split(new char[] { ',' })[0]) + string.Format("<br /><span  class='blue'>{0}</span>@<b class='red'>{1}</b>", "中二", row["odds"].ToString().Split(new char[] { ',' })[1]);
                                }
                                else
                                {
                                    str21 = string.Format("<span  class='blue'>{0}</span>@<b class='red'>{1}</b>", row["play_name"].ToString(), row["odds"].ToString());
                                }
                                str21 = (str21 + string.Format("<br />尾數對碰【{0}組】", row["unit_cnt"].ToString())) + string.Format("<br />{0}<br />對碰<br />{1}", row["bet_val"].ToString().Split(new char[] { '|' })[0], row["bet_val"].ToString().Split(new char[] { '|' })[1]) + FunctionSix.GetLMWT(row["odds_id"].ToString(), row["odds"].ToString(), row["bet_wt"].ToString());
                            }
                            else if (str24.Equals("4"))
                            {
                                if (str19.Equals("92286") || str19.Equals("92639"))
                                {
                                    str21 = string.Format("<span  class='blue'>{0}</span>@<b class='red'>{1}</b>", row["play_name"].ToString(), row["odds"].ToString().Split(new char[] { ',' })[0]) + string.Format("<br /><span  class='blue'>{0}</span>@<b class='red'>{1}</b>", "中三", row["odds"].ToString().Split(new char[] { ',' })[1]);
                                }
                                else if (str19.Equals("92288") || str19.Equals("92641"))
                                {
                                    str21 = string.Format("<span  class='blue'>{0}</span>@<b class='red'>{1}</b>", row["play_name"].ToString(), row["odds"].ToString().Split(new char[] { ',' })[0]) + string.Format("<br /><span  class='blue'>{0}</span>@<b class='red'>{1}</b>", "中二", row["odds"].ToString().Split(new char[] { ',' })[1]);
                                }
                                else
                                {
                                    str21 = string.Format("<span  class='blue'>{0}</span>@<b class='red'>{1}</b>", row["play_name"].ToString(), row["odds"].ToString());
                                }
                                str21 = (str21 + string.Format("<br />混合對碰【{0}組】", row["unit_cnt"].ToString())) + string.Format("<br />{0}<br />對碰<br />{1}", row["bet_val"].ToString().Split(new char[] { '|' })[0], row["bet_val"].ToString().Split(new char[] { '|' })[1]) + FunctionSix.GetLMWT(row["odds_id"].ToString(), row["odds"].ToString(), row["bet_wt"].ToString());
                            }
                        }
                        else
                        {
                            str21 = string.Format("{0}<span class='blue'>【{1}】</span>@<b class='red'>{2}</b>", row["play_name"].ToString(), row["bet_val"].ToString(), row["odds"].ToString());
                        }
                        if (row["fgs_name"].ToString().Equals(_session.get_u_name()) && _session.get_u_type().Equals("zj"))
                        {
                            str21 = str21 + "<div class='red'><外補></div>";
                        }
                        else if (_session.get_u_type().Equals("fgs") && ((_session.get_allow_view_report() == 1) && str16.Equals("2")))
                        {
                            str21 = str21 + "<div class='red'><外補></div>";
                        }
                        else if ((row["sale_type"]).Equals("1"))
                        {
                            str21 = str21 + "<div class='red'><補></div>";
                        }
                        dictionary3.Add("xzmx", str21);
                        string str25 = "";
                        if ((row["isLM"]).Equals("1"))
                        {
                            int num7 = int.Parse(row["unit_cnt"].ToString());
                            if (num7.Equals(0))
                            {
                                str25 = Math.Round(Convert.ToDecimal(num6.ToString()), 0).ToString();
                            }
                            else
                            {
                                decimal d = decimal.Parse(num6.ToString()) / num7;
                                str25 = string.Concat(new object[] { "￥", Math.Round(d, 0), "\x00d7", num7, "<br />", Math.Round(Convert.ToDecimal(num6.ToString()), 0) });
                            }
                        }
                        else
                        {
                            str25 = Math.Round(Convert.ToDecimal(num6.ToString()), 0).ToString();
                        }
                        dictionary3.Add("hyxz", str25);
                        string str26 = "";
                        if (row["is_payment"].ToString().Trim() != "1")
                        {
                            str26 = "『未派彩』";
                        }
                        else
                        {
                            str26 = Math.Round(double.Parse(row["profit"].ToString()), 2).ToString();
                        }
                        dictionary3.Add("hysy", str26);
                        string str27 = "";
                        string str28 = "";
                        string str29 = "";
                        string str30 = "";
                        string str31 = "";
                        if (num >= 1)
                        {
                            str27 = string.Format(row["dl_name"].ToString() + "<br>" + row["dl_rate"].ToString() + "%<br>" + row["hy_drawback"].ToString(), new object[0]);
                            dictionary3.Add("dl_name", str27);
                        }
                        if (num >= 2)
                        {
                            str28 = string.Format(row["zd_name"].ToString() + "<br>" + row["zd_rate"].ToString() + "%<br>" + row["dl_drawback"].ToString(), new object[0]);
                            dictionary3.Add("zd_name", str28);
                        }
                        if (num >= 3)
                        {
                            str29 = string.Format(row["gd_name"].ToString() + "<br>" + row["gd_rate"].ToString() + "%<br>" + row["zd_drawback"].ToString(), new object[0]);
                            dictionary3.Add("gd_name", str29);
                        }
                        if (num >= 4)
                        {
                            if (_session.get_u_type().Equals("fgs") && ((_session.get_allow_view_report() == 1) && str16.Equals("2")))
                            {
                                str30 = "-";
                            }
                            else
                            {
                                str30 = string.Format(row["fgs_name"].ToString() + "<br>" + row["fgs_rate"].ToString() + "%<br>" + row["gd_drawback"].ToString(), new object[0]);
                            }
                            dictionary3.Add("fgs_name", str30);
                        }
                        if (num >= 5)
                        {
                            if ((_session.get_u_type().Equals("fgs") && _session.get_allow_view_report().ToString().Equals("1")) && str16.Equals("2"))
                            {
                                str31 = "-";
                            }
                            else
                            {
                                str31 = string.Format(_session.get_u_name() + "<br>" + row["zj_rate"].ToString() + "%<br>" + row["fgs_drawback"].ToString(), new object[0]);
                            }
                            dictionary3.Add("zj_name", str31);
                        }
                        string str32 = "";
                        if (row["is_payment"].ToString().Trim() != "1")
                        {
                            str32 = "『未派彩』";
                        }
                        else
                        {
                            str32 = Math.Round((double) ((double.Parse(row["profit"].ToString()) * double.Parse(row[str14 + "_rate"].ToString())) / 100.0), 2).ToString() + "&nbsp;<br>";
                            if (double.Parse(row["profit"].ToString()) == 0.0)
                            {
                                str32 = str32 + "0";
                            }
                            else
                            {
                                str32 = str32 + Math.Round((double) ((((num6 * double.Parse(row[str14 + "_rate"].ToString())) / 100.0) * double.Parse(row[str15 + "_drawback"].ToString())) / 100.0), 2);
                            }
                        }
                        dictionary3.Add("ndjg", str32);
                        list2.Add(dictionary3);
                    }
                    dictionary2.Add("listdetail", list2);
                    data.Add("listdata", dictionary2);
                    base.successOptMsg("交收報表明細", data);
                }
            }
        }

        public void ReportDetailSix_T()
        {
            agent_userinfo_session _session = this.Session[this.Session["user_name"] + "lottery_session_user_info"] as agent_userinfo_session;
            if ((_session.get_u_type().ToLower().Equals("zj") && (_session.get_users_child_session() != null)) && (_session.get_users_child_session().get_permissions_name().IndexOf("po_4_2") < 0))
            {
                base.noRightOptMsg(PageBase.GetMessageByCache("u100057", "MessageHint"));
            }
            else if ((!_session.get_u_type().ToLower().Equals("zj") && (_session.get_users_child_session() != null)) && (_session.get_users_child_session().get_permissions_name().IndexOf("po_7_2") < 0))
            {
                base.noRightOptMsg(PageBase.GetMessageByCache("u100057", "MessageHint"));
            }
            else
            {
                List<SqlParameter> list = new List<SqlParameter>();
                SqlParameter item = new SqlParameter();
                string str = "";
                string str2 = "";
                string str3 = "";
                string str4 = "";
                string str5 = "";
                string str6 = "";
                base.q("t_FT");
                base.q("q_type");
                string str7 = base.q("t_Balance");
                string fTable = "";
                string str9 = "select is_closed,p_id from cz_phase_six where is_opendata=0";
                DataTable table = DbHelperSQL.Query(str9, null).Tables[0];
                if (table.Rows.Count > 0)
                {
                    if ((table.Rows[0][0].ToString().Trim() == "1") && (_session.get_u_type() != "zj"))
                    {
                        base.noRightOptMsg(string.Format(PageBase.GetMessageByCache("u100067", "MessageHint"), new object[0]));
                        return;
                    }
                    if (base.q("t_FT") == "0")
                    {
                        if (base.Request["t_LID"] != null)
                        {
                            if (base.q("t_LID").Trim() != table.Rows[0]["p_id"].ToString().Trim())
                            {
                                base.noRightOptMsg(string.Format(PageBase.GetMessageByCache("u100068", "MessageHint"), new object[0]));
                                return;
                            }
                        }
                        else
                        {
                            base.Response.End();
                        }
                    }
                    else
                    {
                        string str10 = DateTime.Now.ToString("yyyy-MM-dd");
                        if (base.q("BeginDate") != str10)
                        {
                            base.noRightOptMsg(string.Format(PageBase.GetMessageByCache("u100068", "MessageHint"), new object[0]));
                            return;
                        }
                    }
                    fTable = " cz_bet_six  with(NOLOCK) ";
                }
                else
                {
                    fTable = " cz_betold_six  with(NOLOCK) ";
                }
                if (base.q("t_FT") == "0")
                {
                    base.q("t_LID");
                    string str11 = "";
                    SqlParameter[] collection = Utils.GetParams(LSRequest.qq("t_LID"), ref str11, SqlDbType.Int);
                    str = string.Format(" phase_id in({0})  ", str11);
                    list.AddRange(collection);
                }
                else
                {
                    str = " bet_time between  @BeginDate and @EndDate  ";
                    item = new SqlParameter("@BeginDate", SqlDbType.NVarChar) {
                        Value = LSRequest.qq("BeginDate") + " 00:00:00"
                    };
                    list.Add(item);
                    item = new SqlParameter("@EndDate", SqlDbType.NVarChar) {
                        Value = base.q("EndDate") + " 23:59:59"
                    };
                    list.Add(item);
                }
                base.q("mID");
                str2 = base.q("UP_ID");
                str3 = base.q("FactSize");
                if (str2 == "my")
                {
                    str2 = _session.get_u_name();
                }
                else
                {
                    str2 = base.q("UP_ID");
                }
                str5 = base.q("uID");
                if (base.Request["UP_1_ID"] == null)
                {
                    str4 = _session.get_u_name();
                }
                else
                {
                    str4 = base.q("UP_1_ID");
                }
                if (!base.IsUpperLowerLevels(str4, _session.get_u_type(), _session.get_u_name()))
                {
                    base.noRightOptMsg(string.Format(PageBase.GetMessageByCache("u100057", "MessageHint"), new object[0]));
                }
                else if (!string.IsNullOrEmpty(str5) && !base.IsUpperLowerLevels(str5, _session.get_u_type(), _session.get_u_name()))
                {
                    base.noRightOptMsg(string.Format(PageBase.GetMessageByCache("u100057", "MessageHint"), new object[0]));
                }
                else
                {
                    if (str3 == "my")
                    {
                        str3 = "zj_rate";
                    }
                    string str12 = "";
                    string str13 = "";
                    string str14 = "";
                    string str15 = "";
                    int num = 0;
                    string str33 = _session.get_u_type();
                    if (str33 != null)
                    {
                        if (!(str33 == "zj"))
                        {
                            if (str33 == "fgs")
                            {
                                str14 = "fgs";
                                str15 = "gd";
                                num = 4;
                            }
                            else if (str33 == "gd")
                            {
                                str14 = "gd";
                                str15 = "zd";
                                num = 3;
                            }
                            else if (str33 == "zd")
                            {
                                str14 = "zd";
                                str15 = "dl";
                                num = 2;
                            }
                            else if (str33 == "dl")
                            {
                                str14 = "dl";
                                str15 = "hy";
                                num = 1;
                            }
                        }
                        else
                        {
                            str14 = "zj";
                            str15 = "fgs";
                            num = 5;
                        }
                    }
                    if (base.q("m_type") != "")
                    {
                        str12 = base.q("m_type");
                    }
                    else
                    {
                        str12 = _session.get_u_type();
                    }
                    if (((str12 == "fgs") && (str3 != "")) && (base.q("gID") == "0"))
                    {
                        str6 = " and u_name=@u_name  ";
                        item = new SqlParameter("@u_name", SqlDbType.NVarChar) {
                            Value = LSRequest.qq("UP_1_ID")
                        };
                        list.Add(item);
                    }
                    string str16 = base.q("zjbh");
                    string str17 = "0";
                    if ((string.IsNullOrEmpty(str5.Trim()) && str16.Equals("1")) && (_session.get_u_type().Equals("fgs") && _session.get_allow_view_report().ToString().Equals("1")))
                    {
                        str5 = _session.get_zjname();
                        str17 = "2";
                    }
                    if (str5.Trim() != "")
                    {
                        if (str12 == "fgs")
                        {
                            str13 = " and fgs_name= @fgs_name_uid ";
                            item = new SqlParameter("@fgs_name_uid", SqlDbType.NVarChar) {
                                Value = str5
                            };
                            list.Add(item);
                        }
                        else
                        {
                            str13 = " and u_name=@u_name_uid  ";
                            item = new SqlParameter("@u_name_uid", SqlDbType.NVarChar) {
                                Value = str5
                            };
                            list.Add(item);
                        }
                    }
                    else if (str12 == "hy")
                    {
                        str13 = " and  u_name=@u_name_UP_1_ID  ";
                        item = new SqlParameter("@u_name_UP_1_ID", SqlDbType.NVarChar) {
                            Value = str4
                        };
                        list.Add(item);
                    }
                    else if (str12 == "zj")
                    {
                        str13 = string.Format(" and fgs_name!='{0}'", _session.get_zjname());
                    }
                    else
                    {
                        str13 = " and " + str12 + "_name= @m_type_name ";
                        item = new SqlParameter("@m_type_name", SqlDbType.NVarChar) {
                            Value = str4
                        };
                        list.Add(item);
                    }
                    base.q("gID");
                    if (base.q("gID") != "0")
                    {
                        str13 = str13 + " and play_id=@play_id_xx  ";
                        item = new SqlParameter("@play_id_xx", SqlDbType.NVarChar) {
                            Value = LSRequest.qq("gID")
                        };
                        list.Add(item);
                    }
                    if (str7 != "")
                    {
                        str13 = str13 + " and is_payment=@is_payment  ";
                        item = new SqlParameter("@is_payment", SqlDbType.NVarChar) {
                            Value = str7
                        };
                        list.Add(item);
                    }
                    int num2 = Convert.ToInt32("0" + base.q("page"));
                    int num3 = 0;
                    int num4 = 1;
                    num2 = (num2 == 0) ? 1 : num2;
                    string str18 = "";
                    str18 = " and  " + str + " " + str13 + str6;
                    num3 = Convert.ToInt32(base.GetValueByKey("count(bet_id)", fTable, "bet_id>0" + str18, list.ToArray()));
                    num4 = ((num3 % this.pageSize) == 0) ? (num3 / this.pageSize) : ((num3 / this.pageSize) + 1);
                    num2 = (num2 > num4) ? num4 : num2;
                    if (num2 <= 0)
                    {
                        num2 = 1;
                    }
                    str18 = " " + str + " " + str13 + str6;
                    StringBuilder builder = new StringBuilder();
                    builder.AppendFormat(" select {0} from {1} where {2} order by bet_id desc offset {3} rows fetch next {4} rows only  ", new object[] { " * ", fTable, str18, (((num2 - 1) * this.pageSize) < 0) ? 0 : ((num2 - 1) * this.pageSize), this.pageSize });
                    DataTable table2 = DbHelperSQL.Query(builder.ToString(), list.ToArray()).Tables[0];
                    if (table2.Rows.Count == 0)
                    {
                        if (num2 <= 1)
                        {
                            base.noRightOptMsg("無符合條件的數據！");
                        }
                        else
                        {
                            base.noRightOptMsg("無數據加載！");
                        }
                    }
                    else
                    {
                        int num5 = 0;
                        double num6 = 0.0;
                        Dictionary<string, object> data = new Dictionary<string, object>();
                        Dictionary<string, List<Dictionary<string, string>>> dictionary2 = new Dictionary<string, List<Dictionary<string, string>>>();
                        List<Dictionary<string, string>> list2 = new List<Dictionary<string, string>>();
                        foreach (DataRow row in table2.Rows)
                        {
                            Dictionary<string, string> dictionary3 = new Dictionary<string, string>();
                            string str19 = row["odds_id"].ToString();
                            int num1 = num5 % 2;
                            DateTime time = Convert.ToDateTime(row["bet_time"].ToString());
                            if (row["u_name"].ToString().Trim() == _session.get_u_name())
                            {
                                num6 = -double.Parse(row["amount"].ToString());
                            }
                            else
                            {
                                num6 = double.Parse(row["amount"].ToString());
                            }
                            if (_session.get_u_type() == "zj")
                            {
                                if (row["fgs_name"].ToString().Trim() == _session.get_u_name())
                                {
                                    num6 = -double.Parse(row["amount"].ToString());
                                }
                                else
                                {
                                    num6 = double.Parse(row["amount"].ToString());
                                }
                            }
                            (row["isDelete"]).Equals("1");
                            if (!string.IsNullOrEmpty(row["unit_cnt"].ToString()) && (int.Parse(row["unit_cnt"].ToString()) > 1))
                            {
                                base.GroupShowHrefString(1, row["order_num"].ToString(), row["is_payment"].ToString(), "1", "1");
                            }
                            dictionary3.Add("zdhmsj", row["order_num"].ToString().Trim() + "#<br />" + time.ToString("yy-MM-dd HH:mm:ss"));
                            int num9 = 100;
                            dictionary3.Add("xzlx", base.GetGameNameByID(num9.ToString()) + "<br />" + row["phase"].ToString() + "期");
                            dictionary3.Add("hy", row["u_name"].ToString().Trim() + "<br>" + row["kind"].ToString().Trim());
                            string str21 = "";
                            if ("92565".Equals(str19))
                            {
                                str21 = string.Format("<span  class='blue'>{0}【{1}】</span>@<b class='red'>{2}</b><br />{3}", new object[] { row["play_name"].ToString(), "中", row["odds"].ToString(), FunctionSix.GetSXLNameBySXid(row["bet_val"].ToString()) });
                            }
                            else if (("92566".Equals(str19) || "92567".Equals(str19)) || ("92568".Equals(str19) || "92636".Equals(str19)))
                            {
                                string str22 = row["lm_type"].ToString();
                                if (str22.Equals("0"))
                                {
                                    str21 = (string.Format("<span  class='blue'>{0}</span>@<b class='red'>{1}</b>", row["play_name"].ToString(), row["odds"].ToString().Split(new char[] { ',' })[0]) + string.Format("<br /><span  class='blue'>{0}</span>@<b class='red'>{1}</b>", "連" + base.get_YearLian(), row["odds"].ToString().Split(new char[] { ',' })[1]) + string.Format("<br />【{0}組】", row["unit_cnt"].ToString())) + string.Format("<br />{0}", FunctionSix.GetSXLNameBySXid(row["bet_val"].ToString())) + FunctionSix.GetSXLWT(row["odds_id"].ToString(), row["odds"].ToString(), row["bet_wt"].ToString(), base.get_YearLianID());
                                }
                                else if (str22.Equals("1"))
                                {
                                    str21 = (string.Format("<span  class='blue'>{0}</span>@<b class='red'>{1}</b>", row["play_name"].ToString(), row["odds"].ToString().Split(new char[] { ',' })[0]) + string.Format("<br /><span  class='blue'>{0}</span>@<b class='red'>{1}</b>", "連" + base.get_YearLian(), row["odds"].ToString().Split(new char[] { ',' })[1]) + string.Format("<br />膽拖【{0}組】", row["unit_cnt"].ToString())) + string.Format("<br />{0}<br />拖<br />{1}", FunctionSix.GetSXLNameBySXid(row["bet_val"].ToString().Split(new char[] { '|' })[0]), FunctionSix.GetSXLNameBySXid(row["bet_val"].ToString().Split(new char[] { '|' })[1])) + FunctionSix.GetSXLWT(row["odds_id"].ToString(), row["odds"].ToString(), row["bet_wt"].ToString(), base.get_YearLianID());
                                }
                            }
                            else if (("92569".Equals(str19) || "92570".Equals(str19)) || ("92571".Equals(str19) || "92637".Equals(str19)))
                            {
                                string str23 = row["lm_type"].ToString();
                                if (str23.Equals("0"))
                                {
                                    str21 = (string.Format("<span  class='blue'>{0}</span>@<b class='red'>{1}</b>", row["play_name"].ToString(), row["odds"].ToString().Split(new char[] { ',' })[0]) + string.Format("<br /><span  class='blue'>{0}</span>@<b class='red'>{1}</b>", "連0", row["odds"].ToString().Split(new char[] { ',' })[1]) + string.Format("<br />【{0}組】", row["unit_cnt"].ToString())) + string.Format("<br />{0}", FunctionSix.GetWSLNameBySXid(row["bet_val"].ToString())) + FunctionSix.GetWSLWT(row["odds_id"].ToString(), row["odds"].ToString(), row["bet_wt"].ToString());
                                }
                                else if (str23.Equals("1"))
                                {
                                    str21 = (string.Format("<span  class='blue'>{0}</span>@<b class='red'>{1}</b>", row["play_name"].ToString(), row["odds"].ToString().Split(new char[] { ',' })[0]) + string.Format("<br /><span  class='blue'>{0}</span>@<b class='red'>{1}</b>", "連0", row["odds"].ToString().Split(new char[] { ',' })[1]) + string.Format("<br />膽拖【{0}組】", row["unit_cnt"].ToString())) + string.Format("<br />{0}<br />拖<br />{1}", FunctionSix.GetWSLNameBySXid(row["bet_val"].ToString().Split(new char[] { '|' })[0]), FunctionSix.GetWSLNameBySXid(row["bet_val"].ToString().Split(new char[] { '|' })[1])) + FunctionSix.GetWSLWT(row["odds_id"].ToString(), row["odds"].ToString(), row["bet_wt"].ToString());
                                }
                            }
                            else if ((("92572,92588,92589,92590,92591,92592".IndexOf(str19) > -1) || ("92285,92286,92287,92288,92289,92575".IndexOf(str19) > -1)) || ("92638,92639,92640,92641,92642,92643".IndexOf(str19) > -1))
                            {
                                string str24 = row["lm_type"].ToString();
                                if (str24.Equals("0"))
                                {
                                    if (str19.Equals("92286") || str19.Equals("92639"))
                                    {
                                        str21 = string.Format("<span  class='blue'>{0}</span>@<b class='red'>{1}</b>", row["play_name"].ToString(), row["odds"].ToString().Split(new char[] { ',' })[0]) + string.Format("<br /><span  class='blue'>{0}</span>@<b class='red'>{1}</b>", "中三", row["odds"].ToString().Split(new char[] { ',' })[1]);
                                    }
                                    else if (str19.Equals("92288") || str19.Equals("92641"))
                                    {
                                        str21 = string.Format("<span  class='blue'>{0}</span>@<b class='red'>{1}</b>", row["play_name"].ToString(), row["odds"].ToString().Split(new char[] { ',' })[0]) + string.Format("<br /><span  class='blue'>{0}</span>@<b class='red'>{1}</b>", "中二", row["odds"].ToString().Split(new char[] { ',' })[1]);
                                    }
                                    else
                                    {
                                        str21 = string.Format("<span  class='blue'>{0}</span>@<b class='red'>{1}</b>", row["play_name"].ToString(), row["odds"].ToString());
                                    }
                                    str21 = (str21 + string.Format("<br />【{0}組】", row["unit_cnt"].ToString())) + string.Format("<br />{0}", row["bet_val"].ToString()) + FunctionSix.GetLMWT(row["odds_id"].ToString(), row["odds"].ToString(), row["bet_wt"].ToString());
                                }
                                else if (str24.Equals("1"))
                                {
                                    if (str19.Equals("92286") || str19.Equals("92639"))
                                    {
                                        str21 = string.Format("<span  class='blue'>{0}</span>@<b class='red'>{1}</b>", row["play_name"].ToString(), row["odds"].ToString().Split(new char[] { ',' })[0]) + string.Format("<br /><span  class='blue'>{0}</span>@<b class='red'>{1}</b>", "中三", row["odds"].ToString().Split(new char[] { ',' })[1]);
                                    }
                                    else if (str19.Equals("92288") || str19.Equals("92641"))
                                    {
                                        str21 = string.Format("<span  class='blue'>{0}</span>@<b class='red'>{1}</b>", row["play_name"].ToString(), row["odds"].ToString().Split(new char[] { ',' })[0]) + string.Format("<br /><span  class='blue'>{0}</span>@<b class='red'>{1}</b>", "中二", row["odds"].ToString().Split(new char[] { ',' })[1]);
                                    }
                                    else
                                    {
                                        base.Response.Write(string.Format("<span  class='blue'>{0}</span>@<b class='red'>{1}</b>", row["play_name"].ToString(), row["odds"].ToString()));
                                    }
                                    str21 = (str21 + string.Format("<br />膽拖【{0}組】", row["unit_cnt"].ToString())) + string.Format("<br />{0}<br />拖<br />{1}", row["bet_val"].ToString().Split(new char[] { '|' })[0], row["bet_val"].ToString().Split(new char[] { '|' })[1]) + FunctionSix.GetLMWT(row["odds_id"].ToString(), row["odds"].ToString(), row["bet_wt"].ToString());
                                }
                                else if (str24.Equals("2"))
                                {
                                    if (str19.Equals("92286") || str19.Equals("92639"))
                                    {
                                        str21 = string.Format("<span  class='blue'>{0}</span>@<b class='red'>{1}</b>", row["play_name"].ToString(), row["odds"].ToString().Split(new char[] { ',' })[0]) + string.Format("<br /><span  class='blue'>{0}</span>@<b class='red'>{1}</b>", "中三", row["odds"].ToString().Split(new char[] { ',' })[1]);
                                    }
                                    else if (str19.Equals("92288") || str19.Equals("92641"))
                                    {
                                        str21 = string.Format("<span  class='blue'>{0}</span>@<b class='red'>{1}</b>", row["play_name"].ToString(), row["odds"].ToString().Split(new char[] { ',' })[0]) + string.Format("<br /><span  class='blue'>{0}</span>@<b class='red'>{1}</b>", "中二", row["odds"].ToString().Split(new char[] { ',' })[1]);
                                    }
                                    else
                                    {
                                        base.Response.Write(string.Format("<span  class='blue'>{0}</span>@<b class='red'>{1}</b>", row["play_name"].ToString(), row["odds"].ToString()));
                                    }
                                    str21 = (str21 + string.Format("<br />生肖對碰【{0}組】", row["unit_cnt"].ToString())) + string.Format("<br />{0}<br />對碰<br />{1}", row["bet_val"].ToString().Split(new char[] { '|' })[0], row["bet_val"].ToString().Split(new char[] { '|' })[1]) + FunctionSix.GetLMWT(row["odds_id"].ToString(), row["odds"].ToString(), row["bet_wt"].ToString());
                                }
                                else if (str24.Equals("3"))
                                {
                                    if (str19.Equals("92286") || str19.Equals("92639"))
                                    {
                                        str21 = string.Format("<span  class='blue'>{0}</span>@<b class='red'>{1}</b>", row["play_name"].ToString(), row["odds"].ToString().Split(new char[] { ',' })[0]) + string.Format("<br /><span  class='blue'>{0}</span>@<b class='red'>{1}</b>", "中三", row["odds"].ToString().Split(new char[] { ',' })[1]);
                                    }
                                    else if (str19.Equals("92288") || str19.Equals("92641"))
                                    {
                                        str21 = string.Format("<span  class='blue'>{0}</span>@<b class='red'>{1}</b>", row["play_name"].ToString(), row["odds"].ToString().Split(new char[] { ',' })[0]) + string.Format("<br /><span  class='blue'>{0}</span>@<b class='red'>{1}</b>", "中二", row["odds"].ToString().Split(new char[] { ',' })[1]);
                                    }
                                    else
                                    {
                                        str21 = string.Format("<span  class='blue'>{0}</span>@<b class='red'>{1}</b>", row["play_name"].ToString(), row["odds"].ToString());
                                    }
                                    str21 = (str21 + string.Format("<br />尾數對碰【{0}組】", row["unit_cnt"].ToString())) + string.Format("<br />{0}<br />對碰<br />{1}", row["bet_val"].ToString().Split(new char[] { '|' })[0], row["bet_val"].ToString().Split(new char[] { '|' })[1]) + FunctionSix.GetLMWT(row["odds_id"].ToString(), row["odds"].ToString(), row["bet_wt"].ToString());
                                }
                                else if (str24.Equals("4"))
                                {
                                    if (str19.Equals("92286") || str19.Equals("92639"))
                                    {
                                        str21 = string.Format("<span  class='blue'>{0}</span>@<b class='red'>{1}</b>", row["play_name"].ToString(), row["odds"].ToString().Split(new char[] { ',' })[0]) + string.Format("<br /><span  class='blue'>{0}</span>@<b class='red'>{1}</b>", "中三", row["odds"].ToString().Split(new char[] { ',' })[1]);
                                    }
                                    else if (str19.Equals("92288") || str19.Equals("92641"))
                                    {
                                        str21 = string.Format("<span  class='blue'>{0}</span>@<b class='red'>{1}</b>", row["play_name"].ToString(), row["odds"].ToString().Split(new char[] { ',' })[0]) + string.Format("<br /><span  class='blue'>{0}</span>@<b class='red'>{1}</b>", "中二", row["odds"].ToString().Split(new char[] { ',' })[1]);
                                    }
                                    else
                                    {
                                        str21 = string.Format("<span  class='blue'>{0}</span>@<b class='red'>{1}</b>", row["play_name"].ToString(), row["odds"].ToString());
                                    }
                                    str21 = (str21 + string.Format("<br />混合對碰【{0}組】", row["unit_cnt"].ToString())) + string.Format("<br />{0}<br />對碰<br />{1}", row["bet_val"].ToString().Split(new char[] { '|' })[0], row["bet_val"].ToString().Split(new char[] { '|' })[1]) + FunctionSix.GetLMWT(row["odds_id"].ToString(), row["odds"].ToString(), row["bet_wt"].ToString());
                                }
                            }
                            else
                            {
                                str21 = string.Format("{0}<span class='blue'>【{1}】</span>@<b class='red'>{2}</b>", row["play_name"].ToString(), row["bet_val"].ToString(), row["odds"].ToString());
                            }
                            if (row["fgs_name"].ToString().Equals(_session.get_u_name()) && _session.get_u_type().Equals("zj"))
                            {
                                str21 = str21 + "<br /><span class='red'><外補></span>";
                            }
                            else if ((row["sale_type"]).Equals("1"))
                            {
                                str21 = str21 + "<br /><span class='red'><補></span>";
                            }
                            dictionary3.Add("xzmx", str21);
                            string str25 = "";
                            if ((row["isLM"]).Equals("1"))
                            {
                                int num7 = int.Parse(row["unit_cnt"].ToString());
                                if (num7.Equals(0))
                                {
                                    str25 = Math.Round(Convert.ToDecimal(row["amount"].ToString()), 0).ToString();
                                }
                                else
                                {
                                    decimal d = decimal.Parse(row["amount"].ToString()) / num7;
                                    str25 = string.Concat(new object[] { "￥", Math.Round(d, 0), "\x00d7", num7, "<br />", Math.Round(Convert.ToDecimal(row["amount"].ToString()), 0) });
                                }
                            }
                            else
                            {
                                str25 = Math.Round(Convert.ToDecimal(row["amount"].ToString()), 0).ToString();
                            }
                            dictionary3.Add("hyxz", str25);
                            string str26 = "";
                            if (row["is_payment"].ToString().Trim() != "1")
                            {
                                str26 = "『未派彩』";
                            }
                            else
                            {
                                str26 = Math.Round(double.Parse(row["profit"].ToString()), 2).ToString();
                            }
                            dictionary3.Add("hysy", str26);
                            string str27 = "";
                            string str28 = "";
                            string str29 = "";
                            string str30 = "";
                            string str31 = "";
                            if (num >= 1)
                            {
                                str27 = row["dl_name"].ToString() + "<br>" + row["dl_rate"].ToString() + "%<br>" + row["hy_drawback"].ToString();
                                dictionary3.Add("dl_name", str27);
                            }
                            if (num >= 2)
                            {
                                str28 = row["zd_name"].ToString() + "<br>" + row["zd_rate"].ToString() + "%<br>" + row["dl_drawback"].ToString();
                                dictionary3.Add("zd_name", str28);
                            }
                            if (num >= 3)
                            {
                                str29 = row["gd_name"].ToString() + "<br>" + row["gd_rate"].ToString() + "%<br>" + row["zd_drawback"].ToString();
                                dictionary3.Add("gd_name", str29);
                            }
                            if (num >= 4)
                            {
                                if (_session.get_u_type().Equals("fgs") && ((_session.get_allow_view_report() == 1) && str17.Equals("2")))
                                {
                                    str30 = "-";
                                }
                                else
                                {
                                    str30 = row["fgs_name"].ToString();
                                }
                                str33 = str30;
                                str30 = str33 + "<br>" + row["fgs_rate"].ToString() + "%<br>" + row["gd_drawback"].ToString();
                                dictionary3.Add("fgs_name", str30);
                            }
                            if (num >= 5)
                            {
                                str31 = _session.get_u_name() + "<br>" + row["zj_rate"].ToString() + "%<br>" + row["fgs_drawback"].ToString();
                                dictionary3.Add("zj_name", str31);
                            }
                            string str32 = "";
                            if (row["is_payment"].ToString().Trim() != "1")
                            {
                                str32 = "『未派彩』";
                            }
                            else
                            {
                                str32 = Math.Round((double) ((double.Parse(row["profit"].ToString()) * double.Parse(row[str14 + "_rate"].ToString())) / 100.0), 2).ToString() + "&nbsp;<br>";
                                if (double.Parse(row["profit"].ToString()) == 0.0)
                                {
                                    str32 = str32 + "0";
                                }
                                else
                                {
                                    str32 = str32 + Math.Round((double) ((((num6 * double.Parse(row[str14 + "_rate"].ToString())) / 100.0) * double.Parse(row[str15 + "_drawback"].ToString())) / 100.0), 2);
                                }
                            }
                            dictionary3.Add("ndjg", str32);
                            list2.Add(dictionary3);
                        }
                        dictionary2.Add("listdetail", list2);
                        data.Add("listdata", dictionary2);
                        base.successOptMsg("分類報表明細", data);
                    }
                }
            }
        }
    }
}

