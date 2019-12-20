namespace User.Web.Handler
{
    using LotterySystem.Common;
    using LotterySystem.Model;
    using LotterySystem.WebPageBase;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Web;
    using User.Web.WebBase;

    public class QueryHandler : BaseHandler
    {
        public string DD()
        {
            ReturnResult result = new ReturnResult();
            result.set_data("");
            result.set_success(200);
            result.set_tipinfo("系统错误！");
            return JsonHandle.ObjectToJson(result);
        }

        public void do_default(HttpContext context, ref string strResult)
        {
            ReturnResult result = new ReturnResult();
            result.set_success(400);
            result.set_tipinfo("action param error");
            strResult = JsonHandle.ObjectToJson(result);
        }

        public void get_ad(HttpContext context, ref string strResult)
        {
            ReturnResult result = new ReturnResult();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("type", "get_ad");
            int num = base.get_current_master_id();
            string str = num.ToString();
            if (num == 0)
            {
                str = "0,1,2";
            }
            else if (num == 1)
            {
                str = "0,1";
            }
            else
            {
                str = "0,2";
            }
            string str2 = "hy";
            List<object> list = base.get_ad_level(str2, 1, str);
            if (list == null)
            {
                dictionary.Add("ad", new List<object>());
            }
            else
            {
                dictionary.Add("ad", list);
            }
            string str3 = "";
            string str4 = "";
            string str5 = "";
            string str6 = "";
            string s = "";
            string str8 = "";
            string str10 = "";
            string str11 = "";
            if (context.Session["user_name"] == null)
            {
                context.Response.End();
            }
            else
            {
                string user = context.Session["user_name"].ToString();
                cz_userinfo_session getUserModelInfo = base.GetUserModelInfo;
                string str13 = getUserModelInfo.get_su_type().ToString();
                string str14 = getUserModelInfo.get_u_name().ToString();
                string str15 = LSRequest.qq("browserCode");
                string str16 = HttpContext.Current.Session["lottery_session_img_code_brower"];
                if (!((str15 != null) && str15.Equals(str16)))
                {
                    this.Session.Abandon();
                    result.set_success(300);
                    result.set_data(dictionary);
                    strResult = JsonHandle.ObjectToJson(result);
                }
                else
                {
                    if (FileCacheHelper.get_RedisStatOnline().Equals(1))
                    {
                        base.CheckIsOut(getUserModelInfo.get_u_name());
                        base.stat_online_redis(getUserModelInfo.get_u_name(), "hy");
                        base.stat_online_redis_timer();
                    }
                    else if (FileCacheHelper.get_RedisStatOnline().Equals(2))
                    {
                        base.CheckIsOutStack(getUserModelInfo.get_u_name());
                        base.stat_online_redisStack(getUserModelInfo.get_u_name(), "hy");
                        base.stat_online_redis_timerStack();
                    }
                    else
                    {
                        MemberPageBase.stat_online(user, "hy");
                    }
                    DataTable table = CallBLL.cz_users_bll.GetCredit(str14, str13).Tables[0];
                    if ((int.Parse(table.Rows[0]["u_status"].ToString()) == 2) || base.IsUserOut(str14))
                    {
                        result.set_success(100);
                        result.set_data(dictionary);
                        strResult = JsonHandle.ObjectToJson(result);
                    }
                    else
                    {
                        Dictionary<string, object> dictionary2;
                        string str17;
                        Dictionary<string, object> dictionary3;
                        string str18;
                        str5 = table.Rows[0]["six_credit"].ToString();
                        str6 = table.Rows[0]["kc_credit"].ToString();
                        str4 = table.Rows[0]["kc_kind"].ToString();
                        str3 = table.Rows[0]["six_kind"].ToString();
                        s = table.Rows[0]["six_usable_credit"].ToString();
                        str8 = table.Rows[0]["kc_usable_credit"].ToString();
                        str10 = table.Rows[0]["six_iscash"].ToString();
                        str11 = table.Rows[0]["kc_iscash"].ToString();
                        getUserModelInfo.set_kc_kind(str4.ToUpper());
                        getUserModelInfo.set_six_kind(str3.ToUpper());
                        if (str.Equals("0,1,2"))
                        {
                            dictionary2 = new Dictionary<string, object>();
                            str17 = string.Format("game_{0}", 1);
                            dictionary2.Add("kind", str3);
                            dictionary2.Add("name", "香港⑥合彩");
                            dictionary2.Add("credit", Convert.ToDouble(str5).ToString());
                            dictionary2.Add("usable_credit", (int) double.Parse(s));
                            dictionary2.Add("iscash", str10);
                            dictionary.Add(str17, dictionary2);
                            dictionary3 = new Dictionary<string, object>();
                            str18 = string.Format("game_{0}", 2);
                            dictionary3.Add("kind", str4);
                            dictionary3.Add("name", "快彩");
                            dictionary3.Add("credit", Convert.ToDouble(str6).ToString());
                            dictionary3.Add("usable_credit", (int) double.Parse(str8));
                            dictionary3.Add("profit", base.GetKCProfit());
                            dictionary3.Add("iscash", str11);
                            dictionary.Add(str18, dictionary3);
                        }
                        if (str.Equals("0,1"))
                        {
                            dictionary2 = new Dictionary<string, object>();
                            str17 = string.Format("game_{0}", 1);
                            dictionary2.Add("kind", str3);
                            dictionary2.Add("name", "香港⑥合彩");
                            dictionary2.Add("credit", Convert.ToDouble(str5).ToString());
                            dictionary2.Add("usable_credit", (int) double.Parse(s));
                            dictionary2.Add("iscash", str10);
                            dictionary.Add(str17, dictionary2);
                        }
                        if (str.Equals("0,2"))
                        {
                            dictionary3 = new Dictionary<string, object>();
                            str18 = string.Format("game_{0}", 2);
                            dictionary3.Add("kind", str4);
                            dictionary3.Add("name", "快彩");
                            dictionary3.Add("credit", Convert.ToDouble(str6).ToString());
                            dictionary3.Add("usable_credit", (int) double.Parse(str8));
                            dictionary3.Add("profit", base.GetKCProfit());
                            dictionary3.Add("iscash", str11);
                            dictionary.Add(str18, dictionary3);
                        }
                        result.set_data(dictionary);
                        strResult = JsonHandle.ObjectToJson(result);
                    }
                }
            }
        }

        public void get_currentphase(HttpContext context, ref string strResult)
        {
            ReturnResult result = new ReturnResult();
            result.set_success(400);
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("type", "get_currentphase");
            if (context.Session["user_name"] == null)
            {
                context.Response.End();
            }
            else
            {
                List<object> list;
                DataSet currentByPhase;
                List<string> list2;
                DataTable table;
                int num;
                Dictionary<string, object> dictionary3;
                string str4;
                string str5;
                string str6;
                string str10;
                string str11;
                string str15;
                int num2;
                string str16;
                int num3;
                string str17;
                string str18;
                string str19;
                string str20;
                string str21;
                string str22;
                string str23;
                string str24;
                string str25;
                string str26;
                int num8;
                int num9;
                int num10;
                int num11;
                int num12;
                string str27;
                string str28;
                string str29;
                string str30;
                string str31;
                string str32;
                string str33;
                string str34;
                int num13;
                int num14;
                int num15;
                int num16;
                int num17;
                int num35;
                string s = LSRequest.qq("lid");
                string str2 = LSRequest.qq("phase");
                string str3 = LSRequest.qq("tabletype");
                Dictionary<string, string> dictionary2 = new Dictionary<string, string>();
                switch (int.Parse(s))
                {
                    case 0:
                        list = new List<object>();
                        list2 = new List<string>();
                        currentByPhase = CallBLL.cz_phase_kl10_bll.GetCurrentByPhase(str2);
                        if (currentByPhase != null)
                        {
                            table = currentByPhase.Tables[0];
                            num = 8;
                            dictionary3 = new Dictionary<string, object>();
                            str4 = table.Rows[0]["is_closed"];
                            str5 = table.Rows[0]["is_opendata"];
                            if (str4.Equals("1") && str5.Equals("1"))
                            {
                                foreach (DataRow row in table.Rows)
                                {
                                    str6 = "";
                                    str18 = "";
                                    str19 = "";
                                    str15 = "";
                                    str16 = "";
                                    str20 = "";
                                    str21 = "";
                                    str6 = row["phase"].ToString();
                                    str18 = row["play_open_date"].ToString();
                                    num3 = 1;
                                    while (num3 <= num)
                                    {
                                        str17 = "n" + num3;
                                        list2.Add(row[str17].ToString());
                                        num3++;
                                    }
                                    str19 = KL10Phase.get_zh(list2).ToString();
                                    str15 = KL10Phase.get_cl_zhdx(str19);
                                    str15 = base.SetWordColor(str15);
                                    str16 = KL10Phase.get_cl_zhds(str19);
                                    str16 = base.SetWordColor(str16);
                                    str20 = KL10Phase.get_cl_zhwsdx(str19);
                                    str20 = base.SetWordColor(str20);
                                    str21 = KL10Phase.get_cl_lh(list2[0].ToString(), list2[num - 1].ToString());
                                    str21 = base.SetWordColor(str21);
                                    dictionary3.Add("phase", str6);
                                    dictionary3.Add("play_open_date", str18);
                                    dictionary3.Add("draw_num", new List<string>(list2));
                                    dictionary3.Add("total", new List<string> { str19, str15, str16, str20, str21 });
                                    list.Add(new Dictionary<string, object>(dictionary3));
                                    dictionary3.Clear();
                                    list2.Clear();
                                }
                            }
                        }
                        dictionary.Add("jqkj", list);
                        break;

                    case 1:
                        list = new List<object>();
                        list2 = new List<string>();
                        currentByPhase = CallBLL.cz_phase_cqsc_bll.GetCurrentByPhase(str2);
                        if (currentByPhase != null)
                        {
                            table = currentByPhase.Tables[0];
                            dictionary3 = new Dictionary<string, object>();
                            str4 = table.Rows[0]["is_closed"];
                            str5 = table.Rows[0]["is_opendata"];
                            if (str4.Equals("1") && str5.Equals("1"))
                            {
                                foreach (DataRow row in table.Rows)
                                {
                                    str6 = "";
                                    str18 = "";
                                    str22 = "";
                                    str15 = "";
                                    str16 = "";
                                    str23 = "";
                                    str24 = "";
                                    str25 = "";
                                    str26 = "";
                                    str6 = row["phase"].ToString();
                                    str18 = row["play_open_date"].ToString();
                                    num8 = Convert.ToInt32(row["n1"].ToString());
                                    num9 = Convert.ToInt32(row["n2"].ToString());
                                    num10 = Convert.ToInt32(row["n3"].ToString());
                                    num11 = Convert.ToInt32(row["n4"].ToString());
                                    num12 = Convert.ToInt32(row["n5"].ToString());
                                    list2.Add(num8.ToString());
                                    list2.Add(num9.ToString());
                                    list2.Add(num10.ToString());
                                    list2.Add(num11.ToString());
                                    list2.Add(num12.ToString());
                                    num35 = (((num8 + num9) + num10) + num11) + num12;
                                    str22 = num35.ToString();
                                    str15 = CQSCPhase.get_cl_zhdx(str22);
                                    str15 = base.SetWordColor(str15);
                                    str16 = CQSCPhase.get_cl_zhds(str22);
                                    str16 = base.SetWordColor(str16);
                                    str23 = CQSCPhase.get_cl_lh(num8.ToString(), num12.ToString());
                                    str23 = base.SetWordColor(str23);
                                    str24 = CQSCPhase.get_cqsc_str(string.Concat(new object[] { num8, ",", num9, ",", num10 }));
                                    str25 = CQSCPhase.get_cqsc_str(string.Concat(new object[] { num9, ",", num10, ",", num11 }));
                                    str26 = CQSCPhase.get_cqsc_str(string.Concat(new object[] { num10, ",", num11, ",", num12 }));
                                    dictionary3.Add("phase", str6);
                                    dictionary3.Add("play_open_date", str18);
                                    dictionary3.Add("draw_num", new List<string>(list2));
                                    dictionary3.Add("total", new List<string> { str22, str15, str16, str23, str24, str25, str26 });
                                    list.Add(new Dictionary<string, object>(dictionary3));
                                    dictionary3.Clear();
                                    list2.Clear();
                                }
                            }
                        }
                        dictionary.Add("jqkj", list);
                        break;

                    case 2:
                        list = new List<object>();
                        list2 = new List<string>();
                        currentByPhase = CallBLL.cz_phase_pk10_bll.GetCurrentByPhase(str2);
                        if (currentByPhase != null)
                        {
                            table = currentByPhase.Tables[0];
                            num = 5;
                            dictionary3 = new Dictionary<string, object>();
                            str4 = table.Rows[0]["is_closed"];
                            str5 = table.Rows[0]["is_opendata"];
                            if (str4.Equals("1") && str5.Equals("1"))
                            {
                                foreach (DataRow row in table.Rows)
                                {
                                    str6 = "";
                                    str18 = "";
                                    str27 = "";
                                    str28 = "";
                                    str29 = "";
                                    str30 = "";
                                    str31 = "";
                                    str32 = "";
                                    str33 = "";
                                    str34 = "";
                                    str6 = row["phase"].ToString();
                                    str18 = row["play_open_date"].ToString();
                                    num8 = Convert.ToInt32(row["n1"].ToString());
                                    num9 = Convert.ToInt32(row["n2"].ToString());
                                    num10 = Convert.ToInt32(row["n3"].ToString());
                                    num11 = Convert.ToInt32(row["n4"].ToString());
                                    num12 = Convert.ToInt32(row["n5"].ToString());
                                    num13 = Convert.ToInt32(row["n6"].ToString());
                                    num14 = Convert.ToInt32(row["n7"].ToString());
                                    num15 = Convert.ToInt32(row["n8"].ToString());
                                    num16 = Convert.ToInt32(row["n9"].ToString());
                                    num17 = Convert.ToInt32(row["n10"].ToString());
                                    list2.Add((num8 < 10) ? ("0" + num8) : num8.ToString());
                                    list2.Add((num9 < 10) ? ("0" + num9) : num9.ToString());
                                    list2.Add((num10 < 10) ? ("0" + num10) : num10.ToString());
                                    list2.Add((num11 < 10) ? ("0" + num11) : num11.ToString());
                                    list2.Add((num12 < 10) ? ("0" + num12) : num12.ToString());
                                    list2.Add((num13 < 10) ? ("0" + num13) : num13.ToString());
                                    list2.Add((num14 < 10) ? ("0" + num14) : num14.ToString());
                                    list2.Add((num15 < 10) ? ("0" + num15) : num15.ToString());
                                    list2.Add((num16 < 10) ? ("0" + num16) : num16.ToString());
                                    list2.Add((num17 < 10) ? ("0" + num17) : num17.ToString());
                                    num35 = num8 + num9;
                                    str27 = num35.ToString();
                                    if (Convert.ToInt32(str27) <= 11)
                                    {
                                        str28 = "小";
                                    }
                                    else
                                    {
                                        str28 = "大";
                                    }
                                    str28 = base.SetWordColor(str28);
                                    if ((Convert.ToInt32(str27) % 2) == 0)
                                    {
                                        str29 = "雙";
                                    }
                                    else
                                    {
                                        str29 = "單";
                                    }
                                    str29 = base.SetWordColor(str29);
                                    if (num8 > num17)
                                    {
                                        str30 = "龍";
                                    }
                                    else
                                    {
                                        str30 = "虎";
                                    }
                                    str30 = base.SetWordColor(str30);
                                    if (num9 > num16)
                                    {
                                        str31 = "龍";
                                    }
                                    else
                                    {
                                        str31 = "虎";
                                    }
                                    str31 = base.SetWordColor(str31);
                                    if (num10 > num15)
                                    {
                                        str32 = "龍";
                                    }
                                    else
                                    {
                                        str32 = "虎";
                                    }
                                    str32 = base.SetWordColor(str32);
                                    if (num11 > num14)
                                    {
                                        str33 = "龍";
                                    }
                                    else
                                    {
                                        str33 = "虎";
                                    }
                                    str33 = base.SetWordColor(str33);
                                    if (num12 > num13)
                                    {
                                        str34 = "龍";
                                    }
                                    else
                                    {
                                        str34 = "虎";
                                    }
                                    str34 = base.SetWordColor(str34);
                                    dictionary3.Add("phase", str6);
                                    dictionary3.Add("play_open_date", str18);
                                    dictionary3.Add("draw_num", new List<string>(list2));
                                    dictionary3.Add("total", new List<string> { str27, str28, str29, str30, str31, str32, str33, str34 });
                                    list.Add(new Dictionary<string, object>(dictionary3));
                                    dictionary3.Clear();
                                    list2.Clear();
                                }
                            }
                        }
                        dictionary.Add("jqkj", list);
                        break;

                    case 3:
                        list = new List<object>();
                        list2 = new List<string>();
                        currentByPhase = CallBLL.cz_phase_xync_bll.GetCurrentByPhase(str2);
                        if (currentByPhase != null)
                        {
                            str6 = "";
                            str18 = "";
                            num2 = 0;
                            str15 = "";
                            str16 = "";
                            str20 = "";
                            string str35 = "";
                            table = currentByPhase.Tables[0];
                            num = 8;
                            dictionary3 = new Dictionary<string, object>();
                            str4 = table.Rows[0]["is_closed"];
                            str5 = table.Rows[0]["is_opendata"];
                            if (str4.Equals("1") && str5.Equals("1"))
                            {
                                foreach (DataRow row in table.Rows)
                                {
                                    str6 = row["phase"].ToString();
                                    str18 = row["play_open_date"].ToString();
                                    for (num3 = 1; num3 <= num; num3++)
                                    {
                                        str17 = "n" + num3;
                                        list2.Add(row[str17].ToString());
                                        num2 += Convert.ToInt32(row[str17]);
                                    }
                                    if (num2 == 0x54)
                                    {
                                        str15 = "和";
                                    }
                                    else if (num2 <= 0x53)
                                    {
                                        str15 = "小";
                                    }
                                    else
                                    {
                                        str15 = "大";
                                    }
                                    str15 = base.SetWordColor(str15);
                                    if ((num2 % 2) == 0)
                                    {
                                        str16 = "雙";
                                    }
                                    else
                                    {
                                        str16 = "單";
                                    }
                                    str16 = base.SetWordColor(str16);
                                    if ((num2 % 10) <= 4)
                                    {
                                        str20 = "尾小";
                                    }
                                    else
                                    {
                                        str20 = "尾大";
                                    }
                                    str20 = base.SetWordColor(str20);
                                    if (int.Parse(row["N1"].ToString().Trim()) > int.Parse(row["N8"].ToString().Trim()))
                                    {
                                        str35 = "家禽";
                                    }
                                    else
                                    {
                                        str35 = "野獸";
                                    }
                                    str35 = base.SetWordColor(str35);
                                    dictionary3.Add("phase", str6);
                                    dictionary3.Add("play_open_date", str18);
                                    dictionary3.Add("draw_num", new List<string>(list2));
                                    dictionary3.Add("total", new List<string> { num2.ToString(), str15, str16, str20, str35 });
                                    list.Add(new Dictionary<string, object>(dictionary3));
                                    dictionary3.Clear();
                                    list2.Clear();
                                }
                            }
                        }
                        dictionary.Add("jqkj", list);
                        break;

                    case 4:
                        list = new List<object>();
                        list2 = new List<string>();
                        currentByPhase = CallBLL.cz_phase_jsk3_bll.GetCurrentByPhase(str2);
                        if (currentByPhase != null)
                        {
                            table = currentByPhase.Tables[0];
                            dictionary3 = new Dictionary<string, object>();
                            str4 = table.Rows[0]["is_closed"];
                            str5 = table.Rows[0]["is_opendata"];
                            if (str4.Equals("1") && str5.Equals("1"))
                            {
                                foreach (DataRow row in table.Rows)
                                {
                                    str6 = "";
                                    str18 = "";
                                    str22 = "";
                                    str15 = "";
                                    str6 = row["phase"].ToString();
                                    str18 = row["play_open_date"].ToString();
                                    num8 = Convert.ToInt32(row["n1"].ToString());
                                    num9 = Convert.ToInt32(row["n2"].ToString());
                                    num10 = Convert.ToInt32(row["n3"].ToString());
                                    list2.Add(num8.ToString());
                                    list2.Add(num9.ToString());
                                    list2.Add(num10.ToString());
                                    num35 = (num8 + num9) + num10;
                                    str22 = num35.ToString();
                                    if ((num8 == num9) && (num9 == num10))
                                    {
                                        str15 = "通吃";
                                    }
                                    else if (Convert.ToInt32(str22) <= 10)
                                    {
                                        str15 = "小";
                                    }
                                    else
                                    {
                                        str15 = "大";
                                    }
                                    str15 = base.SetWordColor(str15);
                                    dictionary3.Add("phase", str6);
                                    dictionary3.Add("play_open_date", str18);
                                    dictionary3.Add("draw_num", new List<string>(list2));
                                    dictionary3.Add("total", new List<string> { str22, str15 });
                                    list.Add(new Dictionary<string, object>(dictionary3));
                                    dictionary3.Clear();
                                    list2.Clear();
                                }
                            }
                        }
                        dictionary.Add("jqkj", list);
                        break;

                    case 5:
                        list = new List<object>();
                        list2 = new List<string>();
                        currentByPhase = CallBLL.cz_phase_kl8_bll.GetCurrentByPhase(str2);
                        if (currentByPhase != null)
                        {
                            table = currentByPhase.Tables[0];
                            dictionary3 = new Dictionary<string, object>();
                            num3 = 0;
                            str4 = table.Rows[0]["is_closed"];
                            str5 = table.Rows[0]["is_opendata"];
                            if (str4.Equals("1") && str5.Equals("1"))
                            {
                                foreach (DataRow row in table.Rows)
                                {
                                    str6 = "";
                                    str18 = "";
                                    str22 = "";
                                    str15 = "";
                                    str16 = "";
                                    string str36 = "";
                                    string str37 = "";
                                    string str38 = "";
                                    str6 = row["phase"].ToString();
                                    str18 = row["play_open_date"].ToString();
                                    num8 = Convert.ToInt32(row["n1"].ToString());
                                    num9 = Convert.ToInt32(row["n2"].ToString());
                                    num10 = Convert.ToInt32(row["n3"].ToString());
                                    num11 = Convert.ToInt32(row["n4"].ToString());
                                    num12 = Convert.ToInt32(row["n5"].ToString());
                                    num13 = Convert.ToInt32(row["n6"].ToString());
                                    num14 = Convert.ToInt32(row["n7"].ToString());
                                    num15 = Convert.ToInt32(row["n8"].ToString());
                                    num16 = Convert.ToInt32(row["n9"].ToString());
                                    num17 = Convert.ToInt32(row["n10"].ToString());
                                    int num18 = Convert.ToInt32(row["n11"].ToString());
                                    int num19 = Convert.ToInt32(row["n12"].ToString());
                                    int num20 = Convert.ToInt32(row["n13"].ToString());
                                    int num21 = Convert.ToInt32(row["n14"].ToString());
                                    int num22 = Convert.ToInt32(row["n15"].ToString());
                                    int num23 = Convert.ToInt32(row["n16"].ToString());
                                    int num24 = Convert.ToInt32(row["n17"].ToString());
                                    int num25 = Convert.ToInt32(row["n18"].ToString());
                                    int num26 = Convert.ToInt32(row["n19"].ToString());
                                    int num27 = Convert.ToInt32(row["n20"].ToString());
                                    list2.Add((num8 < 10) ? ("0" + num8) : num8.ToString());
                                    list2.Add((num9 < 10) ? ("0" + num9) : num9.ToString());
                                    list2.Add((num10 < 10) ? ("0" + num10) : num10.ToString());
                                    list2.Add((num11 < 10) ? ("0" + num11) : num11.ToString());
                                    list2.Add((num12 < 10) ? ("0" + num12) : num12.ToString());
                                    list2.Add((num13 < 10) ? ("0" + num13) : num13.ToString());
                                    list2.Add((num14 < 10) ? ("0" + num14) : num14.ToString());
                                    list2.Add((num15 < 10) ? ("0" + num15) : num15.ToString());
                                    list2.Add((num16 < 10) ? ("0" + num16) : num16.ToString());
                                    list2.Add((num17 < 10) ? ("0" + num17) : num17.ToString());
                                    list2.Add((num18 < 10) ? ("0" + num18) : num18.ToString());
                                    list2.Add((num19 < 10) ? ("0" + num19) : num19.ToString());
                                    list2.Add((num20 < 10) ? ("0" + num20) : num20.ToString());
                                    list2.Add((num21 < 10) ? ("0" + num21) : num21.ToString());
                                    list2.Add((num22 < 10) ? ("0" + num22) : num22.ToString());
                                    list2.Add((num23 < 10) ? ("0" + num23) : num23.ToString());
                                    list2.Add((num24 < 10) ? ("0" + num24) : num24.ToString());
                                    list2.Add((num25 < 10) ? ("0" + num25) : num25.ToString());
                                    list2.Add((num26 < 10) ? ("0" + num26) : num26.ToString());
                                    list2.Add((num27 < 10) ? ("0" + num27) : num27.ToString());
                                    num35 = ((((((((((((((((((num8 + num9) + num10) + num11) + num12) + num13) + num14) + num15) + num16) + num17) + num18) + num19) + num20) + num21) + num22) + num23) + num24) + num25) + num26) + num27;
                                    str22 = num35.ToString();
                                    if (Convert.ToInt32(str22) == 810)
                                    {
                                        str16 = "和";
                                    }
                                    else if ((Convert.ToInt32(str22) % 2) == 0)
                                    {
                                        str16 = "雙";
                                    }
                                    else
                                    {
                                        str16 = "單";
                                    }
                                    str16 = base.SetWordColor(str16);
                                    if (Convert.ToInt32(str22) > 810)
                                    {
                                        str15 = "大";
                                    }
                                    else if (Convert.ToInt32(str22) < 810)
                                    {
                                        str15 = "小";
                                    }
                                    else
                                    {
                                        str15 = "和";
                                    }
                                    str15 = base.SetWordColor(str15);
                                    int num28 = 0;
                                    int num29 = 0;
                                    int num30 = 0;
                                    while (num30 < 20)
                                    {
                                        if ((int.Parse(table.Rows[num3]["n" + (num30 + 1)].ToString().Trim()) % 2) == 0)
                                        {
                                            num29++;
                                        }
                                        else
                                        {
                                            num28++;
                                        }
                                        num30++;
                                    }
                                    str36 = KL8Phase.get_cl_dsh(num28.ToString(), num29.ToString());
                                    str36 = base.SetWordColor(str36);
                                    int num31 = 0;
                                    int num32 = 0;
                                    for (num30 = 0; num30 < 20; num30++)
                                    {
                                        if (int.Parse(table.Rows[num3]["n" + (num30 + 1)].ToString().Trim()) <= 40)
                                        {
                                            num31++;
                                        }
                                        else
                                        {
                                            num32++;
                                        }
                                    }
                                    str37 = KL8Phase.get_cl_qhh(num31.ToString(), num32.ToString());
                                    str37 = base.SetWordColor(str37);
                                    str38 = KL8Phase.get_cl_wh(str22.ToString());
                                    dictionary3.Add("phase", str6);
                                    dictionary3.Add("play_open_date", str18);
                                    dictionary3.Add("draw_num", new List<string>(list2));
                                    dictionary3.Add("total", new List<string> { str22, str16, str15, str36, str37, str38 });
                                    list.Add(new Dictionary<string, object>(dictionary3));
                                    dictionary3.Clear();
                                    list2.Clear();
                                    num3++;
                                }
                            }
                        }
                        dictionary.Add("jqkj", list);
                        break;

                    case 6:
                        list = new List<object>();
                        list2 = new List<string>();
                        currentByPhase = CallBLL.cz_phase_k8sc_bll.GetCurrentByPhase(str2);
                        if (currentByPhase != null)
                        {
                            table = currentByPhase.Tables[0];
                            dictionary3 = new Dictionary<string, object>();
                            str4 = table.Rows[0]["is_closed"];
                            str5 = table.Rows[0]["is_opendata"];
                            if (str4.Equals("1") && str5.Equals("1"))
                            {
                                foreach (DataRow row in table.Rows)
                                {
                                    str6 = "";
                                    str18 = "";
                                    str22 = "";
                                    str15 = "";
                                    str16 = "";
                                    str23 = "";
                                    str24 = "";
                                    str25 = "";
                                    str26 = "";
                                    str6 = row["phase"].ToString();
                                    str18 = row["play_open_date"].ToString();
                                    num8 = Convert.ToInt32(row["n1"].ToString());
                                    num9 = Convert.ToInt32(row["n2"].ToString());
                                    num10 = Convert.ToInt32(row["n3"].ToString());
                                    num11 = Convert.ToInt32(row["n4"].ToString());
                                    num12 = Convert.ToInt32(row["n5"].ToString());
                                    list2.Add(num8.ToString());
                                    list2.Add(num9.ToString());
                                    list2.Add(num10.ToString());
                                    list2.Add(num11.ToString());
                                    list2.Add(num12.ToString());
                                    num35 = (((num8 + num9) + num10) + num11) + num12;
                                    str22 = num35.ToString();
                                    str15 = K8SCPhase.get_cl_zhdx(str22);
                                    str15 = base.SetWordColor(str15);
                                    str16 = K8SCPhase.get_cl_zhds(str22);
                                    str16 = base.SetWordColor(str16);
                                    str23 = K8SCPhase.get_cl_lh(num8.ToString(), num12.ToString());
                                    str23 = base.SetWordColor(str23);
                                    str24 = K8SCPhase.get_k8sc_str(string.Concat(new object[] { num8, ",", num9, ",", num10 }));
                                    str25 = K8SCPhase.get_k8sc_str(string.Concat(new object[] { num9, ",", num10, ",", num11 }));
                                    str26 = K8SCPhase.get_k8sc_str(string.Concat(new object[] { num10, ",", num11, ",", num12 }));
                                    dictionary3.Add("phase", str6);
                                    dictionary3.Add("play_open_date", str18);
                                    dictionary3.Add("draw_num", new List<string>(list2));
                                    dictionary3.Add("total", new List<string> { str22, str15, str16, str23, str24, str25, str26 });
                                    list.Add(new Dictionary<string, object>(dictionary3));
                                    dictionary3.Clear();
                                    list2.Clear();
                                }
                            }
                        }
                        dictionary.Add("jqkj", list);
                        break;

                    case 7:
                        list = new List<object>();
                        list2 = new List<string>();
                        currentByPhase = CallBLL.cz_phase_pcdd_bll.GetCurrentByPhase(str2);
                        if (currentByPhase != null)
                        {
                            table = currentByPhase.Tables[0];
                            dictionary3 = new Dictionary<string, object>();
                            str4 = table.Rows[0]["is_closed"];
                            str5 = table.Rows[0]["is_opendata"];
                            if (str4.Equals("1") && str5.Equals("1"))
                            {
                                foreach (DataRow row in table.Rows)
                                {
                                    str6 = "";
                                    str18 = "";
                                    str11 = "";
                                    str10 = "";
                                    string str39 = "";
                                    string str40 = "";
                                    string str41 = "";
                                    str6 = row["phase"].ToString();
                                    str18 = row["play_open_date"].ToString();
                                    num8 = Convert.ToInt32(row["n1"].ToString());
                                    num9 = Convert.ToInt32(row["n2"].ToString());
                                    num10 = Convert.ToInt32(row["n3"].ToString());
                                    int num33 = Convert.ToInt32(row["sn"].ToString());
                                    list2.Add(num8.ToString());
                                    list2.Add(num9.ToString());
                                    list2.Add(num10.ToString());
                                    list2.Add(num33.ToString());
                                    str11 = PCDDPhase.get_dx(num33.ToString());
                                    str11 = base.SetWordColor(str11);
                                    str10 = PCDDPhase.get_ds(num33.ToString());
                                    str10 = base.SetWordColor(str10);
                                    str39 = PCDDPhase.get_bs(num33.ToString());
                                    str39 = base.SetWordColor(str39);
                                    str40 = PCDDPhase.get_jdx(num33.ToString());
                                    str40 = base.SetWordColor(str40);
                                    str41 = PCDDPhase.get_bz(num8.ToString(), num9.ToString(), num10.ToString());
                                    str41 = base.SetWordColor(str41);
                                    dictionary3.Add("phase", str6);
                                    dictionary3.Add("play_open_date", str18);
                                    dictionary3.Add("draw_num", new List<string>(list2));
                                    dictionary3.Add("total", new List<string> { str11, str10, str39, str40, str41 });
                                    list.Add(new Dictionary<string, object>(dictionary3));
                                    dictionary3.Clear();
                                    list2.Clear();
                                }
                            }
                        }
                        dictionary.Add("jqkj", list);
                        break;

                    case 8:
                        list = new List<object>();
                        list2 = new List<string>();
                        currentByPhase = CallBLL.cz_phase_pkbjl_bll.GetCurrentByPhase(str2, str3);
                        if (currentByPhase != null)
                        {
                            table = currentByPhase.Tables[0];
                            num = 5;
                            dictionary3 = new Dictionary<string, object>();
                            str4 = table.Rows[0]["is_closed"];
                            str5 = table.Rows[0]["is_opendata"];
                            if (str4.Equals("1") && str5.Equals("1"))
                            {
                                foreach (DataRow row in table.Rows)
                                {
                                    str6 = row["phase"].ToString();
                                    str18 = row["play_open_date"].ToString();
                                    num8 = Convert.ToInt32(row["n1"].ToString());
                                    num9 = Convert.ToInt32(row["n2"].ToString());
                                    num10 = Convert.ToInt32(row["n3"].ToString());
                                    num11 = Convert.ToInt32(row["n4"].ToString());
                                    num12 = Convert.ToInt32(row["n5"].ToString());
                                    num13 = Convert.ToInt32(row["n6"].ToString());
                                    num14 = Convert.ToInt32(row["n7"].ToString());
                                    num15 = Convert.ToInt32(row["n8"].ToString());
                                    num16 = Convert.ToInt32(row["n9"].ToString());
                                    num17 = Convert.ToInt32(row["n10"].ToString());
                                    list2.Add((num8 < 10) ? ("0" + num8) : num8.ToString());
                                    list2.Add((num9 < 10) ? ("0" + num9) : num9.ToString());
                                    list2.Add((num10 < 10) ? ("0" + num10) : num10.ToString());
                                    list2.Add((num11 < 10) ? ("0" + num11) : num11.ToString());
                                    list2.Add((num12 < 10) ? ("0" + num12) : num12.ToString());
                                    list2.Add((num13 < 10) ? ("0" + num13) : num13.ToString());
                                    list2.Add((num14 < 10) ? ("0" + num14) : num14.ToString());
                                    list2.Add((num15 < 10) ? ("0" + num15) : num15.ToString());
                                    list2.Add((num16 < 10) ? ("0" + num16) : num16.ToString());
                                    list2.Add((num17 < 10) ? ("0" + num17) : num17.ToString());
                                    string str42 = row["ten_poker"].ToString();
                                    string str43 = row["xian_nn"].ToString();
                                    string str44 = row["zhuang_nn"].ToString();
                                    string pKBJLBalanceMaxMin = Utils.GetPKBJLBalanceMaxMin(str44, str43);
                                    bool pKBJLBalanceIsDuizi = Utils.GetPKBJLBalanceIsDuizi(str43);
                                    bool flag2 = Utils.GetPKBJLBalanceIsDuizi(str44);
                                    string str46 = "-";
                                    string str47 = "-";
                                    string pKBJLBalanceZXH = Utils.GetPKBJLBalanceZXH(str44, str43);
                                    if (pKBJLBalanceMaxMin.Equals("min"))
                                    {
                                        pKBJLBalanceMaxMin = base.SetWordColor("小");
                                    }
                                    else
                                    {
                                        pKBJLBalanceMaxMin = base.SetWordColor("大");
                                    }
                                    if (pKBJLBalanceIsDuizi)
                                    {
                                        str46 = base.SetWordColor("閑對");
                                    }
                                    if (flag2)
                                    {
                                        str47 = base.SetWordColor("莊對");
                                    }
                                    if (pKBJLBalanceZXH.Equals("zhuang"))
                                    {
                                        pKBJLBalanceZXH = base.SetWordColor("莊");
                                    }
                                    else if (pKBJLBalanceZXH.Equals("xian"))
                                    {
                                        pKBJLBalanceZXH = base.SetWordColor("閑");
                                    }
                                    else
                                    {
                                        pKBJLBalanceZXH = base.SetWordColor("和");
                                    }
                                    string pKBJLBalanceDianshu = Utils.GetPKBJLBalanceDianshu(str43);
                                    string str50 = Utils.GetPKBJLBalanceDianshu(str44);
                                    dictionary3.Add("phase", str6);
                                    dictionary3.Add("play_open_date", str18);
                                    dictionary3.Add("draw_num", new List<string>(list2));
                                    dictionary3.Add("pokerList", str42);
                                    dictionary3.Add("xian_nn", str43);
                                    dictionary3.Add("zhuang_nn", str44);
                                    dictionary3.Add("xian_dian", pKBJLBalanceDianshu);
                                    dictionary3.Add("zhuang_dian", str50);
                                    dictionary3.Add("total", new List<string> { pKBJLBalanceZXH, pKBJLBalanceMaxMin, str46, str47 });
                                    list.Add(new Dictionary<string, object>(dictionary3));
                                    dictionary3.Clear();
                                    list2.Clear();
                                }
                            }
                        }
                        dictionary.Add("jqkj", list);
                        break;

                    case 9:
                        list = new List<object>();
                        list2 = new List<string>();
                        currentByPhase = CallBLL.cz_phase_xyft5_bll.GetCurrentByPhase(str2);
                        if (currentByPhase != null)
                        {
                            table = currentByPhase.Tables[0];
                            num = 5;
                            dictionary3 = new Dictionary<string, object>();
                            str4 = table.Rows[0]["is_closed"];
                            str5 = table.Rows[0]["is_opendata"];
                            if (str4.Equals("1") && str5.Equals("1"))
                            {
                                foreach (DataRow row in table.Rows)
                                {
                                    str6 = "";
                                    str18 = "";
                                    str27 = "";
                                    str28 = "";
                                    str29 = "";
                                    str30 = "";
                                    str31 = "";
                                    str32 = "";
                                    str33 = "";
                                    str34 = "";
                                    str6 = row["phase"].ToString();
                                    str18 = row["play_open_date"].ToString();
                                    num8 = Convert.ToInt32(row["n1"].ToString());
                                    num9 = Convert.ToInt32(row["n2"].ToString());
                                    num10 = Convert.ToInt32(row["n3"].ToString());
                                    num11 = Convert.ToInt32(row["n4"].ToString());
                                    num12 = Convert.ToInt32(row["n5"].ToString());
                                    num13 = Convert.ToInt32(row["n6"].ToString());
                                    num14 = Convert.ToInt32(row["n7"].ToString());
                                    num15 = Convert.ToInt32(row["n8"].ToString());
                                    num16 = Convert.ToInt32(row["n9"].ToString());
                                    num17 = Convert.ToInt32(row["n10"].ToString());
                                    list2.Add((num8 < 10) ? ("0" + num8) : num8.ToString());
                                    list2.Add((num9 < 10) ? ("0" + num9) : num9.ToString());
                                    list2.Add((num10 < 10) ? ("0" + num10) : num10.ToString());
                                    list2.Add((num11 < 10) ? ("0" + num11) : num11.ToString());
                                    list2.Add((num12 < 10) ? ("0" + num12) : num12.ToString());
                                    list2.Add((num13 < 10) ? ("0" + num13) : num13.ToString());
                                    list2.Add((num14 < 10) ? ("0" + num14) : num14.ToString());
                                    list2.Add((num15 < 10) ? ("0" + num15) : num15.ToString());
                                    list2.Add((num16 < 10) ? ("0" + num16) : num16.ToString());
                                    list2.Add((num17 < 10) ? ("0" + num17) : num17.ToString());
                                    num35 = num8 + num9;
                                    str27 = num35.ToString();
                                    if (Convert.ToInt32(str27) <= 11)
                                    {
                                        str28 = "小";
                                    }
                                    else
                                    {
                                        str28 = "大";
                                    }
                                    str28 = base.SetWordColor(str28);
                                    if ((Convert.ToInt32(str27) % 2) == 0)
                                    {
                                        str29 = "雙";
                                    }
                                    else
                                    {
                                        str29 = "單";
                                    }
                                    str29 = base.SetWordColor(str29);
                                    if (num8 > num17)
                                    {
                                        str30 = "龍";
                                    }
                                    else
                                    {
                                        str30 = "虎";
                                    }
                                    str30 = base.SetWordColor(str30);
                                    if (num9 > num16)
                                    {
                                        str31 = "龍";
                                    }
                                    else
                                    {
                                        str31 = "虎";
                                    }
                                    str31 = base.SetWordColor(str31);
                                    if (num10 > num15)
                                    {
                                        str32 = "龍";
                                    }
                                    else
                                    {
                                        str32 = "虎";
                                    }
                                    str32 = base.SetWordColor(str32);
                                    if (num11 > num14)
                                    {
                                        str33 = "龍";
                                    }
                                    else
                                    {
                                        str33 = "虎";
                                    }
                                    str33 = base.SetWordColor(str33);
                                    if (num12 > num13)
                                    {
                                        str34 = "龍";
                                    }
                                    else
                                    {
                                        str34 = "虎";
                                    }
                                    str34 = base.SetWordColor(str34);
                                    dictionary3.Add("phase", str6);
                                    dictionary3.Add("play_open_date", str18);
                                    dictionary3.Add("draw_num", new List<string>(list2));
                                    dictionary3.Add("total", new List<string> { str27, str28, str29, str30, str31, str32, str33, str34 });
                                    list.Add(new Dictionary<string, object>(dictionary3));
                                    dictionary3.Clear();
                                    list2.Clear();
                                }
                            }
                        }
                        dictionary.Add("jqkj", list);
                        break;

                    case 10:
                        list = new List<object>();
                        list2 = new List<string>();
                        currentByPhase = CallBLL.cz_phase_jscar_bll.GetCurrentByPhase(str2);
                        if (currentByPhase != null)
                        {
                            table = currentByPhase.Tables[0];
                            num = 5;
                            dictionary3 = new Dictionary<string, object>();
                            str4 = table.Rows[0]["is_closed"];
                            str5 = table.Rows[0]["is_opendata"];
                            if (str4.Equals("1") && str5.Equals("1"))
                            {
                                foreach (DataRow row in table.Rows)
                                {
                                    str6 = "";
                                    str18 = "";
                                    str27 = "";
                                    str28 = "";
                                    str29 = "";
                                    str30 = "";
                                    str31 = "";
                                    str32 = "";
                                    str33 = "";
                                    str34 = "";
                                    str6 = row["phase"].ToString();
                                    str18 = row["play_open_date"].ToString();
                                    num8 = Convert.ToInt32(row["n1"].ToString());
                                    num9 = Convert.ToInt32(row["n2"].ToString());
                                    num10 = Convert.ToInt32(row["n3"].ToString());
                                    num11 = Convert.ToInt32(row["n4"].ToString());
                                    num12 = Convert.ToInt32(row["n5"].ToString());
                                    num13 = Convert.ToInt32(row["n6"].ToString());
                                    num14 = Convert.ToInt32(row["n7"].ToString());
                                    num15 = Convert.ToInt32(row["n8"].ToString());
                                    num16 = Convert.ToInt32(row["n9"].ToString());
                                    num17 = Convert.ToInt32(row["n10"].ToString());
                                    list2.Add((num8 < 10) ? ("0" + num8) : num8.ToString());
                                    list2.Add((num9 < 10) ? ("0" + num9) : num9.ToString());
                                    list2.Add((num10 < 10) ? ("0" + num10) : num10.ToString());
                                    list2.Add((num11 < 10) ? ("0" + num11) : num11.ToString());
                                    list2.Add((num12 < 10) ? ("0" + num12) : num12.ToString());
                                    list2.Add((num13 < 10) ? ("0" + num13) : num13.ToString());
                                    list2.Add((num14 < 10) ? ("0" + num14) : num14.ToString());
                                    list2.Add((num15 < 10) ? ("0" + num15) : num15.ToString());
                                    list2.Add((num16 < 10) ? ("0" + num16) : num16.ToString());
                                    list2.Add((num17 < 10) ? ("0" + num17) : num17.ToString());
                                    num35 = num8 + num9;
                                    str27 = num35.ToString();
                                    if (Convert.ToInt32(str27) <= 11)
                                    {
                                        str28 = "小";
                                    }
                                    else
                                    {
                                        str28 = "大";
                                    }
                                    str28 = base.SetWordColor(str28);
                                    if ((Convert.ToInt32(str27) % 2) == 0)
                                    {
                                        str29 = "雙";
                                    }
                                    else
                                    {
                                        str29 = "單";
                                    }
                                    str29 = base.SetWordColor(str29);
                                    if (num8 > num17)
                                    {
                                        str30 = "龍";
                                    }
                                    else
                                    {
                                        str30 = "虎";
                                    }
                                    str30 = base.SetWordColor(str30);
                                    if (num9 > num16)
                                    {
                                        str31 = "龍";
                                    }
                                    else
                                    {
                                        str31 = "虎";
                                    }
                                    str31 = base.SetWordColor(str31);
                                    if (num10 > num15)
                                    {
                                        str32 = "龍";
                                    }
                                    else
                                    {
                                        str32 = "虎";
                                    }
                                    str32 = base.SetWordColor(str32);
                                    if (num11 > num14)
                                    {
                                        str33 = "龍";
                                    }
                                    else
                                    {
                                        str33 = "虎";
                                    }
                                    str33 = base.SetWordColor(str33);
                                    if (num12 > num13)
                                    {
                                        str34 = "龍";
                                    }
                                    else
                                    {
                                        str34 = "虎";
                                    }
                                    str34 = base.SetWordColor(str34);
                                    dictionary3.Add("phase", str6);
                                    dictionary3.Add("play_open_date", str18);
                                    dictionary3.Add("draw_num", new List<string>(list2));
                                    dictionary3.Add("total", new List<string> { str27, str28, str29, str30, str31, str32, str33, str34 });
                                    list.Add(new Dictionary<string, object>(dictionary3));
                                    dictionary3.Clear();
                                    list2.Clear();
                                }
                            }
                        }
                        dictionary.Add("jqkj", list);
                        break;

                    case 11:
                        list = new List<object>();
                        list2 = new List<string>();
                        currentByPhase = CallBLL.cz_phase_speed5_bll.GetCurrentByPhase(str2);
                        if (currentByPhase != null)
                        {
                            table = currentByPhase.Tables[0];
                            dictionary3 = new Dictionary<string, object>();
                            str4 = table.Rows[0]["is_closed"];
                            str5 = table.Rows[0]["is_opendata"];
                            if (str4.Equals("1") && str5.Equals("1"))
                            {
                                foreach (DataRow row in table.Rows)
                                {
                                    str6 = "";
                                    str18 = "";
                                    str22 = "";
                                    str15 = "";
                                    str16 = "";
                                    str23 = "";
                                    str24 = "";
                                    str25 = "";
                                    str26 = "";
                                    str6 = row["phase"].ToString();
                                    str18 = row["play_open_date"].ToString();
                                    num8 = Convert.ToInt32(row["n1"].ToString());
                                    num9 = Convert.ToInt32(row["n2"].ToString());
                                    num10 = Convert.ToInt32(row["n3"].ToString());
                                    num11 = Convert.ToInt32(row["n4"].ToString());
                                    num12 = Convert.ToInt32(row["n5"].ToString());
                                    list2.Add(num8.ToString());
                                    list2.Add(num9.ToString());
                                    list2.Add(num10.ToString());
                                    list2.Add(num11.ToString());
                                    list2.Add(num12.ToString());
                                    num35 = (((num8 + num9) + num10) + num11) + num12;
                                    str22 = num35.ToString();
                                    str15 = SPEED5Phase.get_cl_zhdx(str22);
                                    str15 = base.SetWordColor(str15);
                                    str16 = SPEED5Phase.get_cl_zhds(str22);
                                    str16 = base.SetWordColor(str16);
                                    str23 = SPEED5Phase.get_cl_lh(num8.ToString(), num12.ToString());
                                    str23 = base.SetWordColor(str23);
                                    str24 = SPEED5Phase.get_speed5_str(string.Concat(new object[] { num8, ",", num9, ",", num10 }));
                                    str25 = SPEED5Phase.get_speed5_str(string.Concat(new object[] { num9, ",", num10, ",", num11 }));
                                    str26 = SPEED5Phase.get_speed5_str(string.Concat(new object[] { num10, ",", num11, ",", num12 }));
                                    dictionary3.Add("phase", str6);
                                    dictionary3.Add("play_open_date", str18);
                                    dictionary3.Add("draw_num", new List<string>(list2));
                                    dictionary3.Add("total", new List<string> { str22, str15, str16, str23, str24, str25, str26 });
                                    list.Add(new Dictionary<string, object>(dictionary3));
                                    dictionary3.Clear();
                                    list2.Clear();
                                }
                            }
                        }
                        dictionary.Add("jqkj", list);
                        break;

                    case 12:
                        list = new List<object>();
                        list2 = new List<string>();
                        currentByPhase = CallBLL.cz_phase_jspk10_bll.GetCurrentByPhase(str2);
                        if (currentByPhase != null)
                        {
                            table = currentByPhase.Tables[0];
                            num = 5;
                            dictionary3 = new Dictionary<string, object>();
                            str4 = table.Rows[0]["is_closed"];
                            str5 = table.Rows[0]["is_opendata"];
                            if (str4.Equals("1") && str5.Equals("1"))
                            {
                                foreach (DataRow row in table.Rows)
                                {
                                    str6 = "";
                                    str18 = "";
                                    str27 = "";
                                    str28 = "";
                                    str29 = "";
                                    str30 = "";
                                    str31 = "";
                                    str32 = "";
                                    str33 = "";
                                    str34 = "";
                                    str6 = row["phase"].ToString();
                                    str18 = row["play_open_date"].ToString();
                                    num8 = Convert.ToInt32(row["n1"].ToString());
                                    num9 = Convert.ToInt32(row["n2"].ToString());
                                    num10 = Convert.ToInt32(row["n3"].ToString());
                                    num11 = Convert.ToInt32(row["n4"].ToString());
                                    num12 = Convert.ToInt32(row["n5"].ToString());
                                    num13 = Convert.ToInt32(row["n6"].ToString());
                                    num14 = Convert.ToInt32(row["n7"].ToString());
                                    num15 = Convert.ToInt32(row["n8"].ToString());
                                    num16 = Convert.ToInt32(row["n9"].ToString());
                                    num17 = Convert.ToInt32(row["n10"].ToString());
                                    list2.Add((num8 < 10) ? ("0" + num8) : num8.ToString());
                                    list2.Add((num9 < 10) ? ("0" + num9) : num9.ToString());
                                    list2.Add((num10 < 10) ? ("0" + num10) : num10.ToString());
                                    list2.Add((num11 < 10) ? ("0" + num11) : num11.ToString());
                                    list2.Add((num12 < 10) ? ("0" + num12) : num12.ToString());
                                    list2.Add((num13 < 10) ? ("0" + num13) : num13.ToString());
                                    list2.Add((num14 < 10) ? ("0" + num14) : num14.ToString());
                                    list2.Add((num15 < 10) ? ("0" + num15) : num15.ToString());
                                    list2.Add((num16 < 10) ? ("0" + num16) : num16.ToString());
                                    list2.Add((num17 < 10) ? ("0" + num17) : num17.ToString());
                                    num35 = num8 + num9;
                                    str27 = num35.ToString();
                                    if (Convert.ToInt32(str27) <= 11)
                                    {
                                        str28 = "小";
                                    }
                                    else
                                    {
                                        str28 = "大";
                                    }
                                    str28 = base.SetWordColor(str28);
                                    if ((Convert.ToInt32(str27) % 2) == 0)
                                    {
                                        str29 = "雙";
                                    }
                                    else
                                    {
                                        str29 = "單";
                                    }
                                    str29 = base.SetWordColor(str29);
                                    if (num8 > num17)
                                    {
                                        str30 = "龍";
                                    }
                                    else
                                    {
                                        str30 = "虎";
                                    }
                                    str30 = base.SetWordColor(str30);
                                    if (num9 > num16)
                                    {
                                        str31 = "龍";
                                    }
                                    else
                                    {
                                        str31 = "虎";
                                    }
                                    str31 = base.SetWordColor(str31);
                                    if (num10 > num15)
                                    {
                                        str32 = "龍";
                                    }
                                    else
                                    {
                                        str32 = "虎";
                                    }
                                    str32 = base.SetWordColor(str32);
                                    if (num11 > num14)
                                    {
                                        str33 = "龍";
                                    }
                                    else
                                    {
                                        str33 = "虎";
                                    }
                                    str33 = base.SetWordColor(str33);
                                    if (num12 > num13)
                                    {
                                        str34 = "龍";
                                    }
                                    else
                                    {
                                        str34 = "虎";
                                    }
                                    str34 = base.SetWordColor(str34);
                                    dictionary3.Add("phase", str6);
                                    dictionary3.Add("play_open_date", str18);
                                    dictionary3.Add("draw_num", new List<string>(list2));
                                    dictionary3.Add("total", new List<string> { str27, str28, str29, str30, str31, str32, str33, str34 });
                                    list.Add(new Dictionary<string, object>(dictionary3));
                                    dictionary3.Clear();
                                    list2.Clear();
                                }
                            }
                        }
                        dictionary.Add("jqkj", list);
                        break;

                    case 13:
                        list = new List<object>();
                        list2 = new List<string>();
                        currentByPhase = CallBLL.cz_phase_jscqsc_bll.GetCurrentByPhase(str2);
                        if (currentByPhase != null)
                        {
                            table = currentByPhase.Tables[0];
                            dictionary3 = new Dictionary<string, object>();
                            str4 = table.Rows[0]["is_closed"];
                            str5 = table.Rows[0]["is_opendata"];
                            if (str4.Equals("1") && str5.Equals("1"))
                            {
                                foreach (DataRow row in table.Rows)
                                {
                                    str6 = "";
                                    str18 = "";
                                    str22 = "";
                                    str15 = "";
                                    str16 = "";
                                    str23 = "";
                                    str24 = "";
                                    str25 = "";
                                    str26 = "";
                                    str6 = row["phase"].ToString();
                                    str18 = row["play_open_date"].ToString();
                                    num8 = Convert.ToInt32(row["n1"].ToString());
                                    num9 = Convert.ToInt32(row["n2"].ToString());
                                    num10 = Convert.ToInt32(row["n3"].ToString());
                                    num11 = Convert.ToInt32(row["n4"].ToString());
                                    num12 = Convert.ToInt32(row["n5"].ToString());
                                    list2.Add(num8.ToString());
                                    list2.Add(num9.ToString());
                                    list2.Add(num10.ToString());
                                    list2.Add(num11.ToString());
                                    list2.Add(num12.ToString());
                                    num35 = (((num8 + num9) + num10) + num11) + num12;
                                    str22 = num35.ToString();
                                    str15 = JSCQSCPhase.get_cl_zhdx(str22);
                                    str15 = base.SetWordColor(str15);
                                    str16 = JSCQSCPhase.get_cl_zhds(str22);
                                    str16 = base.SetWordColor(str16);
                                    str23 = JSCQSCPhase.get_cl_lh(num8.ToString(), num12.ToString());
                                    str23 = base.SetWordColor(str23);
                                    str24 = JSCQSCPhase.get_jscqsc_str(string.Concat(new object[] { num8, ",", num9, ",", num10 }));
                                    str25 = JSCQSCPhase.get_jscqsc_str(string.Concat(new object[] { num9, ",", num10, ",", num11 }));
                                    str26 = JSCQSCPhase.get_jscqsc_str(string.Concat(new object[] { num10, ",", num11, ",", num12 }));
                                    dictionary3.Add("phase", str6);
                                    dictionary3.Add("play_open_date", str18);
                                    dictionary3.Add("draw_num", new List<string>(list2));
                                    dictionary3.Add("total", new List<string> { str22, str15, str16, str23, str24, str25, str26 });
                                    list.Add(new Dictionary<string, object>(dictionary3));
                                    dictionary3.Clear();
                                    list2.Clear();
                                }
                            }
                        }
                        dictionary.Add("jqkj", list);
                        break;

                    case 14:
                        list = new List<object>();
                        list2 = new List<string>();
                        currentByPhase = CallBLL.cz_phase_jssfc_bll.GetCurrentByPhase(str2);
                        if (currentByPhase != null)
                        {
                            table = currentByPhase.Tables[0];
                            num = 8;
                            dictionary3 = new Dictionary<string, object>();
                            str4 = table.Rows[0]["is_closed"];
                            str5 = table.Rows[0]["is_opendata"];
                            if (str4.Equals("1") && str5.Equals("1"))
                            {
                                foreach (DataRow row in table.Rows)
                                {
                                    str6 = "";
                                    str18 = "";
                                    str19 = "";
                                    str15 = "";
                                    str16 = "";
                                    str20 = "";
                                    str21 = "";
                                    str6 = row["phase"].ToString();
                                    str18 = row["play_open_date"].ToString();
                                    for (num3 = 1; num3 <= num; num3++)
                                    {
                                        str17 = "n" + num3;
                                        list2.Add(row[str17].ToString());
                                    }
                                    str19 = JSSFCPhase.get_zh(list2).ToString();
                                    str15 = JSSFCPhase.get_cl_zhdx(str19);
                                    str15 = base.SetWordColor(str15);
                                    str16 = JSSFCPhase.get_cl_zhds(str19);
                                    str16 = base.SetWordColor(str16);
                                    str20 = JSSFCPhase.get_cl_zhwsdx(str19);
                                    str20 = base.SetWordColor(str20);
                                    str21 = JSSFCPhase.get_cl_lh(list2[0].ToString(), list2[num - 1].ToString());
                                    str21 = base.SetWordColor(str21);
                                    dictionary3.Add("phase", str6);
                                    dictionary3.Add("play_open_date", str18);
                                    dictionary3.Add("draw_num", new List<string>(list2));
                                    dictionary3.Add("total", new List<string> { str19, str15, str16, str20, str21 });
                                    list.Add(new Dictionary<string, object>(dictionary3));
                                    dictionary3.Clear();
                                    list2.Clear();
                                }
                            }
                        }
                        dictionary.Add("jqkj", list);
                        break;

                    case 15:
                        list = new List<object>();
                        list2 = new List<string>();
                        currentByPhase = CallBLL.cz_phase_jsft2_bll.GetCurrentByPhase(str2);
                        if (currentByPhase != null)
                        {
                            table = currentByPhase.Tables[0];
                            num = 5;
                            dictionary3 = new Dictionary<string, object>();
                            str4 = table.Rows[0]["is_closed"];
                            str5 = table.Rows[0]["is_opendata"];
                            if (str4.Equals("1") && str5.Equals("1"))
                            {
                                foreach (DataRow row in table.Rows)
                                {
                                    str6 = "";
                                    str18 = "";
                                    str27 = "";
                                    str28 = "";
                                    str29 = "";
                                    str30 = "";
                                    str31 = "";
                                    str32 = "";
                                    str33 = "";
                                    str34 = "";
                                    str6 = row["phase"].ToString();
                                    str18 = row["play_open_date"].ToString();
                                    num8 = Convert.ToInt32(row["n1"].ToString());
                                    num9 = Convert.ToInt32(row["n2"].ToString());
                                    num10 = Convert.ToInt32(row["n3"].ToString());
                                    num11 = Convert.ToInt32(row["n4"].ToString());
                                    num12 = Convert.ToInt32(row["n5"].ToString());
                                    num13 = Convert.ToInt32(row["n6"].ToString());
                                    num14 = Convert.ToInt32(row["n7"].ToString());
                                    num15 = Convert.ToInt32(row["n8"].ToString());
                                    num16 = Convert.ToInt32(row["n9"].ToString());
                                    num17 = Convert.ToInt32(row["n10"].ToString());
                                    list2.Add((num8 < 10) ? ("0" + num8) : num8.ToString());
                                    list2.Add((num9 < 10) ? ("0" + num9) : num9.ToString());
                                    list2.Add((num10 < 10) ? ("0" + num10) : num10.ToString());
                                    list2.Add((num11 < 10) ? ("0" + num11) : num11.ToString());
                                    list2.Add((num12 < 10) ? ("0" + num12) : num12.ToString());
                                    list2.Add((num13 < 10) ? ("0" + num13) : num13.ToString());
                                    list2.Add((num14 < 10) ? ("0" + num14) : num14.ToString());
                                    list2.Add((num15 < 10) ? ("0" + num15) : num15.ToString());
                                    list2.Add((num16 < 10) ? ("0" + num16) : num16.ToString());
                                    list2.Add((num17 < 10) ? ("0" + num17) : num17.ToString());
                                    num35 = num8 + num9;
                                    str27 = num35.ToString();
                                    if (Convert.ToInt32(str27) <= 11)
                                    {
                                        str28 = "小";
                                    }
                                    else
                                    {
                                        str28 = "大";
                                    }
                                    str28 = base.SetWordColor(str28);
                                    if ((Convert.ToInt32(str27) % 2) == 0)
                                    {
                                        str29 = "雙";
                                    }
                                    else
                                    {
                                        str29 = "單";
                                    }
                                    str29 = base.SetWordColor(str29);
                                    if (num8 > num17)
                                    {
                                        str30 = "龍";
                                    }
                                    else
                                    {
                                        str30 = "虎";
                                    }
                                    str30 = base.SetWordColor(str30);
                                    if (num9 > num16)
                                    {
                                        str31 = "龍";
                                    }
                                    else
                                    {
                                        str31 = "虎";
                                    }
                                    str31 = base.SetWordColor(str31);
                                    if (num10 > num15)
                                    {
                                        str32 = "龍";
                                    }
                                    else
                                    {
                                        str32 = "虎";
                                    }
                                    str32 = base.SetWordColor(str32);
                                    if (num11 > num14)
                                    {
                                        str33 = "龍";
                                    }
                                    else
                                    {
                                        str33 = "虎";
                                    }
                                    str33 = base.SetWordColor(str33);
                                    if (num12 > num13)
                                    {
                                        str34 = "龍";
                                    }
                                    else
                                    {
                                        str34 = "虎";
                                    }
                                    str34 = base.SetWordColor(str34);
                                    dictionary3.Add("phase", str6);
                                    dictionary3.Add("play_open_date", str18);
                                    dictionary3.Add("draw_num", new List<string>(list2));
                                    dictionary3.Add("total", new List<string> { str27, str28, str29, str30, str31, str32, str33, str34 });
                                    list.Add(new Dictionary<string, object>(dictionary3));
                                    dictionary3.Clear();
                                    list2.Clear();
                                }
                            }
                        }
                        dictionary.Add("jqkj", list);
                        break;

                    case 0x10:
                        list = new List<object>();
                        list2 = new List<string>();
                        currentByPhase = CallBLL.cz_phase_car168_bll.GetCurrentByPhase(str2);
                        if (currentByPhase != null)
                        {
                            table = currentByPhase.Tables[0];
                            num = 5;
                            dictionary3 = new Dictionary<string, object>();
                            str4 = table.Rows[0]["is_closed"];
                            str5 = table.Rows[0]["is_opendata"];
                            if (str4.Equals("1") && str5.Equals("1"))
                            {
                                foreach (DataRow row in table.Rows)
                                {
                                    str6 = "";
                                    str18 = "";
                                    str27 = "";
                                    str28 = "";
                                    str29 = "";
                                    str30 = "";
                                    str31 = "";
                                    str32 = "";
                                    str33 = "";
                                    str34 = "";
                                    str6 = row["phase"].ToString();
                                    str18 = row["play_open_date"].ToString();
                                    num8 = Convert.ToInt32(row["n1"].ToString());
                                    num9 = Convert.ToInt32(row["n2"].ToString());
                                    num10 = Convert.ToInt32(row["n3"].ToString());
                                    num11 = Convert.ToInt32(row["n4"].ToString());
                                    num12 = Convert.ToInt32(row["n5"].ToString());
                                    num13 = Convert.ToInt32(row["n6"].ToString());
                                    num14 = Convert.ToInt32(row["n7"].ToString());
                                    num15 = Convert.ToInt32(row["n8"].ToString());
                                    num16 = Convert.ToInt32(row["n9"].ToString());
                                    num17 = Convert.ToInt32(row["n10"].ToString());
                                    list2.Add((num8 < 10) ? ("0" + num8) : num8.ToString());
                                    list2.Add((num9 < 10) ? ("0" + num9) : num9.ToString());
                                    list2.Add((num10 < 10) ? ("0" + num10) : num10.ToString());
                                    list2.Add((num11 < 10) ? ("0" + num11) : num11.ToString());
                                    list2.Add((num12 < 10) ? ("0" + num12) : num12.ToString());
                                    list2.Add((num13 < 10) ? ("0" + num13) : num13.ToString());
                                    list2.Add((num14 < 10) ? ("0" + num14) : num14.ToString());
                                    list2.Add((num15 < 10) ? ("0" + num15) : num15.ToString());
                                    list2.Add((num16 < 10) ? ("0" + num16) : num16.ToString());
                                    list2.Add((num17 < 10) ? ("0" + num17) : num17.ToString());
                                    num35 = num8 + num9;
                                    str27 = num35.ToString();
                                    if (Convert.ToInt32(str27) <= 11)
                                    {
                                        str28 = "小";
                                    }
                                    else
                                    {
                                        str28 = "大";
                                    }
                                    str28 = base.SetWordColor(str28);
                                    if ((Convert.ToInt32(str27) % 2) == 0)
                                    {
                                        str29 = "雙";
                                    }
                                    else
                                    {
                                        str29 = "單";
                                    }
                                    str29 = base.SetWordColor(str29);
                                    if (num8 > num17)
                                    {
                                        str30 = "龍";
                                    }
                                    else
                                    {
                                        str30 = "虎";
                                    }
                                    str30 = base.SetWordColor(str30);
                                    if (num9 > num16)
                                    {
                                        str31 = "龍";
                                    }
                                    else
                                    {
                                        str31 = "虎";
                                    }
                                    str31 = base.SetWordColor(str31);
                                    if (num10 > num15)
                                    {
                                        str32 = "龍";
                                    }
                                    else
                                    {
                                        str32 = "虎";
                                    }
                                    str32 = base.SetWordColor(str32);
                                    if (num11 > num14)
                                    {
                                        str33 = "龍";
                                    }
                                    else
                                    {
                                        str33 = "虎";
                                    }
                                    str33 = base.SetWordColor(str33);
                                    if (num12 > num13)
                                    {
                                        str34 = "龍";
                                    }
                                    else
                                    {
                                        str34 = "虎";
                                    }
                                    str34 = base.SetWordColor(str34);
                                    dictionary3.Add("phase", str6);
                                    dictionary3.Add("play_open_date", str18);
                                    dictionary3.Add("draw_num", new List<string>(list2));
                                    dictionary3.Add("total", new List<string> { str27, str28, str29, str30, str31, str32, str33, str34 });
                                    list.Add(new Dictionary<string, object>(dictionary3));
                                    dictionary3.Clear();
                                    list2.Clear();
                                }
                            }
                        }
                        dictionary.Add("jqkj", list);
                        break;

                    case 0x11:
                        list = new List<object>();
                        list2 = new List<string>();
                        currentByPhase = CallBLL.cz_phase_ssc168_bll.GetCurrentByPhase(str2);
                        if (currentByPhase != null)
                        {
                            table = currentByPhase.Tables[0];
                            dictionary3 = new Dictionary<string, object>();
                            str4 = table.Rows[0]["is_closed"];
                            str5 = table.Rows[0]["is_opendata"];
                            if (str4.Equals("1") && str5.Equals("1"))
                            {
                                foreach (DataRow row in table.Rows)
                                {
                                    str6 = "";
                                    str18 = "";
                                    str22 = "";
                                    str15 = "";
                                    str16 = "";
                                    str23 = "";
                                    str24 = "";
                                    str25 = "";
                                    str26 = "";
                                    str6 = row["phase"].ToString();
                                    str18 = row["play_open_date"].ToString();
                                    num8 = Convert.ToInt32(row["n1"].ToString());
                                    num9 = Convert.ToInt32(row["n2"].ToString());
                                    num10 = Convert.ToInt32(row["n3"].ToString());
                                    num11 = Convert.ToInt32(row["n4"].ToString());
                                    num12 = Convert.ToInt32(row["n5"].ToString());
                                    list2.Add(num8.ToString());
                                    list2.Add(num9.ToString());
                                    list2.Add(num10.ToString());
                                    list2.Add(num11.ToString());
                                    list2.Add(num12.ToString());
                                    num35 = (((num8 + num9) + num10) + num11) + num12;
                                    str22 = num35.ToString();
                                    str15 = SSC168Phase.get_cl_zhdx(str22);
                                    str15 = base.SetWordColor(str15);
                                    str16 = SSC168Phase.get_cl_zhds(str22);
                                    str16 = base.SetWordColor(str16);
                                    str23 = SSC168Phase.get_cl_lh(num8.ToString(), num12.ToString());
                                    str23 = base.SetWordColor(str23);
                                    str24 = SSC168Phase.get_ssc168_str(string.Concat(new object[] { num8, ",", num9, ",", num10 }));
                                    str25 = SSC168Phase.get_ssc168_str(string.Concat(new object[] { num9, ",", num10, ",", num11 }));
                                    str26 = SSC168Phase.get_ssc168_str(string.Concat(new object[] { num10, ",", num11, ",", num12 }));
                                    dictionary3.Add("phase", str6);
                                    dictionary3.Add("play_open_date", str18);
                                    dictionary3.Add("draw_num", new List<string>(list2));
                                    dictionary3.Add("total", new List<string> { str22, str15, str16, str23, str24, str25, str26 });
                                    list.Add(new Dictionary<string, object>(dictionary3));
                                    dictionary3.Clear();
                                    list2.Clear();
                                }
                            }
                        }
                        dictionary.Add("jqkj", list);
                        break;

                    case 0x12:
                        list = new List<object>();
                        list2 = new List<string>();
                        currentByPhase = CallBLL.cz_phase_vrcar_bll.GetCurrentByPhase(str2);
                        if (currentByPhase != null)
                        {
                            table = currentByPhase.Tables[0];
                            num = 5;
                            dictionary3 = new Dictionary<string, object>();
                            str4 = table.Rows[0]["is_closed"];
                            str5 = table.Rows[0]["is_opendata"];
                            if (str4.Equals("1") && str5.Equals("1"))
                            {
                                foreach (DataRow row in table.Rows)
                                {
                                    str6 = "";
                                    str18 = "";
                                    str27 = "";
                                    str28 = "";
                                    str29 = "";
                                    str30 = "";
                                    str31 = "";
                                    str32 = "";
                                    str33 = "";
                                    str34 = "";
                                    str6 = row["phase"].ToString();
                                    str18 = row["play_open_date"].ToString();
                                    num8 = Convert.ToInt32(row["n1"].ToString());
                                    num9 = Convert.ToInt32(row["n2"].ToString());
                                    num10 = Convert.ToInt32(row["n3"].ToString());
                                    num11 = Convert.ToInt32(row["n4"].ToString());
                                    num12 = Convert.ToInt32(row["n5"].ToString());
                                    num13 = Convert.ToInt32(row["n6"].ToString());
                                    num14 = Convert.ToInt32(row["n7"].ToString());
                                    num15 = Convert.ToInt32(row["n8"].ToString());
                                    num16 = Convert.ToInt32(row["n9"].ToString());
                                    num17 = Convert.ToInt32(row["n10"].ToString());
                                    list2.Add((num8 < 10) ? ("0" + num8) : num8.ToString());
                                    list2.Add((num9 < 10) ? ("0" + num9) : num9.ToString());
                                    list2.Add((num10 < 10) ? ("0" + num10) : num10.ToString());
                                    list2.Add((num11 < 10) ? ("0" + num11) : num11.ToString());
                                    list2.Add((num12 < 10) ? ("0" + num12) : num12.ToString());
                                    list2.Add((num13 < 10) ? ("0" + num13) : num13.ToString());
                                    list2.Add((num14 < 10) ? ("0" + num14) : num14.ToString());
                                    list2.Add((num15 < 10) ? ("0" + num15) : num15.ToString());
                                    list2.Add((num16 < 10) ? ("0" + num16) : num16.ToString());
                                    list2.Add((num17 < 10) ? ("0" + num17) : num17.ToString());
                                    num35 = num8 + num9;
                                    str27 = num35.ToString();
                                    if (Convert.ToInt32(str27) <= 11)
                                    {
                                        str28 = "小";
                                    }
                                    else
                                    {
                                        str28 = "大";
                                    }
                                    str28 = base.SetWordColor(str28);
                                    if ((Convert.ToInt32(str27) % 2) == 0)
                                    {
                                        str29 = "雙";
                                    }
                                    else
                                    {
                                        str29 = "單";
                                    }
                                    str29 = base.SetWordColor(str29);
                                    if (num8 > num17)
                                    {
                                        str30 = "龍";
                                    }
                                    else
                                    {
                                        str30 = "虎";
                                    }
                                    str30 = base.SetWordColor(str30);
                                    if (num9 > num16)
                                    {
                                        str31 = "龍";
                                    }
                                    else
                                    {
                                        str31 = "虎";
                                    }
                                    str31 = base.SetWordColor(str31);
                                    if (num10 > num15)
                                    {
                                        str32 = "龍";
                                    }
                                    else
                                    {
                                        str32 = "虎";
                                    }
                                    str32 = base.SetWordColor(str32);
                                    if (num11 > num14)
                                    {
                                        str33 = "龍";
                                    }
                                    else
                                    {
                                        str33 = "虎";
                                    }
                                    str33 = base.SetWordColor(str33);
                                    if (num12 > num13)
                                    {
                                        str34 = "龍";
                                    }
                                    else
                                    {
                                        str34 = "虎";
                                    }
                                    str34 = base.SetWordColor(str34);
                                    dictionary3.Add("phase", str6);
                                    dictionary3.Add("play_open_date", str18);
                                    dictionary3.Add("draw_num", new List<string>(list2));
                                    dictionary3.Add("total", new List<string> { str27, str28, str29, str30, str31, str32, str33, str34 });
                                    list.Add(new Dictionary<string, object>(dictionary3));
                                    dictionary3.Clear();
                                    list2.Clear();
                                }
                            }
                        }
                        dictionary.Add("jqkj", list);
                        break;

                    case 0x13:
                        list = new List<object>();
                        list2 = new List<string>();
                        currentByPhase = CallBLL.cz_phase_vrssc_bll.GetCurrentByPhase(str2);
                        if (currentByPhase != null)
                        {
                            table = currentByPhase.Tables[0];
                            dictionary3 = new Dictionary<string, object>();
                            str4 = table.Rows[0]["is_closed"];
                            str5 = table.Rows[0]["is_opendata"];
                            if (str4.Equals("1") && str5.Equals("1"))
                            {
                                foreach (DataRow row in table.Rows)
                                {
                                    str6 = "";
                                    str18 = "";
                                    str22 = "";
                                    str15 = "";
                                    str16 = "";
                                    str23 = "";
                                    str24 = "";
                                    str25 = "";
                                    str26 = "";
                                    str6 = row["phase"].ToString();
                                    str18 = row["play_open_date"].ToString();
                                    num8 = Convert.ToInt32(row["n1"].ToString());
                                    num9 = Convert.ToInt32(row["n2"].ToString());
                                    num10 = Convert.ToInt32(row["n3"].ToString());
                                    num11 = Convert.ToInt32(row["n4"].ToString());
                                    num12 = Convert.ToInt32(row["n5"].ToString());
                                    list2.Add(num8.ToString());
                                    list2.Add(num9.ToString());
                                    list2.Add(num10.ToString());
                                    list2.Add(num11.ToString());
                                    list2.Add(num12.ToString());
                                    num35 = (((num8 + num9) + num10) + num11) + num12;
                                    str22 = num35.ToString();
                                    str15 = VRSSCPhase.get_cl_zhdx(str22);
                                    str15 = base.SetWordColor(str15);
                                    str16 = VRSSCPhase.get_cl_zhds(str22);
                                    str16 = base.SetWordColor(str16);
                                    str23 = VRSSCPhase.get_cl_lh(num8.ToString(), num12.ToString());
                                    str23 = base.SetWordColor(str23);
                                    str24 = VRSSCPhase.get_vrssc_str(string.Concat(new object[] { num8, ",", num9, ",", num10 }));
                                    str25 = VRSSCPhase.get_vrssc_str(string.Concat(new object[] { num9, ",", num10, ",", num11 }));
                                    str26 = VRSSCPhase.get_vrssc_str(string.Concat(new object[] { num10, ",", num11, ",", num12 }));
                                    dictionary3.Add("phase", str6);
                                    dictionary3.Add("play_open_date", str18);
                                    dictionary3.Add("draw_num", new List<string>(list2));
                                    dictionary3.Add("total", new List<string> { str22, str15, str16, str23, str24, str25, str26 });
                                    list.Add(new Dictionary<string, object>(dictionary3));
                                    dictionary3.Clear();
                                    list2.Clear();
                                }
                            }
                        }
                        dictionary.Add("jqkj", list);
                        break;

                    case 20:
                        list = new List<object>();
                        list2 = new List<string>();
                        currentByPhase = CallBLL.cz_phase_xyftoa_bll.GetCurrentByPhase(str2);
                        if (currentByPhase != null)
                        {
                            table = currentByPhase.Tables[0];
                            num = 5;
                            dictionary3 = new Dictionary<string, object>();
                            str4 = table.Rows[0]["is_closed"];
                            str5 = table.Rows[0]["is_opendata"];
                            if (str4.Equals("1") && str5.Equals("1"))
                            {
                                foreach (DataRow row in table.Rows)
                                {
                                    str6 = "";
                                    str18 = "";
                                    str27 = "";
                                    str28 = "";
                                    str29 = "";
                                    str30 = "";
                                    str31 = "";
                                    str32 = "";
                                    str33 = "";
                                    str34 = "";
                                    str6 = row["phase"].ToString();
                                    str18 = row["play_open_date"].ToString();
                                    num8 = Convert.ToInt32(row["n1"].ToString());
                                    num9 = Convert.ToInt32(row["n2"].ToString());
                                    num10 = Convert.ToInt32(row["n3"].ToString());
                                    num11 = Convert.ToInt32(row["n4"].ToString());
                                    num12 = Convert.ToInt32(row["n5"].ToString());
                                    num13 = Convert.ToInt32(row["n6"].ToString());
                                    num14 = Convert.ToInt32(row["n7"].ToString());
                                    num15 = Convert.ToInt32(row["n8"].ToString());
                                    num16 = Convert.ToInt32(row["n9"].ToString());
                                    num17 = Convert.ToInt32(row["n10"].ToString());
                                    list2.Add((num8 < 10) ? ("0" + num8) : num8.ToString());
                                    list2.Add((num9 < 10) ? ("0" + num9) : num9.ToString());
                                    list2.Add((num10 < 10) ? ("0" + num10) : num10.ToString());
                                    list2.Add((num11 < 10) ? ("0" + num11) : num11.ToString());
                                    list2.Add((num12 < 10) ? ("0" + num12) : num12.ToString());
                                    list2.Add((num13 < 10) ? ("0" + num13) : num13.ToString());
                                    list2.Add((num14 < 10) ? ("0" + num14) : num14.ToString());
                                    list2.Add((num15 < 10) ? ("0" + num15) : num15.ToString());
                                    list2.Add((num16 < 10) ? ("0" + num16) : num16.ToString());
                                    list2.Add((num17 < 10) ? ("0" + num17) : num17.ToString());
                                    num35 = num8 + num9;
                                    str27 = num35.ToString();
                                    if (Convert.ToInt32(str27) <= 11)
                                    {
                                        str28 = "小";
                                    }
                                    else
                                    {
                                        str28 = "大";
                                    }
                                    str28 = base.SetWordColor(str28);
                                    if ((Convert.ToInt32(str27) % 2) == 0)
                                    {
                                        str29 = "雙";
                                    }
                                    else
                                    {
                                        str29 = "單";
                                    }
                                    str29 = base.SetWordColor(str29);
                                    if (num8 > num17)
                                    {
                                        str30 = "龍";
                                    }
                                    else
                                    {
                                        str30 = "虎";
                                    }
                                    str30 = base.SetWordColor(str30);
                                    if (num9 > num16)
                                    {
                                        str31 = "龍";
                                    }
                                    else
                                    {
                                        str31 = "虎";
                                    }
                                    str31 = base.SetWordColor(str31);
                                    if (num10 > num15)
                                    {
                                        str32 = "龍";
                                    }
                                    else
                                    {
                                        str32 = "虎";
                                    }
                                    str32 = base.SetWordColor(str32);
                                    if (num11 > num14)
                                    {
                                        str33 = "龍";
                                    }
                                    else
                                    {
                                        str33 = "虎";
                                    }
                                    str33 = base.SetWordColor(str33);
                                    if (num12 > num13)
                                    {
                                        str34 = "龍";
                                    }
                                    else
                                    {
                                        str34 = "虎";
                                    }
                                    str34 = base.SetWordColor(str34);
                                    dictionary3.Add("phase", str6);
                                    dictionary3.Add("play_open_date", str18);
                                    dictionary3.Add("draw_num", new List<string>(list2));
                                    dictionary3.Add("total", new List<string> { str27, str28, str29, str30, str31, str32, str33, str34 });
                                    list.Add(new Dictionary<string, object>(dictionary3));
                                    dictionary3.Clear();
                                    list2.Clear();
                                }
                            }
                        }
                        dictionary.Add("jqkj", list);
                        break;

                    case 0x15:
                        list = new List<object>();
                        list2 = new List<string>();
                        currentByPhase = CallBLL.cz_phase_xyftsg_bll.GetCurrentByPhase(str2);
                        if (currentByPhase != null)
                        {
                            table = currentByPhase.Tables[0];
                            num = 5;
                            dictionary3 = new Dictionary<string, object>();
                            str4 = table.Rows[0]["is_closed"];
                            str5 = table.Rows[0]["is_opendata"];
                            if (str4.Equals("1") && str5.Equals("1"))
                            {
                                foreach (DataRow row in table.Rows)
                                {
                                    str6 = "";
                                    str18 = "";
                                    str27 = "";
                                    str28 = "";
                                    str29 = "";
                                    str30 = "";
                                    str31 = "";
                                    str32 = "";
                                    str33 = "";
                                    str34 = "";
                                    str6 = row["phase"].ToString();
                                    str18 = row["play_open_date"].ToString();
                                    num8 = Convert.ToInt32(row["n1"].ToString());
                                    num9 = Convert.ToInt32(row["n2"].ToString());
                                    num10 = Convert.ToInt32(row["n3"].ToString());
                                    num11 = Convert.ToInt32(row["n4"].ToString());
                                    num12 = Convert.ToInt32(row["n5"].ToString());
                                    num13 = Convert.ToInt32(row["n6"].ToString());
                                    num14 = Convert.ToInt32(row["n7"].ToString());
                                    num15 = Convert.ToInt32(row["n8"].ToString());
                                    num16 = Convert.ToInt32(row["n9"].ToString());
                                    num17 = Convert.ToInt32(row["n10"].ToString());
                                    list2.Add((num8 < 10) ? ("0" + num8) : num8.ToString());
                                    list2.Add((num9 < 10) ? ("0" + num9) : num9.ToString());
                                    list2.Add((num10 < 10) ? ("0" + num10) : num10.ToString());
                                    list2.Add((num11 < 10) ? ("0" + num11) : num11.ToString());
                                    list2.Add((num12 < 10) ? ("0" + num12) : num12.ToString());
                                    list2.Add((num13 < 10) ? ("0" + num13) : num13.ToString());
                                    list2.Add((num14 < 10) ? ("0" + num14) : num14.ToString());
                                    list2.Add((num15 < 10) ? ("0" + num15) : num15.ToString());
                                    list2.Add((num16 < 10) ? ("0" + num16) : num16.ToString());
                                    list2.Add((num17 < 10) ? ("0" + num17) : num17.ToString());
                                    num35 = num8 + num9;
                                    str27 = num35.ToString();
                                    if (Convert.ToInt32(str27) <= 11)
                                    {
                                        str28 = "小";
                                    }
                                    else
                                    {
                                        str28 = "大";
                                    }
                                    str28 = base.SetWordColor(str28);
                                    if ((Convert.ToInt32(str27) % 2) == 0)
                                    {
                                        str29 = "雙";
                                    }
                                    else
                                    {
                                        str29 = "單";
                                    }
                                    str29 = base.SetWordColor(str29);
                                    if (num8 > num17)
                                    {
                                        str30 = "龍";
                                    }
                                    else
                                    {
                                        str30 = "虎";
                                    }
                                    str30 = base.SetWordColor(str30);
                                    if (num9 > num16)
                                    {
                                        str31 = "龍";
                                    }
                                    else
                                    {
                                        str31 = "虎";
                                    }
                                    str31 = base.SetWordColor(str31);
                                    if (num10 > num15)
                                    {
                                        str32 = "龍";
                                    }
                                    else
                                    {
                                        str32 = "虎";
                                    }
                                    str32 = base.SetWordColor(str32);
                                    if (num11 > num14)
                                    {
                                        str33 = "龍";
                                    }
                                    else
                                    {
                                        str33 = "虎";
                                    }
                                    str33 = base.SetWordColor(str33);
                                    if (num12 > num13)
                                    {
                                        str34 = "龍";
                                    }
                                    else
                                    {
                                        str34 = "虎";
                                    }
                                    str34 = base.SetWordColor(str34);
                                    dictionary3.Add("phase", str6);
                                    dictionary3.Add("play_open_date", str18);
                                    dictionary3.Add("draw_num", new List<string>(list2));
                                    dictionary3.Add("total", new List<string> { str27, str28, str29, str30, str31, str32, str33, str34 });
                                    list.Add(new Dictionary<string, object>(dictionary3));
                                    dictionary3.Clear();
                                    list2.Clear();
                                }
                            }
                        }
                        dictionary.Add("jqkj", list);
                        break;

                    case 0x16:
                        list = new List<object>();
                        list2 = new List<string>();
                        currentByPhase = CallBLL.cz_phase_happycar_bll.GetCurrentByPhase(str2);
                        if (currentByPhase != null)
                        {
                            table = currentByPhase.Tables[0];
                            num = 5;
                            dictionary3 = new Dictionary<string, object>();
                            str4 = table.Rows[0]["is_closed"];
                            str5 = table.Rows[0]["is_opendata"];
                            if (str4.Equals("1") && str5.Equals("1"))
                            {
                                foreach (DataRow row in table.Rows)
                                {
                                    str6 = "";
                                    str18 = "";
                                    str27 = "";
                                    str28 = "";
                                    str29 = "";
                                    str30 = "";
                                    str31 = "";
                                    str32 = "";
                                    str33 = "";
                                    str34 = "";
                                    str6 = row["phase"].ToString();
                                    str18 = row["play_open_date"].ToString();
                                    num8 = Convert.ToInt32(row["n1"].ToString());
                                    num9 = Convert.ToInt32(row["n2"].ToString());
                                    num10 = Convert.ToInt32(row["n3"].ToString());
                                    num11 = Convert.ToInt32(row["n4"].ToString());
                                    num12 = Convert.ToInt32(row["n5"].ToString());
                                    num13 = Convert.ToInt32(row["n6"].ToString());
                                    num14 = Convert.ToInt32(row["n7"].ToString());
                                    num15 = Convert.ToInt32(row["n8"].ToString());
                                    num16 = Convert.ToInt32(row["n9"].ToString());
                                    num17 = Convert.ToInt32(row["n10"].ToString());
                                    list2.Add((num8 < 10) ? ("0" + num8) : num8.ToString());
                                    list2.Add((num9 < 10) ? ("0" + num9) : num9.ToString());
                                    list2.Add((num10 < 10) ? ("0" + num10) : num10.ToString());
                                    list2.Add((num11 < 10) ? ("0" + num11) : num11.ToString());
                                    list2.Add((num12 < 10) ? ("0" + num12) : num12.ToString());
                                    list2.Add((num13 < 10) ? ("0" + num13) : num13.ToString());
                                    list2.Add((num14 < 10) ? ("0" + num14) : num14.ToString());
                                    list2.Add((num15 < 10) ? ("0" + num15) : num15.ToString());
                                    list2.Add((num16 < 10) ? ("0" + num16) : num16.ToString());
                                    list2.Add((num17 < 10) ? ("0" + num17) : num17.ToString());
                                    str27 = (num8 + num9).ToString();
                                    if (Convert.ToInt32(str27) <= 11)
                                    {
                                        str28 = "小";
                                    }
                                    else
                                    {
                                        str28 = "大";
                                    }
                                    str28 = base.SetWordColor(str28);
                                    if ((Convert.ToInt32(str27) % 2) == 0)
                                    {
                                        str29 = "雙";
                                    }
                                    else
                                    {
                                        str29 = "單";
                                    }
                                    str29 = base.SetWordColor(str29);
                                    if (num8 > num17)
                                    {
                                        str30 = "龍";
                                    }
                                    else
                                    {
                                        str30 = "虎";
                                    }
                                    str30 = base.SetWordColor(str30);
                                    if (num9 > num16)
                                    {
                                        str31 = "龍";
                                    }
                                    else
                                    {
                                        str31 = "虎";
                                    }
                                    str31 = base.SetWordColor(str31);
                                    if (num10 > num15)
                                    {
                                        str32 = "龍";
                                    }
                                    else
                                    {
                                        str32 = "虎";
                                    }
                                    str32 = base.SetWordColor(str32);
                                    if (num11 > num14)
                                    {
                                        str33 = "龍";
                                    }
                                    else
                                    {
                                        str33 = "虎";
                                    }
                                    str33 = base.SetWordColor(str33);
                                    if (num12 > num13)
                                    {
                                        str34 = "龍";
                                    }
                                    else
                                    {
                                        str34 = "虎";
                                    }
                                    str34 = base.SetWordColor(str34);
                                    dictionary3.Add("phase", str6);
                                    dictionary3.Add("play_open_date", str18);
                                    dictionary3.Add("draw_num", new List<string>(list2));
                                    dictionary3.Add("total", new List<string> { str27, str28, str29, str30, str31, str32, str33, str34 });
                                    list.Add(new Dictionary<string, object>(dictionary3));
                                    dictionary3.Clear();
                                    list2.Clear();
                                }
                            }
                        }
                        dictionary.Add("jqkj", list);
                        break;

                    case 100:
                        list = new List<object>();
                        currentByPhase = CallBLL.cz_phase_six_bll.GetCurrentByPhase(str2);
                        if (currentByPhase != null)
                        {
                            list2 = new List<string>();
                            table = currentByPhase.Tables[0];
                            num = 6;
                            dictionary3 = new Dictionary<string, object>();
                            str4 = table.Rows[0]["is_closed"];
                            str5 = table.Rows[0]["is_opendata"];
                            if (str4.Equals("1") && str5.Equals("1"))
                            {
                                foreach (DataRow row in table.Rows)
                                {
                                    str6 = "";
                                    string str7 = "";
                                    string str8 = "";
                                    string str9 = "";
                                    str10 = "";
                                    str11 = "";
                                    string str12 = "";
                                    string str13 = "";
                                    string str14 = "";
                                    str15 = "";
                                    num2 = 0;
                                    str16 = "";
                                    str6 = row["phase"].ToString();
                                    str7 = row["sn_stop_date"].ToString();
                                    for (num3 = 1; num3 <= num; num3++)
                                    {
                                        str17 = "n" + num3;
                                        list2.Add(row[str17].ToString());
                                        num2 += Convert.ToInt32(row[str17]);
                                        if (num3.Equals(1))
                                        {
                                            str8 = str8 + row[str17].ToString();
                                        }
                                        else
                                        {
                                            str8 = str8 + "," + row[str17].ToString();
                                        }
                                    }
                                    list2.Add(row["sn"].ToString());
                                    num2 += Convert.ToInt32(row["sn"].ToString());
                                    str9 = row["sn"].ToString();
                                    if (double.Parse(row["sn"].ToString().Trim()) == 49.0)
                                    {
                                        str10 = base.SetWordColor("和");
                                    }
                                    else if ((double.Parse(row["sn"].ToString().Trim()) % 2.0) == 0.0)
                                    {
                                        str10 = base.SetWordColor("雙");
                                    }
                                    else
                                    {
                                        str10 = base.SetWordColor("單");
                                    }
                                    if (double.Parse(row["sn"].ToString().Trim()) <= 24.0)
                                    {
                                        str11 = base.SetWordColor("小");
                                    }
                                    else if (double.Parse(row["sn"].ToString().Trim()) == 49.0)
                                    {
                                        str11 = base.SetWordColor("和");
                                    }
                                    else
                                    {
                                        str11 = base.SetWordColor("大");
                                    }
                                    int num4 = 0;
                                    num4 = int.Parse(row["sn"].ToString().Trim()) % 10;
                                    if (double.Parse(row["sn"].ToString().Trim()) == 49.0)
                                    {
                                        str12 = base.SetWordColor("和");
                                    }
                                    else if (num4 <= 4)
                                    {
                                        str12 = base.SetWordColor("小");
                                    }
                                    else
                                    {
                                        str12 = base.SetWordColor("大");
                                    }
                                    str13 = this.get_tz(row["sn"].ToString().Trim());
                                    int num5 = int.Parse(row["sn"].ToString().Trim()) / 10;
                                    int num6 = int.Parse(row["sn"].ToString().Trim()) % 10;
                                    int num7 = num5 + num6;
                                    if (int.Parse(row["sn"].ToString().Trim()) == 0x31)
                                    {
                                        str14 = base.SetWordColor("和");
                                    }
                                    else if ((num7 % 2) == 0)
                                    {
                                        str14 = base.SetWordColor("雙");
                                    }
                                    else
                                    {
                                        str14 = base.SetWordColor("單");
                                    }
                                    if ((Convert.ToInt32(num2) % 2) == 0)
                                    {
                                        str16 = base.SetWordColor("雙");
                                    }
                                    else
                                    {
                                        str16 = base.SetWordColor("單");
                                    }
                                    if (Convert.ToInt32(num2) <= 0xae)
                                    {
                                        str15 = base.SetWordColor("小");
                                    }
                                    else
                                    {
                                        str15 = base.SetWordColor("大");
                                    }
                                    dictionary3.Add("phase", str6);
                                    dictionary3.Add("play_open_date", Convert.ToDateTime(str7).ToString("yyyy/MM/dd"));
                                    dictionary3.Add("draw_num", new List<string>(list2));
                                    dictionary3.Add("total", new List<string> { str8, str9, str10, str11, str12, str13, str14, num2.ToString(), str16, str15 });
                                    list.Add(new Dictionary<string, object>(dictionary3));
                                    dictionary3.Clear();
                                    list2.Clear();
                                }
                            }
                        }
                        dictionary.Add("jqkj", list);
                        break;
                }
                result.set_success(200);
                result.set_data(dictionary);
                strResult = JsonHandle.ObjectToJson(result);
            }
        }

        public void get_gamehall(HttpContext context, ref string strResult)
        {
            ReturnResult result = new ReturnResult();
            result.set_success(400);
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("type", "get_gamehall");
            if (context.Session["user_name"] == null)
            {
                context.Response.End();
            }
            else
            {
                string str = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                int num = int.Parse(LSRequest.qq("lid"));
                Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
                DataTable lotteryList = base.GetLotteryList();
                string str2 = "0";
                string str3 = "";
                if (num.Equals(100))
                {
                    DataTable currentPhase = CallBLL.cz_phase_six_bll.GetCurrentPhase("1");
                    if ((currentPhase != null) || (currentPhase.Rows.Count > 0))
                    {
                        DateTime now = DateTime.Now;
                        string s = currentPhase.Rows[0]["sn_stop_date"].ToString();
                        if (currentPhase.Rows[0]["is_closed"].ToString().Equals("1"))
                        {
                            str2 = "0";
                            str3 = "-";
                        }
                        else if (now < DateTime.Parse(s))
                        {
                            str2 = "2";
                            str3 = Convert.ToDateTime(s).ToString("yyyy-MM-dd HH:mm:ss");
                        }
                        else
                        {
                            str2 = "0";
                            str3 = "-";
                        }
                    }
                }
                else
                {
                    DataTable table3 = null;
                    switch (num)
                    {
                        case 0:
                            table3 = CallBLL.cz_phase_kl10_bll.IsPhaseClose();
                            break;

                        case 1:
                            table3 = CallBLL.cz_phase_cqsc_bll.IsPhaseClose();
                            break;

                        case 2:
                            table3 = CallBLL.cz_phase_pk10_bll.IsPhaseClose();
                            break;

                        case 3:
                            table3 = CallBLL.cz_phase_xync_bll.IsPhaseClose();
                            break;

                        case 4:
                            table3 = CallBLL.cz_phase_jsk3_bll.IsPhaseClose();
                            break;

                        case 5:
                            table3 = CallBLL.cz_phase_kl8_bll.IsPhaseClose();
                            break;

                        case 6:
                            table3 = CallBLL.cz_phase_k8sc_bll.IsPhaseClose();
                            break;

                        case 7:
                            table3 = CallBLL.cz_phase_pcdd_bll.IsPhaseClose();
                            break;

                        case 8:
                            table3 = CallBLL.cz_phase_pkbjl_bll.IsPhaseClose();
                            break;

                        case 9:
                            table3 = CallBLL.cz_phase_xyft5_bll.IsPhaseClose();
                            break;

                        case 10:
                            table3 = CallBLL.cz_phase_jscar_bll.IsPhaseClose();
                            break;

                        case 11:
                            table3 = CallBLL.cz_phase_speed5_bll.IsPhaseClose();
                            break;

                        case 12:
                            table3 = CallBLL.cz_phase_jspk10_bll.IsPhaseClose();
                            break;

                        case 13:
                            table3 = CallBLL.cz_phase_jscqsc_bll.IsPhaseClose();
                            break;

                        case 14:
                            table3 = CallBLL.cz_phase_jssfc_bll.IsPhaseClose();
                            break;

                        case 15:
                            table3 = CallBLL.cz_phase_jsft2_bll.IsPhaseClose();
                            break;

                        case 0x10:
                            table3 = CallBLL.cz_phase_car168_bll.IsPhaseClose();
                            break;

                        case 0x11:
                            table3 = CallBLL.cz_phase_ssc168_bll.IsPhaseClose();
                            break;

                        case 0x12:
                            table3 = CallBLL.cz_phase_vrcar_bll.IsPhaseClose();
                            break;

                        case 0x13:
                            table3 = CallBLL.cz_phase_vrssc_bll.IsPhaseClose();
                            break;

                        case 20:
                            table3 = CallBLL.cz_phase_xyftoa_bll.IsPhaseClose();
                            break;

                        case 0x15:
                            table3 = CallBLL.cz_phase_xyftsg_bll.IsPhaseClose();
                            break;

                        case 0x16:
                            table3 = CallBLL.cz_phase_happycar_bll.IsPhaseClose();
                            break;
                    }
                    if ((table3 != null) && (table3.Rows.Count > 0))
                    {
                        string str6 = table3.Rows[0]["isopen"].ToString();
                        string str7 = table3.Rows[0]["openning"].ToString();
                        string str8 = table3.Rows[0]["opendate"].ToString();
                        string str9 = table3.Rows[0]["endtime"].ToString();
                        DateTime time2 = Convert.ToDateTime(lotteryList.Select(string.Format(" id={0} ", num))[0]["begintime"].ToString());
                        if (str6.Equals("0"))
                        {
                            str2 = "0";
                            DateTime time3 = DateTime.Now;
                            string introduced24 = time3.ToString("yyyy-MM-dd");
                            if (introduced24 == time3.AddHours(7.0).ToString("yyyy-MM-dd"))
                            {
                                str3 = time3.ToString("yyyy-MM-dd ") + time2.ToString("HH:mm:ss");
                            }
                            else
                            {
                                str3 = time3.AddDays(1.0).ToString("yyyy-MM-dd ") + time2.ToString("HH:mm:ss");
                            }
                        }
                        else
                        {
                            if (str7.Equals("n"))
                            {
                                str2 = "1";
                            }
                            else
                            {
                                str2 = "2";
                            }
                            str3 = Convert.ToDateTime(str9).ToString("HH:mm:ss");
                        }
                    }
                }
                dictionary2.Add(num.ToString(), str2 + "," + str3 + "," + str);
                dictionary.Add("isopendata", dictionary2);
                result.set_data(dictionary);
                result.set_success(200);
                strResult = JsonHandle.ObjectToJson(result);
            }
        }

        public void get_profit(HttpContext context, ref string strResult)
        {
            ReturnResult result = new ReturnResult();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("type", "get_profit");
            dictionary.Add("profit", base.GetKCProfit());
            result.set_data(dictionary);
            strResult = JsonHandle.ObjectToJson(result);
        }

        public void get_six_date(HttpContext context, ref string strResult)
        {
            string s = base.get_six_schedule();
            HttpContext.Current.Response.AddHeader("content-type", "text/xml");
            if (s == "")
            {
                HttpContext.Current.Response.Write("<script>alert('接口获取数据失败!');</script>");
            }
            else
            {
                context.Response.Write(s);
            }
            context.Response.End();
        }

        protected string get_tz(string hm)
        {
            int num = 0;
            if (hm.Trim() == "49")
            {
                return "和";
            }
            num = int.Parse(hm) % 4;
            if (num == 0)
            {
                return "4";
            }
            int num2 = int.Parse(hm) % 4;
            return num2.ToString();
        }

        public void IsOpenLottery(HttpContext context, ref string strResult)
        {
            ReturnResult result = new ReturnResult();
            result.set_success(200);
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            if (context.Session["user_name"] == null)
            {
                result.set_success(300);
                dictionary.Add("type", "isopenlottery");
                result.set_data(dictionary);
                strResult = JsonHandle.ObjectToJson(result);
                context.Response.End();
            }
            else
            {
                string str2;
                Dictionary<string, object> dictionary2;
                int num = Convert.ToInt32(LSRequest.qq("lid"));
                DataTable currentPhase = null;
                switch (num)
                {
                    case 0:
                        currentPhase = CallBLL.cz_phase_kl10_bll.IsPhaseClose();
                        break;

                    case 1:
                        currentPhase = CallBLL.cz_phase_cqsc_bll.IsPhaseClose();
                        break;

                    case 2:
                        currentPhase = CallBLL.cz_phase_pk10_bll.IsPhaseClose();
                        break;

                    case 3:
                        currentPhase = CallBLL.cz_phase_xync_bll.IsPhaseClose();
                        break;

                    case 4:
                        currentPhase = CallBLL.cz_phase_jsk3_bll.IsPhaseClose();
                        break;

                    case 5:
                        currentPhase = CallBLL.cz_phase_kl8_bll.IsPhaseClose();
                        break;

                    case 6:
                        currentPhase = CallBLL.cz_phase_k8sc_bll.IsPhaseClose();
                        break;

                    case 7:
                        currentPhase = CallBLL.cz_phase_pcdd_bll.IsPhaseClose();
                        break;

                    case 8:
                        currentPhase = CallBLL.cz_phase_pkbjl_bll.IsPhaseClose();
                        break;

                    case 9:
                        currentPhase = CallBLL.cz_phase_xyft5_bll.IsPhaseClose();
                        break;

                    case 10:
                        currentPhase = CallBLL.cz_phase_jscar_bll.IsPhaseClose();
                        break;

                    case 11:
                        currentPhase = CallBLL.cz_phase_speed5_bll.IsPhaseClose();
                        break;

                    case 12:
                        currentPhase = CallBLL.cz_phase_jspk10_bll.IsPhaseClose();
                        break;

                    case 13:
                        currentPhase = CallBLL.cz_phase_jscqsc_bll.IsPhaseClose();
                        break;

                    case 14:
                        currentPhase = CallBLL.cz_phase_jssfc_bll.IsPhaseClose();
                        break;

                    case 15:
                        currentPhase = CallBLL.cz_phase_jsft2_bll.IsPhaseClose();
                        break;

                    case 0x10:
                        currentPhase = CallBLL.cz_phase_car168_bll.IsPhaseClose();
                        break;

                    case 0x11:
                        currentPhase = CallBLL.cz_phase_ssc168_bll.IsPhaseClose();
                        break;

                    case 0x12:
                        currentPhase = CallBLL.cz_phase_vrcar_bll.IsPhaseClose();
                        break;

                    case 0x13:
                        currentPhase = CallBLL.cz_phase_vrssc_bll.IsPhaseClose();
                        break;

                    case 20:
                        currentPhase = CallBLL.cz_phase_xyftoa_bll.IsPhaseClose();
                        break;

                    case 0x15:
                        currentPhase = CallBLL.cz_phase_xyftsg_bll.IsPhaseClose();
                        break;

                    case 0x16:
                        currentPhase = CallBLL.cz_phase_happycar_bll.IsPhaseClose();
                        break;

                    case 100:
                        currentPhase = CallBLL.cz_phase_six_bll.GetCurrentPhase("1");
                        break;
                }
                if (!num.Equals(100))
                {
                    str2 = "-1";
                    if (!currentPhase.Rows[0]["isopen"].ToString().Equals("0"))
                    {
                        str2 = "1";
                    }
                    dictionary.Add("type", "isopenlottery");
                    dictionary2 = new Dictionary<string, object>();
                    dictionary2.Add("isopen", str2);
                    dictionary.Add("isopenvalue", dictionary2);
                    result.set_data(dictionary);
                    result.set_success(200);
                    strResult = JsonHandle.ObjectToJson(result);
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
                    str2 = "-1";
                    if (_six.get_is_closed().ToString().Equals("1") || str3.Equals("n"))
                    {
                        str2 = "1";
                    }
                    dictionary.Add("type", "isopenlottery");
                    dictionary2 = new Dictionary<string, object>();
                    dictionary2.Add("isopen", str2);
                    dictionary.Add("isopenvalue", dictionary2);
                    result.set_data(dictionary);
                    result.set_success(200);
                    strResult = JsonHandle.ObjectToJson(result);
                }
            }
        }

        public override void ProcessRequest(HttpContext context)
        {
            base.checkLogin();
            string str = LSRequest.qq("action");
            if (!string.IsNullOrEmpty(str))
            {
                string strResult = "";
                switch (str)
                {
                    case "get_ad":
                        this.get_ad(context, ref strResult);
                        break;

                    case "get_profit":
                        this.get_profit(context, ref strResult);
                        break;

                    case "set_skin":
                        this.set_skin(context, ref strResult);
                        break;

                    case "get_six_date":
                        this.get_six_date(context, ref strResult);
                        break;

                    case "get_openlottery":
                        this.IsOpenLottery(context, ref strResult);
                        break;

                    case "get_currentphase":
                        this.get_currentphase(context, ref strResult);
                        break;

                    case "get_gamehall":
                        this.get_gamehall(context, ref strResult);
                        break;

                    default:
                        this.do_default(context, ref strResult);
                        break;
                }
                context.Response.ContentType = "text/json";
                context.Response.Write(strResult);
            }
        }

        public void set_skin(HttpContext context, ref string strResult)
        {
            ReturnResult result = new ReturnResult();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("type", "set_skin");
            string str = LSRequest.qq("skin");
            if (context.Session["user_name"] == null)
            {
                context.Response.End();
            }
            else
            {
                string str2 = context.Session["user_name"].ToString();
                cz_userinfo_session getUserModelInfo = base.GetUserModelInfo;
                string str3 = getUserModelInfo.get_u_id().ToString();
                if (CallBLL.cz_users_bll.UpdateSkin(str3, str) > 0)
                {
                    result.set_success(200);
                    getUserModelInfo.set_u_skin(str);
                    context.Session[str2 + "lottery_session_user_info"] = getUserModelInfo;
                }
                else
                {
                    result.set_success(400);
                    result.set_tipinfo(PageBase.GetMessageByCache("u100021", "MessageHint"));
                }
                result.set_data(dictionary);
                strResult = JsonHandle.ObjectToJson(result);
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

