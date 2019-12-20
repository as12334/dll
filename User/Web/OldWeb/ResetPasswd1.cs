namespace User.Web.OldWeb
{
    using LotterySystem.Common;
    using LotterySystem.Model;
    using System;
    using User.Web.WebBase;

    public class ResetPasswd1 : MemberPageBase
    {
        protected string skin = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            cz_userinfo_session _session = new cz_userinfo_session();
            this.skin = (this.Session[this.Session["user_name"].ToString() + "lottery_session_user_info"] as cz_userinfo_session).get_u_skin();
            string str = LSRequest.qq("hdnsubmit");
            string str2 = LSRequest.qq("txtoldpwd");
            string str3 = LSRequest.qq("txtnewpwd");
            string str4 = LSRequest.qq("txtnewpwdcf");
            if (str.Equals("submit"))
            {
                if ((string.IsNullOrEmpty(str2) || string.IsNullOrEmpty(str3)) || string.IsNullOrEmpty(str4))
                {
                    base.Response.Write("<script>alert('請輸入完整的密碼！');</script>");
                    base.Response.End();
                }
                if (str3 == str2)
                {
                    base.Response.Write("<script>alert('新密碼和舊密碼不能相同！');</script>");
                    base.Response.End();
                }
                if (str3 != str4)
                {
                    base.Response.Write("<script>alert('新密碼和確認新密碼不一致！');</script>");
                    base.Response.End();
                }
                if (!Regexlib.IsValidPassword(str3.Trim(), base.get_GetPasswordLU()))
                {
                    if (base.get_GetPasswordLU().Equals("1"))
                    {
                        base.Response.Write("<script>alert('密碼要8-20位,且必需包含大寫字母、小寫字母和数字！');</script>");
                    }
                    else
                    {
                        base.Response.Write("<script>alert('密碼要8-20位,且必需包含字母、和数字！');</script>");
                    }
                    base.Response.End();
                }
                cz_users _users = CallBLL.cz_users_bll.UserLogin(this.Session["user_name"].ToString());
                if (_users != null)
                {
                    string str5 = _users.get_salt().Trim();
                    string str6 = DESEncrypt.EncryptString(str2, str5);
                    if (_users.get_u_psw() != str6)
                    {
                        base.Response.Write("<script>alert('您輸入原密碼不正確！');</script>");
                        base.Response.End();
                    }
                    else
                    {
                        string ramSalt = Utils.GetRamSalt(6);
                        string str8 = DESEncrypt.EncryptString(str3, ramSalt);
                        if (CallBLL.cz_users_bll.UpUserPwd(this.Session["user_name"].ToString(), str8, ramSalt) > 0)
                        {
                            if (CallBLL.cz_users_bll.UpdateUserPwdStutas(this.Session["user_name"].ToString()) > 0)
                            {
                                MemberPageBase.log_user_reset_password(this.Session["user_name"].ToString(), this.Session["modifypassword"]);
                                this.Session["modifypassword"] = null;
                                base.Response.Write("<script>alert('修改密碼成功！');location.href='/Quit.aspx';</script>");
                                base.Response.End();
                            }
                            else
                            {
                                base.Response.Write("<script>alert('修改密碼不成功！');</script>");
                                base.Response.End();
                            }
                        }
                        else
                        {
                            base.Response.Write("<script>alert('修改密碼不成功！');</script>");
                            base.Response.End();
                        }
                    }
                }
            }
        }
    }
}

