namespace Agent.Web.M
{
    using Agent.Web.WebBase;
    using LotterySystem.Common;
    using LotterySystem.Model;
    using LotterySystem.WebPageBase;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Text;

    public class manage : MemberPageBase_Mobile
    {
        protected string kc_allow_sale_d = "0";
        protected string kc_credit_d = "0";
        protected string kc_dl_rate = "0";
        protected string kc_fgs_rate = "0";
        protected string kc_gd_rate = "0";
        protected string kc_iscash = "0";
        protected string kc_kind_d = "0";
        protected string kc_rate_d = "0";
        protected string kc_u_nicker_d = "";
        protected string kc_usable_credit_d = "0";
        protected string kc_zd_rate = "0";
        protected string kc_zj_rate = "0";
        protected string six_allow_sale_d = "0";
        protected string six_credit_d = "0";
        protected string six_dl_rate = "0";
        protected string six_fgs_rate = "0";
        protected string six_gd_rate = "0";
        protected string six_iscash = "0";
        protected string six_kind_d = "0";
        protected string six_rate_d = "0";
        protected string six_u_nicker_d = "";
        protected string six_usable_credit_d = "0";
        protected string six_zd_rate = "0";
        protected string six_zj_rate = "0";
        private string tPath = "Account";
        protected string u_name = "";

        private void delFilluser(ref string strResult)
        {
            base.checkLoginByHandler(0);
            string str = this.Session["user_name"].ToString();
            agent_userinfo_session _session = this.Session[str + "lottery_session_user_info"] as agent_userinfo_session;
            if (!_session.get_u_type().Trim().Equals("zj"))
            {
                base.Response.End();
            }
            base.Permission_Aspx_ZJ_Mobile("po_2_3");
            string str2 = LSRequest.qq("uid");
            if (!string.IsNullOrEmpty(str2))
            {
                if (CallBLL.cz_saleset_six_bll.GetModel(str2).get_flag().Equals(1))
                {
                    base.noRightOptMsg("該外補會員不允許刪除！");
                }
                if (!CallBLL.cz_saleset_six_bll.DeleteUser(str2))
                {
                    base.noRightOptMsg("刪除外補會員失敗！");
                }
                else
                {
                    base.user_del_fill_log(str2);
                    base.successOptMsg("刪除外補會員成功！");
                }
            }
        }

        private int formatRole(string uType)
        {
            switch (uType)
            {
                case "fgs":
                    return 2;

                case "gd":
                    return 3;

                case "zd":
                    return 4;

                case "dl":
                    return 5;

                case "hy":
                    return 6;

                case "clone":
                    return 7;

                case "sales":
                    return 8;
            }
            return -1;
        }

        private void getAboveMemeber(ref string strResult)
        {
            base.checkLoginByHandler(0);
            ReturnResult result = new ReturnResult();
            new Dictionary<string, object>();
            List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
            string uName = this.Session["user_name"].ToString();
            agent_userinfo_session _session = this.Session[uName + "lottery_session_user_info"] as agent_userinfo_session;
            if (_session.get_u_type().Trim().Equals("zj"))
            {
                DataTable table = this.MemberNameAndCount(1, uName);
                if ((table != null) && (table.Rows.Count > 0))
                {
                    foreach (DataRow row in table.Rows)
                    {
                        Dictionary<string, object> item = new Dictionary<string, object>();
                        item.Add("sort", this.formatRole(row[0].ToString()));
                        item.Add("cnt", row[1].ToString());
                        item.Add("id", row[0].ToString());
                        list.Add(item);
                    }
                }
                if (_session.get_users_child_session() == null)
                {
                    table = this.MemberNameAndCount(7, uName);
                    if ((table != null) && (table.Rows.Count > 0))
                    {
                        Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
                        dictionary2.Add("sort", this.formatRole("clone"));
                        dictionary2.Add("cnt", table.Rows[0][0].ToString());
                        dictionary2.Add("id", "clone");
                        list.Add(dictionary2);
                    }
                }
                table = this.MemberNameAndCount(8, uName);
                if ((table != null) && (table.Rows.Count > 0))
                {
                    Dictionary<string, object> dictionary3 = new Dictionary<string, object>();
                    dictionary3.Add("sort", this.formatRole("sales"));
                    dictionary3.Add("cnt", table.Rows[0][0].ToString());
                    dictionary3.Add("id", "sales");
                    list.Add(dictionary3);
                }
            }
            else if (_session.get_u_type().Trim().Equals("fgs"))
            {
                DataTable table2 = this.MemberNameAndCount(2, uName);
                if ((table2 != null) && (table2.Rows.Count > 0))
                {
                    foreach (DataRow row2 in table2.Rows)
                    {
                        if (!(row2[0].ToString() == "fgs"))
                        {
                            Dictionary<string, object> dictionary4 = new Dictionary<string, object>();
                            dictionary4.Add("sort", this.formatRole(row2[0].ToString()));
                            dictionary4.Add("cnt", row2[1].ToString());
                            dictionary4.Add("id", row2[0].ToString());
                            list.Add(dictionary4);
                        }
                    }
                }
            }
            else if (_session.get_u_type().Trim().Equals("gd"))
            {
                DataTable table3 = this.MemberNameAndCount(3, uName);
                if ((table3 != null) && (table3.Rows.Count > 0))
                {
                    foreach (DataRow row3 in table3.Rows)
                    {
                        if (!(row3[0].ToString() == "gd"))
                        {
                            Dictionary<string, object> dictionary5 = new Dictionary<string, object>();
                            dictionary5.Add("sort", this.formatRole(row3[0].ToString()));
                            dictionary5.Add("cnt", row3[1].ToString());
                            dictionary5.Add("id", row3[0].ToString());
                            list.Add(dictionary5);
                        }
                    }
                }
            }
            else if (_session.get_u_type().Trim().Equals("zd"))
            {
                DataTable table4 = this.MemberNameAndCount(4, uName);
                if ((table4 != null) && (table4.Rows.Count > 0))
                {
                    foreach (DataRow row4 in table4.Rows)
                    {
                        if (!(row4[0].ToString() == "zd"))
                        {
                            Dictionary<string, object> dictionary6 = new Dictionary<string, object>();
                            dictionary6.Add("sort", this.formatRole(row4[0].ToString()));
                            dictionary6.Add("cnt", row4[1].ToString());
                            dictionary6.Add("id", row4[0].ToString());
                            list.Add(dictionary6);
                        }
                    }
                }
            }
            else if (_session.get_u_type().Trim().Equals("dl"))
            {
                DataTable table5 = this.MemberNameAndCount(5, uName);
                if ((table5 != null) && (table5.Rows.Count > 0))
                {
                    foreach (DataRow row5 in table5.Rows)
                    {
                        if (!(row5[0].ToString() == "dl"))
                        {
                            Dictionary<string, object> dictionary7 = new Dictionary<string, object>();
                            dictionary7.Add("sort", this.formatRole(row5[0].ToString()));
                            dictionary7.Add("cnt", row5[1].ToString());
                            dictionary7.Add("id", row5[0].ToString());
                            list.Add(dictionary7);
                        }
                    }
                }
            }
            else
            {
                return;
            }
            if (list.Count > 0)
            {
                list = (from d in list
                    orderby d["sort"]
                    select d).ToList<Dictionary<string, object>>();
            }
            result.set_success(200);
            result.set_data(list);
            strResult = base.ObjectToJson(result);
        }

        private void getAd(ref string strResult)
        {
            base.checkLoginByHandler(0);
            ReturnResult result = new ReturnResult();
            new Dictionary<string, object>();
            List<object> list = new List<object>();
            DataTable table = null;
            int num = 1;
            int num2 = 20;
            int num3 = 0;
            int num4 = 0;
            string str = this.Session["user_name"].ToString();
            object obj1 = this.Session[str + "lottery_session_user_info"];
            if (!string.IsNullOrEmpty(LSRequest.qq("page")))
            {
                num = Convert.ToInt32(LSRequest.qq("page"));
            }
            if (num < 1)
            {
                num = 1;
            }
            int num5 = base.get_current_master_id();
            DataSet set = CallBLL.cz_ad_bll.GetAdByPage(this.Session["user_type"].ToString(), num5, num - 1, num2, ref num3, ref num4);
            if (((set != null) && (set.Tables.Count > 0)) && (set.Tables[0].Rows.Count > 0))
            {
                table = set.Tables[0];
                if ((table != null) && (table.Rows.Count > 0))
                {
                    foreach (DataRow row in table.Rows)
                    {
                        string str2 = Convert.ToDateTime(row["post_date"].ToString()).ToString("yyyy-MM-dd");
                        string str3 = row["ad_content"].ToString();
                        Dictionary<string, string> item = new Dictionary<string, string>();
                        item.Add("time", str2);
                        item.Add("content", str3);
                        list.Add(item);
                    }
                }
            }
            result.set_success(200);
            result.set_data(list);
            strResult = base.ObjectToJson(result);
        }

        private void getAddMemberInitData(ref string strResult)
        {
            base.checkLoginByHandler(0);
            string str = this.Session["user_name"].ToString();
            agent_userinfo_session _session = this.Session[str + "lottery_session_user_info"] as agent_userinfo_session;
            string str2 = LSRequest.qq("memberId");
            switch (str2)
            {
                case "fgs":
                    if (!_session.get_u_type().Trim().Equals("zj"))
                    {
                        base.Response.End();
                    }
                    base.checkCloneRight();
                    this.initFGS(ref strResult);
                    return;

                case "gd":
                    if (!_session.get_u_type().Trim().Equals("zj") && !_session.get_u_type().Trim().Equals("fgs"))
                    {
                        base.Response.End();
                    }
                    base.checkCloneRight();
                    this.initGD(ref strResult);
                    return;

                case "zd":
                    if ((!_session.get_u_type().Trim().Equals("zj") && !_session.get_u_type().Trim().Equals("fgs")) && !_session.get_u_type().Trim().Equals("gd"))
                    {
                        base.Response.End();
                    }
                    base.checkCloneRight();
                    this.initZD(ref strResult);
                    return;

                case "dl":
                    if ((!_session.get_u_type().Trim().Equals("zj") && !_session.get_u_type().Trim().Equals("fgs")) && (!_session.get_u_type().Trim().Equals("gd") && !_session.get_u_type().Trim().Equals("zd")))
                    {
                        base.Response.End();
                    }
                    base.checkCloneRight();
                    this.initDL(ref strResult);
                    return;

                case "hy":
                {
                    base.checkCloneRight();
                    string str3 = LSRequest.qq("hyType");
                    if (str3 == "zs")
                    {
                        if ((!_session.get_u_type().Trim().Equals("zj") && !_session.get_u_type().Trim().Equals("fgs")) && (!_session.get_u_type().Trim().Equals("gd") && !_session.get_u_type().Trim().Equals("zd")))
                        {
                            base.Response.End();
                        }
                        this.initZSHY(ref strResult);
                        return;
                    }
                    if (string.IsNullOrEmpty(str3))
                    {
                        if (((!_session.get_u_type().Trim().Equals("zj") && !_session.get_u_type().Trim().Equals("fgs")) && (!_session.get_u_type().Trim().Equals("gd") && !_session.get_u_type().Trim().Equals("zd"))) && !_session.get_u_type().Trim().Equals("dl"))
                        {
                            base.Response.End();
                        }
                        this.initHY(ref strResult);
                        return;
                    }
                    break;
                }
                default:
                    if (str2 == "clone")
                    {
                        if (((!_session.get_u_type().Trim().Equals("zj") && !_session.get_u_type().Trim().Equals("fgs")) && (!_session.get_u_type().Trim().Equals("gd") && !_session.get_u_type().Trim().Equals("zd"))) && !_session.get_u_type().Trim().Equals("dl"))
                        {
                            base.Response.End();
                        }
                        if (_session.get_users_child_session() == null)
                        {
                            this.initClone(ref strResult);
                            return;
                        }
                    }
                    else
                    {
                        bool flag1 = str2 == "sales";
                    }
                    break;
            }
        }

        private void getCreditInfo(ref string strResult)
        {
            base.checkLoginByHandler(0);
            ReturnResult result = new ReturnResult();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            string str = this.Session["user_name"].ToString();
            agent_userinfo_session _session = this.Session[str + "lottery_session_user_info"] as agent_userinfo_session;
            if (_session.get_u_type().Equals("zj"))
            {
                result.set_success(400);
                result.set_tipinfo("總監無信用資料！");
                strResult = base.ObjectToJson(result);
            }
            else
            {
                DataSet userCredit = CallBLL.cz_users_bll.GetUserCredit(str);
                if (((userCredit != null) && (userCredit.Tables.Count > 0)) && (userCredit.Tables[0].Rows.Count > 0))
                {
                    dictionary.Add("name", _session.get_u_name());
                    dictionary.Add("nicker", _session.get_u_nicker());
                    dictionary.Add("utype", base.GetUTypeText(_session.get_u_type()));
                    dictionary.Add("userCreditSix", Convert.ToDouble(userCredit.Tables[0].Rows[0]["six_credit"]));
                    dictionary.Add("recoverCreditSix", Convert.ToDouble(userCredit.Tables[0].Rows[0]["six_usable_credit"]));
                    dictionary.Add("isCashSix", userCredit.Tables[0].Rows[0]["six_iscash"]);
                    dictionary.Add("userKindSix", _session.get_six_kind());
                    dictionary.Add("userCreditKc", Convert.ToDouble(userCredit.Tables[0].Rows[0]["kc_credit"]));
                    dictionary.Add("recoverCreditKc", Convert.ToDouble(userCredit.Tables[0].Rows[0]["kc_usable_credit"]));
                    dictionary.Add("isCashKc", userCredit.Tables[0].Rows[0]["kc_iscash"]);
                    dictionary.Add("userKindKc", _session.get_kc_kind());
                }
                result.set_success(200);
                result.set_data(dictionary);
                strResult = base.ObjectToJson(result);
            }
        }

