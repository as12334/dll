namespace Agent.Web.Account
{
    using Agent.Web.WebBase;
    using LotterySystem.Common;
    using LotterySystem.Model;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Text;

    public class ExistNameAjax : MemberPageBase
    {
        private void AddHyDu(string u_type, string u_id)
        {
            StringBuilder builder = new StringBuilder();
            agent_userinfo_session _session = this.Session[this.Session["user_name"] + "lottery_session_user_info"] as agent_userinfo_session;
            if ((_session.get_u_type().Equals("dl") || u_type.Equals("zj")) || (u_type.Equals("dl") || u_type.Equals("hy")))
            {
                builder.Append("{");
                builder.Append("\"success\": 400,");
                builder.Append("\"data\": {\"type\": \"get_hydu\",\"six\": {},\"kc\": {}},");
                builder.Append("\"tipinfo\": \"操作失敗！\"");
                builder.Append("}");
                base.Response.Write(builder);
            }
            else
            {
                string str = "";
                string str2 = "";
                DataTable table2 = base.GetLotteryList().DefaultView.ToTable(true, new string[] { "master_id" });
                for (int i = 0; i < table2.Rows.Count; i++)
                {
                    if (Convert.ToInt32(table2.Rows[i][0]).Equals(1))
                    {
                        str2 = table2.Rows[i][0].ToString();
                    }
                    else if (Convert.ToInt32(table2.Rows[i][0]).Equals(2))
                    {
                        str = table2.Rows[i][0].ToString();
                    }
                }
                if (((string.IsNullOrEmpty(str2) || string.IsNullOrEmpty(str)) && (string.IsNullOrEmpty(str2) || !string.IsNullOrEmpty(str))) && string.IsNullOrEmpty(str2))
                {
                    string.IsNullOrEmpty(str);
                }
                string str3 = "0";
                string str4 = "0";
                string str5 = "";
                string str6 = "0";
                string str7 = "";
                string str8 = "0";
                string str9 = "0";
                string str10 = "";
                string str11 = "0";
                string str12 = "0";
                if (!string.IsNullOrEmpty(str2))
                {
                    Dictionary<string, string> rateByUID = CallBLL.cz_rate_six_bll.GetRateByUID(u_id, u_type);
                    if (rateByUID != null)
                    {
                        str3 = rateByUID["six_rate"];
                        string local1 = rateByUID["six_credit"];
                        str4 = rateByUID["six_usable_credit"];
                        string local2 = rateByUID["six_allow_sale"];
                        str5 = rateByUID["six_u_nicker"];
                        str6 = rateByUID["six_kind"];
                        str7 = rateByUID["u_name"];
                        string local3 = rateByUID["six_zj_rate"];
                        string local4 = rateByUID["six_fgs_rate"];
                        string local5 = rateByUID["six_gd_rate"];
                        string local6 = rateByUID["six_zd_rate"];
                        string local7 = rateByUID["six_dl_rate"];
                        string local8 = rateByUID["six_fgs_name"];
                        string local9 = rateByUID["six_gd_name"];
                        string local10 = rateByUID["six_zd_name"];
                        string local11 = rateByUID["six_dl_name"];
                    }
                }
                if (!string.IsNullOrEmpty(str))
                {
                    Dictionary<string, string> dictionary2 = CallBLL.cz_rate_kc_bll.GetRateByUID(u_id, u_type);
                    if (dictionary2 != null)
                    {
                        str8 = dictionary2["kc_rate"];
                        string local12 = dictionary2["kc_credit"];
                        str9 = dictionary2["kc_usable_credit"];
                        string local13 = dictionary2["kc_allow_sale"];
                        str10 = dictionary2["kc_u_nicker"];
                        str11 = dictionary2["kc_kind"];
                        str7 = dictionary2["u_name"];
                        string local14 = dictionary2["kc_zj_rate"];
                        string local15 = dictionary2["kc_fgs_rate"];
                        string local16 = dictionary2["kc_gd_rate"];
                        string local17 = dictionary2["kc_zd_rate"];
                        string local18 = dictionary2["kc_dl_rate"];
                        string local19 = dictionary2["kc_fgs_name"];
                        string local20 = dictionary2["kc_gd_name"];
                        string local21 = dictionary2["kc_zd_name"];
                        string local22 = dictionary2["kc_dl_name"];
                        str12 = dictionary2["kc_iscash"];
                    }
                }
                if (!base.IsUpperLowerLevels(str7, _session.get_u_type(), _session.get_u_name()))
                {
                    builder.Append("{");
                    builder.Append("\"success\": 400,");
                    builder.Append("\"data\": {\"type\": \"get_hydu\",\"six\": {},\"kc\": {}},");
                    builder.Append("\"tipinfo\": \"不能越級操作！\"");
                    builder.Append("}");
                }
                else
                {
                    builder.Append("{");
                    builder.Append("\"success\": 200,");
                    builder.Append("\"data\": {");
                    builder.Append("\"type\": \"get_hydu\",");
                    builder.AppendFormat("\"username\": \"{0}\",", str7);
                    if (!string.IsNullOrEmpty(str2))
                    {
                        builder.AppendFormat("\"u_nicker_d\": \"{0}\",", str5);
                    }
                    else
                    {
                        builder.AppendFormat("\"u_nicker_d\": \"{0}\",", str10);
                    }
                    builder.Append("\"six\": {");
                    if (!string.IsNullOrEmpty(str2))
                    {
                        builder.AppendFormat("\"six_rate_d\": {0},", str3);
                        builder.AppendFormat("\"six_kind_d\": \"{0}\",", str6);
                        builder.AppendFormat("\"six_usable_credit_d\": {0}", str4);
                    }
                    builder.Append("},");
                    builder.Append("\"kc\": {");
                    if (!string.IsNullOrEmpty(str))
                    {
                        builder.AppendFormat("\"kc_rate_d\": {0},", str8);
                        builder.AppendFormat("\"kc_kind_d\": \"{0}\",", str11);
                        builder.AppendFormat("\"kc_usable_credit_d\": {0},", str9);
                        builder.AppendFormat("\"kc_iscash\": \"{0}\"", str12);
                    }
                    builder.Append("}");
                    builder.Append("},");
                    builder.Append("\"tipinfo\": \"\"");
                    builder.Append("}");
                }
                base.Response.Write(builder.ToString());
            }
        }

        private void ExistName()
        {
            string str = LSRequest.qq("uname").Trim();
            if (string.IsNullOrEmpty(str))
            {
                base.Response.Write("2");
            }
            else if (!Utils.UserNameRegex(str))
            {
                base.Response.Write("3");
            }
            if (CallBLL.cz_users_bll.ExistName(str))
            {
                base.Response.Write("1");
            }
            else
            {
                base.Response.Write("0");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string str4 = LSRequest.qq("action");
            if (str4 != null)
            {
                if (!(str4 == "existname"))
                {
                    if (!(str4 == "updstatus"))
                    {
                        if (!(str4 == "updstatuschild"))
                        {
                            if (str4 == "get_hydu")
                            {
                                string str2 = LSRequest.qq("chktype");
                                string str3 = LSRequest.qq("sltuid");
                                this.AddHyDu(str2, str3);
                            }
                            return;
                        }
                        this.UpdateStatusChild();
                        return;
                    }
                }
                else
                {
                    this.ExistName();
                    return;
                }
                this.UpdateStatus();
            }
        }

        private void UpdateStatus()
        {
            string uid = LSRequest.qq("uid").Trim();
            string str2 = LSRequest.qq("status").Trim();
            cz_users userInfoByUID = CallBLL.cz_users_bll.GetUserInfoByUID(uid);
            string str3 = "";
            if (userInfoByUID != null)
            {
                str3 = userInfoByUID.get_a_state().ToString();
            }
            if (userInfoByUID == null)
            {
                base.Response.Redirect("../MessagePage.aspx?code=u100034&url=&issuccess=1&isback=0&isopen=1");
                base.Response.End();
            }
            if ((!str2.Equals("0") && !str2.Equals("1")) && !str2.Equals("2"))
            {
                base.Response.End();
            }
            string rateName = this.Session["user_name"].ToString();
            agent_userinfo_session model = this.Session[rateName + "lottery_session_user_info"] as agent_userinfo_session;
            base.Permission_Aspx_ZJ(model, "po_2_1");
            base.Permission_Aspx_DL(model, "po_6_1");
            if (model.get_u_name().Equals(userInfoByUID.get_u_name()))
            {
                base.Response.End();
            }
            if (!base.IsUnderLing(userInfoByUID.get_u_name(), rateName, model.get_u_type().Trim()))
            {
                base.Response.End();
            }
            if (!base.IsUpperLowerLevels(userInfoByUID.get_u_name(), model.get_u_type(), model.get_u_name()))
            {
                base.Response.End();
            }
            if (CallBLL.cz_users_bll.UpdateStatus(uid, str2))
            {
                if (FileCacheHelper.get_RedisStatOnline().Equals(1))
                {
                    base.UpdateIsOutOpts(userInfoByUID.get_u_name());
                }
                else if (FileCacheHelper.get_RedisStatOnline().Equals(2))
                {
                    base.UpdateIsOutOptsStack(userInfoByUID.get_u_name());
                }
                base.user_change_status_log(uid, str3, false);
                base.Response.Write("1");
            }
            else
            {
                base.Response.Write("0");
            }
        }

        private void UpdateStatusChild()
        {
            string uid = LSRequest.qq("uid").Trim();
            string str2 = LSRequest.qq("status").Trim();
            cz_users_child userByUID = CallBLL.cz_users_child_bll.GetUserByUID(uid);
            string str3 = "";
            if (userByUID != null)
            {
                str3 = userByUID.get_status().ToString();
            }
            string str4 = this.Session["user_name"].ToString();
            if (!userByUID.get_parent_u_name().Equals(str4))
            {
                base.Response.End();
            }
            if (CallBLL.cz_users_child_bll.UpdateStatus(uid, str2))
            {
                if (FileCacheHelper.get_RedisStatOnline().Equals(1))
                {
                    base.UpdateIsOutOpt(userByUID.get_u_name());
                }
                else if (FileCacheHelper.get_RedisStatOnline().Equals(2))
                {
                    base.UpdateIsOutOptStack(userByUID.get_u_name());
                }
                base.user_change_status_log(uid, str3, true);
                base.Response.Write("1");
            }
            else
            {
                base.Response.Write("0");
            }
        }
    }
}

