namespace User.Web.Handler
{
    using LotterySystem.Common;
    using LotterySystem.Model;
    using LotterySystem.WebPageBase;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.SessionState;
    using User.Web.WebBase;

    public class LoginHandler : IHttpHandler, IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            string str = LSRequest.qq("action").Trim();
            if (!string.IsNullOrEmpty(str) && !(str != "user_login"))
            {
                string strResult = "";
                this.user_login(context, ref strResult);
                context.Response.ContentType = "text/json";
                context.Response.Write(strResult);
                context.Response.End();
            }
        }

        private void user_login(HttpContext context, ref string strResult)
        {
            cz_login_log login_log;
            ReturnResult result = new ReturnResult();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("type", "user_login");
            string userName = LSRequest.qq("loginName").Trim().ToLower();
            string str = LSRequest.qq("loginPwd").Trim();
            string str2 = LSRequest.qq("ValidateCode").Trim();
            if (PageBase.is_ip_locked())
            {
                context.Session["lottery_session_img_code"] = null;
                result.set_success(400);
                result.set_tipinfo("由於輸入錯誤次數過多,您已被禁用,請稍後再試!");
                strResult = JsonHandle.ObjectToJson(result);
            }
            else if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(str))
            {
                context.Response.End();
            }
            else
            {
                DateTime time;
                if (int.Parse(FileCacheHelper.get_GetLockedPasswordCount()) == 0)
                {
                    context.Session["lottery_session_img_code_display"] = 1;
                }
                if (context.Session["lottery_session_img_code_display"] == null)
                {
                    if (CallBLL.cz_user_psw_err_log_bll.IsExistUser(userName))
                    {
                        if (PageBase.IsErrTimesAbove(ref time, userName))
                        {
                            if (!PageBase.IsErrTimeout(time))
                            {
                                context.Session["lottery_session_img_code"] = null;
                                result.set_success(400);
                                result.set_tipinfo("");
                                dictionary.Add("is_display_code", "1");
                                result.set_data(dictionary);
                                strResult = JsonHandle.ObjectToJson(result);
                                context.Session["lottery_session_img_code_display"] = 1;
                                return;
                            }
                            CallBLL.cz_user_psw_err_log_bll.ZeroErrTimes(userName);
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
                    if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(str))
                    {
                        context.Response.End();
                        return;
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(str))
                    {
                        context.Response.End();
                        return;
                    }
                    if (string.IsNullOrEmpty(str2))
                    {
                        context.Session["lottery_session_img_code"] = null;
                        result.set_success(400);
                        result.set_tipinfo("");
                        dictionary.Add("is_display_code", "1");
                        result.set_data(dictionary);
                        strResult = JsonHandle.ObjectToJson(result);
                        context.Session["lottery_session_img_code_display"] = 1;
                        return;
                    }
                    if (context.Session["lottery_session_img_code"] == null)
                    {
                        context.Response.End();
                        return;
                    }
                    if (context.Session["lottery_session_img_code"].ToString().ToLower() != str2.ToLower())
                    {
                        context.Session["lottery_session_img_code"] = null;
                        result.set_success(400);
                        result.set_tipinfo(PageBase.GetMessageByCache("u100004", "MessageHint"));
                        dictionary.Add("fs_name", "ValidateCode");
                        strResult = JsonHandle.ObjectToJson(result);
                        return;
                    }
                }
                cz_users _users = CallBLL.cz_users_bll.UserLogin(userName.ToLower());
                if (_users == null)
                {
                    context.Session["lottery_session_img_code"] = null;
                    PageBase.login_error_ip();
                    result.set_success(400);
                    result.set_tipinfo(PageBase.GetMessageByCache("u100005", "MessageHint"));
                    dictionary.Add("fs_name", "loginName");
                    strResult = JsonHandle.ObjectToJson(result);
                }
                else
                {
                    string str4 = _users.get_retry_times().ToString();
                    if (!string.IsNullOrEmpty(str4) && (int.Parse(str4) > int.Parse(FileCacheHelper.get_GetLockedUserCount())))
                    {
                        if (!PageBase.IsLockedTimeout(userName, "master"))
                        {
                            context.Session["lottery_session_img_code"] = null;
                            result.set_success(560);
                            result.set_tipinfo("您的帳號因密碼多次輸入錯誤被鎖死,請與管理員聯系!");
                            strResult = JsonHandle.ObjectToJson(result);
                            return;
                        }
                        PageBase.zero_retry_times(userName);
                    }
                    string str5 = _users.get_a_state().ToString();
                    string str6 = PageBase.upper_user_status(_users.get_u_name().ToLower());
                    if (str5 == "2")
                    {
                        context.Session["lottery_session_img_code"] = null;
                        result.set_success(400);
                        result.set_tipinfo(PageBase.GetMessageByCache("u100008", "MessageHint"));
                        strResult = JsonHandle.ObjectToJson(result);
                        context.Session.Abandon();
                    }
                    else if (str6 == "2")
                    {
                        context.Session["lottery_session_img_code"] = null;
                        result.set_success(400);
                        result.set_tipinfo("您的上級帳號已被停用,请与管理员联系!");
                        strResult = JsonHandle.ObjectToJson(result);
                        context.Session.Abandon();
                    }
                    else
                    {
                        if (str5 == "1")
                        {
                            result.set_success(200);
                            result.set_tipinfo(PageBase.GetMessageByCache("u100007", "MessageHint"));
                            strResult = JsonHandle.ObjectToJson(result);
                            context.Session["user_state"] = str5;
                        }
                        else if (str6 == "1")
                        {
                            result.set_success(200);
                            result.set_tipinfo(PageBase.GetMessageByCache("u100010", "MessageHint"));
                            strResult = JsonHandle.ObjectToJson(result);
                            context.Session["user_state"] = str6;
                        }
                        else
                        {
                            context.Session["user_state"] = "0";
                            result.set_success(200);
                            strResult = JsonHandle.ObjectToJson(result);
                        }
                        string str7 = _users.get_salt().Trim();
                        string str8 = DESEncrypt.EncryptString(str, str7);
                        if (_users.get_u_psw() != str8)
                        {
                            context.Session["lottery_session_img_code"] = null;
                            PageBase.inc_retry_times(userName);
                            PageBase.login_error_ip();
                            result.set_success(400);
                            result.set_tipinfo(PageBase.GetMessageByCache("u100006", "MessageHint"));
                            strResult = JsonHandle.ObjectToJson(result);
                            if (context.Session["lottery_session_img_code_display"].ToString() == "0")
                            {
                                if (CallBLL.cz_user_psw_err_log_bll.IsExistUser(userName))
                                {
                                    CallBLL.cz_user_psw_err_log_bll.UpdateErrTimes(userName);
                                }
                                else
                                {
                                    CallBLL.cz_user_psw_err_log_bll.AddUser(userName);
                                }
                                if (PageBase.IsErrTimesAbove(ref time, userName))
                                {
                                    context.Session["lottery_session_img_code"] = null;
                                    result.set_success(400);
                                    result.set_tipinfo(PageBase.GetMessageByCache("u100006", "MessageHint"));
                                    dictionary.Add("is_display_code", "1");
                                    result.set_data(dictionary);
                                    strResult = JsonHandle.ObjectToJson(result);
                                    context.Session["lottery_session_img_code_display"] = 1;
                                }
                            }
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
                            _session.set_a_state(new int?(int.Parse(context.Session["user_state"].ToString())));
                            DataTable zJInfo = CallBLL.cz_users_bll.GetZJInfo();
                            if (zJInfo != null)
                            {
                                _session.set_zjname(zJInfo.Rows[0]["u_name"].ToString().Trim());
                            }
                            DataRow item = CallBLL.cz_admin_sysconfig_bll.GetItem();
                            if (item == null)
                            {
                                _session.set_u_skin("Blue");
                            }
                            else
                            {
                                string str9 = item["hy_skin"].ToString();
                                if (string.IsNullOrEmpty(_session.get_u_skin()) || (str9.IndexOf(_session.get_u_skin()) < 0))
                                {
                                    _session.set_u_skin(str9.Split(new char[] { '|' })[0]);
                                }
                            }
                            DataTable table2 = CallBLL.cz_rate_six_bll.GetRateByAccount(userName.ToLower()).Tables[0];
                            _session.get_six_session().set_fgsname(table2.Rows[0]["fgs_name"].ToString().Trim());
                            _session.get_six_session().set_gdname(table2.Rows[0]["gd_name"].ToString().Trim());
                            _session.get_six_session().set_zdname(table2.Rows[0]["zd_name"].ToString().Trim());
                            _session.get_six_session().set_dlname(table2.Rows[0]["dl_name"].ToString().Trim());
                            DataTable table3 = CallBLL.cz_rate_kc_bll.GetRateByAccount(userName.ToLower()).Tables[0];
                            _session.get_kc_session().set_fgsname(table3.Rows[0]["fgs_name"].ToString().Trim());
                            _session.get_kc_session().set_gdname(table3.Rows[0]["gd_name"].ToString().Trim());
                            _session.get_kc_session().set_zdname(table3.Rows[0]["zd_name"].ToString().Trim());
                            _session.get_kc_session().set_dlname(table3.Rows[0]["dl_name"].ToString().Trim());
                            _session.set_kc_rate_owner(new int?(Convert.ToInt32(table3.Rows[0]["kc_rate_owner"])));
                            _session.set_six_rate_owner(new int?(Convert.ToInt32(table2.Rows[0]["six_rate_owner"])));
                            DataTable userOpOdds = CallBLL.cz_rate_kc_bll.GetUserOpOdds(userName.ToLower());
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
                            context.Session["user_name"] = userName.ToLower();
                            context.Session[userName + "lottery_session_user_info"] = _session;
                            PageBase.SetAppcationFlag(userName);
                            if (FileCacheHelper.get_RedisStatOnline().Equals(1))
                            {
                                new PageBase_Redis().InitUserOnlineTopToRedis(userName, _session.get_u_type());
                            }
                            else if (FileCacheHelper.get_RedisStatOnline().Equals(2))
                            {
                                new PageBase_Redis().InitUserOnlineTopToRedisStack(userName, _session.get_u_type());
                            }
                            else
                            {
                                MemberPageBase.stat_top_online(userName);
                                MemberPageBase.stat_online(userName, _session.get_u_type());
                            }
                            if (FileCacheHelper.get_RedisStatOnline().Equals(0))
                            {
                                PageBase.ZeroIsOutFlag(userName);
                            }
                            login_log = new cz_login_log();
                            login_log.set_ip(LSRequest.GetIP());
                            login_log.set_login_time(new DateTime?(DateTime.Now));
                            login_log.set_u_name(userName);
                            login_log.set_browser_type(Utils.GetBrowserInfo(HttpContext.Current));
                            Task.Factory.StartNew(delegate {
                                PageBase.zero_retry_times(userName);
                                CallBLL.cz_user_psw_err_log_bll.ZeroErrTimes(userName);
                                CallBLL.cz_login_log_bll.Add(login_log);
                            }).ContinueWith(delegate (Task t) {
                                string str = string.Format("Task Exception: {0}", t.Exception.InnerException.Message);
                                MessageQueueConfig.TaskQueue.Enqueue(new TaskModel(0, str));
                            }, TaskContinuationOptions.OnlyOnFaulted);
                            if (FileCacheHelper.get_GetWebModelView().Equals(0))
                            {
                                HttpContext.Current.Session["Session_LoginSystem_Flag"] = "LoginSystem_OldWeb";
                                _session.set_u_skin("Yellow");
                            }
                            else
                            {
                                HttpContext.Current.Session["Session_LoginSystem_Flag"] = "LoginSystem_NewWeb";
                            }
                            string str10 = _users.get_is_changed().ToString();
                            if (string.IsNullOrEmpty(str10))
                            {
                                result.set_success(550);
                                result.set_tipinfo("新密碼首次登錄,需重置密碼!");
                                strResult = JsonHandle.ObjectToJson(result);
                                context.Session["modifypassword"] = "【首次登錄，重置密碼】";
                            }
                            else if (str10 == "0")
                            {
                                result.set_success(550);
                                result.set_tipinfo("新密碼首次登錄,需重置密碼!");
                                strResult = JsonHandle.ObjectToJson(result);
                                context.Session["modifypassword"] = "【重置上級修改的密碼】";
                            }
                            else
                            {
                                DateTime? nullable3;
                                DateTime? nullable = _users.get_last_changedate();
                                int num2 = PageBase.PasswordExpire();
                                if (nullable.HasValue && ((nullable3 = nullable).HasValue ? (nullable3.GetValueOrDefault() < DateTime.Now.AddDays((double) -num2)) : false))
                                {
                                    result.set_success(550);
                                    result.set_tipinfo("密碼過期,需重置密碼!");
                                    strResult = JsonHandle.ObjectToJson(result);
                                    context.Session["modifypassword"] = "【密碼過期，重置密碼】";
                                }
                                else
                                {
                                    CallBLL.cz_credit_lock_bll.Delete(_users.get_u_name());
                                    result.set_data(dictionary);
                                    strResult = JsonHandle.ObjectToJson(result);
                                }
                            }
                        }
                    }
                }
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}

