namespace Agent.Web.Account
{
    using Agent.Web.WebBase;
    using LotterySystem.Common;
    using LotterySystem.Model;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class zd_edit : MemberPageBase
    {
        protected string d_a_state = "0";
        protected string d_allow_view_report = "";
        protected double d_downRate_kc;
        protected double d_downRate_six;
        protected string d_kc_allow_maxrate = "0";
        protected string d_kc_allow_sale = "1";
        protected string d_kc_credit = "0";
        protected bool d_kc_ExistsBet;
        protected string d_kc_iscash = "0";
        protected string d_kc_kind = "0";
        protected string d_kc_low_maxrate = "0";
        protected bool d_kc_OpenPhase;
        protected string d_kc_rate = "0";
        protected string d_kc_rate_owner = "0";
        protected string d_kc_usable_credit = "0";
        protected double d_maxRate_kc;
        protected double d_maxRate_six;
        protected string d_six_allow_maxrate = "0";
        protected string d_six_allow_sale = "1";
        protected string d_six_credit = "0";
        protected bool d_six_ExistsBet;
        protected string d_six_iscash = "0";
        protected string d_six_kind = "0";
        protected string d_six_low_maxrate = "0";
        protected bool d_six_OpenPhase;
        protected string d_six_rate = "0";
        protected string d_six_rate_owner = "0";
        protected string d_six_usable_credit = "0";
        protected string d_u_name = "";
        protected string d_u_nicker = "";
        protected string d_u_type = "";
        protected string d_up_kc_allow_sale = "1";
        protected string d_up_kc_usable_credit = "0";
        protected string d_up_six_allow_sale = "1";
        protected string d_up_six_usable_credit = "0";
        protected string d_up_u_name = "";
        protected string d_up_u_nicker = "";
        protected string d_up_u_type = "";
        protected bool is_locked;
        protected DataTable lotteryDT;
        protected string lottrty_kc = "";
        protected string lottrty_six = "";
        protected string r_u_id = "";
        protected string tabState_kc = "";
        protected string tabState_six = "";
        private agent_userinfo_session userModel;

        private void AddUser()
        {
            cz_rate_kc rateKCByUserName = CallBLL.cz_rate_kc_bll.GetRateKCByUserName(this.d_u_name);
            base.En_User_Lock(rateKCByUserName.get_fgs_name());
            this.InitData();
            string str = LSRequest.qq("userState");
            string str2 = LSRequest.qq("userPassword");
            string str3 = LSRequest.qq("userNicker");
            string str4 = LSRequest.qq("unlock");
            string s = LSRequest.qq("userCredit_six");
            if (this.d_six_iscash.Equals("1"))
            {
                s = this.d_six_credit;
            }
            string str6 = LSRequest.qq("userRate_six");
            string str7 = LSRequest.qq("userAllowSale_six");
            string str8 = LSRequest.qq("userKind_six");
            string str9 = LSRequest.qq("allowmaxrate_six");
            string str10 = LSRequest.qq("lowmaxrate_six");
            string str11 = LSRequest.qq("userCredit_kc");
            if (this.d_kc_iscash.Equals("1"))
            {
                str11 = this.d_kc_credit;
            }
            string str12 = LSRequest.qq("userRate_kc");
            string str13 = LSRequest.qq("userAllowSale_kc");
            string str14 = LSRequest.qq("userKind_kc");
            string str15 = LSRequest.qq("allowmaxrate_kc");
            string str16 = LSRequest.qq("lowmaxrate_kc");
            string message = "";
            if (!base.ValidParamByUserEdit("zd", ref message, null, this.lottrty_six, this.lottrty_kc))
            {
                base.Response.Write(base.ShowDialogBox(message, null, 400));
                base.Response.End();
            }
            if (!string.IsNullOrEmpty(str2.Trim()) && !Regexlib.IsValidPassword(str2.Trim(), base.get_GetPasswordLU()))
            {
                if (base.get_GetPasswordLU().Equals("1"))
                {
                    base.Response.Write(base.ShowDialogBox("密碼要8-20位,且必需包含大寫字母、小寫字母和数字！", null, 400));
                }
                else
                {
                    base.Response.Write(base.ShowDialogBox("密碼要8-20位,且必需包含字母、和数字！", null, 400));
                }
                base.Response.End();
            }
            if ((!str.Equals("0") && !str.Equals("1")) && !str.Equals("2"))
            {
                base.Response.End();
            }
            if (!string.IsNullOrEmpty(this.lottrty_six))
            {
                if ((!str8.ToUpper().Equals("A") && !str8.ToUpper().Equals("B")) && (!str8.ToUpper().Equals("C") && !str8.ToUpper().Equals("0")))
                {
                    base.Response.End();
                }
                if (!str7.ToUpper().Equals("0") && !str7.ToUpper().Equals("1"))
                {
                    base.Response.Write("<script>alert(\"(⑥合彩)補貨功能选择错误!!\");</script>");
                    base.Response.End();
                }
                if ((this.d_up_six_allow_sale == "0") && (str7 == "1"))
                {
                    base.Response.Write("<script>alert(\"(⑥合彩)補貨功能选择错误!!\");</script>");
                    base.Response.End();
                }
            }
            if (!string.IsNullOrEmpty(this.lottrty_kc))
            {
                if ((!str14.ToUpper().Equals("A") && !str14.ToUpper().Equals("B")) && (!str14.ToUpper().Equals("C") && !str14.ToUpper().Equals("0")))
                {
                    base.Response.End();
                }
                if (!str13.ToUpper().Equals("0") && !str13.ToUpper().Equals("1"))
                {
                    base.Response.Write("<script>alert(\"(快彩)補貨功能选择错误!!\");</script>");
                    base.Response.End();
                }
                if ((this.d_up_kc_allow_sale == "0") && (str13 == "1"))
                {
                    base.Response.Write("<script>alert(\"(快彩)補貨功能选择错误!!\");</script>");
                    base.Response.End();
                }
            }
            if ((double.Parse(s) - double.Parse(this.d_six_credit)) > double.Parse(this.d_up_six_usable_credit))
            {
                base.Un_User_Lock(rateKCByUserName.get_fgs_name());
                base.Response.Write(base.ShowDialogBox("⑥合彩: 設定增加‘信用額度’超過上级可以用餘額！", null, 400));
                base.Response.End();
            }
            if (double.Parse(str6) > this.d_maxRate_six)
            {
                base.Un_User_Lock(rateKCByUserName.get_fgs_name());
                base.Response.Write(base.ShowDialogBox("⑥合彩: 分公司占成 數不正确,请核实后重新输入！", null, 400));
                base.Response.End();
            }
            double num = double.Parse(s) - double.Parse(this.d_six_credit);
            if (double.Parse(s) < (double.Parse(this.d_six_credit) - Convert.ToDouble(this.d_six_usable_credit)))
            {
                base.Un_User_Lock(rateKCByUserName.get_fgs_name());
                base.Response.Write(base.ShowDialogBox("⑥合彩: 設定減少‘信用額度’超過可‘回收’餘額！", null, 400));
                base.Response.End();
            }
            if (str9.Equals("1"))
            {
                if (string.IsNullOrEmpty(str10))
                {
                    base.Un_User_Lock(rateKCByUserName.get_fgs_name());
                    base.Response.Write(base.ShowDialogBox("⑥合彩:‘占成上限’不可為空，請修改！", null, 400));
                    base.Response.End();
                }
                try
                {
                    int.Parse(str10);
                }
                catch
                {
                    base.Un_User_Lock(rateKCByUserName.get_fgs_name());
                    base.Response.Write(base.ShowDialogBox("⑥合彩: ‘占成上限’只能為數字，請重新設定！", null, 400));
                    base.Response.End();
                }
                if (Convert.ToInt32(str10) > 100)
                {
                    base.Un_User_Lock(rateKCByUserName.get_fgs_name());
                    base.Response.Write(base.ShowDialogBox("⑥合彩: ‘占成上限’不可高於 100%，請重新設定！", null, 400));
                    base.Response.End();
                }
                else if (Convert.ToInt32(str10) < 0)
                {
                    base.Un_User_Lock(rateKCByUserName.get_fgs_name());
                    base.Response.Write(base.ShowDialogBox("⑥合彩: ‘占成上限’不可低於等於 0%，請重新設定！", null, 400));
                    base.Response.End();
                }
                if (double.Parse(str10) < this.d_downRate_six)
                {
                    base.Un_User_Lock(rateKCByUserName.get_fgs_name());
                    base.Response.Write(base.ShowDialogBox(string.Format("⑥合彩: ‘占成上限’不可低於 {0}%，請重新設定！", this.d_downRate_six), null, 400));
                    base.Response.End();
                }
            }
            else
            {
                str10 = "0";
            }
            if ((double.Parse(str11) - double.Parse(this.d_kc_credit)) > double.Parse(this.d_up_kc_usable_credit))
            {
                base.Un_User_Lock(rateKCByUserName.get_fgs_name());
                base.Response.Write(base.ShowDialogBox("快彩: 設定增加‘信用額度’超過上级可以用餘額！", null, 400));
                base.Response.End();
            }
            if (double.Parse(str12) > this.d_maxRate_kc)
            {
                base.Un_User_Lock(rateKCByUserName.get_fgs_name());
                base.Response.Write(base.ShowDialogBox("快彩: 分公司占成 數不正确,请核实后重新输入！", null, 400));
                base.Response.End();
            }
            double num2 = double.Parse(str11) - double.Parse(this.d_kc_credit);
            if (double.Parse(str11) < (double.Parse(this.d_kc_credit) - Convert.ToDouble(this.d_kc_usable_credit)))
            {
                base.Un_User_Lock(rateKCByUserName.get_fgs_name());
                base.Response.Write(base.ShowDialogBox("快彩: 設定減少‘信用額度’超過可‘回收’餘額！", null, 400));
                base.Response.End();
            }
            if (str15.Equals("1"))
            {
                if (string.IsNullOrEmpty(str16))
                {
                    base.Un_User_Lock(rateKCByUserName.get_fgs_name());
                    base.Response.Write(base.ShowDialogBox("快彩:‘占成上限’不可為空，請修改！", null, 400));
                    base.Response.End();
                }
                try
                {
                    int.Parse(str16);
                }
                catch
                {
                    base.Un_User_Lock(rateKCByUserName.get_fgs_name());
                    base.Response.Write(base.ShowDialogBox("快彩: ‘占成上限’只能為數字，請重新設定！", null, 400));
                    base.Response.End();
                }
                if (Convert.ToInt32(str16) > 100)
                {
                    base.Un_User_Lock(rateKCByUserName.get_fgs_name());
                    base.Response.Write(base.ShowDialogBox("快彩: ‘占成上限’不可高於 100%，請重新設定！", null, 400));
                    base.Response.End();
                }
                else if (Convert.ToInt32(str16) < 0)
                {
                    base.Un_User_Lock(rateKCByUserName.get_fgs_name());
                    base.Response.Write(base.ShowDialogBox("快彩: ‘占成上限’不可低於等於 0%，請重新設定！", null, 400));
                    base.Response.End();
                }
                if (double.Parse(str16) < this.d_downRate_kc)
                {
                    base.Un_User_Lock(rateKCByUserName.get_fgs_name());
                    base.Response.Write(base.ShowDialogBox(string.Format("快彩: ‘占成上限’不可低於 {0}%，請重新設定！", this.d_downRate_kc), null, 400));
                    base.Response.End();
                }
            }
            else
            {
                str16 = "0";
            }
            int num3 = 0;
            decimal num4 = 0M;
            decimal num5 = 0M;
            if (!string.IsNullOrEmpty(this.lottrty_six))
            {
                num3 = Convert.ToInt32(str6);
                num4 = Convert.ToDecimal(num);
                num5 = Convert.ToDecimal(num);
            }
            int num6 = 0;
            decimal num7 = 0M;
            decimal num8 = 0M;
            if (!string.IsNullOrEmpty(this.lottrty_kc))
            {
                num6 = Convert.ToInt32(str12);
                num7 = Convert.ToDecimal(num2);
                num8 = Convert.ToDecimal(num2);
            }
            cz_users _users = new cz_users();
            _users.set_u_id(this.r_u_id.ToUpper());
            _users.set_u_name(this.d_u_name);
            if (!string.IsNullOrEmpty(str4) && (str4 == "1"))
            {
                _users.set_retry_times(0);
            }
            if (!string.IsNullOrEmpty(str2))
            {
                string ramSalt = Utils.GetRamSalt(6);
                _users.set_u_psw(DESEncrypt.EncryptString(str2, ramSalt));
                _users.set_salt(ramSalt);
            }
            _users.set_u_nicker(str3);
            _users.set_a_state(new int?(Convert.ToInt32(str)));
            _users.set_u_type(this.d_u_type);
            _users.set_six_rate(new int?(num3));
            _users.set_six_credit(new decimal?(num4));
            _users.set_six_usable_credit(new decimal?(num5));
            _users.set_allow_sale(new int?(Convert.ToInt32(str7)));
            if (str9.Equals("1"))
            {
                _users.set_six_allow_maxrate(1);
                _users.set_six_low_maxrate(new int?(Convert.ToInt32(str10)));
            }
            else
            {
                _users.set_six_allow_maxrate(0);
                _users.set_six_low_maxrate(0);
            }
            _users.set_kc_rate(new int?(num6));
            _users.set_kc_credit(new decimal?(num7));
            _users.set_kc_usable_credit(new decimal?(num8));
            _users.set_kc_allow_sale(new int?(Convert.ToInt32(str13)));
            if (str15.Equals("1"))
            {
                _users.set_kc_allow_maxrate(1);
                _users.set_kc_low_maxrate(new int?(Convert.ToInt32(str16)));
            }
            else
            {
                _users.set_kc_allow_maxrate(0);
                _users.set_kc_low_maxrate(0);
            }
            bool flag2 = true;
            bool flag3 = true;
            bool flag4 = false;
            bool flag5 = false;
            if (str7.Equals("0") && this.d_six_allow_sale.Equals("1"))
            {
                flag4 = true;
            }
            if (str13.Equals("0") && this.d_kc_allow_sale.Equals("1"))
            {
                flag5 = true;
            }
            if (this.d_six_ExistsBet && this.d_six_OpenPhase)
            {
                flag2 = false;
                _users.set_allow_sale(new int?(int.Parse(this.d_six_allow_sale)));
                flag4 = false;
            }
            if (this.d_kc_ExistsBet && this.d_kc_OpenPhase)
            {
                flag3 = false;
                _users.set_kc_allow_sale(new int?(int.Parse(this.d_kc_allow_sale)));
                flag5 = false;
            }
            if (string.IsNullOrEmpty(this.lottrty_six))
            {
                flag2 = false;
            }
            if (string.IsNullOrEmpty(this.lottrty_kc))
            {
                flag3 = false;
            }
            DataTable userInfoTableByUID = CallBLL.cz_users_bll.GetUserInfoTableByUID(this.r_u_id);
            if (CallBLL.cz_users_bll.UpdateUserInfo(_users, flag2, flag3, flag4, flag5, this.d_up_u_type, this.d_up_u_name))
            {
                if (!this.d_kc_rate.Equals(_users.get_kc_rate().ToString()))
                {
                    FileCacheHelper.UpdateRateFile_kc();
                }
                if (!this.d_six_rate.Equals(_users.get_six_rate().ToString()))
                {
                    FileCacheHelper.UpdateRateFile_six();
                }
                base.user_edit_agent_log(userInfoTableByUID, CallBLL.cz_users_bll.GetUserInfoTableByUID(this.r_u_id), _users.get_u_name(), _users.get_u_type());
                base.Un_User_Lock(rateKCByUserName.get_fgs_name());
                base.Response.Write(base.ShowDialogBox("修改總代理成功！", base.UserReturnBackUrl, 0));
                base.Response.End();
            }
            else
            {
                base.Un_User_Lock(rateKCByUserName.get_fgs_name());
                base.Response.Write(base.ShowDialogBox("修改總代理失敗！", base.UserReturnBackUrl, 400));
                base.Response.End();
            }
        }

        private void InitData()
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            Dictionary<string, string> dictionary2 = new Dictionary<string, string>();
            DataTable editGD = CallBLL.cz_rate_six_bll.GetEditGD(this.r_u_id, ref dictionary);
            DataTable table2 = CallBLL.cz_rate_kc_bll.GetEditGD(this.r_u_id, ref dictionary2);
            if ((editGD == null) || (table2 == null))
            {
                base.Response.Redirect("../MessagePage.aspx?code=u100038&url=&issuccess=1&isback=0");
                base.Response.End();
            }
            this.d_u_name = editGD.Rows[0]["u_name"].ToString();
            this.d_u_type = editGD.Rows[0]["u_type"].ToString();
            this.d_u_nicker = editGD.Rows[0]["u_nicker"].ToString();
            this.d_a_state = editGD.Rows[0]["a_state"].ToString();
            this.d_allow_view_report = editGD.Rows[0]["allow_view_report"].ToString();
            this.d_six_credit = string.Format("{0:F0}", Convert.ToDouble(editGD.Rows[0]["six_credit"].ToString()));
            this.d_six_usable_credit = string.Format("{0:F0}", Convert.ToDouble(editGD.Rows[0]["six_usable_credit"].ToString()));
            this.d_six_rate = editGD.Rows[0]["six_rate"].ToString();
            this.d_six_allow_sale = editGD.Rows[0]["allow_sale"].ToString();
            this.d_six_allow_maxrate = editGD.Rows[0]["six_allow_maxrate"].ToString();
            this.d_six_low_maxrate = editGD.Rows[0]["six_low_maxrate"].ToString();
            if (string.IsNullOrEmpty(editGD.Rows[0]["six_kind"]))
            {
                this.d_six_kind = "0";
            }
            else
            {
                this.d_six_kind = editGD.Rows[0]["six_kind"].ToString();
            }
            if (string.IsNullOrEmpty(editGD.Rows[0]["six_iscash"]))
            {
                this.d_six_iscash = "0";
            }
            else
            {
                this.d_six_iscash = editGD.Rows[0]["six_iscash"].ToString();
            }
            this.d_six_ExistsBet = CallBLL.cz_bet_six_bll.ExistsBetByUserName(this.d_u_type, this.d_u_name);
            this.d_six_OpenPhase = CallBLL.cz_phase_six_bll.IsOpenPhase();
            this.d_up_u_name = dictionary["u_name"];
            this.d_up_u_nicker = dictionary["u_nicker"];
            this.d_up_u_type = dictionary["u_type"];
            this.d_up_six_allow_sale = dictionary["six_allow_sale"];
            this.d_up_six_usable_credit = dictionary["six_usable_credit"];
            this.d_maxRate_six = Convert.ToDouble(dictionary["six_maxRate"].ToString());
            this.d_downRate_six = Convert.ToDouble(dictionary["six_downRate"].ToString());
            this.d_kc_credit = string.Format("{0:F0}", Convert.ToDouble(table2.Rows[0]["kc_credit"].ToString()));
            this.d_kc_usable_credit = string.Format("{0:F0}", Convert.ToDouble(table2.Rows[0]["kc_usable_credit"].ToString()));
            this.d_kc_rate = table2.Rows[0]["kc_rate"].ToString();
            this.d_kc_allow_sale = table2.Rows[0]["kc_allow_sale"].ToString();
            this.d_kc_allow_maxrate = table2.Rows[0]["kc_allow_maxrate"].ToString();
            this.d_kc_low_maxrate = table2.Rows[0]["kc_low_maxrate"].ToString();
            if (string.IsNullOrEmpty(table2.Rows[0]["kc_kind"]))
            {
                this.d_kc_kind = "0";
            }
            else
            {
                this.d_kc_kind = table2.Rows[0]["kc_kind"].ToString();
            }
            if (string.IsNullOrEmpty(table2.Rows[0]["kc_iscash"]))
            {
                this.d_kc_iscash = "0";
            }
            else
            {
                this.d_kc_iscash = table2.Rows[0]["kc_iscash"].ToString();
            }
            this.d_kc_ExistsBet = CallBLL.cz_bet_kc_bll.ExistsBetByUserName(this.d_u_type, this.d_u_name);
            this.d_kc_OpenPhase = base.IsOpenPhase_KC();
            this.d_up_kc_allow_sale = dictionary2["kc_allow_sale"];
            this.d_up_kc_usable_credit = dictionary2["kc_usable_credit"];
            this.d_maxRate_kc = Convert.ToDouble(dictionary2["kc_maxRate"].ToString());
            this.d_downRate_kc = Convert.ToDouble(dictionary2["kc_downRate"].ToString());
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string str = this.Session["user_name"].ToString();
            this.userModel = this.Session[str + "lottery_session_user_info"] as agent_userinfo_session;
            if ((!this.userModel.get_u_type().Trim().Equals("zj") && !this.userModel.get_u_type().Trim().Equals("fgs")) && !this.userModel.get_u_type().Trim().Equals("gd"))
            {
                base.Response.Redirect("../Quit.aspx", true);
                base.Response.End();
            }
            base.Permission_Aspx_ZJ(this.userModel, "po_2_1");
            base.Permission_Aspx_DL(this.userModel, "po_6_1");
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
            this.r_u_id = LSRequest.qq("uid");
            if (!Utils.IsGuid(this.r_u_id))
            {
                base.Response.Redirect("../MessagePage.aspx?code=u100079&url=&issuccess=1&isback=0&isopen=1");
                base.Response.End();
            }
            cz_users userInfoByUID = CallBLL.cz_users_bll.GetUserInfoByUID(this.r_u_id, "zd");
            if (userInfoByUID == null)
            {
                base.Response.Redirect("../MessagePage.aspx?code=u100079&url=&issuccess=1&isback=0&isopen=1");
                base.Response.End();
            }
            this.is_locked = base.is_locked_user(userInfoByUID.get_retry_times().ToString());
            this.InitData();
            if (this.d_u_name.Equals(this.userModel.get_u_name()))
            {
                base.Response.Redirect("../MessagePage.aspx?code=u100057&url=&issuccess=1&isback=0&isopen=1");
                base.Response.End();
            }
            if (!base.IsUpperLowerLevels(this.d_u_name, this.userModel.get_u_type(), this.userModel.get_u_name()))
            {
                base.Response.Redirect("../MessagePage.aspx?code=u100057&url=&issuccess=1&isback=0&isopen=1");
                base.Response.End();
            }
            if (LSRequest.qq("hdnadd").Equals("hdnadd") && !string.IsNullOrEmpty(this.d_u_name))
            {
                this.AddUser();
            }
        }
    }
}

