namespace Agent.Web.Handler
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
    using System.Web;

    public class QueryHandler : BaseHandler
    {
        public void get_ad(HttpContext context, ref string strResult)
        {
            ReturnResult result = new ReturnResult();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("type", "get_ad");
            agent_userinfo_session _session = new agent_userinfo_session();
            string str = context.Session["user_name"].ToString();
            _session = context.Session[str + "lottery_session_user_info"] as agent_userinfo_session;
            string str2 = _session.get_u_type();
            string str3 = LSRequest.qq("browserCode");
            string str4 = HttpContext.Current.Session["lottery_session_img_code_brower"];
            if ((str3 == null) || !str3.Equals(str4))
            {
                this.Session.Abandon();
                result.set_success(300);
                result.set_data(dictionary);
                strResult = JsonHandle.ObjectToJson(result);
            }
            else
            {
                int num = base.get_current_master_id();
                string str5 = num.ToString();
                switch (num)
                {
                    case 0:
                        str5 = "0,1,2";
                        break;

                    case 1:
                        str5 = "0,1";
                        break;

                    default:
                        str5 = "0,2";
                        break;
                }
                List<object> list = base.get_ad_level(str2, 3, str5);
                if (list == null)
                {
                    dictionary.Add("ad", new List<object>());
                }
                else
                {
                    dictionary.Add("ad", list);
                }
                if (_session.get_u_type().Equals("zj"))
                {
                    string str6 = "";
                    if (FileCacheHelper.get_RedisStatOnline().Equals(1))
                    {
                        str6 = base.get_online_cnt("redis");
                    }
                    else if (FileCacheHelper.get_RedisStatOnline().Equals(2))
                    {
                        str6 = base.get_online_cntStack("redis");
                    }
                    else
                    {
                        str6 = base.get_online_cnt();
                    }
                    dictionary.Add("online_cnt", str6);
                }
                string str7 = base.get_children_name();
                if (FileCacheHelper.get_RedisStatOnline().Equals(1) || FileCacheHelper.get_RedisStatOnline().Equals(2))
                {
                    bool flag = false;
                    if ((_session.get_users_child_session() != null) && _session.get_users_child_session().get_is_admin().Equals(1))
                    {
                        flag = true;
                    }
                    if (!flag)
                    {
                        if (FileCacheHelper.get_RedisStatOnline().Equals(1))
                        {
                            base.CheckIsOut((str7 == "") ? str : str7);
                            base.stat_online_redis((str7 == "") ? str : str7, str2);
                        }
                        else if (FileCacheHelper.get_RedisStatOnline().Equals(2))
                        {
                            base.CheckIsOutStack((str7 == "") ? str : str7);
                            base.stat_online_redisStack((str7 == "") ? str : str7, str2);
                        }
                    }
                    if (FileCacheHelper.get_RedisStatOnline().Equals(1))
                    {
                        base.stat_online_redis_timer();
                    }
                    else if (FileCacheHelper.get_RedisStatOnline().Equals(2))
                    {
                        base.stat_online_redis_timerStack();
                    }
                }
                else
                {
                    MemberPageBase.stat_online((str7 == "") ? str : str7, str2);
                }
                string compareTime = LSRequest.qq("oldTime");
                DateTime now = DateTime.Now;
                Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
                if (base.IsAutoUpdate(context.Session["user_name"].ToString(), compareTime))
                {
                    List<object> autoJPForAd = base.GetAutoJPForAd(compareTime, ref now);
                    if (autoJPForAd != null)
                    {
                        dictionary2.Add("tipsList", autoJPForAd);
                    }
                    else
                    {
                        dictionary2.Add("tipsList", new List<object>());
                    }
                }
                else
                {
                    dictionary2.Add("tipsList", new List<object>());
                }
                dictionary2.Add("timestamp", Utils.DateTimeToStamp(now));
                dictionary.Add("autoJP", dictionary2);
                result.set_data(dictionary);
                strResult = JsonHandle.ObjectToJson(result);
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
                string s = LSRequest.qq("lid");
                string str2 = LSRequest.qq("phase");
                string str3 = LSRequest.qq("tabletype");
                new Dictionary<string, string>();
                int num189 = int.Parse(s);
                switch (num189)
                {
                    case 0:
                    {
                        List<object> list4 = new List<object>();
                        List<string> collection = new List<string>();
                        DataSet currentByPhase = CallBLL.cz_phase_kl10_bll.GetCurrentByPhase(str2);
                        if (currentByPhase != null)
                        {
                            DataTable table2 = currentByPhase.Tables[0];
                            int num8 = 8;
                            Dictionary<string, object> dictionary3 = new Dictionary<string, object>();
                            string str18 = table2.Rows[0]["is_closed"];
                            string str19 = table2.Rows[0]["is_opendata"];
                            if (str18.Equals("1") && str19.Equals("1"))
                            {
                                foreach (DataRow row2 in table2.Rows)
                                {
                                    string str20 = "";
                                    string str21 = "";
                                    string str22 = "";
                                    string str23 = "";
                                    string str24 = "";
                                    string str25 = "";
                                    string str26 = "";
                                    str20 = row2["phase"].ToString();
                                    str21 = row2["play_open_date"].ToString();
                                    for (int i = 1; i <= num8; i++)
                                    {
                                        string str27 = "n" + i;
                                        collection.Add(row2[str27].ToString());
                                    }
                                    str22 = KL10Phase.get_zh(collection).ToString();
                                    str23 = KL10Phase.get_cl_zhdx(str22);
                                    str23 = base.SetWordColor(str23);
                                    str24 = KL10Phase.get_cl_zhds(str22);
                                    str24 = base.SetWordColor(str24);
                                    str25 = KL10Phase.get_cl_zhwsdx(str22);
                                    str25 = base.SetWordColor(str25);
                                    str26 = KL10Phase.get_cl_lh(collection[0].ToString(), collection[num8 - 1].ToString());
                                    str26 = base.SetWordColor(str26);
                                    dictionary3.Add("phase", str20);
                                    dictionary3.Add("play_open_date", str21);
                                    dictionary3.Add("draw_num", new List<string>(collection));
                                    dictionary3.Add("total", new List<string> { str22, str23, str24, str25, str26 });
                                    list4.Add(new Dictionary<string, object>(dictionary3));
                                    dictionary3.Clear();
                                    collection.Clear();
                                }
                            }
                        }
                        dictionary.Add("jqkj", list4);
                        break;
                    }
                    case 1:
                    {
                        List<object> list7 = new List<object>();
                        List<string> list8 = new List<string>();
                        DataSet set3 = CallBLL.cz_phase_cqsc_bll.GetCurrentByPhase(str2);
                        if (set3 != null)
                        {
                            DataTable table3 = set3.Tables[0];
                            Dictionary<string, object> dictionary4 = new Dictionary<string, object>();
                            string str28 = table3.Rows[0]["is_closed"];
                            string str29 = table3.Rows[0]["is_opendata"];
                            if (str28.Equals("1") && str29.Equals("1"))
                            {
                                foreach (DataRow row3 in table3.Rows)
                                {
                                    string str30 = "";
                                    string str31 = "";
                                    string str32 = "";
                                    string str33 = "";
                                    string str34 = "";
                                    string str35 = "";
                                    string str36 = "";
                                    string str37 = "";
                                    string str38 = "";
                                    str30 = row3["phase"].ToString();
                                    str31 = row3["play_open_date"].ToString();
                                    int num10 = Convert.ToInt32(row3["n1"].ToString());
                                    int num11 = Convert.ToInt32(row3["n2"].ToString());
                                    int num12 = Convert.ToInt32(row3["n3"].ToString());
                                    int num13 = Convert.ToInt32(row3["n4"].ToString());
                                    int num14 = Convert.ToInt32(row3["n5"].ToString());
                                    list8.Add(num10.ToString());
                                    list8.Add(num11.ToString());
                                    list8.Add(num12.ToString());
                                    list8.Add(num13.ToString());
                                    list8.Add(num14.ToString());
                                    num189 = (((num10 + num11) + num12) + num13) + num14;
                                    str32 = num189.ToString();
                                    str33 = CQSCPhase.get_cl_zhdx(str32);
                                    str33 = base.SetWordColor(str33);
                                    str34 = CQSCPhase.get_cl_zhds(str32);
                                    str34 = base.SetWordColor(str34);
                                    str35 = CQSCPhase.get_cl_lh(num10.ToString(), num14.ToString());
                                    str35 = base.SetWordColor(str35);
                                    str36 = CQSCPhase.get_cqsc_str(string.Concat(new object[] { num10, ",", num11, ",", num12 }));
                                    str37 = CQSCPhase.get_cqsc_str(string.Concat(new object[] { num11, ",", num12, ",", num13 }));
                                    str38 = CQSCPhase.get_cqsc_str(string.Concat(new object[] { num12, ",", num13, ",", num14 }));
                                    dictionary4.Add("phase", str30);
                                    dictionary4.Add("play_open_date", str31);
                                    dictionary4.Add("draw_num", new List<string>(list8));
                                    dictionary4.Add("total", new List<string> { str32, str33, str34, str35, str36, str37, str38 });
                                    list7.Add(new Dictionary<string, object>(dictionary4));
                                    dictionary4.Clear();
                                    list8.Clear();
                                }
                            }
                        }
                        dictionary.Add("jqkj", list7);
                        break;
                    }
                    case 2:
                    {
                        List<object> list10 = new List<object>();
                        List<string> list11 = new List<string>();
                        DataSet set4 = CallBLL.cz_phase_pk10_bll.GetCurrentByPhase(str2);
                        if (set4 != null)
                        {
                            DataTable table4 = set4.Tables[0];
                            Dictionary<string, object> dictionary5 = new Dictionary<string, object>();
                            string str39 = table4.Rows[0]["is_closed"];
                            string str40 = table4.Rows[0]["is_opendata"];
                            if (str39.Equals("1") && str40.Equals("1"))
                            {
                                foreach (DataRow row4 in table4.Rows)
                                {
                                    string str41 = "";
                                    string str42 = "";
                                    string str43 = "";
                                    string str44 = "";
                                    string str45 = "";
                                    string str46 = "";
                                    string str47 = "";
                                    string str48 = "";
                                    string str49 = "";
                                    string str50 = "";
                                    str41 = row4["phase"].ToString();
                                    str42 = row4["play_open_date"].ToString();
                                    int num15 = Convert.ToInt32(row4["n1"].ToString());
                                    int num16 = Convert.ToInt32(row4["n2"].ToString());
                                    int num17 = Convert.ToInt32(row4["n3"].ToString());
                                    int num18 = Convert.ToInt32(row4["n4"].ToString());
                                    int num19 = Convert.ToInt32(row4["n5"].ToString());
                                    int num20 = Convert.ToInt32(row4["n6"].ToString());
                                    int num21 = Convert.ToInt32(row4["n7"].ToString());
                                    int num22 = Convert.ToInt32(row4["n8"].ToString());
                                    int num23 = Convert.ToInt32(row4["n9"].ToString());
                                    int num24 = Convert.ToInt32(row4["n10"].ToString());
                                    list11.Add((num15 < 10) ? ("0" + num15) : num15.ToString());
                                    list11.Add((num16 < 10) ? ("0" + num16) : num16.ToString());
                                    list11.Add((num17 < 10) ? ("0" + num17) : num17.ToString());
                                    list11.Add((num18 < 10) ? ("0" + num18) : num18.ToString());
                                    list11.Add((num19 < 10) ? ("0" + num19) : num19.ToString());
                                    list11.Add((num20 < 10) ? ("0" + num20) : num20.ToString());
                                    list11.Add((num21 < 10) ? ("0" + num21) : num21.ToString());
                                    list11.Add((num22 < 10) ? ("0" + num22) : num22.ToString());
                                    list11.Add((num23 < 10) ? ("0" + num23) : num23.ToString());
                                    list11.Add((num24 < 10) ? ("0" + num24) : num24.ToString());
                                    num189 = num15 + num16;
                                    str43 = num189.ToString();
                                    if (Convert.ToInt32(str43) <= 11)
                                    {
                                        str44 = "小";
                                    }
                                    else
                                    {
                                        str44 = "大";
                                    }
                                    str44 = base.SetWordColor(str44);
                                    if ((Convert.ToInt32(str43) % 2) == 0)
                                    {
                                        str45 = "雙";
                                    }
                                    else
                                    {
                                        str45 = "單";
                                    }
                                    str45 = base.SetWordColor(str45);
                                    if (num15 > num24)
                                    {
                                        str46 = "龍";
                                    }
                                    else
                                    {
                                        str46 = "虎";
                                    }
                                    str46 = base.SetWordColor(str46);
                                    if (num16 > num23)
                                    {
                                        str47 = "龍";
                                    }
                                    else
                                    {
                                        str47 = "虎";
                                    }
                                    str47 = base.SetWordColor(str47);
                                    if (num17 > num22)
                                    {
                                        str48 = "龍";
                                    }
                                    else
                                    {
                                        str48 = "虎";
                                    }
                                    str48 = base.SetWordColor(str48);
                                    if (num18 > num21)
                                    {
                                        str49 = "龍";
                                    }
                                    else
                                    {
                                        str49 = "虎";
                                    }
                                    str49 = base.SetWordColor(str49);
                                    if (num19 > num20)
                                    {
                                        str50 = "龍";
                                    }
                                    else
                                    {
                                        str50 = "虎";
                                    }
                                    str50 = base.SetWordColor(str50);
                                    dictionary5.Add("phase", str41);
                                    dictionary5.Add("play_open_date", str42);
                                    dictionary5.Add("draw_num", new List<string>(list11));
                                    dictionary5.Add("total", new List<string> { str43, str44, str45, str46, str47, str48, str49, str50 });
                                    list10.Add(new Dictionary<string, object>(dictionary5));
                                    dictionary5.Clear();
                                    list11.Clear();
                                }
                            }
                        }
                        dictionary.Add("jqkj", list10);
                        break;
                    }
                    case 3:
                    {
                        List<object> list13 = new List<object>();
                        List<string> list14 = new List<string>();
                        DataSet set5 = CallBLL.cz_phase_xync_bll.GetCurrentByPhase(str2);
                        if (set5 != null)
                        {
                            string str51 = "";
                            string str52 = "";
                            int num25 = 0;
                            string str53 = "";
                            string str54 = "";
                            string str55 = "";
                            string str56 = "";
                            DataTable table5 = set5.Tables[0];
                            int num26 = 8;
                            Dictionary<string, object> dictionary6 = new Dictionary<string, object>();
                            string str57 = table5.Rows[0]["is_closed"];
                            string str58 = table5.Rows[0]["is_opendata"];
                            if (str57.Equals("1") && str58.Equals("1"))
                            {
                                foreach (DataRow row5 in table5.Rows)
                                {
                                    str51 = row5["phase"].ToString();
                                    str52 = row5["play_open_date"].ToString();
                                    for (int j = 1; j <= num26; j++)
                                    {
                                        string str59 = "n" + j;
                                        list14.Add(row5[str59].ToString());
                                        num25 += Convert.ToInt32(row5[str59]);
                                    }
                                    if (num25 == 0x54)
                                    {
                                        str53 = "和";
                                    }
                                    else if (num25 <= 0x53)
                                    {
                                        str53 = "小";
                                    }
                                    else
                                    {
                                        str53 = "大";
                                    }
                                    str53 = base.SetWordColor(str53);
                                    if ((num25 % 2) == 0)
                                    {
                                        str54 = "雙";
                                    }
                                    else
                                    {
                                        str54 = "單";
                                    }
                                    str54 = base.SetWordColor(str54);
                                    if ((num25 % 10) <= 4)
                                    {
                                        str55 = "尾小";
                                    }
                                    else
                                    {
                                        str55 = "尾大";
                                    }
                                    str55 = base.SetWordColor(str55);
                                    if (int.Parse(row5["N1"].ToString().Trim()) > int.Parse(row5["N8"].ToString().Trim()))
                                    {
                                        str56 = "家禽";
                                    }
                                    else
                                    {
                                        str56 = "野獸";
                                    }
                                    str56 = base.SetWordColor(str56);
                                    dictionary6.Add("phase", str51);
                                    dictionary6.Add("play_open_date", str52);
                                    dictionary6.Add("draw_num", new List<string>(list14));
                                    dictionary6.Add("total", new List<string> { num25.ToString(), str53, str54, str55, str56 });
                                    list13.Add(new Dictionary<string, object>(dictionary6));
                                    dictionary6.Clear();
                                    list14.Clear();
                                }
                            }
                        }
                        dictionary.Add("jqkj", list13);
                        break;
                    }
                    case 4:
                    {
                        List<object> list16 = new List<object>();
                        List<string> list17 = new List<string>();
                        DataSet set6 = CallBLL.cz_phase_jsk3_bll.GetCurrentByPhase(str2);
                        if (set6 != null)
                        {
                            DataTable table6 = set6.Tables[0];
                            Dictionary<string, object> dictionary7 = new Dictionary<string, object>();
                            string str60 = table6.Rows[0]["is_closed"];
                            string str61 = table6.Rows[0]["is_opendata"];
                            if (str60.Equals("1") && str61.Equals("1"))
                            {
                                foreach (DataRow row6 in table6.Rows)
                                {
                                    string str62 = "";
                                    string str63 = "";
                                    string str64 = "";
                                    string str65 = "";
                                    str62 = row6["phase"].ToString();
                                    str63 = row6["play_open_date"].ToString();
                                    int num28 = Convert.ToInt32(row6["n1"].ToString());
                                    int num29 = Convert.ToInt32(row6["n2"].ToString());
                                    int num30 = Convert.ToInt32(row6["n3"].ToString());
                                    list17.Add(num28.ToString());
                                    list17.Add(num29.ToString());
                                    list17.Add(num30.ToString());
                                    num189 = (num28 + num29) + num30;
                                    str64 = num189.ToString();
                                    if ((num28 == num29) && (num29 == num30))
                                    {
                                        str65 = "通吃";
                                    }
                                    else if (Convert.ToInt32(str64) <= 10)
                                    {
                                        str65 = "小";
                                    }
                                    else
                                    {
                                        str65 = "大";
                                    }
                                    str65 = base.SetWordColor(str65);
                                    dictionary7.Add("phase", str62);
                                    dictionary7.Add("play_open_date", str63);
                                    dictionary7.Add("draw_num", new List<string>(list17));
                                    dictionary7.Add("total", new List<string> { str64, str65 });
                                    list16.Add(new Dictionary<string, object>(dictionary7));
                                    dictionary7.Clear();
                                    list17.Clear();
                                }
                            }
                        }
                        dictionary.Add("jqkj", list16);
                        break;
                    }
                    case 5:
                    {
                        List<object> list19 = new List<object>();
                        List<string> list20 = new List<string>();
                        DataSet set7 = CallBLL.cz_phase_kl8_bll.GetCurrentByPhase(str2);
                        if (set7 != null)
                        {
                            DataTable table7 = set7.Tables[0];
                            Dictionary<string, object> dictionary8 = new Dictionary<string, object>();
                            int num31 = 0;
                            string str66 = table7.Rows[0]["is_closed"];
                            string str67 = table7.Rows[0]["is_opendata"];
                            if (str66.Equals("1") && str67.Equals("1"))
                            {
                                foreach (DataRow row7 in table7.Rows)
                                {
                                    string str68 = "";
                                    string str69 = "";
                                    string str70 = "";
                                    string str71 = "";
                                    string str72 = "";
                                    string str73 = "";
                                    string str74 = "";
                                    string str75 = "";
                                    str68 = row7["phase"].ToString();
                                    str69 = row7["play_open_date"].ToString();
                                    int num32 = Convert.ToInt32(row7["n1"].ToString());
                                    int num33 = Convert.ToInt32(row7["n2"].ToString());
                                    int num34 = Convert.ToInt32(row7["n3"].ToString());
                                    int num35 = Convert.ToInt32(row7["n4"].ToString());
                                    int num36 = Convert.ToInt32(row7["n5"].ToString());
                                    int num37 = Convert.ToInt32(row7["n6"].ToString());
                                    int num38 = Convert.ToInt32(row7["n7"].ToString());
                                    int num39 = Convert.ToInt32(row7["n8"].ToString());
                                    int num40 = Convert.ToInt32(row7["n9"].ToString());
                                    int num41 = Convert.ToInt32(row7["n10"].ToString());
                                    int num42 = Convert.ToInt32(row7["n11"].ToString());
                                    int num43 = Convert.ToInt32(row7["n12"].ToString());
                                    int num44 = Convert.ToInt32(row7["n13"].ToString());
                                    int num45 = Convert.ToInt32(row7["n14"].ToString());
                                    int num46 = Convert.ToInt32(row7["n15"].ToString());
                                    int num47 = Convert.ToInt32(row7["n16"].ToString());
                                    int num48 = Convert.ToInt32(row7["n17"].ToString());
                                    int num49 = Convert.ToInt32(row7["n18"].ToString());
                                    int num50 = Convert.ToInt32(row7["n19"].ToString());
                                    int num51 = Convert.ToInt32(row7["n20"].ToString());
                                    list20.Add((num32 < 10) ? ("0" + num32) : num32.ToString());
                                    list20.Add((num33 < 10) ? ("0" + num33) : num33.ToString());
                                    list20.Add((num34 < 10) ? ("0" + num34) : num34.ToString());
                                    list20.Add((num35 < 10) ? ("0" + num35) : num35.ToString());
                                    list20.Add((num36 < 10) ? ("0" + num36) : num36.ToString());
                                    list20.Add((num37 < 10) ? ("0" + num37) : num37.ToString());
                                    list20.Add((num38 < 10) ? ("0" + num38) : num38.ToString());
                                    list20.Add((num39 < 10) ? ("0" + num39) : num39.ToString());
                                    list20.Add((num40 < 10) ? ("0" + num40) : num40.ToString());
                                    list20.Add((num41 < 10) ? ("0" + num41) : num41.ToString());
                                    list20.Add((num42 < 10) ? ("0" + num42) : num42.ToString());
                                    list20.Add((num43 < 10) ? ("0" + num43) : num43.ToString());
                                    list20.Add((num44 < 10) ? ("0" + num44) : num44.ToString());
                                    list20.Add((num45 < 10) ? ("0" + num45) : num45.ToString());
                                    list20.Add((num46 < 10) ? ("0" + num46) : num46.ToString());
                                    list20.Add((num47 < 10) ? ("0" + num47) : num47.ToString());
                                    list20.Add((num48 < 10) ? ("0" + num48) : num48.ToString());
                                    list20.Add((num49 < 10) ? ("0" + num49) : num49.ToString());
                                    list20.Add((num50 < 10) ? ("0" + num50) : num50.ToString());
                                    list20.Add((num51 < 10) ? ("0" + num51) : num51.ToString());
                                    num189 = ((((((((((((((((((num32 + num33) + num34) + num35) + num36) + num37) + num38) + num39) + num40) + num41) + num42) + num43) + num44) + num45) + num46) + num47) + num48) + num49) + num50) + num51;
                                    str70 = num189.ToString();
                                    if (Convert.ToInt32(str70) == 810)
                                    {
                                        str72 = "和";
                                    }
                                    else if ((Convert.ToInt32(str70) % 2) == 0)
                                    {
                                        str72 = "雙";
                                    }
                                    else
                                    {
                                        str72 = "單";
                                    }
                                    str72 = base.SetWordColor(str72);
                                    if (Convert.ToInt32(str70) > 810)
                                    {
                                        str71 = "大";
                                    }
                                    else if (Convert.ToInt32(str70) < 810)
                                    {
                                        str71 = "小";
                                    }
                                    else
                                    {
                                        str71 = "和";
                                    }
                                    str71 = base.SetWordColor(str71);
                                    int num52 = 0;
                                    int num53 = 0;
                                    for (int k = 0; k < 20; k++)
                                    {
                                        if ((int.Parse(table7.Rows[num31]["n" + (k + 1)].ToString().Trim()) % 2) == 0)
                                        {
                                            num53++;
                                        }
                                        else
                                        {
                                            num52++;
                                        }
                                    }
                                    str73 = KL8Phase.get_cl_dsh(num52.ToString(), num53.ToString());
                                    str73 = base.SetWordColor(str73);
                                    int num55 = 0;
                                    int num56 = 0;
                                    for (int m = 0; m < 20; m++)
                                    {
                                        if (int.Parse(table7.Rows[num31]["n" + (m + 1)].ToString().Trim()) <= 40)
                                        {
                                            num55++;
                                        }
                                        else
                                        {
                                            num56++;
                                        }
                                    }
                                    str74 = KL8Phase.get_cl_qhh(num55.ToString(), num56.ToString());
                                    str74 = base.SetWordColor(str74);
                                    str75 = KL8Phase.get_cl_wh(str70.ToString());
                                    dictionary8.Add("phase", str68);
                                    dictionary8.Add("play_open_date", str69);
                                    dictionary8.Add("draw_num", new List<string>(list20));
                                    dictionary8.Add("total", new List<string> { str70, str72, str71, str73, str74, str75 });
                                    list19.Add(new Dictionary<string, object>(dictionary8));
                                    dictionary8.Clear();
                                    list20.Clear();
                                    num31++;
                                }
                            }
                        }
                        dictionary.Add("jqkj", list19);
                        break;
                    }
                    case 6:
                    {
                        List<object> list22 = new List<object>();
                        List<string> list23 = new List<string>();
                        DataSet set8 = CallBLL.cz_phase_k8sc_bll.GetCurrentByPhase(str2);
                        if (set8 != null)
                        {
                            DataTable table8 = set8.Tables[0];
                            Dictionary<string, object> dictionary9 = new Dictionary<string, object>();
                            string str76 = table8.Rows[0]["is_closed"];
                            string str77 = table8.Rows[0]["is_opendata"];
                            if (str76.Equals("1") && str77.Equals("1"))
                            {
                                foreach (DataRow row8 in table8.Rows)
                                {
                                    string str78 = "";
                                    string str79 = "";
                                    string str80 = "";
                                    string str81 = "";
                                    string str82 = "";
                                    string str83 = "";
                                    string str84 = "";
                                    string str85 = "";
                                    string str86 = "";
                                    str78 = row8["phase"].ToString();
                                    str79 = row8["play_open_date"].ToString();
                                    int num58 = Convert.ToInt32(row8["n1"].ToString());
                                    int num59 = Convert.ToInt32(row8["n2"].ToString());
                                    int num60 = Convert.ToInt32(row8["n3"].ToString());
                                    int num61 = Convert.ToInt32(row8["n4"].ToString());
                                    int num62 = Convert.ToInt32(row8["n5"].ToString());
                                    list23.Add(num58.ToString());
                                    list23.Add(num59.ToString());
                                    list23.Add(num60.ToString());
                                    list23.Add(num61.ToString());
                                    list23.Add(num62.ToString());
                                    num189 = (((num58 + num59) + num60) + num61) + num62;
                                    str80 = num189.ToString();
                                    str81 = K8SCPhase.get_cl_zhdx(str80);
                                    str81 = base.SetWordColor(str81);
                                    str82 = K8SCPhase.get_cl_zhds(str80);
                                    str82 = base.SetWordColor(str82);
                                    str83 = K8SCPhase.get_cl_lh(num58.ToString(), num62.ToString());
                                    str83 = base.SetWordColor(str83);
                                    str84 = K8SCPhase.get_k8sc_str(string.Concat(new object[] { num58, ",", num59, ",", num60 }));
                                    str85 = K8SCPhase.get_k8sc_str(string.Concat(new object[] { num59, ",", num60, ",", num61 }));
                                    str86 = K8SCPhase.get_k8sc_str(string.Concat(new object[] { num60, ",", num61, ",", num62 }));
                                    dictionary9.Add("phase", str78);
                                    dictionary9.Add("play_open_date", str79);
                                    dictionary9.Add("draw_num", new List<string>(list23));
                                    dictionary9.Add("total", new List<string> { str80, str81, str82, str83, str84, str85, str86 });
                                    list22.Add(new Dictionary<string, object>(dictionary9));
                                    dictionary9.Clear();
                                    list23.Clear();
                                }
                            }
                        }
                        dictionary.Add("jqkj", list22);
                        break;
                    }
                    case 7:
                    {
                        List<object> list25 = new List<object>();
                        List<string> list26 = new List<string>();
                        DataSet set9 = CallBLL.cz_phase_pcdd_bll.GetCurrentByPhase(str2);
                        if (set9 != null)
                        {
                            DataTable table9 = set9.Tables[0];
                            Dictionary<string, object> dictionary10 = new Dictionary<string, object>();
                            string str87 = table9.Rows[0]["is_closed"];
                            string str88 = table9.Rows[0]["is_opendata"];
                            if (str87.Equals("1") && str88.Equals("1"))
                            {
                                foreach (DataRow row9 in table9.Rows)
                                {
                                    string str89 = "";
                                    string str90 = "";
                                    string str91 = "";
                                    string str92 = "";
                                    string str93 = "";
                                    string str94 = "";
                                    string str95 = "";
                                    str89 = row9["phase"].ToString();
                                    str90 = row9["play_open_date"].ToString();
                                    int num63 = Convert.ToInt32(row9["n1"].ToString());
                                    int num64 = Convert.ToInt32(row9["n2"].ToString());
                                    int num65 = Convert.ToInt32(row9["n3"].ToString());
                                    int num66 = Convert.ToInt32(row9["sn"].ToString());
                                    list26.Add(num63.ToString());
                                    list26.Add(num64.ToString());
                                    list26.Add(num65.ToString());
                                    list26.Add(num66.ToString());
                                    if (num66 < 14)
                                    {
                                        str91 = base.SetWordColor("小");
                                    }
                                    else
                                    {
                                        str91 = base.SetWordColor("大");
                                    }
                                    if ((num66 % 2) == 0)
                                    {
                                        str92 = base.SetWordColor("雙");
                                    }
                                    else
                                    {
                                        str92 = base.SetWordColor("單");
                                    }
                                    str93 = base.SetWordColor(this.get_pcdd_bs_str(num66.ToString()));
                                    if (num66 <= 4)
                                    {
                                        str94 = base.SetWordColor("極小");
                                    }
                                    else if (num66 >= 0x17)
                                    {
                                        str94 = base.SetWordColor("極大");
                                    }
                                    else
                                    {
                                        str94 = "-";
                                    }
                                    if (((num63 == num64) && (num63 == num65)) && (num64 == num65))
                                    {
                                        str95 = base.SetWordColor("豹子");
                                    }
                                    else
                                    {
                                        str95 = "-";
                                    }
                                    dictionary10.Add("phase", str89);
                                    dictionary10.Add("play_open_date", str90);
                                    dictionary10.Add("draw_num", new List<string>(list26));
                                    dictionary10.Add("total", new List<string> { str91, str92, str93, str94, str95 });
                                    list25.Add(new Dictionary<string, object>(dictionary10));
                                    dictionary10.Clear();
                                    list26.Clear();
                                }
                            }
                        }
                        dictionary.Add("jqkj", list25);
                        break;
                    }
                    case 8:
                    {
                        List<object> list31 = new List<object>();
                        List<string> list32 = new List<string>();
                        DataSet set11 = CallBLL.cz_phase_pkbjl_bll.GetCurrentByPhase(str2, str3);
                        if (set11 != null)
                        {
                            DataTable table11 = set11.Tables[0];
                            Dictionary<string, object> dictionary12 = new Dictionary<string, object>();
                            string str108 = table11.Rows[0]["is_closed"];
                            string str109 = table11.Rows[0]["is_opendata"];
                            if (str108.Equals("1") && str109.Equals("1"))
                            {
                                foreach (DataRow row11 in table11.Rows)
                                {
                                    string str110 = row11["phase"].ToString();
                                    string str111 = row11["play_open_date"].ToString();
                                    int num77 = Convert.ToInt32(row11["n1"].ToString());
                                    int num78 = Convert.ToInt32(row11["n2"].ToString());
                                    int num79 = Convert.ToInt32(row11["n3"].ToString());
                                    int num80 = Convert.ToInt32(row11["n4"].ToString());
                                    int num81 = Convert.ToInt32(row11["n5"].ToString());
                                    int num82 = Convert.ToInt32(row11["n6"].ToString());
                                    int num83 = Convert.ToInt32(row11["n7"].ToString());
                                    int num84 = Convert.ToInt32(row11["n8"].ToString());
                                    int num85 = Convert.ToInt32(row11["n9"].ToString());
                                    int num86 = Convert.ToInt32(row11["n10"].ToString());
                                    list32.Add((num77 < 10) ? ("0" + num77) : num77.ToString());
                                    list32.Add((num78 < 10) ? ("0" + num78) : num78.ToString());
                                    list32.Add((num79 < 10) ? ("0" + num79) : num79.ToString());
                                    list32.Add((num80 < 10) ? ("0" + num80) : num80.ToString());
                                    list32.Add((num81 < 10) ? ("0" + num81) : num81.ToString());
                                    list32.Add((num82 < 10) ? ("0" + num82) : num82.ToString());
                                    list32.Add((num83 < 10) ? ("0" + num83) : num83.ToString());
                                    list32.Add((num84 < 10) ? ("0" + num84) : num84.ToString());
                                    list32.Add((num85 < 10) ? ("0" + num85) : num85.ToString());
                                    list32.Add((num86 < 10) ? ("0" + num86) : num86.ToString());
                                    string str112 = row11["ten_poker"].ToString();
                                    string str113 = row11["xian_nn"].ToString();
                                    string str114 = row11["zhuang_nn"].ToString();
                                    string pKBJLBalanceMaxMin = Utils.GetPKBJLBalanceMaxMin(str114, str113);
                                    bool pKBJLBalanceIsDuizi = Utils.GetPKBJLBalanceIsDuizi(str113);
                                    bool flag2 = Utils.GetPKBJLBalanceIsDuizi(str114);
                                    string str116 = "-";
                                    string str117 = "-";
                                    string pKBJLBalanceZXH = Utils.GetPKBJLBalanceZXH(str114, str113);
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
                                        str116 = base.SetWordColor("閑對");
                                    }
                                    if (flag2)
                                    {
                                        str117 = base.SetWordColor("莊對");
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
                                    string pKBJLBalanceDianshu = Utils.GetPKBJLBalanceDianshu(str113);
                                    string str120 = Utils.GetPKBJLBalanceDianshu(str114);
                                    dictionary12.Add("phase", str110);
                                    dictionary12.Add("play_open_date", str111);
                                    dictionary12.Add("draw_num", new List<string>(list32));
                                    dictionary12.Add("pokerList", str112);
                                    dictionary12.Add("xian_nn", str113);
                                    dictionary12.Add("zhuang_nn", str114);
                                    dictionary12.Add("xian_dian", pKBJLBalanceDianshu);
                                    dictionary12.Add("zhuang_dian", str120);
                                    dictionary12.Add("total", new List<string> { pKBJLBalanceZXH, pKBJLBalanceMaxMin, str116, str117 });
                                    list31.Add(new Dictionary<string, object>(dictionary12));
                                    dictionary12.Clear();
                                    list32.Clear();
                                }
                            }
                        }
                        dictionary.Add("jqkj", list31);
                        break;
                    }
                    case 9:
                    {
                        List<object> list28 = new List<object>();
                        List<string> list29 = new List<string>();
                        DataSet set10 = CallBLL.cz_phase_xyft5_bll.GetCurrentByPhase(str2);
                        if (set10 != null)
                        {
                            DataTable table10 = set10.Tables[0];
                            Dictionary<string, object> dictionary11 = new Dictionary<string, object>();
                            string str96 = table10.Rows[0]["is_closed"];
                            string str97 = table10.Rows[0]["is_opendata"];
                            if (str96.Equals("1") && str97.Equals("1"))
                            {
                                foreach (DataRow row10 in table10.Rows)
                                {
                                    string str98 = "";
                                    string str99 = "";
                                    string str100 = "";
                                    string str101 = "";
                                    string str102 = "";
                                    string str103 = "";
                                    string str104 = "";
                                    string str105 = "";
                                    string str106 = "";
                                    string str107 = "";
                                    str98 = row10["phase"].ToString();
                                    str99 = row10["play_open_date"].ToString();
                                    int num67 = Convert.ToInt32(row10["n1"].ToString());
                                    int num68 = Convert.ToInt32(row10["n2"].ToString());
                                    int num69 = Convert.ToInt32(row10["n3"].ToString());
                                    int num70 = Convert.ToInt32(row10["n4"].ToString());
                                    int num71 = Convert.ToInt32(row10["n5"].ToString());
                                    int num72 = Convert.ToInt32(row10["n6"].ToString());
                                    int num73 = Convert.ToInt32(row10["n7"].ToString());
                                    int num74 = Convert.ToInt32(row10["n8"].ToString());
                                    int num75 = Convert.ToInt32(row10["n9"].ToString());
                                    int num76 = Convert.ToInt32(row10["n10"].ToString());
                                    list29.Add((num67 < 10) ? ("0" + num67) : num67.ToString());
                                    list29.Add((num68 < 10) ? ("0" + num68) : num68.ToString());
                                    list29.Add((num69 < 10) ? ("0" + num69) : num69.ToString());
                                    list29.Add((num70 < 10) ? ("0" + num70) : num70.ToString());
                                    list29.Add((num71 < 10) ? ("0" + num71) : num71.ToString());
                                    list29.Add((num72 < 10) ? ("0" + num72) : num72.ToString());
                                    list29.Add((num73 < 10) ? ("0" + num73) : num73.ToString());
                                    list29.Add((num74 < 10) ? ("0" + num74) : num74.ToString());
                                    list29.Add((num75 < 10) ? ("0" + num75) : num75.ToString());
                                    list29.Add((num76 < 10) ? ("0" + num76) : num76.ToString());
                                    num189 = num67 + num68;
                                    str100 = num189.ToString();
                                    if (Convert.ToInt32(str100) <= 11)
                                    {
                                        str101 = "小";
                                    }
                                    else
                                    {
                                        str101 = "大";
                                    }
                                    str101 = base.SetWordColor(str101);
                                    if ((Convert.ToInt32(str100) % 2) == 0)
                                    {
                                        str102 = "雙";
                                    }
                                    else
                                    {
                                        str102 = "單";
                                    }
                                    str102 = base.SetWordColor(str102);
                                    if (num67 > num76)
                                    {
                                        str103 = "龍";
                                    }
                                    else
                                    {
                                        str103 = "虎";
                                    }
                                    str103 = base.SetWordColor(str103);
                                    if (num68 > num75)
                                    {
                                        str104 = "龍";
                                    }
                                    else
                                    {
                                        str104 = "虎";
                                    }
                                    str104 = base.SetWordColor(str104);
                                    if (num69 > num74)
                                    {
                                        str105 = "龍";
                                    }
                                    else
                                    {
                                        str105 = "虎";
                                    }
                                    str105 = base.SetWordColor(str105);
                                    if (num70 > num73)
                                    {
                                        str106 = "龍";
                                    }
                                    else
                                    {
                                        str106 = "虎";
                                    }
                                    str106 = base.SetWordColor(str106);
                                    if (num71 > num72)
                                    {
                                        str107 = "龍";
                                    }
                                    else
                                    {
                                        str107 = "虎";
                                    }
                                    str107 = base.SetWordColor(str107);
                                    dictionary11.Add("phase", str98);
                                    dictionary11.Add("play_open_date", str99);
                                    dictionary11.Add("draw_num", new List<string>(list29));
                                    dictionary11.Add("total", new List<string> { str100, str101, str102, str103, str104, str105, str106, str107 });
                                    list28.Add(new Dictionary<string, object>(dictionary11));
                                    dictionary11.Clear();
                                    list29.Clear();
                                }
                            }
                        }
                        dictionary.Add("jqkj", list28);
                        break;
                    }
                    case 10:
                    {
                        List<object> list34 = new List<object>();
                        List<string> list35 = new List<string>();
                        DataSet set12 = CallBLL.cz_phase_jscar_bll.GetCurrentByPhase(str2);
                        if (set12 != null)
                        {
                            DataTable table12 = set12.Tables[0];
                            Dictionary<string, object> dictionary13 = new Dictionary<string, object>();
                            string str121 = table12.Rows[0]["is_closed"];
                            string str122 = table12.Rows[0]["is_opendata"];
                            if (str121.Equals("1") && str122.Equals("1"))
                            {
                                foreach (DataRow row12 in table12.Rows)
                                {
                                    string str123 = "";
                                    string str124 = "";
                                    string str125 = "";
                                    string str126 = "";
                                    string str127 = "";
                                    string str128 = "";
                                    string str129 = "";
                                    string str130 = "";
                                    string str131 = "";
                                    string str132 = "";
                                    str123 = row12["phase"].ToString();
                                    str124 = row12["play_open_date"].ToString();
                                    int num87 = Convert.ToInt32(row12["n1"].ToString());
                                    int num88 = Convert.ToInt32(row12["n2"].ToString());
                                    int num89 = Convert.ToInt32(row12["n3"].ToString());
                                    int num90 = Convert.ToInt32(row12["n4"].ToString());
                                    int num91 = Convert.ToInt32(row12["n5"].ToString());
                                    int num92 = Convert.ToInt32(row12["n6"].ToString());
                                    int num93 = Convert.ToInt32(row12["n7"].ToString());
                                    int num94 = Convert.ToInt32(row12["n8"].ToString());
                                    int num95 = Convert.ToInt32(row12["n9"].ToString());
                                    int num96 = Convert.ToInt32(row12["n10"].ToString());
                                    list35.Add((num87 < 10) ? ("0" + num87) : num87.ToString());
                                    list35.Add((num88 < 10) ? ("0" + num88) : num88.ToString());
                                    list35.Add((num89 < 10) ? ("0" + num89) : num89.ToString());
                                    list35.Add((num90 < 10) ? ("0" + num90) : num90.ToString());
                                    list35.Add((num91 < 10) ? ("0" + num91) : num91.ToString());
                                    list35.Add((num92 < 10) ? ("0" + num92) : num92.ToString());
                                    list35.Add((num93 < 10) ? ("0" + num93) : num93.ToString());
                                    list35.Add((num94 < 10) ? ("0" + num94) : num94.ToString());
                                    list35.Add((num95 < 10) ? ("0" + num95) : num95.ToString());
                                    list35.Add((num96 < 10) ? ("0" + num96) : num96.ToString());
                                    num189 = num87 + num88;
                                    str125 = num189.ToString();
                                    if (Convert.ToInt32(str125) <= 11)
                                    {
                                        str126 = "小";
                                    }
                                    else
                                    {
                                        str126 = "大";
                                    }
                                    str126 = base.SetWordColor(str126);
                                    if ((Convert.ToInt32(str125) % 2) == 0)
                                    {
                                        str127 = "雙";
                                    }
                                    else
                                    {
                                        str127 = "單";
                                    }
                                    str127 = base.SetWordColor(str127);
                                    if (num87 > num96)
                                    {
                                        str128 = "龍";
                                    }
                                    else
                                    {
                                        str128 = "虎";
                                    }
                                    str128 = base.SetWordColor(str128);
                                    if (num88 > num95)
                                    {
                                        str129 = "龍";
                                    }
                                    else
                                    {
                                        str129 = "虎";
                                    }
                                    str129 = base.SetWordColor(str129);
                                    if (num89 > num94)
                                    {
                                        str130 = "龍";
                                    }
                                    else
                                    {
                                        str130 = "虎";
                                    }
                                    str130 = base.SetWordColor(str130);
                                    if (num90 > num93)
                                    {
                                        str131 = "龍";
                                    }
                                    else
                                    {
                                        str131 = "虎";
                                    }
                                    str131 = base.SetWordColor(str131);
                                    if (num91 > num92)
                                    {
                                        str132 = "龍";
                                    }
                                    else
                                    {
                                        str132 = "虎";
                                    }
                                    str132 = base.SetWordColor(str132);
                                    dictionary13.Add("phase", str123);
                                    dictionary13.Add("play_open_date", str124);
                                    dictionary13.Add("draw_num", new List<string>(list35));
                                    dictionary13.Add("total", new List<string> { str125, str126, str127, str128, str129, str130, str131, str132 });
                                    list34.Add(new Dictionary<string, object>(dictionary13));
                                    dictionary13.Clear();
                                    list35.Clear();
                                }
                            }
                        }
                        dictionary.Add("jqkj", list34);
                        break;
                    }
                    case 11:
                    {
                        List<object> list37 = new List<object>();
                        List<string> list38 = new List<string>();
                        DataSet set13 = CallBLL.cz_phase_speed5_bll.GetCurrentByPhase(str2);
                        if (set13 != null)
                        {
                            DataTable table13 = set13.Tables[0];
                            Dictionary<string, object> dictionary14 = new Dictionary<string, object>();
                            string str133 = table13.Rows[0]["is_closed"];
                            string str134 = table13.Rows[0]["is_opendata"];
                            if (str133.Equals("1") && str134.Equals("1"))
                            {
                                foreach (DataRow row13 in table13.Rows)
                                {
                                    string str135 = "";
                                    string str136 = "";
                                    string str137 = "";
                                    string str138 = "";
                                    string str139 = "";
                                    string str140 = "";
                                    string str141 = "";
                                    string str142 = "";
                                    string str143 = "";
                                    str135 = row13["phase"].ToString();
                                    str136 = row13["play_open_date"].ToString();
                                    int num97 = Convert.ToInt32(row13["n1"].ToString());
                                    int num98 = Convert.ToInt32(row13["n2"].ToString());
                                    int num99 = Convert.ToInt32(row13["n3"].ToString());
                                    int num100 = Convert.ToInt32(row13["n4"].ToString());
                                    int num101 = Convert.ToInt32(row13["n5"].ToString());
                                    list38.Add(num97.ToString());
                                    list38.Add(num98.ToString());
                                    list38.Add(num99.ToString());
                                    list38.Add(num100.ToString());
                                    list38.Add(num101.ToString());
                                    num189 = (((num97 + num98) + num99) + num100) + num101;
                                    str137 = num189.ToString();
                                    str138 = SPEED5Phase.get_cl_zhdx(str137);
                                    str138 = base.SetWordColor(str138);
                                    str139 = SPEED5Phase.get_cl_zhds(str137);
                                    str139 = base.SetWordColor(str139);
                                    str140 = SPEED5Phase.get_cl_lh(num97.ToString(), num101.ToString());
                                    str140 = base.SetWordColor(str140);
                                    str141 = SPEED5Phase.get_speed5_str(string.Concat(new object[] { num97, ",", num98, ",", num99 }));
                                    str142 = SPEED5Phase.get_speed5_str(string.Concat(new object[] { num98, ",", num99, ",", num100 }));
                                    str143 = SPEED5Phase.get_speed5_str(string.Concat(new object[] { num99, ",", num100, ",", num101 }));
                                    dictionary14.Add("phase", str135);
                                    dictionary14.Add("play_open_date", str136);
                                    dictionary14.Add("draw_num", new List<string>(list38));
                                    dictionary14.Add("total", new List<string> { str137, str138, str139, str140, str141, str142, str143 });
                                    list37.Add(new Dictionary<string, object>(dictionary14));
                                    dictionary14.Clear();
                                    list38.Clear();
                                }
                            }
                        }
                        dictionary.Add("jqkj", list37);
                        break;
                    }
                    case 12:
                    {
                        List<object> list43 = new List<object>();
                        List<string> list44 = new List<string>();
                        DataSet set15 = CallBLL.cz_phase_jspk10_bll.GetCurrentByPhase(str2);
                        if (set15 != null)
                        {
                            DataTable table15 = set15.Tables[0];
                            Dictionary<string, object> dictionary16 = new Dictionary<string, object>();
                            string str155 = table15.Rows[0]["is_closed"];
                            string str156 = table15.Rows[0]["is_opendata"];
                            if (str155.Equals("1") && str156.Equals("1"))
                            {
                                foreach (DataRow row15 in table15.Rows)
                                {
                                    string str157 = "";
                                    string str158 = "";
                                    string str159 = "";
                                    string str160 = "";
                                    string str161 = "";
                                    string str162 = "";
                                    string str163 = "";
                                    string str164 = "";
                                    string str165 = "";
                                    string str166 = "";
                                    str157 = row15["phase"].ToString();
                                    str158 = row15["play_open_date"].ToString();
                                    int num107 = Convert.ToInt32(row15["n1"].ToString());
                                    int num108 = Convert.ToInt32(row15["n2"].ToString());
                                    int num109 = Convert.ToInt32(row15["n3"].ToString());
                                    int num110 = Convert.ToInt32(row15["n4"].ToString());
                                    int num111 = Convert.ToInt32(row15["n5"].ToString());
                                    int num112 = Convert.ToInt32(row15["n6"].ToString());
                                    int num113 = Convert.ToInt32(row15["n7"].ToString());
                                    int num114 = Convert.ToInt32(row15["n8"].ToString());
                                    int num115 = Convert.ToInt32(row15["n9"].ToString());
                                    int num116 = Convert.ToInt32(row15["n10"].ToString());
                                    list44.Add((num107 < 10) ? ("0" + num107) : num107.ToString());
                                    list44.Add((num108 < 10) ? ("0" + num108) : num108.ToString());
                                    list44.Add((num109 < 10) ? ("0" + num109) : num109.ToString());
                                    list44.Add((num110 < 10) ? ("0" + num110) : num110.ToString());
                                    list44.Add((num111 < 10) ? ("0" + num111) : num111.ToString());
                                    list44.Add((num112 < 10) ? ("0" + num112) : num112.ToString());
                                    list44.Add((num113 < 10) ? ("0" + num113) : num113.ToString());
                                    list44.Add((num114 < 10) ? ("0" + num114) : num114.ToString());
                                    list44.Add((num115 < 10) ? ("0" + num115) : num115.ToString());
                                    list44.Add((num116 < 10) ? ("0" + num116) : num116.ToString());
                                    num189 = num107 + num108;
                                    str159 = num189.ToString();
                                    if (Convert.ToInt32(str159) <= 11)
                                    {
                                        str160 = "小";
                                    }
                                    else
                                    {
                                        str160 = "大";
                                    }
                                    str160 = base.SetWordColor(str160);
                                    if ((Convert.ToInt32(str159) % 2) == 0)
                                    {
                                        str161 = "雙";
                                    }
                                    else
                                    {
                                        str161 = "單";
                                    }
                                    str161 = base.SetWordColor(str161);
                                    if (num107 > num116)
                                    {
                                        str162 = "龍";
                                    }
                                    else
                                    {
                                        str162 = "虎";
                                    }
                                    str162 = base.SetWordColor(str162);
                                    if (num108 > num115)
                                    {
                                        str163 = "龍";
                                    }
                                    else
                                    {
                                        str163 = "虎";
                                    }
                                    str163 = base.SetWordColor(str163);
                                    if (num109 > num114)
                                    {
                                        str164 = "龍";
                                    }
                                    else
                                    {
                                        str164 = "虎";
                                    }
                                    str164 = base.SetWordColor(str164);
                                    if (num110 > num113)
                                    {
                                        str165 = "龍";
                                    }
                                    else
                                    {
                                        str165 = "虎";
                                    }
                                    str165 = base.SetWordColor(str165);
                                    if (num111 > num112)
                                    {
                                        str166 = "龍";
                                    }
                                    else
                                    {
                                        str166 = "虎";
                                    }
                                    str166 = base.SetWordColor(str166);
                                    dictionary16.Add("phase", str157);
                                    dictionary16.Add("play_open_date", str158);
                                    dictionary16.Add("draw_num", new List<string>(list44));
                                    dictionary16.Add("total", new List<string> { str159, str160, str161, str162, str163, str164, str165, str166 });
                                    list43.Add(new Dictionary<string, object>(dictionary16));
                                    dictionary16.Clear();
                                    list44.Clear();
                                }
                            }
                        }
                        dictionary.Add("jqkj", list43);
                        break;
                    }
                    case 13:
                    {
                        List<object> list40 = new List<object>();
                        List<string> list41 = new List<string>();
                        DataSet set14 = CallBLL.cz_phase_jscqsc_bll.GetCurrentByPhase(str2);
                        if (set14 != null)
                        {
                            DataTable table14 = set14.Tables[0];
                            Dictionary<string, object> dictionary15 = new Dictionary<string, object>();
                            string str144 = table14.Rows[0]["is_closed"];
                            string str145 = table14.Rows[0]["is_opendata"];
                            if (str144.Equals("1") && str145.Equals("1"))
                            {
                                foreach (DataRow row14 in table14.Rows)
                                {
                                    string str146 = "";
                                    string str147 = "";
                                    string str148 = "";
                                    string str149 = "";
                                    string str150 = "";
                                    string str151 = "";
                                    string str152 = "";
                                    string str153 = "";
                                    string str154 = "";
                                    str146 = row14["phase"].ToString();
                                    str147 = row14["play_open_date"].ToString();
                                    int num102 = Convert.ToInt32(row14["n1"].ToString());
                                    int num103 = Convert.ToInt32(row14["n2"].ToString());
                                    int num104 = Convert.ToInt32(row14["n3"].ToString());
                                    int num105 = Convert.ToInt32(row14["n4"].ToString());
                                    int num106 = Convert.ToInt32(row14["n5"].ToString());
                                    list41.Add(num102.ToString());
                                    list41.Add(num103.ToString());
                                    list41.Add(num104.ToString());
                                    list41.Add(num105.ToString());
                                    list41.Add(num106.ToString());
                                    num189 = (((num102 + num103) + num104) + num105) + num106;
                                    str148 = num189.ToString();
                                    str149 = JSCQSCPhase.get_cl_zhdx(str148);
                                    str149 = base.SetWordColor(str149);
                                    str150 = JSCQSCPhase.get_cl_zhds(str148);
                                    str150 = base.SetWordColor(str150);
                                    str151 = JSCQSCPhase.get_cl_lh(num102.ToString(), num106.ToString());
                                    str151 = base.SetWordColor(str151);
                                    str152 = JSCQSCPhase.get_jscqsc_str(string.Concat(new object[] { num102, ",", num103, ",", num104 }));
                                    str153 = JSCQSCPhase.get_jscqsc_str(string.Concat(new object[] { num103, ",", num104, ",", num105 }));
                                    str154 = JSCQSCPhase.get_jscqsc_str(string.Concat(new object[] { num104, ",", num105, ",", num106 }));
                                    dictionary15.Add("phase", str146);
                                    dictionary15.Add("play_open_date", str147);
                                    dictionary15.Add("draw_num", new List<string>(list41));
                                    dictionary15.Add("total", new List<string> { str148, str149, str150, str151, str152, str153, str154 });
                                    list40.Add(new Dictionary<string, object>(dictionary15));
                                    dictionary15.Clear();
                                    list41.Clear();
                                }
                            }
                        }
                        dictionary.Add("jqkj", list40);
                        break;
                    }
                    case 14:
                    {
                        List<object> list46 = new List<object>();
                        List<string> list47 = new List<string>();
                        DataSet set16 = CallBLL.cz_phase_jssfc_bll.GetCurrentByPhase(str2);
                        if (set16 != null)
                        {
                            DataTable table16 = set16.Tables[0];
                            int num117 = 8;
                            Dictionary<string, object> dictionary17 = new Dictionary<string, object>();
                            string str167 = table16.Rows[0]["is_closed"];
                            string str168 = table16.Rows[0]["is_opendata"];
                            if (str167.Equals("1") && str168.Equals("1"))
                            {
                                foreach (DataRow row16 in table16.Rows)
                                {
                                    string str169 = "";
                                    string str170 = "";
                                    string str171 = "";
                                    string str172 = "";
                                    string str173 = "";
                                    string str174 = "";
                                    string str175 = "";
                                    str169 = row16["phase"].ToString();
                                    str170 = row16["play_open_date"].ToString();
                                    for (int n = 1; n <= num117; n++)
                                    {
                                        string str176 = "n" + n;
                                        list47.Add(row16[str176].ToString());
                                    }
                                    str171 = JSSFCPhase.get_zh(list47).ToString();
                                    str172 = JSSFCPhase.get_cl_zhdx(str171);
                                    str172 = base.SetWordColor(str172);
                                    str173 = JSSFCPhase.get_cl_zhds(str171);
                                    str173 = base.SetWordColor(str173);
                                    str174 = JSSFCPhase.get_cl_zhwsdx(str171);
                                    str174 = base.SetWordColor(str174);
                                    str175 = JSSFCPhase.get_cl_lh(list47[0].ToString(), list47[num117 - 1].ToString());
                                    str175 = base.SetWordColor(str175);
                                    dictionary17.Add("phase", str169);
                                    dictionary17.Add("play_open_date", str170);
                                    dictionary17.Add("draw_num", new List<string>(list47));
                                    dictionary17.Add("total", new List<string> { str171, str172, str173, str174, str175 });
                                    list46.Add(new Dictionary<string, object>(dictionary17));
                                    dictionary17.Clear();
                                    list47.Clear();
                                }
                            }
                        }
                        dictionary.Add("jqkj", list46);
                        break;
                    }
                    case 15:
                    {
                        List<object> list49 = new List<object>();
                        List<string> list50 = new List<string>();
                        DataSet set17 = CallBLL.cz_phase_jsft2_bll.GetCurrentByPhase(str2);
                        if (set17 != null)
                        {
                            DataTable table17 = set17.Tables[0];
                            Dictionary<string, object> dictionary18 = new Dictionary<string, object>();
                            string str177 = table17.Rows[0]["is_closed"];
                            string str178 = table17.Rows[0]["is_opendata"];
                            if (str177.Equals("1") && str178.Equals("1"))
                            {
                                foreach (DataRow row17 in table17.Rows)
                                {
                                    string str179 = "";
                                    string str180 = "";
                                    string str181 = "";
                                    string str182 = "";
                                    string str183 = "";
                                    string str184 = "";
                                    string str185 = "";
                                    string str186 = "";
                                    string str187 = "";
                                    string str188 = "";
                                    str179 = row17["phase"].ToString();
                                    str180 = row17["play_open_date"].ToString();
                                    int num119 = Convert.ToInt32(row17["n1"].ToString());
                                    int num120 = Convert.ToInt32(row17["n2"].ToString());
                                    int num121 = Convert.ToInt32(row17["n3"].ToString());
                                    int num122 = Convert.ToInt32(row17["n4"].ToString());
                                    int num123 = Convert.ToInt32(row17["n5"].ToString());
                                    int num124 = Convert.ToInt32(row17["n6"].ToString());
                                    int num125 = Convert.ToInt32(row17["n7"].ToString());
                                    int num126 = Convert.ToInt32(row17["n8"].ToString());
                                    int num127 = Convert.ToInt32(row17["n9"].ToString());
                                    int num128 = Convert.ToInt32(row17["n10"].ToString());
                                    list50.Add((num119 < 10) ? ("0" + num119) : num119.ToString());
                                    list50.Add((num120 < 10) ? ("0" + num120) : num120.ToString());
                                    list50.Add((num121 < 10) ? ("0" + num121) : num121.ToString());
                                    list50.Add((num122 < 10) ? ("0" + num122) : num122.ToString());
                                    list50.Add((num123 < 10) ? ("0" + num123) : num123.ToString());
                                    list50.Add((num124 < 10) ? ("0" + num124) : num124.ToString());
                                    list50.Add((num125 < 10) ? ("0" + num125) : num125.ToString());
                                    list50.Add((num126 < 10) ? ("0" + num126) : num126.ToString());
                                    list50.Add((num127 < 10) ? ("0" + num127) : num127.ToString());
                                    list50.Add((num128 < 10) ? ("0" + num128) : num128.ToString());
                                    num189 = num119 + num120;
                                    str181 = num189.ToString();
                                    if (Convert.ToInt32(str181) <= 11)
                                    {
                                        str182 = "小";
                                    }
                                    else
                                    {
                                        str182 = "大";
                                    }
                                    str182 = base.SetWordColor(str182);
                                    if ((Convert.ToInt32(str181) % 2) == 0)
                                    {
                                        str183 = "雙";
                                    }
                                    else
                                    {
                                        str183 = "單";
                                    }
                                    str183 = base.SetWordColor(str183);
                                    if (num119 > num128)
                                    {
                                        str184 = "龍";
                                    }
                                    else
                                    {
                                        str184 = "虎";
                                    }
                                    str184 = base.SetWordColor(str184);
                                    if (num120 > num127)
                                    {
                                        str185 = "龍";
                                    }
                                    else
                                    {
                                        str185 = "虎";
                                    }
                                    str185 = base.SetWordColor(str185);
                                    if (num121 > num126)
                                    {
                                        str186 = "龍";
                                    }
                                    else
                                    {
                                        str186 = "虎";
                                    }
                                    str186 = base.SetWordColor(str186);
                                    if (num122 > num125)
                                    {
                                        str187 = "龍";
                                    }
                                    else
                                    {
                                        str187 = "虎";
                                    }
                                    str187 = base.SetWordColor(str187);
                                    if (num123 > num124)
                                    {
                                        str188 = "龍";
                                    }
                                    else
                                    {
                                        str188 = "虎";
                                    }
                                    str188 = base.SetWordColor(str188);
                                    dictionary18.Add("phase", str179);
                                    dictionary18.Add("play_open_date", str180);
                                    dictionary18.Add("draw_num", new List<string>(list50));
                                    dictionary18.Add("total", new List<string> { str181, str182, str183, str184, str185, str186, str187, str188 });
                                    list49.Add(new Dictionary<string, object>(dictionary18));
                                    dictionary18.Clear();
                                    list50.Clear();
                                }
                            }
                        }
                        dictionary.Add("jqkj", list49);
                        break;
                    }
                    case 0x10:
                    {
                        List<object> list52 = new List<object>();
                        List<string> list53 = new List<string>();
                        DataSet set18 = CallBLL.cz_phase_car168_bll.GetCurrentByPhase(str2);
                        if (set18 != null)
                        {
                            DataTable table18 = set18.Tables[0];
                            Dictionary<string, object> dictionary19 = new Dictionary<string, object>();
                            string str189 = table18.Rows[0]["is_closed"];
                            string str190 = table18.Rows[0]["is_opendata"];
                            if (str189.Equals("1") && str190.Equals("1"))
                            {
                                foreach (DataRow row18 in table18.Rows)
                                {
                                    string str191 = "";
                                    string str192 = "";
                                    string str193 = "";
                                    string str194 = "";
                                    string str195 = "";
                                    string str196 = "";
                                    string str197 = "";
                                    string str198 = "";
                                    string str199 = "";
                                    string str200 = "";
                                    str191 = row18["phase"].ToString();
                                    str192 = row18["play_open_date"].ToString();
                                    int num129 = Convert.ToInt32(row18["n1"].ToString());
                                    int num130 = Convert.ToInt32(row18["n2"].ToString());
                                    int num131 = Convert.ToInt32(row18["n3"].ToString());
                                    int num132 = Convert.ToInt32(row18["n4"].ToString());
                                    int num133 = Convert.ToInt32(row18["n5"].ToString());
                                    int num134 = Convert.ToInt32(row18["n6"].ToString());
                                    int num135 = Convert.ToInt32(row18["n7"].ToString());
                                    int num136 = Convert.ToInt32(row18["n8"].ToString());
                                    int num137 = Convert.ToInt32(row18["n9"].ToString());
                                    int num138 = Convert.ToInt32(row18["n10"].ToString());
                                    list53.Add((num129 < 10) ? ("0" + num129) : num129.ToString());
                                    list53.Add((num130 < 10) ? ("0" + num130) : num130.ToString());
                                    list53.Add((num131 < 10) ? ("0" + num131) : num131.ToString());
                                    list53.Add((num132 < 10) ? ("0" + num132) : num132.ToString());
                                    list53.Add((num133 < 10) ? ("0" + num133) : num133.ToString());
                                    list53.Add((num134 < 10) ? ("0" + num134) : num134.ToString());
                                    list53.Add((num135 < 10) ? ("0" + num135) : num135.ToString());
                                    list53.Add((num136 < 10) ? ("0" + num136) : num136.ToString());
                                    list53.Add((num137 < 10) ? ("0" + num137) : num137.ToString());
                                    list53.Add((num138 < 10) ? ("0" + num138) : num138.ToString());
                                    num189 = num129 + num130;
                                    str193 = num189.ToString();
                                    if (Convert.ToInt32(str193) <= 11)
                                    {
                                        str194 = "小";
                                    }
                                    else
                                    {
                                        str194 = "大";
                                    }
                                    str194 = base.SetWordColor(str194);
                                    if ((Convert.ToInt32(str193) % 2) == 0)
                                    {
                                        str195 = "雙";
                                    }
                                    else
                                    {
                                        str195 = "單";
                                    }
                                    str195 = base.SetWordColor(str195);
                                    if (num129 > num138)
                                    {
                                        str196 = "龍";
                                    }
                                    else
                                    {
                                        str196 = "虎";
                                    }
                                    str196 = base.SetWordColor(str196);
                                    if (num130 > num137)
                                    {
                                        str197 = "龍";
                                    }
                                    else
                                    {
                                        str197 = "虎";
                                    }
                                    str197 = base.SetWordColor(str197);
                                    if (num131 > num136)
                                    {
                                        str198 = "龍";
                                    }
                                    else
                                    {
                                        str198 = "虎";
                                    }
                                    str198 = base.SetWordColor(str198);
                                    if (num132 > num135)
                                    {
                                        str199 = "龍";
                                    }
                                    else
                                    {
                                        str199 = "虎";
                                    }
                                    str199 = base.SetWordColor(str199);
                                    if (num133 > num134)
                                    {
                                        str200 = "龍";
                                    }
                                    else
                                    {
                                        str200 = "虎";
                                    }
                                    str200 = base.SetWordColor(str200);
                                    dictionary19.Add("phase", str191);
                                    dictionary19.Add("play_open_date", str192);
                                    dictionary19.Add("draw_num", new List<string>(list53));
                                    dictionary19.Add("total", new List<string> { str193, str194, str195, str196, str197, str198, str199, str200 });
                                    list52.Add(new Dictionary<string, object>(dictionary19));
                                    dictionary19.Clear();
                                    list53.Clear();
                                }
                            }
                        }
                        dictionary.Add("jqkj", list52);
                        break;
                    }
                    case 0x11:
                    {
                        List<object> list55 = new List<object>();
                        List<string> list56 = new List<string>();
                        DataSet set19 = CallBLL.cz_phase_ssc168_bll.GetCurrentByPhase(str2);
                        if (set19 != null)
                        {
                            DataTable table19 = set19.Tables[0];
                            Dictionary<string, object> dictionary20 = new Dictionary<string, object>();
                            string str201 = table19.Rows[0]["is_closed"];
                            string str202 = table19.Rows[0]["is_opendata"];
                            if (str201.Equals("1") && str202.Equals("1"))
                            {
                                foreach (DataRow row19 in table19.Rows)
                                {
                                    string str203 = "";
                                    string str204 = "";
                                    string str205 = "";
                                    string str206 = "";
                                    string str207 = "";
                                    string str208 = "";
                                    string str209 = "";
                                    string str210 = "";
                                    string str211 = "";
                                    str203 = row19["phase"].ToString();
                                    str204 = row19["play_open_date"].ToString();
                                    int num139 = Convert.ToInt32(row19["n1"].ToString());
                                    int num140 = Convert.ToInt32(row19["n2"].ToString());
                                    int num141 = Convert.ToInt32(row19["n3"].ToString());
                                    int num142 = Convert.ToInt32(row19["n4"].ToString());
                                    int num143 = Convert.ToInt32(row19["n5"].ToString());
                                    list56.Add(num139.ToString());
                                    list56.Add(num140.ToString());
                                    list56.Add(num141.ToString());
                                    list56.Add(num142.ToString());
                                    list56.Add(num143.ToString());
                                    num189 = (((num139 + num140) + num141) + num142) + num143;
                                    str205 = num189.ToString();
                                    str206 = SSC168Phase.get_cl_zhdx(str205);
                                    str206 = base.SetWordColor(str206);
                                    str207 = SSC168Phase.get_cl_zhds(str205);
                                    str207 = base.SetWordColor(str207);
                                    str208 = SSC168Phase.get_cl_lh(num139.ToString(), num143.ToString());
                                    str208 = base.SetWordColor(str208);
                                    str209 = SSC168Phase.get_ssc168_str(string.Concat(new object[] { num139, ",", num140, ",", num141 }));
                                    str210 = SSC168Phase.get_ssc168_str(string.Concat(new object[] { num140, ",", num141, ",", num142 }));
                                    str211 = SSC168Phase.get_ssc168_str(string.Concat(new object[] { num141, ",", num142, ",", num143 }));
                                    dictionary20.Add("phase", str203);
                                    dictionary20.Add("play_open_date", str204);
                                    dictionary20.Add("draw_num", new List<string>(list56));
                                    dictionary20.Add("total", new List<string> { str205, str206, str207, str208, str209, str210, str211 });
                                    list55.Add(new Dictionary<string, object>(dictionary20));
                                    dictionary20.Clear();
                                    list56.Clear();
                                }
                            }
                        }
                        dictionary.Add("jqkj", list55);
                        break;
                    }
                    case 0x12:
                    {
                        List<object> list58 = new List<object>();
                        List<string> list59 = new List<string>();
                        DataSet set20 = CallBLL.cz_phase_vrcar_bll.GetCurrentByPhase(str2);
                        if (set20 != null)
                        {
                            DataTable table20 = set20.Tables[0];
                            Dictionary<string, object> dictionary21 = new Dictionary<string, object>();
                            string str212 = table20.Rows[0]["is_closed"];
                            string str213 = table20.Rows[0]["is_opendata"];
                            if (str212.Equals("1") && str213.Equals("1"))
                            {
                                foreach (DataRow row20 in table20.Rows)
                                {
                                    string str214 = "";
                                    string str215 = "";
                                    string str216 = "";
                                    string str217 = "";
                                    string str218 = "";
                                    string str219 = "";
                                    string str220 = "";
                                    string str221 = "";
                                    string str222 = "";
                                    string str223 = "";
                                    str214 = row20["phase"].ToString();
                                    str215 = row20["play_open_date"].ToString();
                                    int num144 = Convert.ToInt32(row20["n1"].ToString());
                                    int num145 = Convert.ToInt32(row20["n2"].ToString());
                                    int num146 = Convert.ToInt32(row20["n3"].ToString());
                                    int num147 = Convert.ToInt32(row20["n4"].ToString());
                                    int num148 = Convert.ToInt32(row20["n5"].ToString());
                                    int num149 = Convert.ToInt32(row20["n6"].ToString());
                                    int num150 = Convert.ToInt32(row20["n7"].ToString());
                                    int num151 = Convert.ToInt32(row20["n8"].ToString());
                                    int num152 = Convert.ToInt32(row20["n9"].ToString());
                                    int num153 = Convert.ToInt32(row20["n10"].ToString());
                                    list59.Add((num144 < 10) ? ("0" + num144) : num144.ToString());
                                    list59.Add((num145 < 10) ? ("0" + num145) : num145.ToString());
                                    list59.Add((num146 < 10) ? ("0" + num146) : num146.ToString());
                                    list59.Add((num147 < 10) ? ("0" + num147) : num147.ToString());
                                    list59.Add((num148 < 10) ? ("0" + num148) : num148.ToString());
                                    list59.Add((num149 < 10) ? ("0" + num149) : num149.ToString());
                                    list59.Add((num150 < 10) ? ("0" + num150) : num150.ToString());
                                    list59.Add((num151 < 10) ? ("0" + num151) : num151.ToString());
                                    list59.Add((num152 < 10) ? ("0" + num152) : num152.ToString());
                                    list59.Add((num153 < 10) ? ("0" + num153) : num153.ToString());
                                    num189 = num144 + num145;
                                    str216 = num189.ToString();
                                    if (Convert.ToInt32(str216) <= 11)
                                    {
                                        str217 = "小";
                                    }
                                    else
                                    {
                                        str217 = "大";
                                    }
                                    str217 = base.SetWordColor(str217);
                                    if ((Convert.ToInt32(str216) % 2) == 0)
                                    {
                                        str218 = "雙";
                                    }
                                    else
                                    {
                                        str218 = "單";
                                    }
                                    str218 = base.SetWordColor(str218);
                                    if (num144 > num153)
                                    {
                                        str219 = "龍";
                                    }
                                    else
                                    {
                                        str219 = "虎";
                                    }
                                    str219 = base.SetWordColor(str219);
                                    if (num145 > num152)
                                    {
                                        str220 = "龍";
                                    }
                                    else
                                    {
                                        str220 = "虎";
                                    }
                                    str220 = base.SetWordColor(str220);
                                    if (num146 > num151)
                                    {
                                        str221 = "龍";
                                    }
                                    else
                                    {
                                        str221 = "虎";
                                    }
                                    str221 = base.SetWordColor(str221);
                                    if (num147 > num150)
                                    {
                                        str222 = "龍";
                                    }
                                    else
                                    {
                                        str222 = "虎";
                                    }
                                    str222 = base.SetWordColor(str222);
                                    if (num148 > num149)
                                    {
                                        str223 = "龍";
                                    }
                                    else
                                    {
                                        str223 = "虎";
                                    }
                                    str223 = base.SetWordColor(str223);
                                    dictionary21.Add("phase", str214);
                                    dictionary21.Add("play_open_date", str215);
                                    dictionary21.Add("draw_num", new List<string>(list59));
                                    dictionary21.Add("total", new List<string> { str216, str217, str218, str219, str220, str221, str222, str223 });
                                    list58.Add(new Dictionary<string, object>(dictionary21));
                                    dictionary21.Clear();
                                    list59.Clear();
                                }
                            }
                        }
                        dictionary.Add("jqkj", list58);
                        break;
                    }
                    case 0x13:
                    {
                        List<object> list61 = new List<object>();
                        List<string> list62 = new List<string>();
                        DataSet set21 = CallBLL.cz_phase_vrssc_bll.GetCurrentByPhase(str2);
                        if (set21 != null)
                        {
                            DataTable table21 = set21.Tables[0];
                            Dictionary<string, object> dictionary22 = new Dictionary<string, object>();
                            string str224 = table21.Rows[0]["is_closed"];
                            string str225 = table21.Rows[0]["is_opendata"];
                            if (str224.Equals("1") && str225.Equals("1"))
                            {
                                foreach (DataRow row21 in table21.Rows)
                                {
                                    string str226 = "";
                                    string str227 = "";
                                    string str228 = "";
                                    string str229 = "";
                                    string str230 = "";
                                    string str231 = "";
                                    string str232 = "";
                                    string str233 = "";
                                    string str234 = "";
                                    str226 = row21["phase"].ToString();
                                    str227 = row21["play_open_date"].ToString();
                                    int num154 = Convert.ToInt32(row21["n1"].ToString());
                                    int num155 = Convert.ToInt32(row21["n2"].ToString());
                                    int num156 = Convert.ToInt32(row21["n3"].ToString());
                                    int num157 = Convert.ToInt32(row21["n4"].ToString());
                                    int num158 = Convert.ToInt32(row21["n5"].ToString());
                                    list62.Add(num154.ToString());
                                    list62.Add(num155.ToString());
                                    list62.Add(num156.ToString());
                                    list62.Add(num157.ToString());
                                    list62.Add(num158.ToString());
                                    num189 = (((num154 + num155) + num156) + num157) + num158;
                                    str228 = num189.ToString();
                                    str229 = VRSSCPhase.get_cl_zhdx(str228);
                                    str229 = base.SetWordColor(str229);
                                    str230 = VRSSCPhase.get_cl_zhds(str228);
                                    str230 = base.SetWordColor(str230);
                                    str231 = VRSSCPhase.get_cl_lh(num154.ToString(), num158.ToString());
                                    str231 = base.SetWordColor(str231);
                                    str232 = VRSSCPhase.get_vrssc_str(string.Concat(new object[] { num154, ",", num155, ",", num156 }));
                                    str233 = VRSSCPhase.get_vrssc_str(string.Concat(new object[] { num155, ",", num156, ",", num157 }));
                                    str234 = VRSSCPhase.get_vrssc_str(string.Concat(new object[] { num156, ",", num157, ",", num158 }));
                                    dictionary22.Add("phase", str226);
                                    dictionary22.Add("play_open_date", str227);
                                    dictionary22.Add("draw_num", new List<string>(list62));
                                    dictionary22.Add("total", new List<string> { str228, str229, str230, str231, str232, str233, str234 });
                                    list61.Add(new Dictionary<string, object>(dictionary22));
                                    dictionary22.Clear();
                                    list62.Clear();
                                }
                            }
                        }
                        dictionary.Add("jqkj", list61);
                        break;
                    }
                    case 20:
                    {
                        List<object> list64 = new List<object>();
                        List<string> list65 = new List<string>();
                        DataSet set22 = CallBLL.cz_phase_xyftoa_bll.GetCurrentByPhase(str2);
                        if (set22 != null)
                        {
                            DataTable table22 = set22.Tables[0];
                            Dictionary<string, object> dictionary23 = new Dictionary<string, object>();
                            string str235 = table22.Rows[0]["is_closed"];
                            string str236 = table22.Rows[0]["is_opendata"];
                            if (str235.Equals("1") && str236.Equals("1"))
                            {
                                foreach (DataRow row22 in table22.Rows)
                                {
                                    string str237 = "";
                                    string str238 = "";
                                    string str239 = "";
                                    string str240 = "";
                                    string str241 = "";
                                    string str242 = "";
                                    string str243 = "";
                                    string str244 = "";
                                    string str245 = "";
                                    string str246 = "";
                                    str237 = row22["phase"].ToString();
                                    str238 = row22["play_open_date"].ToString();
                                    int num159 = Convert.ToInt32(row22["n1"].ToString());
                                    int num160 = Convert.ToInt32(row22["n2"].ToString());
                                    int num161 = Convert.ToInt32(row22["n3"].ToString());
                                    int num162 = Convert.ToInt32(row22["n4"].ToString());
                                    int num163 = Convert.ToInt32(row22["n5"].ToString());
                                    int num164 = Convert.ToInt32(row22["n6"].ToString());
                                    int num165 = Convert.ToInt32(row22["n7"].ToString());
                                    int num166 = Convert.ToInt32(row22["n8"].ToString());
                                    int num167 = Convert.ToInt32(row22["n9"].ToString());
                                    int num168 = Convert.ToInt32(row22["n10"].ToString());
                                    list65.Add((num159 < 10) ? ("0" + num159) : num159.ToString());
                                    list65.Add((num160 < 10) ? ("0" + num160) : num160.ToString());
                                    list65.Add((num161 < 10) ? ("0" + num161) : num161.ToString());
                                    list65.Add((num162 < 10) ? ("0" + num162) : num162.ToString());
                                    list65.Add((num163 < 10) ? ("0" + num163) : num163.ToString());
                                    list65.Add((num164 < 10) ? ("0" + num164) : num164.ToString());
                                    list65.Add((num165 < 10) ? ("0" + num165) : num165.ToString());
                                    list65.Add((num166 < 10) ? ("0" + num166) : num166.ToString());
                                    list65.Add((num167 < 10) ? ("0" + num167) : num167.ToString());
                                    list65.Add((num168 < 10) ? ("0" + num168) : num168.ToString());
                                    num189 = num159 + num160;
                                    str239 = num189.ToString();
                                    if (Convert.ToInt32(str239) <= 11)
                                    {
                                        str240 = "小";
                                    }
                                    else
                                    {
                                        str240 = "大";
                                    }
                                    str240 = base.SetWordColor(str240);
                                    if ((Convert.ToInt32(str239) % 2) == 0)
                                    {
                                        str241 = "雙";
                                    }
                                    else
                                    {
                                        str241 = "單";
                                    }
                                    str241 = base.SetWordColor(str241);
                                    if (num159 > num168)
                                    {
                                        str242 = "龍";
                                    }
                                    else
                                    {
                                        str242 = "虎";
                                    }
                                    str242 = base.SetWordColor(str242);
                                    if (num160 > num167)
                                    {
                                        str243 = "龍";
                                    }
                                    else
                                    {
                                        str243 = "虎";
                                    }
                                    str243 = base.SetWordColor(str243);
                                    if (num161 > num166)
                                    {
                                        str244 = "龍";
                                    }
                                    else
                                    {
                                        str244 = "虎";
                                    }
                                    str244 = base.SetWordColor(str244);
                                    if (num162 > num165)
                                    {
                                        str245 = "龍";
                                    }
                                    else
                                    {
                                        str245 = "虎";
                                    }
                                    str245 = base.SetWordColor(str245);
                                    if (num163 > num164)
                                    {
                                        str246 = "龍";
                                    }
                                    else
                                    {
                                        str246 = "虎";
                                    }
                                    str246 = base.SetWordColor(str246);
                                    dictionary23.Add("phase", str237);
                                    dictionary23.Add("play_open_date", str238);
                                    dictionary23.Add("draw_num", new List<string>(list65));
                                    dictionary23.Add("total", new List<string> { str239, str240, str241, str242, str243, str244, str245, str246 });
                                    list64.Add(new Dictionary<string, object>(dictionary23));
                                    dictionary23.Clear();
                                    list65.Clear();
                                }
                            }
                        }
                        dictionary.Add("jqkj", list64);
                        break;
                    }
                    case 0x15:
                    {
                        List<object> list67 = new List<object>();
                        List<string> list68 = new List<string>();
                        DataSet set23 = CallBLL.cz_phase_xyftsg_bll.GetCurrentByPhase(str2);
                        if (set23 != null)
                        {
                            DataTable table23 = set23.Tables[0];
                            Dictionary<string, object> dictionary24 = new Dictionary<string, object>();
                            string str247 = table23.Rows[0]["is_closed"];
                            string str248 = table23.Rows[0]["is_opendata"];
                            if (str247.Equals("1") && str248.Equals("1"))
                            {
                                foreach (DataRow row23 in table23.Rows)
                                {
                                    string str249 = "";
                                    string str250 = "";
                                    string str251 = "";
                                    string str252 = "";
                                    string str253 = "";
                                    string str254 = "";
                                    string str255 = "";
                                    string str256 = "";
                                    string str257 = "";
                                    string str258 = "";
                                    str249 = row23["phase"].ToString();
                                    str250 = row23["play_open_date"].ToString();
                                    int num169 = Convert.ToInt32(row23["n1"].ToString());
                                    int num170 = Convert.ToInt32(row23["n2"].ToString());
                                    int num171 = Convert.ToInt32(row23["n3"].ToString());
                                    int num172 = Convert.ToInt32(row23["n4"].ToString());
                                    int num173 = Convert.ToInt32(row23["n5"].ToString());
                                    int num174 = Convert.ToInt32(row23["n6"].ToString());
                                    int num175 = Convert.ToInt32(row23["n7"].ToString());
                                    int num176 = Convert.ToInt32(row23["n8"].ToString());
                                    int num177 = Convert.ToInt32(row23["n9"].ToString());
                                    int num178 = Convert.ToInt32(row23["n10"].ToString());
                                    list68.Add((num169 < 10) ? ("0" + num169) : num169.ToString());
                                    list68.Add((num170 < 10) ? ("0" + num170) : num170.ToString());
                                    list68.Add((num171 < 10) ? ("0" + num171) : num171.ToString());
                                    list68.Add((num172 < 10) ? ("0" + num172) : num172.ToString());
                                    list68.Add((num173 < 10) ? ("0" + num173) : num173.ToString());
                                    list68.Add((num174 < 10) ? ("0" + num174) : num174.ToString());
                                    list68.Add((num175 < 10) ? ("0" + num175) : num175.ToString());
                                    list68.Add((num176 < 10) ? ("0" + num176) : num176.ToString());
                                    list68.Add((num177 < 10) ? ("0" + num177) : num177.ToString());
                                    list68.Add((num178 < 10) ? ("0" + num178) : num178.ToString());
                                    num189 = num169 + num170;
                                    str251 = num189.ToString();
                                    if (Convert.ToInt32(str251) <= 11)
                                    {
                                        str252 = "小";
                                    }
                                    else
                                    {
                                        str252 = "大";
                                    }
                                    str252 = base.SetWordColor(str252);
                                    if ((Convert.ToInt32(str251) % 2) == 0)
                                    {
                                        str253 = "雙";
                                    }
                                    else
                                    {
                                        str253 = "單";
                                    }
                                    str253 = base.SetWordColor(str253);
                                    if (num169 > num178)
                                    {
                                        str254 = "龍";
                                    }
                                    else
                                    {
                                        str254 = "虎";
                                    }
                                    str254 = base.SetWordColor(str254);
                                    if (num170 > num177)
                                    {
                                        str255 = "龍";
                                    }
                                    else
                                    {
                                        str255 = "虎";
                                    }
                                    str255 = base.SetWordColor(str255);
                                    if (num171 > num176)
                                    {
                                        str256 = "龍";
                                    }
                                    else
                                    {
                                        str256 = "虎";
                                    }
                                    str256 = base.SetWordColor(str256);
                                    if (num172 > num175)
                                    {
                                        str257 = "龍";
                                    }
                                    else
                                    {
                                        str257 = "虎";
                                    }
                                    str257 = base.SetWordColor(str257);
                                    if (num173 > num174)
                                    {
                                        str258 = "龍";
                                    }
                                    else
                                    {
                                        str258 = "虎";
                                    }
                                    str258 = base.SetWordColor(str258);
                                    dictionary24.Add("phase", str249);
                                    dictionary24.Add("play_open_date", str250);
                                    dictionary24.Add("draw_num", new List<string>(list68));
                                    dictionary24.Add("total", new List<string> { str251, str252, str253, str254, str255, str256, str257, str258 });
                                    list67.Add(new Dictionary<string, object>(dictionary24));
                                    dictionary24.Clear();
                                    list68.Clear();
                                }
                            }
                        }
                        dictionary.Add("jqkj", list67);
                        break;
                    }
                    case 0x16:
                    {
                        List<object> list70 = new List<object>();
                        List<string> list71 = new List<string>();
                        DataSet set24 = CallBLL.cz_phase_happycar_bll.GetCurrentByPhase(str2);
                        if (set24 != null)
                        {
                            DataTable table24 = set24.Tables[0];
                            Dictionary<string, object> dictionary25 = new Dictionary<string, object>();
                            string str259 = table24.Rows[0]["is_closed"];
                            string str260 = table24.Rows[0]["is_opendata"];
                            if (str259.Equals("1") && str260.Equals("1"))
                            {
                                foreach (DataRow row24 in table24.Rows)
                                {
                                    string str261 = "";
                                    string str262 = "";
                                    string str263 = "";
                                    string str264 = "";
                                    string str265 = "";
                                    string str266 = "";
                                    string str267 = "";
                                    string str268 = "";
                                    string str269 = "";
                                    string str270 = "";
                                    str261 = row24["phase"].ToString();
                                    str262 = row24["play_open_date"].ToString();
                                    int num179 = Convert.ToInt32(row24["n1"].ToString());
                                    int num180 = Convert.ToInt32(row24["n2"].ToString());
                                    int num181 = Convert.ToInt32(row24["n3"].ToString());
                                    int num182 = Convert.ToInt32(row24["n4"].ToString());
                                    int num183 = Convert.ToInt32(row24["n5"].ToString());
                                    int num184 = Convert.ToInt32(row24["n6"].ToString());
                                    int num185 = Convert.ToInt32(row24["n7"].ToString());
                                    int num186 = Convert.ToInt32(row24["n8"].ToString());
                                    int num187 = Convert.ToInt32(row24["n9"].ToString());
                                    int num188 = Convert.ToInt32(row24["n10"].ToString());
                                    list71.Add((num179 < 10) ? ("0" + num179) : num179.ToString());
                                    list71.Add((num180 < 10) ? ("0" + num180) : num180.ToString());
                                    list71.Add((num181 < 10) ? ("0" + num181) : num181.ToString());
                                    list71.Add((num182 < 10) ? ("0" + num182) : num182.ToString());
                                    list71.Add((num183 < 10) ? ("0" + num183) : num183.ToString());
                                    list71.Add((num184 < 10) ? ("0" + num184) : num184.ToString());
                                    list71.Add((num185 < 10) ? ("0" + num185) : num185.ToString());
                                    list71.Add((num186 < 10) ? ("0" + num186) : num186.ToString());
                                    list71.Add((num187 < 10) ? ("0" + num187) : num187.ToString());
                                    list71.Add((num188 < 10) ? ("0" + num188) : num188.ToString());
                                    num189 = num179 + num180;
                                    str263 = num189.ToString();
                                    if (Convert.ToInt32(str263) <= 11)
                                    {
                                        str264 = "小";
                                    }
                                    else
                                    {
                                        str264 = "大";
                                    }
                                    str264 = base.SetWordColor(str264);
                                    if ((Convert.ToInt32(str263) % 2) == 0)
                                    {
                                        str265 = "雙";
                                    }
                                    else
                                    {
                                        str265 = "單";
                                    }
                                    str265 = base.SetWordColor(str265);
                                    if (num179 > num188)
                                    {
                                        str266 = "龍";
                                    }
                                    else
                                    {
                                        str266 = "虎";
                                    }
                                    str266 = base.SetWordColor(str266);
                                    if (num180 > num187)
                                    {
                                        str267 = "龍";
                                    }
                                    else
                                    {
                                        str267 = "虎";
                                    }
                                    str267 = base.SetWordColor(str267);
                                    if (num181 > num186)
                                    {
                                        str268 = "龍";
                                    }
                                    else
                                    {
                                        str268 = "虎";
                                    }
                                    str268 = base.SetWordColor(str268);
                                    if (num182 > num185)
                                    {
                                        str269 = "龍";
                                    }
                                    else
                                    {
                                        str269 = "虎";
                                    }
                                    str269 = base.SetWordColor(str269);
                                    if (num183 > num184)
                                    {
                                        str270 = "龍";
                                    }
                                    else
                                    {
                                        str270 = "虎";
                                    }
                                    str270 = base.SetWordColor(str270);
                                    dictionary25.Add("phase", str261);
                                    dictionary25.Add("play_open_date", str262);
                                    dictionary25.Add("draw_num", new List<string>(list71));
                                    dictionary25.Add("total", new List<string> { str263, str264, str265, str266, str267, str268, str269, str270 });
                                    list70.Add(new Dictionary<string, object>(dictionary25));
                                    dictionary25.Clear();
                                    list71.Clear();
                                }
                            }
                        }
                        dictionary.Add("jqkj", list70);
                        break;
                    }
                    default:
                        if (num189 == 100)
                        {
                            List<object> list = new List<object>();
                            DataSet set = CallBLL.cz_phase_six_bll.GetCurrentByPhase(str2);
                            if (set != null)
                            {
                                List<string> list2 = new List<string>();
                                DataTable table = set.Tables[0];
                                int num = 6;
                                Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
                                string str4 = table.Rows[0]["is_closed"];
                                string str5 = table.Rows[0]["is_opendata"];
                                if (str4.Equals("1") && str5.Equals("1"))
                                {
                                    foreach (DataRow row in table.Rows)
                                    {
                                        string str6 = "";
                                        string str7 = "";
                                        string str8 = "";
                                        string str9 = "";
                                        string str10 = "";
                                        string str11 = "";
                                        string str12 = "";
                                        string str13 = "";
                                        string str14 = "";
                                        string str15 = "";
                                        int num2 = 0;
                                        string str16 = "";
                                        str6 = row["phase"].ToString();
                                        str7 = row["sn_stop_date"].ToString();
                                        for (int num3 = 1; num3 <= num; num3++)
                                        {
                                            string str17 = "n" + num3;
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
                                        dictionary2.Add("phase", str6);
                                        dictionary2.Add("play_open_date", Convert.ToDateTime(str7).ToString("yyyy/MM/dd"));
                                        dictionary2.Add("draw_num", new List<string>(list2));
                                        dictionary2.Add("total", new List<string> { str8, str9, str10, str11, str12, str13, str14, num2.ToString(), str16, str15 });
                                        list.Add(new Dictionary<string, object>(dictionary2));
                                        dictionary2.Clear();
                                        list2.Clear();
                                    }
                                }
                            }
                            dictionary.Add("jqkj", list);
                        }
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
                        if (now < DateTime.Parse(s))
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
                        string str5 = table3.Rows[0]["isopen"].ToString();
                        string str6 = table3.Rows[0]["openning"].ToString();
                        table3.Rows[0]["opendate"].ToString();
                        string str7 = table3.Rows[0]["endtime"].ToString();
                        DateTime time2 = Convert.ToDateTime(lotteryList.Select(string.Format(" id={0} ", num))[0]["begintime"].ToString());
                        if (str5.Equals("0"))
                        {
                            str2 = "0";
                            DateTime time3 = DateTime.Now;
                            string introduced25 = time3.ToString("yyyy-MM-dd");
                            if (introduced25 == time3.AddHours(7.0).ToString("yyyy-MM-dd"))
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
                            if (str6.Equals("n"))
                            {
                                str2 = "1";
                            }
                            else
                            {
                                str2 = "2";
                            }
                            str3 = Convert.ToDateTime(str7).ToString("HH:mm:ss");
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

        public void get_newbetlist(HttpContext context, ref string strResult)
        {
            ReturnResult result = new ReturnResult();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("type", "get_newbetlist");
            agent_userinfo_session model = new agent_userinfo_session();
            string str = context.Session["user_name"].ToString();
            model = context.Session[str + "lottery_session_user_info"] as agent_userinfo_session;
            if (!model.get_u_type().Trim().Equals("zj"))
            {
                context.Response.Write(PageBase.GetMessageByCache("u100027", "MessageHint"));
                context.Response.End();
            }
            if (!string.IsNullOrEmpty(base.Permission_Ashx_ZJ(model, "po_1_1")))
            {
                context.Response.Write(PageBase.GetMessageByCache("u100027", "MessageHint"));
                context.Response.End();
            }
            string str3 = LSRequest.qq("top");
            string str4 = LSRequest.qq("amount");
            string str5 = LSRequest.qq("oldId");
            string str6 = LSRequest.qq("lids");
            if (!string.IsNullOrEmpty(str6))
            {
                string compareTime = LSRequest.qq("oldTime");
                if (string.IsNullOrEmpty(str4))
                {
                    str4 = "0";
                }
                int num = 0;
                if (!string.IsNullOrEmpty(str5))
                {
                    num = Convert.ToInt32(str5);
                }
                int num2 = num;
                DataTable table = CallBLL.cz_bet_kc_bll.GetNewBet(str3, str5, str6, str4);
                if (table != null)
                {
                    Dictionary<string, Dictionary<string, string>> dictionary2 = new Dictionary<string, Dictionary<string, string>>();
                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        Dictionary<string, string> dictionary3 = new Dictionary<string, string>();
                        DataRow row = table.Rows[i];
                        string str8 = row["u_name"].ToString();
                        int num4 = Convert.ToInt32(row["bet_id"].ToString());
                        if (num4 > num2)
                        {
                            num2 = num4;
                        }
                        if ((row["isLM"]).Equals("1"))
                        {
                            string str9 = "";
                            int num5 = 0;
                            if (!string.IsNullOrEmpty(row["unit_cnt"]) && (int.Parse(row["unit_cnt"].ToString()) > 0))
                            {
                                num5 = decimal.ToInt32(Convert.ToDecimal(row["amount"])) / int.Parse(row["unit_cnt"].ToString());
                            }
                            else
                            {
                                num5 = decimal.ToInt32(Convert.ToDecimal(row["amount"]));
                            }
                            if (!string.IsNullOrEmpty(row["unit_cnt"]) && (int.Parse(row["unit_cnt"].ToString()) > 0))
                            {
                                if ((row["sale_type"]).Equals("1"))
                                {
                                    str9 = "-" + Math.Round(Convert.ToDecimal(row["amount"].ToString()), 0).ToString();
                                }
                                else
                                {
                                    str9 = string.Format("<span>￥{0}</span>\x00d7<span>{1}</span>&nbsp;<br>{2}&nbsp;", num5, row["unit_cnt"].ToString(), string.Format("{0:F0}", row["amount"]));
                                }
                            }
                            else
                            {
                                str9 = num5.ToString();
                            }
                            dictionary3.Add("amounttext", str9);
                        }
                        else
                        {
                            dictionary3.Add("amounttext", Math.Round(Convert.ToDecimal(row["amount"].ToString()), 0).ToString());
                        }
                        if ((row["sale_type"]).Equals("1"))
                        {
                            if (model.get_u_name().Equals(str8))
                            {
                                dictionary3.Add("amount", "-" + Math.Round(Convert.ToDecimal(row["amount"].ToString()), 0).ToString());
                            }
                            else if ((row["sale_type"]).Equals("1"))
                            {
                                dictionary3.Add("amount", "-" + Math.Round(Convert.ToDecimal(row["amount"].ToString()), 0).ToString());
                            }
                            else
                            {
                                dictionary3.Add("amount", Math.Round(Convert.ToDecimal(row["amount"].ToString()), 0).ToString());
                            }
                        }
                        else
                        {
                            dictionary3.Add("amount", Math.Round(Convert.ToDecimal(row["amount"].ToString()), 0).ToString());
                        }
                        dictionary3.Add("szamount", row["ds"].ToString());
                        dictionary3.Add("isdelete", row["isDelete"].ToString());
                        row["play_id"].ToString();
                        string str10 = row["odds_id"].ToString();
                        dictionary3.Add("week", Utils.GetWeekByDate(Convert.ToDateTime(row["bet_time"].ToString())));
                        dictionary3.Add("ordernum", row["order_num"].ToString());
                        dictionary3.Add("phase", row["phase"].ToString());
                        dictionary3.Add("bettime", row["bet_time"].ToString());
                        dictionary3.Add("lottery_type", row["lottery_type"].ToString());
                        dictionary3.Add("lottery_name", base.GetGameNameByID(row["lottery_type"].ToString()));
                        dictionary3.Add("islm", row["isLM"].ToString());
                        dictionary3.Add("lmtype", row["lm_type"].ToString());
                        dictionary3.Add("hyname", row["u_name"].ToString());
                        dictionary3.Add("dlname", row["dl_name"]);
                        dictionary3.Add("zdname", row["zd_name"]);
                        dictionary3.Add("gdname", row["gd_name"]);
                        dictionary3.Add("fgsname", row["fgs_name"]);
                        dictionary3.Add("hydrawback", row["hy_drawback"]);
                        dictionary3.Add("dldrawback", row["dl_drawback"]);
                        dictionary3.Add("zddrawback", row["zd_drawback"]);
                        dictionary3.Add("gddrawback", row["gd_drawback"]);
                        dictionary3.Add("fgsdrawback", row["fgs_drawback"]);
                        dictionary3.Add("zjdrawback", row["zj_drawback"]);
                        dictionary3.Add("dlrate", row["dl_rate"]);
                        dictionary3.Add("zdrate", row["zd_rate"]);
                        dictionary3.Add("gdrate", row["gd_rate"]);
                        dictionary3.Add("fgsrate", row["fgs_rate"]);
                        dictionary3.Add("zjrate", row["zj_rate"]);
                        dictionary3.Add("unitcnt", row["unit_cnt"]);
                        dictionary3.Add("pk", row["kind"].ToString());
                        string str11 = "";
                        int num12 = 8;
                        if (row["lottery_type"].ToString().Equals(num12.ToString()))
                        {
                            str11 = string.Format(" #[第{0}桌 {1}] ", row["table_type"].ToString(), Utils.GetPKBJLPlaytypeColorTxt(row["play_type"].ToString()));
                        }
                        dictionary3.Add("tabletype", str11);
                        string str12 = "";
                        if (!string.IsNullOrEmpty(row["unit_cnt"].ToString()) && (int.Parse(row["unit_cnt"].ToString()) > 1))
                        {
                            str12 = base.GroupShowHrefString(2, row["order_num"].ToString(), row["is_payment"].ToString(), "1", "1");
                        }
                        if (row["fgs_name"].ToString().Equals(str))
                        {
                            dictionary3.Add("saletype", "2");
                        }
                        else if ((row["sale_type"]).Equals("1"))
                        {
                            dictionary3.Add("saletype", "1");
                        }
                        else
                        {
                            dictionary3.Add("saletype", "0");
                        }
                        if (((("329".Equals(str10) || "330".Equals(str10)) || ("331".Equals(str10) || "1181".Equals(str10))) || (("1200".Equals(str10) || "1201".Equals(str10)) || ("1202".Equals(str10) || "1203".Equals(str10)))) || (((("330".Equals(str10) || "331".Equals(str10)) || ("1181".Equals(str10) || "1202".Equals(str10))) || (("1203".Equals(str10) || "72055".Equals(str10)) || ("329".Equals(str10) || "330".Equals(str10)))) || ((("331".Equals(str10) || "1181".Equals(str10)) || ("1200".Equals(str10) || "1201".Equals(str10))) || ("1202".Equals(str10) || "1203".Equals(str10)))))
                        {
                            string str13 = row["lm_type"].ToString();
                            dictionary3.Add("playname", row["play_name"].ToString());
                            string str14 = model.get_u_type().Equals("zj") ? row["odds_zj"].ToString() : row["odds"].ToString();
                            dictionary3.Add("odds", str14);
                            if (str13.Equals("0"))
                            {
                                string str15 = "";
                                str15 = str15 + string.Format("<br />【{0}組】 {1}", row["unit_cnt"].ToString(), str12) + string.Format("<br />{0}", row["bet_val"].ToString());
                                dictionary3.Add("betval", str15);
                            }
                        }
                        else
                        {
                            dictionary3.Add("playname", row["play_name"].ToString() + "【" + row["bet_val"].ToString() + "】");
                            dictionary3.Add("odds", model.get_u_type().Equals("zj") ? row["odds_zj"].ToString() : row["odds"].ToString());
                            dictionary3.Add("betval", row["bet_val"].ToString());
                        }
                        dictionary2.Add(num4.ToString(), dictionary3);
                    }
                    dictionary.Add("newbetlist", dictionary2);
                }
                new Dictionary<string, object>();
                dictionary.Add("maxidvalid", num2);
                DateTime now = DateTime.Now;
                List<object> list = base.GetAutoJPForTable(str6, compareTime, ref now);
                Dictionary<string, object> dictionary4 = new Dictionary<string, object>();
                if (list != null)
                {
                    dictionary4.Add("tipsList", list);
                }
                else
                {
                    dictionary4.Add("tipsList", new List<object>());
                }
                dictionary4.Add("timestamp", Utils.DateTimeToStamp(now));
                dictionary.Add("autoJP", dictionary4);
                result.set_data(dictionary);
                strResult = JsonHandle.ObjectToJson(result);
            }
        }

        protected string get_pcdd_bs_str(string num)
        {
            string str = "03,06,09,12,15,18,21,24";
            string str2 = "02,05,08,11,17,20,23,26";
            string str3 = "01,04,07,10,16,19,22,25";
            string s = num;
            if (int.Parse(s) < 10)
            {
                s = "0" + int.Parse(s);
            }
            if (str.IndexOf(s) > -1)
            {
                return "紅波";
            }
            if (str2.IndexOf(s) > -1)
            {
                return "藍波";
            }
            if (str3.IndexOf(s) > -1)
            {
                return "綠波";
            }
            return "-";
        }

        public void get_reportcookies(HttpContext context, ref string strResult)
        {
            ReturnResult result = new ReturnResult();
            result.set_success(400);
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("type", "get_reportcookies");
            if (context.Session["user_name"] == null)
            {
                context.Response.End();
            }
            else
            {
                HttpCookie reportCookies = LSRequest.GetReportCookies();
                Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
                if (reportCookies != null)
                {
                    dictionary2.Add("t_TYPE", reportCookies.Values["t_TYPE"]);
                    dictionary2.Add("t_LT", reportCookies.Values["t_LT"]);
                    dictionary2.Add("gID", reportCookies.Values["gID"]);
                    dictionary2.Add("sltTabletype", reportCookies.Values["sltTabletype"]);
                    dictionary2.Add("sltPlaytype", reportCookies.Values["sltPlaytype"]);
                    dictionary2.Add("t_LID", reportCookies.Values["t_LID"]);
                    dictionary2.Add("isQs", reportCookies.Values["isQs"]);
                    dictionary2.Add("t_FT", reportCookies.Values["t_FT"]);
                    dictionary2.Add("BeginDate", reportCookies.Values["BeginDate"]);
                    dictionary2.Add("EndDate", reportCookies.Values["EndDate"]);
                    dictionary2.Add("ReportType", reportCookies.Values["ReportType"]);
                    dictionary2.Add("t_Balance", reportCookies.Values["t_Balance"]);
                }
                dictionary.Add("cookies", dictionary2);
                result.set_data(dictionary);
                result.set_success(200);
                strResult = JsonHandle.ObjectToJson(result);
            }
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
            if (hm.Trim() == "49")
            {
                return "和";
            }
            if ((int.Parse(hm) % 4) == 0)
            {
                return "4";
            }
            int num2 = int.Parse(hm) % 4;
            return num2.ToString();
        }

        public void get_user_rate(HttpContext context, ref string strResult)
        {
            ReturnResult result = new ReturnResult();
            result.set_success(400);
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("type", "get_user_rate");
            if (context.Session["user_name"] == null)
            {
                context.Response.End();
            }
            else
            {
                string str = LSRequest.qq("uid");
                string str2 = context.Session["user_name"].ToString();
                agent_userinfo_session _session = context.Session[str2 + "lottery_session_user_info"] as agent_userinfo_session;
                cz_users userInfoByUID = CallBLL.cz_users_bll.GetUserInfoByUID(str);
                if (!base.IsUpperLowerLevels(userInfoByUID.get_u_name(), _session.get_u_type(), _session.get_u_name()))
                {
                    context.Response.Write(PageBase.GetMessageByCache("u100014", "MessageHint"));
                    context.Response.End();
                }
                DataTable lotteryList = base.GetLotteryList();
                DataRow[] rowArray = lotteryList.Select(string.Format(" master_id={0} ", 1));
                DataRow[] rowArray2 = lotteryList.Select(string.Format(" master_id={0} ", 2));
                Dictionary<string, Dictionary<string, string>> dictionary2 = new Dictionary<string, Dictionary<string, string>>();
                if (rowArray.Length > 0)
                {
                    DataTable rateByUserID = CallBLL.cz_rate_six_bll.GetRateByUserID(str);
                    rateByUserID.Rows[0]["u_type"].ToString();
                    string str3 = rateByUserID.Rows[0]["su_type"].ToString();
                    string str4 = "";
                    string str5 = "";
                    string str6 = "";
                    string str7 = "";
                    string str8 = "";
                    string str9 = "";
                    string str10 = "";
                    string str11 = "";
                    string str12 = "";
                    string str13 = "";
                    str4 = _session.get_zjname();
                    str5 = rateByUserID.Rows[0]["zj_rate"];
                    str6 = rateByUserID.Rows[0]["fgs_name"];
                    str7 = rateByUserID.Rows[0]["fgs_rate"];
                    if (str3.Equals("dl"))
                    {
                        str8 = rateByUserID.Rows[0]["gd_name"];
                        str9 = rateByUserID.Rows[0]["gd_rate"];
                        str10 = rateByUserID.Rows[0]["zd_name"];
                        str11 = rateByUserID.Rows[0]["zd_rate"];
                        str12 = rateByUserID.Rows[0]["dl_name"];
                        str13 = rateByUserID.Rows[0]["dl_rate"];
                    }
                    if (str3.Equals("zd"))
                    {
                        str8 = rateByUserID.Rows[0]["gd_name"];
                        str9 = rateByUserID.Rows[0]["gd_rate"];
                        str10 = rateByUserID.Rows[0]["zd_name"];
                        str11 = rateByUserID.Rows[0]["zd_rate"];
                    }
                    if (str3.Equals("gd"))
                    {
                        str8 = rateByUserID.Rows[0]["gd_name"];
                        str9 = rateByUserID.Rows[0]["gd_rate"];
                    }
                    Dictionary<string, string> dictionary3 = new Dictionary<string, string>();
                    string str25 = _session.get_u_type();
                    if (str25 != null)
                    {
                        if (!(str25 == "zj"))
                        {
                            if (str25 == "fgs")
                            {
                                if (!string.IsNullOrEmpty(str6))
                                {
                                    if (_session.get_users_child_session() != null)
                                    {
                                        str6 = "-";
                                    }
                                    dictionary3.Add("fgs", string.Format("{0},{1}", str6, str7));
                                }
                                if (!string.IsNullOrEmpty(str8))
                                {
                                    dictionary3.Add("gd", string.Format("{0},{1}", str8, str9));
                                }
                                if (!string.IsNullOrEmpty(str10))
                                {
                                    dictionary3.Add("zd", string.Format("{0},{1}", str10, str11));
                                }
                                if (!string.IsNullOrEmpty(str12))
                                {
                                    dictionary3.Add("dl", string.Format("{0},{1}", str12, str13));
                                }
                            }
                            else if (str25 == "gd")
                            {
                                if (!string.IsNullOrEmpty(str8))
                                {
                                    if (_session.get_users_child_session() != null)
                                    {
                                        str8 = "-";
                                    }
                                    dictionary3.Add("gd", string.Format("{0},{1}", str8, str9));
                                }
                                if (!string.IsNullOrEmpty(str10))
                                {
                                    dictionary3.Add("zd", string.Format("{0},{1}", str10, str11));
                                }
                                if (!string.IsNullOrEmpty(str12))
                                {
                                    dictionary3.Add("dl", string.Format("{0},{1}", str12, str13));
                                }
                            }
                            else if (str25 == "zd")
                            {
                                if (!string.IsNullOrEmpty(str10))
                                {
                                    if (_session.get_users_child_session() != null)
                                    {
                                        str10 = "-";
                                    }
                                    dictionary3.Add("zd", string.Format("{0},{1}", str10, str11));
                                }
                                if (!string.IsNullOrEmpty(str12))
                                {
                                    dictionary3.Add("dl", string.Format("{0},{1}", str12, str13));
                                }
                            }
                            else if ((str25 == "dl") && !string.IsNullOrEmpty(str12))
                            {
                                if (_session.get_users_child_session() != null)
                                {
                                    str12 = "-";
                                }
                                dictionary3.Add("dl", string.Format("{0},{1}", str12, str13));
                            }
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(str4))
                            {
                                if (_session.get_users_child_session() != null)
                                {
                                    str4 = "-";
                                }
                                dictionary3.Add("zj", string.Format("{0},{1}", str4, str5));
                            }
                            if (!string.IsNullOrEmpty(str6))
                            {
                                dictionary3.Add("fgs", string.Format("{0},{1}", str6, str7));
                            }
                            if (!string.IsNullOrEmpty(str8))
                            {
                                dictionary3.Add("gd", string.Format("{0},{1}", str8, str9));
                            }
                            if (!string.IsNullOrEmpty(str10))
                            {
                                dictionary3.Add("zd", string.Format("{0},{1}", str10, str11));
                            }
                            if (!string.IsNullOrEmpty(str12))
                            {
                                dictionary3.Add("dl", string.Format("{0},{1}", str12, str13));
                            }
                        }
                    }
                    dictionary2.Add("six", dictionary3);
                }
                if (rowArray2.Length > 0)
                {
                    DataTable table3 = CallBLL.cz_rate_kc_bll.GetRateByUserID(str);
                    if (table3 == null)
                    {
                        return;
                    }
                    table3.Rows[0]["u_type"].ToString();
                    string str14 = table3.Rows[0]["su_type"].ToString();
                    string str15 = "";
                    string str16 = "";
                    string str17 = "";
                    string str18 = "";
                    string str19 = "";
                    string str20 = "";
                    string str21 = "";
                    string str22 = "";
                    string str23 = "";
                    string str24 = "";
                    str15 = _session.get_zjname();
                    str16 = table3.Rows[0]["zj_rate"];
                    str17 = table3.Rows[0]["fgs_name"];
                    str18 = table3.Rows[0]["fgs_rate"];
                    if (str14.Equals("dl"))
                    {
                        str19 = table3.Rows[0]["gd_name"];
                        str20 = table3.Rows[0]["gd_rate"];
                        str21 = table3.Rows[0]["zd_name"];
                        str22 = table3.Rows[0]["zd_rate"];
                        str23 = table3.Rows[0]["dl_name"];
                        str24 = table3.Rows[0]["dl_rate"];
                    }
                    if (str14.Equals("zd"))
                    {
                        str19 = table3.Rows[0]["gd_name"];
                        str20 = table3.Rows[0]["gd_rate"];
                        str21 = table3.Rows[0]["zd_name"];
                        str22 = table3.Rows[0]["zd_rate"];
                    }
                    if (str14.Equals("gd"))
                    {
                        str19 = table3.Rows[0]["gd_name"];
                        str20 = table3.Rows[0]["gd_rate"];
                    }
                    Dictionary<string, string> dictionary4 = new Dictionary<string, string>();
                    string str26 = _session.get_u_type();
                    if (str26 != null)
                    {
                        if (!(str26 == "zj"))
                        {
                            if (str26 == "fgs")
                            {
                                if (!string.IsNullOrEmpty(str17))
                                {
                                    if (_session.get_users_child_session() != null)
                                    {
                                        str17 = "-";
                                    }
                                    dictionary4.Add("fgs", string.Format("{0},{1}", str17, str18));
                                }
                                if (!string.IsNullOrEmpty(str19))
                                {
                                    dictionary4.Add("gd", string.Format("{0},{1}", str19, str20));
                                }
                                if (!string.IsNullOrEmpty(str21))
                                {
                                    dictionary4.Add("zd", string.Format("{0},{1}", str21, str22));
                                }
                                if (!string.IsNullOrEmpty(str23))
                                {
                                    dictionary4.Add("dl", string.Format("{0},{1}", str23, str24));
                                }
                            }
                            else if (str26 == "gd")
                            {
                                if (!string.IsNullOrEmpty(str19))
                                {
                                    if (_session.get_users_child_session() != null)
                                    {
                                        str19 = "-";
                                    }
                                    dictionary4.Add("gd", string.Format("{0},{1}", str19, str20));
                                }
                                if (!string.IsNullOrEmpty(str21))
                                {
                                    dictionary4.Add("zd", string.Format("{0},{1}", str21, str22));
                                }
                                if (!string.IsNullOrEmpty(str23))
                                {
                                    dictionary4.Add("dl", string.Format("{0},{1}", str23, str24));
                                }
                            }
                            else if (str26 == "zd")
                            {
                                if (!string.IsNullOrEmpty(str21))
                                {
                                    if (_session.get_users_child_session() != null)
                                    {
                                        str21 = "-";
                                    }
                                    dictionary4.Add("zd", string.Format("{0},{1}", str21, str22));
                                }
                                if (!string.IsNullOrEmpty(str23))
                                {
                                    dictionary4.Add("dl", string.Format("{0},{1}", str23, str24));
                                }
                            }
                            else if ((str26 == "dl") && !string.IsNullOrEmpty(str23))
                            {
                                if (_session.get_users_child_session() != null)
                                {
                                    str23 = "-";
                                }
                                dictionary4.Add("dl", string.Format("{0},{1}", str23, str24));
                            }
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(str15))
                            {
                                if (_session.get_users_child_session() != null)
                                {
                                    str15 = "-";
                                }
                                dictionary4.Add("zj", string.Format("{0},{1}", str15, str16));
                            }
                            if (!string.IsNullOrEmpty(str17))
                            {
                                dictionary4.Add("fgs", string.Format("{0},{1}", str17, str18));
                            }
                            if (!string.IsNullOrEmpty(str19))
                            {
                                dictionary4.Add("gd", string.Format("{0},{1}", str19, str20));
                            }
                            if (!string.IsNullOrEmpty(str21))
                            {
                                dictionary4.Add("zd", string.Format("{0},{1}", str21, str22));
                            }
                            if (!string.IsNullOrEmpty(str23))
                            {
                                dictionary4.Add("dl", string.Format("{0},{1}", str23, str24));
                            }
                        }
                    }
                    dictionary2.Add("kc", dictionary4);
                }
                dictionary.Add("tips", dictionary2);
                result.set_success(200);
                result.set_data(dictionary);
                strResult = JsonHandle.ObjectToJson(result);
            }
        }

        public void GetPhaseByLottery(HttpContext context, ref string strResult)
        {
            ReturnResult result = new ReturnResult();
            result.set_success(200);
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            if (context.Session["user_name"] == null)
            {
                result.set_success(300);
                dictionary.Add("type", "get_phasebylottery");
                result.set_data(dictionary);
                strResult = JsonHandle.ObjectToJson(result);
                context.Response.End();
            }
            else
            {
                int num = Convert.ToInt32(LSRequest.qq("lid"));
                string str2 = DateTime.Now.AddHours((double) -int.Parse(base.get_BetTime_KC())).ToString("yyyy-MM-dd");
                DataTable phaseByQueryDate = null;
                DataTable play = null;
                switch (num)
                {
                    case 0:
                        phaseByQueryDate = CallBLL.cz_phase_kl10_bll.GetPhaseByQueryDate(str2);
                        play = CallBLL.cz_play_kl10_bll.GetPlay();
                        break;

                    case 1:
                        phaseByQueryDate = CallBLL.cz_phase_cqsc_bll.GetPhaseByQueryDate(str2);
                        play = CallBLL.cz_play_cqsc_bll.GetPlay();
                        break;

                    case 2:
                        phaseByQueryDate = CallBLL.cz_phase_pk10_bll.GetPhaseByQueryDate(str2);
                        play = CallBLL.cz_play_pk10_bll.GetPlay();
                        break;

                    case 3:
                        phaseByQueryDate = CallBLL.cz_phase_xync_bll.GetPhaseByQueryDate(str2);
                        play = CallBLL.cz_play_xync_bll.GetPlay();
                        break;

                    case 4:
                        phaseByQueryDate = CallBLL.cz_phase_jsk3_bll.GetPhaseByQueryDate(str2);
                        play = CallBLL.cz_play_jsk3_bll.GetPlay();
                        break;

                    case 5:
                        phaseByQueryDate = CallBLL.cz_phase_kl8_bll.GetPhaseByQueryDate(str2);
                        play = CallBLL.cz_play_kl8_bll.GetPlay();
                        break;

                    case 6:
                        phaseByQueryDate = CallBLL.cz_phase_k8sc_bll.GetPhaseByQueryDate(str2);
                        play = CallBLL.cz_play_k8sc_bll.GetPlay();
                        break;

                    case 7:
                        phaseByQueryDate = CallBLL.cz_phase_pcdd_bll.GetPhaseByQueryDate(str2);
                        play = CallBLL.cz_play_pcdd_bll.GetPlay();
                        break;

                    case 8:
                        phaseByQueryDate = CallBLL.cz_phase_pkbjl_bll.GetPhaseByQueryDate(str2);
                        play = CallBLL.cz_play_pkbjl_bll.GetPlay();
                        break;

                    case 9:
                        phaseByQueryDate = CallBLL.cz_phase_xyft5_bll.GetPhaseByQueryDate(str2);
                        play = CallBLL.cz_play_xyft5_bll.GetPlay();
                        break;

                    case 10:
                        phaseByQueryDate = CallBLL.cz_phase_jscar_bll.GetPhaseByQueryDate(str2);
                        play = CallBLL.cz_play_jscar_bll.GetPlay();
                        break;

                    case 11:
                        phaseByQueryDate = CallBLL.cz_phase_speed5_bll.GetPhaseByQueryDate(str2);
                        play = CallBLL.cz_play_speed5_bll.GetPlay();
                        break;

                    case 12:
                        phaseByQueryDate = CallBLL.cz_phase_jspk10_bll.GetPhaseByQueryDate(str2);
                        play = CallBLL.cz_play_jspk10_bll.GetPlay();
                        break;

                    case 13:
                        phaseByQueryDate = CallBLL.cz_phase_jscqsc_bll.GetPhaseByQueryDate(str2);
                        play = CallBLL.cz_play_jscqsc_bll.GetPlay();
                        break;

                    case 14:
                        phaseByQueryDate = CallBLL.cz_phase_jssfc_bll.GetPhaseByQueryDate(str2);
                        play = CallBLL.cz_play_jssfc_bll.GetPlay();
                        break;

                    case 15:
                        phaseByQueryDate = CallBLL.cz_phase_jsft2_bll.GetPhaseByQueryDate(str2);
                        play = CallBLL.cz_play_jsft2_bll.GetPlay();
                        break;

                    case 0x10:
                        phaseByQueryDate = CallBLL.cz_phase_car168_bll.GetPhaseByQueryDate(str2);
                        play = CallBLL.cz_play_car168_bll.GetPlay();
                        break;

                    case 0x11:
                        phaseByQueryDate = CallBLL.cz_phase_ssc168_bll.GetPhaseByQueryDate(str2);
                        play = CallBLL.cz_play_ssc168_bll.GetPlay();
                        break;

                    case 0x12:
                        phaseByQueryDate = CallBLL.cz_phase_vrcar_bll.GetPhaseByQueryDate(str2);
                        play = CallBLL.cz_play_vrcar_bll.GetPlay();
                        break;

                    case 0x13:
                        phaseByQueryDate = CallBLL.cz_phase_vrssc_bll.GetPhaseByQueryDate(str2);
                        play = CallBLL.cz_play_vrssc_bll.GetPlay();
                        break;

                    case 20:
                        phaseByQueryDate = CallBLL.cz_phase_xyftoa_bll.GetPhaseByQueryDate(str2);
                        play = CallBLL.cz_play_xyftoa_bll.GetPlay();
                        break;

                    case 0x15:
                        phaseByQueryDate = CallBLL.cz_phase_xyftsg_bll.GetPhaseByQueryDate(str2);
                        play = CallBLL.cz_play_xyftsg_bll.GetPlay();
                        break;

                    case 0x16:
                        phaseByQueryDate = CallBLL.cz_phase_happycar_bll.GetPhaseByQueryDate(str2);
                        play = CallBLL.cz_play_happycar_bll.GetPlay();
                        break;

                    case 100:
                        phaseByQueryDate = CallBLL.cz_phase_six_bll.GetCurrentPhase("20");
                        if (!FileCacheHelper.get_IsShowLM_B())
                        {
                            play = CallBLL.cz_play_six_bll.GetPlay("91060,91061,91062,91063,91064,91065");
                        }
                        else
                        {
                            play = CallBLL.cz_play_six_bll.GetPlay();
                        }
                        break;
                }
                dictionary.Add("type", "get_phasebylottery");
                StringBuilder builder = new StringBuilder();
                StringBuilder builder2 = new StringBuilder();
                if (phaseByQueryDate != null)
                {
                    if (num.Equals(8))
                    {
                        Dictionary<string, string> source = new Dictionary<string, string>();
                        for (int i = 0; i < phaseByQueryDate.Rows.Count; i++)
                        {
                            string str3 = phaseByQueryDate.Rows[i]["p_id"].ToString();
                            string key = phaseByQueryDate.Rows[i]["phase"].ToString();
                            if (!source.ContainsKey(key))
                            {
                                source.Add(key, str3);
                            }
                        }
                        for (int j = 0; j < source.Count; j++)
                        {
                            if (j == 0)
                            {
                                builder.Append(source.ElementAt<KeyValuePair<string, string>>(j).Value + "," + source.ElementAt<KeyValuePair<string, string>>(j).Key + " 期");
                            }
                            else
                            {
                                builder.Append("|" + source.ElementAt<KeyValuePair<string, string>>(j).Value + "," + source.ElementAt<KeyValuePair<string, string>>(j).Key + " 期");
                            }
                        }
                    }
                    else
                    {
                        for (int k = 0; k < phaseByQueryDate.Rows.Count; k++)
                        {
                            if (k == 0)
                            {
                                builder.Append(phaseByQueryDate.Rows[k]["p_id"].ToString().Trim() + "," + phaseByQueryDate.Rows[k]["phase"].ToString().Trim() + " 期");
                            }
                            else
                            {
                                builder.Append("|" + phaseByQueryDate.Rows[k]["p_id"].ToString().Trim() + "," + phaseByQueryDate.Rows[k]["phase"].ToString().Trim() + " 期");
                            }
                        }
                    }
                    dictionary.Add("phaseoption", builder.ToString());
                }
                if (play != null)
                {
                    for (int m = 0; m < play.Rows.Count; m++)
                    {
                        if (m == 0)
                        {
                            builder2.Append(play.Rows[m]["play_id"].ToString().Trim() + "," + play.Rows[m]["play_name"].ToString().Trim());
                        }
                        else
                        {
                            builder2.Append("|" + play.Rows[m]["play_id"].ToString().Trim() + "," + play.Rows[m]["play_name"].ToString().Trim());
                        }
                    }
                    dictionary.Add("playoption", builder2.ToString());
                }
                if (num.Equals(8))
                {
                    dictionary.Add("tabletype", string.Format("p1,第1桌|p2,第2桌|p3,第3桌|p4,第4桌|p5,第5桌", new object[0]));
                    dictionary.Add("playtype", string.Format("1,一般|0,免傭", new object[0]));
                }
                result.set_data(dictionary);
                strResult = JsonHandle.ObjectToJson(result);
            }
        }

        public void GetPlayByLottery(HttpContext context, ref string strResult)
        {
            ReturnResult result = new ReturnResult();
            result.set_success(200);
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            if (context.Session["user_name"] == null)
            {
                result.set_success(300);
                dictionary.Add("type", "get_playbylottery");
                result.set_data(dictionary);
                strResult = JsonHandle.ObjectToJson(result);
                context.Response.End();
            }
            else if (!context.Session["user_type"].ToString().Equals("zj"))
            {
                result.set_success(400);
                dictionary.Add("type", "get_playbylottery");
                result.set_data(dictionary);
                result.set_tipinfo(PageBase.GetMessageByCache("u100014", "MessageHint"));
                strResult = JsonHandle.ObjectToJson(result);
                context.Response.End();
            }
            else
            {
                int num = Convert.ToInt32(LSRequest.qq("lid"));
                DataTable play = null;
                switch (num)
                {
                    case 0:
                        play = CallBLL.cz_play_kl10_bll.GetPlay();
                        break;

                    case 1:
                        play = CallBLL.cz_play_cqsc_bll.GetPlay();
                        break;

                    case 2:
                        play = CallBLL.cz_play_pk10_bll.GetPlay();
                        break;

                    case 3:
                        play = CallBLL.cz_play_xync_bll.GetPlay();
                        break;

                    case 4:
                        play = CallBLL.cz_play_jsk3_bll.GetPlay();
                        break;

                    case 5:
                        play = CallBLL.cz_play_kl8_bll.GetPlay();
                        break;

                    case 6:
                        play = CallBLL.cz_play_k8sc_bll.GetPlay();
                        break;

                    case 7:
                        play = CallBLL.cz_play_pcdd_bll.GetPlay();
                        break;

                    case 8:
                        play = CallBLL.cz_play_pkbjl_bll.GetPlay();
                        break;

                    case 9:
                        play = CallBLL.cz_play_xyft5_bll.GetPlay();
                        break;

                    case 10:
                        play = CallBLL.cz_play_jscar_bll.GetPlay();
                        break;

                    case 11:
                        play = CallBLL.cz_play_speed5_bll.GetPlay();
                        break;

                    case 12:
                        play = CallBLL.cz_play_jspk10_bll.GetPlay();
                        break;

                    case 13:
                        play = CallBLL.cz_play_jscqsc_bll.GetPlay();
                        break;

                    case 14:
                        play = CallBLL.cz_play_jssfc_bll.GetPlay();
                        break;

                    case 15:
                        play = CallBLL.cz_play_jsft2_bll.GetPlay();
                        break;

                    case 0x10:
                        play = CallBLL.cz_play_car168_bll.GetPlay();
                        break;

                    case 0x11:
                        play = CallBLL.cz_play_ssc168_bll.GetPlay();
                        break;

                    case 0x12:
                        play = CallBLL.cz_play_vrcar_bll.GetPlay();
                        break;

                    case 0x13:
                        play = CallBLL.cz_play_vrssc_bll.GetPlay();
                        break;

                    case 20:
                        play = CallBLL.cz_play_xyftoa_bll.GetPlay();
                        break;

                    case 0x15:
                        play = CallBLL.cz_play_xyftsg_bll.GetPlay();
                        break;

                    case 0x16:
                        play = CallBLL.cz_play_happycar_bll.GetPlay();
                        break;

                    case 100:
                        if (!FileCacheHelper.get_IsShowLM_B())
                        {
                            play = CallBLL.cz_play_six_bll.GetPlay("91060,91061,91062,91063,91064,91065");
                        }
                        else
                        {
                            play = CallBLL.cz_play_six_bll.GetPlay();
                        }
                        break;
                }
                StringBuilder builder = new StringBuilder();
                if (play != null)
                {
                    for (int i = 0; i < play.Rows.Count; i++)
                    {
                        if (i == 0)
                        {
                            builder.Append(play.Rows[i]["play_id"].ToString().Trim() + "," + play.Rows[i]["play_name"].ToString().Trim());
                        }
                        else
                        {
                            builder.Append("|" + play.Rows[i]["play_id"].ToString().Trim() + "," + play.Rows[i]["play_name"].ToString().Trim());
                        }
                    }
                    dictionary.Add("type", "get_playbylottery");
                    dictionary.Add("playoption", builder.ToString());
                    result.set_data(dictionary);
                    strResult = JsonHandle.ObjectToJson(result);
                }
            }
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
                int num = Convert.ToInt32(LSRequest.qq("lid"));
                DataTable table = null;
                switch (num)
                {
                    case 0:
                        table = CallBLL.cz_phase_kl10_bll.IsPhaseClose();
                        break;

                    case 1:
                        table = CallBLL.cz_phase_cqsc_bll.IsPhaseClose();
                        break;

                    case 2:
                        table = CallBLL.cz_phase_pk10_bll.IsPhaseClose();
                        break;

                    case 3:
                        table = CallBLL.cz_phase_xync_bll.IsPhaseClose();
                        break;

                    case 4:
                        table = CallBLL.cz_phase_jsk3_bll.IsPhaseClose();
                        break;

                    case 5:
                        table = CallBLL.cz_phase_kl8_bll.IsPhaseClose();
                        break;

                    case 6:
                        table = CallBLL.cz_phase_k8sc_bll.IsPhaseClose();
                        break;

                    case 7:
                        table = CallBLL.cz_phase_pcdd_bll.IsPhaseClose();
                        break;

                    case 8:
                        table = CallBLL.cz_phase_pkbjl_bll.IsPhaseClose();
                        break;

                    case 9:
                        table = CallBLL.cz_phase_xyft5_bll.IsPhaseClose();
                        break;

                    case 10:
                        table = CallBLL.cz_phase_jscar_bll.IsPhaseClose();
                        break;

                    case 11:
                        table = CallBLL.cz_phase_speed5_bll.IsPhaseClose();
                        break;

                    case 12:
                        table = CallBLL.cz_phase_jspk10_bll.IsPhaseClose();
                        break;

                    case 13:
                        table = CallBLL.cz_phase_jscqsc_bll.IsPhaseClose();
                        break;

                    case 14:
                        table = CallBLL.cz_phase_jssfc_bll.IsPhaseClose();
                        break;

                    case 15:
                        table = CallBLL.cz_phase_jsft2_bll.IsPhaseClose();
                        break;

                    case 0x10:
                        table = CallBLL.cz_phase_car168_bll.IsPhaseClose();
                        break;

                    case 0x11:
                        table = CallBLL.cz_phase_ssc168_bll.IsPhaseClose();
                        break;

                    case 0x12:
                        table = CallBLL.cz_phase_vrcar_bll.IsPhaseClose();
                        break;

                    case 0x13:
                        table = CallBLL.cz_phase_vrssc_bll.IsPhaseClose();
                        break;

                    case 20:
                        table = CallBLL.cz_phase_xyftoa_bll.IsPhaseClose();
                        break;

                    case 0x15:
                        table = CallBLL.cz_phase_xyftsg_bll.IsPhaseClose();
                        break;

                    case 0x16:
                        table = CallBLL.cz_phase_happycar_bll.IsPhaseClose();
                        break;
                }
                if (table.Rows[0]["isopen"].ToString().Equals("0"))
                {
                    dictionary.Add("type", "isopenlottery");
                    Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
                    dictionary2.Add("isopen", "-1");
                    dictionary.Add("isopenvalue", dictionary2);
                    result.set_data(dictionary);
                    result.set_success(200);
                    strResult = JsonHandle.ObjectToJson(result);
                }
                else
                {
                    dictionary.Add("type", "isopenlottery");
                    Dictionary<string, object> dictionary3 = new Dictionary<string, object>();
                    dictionary3.Add("isopen", "1");
                    dictionary.Add("isopenvalue", dictionary3);
                    result.set_data(dictionary);
                    result.set_success(200);
                    strResult = JsonHandle.ObjectToJson(result);
                }
            }
        }

        public override void ProcessRequest(HttpContext context)
        {
            base.checkLoginByHandler(0);
            string str = LSRequest.qq("action");
            string strResult = "";
            if (!string.IsNullOrEmpty(str))
            {
                switch (str)
                {
                    case "get_ad":
                        this.get_ad(context, ref strResult);
                        break;

                    case "get_newbetlist":
                        this.get_newbetlist(context, ref strResult);
                        break;

                    case "get_openlottery":
                        this.IsOpenLottery(context, ref strResult);
                        break;

                    case "get_playbylottery":
                        this.GetPlayByLottery(context, ref strResult);
                        break;

                    case "get_phasebylottery":
                        this.GetPhaseByLottery(context, ref strResult);
                        break;

                    case "set_skin":
                        this.set_skin(context, ref strResult);
                        break;

                    case "get_six_date":
                        this.get_six_date(context, ref strResult);
                        break;

                    case "get_user_rate":
                        this.get_user_rate(context, ref strResult);
                        break;

                    case "get_currentphase":
                        this.get_currentphase(context, ref strResult);
                        break;

                    case "get_gamehall":
                        this.get_gamehall(context, ref strResult);
                        break;

                    case "get_reptcookies":
                        this.get_reportcookies(context, ref strResult);
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
                agent_userinfo_session _session = new agent_userinfo_session();
                string str2 = context.Session["user_name"].ToString();
                _session = context.Session[str2 + "lottery_session_user_info"] as agent_userinfo_session;
                string str3 = _session.get_u_id().ToString();
                int num = -1;
                if (context.Session["child_user_name"] != null)
                {
                    str3 = _session.get_users_child_session().get_u_id().ToString();
                    num = CallBLL.cz_users_child_bll.UpdateSkin(str3, str);
                }
                else
                {
                    num = CallBLL.cz_users_bll.UpdateSkin(str3, str);
                }
                if (num > 0)
                {
                    result.set_success(200);
                    _session.set_u_skin(str);
                    context.Session[str2 + "lottery_session_user_info"] = _session;
                }
                else
                {
                    result.set_success(400);
                    result.set_tipinfo(PageBase.GetMessageByCache("u100059", "MessageHint"));
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