        private void getDrawBack(ref string strResult)
        {
            base.checkLoginByHandler(0);
            if (!LSRequest.qq("memberId").Equals("sales"))
            {
                base.Server.Transfer(this.tPath + "/drawback.aspx");
            }
            else
            {
                base.Server.Transfer(this.tPath + "/filluser_drawback.aspx");
            }
        }

        private void getExistName(ref string strResult)
        {
            base.checkLoginByHandler(0);
            ReturnResult result = new ReturnResult();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            string str = LSRequest.qq("uname").Trim();
            if (string.IsNullOrEmpty(str))
            {
                dictionary.Add("status", "2");
            }
            else if (!Utils.UserNameRegex(str))
            {
                dictionary.Add("status", "3");
            }
            if (CallBLL.cz_users_bll.ExistName(str))
            {
                dictionary.Add("status", "1");
            }
            else
            {
                dictionary.Add("status", "0");
            }
            result.set_success(200);
            result.set_data(dictionary);
            strResult = base.ObjectToJson(result);
        }

        private void getInitZSHY(ref string strResult)
        {
            base.checkLoginByHandler(0);
            new ReturnResult();
            new Dictionary<string, object>();
            string str = LSRequest.qq("rdoutype");
            string str2 = LSRequest.qq("sltuid");
            StringBuilder builder = new StringBuilder();
            agent_userinfo_session _session = this.Session[this.Session["user_name"] + "lottery_session_user_info"] as agent_userinfo_session;
            if ((_session.get_u_type().Equals("dl") || str.Equals("zj")) || (str.Equals("dl") || str.Equals("hy")))
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
                string str3 = "";
                string str4 = "";
                DataTable table2 = base.GetLotteryList().DefaultView.ToTable(true, new string[] { "master_id" });
                for (int i = 0; i < table2.Rows.Count; i++)
                {
                    if (Convert.ToInt32(table2.Rows[i][0]).Equals(1))
                    {
                        str4 = table2.Rows[i][0].ToString();
                    }
                    else if (Convert.ToInt32(table2.Rows[i][0]).Equals(2))
                    {
                        str3 = table2.Rows[i][0].ToString();
                    }
                }
                if (((string.IsNullOrEmpty(str4) || string.IsNullOrEmpty(str3)) && (string.IsNullOrEmpty(str4) || !string.IsNullOrEmpty(str3))) && string.IsNullOrEmpty(str4))
                {
                    string.IsNullOrEmpty(str3);
                }
                string str5 = "0";
                string str6 = "0";
                string str7 = "";
                string str8 = "0";
                string str9 = "";
                string str10 = "0";
                string str11 = "0";
                string str12 = "";
                string str13 = "0";
                string str14 = "0";
                if (!string.IsNullOrEmpty(str4))
                {
                    Dictionary<string, string> rateByUID = CallBLL.cz_rate_six_bll.GetRateByUID(str2, str);
                    if (rateByUID != null)
                    {
                        str5 = rateByUID["six_rate"];
                        string local1 = rateByUID["six_credit"];
                        str6 = rateByUID["six_usable_credit"];
                        string local2 = rateByUID["six_allow_sale"];
                        str7 = rateByUID["six_u_nicker"];
                        str8 = rateByUID["six_kind"];
                        str9 = rateByUID["u_name"];
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
                if (!string.IsNullOrEmpty(str3))
                {
                    Dictionary<string, string> dictionary2 = CallBLL.cz_rate_kc_bll.GetRateByUID(str2, str);
                    if (dictionary2 != null)
                    {
                        str10 = dictionary2["kc_rate"];
                        string local12 = dictionary2["kc_credit"];
                        str11 = dictionary2["kc_usable_credit"];
                        string local13 = dictionary2["kc_allow_sale"];
                        str12 = dictionary2["kc_u_nicker"];
                        str13 = dictionary2["kc_kind"];
                        str9 = dictionary2["u_name"];
                        string local14 = dictionary2["kc_zj_rate"];
                        string local15 = dictionary2["kc_fgs_rate"];
                        string local16 = dictionary2["kc_gd_rate"];
                        string local17 = dictionary2["kc_zd_rate"];
                        string local18 = dictionary2["kc_dl_rate"];
                        string local19 = dictionary2["kc_fgs_name"];
                        string local20 = dictionary2["kc_gd_name"];
                        string local21 = dictionary2["kc_zd_name"];
                        string local22 = dictionary2["kc_dl_name"];
                        str14 = dictionary2["kc_iscash"];
                    }
                }
                if (!base.IsUpperLowerLevels(str9, _session.get_u_type(), _session.get_u_name()))
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
                    builder.AppendFormat("\"username\": \"{0}\",", str9);
                    if (!string.IsNullOrEmpty(str4))
                    {
                        builder.AppendFormat("\"u_nicker_d\": \"{0}\",", str7);
                    }
                    else
                    {
                        builder.AppendFormat("\"u_nicker_d\": \"{0}\",", str12);
                    }
                    builder.Append("\"six\": {");
                    if (!string.IsNullOrEmpty(str4))
                    {
                        builder.AppendFormat("\"six_rate_d\": {0},", str5);
                        builder.AppendFormat("\"six_kind_d\": \"{0}\",", str8);
                        builder.AppendFormat("\"six_usable_credit_d\": {0}", str6);
                    }
                    builder.Append("},");
                    builder.Append("\"kc\": {");
                    if (!string.IsNullOrEmpty(str3))
                    {
                        builder.AppendFormat("\"kc_rate_d\": {0},", str10);
                        builder.AppendFormat("\"kc_kind_d\": \"{0}\",", str13);
                        builder.AppendFormat("\"kc_usable_credit_d\": {0},", str11);
                        builder.AppendFormat("\"kc_iscash\": \"{0}\"", str14);
                    }
                    builder.Append("}");
                    builder.Append("},");
                    builder.Append("\"tipinfo\": \"\"");
                    builder.Append("}");
                }
                base.Response.Write(builder.ToString());
            }
        }

        private void getMemberDetail(ref string strResult)
        {
            base.checkLoginByHandler(0);
            LSRequest.qq("uid");
            string str = LSRequest.qq("memberId");
            string str2 = LSRequest.qq("submitType");
            if (((str2 != "view") && (str2 != "edit")) && (str2 != "add"))
            {
                base.Response.End();
            }
            switch (str)
            {
                case "fgs":
                    switch (str2)
                    {
                        case "view":
                        case "edit":
                            base.Server.Transfer(this.tPath + "/fgs_edit.aspx");
                            return;
                    }
                    base.Server.Transfer(this.tPath + "/fgs_add.aspx");
                    return;

                case "gd":
                    switch (str2)
                    {
                        case "view":
                        case "edit":
                            base.Server.Transfer(this.tPath + "/gd_edit.aspx");
                            return;
                    }
                    base.Server.Transfer(this.tPath + "/gd_add.aspx");
                    return;

                case "zd":
                    switch (str2)
                    {
                        case "view":
                        case "edit":
                            base.Server.Transfer(this.tPath + "/zd_edit.aspx");
                            return;
                    }
                    base.Server.Transfer(this.tPath + "/zd_add.aspx");
                    return;

                case "dl":
                    switch (str2)
                    {
                        case "view":
                        case "edit":
                            base.Server.Transfer(this.tPath + "/dl_edit.aspx");
                            return;
                    }
                    base.Server.Transfer(this.tPath + "/dl_add.aspx");
                    return;

                case "hy":
                {
                    switch (str2)
                    {
                        case "view":
                        case "edit":
                            base.Server.Transfer(this.tPath + "/hy_edit.aspx");
                            return;
                    }
                    string str3 = LSRequest.qq("hyType");
                    if (string.IsNullOrEmpty(str3))
                    {
                        base.Server.Transfer(this.tPath + "/hy_add.aspx");
                        return;
                    }
                    if (str3 == "zs")
                    {
                        base.Server.Transfer(this.tPath + "/hy_zs_add.aspx");
                        return;
                    }
                    break;
                }
                case "clone":
                    switch (str2)
                    {
                        case "view":
                        case "edit":
                            base.Server.Transfer(this.tPath + "/clone_edit.aspx");
                            return;
                    }
                    base.Server.Transfer(this.tPath + "/clone_add.aspx");
                    return;

                case "sales":
                    switch (str2)
                    {
                        case "view":
                        case "edit":
                            base.Server.Transfer(this.tPath + "/filluser_edit.aspx");
                            return;
                    }
                    base.Server.Transfer(this.tPath + "/filluser_add.aspx");
                    break;
            }
        }

