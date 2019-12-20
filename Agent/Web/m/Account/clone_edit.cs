namespace Agent.Web.m.Account
{
    using Agent.Web.WebBase;
    using LotterySystem.Common;
    using LotterySystem.Model;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Web.UI.HtmlControls;

    public class clone_edit : MemberPageBase_Mobile
    {
        private DataRow[] dlDataRow1;
        private DataRow[] dlDataRow2;
        private DataRow[] dlDataRow3;
        protected HtmlForm form1;
        protected string u_id = "";
        private cz_users_child userChildModel;
        private DataRow[] zjDataRow1;
        private DataRow[] zjDataRow2;
        private DataRow[] zjDataRow3;
        private DataRow[] zjDataRow4;

        private void AddUser()
        {
            LSRequest.qq("cloneName");
            string str = LSRequest.qq("password");
            string str2 = LSRequest.qq("nicker");
            string str3 = LSRequest.qq("qx");
            string str4 = LSRequest.qq("isLocked");
            string message = "";
            if (!base.ValidParamByUserEditPhone("child", ref message, null, null, null))
            {
                base.noRightOptMsg(message);
            }
            if (!string.IsNullOrEmpty(str.Trim()) && !Regexlib.IsValidPassword(str.Trim(), base.get_GetPasswordLU()))
            {
                if (base.get_GetPasswordLU().Equals("1"))
                {
                    base.noRightOptMsg("密碼要8-20位,且必需包含大寫字母、小寫字母和数字！");
                }
                else
                {
                    base.noRightOptMsg("密碼要8-20位,且必需包含字母、和数字！");
                }
            }
            cz_users_child _child = new cz_users_child();
            _child.set_u_id(this.u_id.ToString().ToUpper());
            _child.set_u_nicker(str2.Trim());
            if (!string.IsNullOrEmpty(str4) && (str4 == "1"))
            {
                _child.set_retry_times(0);
            }
            if (!string.IsNullOrEmpty(str))
            {
                string ramSalt = Utils.GetRamSalt(6);
                _child.set_u_psw(DESEncrypt.EncryptString(str, ramSalt));
                _child.set_salt(ramSalt);
            }
            _child.set_permissions_name(str3.Trim());
            cz_users_child userByUID = CallBLL.cz_users_child_bll.GetUserByUID(_child.get_u_id());
            if (CallBLL.cz_users_child_bll.UpdateUser(_child))
            {
                base.user_edit_children_log(userByUID, _child);
                base.successOptMsg("修改子帳號成功！");
            }
            else
            {
                base.noRightOptMsg("修改子帳號失敗！");
            }
        }

        private void getMemberDetail(ref string strResult)
        {
            base.checkLoginByHandler(0);
            this.u_id = LSRequest.qq("uid");
            string str = LSRequest.qq("memberId");
            string str2 = LSRequest.qq("submitType");
            if (str != "clone")
            {
                base.Response.End();
            }
            ReturnResult result = new ReturnResult();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            List<object> list = new List<object>();
            List<object> list2 = new List<object>();
            string str3 = this.Session["user_name"].ToString();
            agent_userinfo_session _session = this.Session[str3 + "lottery_session_user_info"] as agent_userinfo_session;
            if (((!_session.get_u_type().Trim().Equals("zj") && !_session.get_u_type().Trim().Equals("fgs")) && (!_session.get_u_type().Trim().Equals("gd") && !_session.get_u_type().Trim().Equals("zd"))) && !_session.get_u_type().Trim().Equals("dl"))
            {
                base.Response.End();
            }
            if (this.Session["child_user_name"] != null)
            {
                base.Response.End();
            }
            this.userChildModel = CallBLL.cz_users_child_bll.GetUserByUID(this.u_id);
            if (this.userChildModel == null)
            {
                base.Response.End();
            }
            if (!this.userChildModel.get_is_admin().Equals(0))
            {
                base.Response.End();
            }
            if (!this.userChildModel.get_parent_u_name().Equals(str3))
            {
                base.Response.End();
            }
            bool flag = base.is_locked_user(this.userChildModel.get_retry_times().ToString());
            DataTable table = CallBLL.cz_permissions_bll.GetListByUType((_session.get_u_type().Equals("zj") != null) ? "zj" : "dl").Tables[0];
            if (_session.get_u_type().Equals("zj"))
            {
                this.zjDataRow1 = table.Select(string.Format(" group_id={0} ", 1));
                this.zjDataRow2 = table.Select(string.Format(" group_id={0} ", 2));
                this.zjDataRow3 = table.Select(string.Format(" group_id={0} ", 3));
                this.zjDataRow4 = table.Select(string.Format(" group_id={0} ", 4));
            }
            else
            {
                if (_session.get_u_type().Equals("fgs"))
                {
                    if (_session.get_six_op_odds().Equals(1) || _session.get_kc_op_odds().Equals(1))
                    {
                        this.dlDataRow1 = table.Select(string.Format(" group_id={0} ", 5));
                    }
                    else
                    {
                        this.dlDataRow1 = table.Select(string.Format(" group_id={0} and name<>'{1}' ", 5, "po_5_3"));
                    }
                }
                else
                {
                    this.dlDataRow1 = table.Select(string.Format(" group_id={0} and name<>'{1}' ", 5, "po_5_3"));
                }
                this.dlDataRow2 = table.Select(string.Format(" group_id={0} ", 6));
                this.dlDataRow3 = table.Select(string.Format(" group_id={0} ", 7));
            }
            switch (str2)
            {
                case "view":
                {
                    if (_session.get_u_type().Equals("zj"))
                    {
                        foreach (DataRow row in this.zjDataRow1)
                        {
                            string str4 = row["name"].ToString();
                            int num = (this.userChildModel.get_permissions_name().IndexOf(str4) > -1) ? 1 : 0;
                            if (num == 1)
                            {
                                list2.Add(str4);
                            }
                            string str5 = row["name_remark"].ToString();
                            Dictionary<string, object> item = new Dictionary<string, object>();
                            item.Add("value", str4);
                            item.Add("label", str5);
                            list.Add(item);
                        }
                        foreach (DataRow row2 in this.zjDataRow2)
                        {
                            string str6 = row2["name"].ToString();
                            int num2 = (this.userChildModel.get_permissions_name().IndexOf(str6) > -1) ? 1 : 0;
                            if (num2 == 1)
                            {
                                list2.Add(str6);
                            }
                            string str7 = row2["name_remark"].ToString();
                            Dictionary<string, object> dictionary3 = new Dictionary<string, object>();
                            dictionary3.Add("value", str6);
                            dictionary3.Add("label", str7);
                            list.Add(dictionary3);
                        }
                        foreach (DataRow row3 in this.zjDataRow3)
                        {
                            string str8 = row3["name"].ToString();
                            int num3 = (this.userChildModel.get_permissions_name().IndexOf(str8) > -1) ? 1 : 0;
                            if (num3 == 1)
                            {
                                list2.Add(str8);
                            }
                            string str9 = row3["name_remark"].ToString();
                            Dictionary<string, object> dictionary4 = new Dictionary<string, object>();
                            dictionary4.Add("value", str8.ToString());
                            dictionary4.Add("label", str9);
                            list.Add(dictionary4);
                        }
                        foreach (DataRow row4 in this.zjDataRow4)
                        {
                            string str10 = row4["name"].ToString();
                            int num4 = (this.userChildModel.get_permissions_name().IndexOf(str10) > -1) ? 1 : 0;
                            if (num4 == 1)
                            {
                                list2.Add(str10);
                            }
                            string str11 = row4["name_remark"].ToString();
                            Dictionary<string, object> dictionary5 = new Dictionary<string, object>();
                            dictionary5.Add("value", str10);
                            dictionary5.Add("label", str11);
                            list.Add(dictionary5);
                        }
                    }
                    else
                    {
                        foreach (DataRow row5 in this.dlDataRow1)
                        {
                            string str12 = row5["name"].ToString();
                            int num5 = (this.userChildModel.get_permissions_name().IndexOf(str12) > -1) ? 1 : 0;
                            if (num5 == 1)
                            {
                                list2.Add(str12);
                            }
                            string str13 = row5["name_remark"].ToString();
                            Dictionary<string, object> dictionary6 = new Dictionary<string, object>();
                            dictionary6.Add("value", str12);
                            dictionary6.Add("label", str13);
                            list.Add(dictionary6);
                        }
                        foreach (DataRow row6 in this.dlDataRow2)
                        {
                            string str14 = row6["name"].ToString();
                            int num6 = (this.userChildModel.get_permissions_name().IndexOf(str14) > -1) ? 1 : 0;
                            if (num6 == 1)
                            {
                                list2.Add(str14);
                            }
                            string str15 = row6["name_remark"].ToString();
                            Dictionary<string, object> dictionary7 = new Dictionary<string, object>();
                            dictionary7.Add("value", str14);
                            dictionary7.Add("label", str15);
                            list.Add(dictionary7);
                        }
                        foreach (DataRow row7 in this.dlDataRow3)
                        {
                            string str16 = row7["name"].ToString();
                            int num7 = (this.userChildModel.get_permissions_name().IndexOf(str16) > -1) ? 1 : 0;
                            if (num7 == 1)
                            {
                                list2.Add(str16);
                            }
                            string str17 = row7["name_remark"].ToString();
                            Dictionary<string, object> dictionary8 = new Dictionary<string, object>();
                            dictionary8.Add("value", str16);
                            dictionary8.Add("label", str17);
                            list.Add(dictionary8);
                        }
                    }
                    Dictionary<string, object> dictionary9 = new Dictionary<string, object>();
                    dictionary9.Add("cloneName", this.userChildModel.get_u_name());
                    dictionary9.Add("isLocked", flag ? "1" : "0");
                    dictionary9.Add("userState", this.userChildModel.get_status().ToString());
                    dictionary9.Add("nicker", this.userChildModel.get_u_nicker());
                    dictionary9.Add("qx", list2);
                    dictionary9.Add("qxOptions", list);
                    dictionary9.Add("date", this.userChildModel.get_add_date());
                    dictionary = dictionary9;
                    result.set_success(200);
                    result.set_data(dictionary);
                    strResult = base.ObjectToJson(result);
                    return;
                }
                case "edit":
                    this.UpdateStatusChild();
                    this.AddUser();
                    break;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string str3;
            base.Response.Expires = 0;
            base.Response.CacheControl = "no-cache";
            string str = LSRequest.qq("action").Trim();
            string strResult = "";
            if (((str3 = str) != null) && (str3 == "getMemberDetail"))
            {
                this.getMemberDetail(ref strResult);
            }
            base.OutJson(strResult);
        }

        private void UpdateStatusChild()
        {
            string uid = LSRequest.qq("uid").Trim();
            string str2 = LSRequest.qq("userState").Trim();
            cz_users_child userByUID = CallBLL.cz_users_child_bll.GetUserByUID(uid);
            string str3 = "";
            if (userByUID != null)
            {
                str3 = userByUID.get_status().ToString();
            }
            string str4 = this.Session["user_name"].ToString();
            if ((userByUID.get_parent_u_name().Equals(str4) && (str3 != str2)) && CallBLL.cz_users_child_bll.UpdateStatus(uid, str2))
            {
                base.user_change_status_log(uid, str3, true);
            }
        }
    }
}

