namespace Agent.Web.m.Account
{
    using Agent.Web.WebBase;
    using LotterySystem.Common;
    using LotterySystem.Model;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Web.UI.HtmlControls;

    public class gd_add : MemberPageBase_Mobile
    {
        protected HtmlForm form1;
        protected string kc_allow_sale_d = "0";
        protected string kc_credit_d = "0";
        protected string kc_dl_rate = "0";
        protected string kc_fgs_rate = "0";
        protected string kc_gd_rate = "0";
        protected string kc_iscash = "0";
        protected string kc_kind_d = "0";
        protected string kc_rate_d = "0";
        protected string kc_u_nicker_d = "";
        protected string kc_usable_credit_d = "0";
        protected string kc_zd_rate = "0";
        protected string kc_zj_rate = "0";
        protected DataTable lotteryDT;
        protected string lottrty_kc = "";
        protected string lottrty_six = "";
        protected string six_allow_sale_d = "0";
        protected string six_credit_d = "0";
        protected string six_dl_rate = "0";
        protected string six_fgs_rate = "0";
        protected string six_gd_rate = "0";
        protected string six_iscash = "0";
        protected string six_kind_d = "0";
        protected string six_rate_d = "0";
        protected string six_u_nicker_d = "";
        protected string six_usable_credit_d = "0";
        protected string six_zd_rate = "0";
        protected string six_zj_rate = "0";
        protected string sltuid = "";
        protected string tabState_kc = "";
        protected string tabState_six = "";
        protected string u_name = "";
        protected string up_userString = "";
        protected string upuid = "";
        protected string upuname = "";
        private agent_userinfo_session userModel;
        protected string vl = "";