        private void getMemberInfo(ref string strResult)
        {
            base.checkLoginByHandler(0);
            ReturnResult result = new ReturnResult();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            List<object> list = new List<object>();
            string str = this.Session["user_name"].ToString();
            agent_userinfo_session _session = this.Session[str + "lottery_session_user_info"] as agent_userinfo_session;
            base.checkCloneRight();
            string str2 = "0";
            string str3 = "0";
            string str4 = "";
            string str5 = "0";
            string str6 = "0";
            int num = 20;
            int num2 = 0;
            int num3 = 0;
            string str7 = "";
            string str8 = "";
            string str9 = "";
            cz_users userInfoByUID = null;
            string str10 = LSRequest.qq("page");
            string str11 = LSRequest.qq("memberId");
            string str12 = LSRequest.qq("uid");
            str4 = LSRequest.qq("keyword");
            str2 = LSRequest.qq("status");
            if (string.IsNullOrEmpty(str2))
            {
                str2 = "0";
            }
            if (((str2 != "0") && (str2 != "1")) && (str2 != "2"))
            {
                base.Response.End();
            }
            if (string.IsNullOrEmpty(str10) || (int.Parse(str10) < 1))
            {
                str10 = "1";
            }
            if (string.IsNullOrEmpty(str11))
            {
                base.Response.End();
            }
            if (string.IsNullOrEmpty(str12))
            {
                str7 = _session.get_u_id();
                str8 = _session.get_u_type();
                if (_session.get_u_type().Equals("zj"))
                {
                    str8 = "";
                }
            }
            else
            {
                str7 = str12;
                userInfoByUID = CallBLL.cz_users_bll.GetUserInfoByUID(str7);
                if (userInfoByUID == null)
                {
                    base.Response.End();
                }
                str8 = userInfoByUID.get_u_type();
            }
            DataTable table = null;
            if (_session.get_u_type().Equals("zj"))
            {
                if (str11.Equals("fgs"))
                {
                    if (FileCacheHelper.get_RedisStatOnline().Equals(1) || FileCacheHelper.get_RedisStatOnline().Equals(2))
                    {
                        table = CallBLL.cz_users_bll.GetUserFGSList(str2, "fgs", str3, str4, Convert.ToInt32(str10) - 1, num, ref num3, ref num2, str5, str6, "redis");
                    }
                    else
                    {
                        table = CallBLL.cz_users_bll.GetUserFGSList(str2, "fgs", str3, str4, Convert.ToInt32(str10) - 1, num, ref num3, ref num2, str5, str6);
                    }
                }
                else if (str11.Equals("gd"))
                {
                    if (FileCacheHelper.get_RedisStatOnline().Equals(1) || FileCacheHelper.get_RedisStatOnline().Equals(2))
                    {
                        table = CallBLL.cz_users_bll.GetUserGDList(str2, "gd", str3, str4, Convert.ToInt32(str10) - 1, num, ref num3, ref num2, str5, str6, str7, str8, "redis");
                    }
                    else
                    {
                        table = CallBLL.cz_users_bll.GetUserGDList(str2, "gd", str3, str4, Convert.ToInt32(str10) - 1, num, ref num3, ref num2, str5, str6, str7, str8);
                    }
                }
                else if (str11.Equals("zd"))
                {
                    if (FileCacheHelper.get_RedisStatOnline().Equals(1) || FileCacheHelper.get_RedisStatOnline().Equals(2))
                    {
                        table = CallBLL.cz_users_bll.GetUserZDList(str2, "zd", str3, str4, Convert.ToInt32(str10) - 1, num, ref num3, ref num2, str5, str6, str7, str8, "redis");
                    }
                    else
                    {
                        table = CallBLL.cz_users_bll.GetUserZDList(str2, "zd", str3, str4, Convert.ToInt32(str10) - 1, num, ref num3, ref num2, str5, str6, str7, str8);
                    }
                }
                else if (str11.Equals("dl"))
                {
                    if (FileCacheHelper.get_RedisStatOnline().Equals(1) || FileCacheHelper.get_RedisStatOnline().Equals(2))
                    {
                        table = CallBLL.cz_users_bll.GetUserDLList(str2, "dl", str3, str4, Convert.ToInt32(str10) - 1, num, ref num3, ref num2, str5, str6, str7, str8, "redis");
                    }
                    else
                    {
                        table = CallBLL.cz_users_bll.GetUserDLList(str2, "dl", str3, str4, Convert.ToInt32(str10) - 1, num, ref num3, ref num2, str5, str6, str7, str8);
                    }
                }
                else if (str11.Equals("hy"))
                {
                    if (FileCacheHelper.get_RedisStatOnline().Equals(1) || FileCacheHelper.get_RedisStatOnline().Equals(2))
                    {
                        table = CallBLL.cz_users_bll.GetUserHYList(str2, "hy", str3, str4, Convert.ToInt32(str10) - 1, num, ref num3, ref num2, str5, str6, str7, str8, str9, "redis");
                    }
                    else
                    {
                        table = CallBLL.cz_users_bll.GetUserHYList(str2, "hy", str3, str4, Convert.ToInt32(str10) - 1, num, ref num3, ref num2, str5, str6, str7, str8, str9);
                    }
                }
                else if (str11.Equals("clone"))
                {
                    DataTable table2 = null;
                    if (FileCacheHelper.get_RedisStatOnline().Equals(1) || FileCacheHelper.get_RedisStatOnline().Equals(2))
                    {
                        table2 = CallBLL.cz_users_child_bll.GetChildList(str, Convert.ToInt32(str10) - 1, num, ref num3, ref num2, "redis");
                    }
                    else
                    {
                        table2 = CallBLL.cz_users_child_bll.GetChildList(str, Convert.ToInt32(str10) - 1, num, ref num3, ref num2);
                    }
                    if ((table2 != null) && (table2.Rows.Count > 0))
                    {
                        foreach (DataRow row in table2.Rows)
                        {
                            string str13 = row["u_id"].ToString();
                            string str14 = row["u_name"].ToString();
                            string str15 = row["parent_u_name"].ToString();
                            row["u_nicker"].ToString();
                            string str16 = "0";
                            if (FileCacheHelper.get_RedisStatOnline().Equals(1))
                            {
                                string str17 = CallBLL.cz_users_bll.GetUserInfoByUName(str15).get_u_type();
                                if (!base.m_agent_is_stat_online(str14, str17, "redis"))
                                {
                                    str16 = "0";
                                }
                                else
                                {
                                    str16 = "1";
                                }
                            }
                            else if (FileCacheHelper.get_RedisStatOnline().Equals(2))
                            {
                                string str18 = CallBLL.cz_users_bll.GetUserInfoByUName(str15).get_u_type();
                                if (!base.m_agent_is_stat_onlineStack(str14, str18, "redis"))
                                {
                                    str16 = "0";
                                }
                                else
                                {
                                    str16 = "1";
                                }
                            }
                            else
                            {
                                str16 = string.IsNullOrEmpty(row["onlinename"].ToString()) ? "0" : "1";
                            }
                            row["add_date"].ToString();
                            string str20 = row["status"].ToString();
                            Dictionary<string, object> item = new Dictionary<string, object>();
                            item.Add("uid", str13);
                            item.Add("userState", str20);
                            item.Add("name", str14);
                            item.Add("online", str16);
                            list.Add(item);
                        }
                    }
                }
                else if (str11.Equals("sales"))
                {
                    DataTable table3 = CallBLL.cz_saleset_six_bll.GetSaleSetUserByPage(Convert.ToInt32(str10) - 1, num, ref num3, ref num2);
                    if ((table3 != null) && (table3.Rows.Count > 0))
                    {
                        foreach (DataRow row2 in table3.Rows)
                        {
                            string str21 = row2["flag"].ToString();
                            string str22 = "";
                            if (str21 == "1")
                            {
                                str22 = "網內走飛";
                            }
                            else
                            {
                                str22 = "外網走飛";
                            }
                            string str23 = row2["u_id"].ToString();
                            string str24 = row2["u_name"].ToString();
                            row2["u_nicker"].ToString();
                            string str25 = row2["six_kind"].ToString();
                            row2["add_date"].ToString();
                            Dictionary<string, object> dictionary3 = new Dictionary<string, object>();
                            dictionary3.Add("uid", str23);
                            dictionary3.Add("name", str24);
                            dictionary3.Add("sixKind", str25);
                            dictionary3.Add("utype", str22);
                            list.Add(dictionary3);
                        }
                    }
                }
                else
                {
                    base.Response.End();
                }
            }
            else if (_session.get_u_type().Equals("fgs"))
            {
                if (!_session.get_u_type().Equals("zj"))
                {
                    cz_users _users2 = CallBLL.cz_users_bll.GetUserInfoByUID(str7);
                    if (!base.IsUpperLowerLevels(_users2.get_u_name(), _session.get_u_type(), _session.get_u_name()))
                    {
                        base.Response.End();
                    }
                }
                if (str11.Equals("gd"))
                {
                    if (FileCacheHelper.get_RedisStatOnline().Equals(1) || FileCacheHelper.get_RedisStatOnline().Equals(2))
                    {
                        table = CallBLL.cz_users_bll.GetUserGDList(str2, "gd", str3, str4, Convert.ToInt32(str10) - 1, num, ref num3, ref num2, str5, str6, str7, str8, "redis");
                    }
                    else
                    {
                        table = CallBLL.cz_users_bll.GetUserGDList(str2, "gd", str3, str4, Convert.ToInt32(str10) - 1, num, ref num3, ref num2, str5, str6, str7, str8);
                    }
                }
                else if (str11.Equals("zd"))
                {
                    if (FileCacheHelper.get_RedisStatOnline().Equals(1) || FileCacheHelper.get_RedisStatOnline().Equals(2))
                    {
                        table = CallBLL.cz_users_bll.GetUserZDList(str2, "zd", str3, str4, Convert.ToInt32(str10) - 1, num, ref num3, ref num2, str5, str6, str7, str8, "redis");
                    }
                    else
                    {
                        table = CallBLL.cz_users_bll.GetUserZDList(str2, "zd", str3, str4, Convert.ToInt32(str10) - 1, num, ref num3, ref num2, str5, str6, str7, str8);
                    }
                }
                else if (str11.Equals("dl"))
                {
                    if (FileCacheHelper.get_RedisStatOnline().Equals(1) || FileCacheHelper.get_RedisStatOnline().Equals(2))
                    {
                        table = CallBLL.cz_users_bll.GetUserDLList(str2, "dl", str3, str4, Convert.ToInt32(str10) - 1, num, ref num3, ref num2, str5, str6, str7, str8, "redis");
                    }
                    else
                    {
                        table = CallBLL.cz_users_bll.GetUserDLList(str2, "dl", str3, str4, Convert.ToInt32(str10) - 1, num, ref num3, ref num2, str5, str6, str7, str8);
                    }
                }
                else if (str11.Equals("hy"))
                {
                    if (FileCacheHelper.get_RedisStatOnline().Equals(1) || FileCacheHelper.get_RedisStatOnline().Equals(2))
                    {
                        table = CallBLL.cz_users_bll.GetUserHYList(str2, "hy", str3, str4, Convert.ToInt32(str10) - 1, num, ref num3, ref num2, str5, str6, str7, str8, str9, "redis");
                    }
                    else
                    {
                        table = CallBLL.cz_users_bll.GetUserHYList(str2, "hy", str3, str4, Convert.ToInt32(str10) - 1, num, ref num3, ref num2, str5, str6, str7, str8, str9);
                    }
                }
                else
                {
                    base.Response.End();
                }
            }
            else if (_session.get_u_type().Equals("gd"))
            {
                if (!_session.get_u_type().Equals("zj"))
                {
                    cz_users _users3 = CallBLL.cz_users_bll.GetUserInfoByUID(str7);
                    if (!base.IsUpperLowerLevels(_users3.get_u_name(), _session.get_u_type(), _session.get_u_name()))
                    {
                        base.Response.End();
                    }
                }
                if (str11.Equals("zd"))
                {
                    if (FileCacheHelper.get_RedisStatOnline().Equals(1) || FileCacheHelper.get_RedisStatOnline().Equals(2))
                    {
                        table = CallBLL.cz_users_bll.GetUserZDList(str2, "zd", str3, str4, Convert.ToInt32(str10) - 1, num, ref num3, ref num2, str5, str6, str7, str8, "redis");
                    }
                    else
                    {
                        table = CallBLL.cz_users_bll.GetUserZDList(str2, "zd", str3, str4, Convert.ToInt32(str10) - 1, num, ref num3, ref num2, str5, str6, str7, str8);
                    }
                }
                else if (str11.Equals("dl"))
                {
                    if (FileCacheHelper.get_RedisStatOnline().Equals(1) || FileCacheHelper.get_RedisStatOnline().Equals(2))
                    {
                        table = CallBLL.cz_users_bll.GetUserDLList(str2, "dl", str3, str4, Convert.ToInt32(str10) - 1, num, ref num3, ref num2, str5, str6, str7, str8, "redis");
                    }
                    else
                    {
                        table = CallBLL.cz_users_bll.GetUserDLList(str2, "dl", str3, str4, Convert.ToInt32(str10) - 1, num, ref num3, ref num2, str5, str6, str7, str8);
                    }
                }
                else if (str11.Equals("hy"))
                {
                    if (FileCacheHelper.get_RedisStatOnline().Equals(1) || FileCacheHelper.get_RedisStatOnline().Equals(2))
                    {
                        table = CallBLL.cz_users_bll.GetUserHYList(str2, "hy", str3, str4, Convert.ToInt32(str10) - 1, num, ref num3, ref num2, str5, str6, str7, str8, str9, "redis");
                    }
                    else
                    {
                        table = CallBLL.cz_users_bll.GetUserHYList(str2, "hy", str3, str4, Convert.ToInt32(str10) - 1, num, ref num3, ref num2, str5, str6, str7, str8, str9);
                    }
                }
                else
                {
                    base.Response.End();
                }
            }
            else if (_session.get_u_type().Equals("zd"))
            {
                if (!_session.get_u_type().Equals("zj"))
                {
                    cz_users _users4 = CallBLL.cz_users_bll.GetUserInfoByUID(str7);
                    if (!base.IsUpperLowerLevels(_users4.get_u_name(), _session.get_u_type(), _session.get_u_name()))
                    {
                        base.Response.End();
                    }
                }
                if (str11.Equals("dl"))
                {
                    if (FileCacheHelper.get_RedisStatOnline().Equals(1) || FileCacheHelper.get_RedisStatOnline().Equals(2))
                    {
                        table = CallBLL.cz_users_bll.GetUserDLList(str2, "dl", str3, str4, Convert.ToInt32(str10) - 1, num, ref num3, ref num2, str5, str6, str7, str8, "redis");
                    }
                    else
                    {
                        table = CallBLL.cz_users_bll.GetUserDLList(str2, "dl", str3, str4, Convert.ToInt32(str10) - 1, num, ref num3, ref num2, str5, str6, str7, str8);
                    }
                }
                else if (str11.Equals("hy"))
                {
                    if (FileCacheHelper.get_RedisStatOnline().Equals(1) || FileCacheHelper.get_RedisStatOnline().Equals(2))
                    {
                        table = CallBLL.cz_users_bll.GetUserHYList(str2, "hy", str3, str4, Convert.ToInt32(str10) - 1, num, ref num3, ref num2, str5, str6, str7, str8, str9, "redis");
                    }
                    else
                    {
                        table = CallBLL.cz_users_bll.GetUserHYList(str2, "hy", str3, str4, Convert.ToInt32(str10) - 1, num, ref num3, ref num2, str5, str6, str7, str8, str9);
                    }
                }
                else
                {
                    base.Response.End();
                }
            }
            else if (_session.get_u_type().Equals("dl"))
            {
                if (!_session.get_u_type().Equals("zj"))
                {
                    cz_users _users5 = CallBLL.cz_users_bll.GetUserInfoByUID(str7);
                    if (!base.IsUpperLowerLevels(_users5.get_u_name(), _session.get_u_type(), _session.get_u_name()))
                    {
                        base.Response.End();
                    }
                }
                if (str11.Equals("hy"))
                {
                    if (FileCacheHelper.get_RedisStatOnline().Equals(1) || FileCacheHelper.get_RedisStatOnline().Equals(2))
                    {
                        table = CallBLL.cz_users_bll.GetUserHYList(str2, "hy", str3, str4, Convert.ToInt32(str10) - 1, num, ref num3, ref num2, str5, str6, str7, str8, str9, "redis");
                    }
                    else
                    {
                        table = CallBLL.cz_users_bll.GetUserHYList(str2, "hy", str3, str4, Convert.ToInt32(str10) - 1, num, ref num3, ref num2, str5, str6, str7, str8, str9);
                    }
                }
                else
                {
                    base.Response.End();
                }
            }
            if ((table != null) && (table.Rows.Count > 0))
            {
                foreach (DataRow row3 in table.Rows)
                {
                    string str26 = row3["u_id"].ToString();
                    string str27 = row3["u_name"].ToString();
                    string str28 = row3["sup_name"].ToString();
                    row3["u_nicker"].ToString();
                    string str29 = row3["su_type"].ToString();
                    string str30 = "0";
                    if (FileCacheHelper.get_RedisStatOnline().Equals(1))
                    {
                        string str31 = CallBLL.cz_users_bll.GetUserInfoByUName(str28).get_u_type();
                        if (!base.m_agent_is_stat_online(str27, str31, "redis"))
                        {
                            str30 = "0";
                        }
                        else
                        {
                            str30 = "1";
                        }
                    }
                    else if (FileCacheHelper.get_RedisStatOnline().Equals(2))
                    {
                        string str32 = CallBLL.cz_users_bll.GetUserInfoByUName(str28).get_u_type();
                        if (!base.m_agent_is_stat_onlineStack(str27, str32, "redis"))
                        {
                            str30 = "0";
                        }
                        else
                        {
                            str30 = "1";
                        }
                    }
                    else
                    {
                        str30 = string.IsNullOrEmpty(row3["onlinename"].ToString()) ? "0" : "1";
                    }
                    row3["kc_rate"].ToString();
                    row3["six_rate"].ToString();
                    string.Format("{0:F0}", Convert.ToDouble(row3["kc_credit"].ToString()));
                    string.Format("{0:F0}", Convert.ToDouble(row3["six_credit"].ToString()));
                    string str34 = string.Format("{0:F0}", Convert.ToDouble(row3["kc_usable_credit"].ToString()));
                    string str35 = string.Format("{0:F0}", Convert.ToDouble(row3["six_usable_credit"].ToString()));
                    row3["add_date"].ToString();
                    string str36 = "";
                    if (str11.Equals("fgs"))
                    {
                        str36 = row3["m_rate_kc"].ToString();
                        str36.Replace("fgs-1,", "");
                        string str37 = "0";
                        string str38 = "0";
                        string str39 = "0";
                        string str40 = "0";
                        string[] strArray = str36.Split(new char[] { ',' });
                        Dictionary<int, string> dictionary4 = new Dictionary<int, string>();
                        foreach (string str41 in strArray)
                        {
                            if (str41.IndexOf("gd") > -1)
                            {
                                str37 = str41.Split(new char[] { '-' })[1];
                            }
                            if (str41.IndexOf("zd") > -1)
                            {
                                str38 = str41.Split(new char[] { '-' })[1];
                            }
                            if (str41.IndexOf("dl") > -1)
                            {
                                str39 = str41.Split(new char[] { '-' })[1];
                            }
                            if (str41.IndexOf("hy") > -1)
                            {
                                str40 = str41.Split(new char[] { '-' })[1];
                            }
                        }
                        dictionary4.Add(1, "gd-" + str37);
                        dictionary4.Add(2, "zd-" + str38);
                        dictionary4.Add(3, "dl-" + str39);
                        dictionary4.Add(4, "hy-" + str40);
                        List<string> values = (from d in dictionary4
                            orderby d.Key
                            select d).ToDictionary<KeyValuePair<int, string>, int, string>(p => p.Key, o => o.Value).Values.ToList<string>();
                        str36 = string.Join(",", values);
                    }
                    else if (!str11.Equals("hy"))
                    {
                        string zdcount = "";
                        string dlcount = "";
                        string hycount = "";
                        base.GetCount(str27, str11, ref zdcount, ref dlcount, ref hycount);
                        zdcount = (zdcount == "") ? "0" : zdcount;
                        dlcount = (dlcount == "") ? "0" : dlcount;
                        hycount = (hycount == "") ? "0" : hycount;
                        if (str11.Equals("gd"))
                        {
                            str36 = "zd-" + zdcount + ",dl-" + dlcount + ",hy-" + hycount;
                        }
                        else if (str11.Equals("zd"))
                        {
                            str36 = "dl-" + dlcount + ",hy-" + hycount;
                        }
                        else if (str11.Equals("dl"))
                        {
                            str36 = "hy-" + hycount;
                        }
                        else
                        {
                            base.Response.End();
                        }
                    }
                    Dictionary<string, object> dictionary6 = new Dictionary<string, object>();
                    dictionary6.Add("uid", str26);
                    dictionary6.Add("name", str27);
                    dictionary6.Add("userState", row3["a_state"].ToString());
                    dictionary6.Add("supType", str29);
                    dictionary6.Add("online", str30);
                    dictionary6.Add("kcUsableCredit", str34);
                    dictionary6.Add("sixUsableCredit", str35);
                    dictionary6.Add("aboveMember", str36);
                    list.Add(dictionary6);
                }
            }
            dictionary.Add("page", int.Parse(str10) + 1);
            dictionary.Add("group", list);
            result.set_success(200);
            result.set_data(dictionary);
            strResult = base.ObjectToJson(result);
        }

