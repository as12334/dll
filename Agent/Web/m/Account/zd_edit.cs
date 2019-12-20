namespace Agent.Web.m.Account
{
    using Agent.Web.WebBase;
    using LotterySystem.Common;
    using LotterySystem.Model;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Web.UI.HtmlControls;

    public class zd_edit : MemberPageBase_Mobile
    {
        private string d_a_state = "0";
        private string d_allow_view_report = "";
        private double d_downRate_kc;
        private double d_downRate_six;
        private string d_kc_allow_maxrate = "0";
        private string d_kc_allow_sale = "1";
        private string d_kc_credit = "0";
        private bool d_kc_ExistsBet;
        private string d_kc_iscash = "0";
        private string d_kc_kind = "0";
        private string d_kc_low_maxrate = "0";
        private bool d_kc_OpenPhase;
        private string d_kc_rate = "0";
        private string d_kc_usable_credit = "0";
        private double d_maxRate_kc;
        private double d_maxRate_six;
        private string d_six_allow_maxrate = "0";
        private string d_six_allow_sale = "1";
        private string d_six_credit = "0";
        private bool d_six_ExistsBet;
        private string d_six_iscash = "0";
        private string d_six_kind = "0";
        private string d_six_low_maxrate = "0";
        private bool d_six_OpenPhase;
        private string d_six_rate = "0";
        private string d_six_usable_credit = "0";
        private string d_u_name = "";
        private string d_u_nicker = "";
        private string d_u_type = "";
        private string d_up_kc_allow_sale = "1";
        private string d_up_kc_usable_credit = "0";
        private string d_up_six_allow_sale = "1";
        private string d_up_six_usable_credit = "0";
        private string d_up_u_name = "";
        private string d_up_u_nicker = "";
        private string d_up_u_type = "";
        protected HtmlForm form1;
        protected DataTable lotteryDT;
        protected string lottrty_kc = "";
        protected string lottrty_six = "";
        private string r_u_id = "";

        private void AddUser()
        {
            string str = LSRequest.qq("userState");
            string str2 = LSRequest.qq("password");
            string str3 = LSRequest.qq("nicker");
            string str4 = LSRequest.qq("isLocked");
            string s = LSRequest.qq("userCreditSix");
            if (this.d_six_iscash.Equals("1"))
            {
                s = this.d_six_credit;
            }
            string str6 = LSRequest.qq("userRateSix");
            string str7 = LSRequest.qq("userAllowSaleSix");
            string str8 = LSRequest.qq("userKindSix");
            string str9 = LSRequest.qq("allowMaxRateSix");
            string str10 = LSRequest.qq("downRateSix");
            string str11 = LSRequest.qq("userCreditKc");
            if (this.d_kc_iscash.Equals("1"))
            {
                str11 = this.d_kc_credit;
            }
            string str12 = LSRequest.qq("userRateKc");
            string str13 = LSRequest.qq("userAllowSaleKc");
            string str14 = LSRequest.qq("userKindKc");
            string str15 = LSRequest.qq("allowMaxRateKc");
            string str16 = LSRequest.qq("downRateKc");
            string message = "";
            if (!base.ValidParamByUserEditPhone("zd", ref message, null, this.lottrty_six, this.lottrty_kc))
            {
                base.noRightOptMsg(message);
            }
            if (!string.IsNullOrEmpty(str2.Trim()) && !Regexlib.IsValidPassword(str2.Trim(), base.get_GetPasswordLU()))
            {
                if (base.get_GetPasswordLU().Equals("1"))
                {
                    base.noRightOptMsg("密碼要8-20位,且必需包含大寫字母、小寫字母和数字！");
                }
                else
                {
                    base.noRightOptMsg("密碼要8-20位,且必需包含字母、和数字！");
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
                    base.Response.End();
                }
            }
            cz_rate_kc rateKCByUserName = CallBLL.cz_rate_kc_bll.GetRateKCByUserName(this.d_u_name);
            base.En_User_Lock(rateKCByUserName.get_fgs_name());
            double num = 0.0;
            if (!string.IsNullOrEmpty(this.lottrty_six))
            {
                if ((double.Parse(s) - double.Parse(this.d_six_credit)) > double.Parse(this.d_up_six_usable_credit))
                {
                    base.Un_User_Lock(rateKCByUserName.get_fgs_name());
                    base.noRightOptMsg("⑥合彩: 設定增加‘信用額度’超過上级可以用餘額！");
                }
                if (double.Parse(str6) > this.d_maxRate_six)
                {
                    base.Un_User_Lock(rateKCByUserName.get_fgs_name());
                    base.noRightOptMsg("⑥合彩: 分公司占成 數不正确,请核实后重新输入！");
                }
                num = double.Parse(s) - double.Parse(this.d_six_credit);
                if (double.Parse(s) < (double.Parse(this.d_six_credit) - Convert.ToDouble(this.d_six_usable_credit)))
                {
                    base.Un_User_Lock(rateKCByUserName.get_fgs_name());
                    base.noRightOptMsg("⑥合彩: 設定減少‘信用額度’超過可‘回收’餘額！");
                }
                if (str9.Equals("1"))
                {
                    if (string.IsNullOrEmpty(str10))
                    {
                        base.Un_User_Lock(rateKCByUserName.get_fgs_name());
                        base.noRightOptMsg("⑥合彩:‘占成上限’不可為空，請修改！");
                    }
                    try
                    {
                        int.Parse(str10);
                    }
                    catch
                    {
                        base.Un_User_Lock(rateKCByUserName.get_fgs_name());
                        base.noRightOptMsg("⑥合彩: ‘占成上限’只能為數字，請重新設定！");
                    }
                    if (Convert.ToInt32(str10) > 100)
                    {
                        base.Un_User_Lock(rateKCByUserName.get_fgs_name());
                        base.noRightOptMsg("⑥合彩: ‘占成上限’不可高於 100%，請重新設定！");
                    }
                    else if (Convert.ToInt32(str10) < 0)
                    {
                        base.Un_User_Lock(rateKCByUserName.get_fgs_name());
                        base.noRightOptMsg("⑥合彩: ‘占成上限’不可低於等於 0%，請重新設定！");
                    }
                    if (double.Parse(str10) < this.d_downRate_six)
                    {
                        base.Un_User_Lock(rateKCByUserName.get_fgs_name());
                        base.noRightOptMsg(string.Format("⑥合彩: ‘占成上限’不可低於 {0}%，請重新設定！", this.d_downRate_six));
                    }
                }
                else
                {
                    str10 = "0";
                }
            }
            double num2 = 0.0;
            if (!string.IsNullOrEmpty(this.lottrty_kc))
            {
                if ((double.Parse(str11) - double.Parse(this.d_kc_credit)) > double.Parse(this.d_up_kc_usable_credit))
                {
                    base.Un_User_Lock(rateKCByUserName.get_fgs_name());
                    base.noRightOptMsg("快彩: 設定增加‘信用額度’超過上级可以用餘額！");
                }
                if (double.Parse(str12) > this.d_maxRate_kc)
                {
                    base.Un_User_Lock(rateKCByUserName.get_fgs_name());
                    base.noRightOptMsg("快彩: 分公司占成 數不正确,请核实后重新输入！");
                }
                num2 = double.Parse(str11) - double.Parse(this.d_kc_credit);
                if (double.Parse(str11) < (double.Parse(this.d_kc_credit) - Convert.ToDouble(this.d_kc_usable_credit)))
                {
                    base.Un_User_Lock(rateKCByUserName.get_fgs_name());
                    base.noRightOptMsg("快彩: 設定減少‘信用額度’超過可‘回收’餘額！");
                }
                if (str15.Equals("1"))
                {
                    if (string.IsNullOrEmpty(str16))
                    {
                        base.Un_User_Lock(rateKCByUserName.get_fgs_name());
                        base.noRightOptMsg("快彩:‘占成上限’不可為空，請修改！");
                    }
                    try
                    {
                        int.Parse(str16);
                    }
                    catch
                    {
                        base.Un_User_Lock(rateKCByUserName.get_fgs_name());
                        base.noRightOptMsg("快彩: ‘占成上限’只能為數字，請重新設定！");
                    }
                    if (Convert.ToInt32(str16) > 100)
                    {
                        base.Un_User_Lock(rateKCByUserName.get_fgs_name());
                        base.noRightOptMsg("快彩: ‘占成上限’不可高於 100%，請重新設定！");
                    }
                    else if (Convert.ToInt32(str16) < 0)
                    {
                        base.Un_User_Lock(rateKCByUserName.get_fgs_name());
                        base.noRightOptMsg("快彩: ‘占成上限’不可低於等於 0%，請重新設定！");
                    }
                    if (double.Parse(str16) < this.d_downRate_kc)
                    {
                        base.Un_User_Lock(rateKCByUserName.get_fgs_name());
                        base.noRightOptMsg(string.Format("快彩: ‘占成上限’不可低於 {0}%，請重新設定！", this.d_downRate_kc));
                    }
                }
                else
                {
                    str16 = "0";
                }
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
                base.successOptMsg("已經成功修改總代！");
            }
            else
            {
                base.Un_User_Lock(rateKCByUserName.get_fgs_name());
                base.noRightOptMsg("修改總代失敗！");
            }
        }

        private void getMemberDetail(ref string strResult)
        {
            bool flag2;
            bool flag3;
            base.checkLoginByHandler(0);
            this.r_u_id = LSRequest.qq("uid");
            string str = LSRequest.qq("memberId");
            string str2 = LSRequest.qq("submitType");
            if (str != "zd")
            {
                base.Response.End();
            }
            ReturnResult result = new ReturnResult();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            new List<object>();
            string str3 = this.Session["user_name"].ToString();
            agent_userinfo_session _session = this.Session[str3 + "lottery_session_user_info"] as agent_userinfo_session;
            if (!Utils.IsGuid(this.r_u_id))
            {
                base.Response.End();
            }
            if ((!_session.get_u_type().Trim().Equals("zj") && !_session.get_u_type().Trim().Equals("fgs")) && !_session.get_u_type().Trim().Equals("gd"))
            {
                base.noRightOptMsg();
            }
            base.checkCloneRight();
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
            cz_users userInfoByUID = CallBLL.cz_users_bll.GetUserInfoByUID(this.r_u_id, str);
            if (userInfoByUID == null)
            {
                base.Response.End();
            }
            bool flag = base.is_locked_user(userInfoByUID.get_retry_times().ToString());
            Dictionary<string, string> dictionary2 = new Dictionary<string, string>();
            Dictionary<string, string> dictionary3 = new Dictionary<string, string>();
            DataTable editGD = CallBLL.cz_rate_six_bll.GetEditGD(this.r_u_id, ref dictionary2);
            DataTable table3 = CallBLL.cz_rate_kc_bll.GetEditGD(this.r_u_id, ref dictionary3);
            if ((editGD == null) || (table3 == null))
            {
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
            this.d_up_u_name = dictionary2["u_name"];
            this.d_up_u_nicker = dictionary2["u_nicker"];
            this.d_up_u_type = dictionary2["u_type"];
            this.d_up_six_allow_sale = dictionary2["six_allow_sale"];
            this.d_up_six_usable_credit = dictionary2["six_usable_credit"];
            this.d_maxRate_six = Convert.ToDouble(dictionary2["six_maxRate"].ToString());
            this.d_downRate_six = Convert.ToDouble(dictionary2["six_downRate"].ToString());
            this.d_kc_credit = string.Format("{0:F0}", Convert.ToDouble(table3.Rows[0]["kc_credit"].ToString()));
            this.d_kc_usable_credit = string.Format("{0:F0}", Convert.ToDouble(table3.Rows[0]["kc_usable_credit"].ToString()));
            this.d_kc_rate = table3.Rows[0]["kc_rate"].ToString();
            this.d_kc_allow_sale = table3.Rows[0]["kc_allow_sale"].ToString();
            this.d_kc_allow_maxrate = table3.Rows[0]["kc_allow_maxrate"].ToString();
            this.d_kc_low_maxrate = table3.Rows[0]["kc_low_maxrate"].ToString();
            if (string.IsNullOrEmpty(table3.Rows[0]["kc_kind"]))
            {
                this.d_kc_kind = "0";
            }
            else
            {
                this.d_kc_kind = table3.Rows[0]["kc_kind"].ToString();
            }
            if (string.IsNullOrEmpty(table3.Rows[0]["kc_iscash"]))
            {
                this.d_kc_iscash = "0";
            }
            else
            {
                this.d_kc_iscash = table3.Rows[0]["kc_iscash"].ToString();
            }
            this.d_kc_ExistsBet = CallBLL.cz_bet_kc_bll.ExistsBetByUserName(this.d_u_type, this.d_u_name);
            this.d_kc_OpenPhase = base.IsOpenPhase_KC();
            this.d_up_kc_allow_sale = dictionary3["kc_allow_sale"];
            this.d_up_kc_usable_credit = dictionary3["kc_usable_credit"];
            this.d_maxRate_kc = Convert.ToDouble(dictionary3["kc_maxRate"].ToString());
            this.d_downRate_kc = Convert.ToDouble(dictionary3["kc_downRate"].ToString());
            if (this.d_u_name.Equals(_session.get_u_name()))
            {
                base.noRightOptMsg();
            }
            if (!base.IsUpperLowerLevels(this.d_u_name, _session.get_u_type(), _session.get_u_name()))
            {
                base.noRightOptMsg();
            }
            base.OpenLotteryMaster(out flag2, out flag3);
            if (str2 == "view")
            {
                if (flag2 && flag3)
                {
                    int num2 = (this.d_six_ExistsBet && this.d_six_OpenPhase) ? 0 : 1;
                    int num3 = (this.d_kc_ExistsBet && this.d_kc_OpenPhase) ? 0 : 1;
                    Dictionary<string, object> dictionary4 = new Dictionary<string, object>();
                    dictionary4.Add("disabledKc", num3.ToString());
                    dictionary4.Add("disabledSix", num2.ToString());
                    dictionary4.Add("supName", this.d_up_u_name);
                    dictionary4.Add("supNameNicker", this.d_up_u_nicker);
                    dictionary4.Add("name", this.d_u_name);
                    dictionary4.Add("isLocked", flag ? "1" : "0");
                    dictionary4.Add("userState", this.d_a_state);
                    dictionary4.Add("nicker", this.d_u_nicker);
                    dictionary4.Add("userCreditSix", this.d_six_credit);
                    dictionary4.Add("recoverCreditSix", this.d_six_usable_credit);
                    dictionary4.Add("supCreditSix", string.Format("{0:F0}", Convert.ToDouble(this.d_up_six_usable_credit)));
                    dictionary4.Add("userRateSix", this.d_six_rate);
                    dictionary4.Add("maxRateSix", this.d_maxRate_six.ToString());
                    dictionary4.Add("allowMaxRateSix", this.d_six_allow_maxrate);
                    dictionary4.Add("downRateSix", this.d_downRate_six.ToString());
                    dictionary4.Add("userAllowSaleSix", this.d_six_allow_sale);
                    dictionary4.Add("userKindSix", this.d_six_kind);
                    dictionary4.Add("isCashSix", this.d_six_iscash);
                    dictionary4.Add("userCreditKc", this.d_kc_credit);
                    dictionary4.Add("recoverCreditKc", this.d_kc_usable_credit);
                    dictionary4.Add("supCreditKc", string.Format("{0:F0}", Convert.ToDouble(this.d_up_kc_usable_credit)));
                    dictionary4.Add("userRateKc", this.d_kc_rate);
                    dictionary4.Add("maxRateKc", this.d_maxRate_kc.ToString());
                    dictionary4.Add("allowMaxRateKc", this.d_kc_allow_maxrate);
                    dictionary4.Add("downRateKc", this.d_downRate_kc.ToString());
                    dictionary4.Add("userAllowSaleKc", this.d_kc_allow_sale);
                    dictionary4.Add("userKindKc", this.d_kc_kind);
                    dictionary4.Add("isCashKc", this.d_kc_iscash);
                    dictionary4.Add("date", userInfoByUID.get_add_date());
                    dictionary = dictionary4;
                }
                else if (flag2)
                {
                    int num4 = (this.d_six_ExistsBet && this.d_six_OpenPhase) ? 0 : 1;
                    Dictionary<string, object> dictionary5 = new Dictionary<string, object>();
                    dictionary5.Add("disabledSix", num4.ToString());
                    dictionary5.Add("supName", this.d_up_u_name);
                    dictionary5.Add("supNameNicker", this.d_up_u_nicker);
                    dictionary5.Add("name", this.d_u_name);
                    dictionary5.Add("isLocked", flag ? "1" : "0");
                    dictionary5.Add("userState", this.d_a_state);
                    dictionary5.Add("nicker", this.d_u_nicker);
                    dictionary5.Add("userCreditSix", this.d_six_credit);
                    dictionary5.Add("recoverCreditSix", this.d_six_usable_credit);
                    dictionary5.Add("supCreditSix", string.Format("{0:F0}", Convert.ToDouble(this.d_up_six_usable_credit)));
                    dictionary5.Add("userRateSix", this.d_six_rate);
                    dictionary5.Add("maxRateSix", this.d_maxRate_six.ToString());
                    dictionary5.Add("allowMaxRateSix", this.d_six_allow_maxrate);
                    dictionary5.Add("downRateSix", this.d_downRate_six.ToString());
                    dictionary5.Add("userAllowSaleSix", this.d_six_allow_sale);
                    dictionary5.Add("userKindSix", this.d_six_kind);
                    dictionary5.Add("isCashSix", this.d_six_iscash);
                    dictionary5.Add("date", userInfoByUID.get_add_date());
                    dictionary = dictionary5;
                }
                else if (flag3)
                {
                    int num5 = (this.d_kc_ExistsBet && this.d_kc_OpenPhase) ? 0 : 1;
                    Dictionary<string, object> dictionary6 = new Dictionary<string, object>();
                    dictionary6.Add("disabledKc", num5.ToString());
                    dictionary6.Add("supName", this.d_up_u_name);
                    dictionary6.Add("supNameNicker", this.d_up_u_nicker);
                    dictionary6.Add("name", this.d_u_name);
                    dictionary6.Add("isLocked", flag ? "1" : "0");
                    dictionary6.Add("userState", this.d_a_state);
                    dictionary6.Add("nicker", this.d_u_nicker);
                    dictionary6.Add("userCreditKc", this.d_kc_credit);
                    dictionary6.Add("recoverCreditKc", this.d_kc_usable_credit);
                    dictionary6.Add("supCreditKc", string.Format("{0:F0}", Convert.ToDouble(this.d_up_kc_usable_credit)));
                    dictionary6.Add("userRateKc", this.d_kc_rate);
                    dictionary6.Add("maxRateKc", this.d_maxRate_kc.ToString());
                    dictionary6.Add("allowMaxRateKc", this.d_kc_allow_maxrate);
                    dictionary6.Add("downRateKc", this.d_downRate_kc.ToString());
                    dictionary6.Add("userAllowSaleKc", this.d_kc_allow_sale);
                    dictionary6.Add("userKindKc", this.d_kc_kind);
                    dictionary6.Add("isCashKc", this.d_kc_iscash);
                    dictionary6.Add("date", userInfoByUID.get_add_date());
                    dictionary = dictionary6;
                }
                result.set_success(200);
                result.set_data(dictionary);
                strResult = base.ObjectToJson(result);
            }
            else if ((str2 == "edit") && !string.IsNullOrEmpty(this.d_u_name))
            {
                this.AddUser();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string str3;
            base.Response.Expires = 0;
            base.Response.CacheControl = "no-cache";
            string str = LSRequest.qq("action").Trim();
            string strResult = "";
            if (((str3 = str) != null) && (str3 == "getMemberDetail"))
            {
                this.getMemberDetail(ref strResult);
            }
            base.OutJson(strResult);
        }
    }
}

