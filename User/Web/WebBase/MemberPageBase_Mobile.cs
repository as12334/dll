namespace User.Web.WebBase
{
    using LotterySystem.Common;
    using LotterySystem.Common.ServiceStackRedis;
    using LotterySystem.Model;
    using LotterySystem.WebPageBase;
    using ServiceStack.Redis;
    using System;
    using System.Data;
    using System.Web;

    public class MemberPageBase_Mobile : MemberPageBase
    {
        public MemberPageBase_Mobile() : base("mobile")
        {
            base.Load += new EventHandler(this.MemberPageBase_Mobile_Load);
        }

        protected string bbh()
        {
            return PageBase.get_GetLottorySystemName();
        }

        protected string CodeValidateByMobile()
        {
            string str = string.Empty;
            Random random = new Random();
            for (int i = 0; i < 4; i++)
            {
                int num = random.Next();
                str = str + ((char) (0x30 + ((ushort) (num % 10)))).ToString();
            }
            return (DateTime.Now.ToString("MMddhhmmss") + str);
        }

        protected void IsOpenLottery()
        {
            string str = LSRequest.qq("player_type");
            string str2 = "";
            int? nullable = null;
            DataTable currentPhase = null;
            if ((this.Context.Request.Path.ToLower().IndexOf("cqsc_") > -1) && (this.Context.Request.Path.ToLower().IndexOf(".aspx") > -1))
            {
                currentPhase = CallBLL.cz_phase_cqsc_bll.IsPhaseClose();
                nullable = 1;
                str2 = "lm";
            }
            if ((this.Context.Request.Path.ToLower().IndexOf("kl10_") > -1) && (this.Context.Request.Path.ToLower().IndexOf(".aspx") > -1))
            {
                currentPhase = CallBLL.cz_phase_kl10_bll.IsPhaseClose();
                nullable = 0;
                str2 = "lm";
            }
            if ((this.Context.Request.Path.ToLower().IndexOf("pk10_") > -1) && (this.Context.Request.Path.ToLower().IndexOf(".aspx") > -1))
            {
                currentPhase = CallBLL.cz_phase_pk10_bll.IsPhaseClose();
                nullable = 2;
                str2 = "lm";
            }
            if ((this.Context.Request.Path.ToLower().IndexOf("xync_") > -1) && (this.Context.Request.Path.ToLower().IndexOf(".aspx") > -1))
            {
                currentPhase = CallBLL.cz_phase_xync_bll.IsPhaseClose();
                nullable = 3;
                str2 = "lm";
            }
            if ((this.Context.Request.Path.ToLower().IndexOf("jsk3.aspx") > -1) && (this.Context.Request.Path.ToLower().IndexOf(".aspx") > -1))
            {
                currentPhase = CallBLL.cz_phase_jsk3_bll.IsPhaseClose();
                nullable = 4;
                str2 = "";
            }
            if ((this.Context.Request.Path.ToLower().IndexOf("kl8_") > -1) && (this.Context.Request.Path.ToLower().IndexOf(".aspx") > -1))
            {
                currentPhase = CallBLL.cz_phase_kl8_bll.IsPhaseClose();
                nullable = 5;
                str2 = "zh";
            }
            if ((this.Context.Request.Path.ToLower().IndexOf("k8sc_") > -1) && (this.Context.Request.Path.ToLower().IndexOf(".aspx") > -1))
            {
                currentPhase = CallBLL.cz_phase_k8sc_bll.IsPhaseClose();
                nullable = 6;
                str2 = "lm";
            }
            if ((this.Context.Request.Path.ToLower().IndexOf("pcdd_") > -1) && (this.Context.Request.Path.ToLower().IndexOf(".aspx") > -1))
            {
                currentPhase = CallBLL.cz_phase_pcdd_bll.IsPhaseClose();
                nullable = 7;
                str2 = "lmp";
            }
            if ((this.Context.Request.Path.ToLower().IndexOf("six_") > -1) && (this.Context.Request.Path.ToLower().IndexOf(".aspx") > -1))
            {
                currentPhase = CallBLL.cz_phase_six_bll.GetCurrentPhase("1");
                nullable = 100;
                str2 = "tm";
            }
            if ((this.Context.Request.Path.ToLower().IndexOf("xyft5_") > -1) && (this.Context.Request.Path.ToLower().IndexOf(".aspx") > -1))
            {
                currentPhase = CallBLL.cz_phase_xyft5_bll.IsPhaseClose();
                nullable = 9;
                str2 = "lm";
            }
            if ((this.Context.Request.Path.ToLower().IndexOf("pkbjl_") > -1) && (this.Context.Request.Path.ToLower().IndexOf(".aspx") > -1))
            {
                currentPhase = CallBLL.cz_phase_pkbjl_bll.IsPhaseClose();
                nullable = 8;
                str2 = "p1";
            }
            if ((this.Context.Request.Path.ToLower().IndexOf("jscar_") > -1) && (this.Context.Request.Path.ToLower().IndexOf(".aspx") > -1))
            {
                currentPhase = CallBLL.cz_phase_jscar_bll.IsPhaseClose();
                nullable = 10;
                str2 = "lm";
            }
            if ((this.Context.Request.Path.ToLower().IndexOf("speed5_") > -1) && (this.Context.Request.Path.ToLower().IndexOf(".aspx") > -1))
            {
                currentPhase = CallBLL.cz_phase_speed5_bll.IsPhaseClose();
                nullable = 11;
                str2 = "lm";
            }
            if ((this.Context.Request.Path.ToLower().IndexOf("jscqsc_") > -1) && (this.Context.Request.Path.ToLower().IndexOf(".aspx") > -1))
            {
                currentPhase = CallBLL.cz_phase_jscqsc_bll.IsPhaseClose();
                nullable = 13;
                str2 = "lm";
            }
            if ((this.Context.Request.Path.ToLower().IndexOf("jspk10_") > -1) && (this.Context.Request.Path.ToLower().IndexOf(".aspx") > -1))
            {
                currentPhase = CallBLL.cz_phase_jspk10_bll.IsPhaseClose();
                nullable = 12;
                str2 = "lm";
            }
            if ((this.Context.Request.Path.ToLower().IndexOf("jssfc_") > -1) && (this.Context.Request.Path.ToLower().IndexOf(".aspx") > -1))
            {
                currentPhase = CallBLL.cz_phase_jssfc_bll.IsPhaseClose();
                nullable = 14;
                str2 = "lm";
            }
            if ((this.Context.Request.Path.ToLower().IndexOf("jsft2_") > -1) && (this.Context.Request.Path.ToLower().IndexOf(".aspx") > -1))
            {
                currentPhase = CallBLL.cz_phase_jsft2_bll.IsPhaseClose();
                nullable = 15;
                str2 = "lm";
            }
            if ((this.Context.Request.Path.ToLower().IndexOf("car168_") > -1) && (this.Context.Request.Path.ToLower().IndexOf(".aspx") > -1))
            {
                currentPhase = CallBLL.cz_phase_car168_bll.IsPhaseClose();
                nullable = 0x10;
                str2 = "lm";
            }
            if ((this.Context.Request.Path.ToLower().IndexOf("ssc168_") > -1) && (this.Context.Request.Path.ToLower().IndexOf(".aspx") > -1))
            {
                currentPhase = CallBLL.cz_phase_ssc168_bll.IsPhaseClose();
                nullable = 0x11;
                str2 = "lm";
            }
            if ((this.Context.Request.Path.ToLower().IndexOf("vrcar_") > -1) && (this.Context.Request.Path.ToLower().IndexOf(".aspx") > -1))
            {
                currentPhase = CallBLL.cz_phase_vrcar_bll.IsPhaseClose();
                nullable = 0x12;
                str2 = "lm";
            }
            if ((this.Context.Request.Path.ToLower().IndexOf("vrssc_") > -1) && (this.Context.Request.Path.ToLower().IndexOf(".aspx") > -1))
            {
                currentPhase = CallBLL.cz_phase_vrssc_bll.IsPhaseClose();
                nullable = 0x13;
                str2 = "lm";
            }
            if ((this.Context.Request.Path.ToLower().IndexOf("xyftoa_") > -1) && (this.Context.Request.Path.ToLower().IndexOf(".aspx") > -1))
            {
                currentPhase = CallBLL.cz_phase_xyftoa_bll.IsPhaseClose();
                nullable = 20;
                str2 = "lm";
            }
            if ((this.Context.Request.Path.ToLower().IndexOf("xyftsg_") > -1) && (this.Context.Request.Path.ToLower().IndexOf(".aspx") > -1))
            {
                currentPhase = CallBLL.cz_phase_xyftsg_bll.IsPhaseClose();
                nullable = 0x15;
                str2 = "lm";
            }
            if ((this.Context.Request.Path.ToLower().IndexOf("happycar_") > -1) && (this.Context.Request.Path.ToLower().IndexOf(".aspx") > -1))
            {
                currentPhase = CallBLL.cz_phase_happycar_bll.IsPhaseClose();
                nullable = 0x16;
                str2 = "lm";
            }
            if (!string.IsNullOrEmpty(str))
            {
                str2 = str;
            }
            if (nullable.HasValue)
            {
                DataTable lotteryList = base.GetLotteryList();
                if (!nullable.Equals(100))
                {
                    if (currentPhase.Rows[0]["isopen"].ToString().Equals("0"))
                    {
                        base.Response.Redirect(string.Format("/m/ClosedLottery.aspx?lottery_type={0}&player_type={1}", nullable, str2), true);
                        base.Response.End();
                    }
                }
                else
                {
                    cz_phase_six _six = CallBLL.cz_phase_six_bll.GetCurrentPhase();
                    DateTime now = DateTime.Now;
                    string str3 = "n";
                    DateTime time2 = Convert.ToDateTime(_six.get_stop_date());
                    DateTime time3 = Convert.ToDateTime(_six.get_sn_stop_date());
                    if (time2 >= now)
                    {
                        str3 = "y";
                    }
                    if (time3 >= now)
                    {
                        str3 = "y";
                    }
                    if (_six.get_is_closed().ToString().Equals("1") || str3.Equals("n"))
                    {
                        base.Response.Redirect(string.Format("/m/ClosedLottery.aspx?lottery_type={0}&player_type={1}", nullable, str2), true);
                        base.Response.End();
                    }
                }
            }
        }

        protected void IsOpenSix()
        {
            cz_phase_six currentPhase = CallBLL.cz_phase_six_bll.GetCurrentPhase();
            DateTime now = DateTime.Now;
            string str = "n";
            DateTime time2 = Convert.ToDateTime(currentPhase.get_stop_date());
            DateTime time3 = Convert.ToDateTime(currentPhase.get_sn_stop_date());
            if (time2 >= now)
            {
                str = "y";
            }
            if (time3 >= now)
            {
                str = "y";
            }
            if (currentPhase.get_is_closed().ToString().Equals("1") || str.Equals("n"))
            {
                base.Response.Redirect(string.Format("ClosedLottery.aspx?lottery_type=100&player_type=tmA", new object[0]), true);
                base.Response.End();
            }
        }

        protected void IsUserLoginByMobile()
        {
            if (((HttpContext.Current.Session["user_name"] == null) || base.IsUserOut(HttpContext.Current.Session["user_name"].ToString())) || PageBase.IsNeedPopBrower(HttpContext.Current.Session["user_name"].ToString()))
            {
                this.Session.Abandon();
                base.Response.Write("<script>top.location.href='/m'</script>");
                base.Response.End();
            }
            if ((base.GetUserModelInfo != null) && !(string.IsNullOrEmpty(this.Session["modifypassword"]) || (this.Context.Request.Path.ToLower().IndexOf("amendpwd.aspx") >= 0)))
            {
                base.Response.Redirect(string.Format("AmendPwd.aspx", new object[0]), true);
                base.Response.End();
            }
        }

        protected bool IsUserLoginByMobileForAjax()
        {
            string str;
            cz_stat_online _online;
            if (HttpContext.Current.Session["user_name"] == null)
            {
                this.Session.Abandon();
                return false;
            }
            if (FileCacheHelper.get_RedisStatOnline().Equals(1))
            {
                if (PageBase.IsNeedPopBrower(HttpContext.Current.Session["user_name"].ToString()))
                {
                    this.Session.Abandon();
                    return false;
                }
                str = HttpContext.Current.Session["user_name"].ToString();
                if (CallBLL.redisHelper.HashExists("useronline:list", str))
                {
                    _online = CallBLL.redisHelper.HashGet<cz_stat_online>("useronline:list", str);
                    if ((_online != null) && _online.get_is_out().Equals(1))
                    {
                        this.Session.Abandon();
                        return false;
                    }
                }
            }
            else if (FileCacheHelper.get_RedisStatOnline().Equals(2))
            {
                if (PageBase.IsNeedPopBrower(HttpContext.Current.Session["user_name"].ToString()))
                {
                    this.Session.Abandon();
                    return false;
                }
                str = HttpContext.Current.Session["user_name"].ToString();
                using (RedisClient client = new RedisClient(RedisConnectSplit.get_RedisIP(), RedisConnectSplit.get_RedisPort(), RedisConnectSplit.get_RedisPassword(), (long) FileCacheHelper.get_GetRedisDBIndex()))
                {
                    client.set_ConnectTimeout(int.Parse(RedisConnectSplit.get_RedisConnectTimeout()));
                    if (client.HashContainsEntry("useronline:list", str))
                    {
                        _online = JsonHandle.JsonToObject<cz_stat_online>(client.GetValueFromHash("useronline:list", str)) as cz_stat_online;
                        if ((_online != null) && _online.get_is_out().Equals(1))
                        {
                            this.Session.Abandon();
                            return false;
                        }
                    }
                }
            }
            else if (base.IsUserOut(HttpContext.Current.Session["user_name"].ToString()) || PageBase.IsNeedPopBrower(HttpContext.Current.Session["user_name"].ToString()))
            {
                this.Session.Abandon();
                return false;
            }
            return true;
        }

        public string LotteryTypeSave()
        {
            if (HttpContext.Current.Session["LotteryTypeByPhone"] == null)
            {
                DataTable lotteryList = base.GetLotteryList();
                string str = lotteryList.Rows[0]["id"].ToString();
                HttpContext.Current.Session["LotteryTypeByPhone"] = lotteryList.Rows[0]["id"].ToString();
                return str;
            }
            return HttpContext.Current.Session["LotteryTypeByPhone"].ToString();
        }

        public void MemberPageBase_Mobile_Load(object sender, EventArgs e)
        {
            cz_userinfo_session _session = HttpContext.Current.Session[HttpContext.Current.Session["user_name"] + "lottery_session_user_info"] as cz_userinfo_session;
            if (_session != null)
            {
                if ((this.Session["Session_LoginSystem_Flag"]) != "LoginSystem_PhoneWeb")
                {
                    this.Session.Abandon();
                    base.Response.Write("<script>top.location.href='/m'</script>");
                    base.Response.End();
                }
                this.IsOpenLottery();
                this.RedirectReport();
            }
        }

        protected void OutJson(string strResult)
        {
            base.Response.ContentType = "text/json";
            base.Response.Write(strResult);
            base.Response.End();
        }

        protected void RedirectReport()
        {
            int num = 1;
            if (HttpContext.Current.Session["user_state"].ToString().Equals(num.ToString()) && ((((((((((this.Context.Request.Path.ToLower().IndexOf("cqsc_d1.aspx") > 0) || (this.Context.Request.Path.ToLower().IndexOf("cqsc_d2.aspx") > 0)) || ((this.Context.Request.Path.ToLower().IndexOf("cqsc_d3.aspx") > 0) || (this.Context.Request.Path.ToLower().IndexOf("cqsc_d4.aspx") > 0))) || (((this.Context.Request.Path.ToLower().IndexOf("cqsc_dq.aspx") > 0) || (this.Context.Request.Path.ToLower().IndexOf("cqsc_lm.aspx") > 0)) || ((this.Context.Request.Path.ToLower().IndexOf("kl10_d1.aspx") > 0) || (this.Context.Request.Path.ToLower().IndexOf("kl10_d2.aspx") > 0)))) || ((((this.Context.Request.Path.ToLower().IndexOf("kl10_d3.aspx") > 0) || (this.Context.Request.Path.ToLower().IndexOf("kl10_d4.aspx") > 0)) || ((this.Context.Request.Path.ToLower().IndexOf("kl10_d5.aspx") > 0) || (this.Context.Request.Path.ToLower().IndexOf("kl10_d6.aspx") > 0))) || (((this.Context.Request.Path.ToLower().IndexOf("kl10_d7.aspx") > 0) || (this.Context.Request.Path.ToLower().IndexOf("kl10_d8.aspx") > 0)) || ((this.Context.Request.Path.ToLower().IndexOf("kl10_lm.aspx") > 0) || (this.Context.Request.Path.ToLower().IndexOf("kl10_zh.aspx") > 0))))) || (((((this.Context.Request.Path.ToLower().IndexOf("pk10_3456.aspx") > 0) || (this.Context.Request.Path.ToLower().IndexOf("pk10_78910.aspx") > 0)) || ((this.Context.Request.Path.ToLower().IndexOf("pk10_dq.aspx") > 0) || (this.Context.Request.Path.ToLower().IndexOf("pk10_lm.aspx") > 0))) || (((this.Context.Request.Path.ToLower().IndexOf("pk10_zh.aspx") > 0) || (this.Context.Request.Path.ToLower().IndexOf("xync_d1.aspx") > 0)) || ((this.Context.Request.Path.ToLower().IndexOf("xync_d2.aspx") > 0) || (this.Context.Request.Path.ToLower().IndexOf("xync_d3.aspx") > 0)))) || ((((this.Context.Request.Path.ToLower().IndexOf("xync_d4.aspx") > 0) || (this.Context.Request.Path.ToLower().IndexOf("xync_d5.aspx") > 0)) || ((this.Context.Request.Path.ToLower().IndexOf("xync_d6.aspx") > 0) || (this.Context.Request.Path.ToLower().IndexOf("xync_d7.aspx") > 0))) || (((this.Context.Request.Path.ToLower().IndexOf("xync_d8.aspx") > 0) || (this.Context.Request.Path.ToLower().IndexOf("xync_lm.aspx") > 0)) || ((this.Context.Request.Path.ToLower().IndexOf("xync_zd.aspx") > 0) || (this.Context.Request.Path.ToLower().IndexOf("jsk3.aspx") > 0)))))) || ((((((this.Context.Request.Path.ToLower().IndexOf("kl8_zh.aspx") > 0) || (this.Context.Request.Path.ToLower().IndexOf("kl8_zm.aspx") > 0)) || ((this.Context.Request.Path.ToLower().IndexOf("k8sc_d1.aspx") > 0) || (this.Context.Request.Path.ToLower().IndexOf("k8sc_d2.aspx") > 0))) || (((this.Context.Request.Path.ToLower().IndexOf("k8sc_d3.aspx") > 0) || (this.Context.Request.Path.ToLower().IndexOf("k8sc_d4.aspx") > 0)) || ((this.Context.Request.Path.ToLower().IndexOf("k8sc_dq.aspx") > 0) || (this.Context.Request.Path.ToLower().IndexOf("k8sc_lm.aspx") > 0)))) || ((((this.Context.Request.Path.ToLower().IndexOf("pcdd_lmp.aspx") > 0) || (this.Context.Request.Path.ToLower().IndexOf("pcdd_tm.aspx") > 0)) || ((this.Context.Request.Path.ToLower().IndexOf("pcdd_lm.aspx") > 0) || (this.Context.Request.Path.ToLower().IndexOf("xyft5_3456.aspx") > 0))) || (((this.Context.Request.Path.ToLower().IndexOf("xyft5_78910.aspx") > 0) || (this.Context.Request.Path.ToLower().IndexOf("xyft5_dq.aspx") > 0)) || ((this.Context.Request.Path.ToLower().IndexOf("xyft5_lm.aspx") > 0) || (this.Context.Request.Path.ToLower().IndexOf("xyft5_zh.aspx") > 0))))) || (((((this.Context.Request.Path.ToLower().IndexOf("touzhu_0.aspx") > 0) || (this.Context.Request.Path.ToLower().IndexOf("touzhu_1.aspx") > 0)) || ((this.Context.Request.Path.ToLower().IndexOf("touzhu_2.aspx") > 0) || (this.Context.Request.Path.ToLower().IndexOf("touzhu_3.aspx") > 0))) || (((this.Context.Request.Path.ToLower().IndexOf("touzhu_4.aspx") > 0) || (this.Context.Request.Path.ToLower().IndexOf("touzhu_5.aspx") > 0)) || ((this.Context.Request.Path.ToLower().IndexOf("touzhu_6.aspx") > 0) || (this.Context.Request.Path.ToLower().IndexOf("touzhu_7.aspx") > 0)))) || ((((this.Context.Request.Path.ToLower().IndexOf("touzhu_100.aspx") > 0) || (this.Context.Request.Path.ToLower().IndexOf("pkbjl_p1.aspx") > 0)) || ((this.Context.Request.Path.ToLower().IndexOf("pkbjl_p2.aspx") > 0) || (this.Context.Request.Path.ToLower().IndexOf("pkbjl_p3.aspx") > 0))) || (((this.Context.Request.Path.ToLower().IndexOf("pkbjl_p4.aspx") > 0) || (this.Context.Request.Path.ToLower().IndexOf("pkbjl_p5.aspx") > 0)) || ((this.Context.Request.Path.ToLower().IndexOf("speed5_d1.aspx") > 0) || (this.Context.Request.Path.ToLower().IndexOf("speed5_d2.aspx") > 0))))))) || (((((((this.Context.Request.Path.ToLower().IndexOf("speed5_d3.aspx") > 0) || (this.Context.Request.Path.ToLower().IndexOf("speed5_d4.aspx") > 0)) || ((this.Context.Request.Path.ToLower().IndexOf("speed5_dq.aspx") > 0) || (this.Context.Request.Path.ToLower().IndexOf("speed5_lm.aspx") > 0))) || (((this.Context.Request.Path.ToLower().IndexOf("jscar_3456.aspx") > 0) || (this.Context.Request.Path.ToLower().IndexOf("jscar_78910.aspx") > 0)) || ((this.Context.Request.Path.ToLower().IndexOf("jscar_dq.aspx") > 0) || (this.Context.Request.Path.ToLower().IndexOf("jscar_lm.aspx") > 0)))) || ((((this.Context.Request.Path.ToLower().IndexOf("jscar_zh.aspx") > 0) || (this.Context.Request.Path.ToLower().IndexOf("jscqsc_d1.aspx") > 0)) || ((this.Context.Request.Path.ToLower().IndexOf("jscqsc_d2.aspx") > 0) || (this.Context.Request.Path.ToLower().IndexOf("jscqsc_d3.aspx") > 0))) || (((this.Context.Request.Path.ToLower().IndexOf("jscqsc_d4.aspx") > 0) || (this.Context.Request.Path.ToLower().IndexOf("jscqsc_dq.aspx") > 0)) || ((this.Context.Request.Path.ToLower().IndexOf("jscqsc_lm.aspx") > 0) || (this.Context.Request.Path.ToLower().IndexOf("jspk10_3456.aspx") > 0))))) || (((((this.Context.Request.Path.ToLower().IndexOf("jspk10_78910.aspx") > 0) || (this.Context.Request.Path.ToLower().IndexOf("jspk10_dq.aspx") > 0)) || ((this.Context.Request.Path.ToLower().IndexOf("jspk10_lm.aspx") > 0) || (this.Context.Request.Path.ToLower().IndexOf("jspk10_zh.aspx") > 0))) || (((this.Context.Request.Path.ToLower().IndexOf("jssfc_d1.aspx") > 0) || (this.Context.Request.Path.ToLower().IndexOf("jssfc_d2.aspx") > 0)) || ((this.Context.Request.Path.ToLower().IndexOf("jssfc_d3.aspx") > 0) || (this.Context.Request.Path.ToLower().IndexOf("jssfc_d4.aspx") > 0)))) || ((((this.Context.Request.Path.ToLower().IndexOf("jssfc_d5.aspx") > 0) || (this.Context.Request.Path.ToLower().IndexOf("jssfc_d6.aspx") > 0)) || ((this.Context.Request.Path.ToLower().IndexOf("jssfc_d7.aspx") > 0) || (this.Context.Request.Path.ToLower().IndexOf("jssfc_d8.aspx") > 0))) || (((this.Context.Request.Path.ToLower().IndexOf("jssfc_lm.aspx") > 0) || (this.Context.Request.Path.ToLower().IndexOf("jssfc_zh.aspx") > 0)) || ((this.Context.Request.Path.ToLower().IndexOf("jsft2_3456.aspx") > 0) || (this.Context.Request.Path.ToLower().IndexOf("jsft2_78910.aspx") > 0)))))) || ((((((this.Context.Request.Path.ToLower().IndexOf("jsft2_dq.aspx") > 0) || (this.Context.Request.Path.ToLower().IndexOf("jsft2_lm.aspx") > 0)) || ((this.Context.Request.Path.ToLower().IndexOf("jsft2_zh.aspx") > 0) || (this.Context.Request.Path.ToLower().IndexOf("car168_3456.aspx") > 0))) || (((this.Context.Request.Path.ToLower().IndexOf("car168_78910.aspx") > 0) || (this.Context.Request.Path.ToLower().IndexOf("car168_dq.aspx") > 0)) || ((this.Context.Request.Path.ToLower().IndexOf("car168_lm.aspx") > 0) || (this.Context.Request.Path.ToLower().IndexOf("car168_zh.aspx") > 0)))) || ((((this.Context.Request.Path.ToLower().IndexOf("ssc168_d1.aspx") > 0) || (this.Context.Request.Path.ToLower().IndexOf("ssc168_d2.aspx") > 0)) || ((this.Context.Request.Path.ToLower().IndexOf("ssc168_d3.aspx") > 0) || (this.Context.Request.Path.ToLower().IndexOf("ssc168_d4.aspx") > 0))) || (((this.Context.Request.Path.ToLower().IndexOf("ssc168_dq.aspx") > 0) || (this.Context.Request.Path.ToLower().IndexOf("ssc168_lm.aspx") > 0)) || ((this.Context.Request.Path.ToLower().IndexOf("vrcar_3456.aspx") > 0) || (this.Context.Request.Path.ToLower().IndexOf("vrcar_78910.aspx") > 0))))) || (((((this.Context.Request.Path.ToLower().IndexOf("vrcar_dq.aspx") > 0) || (this.Context.Request.Path.ToLower().IndexOf("vrcar_lm.aspx") > 0)) || ((this.Context.Request.Path.ToLower().IndexOf("vrcar_zh.aspx") > 0) || (this.Context.Request.Path.ToLower().IndexOf("vrssc_d1.aspx") > 0))) || (((this.Context.Request.Path.ToLower().IndexOf("vrssc_d2.aspx") > 0) || (this.Context.Request.Path.ToLower().IndexOf("vrssc_d3.aspx") > 0)) || ((this.Context.Request.Path.ToLower().IndexOf("vrssc_d4.aspx") > 0) || (this.Context.Request.Path.ToLower().IndexOf("vrssc_dq.aspx") > 0)))) || ((((this.Context.Request.Path.ToLower().IndexOf("vrssc_lm.aspx") > 0) || (this.Context.Request.Path.ToLower().IndexOf("xyftoa_3456.aspx") > 0)) || ((this.Context.Request.Path.ToLower().IndexOf("xyftoa_78910.aspx") > 0) || (this.Context.Request.Path.ToLower().IndexOf("xyftoa_dq.aspx") > 0))) || (((this.Context.Request.Path.ToLower().IndexOf("xyftoa_lm.aspx") > 0) || (this.Context.Request.Path.ToLower().IndexOf("xyftoa_zh.aspx") > 0)) || ((this.Context.Request.Path.ToLower().IndexOf("xyftsg_3456.aspx") > 0) || (this.Context.Request.Path.ToLower().IndexOf("xyftsg_78910.aspx") > 0)))))))) || ((((this.Context.Request.Path.ToLower().IndexOf("xyftsg_dq.aspx") > 0) || (this.Context.Request.Path.ToLower().IndexOf("xyftsg_lm.aspx") > 0)) || ((this.Context.Request.Path.ToLower().IndexOf("xyftsg_zh.aspx") > 0) || (this.Context.Request.Path.ToLower().IndexOf("happycar_3456.aspx") > 0))) || (((this.Context.Request.Path.ToLower().IndexOf("happycar_78910.aspx") > 0) || (this.Context.Request.Path.ToLower().IndexOf("happycar_dq.aspx") > 0)) || ((this.Context.Request.Path.ToLower().IndexOf("happycar_lm.aspx") > 0) || (this.Context.Request.Path.ToLower().IndexOf("happycar_zh.aspx") > 0))))) || (this.Context.Request.Path.ToLower().IndexOf("six_") > 0)))
            {
                string str = LSRequest.qq("lid");
                base.Response.Redirect(string.Format("/m/Report_JeuWeek.aspx?r={0}", base.CodeValidateByModel()), true);
                base.Response.End();
            }
        }

        protected void UsePageCache()
        {
            base.Response.Cache.SetMaxAge(new TimeSpan(0, 10, 0));
            string str = base.Request.Headers["If-Modified-Since"];
            DateTime time = Convert.ToDateTime(str);
            TimeSpan span = (TimeSpan) (DateTime.Now - time);
            if (span.TotalSeconds < 7200.0)
            {
                base.Response.StatusCode = 0x130;
                base.Response.End();
            }
            else
            {
                DateTime now = DateTime.Now;
                base.Response.Cache.SetLastModified(now);
            }
        }
    }
}