        private void getPersonLog(ref string strResult)
        {
            bool flag = false;
            base.checkLoginByHandler(0);
            ReturnResult result = new ReturnResult();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            List<object> list = new List<object>();
            string str = this.Session["user_name"].ToString();
            agent_userinfo_session _session = this.Session[str + "lottery_session_user_info"] as agent_userinfo_session;
            base.checkCloneRight();
            string str2 = _session.get_u_type().Trim();
            if (this.Session["child_user_name"] != null)
            {
                this.Session["child_user_name"].ToString();
            }
            string str3 = LSRequest.qq("uid");
            if (string.IsNullOrEmpty(str3))
            {
                base.Response.End();
            }
            string userNameByUid = CallBLL.cz_users_bll.GetUserNameByUid(str3, ref flag);
            if (string.IsNullOrEmpty(userNameByUid))
            {
                base.Response.End();
            }
            if (CallBLL.cz_users_bll.GetUserInfoByUID(str3) == null)
            {
                cz_users_child userByUID = CallBLL.cz_users_child_bll.GetUserByUID(str3);
                if ((userByUID != null) && userByUID.get_parent_u_name().Equals(_session.get_u_name()))
                {
                    if (!base.IsUpperLowerLevels(userByUID.get_parent_u_name(), _session.get_u_type(), _session.get_u_name()))
                    {
                        base.Response.End();
                    }
                }
                else
                {
                    base.Response.End();
                }
            }
            else if (!base.IsUpperLowerLevels(userNameByUid, _session.get_u_type(), _session.get_u_name()))
            {
                base.Response.End();
            }
            string str5 = LSRequest.qq("page");
            if (string.IsNullOrEmpty(str5) || (int.Parse(str5) < 1))
            {
                str5 = "1";
            }
            int num = 20;
            int num2 = 0;
            int num3 = 0;
            bool flag2 = true;
            DataTable table = null;
            table = CallBLL.cz_login_log_bll.get_log_table(Convert.ToInt32(str5) - 1, num, ref num3, ref num2, userNameByUid.Trim(), ref flag2);
            if ((table != null) && (table.Rows.Count > 0))
            {
                foreach (DataRow row in table.Rows)
                {
                    Dictionary<string, object> item = new Dictionary<string, object>();
                    item.Add("time", row["login_time"].ToString());
                    item.Add("ip", base.GetIPText(str2, row["ip"].ToString()));
                    item.Add("ipZone", base.GetAreaByIP(row["ip"].ToString()));
                    list.Add(item);
                }
            }
            dictionary.Add("page", int.Parse(str5) + 1);
            dictionary.Add("group", list);
            result.set_success(200);
            result.set_data(dictionary);
            strResult = base.ObjectToJson(result);
        }

        private void getProfit(ref string strResult)
        {
            base.checkLoginByHandler(0);
            base.checkCloneRight();
            ReturnResult result = new ReturnResult();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            new List<object>();
            string str = this.Session["user_name"].ToString();
            agent_userinfo_session _session = this.Session[str + "lottery_session_user_info"] as agent_userinfo_session;
            if (!base.GetMasterLotteryIsOpen(base.GetLotteryList(), 2))
            {
                result.set_success(400);
                result.set_tipinfo("只有快彩具備盈利回收功能！");
                strResult = base.ObjectToJson(result);
            }
            else
            {
                bool flag;
                bool flag2;
                string str2 = LSRequest.qq("uid");
                string str3 = LSRequest.qq("submitType");
                if ((str3 != "view") && (str3 != "edit"))
                {
                    base.Response.End();
                }
                cz_users userInfoByUID = CallBLL.cz_users_bll.GetUserInfoByUID(str2, "hy");
                if (!base.IsUpperLowerLevels(userInfoByUID.get_u_name(), _session.get_u_type(), _session.get_u_name()))
                {
                    base.noRightOptMsg();
                }
                double num = double.Parse(userInfoByUID.get_kc_usable_credit().ToString()) - double.Parse(userInfoByUID.get_kc_credit().ToString());
                if (num < 0.0)
                {
                    num = 0.0;
                }
                base.OpenLotteryMaster(out flag, out flag2);
                switch (str3)
                {
                    case "view":
                    {
                        Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
                        dictionary2.Add("name", userInfoByUID.get_u_name());
                        dictionary2.Add("nicker", userInfoByUID.get_u_nicker());
                        dictionary2.Add("creditKc", userInfoByUID.get_kc_credit());
                        dictionary2.Add("usableCreditKc", userInfoByUID.get_kc_usable_credit());
                        dictionary2.Add("profitCreditKc", num.ToString("G"));
                        dictionary2.Add("usableProfitKc", 0);
                        dictionary = dictionary2;
                        result.set_success(200);
                        result.set_data(dictionary);
                        strResult = base.ObjectToJson(result);
                        break;
                    }
                    case "edit":
                        if (!base.IsCreditLock(userInfoByUID.get_u_name()))
                        {
                            result.set_success(400);
                            result.set_tipinfo("系統繁忙，請稍後！");
                            strResult = base.ObjectToJson(result);
                        }
                        else
                        {
                            DataTable table = CallBLL.cz_rate_six_bll.GetRateByAccount(userInfoByUID.get_u_name()).Tables[0];
                            string str4 = table.Rows[0]["fgs_name"].ToString().Trim();
                            base.En_User_Lock(str4);
                            string s = LSRequest.qq("profitCreditKc");
                            if (flag2)
                            {
                                try
                                {
                                    double.Parse(s);
                                }
                                catch
                                {
                                    base.DeleteCreditLock(userInfoByUID.get_u_name());
                                    base.Un_User_Lock(str4);
                                    result.set_success(400);
                                    result.set_tipinfo("(快)回收額度’輸入錯誤！");
                                    strResult = base.ObjectToJson(result);
                                    return;
                                }
                                if (double.Parse(s) < 0.0)
                                {
                                    base.DeleteCreditLock(userInfoByUID.get_u_name());
                                    base.Un_User_Lock(str4);
                                    result.set_success(400);
                                    result.set_tipinfo("(快)回收額度’不能小於0！");
                                    strResult = base.ObjectToJson(result);
                                    return;
                                }
                                if (double.Parse(s) > num)
                                {
                                    base.DeleteCreditLock(userInfoByUID.get_u_name());
                                    base.Un_User_Lock(str4);
                                    result.set_success(400);
                                    result.set_tipinfo("(快)回收額度’不可高於 ‘盈利額度’！");
                                    strResult = base.ObjectToJson(result);
                                    return;
                                }
                            }
                            double kcProfit = 0.0;
                            double sixProfit = 0.0;
                            List<SqlParameter> paramList = new List<SqlParameter>();
                            string str6 = base.user_hy_profit_log(userInfoByUID, flag2, kcProfit, double.Parse(userInfoByUID.get_kc_usable_credit().ToString()), false, sixProfit, double.Parse(userInfoByUID.get_six_usable_credit().ToString()), ref paramList);
                            if (CallBLL.cz_users_bll.UpdateHyProfit(str2, flag2, kcProfit, false, sixProfit, str6, paramList) != 0)
                            {
                                base.DeleteCreditLock(userInfoByUID.get_u_name());
                                base.Un_User_Lock(str4);
                                result.set_success(220);
                                result.set_tipinfo("盈利額度回收成功！");
                                strResult = base.ObjectToJson(result);
                            }
                            else
                            {
                                base.DeleteCreditLock(userInfoByUID.get_u_name());
                                base.Un_User_Lock(str4);
                                result.set_success(400);
                                result.set_tipinfo("盈利額度回收失敗！");
                                strResult = base.ObjectToJson(result);
                            }
                        }
                        break;
                }
            }
        }