        private void AddUser()
        {
            string str = LSRequest.qq("userName").ToLower().Trim();
            string str2 = "0";
            string str3 = LSRequest.qq("userPassword");
            string str4 = LSRequest.qq("userNicker");
            this.upuname = this.u_name;
            string str5 = LSRequest.qq("userCredit_six");
            string str6 = LSRequest.qq("allowmaxrate_six");
            string str7 = LSRequest.qq("lowmaxrate_six");
            string str8 = LSRequest.qq("userAllowSale_six");
            string str9 = LSRequest.qq("userKind_six");
            if (!this.six_kind_d.Equals("0"))
            {
                str9 = this.six_kind_d;
            }
            if (string.IsNullOrEmpty(str5))
            {
                str5 = "0";
            }
            if (string.IsNullOrEmpty(str9))
            {
                str9 = "0";
            }
            string str10 = LSRequest.qq("userCredit_kc");
            string str11 = LSRequest.qq("allowmaxrate_kc");
            string str12 = LSRequest.qq("lowmaxrate_kc");
            string str13 = LSRequest.qq("userAllowSale_kc");
            string str14 = LSRequest.qq("userKind_kc");
            if (!this.kc_kind_d.Equals("0"))
            {
                str14 = this.kc_kind_d;
            }
            if (string.IsNullOrEmpty(str10))
            {
                str10 = "0";
            }
            if (string.IsNullOrEmpty(str14))
            {
                str14 = "0";
            }
            string message = "";
            if (!base.ValidParamByUserAdd("gd", ref message, null, this.lottrty_six, this.lottrty_kc))
            {
                base.noRightOptMsg(message);
            }
            if (!Regexlib.IsValidPassword(str3.Trim(), base.get_GetPasswordLU()))
            {
                if (base.get_GetPasswordLU().Equals("1"))
                {
                    base.noRightOptMsg("密碼要8-20位,且必需包含大寫字母、小寫字母和数字！");
                }
                else
                {
                    base.noRightOptMsg("<script>alert('密碼要8-20位,且必需包含字母、和数字！');</script>");
                }
                base.Response.End();
            }
            if (!string.IsNullOrEmpty(this.lottrty_six))
            {
                if ((!str9.ToUpper().Equals("A") && !str9.ToUpper().Equals("B")) && (!str9.ToUpper().Equals("C") && !str9.ToUpper().Equals("0")))
                {
                    base.Response.End();
                }
                if (!str8.ToUpper().Equals("0") && !str8.ToUpper().Equals("1"))
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
            cz_rate_kc rateKCByUserName = CallBLL.cz_rate_kc_bll.GetRateKCByUserName(this.upuname);
            base.En_User_Lock(rateKCByUserName.get_fgs_name());
            if (Convert.ToDouble(str5) > Convert.ToDouble(this.six_usable_credit_d))
            {
                base.Un_User_Lock(rateKCByUserName.get_fgs_name());
                base.noRightOptMsg("⑥合彩:‘信用額度’已超過‘上級可用餘額’，請修改！");
            }
            string str16 = LSRequest.qq("userRate_six");
            if (string.IsNullOrEmpty(str16))
            {
                str16 = "0";
            }
            if (Convert.ToDouble(str16) > Convert.ToDouble(this.six_rate_d))
            {
                base.Un_User_Lock(rateKCByUserName.get_fgs_name());
                base.noRightOptMsg("⑥合彩: 分公司占成 不可高於" + this.six_rate_d + "，請修改！");
            }
            if (str6.Equals("1"))
            {
                if (string.IsNullOrEmpty(str7))
                {
                    base.Un_User_Lock(rateKCByUserName.get_fgs_name());
                    base.noRightOptMsg("⑥合彩:‘占成上限’不可為空，請修改！");
                }
                try
                {
                    int.Parse(str7);
                }
                catch
                {
                    base.Un_User_Lock(rateKCByUserName.get_fgs_name());
                    base.noRightOptMsg("⑥合彩: ‘占成上限’只能為數字，請重新設定！");
                }
                if (int.Parse(str7) > 100)
                {
                    base.Un_User_Lock(rateKCByUserName.get_fgs_name());
                    base.noRightOptMsg("⑥合彩: ‘占成上限’不可高於 100%，請重新設定！");
                    return;
                }
                if (int.Parse(str7) < 0)
                {
                    base.Un_User_Lock(rateKCByUserName.get_fgs_name());
                    base.noRightOptMsg("⑥合彩: ‘占成上限’不可低於等於 0%，請重新設定！");
                    return;
                }
            }
            else
            {
                str7 = "0";
            }
            if (Convert.ToDouble(str10) > Convert.ToDouble(this.kc_usable_credit_d))
            {
                base.Un_User_Lock(rateKCByUserName.get_fgs_name());
                base.noRightOptMsg("快彩:‘信用額度’已超過‘上級可用餘額’，請修改！");
            }
            string str17 = LSRequest.qq("userRate_kc");
            if (string.IsNullOrEmpty(str17))
            {
                str17 = "0";
            }
            if (Convert.ToDouble(str17) > Convert.ToDouble(this.kc_rate_d))
            {
                base.Un_User_Lock(rateKCByUserName.get_fgs_name());
                base.noRightOptMsg("快彩: 分公司占成 不可高於" + this.kc_rate_d + "，請修改！");
            }
            if (str11.Equals("1"))
            {
                if (string.IsNullOrEmpty(str12))
                {
                    base.Un_User_Lock(rateKCByUserName.get_fgs_name());
                    base.noRightOptMsg("快彩:‘占成上限’不可為空，請修改！");
                }
                try
                {
                    int.Parse(str12);
                }
                catch
                {
                    base.Un_User_Lock(rateKCByUserName.get_fgs_name());
                    base.noRightOptMsg("快彩: ‘占成上限’只能為數字，請重新設定！");
                }
                if (Convert.ToInt32(str12) > 100)
                {
                    base.Un_User_Lock(rateKCByUserName.get_fgs_name());
                    base.noRightOptMsg("快彩: ‘占成上限’不可高於 100%，請重新設定！");
                }
                else if (Convert.ToInt32(str12) < 0)
                {
                    base.Un_User_Lock(rateKCByUserName.get_fgs_name());
                    base.noRightOptMsg("快彩: ‘占成上限’不可低於等於 0%，請重新設定！");
                }
            }
            else
            {
                str12 = "0";
            }
            int num = 0;
            decimal num2 = 0M;
            decimal num3 = 0M;
            if (!string.IsNullOrEmpty(this.lottrty_six))
            {
                num = Convert.ToInt32(str16);
                num2 = Convert.ToDecimal(str5);
                num3 = Convert.ToDecimal(str5);
            }
            int num4 = 0;
            decimal num5 = 0M;
            decimal num6 = 0M;
            if (!string.IsNullOrEmpty(this.lottrty_kc))
            {
                num4 = Convert.ToInt32(str17);
                num5 = Convert.ToDecimal(str10);
                num6 = Convert.ToDecimal(str10);
            }
            cz_users model = new cz_users();
            model.set_u_id(Guid.NewGuid().ToString().ToUpper());
            model.set_u_name(str);
            string ramSalt = Utils.GetRamSalt(6);
            model.set_u_psw(DESEncrypt.EncryptString(str3, ramSalt));
            model.set_salt(ramSalt);
            model.set_u_nicker(str4);
            model.set_u_skin(base.GetUserSkin("agent"));
            model.set_sup_name(this.upuname);
            model.set_u_type("gd");
            model.set_su_type("fgs");
            model.set_add_date(new DateTime?(DateTime.Now));
            model.set_six_rate(new int?(num));
            model.set_six_credit(new decimal?(num2));
            model.set_six_usable_credit(new decimal?(num3));
            model.set_six_kind(str9);
            model.set_a_state(new int?(Convert.ToInt32(str2)));
            model.set_allow_sale(new int?(Convert.ToInt32(str8)));
            model.set_six_allow_maxrate(new int?(Convert.ToInt32(str6)));
            model.set_six_low_maxrate(new int?(Convert.ToInt32(str7)));
            model.set_allow_opt(1);
            model.set_is_changed(0);
            model.set_kc_rate(new int?(num4));
            model.set_kc_credit(new decimal?(num5));
            model.set_kc_usable_credit(new decimal?(num6));
            model.set_kc_kind(str14);
            model.set_kc_allow_sale(new int?(Convert.ToInt32(str13)));
            model.set_kc_allow_maxrate(new int?(Convert.ToInt32(str11)));
            model.set_kc_low_maxrate(new int?(Convert.ToInt32(str12)));
            model.set_kc_crash_payment(0);
            model.set_kc_iscash(new int?(Convert.ToInt32(this.kc_iscash)));
            model.set_six_iscash(new int?(Convert.ToInt32(this.six_iscash)));
            cz_rate_kc _kc2 = new cz_rate_kc();
            _kc2.set_u_name(str);
            _kc2.set_u_type("gd");
            _kc2.set_dl_name("");
            _kc2.set_zd_name("");
            _kc2.set_gd_name(str);
            _kc2.set_fgs_name(this.upuname);
            _kc2.set_dl_rate(0);
            _kc2.set_zd_rate(0);
            _kc2.set_gd_rate(0);
            _kc2.set_fgs_rate(new decimal?(num4));
            _kc2.set_zj_rate(new decimal?(Convert.ToDecimal(this.kc_zj_rate)));
            cz_rate_six _six = new cz_rate_six();
            _six.set_u_name(str);
            _six.set_u_type("gd");
            _six.set_dl_name("");
            _six.set_zd_name("");
            _six.set_gd_name(str);
            _six.set_fgs_name(this.upuname);
            _six.set_dl_rate(0);
            _six.set_zd_rate(0);
            _six.set_gd_rate(0);
            _six.set_fgs_rate(new decimal?(num));
            _six.set_zj_rate(new decimal?(Convert.ToDecimal(this.six_zj_rate)));
            string str19 = "";
            Dictionary<string, string> data = new Dictionary<string, string>();
            if (CallBLL.cz_users_bll.AddUserInfo(model, _kc2, _six, ref str19))
            {
                int num7 = 2;
                if (this.lottrty_six == "")
                {
                    num7 = 1;
                }
                else if (this.lottrty_kc == "")
                {
                    num7 = 0;
                }
                else if ((this.lottrty_kc != "") && (this.lottrty_six != ""))
                {
                    num7 = 2;
                }
                base.user_add_agent_log(model, num7);
                base.Un_User_Lock(rateKCByUserName.get_fgs_name());
                string text1 = "/Account/drawback.aspx?uid=" + str19 + "&isadd=1";
                data.Add("uname", model.get_u_name());
                data.Add("uid", model.get_u_id());
                base.successOptMsg("添加股東成功！", data);
            }
            else
            {
                base.Un_User_Lock(rateKCByUserName.get_fgs_name());
                base.noRightOptMsg("添加股東失敗！");
            }
        }

        private void getMemberDetail(ref string strResult)
        {
            base.checkLoginByHandler(0);
            new ReturnResult();
            new Dictionary<string, object>();
            new List<object>();
            string str = this.Session["user_name"].ToString();
            this.userModel = this.Session[str + "lottery_session_user_info"] as agent_userinfo_session;
            if (!this.userModel.get_u_type().Trim().Equals("zj") && !this.userModel.get_u_type().Trim().Equals("fgs"))
            {
                base.Response.End();
            }
            base.checkCloneRight();
            string str2 = LSRequest.qq("memberId");
            string str3 = LSRequest.qq("submitType");
            if (str3 != "add")
            {
                base.Response.End();
            }
            if (str2 != "gd")
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
                this.vl = "3";
            }
            else if (!string.IsNullOrEmpty(this.lottrty_six) && string.IsNullOrEmpty(this.lottrty_kc))
            {
                this.tabState_six = "on";
                this.vl = "1";
            }
            else if (string.IsNullOrEmpty(this.lottrty_six) && !string.IsNullOrEmpty(this.lottrty_kc))
            {
                this.tabState_kc = "on";
                this.vl = "2";
            }
            this.sltuid = LSRequest.qq("sltuid");
            this.upuid = LSRequest.qq("uid");
            if (string.IsNullOrEmpty(this.sltuid))
            {
                this.sltuid = this.upuid;
            }
            this.InitSelect();
            if (!string.IsNullOrEmpty(this.u_name) && !base.IsUpperLowerLevels(this.u_name, this.userModel.get_u_type(), this.userModel.get_u_name()))
            {
                base.Response.End();
            }
            if ((str3 == "add") && !string.IsNullOrEmpty(this.u_name))
            {
                this.AddUser();
            }
        }

