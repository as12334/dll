namespace Agent.Web.m.Account
{
    using Agent.Web.WebBase;
    using LotterySystem.Common;
    using LotterySystem.Model;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class credit_recharge : MemberPageBase_Mobile
    {
        protected DataTable lotteryDT;
        private agent_userinfo_session userModel;

        private void addRecharge(ref string strResult)
        {
            bool flag;
            bool flag2;
            base.checkLoginByHandler(0);
            new ReturnResult();
            new Dictionary<string, object>();
            new List<object>();
            string str = this.Session["user_name"].ToString();
            this.userModel = this.Session[str + "lottery_session_user_info"] as agent_userinfo_session;
            if ((!this.userModel.get_u_type().Trim().Equals("zj") && !this.userModel.get_u_type().Trim().Equals("fgs")) && (!this.userModel.get_u_type().Trim().Equals("gd") && !this.userModel.get_u_type().Trim().Equals("zd")))
            {
                base.Response.End();
            }
            base.checkCloneRight();
            this.lotteryDT = base.GetLotteryList();
            base.Permission_Aspx_ZJ_Ajax(this.userModel, "po_2_1");
            base.Permission_Aspx_DL_Ajax(this.userModel, "po_6_1");
            string s = LSRequest.qq("masterid");
            string str3 = LSRequest.qq("uid");
            string str4 = LSRequest.qq("czje");
            cz_users userInfoByUID = null;
            cz_users userInfoByUName = null;
            userInfoByUID = CallBLL.cz_users_bll.GetUserInfoByUID(str3);
            this.Hy_SubmitCreditLock(userInfoByUID.get_u_name(), userInfoByUID.get_u_type());
            if (!base.IsUpperLowerLevels(userInfoByUID.get_u_name(), this.userModel.get_u_type(), this.userModel.get_u_name()))
            {
                this.Hy_DeleteCreditLock(userInfoByUID.get_u_name(), userInfoByUID.get_u_type());
                base.Response.End();
            }
            cz_rate_kc rateKCByUserName = CallBLL.cz_rate_kc_bll.GetRateKCByUserName(userInfoByUID.get_u_name());
            base.En_User_Lock(rateKCByUserName.get_fgs_name());
            userInfoByUName = CallBLL.cz_users_bll.GetUserInfoByUName(userInfoByUID.get_sup_name());
            base.OpenLotteryMaster(out flag, out flag2);
            if (flag && userInfoByUID.get_six_iscash().Equals(int.Parse("1")))
            {
                int num3 = 1;
                if (s.Equals(num3.ToString()))
                {
                    if (!userInfoByUID.get_six_iscash().Equals(int.Parse("1")))
                    {
                        base.Response.End();
                    }
                    if (!Utils.IsDouble(str4))
                    {
                        this.Hy_DeleteCreditLock(userInfoByUID.get_u_name(), userInfoByUID.get_u_type());
                        base.Un_User_Lock(rateKCByUserName.get_fgs_name());
                        base.noRightOptMsg("(⑥)充值格式不正確！");
                        base.Response.End();
                    }
                    if (double.Parse(str4) < 1.0)
                    {
                        this.Hy_DeleteCreditLock(userInfoByUID.get_u_name(), userInfoByUID.get_u_type());
                        base.Un_User_Lock(rateKCByUserName.get_fgs_name());
                        base.noRightOptMsg("(⑥)充值金額不能小於1！");
                        base.Response.End();
                    }
                    if (!userInfoByUName.get_u_type().Equals("zj"))
                    {
                        decimal num4 = decimal.Parse(str4);
                        if (num4 > userInfoByUName.get_six_usable_credit())
                        {
                            this.Hy_DeleteCreditLock(userInfoByUID.get_u_name(), userInfoByUID.get_u_type());
                            base.Un_User_Lock(rateKCByUserName.get_fgs_name());
                            base.noRightOptMsg("(⑥)充值金額不能大於 “上級可用餘額” ！！");
                            base.Response.End();
                        }
                    }
                    string random = Utils.GetRandom();
                    cz_credit_flow _flow = new cz_credit_flow();
                    _flow.set_master_id(new int?(int.Parse(s)));
                    _flow.set_credit_old(userInfoByUID.get_six_usable_credit());
                    decimal? nullable4 = userInfoByUID.get_six_usable_credit();
                    decimal num5 = decimal.Parse(str4);
                    _flow.set_credit_new(nullable4.HasValue ? new decimal?(nullable4.GetValueOrDefault() + num5) : null);
                    _flow.set_credit_change(new decimal?(decimal.Parse(str4)));
                    _flow.set_u_name(userInfoByUID.get_u_name());
                    _flow.set_iscash(new int?(int.Parse("1")));
                    _flow.set_operator_name(this.userModel.get_u_name());
                    if (this.userModel.get_users_child_session() != null)
                    {
                        _flow.set_operator_child_name(this.userModel.get_users_child_session().get_u_name());
                    }
                    _flow.set_operator_time(DateTime.Now);
                    _flow.set_flag(0);
                    _flow.set_checkcode(random);
                    _flow.set_isphone(1);
                    _flow.set_ip(LSRequest.GetIP());
                    cz_credit_flow _flow2 = new cz_credit_flow();
                    _flow2.set_master_id(new int?(int.Parse(s)));
                    _flow2.set_credit_old(userInfoByUName.get_six_usable_credit());
                    decimal? nullable6 = userInfoByUName.get_six_usable_credit();
                    decimal num6 = decimal.Parse(str4);
                    _flow2.set_credit_new(nullable6.HasValue ? new decimal?(nullable6.GetValueOrDefault() - num6) : null);
                    _flow2.set_credit_change(new decimal?(-decimal.Parse(str4)));
                    _flow2.set_u_name(userInfoByUName.get_u_name());
                    _flow2.set_iscash(new int?(int.Parse("1")));
                    _flow2.set_operator_name(this.userModel.get_u_name());
                    if (this.userModel.get_users_child_session() != null)
                    {
                        _flow2.set_operator_child_name(this.userModel.get_users_child_session().get_u_name());
                    }
                    _flow2.set_operator_time(DateTime.Now);
                    _flow2.set_flag(1);
                    _flow2.set_checkcode(random);
                    _flow2.set_note(userInfoByUID.get_u_name());
                    _flow2.set_isphone(1);
                    _flow2.set_ip(LSRequest.GetIP());
                    if (CallBLL.cz_users_bll.UserRecharge(userInfoByUName.get_u_type(), userInfoByUName.get_u_id(), userInfoByUID.get_u_id(), double.Parse(str4), _flow, _flow2) > 0)
                    {
                        this.Hy_DeleteCreditLock(userInfoByUID.get_u_name(), userInfoByUID.get_u_type());
                        base.Un_User_Lock(rateKCByUserName.get_fgs_name());
                        base.successOptMsg("(⑥)充值成功！");
                        base.Response.End();
                        return;
                    }
                    this.Hy_DeleteCreditLock(userInfoByUID.get_u_name(), userInfoByUID.get_u_type());
                    base.Un_User_Lock(rateKCByUserName.get_fgs_name());
                    base.noRightOptMsg("(⑥)充值失敗！");
                    base.Response.End();
                    return;
                }
            }
            if (flag2 && userInfoByUID.get_kc_iscash().Equals(int.Parse("1")))
            {
                int num7 = 2;
                if (s.Equals(num7.ToString()))
                {
                    if (!userInfoByUID.get_kc_iscash().Equals(int.Parse("1")))
                    {
                        base.Response.End();
                    }
                    if (!Utils.IsDouble(str4))
                    {
                        this.Hy_DeleteCreditLock(userInfoByUID.get_u_name(), userInfoByUID.get_u_type());
                        base.Un_User_Lock(rateKCByUserName.get_fgs_name());
                        base.noRightOptMsg("(⑥)充值格式不正確！");
                        base.Response.End();
                    }
                    if (double.Parse(str4) < 1.0)
                    {
                        this.Hy_DeleteCreditLock(userInfoByUID.get_u_name(), userInfoByUID.get_u_type());
                        base.Un_User_Lock(rateKCByUserName.get_fgs_name());
                        base.noRightOptMsg("(快)充值金額不能小於！");
                        base.Response.End();
                    }
                    if (!userInfoByUName.get_u_type().Equals("zj"))
                    {
                        decimal num8 = decimal.Parse(str4);
                        if (num8 > userInfoByUName.get_kc_usable_credit())
                        {
                            this.Hy_DeleteCreditLock(userInfoByUID.get_u_name(), userInfoByUID.get_u_type());
                            base.Un_User_Lock(rateKCByUserName.get_fgs_name());
                            base.noRightOptMsg("(快)充值金額不能大於 “上級可用餘額” ！");
                            base.Response.End();
                        }
                    }
                    string str6 = Utils.GetRandom();
                    cz_credit_flow _flow3 = new cz_credit_flow();
                    _flow3.set_master_id(new int?(int.Parse(s)));
                    _flow3.set_credit_old(userInfoByUID.get_kc_usable_credit());
                    decimal? nullable11 = userInfoByUID.get_kc_usable_credit();
                    decimal num9 = decimal.Parse(str4);
                    _flow3.set_credit_new(nullable11.HasValue ? new decimal?(nullable11.GetValueOrDefault() + num9) : null);
                    _flow3.set_credit_change(new decimal?(decimal.Parse(str4)));
                    _flow3.set_u_name(userInfoByUID.get_u_name());
                    _flow3.set_iscash(new int?(int.Parse("1")));
                    _flow3.set_operator_name(this.userModel.get_u_name());
                    if (this.userModel.get_users_child_session() != null)
                    {
                        _flow3.set_operator_child_name(this.userModel.get_users_child_session().get_u_name());
                    }
                    _flow3.set_operator_time(DateTime.Now);
                    _flow3.set_flag(0);
                    _flow3.set_checkcode(str6);
                    _flow3.set_isphone(1);
                    _flow3.set_ip(LSRequest.GetIP());
                    cz_credit_flow _flow4 = new cz_credit_flow();
                    _flow4.set_master_id(new int?(int.Parse(s)));
                    _flow4.set_credit_old(userInfoByUName.get_kc_usable_credit());
                    decimal? nullable13 = userInfoByUName.get_kc_usable_credit();
                    decimal num10 = decimal.Parse(str4);
                    _flow4.set_credit_new(nullable13.HasValue ? new decimal?(nullable13.GetValueOrDefault() - num10) : null);
                    _flow4.set_credit_change(new decimal?(-decimal.Parse(str4)));
                    _flow4.set_u_name(userInfoByUName.get_u_name());
                    _flow4.set_iscash(new int?(int.Parse("1")));
                    _flow4.set_operator_name(this.userModel.get_u_name());
                    if (this.userModel.get_users_child_session() != null)
                    {
                        _flow4.set_operator_child_name(this.userModel.get_users_child_session().get_u_name());
                    }
                    _flow4.set_operator_time(DateTime.Now);
                    _flow4.set_flag(1);
                    _flow4.set_checkcode(str6);
                    _flow4.set_note(userInfoByUID.get_u_name());
                    _flow4.set_isphone(1);
                    _flow4.set_ip(LSRequest.GetIP());
                    if (CallBLL.cz_users_bll.UserRecharge(userInfoByUName.get_u_type(), userInfoByUName.get_u_id(), userInfoByUID.get_u_id(), double.Parse(str4), _flow3, _flow4) > 0)
                    {
                        this.Hy_DeleteCreditLock(userInfoByUID.get_u_name(), userInfoByUID.get_u_type());
                        base.Un_User_Lock(rateKCByUserName.get_fgs_name());
                        base.successOptMsg("(快)充值成功！");
                        base.Response.End();
                        return;
                    }
                    this.Hy_DeleteCreditLock(userInfoByUID.get_u_name(), userInfoByUID.get_u_type());
                    base.Un_User_Lock(rateKCByUserName.get_fgs_name());
                    base.noRightOptMsg("(快)充值失敗！");
                    base.Response.End();
                    return;
                }
            }
            this.Hy_DeleteCreditLock(userInfoByUID.get_u_name(), userInfoByUID.get_u_type());
            base.Un_User_Lock(rateKCByUserName.get_fgs_name());
            base.noRightOptMsg("參數錯誤！");
            base.Response.End();
        }

        private void getRechargeInit(ref string strResult)
        {
            bool flag;
            bool flag2;
            base.checkLoginByHandler(0);
            ReturnResult result = new ReturnResult();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            new List<object>();
            string str = this.Session["user_name"].ToString();
            this.userModel = this.Session[str + "lottery_session_user_info"] as agent_userinfo_session;
            if ((!this.userModel.get_u_type().Trim().Equals("zj") && !this.userModel.get_u_type().Trim().Equals("fgs")) && (!this.userModel.get_u_type().Trim().Equals("gd") && !this.userModel.get_u_type().Trim().Equals("zd")))
            {
                base.Response.End();
            }
            base.checkCloneRight();
            this.lotteryDT = base.GetLotteryList();
            base.Permission_Aspx_ZJ_Ajax(this.userModel, "po_2_1");
            base.Permission_Aspx_DL_Ajax(this.userModel, "po_6_1");
            string str2 = LSRequest.qq("uid");
            cz_users userInfoByUID = null;
            cz_users userInfoByUName = null;
            userInfoByUID = CallBLL.cz_users_bll.GetUserInfoByUID(str2);
            if (userInfoByUID.get_u_name().Equals(this.userModel.get_u_name()))
            {
                base.Response.End();
            }
            if (!base.IsUpperLowerLevels(userInfoByUID.get_u_name(), this.userModel.get_u_type(), this.userModel.get_u_name()))
            {
                base.Response.End();
            }
            userInfoByUName = CallBLL.cz_users_bll.GetUserInfoByUName(userInfoByUID.get_sup_name());
            base.OpenLotteryMaster(out flag, out flag2);
            dictionary.Add("name", userInfoByUID.get_u_name());
            dictionary.Add("utype", base.GetUTypeText(userInfoByUID.get_u_type()));
            dictionary.Add("nicker", userInfoByUID.get_u_nicker());
            if (!userInfoByUName.get_u_type().Equals("zj"))
            {
                dictionary.Add("supName", userInfoByUName.get_u_name());
                dictionary.Add("supType", base.GetUTypeText(userInfoByUName.get_u_type()));
                dictionary.Add("supNicker", userInfoByUName.get_u_nicker());
            }
            if (flag && userInfoByUID.get_six_iscash().Equals(int.Parse("1")))
            {
                if (!userInfoByUName.get_u_type().Equals("zj"))
                {
                    dictionary.Add("supRecoverCreditSix", userInfoByUName.get_six_usable_credit());
                }
                dictionary.Add("recoverCreditSix", userInfoByUID.get_six_usable_credit());
                dictionary.Add("isCashSix", userInfoByUID.get_six_iscash());
                dictionary.Add("isMasteridSix", 1);
            }
            if (flag2 && userInfoByUID.get_kc_iscash().Equals(int.Parse("1")))
            {
                if (!userInfoByUName.get_u_type().Equals("zj"))
                {
                    dictionary.Add("supRecoverCreditKc", userInfoByUName.get_kc_usable_credit());
                }
                dictionary.Add("recoverCreditKc", userInfoByUID.get_kc_usable_credit());
                dictionary.Add("isCashKc", userInfoByUID.get_kc_iscash());
                dictionary.Add("isMasteridKc", 2);
            }
            result.set_success(200);
            result.set_data(dictionary);
            strResult = base.ObjectToJson(result);
        }

        private void Hy_DeleteCreditLock(string u_name, string u_type)
        {
            if (u_type.Equals("hy"))
            {
                base.DeleteCreditLock(u_name);
            }
        }

        private void Hy_SubmitCreditLock(string u_name, string u_type)
        {
            if (u_type.Equals("hy") && !base.IsCreditLock(u_name))
            {
                base.noRightOptMsg("系統繁忙，請稍後！");
                base.Response.End();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            base.Response.Expires = 0;
            base.Response.CacheControl = "no-cache";
            string str = LSRequest.qq("action").Trim();
            string strResult = "";
            string str3 = str;
            if (str3 != null)
            {
                if (!(str3 == "getUserCZ"))
                {
                    if (str3 == "addUserCZ")
                    {
                        this.addRecharge(ref strResult);
                    }
                }
                else
                {
                    this.getRechargeInit(ref strResult);
                }
            }
            base.OutJson(strResult);
        }
    }
}