        private void GetRateInfo(string g_uid, string memberType)
        {
            if (!string.IsNullOrEmpty(g_uid))
            {
                Dictionary<string, string> rateByUID = CallBLL.cz_rate_six_bll.GetRateByUID(g_uid, memberType);
                if (rateByUID != null)
                {
                    this.six_rate_d = rateByUID["six_rate"];
                    this.six_credit_d = rateByUID["six_credit"];
                    this.six_usable_credit_d = rateByUID["six_usable_credit"];
                    this.six_allow_sale_d = rateByUID["six_allow_sale"];
                    this.six_u_nicker_d = rateByUID["six_u_nicker"];
                    this.six_kind_d = rateByUID["six_kind"];
                    this.u_name = rateByUID["u_name"];
                    this.six_zj_rate = rateByUID["six_zj_rate"];
                    this.six_fgs_rate = rateByUID["six_fgs_rate"];
                    this.six_gd_rate = rateByUID["six_gd_rate"];
                    this.six_zd_rate = rateByUID["six_zd_rate"];
                    this.six_dl_rate = rateByUID["six_dl_rate"];
                    this.six_iscash = rateByUID["six_iscash"];
                }
                Dictionary<string, string> dictionary2 = CallBLL.cz_rate_kc_bll.GetRateByUID(g_uid, memberType);
                if (dictionary2 != null)
                {
                    this.kc_rate_d = dictionary2["kc_rate"];
                    this.kc_credit_d = dictionary2["kc_credit"];
                    this.kc_usable_credit_d = dictionary2["kc_usable_credit"];
                    this.kc_allow_sale_d = dictionary2["kc_allow_sale"];
                    this.kc_u_nicker_d = dictionary2["kc_u_nicker"];
                    this.kc_kind_d = dictionary2["kc_kind"];
                    this.u_name = dictionary2["u_name"];
                    this.kc_zj_rate = dictionary2["kc_zj_rate"];
                    this.kc_fgs_rate = dictionary2["kc_fgs_rate"];
                    this.kc_gd_rate = dictionary2["kc_gd_rate"];
                    this.kc_zd_rate = dictionary2["kc_zd_rate"];
                    this.kc_dl_rate = dictionary2["kc_dl_rate"];
                    this.kc_iscash = dictionary2["kc_iscash"];
                }
            }
        }

        private void initClone(ref string strResult)
        {
            ReturnResult result = new ReturnResult();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            List<object> list = new List<object>();
            new List<object>();
            string str = this.Session["user_name"].ToString();
            agent_userinfo_session _session = this.Session[str + "lottery_session_user_info"] as agent_userinfo_session;
            DataRow[] rowArray = null;
            DataRow[] rowArray2 = null;
            DataRow[] rowArray3 = null;
            DataRow[] rowArray4 = null;
            DataRow[] rowArray5 = null;
            DataRow[] rowArray6 = null;
            DataRow[] rowArray7 = null;
            DataTable table = CallBLL.cz_permissions_bll.GetListByUType((_session.get_u_type().Equals("zj") != null) ? "zj" : "dl").Tables[0];
            if (_session.get_u_type().Equals("zj"))
            {
                rowArray = table.Select(string.Format(" group_id={0} ", 1));
                rowArray2 = table.Select(string.Format(" group_id={0} ", 2));
                rowArray3 = table.Select(string.Format(" group_id={0} ", 3));
                rowArray4 = table.Select(string.Format(" group_id={0} ", 4));
            }
            else
            {
                if (_session.get_u_type().Equals("fgs"))
                {
                    if (_session.get_six_op_odds().Equals(1) || _session.get_kc_op_odds().Equals(1))
                    {
                        rowArray5 = table.Select(string.Format(" group_id={0} ", 5));
                    }
                    else
                    {
                        rowArray5 = table.Select(string.Format(" group_id={0} and name<>'{1}' ", 5, "po_5_3"));
                    }
                }
                else
                {
                    rowArray5 = table.Select(string.Format(" group_id={0} and name<>'{1}' ", 5, "po_5_3"));
                }
                rowArray6 = table.Select(string.Format(" group_id={0} ", 6));
                rowArray7 = table.Select(string.Format(" group_id={0} ", 7));
            }
            if (_session.get_u_type().Equals("zj"))
            {
                foreach (DataRow row in rowArray)
                {
                    string str2 = row["name"].ToString();
                    string str3 = row["name_remark"].ToString();
                    Dictionary<string, object> item = new Dictionary<string, object>();
                    item.Add("value", str2);
                    item.Add("label", str3);
                    list.Add(item);
                }
                foreach (DataRow row2 in rowArray2)
                {
                    string str4 = row2["name"].ToString();
                    string str5 = row2["name_remark"].ToString();
                    Dictionary<string, object> dictionary3 = new Dictionary<string, object>();
                    dictionary3.Add("value", str4);
                    dictionary3.Add("label", str5);
                    list.Add(dictionary3);
                }
                foreach (DataRow row3 in rowArray3)
                {
                    string str6 = row3["name"].ToString();
                    string str7 = row3["name_remark"].ToString();
                    Dictionary<string, object> dictionary4 = new Dictionary<string, object>();
                    dictionary4.Add("value", str6.ToString());
                    dictionary4.Add("label", str7);
                    list.Add(dictionary4);
                }
                foreach (DataRow row4 in rowArray4)
                {
                    string str8 = row4["name"].ToString();
                    string str9 = row4["name_remark"].ToString();
                    Dictionary<string, object> dictionary5 = new Dictionary<string, object>();
                    dictionary5.Add("value", str8);
                    dictionary5.Add("label", str9);
                    list.Add(dictionary5);
                }
            }
            else
            {
                foreach (DataRow row5 in rowArray5)
                {
                    string str10 = row5["name"].ToString();
                    string str11 = row5["name_remark"].ToString();
                    Dictionary<string, object> dictionary6 = new Dictionary<string, object>();
                    dictionary6.Add("value", str10);
                    dictionary6.Add("label", str11);
                    list.Add(dictionary6);
                }
                foreach (DataRow row6 in rowArray6)
                {
                    string str12 = row6["name"].ToString();
                    string str13 = row6["name_remark"].ToString();
                    Dictionary<string, object> dictionary7 = new Dictionary<string, object>();
                    dictionary7.Add("value", str12);
                    dictionary7.Add("label", str13);
                    list.Add(dictionary7);
                }
                foreach (DataRow row7 in rowArray7)
                {
                    string str14 = row7["name"].ToString();
                    string str15 = row7["name_remark"].ToString();
                    Dictionary<string, object> dictionary8 = new Dictionary<string, object>();
                    dictionary8.Add("value", str14);
                    dictionary8.Add("label", str15);
                    list.Add(dictionary8);
                }
            }
            dictionary.Add("qxOption", list);
            result.set_success(200);
            result.set_data(dictionary);
            strResult = base.ObjectToJson(result);
        }

        private void initDL(ref string strResult)
        {
            bool flag;
            bool flag2;
            string str = this.Session["user_name"].ToString();
            agent_userinfo_session _session = this.Session[str + "lottery_session_user_info"] as agent_userinfo_session;
            ReturnResult result = new ReturnResult();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            List<object> list = new List<object>();
            base.OpenLotteryMaster(out flag, out flag2);
            string str2 = "";
            string str3 = "";
            string str4 = LSRequest.qq("sltuid");
            str3 = LSRequest.qq("uid");
            if (string.IsNullOrEmpty(str4))
            {
                str4 = str3;
            }
            if (string.IsNullOrEmpty(str3))
            {
                string str5 = "";
                if (_session.get_u_type().Equals("zj"))
                {
                    str5 = "";
                }
                else
                {
                    str5 = " and " + _session.get_u_type() + "_name='" + _session.get_u_name() + "'";
                }
                str2 = "select u_name from  cz_rate_six  where u_type='zd' " + str5;
            }
            else
            {
                cz_users userInfoByUID = CallBLL.cz_users_bll.GetUserInfoByUID(str3);
                str2 = string.Format("select u_name from cz_rate_six where u_type='zd' and {0}_name='{1}'", userInfoByUID.get_u_type(), userInfoByUID.get_u_name());
            }
            DataTable userListByAddUser = CallBLL.cz_users_bll.GetUserListByAddUser(str2, "zd");
            if (userListByAddUser == null)
            {
                base.noRightOptMsg(PageBase.GetMessageByCache("u100058", "MessageHint"));
            }
            else
            {
                for (int i = 0; i < userListByAddUser.Rows.Count; i++)
                {
                    if (string.IsNullOrEmpty(str4))
                    {
                        if (i == 0)
                        {
                            Dictionary<string, object> item = new Dictionary<string, object>();
                            item.Add("value", userListByAddUser.Rows[i]["u_id"].ToString());
                            item.Add("label", userListByAddUser.Rows[i]["u_name"].ToString());
                            item.Add("nicker", userListByAddUser.Rows[i]["u_nicker"].ToString());
                            list.Add(item);
                            this.GetRateInfo(userListByAddUser.Rows[i]["u_id"].ToString(), "zd");
                            if (flag && flag2)
                            {
                                Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
                                dictionary2.Add("supUsableCredit", this.six_usable_credit_d);
                                dictionary2.Add("userMaxRate", this.six_rate_d);
                                dictionary2.Add("isAllowSale", this.six_allow_sale_d);
                                dictionary2.Add("userKind", this.six_kind_d);
                                dictionary2.Add("isCash", this.six_iscash);
                                dictionary.Add("Six", dictionary2);
                                Dictionary<string, object> dictionary3 = new Dictionary<string, object>();
                                dictionary3.Add("supUsableCredit", this.kc_usable_credit_d);
                                dictionary3.Add("userMaxRate", this.kc_rate_d);
                                dictionary3.Add("isAllowSale", this.kc_allow_sale_d);
                                dictionary3.Add("userKind", this.kc_kind_d);
                                dictionary3.Add("isCash", this.kc_iscash);
                                dictionary.Add("Kc", dictionary3);
                            }
                            else if (flag)
                            {
                                Dictionary<string, object> dictionary4 = new Dictionary<string, object>();
                                dictionary4.Add("supUsableCredit", this.six_usable_credit_d);
                                dictionary4.Add("userMaxRate", this.six_rate_d);
                                dictionary4.Add("isAllowSale", this.six_allow_sale_d);
                                dictionary4.Add("userKind", this.six_kind_d);
                                dictionary4.Add("isCash", this.six_iscash);
                                dictionary.Add("Six", dictionary4);
                            }
                            else if (flag2)
                            {
                                Dictionary<string, object> dictionary5 = new Dictionary<string, object>();
                                dictionary5.Add("supUsableCredit", this.kc_usable_credit_d);
                                dictionary5.Add("userMaxRate", this.kc_rate_d);
                                dictionary5.Add("isAllowSale", this.kc_allow_sale_d);
                                dictionary5.Add("userKind", this.kc_kind_d);
                                dictionary5.Add("isCash", this.kc_iscash);
                                dictionary.Add("Kc", dictionary5);
                            }
                        }
                        else
                        {
                            Dictionary<string, object> dictionary7 = new Dictionary<string, object>();
                            dictionary7.Add("value", userListByAddUser.Rows[i]["u_id"].ToString());
                            dictionary7.Add("label", userListByAddUser.Rows[i]["u_name"].ToString());
                            dictionary7.Add("nicker", userListByAddUser.Rows[i]["u_nicker"].ToString());
                            list.Add(dictionary7);
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(str4) && str4.ToUpper().Equals(userListByAddUser.Rows[i]["u_id"].ToString().ToUpper()))
                        {
                            Dictionary<string, object> dictionary12 = new Dictionary<string, object>();
                            dictionary12.Add("value", userListByAddUser.Rows[i]["u_id"].ToString());
                            dictionary12.Add("label", userListByAddUser.Rows[i]["u_name"].ToString());
                            dictionary12.Add("nicker", userListByAddUser.Rows[i]["u_nicker"].ToString());
                            list.Add(dictionary12);
                            this.GetRateInfo(str4, "zd");
                            if (flag && flag2)
                            {
                                Dictionary<string, object> dictionary8 = new Dictionary<string, object>();
                                dictionary8.Add("supUsableCredit", this.six_usable_credit_d);
                                dictionary8.Add("userMaxRate", this.six_rate_d);
                                dictionary8.Add("isAllowSale", this.six_allow_sale_d);
                                dictionary8.Add("userKind", this.six_kind_d);
                                dictionary8.Add("isCash", this.six_iscash);
                                dictionary.Add("Six", dictionary8);
                                Dictionary<string, object> dictionary9 = new Dictionary<string, object>();
                                dictionary9.Add("supUsableCredit", this.kc_usable_credit_d);
                                dictionary9.Add("userMaxRate", this.kc_rate_d);
                                dictionary9.Add("isAllowSale", this.kc_allow_sale_d);
                                dictionary9.Add("userKind", this.kc_kind_d);
                                dictionary9.Add("isCash", this.kc_iscash);
                                dictionary.Add("Kc", dictionary9);
                            }
                            else if (flag)
                            {
                                Dictionary<string, object> dictionary10 = new Dictionary<string, object>();
                                dictionary10.Add("supUsableCredit", this.six_usable_credit_d);
                                dictionary10.Add("userMaxRate", this.six_rate_d);
                                dictionary10.Add("isAllowSale", this.six_allow_sale_d);
                                dictionary10.Add("userKind", this.six_kind_d);
                                dictionary10.Add("isCash", this.six_iscash);
                                dictionary.Add("Six", dictionary10);
                            }
                            else if (flag2)
                            {
                                Dictionary<string, object> dictionary11 = new Dictionary<string, object>();
                                dictionary11.Add("supUsableCredit", this.kc_usable_credit_d);
                                dictionary11.Add("userMaxRate", this.kc_rate_d);
                                dictionary11.Add("isAllowSale", this.kc_allow_sale_d);
                                dictionary11.Add("userKind", this.kc_kind_d);
                                dictionary11.Add("isCash", this.kc_iscash);
                                dictionary.Add("Kc", dictionary11);
                            }
                        }
                        if (!str4.ToUpper().Equals(userListByAddUser.Rows[i]["u_id"].ToString().ToUpper()))
                        {
                            Dictionary<string, object> dictionary13 = new Dictionary<string, object>();
                            dictionary13.Add("value", userListByAddUser.Rows[i]["u_id"].ToString());
                            dictionary13.Add("label", userListByAddUser.Rows[i]["u_name"].ToString());
                            dictionary13.Add("nicker", userListByAddUser.Rows[i]["u_nicker"].ToString());
                            list.Add(dictionary13);
                        }
                    }
                }
                dictionary.Add("nameList", list);
                result.set_success(200);
                result.set_data(dictionary);
                strResult = base.ObjectToJson(result);
            }
        }

