namespace Agent.Web.m.Account
{
    using Agent.Web.WebBase;
    using LotterySystem.Common;
    using LotterySystem.Model;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class credit_withdraw : MemberPageBase_Mobile
    {
        protected DataTable lotteryDT;
        private agent_userinfo_session userModel;

        private void addWithdraw(ref string strResult)
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
            string str4 = LSRequest.qq("txje");
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
                        base.noRightOptMsg("(⑥)提現格式不正確！");
                        base.Response.End();
                    }
                    if (double.Parse(str4) < 1.0)
                    {
                        this.Hy_DeleteCreditLock(userInfoByUID.get_u_name(), userInfoByUID.get_u_type());
                        base.Un_User_Lock(rateKCByUserName.get_fgs_name());
                        base.noRightOptMsg("(⑥)提現金額不能小於1！");
                        base.Response.End();
                    }
                    if (double.Parse(str4) > double.Parse(userInfoByUID.get_six_usable_credit().ToString()))
                    {
                        this.Hy_DeleteCreditLock(userInfoByUID.get_u_name(), userInfoByUID.get_u_type());
                        base.Un_User_Lock(rateKCByUserName.get_fgs_name());
                        base.noRightOptMsg(string.Format("(⑥)提現金額不能大於{0}！", Utils.GetMathRound(double.Parse(userInfoByUID.get_six_usable_credit().ToString()), null)));
                        base.Response.End();
                    }
                    if (CallBLL.cz_users_bll.UserWithdraw_six(userInfoByUID, userInfoByUName, double.Parse(str4), int.Parse(s), this.userModel.get_u_name(), (this.userModel.get_users_child_session() != null) ? this.userModel.get_users_child_session().get_u_name() : "", 1) > 0)
                    {
                        this.Hy_DeleteCreditLock(userInfoByUID.get_u_name(), userInfoByUID.get_u_type());
                        base.Un_User_Lock(rateKCByUserName.get_fgs_name());
                        base.successOptMsg("(⑥)提現成功！");
                        base.Response.End();
                        return;
                    }
                    this.Hy_DeleteCreditLock(userInfoByUID.get_u_name(), userInfoByUID.get_u_type());
                    base.Un_User_Lock(rateKCByUserName.get_fgs_name());
                    base.noRightOptMsg("(⑥)提現失敗！");
                    base.Response.End();
                    return;
                }
            }
            if (flag2 && userInfoByUID.get_kc_iscash().Equals(int.Parse("1")))
            {
                int num4 = 2;
                if (s.Equals(num4.ToString()))
                {
                    if (!userInfoByUID.get_kc_iscash().Equals(int.Parse("1")))
                    {
                        base.Response.End();
                    }
                    if (!Utils.IsDouble(str4))
                    {
                        this.Hy_DeleteCreditLock(userInfoByUID.get_u_name(), userInfoByUID.get_u_type());
                        base.Un_User_Lock(rateKCByUserName.get_fgs_name());
                        base.noRightOptMsg("(快)提現格式不正確！");
                        base.Response.End();
                    }
                    if (double.Parse(str4) < 1.0)
                    {
                        this.Hy_DeleteCreditLock(userInfoByUID.get_u_name(), userInfoByUID.get_u_type());
                        base.Un_User_Lock(rateKCByUserName.get_fgs_name());
                        base.noRightOptMsg("(快)提現金額不能小於1！");
                        base.Response.End();
                    }
                    if (double.Parse(str4) > double.Parse(userInfoByUID.get_kc_usable_credit().ToString()))
                    {
                        this.Hy_DeleteCreditLock(userInfoByUID.get_u_name(), userInfoByUID.get_u_type());
                        base.Un_User_Lock(rateKCByUserName.get_fgs_name());
                        base.noRightOptMsg(string.Format("(快)提現金額不能大於{0}！", Utils.GetMathRound(double.Parse(userInfoByUID.get_kc_usable_credit().ToString()), null)));
                        base.Response.End();
                    }
                    if (CallBLL.cz_users_bll.UserWithdraw_kc(userInfoByUID, userInfoByUName, double.Parse(str4), int.Parse(s), this.userModel.get_u_name(), (this.userModel.get_users_child_session() != null) ? this.userModel.get_users_child_session().get_u_name() : "", 1) > 0)
                    {
                        this.Hy_DeleteCreditLock(userInfoByUID.get_u_name(), userInfoByUID.get_u_type());
                        base.Un_User_Lock(rateKCByUserName.get_fgs_name());
                        base.successOptMsg("(快)提現成功！");
                        base.Response.End();
                        return;
                    }
                    this.Hy_DeleteCreditLock(userInfoByUID.get_u_name(), userInfoByUID.get_u_type());
                    base.Un_User_Lock(rateKCByUserName.get_fgs_name());
                    base.noRightOptMsg("(快)提現失敗！");
                    base.Response.End();
                    return;
                }
            }
            this.Hy_DeleteCreditLock(userInfoByUID.get_u_name(), userInfoByUID.get_u_type());
            base.Un_User_Lock(rateKCByUserName.get_fgs_name());
            base.noRightOptMsg("參數錯誤！");
            base.Response.End();
        }

        private void getWithdrawInit(ref string strResult)
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
            userInfoByUID = CallBLL.cz_users_bll.GetUserInfoByUID(str2);
            if (userInfoByUID.get_u_name().Equals(this.userModel.get_u_name()))
            {
                base.Response.End();
            }
            if (!base.IsUpperLowerLevels(userInfoByUID.get_u_name(), this.userModel.get_u_type(), this.userModel.get_u_name()))
            {
                base.Response.End();
            }
            CallBLL.cz_users_bll.GetUserInfoByUName(userInfoByUID.get_sup_name());
            base.OpenLotteryMaster(out flag, out flag2);
            dictionary.Add("name", userInfoByUID.get_u_name());
            dictionary.Add("utype", base.GetUTypeText(userInfoByUID.get_u_type()));
            dictionary.Add("nicker", userInfoByUID.get_u_nicker());
            if (flag && userInfoByUID.get_six_iscash().Equals(int.Parse("1")))
            {
                dictionary.Add("recoverCreditSix", userInfoByUID.get_six_usable_credit());
                dictionary.Add("isCashSix", userInfoByUID.get_six_iscash());
                dictionary.Add("isMasteridSix", 1);
            }
            if (flag2 && userInfoByUID.get_kc_iscash().Equals(int.Parse("1")))
            {
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
                if (!(str3 == "getUserTX"))
                {
                    if (str3 == "addUserTX")
                    {
                        this.addWithdraw(ref strResult);
                    }
                }
                else
                {
                    this.getWithdrawInit(ref strResult);
                }
            }
            base.OutJson(strResult);
        }
    }
}

