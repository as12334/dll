namespace Agent.Web.Account
{
    using Agent.Web.WebBase;
    using LotterySystem.Common;
    using LotterySystem.Model;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class hy_profit : MemberPageBase
    {
        protected cz_users edit_model;
        protected string ispc = "1";
        protected bool kc_show;
        protected string lid = "";
        protected DataTable lotteryDT;
        protected bool six_show;
        protected string uid = "";
        private agent_userinfo_session userModel;
        protected double yled_kc;
        protected double yled_six;

        private void AddData()
        {
            DataTable table = CallBLL.cz_rate_six_bll.GetRateByAccount(this.edit_model.get_u_name()).Tables[0];
            string str = table.Rows[0]["fgs_name"].ToString().Trim();
            base.En_User_Lock(str);
            string s = LSRequest.qq("kc_hsed");
            string str3 = LSRequest.qq("six_hsed");
            if (this.kc_show)
            {
                try
                {
                    double.Parse(s);
                }
                catch
                {
                    if (this.ispc.Equals("1"))
                    {
                        base.Un_User_Lock(str);
                        base.DeleteCreditLock(this.edit_model.get_u_name());
                        base.Response.Write(base.ShowDialogBox("‘(快)回收額度’輸入錯誤！", "", 400));
                        base.Response.End();
                    }
                    else
                    {
                        base.Un_User_Lock(str);
                        base.DeleteCreditLock(this.edit_model.get_u_name());
                        base.Response.Write("<script>alert('‘(快)回收額度’輸入錯誤！');</script>");
                        base.Response.End();
                    }
                }
                if (double.Parse(s) < 0.0)
                {
                    if (this.ispc.Equals("1"))
                    {
                        base.Un_User_Lock(str);
                        base.DeleteCreditLock(this.edit_model.get_u_name());
                        base.Response.Write(base.ShowDialogBox("‘(快)回收額度’不能小於0！", "", 400));
                        base.Response.End();
                    }
                    else
                    {
                        base.Un_User_Lock(str);
                        base.DeleteCreditLock(this.edit_model.get_u_name());
                        base.Response.Write("<script>alert('‘(快)回收額度’不能小於0！');</script>");
                        base.Response.End();
                    }
                }
                if (double.Parse(s) > this.yled_kc)
                {
                    if (this.ispc.Equals("1"))
                    {
                        base.Un_User_Lock(str);
                        base.DeleteCreditLock(this.edit_model.get_u_name());
                        base.Response.Write(base.ShowDialogBox("‘(快)回收額度’不可高於 ‘盈利額度’！", "", 400));
                        base.Response.End();
                    }
                    else
                    {
                        base.Un_User_Lock(str);
                        base.DeleteCreditLock(this.edit_model.get_u_name());
                        base.Response.Write("<script>alert('‘(快)回收額度’不可高於 ‘盈利額度’！');</script>");
                        base.Response.End();
                    }
                }
            }
            if (this.six_show)
            {
                try
                {
                    double.Parse(str3);
                }
                catch
                {
                    if (this.ispc.Equals("1"))
                    {
                        base.Un_User_Lock(str);
                        base.DeleteCreditLock(this.edit_model.get_u_name());
                        base.Response.Write(base.ShowDialogBox("‘(⑥)回收額度’輸入錯誤！", "", 400));
                        base.Response.End();
                    }
                    else
                    {
                        base.Un_User_Lock(str);
                        base.DeleteCreditLock(this.edit_model.get_u_name());
                        base.Response.Write("<script>alert('‘(⑥)回收額度’輸入錯誤！');</script>");
                        base.Response.End();
                    }
                }
                if (double.Parse(str3) < 0.0)
                {
                    if (this.ispc.Equals("1"))
                    {
                        base.Un_User_Lock(str);
                        base.DeleteCreditLock(this.edit_model.get_u_name());
                        base.Response.Write(base.ShowDialogBox("‘(⑥)回收額度’不能小於0！", "", 400));
                        base.Response.End();
                    }
                    else
                    {
                        base.Un_User_Lock(str);
                        base.DeleteCreditLock(this.edit_model.get_u_name());
                        base.Response.Write("<script>alert('‘(⑥)回收額度’不能小於0！');</script>");
                        base.Response.End();
                    }
                }
                if (double.Parse(str3) > this.yled_six)
                {
                    if (this.ispc.Equals("1"))
                    {
                        base.Un_User_Lock(str);
                        base.DeleteCreditLock(this.edit_model.get_u_name());
                        base.Response.Write(base.ShowDialogBox("‘(⑥)回收額度’不可高於 ‘盈利額度’！", "", 400));
                        base.Response.End();
                    }
                    else
                    {
                        base.Un_User_Lock(str);
                        base.DeleteCreditLock(this.edit_model.get_u_name());
                        base.Response.Write("<script>alert('‘(⑥)回收額度’不可高於 ‘盈利額度’！');</script>");
                        base.Response.End();
                    }
                }
            }
            double kcProfit = 0.0;
            double sixProfit = 0.0;
            if (this.kc_show && this.six_show)
            {
                kcProfit = double.Parse(s);
                sixProfit = double.Parse(str3);
                if ((kcProfit <= 0.0) && (sixProfit <= 0.0))
                {
                    if (this.ispc.Equals("1"))
                    {
                        base.Un_User_Lock(str);
                        base.DeleteCreditLock(this.edit_model.get_u_name());
                        base.Response.Write(base.ShowDialogBox("‘(⑥)回收額度’或‘(快)回收額度’必須有一項大於0！", "", 400));
                        base.Response.End();
                    }
                    else
                    {
                        base.Un_User_Lock(str);
                        base.DeleteCreditLock(this.edit_model.get_u_name());
                        base.Response.Write("<script>alert('‘(⑥)回收額度’或‘(快)回收額度’必須有一項大於0！');</script>");
                        base.Response.End();
                    }
                }
            }
            if (this.kc_show && !this.six_show)
            {
                kcProfit = double.Parse(s);
                if (kcProfit <= 0.0)
                {
                    if (this.ispc.Equals("1"))
                    {
                        base.Un_User_Lock(str);
                        base.DeleteCreditLock(this.edit_model.get_u_name());
                        base.Response.Write(base.ShowDialogBox("‘(快)回收額度’必須大於0！", "", 400));
                        base.Response.End();
                    }
                    else
                    {
                        base.Un_User_Lock(str);
                        base.DeleteCreditLock(this.edit_model.get_u_name());
                        base.Response.Write("<script>alert('‘(快)回收額度’必須大於0！');</script>");
                        base.Response.End();
                    }
                }
            }
            if (!this.kc_show && this.six_show)
            {
                sixProfit = double.Parse(str3);
                if (sixProfit <= 0.0)
                {
                    if (this.ispc.Equals("1"))
                    {
                        base.Un_User_Lock(str);
                        base.DeleteCreditLock(this.edit_model.get_u_name());
                        base.Response.Write(base.ShowDialogBox("‘(⑥)回收額度’必須大於0！", "", 400));
                        base.Response.End();
                    }
                    else
                    {
                        base.Un_User_Lock(str);
                        base.DeleteCreditLock(this.edit_model.get_u_name());
                        base.Response.Write("<script>alert('‘(⑥)回收額度’必須大於0！');</script>");
                        base.Response.End();
                    }
                }
            }
            List<SqlParameter> paramList = new List<SqlParameter>();
            string str4 = base.user_hy_profit_log(this.edit_model, this.kc_show, kcProfit, double.Parse(this.edit_model.get_kc_usable_credit().ToString()), this.six_show, sixProfit, double.Parse(this.edit_model.get_six_usable_credit().ToString()), ref paramList);
            if (CallBLL.cz_users_bll.UpdateHyProfit(this.uid, this.kc_show, kcProfit, this.six_show, sixProfit, str4, paramList) != 0)
            {
                if (this.ispc.Equals("1"))
                {
                    base.Un_User_Lock(str);
                    base.DeleteCreditLock(this.edit_model.get_u_name());
                    base.Response.Write(base.ShowDialogBox("盈利額度回收成功！", base.UserReturnBackUrl, 3));
                    base.Response.End();
                }
                else
                {
                    base.Un_User_Lock(str);
                    base.DeleteCreditLock(this.edit_model.get_u_name());
                    base.Response.Write(string.Format("<script>alert('{0}');location.href='{1}';</script>", "盈利額度回收成功！", base.UserReturnBackUrl));
                    base.Response.End();
                }
            }
            else if (this.ispc.Equals("1"))
            {
                base.Un_User_Lock(str);
                base.DeleteCreditLock(this.edit_model.get_u_name());
                base.Response.Write(base.ShowDialogBox("盈利額度回收失敗！", base.UserReturnBackUrl, 400));
                base.Response.End();
            }
            else
            {
                base.Un_User_Lock(str);
                base.DeleteCreditLock(this.edit_model.get_u_name());
                base.Response.Write("<script>alert('盈利額度回收失敗！');</script>");
                base.Response.End();
            }
        }

        private void InitData()
        {
            DataRow[] rowArray = this.lotteryDT.Select(string.Format(" master_id={0}", 1));
            DataRow[] rowArray2 = this.lotteryDT.Select(string.Format(" master_id={0}", 2));
            if (rowArray.Length > 0)
            {
                this.six_show = true;
            }
            if (rowArray2.Length > 0)
            {
                this.kc_show = true;
            }
            if (this.kc_show)
            {
                this.yled_kc = double.Parse(this.edit_model.get_kc_usable_credit().ToString()) - double.Parse(this.edit_model.get_kc_credit().ToString());
                if (this.yled_kc < 0.0)
                {
                    this.yled_kc = 0.0;
                }
            }
            if (this.six_show)
            {
                this.yled_six = double.Parse(this.edit_model.get_six_usable_credit().ToString()) - double.Parse(this.edit_model.get_six_credit().ToString());
                if (this.yled_six < 0.0)
                {
                    this.yled_six = 0.0;
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string str = this.Session["user_name"].ToString();
            this.userModel = this.Session[str + "lottery_session_user_info"] as agent_userinfo_session;
            this.lotteryDT = base.GetLotteryList();
            this.uid = LSRequest.qq("uid");
            this.lid = LSRequest.qq("lid");
            this.ispc = LSRequest.qq("isPc");
            this.edit_model = CallBLL.cz_users_bll.GetUserInfoByUID(this.uid, "hy");
            if (!this.edit_model.get_kc_iscash().Equals(int.Parse("0")))
            {
                base.Response.End();
            }
            base.Permission_Aspx_ZJ(this.userModel, "po_2_1");
            base.Permission_Aspx_DL(this.userModel, "po_6_1");
            if (!base.IsUpperLowerLevels(this.edit_model.get_u_name(), this.userModel.get_u_type(), this.userModel.get_u_name()))
            {
                if (this.ispc.Equals("1"))
                {
                    base.Response.Redirect("../MessagePage.aspx?code=u100057&url=&issuccess=1&isback=0&isopen=1");
                    base.Response.End();
                }
                else
                {
                    base.Response.Redirect("../MessagePage.aspx?code=u100057&url=&issuccess=1&isback=0&isopen=0");
                    base.Response.End();
                }
            }
            this.InitData();
            if (!this.edit_model.get_six_iscash().Equals(1))
            {
                this.six_show = false;
            }
            this.six_show = false;
            if (LSRequest.qq("addprofit").Equals("add"))
            {
                if (!base.IsCreditLock(this.edit_model.get_u_name()))
                {
                    base.Response.Write(base.ShowDialogBox("系統繁忙，請稍後！", null, 400));
                    base.Response.End();
                }
                this.AddData();
                base.DeleteCreditLock(this.edit_model.get_u_name());
            }
        }
    }
}