        private void initFGS(ref string strResult)
        {
            ReturnResult result = new ReturnResult();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            new List<object>();
            dictionary.Add("displayFgsWT", base.get_GetIsShowFgsWT().Equals("0") ? 0 : 1);
            result.set_success(200);
            result.set_data(dictionary);
            strResult = base.ObjectToJson(result);
        }

        private void initGD(ref string strResult)
        {
            bool flag;
            bool flag2;
            string str = this.Session["user_name"].ToString();
            agent_userinfo_session _session = this.Session[str + "lottery_session_user_info"] as agent_userinfo_session;
            ReturnResult result = new ReturnResult();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            List<object> list = new List<object>();
            base.OpenLotteryMaster(out flag, out flag2);
            string str2 = "";
            string str3 = "";
            string str4 = LSRequest.qq("sltuid");
            str3 = LSRequest.qq("uid");
            if (string.IsNullOrEmpty(str4))
            {
                str4 = str3;
            }
            if (string.IsNullOrEmpty(str3))
            {
                string str5 = "";
                if (_session.get_u_type().Equals("zj"))
                {
                    str5 = "";
                }
                else
                {
                    str5 = " and " + _session.get_u_type() + "_name='" + _session.get_u_name() + "'";
                }
                str2 = "select u_name from  cz_rate_six  where u_type='fgs' " + str5;
            }
            else
            {
                cz_users userInfoByUID = CallBLL.cz_users_bll.GetUserInfoByUID(str3);
                str2 = string.Format("select u_name from cz_rate_six where u_type='fgs' and {0}_name='{1}'", userInfoByUID.get_u_type(), userInfoByUID.get_u_name());
            }
            DataTable userListByAddUser = CallBLL.cz_users_bll.GetUserListByAddUser(str2, "fgs");
            if (userListByAddUser == null)
            {
                base.noRightOptMsg(PageBase.GetMessageByCache("u100058", "MessageHint"));
            }
            else
            {
                for (int i = 0; i < userListByAddUser.Rows.Count; i++)
                {
                    if (string.IsNullOrEmpty(str4))
                    {
                        if (i == 0)
                        {
                            Dictionary<string, object> item = new Dictionary<string, object>();
                            item.Add("value", userListByAddUser.Rows[i]["u_id"].ToString());
                            item.Add("label", userListByAddUser.Rows[i]["u_name"].ToString());
                            item.Add("nicker", userListByAddUser.Rows[i]["u_nicker"].ToString());
                            list.Add(item);
                            this.GetRateInfo(userListByAddUser.Rows[i]["u_id"].ToString(), "fgs");
                            if (flag && flag2)
                            {
                                Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
                                dictionary2.Add("supUsableCredit", this.six_usable_credit_d);
                                dictionary2.Add("userMaxRate", this.six_rate_d);
                                dictionary2.Add("isAllowSale", this.six_allow_sale_d);
                                dictionary2.Add("userKind", this.six_kind_d);
                                dictionary2.Add("isCash", this.six_iscash);
                                dictionary.Add("Six", dictionary2);
                                Dictionary<string, object> dictionary3 = new Dictionary<string, object>();
                                dictionary3.Add("supUsableCredit", this.kc_usable_credit_d);
                                dictionary3.Add("userMaxRate", this.kc_rate_d);
                                dictionary3.Add("isAllowSale", this.kc_allow_sale_d);
                                dictionary3.Add("userKind", this.kc_kind_d);
                                dictionary3.Add("isCash", this.kc_iscash);
                                dictionary.Add("Kc", dictionary3);
                            }
                            else if (flag)
                            {
                                Dictionary<string, object> dictionary4 = new Dictionary<string, object>();
                                dictionary4.Add("supUsableCredit", this.six_usable_credit_d);
                                dictionary4.Add("userMaxRate", this.six_rate_d);
                                dictionary4.Add("isAllowSale", this.six_allow_sale_d);
                                dictionary4.Add("userKind", this.six_kind_d);
                                dictionary4.Add("isCash", this.six_iscash);
                                dictionary.Add("Six", dictionary4);
                            }
                            else if (flag2)
                            {
                                Dictionary<string, object> dictionary5 = new Dictionary<string, object>();
                                dictionary5.Add("supUsableCredit", this.kc_usable_credit_d);
                                dictionary5.Add("userMaxRate", this.kc_rate_d);
                                dictionary5.Add("isAllowSale", this.kc_allow_sale_d);
                                dictionary5.Add("userKind", this.kc_kind_d);
                                dictionary5.Add("isCash", this.kc_iscash);
                                dictionary.Add("Kc", dictionary5);
                            }
                        }
                        else
                        {
                            Dictionary<string, object> dictionary7 = new Dictionary<string, object>();
                            dictionary7.Add("value", userListByAddUser.Rows[i]["u_id"].ToString());
                            dictionary7.Add("label", userListByAddUser.Rows[i]["u_name"].ToString());
                            dictionary7.Add("nicker", userListByAddUser.Rows[i]["u_nicker"].ToString());
                            list.Add(dictionary7);
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(str4) && str4.ToUpper().Equals(userListByAddUser.Rows[i]["u_id"].ToString().ToUpper()))
                        {
                            Dictionary<string, object> dictionary12 = new Dictionary<string, object>();
                            dictionary12.Add("value", userListByAddUser.Rows[i]["u_id"].ToString());
                            dictionary12.Add("label", userListByAddUser.Rows[i]["u_name"].ToString());
                            dictionary12.Add("nicker", userListByAddUser.Rows[i]["u_nicker"].ToString());
                            list.Add(dictionary12);
                            this.GetRateInfo(str4, "fgs");
                            if (flag && flag2)
                            {
                                Dictionary<string, object> dictionary8 = new Dictionary<string, object>();
                                dictionary8.Add("supUsableCredit", this.six_usable_credit_d);
                                dictionary8.Add("userMaxRate", this.six_rate_d);
                                dictionary8.Add("isAllowSale", this.six_allow_sale_d);
                                dictionary8.Add("userKind", this.six_kind_d);
                                dictionary8.Add("isCash", this.six_iscash);
                                dictionary.Add("Six", dictionary8);
                                Dictionary<string, object> dictionary9 = new Dictionary<string, object>();
                                dictionary9.Add("supUsableCredit", this.kc_usable_credit_d);
                                dictionary9.Add("userMaxRate", this.kc_rate_d);
                                dictionary9.Add("isAllowSale", this.kc_allow_sale_d);
                                dictionary9.Add("userKind", this.kc_kind_d);
                                dictionary9.Add("isCash", this.kc_iscash);
                                dictionary.Add("Kc", dictionary9);
                            }
                            else if (flag)
                            {
                                Dictionary<string, object> dictionary10 = new Dictionary<string, object>();
                                dictionary10.Add("supUsableCredit", this.six_usable_credit_d);
                                dictionary10.Add("userMaxRate", this.six_rate_d);
                                dictionary10.Add("isAllowSale", this.six_allow_sale_d);
                                dictionary10.Add("userKind", this.six_kind_d);
                                dictionary10.Add("isCash", this.six_iscash);
                                dictionary.Add("Six", dictionary10);
                            }
                            else if (flag2)
                            {
                                Dictionary<string, object> dictionary11 = new Dictionary<string, object>();
                                dictionary11.Add("supUsableCredit", this.kc_usable_credit_d);
                                dictionary11.Add("userMaxRate", this.kc_rate_d);
                                dictionary11.Add("isAllowSale", this.kc_allow_sale_d);
                                dictionary11.Add("userKind", this.kc_kind_d);
                                dictionary11.Add("isCash", this.kc_iscash);
                                dictionary.Add("Kc", dictionary11);
                            }
                        }
                        if (!str4.ToUpper().Equals(userListByAddUser.Rows[i]["u_id"].ToString().ToUpper()))
                        {
                            Dictionary<string, object> dictionary13 = new Dictionary<string, object>();
                            dictionary13.Add("value", userListByAddUser.Rows[i]["u_id"].ToString());
                            dictionary13.Add("label", userListByAddUser.Rows[i]["u_name"].ToString());
                            dictionary13.Add("nicker", userListByAddUser.Rows[i]["u_nicker"].ToString());
                            list.Add(dictionary13);
                        }
                    }
                }
                dictionary.Add("nameList", list);
                result.set_success(200);
                result.set_data(dictionary);
                strResult = base.ObjectToJson(result);
            }
        }