        private void GetRateInfo(string g_uid)
        {
            if (!string.IsNullOrEmpty(g_uid))
            {
                Dictionary<string, string> rateByUID = CallBLL.cz_rate_six_bll.GetRateByUID(g_uid, "fgs");
                if (rateByUID != null)
                {
                    this.six_rate_d = rateByUID["six_rate"];
                    this.six_credit_d = rateByUID["six_credit"];
                    this.six_usable_credit_d = rateByUID["six_usable_credit"];
                    this.six_allow_sale_d = rateByUID["six_allow_sale"];
                    this.six_u_nicker_d = rateByUID["six_u_nicker"];
                    this.six_kind_d = rateByUID["six_kind"];
                    this.u_name = rateByUID["u_name"];
                    this.six_zj_rate = rateByUID["six_zj_rate"];
                    this.six_fgs_rate = rateByUID["six_fgs_rate"];
                    this.six_gd_rate = rateByUID["six_gd_rate"];
                    this.six_zd_rate = rateByUID["six_zd_rate"];
                    this.six_dl_rate = rateByUID["six_dl_rate"];
                    this.six_iscash = rateByUID["six_iscash"];
                }
                Dictionary<string, string> dictionary2 = CallBLL.cz_rate_kc_bll.GetRateByUID(g_uid, "fgs");
                if (dictionary2 != null)
                {
                    this.kc_rate_d = dictionary2["kc_rate"];
                    this.kc_credit_d = dictionary2["kc_credit"];
                    this.kc_usable_credit_d = dictionary2["kc_usable_credit"];
                    this.kc_allow_sale_d = dictionary2["kc_allow_sale"];
                    this.kc_u_nicker_d = dictionary2["kc_u_nicker"];
                    this.kc_kind_d = dictionary2["kc_kind"];
                    this.u_name = dictionary2["u_name"];
                    this.kc_zj_rate = dictionary2["kc_zj_rate"];
                    this.kc_fgs_rate = dictionary2["kc_fgs_rate"];
                    this.kc_gd_rate = dictionary2["kc_gd_rate"];
                    this.kc_zd_rate = dictionary2["kc_zd_rate"];
                    this.kc_dl_rate = dictionary2["kc_dl_rate"];
                    this.kc_iscash = dictionary2["kc_iscash"];
                }
            }
        }

