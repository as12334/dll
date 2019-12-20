namespace Agent.Web.m.Account
{
    using Agent.Web.WebBase;
    using LotterySystem.Common;
    using LotterySystem.Model;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Web.UI.HtmlControls;

    public class drawback : MemberPageBase_Mobile
    {
        private string cqsc_modify_playid = "";
        private cz_users cz_users_model;
        private Dictionary<string, Dictionary<string, string>> DICT = new Dictionary<string, Dictionary<string, string>>();
        protected HtmlForm form1;
        private bool isAllowUpdate_kc;
        private bool isAllowUpdate_six;
        private string jscar_modify_playid = "";
        private string jsk3_modify_playid = "";
        private string k8sc_modify_playid = "";
        private string kl10_modify_playid = "";
        private string kl8_modify_playid = "";
        private string lm_max_amount = "";
        private string lm_phase_amount = "";
        private string lm_pk_a = "";
        private string lm_pk_b = "";
        private string lm_pk_c = "";
        private string lm_single_min_amount = "";
        private string lmp_max_amount = "";
        private string lmp_phase_amount = "";
        private string lmp_pk_a = "";
        private string lmp_pk_b = "";
        private string lmp_pk_c = "";
        private string lmp_single_min_amount = "";
        private DataTable lotteryDT;
        protected string lottrty_kc = "";
        protected string lottrty_six = "";
        private string namestr = "";
        private string newAdd = "";
        private Dictionary<string, string> paramDict = new Dictionary<string, string>();
        private string pcdd_modify_playid = "";
        private string pk_kc = "";
        private string pk_kc_a = "";
        private string pk_kc_b = "";
        private string pk_kc_c = "";
        private string pk_six = "";
        private string pk_six_a = "";
        private string pk_six_b = "";
        private string pk_six_c = "";
        private string pk10_modify_playid = "";
        private string pkbjl_modify_playid = "";
        private cz_rate_kc rateKCModel;
        private string shortcut_blue = "display:none;";
        private string shortcut_cqsc_lmp = "2,3,16,17,18,19,20,21";
        private string shortcut_cqsc_tm = "1";
        private string shortcut_green = "display:none;";
        private string shortcut_jscar_lmp = "2,3,4,37,38,36";
        private string shortcut_jscar_tm = "1";
        private string shortcut_jsk3_lmp = "58,59,60,61,62,63,64";
        protected string shortcut_k8sc_lmp = "2,3,16,17,18,19,20,21";
        protected string shortcut_k8sc_tm = "1";
        private string shortcut_kl10_lm = "72,73,74,75,76,77,78,79";
        private string shortcut_kl10_lmp = "82,83,84,85,121,122,11,12,13,80";
        private string shortcut_kl10_tm = "81";
        private string shortcut_kl8_lmp = "65,66,67,68,69,70,71,72";
        protected string shortcut_pcdd_lm = "71014";
        protected string shortcut_pcdd_lmp = "71002,71003,71004,71005,71007,71008,71013";
        protected string shortcut_pcdd_tm = "71001,71006";
        private string shortcut_pk10_lmp = "2,3,4,37,38,36";
        private string shortcut_pk10_tm = "1";
        private string shortcut_pkbjl_lmp = "81001,81002,81003,81004";
        private string shortcut_speed5_lmp = "2,3,16,17,18,19,20,21";
        private string shortcut_speed5_tm = "1";
        private string shortcut_violet = "display:none;";
        private string shortcut_xyft5_lmp = "2,3,4,37,38,36";
        private string shortcut_xyft5_tm = "1";
        private string shortcut_xync_lm = "72,73,74,75,78,79";
        private string shortcut_xync_lmp = "82,83,84,85,121,122,11,12,13,80";
        private string shortcut_xync_tm = "81";
        private string six_lm_max_amount = "";
        private string six_lm_phase_amount = "";
        private string six_lm_pk_a = "";
        private string six_lm_pk_b = "";
        private string six_lm_pk_c = "";
        private string six_lm_single_min_amount = "";
        private string six_lmp_max_amount = "";
        private string six_lmp_phase_amount = "";
        private string six_lmp_pk_a = "";
        private string six_lmp_pk_b = "";
        private string six_lmp_pk_c = "";
        private string six_lmp_single_min_amount = "";
        private string six_modify_playid = "";
        private string six_shortcut_blue = "display:none;";
        private string six_shortcut_green = "display:none;";
        private string six_shortcut_lm = "91016,91017,91018,91019,91020,91030,91031,91032,91033,91034,91035,91036,91037,91040,91047,91048,91049,91050,91051,91058,91059";
        private string six_shortcut_lmp = "91003,91004,91005,91011,91012,91013,91014,91023,91024,91038,91041,91042,91043,91044,91045,91046";
        private string six_shortcut_tm = "91001,91002,91006,91007,91008,91057,91009,91010,91015,91021,91022,91025,91026,91027,91028,91029,91039,91052,91053,91054,91055,91056";
        private string six_shortcut_violet = "display:none;";
        private string six_tm_max_amount = "";
        private string six_tm_phase_amount = "";
        private string six_tm_pk_a = "";
        private string six_tm_pk_b = "";
        private string six_tm_pk_c = "";
        private string six_tm_single_min_amount = "";
        private string speed5_modify_playid = "";
        private string string_cqsc = "";
        private string string_jscar = "";
        private string string_jsk3 = "";
        private string string_k8sc = "";
        private string string_kl10 = "";
        private string string_kl8 = "";
        private string string_pcdd = "";
        private string string_pk10 = "";
        private string string_pkbjl = "";
        private string string_six = "";
        private string string_speed5 = "";
        private string string_xyft5 = "";
        private string string_xync = "";
        private DataTable table_cqsc;
        private DataTable table_jscar;
        private DataTable table_jsk3;
        private DataTable table_k8sc;
        private DataTable table_kl10;
        private DataTable table_kl8;
        private DataTable table_pcdd;
        private DataTable table_pk10;
        private DataTable table_pkbjl;
        private DataTable table_six;
        private DataTable table_speed5;
        private DataTable table_xyft5;
        private DataTable table_xync;
        protected string tabState_kc = "";
        protected string tabState_six = "";
        private string tm_max_amount = "";
        private string tm_phase_amount = "";
        private string tm_pk_a = "";
        private string tm_pk_b = "";
        private string tm_pk_c = "";
        private string tm_single_min_amount = "";
        private string userid = "";
        private agent_userinfo_session userModel;
        private string xyft5_modify_playid = "";
        private string xync_modify_playid = "";

        private void getDrawBack(ref string strResult)
        {
            bool flag2;
            bool flag3;
            base.checkLoginByHandler(0);
            ReturnResult result = new ReturnResult();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            new List<object>();
            new List<object>();
            string rateName = this.Session["user_name"].ToString();
            this.userModel = this.Session[rateName + "lottery_session_user_info"] as agent_userinfo_session;
            this.userid = LSRequest.qq("uid");
            this.newAdd = LSRequest.qq("isadd");
            string str2 = LSRequest.qq("submitType");
            if ((str2 != "view") && (str2 != "edit"))
            {
                base.Response.End();
            }
            this.newAdd = "0";
            LSRequest.qq("memberId");
            this.cz_users_model = CallBLL.cz_users_bll.GetUserInfoByUID(this.userid);
            if (!Utils.IsGuid(this.userid))
            {
                base.Response.End();
            }
            if (this.cz_users_model == null)
            {
                base.Response.End();
            }
            base.checkCloneRight();
            if (this.userModel.get_u_name().Equals(this.cz_users_model.get_u_name()))
            {
                base.Response.End();
            }
            if (!base.IsUnderLing(this.cz_users_model.get_u_name(), rateName, this.userModel.get_u_type().Trim()))
            {
                base.Response.End();
            }
            if (!base.IsUpperLowerLevels(this.cz_users_model.get_u_name(), this.userModel.get_u_type(), this.userModel.get_u_name()))
            {
                base.Response.End();
            }
            this.lotteryDT = base.GetLotteryList();
            DataTable table = this.lotteryDT.DefaultView.ToTable(true, new string[] { "master_id" });
            for (int i = 0; i < table.Rows.Count; i++)
            {
                if (Convert.ToInt32(table.Rows[i][0]).Equals(1))
                {
                    this.lottrty_six = table.Rows[i][0].ToString();
                }
                else if (Convert.ToInt32(table.Rows[i][0]).Equals(2))
                {
                    this.lottrty_kc = table.Rows[i][0].ToString();
                }
            }
            if (!string.IsNullOrEmpty(this.lottrty_six) && !string.IsNullOrEmpty(this.lottrty_kc))
            {
                this.tabState_six = "on";
            }
            else if (!string.IsNullOrEmpty(this.lottrty_six) && string.IsNullOrEmpty(this.lottrty_kc))
            {
                this.tabState_six = "on";
            }
            else if (string.IsNullOrEmpty(this.lottrty_six) && !string.IsNullOrEmpty(this.lottrty_kc))
            {
                this.tabState_kc = "on";
            }
            this.InitData();
            this.isAllowUpdate_six = this.IsAllowUpdate_six(this.cz_users_model);
            this.isAllowUpdate_kc = this.IsAllowUpdate_kc(this.cz_users_model);
            this.InitShortCut_SIX(this.cz_users_model.get_six_kind(), this.cz_users_model);
            this.InitShortCut_KC(this.cz_users_model.get_kc_kind(), this.cz_users_model);
            base.OpenLotteryMaster(out flag2, out flag3);
            switch (str2)
            {
                case "view":
                    if (flag2 && flag3)
                    {
                        string str3 = this.isAllowUpdate_six ? "0" : "1";
                        string str4 = this.isAllowUpdate_kc ? "0" : "1";
                        Dictionary<string, object> dictionary4 = new Dictionary<string, object>();
                        dictionary4.Add("disabled", str3);
                        dictionary4.Add("userKind", this.cz_users_model.get_six_kind());
                        dictionary4.Add("A", this.six_tm_pk_a);
                        dictionary4.Add("B", this.six_tm_pk_b);
                        dictionary4.Add("C", this.six_tm_pk_c);
                        dictionary4.Add("dzMaxAmount", this.six_tm_max_amount);
                        dictionary4.Add("dqMaxAmount", this.six_tm_phase_amount);
                        dictionary4.Add("dzMinAmount", this.six_tm_single_min_amount);
                        dictionary.Add("SixTM", dictionary4);
                        Dictionary<string, object> dictionary5 = new Dictionary<string, object>();
                        dictionary5.Add("disabled", str3);
                        dictionary5.Add("userKind", this.cz_users_model.get_six_kind());
                        dictionary5.Add("A", this.six_lmp_pk_a);
                        dictionary5.Add("B", this.six_lmp_pk_b);
                        dictionary5.Add("C", this.six_lmp_pk_c);
                        dictionary5.Add("dzMaxAmount", this.six_lmp_max_amount);
                        dictionary5.Add("dqMaxAmount", this.six_lmp_phase_amount);
                        dictionary5.Add("dzMinAmount", this.six_lmp_single_min_amount);
                        dictionary.Add("SixLMP", dictionary5);
                        Dictionary<string, object> dictionary6 = new Dictionary<string, object>();
                        dictionary6.Add("disabled", str3);
                        dictionary6.Add("userKind", this.cz_users_model.get_six_kind());
                        dictionary6.Add("A", this.six_lm_pk_a);
                        dictionary6.Add("B", this.six_lm_pk_b);
                        dictionary6.Add("C", this.six_lm_pk_c);
                        dictionary6.Add("dzMaxAmount", this.six_lm_max_amount);
                        dictionary6.Add("dqMaxAmount", this.six_lm_phase_amount);
                        dictionary6.Add("dzMinAmount", this.six_lm_single_min_amount);
                        dictionary.Add("SixLM", dictionary6);
                        if ((((this.string_kl10 == "y") || (this.string_cqsc == "y")) || ((this.string_pk10 == "y") || (this.string_xync == "y"))) || ((((this.string_k8sc == "y") || (this.string_pcdd == "y")) || ((this.string_xyft5 == "y") || (this.string_pkbjl == "y"))) || ((this.string_jscar == "y") || (this.string_speed5 == "y"))))
                        {
                            Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
                            dictionary2.Add("disabled", str4);
                            dictionary2.Add("userKind", this.cz_users_model.get_kc_kind());
                            dictionary2.Add("A", this.tm_pk_a);
                            dictionary2.Add("B", this.tm_pk_b);
                            dictionary2.Add("C", this.tm_pk_c);
                            dictionary2.Add("dzMaxAmount", this.tm_max_amount);
                            dictionary2.Add("dqMaxAmount", this.tm_phase_amount);
                            dictionary2.Add("dzMinAmount", this.tm_single_min_amount);
                            dictionary.Add("KcTM", dictionary2);
                        }
                        Dictionary<string, object> dictionary7 = new Dictionary<string, object>();
                        dictionary7.Add("disabled", str4);
                        dictionary7.Add("userKind", this.cz_users_model.get_kc_kind());
                        dictionary7.Add("A", this.lmp_pk_a);
                        dictionary7.Add("B", this.lmp_pk_b);
                        dictionary7.Add("C", this.lmp_pk_c);
                        dictionary7.Add("dzMaxAmount", this.lmp_max_amount);
                        dictionary7.Add("dqMaxAmount", this.lmp_phase_amount);
                        dictionary7.Add("dzMinAmount", this.lmp_single_min_amount);
                        dictionary.Add("KcLMP", dictionary7);
                        if (((this.string_kl10 == "y") || (this.string_xync == "y")) || (this.string_pcdd == "y"))
                        {
                            Dictionary<string, object> dictionary3 = new Dictionary<string, object>();
                            dictionary3.Add("disabled", str4);
                            dictionary3.Add("userKind", this.cz_users_model.get_kc_kind());
                            dictionary3.Add("A", this.lm_pk_a);
                            dictionary3.Add("B", this.lm_pk_b);
                            dictionary3.Add("C", this.lm_pk_c);
                            dictionary3.Add("dzMaxAmount", this.lm_max_amount);
                            dictionary3.Add("dqMaxAmount", this.lm_phase_amount);
                            dictionary3.Add("dzMinAmount", this.lm_single_min_amount);
                            dictionary.Add("KcLM", dictionary3);
                        }
                    }
                    else if (flag2)
                    {
                        string str5 = this.isAllowUpdate_six ? "0" : "1";
                        Dictionary<string, object> dictionary8 = new Dictionary<string, object>();
                        dictionary8.Add("disabled", str5);
                        dictionary8.Add("userKind", this.cz_users_model.get_six_kind());
                        dictionary8.Add("A", this.six_tm_pk_a);
                        dictionary8.Add("B", this.six_tm_pk_b);
                        dictionary8.Add("C", this.six_tm_pk_c);
                        dictionary8.Add("dzMaxAmount", this.six_tm_max_amount);
                        dictionary8.Add("dqMaxAmount", this.six_tm_phase_amount);
                        dictionary8.Add("dzMinAmount", this.six_tm_single_min_amount);
                        dictionary.Add("SixTM", dictionary8);
                        Dictionary<string, object> dictionary9 = new Dictionary<string, object>();
                        dictionary9.Add("disabled", str5);
                        dictionary9.Add("userKind", this.cz_users_model.get_six_kind());
                        dictionary9.Add("A", this.six_lmp_pk_a);
                        dictionary9.Add("B", this.six_lmp_pk_b);
                        dictionary9.Add("C", this.six_lmp_pk_c);
                        dictionary9.Add("dzMaxAmount", this.six_lmp_max_amount);
                        dictionary9.Add("dqMaxAmount", this.six_lmp_phase_amount);
                        dictionary9.Add("dzMinAmount", this.six_lmp_single_min_amount);
                        dictionary.Add("SixLMP", dictionary9);
                        Dictionary<string, object> dictionary10 = new Dictionary<string, object>();
                        dictionary10.Add("disabled", str5);
                        dictionary10.Add("userKind", this.cz_users_model.get_six_kind());
                        dictionary10.Add("A", this.six_lm_pk_a);
                        dictionary10.Add("B", this.six_lm_pk_b);
                        dictionary10.Add("C", this.six_lm_pk_c);
                        dictionary10.Add("dzMaxAmount", this.six_lm_max_amount);
                        dictionary10.Add("dqMaxAmount", this.six_lm_phase_amount);
                        dictionary10.Add("dzMinAmount", this.six_lm_single_min_amount);
                        dictionary.Add("SixLM", dictionary10);
                    }
                    else if (flag3)
                    {
                        string str6 = this.isAllowUpdate_kc ? "0" : "1";
                        if ((((this.string_kl10 == "y") || (this.string_cqsc == "y")) || ((this.string_pk10 == "y") || (this.string_xync == "y"))) || ((((this.string_k8sc == "y") || (this.string_pcdd == "y")) || ((this.string_xyft5 == "y") || (this.string_pkbjl == "y"))) || ((this.string_jscar == "y") || (this.string_speed5 == "y"))))
                        {
                            Dictionary<string, object> dictionary11 = new Dictionary<string, object>();
                            dictionary11.Add("disabled", str6);
                            dictionary11.Add("userKind", this.cz_users_model.get_kc_kind());
                            dictionary11.Add("A", this.tm_pk_a);
                            dictionary11.Add("B", this.tm_pk_b);
                            dictionary11.Add("C", this.tm_pk_c);
                            dictionary11.Add("dzMaxAmount", this.tm_max_amount);
                            dictionary11.Add("dqMaxAmount", this.tm_phase_amount);
                            dictionary11.Add("dzMinAmount", this.tm_single_min_amount);
                            dictionary.Add("KcTM", dictionary11);
                        }
                        Dictionary<string, object> dictionary13 = new Dictionary<string, object>();
                        dictionary13.Add("disabled", str6);
                        dictionary13.Add("userKind", this.cz_users_model.get_kc_kind());
                        dictionary13.Add("A", this.lmp_pk_a);
                        dictionary13.Add("B", this.lmp_pk_b);
                        dictionary13.Add("C", this.lmp_pk_c);
                        dictionary13.Add("dzMaxAmount", this.lmp_max_amount);
                        dictionary13.Add("dqMaxAmount", this.lmp_phase_amount);
                        dictionary13.Add("dzMinAmount", this.lmp_single_min_amount);
                        dictionary.Add("KcLMP", dictionary13);
                        if (((this.string_kl10 == "y") || (this.string_xync == "y")) || (this.string_pcdd == "y"))
                        {
                            Dictionary<string, object> dictionary12 = new Dictionary<string, object>();
                            dictionary12.Add("disabled", str6);
                            dictionary12.Add("userKind", this.cz_users_model.get_kc_kind());
                            dictionary12.Add("A", this.lm_pk_a);
                            dictionary12.Add("B", this.lm_pk_b);
                            dictionary12.Add("C", this.lm_pk_c);
                            dictionary12.Add("dzMaxAmount", this.lm_max_amount);
                            dictionary12.Add("dqMaxAmount", this.lm_phase_amount);
                            dictionary12.Add("dzMinAmount", this.lm_single_min_amount);
                            dictionary.Add("KcLM", dictionary12);
                        }
                    }
                    result.set_success(200);
                    dictionary.Add("name", this.cz_users_model.get_u_name());
                    result.set_data(dictionary);
                    strResult = base.ObjectToJson(result);
                    return;

                case "edit":
                {
                    this.rateKCModel = CallBLL.cz_rate_kc_bll.GetRateKCByUserName(this.cz_users_model.get_u_name());
                    base.En_User_Lock(this.rateKCModel.get_fgs_name());
                    string mess = "";
                    string str8 = "";
                    bool isSuccess = true;
                    bool flag5 = true;
                    this.PreDoParam(ref this.namestr);
                    this.GetModifyPlays(this.namestr);
                    if ((this.table_six != null) && !string.IsNullOrEmpty(this.six_modify_playid))
                    {
                        this.ValidSix();
                        this.UpdateSix(ref mess, ref isSuccess);
                        if (!this.newAdd.Equals("1"))
                        {
                            FileCacheHelper.UpdateDrawbackFile(100);
                        }
                    }
                    if (((((this.table_kl10 != null) || (this.table_cqsc != null)) || ((this.table_pk10 != null) || (this.table_xync != null))) || (((this.table_jsk3 != null) || (this.table_kl8 != null)) || ((this.table_k8sc != null) || (this.table_pcdd != null)))) || (((this.table_xyft5 != null) || (this.table_pkbjl != null)) || ((this.table_jscar != null) || (this.table_speed5 != null))))
                    {
                        if ((this.table_kl10 != null) && !string.IsNullOrEmpty(this.kl10_modify_playid))
                        {
                            this.ValidKC("kl10", 0.ToString(), this.table_kl10, this.kl10_modify_playid);
                        }
                        if ((this.table_cqsc != null) && !string.IsNullOrEmpty(this.cqsc_modify_playid))
                        {
                            this.ValidKC("cqsc", 1.ToString(), this.table_cqsc, this.cqsc_modify_playid);
                        }
                        if ((this.table_pk10 != null) && !string.IsNullOrEmpty(this.pk10_modify_playid))
                        {
                            this.ValidKC("pk10", 2.ToString(), this.table_pk10, this.pk10_modify_playid);
                        }
                        if ((this.table_xync != null) && !string.IsNullOrEmpty(this.xync_modify_playid))
                        {
                            this.ValidKC("xync", 3.ToString(), this.table_xync, this.xync_modify_playid);
                        }
                        if ((this.table_jsk3 != null) && !string.IsNullOrEmpty(this.jsk3_modify_playid))
                        {
                            this.ValidKC("jsk3", 4.ToString(), this.table_jsk3, this.jsk3_modify_playid);
                        }
                        if ((this.table_kl8 != null) && !string.IsNullOrEmpty(this.kl8_modify_playid))
                        {
                            this.ValidKC("kl8", 5.ToString(), this.table_kl8, this.kl8_modify_playid);
                        }
                        if ((this.table_k8sc != null) && !string.IsNullOrEmpty(this.k8sc_modify_playid))
                        {
                            this.ValidKC("k8sc", 6.ToString(), this.table_k8sc, this.k8sc_modify_playid);
                        }
                        if ((this.table_pcdd != null) && !string.IsNullOrEmpty(this.pcdd_modify_playid))
                        {
                            this.ValidKC("pcdd", 7.ToString(), this.table_pcdd, this.pcdd_modify_playid);
                        }
                        if ((this.table_xyft5 != null) && !string.IsNullOrEmpty(this.xyft5_modify_playid))
                        {
                            this.ValidKC("xyft5", 9.ToString(), this.table_xyft5, this.xyft5_modify_playid);
                        }
                        if ((this.table_pkbjl != null) && !string.IsNullOrEmpty(this.pkbjl_modify_playid))
                        {
                            this.ValidKC("pkbjl", 8.ToString(), this.table_pkbjl, this.pkbjl_modify_playid);
                        }
                        if ((this.table_jscar != null) && !string.IsNullOrEmpty(this.jscar_modify_playid))
                        {
                            this.ValidKC("jscar", 10.ToString(), this.table_jscar, this.jscar_modify_playid);
                        }
                        if ((this.table_speed5 != null) && !string.IsNullOrEmpty(this.cqsc_modify_playid))
                        {
                            this.ValidKC("speed5", 11.ToString(), this.table_speed5, this.speed5_modify_playid);
                        }
                        if ((((!string.IsNullOrEmpty(this.kl10_modify_playid) || !string.IsNullOrEmpty(this.cqsc_modify_playid)) || (!string.IsNullOrEmpty(this.pk10_modify_playid) || !string.IsNullOrEmpty(this.xync_modify_playid))) || ((!string.IsNullOrEmpty(this.jsk3_modify_playid) || !string.IsNullOrEmpty(this.kl8_modify_playid)) || (!string.IsNullOrEmpty(this.k8sc_modify_playid) || !string.IsNullOrEmpty(this.pcdd_modify_playid)))) || ((!string.IsNullOrEmpty(this.xyft5_modify_playid) || !string.IsNullOrEmpty(this.pkbjl_modify_playid)) || (!string.IsNullOrEmpty(this.jscar_modify_playid) || !string.IsNullOrEmpty(this.speed5_modify_playid))))
                        {
                            this.UpdateKC(ref str8, ref flag5);
                        }
                        if (!string.IsNullOrEmpty(this.kl10_modify_playid) && !this.newAdd.Equals("1"))
                        {
                            FileCacheHelper.UpdateDrawbackFile(0);
                        }
                        if (!string.IsNullOrEmpty(this.cqsc_modify_playid) && !this.newAdd.Equals("1"))
                        {
                            FileCacheHelper.UpdateDrawbackFile(1);
                        }
                        if (!string.IsNullOrEmpty(this.pk10_modify_playid) && !this.newAdd.Equals("1"))
                        {
                            FileCacheHelper.UpdateDrawbackFile(2);
                        }
                        if (!string.IsNullOrEmpty(this.xync_modify_playid) && !this.newAdd.Equals("1"))
                        {
                            FileCacheHelper.UpdateDrawbackFile(3);
                        }
                        if (!string.IsNullOrEmpty(this.jsk3_modify_playid) && !this.newAdd.Equals("1"))
                        {
                            FileCacheHelper.UpdateDrawbackFile(4);
                        }
                        if (!string.IsNullOrEmpty(this.kl8_modify_playid) && !this.newAdd.Equals("1"))
                        {
                            FileCacheHelper.UpdateDrawbackFile(5);
                        }
                        if (!string.IsNullOrEmpty(this.k8sc_modify_playid) && !this.newAdd.Equals("1"))
                        {
                            FileCacheHelper.UpdateDrawbackFile(6);
                        }
                        if (!string.IsNullOrEmpty(this.pcdd_modify_playid) && !this.newAdd.Equals("1"))
                        {
                            FileCacheHelper.UpdateDrawbackFile(7);
                        }
                        if (!string.IsNullOrEmpty(this.xyft5_modify_playid) && !this.newAdd.Equals("1"))
                        {
                            FileCacheHelper.UpdateDrawbackFile(9);
                        }
                        if (!string.IsNullOrEmpty(this.pkbjl_modify_playid) && !this.newAdd.Equals("1"))
                        {
                            FileCacheHelper.UpdateDrawbackFile(8);
                        }
                        if (!string.IsNullOrEmpty(this.jscar_modify_playid) && !this.newAdd.Equals("1"))
                        {
                            FileCacheHelper.UpdateDrawbackFile(10);
                        }
                        if (!string.IsNullOrEmpty(this.speed5_modify_playid) && !this.newAdd.Equals("1"))
                        {
                            FileCacheHelper.UpdateDrawbackFile(11);
                        }
                    }
                    base.Un_User_Lock(this.rateKCModel.get_fgs_name());
                    string str9 = "";
                    if (this.table_six != null)
                    {
                        str9 = str9 + mess;
                    }
                    if (((((this.table_kl10 != null) || (this.table_cqsc != null)) || ((this.table_pk10 != null) || (this.table_xync != null))) || (((this.table_jsk3 != null) || (this.table_kl8 != null)) || ((this.table_k8sc != null) || (this.table_pcdd != null)))) || (((this.table_xyft5 != null) || (this.table_pkbjl != null)) || ((this.table_jscar != null) || (this.table_speed5 != null))))
                    {
                        if (!string.IsNullOrEmpty(str9) && !string.IsNullOrEmpty(str8))
                        {
                            str9 = str9 + "\r\n" + str8;
                        }
                        else if (string.IsNullOrEmpty(str9) && !string.IsNullOrEmpty(str8))
                        {
                            str9 = str9 + str8;
                        }
                        else if (string.IsNullOrEmpty(str9) && string.IsNullOrEmpty(str8))
                        {
                            str9 = str9 + "無數據修改!";
                        }
                    }
                    if (isSuccess && flag5)
                    {
                        if (string.IsNullOrEmpty(this.newAdd))
                        {
                            this.newAdd = "0";
                        }
                        base.successOptMsg(str9);
                    }
                    else
                    {
                        base.noRightOptMsg(str9);
                    }
                    base.Response.End();
                    break;
                }
            }
        }

        private void GetModifyPlays(string nameStr)
        {
            if (!string.IsNullOrEmpty(nameStr))
            {
                this.six_modify_playid = "";
                this.kl10_modify_playid = "";
                this.cqsc_modify_playid = "";
                this.pk10_modify_playid = "";
                this.xync_modify_playid = "";
                this.jsk3_modify_playid = "";
                this.kl8_modify_playid = "";
                this.k8sc_modify_playid = "";
                this.pcdd_modify_playid = "";
                this.xyft5_modify_playid = "";
                this.pkbjl_modify_playid = "";
                this.jscar_modify_playid = "";
                this.speed5_modify_playid = "";
                string[] strArray = nameStr.Split(new char[] { ',' });
                for (int i = 0; i < strArray.Length; i++)
                {
                    string str = strArray[i].Split(new char[] { '_' })[0].ToLower();
                    string str2 = strArray[i].Split(new char[] { '_' })[1];
                    switch (str)
                    {
                        case "six":
                        {
                            if (!string.IsNullOrEmpty(this.six_modify_playid))
                            {
                                break;
                            }
                            this.six_modify_playid = this.six_modify_playid + str2;
                            continue;
                        }
                        case "kl10":
                        {
                            if (!string.IsNullOrEmpty(this.kl10_modify_playid))
                            {
                                goto Label_026D;
                            }
                            this.kl10_modify_playid = this.kl10_modify_playid + str2;
                            continue;
                        }
                        case "cqsc":
                        {
                            if (!string.IsNullOrEmpty(this.cqsc_modify_playid))
                            {
                                goto Label_02AD;
                            }
                            this.cqsc_modify_playid = this.cqsc_modify_playid + str2;
                            continue;
                        }
                        case "pk10":
                        {
                            if (!string.IsNullOrEmpty(this.pk10_modify_playid))
                            {
                                goto Label_02ED;
                            }
                            this.pk10_modify_playid = this.pk10_modify_playid + str2;
                            continue;
                        }
                        case "xync":
                        {
                            if (!string.IsNullOrEmpty(this.xync_modify_playid))
                            {
                                goto Label_032D;
                            }
                            this.xync_modify_playid = this.xync_modify_playid + str2;
                            continue;
                        }
                        case "jsk3":
                        {
                            if (!string.IsNullOrEmpty(this.jsk3_modify_playid))
                            {
                                goto Label_036D;
                            }
                            this.jsk3_modify_playid = this.jsk3_modify_playid + str2;
                            continue;
                        }
                        case "kl8":
                        {
                            if (!string.IsNullOrEmpty(this.kl8_modify_playid))
                            {
                                goto Label_03AD;
                            }
                            this.kl8_modify_playid = this.kl8_modify_playid + str2;
                            continue;
                        }
                        case "k8sc":
                        {
                            if (!string.IsNullOrEmpty(this.k8sc_modify_playid))
                            {
                                goto Label_03ED;
                            }
                            this.k8sc_modify_playid = this.k8sc_modify_playid + str2;
                            continue;
                        }
                        case "pcdd":
                        {
                            if (!string.IsNullOrEmpty(this.pcdd_modify_playid))
                            {
                                goto Label_042D;
                            }
                            this.pcdd_modify_playid = this.pcdd_modify_playid + str2;
                            continue;
                        }
                        case "xyft5":
                        {
                            if (!string.IsNullOrEmpty(this.xyft5_modify_playid))
                            {
                                goto Label_046D;
                            }
                            this.xyft5_modify_playid = this.xyft5_modify_playid + str2;
                            continue;
                        }
                        case "pkbjl":
                        {
                            if (!string.IsNullOrEmpty(this.pkbjl_modify_playid))
                            {
                                goto Label_04AD;
                            }
                            this.pkbjl_modify_playid = this.pkbjl_modify_playid + str2;
                            continue;
                        }
                        case "jscar":
                        {
                            if (!string.IsNullOrEmpty(this.jscar_modify_playid))
                            {
                                goto Label_04E7;
                            }
                            this.jscar_modify_playid = this.jscar_modify_playid + str2;
                            continue;
                        }
                        case "speed5":
                        {
                            if (!string.IsNullOrEmpty(this.speed5_modify_playid))
                            {
                                goto Label_0521;
                            }
                            this.speed5_modify_playid = this.speed5_modify_playid + str2;
                            continue;
                        }
                        default:
                        {
                            continue;
                        }
                    }
                    this.six_modify_playid = this.six_modify_playid + "," + str2;
                    continue;
                Label_026D:
                    this.kl10_modify_playid = this.kl10_modify_playid + "," + str2;
                    continue;
                Label_02AD:
                    this.cqsc_modify_playid = this.cqsc_modify_playid + "," + str2;
                    continue;
                Label_02ED:
                    this.pk10_modify_playid = this.pk10_modify_playid + "," + str2;
                    continue;
                Label_032D:
                    this.xync_modify_playid = this.xync_modify_playid + "," + str2;
                    continue;
                Label_036D:
                    this.jsk3_modify_playid = this.jsk3_modify_playid + "," + str2;
                    continue;
                Label_03AD:
                    this.kl8_modify_playid = this.kl8_modify_playid + "," + str2;
                    continue;
                Label_03ED:
                    this.k8sc_modify_playid = this.k8sc_modify_playid + "," + str2;
                    continue;
                Label_042D:
                    this.pcdd_modify_playid = this.pcdd_modify_playid + "," + str2;
                    continue;
                Label_046D:
                    this.xyft5_modify_playid = this.xyft5_modify_playid + "," + str2;
                    continue;
                Label_04AD:
                    this.pkbjl_modify_playid = this.pkbjl_modify_playid + "," + str2;
                    continue;
                Label_04E7:
                    this.jscar_modify_playid = this.jscar_modify_playid + "," + str2;
                    continue;
                Label_0521:
                    this.speed5_modify_playid = this.speed5_modify_playid + "," + str2;
                }
            }
        }

        protected Dictionary<string, string> GetUpDrawbackSix(DataTable table, string lottoryStr, string pk)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            foreach (DataRow row in table.Rows)
            {
                string str = row["play_id"].ToString().ToLower();
                if (string.IsNullOrEmpty(pk) || pk.Equals("0"))
                {
                    try
                    {
                        dictionary.Add(string.Format("{0}_a_{1}", lottoryStr, str), row["a_drawback"].ToString());
                        dictionary.Add(string.Format("{0}_b_{1}", lottoryStr, str), row["b_drawback"].ToString());
                        dictionary.Add(string.Format("{0}_c_{1}", lottoryStr, str), row["c_drawback"].ToString());
                    }
                    catch (Exception exception)
                    {
                        string message = exception.Message;
                    }
                }
                else
                {
                    string str2 = pk.ToLower();
                    if (str2 != null)
                    {
                        if (!(str2 == "a"))
                        {
                            if (str2 == "b")
                            {
                                goto Label_0126;
                            }
                            if (str2 == "c")
                            {
                                goto Label_014A;
                            }
                        }
                        else
                        {
                            dictionary.Add(string.Format("{0}_a_{1}", lottoryStr, str), row["a_drawback"].ToString());
                        }
                    }
                }
                goto Label_016C;
            Label_0126:
                dictionary.Add(string.Format("{0}_b_{1}", lottoryStr, str), row["b_drawback"].ToString());
                goto Label_016C;
            Label_014A:
                dictionary.Add(string.Format("{0}_c_{1}", lottoryStr, str), row["c_drawback"].ToString());
            Label_016C:
                dictionary.Add(string.Format("{0}_max_amount_{1}", lottoryStr, str), string.Format("{0:F0}", Convert.ToDouble(row["single_max_amount"].ToString())));
                dictionary.Add(string.Format("{0}_phase_amount_{1}", lottoryStr, str), string.Format("{0:F0}", Convert.ToDouble(row["single_phase_amount"].ToString())));
                dictionary.Add(string.Format("{0}_single_min_amount_{1}", lottoryStr, str), string.Format("{0:F0}", Convert.ToDouble(row["single_min_amount"].ToString())));
            }
            return dictionary;
        }

        private void InitData()
        {
            if (!string.IsNullOrEmpty(this.lottrty_six))
            {
                DataSet drawBackList = CallBLL.cz_drawback_six_bll.GetDrawBackList(this.cz_users_model.get_u_name());
                if (((drawBackList != null) && (drawBackList.Tables.Count > 0)) && (drawBackList.Tables[0].Rows.Count > 0))
                {
                    this.table_six = drawBackList.Tables[0];
                    DataTable table = CallBLL.cz_drawback_six_bll.GetDrawBackList(this.cz_users_model.get_sup_name()).Tables[0];
                    this.DICT.Add("six", this.GetUpDrawbackSix(table, "six", this.cz_users_model.get_six_kind()));
                }
            }
            for (int i = 0; i < this.lotteryDT.Rows.Count; i++)
            {
                switch (Convert.ToInt32(this.lotteryDT.Rows[i]["id"]))
                {
                    case 0:
                    {
                        DataSet drawbackByPlayId = CallBLL.cz_drawback_kl10_bll.GetDrawbackByPlayId(this.cz_users_model.get_u_name(), "86,87,88,89,90,91,92,93,94,95,96,97,98,99,100,101,102,103,104,105,106,107,108,109,110,111,112,113,114,115,116,117,118,119,120,123,124,125,126,127,128,129,130,131,132,133,134,135,136");
                        if (((drawbackByPlayId != null) && (drawbackByPlayId.Tables.Count > 0)) && (drawbackByPlayId.Tables[0].Rows.Count > 0))
                        {
                            this.table_kl10 = drawbackByPlayId.Tables[0];
                            DataTable table2 = CallBLL.cz_drawback_kl10_bll.GetDrawbackByPlayId(this.cz_users_model.get_sup_name(), "86,87,88,89,90,91,92,93,94,95,96,97,98,99,100,101,102,103,104,105,106,107,108,109,110,111,112,113,114,115,116,117,118,119,120,123,124,125,126,127,128,129,130,131,132,133,134,135,136").Tables[0];
                            this.DICT.Add("kl10", this.GetUpDrawbackSix(table2, "kl10", this.cz_users_model.get_kc_kind()));
                            this.string_kl10 = "y";
                        }
                        break;
                    }
                    case 1:
                    {
                        DataSet drawbackList = CallBLL.cz_drawback_cqsc_bll.GetDrawbackList(this.cz_users_model.get_u_name(), "4,5,6,7,8,9,10,11,12,13,14,15");
                        if (((drawbackList != null) && (drawbackList.Tables.Count > 0)) && (drawbackList.Tables[0].Rows.Count > 0))
                        {
                            this.table_cqsc = drawbackList.Tables[0];
                            DataTable table3 = CallBLL.cz_drawback_cqsc_bll.GetDrawbackList(this.cz_users_model.get_sup_name(), "4,5,6,7,8,9,10,11,12,13,14,15").Tables[0];
                            this.DICT.Add("cqsc", this.GetUpDrawbackSix(table3, "cqsc", this.cz_users_model.get_kc_kind()));
                            this.string_cqsc = "y";
                        }
                        break;
                    }
                    case 2:
                    {
                        DataSet set4 = CallBLL.cz_drawback_pk10_bll.GetDrawbackList(this.cz_users_model.get_u_name(), "1,2,3,4,36,37,38");
                        if (((set4 != null) && (set4.Tables.Count > 0)) && (set4.Tables[0].Rows.Count > 0))
                        {
                            this.table_pk10 = set4.Tables[0];
                            DataTable table4 = CallBLL.cz_drawback_pk10_bll.GetDrawbackList(this.cz_users_model.get_sup_name(), "1,2,3,4,36,37,38").Tables[0];
                            this.DICT.Add("pk10", this.GetUpDrawbackSix(table4, "pk10", this.cz_users_model.get_kc_kind()));
                            this.string_pk10 = "y";
                        }
                        break;
                    }
                    case 3:
                    {
                        DataSet set5 = CallBLL.cz_drawback_xync_bll.GetDrawbackList(this.cz_users_model.get_u_name(), "86,87,88,89,90,91,92,93,94,95,96,97,98,99,100,101,102,103,104,105,106,107,108,109,110,111,112,113,114,115,116,117,118,119,120,123,124,125,126,127,128,129,130,131,132,133,134,135,136");
                        if (((set5 != null) && (set5.Tables.Count > 0)) && (set5.Tables[0].Rows.Count > 0))
                        {
                            this.table_xync = set5.Tables[0];
                            DataTable table5 = CallBLL.cz_drawback_xync_bll.GetDrawbackList(this.cz_users_model.get_sup_name(), "86,87,88,89,90,91,92,93,94,95,96,97,98,99,100,101,102,103,104,105,106,107,108,109,110,111,112,113,114,115,116,117,118,119,120,123,124,125,126,127,128,129,130,131,132,133,134,135,136").Tables[0];
                            this.DICT.Add("xync", this.GetUpDrawbackSix(table5, "xync", this.cz_users_model.get_kc_kind()));
                            this.string_xync = "y";
                        }
                        break;
                    }
                    case 4:
                    {
                        DataSet set6 = CallBLL.cz_drawback_jsk3_bll.GetDrawbackList(this.cz_users_model.get_u_name());
                        if (((set6 != null) && (set6.Tables.Count > 0)) && (set6.Tables[0].Rows.Count > 0))
                        {
                            this.table_jsk3 = set6.Tables[0];
                            DataTable table6 = CallBLL.cz_drawback_jsk3_bll.GetDrawbackList(this.cz_users_model.get_sup_name()).Tables[0];
                            this.DICT.Add("jsk3", this.GetUpDrawbackSix(table6, "jsk3", this.cz_users_model.get_kc_kind()));
                            this.string_jsk3 = "y";
                        }
                        break;
                    }
                    case 5:
                    {
                        DataSet set7 = CallBLL.cz_drawback_kl8_bll.GetDrawbackList(this.cz_users_model.get_u_name());
                        if (((set7 != null) && (set7.Tables.Count > 0)) && (set7.Tables[0].Rows.Count > 0))
                        {
                            this.table_kl8 = set7.Tables[0];
                            DataTable table7 = CallBLL.cz_drawback_kl8_bll.GetDrawbackList(this.cz_users_model.get_sup_name()).Tables[0];
                            this.DICT.Add("kl8", this.GetUpDrawbackSix(table7, "kl8", this.cz_users_model.get_kc_kind()));
                            this.string_kl8 = "y";
                        }
                        break;
                    }
                    case 6:
                    {
                        DataSet set8 = CallBLL.cz_drawback_k8sc_bll.GetDrawbackList(this.cz_users_model.get_u_name(), "4,5,6,7,8,9,10,11,12,13,14,15");
                        if (((set8 != null) && (set8.Tables.Count > 0)) && (set8.Tables[0].Rows.Count > 0))
                        {
                            this.table_k8sc = set8.Tables[0];
                            DataTable table8 = CallBLL.cz_drawback_k8sc_bll.GetDrawbackList(this.cz_users_model.get_sup_name(), "4,5,6,7,8,9,10,11,12,13,14,15").Tables[0];
                            this.DICT.Add("k8sc", this.GetUpDrawbackSix(table8, "k8sc", this.cz_users_model.get_kc_kind()));
                            this.string_k8sc = "y";
                        }
                        break;
                    }
                    case 7:
                    {
                        DataSet set9 = CallBLL.cz_drawback_pcdd_bll.GetDrawbackList(this.cz_users_model.get_u_name(), "71009,71010,71011,71012");
                        if (((set9 != null) && (set9.Tables.Count > 0)) && (set9.Tables[0].Rows.Count > 0))
                        {
                            this.table_pcdd = set9.Tables[0];
                            DataTable table9 = CallBLL.cz_drawback_pcdd_bll.GetDrawbackList(this.cz_users_model.get_sup_name(), "71009,71010,71011,71012").Tables[0];
                            this.DICT.Add("pcdd", this.GetUpDrawbackSix(table9, "pcdd", this.cz_users_model.get_kc_kind()));
                            this.string_pcdd = "y";
                        }
                        break;
                    }
                    case 8:
                    {
                        DataSet set11 = CallBLL.cz_drawback_pkbjl_bll.GetDrawbackList(this.cz_users_model.get_u_name(), "81001,81002,81003,81004");
                        if (((set11 != null) && (set11.Tables.Count > 0)) && (set11.Tables[0].Rows.Count > 0))
                        {
                            this.table_pkbjl = set11.Tables[0];
                            DataTable table11 = CallBLL.cz_drawback_pkbjl_bll.GetDrawbackList(this.cz_users_model.get_sup_name(), "81001,81002,81003,81004").Tables[0];
                            this.DICT.Add("pkbjl", this.GetUpDrawbackSix(table11, "pkbjl", this.cz_users_model.get_kc_kind()));
                            this.string_pkbjl = "y";
                        }
                        break;
                    }
                    case 9:
                    {
                        DataSet set10 = CallBLL.cz_drawback_xyft5_bll.GetDrawbackList(this.cz_users_model.get_u_name(), "1,2,3,4,36,37,38");
                        if (((set10 != null) && (set10.Tables.Count > 0)) && (set10.Tables[0].Rows.Count > 0))
                        {
                            this.table_xyft5 = set10.Tables[0];
                            DataTable table10 = CallBLL.cz_drawback_xyft5_bll.GetDrawbackList(this.cz_users_model.get_sup_name(), "1,2,3,4,36,37,38").Tables[0];
                            this.DICT.Add("xyft5", this.GetUpDrawbackSix(table10, "xyft5", this.cz_users_model.get_kc_kind()));
                            this.string_xyft5 = "y";
                        }
                        break;
                    }
                    case 10:
                    {
                        DataSet set12 = CallBLL.cz_drawback_jscar_bll.GetDrawbackList(this.cz_users_model.get_u_name(), "1,2,3,4,36,37,38");
                        if (((set12 != null) && (set12.Tables.Count > 0)) && (set12.Tables[0].Rows.Count > 0))
                        {
                            this.table_jscar = set12.Tables[0];
                            DataTable table12 = CallBLL.cz_drawback_jscar_bll.GetDrawbackList(this.cz_users_model.get_sup_name(), "1,2,3,4,36,37,38").Tables[0];
                            this.DICT.Add("jscar", this.GetUpDrawbackSix(table12, "jscar", this.cz_users_model.get_kc_kind()));
                            this.string_jscar = "y";
                        }
                        break;
                    }
                    case 11:
                    {
                        DataSet set13 = CallBLL.cz_drawback_speed5_bll.GetDrawbackList(this.cz_users_model.get_u_name(), "4,5,6,7,8,9,10,11,12,13,14,15");
                        if (((set13 != null) && (set13.Tables.Count > 0)) && (set13.Tables[0].Rows.Count > 0))
                        {
                            this.table_speed5 = set13.Tables[0];
                            DataTable table13 = CallBLL.cz_drawback_speed5_bll.GetDrawbackList(this.cz_users_model.get_sup_name(), "4,5,6,7,8,9,10,11,12,13,14,15").Tables[0];
                            this.DICT.Add("speed5", this.GetUpDrawbackSix(table13, "speed5", this.cz_users_model.get_kc_kind()));
                            this.string_speed5 = "y";
                        }
                        break;
                    }
                }
            }
        }

        protected void InitShortCut_KC(string pk, cz_users cz_users_model)
        {
            DataTable lotteryList = base.GetLotteryList();
            for (int i = 0; i < lotteryList.Rows.Count; i++)
            {
                switch (Convert.ToInt32(lotteryList.Rows[i]["id"]))
                {
                    case 0:
                    {
                        DataSet drawbackByPlayId = CallBLL.cz_drawback_kl10_bll.GetDrawbackByPlayId(cz_users_model.get_u_name(), "86,87,88,89,90,91,92,93,94,95,96,97,98,99,100,101,102,103,104,105,106,107,108,109,110,111,112,113,114,115,116,117,118,119,120,123,124,125,126,127,128,129,130,131,132,133,134,135,136");
                        if (((drawbackByPlayId != null) && (drawbackByPlayId.Tables.Count > 0)) && (drawbackByPlayId.Tables[0].Rows.Count > 0))
                        {
                            this.table_kl10 = drawbackByPlayId.Tables[0];
                            this.string_kl10 = "y";
                        }
                        break;
                    }
                    case 1:
                    {
                        DataSet drawbackList = CallBLL.cz_drawback_cqsc_bll.GetDrawbackList(cz_users_model.get_u_name(), "4,5,6,7,8,9,10,11,12,13,14,15");
                        if (((drawbackList != null) && (drawbackList.Tables.Count > 0)) && (drawbackList.Tables[0].Rows.Count > 0))
                        {
                            this.table_cqsc = drawbackList.Tables[0];
                            this.string_cqsc = "y";
                        }
                        break;
                    }
                    case 2:
                    {
                        DataSet set3 = CallBLL.cz_drawback_pk10_bll.GetDrawbackList(cz_users_model.get_u_name(), "1,2,3,4,36,37,38");
                        if (((set3 != null) && (set3.Tables.Count > 0)) && (set3.Tables[0].Rows.Count > 0))
                        {
                            this.table_pk10 = set3.Tables[0];
                            this.string_pk10 = "y";
                        }
                        break;
                    }
                    case 3:
                    {
                        DataSet set4 = CallBLL.cz_drawback_xync_bll.GetDrawbackList(cz_users_model.get_u_name(), "86,87,88,89,90,91,92,93,94,95,96,97,98,99,100,101,102,103,104,105,106,107,108,109,110,111,112,113,114,115,116,117,118,119,120,123,124,125,126,127,128,129,130,131,132,133,134,135,136");
                        if (((set4 != null) && (set4.Tables.Count > 0)) && (set4.Tables[0].Rows.Count > 0))
                        {
                            this.table_xync = set4.Tables[0];
                            this.string_xync = "y";
                        }
                        break;
                    }
                    case 4:
                    {
                        DataSet set5 = CallBLL.cz_drawback_jsk3_bll.GetDrawbackList(cz_users_model.get_u_name());
                        if (((set5 != null) && (set5.Tables.Count > 0)) && (set5.Tables[0].Rows.Count > 0))
                        {
                            this.table_jsk3 = set5.Tables[0];
                            this.string_jsk3 = "y";
                        }
                        break;
                    }
                    case 5:
                    {
                        DataSet set6 = CallBLL.cz_drawback_kl8_bll.GetDrawbackList(cz_users_model.get_u_name());
                        if (((set6 != null) && (set6.Tables.Count > 0)) && (set6.Tables[0].Rows.Count > 0))
                        {
                            this.table_kl8 = set6.Tables[0];
                            this.string_kl8 = "y";
                        }
                        break;
                    }
                    case 6:
                    {
                        DataSet set7 = CallBLL.cz_drawback_k8sc_bll.GetDrawbackList(cz_users_model.get_u_name(), "4,5,6,7,8,9,10,11,12,13,14,15");
                        if (((set7 != null) && (set7.Tables.Count > 0)) && (set7.Tables[0].Rows.Count > 0))
                        {
                            this.table_k8sc = set7.Tables[0];
                            this.string_k8sc = "y";
                        }
                        break;
                    }
                    case 7:
                    {
                        DataSet set8 = CallBLL.cz_drawback_pcdd_bll.GetDrawbackList(cz_users_model.get_u_name(), "71009,71010,71011,71012");
                        if (((set8 != null) && (set8.Tables.Count > 0)) && (set8.Tables[0].Rows.Count > 0))
                        {
                            this.table_pcdd = set8.Tables[0];
                            this.string_pcdd = "y";
                        }
                        break;
                    }
                    case 8:
                    {
                        DataSet set10 = CallBLL.cz_drawback_pkbjl_bll.GetDrawbackList(cz_users_model.get_u_name(), "81001,81002,81003,81004");
                        if (((set10 != null) && (set10.Tables.Count > 0)) && (set10.Tables[0].Rows.Count > 0))
                        {
                            this.table_pkbjl = set10.Tables[0];
                            this.string_pkbjl = "y";
                        }
                        break;
                    }
                    case 9:
                    {
                        DataSet set9 = CallBLL.cz_drawback_xyft5_bll.GetDrawbackList(cz_users_model.get_u_name(), "1,2,3,4,36,37,38");
                        if (((set9 != null) && (set9.Tables.Count > 0)) && (set9.Tables[0].Rows.Count > 0))
                        {
                            this.table_xyft5 = set9.Tables[0];
                            this.string_xyft5 = "y";
                        }
                        break;
                    }
                    case 10:
                    {
                        DataSet set11 = CallBLL.cz_drawback_jscar_bll.GetDrawbackList(cz_users_model.get_u_name(), "1,2,3,4,36,37,38");
                        if (((set11 != null) && (set11.Tables.Count > 0)) && (set11.Tables[0].Rows.Count > 0))
                        {
                            this.table_jscar = set11.Tables[0];
                            this.string_jscar = "y";
                        }
                        break;
                    }
                    case 11:
                    {
                        DataSet set12 = CallBLL.cz_drawback_speed5_bll.GetDrawbackList(cz_users_model.get_u_name(), "4,5,6,7,8,9,10,11,12,13,14,15");
                        if (((set12 != null) && (set12.Tables.Count > 0)) && (set12.Tables[0].Rows.Count > 0))
                        {
                            this.table_speed5 = set12.Tables[0];
                            this.string_speed5 = "y";
                        }
                        break;
                    }
                }
            }
            DataRow row = null;
            DataRow row2 = null;
            DataRow row3 = null;
            if (this.table_kl10 != null)
            {
                row = this.table_kl10.Select(" play_id=81")[0];
                row2 = this.table_kl10.Select(" play_id=82")[0];
                row3 = this.table_kl10.Select(" play_id=72")[0];
            }
            else if (this.table_xync != null)
            {
                row = this.table_xync.Select(" play_id=81")[0];
                row2 = this.table_xync.Select(" play_id=82")[0];
                row3 = this.table_xync.Select(" play_id=72")[0];
            }
            else if (this.table_cqsc != null)
            {
                row = this.table_cqsc.Select(" play_id=1")[0];
                row2 = this.table_cqsc.Select(" play_id=2")[0];
            }
            else if (this.table_pk10 != null)
            {
                row = this.table_pk10.Select(" play_id=1")[0];
                row2 = this.table_pk10.Select(" play_id=2")[0];
            }
            else if (this.table_jsk3 != null)
            {
                row2 = this.table_jsk3.Select(" play_id=58")[0];
            }
            else if (this.table_kl8 != null)
            {
                row2 = this.table_kl8.Select(" play_id=65")[0];
            }
            else if (this.table_k8sc != null)
            {
                row = this.table_k8sc.Select(" play_id=1")[0];
                row2 = this.table_k8sc.Select(" play_id=2")[0];
            }
            else if (this.table_pcdd != null)
            {
                row = this.table_pcdd.Select(" play_id=71001")[0];
                row2 = this.table_pcdd.Select(" play_id=71002")[0];
                row3 = this.table_pcdd.Select(" play_id=71014")[0];
            }
            else if (this.table_xyft5 != null)
            {
                row = this.table_xyft5.Select(" play_id=1")[0];
                row2 = this.table_xyft5.Select(" play_id=2")[0];
            }
            else if (this.table_pkbjl != null)
            {
                row2 = this.table_pkbjl.Select(" play_id=81001")[0];
            }
            else if (this.table_jscar != null)
            {
                row = this.table_jscar.Select(" play_id=1")[0];
                row2 = this.table_jscar.Select(" play_id=2")[0];
            }
            else if (this.table_speed5 != null)
            {
                row = this.table_speed5.Select(" play_id=1")[0];
                row2 = this.table_speed5.Select(" play_id=2")[0];
            }
            if (row != null)
            {
                this.tm_pk_a = row["a_drawback"].ToString();
                this.tm_pk_b = row["b_drawback"].ToString();
                this.tm_pk_c = row["c_drawback"].ToString();
                this.tm_max_amount = row["single_max_amount"].ToString();
                this.tm_phase_amount = row["single_phase_amount"].ToString();
                this.tm_max_amount = string.Format("{0:F0}", Convert.ToDouble(this.tm_max_amount));
                this.tm_phase_amount = string.Format("{0:F0}", Convert.ToDouble(this.tm_phase_amount));
                this.tm_single_min_amount = string.Format("{0:F0}", Convert.ToDouble(row["single_min_amount"].ToString()));
            }
            if (row2 != null)
            {
                this.lmp_pk_a = row2["a_drawback"].ToString();
                this.lmp_pk_b = row2["b_drawback"].ToString();
                this.lmp_pk_c = row2["c_drawback"].ToString();
                this.lmp_max_amount = row2["single_max_amount"].ToString();
                this.lmp_phase_amount = row2["single_phase_amount"].ToString();
                this.lmp_max_amount = string.Format("{0:F0}", Convert.ToDouble(this.lmp_max_amount));
                this.lmp_phase_amount = string.Format("{0:F0}", Convert.ToDouble(this.lmp_phase_amount));
                this.lmp_single_min_amount = string.Format("{0:F0}", Convert.ToDouble(row2["single_min_amount"].ToString()));
            }
            if (row3 != null)
            {
                this.lm_pk_a = row3["a_drawback"].ToString();
                this.lm_pk_b = row3["b_drawback"].ToString();
                this.lm_pk_c = row3["c_drawback"].ToString();
                this.lm_max_amount = row3["single_max_amount"].ToString();
                this.lm_phase_amount = row3["single_phase_amount"].ToString();
                this.lm_max_amount = string.Format("{0:F0}", Convert.ToDouble(this.lm_max_amount));
                this.lm_phase_amount = string.Format("{0:F0}", Convert.ToDouble(this.lm_phase_amount));
                this.lm_single_min_amount = string.Format("{0:F0}", Convert.ToDouble(row3["single_min_amount"].ToString()));
            }
        }

        protected void InitShortCut_SIX(string pk, cz_users cz_users_model)
        {
            DataSet drawBackList = CallBLL.cz_drawback_six_bll.GetDrawBackList(cz_users_model.get_u_name());
            if (((drawBackList != null) && (drawBackList.Tables.Count > 0)) && (drawBackList.Tables[0].Rows.Count > 0))
            {
                this.table_six = drawBackList.Tables[0];
            }
            DataRow row = null;
            DataRow row2 = null;
            DataRow row3 = null;
            if (this.table_six != null)
            {
                row = this.table_six.Select(" play_id=91001")[0];
                row2 = this.table_six.Select(" play_id=91003")[0];
                row3 = this.table_six.Select(" play_id=91016")[0];
            }
            if (row != null)
            {
                this.six_tm_pk_a = row["a_drawback"].ToString();
                this.six_tm_pk_b = row["b_drawback"].ToString();
                this.six_tm_pk_c = row["c_drawback"].ToString();
                this.six_tm_max_amount = row["single_max_amount"].ToString();
                this.six_tm_phase_amount = row["single_phase_amount"].ToString();
                this.six_tm_max_amount = string.Format("{0:F0}", Convert.ToDouble(this.six_tm_max_amount));
                this.six_tm_phase_amount = string.Format("{0:F0}", Convert.ToDouble(this.six_tm_phase_amount));
                this.six_tm_single_min_amount = string.Format("{0:F0}", Convert.ToDouble(row["single_min_amount"].ToString()));
            }
            if (row2 != null)
            {
                this.six_lmp_pk_a = row2["a_drawback"].ToString();
                this.six_lmp_pk_b = row2["b_drawback"].ToString();
                this.six_lmp_pk_c = row2["c_drawback"].ToString();
                this.six_lmp_max_amount = row2["single_max_amount"].ToString();
                this.six_lmp_phase_amount = row2["single_phase_amount"].ToString();
                this.six_lmp_max_amount = string.Format("{0:F0}", Convert.ToDouble(this.six_lmp_max_amount));
                this.six_lmp_phase_amount = string.Format("{0:F0}", Convert.ToDouble(this.six_lmp_phase_amount));
                this.six_lmp_single_min_amount = string.Format("{0:F0}", Convert.ToDouble(row2["single_min_amount"].ToString()));
            }
            if (row3 != null)
            {
                this.six_lm_pk_a = row3["a_drawback"].ToString();
                this.six_lm_pk_b = row3["b_drawback"].ToString();
                this.six_lm_pk_c = row3["c_drawback"].ToString();
                this.six_lm_max_amount = row3["single_max_amount"].ToString();
                this.six_lm_phase_amount = row3["single_phase_amount"].ToString();
                this.six_lm_max_amount = string.Format("{0:F0}", Convert.ToDouble(this.six_lm_max_amount));
                this.six_lm_phase_amount = string.Format("{0:F0}", Convert.ToDouble(this.six_lm_phase_amount));
                this.six_lm_single_min_amount = string.Format("{0:F0}", Convert.ToDouble(row3["single_min_amount"].ToString()));
            }
        }

        private bool IsAllowUpdate_kc(cz_users cz_users_model)
        {
            bool flag = CallBLL.cz_bet_kc_bll.ExistsBetByUserName(cz_users_model.get_u_type(), cz_users_model.get_u_name());
            bool flag2 = false;
            if (flag && this.IsOpenKC())
            {
                flag2 = true;
            }
            return flag2;
        }

        private bool IsAllowUpdate_six(cz_users cz_users_model)
        {
            bool flag = CallBLL.cz_bet_six_bll.ExistsBetByUserName(cz_users_model.get_u_type(), cz_users_model.get_u_name());
            bool flag2 = CallBLL.cz_bet_six_bll.ExistsBetByUserName(cz_users_model.get_u_name());
            bool flag3 = CallBLL.cz_phase_six_bll.IsOpenPhase();
            bool flag4 = false;
            if ((flag && flag2) && flag3)
            {
                flag4 = true;
            }
            return flag4;
        }

        private bool IsOpenKC()
        {
            return base.IsOpenPhase_KC();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string str3;
            base.Response.Expires = 0;
            base.Response.CacheControl = "no-cache";
            string str = LSRequest.qq("action").Trim();
            string strResult = "";
            if (((str3 = str) != null) && (str3 == "getDrawBack"))
            {
                this.getDrawBack(ref strResult);
            }
            base.OutJson(strResult);
        }

        private void PreDoParam(ref string namestr)
        {
            Action<string> action = null;
            Action<string> action2 = null;
            Action<string> action3 = null;
            List<string> sixLMList = new List<string>();
            List<string> sixLMPList = new List<string>();
            List<string> sixTMList = new List<string>();
            List<string> second = new List<string>();
            List<string> list2 = new List<string>();
            List<string> list3 = new List<string>();
            if (!string.IsNullOrEmpty(LSRequest.qq("SixLM_dzMaxAmount")))
            {
                string str = LSRequest.qq("SixLM_A");
                string str2 = LSRequest.qq("SixLM_B");
                string str3 = LSRequest.qq("SixLM_C");
                string str4 = LSRequest.qq("SixLM_dzMaxAmount");
                string str5 = LSRequest.qq("SixLM_dqMaxAmount");
                string str6 = LSRequest.qq("SixLM_dzMinAmount");
                List<string> list4 = new List<string>(this.six_shortcut_lm.Split(new char[] { ',' }).ToList<string>());
                if (action == null)
                {
                    action = delegate (string x) {
                        x = "six_" + x;
                        sixLMList.Add(x);
                    };
                }
                list4.ForEach(action);
                foreach (string str7 in this.six_shortcut_lm.Split(new char[] { ',' }))
                {
                    this.paramDict.Add("six_a_" + str7, str);
                    this.paramDict.Add("six_b_" + str7, str2);
                    this.paramDict.Add("six_c_" + str7, str3);
                    this.paramDict.Add("six_max_amount_" + str7, str4);
                    this.paramDict.Add("six_phase_amount_" + str7, str5);
                    this.paramDict.Add("six_single_min_amount_" + str7, str6);
                }
            }
            if (!string.IsNullOrEmpty(LSRequest.qq("SixTM_dzMaxAmount")))
            {
                string str8 = LSRequest.qq("SixTM_A");
                string str9 = LSRequest.qq("SixTM_B");
                string str10 = LSRequest.qq("SixTM_C");
                string str11 = LSRequest.qq("SixTM_dzMaxAmount");
                string str12 = LSRequest.qq("SixTM_dqMaxAmount");
                string str13 = LSRequest.qq("SixTM_dzMinAmount");
                List<string> list5 = new List<string>(this.six_shortcut_tm.Split(new char[] { ',' }).ToList<string>());
                if (action2 == null)
                {
                    action2 = delegate (string x) {
                        x = "six_" + x;
                        sixTMList.Add(x);
                    };
                }
                list5.ForEach(action2);
                foreach (string str14 in this.six_shortcut_tm.Split(new char[] { ',' }))
                {
                    this.paramDict.Add("six_a_" + str14, str8);
                    this.paramDict.Add("six_b_" + str14, str9);
                    this.paramDict.Add("six_c_" + str14, str10);
                    this.paramDict.Add("six_max_amount_" + str14, str11);
                    this.paramDict.Add("six_phase_amount_" + str14, str12);
                    this.paramDict.Add("six_single_min_amount_" + str14, str13);
                }
            }
            if (!string.IsNullOrEmpty(LSRequest.qq("SixLMP_dzMaxAmount")))
            {
                string str15 = LSRequest.qq("SixLMP_A");
                string str16 = LSRequest.qq("SixLMP_B");
                string str17 = LSRequest.qq("SixLMP_C");
                string str18 = LSRequest.qq("SixLMP_dzMaxAmount");
                string str19 = LSRequest.qq("SixLMP_dqMaxAmount");
                string str20 = LSRequest.qq("SixLMP_dzMinAmount");
                List<string> list6 = new List<string>(this.six_shortcut_lmp.Split(new char[] { ',' }).ToList<string>());
                if (action3 == null)
                {
                    action3 = delegate (string x) {
                        x = "six_" + x;
                        sixLMPList.Add(x);
                    };
                }
                list6.ForEach(action3);
                foreach (string str21 in this.six_shortcut_lmp.Split(new char[] { ',' }))
                {
                    this.paramDict.Add("six_a_" + str21, str15);
                    this.paramDict.Add("six_b_" + str21, str16);
                    this.paramDict.Add("six_c_" + str21, str17);
                    this.paramDict.Add("six_max_amount_" + str21, str18);
                    this.paramDict.Add("six_phase_amount_" + str21, str19);
                    this.paramDict.Add("six_single_min_amount_" + str21, str20);
                }
            }
            if (!string.IsNullOrEmpty(LSRequest.qq("KcLM_dzMaxAmount")))
            {
                string str22 = LSRequest.qq("KcLM_A");
                string str23 = LSRequest.qq("KcLM_B");
                string str24 = LSRequest.qq("KcLM_C");
                string str25 = LSRequest.qq("KcLM_dzMaxAmount");
                string str26 = LSRequest.qq("KcLM_dqMaxAmount");
                string str27 = LSRequest.qq("KcLM_dzMinAmount");
                List<string> list7 = new List<string>(this.shortcut_kl10_lm.Split(new char[] { ',' }).ToList<string>());
                List<string> kl10Arr = new List<string>();
                list7.ForEach(delegate (string x) {
                    x = "kl10_" + x;
                    kl10Arr.Add(x);
                });
                foreach (string str28 in this.shortcut_kl10_lm.Split(new char[] { ',' }))
                {
                    this.paramDict.Add("kl10_a_" + str28, str22);
                    this.paramDict.Add("kl10_b_" + str28, str23);
                    this.paramDict.Add("kl10_c_" + str28, str24);
                    this.paramDict.Add("kl10_max_amount_" + str28, str25);
                    this.paramDict.Add("kl10_phase_amount_" + str28, str26);
                    this.paramDict.Add("kl10_single_min_amount_" + str28, str27);
                }
                List<string> list8 = new List<string>(this.shortcut_xync_lm.Split(new char[] { ',' }).ToList<string>());
                List<string> xyncArr = new List<string>();
                list8.ForEach(delegate (string x) {
                    x = "xync_" + x;
                    xyncArr.Add(x);
                });
                foreach (string str29 in this.shortcut_xync_lm.Split(new char[] { ',' }))
                {
                    this.paramDict.Add("xync_a_" + str29, str22);
                    this.paramDict.Add("xync_b_" + str29, str23);
                    this.paramDict.Add("xync_c_" + str29, str24);
                    this.paramDict.Add("xync_max_amount_" + str29, str25);
                    this.paramDict.Add("xync_phase_amount_" + str29, str26);
                    this.paramDict.Add("xync_single_min_amount_" + str29, str27);
                }
                List<string> list9 = new List<string>(this.shortcut_pcdd_lm.Split(new char[] { ',' }).ToList<string>());
                List<string> pcddArr = new List<string>();
                list9.ForEach(delegate (string x) {
                    x = "pcdd_" + x;
                    pcddArr.Add(x);
                });
                foreach (string str30 in this.shortcut_pcdd_lm.Split(new char[] { ',' }))
                {
                    this.paramDict.Add("pcdd_a_" + str30, str22);
                    this.paramDict.Add("pcdd_b_" + str30, str23);
                    this.paramDict.Add("pcdd_c_" + str30, str24);
                    this.paramDict.Add("pcdd_max_amount_" + str30, str25);
                    this.paramDict.Add("pcdd_phase_amount_" + str30, str26);
                    this.paramDict.Add("pcdd_single_min_amount_" + str30, str27);
                }
                second.AddRange(kl10Arr);
                second.AddRange(xyncArr);
                second.AddRange(pcddArr);
            }
            if (!string.IsNullOrEmpty(LSRequest.qq("KcTM_dzMaxAmount")))
            {
                string str31 = LSRequest.qq("KcTM_A");
                string str32 = LSRequest.qq("KcTM_B");
                string str33 = LSRequest.qq("KcTM_C");
                string str34 = LSRequest.qq("KcTM_dzMaxAmount");
                string str35 = LSRequest.qq("KcTM_dqMaxAmount");
                string str36 = LSRequest.qq("KcTM_dzMinAmount");
                list3 = new List<string> { "kl10_81", "xync_81", "cqsc_1", "pk10_1", "k8sc_1", "pcdd_71001" };
                this.paramDict.Add("kl10_a_81", str31);
                this.paramDict.Add("kl10_b_81", str32);
                this.paramDict.Add("kl10_c_81", str33);
                this.paramDict.Add("kl10_max_amount_81", str34);
                this.paramDict.Add("kl10_phase_amount_81", str35);
                this.paramDict.Add("kl10_single_min_amount_81", str36);
                this.paramDict.Add("xync_a_81", str31);
                this.paramDict.Add("xync_b_81", str32);
                this.paramDict.Add("xync_c_81", str33);
                this.paramDict.Add("xync_max_amount_81", str34);
                this.paramDict.Add("xync_phase_amount_81", str35);
                this.paramDict.Add("xync_single_min_amount_81", str36);
                this.paramDict.Add("cqsc_a_1", str31);
                this.paramDict.Add("cqsc_b_1", str32);
                this.paramDict.Add("cqsc_c_1", str33);
                this.paramDict.Add("cqsc_max_amount_1", str34);
                this.paramDict.Add("cqsc_phase_amount_1", str35);
                this.paramDict.Add("cqsc_single_min_amount_1", str36);
                this.paramDict.Add("pk10_a_1", str31);
                this.paramDict.Add("pk10_b_1", str32);
                this.paramDict.Add("pk10_c_1", str33);
                this.paramDict.Add("pk10_max_amount_1", str34);
                this.paramDict.Add("pk10_phase_amount_1", str35);
                this.paramDict.Add("pk10_single_min_amount_1", str36);
                this.paramDict.Add("k8sc_a_1", str31);
                this.paramDict.Add("k8sc_b_1", str32);
                this.paramDict.Add("k8sc_c_1", str33);
                this.paramDict.Add("k8sc_max_amount_1", str34);
                this.paramDict.Add("k8sc_phase_amount_1", str35);
                this.paramDict.Add("k8sc_single_min_amount_1", str36);
                this.paramDict.Add("pcdd_a_71001", str31);
                this.paramDict.Add("pcdd_b_71001", str32);
                this.paramDict.Add("pcdd_c_71001", str33);
                this.paramDict.Add("pcdd_max_amount_71001", str34);
                this.paramDict.Add("pcdd_phase_amount_71001", str35);
                this.paramDict.Add("pcdd_single_min_amount_71001", str36);
                this.paramDict.Add("xyft5_a_1", str31);
                this.paramDict.Add("xyft5_b_1", str32);
                this.paramDict.Add("xyft5_c_1", str33);
                this.paramDict.Add("xyft5_max_amount_1", str34);
                this.paramDict.Add("xyft5_phase_amount_1", str35);
                this.paramDict.Add("xyft5_single_min_amount_1", str36);
                this.paramDict.Add("jscar_a_1", str31);
                this.paramDict.Add("jscar_b_1", str32);
                this.paramDict.Add("jscar_c_1", str33);
                this.paramDict.Add("jscar_max_amount_1", str34);
                this.paramDict.Add("jscar_phase_amount_1", str35);
                this.paramDict.Add("jscar_single_min_amount_1", str36);
                this.paramDict.Add("speed5_a_1", str31);
                this.paramDict.Add("speed5_b_1", str32);
                this.paramDict.Add("speed5_c_1", str33);
                this.paramDict.Add("speed5_max_amount_1", str34);
                this.paramDict.Add("speed5_phase_amount_1", str35);
                this.paramDict.Add("speed5_single_min_amount_1", str36);
            }
            if (!string.IsNullOrEmpty(LSRequest.qq("KcLMP_dzMaxAmount")))
            {
                string str37 = LSRequest.qq("KcLMP_A");
                string str38 = LSRequest.qq("KcLMP_B");
                string str39 = LSRequest.qq("KcLMP_C");
                string str40 = LSRequest.qq("KcLMP_dzMaxAmount");
                string str41 = LSRequest.qq("KcLMP_dqMaxAmount");
                string str42 = LSRequest.qq("KcLMP_dzMinAmount");
                List<string> list11 = new List<string>(this.shortcut_kl10_lmp.Split(new char[] { ',' }).ToList<string>());
                List<string> kl10Arr = new List<string>();
                list11.ForEach(delegate (string x) {
                    x = "kl10_" + x;
                    kl10Arr.Add(x);
                });
                list2.AddRange(kl10Arr);
                foreach (string str43 in this.shortcut_kl10_lmp.Split(new char[] { ',' }))
                {
                    this.paramDict.Add("kl10_a_" + str43, str37);
                    this.paramDict.Add("kl10_b_" + str43, str38);
                    this.paramDict.Add("kl10_c_" + str43, str39);
                    this.paramDict.Add("kl10_max_amount_" + str43, str40);
                    this.paramDict.Add("kl10_phase_amount_" + str43, str41);
                    this.paramDict.Add("kl10_single_min_amount_" + str43, str42);
                }
                List<string> list12 = new List<string>(this.shortcut_cqsc_lmp.Split(new char[] { ',' }).ToList<string>());
                List<string> cqscArr = new List<string>();
                list12.ForEach(delegate (string x) {
                    x = "cqsc_" + x;
                    cqscArr.Add(x);
                });
                list2.AddRange(cqscArr);
                foreach (string str44 in this.shortcut_cqsc_lmp.Split(new char[] { ',' }))
                {
                    this.paramDict.Add("cqsc_a_" + str44, str37);
                    this.paramDict.Add("cqsc_b_" + str44, str38);
                    this.paramDict.Add("cqsc_c_" + str44, str39);
                    this.paramDict.Add("cqsc_max_amount_" + str44, str40);
                    this.paramDict.Add("cqsc_phase_amount_" + str44, str41);
                    this.paramDict.Add("cqsc_single_min_amount_" + str44, str42);
                }
                List<string> list13 = new List<string>(this.shortcut_k8sc_lmp.Split(new char[] { ',' }).ToList<string>());
                List<string> k8scArr = new List<string>();
                list13.ForEach(delegate (string x) {
                    x = "k8sc_" + x;
                    k8scArr.Add(x);
                });
                list2.AddRange(k8scArr);
                foreach (string str45 in this.shortcut_k8sc_lmp.Split(new char[] { ',' }))
                {
                    this.paramDict.Add("k8sc_a_" + str45, str37);
                    this.paramDict.Add("k8sc_b_" + str45, str38);
                    this.paramDict.Add("k8sc_c_" + str45, str39);
                    this.paramDict.Add("k8sc_max_amount_" + str45, str40);
                    this.paramDict.Add("k8sc_phase_amount_" + str45, str41);
                    this.paramDict.Add("k8sc_single_min_amount_" + str45, str42);
                }
                List<string> list14 = new List<string>(this.shortcut_pk10_lmp.Split(new char[] { ',' }).ToList<string>());
                List<string> pk10Arr = new List<string>();
                list14.ForEach(delegate (string x) {
                    x = "pk10_" + x;
                    pk10Arr.Add(x);
                });
                list2.AddRange(pk10Arr);
                foreach (string str46 in this.shortcut_pk10_lmp.Split(new char[] { ',' }))
                {
                    this.paramDict.Add("pk10_a_" + str46, str37);
                    this.paramDict.Add("pk10_b_" + str46, str38);
                    this.paramDict.Add("pk10_c_" + str46, str39);
                    this.paramDict.Add("pk10_max_amount_" + str46, str40);
                    this.paramDict.Add("pk10_phase_amount_" + str46, str41);
                    this.paramDict.Add("pk10_single_min_amount_" + str46, str42);
                }
                List<string> list15 = new List<string>(this.shortcut_xync_lmp.Split(new char[] { ',' }).ToList<string>());
                List<string> xyncArr = new List<string>();
                list15.ForEach(delegate (string x) {
                    x = "xync_" + x;
                    xyncArr.Add(x);
                });
                list2.AddRange(xyncArr);
                foreach (string str47 in this.shortcut_xync_lmp.Split(new char[] { ',' }))
                {
                    this.paramDict.Add("xync_a_" + str47, str37);
                    this.paramDict.Add("xync_b_" + str47, str38);
                    this.paramDict.Add("xync_c_" + str47, str39);
                    this.paramDict.Add("xync_max_amount_" + str47, str40);
                    this.paramDict.Add("xync_phase_amount_" + str47, str41);
                    this.paramDict.Add("xync_single_min_amount_" + str47, str42);
                }
                List<string> list16 = new List<string>(this.shortcut_jsk3_lmp.Split(new char[] { ',' }).ToList<string>());
                List<string> jsk3Arr = new List<string>();
                list16.ForEach(delegate (string x) {
                    x = "jsk3_" + x;
                    jsk3Arr.Add(x);
                });
                list2.AddRange(jsk3Arr);
                foreach (string str48 in this.shortcut_jsk3_lmp.Split(new char[] { ',' }))
                {
                    this.paramDict.Add("jsk3_a_" + str48, str37);
                    this.paramDict.Add("jsk3_b_" + str48, str38);
                    this.paramDict.Add("jsk3_c_" + str48, str39);
                    this.paramDict.Add("jsk3_max_amount_" + str48, str40);
                    this.paramDict.Add("jsk3_phase_amount_" + str48, str41);
                    this.paramDict.Add("jsk3_single_min_amount_" + str48, str42);
                }
                List<string> list17 = new List<string>(this.shortcut_kl8_lmp.Split(new char[] { ',' }).ToList<string>());
                List<string> kl8Arr = new List<string>();
                list17.ForEach(delegate (string x) {
                    x = "kl8_" + x;
                    kl8Arr.Add(x);
                });
                list2.AddRange(kl8Arr);
                foreach (string str49 in this.shortcut_kl8_lmp.Split(new char[] { ',' }))
                {
                    this.paramDict.Add("kl8_a_" + str49, str37);
                    this.paramDict.Add("kl8_b_" + str49, str38);
                    this.paramDict.Add("kl8_c_" + str49, str39);
                    this.paramDict.Add("kl8_max_amount_" + str49, str40);
                    this.paramDict.Add("kl8_phase_amount_" + str49, str41);
                    this.paramDict.Add("kl8_single_min_amount_" + str49, str42);
                }
                List<string> list18 = new List<string>(this.shortcut_pcdd_lmp.Split(new char[] { ',' }).ToList<string>());
                List<string> pcddArr = new List<string>();
                list18.ForEach(delegate (string x) {
                    x = "pcdd_" + x;
                    pcddArr.Add(x);
                });
                list2.AddRange(pcddArr);
                foreach (string str50 in this.shortcut_pcdd_lmp.Split(new char[] { ',' }))
                {
                    this.paramDict.Add("pcdd_a_" + str50, str37);
                    this.paramDict.Add("pcdd_b_" + str50, str38);
                    this.paramDict.Add("pcdd_c_" + str50, str39);
                    this.paramDict.Add("pcdd_max_amount_" + str50, str40);
                    this.paramDict.Add("pcdd_phase_amount_" + str50, str41);
                    this.paramDict.Add("pcdd_single_min_amount_" + str50, str42);
                }
                List<string> list19 = new List<string>(this.shortcut_xyft5_lmp.Split(new char[] { ',' }).ToList<string>());
                List<string> xyft5Arr = new List<string>();
                list19.ForEach(delegate (string x) {
                    x = "xyft5_" + x;
                    xyft5Arr.Add(x);
                });
                list2.AddRange(xyft5Arr);
                foreach (string str51 in this.shortcut_xyft5_lmp.Split(new char[] { ',' }))
                {
                    this.paramDict.Add("xyft5_a_" + str51, str37);
                    this.paramDict.Add("xyft5_b_" + str51, str38);
                    this.paramDict.Add("xyft5_c_" + str51, str39);
                    this.paramDict.Add("xyft5_max_amount_" + str51, str40);
                    this.paramDict.Add("xyft5_phase_amount_" + str51, str41);
                    this.paramDict.Add("xyft5_single_min_amount_" + str51, str42);
                }
                List<string> list20 = new List<string>(this.shortcut_pkbjl_lmp.Split(new char[] { ',' }).ToList<string>());
                List<string> pkbjlArr = new List<string>();
                list20.ForEach(delegate (string x) {
                    x = "pkbjl_" + x;
                    pkbjlArr.Add(x);
                });
                list2.AddRange(pkbjlArr);
                foreach (string str52 in this.shortcut_pkbjl_lmp.Split(new char[] { ',' }))
                {
                    this.paramDict.Add("pkbjl_a_" + str52, str37);
                    this.paramDict.Add("pkbjl_b_" + str52, str38);
                    this.paramDict.Add("pkbjl_c_" + str52, str39);
                    this.paramDict.Add("pkbjl_max_amount_" + str52, str40);
                    this.paramDict.Add("pkbjl_phase_amount_" + str52, str41);
                    this.paramDict.Add("pkbjl_single_min_amount_" + str52, str42);
                }
                List<string> list21 = new List<string>(this.shortcut_jscar_lmp.Split(new char[] { ',' }).ToList<string>());
                List<string> jscarArr = new List<string>();
                list21.ForEach(delegate (string x) {
                    x = "jscar_" + x;
                    jscarArr.Add(x);
                });
                list2.AddRange(jscarArr);
                foreach (string str53 in this.shortcut_jscar_lmp.Split(new char[] { ',' }))
                {
                    this.paramDict.Add("jscar_a_" + str53, str37);
                    this.paramDict.Add("jscar_b_" + str53, str38);
                    this.paramDict.Add("jscar_c_" + str53, str39);
                    this.paramDict.Add("jscar_max_amount_" + str53, str40);
                    this.paramDict.Add("jscar_phase_amount_" + str53, str41);
                    this.paramDict.Add("jscar_single_min_amount_" + str53, str42);
                }
                List<string> list22 = new List<string>(this.shortcut_speed5_lmp.Split(new char[] { ',' }).ToList<string>());
                List<string> speed5Arr = new List<string>();
                list22.ForEach(delegate (string x) {
                    x = "speed5_" + x;
                    speed5Arr.Add(x);
                });
                list2.AddRange(speed5Arr);
                foreach (string str54 in this.shortcut_speed5_lmp.Split(new char[] { ',' }))
                {
                    this.paramDict.Add("speed5_a_" + str54, str37);
                    this.paramDict.Add("speed5_b_" + str54, str38);
                    this.paramDict.Add("speed5_c_" + str54, str39);
                    this.paramDict.Add("speed5_max_amount_" + str54, str40);
                    this.paramDict.Add("speed5_phase_amount_" + str54, str41);
                    this.paramDict.Add("speed5_single_min_amount_" + str54, str42);
                }
            }
            IEnumerable<string> values = sixLMList.Concat<string>(sixLMPList).Concat<string>(sixTMList).Concat<string>(second).Concat<string>(list3).Concat<string>(list2);
            namestr = string.Join(",", values);
        }

        private void UpdateKC(ref string mess, ref bool isSuccess)
        {
            string str265;
            bool flag = CallBLL.cz_bet_kc_bll.ExistsBetByUserName(this.cz_users_model.get_u_type(), this.cz_users_model.get_u_name());
            bool flag2 = false;
            if (flag && this.IsOpenKC())
            {
                flag2 = true;
            }
            if ((this.table_kl10 != null) && !string.IsNullOrEmpty(this.kl10_modify_playid))
            {
                DataTable table = CallBLL.cz_drawback_kl10_bll.GetDrawbackByPlayIds(this.cz_users_model.get_sup_name(), this.kl10_modify_playid, "").Tables[0];
                DataTable oldDT = CallBLL.cz_drawback_kl10_bll.GetDrawbackByPlayIds(this.cz_users_model.get_u_name(), this.kl10_modify_playid, "").Tables[0];
                Dictionary<string, cz_drawback_kl10> dictionary = new Dictionary<string, cz_drawback_kl10>();
                List<drawback_downuser> list = new List<drawback_downuser>();
                List<drawback_downuser> list2 = new List<drawback_downuser>();
                foreach (DataRow row in table.Rows)
                {
                    string str = row["play_id"].ToString();
                    string s = row["a_drawback"].ToString();
                    string str3 = row["b_drawback"].ToString();
                    string str4 = row["c_drawback"].ToString();
                    string str5 = row["single_max_amount"].ToString();
                    string str6 = row["single_phase_amount"].ToString();
                    string str7 = row["single_min_amount"].ToString();
                    string str8 = this.paramDict[string.Format("kl10_a_{0}", str)];
                    string str9 = this.paramDict[string.Format("kl10_b_{0}", str)];
                    string str10 = this.paramDict[string.Format("kl10_c_{0}", str)];
                    string str11 = this.paramDict[string.Format("kl10_max_amount_{0}", str)];
                    string str12 = this.paramDict[string.Format("kl10_phase_amount_{0}", str)];
                    string str13 = this.paramDict[string.Format("kl10_single_min_amount_{0}", str.ToLower())];
                    DataRow[] rowArray = oldDT.Select(string.Format(" play_id={0} ", str));
                    string str14 = rowArray[0]["a_drawback"].ToString();
                    string str15 = rowArray[0]["b_drawback"].ToString();
                    string str16 = rowArray[0]["c_drawback"].ToString();
                    string str17 = rowArray[0]["single_max_amount"].ToString();
                    string str18 = rowArray[0]["single_phase_amount"].ToString();
                    string str19 = rowArray[0]["single_min_amount"].ToString();
                    if ((((double.Parse(str14) == double.Parse(str8)) && (double.Parse(str15) == double.Parse(str9))) && ((double.Parse(str16) == double.Parse(str10)) && (double.Parse(str17) == double.Parse(str11)))) && ((double.Parse(str18) == double.Parse(str12)) && (double.Parse(str19) == double.Parse(str13))))
                    {
                        continue;
                    }
                    if (string.IsNullOrEmpty(this.cz_users_model.get_kc_kind()) || this.cz_users_model.get_kc_kind().Equals("0"))
                    {
                        if (double.Parse(str8) > double.Parse(s))
                        {
                            str8 = s;
                        }
                        if (double.Parse(str9) > double.Parse(str3))
                        {
                            str9 = str3;
                        }
                        if (double.Parse(str10) > double.Parse(str4))
                        {
                            str10 = str4;
                        }
                    }
                    else
                    {
                        str265 = this.cz_users_model.get_kc_kind().ToLower();
                        if (str265 != null)
                        {
                            if (!(str265 == "a"))
                            {
                                if (str265 == "b")
                                {
                                    goto Label_03C1;
                                }
                                if (str265 == "c")
                                {
                                    goto Label_03D7;
                                }
                            }
                            else if (double.Parse(str8) > double.Parse(s))
                            {
                                str8 = s;
                            }
                        }
                    }
                    goto Label_03EB;
                Label_03C1:
                    if (double.Parse(str9) > double.Parse(str3))
                    {
                        str9 = str3;
                    }
                    goto Label_03EB;
                Label_03D7:
                    if (double.Parse(str10) > double.Parse(str4))
                    {
                        str10 = str4;
                    }
                Label_03EB:
                    if (double.Parse(str11) > double.Parse(str5))
                    {
                        str11 = str5;
                    }
                    if (double.Parse(str12) > double.Parse(str6))
                    {
                        str12 = str6;
                    }
                    if (double.Parse(str13) < double.Parse(str7))
                    {
                        str13 = str7;
                    }
                    if ((((double.Parse(str14) != double.Parse(str8)) || (double.Parse(str15) != double.Parse(str9))) || ((double.Parse(str16) != double.Parse(str10)) || (double.Parse(str17) != double.Parse(str11)))) || ((double.Parse(str18) != double.Parse(str12)) || (double.Parse(str19) != double.Parse(str13))))
                    {
                        cz_drawback_kl10 _kl = new cz_drawback_kl10();
                        _kl.set_play_id(new int?(Convert.ToInt32(str)));
                        if (!string.IsNullOrEmpty(str8))
                        {
                            _kl.set_a_drawback(new decimal?(Convert.ToDecimal(str8)));
                        }
                        if (!string.IsNullOrEmpty(str9))
                        {
                            _kl.set_b_drawback(new decimal?(Convert.ToDecimal(str9)));
                        }
                        if (!string.IsNullOrEmpty(str10))
                        {
                            _kl.set_c_drawback(new decimal?(Convert.ToDecimal(str10)));
                        }
                        _kl.set_single_max_amount(new decimal?(Convert.ToDecimal(str11)));
                        _kl.set_single_phase_amount(new decimal?(Convert.ToDecimal(str12)));
                        _kl.set_single_min_amount(new decimal?(Convert.ToDecimal(str13)));
                        _kl.set_u_name(this.cz_users_model.get_u_name());
                        string str20 = "0";
                        if (string.IsNullOrEmpty(this.cz_users_model.get_kc_kind()) || this.cz_users_model.get_kc_kind().Equals("0"))
                        {
                            str20 = "0";
                        }
                        else
                        {
                            str20 = this.cz_users_model.get_kc_kind();
                        }
                        string key = "";
                        switch (str)
                        {
                            case "81":
                                key = "81,86,91,96,101,106,111,116";
                                break;

                            case "82":
                                key = "82,87,92,97,102,107,112,117";
                                break;

                            case "83":
                                key = "83,88,93,98,103,108,113,118";
                                break;

                            case "84":
                                key = "84,89,94,99,104,109,114,119";
                                break;

                            case "85":
                                key = "85,90,95,100,105,110,115,120";
                                break;

                            case "121":
                                key = "121,123,125,127,129,131,133,135";
                                break;

                            case "122":
                                key = "122,124,126,128,130,132,134,136";
                                break;

                            default:
                                key = str;
                                break;
                        }
                        dictionary.Add(key, _kl);
                        if (!this.cz_users_model.get_u_type().Equals("hy"))
                        {
                            drawback_downuser _downuser = new drawback_downuser();
                            _downuser.set_updateUserName(this.cz_users_model.get_u_name());
                            _downuser.set_updateUserType(this.cz_users_model.get_u_type());
                            _downuser.set_play_id(key);
                            _downuser.set_opt("dq");
                            _downuser.set_douvalue(double.Parse(_kl.get_single_phase_amount().ToString()));
                            list.Add(_downuser);
                            drawback_downuser _downuser2 = new drawback_downuser();
                            _downuser2.set_updateUserName(this.cz_users_model.get_u_name());
                            _downuser2.set_updateUserType(this.cz_users_model.get_u_type());
                            _downuser2.set_play_id(key);
                            _downuser2.set_opt("dz");
                            _downuser2.set_douvalue(double.Parse(_kl.get_single_max_amount().ToString()));
                            list.Add(_downuser2);
                            if (str20.ToLower().Equals("a"))
                            {
                                drawback_downuser _downuser3 = new drawback_downuser();
                                _downuser3.set_updateUserName(this.cz_users_model.get_u_name());
                                _downuser3.set_updateUserType(this.cz_users_model.get_u_type());
                                _downuser3.set_play_id(key);
                                _downuser3.set_opt("a");
                                _downuser3.set_douvalue(double.Parse(_kl.get_a_drawback().ToString()));
                                list.Add(_downuser3);
                            }
                            else if (str20.ToLower().Equals("b"))
                            {
                                drawback_downuser _downuser4 = new drawback_downuser();
                                _downuser4.set_updateUserName(this.cz_users_model.get_u_name());
                                _downuser4.set_updateUserType(this.cz_users_model.get_u_type());
                                _downuser4.set_play_id(key);
                                _downuser4.set_opt("b");
                                _downuser4.set_douvalue(double.Parse(_kl.get_b_drawback().ToString()));
                                list.Add(_downuser4);
                            }
                            else if (str20.ToLower().Equals("c"))
                            {
                                drawback_downuser _downuser5 = new drawback_downuser();
                                _downuser5.set_updateUserName(this.cz_users_model.get_u_name());
                                _downuser5.set_updateUserType(this.cz_users_model.get_u_type());
                                _downuser5.set_play_id(key);
                                _downuser5.set_opt("c");
                                _downuser5.set_douvalue(double.Parse(_kl.get_c_drawback().ToString()));
                                list.Add(_downuser5);
                            }
                            else if (str20.Equals("0"))
                            {
                                drawback_downuser _downuser6 = new drawback_downuser();
                                _downuser6.set_updateUserName(this.cz_users_model.get_u_name());
                                _downuser6.set_updateUserType(this.cz_users_model.get_u_type());
                                _downuser6.set_play_id(key);
                                _downuser6.set_opt("a");
                                _downuser6.set_douvalue(double.Parse(_kl.get_a_drawback().ToString()));
                                list.Add(_downuser6);
                                drawback_downuser _downuser7 = new drawback_downuser();
                                _downuser7.set_updateUserName(this.cz_users_model.get_u_name());
                                _downuser7.set_updateUserType(this.cz_users_model.get_u_type());
                                _downuser7.set_play_id(key);
                                _downuser7.set_opt("b");
                                _downuser7.set_douvalue(double.Parse(_kl.get_b_drawback().ToString()));
                                list.Add(_downuser7);
                                drawback_downuser _downuser8 = new drawback_downuser();
                                _downuser8.set_updateUserName(this.cz_users_model.get_u_name());
                                _downuser8.set_updateUserType(this.cz_users_model.get_u_type());
                                _downuser8.set_play_id(key);
                                _downuser8.set_opt("c");
                                _downuser8.set_douvalue(double.Parse(_kl.get_c_drawback().ToString()));
                                list.Add(_downuser8);
                            }
                        }
                        drawback_downuser item = new drawback_downuser();
                        item.set_updateUserName(this.cz_users_model.get_u_name());
                        item.set_updateUserType(this.cz_users_model.get_u_type());
                        item.set_play_id(key);
                        item.set_opt("mindz");
                        item.set_douvalue(double.Parse(_kl.get_single_min_amount().ToString()));
                        list2.Add(item);
                    }
                }
                string str22 = "0";
                if (string.IsNullOrEmpty(this.cz_users_model.get_kc_kind()) || this.cz_users_model.get_kc_kind().Equals("0"))
                {
                    str22 = "0";
                }
                else
                {
                    str22 = this.cz_users_model.get_kc_kind();
                }
                if (!CallBLL.cz_drawback_kl10_bll.UpdateDataList(dictionary, str22, flag2, list, list2))
                {
                    mess = string.Format("快彩：修改【{0}】退水出現異常，請重試!", LSKeys.get_KL10_Name());
                    isSuccess = false;
                    return;
                }
                base.user_drawback_kc_log(oldDT, CallBLL.cz_drawback_kl10_bll.GetDrawbackByPlayIds(this.cz_users_model.get_u_name(), this.kl10_modify_playid, "").Tables[0], this.cz_users_model.get_u_name(), 0);
            }
            if ((this.table_cqsc != null) && !string.IsNullOrEmpty(this.cqsc_modify_playid))
            {
                DataTable table3 = CallBLL.cz_drawback_cqsc_bll.GetDrawbackLists(this.cz_users_model.get_sup_name(), this.cqsc_modify_playid).Tables[0];
                DataTable table4 = CallBLL.cz_drawback_cqsc_bll.GetDrawbackLists(this.cz_users_model.get_u_name(), this.cqsc_modify_playid).Tables[0];
                Dictionary<string, cz_drawback_cqsc> dictionary2 = new Dictionary<string, cz_drawback_cqsc>();
                List<drawback_downuser> list3 = new List<drawback_downuser>();
                List<drawback_downuser> list4 = new List<drawback_downuser>();
                foreach (DataRow row2 in table3.Rows)
                {
                    string str23 = row2["play_id"].ToString();
                    string str24 = row2["a_drawback"].ToString();
                    string str25 = row2["b_drawback"].ToString();
                    string str26 = row2["c_drawback"].ToString();
                    string str27 = row2["single_max_amount"].ToString();
                    string str28 = row2["single_phase_amount"].ToString();
                    string str29 = row2["single_min_amount"].ToString();
                    string str30 = this.paramDict[string.Format("cqsc_a_{0}", str23)];
                    string str31 = this.paramDict[string.Format("cqsc_b_{0}", str23)];
                    string str32 = this.paramDict[string.Format("cqsc_c_{0}", str23)];
                    string str33 = this.paramDict[string.Format("cqsc_max_amount_{0}", str23)];
                    string str34 = this.paramDict[string.Format("cqsc_phase_amount_{0}", str23)];
                    string str35 = this.paramDict[string.Format("cqsc_single_min_amount_{0}", str23.ToLower())];
                    DataRow[] rowArray2 = table4.Select(string.Format(" play_id={0} ", str23));
                    string str36 = rowArray2[0]["a_drawback"].ToString();
                    string str37 = rowArray2[0]["b_drawback"].ToString();
                    string str38 = rowArray2[0]["c_drawback"].ToString();
                    string str39 = rowArray2[0]["single_max_amount"].ToString();
                    string str40 = rowArray2[0]["single_phase_amount"].ToString();
                    string str41 = rowArray2[0]["single_min_amount"].ToString();
                    if ((((double.Parse(str36) == double.Parse(str30)) && (double.Parse(str37) == double.Parse(str31))) && ((double.Parse(str38) == double.Parse(str32)) && (double.Parse(str39) == double.Parse(str33)))) && ((double.Parse(str40) == double.Parse(str34)) && (double.Parse(str41) == double.Parse(str35))))
                    {
                        continue;
                    }
                    if (string.IsNullOrEmpty(this.cz_users_model.get_kc_kind()) || this.cz_users_model.get_kc_kind().Equals("0"))
                    {
                        if (double.Parse(str30) > double.Parse(str24))
                        {
                            str30 = str24;
                        }
                        if (double.Parse(str31) > double.Parse(str25))
                        {
                            str31 = str25;
                        }
                        if (double.Parse(str32) > double.Parse(str26))
                        {
                            str32 = str26;
                        }
                    }
                    else
                    {
                        str265 = this.cz_users_model.get_kc_kind().ToLower();
                        if (str265 != null)
                        {
                            if (!(str265 == "a"))
                            {
                                if (str265 == "b")
                                {
                                    goto Label_0F56;
                                }
                                if (str265 == "c")
                                {
                                    goto Label_0F6C;
                                }
                            }
                            else if (double.Parse(str30) > double.Parse(str24))
                            {
                                str30 = str24;
                            }
                        }
                    }
                    goto Label_0F80;
                Label_0F56:
                    if (double.Parse(str31) > double.Parse(str25))
                    {
                        str31 = str25;
                    }
                    goto Label_0F80;
                Label_0F6C:
                    if (double.Parse(str32) > double.Parse(str26))
                    {
                        str32 = str26;
                    }
                Label_0F80:
                    if (double.Parse(str33) > double.Parse(str27))
                    {
                        str33 = str27;
                    }
                    if (double.Parse(str34) > double.Parse(str28))
                    {
                        str34 = str28;
                    }
                    if (double.Parse(str35) < double.Parse(str29))
                    {
                        str35 = str29;
                    }
                    if ((((double.Parse(str36) == double.Parse(str30)) && (double.Parse(str37) == double.Parse(str31))) && ((double.Parse(str38) == double.Parse(str32)) && (double.Parse(str39) == double.Parse(str33)))) && ((double.Parse(str40) == double.Parse(str34)) && (double.Parse(str41) == double.Parse(str35))))
                    {
                        continue;
                    }
                    cz_drawback_cqsc _cqsc = new cz_drawback_cqsc();
                    _cqsc.set_play_id(new int?(Convert.ToInt32(str23)));
                    if (!string.IsNullOrEmpty(str30))
                    {
                        _cqsc.set_a_drawback(new decimal?(Convert.ToDecimal(str30)));
                    }
                    if (!string.IsNullOrEmpty(str31))
                    {
                        _cqsc.set_b_drawback(new decimal?(Convert.ToDecimal(str31)));
                    }
                    if (!string.IsNullOrEmpty(str32))
                    {
                        _cqsc.set_c_drawback(new decimal?(Convert.ToDecimal(str32)));
                    }
                    _cqsc.set_single_max_amount(new decimal?(Convert.ToDecimal(str33)));
                    _cqsc.set_single_phase_amount(new decimal?(Convert.ToDecimal(str34)));
                    _cqsc.set_single_min_amount(new decimal?(Convert.ToDecimal(str35)));
                    _cqsc.set_u_name(this.cz_users_model.get_u_name());
                    string str42 = "0";
                    if (string.IsNullOrEmpty(this.cz_users_model.get_kc_kind()) || this.cz_users_model.get_kc_kind().Equals("0"))
                    {
                        str42 = "0";
                    }
                    else
                    {
                        str42 = this.cz_users_model.get_kc_kind();
                    }
                    string str43 = "";
                    str265 = str23;
                    if (str265 == null)
                    {
                        goto Label_117B;
                    }
                    if (!(str265 == "1"))
                    {
                        if (str265 == "2")
                        {
                            goto Label_1169;
                        }
                        if (str265 == "3")
                        {
                            goto Label_1172;
                        }
                        goto Label_117B;
                    }
                    str43 = "1,4,7,10,13";
                    goto Label_117F;
                Label_1169:
                    str43 = "2,5,8,11,14";
                    goto Label_117F;
                Label_1172:
                    str43 = "3,6,9,12,15";
                    goto Label_117F;
                Label_117B:
                    str43 = str23;
                Label_117F:
                    dictionary2.Add(str43, _cqsc);
                    if (!this.cz_users_model.get_u_type().Equals("hy"))
                    {
                        drawback_downuser _downuser10 = new drawback_downuser();
                        _downuser10.set_updateUserName(this.cz_users_model.get_u_name());
                        _downuser10.set_updateUserType(this.cz_users_model.get_u_type());
                        _downuser10.set_play_id(str43);
                        _downuser10.set_opt("dq");
                        _downuser10.set_douvalue(double.Parse(_cqsc.get_single_phase_amount().ToString()));
                        list3.Add(_downuser10);
                        drawback_downuser _downuser11 = new drawback_downuser();
                        _downuser11.set_updateUserName(this.cz_users_model.get_u_name());
                        _downuser11.set_updateUserType(this.cz_users_model.get_u_type());
                        _downuser11.set_play_id(str43);
                        _downuser11.set_opt("dz");
                        _downuser11.set_douvalue(double.Parse(_cqsc.get_single_max_amount().ToString()));
                        list3.Add(_downuser11);
                        if (str42.ToLower().Equals("a"))
                        {
                            drawback_downuser _downuser12 = new drawback_downuser();
                            _downuser12.set_updateUserName(this.cz_users_model.get_u_name());
                            _downuser12.set_updateUserType(this.cz_users_model.get_u_type());
                            _downuser12.set_play_id(str43);
                            _downuser12.set_opt("a");
                            _downuser12.set_douvalue(double.Parse(_cqsc.get_a_drawback().ToString()));
                            list3.Add(_downuser12);
                        }
                        else if (str42.ToLower().Equals("b"))
                        {
                            drawback_downuser _downuser13 = new drawback_downuser();
                            _downuser13.set_updateUserName(this.cz_users_model.get_u_name());
                            _downuser13.set_updateUserType(this.cz_users_model.get_u_type());
                            _downuser13.set_play_id(str43);
                            _downuser13.set_opt("b");
                            _downuser13.set_douvalue(double.Parse(_cqsc.get_b_drawback().ToString()));
                            list3.Add(_downuser13);
                        }
                        else if (str42.ToLower().Equals("c"))
                        {
                            drawback_downuser _downuser14 = new drawback_downuser();
                            _downuser14.set_updateUserName(this.cz_users_model.get_u_name());
                            _downuser14.set_updateUserType(this.cz_users_model.get_u_type());
                            _downuser14.set_play_id(str43);
                            _downuser14.set_opt("c");
                            _downuser14.set_douvalue(double.Parse(_cqsc.get_c_drawback().ToString()));
                            list3.Add(_downuser14);
                        }
                        else if (str42.Equals("0"))
                        {
                            drawback_downuser _downuser15 = new drawback_downuser();
                            _downuser15.set_updateUserName(this.cz_users_model.get_u_name());
                            _downuser15.set_updateUserType(this.cz_users_model.get_u_type());
                            _downuser15.set_play_id(str43);
                            _downuser15.set_opt("a");
                            _downuser15.set_douvalue(double.Parse(_cqsc.get_a_drawback().ToString()));
                            list3.Add(_downuser15);
                            drawback_downuser _downuser16 = new drawback_downuser();
                            _downuser16.set_updateUserName(this.cz_users_model.get_u_name());
                            _downuser16.set_updateUserType(this.cz_users_model.get_u_type());
                            _downuser16.set_play_id(str43);
                            _downuser16.set_opt("b");
                            _downuser16.set_douvalue(double.Parse(_cqsc.get_b_drawback().ToString()));
                            list3.Add(_downuser16);
                            drawback_downuser _downuser17 = new drawback_downuser();
                            _downuser17.set_updateUserName(this.cz_users_model.get_u_name());
                            _downuser17.set_updateUserType(this.cz_users_model.get_u_type());
                            _downuser17.set_play_id(str43);
                            _downuser17.set_opt("c");
                            _downuser17.set_douvalue(double.Parse(_cqsc.get_c_drawback().ToString()));
                            list3.Add(_downuser17);
                        }
                    }
                    drawback_downuser _downuser18 = new drawback_downuser();
                    _downuser18.set_updateUserName(this.cz_users_model.get_u_name());
                    _downuser18.set_updateUserType(this.cz_users_model.get_u_type());
                    _downuser18.set_play_id(str43);
                    _downuser18.set_opt("mindz");
                    _downuser18.set_douvalue(double.Parse(_cqsc.get_single_min_amount().ToString()));
                    list4.Add(_downuser18);
                }
                string str44 = "0";
                if (string.IsNullOrEmpty(this.cz_users_model.get_kc_kind()) || this.cz_users_model.get_kc_kind().Equals("0"))
                {
                    str44 = "0";
                }
                else
                {
                    str44 = this.cz_users_model.get_kc_kind();
                }
                if (!CallBLL.cz_drawback_cqsc_bll.UpdateDataList(dictionary2, str44, flag2, list3, list4))
                {
                    mess = string.Format("快彩：修改【{0}】退水出現異常，請重試!", LSKeys.get_CQSC_Name());
                    isSuccess = false;
                    return;
                }
                base.user_drawback_kc_log(table4, CallBLL.cz_drawback_cqsc_bll.GetDrawbackLists(this.cz_users_model.get_u_name(), this.cqsc_modify_playid).Tables[0], this.cz_users_model.get_u_name(), 1);
            }
            if ((this.table_pk10 != null) && !string.IsNullOrEmpty(this.pk10_modify_playid))
            {
                DataTable table5 = CallBLL.cz_drawback_pk10_bll.GetDrawbackList(this.cz_users_model.get_sup_name(), this.pk10_modify_playid).Tables[0];
                DataTable table6 = CallBLL.cz_drawback_pk10_bll.GetDrawbackList(this.cz_users_model.get_u_name(), this.pk10_modify_playid).Tables[0];
                Dictionary<string, cz_drawback_pk10> dictionary3 = new Dictionary<string, cz_drawback_pk10>();
                List<drawback_downuser> list5 = new List<drawback_downuser>();
                List<drawback_downuser> list6 = new List<drawback_downuser>();
                foreach (DataRow row3 in table5.Rows)
                {
                    string str45 = row3["play_id"].ToString();
                    string str46 = row3["a_drawback"].ToString();
                    string str47 = row3["b_drawback"].ToString();
                    string str48 = row3["c_drawback"].ToString();
                    string str49 = row3["single_max_amount"].ToString();
                    string str50 = row3["single_phase_amount"].ToString();
                    string str51 = row3["single_min_amount"].ToString();
                    string str52 = this.paramDict[string.Format("pk10_a_{0}", str45)];
                    string str53 = this.paramDict[string.Format("pk10_b_{0}", str45)];
                    string str54 = this.paramDict[string.Format("pk10_c_{0}", str45)];
                    string str55 = this.paramDict[string.Format("pk10_max_amount_{0}", str45)];
                    string str56 = this.paramDict[string.Format("pk10_phase_amount_{0}", str45)];
                    string str57 = this.paramDict[string.Format("pk10_single_min_amount_{0}", str45.ToLower())];
                    DataRow[] rowArray3 = table6.Select(string.Format(" play_id={0} ", str45));
                    string str58 = rowArray3[0]["a_drawback"].ToString();
                    string str59 = rowArray3[0]["b_drawback"].ToString();
                    string str60 = rowArray3[0]["c_drawback"].ToString();
                    string str61 = rowArray3[0]["single_max_amount"].ToString();
                    string str62 = rowArray3[0]["single_phase_amount"].ToString();
                    string str63 = rowArray3[0]["single_min_amount"].ToString();
                    if ((((double.Parse(str58) == double.Parse(str52)) && (double.Parse(str59) == double.Parse(str53))) && ((double.Parse(str60) == double.Parse(str54)) && (double.Parse(str61) == double.Parse(str55)))) && ((double.Parse(str62) == double.Parse(str56)) && (double.Parse(str63) == double.Parse(str57))))
                    {
                        continue;
                    }
                    if (string.IsNullOrEmpty(this.cz_users_model.get_kc_kind()) || this.cz_users_model.get_kc_kind().Equals("0"))
                    {
                        if (double.Parse(str52) > double.Parse(str46))
                        {
                            str52 = str46;
                        }
                        if (double.Parse(str53) > double.Parse(str47))
                        {
                            str53 = str47;
                        }
                        if (double.Parse(str54) > double.Parse(str48))
                        {
                            str54 = str48;
                        }
                    }
                    else
                    {
                        str265 = this.cz_users_model.get_kc_kind().ToLower();
                        if (str265 != null)
                        {
                            if (!(str265 == "a"))
                            {
                                if (str265 == "b")
                                {
                                    goto Label_1A4A;
                                }
                                if (str265 == "c")
                                {
                                    goto Label_1A60;
                                }
                            }
                            else if (double.Parse(str52) > double.Parse(str46))
                            {
                                str52 = str46;
                            }
                        }
                    }
                    goto Label_1A74;
                Label_1A4A:
                    if (double.Parse(str53) > double.Parse(str47))
                    {
                        str53 = str47;
                    }
                    goto Label_1A74;
                Label_1A60:
                    if (double.Parse(str54) > double.Parse(str48))
                    {
                        str54 = str48;
                    }
                Label_1A74:
                    if (double.Parse(str55) > double.Parse(str49))
                    {
                        str55 = str49;
                    }
                    if (double.Parse(str56) > double.Parse(str50))
                    {
                        str56 = str50;
                    }
                    if (double.Parse(str57) < double.Parse(str51))
                    {
                        str57 = str51;
                    }
                    if ((((double.Parse(str58) == double.Parse(str52)) && (double.Parse(str59) == double.Parse(str53))) && ((double.Parse(str60) == double.Parse(str54)) && (double.Parse(str61) == double.Parse(str55)))) && ((double.Parse(str62) == double.Parse(str56)) && (double.Parse(str63) == double.Parse(str57))))
                    {
                        continue;
                    }
                    cz_drawback_pk10 _pk = new cz_drawback_pk10();
                    _pk.set_play_id(new int?(Convert.ToInt32(str45)));
                    if (!string.IsNullOrEmpty(str52))
                    {
                        _pk.set_a_drawback(new decimal?(Convert.ToDecimal(str52)));
                    }
                    if (!string.IsNullOrEmpty(str53))
                    {
                        _pk.set_b_drawback(new decimal?(Convert.ToDecimal(str53)));
                    }
                    if (!string.IsNullOrEmpty(str54))
                    {
                        _pk.set_c_drawback(new decimal?(Convert.ToDecimal(str54)));
                    }
                    _pk.set_single_max_amount(new decimal?(Convert.ToDecimal(str55)));
                    _pk.set_single_phase_amount(new decimal?(Convert.ToDecimal(str56)));
                    _pk.set_single_min_amount(new decimal?(Convert.ToDecimal(str57)));
                    _pk.set_u_name(this.cz_users_model.get_u_name());
                    string str64 = "0";
                    if (string.IsNullOrEmpty(this.cz_users_model.get_kc_kind()) || this.cz_users_model.get_kc_kind().Equals("0"))
                    {
                        str64 = "0";
                    }
                    else
                    {
                        str64 = this.cz_users_model.get_kc_kind();
                    }
                    string str65 = "";
                    str265 = str45;
                    if (str265 == null)
                    {
                        goto Label_1C88;
                    }
                    if (!(str265 == "1"))
                    {
                        if (str265 == "2")
                        {
                            goto Label_1C6D;
                        }
                        if (str265 == "3")
                        {
                            goto Label_1C76;
                        }
                        if (str265 == "4")
                        {
                            goto Label_1C7F;
                        }
                        goto Label_1C88;
                    }
                    str65 = "1,5,9,13,17,21,24,27,30,33";
                    goto Label_1C8C;
                Label_1C6D:
                    str65 = "2,6,10,14,18,22,25,28,31,34";
                    goto Label_1C8C;
                Label_1C76:
                    str65 = "3,7,11,15,19,23,26,29,32,35";
                    goto Label_1C8C;
                Label_1C7F:
                    str65 = "4,8,12,16,20";
                    goto Label_1C8C;
                Label_1C88:
                    str65 = str45;
                Label_1C8C:
                    dictionary3.Add(str65, _pk);
                    if (!this.cz_users_model.get_u_type().Equals("hy"))
                    {
                        drawback_downuser _downuser19 = new drawback_downuser();
                        _downuser19.set_updateUserName(this.cz_users_model.get_u_name());
                        _downuser19.set_updateUserType(this.cz_users_model.get_u_type());
                        _downuser19.set_play_id(str65);
                        _downuser19.set_opt("dq");
                        _downuser19.set_douvalue(double.Parse(_pk.get_single_phase_amount().ToString()));
                        list5.Add(_downuser19);
                        drawback_downuser _downuser20 = new drawback_downuser();
                        _downuser20.set_updateUserName(this.cz_users_model.get_u_name());
                        _downuser20.set_updateUserType(this.cz_users_model.get_u_type());
                        _downuser20.set_play_id(str65);
                        _downuser20.set_opt("dz");
                        _downuser20.set_douvalue(double.Parse(_pk.get_single_max_amount().ToString()));
                        list5.Add(_downuser20);
                        if (str64.ToLower().Equals("a"))
                        {
                            drawback_downuser _downuser21 = new drawback_downuser();
                            _downuser21.set_updateUserName(this.cz_users_model.get_u_name());
                            _downuser21.set_updateUserType(this.cz_users_model.get_u_type());
                            _downuser21.set_play_id(str65);
                            _downuser21.set_opt("a");
                            _downuser21.set_douvalue(double.Parse(_pk.get_a_drawback().ToString()));
                            list5.Add(_downuser21);
                        }
                        else if (str64.ToLower().Equals("b"))
                        {
                            drawback_downuser _downuser22 = new drawback_downuser();
                            _downuser22.set_updateUserName(this.cz_users_model.get_u_name());
                            _downuser22.set_updateUserType(this.cz_users_model.get_u_type());
                            _downuser22.set_play_id(str65);
                            _downuser22.set_opt("b");
                            _downuser22.set_douvalue(double.Parse(_pk.get_b_drawback().ToString()));
                            list5.Add(_downuser22);
                        }
                        else if (str64.ToLower().Equals("c"))
                        {
                            drawback_downuser _downuser23 = new drawback_downuser();
                            _downuser23.set_updateUserName(this.cz_users_model.get_u_name());
                            _downuser23.set_updateUserType(this.cz_users_model.get_u_type());
                            _downuser23.set_play_id(str65);
                            _downuser23.set_opt("c");
                            _downuser23.set_douvalue(double.Parse(_pk.get_c_drawback().ToString()));
                            list5.Add(_downuser23);
                        }
                        else if (str64.Equals("0"))
                        {
                            drawback_downuser _downuser24 = new drawback_downuser();
                            _downuser24.set_updateUserName(this.cz_users_model.get_u_name());
                            _downuser24.set_updateUserType(this.cz_users_model.get_u_type());
                            _downuser24.set_play_id(str65);
                            _downuser24.set_opt("a");
                            _downuser24.set_douvalue(double.Parse(_pk.get_a_drawback().ToString()));
                            list5.Add(_downuser24);
                            drawback_downuser _downuser25 = new drawback_downuser();
                            _downuser25.set_updateUserName(this.cz_users_model.get_u_name());
                            _downuser25.set_updateUserType(this.cz_users_model.get_u_type());
                            _downuser25.set_play_id(str65);
                            _downuser25.set_opt("b");
                            _downuser25.set_douvalue(double.Parse(_pk.get_b_drawback().ToString()));
                            list5.Add(_downuser25);
                            drawback_downuser _downuser26 = new drawback_downuser();
                            _downuser26.set_updateUserName(this.cz_users_model.get_u_name());
                            _downuser26.set_updateUserType(this.cz_users_model.get_u_type());
                            _downuser26.set_play_id(str65);
                            _downuser26.set_opt("c");
                            _downuser26.set_douvalue(double.Parse(_pk.get_c_drawback().ToString()));
                            list5.Add(_downuser26);
                        }
                    }
                    drawback_downuser _downuser27 = new drawback_downuser();
                    _downuser27.set_updateUserName(this.cz_users_model.get_u_name());
                    _downuser27.set_updateUserType(this.cz_users_model.get_u_type());
                    _downuser27.set_play_id(str65);
                    _downuser27.set_opt("mindz");
                    _downuser27.set_douvalue(double.Parse(_pk.get_single_min_amount().ToString()));
                    list6.Add(_downuser27);
                }
                string str66 = "0";
                if (string.IsNullOrEmpty(this.cz_users_model.get_kc_kind()) || this.cz_users_model.get_kc_kind().Equals("0"))
                {
                    str66 = "0";
                }
                else
                {
                    str66 = this.cz_users_model.get_kc_kind();
                }
                if (!CallBLL.cz_drawback_pk10_bll.UpdateDataList(dictionary3, str66, flag2, list5, list6))
                {
                    mess = string.Format("快彩：修改【{0}】退水出現異常，請重試!", LSKeys.get_PK10_Name());
                    isSuccess = false;
                    return;
                }
                base.user_drawback_kc_log(table6, CallBLL.cz_drawback_pk10_bll.GetDrawbackList(this.cz_users_model.get_u_name(), this.pk10_modify_playid).Tables[0], this.cz_users_model.get_u_name(), 2);
            }
            if ((this.table_xync != null) && !string.IsNullOrEmpty(this.xync_modify_playid))
            {
                DataTable table7 = CallBLL.cz_drawback_xync_bll.GetDrawbackLists(this.cz_users_model.get_sup_name(), this.xync_modify_playid).Tables[0];
                DataTable table8 = CallBLL.cz_drawback_xync_bll.GetDrawbackLists(this.cz_users_model.get_u_name(), this.xync_modify_playid).Tables[0];
                Dictionary<string, cz_drawback_xync> dictionary4 = new Dictionary<string, cz_drawback_xync>();
                List<drawback_downuser> list7 = new List<drawback_downuser>();
                List<drawback_downuser> list8 = new List<drawback_downuser>();
                foreach (DataRow row4 in table7.Rows)
                {
                    string str67 = row4["play_id"].ToString();
                    string str68 = row4["a_drawback"].ToString();
                    string str69 = row4["b_drawback"].ToString();
                    string str70 = row4["c_drawback"].ToString();
                    string str71 = row4["single_max_amount"].ToString();
                    string str72 = row4["single_phase_amount"].ToString();
                    string str73 = row4["single_min_amount"].ToString();
                    string str74 = this.paramDict[string.Format("xync_a_{0}", str67)];
                    string str75 = this.paramDict[string.Format("xync_b_{0}", str67)];
                    string str76 = this.paramDict[string.Format("xync_c_{0}", str67)];
                    string str77 = this.paramDict[string.Format("xync_max_amount_{0}", str67)];
                    string str78 = this.paramDict[string.Format("xync_phase_amount_{0}", str67)];
                    string str79 = this.paramDict[string.Format("xync_single_min_amount_{0}", str67.ToLower())];
                    DataRow[] rowArray4 = table8.Select(string.Format(" play_id={0} ", str67));
                    string str80 = rowArray4[0]["a_drawback"].ToString();
                    string str81 = rowArray4[0]["b_drawback"].ToString();
                    string str82 = rowArray4[0]["c_drawback"].ToString();
                    string str83 = rowArray4[0]["single_max_amount"].ToString();
                    string str84 = rowArray4[0]["single_phase_amount"].ToString();
                    string str85 = rowArray4[0]["single_min_amount"].ToString();
                    if ((((double.Parse(str80) == double.Parse(str74)) && (double.Parse(str81) == double.Parse(str75))) && ((double.Parse(str82) == double.Parse(str76)) && (double.Parse(str83) == double.Parse(str77)))) && ((double.Parse(str84) == double.Parse(str78)) && (double.Parse(str85) == double.Parse(str79))))
                    {
                        continue;
                    }
                    if (string.IsNullOrEmpty(this.cz_users_model.get_kc_kind()) || this.cz_users_model.get_kc_kind().Equals("0"))
                    {
                        if (double.Parse(str74) > double.Parse(str68))
                        {
                            str74 = str68;
                        }
                        if (double.Parse(str75) > double.Parse(str69))
                        {
                            str75 = str69;
                        }
                        if (double.Parse(str76) > double.Parse(str70))
                        {
                            str76 = str70;
                        }
                    }
                    else
                    {
                        str265 = this.cz_users_model.get_kc_kind().ToLower();
                        if (str265 != null)
                        {
                            if (!(str265 == "a"))
                            {
                                if (str265 == "b")
                                {
                                    goto Label_2557;
                                }
                                if (str265 == "c")
                                {
                                    goto Label_256D;
                                }
                            }
                            else if (double.Parse(str74) > double.Parse(str68))
                            {
                                str74 = str68;
                            }
                        }
                    }
                    goto Label_2581;
                Label_2557:
                    if (double.Parse(str75) > double.Parse(str69))
                    {
                        str75 = str69;
                    }
                    goto Label_2581;
                Label_256D:
                    if (double.Parse(str76) > double.Parse(str70))
                    {
                        str76 = str70;
                    }
                Label_2581:
                    if (double.Parse(str77) > double.Parse(str71))
                    {
                        str77 = str71;
                    }
                    if (double.Parse(str78) > double.Parse(str72))
                    {
                        str78 = str72;
                    }
                    if (double.Parse(str79) < double.Parse(str73))
                    {
                        str79 = str73;
                    }
                    if ((((double.Parse(str80) != double.Parse(str74)) || (double.Parse(str81) != double.Parse(str75))) || ((double.Parse(str82) != double.Parse(str76)) || (double.Parse(str83) != double.Parse(str77)))) || ((double.Parse(str84) != double.Parse(str78)) || (double.Parse(str85) != double.Parse(str79))))
                    {
                        cz_drawback_xync _xync = new cz_drawback_xync();
                        _xync.set_play_id(new int?(Convert.ToInt32(str67)));
                        if (!string.IsNullOrEmpty(str74))
                        {
                            _xync.set_a_drawback(new decimal?(Convert.ToDecimal(str74)));
                        }
                        if (!string.IsNullOrEmpty(str75))
                        {
                            _xync.set_b_drawback(new decimal?(Convert.ToDecimal(str75)));
                        }
                        if (!string.IsNullOrEmpty(str76))
                        {
                            _xync.set_c_drawback(new decimal?(Convert.ToDecimal(str76)));
                        }
                        _xync.set_single_max_amount(new decimal?(Convert.ToDecimal(str77)));
                        _xync.set_single_phase_amount(new decimal?(Convert.ToDecimal(str78)));
                        _xync.set_single_min_amount(new decimal?(Convert.ToDecimal(str79)));
                        _xync.set_u_name(this.cz_users_model.get_u_name());
                        string str86 = "0";
                        if (string.IsNullOrEmpty(this.cz_users_model.get_kc_kind()) || this.cz_users_model.get_kc_kind().Equals("0"))
                        {
                            str86 = "0";
                        }
                        else
                        {
                            str86 = this.cz_users_model.get_kc_kind();
                        }
                        string str87 = "";
                        switch (str67)
                        {
                            case "81":
                                str87 = "81,86,91,96,101,106,111,116";
                                break;

                            case "82":
                                str87 = "82,87,92,97,102,107,112,117";
                                break;

                            case "83":
                                str87 = "83,88,93,98,103,108,113,118";
                                break;

                            case "84":
                                str87 = "84,89,94,99,104,109,114,119";
                                break;

                            case "85":
                                str87 = "85,90,95,100,105,110,115,120";
                                break;

                            case "121":
                                str87 = "121,123,125,127,129,131,133,135";
                                break;

                            case "122":
                                str87 = "122,124,126,128,130,132,134,136";
                                break;

                            default:
                                str87 = str67;
                                break;
                        }
                        dictionary4.Add(str87, _xync);
                        if (!this.cz_users_model.get_u_type().Equals("hy"))
                        {
                            drawback_downuser _downuser28 = new drawback_downuser();
                            _downuser28.set_updateUserName(this.cz_users_model.get_u_name());
                            _downuser28.set_updateUserType(this.cz_users_model.get_u_type());
                            _downuser28.set_play_id(str87);
                            _downuser28.set_opt("dq");
                            _downuser28.set_douvalue(double.Parse(_xync.get_single_phase_amount().ToString()));
                            list7.Add(_downuser28);
                            drawback_downuser _downuser29 = new drawback_downuser();
                            _downuser29.set_updateUserName(this.cz_users_model.get_u_name());
                            _downuser29.set_updateUserType(this.cz_users_model.get_u_type());
                            _downuser29.set_play_id(str87);
                            _downuser29.set_opt("dz");
                            _downuser29.set_douvalue(double.Parse(_xync.get_single_max_amount().ToString()));
                            list7.Add(_downuser29);
                            if (str86.ToLower().Equals("a"))
                            {
                                drawback_downuser _downuser30 = new drawback_downuser();
                                _downuser30.set_updateUserName(this.cz_users_model.get_u_name());
                                _downuser30.set_updateUserType(this.cz_users_model.get_u_type());
                                _downuser30.set_play_id(str87);
                                _downuser30.set_opt("a");
                                _downuser30.set_douvalue(double.Parse(_xync.get_a_drawback().ToString()));
                                list7.Add(_downuser30);
                            }
                            else if (str86.ToLower().Equals("b"))
                            {
                                drawback_downuser _downuser31 = new drawback_downuser();
                                _downuser31.set_updateUserName(this.cz_users_model.get_u_name());
                                _downuser31.set_updateUserType(this.cz_users_model.get_u_type());
                                _downuser31.set_play_id(str87);
                                _downuser31.set_opt("b");
                                _downuser31.set_douvalue(double.Parse(_xync.get_b_drawback().ToString()));
                                list7.Add(_downuser31);
                            }
                            else if (str86.ToLower().Equals("c"))
                            {
                                drawback_downuser _downuser32 = new drawback_downuser();
                                _downuser32.set_updateUserName(this.cz_users_model.get_u_name());
                                _downuser32.set_updateUserType(this.cz_users_model.get_u_type());
                                _downuser32.set_play_id(str87);
                                _downuser32.set_opt("c");
                                _downuser32.set_douvalue(double.Parse(_xync.get_c_drawback().ToString()));
                                list7.Add(_downuser32);
                            }
                            else if (str86.Equals("0"))
                            {
                                drawback_downuser _downuser33 = new drawback_downuser();
                                _downuser33.set_updateUserName(this.cz_users_model.get_u_name());
                                _downuser33.set_updateUserType(this.cz_users_model.get_u_type());
                                _downuser33.set_play_id(str87);
                                _downuser33.set_opt("a");
                                _downuser33.set_douvalue(double.Parse(_xync.get_a_drawback().ToString()));
                                list7.Add(_downuser33);
                                drawback_downuser _downuser34 = new drawback_downuser();
                                _downuser34.set_updateUserName(this.cz_users_model.get_u_name());
                                _downuser34.set_updateUserType(this.cz_users_model.get_u_type());
                                _downuser34.set_play_id(str87);
                                _downuser34.set_opt("b");
                                _downuser34.set_douvalue(double.Parse(_xync.get_b_drawback().ToString()));
                                list7.Add(_downuser34);
                                drawback_downuser _downuser35 = new drawback_downuser();
                                _downuser35.set_updateUserName(this.cz_users_model.get_u_name());
                                _downuser35.set_updateUserType(this.cz_users_model.get_u_type());
                                _downuser35.set_play_id(str87);
                                _downuser35.set_opt("c");
                                _downuser35.set_douvalue(double.Parse(_xync.get_c_drawback().ToString()));
                                list7.Add(_downuser35);
                            }
                        }
                        drawback_downuser _downuser36 = new drawback_downuser();
                        _downuser36.set_updateUserName(this.cz_users_model.get_u_name());
                        _downuser36.set_updateUserType(this.cz_users_model.get_u_type());
                        _downuser36.set_play_id(str87);
                        _downuser36.set_opt("mindz");
                        _downuser36.set_douvalue(double.Parse(_xync.get_single_min_amount().ToString()));
                        list8.Add(_downuser36);
                    }
                }
                string str88 = "0";
                if (string.IsNullOrEmpty(this.cz_users_model.get_kc_kind()) || this.cz_users_model.get_kc_kind().Equals("0"))
                {
                    str88 = "0";
                }
                else
                {
                    str88 = this.cz_users_model.get_kc_kind();
                }
                if (!CallBLL.cz_drawback_xync_bll.UpdateDataList(dictionary4, str88, flag2, list7, list8))
                {
                    mess = string.Format("快彩：修改【{0}】退水出現異常，請重試!", LSKeys.get_XYNC_Name());
                    isSuccess = false;
                    return;
                }
                base.user_drawback_kc_log(table8, CallBLL.cz_drawback_xync_bll.GetDrawbackLists(this.cz_users_model.get_u_name(), this.xync_modify_playid).Tables[0], this.cz_users_model.get_u_name(), 3);
            }
            if ((this.table_jsk3 != null) && !string.IsNullOrEmpty(this.jsk3_modify_playid))
            {
                DataTable table9 = CallBLL.cz_drawback_jsk3_bll.GetDrawbackLists(this.cz_users_model.get_sup_name(), this.jsk3_modify_playid).Tables[0];
                DataTable table10 = CallBLL.cz_drawback_jsk3_bll.GetDrawbackLists(this.cz_users_model.get_u_name(), this.jsk3_modify_playid).Tables[0];
                Dictionary<string, cz_drawback_jsk3> dictionary5 = new Dictionary<string, cz_drawback_jsk3>();
                List<drawback_downuser> list9 = new List<drawback_downuser>();
                List<drawback_downuser> list10 = new List<drawback_downuser>();
                foreach (DataRow row5 in table9.Rows)
                {
                    string str89 = row5["play_id"].ToString();
                    string str90 = row5["a_drawback"].ToString();
                    string str91 = row5["b_drawback"].ToString();
                    string str92 = row5["c_drawback"].ToString();
                    string str93 = row5["single_max_amount"].ToString();
                    string str94 = row5["single_phase_amount"].ToString();
                    string str95 = row5["single_min_amount"].ToString();
                    string str96 = this.paramDict[string.Format("jsk3_a_{0}", str89)];
                    string str97 = this.paramDict[string.Format("jsk3_b_{0}", str89)];
                    string str98 = this.paramDict[string.Format("jsk3_c_{0}", str89)];
                    string str99 = this.paramDict[string.Format("jsk3_max_amount_{0}", str89)];
                    string str100 = this.paramDict[string.Format("jsk3_phase_amount_{0}", str89)];
                    string str101 = this.paramDict[string.Format("jsk3_single_min_amount_{0}", str89.ToLower())];
                    DataRow[] rowArray5 = table10.Select(string.Format(" play_id={0} ", str89));
                    string str102 = rowArray5[0]["a_drawback"].ToString();
                    string str103 = rowArray5[0]["b_drawback"].ToString();
                    string str104 = rowArray5[0]["c_drawback"].ToString();
                    string str105 = rowArray5[0]["single_max_amount"].ToString();
                    string str106 = rowArray5[0]["single_phase_amount"].ToString();
                    string str107 = rowArray5[0]["single_min_amount"].ToString();
                    if ((((double.Parse(str102) == double.Parse(str96)) && (double.Parse(str103) == double.Parse(str97))) && ((double.Parse(str104) == double.Parse(str98)) && (double.Parse(str105) == double.Parse(str99)))) && ((double.Parse(str106) == double.Parse(str100)) && (double.Parse(str107) == double.Parse(str101))))
                    {
                        continue;
                    }
                    if (string.IsNullOrEmpty(this.cz_users_model.get_kc_kind()) || string.IsNullOrEmpty("0"))
                    {
                        if (double.Parse(str96) > double.Parse(str90))
                        {
                            str96 = str90;
                        }
                        if (double.Parse(str97) > double.Parse(str91))
                        {
                            str97 = str91;
                        }
                        if (double.Parse(str98) > double.Parse(str92))
                        {
                            str98 = str92;
                        }
                    }
                    else
                    {
                        str265 = this.cz_users_model.get_kc_kind().ToLower();
                        if (str265 != null)
                        {
                            if (!(str265 == "a"))
                            {
                                if (str265 == "b")
                                {
                                    goto Label_30DC;
                                }
                                if (str265 == "c")
                                {
                                    goto Label_30F2;
                                }
                            }
                            else if (double.Parse(str96) > double.Parse(str90))
                            {
                                str96 = str90;
                            }
                        }
                    }
                    goto Label_3106;
                Label_30DC:
                    if (double.Parse(str97) > double.Parse(str91))
                    {
                        str97 = str91;
                    }
                    goto Label_3106;
                Label_30F2:
                    if (double.Parse(str98) > double.Parse(str92))
                    {
                        str98 = str92;
                    }
                Label_3106:
                    if (double.Parse(str99) > double.Parse(str93))
                    {
                        str99 = str93;
                    }
                    if (double.Parse(str100) > double.Parse(str94))
                    {
                        str100 = str94;
                    }
                    if (double.Parse(str101) < double.Parse(str95))
                    {
                        str101 = str95;
                    }
                    if ((((double.Parse(str102) != double.Parse(str96)) || (double.Parse(str103) != double.Parse(str97))) || ((double.Parse(str104) != double.Parse(str98)) || (double.Parse(str105) != double.Parse(str99)))) || ((double.Parse(str106) != double.Parse(str100)) || (double.Parse(str107) != double.Parse(str101))))
                    {
                        cz_drawback_jsk3 _jsk = new cz_drawback_jsk3();
                        _jsk.set_play_id(new int?(Convert.ToInt32(str89)));
                        if (!string.IsNullOrEmpty(str96))
                        {
                            _jsk.set_a_drawback(new decimal?(Convert.ToDecimal(str96)));
                        }
                        if (!string.IsNullOrEmpty(str97))
                        {
                            _jsk.set_b_drawback(new decimal?(Convert.ToDecimal(str97)));
                        }
                        if (!string.IsNullOrEmpty(str98))
                        {
                            _jsk.set_c_drawback(new decimal?(Convert.ToDecimal(str98)));
                        }
                        _jsk.set_single_max_amount(new decimal?(Convert.ToDecimal(str99)));
                        _jsk.set_single_phase_amount(new decimal?(Convert.ToDecimal(str100)));
                        _jsk.set_single_min_amount(new decimal?(Convert.ToDecimal(str101)));
                        _jsk.set_u_name(this.cz_users_model.get_u_name());
                        string str108 = "0";
                        if (string.IsNullOrEmpty(this.cz_users_model.get_kc_kind()) || this.cz_users_model.get_kc_kind().Equals("0"))
                        {
                            str108 = "0";
                        }
                        else
                        {
                            str108 = this.cz_users_model.get_kc_kind();
                        }
                        string str109 = str89;
                        dictionary5.Add(str109, _jsk);
                        if (!this.cz_users_model.get_u_type().Equals("hy"))
                        {
                            drawback_downuser _downuser37 = new drawback_downuser();
                            _downuser37.set_updateUserName(this.cz_users_model.get_u_name());
                            _downuser37.set_updateUserType(this.cz_users_model.get_u_type());
                            _downuser37.set_play_id(str109);
                            _downuser37.set_opt("dq");
                            _downuser37.set_douvalue(double.Parse(_jsk.get_single_phase_amount().ToString()));
                            list9.Add(_downuser37);
                            drawback_downuser _downuser38 = new drawback_downuser();
                            _downuser38.set_updateUserName(this.cz_users_model.get_u_name());
                            _downuser38.set_updateUserType(this.cz_users_model.get_u_type());
                            _downuser38.set_play_id(str109);
                            _downuser38.set_opt("dz");
                            _downuser38.set_douvalue(double.Parse(_jsk.get_single_max_amount().ToString()));
                            list9.Add(_downuser38);
                            if (str108.ToLower().Equals("a"))
                            {
                                drawback_downuser _downuser39 = new drawback_downuser();
                                _downuser39.set_updateUserName(this.cz_users_model.get_u_name());
                                _downuser39.set_updateUserType(this.cz_users_model.get_u_type());
                                _downuser39.set_play_id(str109);
                                _downuser39.set_opt("a");
                                _downuser39.set_douvalue(double.Parse(_jsk.get_a_drawback().ToString()));
                                list9.Add(_downuser39);
                            }
                            else if (str108.ToLower().Equals("b"))
                            {
                                drawback_downuser _downuser40 = new drawback_downuser();
                                _downuser40.set_updateUserName(this.cz_users_model.get_u_name());
                                _downuser40.set_updateUserType(this.cz_users_model.get_u_type());
                                _downuser40.set_play_id(str109);
                                _downuser40.set_opt("b");
                                _downuser40.set_douvalue(double.Parse(_jsk.get_b_drawback().ToString()));
                                list9.Add(_downuser40);
                            }
                            else if (str108.ToLower().Equals("c"))
                            {
                                drawback_downuser _downuser41 = new drawback_downuser();
                                _downuser41.set_updateUserName(this.cz_users_model.get_u_name());
                                _downuser41.set_updateUserType(this.cz_users_model.get_u_type());
                                _downuser41.set_play_id(str109);
                                _downuser41.set_opt("c");
                                _downuser41.set_douvalue(double.Parse(_jsk.get_c_drawback().ToString()));
                                list9.Add(_downuser41);
                            }
                            else if (str108.Equals("0"))
                            {
                                drawback_downuser _downuser42 = new drawback_downuser();
                                _downuser42.set_updateUserName(this.cz_users_model.get_u_name());
                                _downuser42.set_updateUserType(this.cz_users_model.get_u_type());
                                _downuser42.set_play_id(str109);
                                _downuser42.set_opt("a");
                                _downuser42.set_douvalue(double.Parse(_jsk.get_a_drawback().ToString()));
                                list9.Add(_downuser42);
                                drawback_downuser _downuser43 = new drawback_downuser();
                                _downuser43.set_updateUserName(this.cz_users_model.get_u_name());
                                _downuser43.set_updateUserType(this.cz_users_model.get_u_type());
                                _downuser43.set_play_id(str109);
                                _downuser43.set_opt("b");
                                _downuser43.set_douvalue(double.Parse(_jsk.get_b_drawback().ToString()));
                                list9.Add(_downuser43);
                                drawback_downuser _downuser44 = new drawback_downuser();
                                _downuser44.set_updateUserName(this.cz_users_model.get_u_name());
                                _downuser44.set_updateUserType(this.cz_users_model.get_u_type());
                                _downuser44.set_play_id(str109);
                                _downuser44.set_opt("c");
                                _downuser44.set_douvalue(double.Parse(_jsk.get_c_drawback().ToString()));
                                list9.Add(_downuser44);
                            }
                        }
                        drawback_downuser _downuser45 = new drawback_downuser();
                        _downuser45.set_updateUserName(this.cz_users_model.get_u_name());
                        _downuser45.set_updateUserType(this.cz_users_model.get_u_type());
                        _downuser45.set_play_id(str109);
                        _downuser45.set_opt("mindz");
                        _downuser45.set_douvalue(double.Parse(_jsk.get_single_min_amount().ToString()));
                        list10.Add(_downuser45);
                    }
                }
                string str110 = "0";
                if (string.IsNullOrEmpty(this.cz_users_model.get_kc_kind()) || this.cz_users_model.get_kc_kind().Equals("0"))
                {
                    str110 = "0";
                }
                else
                {
                    str110 = this.cz_users_model.get_kc_kind();
                }
                if (!CallBLL.cz_drawback_jsk3_bll.UpdateDataList(dictionary5, str110, flag2, list9, list10))
                {
                    mess = string.Format("快彩：修改【{0}】退水出現異常，請重試!", LSKeys.get_K3_Name());
                    isSuccess = false;
                    return;
                }
                base.user_drawback_kc_log(table10, CallBLL.cz_drawback_jsk3_bll.GetDrawbackLists(this.cz_users_model.get_u_name(), this.jsk3_modify_playid).Tables[0], this.cz_users_model.get_u_name(), 4);
            }
            if ((this.table_kl8 != null) && !string.IsNullOrEmpty(this.kl8_modify_playid))
            {
                DataTable table11 = CallBLL.cz_drawback_kl8_bll.GetDrawbackLists(this.cz_users_model.get_sup_name(), this.kl8_modify_playid).Tables[0];
                DataTable table12 = CallBLL.cz_drawback_kl8_bll.GetDrawbackLists(this.cz_users_model.get_u_name(), this.kl8_modify_playid).Tables[0];
                Dictionary<string, cz_drawback_kl8> dictionary6 = new Dictionary<string, cz_drawback_kl8>();
                List<drawback_downuser> list11 = new List<drawback_downuser>();
                List<drawback_downuser> list12 = new List<drawback_downuser>();
                foreach (DataRow row6 in table11.Rows)
                {
                    string str111 = row6["play_id"].ToString();
                    string str112 = row6["a_drawback"].ToString();
                    string str113 = row6["b_drawback"].ToString();
                    string str114 = row6["c_drawback"].ToString();
                    string str115 = row6["single_max_amount"].ToString();
                    string str116 = row6["single_phase_amount"].ToString();
                    string str117 = row6["single_min_amount"].ToString();
                    string str118 = this.paramDict[string.Format("kl8_a_{0}", str111)];
                    string str119 = this.paramDict[string.Format("kl8_b_{0}", str111)];
                    string str120 = this.paramDict[string.Format("kl8_c_{0}", str111)];
                    string str121 = this.paramDict[string.Format("kl8_max_amount_{0}", str111)];
                    string str122 = this.paramDict[string.Format("kl8_phase_amount_{0}", str111)];
                    string str123 = this.paramDict[string.Format("kl8_single_min_amount_{0}", str111.ToLower())];
                    DataRow[] rowArray6 = table12.Select(string.Format(" play_id={0} ", str111));
                    string str124 = rowArray6[0]["a_drawback"].ToString();
                    string str125 = rowArray6[0]["b_drawback"].ToString();
                    string str126 = rowArray6[0]["c_drawback"].ToString();
                    string str127 = rowArray6[0]["single_max_amount"].ToString();
                    string str128 = rowArray6[0]["single_phase_amount"].ToString();
                    string str129 = rowArray6[0]["single_min_amount"].ToString();
                    if ((((double.Parse(str124) == double.Parse(str118)) && (double.Parse(str125) == double.Parse(str119))) && ((double.Parse(str126) == double.Parse(str120)) && (double.Parse(str127) == double.Parse(str121)))) && ((double.Parse(str128) == double.Parse(str122)) && (double.Parse(str129) == double.Parse(str123))))
                    {
                        continue;
                    }
                    if (string.IsNullOrEmpty(this.cz_users_model.get_kc_kind()) || string.IsNullOrEmpty("0"))
                    {
                        if (double.Parse(str118) > double.Parse(str112))
                        {
                            str118 = str112;
                        }
                        if (double.Parse(str119) > double.Parse(str113))
                        {
                            str119 = str113;
                        }
                        if (double.Parse(str120) > double.Parse(str114))
                        {
                            str120 = str114;
                        }
                    }
                    else
                    {
                        str265 = this.cz_users_model.get_kc_kind().ToLower();
                        if (str265 != null)
                        {
                            if (!(str265 == "a"))
                            {
                                if (str265 == "b")
                                {
                                    goto Label_3B68;
                                }
                                if (str265 == "c")
                                {
                                    goto Label_3B7E;
                                }
                            }
                            else if (double.Parse(str118) > double.Parse(str112))
                            {
                                str118 = str112;
                            }
                        }
                    }
                    goto Label_3B92;
                Label_3B68:
                    if (double.Parse(str119) > double.Parse(str113))
                    {
                        str119 = str113;
                    }
                    goto Label_3B92;
                Label_3B7E:
                    if (double.Parse(str120) > double.Parse(str114))
                    {
                        str120 = str114;
                    }
                Label_3B92:
                    if (double.Parse(str121) > double.Parse(str115))
                    {
                        str121 = str115;
                    }
                    if (double.Parse(str122) > double.Parse(str116))
                    {
                        str122 = str116;
                    }
                    if (double.Parse(str123) < double.Parse(str117))
                    {
                        str123 = str117;
                    }
                    if ((((double.Parse(str124) != double.Parse(str118)) || (double.Parse(str125) != double.Parse(str119))) || ((double.Parse(str126) != double.Parse(str120)) || (double.Parse(str127) != double.Parse(str121)))) || ((double.Parse(str128) != double.Parse(str122)) || (double.Parse(str129) != double.Parse(str123))))
                    {
                        cz_drawback_kl8 _kl2 = new cz_drawback_kl8();
                        _kl2.set_play_id(new int?(Convert.ToInt32(str111)));
                        if (!string.IsNullOrEmpty(str118))
                        {
                            _kl2.set_a_drawback(new decimal?(Convert.ToDecimal(str118)));
                        }
                        if (!string.IsNullOrEmpty(str119))
                        {
                            _kl2.set_b_drawback(new decimal?(Convert.ToDecimal(str119)));
                        }
                        if (!string.IsNullOrEmpty(str120))
                        {
                            _kl2.set_c_drawback(new decimal?(Convert.ToDecimal(str120)));
                        }
                        _kl2.set_single_max_amount(new decimal?(Convert.ToDecimal(str121)));
                        _kl2.set_single_phase_amount(new decimal?(Convert.ToDecimal(str122)));
                        _kl2.set_single_min_amount(new decimal?(Convert.ToDecimal(str123)));
                        _kl2.set_u_name(this.cz_users_model.get_u_name());
                        string str130 = "0";
                        if (string.IsNullOrEmpty(this.cz_users_model.get_kc_kind()) || this.cz_users_model.get_kc_kind().Equals("0"))
                        {
                            str130 = "0";
                        }
                        else
                        {
                            str130 = this.cz_users_model.get_kc_kind();
                        }
                        string str131 = str111;
                        dictionary6.Add(str131, _kl2);
                        if (!this.cz_users_model.get_u_type().Equals("hy"))
                        {
                            drawback_downuser _downuser46 = new drawback_downuser();
                            _downuser46.set_updateUserName(this.cz_users_model.get_u_name());
                            _downuser46.set_updateUserType(this.cz_users_model.get_u_type());
                            _downuser46.set_play_id(str131);
                            _downuser46.set_opt("dq");
                            _downuser46.set_douvalue(double.Parse(_kl2.get_single_phase_amount().ToString()));
                            list11.Add(_downuser46);
                            drawback_downuser _downuser47 = new drawback_downuser();
                            _downuser47.set_updateUserName(this.cz_users_model.get_u_name());
                            _downuser47.set_updateUserType(this.cz_users_model.get_u_type());
                            _downuser47.set_play_id(str131);
                            _downuser47.set_opt("dz");
                            _downuser47.set_douvalue(double.Parse(_kl2.get_single_max_amount().ToString()));
                            list11.Add(_downuser47);
                            if (str130.ToLower().Equals("a"))
                            {
                                drawback_downuser _downuser48 = new drawback_downuser();
                                _downuser48.set_updateUserName(this.cz_users_model.get_u_name());
                                _downuser48.set_updateUserType(this.cz_users_model.get_u_type());
                                _downuser48.set_play_id(str131);
                                _downuser48.set_opt("a");
                                _downuser48.set_douvalue(double.Parse(_kl2.get_a_drawback().ToString()));
                                list11.Add(_downuser48);
                            }
                            else if (str130.ToLower().Equals("b"))
                            {
                                drawback_downuser _downuser49 = new drawback_downuser();
                                _downuser49.set_updateUserName(this.cz_users_model.get_u_name());
                                _downuser49.set_updateUserType(this.cz_users_model.get_u_type());
                                _downuser49.set_play_id(str131);
                                _downuser49.set_opt("b");
                                _downuser49.set_douvalue(double.Parse(_kl2.get_b_drawback().ToString()));
                                list11.Add(_downuser49);
                            }
                            else if (str130.ToLower().Equals("c"))
                            {
                                drawback_downuser _downuser50 = new drawback_downuser();
                                _downuser50.set_updateUserName(this.cz_users_model.get_u_name());
                                _downuser50.set_updateUserType(this.cz_users_model.get_u_type());
                                _downuser50.set_play_id(str131);
                                _downuser50.set_opt("c");
                                _downuser50.set_douvalue(double.Parse(_kl2.get_c_drawback().ToString()));
                                list11.Add(_downuser50);
                            }
                            else if (str130.Equals("0"))
                            {
                                drawback_downuser _downuser51 = new drawback_downuser();
                                _downuser51.set_updateUserName(this.cz_users_model.get_u_name());
                                _downuser51.set_updateUserType(this.cz_users_model.get_u_type());
                                _downuser51.set_play_id(str131);
                                _downuser51.set_opt("a");
                                _downuser51.set_douvalue(double.Parse(_kl2.get_a_drawback().ToString()));
                                list11.Add(_downuser51);
                                drawback_downuser _downuser52 = new drawback_downuser();
                                _downuser52.set_updateUserName(this.cz_users_model.get_u_name());
                                _downuser52.set_updateUserType(this.cz_users_model.get_u_type());
                                _downuser52.set_play_id(str131);
                                _downuser52.set_opt("b");
                                _downuser52.set_douvalue(double.Parse(_kl2.get_b_drawback().ToString()));
                                list11.Add(_downuser52);
                                drawback_downuser _downuser53 = new drawback_downuser();
                                _downuser53.set_updateUserName(this.cz_users_model.get_u_name());
                                _downuser53.set_updateUserType(this.cz_users_model.get_u_type());
                                _downuser53.set_play_id(str131);
                                _downuser53.set_opt("c");
                                _downuser53.set_douvalue(double.Parse(_kl2.get_c_drawback().ToString()));
                                list11.Add(_downuser53);
                            }
                        }
                        drawback_downuser _downuser54 = new drawback_downuser();
                        _downuser54.set_updateUserName(this.cz_users_model.get_u_name());
                        _downuser54.set_updateUserType(this.cz_users_model.get_u_type());
                        _downuser54.set_play_id(str131);
                        _downuser54.set_opt("mindz");
                        _downuser54.set_douvalue(double.Parse(_kl2.get_single_min_amount().ToString()));
                        list12.Add(_downuser54);
                    }
                }
                string str132 = "0";
                if (string.IsNullOrEmpty(this.cz_users_model.get_kc_kind()) || this.cz_users_model.get_kc_kind().Equals("0"))
                {
                    str132 = "0";
                }
                else
                {
                    str132 = this.cz_users_model.get_kc_kind();
                }
                if (!CallBLL.cz_drawback_kl8_bll.UpdateDataList(dictionary6, str132, flag2, list11, list12))
                {
                    mess = string.Format("快彩：修改【{0}】退水出現異常，請重試!", LSKeys.get_KL8_Name());
                    isSuccess = false;
                    return;
                }
                base.user_drawback_kc_log(table12, CallBLL.cz_drawback_kl8_bll.GetDrawbackLists(this.cz_users_model.get_u_name(), this.kl8_modify_playid).Tables[0], this.cz_users_model.get_u_name(), 5);
            }
            if ((this.table_k8sc != null) && !string.IsNullOrEmpty(this.k8sc_modify_playid))
            {
                DataTable table13 = CallBLL.cz_drawback_k8sc_bll.GetDrawbackLists(this.cz_users_model.get_sup_name(), this.k8sc_modify_playid).Tables[0];
                DataTable table14 = CallBLL.cz_drawback_k8sc_bll.GetDrawbackLists(this.cz_users_model.get_u_name(), this.k8sc_modify_playid).Tables[0];
                Dictionary<string, cz_drawback_k8sc> dictionary7 = new Dictionary<string, cz_drawback_k8sc>();
                List<drawback_downuser> list13 = new List<drawback_downuser>();
                List<drawback_downuser> list14 = new List<drawback_downuser>();
                foreach (DataRow row7 in table13.Rows)
                {
                    string str133 = row7["play_id"].ToString();
                    string str134 = row7["a_drawback"].ToString();
                    string str135 = row7["b_drawback"].ToString();
                    string str136 = row7["c_drawback"].ToString();
                    string str137 = row7["single_max_amount"].ToString();
                    string str138 = row7["single_phase_amount"].ToString();
                    string str139 = row7["single_min_amount"].ToString();
                    string str140 = this.paramDict[string.Format("k8sc_a_{0}", str133)];
                    string str141 = this.paramDict[string.Format("k8sc_b_{0}", str133)];
                    string str142 = this.paramDict[string.Format("k8sc_c_{0}", str133)];
                    string str143 = this.paramDict[string.Format("k8sc_max_amount_{0}", str133)];
                    string str144 = this.paramDict[string.Format("k8sc_phase_amount_{0}", str133)];
                    string str145 = this.paramDict[string.Format("k8sc_single_min_amount_{0}", str133.ToLower())];
                    DataRow[] rowArray7 = table14.Select(string.Format(" play_id={0} ", str133));
                    string str146 = rowArray7[0]["a_drawback"].ToString();
                    string str147 = rowArray7[0]["b_drawback"].ToString();
                    string str148 = rowArray7[0]["c_drawback"].ToString();
                    string str149 = rowArray7[0]["single_max_amount"].ToString();
                    string str150 = rowArray7[0]["single_phase_amount"].ToString();
                    string str151 = rowArray7[0]["single_min_amount"].ToString();
                    if ((((double.Parse(str146) == double.Parse(str140)) && (double.Parse(str147) == double.Parse(str141))) && ((double.Parse(str148) == double.Parse(str142)) && (double.Parse(str149) == double.Parse(str143)))) && ((double.Parse(str150) == double.Parse(str144)) && (double.Parse(str151) == double.Parse(str145))))
                    {
                        continue;
                    }
                    if (string.IsNullOrEmpty(this.cz_users_model.get_kc_kind()) || this.cz_users_model.get_kc_kind().Equals("0"))
                    {
                        if (double.Parse(str140) > double.Parse(str134))
                        {
                            str140 = str134;
                        }
                        if (double.Parse(str141) > double.Parse(str135))
                        {
                            str141 = str135;
                        }
                        if (double.Parse(str142) > double.Parse(str136))
                        {
                            str142 = str136;
                        }
                    }
                    else
                    {
                        str265 = this.cz_users_model.get_kc_kind().ToLower();
                        if (str265 != null)
                        {
                            if (!(str265 == "a"))
                            {
                                if (str265 == "b")
                                {
                                    goto Label_466C;
                                }
                                if (str265 == "c")
                                {
                                    goto Label_468A;
                                }
                            }
                            else if (double.Parse(str140) > double.Parse(str134))
                            {
                                str140 = str134;
                            }
                        }
                    }
                    goto Label_46A6;
                Label_466C:
                    if (double.Parse(str141) > double.Parse(str135))
                    {
                        str141 = str135;
                    }
                    goto Label_46A6;
                Label_468A:
                    if (double.Parse(str142) > double.Parse(str136))
                    {
                        str142 = str136;
                    }
                Label_46A6:
                    if (double.Parse(str143) > double.Parse(str137))
                    {
                        str143 = str137;
                    }
                    if (double.Parse(str144) > double.Parse(str138))
                    {
                        str144 = str138;
                    }
                    if (double.Parse(str145) < double.Parse(str139))
                    {
                        str145 = str139;
                    }
                    if ((((double.Parse(str146) == double.Parse(str140)) && (double.Parse(str147) == double.Parse(str141))) && ((double.Parse(str148) == double.Parse(str142)) && (double.Parse(str149) == double.Parse(str143)))) && ((double.Parse(str150) == double.Parse(str144)) && (double.Parse(str151) == double.Parse(str145))))
                    {
                        continue;
                    }
                    cz_drawback_k8sc _ksc = new cz_drawback_k8sc();
                    _ksc.set_play_id(new int?(Convert.ToInt32(str133)));
                    if (!string.IsNullOrEmpty(str140))
                    {
                        _ksc.set_a_drawback(new decimal?(Convert.ToDecimal(str140)));
                    }
                    if (!string.IsNullOrEmpty(str141))
                    {
                        _ksc.set_b_drawback(new decimal?(Convert.ToDecimal(str141)));
                    }
                    if (!string.IsNullOrEmpty(str142))
                    {
                        _ksc.set_c_drawback(new decimal?(Convert.ToDecimal(str142)));
                    }
                    _ksc.set_single_max_amount(new decimal?(Convert.ToDecimal(str143)));
                    _ksc.set_single_phase_amount(new decimal?(Convert.ToDecimal(str144)));
                    _ksc.set_single_min_amount(new decimal?(Convert.ToDecimal(str145)));
                    _ksc.set_u_name(this.cz_users_model.get_u_name());
                    string str152 = "0";
                    if (string.IsNullOrEmpty(this.cz_users_model.get_kc_kind()) || this.cz_users_model.get_kc_kind().Equals("0"))
                    {
                        str152 = "0";
                    }
                    else
                    {
                        str152 = this.cz_users_model.get_kc_kind();
                    }
                    string str153 = "";
                    str265 = str133;
                    if (str265 == null)
                    {
                        goto Label_4903;
                    }
                    if (!(str265 == "1"))
                    {
                        if (str265 == "2")
                        {
                            goto Label_48ED;
                        }
                        if (str265 == "3")
                        {
                            goto Label_48F8;
                        }
                        goto Label_4903;
                    }
                    str153 = "1,4,7,10,13";
                    goto Label_4909;
                Label_48ED:
                    str153 = "2,5,8,11,14";
                    goto Label_4909;
                Label_48F8:
                    str153 = "3,6,9,12,15";
                    goto Label_4909;
                Label_4903:
                    str153 = str133;
                Label_4909:
                    dictionary7.Add(str153, _ksc);
                    if (!this.cz_users_model.get_u_type().Equals("hy"))
                    {
                        drawback_downuser _downuser55 = new drawback_downuser();
                        _downuser55.set_updateUserName(this.cz_users_model.get_u_name());
                        _downuser55.set_updateUserType(this.cz_users_model.get_u_type());
                        _downuser55.set_play_id(str153);
                        _downuser55.set_opt("dq");
                        _downuser55.set_douvalue(double.Parse(_ksc.get_single_phase_amount().ToString()));
                        list13.Add(_downuser55);
                        drawback_downuser _downuser56 = new drawback_downuser();
                        _downuser56.set_updateUserName(this.cz_users_model.get_u_name());
                        _downuser56.set_updateUserType(this.cz_users_model.get_u_type());
                        _downuser56.set_play_id(str153);
                        _downuser56.set_opt("dz");
                        _downuser56.set_douvalue(double.Parse(_ksc.get_single_max_amount().ToString()));
                        list13.Add(_downuser56);
                        if (str152.ToLower().Equals("a"))
                        {
                            drawback_downuser _downuser57 = new drawback_downuser();
                            _downuser57.set_updateUserName(this.cz_users_model.get_u_name());
                            _downuser57.set_updateUserType(this.cz_users_model.get_u_type());
                            _downuser57.set_play_id(str153);
                            _downuser57.set_opt("a");
                            _downuser57.set_douvalue(double.Parse(_ksc.get_a_drawback().ToString()));
                            list13.Add(_downuser57);
                        }
                        else if (str152.ToLower().Equals("b"))
                        {
                            drawback_downuser _downuser58 = new drawback_downuser();
                            _downuser58.set_updateUserName(this.cz_users_model.get_u_name());
                            _downuser58.set_updateUserType(this.cz_users_model.get_u_type());
                            _downuser58.set_play_id(str153);
                            _downuser58.set_opt("b");
                            _downuser58.set_douvalue(double.Parse(_ksc.get_b_drawback().ToString()));
                            list13.Add(_downuser58);
                        }
                        else if (str152.ToLower().Equals("c"))
                        {
                            drawback_downuser _downuser59 = new drawback_downuser();
                            _downuser59.set_updateUserName(this.cz_users_model.get_u_name());
                            _downuser59.set_updateUserType(this.cz_users_model.get_u_type());
                            _downuser59.set_play_id(str153);
                            _downuser59.set_opt("c");
                            _downuser59.set_douvalue(double.Parse(_ksc.get_c_drawback().ToString()));
                            list13.Add(_downuser59);
                        }
                        else if (str152.Equals("0"))
                        {
                            drawback_downuser _downuser60 = new drawback_downuser();
                            _downuser60.set_updateUserName(this.cz_users_model.get_u_name());
                            _downuser60.set_updateUserType(this.cz_users_model.get_u_type());
                            _downuser60.set_play_id(str153);
                            _downuser60.set_opt("a");
                            _downuser60.set_douvalue(double.Parse(_ksc.get_a_drawback().ToString()));
                            list13.Add(_downuser60);
                            drawback_downuser _downuser61 = new drawback_downuser();
                            _downuser61.set_updateUserName(this.cz_users_model.get_u_name());
                            _downuser61.set_updateUserType(this.cz_users_model.get_u_type());
                            _downuser61.set_play_id(str153);
                            _downuser61.set_opt("b");
                            _downuser61.set_douvalue(double.Parse(_ksc.get_b_drawback().ToString()));
                            list13.Add(_downuser61);
                            drawback_downuser _downuser62 = new drawback_downuser();
                            _downuser62.set_updateUserName(this.cz_users_model.get_u_name());
                            _downuser62.set_updateUserType(this.cz_users_model.get_u_type());
                            _downuser62.set_play_id(str153);
                            _downuser62.set_opt("c");
                            _downuser62.set_douvalue(double.Parse(_ksc.get_c_drawback().ToString()));
                            list13.Add(_downuser62);
                        }
                    }
                    drawback_downuser _downuser63 = new drawback_downuser();
                    _downuser63.set_updateUserName(this.cz_users_model.get_u_name());
                    _downuser63.set_updateUserType(this.cz_users_model.get_u_type());
                    _downuser63.set_play_id(str153);
                    _downuser63.set_opt("mindz");
                    _downuser63.set_douvalue(double.Parse(_ksc.get_single_min_amount().ToString()));
                    list14.Add(_downuser63);
                }
                string str154 = "0";
                if (string.IsNullOrEmpty(this.cz_users_model.get_kc_kind()) || this.cz_users_model.get_kc_kind().Equals("0"))
                {
                    str154 = "0";
                }
                else
                {
                    str154 = this.cz_users_model.get_kc_kind();
                }
                if (!CallBLL.cz_drawback_k8sc_bll.UpdateDataList(dictionary7, str154, flag2, list13, list14))
                {
                    mess = string.Format("快彩：修改【{0}】退水出現異常，請重試!", LSKeys.get_K8SC_Name());
                    isSuccess = false;
                    return;
                }
                base.user_drawback_kc_log(table14, CallBLL.cz_drawback_k8sc_bll.GetDrawbackLists(this.cz_users_model.get_u_name(), this.k8sc_modify_playid).Tables[0], this.cz_users_model.get_u_name(), 6);
            }
            if ((this.table_pcdd != null) && !string.IsNullOrEmpty(this.pcdd_modify_playid))
            {
                DataTable table15 = CallBLL.cz_drawback_pcdd_bll.GetDrawbackLists(this.cz_users_model.get_sup_name(), this.pcdd_modify_playid).Tables[0];
                DataTable table16 = CallBLL.cz_drawback_pcdd_bll.GetDrawbackLists(this.cz_users_model.get_u_name(), this.pcdd_modify_playid).Tables[0];
                Dictionary<string, cz_drawback_pcdd> dictionary8 = new Dictionary<string, cz_drawback_pcdd>();
                List<drawback_downuser> list15 = new List<drawback_downuser>();
                List<drawback_downuser> list16 = new List<drawback_downuser>();
                foreach (DataRow row8 in table15.Rows)
                {
                    string str155 = row8["play_id"].ToString();
                    string str156 = row8["a_drawback"].ToString();
                    string str157 = row8["b_drawback"].ToString();
                    string str158 = row8["c_drawback"].ToString();
                    string str159 = row8["single_max_amount"].ToString();
                    string str160 = row8["single_phase_amount"].ToString();
                    string str161 = row8["single_min_amount"].ToString();
                    string str162 = this.paramDict[string.Format("pcdd_a_{0}", str155)];
                    string str163 = this.paramDict[string.Format("pcdd_b_{0}", str155)];
                    string str164 = this.paramDict[string.Format("pcdd_c_{0}", str155)];
                    string str165 = this.paramDict[string.Format("pcdd_max_amount_{0}", str155)];
                    string str166 = this.paramDict[string.Format("pcdd_phase_amount_{0}", str155)];
                    string str167 = this.paramDict[string.Format("pcdd_single_min_amount_{0}", str155.ToLower())];
                    DataRow[] rowArray8 = table16.Select(string.Format(" play_id={0} ", str155));
                    string str168 = rowArray8[0]["a_drawback"].ToString();
                    string str169 = rowArray8[0]["b_drawback"].ToString();
                    string str170 = rowArray8[0]["c_drawback"].ToString();
                    string str171 = rowArray8[0]["single_max_amount"].ToString();
                    string str172 = rowArray8[0]["single_phase_amount"].ToString();
                    string str173 = rowArray8[0]["single_min_amount"].ToString();
                    if ((((double.Parse(str168) == double.Parse(str162)) && (double.Parse(str169) == double.Parse(str163))) && ((double.Parse(str170) == double.Parse(str164)) && (double.Parse(str171) == double.Parse(str165)))) && ((double.Parse(str172) == double.Parse(str166)) && (double.Parse(str173) == double.Parse(str167))))
                    {
                        continue;
                    }
                    if (string.IsNullOrEmpty(this.cz_users_model.get_kc_kind()) || this.cz_users_model.get_kc_kind().Equals("0"))
                    {
                        if (double.Parse(str162) > double.Parse(str156))
                        {
                            str162 = str156;
                        }
                        if (double.Parse(str163) > double.Parse(str157))
                        {
                            str163 = str157;
                        }
                        if (double.Parse(str164) > double.Parse(str158))
                        {
                            str164 = str158;
                        }
                    }
                    else
                    {
                        str265 = this.cz_users_model.get_kc_kind().ToLower();
                        if (str265 != null)
                        {
                            if (!(str265 == "a"))
                            {
                                if (str265 == "b")
                                {
                                    goto Label_5336;
                                }
                                if (str265 == "c")
                                {
                                    goto Label_5354;
                                }
                            }
                            else if (double.Parse(str162) > double.Parse(str156))
                            {
                                str162 = str156;
                            }
                        }
                    }
                    goto Label_5370;
                Label_5336:
                    if (double.Parse(str163) > double.Parse(str157))
                    {
                        str163 = str157;
                    }
                    goto Label_5370;
                Label_5354:
                    if (double.Parse(str164) > double.Parse(str158))
                    {
                        str164 = str158;
                    }
                Label_5370:
                    if (double.Parse(str165) > double.Parse(str159))
                    {
                        str165 = str159;
                    }
                    if (double.Parse(str166) > double.Parse(str160))
                    {
                        str166 = str160;
                    }
                    if (double.Parse(str167) < double.Parse(str161))
                    {
                        str167 = str161;
                    }
                    if ((((double.Parse(str168) == double.Parse(str162)) && (double.Parse(str169) == double.Parse(str163))) && ((double.Parse(str170) == double.Parse(str164)) && (double.Parse(str171) == double.Parse(str165)))) && ((double.Parse(str172) == double.Parse(str166)) && (double.Parse(str173) == double.Parse(str167))))
                    {
                        continue;
                    }
                    cz_drawback_pcdd _pcdd = new cz_drawback_pcdd();
                    _pcdd.set_play_id(new int?(Convert.ToInt32(str155)));
                    if (!string.IsNullOrEmpty(str162))
                    {
                        _pcdd.set_a_drawback(new decimal?(Convert.ToDecimal(str162)));
                    }
                    if (!string.IsNullOrEmpty(str163))
                    {
                        _pcdd.set_b_drawback(new decimal?(Convert.ToDecimal(str163)));
                    }
                    if (!string.IsNullOrEmpty(str164))
                    {
                        _pcdd.set_c_drawback(new decimal?(Convert.ToDecimal(str164)));
                    }
                    _pcdd.set_single_max_amount(new decimal?(Convert.ToDecimal(str165)));
                    _pcdd.set_single_phase_amount(new decimal?(Convert.ToDecimal(str166)));
                    _pcdd.set_single_min_amount(new decimal?(Convert.ToDecimal(str167)));
                    _pcdd.set_u_name(this.cz_users_model.get_u_name());
                    string str174 = "0";
                    if (string.IsNullOrEmpty(this.cz_users_model.get_kc_kind()) || this.cz_users_model.get_kc_kind().Equals("0"))
                    {
                        str174 = "0";
                    }
                    else
                    {
                        str174 = this.cz_users_model.get_kc_kind();
                    }
                    string str175 = "";
                    str265 = str155;
                    if (str265 == null)
                    {
                        goto Label_55B6;
                    }
                    if (!(str265 == "71007"))
                    {
                        if (str265 == "71008")
                        {
                            goto Label_55AB;
                        }
                        goto Label_55B6;
                    }
                    str175 = "71007,71009,71011";
                    goto Label_55BE;
                Label_55AB:
                    str175 = "71008,71010,71012";
                    goto Label_55BE;
                Label_55B6:
                    str175 = str155;
                Label_55BE:
                    dictionary8.Add(str175, _pcdd);
                    if (!this.cz_users_model.get_u_type().Equals("hy"))
                    {
                        drawback_downuser _downuser64 = new drawback_downuser();
                        _downuser64.set_updateUserName(this.cz_users_model.get_u_name());
                        _downuser64.set_updateUserType(this.cz_users_model.get_u_type());
                        _downuser64.set_play_id(str175);
                        _downuser64.set_opt("dq");
                        _downuser64.set_douvalue(double.Parse(_pcdd.get_single_phase_amount().ToString()));
                        list15.Add(_downuser64);
                        drawback_downuser _downuser65 = new drawback_downuser();
                        _downuser65.set_updateUserName(this.cz_users_model.get_u_name());
                        _downuser65.set_updateUserType(this.cz_users_model.get_u_type());
                        _downuser65.set_play_id(str175);
                        _downuser65.set_opt("dz");
                        _downuser65.set_douvalue(double.Parse(_pcdd.get_single_max_amount().ToString()));
                        list15.Add(_downuser65);
                        if (str174.ToLower().Equals("a"))
                        {
                            drawback_downuser _downuser66 = new drawback_downuser();
                            _downuser66.set_updateUserName(this.cz_users_model.get_u_name());
                            _downuser66.set_updateUserType(this.cz_users_model.get_u_type());
                            _downuser66.set_play_id(str175);
                            _downuser66.set_opt("a");
                            _downuser66.set_douvalue(double.Parse(_pcdd.get_a_drawback().ToString()));
                            list15.Add(_downuser66);
                        }
                        else if (str174.ToLower().Equals("b"))
                        {
                            drawback_downuser _downuser67 = new drawback_downuser();
                            _downuser67.set_updateUserName(this.cz_users_model.get_u_name());
                            _downuser67.set_updateUserType(this.cz_users_model.get_u_type());
                            _downuser67.set_play_id(str175);
                            _downuser67.set_opt("b");
                            _downuser67.set_douvalue(double.Parse(_pcdd.get_b_drawback().ToString()));
                            list15.Add(_downuser67);
                        }
                        else if (str174.ToLower().Equals("c"))
                        {
                            drawback_downuser _downuser68 = new drawback_downuser();
                            _downuser68.set_updateUserName(this.cz_users_model.get_u_name());
                            _downuser68.set_updateUserType(this.cz_users_model.get_u_type());
                            _downuser68.set_play_id(str175);
                            _downuser68.set_opt("c");
                            _downuser68.set_douvalue(double.Parse(_pcdd.get_c_drawback().ToString()));
                            list15.Add(_downuser68);
                        }
                        else if (str174.Equals("0"))
                        {
                            drawback_downuser _downuser69 = new drawback_downuser();
                            _downuser69.set_updateUserName(this.cz_users_model.get_u_name());
                            _downuser69.set_updateUserType(this.cz_users_model.get_u_type());
                            _downuser69.set_play_id(str175);
                            _downuser69.set_opt("a");
                            _downuser69.set_douvalue(double.Parse(_pcdd.get_a_drawback().ToString()));
                            list15.Add(_downuser69);
                            drawback_downuser _downuser70 = new drawback_downuser();
                            _downuser70.set_updateUserName(this.cz_users_model.get_u_name());
                            _downuser70.set_updateUserType(this.cz_users_model.get_u_type());
                            _downuser70.set_play_id(str175);
                            _downuser70.set_opt("b");
                            _downuser70.set_douvalue(double.Parse(_pcdd.get_b_drawback().ToString()));
                            list15.Add(_downuser70);
                            drawback_downuser _downuser71 = new drawback_downuser();
                            _downuser71.set_updateUserName(this.cz_users_model.get_u_name());
                            _downuser71.set_updateUserType(this.cz_users_model.get_u_type());
                            _downuser71.set_play_id(str175);
                            _downuser71.set_opt("c");
                            _downuser71.set_douvalue(double.Parse(_pcdd.get_c_drawback().ToString()));
                            list15.Add(_downuser71);
                        }
                    }
                    drawback_downuser _downuser72 = new drawback_downuser();
                    _downuser72.set_updateUserName(this.cz_users_model.get_u_name());
                    _downuser72.set_updateUserType(this.cz_users_model.get_u_type());
                    _downuser72.set_play_id(str175);
                    _downuser72.set_opt("mindz");
                    _downuser72.set_douvalue(double.Parse(_pcdd.get_single_min_amount().ToString()));
                    list16.Add(_downuser72);
                }
                string str176 = "0";
                if (string.IsNullOrEmpty(this.cz_users_model.get_kc_kind()) || this.cz_users_model.get_kc_kind().Equals("0"))
                {
                    str176 = "0";
                }
                else
                {
                    str176 = this.cz_users_model.get_kc_kind();
                }
                if (!CallBLL.cz_drawback_pcdd_bll.UpdateDataList(dictionary8, str176, flag2, list15, list16))
                {
                    mess = string.Format("快彩：修改【{0}】退水出現異常，請重試!", LSKeys.get_PCDD_Name());
                    isSuccess = false;
                    return;
                }
                base.user_drawback_kc_log(table16, CallBLL.cz_drawback_pcdd_bll.GetDrawbackLists(this.cz_users_model.get_u_name(), this.pcdd_modify_playid).Tables[0], this.cz_users_model.get_u_name(), 7);
            }
            if ((this.table_xyft5 != null) && !string.IsNullOrEmpty(this.xyft5_modify_playid))
            {
                DataTable table17 = CallBLL.cz_drawback_xyft5_bll.GetDrawbackList(this.cz_users_model.get_sup_name(), this.xyft5_modify_playid).Tables[0];
                DataTable table18 = CallBLL.cz_drawback_xyft5_bll.GetDrawbackList(this.cz_users_model.get_u_name(), this.xyft5_modify_playid).Tables[0];
                Dictionary<string, cz_drawback_xyft5> dictionary9 = new Dictionary<string, cz_drawback_xyft5>();
                List<drawback_downuser> list17 = new List<drawback_downuser>();
                List<drawback_downuser> list18 = new List<drawback_downuser>();
                foreach (DataRow row9 in table17.Rows)
                {
                    string str177 = row9["play_id"].ToString();
                    string str178 = row9["a_drawback"].ToString();
                    string str179 = row9["b_drawback"].ToString();
                    string str180 = row9["c_drawback"].ToString();
                    string str181 = row9["single_max_amount"].ToString();
                    string str182 = row9["single_phase_amount"].ToString();
                    string str183 = row9["single_min_amount"].ToString();
                    string str184 = this.paramDict[string.Format("xyft5_a_{0}", str177)];
                    string str185 = this.paramDict[string.Format("xyft5_b_{0}", str177)];
                    string str186 = this.paramDict[string.Format("xyft5_c_{0}", str177)];
                    string str187 = this.paramDict[string.Format("xyft5_max_amount_{0}", str177)];
                    string str188 = this.paramDict[string.Format("xyft5_phase_amount_{0}", str177)];
                    string str189 = this.paramDict[string.Format("xyft5_single_min_amount_{0}", str177.ToLower())];
                    DataRow[] rowArray9 = table18.Select(string.Format(" play_id={0} ", str177));
                    string str190 = rowArray9[0]["a_drawback"].ToString();
                    string str191 = rowArray9[0]["b_drawback"].ToString();
                    string str192 = rowArray9[0]["c_drawback"].ToString();
                    string str193 = rowArray9[0]["single_max_amount"].ToString();
                    string str194 = rowArray9[0]["single_phase_amount"].ToString();
                    string str195 = rowArray9[0]["single_min_amount"].ToString();
                    if ((((double.Parse(str190) == double.Parse(str184)) && (double.Parse(str191) == double.Parse(str185))) && ((double.Parse(str192) == double.Parse(str186)) && (double.Parse(str193) == double.Parse(str187)))) && ((double.Parse(str194) == double.Parse(str188)) && (double.Parse(str195) == double.Parse(str189))))
                    {
                        continue;
                    }
                    if (string.IsNullOrEmpty(this.cz_users_model.get_kc_kind()) || this.cz_users_model.get_kc_kind().Equals("0"))
                    {
                        if (double.Parse(str184) > double.Parse(str178))
                        {
                            str184 = str178;
                        }
                        if (double.Parse(str185) > double.Parse(str179))
                        {
                            str185 = str179;
                        }
                        if (double.Parse(str186) > double.Parse(str180))
                        {
                            str186 = str180;
                        }
                    }
                    else
                    {
                        str265 = this.cz_users_model.get_kc_kind().ToLower();
                        if (str265 != null)
                        {
                            if (!(str265 == "a"))
                            {
                                if (str265 == "b")
                                {
                                    goto Label_6007;
                                }
                                if (str265 == "c")
                                {
                                    goto Label_6025;
                                }
                            }
                            else if (double.Parse(str184) > double.Parse(str178))
                            {
                                str184 = str178;
                            }
                        }
                    }
                    goto Label_6041;
                Label_6007:
                    if (double.Parse(str185) > double.Parse(str179))
                    {
                        str185 = str179;
                    }
                    goto Label_6041;
                Label_6025:
                    if (double.Parse(str186) > double.Parse(str180))
                    {
                        str186 = str180;
                    }
                Label_6041:
                    if (double.Parse(str187) > double.Parse(str181))
                    {
                        str187 = str181;
                    }
                    if (double.Parse(str188) > double.Parse(str182))
                    {
                        str188 = str182;
                    }
                    if (double.Parse(str189) < double.Parse(str183))
                    {
                        str189 = str183;
                    }
                    if ((((double.Parse(str190) == double.Parse(str184)) && (double.Parse(str191) == double.Parse(str185))) && ((double.Parse(str192) == double.Parse(str186)) && (double.Parse(str193) == double.Parse(str187)))) && ((double.Parse(str194) == double.Parse(str188)) && (double.Parse(str195) == double.Parse(str189))))
                    {
                        continue;
                    }
                    cz_drawback_xyft5 _xyft = new cz_drawback_xyft5();
                    _xyft.set_play_id(new int?(Convert.ToInt32(str177)));
                    if (!string.IsNullOrEmpty(str184))
                    {
                        _xyft.set_a_drawback(new decimal?(Convert.ToDecimal(str184)));
                    }
                    if (!string.IsNullOrEmpty(str185))
                    {
                        _xyft.set_b_drawback(new decimal?(Convert.ToDecimal(str185)));
                    }
                    if (!string.IsNullOrEmpty(str186))
                    {
                        _xyft.set_c_drawback(new decimal?(Convert.ToDecimal(str186)));
                    }
                    _xyft.set_single_max_amount(new decimal?(Convert.ToDecimal(str187)));
                    _xyft.set_single_phase_amount(new decimal?(Convert.ToDecimal(str188)));
                    _xyft.set_single_min_amount(new decimal?(Convert.ToDecimal(str189)));
                    _xyft.set_u_name(this.cz_users_model.get_u_name());
                    string str196 = "0";
                    if (string.IsNullOrEmpty(this.cz_users_model.get_kc_kind()) || this.cz_users_model.get_kc_kind().Equals("0"))
                    {
                        str196 = "0";
                    }
                    else
                    {
                        str196 = this.cz_users_model.get_kc_kind();
                    }
                    string str197 = "";
                    str265 = str177;
                    if (str265 == null)
                    {
                        goto Label_62BD;
                    }
                    if (!(str265 == "1"))
                    {
                        if (str265 == "2")
                        {
                            goto Label_629C;
                        }
                        if (str265 == "3")
                        {
                            goto Label_62A7;
                        }
                        if (str265 == "4")
                        {
                            goto Label_62B2;
                        }
                        goto Label_62BD;
                    }
                    str197 = "1,5,9,13,17,21,24,27,30,33";
                    goto Label_62C5;
                Label_629C:
                    str197 = "2,6,10,14,18,22,25,28,31,34";
                    goto Label_62C5;
                Label_62A7:
                    str197 = "3,7,11,15,19,23,26,29,32,35";
                    goto Label_62C5;
                Label_62B2:
                    str197 = "4,8,12,16,20";
                    goto Label_62C5;
                Label_62BD:
                    str197 = str177;
                Label_62C5:
                    dictionary9.Add(str197, _xyft);
                    if (!this.cz_users_model.get_u_type().Equals("hy"))
                    {
                        drawback_downuser _downuser73 = new drawback_downuser();
                        _downuser73.set_updateUserName(this.cz_users_model.get_u_name());
                        _downuser73.set_updateUserType(this.cz_users_model.get_u_type());
                        _downuser73.set_play_id(str197);
                        _downuser73.set_opt("dq");
                        _downuser73.set_douvalue(double.Parse(_xyft.get_single_phase_amount().ToString()));
                        list17.Add(_downuser73);
                        drawback_downuser _downuser74 = new drawback_downuser();
                        _downuser74.set_updateUserName(this.cz_users_model.get_u_name());
                        _downuser74.set_updateUserType(this.cz_users_model.get_u_type());
                        _downuser74.set_play_id(str197);
                        _downuser74.set_opt("dz");
                        _downuser74.set_douvalue(double.Parse(_xyft.get_single_max_amount().ToString()));
                        list17.Add(_downuser74);
                        if (str196.ToLower().Equals("a"))
                        {
                            drawback_downuser _downuser75 = new drawback_downuser();
                            _downuser75.set_updateUserName(this.cz_users_model.get_u_name());
                            _downuser75.set_updateUserType(this.cz_users_model.get_u_type());
                            _downuser75.set_play_id(str197);
                            _downuser75.set_opt("a");
                            _downuser75.set_douvalue(double.Parse(_xyft.get_a_drawback().ToString()));
                            list17.Add(_downuser75);
                        }
                        else if (str196.ToLower().Equals("b"))
                        {
                            drawback_downuser _downuser76 = new drawback_downuser();
                            _downuser76.set_updateUserName(this.cz_users_model.get_u_name());
                            _downuser76.set_updateUserType(this.cz_users_model.get_u_type());
                            _downuser76.set_play_id(str197);
                            _downuser76.set_opt("b");
                            _downuser76.set_douvalue(double.Parse(_xyft.get_b_drawback().ToString()));
                            list17.Add(_downuser76);
                        }
                        else if (str196.ToLower().Equals("c"))
                        {
                            drawback_downuser _downuser77 = new drawback_downuser();
                            _downuser77.set_updateUserName(this.cz_users_model.get_u_name());
                            _downuser77.set_updateUserType(this.cz_users_model.get_u_type());
                            _downuser77.set_play_id(str197);
                            _downuser77.set_opt("c");
                            _downuser77.set_douvalue(double.Parse(_xyft.get_c_drawback().ToString()));
                            list17.Add(_downuser77);
                        }
                        else if (str196.Equals("0"))
                        {
                            drawback_downuser _downuser78 = new drawback_downuser();
                            _downuser78.set_updateUserName(this.cz_users_model.get_u_name());
                            _downuser78.set_updateUserType(this.cz_users_model.get_u_type());
                            _downuser78.set_play_id(str197);
                            _downuser78.set_opt("a");
                            _downuser78.set_douvalue(double.Parse(_xyft.get_a_drawback().ToString()));
                            list17.Add(_downuser78);
                            drawback_downuser _downuser79 = new drawback_downuser();
                            _downuser79.set_updateUserName(this.cz_users_model.get_u_name());
                            _downuser79.set_updateUserType(this.cz_users_model.get_u_type());
                            _downuser79.set_play_id(str197);
                            _downuser79.set_opt("b");
                            _downuser79.set_douvalue(double.Parse(_xyft.get_b_drawback().ToString()));
                            list17.Add(_downuser79);
                            drawback_downuser _downuser80 = new drawback_downuser();
                            _downuser80.set_updateUserName(this.cz_users_model.get_u_name());
                            _downuser80.set_updateUserType(this.cz_users_model.get_u_type());
                            _downuser80.set_play_id(str197);
                            _downuser80.set_opt("c");
                            _downuser80.set_douvalue(double.Parse(_xyft.get_c_drawback().ToString()));
                            list17.Add(_downuser80);
                        }
                    }
                    drawback_downuser _downuser81 = new drawback_downuser();
                    _downuser81.set_updateUserName(this.cz_users_model.get_u_name());
                    _downuser81.set_updateUserType(this.cz_users_model.get_u_type());
                    _downuser81.set_play_id(str197);
                    _downuser81.set_opt("mindz");
                    _downuser81.set_douvalue(double.Parse(_xyft.get_single_min_amount().ToString()));
                    list18.Add(_downuser81);
                }
                string str198 = "0";
                if (string.IsNullOrEmpty(this.cz_users_model.get_kc_kind()) || this.cz_users_model.get_kc_kind().Equals("0"))
                {
                    str198 = "0";
                }
                else
                {
                    str198 = this.cz_users_model.get_kc_kind();
                }
                if (!CallBLL.cz_drawback_xyft5_bll.UpdateDataList(dictionary9, str198, flag2, list17, list18))
                {
                    mess = string.Format("快彩：修改【{0}】退水出現異常，請重試!", LSKeys.get_XYFT5_Name());
                    isSuccess = false;
                    return;
                }
                base.user_drawback_kc_log(table18, CallBLL.cz_drawback_xyft5_bll.GetDrawbackList(this.cz_users_model.get_u_name(), this.xyft5_modify_playid).Tables[0], this.cz_users_model.get_u_name(), 9);
            }
            if ((this.table_pkbjl != null) && !string.IsNullOrEmpty(this.pkbjl_modify_playid))
            {
                DataTable table19 = CallBLL.cz_drawback_pkbjl_bll.GetDrawbackList(this.cz_users_model.get_sup_name(), this.pkbjl_modify_playid).Tables[0];
                DataTable table20 = CallBLL.cz_drawback_pkbjl_bll.GetDrawbackList(this.cz_users_model.get_u_name(), this.pkbjl_modify_playid).Tables[0];
                Dictionary<string, cz_drawback_pkbjl> dictionary10 = new Dictionary<string, cz_drawback_pkbjl>();
                List<drawback_downuser> list19 = new List<drawback_downuser>();
                List<drawback_downuser> list20 = new List<drawback_downuser>();
                foreach (DataRow row10 in table19.Rows)
                {
                    string str199 = row10["play_id"].ToString();
                    string str200 = row10["a_drawback"].ToString();
                    string str201 = row10["b_drawback"].ToString();
                    string str202 = row10["c_drawback"].ToString();
                    string str203 = row10["single_max_amount"].ToString();
                    string str204 = row10["single_phase_amount"].ToString();
                    string str205 = row10["single_min_amount"].ToString();
                    string str206 = this.paramDict[string.Format("pkbjl_a_{0}", str199)];
                    string str207 = this.paramDict[string.Format("pkbjl_b_{0}", str199)];
                    string str208 = this.paramDict[string.Format("pkbjl_c_{0}", str199)];
                    string str209 = this.paramDict[string.Format("pkbjl_max_amount_{0}", str199)];
                    string str210 = this.paramDict[string.Format("pkbjl_phase_amount_{0}", str199)];
                    string str211 = this.paramDict[string.Format("pkbjl_single_min_amount_{0}", str199.ToLower())];
                    DataRow[] rowArray10 = table20.Select(string.Format(" play_id={0} ", str199));
                    string str212 = rowArray10[0]["a_drawback"].ToString();
                    string str213 = rowArray10[0]["b_drawback"].ToString();
                    string str214 = rowArray10[0]["c_drawback"].ToString();
                    string str215 = rowArray10[0]["single_max_amount"].ToString();
                    string str216 = rowArray10[0]["single_phase_amount"].ToString();
                    string str217 = rowArray10[0]["single_min_amount"].ToString();
                    if ((((double.Parse(str212) == double.Parse(str206)) && (double.Parse(str213) == double.Parse(str207))) && ((double.Parse(str214) == double.Parse(str208)) && (double.Parse(str215) == double.Parse(str209)))) && ((double.Parse(str216) == double.Parse(str210)) && (double.Parse(str217) == double.Parse(str211))))
                    {
                        continue;
                    }
                    if (string.IsNullOrEmpty(this.cz_users_model.get_kc_kind()) || this.cz_users_model.get_kc_kind().Equals("0"))
                    {
                        if (double.Parse(str206) > double.Parse(str200))
                        {
                            str206 = str200;
                        }
                        if (double.Parse(str207) > double.Parse(str201))
                        {
                            str207 = str201;
                        }
                        if (double.Parse(str208) > double.Parse(str202))
                        {
                            str208 = str202;
                        }
                    }
                    else
                    {
                        str265 = this.cz_users_model.get_kc_kind().ToLower();
                        if (str265 != null)
                        {
                            if (!(str265 == "a"))
                            {
                                if (str265 == "b")
                                {
                                    goto Label_6D0F;
                                }
                                if (str265 == "c")
                                {
                                    goto Label_6D2D;
                                }
                            }
                            else if (double.Parse(str206) > double.Parse(str200))
                            {
                                str206 = str200;
                            }
                        }
                    }
                    goto Label_6D49;
                Label_6D0F:
                    if (double.Parse(str207) > double.Parse(str201))
                    {
                        str207 = str201;
                    }
                    goto Label_6D49;
                Label_6D2D:
                    if (double.Parse(str208) > double.Parse(str202))
                    {
                        str208 = str202;
                    }
                Label_6D49:
                    if (double.Parse(str209) > double.Parse(str203))
                    {
                        str209 = str203;
                    }
                    if (double.Parse(str210) > double.Parse(str204))
                    {
                        str210 = str204;
                    }
                    if (double.Parse(str211) < double.Parse(str205))
                    {
                        str211 = str205;
                    }
                    if ((((double.Parse(str212) != double.Parse(str206)) || (double.Parse(str213) != double.Parse(str207))) || ((double.Parse(str214) != double.Parse(str208)) || (double.Parse(str215) != double.Parse(str209)))) || ((double.Parse(str216) != double.Parse(str210)) || (double.Parse(str217) != double.Parse(str211))))
                    {
                        cz_drawback_pkbjl _pkbjl = new cz_drawback_pkbjl();
                        _pkbjl.set_play_id(new int?(Convert.ToInt32(str199)));
                        if (!string.IsNullOrEmpty(str206))
                        {
                            _pkbjl.set_a_drawback(new decimal?(Convert.ToDecimal(str206)));
                        }
                        if (!string.IsNullOrEmpty(str207))
                        {
                            _pkbjl.set_b_drawback(new decimal?(Convert.ToDecimal(str207)));
                        }
                        if (!string.IsNullOrEmpty(str208))
                        {
                            _pkbjl.set_c_drawback(new decimal?(Convert.ToDecimal(str208)));
                        }
                        _pkbjl.set_single_max_amount(new decimal?(Convert.ToDecimal(str209)));
                        _pkbjl.set_single_phase_amount(new decimal?(Convert.ToDecimal(str210)));
                        _pkbjl.set_single_min_amount(new decimal?(Convert.ToDecimal(str211)));
                        _pkbjl.set_u_name(this.cz_users_model.get_u_name());
                        string str218 = "0";
                        if (string.IsNullOrEmpty(this.cz_users_model.get_kc_kind()) || this.cz_users_model.get_kc_kind().Equals("0"))
                        {
                            str218 = "0";
                        }
                        else
                        {
                            str218 = this.cz_users_model.get_kc_kind();
                        }
                        string str219 = str199;
                        dictionary10.Add(str219, _pkbjl);
                        if (!this.cz_users_model.get_u_type().Equals("hy"))
                        {
                            drawback_downuser _downuser82 = new drawback_downuser();
                            _downuser82.set_updateUserName(this.cz_users_model.get_u_name());
                            _downuser82.set_updateUserType(this.cz_users_model.get_u_type());
                            _downuser82.set_play_id(str219);
                            _downuser82.set_opt("dq");
                            _downuser82.set_douvalue(double.Parse(_pkbjl.get_single_phase_amount().ToString()));
                            list19.Add(_downuser82);
                            drawback_downuser _downuser83 = new drawback_downuser();
                            _downuser83.set_updateUserName(this.cz_users_model.get_u_name());
                            _downuser83.set_updateUserType(this.cz_users_model.get_u_type());
                            _downuser83.set_play_id(str219);
                            _downuser83.set_opt("dz");
                            _downuser83.set_douvalue(double.Parse(_pkbjl.get_single_max_amount().ToString()));
                            list19.Add(_downuser83);
                            if (str218.ToLower().Equals("a"))
                            {
                                drawback_downuser _downuser84 = new drawback_downuser();
                                _downuser84.set_updateUserName(this.cz_users_model.get_u_name());
                                _downuser84.set_updateUserType(this.cz_users_model.get_u_type());
                                _downuser84.set_play_id(str219);
                                _downuser84.set_opt("a");
                                _downuser84.set_douvalue(double.Parse(_pkbjl.get_a_drawback().ToString()));
                                list19.Add(_downuser84);
                            }
                            else if (str218.ToLower().Equals("b"))
                            {
                                drawback_downuser _downuser85 = new drawback_downuser();
                                _downuser85.set_updateUserName(this.cz_users_model.get_u_name());
                                _downuser85.set_updateUserType(this.cz_users_model.get_u_type());
                                _downuser85.set_play_id(str219);
                                _downuser85.set_opt("b");
                                _downuser85.set_douvalue(double.Parse(_pkbjl.get_b_drawback().ToString()));
                                list19.Add(_downuser85);
                            }
                            else if (str218.ToLower().Equals("c"))
                            {
                                drawback_downuser _downuser86 = new drawback_downuser();
                                _downuser86.set_updateUserName(this.cz_users_model.get_u_name());
                                _downuser86.set_updateUserType(this.cz_users_model.get_u_type());
                                _downuser86.set_play_id(str219);
                                _downuser86.set_opt("c");
                                _downuser86.set_douvalue(double.Parse(_pkbjl.get_c_drawback().ToString()));
                                list19.Add(_downuser86);
                            }
                            else if (str218.Equals("0"))
                            {
                                drawback_downuser _downuser87 = new drawback_downuser();
                                _downuser87.set_updateUserName(this.cz_users_model.get_u_name());
                                _downuser87.set_updateUserType(this.cz_users_model.get_u_type());
                                _downuser87.set_play_id(str219);
                                _downuser87.set_opt("a");
                                _downuser87.set_douvalue(double.Parse(_pkbjl.get_a_drawback().ToString()));
                                list19.Add(_downuser87);
                                drawback_downuser _downuser88 = new drawback_downuser();
                                _downuser88.set_updateUserName(this.cz_users_model.get_u_name());
                                _downuser88.set_updateUserType(this.cz_users_model.get_u_type());
                                _downuser88.set_play_id(str219);
                                _downuser88.set_opt("b");
                                _downuser88.set_douvalue(double.Parse(_pkbjl.get_b_drawback().ToString()));
                                list19.Add(_downuser88);
                                drawback_downuser _downuser89 = new drawback_downuser();
                                _downuser89.set_updateUserName(this.cz_users_model.get_u_name());
                                _downuser89.set_updateUserType(this.cz_users_model.get_u_type());
                                _downuser89.set_play_id(str219);
                                _downuser89.set_opt("c");
                                _downuser89.set_douvalue(double.Parse(_pkbjl.get_c_drawback().ToString()));
                                list19.Add(_downuser89);
                            }
                        }
                        drawback_downuser _downuser90 = new drawback_downuser();
                        _downuser90.set_updateUserName(this.cz_users_model.get_u_name());
                        _downuser90.set_updateUserType(this.cz_users_model.get_u_type());
                        _downuser90.set_play_id(str219);
                        _downuser90.set_opt("mindz");
                        _downuser90.set_douvalue(double.Parse(_pkbjl.get_single_min_amount().ToString()));
                        list20.Add(_downuser90);
                    }
                }
                string str220 = "0";
                if (string.IsNullOrEmpty(this.cz_users_model.get_kc_kind()) || this.cz_users_model.get_kc_kind().Equals("0"))
                {
                    str220 = "0";
                }
                else
                {
                    str220 = this.cz_users_model.get_kc_kind();
                }
                if (!CallBLL.cz_drawback_pkbjl_bll.UpdateDataList(dictionary10, str220, flag2, list19, list20))
                {
                    mess = string.Format("快彩：修改【{0}】退水出現異常，請重試!", LSKeys.get_PKBJL_Name());
                    isSuccess = false;
                    return;
                }
                base.user_drawback_kc_log(table20, CallBLL.cz_drawback_pkbjl_bll.GetDrawbackList(this.cz_users_model.get_u_name(), this.pkbjl_modify_playid).Tables[0], this.cz_users_model.get_u_name(), 8);
            }
            if ((this.table_jscar != null) && !string.IsNullOrEmpty(this.jscar_modify_playid))
            {
                DataTable table21 = CallBLL.cz_drawback_jscar_bll.GetDrawbackList(this.cz_users_model.get_sup_name(), this.jscar_modify_playid).Tables[0];
                DataTable table22 = CallBLL.cz_drawback_jscar_bll.GetDrawbackList(this.cz_users_model.get_u_name(), this.jscar_modify_playid).Tables[0];
                Dictionary<string, cz_drawback_jscar> dictionary11 = new Dictionary<string, cz_drawback_jscar>();
                List<drawback_downuser> list21 = new List<drawback_downuser>();
                List<drawback_downuser> list22 = new List<drawback_downuser>();
                foreach (DataRow row11 in table21.Rows)
                {
                    string str221 = row11["play_id"].ToString();
                    string str222 = row11["a_drawback"].ToString();
                    string str223 = row11["b_drawback"].ToString();
                    string str224 = row11["c_drawback"].ToString();
                    string str225 = row11["single_max_amount"].ToString();
                    string str226 = row11["single_phase_amount"].ToString();
                    string str227 = row11["single_min_amount"].ToString();
                    string str228 = this.paramDict[string.Format("jscar_a_{0}", str221)];
                    string str229 = this.paramDict[string.Format("jscar_b_{0}", str221)];
                    string str230 = this.paramDict[string.Format("jscar_c_{0}", str221)];
                    string str231 = this.paramDict[string.Format("jscar_max_amount_{0}", str221)];
                    string str232 = this.paramDict[string.Format("jscar_phase_amount_{0}", str221)];
                    string str233 = this.paramDict[string.Format("jscar_single_min_amount_{0}", str221.ToLower())];
                    DataRow[] rowArray11 = table22.Select(string.Format(" play_id={0} ", str221));
                    string str234 = rowArray11[0]["a_drawback"].ToString();
                    string str235 = rowArray11[0]["b_drawback"].ToString();
                    string str236 = rowArray11[0]["c_drawback"].ToString();
                    string str237 = rowArray11[0]["single_max_amount"].ToString();
                    string str238 = rowArray11[0]["single_phase_amount"].ToString();
                    string str239 = rowArray11[0]["single_min_amount"].ToString();
                    if ((((double.Parse(str234) == double.Parse(str228)) && (double.Parse(str235) == double.Parse(str229))) && ((double.Parse(str236) == double.Parse(str230)) && (double.Parse(str237) == double.Parse(str231)))) && ((double.Parse(str238) == double.Parse(str232)) && (double.Parse(str239) == double.Parse(str233))))
                    {
                        continue;
                    }
                    if (string.IsNullOrEmpty(this.cz_users_model.get_kc_kind()) || this.cz_users_model.get_kc_kind().Equals("0"))
                    {
                        if (double.Parse(str228) > double.Parse(str222))
                        {
                            str228 = str222;
                        }
                        if (double.Parse(str229) > double.Parse(str223))
                        {
                            str229 = str223;
                        }
                        if (double.Parse(str230) > double.Parse(str224))
                        {
                            str230 = str224;
                        }
                    }
                    else
                    {
                        str265 = this.cz_users_model.get_kc_kind().ToLower();
                        if (str265 != null)
                        {
                            if (!(str265 == "a"))
                            {
                                if (str265 == "b")
                                {
                                    goto Label_7994;
                                }
                                if (str265 == "c")
                                {
                                    goto Label_79B2;
                                }
                            }
                            else if (double.Parse(str228) > double.Parse(str222))
                            {
                                str228 = str222;
                            }
                        }
                    }
                    goto Label_79CE;
                Label_7994:
                    if (double.Parse(str229) > double.Parse(str223))
                    {
                        str229 = str223;
                    }
                    goto Label_79CE;
                Label_79B2:
                    if (double.Parse(str230) > double.Parse(str224))
                    {
                        str230 = str224;
                    }
                Label_79CE:
                    if (double.Parse(str231) > double.Parse(str225))
                    {
                        str231 = str225;
                    }
                    if (double.Parse(str232) > double.Parse(str226))
                    {
                        str232 = str226;
                    }
                    if (double.Parse(str233) < double.Parse(str227))
                    {
                        str233 = str227;
                    }
                    if ((((double.Parse(str234) == double.Parse(str228)) && (double.Parse(str235) == double.Parse(str229))) && ((double.Parse(str236) == double.Parse(str230)) && (double.Parse(str237) == double.Parse(str231)))) && ((double.Parse(str238) == double.Parse(str232)) && (double.Parse(str239) == double.Parse(str233))))
                    {
                        continue;
                    }
                    cz_drawback_jscar _jscar = new cz_drawback_jscar();
                    _jscar.set_play_id(new int?(Convert.ToInt32(str221)));
                    if (!string.IsNullOrEmpty(str228))
                    {
                        _jscar.set_a_drawback(new decimal?(Convert.ToDecimal(str228)));
                    }
                    if (!string.IsNullOrEmpty(str229))
                    {
                        _jscar.set_b_drawback(new decimal?(Convert.ToDecimal(str229)));
                    }
                    if (!string.IsNullOrEmpty(str230))
                    {
                        _jscar.set_c_drawback(new decimal?(Convert.ToDecimal(str230)));
                    }
                    _jscar.set_single_max_amount(new decimal?(Convert.ToDecimal(str231)));
                    _jscar.set_single_phase_amount(new decimal?(Convert.ToDecimal(str232)));
                    _jscar.set_single_min_amount(new decimal?(Convert.ToDecimal(str233)));
                    _jscar.set_u_name(this.cz_users_model.get_u_name());
                    string str240 = "0";
                    if (string.IsNullOrEmpty(this.cz_users_model.get_kc_kind()) || this.cz_users_model.get_kc_kind().Equals("0"))
                    {
                        str240 = "0";
                    }
                    else
                    {
                        str240 = this.cz_users_model.get_kc_kind();
                    }
                    string str241 = "";
                    str265 = str221;
                    if (str265 == null)
                    {
                        goto Label_7C4A;
                    }
                    if (!(str265 == "1"))
                    {
                        if (str265 == "2")
                        {
                            goto Label_7C29;
                        }
                        if (str265 == "3")
                        {
                            goto Label_7C34;
                        }
                        if (str265 == "4")
                        {
                            goto Label_7C3F;
                        }
                        goto Label_7C4A;
                    }
                    str241 = "1,5,9,13,17,21,24,27,30,33";
                    goto Label_7C52;
                Label_7C29:
                    str241 = "2,6,10,14,18,22,25,28,31,34";
                    goto Label_7C52;
                Label_7C34:
                    str241 = "3,7,11,15,19,23,26,29,32,35";
                    goto Label_7C52;
                Label_7C3F:
                    str241 = "4,8,12,16,20";
                    goto Label_7C52;
                Label_7C4A:
                    str241 = str221;
                Label_7C52:
                    dictionary11.Add(str241, _jscar);
                    if (!this.cz_users_model.get_u_type().Equals("hy"))
                    {
                        drawback_downuser _downuser91 = new drawback_downuser();
                        _downuser91.set_updateUserName(this.cz_users_model.get_u_name());
                        _downuser91.set_updateUserType(this.cz_users_model.get_u_type());
                        _downuser91.set_play_id(str241);
                        _downuser91.set_opt("dq");
                        _downuser91.set_douvalue(double.Parse(_jscar.get_single_phase_amount().ToString()));
                        list21.Add(_downuser91);
                        drawback_downuser _downuser92 = new drawback_downuser();
                        _downuser92.set_updateUserName(this.cz_users_model.get_u_name());
                        _downuser92.set_updateUserType(this.cz_users_model.get_u_type());
                        _downuser92.set_play_id(str241);
                        _downuser92.set_opt("dz");
                        _downuser92.set_douvalue(double.Parse(_jscar.get_single_max_amount().ToString()));
                        list21.Add(_downuser92);
                        if (str240.ToLower().Equals("a"))
                        {
                            drawback_downuser _downuser93 = new drawback_downuser();
                            _downuser93.set_updateUserName(this.cz_users_model.get_u_name());
                            _downuser93.set_updateUserType(this.cz_users_model.get_u_type());
                            _downuser93.set_play_id(str241);
                            _downuser93.set_opt("a");
                            _downuser93.set_douvalue(double.Parse(_jscar.get_a_drawback().ToString()));
                            list21.Add(_downuser93);
                        }
                        else if (str240.ToLower().Equals("b"))
                        {
                            drawback_downuser _downuser94 = new drawback_downuser();
                            _downuser94.set_updateUserName(this.cz_users_model.get_u_name());
                            _downuser94.set_updateUserType(this.cz_users_model.get_u_type());
                            _downuser94.set_play_id(str241);
                            _downuser94.set_opt("b");
                            _downuser94.set_douvalue(double.Parse(_jscar.get_b_drawback().ToString()));
                            list21.Add(_downuser94);
                        }
                        else if (str240.ToLower().Equals("c"))
                        {
                            drawback_downuser _downuser95 = new drawback_downuser();
                            _downuser95.set_updateUserName(this.cz_users_model.get_u_name());
                            _downuser95.set_updateUserType(this.cz_users_model.get_u_type());
                            _downuser95.set_play_id(str241);
                            _downuser95.set_opt("c");
                            _downuser95.set_douvalue(double.Parse(_jscar.get_c_drawback().ToString()));
                            list21.Add(_downuser95);
                        }
                        else if (str240.Equals("0"))
                        {
                            drawback_downuser _downuser96 = new drawback_downuser();
                            _downuser96.set_updateUserName(this.cz_users_model.get_u_name());
                            _downuser96.set_updateUserType(this.cz_users_model.get_u_type());
                            _downuser96.set_play_id(str241);
                            _downuser96.set_opt("a");
                            _downuser96.set_douvalue(double.Parse(_jscar.get_a_drawback().ToString()));
                            list21.Add(_downuser96);
                            drawback_downuser _downuser97 = new drawback_downuser();
                            _downuser97.set_updateUserName(this.cz_users_model.get_u_name());
                            _downuser97.set_updateUserType(this.cz_users_model.get_u_type());
                            _downuser97.set_play_id(str241);
                            _downuser97.set_opt("b");
                            _downuser97.set_douvalue(double.Parse(_jscar.get_b_drawback().ToString()));
                            list21.Add(_downuser97);
                            drawback_downuser _downuser98 = new drawback_downuser();
                            _downuser98.set_updateUserName(this.cz_users_model.get_u_name());
                            _downuser98.set_updateUserType(this.cz_users_model.get_u_type());
                            _downuser98.set_play_id(str241);
                            _downuser98.set_opt("c");
                            _downuser98.set_douvalue(double.Parse(_jscar.get_c_drawback().ToString()));
                            list21.Add(_downuser98);
                        }
                    }
                    drawback_downuser _downuser99 = new drawback_downuser();
                    _downuser99.set_updateUserName(this.cz_users_model.get_u_name());
                    _downuser99.set_updateUserType(this.cz_users_model.get_u_type());
                    _downuser99.set_play_id(str241);
                    _downuser99.set_opt("mindz");
                    _downuser99.set_douvalue(double.Parse(_jscar.get_single_min_amount().ToString()));
                    list22.Add(_downuser99);
                }
                string str242 = "0";
                if (string.IsNullOrEmpty(this.cz_users_model.get_kc_kind()) || this.cz_users_model.get_kc_kind().Equals("0"))
                {
                    str242 = "0";
                }
                else
                {
                    str242 = this.cz_users_model.get_kc_kind();
                }
                if (!CallBLL.cz_drawback_jscar_bll.UpdateDataList(dictionary11, str242, flag2, list21, list22))
                {
                    mess = string.Format("快彩：修改【{0}】退水出現異常，請重試!", LSKeys.get_JSCAR_Name());
                    isSuccess = false;
                    return;
                }
                base.user_drawback_kc_log(table22, CallBLL.cz_drawback_jscar_bll.GetDrawbackList(this.cz_users_model.get_u_name(), this.jscar_modify_playid).Tables[0], this.cz_users_model.get_u_name(), 10);
            }
            if ((this.table_speed5 != null) && !string.IsNullOrEmpty(this.speed5_modify_playid))
            {
                DataTable table23 = CallBLL.cz_drawback_speed5_bll.GetDrawbackLists(this.cz_users_model.get_sup_name(), this.speed5_modify_playid).Tables[0];
                DataTable table24 = CallBLL.cz_drawback_speed5_bll.GetDrawbackLists(this.cz_users_model.get_u_name(), this.speed5_modify_playid).Tables[0];
                Dictionary<string, cz_drawback_speed5> dictionary12 = new Dictionary<string, cz_drawback_speed5>();
                List<drawback_downuser> list23 = new List<drawback_downuser>();
                List<drawback_downuser> list24 = new List<drawback_downuser>();
                foreach (DataRow row12 in table23.Rows)
                {
                    string str243 = row12["play_id"].ToString();
                    string str244 = row12["a_drawback"].ToString();
                    string str245 = row12["b_drawback"].ToString();
                    string str246 = row12["c_drawback"].ToString();
                    string str247 = row12["single_max_amount"].ToString();
                    string str248 = row12["single_phase_amount"].ToString();
                    string str249 = row12["single_min_amount"].ToString();
                    string str250 = this.paramDict[string.Format("speed5_a_{0}", str243)];
                    string str251 = this.paramDict[string.Format("speed5_b_{0}", str243)];
                    string str252 = this.paramDict[string.Format("speed5_c_{0}", str243)];
                    string str253 = this.paramDict[string.Format("speed5_max_amount_{0}", str243)];
                    string str254 = this.paramDict[string.Format("speed5_phase_amount_{0}", str243)];
                    string str255 = this.paramDict[string.Format("speed5_single_min_amount_{0}", str243.ToLower())];
                    DataRow[] rowArray12 = table24.Select(string.Format(" play_id={0} ", str243));
                    string str256 = rowArray12[0]["a_drawback"].ToString();
                    string str257 = rowArray12[0]["b_drawback"].ToString();
                    string str258 = rowArray12[0]["c_drawback"].ToString();
                    string str259 = rowArray12[0]["single_max_amount"].ToString();
                    string str260 = rowArray12[0]["single_phase_amount"].ToString();
                    string str261 = rowArray12[0]["single_min_amount"].ToString();
                    if ((((double.Parse(str256) == double.Parse(str250)) && (double.Parse(str257) == double.Parse(str251))) && ((double.Parse(str258) == double.Parse(str252)) && (double.Parse(str259) == double.Parse(str253)))) && ((double.Parse(str260) == double.Parse(str254)) && (double.Parse(str261) == double.Parse(str255))))
                    {
                        continue;
                    }
                    if (string.IsNullOrEmpty(this.cz_users_model.get_kc_kind()) || this.cz_users_model.get_kc_kind().Equals("0"))
                    {
                        if (double.Parse(str250) > double.Parse(str244))
                        {
                            str250 = str244;
                        }
                        if (double.Parse(str251) > double.Parse(str245))
                        {
                            str251 = str245;
                        }
                        if (double.Parse(str252) > double.Parse(str246))
                        {
                            str252 = str246;
                        }
                    }
                    else
                    {
                        str265 = this.cz_users_model.get_kc_kind().ToLower();
                        if (str265 != null)
                        {
                            if (!(str265 == "a"))
                            {
                                if (str265 == "b")
                                {
                                    goto Label_869C;
                                }
                                if (str265 == "c")
                                {
                                    goto Label_86BA;
                                }
                            }
                            else if (double.Parse(str250) > double.Parse(str244))
                            {
                                str250 = str244;
                            }
                        }
                    }
                    goto Label_86D6;
                Label_869C:
                    if (double.Parse(str251) > double.Parse(str245))
                    {
                        str251 = str245;
                    }
                    goto Label_86D6;
                Label_86BA:
                    if (double.Parse(str252) > double.Parse(str246))
                    {
                        str252 = str246;
                    }
                Label_86D6:
                    if (double.Parse(str253) > double.Parse(str247))
                    {
                        str253 = str247;
                    }
                    if (double.Parse(str254) > double.Parse(str248))
                    {
                        str254 = str248;
                    }
                    if (double.Parse(str255) < double.Parse(str249))
                    {
                        str255 = str249;
                    }
                    if ((((double.Parse(str256) == double.Parse(str250)) && (double.Parse(str257) == double.Parse(str251))) && ((double.Parse(str258) == double.Parse(str252)) && (double.Parse(str259) == double.Parse(str253)))) && ((double.Parse(str260) == double.Parse(str254)) && (double.Parse(str261) == double.Parse(str255))))
                    {
                        continue;
                    }
                    cz_drawback_speed5 _speed = new cz_drawback_speed5();
                    _speed.set_play_id(new int?(Convert.ToInt32(str243)));
                    if (!string.IsNullOrEmpty(str250))
                    {
                        _speed.set_a_drawback(new decimal?(Convert.ToDecimal(str250)));
                    }
                    if (!string.IsNullOrEmpty(str251))
                    {
                        _speed.set_b_drawback(new decimal?(Convert.ToDecimal(str251)));
                    }
                    if (!string.IsNullOrEmpty(str252))
                    {
                        _speed.set_c_drawback(new decimal?(Convert.ToDecimal(str252)));
                    }
                    _speed.set_single_max_amount(new decimal?(Convert.ToDecimal(str253)));
                    _speed.set_single_phase_amount(new decimal?(Convert.ToDecimal(str254)));
                    _speed.set_single_min_amount(new decimal?(Convert.ToDecimal(str255)));
                    _speed.set_u_name(this.cz_users_model.get_u_name());
                    string str262 = "0";
                    if (string.IsNullOrEmpty(this.cz_users_model.get_kc_kind()) || this.cz_users_model.get_kc_kind().Equals("0"))
                    {
                        str262 = "0";
                    }
                    else
                    {
                        str262 = this.cz_users_model.get_kc_kind();
                    }
                    string str263 = "";
                    str265 = str243;
                    if (str265 == null)
                    {
                        goto Label_8937;
                    }
                    if (!(str265 == "1"))
                    {
                        if (str265 == "2")
                        {
                            goto Label_8921;
                        }
                        if (str265 == "3")
                        {
                            goto Label_892C;
                        }
                        goto Label_8937;
                    }
                    str263 = "1,4,7,10,13";
                    goto Label_893F;
                Label_8921:
                    str263 = "2,5,8,11,14";
                    goto Label_893F;
                Label_892C:
                    str263 = "3,6,9,12,15";
                    goto Label_893F;
                Label_8937:
                    str263 = str243;
                Label_893F:
                    dictionary12.Add(str263, _speed);
                    if (!this.cz_users_model.get_u_type().Equals("hy"))
                    {
                        drawback_downuser _downuser100 = new drawback_downuser();
                        _downuser100.set_updateUserName(this.cz_users_model.get_u_name());
                        _downuser100.set_updateUserType(this.cz_users_model.get_u_type());
                        _downuser100.set_play_id(str263);
                        _downuser100.set_opt("dq");
                        _downuser100.set_douvalue(double.Parse(_speed.get_single_phase_amount().ToString()));
                        list23.Add(_downuser100);
                        drawback_downuser _downuser101 = new drawback_downuser();
                        _downuser101.set_updateUserName(this.cz_users_model.get_u_name());
                        _downuser101.set_updateUserType(this.cz_users_model.get_u_type());
                        _downuser101.set_play_id(str263);
                        _downuser101.set_opt("dz");
                        _downuser101.set_douvalue(double.Parse(_speed.get_single_max_amount().ToString()));
                        list23.Add(_downuser101);
                        if (str262.ToLower().Equals("a"))
                        {
                            drawback_downuser _downuser102 = new drawback_downuser();
                            _downuser102.set_updateUserName(this.cz_users_model.get_u_name());
                            _downuser102.set_updateUserType(this.cz_users_model.get_u_type());
                            _downuser102.set_play_id(str263);
                            _downuser102.set_opt("a");
                            _downuser102.set_douvalue(double.Parse(_speed.get_a_drawback().ToString()));
                            list23.Add(_downuser102);
                        }
                        else if (str262.ToLower().Equals("b"))
                        {
                            drawback_downuser _downuser103 = new drawback_downuser();
                            _downuser103.set_updateUserName(this.cz_users_model.get_u_name());
                            _downuser103.set_updateUserType(this.cz_users_model.get_u_type());
                            _downuser103.set_play_id(str263);
                            _downuser103.set_opt("b");
                            _downuser103.set_douvalue(double.Parse(_speed.get_b_drawback().ToString()));
                            list23.Add(_downuser103);
                        }
                        else if (str262.ToLower().Equals("c"))
                        {
                            drawback_downuser _downuser104 = new drawback_downuser();
                            _downuser104.set_updateUserName(this.cz_users_model.get_u_name());
                            _downuser104.set_updateUserType(this.cz_users_model.get_u_type());
                            _downuser104.set_play_id(str263);
                            _downuser104.set_opt("c");
                            _downuser104.set_douvalue(double.Parse(_speed.get_c_drawback().ToString()));
                            list23.Add(_downuser104);
                        }
                        else if (str262.Equals("0"))
                        {
                            drawback_downuser _downuser105 = new drawback_downuser();
                            _downuser105.set_updateUserName(this.cz_users_model.get_u_name());
                            _downuser105.set_updateUserType(this.cz_users_model.get_u_type());
                            _downuser105.set_play_id(str263);
                            _downuser105.set_opt("a");
                            _downuser105.set_douvalue(double.Parse(_speed.get_a_drawback().ToString()));
                            list23.Add(_downuser105);
                            drawback_downuser _downuser106 = new drawback_downuser();
                            _downuser106.set_updateUserName(this.cz_users_model.get_u_name());
                            _downuser106.set_updateUserType(this.cz_users_model.get_u_type());
                            _downuser106.set_play_id(str263);
                            _downuser106.set_opt("b");
                            _downuser106.set_douvalue(double.Parse(_speed.get_b_drawback().ToString()));
                            list23.Add(_downuser106);
                            drawback_downuser _downuser107 = new drawback_downuser();
                            _downuser107.set_updateUserName(this.cz_users_model.get_u_name());
                            _downuser107.set_updateUserType(this.cz_users_model.get_u_type());
                            _downuser107.set_play_id(str263);
                            _downuser107.set_opt("c");
                            _downuser107.set_douvalue(double.Parse(_speed.get_c_drawback().ToString()));
                            list23.Add(_downuser107);
                        }
                    }
                    drawback_downuser _downuser108 = new drawback_downuser();
                    _downuser108.set_updateUserName(this.cz_users_model.get_u_name());
                    _downuser108.set_updateUserType(this.cz_users_model.get_u_type());
                    _downuser108.set_play_id(str263);
                    _downuser108.set_opt("mindz");
                    _downuser108.set_douvalue(double.Parse(_speed.get_single_min_amount().ToString()));
                    list24.Add(_downuser108);
                }
                string str264 = "0";
                if (string.IsNullOrEmpty(this.cz_users_model.get_kc_kind()) || this.cz_users_model.get_kc_kind().Equals("0"))
                {
                    str264 = "0";
                }
                else
                {
                    str264 = this.cz_users_model.get_kc_kind();
                }
                if (!CallBLL.cz_drawback_speed5_bll.UpdateDataList(dictionary12, str264, flag2, list23, list24))
                {
                    mess = string.Format("快彩：修改【{0}】退水出現異常，請重試!", LSKeys.get_SPEED5_Name());
                    isSuccess = false;
                    return;
                }
                base.user_drawback_kc_log(table24, CallBLL.cz_drawback_speed5_bll.GetDrawbackLists(this.cz_users_model.get_u_name(), this.speed5_modify_playid).Tables[0], this.cz_users_model.get_u_name(), 11);
            }
            if (flag && this.IsOpenKC())
            {
                mess = "快彩：因獎期正在開盤中且下级已经有下单[退水]未能修改,單期單注修改成功!";
            }
            else
            {
                mess = "快彩：已經成功修改設置(注:如開盤中退水未能修改)!";
            }
        }

        private void UpdateSix(ref string mess, ref bool isSuccess)
        {
            if (!string.IsNullOrEmpty(this.six_modify_playid))
            {
                bool flag = CallBLL.cz_bet_six_bll.ExistsBetByUserName(this.cz_users_model.get_u_type(), this.cz_users_model.get_u_name());
                bool flag2 = CallBLL.cz_bet_six_bll.ExistsBetByUserName(this.cz_users_model.get_u_name());
                bool flag3 = CallBLL.cz_phase_six_bll.IsOpenPhase();
                bool flag4 = false;
                if ((flag && flag2) && flag3)
                {
                    flag4 = true;
                }
                DataTable table = CallBLL.cz_drawback_six_bll.GetDrawBackList(this.cz_users_model.get_sup_name(), this.six_modify_playid).Tables[0];
                DataTable drawbackByUserName = CallBLL.cz_drawback_six_bll.GetDrawbackByUserName(this.cz_users_model.get_u_name(), this.six_modify_playid);
                List<cz_drawback_six> list = new List<cz_drawback_six>();
                List<drawback_downuser> list2 = new List<drawback_downuser>();
                List<drawback_downuser> list3 = new List<drawback_downuser>();
                foreach (DataRow row in table.Rows)
                {
                    string str = row["play_id"].ToString();
                    string s = row["a_drawback"].ToString();
                    string str3 = row["b_drawback"].ToString();
                    string str4 = row["c_drawback"].ToString();
                    string str5 = row["single_max_amount"].ToString();
                    string str6 = row["single_phase_amount"].ToString();
                    string str7 = row["single_min_amount"].ToString();
                    string str8 = this.paramDict[string.Format("six_a_{0}", str.ToLower())];
                    string str9 = this.paramDict[string.Format("six_b_{0}", str.ToLower())];
                    string str10 = this.paramDict[string.Format("six_c_{0}", str.ToLower())];
                    string str11 = this.paramDict[string.Format("six_max_amount_{0}", str.ToLower())];
                    string str12 = this.paramDict[string.Format("six_phase_amount_{0}", str.ToLower())];
                    string str13 = this.paramDict[string.Format("six_single_min_amount_{0}", str.ToLower())];
                    DataRow[] rowArray = drawbackByUserName.Select(string.Format(" play_id={0} ", str));
                    string str14 = rowArray[0]["a_drawback"].ToString();
                    string str15 = rowArray[0]["b_drawback"].ToString();
                    string str16 = rowArray[0]["c_drawback"].ToString();
                    string str17 = rowArray[0]["single_max_amount"].ToString();
                    string str18 = rowArray[0]["single_phase_amount"].ToString();
                    string str19 = rowArray[0]["single_min_amount"].ToString();
                    if ((((double.Parse(str14) == double.Parse(str8)) && (double.Parse(str15) == double.Parse(str9))) && ((double.Parse(str16) == double.Parse(str10)) && (double.Parse(str17) == double.Parse(str11)))) && ((double.Parse(str18) == double.Parse(str12)) && (double.Parse(str19) == double.Parse(str13))))
                    {
                        continue;
                    }
                    if (string.IsNullOrEmpty(this.cz_users_model.get_six_kind()) || this.cz_users_model.get_six_kind().Equals("0"))
                    {
                        if (double.Parse(str8) > double.Parse(s))
                        {
                            str8 = s;
                        }
                        if (double.Parse(str9) > double.Parse(str3))
                        {
                            str9 = str3;
                        }
                        if (double.Parse(str10) > double.Parse(str4))
                        {
                            str10 = str4;
                        }
                    }
                    else
                    {
                        string str22 = this.cz_users_model.get_six_kind().ToLower();
                        if (str22 != null)
                        {
                            if (!(str22 == "a"))
                            {
                                if (str22 == "b")
                                {
                                    goto Label_03CF;
                                }
                                if (str22 == "c")
                                {
                                    goto Label_03E5;
                                }
                            }
                            else if (double.Parse(str8) > double.Parse(s))
                            {
                                str8 = s;
                            }
                        }
                    }
                    goto Label_03F9;
                Label_03CF:
                    if (double.Parse(str9) > double.Parse(str3))
                    {
                        str9 = str3;
                    }
                    goto Label_03F9;
                Label_03E5:
                    if (double.Parse(str10) > double.Parse(str4))
                    {
                        str10 = str4;
                    }
                Label_03F9:
                    if (double.Parse(str11) > double.Parse(str5))
                    {
                        str11 = str5;
                    }
                    if (double.Parse(str12) > double.Parse(str6))
                    {
                        str12 = str6;
                    }
                    if (double.Parse(str13) < double.Parse(str7))
                    {
                        str13 = str7;
                    }
                    if ((((double.Parse(str14) != double.Parse(str8)) || (double.Parse(str15) != double.Parse(str9))) || ((double.Parse(str16) != double.Parse(str10)) || (double.Parse(str17) != double.Parse(str11)))) || ((double.Parse(str18) != double.Parse(str12)) || (double.Parse(str19) != double.Parse(str13))))
                    {
                        cz_drawback_six item = new cz_drawback_six();
                        item.set_play_id(new int?(Convert.ToInt32(str)));
                        if (!string.IsNullOrEmpty(str8))
                        {
                            item.set_a_drawback(new decimal?(Convert.ToDecimal(str8)));
                        }
                        if (!string.IsNullOrEmpty(str9))
                        {
                            item.set_b_drawback(new decimal?(Convert.ToDecimal(str9)));
                        }
                        if (!string.IsNullOrEmpty(str10))
                        {
                            item.set_c_drawback(new decimal?(Convert.ToDecimal(str10)));
                        }
                        item.set_single_max_amount(new decimal?(Convert.ToDecimal(str11)));
                        item.set_single_phase_amount(new decimal?(Convert.ToDecimal(str12)));
                        item.set_single_min_amount(new decimal?(Convert.ToDecimal(str13)));
                        item.set_u_name(this.cz_users_model.get_u_name());
                        string str20 = "0";
                        if (string.IsNullOrEmpty(this.cz_users_model.get_six_kind()) || this.cz_users_model.get_six_kind().Equals("0"))
                        {
                            str20 = "0";
                        }
                        else
                        {
                            str20 = this.cz_users_model.get_six_kind();
                        }
                        list.Add(item);
                        if (!this.cz_users_model.get_u_type().Equals("hy"))
                        {
                            drawback_downuser _downuser = new drawback_downuser();
                            _downuser.set_updateUserName(this.cz_users_model.get_u_name());
                            _downuser.set_updateUserType(this.cz_users_model.get_u_type());
                            _downuser.set_play_id(str);
                            _downuser.set_opt("dq");
                            _downuser.set_douvalue(double.Parse(item.get_single_phase_amount().ToString()));
                            list2.Add(_downuser);
                            drawback_downuser _downuser2 = new drawback_downuser();
                            _downuser2.set_updateUserName(this.cz_users_model.get_u_name());
                            _downuser2.set_updateUserType(this.cz_users_model.get_u_type());
                            _downuser2.set_play_id(str);
                            _downuser2.set_opt("dz");
                            _downuser2.set_douvalue(double.Parse(item.get_single_max_amount().ToString()));
                            list2.Add(_downuser2);
                            if (str20.ToLower().Equals("a"))
                            {
                                drawback_downuser _downuser3 = new drawback_downuser();
                                _downuser3.set_updateUserName(this.cz_users_model.get_u_name());
                                _downuser3.set_updateUserType(this.cz_users_model.get_u_type());
                                _downuser3.set_play_id(str);
                                _downuser3.set_opt("a");
                                _downuser3.set_douvalue(double.Parse(item.get_a_drawback().ToString()));
                                list2.Add(_downuser3);
                            }
                            else if (str20.ToLower().Equals("b"))
                            {
                                drawback_downuser _downuser4 = new drawback_downuser();
                                _downuser4.set_updateUserName(this.cz_users_model.get_u_name());
                                _downuser4.set_updateUserType(this.cz_users_model.get_u_type());
                                _downuser4.set_play_id(str);
                                _downuser4.set_opt("b");
                                _downuser4.set_douvalue(double.Parse(item.get_b_drawback().ToString()));
                                list2.Add(_downuser4);
                            }
                            else if (str20.ToLower().Equals("c"))
                            {
                                drawback_downuser _downuser5 = new drawback_downuser();
                                _downuser5.set_updateUserName(this.cz_users_model.get_u_name());
                                _downuser5.set_updateUserType(this.cz_users_model.get_u_type());
                                _downuser5.set_play_id(str);
                                _downuser5.set_opt("c");
                                _downuser5.set_douvalue(double.Parse(item.get_c_drawback().ToString()));
                                list2.Add(_downuser5);
                            }
                            else if (str20.Equals("0"))
                            {
                                drawback_downuser _downuser6 = new drawback_downuser();
                                _downuser6.set_updateUserName(this.cz_users_model.get_u_name());
                                _downuser6.set_updateUserType(this.cz_users_model.get_u_type());
                                _downuser6.set_play_id(str);
                                _downuser6.set_opt("a");
                                _downuser6.set_douvalue(double.Parse(item.get_a_drawback().ToString()));
                                list2.Add(_downuser6);
                                drawback_downuser _downuser7 = new drawback_downuser();
                                _downuser7.set_updateUserName(this.cz_users_model.get_u_name());
                                _downuser7.set_updateUserType(this.cz_users_model.get_u_type());
                                _downuser7.set_play_id(str);
                                _downuser7.set_opt("b");
                                _downuser7.set_douvalue(double.Parse(item.get_b_drawback().ToString()));
                                list2.Add(_downuser7);
                                drawback_downuser _downuser8 = new drawback_downuser();
                                _downuser8.set_updateUserName(this.cz_users_model.get_u_name());
                                _downuser8.set_updateUserType(this.cz_users_model.get_u_type());
                                _downuser8.set_play_id(str);
                                _downuser8.set_opt("c");
                                _downuser8.set_douvalue(double.Parse(item.get_c_drawback().ToString()));
                                list2.Add(_downuser8);
                            }
                        }
                        drawback_downuser _downuser9 = new drawback_downuser();
                        _downuser9.set_updateUserName(this.cz_users_model.get_u_name());
                        _downuser9.set_updateUserType(this.cz_users_model.get_u_type());
                        _downuser9.set_play_id(str);
                        _downuser9.set_opt("mindz");
                        _downuser9.set_douvalue(double.Parse(item.get_single_min_amount().ToString()));
                        list3.Add(_downuser9);
                    }
                }
                string str21 = "0";
                if (string.IsNullOrEmpty(this.cz_users_model.get_six_kind()) || this.cz_users_model.get_six_kind().Equals("0"))
                {
                    str21 = "0";
                }
                else
                {
                    str21 = this.cz_users_model.get_six_kind();
                }
                if (!CallBLL.cz_drawback_six_bll.UpdateDataList(list, str21, flag4, list2, list3))
                {
                    mess = string.Format("⑥合彩：修改【{0}】退水出現異常，請重試!", LSKeys.get_SIX_Name());
                    isSuccess = false;
                }
                else
                {
                    base.user_drawback_six_log(drawbackByUserName, CallBLL.cz_drawback_six_bll.GetDrawbackByUserName(this.cz_users_model.get_u_name(), this.six_modify_playid), this.cz_users_model.get_u_name());
                    if ((flag && flag2) && flag3)
                    {
                        mess = "⑥合彩：因獎期正在開盤中且下级已经有下单[退水]未能修改,單期單注修改成功!";
                    }
                    else
                    {
                        mess = "⑥合彩：已經成功修改設置(注:如開盤中且下级已经有下单退水不能修改)!";
                    }
                }
            }
        }

        private void ValidKC(string lottoryStr, string lottoryID, DataTable table, string modifyPlayids)
        {
            foreach (DataRow row in table.Select(string.Format(" play_id in ({0})", modifyPlayids)))
            {
                string str = row["play_id"].ToString();
                string str2 = this.paramDict[string.Format("{0}_a_{1}", lottoryStr, str)];
                string str3 = this.paramDict[string.Format("{0}_b_{1}", lottoryStr, str)];
                string str4 = this.paramDict[string.Format("{0}_c_{1}", lottoryStr, str)];
                string str5 = this.paramDict[string.Format("{0}_max_amount_{1}", lottoryStr, str)];
                string str6 = this.paramDict[string.Format("{0}_phase_amount_{1}", lottoryStr, str)];
                string str7 = this.paramDict[string.Format("{0}_single_min_amount_{1}", lottoryStr, str)];
                if (this.cz_users_model.get_kc_kind().ToLower().Equals("a"))
                {
                    if (string.IsNullOrEmpty(str2))
                    {
                        base.Un_User_Lock(this.rateKCModel.get_fgs_name());
                        base.noRightOptMsg(base.GetGameNameByID(lottoryID) + "：A盤退水不能為空！");
                    }
                    else
                    {
                        try
                        {
                            if (Convert.ToDouble(str2) < 0.0)
                            {
                                base.Un_User_Lock(this.rateKCModel.get_fgs_name());
                                base.noRightOptMsg(base.GetGameNameByID(lottoryID) + "：A盤退水輸入值不能小於0！");
                            }
                        }
                        catch
                        {
                            base.Un_User_Lock(this.rateKCModel.get_fgs_name());
                            base.noRightOptMsg(base.GetGameNameByID(lottoryID) + "：A盤退水輸入值錯誤！");
                        }
                    }
                }
                else if (this.cz_users_model.get_kc_kind().ToLower().Equals("b"))
                {
                    if (string.IsNullOrEmpty(str3))
                    {
                        base.Un_User_Lock(this.rateKCModel.get_fgs_name());
                        base.noRightOptMsg(base.GetGameNameByID(lottoryID) + "：B盤退水不能為空！");
                    }
                    else
                    {
                        try
                        {
                            if (Convert.ToDouble(str3) < 0.0)
                            {
                                base.Un_User_Lock(this.rateKCModel.get_fgs_name());
                                base.noRightOptMsg(base.GetGameNameByID(lottoryID) + "：B盤退水輸入值不能小於！");
                            }
                        }
                        catch
                        {
                            base.Un_User_Lock(this.rateKCModel.get_fgs_name());
                            base.noRightOptMsg(base.GetGameNameByID(lottoryID) + "：B盤退水輸入值錯誤！");
                        }
                    }
                }
                else if (this.cz_users_model.get_kc_kind().ToLower().Equals("c"))
                {
                    if (string.IsNullOrEmpty(str4))
                    {
                        base.Un_User_Lock(this.rateKCModel.get_fgs_name());
                        base.noRightOptMsg(base.GetGameNameByID(lottoryID) + "：C盤退水不能為空！");
                    }
                    else
                    {
                        try
                        {
                            if (Convert.ToDouble(str4) < 0.0)
                            {
                                base.Un_User_Lock(this.rateKCModel.get_fgs_name());
                                base.noRightOptMsg(base.GetGameNameByID(lottoryID) + "：C盤退水輸入值不能小於0！");
                            }
                        }
                        catch
                        {
                            base.Un_User_Lock(this.rateKCModel.get_fgs_name());
                            base.noRightOptMsg(base.GetGameNameByID(lottoryID) + "：C盤退水輸入值錯誤！");
                        }
                    }
                }
                else if (string.IsNullOrEmpty(str2))
                {
                    base.Un_User_Lock(this.rateKCModel.get_fgs_name());
                    base.noRightOptMsg(base.GetGameNameByID(lottoryID) + "：A盤退水不能為空！");
                }
                else if (string.IsNullOrEmpty(str3))
                {
                    base.Un_User_Lock(this.rateKCModel.get_fgs_name());
                    base.noRightOptMsg(base.GetGameNameByID(lottoryID) + "：B盤退水不能為空！");
                }
                else if (string.IsNullOrEmpty(str4))
                {
                    base.Un_User_Lock(this.rateKCModel.get_fgs_name());
                    base.noRightOptMsg(base.GetGameNameByID(lottoryID) + "：C盤退水不能為空！");
                }
                else
                {
                    try
                    {
                        if (Convert.ToDouble(str2) < 0.0)
                        {
                            base.Un_User_Lock(this.rateKCModel.get_fgs_name());
                            base.noRightOptMsg(base.GetGameNameByID(lottoryID) + "：A盤退水輸入值不能小於0！");
                        }
                        else if (Convert.ToDouble(str3) < 0.0)
                        {
                            base.Un_User_Lock(this.rateKCModel.get_fgs_name());
                            base.noRightOptMsg(base.GetGameNameByID(lottoryID) + "：B盤退水輸入值不能小於0！");
                        }
                        else if (Convert.ToDouble(str4) < 0.0)
                        {
                            base.Un_User_Lock(this.rateKCModel.get_fgs_name());
                            base.noRightOptMsg(base.GetGameNameByID(lottoryID) + "：C盤退水輸入值不能小於0！");
                        }
                    }
                    catch
                    {
                        base.Un_User_Lock(this.rateKCModel.get_fgs_name());
                        base.noRightOptMsg(base.GetGameNameByID(lottoryID) + "：退水輸入值錯誤！");
                    }
                }
                if (string.IsNullOrEmpty(str5))
                {
                    base.Un_User_Lock(this.rateKCModel.get_fgs_name());
                    base.noRightOptMsg(base.GetGameNameByID(lottoryID) + "：單注最大限額不能為空！");
                }
                else if (!Utils.IsInteger(str5))
                {
                    base.Un_User_Lock(this.rateKCModel.get_fgs_name());
                    base.noRightOptMsg(base.GetGameNameByID(lottoryID) + "：單注最大限額只能為整數！");
                }
                if (string.IsNullOrEmpty(str6))
                {
                    base.Un_User_Lock(this.rateKCModel.get_fgs_name());
                    base.noRightOptMsg(base.GetGameNameByID(lottoryID) + "：單期最大限額不能為空！");
                }
                else if (!Utils.IsInteger(str6))
                {
                    base.Un_User_Lock(this.rateKCModel.get_fgs_name());
                    base.noRightOptMsg(base.GetGameNameByID(lottoryID) + "：單期最大限額只能為整數！");
                }
                if (string.IsNullOrEmpty(str7))
                {
                    base.Un_User_Lock(this.rateKCModel.get_fgs_name());
                    base.noRightOptMsg(base.GetGameNameByID(lottoryID) + "：最小單注不能為空！");
                }
                else if (!Utils.IsInteger(str7))
                {
                    base.Un_User_Lock(this.rateKCModel.get_fgs_name());
                    base.noRightOptMsg(base.GetGameNameByID(lottoryID) + "：最小單注只能為整數！");
                }
            }
        }

        private void ValidSix()
        {
            if (!string.IsNullOrEmpty(this.six_modify_playid))
            {
                foreach (DataRow row in this.table_six.Select(string.Format(" play_id in({0}) ", this.six_modify_playid)))
                {
                    string str = row["play_id"].ToString();
                    string str2 = this.paramDict[string.Format("six_a_{0}", str.ToLower())];
                    string str3 = this.paramDict[string.Format("six_b_{0}", str.ToLower())];
                    string str4 = this.paramDict[string.Format("six_c_{0}", str.ToLower())];
                    string str5 = this.paramDict[string.Format("six_max_amount_{0}", str.ToLower())];
                    string str6 = this.paramDict[string.Format("six_phase_amount_{0}", str.ToLower())];
                    string str7 = this.paramDict[string.Format("six_single_min_amount_{0}", str.ToLower())];
                    if (this.cz_users_model.get_six_kind().ToLower().Equals("a"))
                    {
                        if (string.IsNullOrEmpty(str2))
                        {
                            base.Un_User_Lock(this.rateKCModel.get_fgs_name());
                            base.noRightOptMsg("⑥合彩：A盤退水不能為空！");
                        }
                        else
                        {
                            try
                            {
                                if (Convert.ToDouble(str2) < 0.0)
                                {
                                    base.Un_User_Lock(this.rateKCModel.get_fgs_name());
                                    base.noRightOptMsg("⑥合彩：A盤退水輸入值不能小於0！");
                                }
                            }
                            catch
                            {
                                base.Un_User_Lock(this.rateKCModel.get_fgs_name());
                                base.noRightOptMsg("⑥合彩：A盤退水輸入值錯誤！");
                            }
                        }
                    }
                    else if (this.cz_users_model.get_six_kind().ToLower().Equals("b"))
                    {
                        if (string.IsNullOrEmpty(str3))
                        {
                            base.Un_User_Lock(this.rateKCModel.get_fgs_name());
                            base.noRightOptMsg("⑥合彩：B盤退水不能為空！");
                        }
                        else
                        {
                            try
                            {
                                if (Convert.ToDouble(str3) < 0.0)
                                {
                                    base.Un_User_Lock(this.rateKCModel.get_fgs_name());
                                    base.noRightOptMsg("⑥合彩：B盤退水輸入值不能小於0！");
                                }
                            }
                            catch
                            {
                                base.Un_User_Lock(this.rateKCModel.get_fgs_name());
                                base.noRightOptMsg("⑥合彩：B盤退水輸入值錯誤！");
                            }
                        }
                    }
                    else if (this.cz_users_model.get_six_kind().ToLower().Equals("c"))
                    {
                        if (string.IsNullOrEmpty(str4))
                        {
                            base.Un_User_Lock(this.rateKCModel.get_fgs_name());
                            base.noRightOptMsg("⑥合彩：C盤退水不能為空！");
                        }
                        else
                        {
                            try
                            {
                                if (Convert.ToDouble(str4) < 0.0)
                                {
                                    base.Un_User_Lock(this.rateKCModel.get_fgs_name());
                                    base.noRightOptMsg("⑥合彩：C盤退水輸入值不能小於0！");
                                }
                            }
                            catch
                            {
                                base.Un_User_Lock(this.rateKCModel.get_fgs_name());
                                base.noRightOptMsg("⑥合彩：C盤退水輸入值錯誤！");
                            }
                        }
                    }
                    else if (string.IsNullOrEmpty(str2))
                    {
                        base.Un_User_Lock(this.rateKCModel.get_fgs_name());
                        base.noRightOptMsg("⑥合彩：A盤退水不能為空！");
                    }
                    else if (string.IsNullOrEmpty(str3))
                    {
                        base.Un_User_Lock(this.rateKCModel.get_fgs_name());
                        base.noRightOptMsg("⑥合彩：B盤退水不能為空！");
                    }
                    else if (string.IsNullOrEmpty(str4))
                    {
                        base.Un_User_Lock(this.rateKCModel.get_fgs_name());
                        base.noRightOptMsg("⑥合彩：C盤退水不能為空！");
                    }
                    else
                    {
                        try
                        {
                            if (Convert.ToDouble(str2) < 0.0)
                            {
                                base.Un_User_Lock(this.rateKCModel.get_fgs_name());
                                base.noRightOptMsg("⑥合彩：A盤退水輸入值不能小於0！");
                            }
                            if (Convert.ToDouble(str3) < 0.0)
                            {
                                base.Un_User_Lock(this.rateKCModel.get_fgs_name());
                                base.noRightOptMsg("⑥合彩：B盤退水輸入值不能小於0！");
                            }
                            if (Convert.ToDouble(str4) < 0.0)
                            {
                                base.Un_User_Lock(this.rateKCModel.get_fgs_name());
                                base.noRightOptMsg("⑥合彩：C盤退水輸入值不能小於0！");
                            }
                        }
                        catch
                        {
                            base.Un_User_Lock(this.rateKCModel.get_fgs_name());
                            base.noRightOptMsg("⑥合彩：退水輸入值錯誤！");
                        }
                    }
                    if (string.IsNullOrEmpty(str5))
                    {
                        base.Un_User_Lock(this.rateKCModel.get_fgs_name());
                        base.noRightOptMsg("⑥合彩：單注最大限額不能為空！");
                    }
                    else if (!Utils.IsInteger(str5))
                    {
                        base.Un_User_Lock(this.rateKCModel.get_fgs_name());
                        base.noRightOptMsg("⑥合彩：單注最大限額只能為整數！");
                    }
                    if (string.IsNullOrEmpty(str6))
                    {
                        base.Un_User_Lock(this.rateKCModel.get_fgs_name());
                        base.noRightOptMsg("⑥合彩：單期最大限額不能為空！");
                    }
                    else if (!Utils.IsInteger(str6))
                    {
                        base.Un_User_Lock(this.rateKCModel.get_fgs_name());
                        base.noRightOptMsg("⑥合彩：單期最大限額只能為整數！");
                    }
                    if (string.IsNullOrEmpty(str7))
                    {
                        base.Un_User_Lock(this.rateKCModel.get_fgs_name());
                        base.noRightOptMsg("⑥合彩：最小單注不能為空！");
                    }
                    else if (!Utils.IsInteger(str7))
                    {
                        base.Un_User_Lock(this.rateKCModel.get_fgs_name());
                        base.noRightOptMsg("⑥合彩：最小單注只能為整數！");
                    }
                }
            }
        }
    }
}