        private void initHY(ref string strResult)
        {
            bool flag;
            bool flag2;
            string str = this.Session["user_name"].ToString();
            agent_userinfo_session _session = this.Session[str + "lottery_session_user_info"] as agent_userinfo_session;
            ReturnResult result = new ReturnResult();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            List<object> list = new List<object>();
            base.OpenLotteryMaster(out flag, out flag2);
            string str2 = "";
            string str3 = "";
            string str4 = LSRequest.qq("sltuid");
            str3 = LSRequest.qq("uid");
            if (string.IsNullOrEmpty(str3))
            {
                string str5 = "";
                if (_session.get_u_type().Equals("zj"))
                {
                    str5 = "";
                }
                else
                {
                    str5 = " and " + _session.get_u_type() + "_name='" + _session.get_u_name() + "'";
                }
                str2 = "select u_name from  cz_rate_six  where u_type='dl' " + str5;
            }
            else
            {
                cz_users userInfoByUID = CallBLL.cz_users_bll.GetUserInfoByUID(str3);
                str2 = string.Format("select u_name from cz_rate_six where u_type='dl' and {0}_name='{1}'", userInfoByUID.get_u_type(), userInfoByUID.get_u_name());
            }
            DataTable userListByAddUser = CallBLL.cz_users_bll.GetUserListByAddUser(str2, "dl");
            if (userListByAddUser == null)
            {
                base.noRightOptMsg(PageBase.GetMessageByCache("u100058", "MessageHint"));
            }
            else
            {
                for (int i = 0; i < userListByAddUser.Rows.Count; i++)
                {
                    if (string.IsNullOrEmpty(str4))
                    {
                        if (i == 0)
                        {
                            Dictionary<string, object> item = new Dictionary<string, object>();
                            item.Add("value", userListByAddUser.Rows[i]["u_id"].ToString());
                            item.Add("label", userListByAddUser.Rows[i]["u_name"].ToString());
                            item.Add("nicker", userListByAddUser.Rows[i]["u_nicker"].ToString());
                            list.Add(item);
                            this.GetRateInfo(userListByAddUser.Rows[i]["u_id"].ToString(), "dl");
                            if (flag && flag2)
                            {
                                Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
                                dictionary2.Add("supUsableCredit", this.six_usable_credit_d);
                                dictionary2.Add("userMaxRate", this.six_rate_d);
                                dictionary2.Add("userKind", this.six_kind_d);
                                dictionary2.Add("isCash", this.six_iscash);
                                dictionary.Add("Six", dictionary2);
                                Dictionary<string, object> dictionary3 = new Dictionary<string, object>();
                                dictionary3.Add("supUsableCredit", this.kc_usable_credit_d);
                                dictionary3.Add("userMaxRate", this.kc_rate_d);
                                dictionary3.Add("userKind", this.kc_kind_d);
                                dictionary3.Add("isCash", this.kc_iscash);
                                dictionary.Add("Kc", dictionary3);
                            }
                            else if (flag)
                            {
                                Dictionary<string, object> dictionary4 = new Dictionary<string, object>();
                                dictionary4.Add("supUsableCredit", this.six_usable_credit_d);
                                dictionary4.Add("userMaxRate", this.six_rate_d);
                                dictionary4.Add("userKind", this.six_kind_d);
                                dictionary4.Add("isCash", this.six_iscash);
                                dictionary.Add("Six", dictionary4);
                            }
                            else if (flag2)
                            {
                                Dictionary<string, object> dictionary5 = new Dictionary<string, object>();
                                dictionary5.Add("supUsableCredit", this.kc_usable_credit_d);
                                dictionary5.Add("userMaxRate", this.kc_rate_d);
                                dictionary5.Add("userKind", this.kc_kind_d);
                                dictionary5.Add("isCash", this.kc_iscash);
                                dictionary.Add("Kc", dictionary5);
                            }
                        }
                        else
                        {
                            Dictionary<string, object> dictionary7 = new Dictionary<string, object>();
                            dictionary7.Add("value", userListByAddUser.Rows[i]["u_id"].ToString());
                            dictionary7.Add("label", userListByAddUser.Rows[i]["u_name"].ToString());
                            dictionary7.Add("nicker", userListByAddUser.Rows[i]["u_nicker"].ToString());
                            list.Add(dictionary7);
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(str4) && str4.ToUpper().Equals(userListByAddUser.Rows[i]["u_id"].ToString().ToUpper()))
                        {
                            Dictionary<string, object> dictionary12 = new Dictionary<string, object>();
                            dictionary12.Add("value", userListByAddUser.Rows[i]["u_id"].ToString());
                            dictionary12.Add("label", userListByAddUser.Rows[i]["u_name"].ToString());
                            dictionary12.Add("nicker", userListByAddUser.Rows[i]["u_nicker"].ToString());
                            list.Add(dictionary12);
                            this.GetRateInfo(str4, "dl");
                            if (flag && flag2)
                            {
                                Dictionary<string, object> dictionary8 = new Dictionary<string, object>();
                                dictionary8.Add("supUsableCredit", this.six_usable_credit_d);
                                dictionary8.Add("userMaxRate", this.six_rate_d);
                                dictionary8.Add("userKind", this.six_kind_d);
                                dictionary8.Add("isCash", this.six_iscash);
                                dictionary.Add("Six", dictionary8);
                                Dictionary<string, object> dictionary9 = new Dictionary<string, object>();
                                dictionary9.Add("supUsableCredit", this.kc_usable_credit_d);
                                dictionary9.Add("userMaxRate", this.kc_rate_d);
                                dictionary9.Add("userKind", this.kc_kind_d);
                                dictionary9.Add("isCash", this.kc_iscash);
                                dictionary.Add("Kc", dictionary9);
                            }
                            else if (flag)
                            {
                                Dictionary<string, object> dictionary10 = new Dictionary<string, object>();
                                dictionary10.Add("supUsableCredit", this.six_usable_credit_d);
                                dictionary10.Add("userMaxRate", this.six_rate_d);
                                dictionary10.Add("userKind", this.six_kind_d);
                                dictionary10.Add("isCash", this.six_iscash);
                                dictionary.Add("Six", dictionary10);
                            }
                            else if (flag2)
                            {
                                Dictionary<string, object> dictionary11 = new Dictionary<string, object>();
                                dictionary11.Add("supUsableCredit", this.kc_usable_credit_d);
                                dictionary11.Add("userMaxRate", this.kc_rate_d);
                                dictionary11.Add("userKind", this.kc_kind_d);
                                dictionary11.Add("isCash", this.kc_iscash);
                                dictionary.Add("Kc", dictionary11);
                            }
                        }
                        if (!str4.ToUpper().Equals(userListByAddUser.Rows[i]["u_id"].ToString().ToUpper()))
                        {
                            Dictionary<string, object> dictionary13 = new Dictionary<string, object>();
                            dictionary13.Add("value", userListByAddUser.Rows[i]["u_id"].ToString());
                            dictionary13.Add("label", userListByAddUser.Rows[i]["u_name"].ToString());
                            dictionary13.Add("nicker", userListByAddUser.Rows[i]["u_nicker"].ToString());
                            list.Add(dictionary13);
                        }
                    }
                }
                dictionary.Add("nameList", list);
                result.set_success(200);
                result.set_data(dictionary);
                strResult = base.ObjectToJson(result);
            }
        }

        private void initZD(ref string strResult)
        {
            bool flag;
            bool flag2;
            string str = this.Session["user_name"].ToString();
            agent_userinfo_session _session = this.Session[str + "lottery_session_user_info"] as agent_userinfo_session;
            ReturnResult result = new ReturnResult();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            List<object> list = new List<object>();
            base.OpenLotteryMaster(out flag, out flag2);
            string str2 = "";
            string str3 = "";
            string str4 = LSRequest.qq("sltuid");
            str3 = LSRequest.qq("uid");
            if (string.IsNullOrEmpty(str4))
            {
                str4 = str3;
            }
            if (string.IsNullOrEmpty(str3))
            {
                string str5 = "";
                if (_session.get_u_type().Equals("zj"))
                {
                    str5 = "";
                }
                else
                {
                    str5 = " and " + _session.get_u_type() + "_name='" + _session.get_u_name() + "'";
                }
                str2 = "select u_name from  cz_rate_six  where u_type='gd' " + str5;
            }
            else
            {
                cz_users userInfoByUID = CallBLL.cz_users_bll.GetUserInfoByUID(str3);
                str2 = string.Format("select u_name from cz_rate_six where u_type='gd' and {0}_name='{1}'", userInfoByUID.get_u_type(), userInfoByUID.get_u_name());
            }
            DataTable userListByAddUser = CallBLL.cz_users_bll.GetUserListByAddUser(str2, "gd");
            if (userListByAddUser == null)
            {
                base.noRightOptMsg(PageBase.GetMessageByCache("u100058", "MessageHint"));
            }
            else
            {
                for (int i = 0; i < userListByAddUser.Rows.Count; i++)
                {
                    if (string.IsNullOrEmpty(str4))
                    {
                        if (i == 0)
                        {
                            Dictionary<string, object> item = new Dictionary<string, object>();
                            item.Add("value", userListByAddUser.Rows[i]["u_id"].ToString());
                            item.Add("label", userListByAddUser.Rows[i]["u_name"].ToString());
                            item.Add("nicker", userListByAddUser.Rows[i]["u_nicker"].ToString());
                            list.Add(item);
                            this.GetRateInfo(userListByAddUser.Rows[i]["u_id"].ToString(), "gd");
                            if (flag && flag2)
                            {
                                Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
                                dictionary2.Add("supUsableCredit", this.six_usable_credit_d);
                                dictionary2.Add("userMaxRate", this.six_rate_d);
                                dictionary2.Add("isAllowSale", this.six_allow_sale_d);
                                dictionary2.Add("userKind", this.six_kind_d);
                                dictionary2.Add("isCash", this.six_iscash);
                                dictionary.Add("Six", dictionary2);
                                Dictionary<string, object> dictionary3 = new Dictionary<string, object>();
                                dictionary3.Add("supUsableCredit", this.kc_usable_credit_d);
                                dictionary3.Add("userMaxRate", this.kc_rate_d);
                                dictionary3.Add("isAllowSale", this.kc_allow_sale_d);
                                dictionary3.Add("userKind", this.kc_kind_d);
                                dictionary3.Add("isCash", this.kc_iscash);
                                dictionary.Add("Kc", dictionary3);
                            }
                            else if (flag)
                            {
                                Dictionary<string, object> dictionary4 = new Dictionary<string, object>();
                                dictionary4.Add("supUsableCredit", this.six_usable_credit_d);
                                dictionary4.Add("userMaxRate", this.six_rate_d);
                                dictionary4.Add("isAllowSale", this.six_allow_sale_d);
                                dictionary4.Add("userKind", this.six_kind_d);
                                dictionary4.Add("isCash", this.six_iscash);
                                dictionary.Add("Six", dictionary4);
                            }
                            else if (flag2)
                            {
                                Dictionary<string, object> dictionary5 = new Dictionary<string, object>();
                                dictionary5.Add("supUsableCredit", this.kc_usable_credit_d);
                                dictionary5.Add("userMaxRate", this.kc_rate_d);
                                dictionary5.Add("isAllowSale", this.kc_allow_sale_d);
                                dictionary5.Add("userKind", this.kc_kind_d);
                                dictionary5.Add("isCash", this.kc_iscash);
                                dictionary.Add("Kc", dictionary5);
                            }
                        }
                        else
                        {
                            Dictionary<string, object> dictionary7 = new Dictionary<string, object>();
                            dictionary7.Add("value", userListByAddUser.Rows[i]["u_id"].ToString());
                            dictionary7.Add("label", userListByAddUser.Rows[i]["u_name"].ToString());
                            dictionary7.Add("nicker", userListByAddUser.Rows[i]["u_nicker"].ToString());
                            list.Add(dictionary7);
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(str4) && str4.ToUpper().Equals(userListByAddUser.Rows[i]["u_id"].ToString().ToUpper()))
                        {
                            Dictionary<string, object> dictionary12 = new Dictionary<string, object>();
                            dictionary12.Add("value", userListByAddUser.Rows[i]["u_id"].ToString());
                            dictionary12.Add("label", userListByAddUser.Rows[i]["u_name"].ToString());
                            dictionary12.Add("nicker", userListByAddUser.Rows[i]["u_nicker"].ToString());
                            list.Add(dictionary12);
                            this.GetRateInfo(str4, "gd");
                            if (flag && flag2)
                            {
                                Dictionary<string, object> dictionary8 = new Dictionary<string, object>();
                                dictionary8.Add("supUsableCredit", this.six_usable_credit_d);
                                dictionary8.Add("userMaxRate", this.six_rate_d);
                                dictionary8.Add("isAllowSale", this.six_allow_sale_d);
                                dictionary8.Add("userKind", this.six_kind_d);
                                dictionary8.Add("isCash", this.six_iscash);
                                dictionary.Add("Six", dictionary8);
                                Dictionary<string, object> dictionary9 = new Dictionary<string, object>();
                                dictionary9.Add("supUsableCredit", this.kc_usable_credit_d);
                                dictionary9.Add("userMaxRate", this.kc_rate_d);
                                dictionary9.Add("isAllowSale", this.kc_allow_sale_d);
                                dictionary9.Add("userKind", this.kc_kind_d);
                                dictionary9.Add("isCash", this.kc_iscash);
                                dictionary.Add("Kc", dictionary9);
                            }
                            else if (flag)
                            {
                                Dictionary<string, object> dictionary10 = new Dictionary<string, object>();
                                dictionary10.Add("supUsableCredit", this.six_usable_credit_d);
                                dictionary10.Add("userMaxRate", this.six_rate_d);
                                dictionary10.Add("isAllowSale", this.six_allow_sale_d);
                                dictionary10.Add("userKind", this.six_kind_d);
                                dictionary10.Add("isCash", this.six_iscash);
                                dictionary.Add("Six", dictionary10);
                            }
                            else if (flag2)
                            {
                                Dictionary<string, object> dictionary11 = new Dictionary<string, object>();
                                dictionary11.Add("supUsableCredit", this.kc_usable_credit_d);
                                dictionary11.Add("userMaxRate", this.kc_rate_d);
                                dictionary11.Add("isAllowSale", this.kc_allow_sale_d);
                                dictionary11.Add("userKind", this.kc_kind_d);
                                dictionary11.Add("isCash", this.kc_iscash);
                                dictionary.Add("Kc", dictionary11);
                            }
                        }
                        if (!str4.ToUpper().Equals(userListByAddUser.Rows[i]["u_id"].ToString().ToUpper()))
                        {
                            Dictionary<string, object> dictionary13 = new Dictionary<string, object>();
                            dictionary13.Add("value", userListByAddUser.Rows[i]["u_id"].ToString());
                            dictionary13.Add("label", userListByAddUser.Rows[i]["u_name"].ToString());
                            dictionary13.Add("nicker", userListByAddUser.Rows[i]["u_nicker"].ToString());
                            list.Add(dictionary13);
                        }
                    }
                }
                dictionary.Add("nameList", list);
                result.set_success(200);
                result.set_data(dictionary);
                strResult = base.ObjectToJson(result);
            }
        }

