namespace Agent.Web.Account
{
    using Agent.Web.WebBase;
    using LotterySystem.Common;
    using LotterySystem.Model;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class drawback : MemberPageBase
    {
        protected string car168_modify_playid = "";
        protected string cqsc_modify_playid = "";
        protected cz_users cz_users_model;
        private Dictionary<string, Dictionary<string, string>> DICT = new Dictionary<string, Dictionary<string, string>>();
        protected string drawBackjson = "";
        protected string happycar_modify_playid = "";
        protected bool isAllowUpdate_kc;
        protected bool isAllowUpdate_six;
        protected string jscar_modify_playid = "";
        protected string jscqsc_modify_playid = "";
        protected string jsft2_modify_playid = "";
        protected string jsk3_modify_playid = "";
        protected string jspk10_modify_playid = "";
        protected string jssfc_modify_playid = "";
        protected string k8sc_modify_playid = "";
        protected string kl10_modify_playid = "";
        protected string kl8_modify_playid = "";
        protected string lm_max_amount = "";
        protected string lm_phase_amount = "";
        protected string lm_pk_a = "";
        protected string lm_pk_b = "";
        protected string lm_pk_c = "";
        protected string lm_single_min_amount = "";
        protected string lmp_max_amount = "";
        protected string lmp_phase_amount = "";
        protected string lmp_pk_a = "";
        protected string lmp_pk_b = "";
        protected string lmp_pk_c = "";
        protected string lmp_single_min_amount = "";
        private DataTable lotteryDT;
        protected string lottrty_kc = "";
        protected string lottrty_six = "";
        protected string namestr = "";
        protected string newAdd = "";
        protected string pcdd_modify_playid = "";
        protected string pk_kc = "";
        protected string pk_kc_a = "";
        protected string pk_kc_b = "";
        protected string pk_kc_c = "";
        protected string pk_six = "";
        protected string pk_six_a = "";
        protected string pk_six_b = "";
        protected string pk_six_c = "";
        protected string pk10_modify_playid = "";
        protected string pkbjl_modify_playid = "";
        private cz_rate_kc rateKCModel;
        protected string shortcut_blue = "display:none;";
        protected string shortcut_car168_lmp = "2,3,4,37,38,36";
        protected string shortcut_car168_tm = "1";
        protected string shortcut_cqsc_lmp = "2,3,16,17,18,19,20,21";
        protected string shortcut_cqsc_tm = "1";
        protected string shortcut_green = "display:none;";
        protected string shortcut_happycar_lmp = "2,3,4,37,38,36";
        protected string shortcut_happycar_tm = "1";
        protected string shortcut_jscar_lmp = "2,3,4,37,38,36";
        protected string shortcut_jscar_tm = "1";
        protected string shortcut_jscqsc_lmp = "2,3,16,17,18,19,20,21";
        protected string shortcut_jscqsc_tm = "1";
        protected string shortcut_jsft2_lmp = "2,3,4,37,38,36";
        protected string shortcut_jsft2_tm = "1";
        protected string shortcut_jsk3_lmp = "58,59,60,61,62,63,64";
        protected string shortcut_jspk10_lmp = "2,3,4,37,38,36";
        protected string shortcut_jspk10_tm = "1";
        protected string shortcut_jssfc_lm = "72,73,74,75,76,77,78,79";
        protected string shortcut_jssfc_lmp = "82,83,84,85,121,122,11,12,13,80";
        protected string shortcut_jssfc_tm = "81";
        protected string shortcut_k8sc_lmp = "2,3,16,17,18,19,20,21";
        protected string shortcut_k8sc_tm = "1";
        protected string shortcut_kl10_lm = "72,73,74,75,76,77,78,79";
        protected string shortcut_kl10_lmp = "82,83,84,85,121,122,11,12,13,80";
        protected string shortcut_kl10_tm = "81";
        protected string shortcut_kl8_lmp = "65,66,67,68,69,70,71,72";
        protected string shortcut_pcdd_lm = "71014";
        protected string shortcut_pcdd_lmp = "71002,71003,71004,71005,71007,71008,71013";
        protected string shortcut_pcdd_tm = "71001,71006";
        protected string shortcut_pk10_lmp = "2,3,4,37,38,36";
        protected string shortcut_pk10_tm = "1";
        protected string shortcut_pkbjl_lmp = "81001,81002,81003,81004";
        protected string shortcut_speed5_lmp = "2,3,16,17,18,19,20,21";
        protected string shortcut_speed5_tm = "1";
        protected string shortcut_ssc168_lmp = "2,3,16,17,18,19,20,21";
        protected string shortcut_ssc168_tm = "1";
        protected string shortcut_violet = "display:none;";
        protected string shortcut_vrcar_lmp = "2,3,4,37,38,36";
        protected string shortcut_vrcar_tm = "1";
        protected string shortcut_vrssc_lmp = "2,3,16,17,18,19,20,21";
        protected string shortcut_vrssc_tm = "1";
        protected string shortcut_xyft5_lmp = "2,3,4,37,38,36";
        protected string shortcut_xyft5_tm = "1";
        protected string shortcut_xyftoa_lmp = "2,3,4,37,38,36";
        protected string shortcut_xyftoa_tm = "1";
        protected string shortcut_xyftsg_lmp = "2,3,4,37,38,36";
        protected string shortcut_xyftsg_tm = "1";
        protected string shortcut_xync_lm = "72,73,74,75,78,79";
        protected string shortcut_xync_lmp = "82,83,84,85,121,122,11,12,13,80";
        protected string shortcut_xync_tm = "81";
        protected string six_lm_max_amount = "";
        protected string six_lm_phase_amount = "";
        protected string six_lm_pk_a = "";
        protected string six_lm_pk_b = "";
        protected string six_lm_pk_c = "";
        protected string six_lm_single_min_amount = "";
        protected string six_lmp_max_amount = "";
        protected string six_lmp_phase_amount = "";
        protected string six_lmp_pk_a = "";
        protected string six_lmp_pk_b = "";
        protected string six_lmp_pk_c = "";
        protected string six_lmp_single_min_amount = "";
        protected string six_modify_playid = "";
        protected string six_shortcut_blue = "display:none;";
        protected string six_shortcut_green = "display:none;";
        protected string six_shortcut_lm = "91016,91017,91018,91019,91020,91030,91031,91032,91033,91034,91035,91036,91037,91040,91047,91048,91049,91050,91051,91058,91059,91060,91061,91062,91063,91064,91065";
        protected string six_shortcut_lmp = "91003,91004,91005,91011,91012,91013,91014,91023,91024,91038,91041,91042,91043,91044,91045,91046";
        protected string six_shortcut_tm = "91001,91002,91006,91007,91008,91057,91009,91010,91015,91021,91022,91025,91026,91027,91028,91029,91039,91052,91053,91054,91055,91056";
        protected string six_shortcut_violet = "display:none;";
        protected string six_tm_max_amount = "";
        protected string six_tm_phase_amount = "";
        protected string six_tm_pk_a = "";
        protected string six_tm_pk_b = "";
        protected string six_tm_pk_c = "";
        protected string six_tm_single_min_amount = "";
        protected string speed5_modify_playid = "";
        protected string ssc168_modify_playid = "";
        protected string string_car168 = "";
        protected string string_cqsc = "";
        protected string string_happycar = "";
        protected string string_jscar = "";
        protected string string_jscqsc = "";
        protected string string_jsft2 = "";
        protected string string_jsk3 = "";
        protected string string_jspk10 = "";
        protected string string_jssfc = "";
        protected string string_k8sc = "";
        protected string string_kl10 = "";
        protected string string_kl8 = "";
        protected string string_pcdd = "";
        protected string string_pk10 = "";
        protected string string_pkbjl = "";
        protected string string_six = "";
        protected string string_speed5 = "";
        protected string string_ssc168 = "";
        protected string string_vrcar = "";
        protected string string_vrssc = "";
        protected string string_xyft5 = "";
        protected string string_xyftoa = "";
        protected string string_xyftsg = "";
        protected string string_xync = "";
        protected DataTable table_car168;
        protected DataTable table_cqsc;
        protected DataTable table_happycar;
        protected DataTable table_jscar;
        protected DataTable table_jscqsc;
        protected DataTable table_jsft2;
        protected DataTable table_jsk3;
        protected DataTable table_jspk10;
        protected DataTable table_jssfc;
        protected DataTable table_k8sc;
        protected DataTable table_kl10;
        protected DataTable table_kl8;
        protected DataTable table_pcdd;
        protected DataTable table_pk10;
        protected DataTable table_pkbjl;
        protected DataTable table_six;
        protected DataTable table_speed5;
        protected DataTable table_ssc168;
        protected DataTable table_vrcar;
        protected DataTable table_vrssc;
        protected DataTable table_xyft5;
        protected DataTable table_xyftoa;
        protected DataTable table_xyftsg;
        protected DataTable table_xync;
        protected string tabState_kc = "";
        protected string tabState_six = "";
        protected string tm_max_amount = "";
        protected string tm_phase_amount = "";
        protected string tm_pk_a = "";
        protected string tm_pk_b = "";
        protected string tm_pk_c = "";
        protected string tm_single_min_amount = "";
        protected string userid = "";
        protected agent_userinfo_session userModel;
        protected string vrcar_modify_playid = "";
        protected string vrssc_modify_playid = "";
        protected string xyft5_modify_playid = "";
        protected string xyftoa_modify_playid = "";
        protected string xyftsg_modify_playid = "";
        protected string xync_modify_playid = "";

        protected string GetLabelBg(string play_id)
        {
            string str = "";
            if (this.six_shortcut_tm.IndexOf(play_id) > -1)
            {
                return "<label class=\"lBg1\"></label>";
            }
            if (this.six_shortcut_lmp.IndexOf(play_id) > -1)
            {
                return "<label class=\"lBg2\"></label>";
            }
            if (this.six_shortcut_lm.IndexOf(play_id) > -1)
            {
                str = "<label class=\"lBg3\"></label>";
            }
            return str;
        }

        private void GetModifyPlays(string nameStr)
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
            this.jscqsc_modify_playid = "";
            this.jspk10_modify_playid = "";
            this.jssfc_modify_playid = "";
            this.jsft2_modify_playid = "";
            this.car168_modify_playid = "";
            this.ssc168_modify_playid = "";
            this.vrcar_modify_playid = "";
            this.vrssc_modify_playid = "";
            this.xyftoa_modify_playid = "";
            this.xyftsg_modify_playid = "";
            this.happycar_modify_playid = "";
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
                            goto Label_0398;
                        }
                        this.kl10_modify_playid = this.kl10_modify_playid + str2;
                        continue;
                    }
                    case "cqsc":
                    {
                        if (!string.IsNullOrEmpty(this.cqsc_modify_playid))
                        {
                            goto Label_03D8;
                        }
                        this.cqsc_modify_playid = this.cqsc_modify_playid + str2;
                        continue;
                    }
                    case "pk10":
                    {
                        if (!string.IsNullOrEmpty(this.pk10_modify_playid))
                        {
                            goto Label_0418;
                        }
                        this.pk10_modify_playid = this.pk10_modify_playid + str2;
                        continue;
                    }
                    case "xync":
                    {
                        if (!string.IsNullOrEmpty(this.xync_modify_playid))
                        {
                            goto Label_0458;
                        }
                        this.xync_modify_playid = this.xync_modify_playid + str2;
                        continue;
                    }
                    case "jsk3":
                    {
                        if (!string.IsNullOrEmpty(this.jsk3_modify_playid))
                        {
                            goto Label_0498;
                        }
                        this.jsk3_modify_playid = this.jsk3_modify_playid + str2;
                        continue;
                    }
                    case "kl8":
                    {
                        if (!string.IsNullOrEmpty(this.kl8_modify_playid))
                        {
                            goto Label_04D8;
                        }
                        this.kl8_modify_playid = this.kl8_modify_playid + str2;
                        continue;
                    }
                    case "k8sc":
                    {
                        if (!string.IsNullOrEmpty(this.k8sc_modify_playid))
                        {
                            goto Label_0518;
                        }
                        this.k8sc_modify_playid = this.k8sc_modify_playid + str2;
                        continue;
                    }
                    case "pcdd":
                    {
                        if (!string.IsNullOrEmpty(this.pcdd_modify_playid))
                        {
                            goto Label_0558;
                        }
                        this.pcdd_modify_playid = this.pcdd_modify_playid + str2;
                        continue;
                    }
                    case "xyft5":
                    {
                        if (!string.IsNullOrEmpty(this.xyft5_modify_playid))
                        {
                            goto Label_0598;
                        }
                        this.xyft5_modify_playid = this.xyft5_modify_playid + str2;
                        continue;
                    }
                    case "pkbjl":
                    {
                        if (!string.IsNullOrEmpty(this.pkbjl_modify_playid))
                        {
                            goto Label_05D8;
                        }
                        this.pkbjl_modify_playid = this.pkbjl_modify_playid + str2;
                        continue;
                    }
                    case "jscar":
                    {
                        if (!string.IsNullOrEmpty(this.jscar_modify_playid))
                        {
                            goto Label_0618;
                        }
                        this.jscar_modify_playid = this.jscar_modify_playid + str2;
                        continue;
                    }
                    case "speed5":
                    {
                        if (!string.IsNullOrEmpty(this.speed5_modify_playid))
                        {
                            goto Label_0658;
                        }
                        this.speed5_modify_playid = this.speed5_modify_playid + str2;
                        continue;
                    }
                    case "jscqsc":
                    {
                        if (!string.IsNullOrEmpty(this.jscqsc_modify_playid))
                        {
                            goto Label_0698;
                        }
                        this.jscqsc_modify_playid = this.jscqsc_modify_playid + str2;
                        continue;
                    }
                    case "jspk10":
                    {
                        if (!string.IsNullOrEmpty(this.jspk10_modify_playid))
                        {
                            goto Label_06D8;
                        }
                        this.jspk10_modify_playid = this.jspk10_modify_playid + str2;
                        continue;
                    }
                    case "jssfc":
                    {
                        if (!string.IsNullOrEmpty(this.jssfc_modify_playid))
                        {
                            goto Label_0718;
                        }
                        this.jssfc_modify_playid = this.jssfc_modify_playid + str2;
                        continue;
                    }
                    case "jsft2":
                    {
                        if (!string.IsNullOrEmpty(this.jsft2_modify_playid))
                        {
                            goto Label_0758;
                        }
                        this.jsft2_modify_playid = this.jsft2_modify_playid + str2;
                        continue;
                    }
                    case "car168":
                    {
                        if (!string.IsNullOrEmpty(this.car168_modify_playid))
                        {
                            goto Label_0798;
                        }
                        this.car168_modify_playid = this.car168_modify_playid + str2;
                        continue;
                    }
                    case "ssc168":
                    {
                        if (!string.IsNullOrEmpty(this.ssc168_modify_playid))
                        {
                            goto Label_07D8;
                        }
                        this.ssc168_modify_playid = this.ssc168_modify_playid + str2;
                        continue;
                    }
                    case "vrcar":
                    {
                        if (!string.IsNullOrEmpty(this.vrcar_modify_playid))
                        {
                            goto Label_0818;
                        }
                        this.vrcar_modify_playid = this.vrcar_modify_playid + str2;
                        continue;
                    }
                    case "vrssc":
                    {
                        if (!string.IsNullOrEmpty(this.vrssc_modify_playid))
                        {
                            goto Label_0858;
                        }
                        this.vrssc_modify_playid = this.vrssc_modify_playid + str2;
                        continue;
                    }
                    case "xyftoa":
                    {
                        if (!string.IsNullOrEmpty(this.xyftoa_modify_playid))
                        {
                            goto Label_0898;
                        }
                        this.xyftoa_modify_playid = this.xyftoa_modify_playid + str2;
                        continue;
                    }
                    case "xyftsg":
                    {
                        if (!string.IsNullOrEmpty(this.xyftsg_modify_playid))
                        {
                            goto Label_08D2;
                        }
                        this.xyftsg_modify_playid = this.xyftsg_modify_playid + str2;
                        continue;
                    }
                    case "happycar":
                    {
                        if (!string.IsNullOrEmpty(this.happycar_modify_playid))
                        {
                            goto Label_090C;
                        }
                        this.happycar_modify_playid = this.happycar_modify_playid + str2;
                        continue;
                    }
                    default:
                    {
                        continue;
                    }
                }
                this.six_modify_playid = this.six_modify_playid + "," + str2;
                continue;
            Label_0398:
                this.kl10_modify_playid = this.kl10_modify_playid + "," + str2;
                continue;
            Label_03D8:
                this.cqsc_modify_playid = this.cqsc_modify_playid + "," + str2;
                continue;
            Label_0418:
                this.pk10_modify_playid = this.pk10_modify_playid + "," + str2;
                continue;
            Label_0458:
                this.xync_modify_playid = this.xync_modify_playid + "," + str2;
                continue;
            Label_0498:
                this.jsk3_modify_playid = this.jsk3_modify_playid + "," + str2;
                continue;
            Label_04D8:
                this.kl8_modify_playid = this.kl8_modify_playid + "," + str2;
                continue;
            Label_0518:
                this.k8sc_modify_playid = this.k8sc_modify_playid + "," + str2;
                continue;
            Label_0558:
                this.pcdd_modify_playid = this.pcdd_modify_playid + "," + str2;
                continue;
            Label_0598:
                this.xyft5_modify_playid = this.xyft5_modify_playid + "," + str2;
                continue;
            Label_05D8:
                this.pkbjl_modify_playid = this.pkbjl_modify_playid + "," + str2;
                continue;
            Label_0618:
                this.jscar_modify_playid = this.jscar_modify_playid + "," + str2;
                continue;
            Label_0658:
                this.speed5_modify_playid = this.speed5_modify_playid + "," + str2;
                continue;
            Label_0698:
                this.jscqsc_modify_playid = this.jscqsc_modify_playid + "," + str2;
                continue;
            Label_06D8:
                this.jspk10_modify_playid = this.jspk10_modify_playid + "," + str2;
                continue;
            Label_0718:
                this.jssfc_modify_playid = this.jssfc_modify_playid + "," + str2;
                continue;
            Label_0758:
                this.jsft2_modify_playid = this.jsft2_modify_playid + "," + str2;
                continue;
            Label_0798:
                this.car168_modify_playid = this.car168_modify_playid + "," + str2;
                continue;
            Label_07D8:
                this.ssc168_modify_playid = this.ssc168_modify_playid + "," + str2;
                continue;
            Label_0818:
                this.vrcar_modify_playid = this.vrcar_modify_playid + "," + str2;
                continue;
            Label_0858:
                this.vrssc_modify_playid = this.vrssc_modify_playid + "," + str2;
                continue;
            Label_0898:
                this.xyftoa_modify_playid = this.xyftoa_modify_playid + "," + str2;
                continue;
            Label_08D2:
                this.xyftsg_modify_playid = this.xyftsg_modify_playid + "," + str2;
                continue;
            Label_090C:
                this.happycar_modify_playid = this.happycar_modify_playid + "," + str2;
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
                DataSet drawBackList = null;
                if (!FileCacheHelper.get_IsShowLM_B())
                {
                    drawBackList = CallBLL.cz_drawback_six_bll.User_GetDrawBackList(this.cz_users_model.get_u_name(), "91060,91061,91062,91063,91064,91065");
                }
                else
                {
                    drawBackList = CallBLL.cz_drawback_six_bll.GetDrawBackList(this.cz_users_model.get_u_name());
                }
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
                    case 12:
                    {
                        DataSet set15 = CallBLL.cz_drawback_jspk10_bll.GetDrawbackList(this.cz_users_model.get_u_name(), "1,2,3,4,36,37,38");
                        if (((set15 != null) && (set15.Tables.Count > 0)) && (set15.Tables[0].Rows.Count > 0))
                        {
                            this.table_jspk10 = set15.Tables[0];
                            DataTable table15 = CallBLL.cz_drawback_jspk10_bll.GetDrawbackList(this.cz_users_model.get_sup_name(), "1,2,3,4,36,37,38").Tables[0];
                            this.DICT.Add("jspk10", this.GetUpDrawbackSix(table15, "jspk10", this.cz_users_model.get_kc_kind()));
                            this.string_jspk10 = "y";
                        }
                        break;
                    }
                    case 13:
                    {
                        DataSet set14 = CallBLL.cz_drawback_jscqsc_bll.GetDrawbackList(this.cz_users_model.get_u_name(), "4,5,6,7,8,9,10,11,12,13,14,15");
                        if (((set14 != null) && (set14.Tables.Count > 0)) && (set14.Tables[0].Rows.Count > 0))
                        {
                            this.table_jscqsc = set14.Tables[0];
                            DataTable table14 = CallBLL.cz_drawback_jscqsc_bll.GetDrawbackList(this.cz_users_model.get_sup_name(), "4,5,6,7,8,9,10,11,12,13,14,15").Tables[0];
                            this.DICT.Add("jscqsc", this.GetUpDrawbackSix(table14, "jscqsc", this.cz_users_model.get_kc_kind()));
                            this.string_jscqsc = "y";
                        }
                        break;
                    }
                    case 14:
                    {
                        DataSet set16 = CallBLL.cz_drawback_jssfc_bll.GetDrawbackByPlayId(this.cz_users_model.get_u_name(), "86,87,88,89,90,91,92,93,94,95,96,97,98,99,100,101,102,103,104,105,106,107,108,109,110,111,112,113,114,115,116,117,118,119,120,123,124,125,126,127,128,129,130,131,132,133,134,135,136");
                        if (((set16 != null) && (set16.Tables.Count > 0)) && (set16.Tables[0].Rows.Count > 0))
                        {
                            this.table_jssfc = set16.Tables[0];
                            DataTable table16 = CallBLL.cz_drawback_jssfc_bll.GetDrawbackByPlayId(this.cz_users_model.get_sup_name(), "86,87,88,89,90,91,92,93,94,95,96,97,98,99,100,101,102,103,104,105,106,107,108,109,110,111,112,113,114,115,116,117,118,119,120,123,124,125,126,127,128,129,130,131,132,133,134,135,136").Tables[0];
                            this.DICT.Add("jssfc", this.GetUpDrawbackSix(table16, "jssfc", this.cz_users_model.get_kc_kind()));
                            this.string_jssfc = "y";
                        }
                        break;
                    }
                    case 15:
                    {
                        DataSet set17 = CallBLL.cz_drawback_jsft2_bll.GetDrawbackList(this.cz_users_model.get_u_name(), "1,2,3,4,36,37,38");
                        if (((set17 != null) && (set17.Tables.Count > 0)) && (set17.Tables[0].Rows.Count > 0))
                        {
                            this.table_jsft2 = set17.Tables[0];
                            DataTable table17 = CallBLL.cz_drawback_jsft2_bll.GetDrawbackList(this.cz_users_model.get_sup_name(), "1,2,3,4,36,37,38").Tables[0];
                            this.DICT.Add("jsft2", this.GetUpDrawbackSix(table17, "jsft2", this.cz_users_model.get_kc_kind()));
                            this.string_jsft2 = "y";
                        }
                        break;
                    }
                    case 0x10:
                    {
                        DataSet set18 = CallBLL.cz_drawback_car168_bll.GetDrawbackList(this.cz_users_model.get_u_name(), "1,2,3,4,36,37,38");
                        if (((set18 != null) && (set18.Tables.Count > 0)) && (set18.Tables[0].Rows.Count > 0))
                        {
                            this.table_car168 = set18.Tables[0];
                            DataTable table18 = CallBLL.cz_drawback_car168_bll.GetDrawbackList(this.cz_users_model.get_sup_name(), "1,2,3,4,36,37,38").Tables[0];
                            this.DICT.Add("car168", this.GetUpDrawbackSix(table18, "car168", this.cz_users_model.get_kc_kind()));
                            this.string_car168 = "y";
                        }
                        break;
                    }
                    case 0x11:
                    {
                        DataSet set19 = CallBLL.cz_drawback_ssc168_bll.GetDrawbackList(this.cz_users_model.get_u_name(), "4,5,6,7,8,9,10,11,12,13,14,15");
                        if (((set19 != null) && (set19.Tables.Count > 0)) && (set19.Tables[0].Rows.Count > 0))
                        {
                            this.table_ssc168 = set19.Tables[0];
                            DataTable table19 = CallBLL.cz_drawback_ssc168_bll.GetDrawbackList(this.cz_users_model.get_sup_name(), "4,5,6,7,8,9,10,11,12,13,14,15").Tables[0];
                            this.DICT.Add("ssc168", this.GetUpDrawbackSix(table19, "ssc168", this.cz_users_model.get_kc_kind()));
                            this.string_ssc168 = "y";
                        }
                        break;
                    }
                    case 0x12:
                    {
                        DataSet set20 = CallBLL.cz_drawback_vrcar_bll.GetDrawbackList(this.cz_users_model.get_u_name(), "1,2,3,4,36,37,38");
                        if (((set20 != null) && (set20.Tables.Count > 0)) && (set20.Tables[0].Rows.Count > 0))
                        {
                            this.table_vrcar = set20.Tables[0];
                            DataTable table20 = CallBLL.cz_drawback_vrcar_bll.GetDrawbackList(this.cz_users_model.get_sup_name(), "1,2,3,4,36,37,38").Tables[0];
                            this.DICT.Add("vrcar", this.GetUpDrawbackSix(table20, "vrcar", this.cz_users_model.get_kc_kind()));
                            this.string_vrcar = "y";
                        }
                        break;
                    }
                    case 0x13:
                    {
                        DataSet set21 = CallBLL.cz_drawback_vrssc_bll.GetDrawbackList(this.cz_users_model.get_u_name(), "4,5,6,7,8,9,10,11,12,13,14,15");
                        if (((set21 != null) && (set21.Tables.Count > 0)) && (set21.Tables[0].Rows.Count > 0))
                        {
                            this.table_vrssc = set21.Tables[0];
                            DataTable table21 = CallBLL.cz_drawback_vrssc_bll.GetDrawbackList(this.cz_users_model.get_sup_name(), "4,5,6,7,8,9,10,11,12,13,14,15").Tables[0];
                            this.DICT.Add("vrssc", this.GetUpDrawbackSix(table21, "vrssc", this.cz_users_model.get_kc_kind()));
                            this.string_vrssc = "y";
                        }
                        break;
                    }
                    case 20:
                    {
                        DataSet set22 = CallBLL.cz_drawback_xyftoa_bll.GetDrawbackList(this.cz_users_model.get_u_name(), "1,2,3,4,36,37,38");
                        if (((set22 != null) && (set22.Tables.Count > 0)) && (set22.Tables[0].Rows.Count > 0))
                        {
                            this.table_xyftoa = set22.Tables[0];
                            DataTable table22 = CallBLL.cz_drawback_xyftoa_bll.GetDrawbackList(this.cz_users_model.get_sup_name(), "1,2,3,4,36,37,38").Tables[0];
                            this.DICT.Add("xyftoa", this.GetUpDrawbackSix(table22, "xyftoa", this.cz_users_model.get_kc_kind()));
                            this.string_xyftoa = "y";
                        }
                        break;
                    }
                    case 0x15:
                    {
                        DataSet set23 = CallBLL.cz_drawback_xyftsg_bll.GetDrawbackList(this.cz_users_model.get_u_name(), "1,2,3,4,36,37,38");
                        if (((set23 != null) && (set23.Tables.Count > 0)) && (set23.Tables[0].Rows.Count > 0))
                        {
                            this.table_xyftsg = set23.Tables[0];
                            DataTable table23 = CallBLL.cz_drawback_xyftsg_bll.GetDrawbackList(this.cz_users_model.get_sup_name(), "1,2,3,4,36,37,38").Tables[0];
                            this.DICT.Add("xyftsg", this.GetUpDrawbackSix(table23, "xyftsg", this.cz_users_model.get_kc_kind()));
                            this.string_xyftsg = "y";
                        }
                        break;
                    }
                    case 0x16:
                    {
                        DataSet set24 = CallBLL.cz_drawback_happycar_bll.GetDrawbackList(this.cz_users_model.get_u_name(), "1,2,3,4,36,37,38");
                        if (((set24 != null) && (set24.Tables.Count > 0)) && (set24.Tables[0].Rows.Count > 0))
                        {
                            this.table_happycar = set24.Tables[0];
                            DataTable table24 = CallBLL.cz_drawback_happycar_bll.GetDrawbackList(this.cz_users_model.get_sup_name(), "1,2,3,4,36,37,38").Tables[0];
                            this.DICT.Add("happycar", this.GetUpDrawbackSix(table24, "happycar", this.cz_users_model.get_kc_kind()));
                            this.string_happycar = "y";
                        }
                        break;
                    }
                }
            }
        }

        protected void InitShortCut_KC(string pk)
        {
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
            }
            else if (this.table_xyft5 != null)
            {
                row = this.table_xyft5.Select(" play_id=1")[0];
                row2 = this.table_xyft5.Select(" play_id=2")[0];
            }
            else if (this.table_pkbjl != null)
            {
                row2 = this.table_pkbjl.Select(" play_id in (81001,81002,81003,81004)")[0];
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
            else if (this.table_jscqsc != null)
            {
                row = this.table_jscqsc.Select(" play_id=1")[0];
                row2 = this.table_jscqsc.Select(" play_id=2")[0];
            }
            else if (this.table_jspk10 != null)
            {
                row = this.table_jspk10.Select(" play_id=1")[0];
                row2 = this.table_jspk10.Select(" play_id=2")[0];
            }
            if (this.table_jssfc != null)
            {
                row = this.table_jssfc.Select(" play_id=81")[0];
                row2 = this.table_jssfc.Select(" play_id=82")[0];
                row3 = this.table_jssfc.Select(" play_id=72")[0];
            }
            else if (this.table_jsft2 != null)
            {
                row = this.table_jsft2.Select(" play_id=1")[0];
                row2 = this.table_jsft2.Select(" play_id=2")[0];
            }
            else if (this.table_car168 != null)
            {
                row = this.table_car168.Select(" play_id=1")[0];
                row2 = this.table_car168.Select(" play_id=2")[0];
            }
            else if (this.table_ssc168 != null)
            {
                row = this.table_ssc168.Select(" play_id=1")[0];
                row2 = this.table_ssc168.Select(" play_id=2")[0];
            }
            else if (this.table_vrcar != null)
            {
                row = this.table_vrcar.Select(" play_id=1")[0];
                row2 = this.table_vrcar.Select(" play_id=2")[0];
            }
            else if (this.table_vrssc != null)
            {
                row = this.table_vrssc.Select(" play_id=1")[0];
                row2 = this.table_vrssc.Select(" play_id=2")[0];
            }
            else if (this.table_xyftoa != null)
            {
                row = this.table_xyftoa.Select(" play_id=1")[0];
                row2 = this.table_xyftoa.Select(" play_id=2")[0];
            }
            else if (this.table_xyftsg != null)
            {
                row = this.table_xyftsg.Select(" play_id=1")[0];
                row2 = this.table_xyftsg.Select(" play_id=2")[0];
            }
            else if (this.table_happycar != null)
            {
                row = this.table_happycar.Select(" play_id=1")[0];
                row2 = this.table_happycar.Select(" play_id=2")[0];
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

        protected void InitShortCut_SIX(string pk)
        {
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

        private bool IsAllowUpdate_kc()
        {
            bool flag = CallBLL.cz_bet_kc_bll.ExistsBetByUserName(this.cz_users_model.get_u_type(), this.cz_users_model.get_u_name());
            bool flag2 = false;
            if (flag && this.IsOpenKC())
            {
                flag2 = true;
            }
            return flag2;
        }

        private bool IsAllowUpdate_six()
        {
            bool flag = CallBLL.cz_bet_six_bll.ExistsBetByUserName(this.cz_users_model.get_u_type(), this.cz_users_model.get_u_name());
            bool flag2 = CallBLL.cz_bet_six_bll.ExistsBetByUserName(this.cz_users_model.get_u_name());
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
            string rateName = this.Session["user_name"].ToString();
            this.userModel = this.Session[rateName + "lottery_session_user_info"] as agent_userinfo_session;
            this.userid = LSRequest.qq("uid");
            this.newAdd = LSRequest.qq("isadd");
            if (!Utils.IsGuid(this.userid))
            {
                base.Response.Redirect("../MessagePage.aspx?code=u100079&url=&issuccess=1&isback=0&isopen=1");
                base.Response.End();
            }
            this.cz_users_model = CallBLL.cz_users_bll.GetUserInfoByUID(this.userid);
            if (this.cz_users_model == null)
            {
                base.Response.Redirect("../MessagePage.aspx?code=u100034&url=&issuccess=1&isback=0&isopen=1");
                base.Response.End();
            }
            base.Permission_Aspx_ZJ(this.userModel, "po_2_1");
            base.Permission_Aspx_DL(this.userModel, "po_6_1");
            if (this.userModel.get_u_name().Equals(this.cz_users_model.get_u_name()))
            {
                base.Response.Redirect("../MessagePage.aspx?code=u100035&url=&issuccess=1&isback=1&isopen=1");
                base.Response.End();
            }
            if (!base.IsUnderLing(this.cz_users_model.get_u_name(), rateName, this.userModel.get_u_type().Trim()))
            {
                base.Response.Redirect("../MessagePage.aspx?code=u100014&url=&issuccess=1&isback=0&isopen=1");
                base.Response.End();
            }
            if (!base.IsUpperLowerLevels(this.cz_users_model.get_u_name(), this.userModel.get_u_type(), this.userModel.get_u_name()))
            {
                base.Response.Redirect("../MessagePage.aspx?code=u100057&url=&issuccess=1&isback=0&isopen=1");
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
            if (this.cz_users_model.get_six_kind().ToLower() != "0")
            {
                if (this.cz_users_model.get_six_kind().ToLower() != "a")
                {
                    this.pk_six_a = "display:none;";
                }
                if (this.cz_users_model.get_six_kind().ToLower() != "b")
                {
                    this.pk_six_b = "display:none;";
                }
                if (this.cz_users_model.get_six_kind().ToLower() != "c")
                {
                    this.pk_six_c = "display:none;";
                }
            }
            else
            {
                this.pk_six_a = "";
                this.pk_six_b = "";
                this.pk_six_c = "";
            }
            if (!string.IsNullOrEmpty(this.cz_users_model.get_kc_kind()) && !this.cz_users_model.get_kc_kind().Equals("0"))
            {
                if (this.cz_users_model.get_kc_kind().ToLower() != "a")
                {
                    this.pk_kc_a = "display:none;";
                }
                if (this.cz_users_model.get_kc_kind().ToLower() != "b")
                {
                    this.pk_kc_b = "display:none;";
                }
                if (this.cz_users_model.get_kc_kind().ToLower() != "c")
                {
                    this.pk_kc_c = "display:none;";
                }
                this.pk_kc = this.cz_users_model.get_kc_kind().ToLower();
            }
            else
            {
                this.pk_kc_a = "";
                this.pk_kc_b = "";
                this.pk_kc_c = "";
                this.pk_kc = "0";
            }
            this.InitData();
            this.isAllowUpdate_six = this.IsAllowUpdate_six();
            this.isAllowUpdate_kc = this.IsAllowUpdate_kc();
            this.drawBackjson = JsonHandle.ObjectToJson(this.DICT);
            if (((this.table_kl10 != null) || (this.table_xync != null)) || ((this.table_pcdd != null) || (this.table_jssfc != null)))
            {
                this.shortcut_blue = "";
                this.shortcut_violet = "";
                this.shortcut_green = "";
            }
            else
            {
                if (((((this.table_cqsc != null) || (this.table_pk10 != null)) || ((this.table_k8sc != null) || (this.table_xyft5 != null))) || (((this.table_jscar != null) || (this.table_speed5 != null)) || ((this.table_jscqsc != null) || (this.table_jspk10 != null)))) || ((((this.table_jsft2 != null) || (this.table_car168 != null)) || ((this.table_ssc168 != null) || (this.table_vrcar != null))) || (((this.table_vrssc != null) || (this.table_xyftoa != null)) || ((this.table_xyftsg != null) || (this.table_happycar != null)))))
                {
                    this.shortcut_blue = "";
                    this.shortcut_violet = "";
                }
                if (((this.table_jsk3 != null) || (this.table_kl8 != null)) || (this.table_pkbjl != null))
                {
                    this.shortcut_violet = "";
                }
            }
            if (this.table_six != null)
            {
                this.six_shortcut_blue = "";
                this.six_shortcut_violet = "";
                this.six_shortcut_green = "";
            }
            this.InitShortCut_SIX(this.cz_users_model.get_six_kind());
            this.InitShortCut_KC(this.cz_users_model.get_kc_kind());
            if (LSRequest.qq("hdnSubmit").Equals("hdnsubmit"))
            {
                this.rateKCModel = CallBLL.cz_rate_kc_bll.GetRateKCByUserName(this.cz_users_model.get_u_name());
                base.En_User_Lock(this.rateKCModel.get_fgs_name());
                string mess = "";
                string str4 = "";
                bool isSuccess = true;
                bool flag3 = true;
                this.namestr = LSRequest.qq("namestr");
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
                if (((((this.table_kl10 != null) || (this.table_cqsc != null)) || ((this.table_pk10 != null) || (this.table_xync != null))) || (((this.table_jsk3 != null) || (this.table_kl8 != null)) || ((this.table_k8sc != null) || (this.table_pcdd != null)))) || (((((this.table_xyft5 != null) || (this.table_pkbjl != null)) || ((this.table_jscar != null) || (this.table_speed5 != null))) || (((this.table_jscqsc != null) || (this.table_jspk10 != null)) || ((this.table_jssfc != null) || (this.table_jsft2 != null)))) || ((((this.table_car168 != null) || (this.table_ssc168 != null)) || ((this.table_vrcar != null) || (this.table_vrssc != null))) || (((this.table_xyftoa != null) || (this.table_xyftsg != null)) || (this.table_happycar != null)))))
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
                    if ((this.table_speed5 != null) && !string.IsNullOrEmpty(this.speed5_modify_playid))
                    {
                        this.ValidKC("speed5", 11.ToString(), this.table_speed5, this.speed5_modify_playid);
                    }
                    if ((this.table_jscqsc != null) && !string.IsNullOrEmpty(this.jscqsc_modify_playid))
                    {
                        this.ValidKC("jscqsc", 13.ToString(), this.table_jscqsc, this.jscqsc_modify_playid);
                    }
                    if ((this.table_jspk10 != null) && !string.IsNullOrEmpty(this.jspk10_modify_playid))
                    {
                        this.ValidKC("jspk10", 12.ToString(), this.table_jspk10, this.jspk10_modify_playid);
                    }
                    if ((this.table_jssfc != null) && !string.IsNullOrEmpty(this.jssfc_modify_playid))
                    {
                        this.ValidKC("jssfc", 14.ToString(), this.table_jssfc, this.jssfc_modify_playid);
                    }
                    if ((this.table_jsft2 != null) && !string.IsNullOrEmpty(this.jsft2_modify_playid))
                    {
                        this.ValidKC("jsft2", 15.ToString(), this.table_jsft2, this.jsft2_modify_playid);
                    }
                    if ((this.table_car168 != null) && !string.IsNullOrEmpty(this.car168_modify_playid))
                    {
                        this.ValidKC("car168", 0x10.ToString(), this.table_car168, this.car168_modify_playid);
                    }
                    if ((this.table_ssc168 != null) && !string.IsNullOrEmpty(this.ssc168_modify_playid))
                    {
                        this.ValidKC("ssc168", 0x11.ToString(), this.table_ssc168, this.ssc168_modify_playid);
                    }
                    if ((this.table_vrcar != null) && !string.IsNullOrEmpty(this.vrcar_modify_playid))
                    {
                        this.ValidKC("vrcar", 0x12.ToString(), this.table_vrcar, this.vrcar_modify_playid);
                    }
                    if ((this.table_vrssc != null) && !string.IsNullOrEmpty(this.vrssc_modify_playid))
                    {
                        this.ValidKC("vrssc", 0x13.ToString(), this.table_vrssc, this.vrssc_modify_playid);
                    }
                    if ((this.table_xyftoa != null) && !string.IsNullOrEmpty(this.xyftoa_modify_playid))
                    {
                        this.ValidKC("xyftoa", 20.ToString(), this.table_xyftoa, this.xyftoa_modify_playid);
                    }
                    if ((this.table_xyftsg != null) && !string.IsNullOrEmpty(this.xyftsg_modify_playid))
                    {
                        this.ValidKC("xyftsg", 0x15.ToString(), this.table_xyftsg, this.xyftsg_modify_playid);
                    }
                    if ((this.table_happycar != null) && !string.IsNullOrEmpty(this.happycar_modify_playid))
                    {
                        this.ValidKC("happycar", 0x16.ToString(), this.table_happycar, this.happycar_modify_playid);
                    }
                    if ((((!string.IsNullOrEmpty(this.kl10_modify_playid) || !string.IsNullOrEmpty(this.cqsc_modify_playid)) || (!string.IsNullOrEmpty(this.pk10_modify_playid) || !string.IsNullOrEmpty(this.xync_modify_playid))) || ((!string.IsNullOrEmpty(this.jsk3_modify_playid) || !string.IsNullOrEmpty(this.kl8_modify_playid)) || (!string.IsNullOrEmpty(this.k8sc_modify_playid) || !string.IsNullOrEmpty(this.pcdd_modify_playid)))) || ((((!string.IsNullOrEmpty(this.xyft5_modify_playid) || !string.IsNullOrEmpty(this.pkbjl_modify_playid)) || (!string.IsNullOrEmpty(this.jscar_modify_playid) || !string.IsNullOrEmpty(this.speed5_modify_playid))) || ((!string.IsNullOrEmpty(this.jscqsc_modify_playid) || !string.IsNullOrEmpty(this.jspk10_modify_playid)) || (!string.IsNullOrEmpty(this.jssfc_modify_playid) || !string.IsNullOrEmpty(this.jsft2_modify_playid)))) || (((!string.IsNullOrEmpty(this.car168_modify_playid) || !string.IsNullOrEmpty(this.ssc168_modify_playid)) || (!string.IsNullOrEmpty(this.vrcar_modify_playid) || !string.IsNullOrEmpty(this.vrssc_modify_playid))) || ((!string.IsNullOrEmpty(this.xyftoa_modify_playid) || !string.IsNullOrEmpty(this.xyftsg_modify_playid)) || !string.IsNullOrEmpty(this.happycar_modify_playid)))))
                    {
                        this.UpdateKC(ref str4, ref flag3);
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
                    if (!string.IsNullOrEmpty(this.jscqsc_modify_playid) && !this.newAdd.Equals("1"))
                    {
                        FileCacheHelper.UpdateDrawbackFile(13);
                    }
                    if (!string.IsNullOrEmpty(this.jspk10_modify_playid) && !this.newAdd.Equals("1"))
                    {
                        FileCacheHelper.UpdateDrawbackFile(12);
                    }
                    if (!string.IsNullOrEmpty(this.jssfc_modify_playid) && !this.newAdd.Equals("1"))
                    {
                        FileCacheHelper.UpdateDrawbackFile(14);
                    }
                    if (!string.IsNullOrEmpty(this.jsft2_modify_playid) && !this.newAdd.Equals("1"))
                    {
                        FileCacheHelper.UpdateDrawbackFile(15);
                    }
                    if (!string.IsNullOrEmpty(this.car168_modify_playid) && !this.newAdd.Equals("1"))
                    {
                        FileCacheHelper.UpdateDrawbackFile(0x10);
                    }
                    if (!string.IsNullOrEmpty(this.ssc168_modify_playid) && !this.newAdd.Equals("1"))
                    {
                        FileCacheHelper.UpdateDrawbackFile(0x11);
                    }
                    if (!string.IsNullOrEmpty(this.vrcar_modify_playid) && !this.newAdd.Equals("1"))
                    {
                        FileCacheHelper.UpdateDrawbackFile(0x12);
                    }
                    if (!string.IsNullOrEmpty(this.vrssc_modify_playid) && !this.newAdd.Equals("1"))
                    {
                        FileCacheHelper.UpdateDrawbackFile(0x13);
                    }
                    if (!string.IsNullOrEmpty(this.xyftoa_modify_playid) && !this.newAdd.Equals("1"))
                    {
                        FileCacheHelper.UpdateDrawbackFile(20);
                    }
                    if (!string.IsNullOrEmpty(this.xyftsg_modify_playid) && !this.newAdd.Equals("1"))
                    {
                        FileCacheHelper.UpdateDrawbackFile(0x15);
                    }
                    if (!string.IsNullOrEmpty(this.happycar_modify_playid) && !this.newAdd.Equals("1"))
                    {
                        FileCacheHelper.UpdateDrawbackFile(0x16);
                    }
                }
                base.Un_User_Lock(this.rateKCModel.get_fgs_name());
                string str5 = "";
                if (this.table_six != null)
                {
                    str5 = str5 + mess;
                }
                if (((((this.table_kl10 != null) || (this.table_cqsc != null)) || ((this.table_pk10 != null) || (this.table_xync != null))) || (((this.table_jsk3 != null) || (this.table_kl8 != null)) || ((this.table_k8sc != null) || (this.table_pcdd != null)))) || (((((this.table_xyft5 != null) || (this.table_pkbjl != null)) || ((this.table_jscar != null) || (this.table_speed5 != null))) || (((this.table_jscqsc != null) || (this.table_jspk10 != null)) || ((this.table_jssfc != null) || (this.table_jsft2 != null)))) || ((((this.table_car168 != null) || (this.table_ssc168 != null)) || ((this.table_vrcar != null) || (this.table_vrssc != null))) || (((this.table_xyftoa != null) || (this.table_xyftsg != null)) || (this.table_happycar != null)))))
                {
                    str5 = str5 + "<br />" + str4;
                }
                if (isSuccess && flag3)
                {
                    if (string.IsNullOrEmpty(this.newAdd))
                    {
                        this.newAdd = "0";
                    }
                    base.Response.Write(base.ShowDialogBox(str5, base.UserReturnBackUrl, 0));
                }
                else
                {
                    base.Response.Write(base.ShowDialogBox(str5, base.UserReturnBackUrl, 400));
                }
                base.Response.End();
            }
        }

        protected void UpdateDrawback_kc_tpl(string u_name)
        {
            if (LSRequest.qq("savenew").Equals("1") && this.userModel.get_u_type().Equals("zj"))
            {
                if (this.table_kl10 != null)
                {
                    CallBLL.cz_drawback_kl10_tpl_bll.Update(u_name);
                }
                if (this.table_cqsc != null)
                {
                    CallBLL.cz_drawback_cqsc_tpl_bll.Update(u_name);
                }
                if (this.table_pk10 != null)
                {
                    CallBLL.cz_drawback_pk10_tpl_bll.Update(u_name);
                }
                if (this.table_xync != null)
                {
                    CallBLL.cz_drawback_xync_tpl_bll.Update(u_name);
                }
                if (this.table_jsk3 != null)
                {
                    CallBLL.cz_drawback_jsk3_tpl_bll.Update(u_name);
                }
                if (this.table_kl8 != null)
                {
                    CallBLL.cz_drawback_kl8_tpl_bll.Update(u_name);
                }
                if (this.table_k8sc != null)
                {
                    CallBLL.cz_drawback_k8sc_tpl_bll.Update(u_name);
                }
                if (this.table_pcdd != null)
                {
                    CallBLL.cz_drawback_pcdd_tpl_bll.Update(u_name);
                }
                if (this.table_xyft5 != null)
                {
                    CallBLL.cz_drawback_xyft5_tpl_bll.Update(u_name);
                }
                if (this.table_pkbjl != null)
                {
                    CallBLL.cz_drawback_pkbjl_tpl_bll.Update(u_name);
                }
                if (this.table_jscar != null)
                {
                    CallBLL.cz_drawback_jscar_tpl_bll.Update(u_name);
                }
                if (this.table_speed5 != null)
                {
                    CallBLL.cz_drawback_speed5_tpl_bll.Update(u_name);
                }
                if (this.table_jscqsc != null)
                {
                    CallBLL.cz_drawback_jscqsc_tpl_bll.Update(u_name);
                }
                if (this.table_jspk10 != null)
                {
                    CallBLL.cz_drawback_jspk10_tpl_bll.Update(u_name);
                }
                if (this.table_jssfc != null)
                {
                    CallBLL.cz_drawback_jssfc_tpl_bll.Update(u_name);
                }
                if (this.table_jsft2 != null)
                {
                    CallBLL.cz_drawback_jsft2_tpl_bll.Update(u_name);
                }
                if (this.table_car168 != null)
                {
                    CallBLL.cz_drawback_car168_tpl_bll.Update(u_name);
                }
                if (this.table_ssc168 != null)
                {
                    CallBLL.cz_drawback_ssc168_tpl_bll.Update(u_name);
                }
                if (this.table_vrcar != null)
                {
                    CallBLL.cz_drawback_vrcar_tpl_bll.Update(u_name);
                }
                if (this.table_vrssc != null)
                {
                    CallBLL.cz_drawback_vrssc_tpl_bll.Update(u_name);
                }
                if (this.table_xyftoa != null)
                {
                    CallBLL.cz_drawback_xyftoa_tpl_bll.Update(u_name);
                }
                if (this.table_xyftsg != null)
                {
                    CallBLL.cz_drawback_xyftsg_tpl_bll.Update(u_name);
                }
                if (this.table_happycar != null)
                {
                    CallBLL.cz_drawback_happycar_tpl_bll.Update(u_name);
                }
            }
        }

        protected void UpdateDrawback_six_tpl(string u_name)
        {
            if ((LSRequest.qq("savenew").Equals("1") && this.userModel.get_u_type().Equals("zj")) && (this.table_six != null))
            {
                CallBLL.cz_drawback_six_tpl_bll.Update(u_name);
            }
        }

        private void UpdateKC(ref string mess, ref bool isSuccess)
        {
            string str507;
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
                    string str8 = LSRequest.qq(string.Format("kl10_a_{0}", str));
                    string str9 = LSRequest.qq(string.Format("kl10_b_{0}", str));
                    string str10 = LSRequest.qq(string.Format("kl10_c_{0}", str));
                    string str11 = LSRequest.qq(string.Format("kl10_max_amount_{0}", str));
                    string str12 = LSRequest.qq(string.Format("kl10_phase_amount_{0}", str));
                    string str13 = LSRequest.qq(string.Format("kl10_single_min_amount_{0}", str.ToLower()));
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
                        str507 = this.cz_users_model.get_kc_kind().ToLower();
                        if (str507 != null)
                        {
                            if (!(str507 == "a"))
                            {
                                if (str507 == "b")
                                {
                                    goto Label_039D;
                                }
                                if (str507 == "c")
                                {
                                    goto Label_03B3;
                                }
                            }
                            else if (double.Parse(str8) > double.Parse(s))
                            {
                                str8 = s;
                            }
                        }
                    }
                    goto Label_03C7;
                Label_039D:
                    if (double.Parse(str9) > double.Parse(str3))
                    {
                        str9 = str3;
                    }
                    goto Label_03C7;
                Label_03B3:
                    if (double.Parse(str10) > double.Parse(str4))
                    {
                        str10 = str4;
                    }
                Label_03C7:
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
                    string str30 = LSRequest.qq(string.Format("cqsc_a_{0}", str23));
                    string str31 = LSRequest.qq(string.Format("cqsc_b_{0}", str23));
                    string str32 = LSRequest.qq(string.Format("cqsc_c_{0}", str23));
                    string str33 = LSRequest.qq(string.Format("cqsc_max_amount_{0}", str23));
                    string str34 = LSRequest.qq(string.Format("cqsc_phase_amount_{0}", str23));
                    string str35 = LSRequest.qq(string.Format("cqsc_single_min_amount_{0}", str23.ToLower()));
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
                        str507 = this.cz_users_model.get_kc_kind().ToLower();
                        if (str507 != null)
                        {
                            if (!(str507 == "a"))
                            {
                                if (str507 == "b")
                                {
                                    goto Label_0EAB;
                                }
                                if (str507 == "c")
                                {
                                    goto Label_0EC1;
                                }
                            }
                            else if (double.Parse(str30) > double.Parse(str24))
                            {
                                str30 = str24;
                            }
                        }
                    }
                    goto Label_0ED5;
                Label_0EAB:
                    if (double.Parse(str31) > double.Parse(str25))
                    {
                        str31 = str25;
                    }
                    goto Label_0ED5;
                Label_0EC1:
                    if (double.Parse(str32) > double.Parse(str26))
                    {
                        str32 = str26;
                    }
                Label_0ED5:
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
                    str507 = str23;
                    if (str507 == null)
                    {
                        goto Label_106D;
                    }
                    if (!(str507 == "1"))
                    {
                        if (str507 == "2")
                        {
                            goto Label_105B;
                        }
                        if (str507 == "3")
                        {
                            goto Label_1064;
                        }
                        goto Label_106D;
                    }
                    str43 = "1,4,7,10,13";
                    goto Label_1071;
                Label_105B:
                    str43 = "2,5,8,11,14";
                    goto Label_1071;
                Label_1064:
                    str43 = "3,6,9,12,15";
                    goto Label_1071;
                Label_106D:
                    str43 = str23;
                Label_1071:
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
                    string str52 = LSRequest.qq(string.Format("pk10_a_{0}", str45));
                    string str53 = LSRequest.qq(string.Format("pk10_b_{0}", str45));
                    string str54 = LSRequest.qq(string.Format("pk10_c_{0}", str45));
                    string str55 = LSRequest.qq(string.Format("pk10_max_amount_{0}", str45));
                    string str56 = LSRequest.qq(string.Format("pk10_phase_amount_{0}", str45));
                    string str57 = LSRequest.qq(string.Format("pk10_single_min_amount_{0}", str45.ToLower()));
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
                        str507 = this.cz_users_model.get_kc_kind().ToLower();
                        if (str507 != null)
                        {
                            if (!(str507 == "a"))
                            {
                                if (str507 == "b")
                                {
                                    goto Label_1918;
                                }
                                if (str507 == "c")
                                {
                                    goto Label_192E;
                                }
                            }
                            else if (double.Parse(str52) > double.Parse(str46))
                            {
                                str52 = str46;
                            }
                        }
                    }
                    goto Label_1942;
                Label_1918:
                    if (double.Parse(str53) > double.Parse(str47))
                    {
                        str53 = str47;
                    }
                    goto Label_1942;
                Label_192E:
                    if (double.Parse(str54) > double.Parse(str48))
                    {
                        str54 = str48;
                    }
                Label_1942:
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
                    str507 = str45;
                    if (str507 == null)
                    {
                        goto Label_1AF3;
                    }
                    if (!(str507 == "1"))
                    {
                        if (str507 == "2")
                        {
                            goto Label_1AD8;
                        }
                        if (str507 == "3")
                        {
                            goto Label_1AE1;
                        }
                        if (str507 == "4")
                        {
                            goto Label_1AEA;
                        }
                        goto Label_1AF3;
                    }
                    str65 = "1,5,9,13,17,21,24,27,30,33";
                    goto Label_1AF7;
                Label_1AD8:
                    str65 = "2,6,10,14,18,22,25,28,31,34";
                    goto Label_1AF7;
                Label_1AE1:
                    str65 = "3,7,11,15,19,23,26,29,32,35";
                    goto Label_1AF7;
                Label_1AEA:
                    str65 = "4,8,12,16,20";
                    goto Label_1AF7;
                Label_1AF3:
                    str65 = str45;
                Label_1AF7:
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
                    string str74 = LSRequest.qq(string.Format("xync_a_{0}", str67));
                    string str75 = LSRequest.qq(string.Format("xync_b_{0}", str67));
                    string str76 = LSRequest.qq(string.Format("xync_c_{0}", str67));
                    string str77 = LSRequest.qq(string.Format("xync_max_amount_{0}", str67));
                    string str78 = LSRequest.qq(string.Format("xync_phase_amount_{0}", str67));
                    string str79 = LSRequest.qq(string.Format("xync_single_min_amount_{0}", str67.ToLower()));
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
                        str507 = this.cz_users_model.get_kc_kind().ToLower();
                        if (str507 != null)
                        {
                            if (!(str507 == "a"))
                            {
                                if (str507 == "b")
                                {
                                    goto Label_239E;
                                }
                                if (str507 == "c")
                                {
                                    goto Label_23B4;
                                }
                            }
                            else if (double.Parse(str74) > double.Parse(str68))
                            {
                                str74 = str68;
                            }
                        }
                    }
                    goto Label_23C8;
                Label_239E:
                    if (double.Parse(str75) > double.Parse(str69))
                    {
                        str75 = str69;
                    }
                    goto Label_23C8;
                Label_23B4:
                    if (double.Parse(str76) > double.Parse(str70))
                    {
                        str76 = str70;
                    }
                Label_23C8:
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
                    string str96 = LSRequest.qq(string.Format("jsk3_a_{0}", str89));
                    string str97 = LSRequest.qq(string.Format("jsk3_b_{0}", str89));
                    string str98 = LSRequest.qq(string.Format("jsk3_c_{0}", str89));
                    string str99 = LSRequest.qq(string.Format("jsk3_max_amount_{0}", str89));
                    string str100 = LSRequest.qq(string.Format("jsk3_phase_amount_{0}", str89));
                    string str101 = LSRequest.qq(string.Format("jsk3_single_min_amount_{0}", str89.ToLower()));
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
                        str507 = this.cz_users_model.get_kc_kind().ToLower();
                        if (str507 != null)
                        {
                            if (!(str507 == "a"))
                            {
                                if (str507 == "b")
                                {
                                    goto Label_2E9C;
                                }
                                if (str507 == "c")
                                {
                                    goto Label_2EB2;
                                }
                            }
                            else if (double.Parse(str96) > double.Parse(str90))
                            {
                                str96 = str90;
                            }
                        }
                    }
                    goto Label_2EC6;
                Label_2E9C:
                    if (double.Parse(str97) > double.Parse(str91))
                    {
                        str97 = str91;
                    }
                    goto Label_2EC6;
                Label_2EB2:
                    if (double.Parse(str98) > double.Parse(str92))
                    {
                        str98 = str92;
                    }
                Label_2EC6:
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
                    string str118 = LSRequest.qq(string.Format("kl8_a_{0}", str111));
                    string str119 = LSRequest.qq(string.Format("kl8_b_{0}", str111));
                    string str120 = LSRequest.qq(string.Format("kl8_c_{0}", str111));
                    string str121 = LSRequest.qq(string.Format("kl8_max_amount_{0}", str111));
                    string str122 = LSRequest.qq(string.Format("kl8_phase_amount_{0}", str111));
                    string str123 = LSRequest.qq(string.Format("kl8_single_min_amount_{0}", str111.ToLower()));
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
                        str507 = this.cz_users_model.get_kc_kind().ToLower();
                        if (str507 != null)
                        {
                            if (!(str507 == "a"))
                            {
                                if (str507 == "b")
                                {
                                    goto Label_38A1;
                                }
                                if (str507 == "c")
                                {
                                    goto Label_38B7;
                                }
                            }
                            else if (double.Parse(str118) > double.Parse(str112))
                            {
                                str118 = str112;
                            }
                        }
                    }
                    goto Label_38CB;
                Label_38A1:
                    if (double.Parse(str119) > double.Parse(str113))
                    {
                        str119 = str113;
                    }
                    goto Label_38CB;
                Label_38B7:
                    if (double.Parse(str120) > double.Parse(str114))
                    {
                        str120 = str114;
                    }
                Label_38CB:
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
                    string str140 = LSRequest.qq(string.Format("k8sc_a_{0}", str133));
                    string str141 = LSRequest.qq(string.Format("k8sc_b_{0}", str133));
                    string str142 = LSRequest.qq(string.Format("k8sc_c_{0}", str133));
                    string str143 = LSRequest.qq(string.Format("k8sc_max_amount_{0}", str133));
                    string str144 = LSRequest.qq(string.Format("k8sc_phase_amount_{0}", str133));
                    string str145 = LSRequest.qq(string.Format("k8sc_single_min_amount_{0}", str133.ToLower()));
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
                        str507 = this.cz_users_model.get_kc_kind().ToLower();
                        if (str507 != null)
                        {
                            if (!(str507 == "a"))
                            {
                                if (str507 == "b")
                                {
                                    goto Label_431E;
                                }
                                if (str507 == "c")
                                {
                                    goto Label_433C;
                                }
                            }
                            else if (double.Parse(str140) > double.Parse(str134))
                            {
                                str140 = str134;
                            }
                        }
                    }
                    goto Label_4358;
                Label_431E:
                    if (double.Parse(str141) > double.Parse(str135))
                    {
                        str141 = str135;
                    }
                    goto Label_4358;
                Label_433C:
                    if (double.Parse(str142) > double.Parse(str136))
                    {
                        str142 = str136;
                    }
                Label_4358:
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
                    str507 = str133;
                    if (str507 == null)
                    {
                        goto Label_453A;
                    }
                    if (!(str507 == "1"))
                    {
                        if (str507 == "2")
                        {
                            goto Label_4524;
                        }
                        if (str507 == "3")
                        {
                            goto Label_452F;
                        }
                        goto Label_453A;
                    }
                    str153 = "1,4,7,10,13";
                    goto Label_4540;
                Label_4524:
                    str153 = "2,5,8,11,14";
                    goto Label_4540;
                Label_452F:
                    str153 = "3,6,9,12,15";
                    goto Label_4540;
                Label_453A:
                    str153 = str133;
                Label_4540:
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
                    string str162 = LSRequest.qq(string.Format("pcdd_a_{0}", str155));
                    string str163 = LSRequest.qq(string.Format("pcdd_b_{0}", str155));
                    string str164 = LSRequest.qq(string.Format("pcdd_c_{0}", str155));
                    string str165 = LSRequest.qq(string.Format("pcdd_max_amount_{0}", str155));
                    string str166 = LSRequest.qq(string.Format("pcdd_phase_amount_{0}", str155));
                    string str167 = LSRequest.qq(string.Format("pcdd_single_min_amount_{0}", str155.ToLower()));
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
                        str507 = this.cz_users_model.get_kc_kind().ToLower();
                        if (str507 != null)
                        {
                            if (!(str507 == "a"))
                            {
                                if (str507 == "b")
                                {
                                    goto Label_4F49;
                                }
                                if (str507 == "c")
                                {
                                    goto Label_4F67;
                                }
                            }
                            else if (double.Parse(str162) > double.Parse(str156))
                            {
                                str162 = str156;
                            }
                        }
                    }
                    goto Label_4F83;
                Label_4F49:
                    if (double.Parse(str163) > double.Parse(str157))
                    {
                        str163 = str157;
                    }
                    goto Label_4F83;
                Label_4F67:
                    if (double.Parse(str164) > double.Parse(str158))
                    {
                        str164 = str158;
                    }
                Label_4F83:
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
                    str507 = str155;
                    if (str507 == null)
                    {
                        goto Label_514E;
                    }
                    if (!(str507 == "71007"))
                    {
                        if (str507 == "71008")
                        {
                            goto Label_5143;
                        }
                        goto Label_514E;
                    }
                    str175 = "71007,71009,71011";
                    goto Label_5156;
                Label_5143:
                    str175 = "71008,71010,71012";
                    goto Label_5156;
                Label_514E:
                    str175 = str155;
                Label_5156:
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
                    string str184 = LSRequest.qq(string.Format("xyft5_a_{0}", str177));
                    string str185 = LSRequest.qq(string.Format("xyft5_b_{0}", str177));
                    string str186 = LSRequest.qq(string.Format("xyft5_c_{0}", str177));
                    string str187 = LSRequest.qq(string.Format("xyft5_max_amount_{0}", str177));
                    string str188 = LSRequest.qq(string.Format("xyft5_phase_amount_{0}", str177));
                    string str189 = LSRequest.qq(string.Format("xyft5_single_min_amount_{0}", str177.ToLower()));
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
                        str507 = this.cz_users_model.get_kc_kind().ToLower();
                        if (str507 != null)
                        {
                            if (!(str507 == "a"))
                            {
                                if (str507 == "b")
                                {
                                    goto Label_5B7B;
                                }
                                if (str507 == "c")
                                {
                                    goto Label_5B99;
                                }
                            }
                            else if (double.Parse(str184) > double.Parse(str178))
                            {
                                str184 = str178;
                            }
                        }
                    }
                    goto Label_5BB5;
                Label_5B7B:
                    if (double.Parse(str185) > double.Parse(str179))
                    {
                        str185 = str179;
                    }
                    goto Label_5BB5;
                Label_5B99:
                    if (double.Parse(str186) > double.Parse(str180))
                    {
                        str186 = str180;
                    }
                Label_5BB5:
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
                    str507 = str177;
                    if (str507 == null)
                    {
                        goto Label_5DB6;
                    }
                    if (!(str507 == "1"))
                    {
                        if (str507 == "2")
                        {
                            goto Label_5D95;
                        }
                        if (str507 == "3")
                        {
                            goto Label_5DA0;
                        }
                        if (str507 == "4")
                        {
                            goto Label_5DAB;
                        }
                        goto Label_5DB6;
                    }
                    str197 = "1,5,9,13,17,21,24,27,30,33";
                    goto Label_5DBE;
                Label_5D95:
                    str197 = "2,6,10,14,18,22,25,28,31,34";
                    goto Label_5DBE;
                Label_5DA0:
                    str197 = "3,7,11,15,19,23,26,29,32,35";
                    goto Label_5DBE;
                Label_5DAB:
                    str197 = "4,8,12,16,20";
                    goto Label_5DBE;
                Label_5DB6:
                    str197 = str177;
                Label_5DBE:
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
                    string str206 = LSRequest.qq(string.Format("pkbjl_a_{0}", str199));
                    string str207 = LSRequest.qq(string.Format("pkbjl_b_{0}", str199));
                    string str208 = LSRequest.qq(string.Format("pkbjl_c_{0}", str199));
                    string str209 = LSRequest.qq(string.Format("pkbjl_max_amount_{0}", str199));
                    string str210 = LSRequest.qq(string.Format("pkbjl_phase_amount_{0}", str199));
                    string str211 = LSRequest.qq(string.Format("pkbjl_single_min_amount_{0}", str199.ToLower()));
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
                        str507 = this.cz_users_model.get_kc_kind().ToLower();
                        if (str507 != null)
                        {
                            if (!(str507 == "a"))
                            {
                                if (str507 == "b")
                                {
                                    goto Label_67E4;
                                }
                                if (str507 == "c")
                                {
                                    goto Label_6802;
                                }
                            }
                            else if (double.Parse(str206) > double.Parse(str200))
                            {
                                str206 = str200;
                            }
                        }
                    }
                    goto Label_681E;
                Label_67E4:
                    if (double.Parse(str207) > double.Parse(str201))
                    {
                        str207 = str201;
                    }
                    goto Label_681E;
                Label_6802:
                    if (double.Parse(str208) > double.Parse(str202))
                    {
                        str208 = str202;
                    }
                Label_681E:
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
                    string str219 = "";
                    str219 = str199;
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
                    string str228 = LSRequest.qq(string.Format("jscar_a_{0}", str221));
                    string str229 = LSRequest.qq(string.Format("jscar_b_{0}", str221));
                    string str230 = LSRequest.qq(string.Format("jscar_c_{0}", str221));
                    string str231 = LSRequest.qq(string.Format("jscar_max_amount_{0}", str221));
                    string str232 = LSRequest.qq(string.Format("jscar_phase_amount_{0}", str221));
                    string str233 = LSRequest.qq(string.Format("jscar_single_min_amount_{0}", str221.ToLower()));
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
                        str507 = this.cz_users_model.get_kc_kind().ToLower();
                        if (str507 != null)
                        {
                            if (!(str507 == "a"))
                            {
                                if (str507 == "b")
                                {
                                    goto Label_73D3;
                                }
                                if (str507 == "c")
                                {
                                    goto Label_73F1;
                                }
                            }
                            else if (double.Parse(str228) > double.Parse(str222))
                            {
                                str228 = str222;
                            }
                        }
                    }
                    goto Label_740D;
                Label_73D3:
                    if (double.Parse(str229) > double.Parse(str223))
                    {
                        str229 = str223;
                    }
                    goto Label_740D;
                Label_73F1:
                    if (double.Parse(str230) > double.Parse(str224))
                    {
                        str230 = str224;
                    }
                Label_740D:
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
                    str507 = str221;
                    if (str507 == null)
                    {
                        goto Label_760E;
                    }
                    if (!(str507 == "1"))
                    {
                        if (str507 == "2")
                        {
                            goto Label_75ED;
                        }
                        if (str507 == "3")
                        {
                            goto Label_75F8;
                        }
                        if (str507 == "4")
                        {
                            goto Label_7603;
                        }
                        goto Label_760E;
                    }
                    str241 = "1,5,9,13,17,21,24,27,30,33";
                    goto Label_7616;
                Label_75ED:
                    str241 = "2,6,10,14,18,22,25,28,31,34";
                    goto Label_7616;
                Label_75F8:
                    str241 = "3,7,11,15,19,23,26,29,32,35";
                    goto Label_7616;
                Label_7603:
                    str241 = "4,8,12,16,20";
                    goto Label_7616;
                Label_760E:
                    str241 = str221;
                Label_7616:
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
                    string str250 = LSRequest.qq(string.Format("speed5_a_{0}", str243));
                    string str251 = LSRequest.qq(string.Format("speed5_b_{0}", str243));
                    string str252 = LSRequest.qq(string.Format("speed5_c_{0}", str243));
                    string str253 = LSRequest.qq(string.Format("speed5_max_amount_{0}", str243));
                    string str254 = LSRequest.qq(string.Format("speed5_phase_amount_{0}", str243));
                    string str255 = LSRequest.qq(string.Format("speed5_single_min_amount_{0}", str243.ToLower()));
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
                        str507 = this.cz_users_model.get_kc_kind().ToLower();
                        if (str507 != null)
                        {
                            if (!(str507 == "a"))
                            {
                                if (str507 == "b")
                                {
                                    goto Label_803C;
                                }
                                if (str507 == "c")
                                {
                                    goto Label_805A;
                                }
                            }
                            else if (double.Parse(str250) > double.Parse(str244))
                            {
                                str250 = str244;
                            }
                        }
                    }
                    goto Label_8076;
                Label_803C:
                    if (double.Parse(str251) > double.Parse(str245))
                    {
                        str251 = str245;
                    }
                    goto Label_8076;
                Label_805A:
                    if (double.Parse(str252) > double.Parse(str246))
                    {
                        str252 = str246;
                    }
                Label_8076:
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
                    str507 = str243;
                    if (str507 == null)
                    {
                        goto Label_825C;
                    }
                    if (!(str507 == "1"))
                    {
                        if (str507 == "2")
                        {
                            goto Label_8246;
                        }
                        if (str507 == "3")
                        {
                            goto Label_8251;
                        }
                        goto Label_825C;
                    }
                    str263 = "1,4,7,10,13";
                    goto Label_8264;
                Label_8246:
                    str263 = "2,5,8,11,14";
                    goto Label_8264;
                Label_8251:
                    str263 = "3,6,9,12,15";
                    goto Label_8264;
                Label_825C:
                    str263 = str243;
                Label_8264:
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
            if ((this.table_jscqsc != null) && !string.IsNullOrEmpty(this.jscqsc_modify_playid))
            {
                DataTable table25 = CallBLL.cz_drawback_jscqsc_bll.GetDrawbackLists(this.cz_users_model.get_sup_name(), this.jscqsc_modify_playid).Tables[0];
                DataTable table26 = CallBLL.cz_drawback_jscqsc_bll.GetDrawbackLists(this.cz_users_model.get_u_name(), this.jscqsc_modify_playid).Tables[0];
                Dictionary<string, cz_drawback_jscqsc> dictionary13 = new Dictionary<string, cz_drawback_jscqsc>();
                List<drawback_downuser> list25 = new List<drawback_downuser>();
                List<drawback_downuser> list26 = new List<drawback_downuser>();
                foreach (DataRow row13 in table25.Rows)
                {
                    string str265 = row13["play_id"].ToString();
                    string str266 = row13["a_drawback"].ToString();
                    string str267 = row13["b_drawback"].ToString();
                    string str268 = row13["c_drawback"].ToString();
                    string str269 = row13["single_max_amount"].ToString();
                    string str270 = row13["single_phase_amount"].ToString();
                    string str271 = row13["single_min_amount"].ToString();
                    string str272 = LSRequest.qq(string.Format("jscqsc_a_{0}", str265));
                    string str273 = LSRequest.qq(string.Format("jscqsc_b_{0}", str265));
                    string str274 = LSRequest.qq(string.Format("jscqsc_c_{0}", str265));
                    string str275 = LSRequest.qq(string.Format("jscqsc_max_amount_{0}", str265));
                    string str276 = LSRequest.qq(string.Format("jscqsc_phase_amount_{0}", str265));
                    string str277 = LSRequest.qq(string.Format("jscqsc_single_min_amount_{0}", str265.ToLower()));
                    DataRow[] rowArray13 = table26.Select(string.Format(" play_id={0} ", str265));
                    string str278 = rowArray13[0]["a_drawback"].ToString();
                    string str279 = rowArray13[0]["b_drawback"].ToString();
                    string str280 = rowArray13[0]["c_drawback"].ToString();
                    string str281 = rowArray13[0]["single_max_amount"].ToString();
                    string str282 = rowArray13[0]["single_phase_amount"].ToString();
                    string str283 = rowArray13[0]["single_min_amount"].ToString();
                    if ((((double.Parse(str278) == double.Parse(str272)) && (double.Parse(str279) == double.Parse(str273))) && ((double.Parse(str280) == double.Parse(str274)) && (double.Parse(str281) == double.Parse(str275)))) && ((double.Parse(str282) == double.Parse(str276)) && (double.Parse(str283) == double.Parse(str277))))
                    {
                        continue;
                    }
                    if (string.IsNullOrEmpty(this.cz_users_model.get_kc_kind()) || this.cz_users_model.get_kc_kind().Equals("0"))
                    {
                        if (double.Parse(str272) > double.Parse(str266))
                        {
                            str272 = str266;
                        }
                        if (double.Parse(str273) > double.Parse(str267))
                        {
                            str273 = str267;
                        }
                        if (double.Parse(str274) > double.Parse(str268))
                        {
                            str274 = str268;
                        }
                    }
                    else
                    {
                        str507 = this.cz_users_model.get_kc_kind().ToLower();
                        if (str507 != null)
                        {
                            if (!(str507 == "a"))
                            {
                                if (str507 == "b")
                                {
                                    goto Label_8C8A;
                                }
                                if (str507 == "c")
                                {
                                    goto Label_8CA8;
                                }
                            }
                            else if (double.Parse(str272) > double.Parse(str266))
                            {
                                str272 = str266;
                            }
                        }
                    }
                    goto Label_8CC4;
                Label_8C8A:
                    if (double.Parse(str273) > double.Parse(str267))
                    {
                        str273 = str267;
                    }
                    goto Label_8CC4;
                Label_8CA8:
                    if (double.Parse(str274) > double.Parse(str268))
                    {
                        str274 = str268;
                    }
                Label_8CC4:
                    if (double.Parse(str275) > double.Parse(str269))
                    {
                        str275 = str269;
                    }
                    if (double.Parse(str276) > double.Parse(str270))
                    {
                        str276 = str270;
                    }
                    if (double.Parse(str277) < double.Parse(str271))
                    {
                        str277 = str271;
                    }
                    cz_drawback_jscqsc _jscqsc = new cz_drawback_jscqsc();
                    _jscqsc.set_play_id(new int?(Convert.ToInt32(str265)));
                    if (!string.IsNullOrEmpty(str272))
                    {
                        _jscqsc.set_a_drawback(new decimal?(Convert.ToDecimal(str272)));
                    }
                    if (!string.IsNullOrEmpty(str273))
                    {
                        _jscqsc.set_b_drawback(new decimal?(Convert.ToDecimal(str273)));
                    }
                    if (!string.IsNullOrEmpty(str274))
                    {
                        _jscqsc.set_c_drawback(new decimal?(Convert.ToDecimal(str274)));
                    }
                    _jscqsc.set_single_max_amount(new decimal?(Convert.ToDecimal(str275)));
                    _jscqsc.set_single_phase_amount(new decimal?(Convert.ToDecimal(str276)));
                    _jscqsc.set_single_min_amount(new decimal?(Convert.ToDecimal(str277)));
                    _jscqsc.set_u_name(this.cz_users_model.get_u_name());
                    string str284 = "0";
                    if (string.IsNullOrEmpty(this.cz_users_model.get_kc_kind()) || this.cz_users_model.get_kc_kind().Equals("0"))
                    {
                        str284 = "0";
                    }
                    else
                    {
                        str284 = this.cz_users_model.get_kc_kind();
                    }
                    string str285 = "";
                    str507 = str265;
                    if (str507 == null)
                    {
                        goto Label_8EAA;
                    }
                    if (!(str507 == "1"))
                    {
                        if (str507 == "2")
                        {
                            goto Label_8E94;
                        }
                        if (str507 == "3")
                        {
                            goto Label_8E9F;
                        }
                        goto Label_8EAA;
                    }
                    str285 = "1,4,7,10,13";
                    goto Label_8EB2;
                Label_8E94:
                    str285 = "2,5,8,11,14";
                    goto Label_8EB2;
                Label_8E9F:
                    str285 = "3,6,9,12,15";
                    goto Label_8EB2;
                Label_8EAA:
                    str285 = str265;
                Label_8EB2:
                    dictionary13.Add(str285, _jscqsc);
                    if (!this.cz_users_model.get_u_type().Equals("hy"))
                    {
                        drawback_downuser _downuser109 = new drawback_downuser();
                        _downuser109.set_updateUserName(this.cz_users_model.get_u_name());
                        _downuser109.set_updateUserType(this.cz_users_model.get_u_type());
                        _downuser109.set_play_id(str285);
                        _downuser109.set_opt("dq");
                        _downuser109.set_douvalue(double.Parse(_jscqsc.get_single_phase_amount().ToString()));
                        list25.Add(_downuser109);
                        drawback_downuser _downuser110 = new drawback_downuser();
                        _downuser110.set_updateUserName(this.cz_users_model.get_u_name());
                        _downuser110.set_updateUserType(this.cz_users_model.get_u_type());
                        _downuser110.set_play_id(str285);
                        _downuser110.set_opt("dz");
                        _downuser110.set_douvalue(double.Parse(_jscqsc.get_single_max_amount().ToString()));
                        list25.Add(_downuser110);
                        if (str284.ToLower().Equals("a"))
                        {
                            drawback_downuser _downuser111 = new drawback_downuser();
                            _downuser111.set_updateUserName(this.cz_users_model.get_u_name());
                            _downuser111.set_updateUserType(this.cz_users_model.get_u_type());
                            _downuser111.set_play_id(str285);
                            _downuser111.set_opt("a");
                            _downuser111.set_douvalue(double.Parse(_jscqsc.get_a_drawback().ToString()));
                            list25.Add(_downuser111);
                        }
                        else if (str284.ToLower().Equals("b"))
                        {
                            drawback_downuser _downuser112 = new drawback_downuser();
                            _downuser112.set_updateUserName(this.cz_users_model.get_u_name());
                            _downuser112.set_updateUserType(this.cz_users_model.get_u_type());
                            _downuser112.set_play_id(str285);
                            _downuser112.set_opt("b");
                            _downuser112.set_douvalue(double.Parse(_jscqsc.get_b_drawback().ToString()));
                            list25.Add(_downuser112);
                        }
                        else if (str284.ToLower().Equals("c"))
                        {
                            drawback_downuser _downuser113 = new drawback_downuser();
                            _downuser113.set_updateUserName(this.cz_users_model.get_u_name());
                            _downuser113.set_updateUserType(this.cz_users_model.get_u_type());
                            _downuser113.set_play_id(str285);
                            _downuser113.set_opt("c");
                            _downuser113.set_douvalue(double.Parse(_jscqsc.get_c_drawback().ToString()));
                            list25.Add(_downuser113);
                        }
                        else if (str284.Equals("0"))
                        {
                            drawback_downuser _downuser114 = new drawback_downuser();
                            _downuser114.set_updateUserName(this.cz_users_model.get_u_name());
                            _downuser114.set_updateUserType(this.cz_users_model.get_u_type());
                            _downuser114.set_play_id(str285);
                            _downuser114.set_opt("a");
                            _downuser114.set_douvalue(double.Parse(_jscqsc.get_a_drawback().ToString()));
                            list25.Add(_downuser114);
                            drawback_downuser _downuser115 = new drawback_downuser();
                            _downuser115.set_updateUserName(this.cz_users_model.get_u_name());
                            _downuser115.set_updateUserType(this.cz_users_model.get_u_type());
                            _downuser115.set_play_id(str285);
                            _downuser115.set_opt("b");
                            _downuser115.set_douvalue(double.Parse(_jscqsc.get_b_drawback().ToString()));
                            list25.Add(_downuser115);
                            drawback_downuser _downuser116 = new drawback_downuser();
                            _downuser116.set_updateUserName(this.cz_users_model.get_u_name());
                            _downuser116.set_updateUserType(this.cz_users_model.get_u_type());
                            _downuser116.set_play_id(str285);
                            _downuser116.set_opt("c");
                            _downuser116.set_douvalue(double.Parse(_jscqsc.get_c_drawback().ToString()));
                            list25.Add(_downuser116);
                        }
                    }
                    drawback_downuser _downuser117 = new drawback_downuser();
                    _downuser117.set_updateUserName(this.cz_users_model.get_u_name());
                    _downuser117.set_updateUserType(this.cz_users_model.get_u_type());
                    _downuser117.set_play_id(str285);
                    _downuser117.set_opt("mindz");
                    _downuser117.set_douvalue(double.Parse(_jscqsc.get_single_min_amount().ToString()));
                    list26.Add(_downuser117);
                }
                string str286 = "0";
                if (string.IsNullOrEmpty(this.cz_users_model.get_kc_kind()) || this.cz_users_model.get_kc_kind().Equals("0"))
                {
                    str286 = "0";
                }
                else
                {
                    str286 = this.cz_users_model.get_kc_kind();
                }
                if (!CallBLL.cz_drawback_jscqsc_bll.UpdateDataList(dictionary13, str286, flag2, list25, list26))
                {
                    mess = string.Format("快彩：修改【{0}】退水出現異常，請重試!", LSKeys.get_JSCQSC_Name());
                    isSuccess = false;
                    return;
                }
                base.user_drawback_kc_log(table26, CallBLL.cz_drawback_jscqsc_bll.GetDrawbackLists(this.cz_users_model.get_u_name(), this.jscqsc_modify_playid).Tables[0], this.cz_users_model.get_u_name(), 13);
            }
            if ((this.table_jspk10 != null) && !string.IsNullOrEmpty(this.jspk10_modify_playid))
            {
                DataTable table27 = CallBLL.cz_drawback_jspk10_bll.GetDrawbackList(this.cz_users_model.get_sup_name(), this.jspk10_modify_playid).Tables[0];
                DataTable table28 = CallBLL.cz_drawback_jspk10_bll.GetDrawbackList(this.cz_users_model.get_u_name(), this.jspk10_modify_playid).Tables[0];
                Dictionary<string, cz_drawback_jspk10> dictionary14 = new Dictionary<string, cz_drawback_jspk10>();
                List<drawback_downuser> list27 = new List<drawback_downuser>();
                List<drawback_downuser> list28 = new List<drawback_downuser>();
                foreach (DataRow row14 in table27.Rows)
                {
                    string str287 = row14["play_id"].ToString();
                    string str288 = row14["a_drawback"].ToString();
                    string str289 = row14["b_drawback"].ToString();
                    string str290 = row14["c_drawback"].ToString();
                    string str291 = row14["single_max_amount"].ToString();
                    string str292 = row14["single_phase_amount"].ToString();
                    string str293 = row14["single_min_amount"].ToString();
                    string str294 = LSRequest.qq(string.Format("jspk10_a_{0}", str287));
                    string str295 = LSRequest.qq(string.Format("jspk10_b_{0}", str287));
                    string str296 = LSRequest.qq(string.Format("jspk10_c_{0}", str287));
                    string str297 = LSRequest.qq(string.Format("jspk10_max_amount_{0}", str287));
                    string str298 = LSRequest.qq(string.Format("jspk10_phase_amount_{0}", str287));
                    string str299 = LSRequest.qq(string.Format("jspk10_single_min_amount_{0}", str287.ToLower()));
                    DataRow[] rowArray14 = table28.Select(string.Format(" play_id={0} ", str287));
                    string str300 = rowArray14[0]["a_drawback"].ToString();
                    string str301 = rowArray14[0]["b_drawback"].ToString();
                    string str302 = rowArray14[0]["c_drawback"].ToString();
                    string str303 = rowArray14[0]["single_max_amount"].ToString();
                    string str304 = rowArray14[0]["single_phase_amount"].ToString();
                    string str305 = rowArray14[0]["single_min_amount"].ToString();
                    if ((((double.Parse(str300) == double.Parse(str294)) && (double.Parse(str301) == double.Parse(str295))) && ((double.Parse(str302) == double.Parse(str296)) && (double.Parse(str303) == double.Parse(str297)))) && ((double.Parse(str304) == double.Parse(str298)) && (double.Parse(str305) == double.Parse(str299))))
                    {
                        continue;
                    }
                    if (string.IsNullOrEmpty(this.cz_users_model.get_kc_kind()) || this.cz_users_model.get_kc_kind().Equals("0"))
                    {
                        if (double.Parse(str294) > double.Parse(str288))
                        {
                            str294 = str288;
                        }
                        if (double.Parse(str295) > double.Parse(str289))
                        {
                            str295 = str289;
                        }
                        if (double.Parse(str296) > double.Parse(str290))
                        {
                            str296 = str290;
                        }
                    }
                    else
                    {
                        str507 = this.cz_users_model.get_kc_kind().ToLower();
                        if (str507 != null)
                        {
                            if (!(str507 == "a"))
                            {
                                if (str507 == "b")
                                {
                                    goto Label_98D8;
                                }
                                if (str507 == "c")
                                {
                                    goto Label_98F6;
                                }
                            }
                            else if (double.Parse(str294) > double.Parse(str288))
                            {
                                str294 = str288;
                            }
                        }
                    }
                    goto Label_9912;
                Label_98D8:
                    if (double.Parse(str295) > double.Parse(str289))
                    {
                        str295 = str289;
                    }
                    goto Label_9912;
                Label_98F6:
                    if (double.Parse(str296) > double.Parse(str290))
                    {
                        str296 = str290;
                    }
                Label_9912:
                    if (double.Parse(str297) > double.Parse(str291))
                    {
                        str297 = str291;
                    }
                    if (double.Parse(str298) > double.Parse(str292))
                    {
                        str298 = str292;
                    }
                    if (double.Parse(str299) < double.Parse(str293))
                    {
                        str299 = str293;
                    }
                    cz_drawback_jspk10 _jspk = new cz_drawback_jspk10();
                    _jspk.set_play_id(new int?(Convert.ToInt32(str287)));
                    if (!string.IsNullOrEmpty(str294))
                    {
                        _jspk.set_a_drawback(new decimal?(Convert.ToDecimal(str294)));
                    }
                    if (!string.IsNullOrEmpty(str295))
                    {
                        _jspk.set_b_drawback(new decimal?(Convert.ToDecimal(str295)));
                    }
                    if (!string.IsNullOrEmpty(str296))
                    {
                        _jspk.set_c_drawback(new decimal?(Convert.ToDecimal(str296)));
                    }
                    _jspk.set_single_max_amount(new decimal?(Convert.ToDecimal(str297)));
                    _jspk.set_single_phase_amount(new decimal?(Convert.ToDecimal(str298)));
                    _jspk.set_single_min_amount(new decimal?(Convert.ToDecimal(str299)));
                    _jspk.set_u_name(this.cz_users_model.get_u_name());
                    string str306 = "0";
                    if (string.IsNullOrEmpty(this.cz_users_model.get_kc_kind()) || this.cz_users_model.get_kc_kind().Equals("0"))
                    {
                        str306 = "0";
                    }
                    else
                    {
                        str306 = this.cz_users_model.get_kc_kind();
                    }
                    string str307 = "";
                    str507 = str287;
                    if (str507 == null)
                    {
                        goto Label_9B13;
                    }
                    if (!(str507 == "1"))
                    {
                        if (str507 == "2")
                        {
                            goto Label_9AF2;
                        }
                        if (str507 == "3")
                        {
                            goto Label_9AFD;
                        }
                        if (str507 == "4")
                        {
                            goto Label_9B08;
                        }
                        goto Label_9B13;
                    }
                    str307 = "1,5,9,13,17,21,24,27,30,33";
                    goto Label_9B1B;
                Label_9AF2:
                    str307 = "2,6,10,14,18,22,25,28,31,34";
                    goto Label_9B1B;
                Label_9AFD:
                    str307 = "3,7,11,15,19,23,26,29,32,35";
                    goto Label_9B1B;
                Label_9B08:
                    str307 = "4,8,12,16,20";
                    goto Label_9B1B;
                Label_9B13:
                    str307 = str287;
                Label_9B1B:
                    dictionary14.Add(str307, _jspk);
                    if (!this.cz_users_model.get_u_type().Equals("hy"))
                    {
                        drawback_downuser _downuser118 = new drawback_downuser();
                        _downuser118.set_updateUserName(this.cz_users_model.get_u_name());
                        _downuser118.set_updateUserType(this.cz_users_model.get_u_type());
                        _downuser118.set_play_id(str307);
                        _downuser118.set_opt("dq");
                        _downuser118.set_douvalue(double.Parse(_jspk.get_single_phase_amount().ToString()));
                        list27.Add(_downuser118);
                        drawback_downuser _downuser119 = new drawback_downuser();
                        _downuser119.set_updateUserName(this.cz_users_model.get_u_name());
                        _downuser119.set_updateUserType(this.cz_users_model.get_u_type());
                        _downuser119.set_play_id(str307);
                        _downuser119.set_opt("dz");
                        _downuser119.set_douvalue(double.Parse(_jspk.get_single_max_amount().ToString()));
                        list27.Add(_downuser119);
                        if (str306.ToLower().Equals("a"))
                        {
                            drawback_downuser _downuser120 = new drawback_downuser();
                            _downuser120.set_updateUserName(this.cz_users_model.get_u_name());
                            _downuser120.set_updateUserType(this.cz_users_model.get_u_type());
                            _downuser120.set_play_id(str307);
                            _downuser120.set_opt("a");
                            _downuser120.set_douvalue(double.Parse(_jspk.get_a_drawback().ToString()));
                            list27.Add(_downuser120);
                        }
                        else if (str306.ToLower().Equals("b"))
                        {
                            drawback_downuser _downuser121 = new drawback_downuser();
                            _downuser121.set_updateUserName(this.cz_users_model.get_u_name());
                            _downuser121.set_updateUserType(this.cz_users_model.get_u_type());
                            _downuser121.set_play_id(str307);
                            _downuser121.set_opt("b");
                            _downuser121.set_douvalue(double.Parse(_jspk.get_b_drawback().ToString()));
                            list27.Add(_downuser121);
                        }
                        else if (str306.ToLower().Equals("c"))
                        {
                            drawback_downuser _downuser122 = new drawback_downuser();
                            _downuser122.set_updateUserName(this.cz_users_model.get_u_name());
                            _downuser122.set_updateUserType(this.cz_users_model.get_u_type());
                            _downuser122.set_play_id(str307);
                            _downuser122.set_opt("c");
                            _downuser122.set_douvalue(double.Parse(_jspk.get_c_drawback().ToString()));
                            list27.Add(_downuser122);
                        }
                        else if (str306.Equals("0"))
                        {
                            drawback_downuser _downuser123 = new drawback_downuser();
                            _downuser123.set_updateUserName(this.cz_users_model.get_u_name());
                            _downuser123.set_updateUserType(this.cz_users_model.get_u_type());
                            _downuser123.set_play_id(str307);
                            _downuser123.set_opt("a");
                            _downuser123.set_douvalue(double.Parse(_jspk.get_a_drawback().ToString()));
                            list27.Add(_downuser123);
                            drawback_downuser _downuser124 = new drawback_downuser();
                            _downuser124.set_updateUserName(this.cz_users_model.get_u_name());
                            _downuser124.set_updateUserType(this.cz_users_model.get_u_type());
                            _downuser124.set_play_id(str307);
                            _downuser124.set_opt("b");
                            _downuser124.set_douvalue(double.Parse(_jspk.get_b_drawback().ToString()));
                            list27.Add(_downuser124);
                            drawback_downuser _downuser125 = new drawback_downuser();
                            _downuser125.set_updateUserName(this.cz_users_model.get_u_name());
                            _downuser125.set_updateUserType(this.cz_users_model.get_u_type());
                            _downuser125.set_play_id(str307);
                            _downuser125.set_opt("c");
                            _downuser125.set_douvalue(double.Parse(_jspk.get_c_drawback().ToString()));
                            list27.Add(_downuser125);
                        }
                    }
                    drawback_downuser _downuser126 = new drawback_downuser();
                    _downuser126.set_updateUserName(this.cz_users_model.get_u_name());
                    _downuser126.set_updateUserType(this.cz_users_model.get_u_type());
                    _downuser126.set_play_id(str307);
                    _downuser126.set_opt("mindz");
                    _downuser126.set_douvalue(double.Parse(_jspk.get_single_min_amount().ToString()));
                    list28.Add(_downuser126);
                }
                string str308 = "0";
                if (string.IsNullOrEmpty(this.cz_users_model.get_kc_kind()) || this.cz_users_model.get_kc_kind().Equals("0"))
                {
                    str308 = "0";
                }
                else
                {
                    str308 = this.cz_users_model.get_kc_kind();
                }
                if (!CallBLL.cz_drawback_jspk10_bll.UpdateDataList(dictionary14, str308, flag2, list27, list28))
                {
                    mess = string.Format("快彩：修改【{0}】退水出現異常，請重試!", LSKeys.get_JSPK10_Name());
                    isSuccess = false;
                    return;
                }
                base.user_drawback_kc_log(table28, CallBLL.cz_drawback_jspk10_bll.GetDrawbackList(this.cz_users_model.get_u_name(), this.jspk10_modify_playid).Tables[0], this.cz_users_model.get_u_name(), 12);
            }
            if ((this.table_jssfc != null) && !string.IsNullOrEmpty(this.jssfc_modify_playid))
            {
                DataTable table29 = CallBLL.cz_drawback_jssfc_bll.GetDrawbackByPlayIds(this.cz_users_model.get_sup_name(), this.jssfc_modify_playid, "").Tables[0];
                DataTable table30 = CallBLL.cz_drawback_jssfc_bll.GetDrawbackByPlayIds(this.cz_users_model.get_u_name(), this.jssfc_modify_playid, "").Tables[0];
                Dictionary<string, cz_drawback_jssfc> dictionary15 = new Dictionary<string, cz_drawback_jssfc>();
                List<drawback_downuser> list29 = new List<drawback_downuser>();
                List<drawback_downuser> list30 = new List<drawback_downuser>();
                foreach (DataRow row15 in table29.Rows)
                {
                    string str309 = row15["play_id"].ToString();
                    string str310 = row15["a_drawback"].ToString();
                    string str311 = row15["b_drawback"].ToString();
                    string str312 = row15["c_drawback"].ToString();
                    string str313 = row15["single_max_amount"].ToString();
                    string str314 = row15["single_phase_amount"].ToString();
                    string str315 = row15["single_min_amount"].ToString();
                    string str316 = LSRequest.qq(string.Format("jssfc_a_{0}", str309));
                    string str317 = LSRequest.qq(string.Format("jssfc_b_{0}", str309));
                    string str318 = LSRequest.qq(string.Format("jssfc_c_{0}", str309));
                    string str319 = LSRequest.qq(string.Format("jssfc_max_amount_{0}", str309));
                    string str320 = LSRequest.qq(string.Format("jssfc_phase_amount_{0}", str309));
                    string str321 = LSRequest.qq(string.Format("jssfc_single_min_amount_{0}", str309.ToLower()));
                    DataRow[] rowArray15 = table30.Select(string.Format(" play_id={0} ", str309));
                    string str322 = rowArray15[0]["a_drawback"].ToString();
                    string str323 = rowArray15[0]["b_drawback"].ToString();
                    string str324 = rowArray15[0]["c_drawback"].ToString();
                    string str325 = rowArray15[0]["single_max_amount"].ToString();
                    string str326 = rowArray15[0]["single_phase_amount"].ToString();
                    string str327 = rowArray15[0]["single_min_amount"].ToString();
                    if ((((double.Parse(str322) == double.Parse(str316)) && (double.Parse(str323) == double.Parse(str317))) && ((double.Parse(str324) == double.Parse(str318)) && (double.Parse(str325) == double.Parse(str319)))) && ((double.Parse(str326) == double.Parse(str320)) && (double.Parse(str327) == double.Parse(str321))))
                    {
                        continue;
                    }
                    if (string.IsNullOrEmpty(this.cz_users_model.get_kc_kind()) || this.cz_users_model.get_kc_kind().Equals("0"))
                    {
                        if (double.Parse(str316) > double.Parse(str310))
                        {
                            str316 = str310;
                        }
                        if (double.Parse(str317) > double.Parse(str311))
                        {
                            str317 = str311;
                        }
                        if (double.Parse(str318) > double.Parse(str312))
                        {
                            str318 = str312;
                        }
                    }
                    else
                    {
                        str507 = this.cz_users_model.get_kc_kind().ToLower();
                        if (str507 != null)
                        {
                            if (!(str507 == "a"))
                            {
                                if (str507 == "b")
                                {
                                    goto Label_A54B;
                                }
                                if (str507 == "c")
                                {
                                    goto Label_A569;
                                }
                            }
                            else if (double.Parse(str316) > double.Parse(str310))
                            {
                                str316 = str310;
                            }
                        }
                    }
                    goto Label_A585;
                Label_A54B:
                    if (double.Parse(str317) > double.Parse(str311))
                    {
                        str317 = str311;
                    }
                    goto Label_A585;
                Label_A569:
                    if (double.Parse(str318) > double.Parse(str312))
                    {
                        str318 = str312;
                    }
                Label_A585:
                    if (double.Parse(str319) > double.Parse(str313))
                    {
                        str319 = str313;
                    }
                    if (double.Parse(str320) > double.Parse(str314))
                    {
                        str320 = str314;
                    }
                    if (double.Parse(str321) < double.Parse(str315))
                    {
                        str321 = str315;
                    }
                    cz_drawback_jssfc _jssfc = new cz_drawback_jssfc();
                    _jssfc.set_play_id(new int?(Convert.ToInt32(str309)));
                    if (!string.IsNullOrEmpty(str316))
                    {
                        _jssfc.set_a_drawback(new decimal?(Convert.ToDecimal(str316)));
                    }
                    if (!string.IsNullOrEmpty(str317))
                    {
                        _jssfc.set_b_drawback(new decimal?(Convert.ToDecimal(str317)));
                    }
                    if (!string.IsNullOrEmpty(str318))
                    {
                        _jssfc.set_c_drawback(new decimal?(Convert.ToDecimal(str318)));
                    }
                    _jssfc.set_single_max_amount(new decimal?(Convert.ToDecimal(str319)));
                    _jssfc.set_single_phase_amount(new decimal?(Convert.ToDecimal(str320)));
                    _jssfc.set_single_min_amount(new decimal?(Convert.ToDecimal(str321)));
                    _jssfc.set_u_name(this.cz_users_model.get_u_name());
                    string str328 = "0";
                    if (string.IsNullOrEmpty(this.cz_users_model.get_kc_kind()) || this.cz_users_model.get_kc_kind().Equals("0"))
                    {
                        str328 = "0";
                    }
                    else
                    {
                        str328 = this.cz_users_model.get_kc_kind();
                    }
                    string str329 = "";
                    switch (str309)
                    {
                        case "81":
                            str329 = "81,86,91,96,101,106,111,116";
                            break;

                        case "82":
                            str329 = "82,87,92,97,102,107,112,117";
                            break;

                        case "83":
                            str329 = "83,88,93,98,103,108,113,118";
                            break;

                        case "84":
                            str329 = "84,89,94,99,104,109,114,119";
                            break;

                        case "85":
                            str329 = "85,90,95,100,105,110,115,120";
                            break;

                        case "121":
                            str329 = "121,123,125,127,129,131,133,135";
                            break;

                        case "122":
                            str329 = "122,124,126,128,130,132,134,136";
                            break;

                        default:
                            str329 = str309;
                            break;
                    }
                    dictionary15.Add(str329, _jssfc);
                    if (!this.cz_users_model.get_u_type().Equals("hy"))
                    {
                        drawback_downuser _downuser127 = new drawback_downuser();
                        _downuser127.set_updateUserName(this.cz_users_model.get_u_name());
                        _downuser127.set_updateUserType(this.cz_users_model.get_u_type());
                        _downuser127.set_play_id(str329);
                        _downuser127.set_opt("dq");
                        _downuser127.set_douvalue(double.Parse(_jssfc.get_single_phase_amount().ToString()));
                        list29.Add(_downuser127);
                        drawback_downuser _downuser128 = new drawback_downuser();
                        _downuser128.set_updateUserName(this.cz_users_model.get_u_name());
                        _downuser128.set_updateUserType(this.cz_users_model.get_u_type());
                        _downuser128.set_play_id(str329);
                        _downuser128.set_opt("dz");
                        _downuser128.set_douvalue(double.Parse(_jssfc.get_single_max_amount().ToString()));
                        list29.Add(_downuser128);
                        if (str328.ToLower().Equals("a"))
                        {
                            drawback_downuser _downuser129 = new drawback_downuser();
                            _downuser129.set_updateUserName(this.cz_users_model.get_u_name());
                            _downuser129.set_updateUserType(this.cz_users_model.get_u_type());
                            _downuser129.set_play_id(str329);
                            _downuser129.set_opt("a");
                            _downuser129.set_douvalue(double.Parse(_jssfc.get_a_drawback().ToString()));
                            list29.Add(_downuser129);
                        }
                        else if (str328.ToLower().Equals("b"))
                        {
                            drawback_downuser _downuser130 = new drawback_downuser();
                            _downuser130.set_updateUserName(this.cz_users_model.get_u_name());
                            _downuser130.set_updateUserType(this.cz_users_model.get_u_type());
                            _downuser130.set_play_id(str329);
                            _downuser130.set_opt("b");
                            _downuser130.set_douvalue(double.Parse(_jssfc.get_b_drawback().ToString()));
                            list29.Add(_downuser130);
                        }
                        else if (str328.ToLower().Equals("c"))
                        {
                            drawback_downuser _downuser131 = new drawback_downuser();
                            _downuser131.set_updateUserName(this.cz_users_model.get_u_name());
                            _downuser131.set_updateUserType(this.cz_users_model.get_u_type());
                            _downuser131.set_play_id(str329);
                            _downuser131.set_opt("c");
                            _downuser131.set_douvalue(double.Parse(_jssfc.get_c_drawback().ToString()));
                            list29.Add(_downuser131);
                        }
                        else if (str328.Equals("0"))
                        {
                            drawback_downuser _downuser132 = new drawback_downuser();
                            _downuser132.set_updateUserName(this.cz_users_model.get_u_name());
                            _downuser132.set_updateUserType(this.cz_users_model.get_u_type());
                            _downuser132.set_play_id(str329);
                            _downuser132.set_opt("a");
                            _downuser132.set_douvalue(double.Parse(_jssfc.get_a_drawback().ToString()));
                            list29.Add(_downuser132);
                            drawback_downuser _downuser133 = new drawback_downuser();
                            _downuser133.set_updateUserName(this.cz_users_model.get_u_name());
                            _downuser133.set_updateUserType(this.cz_users_model.get_u_type());
                            _downuser133.set_play_id(str329);
                            _downuser133.set_opt("b");
                            _downuser133.set_douvalue(double.Parse(_jssfc.get_b_drawback().ToString()));
                            list29.Add(_downuser133);
                            drawback_downuser _downuser134 = new drawback_downuser();
                            _downuser134.set_updateUserName(this.cz_users_model.get_u_name());
                            _downuser134.set_updateUserType(this.cz_users_model.get_u_type());
                            _downuser134.set_play_id(str329);
                            _downuser134.set_opt("c");
                            _downuser134.set_douvalue(double.Parse(_jssfc.get_c_drawback().ToString()));
                            list29.Add(_downuser134);
                        }
                    }
                    drawback_downuser _downuser135 = new drawback_downuser();
                    _downuser135.set_updateUserName(this.cz_users_model.get_u_name());
                    _downuser135.set_updateUserType(this.cz_users_model.get_u_type());
                    _downuser135.set_play_id(str329);
                    _downuser135.set_opt("mindz");
                    _downuser135.set_douvalue(double.Parse(_jssfc.get_single_min_amount().ToString()));
                    list30.Add(_downuser135);
                }
                string str330 = "0";
                if (string.IsNullOrEmpty(this.cz_users_model.get_kc_kind()) || this.cz_users_model.get_kc_kind().Equals("0"))
                {
                    str330 = "0";
                }
                else
                {
                    str330 = this.cz_users_model.get_kc_kind();
                }
                if (!CallBLL.cz_drawback_jssfc_bll.UpdateDataList(dictionary15, str330, flag2, list29, list30))
                {
                    mess = string.Format("快彩：修改【{0}】退水出現異常，請重試!", LSKeys.get_JSSFC_Name());
                    isSuccess = false;
                    return;
                }
                base.user_drawback_kc_log(table30, CallBLL.cz_drawback_jssfc_bll.GetDrawbackByPlayIds(this.cz_users_model.get_u_name(), this.jssfc_modify_playid, "").Tables[0], this.cz_users_model.get_u_name(), 14);
            }
            if ((this.table_jsft2 != null) && !string.IsNullOrEmpty(this.jsft2_modify_playid))
            {
                DataTable table31 = CallBLL.cz_drawback_jsft2_bll.GetDrawbackList(this.cz_users_model.get_sup_name(), this.jsft2_modify_playid).Tables[0];
                DataTable table32 = CallBLL.cz_drawback_jsft2_bll.GetDrawbackList(this.cz_users_model.get_u_name(), this.jsft2_modify_playid).Tables[0];
                Dictionary<string, cz_drawback_jsft2> dictionary16 = new Dictionary<string, cz_drawback_jsft2>();
                List<drawback_downuser> list31 = new List<drawback_downuser>();
                List<drawback_downuser> list32 = new List<drawback_downuser>();
                foreach (DataRow row16 in table31.Rows)
                {
                    string str331 = row16["play_id"].ToString();
                    string str332 = row16["a_drawback"].ToString();
                    string str333 = row16["b_drawback"].ToString();
                    string str334 = row16["c_drawback"].ToString();
                    string str335 = row16["single_max_amount"].ToString();
                    string str336 = row16["single_phase_amount"].ToString();
                    string str337 = row16["single_min_amount"].ToString();
                    string str338 = LSRequest.qq(string.Format("jsft2_a_{0}", str331));
                    string str339 = LSRequest.qq(string.Format("jsft2_b_{0}", str331));
                    string str340 = LSRequest.qq(string.Format("jsft2_c_{0}", str331));
                    string str341 = LSRequest.qq(string.Format("jsft2_max_amount_{0}", str331));
                    string str342 = LSRequest.qq(string.Format("jsft2_phase_amount_{0}", str331));
                    string str343 = LSRequest.qq(string.Format("jsft2_single_min_amount_{0}", str331.ToLower()));
                    DataRow[] rowArray16 = table32.Select(string.Format(" play_id={0} ", str331));
                    string str344 = rowArray16[0]["a_drawback"].ToString();
                    string str345 = rowArray16[0]["b_drawback"].ToString();
                    string str346 = rowArray16[0]["c_drawback"].ToString();
                    string str347 = rowArray16[0]["single_max_amount"].ToString();
                    string str348 = rowArray16[0]["single_phase_amount"].ToString();
                    string str349 = rowArray16[0]["single_min_amount"].ToString();
                    if ((((double.Parse(str344) == double.Parse(str338)) && (double.Parse(str345) == double.Parse(str339))) && ((double.Parse(str346) == double.Parse(str340)) && (double.Parse(str347) == double.Parse(str341)))) && ((double.Parse(str348) == double.Parse(str342)) && (double.Parse(str349) == double.Parse(str343))))
                    {
                        continue;
                    }
                    if (string.IsNullOrEmpty(this.cz_users_model.get_kc_kind()) || this.cz_users_model.get_kc_kind().Equals("0"))
                    {
                        if (double.Parse(str338) > double.Parse(str332))
                        {
                            str338 = str332;
                        }
                        if (double.Parse(str339) > double.Parse(str333))
                        {
                            str339 = str333;
                        }
                        if (double.Parse(str340) > double.Parse(str334))
                        {
                            str340 = str334;
                        }
                    }
                    else
                    {
                        str507 = this.cz_users_model.get_kc_kind().ToLower();
                        if (str507 != null)
                        {
                            if (!(str507 == "a"))
                            {
                                if (str507 == "b")
                                {
                                    goto Label_B242;
                                }
                                if (str507 == "c")
                                {
                                    goto Label_B260;
                                }
                            }
                            else if (double.Parse(str338) > double.Parse(str332))
                            {
                                str338 = str332;
                            }
                        }
                    }
                    goto Label_B27C;
                Label_B242:
                    if (double.Parse(str339) > double.Parse(str333))
                    {
                        str339 = str333;
                    }
                    goto Label_B27C;
                Label_B260:
                    if (double.Parse(str340) > double.Parse(str334))
                    {
                        str340 = str334;
                    }
                Label_B27C:
                    if (double.Parse(str341) > double.Parse(str335))
                    {
                        str341 = str335;
                    }
                    if (double.Parse(str342) > double.Parse(str336))
                    {
                        str342 = str336;
                    }
                    if (double.Parse(str343) < double.Parse(str337))
                    {
                        str343 = str337;
                    }
                    cz_drawback_jsft2 _jsft = new cz_drawback_jsft2();
                    _jsft.set_play_id(new int?(Convert.ToInt32(str331)));
                    if (!string.IsNullOrEmpty(str338))
                    {
                        _jsft.set_a_drawback(new decimal?(Convert.ToDecimal(str338)));
                    }
                    if (!string.IsNullOrEmpty(str339))
                    {
                        _jsft.set_b_drawback(new decimal?(Convert.ToDecimal(str339)));
                    }
                    if (!string.IsNullOrEmpty(str340))
                    {
                        _jsft.set_c_drawback(new decimal?(Convert.ToDecimal(str340)));
                    }
                    _jsft.set_single_max_amount(new decimal?(Convert.ToDecimal(str341)));
                    _jsft.set_single_phase_amount(new decimal?(Convert.ToDecimal(str342)));
                    _jsft.set_single_min_amount(new decimal?(Convert.ToDecimal(str343)));
                    _jsft.set_u_name(this.cz_users_model.get_u_name());
                    string str350 = "0";
                    if (string.IsNullOrEmpty(this.cz_users_model.get_kc_kind()) || this.cz_users_model.get_kc_kind().Equals("0"))
                    {
                        str350 = "0";
                    }
                    else
                    {
                        str350 = this.cz_users_model.get_kc_kind();
                    }
                    string str351 = "";
                    str507 = str331;
                    if (str507 == null)
                    {
                        goto Label_B47D;
                    }
                    if (!(str507 == "1"))
                    {
                        if (str507 == "2")
                        {
                            goto Label_B45C;
                        }
                        if (str507 == "3")
                        {
                            goto Label_B467;
                        }
                        if (str507 == "4")
                        {
                            goto Label_B472;
                        }
                        goto Label_B47D;
                    }
                    str351 = "1,5,9,13,17,21,24,27,30,33";
                    goto Label_B485;
                Label_B45C:
                    str351 = "2,6,10,14,18,22,25,28,31,34";
                    goto Label_B485;
                Label_B467:
                    str351 = "3,7,11,15,19,23,26,29,32,35";
                    goto Label_B485;
                Label_B472:
                    str351 = "4,8,12,16,20";
                    goto Label_B485;
                Label_B47D:
                    str351 = str331;
                Label_B485:
                    dictionary16.Add(str351, _jsft);
                    if (!this.cz_users_model.get_u_type().Equals("hy"))
                    {
                        drawback_downuser _downuser136 = new drawback_downuser();
                        _downuser136.set_updateUserName(this.cz_users_model.get_u_name());
                        _downuser136.set_updateUserType(this.cz_users_model.get_u_type());
                        _downuser136.set_play_id(str351);
                        _downuser136.set_opt("dq");
                        _downuser136.set_douvalue(double.Parse(_jsft.get_single_phase_amount().ToString()));
                        list31.Add(_downuser136);
                        drawback_downuser _downuser137 = new drawback_downuser();
                        _downuser137.set_updateUserName(this.cz_users_model.get_u_name());
                        _downuser137.set_updateUserType(this.cz_users_model.get_u_type());
                        _downuser137.set_play_id(str351);
                        _downuser137.set_opt("dz");
                        _downuser137.set_douvalue(double.Parse(_jsft.get_single_max_amount().ToString()));
                        list31.Add(_downuser137);
                        if (str350.ToLower().Equals("a"))
                        {
                            drawback_downuser _downuser138 = new drawback_downuser();
                            _downuser138.set_updateUserName(this.cz_users_model.get_u_name());
                            _downuser138.set_updateUserType(this.cz_users_model.get_u_type());
                            _downuser138.set_play_id(str351);
                            _downuser138.set_opt("a");
                            _downuser138.set_douvalue(double.Parse(_jsft.get_a_drawback().ToString()));
                            list31.Add(_downuser138);
                        }
                        else if (str350.ToLower().Equals("b"))
                        {
                            drawback_downuser _downuser139 = new drawback_downuser();
                            _downuser139.set_updateUserName(this.cz_users_model.get_u_name());
                            _downuser139.set_updateUserType(this.cz_users_model.get_u_type());
                            _downuser139.set_play_id(str351);
                            _downuser139.set_opt("b");
                            _downuser139.set_douvalue(double.Parse(_jsft.get_b_drawback().ToString()));
                            list31.Add(_downuser139);
                        }
                        else if (str350.ToLower().Equals("c"))
                        {
                            drawback_downuser _downuser140 = new drawback_downuser();
                            _downuser140.set_updateUserName(this.cz_users_model.get_u_name());
                            _downuser140.set_updateUserType(this.cz_users_model.get_u_type());
                            _downuser140.set_play_id(str351);
                            _downuser140.set_opt("c");
                            _downuser140.set_douvalue(double.Parse(_jsft.get_c_drawback().ToString()));
                            list31.Add(_downuser140);
                        }
                        else if (str350.Equals("0"))
                        {
                            drawback_downuser _downuser141 = new drawback_downuser();
                            _downuser141.set_updateUserName(this.cz_users_model.get_u_name());
                            _downuser141.set_updateUserType(this.cz_users_model.get_u_type());
                            _downuser141.set_play_id(str351);
                            _downuser141.set_opt("a");
                            _downuser141.set_douvalue(double.Parse(_jsft.get_a_drawback().ToString()));
                            list31.Add(_downuser141);
                            drawback_downuser _downuser142 = new drawback_downuser();
                            _downuser142.set_updateUserName(this.cz_users_model.get_u_name());
                            _downuser142.set_updateUserType(this.cz_users_model.get_u_type());
                            _downuser142.set_play_id(str351);
                            _downuser142.set_opt("b");
                            _downuser142.set_douvalue(double.Parse(_jsft.get_b_drawback().ToString()));
                            list31.Add(_downuser142);
                            drawback_downuser _downuser143 = new drawback_downuser();
                            _downuser143.set_updateUserName(this.cz_users_model.get_u_name());
                            _downuser143.set_updateUserType(this.cz_users_model.get_u_type());
                            _downuser143.set_play_id(str351);
                            _downuser143.set_opt("c");
                            _downuser143.set_douvalue(double.Parse(_jsft.get_c_drawback().ToString()));
                            list31.Add(_downuser143);
                        }
                    }
                    drawback_downuser _downuser144 = new drawback_downuser();
                    _downuser144.set_updateUserName(this.cz_users_model.get_u_name());
                    _downuser144.set_updateUserType(this.cz_users_model.get_u_type());
                    _downuser144.set_play_id(str351);
                    _downuser144.set_opt("mindz");
                    _downuser144.set_douvalue(double.Parse(_jsft.get_single_min_amount().ToString()));
                    list32.Add(_downuser144);
                }
                string str352 = "0";
                if (string.IsNullOrEmpty(this.cz_users_model.get_kc_kind()) || this.cz_users_model.get_kc_kind().Equals("0"))
                {
                    str352 = "0";
                }
                else
                {
                    str352 = this.cz_users_model.get_kc_kind();
                }
                if (!CallBLL.cz_drawback_jsft2_bll.UpdateDataList(dictionary16, str352, flag2, list31, list32))
                {
                    mess = string.Format("快彩：修改【{0}】退水出現異常，請重試!", LSKeys.get_JSFT2_Name());
                    isSuccess = false;
                    return;
                }
                base.user_drawback_kc_log(table32, CallBLL.cz_drawback_jsft2_bll.GetDrawbackList(this.cz_users_model.get_u_name(), this.jsft2_modify_playid).Tables[0], this.cz_users_model.get_u_name(), 15);
            }
            if ((this.table_car168 != null) && !string.IsNullOrEmpty(this.car168_modify_playid))
            {
                DataTable table33 = CallBLL.cz_drawback_car168_bll.GetDrawbackList(this.cz_users_model.get_sup_name(), this.car168_modify_playid).Tables[0];
                DataTable table34 = CallBLL.cz_drawback_car168_bll.GetDrawbackList(this.cz_users_model.get_u_name(), this.car168_modify_playid).Tables[0];
                Dictionary<string, cz_drawback_car168> dictionary17 = new Dictionary<string, cz_drawback_car168>();
                List<drawback_downuser> list33 = new List<drawback_downuser>();
                List<drawback_downuser> list34 = new List<drawback_downuser>();
                foreach (DataRow row17 in table33.Rows)
                {
                    string str353 = row17["play_id"].ToString();
                    string str354 = row17["a_drawback"].ToString();
                    string str355 = row17["b_drawback"].ToString();
                    string str356 = row17["c_drawback"].ToString();
                    string str357 = row17["single_max_amount"].ToString();
                    string str358 = row17["single_phase_amount"].ToString();
                    string str359 = row17["single_min_amount"].ToString();
                    string str360 = LSRequest.qq(string.Format("car168_a_{0}", str353));
                    string str361 = LSRequest.qq(string.Format("car168_b_{0}", str353));
                    string str362 = LSRequest.qq(string.Format("car168_c_{0}", str353));
                    string str363 = LSRequest.qq(string.Format("car168_max_amount_{0}", str353));
                    string str364 = LSRequest.qq(string.Format("car168_phase_amount_{0}", str353));
                    string str365 = LSRequest.qq(string.Format("car168_single_min_amount_{0}", str353.ToLower()));
                    DataRow[] rowArray17 = table34.Select(string.Format(" play_id={0} ", str353));
                    string str366 = rowArray17[0]["a_drawback"].ToString();
                    string str367 = rowArray17[0]["b_drawback"].ToString();
                    string str368 = rowArray17[0]["c_drawback"].ToString();
                    string str369 = rowArray17[0]["single_max_amount"].ToString();
                    string str370 = rowArray17[0]["single_phase_amount"].ToString();
                    string str371 = rowArray17[0]["single_min_amount"].ToString();
                    if ((((double.Parse(str366) == double.Parse(str360)) && (double.Parse(str367) == double.Parse(str361))) && ((double.Parse(str368) == double.Parse(str362)) && (double.Parse(str369) == double.Parse(str363)))) && ((double.Parse(str370) == double.Parse(str364)) && (double.Parse(str371) == double.Parse(str365))))
                    {
                        continue;
                    }
                    if (string.IsNullOrEmpty(this.cz_users_model.get_kc_kind()) || this.cz_users_model.get_kc_kind().Equals("0"))
                    {
                        if (double.Parse(str360) > double.Parse(str354))
                        {
                            str360 = str354;
                        }
                        if (double.Parse(str361) > double.Parse(str355))
                        {
                            str361 = str355;
                        }
                        if (double.Parse(str362) > double.Parse(str356))
                        {
                            str362 = str356;
                        }
                    }
                    else
                    {
                        str507 = this.cz_users_model.get_kc_kind().ToLower();
                        if (str507 != null)
                        {
                            if (!(str507 == "a"))
                            {
                                if (str507 == "b")
                                {
                                    goto Label_BEAB;
                                }
                                if (str507 == "c")
                                {
                                    goto Label_BEC9;
                                }
                            }
                            else if (double.Parse(str360) > double.Parse(str354))
                            {
                                str360 = str354;
                            }
                        }
                    }
                    goto Label_BEE5;
                Label_BEAB:
                    if (double.Parse(str361) > double.Parse(str355))
                    {
                        str361 = str355;
                    }
                    goto Label_BEE5;
                Label_BEC9:
                    if (double.Parse(str362) > double.Parse(str356))
                    {
                        str362 = str356;
                    }
                Label_BEE5:
                    if (double.Parse(str363) > double.Parse(str357))
                    {
                        str363 = str357;
                    }
                    if (double.Parse(str364) > double.Parse(str358))
                    {
                        str364 = str358;
                    }
                    if (double.Parse(str365) < double.Parse(str359))
                    {
                        str365 = str359;
                    }
                    cz_drawback_car168 _car = new cz_drawback_car168();
                    _car.set_play_id(new int?(Convert.ToInt32(str353)));
                    if (!string.IsNullOrEmpty(str360))
                    {
                        _car.set_a_drawback(new decimal?(Convert.ToDecimal(str360)));
                    }
                    if (!string.IsNullOrEmpty(str361))
                    {
                        _car.set_b_drawback(new decimal?(Convert.ToDecimal(str361)));
                    }
                    if (!string.IsNullOrEmpty(str362))
                    {
                        _car.set_c_drawback(new decimal?(Convert.ToDecimal(str362)));
                    }
                    _car.set_single_max_amount(new decimal?(Convert.ToDecimal(str363)));
                    _car.set_single_phase_amount(new decimal?(Convert.ToDecimal(str364)));
                    _car.set_single_min_amount(new decimal?(Convert.ToDecimal(str365)));
                    _car.set_u_name(this.cz_users_model.get_u_name());
                    string str372 = "0";
                    if (string.IsNullOrEmpty(this.cz_users_model.get_kc_kind()) || this.cz_users_model.get_kc_kind().Equals("0"))
                    {
                        str372 = "0";
                    }
                    else
                    {
                        str372 = this.cz_users_model.get_kc_kind();
                    }
                    string str373 = "";
                    str507 = str353;
                    if (str507 == null)
                    {
                        goto Label_C0E6;
                    }
                    if (!(str507 == "1"))
                    {
                        if (str507 == "2")
                        {
                            goto Label_C0C5;
                        }
                        if (str507 == "3")
                        {
                            goto Label_C0D0;
                        }
                        if (str507 == "4")
                        {
                            goto Label_C0DB;
                        }
                        goto Label_C0E6;
                    }
                    str373 = "1,5,9,13,17,21,24,27,30,33";
                    goto Label_C0EE;
                Label_C0C5:
                    str373 = "2,6,10,14,18,22,25,28,31,34";
                    goto Label_C0EE;
                Label_C0D0:
                    str373 = "3,7,11,15,19,23,26,29,32,35";
                    goto Label_C0EE;
                Label_C0DB:
                    str373 = "4,8,12,16,20";
                    goto Label_C0EE;
                Label_C0E6:
                    str373 = str353;
                Label_C0EE:
                    dictionary17.Add(str373, _car);
                    if (!this.cz_users_model.get_u_type().Equals("hy"))
                    {
                        drawback_downuser _downuser145 = new drawback_downuser();
                        _downuser145.set_updateUserName(this.cz_users_model.get_u_name());
                        _downuser145.set_updateUserType(this.cz_users_model.get_u_type());
                        _downuser145.set_play_id(str373);
                        _downuser145.set_opt("dq");
                        _downuser145.set_douvalue(double.Parse(_car.get_single_phase_amount().ToString()));
                        list33.Add(_downuser145);
                        drawback_downuser _downuser146 = new drawback_downuser();
                        _downuser146.set_updateUserName(this.cz_users_model.get_u_name());
                        _downuser146.set_updateUserType(this.cz_users_model.get_u_type());
                        _downuser146.set_play_id(str373);
                        _downuser146.set_opt("dz");
                        _downuser146.set_douvalue(double.Parse(_car.get_single_max_amount().ToString()));
                        list33.Add(_downuser146);
                        if (str372.ToLower().Equals("a"))
                        {
                            drawback_downuser _downuser147 = new drawback_downuser();
                            _downuser147.set_updateUserName(this.cz_users_model.get_u_name());
                            _downuser147.set_updateUserType(this.cz_users_model.get_u_type());
                            _downuser147.set_play_id(str373);
                            _downuser147.set_opt("a");
                            _downuser147.set_douvalue(double.Parse(_car.get_a_drawback().ToString()));
                            list33.Add(_downuser147);
                        }
                        else if (str372.ToLower().Equals("b"))
                        {
                            drawback_downuser _downuser148 = new drawback_downuser();
                            _downuser148.set_updateUserName(this.cz_users_model.get_u_name());
                            _downuser148.set_updateUserType(this.cz_users_model.get_u_type());
                            _downuser148.set_play_id(str373);
                            _downuser148.set_opt("b");
                            _downuser148.set_douvalue(double.Parse(_car.get_b_drawback().ToString()));
                            list33.Add(_downuser148);
                        }
                        else if (str372.ToLower().Equals("c"))
                        {
                            drawback_downuser _downuser149 = new drawback_downuser();
                            _downuser149.set_updateUserName(this.cz_users_model.get_u_name());
                            _downuser149.set_updateUserType(this.cz_users_model.get_u_type());
                            _downuser149.set_play_id(str373);
                            _downuser149.set_opt("c");
                            _downuser149.set_douvalue(double.Parse(_car.get_c_drawback().ToString()));
                            list33.Add(_downuser149);
                        }
                        else if (str372.Equals("0"))
                        {
                            drawback_downuser _downuser150 = new drawback_downuser();
                            _downuser150.set_updateUserName(this.cz_users_model.get_u_name());
                            _downuser150.set_updateUserType(this.cz_users_model.get_u_type());
                            _downuser150.set_play_id(str373);
                            _downuser150.set_opt("a");
                            _downuser150.set_douvalue(double.Parse(_car.get_a_drawback().ToString()));
                            list33.Add(_downuser150);
                            drawback_downuser _downuser151 = new drawback_downuser();
                            _downuser151.set_updateUserName(this.cz_users_model.get_u_name());
                            _downuser151.set_updateUserType(this.cz_users_model.get_u_type());
                            _downuser151.set_play_id(str373);
                            _downuser151.set_opt("b");
                            _downuser151.set_douvalue(double.Parse(_car.get_b_drawback().ToString()));
                            list33.Add(_downuser151);
                            drawback_downuser _downuser152 = new drawback_downuser();
                            _downuser152.set_updateUserName(this.cz_users_model.get_u_name());
                            _downuser152.set_updateUserType(this.cz_users_model.get_u_type());
                            _downuser152.set_play_id(str373);
                            _downuser152.set_opt("c");
                            _downuser152.set_douvalue(double.Parse(_car.get_c_drawback().ToString()));
                            list33.Add(_downuser152);
                        }
                    }
                    drawback_downuser _downuser153 = new drawback_downuser();
                    _downuser153.set_updateUserName(this.cz_users_model.get_u_name());
                    _downuser153.set_updateUserType(this.cz_users_model.get_u_type());
                    _downuser153.set_play_id(str373);
                    _downuser153.set_opt("mindz");
                    _downuser153.set_douvalue(double.Parse(_car.get_single_min_amount().ToString()));
                    list34.Add(_downuser153);
                }
                string str374 = "0";
                if (string.IsNullOrEmpty(this.cz_users_model.get_kc_kind()) || this.cz_users_model.get_kc_kind().Equals("0"))
                {
                    str374 = "0";
                }
                else
                {
                    str374 = this.cz_users_model.get_kc_kind();
                }
                if (!CallBLL.cz_drawback_car168_bll.UpdateDataList(dictionary17, str374, flag2, list33, list34))
                {
                    mess = string.Format("快彩：修改【{0}】退水出現異常，請重試!", LSKeys.get_CAR168_Name());
                    isSuccess = false;
                    return;
                }
                base.user_drawback_kc_log(table34, CallBLL.cz_drawback_car168_bll.GetDrawbackList(this.cz_users_model.get_u_name(), this.car168_modify_playid).Tables[0], this.cz_users_model.get_u_name(), 0x10);
            }
            if ((this.table_ssc168 != null) && !string.IsNullOrEmpty(this.ssc168_modify_playid))
            {
                DataTable table35 = CallBLL.cz_drawback_ssc168_bll.GetDrawbackLists(this.cz_users_model.get_sup_name(), this.ssc168_modify_playid).Tables[0];
                DataTable table36 = CallBLL.cz_drawback_ssc168_bll.GetDrawbackLists(this.cz_users_model.get_u_name(), this.ssc168_modify_playid).Tables[0];
                Dictionary<string, cz_drawback_ssc168> dictionary18 = new Dictionary<string, cz_drawback_ssc168>();
                List<drawback_downuser> list35 = new List<drawback_downuser>();
                List<drawback_downuser> list36 = new List<drawback_downuser>();
                foreach (DataRow row18 in table35.Rows)
                {
                    string str375 = row18["play_id"].ToString();
                    string str376 = row18["a_drawback"].ToString();
                    string str377 = row18["b_drawback"].ToString();
                    string str378 = row18["c_drawback"].ToString();
                    string str379 = row18["single_max_amount"].ToString();
                    string str380 = row18["single_phase_amount"].ToString();
                    string str381 = row18["single_min_amount"].ToString();
                    string str382 = LSRequest.qq(string.Format("ssc168_a_{0}", str375));
                    string str383 = LSRequest.qq(string.Format("ssc168_b_{0}", str375));
                    string str384 = LSRequest.qq(string.Format("ssc168_c_{0}", str375));
                    string str385 = LSRequest.qq(string.Format("ssc168_max_amount_{0}", str375));
                    string str386 = LSRequest.qq(string.Format("ssc168_phase_amount_{0}", str375));
                    string str387 = LSRequest.qq(string.Format("ssc168_single_min_amount_{0}", str375.ToLower()));
                    DataRow[] rowArray18 = table36.Select(string.Format(" play_id={0} ", str375));
                    string str388 = rowArray18[0]["a_drawback"].ToString();
                    string str389 = rowArray18[0]["b_drawback"].ToString();
                    string str390 = rowArray18[0]["c_drawback"].ToString();
                    string str391 = rowArray18[0]["single_max_amount"].ToString();
                    string str392 = rowArray18[0]["single_phase_amount"].ToString();
                    string str393 = rowArray18[0]["single_min_amount"].ToString();
                    if ((((double.Parse(str388) == double.Parse(str382)) && (double.Parse(str389) == double.Parse(str383))) && ((double.Parse(str390) == double.Parse(str384)) && (double.Parse(str391) == double.Parse(str385)))) && ((double.Parse(str392) == double.Parse(str386)) && (double.Parse(str393) == double.Parse(str387))))
                    {
                        continue;
                    }
                    if (string.IsNullOrEmpty(this.cz_users_model.get_kc_kind()) || this.cz_users_model.get_kc_kind().Equals("0"))
                    {
                        if (double.Parse(str382) > double.Parse(str376))
                        {
                            str382 = str376;
                        }
                        if (double.Parse(str383) > double.Parse(str377))
                        {
                            str383 = str377;
                        }
                        if (double.Parse(str384) > double.Parse(str378))
                        {
                            str384 = str378;
                        }
                    }
                    else
                    {
                        str507 = this.cz_users_model.get_kc_kind().ToLower();
                        if (str507 != null)
                        {
                            if (!(str507 == "a"))
                            {
                                if (str507 == "b")
                                {
                                    goto Label_CB14;
                                }
                                if (str507 == "c")
                                {
                                    goto Label_CB32;
                                }
                            }
                            else if (double.Parse(str382) > double.Parse(str376))
                            {
                                str382 = str376;
                            }
                        }
                    }
                    goto Label_CB4E;
                Label_CB14:
                    if (double.Parse(str383) > double.Parse(str377))
                    {
                        str383 = str377;
                    }
                    goto Label_CB4E;
                Label_CB32:
                    if (double.Parse(str384) > double.Parse(str378))
                    {
                        str384 = str378;
                    }
                Label_CB4E:
                    if (double.Parse(str385) > double.Parse(str379))
                    {
                        str385 = str379;
                    }
                    if (double.Parse(str386) > double.Parse(str380))
                    {
                        str386 = str380;
                    }
                    if (double.Parse(str387) < double.Parse(str381))
                    {
                        str387 = str381;
                    }
                    cz_drawback_ssc168 _ssc = new cz_drawback_ssc168();
                    _ssc.set_play_id(new int?(Convert.ToInt32(str375)));
                    if (!string.IsNullOrEmpty(str382))
                    {
                        _ssc.set_a_drawback(new decimal?(Convert.ToDecimal(str382)));
                    }
                    if (!string.IsNullOrEmpty(str383))
                    {
                        _ssc.set_b_drawback(new decimal?(Convert.ToDecimal(str383)));
                    }
                    if (!string.IsNullOrEmpty(str384))
                    {
                        _ssc.set_c_drawback(new decimal?(Convert.ToDecimal(str384)));
                    }
                    _ssc.set_single_max_amount(new decimal?(Convert.ToDecimal(str385)));
                    _ssc.set_single_phase_amount(new decimal?(Convert.ToDecimal(str386)));
                    _ssc.set_single_min_amount(new decimal?(Convert.ToDecimal(str387)));
                    _ssc.set_u_name(this.cz_users_model.get_u_name());
                    string str394 = "0";
                    if (string.IsNullOrEmpty(this.cz_users_model.get_kc_kind()) || this.cz_users_model.get_kc_kind().Equals("0"))
                    {
                        str394 = "0";
                    }
                    else
                    {
                        str394 = this.cz_users_model.get_kc_kind();
                    }
                    string str395 = "";
                    str507 = str375;
                    if (str507 == null)
                    {
                        goto Label_CD34;
                    }
                    if (!(str507 == "1"))
                    {
                        if (str507 == "2")
                        {
                            goto Label_CD1E;
                        }
                        if (str507 == "3")
                        {
                            goto Label_CD29;
                        }
                        goto Label_CD34;
                    }
                    str395 = "1,4,7,10,13";
                    goto Label_CD3C;
                Label_CD1E:
                    str395 = "2,5,8,11,14";
                    goto Label_CD3C;
                Label_CD29:
                    str395 = "3,6,9,12,15";
                    goto Label_CD3C;
                Label_CD34:
                    str395 = str375;
                Label_CD3C:
                    dictionary18.Add(str395, _ssc);
                    if (!this.cz_users_model.get_u_type().Equals("hy"))
                    {
                        drawback_downuser _downuser154 = new drawback_downuser();
                        _downuser154.set_updateUserName(this.cz_users_model.get_u_name());
                        _downuser154.set_updateUserType(this.cz_users_model.get_u_type());
                        _downuser154.set_play_id(str395);
                        _downuser154.set_opt("dq");
                        _downuser154.set_douvalue(double.Parse(_ssc.get_single_phase_amount().ToString()));
                        list35.Add(_downuser154);
                        drawback_downuser _downuser155 = new drawback_downuser();
                        _downuser155.set_updateUserName(this.cz_users_model.get_u_name());
                        _downuser155.set_updateUserType(this.cz_users_model.get_u_type());
                        _downuser155.set_play_id(str395);
                        _downuser155.set_opt("dz");
                        _downuser155.set_douvalue(double.Parse(_ssc.get_single_max_amount().ToString()));
                        list35.Add(_downuser155);
                        if (str394.ToLower().Equals("a"))
                        {
                            drawback_downuser _downuser156 = new drawback_downuser();
                            _downuser156.set_updateUserName(this.cz_users_model.get_u_name());
                            _downuser156.set_updateUserType(this.cz_users_model.get_u_type());
                            _downuser156.set_play_id(str395);
                            _downuser156.set_opt("a");
                            _downuser156.set_douvalue(double.Parse(_ssc.get_a_drawback().ToString()));
                            list35.Add(_downuser156);
                        }
                        else if (str394.ToLower().Equals("b"))
                        {
                            drawback_downuser _downuser157 = new drawback_downuser();
                            _downuser157.set_updateUserName(this.cz_users_model.get_u_name());
                            _downuser157.set_updateUserType(this.cz_users_model.get_u_type());
                            _downuser157.set_play_id(str395);
                            _downuser157.set_opt("b");
                            _downuser157.set_douvalue(double.Parse(_ssc.get_b_drawback().ToString()));
                            list35.Add(_downuser157);
                        }
                        else if (str394.ToLower().Equals("c"))
                        {
                            drawback_downuser _downuser158 = new drawback_downuser();
                            _downuser158.set_updateUserName(this.cz_users_model.get_u_name());
                            _downuser158.set_updateUserType(this.cz_users_model.get_u_type());
                            _downuser158.set_play_id(str395);
                            _downuser158.set_opt("c");
                            _downuser158.set_douvalue(double.Parse(_ssc.get_c_drawback().ToString()));
                            list35.Add(_downuser158);
                        }
                        else if (str394.Equals("0"))
                        {
                            drawback_downuser _downuser159 = new drawback_downuser();
                            _downuser159.set_updateUserName(this.cz_users_model.get_u_name());
                            _downuser159.set_updateUserType(this.cz_users_model.get_u_type());
                            _downuser159.set_play_id(str395);
                            _downuser159.set_opt("a");
                            _downuser159.set_douvalue(double.Parse(_ssc.get_a_drawback().ToString()));
                            list35.Add(_downuser159);
                            drawback_downuser _downuser160 = new drawback_downuser();
                            _downuser160.set_updateUserName(this.cz_users_model.get_u_name());
                            _downuser160.set_updateUserType(this.cz_users_model.get_u_type());
                            _downuser160.set_play_id(str395);
                            _downuser160.set_opt("b");
                            _downuser160.set_douvalue(double.Parse(_ssc.get_b_drawback().ToString()));
                            list35.Add(_downuser160);
                            drawback_downuser _downuser161 = new drawback_downuser();
                            _downuser161.set_updateUserName(this.cz_users_model.get_u_name());
                            _downuser161.set_updateUserType(this.cz_users_model.get_u_type());
                            _downuser161.set_play_id(str395);
                            _downuser161.set_opt("c");
                            _downuser161.set_douvalue(double.Parse(_ssc.get_c_drawback().ToString()));
                            list35.Add(_downuser161);
                        }
                    }
                    drawback_downuser _downuser162 = new drawback_downuser();
                    _downuser162.set_updateUserName(this.cz_users_model.get_u_name());
                    _downuser162.set_updateUserType(this.cz_users_model.get_u_type());
                    _downuser162.set_play_id(str395);
                    _downuser162.set_opt("mindz");
                    _downuser162.set_douvalue(double.Parse(_ssc.get_single_min_amount().ToString()));
                    list36.Add(_downuser162);
                }
                string str396 = "0";
                if (string.IsNullOrEmpty(this.cz_users_model.get_kc_kind()) || this.cz_users_model.get_kc_kind().Equals("0"))
                {
                    str396 = "0";
                }
                else
                {
                    str396 = this.cz_users_model.get_kc_kind();
                }
                if (!CallBLL.cz_drawback_ssc168_bll.UpdateDataList(dictionary18, str396, flag2, list35, list36))
                {
                    mess = string.Format("快彩：修改【{0}】退水出現異常，請重試!", LSKeys.get_SSC168_Name());
                    isSuccess = false;
                    return;
                }
                base.user_drawback_kc_log(table36, CallBLL.cz_drawback_ssc168_bll.GetDrawbackLists(this.cz_users_model.get_u_name(), this.ssc168_modify_playid).Tables[0], this.cz_users_model.get_u_name(), 0x11);
            }
            if ((this.table_vrcar != null) && !string.IsNullOrEmpty(this.vrcar_modify_playid))
            {
                DataTable table37 = CallBLL.cz_drawback_vrcar_bll.GetDrawbackList(this.cz_users_model.get_sup_name(), this.vrcar_modify_playid).Tables[0];
                DataTable table38 = CallBLL.cz_drawback_vrcar_bll.GetDrawbackList(this.cz_users_model.get_u_name(), this.vrcar_modify_playid).Tables[0];
                Dictionary<string, cz_drawback_vrcar> dictionary19 = new Dictionary<string, cz_drawback_vrcar>();
                List<drawback_downuser> list37 = new List<drawback_downuser>();
                List<drawback_downuser> list38 = new List<drawback_downuser>();
                foreach (DataRow row19 in table37.Rows)
                {
                    string str397 = row19["play_id"].ToString();
                    string str398 = row19["a_drawback"].ToString();
                    string str399 = row19["b_drawback"].ToString();
                    string str400 = row19["c_drawback"].ToString();
                    string str401 = row19["single_max_amount"].ToString();
                    string str402 = row19["single_phase_amount"].ToString();
                    string str403 = row19["single_min_amount"].ToString();
                    string str404 = LSRequest.qq(string.Format("vrcar_a_{0}", str397));
                    string str405 = LSRequest.qq(string.Format("vrcar_b_{0}", str397));
                    string str406 = LSRequest.qq(string.Format("vrcar_c_{0}", str397));
                    string str407 = LSRequest.qq(string.Format("vrcar_max_amount_{0}", str397));
                    string str408 = LSRequest.qq(string.Format("vrcar_phase_amount_{0}", str397));
                    string str409 = LSRequest.qq(string.Format("vrcar_single_min_amount_{0}", str397.ToLower()));
                    DataRow[] rowArray19 = table38.Select(string.Format(" play_id={0} ", str397));
                    string str410 = rowArray19[0]["a_drawback"].ToString();
                    string str411 = rowArray19[0]["b_drawback"].ToString();
                    string str412 = rowArray19[0]["c_drawback"].ToString();
                    string str413 = rowArray19[0]["single_max_amount"].ToString();
                    string str414 = rowArray19[0]["single_phase_amount"].ToString();
                    string str415 = rowArray19[0]["single_min_amount"].ToString();
                    if ((((double.Parse(str410) == double.Parse(str404)) && (double.Parse(str411) == double.Parse(str405))) && ((double.Parse(str412) == double.Parse(str406)) && (double.Parse(str413) == double.Parse(str407)))) && ((double.Parse(str414) == double.Parse(str408)) && (double.Parse(str415) == double.Parse(str409))))
                    {
                        continue;
                    }
                    if (string.IsNullOrEmpty(this.cz_users_model.get_kc_kind()) || this.cz_users_model.get_kc_kind().Equals("0"))
                    {
                        if (double.Parse(str404) > double.Parse(str398))
                        {
                            str404 = str398;
                        }
                        if (double.Parse(str405) > double.Parse(str399))
                        {
                            str405 = str399;
                        }
                        if (double.Parse(str406) > double.Parse(str400))
                        {
                            str406 = str400;
                        }
                    }
                    else
                    {
                        str507 = this.cz_users_model.get_kc_kind().ToLower();
                        if (str507 != null)
                        {
                            if (!(str507 == "a"))
                            {
                                if (str507 == "b")
                                {
                                    goto Label_D762;
                                }
                                if (str507 == "c")
                                {
                                    goto Label_D780;
                                }
                            }
                            else if (double.Parse(str404) > double.Parse(str398))
                            {
                                str404 = str398;
                            }
                        }
                    }
                    goto Label_D79C;
                Label_D762:
                    if (double.Parse(str405) > double.Parse(str399))
                    {
                        str405 = str399;
                    }
                    goto Label_D79C;
                Label_D780:
                    if (double.Parse(str406) > double.Parse(str400))
                    {
                        str406 = str400;
                    }
                Label_D79C:
                    if (double.Parse(str407) > double.Parse(str401))
                    {
                        str407 = str401;
                    }
                    if (double.Parse(str408) > double.Parse(str402))
                    {
                        str408 = str402;
                    }
                    if (double.Parse(str409) < double.Parse(str403))
                    {
                        str409 = str403;
                    }
                    cz_drawback_vrcar _vrcar = new cz_drawback_vrcar();
                    _vrcar.set_play_id(new int?(Convert.ToInt32(str397)));
                    if (!string.IsNullOrEmpty(str404))
                    {
                        _vrcar.set_a_drawback(new decimal?(Convert.ToDecimal(str404)));
                    }
                    if (!string.IsNullOrEmpty(str405))
                    {
                        _vrcar.set_b_drawback(new decimal?(Convert.ToDecimal(str405)));
                    }
                    if (!string.IsNullOrEmpty(str406))
                    {
                        _vrcar.set_c_drawback(new decimal?(Convert.ToDecimal(str406)));
                    }
                    _vrcar.set_single_max_amount(new decimal?(Convert.ToDecimal(str407)));
                    _vrcar.set_single_phase_amount(new decimal?(Convert.ToDecimal(str408)));
                    _vrcar.set_single_min_amount(new decimal?(Convert.ToDecimal(str409)));
                    _vrcar.set_u_name(this.cz_users_model.get_u_name());
                    string str416 = "0";
                    if (string.IsNullOrEmpty(this.cz_users_model.get_kc_kind()) || this.cz_users_model.get_kc_kind().Equals("0"))
                    {
                        str416 = "0";
                    }
                    else
                    {
                        str416 = this.cz_users_model.get_kc_kind();
                    }
                    string str417 = "";
                    str507 = str397;
                    if (str507 == null)
                    {
                        goto Label_D99D;
                    }
                    if (!(str507 == "1"))
                    {
                        if (str507 == "2")
                        {
                            goto Label_D97C;
                        }
                        if (str507 == "3")
                        {
                            goto Label_D987;
                        }
                        if (str507 == "4")
                        {
                            goto Label_D992;
                        }
                        goto Label_D99D;
                    }
                    str417 = "1,5,9,13,17,21,24,27,30,33";
                    goto Label_D9A5;
                Label_D97C:
                    str417 = "2,6,10,14,18,22,25,28,31,34";
                    goto Label_D9A5;
                Label_D987:
                    str417 = "3,7,11,15,19,23,26,29,32,35";
                    goto Label_D9A5;
                Label_D992:
                    str417 = "4,8,12,16,20";
                    goto Label_D9A5;
                Label_D99D:
                    str417 = str397;
                Label_D9A5:
                    dictionary19.Add(str417, _vrcar);
                    if (!this.cz_users_model.get_u_type().Equals("hy"))
                    {
                        drawback_downuser _downuser163 = new drawback_downuser();
                        _downuser163.set_updateUserName(this.cz_users_model.get_u_name());
                        _downuser163.set_updateUserType(this.cz_users_model.get_u_type());
                        _downuser163.set_play_id(str417);
                        _downuser163.set_opt("dq");
                        _downuser163.set_douvalue(double.Parse(_vrcar.get_single_phase_amount().ToString()));
                        list37.Add(_downuser163);
                        drawback_downuser _downuser164 = new drawback_downuser();
                        _downuser164.set_updateUserName(this.cz_users_model.get_u_name());
                        _downuser164.set_updateUserType(this.cz_users_model.get_u_type());
                        _downuser164.set_play_id(str417);
                        _downuser164.set_opt("dz");
                        _downuser164.set_douvalue(double.Parse(_vrcar.get_single_max_amount().ToString()));
                        list37.Add(_downuser164);
                        if (str416.ToLower().Equals("a"))
                        {
                            drawback_downuser _downuser165 = new drawback_downuser();
                            _downuser165.set_updateUserName(this.cz_users_model.get_u_name());
                            _downuser165.set_updateUserType(this.cz_users_model.get_u_type());
                            _downuser165.set_play_id(str417);
                            _downuser165.set_opt("a");
                            _downuser165.set_douvalue(double.Parse(_vrcar.get_a_drawback().ToString()));
                            list37.Add(_downuser165);
                        }
                        else if (str416.ToLower().Equals("b"))
                        {
                            drawback_downuser _downuser166 = new drawback_downuser();
                            _downuser166.set_updateUserName(this.cz_users_model.get_u_name());
                            _downuser166.set_updateUserType(this.cz_users_model.get_u_type());
                            _downuser166.set_play_id(str417);
                            _downuser166.set_opt("b");
                            _downuser166.set_douvalue(double.Parse(_vrcar.get_b_drawback().ToString()));
                            list37.Add(_downuser166);
                        }
                        else if (str416.ToLower().Equals("c"))
                        {
                            drawback_downuser _downuser167 = new drawback_downuser();
                            _downuser167.set_updateUserName(this.cz_users_model.get_u_name());
                            _downuser167.set_updateUserType(this.cz_users_model.get_u_type());
                            _downuser167.set_play_id(str417);
                            _downuser167.set_opt("c");
                            _downuser167.set_douvalue(double.Parse(_vrcar.get_c_drawback().ToString()));
                            list37.Add(_downuser167);
                        }
                        else if (str416.Equals("0"))
                        {
                            drawback_downuser _downuser168 = new drawback_downuser();
                            _downuser168.set_updateUserName(this.cz_users_model.get_u_name());
                            _downuser168.set_updateUserType(this.cz_users_model.get_u_type());
                            _downuser168.set_play_id(str417);
                            _downuser168.set_opt("a");
                            _downuser168.set_douvalue(double.Parse(_vrcar.get_a_drawback().ToString()));
                            list37.Add(_downuser168);
                            drawback_downuser _downuser169 = new drawback_downuser();
                            _downuser169.set_updateUserName(this.cz_users_model.get_u_name());
                            _downuser169.set_updateUserType(this.cz_users_model.get_u_type());
                            _downuser169.set_play_id(str417);
                            _downuser169.set_opt("b");
                            _downuser169.set_douvalue(double.Parse(_vrcar.get_b_drawback().ToString()));
                            list37.Add(_downuser169);
                            drawback_downuser _downuser170 = new drawback_downuser();
                            _downuser170.set_updateUserName(this.cz_users_model.get_u_name());
                            _downuser170.set_updateUserType(this.cz_users_model.get_u_type());
                            _downuser170.set_play_id(str417);
                            _downuser170.set_opt("c");
                            _downuser170.set_douvalue(double.Parse(_vrcar.get_c_drawback().ToString()));
                            list37.Add(_downuser170);
                        }
                    }
                    drawback_downuser _downuser171 = new drawback_downuser();
                    _downuser171.set_updateUserName(this.cz_users_model.get_u_name());
                    _downuser171.set_updateUserType(this.cz_users_model.get_u_type());
                    _downuser171.set_play_id(str417);
                    _downuser171.set_opt("mindz");
                    _downuser171.set_douvalue(double.Parse(_vrcar.get_single_min_amount().ToString()));
                    list38.Add(_downuser171);
                }
                string str418 = "0";
                if (string.IsNullOrEmpty(this.cz_users_model.get_kc_kind()) || this.cz_users_model.get_kc_kind().Equals("0"))
                {
                    str418 = "0";
                }
                else
                {
                    str418 = this.cz_users_model.get_kc_kind();
                }
                if (!CallBLL.cz_drawback_vrcar_bll.UpdateDataList(dictionary19, str418, flag2, list37, list38))
                {
                    mess = string.Format("快彩：修改【{0}】退水出現異常，請重試!", LSKeys.get_VRCAR_Name());
                    isSuccess = false;
                    return;
                }
                base.user_drawback_kc_log(table38, CallBLL.cz_drawback_vrcar_bll.GetDrawbackList(this.cz_users_model.get_u_name(), this.vrcar_modify_playid).Tables[0], this.cz_users_model.get_u_name(), 0x12);
            }
            if ((this.table_vrssc != null) && !string.IsNullOrEmpty(this.vrssc_modify_playid))
            {
                DataTable table39 = CallBLL.cz_drawback_vrssc_bll.GetDrawbackLists(this.cz_users_model.get_sup_name(), this.vrssc_modify_playid).Tables[0];
                DataTable table40 = CallBLL.cz_drawback_vrssc_bll.GetDrawbackLists(this.cz_users_model.get_u_name(), this.vrssc_modify_playid).Tables[0];
                Dictionary<string, cz_drawback_vrssc> dictionary20 = new Dictionary<string, cz_drawback_vrssc>();
                List<drawback_downuser> list39 = new List<drawback_downuser>();
                List<drawback_downuser> list40 = new List<drawback_downuser>();
                foreach (DataRow row20 in table39.Rows)
                {
                    string str419 = row20["play_id"].ToString();
                    string str420 = row20["a_drawback"].ToString();
                    string str421 = row20["b_drawback"].ToString();
                    string str422 = row20["c_drawback"].ToString();
                    string str423 = row20["single_max_amount"].ToString();
                    string str424 = row20["single_phase_amount"].ToString();
                    string str425 = row20["single_min_amount"].ToString();
                    string str426 = LSRequest.qq(string.Format("vrssc_a_{0}", str419));
                    string str427 = LSRequest.qq(string.Format("vrssc_b_{0}", str419));
                    string str428 = LSRequest.qq(string.Format("vrssc_c_{0}", str419));
                    string str429 = LSRequest.qq(string.Format("vrssc_max_amount_{0}", str419));
                    string str430 = LSRequest.qq(string.Format("vrssc_phase_amount_{0}", str419));
                    string str431 = LSRequest.qq(string.Format("vrssc_single_min_amount_{0}", str419.ToLower()));
                    DataRow[] rowArray20 = table40.Select(string.Format(" play_id={0} ", str419));
                    string str432 = rowArray20[0]["a_drawback"].ToString();
                    string str433 = rowArray20[0]["b_drawback"].ToString();
                    string str434 = rowArray20[0]["c_drawback"].ToString();
                    string str435 = rowArray20[0]["single_max_amount"].ToString();
                    string str436 = rowArray20[0]["single_phase_amount"].ToString();
                    string str437 = rowArray20[0]["single_min_amount"].ToString();
                    if ((((double.Parse(str432) == double.Parse(str426)) && (double.Parse(str433) == double.Parse(str427))) && ((double.Parse(str434) == double.Parse(str428)) && (double.Parse(str435) == double.Parse(str429)))) && ((double.Parse(str436) == double.Parse(str430)) && (double.Parse(str437) == double.Parse(str431))))
                    {
                        continue;
                    }
                    if (string.IsNullOrEmpty(this.cz_users_model.get_kc_kind()) || this.cz_users_model.get_kc_kind().Equals("0"))
                    {
                        if (double.Parse(str426) > double.Parse(str420))
                        {
                            str426 = str420;
                        }
                        if (double.Parse(str427) > double.Parse(str421))
                        {
                            str427 = str421;
                        }
                        if (double.Parse(str428) > double.Parse(str422))
                        {
                            str428 = str422;
                        }
                    }
                    else
                    {
                        str507 = this.cz_users_model.get_kc_kind().ToLower();
                        if (str507 != null)
                        {
                            if (!(str507 == "a"))
                            {
                                if (str507 == "b")
                                {
                                    goto Label_E3CB;
                                }
                                if (str507 == "c")
                                {
                                    goto Label_E3E9;
                                }
                            }
                            else if (double.Parse(str426) > double.Parse(str420))
                            {
                                str426 = str420;
                            }
                        }
                    }
                    goto Label_E405;
                Label_E3CB:
                    if (double.Parse(str427) > double.Parse(str421))
                    {
                        str427 = str421;
                    }
                    goto Label_E405;
                Label_E3E9:
                    if (double.Parse(str428) > double.Parse(str422))
                    {
                        str428 = str422;
                    }
                Label_E405:
                    if (double.Parse(str429) > double.Parse(str423))
                    {
                        str429 = str423;
                    }
                    if (double.Parse(str430) > double.Parse(str424))
                    {
                        str430 = str424;
                    }
                    if (double.Parse(str431) < double.Parse(str425))
                    {
                        str431 = str425;
                    }
                    cz_drawback_vrssc _vrssc = new cz_drawback_vrssc();
                    _vrssc.set_play_id(new int?(Convert.ToInt32(str419)));
                    if (!string.IsNullOrEmpty(str426))
                    {
                        _vrssc.set_a_drawback(new decimal?(Convert.ToDecimal(str426)));
                    }
                    if (!string.IsNullOrEmpty(str427))
                    {
                        _vrssc.set_b_drawback(new decimal?(Convert.ToDecimal(str427)));
                    }
                    if (!string.IsNullOrEmpty(str428))
                    {
                        _vrssc.set_c_drawback(new decimal?(Convert.ToDecimal(str428)));
                    }
                    _vrssc.set_single_max_amount(new decimal?(Convert.ToDecimal(str429)));
                    _vrssc.set_single_phase_amount(new decimal?(Convert.ToDecimal(str430)));
                    _vrssc.set_single_min_amount(new decimal?(Convert.ToDecimal(str431)));
                    _vrssc.set_u_name(this.cz_users_model.get_u_name());
                    string str438 = "0";
                    if (string.IsNullOrEmpty(this.cz_users_model.get_kc_kind()) || this.cz_users_model.get_kc_kind().Equals("0"))
                    {
                        str438 = "0";
                    }
                    else
                    {
                        str438 = this.cz_users_model.get_kc_kind();
                    }
                    string str439 = "";
                    str507 = str419;
                    if (str507 == null)
                    {
                        goto Label_E5EB;
                    }
                    if (!(str507 == "1"))
                    {
                        if (str507 == "2")
                        {
                            goto Label_E5D5;
                        }
                        if (str507 == "3")
                        {
                            goto Label_E5E0;
                        }
                        goto Label_E5EB;
                    }
                    str439 = "1,4,7,10,13";
                    goto Label_E5F3;
                Label_E5D5:
                    str439 = "2,5,8,11,14";
                    goto Label_E5F3;
                Label_E5E0:
                    str439 = "3,6,9,12,15";
                    goto Label_E5F3;
                Label_E5EB:
                    str439 = str419;
                Label_E5F3:
                    dictionary20.Add(str439, _vrssc);
                    if (!this.cz_users_model.get_u_type().Equals("hy"))
                    {
                        drawback_downuser _downuser172 = new drawback_downuser();
                        _downuser172.set_updateUserName(this.cz_users_model.get_u_name());
                        _downuser172.set_updateUserType(this.cz_users_model.get_u_type());
                        _downuser172.set_play_id(str439);
                        _downuser172.set_opt("dq");
                        _downuser172.set_douvalue(double.Parse(_vrssc.get_single_phase_amount().ToString()));
                        list39.Add(_downuser172);
                        drawback_downuser _downuser173 = new drawback_downuser();
                        _downuser173.set_updateUserName(this.cz_users_model.get_u_name());
                        _downuser173.set_updateUserType(this.cz_users_model.get_u_type());
                        _downuser173.set_play_id(str439);
                        _downuser173.set_opt("dz");
                        _downuser173.set_douvalue(double.Parse(_vrssc.get_single_max_amount().ToString()));
                        list39.Add(_downuser173);
                        if (str438.ToLower().Equals("a"))
                        {
                            drawback_downuser _downuser174 = new drawback_downuser();
                            _downuser174.set_updateUserName(this.cz_users_model.get_u_name());
                            _downuser174.set_updateUserType(this.cz_users_model.get_u_type());
                            _downuser174.set_play_id(str439);
                            _downuser174.set_opt("a");
                            _downuser174.set_douvalue(double.Parse(_vrssc.get_a_drawback().ToString()));
                            list39.Add(_downuser174);
                        }
                        else if (str438.ToLower().Equals("b"))
                        {
                            drawback_downuser _downuser175 = new drawback_downuser();
                            _downuser175.set_updateUserName(this.cz_users_model.get_u_name());
                            _downuser175.set_updateUserType(this.cz_users_model.get_u_type());
                            _downuser175.set_play_id(str439);
                            _downuser175.set_opt("b");
                            _downuser175.set_douvalue(double.Parse(_vrssc.get_b_drawback().ToString()));
                            list39.Add(_downuser175);
                        }
                        else if (str438.ToLower().Equals("c"))
                        {
                            drawback_downuser _downuser176 = new drawback_downuser();
                            _downuser176.set_updateUserName(this.cz_users_model.get_u_name());
                            _downuser176.set_updateUserType(this.cz_users_model.get_u_type());
                            _downuser176.set_play_id(str439);
                            _downuser176.set_opt("c");
                            _downuser176.set_douvalue(double.Parse(_vrssc.get_c_drawback().ToString()));
                            list39.Add(_downuser176);
                        }
                        else if (str438.Equals("0"))
                        {
                            drawback_downuser _downuser177 = new drawback_downuser();
                            _downuser177.set_updateUserName(this.cz_users_model.get_u_name());
                            _downuser177.set_updateUserType(this.cz_users_model.get_u_type());
                            _downuser177.set_play_id(str439);
                            _downuser177.set_opt("a");
                            _downuser177.set_douvalue(double.Parse(_vrssc.get_a_drawback().ToString()));
                            list39.Add(_downuser177);
                            drawback_downuser _downuser178 = new drawback_downuser();
                            _downuser178.set_updateUserName(this.cz_users_model.get_u_name());
                            _downuser178.set_updateUserType(this.cz_users_model.get_u_type());
                            _downuser178.set_play_id(str439);
                            _downuser178.set_opt("b");
                            _downuser178.set_douvalue(double.Parse(_vrssc.get_b_drawback().ToString()));
                            list39.Add(_downuser178);
                            drawback_downuser _downuser179 = new drawback_downuser();
                            _downuser179.set_updateUserName(this.cz_users_model.get_u_name());
                            _downuser179.set_updateUserType(this.cz_users_model.get_u_type());
                            _downuser179.set_play_id(str439);
                            _downuser179.set_opt("c");
                            _downuser179.set_douvalue(double.Parse(_vrssc.get_c_drawback().ToString()));
                            list39.Add(_downuser179);
                        }
                    }
                    drawback_downuser _downuser180 = new drawback_downuser();
                    _downuser180.set_updateUserName(this.cz_users_model.get_u_name());
                    _downuser180.set_updateUserType(this.cz_users_model.get_u_type());
                    _downuser180.set_play_id(str439);
                    _downuser180.set_opt("mindz");
                    _downuser180.set_douvalue(double.Parse(_vrssc.get_single_min_amount().ToString()));
                    list40.Add(_downuser180);
                }
                string str440 = "0";
                if (string.IsNullOrEmpty(this.cz_users_model.get_kc_kind()) || this.cz_users_model.get_kc_kind().Equals("0"))
                {
                    str440 = "0";
                }
                else
                {
                    str440 = this.cz_users_model.get_kc_kind();
                }
                if (!CallBLL.cz_drawback_vrssc_bll.UpdateDataList(dictionary20, str440, flag2, list39, list40))
                {
                    mess = string.Format("快彩：修改【{0}】退水出現異常，請重試!", LSKeys.get_VRSSC_Name());
                    isSuccess = false;
                    return;
                }
                base.user_drawback_kc_log(table40, CallBLL.cz_drawback_vrssc_bll.GetDrawbackLists(this.cz_users_model.get_u_name(), this.vrssc_modify_playid).Tables[0], this.cz_users_model.get_u_name(), 0x13);
            }
            if ((this.table_xyftoa != null) && !string.IsNullOrEmpty(this.xyftoa_modify_playid))
            {
                DataTable table41 = CallBLL.cz_drawback_xyftoa_bll.GetDrawbackList(this.cz_users_model.get_sup_name(), this.xyftoa_modify_playid).Tables[0];
                DataTable table42 = CallBLL.cz_drawback_xyftoa_bll.GetDrawbackList(this.cz_users_model.get_u_name(), this.xyftoa_modify_playid).Tables[0];
                Dictionary<string, cz_drawback_xyftoa> dictionary21 = new Dictionary<string, cz_drawback_xyftoa>();
                List<drawback_downuser> list41 = new List<drawback_downuser>();
                List<drawback_downuser> list42 = new List<drawback_downuser>();
                foreach (DataRow row21 in table41.Rows)
                {
                    string str441 = row21["play_id"].ToString();
                    string str442 = row21["a_drawback"].ToString();
                    string str443 = row21["b_drawback"].ToString();
                    string str444 = row21["c_drawback"].ToString();
                    string str445 = row21["single_max_amount"].ToString();
                    string str446 = row21["single_phase_amount"].ToString();
                    string str447 = row21["single_min_amount"].ToString();
                    string str448 = LSRequest.qq(string.Format("xyftoa_a_{0}", str441));
                    string str449 = LSRequest.qq(string.Format("xyftoa_b_{0}", str441));
                    string str450 = LSRequest.qq(string.Format("xyftoa_c_{0}", str441));
                    string str451 = LSRequest.qq(string.Format("xyftoa_max_amount_{0}", str441));
                    string str452 = LSRequest.qq(string.Format("xyftoa_phase_amount_{0}", str441));
                    string str453 = LSRequest.qq(string.Format("xyftoa_single_min_amount_{0}", str441.ToLower()));
                    DataRow[] rowArray21 = table42.Select(string.Format(" play_id={0} ", str441));
                    string str454 = rowArray21[0]["a_drawback"].ToString();
                    string str455 = rowArray21[0]["b_drawback"].ToString();
                    string str456 = rowArray21[0]["c_drawback"].ToString();
                    string str457 = rowArray21[0]["single_max_amount"].ToString();
                    string str458 = rowArray21[0]["single_phase_amount"].ToString();
                    string str459 = rowArray21[0]["single_min_amount"].ToString();
                    if ((((double.Parse(str454) == double.Parse(str448)) && (double.Parse(str455) == double.Parse(str449))) && ((double.Parse(str456) == double.Parse(str450)) && (double.Parse(str457) == double.Parse(str451)))) && ((double.Parse(str458) == double.Parse(str452)) && (double.Parse(str459) == double.Parse(str453))))
                    {
                        continue;
                    }
                    if (string.IsNullOrEmpty(this.cz_users_model.get_kc_kind()) || this.cz_users_model.get_kc_kind().Equals("0"))
                    {
                        if (double.Parse(str448) > double.Parse(str442))
                        {
                            str448 = str442;
                        }
                        if (double.Parse(str449) > double.Parse(str443))
                        {
                            str449 = str443;
                        }
                        if (double.Parse(str450) > double.Parse(str444))
                        {
                            str450 = str444;
                        }
                    }
                    else
                    {
                        str507 = this.cz_users_model.get_kc_kind().ToLower();
                        if (str507 != null)
                        {
                            if (!(str507 == "a"))
                            {
                                if (str507 == "b")
                                {
                                    goto Label_F019;
                                }
                                if (str507 == "c")
                                {
                                    goto Label_F037;
                                }
                            }
                            else if (double.Parse(str448) > double.Parse(str442))
                            {
                                str448 = str442;
                            }
                        }
                    }
                    goto Label_F053;
                Label_F019:
                    if (double.Parse(str449) > double.Parse(str443))
                    {
                        str449 = str443;
                    }
                    goto Label_F053;
                Label_F037:
                    if (double.Parse(str450) > double.Parse(str444))
                    {
                        str450 = str444;
                    }
                Label_F053:
                    if (double.Parse(str451) > double.Parse(str445))
                    {
                        str451 = str445;
                    }
                    if (double.Parse(str452) > double.Parse(str446))
                    {
                        str452 = str446;
                    }
                    if (double.Parse(str453) < double.Parse(str447))
                    {
                        str453 = str447;
                    }
                    cz_drawback_xyftoa _xyftoa = new cz_drawback_xyftoa();
                    _xyftoa.set_play_id(new int?(Convert.ToInt32(str441)));
                    if (!string.IsNullOrEmpty(str448))
                    {
                        _xyftoa.set_a_drawback(new decimal?(Convert.ToDecimal(str448)));
                    }
                    if (!string.IsNullOrEmpty(str449))
                    {
                        _xyftoa.set_b_drawback(new decimal?(Convert.ToDecimal(str449)));
                    }
                    if (!string.IsNullOrEmpty(str450))
                    {
                        _xyftoa.set_c_drawback(new decimal?(Convert.ToDecimal(str450)));
                    }
                    _xyftoa.set_single_max_amount(new decimal?(Convert.ToDecimal(str451)));
                    _xyftoa.set_single_phase_amount(new decimal?(Convert.ToDecimal(str452)));
                    _xyftoa.set_single_min_amount(new decimal?(Convert.ToDecimal(str453)));
                    _xyftoa.set_u_name(this.cz_users_model.get_u_name());
                    string str460 = "0";
                    if (string.IsNullOrEmpty(this.cz_users_model.get_kc_kind()) || this.cz_users_model.get_kc_kind().Equals("0"))
                    {
                        str460 = "0";
                    }
                    else
                    {
                        str460 = this.cz_users_model.get_kc_kind();
                    }
                    string str461 = "";
                    str507 = str441;
                    if (str507 == null)
                    {
                        goto Label_F254;
                    }
                    if (!(str507 == "1"))
                    {
                        if (str507 == "2")
                        {
                            goto Label_F233;
                        }
                        if (str507 == "3")
                        {
                            goto Label_F23E;
                        }
                        if (str507 == "4")
                        {
                            goto Label_F249;
                        }
                        goto Label_F254;
                    }
                    str461 = "1,5,9,13,17,21,24,27,30,33";
                    goto Label_F25C;
                Label_F233:
                    str461 = "2,6,10,14,18,22,25,28,31,34";
                    goto Label_F25C;
                Label_F23E:
                    str461 = "3,7,11,15,19,23,26,29,32,35";
                    goto Label_F25C;
                Label_F249:
                    str461 = "4,8,12,16,20";
                    goto Label_F25C;
                Label_F254:
                    str461 = str441;
                Label_F25C:
                    dictionary21.Add(str461, _xyftoa);
                    if (!this.cz_users_model.get_u_type().Equals("hy"))
                    {
                        drawback_downuser _downuser181 = new drawback_downuser();
                        _downuser181.set_updateUserName(this.cz_users_model.get_u_name());
                        _downuser181.set_updateUserType(this.cz_users_model.get_u_type());
                        _downuser181.set_play_id(str461);
                        _downuser181.set_opt("dq");
                        _downuser181.set_douvalue(double.Parse(_xyftoa.get_single_phase_amount().ToString()));
                        list41.Add(_downuser181);
                        drawback_downuser _downuser182 = new drawback_downuser();
                        _downuser182.set_updateUserName(this.cz_users_model.get_u_name());
                        _downuser182.set_updateUserType(this.cz_users_model.get_u_type());
                        _downuser182.set_play_id(str461);
                        _downuser182.set_opt("dz");
                        _downuser182.set_douvalue(double.Parse(_xyftoa.get_single_max_amount().ToString()));
                        list41.Add(_downuser182);
                        if (str460.ToLower().Equals("a"))
                        {
                            drawback_downuser _downuser183 = new drawback_downuser();
                            _downuser183.set_updateUserName(this.cz_users_model.get_u_name());
                            _downuser183.set_updateUserType(this.cz_users_model.get_u_type());
                            _downuser183.set_play_id(str461);
                            _downuser183.set_opt("a");
                            _downuser183.set_douvalue(double.Parse(_xyftoa.get_a_drawback().ToString()));
                            list41.Add(_downuser183);
                        }
                        else if (str460.ToLower().Equals("b"))
                        {
                            drawback_downuser _downuser184 = new drawback_downuser();
                            _downuser184.set_updateUserName(this.cz_users_model.get_u_name());
                            _downuser184.set_updateUserType(this.cz_users_model.get_u_type());
                            _downuser184.set_play_id(str461);
                            _downuser184.set_opt("b");
                            _downuser184.set_douvalue(double.Parse(_xyftoa.get_b_drawback().ToString()));
                            list41.Add(_downuser184);
                        }
                        else if (str460.ToLower().Equals("c"))
                        {
                            drawback_downuser _downuser185 = new drawback_downuser();
                            _downuser185.set_updateUserName(this.cz_users_model.get_u_name());
                            _downuser185.set_updateUserType(this.cz_users_model.get_u_type());
                            _downuser185.set_play_id(str461);
                            _downuser185.set_opt("c");
                            _downuser185.set_douvalue(double.Parse(_xyftoa.get_c_drawback().ToString()));
                            list41.Add(_downuser185);
                        }
                        else if (str460.Equals("0"))
                        {
                            drawback_downuser _downuser186 = new drawback_downuser();
                            _downuser186.set_updateUserName(this.cz_users_model.get_u_name());
                            _downuser186.set_updateUserType(this.cz_users_model.get_u_type());
                            _downuser186.set_play_id(str461);
                            _downuser186.set_opt("a");
                            _downuser186.set_douvalue(double.Parse(_xyftoa.get_a_drawback().ToString()));
                            list41.Add(_downuser186);
                            drawback_downuser _downuser187 = new drawback_downuser();
                            _downuser187.set_updateUserName(this.cz_users_model.get_u_name());
                            _downuser187.set_updateUserType(this.cz_users_model.get_u_type());
                            _downuser187.set_play_id(str461);
                            _downuser187.set_opt("b");
                            _downuser187.set_douvalue(double.Parse(_xyftoa.get_b_drawback().ToString()));
                            list41.Add(_downuser187);
                            drawback_downuser _downuser188 = new drawback_downuser();
                            _downuser188.set_updateUserName(this.cz_users_model.get_u_name());
                            _downuser188.set_updateUserType(this.cz_users_model.get_u_type());
                            _downuser188.set_play_id(str461);
                            _downuser188.set_opt("c");
                            _downuser188.set_douvalue(double.Parse(_xyftoa.get_c_drawback().ToString()));
                            list41.Add(_downuser188);
                        }
                    }
                    drawback_downuser _downuser189 = new drawback_downuser();
                    _downuser189.set_updateUserName(this.cz_users_model.get_u_name());
                    _downuser189.set_updateUserType(this.cz_users_model.get_u_type());
                    _downuser189.set_play_id(str461);
                    _downuser189.set_opt("mindz");
                    _downuser189.set_douvalue(double.Parse(_xyftoa.get_single_min_amount().ToString()));
                    list42.Add(_downuser189);
                }
                string str462 = "0";
                if (string.IsNullOrEmpty(this.cz_users_model.get_kc_kind()) || this.cz_users_model.get_kc_kind().Equals("0"))
                {
                    str462 = "0";
                }
                else
                {
                    str462 = this.cz_users_model.get_kc_kind();
                }
                if (!CallBLL.cz_drawback_xyftoa_bll.UpdateDataList(dictionary21, str462, flag2, list41, list42))
                {
                    mess = string.Format("快彩：修改【{0}】退水出現異常，請重試!", LSKeys.get_XYFTOA_Name());
                    isSuccess = false;
                    return;
                }
                base.user_drawback_kc_log(table42, CallBLL.cz_drawback_xyftoa_bll.GetDrawbackList(this.cz_users_model.get_u_name(), this.xyftoa_modify_playid).Tables[0], this.cz_users_model.get_u_name(), 20);
            }
            if ((this.table_xyftsg != null) && !string.IsNullOrEmpty(this.xyftsg_modify_playid))
            {
                DataTable table43 = CallBLL.cz_drawback_xyftsg_bll.GetDrawbackList(this.cz_users_model.get_sup_name(), this.xyftsg_modify_playid).Tables[0];
                DataTable table44 = CallBLL.cz_drawback_xyftsg_bll.GetDrawbackList(this.cz_users_model.get_u_name(), this.xyftsg_modify_playid).Tables[0];
                Dictionary<string, cz_drawback_xyftsg> dictionary22 = new Dictionary<string, cz_drawback_xyftsg>();
                List<drawback_downuser> list43 = new List<drawback_downuser>();
                List<drawback_downuser> list44 = new List<drawback_downuser>();
                foreach (DataRow row22 in table43.Rows)
                {
                    string str463 = row22["play_id"].ToString();
                    string str464 = row22["a_drawback"].ToString();
                    string str465 = row22["b_drawback"].ToString();
                    string str466 = row22["c_drawback"].ToString();
                    string str467 = row22["single_max_amount"].ToString();
                    string str468 = row22["single_phase_amount"].ToString();
                    string str469 = row22["single_min_amount"].ToString();
                    string str470 = LSRequest.qq(string.Format("xyftsg_a_{0}", str463));
                    string str471 = LSRequest.qq(string.Format("xyftsg_b_{0}", str463));
                    string str472 = LSRequest.qq(string.Format("xyftsg_c_{0}", str463));
                    string str473 = LSRequest.qq(string.Format("xyftsg_max_amount_{0}", str463));
                    string str474 = LSRequest.qq(string.Format("xyftsg_phase_amount_{0}", str463));
                    string str475 = LSRequest.qq(string.Format("xyftsg_single_min_amount_{0}", str463.ToLower()));
                    DataRow[] rowArray22 = table44.Select(string.Format(" play_id={0} ", str463));
                    string str476 = rowArray22[0]["a_drawback"].ToString();
                    string str477 = rowArray22[0]["b_drawback"].ToString();
                    string str478 = rowArray22[0]["c_drawback"].ToString();
                    string str479 = rowArray22[0]["single_max_amount"].ToString();
                    string str480 = rowArray22[0]["single_phase_amount"].ToString();
                    string str481 = rowArray22[0]["single_min_amount"].ToString();
                    if ((((double.Parse(str476) == double.Parse(str470)) && (double.Parse(str477) == double.Parse(str471))) && ((double.Parse(str478) == double.Parse(str472)) && (double.Parse(str479) == double.Parse(str473)))) && ((double.Parse(str480) == double.Parse(str474)) && (double.Parse(str481) == double.Parse(str475))))
                    {
                        continue;
                    }
                    if (string.IsNullOrEmpty(this.cz_users_model.get_kc_kind()) || this.cz_users_model.get_kc_kind().Equals("0"))
                    {
                        if (double.Parse(str470) > double.Parse(str464))
                        {
                            str470 = str464;
                        }
                        if (double.Parse(str471) > double.Parse(str465))
                        {
                            str471 = str465;
                        }
                        if (double.Parse(str472) > double.Parse(str466))
                        {
                            str472 = str466;
                        }
                    }
                    else
                    {
                        str507 = this.cz_users_model.get_kc_kind().ToLower();
                        if (str507 != null)
                        {
                            if (!(str507 == "a"))
                            {
                                if (str507 == "b")
                                {
                                    goto Label_FC82;
                                }
                                if (str507 == "c")
                                {
                                    goto Label_FCA0;
                                }
                            }
                            else if (double.Parse(str470) > double.Parse(str464))
                            {
                                str470 = str464;
                            }
                        }
                    }
                    goto Label_FCBC;
                Label_FC82:
                    if (double.Parse(str471) > double.Parse(str465))
                    {
                        str471 = str465;
                    }
                    goto Label_FCBC;
                Label_FCA0:
                    if (double.Parse(str472) > double.Parse(str466))
                    {
                        str472 = str466;
                    }
                Label_FCBC:
                    if (double.Parse(str473) > double.Parse(str467))
                    {
                        str473 = str467;
                    }
                    if (double.Parse(str474) > double.Parse(str468))
                    {
                        str474 = str468;
                    }
                    if (double.Parse(str475) < double.Parse(str469))
                    {
                        str475 = str469;
                    }
                    cz_drawback_xyftsg _xyftsg = new cz_drawback_xyftsg();
                    _xyftsg.set_play_id(new int?(Convert.ToInt32(str463)));
                    if (!string.IsNullOrEmpty(str470))
                    {
                        _xyftsg.set_a_drawback(new decimal?(Convert.ToDecimal(str470)));
                    }
                    if (!string.IsNullOrEmpty(str471))
                    {
                        _xyftsg.set_b_drawback(new decimal?(Convert.ToDecimal(str471)));
                    }
                    if (!string.IsNullOrEmpty(str472))
                    {
                        _xyftsg.set_c_drawback(new decimal?(Convert.ToDecimal(str472)));
                    }
                    _xyftsg.set_single_max_amount(new decimal?(Convert.ToDecimal(str473)));
                    _xyftsg.set_single_phase_amount(new decimal?(Convert.ToDecimal(str474)));
                    _xyftsg.set_single_min_amount(new decimal?(Convert.ToDecimal(str475)));
                    _xyftsg.set_u_name(this.cz_users_model.get_u_name());
                    string str482 = "0";
                    if (string.IsNullOrEmpty(this.cz_users_model.get_kc_kind()) || this.cz_users_model.get_kc_kind().Equals("0"))
                    {
                        str482 = "0";
                    }
                    else
                    {
                        str482 = this.cz_users_model.get_kc_kind();
                    }
                    string str483 = "";
                    str507 = str463;
                    if (str507 == null)
                    {
                        goto Label_FEBD;
                    }
                    if (!(str507 == "1"))
                    {
                        if (str507 == "2")
                        {
                            goto Label_FE9C;
                        }
                        if (str507 == "3")
                        {
                            goto Label_FEA7;
                        }
                        if (str507 == "4")
                        {
                            goto Label_FEB2;
                        }
                        goto Label_FEBD;
                    }
                    str483 = "1,5,9,13,17,21,24,27,30,33";
                    goto Label_FEC5;
                Label_FE9C:
                    str483 = "2,6,10,14,18,22,25,28,31,34";
                    goto Label_FEC5;
                Label_FEA7:
                    str483 = "3,7,11,15,19,23,26,29,32,35";
                    goto Label_FEC5;
                Label_FEB2:
                    str483 = "4,8,12,16,20";
                    goto Label_FEC5;
                Label_FEBD:
                    str483 = str463;
                Label_FEC5:
                    dictionary22.Add(str483, _xyftsg);
                    if (!this.cz_users_model.get_u_type().Equals("hy"))
                    {
                        drawback_downuser _downuser190 = new drawback_downuser();
                        _downuser190.set_updateUserName(this.cz_users_model.get_u_name());
                        _downuser190.set_updateUserType(this.cz_users_model.get_u_type());
                        _downuser190.set_play_id(str483);
                        _downuser190.set_opt("dq");
                        _downuser190.set_douvalue(double.Parse(_xyftsg.get_single_phase_amount().ToString()));
                        list43.Add(_downuser190);
                        drawback_downuser _downuser191 = new drawback_downuser();
                        _downuser191.set_updateUserName(this.cz_users_model.get_u_name());
                        _downuser191.set_updateUserType(this.cz_users_model.get_u_type());
                        _downuser191.set_play_id(str483);
                        _downuser191.set_opt("dz");
                        _downuser191.set_douvalue(double.Parse(_xyftsg.get_single_max_amount().ToString()));
                        list43.Add(_downuser191);
                        if (str482.ToLower().Equals("a"))
                        {
                            drawback_downuser _downuser192 = new drawback_downuser();
                            _downuser192.set_updateUserName(this.cz_users_model.get_u_name());
                            _downuser192.set_updateUserType(this.cz_users_model.get_u_type());
                            _downuser192.set_play_id(str483);
                            _downuser192.set_opt("a");
                            _downuser192.set_douvalue(double.Parse(_xyftsg.get_a_drawback().ToString()));
                            list43.Add(_downuser192);
                        }
                        else if (str482.ToLower().Equals("b"))
                        {
                            drawback_downuser _downuser193 = new drawback_downuser();
                            _downuser193.set_updateUserName(this.cz_users_model.get_u_name());
                            _downuser193.set_updateUserType(this.cz_users_model.get_u_type());
                            _downuser193.set_play_id(str483);
                            _downuser193.set_opt("b");
                            _downuser193.set_douvalue(double.Parse(_xyftsg.get_b_drawback().ToString()));
                            list43.Add(_downuser193);
                        }
                        else if (str482.ToLower().Equals("c"))
                        {
                            drawback_downuser _downuser194 = new drawback_downuser();
                            _downuser194.set_updateUserName(this.cz_users_model.get_u_name());
                            _downuser194.set_updateUserType(this.cz_users_model.get_u_type());
                            _downuser194.set_play_id(str483);
                            _downuser194.set_opt("c");
                            _downuser194.set_douvalue(double.Parse(_xyftsg.get_c_drawback().ToString()));
                            list43.Add(_downuser194);
                        }
                        else if (str482.Equals("0"))
                        {
                            drawback_downuser _downuser195 = new drawback_downuser();
                            _downuser195.set_updateUserName(this.cz_users_model.get_u_name());
                            _downuser195.set_updateUserType(this.cz_users_model.get_u_type());
                            _downuser195.set_play_id(str483);
                            _downuser195.set_opt("a");
                            _downuser195.set_douvalue(double.Parse(_xyftsg.get_a_drawback().ToString()));
                            list43.Add(_downuser195);
                            drawback_downuser _downuser196 = new drawback_downuser();
                            _downuser196.set_updateUserName(this.cz_users_model.get_u_name());
                            _downuser196.set_updateUserType(this.cz_users_model.get_u_type());
                            _downuser196.set_play_id(str483);
                            _downuser196.set_opt("b");
                            _downuser196.set_douvalue(double.Parse(_xyftsg.get_b_drawback().ToString()));
                            list43.Add(_downuser196);
                            drawback_downuser _downuser197 = new drawback_downuser();
                            _downuser197.set_updateUserName(this.cz_users_model.get_u_name());
                            _downuser197.set_updateUserType(this.cz_users_model.get_u_type());
                            _downuser197.set_play_id(str483);
                            _downuser197.set_opt("c");
                            _downuser197.set_douvalue(double.Parse(_xyftsg.get_c_drawback().ToString()));
                            list43.Add(_downuser197);
                        }
                    }
                    drawback_downuser _downuser198 = new drawback_downuser();
                    _downuser198.set_updateUserName(this.cz_users_model.get_u_name());
                    _downuser198.set_updateUserType(this.cz_users_model.get_u_type());
                    _downuser198.set_play_id(str483);
                    _downuser198.set_opt("mindz");
                    _downuser198.set_douvalue(double.Parse(_xyftsg.get_single_min_amount().ToString()));
                    list44.Add(_downuser198);
                }
                string str484 = "0";
                if (string.IsNullOrEmpty(this.cz_users_model.get_kc_kind()) || this.cz_users_model.get_kc_kind().Equals("0"))
                {
                    str484 = "0";
                }
                else
                {
                    str484 = this.cz_users_model.get_kc_kind();
                }
                if (!CallBLL.cz_drawback_xyftsg_bll.UpdateDataList(dictionary22, str484, flag2, list43, list44))
                {
                    mess = string.Format("快彩：修改【{0}】退水出現異常，請重試!", LSKeys.get_XYFTSG_Name());
                    isSuccess = false;
                    return;
                }
                base.user_drawback_kc_log(table44, CallBLL.cz_drawback_xyftsg_bll.GetDrawbackList(this.cz_users_model.get_u_name(), this.xyftsg_modify_playid).Tables[0], this.cz_users_model.get_u_name(), 0x15);
            }
            if ((this.table_happycar != null) && !string.IsNullOrEmpty(this.happycar_modify_playid))
            {
                DataTable table45 = CallBLL.cz_drawback_happycar_bll.GetDrawbackList(this.cz_users_model.get_sup_name(), this.happycar_modify_playid).Tables[0];
                DataTable table46 = CallBLL.cz_drawback_happycar_bll.GetDrawbackList(this.cz_users_model.get_u_name(), this.happycar_modify_playid).Tables[0];
                Dictionary<string, cz_drawback_happycar> dictionary23 = new Dictionary<string, cz_drawback_happycar>();
                List<drawback_downuser> list45 = new List<drawback_downuser>();
                List<drawback_downuser> list46 = new List<drawback_downuser>();
                foreach (DataRow row23 in table45.Rows)
                {
                    string str485 = row23["play_id"].ToString();
                    string str486 = row23["a_drawback"].ToString();
                    string str487 = row23["b_drawback"].ToString();
                    string str488 = row23["c_drawback"].ToString();
                    string str489 = row23["single_max_amount"].ToString();
                    string str490 = row23["single_phase_amount"].ToString();
                    string str491 = row23["single_min_amount"].ToString();
                    string str492 = LSRequest.qq(string.Format("happycar_a_{0}", str485));
                    string str493 = LSRequest.qq(string.Format("happycar_b_{0}", str485));
                    string str494 = LSRequest.qq(string.Format("happycar_c_{0}", str485));
                    string str495 = LSRequest.qq(string.Format("happycar_max_amount_{0}", str485));
                    string str496 = LSRequest.qq(string.Format("happycar_phase_amount_{0}", str485));
                    string str497 = LSRequest.qq(string.Format("happycar_single_min_amount_{0}", str485.ToLower()));
                    DataRow[] rowArray23 = table46.Select(string.Format(" play_id={0} ", str485));
                    string str498 = rowArray23[0]["a_drawback"].ToString();
                    string str499 = rowArray23[0]["b_drawback"].ToString();
                    string str500 = rowArray23[0]["c_drawback"].ToString();
                    string str501 = rowArray23[0]["single_max_amount"].ToString();
                    string str502 = rowArray23[0]["single_phase_amount"].ToString();
                    string str503 = rowArray23[0]["single_min_amount"].ToString();
                    if ((((double.Parse(str498) == double.Parse(str492)) && (double.Parse(str499) == double.Parse(str493))) && ((double.Parse(str500) == double.Parse(str494)) && (double.Parse(str501) == double.Parse(str495)))) && ((double.Parse(str502) == double.Parse(str496)) && (double.Parse(str503) == double.Parse(str497))))
                    {
                        continue;
                    }
                    if (string.IsNullOrEmpty(this.cz_users_model.get_kc_kind()) || this.cz_users_model.get_kc_kind().Equals("0"))
                    {
                        if (double.Parse(str492) > double.Parse(str486))
                        {
                            str492 = str486;
                        }
                        if (double.Parse(str493) > double.Parse(str487))
                        {
                            str493 = str487;
                        }
                        if (double.Parse(str494) > double.Parse(str488))
                        {
                            str494 = str488;
                        }
                    }
                    else
                    {
                        str507 = this.cz_users_model.get_kc_kind().ToLower();
                        if (str507 != null)
                        {
                            if (!(str507 == "a"))
                            {
                                if (str507 == "b")
                                {
                                    goto Label_108EB;
                                }
                                if (str507 == "c")
                                {
                                    goto Label_10909;
                                }
                            }
                            else if (double.Parse(str492) > double.Parse(str486))
                            {
                                str492 = str486;
                            }
                        }
                    }
                    goto Label_10925;
                Label_108EB:
                    if (double.Parse(str493) > double.Parse(str487))
                    {
                        str493 = str487;
                    }
                    goto Label_10925;
                Label_10909:
                    if (double.Parse(str494) > double.Parse(str488))
                    {
                        str494 = str488;
                    }
                Label_10925:
                    if (double.Parse(str495) > double.Parse(str489))
                    {
                        str495 = str489;
                    }
                    if (double.Parse(str496) > double.Parse(str490))
                    {
                        str496 = str490;
                    }
                    if (double.Parse(str497) < double.Parse(str491))
                    {
                        str497 = str491;
                    }
                    cz_drawback_happycar _happycar = new cz_drawback_happycar();
                    _happycar.set_play_id(new int?(Convert.ToInt32(str485)));
                    if (!string.IsNullOrEmpty(str492))
                    {
                        _happycar.set_a_drawback(new decimal?(Convert.ToDecimal(str492)));
                    }
                    if (!string.IsNullOrEmpty(str493))
                    {
                        _happycar.set_b_drawback(new decimal?(Convert.ToDecimal(str493)));
                    }
                    if (!string.IsNullOrEmpty(str494))
                    {
                        _happycar.set_c_drawback(new decimal?(Convert.ToDecimal(str494)));
                    }
                    _happycar.set_single_max_amount(new decimal?(Convert.ToDecimal(str495)));
                    _happycar.set_single_phase_amount(new decimal?(Convert.ToDecimal(str496)));
                    _happycar.set_single_min_amount(new decimal?(Convert.ToDecimal(str497)));
                    _happycar.set_u_name(this.cz_users_model.get_u_name());
                    string str504 = "0";
                    if (string.IsNullOrEmpty(this.cz_users_model.get_kc_kind()) || this.cz_users_model.get_kc_kind().Equals("0"))
                    {
                        str504 = "0";
                    }
                    else
                    {
                        str504 = this.cz_users_model.get_kc_kind();
                    }
                    string str505 = "";
                    str507 = str485;
                    if (str507 == null)
                    {
                        goto Label_10B26;
                    }
                    if (!(str507 == "1"))
                    {
                        if (str507 == "2")
                        {
                            goto Label_10B05;
                        }
                        if (str507 == "3")
                        {
                            goto Label_10B10;
                        }
                        if (str507 == "4")
                        {
                            goto Label_10B1B;
                        }
                        goto Label_10B26;
                    }
                    str505 = "1,5,9,13,17,21,24,27,30,33";
                    goto Label_10B2E;
                Label_10B05:
                    str505 = "2,6,10,14,18,22,25,28,31,34";
                    goto Label_10B2E;
                Label_10B10:
                    str505 = "3,7,11,15,19,23,26,29,32,35";
                    goto Label_10B2E;
                Label_10B1B:
                    str505 = "4,8,12,16,20";
                    goto Label_10B2E;
                Label_10B26:
                    str505 = str485;
                Label_10B2E:
                    dictionary23.Add(str505, _happycar);
                    if (!this.cz_users_model.get_u_type().Equals("hy"))
                    {
                        drawback_downuser _downuser199 = new drawback_downuser();
                        _downuser199.set_updateUserName(this.cz_users_model.get_u_name());
                        _downuser199.set_updateUserType(this.cz_users_model.get_u_type());
                        _downuser199.set_play_id(str505);
                        _downuser199.set_opt("dq");
                        _downuser199.set_douvalue(double.Parse(_happycar.get_single_phase_amount().ToString()));
                        list45.Add(_downuser199);
                        drawback_downuser _downuser200 = new drawback_downuser();
                        _downuser200.set_updateUserName(this.cz_users_model.get_u_name());
                        _downuser200.set_updateUserType(this.cz_users_model.get_u_type());
                        _downuser200.set_play_id(str505);
                        _downuser200.set_opt("dz");
                        _downuser200.set_douvalue(double.Parse(_happycar.get_single_max_amount().ToString()));
                        list45.Add(_downuser200);
                        if (str504.ToLower().Equals("a"))
                        {
                            drawback_downuser _downuser201 = new drawback_downuser();
                            _downuser201.set_updateUserName(this.cz_users_model.get_u_name());
                            _downuser201.set_updateUserType(this.cz_users_model.get_u_type());
                            _downuser201.set_play_id(str505);
                            _downuser201.set_opt("a");
                            _downuser201.set_douvalue(double.Parse(_happycar.get_a_drawback().ToString()));
                            list45.Add(_downuser201);
                        }
                        else if (str504.ToLower().Equals("b"))
                        {
                            drawback_downuser _downuser202 = new drawback_downuser();
                            _downuser202.set_updateUserName(this.cz_users_model.get_u_name());
                            _downuser202.set_updateUserType(this.cz_users_model.get_u_type());
                            _downuser202.set_play_id(str505);
                            _downuser202.set_opt("b");
                            _downuser202.set_douvalue(double.Parse(_happycar.get_b_drawback().ToString()));
                            list45.Add(_downuser202);
                        }
                        else if (str504.ToLower().Equals("c"))
                        {
                            drawback_downuser _downuser203 = new drawback_downuser();
                            _downuser203.set_updateUserName(this.cz_users_model.get_u_name());
                            _downuser203.set_updateUserType(this.cz_users_model.get_u_type());
                            _downuser203.set_play_id(str505);
                            _downuser203.set_opt("c");
                            _downuser203.set_douvalue(double.Parse(_happycar.get_c_drawback().ToString()));
                            list45.Add(_downuser203);
                        }
                        else if (str504.Equals("0"))
                        {
                            drawback_downuser _downuser204 = new drawback_downuser();
                            _downuser204.set_updateUserName(this.cz_users_model.get_u_name());
                            _downuser204.set_updateUserType(this.cz_users_model.get_u_type());
                            _downuser204.set_play_id(str505);
                            _downuser204.set_opt("a");
                            _downuser204.set_douvalue(double.Parse(_happycar.get_a_drawback().ToString()));
                            list45.Add(_downuser204);
                            drawback_downuser _downuser205 = new drawback_downuser();
                            _downuser205.set_updateUserName(this.cz_users_model.get_u_name());
                            _downuser205.set_updateUserType(this.cz_users_model.get_u_type());
                            _downuser205.set_play_id(str505);
                            _downuser205.set_opt("b");
                            _downuser205.set_douvalue(double.Parse(_happycar.get_b_drawback().ToString()));
                            list45.Add(_downuser205);
                            drawback_downuser _downuser206 = new drawback_downuser();
                            _downuser206.set_updateUserName(this.cz_users_model.get_u_name());
                            _downuser206.set_updateUserType(this.cz_users_model.get_u_type());
                            _downuser206.set_play_id(str505);
                            _downuser206.set_opt("c");
                            _downuser206.set_douvalue(double.Parse(_happycar.get_c_drawback().ToString()));
                            list45.Add(_downuser206);
                        }
                    }
                    drawback_downuser _downuser207 = new drawback_downuser();
                    _downuser207.set_updateUserName(this.cz_users_model.get_u_name());
                    _downuser207.set_updateUserType(this.cz_users_model.get_u_type());
                    _downuser207.set_play_id(str505);
                    _downuser207.set_opt("mindz");
                    _downuser207.set_douvalue(double.Parse(_happycar.get_single_min_amount().ToString()));
                    list46.Add(_downuser207);
                }
                string str506 = "0";
                if (string.IsNullOrEmpty(this.cz_users_model.get_kc_kind()) || this.cz_users_model.get_kc_kind().Equals("0"))
                {
                    str506 = "0";
                }
                else
                {
                    str506 = this.cz_users_model.get_kc_kind();
                }
                if (!CallBLL.cz_drawback_happycar_bll.UpdateDataList(dictionary23, str506, flag2, list45, list46))
                {
                    mess = string.Format("快彩：修改【{0}】退水出現異常，請重試!", LSKeys.get_HAPPYCAR_Name());
                    isSuccess = false;
                    return;
                }
                base.user_drawback_kc_log(table46, CallBLL.cz_drawback_happycar_bll.GetDrawbackList(this.cz_users_model.get_u_name(), this.happycar_modify_playid).Tables[0], this.cz_users_model.get_u_name(), 0x16);
            }
            this.UpdateDrawback_kc_tpl(this.cz_users_model.get_u_name());
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
                    string str8 = LSRequest.qq(string.Format("six_a_{0}", str.ToLower()));
                    string str9 = LSRequest.qq(string.Format("six_b_{0}", str.ToLower()));
                    string str10 = LSRequest.qq(string.Format("six_c_{0}", str.ToLower()));
                    string str11 = LSRequest.qq(string.Format("six_max_amount_{0}", str.ToLower()));
                    string str12 = LSRequest.qq(string.Format("six_phase_amount_{0}", str.ToLower()));
                    string str13 = LSRequest.qq(string.Format("six_single_min_amount_{0}", str.ToLower()));
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
                                    goto Label_03AB;
                                }
                                if (str22 == "c")
                                {
                                    goto Label_03C1;
                                }
                            }
                            else if (double.Parse(str8) > double.Parse(s))
                            {
                                str8 = s;
                            }
                        }
                    }
                    goto Label_03D5;
                Label_03AB:
                    if (double.Parse(str9) > double.Parse(str3))
                    {
                        str9 = str3;
                    }
                    goto Label_03D5;
                Label_03C1:
                    if (double.Parse(str10) > double.Parse(str4))
                    {
                        str10 = str4;
                    }
                Label_03D5:
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
                    this.UpdateDrawback_six_tpl(this.cz_users_model.get_u_name());
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
                string str2 = LSRequest.qq(string.Format("{0}_a_{1}", lottoryStr, str));
                string str3 = LSRequest.qq(string.Format("{0}_b_{1}", lottoryStr, str));
                string str4 = LSRequest.qq(string.Format("{0}_c_{1}", lottoryStr, str));
                string str5 = LSRequest.qq(string.Format("{0}_max_amount_{1}", lottoryStr, str));
                string str6 = LSRequest.qq(string.Format("{0}_phase_amount_{1}", lottoryStr, str));
                string str7 = LSRequest.qq(string.Format("{0}_single_min_amount_{1}", lottoryStr, str));
                if (this.cz_users_model.get_kc_kind().ToLower().Equals("a"))
                {
                    if (string.IsNullOrEmpty(str2))
                    {
                        base.Un_User_Lock(this.rateKCModel.get_fgs_name());
                        base.Response.Write(base.GetAlert(base.GetGameNameByID(lottoryID) + "：A盤退水不能為空！"));
                        base.Response.End();
                    }
                    else
                    {
                        try
                        {
                            if (Convert.ToDouble(str2) < 0.0)
                            {
                                base.Un_User_Lock(this.rateKCModel.get_fgs_name());
                                base.Response.Write(base.GetAlert(base.GetGameNameByID(lottoryID) + "：A盤退水輸入值不能小於0！"));
                                base.Response.End();
                            }
                        }
                        catch
                        {
                            base.Un_User_Lock(this.rateKCModel.get_fgs_name());
                            base.Response.Write(base.GetAlert(base.GetGameNameByID(lottoryID) + "：A盤退水輸入值錯誤！"));
                            base.Response.End();
                        }
                    }
                }
                else if (this.cz_users_model.get_kc_kind().ToLower().Equals("b"))
                {
                    if (string.IsNullOrEmpty(str3))
                    {
                        base.Un_User_Lock(this.rateKCModel.get_fgs_name());
                        base.Response.Write(base.GetAlert(base.GetGameNameByID(lottoryID) + "：B盤退水不能為空！"));
                        base.Response.End();
                    }
                    else
                    {
                        try
                        {
                            if (Convert.ToDouble(str3) < 0.0)
                            {
                                base.Un_User_Lock(this.rateKCModel.get_fgs_name());
                                base.Response.Write(base.GetAlert(base.GetGameNameByID(lottoryID) + "：B盤退水輸入值不能小於！"));
                                base.Response.End();
                            }
                        }
                        catch
                        {
                            base.Un_User_Lock(this.rateKCModel.get_fgs_name());
                            base.Response.Write(base.GetAlert(base.GetGameNameByID(lottoryID) + "：B盤退水輸入值錯誤！"));
                            base.Response.End();
                        }
                    }
                }
                else if (this.cz_users_model.get_kc_kind().ToLower().Equals("c"))
                {
                    if (string.IsNullOrEmpty(str4))
                    {
                        base.Un_User_Lock(this.rateKCModel.get_fgs_name());
                        base.Response.Write(base.GetAlert(base.GetGameNameByID(lottoryID) + "：C盤退水不能為空！"));
                        base.Response.End();
                    }
                    else
                    {
                        try
                        {
                            if (Convert.ToDouble(str4) < 0.0)
                            {
                                base.Un_User_Lock(this.rateKCModel.get_fgs_name());
                                base.Response.Write(base.GetAlert(base.GetGameNameByID(lottoryID) + "：C盤退水輸入值不能小於0！"));
                                base.Response.End();
                            }
                        }
                        catch
                        {
                            base.Un_User_Lock(this.rateKCModel.get_fgs_name());
                            base.Response.Write(base.GetAlert(base.GetGameNameByID(lottoryID) + "：C盤退水輸入值錯誤！"));
                            base.Response.End();
                        }
                    }
                }
                else if (string.IsNullOrEmpty(str2))
                {
                    base.Un_User_Lock(this.rateKCModel.get_fgs_name());
                    base.Response.Write(base.GetAlert(base.GetGameNameByID(lottoryID) + "：A盤退水不能為空！"));
                    base.Response.End();
                }
                else if (string.IsNullOrEmpty(str3))
                {
                    base.Un_User_Lock(this.rateKCModel.get_fgs_name());
                    base.Response.Write(base.GetAlert(base.GetGameNameByID(lottoryID) + "：B盤退水不能為空！"));
                    base.Response.End();
                }
                else if (string.IsNullOrEmpty(str4))
                {
                    base.Un_User_Lock(this.rateKCModel.get_fgs_name());
                    base.Response.Write(base.GetAlert(base.GetGameNameByID(lottoryID) + "：C盤退水不能為空！"));
                    base.Response.End();
                }
                else
                {
                    try
                    {
                        if (Convert.ToDouble(str2) < 0.0)
                        {
                            base.Un_User_Lock(this.rateKCModel.get_fgs_name());
                            base.Response.Write(base.GetAlert(base.GetGameNameByID(lottoryID) + "：A盤退水輸入值不能小於0！"));
                            base.Response.End();
                        }
                        else if (Convert.ToDouble(str3) < 0.0)
                        {
                            base.Un_User_Lock(this.rateKCModel.get_fgs_name());
                            base.Response.Write(base.GetAlert(base.GetGameNameByID(lottoryID) + "：B盤退水輸入值不能小於0！"));
                            base.Response.End();
                        }
                        else if (Convert.ToDouble(str4) < 0.0)
                        {
                            base.Un_User_Lock(this.rateKCModel.get_fgs_name());
                            base.Response.Write(base.GetAlert(base.GetGameNameByID(lottoryID) + "：C盤退水輸入值不能小於0！"));
                            base.Response.End();
                        }
                    }
                    catch
                    {
                        base.Un_User_Lock(this.rateKCModel.get_fgs_name());
                        base.Response.Write(base.GetAlert(base.GetGameNameByID(lottoryID) + "：退水輸入值錯誤！"));
                        base.Response.End();
                    }
                }
                if (string.IsNullOrEmpty(str5))
                {
                    base.Un_User_Lock(this.rateKCModel.get_fgs_name());
                    base.Response.Write(base.GetAlert(base.GetGameNameByID(lottoryID) + "：單注最大限額不能為空！"));
                    base.Response.End();
                }
                else if (!Utils.IsInteger(str5))
                {
                    base.Un_User_Lock(this.rateKCModel.get_fgs_name());
                    base.Response.Write(base.GetAlert(base.GetGameNameByID(lottoryID) + "：單注最大限額只能為整數！"));
                    base.Response.End();
                }
                if (string.IsNullOrEmpty(str6))
                {
                    base.Un_User_Lock(this.rateKCModel.get_fgs_name());
                    base.Response.Write(base.GetAlert(base.GetGameNameByID(lottoryID) + "：單期最大限額不能為空！"));
                    base.Response.End();
                }
                else if (!Utils.IsInteger(str6))
                {
                    base.Un_User_Lock(this.rateKCModel.get_fgs_name());
                    base.Response.Write(base.GetAlert(base.GetGameNameByID(lottoryID) + "：單期最大限額只能為整數！"));
                    base.Response.End();
                }
                if (string.IsNullOrEmpty(str7))
                {
                    base.Un_User_Lock(this.rateKCModel.get_fgs_name());
                    base.Response.Write(base.GetAlert(base.GetGameNameByID(lottoryID) + "：最小單注不能為空！"));
                    base.Response.End();
                }
                else if (!Utils.IsInteger(str7))
                {
                    base.Un_User_Lock(this.rateKCModel.get_fgs_name());
                    base.Response.Write(base.GetAlert(base.GetGameNameByID(lottoryID) + "：最小單注只能為整數！"));
                    base.Response.End();
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
                    string str2 = LSRequest.qq(string.Format("six_a_{0}", str.ToLower()));
                    string str3 = LSRequest.qq(string.Format("six_b_{0}", str.ToLower()));
                    string str4 = LSRequest.qq(string.Format("six_c_{0}", str.ToLower()));
                    string str5 = LSRequest.qq(string.Format("six_max_amount_{0}", str.ToLower()));
                    string str6 = LSRequest.qq(string.Format("six_phase_amount_{0}", str.ToLower()));
                    string str7 = LSRequest.qq(string.Format("six_single_min_amount_{0}", str.ToLower()));
                    if (this.cz_users_model.get_six_kind().ToLower().Equals("a"))
                    {
                        if (string.IsNullOrEmpty(str2))
                        {
                            base.Un_User_Lock(this.rateKCModel.get_fgs_name());
                            base.Response.Write(base.GetAlert("⑥合彩：A盤退水不能為空！"));
                            base.Response.End();
                        }
                        else
                        {
                            try
                            {
                                if (Convert.ToDouble(str2) < 0.0)
                                {
                                    base.Un_User_Lock(this.rateKCModel.get_fgs_name());
                                    base.Response.Write(base.GetAlert("⑥合彩：A盤退水輸入值不能小於0！"));
                                    base.Response.End();
                                }
                            }
                            catch
                            {
                                base.Un_User_Lock(this.rateKCModel.get_fgs_name());
                                base.Response.Write(base.GetAlert("⑥合彩：A盤退水輸入值錯誤！"));
                                base.Response.End();
                            }
                        }
                    }
                    else if (this.cz_users_model.get_six_kind().ToLower().Equals("b"))
                    {
                        if (string.IsNullOrEmpty(str3))
                        {
                            base.Un_User_Lock(this.rateKCModel.get_fgs_name());
                            base.Response.Write(base.GetAlert("⑥合彩：B盤退水不能為空！"));
                            base.Response.End();
                        }
                        else
                        {
                            try
                            {
                                if (Convert.ToDouble(str3) < 0.0)
                                {
                                    base.Un_User_Lock(this.rateKCModel.get_fgs_name());
                                    base.Response.Write(base.GetAlert("⑥合彩：B盤退水輸入值不能小於0！"));
                                    base.Response.End();
                                }
                            }
                            catch
                            {
                                base.Un_User_Lock(this.rateKCModel.get_fgs_name());
                                base.Response.Write(base.GetAlert("⑥合彩：B盤退水輸入值錯誤！"));
                                base.Response.End();
                            }
                        }
                    }
                    else if (this.cz_users_model.get_six_kind().ToLower().Equals("c"))
                    {
                        if (string.IsNullOrEmpty(str4))
                        {
                            base.Un_User_Lock(this.rateKCModel.get_fgs_name());
                            base.Response.Write(base.GetAlert("⑥合彩：C盤退水不能為空！"));
                            base.Response.End();
                        }
                        else
                        {
                            try
                            {
                                if (Convert.ToDouble(str4) < 0.0)
                                {
                                    base.Un_User_Lock(this.rateKCModel.get_fgs_name());
                                    base.Response.Write(base.GetAlert("⑥合彩：C盤退水輸入值不能小於0！"));
                                    base.Response.End();
                                }
                            }
                            catch
                            {
                                base.Un_User_Lock(this.rateKCModel.get_fgs_name());
                                base.Response.Write(base.GetAlert("⑥合彩：C盤退水輸入值錯誤！"));
                                base.Response.End();
                            }
                        }
                    }
                    else if (string.IsNullOrEmpty(str2))
                    {
                        base.Un_User_Lock(this.rateKCModel.get_fgs_name());
                        base.Response.Write(base.GetAlert("⑥合彩：A盤退水不能為空！"));
                        base.Response.End();
                    }
                    else if (string.IsNullOrEmpty(str3))
                    {
                        base.Un_User_Lock(this.rateKCModel.get_fgs_name());
                        base.Response.Write(base.GetAlert("⑥合彩：B盤退水不能為空！"));
                        base.Response.End();
                    }
                    else if (string.IsNullOrEmpty(str4))
                    {
                        base.Un_User_Lock(this.rateKCModel.get_fgs_name());
                        base.Response.Write(base.GetAlert("⑥合彩：C盤退水不能為空！"));
                        base.Response.End();
                    }
                    else
                    {
                        try
                        {
                            if (Convert.ToDouble(str2) < 0.0)
                            {
                                base.Un_User_Lock(this.rateKCModel.get_fgs_name());
                                base.Response.Write(base.GetAlert("⑥合彩：A盤退水輸入值不能小於0！"));
                                base.Response.End();
                            }
                            if (Convert.ToDouble(str3) < 0.0)
                            {
                                base.Un_User_Lock(this.rateKCModel.get_fgs_name());
                                base.Response.Write(base.GetAlert("⑥合彩：B盤退水輸入值不能小於0！"));
                                base.Response.End();
                            }
                            if (Convert.ToDouble(str4) < 0.0)
                            {
                                base.Un_User_Lock(this.rateKCModel.get_fgs_name());
                                base.Response.Write(base.GetAlert("⑥合彩：C盤退水輸入值不能小於0！"));
                                base.Response.End();
                            }
                        }
                        catch
                        {
                            base.Un_User_Lock(this.rateKCModel.get_fgs_name());
                            base.Response.Write(base.GetAlert("⑥合彩：退水輸入值錯誤！"));
                            base.Response.End();
                        }
                    }
                    if (string.IsNullOrEmpty(str5))
                    {
                        base.Un_User_Lock(this.rateKCModel.get_fgs_name());
                        base.Response.Write(base.GetAlert("⑥合彩：單注最大限額不能為空！"));
                        base.Response.End();
                    }
                    else if (!Utils.IsInteger(str5))
                    {
                        base.Un_User_Lock(this.rateKCModel.get_fgs_name());
                        base.Response.Write(base.GetAlert("⑥合彩：單注最大限額只能為整數！"));
                        base.Response.End();
                    }
                    if (string.IsNullOrEmpty(str6))
                    {
                        base.Un_User_Lock(this.rateKCModel.get_fgs_name());
                        base.Response.Write(base.GetAlert("⑥合彩：單期最大限額不能為空！"));
                        base.Response.End();
                    }
                    else if (!Utils.IsInteger(str6))
                    {
                        base.Un_User_Lock(this.rateKCModel.get_fgs_name());
                        base.Response.Write(base.GetAlert("⑥合彩：單期最大限額只能為整數！"));
                        base.Response.End();
                    }
                    if (string.IsNullOrEmpty(str7))
                    {
                        base.Un_User_Lock(this.rateKCModel.get_fgs_name());
                        base.Response.Write(base.GetAlert("⑥合彩：最小單注不能為空！"));
                        base.Response.End();
                    }
                    else if (!Utils.IsInteger(str7))
                    {
                        base.Un_User_Lock(this.rateKCModel.get_fgs_name());
                        base.Response.Write(base.GetAlert("⑥合彩：最小單注只能為整數！"));
                        base.Response.End();
                    }
                }
            }
        }
    }
}

