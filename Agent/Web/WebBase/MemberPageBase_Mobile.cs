namespace Agent.Web.WebBase
{
    using LotterySystem.Common;
    using LotterySystem.Model;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Web;

    public class MemberPageBase_Mobile : MemberPageBase
    {
        public static Dictionary<string, string> roleCfg;

        static MemberPageBase_Mobile()
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary.Add("all", "所有人");
            dictionary.Add("zj", "总监");
            dictionary.Add("fgs", "分公司");
            dictionary.Add("gd", "股東");
            dictionary.Add("zd", "總代理");
            dictionary.Add("dl", "代理");
            dictionary.Add("hy", "會員");
            dictionary.Add("clone", "子賬號");
            dictionary.Add("sales", "出貨會員");
            roleCfg = dictionary;
        }

        public MemberPageBase_Mobile() : base("mobile")
        {
            base.Load += new EventHandler(this.MemberPageBase_Mobile_Load);
        }

        protected void checkCloneRight()
        {
            string str;
            base.checkLoginByHandler(0);
            ReturnResult result = new ReturnResult();
            string str2 = this.Session["user_name"].ToString();
            agent_userinfo_session model = this.Session[str2 + "lottery_session_user_info"] as agent_userinfo_session;
            if (model.get_u_type().Trim().Equals("zj"))
            {
                if (this.Permission_Aspx_ZJ_Ajax(model, "po_2_1"))
                {
                    result.set_success(400);
                    result.set_tipinfo("子帳號無權操作!");
                    str = this.ObjectToJson(result);
                    this.OutJson(str);
                }
            }
            else if (model.get_u_type().Trim().Equals("fgs"))
            {
                if (this.Permission_Aspx_DL_Ajax(model, "po_6_1"))
                {
                    result.set_success(400);
                    result.set_tipinfo("子帳號無權操作!");
                    str = this.ObjectToJson(result);
                    this.OutJson(str);
                }
            }
            else if (model.get_u_type().Trim().Equals("gd"))
            {
                if (this.Permission_Aspx_DL_Ajax(model, "po_6_1"))
                {
                    result.set_success(400);
                    result.set_tipinfo("子帳號無權操作!");
                    str = this.ObjectToJson(result);
                    this.OutJson(str);
                }
            }
            else if (model.get_u_type().Trim().Equals("zd"))
            {
                if (this.Permission_Aspx_DL_Ajax(model, "po_6_1"))
                {
                    result.set_success(400);
                    result.set_tipinfo("子帳號無權操作!");
                    str = this.ObjectToJson(result);
                    this.OutJson(str);
                }
            }
            else if (model.get_u_type().Trim().Equals("dl"))
            {
                if (this.Permission_Aspx_DL_Ajax(model, "po_6_1"))
                {
                    result.set_success(400);
                    result.set_tipinfo("子帳號無權操作!");
                    str = this.ObjectToJson(result);
                    this.OutJson(str);
                }
            }
            else
            {
                result.set_success(400);
                result.set_tipinfo("無權操作!");
                str = this.ObjectToJson(result);
                this.OutJson(str);
            }
        }

        protected string CodeValidateByMobile()
        {
            string str = string.Empty;
            Random random = new Random();
            for (int i = 0; i < 4; i++)
            {
                int num = random.Next();
                str = str + ((char) (0x30 + ((ushort) (num % 10)))).ToString();
            }
            return (DateTime.Now.ToString("MMddhhmmss") + str);
        }

        protected void GetCount(string u_name, string utype, ref string zdcount, ref string dlcount, ref string hycount)
        {
            foreach (DataRow row in CallBLL.cz_rate_kc_bll.GetUserCount(u_name, utype).Rows)
            {
                string str = row[0].ToString();
                if (str != null)
                {
                    if (!(str == "zd"))
                    {
                        if (str == "dl")
                        {
                            goto Label_0070;
                        }
                        if (str == "hy")
                        {
                            goto Label_0081;
                        }
                    }
                    else
                    {
                        zdcount = row[1].ToString();
                    }
                }
                continue;
            Label_0070:
                dlcount = row[1].ToString();
                continue;
            Label_0081:
                hycount = row[1].ToString();
            }
        }

        public bool IsOpenBigLottery(int master_id)
        {
            DataRow[] source = base.GetLotteryList().Select(string.Format(" master_id={0} ", master_id));
            return ((source != null) && (source.Count<DataRow>() > 0));
        }

        public bool IsOpenLottery(int id)
        {
            DataRow[] source = base.GetLotteryList().Select(string.Format(" id={0} ", id));
            return ((source != null) && (source.Count<DataRow>() > 0));
        }

        public string LotteryTypeSave()
        {
            if (HttpContext.Current.Session["LotteryTypeByPhone"] == null)
            {
                DataTable lotteryList = base.GetLotteryList();
                string str = lotteryList.Rows[0]["id"].ToString();
                HttpContext.Current.Session["LotteryTypeByPhone"] = lotteryList.Rows[0]["id"].ToString();
                return str;
            }
            return HttpContext.Current.Session["LotteryTypeByPhone"].ToString();
        }

        public void MemberPageBase_Mobile_Load(object sender, EventArgs e)
        {
        }

        protected void noRightOptMsg()
        {
            ReturnResult result = new ReturnResult();
            result.set_success(400);
            result.set_tipinfo("無權操作!");
            string strResult = this.ObjectToJson(result);
            this.OutJson(strResult);
        }

        protected void noRightOptMsg(string msg)
        {
            ReturnResult result = new ReturnResult();
            result.set_success(400);
            result.set_tipinfo(msg);
            string strResult = this.ObjectToJson(result);
            this.OutJson(strResult);
        }

        protected string ObjectToJson(object obj)
        {
            return JsonHandle.ObjectToJson(obj);
        }

        protected void OpenLotteryMaster(out bool bSix, out bool bKC)
        {
            DataTable lotteryList = base.GetLotteryList();
            string[] source = base.GetLotteryMasterID(lotteryList).Split(new char[] { ',' });
            int num = 1;
            if (source.Contains<string>(num.ToString()))
            {
                bSix = true;
            }
            else
            {
                bSix = false;
            }
            int num2 = 2;
            if (source.Contains<string>(num2.ToString()))
            {
                bKC = true;
            }
            else
            {
                bKC = false;
            }
        }

        protected void OutJson(string strResult)
        {
            base.Response.ContentType = "application/json";
            base.Response.Write(strResult);
            base.Response.End();
        }

        public bool Permission_Aspx_DL_Ajax(agent_userinfo_session model, string perName)
        {
            return ((!model.get_u_type().ToLower().Equals("zj") && (model.get_users_child_session() != null)) && (model.get_users_child_session().get_permissions_name().IndexOf(perName) < 0));
        }

        public bool Permission_Aspx_ZJ_Ajax(agent_userinfo_session model, string perName)
        {
            return ((model.get_u_type().ToLower().Equals("zj") && (model.get_users_child_session() != null)) && (model.get_users_child_session().get_permissions_name().IndexOf(perName) < 0));
        }

        public void Permission_Aspx_ZJ_Mobile(string perName)
        {
            base.checkLoginByHandler(0);
            ReturnResult result = new ReturnResult();
            string str2 = this.Session["user_name"].ToString();
            agent_userinfo_session _session = this.Session[str2 + "lottery_session_user_info"] as agent_userinfo_session;
            if ((_session.get_u_type().ToLower().Equals("zj") && (_session.get_users_child_session() != null)) && (_session.get_users_child_session().get_permissions_name().IndexOf(perName) < 0))
            {
                result.set_success(400);
                result.set_tipinfo("無權操作!");
                string strResult = this.ObjectToJson(result);
                this.OutJson(strResult);
            }
        }

        protected void successOptMsg(string msg)
        {
            ReturnResult result = new ReturnResult();
            result.set_success(200);
            result.set_tipinfo(msg);
            string strResult = this.ObjectToJson(result);
            this.OutJson(strResult);
        }

        protected void successOptMsg(string msg, object data)
        {
            ReturnResult result = new ReturnResult();
            result.set_success(200);
            result.set_tipinfo(msg);
            result.set_data(data);
            string strResult = this.ObjectToJson(result);
            this.OutJson(strResult);
        }

        protected void UsePageCache()
        {
            base.Response.Cache.SetMaxAge(new TimeSpan(0, 10, 0));
            string str = base.Request.Headers["If-Modified-Since"];
            DateTime time = Convert.ToDateTime(str);
            TimeSpan span = (TimeSpan) (DateTime.Now - time);
            if (span.TotalSeconds < 7200.0)
            {
                base.Response.StatusCode = 0x130;
                base.Response.End();
            }
            else
            {
                DateTime now = DateTime.Now;
                base.Response.Cache.SetLastModified(now);
            }
        }
    }
}