        private void initZSHY(ref string strResult)
        {
            bool flag;
            bool flag2;
            string str8;
            string str9;
            string str = this.Session["user_name"].ToString();
            agent_userinfo_session _session = this.Session[str + "lottery_session_user_info"] as agent_userinfo_session;
            ReturnResult result = new ReturnResult();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            List<object> list = new List<object>();
            base.OpenLotteryMaster(out flag, out flag2);
            string str2 = "";
            string str3 = "";
            string str4 = LSRequest.qq("sltuid");
            str3 = LSRequest.qq("uid");
            string str5 = LSRequest.qq("rdoutype");
            if ((((str8 = str5) == null) || (((str8 != "fgs") && (str8 != "gd")) && (str8 != "zd"))) && ((((str9 = _session.get_u_type()) != null) && !(str9 == "zj")) && (!(str9 == "fgs") && !(str9 == "gd"))))
            {
                bool flag1 = str9 == "zd";
            }
            if ((_session.get_u_type().Equals("fgs") || _session.get_u_type().Equals("gd")) || _session.get_u_type().Equals("zd"))
            {
                if (string.IsNullOrEmpty(str5))
                {
                    str5 = _session.get_u_type();
                }
            }
            else if (_session.get_u_type().Equals("zj") && string.IsNullOrEmpty(str5))
            {
                str5 = "fgs";
            }
            if (string.IsNullOrEmpty(str3))
            {
                string str6 = "";
                if (_session.get_u_type().Equals("zj"))
                {
                    str6 = "";
                }
                else
                {
                    str6 = " and " + _session.get_u_type() + "_name='" + _session.get_u_name() + "'";
                }
                str2 = "select u_name from  cz_rate_six  where u_type='" + str5 + "' " + str6;
            }
            else if (!string.IsNullOrEmpty(str3) && string.IsNullOrEmpty(str4))
            {
                string str7 = "";
                if (_session.get_u_type().Equals("zj"))
                {
                    str7 = "";
                }
                else
                {
                    str7 = " and " + _session.get_u_type() + "_name='" + _session.get_u_name() + "'";
                }
                str2 = "select u_name from  cz_rate_six  where u_type='" + str5 + "' " + str7;
            }
            else
            {
                cz_users userInfoByUID = CallBLL.cz_users_bll.GetUserInfoByUID(str3);
                str2 = string.Format("select u_name from cz_rate_six where u_type='" + str5 + "' and {0}_name='{1}'", userInfoByUID.get_u_type(), userInfoByUID.get_u_name());
            }
            DataTable userListByAddUser = CallBLL.cz_users_bll.GetUserListByAddUser(str2, str5);
            if (userListByAddUser == null)
            {
                base.noRightOptMsg(PageBase.GetMessageByCache("u100058", "MessageHint"));
            }
            else
            {
                for (int i = 0; i < userListByAddUser.Rows.Count; i++)
                {
                    if (string.IsNullOrEmpty(str4))
                    {
                        if (i == 0)
                        {
                            Dictionary<string, object> item = new Dictionary<string, object>();
                            item.Add("value", userListByAddUser.Rows[i]["u_id"].ToString());
                            item.Add("label", userListByAddUser.Rows[i]["u_name"].ToString());
                            item.Add("nicker", userListByAddUser.Rows[i]["u_nicker"].ToString());
                            list.Add(item);
                            this.GetRateInfo(userListByAddUser.Rows[i]["u_id"].ToString(), str5);
                            if (flag && flag2)
                            {
                                Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
                                dictionary2.Add("supUsableCredit", this.six_usable_credit_d);
                                dictionary2.Add("userMaxRate", this.six_rate_d);
                                dictionary2.Add("userKind", this.six_kind_d);
                                dictionary2.Add("isCash", this.six_iscash);
                                dictionary.Add("Six", dictionary2);
                                Dictionary<string, object> dictionary3 = new Dictionary<string, object>();
                                dictionary3.Add("supUsableCredit", this.kc_usable_credit_d);
                                dictionary3.Add("userMaxRate", this.kc_rate_d);
                                dictionary3.Add("userKind", this.kc_kind_d);
                                dictionary3.Add("isCash", this.kc_iscash);
                                dictionary.Add("Kc", dictionary3);
                            }
                            else if (flag)
                            {
                                Dictionary<string, object> dictionary4 = new Dictionary<string, object>();
                                dictionary4.Add("supUsableCredit", this.six_usable_credit_d);
                                dictionary4.Add("userMaxRate", this.six_rate_d);
                                dictionary4.Add("userKind", this.six_kind_d);
                                dictionary4.Add("isCash", this.six_iscash);
                                dictionary.Add("Six", dictionary4);
                            }
                            else if (flag2)
                            {
                                Dictionary<string, object> dictionary5 = new Dictionary<string, object>();
                                dictionary5.Add("supUsableCredit", this.kc_usable_credit_d);
                                dictionary5.Add("userMaxRate", this.kc_rate_d);
                                dictionary5.Add("userKind", this.kc_kind_d);
                                dictionary5.Add("isCash", this.kc_iscash);
                                dictionary.Add("Kc", dictionary5);
                            }
                        }
                        else
                        {
                            Dictionary<string, object> dictionary7 = new Dictionary<string, object>();
                            dictionary7.Add("value", userListByAddUser.Rows[i]["u_id"].ToString());
                            dictionary7.Add("label", userListByAddUser.Rows[i]["u_name"].ToString());
                            dictionary7.Add("nicker", userListByAddUser.Rows[i]["u_nicker"].ToString());
                            list.Add(dictionary7);
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(str4) && str4.ToUpper().Equals(userListByAddUser.Rows[i]["u_id"].ToString().ToUpper()))
                        {
                            Dictionary<string, object> dictionary12 = new Dictionary<string, object>();
                            dictionary12.Add("value", userListByAddUser.Rows[i]["u_id"].ToString());
                            dictionary12.Add("label", userListByAddUser.Rows[i]["u_name"].ToString());
                            dictionary12.Add("nicker", userListByAddUser.Rows[i]["u_nicker"].ToString());
                            list.Add(dictionary12);
                            this.GetRateInfo(str4, str5);
                            if (flag && flag2)
                            {
                                Dictionary<string, object> dictionary8 = new Dictionary<string, object>();
                                dictionary8.Add("supUsableCredit", this.six_usable_credit_d);
                                dictionary8.Add("userMaxRate", this.six_rate_d);
                                dictionary8.Add("userKind", this.six_kind_d);
                                dictionary8.Add("isCash", this.six_iscash);
                                dictionary.Add("Six", dictionary8);
                                Dictionary<string, object> dictionary9 = new Dictionary<string, object>();
                                dictionary9.Add("supUsableCredit", this.kc_usable_credit_d);
                                dictionary9.Add("userMaxRate", this.kc_rate_d);
                                dictionary9.Add("userKind", this.kc_kind_d);
                                dictionary9.Add("isCash", this.kc_iscash);
                                dictionary.Add("Kc", dictionary9);
                            }
                            else if (flag)
                            {
                                Dictionary<string, object> dictionary10 = new Dictionary<string, object>();
                                dictionary10.Add("supUsableCredit", this.six_usable_credit_d);
                                dictionary10.Add("userMaxRate", this.six_rate_d);
                                dictionary10.Add("userKind", this.six_kind_d);
                                dictionary10.Add("isCash", this.six_iscash);
                                dictionary.Add("Six", dictionary10);
                            }
                            else if (flag2)
                            {
                                Dictionary<string, object> dictionary11 = new Dictionary<string, object>();
                                dictionary11.Add("supUsableCredit", this.kc_usable_credit_d);
                                dictionary11.Add("userMaxRate", this.kc_rate_d);
                                dictionary11.Add("userKind", this.kc_kind_d);
                                dictionary11.Add("isCash", this.kc_iscash);
                                dictionary.Add("Kc", dictionary11);
                            }
                        }
                        if (!str4.ToUpper().Equals(userListByAddUser.Rows[i]["u_id"].ToString().ToUpper()))
                        {
                            Dictionary<string, object> dictionary13 = new Dictionary<string, object>();
                            dictionary13.Add("value", userListByAddUser.Rows[i]["u_id"].ToString());
                            dictionary13.Add("label", userListByAddUser.Rows[i]["u_name"].ToString());
                            dictionary13.Add("nicker", userListByAddUser.Rows[i]["u_nicker"].ToString());
                            list.Add(dictionary13);
                        }
                    }
                }
                dictionary.Add("nameList", list);
                result.set_success(200);
                result.set_data(dictionary);
                strResult = base.ObjectToJson(result);
            }
        }

        private DataTable MemberNameAndCount(int nType, string uName)
        {
            switch (nType)
            {
                case 1:
                    return CallBLL.cz_rate_kc_bll.GetUserCount();

                case 2:
                    return CallBLL.cz_rate_kc_bll.GetUserCount(uName, "fgs");

                case 3:
                    return CallBLL.cz_rate_kc_bll.GetUserCount(uName, "gd");

                case 4:
                    return CallBLL.cz_rate_kc_bll.GetUserCount(uName, "zd");

                case 5:
                    return CallBLL.cz_rate_kc_bll.GetUserCount(uName, "dl");

                case 7:
                    return CallBLL.cz_users_child_bll.GetChildCount(uName);

                case 8:
                    return CallBLL.cz_saleset_six_bll.GetSaleSetUserCount();
            }
            return null;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            base.Response.Expires = 0;
            base.Response.CacheControl = "no-cache";
            string str = LSRequest.qq("action").Trim();
            string strResult = "";
            switch (str)
            {
                case "getAd":
                    this.getAd(ref strResult);
                    break;

                case "getAboveMemeber":
                    this.getAboveMemeber(ref strResult);
                    break;

                case "getMemberInfo":
                    this.getMemberInfo(ref strResult);
                    break;

                case "getMemberDetail":
                    this.getMemberDetail(ref strResult);
                    break;

                case "getAddMemberInitData":
                    this.getAddMemberInitData(ref strResult);
                    break;

                case "getDrawBack":
                    this.getDrawBack(ref strResult);
                    break;

                case "getProfit":
                    this.getProfit(ref strResult);
                    break;

                case "delFilluser":
                    this.delFilluser(ref strResult);
                    break;

                case "getPersonLog":
                    this.getPersonLog(ref strResult);
                    break;

                case "getExistName":
                    this.getExistName(ref strResult);
                    break;

                case "getInitZSHY":
                    this.getInitZSHY(ref strResult);
                    break;

                case "getCreditInfo":
                    this.getCreditInfo(ref strResult);
                    break;

                case "getUserCZ":
                    this.UserCreditRecharge(ref strResult);
                    break;

                case "addUserCZ":
                    this.UserCreditRecharge(ref strResult);
                    break;

                case "getUserTX":
                    this.UserCreditWithdraw(ref strResult);
                    break;

                case "addUserTX":
                    this.UserCreditWithdraw(ref strResult);
                    break;

                case "getUserFlowInit":
                    this.UserCreditFlowInit(ref strResult);
                    break;

                case "getUserFlow":
                    this.UserCreditFlow(ref strResult);
                    break;
            }
            base.OutJson(strResult);
        }

        private void UserCreditFlow(ref string strResult)
        {
            string str = LSRequest.qq("action").Trim();
            string str2 = LSRequest.qq("uid").Trim();
            string str3 = LSRequest.qq("masterid");
            string str4 = LSRequest.qq("page");
            if (str.Equals("getUserFlow"))
            {
                base.Server.Transfer(this.tPath + string.Format("/credit_flow_list.aspx?action=getUserFlow&uid={0}&masterid={1}&page={2}", str2, str3, str4));
            }
            else
            {
                base.Response.End();
            }
        }

        private void UserCreditFlowInit(ref string strResult)
        {
            string str = LSRequest.qq("action").Trim();
            string str2 = LSRequest.qq("uid").Trim();
            if (str.Equals("getUserFlowInit"))
            {
                base.Server.Transfer(this.tPath + string.Format("/credit_flow_list.aspx?action=getUserFlowInit&uid={0}", str2));
            }
            else
            {
                base.Response.End();
            }
        }

        private void UserCreditRecharge(ref string strResult)
        {
            string str = LSRequest.qq("action").Trim();
            string str2 = LSRequest.qq("uid").Trim();
            string str3 = LSRequest.qq("masterid");
            string str4 = LSRequest.qq("czje");
            if (str.Equals("addUserCZ"))
            {
                base.Server.Transfer(this.tPath + string.Format("/credit_recharge.aspx?action=addUserCZ&uid={0}&masterid={1}&czje={2}", str2, str3, str4));
            }
            else if (str.Equals("getUserCZ"))
            {
                base.Server.Transfer(this.tPath + string.Format("/credit_recharge.aspx?action=getUserCZ&uid={0}", str2));
            }
            else
            {
                base.Response.End();
            }
        }

        private void UserCreditWithdraw(ref string strResult)
        {
            string str = LSRequest.qq("action").Trim();
            string str2 = LSRequest.qq("uid").Trim();
            string str3 = LSRequest.qq("masterid");
            string str4 = LSRequest.qq("txje");
            if (str.Equals("addUserTX"))
            {
                base.Server.Transfer(this.tPath + string.Format("/credit_withdraw.aspx?action=addUserTX&uid={0}&masterid={1}&txje={2}", str2, str3, str4));
            }
            else if (str.Equals("getUserTX"))
            {
                base.Server.Transfer(this.tPath + string.Format("/credit_withdraw.aspx?action=getUserTX&uid={0}", str2));
            }
            else
            {
                base.Response.End();
            }
        }
    }
}

