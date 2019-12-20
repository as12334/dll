namespace Agent.Web
{
    using Agent.Web.WebBase;
    using LotterySystem.Common;
    using LotterySystem.Model;
    using System;

    public class ResetPasswd : MemberPageBase
    {
        protected string skin = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            agent_userinfo_session _session = this.Session[this.Session["user_name"].ToString() + "lottery_session_user_info"] as agent_userinfo_session;
            this.skin = _session.get_u_skin();
            string str = LSRequest.qq("hdnsubmit");
            string str2 = LSRequest.qq("txtoldpwd");
            string str3 = LSRequest.qq("txtnewpwd");
            string str4 = LSRequest.qq("txtnewpwdcf");
            if (str.Equals("submit"))
            {
                if ((string.IsNullOrEmpty(str2) || string.IsNullOrEmpty(str3)) || string.IsNullOrEmpty(str4))
                {
                    base.Response.Write(base.GetAlert("請輸入完整的密碼！"));
                    base.Response.End();
                }
                if (str3 == str2)
                {
                    base.Response.Write(base.GetAlert("新密碼和舊密碼不能相同！"));
                    base.Response.End();
                }
                if (str3 != str4)
                {
                    base.Response.Write(base.GetAlert("新密碼和確認新密碼不一致！"));
                    base.Response.End();
                }
                if (!Regexlib.IsValidPassword(str3.Trim(), base.get_GetPasswordLU()))
                {
                    if (base.get_GetPasswordLU().Equals("1"))
                    {
                        base.Response.Write(base.GetAlert("密碼要8-20位,且必需包含大寫字母、小寫字母和数字！"));
                        base.Response.End();
                    }
                    else
                    {
                        base.Response.Write(base.GetAlert("密碼要8-20位,且必需包含字母、和数字！"));
                        base.Response.End();
                    }
                    base.Response.End();
                }
                if (_session.get_users_child_session() == null)
                {
                    cz_users _users = CallBLL.cz_users_bll.AgentLogin(this.Session["user_name"].ToString());
                    if (_users != null)
                    {
                        string str5 = _users.get_salt().Trim();
                        string str6 = DESEncrypt.EncryptString(str2, str5);
                        if (_users.get_u_psw() != str6)
                        {
                            base.Response.Write(base.GetAlert("您輸入原密碼不正確！"));
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
                                    base.log_user_reset_password(this.Session["user_name"].ToString(), this.Session["user_name"].ToString(), "", this.Session["modifypassword"]);
                                    this.Session["modifypassword"] = null;
                                    string url = "Quit.aspx";
                                    base.Response.Write(base.GetAlert("修改密碼成功"));
                                    base.Response.Write(base.LocationHref(url));
                                    base.Response.End();
                                }
                                else
                                {
                                    base.Response.Write(base.GetAlert("修改密碼不成功！"));
                                    base.Response.End();
                                }
                            }
                            else
                            {
                                base.Response.Write(base.GetAlert("修改密碼不成功！"));
                                base.Response.End();
                            }
                        }
                    }
                }
                else
                {
                    cz_users_child _child = CallBLL.cz_users_child_bll.AgentLogin(this.Session["child_user_name"].ToString().ToLower());
                    if (_child != null)
                    {
                        string str10 = _child.get_salt().Trim();
                        string str11 = DESEncrypt.EncryptString(str2, str10);
                        if (_child.get_u_psw() != str11)
                        {
                            base.Response.Write(base.GetAlert("您輸入原密碼不正確！"));
                            base.Response.End();
                        }
                        else
                        {
                            string str12 = Utils.GetRamSalt(6);
                            string str13 = DESEncrypt.EncryptString(str3, str12);
                            if (CallBLL.cz_users_child_bll.UpUserPwd(this.Session["child_user_name"].ToString(), str13, str12) > 0)
                            {
                                if (CallBLL.cz_users_child_bll.UpdateUserPwdStutas(this.Session["child_user_name"].ToString()) > 0)
                                {
                                    base.log_user_reset_password(this.Session["child_user_name"].ToString(), this.Session["child_user_name"].ToString(), "", this.Session["modifypassword"]);
                                    this.Session["modifypassword"] = null;
                                    string str14 = "Quit.aspx";
                                    base.Response.Write(base.GetAlert("修改密碼成功！"));
                                    base.Response.Write(base.LocationHref(str14));
                                    base.Response.End();
                                }
                                else
                                {
                                    base.Response.Write(base.GetAlert("修改密碼不成功！"));
                                    base.Response.End();
                                }
                            }
                            else
                            {
                                base.Response.Write(base.GetAlert("修改密碼不成功！"));
                                base.Response.End();
                            }
                        }
                    }
                }
            }
        }
    }
}

