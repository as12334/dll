namespace User.Web.L_PCDD.Handler
{
    using LotterySystem.Common;
    using LotterySystem.Model;
    using LotterySystem.WebPageBase;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Text;
    using System.Web;
    using User.Web.Handler;
    using User.Web.WebBase;

    public class Handler : BaseHandler
    {
        public void do_default(HttpContext context, ref string strResult)
        {
            ReturnResult result = new ReturnResult();
            result.set_success(400);
            result.set_tipinfo("action param error");
            strResult = JsonHandle.ObjectToJson(result);
        }

        public void do_put_money(HttpContext context, ref string strResult)
        {
            DateTime time;
            double num8;
            int num41;
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
            user_kc_rate rate = base.GetUserRate_kc(getUserModelInfo.get_zjname());
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
                    string str3 = base.get_JeuValidate();
                    dictionary.Add("JeuValidate", str3);
                    result.set_data(dictionary);
                    strResult = JsonHandle.ObjectToJson(result);
                    return;
                }
            }
            string str4 = "";
            string s = "";
            string item = "";
            string str7 = "";
            string str8 = "";
            int num = 7;
            s = LSRequest.qq("phaseid");
            str4 = LSRequest.qq("playpage");
            item = LSRequest.qq("oddsid");
            str7 = LSRequest.qq("uPI_P");
            str8 = LSRequest.qq("uPI_M");
            string str9 = LSRequest.qq("uPI_TM");
            string[] source = item.Split(new char[] { ',' });
            if (source.Distinct<string>().ToList<string>().Count != source.Length)
            {
                base.DeleteCreditLock(str);
                context.Response.End();
            }
            if (source.Length > FileCacheHelper.get_GetPCDDMaxGroup())
            {
                base.DeleteCreditLock(str);
                result.set_success(400);
                result.set_tipinfo(string.Format("您選擇的號碼超過了最大{0}組，請重新選擇號碼!", FileCacheHelper.get_GetPCDDMaxGroup()));
                dictionary.Add("JeuValidate", base.get_JeuValidate());
                result.set_data(dictionary);
                strResult = JsonHandle.ObjectToJson(result);
                return;
            }
            string[] strArray2 = LSRequest.qq("i_index").Split(new char[] { ',' });
            string str10 = LSRequest.qq("JeuValidate");
            if (((context.Session["JeuValidate"] == null) || (context.Session["JeuValidate"].ToString().Trim() != str10)) || (context.Session["JeuValidate"].ToString().Trim() == ""))
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
            int num3 = 1;
            if (getUserModelInfo.get_su_type().ToString().Trim() != "dl")
            {
                num3 = -1;
            }
            int index = 0;
        Label_0534:;
            if (index < str8.Split(new char[] { ',' }).Length)
            {
                if (!base.IsNumber(str8.Split(new char[] { ',' })[index]))
                {
                    base.DeleteCreditLock(str);
                    result.set_tipinfo("下注金額有誤！");
                    result.set_success(500);
                    dictionary.Add("index", strArray2[index]);
                    dictionary.Add("JeuValidate", base.get_JeuValidate());
                    result.set_data(dictionary);
                    strResult = JsonHandle.ObjectToJson(result);
                    return;
                }
                index++;
                goto Label_0534;
            }
            if (getUserModelInfo.get_begin_kc().Trim() != "yes")
            {
                getUserModelInfo = base.GetRateByUserObject(2, rate);
            }
            string str12 = "";
            string str13 = "";
            if (!Utils.IsInteger(s))
            {
                base.Response.End();
            }
            DataTable table2 = CallBLL.cz_phase_pcdd_bll.GetIsClosedByTime(int.Parse(s)).Tables[0];
            if ((table2 != null) && (table2.Rows.Count > 0))
            {
                if (table2.Rows[0]["is_closed"].ToString().Trim() == "1")
                {
                    base.DeleteCreditLock(str);
                    result.set_tipinfo(string.Format("奖期[{0}]已封盘,停止下注！", table2.Rows[0]["phase"].ToString()));
                    result.set_success(400);
                    dictionary.Add("JeuValidate", base.get_JeuValidate());
                    result.set_data(dictionary);
                    strResult = JsonHandle.ObjectToJson(result);
                    return;
                }
                str12 = table2.Rows[0]["phase"].ToString();
                str13 = table2.Rows[0]["p_id"].ToString();
                time = Convert.ToDateTime(table2.Rows[0]["stop_date"].ToString());
                num41 = 7;
                string str14 = base.BetReceiveEnd(num41.ToString());
                if (!string.IsNullOrEmpty(str14))
                {
                    time = Convert.ToDateTime(table2.Rows[0]["play_open_date"].ToString()).AddSeconds((double) -int.Parse(str14));
                }
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
            string str15 = "";
            string str16 = "";
            double num5 = 0.0;
            DataTable table3 = CallBLL.cz_users_bll.GetUserRate(str, 2).Tables[0];
            str15 = table3.Rows[0]["kc_kind"].ToString().Trim().ToUpper();
            str16 = table3.Rows[0]["su_type"].ToString().Trim();
            num5 = double.Parse(table3.Rows[0]["kc_usable_credit"].ToString().Trim());
            getUserModelInfo.set_kc_kind(str15.ToUpper());
            string str17 = "0";
            string str18 = "0";
            string str19 = "0";
            string str20 = "0";
            string str21 = "0";
            string str22 = "0";
            double num6 = 0.0;
            double num7 = 0.0;
            if (rate.get_zcyg().Equals("1"))
            {
                num8 = (((100.0 - double.Parse(rate.get_fgszc())) - double.Parse(rate.get_gdzc())) - double.Parse(rate.get_zdzc())) - double.Parse(rate.get_dlzc());
                num6 = num8;
                num7 = double.Parse(rate.get_fgszc());
            }
            else if (getUserModelInfo.get_u_type() == "fgs")
            {
                num6 = 100.0;
            }
            else
            {
                num8 = (((100.0 - double.Parse(rate.get_zjzc())) - double.Parse(rate.get_gdzc())) - double.Parse(rate.get_zdzc())) - double.Parse(rate.get_dlzc());
                num7 = num8;
                num6 = double.Parse(rate.get_zjzc());
            }
            double num9 = 0.0;
            double num10 = 0.0;
            double num11 = 0.0;
            double num12 = 0.0;
            double num13 = 0.0;
            double num14 = 0.0;
            string str23 = "";
            string str24 = "";
            string str25 = "";
            string str26 = "";
            string str27 = "";
            string[] strArray3 = item.Split(new char[] { ',' });
            string[] strArray4 = str7.Split(new char[] { ',' });
            string[] strArray5 = str8.Split(new char[] { ',' });
            DataTable plDT = CallBLL.cz_odds_pcdd_bll.GetPlayOddsByID(item).Tables[0];
            if (plDT.Rows.Count == 0)
            {
                base.DeleteCreditLock(str);
                result.set_success(400);
                result.set_tipinfo("賠率ID設置错误!");
                dictionary.Add("JeuValidate", base.get_JeuValidate());
                result.set_data(dictionary);
                strResult = JsonHandle.ObjectToJson(result);
            }
            else
            {
                DataTable table5 = CallBLL.cz_bet_kc_bll.GetSinglePhaseByBet(str12, str, num.ToString(), item, null).Tables[0];
                if ((plDT.Rows.Count == 0) || (plDT.Rows.Count != strArray3.Count<string>()))
                {
                    base.DeleteCreditLock(str);
                    result.set_success(400);
                    result.set_tipinfo("系统错误!");
                    dictionary.Add("JeuValidate", base.get_JeuValidate());
                    result.set_data(dictionary);
                    strResult = JsonHandle.ObjectToJson(result);
                }
                else
                {
                    List<string> list = new List<string> { "72055" };
                    double num15 = 0.0;
                    string str28 = "";
                    List<string> list2 = new List<string>();
                    List<string> list3 = new List<string>();
                    DataTable dataTable = null;
                    int num16 = 0;
                    foreach (string str29 in strArray3)
                    {
                        num9 = 0.0;
                        foreach (DataRow row in plDT.Rows)
                        {
                            if (row["odds_id"].ToString().Trim() == str29.Trim())
                            {
                                str28 = row["is_open"].ToString().Trim();
                                str24 = row["current_odds"].ToString().Trim();
                                str25 = row[str15 + "_diff"].ToString().Trim();
                                str23 = row["play_id"].ToString().Trim();
                                str26 = row["play_name"].ToString().Trim();
                                str27 = row["put_amount"].ToString().Trim();
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
                                if (row2["odds_id"].ToString().Trim() == str29.Trim())
                                {
                                    num9 = double.Parse(row2["sumbet"].ToString().Trim());
                                    break;
                                }
                            }
                        }
                        DataTable table7 = null;
                        if (FileCacheHelper.get_GetKCPutMoneyCache() == "1")
                        {
                            if (CacheHelper.GetCache("kc_drawback_FileCacheKey_pcdd" + str23 + this.Session["user_name"].ToString()) == null)
                            {
                                table7 = CallBLL.cz_drawback_pcdd_bll.GetDrawback(str23, str).Tables[0];
                            }
                            else
                            {
                                table7 = base.GetUserDrawback_pcdd(rate, getUserModelInfo.get_kc_kind(), str23);
                            }
                        }
                        else if (CacheHelper.GetCache("kc_drawback_FileCacheKey_pcdd" + this.Session["user_name"].ToString()) == null)
                        {
                            table7 = CallBLL.cz_drawback_pcdd_bll.GetDrawback(str23, str).Tables[0];
                        }
                        else
                        {
                            table7 = base.GetUserDrawback_pcdd(rate, getUserModelInfo.get_kc_kind());
                        }
                        DataRow[] rowArray = table7.Select(string.Format(" play_id={0} and u_name='{1}' ", str23, str));
                        num10 = double.Parse(rowArray[0]["single_max_amount"].ToString().Trim());
                        double num17 = double.Parse(rowArray[0]["single_min_amount"].ToString().Trim());
                        num12 = double.Parse(rowArray[0]["single_phase_amount"].ToString().Trim());
                        if (num10 > num11)
                        {
                            num10 = num11;
                        }
                        if (num12 > num13)
                        {
                            num12 = num13;
                        }
                        if (num17 > num14)
                        {
                            num14 = num17;
                        }
                        if (str28 != "1")
                        {
                            base.DeleteCreditLock(str);
                            result.set_tipinfo(string.Format("{0}<{1}>已經停止投注！", str26, str27));
                            result.set_success(400);
                            dictionary.Add("JeuValidate", base.get_JeuValidate());
                            result.set_data(dictionary);
                            strResult = JsonHandle.ObjectToJson(result);
                            return;
                        }
                        num15 = double.Parse(str24) + double.Parse(str25);
                        string pl = num15.ToString();
                        base.GetOdds_KC(7, str29, ref pl);
                        num15 = Convert.ToDouble(pl);
                        if (!list.Contains(str29))
                        {
                            if (!(double.Parse(strArray4[num16]) == double.Parse(num15.ToString())))
                            {
                                list2.Add(num16.ToString());
                                list3.Add(num15.ToString());
                            }
                        }
                        else if (!(double.Parse(str7) == double.Parse(num15.ToString())))
                        {
                            list2.Add(num16.ToString());
                            list3.Add(num15.ToString());
                        }
                        if (!list.Contains(str29) && (double.Parse(strArray5[num16]) > num10))
                        {
                            base.DeleteCreditLock(str);
                            result.set_tipinfo(string.Format("下注金額超出單注最大金額！", new object[0]));
                            result.set_success(500);
                            dictionary.Add("JeuValidate", base.get_JeuValidate());
                            result.set_data(dictionary);
                            strResult = JsonHandle.ObjectToJson(result);
                            return;
                        }
                        if (double.Parse(strArray5[num16]) > num5)
                        {
                            base.DeleteCreditLock(str);
                            result.set_tipinfo(string.Format("下注金額超出可用金額！", new object[0]));
                            result.set_success(500);
                            dictionary.Add("JeuValidate", base.get_JeuValidate());
                            result.set_data(dictionary);
                            strResult = JsonHandle.ObjectToJson(result);
                            return;
                        }
                        if (double.Parse(strArray5[num16]) < num14)
                        {
                            base.DeleteCreditLock(str);
                            result.set_tipinfo(string.Format("下注金額低過最低金額！", new object[0]));
                            result.set_success(500);
                            dictionary.Add("JeuValidate", base.get_JeuValidate());
                            result.set_data(dictionary);
                            strResult = JsonHandle.ObjectToJson(result);
                            return;
                        }
                        if (list.Contains(str29))
                        {
                            if (double.Parse(str8) > num12)
                            {
                                base.DeleteCreditLock(str);
                                result.set_tipinfo(string.Format("<{0}>下注單期金額超出單期最大金額！", str27));
                                result.set_success(500);
                                dictionary.Add("JeuValidate", base.get_JeuValidate());
                                result.set_data(dictionary);
                                strResult = JsonHandle.ObjectToJson(result);
                                return;
                            }
                        }
                        else if ((double.Parse(strArray5[num16]) + num9) > num12)
                        {
                            base.DeleteCreditLock(str);
                            result.set_tipinfo(string.Format("{0}<{1}>下注單期金額超出單期最大金額！", str26, str27));
                            result.set_success(500);
                            dictionary.Add("JeuValidate", base.get_JeuValidate());
                            result.set_data(dictionary);
                            strResult = JsonHandle.ObjectToJson(result);
                            return;
                        }
                        num16++;
                    }
                    if (list2.Count > 0)
                    {
                        base.DeleteCreditLock(str);
                        result.set_tipinfo(string.Format("賠率有變動,請確認後再提交!", new object[0]));
                        result.set_success(600);
                        dictionary.Add("JeuValidate", base.get_JeuValidate());
                        dictionary.Add("index", list2);
                        dictionary.Add("newpl", list3);
                        result.set_data(dictionary);
                        strResult = JsonHandle.ObjectToJson(result);
                    }
                    else
                    {
                        double num18 = 0.0;
                        int num19 = 0;
                        foreach (string str31 in strArray3)
                        {
                            num18 += double.Parse(strArray5[num19].ToString().Trim());
                            num19++;
                        }
                        if (num5 < num18)
                        {
                            base.DeleteCreditLock(str);
                            result.set_tipinfo(string.Format("可用餘額不足！", new object[0]));
                            result.set_success(500);
                            dictionary.Add("JeuValidate", base.get_JeuValidate());
                            result.set_data(dictionary);
                            strResult = JsonHandle.ObjectToJson(result);
                        }
                        else
                        {
                            DateTime? nullable;
                            DataTable table9;
                            string str40;
                            string str41;
                            string str42;
                            DataRow[] rowArray2;
                            string str44;
                            string str45;
                            double playDrawbackValue;
                            cz_bet_kc _kc;
                            int num26;
                            double num33;
                            double num34;
                            double num35;
                            double num36;
                            double num37;
                            int num39;
                            cz_system_log _log;
                            string str48;
                            cz_jp_odds _odds;
                            DataTable fgsWTTable;
                            string str49;
                            double num43;
                            string str32 = "";
                            string str33 = "";
                            if (!list.Contains(item))
                            {
                                bool flag3 = false;
                                List<string> successBetList = new List<string>();
                                num16 = 0;
                                nullable = null;
                                foreach (string str29 in strArray3)
                                {
                                    if (FileCacheHelper.get_AddBetLockUType_KC().Equals("zj"))
                                    {
                                        num41 = 7;
                                        base.Add_Bet_Lock(num41.ToString(), getUserModelInfo.get_zjname(), str29);
                                    }
                                    table9 = CallBLL.cz_odds_pcdd_bll.GetOddsByID(str29).Tables[0];
                                    int num40 = int.Parse(table9.Rows[0]["play_id"].ToString());
                                    str40 = table9.Rows[0]["play_name"].ToString();
                                    str41 = table9.Rows[0]["put_amount"].ToString();
                                    str42 = table9.Rows[0]["ratio"].ToString();
                                    if (FileCacheHelper.get_GetKCPutMoneyCache() == "1")
                                    {
                                        dataTable = base.GetUserDrawback_pcdd(rate, getUserModelInfo.get_kc_kind(), num40.ToString());
                                    }
                                    else
                                    {
                                        dataTable = base.GetUserDrawback_pcdd(rate, getUserModelInfo.get_kc_kind());
                                    }
                                    if (dataTable == null)
                                    {
                                        base.DeleteCreditLock(str);
                                        if (FileCacheHelper.get_AddBetLockUType_KC().Equals("zj"))
                                        {
                                            num41 = 7;
                                            base.Un_Bet_Lock(num41.ToString(), getUserModelInfo.get_zjname(), str29);
                                        }
                                        else
                                        {
                                            num41 = 7;
                                            base.Un_Bet_Lock(num41.ToString(), rate.get_fgsname(), str29);
                                        }
                                        result.set_tipinfo(string.Format("系統錯誤！", new object[0]));
                                        result.set_success(400);
                                        dictionary.Add("JeuValidate", base.get_JeuValidate());
                                        result.set_data(dictionary);
                                        strResult = JsonHandle.ObjectToJson(result);
                                        return;
                                    }
                                    rowArray2 = dataTable.Select(string.Format(" play_id={0} ", num40));
                                    foreach (DataRow row in rowArray2)
                                    {
                                        str49 = row["u_type"].ToString().Trim();
                                        if (str49 != null)
                                        {
                                            if (!(str49 == "zj"))
                                            {
                                                if (str49 == "fgs")
                                                {
                                                    goto Label_2F5D;
                                                }
                                                if (str49 == "gd")
                                                {
                                                    goto Label_2F81;
                                                }
                                                if (str49 == "zd")
                                                {
                                                    goto Label_2FA5;
                                                }
                                                if (str49 == "dl")
                                                {
                                                    goto Label_2FC9;
                                                }
                                                if (str49 == "hy")
                                                {
                                                    goto Label_2FED;
                                                }
                                            }
                                            else
                                            {
                                                str17 = row[str15 + "_drawback"].ToString().Trim();
                                            }
                                        }
                                        continue;
                                    Label_2F5D:
                                        str18 = row[str15 + "_drawback"].ToString().Trim();
                                        continue;
                                    Label_2F81:
                                        str19 = row[str15 + "_drawback"].ToString().Trim();
                                        continue;
                                    Label_2FA5:
                                        str20 = row[str15 + "_drawback"].ToString().Trim();
                                        continue;
                                    Label_2FC9:
                                        str21 = row[str15 + "_drawback"].ToString().Trim();
                                        continue;
                                    Label_2FED:
                                        if (getUserModelInfo.get_su_type() == "dl")
                                        {
                                            str22 = row[str15 + "_drawback"].ToString().Trim();
                                        }
                                        else if (getUserModelInfo.get_su_type() == "zd")
                                        {
                                            str21 = row[str15 + "_drawback"].ToString().Trim();
                                            str22 = row[str15 + "_drawback"].ToString().Trim();
                                        }
                                        else if (getUserModelInfo.get_su_type() == "gd")
                                        {
                                            str20 = row[str15 + "_drawback"].ToString().Trim();
                                            str22 = row[str15 + "_drawback"].ToString().Trim();
                                        }
                                        else if (getUserModelInfo.get_su_type() == "fgs")
                                        {
                                            str19 = row[str15 + "_drawback"].ToString().Trim();
                                            str22 = row[str15 + "_drawback"].ToString().Trim();
                                        }
                                    }
                                    if (DateTime.Now >= time.AddSeconds(0.0))
                                    {
                                        flag3 = true;
                                        break;
                                    }
                                    if (!FileCacheHelper.get_AddBetLockUType_KC().Equals("zj"))
                                    {
                                        num41 = 7;
                                        base.Add_Bet_Lock(num41.ToString(), rate.get_fgsname(), str29);
                                    }
                                    num43 = double.Parse(table9.Rows[0]["current_odds"].ToString()) + Convert.ToDouble(table9.Rows[0][str15 + "_diff"].ToString().Trim());
                                    str44 = num43.ToString();
                                    str45 = str44.ToString();
                                    base.GetOdds_KC(7, str29, ref str45);
                                    playDrawbackValue = base.GetPlayDrawbackValue(str18, str42);
                                    if ((playDrawbackValue != 0.0) && (double.Parse(str44) > playDrawbackValue))
                                    {
                                        base.DeleteCreditLock(str);
                                        if (FileCacheHelper.get_AddBetLockUType_KC().Equals("zj"))
                                        {
                                            num41 = 7;
                                            base.Un_Bet_Lock(num41.ToString(), getUserModelInfo.get_zjname(), str29);
                                        }
                                        else
                                        {
                                            num41 = 7;
                                            base.Un_Bet_Lock(num41.ToString(), rate.get_fgsname(), str29);
                                        }
                                        result.set_tipinfo(string.Format("賠率錯誤！", new object[0]));
                                        result.set_success(400);
                                        dictionary.Add("JeuValidate", base.get_JeuValidate());
                                        result.set_data(dictionary);
                                        strResult = JsonHandle.ObjectToJson(result);
                                        return;
                                    }
                                    if (!nullable.HasValue)
                                    {
                                        nullable = new DateTime?(DateTime.Now);
                                    }
                                    _kc = new cz_bet_kc();
                                    _kc.set_order_num(Utils.GetOrderNumber());
                                    _kc.set_checkcode(str10);
                                    _kc.set_u_name(getUserModelInfo.get_u_name());
                                    _kc.set_u_nicker(getUserModelInfo.get_u_nicker());
                                    _kc.set_phase_id(new int?(int.Parse(str13)));
                                    _kc.set_phase(str12);
                                    _kc.set_bet_time(nullable);
                                    _kc.set_odds_id(new int?(int.Parse(str29)));
                                    _kc.set_category(table9.Rows[0]["category"].ToString());
                                    _kc.set_play_id(new int?(int.Parse(table9.Rows[0]["play_id"].ToString())));
                                    _kc.set_play_name(str40);
                                    _kc.set_bet_val(str41);
                                    _kc.set_odds(str45);
                                    _kc.set_amount(new decimal?(decimal.Parse(strArray5[num16])));
                                    _kc.set_profit(0);
                                    _kc.set_hy_drawback(new decimal?(decimal.Parse(str22)));
                                    _kc.set_dl_drawback(new decimal?(decimal.Parse(str21)));
                                    _kc.set_zd_drawback(new decimal?(decimal.Parse(str20)));
                                    _kc.set_gd_drawback(new decimal?(decimal.Parse(str19)));
                                    _kc.set_fgs_drawback(new decimal?(decimal.Parse(str18)));
                                    _kc.set_zj_drawback(new decimal?(decimal.Parse(str17)));
                                    _kc.set_dl_rate((float) int.Parse(rate.get_dlzc()));
                                    _kc.set_zd_rate((float) int.Parse(rate.get_zdzc()));
                                    _kc.set_gd_rate((float) int.Parse(rate.get_gdzc()));
                                    _kc.set_fgs_rate(float.Parse(num7.ToString()));
                                    _kc.set_zj_rate(float.Parse(num6.ToString()));
                                    _kc.set_is_payment(0);
                                    _kc.set_dl_name(rate.get_dlname());
                                    _kc.set_zd_name(rate.get_zdname());
                                    _kc.set_gd_name(rate.get_gdname());
                                    _kc.set_fgs_name(rate.get_fgsname());
                                    _kc.set_m_type(new int?(num3));
                                    _kc.set_kind(str15);
                                    _kc.set_ip(LSRequest.GetIP());
                                    _kc.set_lottery_type(new int?(num));
                                    _kc.set_lottery_name(base.GetGameNameByID(_kc.get_lottery_type().ToString()));
                                    _kc.set_isLM(0);
                                    _kc.set_ordervalidcode(Utils.GetOrderValidCode(_kc.get_u_name(), _kc.get_order_num(), _kc.get_bet_val(), _kc.get_odds(), _kc.get_kind(), Convert.ToInt32(_kc.get_phase_id()), Convert.ToInt32(_kc.get_odds_id()), Convert.ToDouble(_kc.get_amount())));
                                    _kc.set_odds_zj(str44);
                                    num26 = 0;
                                    if (!CallBLL.cz_bet_kc_bll.AddBet(_kc, decimal.Parse(strArray5[num16]), str, ref num26) || (num26 <= 0))
                                    {
                                        base.DeleteCreditLock(str);
                                        if (FileCacheHelper.get_AddBetLockUType_KC().Equals("zj"))
                                        {
                                            num41 = 7;
                                            base.Un_Bet_Lock(num41.ToString(), getUserModelInfo.get_zjname(), str29);
                                        }
                                        else
                                        {
                                            num41 = 7;
                                            base.Un_Bet_Lock(num41.ToString(), rate.get_fgsname(), str29);
                                        }
                                        result.set_tipinfo(string.Format("系統錯誤！", new object[0]));
                                        result.set_success(400);
                                        dictionary.Add("JeuValidate", base.get_JeuValidate());
                                        result.set_data(dictionary);
                                        strResult = JsonHandle.ObjectToJson(result);
                                        return;
                                    }
                                    successBetList.Add(string.Concat(new object[] { _kc.get_odds_id(), ",", _kc.get_order_num(), ",", _kc.get_play_name(), ",", _kc.get_bet_val(), ",", _kc.get_odds(), ",", _kc.get_amount() }));
                                    num33 = (double.Parse(strArray5[num16]) * num6) / 100.0;
                                    CallBLL.cz_odds_pcdd_bll.UpdateGrandTotal(Convert.ToDecimal(num33), int.Parse(str29));
                                    num34 = double.Parse(table9.Rows[0]["grand_total"].ToString()) + num33;
                                    num35 = double.Parse(table9.Rows[0]["downbase"].ToString());
                                    num36 = Math.Floor((double) (num34 / num35));
                                    if ((num36 >= 1.0) && (num35 >= 1.0))
                                    {
                                        num37 = double.Parse(table9.Rows[0]["down_odds_rate"].ToString()) * num36;
                                        double num38 = num34 - (num35 * num36);
                                        num39 = CallBLL.cz_odds_pcdd_bll.UpdateGrandTotalCurrentOdds(num37.ToString(), num38.ToString(), str29);
                                        _log = new cz_system_log();
                                        _log.set_user_name("系統");
                                        _log.set_children_name("");
                                        _log.set_category(table9.Rows[0]["category"].ToString());
                                        _log.set_play_name(str40);
                                        _log.set_put_amount(str41);
                                        num41 = 7;
                                        _log.set_l_name(base.GetGameNameByID(num41.ToString()));
                                        _log.set_l_phase(str12);
                                        _log.set_action("降賠率");
                                        _log.set_odds_id(int.Parse(str29));
                                        str48 = table9.Rows[0]["current_odds"].ToString();
                                        _log.set_old_val(str48);
                                        num43 = double.Parse(str48) - num37;
                                        _log.set_new_val(num43.ToString());
                                        _log.set_ip(LSRequest.GetIP());
                                        _log.set_add_time(DateTime.Now);
                                        _log.set_note("系統自動降賠");
                                        _log.set_type_id(Convert.ToInt32((LSEnums.LogTypeID) 0));
                                        _log.set_lottery_type(7);
                                        CallBLL.cz_system_log_bll.Add(_log);
                                        _odds = new cz_jp_odds();
                                        _odds.set_add_time(DateTime.Now);
                                        _odds.set_odds_id(int.Parse(str29));
                                        _odds.set_phase_id(int.Parse(str13));
                                        _odds.set_play_name(str40);
                                        _odds.set_put_amount(str41);
                                        _odds.set_odds(num37.ToString());
                                        _odds.set_lottery_type(7);
                                        _odds.set_phase(str12);
                                        _odds.set_old_odds(str48);
                                        num43 = double.Parse(str48) - num37;
                                        _odds.set_new_odds(num43.ToString());
                                        CallBLL.cz_jp_odds_bll.Add(_odds);
                                    }
                                    fgsWTTable = null;
                                    if (getUserModelInfo.get_kc_op_odds().Equals(1))
                                    {
                                        fgsWTTable = base.GetFgsWTTable(7);
                                    }
                                    CallBLL.cz_autosale_pcdd_bll.DLAutoSale(_kc.get_order_num(), str10, getUserModelInfo.get_u_nicker(), getUserModelInfo.get_u_name(), str15, table9.Rows[0]["play_id"].ToString(), str29.Trim(), str17, str18, str19, str20, str21, str22, str13, str12, _kc.get_ip(), num, _kc.get_lottery_name(), getUserModelInfo, rate, 0, fgsWTTable);
                                    if (getUserModelInfo.get_kc_op_odds().Equals(1))
                                    {
                                        fgsWTTable = base.GetFgsWTTable(7);
                                    }
                                    CallBLL.cz_autosale_pcdd_bll.ZDAutoSale(_kc.get_order_num(), str10, getUserModelInfo.get_u_nicker(), getUserModelInfo.get_u_name(), str15, table9.Rows[0]["play_id"].ToString(), str29.Trim(), str17, str18, str19, str20, str21, str22, str13, str12, _kc.get_ip(), num, _kc.get_lottery_name(), getUserModelInfo, rate, 0, fgsWTTable);
                                    if (getUserModelInfo.get_kc_op_odds().Equals(1))
                                    {
                                        fgsWTTable = base.GetFgsWTTable(7);
                                    }
                                    CallBLL.cz_autosale_pcdd_bll.GDAutoSale(_kc.get_order_num(), str10, getUserModelInfo.get_u_nicker(), getUserModelInfo.get_u_name(), str15, table9.Rows[0]["play_id"].ToString(), str29.Trim(), str17, str18, str19, str20, str21, str22, str13, str12, _kc.get_ip(), num, _kc.get_lottery_name(), getUserModelInfo, rate, 0, fgsWTTable);
                                    if (getUserModelInfo.get_kc_op_odds().Equals(1))
                                    {
                                        fgsWTTable = base.GetFgsWTTable(7);
                                    }
                                    CallBLL.cz_autosale_pcdd_bll.FGSAutoSale(_kc.get_order_num(), str10, getUserModelInfo.get_u_nicker(), getUserModelInfo.get_u_name(), str15, table9.Rows[0]["play_id"].ToString(), str29.Trim(), str17, str18, str19, str20, str21, str22, str13, str12, _kc.get_ip(), num, _kc.get_lottery_name(), getUserModelInfo, rate, 0, fgsWTTable);
                                    if (FileCacheHelper.get_AddBetLockUType_KC().Equals("zj"))
                                    {
                                        num41 = 7;
                                        base.Un_Bet_Lock(num41.ToString(), getUserModelInfo.get_zjname(), str29);
                                    }
                                    else
                                    {
                                        num41 = 7;
                                        base.Un_Bet_Lock(num41.ToString(), rate.get_fgsname(), str29);
                                    }
                                    num16++;
                                    if (FileCacheHelper.get_GetKCPutMoneyCache() == "1")
                                    {
                                        base.SetUserDrawback_pcdd(dataTable, num40.ToString());
                                    }
                                }
                                base.SetUserRate_kc(rate);
                                if (FileCacheHelper.get_GetKCPutMoneyCache() != "1")
                                {
                                    base.SetUserDrawback_pcdd(dataTable);
                                }
                                getUserModelInfo.set_begin_kc("yes");
                                if (flag3)
                                {
                                    base.DeleteCreditLock(str);
                                    List<FastSale> list5 = base.UserPutBet(plDT, successBetList, strArray3, strArray4, strArray5);
                                    result.set_tipinfo(string.Format("下注提示！", new object[0]));
                                    result.set_success(210);
                                    dictionary.Add("JeuValidate", base.get_JeuValidate());
                                    dictionary.Add("returnlist", list5);
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
                            }
                            else
                            {
                                nullable = null;
                                string str34 = LSRequest.qq("LM_Type");
                                string str35 = LSRequest.qq("NoS");
                                if (!base.checkVal(num, ref str35))
                                {
                                    base.DeleteCreditLock(str);
                                    result.set_tipinfo(string.Format("[{0}]你選擇的號碼不能構成有效注單，請重新選擇!", str26));
                                    result.set_success(400);
                                    dictionary.Add("JeuValidate", base.get_JeuValidate());
                                    result.set_data(dictionary);
                                    strResult = JsonHandle.ObjectToJson(result);
                                }
                                else
                                {
                                    int length = 0;
                                    str49 = str34;
                                    if ((str49 != null) && (str49 == "0"))
                                    {
                                        if (item == "72055")
                                        {
                                            str32 = base.getfz(str35, 3);
                                            str33 = get_ball_pl(str23, double.Parse(str7), str35);
                                        }
                                        length = str32.Split(new char[] { '~' }).Length;
                                        if (length == 0)
                                        {
                                            base.DeleteCreditLock(str);
                                            result.set_tipinfo(string.Format("[{0}]你選擇的號碼不能構成有效注單，請重新選擇!", str26));
                                            result.set_success(400);
                                            dictionary.Add("JeuValidate", base.get_JeuValidate());
                                            result.set_data(dictionary);
                                            strResult = JsonHandle.ObjectToJson(result);
                                        }
                                        else if ((length * double.Parse(str8)) == double.Parse(str9))
                                        {
                                            double num21 = num12 - double.Parse(str8);
                                            string str36 = "";
                                            string str37 = "";
                                            string[] strArray6 = str32.Split(new char[] { '~' });
                                            int num22 = 0;
                                            foreach (string str38 in strArray6)
                                            {
                                                if (num22 == 0)
                                                {
                                                    str36 = str36 + "'" + px(str38) + "'";
                                                    str37 = str37 + px(str38);
                                                }
                                                else
                                                {
                                                    str36 = str36 + ",'" + px(str38) + "'";
                                                    str37 = str37 + "|" + px(str38);
                                                }
                                                num22++;
                                            }
                                            DataTable table8 = CallBLL.cz_bet_kc_bll.DQLMValid(int.Parse(item), getUserModelInfo.get_u_name(), str37, num21, "pcdd", str13);
                                            if (table8 != null)
                                            {
                                                base.DeleteCreditLock(str);
                                                result.set_tipinfo(string.Format("單組【" + table8.Rows[0][0].ToString().Trim() + "】單期下註金額超出單期金額[" + num12.ToString() + "]的限制！", new object[0]));
                                                result.set_success(400);
                                                dictionary.Add("JeuValidate", base.get_JeuValidate());
                                                result.set_data(dictionary);
                                                strResult = JsonHandle.ObjectToJson(result);
                                            }
                                            else
                                            {
                                                double num23 = double.Parse(str8) * length;
                                                if (double.Parse(str8) > num10)
                                                {
                                                    base.DeleteCreditLock(str);
                                                    result.set_tipinfo(string.Format("{0}下注金額超出單注最大金額！", str26));
                                                    result.set_success(500);
                                                    dictionary.Add("JeuValidate", base.get_JeuValidate());
                                                    result.set_data(dictionary);
                                                    strResult = JsonHandle.ObjectToJson(result);
                                                }
                                                else
                                                {
                                                    if (FileCacheHelper.get_AddBetLockUType_KC().Equals("zj"))
                                                    {
                                                        num41 = 7;
                                                        base.Add_Bet_Lock(num41.ToString(), getUserModelInfo.get_zjname(), item);
                                                    }
                                                    table9 = CallBLL.cz_odds_pcdd_bll.GetOddsByID(item).Tables[0];
                                                    string str39 = table9.Rows[0]["play_id"].ToString();
                                                    str40 = table9.Rows[0]["play_name"].ToString();
                                                    str41 = table9.Rows[0]["put_amount"].ToString();
                                                    str42 = table9.Rows[0]["ratio"].ToString();
                                                    string str43 = base.GetLmGroup()[str39 + "_" + 7];
                                                    int num24 = int.Parse(str43.Split(new char[] { ',' })[1]);
                                                    if (str32.Split(new char[] { '~' }).Length > num24)
                                                    {
                                                        base.DeleteCreditLock(str);
                                                        if (FileCacheHelper.get_AddBetLockUType_KC().Equals("zj"))
                                                        {
                                                            num41 = 7;
                                                            base.Un_Bet_Lock(num41.ToString(), getUserModelInfo.get_zjname(), item);
                                                        }
                                                        else
                                                        {
                                                            num41 = 7;
                                                            base.Un_Bet_Lock(num41.ToString(), rate.get_fgsname(), item);
                                                        }
                                                        result.set_tipinfo(string.Format("您選擇的號碼組合超過了最大{0}組，請重新選擇號碼！", num24));
                                                        result.set_success(400);
                                                        dictionary.Add("JeuValidate", base.get_JeuValidate());
                                                        result.set_data(dictionary);
                                                        strResult = JsonHandle.ObjectToJson(result);
                                                    }
                                                    else
                                                    {
                                                        if (FileCacheHelper.get_GetKCPutMoneyCache() == "1")
                                                        {
                                                            dataTable = base.GetUserDrawback_pcdd(rate, getUserModelInfo.get_kc_kind(), str39.ToString());
                                                        }
                                                        else
                                                        {
                                                            dataTable = base.GetUserDrawback_pcdd(rate, getUserModelInfo.get_kc_kind());
                                                        }
                                                        if (dataTable == null)
                                                        {
                                                            base.DeleteCreditLock(str);
                                                            if (FileCacheHelper.get_AddBetLockUType_KC().Equals("zj"))
                                                            {
                                                                num41 = 7;
                                                                base.Un_Bet_Lock(num41.ToString(), getUserModelInfo.get_zjname(), item);
                                                            }
                                                            else
                                                            {
                                                                num41 = 7;
                                                                base.Un_Bet_Lock(num41.ToString(), rate.get_fgsname(), item);
                                                            }
                                                            result.set_tipinfo(string.Format("系統錯誤！", new object[0]));
                                                            result.set_success(400);
                                                            dictionary.Add("JeuValidate", base.get_JeuValidate());
                                                            result.set_data(dictionary);
                                                            strResult = JsonHandle.ObjectToJson(result);
                                                        }
                                                        else
                                                        {
                                                            rowArray2 = dataTable.Select(string.Format(" play_id={0} ", str39));
                                                            foreach (DataRow row in rowArray2)
                                                            {
                                                                str49 = row["u_type"].ToString().Trim();
                                                                if (str49 != null)
                                                                {
                                                                    if (!(str49 == "zj"))
                                                                    {
                                                                        if (str49 == "fgs")
                                                                        {
                                                                            goto Label_1C06;
                                                                        }
                                                                        if (str49 == "gd")
                                                                        {
                                                                            goto Label_1C2A;
                                                                        }
                                                                        if (str49 == "zd")
                                                                        {
                                                                            goto Label_1C4E;
                                                                        }
                                                                        if (str49 == "dl")
                                                                        {
                                                                            goto Label_1C72;
                                                                        }
                                                                        if (str49 == "hy")
                                                                        {
                                                                            goto Label_1C96;
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        str17 = row[str15 + "_drawback"].ToString().Trim();
                                                                    }
                                                                }
                                                                continue;
                                                            Label_1C06:
                                                                str18 = row[str15 + "_drawback"].ToString().Trim();
                                                                continue;
                                                            Label_1C2A:
                                                                str19 = row[str15 + "_drawback"].ToString().Trim();
                                                                continue;
                                                            Label_1C4E:
                                                                str20 = row[str15 + "_drawback"].ToString().Trim();
                                                                continue;
                                                            Label_1C72:
                                                                str21 = row[str15 + "_drawback"].ToString().Trim();
                                                                continue;
                                                            Label_1C96:
                                                                if (getUserModelInfo.get_su_type() == "dl")
                                                                {
                                                                    str22 = row[str15 + "_drawback"].ToString().Trim();
                                                                }
                                                                else if (getUserModelInfo.get_su_type() == "zd")
                                                                {
                                                                    str21 = row[str15 + "_drawback"].ToString().Trim();
                                                                    str22 = row[str15 + "_drawback"].ToString().Trim();
                                                                }
                                                                else if (getUserModelInfo.get_su_type() == "gd")
                                                                {
                                                                    str20 = row[str15 + "_drawback"].ToString().Trim();
                                                                    str22 = row[str15 + "_drawback"].ToString().Trim();
                                                                }
                                                                else if (getUserModelInfo.get_su_type() == "fgs")
                                                                {
                                                                    str19 = row[str15 + "_drawback"].ToString().Trim();
                                                                    str22 = row[str15 + "_drawback"].ToString().Trim();
                                                                }
                                                            }
                                                            if (!FileCacheHelper.get_AddBetLockUType_KC().Equals("zj"))
                                                            {
                                                                num41 = 7;
                                                                base.Add_Bet_Lock(num41.ToString(), rate.get_fgsname(), item);
                                                            }
                                                            num43 = double.Parse(table9.Rows[0]["current_odds"].ToString()) + Convert.ToDouble(table9.Rows[0][str15 + "_diff"].ToString().Trim());
                                                            str44 = num43.ToString();
                                                            str45 = str44.ToString();
                                                            base.GetOdds_KC(7, item, ref str45);
                                                            playDrawbackValue = base.GetPlayDrawbackValue(str18, str42);
                                                            if ((playDrawbackValue != 0.0) && (double.Parse(str44) > playDrawbackValue))
                                                            {
                                                                base.DeleteCreditLock(str);
                                                                if (FileCacheHelper.get_AddBetLockUType_KC().Equals("zj"))
                                                                {
                                                                    num41 = 7;
                                                                    base.Un_Bet_Lock(num41.ToString(), getUserModelInfo.get_zjname(), item);
                                                                }
                                                                else
                                                                {
                                                                    num41 = 7;
                                                                    base.Un_Bet_Lock(num41.ToString(), rate.get_fgsname(), item);
                                                                }
                                                                result.set_tipinfo(string.Format("賠率錯誤！", new object[0]));
                                                                result.set_success(400);
                                                                dictionary.Add("JeuValidate", base.get_JeuValidate());
                                                                result.set_data(dictionary);
                                                                strResult = JsonHandle.ObjectToJson(result);
                                                            }
                                                            else
                                                            {
                                                                if (!nullable.HasValue)
                                                                {
                                                                    nullable = new DateTime?(DateTime.Now);
                                                                }
                                                                _kc = new cz_bet_kc();
                                                                _kc.set_order_num(Utils.GetOrderNumber());
                                                                _kc.set_checkcode(str10);
                                                                _kc.set_u_name(getUserModelInfo.get_u_name());
                                                                _kc.set_u_nicker(getUserModelInfo.get_u_nicker());
                                                                _kc.set_phase_id(new int?(int.Parse(str13)));
                                                                _kc.set_phase(str12);
                                                                _kc.set_bet_time(nullable);
                                                                _kc.set_odds_id(new int?(int.Parse(item)));
                                                                _kc.set_category(table9.Rows[0]["category"].ToString());
                                                                _kc.set_play_id(new int?(int.Parse(str39)));
                                                                _kc.set_play_name(str40);
                                                                _kc.set_bet_val(str35);
                                                                _kc.set_bet_wt(str33);
                                                                _kc.set_bet_group(str32);
                                                                _kc.set_odds(str45);
                                                                _kc.set_amount(new decimal?(decimal.Parse(num23.ToString())));
                                                                _kc.set_profit(0);
                                                                _kc.set_unit_cnt(new int?(length));
                                                                _kc.set_lm_type(new int?(int.Parse(str34)));
                                                                _kc.set_hy_drawback(new decimal?(decimal.Parse(str22)));
                                                                _kc.set_dl_drawback(new decimal?(decimal.Parse(str21)));
                                                                _kc.set_zd_drawback(new decimal?(decimal.Parse(str20)));
                                                                _kc.set_gd_drawback(new decimal?(decimal.Parse(str19)));
                                                                _kc.set_fgs_drawback(new decimal?(decimal.Parse(str18)));
                                                                _kc.set_zj_drawback(new decimal?(decimal.Parse(str17)));
                                                                _kc.set_dl_rate((float) int.Parse(rate.get_dlzc()));
                                                                _kc.set_zd_rate((float) int.Parse(rate.get_zdzc()));
                                                                _kc.set_gd_rate((float) int.Parse(rate.get_gdzc()));
                                                                _kc.set_fgs_rate(float.Parse(num7.ToString()));
                                                                _kc.set_zj_rate(float.Parse(num6.ToString()));
                                                                _kc.set_dl_name(rate.get_dlname());
                                                                _kc.set_zd_name(rate.get_zdname());
                                                                _kc.set_gd_name(rate.get_gdname());
                                                                _kc.set_fgs_name(rate.get_fgsname());
                                                                _kc.set_is_payment(0);
                                                                _kc.set_sale_type(0);
                                                                _kc.set_m_type(new int?(num3));
                                                                _kc.set_kind(str15);
                                                                _kc.set_ip(LSRequest.GetIP());
                                                                _kc.set_lottery_type(new int?(num));
                                                                _kc.set_lottery_name(base.GetGameNameByID(_kc.get_lottery_type().ToString()));
                                                                _kc.set_isLM(1);
                                                                _kc.set_odds_zj(str44);
                                                                _kc.set_ordervalidcode(Utils.GetOrderValidCode(_kc.get_u_name(), _kc.get_order_num(), _kc.get_bet_val(), _kc.get_odds(), _kc.get_kind(), Convert.ToInt32(_kc.get_phase_id()), Convert.ToInt32(_kc.get_odds_id()), Convert.ToDouble(_kc.get_amount())));
                                                                num26 = 0;
                                                                if (!CallBLL.cz_bet_kc_bll.AddBet(_kc, decimal.Parse(num23.ToString()), str, ref num26) || (num26 <= 0))
                                                                {
                                                                    base.DeleteCreditLock(str);
                                                                    if (FileCacheHelper.get_AddBetLockUType_KC().Equals("zj"))
                                                                    {
                                                                        num41 = 7;
                                                                        base.Un_Bet_Lock(num41.ToString(), getUserModelInfo.get_zjname(), item);
                                                                    }
                                                                    else
                                                                    {
                                                                        num41 = 7;
                                                                        base.Un_Bet_Lock(num41.ToString(), rate.get_fgsname(), item);
                                                                    }
                                                                    result.set_tipinfo(string.Format("系統錯誤！", new object[0]));
                                                                    result.set_success(400);
                                                                    dictionary.Add("JeuValidate", base.get_JeuValidate());
                                                                    result.set_data(dictionary);
                                                                    strResult = JsonHandle.ObjectToJson(result);
                                                                }
                                                                else
                                                                {
                                                                    double num27 = double.Parse(str8);
                                                                    double num28 = 0.0;
                                                                    double num29 = 0.0;
                                                                    string[] strArray7 = str45.Split(new char[] { ',' });
                                                                    num28 = double.Parse(strArray7[0].ToString());
                                                                    if (strArray7.Length > 1)
                                                                    {
                                                                        num29 = double.Parse(strArray7[1].ToString());
                                                                    }
                                                                    double num30 = 0.0;
                                                                    double num31 = 0.0;
                                                                    string[] strArray8 = str44.Split(new char[] { ',' });
                                                                    num30 = double.Parse(strArray8[0].ToString());
                                                                    if (strArray8.Length > 1)
                                                                    {
                                                                        num31 = double.Parse(strArray8[1].ToString());
                                                                    }
                                                                    string[] strArray9 = str32.Split(new char[] { '~' });
                                                                    string str46 = "";
                                                                    cz_splitgroup_pcdd _pcdd = new cz_splitgroup_pcdd();
                                                                    _pcdd.set_odds_id(new int?(int.Parse(item)));
                                                                    _pcdd.set_item_money(new decimal?((decimal) num27));
                                                                    _pcdd.set_odds1(new decimal?(Convert.ToDecimal(num28.ToString())));
                                                                    _pcdd.set_odds2(new decimal?(Convert.ToDecimal(num29.ToString())));
                                                                    _pcdd.set_bet_id(new int?(num26));
                                                                    _pcdd.set_odds1_zj(new decimal?(Convert.ToDecimal(num30.ToString())));
                                                                    _pcdd.set_odds2_zj(new decimal?(Convert.ToDecimal(num31.ToString())));
                                                                    foreach (string str47 in strArray9)
                                                                    {
                                                                        str46 = px(str47);
                                                                        _pcdd.set_item(str46);
                                                                        if (CallBLL.cz_splitgroup_pcdd_bll.AddSplitGroup(_pcdd) == 0)
                                                                        {
                                                                            base.DeleteCreditLock(str);
                                                                            if (FileCacheHelper.get_AddBetLockUType_KC().Equals("zj"))
                                                                            {
                                                                                num41 = 7;
                                                                                base.Un_Bet_Lock(num41.ToString(), getUserModelInfo.get_zjname(), item);
                                                                            }
                                                                            else
                                                                            {
                                                                                num41 = 7;
                                                                                base.Un_Bet_Lock(num41.ToString(), rate.get_fgsname(), item);
                                                                            }
                                                                            result.set_tipinfo(string.Format("系統錯誤！", new object[0]));
                                                                            result.set_success(400);
                                                                            dictionary.Add("JeuValidate", base.get_JeuValidate());
                                                                            result.set_data(dictionary);
                                                                            strResult = JsonHandle.ObjectToJson(result);
                                                                            return;
                                                                        }
                                                                    }
                                                                    num33 = (double.Parse(num23.ToString()) * num6) / 100.0;
                                                                    CallBLL.cz_odds_pcdd_bll.UpdateGrandTotal(Convert.ToDecimal(num33), int.Parse(item.Trim()));
                                                                    num34 = double.Parse(table9.Rows[0]["grand_total"].ToString()) + num33;
                                                                    num35 = double.Parse(table9.Rows[0]["downbase"].ToString());
                                                                    num36 = Math.Floor((double) (num34 / num35));
                                                                    if ((num36 >= 1.0) && (num35 >= 1.0))
                                                                    {
                                                                        num37 = double.Parse(table9.Rows[0]["down_odds_rate"].ToString()) * num36;
                                                                        num39 = CallBLL.cz_odds_pcdd_bll.UpdateGrandTotalCurrentOdds(num37.ToString(), (num34 - (num35 * num36)).ToString(), item);
                                                                        _log = new cz_system_log();
                                                                        _log.set_user_name("系統");
                                                                        _log.set_children_name("");
                                                                        _log.set_category(table9.Rows[0]["category"].ToString());
                                                                        _log.set_play_name(str40);
                                                                        _log.set_put_amount(str41);
                                                                        num41 = 7;
                                                                        _log.set_l_name(base.GetGameNameByID(num41.ToString()));
                                                                        _log.set_l_phase(str12);
                                                                        _log.set_action("降賠率");
                                                                        _log.set_odds_id(int.Parse(item));
                                                                        str48 = table9.Rows[0]["current_odds"].ToString();
                                                                        _log.set_old_val(str48);
                                                                        num43 = double.Parse(str48) - num37;
                                                                        _log.set_new_val(num43.ToString());
                                                                        _log.set_ip(LSRequest.GetIP());
                                                                        _log.set_add_time(DateTime.Now);
                                                                        _log.set_note("系統自動降賠");
                                                                        _log.set_type_id(Convert.ToInt32((LSEnums.LogTypeID) 0));
                                                                        _log.set_lottery_type(7);
                                                                        CallBLL.cz_system_log_bll.Add(_log);
                                                                        _odds = new cz_jp_odds();
                                                                        _odds.set_add_time(DateTime.Now);
                                                                        _odds.set_odds_id(int.Parse(item));
                                                                        _odds.set_phase_id(int.Parse(str13));
                                                                        _odds.set_play_name(str40);
                                                                        _odds.set_put_amount(str41);
                                                                        _odds.set_odds(num37.ToString());
                                                                        _odds.set_lottery_type(7);
                                                                        _odds.set_phase(str12);
                                                                        _odds.set_old_odds(str48);
                                                                        _odds.set_new_odds((double.Parse(str48) - num37).ToString());
                                                                        CallBLL.cz_jp_odds_bll.Add(_odds);
                                                                    }
                                                                    fgsWTTable = null;
                                                                    if (getUserModelInfo.get_kc_op_odds().Equals(1))
                                                                    {
                                                                        fgsWTTable = base.GetFgsWTTable(7);
                                                                    }
                                                                    CallBLL.cz_autosale_pcdd_bll.DLAutoSaleLM(_kc.get_order_num(), str10, int.Parse(table9.Rows[0]["play_id"].ToString()), int.Parse(item.Trim()), double.Parse(_kc.get_odds()), num, _kc.get_lottery_name(), int.Parse(str13), str12, str17, str18, str19, str20, str21, str22, _kc.get_ip(), getUserModelInfo, rate, 1, str15, fgsWTTable);
                                                                    if (getUserModelInfo.get_kc_op_odds().Equals(1))
                                                                    {
                                                                        fgsWTTable = base.GetFgsWTTable(7);
                                                                    }
                                                                    CallBLL.cz_autosale_pcdd_bll.ZDAutoSaleLM(_kc.get_order_num(), str10, int.Parse(table9.Rows[0]["play_id"].ToString()), int.Parse(item.Trim()), double.Parse(_kc.get_odds()), num, _kc.get_lottery_name(), int.Parse(str13), str12, str17, str18, str19, str20, str21, str22, _kc.get_ip(), getUserModelInfo, rate, 1, str15, fgsWTTable);
                                                                    if (getUserModelInfo.get_kc_op_odds().Equals(1))
                                                                    {
                                                                        fgsWTTable = base.GetFgsWTTable(7);
                                                                    }
                                                                    CallBLL.cz_autosale_pcdd_bll.GDAutoSaleLM(_kc.get_order_num(), str10, int.Parse(table9.Rows[0]["play_id"].ToString()), int.Parse(item.Trim()), double.Parse(_kc.get_odds()), num, _kc.get_lottery_name(), int.Parse(str13), str12, str17, str18, str19, str20, str21, str22, _kc.get_ip(), getUserModelInfo, rate, 1, str15, fgsWTTable);
                                                                    if (getUserModelInfo.get_kc_op_odds().Equals(1))
                                                                    {
                                                                        fgsWTTable = base.GetFgsWTTable(7);
                                                                    }
                                                                    CallBLL.cz_autosale_pcdd_bll.FGSAutoSaleLM(_kc.get_order_num(), str10, int.Parse(table9.Rows[0]["play_id"].ToString()), int.Parse(item.Trim()), double.Parse(_kc.get_odds()), num, _kc.get_lottery_name(), int.Parse(str13), str12, str17, str18, str19, str20, str21, str22, _kc.get_ip(), getUserModelInfo, rate, 1, str15, fgsWTTable);
                                                                    if (FileCacheHelper.get_AddBetLockUType_KC().Equals("zj"))
                                                                    {
                                                                        num41 = 7;
                                                                        base.Un_Bet_Lock(num41.ToString(), getUserModelInfo.get_zjname(), item);
                                                                    }
                                                                    else
                                                                    {
                                                                        base.Un_Bet_Lock(7.ToString(), rate.get_fgsname(), item);
                                                                    }
                                                                    base.DeleteCreditLock(str);
                                                                    result.set_tipinfo(string.Format("下注成功！", new object[0]));
                                                                    result.set_success(200);
                                                                    dictionary.Add("JeuValidate", base.get_JeuValidate());
                                                                    result.set_data(dictionary);
                                                                    strResult = JsonHandle.ObjectToJson(result);
                                                                    base.SetUserRate_kc(rate);
                                                                    if (FileCacheHelper.get_GetKCPutMoneyCache() == "1")
                                                                    {
                                                                        base.SetUserDrawback_pcdd(dataTable, str39);
                                                                    }
                                                                    else
                                                                    {
                                                                        base.SetUserDrawback_pcdd(dataTable);
                                                                    }
                                                                    getUserModelInfo.set_begin_kc("yes");
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            base.DeleteCreditLock(str);
                                            result.set_tipinfo(string.Format("[{0}]你下注金额不能構成有效注單，請确认!", str26));
                                            result.set_success(500);
                                            dictionary.Add("JeuValidate", base.get_JeuValidate());
                                            result.set_data(dictionary);
                                            strResult = JsonHandle.ObjectToJson(result);
                                        }
                                    }
                                    else
                                    {
                                        base.DeleteCreditLock(str);
                                        result.set_tipinfo(string.Format("[{0}]非法操作!", "連碼"));
                                        result.set_success(400);
                                        dictionary.Add("JeuValidate", base.get_JeuValidate());
                                        result.set_data(dictionary);
                                        strResult = JsonHandle.ObjectToJson(result);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        public static double get_ball_min_pl(DataTable myDT, string nos, double pl)
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

        public static string get_ball_pl(string playid, double pl, string nos)
        {
            List<string> list = new List<string>();
            DataTable table = CallBLL.cz_wt_pcdd_bll.GetWtByPlayID(int.Parse(playid), nos).Tables[0];
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
                            list.Add(row["number"].ToString().Trim() + "|" + ((pl + double.Parse(s))).ToString());
                            break;
                        }
                    }
                }
            }
            return string.Join("~", list.ToArray());
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
                string str6 = getUserModelInfo.get_kc_kind().Trim();
                ReturnResult result = new ReturnResult();
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                dictionary.Add("type", "get_oddsinfo");
                dictionary.Add("playpage", str2);
                DataTable table = CallBLL.cz_users_bll.GetCredit(str5, str4).Tables[0];
                string str7 = table.Rows[0]["kc_credit"].ToString();
                string s = table.Rows[0]["kc_usable_credit"].ToString();
                dictionary.Add("credit", Convert.ToDouble(str7).ToString());
                dictionary.Add("usable_credit", (int) double.Parse(s));
                DataSet set2 = CallBLL.cz_play_pcdd_bll.GetPlay(str, str5, str4, str2);
                if (set2 == null)
                {
                    context.Response.End();
                }
                else
                {
                    DataTable table2 = set2.Tables["phase"];
                    DataTable table3 = set2.Tables["odds"];
                    DataTable table4 = set2.Tables["wtTable"];
                    if ((table2 == null) || (table3 == null))
                    {
                        context.Response.End();
                    }
                    else
                    {
                        DataRow row = table2.Rows[0];
                        string str9 = row["openning"].ToString();
                        string str10 = row["isopen"].ToString();
                        string str11 = row["opendate"].ToString();
                        string str12 = row["endtime"].ToString();
                        string str13 = row["nn"].ToString();
                        string str14 = row["p_id"].ToString();
                        dictionary.Add("openning", str9);
                        dictionary.Add("isopen", str10);
                        dictionary.Add("drawopen_time", str11);
                        dictionary.Add("stop_time", str12);
                        dictionary.Add("nn", str13);
                        dictionary.Add("p_id", str14);
                        dictionary.Add("profit", base.GetKCProfit());
                        DataTable drawbackByPlayIds = null;
                        if (FileCacheHelper.get_GetKCPutMoneyCache() == "1")
                        {
                            foreach (string str15 in str.Split(new char[] { ',' }))
                            {
                                DataTable table7;
                                if (CacheHelper.GetCache("kc_drawback_FileCacheKey_pcdd" + str15 + context.Session["user_name"].ToString()) != null)
                                {
                                    DataTable cache = CacheHelper.GetCache("kc_drawback_FileCacheKey_pcdd" + str15 + context.Session["user_name"].ToString()) as DataTable;
                                    if (drawbackByPlayIds == null)
                                    {
                                        drawbackByPlayIds = cache;
                                    }
                                    else
                                    {
                                        table7 = drawbackByPlayIds.Clone();
                                        table7 = cache;
                                        if (table7 != null)
                                        {
                                            drawbackByPlayIds.Merge(table7);
                                        }
                                    }
                                }
                                else if (drawbackByPlayIds == null)
                                {
                                    drawbackByPlayIds = CallBLL.cz_drawback_pcdd_bll.GetDrawbackByPlayIds(str15, getUserModelInfo.get_u_name());
                                }
                                else
                                {
                                    table7 = drawbackByPlayIds.Clone();
                                    table7 = CallBLL.cz_drawback_pcdd_bll.GetDrawbackByPlayIds(str15, getUserModelInfo.get_u_name());
                                    if (table7 != null)
                                    {
                                        drawbackByPlayIds.Merge(table7);
                                    }
                                }
                            }
                        }
                        else if (CacheHelper.GetCache("kc_drawback_FileCacheKey_pcdd" + this.Session["user_name"].ToString()) != null)
                        {
                            drawbackByPlayIds = CacheHelper.GetCache("kc_drawback_FileCacheKey_pcdd" + this.Session["user_name"]) as DataTable;
                        }
                        else
                        {
                            drawbackByPlayIds = CallBLL.cz_drawback_pcdd_bll.GetDrawbackByPlayIds(str, getUserModelInfo.get_u_name());
                        }
                        Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
                        Dictionary<string, object> dictionary3 = new Dictionary<string, object>();
                        List<double> source = new List<double>();
                        foreach (DataRow row2 in table3.Rows)
                        {
                            string key = "";
                            string pl = "";
                            string str18 = "";
                            string str19 = "";
                            string str20 = "";
                            string str21 = "";
                            string str22 = "";
                            if ((((str2 == "pcdd_lm") && (table4 != null)) && (table4.Rows.Count > 0)) && !row2["play_id"].ToString().Trim().Equals("71014"))
                            {
                                foreach (DataRow row3 in table4.Rows)
                                {
                                    if (row2["play_id"].ToString() == row3["play_id"].ToString())
                                    {
                                        source.Add(double.Parse(row3["wt_value"].ToString()));
                                    }
                                }
                            }
                            if (source.Count<double>(p => (p == 0.0)) == source.Count)
                            {
                                source.Clear();
                            }
                            key = row2["play_id"].ToString() + "_" + row2["odds_id"].ToString();
                            string str23 = row2["current_odds"].ToString();
                            if (str9 == "n")
                            {
                                str23 = "-";
                            }
                            string str24 = row2[str6 + "_diff"].ToString().Trim();
                            if (str23 != "-")
                            {
                                pl = (double.Parse(str23) + double.Parse(str24)).ToString();
                            }
                            else
                            {
                                pl = str23;
                            }
                            if (str23 != "-")
                            {
                                base.GetOdds_KC(7, row2["odds_id"].ToString(), ref pl);
                            }
                            DataRow[] rowArray = drawbackByPlayIds.Select(string.Format(" play_id={0} and u_name='{1}' ", row2["play_id"].ToString(), getUserModelInfo.get_u_name()));
                            string str25 = rowArray[0]["single_phase_amount"].ToString();
                            string str26 = rowArray[0]["single_max_amount"].ToString();
                            string str27 = rowArray[0]["single_min_amount"].ToString();
                            str18 = row2["allow_min_amount"].ToString();
                            str19 = row2["allow_max_amount"].ToString();
                            str20 = row2["max_appoint"].ToString();
                            str21 = row2["total_amount"].ToString();
                            str22 = row2["allow_max_put_amount"].ToString();
                            if (Convert.ToDecimal(str19) > Convert.ToDecimal(str22))
                            {
                                str19 = row2["allow_max_put_amount"].ToString();
                            }
                            if (Convert.ToDecimal(str19) > Convert.ToDecimal(str21))
                            {
                                str19 = row2["total_amount"].ToString();
                            }
                            if (double.Parse(str18) < double.Parse(str27))
                            {
                                str18 = str27;
                            }
                            if (double.Parse(str19) > double.Parse(str26))
                            {
                                str19 = str26;
                            }
                            dictionary3.Add("pl", pl);
                            dictionary3.Add("plx", new List<double>(source));
                            dictionary3.Add("min_amount", Convert.ToDouble(str18).ToString());
                            dictionary3.Add("max_amount", Convert.ToDouble(str19).ToString());
                            dictionary3.Add("top_amount", Convert.ToDouble(str20).ToString());
                            dictionary3.Add("dq_max_amount", Convert.ToDouble(str21).ToString());
                            dictionary3.Add("dh_max_amount", Convert.ToDouble(str22).ToString());
                            dictionary2.Add(key, new Dictionary<string, object>(dictionary3));
                            dictionary3.Clear();
                            source.Clear();
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
                ReturnResult result = new ReturnResult();
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                dictionary.Add("type", "get_openball");
                DataTable kCOpenBall = base.GetKCOpenBall(7);
                if (kCOpenBall != null)
                {
                    string str = kCOpenBall.Rows[0]["previousphase"].ToString().Trim();
                    string str2 = kCOpenBall.Rows[0]["sold"].ToString().Trim();
                    string str3 = kCOpenBall.Rows[0]["surplus"].ToString().Trim();
                    string str4 = kCOpenBall.Rows[0]["open_date"].ToString().Trim();
                    string str5 = kCOpenBall.Rows[0]["play_open_date"].ToString().Trim();
                    string dateDiff = LSKeys.get_PCDD_Phase_Interval().ToString() + "分鐘";
                    if (!(string.IsNullOrEmpty(str4) || string.IsNullOrEmpty(str5)))
                    {
                        dateDiff = Utils.GetDateDiff(str4, str5);
                    }
                    Dictionary<string, string> dictionary2 = new Dictionary<string, string>();
                    DataRow[] rowArray = base.GetLotteryList().Select(string.Format(" id={0} ", 7));
                    dictionary2.Add("intervaltime", dateDiff);
                    dictionary2.Add("begintime", rowArray[0]["begintime"].ToString());
                    dictionary2.Add("endtime", rowArray[0]["endtime"].ToString());
                    dictionary2.Add("sold", str2);
                    dictionary2.Add("surplus", str3);
                    string str7 = kCOpenBall.Rows[0]["nns"].ToString().Trim();
                    ArrayList list2 = new ArrayList();
                    list2.Add(str2);
                    list2.Add(str3);
                    ArrayList list = list2;
                    string[] strArray = str7.Split(new char[] { ',' }).ToArray<string>();
                    dictionary.Add("draw_phase", str);
                    dictionary.Add("draw_result", strArray);
                    dictionary.Add("phaseinfo", dictionary2);
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
                DataSet topBet = CallBLL.cz_bet_kc_bll.GetTopBet(num, str);
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
                    DataTable table = topBet.Tables[0];
                    foreach (DataRow row in table.Rows)
                    {
                        string str2 = row["lottery_name"].ToString();
                        string str3 = row["order_num"].ToString();
                        string str4 = row["phase"].ToString();
                        StringBuilder builder = new StringBuilder();
                        builder.Append("{");
                        builder.AppendFormat("\"play_name\": \"{0}\"", row["play_name"].ToString());
                        builder.AppendFormat(",\"bet_val\": \"{0}\"", row["bet_val"].ToString().Replace(",", "、"));
                        builder.Append("}");
                        string str5 = builder.ToString();
                        string str6 = "";
                        if (string.IsNullOrEmpty(row["unit_cnt"]) || (row["unit_cnt"]).Equals("0"))
                        {
                            str6 = string.Format("{0:F1}", float.Parse(row["amount"].ToString()));
                            list8.Add("");
                            list9.Add("");
                        }
                        else
                        {
                            str6 = string.Format("{0:F1}", float.Parse(row["amount"].ToString()) / ((float) int.Parse(row["unit_cnt"].ToString())));
                            list8.Add("復式");
                            list9.Add(row["lm_type"].ToString());
                        }
                        string str7 = string.Format(" {0}", row["odds"].ToString());
                        list6.Add(str7);
                        list.Add(str3);
                        list2.Add(str4);
                        list3.Add(str5);
                        list4.Add(str6);
                        list5.Add(str2);
                        list7.Add(row["unit_cnt"]);
                    }
                    dictionary.Add("order_num", list);
                    dictionary.Add("nn", list2);
                    dictionary.Add("item", list3);
                    dictionary.Add("odds", list6);
                    dictionary.Add("money", list4);
                    dictionary.Add("name", list5);
                    dictionary.Add("group", list7);
                    dictionary.Add("grouptext", list8);
                    dictionary.Add("lmtype", list9);
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
                Dictionary<string, object> dictionary2;
                ReturnResult result = new ReturnResult();
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                dictionary.Add("type", "get_ranklist");
                dictionary.Add("playpage", str);
                string str2 = "_pcdd_jqkj";
                string str3 = "_pcdd_lmcl";
                string str4 = "_pcdd_cql";
                List<object> cache = new List<object>();
                if (CacheHelper.GetCache("balance_kc_FileCacheKey" + str2) != null)
                {
                    cache = CacheHelper.GetCache("balance_kc_FileCacheKey" + str2) as List<object>;
                }
                else
                {
                    List<string> collection = new List<string>();
                    DataSet topPhase = CallBLL.cz_phase_pcdd_bll.GetTopPhase(5);
                    if (topPhase != null)
                    {
                        DataTable table = topPhase.Tables[0];
                        dictionary2 = new Dictionary<string, object>();
                        foreach (DataRow row in table.Rows)
                        {
                            string str6 = "";
                            string str7 = "";
                            string str8 = "";
                            string str9 = "";
                            string str10 = "";
                            string str11 = "";
                            string str12 = "";
                            str6 = row["phase"].ToString();
                            str7 = row["play_open_date"].ToString();
                            int num = Convert.ToInt32(row["n1"].ToString());
                            int num2 = Convert.ToInt32(row["n2"].ToString());
                            int num3 = Convert.ToInt32(row["n3"].ToString());
                            int num4 = Convert.ToInt32(row["sn"].ToString());
                            collection.Add(num.ToString());
                            collection.Add(num2.ToString());
                            collection.Add(num3.ToString());
                            collection.Add(num4.ToString());
                            str8 = PCDDPhase.get_dx(num4.ToString());
                            str8 = base.SetWordColor(str8);
                            str9 = PCDDPhase.get_ds(num4.ToString());
                            str9 = base.SetWordColor(str9);
                            str10 = PCDDPhase.get_bs(num4.ToString());
                            str10 = base.SetWordColor(str10);
                            str11 = PCDDPhase.get_jdx(num4.ToString());
                            str11 = base.SetWordColor(str11);
                            str12 = PCDDPhase.get_bz(num.ToString(), num2.ToString(), num3.ToString());
                            str12 = base.SetWordColor(str12);
                            dictionary2.Add("phase", str6);
                            dictionary2.Add("play_open_date", str7);
                            dictionary2.Add("draw_num", new List<string>(collection));
                            dictionary2.Add("total", new List<string> { str8, str9, str10, str11, str12 });
                            cache.Add(new Dictionary<string, object>(dictionary2));
                            dictionary2.Clear();
                            collection.Clear();
                        }
                    }
                    CacheHelper.SetCache("balance_kc_FileCacheKey" + str2, cache);
                    CacheHelper.SetPublicFileCache("balance_kc_FileCacheKey" + str2, cache, PageBase.GetPublicForderPath(base.get_KC_BalanceFileName()));
                }
                dictionary.Add("jqkj", cache);
                List<object> list4 = new List<object>();
                if (CacheHelper.GetCache("balance_kc_FileCacheKey" + str3) != null)
                {
                    list4 = CacheHelper.GetCache("balance_kc_FileCacheKey" + str3) as List<object>;
                }
                else
                {
                    DataTable table2 = CallBLL.cz_phase_pcdd_bll.GetChangLong().Tables[0];
                    if (table2.Rows.Count > 0)
                    {
                        dictionary2 = new Dictionary<string, object>();
                        foreach (DataRow row in table2.Rows)
                        {
                            string str13 = row["c_name"].ToString();
                            string str14 = row["c_qs"].ToString();
                            dictionary2.Add("cl_name", str13);
                            dictionary2.Add("cl_num", str14);
                            list4.Add(new Dictionary<string, object>(dictionary2));
                            dictionary2.Clear();
                        }
                    }
                    CacheHelper.SetCache("balance_kc_FileCacheKey" + str3, list4);
                    CacheHelper.SetPublicFileCache("balance_kc_FileCacheKey" + str3, list4, PageBase.GetPublicForderPath(base.get_KC_BalanceFileName()));
                }
                dictionary.Add("lmcl", list4);
                Dictionary<string, object> dictionary3 = new Dictionary<string, object>();
                if (CacheHelper.GetCache("balance_kc_FileCacheKey" + str4) != null)
                {
                    dictionary3 = CacheHelper.GetCache("balance_kc_FileCacheKey" + str4) as Dictionary<string, object>;
                }
                else
                {
                    Dictionary<string, string> paiHang = CallBLL.cz_phase_pcdd_bll.GetPaiHang(0x19);
                    if ((paiHang != null) && (paiHang.Count > 0))
                    {
                        ArrayList list5 = new ArrayList();
                        ArrayList list6 = new ArrayList();
                        foreach (KeyValuePair<string, string> pair in paiHang)
                        {
                            list5.Add(pair.Key);
                            list6.Add(pair.Value);
                        }
                        dictionary3.Add("title", list5);
                        dictionary3.Add("content", list6);
                    }
                    CacheHelper.SetCache("balance_kc_FileCacheKey" + str4, dictionary3);
                    CacheHelper.SetPublicFileCache("balance_kc_FileCacheKey" + str4, dictionary3, PageBase.GetPublicForderPath(base.get_KC_BalanceFileName()));
                }
                dictionary.Add("cql", dictionary3);
                result.set_data(dictionary);
                strResult = JsonHandle.ObjectToJson(result);
            }
        }

        public override void ProcessRequest(HttpContext context)
        {
            base.checkLogin();
            base.IsLotteryExist(7.ToString());
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
                    if (str3 == "get_openball")
                    {
                        this.get_openball(context, ref strResult);
                        goto Label_00E1;
                    }
                    if (str3 == "get_putinfo")
                    {
                        this.get_putinfo(context, ref strResult);
                        goto Label_00E1;
                    }
                    if (str3 == "get_ranklist")
                    {
                        this.get_ranklist(context, ref strResult);
                        goto Label_00E1;
                    }
                    if (str3 == "put_money")
                    {
                        this.put_money(context, ref strResult);
                        goto Label_00E1;
                    }
                }
                else
                {
                    this.get_oddsinfo(context, ref strResult);
                    goto Label_00E1;
                }
            }
            this.do_default(context, ref strResult);
        Label_00E1:
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

        public static string px(string p_str)
        {
            int num;
            string str = "";
            string[] strArray = p_str.Split(new char[] { ',' });
            int[] array = new int[strArray.Length];
            for (num = 0; num < strArray.Length; num++)
            {
                array[num] = Convert.ToInt32(strArray[num]);
            }
            Array.Sort<int>(array);
            for (num = 0; num < array.Length; num++)
            {
                str = str + array[num] + ",";
            }
            return str.Substring(0, str.Length - 1);
        }
    }
}

