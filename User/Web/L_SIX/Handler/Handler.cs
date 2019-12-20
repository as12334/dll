namespace User.Web.L_SIX.Handler
{
    using LotterySystem.Common;
    using LotterySystem.DBUtility;
    using LotterySystem.Model;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Text;
    using System.Web;
    using User.Web.Handler;
    using User.Web.WebBase;

    public class Handler : BaseHandler
    {
        private static string comb(int[] a, int n, int m, int M)
        {
            string str = "";
            for (int i = n; i >= m; i--)
            {
                a[m - 1] = i - 1;
                if (m > 1)
                {
                    str = str + comb(a, i - 1, m - 1, M);
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

        public void do_default(HttpContext context, ref string strResult)
        {
            ReturnResult result = new ReturnResult();
            result.set_success(400);
            result.set_tipinfo("action param error");
            strResult = JsonHandle.ObjectToJson(result);
        }

        public void do_put_money(HttpContext context, ref string strResult)
        {
            double num3;
            DateTime time;
            DateTime time2;
            string[] strArray7;
            string[] strArray8;
            string str46;
            string[] strArray9;
            string str47;
            string[] strArray10;
            string[] strArray11;
            int num27;
            DataTable table7;
            int num28;
            string str54;
            string str55;
            string str56;
            string str58;
            string str59;
            DataRow[] rowArray2;
            string str67;
            double playDrawbackValue;
            cz_bet_six _six2;
            int num35;
            cz_system_set_six systemSet;
            double num43;
            double num44;
            double num45;
            double num46;
            double num47;
            double num48;
            cz_system_log _log;
            string str91;
            cz_jp_odds _odds;
            cz_system_log _log2;
            string str94;
            cz_jp_odds _odds2;
            DataTable fgsWTTable;
            int num56;
            string str109;
            double num58;
            if (context.Session["user_name"] == null)
            {
                context.Response.End();
                return;
            }
            string str = context.Session["user_name"].ToString();
            ReturnResult result = new ReturnResult();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("type", "put_money");
            cz_userinfo_session getUserModelInfo = base.GetUserModelInfo;
            if (FileCacheHelper.get_RedisStatOnline().Equals(1))
            {
                base.CheckIsOut(getUserModelInfo.get_u_name());
                base.stat_online_redis(getUserModelInfo.get_u_name(), "hy");
            }
            else if (FileCacheHelper.get_RedisStatOnline().Equals(2))
            {
                base.CheckIsOutStack(getUserModelInfo.get_u_name());
                base.stat_online_redisStack(getUserModelInfo.get_u_name(), "hy");
            }
            else
            {
                MemberPageBase.stat_online_bet(getUserModelInfo.get_u_name(), "hy");
            }
            if (!base.BetCreditLock(str))
            {
                result.set_success(400);
                result.set_tipinfo("系統繁忙，請稍後！");
                dictionary.Add("JeuValidate", base.get_JeuValidate());
                result.set_data(dictionary);
                strResult = JsonHandle.ObjectToJson(result);
                return;
            }
            user_six_rate rate = base.GetUserRate_six(getUserModelInfo.get_zjname());
            double num = 0.0;
            double num2 = 0.0;
            if (rate.get_zcyg().Equals("1"))
            {
                num3 = (((100.0 - double.Parse(rate.get_fgszc())) - double.Parse(rate.get_gdzc())) - double.Parse(rate.get_zdzc())) - double.Parse(rate.get_dlzc());
                num = num3;
                num2 = double.Parse(rate.get_fgszc());
            }
            else
            {
                num3 = (((100.0 - double.Parse(rate.get_zjzc())) - double.Parse(rate.get_gdzc())) - double.Parse(rate.get_zdzc())) - double.Parse(rate.get_dlzc());
                num2 = num3;
                num = double.Parse(rate.get_zjzc());
            }
            int num4 = 1;
            if (getUserModelInfo.get_su_type().ToString().Trim() != "dl")
            {
                num4 = -1;
            }
            DataTable table = null;
            DataSet set = CallBLL.cz_users_bll.AccountIsDisabled(rate.get_fgsname(), rate.get_gdname(), rate.get_zdname(), rate.get_dlname(), getUserModelInfo.get_u_name());
            if (set != null)
            {
                table = set.Tables[0];
                if ((table != null) && (table.Rows.Count > 0))
                {
                    if (table.Rows[0]["a_state"].ToString().Trim() == "1")
                    {
                        result.set_tipinfo("會員或上級已經被凍結,請與上級聯系！");
                    }
                    else
                    {
                        result.set_tipinfo("會員或上級已經被停用,請與上級聯系！");
                    }
                    base.DeleteCreditLock(str);
                    result.set_success(400);
                    string str4 = base.get_JeuValidate();
                    dictionary.Add("JeuValidate", str4);
                    result.set_data(dictionary);
                    strResult = JsonHandle.ObjectToJson(result);
                    return;
                }
            }
            int num5 = 100;
            string str5 = "";
            string s = "";
            string str7 = "";
            string str8 = LSRequest.qq("phaseid");
            string str9 = LSRequest.qq("playpage");
            str5 = LSRequest.qq("oddsid");
            s = LSRequest.qq("uPI_P");
            str7 = LSRequest.qq("uPI_M");
            string str10 = LSRequest.qq("uPI_TM");
            string[] strArray = LSRequest.qq("i_index").Split(new char[] { ',' });
            string str11 = LSRequest.qq("JeuValidate");
            string[] source = str5.Split(new char[] { ',' });
            string[] strArray3 = s.Split(new char[] { ',' });
            string[] strArray4 = str7.Split(new char[] { ',' });
            if (source.Distinct<string>().ToList<string>().Count != source.Length)
            {
                base.DeleteCreditLock(str);
                context.Response.End();
            }
            if (source.Length > FileCacheHelper.get_GetSIXMaxGroup())
            {
                base.DeleteCreditLock(str);
                result.set_success(400);
                result.set_tipinfo(string.Format("您選擇的號碼超過了最大{0}組，請重新選擇號碼!", FileCacheHelper.get_GetSIXMaxGroup()));
                dictionary.Add("JeuValidate", base.get_JeuValidate());
                result.set_data(dictionary);
                strResult = JsonHandle.ObjectToJson(result);
                return;
            }
            if (((context.Session["JeuValidate"] == null) || (context.Session["JeuValidate"].ToString().Trim() != str11)) || (context.Session["JeuValidate"].ToString().Trim() == ""))
            {
                base.DeleteCreditLock(str);
                result.set_tipinfo("下注規則有誤,請重新下注,謝謝合作!");
                result.set_success(400);
                dictionary.Add("JeuValidate", base.get_JeuValidate());
                result.set_data(dictionary);
                strResult = JsonHandle.ObjectToJson(result);
                return;
            }
            context.Session["JeuValidate"] = "";
            int index = 0;
        Label_068B:;
            if (index < str7.Split(new char[] { ',' }).Length)
            {
                if (!base.IsNumber(str7.Split(new char[] { ',' })[index]))
                {
                    base.DeleteCreditLock(str);
                    result.set_tipinfo("下注金額有誤！");
                    result.set_success(500);
                    dictionary.Add("index", strArray[index]);
                    dictionary.Add("JeuValidate", base.get_JeuValidate());
                    result.set_data(dictionary);
                    strResult = JsonHandle.ObjectToJson(result);
                    return;
                }
                index++;
                goto Label_068B;
            }
            if (getUserModelInfo.get_begin_six().Trim() != "yes")
            {
                getUserModelInfo = base.GetRateByUserObject(1, rate);
            }
            string str12 = "";
            string str13 = "";
            cz_phase_six currentPhase = CallBLL.cz_phase_six_bll.GetCurrentPhase();
            if (currentPhase != null)
            {
                if (currentPhase.get_is_closed() == 1)
                {
                    base.DeleteCreditLock(str);
                    result.set_tipinfo(string.Format("奖期[{0}]已封盘,停止下注！", currentPhase.get_phase().ToString()));
                    result.set_success(400);
                    dictionary.Add("JeuValidate", base.get_JeuValidate());
                    result.set_data(dictionary);
                    strResult = JsonHandle.ObjectToJson(result);
                    return;
                }
                str12 = currentPhase.get_phase().ToString();
                num56 = currentPhase.get_p_id();
                str13 = num56.ToString();
                time = Convert.ToDateTime(currentPhase.get_stop_date());
                time2 = Convert.ToDateTime(currentPhase.get_sn_stop_date());
            }
            else
            {
                base.DeleteCreditLock(str);
                result.set_tipinfo(string.Format("该奖期已停止下注！", new object[0]));
                result.set_success(400);
                dictionary.Add("JeuValidate", base.get_JeuValidate());
                result.set_data(dictionary);
                strResult = JsonHandle.ObjectToJson(result);
                return;
            }
            DataTable playIDByOddsIds = CallBLL.cz_odds_six_bll.GetPlayIDByOddsIds(str5);
            DateTime now = DateTime.Now;
            DateTime time4 = Convert.ToDateTime(currentPhase.get_stop_date());
            DateTime time5 = Convert.ToDateTime(currentPhase.get_sn_stop_date());
            if (((((playIDByOddsIds.Rows[0]["play_id"].ToString().Trim() == "91001") || (playIDByOddsIds.Rows[0]["play_id"].ToString().Trim() == "91002")) || ((playIDByOddsIds.Rows[0]["play_id"].ToString().Trim() == "91004") || (playIDByOddsIds.Rows[0]["play_id"].ToString().Trim() == "91005"))) || (playIDByOddsIds.Rows[0]["play_id"].ToString().Trim() == "91007")) || (playIDByOddsIds.Rows[0]["play_id"].ToString().Trim() == "91038"))
            {
                if (time5 < now)
                {
                    base.DeleteCreditLock(str);
                    result.set_tipinfo(string.Format("已經截止下註！", new object[0]));
                    result.set_success(400);
                    dictionary.Add("JeuValidate", base.get_JeuValidate());
                    result.set_data(dictionary);
                    strResult = JsonHandle.ObjectToJson(result);
                    return;
                }
            }
            else if (time4 < now)
            {
                base.DeleteCreditLock(str);
                result.set_tipinfo(string.Format("已經截止下註！", new object[0]));
                result.set_success(400);
                dictionary.Add("JeuValidate", base.get_JeuValidate());
                result.set_data(dictionary);
                strResult = JsonHandle.ObjectToJson(result);
                return;
            }
            string str14 = "";
            string str15 = "";
            double num8 = 0.0;
            DataTable table3 = CallBLL.cz_users_bll.GetUserRate(str, 1).Tables[0];
            str14 = table3.Rows[0]["six_kind"].ToString().Trim().ToUpper();
            str15 = table3.Rows[0]["su_type"].ToString().Trim();
            num8 = double.Parse(table3.Rows[0]["six_usable_credit"].ToString().Trim());
            getUserModelInfo.set_six_kind(str14.ToUpper());
            string str16 = "0";
            string str17 = "0";
            string str18 = "0";
            string str19 = "0";
            string str20 = "0";
            string str21 = "0";
            double num9 = 0.0;
            double num10 = 0.0;
            double num11 = 0.0;
            double num12 = 0.0;
            double num13 = 0.0;
            double num14 = 0.0;
            string str22 = "";
            string str23 = "";
            string diff = "";
            string str25 = "";
            string str26 = "";
            string str27 = "0";
            string str28 = "0";
            DataTable plDT = CallBLL.cz_odds_six_bll.GetPlayOddsByID(str5).Tables[0];
            if (plDT.Rows.Count == 0)
            {
                base.DeleteCreditLock(str);
                result.set_success(400);
                result.set_tipinfo("賠率ID設置错误!");
                dictionary.Add("JeuValidate", base.get_JeuValidate());
                result.set_data(dictionary);
                strResult = JsonHandle.ObjectToJson(result);
                return;
            }
            DataTable table5 = CallBLL.cz_bet_six_bll.GetSinglePhaseByBet(str12, str, num5.ToString(), str5).Tables[0];
            for (int i = 0; i < source.Length; i++)
            {
                bool flag2 = false;
                for (int j = 0; j < plDT.Rows.Count; j++)
                {
                    if (source[i].Equals(plDT.Rows[j]["odds_id"].ToString()))
                    {
                        flag2 = true;
                        break;
                    }
                }
                if (!flag2)
                {
                    base.DeleteCreditLock(str);
                    result.set_success(400);
                    result.set_tipinfo("系统错误!");
                    dictionary.Add("JeuValidate", base.get_JeuValidate());
                    result.set_data(dictionary);
                    strResult = JsonHandle.ObjectToJson(result);
                    return;
                }
            }
            double num17 = 0.0;
            string str29 = "";
            List<string> list = new List<string>();
            List<string> list2 = new List<string>();
            int num18 = 0;
            foreach (string str30 in source)
            {
                num9 = 0.0;
                foreach (DataRow row in plDT.Rows)
                {
                    if (row["odds_id"].ToString().Trim() == str30.Trim())
                    {
                        str29 = row["is_open"].ToString().Trim();
                        str23 = row["current_odds"].ToString().Trim();
                        diff = row[str14 + "_diff"].ToString().Trim();
                        str22 = row["play_id"].ToString().Trim();
                        str25 = row["play_name"].ToString().Trim();
                        str26 = row["put_amount"].ToString().Trim();
                        str27 = row["isLM"].ToString().Trim();
                        str28 = row["isDoubleOdds"].ToString().Trim();
                        num14 = double.Parse(row["allow_min_amount"].ToString().Trim());
                        num11 = double.Parse(row["allow_max_amount"].ToString().Trim());
                        num13 = double.Parse(row["allow_max_put_amount"].ToString().Trim());
                        break;
                    }
                }
                if (table5.Rows.Count > 0)
                {
                    foreach (DataRow row2 in table5.Rows)
                    {
                        if (row2["odds_id"].ToString().Trim() == str30.Trim())
                        {
                            num9 = double.Parse(row2["sumbet"].ToString().Trim());
                            break;
                        }
                    }
                }
                DataSet drawback = null;
                if (FileCacheHelper.get_GetSixPutMoneyCache() == "1")
                {
                    if (CacheHelper.GetCache("six_drawback_FileCacheKey" + str22 + HttpContext.Current.Session["user_name"].ToString()) == null)
                    {
                        drawback = CallBLL.cz_drawback_six_bll.GetDrawback(str22, str);
                    }
                    else
                    {
                        drawback = base.GetUserDrawback_six(rate, getUserModelInfo.get_six_kind(), str22);
                    }
                }
                else if (CacheHelper.GetCache("six_drawback_FileCacheKey" + HttpContext.Current.Session["user_name"].ToString()) == null)
                {
                    drawback = CallBLL.cz_drawback_six_bll.GetDrawback(str22, str);
                }
                else
                {
                    drawback = base.GetUserDrawback_six(rate, getUserModelInfo.get_six_kind());
                }
                DataRow[] rowArray = drawback.Tables[0].Select(string.Format(" play_id={0} and u_name='{1}' ", str22, str));
                num10 = double.Parse(rowArray[0]["single_max_amount"].ToString().Trim());
                double num19 = double.Parse(rowArray[0]["single_min_amount"].ToString().Trim());
                num12 = double.Parse(rowArray[0]["single_phase_amount"].ToString().Trim());
                if (num10 > num11)
                {
                    num10 = num11;
                }
                if (num12 > num13)
                {
                    num12 = num13;
                }
                if (num19 > num14)
                {
                    num14 = num19;
                }
                if (str29 != "1")
                {
                    base.DeleteCreditLock(str);
                    result.set_tipinfo(string.Format("{0}<{1}>已經停止投注！", str25, str26));
                    result.set_success(400);
                    dictionary.Add("JeuValidate", base.get_JeuValidate());
                    result.set_data(dictionary);
                    strResult = JsonHandle.ObjectToJson(result);
                    return;
                }
                if (str28.Equals("0"))
                {
                    string pl = "";
                    base.GetOdds_SIX(str30, str23, diff, ref pl);
                    num17 = double.Parse(pl);
                    if (!(double.Parse(strArray3[num18]) == double.Parse(num17.ToString())))
                    {
                        list.Add(num18.ToString());
                        list2.Add(num17.ToString());
                    }
                }
                if (double.Parse(strArray4[num18]) > num10)
                {
                    base.DeleteCreditLock(str);
                    result.set_tipinfo(string.Format("下注金額超出單注最大金額！", new object[0]));
                    result.set_success(500);
                    dictionary.Add("JeuValidate", base.get_JeuValidate());
                    result.set_data(dictionary);
                    strResult = JsonHandle.ObjectToJson(result);
                    return;
                }
                if (double.Parse(strArray4[num18]) > num8)
                {
                    base.DeleteCreditLock(str);
                    result.set_tipinfo(string.Format("下注金額超出可用金額！", new object[0]));
                    result.set_success(500);
                    dictionary.Add("JeuValidate", base.get_JeuValidate());
                    result.set_data(dictionary);
                    strResult = JsonHandle.ObjectToJson(result);
                    return;
                }
                if (double.Parse(strArray4[num18]) < num14)
                {
                    base.DeleteCreditLock(str);
                    result.set_tipinfo(string.Format("下注金額低過最低金額！", new object[0]));
                    result.set_success(500);
                    dictionary.Add("JeuValidate", base.get_JeuValidate());
                    result.set_data(dictionary);
                    strResult = JsonHandle.ObjectToJson(result);
                    return;
                }
                if (!str27.Equals("1") && ((double.Parse(strArray4[num18]) + num9) > num12))
                {
                    base.DeleteCreditLock(str);
                    result.set_tipinfo(string.Format("{0}<{1}>下注單期金額超出單期最大金額！", str25, str26));
                    result.set_success(500);
                    dictionary.Add("JeuValidate", base.get_JeuValidate());
                    result.set_data(dictionary);
                    strResult = JsonHandle.ObjectToJson(result);
                    return;
                }
                num18++;
            }
            string str32 = "";
            if (!((list.Count <= 0) || str27.Equals("1")))
            {
                base.DeleteCreditLock(str);
                result.set_tipinfo(string.Format("賠率有變動,請確認後再提交!", new object[0]));
                result.set_success(600);
                dictionary.Add("JeuValidate", base.get_JeuValidate());
                dictionary.Add("index", list);
                dictionary.Add("newpl", list2);
                dictionary.Add("newwt", str32);
                result.set_data(dictionary);
                strResult = JsonHandle.ObjectToJson(result);
                return;
            }
            double num20 = 0.0;
            int num21 = 0;
            foreach (string str33 in source)
            {
                num20 += double.Parse(strArray4[num21].ToString().Trim());
                num21++;
            }
            if (num8 < num20)
            {
                base.DeleteCreditLock(str);
                result.set_tipinfo(string.Format("可用餘額不足！", new object[0]));
                result.set_success(500);
                dictionary.Add("JeuValidate", base.get_JeuValidate());
                result.set_data(dictionary);
                strResult = JsonHandle.ObjectToJson(result);
                return;
            }
            DataSet ds = null;
            int num22 = 0;
            string str34 = "";
            string str36 = "";
            if (((("92285,92286,92287,92288,92289,92575".IndexOf(str5) <= -1) && ("92638,92639,92640,92641,92642,92643".IndexOf(str5) <= -1)) && ("92565,92566,92567,92568,92569,92570,92571,92636,92637".IndexOf(str5) <= -1)) && ("92572,92588,92589,92590,92591,92592".IndexOf(str5) <= -1))
            {
                bool flag7 = false;
                List<string> successBetList = new List<string>();
                num18 = 0;
                foreach (string str30 in source)
                {
                    table7 = CallBLL.cz_odds_six_bll.GetOddsByID(str30).Tables[0];
                    num28 = int.Parse(table7.Rows[0]["play_id"].ToString());
                    str54 = table7.Rows[0]["o_play_name"].ToString();
                    str55 = table7.Rows[0]["put_amount"].ToString();
                    str56 = table7.Rows[0]["ratio"].ToString();
                    if (FileCacheHelper.get_GetSixPutMoneyCache() == "1")
                    {
                        ds = base.GetUserDrawback_six(rate, getUserModelInfo.get_six_kind(), num28.ToString());
                    }
                    else
                    {
                        ds = base.GetUserDrawback_six(rate, getUserModelInfo.get_six_kind());
                    }
                    if (ds == null)
                    {
                        base.DeleteCreditLock(str);
                        result.set_tipinfo(string.Format("系統錯誤！", new object[0]));
                        result.set_success(400);
                        dictionary.Add("JeuValidate", base.get_JeuValidate());
                        result.set_data(dictionary);
                        strResult = JsonHandle.ObjectToJson(result);
                        return;
                    }
                    DataTable table8 = ds.Tables[0];
                    rowArray2 = table8.Select(string.Format(" play_id={0} ", num28));
                    foreach (DataRow row in rowArray2)
                    {
                        str109 = row["u_type"].ToString().Trim();
                        if (str109 != null)
                        {
                            if (!(str109 == "zj"))
                            {
                                if (str109 == "fgs")
                                {
                                    goto Label_6ED1;
                                }
                                if (str109 == "gd")
                                {
                                    goto Label_6EF5;
                                }
                                if (str109 == "zd")
                                {
                                    goto Label_6F19;
                                }
                                if (str109 == "dl")
                                {
                                    goto Label_6F3D;
                                }
                                if (str109 == "hy")
                                {
                                    goto Label_6F61;
                                }
                            }
                            else
                            {
                                str16 = row[str14 + "_drawback"].ToString().Trim();
                            }
                        }
                        continue;
                    Label_6ED1:
                        str17 = row[str14 + "_drawback"].ToString().Trim();
                        continue;
                    Label_6EF5:
                        str18 = row[str14 + "_drawback"].ToString().Trim();
                        continue;
                    Label_6F19:
                        str19 = row[str14 + "_drawback"].ToString().Trim();
                        continue;
                    Label_6F3D:
                        str20 = row[str14 + "_drawback"].ToString().Trim();
                        continue;
                    Label_6F61:
                        switch (str15)
                        {
                            case "dl":
                                str21 = row[str14 + "_drawback"].ToString().Trim();
                                break;

                            case "zd":
                                str20 = row[str14 + "_drawback"].ToString().Trim();
                                break;

                            case "gd":
                                str19 = row[str14 + "_drawback"].ToString().Trim();
                                break;

                            case "fgs":
                                str18 = row[str14 + "_drawback"].ToString().Trim();
                                break;
                        }
                    }
                    DateTime time6 = DateTime.Now;
                    if (((((playIDByOddsIds.Rows[0]["play_id"].ToString().Trim() == "91001") || (playIDByOddsIds.Rows[0]["play_id"].ToString().Trim() == "91002")) || ((playIDByOddsIds.Rows[0]["play_id"].ToString().Trim() == "91004") || (playIDByOddsIds.Rows[0]["play_id"].ToString().Trim() == "91005"))) || (playIDByOddsIds.Rows[0]["play_id"].ToString().Trim() == "91007")) || (playIDByOddsIds.Rows[0]["play_id"].ToString().Trim() == "91038"))
                    {
                        if (time6 >= time2.AddSeconds(-2.0))
                        {
                            flag7 = true;
                            break;
                        }
                    }
                    else if (time6 >= time.AddSeconds(-2.0))
                    {
                        flag7 = true;
                        break;
                    }
                    num58 = double.Parse(table7.Rows[0]["current_odds"].ToString()) + Convert.ToDouble(table7.Rows[0][str14 + "_diff"].ToString().Trim());
                    str36 = num58.ToString();
                    str67 = str36;
                    base.GetOdds_SIX(str30, table7.Rows[0]["current_odds"].ToString(), table7.Rows[0][str14 + "_diff"].ToString().Trim(), ref str67);
                    playDrawbackValue = base.GetPlayDrawbackValue(str17, str56);
                    if ((playDrawbackValue != 0.0) && (double.Parse(str36) > playDrawbackValue))
                    {
                        base.DeleteCreditLock(str);
                        result.set_tipinfo(string.Format("賠率錯誤！", new object[0]));
                        result.set_success(400);
                        dictionary.Add("JeuValidate", base.get_JeuValidate());
                        result.set_data(dictionary);
                        strResult = JsonHandle.ObjectToJson(result);
                        return;
                    }
                    _six2 = new cz_bet_six();
                    _six2.set_order_num(Utils.GetOrderNumber());
                    _six2.set_checkcode(str11);
                    _six2.set_u_name(getUserModelInfo.get_u_name());
                    _six2.set_u_nicker(getUserModelInfo.get_u_nicker());
                    _six2.set_phase_id(new int?(int.Parse(str13)));
                    _six2.set_phase(str12);
                    _six2.set_bet_time(new DateTime?(DateTime.Now));
                    _six2.set_odds_id(new int?(int.Parse(str30)));
                    _six2.set_category(table7.Rows[0]["category"].ToString());
                    _six2.set_play_id(new int?(int.Parse(table7.Rows[0]["play_id"].ToString())));
                    _six2.set_play_name(table7.Rows[0]["o_play_name"].ToString());
                    _six2.set_bet_val(table7.Rows[0]["put_amount"].ToString());
                    _six2.set_odds(str67);
                    _six2.set_amount(new decimal?(decimal.Parse(strArray4[num18])));
                    _six2.set_profit(0);
                    _six2.set_hy_drawback(new decimal?(decimal.Parse(str21)));
                    _six2.set_dl_drawback(new decimal?(decimal.Parse(str20)));
                    _six2.set_zd_drawback(new decimal?(decimal.Parse(str19)));
                    _six2.set_gd_drawback(new decimal?(decimal.Parse(str18)));
                    _six2.set_fgs_drawback(new decimal?(decimal.Parse(str17)));
                    _six2.set_zj_drawback(new decimal?(decimal.Parse(str16)));
                    _six2.set_dl_rate((float) int.Parse(rate.get_dlzc()));
                    _six2.set_zd_rate((float) int.Parse(rate.get_zdzc()));
                    _six2.set_gd_rate((float) int.Parse(rate.get_gdzc()));
                    _six2.set_fgs_rate((float) int.Parse(num2.ToString()));
                    _six2.set_zj_rate((float) int.Parse(num.ToString()));
                    _six2.set_is_payment(0);
                    _six2.set_dl_name(rate.get_dlname());
                    _six2.set_zd_name(rate.get_zdname());
                    _six2.set_gd_name(rate.get_gdname());
                    _six2.set_fgs_name(rate.get_fgsname());
                    _six2.set_m_type(new int?(num4));
                    _six2.set_kind(str14);
                    _six2.set_ip(LSRequest.GetIP());
                    _six2.set_lottery_type(new int?(num5));
                    _six2.set_lottery_name(base.GetGameNameByID(_six2.get_lottery_type().ToString()));
                    _six2.set_isLM(0);
                    _six2.set_ordervalidcode(Utils.GetOrderValidCode(_six2.get_u_name(), _six2.get_order_num(), _six2.get_bet_val(), _six2.get_odds(), _six2.get_kind(), Convert.ToInt32(_six2.get_phase_id()), Convert.ToInt32(_six2.get_odds_id()), Convert.ToDouble(_six2.get_amount())));
                    _six2.set_odds_zj(str36);
                    num35 = 0;
                    if (!(CallBLL.cz_bet_six_bll.AddBet(_six2, decimal.Parse(strArray4[num18]), str, ref num35) && (num35 > 0)))
                    {
                        base.DeleteCreditLock(str);
                        result.set_tipinfo(string.Format("系統錯誤！", new object[0]));
                        result.set_success(400);
                        dictionary.Add("JeuValidate", base.get_JeuValidate());
                        result.set_data(dictionary);
                        strResult = JsonHandle.ObjectToJson(result);
                        return;
                    }
                    successBetList.Add(string.Concat(new object[] { _six2.get_odds_id(), ",", _six2.get_order_num(), ",", _six2.get_play_name(), ",", _six2.get_bet_val(), ",", _six2.get_odds(), ",", _six2.get_amount() }));
                    num43 = (double.Parse(strArray4[num18]) * num) / 100.0;
                    systemSet = CallBLL.cz_system_set_six_bll.GetSystemSet(100);
                    num44 = 0.0;
                    num45 = 0.0;
                    double num53 = 0.0;
                    if (((int.Parse(str30) > 0x16791) && (int.Parse(str30) <= 0x167c2)) && systemSet.get_is_tmab().Equals(1))
                    {
                        num56 = int.Parse(str30) - 0x31;
                        DataSet oddsByID = CallBLL.cz_odds_six_bll.GetOddsByID(num56.ToString());
                        CallBLL.cz_odds_six_bll.UpdateGrandTotal(Convert.ToDecimal(num43), int.Parse(str30) - 0x31);
                        num44 = double.Parse(oddsByID.Tables[0].Rows[0]["grand_total"].ToString()) + num43;
                        num45 = double.Parse(oddsByID.Tables[0].Rows[0]["downbase"].ToString());
                        num53 = double.Parse(oddsByID.Tables[0].Rows[0]["down_odds_rate"].ToString());
                    }
                    else
                    {
                        CallBLL.cz_odds_six_bll.UpdateGrandTotal(Convert.ToDecimal(num43), int.Parse(str30));
                        num44 = double.Parse(table7.Rows[0]["grand_total"].ToString()) + num43;
                        num45 = double.Parse(table7.Rows[0]["downbase"].ToString());
                        num53 = double.Parse(table7.Rows[0]["down_odds_rate"].ToString());
                    }
                    num46 = Math.Floor((double) (num44 / num45));
                    if (num46 >= 1.0)
                    {
                        int num50;
                        num47 = num53 * num46;
                        num48 = num44 - (num45 * num46);
                        if (((int.Parse(str30) > 0x16791) && (int.Parse(str30) <= 0x167c2)) && systemSet.get_is_tmab().Equals(1))
                        {
                            num50 = CallBLL.cz_odds_six_bll.UpdateGrandTotalCurrentOdds(num47.ToString(), num48.ToString(), str30, true);
                        }
                        else
                        {
                            num50 = CallBLL.cz_odds_six_bll.UpdateGrandTotalCurrentOdds(num47.ToString(), num48.ToString(), str30);
                        }
                        _log = new cz_system_log();
                        _log.set_user_name("系統");
                        _log.set_children_name("");
                        _log.set_category(table7.Rows[0]["category"].ToString());
                        _log.set_play_name(str54);
                        _log.set_put_amount(str55);
                        num56 = 100;
                        _log.set_l_name(base.GetGameNameByID(num56.ToString()));
                        _log.set_l_phase(str12);
                        _log.set_action("降賠率");
                        _log.set_odds_id(int.Parse(str30));
                        str91 = table7.Rows[0]["current_odds"].ToString();
                        _log.set_old_val(str91);
                        num58 = double.Parse(str91) - num47;
                        _log.set_new_val(num58.ToString());
                        _log.set_ip(LSRequest.GetIP());
                        _log.set_add_time(DateTime.Now);
                        _log.set_note("系統自動降賠");
                        _log.set_type_id(Convert.ToInt32((LSEnums.LogTypeID) 0));
                        _log.set_lottery_type(100);
                        CallBLL.cz_system_log_bll.Add(_log);
                        _odds = new cz_jp_odds();
                        _odds.set_add_time(DateTime.Now);
                        _odds.set_odds_id(int.Parse(str30));
                        _odds.set_phase_id(int.Parse(str13));
                        _odds.set_play_name(str54);
                        _odds.set_put_amount(str55);
                        _odds.set_odds(num47.ToString());
                        _odds.set_lottery_type(100);
                        _odds.set_phase(str12);
                        _odds.set_old_odds(str91);
                        num58 = double.Parse(str91) - num47;
                        _odds.set_new_odds(num58.ToString());
                        CallBLL.cz_jp_odds_bll.Add(_odds);
                        if (int.Parse(str30) <= 0x16791)
                        {
                            num56 = Convert.ToInt32(str30) + 0x31;
                            DataTable table16 = CallBLL.cz_odds_six_bll.GetOddsByID(num56.ToString()).Tables[0];
                            num56 = Convert.ToInt32(str30) + 0x31;
                            int num54 = CallBLL.cz_odds_six_bll.UpdateCurrentOdds(num47.ToString(), num56.ToString());
                            _log2 = new cz_system_log();
                            _log2.set_user_name("系統");
                            _log2.set_children_name("");
                            _log2.set_category(table16.Rows[0]["category"].ToString());
                            _log2.set_play_name(table16.Rows[0]["play_name"].ToString());
                            _log2.set_put_amount(table16.Rows[0]["put_amount"].ToString());
                            num56 = 100;
                            _log2.set_l_name(base.GetGameNameByID(num56.ToString()));
                            _log2.set_l_phase(str12);
                            _log2.set_action("降賠率");
                            _log2.set_odds_id(int.Parse(str30) + 0x31);
                            str94 = table16.Rows[0]["current_odds"].ToString();
                            _log2.set_old_val(str94);
                            num58 = double.Parse(str94) - num47;
                            _log2.set_new_val(num58.ToString());
                            _log2.set_ip(LSRequest.GetIP());
                            _log2.set_add_time(DateTime.Now);
                            _log2.set_note("系統自動降賠");
                            _log2.set_type_id(Convert.ToInt32((LSEnums.LogTypeID) 0));
                            _log2.set_lottery_type(100);
                            CallBLL.cz_system_log_bll.Add(_log2);
                            _odds2 = new cz_jp_odds();
                            _odds2.set_add_time(DateTime.Now);
                            _odds2.set_odds_id(int.Parse(str30) + 0x31);
                            _odds2.set_phase_id(int.Parse(str13));
                            _odds2.set_play_name(table16.Rows[0]["play_name"].ToString());
                            _odds2.set_put_amount(table16.Rows[0]["put_amount"].ToString());
                            _odds2.set_odds(num47.ToString());
                            _odds2.set_lottery_type(100);
                            _odds2.set_phase(str12);
                            _odds2.set_old_odds(str94);
                            num58 = double.Parse(str94) - num47;
                            _odds2.set_new_odds(num58.ToString());
                            CallBLL.cz_jp_odds_bll.Add(_odds2);
                        }
                        if ((int.Parse(str30) > 0x16791) && (int.Parse(str30) <= 0x167c2))
                        {
                            num56 = Convert.ToInt32(str30) - 0x31;
                            DataTable table17 = CallBLL.cz_odds_six_bll.GetOddsByID(num56.ToString()).Tables[0];
                            num56 = Convert.ToInt32(str30) - 0x31;
                            int num55 = CallBLL.cz_odds_six_bll.UpdateCurrentOdds(num47.ToString(), num56.ToString());
                            _log2 = new cz_system_log();
                            _log2.set_user_name("系統");
                            _log2.set_children_name("");
                            _log2.set_category(table17.Rows[0]["category"].ToString());
                            _log2.set_play_name(table17.Rows[0]["play_name"].ToString());
                            _log2.set_put_amount(table17.Rows[0]["put_amount"].ToString());
                            num56 = 100;
                            _log2.set_l_name(base.GetGameNameByID(num56.ToString()));
                            _log2.set_l_phase(str12);
                            _log2.set_action("降賠率");
                            _log2.set_odds_id(int.Parse(str30) - 0x31);
                            str94 = table17.Rows[0]["current_odds"].ToString();
                            _log2.set_old_val(str94);
                            num58 = double.Parse(str94) - num47;
                            _log2.set_new_val(num58.ToString());
                            _log2.set_ip(LSRequest.GetIP());
                            _log2.set_add_time(DateTime.Now);
                            _log2.set_note("系統自動降賠");
                            _log2.set_type_id(Convert.ToInt32((LSEnums.LogTypeID) 0));
                            _log2.set_lottery_type(100);
                            CallBLL.cz_system_log_bll.Add(_log2);
                            _odds2 = new cz_jp_odds();
                            _odds2.set_add_time(DateTime.Now);
                            _odds2.set_odds_id(int.Parse(str30) - 0x31);
                            _odds2.set_phase_id(int.Parse(str13));
                            _odds2.set_play_name(table17.Rows[0]["play_name"].ToString());
                            _odds2.set_put_amount(table17.Rows[0]["put_amount"].ToString());
                            _odds2.set_odds(num47.ToString());
                            _odds2.set_lottery_type(100);
                            _odds2.set_phase(str12);
                            _odds2.set_old_odds(str94);
                            num58 = double.Parse(str94) - num47;
                            _odds2.set_new_odds(num58.ToString());
                            CallBLL.cz_jp_odds_bll.Add(_odds2);
                        }
                    }
                    fgsWTTable = null;
                    if (getUserModelInfo.get_six_op_odds().Equals(1))
                    {
                        fgsWTTable = base.GetFgsWTTable(100);
                    }
                    num56 = 100;
                    base.Add_Bet_Lock(num56.ToString(), rate.get_dlname(), str30);
                    CallBLL.cz_autosale_six_bll.DLAutoSale(_six2.get_order_num(), str11, getUserModelInfo.get_u_nicker(), str14, table7.Rows[0]["play_id"].ToString(), str30.Trim(), str16, str17, str18, str19, str20, str21, str13, str12, _six2.get_ip(), num5, _six2.get_lottery_name(), getUserModelInfo, rate, fgsWTTable);
                    num56 = 100;
                    base.Un_Bet_Lock(num56.ToString(), rate.get_dlname(), str30);
                    if (getUserModelInfo.get_six_op_odds().Equals(1))
                    {
                        fgsWTTable = base.GetFgsWTTable(100);
                    }
                    num56 = 100;
                    base.Add_Bet_Lock(num56.ToString(), rate.get_zdname(), str30);
                    CallBLL.cz_autosale_six_bll.ZDAutoSale(_six2.get_order_num(), str11, getUserModelInfo.get_u_nicker(), str14, table7.Rows[0]["play_id"].ToString(), str30.Trim(), str16, str17, str18, str19, str20, str21, str13, str12, _six2.get_ip(), num5, _six2.get_lottery_name(), getUserModelInfo, rate, fgsWTTable);
                    num56 = 100;
                    base.Un_Bet_Lock(num56.ToString(), rate.get_zdname(), str30);
                    if (getUserModelInfo.get_six_op_odds().Equals(1))
                    {
                        fgsWTTable = base.GetFgsWTTable(100);
                    }
                    num56 = 100;
                    base.Add_Bet_Lock(num56.ToString(), rate.get_gdname(), str30);
                    CallBLL.cz_autosale_six_bll.GDAutoSale(_six2.get_order_num(), str11, getUserModelInfo.get_u_nicker(), str14, table7.Rows[0]["play_id"].ToString(), str30.Trim(), str16, str17, str18, str19, str20, str21, str13, str12, _six2.get_ip(), num5, _six2.get_lottery_name(), getUserModelInfo, rate, fgsWTTable);
                    num56 = 100;
                    base.Un_Bet_Lock(num56.ToString(), rate.get_gdname(), str30);
                    if (getUserModelInfo.get_six_op_odds().Equals(1))
                    {
                        fgsWTTable = base.GetFgsWTTable(100);
                    }
                    num56 = 100;
                    base.Add_Bet_Lock(num56.ToString(), rate.get_fgsname(), str30);
                    CallBLL.cz_autosale_six_bll.FGSAutoSale(_six2.get_order_num(), str11, getUserModelInfo.get_u_nicker(), str14, table7.Rows[0]["play_id"].ToString(), str30.Trim(), str16, str17, str18, str19, str20, str21, str13, str12, _six2.get_ip(), num5, _six2.get_lottery_name(), getUserModelInfo, rate, fgsWTTable);
                    num56 = 100;
                    base.Un_Bet_Lock(num56.ToString(), rate.get_fgsname(), str30);
                    num18++;
                    if (FileCacheHelper.get_GetSixPutMoneyCache() == "1")
                    {
                        base.SetUserDrawback_six(ds, num28.ToString());
                    }
                }
                base.SetUserRate_six(rate);
                if (FileCacheHelper.get_GetSixPutMoneyCache() != "1")
                {
                    base.SetUserDrawback_six(ds);
                }
                getUserModelInfo.set_begin_six("yes");
                if (flag7)
                {
                    base.DeleteCreditLock(str);
                    List<FastSale> list9 = base.UserPutBet(plDT, successBetList, source, strArray3, strArray4);
                    result.set_tipinfo(string.Format("下注提示！", new object[0]));
                    result.set_success(210);
                    dictionary.Add("JeuValidate", base.get_JeuValidate());
                    dictionary.Add("returnlist", list9);
                    result.set_data(dictionary);
                    strResult = JsonHandle.ObjectToJson(result);
                }
                else
                {
                    base.DeleteCreditLock(str);
                    result.set_tipinfo(string.Format("下注成功！", new object[0]));
                    result.set_success(200);
                    dictionary.Add("JeuValidate", base.get_JeuValidate());
                    result.set_data(dictionary);
                    strResult = JsonHandle.ObjectToJson(result);
                }
                return;
            }
            if (("92638,92639,92640,92641,92642,92643".IndexOf(str5) > -1) && (base.IsShowLM_B() == 0))
            {
                base.DeleteCreditLock(str);
                result.set_tipinfo(string.Format("下注錯誤！！", new object[0]));
                result.set_success(500);
                dictionary.Add("JeuValidate", base.get_JeuValidate());
                result.set_data(dictionary);
                strResult = JsonHandle.ObjectToJson(result);
                return;
            }
            string str38 = LSRequest.qq("lmtype");
            string str39 = LSRequest.qq("numbers");
            string str40 = LSRequest.qq("uPI_WT");
            string str41 = px(getlongnum(str39.Replace("|", ",")));
            int length = str41.Split(new char[] { ',' }).Length;
            string[] strArray5 = str41.Split(new char[] { ',' });
            int count = strArray5.Distinct<string>().ToList<string>().Count;
            for (index = 0; index < strArray5.Length; index++)
            {
                if (((str5.Equals("92569") || str5.Equals("92570")) || str5.Equals("92571")) || str5.Equals("92637"))
                {
                    num56 = int.Parse(strArray5[index]);
                    if (num56.Equals(0))
                    {
                        strArray5[index] = "10";
                    }
                }
            }
            if (count != strArray5.Length)
            {
                base.DeleteCreditLock(str);
                result.set_tipinfo(string.Format("下注不能有重複碼！", new object[0]));
                result.set_success(400);
                dictionary.Add("JeuValidate", base.get_JeuValidate());
                result.set_data(dictionary);
                strResult = JsonHandle.ObjectToJson(result);
                return;
            }
            if (strArray5.Length != length)
            {
                base.DeleteCreditLock(str);
                result.set_tipinfo(string.Format("下注不能有重複碼！", new object[0]));
                result.set_success(400);
                dictionary.Add("JeuValidate", base.get_JeuValidate());
                result.set_data(dictionary);
                strResult = JsonHandle.ObjectToJson(result);
                return;
            }
            if (str5.Equals("92565"))
            {
                if (strArray5.Length != 6)
                {
                    base.DeleteCreditLock(str);
                    result.set_tipinfo(string.Format("“六肖”最少必須選擇六個生肖！", new object[0]));
                    result.set_success(400);
                    dictionary.Add("JeuValidate", base.get_JeuValidate());
                    result.set_data(dictionary);
                    strResult = JsonHandle.ObjectToJson(result);
                    return;
                }
                if (strArray5.All<string>(p => (int.Parse(p) % 2) == 0))
                {
                    base.DeleteCreditLock(str);
                    result.set_tipinfo(string.Format("抱歉！“六肖”禁止全單/雙路投注，該注單請投在“特碼單雙”項！", new object[0]));
                    result.set_success(400);
                    dictionary.Add("JeuValidate", base.get_JeuValidate());
                    result.set_data(dictionary);
                    strResult = JsonHandle.ObjectToJson(result);
                    return;
                }
                if (strArray5.All<string>(p => (int.Parse(p) % 2) != 0))
                {
                    base.DeleteCreditLock(str);
                    result.set_tipinfo(string.Format("抱歉！“六肖”禁止全單/雙路投注，該注單請投在“特碼單雙”項！", new object[0]));
                    result.set_success(400);
                    dictionary.Add("JeuValidate", base.get_JeuValidate());
                    result.set_data(dictionary);
                    strResult = JsonHandle.ObjectToJson(result);
                    return;
                }
                num22 = 1;
                str34 = px(str41);
            }
            foreach (string str42 in strArray5)
            {
                if (((str5.Equals("92565") || str5.Equals("92566")) || (str5.Equals("92567") || str5.Equals("92568"))) || str5.Equals("92636"))
                {
                    if ((int.Parse(str42) > 12) || (int.Parse(str42) < 1))
                    {
                        base.DeleteCreditLock(str);
                        result.set_tipinfo(string.Format("下注號碼錯誤！", new object[0]));
                        result.set_success(400);
                        dictionary.Add("JeuValidate", base.get_JeuValidate());
                        result.set_data(dictionary);
                        strResult = JsonHandle.ObjectToJson(result);
                        return;
                    }
                }
                else if (((str5.Equals("92569") || str5.Equals("92570")) || str5.Equals("92571")) || str5.Equals("92637"))
                {
                    if ((int.Parse(str42) > 10) || (int.Parse(str42) < 1))
                    {
                        base.DeleteCreditLock(str);
                        result.set_tipinfo(string.Format("下注號碼錯誤！", new object[0]));
                        result.set_success(400);
                        dictionary.Add("JeuValidate", base.get_JeuValidate());
                        result.set_data(dictionary);
                        strResult = JsonHandle.ObjectToJson(result);
                        return;
                    }
                }
                else if ((int.Parse(str42) > 0x31) || (int.Parse(str42) < 1))
                {
                    base.DeleteCreditLock(str);
                    result.set_tipinfo(string.Format("下注號碼錯誤！", new object[0]));
                    result.set_success(400);
                    dictionary.Add("JeuValidate", base.get_JeuValidate());
                    result.set_data(dictionary);
                    strResult = JsonHandle.ObjectToJson(result);
                    return;
                }
            }
            string str43 = "";
            str109 = str38;
            if (str109 != null)
            {
                if (!(str109 == "0"))
                {
                    if (str109 == "1")
                    {
                        string[] strArray6 = str39.Split(new char[] { '|' });
                        str43 = str39.Replace("|", ",");
                        string str44 = "";
                        string str45 = "";
                        int num25 = 0;
                        int num26 = 0;
                    Label_22C2:;
                        if (num26 < strArray6[0].Split(new char[] { ',' }).Length)
                        {
                            str44 = str44 + getnum(strArray6[0].Split(new char[] { ',' })[num26]) + ",";
                            num25++;
                            num26++;
                            goto Label_22C2;
                        }
                        num26 = 0;
                    Label_2333:;
                        if (num26 < strArray6[1].Split(new char[] { ',' }).Length)
                        {
                            str45 = str45 + getnum(strArray6[1].Split(new char[] { ',' })[num26]) + ",";
                            num26++;
                            goto Label_2333;
                        }
                        if (num25.Equals(0))
                        {
                            context.Response.End();
                        }
                        if ((("92285".Equals(str5) || "92286".Equals(str5)) || str5.Equals("92638")) || str5.Equals("92639"))
                        {
                            num22 = length - 2;
                            if (!num25.Equals(2))
                            {
                                context.Response.End();
                            }
                        }
                        else if (((("92287".Equals(str5) || "92288".Equals(str5)) || ("92289".Equals(str5) || str5.Equals("92640"))) || str5.Equals("92641")) || str5.Equals("92642"))
                        {
                            num22 = length - 1;
                            if (num25 != 1)
                            {
                                context.Response.End();
                            }
                        }
                        else if ("92566".Equals(str5) || "92569".Equals(str5))
                        {
                            num22 = length - 1;
                            if (num25 != 1)
                            {
                                context.Response.End();
                            }
                        }
                        else if ("92567".Equals(str5) || "92570".Equals(str5))
                        {
                            num22 = length - 2;
                            if (num25 != 2)
                            {
                                context.Response.End();
                            }
                        }
                        else if ("92568".Equals(str5) || "92571".Equals(str5))
                        {
                            num22 = length - 3;
                            if (num25 != 3)
                            {
                                context.Response.End();
                            }
                        }
                        else if ("92636".Equals(str5) || "92637".Equals(str5))
                        {
                            num22 = length - 4;
                            if (num25 != 4)
                            {
                                context.Response.End();
                            }
                        }
                        str45 = str45.Substring(0, str45.Length - 1);
                        str41 = str44 + "|" + str45;
                        strArray6 = str45.Split(new char[] { ',' });
                        str34 = "";
                        foreach (string str30 in strArray6)
                        {
                            if (str30.Trim() != "")
                            {
                                str34 = str34 + str44 + str30.Trim() + "~";
                            }
                        }
                        str34 = str34.Substring(0, str34.Length - 1);
                        goto Label_2B62;
                    }
                    if (str109 == "2")
                    {
                        num22 = 0x10;
                        DataTable table6 = CallBLL.cz_zodiac_six_bll.GetZodiacByID(str41).Tables[0];
                        strArray7 = base.get_ZodiacNumberArray().Split(new char[] { ',' });
                        strArray8 = strArray7[int.Parse(str41.Split(new char[] { ',' })[0]) - 1].Replace("、", ",").Split(new char[] { ',' });
                        str46 = strArray7[int.Parse(str41.Split(new char[] { ',' })[0]) - 1].Replace("、", ",");
                        str43 = str46;
                        if (strArray8.Length > 4)
                        {
                            num22 = 20;
                        }
                        strArray9 = strArray7[int.Parse(str41.Split(new char[] { ',' })[1]) - 1].Replace("、", ",").Split(new char[] { ',' });
                        str47 = strArray7[int.Parse(str41.Split(new char[] { ',' })[1]) - 1].Replace("、", ",");
                        str43 = str43 + "," + str47;
                        if (strArray9.Length > 4)
                        {
                            num22 = 20;
                        }
                        str41 = str46 + "|" + str47;
                        str34 = getzsstr(str46, str47);
                        goto Label_2B62;
                    }
                    if (str109 == "3")
                    {
                        if (str41.IndexOf("10") > -1)
                        {
                            num22 = 20;
                        }
                        else
                        {
                            num22 = 0x19;
                        }
                        strArray10 = base.get_WSNumberArray().Split(new char[] { ',' });
                        str46 = strArray10[int.Parse(str41.Split(new char[] { ',' })[0]) - 1].Replace("、", ",");
                        str47 = strArray10[int.Parse(str41.Split(new char[] { ',' })[1]) - 1].Replace("、", ",");
                        str41 = str46 + "|" + str47;
                        str43 = str46 + "|" + str47;
                        str34 = getzsstr(str46, str47);
                        goto Label_2B62;
                    }
                    if (str109 == "4")
                    {
                        strArray11 = str41.Split(new char[] { ',' });
                        str46 = "";
                        str47 = "";
                        if (int.Parse(strArray11[0]) > 12)
                        {
                            num27 = (int.Parse(strArray11[0]) - 1) - 12;
                            str46 = base.get_WSNumberArray().Split(new char[] { ',' })[num27].Replace("、", ",");
                        }
                        else
                        {
                            str46 = base.get_ZodiacNumberArray().Split(new char[] { ',' })[int.Parse(strArray11[0]) - 1].Replace("、", ",");
                        }
                        if (int.Parse(strArray11[1]) > 12)
                        {
                            num27 = (int.Parse(strArray11[1]) - 1) - 12;
                            str47 = base.get_WSNumberArray().Split(new char[] { ',' })[num27].Replace("、", ",");
                        }
                        else
                        {
                            str47 = base.get_ZodiacNumberArray().Split(new char[] { ',' })[int.Parse(strArray11[1]) - 1].Replace("、", ",");
                        }
                        num22 = getzs(str46.Split(new char[] { ',' }), str47.Split(new char[] { ',' }));
                        str41 = str46 + "|" + str47;
                        str43 = str46 + "," + str47;
                        str34 = getzsstr(str46, str47);
                        goto Label_2B62;
                    }
                }
                else
                {
                    if (((str5.Equals("92285") || str5.Equals("92286")) || str5.Equals("92638")) || str5.Equals("92639"))
                    {
                        num22 = ((length * (length - 1)) * (length - 2)) / 6;
                        str34 = getfz(str41, 3);
                    }
                    else if ((((str5.Equals("92287") || str5.Equals("92288")) || (str5.Equals("92289") || str5.Equals("92640"))) || str5.Equals("92641")) || str5.Equals("92642"))
                    {
                        num22 = (length * (length - 1)) / 2;
                        str34 = getfz(str41, 2);
                    }
                    else if (str5.Equals("92575") || str5.Equals("92643"))
                    {
                        num22 = (((length * (length - 1)) * (length - 2)) * (length - 3)) / 0x18;
                        str34 = getfz(str41, 4);
                    }
                    else if (str5.Equals("92572"))
                    {
                        num22 = ((((length * (length - 1)) * (length - 2)) * (length - 3)) * (length - 4)) / 120;
                        str34 = getfz(str41, 5);
                    }
                    else if (str5.Equals("92588"))
                    {
                        num22 = (((((length * (length - 1)) * (length - 2)) * (length - 3)) * (length - 4)) * (length - 5)) / 720;
                        str34 = getfz(str41, 6);
                    }
                    else if (str5.Equals("92589"))
                    {
                        num22 = ((((((length * (length - 1)) * (length - 2)) * (length - 3)) * (length - 4)) * (length - 5)) * (length - 6)) / 0x13b0;
                        str41 = getlongnum(str41);
                        str34 = getfz(str41, 7);
                    }
                    else if (str5.Equals("92590"))
                    {
                        num22 = (((((((length * (length - 1)) * (length - 2)) * (length - 3)) * (length - 4)) * (length - 5)) * (length - 6)) * (length - 7)) / 0x9d80;
                        str41 = getlongnum(str41);
                        str34 = getfz(str41, 8);
                    }
                    else if (str5.Equals("92591"))
                    {
                        num22 = ((((((((length * (length - 1)) * (length - 2)) * (length - 3)) * (length - 4)) * (length - 5)) * (length - 6)) * (length - 7)) * (length - 8)) / 0x58980;
                        str41 = getlongnum(str41);
                        str34 = getfz(str41, 9);
                    }
                    else if (str5.Equals("92592"))
                    {
                        num22 = (((((((((length * (length - 1)) * (length - 2)) * (length - 3)) * (length - 4)) * (length - 5)) * (length - 6)) * (length - 7)) * (length - 8)) * (length - 9)) / 0x375f00;
                        str41 = getlongnum(str41);
                        str34 = getfz(str41, 10);
                    }
                    else if (str5.Equals("92566") || str5.Equals("92569"))
                    {
                        num22 = (length * (length - 1)) / 2;
                        str34 = getfz(str41, 2);
                    }
                    else if (str5.Equals("92567") || str5.Equals("92570"))
                    {
                        num22 = ((length * (length - 1)) * (length - 2)) / 6;
                        str34 = getfz(str41, 3);
                    }
                    else if (str5.Equals("92568") || str5.Equals("92571"))
                    {
                        num22 = (((length * (length - 1)) * (length - 2)) * (length - 3)) / 0x18;
                        str34 = getfz(str41, 4);
                    }
                    else if (str5.Equals("92636") || str5.Equals("92637"))
                    {
                        num22 = ((((length * (length - 1)) * (length - 2)) * (length - 3)) * (length - 4)) / 120;
                        str34 = getfz(str41, 5);
                    }
                    str43 = str41;
                    goto Label_2B62;
                }
            }
            context.Response.End();
        Label_2B62:
            table7 = CallBLL.cz_odds_six_bll.GetOddsByID(str5).Tables[0];
            str23 = table7.Rows[0]["current_odds"].ToString();
            string str48 = table7.Rows[0]["max_odds"].ToString();
            string str49 = table7.Rows[0]["min_odds"].ToString();
            string str50 = table7.Rows[0]["a_diff"].ToString();
            string str51 = table7.Rows[0]["b_diff"].ToString();
            string str52 = table7.Rows[0]["c_diff"].ToString();
            string str53 = table7.Rows[0][str14 + "_diff"].ToString();
            num28 = int.Parse(table7.Rows[0]["play_id"].ToString());
            str54 = table7.Rows[0]["o_play_name"].ToString();
            str55 = table7.Rows[0]["put_amount"].ToString();
            str56 = table7.Rows[0]["ratio"].ToString();
            string ratio = table7.Rows[0]["ratio2"].ToString();
            DataSet set8 = null;
            if ((base.IsShowLM_B() == 1) && (("92285,92286,92287,92288,92289,92575".IndexOf(str5) > -1) || ("92638,92639,92640,92641,92642,92643".IndexOf(str5) > -1)))
            {
                str58 = "";
                str59 = "";
                Utils.Six_LM_AB(str5, ref str58, ref str59);
                set8 = CallBLL.cz_odds_six_bll.GetOddsByID(str58);
            }
            if (FileCacheHelper.get_GetSixPutMoneyCache() == "1")
            {
                ds = base.GetUserDrawback_six(rate, getUserModelInfo.get_six_kind(), num28.ToString());
            }
            else
            {
                ds = base.GetUserDrawback_six(rate, getUserModelInfo.get_six_kind());
            }
            if (ds == null)
            {
                base.DeleteCreditLock(str);
                result.set_tipinfo(string.Format("系統錯誤！", new object[0]));
                result.set_success(400);
                dictionary.Add("JeuValidate", base.get_JeuValidate());
                result.set_data(dictionary);
                strResult = JsonHandle.ObjectToJson(result);
            }
            else
            {
                double num30;
                string str60;
                string str61;
                string[] strArray12;
                int num31;
                DataTable table9;
                string sXLNameBySXid;
                string[] strArray13;
                string str68;
                string str69;
                string str70;
                string str71;
                List<string> list3;
                List<string> list4;
                rowArray2 = ds.Tables[0].Select(string.Format(" play_id={0} ", num28));
                foreach (DataRow row in rowArray2)
                {
                    str109 = row["u_type"].ToString().Trim();
                    if (str109 != null)
                    {
                        if (!(str109 == "zj"))
                        {
                            if (str109 == "fgs")
                            {
                                goto Label_2F08;
                            }
                            if (str109 == "gd")
                            {
                                goto Label_2F2C;
                            }
                            if (str109 == "zd")
                            {
                                goto Label_2F50;
                            }
                            if (str109 == "dl")
                            {
                                goto Label_2F74;
                            }
                            if (str109 == "hy")
                            {
                                goto Label_2F98;
                            }
                        }
                        else
                        {
                            str16 = row[str14 + "_drawback"].ToString().Trim();
                        }
                    }
                    continue;
                Label_2F08:
                    str17 = row[str14 + "_drawback"].ToString().Trim();
                    continue;
                Label_2F2C:
                    str18 = row[str14 + "_drawback"].ToString().Trim();
                    continue;
                Label_2F50:
                    str19 = row[str14 + "_drawback"].ToString().Trim();
                    continue;
                Label_2F74:
                    str20 = row[str14 + "_drawback"].ToString().Trim();
                    continue;
                Label_2F98:
                    switch (str15)
                    {
                        case "dl":
                            str21 = row[str14 + "_drawback"].ToString().Trim();
                            break;

                        case "zd":
                            str20 = row[str14 + "_drawback"].ToString().Trim();
                            break;

                        case "gd":
                            str19 = row[str14 + "_drawback"].ToString().Trim();
                            break;

                        case "fgs":
                            str18 = row[str14 + "_drawback"].ToString().Trim();
                            break;
                    }
                }
                double num29 = double.Parse(str7) * num22;
                if (str5.Equals("92565"))
                {
                    num29 = double.Parse(str7) * 1.0;
                    if (double.Parse(str7) > num10)
                    {
                        base.DeleteCreditLock(str);
                        result.set_tipinfo(string.Format("下註金額超出單註最大金額！", new object[0]));
                        result.set_success(400);
                        dictionary.Add("JeuValidate", base.get_JeuValidate());
                        result.set_data(dictionary);
                        strResult = JsonHandle.ObjectToJson(result);
                        return;
                    }
                    if (double.Parse(str7) < num14)
                    {
                        base.DeleteCreditLock(str);
                        result.set_tipinfo(string.Format("下註金額低過最低金額！", new object[0]));
                        result.set_success(400);
                        dictionary.Add("JeuValidate", base.get_JeuValidate());
                        result.set_data(dictionary);
                        strResult = JsonHandle.ObjectToJson(result);
                        return;
                    }
                    num30 = num12 - double.Parse(str7);
                    str60 = "";
                    str61 = "";
                    strArray12 = str34.Replace("00", "10").Split(new char[] { '~' });
                    num31 = 0;
                    foreach (string str62 in strArray12)
                    {
                        if (num31 == 0)
                        {
                            str60 = str60 + "'" + px(str62) + "'";
                            str61 = str61 + px(str62);
                        }
                        else
                        {
                            str60 = str60 + ",'" + px(str62) + "'";
                            str61 = str61 + "|" + px(str62);
                        }
                        num31++;
                    }
                    table9 = CallBLL.cz_bet_six_bll.DQLMValid(int.Parse(str5), getUserModelInfo.get_u_name(), str61, num30);
                    if (table9 != null)
                    {
                        sXLNameBySXid = table9.Rows[0][0].ToString().Trim();
                        strArray13 = sXLNameBySXid.Split(new char[] { ',' });
                        for (index = 0; index < strArray13.Length; index++)
                        {
                            if (index == 0)
                            {
                                sXLNameBySXid = FunctionSix.GetSXLNameBySXid(strArray13[index]);
                            }
                            else
                            {
                                sXLNameBySXid = sXLNameBySXid + "、" + FunctionSix.GetSXLNameBySXid(strArray13[index]);
                            }
                        }
                        base.DeleteCreditLock(str);
                        result.set_tipinfo(string.Format("單組【" + sXLNameBySXid + "】單期下註金額超出單期金額[" + num12.ToString() + "]的限制！", new object[0]));
                        result.set_success(400);
                        dictionary.Add("JeuValidate", base.get_JeuValidate());
                        result.set_data(dictionary);
                        strResult = JsonHandle.ObjectToJson(result);
                        return;
                    }
                }
                else
                {
                    if (double.Parse(str7) > num10)
                    {
                        base.DeleteCreditLock(str);
                        result.set_tipinfo(string.Format("下註金額超出單註最大金額！", new object[0]));
                        result.set_success(400);
                        dictionary.Add("JeuValidate", base.get_JeuValidate());
                        result.set_data(dictionary);
                        strResult = JsonHandle.ObjectToJson(result);
                        return;
                    }
                    if (double.Parse(str7) < num14)
                    {
                        base.DeleteCreditLock(str);
                        result.set_tipinfo(string.Format("下註金額低過最低金額！", new object[0]));
                        result.set_success(400);
                        dictionary.Add("JeuValidate", base.get_JeuValidate());
                        result.set_data(dictionary);
                        strResult = JsonHandle.ObjectToJson(result);
                        return;
                    }
                    if (num8 < num29)
                    {
                        base.DeleteCreditLock(str);
                        result.set_tipinfo(string.Format("可用餘額不足！", new object[0]));
                        result.set_success(500);
                        dictionary.Add("JeuValidate", base.get_JeuValidate());
                        result.set_data(dictionary);
                        strResult = JsonHandle.ObjectToJson(result);
                        return;
                    }
                    num30 = num12 - double.Parse(str7);
                    str60 = "";
                    str61 = "";
                    strArray12 = str34.Replace("00", "10").Split(new char[] { '~' });
                    num31 = 0;
                    foreach (string str62 in strArray12)
                    {
                        if (num31 == 0)
                        {
                            str60 = str60 + "'" + px(str62) + "'";
                            str61 = str61 + px(str62);
                        }
                        else
                        {
                            str60 = str60 + ",'" + px(str62) + "'";
                            str61 = str61 + "|" + px(str62);
                        }
                        num31++;
                    }
                    table9 = CallBLL.cz_bet_six_bll.DQLMValid(int.Parse(str5), getUserModelInfo.get_u_name(), str61, num30);
                    if (table9 != null)
                    {
                        sXLNameBySXid = table9.Rows[0][0].ToString().Trim();
                        switch (str5)
                        {
                            case "92566":
                            case "92567":
                            case "92568":
                            case "92636":
                                strArray13 = sXLNameBySXid.Split(new char[] { ',' });
                                for (index = 0; index < strArray13.Length; index++)
                                {
                                    if (index == 0)
                                    {
                                        sXLNameBySXid = FunctionSix.GetSXLNameBySXid(strArray13[index]);
                                    }
                                    else
                                    {
                                        sXLNameBySXid = sXLNameBySXid + "、" + FunctionSix.GetSXLNameBySXid(strArray13[index]);
                                    }
                                }
                                break;

                            case "92569":
                            case "92570":
                            case "92571":
                            case "92637":
                                strArray13 = sXLNameBySXid.Split(new char[] { ',' });
                                for (index = 0; index < strArray13.Length; index++)
                                {
                                    if (index == 0)
                                    {
                                        sXLNameBySXid = FunctionSix.GetWSLNameBySXid(strArray13[index]);
                                    }
                                    else
                                    {
                                        sXLNameBySXid = sXLNameBySXid + "、" + FunctionSix.GetWSLNameBySXid(strArray13[index]);
                                    }
                                }
                                break;
                        }
                        base.DeleteCreditLock(str);
                        result.set_tipinfo(string.Format("單組【" + sXLNameBySXid + "】單期下註金額超出單期金額[" + num12.ToString() + "]的限制！", new object[0]));
                        result.set_success(400);
                        dictionary.Add("JeuValidate", base.get_JeuValidate());
                        result.set_data(dictionary);
                        strResult = JsonHandle.ObjectToJson(result);
                        return;
                    }
                }
                string str64 = "";
                bool isChange = false;
                if (str27.Equals("1"))
                {
                    if ((!str38.Equals("2") && !str38.Equals("3")) && !str38.Equals("4"))
                    {
                        str64 = base.GetWTListPC(str5, str41.Replace("|", ",").Replace(",,", ","), str40, ref isChange);
                    }
                    else
                    {
                        string str65 = "";
                        string str66 = LSRequest.qq("numbers");
                        str109 = str38;
                        if (str109 != null)
                        {
                            if (!(str109 == "2"))
                            {
                                if (str109 == "3")
                                {
                                    strArray10 = base.get_WSNumberArray().Split(new char[] { ',' });
                                    str46 = strArray10[int.Parse(str66.Split(new char[] { ',' })[0]) - 1].Replace("、", ",");
                                    str47 = strArray10[int.Parse(str66.Split(new char[] { ',' })[1]) - 1].Replace("、", ",");
                                    str65 = str46 + "|" + str47;
                                }
                                else if (str109 == "4")
                                {
                                    strArray11 = str66.Split(new char[] { ',' });
                                    str46 = "";
                                    str47 = "";
                                    if (int.Parse(strArray11[0]) > 12)
                                    {
                                        num27 = (int.Parse(strArray11[0]) - 1) - 12;
                                        str46 = base.get_WSNumberArray().Split(new char[] { ',' })[num27].Replace("、", ",");
                                    }
                                    else
                                    {
                                        str46 = base.get_ZodiacNumberArray().Split(new char[] { ',' })[int.Parse(strArray11[0]) - 1].Replace("、", ",");
                                    }
                                    if (int.Parse(strArray11[1]) > 12)
                                    {
                                        num27 = (int.Parse(strArray11[1]) - 1) - 12;
                                        str47 = base.get_WSNumberArray().Split(new char[] { ',' })[num27].Replace("、", ",");
                                    }
                                    else
                                    {
                                        str47 = base.get_ZodiacNumberArray().Split(new char[] { ',' })[int.Parse(strArray11[1]) - 1].Replace("、", ",");
                                    }
                                    str65 = str46 + "," + str47;
                                }
                            }
                            else
                            {
                                strArray7 = base.get_ZodiacNumberArray().Split(new char[] { ',' });
                                strArray8 = strArray7[int.Parse(str66.Split(new char[] { ',' })[0]) - 1].Replace("、", ",").Split(new char[] { ',' });
                                str46 = strArray7[int.Parse(str66.Split(new char[] { ',' })[0]) - 1].Replace("、", ",");
                                strArray9 = strArray7[int.Parse(str66.Split(new char[] { ',' })[1]) - 1].Replace("、", ",").Split(new char[] { ',' });
                                str47 = strArray7[int.Parse(str66.Split(new char[] { ',' })[1]) - 1].Replace("、", ",");
                                str65 = str46 + "|" + str47;
                            }
                        }
                        str64 = base.GetWTListPC(str5, str65.Replace("|", ","), str40, ref isChange);
                    }
                }
                str67 = "";
                if (str27.Equals("1") && str28.Equals("1"))
                {
                    str68 = str23.Split(new char[] { ',' })[0];
                    str69 = str23.Split(new char[] { ',' })[1];
                    str70 = "";
                    str71 = "";
                    str70 = str53.Split(new char[] { ',' })[0];
                    str71 = str70;
                    if (str53.Split(new char[] { ',' }).Length > 1)
                    {
                        str71 = str53.Split(new char[] { ',' })[1];
                    }
                    str67 = (Convert.ToDouble(str68) + Convert.ToDouble(str70)) + "," + (Convert.ToDouble(str69) + Convert.ToDouble(str71));
                    str36 = (Convert.ToDouble(str68) + Convert.ToDouble(str70)) + "," + (Convert.ToDouble(str69) + Convert.ToDouble(str71));
                    base.GetOdds_SIX(str5, str23, table7.Rows[0][str14 + "_diff"].ToString(), ref str67);
                    string[] strArray14 = str67.Split(new char[] { ',' });
                    if (((double.Parse(s.Split(new char[] { ',' })[0]) != double.Parse(strArray14[0])) || (double.Parse(s.Split(new char[] { ',' })[1]) != double.Parse(strArray14[1]))) || (!string.IsNullOrEmpty(str64) && isChange))
                    {
                        base.DeleteCreditLock(str);
                        list3 = new List<string> { "1" };
                        list4 = new List<string> {
                            str67
                        };
                        result.set_tipinfo(string.Format("賠率有變動,請確認後再提交!", new object[0]));
                        result.set_success(600);
                        dictionary.Add("JeuValidate", base.get_JeuValidate());
                        dictionary.Add("index", list);
                        dictionary.Add("newpl", list4);
                        dictionary.Add("newwt", str64);
                        result.set_data(dictionary);
                        strResult = JsonHandle.ObjectToJson(result);
                        return;
                    }
                    playDrawbackValue = base.GetPlayDrawbackValue(str17, str56);
                    if ((playDrawbackValue != 0.0) && (double.Parse(str36.Split(new char[] { ',' })[0]) > playDrawbackValue))
                    {
                        base.DeleteCreditLock(str);
                        result.set_tipinfo(string.Format("賠率錯誤！", new object[0]));
                        result.set_success(400);
                        dictionary.Add("JeuValidate", base.get_JeuValidate());
                        result.set_data(dictionary);
                        strResult = JsonHandle.ObjectToJson(result);
                        return;
                    }
                    double num33 = base.GetPlayDrawbackValue(str17, ratio);
                    if ((num33 != 0.0) && (double.Parse(str36.Split(new char[] { ',' })[1]) > num33))
                    {
                        base.DeleteCreditLock(str);
                        result.set_tipinfo(string.Format("賠率錯誤！", new object[0]));
                        result.set_success(400);
                        dictionary.Add("JeuValidate", base.get_JeuValidate());
                        result.set_data(dictionary);
                        strResult = JsonHandle.ObjectToJson(result);
                        return;
                    }
                }
                else if (str27.Equals("1") && str28.Equals("0"))
                {
                    str68 = str23;
                    str70 = str53;
                    num58 = Convert.ToDouble(str68) + Convert.ToDouble(str70);
                    str67 = num58.ToString();
                    num58 = Convert.ToDouble(str68) + Convert.ToDouble(str70);
                    str36 = num58.ToString();
                    base.GetOdds_SIX(str5, str23, table7.Rows[0][str14 + "_diff"].ToString(), ref str67);
                    if ((double.Parse(s) != double.Parse(str67)) || (!string.IsNullOrEmpty(str64) && isChange))
                    {
                        base.DeleteCreditLock(str);
                        list3 = new List<string> { "1" };
                        list4 = new List<string> {
                            str67
                        };
                        result.set_tipinfo(string.Format("賠率有變動,請確認後再提交!", new object[0]));
                        result.set_success(600);
                        dictionary.Add("JeuValidate", base.get_JeuValidate());
                        dictionary.Add("index", list);
                        dictionary.Add("newpl", list4);
                        dictionary.Add("newwt", str64);
                        result.set_data(dictionary);
                        strResult = JsonHandle.ObjectToJson(result);
                        return;
                    }
                    playDrawbackValue = base.GetPlayDrawbackValue(str17, str56);
                    if ((playDrawbackValue != 0.0) && (double.Parse(str36) > playDrawbackValue))
                    {
                        base.DeleteCreditLock(str);
                        result.set_tipinfo(string.Format("賠率錯誤！", new object[0]));
                        result.set_success(400);
                        dictionary.Add("JeuValidate", base.get_JeuValidate());
                        result.set_data(dictionary);
                        strResult = JsonHandle.ObjectToJson(result);
                        return;
                    }
                }
                else
                {
                    base.DeleteCreditLock(str);
                    context.Response.End();
                }
                string str72 = base.GetLmGroup()[num28 + "_" + 100];
                int num34 = int.Parse(str72.Split(new char[] { ',' })[1]);
                if (str34.Split(new char[] { '~' }).Length > num34)
                {
                    base.DeleteCreditLock(str);
                    result.set_tipinfo(string.Format("您選擇的號碼組合超過了最大{0}組，請重新選擇號碼！", num34));
                    result.set_success(400);
                    dictionary.Add("JeuValidate", base.get_JeuValidate());
                    result.set_data(dictionary);
                    strResult = JsonHandle.ObjectToJson(result);
                }
                else
                {
                    string str76;
                    string str77;
                    DataTable myDT = null;
                    switch (str5)
                    {
                        case "92565":
                            myDT = CallBLL.cz_wt_6xyz_six_bll.GetWT();
                            break;

                        case "92286":
                            myDT = CallBLL.cz_wt_3z2_six_bll.GetWT();
                            break;

                        case "92285":
                            myDT = CallBLL.cz_wt_3qz_six_bll.GetWT();
                            break;

                        case "92287":
                            myDT = CallBLL.cz_wt_2qz_six_bll.GetWT();
                            break;

                        case "92288":
                            myDT = CallBLL.cz_wt_2zt_six_bll.GetWT();
                            break;

                        case "92289":
                            myDT = CallBLL.cz_wt_tc_six_bll.GetWT();
                            break;

                        case "92575":
                            myDT = CallBLL.cz_wt_4z1_six_bll.GetWT();
                            break;

                        case "92638":
                            myDT = CallBLL.cz_wt_3qz_b_six_bll.GetWT();
                            break;

                        case "92639":
                            myDT = CallBLL.cz_wt_3z2_b_six_bll.GetWT();
                            break;

                        case "92640":
                            myDT = CallBLL.cz_wt_2qz_b_six_bll.GetWT();
                            break;

                        case "92641":
                            myDT = CallBLL.cz_wt_2zt_b_six_bll.GetWT();
                            break;

                        case "92642":
                            myDT = CallBLL.cz_wt_tc_b_six_bll.GetWT();
                            break;

                        case "92643":
                            myDT = CallBLL.cz_wt_4z1_b_six_bll.GetWT();
                            break;

                        case "92566":
                            myDT = CallBLL.cz_wt_sxlwsl_six_bll.GetWT();
                            break;

                        case "92567":
                            myDT = CallBLL.cz_wt_sxlwsl_six_bll.GetWT();
                            break;

                        case "92568":
                            myDT = CallBLL.cz_wt_sxlwsl_six_bll.GetWT();
                            break;

                        case "92636":
                            myDT = CallBLL.cz_wt_sxlwsl_six_bll.GetWT();
                            break;

                        case "92569":
                            myDT = CallBLL.cz_wt_sxlwsl_six_bll.GetWT();
                            break;

                        case "92570":
                            myDT = CallBLL.cz_wt_sxlwsl_six_bll.GetWT();
                            break;

                        case "92571":
                            myDT = CallBLL.cz_wt_sxlwsl_six_bll.GetWT();
                            break;

                        case "92637":
                            myDT = CallBLL.cz_wt_sxlwsl_six_bll.GetWT();
                            break;

                        case "92572":
                            myDT = CallBLL.cz_wt_5bz_six_bll.GetWT();
                            break;

                        case "92588":
                            myDT = CallBLL.cz_wt_6bz_six_bll.GetWT();
                            break;

                        case "92589":
                            myDT = CallBLL.cz_wt_7bz_six_bll.GetWT();
                            break;

                        case "92590":
                            myDT = CallBLL.cz_wt_8bz_six_bll.GetWT();
                            break;

                        case "92591":
                            myDT = CallBLL.cz_wt_9bz_six_bll.GetWT();
                            break;

                        case "92592":
                            myDT = CallBLL.cz_wt_10bz_six_bll.GetWT();
                            break;
                    }
                    string[] strArray15 = str34.Split(new char[] { '~' });
                    string str73 = "";
                    string str74 = "";
                    ArrayList list5 = new ArrayList();
                    ArrayList list6 = new ArrayList();
                    if ((((str5.Equals("92566") || str5.Equals("92567")) || (str5.Equals("92568") || str5.Equals("92569"))) || ((str5.Equals("92570") || str5.Equals("92571")) || str5.Equals("92636"))) || str5.Equals("92637"))
                    {
                        foreach (string str75 in strArray15)
                        {
                            string[] strArray16;
                            string[] strArray17;
                            str76 = "";
                            str77 = "";
                            if (((str5.Equals("92566") || str5.Equals("92567")) || str5.Equals("92568")) || str5.Equals("92636"))
                            {
                                string str78 = base.get_YearLianID();
                                if (str75.IndexOf(str78.Trim()) >= 0)
                                {
                                    strArray16 = str67.Split(new char[] { ',' });
                                    str76 = str78 + "|" + strArray16[1];
                                    strArray17 = str36.Split(new char[] { ',' });
                                    str77 = str78 + "|" + strArray17[1];
                                }
                                else
                                {
                                    str76 = get_sxws_min_pl(myDT, str75, str67, str5);
                                    str77 = get_sxws_min_pl(myDT, str75, str36, str5);
                                    if (str76 == "err")
                                    {
                                        base.DeleteCreditLock(str);
                                        result.set_tipinfo(string.Format("下註有誤,請重新投註！", new object[0]));
                                        result.set_success(400);
                                        result.set_data(dictionary);
                                        strResult = JsonHandle.ObjectToJson(result);
                                        return;
                                    }
                                }
                            }
                            else if (str75.IndexOf("00") >= 0)
                            {
                                strArray16 = str67.Split(new char[] { ',' });
                                str76 = 10 + "|" + strArray16[1];
                                strArray17 = str36.Split(new char[] { ',' });
                                str77 = 10 + "|" + strArray17[1];
                            }
                            else
                            {
                                str76 = get_sxws_min_pl(myDT, str75, str67, str5);
                                str77 = get_sxws_min_pl(myDT, str75, str36, str5);
                                if (str76 == "err")
                                {
                                    base.DeleteCreditLock(str);
                                    result.set_tipinfo(string.Format("下註有誤,請重新投註！", new object[0]));
                                    result.set_success(400);
                                    result.set_data(dictionary);
                                    strResult = JsonHandle.ObjectToJson(result);
                                    return;
                                }
                            }
                            list5.Add(str76);
                            list6.Add(str77);
                        }
                        str73 = string.Join("~", list5.ToArray());
                        str74 = string.Join("~", list6.ToArray());
                    }
                    else
                    {
                        foreach (string str75 in strArray15)
                        {
                            str76 = get_ball_min_pl(myDT, str75, str67);
                            str77 = get_ball_min_pl(myDT, str75, str36);
                            if (str76 == "err")
                            {
                                base.DeleteCreditLock(str);
                                result.set_tipinfo(string.Format("下註有誤,請重新投註！", new object[0]));
                                result.set_success(400);
                                dictionary.Add("JeuValidate", base.get_JeuValidate());
                                result.set_data(dictionary);
                                strResult = JsonHandle.ObjectToJson(result);
                                return;
                            }
                            list5.Add(str76);
                            list6.Add(str77);
                        }
                        str73 = string.Join("~", list5.ToArray());
                        str74 = string.Join("~", list6.ToArray());
                    }
                    string orderNumber = Utils.GetOrderNumber();
                    str34 = str34.Replace("00", "10");
                    _six2 = new cz_bet_six();
                    _six2.set_order_num(orderNumber);
                    _six2.set_checkcode(str11);
                    _six2.set_u_name(getUserModelInfo.get_u_name());
                    _six2.set_u_nicker(getUserModelInfo.get_u_nicker());
                    _six2.set_phase_id(new int?(int.Parse(str13)));
                    _six2.set_phase(str12);
                    _six2.set_bet_time(new DateTime?(DateTime.Now));
                    _six2.set_odds_id(new int?(int.Parse(str5)));
                    _six2.set_category(table7.Rows[0]["category"].ToString());
                    _six2.set_play_id(new int?(int.Parse(table7.Rows[0]["play_id"].ToString())));
                    _six2.set_play_name(str54);
                    _six2.set_bet_val(str41.Replace("00", "10"));
                    _six2.set_bet_wt(str73);
                    _six2.set_bet_group(str34);
                    _six2.set_odds(str67);
                    _six2.set_amount(new decimal?(decimal.Parse(num29.ToString())));
                    _six2.set_profit(0);
                    _six2.set_unit_cnt(new int?(num22));
                    _six2.set_lm_type(new int?(int.Parse(str38)));
                    _six2.set_hy_drawback(new decimal?(decimal.Parse(str21)));
                    _six2.set_dl_drawback(new decimal?(decimal.Parse(str20)));
                    _six2.set_zd_drawback(new decimal?(decimal.Parse(str19)));
                    _six2.set_gd_drawback(new decimal?(decimal.Parse(str18)));
                    _six2.set_fgs_drawback(new decimal?(decimal.Parse(str17)));
                    _six2.set_zj_drawback(new decimal?(decimal.Parse(str16)));
                    _six2.set_dl_rate((float) int.Parse(rate.get_dlzc()));
                    _six2.set_zd_rate((float) int.Parse(rate.get_zdzc()));
                    _six2.set_gd_rate((float) int.Parse(rate.get_gdzc()));
                    _six2.set_fgs_rate(float.Parse(num2.ToString()));
                    _six2.set_zj_rate(float.Parse(num.ToString()));
                    _six2.set_is_payment(0);
                    _six2.set_dl_name(rate.get_dlname());
                    _six2.set_zd_name(rate.get_zdname());
                    _six2.set_gd_name(rate.get_gdname());
                    _six2.set_fgs_name(rate.get_fgsname());
                    _six2.set_m_type(new int?(num4));
                    _six2.set_kind(str14);
                    _six2.set_ip(LSRequest.GetIP());
                    _six2.set_lottery_type(new int?(num5));
                    _six2.set_lottery_name(base.GetGameNameByID(_six2.get_lottery_type().ToString()));
                    _six2.set_isLM(1);
                    _six2.set_ordervalidcode(Utils.GetOrderValidCode(_six2.get_u_name(), _six2.get_order_num(), _six2.get_bet_val(), _six2.get_odds(), _six2.get_kind(), Convert.ToInt32(_six2.get_phase_id()), Convert.ToInt32(_six2.get_odds_id()), Convert.ToDouble(_six2.get_amount())));
                    _six2.set_odds_zj(str36);
                    _six2.set_bet_wt_zj(str74);
                    num35 = 0;
                    if (!(CallBLL.cz_bet_six_bll.AddBet(_six2, decimal.Parse(num29.ToString()), str, ref num35) && (num35 > 0)))
                    {
                        base.DeleteCreditLock(str);
                        result.set_tipinfo(string.Format("系統錯誤！", new object[0]));
                        result.set_success(400);
                        dictionary.Add("JeuValidate", base.get_JeuValidate());
                        result.set_data(dictionary);
                        strResult = JsonHandle.ObjectToJson(result);
                    }
                    else
                    {
                        cz_splitgroup_six _six3;
                        double num36 = double.Parse(str7);
                        double num37 = 0.0;
                        double num38 = 0.0;
                        string[] strArray18 = str67.Split(new char[] { ',' });
                        num37 = double.Parse(strArray18[0].ToString());
                        if (strArray18.Length > 1)
                        {
                            num38 = double.Parse(strArray18[1].ToString());
                        }
                        string[] strArray19 = str34.Split(new char[] { '~' });
                        string[] strArray20 = str73.Split(new char[] { '~' });
                        double num39 = 0.0;
                        double num40 = 0.0;
                        string[] strArray21 = str36.Split(new char[] { ',' });
                        num39 = double.Parse(strArray21[0].ToString());
                        if (strArray21.Length > 1)
                        {
                            num40 = double.Parse(strArray21[1].ToString());
                        }
                        string[] strArray22 = str74.Split(new char[] { '~' });
                        string str80 = "";
                        if (str5.Equals("92565"))
                        {
                            string str81 = px(str41.ToString().Trim());
                            _six3 = new cz_splitgroup_six();
                            _six3.set_order_num(orderNumber);
                            if (strArray20.Length > 0)
                            {
                                if (!string.IsNullOrEmpty(strArray20[0].Trim()))
                                {
                                    _six3.set_odds1(new decimal?(decimal.Parse(strArray20[0].Split(new char[] { '|' })[1])));
                                }
                                else
                                {
                                    _six3.set_odds1(new decimal?(decimal.Parse(num37.ToString())));
                                }
                            }
                            else
                            {
                                _six3.set_odds1(new decimal?(decimal.Parse(num37.ToString())));
                            }
                            _six3.set_odds2(new decimal?(decimal.Parse(num38.ToString())));
                            _six3.set_odds_id(new int?(int.Parse(str5)));
                            _six3.set_item(str81);
                            _six3.set_item_money(new decimal?(decimal.Parse(str7)));
                            if (strArray22.Length > 0)
                            {
                                if (!string.IsNullOrEmpty(strArray22[0].Trim()))
                                {
                                    _six3.set_odds1_zj(new decimal?(decimal.Parse(strArray22[0].Split(new char[] { '|' })[1])));
                                }
                                else
                                {
                                    _six3.set_odds1_zj(new decimal?(decimal.Parse(num39.ToString())));
                                }
                            }
                            else
                            {
                                _six3.set_odds1_zj(new decimal?(decimal.Parse(num39.ToString())));
                            }
                            _six3.set_odds2_zj(new decimal?(decimal.Parse(num40.ToString())));
                            int num41 = CallBLL.cz_splitgroup_six_bll.Add(_six3);
                        }
                        else
                        {
                            int num42 = 0;
                            StringBuilder builder = new StringBuilder();
                            foreach (string str82 in strArray19)
                            {
                                string str83;
                                string str84;
                                string str85;
                                string str86;
                                string str87;
                                string str88;
                                str80 = px(str82);
                                if ((((str5.Equals("92566") || str5.Equals("92567")) || (str5.Equals("92568") || str5.Equals("92569"))) || ((str5.Equals("92570") || str5.Equals("92571")) || str5.Equals("92636"))) || str5.Equals("92637"))
                                {
                                    str83 = "";
                                    str84 = "0";
                                    str85 = strArray20[num42].Trim();
                                    if (str85 == "")
                                    {
                                        str83 = num37.ToString();
                                    }
                                    else
                                    {
                                        str83 = str85.Split(new char[] { '|' })[1];
                                    }
                                    str86 = "";
                                    str87 = "0";
                                    str88 = strArray22[num42].Trim();
                                    if (str88 == "")
                                    {
                                        str86 = num39.ToString();
                                    }
                                    else
                                    {
                                        str86 = str88.Split(new char[] { '|' })[1];
                                    }
                                    _six3 = new cz_splitgroup_six();
                                    _six3.set_order_num(orderNumber);
                                    _six3.set_odds1(new decimal?(decimal.Parse(str83.ToString())));
                                    _six3.set_odds2(new decimal?(decimal.Parse(str84.ToString())));
                                    _six3.set_odds_id(new int?(int.Parse(str5)));
                                    _six3.set_item(str80);
                                    _six3.set_item_money(new decimal?(decimal.Parse(num36.ToString())));
                                    _six3.set_odds1_zj(new decimal?(decimal.Parse(str86.ToString())));
                                    _six3.set_odds2_zj(new decimal?(decimal.Parse(str87.ToString())));
                                    builder.Append("insert into cz_splitgroup_six(");
                                    builder.Append("item,odds_id,item_money,odds1,odds2,order_num,odds1_zj,odds2_zj)");
                                    builder.Append(" values (");
                                    builder.AppendFormat("'{0}',{1},{2},{3},{4},'{5}',{6},{7});", new object[] { _six3.get_item(), _six3.get_odds_id(), _six3.get_item_money(), _six3.get_odds1(), _six3.get_odds2(), orderNumber, _six3.get_odds1_zj(), _six3.get_odds2_zj() });
                                }
                                else
                                {
                                    string[] strArray24;
                                    str83 = "";
                                    str84 = "0";
                                    str85 = strArray20[num42].Trim();
                                    if (str85 == "")
                                    {
                                        str83 = num37.ToString();
                                        str84 = num38.ToString();
                                    }
                                    else
                                    {
                                        string str89 = str85.Split(new char[] { '|' })[1];
                                        strArray24 = str89.Split(new char[] { ',' });
                                        str83 = strArray24[0];
                                        if (strArray24.Length > 1)
                                        {
                                            str84 = strArray24[1];
                                        }
                                    }
                                    str86 = "";
                                    str87 = "0";
                                    str88 = strArray22[num42].Trim();
                                    if (str88 == "")
                                    {
                                        str86 = num39.ToString();
                                        str87 = num40.ToString();
                                    }
                                    else
                                    {
                                        strArray24 = str88.Split(new char[] { '|' })[1].Split(new char[] { ',' });
                                        str86 = strArray24[0];
                                        if (strArray24.Length > 1)
                                        {
                                            str87 = strArray24[1];
                                        }
                                    }
                                    _six3 = new cz_splitgroup_six();
                                    _six3.set_order_num(orderNumber);
                                    _six3.set_odds1(new decimal?(decimal.Parse(str83.ToString())));
                                    _six3.set_odds2(new decimal?(decimal.Parse(str84.ToString())));
                                    _six3.set_odds_id(new int?(int.Parse(str5)));
                                    _six3.set_item(str80);
                                    _six3.set_item_money(new decimal?(decimal.Parse(num36.ToString())));
                                    _six3.set_odds1_zj(new decimal?(decimal.Parse(str86.ToString())));
                                    _six3.set_odds2_zj(new decimal?(decimal.Parse(str87.ToString())));
                                    builder.Append("insert into cz_splitgroup_six(");
                                    builder.Append("item,odds_id,item_money,odds1,odds2,order_num,odds1_zj,odds2_zj)");
                                    builder.Append(" values (");
                                    builder.AppendFormat("'{0}',{1},{2},{3},{4},'{5}',{6},{7});", new object[] { _six3.get_item(), _six3.get_odds_id(), _six3.get_item_money(), _six3.get_odds1(), _six3.get_odds2(), orderNumber, _six3.get_odds1_zj(), _six3.get_odds2_zj() });
                                }
                                num42++;
                            }
                            SqlParameter[] parameterArray = new SqlParameter[] { new SqlParameter("@betString", ""), new SqlParameter("@groupString", builder.ToString()) };
                            DbHelperSQL_ADMIN.RunProcedure("ProcInserSixLMBet", parameterArray);
                        }
                        systemSet = CallBLL.cz_system_set_six_bll.GetSystemSet(100);
                        string str90 = "92285,92286,92287,92288,92289,92575,92638,92639,92640,92641,92642,92643";
                        if ((str90.IndexOf(str5) < 0) || systemSet.get_single_number_isdown().Equals(0))
                        {
                            num43 = (double.Parse(num29.ToString()) * num) / 100.0;
                            CallBLL.cz_odds_six_bll.UpdateGrandTotal(Convert.ToDecimal(num43), int.Parse(str5));
                            num44 = double.Parse(table7.Rows[0]["grand_total"].ToString()) + num43;
                            num45 = double.Parse(table7.Rows[0]["downbase"].ToString());
                            num46 = Math.Floor((double) (num44 / num45));
                            if (num46 >= 1.0)
                            {
                                num47 = double.Parse(table7.Rows[0]["down_odds_rate"].ToString()) * num46;
                                num48 = num44 - (num45 * num46);
                                str58 = "";
                                str59 = "";
                                if ((base.IsShowLM_B() == 1) && (("92285,92286,92287,92288,92289,92575".IndexOf(str5) > -1) || ("92638,92639,92640,92641,92642,92643".IndexOf(str5) > -1)))
                                {
                                    Utils.Six_LM_AB(str5, ref str58, ref str59);
                                }
                                if (str28.Equals("1"))
                                {
                                    if (!((CallBLL.cz_odds_six_bll.UpdateGrandTotalCurrentOdds_DoubleOdds(num47.ToString(), num48.ToString(), str5) <= 0) || string.IsNullOrEmpty(str58)))
                                    {
                                        CallBLL.cz_odds_six_bll.UpdateGrandTotalCurrentOdds_DoubleOddsLM_AB(num47.ToString(), str58);
                                    }
                                }
                                else if (!((CallBLL.cz_odds_six_bll.UpdateGrandTotalCurrentOdds(num47.ToString(), num48.ToString(), str5) <= 0) || string.IsNullOrEmpty(str58)))
                                {
                                    CallBLL.cz_odds_six_bll.UpdateGrandTotalCurrentOddsLM_AB(num47.ToString(), str58);
                                }
                                _log = new cz_system_log();
                                _log.set_user_name("系統");
                                _log.set_children_name("");
                                _log.set_category(table7.Rows[0]["category"].ToString());
                                _log.set_play_name(str54);
                                _log.set_put_amount(str55);
                                num56 = 100;
                                _log.set_l_name(base.GetGameNameByID(num56.ToString()));
                                _log.set_l_phase(str12);
                                _log.set_action("降賠率");
                                _log.set_odds_id(int.Parse(str5));
                                str91 = table7.Rows[0]["current_odds"].ToString();
                                _log.set_old_val(str91);
                                if (str91.Split(new char[] { ',' }).Length > 1)
                                {
                                    num58 = double.Parse(str91.Split(new char[] { ',' })[0]) - num47;
                                    num58 = double.Parse(str91.Split(new char[] { ',' })[1]) - num47;
                                    _log.set_new_val(num58.ToString() + "," + num58.ToString());
                                }
                                else
                                {
                                    num58 = double.Parse(str91) - num47;
                                    _log.set_new_val(num58.ToString());
                                }
                                _log.set_ip(LSRequest.GetIP());
                                _log.set_add_time(DateTime.Now);
                                _log.set_note("系統自動降賠");
                                _log.set_type_id(Convert.ToInt32((LSEnums.LogTypeID) 0));
                                _log.set_lottery_type(100);
                                CallBLL.cz_system_log_bll.Add(_log);
                                _odds = new cz_jp_odds();
                                _odds.set_add_time(DateTime.Now);
                                _odds.set_odds_id(int.Parse(str5));
                                _odds.set_phase_id(int.Parse(str13));
                                _odds.set_play_name(str54);
                                _odds.set_put_amount(str55);
                                _odds.set_odds(num47.ToString());
                                _odds.set_lottery_type(100);
                                _odds.set_phase(str12);
                                _odds.set_old_odds(str91);
                                if (str91.Split(new char[] { ',' }).Length > 1)
                                {
                                    num58 = double.Parse(str91.Split(new char[] { ',' })[0]) - num47;
                                    num58 = double.Parse(str91.Split(new char[] { ',' })[1]) - num47;
                                    _odds.set_new_odds(num58.ToString() + "," + num58.ToString());
                                }
                                else
                                {
                                    num58 = double.Parse(str91) - num47;
                                    _odds.set_new_odds(num58.ToString());
                                }
                                CallBLL.cz_jp_odds_bll.Add(_odds);
                                if (!string.IsNullOrEmpty(str58))
                                {
                                    DataTable table11 = set8.Tables[0];
                                    string str92 = table11.Rows[0]["o_play_name"].ToString();
                                    string str93 = table11.Rows[0]["put_amount"].ToString();
                                    _log2 = new cz_system_log();
                                    _log2.set_user_name("系統");
                                    _log2.set_children_name("");
                                    _log2.set_category(table11.Rows[0]["category"].ToString());
                                    _log2.set_play_name(str92);
                                    _log2.set_put_amount(str93);
                                    num56 = 100;
                                    _log2.set_l_name(base.GetGameNameByID(num56.ToString()));
                                    _log2.set_l_phase(str12);
                                    _log2.set_action("降賠率");
                                    _log2.set_odds_id(int.Parse(str58));
                                    str94 = table11.Rows[0]["current_odds"].ToString();
                                    _log2.set_old_val(str94);
                                    if (str94.Split(new char[] { ',' }).Length > 1)
                                    {
                                        num58 = double.Parse(str94.Split(new char[] { ',' })[0]) - num47;
                                        num58 = double.Parse(str94.Split(new char[] { ',' })[1]) - num47;
                                        _log2.set_new_val(num58.ToString() + "," + num58.ToString());
                                    }
                                    else
                                    {
                                        num58 = double.Parse(str94) - num47;
                                        _log2.set_new_val(num58.ToString());
                                    }
                                    _log2.set_ip(LSRequest.GetIP());
                                    _log2.set_add_time(DateTime.Now);
                                    _log2.set_note("系統自動降賠(聯動)");
                                    _log2.set_type_id(Convert.ToInt32((LSEnums.LogTypeID) 0));
                                    _log2.set_lottery_type(100);
                                    CallBLL.cz_system_log_bll.Add(_log2);
                                    _odds2 = new cz_jp_odds();
                                    _odds2.set_add_time(DateTime.Now);
                                    _odds2.set_odds_id(int.Parse(str58));
                                    _odds2.set_phase_id(int.Parse(str13));
                                    _odds2.set_play_name(str92);
                                    _odds2.set_put_amount(str93);
                                    _odds2.set_odds(num47.ToString());
                                    _odds2.set_lottery_type(100);
                                    _odds2.set_phase(str12);
                                    _odds2.set_old_odds(str94);
                                    if (str94.Split(new char[] { ',' }).Length > 1)
                                    {
                                        num58 = double.Parse(str94.Split(new char[] { ',' })[0]) - num47;
                                        num58 = double.Parse(str94.Split(new char[] { ',' })[1]) - num47;
                                        _odds2.set_new_odds(num58.ToString() + "," + num58.ToString());
                                    }
                                    else
                                    {
                                        num58 = double.Parse(str94) - num47;
                                        _odds2.set_new_odds(num58.ToString());
                                    }
                                    CallBLL.cz_jp_odds_bll.Add(_odds2);
                                }
                            }
                        }
                        fgsWTTable = null;
                        string str95 = "";
                        string str96 = "";
                        string[] strArray25 = str34.Replace("00", "10").Split(new char[] { '~' });
                        int num51 = 0;
                        foreach (string str62 in strArray25)
                        {
                            if (num51 == 0)
                            {
                                str96 = str96 + "'" + Utils.px(str62) + "'";
                                str95 = str95 + Utils.px(str62);
                            }
                            else
                            {
                                str96 = str96 + ",'" + Utils.px(str62) + "'";
                                str95 = str95 + "~" + Utils.px(str62);
                            }
                            num51++;
                        }
                        if (getUserModelInfo.get_six_op_odds().Equals(1))
                        {
                            fgsWTTable = base.GetFgsWTTable(100);
                        }
                        CallBLL.cz_autosale_six_bll.DLAutoSaleLM(_six2.get_order_num(), str11, getUserModelInfo.get_u_nicker(), str14, table7.Rows[0]["play_id"].ToString(), str5, str16, str17, str18, str19, str20, str21, str13, str12, _six2.get_ip(), num5, _six2.get_lottery_name(), getUserModelInfo, rate, myDT, base.get_YearLianID(), 0, fgsWTTable, str95, str96);
                        if (getUserModelInfo.get_six_op_odds().Equals(1))
                        {
                            fgsWTTable = base.GetFgsWTTable(100);
                        }
                        CallBLL.cz_autosale_six_bll.ZDAutoSaleLM(_six2.get_order_num(), str11, getUserModelInfo.get_u_nicker(), str14, table7.Rows[0]["play_id"].ToString(), str5, str16, str17, str18, str19, str20, str21, str13, str12, _six2.get_ip(), num5, _six2.get_lottery_name(), getUserModelInfo, rate, myDT, base.get_YearLianID(), 0, fgsWTTable, str95, str96);
                        if (getUserModelInfo.get_six_op_odds().Equals(1))
                        {
                            fgsWTTable = base.GetFgsWTTable(100);
                        }
                        CallBLL.cz_autosale_six_bll.GDAutoSaleLM(_six2.get_order_num(), str11, getUserModelInfo.get_u_nicker(), str14, table7.Rows[0]["play_id"].ToString(), str5, str16, str17, str18, str19, str20, str21, str13, str12, _six2.get_ip(), num5, _six2.get_lottery_name(), getUserModelInfo, rate, myDT, base.get_YearLianID(), 0, fgsWTTable, str95, str96);
                        if (getUserModelInfo.get_six_op_odds().Equals(1))
                        {
                            fgsWTTable = base.GetFgsWTTable(100);
                        }
                        CallBLL.cz_autosale_six_bll.FGSAutoSaleLM(_six2.get_order_num(), str11, getUserModelInfo.get_u_nicker(), str14, table7.Rows[0]["play_id"].ToString(), str5, str16, str17, str18, str19, str20, str21, str13, str12, _six2.get_ip(), num5, _six2.get_lottery_name(), getUserModelInfo, rate, myDT, base.get_YearLianID(), 0, fgsWTTable, str95, str96);
                        if (systemSet.get_single_number_isdown().Equals(1) && (str90.IndexOf(str5) > -1))
                        {
                            List<string> values = new List<string>();
                            double num52 = ((((double) _six2.get_amount().Value) / ((double) _six2.get_unit_cnt().Value)) * num) / 100.0;
                            bool flag6 = CallBLL.cz_lm_number_amount_six_bll.SetLMSingleNumber(int.Parse(_six2.get_odds_id().ToString()), _six2.get_bet_group(), num52, ref values);
                            num45 = double.Parse(table7.Rows[0]["downbase"].ToString());
                            num47 = double.Parse(table7.Rows[0]["down_odds_rate"].ToString());
                            string str97 = string.Join(",", values);
                            DataSet set9 = CallBLL.cz_lm_number_amount_six_bll.GetListByNumber(str5, str97, num45);
                            if (set9 != null)
                            {
                                string str105;
                                string category = _six2.get_category();
                                string str99 = _six2.get_play_name();
                                string str100 = str55;
                                string phase = _six2.get_phase();
                                string str102 = _six2.get_phase_id().ToString();
                                DataTable numberAmountTableDT = set9.Tables["numberAmountTable"];
                                DataTable wtTableDT = set9.Tables["wtTable"];
                                DataTable table15 = set9.Tables["oddsTable"];
                                string str103 = table15.Rows[0]["isDoubleOdds"].ToString();
                                string str104 = table15.Rows[0][str14 + "_diff"].ToString();
                                if (str103.Equals("1"))
                                {
                                    str68 = str23.Split(new char[] { ',' })[0];
                                    str69 = str23.Split(new char[] { ',' })[1];
                                    str70 = "";
                                    str71 = "";
                                    str70 = str53.Split(new char[] { ',' })[0];
                                    str71 = str70;
                                    if (str53.Split(new char[] { ',' }).Length > 1)
                                    {
                                        str71 = str53.Split(new char[] { ',' })[1];
                                    }
                                    str105 = (Convert.ToDouble(str68) + Convert.ToDouble(str70)) + "," + (Convert.ToDouble(str69) + Convert.ToDouble(str71));
                                    string str106 = table15.Rows[0]["min_odds"].ToString();
                                    base.GetOdds_SIX(str5, str23, table15.Rows[0][str14 + "_diff"].ToString(), ref str105);
                                    this.SetWtValueDuble(numberAmountTableDT, wtTableDT, str5, str106, str105, num45, num47, category, str99, str100, phase, str102);
                                }
                                else
                                {
                                    string str107 = table15.Rows[0]["current_odds"].ToString();
                                    string str108 = table15.Rows[0]["min_odds"].ToString();
                                    str105 = (Convert.ToDouble(str107) + Convert.ToDouble(str104)).ToString();
                                    base.GetOdds_SIX(str5, str107, table15.Rows[0][str14 + "_diff"].ToString(), ref str105);
                                    if (double.Parse(str108) < double.Parse(str105))
                                    {
                                        this.SetWtValue(numberAmountTableDT, wtTableDT, str5, double.Parse(str108), double.Parse(str105), num45, num47, category, str99, str100, phase, str102);
                                    }
                                }
                            }
                        }
                        base.SetUserRate_six(rate);
                        if (FileCacheHelper.get_GetSixPutMoneyCache() == "1")
                        {
                            base.SetUserDrawback_six(ds, num28.ToString());
                        }
                        else
                        {
                            base.SetUserDrawback_six(ds);
                        }
                        getUserModelInfo.set_begin_six("yes");
                        base.DeleteCreditLock(str);
                        result.set_tipinfo(string.Format("下注成功！", new object[0]));
                        result.set_success(200);
                        dictionary.Add("JeuValidate", base.get_JeuValidate());
                        result.set_data(dictionary);
                        strResult = JsonHandle.ObjectToJson(result);
                    }
                }
            }
        }

        private static double get_ball_min_pl(DataTable myDT, string nos, double pl)
        {
            if (myDT.Rows.Count > 0)
            {
                List<double> list = new List<double>();
                int[] source = Array.ConvertAll<string, int>(nos.Split(new char[] { ',' }), s => int.Parse(s));
                foreach (DataRow row in myDT.Rows)
                {
                    if (source.Contains<int>(int.Parse(row["number"].ToString().Trim())))
                    {
                        list.Add(double.Parse(row["wt_value"].ToString()));
                    }
                }
                if (list.Count > 0)
                {
                    double num = ((IEnumerable<double>) list).Min();
                    return (pl + Convert.ToDouble(num));
                }
                return pl;
            }
            return pl;
        }

        private static string get_ball_min_pl(DataTable myDT, string nos, string pl)
        {
            if (myDT.Rows.Count > 0)
            {
                var selector = null;
                Dictionary<string, double> ddDict = new Dictionary<string, double>();
                int[] source = Array.ConvertAll<string, int>(nos.Split(new char[] { ',' }), s => int.Parse(s));
                foreach (DataRow row in myDT.Rows)
                {
                    if (source.Contains<int>(int.Parse(row["number"].ToString().Trim())))
                    {
                        ddDict.Add(row["number"].ToString().Trim(), double.Parse(row["wt_value"].ToString()));
                    }
                }
                if (ddDict.Count > 0)
                {
                    if (selector == null)
                    {
                        selector = x => new { x = x, y = ddDict[x] };
                    }
                    string str = (from x in ddDict.Keys.Select(selector)
                        orderby x.y
                        select x).First().x;
                    double num = ddDict[str.ToString()];
                    if (num == 0.0)
                    {
                        return "";
                    }
                    string str2 = str.ToString();
                    if (str2.Length == 1)
                    {
                        str2 = "0" + str2;
                    }
                    if (pl.IndexOf(',') > 0)
                    {
                        string[] strArray2 = pl.Split(new char[] { ',' });
                        string str3 = strArray2[0];
                        string str4 = strArray2[1];
                        string str5 = string.Format("{0}", Convert.ToDouble(str3) + num);
                        string str6 = string.Format("{0}", Convert.ToDouble(str4) + num);
                        return (str2 + "|" + str5 + "," + str6);
                    }
                    double num2 = Convert.ToDouble(pl) + num;
                    return (str2 + "|" + num2.ToString());
                }
                return "err";
            }
            return "err";
        }

        private static string get_ball_pl(string playid, double pl, string nos)
        {
            List<string> list = new List<string>();
            DataTable table = CallBLL.cz_wt_kl10_bll.GetWtByPlayID(int.Parse(playid), nos).Tables[0];
            string[] strArray = nos.Split(new char[] { ',' });
            foreach (string str in strArray)
            {
                foreach (DataRow row in table.Rows)
                {
                    if (int.Parse(row["number"].ToString().Trim()) == int.Parse(str.Trim()))
                    {
                        string s = row["wt_value"].ToString();
                        if (double.Parse(s) != 0.0)
                        {
                            list.Add(row["number"].ToString().Trim() + "~" + ((pl + double.Parse(s))).ToString());
                            break;
                        }
                    }
                }
            }
            return string.Join("|", list.ToArray());
        }

        public void get_oddsinfo(HttpContext context, ref string strResult)
        {
            string str = LSRequest.qq("playid");
            string str2 = LSRequest.qq("playpage");
            if (string.IsNullOrEmpty(str))
            {
                context.Response.End();
            }
            else if (context.Session["user_name"] == null)
            {
                context.Response.End();
            }
            else
            {
                string str3 = context.Session["user_name"].ToString();
                cz_userinfo_session getUserModelInfo = base.GetUserModelInfo;
                string str4 = getUserModelInfo.get_su_type().ToString();
                string str5 = getUserModelInfo.get_u_name().ToString();
                string str6 = getUserModelInfo.get_six_kind().Trim();
                ReturnResult result = new ReturnResult();
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                dictionary.Add("type", "get_oddsinfo");
                dictionary.Add("playpage", str2);
                DataTable table = CallBLL.cz_users_bll.GetCredit(str5, str4).Tables[0];
                string str7 = table.Rows[0]["six_credit"].ToString();
                string str8 = table.Rows[0]["six_usable_credit"].ToString();
                dictionary.Add("credit", Convert.ToDouble(str7).ToString());
                int num4 = (int) Convert.ToDouble(str8);
                dictionary.Add("usable_credit", num4.ToString());
                cz_phase_six currentPhase = CallBLL.cz_phase_six_bll.GetCurrentPhase();
                cz_phase_six currentPhaseClose = CallBLL.cz_phase_six_bll.GetCurrentPhaseClose();
                DateTime now = DateTime.Now;
                string str9 = "n";
                string str10 = "0";
                string str11 = "";
                string str13 = "";
                DateTime time2 = Convert.ToDateTime(currentPhase.get_stop_date());
                DateTime time3 = Convert.ToDateTime(currentPhase.get_sn_stop_date());
                TimeSpan span = Utils.DateDiff(time2, now);
                TimeSpan span2 = Utils.DateDiff(time3, now);
                string str14 = string.Concat(new object[] { (span.Days * 0x18) + span.Hours, ":", span.Minutes, ":", span.Seconds });
                string str15 = string.Concat(new object[] { (span2.Days * 0x18) + span2.Hours, ":", span2.Minutes, ":", span2.Seconds });
                if (time2 < now)
                {
                    str13 = str14;
                }
                else
                {
                    str9 = "y";
                    str10 = "1";
                    str13 = str14;
                }
                if (time3 < now)
                {
                    str11 = str15;
                }
                else
                {
                    str9 = "y";
                    str10 = "1";
                    str11 = str15;
                }
                if ((currentPhase != null) && currentPhase.get_is_closed().Equals(0))
                {
                    int num;
                    if (time2 < now)
                    {
                        num = CallBLL.cz_odds_six_bll.UpdateIsOpen(1, "91001,91002,91003,91004,91005,91007,91038");
                    }
                    if (time3 < now)
                    {
                        num = CallBLL.cz_odds_six_bll.UpdateIsOpen(0, "");
                    }
                }
                string str16 = (currentPhase != null) ? currentPhase.get_phase() : "";
                string str17 = (currentPhase != null) ? (num4 = currentPhase.get_p_id()).ToString() : "";
                string str18 = (currentPhaseClose != null) ? currentPhaseClose.get_phase() : "";
                if (str16.Equals(str18))
                {
                    str9 = "n";
                    str10 = "0";
                }
                dictionary.Add("openning", str9);
                dictionary.Add("isopen", str10);
                dictionary.Add("drawopen_time", "");
                string str19 = "";
                string[] strArray = str11.Split(new char[] { ':' });
                str19 = (Convert.ToInt32(strArray[0]) < 10) ? ("0" + strArray[0]) : strArray[0];
                str19 = (str19 + ":" + ((Convert.ToInt32(strArray[1]) < 10) ? ("0" + strArray[1]) : strArray[1])) + ":" + ((Convert.ToInt32(strArray[2]) < 10) ? ("0" + strArray[2]) : strArray[2]);
                dictionary.Add("stop_time", str19);
                dictionary.Add("nn", str16);
                dictionary.Add("p_id", str17);
                dictionary.Add("profit", "");
                DataSet play = CallBLL.cz_play_six_bll.GetPlay(str, str4);
                if (play == null)
                {
                    context.Response.End();
                }
                else
                {
                    DataTable table2 = play.Tables["odds"];
                    if (table2 == null)
                    {
                        context.Response.End();
                    }
                    else
                    {
                        DataSet cache;
                        DataTable table3 = null;
                        DataTable table4 = null;
                        DataTable table5 = null;
                        DataTable table6 = null;
                        DataTable table7 = null;
                        DataTable table8 = null;
                        DataTable wT = null;
                        DataTable table10 = null;
                        DataTable table11 = null;
                        DataTable table12 = null;
                        DataTable table13 = null;
                        DataTable table14 = null;
                        DataTable table15 = null;
                        DataTable table16 = null;
                        DataTable table17 = null;
                        DataTable table18 = null;
                        DataTable table19 = null;
                        DataTable table20 = null;
                        DataTable table21 = null;
                        DataTable table22 = null;
                        string str36 = str2;
                        if (str36 != null)
                        {
                            if (!(str36 == "six_lm"))
                            {
                                if (str36 == "six_lm_b")
                                {
                                    wT = CallBLL.cz_wt_3qz_b_six_bll.GetWT();
                                    table10 = CallBLL.cz_wt_3z2_b_six_bll.GetWT();
                                    table11 = CallBLL.cz_wt_2qz_b_six_bll.GetWT();
                                    table12 = CallBLL.cz_wt_2zt_b_six_bll.GetWT();
                                    table13 = CallBLL.cz_wt_tc_b_six_bll.GetWT();
                                    table14 = CallBLL.cz_wt_4z1_b_six_bll.GetWT();
                                }
                                else if (str36 == "six_bz")
                                {
                                    table15 = CallBLL.cz_wt_5bz_six_bll.GetWT();
                                    table16 = CallBLL.cz_wt_6bz_six_bll.GetWT();
                                    table17 = CallBLL.cz_wt_7bz_six_bll.GetWT();
                                    table18 = CallBLL.cz_wt_8bz_six_bll.GetWT();
                                    table19 = CallBLL.cz_wt_9bz_six_bll.GetWT();
                                    table20 = CallBLL.cz_wt_10bz_six_bll.GetWT();
                                }
                                else if (str36 == "six_lx")
                                {
                                    table21 = CallBLL.cz_wt_sxlwsl_six_bll.GetWT();
                                    table22 = CallBLL.cz_wt_6xyz_six_bll.GetWT();
                                }
                                else if (str36 == "six_ws")
                                {
                                    table21 = CallBLL.cz_wt_sxlwsl_six_bll.GetWT();
                                }
                            }
                            else
                            {
                                table3 = CallBLL.cz_wt_3qz_six_bll.GetWT();
                                table4 = CallBLL.cz_wt_3z2_six_bll.GetWT();
                                table5 = CallBLL.cz_wt_2qz_six_bll.GetWT();
                                table6 = CallBLL.cz_wt_2zt_six_bll.GetWT();
                                table7 = CallBLL.cz_wt_tc_six_bll.GetWT();
                                table8 = CallBLL.cz_wt_4z1_six_bll.GetWT();
                            }
                        }
                        DataTable drawbackByPlayIds = null;
                        if (FileCacheHelper.get_GetSixPutMoneyCache() == "1")
                        {
                            foreach (string str20 in str.Split(new char[] { ',' }))
                            {
                                DataTable table24;
                                if (CacheHelper.GetCache("six_drawback_FileCacheKey" + str20 + context.Session["user_name"].ToString()) != null)
                                {
                                    cache = CacheHelper.GetCache("six_drawback_FileCacheKey" + str20 + context.Session["user_name"].ToString()) as DataSet;
                                    if (drawbackByPlayIds == null)
                                    {
                                        drawbackByPlayIds = cache.Tables[0];
                                    }
                                    else
                                    {
                                        table24 = drawbackByPlayIds.Clone();
                                        table24 = cache.Tables[0];
                                        if (table24 != null)
                                        {
                                            drawbackByPlayIds.Merge(table24);
                                        }
                                    }
                                }
                                else if (drawbackByPlayIds == null)
                                {
                                    drawbackByPlayIds = CallBLL.cz_drawback_six_bll.GetDrawbackByPlayIds(str20, getUserModelInfo.get_u_name());
                                }
                                else
                                {
                                    table24 = drawbackByPlayIds.Clone();
                                    table24 = CallBLL.cz_drawback_six_bll.GetDrawbackByPlayIds(str20, getUserModelInfo.get_u_name());
                                    if (table24 != null)
                                    {
                                        drawbackByPlayIds.Merge(table24);
                                    }
                                }
                            }
                        }
                        else if (CacheHelper.GetCache("six_drawback_FileCacheKey" + context.Session["user_name"].ToString()) != null)
                        {
                            cache = CacheHelper.GetCache("six_drawback_FileCacheKey" + context.Session["user_name"].ToString()) as DataSet;
                            drawbackByPlayIds = cache.Tables[0];
                        }
                        else
                        {
                            drawbackByPlayIds = CallBLL.cz_drawback_six_bll.GetDrawbackByPlayIds(str, getUserModelInfo.get_u_name());
                        }
                        Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
                        foreach (DataRow row in table2.Rows)
                        {
                            string str35;
                            DataRow[] rowArray2;
                            string key = "";
                            string pl = "";
                            string s = "";
                            string str24 = "";
                            string str25 = "";
                            string str26 = "";
                            string str27 = "";
                            List<double> source = new List<double>();
                            Dictionary<string, object> dictionary3 = new Dictionary<string, object>();
                            key = row["play_id"].ToString() + "_" + row["odds_id"].ToString();
                            string str28 = row["current_odds"].ToString();
                            string str29 = row[str6 + "_diff"].ToString().Trim();
                            if (!row["is_open"].ToString().Equals("0"))
                            {
                                try
                                {
                                    string[] strArray3 = row["current_odds"].ToString().Split(new char[] { ',' });
                                    string[] strArray4 = row[str6 + "_diff"].ToString().Trim().Split(new char[] { ',' });
                                    base.GetOdds_SIX(row["odds_id"].ToString(), row["current_odds"].ToString(), row[str6 + "_diff"].ToString(), ref pl);
                                }
                                catch (Exception exception)
                                {
                                    string message = exception.Message;
                                }
                            }
                            else if (row["current_odds"].ToString().Split(new char[] { ',' }).Length > 1)
                            {
                                pl = "-,-";
                            }
                            else
                            {
                                pl = "-";
                            }
                            DataRow[] rowArray = drawbackByPlayIds.Select(string.Format(" play_id={0} and u_name='{1}' ", row["play_id"].ToString(), getUserModelInfo.get_u_name()));
                            string str31 = rowArray[0]["single_phase_amount"].ToString();
                            string str32 = rowArray[0]["single_max_amount"].ToString();
                            string str33 = rowArray[0]["single_min_amount"].ToString();
                            s = row["allow_min_amount"].ToString();
                            str24 = row["allow_max_amount"].ToString();
                            str25 = row["max_appoint"].ToString();
                            str26 = row["total_amount"].ToString();
                            str27 = row["allow_max_put_amount"].ToString();
                            if (Convert.ToDecimal(str24) > Convert.ToDecimal(str27))
                            {
                                str24 = row["allow_max_put_amount"].ToString();
                            }
                            if (Convert.ToDecimal(str24) > Convert.ToDecimal(str26))
                            {
                                str24 = row["total_amount"].ToString();
                            }
                            if (double.Parse(s) < double.Parse(str33))
                            {
                                s = str33;
                            }
                            if (double.Parse(str24) > double.Parse(str32))
                            {
                                str24 = str32;
                            }
                            if ((str2.Equals("six_lm") || str2.Equals("six_lm_b")) || str2.Equals("six_bz"))
                            {
                                switch (row["play_id"].ToString())
                                {
                                    case "91016":
                                        foreach (DataRow row2 in table3.Rows)
                                        {
                                            source.Add(double.Parse(row2["wt_value"].ToString()));
                                        }
                                        if (source.Count<double>(p => (p == 0.0)) == source.Count)
                                        {
                                            source.Clear();
                                        }
                                        break;

                                    case "91017":
                                        foreach (DataRow row2 in table4.Rows)
                                        {
                                            source.Add(double.Parse(row2["wt_value"].ToString()));
                                        }
                                        if (source.Count<double>(p => (p == 0.0)) == source.Count)
                                        {
                                            source.Clear();
                                        }
                                        break;

                                    case "91018":
                                        foreach (DataRow row2 in table5.Rows)
                                        {
                                            source.Add(double.Parse(row2["wt_value"].ToString()));
                                        }
                                        if (source.Count<double>(p => (p == 0.0)) == source.Count)
                                        {
                                            source.Clear();
                                        }
                                        break;

                                    case "91019":
                                        foreach (DataRow row2 in table6.Rows)
                                        {
                                            source.Add(double.Parse(row2["wt_value"].ToString()));
                                        }
                                        if (source.Count<double>(p => (p == 0.0)) == source.Count)
                                        {
                                            source.Clear();
                                        }
                                        break;

                                    case "91020":
                                        foreach (DataRow row2 in table7.Rows)
                                        {
                                            source.Add(double.Parse(row2["wt_value"].ToString()));
                                        }
                                        if (source.Count<double>(p => (p == 0.0)) == source.Count)
                                        {
                                            source.Clear();
                                        }
                                        break;

                                    case "91040":
                                        foreach (DataRow row2 in table8.Rows)
                                        {
                                            source.Add(double.Parse(row2["wt_value"].ToString()));
                                        }
                                        if (source.Count<double>(p => (p == 0.0)) == source.Count)
                                        {
                                            source.Clear();
                                        }
                                        break;

                                    case "91060":
                                        foreach (DataRow row2 in wT.Rows)
                                        {
                                            source.Add(double.Parse(row2["wt_value"].ToString()));
                                        }
                                        if (source.Count<double>(p => (p == 0.0)) == source.Count)
                                        {
                                            source.Clear();
                                        }
                                        break;

                                    case "91061":
                                        foreach (DataRow row2 in table10.Rows)
                                        {
                                            source.Add(double.Parse(row2["wt_value"].ToString()));
                                        }
                                        if (source.Count<double>(p => (p == 0.0)) == source.Count)
                                        {
                                            source.Clear();
                                        }
                                        break;

                                    case "91062":
                                        foreach (DataRow row2 in table11.Rows)
                                        {
                                            source.Add(double.Parse(row2["wt_value"].ToString()));
                                        }
                                        if (source.Count<double>(p => (p == 0.0)) == source.Count)
                                        {
                                            source.Clear();
                                        }
                                        break;

                                    case "91063":
                                        foreach (DataRow row2 in table12.Rows)
                                        {
                                            source.Add(double.Parse(row2["wt_value"].ToString()));
                                        }
                                        if (source.Count<double>(p => (p == 0.0)) == source.Count)
                                        {
                                            source.Clear();
                                        }
                                        break;

                                    case "91064":
                                        foreach (DataRow row2 in table13.Rows)
                                        {
                                            source.Add(double.Parse(row2["wt_value"].ToString()));
                                        }
                                        if (source.Count<double>(p => (p == 0.0)) == source.Count)
                                        {
                                            source.Clear();
                                        }
                                        break;

                                    case "91065":
                                        foreach (DataRow row2 in table14.Rows)
                                        {
                                            source.Add(double.Parse(row2["wt_value"].ToString()));
                                        }
                                        if (source.Count<double>(p => (p == 0.0)) == source.Count)
                                        {
                                            source.Clear();
                                        }
                                        break;

                                    case "91037":
                                        foreach (DataRow row2 in table15.Rows)
                                        {
                                            source.Add(double.Parse(row2["wt_value"].ToString()));
                                        }
                                        if (source.Count<double>(p => (p == 0.0)) == source.Count)
                                        {
                                            source.Clear();
                                        }
                                        break;

                                    case "91047":
                                        foreach (DataRow row2 in table16.Rows)
                                        {
                                            source.Add(double.Parse(row2["wt_value"].ToString()));
                                        }
                                        if (source.Count<double>(p => (p == 0.0)) == source.Count)
                                        {
                                            source.Clear();
                                        }
                                        break;

                                    case "91048":
                                        foreach (DataRow row2 in table17.Rows)
                                        {
                                            source.Add(double.Parse(row2["wt_value"].ToString()));
                                        }
                                        if (source.Count<double>(p => (p == 0.0)) == source.Count)
                                        {
                                            source.Clear();
                                        }
                                        break;

                                    case "91049":
                                        foreach (DataRow row2 in table18.Rows)
                                        {
                                            source.Add(double.Parse(row2["wt_value"].ToString()));
                                        }
                                        if (source.Count<double>(p => (p == 0.0)) == source.Count)
                                        {
                                            source.Clear();
                                        }
                                        break;

                                    case "91050":
                                        foreach (DataRow row2 in table19.Rows)
                                        {
                                            source.Add(double.Parse(row2["wt_value"].ToString()));
                                        }
                                        if (source.Count<double>(p => (p == 0.0)) == source.Count)
                                        {
                                            source.Clear();
                                        }
                                        break;

                                    case "91051":
                                        foreach (DataRow row2 in table20.Rows)
                                        {
                                            source.Add(double.Parse(row2["wt_value"].ToString()));
                                        }
                                        if (source.Count<double>(p => (p == 0.0)) == source.Count)
                                        {
                                            source.Clear();
                                        }
                                        break;
                                }
                            }
                            if (str2.Equals("six_lx"))
                            {
                                str35 = row["odds_id"].ToString();
                                if (str35.Equals("92565"))
                                {
                                    foreach (DataRow row2 in table22.Rows)
                                    {
                                        source.Add(double.Parse(row2["wt_value"].ToString()));
                                    }
                                    if (source.Count<double>(p => (p == 0.0)) == source.Count)
                                    {
                                        source.Clear();
                                    }
                                }
                                else
                                {
                                    rowArray2 = table21.Select(string.Format(" odds_id={0} ", Convert.ToInt32(str35)));
                                    foreach (DataRow row2 in rowArray2)
                                    {
                                        source.Add(double.Parse(row2["wt_value"].ToString()));
                                    }
                                    if (source.Count<double>(p => (p == 0.0)) == source.Count)
                                    {
                                        source.Clear();
                                    }
                                }
                            }
                            if (str2.Equals("six_ws"))
                            {
                                str35 = row["odds_id"].ToString();
                                rowArray2 = table21.Select(string.Format(" odds_id={0} ", Convert.ToInt32(str35)));
                                foreach (DataRow row2 in rowArray2)
                                {
                                    source.Add(double.Parse(row2["wt_value"].ToString()));
                                }
                                if (source.Count<double>(p => (p == 0.0)) == source.Count)
                                {
                                    source.Clear();
                                }
                            }
                            dictionary3.Add("pl", pl);
                            dictionary3.Add("plx", new List<double>(source));
                            dictionary3.Add("min_amount", Convert.ToDouble(s).ToString());
                            dictionary3.Add("max_amount", Convert.ToDouble(str24).ToString());
                            dictionary3.Add("top_amount", Convert.ToDouble(str25).ToString());
                            dictionary3.Add("dq_max_amount", Convert.ToDouble(str26).ToString());
                            dictionary3.Add("dh_max_amount", Convert.ToDouble(str27).ToString());
                            dictionary2.Add(key, new Dictionary<string, object>(dictionary3));
                        }
                        dictionary.Add("play_odds", dictionary2);
                        result.set_data(dictionary);
                        strResult = JsonHandle.ObjectToJson(result);
                    }
                }
            }
        }

        public void get_openball(HttpContext context, ref string strResult)
        {
            if (context.Session["user_name"] == null)
            {
                context.Response.End();
            }
            else
            {
                string str = context.Session["user_name"].ToString();
                ReturnResult result = new ReturnResult();
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                dictionary.Add("type", "get_openball");
                cz_phase_six currentPhaseClose = CallBLL.cz_phase_six_bll.GetCurrentPhaseClose();
                if (currentPhaseClose != null)
                {
                    string str2 = (currentPhaseClose != null) ? currentPhaseClose.get_phase() : "";
                    string str3 = (currentPhaseClose != null) ? string.Format("{0},{1},{2},{3},{4},{5},{6}", new object[] { currentPhaseClose.get_n1(), currentPhaseClose.get_n2(), currentPhaseClose.get_n3(), currentPhaseClose.get_n4(), currentPhaseClose.get_n5(), currentPhaseClose.get_n6(), currentPhaseClose.get_sn() }) : "";
                    string str4 = (currentPhaseClose != null) ? string.Format("{0},{1},{2},{3},{4},{5},{6}", new object[] { currentPhaseClose.get_zodiac_n1(), currentPhaseClose.get_zodiac_n2(), currentPhaseClose.get_zodiac_n3(), currentPhaseClose.get_zodiac_n4(), currentPhaseClose.get_zodiac_n5(), currentPhaseClose.get_zodiac_n6(), currentPhaseClose.get_zodiac_sn() }) : "";
                    dictionary.Add("draw_phase", str2);
                    dictionary.Add("draw_result", str3.Split(new char[] { ',' }).ToArray<string>());
                    dictionary.Add("upopennumberzodiac", str4.Split(new char[] { ',' }).ToArray<string>());
                    dictionary.Add("phaseinfo", "[]");
                    cz_phase_six currentPhase = CallBLL.cz_phase_six_bll.GetCurrentPhase();
                    DateTime now = DateTime.Now;
                    string str5 = "n";
                    DateTime time2 = Convert.ToDateTime(currentPhase.get_stop_date());
                    DateTime time3 = Convert.ToDateTime(currentPhase.get_sn_stop_date());
                    if (time2 >= now)
                    {
                        str5 = "y";
                    }
                    if (time3 >= now)
                    {
                        str5 = "y";
                    }
                    string str6 = "";
                    if (!(currentPhase.get_is_closed().ToString().Equals("1") || !str5.Equals("y")))
                    {
                        str6 = string.Format("{0}/index.aspx?lid={1}&path={2}", "L_SIX", 100, "L_SIX");
                    }
                    dictionary.Add("newphase", str6);
                }
                result.set_data(dictionary);
                strResult = JsonHandle.ObjectToJson(result);
            }
        }

        public void get_putinfo(HttpContext context, ref string strResult)
        {
            if (context.Session["user_name"] == null)
            {
                context.Response.End();
            }
            else
            {
                string str = context.Session["user_name"].ToString();
                ReturnResult result = new ReturnResult();
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                dictionary.Add("type", "get_putinfo");
                int num = 5;
                DataSet topBet = CallBLL.cz_bet_six_bll.GetTopBet(num, str);
                if (topBet != null)
                {
                    ArrayList list = new ArrayList();
                    ArrayList list2 = new ArrayList();
                    ArrayList list3 = new ArrayList();
                    ArrayList list4 = new ArrayList();
                    ArrayList list5 = new ArrayList();
                    ArrayList list6 = new ArrayList();
                    ArrayList list7 = new ArrayList();
                    ArrayList list8 = new ArrayList();
                    ArrayList list9 = new ArrayList();
                    ArrayList list10 = new ArrayList();
                    DataTable table = topBet.Tables[0];
                    if (table.Rows.Count > 0)
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            StringBuilder builder;
                            StringBuilder builder2;
                            string str2 = "";
                            string str3 = "";
                            string str4 = "";
                            string str5 = "";
                            string str6 = "";
                            string str7 = "";
                            string str8 = "";
                            string str9 = "";
                            string str10 = "";
                            string str11 = "";
                            if (((("92285,92286,92287,92288,92289,92575".IndexOf(row["odds_id"].ToString()) > -1) || ("92638,92639,92640,92641,92642,92643".IndexOf(row["odds_id"].ToString()) > -1)) || ("92565,92566,92567,92568,92569,92570,92571,92636,92637".IndexOf(row["odds_id"].ToString()) > -1)) || ("92572,92588,92589,92590,92591,92592".IndexOf(row["odds_id"].ToString()) > -1))
                            {
                                Dictionary<string, string> dictionary2;
                                string str12;
                                string[] strArray;
                                int num2;
                                string str13;
                                string str14;
                                string str15;
                                int num3;
                                StringBuilder builder3;
                                string str16;
                                str2 = row["lottery_name"].ToString();
                                str3 = row["order_num"].ToString();
                                str4 = row["phase"].ToString();
                                if (int.Parse(row["unit_cnt"].ToString()) > 0)
                                {
                                    str6 = string.Format("{0:F1}", float.Parse(row["amount"].ToString()) / ((float) int.Parse(row["unit_cnt"].ToString())));
                                }
                                else
                                {
                                    str6 = string.Format("{0:F1}", float.Parse(row["amount"].ToString()));
                                }
                                builder = new StringBuilder();
                                builder.Append("{");
                                builder.AppendFormat("\"play_name\": \"{0}\"", row["play_name"].ToString());
                                if ((("92566".Equals(row["odds_id"].ToString()) || "92567".Equals(row["odds_id"].ToString())) || "92568".Equals(row["odds_id"].ToString())) || "92636".Equals(row["odds_id"].ToString()))
                                {
                                    builder.AppendFormat(",\"bet_val1\": \"{0}\"", FunctionSix.GetSXLNameBySXid(row["bet_val"].ToString().Split(new char[] { '|' })[0].Replace("、", ",")));
                                    builder.AppendFormat(",\"bet_val2\": \"{0}\"", (row["bet_val"].ToString().Split(new char[] { '|' }).Length > 1) ? FunctionSix.GetSXLNameBySXid(row["bet_val"].ToString().Split(new char[] { '|' })[1].Replace("、", ",")) : "");
                                }
                                else if ((("92569".Equals(row["odds_id"].ToString()) || "92570".Equals(row["odds_id"].ToString())) || "92571".Equals(row["odds_id"].ToString())) || "92637".Equals(row["odds_id"].ToString()))
                                {
                                    builder.AppendFormat(",\"bet_val1\": \"{0}\"", FunctionSix.GetWSLNameBySXid(row["bet_val"].ToString().Split(new char[] { '|' })[0].Replace("、", ",")));
                                    builder.AppendFormat(",\"bet_val2\": \"{0}\"", (row["bet_val"].ToString().Split(new char[] { '|' }).Length > 1) ? FunctionSix.GetWSLNameBySXid(row["bet_val"].ToString().Split(new char[] { '|' })[1].Replace("、", ",")) : "");
                                }
                                else if ("92565".Equals(row["odds_id"].ToString()))
                                {
                                    builder.AppendFormat(",\"bet_val1\": \"{0}\"", FunctionSix.GetSXLNameBySXid(row["bet_val"].ToString().Split(new char[] { '|' })[0].Replace("、", ",")));
                                    builder.AppendFormat(",\"bet_val2\": \"{0}\"", (row["bet_val"].ToString().Split(new char[] { '|' }).Length > 1) ? row["bet_val"].ToString().Split(new char[] { '|' })[1] : "");
                                }
                                else
                                {
                                    builder.AppendFormat(",\"bet_val1\": \"{0}\"", row["bet_val"].ToString().Split(new char[] { '|' })[0]);
                                    builder.AppendFormat(",\"bet_val2\": \"{0}\"", (row["bet_val"].ToString().Split(new char[] { '|' }).Length > 1) ? row["bet_val"].ToString().Split(new char[] { '|' })[1] : "");
                                }
                                builder.Append("}");
                                str5 = builder.ToString();
                                builder2 = new StringBuilder();
                                builder2.Append("{");
                                if ((("92566".Equals(row["odds_id"].ToString()) || "92567".Equals(row["odds_id"].ToString())) || "92568".Equals(row["odds_id"].ToString())) || "92636".Equals(row["odds_id"].ToString()))
                                {
                                    builder2.AppendFormat("\"odds1\": \"{0}\"", row["odds"].ToString().Split(new char[] { ',' })[0]);
                                    builder2.AppendFormat(",\"oddstext1\": \"{0}\"", row["play_name"].ToString());
                                    builder2.AppendFormat(",\"odds2\": \"{0}\"", row["odds"].ToString().Split(new char[] { ',' })[1]);
                                    builder2.AppendFormat(",\"oddstext2\": \"{0}\"", "連" + base.get_YearLian());
                                }
                                else if ((("92569".Equals(row["odds_id"].ToString()) || "92570".Equals(row["odds_id"].ToString())) || "92571".Equals(row["odds_id"].ToString())) || "92637".Equals(row["odds_id"].ToString()))
                                {
                                    builder2.AppendFormat("\"odds1\": \"{0}\"", row["odds"].ToString().Split(new char[] { ',' })[0]);
                                    builder2.AppendFormat(",\"oddstext1\": \"{0}\"", row["play_name"].ToString());
                                    builder2.AppendFormat(",\"odds2\": \"{0}\"", row["odds"].ToString().Split(new char[] { ',' })[1]);
                                    builder2.AppendFormat(",\"oddstext2\": \"{0}\"", "連0");
                                }
                                else if ("92286".Equals(row["odds_id"].ToString()) || "92639".Equals(row["odds_id"].ToString()))
                                {
                                    builder2.AppendFormat("\"odds1\": \"{0}\"", row["odds"].ToString().Split(new char[] { ',' })[0]);
                                    builder2.AppendFormat(",\"oddstext1\": \"{0}\"", row["play_name"].ToString());
                                    builder2.AppendFormat(",\"odds2\": \"{0}\"", row["odds"].ToString().Split(new char[] { ',' })[1]);
                                    builder2.AppendFormat(",\"oddstext2\": \"{0}\"", "中三");
                                }
                                else if ("92288".Equals(row["odds_id"].ToString()) || "92641".Equals(row["odds_id"].ToString()))
                                {
                                    builder2.AppendFormat("\"odds1\": \"{0}\"", row["odds"].ToString().Split(new char[] { ',' })[0]);
                                    builder2.AppendFormat(",\"oddstext1\": \"{0}\"", row["play_name"].ToString());
                                    builder2.AppendFormat(",\"odds2\": \"{0}\"", row["odds"].ToString().Split(new char[] { ',' })[1]);
                                    builder2.AppendFormat(",\"oddstext2\": \"{0}\"", "中二");
                                }
                                else
                                {
                                    builder2.AppendFormat("\"odds1\": \"{0}\"", row["odds"].ToString().Split(new char[] { ',' })[0]);
                                    builder2.AppendFormat(",\"oddstext1\": \"{0}\"", row["play_name"].ToString());
                                    builder2.AppendFormat(",\"odds2\": \"{0}\"", "");
                                    builder2.AppendFormat(",\"oddstext2\": \"{0}\"", "");
                                }
                                builder2.Append("}");
                                str7 = builder2.ToString();
                                str8 = row["GroupText"].ToString();
                                str9 = row["unit_cnt"].ToString();
                                if (!string.IsNullOrEmpty(row["lm_type"]))
                                {
                                    str10 = row["lm_type"].ToString();
                                }
                                if (((("92285,92286,92287,92288,92289,92575".IndexOf(row["odds_id"].ToString()) > -1) || ("92638,92639,92640,92641,92642,92643".IndexOf(row["odds_id"].ToString()) > -1)) || ("92572,92588,92589,92590,92591,92592".IndexOf(row["odds_id"].ToString()) > -1)) || "92565".Equals(row["odds_id"].ToString()))
                                {
                                    dictionary2 = new Dictionary<string, string>();
                                    str12 = row["odds"].ToString().Split(new char[] { ',' })[0];
                                    strArray = row["bet_wt"].ToString().Split(new char[] { '~' });
                                    num2 = 0;
                                    while (num2 < strArray.Length)
                                    {
                                        if (!string.IsNullOrEmpty(strArray[num2]))
                                        {
                                            str13 = strArray[num2].Split(new char[] { '|' })[0];
                                            str14 = strArray[num2].Split(new char[] { '|' })[1].Split(new char[] { ',' })[0];
                                            if (row["odds"].ToString().Split(new char[] { ',' }).Length > 1)
                                            {
                                                str15 = strArray[num2].Split(new char[] { '|' })[1];
                                                if (!dictionary2.ContainsKey(str13))
                                                {
                                                    dictionary2.Add(str13, str15);
                                                }
                                            }
                                            else
                                            {
                                                str15 = str14;
                                                if (!dictionary2.ContainsKey(str13))
                                                {
                                                    dictionary2.Add(str13, str15);
                                                }
                                            }
                                        }
                                        num2++;
                                    }
                                    num3 = 0;
                                    builder3 = new StringBuilder();
                                    builder3.Append("{");
                                    foreach (KeyValuePair<string, string> pair in dictionary2)
                                    {
                                        if (num3 == 0)
                                        {
                                            if ("92565".Equals(row["odds_id"].ToString()))
                                            {
                                                string sXLNameBySXid = FunctionSix.GetSXLNameBySXid(pair.Key);
                                                builder3.AppendFormat("\"{0}\": \"{1}\"", sXLNameBySXid, pair.Value);
                                            }
                                            else
                                            {
                                                builder3.AppendFormat("\"{0}\": \"{1}\"", pair.Key, pair.Value);
                                            }
                                        }
                                        else if ("92565".Equals(row["odds_id"].ToString()))
                                        {
                                            string introduced46 = FunctionSix.GetSXLNameBySXid(pair.Key);
                                            builder3.AppendFormat("\"{0}\": \"{1}\"", introduced46, pair.Value);
                                        }
                                        else
                                        {
                                            builder3.AppendFormat(",\"{0}\": \"{1}\"", pair.Key, pair.Value);
                                        }
                                        num3++;
                                    }
                                    builder3.Append("}");
                                    str11 = builder3.ToString();
                                }
                                if ((("92566".Equals(row["odds_id"].ToString()) || "92567".Equals(row["odds_id"].ToString())) || "92568".Equals(row["odds_id"].ToString())) || "92636".Equals(row["odds_id"].ToString()))
                                {
                                    dictionary2 = new Dictionary<string, string>();
                                    str12 = row["odds"].ToString().Split(new char[] { ',' })[0];
                                    str16 = row["odds"].ToString().Split(new char[] { ',' })[1];
                                    strArray = row["bet_wt"].ToString().Split(new char[] { '~' });
                                    num2 = 0;
                                    while (num2 < strArray.Length)
                                    {
                                        if (!string.IsNullOrEmpty(strArray[num2]))
                                        {
                                            str13 = strArray[num2].Split(new char[] { '|' })[0];
                                            str14 = strArray[num2].Split(new char[] { '|' })[1].Split(new char[] { ',' })[0];
                                            str15 = "0";
                                            if (str13.Equals(base.get_YearLianID()))
                                            {
                                                str15 = str7;
                                            }
                                            else
                                            {
                                                str15 = str14 + "," + str16;
                                            }
                                            if (!(dictionary2.ContainsKey(str13) || str13.Equals(base.get_YearLianID())))
                                            {
                                                dictionary2.Add(str13, str15);
                                            }
                                        }
                                        num2++;
                                    }
                                    num3 = 0;
                                    builder3 = new StringBuilder();
                                    builder3.Append("{");
                                    foreach (KeyValuePair<string, string> pair in dictionary2)
                                    {
                                        if (num3 == 0)
                                        {
                                            string introduced47 = FunctionSix.GetSXLNameBySXid(pair.Key);
                                            builder3.AppendFormat("\"{0}\": \"{1}\"", introduced47, pair.Value);
                                        }
                                        else
                                        {
                                            string introduced48 = FunctionSix.GetSXLNameBySXid(pair.Key);
                                            builder3.AppendFormat(",\"{0}\": \"{1}\"", introduced48, pair.Value);
                                        }
                                        num3++;
                                    }
                                    builder3.Append("}");
                                    str11 = builder3.ToString();
                                }
                                if ((("92569".Equals(row["odds_id"].ToString()) || "92570".Equals(row["odds_id"].ToString())) || "92571".Equals(row["odds_id"].ToString())) || "92637".Equals(row["odds_id"].ToString()))
                                {
                                    dictionary2 = new Dictionary<string, string>();
                                    str12 = row["odds"].ToString().Split(new char[] { ',' })[0];
                                    str16 = row["odds"].ToString().Split(new char[] { ',' })[1];
                                    strArray = row["bet_wt"].ToString().Split(new char[] { '~' });
                                    for (num2 = 0; num2 < strArray.Length; num2++)
                                    {
                                        if (!string.IsNullOrEmpty(strArray[num2]))
                                        {
                                            str13 = strArray[num2].Split(new char[] { '|' })[0];
                                            str14 = strArray[num2].Split(new char[] { '|' })[1].Split(new char[] { ',' })[0];
                                            str15 = "0";
                                            if (str13.Equals("10"))
                                            {
                                                str15 = str14;
                                            }
                                            else
                                            {
                                                str15 = str14 + "," + str16;
                                            }
                                            if (!(dictionary2.ContainsKey(str13) || str13.Equals("10")))
                                            {
                                                dictionary2.Add(str13, str15);
                                            }
                                        }
                                    }
                                    num3 = 0;
                                    builder3 = new StringBuilder();
                                    builder3.Append("{");
                                    foreach (KeyValuePair<string, string> pair in dictionary2)
                                    {
                                        if (num3 == 0)
                                        {
                                            string wSLNameBySXid = FunctionSix.GetWSLNameBySXid(pair.Key);
                                            builder3.AppendFormat("\"{0}\": \"{1}\"", wSLNameBySXid, pair.Value);
                                        }
                                        else
                                        {
                                            string introduced50 = FunctionSix.GetWSLNameBySXid(pair.Key);
                                            builder3.AppendFormat(",\"{0}\": \"{1}\"", introduced50, pair.Value);
                                        }
                                        num3++;
                                    }
                                    builder3.Append("}");
                                    str11 = builder3.ToString();
                                }
                                if (string.IsNullOrEmpty(str11))
                                {
                                    str11 = "{}";
                                }
                            }
                            else
                            {
                                str2 = row["lottery_name"].ToString();
                                str3 = row["order_num"].ToString();
                                str4 = row["phase"].ToString();
                                str6 = string.Format("{0:F1}", float.Parse(row["amount"].ToString()));
                                if (!string.IsNullOrEmpty(row["lm_type"]))
                                {
                                    str10 = row["lm_type"].ToString();
                                }
                                builder = new StringBuilder();
                                builder.Append("{");
                                builder.AppendFormat("\"play_name\": \"{0}\"", row["play_name"].ToString());
                                if (row["play_name"].ToString().IndexOf("尾數") > -1)
                                {
                                    builder.AppendFormat(",\"bet_val1\": \"{0}\"", row["bet_val"].ToString().Split(new char[] { '|' })[0] + "尾");
                                }
                                else
                                {
                                    builder.AppendFormat(",\"bet_val1\": \"{0}\"", row["bet_val"].ToString().Split(new char[] { '|' })[0]);
                                }
                                builder.AppendFormat(",\"bet_val2\": \"{0}\"", (row["bet_val"].ToString().Split(new char[] { '|' }).Length > 1) ? row["bet_val"].ToString().Split(new char[] { '|' })[1] : "");
                                builder.Append("}");
                                str5 = builder.ToString();
                                builder2 = new StringBuilder();
                                builder2.Append("{");
                                builder2.AppendFormat("\"odds1\": \"{0}\"", row["odds"].ToString().Split(new char[] { ',' })[0]);
                                builder2.AppendFormat(",\"oddstext1\": \"{0}\"", row["play_name"].ToString());
                                builder2.AppendFormat(",\"odds2\": \"{0}\"", "");
                                builder2.AppendFormat(",\"oddstext2\": \"{0}\"", "");
                                builder2.Append("}");
                                str7 = builder2.ToString();
                                str11 = "{}";
                            }
                            list6.Add(str7);
                            list.Add(str3);
                            list2.Add(str4);
                            list3.Add(str5);
                            list4.Add(str6);
                            list5.Add(str2);
                            list8.Add(str8);
                            list7.Add(str9);
                            list9.Add(str10);
                            list10.Add(str11);
                        }
                        dictionary.Add("order_num", list);
                        dictionary.Add("name", list5);
                        dictionary.Add("nn", list2);
                        dictionary.Add("item", list3);
                        dictionary.Add("odds", list6);
                        dictionary.Add("money", list4);
                        dictionary.Add("grouptext", list8);
                        dictionary.Add("group", list7);
                        dictionary.Add("lmtype", list9);
                        dictionary.Add("wtvalue", list10);
                    }
                }
                result.set_data(dictionary);
                strResult = JsonHandle.ObjectToJson(result).Replace("\"{\\", "{").Replace("}\"", "}").Replace(@"\", "").Replace("\"{", "{");
            }
        }

        public void get_ranklist(HttpContext context, ref string strResult)
        {
            string str = LSRequest.qq("playpage").Trim();
            if (string.IsNullOrEmpty(str))
            {
                context.Response.End();
            }
            else if (context.Session["user_name"] == null)
            {
                context.Response.End();
            }
            else
            {
                ReturnResult result = new ReturnResult();
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                dictionary.Add("type", "get_ranklist");
                dictionary.Add("playpage", str);
                List<object> list = new List<object>();
                DataSet topPhase = CallBLL.cz_phase_six_bll.GetTopPhase(5);
                if (topPhase != null)
                {
                    List<string> collection = new List<string>();
                    List<string> list3 = new List<string>();
                    DataTable table = topPhase.Tables[0];
                    int num = 6;
                    Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
                    foreach (DataRow row in table.Rows)
                    {
                        string str2 = "";
                        string str3 = "";
                        string str4 = "";
                        string str5 = "";
                        string str6 = "";
                        string str7 = "";
                        string str8 = "";
                        string str9 = "";
                        string str10 = "";
                        string str11 = "";
                        int num2 = 0;
                        string str12 = "";
                        str2 = row["phase"].ToString();
                        str3 = row["sn_stop_date"].ToString();
                        for (int i = 1; i <= num; i++)
                        {
                            string str13 = "n" + i;
                            collection.Add(row[str13].ToString());
                            string str14 = "zodiac_n" + i;
                            list3.Add(row[str14].ToString());
                            num2 += Convert.ToInt32(row[str13]);
                            if (i.Equals(1))
                            {
                                str4 = str4 + row[str14].ToString();
                            }
                            else
                            {
                                str4 = str4 + "," + row[str14].ToString();
                            }
                        }
                        collection.Add(row["sn"].ToString());
                        num2 += Convert.ToInt32(row["sn"].ToString());
                        str5 = row["zodiac_sn"].ToString();
                        if (double.Parse(row["sn"].ToString().Trim()) == 49.0)
                        {
                            str6 = base.SetWordColor("和");
                        }
                        else if ((double.Parse(row["sn"].ToString().Trim()) % 2.0) == 0.0)
                        {
                            str6 = base.SetWordColor("雙");
                        }
                        else
                        {
                            str6 = base.SetWordColor("單");
                        }
                        if (double.Parse(row["sn"].ToString().Trim()) <= 24.0)
                        {
                            str7 = base.SetWordColor("小");
                        }
                        else if (double.Parse(row["sn"].ToString().Trim()) == 49.0)
                        {
                            str7 = base.SetWordColor("和");
                        }
                        else
                        {
                            str7 = base.SetWordColor("大");
                        }
                        int num4 = 0;
                        num4 = int.Parse(row["sn"].ToString().Trim()) % 10;
                        if (double.Parse(row["sn"].ToString().Trim()) == 49.0)
                        {
                            str8 = base.SetWordColor("和");
                        }
                        else if (num4 <= 4)
                        {
                            str8 = base.SetWordColor("小");
                        }
                        else
                        {
                            str8 = base.SetWordColor("大");
                        }
                        str9 = this.get_tz(row["sn"].ToString().Trim());
                        int num5 = int.Parse(row["sn"].ToString().Trim()) / 10;
                        int num6 = int.Parse(row["sn"].ToString().Trim()) % 10;
                        int num7 = num5 + num6;
                        if (int.Parse(row["sn"].ToString().Trim()) == 0x31)
                        {
                            str10 = base.SetWordColor("和");
                        }
                        else if ((num7 % 2) == 0)
                        {
                            str10 = base.SetWordColor("合雙");
                        }
                        else
                        {
                            str10 = base.SetWordColor("合單");
                        }
                        if ((Convert.ToInt32(num2) % 2) == 0)
                        {
                            str12 = base.SetWordColor("雙");
                        }
                        else
                        {
                            str12 = base.SetWordColor("單");
                        }
                        if (Convert.ToInt32(num2) <= 0xae)
                        {
                            str11 = base.SetWordColor("小");
                        }
                        else
                        {
                            str11 = base.SetWordColor("大");
                        }
                        dictionary2.Add("phase", str2);
                        dictionary2.Add("play_open_date", Convert.ToDateTime(str3).ToString("yyyy/MM/dd"));
                        dictionary2.Add("draw_num", new List<string>(collection));
                        dictionary2.Add("total", new List<string> { str4, str5, str6, str7, str8, str9, str10, num2.ToString(), str12, str11 });
                        list.Add(new Dictionary<string, object>(dictionary2));
                        dictionary2.Clear();
                        collection.Clear();
                    }
                }
                dictionary.Add("jqkj", list);
                result.set_data(dictionary);
                strResult = JsonHandle.ObjectToJson(result);
            }
        }

        private static string get_sxws_min_pl(DataTable myDT, string nos, string pl, string pl_ID)
        {
            if (myDT.Rows.Count > 0)
            {
                var selector = null;
                Dictionary<string, double> ddDict = new Dictionary<string, double>();
                int[] source = Array.ConvertAll<string, int>(nos.Split(new char[] { ',' }), s => int.Parse(s));
                foreach (DataRow row in myDT.Rows)
                {
                    int num = int.Parse(row["number"].ToString().Trim());
                    string str = row["odds_id"].ToString().Trim();
                    if (source.Contains<int>(num) && (str == pl_ID.Trim()))
                    {
                        ddDict.Add(num.ToString(), double.Parse(row["wt_value"].ToString()));
                    }
                }
                if (ddDict.Count > 0)
                {
                    if (selector == null)
                    {
                        selector = x => new { x = x, y = ddDict[x] };
                    }
                    string str2 = (from x in ddDict.Keys.Select(selector)
                        orderby x.y
                        select x).First().x;
                    double num2 = ddDict[str2.ToString()];
                    if (num2 == 0.0)
                    {
                        return "";
                    }
                    string str3 = str2.ToString();
                    if (str3.Length == 1)
                    {
                        str3 = "0" + str3;
                    }
                    if (pl.IndexOf(',') > 0)
                    {
                        string str4 = pl.Split(new char[] { ',' })[0];
                        string str5 = string.Format("{0}", Convert.ToDouble(str4) + num2);
                        return (str3 + "|" + str5);
                    }
                    return "err";
                }
                return "err";
            }
            return "err";
        }

        public string get_tz(string hm)
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

        private static string getfz(string xz_str, int xz)
        {
            int[] a = new int[10];
            string[] strArray = xz_str.Split(new char[] { ',' });
            string str = "";
            string[] strArray2 = comb(a, strArray.Length, xz, xz).Split(new char[] { ',' });
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

        private static string getlongnum(string ss_str)
        {
            string[] strArray = ss_str.Split(new char[] { ',' });
            string str = "";
            foreach (string str2 in strArray)
            {
                str = str + getnum(str2) + ",";
            }
            return str.Substring(0, str.Length - 1);
        }

        private static string getnum(string ss_num)
        {
            int num = 0;
            num = int.Parse(ss_num);
            if (num < 10)
            {
                return ("0" + num.ToString());
            }
            return num.ToString();
        }

        private static int getzs(string[] a, string[] b)
        {
            int num = 0;
            foreach (string str in a)
            {
                foreach (string str2 in b)
                {
                    if (str.Trim() == str2.Trim())
                    {
                        num++;
                    }
                }
            }
            return ((a.Length * b.Length) - num);
        }

        private static string getzsstr(string a, string b)
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

        public override void ProcessRequest(HttpContext context)
        {
            base.checkLogin();
            base.IsLotteryExist(100.ToString());
            string str = context.Request.Form["action"];
            string strResult = "";
            if (string.IsNullOrEmpty(str))
            {
                return;
            }
            string str3 = str;
            if (str3 != null)
            {
                if (!(str3 == "get_oddsinfo"))
                {
                    if (str3 == "get_ranklist")
                    {
                        this.get_ranklist(context, ref strResult);
                        goto Label_00E2;
                    }
                    if (str3 == "get_openball")
                    {
                        this.get_openball(context, ref strResult);
                        goto Label_00E2;
                    }
                    if (str3 == "get_putinfo")
                    {
                        this.get_putinfo(context, ref strResult);
                        goto Label_00E2;
                    }
                    if (str3 == "put_money")
                    {
                        this.put_money(context, ref strResult);
                        goto Label_00E2;
                    }
                }
                else
                {
                    this.get_oddsinfo(context, ref strResult);
                    goto Label_00E2;
                }
            }
            this.do_default(context, ref strResult);
        Label_00E2:
            context.Response.ContentType = "text/json";
            context.Response.Write(strResult);
        }

        public void put_money(HttpContext context, ref string strResult)
        {
            if (context.Session["user_name"] == null)
            {
                context.Response.End();
            }
            else
            {
                this.do_put_money(context, ref strResult);
            }
        }

        public static void put_money_lm(HttpContext context, ref string strResult)
        {
        }

        private static string px(string p_str)
        {
            string[] array = p_str.Split(new char[] { ',' });
            Array.Sort<string>(array);
            return string.Join(",", array.ToArray<string>());
        }

        private void SetWtValue(DataTable numberAmountTableDT, DataTable wtTableDT, string odds_id, double min_odds, double current_odds, double p_jphl, double p_pljd, string category, string play_name, string put_amount, string phase, string phase_id)
        {
            foreach (DataRow row in numberAmountTableDT.Rows)
            {
                int num = int.Parse(row["number"].ToString());
                double num2 = double.Parse(row["amount"].ToString());
                double num3 = double.Parse(wtTableDT.Select(string.Format(" number={0} ", num))[0]["wt_value"].ToString());
                double num4 = p_jphl;
                double num5 = p_pljd;
                double num6 = Math.Floor((double) (num2 / num4));
                if (num6 >= 1.0)
                {
                    num5 *= num6;
                    double num7 = num4 * num6;
                    double num8 = num3 - num5;
                    if ((num8 + current_odds) > min_odds)
                    {
                        CallBLL.cz_lm_number_amount_six_bll.SetNumberWTValue(int.Parse(odds_id), num, num8, num7);
                    }
                    else if (current_odds > min_odds)
                    {
                        num8 = -(current_odds - min_odds);
                        CallBLL.cz_lm_number_amount_six_bll.SetNumberWTValue(int.Parse(odds_id), num, num8, num7);
                    }
                    this.SetWtValueLog(odds_id, num3, num8, category, play_name, put_amount, phase, phase_id, num.ToString());
                }
            }
        }

        private void SetWtValueDuble(DataTable numberAmountTableDT, DataTable wtTableDT, string odds_id, string min_odds, string current_odds, double p_jphl, double p_pljd, string category, string play_name, string put_amount, string phase, string phase_id)
        {
            foreach (DataRow row in numberAmountTableDT.Rows)
            {
                int num = int.Parse(row["number"].ToString());
                double num2 = double.Parse(row["amount"].ToString());
                double num3 = double.Parse(wtTableDT.Select(string.Format(" number={0} ", num))[0]["wt_value"].ToString());
                double num4 = p_jphl;
                double num5 = p_pljd;
                double num6 = Math.Floor((double) (num2 / num4));
                if (num6 >= 1.0)
                {
                    num5 *= num6;
                    double num7 = num4 * num6;
                    double num8 = num3 - num5;
                    double num9 = double.Parse(min_odds.Split(new char[] { ',' })[0]);
                    double num10 = double.Parse(min_odds.Split(new char[] { ',' })[1]);
                    double num11 = double.Parse(current_odds.Split(new char[] { ',' })[0]);
                    double num12 = double.Parse(current_odds.Split(new char[] { ',' })[1]);
                    if (((num8 + num11) > num9) && ((num8 + num12) > num10))
                    {
                        CallBLL.cz_lm_number_amount_six_bll.SetNumberWTValue(int.Parse(odds_id), num, num8, num7);
                    }
                    else if ((num11 > num9) && (num12 > num10))
                    {
                        if ((num11 - num9) > (num12 - num10))
                        {
                            num8 = -(num12 - num10);
                        }
                        else
                        {
                            num8 = -(num11 - num9);
                        }
                        CallBLL.cz_lm_number_amount_six_bll.SetNumberWTValue(int.Parse(odds_id), num, num8, num7);
                    }
                    this.SetWtValueLog(odds_id, num3, num8, category, play_name, put_amount, phase, phase_id, num.ToString());
                }
            }
        }

        private void SetWtValueLog(string odds_id, double old_val, double new_val, string category, string play_name, string put_amount, string phase, string phase_id, string number)
        {
            cz_system_log _log = new cz_system_log();
            _log.set_user_name("系統");
            _log.set_children_name("");
            _log.set_category(category);
            _log.set_play_name(play_name);
            _log.set_put_amount(put_amount);
            int num = 100;
            _log.set_l_name(base.GetGameNameByID(num.ToString()));
            _log.set_l_phase(phase);
            _log.set_action("降賠率");
            _log.set_odds_id(int.Parse(odds_id));
            _log.set_old_val(string.Format("號碼【{0}】：{1}", number, old_val));
            _log.set_new_val(string.Format("號碼【{0}】：{1}", number, new_val));
            _log.set_ip(LSRequest.GetIP());
            _log.set_add_time(DateTime.Now);
            _log.set_note("系統自動降賠");
            _log.set_type_id(Convert.ToInt32((LSEnums.LogTypeID) 0));
            _log.set_lottery_type(100);
            CallBLL.cz_system_log_bll.Add(_log);
            cz_jp_odds _odds = new cz_jp_odds();
            _odds.set_add_time(DateTime.Now);
            _odds.set_odds_id(int.Parse(odds_id));
            _odds.set_phase_id(int.Parse(phase_id));
            _odds.set_play_name(play_name);
            _odds.set_put_amount(put_amount);
            _odds.set_odds((old_val - new_val).ToString());
            _odds.set_lottery_type(100);
            _odds.set_phase(phase);
            _odds.set_old_odds(old_val.ToString());
            _odds.set_new_odds(new_val.ToString());
            _odds.set_number(number);
            CallBLL.cz_jp_odds_bll.Add(_odds);
        }
    }
}

