namespace Agent.Web.Account
{
    using Agent.Web.WebBase;
    using LotterySystem.Common;
    using LotterySystem.Model;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class filluser_drawback : MemberPageBase
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
        protected string six_modify_playid = "";
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

        private void GetModifyPlays(string nameStr)
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

        protected Dictionary<string, string> GetUpDrawbackSix(DataTable table, string lottoryStr, string pk)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            foreach (DataRow row in table.Rows)
            {
                string str = row["play_id"].ToString().ToLower();
                if (string.IsNullOrEmpty(pk) || pk.Equals("0"))
                {
                    dictionary.Add(string.Format("{0}_a_{1}", lottoryStr, str), row["a_drawback"].ToString());
                    dictionary.Add(string.Format("{0}_b_{1}", lottoryStr, str), row["b_drawback"].ToString());
                    dictionary.Add(string.Format("{0}_c_{1}", lottoryStr, str), row["c_drawback"].ToString());
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
                                goto Label_0117;
                            }
                            if (str2 == "c")
                            {
                                goto Label_013B;
                            }
                        }
                        else
                        {
                            dictionary.Add(string.Format("{0}_a_{1}", lottoryStr, str), row["a_drawback"].ToString());
                        }
                    }
                }
                goto Label_015D;
            Label_0117:
                dictionary.Add(string.Format("{0}_b_{1}", lottoryStr, str), row["b_drawback"].ToString());
                goto Label_015D;
            Label_013B:
                dictionary.Add(string.Format("{0}_c_{1}", lottoryStr, str), row["c_drawback"].ToString());
            Label_015D:
                dictionary.Add(string.Format("{0}_max_amount_{1}", lottoryStr, str), string.Format("{0:F0}", Convert.ToDouble(row["single_max_amount"].ToString())));
                dictionary.Add(string.Format("{0}_phase_amount_{1}", lottoryStr, str), string.Format("{0:F0}", Convert.ToDouble(row["single_phase_amount"].ToString())));
            }
            return dictionary;
        }

        private void InitData()
        {
            if (!string.IsNullOrEmpty(this.lottrty_six))
            {
                if (this.cz_saleset_six_model.get_flag().Equals(1))
                {
                    DataSet set = null;
                    if (!FileCacheHelper.get_IsShowLM_B())
                    {
                        set = CallBLL.cz_drawback_six_bll.GetDrawBackList_Ex(this.cz_saleset_six_model.get_u_name(), DESEncrypt.DecryptStringDES(this.cz_saleset_six_model.get_sqlconn(), this.cz_saleset_six_model.get_salt()), "91060,91061,91062,91063,91064,91065");
                    }
                    else
                    {
                        set = CallBLL.cz_drawback_six_bll.GetDrawBackList_Ex(this.cz_saleset_six_model.get_u_name(), DESEncrypt.DecryptStringDES(this.cz_saleset_six_model.get_sqlconn(), this.cz_saleset_six_model.get_salt()));
                    }
                    this.table_six = set.Tables[0];
                }
                else
                {
                    DataSet drawBackList = null;
                    if (!FileCacheHelper.get_IsShowLM_B())
                    {
                        drawBackList = CallBLL.cz_drawback_six_bll.User_GetDrawBackList(this.cz_saleset_six_model.get_u_name(), "91060,91061,91062,91063,91064,91065");
                    }
                    else
                    {
                        drawBackList = CallBLL.cz_drawback_six_bll.GetDrawBackList(this.cz_saleset_six_model.get_u_name());
                    }
                    if (((drawBackList != null) && (drawBackList.Tables.Count > 0)) && (drawBackList.Tables[0].Rows.Count > 0))
                    {
                        this.table_six = drawBackList.Tables[0];
                        DataTable zJDrawback = CallBLL.cz_drawback_six_bll.GetZJDrawback();
                        this.DICT.Add("six", this.GetUpDrawbackSix(zJDrawback, "six", this.cz_saleset_six_model.get_six_kind()));
                    }
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string str = this.Session["user_name"].ToString();
            agent_userinfo_session model = this.Session[str + "lottery_session_user_info"] as agent_userinfo_session;
            if (!model.get_u_type().Trim().Equals("zj"))
            {
                base.Response.Redirect("../MessagePage.aspx?code=u100035&url=&issuccess=1&isback=0&isopen=1");
                base.Response.End();
            }
            base.Permission_Aspx_ZJ(model, "po_2_3");
            this.userid = LSRequest.qq("uid");
            this.newAdd = LSRequest.qq("isadd");
            this.cz_saleset_six_model = CallBLL.cz_saleset_six_bll.GetModel(this.userid);
            if (this.cz_saleset_six_model == null)
            {
                base.Response.Redirect("../MessagePage.aspx?code=u100034&url=&issuccess=1&isback=0&isopen=1");
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
            this.drawBackjson = JsonHandle.ObjectToJson(this.DICT);
            if (LSRequest.qq("hdnSubmit").Equals("hdnsubmit") && !this.cz_saleset_six_model.get_flag().Equals(1))
            {
                string mess = "";
                string str4 = "";
                this.namestr = LSRequest.qq("namestr");
                this.GetModifyPlays(this.namestr);
                if ((this.table_six != null) && !string.IsNullOrEmpty(this.six_modify_playid))
                {
                    this.ValidSix();
                    this.UpdateSix(ref mess);
                }
                string str5 = mess + "<br />" + str4;
                if (string.IsNullOrEmpty(this.newAdd))
                {
                    this.newAdd = "0";
                }
                base.Response.Write(base.ShowDialogBox(str5, base.UserReturnBackUrl, 0));
                base.Response.End();
            }
        }

        private void UpdateSix(ref string mess)
        {
            bool flag = false;
            bool flag2 = false;
            bool flag3 = false;
            if (flag || flag2)
            {
                flag3 = true;
            }
            DataTable zJDrawback = CallBLL.cz_drawback_six_bll.GetZJDrawback(this.six_modify_playid);
            DataTable table1 = CallBLL.cz_drawback_six_bll.GetDrawBackList(this.cz_saleset_six_model.get_u_name(), this.six_modify_playid).Tables[0];
            foreach (DataRow row in zJDrawback.Rows)
            {
                string str = row["play_id"].ToString();
                string s = row["a_drawback"].ToString();
                string str3 = row["b_drawback"].ToString();
                string str4 = row["c_drawback"].ToString();
                string str5 = row["single_max_amount"].ToString();
                string str6 = row["single_phase_amount"].ToString();
                string str7 = LSRequest.qq(string.Format("six_a_{0}", str.ToLower()));
                string str8 = LSRequest.qq(string.Format("six_b_{0}", str.ToLower()));
                string str9 = LSRequest.qq(string.Format("six_c_{0}", str.ToLower()));
                string str10 = LSRequest.qq(string.Format("six_max_amount_{0}", str.ToLower()));
                string str11 = LSRequest.qq(string.Format("six_phase_amount_{0}", str.ToLower()));
                if (string.IsNullOrEmpty(this.cz_saleset_six_model.get_six_kind()) || this.cz_saleset_six_model.get_six_kind().Equals("0"))
                {
                    if (double.Parse(str7) > double.Parse(s))
                    {
                        str7 = s;
                    }
                    if (double.Parse(str8) > double.Parse(str3))
                    {
                        str8 = str3;
                    }
                    if (double.Parse(str9) > double.Parse(str4))
                    {
                        str9 = str4;
                    }
                }
                else
                {
                    string str13 = this.cz_saleset_six_model.get_six_kind().ToLower();
                    if (str13 != null)
                    {
                        if (!(str13 == "a"))
                        {
                            if (str13 == "b")
                            {
                                goto Label_0214;
                            }
                            if (str13 == "c")
                            {
                                goto Label_022A;
                            }
                        }
                        else if (double.Parse(str7) > double.Parse(s))
                        {
                            str7 = s;
                        }
                    }
                }
                goto Label_023E;
            Label_0214:
                if (double.Parse(str8) > double.Parse(str3))
                {
                    str8 = str3;
                }
                goto Label_023E;
            Label_022A:
                if (double.Parse(str9) > double.Parse(str4))
                {
                    str9 = str4;
                }
            Label_023E:
                if (double.Parse(str10) > double.Parse(str5))
                {
                    str10 = str5;
                }
                if (double.Parse(str11) > double.Parse(str6))
                {
                    str11 = str6;
                }
                cz_drawback_six _six = new cz_drawback_six();
                _six.set_play_id(new int?(Convert.ToInt32(str)));
                if (!string.IsNullOrEmpty(str7))
                {
                    _six.set_a_drawback(new decimal?(Convert.ToDecimal(str7)));
                }
                if (!string.IsNullOrEmpty(str8))
                {
                    _six.set_b_drawback(new decimal?(Convert.ToDecimal(str8)));
                }
                if (!string.IsNullOrEmpty(str9))
                {
                    _six.set_c_drawback(new decimal?(Convert.ToDecimal(str9)));
                }
                _six.set_single_max_amount(new decimal?(Convert.ToDecimal(str10)));
                _six.set_single_phase_amount(new decimal?(Convert.ToDecimal(str11)));
                _six.set_u_name(this.cz_saleset_six_model.get_u_name());
                string str12 = "A";
                if (string.IsNullOrEmpty(this.cz_saleset_six_model.get_six_kind()) || this.cz_saleset_six_model.get_six_kind().Equals("0"))
                {
                    str12 = "A";
                }
                else
                {
                    str12 = this.cz_saleset_six_model.get_six_kind();
                }
                CallBLL.cz_drawback_six_bll.UpdateData(_six, str12, flag3);
            }
            base.user_drawback_six_log(zJDrawback, CallBLL.cz_drawback_six_bll.GetDrawBackList(this.cz_saleset_six_model.get_u_name()).Tables[0], this.cz_saleset_six_model.get_u_name());
            if (flag || flag2)
            {
                mess = "⑥合彩：因獎期正在開盤中且下级已经有下单[退水]未能修改,單期單注修改成功!";
            }
            else
            {
                mess = "⑥合彩：已經成功修改設置(注:如開盤中且下级已经有下单退水不能修改)!";
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
                    if (this.cz_saleset_six_model.get_six_kind().Equals("a"))
                    {
                        if (string.IsNullOrEmpty(str2))
                        {
                            base.Response.Write(base.ShowDialogBox("⑥合彩：A盤退水不能為空！", null, 400));
                            base.Response.End();
                        }
                        else
                        {
                            try
                            {
                                if (Convert.ToDouble(str2) < 0.0)
                                {
                                    base.Response.Write(base.ShowDialogBox("⑥合彩：A盤退水輸入值不能小於0！", null, 400));
                                    base.Response.End();
                                }
                            }
                            catch
                            {
                                base.Response.Write(base.ShowDialogBox("⑥合彩：A盤退水輸入值錯誤！", null, 400));
                                base.Response.End();
                            }
                        }
                    }
                    else if (this.cz_saleset_six_model.get_six_kind().Equals("b"))
                    {
                        if (string.IsNullOrEmpty(str3))
                        {
                            base.Response.Write(base.ShowDialogBox("⑥合彩：B盤退水不能為空！", null, 400));
                            base.Response.End();
                        }
                        else
                        {
                            try
                            {
                                if (Convert.ToDouble(str3) < 0.0)
                                {
                                    base.Response.Write(base.ShowDialogBox("⑥合彩：B盤退水輸入值不能小於0！", null, 400));
                                    base.Response.End();
                                }
                            }
                            catch
                            {
                                base.Response.Write(base.ShowDialogBox("⑥合彩：B盤退水輸入值錯誤！", null, 400));
                                base.Response.End();
                            }
                        }
                    }
                    else if (this.cz_saleset_six_model.get_six_kind().Equals("c"))
                    {
                        if (string.IsNullOrEmpty(str4))
                        {
                            base.Response.Write(base.ShowDialogBox("⑥合彩：C盤退水不能為空！", null, 400));
                            base.Response.End();
                        }
                        else
                        {
                            try
                            {
                                if (Convert.ToDouble(str4) < 0.0)
                                {
                                    base.Response.Write(base.ShowDialogBox("⑥合彩：C盤退水輸入值不能小於0！", null, 400));
                                    base.Response.End();
                                }
                            }
                            catch
                            {
                                base.Response.Write(base.ShowDialogBox("⑥合彩：C盤退水輸入值錯誤！", null, 400));
                                base.Response.End();
                            }
                        }
                    }
                    else if (string.IsNullOrEmpty(str2))
                    {
                        base.Response.Write(base.ShowDialogBox("⑥合彩：A盤退水不能為空！", null, 400));
                        base.Response.End();
                    }
                    else if (string.IsNullOrEmpty(str3))
                    {
                        base.Response.Write(base.ShowDialogBox("⑥合彩：A盤退水不能為空！", null, 400));
                        base.Response.End();
                    }
                    else if (string.IsNullOrEmpty(str4))
                    {
                        base.Response.Write(base.ShowDialogBox("⑥合彩：C盤退水不能為空！", null, 400));
                        base.Response.End();
                    }
                    else
                    {
                        try
                        {
                            if (Convert.ToDouble(str2) < 0.0)
                            {
                                base.Response.Write(base.ShowDialogBox("⑥合彩：A盤退水輸入值不能小於0！", null, 400));
                                base.Response.End();
                            }
                            else if (Convert.ToDouble(str3) < 0.0)
                            {
                                base.Response.Write(base.ShowDialogBox("⑥合彩：B盤退水輸入值不能小於0！", null, 400));
                                base.Response.End();
                            }
                            else if (Convert.ToDouble(str4) < 0.0)
                            {
                                base.Response.Write(base.ShowDialogBox("⑥合彩：C盤退水輸入值不能小於0！", null, 400));
                                base.Response.End();
                            }
                        }
                        catch
                        {
                            base.Response.Write(base.ShowDialogBox("⑥合彩：退水輸入值錯誤！", null, 400));
                            base.Response.End();
                        }
                    }
                    if (string.IsNullOrEmpty(str5))
                    {
                        base.Response.Write(base.ShowDialogBox("⑥合彩：單注最大限額不能為空！", null, 400));
                        base.Response.End();
                    }
                    else if (!Utils.IsInteger(str5))
                    {
                        base.Response.Write(base.ShowDialogBox("⑥合彩：單注最大限額不能只能為整數！", null, 400));
                        base.Response.End();
                    }
                    if (string.IsNullOrEmpty(str6))
                    {
                        base.Response.Write(base.ShowDialogBox("⑥合彩：單期最大限額不能為空！", null, 400));
                        base.Response.End();
                    }
                    else if (!Utils.IsInteger(str6))
                    {
                        base.Response.Write(base.ShowDialogBox("⑥合彩：單期最大限額只能為整數！", null, 400));
                        base.Response.End();
                    }
                }
            }
        }
    }
}

