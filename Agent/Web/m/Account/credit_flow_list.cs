namespace Agent.Web.m.Account
{
    using Agent.Web.WebBase;
    using LotterySystem.Common;
    using LotterySystem.Model;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class credit_flow_list : MemberPageBase_Mobile
    {
        protected int dataCount;
        protected DataTable lotteryDT;
        protected string page = "1";
        protected int pageCount;
        protected int pageSize = 20;
        private agent_userinfo_session userModel;

        private void getFlowList(ref string strResult)
        {
            bool flag;
            bool flag2;
            List<object> list;
            base.checkLoginByHandler(0);
            ReturnResult result = new ReturnResult();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            new List<object>();
            string str = this.Session["user_name"].ToString();
            this.userModel = this.Session[str + "lottery_session_user_info"] as agent_userinfo_session;
            if ((!this.userModel.get_u_type().Trim().Equals("zj") && !this.userModel.get_u_type().Trim().Equals("fgs")) && (!this.userModel.get_u_type().Trim().Equals("gd") && !this.userModel.get_u_type().Trim().Equals("zd")))
            {
                base.Response.End();
            }
            base.checkCloneRight();
            this.lotteryDT = base.GetLotteryList();
            base.Permission_Aspx_ZJ_Ajax(this.userModel, "po_2_1");
            base.Permission_Aspx_DL_Ajax(this.userModel, "po_6_1");
            string str2 = LSRequest.qq("masterid");
            string str3 = LSRequest.qq("uid");
            this.page = LSRequest.qq("page");
            if (string.IsNullOrEmpty(this.page))
            {
                this.page = "1";
            }
            else if (int.Parse(this.page) < 1)
            {
                this.page = "1";
            }
            cz_users userInfoByUID = null;
            userInfoByUID = CallBLL.cz_users_bll.GetUserInfoByUID(str3);
            if (!base.IsUpperLowerLevels(userInfoByUID.get_u_name(), this.userModel.get_u_type(), this.userModel.get_u_name()))
            {
                base.Response.End();
            }
            CallBLL.cz_users_bll.GetUserInfoByUName(userInfoByUID.get_sup_name());
            base.OpenLotteryMaster(out flag, out flag2);
            DataTable table = null;
            if (flag && userInfoByUID.get_six_iscash().Equals(int.Parse("1")))
            {
                int num = 1;
                if (str2.Equals(num.ToString()))
                {
                    table = CallBLL.cz_credit_flow_bll.GetCreditListByUName_Phone(str3, 1, int.Parse(this.page) - 1, this.pageSize, ref this.pageCount, ref this.dataCount, "");
                    goto Label_02B8;
                }
            }
            if (flag2 && userInfoByUID.get_kc_iscash().Equals(int.Parse("1")))
            {
                int num2 = 2;
                if (str2.Equals(num2.ToString()))
                {
                    table = CallBLL.cz_credit_flow_bll.GetCreditListByUName_Phone(str3, 2, int.Parse(this.page) - 1, this.pageSize, ref this.pageCount, ref this.dataCount, "");
                    goto Label_02B8;
                }
            }
            base.noRightOptMsg("無權操作！");
            base.Response.End();
        Label_02B8:
            list = new List<object>();
            if ((table != null) && (table.Rows.Count > 0))
            {
                foreach (DataRow row in table.Rows)
                {
                    Dictionary<string, object> item = new Dictionary<string, object>();
                    item.Add("lottery", base.GetMasterLotteryByMasterid(int.Parse(row["master_id"].ToString())));
                    item.Add("name", row["u_name"].ToString());
                    item.Add("flagtxt", base.GetCreditFlowText(row["flag"].ToString()));
                    int? nullable3 = null;
                    item.Add("jy_old", Utils.GetMathRound(double.Parse(row["credit_old"].ToString()), nullable3));
                    int? nullable4 = null;
                    item.Add("jy_change", Utils.GetMathRound(double.Parse(row["credit_change"].ToString()), nullable4));
                    int? nullable5 = null;
                    item.Add("jy_new", Utils.GetMathRound(double.Parse(row["credit_new"].ToString()), nullable5));
                    item.Add("remark", row["note"].ToString());
                    if (this.userModel.get_u_name().Equals(row["operator_name"].ToString()) && (this.userModel.get_users_child_session() != null))
                    {
                        item.Add("opt_name", " - " + row["operator_child_name"].ToString());
                    }
                    else if (this.userModel.get_u_name().Equals(row["operator_name"].ToString()) && (this.userModel.get_users_child_session() == null))
                    {
                        string str4 = row["operator_name"].ToString();
                        if (!string.IsNullOrEmpty(row["operator_child_name"].ToString()))
                        {
                            str4 = str4 + " - " + row["operator_child_name"].ToString();
                        }
                        item.Add("opt_name", str4);
                    }
                    else if (!base.IsUpperLowerLevels(row["operator_name"].ToString(), this.userModel.get_u_type(), this.userModel.get_u_name()))
                    {
                        item.Add("opt_name", "-");
                    }
                    else
                    {
                        string str5 = row["operator_name"].ToString();
                        if (!string.IsNullOrEmpty(row["operator_child_name"].ToString()))
                        {
                            str5 = str5 + " - " + row["operator_child_name"].ToString();
                        }
                        item.Add("opt_name", str5);
                    }
                    item.Add("opt_time", DateTime.Parse(row["operator_time"].ToString()).ToString("yyyy-MM-dd HH:mm:ss"));
                    list.Add(item);
                }
            }
            dictionary.Add("flowList", list);
            if (dictionary.Count == 0)
            {
                dictionary.Add("page", 0);
            }
            else if ((table != null) && (table.Rows.Count > 0))
            {
                dictionary.Add("page", int.Parse(this.page) + 1);
            }
            else
            {
                dictionary.Add("page", this.page);
            }
            result.set_success(200);
            result.set_data(dictionary);
            strResult = base.ObjectToJson(result);
        }

        private void getFlowListInit(ref string strResult)
        {
            bool flag;
            bool flag2;
            base.checkLoginByHandler(0);
            ReturnResult result = new ReturnResult();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            new List<object>();
            string str = this.Session["user_name"].ToString();
            this.userModel = this.Session[str + "lottery_session_user_info"] as agent_userinfo_session;
            if ((!this.userModel.get_u_type().Trim().Equals("zj") && !this.userModel.get_u_type().Trim().Equals("fgs")) && (!this.userModel.get_u_type().Trim().Equals("gd") && !this.userModel.get_u_type().Trim().Equals("zd")))
            {
                base.Response.End();
            }
            base.checkCloneRight();
            this.lotteryDT = base.GetLotteryList();
            base.Permission_Aspx_ZJ_Ajax(this.userModel, "po_2_1");
            base.Permission_Aspx_DL_Ajax(this.userModel, "po_6_1");
            string str2 = LSRequest.qq("uid");
            cz_users userInfoByUID = null;
            userInfoByUID = CallBLL.cz_users_bll.GetUserInfoByUID(str2);
            if (!base.IsUpperLowerLevels(userInfoByUID.get_u_name(), this.userModel.get_u_type(), this.userModel.get_u_name()))
            {
                base.Response.End();
            }
            base.OpenLotteryMaster(out flag, out flag2);
            if (flag && userInfoByUID.get_six_iscash().Equals(int.Parse("1")))
            {
                dictionary.Add("isCashSix", 1);
            }
            else if (flag2 && userInfoByUID.get_kc_iscash().Equals(int.Parse("1")))
            {
                dictionary.Add("isCashKc", 1);
            }
            else
            {
                base.noRightOptMsg("無權操作！");
                base.Response.End();
            }
            result.set_success(200);
            result.set_data(dictionary);
            strResult = base.ObjectToJson(result);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            base.Response.Expires = 0;
            base.Response.CacheControl = "no-cache";
            string str = LSRequest.qq("action").Trim();
            string strResult = "";
            string str3 = str;
            if (str3 != null)
            {
                if (!(str3 == "getUserFlowInit"))
                {
                    if (str3 == "getUserFlow")
                    {
                        this.getFlowList(ref strResult);
                    }
                }
                else
                {
                    this.getFlowListInit(ref strResult);
                }
            }
            base.OutJson(strResult);
        }
    }
}

