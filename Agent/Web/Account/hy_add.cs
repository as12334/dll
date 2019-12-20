namespace Agent.Web.Account
{
    using Agent.Web.WebBase;
    using LotterySystem.Common;
    using LotterySystem.Model;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class hy_add : MemberPageBase
    {
        protected string kc_allow_sale_d = "0";
        protected string kc_credit_d = "0";
        protected string kc_dl_name = "0";
        protected string kc_dl_rate = "0";
        protected string kc_fgs_name = "0";
        protected string kc_fgs_rate = "0";
        protected string kc_gd_name = "0";
        protected string kc_gd_rate = "0";
        protected string kc_iscash = "0";
        protected string kc_kind_d = "0";
        protected string kc_rate_d = "0";
        protected string kc_u_nicker_d = "";
        protected string kc_usable_credit_d = "0";
        protected string kc_zd_name = "0";
        protected string kc_zd_rate = "0";
        protected string kc_zj_rate = "0";
        protected DataTable lotteryDT;
        protected string lottrty_kc = "";
        protected string lottrty_six = "";
        protected string six_allow_sale_d = "0";
        protected string six_credit_d = "0";
        protected string six_dl_name = "0";
        protected string six_dl_rate = "0";
        protected string six_fgs_name = "0";
        protected string six_fgs_rate = "0";
        protected string six_gd_name = "0";
        protected string six_gd_rate = "0";
        protected string six_iscash = "0";
        protected string six_kind_d = "0";
        protected string six_rate_d = "0";
        protected string six_u_nicker_d = "";
        protected string six_usable_credit_d = "0";
        protected string six_zd_name = "0";
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

        private void AddUser()
        {
            string str = LSRequest.qq("userName").ToLower().Trim();
            string str2 = "0";
            string str3 = LSRequest.qq("userPassword");
            string str4 = LSRequest.qq("userNicker");
            this.upuname = this.u_name;
            string str5 = LSRequest.qq("userCredit_six");
            string str6 = LSRequest.qq("userKind_six");
            if (!this.six_kind_d.Equals("0"))
            {
                str6 = this.six_kind_d;
            }
            else if (string.IsNullOrEmpty(str6) || str6.Equals("0"))
            {
                str6 = "A";
            }
            string str7 = LSRequest.qq("sltDrawback_six");
            if (string.IsNullOrEmpty(str5))
            {
                str5 = "0";
            }
            if (string.IsNullOrEmpty(str7))
            {
                str7 = "0";
            }
            switch (str7)
            {
                case "0":
                    str7 = "0";
                    break;

                case "1":
                    str7 = "0.1";
                    break;

                case "2":
                    str7 = "0.2";
                    break;

                case "3":
                    str7 = "0.3";
                    break;

                case "4":
                    str7 = "0.4";
                    break;

                case "5":
                    str7 = "NO";
                    break;

                case "auto":
                    break;

                default:
                    base.Response.End();
                    break;
            }
            if (str7.Equals("auto"))
            {
                string s = LSRequest.qq("sltDrawbackAuto_six");
                try
                {
                    if (double.Parse(s) < 0.0)
                    {
                        base.Response.Write(base.ShowDialogBox("⑥合彩: 退水設定 不能小於0！", null, 400));
                        base.Response.End();
                    }
                }
                catch
                {
                    base.Response.Write(base.ShowDialogBox("⑥合彩: 退水設定 輸入錯誤！", null, 400));
                    base.Response.End();
                }
                str7 = s;
            }
            string str9 = LSRequest.qq("userCredit_kc");
            string str10 = LSRequest.qq("userKind_kc");
            if (!this.kc_kind_d.Equals("0"))
            {
                str10 = this.kc_kind_d;
            }
            else if (string.IsNullOrEmpty(str10) || str10.Equals("0"))
            {
                str10 = "A";
            }
            string str11 = LSRequest.qq("sltDrawback_kc");
            if (string.IsNullOrEmpty(str9))
            {
                str9 = "0";
            }
            if (string.IsNullOrEmpty(str11))
            {
                str11 = "0";
            }
            switch (str11)
            {
                case "0":
                    str11 = "0";
                    break;

                case "1":
                    str11 = "0.1";
                    break;

                case "2":
                    str11 = "0.2";
                    break;

                case "3":
                    str11 = "0.3";
                    break;

                case "4":
                    str11 = "0.4";
                    break;

                case "5":
                    str11 = "NO";
                    break;

                case "auto":
                    break;

                default:
                    base.Response.End();
                    break;
            }
            if (str11.Equals("auto"))
            {
                string str12 = LSRequest.qq("sltDrawbackAuto_kc");
                try
                {
                    if (double.Parse(str12) < 0.0)
                    {
                        base.Response.Write(base.ShowDialogBox("快彩: 退水設定 不能小於0！", null, 400));
                        base.Response.End();
                    }
                }
                catch
                {
                    base.Response.Write(base.ShowDialogBox("快彩: 退水設定 輸入錯誤！", null, 400));
                    base.Response.End();
                }
                str11 = str12;
            }
            string message = "";
            if (!base.ValidParamByUserAdd("hy", ref message, null, this.lottrty_six, this.lottrty_kc))
            {
                base.Response.Write(base.GetAlert(message));
                base.Response.End();
            }
            if (!Regexlib.IsValidPassword(str3.Trim(), base.get_GetPasswordLU()))
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
            if (!string.IsNullOrEmpty(this.lottrty_six))
            {
                if ((!str6.ToUpper().Equals("A") && !str6.ToUpper().Equals("B")) && !str6.ToUpper().Equals("C"))
                {
                    base.Response.End();
                }
                if (string.IsNullOrEmpty(str7))
                {
                    base.Response.End();
                }
            }
            if (!string.IsNullOrEmpty(this.lottrty_kc))
            {
                if ((!str10.ToUpper().Equals("A") && !str10.ToUpper().Equals("B")) && !str10.ToUpper().Equals("C"))
                {
                    base.Response.End();
                }
                if (string.IsNullOrEmpty(str11))
                {
                    base.Response.End();
                }
            }
            cz_rate_kc rateKCByUserName = CallBLL.cz_rate_kc_bll.GetRateKCByUserName(this.upuname);
            base.En_User_Lock(rateKCByUserName.get_fgs_name());
            if (CallBLL.cz_users_bll.GetLimitHy_DL(this.upuname).Equals(-1))
            {
                base.Un_User_Lock(rateKCByUserName.get_fgs_name());
                if (this.userModel.get_u_type().Equals("dl"))
                {
                    base.Response.Write(base.ShowDialogBox("下綫人數限製已經滿員，請聯繫上級！", null, 400));
                }
                else
                {
                    base.Response.Write(base.ShowDialogBox("代理【" + this.upuname + "】下綫人數限製已經滿員！", null, 400));
                }
                base.Response.End();
            }
            if (Convert.ToDouble(str5) > Convert.ToDouble(this.six_usable_credit_d))
            {
                base.Un_User_Lock(rateKCByUserName.get_fgs_name());
                base.Response.Write(base.ShowDialogBox("⑥合彩:‘信用額度’已超過‘上級可用餘額’，請修改！", null, 400));
                base.Response.End();
            }
            string str14 = LSRequest.qq("userRate_six");
            if (string.IsNullOrEmpty(str14))
            {
                str14 = "0";
            }
            if (Convert.ToDouble(str14) > Convert.ToDouble(this.six_rate_d))
            {
                base.Un_User_Lock(rateKCByUserName.get_fgs_name());
                base.Response.Write(base.ShowDialogBox("⑥合彩: 代理占成 不可高於" + this.six_rate_d + "，請修改！", null, 400));
                base.Response.End();
            }
            if (Convert.ToDouble(str9) > Convert.ToDouble(this.kc_usable_credit_d))
            {
                base.Un_User_Lock(rateKCByUserName.get_fgs_name());
                base.Response.Write(base.ShowDialogBox("快彩:‘信用額度’已超過‘上級可用餘額’，請修改！", null, 400));
                base.Response.End();
            }
            string str15 = LSRequest.qq("userRate_kc");
            if (string.IsNullOrEmpty(str15))
            {
                str15 = "0";
            }
            if (Convert.ToDouble(str15) > Convert.ToDouble(this.kc_rate_d))
            {
                base.Un_User_Lock(rateKCByUserName.get_fgs_name());
                base.Response.Write(base.ShowDialogBox("快彩: 總代占成 不可高於" + this.kc_rate_d + "，請修改！", null, 400));
                base.Response.End();
            }
            int num4 = 0;
            decimal num5 = 0M;
            decimal num6 = 0M;
            if (!string.IsNullOrEmpty(this.lottrty_six))
            {
                num4 = Convert.ToInt32(str14);
                num5 = Convert.ToDecimal(str5);
                num6 = Convert.ToDecimal(str5);
            }
            int num7 = 0;
            decimal num8 = 0M;
            decimal num9 = 0M;
            if (!string.IsNullOrEmpty(this.lottrty_kc))
            {
                num7 = Convert.ToInt32(str15);
                num8 = Convert.ToDecimal(str9);
                num9 = Convert.ToDecimal(str9);
            }
            cz_users model = new cz_users();
            model.set_u_id(Guid.NewGuid().ToString().ToUpper());
            model.set_u_name(str);
            string ramSalt = Utils.GetRamSalt(6);
            model.set_u_psw(DESEncrypt.EncryptString(str3, ramSalt));
            model.set_salt(ramSalt);
            model.set_u_nicker(str4);
            model.set_u_skin(base.GetUserSkin("hy"));
            model.set_sup_name(this.upuname);
            model.set_u_type("hy");
            model.set_su_type("dl");
            model.set_add_date(new DateTime?(DateTime.Now));
            model.set_six_rate(new int?(num4));
            model.set_six_credit(new decimal?(num5));
            model.set_six_usable_credit(new decimal?(num6));
            model.set_six_kind(str6);
            model.set_a_state(new int?(Convert.ToInt32(str2)));
            model.set_allow_opt(1);
            model.set_is_changed(0);
            model.set_kc_rate(new int?(num7));
            model.set_kc_credit(new decimal?(num8));
            model.set_kc_usable_credit(new decimal?(num9));
            model.set_kc_kind(str10);
            model.set_kc_crash_payment(0);
            if (!string.IsNullOrEmpty(this.kc_iscash ?? ""))
            {
                model.set_kc_iscash(new int?(int.Parse(this.kc_iscash)));
            }
            if (!string.IsNullOrEmpty(this.six_iscash ?? ""))
            {
                model.set_six_iscash(new int?(int.Parse(this.six_iscash)));
            }
            cz_rate_kc _kc2 = new cz_rate_kc();
            _kc2.set_u_name(str);
            _kc2.set_u_type("hy");
            _kc2.set_dl_name(this.kc_dl_name);
            _kc2.set_zd_name(this.kc_zd_name);
            _kc2.set_gd_name(this.kc_gd_name);
            _kc2.set_fgs_name(this.kc_fgs_name);
            _kc2.set_dl_rate(new decimal?(num7));
            _kc2.set_zd_rate(new decimal?(Convert.ToDecimal(this.kc_zd_rate)));
            _kc2.set_gd_rate(new decimal?(Convert.ToDecimal(this.kc_gd_rate)));
            _kc2.set_fgs_rate(new decimal?(Convert.ToDecimal(this.kc_fgs_rate)));
            _kc2.set_zj_rate(new decimal?(Convert.ToDecimal(this.kc_zj_rate)));
            cz_rate_six _six = new cz_rate_six();
            _six.set_u_name(str);
            _six.set_u_type("hy");
            _six.set_dl_name(this.six_dl_name);
            _six.set_zd_name(this.six_zd_name);
            _six.set_gd_name(this.six_gd_name);
            _six.set_fgs_name(this.six_fgs_name);
            _six.set_dl_rate(new decimal?(num4));
            _six.set_zd_rate(new decimal?(Convert.ToDecimal(this.six_zd_rate)));
            _six.set_gd_rate(new decimal?(Convert.ToDecimal(this.six_gd_rate)));
            _six.set_fgs_rate(new decimal?(Convert.ToDecimal(this.six_fgs_rate)));
            _six.set_zj_rate(new decimal?(Convert.ToDecimal(this.six_zj_rate)));
            string str17 = "";
            if (CallBLL.cz_users_bll.AddUserInfoHY(model, _kc2, _six, ref str17, str7.ToUpper(), str11.ToUpper()))
            {
                int? nullable = model.get_six_iscash();
                int num13 = int.Parse("1");
                if ((nullable.GetValueOrDefault() == num13) && nullable.HasValue)
                {
                    cz_users _users2 = new cz_users();
                    _users2.set_u_name(model.get_u_name());
                    _users2.set_six_usable_credit(0);
                    cz_users _users3 = new cz_users();
                    _users3.set_u_name(model.get_sup_name());
                    _users3.set_six_usable_credit(new decimal?(decimal.Parse(this.six_usable_credit_d)));
                    int num14 = 1;
                    if (base.IsMasterLotteryExist(num14.ToString()))
                    {
                        base.UserRecharge_six(_users2, _users3, str5);
                    }
                }
                int? nullable2 = model.get_kc_iscash();
                int num15 = int.Parse("1");
                if ((nullable2.GetValueOrDefault() == num15) && nullable2.HasValue)
                {
                    cz_users _users4 = new cz_users();
                    _users4.set_u_name(model.get_u_name());
                    _users4.set_kc_usable_credit(0);
                    cz_users _users5 = new cz_users();
                    _users5.set_u_name(model.get_sup_name());
                    _users5.set_kc_usable_credit(new decimal?(decimal.Parse(this.kc_usable_credit_d)));
                    int num16 = 2;
                    if (base.IsMasterLotteryExist(num16.ToString()))
                    {
                        base.UserRecharge_kc(_users4, _users5, str9);
                    }
                }
                int num10 = 2;
                if (this.lottrty_six == "")
                {
                    num10 = 1;
                }
                else if (this.lottrty_kc == "")
                {
                    num10 = 0;
                }
                else if ((this.lottrty_kc != "") && (this.lottrty_six != ""))
                {
                    num10 = 2;
                }
                base.user_add_hy_log(model, false, num10, str7, str11);
                base.Un_User_Lock(rateKCByUserName.get_fgs_name());
                string url = "/Account/drawback.aspx?uid=" + str17 + "&isadd=1";
                base.Response.Write(base.ShowDialogBox("添加會員成功！", url, 0));
                base.Response.End();
            }
            else
            {
                base.Un_User_Lock(rateKCByUserName.get_fgs_name());
                base.Response.Write(base.ShowDialogBox("添加會員失敗！", base.UserReturnBackUrl, 400));
                base.Response.End();
            }
        }

        private void GetRateInfo(string g_uid)
        {
            if (!string.IsNullOrEmpty(g_uid))
            {
                Dictionary<string, string> rateByUID = CallBLL.cz_rate_six_bll.GetRateByUID(g_uid, "dl");
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
                    this.six_fgs_name = rateByUID["six_fgs_name"];
                    this.six_gd_name = rateByUID["six_gd_name"];
                    this.six_zd_name = rateByUID["six_zd_name"];
                    this.six_dl_name = rateByUID["six_dl_name"];
                    this.six_iscash = rateByUID["six_iscash"];
                }
                Dictionary<string, string> dictionary2 = CallBLL.cz_rate_kc_bll.GetRateByUID(g_uid, "dl");
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
                    this.kc_fgs_name = dictionary2["kc_fgs_name"];
                    this.kc_gd_name = dictionary2["kc_gd_name"];
                    this.kc_zd_name = dictionary2["kc_zd_name"];
                    this.kc_dl_name = dictionary2["kc_dl_name"];
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
                str = "select u_name from  cz_rate_six  where u_type='dl' " + str2;
            }
            else
            {
                cz_users userInfoByUID = CallBLL.cz_users_bll.GetUserInfoByUID(this.upuid);
                str = string.Format("select u_name from cz_rate_six where u_type='dl' and {0}_name='{1}'", userInfoByUID.get_u_type(), userInfoByUID.get_u_name());
            }
            DataTable userListByAddUser = CallBLL.cz_users_bll.GetUserListByAddUser(str, "dl");
            if (userListByAddUser == null)
            {
                base.Response.Redirect("/MessagePage.aspx?code=u100058&url=&issuccess=1&isback=0&isopen=0");
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
            string str = this.Session["user_name"].ToString();
            this.userModel = this.Session[str + "lottery_session_user_info"] as agent_userinfo_session;
            if (((!this.userModel.get_u_type().Trim().Equals("zj") && !this.userModel.get_u_type().Trim().Equals("fgs")) && (!this.userModel.get_u_type().Trim().Equals("gd") && !this.userModel.get_u_type().Trim().Equals("zd"))) && !this.userModel.get_u_type().Trim().Equals("dl"))
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
            this.sltuid = LSRequest.qq("sltuid");
            this.upuid = LSRequest.qq("uid");
            this.InitSelect();
            if (!string.IsNullOrEmpty(this.u_name) && !base.IsUpperLowerLevels(this.u_name, this.userModel.get_u_type(), this.userModel.get_u_name()))
            {
                base.Response.Redirect("../MessagePage.aspx?code=u100057&url=&issuccess=1&isback=0");
                base.Response.End();
            }
            if (LSRequest.qq("hdnadd").Equals("hdnadd") && !string.IsNullOrEmpty(this.u_name))
            {
                this.AddUser();
            }
        }
    }
}

