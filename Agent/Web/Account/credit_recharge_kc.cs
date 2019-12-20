namespace Agent.Web.Account
{
    using Agent.Web.WebBase;
    using LotterySystem.Common;
    using LotterySystem.Model;
    using System;
    using System.Data;

    public class credit_recharge_kc : MemberPageBase
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
            this.uid = LSRequest.qq("uid");
            this.mlid = LSRequest.qq("mlid");
            this.ispc = LSRequest.qq("isPc");
            this.mlid = 2.ToString();
            this.edit_model = CallBLL.cz_users_bll.GetUserInfoByUID(this.uid);
            if (!this.edit_model.get_kc_iscash().Equals(int.Parse("1")))
            {
                base.Response.End();
            }
            if (!base.GetMasterLotteryIsOpen(this.lotteryDT, 2))
            {
                base.Response.End();
            }
            base.Permission_Aspx_ZJ(this.userModel, "po_2_1");
            base.Permission_Aspx_DL(this.userModel, "po_6_1");
            if (this.edit_model.get_u_name().Equals(this.userModel.get_u_name()))
            {
                base.Response.Redirect("/MessagePage.aspx?code=u100057&url=&issuccess=1&isback=0&isopen=1");
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
            string str3 = LSRequest.qq("hdn_recharge");
            string s = LSRequest.qq("txt_recharge");
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
                        base.Response.Write(base.ShowDialogBox("(快)充值格式不正確！", "", 400));
                        base.Response.End();
                    }
                    else
                    {
                        base.Un_User_Lock(rateKCByUserName.get_fgs_name());
                        this.Hy_DeleteCreditLock();
                        base.Response.Write(string.Format("<script>alert('{0}');</script>", "(快)充值格式不正確！"));
                        base.Response.End();
                    }
                }
                if (double.Parse(s) < 1.0)
                {
                    if (this.ispc.Equals("1"))
                    {
                        base.Un_User_Lock(rateKCByUserName.get_fgs_name());
                        this.Hy_DeleteCreditLock();
                        base.Response.Write(base.ShowDialogBox("(快)充值金額不能小於1！", "", 400));
                        base.Response.End();
                    }
                    else
                    {
                        base.Un_User_Lock(rateKCByUserName.get_fgs_name());
                        this.Hy_DeleteCreditLock();
                        base.Response.Write(string.Format("<script>alert('{0}');</script>", "(快)充值金額不能小於1！"));
                        base.Response.End();
                    }
                }
                if (!this.up_edit_model.get_u_type().Equals("zj"))
                {
                    decimal num3 = decimal.Parse(s);
                    if (num3 > this.up_edit_model.get_kc_usable_credit())
                    {
                        if (this.ispc.Equals("1"))
                        {
                            base.Un_User_Lock(rateKCByUserName.get_fgs_name());
                            this.Hy_DeleteCreditLock();
                            base.Response.Write(base.ShowDialogBox("(快)充值金額不能大於 “上級可用餘額” ！", "", 400));
                            base.Response.End();
                        }
                        else
                        {
                            base.Un_User_Lock(rateKCByUserName.get_fgs_name());
                            this.Hy_DeleteCreditLock();
                            base.Response.Write(string.Format("<script>alert('{0}');</script>", "(快)充值金額不能大於 “上級可用餘額” ！"));
                            base.Response.End();
                        }
                    }
                }
                string random = Utils.GetRandom();
                cz_credit_flow _flow = new cz_credit_flow();
                _flow.set_master_id(new int?(int.Parse(this.mlid)));
                _flow.set_credit_old(this.edit_model.get_kc_usable_credit());
                decimal? nullable3 = this.edit_model.get_kc_usable_credit();
                decimal num4 = decimal.Parse(s);
                _flow.set_credit_new(nullable3.HasValue ? new decimal?(nullable3.GetValueOrDefault() + num4) : null);
                _flow.set_credit_change(new decimal?(decimal.Parse(s)));
                _flow.set_u_name(this.edit_model.get_u_name());
                _flow.set_iscash(new int?(int.Parse("1")));
                _flow.set_operator_name(this.userModel.get_u_name());
                if (this.userModel.get_users_child_session() != null)
                {
                    _flow.set_operator_child_name(this.userModel.get_users_child_session().get_u_name());
                }
                _flow.set_operator_time(DateTime.Now);
                _flow.set_flag(0);
                _flow.set_checkcode(random);
                _flow.set_isphone(0);
                _flow.set_ip(LSRequest.GetIP());
                cz_credit_flow _flow2 = new cz_credit_flow();
                _flow2.set_master_id(new int?(int.Parse(this.mlid)));
                _flow2.set_credit_old(this.up_edit_model.get_kc_usable_credit());
                decimal? nullable5 = this.up_edit_model.get_kc_usable_credit();
                decimal num5 = decimal.Parse(s);
                _flow2.set_credit_new(nullable5.HasValue ? new decimal?(nullable5.GetValueOrDefault() - num5) : null);
                _flow2.set_credit_change(new decimal?(-decimal.Parse(s)));
                _flow2.set_u_name(this.up_edit_model.get_u_name());
                _flow2.set_iscash(new int?(int.Parse("1")));
                _flow2.set_operator_name(this.userModel.get_u_name());
                if (this.userModel.get_users_child_session() != null)
                {
                    _flow2.set_operator_child_name(this.userModel.get_users_child_session().get_u_name());
                }
                _flow2.set_operator_time(DateTime.Now);
                _flow2.set_flag(1);
                _flow2.set_checkcode(random);
                _flow2.set_note(this.edit_model.get_u_name());
                _flow2.set_isphone(0);
                _flow2.set_ip(LSRequest.GetIP());
                if (CallBLL.cz_users_bll.UserRecharge(this.up_edit_model.get_u_type(), this.up_edit_model.get_u_id(), this.edit_model.get_u_id(), double.Parse(s), _flow, _flow2) > 0)
                {
                    if (this.ispc.Equals("1"))
                    {
                        base.Un_User_Lock(rateKCByUserName.get_fgs_name());
                        this.Hy_DeleteCreditLock();
                        base.Response.Write(base.ShowDialogBox("(快)充值成功！", base.UserReturnBackUrl, 3));
                        base.Response.End();
                    }
                    else
                    {
                        base.Un_User_Lock(rateKCByUserName.get_fgs_name());
                        this.Hy_DeleteCreditLock();
                        base.Response.Write(string.Format("<script>alert('{0}');location.href='{1}';</script>", "(快)充值成功！", base.UserReturnBackUrl));
                        base.Response.End();
                    }
                }
                else if (this.ispc.Equals("1"))
                {
                    base.Un_User_Lock(rateKCByUserName.get_fgs_name());
                    this.Hy_DeleteCreditLock();
                    base.Response.Write(base.ShowDialogBox("(快)充值失敗！", "", 400));
                    base.Response.End();
                }
                else
                {
                    base.Un_User_Lock(rateKCByUserName.get_fgs_name());
                    this.Hy_DeleteCreditLock();
                    base.Response.Write(string.Format("<script>alert('{0}');</script>", "(快)充值失敗！"));
                    base.Response.End();
                }
            }
        }
    }
}

