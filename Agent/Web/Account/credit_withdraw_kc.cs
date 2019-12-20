namespace Agent.Web.Account
{
    using Agent.Web.WebBase;
    using LotterySystem.Common;
    using LotterySystem.Model;
    using System;
    using System.Data;

    public class credit_withdraw_kc : MemberPageBase
    {
        protected bool bol_masterid;
        protected cz_users edit_model;
        protected string ispc = "1";
        protected DataTable lotteryDT;
        protected string mlid = "";
        protected string uid = "";
        protected cz_users up_edit_model;
        protected agent_userinfo_session userModel;

        private void Hy_DeleteCreditLock()
        {
            if (this.edit_model.get_u_type().Equals("hy"))
            {
                base.DeleteCreditLock(this.edit_model.get_u_name());
            }
        }

        private void Hy_SubmitCreditLock()
        {
            if (this.edit_model.get_u_type().Equals("hy") && !base.IsCreditLock(this.edit_model.get_u_name()))
            {
                base.Response.Write(base.ShowDialogBox("系統繁忙，請稍後！", null, 400));
                base.Response.End();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string str = this.Session["user_name"].ToString();
            this.userModel = this.Session[str + "lottery_session_user_info"] as agent_userinfo_session;
            this.lotteryDT = base.GetLotteryList();
            if (base.GetLotteryMasterID(this.lotteryDT).Split(new char[] { ',' }).Length > 1)
            {
                this.bol_masterid = true;
            }
            if (!base.GetMasterLotteryIsOpen(this.lotteryDT, 2))
            {
                base.Response.End();
            }
            this.uid = LSRequest.qq("uid");
            this.mlid = LSRequest.qq("mlid");
            this.ispc = LSRequest.qq("isPc");
            this.mlid = 2.ToString();
            this.edit_model = CallBLL.cz_users_bll.GetUserInfoByUID(this.uid);
            if (!this.edit_model.get_kc_iscash().Equals(int.Parse("1")))
            {
                base.Response.End();
            }
            base.Permission_Aspx_ZJ(this.userModel, "po_2_1");
            base.Permission_Aspx_DL(this.userModel, "po_6_1");
            if (this.edit_model.get_u_name().Equals(this.userModel.get_u_name()))
            {
                base.Response.Redirect("/MessagePage.aspx?code=u100057&url=&issuccess=1&isback=0&isopen=0");
                base.Response.End();
            }
            if (!base.IsUpperLowerLevels(this.edit_model.get_u_name(), this.userModel.get_u_type(), this.userModel.get_u_name()))
            {
                if (this.ispc.Equals("1"))
                {
                    base.Response.Redirect("/MessagePage.aspx?code=u100057&url=&issuccess=1&isback=0&isopen=1");
                    base.Response.End();
                }
                else
                {
                    base.Response.Redirect("/MessagePage.aspx?code=u100057&url=&issuccess=1&isback=0&isopen=0");
                    base.Response.End();
                }
            }
            this.up_edit_model = CallBLL.cz_users_bll.GetUserInfoByUName(this.edit_model.get_sup_name());
            string str3 = LSRequest.qq("hdn_withdraw");
            string s = LSRequest.qq("txt_withdraw");
            if (str3.Equals("rhrg"))
            {
                this.Hy_SubmitCreditLock();
                cz_rate_kc rateKCByUserName = CallBLL.cz_rate_kc_bll.GetRateKCByUserName(this.edit_model.get_u_name());
                base.En_User_Lock(rateKCByUserName.get_fgs_name());
                this.up_edit_model = CallBLL.cz_users_bll.GetUserInfoByUName(this.edit_model.get_sup_name());
                if (!Utils.IsDouble(s))
                {
                    if (this.ispc.Equals("1"))
                    {
                        base.Un_User_Lock(rateKCByUserName.get_fgs_name());
                        this.Hy_DeleteCreditLock();
                        base.Response.Write(base.ShowDialogBox("(快)提現格式不正確！", "", 400));
                        base.Response.End();
                    }
                    else
                    {
                        base.Un_User_Lock(rateKCByUserName.get_fgs_name());
                        this.Hy_DeleteCreditLock();
                        base.Response.Write(string.Format("<script>alert('{0}');</script>", "(快)提現格式不正確！"));
                        base.Response.End();
                    }
                }
                if (double.Parse(s) < 1.0)
                {
                    if (this.ispc.Equals("1"))
                    {
                        base.Un_User_Lock(rateKCByUserName.get_fgs_name());
                        this.Hy_DeleteCreditLock();
                        base.Response.Write(base.ShowDialogBox("(快)提現金額不能小於1！", "", 400));
                        base.Response.End();
                    }
                    else
                    {
                        base.Un_User_Lock(rateKCByUserName.get_fgs_name());
                        this.Hy_DeleteCreditLock();
                        base.Response.Write(string.Format("<script>alert('{0}');</script>", "(快)提現金額不能小於1"));
                        base.Response.End();
                    }
                }
                if (double.Parse(s) > double.Parse(this.edit_model.get_kc_usable_credit().ToString()))
                {
                    if (this.ispc.Equals("1"))
                    {
                        base.Un_User_Lock(rateKCByUserName.get_fgs_name());
                        this.Hy_DeleteCreditLock();
                        base.Response.Write(base.ShowDialogBox(string.Format("(快)提現金額不能大於{0}！", Utils.GetMathRound(double.Parse(this.edit_model.get_kc_usable_credit().ToString()), null)), "", 400));
                        base.Response.End();
                    }
                    else
                    {
                        base.Un_User_Lock(rateKCByUserName.get_fgs_name());
                        this.Hy_DeleteCreditLock();
                        base.Response.Write(string.Format("<script>alert('(快)提現金額不能大於{0}！');</script>", Utils.GetMathRound(double.Parse(this.edit_model.get_kc_usable_credit().ToString()), null)));
                        base.Response.End();
                    }
                }
                if (CallBLL.cz_users_bll.UserWithdraw_kc(this.edit_model, this.up_edit_model, double.Parse(s), int.Parse(this.mlid), this.userModel.get_u_name(), (this.userModel.get_users_child_session() != null) ? this.userModel.get_users_child_session().get_u_name() : "", 0) > 0)
                {
                    if (this.ispc.Equals("1"))
                    {
                        base.Un_User_Lock(rateKCByUserName.get_fgs_name());
                        this.Hy_DeleteCreditLock();
                        base.Response.Write(base.ShowDialogBox("(快)提現成功！", base.UserReturnBackUrl, 3));
                        base.Response.End();
                    }
                    else
                    {
                        base.Un_User_Lock(rateKCByUserName.get_fgs_name());
                        this.Hy_DeleteCreditLock();
                        base.Response.Write(string.Format("<script>alert('{0}');location.href='{1}';</script>", "(快)提現成功！", base.UserReturnBackUrl));
                        base.Response.End();
                    }
                }
                else if (this.ispc.Equals("1"))
                {
                    base.Un_User_Lock(rateKCByUserName.get_fgs_name());
                    this.Hy_DeleteCreditLock();
                    base.Response.Write(base.ShowDialogBox("(快)提現失敗！", "", 400));
                    base.Response.End();
                }
                else
                {
                    base.Un_User_Lock(rateKCByUserName.get_fgs_name());
                    this.Hy_DeleteCreditLock();
                    base.Response.Write(string.Format("<script>alert('{0}');</script>", "(快)提現失敗！"));
                    base.Response.End();
                }
            }
        }
    }
}