        private void InitSelect()
        {
            string str = "";
            if (string.IsNullOrEmpty(this.upuid))
            {
                string str2 = "";
                if (this.userModel.get_u_type().Equals("zj"))
                {
                    str2 = "";
                }
                else
                {
                    str2 = " and " + this.userModel.get_u_type() + "_name='" + this.userModel.get_u_name() + "'";
                }
                str = "select u_name from  cz_rate_six  where u_type='fgs' " + str2;
            }
            else
            {
                cz_users userInfoByUID = CallBLL.cz_users_bll.GetUserInfoByUID(this.upuid);
                str = string.Format("select u_name from cz_rate_six where u_type='fgs' and {0}_name='{1}'", userInfoByUID.get_u_type(), userInfoByUID.get_u_name());
            }
            DataTable userListByAddUser = CallBLL.cz_users_bll.GetUserListByAddUser(str, "fgs");
            if (userListByAddUser == null)
            {
                base.Response.End();
            }
            for (int i = 0; i < userListByAddUser.Rows.Count; i++)
            {
                if (string.IsNullOrEmpty(this.sltuid))
                {
                    if (i == 0)
                    {
                        this.up_userString = this.up_userString + string.Format("<option value=\"{0}\" selected=selected>{1}</option>", userListByAddUser.Rows[i]["u_id"].ToString(), userListByAddUser.Rows[i]["u_name"].ToString());
                        this.GetRateInfo(userListByAddUser.Rows[i]["u_id"].ToString());
                    }
                    else
                    {
                        this.up_userString = this.up_userString + string.Format("<option value=\"{0}\" >{1}</option>", userListByAddUser.Rows[i]["u_id"].ToString(), userListByAddUser.Rows[i]["u_name"].ToString());
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(this.sltuid) && this.sltuid.ToUpper().Equals(userListByAddUser.Rows[i]["u_id"].ToString().ToUpper()))
                    {
                        this.up_userString = this.up_userString + string.Format("<option value=\"{0}\" selected=selected>{1}</option>", userListByAddUser.Rows[i]["u_id"].ToString(), userListByAddUser.Rows[i]["u_name"].ToString());
                        this.GetRateInfo(this.sltuid);
                    }
                    if (!this.sltuid.ToUpper().Equals(userListByAddUser.Rows[i]["u_id"].ToString().ToUpper()))
                    {
                        this.up_userString = this.up_userString + string.Format("<option value=\"{0}\" >{1}</option>", userListByAddUser.Rows[i]["u_id"].ToString(), userListByAddUser.Rows[i]["u_name"].ToString());
                    }
                }
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

