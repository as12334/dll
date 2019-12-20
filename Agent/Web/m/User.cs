namespace Agent.Web.M
{
    using Agent.Web.WebBase;
    using LotterySystem.Common;
    using LotterySystem.Model;
    using LotterySystem.WebPageBase;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.Linq;
    using System.Web;

    public class User : MemberPageBase_Mobile
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            base.Response.Expires = 0;
            base.Response.CacheControl = "no-cache";
            string str = LSRequest.qq("action").Trim();
            string strResult = "";
            string str3 = str;
            if (str3 != null)
            {
                if (!(str3 == "userLogin"))
                {
                    if (str3 == "userLogout")
                    {
                        this.userLogout(ref strResult);
                    }
                    else if (str3 == "userEditPwd")
                    {
                        this.userEditPwd(ref strResult);
                    }
                    else if (str3 == "userLoginLog")
                    {
                        this.userLoginLog(ref strResult);
                    }
                }
                else
                {
                    this.userLogin(HttpContext.Current, ref strResult);
                }
            }
            base.OutJson(strResult);
        }

        private void userEditPwd(ref string strResult)
        {
            base.checkLoginByHandler(0);
            ReturnResult result = new ReturnResult();
            agent_userinfo_session _session = this.Session[this.Session["user_name"].ToString() + "lottery_session_user_info"] as agent_userinfo_session;
            string str = LSRequest.qq("txtoldpwd");
            string str2 = LSRequest.qq("txtnewpwd");
            string str3 = LSRequest.qq("txtnewpwdcf");
            if ((string.IsNullOrEmpty(str) || string.IsNullOrEmpty(str2)) || string.IsNullOrEmpty(str3))
            {
                result.set_tipinfo("請輸入完整的密碼！");
                result.set_success(400);
                strResult = base.ObjectToJson(result);
            }
            else if (str2 == str)
            {
                result.set_tipinfo("新密碼和舊密碼不能相同！");
                result.set_success(400);
                strResult = base.ObjectToJson(result);
            }
            else if (str2 != str3)
            {
                result.set_tipinfo("新密碼和確認新密碼不一致！");
                result.set_success(400);
                strResult = base.ObjectToJson(result);
            }
            else if (!Regexlib.IsValidPassword(str2.Trim(), base.get_GetPasswordLU()))
            {
                if (base.get_GetPasswordLU().Equals("1"))
                {
                    result.set_tipinfo("密碼要8-20位,且必需包含大寫字母、小寫字母和数字！");
                    result.set_success(400);
                    strResult = base.ObjectToJson(result);
                }
                else
                {
                    result.set_tipinfo("密碼要8-20位,且必需包含字母、和数字！");
                    result.set_success(400);
                    strResult = base.ObjectToJson(result);
                }
            }
            else if (_session.get_users_child_session() == null)
            {
                cz_users _users = CallBLL.cz_users_bll.AgentLogin(this.Session["user_name"].ToString());
                if (_users != null)
                {
                    string str4 = _users.get_salt().Trim();
                    string str5 = DESEncrypt.EncryptString(str, str4);
                    if (_users.get_u_psw() != str5)
                    {
                        result.set_tipinfo("您輸入原密碼不正確！");
                        result.set_success(400);
                        strResult = base.ObjectToJson(result);
                    }
                    else
                    {
                        string ramSalt = Utils.GetRamSalt(6);
                        if (CallBLL.cz_users_bll.UpUserPwd(this.Session["user_name"].ToString(), DESEncrypt.EncryptString(str2, ramSalt), ramSalt) > 0)
                        {
                            int num = CallBLL.cz_users_bll.UpdateUserPwdStutas(this.Session["user_name"].ToString());
                            base.log_user_reset_password(this.Session["user_name"].ToString(), this.Session["user_name"].ToString(), "", this.Session["modifypassword"]);
                            this.Session["modifypassword"] = null;
                            this.Session.Abandon();
                            result.set_tipinfo("修改密碼成功！");
                            result.set_success(210);
                            strResult = base.ObjectToJson(result);
                        }
                        else
                        {
                            result.set_tipinfo("修改密碼失敗！");
                            result.set_success(400);
                            strResult = base.ObjectToJson(result);
                        }
                    }
                }
            }
            else
            {
                cz_users_child _child = CallBLL.cz_users_child_bll.AgentLogin(this.Session["child_user_name"].ToString().ToLower());
                if (_child != null)
                {
                    string str7 = _child.get_salt().Trim();
                    string str8 = DESEncrypt.EncryptString(str, str7);
                    if (_child.get_u_psw() != str8)
                    {
                        result.set_tipinfo("您輸入原密碼不正確！");
                        result.set_success(400);
                        strResult = base.ObjectToJson(result);
                    }
                    else
                    {
                        string str9 = Utils.GetRamSalt(6);
                        if (CallBLL.cz_users_child_bll.UpUserPwd(this.Session["child_user_name"].ToString(), DESEncrypt.EncryptString(str2, str9), str9) > 0)
                        {
                            int num2 = CallBLL.cz_users_bll.UpdateUserPwdStutas(this.Session["child_user_name"].ToString().ToLower());
                            base.log_user_reset_password(this.Session["child_user_name"].ToString(), this.Session["child_user_name"].ToString(), "", this.Session["modifypassword"]);
                            this.Session["modifypassword"] = null;
                            this.Session.Abandon();
                            result.set_tipinfo("修改密碼成功！");
                            result.set_success(210);
                            strResult = base.ObjectToJson(result);
                        }
                        else
                        {
                            result.set_tipinfo("修改密碼失敗！");
                            result.set_success(400);
                            strResult = base.ObjectToJson(result);
                        }
                    }
                }
            }
        }

        private void userLogin(HttpContext context, ref string strResult)
        {
            DateTime? nullable12;
            DateTime time4;
            string str = "";
            string str2 = "";
            string str3 = "";
            string str4 = "";
            ReturnResult result = new ReturnResult();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("type", "user_login");
            string str5 = LSRequest.qq("loginName").Trim().ToLower();
            string str6 = LSRequest.qq("loginPwd").Trim();
            string str7 = LSRequest.qq("ValidateCode").Trim();
            if (PageBase.is_ip_locked())
            {
                context.Session["lottery_session_img_code"] = null;
                result.set_success(400);
                result.set_tipinfo("由於輸入錯誤次數過多,您已被禁用,請稍後再試!");
                strResult = JsonHandle.ObjectToJson(result);
                return;
            }
            if (string.IsNullOrEmpty(str5) || string.IsNullOrEmpty(str6))
            {
                context.Response.End();
                return;
            }
            if (int.Parse(FileCacheHelper.get_GetLockedPasswordCount()) == 0)
            {
                context.Session["lottery_session_img_code_display"] = 1;
            }
            if (context.Session["lottery_session_img_code_display"] == null)
            {
                if (CallBLL.cz_user_psw_err_log_bll.IsExistUser(str5))
                {
                    DateTime time;
                    if (PageBase.IsErrTimesAbove(ref time, str5))
                    {
                        if (!PageBase.IsErrTimeout(time))
                        {
                            context.Session["lottery_session_img_code"] = null;
                            result.set_success(400);
                            result.set_tipinfo("");
                            dictionary.Add("isDisplayCode", 1);
                            result.set_data(dictionary);
                            strResult = JsonHandle.ObjectToJson(result);
                            context.Session["lottery_session_img_code_display"] = 1;
                            return;
                        }
                        CallBLL.cz_user_psw_err_log_bll.ZeroErrTimes(str5);
                        context.Session["lottery_session_img_code"] = null;
                        context.Session["lottery_session_img_code_display"] = 0;
                    }
                    else
                    {
                        context.Session["lottery_session_img_code"] = null;
                        context.Session["lottery_session_img_code_display"] = 0;
                    }
                }
                else
                {
                    context.Session["lottery_session_img_code"] = null;
                    context.Session["lottery_session_img_code_display"] = 0;
                }
            }
            if (context.Session["lottery_session_img_code_display"].ToString() == "0")
            {
                if (string.IsNullOrEmpty(str5) || string.IsNullOrEmpty(str6))
                {
                    context.Response.End();
                    return;
                }
            }
            else
            {
                if (string.IsNullOrEmpty(str5) || string.IsNullOrEmpty(str6))
                {
                    context.Response.End();
                    return;
                }
                if (string.IsNullOrEmpty(str7))
                {
                    context.Session["lottery_session_img_code"] = null;
                    result.set_success(410);
                    result.set_tipinfo(PageBase.GetMessageByCache("u100001", "MessageHint"));
                    dictionary.Add("isDisplayCode", 1);
                    result.set_data(dictionary);
                    strResult = JsonHandle.ObjectToJson(result);
                    context.Session["lottery_session_img_code_display"] = 1;
                    return;
                }
                if (context.Session["lottery_session_img_code"] == null)
                {
                    result.set_success(410);
                    result.set_tipinfo(PageBase.GetMessageByCache("u100001", "MessageHint"));
                    strResult = JsonHandle.ObjectToJson(result);
                    return;
                }
                if (context.Session["lottery_session_img_code"].ToString().ToLower() != str7.ToLower())
                {
                    context.Session["lottery_session_img_code"] = null;
                    result.set_success(410);
                    result.set_tipinfo(PageBase.GetMessageByCache("u100001", "MessageHint"));
                    strResult = JsonHandle.ObjectToJson(result);
                    return;
                }
            }
            cz_users _users = CallBLL.cz_users_bll.AgentLogin(str5.ToLower());
            cz_users_child _child = null;
            if (_users == null)
            {
                _child = CallBLL.cz_users_child_bll.AgentLogin(str5.ToLower());
                if (_child != null)
                {
                    string str9 = _child.get_retry_times().ToString();
                    if (!string.IsNullOrEmpty(str9) && (int.Parse(str9) > int.Parse(FileCacheHelper.get_GetLockedUserCount())))
                    {
                        if (!PageBase.IsLockedTimeout(str5, "child"))
                        {
                            context.Session["lottery_session_img_code"] = null;
                            result.set_success(560);
                            result.set_tipinfo("您的帳號因密碼多次輸入錯誤被鎖死,請與管理員聯系!");
                            strResult = JsonHandle.ObjectToJson(result);
                            return;
                        }
                        PageBase.zero_retry_times_children(str5);
                    }
                    string str10 = _child.get_salt().Trim();
                    string str11 = DESEncrypt.EncryptString(str6, str10);
                    if (_child.get_u_psw() != str11)
                    {
                        context.Session["lottery_session_img_code"] = null;
                        PageBase.inc_retry_times_children(str5);
                        PageBase.login_error_ip();
                        result.set_success(400);
                        result.set_tipinfo(PageBase.GetMessageByCache("u100003", "MessageHint"));
                        strResult = JsonHandle.ObjectToJson(result);
                        if (context.Session["lottery_session_img_code_display"].ToString() == "0")
                        {
                            DateTime time2;
                            if (CallBLL.cz_user_psw_err_log_bll.IsExistUser(str5))
                            {
                                CallBLL.cz_user_psw_err_log_bll.UpdateErrTimes(str5);
                            }
                            else
                            {
                                CallBLL.cz_user_psw_err_log_bll.AddUser(str5);
                            }
                            if (PageBase.IsErrTimesAbove(ref time2, str5))
                            {
                                context.Session["lottery_session_img_code"] = null;
                                result.set_success(400);
                                result.set_tipinfo(PageBase.GetMessageByCache("u100003", "MessageHint"));
                                dictionary.Add("isDisplayCode", 1);
                                result.set_data(dictionary);
                                strResult = JsonHandle.ObjectToJson(result);
                                context.Session["lottery_session_img_code_display"] = 1;
                            }
                        }
                        return;
                    }
                    str2 = _child.get_status().ToString();
                    str3 = PageBase.upper_user_status(_child.get_parent_u_name());
                    _users = CallBLL.cz_users_bll.AgentLogin(_child.get_parent_u_name());
                }
                else
                {
                    context.Session["lottery_session_img_code"] = null;
                    PageBase.login_error_ip();
                    result.set_success(400);
                    result.set_tipinfo(PageBase.GetMessageByCache("u100002", "MessageHint"));
                    dictionary.Add("fs_name", "loginName");
                    strResult = JsonHandle.ObjectToJson(result);
                    return;
                }
                PageBase.zero_retry_times_children(str5);
            }
            else
            {
                string str12 = _users.get_retry_times().ToString();
                if (!string.IsNullOrEmpty(str12) && (int.Parse(str12) > int.Parse(FileCacheHelper.get_GetLockedUserCount())))
                {
                    if (!PageBase.IsLockedTimeout(str5, "master"))
                    {
                        context.Session["lottery_session_img_code"] = null;
                        result.set_success(400);
                        result.set_tipinfo("您的帳號因密碼多次輸入錯誤被鎖死,請與管理員聯系!");
                        strResult = JsonHandle.ObjectToJson(result);
                        return;
                    }
                    PageBase.zero_retry_times(str5);
                }
                str = _users.get_a_state().ToString();
                string str13 = _users.get_a_state().ToString();
                str4 = PageBase.upper_user_status(_users.get_u_name());
                if (str13.Equals("2"))
                {
                    context.Session["lottery_session_img_code"] = null;
                    result.set_success(400);
                    result.set_tipinfo(PageBase.GetMessageByCache("u100005", "MessageHint"));
                    dictionary.Add("fs_name", "loginName");
                    strResult = JsonHandle.ObjectToJson(result);
                    context.Session.Abandon();
                    return;
                }
                if (str4 == "2")
                {
                    context.Session["lottery_session_img_code"] = null;
                    result.set_success(400);
                    result.set_tipinfo("您的上級帳號已被停用,请与管理员联系!");
                    strResult = JsonHandle.ObjectToJson(result);
                    context.Session.Abandon();
                    return;
                }
                str = (str13 == null) ? "0" : str;
                string str14 = _users.get_salt().Trim();
                string str15 = DESEncrypt.EncryptString(str6, str14);
                if (_users.get_u_psw() != str15)
                {
                    context.Session["lottery_session_img_code"] = null;
                    PageBase.inc_retry_times(str5);
                    PageBase.login_error_ip();
                    result.set_success(400);
                    result.set_tipinfo(PageBase.GetMessageByCache("u100003", "MessageHint"));
                    strResult = JsonHandle.ObjectToJson(result);
                    if (context.Session["lottery_session_img_code_display"].ToString() == "0")
                    {
                        DateTime time3;
                        if (CallBLL.cz_user_psw_err_log_bll.IsExistUser(str5))
                        {
                            CallBLL.cz_user_psw_err_log_bll.UpdateErrTimes(str5);
                        }
                        else
                        {
                            CallBLL.cz_user_psw_err_log_bll.AddUser(str5);
                        }
                        if (PageBase.IsErrTimesAbove(ref time3, str5))
                        {
                            context.Session["lottery_session_img_code"] = null;
                            result.set_success(400);
                            result.set_tipinfo(PageBase.GetMessageByCache("u100003", "MessageHint"));
                            dictionary.Add("isDisplayCode", 1);
                            result.set_data(dictionary);
                            strResult = JsonHandle.ObjectToJson(result);
                            context.Session["lottery_session_img_code_display"] = 1;
                        }
                    }
                    return;
                }
                PageBase.zero_retry_times(str5);
            }
            if (!string.IsNullOrEmpty(str2))
            {
                context.Session["user_name"] = _users.get_u_name().Trim();
                context.Session["user_type"] = _users.get_u_type().Trim();
                context.Session["child_user_name"] = _child.get_u_name().Trim();
                context.Session["user_state"] = str2.Trim();
                if (str2.Equals("2"))
                {
                    context.Session["lottery_session_img_code"] = null;
                    result.set_success(400);
                    result.set_tipinfo("您的帳號已被停用,请与管理员联系!");
                    strResult = JsonHandle.ObjectToJson(result);
                    context.Session.Abandon();
                    return;
                }
                if (_users.get_a_state() == 2)
                {
                    context.Session["lottery_session_img_code"] = null;
                    result.set_success(400);
                    result.set_tipinfo("您的主帳號已被停用,请与管理员联系!");
                    strResult = JsonHandle.ObjectToJson(result);
                    context.Session.Abandon();
                    return;
                }
                if (str3 == "2")
                {
                    context.Session["lottery_session_img_code"] = null;
                    result.set_success(400);
                    result.set_tipinfo("您的上級帳號已被停用,请与管理员联系!");
                    strResult = JsonHandle.ObjectToJson(result);
                    context.Session.Abandon();
                    return;
                }
                if (str2 == "1")
                {
                    result.set_success(200);
                    result.set_tipinfo(PageBase.GetMessageByCache("u100004", "MessageHint"));
                    strResult = JsonHandle.ObjectToJson(result);
                    context.Session["user_state"] = str2;
                }
                else if (_users.get_a_state() == 1)
                {
                    result.set_success(200);
                    result.set_tipinfo("您的主帳號已被凍結,请与管理员联系!");
                    strResult = JsonHandle.ObjectToJson(result);
                    context.Session["user_state"] = _users.get_a_state().ToString();
                }
                else if (str3 == "1")
                {
                    result.set_success(200);
                    result.set_tipinfo(PageBase.GetMessageByCache("u100006", "MessageHint"));
                    strResult = JsonHandle.ObjectToJson(result);
                    context.Session["user_state"] = str3;
                }
                else
                {
                    context.Session["user_state"] = "0";
                    result.set_success(200);
                    strResult = JsonHandle.ObjectToJson(result);
                }
            }
            else
            {
                context.Session["user_name"] = _users.get_u_name().Trim();
                context.Session["user_type"] = _users.get_u_type().Trim();
                context.Session["user_state"] = str.Trim();
                if (str.Equals("1"))
                {
                    result.set_success(200);
                    result.set_tipinfo(PageBase.GetMessageByCache("u100004", "MessageHint"));
                    strResult = JsonHandle.ObjectToJson(result);
                    context.Session["user_state"] = str;
                }
                else if (str4 == "1")
                {
                    result.set_success(200);
                    result.set_tipinfo(PageBase.GetMessageByCache("u100006", "MessageHint"));
                    strResult = JsonHandle.ObjectToJson(result);
                    context.Session["user_state"] = str4;
                }
                else
                {
                    context.Session["user_state"] = "0";
                    result.set_success(200);
                    strResult = JsonHandle.ObjectToJson(result);
                }
            }
            agent_userinfo_session _session = new agent_userinfo_session();
            _session.set_u_id(_users.get_u_id());
            _session.set_u_name(_users.get_u_name().Trim());
            _session.set_u_psw(_users.get_u_psw().Trim());
            _session.set_u_nicker(_users.get_u_nicker().Trim());
            _session.set_u_skin(_users.get_u_skin().Trim());
            if (_child != null)
            {
                if (string.IsNullOrEmpty(_child.get_u_skin()))
                {
                    _session.set_u_skin("");
                }
                else
                {
                    _session.set_u_skin(_child.get_u_skin());
                }
            }
            _session.set_sup_name(_users.get_sup_name().Trim());
            _session.set_u_type(_users.get_u_type().Trim());
            _session.set_su_type(_users.get_su_type().Trim());
            _session.set_a_state(_users.get_a_state());
            _session.set_six_kind(_users.get_six_kind());
            _session.set_kc_kind(_users.get_kc_kind());
            _session.set_allow_sale(_users.get_allow_sale());
            _session.set_kc_allow_sale(_users.get_kc_allow_sale());
            _session.set_negative_sale(_users.get_negative_sale());
            if (!_users.get_allow_view_report().HasValue)
            {
                _session.set_allow_view_report(0);
            }
            else
            {
                _session.set_allow_view_report(_users.get_allow_view_report());
            }
            DataRow item = CallBLL.cz_admin_sysconfig_bll.GetItem();
            if (item == null)
            {
                _session.set_u_skin("Blue");
            }
            else
            {
                string str16 = item["agent_skin"].ToString();
                if (string.IsNullOrEmpty(_session.get_u_skin()) || (str16.IndexOf(_session.get_u_skin()) < 0))
                {
                    _session.set_u_skin(str16.Split(new char[] { '|' })[0]);
                }
            }
            if (_child != null)
            {
                _child.set_salt("");
            }
            _session.set_users_child_session(_child);
            DataTable zJInfo = CallBLL.cz_users_bll.GetZJInfo();
            if (zJInfo != null)
            {
                _session.set_zjname(zJInfo.Rows[0]["u_name"].ToString().Trim());
            }
            if (!_session.get_u_type().ToLower().Equals("zj"))
            {
                cz_rate_kc rateKCByUserName = CallBLL.cz_rate_kc_bll.GetRateKCByUserName(_session.get_u_name());
                _session.set_fgs_name(rateKCByUserName.get_fgs_name());
                _session.set_gd_name(rateKCByUserName.get_gd_name());
                _session.set_zd_name(rateKCByUserName.get_zd_name());
                _session.set_dl_name(rateKCByUserName.get_dl_name());
                DataTable userOpOdds = CallBLL.cz_rate_kc_bll.GetUserOpOdds(_session.get_u_name());
                if (userOpOdds != null)
                {
                    if ((userOpOdds.Rows[0]["six_op_odds"] != null) && (userOpOdds.Rows[0]["six_op_odds"].ToString() != ""))
                    {
                        _session.set_six_op_odds(new int?(int.Parse(userOpOdds.Rows[0]["six_op_odds"].ToString())));
                    }
                    if ((userOpOdds.Rows[0]["kc_op_odds"] != null) && (userOpOdds.Rows[0]["kc_op_odds"].ToString() != ""))
                    {
                        _session.set_kc_op_odds(new int?(int.Parse(userOpOdds.Rows[0]["kc_op_odds"].ToString())));
                    }
                }
            }
            context.Session["child_user_name"] = null;
            if (_child != null)
            {
                context.Session["child_user_name"] = _child.get_u_name();
            }
            context.Session["user_name"] = _users.get_u_name();
            context.Session[_users.get_u_name() + "lottery_session_user_info"] = _session;
            PageBase.SetAppcationFlag(str5);
            if (FileCacheHelper.get_RedisStatOnline().Equals(1) || FileCacheHelper.get_RedisStatOnline().Equals(2))
            {
                bool flag6 = false;
                if ((_session.get_users_child_session() != null) && _session.get_users_child_session().get_is_admin().Equals(1))
                {
                    flag6 = true;
                }
                if (!flag6)
                {
                    if (FileCacheHelper.get_RedisStatOnline().Equals(1))
                    {
                        new PageBase_Redis().InitUserOnlineTopToRedis(str5, _session.get_u_type());
                    }
                    else if (FileCacheHelper.get_RedisStatOnline().Equals(2))
                    {
                        new PageBase_Redis().InitUserOnlineTopToRedisStack(str5, _session.get_u_type());
                    }
                }
            }
            else
            {
                MemberPageBase.stat_top_online(str5);
                MemberPageBase.stat_online(str5, _session.get_u_type());
            }
            if (FileCacheHelper.get_RedisStatOnline().Equals(0))
            {
                PageBase.ZeroIsOutFlag(str5);
            }
            CallBLL.cz_user_psw_err_log_bll.ZeroErrTimes(str5);
            cz_login_log _log = new cz_login_log();
            _log.set_ip(LSRequest.GetIP());
            _log.set_login_time(new DateTime?(DateTime.Now));
            _log.set_u_name(str5);
            new PageBase();
            _log.set_browser_type(Utils.GetBrowserInfo(HttpContext.Current));
            CallBLL.cz_login_log_bll.Add(_log);
            if (_child == null)
            {
                string str17 = _users.get_is_changed().ToString();
                if (string.IsNullOrEmpty(str17) || (str17 == "0"))
                {
                    result.set_success(550);
                    result.set_tipinfo("新密碼首次登錄,需重置密碼!");
                    strResult = JsonHandle.ObjectToJson(result);
                    return;
                }
                DateTime? nullable = _users.get_last_changedate();
                int num2 = PageBase.PasswordExpire();
                if (nullable.HasValue)
                {
                    nullable12 = nullable;
                    time4 = DateTime.Now.AddDays((double) -num2);
                    if (!(nullable12.HasValue ? (nullable12.GetValueOrDefault() < time4) : false))
                    {
                        goto Label_131D;
                    }
                }
                result.set_success(550);
                result.set_tipinfo("密碼過期,需重置密碼!");
                strResult = JsonHandle.ObjectToJson(result);
                return;
            }
            string str18 = _child.get_is_changed().ToString();
            if (string.IsNullOrEmpty(str18) || (str18 == "0"))
            {
                result.set_success(550);
                result.set_tipinfo("新密碼首次登錄,需重置密碼!");
                strResult = JsonHandle.ObjectToJson(result);
                return;
            }
            DateTime? nullable2 = _child.get_last_changedate();
            int num3 = PageBase.PasswordExpire();
            if (nullable2.HasValue)
            {
                nullable12 = nullable2;
                time4 = DateTime.Now.AddDays((double) -num3);
                if (!(nullable12.HasValue ? (nullable12.GetValueOrDefault() < time4) : false))
                {
                    goto Label_131D;
                }
            }
            result.set_success(550);
            result.set_tipinfo("密碼過期,需重置密碼!");
            strResult = JsonHandle.ObjectToJson(result);
            return;
        Label_131D:
            dictionary.Add("uid", _session.get_u_id());
            DataTable lotteryList = base.GetLotteryList();
            string[] source = base.GetLotteryMasterID(lotteryList).Split(new char[] { ',' });
            int num4 = 1;
            if (source.Contains<string>(num4.ToString()))
            {
                dictionary.Add("hasSix", 1);
            }
            else
            {
                dictionary.Add("hasSix", 0);
            }
            num4 = 2;
            if (source.Contains<string>(num4.ToString()))
            {
                dictionary.Add("hasKc", 1);
            }
            else
            {
                dictionary.Add("hasKc", 0);
            }
            dictionary.Add("utype", _session.get_u_type().ToLower());
            List<object> list = new List<object>();
            foreach (DataRow row2 in lotteryList.Rows)
            {
                string str20 = row2["lottery_name"].ToString();
                string s = row2["id"].ToString();
                Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
                dictionary2.Add("name", str20);
                dictionary2.Add("id", int.Parse(s));
                list.Add(dictionary2);
            }
            dictionary.Add("lotteryCfg", list);
            dictionary.Add("PasswordLU", ConfigurationManager.AppSettings["PasswordLU"]);
            dictionary.Add("roleCfg", MemberPageBase_Mobile.roleCfg);
            result.set_data(dictionary);
            strResult = JsonHandle.ObjectToJson(result);
        }

        private void userLoginLog(ref string strResult)
        {
            base.checkLoginByHandler(0);
            ReturnResult result = new ReturnResult();
            new Dictionary<string, object>();
            List<object> list = new List<object>();
            string str = this.Session["user_name"].ToString();
            agent_userinfo_session _session = this.Session[str + "lottery_session_user_info"] as agent_userinfo_session;
            string str2 = _session.get_u_type().Trim();
            DataSet newList = CallBLL.cz_login_log_bll.GetNewList((this.Session["child_user_name"] != null) ? this.Session["child_user_name"].ToString() : this.Session["user_name"].ToString(), 50);
            if ((newList != null) && (newList.Tables.Count > 0))
            {
                DataTable table = newList.Tables[0];
                if ((table != null) && (table.Rows.Count > 0))
                {
                    foreach (DataRow row in table.Rows)
                    {
                        DateTime time = Convert.ToDateTime(row["login_time"].ToString());
                        string str3 = row["ip"].ToString();
                        string iPText = base.GetIPText(str2, str3);
                        string areaByIP = base.GetAreaByIP(str3);
                        Dictionary<string, string> item = new Dictionary<string, string>();
                        item.Add("ip", iPText);
                        item.Add("ipzone", areaByIP);
                        item.Add("time", time.ToString());
                        list.Add(item);
                    }
                }
            }
            result.set_success(200);
            result.set_data(list);
            strResult = base.ObjectToJson(result);
        }

        private void userLogout(ref string strResult)
        {
            base.checkLoginByHandler(0);
            this.Session.Abandon();
            ReturnResult result = new ReturnResult();
            result.set_success(200);
            strResult = JsonHandle.ObjectToJson(result);
        }
    }
}

