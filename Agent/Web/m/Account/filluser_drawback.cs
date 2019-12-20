namespace Agent.Web.m.Account
{
    using Agent.Web.WebBase;
    using LotterySystem.Common;
    using LotterySystem.Model;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;

    public class filluser_drawback : MemberPageBase_Mobile
    {
        protected cz_saleset_six cz_saleset_six_model;
        private Dictionary<string, Dictionary<string, string>> DICT = new Dictionary<string, Dictionary<string, string>>();
        protected string drawBackjson = "";
        protected string lm_max_amount = "";
        protected string lm_phase_amount = "";
        protected string lm_pk_a = "";
        protected string lm_pk_b = "";
        protected string lm_pk_c = "";
        protected string lmp_max_amount = "";
        protected string lmp_phase_amount = "";
        protected string lmp_pk_a = "";
        protected string lmp_pk_b = "";
        protected string lmp_pk_c = "";
        private DataTable lotteryDT;
        protected string lottrty_kc = "";
        protected string lottrty_six = "";
        protected string namestr = "";
        protected string newAdd = "";
        private Dictionary<string, string> paramDict = new Dictionary<string, string>();
        protected string pk_kc = "";
        protected string pk_kc_a = "";
        protected string pk_kc_b = "";
        protected string pk_kc_c = "";
        protected string pk_six = "";
        protected string pk_six_a = "";
        protected string pk_six_b = "";
        protected string pk_six_c = "";
        protected string shortcut_blue = "display:none;";
        protected string shortcut_cqsc_lmp = "2,3,16,17,18";
        protected string shortcut_cqsc_tm = "1";
        protected string shortcut_green = "display:none;";
        protected string shortcut_jsk3_lmp = "58";
        protected string shortcut_kl10_lm = "72,73,74,75,76,77,78,79";
        protected string shortcut_kl10_lmp = "82,83,84,85,121,122,11,12,13,80";
        protected string shortcut_kl10_tm = "81";
        protected string shortcut_kl8_lmp = "65";
        protected string shortcut_pk10_lmp = "2,3,4,37,37";
        protected string shortcut_pk10_tm = "1";
        protected string shortcut_violet = "display:none;";
        protected string shortcut_xync_lm = "72,73,74,75,78,79";
        protected string shortcut_xync_lmp = "82,83,84,85,121,122,11,12,13,80";
        protected string shortcut_xync_tm = "81";
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
        protected string six_modify_playid = "";
        private string six_shortcut_lm = "91016,91017,91018,91019,91020,91030,91031,91032,91033,91034,91035,91036,91037,91040,91047,91048,91049,91050,91051,91058,91059";
        private string six_shortcut_lmp = "91003,91004,91005,91011,91012,91013,91014,91023,91024,91038,91041,91042,91043,91044,91045,91046";
        private string six_shortcut_tm = "91001,91002,91006,91007,91008,91057,91009,91010,91015,91021,91022,91025,91026,91027,91028,91029,91039,91052,91053,91054,91055,91056";
        private string six_tm_max_amount = "";
        private string six_tm_phase_amount = "";
        private string six_tm_pk_a = "";
        private string six_tm_pk_b = "";
        private string six_tm_pk_c = "";
        private string six_tm_single_min_amount = "";
        protected string string_six = "";
        protected DataTable table_six;
        protected string tabState_kc = "";
        protected string tabState_six = "";
        protected string tm_max_amount = "";
        protected string tm_phase_amount = "";
        protected string tm_pk_a = "";
        protected string tm_pk_b = "";
        protected string tm_pk_c = "";
        protected string userid = "";
        private agent_userinfo_session userModel;

        private void getDrawBack(ref string strResult)
        {
            base.checkLoginByHandler(0);
            string str = this.Session["user_name"].ToString();
            this.userModel = this.Session[str + "lottery_session_user_info"] as agent_userinfo_session;
            if (!this.userModel.get_u_type().Trim().Equals("zj"))
            {
                base.Response.End();
            }
            if (!base.IsOpenBigLottery(1))
            {
                base.Response.End();
            }
            base.Permission_Aspx_ZJ(this.userModel, "po_2_3");
            if ((this.userModel.get_users_child_session() != null) && (this.userModel.get_users_child_session().get_permissions_name().IndexOf("po_2_3") < 0))
            {
                base.Response.End();
            }
            this.userid = LSRequest.qq("uid");
            this.newAdd = LSRequest.qq("isadd");
            string str2 = LSRequest.qq("submitType");
            if ((str2 != "view") && (str2 != "edit"))
            {
                base.Response.End();
            }
            this.newAdd = "0";
            this.cz_saleset_six_model = CallBLL.cz_saleset_six_bll.GetModel(this.userid);
            ReturnResult result = new ReturnResult();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            if (this.cz_saleset_six_model == null)
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
            if (!string.IsNullOrEmpty(this.lottrty_six))
            {
                this.tabState_six = "on";
            }
            else
            {
                base.Response.Redirect("/Quit.aspx");
            }
            if (this.cz_saleset_six_model.get_six_kind().ToLower() != "0")
            {
                if (this.cz_saleset_six_model.get_six_kind().ToLower() != "a")
                {
                    this.pk_six_a = "display:none;";
                }
                if (this.cz_saleset_six_model.get_six_kind().ToLower() != "b")
                {
                    this.pk_six_b = "display:none;";
                }
                if (this.cz_saleset_six_model.get_six_kind().ToLower() != "c")
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
            this.InitData();
            this.InitShortCut_SIX(this.cz_saleset_six_model.get_six_kind(), this.cz_saleset_six_model);
            switch (str2)
            {
                case "view":
                {
                    string str3 = "1";
                    Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
                    dictionary2.Add("disabled", str3);
                    dictionary2.Add("userKind", this.cz_saleset_six_model.get_six_kind());
                    dictionary2.Add("A", this.six_tm_pk_a);
                    dictionary2.Add("B", this.six_tm_pk_b);
                    dictionary2.Add("C", this.six_tm_pk_c);
                    dictionary2.Add("dzMaxAmount", this.six_tm_max_amount);
                    dictionary2.Add("dqMaxAmount", this.six_tm_phase_amount);
                    dictionary2.Add("dzMinAmount", this.six_tm_single_min_amount);
                    dictionary.Add("SixTM", dictionary2);
                    Dictionary<string, object> dictionary3 = new Dictionary<string, object>();
                    dictionary3.Add("disabled", str3);
                    dictionary3.Add("userKind", this.cz_saleset_six_model.get_six_kind());
                    dictionary3.Add("A", this.six_lmp_pk_a);
                    dictionary3.Add("B", this.six_lmp_pk_b);
                    dictionary3.Add("C", this.six_lmp_pk_c);
                    dictionary3.Add("dzMaxAmount", this.six_lmp_max_amount);
                    dictionary3.Add("dqMaxAmount", this.six_lmp_phase_amount);
                    dictionary3.Add("dzMinAmount", this.six_lmp_single_min_amount);
                    dictionary.Add("SixLMP", dictionary3);
                    Dictionary<string, object> dictionary4 = new Dictionary<string, object>();
                    dictionary4.Add("disabled", str3);
                    dictionary4.Add("userKind", this.cz_saleset_six_model.get_six_kind());
                    dictionary4.Add("A", this.six_lm_pk_a);
                    dictionary4.Add("B", this.six_lm_pk_b);
                    dictionary4.Add("C", this.six_lm_pk_c);
                    dictionary4.Add("dzMaxAmount", this.six_lm_max_amount);
                    dictionary4.Add("dqMaxAmount", this.six_lm_phase_amount);
                    dictionary4.Add("dzMinAmount", this.six_lm_single_min_amount);
                    dictionary.Add("SixLM", dictionary4);
                    result.set_success(200);
                    dictionary.Add("name", this.cz_saleset_six_model.get_u_name());
                    result.set_data(dictionary);
                    strResult = base.ObjectToJson(result);
                    return;
                }
                case "edit":
                {
                    base.En_User_Lock(this.cz_saleset_six_model.get_u_name());
                    this.PreDoParam(ref this.namestr);
                    this.GetModifyPlays(this.namestr);
                    string mess = "";
                    if ((this.table_six != null) && !string.IsNullOrEmpty(this.six_modify_playid))
                    {
                        this.ValidSix();
                        this.UpdateSix(ref mess);
                        if (!this.newAdd.Equals("1"))
                        {
                            FileCacheHelper.UpdateDrawbackFile(100);
                        }
                    }
                    base.Un_User_Lock(this.cz_saleset_six_model.get_u_name());
                    if (!string.IsNullOrEmpty(mess))
                    {
                        base.successOptMsg(mess);
                        return;
                    }
                    base.successOptMsg("無數據修改!");
                    break;
                }
            }
        }

        private void GetModifyPlays(string nameStr)
        {
            if (!string.IsNullOrEmpty(nameStr))
            {
                this.six_modify_playid = "";
                string[] strArray = nameStr.Split(new char[] { ',' });
                for (int i = 0; i < strArray.Length; i++)
                {
                    string str3;
                    string str = strArray[i].Split(new char[] { '_' })[0].ToLower();
                    string str2 = strArray[i].Split(new char[] { '_' })[1];
                    if (((str3 = str) != null) && (str3 == "six"))
                    {
                        if (string.IsNullOrEmpty(this.six_modify_playid))
                        {
                            this.six_modify_playid = this.six_modify_playid + str2;
                        }
                        else
                        {
                            this.six_modify_playid = this.six_modify_playid + "," + str2;
                        }
                    }
                }
            }
        }

        private void InitData()
        {
            if (!string.IsNullOrEmpty(this.lottrty_six))
            {
                if (this.cz_saleset_six_model.get_flag().Equals(1))
                {
                    DataSet set = CallBLL.cz_drawback_six_bll.GetDrawBackList_Ex(this.cz_saleset_six_model.get_u_name(), DESEncrypt.DecryptStringDES(this.cz_saleset_six_model.get_sqlconn(), this.cz_saleset_six_model.get_salt()));
                    this.table_six = set.Tables[0];
                }
                else
                {
                    DataSet drawBackList = CallBLL.cz_drawback_six_bll.GetDrawBackList(this.cz_saleset_six_model.get_u_name());
                    this.table_six = drawBackList.Tables[0];
                }
            }
        }

        protected void InitShortCut_SIX(string pk, cz_saleset_six cz_saleset_six_model)
        {
            DataSet drawBackList = CallBLL.cz_drawback_six_bll.GetDrawBackList(cz_saleset_six_model.get_u_name());
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
            IEnumerable<string> values = sixLMList.Concat<string>(sixLMPList).Concat<string>(sixTMList).Concat<string>(second).Concat<string>(list3).Concat<string>(list2);
            namestr = string.Join(",", values);
        }

        private void UpdateSix(ref string mess)
        {
            if (!string.IsNullOrEmpty(this.six_modify_playid))
            {
                DataTable oldDT = CallBLL.cz_drawback_six_bll.GetDrawBackList(this.cz_saleset_six_model.get_u_name(), this.six_modify_playid).Tables[0];
                foreach (DataRow row in oldDT.Rows)
                {
                    string str = row["play_id"].ToString();
                    row["a_drawback"].ToString();
                    row["b_drawback"].ToString();
                    row["c_drawback"].ToString();
                    row["single_max_amount"].ToString();
                    row["single_phase_amount"].ToString();
                    string str2 = this.paramDict[string.Format("six_a_{0}", str.ToLower())];
                    string str3 = this.paramDict[string.Format("six_b_{0}", str.ToLower())];
                    string str4 = this.paramDict[string.Format("six_c_{0}", str.ToLower())];
                    string str5 = this.paramDict[string.Format("six_max_amount_{0}", str.ToLower())];
                    string str6 = this.paramDict[string.Format("six_phase_amount_{0}", str.ToLower())];
                    cz_drawback_six _six = new cz_drawback_six();
                    _six.set_play_id(new int?(Convert.ToInt32(str)));
                    if (!string.IsNullOrEmpty(str2))
                    {
                        _six.set_a_drawback(new decimal?(Convert.ToDecimal(str2)));
                    }
                    if (!string.IsNullOrEmpty(str3))
                    {
                        _six.set_b_drawback(new decimal?(Convert.ToDecimal(str3)));
                    }
                    if (!string.IsNullOrEmpty(str4))
                    {
                        _six.set_c_drawback(new decimal?(Convert.ToDecimal(str4)));
                    }
                    _six.set_single_max_amount(new decimal?(Convert.ToDecimal(str5)));
                    _six.set_single_phase_amount(new decimal?(Convert.ToDecimal(str6)));
                    _six.set_u_name(this.cz_saleset_six_model.get_u_name());
                    string str7 = this.cz_saleset_six_model.get_six_kind();
                    bool flag = false;
                    CallBLL.cz_drawback_six_bll.UpdateData(_six, str7, flag);
                }
                base.user_drawback_six_log(oldDT, CallBLL.cz_drawback_six_bll.GetDrawBackList(this.cz_saleset_six_model.get_u_name()).Tables[0], this.cz_saleset_six_model.get_u_name());
                mess = "⑥合彩：已經成功修改設置!";
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
                    if (this.cz_saleset_six_model.get_six_kind().ToLower().Equals("a"))
                    {
                        if (string.IsNullOrEmpty(str2))
                        {
                            base.noRightOptMsg("⑥合彩：A盤退水不能為空！");
                            base.Response.End();
                        }
                        else
                        {
                            try
                            {
                                if (Convert.ToDouble(str2) < 0.0)
                                {
                                    base.noRightOptMsg("⑥合彩：A盤退水輸入值不能小於0！");
                                    base.Response.End();
                                }
                            }
                            catch
                            {
                                base.noRightOptMsg("⑥合彩：A盤退水輸入值錯誤！");
                                base.Response.End();
                            }
                        }
                    }
                    else if (this.cz_saleset_six_model.get_six_kind().ToLower().Equals("b"))
                    {
                        if (string.IsNullOrEmpty(str3))
                        {
                            base.noRightOptMsg("⑥合彩：B盤退水不能為空！");
                            base.Response.End();
                        }
                        else
                        {
                            try
                            {
                                if (Convert.ToDouble(str3) < 0.0)
                                {
                                    base.noRightOptMsg("⑥合彩：B盤退水輸入值不能小於0！");
                                    base.Response.End();
                                }
                            }
                            catch
                            {
                                base.noRightOptMsg("⑥合彩：B盤退水輸入值錯誤！");
                                base.Response.End();
                            }
                        }
                    }
                    else if (this.cz_saleset_six_model.get_six_kind().ToLower().Equals("c"))
                    {
                        if (string.IsNullOrEmpty(str4))
                        {
                            base.noRightOptMsg("⑥合彩：C盤退水不能為空！");
                            base.Response.End();
                        }
                        else
                        {
                            try
                            {
                                if (Convert.ToDouble(str4) < 0.0)
                                {
                                    base.noRightOptMsg("⑥合彩：C盤退水輸入值不能小於0！");
                                    base.Response.End();
                                }
                            }
                            catch
                            {
                                base.noRightOptMsg("⑥合彩：C盤退水輸入值錯誤！");
                                base.Response.End();
                            }
                        }
                    }
                    else if (string.IsNullOrEmpty(str2))
                    {
                        base.noRightOptMsg("⑥合彩：A盤退水不能為空！");
                        base.Response.End();
                    }
                    else if (string.IsNullOrEmpty(str3))
                    {
                        base.noRightOptMsg("⑥合彩：B盤退水不能為空！");
                        base.Response.End();
                    }
                    else if (string.IsNullOrEmpty(str4))
                    {
                        base.noRightOptMsg("⑥合彩：C盤退水不能為空！");
                        base.Response.End();
                    }
                    else
                    {
                        try
                        {
                            if (Convert.ToDouble(str2) < 0.0)
                            {
                                base.noRightOptMsg("⑥合彩：A盤退水輸入值不能小於0！");
                                base.Response.End();
                            }
                            else if (Convert.ToDouble(str3) < 0.0)
                            {
                                base.noRightOptMsg("⑥合彩：B盤退水輸入值不能小於0！");
                                base.Response.End();
                            }
                            else if (Convert.ToDouble(str4) < 0.0)
                            {
                                base.noRightOptMsg("⑥合彩：C盤退水輸入值不能小於0！");
                                base.Response.End();
                            }
                        }
                        catch
                        {
                            base.noRightOptMsg("⑥合彩：退水輸入值錯誤！");
                            base.Response.End();
                        }
                    }
                    if (string.IsNullOrEmpty(str5))
                    {
                        base.noRightOptMsg("⑥合彩：單注最大限額不能為空！");
                        base.Response.End();
                    }
                    else if (!Utils.IsInteger(str5))
                    {
                        base.noRightOptMsg("⑥合彩：單注最大限額不能只能為整數！");
                        base.Response.End();
                    }
                    if (string.IsNullOrEmpty(str6))
                    {
                        base.noRightOptMsg("⑥合彩：單期最大限額不能為空！");
                        base.Response.End();
                    }
                    else if (!Utils.IsInteger(str6))
                    {
                        base.noRightOptMsg("⑥合彩：單期最大限額只能為整數！");
                        base.Response.End();
                    }
                }
            }
        }
    }
}

