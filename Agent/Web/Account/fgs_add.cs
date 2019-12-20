namespace Agent.Web.Account
{
    using Agent.Web.WebBase;
    using LotterySystem.Common;
    using LotterySystem.Model;
    using System;
    using System.Data;

    public class fgs_add : MemberPageBase
    {
        protected DataTable lotteryDT;
        protected string lottrty_kc = "";
        protected string lottrty_six = "";
        protected string tabState_kc = "";
        protected string tabState_six = "";
        private agent_userinfo_session userModel;
        protected string vl = "";

        private void AddUser()
        {
            string str = LSRequest.qq("userName").ToLower().Trim();
            string str2 = "0";
            string str3 = LSRequest.qq("userPassword");
            string str4 = LSRequest.qq("userNicker");
            string str5 = LSRequest.qq("userReport");
            string str6 = LSRequest.qq("userCredit_six");
            string str7 = LSRequest.qq("userRate_six");
            string str8 = LSRequest.qq("userAllowSale_six");
            string str9 = LSRequest.qq("userRateOwner_six");
            string str10 = LSRequest.qq("userKind_six");
            string str11 = LSRequest.qq("op_six");
            string str12 = LSRequest.qq("userCredit_kc");
            string str13 = LSRequest.qq("userRate_kc");
            string str14 = LSRequest.qq("userAllowSale_kc");
            string str15 = LSRequest.qq("userRateOwner_kc");
            string str16 = LSRequest.qq("userKind_kc");
            string str17 = LSRequest.qq("isCash_kc");
            string str18 = LSRequest.qq("isCash_six");
            string str19 = LSRequest.qq("op_kc");
            string str20 = "0";
            string str21 = "0";
            string message = "";
            if (!base.ValidParamByUserAdd("fgs", ref message, null, this.lottrty_six, this.lottrty_kc))
            {
                base.Response.Write(base.ShowDialogBox(message, null, 400));
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
            if (!str5.ToUpper().Equals("0") && !str5.ToUpper().Equals("1"))
            {
                base.Response.End();
            }
            if (!str17.ToUpper().Equals("0") && !str17.ToUpper().Equals("1"))
            {
                base.Response.End();
            }
            if (!str18.ToUpper().Equals("0") && !str18.ToUpper().Equals("1"))
            {
                base.Response.End();
            }
            if (!str11.ToUpper().Equals("0") && !str11.ToUpper().Equals("1"))
            {
                base.Response.End();
            }
            if (!str19.ToUpper().Equals("0") && !str19.ToUpper().Equals("1"))
            {
                base.Response.End();
            }
            if (!string.IsNullOrEmpty(this.lottrty_six))
            {
                if ((!str10.ToUpper().Equals("A") && !str10.ToUpper().Equals("B")) && (!str10.ToUpper().Equals("C") && !str10.ToUpper().Equals("0")))
                {
                    base.Response.End();
                }
                if (!str8.ToUpper().Equals("0") && !str8.ToUpper().Equals("1"))
                {
                    base.Response.End();
                }
                if (!str9.ToUpper().Equals("0") && !str9.ToUpper().Equals("1"))
                {
                    base.Response.End();
                }
                if (!str21.ToUpper().Equals("0") && !str21.ToUpper().Equals("1"))
                {
                    base.Response.End();
                }
            }
            if (!string.IsNullOrEmpty(this.lottrty_kc))
            {
                if ((!str16.ToUpper().Equals("A") && !str16.ToUpper().Equals("B")) && (!str16.ToUpper().Equals("C") && !str16.ToUpper().Equals("0")))
                {
                    base.Response.End();
                }
                if (!str14.ToUpper().Equals("0") && !str14.ToUpper().Equals("1"))
                {
                    base.Response.End();
                }
                if (!str15.ToUpper().Equals("0") && !str15.ToUpper().Equals("1"))
                {
                    base.Response.End();
                }
                if (!str21.ToUpper().Equals("0") && !str21.ToUpper().Equals("1"))
                {
                    base.Response.End();
                }
            }
            int num = 100;
            decimal num2 = 0M;
            decimal num3 = 0M;
            if (!string.IsNullOrEmpty(this.lottrty_six))
            {
                num = Convert.ToInt32(str7);
                num2 = Convert.ToDecimal(str6);
                num3 = Convert.ToDecimal(str6);
            }
            int num4 = 100;
            decimal num5 = 0M;
            decimal num6 = 0M;
            if (!string.IsNullOrEmpty(this.lottrty_kc))
            {
                num4 = Convert.ToInt32(str13);
                num5 = Convert.ToDecimal(str12);
                num6 = Convert.ToDecimal(str12);
            }
            cz_users model = new cz_users();
            model.set_u_id(Guid.NewGuid().ToString().ToUpper());
            model.set_u_name(str);
            string ramSalt = Utils.GetRamSalt(6);
            model.set_u_psw(DESEncrypt.EncryptString(str3, ramSalt));
            model.set_salt(ramSalt);
            model.set_u_nicker(str4);
            model.set_u_skin(base.GetUserSkin("agent"));
            model.set_sup_name(this.Session["user_name"].ToString());
            model.set_u_type("fgs");
            model.set_su_type("zj");
            model.set_add_date(new DateTime?(DateTime.Now));
            model.set_six_rate(new int?(num));
            model.set_six_credit(new decimal?(num2));
            model.set_six_usable_credit(new decimal?(num3));
            model.set_six_kind(str10);
            model.set_a_state(new int?(Convert.ToInt32(str2)));
            model.set_allow_sale(new int?(Convert.ToInt32(str8)));
            model.set_allow_view_report(new int?(Convert.ToInt32(str5)));
            model.set_six_allow_maxrate(0);
            model.set_six_low_maxrate(0);
            model.set_six_rate_owner(new int?(Convert.ToInt32(str9)));
            model.set_six_iscash(new int?(Convert.ToInt32(str18)));
            model.set_allow_opt(1);
            model.set_is_changed(0);
            model.set_kc_rate(new int?(num4));
            model.set_kc_credit(new decimal?(num5));
            model.set_kc_usable_credit(new decimal?(num6));
            model.set_kc_kind(str16);
            model.set_kc_allow_sale(new int?(Convert.ToInt32(str14)));
            model.set_kc_allow_maxrate(0);
            model.set_kc_low_maxrate(0);
            model.set_kc_rate_owner(new int?(Convert.ToInt32(str15)));
            model.set_kc_crash_payment(0);
            model.set_kc_iscash(new int?(Convert.ToInt32(str17)));
            model.set_six_op_odds(new int?(Convert.ToInt32(str11)));
            model.set_kc_op_odds(new int?(Convert.ToInt32(str19)));
            if (base.get_GetIsShowFgsWT().Equals("0"))
            {
                model.set_six_op_odds(0);
                model.set_kc_op_odds(0);
            }
            model.set_kc_isauto_back(new int?(Convert.ToInt32(str20)));
            model.set_six_isauto_back(new int?(Convert.ToInt32(str21)));
            cz_rate_kc _kc = new cz_rate_kc();
            _kc.set_u_name(str);
            _kc.set_u_type("fgs");
            _kc.set_dl_name("");
            _kc.set_zd_name("");
            _kc.set_gd_name("");
            _kc.set_fgs_name(str);
            _kc.set_dl_rate(0);
            _kc.set_zd_rate(0);
            _kc.set_gd_rate(0);
            _kc.set_fgs_rate(0);
            _kc.set_zj_rate(new decimal?(num4));
            cz_rate_six _six = new cz_rate_six();
            _six.set_u_name(str);
            _six.set_u_type("fgs");
            _six.set_dl_name("");
            _six.set_zd_name("");
            _six.set_gd_name("");
            _six.set_fgs_name(str);
            _six.set_dl_rate(0);
            _six.set_zd_rate(0);
            _six.set_gd_rate(0);
            _six.set_fgs_rate(0);
            _six.set_zj_rate(new decimal?(num));
            string str24 = "";
            if (CallBLL.cz_users_bll.AddUserInfoFGS(model, _kc, _six, ref str24))
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
                base.user_add_fgs_log(model, num7);
                string url = "/Account/drawback.aspx?uid=" + str24 + "&isadd=1";
                base.Response.Write(base.ShowDialogBox("添加分公司成功！", url, 0));
                base.Response.End();
            }
            else
            {
                base.Response.Write(base.ShowDialogBox("添加分公司失敗！", base.UserReturnBackUrl, 400));
                base.Response.End();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string str = this.Session["user_name"].ToString();
            this.userModel = this.Session[str + "lottery_session_user_info"] as agent_userinfo_session;
            if (!this.userModel.get_u_type().Trim().Equals("zj"))
            {
                base.Response.Redirect("../MessagePage.aspx?code=u100035&url=&issuccess=1&isback=0&isopen=1");
                base.Response.End();
            }
            base.Permission_Aspx_ZJ(this.userModel, "po_2_1");
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
            if (LSRequest.qq("hdnadd").Equals("hdnadd"))
            {
                this.AddUser();
            }
        }
    }
}

