using LotterySystem.Common;
using LotterySystem.Model;
using LotterySystem.WebPageBase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using User.Web.WebBase;

public class m_LoginAjax : MemberPageBase_Mobile
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string strResult = "";
        ReturnResult_Mobile mobile = new ReturnResult_Mobile();
        Dictionary<string, object> dictionary = new Dictionary<string, object>();
        dictionary.Add("type", "user_login");
        string str2 = LSRequest.qq("loginName").Trim();
        string str3 = LSRequest.qq("loginPwd").Trim();
        string str4 = LSRequest.qq("ValidateCode").Trim();
        if (PageBase.is_ip_locked())
        {
            this.Session["lottery_session_img_code"] = null;
            mobile.set_status(2);
            mobile.set_msg("由於輸入錯誤次數過多,您已被禁用,請稍後再試!");
            strResult = JsonHandle.ObjectToJson(mobile);
            base.OutJson(strResult);
        }
        else if (string.IsNullOrEmpty(str2) || string.IsNullOrEmpty(str3))
        {
            base.Response.End();
        }
        else
        {
            DateTime time;
            if (int.Parse(FileCacheHelper.get_GetLockedPasswordCount()) == 0)
            {
                this.Session["lottery_session_img_code_display"] = 1;
            }
            if (this.Session["lottery_session_img_code_display"] == null)
            {
                if (CallBLL.cz_user_psw_err_log_bll.IsExistUser(str2))
                {
                    if (PageBase.IsErrTimesAbove(ref time, str2))
                    {
                        if (!PageBase.IsErrTimeout(time))
                        {
                            this.Session["lottery_session_img_code"] = null;
                            mobile.set_status(2);
                            mobile.set_msg("");
                            dictionary.Add("is_display_code", "1");
                            mobile.set_data(dictionary);
                            strResult = JsonHandle.ObjectToJson(mobile);
                            this.Session["lottery_session_img_code_display"] = 1;
                            base.OutJson(strResult);
                            return;
                        }
                        CallBLL.cz_user_psw_err_log_bll.ZeroErrTimes(str2);
                        this.Session["lottery_session_img_code"] = null;
                        this.Session["lottery_session_img_code_display"] = 0;
                    }
                    else
                    {
                        this.Session["lottery_session_img_code"] = null;
                        this.Session["lottery_session_img_code_display"] = 0;
                    }
                }
                else
                {
                    this.Session["lottery_session_img_code"] = null;
                    this.Session["lottery_session_img_code_display"] = 0;
                }
            }
            if (this.Session["lottery_session_img_code_display"].ToString() == "0")
            {
                if (string.IsNullOrEmpty(str2) || string.IsNullOrEmpty(str3))
                {
                    base.Response.End();
                    return;
                }
            }
            else
            {
                if (string.IsNullOrEmpty(str2) || string.IsNullOrEmpty(str3))
                {
                    base.Response.End();
                    return;
                }
                if (string.IsNullOrEmpty(str4))
                {
                    this.Session["lottery_session_img_code"] = null;
                    mobile.set_status(2);
                    mobile.set_msg("");
                    dictionary.Add("is_display_code", "1");
                    mobile.set_data(dictionary);
                    strResult = JsonHandle.ObjectToJson(mobile);
                    this.Session["lottery_session_img_code_display"] = 1;
                    base.OutJson(strResult);
                    return;
                }
                if (this.Session["lottery_session_img_code"] == null)
                {
                    base.Response.End();
                    return;
                }
                if (this.Session["lottery_session_img_code"].ToString().ToLower() != str4.ToLower())
                {
                    this.Session["lottery_session_img_code"] = null;
                    mobile.set_status(2);
                    mobile.set_msg(PageBase.GetMessageByCache("u100004", "MessageHint"));
                    strResult = JsonHandle.ObjectToJson(mobile);
                    base.OutJson(strResult);
                    return;
                }
            }
            this.Session["lottery_session_img_code"] = null;
            cz_users _users = CallBLL.cz_users_bll.UserLogin(str2.ToLower());
            if (_users == null)
            {
                this.Session["lottery_session_img_code"] = null;
                PageBase.login_error_ip();
                mobile.set_status(2);
                mobile.set_msg(PageBase.GetMessageByCache("u100005", "MessageHint"));
                strResult = JsonHandle.ObjectToJson(mobile);
                base.OutJson(strResult);
            }
            else
            {
                string str6 = _users.get_retry_times().ToString();
                if (!string.IsNullOrEmpty(str6) && (int.Parse(str6) > int.Parse(FileCacheHelper.get_GetLockedUserCount())))
                {
                    if (!PageBase.IsLockedTimeout(str2, "master"))
                    {
                        this.Session["lottery_session_img_code"] = null;
                        mobile.set_status(2);
                        mobile.set_msg("您的帳號因密碼多次輸入錯誤被鎖死,請與管理員聯系!");
                        strResult = JsonHandle.ObjectToJson(mobile);
                        base.OutJson(strResult);
                        return;
                    }
                    PageBase.zero_retry_times(str2);
                }
                string str7 = _users.get_a_state().ToString();
                string str8 = PageBase.upper_user_status(_users.get_u_name().ToLower());
                if (str7 == "2")
                {
                    this.Session["lottery_session_img_code"] = null;
                    mobile.set_status(2);
                    mobile.set_msg(PageBase.GetMessageByCache("u100008", "MessageHint"));
                    strResult = JsonHandle.ObjectToJson(mobile);
                    base.OutJson(strResult);
                }
                else if (str8 == "2")
                {
                    this.Session["lottery_session_img_code"] = null;
                    mobile.set_status(2);
                    mobile.set_msg("您的上級帳號已被停用,请与管理员联系!");
                    strResult = JsonHandle.ObjectToJson(mobile);
                    base.OutJson(strResult);
                }
                else
                {
                    if (str7 == "1")
                    {
                        mobile.set_status(1);
                        mobile.set_msg(PageBase.GetMessageByCache("u100007", "MessageHint"));
                        strResult = JsonHandle.ObjectToJson(mobile);
                        this.Session["user_state"] = str7;
                    }
                    else if (str8 == "1")
                    {
                        mobile.set_status(1);
                        mobile.set_msg(PageBase.GetMessageByCache("u100010", "MessageHint"));
                        strResult = JsonHandle.ObjectToJson(mobile);
                        this.Session["user_state"] = str8;
                    }
                    else
                    {
                        this.Session["user_state"] = "0";
                        mobile.set_status(0);
                        strResult = JsonHandle.ObjectToJson(mobile);
                    }
                    string str9 = _users.get_salt().Trim();
                    string str10 = DESEncrypt.EncryptString(str3, str9);
                    if (_users.get_u_psw() != str10)
                    {
                        this.Session["lottery_session_img_code"] = null;
                        PageBase.inc_retry_times(str2);
                        PageBase.login_error_ip();
                        mobile.set_status(2);
                        mobile.set_msg(PageBase.GetMessageByCache("u100006", "MessageHint"));
                        strResult = JsonHandle.ObjectToJson(mobile);
                        if (this.Session["lottery_session_img_code_display"].ToString() == "0")
                        {
                            if (CallBLL.cz_user_psw_err_log_bll.IsExistUser(str2))
                            {
                                CallBLL.cz_user_psw_err_log_bll.UpdateErrTimes(str2);
                            }
                            else
                            {
                                CallBLL.cz_user_psw_err_log_bll.AddUser(str2);
                            }
                            if (PageBase.IsErrTimesAbove(ref time, str2))
                            {
                                this.Session["lottery_session_img_code"] = null;
                                mobile.set_status(2);
                                mobile.set_msg(PageBase.GetMessageByCache("u100006", "MessageHint"));
                                dictionary.Add("is_display_code", "1");
                                mobile.set_data(dictionary);
                                strResult = JsonHandle.ObjectToJson(mobile);
                                this.Session["lottery_session_img_code_display"] = 1;
                                base.OutJson(strResult);
                                return;
                            }
                        }
                        base.OutJson(strResult);
                    }
                    else
                    {
                        cz_userinfo_session _session = new cz_userinfo_session();
                        _session.set_u_id(_users.get_u_id());
                        _session.set_u_name(_users.get_u_name());
                        _session.set_u_nicker(_users.get_u_nicker());
                        _session.set_u_skin(_users.get_u_skin());
                        _session.set_u_type(_users.get_u_type());
                        _session.set_su_type(_users.get_su_type());
                        _session.set_kc_kind(_users.get_kc_kind().Trim());
                        _session.set_six_kind(_users.get_six_kind().Trim());
                        _session.set_u_psw(_users.get_u_psw().Trim());
                        _session.set_kc_rate_owner(_users.get_kc_rate_owner());
                        _session.set_six_rate_owner(_users.get_six_rate_owner());
                        DataTable zJInfo = CallBLL.cz_users_bll.GetZJInfo();
                        if (zJInfo != null)
                        {
                            _session.set_zjname(zJInfo.Rows[0]["u_name"].ToString().Trim());
                        }
                        DataTable table2 = CallBLL.cz_rate_six_bll.GetRateByAccount(str2.ToLower()).Tables[0];
                        _session.get_six_session().set_fgsname(table2.Rows[0]["fgs_name"].ToString().Trim());
                        _session.get_six_session().set_gdname(table2.Rows[0]["gd_name"].ToString().Trim());
                        _session.get_six_session().set_zdname(table2.Rows[0]["zd_name"].ToString().Trim());
                        _session.get_six_session().set_dlname(table2.Rows[0]["dl_name"].ToString().Trim());
                        DataTable table3 = CallBLL.cz_rate_kc_bll.GetRateByAccount(str2.ToLower()).Tables[0];
                        _session.get_kc_session().set_fgsname(table3.Rows[0]["fgs_name"].ToString().Trim());
                        _session.get_kc_session().set_gdname(table3.Rows[0]["gd_name"].ToString().Trim());
                        _session.get_kc_session().set_zdname(table3.Rows[0]["zd_name"].ToString().Trim());
                        _session.get_kc_session().set_dlname(table3.Rows[0]["dl_name"].ToString().Trim());
                        _session.set_kc_rate_owner(new int?(Convert.ToInt32(table3.Rows[0]["kc_rate_owner"])));
                        _session.set_six_rate_owner(new int?(Convert.ToInt32(table2.Rows[0]["six_rate_owner"])));
                        DataTable userOpOdds = CallBLL.cz_rate_kc_bll.GetUserOpOdds(str2.ToLower());
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
                        _session.set_isPhone(1);
                        this.Session["user_name"] = str2.ToLower();
                        this.Session[str2 + "lottery_session_user_info"] = _session;
                        PageBase.SetAppcationFlag(str2);
                        if (FileCacheHelper.get_RedisStatOnline().Equals(1))
                        {
                            new PageBase_Redis().InitUserOnlineTopToRedis(str2, _session.get_u_type());
                        }
                        else if (FileCacheHelper.get_RedisStatOnline().Equals(2))
                        {
                            new PageBase_Redis().InitUserOnlineTopToRedisStack(str2, _session.get_u_type());
                        }
                        else
                        {
                            MemberPageBase.stat_top_online(str2);
                            MemberPageBase.stat_online(str2, _session.get_u_type());
                        }
                        if (FileCacheHelper.get_RedisStatOnline().Equals(0))
                        {
                            PageBase.ZeroIsOutFlag(str2);
                        }
                        PageBase.zero_retry_times(str2);
                        CallBLL.cz_user_psw_err_log_bll.ZeroErrTimes(str2);
                        cz_login_log _log = new cz_login_log();
                        _log.set_ip(LSRequest.GetIP());
                        _log.set_login_time(new DateTime?(DateTime.Now));
                        _log.set_u_name(str2);
                        _log.set_browser_type(Utils.GetBrowserInfo(HttpContext.Current));
                        CallBLL.cz_login_log_bll.Add(_log);
                        this.Session["Session_LoginSystem_Flag"] = "LoginSystem_PhoneWeb";
                        string str11 = _users.get_is_changed().ToString();
                        if (string.IsNullOrEmpty(str11))
                        {
                            this.Session["lottery_session_img_code"] = null;
                            mobile.set_status(550);
                            mobile.set_msg("新密碼首次登錄,需重置密碼!");
                            strResult = JsonHandle.ObjectToJson(mobile);
                            this.Session["modifypassword"] = "【首次登錄，重置密碼】";
                            base.OutJson(strResult);
                        }
                        else if (str11 == "0")
                        {
                            this.Session["lottery_session_img_code"] = null;
                            mobile.set_status(550);
                            mobile.set_msg("新密碼首次登錄,需重置密碼!");
                            strResult = JsonHandle.ObjectToJson(mobile);
                            this.Session["modifypassword"] = "【重置上級修改的密碼】";
                            base.OutJson(strResult);
                        }
                        else
                        {
                            DateTime? nullable3;
                            DateTime? nullable = _users.get_last_changedate();
                            int num2 = PageBase.PasswordExpire();
                            if (nullable.HasValue && ((nullable3 = nullable).HasValue ? (nullable3.GetValueOrDefault() < DateTime.Now.AddDays((double) -num2)) : false))
                            {
                                this.Session["lottery_session_img_code"] = null;
                                mobile.set_status(550);
                                mobile.set_msg("密碼過期,需重置密碼!");
                                strResult = JsonHandle.ObjectToJson(mobile);
                                this.Session["modifypassword"] = "【密碼過期，重置密碼】";
                                base.OutJson(strResult);
                            }
                            else
                            {
                                CallBLL.cz_credit_lock_bll.Delete(_users.get_u_name());
                                mobile.set_status(0);
                                strResult = JsonHandle.ObjectToJson(mobile);
                                base.OutJson(strResult);
                            }
                        }
                    }
                }
            }
        }
    }
}

