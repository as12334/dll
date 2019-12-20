namespace User.Web.L_KL10.Handler
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
            DateTime? nullable;
            string str36;
            string str37;
            string str38;
            string str39;
            string str40;
            int num21;
            double num24;
            DataTable table9;
            string str46;
            string str47;
            string str48;
            string str49;
            DataRow[] rowArray2;
            string str51;
            string str52;
            double playDrawbackValue;
            cz_bet_kc _kc;
            int num27;
            double num34;
            double num35;
            double num36;
            double num37;
            double num38;
            int num40;
            cz_system_log _log;
            string str55;
            cz_jp_odds _odds;
            DataTable fgsWTTable;
            int num42;
            string str56;
            double num44;
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
            int num = 0;
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
            if (source.Length > FileCacheHelper.get_GetKL10MaxGroup())
            {
                base.DeleteCreditLock(str);
                result.set_success(400);
                result.set_tipinfo(string.Format("您選擇的號碼超過了最大{0}組，請重新選擇號碼!", FileCacheHelper.get_GetKL10MaxGroup()));
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
            DataTable table2 = CallBLL.cz_phase_kl10_bll.GetIsClosedByTime(int.Parse(s)).Tables[0];
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
                num42 = 0;
                string str14 = base.BetReceiveEnd(num42.ToString());
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
            DataTable plDT = CallBLL.cz_odds_kl10_bll.GetPlayOddsByID(item).Tables[0];
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
            DataTable table5 = CallBLL.cz_bet_kc_bll.GetSinglePhaseByBet(str12, str, num.ToString(), item, null).Tables[0];
            if ((plDT.Rows.Count == 0) || (plDT.Rows.Count != strArray3.Count<string>()))
            {
                base.DeleteCreditLock(str);
                result.set_success(400);
                result.set_tipinfo("系统错误!");
                dictionary.Add("JeuValidate", base.get_JeuValidate());
                result.set_data(dictionary);
                strResult = JsonHandle.ObjectToJson(result);
                return;
            }
            List<string> list = new List<string> { "329", "330", "331", "1181", "1200", "1201", "1202", "1203" };
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
                    if (CacheHelper.GetCache("kc_drawback_FileCacheKey_kl10" + str23 + this.Session["user_name"].ToString()) == null)
                    {
                        table7 = CallBLL.cz_drawback_kl10_bll.GetDrawback(str23, str).Tables[0];
                    }
                    else
                    {
                        table7 = base.GetUserDrawback_kl10(rate, getUserModelInfo.get_kc_kind(), str23);
                    }
                }
                else if (CacheHelper.GetCache("kc_drawback_FileCacheKey_kl10" + this.Session["user_name"].ToString()) == null)
                {
                    table7 = CallBLL.cz_drawback_kl10_bll.GetDrawback(str23, str).Tables[0];
                }
                else
                {
                    table7 = base.GetUserDrawback_kl10(rate, getUserModelInfo.get_kc_kind());
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
                base.GetOdds_KC(0, str29, ref pl);
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
                return;
            }
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
                return;
            }
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
                        num42 = 0;
                        base.Add_Bet_Lock(num42.ToString(), getUserModelInfo.get_zjname(), str29);
                    }
                    table9 = CallBLL.cz_odds_kl10_bll.GetOddsByID(str29).Tables[0];
                    int num41 = int.Parse(table9.Rows[0]["play_id"].ToString());
                    str47 = table9.Rows[0]["play_name"].ToString();
                    str48 = table9.Rows[0]["put_amount"].ToString();
                    str49 = table9.Rows[0]["ratio"].ToString();
                    if (FileCacheHelper.get_GetKCPutMoneyCache() == "1")
                    {
                        dataTable = base.GetUserDrawback_kl10(rate, getUserModelInfo.get_kc_kind(), num41.ToString());
                    }
                    else
                    {
                        dataTable = base.GetUserDrawback_kl10(rate, getUserModelInfo.get_kc_kind());
                    }
                    if (dataTable == null)
                    {
                        base.DeleteCreditLock(str);
                        if (FileCacheHelper.get_AddBetLockUType_KC().Equals("zj"))
                        {
                            num42 = 0;
                            base.Un_Bet_Lock(num42.ToString(), getUserModelInfo.get_zjname(), str29);
                        }
                        else
                        {
                            num42 = 0;
                            base.Un_Bet_Lock(num42.ToString(), rate.get_fgsname(), str29);
                        }
                        result.set_tipinfo(string.Format("系統錯誤！", new object[0]));
                        result.set_success(400);
                        dictionary.Add("JeuValidate", base.get_JeuValidate());
                        result.set_data(dictionary);
                        strResult = JsonHandle.ObjectToJson(result);
                        return;
                    }
                    rowArray2 = dataTable.Select(string.Format(" play_id={0} ", num41));
                    foreach (DataRow row in rowArray2)
                    {
                        str56 = row["u_type"].ToString().Trim();
                        if (str56 != null)
                        {
                            if (!(str56 == "zj"))
                            {
                                if (str56 == "fgs")
                                {
                                    goto Label_373F;
                                }
                                if (str56 == "gd")
                                {
                                    goto Label_3763;
                                }
                                if (str56 == "zd")
                                {
                                    goto Label_3787;
                                }
                                if (str56 == "dl")
                                {
                                    goto Label_37AB;
                                }
                                if (str56 == "hy")
                                {
                                    goto Label_37CF;
                                }
                            }
                            else
                            {
                                str17 = row[str15 + "_drawback"].ToString().Trim();
                            }
                        }
                        continue;
                    Label_373F:
                        str18 = row[str15 + "_drawback"].ToString().Trim();
                        continue;
                    Label_3763:
                        str19 = row[str15 + "_drawback"].ToString().Trim();
                        continue;
                    Label_3787:
                        str20 = row[str15 + "_drawback"].ToString().Trim();
                        continue;
                    Label_37AB:
                        str21 = row[str15 + "_drawback"].ToString().Trim();
                        continue;
                    Label_37CF:
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
                        num42 = 0;
                        base.Add_Bet_Lock(num42.ToString(), rate.get_fgsname(), str29);
                    }
                    num44 = double.Parse(table9.Rows[0]["current_odds"].ToString()) + Convert.ToDouble(table9.Rows[0][str15 + "_diff"].ToString().Trim());
                    str51 = num44.ToString();
                    str52 = str51.ToString();
                    base.GetOdds_KC(0, str29, ref str52);
                    playDrawbackValue = base.GetPlayDrawbackValue(str18, str49);
                    if ((playDrawbackValue != 0.0) && (double.Parse(str51) > playDrawbackValue))
                    {
                        base.DeleteCreditLock(str);
                        if (FileCacheHelper.get_AddBetLockUType_KC().Equals("zj"))
                        {
                            num42 = 0;
                            base.Un_Bet_Lock(num42.ToString(), getUserModelInfo.get_zjname(), str29);
                        }
                        else
                        {
                            num42 = 0;
                            base.Un_Bet_Lock(num42.ToString(), rate.get_fgsname(), str29);
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
                    _kc.set_play_name(str47);
                    _kc.set_bet_val(str48);
                    _kc.set_odds(str52);
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
                    _kc.set_odds_zj(str51);
                    num27 = 0;
                    if (!CallBLL.cz_bet_kc_bll.AddBet(_kc, decimal.Parse(strArray5[num16]), str, ref num27) || (num27 <= 0))
                    {
                        base.DeleteCreditLock(str);
                        if (FileCacheHelper.get_AddBetLockUType_KC().Equals("zj"))
                        {
                            num42 = 0;
                            base.Un_Bet_Lock(num42.ToString(), getUserModelInfo.get_zjname(), str29);
                        }
                        else
                        {
                            num42 = 0;
                            base.Un_Bet_Lock(num42.ToString(), rate.get_fgsname(), str29);
                        }
                        result.set_tipinfo(string.Format("系統錯誤！", new object[0]));
                        result.set_success(400);
                        dictionary.Add("JeuValidate", base.get_JeuValidate());
                        result.set_data(dictionary);
                        strResult = JsonHandle.ObjectToJson(result);
                        return;
                    }
                    successBetList.Add(string.Concat(new object[] { _kc.get_odds_id(), ",", _kc.get_order_num(), ",", _kc.get_play_name(), ",", _kc.get_bet_val(), ",", _kc.get_odds(), ",", _kc.get_amount() }));
                    num34 = (double.Parse(strArray5[num16]) * num6) / 100.0;
                    CallBLL.cz_odds_kl10_bll.UpdateGrandTotal(Convert.ToDecimal(num34), int.Parse(str29));
                    num35 = double.Parse(table9.Rows[0]["grand_total"].ToString()) + num34;
                    num36 = double.Parse(table9.Rows[0]["downbase"].ToString());
                    num37 = Math.Floor((double) (num35 / num36));
                    if ((num37 >= 1.0) && (num36 >= 1.0))
                    {
                        num38 = double.Parse(table9.Rows[0]["down_odds_rate"].ToString()) * num37;
                        double num39 = num35 - (num36 * num37);
                        num40 = CallBLL.cz_odds_kl10_bll.UpdateGrandTotalCurrentOdds(num38.ToString(), num39.ToString(), str29);
                        _log = new cz_system_log();
                        _log.set_user_name("系統");
                        _log.set_children_name("");
                        _log.set_category(table9.Rows[0]["category"].ToString());
                        _log.set_play_name(str47);
                        _log.set_put_amount(str48);
                        num42 = 0;
                        _log.set_l_name(base.GetGameNameByID(num42.ToString()));
                        _log.set_l_phase(str12);
                        _log.set_action("降賠率");
                        _log.set_odds_id(int.Parse(str29));
                        str55 = table9.Rows[0]["current_odds"].ToString();
                        _log.set_old_val(str55);
                        num44 = double.Parse(str55) - num38;
                        _log.set_new_val(num44.ToString());
                        _log.set_ip(LSRequest.GetIP());
                        _log.set_add_time(DateTime.Now);
                        _log.set_note("系統自動降賠");
                        _log.set_type_id(Convert.ToInt32((LSEnums.LogTypeID) 0));
                        _log.set_lottery_type(0);
                        CallBLL.cz_system_log_bll.Add(_log);
                        _odds = new cz_jp_odds();
                        _odds.set_add_time(DateTime.Now);
                        _odds.set_odds_id(int.Parse(str29));
                        _odds.set_phase_id(int.Parse(str13));
                        _odds.set_play_name(str47);
                        _odds.set_put_amount(str48);
                        _odds.set_odds(num38.ToString());
                        _odds.set_lottery_type(0);
                        _odds.set_phase(str12);
                        _odds.set_old_odds(str55);
                        num44 = double.Parse(str55) - num38;
                        _odds.set_new_odds(num44.ToString());
                        CallBLL.cz_jp_odds_bll.Add(_odds);
                    }
                    fgsWTTable = null;
                    if (getUserModelInfo.get_kc_op_odds().Equals(1))
                    {
                        fgsWTTable = base.GetFgsWTTable(0);
                    }
                    CallBLL.cz_autosale_kl10_bll.DLAutoSale(_kc.get_order_num(), str10, getUserModelInfo.get_u_nicker(), getUserModelInfo.get_u_name(), str15, table9.Rows[0]["play_id"].ToString(), str29.Trim(), str17, str18, str19, str20, str21, str22, str13, str12, _kc.get_ip(), num, _kc.get_lottery_name(), getUserModelInfo, rate, 0, fgsWTTable);
                    if (getUserModelInfo.get_kc_op_odds().Equals(1))
                    {
                        fgsWTTable = base.GetFgsWTTable(0);
                    }
                    CallBLL.cz_autosale_kl10_bll.ZDAutoSale(_kc.get_order_num(), str10, getUserModelInfo.get_u_nicker(), getUserModelInfo.get_u_name(), str15, table9.Rows[0]["play_id"].ToString(), str29.Trim(), str17, str18, str19, str20, str21, str22, str13, str12, _kc.get_ip(), num, _kc.get_lottery_name(), getUserModelInfo, rate, 0, fgsWTTable);
                    if (getUserModelInfo.get_kc_op_odds().Equals(1))
                    {
                        fgsWTTable = base.GetFgsWTTable(0);
                    }
                    CallBLL.cz_autosale_kl10_bll.GDAutoSale(_kc.get_order_num(), str10, getUserModelInfo.get_u_nicker(), getUserModelInfo.get_u_name(), str15, table9.Rows[0]["play_id"].ToString(), str29.Trim(), str17, str18, str19, str20, str21, str22, str13, str12, _kc.get_ip(), num, _kc.get_lottery_name(), getUserModelInfo, rate, 0, fgsWTTable);
                    if (getUserModelInfo.get_kc_op_odds().Equals(1))
                    {
                        fgsWTTable = base.GetFgsWTTable(0);
                    }
                    CallBLL.cz_autosale_kl10_bll.FGSAutoSale(_kc.get_order_num(), str10, getUserModelInfo.get_u_nicker(), getUserModelInfo.get_u_name(), str15, table9.Rows[0]["play_id"].ToString(), str29.Trim(), str17, str18, str19, str20, str21, str22, str13, str12, _kc.get_ip(), num, _kc.get_lottery_name(), getUserModelInfo, rate, 0, fgsWTTable);
                    if (FileCacheHelper.get_AddBetLockUType_KC().Equals("zj"))
                    {
                        num42 = 0;
                        base.Un_Bet_Lock(num42.ToString(), getUserModelInfo.get_zjname(), str29);
                    }
                    else
                    {
                        num42 = 0;
                        base.Un_Bet_Lock(num42.ToString(), rate.get_fgsname(), str29);
                    }
                    num16++;
                    if (FileCacheHelper.get_GetKCPutMoneyCache() == "1")
                    {
                        base.SetUserDrawback_kl10(dataTable, num41.ToString());
                    }
                }
                base.SetUserRate_kc(rate);
                if (FileCacheHelper.get_GetKCPutMoneyCache() != "1")
                {
                    base.SetUserDrawback_kl10(dataTable);
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
                return;
            }
            nullable = null;
            string str34 = LSRequest.qq("LM_Type");
            string str35 = LSRequest.qq("NoS");
            if (((item != "330") && (item != "1200")) && !base.checkVal(num, ref str35))
            {
                base.DeleteCreditLock(str);
                result.set_tipinfo(string.Format("[{0}]你選擇的號碼不能構成有效注單，請重新選擇!", str26));
                result.set_success(400);
                dictionary.Add("JeuValidate", base.get_JeuValidate());
                result.set_data(dictionary);
                strResult = JsonHandle.ObjectToJson(result);
                return;
            }
            int length = 0;
            str56 = str34;
            if ((str56 == null) || !(str56 == "0"))
            {
                base.DeleteCreditLock(str);
                result.set_tipinfo(string.Format("[{0}]非法操作!", "連碼"));
                result.set_success(400);
                dictionary.Add("JeuValidate", base.get_JeuValidate());
                result.set_data(dictionary);
                strResult = JsonHandle.ObjectToJson(result);
                return;
            }
            if (!(item == "330"))
            {
                string str41;
                string str42;
                switch (item)
                {
                    case "329":
                    case "331":
                        str32 = base.getfz(str35, 2);
                        str33 = get_ball_pl(str23, double.Parse(str7), str35);
                        goto Label_1AEF;

                    default:
                        switch (item)
                        {
                            case "1181":
                            case "1201":
                                str32 = base.getfz(str35, 3);
                                str33 = get_ball_pl(str23, double.Parse(str7), str35);
                                goto Label_1AEF;
                        }
                        if (!(item == "1200"))
                        {
                            if (item == "1202")
                            {
                                str32 = base.getfz(str35, 4);
                                str33 = get_ball_pl(str23, double.Parse(str7), str35);
                            }
                            else if (item == "1203")
                            {
                                str32 = base.getfz(str35, 5);
                                str33 = get_ball_pl(str23, double.Parse(str7), str35);
                            }
                            goto Label_1AEF;
                        }
                        str36 = "";
                        str37 = "";
                        str38 = "";
                        str41 = "";
                        str39 = base.getlongnum(str35.Split(new char[] { '|' })[0]);
                        str40 = base.getlongnum(str35.Split(new char[] { '|' })[1]);
                        str42 = base.getlongnum(str35.Split(new char[] { '|' })[2]);
                        num21 = 0;
                        break;
                }
            Label_18A6:;
                if (num21 < str39.Split(new char[] { ',' }).Length)
                {
                    str37 = str37 + str39.Split(new char[] { ',' })[num21] + ",";
                    num21++;
                    goto Label_18A6;
                }
                num21 = 0;
            Label_18FE:;
                if (num21 < str40.Split(new char[] { ',' }).Length)
                {
                    str38 = str38 + str40.Split(new char[] { ',' })[num21] + ",";
                    num21++;
                    goto Label_18FE;
                }
                num21 = 0;
            Label_1956:;
                if (num21 < str42.Split(new char[] { ',' }).Length)
                {
                    str41 = str41 + str42.Split(new char[] { ',' })[num21] + ",";
                    num21++;
                    goto Label_1956;
                }
                str37 = str37.Substring(0, str37.Length - 1);
                str38 = str38.Substring(0, str38.Length - 1);
                str41 = str41.Substring(0, str41.Length - 1);
                if (!((base.checkVal(num, ref str37) && base.checkVal(num, ref str38)) && base.checkVal(num, ref str41)))
                {
                    base.DeleteCreditLock(str);
                    result.set_tipinfo(string.Format("[{0}]你選擇的號碼不能構成有效注單，請重新選擇!", str26));
                    result.set_success(400);
                    dictionary.Add("JeuValidate", base.get_JeuValidate());
                    result.set_data(dictionary);
                    strResult = JsonHandle.ObjectToJson(result);
                    return;
                }
                str32 = base.getxsqz(str37, str38, str41);
                str33 = str32;
                string str57 = str36;
                str35 = str57 + str37 + "|" + str38 + "|" + str41;
                goto Label_1AEF;
            }
            str36 = "";
            str37 = "";
            str38 = "";
            str39 = base.getlongnum(str35.Split(new char[] { '|' })[0]);
            str40 = base.getlongnum(str35.Split(new char[] { '|' })[1]);
            num21 = 0;
        Label_1603:;
            if (num21 < str39.Split(new char[] { ',' }).Length)
            {
                str37 = str37 + str39.Split(new char[] { ',' })[num21] + ",";
                num21++;
                goto Label_1603;
            }
            num21 = 0;
        Label_165B:;
            if (num21 < str40.Split(new char[] { ',' }).Length)
            {
                str38 = str38 + str40.Split(new char[] { ',' })[num21] + ",";
                num21++;
                goto Label_165B;
            }
            str37 = str37.Substring(0, str37.Length - 1);
            str38 = str38.Substring(0, str38.Length - 1);
            if (!(base.checkVal(num, ref str37) && base.checkVal(num, ref str38)))
            {
                base.DeleteCreditLock(str);
                result.set_tipinfo(string.Format("下注球號規則有誤！", new object[0]));
                result.set_success(400);
                dictionary.Add("JeuValidate", base.get_JeuValidate());
                result.set_data(dictionary);
                strResult = JsonHandle.ObjectToJson(result);
                return;
            }
            str32 = base.getxrlzr(str37, str38);
            str33 = str32;
            str35 = str36 + str37 + "|" + str38;
        Label_1AEF:;
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
            else
            {
                if ((length * double.Parse(str8)) == double.Parse(str9))
                {
                    double num22 = num12 - double.Parse(str8);
                    string str43 = "";
                    string str44 = "";
                    string[] strArray6 = str32.Split(new char[] { '~' });
                    int num23 = 0;
                    foreach (string str45 in strArray6)
                    {
                        if (num23 == 0)
                        {
                            if (item.Equals("330") || item.Equals("1200"))
                            {
                                str43 = str43 + "'" + str45 + "'";
                                str44 = str44 + str45;
                            }
                            else
                            {
                                str43 = str43 + "'" + px(str45) + "'";
                                str44 = str44 + px(str45);
                            }
                        }
                        else if (item.Equals("330") || item.Equals("1200"))
                        {
                            str43 = str43 + ",'" + str45 + "'";
                            str44 = str44 + "|" + str45;
                        }
                        else
                        {
                            str43 = str43 + ",'" + px(str45) + "'";
                            str44 = str44 + "|" + px(str45);
                        }
                        num23++;
                    }
                    DataTable table8 = CallBLL.cz_bet_kc_bll.DQLMValid(int.Parse(item), getUserModelInfo.get_u_name(), str44, num22, "kl10", str13);
                    if (table8 != null)
                    {
                        base.DeleteCreditLock(str);
                        result.set_tipinfo(string.Format("單組【" + table8.Rows[0][0].ToString().Trim() + "】單期下註金額超出單期金額[" + num12.ToString() + "]的限制！", new object[0]));
                        result.set_success(400);
                        dictionary.Add("JeuValidate", base.get_JeuValidate());
                        result.set_data(dictionary);
                        strResult = JsonHandle.ObjectToJson(result);
                        return;
                    }
                    num24 = double.Parse(str8) * length;
                    if (double.Parse(str8) > num10)
                    {
                        base.DeleteCreditLock(str);
                        result.set_tipinfo(string.Format("{0}下注金額超出單注最大金額！", str26));
                        result.set_success(500);
                        dictionary.Add("JeuValidate", base.get_JeuValidate());
                        result.set_data(dictionary);
                        strResult = JsonHandle.ObjectToJson(result);
                        return;
                    }
                    if (FileCacheHelper.get_AddBetLockUType_KC().Equals("zj"))
                    {
                        num42 = 0;
                        base.Add_Bet_Lock(num42.ToString(), getUserModelInfo.get_zjname(), item);
                    }
                    table9 = CallBLL.cz_odds_kl10_bll.GetOddsByID(item).Tables[0];
                    str46 = table9.Rows[0]["play_id"].ToString();
                    str47 = table9.Rows[0]["play_name"].ToString();
                    str48 = table9.Rows[0]["put_amount"].ToString();
                    str49 = table9.Rows[0]["ratio"].ToString();
                    if (!str46.Equals("73") && !str46.Equals("76"))
                    {
                        string str50 = base.GetLmGroup()[str46 + "_" + 0];
                        int num25 = int.Parse(str50.Split(new char[] { ',' })[1]);
                        if (str32.Split(new char[] { '~' }).Length > num25)
                        {
                            base.DeleteCreditLock(str);
                            if (FileCacheHelper.get_AddBetLockUType_KC().Equals("zj"))
                            {
                                num42 = 0;
                                base.Un_Bet_Lock(num42.ToString(), getUserModelInfo.get_zjname(), item);
                            }
                            else
                            {
                                num42 = 0;
                                base.Un_Bet_Lock(num42.ToString(), rate.get_fgsname(), item);
                            }
                            result.set_tipinfo(string.Format("您選擇的號碼組合超過了最大{0}組，請重新選擇號碼！", num25));
                            result.set_success(400);
                            dictionary.Add("JeuValidate", base.get_JeuValidate());
                            result.set_data(dictionary);
                            strResult = JsonHandle.ObjectToJson(result);
                            return;
                        }
                    }
                    if (FileCacheHelper.get_GetKCPutMoneyCache() == "1")
                    {
                        dataTable = base.GetUserDrawback_kl10(rate, getUserModelInfo.get_kc_kind(), str46.ToString());
                    }
                    else
                    {
                        dataTable = base.GetUserDrawback_kl10(rate, getUserModelInfo.get_kc_kind());
                    }
                    if (dataTable == null)
                    {
                        base.DeleteCreditLock(str);
                        if (FileCacheHelper.get_AddBetLockUType_KC().Equals("zj"))
                        {
                            num42 = 0;
                            base.Un_Bet_Lock(num42.ToString(), getUserModelInfo.get_zjname(), item);
                        }
                        else
                        {
                            num42 = 0;
                            base.Un_Bet_Lock(num42.ToString(), rate.get_fgsname(), item);
                        }
                        result.set_tipinfo(string.Format("系統錯誤！", new object[0]));
                        result.set_success(400);
                        dictionary.Add("JeuValidate", base.get_JeuValidate());
                        result.set_data(dictionary);
                        strResult = JsonHandle.ObjectToJson(result);
                        return;
                    }
                    rowArray2 = dataTable.Select(string.Format(" play_id={0} ", str46));
                    foreach (DataRow row in rowArray2)
                    {
                        str56 = row["u_type"].ToString().Trim();
                        if (str56 != null)
                        {
                            if (!(str56 == "zj"))
                            {
                                if (str56 == "fgs")
                                {
                                    goto Label_22B7;
                                }
                                if (str56 == "gd")
                                {
                                    goto Label_22DB;
                                }
                                if (str56 == "zd")
                                {
                                    goto Label_22FF;
                                }
                                if (str56 == "dl")
                                {
                                    goto Label_2323;
                                }
                                if (str56 == "hy")
                                {
                                    goto Label_2347;
                                }
                            }
                            else
                            {
                                str17 = row[str15 + "_drawback"].ToString().Trim();
                            }
                        }
                        continue;
                    Label_22B7:
                        str18 = row[str15 + "_drawback"].ToString().Trim();
                        continue;
                    Label_22DB:
                        str19 = row[str15 + "_drawback"].ToString().Trim();
                        continue;
                    Label_22FF:
                        str20 = row[str15 + "_drawback"].ToString().Trim();
                        continue;
                    Label_2323:
                        str21 = row[str15 + "_drawback"].ToString().Trim();
                        continue;
                    Label_2347:
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
                        num42 = 0;
                        base.Add_Bet_Lock(num42.ToString(), rate.get_fgsname(), item);
                    }
                    num44 = double.Parse(table9.Rows[0]["current_odds"].ToString()) + Convert.ToDouble(table9.Rows[0][str15 + "_diff"].ToString().Trim());
                    str51 = num44.ToString();
                    str52 = str51.ToString();
                    base.GetOdds_KC(0, item, ref str52);
                    playDrawbackValue = base.GetPlayDrawbackValue(str18, str49);
                    if ((playDrawbackValue != 0.0) && (double.Parse(str51) > playDrawbackValue))
                    {
                        base.DeleteCreditLock(str);
                        if (FileCacheHelper.get_AddBetLockUType_KC().Equals("zj"))
                        {
                            num42 = 0;
                            base.Un_Bet_Lock(num42.ToString(), getUserModelInfo.get_zjname(), item);
                        }
                        else
                        {
                            num42 = 0;
                            base.Un_Bet_Lock(num42.ToString(), rate.get_fgsname(), item);
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
                    _kc.set_odds_id(new int?(int.Parse(item)));
                    _kc.set_category(table9.Rows[0]["category"].ToString());
                    _kc.set_play_id(new int?(int.Parse(str46)));
                    _kc.set_play_name(str47);
                    _kc.set_bet_val(str35);
                    _kc.set_bet_wt(str33);
                    _kc.set_bet_group(str32);
                    _kc.set_odds(str52);
                    _kc.set_amount(new decimal?(decimal.Parse(num24.ToString())));
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
                    _kc.set_odds_zj(str51);
                    _kc.set_ordervalidcode(Utils.GetOrderValidCode(_kc.get_u_name(), _kc.get_order_num(), _kc.get_bet_val(), _kc.get_odds(), _kc.get_kind(), Convert.ToInt32(_kc.get_phase_id()), Convert.ToInt32(_kc.get_odds_id()), Convert.ToDouble(_kc.get_amount())));
                    num27 = 0;
                    if (!CallBLL.cz_bet_kc_bll.AddBet(_kc, decimal.Parse(num24.ToString()), str, ref num27) || (num27 <= 0))
                    {
                        base.DeleteCreditLock(str);
                        if (FileCacheHelper.get_AddBetLockUType_KC().Equals("zj"))
                        {
                            num42 = 0;
                            base.Un_Bet_Lock(num42.ToString(), getUserModelInfo.get_zjname(), item);
                        }
                        else
                        {
                            num42 = 0;
                            base.Un_Bet_Lock(num42.ToString(), rate.get_fgsname(), item);
                        }
                        result.set_tipinfo(string.Format("系統錯誤！", new object[0]));
                        result.set_success(400);
                        dictionary.Add("JeuValidate", base.get_JeuValidate());
                        result.set_data(dictionary);
                        strResult = JsonHandle.ObjectToJson(result);
                        return;
                    }
                    double num28 = double.Parse(str8);
                    double num29 = 0.0;
                    double num30 = 0.0;
                    string[] strArray7 = str52.Split(new char[] { ',' });
                    num29 = double.Parse(strArray7[0].ToString());
                    if (strArray7.Length > 1)
                    {
                        num30 = double.Parse(strArray7[1].ToString());
                    }
                    double num31 = 0.0;
                    double num32 = 0.0;
                    string[] strArray8 = str51.Split(new char[] { ',' });
                    num31 = double.Parse(strArray8[0].ToString());
                    if (strArray8.Length > 1)
                    {
                        num32 = double.Parse(strArray8[1].ToString());
                    }
                    string[] strArray9 = str32.Split(new char[] { '~' });
                    string str53 = "";
                    cz_splitgroup_kl10 _kl = new cz_splitgroup_kl10();
                    _kl.set_odds_id(new int?(int.Parse(item)));
                    _kl.set_item_money(new decimal?((decimal) num28));
                    _kl.set_odds1(new decimal?(Convert.ToDecimal(num29.ToString())));
                    _kl.set_odds2(new decimal?(Convert.ToDecimal(num30.ToString())));
                    _kl.set_bet_id(new int?(num27));
                    _kl.set_odds1_zj(new decimal?(Convert.ToDecimal(num31.ToString())));
                    _kl.set_odds2_zj(new decimal?(Convert.ToDecimal(num32.ToString())));
                    switch (item)
                    {
                        case "330":
                        case "1200":
                            foreach (string str54 in strArray9)
                            {
                                str53 = str54;
                                _kl.set_item(str53);
                                if (CallBLL.cz_splitgroup_kl10_bll.AddSplitGroup(_kl) == 0)
                                {
                                    base.DeleteCreditLock(str);
                                    if (FileCacheHelper.get_AddBetLockUType_KC().Equals("zj"))
                                    {
                                        num42 = 0;
                                        base.Un_Bet_Lock(num42.ToString(), getUserModelInfo.get_zjname(), item);
                                    }
                                    else
                                    {
                                        num42 = 0;
                                        base.Un_Bet_Lock(num42.ToString(), rate.get_fgsname(), item);
                                    }
                                    result.set_tipinfo(string.Format("系統錯誤！", new object[0]));
                                    result.set_success(400);
                                    dictionary.Add("JeuValidate", base.get_JeuValidate());
                                    result.set_data(dictionary);
                                    strResult = JsonHandle.ObjectToJson(result);
                                    return;
                                }
                            }
                            goto Label_2E1E;
                    }
                    foreach (string str54 in strArray9)
                    {
                        str53 = px(str54);
                        _kl.set_item(str53);
                        if (CallBLL.cz_splitgroup_kl10_bll.AddSplitGroup(_kl) == 0)
                        {
                            base.DeleteCreditLock(str);
                            if (FileCacheHelper.get_AddBetLockUType_KC().Equals("zj"))
                            {
                                num42 = 0;
                                base.Un_Bet_Lock(num42.ToString(), getUserModelInfo.get_zjname(), item);
                            }
                            else
                            {
                                num42 = 0;
                                base.Un_Bet_Lock(num42.ToString(), rate.get_fgsname(), item);
                            }
                            result.set_tipinfo(string.Format("系統錯誤！", new object[0]));
                            result.set_success(400);
                            dictionary.Add("JeuValidate", base.get_JeuValidate());
                            result.set_data(dictionary);
                            strResult = JsonHandle.ObjectToJson(result);
                            return;
                        }
                    }
                    goto Label_2E1E;
                }
                base.DeleteCreditLock(str);
                result.set_tipinfo(string.Format("[{0}]你下注金额不能構成有效注單，請确认!", str26));
                result.set_success(500);
                dictionary.Add("JeuValidate", base.get_JeuValidate());
                result.set_data(dictionary);
                strResult = JsonHandle.ObjectToJson(result);
            }
            return;
        Label_2E1E:
            num34 = (double.Parse(num24.ToString()) * num6) / 100.0;
            CallBLL.cz_odds_kl10_bll.UpdateGrandTotal(Convert.ToDecimal(num34), int.Parse(item.Trim()));
            num35 = double.Parse(table9.Rows[0]["grand_total"].ToString()) + num34;
            num36 = double.Parse(table9.Rows[0]["downbase"].ToString());
            num37 = Math.Floor((double) (num35 / num36));
            if ((num37 >= 1.0) && (num36 >= 1.0))
            {
                num38 = double.Parse(table9.Rows[0]["down_odds_rate"].ToString()) * num37;
                num40 = CallBLL.cz_odds_kl10_bll.UpdateGrandTotalCurrentOdds(num38.ToString(), (num35 - (num36 * num37)).ToString(), item);
                _log = new cz_system_log();
                _log.set_user_name("系統");
                _log.set_children_name("");
                _log.set_category(table9.Rows[0]["category"].ToString());
                _log.set_play_name(str47);
                _log.set_put_amount(str48);
                num42 = 0;
                _log.set_l_name(base.GetGameNameByID(num42.ToString()));
                _log.set_l_phase(str12);
                _log.set_action("降賠率");
                _log.set_odds_id(int.Parse(item));
                str55 = table9.Rows[0]["current_odds"].ToString();
                _log.set_old_val(str55);
                num44 = double.Parse(str55) - num38;
                _log.set_new_val(num44.ToString());
                _log.set_ip(LSRequest.GetIP());
                _log.set_add_time(DateTime.Now);
                _log.set_note("系統自動降賠");
                _log.set_type_id(Convert.ToInt32((LSEnums.LogTypeID) 0));
                _log.set_lottery_type(0);
                CallBLL.cz_system_log_bll.Add(_log);
                _odds = new cz_jp_odds();
                _odds.set_add_time(DateTime.Now);
                _odds.set_odds_id(int.Parse(item));
                _odds.set_phase_id(int.Parse(str13));
                _odds.set_play_name(str47);
                _odds.set_put_amount(str48);
                _odds.set_odds(num38.ToString());
                _odds.set_lottery_type(0);
                _odds.set_phase(str12);
                _odds.set_old_odds(str55);
                _odds.set_new_odds((double.Parse(str55) - num38).ToString());
                CallBLL.cz_jp_odds_bll.Add(_odds);
            }
            fgsWTTable = null;
            if (getUserModelInfo.get_kc_op_odds().Equals(1))
            {
                fgsWTTable = base.GetFgsWTTable(0);
            }
            CallBLL.cz_autosale_kl10_bll.DLAutoSaleLM(_kc.get_order_num(), str10, int.Parse(table9.Rows[0]["play_id"].ToString()), int.Parse(item.Trim()), double.Parse(_kc.get_odds()), num, _kc.get_lottery_name(), int.Parse(str13), str12, str17, str18, str19, str20, str21, str22, _kc.get_ip(), getUserModelInfo, rate, 1, str15, fgsWTTable);
            if (getUserModelInfo.get_kc_op_odds().Equals(1))
            {
                fgsWTTable = base.GetFgsWTTable(0);
            }
            CallBLL.cz_autosale_kl10_bll.ZDAutoSaleLM(_kc.get_order_num(), str10, int.Parse(table9.Rows[0]["play_id"].ToString()), int.Parse(item.Trim()), double.Parse(_kc.get_odds()), num, _kc.get_lottery_name(), int.Parse(str13), str12, str17, str18, str19, str20, str21, str22, _kc.get_ip(), getUserModelInfo, rate, 1, str15, fgsWTTable);
            if (getUserModelInfo.get_kc_op_odds().Equals(1))
            {
                fgsWTTable = base.GetFgsWTTable(0);
            }
            CallBLL.cz_autosale_kl10_bll.GDAutoSaleLM(_kc.get_order_num(), str10, int.Parse(table9.Rows[0]["play_id"].ToString()), int.Parse(item.Trim()), double.Parse(_kc.get_odds()), num, _kc.get_lottery_name(), int.Parse(str13), str12, str17, str18, str19, str20, str21, str22, _kc.get_ip(), getUserModelInfo, rate, 1, str15, fgsWTTable);
            if (getUserModelInfo.get_kc_op_odds().Equals(1))
            {
                fgsWTTable = base.GetFgsWTTable(0);
            }
            CallBLL.cz_autosale_kl10_bll.FGSAutoSaleLM(_kc.get_order_num(), str10, int.Parse(table9.Rows[0]["play_id"].ToString()), int.Parse(item.Trim()), double.Parse(_kc.get_odds()), num, _kc.get_lottery_name(), int.Parse(str13), str12, str17, str18, str19, str20, str21, str22, _kc.get_ip(), getUserModelInfo, rate, 1, str15, fgsWTTable);
            if (FileCacheHelper.get_AddBetLockUType_KC().Equals("zj"))
            {
                num42 = 0;
                base.Un_Bet_Lock(num42.ToString(), getUserModelInfo.get_zjname(), item);
            }
            else
            {
                base.Un_Bet_Lock(0.ToString(), rate.get_fgsname(), item);
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
                base.SetUserDrawback_kl10(dataTable, str46);
            }
            else
            {
                base.SetUserDrawback_kl10(dataTable);
            }
            getUserModelInfo.set_begin_kc("yes");
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
                DataSet set2 = CallBLL.cz_play_kl10_bll.GetPlay(str, str5, str4, str2);
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
                                if (CacheHelper.GetCache("kc_drawback_FileCacheKey_kl10" + str15 + context.Session["user_name"].ToString()) != null)
                                {
                                    DataTable cache = CacheHelper.GetCache("kc_drawback_FileCacheKey_kl10" + str15 + context.Session["user_name"].ToString()) as DataTable;
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
                                    drawbackByPlayIds = CallBLL.cz_drawback_kl10_bll.GetDrawbackByPlayIds(str15, getUserModelInfo.get_u_name());
                                }
                                else
                                {
                                    table7 = drawbackByPlayIds.Clone();
                                    table7 = CallBLL.cz_drawback_kl10_bll.GetDrawbackByPlayIds(str15, getUserModelInfo.get_u_name());
                                    if (table7 != null)
                                    {
                                        drawbackByPlayIds.Merge(table7);
                                    }
                                }
                            }
                        }
                        else if (CacheHelper.GetCache("kc_drawback_FileCacheKey_kl10" + this.Session["user_name"].ToString()) != null)
                        {
                            drawbackByPlayIds = CacheHelper.GetCache("kc_drawback_FileCacheKey_kl10" + this.Session["user_name"]) as DataTable;
                        }
                        else
                        {
                            drawbackByPlayIds = CallBLL.cz_drawback_kl10_bll.GetDrawbackByPlayIds(str, getUserModelInfo.get_u_name());
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
                            if ((((str2 == "kl10_lm") && (table4 != null)) && (table4.Rows.Count > 0)) && (!row2["play_id"].ToString().Trim().Equals("73") && !row2["play_id"].ToString().Trim().Equals("76")))
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
                                base.GetOdds_KC(0, row2["odds_id"].ToString(), ref pl);
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
                DataTable kCOpenBall = base.GetKCOpenBall(0);
                if (kCOpenBall != null)
                {
                    string str = kCOpenBall.Rows[0]["previousphase"].ToString().Trim();
                    string str2 = kCOpenBall.Rows[0]["sold"].ToString().Trim();
                    string str3 = kCOpenBall.Rows[0]["surplus"].ToString().Trim();
                    string str4 = kCOpenBall.Rows[0]["open_date"].ToString().Trim();
                    string str5 = kCOpenBall.Rows[0]["play_open_date"].ToString().Trim();
                    string dateDiff = LSKeys.get_KL10_Phase_Interval().ToString() + "分鐘";
                    if (!(string.IsNullOrEmpty(str4) || string.IsNullOrEmpty(str5)))
                    {
                        dateDiff = Utils.GetDateDiff(str4, str5);
                    }
                    Dictionary<string, string> dictionary2 = new Dictionary<string, string>();
                    DataRow[] rowArray = base.GetLotteryList().Select(string.Format(" id={0} ", 0));
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
                int num2;
                ArrayList list6;
                ReturnResult result = new ReturnResult();
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                dictionary.Add("type", "get_ranklist");
                dictionary.Add("playpage", str);
                string str2 = "_kl10_jqkj";
                string str3 = "_kl10_lmcl";
                string str4 = "_kl10_cql";
                string str5 = "_kl10_lryl";
                List<object> cache = new List<object>();
                if (CacheHelper.GetCache("balance_kc_FileCacheKey" + str2) != null)
                {
                    cache = CacheHelper.GetCache("balance_kc_FileCacheKey" + str2) as List<object>;
                }
                else
                {
                    List<string> collection = new List<string>();
                    DataSet topPhase = CallBLL.cz_phase_kl10_bll.GetTopPhase(5);
                    if (topPhase != null)
                    {
                        DataTable table = topPhase.Tables[0];
                        int num = 8;
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
                            num2 = 1;
                            while (num2 <= num)
                            {
                                string str13 = "n" + num2;
                                collection.Add(row[str13].ToString());
                                num2++;
                            }
                            str8 = KL10Phase.get_zh(collection).ToString();
                            str9 = KL10Phase.get_cl_zhdx(str8);
                            str9 = base.SetWordColor(str9.Replace("總和", ""));
                            str10 = KL10Phase.get_cl_zhds(str8);
                            str10 = base.SetWordColor(str10.Replace("總和", ""));
                            str11 = KL10Phase.get_cl_zhwsdx(str8);
                            str11 = base.SetWordColor(str11.Replace("總和", ""));
                            str12 = KL10Phase.get_cl_lh(collection[0].ToString(), collection[num - 1].ToString());
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
                    DataTable table2 = CallBLL.cz_phase_kl10_bll.GetChangLong().Tables[0];
                    if (table2.Rows.Count > 0)
                    {
                        dictionary2 = new Dictionary<string, object>();
                        foreach (DataRow row in table2.Rows)
                        {
                            string str14 = row["c_name"].ToString();
                            string str15 = row["c_qs"].ToString();
                            dictionary2.Add("cl_name", str14);
                            dictionary2.Add("cl_num", str15);
                            list4.Add(new Dictionary<string, object>(dictionary2));
                            dictionary2.Clear();
                        }
                    }
                    CacheHelper.SetCache("balance_kc_FileCacheKey" + str3, list4);
                    CacheHelper.SetPublicFileCache("balance_kc_FileCacheKey" + str3, list4, PageBase.GetPublicForderPath(base.get_KC_BalanceFileName()));
                }
                dictionary.Add("lmcl", list4);
                string str16 = str;
                str16 = str16.Replace("kl10_d", "");
                Dictionary<string, object> dictionary3 = new Dictionary<string, object>();
                if (CacheHelper.GetCache("balance_kc_FileCacheKey" + str4) != null)
                {
                    dictionary3 = CacheHelper.GetCache("balance_kc_FileCacheKey" + str4) as Dictionary<string, object>;
                }
                else
                {
                    Dictionary<string, string> paiHang = CallBLL.cz_phase_kl10_bll.GetPaiHang(str16, 0x19);
                    if ((paiHang != null) && (paiHang.Count > 0))
                    {
                        ArrayList list5 = new ArrayList();
                        list6 = new ArrayList();
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
                if (Utils.IsNumeric(str16))
                {
                    Dictionary<string, object> dictionary5 = new Dictionary<string, object>();
                    if (CacheHelper.GetCache("balance_kc_FileCacheKey" + str5) != null)
                    {
                        dictionary5 = CacheHelper.GetCache("balance_kc_FileCacheKey" + str5) as Dictionary<string, object>;
                    }
                    else
                    {
                        DataSet lRYL = CallBLL.cz_phase_kl10_bll.GetLRYL(str16);
                        DataTable table3 = null;
                        if (lRYL != null)
                        {
                            table3 = lRYL.Tables[0];
                            ArrayList list7 = new ArrayList();
                            if ((table3 != null) && (table3.Rows.Count > 0))
                            {
                                list6 = new ArrayList();
                                for (num2 = 0; num2 < table3.Columns.Count; num2++)
                                {
                                    list6.Add(table3.Columns[num2].ColumnName);
                                }
                                dictionary5.Add("num", list6);
                                dictionary5.Add("lr", table3.Rows[0].ItemArray);
                                dictionary5.Add("yl", table3.Rows[1].ItemArray);
                            }
                        }
                        CacheHelper.SetCache("balance_kc_FileCacheKey" + str5, dictionary5);
                        CacheHelper.SetPublicFileCache("balance_kc_FileCacheKey" + str5, dictionary5, PageBase.GetPublicForderPath(base.get_KC_BalanceFileName()));
                    }
                    dictionary.Add("lryl", dictionary5);
                }
                result.set_data(dictionary);
                strResult = JsonHandle.ObjectToJson(result);
            }
        }

        public override void ProcessRequest(HttpContext context)
        {
            base.checkLogin();
            base.IsLotteryExist(0.ToString());
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
                        goto Label_00E1;
                    }
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
            string[] array = p_str.Split(new char[] { ',' });
            Array.Sort<string>(array);
            return string.Join(",", array);
        }
    }
}

