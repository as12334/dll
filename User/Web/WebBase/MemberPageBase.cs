namespace User.Web.WebBase
{
    using LotterySystem.Common;
    using LotterySystem.Common.ServiceStackRedis;
    using LotterySystem.Model;
    using LotterySystem.WebPageBase;
    using ServiceStack.Redis;
    using System;
    using System.Collections;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.IO;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Threading;
    using System.Web;

    public class MemberPageBase : PageBase
    {
        public const int UserPutBetSeconds_kc = 0;
        public const int UserPutBetSeconds_six = 2;

        public MemberPageBase()
        {
            base.Load += new EventHandler(this.MemberPageBase_Load);
        }

        public MemberPageBase(string mobile)
        {
        }

        public void Add_Bet_Lock(string lottery_type, string fgs_name, string odds_id)
        {
            bool flag;
        Label_00D7:
            flag = true;
            if (HttpContext.Current.Application["Bet_" + lottery_type + "_" + fgs_name + "_" + odds_id] != null)
            {
                int num = (int) Math.Floor(Utils.DateDiff(Convert.ToDateTime(HttpContext.Current.Application["Bet_" + lottery_type + "_" + fgs_name + "_" + odds_id].ToString()), DateTime.Now).TotalMilliseconds);
                if (num <= 0x1f40)
                {
                    Thread.Sleep(100);
                    goto Label_00D7;
                }
            }
            HttpContext.Current.Application.Lock();
            HttpContext.Current.Application["Bet_" + lottery_type + "_" + fgs_name + "_" + odds_id] = DateTime.Now.ToString();
            HttpContext.Current.Application.UnLock();
        }

        public void Add_TenPoker_Lock(string lottery_type, string phase_id, string numTable)
        {
            bool flag;
        Label_00D7:
            flag = true;
            if (HttpContext.Current.Application["Poker_" + lottery_type + "_" + phase_id + "_" + numTable] != null)
            {
                int num = (int) Math.Floor(Utils.DateDiff(Convert.ToDateTime(HttpContext.Current.Application["Poker_" + lottery_type + "_" + phase_id + "_" + numTable].ToString()), DateTime.Now).TotalMilliseconds);
                if (num <= 0x7d0)
                {
                    Thread.Sleep(100);
                    goto Label_00D7;
                }
            }
            HttpContext.Current.Application.Lock();
            HttpContext.Current.Application["Poker_" + lottery_type + "_" + phase_id + "_" + numTable] = DateTime.Now.ToString();
            HttpContext.Current.Application.UnLock();
        }

        protected static void add_user_change_log(string changed_user, int lottery_id, string edit_master_name, string edit_child_name, string note, string old_val, string new_val)
        {
            string iP = LSRequest.GetIP();
            DateTime now = DateTime.Now;
            string str2 = string.Format("insert into cz_user_change_log ([u_name],[lottery_id],[old_val],[new_val],[note],[edit_time],[edit_user],[edit_children_user],[ip]) values (@changed_user,@lottery_id,@old_val,@new_val,@note,@edit_time,@edit_master_name,@edit_child_name,@ip)", new object[0]);
            SqlParameter[] parameterArray2 = new SqlParameter[9];
            SqlParameter parameter = new SqlParameter("@changed_user", SqlDbType.NVarChar) {
                Value = changed_user
            };
            parameterArray2[0] = parameter;
            SqlParameter parameter2 = new SqlParameter("@lottery_id", SqlDbType.NVarChar) {
                Value = lottery_id
            };
            parameterArray2[1] = parameter2;
            SqlParameter parameter3 = new SqlParameter("@old_val", SqlDbType.NVarChar) {
                Value = old_val
            };
            parameterArray2[2] = parameter3;
            SqlParameter parameter4 = new SqlParameter("@new_val", SqlDbType.NVarChar) {
                Value = new_val
            };
            parameterArray2[3] = parameter4;
            SqlParameter parameter5 = new SqlParameter("@note", SqlDbType.NVarChar) {
                Value = note
            };
            parameterArray2[4] = parameter5;
            SqlParameter parameter6 = new SqlParameter("@edit_time", SqlDbType.NVarChar) {
                Value = now
            };
            parameterArray2[5] = parameter6;
            SqlParameter parameter7 = new SqlParameter("@edit_master_name", SqlDbType.NVarChar) {
                Value = edit_master_name
            };
            parameterArray2[6] = parameter7;
            SqlParameter parameter8 = new SqlParameter("@edit_child_name", SqlDbType.NVarChar) {
                Value = edit_child_name
            };
            parameterArray2[7] = parameter8;
            SqlParameter parameter9 = new SqlParameter("@ip", SqlDbType.NVarChar) {
                Value = iP
            };
            parameterArray2[8] = parameter9;
            SqlParameter[] parameterArray = parameterArray2;
            CallBLL.cz_user_change_log_bll.executte_sql(str2, parameterArray);
        }

        public bool BetCreditLock(string u_name)
        {
            return this.IsCreditLock(u_name);
        }

        public string BetReceiveEnd(string lottery_type)
        {
            string[] strArray = PageBase.GetWebconfigApp("BetReceiveEnd").ToString().Split(new char[] { '|' });
            for (int i = 0; i < strArray.Length; i++)
            {
                string str3 = strArray[i].Split(new char[] { ',' })[0];
                string str4 = strArray[i].Split(new char[] { ',' })[1];
                if (lottery_type.Equals(str3))
                {
                    return str4;
                }
            }
            return "";
        }

        protected void checkLogin()
        {
            ReturnResult result;
            string str2;
            string str = LSRequest.qq("browserCode");
            bool flag = false;
            if ((HttpContext.Current.Session["lottery_session_img_code_brower"] != null) && !string.IsNullOrEmpty(str))
            {
                if (HttpContext.Current.Session["lottery_session_img_code_brower"].ToString().ToLower() != str.ToLower())
                {
                    flag = true;
                }
                else
                {
                    flag = false;
                }
            }
            if (((HttpContext.Current.Session["user_name"] == null) || PageBase.IsNeedPopBrower(HttpContext.Current.Session["user_name"].ToString())) || flag)
            {
                this.Session.Abandon();
                result = new ReturnResult();
                result.set_success(300);
                str2 = JsonHandle.ObjectToJson(result);
                HttpContext.Current.Response.ContentType = "text/json";
                HttpContext.Current.Response.Write(str2);
                HttpContext.Current.Response.End();
            }
            else if (FileCacheHelper.get_RedisStatOnline().Equals(1))
            {
                base.CheckIsOut(HttpContext.Current.Session["user_name"].ToString());
            }
            else if (FileCacheHelper.get_RedisStatOnline().Equals(2))
            {
                base.CheckIsOutStack(HttpContext.Current.Session["user_name"].ToString());
            }
            else if (base.IsUserOut(HttpContext.Current.Session["user_name"].ToString()))
            {
                this.Session.Abandon();
                result = new ReturnResult();
                result.set_success(100);
                result.set_tipinfo("");
                str2 = JsonHandle.ObjectToJson(result);
                HttpContext.Current.Response.ContentType = "text/json";
                HttpContext.Current.Response.Write(str2);
                HttpContext.Current.Response.End();
            }
        }

        protected string CodeValidateByModel()
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

        protected string comb(int[] a, int n, int m, int M)
        {
            string str = "";
            for (int i = n; i >= m; i--)
            {
                a[m - 1] = i - 1;
                if (m > 1)
                {
                    str = str + this.comb(a, i - 1, m - 1, M);
                }
                else
                {
                    for (int j = M - 1; j >= 0; j--)
                    {
                        str = str + a[j] + ",";
                    }
                }
            }
            return str;
        }

        protected double count_rlz_jssfc(string xzh, string zqh, double m_money, string pl)
        {
            string[] strArray = xzh.Split(new char[] { '~' });
            int num = 0;
            foreach (string str in strArray)
            {
                string[] strArray2 = str.Split(new char[] { ',' });
                if (zqh.IndexOf(strArray2[0] + "," + strArray2[1]) > -1)
                {
                    num++;
                }
                else if (zqh.IndexOf(strArray2[1] + "," + strArray2[0]) > -1)
                {
                    num++;
                }
            }
            int length = strArray.Length;
            double num4 = m_money / ((double) length);
            double num5 = 0.0;
            num5 = -m_money;
            return (num5 + ((num4 * num) * double.Parse(pl)));
        }

        protected double count_rlz_kl10(string xzh, string zqh, double m_money, string pl)
        {
            string[] strArray = xzh.Split(new char[] { '~' });
            int num = 0;
            foreach (string str in strArray)
            {
                string[] strArray2 = str.Split(new char[] { ',' });
                if (zqh.IndexOf(strArray2[0] + "," + strArray2[1]) > -1)
                {
                    num++;
                }
                else if (zqh.IndexOf(strArray2[1] + "," + strArray2[0]) > -1)
                {
                    num++;
                }
            }
            int length = strArray.Length;
            double num4 = m_money / ((double) length);
            double num5 = 0.0;
            num5 = -m_money;
            return (num5 + ((num4 * num) * double.Parse(pl)));
        }

        protected double count_rlz_xync(string xzh, string zqh, double m_money, string pl)
        {
            string[] strArray = xzh.Split(new char[] { '~' });
            int num = 0;
            foreach (string str in strArray)
            {
                string[] strArray2 = str.Split(new char[] { ',' });
                if (zqh.IndexOf(strArray2[0] + "," + strArray2[1]) > -1)
                {
                    num++;
                }
                else if (zqh.IndexOf(strArray2[1] + "," + strArray2[0]) > -1)
                {
                    num++;
                }
            }
            int length = strArray.Length;
            double num4 = m_money / ((double) length);
            double num5 = 0.0;
            num5 = -m_money;
            return (num5 + ((num4 * num) * double.Parse(pl)));
        }

        protected double count_rqz(string xzh, string zqh, double m_money, string pl, string pl_list)
        {
            string[] strArray = xzh.Split(new char[] { '~' });
            string[] strArray2 = pl_list.Split(new char[] { '~' });
            string[] first = zqh.Split(new char[] { ',' });
            int length = strArray2.Length;
            double num2 = 0.0;
            num2 = -m_money;
            int num3 = strArray.Length;
            double num4 = m_money / ((double) num3);
            int index = 0;
            string s = "";
            foreach (string str2 in strArray)
            {
                string[] second = str2.Split(new char[] { ',' });
                if (first.Intersect<string>(second).Count<string>() == 2)
                {
                    s = pl;
                    if ((length > index) && (strArray2[index].ToString().Trim() != ""))
                    {
                        s = strArray2[index];
                    }
                    num2 += num4 * double.Parse(s);
                }
                index++;
            }
            return num2;
        }

        protected double count_rqz_jssfc(string xzh, string zqh, double m_money, string pl)
        {
            string[] strArray = xzh.Split(new char[] { '~' });
            int num = 0;
            int num2 = 0;
            foreach (string str in strArray)
            {
                string[] strArray2 = str.Split(new char[] { ',' });
                num2 = 0;
                foreach (string str2 in strArray2)
                {
                    if (zqh.IndexOf(str2) > -1)
                    {
                        num2++;
                    }
                }
                if (num2 == 2)
                {
                    num++;
                }
            }
            int length = strArray.Length;
            double num4 = m_money / ((double) length);
            double num5 = 0.0;
            num5 = -m_money;
            return (num5 + ((num4 * num) * double.Parse(pl)));
        }

        protected double count_rqz_kl10(string xzh, string zqh, double m_money, string pl)
        {
            string[] strArray = xzh.Split(new char[] { '~' });
            int num = 0;
            int num2 = 0;
            foreach (string str in strArray)
            {
                string[] strArray2 = str.Split(new char[] { ',' });
                num2 = 0;
                foreach (string str2 in strArray2)
                {
                    if (zqh.IndexOf(str2) > -1)
                    {
                        num2++;
                    }
                }
                if (num2 == 2)
                {
                    num++;
                }
            }
            int length = strArray.Length;
            double num4 = m_money / ((double) length);
            double num5 = 0.0;
            num5 = -m_money;
            return (num5 + ((num4 * num) * double.Parse(pl)));
        }

        protected double count_rqz_xync(string xzh, string zqh, double m_money, string pl)
        {
            string[] strArray = xzh.Split(new char[] { '~' });
            int num = 0;
            int num2 = 0;
            foreach (string str in strArray)
            {
                string[] strArray2 = str.Split(new char[] { ',' });
                num2 = 0;
                foreach (string str2 in strArray2)
                {
                    if (zqh.IndexOf(str2) > -1)
                    {
                        num2++;
                    }
                }
                if (num2 == 2)
                {
                    num++;
                }
            }
            int length = strArray.Length;
            double num4 = m_money / ((double) length);
            double num5 = 0.0;
            num5 = -m_money;
            return (num5 + ((num4 * num) * double.Parse(pl)));
        }

        protected double count_rsxz_jssfc(string xzh, string zqh, double m_money, string pl)
        {
            string[] strArray = xzh.Split(new char[] { '~' });
            int num = 0;
            foreach (string str in strArray)
            {
                if (zqh.IndexOf(str) > -1)
                {
                    num++;
                }
            }
            int length = strArray.Length;
            double num4 = m_money / ((double) length);
            double num5 = 0.0;
            num5 = -m_money;
            return (num5 + ((num4 * num) * double.Parse(pl)));
        }

        protected double count_rsxz_kl10(string xzh, string zqh, double m_money, string pl)
        {
            string[] strArray = xzh.Split(new char[] { '~' });
            int num = 0;
            foreach (string str in strArray)
            {
                if (zqh.IndexOf(str) > -1)
                {
                    num++;
                }
            }
            int length = strArray.Length;
            double num4 = m_money / ((double) length);
            double num5 = 0.0;
            num5 = -m_money;
            return (num5 + ((num4 * num) * double.Parse(pl)));
        }

        protected double count_rsxz_xync(string xzh, string zqh, double m_money, string pl)
        {
            string[] strArray = xzh.Split(new char[] { '~' });
            int num = 0;
            foreach (string str in strArray)
            {
                if (zqh.IndexOf(str) > -1)
                {
                    num++;
                }
            }
            int length = strArray.Length;
            double num4 = m_money / ((double) length);
            double num5 = 0.0;
            num5 = -m_money;
            return (num5 + ((num4 * num) * double.Parse(pl)));
        }

        protected double count_rzt(string xzh, string zqh_zm, string zqh_tm, double m_money, string pl, string pl_list, ref int plIndex)
        {
            string[] strArray = xzh.Split(new char[] { '~' });
            string[] strArray2 = pl_list.Split(new char[] { '~' });
            int length = strArray2.Length;
            int num2 = 0;
            int num3 = 0;
            string[] strArray3 = pl.Split(new char[] { ',' });
            int num4 = strArray.Length;
            double num5 = m_money / ((double) num4);
            double num6 = 0.0;
            num6 = -m_money;
            int index = 0;
            string s = "";
            foreach (string str2 in strArray)
            {
                num3 = 0;
                if (str2.IndexOf(zqh_tm.Trim()) > -1)
                {
                    num3 = 1;
                }
                string[] strArray4 = str2.Split(new char[] { ',' });
                num2 = 0;
                foreach (string str3 in strArray4)
                {
                    if (zqh_zm.IndexOf(str3) > -1)
                    {
                        num2++;
                    }
                }
                if ((num2 == 1) && (num3 == 1))
                {
                    s = strArray3[0].ToString();
                    if ((length > index) && (strArray2[index].ToString().Trim() != ""))
                    {
                        s = strArray2[index].Split(new char[] { ',' })[0].ToString().Trim();
                        plIndex = 1;
                    }
                    num6 += num5 * double.Parse(s);
                }
                else if (num2 == 2)
                {
                    s = strArray3[1].ToString();
                    if ((length > index) && (strArray2[index].ToString().Trim() != ""))
                    {
                        s = strArray2[index].Split(new char[] { ',' })[1].ToString().Trim();
                        plIndex = 2;
                    }
                    num6 += num5 * double.Parse(s);
                }
                index++;
            }
            return num6;
        }

        protected double count_sqr(string xzh, string zqh, double m_money, string pl, string pl_list, ref int plIndex)
        {
            string[] strArray = xzh.Split(new char[] { '~' });
            string[] strArray2 = pl_list.Split(new char[] { '~' });
            string[] first = zqh.Split(new char[] { ',' });
            int length = strArray2.Length;
            double num2 = 0.0;
            num2 = -m_money;
            int num3 = strArray.Length;
            double num4 = m_money / ((double) num3);
            string[] strArray4 = pl.Split(new char[] { ',' });
            string s = "";
            int index = 0;
            foreach (string str2 in strArray)
            {
                string[] second = str2.Split(new char[] { ',' });
                IEnumerable<string> source = first.Intersect<string>(second);
                if (source.Count<string>() == 3)
                {
                    s = strArray4[1].ToString();
                    if ((length > index) && (strArray2[index].ToString().Trim() != ""))
                    {
                        s = strArray2[index].Split(new char[] { ',' })[1].ToString().Trim();
                        plIndex = 2;
                    }
                    num2 += num4 * double.Parse(s);
                }
                else if (source.Count<string>() == 2)
                {
                    s = strArray4[0].ToString();
                    if (strArray2[index].ToString().Trim() != "")
                    {
                        s = strArray2[index].Split(new char[] { ',' })[0].ToString().Trim();
                        plIndex = 1;
                    }
                    num2 += num4 * double.Parse(s);
                }
                index++;
            }
            return num2;
        }

        protected double count_sqz(string xzh, string zqh, double m_money, string pl, string pl_list)
        {
            string[] strArray = xzh.Split(new char[] { '~' });
            string[] strArray2 = pl_list.Split(new char[] { '~' });
            string[] first = zqh.Split(new char[] { ',' });
            int length = strArray2.Length;
            double num2 = 0.0;
            num2 = -m_money;
            int num3 = strArray.Length;
            double num4 = m_money / ((double) num3);
            string s = "";
            int index = 0;
            foreach (string str2 in strArray)
            {
                string[] second = str2.Split(new char[] { ',' });
                if (first.Intersect<string>(second).Count<string>() == 3)
                {
                    s = pl;
                    if ((length > index) && (strArray2[index].ToString().Trim() != ""))
                    {
                        s = strArray2[index].ToString();
                    }
                    num2 += num4 * double.Parse(s);
                }
                index++;
            }
            return num2;
        }

        protected double count_sqz_jssfc(string xzh, string zqh, double m_money, string pl)
        {
            string[] strArray = xzh.Split(new char[] { '~' });
            int num = 0;
            int num2 = 0;
            foreach (string str in strArray)
            {
                string[] strArray2 = str.Split(new char[] { ',' });
                num2 = 0;
                foreach (string str2 in strArray2)
                {
                    if (zqh.IndexOf(str2) > -1)
                    {
                        num2++;
                    }
                }
                if (num2 == 3)
                {
                    num++;
                }
            }
            int length = strArray.Length;
            double num4 = m_money / ((double) length);
            double num5 = 0.0;
            num5 = -m_money;
            return (num5 + ((num4 * num) * double.Parse(pl)));
        }

        protected double count_sqz_kl10(string xzh, string zqh, double m_money, string pl)
        {
            string[] strArray = xzh.Split(new char[] { '~' });
            int num = 0;
            int num2 = 0;
            foreach (string str in strArray)
            {
                string[] strArray2 = str.Split(new char[] { ',' });
                num2 = 0;
                foreach (string str2 in strArray2)
                {
                    if (zqh.IndexOf(str2) > -1)
                    {
                        num2++;
                    }
                }
                if (num2 == 3)
                {
                    num++;
                }
            }
            int length = strArray.Length;
            double num4 = m_money / ((double) length);
            double num5 = 0.0;
            num5 = -m_money;
            return (num5 + ((num4 * num) * double.Parse(pl)));
        }

        protected double count_sqz_xync(string xzh, string zqh, double m_money, string pl)
        {
            string[] strArray = xzh.Split(new char[] { '~' });
            int num = 0;
            int num2 = 0;
            foreach (string str in strArray)
            {
                string[] strArray2 = str.Split(new char[] { ',' });
                num2 = 0;
                foreach (string str2 in strArray2)
                {
                    if (zqh.IndexOf(str2) > -1)
                    {
                        num2++;
                    }
                }
                if (num2 == 3)
                {
                    num++;
                }
            }
            int length = strArray.Length;
            double num4 = m_money / ((double) length);
            double num5 = 0.0;
            num5 = -m_money;
            return (num5 + ((num4 * num) * double.Parse(pl)));
        }

        protected double count_sqz4_jssfc(string xzh, string zqh, double m_money, string pl)
        {
            string[] strArray = xzh.Split(new char[] { '~' });
            int num = 0;
            int num2 = 0;
            foreach (string str in strArray)
            {
                string[] strArray2 = str.Split(new char[] { ',' });
                num2 = 0;
                foreach (string str2 in strArray2)
                {
                    if (zqh.IndexOf(str2) > -1)
                    {
                        num2++;
                    }
                }
                if (num2 == 4)
                {
                    num++;
                }
            }
            int length = strArray.Length;
            double num4 = m_money / ((double) length);
            double num5 = 0.0;
            num5 = -m_money;
            return (num5 + ((num4 * num) * double.Parse(pl)));
        }

        protected double count_sqz4_kl10(string xzh, string zqh, double m_money, string pl)
        {
            string[] strArray = xzh.Split(new char[] { '~' });
            int num = 0;
            int num2 = 0;
            foreach (string str in strArray)
            {
                string[] strArray2 = str.Split(new char[] { ',' });
                num2 = 0;
                foreach (string str2 in strArray2)
                {
                    if (zqh.IndexOf(str2) > -1)
                    {
                        num2++;
                    }
                }
                if (num2 == 4)
                {
                    num++;
                }
            }
            int length = strArray.Length;
            double num4 = m_money / ((double) length);
            double num5 = 0.0;
            num5 = -m_money;
            return (num5 + ((num4 * num) * double.Parse(pl)));
        }

        protected double count_sqz4_xync(string xzh, string zqh, double m_money, string pl)
        {
            string[] strArray = xzh.Split(new char[] { '~' });
            int num = 0;
            int num2 = 0;
            foreach (string str in strArray)
            {
                string[] strArray2 = str.Split(new char[] { ',' });
                num2 = 0;
                foreach (string str2 in strArray2)
                {
                    if (zqh.IndexOf(str2) > -1)
                    {
                        num2++;
                    }
                }
                if (num2 == 4)
                {
                    num++;
                }
            }
            int length = strArray.Length;
            double num4 = m_money / ((double) length);
            double num5 = 0.0;
            num5 = -m_money;
            return (num5 + ((num4 * num) * double.Parse(pl)));
        }

        protected double count_sxl(string xzh, string zqh1, double m_money, string pl, string pl_list)
        {
            string[] strArray = xzh.Split(new char[] { '~' });
            string[] strArray2 = pl_list.Split(new char[] { '~' });
            int length = strArray2.Length;
            string[] strArray3 = pl.Split(new char[] { ',' });
            int num2 = 0;
            int num3 = strArray.Length;
            double num4 = m_money / ((double) num3);
            double num5 = 0.0;
            num5 = -m_money;
            int num6 = 0;
            int index = 0;
            string s = "";
            foreach (string str2 in strArray)
            {
                string[] strArray4 = str2.Split(new char[] { ',' });
                num2 = 0;
                num6 = 0;
                foreach (string str3 in strArray4)
                {
                    if (zqh1.IndexOf(str3) < 0)
                    {
                        num2 = -1;
                        break;
                    }
                    if (str3.Trim() == base.get_YearLianID())
                    {
                        num6 = 1;
                    }
                }
                if (num2 == 0)
                {
                    if (num6 < 1)
                    {
                        s = strArray3[0].ToString();
                        if ((length > index) && (strArray2[index].ToString().Trim() != ""))
                        {
                            s = strArray2[index].Split(new char[] { '|' })[1].ToString();
                        }
                        num5 += num4 * double.Parse(s);
                    }
                    else
                    {
                        s = strArray3[1].ToString();
                        if ((length > index) && (strArray2[index].ToString().Trim() != ""))
                        {
                            s = strArray2[index].Split(new char[] { '|' })[1].ToString();
                        }
                        num5 += num4 * double.Parse(s);
                    }
                }
                index++;
            }
            return num5;
        }

        protected double count_szy(string xzh, string zqh, double m_money, string pl, string pl_list)
        {
            string[] strArray = xzh.Split(new char[] { '~' });
            string[] strArray2 = pl_list.Split(new char[] { '~' });
            string[] first = zqh.Split(new char[] { ',' });
            int length = strArray2.Length;
            int num2 = strArray.Length;
            double num3 = m_money / ((double) num2);
            double num4 = 0.0;
            num4 = -m_money;
            int index = 0;
            string s = "";
            foreach (string str2 in strArray)
            {
                string[] second = str2.Split(new char[] { ',' });
                if (first.Intersect<string>(second).Count<string>() > 0)
                {
                    s = pl;
                    if ((length > index) && (strArray2[index].ToString().Trim() != ""))
                    {
                        s = strArray2[index];
                    }
                    num4 += num3 * double.Parse(s);
                }
                index++;
            }
            return num4;
        }

        protected double count_tc(string xzh, string zqh_zm, string zqh_tm, double m_money, string pl, string pl_list)
        {
            string[] strArray = xzh.Split(new char[] { '~' });
            string[] strArray2 = pl_list.Split(new char[] { '~' });
            int length = strArray2.Length;
            int num2 = strArray.Length;
            double num3 = m_money / ((double) num2);
            double num4 = 0.0;
            num4 = -m_money;
            int num6 = 0;
            int num7 = 0;
            int index = 0;
            string s = "";
            foreach (string str2 in strArray)
            {
                num7 = 0;
                if (str2.IndexOf(zqh_tm.Trim()) > -1)
                {
                    num7 = 1;
                }
                string[] strArray3 = str2.Split(new char[] { ',' });
                num6 = 0;
                foreach (string str3 in strArray3)
                {
                    if (zqh_zm.IndexOf(str3) > -1)
                    {
                        num6++;
                    }
                }
                if ((num6 == 1) && (num7 == 1))
                {
                    s = pl;
                    if ((length > index) && (strArray2[index].ToString().Trim() != ""))
                    {
                        s = strArray2[index];
                    }
                    num4 += num3 * double.Parse(s);
                }
                index++;
            }
            return num4;
        }

        protected double count_tmbs(string xzh, string zqh, double m_money, string pl, string pl_list)
        {
            string[] strArray = xzh.Split(new char[] { '~' });
            string[] strArray2 = pl_list.Split(new char[] { '~' });
            string[] first = zqh.Split(new char[] { ',' });
            int length = strArray2.Length;
            int num2 = strArray.Length;
            double num3 = m_money / ((double) num2);
            double num4 = 0.0;
            num4 = -m_money;
            int index = 0;
            string s = "";
            foreach (string str2 in strArray)
            {
                string[] second = str2.Split(new char[] { ',' });
                if (first.Intersect<string>(second).Count<string>() > 0)
                {
                    s = pl;
                    if ((length > index) && (strArray2[index].ToString().Trim() != ""))
                    {
                        s = strArray2[index].Split(new char[] { '|' })[1];
                    }
                    num4 += num3 * double.Parse(s);
                }
                index++;
            }
            return num4;
        }

        protected double count_wbz(string xzh, string zqh, double m_money, string pl, string pl_list)
        {
            string[] strArray = xzh.Split(new char[] { '~' });
            string[] strArray2 = pl_list.Split(new char[] { '~' });
            string[] first = zqh.Split(new char[] { ',' });
            int length = strArray2.Length;
            int num2 = strArray.Length;
            double num3 = m_money / ((double) num2);
            double num4 = 0.0;
            num4 = -m_money;
            int index = 0;
            string s = "";
            foreach (string str2 in strArray)
            {
                string[] second = str2.Split(new char[] { ',' });
                if (first.Intersect<string>(second).Count<string>() == 0)
                {
                    s = pl;
                    if ((length > index) && (strArray2[index].ToString().Trim() != ""))
                    {
                        s = strArray2[index];
                    }
                    num4 += num3 * double.Parse(s);
                }
                index++;
            }
            return num4;
        }

        protected double count_wqz5_jssfc(string xzh, string zqh, double m_money, string pl)
        {
            string[] strArray = xzh.Split(new char[] { '~' });
            int num = 0;
            int num2 = 0;
            foreach (string str in strArray)
            {
                string[] strArray2 = str.Split(new char[] { ',' });
                num2 = 0;
                foreach (string str2 in strArray2)
                {
                    if (zqh.IndexOf(str2) > -1)
                    {
                        num2++;
                    }
                }
                if (num2 == 5)
                {
                    num++;
                }
            }
            int length = strArray.Length;
            double num4 = m_money / ((double) length);
            double num5 = 0.0;
            num5 = -m_money;
            return (num5 + ((num4 * num) * double.Parse(pl)));
        }

        protected double count_wqz5_kl10(string xzh, string zqh, double m_money, string pl)
        {
            string[] strArray = xzh.Split(new char[] { '~' });
            int num = 0;
            int num2 = 0;
            foreach (string str in strArray)
            {
                string[] strArray2 = str.Split(new char[] { ',' });
                num2 = 0;
                foreach (string str2 in strArray2)
                {
                    if (zqh.IndexOf(str2) > -1)
                    {
                        num2++;
                    }
                }
                if (num2 == 5)
                {
                    num++;
                }
            }
            int length = strArray.Length;
            double num4 = m_money / ((double) length);
            double num5 = 0.0;
            num5 = -m_money;
            return (num5 + ((num4 * num) * double.Parse(pl)));
        }

        protected double count_wqz5_xync(string xzh, string zqh, double m_money, string pl)
        {
            string[] strArray = xzh.Split(new char[] { '~' });
            int num = 0;
            int num2 = 0;
            foreach (string str in strArray)
            {
                string[] strArray2 = str.Split(new char[] { ',' });
                num2 = 0;
                foreach (string str2 in strArray2)
                {
                    if (zqh.IndexOf(str2) > -1)
                    {
                        num2++;
                    }
                }
                if (num2 == 5)
                {
                    num++;
                }
            }
            int length = strArray.Length;
            double num4 = m_money / ((double) length);
            double num5 = 0.0;
            num5 = -m_money;
            return (num5 + ((num4 * num) * double.Parse(pl)));
        }

        protected double count_wsl(string xzh, string zqh, double m_money, string pl, string pl_list)
        {
            string[] strArray = xzh.Split(new char[] { '~' });
            string[] strArray2 = pl_list.Split(new char[] { '~' });
            int length = strArray2.Length;
            string[] strArray3 = pl.Split(new char[] { ',' });
            int num2 = 0;
            int num3 = strArray.Length;
            double num4 = m_money / ((double) num3);
            double num5 = 0.0;
            num5 = -m_money;
            int num6 = 0;
            int index = 0;
            string s = "";
            foreach (string str2 in strArray)
            {
                string[] strArray4 = str2.Split(new char[] { ',' });
                num2 = 0;
                num6 = 0;
                foreach (string str3 in strArray4)
                {
                    if (zqh.IndexOf(str3.Trim()) < 0)
                    {
                        num2 = -1;
                        break;
                    }
                    if (str3.Trim() == "10")
                    {
                        num6 = 1;
                    }
                }
                if (num2 == 0)
                {
                    if (num6 == 1)
                    {
                        s = strArray3[1].ToString();
                        if ((length > index) && (strArray2[index].ToString().Trim() != ""))
                        {
                            s = strArray2[index].Split(new char[] { '|' })[1].ToString();
                        }
                        num5 += num4 * double.Parse(s);
                    }
                    else
                    {
                        s = strArray3[0].ToString();
                        if ((length > index) && (strArray2[index].ToString().Trim() != ""))
                        {
                            s = strArray2[index].Split(new char[] { '|' })[1].ToString();
                        }
                        num5 += num4 * double.Parse(s);
                    }
                }
                index++;
            }
            return num5;
        }

        public bool DeleteCreditLock(string u_name)
        {
            if (FileCacheHelper.get_IsRedisCreditLock().Equals("0"))
            {
                int num = CallBLL.cz_credit_lock_bll.Delete(u_name);
                return true;
            }
            return (FileCacheHelper.get_IsRedisCreditLock().Equals("1") || (FileCacheHelper.get_IsRedisCreditLock().Equals("2") || true));
        }

        public DataTable FGS_WTChche(int? lotteryID)
        {
            if (lotteryID.HasValue)
            {
                string str = HttpContext.Current.Session["user_name"].ToString();
                cz_userinfo_session getUserModelInfo = this.GetUserModelInfo;
                int valueOrDefault = lotteryID.GetValueOrDefault();
                if (lotteryID.HasValue)
                {
                    DataTable cache;
                    DataTable oddsWTs;
                    switch (valueOrDefault)
                    {
                        case 0:
                            cache = CacheHelper.GetCache("fgs_wt_FileCacheKey" + HttpContext.Current.Session["user_name"].ToString() + 0) as DataTable;
                            if (cache != null)
                            {
                                return cache;
                            }
                            if (!getUserModelInfo.get_kc_op_odds().Equals(1))
                            {
                                goto Label_14C9;
                            }
                            oddsWTs = CallBLL.cz_odds_wt_kl10_bll.GetOddsWTs(getUserModelInfo.get_kc_session().get_fgsname());
                            CacheHelper.SetPublicFileCache("fgs_wt_FileCacheKey" + HttpContext.Current.Session["user_name"].ToString() + 0, oddsWTs, PageBase.GetPublicForderPath(FileCacheHelper.get_FGS_WTCaches_KL10()));
                            return oddsWTs;

                        case 1:
                            cache = CacheHelper.GetCache("fgs_wt_FileCacheKey" + HttpContext.Current.Session["user_name"].ToString() + 1) as DataTable;
                            if (cache != null)
                            {
                                return cache;
                            }
                            if (!getUserModelInfo.get_kc_op_odds().Equals(1))
                            {
                                goto Label_14C9;
                            }
                            oddsWTs = CallBLL.cz_odds_wt_cqsc_bll.GetOddsWTs(getUserModelInfo.get_kc_session().get_fgsname());
                            CacheHelper.SetPublicFileCache("fgs_wt_FileCacheKey" + HttpContext.Current.Session["user_name"].ToString() + 1, oddsWTs, PageBase.GetPublicForderPath(FileCacheHelper.get_FGS_WTCaches_CQSC()));
                            return oddsWTs;

                        case 2:
                            cache = CacheHelper.GetCache("fgs_wt_FileCacheKey" + HttpContext.Current.Session["user_name"].ToString() + 2) as DataTable;
                            if (cache != null)
                            {
                                return cache;
                            }
                            if (!getUserModelInfo.get_kc_op_odds().Equals(1))
                            {
                                goto Label_14C9;
                            }
                            oddsWTs = CallBLL.cz_odds_wt_pk10_bll.GetOddsWTs(getUserModelInfo.get_kc_session().get_fgsname());
                            CacheHelper.SetPublicFileCache("fgs_wt_FileCacheKey" + HttpContext.Current.Session["user_name"].ToString() + 2, oddsWTs, PageBase.GetPublicForderPath(FileCacheHelper.get_FGS_WTCaches_PK10()));
                            return oddsWTs;

                        case 3:
                            cache = CacheHelper.GetCache("fgs_wt_FileCacheKey" + HttpContext.Current.Session["user_name"].ToString() + 3) as DataTable;
                            if (cache != null)
                            {
                                return cache;
                            }
                            if (!getUserModelInfo.get_kc_op_odds().Equals(1))
                            {
                                goto Label_14C9;
                            }
                            oddsWTs = CallBLL.cz_odds_wt_xync_bll.GetOddsWTs(getUserModelInfo.get_kc_session().get_fgsname());
                            CacheHelper.SetPublicFileCache("fgs_wt_FileCacheKey" + HttpContext.Current.Session["user_name"].ToString() + 3, oddsWTs, PageBase.GetPublicForderPath(FileCacheHelper.get_FGS_WTCaches_XYNC()));
                            return oddsWTs;

                        case 4:
                            cache = CacheHelper.GetCache("fgs_wt_FileCacheKey" + HttpContext.Current.Session["user_name"].ToString() + 4) as DataTable;
                            if (cache != null)
                            {
                                return cache;
                            }
                            if (!getUserModelInfo.get_kc_op_odds().Equals(1))
                            {
                                goto Label_14C9;
                            }
                            oddsWTs = CallBLL.cz_odds_wt_jsk3_bll.GetOddsWTs(getUserModelInfo.get_kc_session().get_fgsname());
                            CacheHelper.SetPublicFileCache("fgs_wt_FileCacheKey" + HttpContext.Current.Session["user_name"].ToString() + 4, oddsWTs, PageBase.GetPublicForderPath(FileCacheHelper.get_FGS_WTCaches_JSK3()));
                            return oddsWTs;

                        case 5:
                            cache = CacheHelper.GetCache("fgs_wt_FileCacheKey" + HttpContext.Current.Session["user_name"].ToString() + 5) as DataTable;
                            if (cache != null)
                            {
                                return cache;
                            }
                            if (!getUserModelInfo.get_kc_op_odds().Equals(1))
                            {
                                goto Label_14C9;
                            }
                            oddsWTs = CallBLL.cz_odds_wt_kl8_bll.GetOddsWTs(getUserModelInfo.get_kc_session().get_fgsname());
                            CacheHelper.SetPublicFileCache("fgs_wt_FileCacheKey" + HttpContext.Current.Session["user_name"].ToString() + 5, oddsWTs, PageBase.GetPublicForderPath(FileCacheHelper.get_FGS_WTCaches_KL8()));
                            return oddsWTs;

                        case 6:
                            cache = CacheHelper.GetCache("fgs_wt_FileCacheKey" + HttpContext.Current.Session["user_name"].ToString() + 6) as DataTable;
                            if (cache != null)
                            {
                                return cache;
                            }
                            if (!getUserModelInfo.get_kc_op_odds().Equals(1))
                            {
                                goto Label_14C9;
                            }
                            oddsWTs = CallBLL.cz_odds_wt_k8sc_bll.GetOddsWTs(getUserModelInfo.get_kc_session().get_fgsname());
                            CacheHelper.SetPublicFileCache("fgs_wt_FileCacheKey" + HttpContext.Current.Session["user_name"].ToString() + 6, oddsWTs, PageBase.GetPublicForderPath(FileCacheHelper.get_FGS_WTCaches_K8SC()));
                            return oddsWTs;

                        case 7:
                            cache = CacheHelper.GetCache("fgs_wt_FileCacheKey" + HttpContext.Current.Session["user_name"].ToString() + 7) as DataTable;
                            if (cache != null)
                            {
                                return cache;
                            }
                            if (!getUserModelInfo.get_kc_op_odds().Equals(1))
                            {
                                goto Label_14C9;
                            }
                            oddsWTs = CallBLL.cz_odds_wt_pcdd_bll.GetOddsWTs(getUserModelInfo.get_kc_session().get_fgsname());
                            CacheHelper.SetPublicFileCache("fgs_wt_FileCacheKey" + HttpContext.Current.Session["user_name"].ToString() + 7, oddsWTs, PageBase.GetPublicForderPath(FileCacheHelper.get_FGS_WTCaches_PCDD()));
                            return oddsWTs;

                        case 8:
                            cache = CacheHelper.GetCache("fgs_wt_FileCacheKey" + HttpContext.Current.Session["user_name"].ToString() + 8) as DataTable;
                            if (cache != null)
                            {
                                return cache;
                            }
                            if (!getUserModelInfo.get_kc_op_odds().Equals(1))
                            {
                                goto Label_14C9;
                            }
                            oddsWTs = CallBLL.cz_odds_wt_pkbjl_bll.GetOddsWTs(getUserModelInfo.get_kc_session().get_fgsname());
                            CacheHelper.SetPublicFileCache("fgs_wt_FileCacheKey" + HttpContext.Current.Session["user_name"].ToString() + 8, oddsWTs, PageBase.GetPublicForderPath(FileCacheHelper.get_FGS_WTCaches_PKBJL()));
                            return oddsWTs;

                        case 9:
                            cache = CacheHelper.GetCache("fgs_wt_FileCacheKey" + HttpContext.Current.Session["user_name"].ToString() + 9) as DataTable;
                            if (cache != null)
                            {
                                return cache;
                            }
                            if (!getUserModelInfo.get_kc_op_odds().Equals(1))
                            {
                                goto Label_14C9;
                            }
                            oddsWTs = CallBLL.cz_odds_wt_xyft5_bll.GetOddsWTs(getUserModelInfo.get_kc_session().get_fgsname());
                            CacheHelper.SetPublicFileCache("fgs_wt_FileCacheKey" + HttpContext.Current.Session["user_name"].ToString() + 9, oddsWTs, PageBase.GetPublicForderPath(FileCacheHelper.get_FGS_WTCaches_XYFT5()));
                            return oddsWTs;

                        case 10:
                            cache = CacheHelper.GetCache("fgs_wt_FileCacheKey" + HttpContext.Current.Session["user_name"].ToString() + 10) as DataTable;
                            if (cache != null)
                            {
                                return cache;
                            }
                            if (!getUserModelInfo.get_kc_op_odds().Equals(1))
                            {
                                goto Label_14C9;
                            }
                            oddsWTs = CallBLL.cz_odds_wt_jscar_bll.GetOddsWTs(getUserModelInfo.get_kc_session().get_fgsname());
                            CacheHelper.SetPublicFileCache("fgs_wt_FileCacheKey" + HttpContext.Current.Session["user_name"].ToString() + 10, oddsWTs, PageBase.GetPublicForderPath(FileCacheHelper.get_FGS_WTCaches_JSCAR()));
                            return oddsWTs;

                        case 11:
                            cache = CacheHelper.GetCache("fgs_wt_FileCacheKey" + HttpContext.Current.Session["user_name"].ToString() + 11) as DataTable;
                            if (cache != null)
                            {
                                return cache;
                            }
                            if (!getUserModelInfo.get_kc_op_odds().Equals(1))
                            {
                                goto Label_14C9;
                            }
                            oddsWTs = CallBLL.cz_odds_wt_speed5_bll.GetOddsWTs(getUserModelInfo.get_kc_session().get_fgsname());
                            CacheHelper.SetPublicFileCache("fgs_wt_FileCacheKey" + HttpContext.Current.Session["user_name"].ToString() + 11, oddsWTs, PageBase.GetPublicForderPath(FileCacheHelper.get_FGS_WTCaches_SPEED5()));
                            return oddsWTs;

                        case 12:
                            cache = CacheHelper.GetCache("fgs_wt_FileCacheKey" + HttpContext.Current.Session["user_name"].ToString() + 12) as DataTable;
                            if (cache != null)
                            {
                                return cache;
                            }
                            if (!getUserModelInfo.get_kc_op_odds().Equals(1))
                            {
                                goto Label_14C9;
                            }
                            oddsWTs = CallBLL.cz_odds_wt_jspk10_bll.GetOddsWTs(getUserModelInfo.get_kc_session().get_fgsname());
                            CacheHelper.SetPublicFileCache("fgs_wt_FileCacheKey" + HttpContext.Current.Session["user_name"].ToString() + 12, oddsWTs, PageBase.GetPublicForderPath(FileCacheHelper.get_FGS_WTCaches_JSPK10()));
                            return oddsWTs;

                        case 13:
                            cache = CacheHelper.GetCache("fgs_wt_FileCacheKey" + HttpContext.Current.Session["user_name"].ToString() + 13) as DataTable;
                            if (cache != null)
                            {
                                return cache;
                            }
                            if (!getUserModelInfo.get_kc_op_odds().Equals(1))
                            {
                                goto Label_14C9;
                            }
                            oddsWTs = CallBLL.cz_odds_wt_jscqsc_bll.GetOddsWTs(getUserModelInfo.get_kc_session().get_fgsname());
                            CacheHelper.SetPublicFileCache("fgs_wt_FileCacheKey" + HttpContext.Current.Session["user_name"].ToString() + 13, oddsWTs, PageBase.GetPublicForderPath(FileCacheHelper.get_FGS_WTCaches_JSCQSC()));
                            return oddsWTs;

                        case 14:
                            cache = CacheHelper.GetCache("fgs_wt_FileCacheKey" + HttpContext.Current.Session["user_name"].ToString() + 14) as DataTable;
                            if (cache != null)
                            {
                                return cache;
                            }
                            if (!getUserModelInfo.get_kc_op_odds().Equals(1))
                            {
                                goto Label_14C9;
                            }
                            oddsWTs = CallBLL.cz_odds_wt_jssfc_bll.GetOddsWTs(getUserModelInfo.get_kc_session().get_fgsname());
                            CacheHelper.SetPublicFileCache("fgs_wt_FileCacheKey" + HttpContext.Current.Session["user_name"].ToString() + 14, oddsWTs, PageBase.GetPublicForderPath(FileCacheHelper.get_FGS_WTCaches_JSSFC()));
                            return oddsWTs;

                        case 15:
                            cache = CacheHelper.GetCache("fgs_wt_FileCacheKey" + HttpContext.Current.Session["user_name"].ToString() + 15) as DataTable;
                            if (cache != null)
                            {
                                return cache;
                            }
                            if (!getUserModelInfo.get_kc_op_odds().Equals(1))
                            {
                                goto Label_14C9;
                            }
                            oddsWTs = CallBLL.cz_odds_wt_jsft2_bll.GetOddsWTs(getUserModelInfo.get_kc_session().get_fgsname());
                            CacheHelper.SetPublicFileCache("fgs_wt_FileCacheKey" + HttpContext.Current.Session["user_name"].ToString() + 15, oddsWTs, PageBase.GetPublicForderPath(FileCacheHelper.get_FGS_WTCaches_JSFT2()));
                            return oddsWTs;

                        case 0x10:
                            cache = CacheHelper.GetCache("fgs_wt_FileCacheKey" + HttpContext.Current.Session["user_name"].ToString() + 0x10) as DataTable;
                            if (cache != null)
                            {
                                return cache;
                            }
                            if (!getUserModelInfo.get_kc_op_odds().Equals(1))
                            {
                                goto Label_14C9;
                            }
                            oddsWTs = CallBLL.cz_odds_wt_car168_bll.GetOddsWTs(getUserModelInfo.get_kc_session().get_fgsname());
                            CacheHelper.SetPublicFileCache("fgs_wt_FileCacheKey" + HttpContext.Current.Session["user_name"].ToString() + 0x10, oddsWTs, PageBase.GetPublicForderPath(FileCacheHelper.get_FGS_WTCaches_CAR168()));
                            return oddsWTs;

                        case 0x11:
                            cache = CacheHelper.GetCache("fgs_wt_FileCacheKey" + HttpContext.Current.Session["user_name"].ToString() + 0x11) as DataTable;
                            if (cache != null)
                            {
                                return cache;
                            }
                            if (!getUserModelInfo.get_kc_op_odds().Equals(1))
                            {
                                goto Label_14C9;
                            }
                            oddsWTs = CallBLL.cz_odds_wt_ssc168_bll.GetOddsWTs(getUserModelInfo.get_kc_session().get_fgsname());
                            CacheHelper.SetPublicFileCache("fgs_wt_FileCacheKey" + HttpContext.Current.Session["user_name"].ToString() + 0x11, oddsWTs, PageBase.GetPublicForderPath(FileCacheHelper.get_FGS_WTCaches_SSC168()));
                            return oddsWTs;

                        case 0x12:
                            cache = CacheHelper.GetCache("fgs_wt_FileCacheKey" + HttpContext.Current.Session["user_name"].ToString() + 0x12) as DataTable;
                            if (cache != null)
                            {
                                return cache;
                            }
                            if (!getUserModelInfo.get_kc_op_odds().Equals(1))
                            {
                                goto Label_14C9;
                            }
                            oddsWTs = CallBLL.cz_odds_wt_vrcar_bll.GetOddsWTs(getUserModelInfo.get_kc_session().get_fgsname());
                            CacheHelper.SetPublicFileCache("fgs_wt_FileCacheKey" + HttpContext.Current.Session["user_name"].ToString() + 0x12, oddsWTs, PageBase.GetPublicForderPath(FileCacheHelper.get_FGS_WTCaches_VRCAR()));
                            return oddsWTs;

                        case 0x13:
                            cache = CacheHelper.GetCache("fgs_wt_FileCacheKey" + HttpContext.Current.Session["user_name"].ToString() + 0x13) as DataTable;
                            if (cache != null)
                            {
                                return cache;
                            }
                            if (!getUserModelInfo.get_kc_op_odds().Equals(1))
                            {
                                goto Label_14C9;
                            }
                            oddsWTs = CallBLL.cz_odds_wt_vrssc_bll.GetOddsWTs(getUserModelInfo.get_kc_session().get_fgsname());
                            CacheHelper.SetPublicFileCache("fgs_wt_FileCacheKey" + HttpContext.Current.Session["user_name"].ToString() + 0x13, oddsWTs, PageBase.GetPublicForderPath(FileCacheHelper.get_FGS_WTCaches_VRSSC()));
                            return oddsWTs;

                        case 20:
                            cache = CacheHelper.GetCache("fgs_wt_FileCacheKey" + HttpContext.Current.Session["user_name"].ToString() + 20) as DataTable;
                            if (cache != null)
                            {
                                return cache;
                            }
                            if (!getUserModelInfo.get_kc_op_odds().Equals(1))
                            {
                                goto Label_14C9;
                            }
                            oddsWTs = CallBLL.cz_odds_wt_xyftoa_bll.GetOddsWTs(getUserModelInfo.get_kc_session().get_fgsname());
                            CacheHelper.SetPublicFileCache("fgs_wt_FileCacheKey" + HttpContext.Current.Session["user_name"].ToString() + 20, oddsWTs, PageBase.GetPublicForderPath(FileCacheHelper.get_FGS_WTCaches_XYFTOA()));
                            return oddsWTs;

                        case 0x15:
                            cache = CacheHelper.GetCache("fgs_wt_FileCacheKey" + HttpContext.Current.Session["user_name"].ToString() + 0x15) as DataTable;
                            if (cache != null)
                            {
                                return cache;
                            }
                            if (!getUserModelInfo.get_kc_op_odds().Equals(1))
                            {
                                goto Label_14C9;
                            }
                            oddsWTs = CallBLL.cz_odds_wt_xyftsg_bll.GetOddsWTs(getUserModelInfo.get_kc_session().get_fgsname());
                            CacheHelper.SetPublicFileCache("fgs_wt_FileCacheKey" + HttpContext.Current.Session["user_name"].ToString() + 0x15, oddsWTs, PageBase.GetPublicForderPath(FileCacheHelper.get_FGS_WTCaches_XYFTSG()));
                            return oddsWTs;

                        case 0x16:
                            cache = CacheHelper.GetCache("fgs_wt_FileCacheKey" + HttpContext.Current.Session["user_name"].ToString() + 0x16) as DataTable;
                            if (cache != null)
                            {
                                return cache;
                            }
                            if (!getUserModelInfo.get_kc_op_odds().Equals(1))
                            {
                                goto Label_14C9;
                            }
                            oddsWTs = CallBLL.cz_odds_wt_happycar_bll.GetOddsWTs(getUserModelInfo.get_kc_session().get_fgsname());
                            CacheHelper.SetPublicFileCache("fgs_wt_FileCacheKey" + HttpContext.Current.Session["user_name"].ToString() + 0x16, oddsWTs, PageBase.GetPublicForderPath(FileCacheHelper.get_FGS_WTCaches_HAPPYCAR()));
                            return oddsWTs;

                        case 100:
                            cache = CacheHelper.GetCache("fgs_wt_FileCacheKey" + HttpContext.Current.Session["user_name"].ToString() + 100) as DataTable;
                            if (cache != null)
                            {
                                return cache;
                            }
                            if (getUserModelInfo.get_six_op_odds().Equals(1))
                            {
                                oddsWTs = CallBLL.cz_odds_wt_six_bll.GetOddsWTs(getUserModelInfo.get_six_session().get_fgsname());
                                CacheHelper.SetPublicFileCache("fgs_wt_FileCacheKey" + HttpContext.Current.Session["user_name"].ToString() + 100, oddsWTs, PageBase.GetPublicForderPath(FileCacheHelper.get_FGS_WTCaches_SIX()));
                                return oddsWTs;
                            }
                            goto Label_14C9;
                    }
                }
            }
            return null;
        Label_14C9:
            return null;
        }

        protected void ForcedModifyPassword()
        {
            string str = this.Context.Request.Path.ToLower();
            if ((!string.IsNullOrEmpty(this.Session["modifypassword"]) && (str.IndexOf("resetpasswd1.aspx") < 0)) && (str.IndexOf("resetpasswd.aspx") < 0))
            {
                if (this.WebModelView.Equals(1))
                {
                    base.Response.Redirect(string.Format("ResetPasswd1.aspx", new object[0]), true);
                }
                else
                {
                    base.Response.Redirect(string.Format("/OldWeb/ResetPasswd1.aspx", new object[0]), true);
                }
                base.Response.End();
            }
        }

        private ArrayList get_current_lottery()
        {
            ArrayList list = new ArrayList();
            DataTable lotteryList = this.GetLotteryList();
            if (lotteryList != null)
            {
                foreach (DataRow row in lotteryList.Rows)
                {
                    list.Add(row["id"].ToString());
                }
            }
            return list;
        }

        public int get_current_master_id()
        {
            int num = 0;
            ArrayList list = this.get_current_lottery();
            int count = list.Count;
            int num4 = 100;
            if (list.Contains(num4.ToString()))
            {
                if (count == 1)
                {
                    num = 1;
                }
                return num;
            }
            return 2;
        }

        protected string GetCAR168_PlayIDChange(string playType)
        {
            string str = "";
            string str3 = playType;
            if (str3 == null)
            {
                return str;
            }
            if (!(str3 == "car168_lmp"))
            {
                if (str3 != "car168_d1_10")
                {
                    if (str3 == "car168_d12")
                    {
                        return "1,2,3,4,5,6,7,8,36,37,38";
                    }
                    if (str3 == "car168_d3456")
                    {
                        return "9,10,11,12,13,14,15,16,17,18,19,20,21,22,23";
                    }
                    if (str3 != "car168_d78910")
                    {
                        return str;
                    }
                    return "24,25,26,27,28,29,30,31,32,33,34,35";
                }
            }
            else
            {
                return "2,3,4,6,7,8,10,11,12,14,15,16,18,19,20,22,23,25,26,28,29,31,32,34,35,37,38";
            }
            return "1,5,9,13,17,21,24,27,30,33";
        }

        protected string GetCQSC_PlayIDChange(string playType)
        {
            switch (playType)
            {
                case "cqsc_lmp":
                    return "2,3,5,6,8,9,11,12,14,15,16,17,18";

                case "cqsc_d1_5":
                    return "1,4,7,10,13";

                case "cqsc_d1":
                    return "1,2,3,16,17,18,19,20,21";

                case "cqsc_d2":
                    return "4,5,6,16,17,18,19,20,21";

                case "cqsc_d3":
                    return "7,8,9,16,17,18,19,20,21";

                case "cqsc_d4":
                    return "10,11,12,16,17,18,19,20,21";

                case "cqsc_d5":
                    return "13,14,15,16,17,18,19,20,21";
            }
            return "";
        }

        public DataTable GetFgsWTTable(int lotteryID)
        {
            string str = HttpContext.Current.Session["user_name"].ToString();
            cz_userinfo_session getUserModelInfo = this.GetUserModelInfo;
            DataTable table = null;
            if (lotteryID.Equals(100))
            {
                if (getUserModelInfo.get_six_op_odds().Equals(1))
                {
                    table = this.FGS_WTChche(100);
                }
                return table;
            }
            if (!getUserModelInfo.get_kc_op_odds().Equals(1))
            {
                return table;
            }
            int? nullable = null;
            switch (lotteryID)
            {
                case 0:
                    nullable = 0;
                    break;

                case 1:
                    nullable = 1;
                    break;

                case 2:
                    nullable = 2;
                    break;

                case 3:
                    nullable = 3;
                    break;

                case 4:
                    nullable = 4;
                    break;

                case 5:
                    nullable = 5;
                    break;

                case 6:
                    nullable = 6;
                    break;

                case 7:
                    nullable = 7;
                    break;

                case 8:
                    nullable = 8;
                    break;

                case 9:
                    nullable = 9;
                    break;

                case 10:
                    nullable = 10;
                    break;

                case 11:
                    nullable = 11;
                    break;

                case 12:
                    nullable = 12;
                    break;

                case 13:
                    nullable = 13;
                    break;

                case 14:
                    nullable = 14;
                    break;

                case 15:
                    nullable = 15;
                    break;

                case 0x10:
                    nullable = 0x10;
                    break;

                case 0x11:
                    nullable = 0x11;
                    break;

                case 0x12:
                    nullable = 0x12;
                    break;

                case 0x13:
                    nullable = 0x13;
                    break;

                case 20:
                    nullable = 20;
                    break;

                case 0x15:
                    nullable = 0x15;
                    break;

                case 0x16:
                    nullable = 0x16;
                    break;
            }
            return this.FGS_WTChche(nullable);
        }

        public string getfz(string xz_str, int xz)
        {
            int[] a = new int[5];
            string[] strArray = xz_str.Split(new char[] { ',' });
            string str = "";
            string[] strArray2 = this.comb(a, strArray.Length, xz, xz).Split(new char[] { ',' });
            int num = 0;
            foreach (string str3 in strArray2)
            {
                num++;
                if (str3.Trim() != "")
                {
                    str = str + strArray[int.Parse(str3)];
                    if (num == xz)
                    {
                        num = 0;
                        str = str + "~";
                    }
                    else
                    {
                        str = str + ",";
                    }
                }
            }
            return str.Substring(0, str.Length - 1);
        }

        protected string GetHAPPYCAR_PlayIDChange(string playType)
        {
            string str = "";
            string str3 = playType;
            if (str3 == null)
            {
                return str;
            }
            if (!(str3 == "happycar_lmp"))
            {
                if (str3 != "happycar_d1_10")
                {
                    if (str3 == "happycar_d12")
                    {
                        return "1,2,3,4,5,6,7,8,36,37,38";
                    }
                    if (str3 == "happycar_d3456")
                    {
                        return "9,10,11,12,13,14,15,16,17,18,19,20,21,22,23";
                    }
                    if (str3 != "happycar_d78910")
                    {
                        return str;
                    }
                    return "24,25,26,27,28,29,30,31,32,33,34,35";
                }
            }
            else
            {
                return "2,3,4,6,7,8,10,11,12,14,15,16,18,19,20,22,23,25,26,28,29,31,32,34,35,37,38";
            }
            return "1,5,9,13,17,21,24,27,30,33";
        }

        private string GetHtml_CZCD()
        {
            StringBuilder builder = new StringBuilder();
            if (this.IsOpenLottery(100))
            {
                if (!string.IsNullOrEmpty(builder.ToString()))
                {
                    builder.Append("        ,\"L_SIX\": [");
                }
                else
                {
                    builder.Append("        \"L_SIX\": [");
                }
                builder.AppendFormat("\"特碼|index.aspx?lid={0}&playpage={1}\",", 100, "six_tma");
                builder.AppendFormat("\"正碼|index.aspx?lid={0}&playpage={1}\",", 100, "six_zm");
                builder.AppendFormat("\"正碼特|index.aspx?lid={0}&playpage={1}\",", 100, "six_zmt1");
                builder.AppendFormat("\"連碼|index.aspx?lid={0}&playpage={1}\",", 100, "six_3z2");
                builder.AppendFormat("\"不中|index.aspx?lid={0}&playpage={1}\",", 100, "six_5bz");
                builder.AppendFormat("\"正碼1-6|index.aspx?lid={0}&playpage={1}\",", 100, "six_zm1_6");
                builder.AppendFormat("\"特碼生肖|index.aspx?lid={0}&playpage={1}\",", 100, "six_tmsx");
                builder.AppendFormat("\"生肖尾數|index.aspx?lid={0}&playpage={1}\",", 100, "six_sxws");
                builder.AppendFormat("\"半波|index.aspx?lid={0}&playpage={1}\",", 100, "six_bb");
                builder.AppendFormat("\"六肖/生肖連|index.aspx?lid={0}&playpage={1}\",", 100, "six_lxsxl");
                builder.AppendFormat("\"尾數連|index.aspx?lid={0}&playpage={1}\",", 100, "six_wsl");
                builder.AppendFormat("\"龍虎/特碼攤子|index.aspx?lid={0}&playpage={1}\",", 100, "six_lhtmtz");
                builder.AppendFormat("\"七碼五行|index.aspx?lid={0}&playpage={1}\"", 100, "six_qmwx");
                builder.Append("        ]");
            }
            if (this.IsOpenLottery(0))
            {
                if (!string.IsNullOrEmpty(builder.ToString()))
                {
                    builder.Append(",\"L_KL10\": [");
                }
                else
                {
                    builder.Append("\"L_KL10\": [");
                }
                builder.AppendFormat("\"兩面盤|index.aspx?lid={0}&playpage={1}\",", 0, "kl10_lmp");
                builder.AppendFormat("\"單球1-8|index.aspx?lid={0}&playpage={1}\",", 0, "kl10_d1_8");
                builder.AppendFormat("\"第一球|index.aspx?lid={0}&playpage={1}\",", 0, "kl10_d1");
                builder.AppendFormat("\"第二球|index.aspx?lid={0}&playpage={1}\",", 0, "kl10_d2");
                builder.AppendFormat("\"第三球|index.aspx?lid={0}&playpage={1}\",", 0, "kl10_d3");
                builder.AppendFormat("\"第四球|index.aspx?lid={0}&playpage={1}\",", 0, "kl10_d4");
                builder.AppendFormat("\"第五球|index.aspx?lid={0}&playpage={1}\",", 0, "kl10_d5");
                builder.AppendFormat("\"第六球|index.aspx?lid={0}&playpage={1}\",", 0, "kl10_d6");
                builder.AppendFormat("\"第七球|index.aspx?lid={0}&playpage={1}\",", 0, "kl10_d7");
                builder.AppendFormat("\"第八球|index.aspx?lid={0}&playpage={1}\",", 0, "kl10_d8");
                builder.AppendFormat("\"總和、龍虎|index.aspx?lid={0}&playpage={1}\",", 0, "kl10_zhlh");
                builder.AppendFormat("\"連碼|index.aspx?lid={0}&playpage={1}\"", 0, "kl10_lm");
                builder.Append("]");
            }
            if (this.IsOpenLottery(1))
            {
                if (!string.IsNullOrEmpty(builder.ToString()))
                {
                    builder.Append("        ,\"L_CQSC\": [");
                }
                else
                {
                    builder.Append("        \"L_CQSC\": [");
                }
                builder.AppendFormat("\"兩面盤|index.aspx?lid={0}&playpage={1}\",", 1, "cqsc_lmp");
                builder.AppendFormat("\"單球1-5|index.aspx?lid={0}&playpage={1}\",", 1, "cqsc_d1_5");
                builder.AppendFormat("\"第一球|index.aspx?lid={0}&playpage={1}\",", 1, "cqsc_d1");
                builder.AppendFormat("\"第二球|index.aspx?lid={0}&playpage={1}\",", 1, "cqsc_d2");
                builder.AppendFormat("\"第三球|index.aspx?lid={0}&playpage={1}\",", 1, "cqsc_d3");
                builder.AppendFormat("\"第四球|index.aspx?lid={0}&playpage={1}\",", 1, "cqsc_d4");
                builder.AppendFormat("\"第五球|index.aspx?lid={0}&playpage={1}\"", 1, "cqsc_d5");
                builder.Append("        ]");
            }
            if (this.IsOpenLottery(2))
            {
                if (!string.IsNullOrEmpty(builder.ToString()))
                {
                    builder.Append("        ,\"L_PK10\": [");
                }
                else
                {
                    builder.Append("        \"L_PK10\": [");
                }
                builder.AppendFormat("\"兩面盤|index.aspx?lid={0}&playpage={1}\",", 2, "pk10_lmp");
                builder.AppendFormat("\"單球1-10|index.aspx?lid={0}&playpage={1}\",", 2, "pk10_d1_8");
                builder.AppendFormat("\"冠、亞軍 組合|index.aspx?lid={0}&playpage={1}\",", 2, "pk10_d12");
                builder.AppendFormat("\"三、四、伍、六名|index.aspx?lid={0}&playpage={1}\",", 2, "pk10_d3456");
                builder.AppendFormat("\"七、八、九、十名|index.aspx?lid={0}&playpage={1}\"", 2, "pk10_d78910");
                builder.Append("        ]");
            }
            if (this.IsOpenLottery(3))
            {
                if (!string.IsNullOrEmpty(builder.ToString()))
                {
                    builder.Append("        ,\"L_XYNC\": [");
                }
                else
                {
                    builder.Append("        \"L_XYNC\": [");
                }
                builder.AppendFormat("\"兩面盤|index.aspx?lid={0}&playpage={1}\",", 3, "xync_lmp");
                builder.AppendFormat("\"單球1-8|index.aspx?lid={0}&playpage={1}\",", 3, "xync_d1_8");
                builder.AppendFormat("\"第一球|index.aspx?lid={0}&playpage={1}\",", 3, "xync_d1");
                builder.AppendFormat("\"第二球|index.aspx?lid={0}&playpage={1}\",", 3, "xync_d2");
                builder.AppendFormat("\"第三球|index.aspx?lid={0}&playpage={1}\",", 3, "xync_d3");
                builder.AppendFormat("\"第四球|index.aspx?lid={0}&playpage={1}\",", 3, "xync_d4");
                builder.AppendFormat("\"第五球|index.aspx?lid={0}&playpage={1}\",", 3, "xync_d5");
                builder.AppendFormat("\"第六球|index.aspx?lid={0}&playpage={1}\",", 3, "xync_d6");
                builder.AppendFormat("\"第七球|index.aspx?lid={0}&playpage={1}\",", 3, "xync_d7");
                builder.AppendFormat("\"第八球|index.aspx?lid={0}&playpage={1}\",", 3, "xync_d8");
                builder.AppendFormat("\"總和、家禽野獸|index.aspx?lid={0}&playpage={1}\",", 3, "xync_jqys");
                builder.AppendFormat("\"連碼|index.aspx?lid={0}&playpage={1}\"", 3, "xync_lm");
                builder.Append("        ]");
            }
            if (this.IsOpenLottery(4))
            {
                if (!string.IsNullOrEmpty(builder.ToString()))
                {
                    builder.Append("        ,\"L_K3\": [");
                }
                else
                {
                    builder.Append("        \"L_K3\": [");
                }
                builder.AppendFormat("            \"大小骰寶|index.aspx?lid={0}&playpage={1}\"", 4, "jsk3_dxsb");
                builder.Append("        ]");
            }
            if (this.IsOpenLottery(5))
            {
                if (!string.IsNullOrEmpty(builder.ToString()))
                {
                    builder.Append("        ,\"L_KL8\": [");
                }
                else
                {
                    builder.Append("        \"L_KL8\": [");
                }
                builder.AppendFormat("            \"總和、比數、五行|index.aspx?lid={0}&playpage={1}\",", 5, "kl8_zhbswh");
                builder.AppendFormat("            \"正碼|index.aspx?lid={0}&playpage={1}\"", 5, "kl8_zm");
                builder.Append("        ]");
            }
            if (this.IsOpenLottery(6))
            {
                if (!string.IsNullOrEmpty(builder.ToString()))
                {
                    builder.Append("        ,\"L_K8SC\": [");
                }
                else
                {
                    builder.Append("        \"L_K8SC\": [");
                }
                builder.AppendFormat("\"兩面盤|index.aspx?lid={0}&playpage={1}\",", 6, "k8sc_lmp");
                builder.AppendFormat("\"單球1-5|index.aspx?lid={0}&playpage={1}\",", 6, "k8sc_d1_5");
                builder.AppendFormat("\"第一球|index.aspx?lid={0}&playpage={1}\",", 6, "k8sc_d1");
                builder.AppendFormat("\"第二球|index.aspx?lid={0}&playpage={1}\",", 6, "k8sc_d2");
                builder.AppendFormat("\"第三球|index.aspx?lid={0}&playpage={1}\",", 6, "k8sc_d3");
                builder.AppendFormat("\"第四球|index.aspx?lid={0}&playpage={1}\",", 6, "k8sc_d4");
                builder.AppendFormat("\"第五球|index.aspx?lid={0}&playpage={1}\"", 6, "k8sc_d5");
                builder.Append("        ]");
            }
            if (this.IsOpenLottery(7))
            {
                if (!string.IsNullOrEmpty(builder.ToString()))
                {
                    builder.Append("        ,\"L_PCDD\": [");
                }
                else
                {
                    builder.Append("        \"L_PCDD\": [");
                }
                builder.AppendFormat("\"兩面盤|index.aspx?lid={0}&playpage={1}\",", 7, "pcdd_lmp");
                builder.AppendFormat("\"特碼|index.aspx?lid={0}&playpage={1}\",", 7, "pcdd_tm");
                builder.AppendFormat("\"特碼包三|index.aspx?lid={0}&playpage={1}\"", 7, "pcdd_lm");
                builder.Append("        ]");
            }
            if (this.IsOpenLottery(9))
            {
                if (!string.IsNullOrEmpty(builder.ToString()))
                {
                    builder.Append("        ,\"L_XYFT5\": [");
                }
                else
                {
                    builder.Append("        \"L_XYFT5\": [");
                }
                builder.AppendFormat("\"兩面盤|index.aspx?lid={0}&playpage={1}\",", 9, "xyft5_lmp");
                builder.AppendFormat("\"單球1-10|index.aspx?lid={0}&playpage={1}\",", 9, "xyft5_d1_8");
                builder.AppendFormat("\"冠、亞軍 組合|index.aspx?lid={0}&playpage={1}\",", 9, "xyft5_d12");
                builder.AppendFormat("\"三、四、伍、六名|index.aspx?lid={0}&playpage={1}\",", 9, "xyft5_d3456");
                builder.AppendFormat("\"七、八、九、十名|index.aspx?lid={0}&playpage={1}\"", 9, "xyft5_d78910");
                builder.Append("        ]");
            }
            if (this.IsOpenLottery(8))
            {
                if (!string.IsNullOrEmpty(builder.ToString()))
                {
                    builder.Append("        ,\"L_PKBJL\": [");
                }
                else
                {
                    builder.Append("        \"L_PKBJL\": [");
                }
                builder.Append("        ]");
            }
            if (this.IsOpenLottery(10))
            {
                if (!string.IsNullOrEmpty(builder.ToString()))
                {
                    builder.Append("        ,\"L_JSCAR\": [");
                }
                else
                {
                    builder.Append("        \"L_JSCAR\": [");
                }
                builder.AppendFormat("\"兩面盤|index.aspx?lid={0}&playpage={1}\",", 10, "jscar_lmp");
                builder.AppendFormat("\"單球1-10|index.aspx?lid={0}&playpage={1}\",", 10, "jscar_d1_8");
                builder.AppendFormat("\"冠、亞軍 組合|index.aspx?lid={0}&playpage={1}\",", 10, "jscar_d12");
                builder.AppendFormat("\"三、四、伍、六名|index.aspx?lid={0}&playpage={1}\",", 10, "jscar_d3456");
                builder.AppendFormat("\"七、八、九、十名|index.aspx?lid={0}&playpage={1}\"", 10, "jscar_d78910");
                builder.Append("        ]");
            }
            if (this.IsOpenLottery(11))
            {
                if (!string.IsNullOrEmpty(builder.ToString()))
                {
                    builder.Append("        ,\"L_SPEED5\": [");
                }
                else
                {
                    builder.Append("        \"L_SPEED5\": [");
                }
                builder.AppendFormat("\"兩面盤|index.aspx?lid={0}&playpage={1}\",", 11, "speed5_lmp");
                builder.AppendFormat("\"單球1-5|index.aspx?lid={0}&playpage={1}\",", 11, "speed5_d1_5");
                builder.AppendFormat("\"第一球|index.aspx?lid={0}&playpage={1}\",", 11, "speed5_d1");
                builder.AppendFormat("\"第二球|index.aspx?lid={0}&playpage={1}\",", 11, "speed5_d2");
                builder.AppendFormat("\"第三球|index.aspx?lid={0}&playpage={1}\",", 11, "speed5_d3");
                builder.AppendFormat("\"第四球|index.aspx?lid={0}&playpage={1}\",", 11, "speed5_d4");
                builder.AppendFormat("\"第五球|index.aspx?lid={0}&playpage={1}\"", 11, "speed5_d5");
                builder.Append("        ]");
            }
            if (this.IsOpenLottery(13))
            {
                if (!string.IsNullOrEmpty(builder.ToString()))
                {
                    builder.Append("        ,\"L_JSCQSC\": [");
                }
                else
                {
                    builder.Append("        \"L_JSCQSC\": [");
                }
                builder.AppendFormat("\"兩面盤|index.aspx?lid={0}&playpage={1}\",", 13, "jscqsc_lmp");
                builder.AppendFormat("\"單球1-5|index.aspx?lid={0}&playpage={1}\",", 13, "jscqsc_d1_5");
                builder.AppendFormat("\"第一球|index.aspx?lid={0}&playpage={1}\",", 13, "jscqsc_d1");
                builder.AppendFormat("\"第二球|index.aspx?lid={0}&playpage={1}\",", 13, "jscqsc_d2");
                builder.AppendFormat("\"第三球|index.aspx?lid={0}&playpage={1}\",", 13, "jscqsc_d3");
                builder.AppendFormat("\"第四球|index.aspx?lid={0}&playpage={1}\",", 13, "jscqsc_d4");
                builder.AppendFormat("\"第五球|index.aspx?lid={0}&playpage={1}\"", 13, "jscqsc_d5");
                builder.Append("        ]");
            }
            if (this.IsOpenLottery(12))
            {
                if (!string.IsNullOrEmpty(builder.ToString()))
                {
                    builder.Append("        ,\"L_JSPK10\": [");
                }
                else
                {
                    builder.Append("        \"L_JSPK10\": [");
                }
                builder.AppendFormat("\"兩面盤|index.aspx?lid={0}&playpage={1}\",", 12, "jspk10_lmp");
                builder.AppendFormat("\"單球1-10|index.aspx?lid={0}&playpage={1}\",", 12, "jspk10_d1_8");
                builder.AppendFormat("\"冠、亞軍 組合|index.aspx?lid={0}&playpage={1}\",", 12, "jspk10_d12");
                builder.AppendFormat("\"三、四、伍、六名|index.aspx?lid={0}&playpage={1}\",", 12, "jspk10_d3456");
                builder.AppendFormat("\"七、八、九、十名|index.aspx?lid={0}&playpage={1}\"", 12, "jspk10_d78910");
                builder.Append("        ]");
            }
            if (this.IsOpenLottery(14))
            {
                if (!string.IsNullOrEmpty(builder.ToString()))
                {
                    builder.Append(",\"L_JSSFC\": [");
                }
                else
                {
                    builder.Append("\"L_JSSFC\": [");
                }
                builder.AppendFormat("\"兩面盤|index.aspx?lid={0}&playpage={1}\",", 14, "jssfc_lmp");
                builder.AppendFormat("\"單球1-8|index.aspx?lid={0}&playpage={1}\",", 14, "jssfc_d1_8");
                builder.AppendFormat("\"第一球|index.aspx?lid={0}&playpage={1}\",", 14, "jssfc_d1");
                builder.AppendFormat("\"第二球|index.aspx?lid={0}&playpage={1}\",", 14, "jssfc_d2");
                builder.AppendFormat("\"第三球|index.aspx?lid={0}&playpage={1}\",", 14, "jssfc_d3");
                builder.AppendFormat("\"第四球|index.aspx?lid={0}&playpage={1}\",", 14, "jssfc_d4");
                builder.AppendFormat("\"第五球|index.aspx?lid={0}&playpage={1}\",", 14, "jssfc_d5");
                builder.AppendFormat("\"第六球|index.aspx?lid={0}&playpage={1}\",", 14, "jssfc_d6");
                builder.AppendFormat("\"第七球|index.aspx?lid={0}&playpage={1}\",", 14, "jssfc_d7");
                builder.AppendFormat("\"第八球|index.aspx?lid={0}&playpage={1}\",", 14, "jssfc_d8");
                builder.AppendFormat("\"總和、龍虎|index.aspx?lid={0}&playpage={1}\",", 14, "jssfc_zhlh");
                builder.AppendFormat("\"連碼|index.aspx?lid={0}&playpage={1}\"", 14, "jssfc_lm");
                builder.Append("]");
            }
            if (this.IsOpenLottery(15))
            {
                if (!string.IsNullOrEmpty(builder.ToString()))
                {
                    builder.Append("        ,\"L_JSFT2\": [");
                }
                else
                {
                    builder.Append("        \"L_JSFT2\": [");
                }
                builder.AppendFormat("\"兩面盤|index.aspx?lid={0}&playpage={1}\",", 15, "jsft2_lmp");
                builder.AppendFormat("\"單球1-10|index.aspx?lid={0}&playpage={1}\",", 15, "jsft2_d1_8");
                builder.AppendFormat("\"冠、亞軍 組合|index.aspx?lid={0}&playpage={1}\",", 15, "jsft2_d12");
                builder.AppendFormat("\"三、四、伍、六名|index.aspx?lid={0}&playpage={1}\",", 15, "jsft2_d3456");
                builder.AppendFormat("\"七、八、九、十名|index.aspx?lid={0}&playpage={1}\"", 15, "jsft2_d78910");
                builder.Append("        ]");
            }
            if (this.IsOpenLottery(0x10))
            {
                if (!string.IsNullOrEmpty(builder.ToString()))
                {
                    builder.Append("        ,\"L_CAR168\": [");
                }
                else
                {
                    builder.Append("        \"L_CAR168\": [");
                }
                builder.AppendFormat("\"兩面盤|index.aspx?lid={0}&playpage={1}\",", 0x10, "car168_lmp");
                builder.AppendFormat("\"單球1-10|index.aspx?lid={0}&playpage={1}\",", 0x10, "car168_d1_8");
                builder.AppendFormat("\"冠、亞軍 組合|index.aspx?lid={0}&playpage={1}\",", 0x10, "car168_d12");
                builder.AppendFormat("\"三、四、伍、六名|index.aspx?lid={0}&playpage={1}\",", 0x10, "car168_d3456");
                builder.AppendFormat("\"七、八、九、十名|index.aspx?lid={0}&playpage={1}\"", 0x10, "car168_d78910");
                builder.Append("        ]");
            }
            if (this.IsOpenLottery(0x11))
            {
                if (!string.IsNullOrEmpty(builder.ToString()))
                {
                    builder.Append("        ,\"L_SSC168\": [");
                }
                else
                {
                    builder.Append("        \"L_SSC168\": [");
                }
                builder.AppendFormat("\"兩面盤|index.aspx?lid={0}&playpage={1}\",", 0x11, "ssc168_lmp");
                builder.AppendFormat("\"單球1-5|index.aspx?lid={0}&playpage={1}\",", 0x11, "ssc168_d1_5");
                builder.AppendFormat("\"第一球|index.aspx?lid={0}&playpage={1}\",", 0x11, "ssc168_d1");
                builder.AppendFormat("\"第二球|index.aspx?lid={0}&playpage={1}\",", 0x11, "ssc168_d2");
                builder.AppendFormat("\"第三球|index.aspx?lid={0}&playpage={1}\",", 0x11, "ssc168_d3");
                builder.AppendFormat("\"第四球|index.aspx?lid={0}&playpage={1}\",", 0x11, "ssc168_d4");
                builder.AppendFormat("\"第五球|index.aspx?lid={0}&playpage={1}\"", 0x11, "ssc168_d5");
                builder.Append("        ]");
            }
            if (this.IsOpenLottery(0x12))
            {
                if (!string.IsNullOrEmpty(builder.ToString()))
                {
                    builder.Append("        ,\"L_VRCAR\": [");
                }
                else
                {
                    builder.Append("        \"L_VRCAR\": [");
                }
                builder.AppendFormat("\"兩面盤|index.aspx?lid={0}&playpage={1}\",", 0x12, "vrcar_lmp");
                builder.AppendFormat("\"單球1-10|index.aspx?lid={0}&playpage={1}\",", 0x12, "vrcar_d1_8");
                builder.AppendFormat("\"冠、亞軍 組合|index.aspx?lid={0}&playpage={1}\",", 0x12, "vrcar_d12");
                builder.AppendFormat("\"三、四、伍、六名|index.aspx?lid={0}&playpage={1}\",", 0x12, "vrcar_d3456");
                builder.AppendFormat("\"七、八、九、十名|index.aspx?lid={0}&playpage={1}\"", 0x12, "vrcar_d78910");
                builder.Append("        ]");
            }
            if (this.IsOpenLottery(0x13))
            {
                if (!string.IsNullOrEmpty(builder.ToString()))
                {
                    builder.Append("        ,\"L_VRSSC\": [");
                }
                else
                {
                    builder.Append("        \"L_VRSSC\": [");
                }
                builder.AppendFormat("\"兩面盤|index.aspx?lid={0}&playpage={1}\",", 0x13, "vrssc_lmp");
                builder.AppendFormat("\"單球1-5|index.aspx?lid={0}&playpage={1}\",", 0x13, "vrssc_d1_5");
                builder.AppendFormat("\"第一球|index.aspx?lid={0}&playpage={1}\",", 0x13, "vrssc_d1");
                builder.AppendFormat("\"第二球|index.aspx?lid={0}&playpage={1}\",", 0x13, "vrssc_d2");
                builder.AppendFormat("\"第三球|index.aspx?lid={0}&playpage={1}\",", 0x13, "vrssc_d3");
                builder.AppendFormat("\"第四球|index.aspx?lid={0}&playpage={1}\",", 0x13, "vrssc_d4");
                builder.AppendFormat("\"第五球|index.aspx?lid={0}&playpage={1}\"", 0x13, "vrssc_d5");
                builder.Append("        ]");
            }
            if (this.IsOpenLottery(20))
            {
                if (!string.IsNullOrEmpty(builder.ToString()))
                {
                    builder.Append("        ,\"L_XYFTOA\": [");
                }
                else
                {
                    builder.Append("        \"L_XYFTOA\": [");
                }
                builder.AppendFormat("\"兩面盤|index.aspx?lid={0}&playpage={1}\",", 20, "xyftoa_lmp");
                builder.AppendFormat("\"單球1-10|index.aspx?lid={0}&playpage={1}\",", 20, "xyftoa_d1_8");
                builder.AppendFormat("\"冠、亞軍 組合|index.aspx?lid={0}&playpage={1}\",", 20, "xyftoa_d12");
                builder.AppendFormat("\"三、四、伍、六名|index.aspx?lid={0}&playpage={1}\",", 20, "xyftoa_d3456");
                builder.AppendFormat("\"七、八、九、十名|index.aspx?lid={0}&playpage={1}\"", 20, "xyftoa_d78910");
                builder.Append("        ]");
            }
            if (this.IsOpenLottery(0x15))
            {
                if (!string.IsNullOrEmpty(builder.ToString()))
                {
                    builder.Append("        ,\"L_XYFTSG\": [");
                }
                else
                {
                    builder.Append("        \"L_XYFTSG\": [");
                }
                builder.AppendFormat("\"兩面盤|index.aspx?lid={0}&playpage={1}\",", 0x15, "xyftsg_lmp");
                builder.AppendFormat("\"單球1-10|index.aspx?lid={0}&playpage={1}\",", 0x15, "xyftsg_d1_8");
                builder.AppendFormat("\"冠、亞軍 組合|index.aspx?lid={0}&playpage={1}\",", 0x15, "xyftsg_d12");
                builder.AppendFormat("\"三、四、伍、六名|index.aspx?lid={0}&playpage={1}\",", 0x15, "xyftsg_d3456");
                builder.AppendFormat("\"七、八、九、十名|index.aspx?lid={0}&playpage={1}\"", 0x15, "xyftsg_d78910");
                builder.Append("        ]");
            }
            if (this.IsOpenLottery(0x16))
            {
                if (!string.IsNullOrEmpty(builder.ToString()))
                {
                    builder.Append("        ,\"L_HAPPYCAR\": [");
                }
                else
                {
                    builder.Append("        \"L_HAPPYCAR\": [");
                }
                builder.AppendFormat("\"兩面盤|index.aspx?lid={0}&playpage={1}\",", 0x16, "happycar_lmp");
                builder.AppendFormat("\"單球1-10|index.aspx?lid={0}&playpage={1}\",", 0x16, "happycar_d1_8");
                builder.AppendFormat("\"冠、亞軍 組合|index.aspx?lid={0}&playpage={1}\",", 0x16, "happycar_d12");
                builder.AppendFormat("\"三、四、伍、六名|index.aspx?lid={0}&playpage={1}\",", 0x16, "happycar_d3456");
                builder.AppendFormat("\"七、八、九、十名|index.aspx?lid={0}&playpage={1}\"", 0x16, "happycar_d78910");
                builder.Append("        ]");
            }
            return builder.ToString();
        }

        protected string GetIframeHeader(string id, string skin)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat(PageBase.GetHeaderByCache(id, "HtmlHeaderHint"), "title", skin);
            return builder.ToString().Replace("ЁJSVersionЁ", base.get_GetJSVersion());
        }

        protected string GetIframeHeader(string id, string skin, string title)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat(PageBase.GetHeaderByCache(id, "HtmlHeaderHint"), title, skin);
            return builder.ToString().Replace("ЁJSVersionЁ", base.get_GetJSVersion());
        }

        protected string GetIframeHeaderLotteryHttp(string lottery_id)
        {
            string str = "lotteryhttp";
            StringBuilder builder = new StringBuilder();
            builder.Append(PageBase.GetHeaderByCache(str, "HtmlHeaderHint"));
            string[] strArray = builder.ToString().Split(new char[] { 'Ё' });
            for (int i = 0; i < strArray.Length; i++)
            {
                string str3 = strArray[i].Split(new char[] { ',' })[0].Trim();
                string str4 = strArray[i].Split(new char[] { ',' })[1].Trim();
                if (lottery_id.ToLower().Equals(str3.ToLower()))
                {
                    int num2 = 5;
                    if (!str3.Equals(num2.ToString()))
                    {
                        num2 = 6;
                    }
                    if (!str3.Equals(num2.ToString()) && !str3.Equals((num2 = 7).ToString()))
                    {
                        return str4;
                    }
                    if (CallBLL.cz_lottery_bll.GetList().Tables[0].Select(string.Format(" id={0} ", str3))[0]["night_flag"].ToString().Equals("1"))
                    {
                        DateTime now = DateTime.Now;
                        if ((now < Convert.ToDateTime(now.ToString("yyyy-MM-dd") + " 06:00:00")) && (now > Convert.ToDateTime(now.ToString("yyyy-MM-dd") + " 00:00:00")))
                        {
                            return str4.Split(new char[] { '|' })[1];
                        }
                        return str4.Split(new char[] { '|' })[0];
                    }
                    return str4.Split(new char[] { '|' })[0];
                }
            }
            return "";
        }

        protected string GetIframeHeaderLotteryHttp(string lottery_id, int index)
        {
            string str = "lotteryhttp";
            string str2 = "";
            StringBuilder builder = new StringBuilder();
            builder.Append(PageBase.GetHeaderByCache(str, "HtmlHeaderHint"));
            string[] strArray = builder.ToString().Split(new char[] { 'Ё' });
            for (int i = 0; i < strArray.Length; i++)
            {
                string str3 = strArray[i].Split(new char[] { ',' })[0].Trim();
                string str4 = strArray[i].Split(new char[] { ',' })[1].Trim();
                if (lottery_id.ToLower().Equals(str3.ToLower()))
                {
                    str2 = str4.Split(new char[] { '|' })[index];
                }
            }
            return str2;
        }

        protected string GetJSCAR_PlayIDChange(string playType)
        {
            string str = "";
            string str3 = playType;
            if (str3 == null)
            {
                return str;
            }
            if (!(str3 == "jscar_lmp"))
            {
                if (str3 != "jscar_d1_10")
                {
                    if (str3 == "jscar_d12")
                    {
                        return "1,2,3,4,5,6,7,8,36,37,38";
                    }
                    if (str3 == "jscar_d3456")
                    {
                        return "9,10,11,12,13,14,15,16,17,18,19,20,21,22,23";
                    }
                    if (str3 != "jscar_d78910")
                    {
                        return str;
                    }
                    return "24,25,26,27,28,29,30,31,32,33,34,35";
                }
            }
            else
            {
                return "2,3,4,6,7,8,10,11,12,14,15,16,18,19,20,22,23,25,26,28,29,31,32,34,35,37,38";
            }
            return "1,5,9,13,17,21,24,27,30,33";
        }

        protected string GetJSCQSC_PlayIDChange(string playType)
        {
            switch (playType)
            {
                case "jscqsc_lmp":
                    return "2,3,5,6,8,9,11,12,14,15,16,17,18";

                case "jscqsc_d1_5":
                    return "1,4,7,10,13";

                case "jscqsc_d1":
                    return "1,2,3,16,17,18,19,20,21";

                case "jscqsc_d2":
                    return "4,5,6,16,17,18,19,20,21";

                case "jscqsc_d3":
                    return "7,8,9,16,17,18,19,20,21";

                case "jscqsc_d4":
                    return "10,11,12,16,17,18,19,20,21";

                case "jscqsc_d5":
                    return "13,14,15,16,17,18,19,20,21";
            }
            return "";
        }

        protected string GetJSFT2_PlayIDChange(string playType)
        {
            string str = "";
            string str3 = playType;
            if (str3 == null)
            {
                return str;
            }
            if (!(str3 == "jsft2_lmp"))
            {
                if (str3 != "jsft2_d1_10")
                {
                    if (str3 == "jsft2_d12")
                    {
                        return "1,2,3,4,5,6,7,8,36,37,38";
                    }
                    if (str3 == "jsft2_d3456")
                    {
                        return "9,10,11,12,13,14,15,16,17,18,19,20,21,22,23";
                    }
                    if (str3 != "jsft2_d78910")
                    {
                        return str;
                    }
                    return "24,25,26,27,28,29,30,31,32,33,34,35";
                }
            }
            else
            {
                return "2,3,4,6,7,8,10,11,12,14,15,16,18,19,20,22,23,25,26,28,29,31,32,34,35,37,38";
            }
            return "1,5,9,13,17,21,24,27,30,33";
        }

        protected string GetJSK3_PlayIDChange(string playType)
        {
            string str = "";
            string str3 = playType;
            if ((str3 != null) && (str3 == "jsk3_dxsb"))
            {
                str = "58,59,60,61,62,63,64";
            }
            return str;
        }

        protected string GetJSPK10_PlayIDChange(string playType)
        {
            string str = "";
            string str3 = playType;
            if (str3 == null)
            {
                return str;
            }
            if (!(str3 == "jspk10_lmp"))
            {
                if (str3 != "jspk10_d1_10")
                {
                    if (str3 == "jspk10_d12")
                    {
                        return "1,2,3,4,5,6,7,8,36,37,38";
                    }
                    if (str3 == "jspk10_d3456")
                    {
                        return "9,10,11,12,13,14,15,16,17,18,19,20,21,22,23";
                    }
                    if (str3 != "jspk10_d78910")
                    {
                        return str;
                    }
                    return "24,25,26,27,28,29,30,31,32,33,34,35";
                }
            }
            else
            {
                return "2,3,4,6,7,8,10,11,12,14,15,16,18,19,20,22,23,25,26,28,29,31,32,34,35,37,38";
            }
            return "1,5,9,13,17,21,24,27,30,33";
        }

        protected string GetJSSFC_PlayIDChange(string playType)
        {
            switch (playType)
            {
                case "jssfc_lmp":
                    return "100,102,103,104,105,107,108,109,11,110,112,113,114,115,117,118,119,12,120,13,80,82,83,84,85,87,88,89,90,92,93,94,95,97,98,99";

                case "jssfc_d1_8":
                    return "81,86,91,96,101,106,111,116";

                case "jssfc_d1":
                    return "121,122,81,82,83,84,85";

                case "jssfc_d2":
                    return "86,87,88,89,90,123,124";

                case "jssfc_d3":
                    return "91,92,93,94,95,125,126";

                case "jssfc_d4":
                    return "96,97,98,99,100,127,128";

                case "jssfc_d5":
                    return "101,102,103,104,105,129,130";

                case "jssfc_d6":
                    return "106,107,108,109,110,131,132";

                case "jssfc_d7":
                    return "111,112,113,114,115,133,134";

                case "jssfc_d8":
                    return "116,117,118,119,120,135,136";

                case "jssfc_zhlh":
                    return "11,12,13,80";

                case "jssfc_lm":
                    return "72,73,74,75,76,77,78,79";
            }
            return "";
        }

        protected string GetK8SC_PlayIDChange(string playType)
        {
            switch (playType)
            {
                case "k8sc_lmp":
                    return "2,3,5,6,8,9,11,12,14,15,16,17,18";

                case "k8sc_d1_5":
                    return "1,4,7,10,13";

                case "k8sc_d1":
                    return "1,2,3,16,17,18,19,20,21";

                case "k8sc_d2":
                    return "4,5,6,16,17,18,19,20,21";

                case "k8sc_d3":
                    return "7,8,9,16,17,18,19,20,21";

                case "k8sc_d4":
                    return "10,11,12,16,17,18,19,20,21";

                case "k8sc_d5":
                    return "13,14,15,16,17,18,19,20,21";
            }
            return "";
        }

        protected DataTable GetKCOpenBall(int lotteryID)
        {
            DataTable table = null;
            if (CacheHelper.GetCache("kc_openball_FileCacheKey" + lotteryID.ToString()) != null)
            {
                return (CacheHelper.GetCache("kc_openball_FileCacheKey" + lotteryID.ToString()) as DataTable);
            }
            switch (lotteryID)
            {
                case 0:
                    table = CallBLL.cz_phase_kl10_bll.GetCurrentPhase().Tables[0];
                    break;

                case 1:
                    table = CallBLL.cz_phase_cqsc_bll.GetCurrentPhase().Tables[0];
                    break;

                case 2:
                    table = CallBLL.cz_phase_pk10_bll.GetCurrentPhase().Tables[0];
                    break;

                case 3:
                    table = CallBLL.cz_phase_xync_bll.GetCurrentPhase().Tables[0];
                    break;

                case 4:
                    table = CallBLL.cz_phase_jsk3_bll.GetCurrentPhase().Tables[0];
                    break;

                case 5:
                    table = CallBLL.cz_phase_kl8_bll.GetCurrentPhase().Tables[0];
                    break;

                case 6:
                    table = CallBLL.cz_phase_k8sc_bll.GetCurrentPhase().Tables[0];
                    break;

                case 7:
                    table = CallBLL.cz_phase_pcdd_bll.GetCurrentPhase().Tables[0];
                    break;

                case 9:
                    table = CallBLL.cz_phase_xyft5_bll.GetCurrentPhase().Tables[0];
                    break;

                case 10:
                    table = CallBLL.cz_phase_jscar_bll.GetCurrentPhase().Tables[0];
                    break;

                case 11:
                    table = CallBLL.cz_phase_speed5_bll.GetCurrentPhase().Tables[0];
                    break;

                case 12:
                    table = CallBLL.cz_phase_jspk10_bll.GetCurrentPhase().Tables[0];
                    break;

                case 13:
                    table = CallBLL.cz_phase_jscqsc_bll.GetCurrentPhase().Tables[0];
                    break;

                case 14:
                    table = CallBLL.cz_phase_jssfc_bll.GetCurrentPhase().Tables[0];
                    break;

                case 15:
                    table = CallBLL.cz_phase_jsft2_bll.GetCurrentPhase().Tables[0];
                    break;

                case 0x10:
                    table = CallBLL.cz_phase_car168_bll.GetCurrentPhase().Tables[0];
                    break;

                case 0x11:
                    table = CallBLL.cz_phase_ssc168_bll.GetCurrentPhase().Tables[0];
                    break;

                case 0x12:
                    table = CallBLL.cz_phase_vrcar_bll.GetCurrentPhase().Tables[0];
                    break;

                case 0x13:
                    table = CallBLL.cz_phase_vrssc_bll.GetCurrentPhase().Tables[0];
                    break;

                case 20:
                    table = CallBLL.cz_phase_xyftoa_bll.GetCurrentPhase().Tables[0];
                    break;

                case 0x15:
                    table = CallBLL.cz_phase_xyftsg_bll.GetCurrentPhase().Tables[0];
                    break;

                case 0x16:
                    table = CallBLL.cz_phase_happycar_bll.GetCurrentPhase().Tables[0];
                    break;
            }
            if (table.Rows.Count > 0)
            {
                CacheHelper.SetPublicFileCacheDependency("kc_openball_FileCacheKey" + lotteryID.ToString(), table, PageBase.GetPublicForderPath(base.get_KC_BalanceFileName()));
            }
            return table;
        }

        protected DataTable GetKCOpenBall(int lotteryID, string numTable)
        {
            DataTable newOpenPhaseByNumTable = null;
            if (CacheHelper.GetCache("kc_openball_FileCacheKey" + lotteryID.ToString() + numTable) == null)
            {
                if (lotteryID == 8)
                {
                    newOpenPhaseByNumTable = CallBLL.cz_phase_pkbjl_bll.GetNewOpenPhaseByNumTable(numTable);
                }
                if ((newOpenPhaseByNumTable != null) && (newOpenPhaseByNumTable.Rows.Count > 0))
                {
                    CacheHelper.SetPublicFileCacheDependency("kc_openball_FileCacheKey" + lotteryID.ToString() + numTable, newOpenPhaseByNumTable, PageBase.GetPublicForderPath(base.get_KC_BalanceFileName()));
                }
                return newOpenPhaseByNumTable;
            }
            return (CacheHelper.GetCache("kc_openball_FileCacheKey" + lotteryID.ToString() + numTable) as DataTable);
        }

        public string GetKCProfit()
        {
            string str = HttpContext.Current.Session["user_name"].ToString();
            cz_userinfo_session getUserModelInfo = this.GetUserModelInfo;
            string str2 = getUserModelInfo.get_u_type();
            double num = 0.0;
            if ((CacheHelper.GetCache("balance_kc_FileCacheKey" + this.Session["user_name"].ToString()) == null) || (HttpContext.Current.Session["balance_kc_FileCacheKey" + this.Session["user_name"].ToString()] == null))
            {
                num = CallBLL.cz_bet_kc_bll.User_Profit(str, getUserModelInfo.get_su_type());
                this.Session["balance_kc_FileCacheKey" + this.Session["user_name"].ToString()] = num;
                CacheHelper.SetPublicFileCache("balance_kc_FileCacheKey" + this.Session["user_name"].ToString(), num, PageBase.GetPublicForderPath(base.get_KC_BalanceFileName()));
            }
            else
            {
                num = double.Parse(this.Session["balance_kc_FileCacheKey" + this.Session["user_name"].ToString()].ToString());
            }
            return Utils.GetKeepDecimalNumber(1, num.ToString());
        }

        protected string GetKL10_PlayIDChange(string playType)
        {
            switch (playType)
            {
                case "kl10_lmp":
                    return "100,102,103,104,105,107,108,109,11,110,112,113,114,115,117,118,119,12,120,13,80,82,83,84,85,87,88,89,90,92,93,94,95,97,98,99";

                case "kl10_d1_8":
                    return "81,86,91,96,101,106,111,116";

                case "kl10_d1":
                    return "121,122,81,82,83,84,85";

                case "kl10_d2":
                    return "86,87,88,89,90,123,124";

                case "kl10_d3":
                    return "91,92,93,94,95,125,126";

                case "kl10_d4":
                    return "96,97,98,99,100,127,128";

                case "kl10_d5":
                    return "101,102,103,104,105,129,130";

                case "kl10_d6":
                    return "106,107,108,109,110,131,132";

                case "kl10_d7":
                    return "111,112,113,114,115,133,134";

                case "kl10_d8":
                    return "116,117,118,119,120,135,136";

                case "kl10_zhlh":
                    return "11,12,13,80";

                case "kl10_lm":
                    return "72,73,74,75,76,77,78,79";
            }
            return "";
        }

        protected string GetKL8_PlayIDChange(string playType)
        {
            string str = "";
            string str3 = playType;
            if (str3 == null)
            {
                return str;
            }
            if (!(str3 == "kl8_zhbswh"))
            {
                if (str3 != "kl8_zm")
                {
                    return str;
                }
            }
            else
            {
                return "66,67,68,69,70,71,72";
            }
            return "65";
        }

        public Dictionary<string, string> GetLmGroup()
        {
            Dictionary<string, string> cache = CacheHelper.GetCache("LmGroupCount_Cache") as Dictionary<string, string>;
            if (cache == null)
            {
                string str = this.LmGroup();
                cache = CacheHelper.GetCache("LmGroupCount_Cache") as Dictionary<string, string>;
            }
            return cache;
        }

        public string getlongnum(string ss_str)
        {
            string[] strArray = ss_str.Split(new char[] { ',' });
            string str = "";
            foreach (string str2 in strArray)
            {
                str = str + this.getnum(str2) + ",";
            }
            return str.Substring(0, str.Length - 1);
        }

        protected string GetLotteryBeginTimeStr(string lottery_type)
        {
            string str = "";
            DataRow[] rowArray = this.GetLotteryList().Select(string.Format(" id={0} ", lottery_type));
            if (rowArray.Length > 0)
            {
                str = rowArray[0]["begintime"].ToString();
            }
            return str;
        }

        protected string GetLotteryCrossdayStr(string lottery_type)
        {
            string str = "";
            DataRow[] rowArray = this.GetLotteryList().Select(string.Format(" id={0} ", lottery_type));
            if (rowArray.Length <= 0)
            {
                return str;
            }
            if (rowArray[0]["crossday"].ToString().Equals("1"))
            {
                return "淩晨";
            }
            return "晚上";
        }

        protected string GetLotteryEndTimeStr(string lottery_type)
        {
            string str = "";
            DataRow[] rowArray = this.GetLotteryList().Select(string.Format(" id={0} ", lottery_type));
            if (rowArray.Length > 0)
            {
                str = rowArray[0]["endtime"].ToString();
            }
            return str;
        }

        public DataTable GetLotteryList()
        {
            if (CacheHelper.GetCache("cz_lottery_FileCacheKey") != null)
            {
                return (CacheHelper.GetCache("cz_lottery_CacheKey") as DataTable);
            }
            DataTable table = CallBLL.cz_lottery_bll.GetList().Tables[0];
            CacheHelper.SetCache("cz_lottery_CacheKey", table);
            CacheHelper.SetPublicFileCache("cz_lottery_FileCacheKey", table, PageBase.GetPublicForderPath(base.get_LotteryCachesFileName()));
            return table;
        }

        protected string GetLotteryLogoImgHtml(string lottery_id)
        {
            string str = "<img id=\"game_logo\" src=\"\" alt=\"\">";
            string iframeHeaderLotteryHttp = this.GetIframeHeaderLotteryHttp(lottery_id);
            if (string.IsNullOrEmpty(iframeHeaderLotteryHttp))
            {
                return str;
            }
            int num = 100;
            if (lottery_id.Equals(num.ToString()))
            {
                return string.Format("<a href=\"javascript:;\" id=\"kjBtn2\" title=\"開獎日期表\"><img id=\"game_logo\" src=\"\" alt=\"\"></a>", new object[0]);
            }
            return string.Format("<a href=\"{0}\" target=\"_blank\"><img id=\"game_logo\" src=\"\" alt=\"{1} 官網\" title=\"{2} 官網\"></a>", iframeHeaderLotteryHttp, base.GetGameNameByID(lottery_id), base.GetGameNameByID(lottery_id));
        }

        protected string GetLotteryLogoImgHtml(string lottery_id, string flag)
        {
            string str = "<img id=\"game_logo\" src=\"\" alt=\"\">";
            string iframeHeaderLotteryHttp = this.GetIframeHeaderLotteryHttp(lottery_id);
            string str3 = "/" + base.GetGameFolderByID(lottery_id) + "/images/logo.png";
            if (string.IsNullOrEmpty(iframeHeaderLotteryHttp))
            {
                return str;
            }
            int num = 100;
            if (lottery_id.Equals(num.ToString()))
            {
                return string.Format("<a href=\"javascript:;\" id=\"kjBtn\"><img id=\"game_logo\" src=\"{0}\" alt=\"{1} 開獎日期表\" title=\"{2} 開獎日期表\"></a>", str3, base.GetGameNameByID(lottery_id), base.GetGameNameByID(lottery_id));
            }
            return string.Format("<a href=\"{0}\" target=\"_blank\"><img id=\"game_logo\" src=\"{1}\" alt=\"{2} 官網\" title=\"{3} 官網\"></a>", new object[] { iframeHeaderLotteryHttp, str3, base.GetGameNameByID(lottery_id), base.GetGameNameByID(lottery_id) });
        }

        public Dictionary<string, object> GetLotteryMenuCfg(DataTable lotteryDT)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
            if (lotteryDT != null)
            {
                foreach (DataRow row in lotteryDT.Rows)
                {
                    string s = row["id"].ToString();
                    string str2 = row["dir_name"].ToString();
                    string gameNameByID = base.GetGameNameByID(s);
                    string key = "play_" + s;
                    dictionary.Add("name", gameNameByID);
                    dictionary.Add("url", str2);
                    dictionary.Add("page", base.GetPlayMenuCfg(int.Parse(s)));
                    dictionary2.Add(key, new Dictionary<string, object>(dictionary));
                    dictionary.Clear();
                }
                return dictionary2;
            }
            return null;
        }

        protected string GetNav()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("{");
            builder.Append("    \"彩種菜單\": {");
            builder.Append(this.GetHtml_CZCD());
            builder.Append("    }");
            builder.Append("}");
            return builder.ToString();
        }

        public string getnum(string ss_num)
        {
            int num = 0;
            num = int.Parse(ss_num);
            if (num < 10)
            {
                return ("0" + num.ToString());
            }
            return num.ToString();
        }

        public void GetOdds_KC(int lotteryID, string odds_id, ref string pl)
        {
            string str = HttpContext.Current.Session["user_name"].ToString();
            cz_userinfo_session getUserModelInfo = this.GetUserModelInfo;
            string str2 = getUserModelInfo.get_kc_session().get_fgsname();
            double num = 0.0;
            if (getUserModelInfo.get_kc_op_odds().Equals(1))
            {
                num = double.Parse(this.FGS_WTChche(new int?(lotteryID)).Select(string.Format(" u_name='{0}' and odds_id={1} ", str2, odds_id)).FirstOrDefault<DataRow>()["wt_value"].ToString());
            }
            pl = (double.Parse(pl) + num).ToString();
        }

        public void GetOdds_SIX(string odds_id, string current_odds, string diff, ref string pl)
        {
            double num = 0.0;
            double num2 = 0.0;
            string str = HttpContext.Current.Session["user_name"].ToString();
            cz_userinfo_session getUserModelInfo = this.GetUserModelInfo;
            if (getUserModelInfo.get_six_op_odds().Equals(1))
            {
                string str2 = getUserModelInfo.get_six_session().get_fgsname();
                DataRow row = this.GetFgsWTTable(100).Select(string.Format(" u_name='{0}' and odds_id={1} ", str2, odds_id)).FirstOrDefault<DataRow>();
                num = double.Parse(row["wt_value"].ToString());
                num2 = double.Parse(row["wt_value2"].ToString());
            }
            string[] strArray = current_odds.Trim().Split(new char[] { ',' });
            string[] strArray2 = diff.Trim().Split(new char[] { ',' });
            if (strArray.Length > 1)
            {
                double num3;
                if (strArray2.Length > 1)
                {
                    num3 = (double.Parse(strArray[0]) + double.Parse(strArray2[0])) + num;
                    num3 = (double.Parse(strArray[1]) + double.Parse(strArray2[1])) + num2;
                    pl = num3.ToString() + "," + num3.ToString();
                }
                else
                {
                    num3 = (double.Parse(strArray[0]) + double.Parse(strArray2[0])) + num;
                    num3 = (double.Parse(strArray[1]) + double.Parse(strArray2[0])) + num2;
                    pl = num3.ToString() + "," + num3.ToString();
                }
            }
            else
            {
                pl = ((double.Parse(current_odds) + double.Parse(diff)) + num).ToString();
            }
        }

        public Dictionary<string, List<string>> GetPAILU(string numTable)
        {
            if (CacheHelper.GetCache("balance_kc_FileCacheKeypkbjl_openball_pailu_dictionaryKey" + numTable) != null)
            {
                return (CacheHelper.GetCache("balance_kc_FileCacheKeypkbjl_openball_pailu_dictionaryKey" + numTable) as Dictionary<string, List<string>>);
            }
            Dictionary<string, List<string>> pAILU = CallBLL.cz_phase_pkbjl_bll.GetPAILU(numTable);
            CacheHelper.SetCache("balance_kc_FileCacheKeypkbjl_openball_pailu_dictionaryKey" + numTable, pAILU);
            CacheHelper.SetPublicFileCache("balance_kc_FileCacheKeypkbjl_openball_pailu_dictionaryKey" + numTable, pAILU, PageBase.GetPublicForderPath(base.get_KC_BalanceFileName()));
            return pAILU;
        }

        protected string GetPCDD_PlayIDChange(string playType)
        {
            string str = "";
            string str3 = playType;
            if (str3 == null)
            {
                return str;
            }
            if (!(str3 == "pcdd_lmp"))
            {
                if (str3 != "pcdd_tm")
                {
                    if (str3 != "pcdd_lm")
                    {
                        return str;
                    }
                    return "71014";
                }
            }
            else
            {
                return "71002,71003,71004,71005,71006,71007,71008,71009,71010,71011,71012,71013";
            }
            return "71001";
        }

        protected string GetPK10_PlayIDChange(string playType)
        {
            string str = "";
            string str3 = playType;
            if (str3 == null)
            {
                return str;
            }
            if (!(str3 == "pk10_lmp"))
            {
                if (str3 != "pk10_d1_10")
                {
                    if (str3 == "pk10_d12")
                    {
                        return "1,2,3,4,5,6,7,8,36,37,38";
                    }
                    if (str3 == "pk10_d3456")
                    {
                        return "9,10,11,12,13,14,15,16,17,18,19,20,21,22,23";
                    }
                    if (str3 != "pk10_d78910")
                    {
                        return str;
                    }
                    return "24,25,26,27,28,29,30,31,32,33,34,35";
                }
            }
            else
            {
                return "2,3,4,6,7,8,10,11,12,14,15,16,18,19,20,22,23,25,26,28,29,31,32,34,35,37,38";
            }
            return "1,5,9,13,17,21,24,27,30,33";
        }

        public string GetPKBJLNumTableTxt(string playtype)
        {
            string str = "";
            string str3 = playtype;
            if (str3 == null)
            {
                return str;
            }
            if (!(str3 == "p1"))
            {
                if (str3 != "p2")
                {
                    if (str3 == "p3")
                    {
                        return "第3桌";
                    }
                    if (str3 == "p4")
                    {
                        return "第4桌";
                    }
                    if (str3 != "p5")
                    {
                        return str;
                    }
                    return "第5桌";
                }
            }
            else
            {
                return "第1桌";
            }
            return "第2桌";
        }

        public double GetPlayDrawbackValue(string fgs_drawback, string ratio)
        {
            if (ratio.Equals("0") || string.IsNullOrEmpty(ratio))
            {
                return 0.0;
            }
            double num = (1.0 - (double.Parse(fgs_drawback) / 100.0)) / Convert.ToDouble(ratio);
            return double.Parse(num.ToString());
        }

        public double GetPlayTypeWTValue_PKBJL(string odds_id)
        {
            DataTable playTypeValue = CallBLL.cz_play_pkbjl_bll.GetPlayTypeValue(odds_id);
            if ((playTypeValue != null) && (playTypeValue.Rows.Count > 0))
            {
                return double.Parse(playTypeValue.Rows[0]["wt_value"].ToString());
            }
            return 0.0;
        }

        public cz_userinfo_session GetRateByUserObject(int masterid, object rate)
        {
            double num;
            string str2;
            double num2;
            string str = HttpContext.Current.Session["user_name"].ToString().Trim();
            cz_userinfo_session getUserModelInfo = this.GetUserModelInfo;
            if (masterid.Equals(1))
            {
                user_six_rate _rate = rate as user_six_rate;
                getUserModelInfo.get_six_session().set_fgsname(_rate.get_fgsname());
                getUserModelInfo.get_six_session().set_gdname(_rate.get_gdname());
                getUserModelInfo.get_six_session().set_zdname(_rate.get_zdname());
                getUserModelInfo.get_six_session().set_dlname(_rate.get_dlname());
                getUserModelInfo.get_six_session().set_zjzc(_rate.get_zjzc());
                getUserModelInfo.get_six_session().set_fgszc(_rate.get_fgszc());
                getUserModelInfo.get_six_session().set_gdzc(_rate.get_gdzc());
                getUserModelInfo.get_six_session().set_zdzc(_rate.get_zdzc());
                getUserModelInfo.get_six_session().set_dlzc(_rate.get_dlzc());
                num = (((double.Parse(_rate.get_zjzc()) + double.Parse(_rate.get_fgszc())) + double.Parse(_rate.get_gdzc())) + double.Parse(_rate.get_zdzc())) + double.Parse(_rate.get_dlzc());
                str2 = "1";
                str2 = _rate.get_zcyg();
                getUserModelInfo.get_six_session().set_zcyg(str2);
                if (str2 == "1")
                {
                    num2 = 100.0 - num;
                    num2 = double.Parse(getUserModelInfo.get_six_session().get_zjzc()) + num2;
                    getUserModelInfo.get_six_session().set_zjzc(num2.ToString());
                    return getUserModelInfo;
                }
                num2 = 100.0 - num;
                num2 = double.Parse(getUserModelInfo.get_six_session().get_fgszc()) + num2;
                getUserModelInfo.get_six_session().set_fgszc(num2.ToString());
                return getUserModelInfo;
            }
            user_kc_rate _rate2 = rate as user_kc_rate;
            getUserModelInfo.get_kc_session().set_fgsname(_rate2.get_fgsname());
            getUserModelInfo.get_kc_session().set_gdname(_rate2.get_gdname());
            getUserModelInfo.get_kc_session().set_zdname(_rate2.get_zdname());
            getUserModelInfo.get_kc_session().set_dlname(_rate2.get_dlname());
            getUserModelInfo.get_kc_session().set_zjzc(_rate2.get_zjzc());
            getUserModelInfo.get_kc_session().set_fgszc(_rate2.get_fgszc());
            getUserModelInfo.get_kc_session().set_gdzc(_rate2.get_gdzc());
            getUserModelInfo.get_kc_session().set_zdzc(_rate2.get_zdzc());
            getUserModelInfo.get_kc_session().set_dlzc(_rate2.get_dlzc());
            num = (((double.Parse(_rate2.get_zjzc()) + double.Parse(_rate2.get_fgszc())) + double.Parse(_rate2.get_gdzc())) + double.Parse(_rate2.get_zdzc())) + double.Parse(_rate2.get_dlzc());
            str2 = "1";
            str2 = _rate2.get_zcyg();
            getUserModelInfo.get_kc_session().set_zcyg(str2);
            if (str2 == "1")
            {
                num2 = 100.0 - num;
                num2 = double.Parse(getUserModelInfo.get_kc_session().get_zjzc()) + num2;
                getUserModelInfo.get_kc_session().set_zjzc(num2.ToString());
                return getUserModelInfo;
            }
            num2 = 100.0 - num;
            num2 = double.Parse(getUserModelInfo.get_kc_session().get_fgszc()) + num2;
            getUserModelInfo.get_kc_session().set_fgszc(num2.ToString());
            return getUserModelInfo;
        }

        protected string GetSIX_PlayIDChange(string playType)
        {
            switch (playType)
            {
                case "six_tma":
                    return "91001,91003,91004,91005,91007,91038";

                case "six_tmb":
                    return "91002,91003,91004,91005,91007,91038";

                case "six_zm":
                    return "91009,91023,91024";

                case "six_zmt_1":
                    return "91010";

                case "six_zmt_2":
                    return "91025";

                case "six_zmt_3":
                    return "91026";

                case "six_zmt_4":
                    return "91027";

                case "six_zmt_5":
                    return "91028";

                case "six_zmt_6":
                    return "91029";

                case "six_lm":
                    return "91016,91017,91018,91019,91020,91040";

                case "six_bz":
                    return "91037,91047,91048,91049,91050,91051";

                case "six_zm1_6":
                    return "91011,91012,91013,91014";

                case "six_tmsx":
                    return "91006";

                case "six_sxws":
                    return "91021,91022";

                case "six_bb":
                    return "91008,91057";

                case "six_lx":
                    return "91030,91031,91032,91033,91058";

                case "six_ws":
                    return "91034,91035,91036,91059";

                case "six_lh":
                    return "91041,91042,91043,91044,91045,91046,91039";

                case "six_qmwx":
                    return "91052,91053,91056";
            }
            return "";
        }

        protected string GetSkinList(string currentSkin)
        {
            ReturnResult result = new ReturnResult();
            result.set_data(null);
            result.set_success(0);
            try
            {
                string[] directories = Directory.GetDirectories(base.Server.MapPath("") + @"\Styles");
                Dictionary<string, string> dictionary = new Dictionary<string, string>();
                for (int i = 0; i < directories.Length; i++)
                {
                    string name = new DirectoryInfo(directories[i]).Name;
                    if (name.ToLower().Equals(currentSkin.ToLower()))
                    {
                        dictionary.Add(name, "1");
                    }
                    else
                    {
                        dictionary.Add(name, "0");
                    }
                }
                result.set_success(200);
                result.set_tipinfo(PageBase.GetMessageByCache("u100002", "MessageHint"));
                result.set_data(dictionary);
            }
            catch
            {
                result.set_tipinfo(PageBase.GetMessageByCache("u100003", "MessageHint"));
            }
            return JsonHandle.ObjectToJson(result);
        }

        protected string GetSPEED5_PlayIDChange(string playType)
        {
            switch (playType)
            {
                case "speed5_lmp":
                    return "2,3,5,6,8,9,11,12,14,15,16,17,18";

                case "speed5_d1_5":
                    return "1,4,7,10,13";

                case "speed5_d1":
                    return "1,2,3,16,17,18,19,20,21";

                case "speed5_d2":
                    return "4,5,6,16,17,18,19,20,21";

                case "speed5_d3":
                    return "7,8,9,16,17,18,19,20,21";

                case "speed5_d4":
                    return "10,11,12,16,17,18,19,20,21";

                case "speed5_d5":
                    return "13,14,15,16,17,18,19,20,21";
            }
            return "";
        }

        protected string GetSSC168_PlayIDChange(string playType)
        {
            switch (playType)
            {
                case "ssc168_lmp":
                    return "2,3,5,6,8,9,11,12,14,15,16,17,18";

                case "ssc168_d1_5":
                    return "1,4,7,10,13";

                case "ssc168_d1":
                    return "1,2,3,16,17,18,19,20,21";

                case "ssc168_d2":
                    return "4,5,6,16,17,18,19,20,21";

                case "ssc168_d3":
                    return "7,8,9,16,17,18,19,20,21";

                case "ssc168_d4":
                    return "10,11,12,16,17,18,19,20,21";

                case "ssc168_d5":
                    return "13,14,15,16,17,18,19,20,21";
            }
            return "";
        }

        public DataTable GetUserDrawback_car168(user_kc_rate kc_rate, string kc_kind)
        {
            DataTable cache = CacheHelper.GetCache("kc_drawback_FileCacheKey_car168" + this.Session["user_name"].ToString()) as DataTable;
            if (cache == null)
            {
                string str = string.Format(kc_rate.get_mastername() + ",'{0}'", this.Session["user_name"].ToString());
                return CallBLL.cz_drawback_car168_bll.GetDrawbackByUsers(str, kc_kind);
            }
            return cache;
        }

        public DataTable GetUserDrawback_car168(user_kc_rate kc_rate, string kc_kind, string play_id)
        {
            DataTable cache = CacheHelper.GetCache("kc_drawback_FileCacheKey_car168" + play_id + this.Session["user_name"].ToString()) as DataTable;
            if (cache == null)
            {
                string str = string.Format(kc_rate.get_mastername() + ",'{0}'", this.Session["user_name"].ToString());
                return CallBLL.cz_drawback_car168_bll.GetDrawbackByUsers(str, kc_kind, play_id);
            }
            return cache;
        }

        public DataTable GetUserDrawback_cqsc(user_kc_rate kc_rate, string kc_kind)
        {
            DataTable cache = CacheHelper.GetCache("kc_drawback_FileCacheKey_cqsc" + this.Session["user_name"].ToString()) as DataTable;
            if (cache == null)
            {
                string str = string.Format(kc_rate.get_mastername() + ",'{0}'", this.Session["user_name"].ToString());
                return CallBLL.cz_drawback_cqsc_bll.GetDrawbackByUsers(str, kc_kind);
            }
            return cache;
        }

        public DataTable GetUserDrawback_cqsc(user_kc_rate kc_rate, string kc_kind, string play_id)
        {
            DataTable cache = CacheHelper.GetCache("kc_drawback_FileCacheKey_cqsc" + play_id + this.Session["user_name"].ToString()) as DataTable;
            if (cache == null)
            {
                string str = string.Format(kc_rate.get_mastername() + ",'{0}'", this.Session["user_name"].ToString());
                return CallBLL.cz_drawback_cqsc_bll.GetDrawbackByUsers(str, kc_kind, play_id);
            }
            return cache;
        }

        public DataTable GetUserDrawback_happycar(user_kc_rate kc_rate, string kc_kind)
        {
            DataTable cache = CacheHelper.GetCache("kc_drawback_FileCacheKey_happycar" + this.Session["user_name"].ToString()) as DataTable;
            if (cache == null)
            {
                string str = string.Format(kc_rate.get_mastername() + ",'{0}'", this.Session["user_name"].ToString());
                return CallBLL.cz_drawback_happycar_bll.GetDrawbackByUsers(str, kc_kind);
            }
            return cache;
        }

        public DataTable GetUserDrawback_happycar(user_kc_rate kc_rate, string kc_kind, string play_id)
        {
            DataTable cache = CacheHelper.GetCache("kc_drawback_FileCacheKey_happycar" + play_id + this.Session["user_name"].ToString()) as DataTable;
            if (cache == null)
            {
                string str = string.Format(kc_rate.get_mastername() + ",'{0}'", this.Session["user_name"].ToString());
                return CallBLL.cz_drawback_happycar_bll.GetDrawbackByUsers(str, kc_kind, play_id);
            }
            return cache;
        }

        public DataTable GetUserDrawback_jscar(user_kc_rate kc_rate, string kc_kind)
        {
            DataTable cache = CacheHelper.GetCache("kc_drawback_FileCacheKey_jscar" + this.Session["user_name"].ToString()) as DataTable;
            if (cache == null)
            {
                string str = string.Format(kc_rate.get_mastername() + ",'{0}'", this.Session["user_name"].ToString());
                return CallBLL.cz_drawback_jscar_bll.GetDrawbackByUsers(str, kc_kind);
            }
            return cache;
        }

        public DataTable GetUserDrawback_jscar(user_kc_rate kc_rate, string kc_kind, string play_id)
        {
            DataTable cache = CacheHelper.GetCache("kc_drawback_FileCacheKey_jscar" + play_id + this.Session["user_name"].ToString()) as DataTable;
            if (cache == null)
            {
                string str = string.Format(kc_rate.get_mastername() + ",'{0}'", this.Session["user_name"].ToString());
                return CallBLL.cz_drawback_jscar_bll.GetDrawbackByUsers(str, kc_kind, play_id);
            }
            return cache;
        }

        public DataTable GetUserDrawback_jscqsc(user_kc_rate kc_rate, string kc_kind)
        {
            DataTable cache = CacheHelper.GetCache("kc_drawback_FileCacheKey_jscqsc" + this.Session["user_name"].ToString()) as DataTable;
            if (cache == null)
            {
                string str = string.Format(kc_rate.get_mastername() + ",'{0}'", this.Session["user_name"].ToString());
                return CallBLL.cz_drawback_jscqsc_bll.GetDrawbackByUsers(str, kc_kind);
            }
            return cache;
        }

        public DataTable GetUserDrawback_jscqsc(user_kc_rate kc_rate, string kc_kind, string play_id)
        {
            DataTable cache = CacheHelper.GetCache("kc_drawback_FileCacheKey_jscqsc" + play_id + this.Session["user_name"].ToString()) as DataTable;
            if (cache == null)
            {
                string str = string.Format(kc_rate.get_mastername() + ",'{0}'", this.Session["user_name"].ToString());
                return CallBLL.cz_drawback_jscqsc_bll.GetDrawbackByUsers(str, kc_kind, play_id);
            }
            return cache;
        }

        public DataTable GetUserDrawback_jsft2(user_kc_rate kc_rate, string kc_kind)
        {
            DataTable cache = CacheHelper.GetCache("kc_drawback_FileCacheKey_jsft2" + this.Session["user_name"].ToString()) as DataTable;
            if (cache == null)
            {
                string str = string.Format(kc_rate.get_mastername() + ",'{0}'", this.Session["user_name"].ToString());
                return CallBLL.cz_drawback_jsft2_bll.GetDrawbackByUsers(str, kc_kind);
            }
            return cache;
        }

        public DataTable GetUserDrawback_jsft2(user_kc_rate kc_rate, string kc_kind, string play_id)
        {
            DataTable cache = CacheHelper.GetCache("kc_drawback_FileCacheKey_jsft2" + play_id + this.Session["user_name"].ToString()) as DataTable;
            if (cache == null)
            {
                string str = string.Format(kc_rate.get_mastername() + ",'{0}'", this.Session["user_name"].ToString());
                return CallBLL.cz_drawback_jsft2_bll.GetDrawbackByUsers(str, kc_kind, play_id);
            }
            return cache;
        }

        public DataTable GetUserDrawback_jsk3(user_kc_rate kc_rate, string kc_kind)
        {
            DataTable cache = CacheHelper.GetCache("kc_drawback_FileCacheKey_jsk3" + this.Session["user_name"].ToString()) as DataTable;
            if (cache == null)
            {
                string str = string.Format(kc_rate.get_mastername() + ",'{0}'", this.Session["user_name"].ToString());
                return CallBLL.cz_drawback_jsk3_bll.GetDrawbackByUsers(str, kc_kind);
            }
            return cache;
        }

        public DataTable GetUserDrawback_jsk3(user_kc_rate kc_rate, string kc_kind, string play_id)
        {
            DataTable cache = CacheHelper.GetCache("kc_drawback_FileCacheKey_jsk3" + play_id + this.Session["user_name"].ToString()) as DataTable;
            if (cache == null)
            {
                string str = string.Format(kc_rate.get_mastername() + ",'{0}'", this.Session["user_name"].ToString());
                return CallBLL.cz_drawback_jsk3_bll.GetDrawbackByUsers(str, kc_kind, play_id);
            }
            return cache;
        }

        public DataTable GetUserDrawback_jspk10(user_kc_rate kc_rate, string kc_kind)
        {
            DataTable cache = CacheHelper.GetCache("kc_drawback_FileCacheKey_jspk10" + this.Session["user_name"].ToString()) as DataTable;
            if (cache == null)
            {
                string str = string.Format(kc_rate.get_mastername() + ",'{0}'", this.Session["user_name"].ToString());
                return CallBLL.cz_drawback_jspk10_bll.GetDrawbackByUsers(str, kc_kind);
            }
            return cache;
        }

        public DataTable GetUserDrawback_jspk10(user_kc_rate kc_rate, string kc_kind, string play_id)
        {
            DataTable cache = CacheHelper.GetCache("kc_drawback_FileCacheKey_jspk10" + play_id + this.Session["user_name"].ToString()) as DataTable;
            if (cache == null)
            {
                string str = string.Format(kc_rate.get_mastername() + ",'{0}'", this.Session["user_name"].ToString());
                return CallBLL.cz_drawback_jspk10_bll.GetDrawbackByUsers(str, kc_kind, play_id);
            }
            return cache;
        }

        public DataTable GetUserDrawback_jssfc(user_kc_rate kc_rate, string kc_kind)
        {
            DataTable cache = CacheHelper.GetCache("kc_drawback_FileCacheKey_jssfc" + this.Session["user_name"].ToString()) as DataTable;
            if (cache == null)
            {
                string str = string.Format(kc_rate.get_mastername() + ",'{0}'", this.Session["user_name"].ToString());
                return CallBLL.cz_drawback_jssfc_bll.GetDrawbackByUsers(str, kc_kind);
            }
            return cache;
        }

        public DataTable GetUserDrawback_jssfc(user_kc_rate kc_rate, string kc_kind, string play_id)
        {
            DataTable cache = CacheHelper.GetCache("kc_drawback_FileCacheKey_jssfc" + play_id + this.Session["user_name"].ToString()) as DataTable;
            if (cache == null)
            {
                string str = string.Format(kc_rate.get_mastername() + ",'{0}'", this.Session["user_name"].ToString());
                return CallBLL.cz_drawback_jssfc_bll.GetDrawbackByUsers(str, kc_kind, play_id);
            }
            return cache;
        }

        public DataTable GetUserDrawback_k8sc(user_kc_rate kc_rate, string kc_kind)
        {
            DataTable cache = CacheHelper.GetCache("kc_drawback_FileCacheKey_k8sc" + this.Session["user_name"].ToString()) as DataTable;
            if (cache == null)
            {
                string str = string.Format(kc_rate.get_mastername() + ",'{0}'", this.Session["user_name"].ToString());
                return CallBLL.cz_drawback_k8sc_bll.GetDrawbackByUsers(str, kc_kind);
            }
            return cache;
        }

        public DataTable GetUserDrawback_k8sc(user_kc_rate kc_rate, string kc_kind, string play_id)
        {
            DataTable cache = CacheHelper.GetCache("kc_drawback_FileCacheKey_k8sc" + play_id + this.Session["user_name"].ToString()) as DataTable;
            if (cache == null)
            {
                string str = string.Format(kc_rate.get_mastername() + ",'{0}'", this.Session["user_name"].ToString());
                return CallBLL.cz_drawback_k8sc_bll.GetDrawbackByUsers(str, kc_kind, play_id);
            }
            return cache;
        }

        public DataTable GetUserDrawback_kl10(user_kc_rate kc_rate, string kc_kind)
        {
            DataTable cache = CacheHelper.GetCache("kc_drawback_FileCacheKey_kl10" + this.Session["user_name"].ToString()) as DataTable;
            if (cache == null)
            {
                string str = string.Format(kc_rate.get_mastername() + ",'{0}'", this.Session["user_name"].ToString());
                return CallBLL.cz_drawback_kl10_bll.GetDrawbackByUsers(str, kc_kind);
            }
            return cache;
        }

        public DataTable GetUserDrawback_kl10(user_kc_rate kc_rate, string kc_kind, string play_id)
        {
            DataTable cache = CacheHelper.GetCache("kc_drawback_FileCacheKey_kl10" + play_id + this.Session["user_name"].ToString()) as DataTable;
            if (cache == null)
            {
                string str = string.Format(kc_rate.get_mastername() + ",'{0}'", this.Session["user_name"].ToString());
                return CallBLL.cz_drawback_kl10_bll.GetDrawbackByUsers(str, kc_kind, play_id);
            }
            return cache;
        }

        public DataTable GetUserDrawback_kl8(user_kc_rate kc_rate, string kc_kind)
        {
            DataTable cache = CacheHelper.GetCache("kc_drawback_FileCacheKey_kl8" + this.Session["user_name"].ToString()) as DataTable;
            if (cache == null)
            {
                string str = string.Format(kc_rate.get_mastername() + ",'{0}'", this.Session["user_name"].ToString());
                return CallBLL.cz_drawback_kl8_bll.GetDrawbackByUsers(str, kc_kind);
            }
            return cache;
        }

        public DataTable GetUserDrawback_kl8(user_kc_rate kc_rate, string kc_kind, string play_id)
        {
            DataTable cache = CacheHelper.GetCache("kc_drawback_FileCacheKey_kl8" + play_id + this.Session["user_name"].ToString()) as DataTable;
            if (cache == null)
            {
                string str = string.Format(kc_rate.get_mastername() + ",'{0}'", this.Session["user_name"].ToString());
                return CallBLL.cz_drawback_kl8_bll.GetDrawbackByUsers(str, kc_kind, play_id);
            }
            return cache;
        }

        public DataTable GetUserDrawback_pcdd(user_kc_rate kc_rate, string kc_kind)
        {
            DataTable cache = CacheHelper.GetCache("kc_drawback_FileCacheKey_pcdd" + this.Session["user_name"].ToString()) as DataTable;
            if (cache == null)
            {
                string str = string.Format(kc_rate.get_mastername() + ",'{0}'", this.Session["user_name"].ToString());
                return CallBLL.cz_drawback_pcdd_bll.GetDrawbackByUsers(str, kc_kind);
            }
            return cache;
        }

        public DataTable GetUserDrawback_pcdd(user_kc_rate kc_rate, string kc_kind, string play_id)
        {
            DataTable cache = CacheHelper.GetCache("kc_drawback_FileCacheKey_pcdd" + play_id + this.Session["user_name"].ToString()) as DataTable;
            if (cache == null)
            {
                string str = string.Format(kc_rate.get_mastername() + ",'{0}'", this.Session["user_name"].ToString());
                return CallBLL.cz_drawback_pcdd_bll.GetDrawbackByUsers(str, kc_kind, play_id);
            }
            return cache;
        }

        public DataTable GetUserDrawback_pk10(user_kc_rate kc_rate, string kc_kind)
        {
            DataTable cache = CacheHelper.GetCache("kc_drawback_FileCacheKey_pk10" + this.Session["user_name"].ToString()) as DataTable;
            if (cache == null)
            {
                string str = string.Format(kc_rate.get_mastername() + ",'{0}'", this.Session["user_name"].ToString());
                return CallBLL.cz_drawback_pk10_bll.GetDrawbackByUsers(str, kc_kind);
            }
            return cache;
        }

        public DataTable GetUserDrawback_pk10(user_kc_rate kc_rate, string kc_kind, string play_id)
        {
            DataTable cache = CacheHelper.GetCache("kc_drawback_FileCacheKey_pk10" + play_id + this.Session["user_name"].ToString()) as DataTable;
            if (cache == null)
            {
                string str = string.Format(kc_rate.get_mastername() + ",'{0}'", this.Session["user_name"].ToString());
                return CallBLL.cz_drawback_pk10_bll.GetDrawbackByUsers(str, kc_kind, play_id);
            }
            return cache;
        }

        public DataTable GetUserDrawback_pkbjl(user_kc_rate kc_rate, string kc_kind)
        {
            DataTable cache = CacheHelper.GetCache("kc_drawback_FileCacheKey_pkbjl" + this.Session["user_name"].ToString()) as DataTable;
            if (cache == null)
            {
                string str = string.Format(kc_rate.get_mastername() + ",'{0}'", this.Session["user_name"].ToString());
                return CallBLL.cz_drawback_pkbjl_bll.GetDrawbackByUsers(str, kc_kind);
            }
            return cache;
        }

        public DataTable GetUserDrawback_pkbjl(user_kc_rate kc_rate, string kc_kind, string play_id)
        {
            DataTable cache = CacheHelper.GetCache("kc_drawback_FileCacheKey_pkbjl" + play_id + this.Session["user_name"].ToString()) as DataTable;
            if (cache == null)
            {
                string str = string.Format(kc_rate.get_mastername() + ",'{0}'", this.Session["user_name"].ToString());
                return CallBLL.cz_drawback_pkbjl_bll.GetDrawbackByUsers(str, kc_kind, play_id);
            }
            return cache;
        }

        public DataSet GetUserDrawback_six(user_six_rate six_rate, string six_kind)
        {
            if (CacheHelper.GetCache("six_drawback_FileCacheKey" + HttpContext.Current.Session["user_name"].ToString()) == null)
            {
                string str = string.Format(six_rate.get_mastername() + ",'{0}'", HttpContext.Current.Session["user_name"].ToString());
                DataSet drawbackListByUsers = CallBLL.cz_drawback_six_bll.GetDrawbackListByUsers(str, six_kind);
                if (((drawbackListByUsers != null) && (drawbackListByUsers.Tables.Count > 0)) && (drawbackListByUsers.Tables[0].Rows.Count > 0))
                {
                    return drawbackListByUsers;
                }
                return null;
            }
            return (CacheHelper.GetCache("six_drawback_FileCacheKey" + HttpContext.Current.Session["user_name"].ToString()) as DataSet);
        }

        public DataSet GetUserDrawback_six(user_six_rate six_rate, string six_kind, string play_id)
        {
            if (CacheHelper.GetCache("six_drawback_FileCacheKey" + play_id + HttpContext.Current.Session["user_name"].ToString()) == null)
            {
                string str = string.Format(six_rate.get_mastername() + ",'{0}'", HttpContext.Current.Session["user_name"].ToString());
                DataSet set = CallBLL.cz_drawback_six_bll.GetDrawbackListByUsers(str, six_kind, play_id);
                if (((set != null) && (set.Tables.Count > 0)) && (set.Tables[0].Rows.Count > 0))
                {
                    return set;
                }
                return null;
            }
            return (CacheHelper.GetCache("six_drawback_FileCacheKey" + play_id + HttpContext.Current.Session["user_name"].ToString()) as DataSet);
        }

        public DataTable GetUserDrawback_speed5(user_kc_rate kc_rate, string kc_kind)
        {
            DataTable cache = CacheHelper.GetCache("kc_drawback_FileCacheKey_speed5" + this.Session["user_name"].ToString()) as DataTable;
            if (cache == null)
            {
                string str = string.Format(kc_rate.get_mastername() + ",'{0}'", this.Session["user_name"].ToString());
                return CallBLL.cz_drawback_speed5_bll.GetDrawbackByUsers(str, kc_kind);
            }
            return cache;
        }

        public DataTable GetUserDrawback_speed5(user_kc_rate kc_rate, string kc_kind, string play_id)
        {
            DataTable cache = CacheHelper.GetCache("kc_drawback_FileCacheKey_speed5" + play_id + this.Session["user_name"].ToString()) as DataTable;
            if (cache == null)
            {
                string str = string.Format(kc_rate.get_mastername() + ",'{0}'", this.Session["user_name"].ToString());
                return CallBLL.cz_drawback_speed5_bll.GetDrawbackByUsers(str, kc_kind, play_id);
            }
            return cache;
        }

        public DataTable GetUserDrawback_ssc168(user_kc_rate kc_rate, string kc_kind)
        {
            DataTable cache = CacheHelper.GetCache("kc_drawback_FileCacheKey_ssc168" + this.Session["user_name"].ToString()) as DataTable;
            if (cache == null)
            {
                string str = string.Format(kc_rate.get_mastername() + ",'{0}'", this.Session["user_name"].ToString());
                return CallBLL.cz_drawback_ssc168_bll.GetDrawbackByUsers(str, kc_kind);
            }
            return cache;
        }

        public DataTable GetUserDrawback_ssc168(user_kc_rate kc_rate, string kc_kind, string play_id)
        {
            DataTable cache = CacheHelper.GetCache("kc_drawback_FileCacheKey_ssc168" + play_id + this.Session["user_name"].ToString()) as DataTable;
            if (cache == null)
            {
                string str = string.Format(kc_rate.get_mastername() + ",'{0}'", this.Session["user_name"].ToString());
                return CallBLL.cz_drawback_ssc168_bll.GetDrawbackByUsers(str, kc_kind, play_id);
            }
            return cache;
        }

        public DataTable GetUserDrawback_vrcar(user_kc_rate kc_rate, string kc_kind)
        {
            DataTable cache = CacheHelper.GetCache("kc_drawback_FileCacheKey_vrcar" + this.Session["user_name"].ToString()) as DataTable;
            if (cache == null)
            {
                string str = string.Format(kc_rate.get_mastername() + ",'{0}'", this.Session["user_name"].ToString());
                return CallBLL.cz_drawback_vrcar_bll.GetDrawbackByUsers(str, kc_kind);
            }
            return cache;
        }

        public DataTable GetUserDrawback_vrcar(user_kc_rate kc_rate, string kc_kind, string play_id)
        {
            DataTable cache = CacheHelper.GetCache("kc_drawback_FileCacheKey_vrcar" + play_id + this.Session["user_name"].ToString()) as DataTable;
            if (cache == null)
            {
                string str = string.Format(kc_rate.get_mastername() + ",'{0}'", this.Session["user_name"].ToString());
                return CallBLL.cz_drawback_vrcar_bll.GetDrawbackByUsers(str, kc_kind, play_id);
            }
            return cache;
        }

        public DataTable GetUserDrawback_vrssc(user_kc_rate kc_rate, string kc_kind)
        {
            DataTable cache = CacheHelper.GetCache("kc_drawback_FileCacheKey_vrssc" + this.Session["user_name"].ToString()) as DataTable;
            if (cache == null)
            {
                string str = string.Format(kc_rate.get_mastername() + ",'{0}'", this.Session["user_name"].ToString());
                return CallBLL.cz_drawback_vrssc_bll.GetDrawbackByUsers(str, kc_kind);
            }
            return cache;
        }

        public DataTable GetUserDrawback_vrssc(user_kc_rate kc_rate, string kc_kind, string play_id)
        {
            DataTable cache = CacheHelper.GetCache("kc_drawback_FileCacheKey_vrssc" + play_id + this.Session["user_name"].ToString()) as DataTable;
            if (cache == null)
            {
                string str = string.Format(kc_rate.get_mastername() + ",'{0}'", this.Session["user_name"].ToString());
                return CallBLL.cz_drawback_vrssc_bll.GetDrawbackByUsers(str, kc_kind, play_id);
            }
            return cache;
        }

        public DataTable GetUserDrawback_xyft5(user_kc_rate kc_rate, string kc_kind)
        {
            DataTable cache = CacheHelper.GetCache("kc_drawback_FileCacheKey_xyft5" + this.Session["user_name"].ToString()) as DataTable;
            if (cache == null)
            {
                string str = string.Format(kc_rate.get_mastername() + ",'{0}'", this.Session["user_name"].ToString());
                return CallBLL.cz_drawback_xyft5_bll.GetDrawbackByUsers(str, kc_kind);
            }
            return cache;
        }

        public DataTable GetUserDrawback_xyft5(user_kc_rate kc_rate, string kc_kind, string play_id)
        {
            DataTable cache = CacheHelper.GetCache("kc_drawback_FileCacheKey_xyft5" + play_id + this.Session["user_name"].ToString()) as DataTable;
            if (cache == null)
            {
                string str = string.Format(kc_rate.get_mastername() + ",'{0}'", this.Session["user_name"].ToString());
                return CallBLL.cz_drawback_xyft5_bll.GetDrawbackByUsers(str, kc_kind, play_id);
            }
            return cache;
        }

        public DataTable GetUserDrawback_xyftoa(user_kc_rate kc_rate, string kc_kind)
        {
            DataTable cache = CacheHelper.GetCache("kc_drawback_FileCacheKey_xyftoa" + this.Session["user_name"].ToString()) as DataTable;
            if (cache == null)
            {
                string str = string.Format(kc_rate.get_mastername() + ",'{0}'", this.Session["user_name"].ToString());
                return CallBLL.cz_drawback_xyftoa_bll.GetDrawbackByUsers(str, kc_kind);
            }
            return cache;
        }

        public DataTable GetUserDrawback_xyftoa(user_kc_rate kc_rate, string kc_kind, string play_id)
        {
            DataTable cache = CacheHelper.GetCache("kc_drawback_FileCacheKey_xyftoa" + play_id + this.Session["user_name"].ToString()) as DataTable;
            if (cache == null)
            {
                string str = string.Format(kc_rate.get_mastername() + ",'{0}'", this.Session["user_name"].ToString());
                return CallBLL.cz_drawback_xyftoa_bll.GetDrawbackByUsers(str, kc_kind, play_id);
            }
            return cache;
        }

        public DataTable GetUserDrawback_xyftsg(user_kc_rate kc_rate, string kc_kind)
        {
            DataTable cache = CacheHelper.GetCache("kc_drawback_FileCacheKey_xyftsg" + this.Session["user_name"].ToString()) as DataTable;
            if (cache == null)
            {
                string str = string.Format(kc_rate.get_mastername() + ",'{0}'", this.Session["user_name"].ToString());
                return CallBLL.cz_drawback_xyftsg_bll.GetDrawbackByUsers(str, kc_kind);
            }
            return cache;
        }

        public DataTable GetUserDrawback_xyftsg(user_kc_rate kc_rate, string kc_kind, string play_id)
        {
            DataTable cache = CacheHelper.GetCache("kc_drawback_FileCacheKey_xyftsg" + play_id + this.Session["user_name"].ToString()) as DataTable;
            if (cache == null)
            {
                string str = string.Format(kc_rate.get_mastername() + ",'{0}'", this.Session["user_name"].ToString());
                return CallBLL.cz_drawback_xyftsg_bll.GetDrawbackByUsers(str, kc_kind, play_id);
            }
            return cache;
        }

        public DataTable GetUserDrawback_xync(user_kc_rate kc_rate, string kc_kind)
        {
            DataTable cache = CacheHelper.GetCache("kc_drawback_FileCacheKey_xync" + this.Session["user_name"].ToString()) as DataTable;
            if (cache == null)
            {
                string str = string.Format(kc_rate.get_mastername() + ",'{0}'", this.Session["user_name"].ToString());
                return CallBLL.cz_drawback_xync_bll.GetDrawbackByUsers(str, kc_kind);
            }
            return cache;
        }

        public DataTable GetUserDrawback_xync(user_kc_rate kc_rate, string kc_kind, string play_id)
        {
            DataTable cache = CacheHelper.GetCache("kc_drawback_FileCacheKey_xync" + play_id + this.Session["user_name"].ToString()) as DataTable;
            if (cache == null)
            {
                string str = string.Format(kc_rate.get_mastername() + ",'{0}'", this.Session["user_name"].ToString());
                return CallBLL.cz_drawback_xync_bll.GetDrawbackByUsers(str, kc_kind, play_id);
            }
            return cache;
        }

        public user_kc_rate GetUserRate_kc(string zjName)
        {
            user_kc_rate _rate;
            if ((CacheHelper.GetCache("kc_rate_FileCacheKey" + HttpContext.Current.Session["user_name"].ToString()) == null) || (HttpContext.Current.Session["kc_rate_FileCacheKey" + HttpContext.Current.Session["user_name"].ToString()] == null))
            {
                DataTable table = CallBLL.cz_rate_kc_bll.GetRateByAccount(HttpContext.Current.Session["user_name"].ToString()).Tables[0];
                if (table.Rows.Count > 0)
                {
                    _rate = new user_kc_rate();
                    _rate.set_fgsname(table.Rows[0]["fgs_name"].ToString().Trim());
                    _rate.set_gdname(table.Rows[0]["gd_name"].ToString().Trim());
                    _rate.set_zdname(table.Rows[0]["zd_name"].ToString().Trim());
                    _rate.set_dlname(table.Rows[0]["dl_name"].ToString().Trim());
                    _rate.set_zjzc(table.Rows[0]["zj_rate"].ToString().Trim());
                    _rate.set_fgszc(table.Rows[0]["fgs_rate"].ToString().Trim());
                    _rate.set_gdzc(table.Rows[0]["gd_rate"].ToString().Trim());
                    _rate.set_zdzc(table.Rows[0]["zd_rate"].ToString().Trim());
                    _rate.set_dlzc(table.Rows[0]["dl_rate"].ToString().Trim());
                    _rate.set_zcyg(table.Rows[0]["kc_rate_owner"].ToString().Trim());
                    string str = table.Rows[0]["kc_rate_owner"].ToString().Trim();
                    string str2 = table.Rows[0]["kc_kind"].ToString().Trim();
                    _rate.set_mastername(string.Format("'{0}','{1}','{2}','{3}','{4}'", new object[] { zjName, _rate.get_fgsname(), _rate.get_gdname(), _rate.get_zdname(), _rate.get_dlname() }));
                    (HttpContext.Current.Session[HttpContext.Current.Session["user_name"] + "lottery_session_user_info"] as cz_userinfo_session).set_kc_kind(str2.ToUpper());
                    return _rate;
                }
            }
            else
            {
                _rate = HttpContext.Current.Session["kc_rate_FileCacheKey" + HttpContext.Current.Session["user_name"].ToString()] as user_kc_rate;
                string str3 = DateTime.Now.AddHours(-7.0).ToString("yyyy-MM-dd");
                if (HttpContext.Current.Session[HttpContext.Current.Session["user_name"] + "_begin_kc_date"].ToString().Trim() != str3)
                {
                    cz_userinfo_session getUserModelInfo = this.GetUserModelInfo;
                    int num = 0;
                    if (getUserModelInfo.get_isPhone().Equals(1))
                    {
                        num = 1;
                    }
                    this.Session.Abandon();
                    FileCacheHelper.DeleteDrawbackCaches(2);
                    if (num.Equals(1))
                    {
                        HttpContext.Current.Response.Write("<script>top.location.href='/m'</script>");
                    }
                    else
                    {
                        HttpContext.Current.Response.Write("<script>top.location.href='/'</script>");
                    }
                    HttpContext.Current.Response.End();
                }
                return _rate;
            }
            return null;
        }

        public user_six_rate GetUserRate_six(string zjName)
        {
            if ((CacheHelper.GetCache("six_rate_FileCacheKey" + HttpContext.Current.Session["user_name"].ToString()) == null) || (HttpContext.Current.Session["six_rate_FileCacheKey" + HttpContext.Current.Session["user_name"].ToString()] == null))
            {
                DataTable table = CallBLL.cz_rate_six_bll.GetRateByAccount(HttpContext.Current.Session["user_name"].ToString()).Tables[0];
                if (table.Rows.Count > 0)
                {
                    user_six_rate _rate = new user_six_rate();
                    _rate.set_fgsname(table.Rows[0]["fgs_name"].ToString().Trim());
                    _rate.set_gdname(table.Rows[0]["gd_name"].ToString().Trim());
                    _rate.set_zdname(table.Rows[0]["zd_name"].ToString().Trim());
                    _rate.set_dlname(table.Rows[0]["dl_name"].ToString().Trim());
                    _rate.set_zjzc(table.Rows[0]["zj_rate"].ToString().Trim());
                    _rate.set_fgszc(table.Rows[0]["fgs_rate"].ToString().Trim());
                    _rate.set_gdzc(table.Rows[0]["gd_rate"].ToString().Trim());
                    _rate.set_zdzc(table.Rows[0]["zd_rate"].ToString().Trim());
                    _rate.set_dlzc(table.Rows[0]["dl_rate"].ToString().Trim());
                    _rate.set_zcyg(table.Rows[0]["six_rate_owner"].ToString().Trim());
                    string str = table.Rows[0]["six_rate_owner"].ToString().Trim();
                    string str2 = table.Rows[0]["six_kind"].ToString().Trim();
                    _rate.set_mastername(string.Format("'{0}','{1}','{2}','{3}','{4}'", new object[] { zjName, _rate.get_fgsname(), _rate.get_gdname(), _rate.get_zdname(), _rate.get_dlname() }));
                    (HttpContext.Current.Session[HttpContext.Current.Session["user_name"] + "lottery_session_user_info"] as cz_userinfo_session).set_six_kind(str2.ToUpper());
                    return _rate;
                }
            }
            else
            {
                return (HttpContext.Current.Session["six_rate_FileCacheKey" + HttpContext.Current.Session["user_name"].ToString()] as user_six_rate);
            }
            return null;
        }

        protected string GetVRCAR_PlayIDChange(string playType)
        {
            string str = "";
            string str3 = playType;
            if (str3 == null)
            {
                return str;
            }
            if (!(str3 == "vrcar_lmp"))
            {
                if (str3 != "vrcar_d1_10")
                {
                    if (str3 == "vrcar_d12")
                    {
                        return "1,2,3,4,5,6,7,8,36,37,38";
                    }
                    if (str3 == "vrcar_d3456")
                    {
                        return "9,10,11,12,13,14,15,16,17,18,19,20,21,22,23";
                    }
                    if (str3 != "vrcar_d78910")
                    {
                        return str;
                    }
                    return "24,25,26,27,28,29,30,31,32,33,34,35";
                }
            }
            else
            {
                return "2,3,4,6,7,8,10,11,12,14,15,16,18,19,20,22,23,25,26,28,29,31,32,34,35,37,38";
            }
            return "1,5,9,13,17,21,24,27,30,33";
        }

        protected string GetVRSSC_PlayIDChange(string playType)
        {
            switch (playType)
            {
                case "vrssc_lmp":
                    return "2,3,5,6,8,9,11,12,14,15,16,17,18";

                case "vrssc_d1_5":
                    return "1,4,7,10,13";

                case "vrssc_d1":
                    return "1,2,3,16,17,18,19,20,21";

                case "vrssc_d2":
                    return "4,5,6,16,17,18,19,20,21";

                case "vrssc_d3":
                    return "7,8,9,16,17,18,19,20,21";

                case "vrssc_d4":
                    return "10,11,12,16,17,18,19,20,21";

                case "vrssc_d5":
                    return "13,14,15,16,17,18,19,20,21";
            }
            return "";
        }

        public string GetWTList(string uPI_ID, string numbers, string uPI_WT, ref bool isChange)
        {
            string str = "";
            DataTable wT = null;
            switch (uPI_ID)
            {
                case "92565":
                    wT = CallBLL.cz_wt_6xyz_six_bll.GetWT();
                    break;

                case "92286":
                    wT = CallBLL.cz_wt_3z2_six_bll.GetWT();
                    break;

                case "92285":
                    wT = CallBLL.cz_wt_3qz_six_bll.GetWT();
                    break;

                case "92287":
                    wT = CallBLL.cz_wt_2qz_six_bll.GetWT();
                    break;

                case "92288":
                    wT = CallBLL.cz_wt_2zt_six_bll.GetWT();
                    break;

                case "92289":
                    wT = CallBLL.cz_wt_tc_six_bll.GetWT();
                    break;

                case "92575":
                    wT = CallBLL.cz_wt_4z1_six_bll.GetWT();
                    break;

                case "92638":
                    wT = CallBLL.cz_wt_3qz_b_six_bll.GetWT();
                    break;

                case "92639":
                    wT = CallBLL.cz_wt_3z2_b_six_bll.GetWT();
                    break;

                case "92640":
                    wT = CallBLL.cz_wt_2qz_b_six_bll.GetWT();
                    break;

                case "92641":
                    wT = CallBLL.cz_wt_2zt_b_six_bll.GetWT();
                    break;

                case "92642":
                    wT = CallBLL.cz_wt_tc_b_six_bll.GetWT();
                    break;

                case "92643":
                    wT = CallBLL.cz_wt_4z1_b_six_bll.GetWT();
                    break;

                case "92566":
                    wT = CallBLL.cz_wt_sxlwsl_six_bll.GetWT();
                    break;

                case "92567":
                    wT = CallBLL.cz_wt_sxlwsl_six_bll.GetWT();
                    break;

                case "92568":
                    wT = CallBLL.cz_wt_sxlwsl_six_bll.GetWT();
                    break;

                case "92636":
                    wT = CallBLL.cz_wt_sxlwsl_six_bll.GetWT();
                    break;

                case "92569":
                    wT = CallBLL.cz_wt_sxlwsl_six_bll.GetWT();
                    break;

                case "92570":
                    wT = CallBLL.cz_wt_sxlwsl_six_bll.GetWT();
                    break;

                case "92571":
                    wT = CallBLL.cz_wt_sxlwsl_six_bll.GetWT();
                    break;

                case "92637":
                    wT = CallBLL.cz_wt_sxlwsl_six_bll.GetWT();
                    break;

                case "92572":
                    wT = CallBLL.cz_wt_5bz_six_bll.GetWT();
                    break;

                case "92588":
                    wT = CallBLL.cz_wt_6bz_six_bll.GetWT();
                    break;

                case "92589":
                    wT = CallBLL.cz_wt_7bz_six_bll.GetWT();
                    break;

                case "92590":
                    wT = CallBLL.cz_wt_8bz_six_bll.GetWT();
                    break;

                case "92591":
                    wT = CallBLL.cz_wt_9bz_six_bll.GetWT();
                    break;

                case "92592":
                    wT = CallBLL.cz_wt_10bz_six_bll.GetWT();
                    break;
            }
            if (wT == null)
            {
                base.Response.End();
            }
            string str2 = "";
            if ((((uPI_ID.Equals("92566") || uPI_ID.Equals("92567")) || (uPI_ID.Equals("92568") || uPI_ID.Equals("92569"))) || ((uPI_ID.Equals("92570") || uPI_ID.Equals("92571")) || uPI_ID.Equals("92636"))) || uPI_ID.Equals("92637"))
            {
                str2 = string.Format(" and odds_id={0}", uPI_ID);
            }
            string[] strArray = numbers.Split(new char[] { ',' });
            string[] strArray2 = uPI_WT.Split(new char[] { ',' });
            for (int i = 0; i < strArray.Length; i++)
            {
                int num2 = int.Parse(strArray[i]);
                if (!string.IsNullOrEmpty(str2) && (num2.Equals(0) || num2.Equals(int.Parse(base.get_YearLianID()))))
                {
                    if (string.IsNullOrEmpty(str))
                    {
                        str = str + 0;
                    }
                    else
                    {
                        str = str + "," + 0;
                    }
                }
                else
                {
                    double num3 = 0.0;
                    if (!string.IsNullOrEmpty(strArray2[i]))
                    {
                        num3 = double.Parse(strArray2[i]);
                    }
                    DataRow[] rowArray = wT.Select(string.Format(" number={0} {1} ", num2, str2));
                    if (rowArray.Length > 0)
                    {
                        double num4 = double.Parse(rowArray[0]["wt_value"].ToString());
                        if (!(num3.Equals(num4) ? true : isChange))
                        {
                            isChange = true;
                        }
                        string str3 = num2.ToString();
                        if (!string.IsNullOrEmpty(str2))
                        {
                            str3 = (num2.ToString().Length == 1) ? ("0" + num2) : num2.ToString();
                        }
                        if (!(string.IsNullOrEmpty(str2) || (!num2.Equals(0) && !num2.Equals(int.Parse(base.get_YearLianID())))))
                        {
                            num4 = 0.0;
                        }
                        if (string.IsNullOrEmpty(str))
                        {
                            str = str + num4;
                        }
                        else
                        {
                            str = str + "," + num4;
                        }
                    }
                }
            }
            return str;
        }

        public string GetWTListPC(string uPI_ID, string numbers, string uPI_WT, ref bool isChange)
        {
            string str = "";
            DataTable wT = null;
            switch (uPI_ID)
            {
                case "92565":
                    wT = CallBLL.cz_wt_6xyz_six_bll.GetWT();
                    break;

                case "92286":
                    wT = CallBLL.cz_wt_3z2_six_bll.GetWT();
                    break;

                case "92285":
                    wT = CallBLL.cz_wt_3qz_six_bll.GetWT();
                    break;

                case "92287":
                    wT = CallBLL.cz_wt_2qz_six_bll.GetWT();
                    break;

                case "92288":
                    wT = CallBLL.cz_wt_2zt_six_bll.GetWT();
                    break;

                case "92289":
                    wT = CallBLL.cz_wt_tc_six_bll.GetWT();
                    break;

                case "92575":
                    wT = CallBLL.cz_wt_4z1_six_bll.GetWT();
                    break;

                case "92638":
                    wT = CallBLL.cz_wt_3qz_b_six_bll.GetWT();
                    break;

                case "92639":
                    wT = CallBLL.cz_wt_3z2_b_six_bll.GetWT();
                    break;

                case "92640":
                    wT = CallBLL.cz_wt_2qz_b_six_bll.GetWT();
                    break;

                case "92641":
                    wT = CallBLL.cz_wt_2zt_b_six_bll.GetWT();
                    break;

                case "92642":
                    wT = CallBLL.cz_wt_tc_b_six_bll.GetWT();
                    break;

                case "92643":
                    wT = CallBLL.cz_wt_4z1_b_six_bll.GetWT();
                    break;

                case "92566":
                    wT = CallBLL.cz_wt_sxlwsl_six_bll.GetWT();
                    break;

                case "92567":
                    wT = CallBLL.cz_wt_sxlwsl_six_bll.GetWT();
                    break;

                case "92568":
                    wT = CallBLL.cz_wt_sxlwsl_six_bll.GetWT();
                    break;

                case "92636":
                    wT = CallBLL.cz_wt_sxlwsl_six_bll.GetWT();
                    break;

                case "92569":
                    wT = CallBLL.cz_wt_sxlwsl_six_bll.GetWT();
                    break;

                case "92570":
                    wT = CallBLL.cz_wt_sxlwsl_six_bll.GetWT();
                    break;

                case "92571":
                    wT = CallBLL.cz_wt_sxlwsl_six_bll.GetWT();
                    break;

                case "92637":
                    wT = CallBLL.cz_wt_sxlwsl_six_bll.GetWT();
                    break;

                case "92572":
                    wT = CallBLL.cz_wt_5bz_six_bll.GetWT();
                    break;

                case "92588":
                    wT = CallBLL.cz_wt_6bz_six_bll.GetWT();
                    break;

                case "92589":
                    wT = CallBLL.cz_wt_7bz_six_bll.GetWT();
                    break;

                case "92590":
                    wT = CallBLL.cz_wt_8bz_six_bll.GetWT();
                    break;

                case "92591":
                    wT = CallBLL.cz_wt_9bz_six_bll.GetWT();
                    break;

                case "92592":
                    wT = CallBLL.cz_wt_10bz_six_bll.GetWT();
                    break;
            }
            if (wT == null)
            {
                base.Response.End();
            }
            string str2 = "";
            if ((((uPI_ID.Equals("92566") || uPI_ID.Equals("92567")) || (uPI_ID.Equals("92568") || uPI_ID.Equals("92569"))) || ((uPI_ID.Equals("92570") || uPI_ID.Equals("92571")) || uPI_ID.Equals("92636"))) || uPI_ID.Equals("92637"))
            {
                str2 = string.Format(" and odds_id={0}", uPI_ID);
            }
            string[] strArray = numbers.Split(new char[] { ',' });
            string[] strArray2 = uPI_WT.Split(new char[] { ',' });
            for (int i = 0; i < strArray.Length; i++)
            {
                int num2 = int.Parse(strArray[i]);
                if (!string.IsNullOrEmpty(str2) && (num2.Equals(0) || num2.Equals(int.Parse(base.get_YearLianID()))))
                {
                    if (string.IsNullOrEmpty(str))
                    {
                        str = str + 0;
                    }
                    else
                    {
                        str = str + "," + 0;
                    }
                }
                else
                {
                    double num3 = 0.0;
                    if (!string.IsNullOrEmpty(strArray2[i]))
                    {
                        num3 = double.Parse(strArray2[i]);
                    }
                    DataRow[] rowArray = wT.Select(string.Format(" number={0} {1} ", num2, str2));
                    if (rowArray.Length > 0)
                    {
                        object obj2;
                        double num4 = double.Parse(rowArray[0]["wt_value"].ToString());
                        if (!(num3.Equals(num4) ? true : isChange))
                        {
                            isChange = true;
                        }
                        string str3 = num2.ToString();
                        if (!string.IsNullOrEmpty(str2))
                        {
                            str3 = (num2.ToString().Length == 1) ? ("0" + num2) : num2.ToString();
                        }
                        if (string.IsNullOrEmpty(str))
                        {
                            obj2 = str;
                            str = string.Concat(new object[] { obj2, str3, ",", num4 });
                        }
                        else
                        {
                            obj2 = str;
                            str = string.Concat(new object[] { obj2, "|", str3, ",", num4 });
                        }
                    }
                }
            }
            return str;
        }

        public string getxrlzr(string a, string b)
        {
            string str = "";
            string[] strArray = a.Split(new char[] { ',' });
            string[] strArray2 = b.Split(new char[] { ',' });
            foreach (string str2 in strArray)
            {
                foreach (string str3 in strArray2)
                {
                    if (str2.Trim() != str3.Trim())
                    {
                        string str5 = str;
                        str = str5 + str2.Trim() + "," + str3.Trim() + "~";
                    }
                }
            }
            return str.Substring(0, str.Length - 1);
        }

        public int getxrlzr_zs(string a, string b)
        {
            int num = 0;
            string[] strArray = a.Split(new char[] { ',' });
            string[] strArray2 = b.Split(new char[] { ',' });
            foreach (string str in strArray)
            {
                foreach (string str2 in strArray2)
                {
                    if (str.Trim() != str2.Trim())
                    {
                        num++;
                    }
                }
            }
            return num;
        }

        public string getxsqz(string a, string b, string c)
        {
            string str = "";
            string[] strArray = a.Split(new char[] { ',' });
            string[] strArray2 = b.Split(new char[] { ',' });
            string[] strArray3 = c.Split(new char[] { ',' });
            foreach (string str2 in strArray)
            {
                foreach (string str3 in strArray2)
                {
                    foreach (string str4 in strArray3)
                    {
                        if (((str2.Trim() != str3.Trim()) && (str3.Trim() != str4.Trim())) && (str2.Trim() != str4.Trim()))
                        {
                            string str6 = str;
                            str = str6 + str2.Trim() + "," + str3.Trim() + "," + str4.Trim() + "~";
                        }
                    }
                }
            }
            return str.Substring(0, str.Length - 1);
        }

        public int getxsqz_zs(string a, string b, string c)
        {
            int num = 0;
            string[] strArray = a.Split(new char[] { ',' });
            string[] strArray2 = b.Split(new char[] { ',' });
            string[] strArray3 = c.Split(new char[] { ',' });
            foreach (string str in strArray)
            {
                foreach (string str2 in strArray2)
                {
                    foreach (string str3 in strArray3)
                    {
                        if (((str.Trim() != str2.Trim()) && (str2.Trim() != str3.Trim())) && (str.Trim() != str3.Trim()))
                        {
                            num++;
                        }
                    }
                }
            }
            return num;
        }

        protected string GetXYFT5_PlayIDChange(string playType)
        {
            string str = "";
            string str3 = playType;
            if (str3 == null)
            {
                return str;
            }
            if (!(str3 == "xyft5_lmp"))
            {
                if (str3 != "xyft5_d1_10")
                {
                    if (str3 == "xyft5_d12")
                    {
                        return "1,2,3,4,5,6,7,8,36,37,38";
                    }
                    if (str3 == "xyft5_d3456")
                    {
                        return "9,10,11,12,13,14,15,16,17,18,19,20,21,22,23";
                    }
                    if (str3 != "xyft5_d78910")
                    {
                        return str;
                    }
                    return "24,25,26,27,28,29,30,31,32,33,34,35";
                }
            }
            else
            {
                return "2,3,4,6,7,8,10,11,12,14,15,16,18,19,20,22,23,25,26,28,29,31,32,34,35,37,38";
            }
            return "1,5,9,13,17,21,24,27,30,33";
        }

        protected string GetXYFTOA_PlayIDChange(string playType)
        {
            string str = "";
            string str3 = playType;
            if (str3 == null)
            {
                return str;
            }
            if (!(str3 == "xyftoa_lmp"))
            {
                if (str3 != "xyftoa_d1_10")
                {
                    if (str3 == "xyftoa_d12")
                    {
                        return "1,2,3,4,5,6,7,8,36,37,38";
                    }
                    if (str3 == "xyftoa_d3456")
                    {
                        return "9,10,11,12,13,14,15,16,17,18,19,20,21,22,23";
                    }
                    if (str3 != "xyftoa_d78910")
                    {
                        return str;
                    }
                    return "24,25,26,27,28,29,30,31,32,33,34,35";
                }
            }
            else
            {
                return "2,3,4,6,7,8,10,11,12,14,15,16,18,19,20,22,23,25,26,28,29,31,32,34,35,37,38";
            }
            return "1,5,9,13,17,21,24,27,30,33";
        }

        protected string GetXYFTSG_PlayIDChange(string playType)
        {
            string str = "";
            string str3 = playType;
            if (str3 == null)
            {
                return str;
            }
            if (!(str3 == "xyftsg_lmp"))
            {
                if (str3 != "xyftsg_d1_10")
                {
                    if (str3 == "xyftsg_d12")
                    {
                        return "1,2,3,4,5,6,7,8,36,37,38";
                    }
                    if (str3 == "xyftsg_d3456")
                    {
                        return "9,10,11,12,13,14,15,16,17,18,19,20,21,22,23";
                    }
                    if (str3 != "xyftsg_d78910")
                    {
                        return str;
                    }
                    return "24,25,26,27,28,29,30,31,32,33,34,35";
                }
            }
            else
            {
                return "2,3,4,6,7,8,10,11,12,14,15,16,18,19,20,22,23,25,26,28,29,31,32,34,35,37,38";
            }
            return "1,5,9,13,17,21,24,27,30,33";
        }

        protected string GetXYNC_PlayIDChange(string playType)
        {
            switch (playType)
            {
                case "xync_lmp":
                    return "100,102,103,104,105,107,108,109,11,110,112,113,114,115,117,118,119,12,120,13,80,82,83,84,85,87,88,89,90,92,93,94,95,97,98,99";

                case "xync_d1_8":
                    return "81,86,91,96,101,106,111,116";

                case "xync_d1":
                    return "121,122,81,82,83,84,85";

                case "xync_d2":
                    return "86,87,88,89,90,123,124";

                case "xync_d3":
                    return "91,92,93,94,95,125,126";

                case "xync_d4":
                    return "96,97,98,99,100,127,128";

                case "xync_d5":
                    return "101,102,103,104,105,129,130";

                case "xync_d6":
                    return "106,107,108,109,110,131,132";

                case "xync_d7":
                    return "111,112,113,114,115,133,134";

                case "xync_d8":
                    return "116,117,118,119,120,135,136";

                case "xync_zhlh":
                    return "11,12,13,80";

                case "xync_lm":
                    return "72,73,74,75,78,79";
            }
            return "";
        }

        protected string GetZodiacName(string numstr)
        {
            numstr = (numstr.Length == 1) ? ("0" + numstr) : numstr;
            string str = "";
            string[] strArray = base.get_ZodiacNumberArray().Split(new char[] { ',' });
            if (strArray[0].IndexOf(numstr) > -1)
            {
                return "鼠";
            }
            if (strArray[1].IndexOf(numstr) > -1)
            {
                return "牛";
            }
            if (strArray[2].IndexOf(numstr) > -1)
            {
                return "虎";
            }
            if (strArray[3].IndexOf(numstr) > -1)
            {
                return "兔";
            }
            if (strArray[4].IndexOf(numstr) > -1)
            {
                return "龍";
            }
            if (strArray[5].IndexOf(numstr) > -1)
            {
                return "蛇";
            }
            if (strArray[6].IndexOf(numstr) > -1)
            {
                return "馬";
            }
            if (strArray[7].IndexOf(numstr) > -1)
            {
                return "羊";
            }
            if (strArray[8].IndexOf(numstr) > -1)
            {
                return "猴";
            }
            if (strArray[9].IndexOf(numstr) > -1)
            {
                return "雞";
            }
            if (strArray[10].IndexOf(numstr) > -1)
            {
                return "狗";
            }
            if (strArray[11].IndexOf(numstr) > -1)
            {
                str = "豬";
            }
            return str;
        }

        public string GetZodiacNum(string numstr)
        {
            numstr = (numstr.Length == 1) ? ("0" + numstr) : numstr;
            string str = "";
            string[] strArray = base.get_ZodiacNumberArray().Split(new char[] { ',' });
            if (strArray[0].IndexOf(numstr) > -1)
            {
                return "01";
            }
            if (strArray[1].IndexOf(numstr) > -1)
            {
                return "02";
            }
            if (strArray[2].IndexOf(numstr) > -1)
            {
                return "03";
            }
            if (strArray[3].IndexOf(numstr) > -1)
            {
                return "04";
            }
            if (strArray[4].IndexOf(numstr) > -1)
            {
                return "05";
            }
            if (strArray[5].IndexOf(numstr) > -1)
            {
                return "06";
            }
            if (strArray[6].IndexOf(numstr) > -1)
            {
                return "07";
            }
            if (strArray[7].IndexOf(numstr) > -1)
            {
                return "08";
            }
            if (strArray[8].IndexOf(numstr) > -1)
            {
                return "09";
            }
            if (strArray[9].IndexOf(numstr) > -1)
            {
                return "10";
            }
            if (strArray[10].IndexOf(numstr) > -1)
            {
                return "11";
            }
            if (strArray[11].IndexOf(numstr) > -1)
            {
                str = "12";
            }
            return str;
        }

        public string GroupShowHrefString(int master_id, string order_num, string is_payment, string optwindow)
        {
            string str = "";
            if (master_id.Equals(1))
            {
                str = "six";
            }
            else
            {
                str = "kc";
            }
            if (this.WebModelView.Equals(0))
            {
                return string.Format("<a class='green groupshow' style='text-decoration:underline;'  href='javascript:;' data-href='/OldWeb/ReportBill/GroupShow_{0}.aspx?orderid={1}&ispay={2}&ow={3}' title='分組明細'>詳細</a>", new object[] { str, order_num, is_payment, optwindow });
            }
            return string.Format("<a class='green groupshow' style='text-decoration:underline;'  href='javascript:;' data-href='/ReportBill/GroupShow_{0}.aspx?orderid={1}&ispay={2}&ow={3}' title='分組明細'>詳細</a>", new object[] { str, order_num, is_payment, optwindow });
        }

        public string GroupShowHrefStringByPhone(int master_id, string order_num, string is_payment, string optwindow)
        {
            string str = "";
            if (master_id.Equals(1))
            {
                str = "six";
            }
            else
            {
                str = "kc";
            }
            return string.Format("<a class='green groupshow' style='text-decoration:underline;' href='javascript:;' data-href='/m/GroupShow_{0}.aspx?orderid={1}&ispay={2}&ow={3}' title='分組明細'>詳細</a>", new object[] { str, order_num, is_payment, optwindow });
        }

        protected static void insert_online(string userIP, string user, string user_type, DateTime first_time, DateTime last_time)
        {
            string str = " select u_name from cz_stat_online where u_name =@u_name ";
            SqlParameter[] parameterArray = new SqlParameter[] { new SqlParameter("@u_name", SqlDbType.NVarChar) };
            parameterArray[0].Value = user.Trim();
            if (CallBLL.cz_stat_online_bll.query_sql(str, parameterArray).Rows.Count > 0)
            {
                str = "update cz_stat_online set ip=@ip, first_time=@first_time, last_time=@last_time where u_name =@u_name ";
                SqlParameter[] parameterArray2 = new SqlParameter[] { new SqlParameter("@ip", SqlDbType.NVarChar), new SqlParameter("@first_time", SqlDbType.DateTime), new SqlParameter("@last_time", SqlDbType.DateTime), new SqlParameter("@u_name", SqlDbType.NVarChar) };
                parameterArray2[0].Value = userIP;
                parameterArray2[1].Value = first_time;
                parameterArray2[2].Value = last_time;
                parameterArray2[3].Value = user.Trim();
                CallBLL.cz_stat_online_bll.executte_sql(str, parameterArray2);
            }
            else
            {
                string str2 = string.Format("insert into cz_stat_online (u_name,u_type,first_time,last_time,ip) values(@u_name,@user_type,@first_time,@last_time,@userIP)", new object[0]);
                SqlParameter[] parameterArray3 = new SqlParameter[] { new SqlParameter("@u_name", SqlDbType.NVarChar), new SqlParameter("@user_type", SqlDbType.NVarChar), new SqlParameter("@first_time", SqlDbType.DateTime), new SqlParameter("@last_time", SqlDbType.DateTime), new SqlParameter("@userIP", SqlDbType.NVarChar) };
                parameterArray3[0].Value = user.Trim();
                parameterArray3[1].Value = user_type;
                parameterArray3[2].Value = first_time;
                parameterArray3[3].Value = last_time;
                parameterArray3[4].Value = userIP;
                try
                {
                    CallBLL.cz_stat_online_bll.executte_sql(str2, parameterArray3);
                }
                catch (SqlException exception)
                {
                    if ((exception.Number != 0xa29) && (exception.Number != 0xa43))
                    {
                        throw exception;
                    }
                }
            }
        }

        public bool IsCreditLock(string u_name)
        {
            if (FileCacheHelper.get_IsRedisCreditLock().Equals("0"))
            {
                return CallBLL.cz_credit_lock_bll.IsBusy(u_name);
            }
            return (FileCacheHelper.get_IsRedisCreditLock().Equals("1") || (FileCacheHelper.get_IsRedisCreditLock().Equals("2") || true));
        }

        protected void IsLotteryExist(string lotteryId)
        {
            bool flag = false;
            DataTable lotteryList = this.GetLotteryList();
            foreach (DataRow row in lotteryList.Rows)
            {
                if (row["id"].ToString().Equals(lotteryId))
                {
                    flag = true;
                    break;
                }
            }
            if (!flag)
            {
                this.Session.Abandon();
                ReturnResult result = new ReturnResult();
                result.set_success(100);
                result.set_tipinfo("");
                string s = JsonHandle.ObjectToJson(result);
                HttpContext.Current.Response.ContentType = "text/json";
                HttpContext.Current.Response.Write(s);
                HttpContext.Current.Response.End();
            }
        }

        protected bool IsLotteryExistByPhone(string lotteryId)
        {
            DataTable lotteryList = this.GetLotteryList();
            foreach (DataRow row in lotteryList.Rows)
            {
                if (row["id"].ToString().Equals(lotteryId))
                {
                    return true;
                }
            }
            return false;
        }

        protected bool IsOpenBigLottery(int master_id)
        {
            DataRow[] source = this.GetLotteryList().Select(string.Format(" master_id={0} ", master_id));
            return ((source != null) && (source.Count<DataRow>() > 0));
        }

        protected void IsOpenLottery()
        {
            if (((((((this.Context.Request.Path.ToLower().IndexOf("l_six") >= 0) || (this.Context.Request.Path.ToLower().IndexOf("l_cqsc") >= 0)) || ((this.Context.Request.Path.ToLower().IndexOf("l_k3") >= 0) || (this.Context.Request.Path.ToLower().IndexOf("l_kl10") >= 0))) || (((this.Context.Request.Path.ToLower().IndexOf("l_kl8") >= 0) || (this.Context.Request.Path.ToLower().IndexOf("l_pk10") >= 0)) || ((this.Context.Request.Path.ToLower().IndexOf("l_xync") >= 0) || (this.Context.Request.Path.ToLower().IndexOf("l_k8sc") >= 0)))) || ((((this.Context.Request.Path.ToLower().IndexOf("l_pcdd") >= 0) || (this.Context.Request.Path.ToLower().IndexOf("l_xyft5") >= 0)) || ((this.Context.Request.Path.ToLower().IndexOf("l_pkbjl") >= 0) || (this.Context.Request.Path.ToLower().IndexOf("l_jscar") >= 0))) || (((this.Context.Request.Path.ToLower().IndexOf("l_speed5") >= 0) || (this.Context.Request.Path.ToLower().IndexOf("l_jscqsc") >= 0)) || ((this.Context.Request.Path.ToLower().IndexOf("l_jspk10") >= 0) || (this.Context.Request.Path.ToLower().IndexOf("l_jssfc") >= 0))))) || ((((this.Context.Request.Path.ToLower().IndexOf("l_jsft2") >= 0) || (this.Context.Request.Path.ToLower().IndexOf("l_car168") >= 0)) || ((this.Context.Request.Path.ToLower().IndexOf("l_ssc168") >= 0) || (this.Context.Request.Path.ToLower().IndexOf("l_vrcar") >= 0))) || (((this.Context.Request.Path.ToLower().IndexOf("l_vrssc") >= 0) || (this.Context.Request.Path.ToLower().IndexOf("l_xyftoa") >= 0)) || (this.Context.Request.Path.ToLower().IndexOf("l_xyftsg") >= 0)))) || (this.Context.Request.Path.ToLower().IndexOf("l_happycar") >= 0))
            {
                string str = LSRequest.qq("lid");
                string str2 = "";
                int num = Convert.ToInt32(str);
                DataTable currentPhase = null;
                switch (num)
                {
                    case 0:
                        currentPhase = CallBLL.cz_phase_kl10_bll.IsPhaseClose();
                        str2 = "L_KL10";
                        break;

                    case 1:
                        currentPhase = CallBLL.cz_phase_cqsc_bll.IsPhaseClose();
                        str2 = "L_CQSC";
                        break;

                    case 2:
                        currentPhase = CallBLL.cz_phase_pk10_bll.IsPhaseClose();
                        str2 = "L_PK10";
                        break;

                    case 3:
                        currentPhase = CallBLL.cz_phase_xync_bll.IsPhaseClose();
                        str2 = "L_XYNC";
                        break;

                    case 4:
                        currentPhase = CallBLL.cz_phase_jsk3_bll.IsPhaseClose();
                        str2 = "L_K3";
                        break;

                    case 5:
                        currentPhase = CallBLL.cz_phase_kl8_bll.IsPhaseClose();
                        str2 = "L_KL8";
                        break;

                    case 6:
                        currentPhase = CallBLL.cz_phase_k8sc_bll.IsPhaseClose();
                        str2 = "L_K8SC";
                        break;

                    case 7:
                        currentPhase = CallBLL.cz_phase_pcdd_bll.IsPhaseClose();
                        str2 = "L_PCDD";
                        break;

                    case 8:
                        currentPhase = CallBLL.cz_phase_pkbjl_bll.IsPhaseClose();
                        str2 = "L_PKBJL";
                        break;

                    case 9:
                        currentPhase = CallBLL.cz_phase_xyft5_bll.IsPhaseClose();
                        str2 = "L_XYFT5";
                        break;

                    case 10:
                        currentPhase = CallBLL.cz_phase_jscar_bll.IsPhaseClose();
                        str2 = "L_JSCAR";
                        break;

                    case 11:
                        currentPhase = CallBLL.cz_phase_speed5_bll.IsPhaseClose();
                        str2 = "L_SPEED5";
                        break;

                    case 12:
                        currentPhase = CallBLL.cz_phase_jspk10_bll.IsPhaseClose();
                        str2 = "L_JSPK10";
                        break;

                    case 13:
                        currentPhase = CallBLL.cz_phase_jscqsc_bll.IsPhaseClose();
                        str2 = "L_JSCQSC";
                        break;

                    case 14:
                        currentPhase = CallBLL.cz_phase_jssfc_bll.IsPhaseClose();
                        str2 = "L_JSSFC";
                        break;

                    case 15:
                        currentPhase = CallBLL.cz_phase_jsft2_bll.IsPhaseClose();
                        str2 = "L_JSFT2";
                        break;

                    case 0x10:
                        currentPhase = CallBLL.cz_phase_car168_bll.IsPhaseClose();
                        str2 = "L_CAR168";
                        break;

                    case 0x11:
                        currentPhase = CallBLL.cz_phase_ssc168_bll.IsPhaseClose();
                        str2 = "L_SSC168";
                        break;

                    case 0x12:
                        currentPhase = CallBLL.cz_phase_vrcar_bll.IsPhaseClose();
                        str2 = "L_VRCAR";
                        break;

                    case 0x13:
                        currentPhase = CallBLL.cz_phase_vrssc_bll.IsPhaseClose();
                        str2 = "L_VRSSC";
                        break;

                    case 20:
                        currentPhase = CallBLL.cz_phase_xyftoa_bll.IsPhaseClose();
                        str2 = "L_XYFTOA";
                        break;

                    case 0x15:
                        currentPhase = CallBLL.cz_phase_xyftsg_bll.IsPhaseClose();
                        str2 = "L_XYFTSG";
                        break;

                    case 0x16:
                        currentPhase = CallBLL.cz_phase_happycar_bll.IsPhaseClose();
                        str2 = "L_HAPPYCAR";
                        break;

                    case 100:
                        currentPhase = CallBLL.cz_phase_six_bll.GetCurrentPhase("1");
                        str2 = "L_SIX";
                        break;
                }
                DataRow[] rowArray = this.GetLotteryList().Select(string.Format("id={0}", num));
                if (!num.Equals(100))
                {
                    if (currentPhase.Rows[0]["isopen"].ToString().Equals("0"))
                    {
                        if (this.WebModelView.Equals(1))
                        {
                            base.Response.Redirect(string.Format("/noopen.aspx?lid={0}&path={1}", num, str2), true);
                        }
                        else
                        {
                            base.Response.Redirect(string.Format("/OldWeb/noopen.aspx?lid={0}&path={1}", num, str2), true);
                        }
                        base.Response.End();
                    }
                }
                else
                {
                    cz_phase_six _six = CallBLL.cz_phase_six_bll.GetCurrentPhase();
                    DateTime now = DateTime.Now;
                    string str3 = "n";
                    DateTime time2 = Convert.ToDateTime(_six.get_stop_date());
                    DateTime time3 = Convert.ToDateTime(_six.get_sn_stop_date());
                    if (time2 >= now)
                    {
                        str3 = "y";
                    }
                    if (time3 >= now)
                    {
                        str3 = "y";
                    }
                    if (_six.get_is_closed().ToString().Equals("1") || str3.Equals("n"))
                    {
                        if (this.WebModelView.Equals(1))
                        {
                            base.Response.Redirect(string.Format("/noopen.aspx?lid={0}&path={1}", num, str2), true);
                        }
                        else
                        {
                            base.Response.Redirect(string.Format("/OldWeb/noopen.aspx?lid={0}&path={1}", num, str2), true);
                        }
                        base.Response.End();
                    }
                }
            }
        }

        protected bool IsOpenLottery(int id)
        {
            DataRow[] source = this.GetLotteryList().Select(string.Format(" id={0} ", id));
            return ((source != null) && (source.Count<DataRow>() > 0));
        }

        protected bool IsOpenLotteryIsNight(int id)
        {
            DataRow[] source = this.GetLotteryList().Select(string.Format(" id={0} and night_flag=1 ", id));
            return ((source != null) && (source.Count<DataRow>() > 0));
        }

        public string LmGroup()
        {
            Dictionary<string, string> cache;
            int num5;
            string str;
            string str2;
            StringBuilder builder = new StringBuilder();
            if (CacheHelper.GetCache("LmGroupCount_Cache") == null)
            {
                int num;
                int num2;
                int num3;
                int num4;
                cache = new Dictionary<string, string>();
                DataTable listKC = CallBLL.cz_lm_group_set_bll.GetListKC(0);
                for (num = 0; num < listKC.Rows.Count; num++)
                {
                    num2 = int.Parse(listKC.Rows[num]["play_id"].ToString());
                    num3 = int.Parse(listKC.Rows[num]["min_num_count"].ToString());
                    num4 = int.Parse(listKC.Rows[num]["max_num_group"].ToString());
                    cache.Add(num2 + "_" + 0, string.Format("{0},{1}", num3, num4));
                }
                DataTable table2 = CallBLL.cz_lm_group_set_bll.GetListKC(3);
                for (num = 0; num < table2.Rows.Count; num++)
                {
                    num2 = int.Parse(table2.Rows[num]["play_id"].ToString());
                    num3 = int.Parse(table2.Rows[num]["min_num_count"].ToString());
                    num4 = int.Parse(table2.Rows[num]["max_num_group"].ToString());
                    cache.Add(num2 + "_" + 3, string.Format("{0},{1}", num3, num4));
                }
                DataTable table3 = CallBLL.cz_lm_group_set_bll.GetListKC(7);
                for (num = 0; num < table3.Rows.Count; num++)
                {
                    num2 = int.Parse(table3.Rows[num]["play_id"].ToString());
                    num3 = int.Parse(table3.Rows[num]["min_num_count"].ToString());
                    num4 = int.Parse(table3.Rows[num]["max_num_group"].ToString());
                    cache.Add(num2 + "_" + 7, string.Format("{0},{1}", num3, num4));
                }
                DataTable table4 = CallBLL.cz_lm_group_set_bll.GetListKC(14);
                for (num = 0; num < table4.Rows.Count; num++)
                {
                    num2 = int.Parse(table4.Rows[num]["play_id"].ToString());
                    num3 = int.Parse(table4.Rows[num]["min_num_count"].ToString());
                    num4 = int.Parse(table4.Rows[num]["max_num_group"].ToString());
                    cache.Add(num2 + "_" + 14, string.Format("{0},{1}", num3, num4));
                }
                DataTable list = CallBLL.cz_lm_group_set_bll.GetList(100);
                for (num = 0; num < list.Rows.Count; num++)
                {
                    num2 = int.Parse(list.Rows[num]["play_id"].ToString());
                    num3 = int.Parse(list.Rows[num]["min_num_count"].ToString());
                    num4 = int.Parse(list.Rows[num]["max_num_group"].ToString());
                    cache.Add(num2 + "_" + 100, string.Format("{0},{1}", num3, num4));
                }
                num5 = 0;
                foreach (KeyValuePair<string, string> pair in cache)
                {
                    str = pair.Value.Split(new char[] { ',' })[0];
                    str2 = pair.Value.Split(new char[] { ',' })[1];
                    if (num5 == (cache.Count - 1))
                    {
                        builder.Append("\"" + pair.Key + "\": {\"minLen\":" + str + ",\"maxLen\":" + str2 + " }");
                    }
                    else
                    {
                        builder.Append("\"" + pair.Key + "\": {\"minLen\":" + str + ",\"maxLen\":" + str2 + " },");
                    }
                    num5++;
                }
                CacheHelper.SetCache("LmGroupCount_Cache", cache);
            }
            else
            {
                cache = CacheHelper.GetCache("LmGroupCount_Cache") as Dictionary<string, string>;
                num5 = 0;
                foreach (KeyValuePair<string, string> pair in cache)
                {
                    str = pair.Value.Split(new char[] { ',' })[0];
                    str2 = pair.Value.Split(new char[] { ',' })[1];
                    if (num5 == (cache.Count - 1))
                    {
                        builder.Append("\"" + pair.Key + "\": {\"minLen\":" + str + ",\"maxLen\":" + str2 + " }");
                    }
                    else
                    {
                        builder.Append("\"" + pair.Key + "\": {\"minLen\":" + str + ",\"maxLen\":" + str2 + " },");
                    }
                    num5++;
                }
            }
            return builder.ToString();
        }

        public static void log_user_reset_password(string u_name, string flagStr)
        {
            add_user_change_log(u_name, 300, u_name, "", "密碼變動" + flagStr, "", "重置新密碼");
        }

        public void MemberPageBase_Load(object sender, EventArgs e)
        {
            DateTime now;
            cz_stat_online _online;
            cz_stat_online _online2;
            if (HttpContext.Current.Session["user_name"] == null)
            {
                this.Session.Abandon();
                base.Response.Write("<script>top.location.href='/'</script>");
                base.Response.End();
            }
            if (((this.Session["Session_LoginSystem_Flag"]) != "LoginSystem_NewWeb") && ((this.Session["Session_LoginSystem_Flag"]) != "LoginSystem_OldWeb"))
            {
                this.Session.Abandon();
                base.Response.Write("<script>top.location.href='/'</script>");
                base.Response.End();
            }
            this.WebModelView = FileCacheHelper.get_GetWebModelView();
            if (this.WebModelView.Equals(1))
            {
                this.UserCurrentSkin = this.GetUserModelInfo.get_u_skin();
            }
            else
            {
                this.UserCurrentSkin = "Yellow";
            }
            this.ValidWebModelView();
            string str = HttpContext.Current.Session["user_name"].ToString();
            if (FileCacheHelper.get_RedisStatOnline().Equals(1))
            {
                if (this.Context.Request.Path.ToLower().IndexOf("resetpasswd1.aspx") > 0)
                {
                    now = DateTime.Now;
                    _online = new cz_stat_online();
                    _online.set_u_name(str);
                    _online.set_is_out(0);
                    _online.set_u_type(this.GetUserModelInfo.get_u_type());
                    _online.set_ip(LSRequest.GetIP());
                    _online.set_first_time(new DateTime?(now));
                    _online.set_last_time(new DateTime?(now));
                    CallBLL.redisHelper.HashSet<cz_stat_online>("useronline:list", str, _online);
                }
                if (CallBLL.redisHelper.HashExists("useronline:list", str))
                {
                    _online2 = CallBLL.redisHelper.HashGet<cz_stat_online>("useronline:list", str);
                    if ((_online2 != null) && _online2.get_is_out().Equals(1))
                    {
                        this.Session.Abandon();
                        base.Response.Write("<script>top.location.href='/'</script>");
                        base.Response.End();
                    }
                }
                if (PageBase.IsNeedPopBrower(str))
                {
                    this.Session.Abandon();
                    base.Response.Write("<script>top.location.href='/'</script>");
                    base.Response.End();
                }
            }
            else if (FileCacheHelper.get_RedisStatOnline().Equals(2))
            {
                RedisClient client;
                if (this.Context.Request.Path.ToLower().IndexOf("resetpasswd1.aspx") > 0)
                {
                    now = DateTime.Now;
                    _online = new cz_stat_online();
                    _online.set_u_name(str);
                    _online.set_is_out(0);
                    _online.set_u_type(this.GetUserModelInfo.get_u_type());
                    _online.set_ip(LSRequest.GetIP());
                    _online.set_first_time(new DateTime?(now));
                    _online.set_last_time(new DateTime?(now));
                    using (client = new RedisClient(RedisConnectSplit.get_RedisIP(), RedisConnectSplit.get_RedisPort(), RedisConnectSplit.get_RedisPassword(), (long) FileCacheHelper.get_GetRedisDBIndex()))
                    {
                        client.set_ConnectTimeout(int.Parse(RedisConnectSplit.get_RedisConnectTimeout()));
                        client.SetEntryInHash("useronline:list", str, JsonHandle.ObjectToJson(_online));
                    }
                }
                using (client = new RedisClient(RedisConnectSplit.get_RedisIP(), RedisConnectSplit.get_RedisPort(), RedisConnectSplit.get_RedisPassword(), (long) FileCacheHelper.get_GetRedisDBIndex()))
                {
                    client.set_ConnectTimeout(int.Parse(RedisConnectSplit.get_RedisConnectTimeout()));
                    if (client.HashContainsEntry("useronline:list", str))
                    {
                        _online2 = JsonHandle.JsonToObject<cz_stat_online>(client.GetValueFromHash("useronline:list", str)) as cz_stat_online;
                        if ((_online2 != null) && _online2.get_is_out().Equals(1))
                        {
                            this.Session.Abandon();
                            base.Response.Write("<script>top.location.href='/'</script>");
                            base.Response.End();
                        }
                    }
                }
                if (PageBase.IsNeedPopBrower(str))
                {
                    this.Session.Abandon();
                    base.Response.Write("<script>top.location.href='/'</script>");
                    base.Response.End();
                }
            }
            else if (base.IsUserOut(str) || PageBase.IsNeedPopBrower(str))
            {
                this.Session.Abandon();
                base.Response.Write("<script>top.location.href='/'</script>");
                base.Response.End();
            }
            this.ForcedModifyPassword();
            this.UserCurrentLottery();
            this.RedirectReport();
            this.IsOpenLottery();
        }

        public string PageList(int size, int pageCount, int currentPage, string[] FieldName, string[] FieldValue, int rcount)
        {
            int num;
            string str6;
            string str = "";
            for (num = 0; num < FieldName.Length; num++)
            {
                str6 = str;
                str = str6 + "&" + FieldName[num].ToString() + "=" + FieldValue[num].ToString();
            }
            int num2 = 5;
            int num3 = 1;
            string str2 = "";
            int num4 = size;
            pageCount = (pageCount == 0) ? 1 : pageCount;
            currentPage = (currentPage == 0) ? 1 : currentPage;
            if (rcount == 0)
            {
                currentPage = 0;
                pageCount = 0;
            }
            if (currentPage > pageCount)
            {
                currentPage = pageCount;
            }
            str2 = string.Concat(new object[] { "<span id='pager'>共 ", rcount, " 條記錄  分頁：", currentPage.ToString(), "/", pageCount.ToString(), "頁&nbsp;&nbsp;&nbsp;" });
            if ((currentPage - num2) < 2)
            {
                num3 = 1;
            }
            else
            {
                num3 = currentPage - num2;
            }
            int num5 = pageCount;
            if ((currentPage + num2) >= pageCount)
            {
                num5 = pageCount;
            }
            else
            {
                num5 = currentPage + num2;
            }
            if (num3 == 1)
            {
                if ((currentPage == 1) || (currentPage == 0))
                {
                    str2 = str2 + "上一頁";
                }
                else
                {
                    str6 = str2 + "<a class='redLink' href='?page=1" + str + "' title='首頁'>首頁</a>&nbsp;&nbsp;";
                    str2 = str6 + "<a class='redLink' href='?page=" + Convert.ToString((int) (currentPage - 1)) + str + "' title='上頁'>上一頁</a>";
                }
            }
            else
            {
                str6 = str2 + "<a class='redLink' href='?page=1" + str + "' title='首頁'>首頁</a>&nbsp;&nbsp;";
                str2 = str6 + "<a class='redLink' href='?page=" + Convert.ToString((int) (currentPage - 1)) + str + "' title='上頁'>上一頁</a>...";
            }
            str2 = str2 + "『";
            for (num = num3; num <= num5; num++)
            {
                if (num == currentPage)
                {
                    str2 = str2 + "<span class='font_c'>" + num.ToString() + "</span>&nbsp;";
                }
                else
                {
                    str6 = str2;
                    str2 = str6 + "<a class='redLink' href='?page=" + num.ToString() + str + "' title='第" + num.ToString() + "頁'>" + num.ToString() + "</a>&nbsp;";
                }
                if (num == pageCount)
                {
                    break;
                }
            }
            str2 = str2 + "』";
            if (num5 == pageCount)
            {
                if (pageCount == currentPage)
                {
                    str2 = str2 + "下一頁";
                }
                else
                {
                    str6 = str2;
                    str6 = str6 + "<a class='redLink' href='?page=" + Convert.ToString((int) (currentPage + 1)) + str + "' title='下頁'>下一頁</a>";
                    str2 = str6 + "&nbsp;&nbsp;<a class='redLink' href='?page=" + pageCount.ToString() + str + "' title='尾頁'>尾頁</a>";
                }
            }
            else
            {
                str6 = str2;
                str6 = str6 + "...<a class='redLink' href='?page=" + Convert.ToString((int) (currentPage + 1)) + str + "' title='下頁'>下一頁</a>";
                str2 = str6 + "&nbsp;&nbsp;<a class='redLink' href='?page=" + pageCount.ToString() + str + "' title='最後一頁'>尾頁</a>";
            }
            str2 = (str2 + "<input type=\"hidden\" value=\"" + currentPage.ToString() + "\" id=\"page\"></span>") + "<input type=\"hidden\" value=\"" + str + "\" id=\"hdnPString\"></span>";
            StringBuilder builder = new StringBuilder();
            string str3 = @"javascript:  var txtPagerValue=document.getElementById('txtPager').value; var regs = /^\d+$/; if(!regs.test(txtPagerValue)){alert('輸入錯誤');document.getElementById('txtPager').focus();return false;};var hdnPStringValue=document.getElementById('hdnPString').value; location.href='?page='+txtPagerValue+hdnPStringValue;";
            string str4 = @"javascript: if (event.keyCode ==13){ var txtPagerValue=document.getElementById('txtPager').value; var regs = /^\d+$/; if(!regs.test(txtPagerValue)){alert('輸入錯誤');document.getElementById('txtPager').focus();return false;};var hdnPStringValue=document.getElementById('hdnPString').value; location.href='?page='+txtPagerValue+hdnPStringValue;};";
            builder.AppendFormat("&nbsp;&nbsp;<input type='text' value='{0}' name='txtPager' id='txtPager' class='GOtext' onkeydown=\"{1}\" style=\"display:inline-block;vertical-align:middle;margin-bottom:2px;\" />", currentPage, str4);
            builder.AppendFormat("<input type='button' class='GObtn' onclick=\"{0}\" id=\"btnPager\" style=\"display:inline-block;vertical-align:middle;margin-bottom:2px;\" />", str3);
            return (str2 + builder.ToString());
        }

        protected void RedirectReport()
        {
            int num = 1;
            if (HttpContext.Current.Session["user_state"].ToString().Equals(num.ToString()) && (((((((this.Context.Request.Path.ToLower().IndexOf("l_six") > 0) || (this.Context.Request.Path.ToLower().IndexOf("l_cqsc") > 0)) || ((this.Context.Request.Path.ToLower().IndexOf("l_k3") > 0) || (this.Context.Request.Path.ToLower().IndexOf("l_kl10") > 0))) || (((this.Context.Request.Path.ToLower().IndexOf("l_kl8") > 0) || (this.Context.Request.Path.ToLower().IndexOf("l_pk10") > 0)) || ((this.Context.Request.Path.ToLower().IndexOf("l_xync") > 0) || (this.Context.Request.Path.ToLower().IndexOf("l_k8sc") > 0)))) || ((((this.Context.Request.Path.ToLower().IndexOf("l_pcdd") > 0) || (this.Context.Request.Path.ToLower().IndexOf("l_xyft5") > 0)) || ((this.Context.Request.Path.ToLower().IndexOf("l_pkbjl") > 0) || (this.Context.Request.Path.ToLower().IndexOf("l_jscar") > 0))) || (((this.Context.Request.Path.ToLower().IndexOf("l_speed5") > 0) || (this.Context.Request.Path.ToLower().IndexOf("l_jscqsc") > 0)) || ((this.Context.Request.Path.ToLower().IndexOf("l_jspk10") > 0) || (this.Context.Request.Path.ToLower().IndexOf("l_jssfc") > 0))))) || ((((this.Context.Request.Path.ToLower().IndexOf("l_jsft2") > 0) || (this.Context.Request.Path.ToLower().IndexOf("l_car168") > 0)) || ((this.Context.Request.Path.ToLower().IndexOf("l_ssc168") > 0) || (this.Context.Request.Path.ToLower().IndexOf("l_vrcar") > 0))) || (((this.Context.Request.Path.ToLower().IndexOf("l_vrssc") > 0) || (this.Context.Request.Path.ToLower().IndexOf("l_xyftoa") > 0)) || (this.Context.Request.Path.ToLower().IndexOf("l_xyftsg") > 0)))) || (this.Context.Request.Path.ToLower().IndexOf("l_happycar") > 0)))
            {
                string str = LSRequest.qq("lid");
                if (this.WebModelView.Equals(1))
                {
                    base.Response.Redirect(string.Format("/ReportBill/ReportPage.aspx?lid={0}", str), true);
                }
                else
                {
                    base.Response.Redirect(string.Format("/OldWeb/ReportBill/ReportPage.aspx?lid={0}", str), true);
                }
                base.Response.End();
            }
        }

        public void SetRateByUserObject(int masterid, cz_userinfo_session uModel)
        {
            if (masterid == 1)
            {
                uModel.set_begin_six("yes");
            }
            else
            {
                uModel.set_begin_kc("yes");
            }
        }

        public void SetUserDrawback_car168(DataTable dataTable)
        {
            if (CacheHelper.GetCache("kc_drawback_FileCacheKey_car168" + this.Session["user_name"].ToString()) == null)
            {
                CacheHelper.SetPublicFileCache("kc_drawback_FileCacheKey_car168" + this.Session["user_name"].ToString(), dataTable, PageBase.GetPublicForderPath(FileCacheHelper.get_Drawback_Caches_CAR168()));
            }
        }

        public void SetUserDrawback_car168(DataTable dataTable, string play_id)
        {
            if (CacheHelper.GetCache("kc_drawback_FileCacheKey_car168" + play_id + this.Session["user_name"].ToString()) == null)
            {
                CacheHelper.SetPublicFileCache("kc_drawback_FileCacheKey_car168" + play_id + this.Session["user_name"].ToString(), dataTable, PageBase.GetPublicForderPath(FileCacheHelper.get_Drawback_Caches_CAR168()));
            }
        }

        public void SetUserDrawback_cqsc(DataTable dataTable)
        {
            if (CacheHelper.GetCache("kc_drawback_FileCacheKey_cqsc" + this.Session["user_name"].ToString()) == null)
            {
                CacheHelper.SetPublicFileCache("kc_drawback_FileCacheKey_cqsc" + this.Session["user_name"].ToString(), dataTable, PageBase.GetPublicForderPath(FileCacheHelper.get_Drawback_Caches_CQSC()));
            }
        }

        public void SetUserDrawback_cqsc(DataTable dataTable, string play_id)
        {
            if (CacheHelper.GetCache("kc_drawback_FileCacheKey_cqsc" + play_id + this.Session["user_name"].ToString()) == null)
            {
                CacheHelper.SetPublicFileCache("kc_drawback_FileCacheKey_cqsc" + play_id + this.Session["user_name"].ToString(), dataTable, PageBase.GetPublicForderPath(FileCacheHelper.get_Drawback_Caches_CQSC()));
            }
        }

        public void SetUserDrawback_happycar(DataTable dataTable)
        {
            if (CacheHelper.GetCache("kc_drawback_FileCacheKey_happycar" + this.Session["user_name"].ToString()) == null)
            {
                CacheHelper.SetPublicFileCache("kc_drawback_FileCacheKey_happycar" + this.Session["user_name"].ToString(), dataTable, PageBase.GetPublicForderPath(FileCacheHelper.get_Drawback_Caches_HAPPYCAR()));
            }
        }

        public void SetUserDrawback_happycar(DataTable dataTable, string play_id)
        {
            if (CacheHelper.GetCache("kc_drawback_FileCacheKey_happycar" + play_id + this.Session["user_name"].ToString()) == null)
            {
                CacheHelper.SetPublicFileCache("kc_drawback_FileCacheKey_happycar" + play_id + this.Session["user_name"].ToString(), dataTable, PageBase.GetPublicForderPath(FileCacheHelper.get_Drawback_Caches_HAPPYCAR()));
            }
        }

        public void SetUserDrawback_jscar(DataTable dataTable)
        {
            if (CacheHelper.GetCache("kc_drawback_FileCacheKey_jscar" + this.Session["user_name"].ToString()) == null)
            {
                CacheHelper.SetPublicFileCache("kc_drawback_FileCacheKey_jscar" + this.Session["user_name"].ToString(), dataTable, PageBase.GetPublicForderPath(FileCacheHelper.get_Drawback_Caches_JSCAR()));
            }
        }

        public void SetUserDrawback_jscar(DataTable dataTable, string play_id)
        {
            if (CacheHelper.GetCache("kc_drawback_FileCacheKey_jscar" + play_id + this.Session["user_name"].ToString()) == null)
            {
                CacheHelper.SetPublicFileCache("kc_drawback_FileCacheKey_jscar" + play_id + this.Session["user_name"].ToString(), dataTable, PageBase.GetPublicForderPath(FileCacheHelper.get_Drawback_Caches_JSCAR()));
            }
        }

        public void SetUserDrawback_jscqsc(DataTable dataTable)
        {
            if (CacheHelper.GetCache("kc_drawback_FileCacheKey_jscqsc" + this.Session["user_name"].ToString()) == null)
            {
                CacheHelper.SetPublicFileCache("kc_drawback_FileCacheKey_jscqsc" + this.Session["user_name"].ToString(), dataTable, PageBase.GetPublicForderPath(FileCacheHelper.get_Drawback_Caches_JSCQSC()));
            }
        }

        public void SetUserDrawback_jscqsc(DataTable dataTable, string play_id)
        {
            if (CacheHelper.GetCache("kc_drawback_FileCacheKey_jscqsc" + play_id + this.Session["user_name"].ToString()) == null)
            {
                CacheHelper.SetPublicFileCache("kc_drawback_FileCacheKey_jscqsc" + play_id + this.Session["user_name"].ToString(), dataTable, PageBase.GetPublicForderPath(FileCacheHelper.get_Drawback_Caches_JSCQSC()));
            }
        }

        public void SetUserDrawback_jsft2(DataTable dataTable)
        {
            if (CacheHelper.GetCache("kc_drawback_FileCacheKey_jsft2" + this.Session["user_name"].ToString()) == null)
            {
                CacheHelper.SetPublicFileCache("kc_drawback_FileCacheKey_jsft2" + this.Session["user_name"].ToString(), dataTable, PageBase.GetPublicForderPath(FileCacheHelper.get_Drawback_Caches_JSFT2()));
            }
        }

        public void SetUserDrawback_jsft2(DataTable dataTable, string play_id)
        {
            if (CacheHelper.GetCache("kc_drawback_FileCacheKey_jsft2" + play_id + this.Session["user_name"].ToString()) == null)
            {
                CacheHelper.SetPublicFileCache("kc_drawback_FileCacheKey_jsft2" + play_id + this.Session["user_name"].ToString(), dataTable, PageBase.GetPublicForderPath(FileCacheHelper.get_Drawback_Caches_JSFT2()));
            }
        }

        public void SetUserDrawback_jsk3(DataTable dataTable)
        {
            if (CacheHelper.GetCache("kc_drawback_FileCacheKey_jsk3" + this.Session["user_name"].ToString()) == null)
            {
                CacheHelper.SetPublicFileCache("kc_drawback_FileCacheKey_jsk3" + this.Session["user_name"].ToString(), dataTable, PageBase.GetPublicForderPath(FileCacheHelper.get_Drawback_Caches_JSK3()));
            }
        }

        public void SetUserDrawback_jsk3(DataTable dataTable, string play_id)
        {
            if (CacheHelper.GetCache("kc_drawback_FileCacheKey_jsk3" + play_id + this.Session["user_name"].ToString()) == null)
            {
                CacheHelper.SetPublicFileCache("kc_drawback_FileCacheKey_jsk3" + play_id + this.Session["user_name"].ToString(), dataTable, PageBase.GetPublicForderPath(FileCacheHelper.get_Drawback_Caches_JSK3()));
            }
        }

        public void SetUserDrawback_jspk10(DataTable dataTable)
        {
            if (CacheHelper.GetCache("kc_drawback_FileCacheKey_jspk10" + this.Session["user_name"].ToString()) == null)
            {
                CacheHelper.SetPublicFileCache("kc_drawback_FileCacheKey_jspk10" + this.Session["user_name"].ToString(), dataTable, PageBase.GetPublicForderPath(FileCacheHelper.get_Drawback_Caches_JSPK10()));
            }
        }

        public void SetUserDrawback_jspk10(DataTable dataTable, string play_id)
        {
            if (CacheHelper.GetCache("kc_drawback_FileCacheKey_jspk10" + play_id + this.Session["user_name"].ToString()) == null)
            {
                CacheHelper.SetPublicFileCache("kc_drawback_FileCacheKey_jspk10" + play_id + this.Session["user_name"].ToString(), dataTable, PageBase.GetPublicForderPath(FileCacheHelper.get_Drawback_Caches_JSPK10()));
            }
        }

        public void SetUserDrawback_jssfc(DataTable dataTable)
        {
            if (CacheHelper.GetCache("kc_drawback_FileCacheKey_jssfc" + this.Session["user_name"].ToString()) == null)
            {
                CacheHelper.SetPublicFileCache("kc_drawback_FileCacheKey_jssfc" + this.Session["user_name"].ToString(), dataTable, PageBase.GetPublicForderPath(FileCacheHelper.get_Drawback_Caches_JSSFC()));
            }
        }

        public void SetUserDrawback_jssfc(DataTable dataTable, string play_id)
        {
            if (CacheHelper.GetCache("kc_drawback_FileCacheKey_jssfc" + play_id + this.Session["user_name"].ToString()) == null)
            {
                CacheHelper.SetPublicFileCache("kc_drawback_FileCacheKey_jssfc" + play_id + this.Session["user_name"].ToString(), dataTable, PageBase.GetPublicForderPath(FileCacheHelper.get_Drawback_Caches_JSSFC()));
            }
        }

        public void SetUserDrawback_k8sc(DataTable dataTable)
        {
            if (CacheHelper.GetCache("kc_drawback_FileCacheKey_k8sc" + this.Session["user_name"].ToString()) == null)
            {
                CacheHelper.SetPublicFileCache("kc_drawback_FileCacheKey_k8sc" + this.Session["user_name"].ToString(), dataTable, PageBase.GetPublicForderPath(FileCacheHelper.get_Drawback_Caches_K8SC()));
            }
        }

        public void SetUserDrawback_k8sc(DataTable dataTable, string play_id)
        {
            if (CacheHelper.GetCache("kc_drawback_FileCacheKey_k8sc" + play_id + this.Session["user_name"].ToString()) == null)
            {
                CacheHelper.SetPublicFileCache("kc_drawback_FileCacheKey_k8sc" + play_id + this.Session["user_name"].ToString(), dataTable, PageBase.GetPublicForderPath(FileCacheHelper.get_Drawback_Caches_K8SC()));
            }
        }

        public void SetUserDrawback_kl10(DataTable dataTable)
        {
            if (CacheHelper.GetCache("kc_drawback_FileCacheKey_kl10" + this.Session["user_name"].ToString()) == null)
            {
                CacheHelper.SetPublicFileCache("kc_drawback_FileCacheKey_kl10" + this.Session["user_name"].ToString(), dataTable, PageBase.GetPublicForderPath(FileCacheHelper.get_Drawback_Caches_KL10()));
            }
        }

        public void SetUserDrawback_kl10(DataTable dataTable, string play_id)
        {
            if (CacheHelper.GetCache("kc_drawback_FileCacheKey_kl10" + play_id + this.Session["user_name"].ToString()) == null)
            {
                CacheHelper.SetPublicFileCache("kc_drawback_FileCacheKey_kl10" + play_id + this.Session["user_name"].ToString(), dataTable, PageBase.GetPublicForderPath(FileCacheHelper.get_Drawback_Caches_KL10()));
            }
        }

        public void SetUserDrawback_kl8(DataTable dataTable)
        {
            if (CacheHelper.GetCache("kc_drawback_FileCacheKey_kl8" + this.Session["user_name"].ToString()) == null)
            {
                CacheHelper.SetPublicFileCache("kc_drawback_FileCacheKey_kl8" + this.Session["user_name"].ToString(), dataTable, PageBase.GetPublicForderPath(FileCacheHelper.get_Drawback_Caches_KL8()));
            }
        }

        public void SetUserDrawback_kl8(DataTable dataTable, string play_id)
        {
            if (CacheHelper.GetCache("kc_drawback_FileCacheKey_kl8" + play_id + this.Session["user_name"].ToString()) == null)
            {
                CacheHelper.SetPublicFileCache("kc_drawback_FileCacheKey_kl8" + play_id + this.Session["user_name"].ToString(), dataTable, PageBase.GetPublicForderPath(FileCacheHelper.get_Drawback_Caches_KL8()));
            }
        }

        public void SetUserDrawback_pcdd(DataTable dataTable)
        {
            if (CacheHelper.GetCache("kc_drawback_FileCacheKey_pcdd" + this.Session["user_name"].ToString()) == null)
            {
                CacheHelper.SetPublicFileCache("kc_drawback_FileCacheKey_pcdd" + this.Session["user_name"].ToString(), dataTable, PageBase.GetPublicForderPath(FileCacheHelper.get_Drawback_Caches_PCDD()));
            }
        }

        public void SetUserDrawback_pcdd(DataTable dataTable, string play_id)
        {
            if (CacheHelper.GetCache("kc_drawback_FileCacheKey_pcdd" + play_id + this.Session["user_name"].ToString()) == null)
            {
                CacheHelper.SetPublicFileCache("kc_drawback_FileCacheKey_pcdd" + play_id + this.Session["user_name"].ToString(), dataTable, PageBase.GetPublicForderPath(FileCacheHelper.get_Drawback_Caches_PCDD()));
            }
        }

        public void SetUserDrawback_pk10(DataTable dataTable)
        {
            if (CacheHelper.GetCache("kc_drawback_FileCacheKey_pk10" + this.Session["user_name"].ToString()) == null)
            {
                CacheHelper.SetPublicFileCache("kc_drawback_FileCacheKey_pk10" + this.Session["user_name"].ToString(), dataTable, PageBase.GetPublicForderPath(FileCacheHelper.get_Drawback_Caches_PK10()));
            }
        }

        public void SetUserDrawback_pk10(DataTable dataTable, string play_id)
        {
            if (CacheHelper.GetCache("kc_drawback_FileCacheKey_pk10" + play_id + this.Session["user_name"].ToString()) == null)
            {
                CacheHelper.SetPublicFileCache("kc_drawback_FileCacheKey_pk10" + play_id + this.Session["user_name"].ToString(), dataTable, PageBase.GetPublicForderPath(FileCacheHelper.get_Drawback_Caches_PK10()));
            }
        }

        public void SetUserDrawback_pkbjl(DataTable dataTable)
        {
            if (CacheHelper.GetCache("kc_drawback_FileCacheKey_pkbjl" + this.Session["user_name"].ToString()) == null)
            {
                CacheHelper.SetPublicFileCache("kc_drawback_FileCacheKey_pkbjl" + this.Session["user_name"].ToString(), dataTable, PageBase.GetPublicForderPath(FileCacheHelper.get_Drawback_Caches_PKBJL()));
            }
        }

        public void SetUserDrawback_pkbjl(DataTable dataTable, string play_id)
        {
            if (CacheHelper.GetCache("kc_drawback_FileCacheKey_pkbjl" + play_id + this.Session["user_name"].ToString()) == null)
            {
                CacheHelper.SetPublicFileCache("kc_drawback_FileCacheKey_pkbjl" + play_id + this.Session["user_name"].ToString(), dataTable, PageBase.GetPublicForderPath(FileCacheHelper.get_Drawback_Caches_PKBJL()));
            }
        }

        public void SetUserDrawback_six(DataSet ds)
        {
            if (CacheHelper.GetCache("six_drawback_FileCacheKey" + HttpContext.Current.Session["user_name"].ToString()) == null)
            {
                CacheHelper.SetPublicFileCache("six_drawback_FileCacheKey" + HttpContext.Current.Session["user_name"].ToString(), ds, PageBase.GetPublicForderPath(FileCacheHelper.get_Drawback_Caches_SIX()));
            }
        }

        public void SetUserDrawback_six(DataSet ds, string play_id)
        {
            if (CacheHelper.GetCache("six_drawback_FileCacheKey" + play_id + HttpContext.Current.Session["user_name"].ToString()) == null)
            {
                CacheHelper.SetPublicFileCache("six_drawback_FileCacheKey" + play_id + HttpContext.Current.Session["user_name"].ToString(), ds, PageBase.GetPublicForderPath(FileCacheHelper.get_Drawback_Caches_SIX()));
            }
        }

        public void SetUserDrawback_speed5(DataTable dataTable)
        {
            if (CacheHelper.GetCache("kc_drawback_FileCacheKey_speed5" + this.Session["user_name"].ToString()) == null)
            {
                CacheHelper.SetPublicFileCache("kc_drawback_FileCacheKey_speed5" + this.Session["user_name"].ToString(), dataTable, PageBase.GetPublicForderPath(FileCacheHelper.get_Drawback_Caches_SPEED5()));
            }
        }

        public void SetUserDrawback_speed5(DataTable dataTable, string play_id)
        {
            if (CacheHelper.GetCache("kc_drawback_FileCacheKey_speed5" + play_id + this.Session["user_name"].ToString()) == null)
            {
                CacheHelper.SetPublicFileCache("kc_drawback_FileCacheKey_speed5" + play_id + this.Session["user_name"].ToString(), dataTable, PageBase.GetPublicForderPath(FileCacheHelper.get_Drawback_Caches_SPEED5()));
            }
        }

        public void SetUserDrawback_ssc168(DataTable dataTable)
        {
            if (CacheHelper.GetCache("kc_drawback_FileCacheKey_ssc168" + this.Session["user_name"].ToString()) == null)
            {
                CacheHelper.SetPublicFileCache("kc_drawback_FileCacheKey_ssc168" + this.Session["user_name"].ToString(), dataTable, PageBase.GetPublicForderPath(FileCacheHelper.get_Drawback_Caches_SSC168()));
            }
        }

        public void SetUserDrawback_ssc168(DataTable dataTable, string play_id)
        {
            if (CacheHelper.GetCache("kc_drawback_FileCacheKey_ssc168" + play_id + this.Session["user_name"].ToString()) == null)
            {
                CacheHelper.SetPublicFileCache("kc_drawback_FileCacheKey_ssc168" + play_id + this.Session["user_name"].ToString(), dataTable, PageBase.GetPublicForderPath(FileCacheHelper.get_Drawback_Caches_SSC168()));
            }
        }

        public void SetUserDrawback_vrcar(DataTable dataTable)
        {
            if (CacheHelper.GetCache("kc_drawback_FileCacheKey_vrcar" + this.Session["user_name"].ToString()) == null)
            {
                CacheHelper.SetPublicFileCache("kc_drawback_FileCacheKey_vrcar" + this.Session["user_name"].ToString(), dataTable, PageBase.GetPublicForderPath(FileCacheHelper.get_Drawback_Caches_VRCAR()));
            }
        }

        public void SetUserDrawback_vrcar(DataTable dataTable, string play_id)
        {
            if (CacheHelper.GetCache("kc_drawback_FileCacheKey_vrcar" + play_id + this.Session["user_name"].ToString()) == null)
            {
                CacheHelper.SetPublicFileCache("kc_drawback_FileCacheKey_vrcar" + play_id + this.Session["user_name"].ToString(), dataTable, PageBase.GetPublicForderPath(FileCacheHelper.get_Drawback_Caches_VRCAR()));
            }
        }

        public void SetUserDrawback_vrssc(DataTable dataTable)
        {
            if (CacheHelper.GetCache("kc_drawback_FileCacheKey_vrssc" + this.Session["user_name"].ToString()) == null)
            {
                CacheHelper.SetPublicFileCache("kc_drawback_FileCacheKey_vrssc" + this.Session["user_name"].ToString(), dataTable, PageBase.GetPublicForderPath(FileCacheHelper.get_Drawback_Caches_VRSSC()));
            }
        }

        public void SetUserDrawback_vrssc(DataTable dataTable, string play_id)
        {
            if (CacheHelper.GetCache("kc_drawback_FileCacheKey_vrssc" + play_id + this.Session["user_name"].ToString()) == null)
            {
                CacheHelper.SetPublicFileCache("kc_drawback_FileCacheKey_vrssc" + play_id + this.Session["user_name"].ToString(), dataTable, PageBase.GetPublicForderPath(FileCacheHelper.get_Drawback_Caches_VRSSC()));
            }
        }

        public void SetUserDrawback_xyft5(DataTable dataTable)
        {
            if (CacheHelper.GetCache("kc_drawback_FileCacheKey_xyft5" + this.Session["user_name"].ToString()) == null)
            {
                CacheHelper.SetPublicFileCache("kc_drawback_FileCacheKey_xyft5" + this.Session["user_name"].ToString(), dataTable, PageBase.GetPublicForderPath(FileCacheHelper.get_Drawback_Caches_XYFT5()));
            }
        }

        public void SetUserDrawback_xyft5(DataTable dataTable, string play_id)
        {
            if (CacheHelper.GetCache("kc_drawback_FileCacheKey_xyft5" + play_id + this.Session["user_name"].ToString()) == null)
            {
                CacheHelper.SetPublicFileCache("kc_drawback_FileCacheKey_xyft5" + play_id + this.Session["user_name"].ToString(), dataTable, PageBase.GetPublicForderPath(FileCacheHelper.get_Drawback_Caches_XYFT5()));
            }
        }

        public void SetUserDrawback_xyftoa(DataTable dataTable)
        {
            if (CacheHelper.GetCache("kc_drawback_FileCacheKey_xyftoa" + this.Session["user_name"].ToString()) == null)
            {
                CacheHelper.SetPublicFileCache("kc_drawback_FileCacheKey_xyftoa" + this.Session["user_name"].ToString(), dataTable, PageBase.GetPublicForderPath(FileCacheHelper.get_Drawback_Caches_XYFTOA()));
            }
        }

        public void SetUserDrawback_xyftoa(DataTable dataTable, string play_id)
        {
            if (CacheHelper.GetCache("kc_drawback_FileCacheKey_xyftoa" + play_id + this.Session["user_name"].ToString()) == null)
            {
                CacheHelper.SetPublicFileCache("kc_drawback_FileCacheKey_xyftoa" + play_id + this.Session["user_name"].ToString(), dataTable, PageBase.GetPublicForderPath(FileCacheHelper.get_Drawback_Caches_XYFTOA()));
            }
        }

        public void SetUserDrawback_xyftsg(DataTable dataTable)
        {
            if (CacheHelper.GetCache("kc_drawback_FileCacheKey_xyftsg" + this.Session["user_name"].ToString()) == null)
            {
                CacheHelper.SetPublicFileCache("kc_drawback_FileCacheKey_xyftsg" + this.Session["user_name"].ToString(), dataTable, PageBase.GetPublicForderPath(FileCacheHelper.get_Drawback_Caches_XYFTSG()));
            }
        }

        public void SetUserDrawback_xyftsg(DataTable dataTable, string play_id)
        {
            if (CacheHelper.GetCache("kc_drawback_FileCacheKey_xyftsg" + play_id + this.Session["user_name"].ToString()) == null)
            {
                CacheHelper.SetPublicFileCache("kc_drawback_FileCacheKey_xyftsg" + play_id + this.Session["user_name"].ToString(), dataTable, PageBase.GetPublicForderPath(FileCacheHelper.get_Drawback_Caches_XYFTSG()));
            }
        }

        public void SetUserDrawback_xync(DataTable dataTable)
        {
            if (CacheHelper.GetCache("kc_drawback_FileCacheKey_xync" + this.Session["user_name"].ToString()) == null)
            {
                CacheHelper.SetPublicFileCache("kc_drawback_FileCacheKey_xync" + this.Session["user_name"].ToString(), dataTable, PageBase.GetPublicForderPath(FileCacheHelper.get_Drawback_Caches_XYNC()));
            }
        }

        public void SetUserDrawback_xync(DataTable dataTable, string play_id)
        {
            if (CacheHelper.GetCache("kc_drawback_FileCacheKey_xync" + play_id + this.Session["user_name"].ToString()) == null)
            {
                CacheHelper.SetPublicFileCache("kc_drawback_FileCacheKey_xync" + play_id + this.Session["user_name"].ToString(), dataTable, PageBase.GetPublicForderPath(FileCacheHelper.get_Drawback_Caches_XYNC()));
            }
        }

        public void SetUserRate_kc(user_kc_rate kc_rate)
        {
            if ((CacheHelper.GetCache("kc_rate_FileCacheKey" + HttpContext.Current.Session["user_name"].ToString()) == null) || (HttpContext.Current.Session["kc_rate_FileCacheKey" + HttpContext.Current.Session["user_name"].ToString()] == null))
            {
                string str = DateTime.Now.AddHours(-7.0).ToString("yyyy-MM-dd");
                HttpContext.Current.Session[HttpContext.Current.Session["user_name"] + "_begin_kc_date"] = str;
                HttpContext.Current.Session["kc_rate_FileCacheKey" + HttpContext.Current.Session["user_name"].ToString()] = kc_rate;
                CacheHelper.SetPublicFileCache("kc_rate_FileCacheKey" + HttpContext.Current.Session["user_name"].ToString(), kc_rate, PageBase.GetPublicForderPath(base.get_KC_RateCachesFileName()));
            }
        }

        public void SetUserRate_six(user_six_rate six_rate)
        {
            if ((CacheHelper.GetCache("six_rate_FileCacheKey" + HttpContext.Current.Session["user_name"].ToString()) == null) || (HttpContext.Current.Session["six_rate_FileCacheKey" + HttpContext.Current.Session["user_name"].ToString()] == null))
            {
                HttpContext.Current.Session["six_rate_FileCacheKey" + HttpContext.Current.Session["user_name"].ToString()] = six_rate;
                CacheHelper.SetPublicFileCache("six_rate_FileCacheKey" + HttpContext.Current.Session["user_name"].ToString(), six_rate, PageBase.GetPublicForderPath(base.get_SIX_RateCachesFileName()));
            }
        }

        public static void stat_online(string user, string user_type)
        {
            ConcurrentDictionary<string, object> dictionary;
            string str = "Online_Stat";
            string str2 = "Online_Stat_CheckTime";
            string str3 = "online_User_Key";
            string iP = LSRequest.GetIP();
            DateTime now = DateTime.Now;
            if (HttpContext.Current.Application[str2] == null)
            {
                HttpContext.Current.Application.Lock();
                HttpContext.Current.Application[str2] = now;
                HttpContext.Current.Application.UnLock();
            }
            if (HttpContext.Current.Application[str] == null)
            {
                dictionary = new ConcurrentDictionary<string, object>();
                ArrayList list = new ArrayList();
                list.Add(iP);
                list.Add(now);
                dictionary.GetOrAdd(user, list);
                HttpContext.Current.Application.Lock();
                HttpContext.Current.Application[str] = dictionary;
                HttpContext.Current.Application.UnLock();
                insert_online(iP, user, user_type, now, now);
            }
            else
            {
                dictionary = HttpContext.Current.Application[str] as ConcurrentDictionary<string, object>;
                if (dictionary.ContainsKey(user))
                {
                    if (HttpContext.Current.Application[str3] == null)
                    {
                        ConcurrentDictionary<string, object> dictionary2 = new ConcurrentDictionary<string, object>();
                        HttpContext.Current.Application[str3] = dictionary2;
                    }
                    ConcurrentDictionary<string, object> dictionary3 = HttpContext.Current.Application[str3] as ConcurrentDictionary<string, object>;
                    ArrayList infoList = new ArrayList();
                    infoList.Add(iP);
                    infoList.Add(DateTime.Now);
                    dictionary3.AddOrUpdate(user, infoList, (key, oldValue) => infoList);
                    dictionary.AddOrUpdate(user, infoList, (key, oldValue) => infoList);
                }
                else
                {
                    ArrayList list2 = new ArrayList();
                    list2.Add(iP);
                    list2.Add(now);
                    dictionary.GetOrAdd(user, list2);
                    insert_online(iP, user, user_type, now, now);
                }
                if (DateTime.Compare(Convert.ToDateTime(HttpContext.Current.Application[str2].ToString()).AddMinutes(1.0), DateTime.Now) < 0)
                {
                    HttpContext.Current.Application.Lock();
                    HttpContext.Current.Application[str2] = DateTime.Now;
                    HttpContext.Current.Application.UnLock();
                    update_online();
                }
            }
        }

        public static void stat_online_bet(string user, string user_type)
        {
            ConcurrentDictionary<string, object> dictionary;
            string str = "Online_Stat";
            string str2 = "Online_Stat_CheckTime";
            string str3 = "online_User_Key";
            string iP = LSRequest.GetIP();
            DateTime now = DateTime.Now;
            if (HttpContext.Current.Application[str2] == null)
            {
                HttpContext.Current.Application.Lock();
                HttpContext.Current.Application[str2] = now;
                HttpContext.Current.Application.UnLock();
            }
            if (HttpContext.Current.Application[str] == null)
            {
                dictionary = new ConcurrentDictionary<string, object>();
                ArrayList list = new ArrayList();
                list.Add(iP);
                list.Add(now);
                dictionary.GetOrAdd(user, list);
                HttpContext.Current.Application.Lock();
                HttpContext.Current.Application[str] = dictionary;
                HttpContext.Current.Application.UnLock();
                insert_online(iP, user, user_type, now, now);
            }
            else
            {
                dictionary = HttpContext.Current.Application[str] as ConcurrentDictionary<string, object>;
                if (dictionary.ContainsKey(user))
                {
                    if (HttpContext.Current.Application[str3] == null)
                    {
                        ConcurrentDictionary<string, object> dictionary2 = new ConcurrentDictionary<string, object>();
                        HttpContext.Current.Application[str3] = dictionary2;
                    }
                    ConcurrentDictionary<string, object> dictionary3 = HttpContext.Current.Application[str3] as ConcurrentDictionary<string, object>;
                    ArrayList infoList = new ArrayList();
                    infoList.Add(iP);
                    infoList.Add(DateTime.Now);
                    dictionary3.AddOrUpdate(user, infoList, (key, oldValue) => infoList);
                    dictionary.AddOrUpdate(user, infoList, (key, oldValue) => infoList);
                }
                else
                {
                    ArrayList list2 = new ArrayList();
                    list2.Add(iP);
                    list2.Add(now);
                    dictionary.GetOrAdd(user, list2);
                    insert_online(iP, user, user_type, now, now);
                }
            }
        }

        public static void stat_top_online(string loginname)
        {
            string str = "Online_Stat";
            string str2 = "Online_Stat_CheckTime";
            DateTime now = DateTime.Now;
            string str3 = string.Format("update  cz_stat_online  set  first_time = '{0}',last_time= '{1}'  where u_name =@u_name ", now, now);
            SqlParameter[] parameterArray = new SqlParameter[] { new SqlParameter("@u_name", SqlDbType.NVarChar) };
            parameterArray[0].Value = loginname;
            CallBLL.cz_stat_online_bll.executte_sql(str3, parameterArray);
            string str4 = "";
            if (HttpContext.Current.Application[str] == null)
            {
                HttpContext.Current.Application.Lock();
                HttpContext.Current.Application[str2] = now;
                HttpContext.Current.Application.UnLock();
                str4 = string.Format(" select * from cz_stat_top_online with(NOLOCK) where update_time > '{0}' and update_time < '{1}' ", DateTime.Today, DateTime.Today.AddHours(24.0));
                if (CallBLL.cz_stat_top_online_bll.query_sql(str4).Rows.Count <= 0)
                {
                    str4 = string.Format("insert into cz_stat_top_online values({0},'{1}') ", 1, DateTime.Now);
                    CallBLL.cz_stat_top_online_bll.executte_sql(str4);
                }
            }
            else
            {
                int num = 1;
                string str5 = string.Format(" select count(1)  from cz_stat_online with(NOLOCK) where last_time > '{0}' ", now.AddMinutes(-3.0));
                DataTable table2 = CallBLL.cz_stat_online_bll.query_sql(str5, parameterArray);
                if (table2.Rows.Count > 0)
                {
                    num = int.Parse(table2.Rows[0][0].ToString());
                }
                str4 = string.Format("select *  from cz_stat_top_online with(NOLOCK) where update_time > '{0}' and update_time < '{1}' ", DateTime.Today, DateTime.Today.AddHours(24.0));
                DataTable table3 = CallBLL.cz_stat_top_online_bll.query_sql(str4);
                if (table3.Rows.Count > 0)
                {
                    string s = table3.Rows[0]["top_cnt"].ToString();
                    if (num > int.Parse(s))
                    {
                        str4 = string.Format("update  cz_stat_top_online  set top_cnt = {0}, update_time = '{1}' where update_time > '{2}' and update_time < '{3}' ", new object[] { num, now, DateTime.Today, DateTime.Today.AddHours(24.0) });
                        CallBLL.cz_stat_top_online_bll.executte_sql(str4);
                    }
                }
                else
                {
                    str4 = string.Format("insert into cz_stat_top_online values({0},'{1}') ", num, now);
                    CallBLL.cz_stat_top_online_bll.executte_sql(str4);
                }
            }
        }

        public void Un_Bet_Lock(string lottery_type, string fgs_name, string odds_id)
        {
            HttpContext.Current.Application.Lock();
            HttpContext.Current.Application["Bet_" + lottery_type + "_" + fgs_name + "_" + odds_id] = null;
            HttpContext.Current.Application.UnLock();
        }

        public void Un_TenPoker_Lock(string lottery_type, string phase_id, string numTable)
        {
            HttpContext.Current.Application.Lock();
            HttpContext.Current.Application["Poker_" + lottery_type + "_" + phase_id + "_" + numTable] = null;
            HttpContext.Current.Application.UnLock();
        }

        protected static void update_online()
        {
            string str = "online_User_Key";
            HttpContext.Current.Application.Lock();
            ConcurrentDictionary<string, object> dictionary = HttpContext.Current.Application[str] as ConcurrentDictionary<string, object>;
            HttpContext.Current.Application.UnLock();
            List<string> list = new List<string>();
            string str2 = "";
            foreach (KeyValuePair<string, object> pair in dictionary)
            {
                object obj2;
                ArrayList list2 = pair.Value as ArrayList;
                string item = string.Format("update  cz_stat_online set ip='{0}', last_time='{1}' where u_name = '{2}';", list2[0].ToString(), list2[1].ToString(), pair.Key);
                list.Add(item);
                str2 = str2 + item;
                dictionary.TryRemove(pair.Key, out obj2);
            }
            if (list.Count > 0)
            {
                CallBLL.cz_stat_online_bll.executte_sql(str2);
            }
        }

        public static void update_online_user(string u_name)
        {
            string str = string.Format("update  cz_stat_online set  last_time=last_time-0.1 where u_name = @u_name ", new object[0]);
            SqlParameter[] parameterArray = new SqlParameter[] { new SqlParameter("@u_name", SqlDbType.NVarChar) };
            parameterArray[0].Value = u_name;
            CallBLL.cz_stat_online_bll.executte_sql(str, parameterArray);
        }

        public void UserCurrentLottery()
        {
            int num;
            string str = this.Context.Request.Path.ToLower();
            if (this.Context.Request.Path.ToLower().IndexOf("l_six") > -1)
            {
                num = 1;
                CookieHelper.SetCookie("cookiescurrentmlid", num.ToString());
                num = 100;
                CookieHelper.SetCookie("cookiescurrentlid", num.ToString());
            }
            else if (this.Context.Request.Path.ToLower().IndexOf("l_kl10") > -1)
            {
                num = 2;
                CookieHelper.SetCookie("cookiescurrentmlid", num.ToString());
                num = 0;
                CookieHelper.SetCookie("cookiescurrentlid", num.ToString());
            }
            else if (this.Context.Request.Path.ToLower().IndexOf("l_cqsc") > -1)
            {
                num = 2;
                CookieHelper.SetCookie("cookiescurrentmlid", num.ToString());
                num = 1;
                CookieHelper.SetCookie("cookiescurrentlid", num.ToString());
            }
            else if (this.Context.Request.Path.ToLower().IndexOf("l_pk10") > -1)
            {
                num = 2;
                CookieHelper.SetCookie("cookiescurrentmlid", num.ToString());
                num = 2;
                CookieHelper.SetCookie("cookiescurrentlid", num.ToString());
            }
            else if (this.Context.Request.Path.ToLower().IndexOf("l_xync") > -1)
            {
                num = 2;
                CookieHelper.SetCookie("cookiescurrentmlid", num.ToString());
                num = 3;
                CookieHelper.SetCookie("cookiescurrentlid", num.ToString());
            }
            else if (this.Context.Request.Path.ToLower().IndexOf("l_k3") > -1)
            {
                num = 2;
                CookieHelper.SetCookie("cookiescurrentmlid", num.ToString());
                num = 4;
                CookieHelper.SetCookie("cookiescurrentlid", num.ToString());
            }
            else if (this.Context.Request.Path.ToLower().IndexOf("l_kl8") > -1)
            {
                num = 2;
                CookieHelper.SetCookie("cookiescurrentmlid", num.ToString());
                num = 5;
                CookieHelper.SetCookie("cookiescurrentlid", num.ToString());
            }
            else if (this.Context.Request.Path.ToLower().IndexOf("l_k8sc") > -1)
            {
                num = 2;
                CookieHelper.SetCookie("cookiescurrentmlid", num.ToString());
                num = 6;
                CookieHelper.SetCookie("cookiescurrentlid", num.ToString());
            }
            else if (this.Context.Request.Path.ToLower().IndexOf("l_pcdd") > -1)
            {
                num = 2;
                CookieHelper.SetCookie("cookiescurrentmlid", num.ToString());
                num = 7;
                CookieHelper.SetCookie("cookiescurrentlid", num.ToString());
            }
            else if (this.Context.Request.Path.ToLower().IndexOf("l_xyft5") > -1)
            {
                num = 2;
                CookieHelper.SetCookie("cookiescurrentmlid", num.ToString());
                num = 9;
                CookieHelper.SetCookie("cookiescurrentlid", num.ToString());
            }
            else if (this.Context.Request.Path.ToLower().IndexOf("l_pkbjl") > -1)
            {
                num = 2;
                CookieHelper.SetCookie("cookiescurrentmlid", num.ToString());
                num = 8;
                CookieHelper.SetCookie("cookiescurrentlid", num.ToString());
            }
            else if (this.Context.Request.Path.ToLower().IndexOf("l_jscar") > -1)
            {
                num = 2;
                CookieHelper.SetCookie("cookiescurrentmlid", num.ToString());
                num = 10;
                CookieHelper.SetCookie("cookiescurrentlid", num.ToString());
            }
            else if (this.Context.Request.Path.ToLower().IndexOf("l_speed5") > -1)
            {
                num = 2;
                CookieHelper.SetCookie("cookiescurrentmlid", num.ToString());
                num = 11;
                CookieHelper.SetCookie("cookiescurrentlid", num.ToString());
            }
            else if (this.Context.Request.Path.ToLower().IndexOf("l_jscqsc") > -1)
            {
                num = 2;
                CookieHelper.SetCookie("cookiescurrentmlid", num.ToString());
                num = 13;
                CookieHelper.SetCookie("cookiescurrentlid", num.ToString());
            }
            else if (this.Context.Request.Path.ToLower().IndexOf("l_jspk10") > -1)
            {
                num = 2;
                CookieHelper.SetCookie("cookiescurrentmlid", num.ToString());
                num = 12;
                CookieHelper.SetCookie("cookiescurrentlid", num.ToString());
            }
            else if (this.Context.Request.Path.ToLower().IndexOf("l_jssfc") > -1)
            {
                num = 2;
                CookieHelper.SetCookie("cookiescurrentmlid", num.ToString());
                num = 14;
                CookieHelper.SetCookie("cookiescurrentlid", num.ToString());
            }
            else if (this.Context.Request.Path.ToLower().IndexOf("l_jsft2") > -1)
            {
                num = 2;
                CookieHelper.SetCookie("cookiescurrentmlid", num.ToString());
                num = 15;
                CookieHelper.SetCookie("cookiescurrentlid", num.ToString());
            }
            else if (this.Context.Request.Path.ToLower().IndexOf("l_car168") > -1)
            {
                num = 2;
                CookieHelper.SetCookie("cookiescurrentmlid", num.ToString());
                num = 0x10;
                CookieHelper.SetCookie("cookiescurrentlid", num.ToString());
            }
            else if (this.Context.Request.Path.ToLower().IndexOf("l_ssc168") > -1)
            {
                num = 2;
                CookieHelper.SetCookie("cookiescurrentmlid", num.ToString());
                num = 0x11;
                CookieHelper.SetCookie("cookiescurrentlid", num.ToString());
            }
            else if (this.Context.Request.Path.ToLower().IndexOf("l_vrcar") > -1)
            {
                num = 2;
                CookieHelper.SetCookie("cookiescurrentmlid", num.ToString());
                num = 0x12;
                CookieHelper.SetCookie("cookiescurrentlid", num.ToString());
            }
            else if (this.Context.Request.Path.ToLower().IndexOf("l_vrssc") > -1)
            {
                num = 2;
                CookieHelper.SetCookie("cookiescurrentmlid", num.ToString());
                num = 0x13;
                CookieHelper.SetCookie("cookiescurrentlid", num.ToString());
            }
            else if (this.Context.Request.Path.ToLower().IndexOf("l_xyftoa") > -1)
            {
                num = 2;
                CookieHelper.SetCookie("cookiescurrentmlid", num.ToString());
                num = 20;
                CookieHelper.SetCookie("cookiescurrentlid", num.ToString());
            }
            else if (this.Context.Request.Path.ToLower().IndexOf("l_xyftsg") > -1)
            {
                num = 2;
                CookieHelper.SetCookie("cookiescurrentmlid", num.ToString());
                num = 0x15;
                CookieHelper.SetCookie("cookiescurrentlid", num.ToString());
            }
            else if (this.Context.Request.Path.ToLower().IndexOf("l_happycar") > -1)
            {
                num = 2;
                CookieHelper.SetCookie("cookiescurrentmlid", num.ToString());
                CookieHelper.SetCookie("cookiescurrentlid", 0x16.ToString());
            }
        }

        public List<FastSale> UserPutBet(DataTable plDT, List<string> successBetList, string[] arrp_id, string[] arr_p, string[] arr_m)
        {
            FastSale sale;
            List<FastSale> list = new List<FastSale>();
            List<string> list2 = new List<string>();
            int num = 0;
            while (num < successBetList.Count)
            {
                string[] strArray = successBetList[num].Split(new char[] { ',' });
                string str = strArray[1];
                string str2 = strArray[2];
                string str3 = strArray[3];
                string str4 = strArray[4];
                string str5 = strArray[5];
                list2.Add(strArray[0]);
                sale = new FastSale();
                sale.set_success("1");
                sale.set_ordernum(str);
                sale.set_playname(str2);
                sale.set_putamount(str3);
                sale.set_islm("0");
                sale.set_pl(str4);
                sale.set_message("成功訂單");
                sale.set_amount(str5);
                list.Add(sale);
                num++;
            }
            int index = 0;
            foreach (string str6 in arrp_id)
            {
                string str7 = arr_p[index];
                string str8 = arr_m[index];
                bool flag = false;
                for (num = 0; num < list2.Count; num++)
                {
                    if (str6.Equals(list2[num].ToString()))
                    {
                        flag = true;
                        break;
                    }
                }
                if (!flag)
                {
                    DataRow[] rowArray = plDT.Select(string.Format(" odds_id={0}", str6));
                    string str9 = rowArray[0]["play_name"].ToString();
                    string str10 = rowArray[0]["put_amount"].ToString();
                    sale = new FastSale();
                    sale.set_success("0");
                    sale.set_ordernum("失敗訂單");
                    sale.set_playname(str9);
                    sale.set_putamount(str10);
                    sale.set_islm("0");
                    sale.set_pl(str7);
                    sale.set_message("失敗訂單");
                    sale.set_amount(str8);
                    list.Add(sale);
                }
                index++;
            }
            return list;
        }

        public void UserPutBetByPhone(DataTable plDT, List<string> successBetList, string[] arrp_id, string[] arr_p, string[] arr_m)
        {
            string str = "";
            string str2 = "";
            this.Session["successStrBetPhone"] = null;
            this.Session["errorStrBetPhone"] = null;
            List<string> list = new List<string>();
            int num = 0;
            while (num < successBetList.Count)
            {
                string[] strArray = successBetList[num].Split(new char[] { ',' });
                string str3 = strArray[1];
                string str4 = strArray[2];
                string str5 = strArray[3];
                string str6 = strArray[4];
                string str7 = strArray[5];
                list.Add(strArray[0]);
                if (string.IsNullOrEmpty(str))
                {
                    str = str + string.Format("{0},{1},{2},{3}", new object[] { str4, str5, str6, str7 });
                }
                else
                {
                    str = str + string.Format("|{0},{1},{2},{3}", new object[] { str4, str5, str6, str7 });
                }
                num++;
            }
            int index = 0;
            foreach (string str8 in arrp_id)
            {
                string str9 = arr_p[index];
                string str10 = arr_m[index];
                bool flag = false;
                for (num = 0; num < list.Count; num++)
                {
                    if (str8.Equals(list[num].ToString()))
                    {
                        flag = true;
                        break;
                    }
                }
                if (!flag)
                {
                    DataRow[] rowArray = plDT.Select(string.Format(" odds_id={0}", str8));
                    string str11 = rowArray[0]["play_name"].ToString();
                    string str12 = rowArray[0]["put_amount"].ToString();
                    if (string.IsNullOrEmpty(str2))
                    {
                        str2 = str2 + string.Format("{0},{1},{2},{3}", new object[] { str11, str12, str9, str10 });
                    }
                    else
                    {
                        str2 = str2 + string.Format("|{0},{1},{2},{3}", new object[] { str11, str12, str9, str10 });
                    }
                }
                index++;
            }
            if (!string.IsNullOrEmpty(str))
            {
                this.Session["successStrBetPhone"] = base.Server.UrlEncode(str);
            }
            if (!string.IsNullOrEmpty(str2))
            {
                this.Session["errorStrBetPhone"] = base.Server.UrlEncode(str2);
            }
        }

        protected void ValidWebModelView()
        {
            string str = HttpContext.Current.Session["user_name"].ToString();
            string str2 = this.Context.Request.Path.ToLower();
            if (((str2.IndexOf("loginvalidate.aspx") <= -1) && (str2.IndexOf("logajaxexception.aspx") <= -1)) && (str2.IndexOf("messagepage.aspx") <= -1))
            {
                if (this.WebModelView.Equals(0) && (str2.IndexOf("oldweb/") < 0))
                {
                    this.Session.Abandon();
                    base.Response.Write("<script>top.location.href='/'</script>");
                    base.Response.End();
                }
                if (this.WebModelView.Equals(1) && (str2.IndexOf("oldweb/") > -1))
                {
                    this.Session.Abandon();
                    base.Response.Write("<script>top.location.href='/'</script>");
                    base.Response.End();
                }
            }
        }

        public cz_userinfo_session GetUserModelInfo
        {
            get
            {
                return (HttpContext.Current.Session[HttpContext.Current.Session["user_name"] + "lottery_session_user_info"] as cz_userinfo_session);
            }
        }

        public string UserCurrentLotteryID
        {
            get
            {
                string cookie = CookieHelper.GetCookie("cookiescurrentlid");
                if (string.IsNullOrEmpty(cookie))
                {
                    string str2 = this.GetLotteryList().Rows[0]["id"].ToString();
                    CookieHelper.SetCookie("cookiescurrentlid", str2);
                }
                return cookie;
            }
        }

        public string UserCurrentMasterLotteryID
        {
            get
            {
                string cookie = CookieHelper.GetCookie("cookiescurrentmlid");
                if (string.IsNullOrEmpty(cookie))
                {
                    string str2 = this.GetLotteryList().Rows[0]["master_id"].ToString();
                    CookieHelper.SetCookie("cookiescurrentmlid", str2);
                }
                return cookie;
            }
        }

        public string UserCurrentSkin { get; set; }

        protected int WebModelView { get; set; }
    }
}

