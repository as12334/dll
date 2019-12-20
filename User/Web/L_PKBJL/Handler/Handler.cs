namespace User.Web.L_PKBJL.Handler
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
            int num29;
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
                        result.set_tipinfo(PageBase.GetMessageByCache("u100010", "MessageHint"));
                    }
                    else
                    {
                        result.set_tipinfo(PageBase.GetMessageByCache("u100011", "MessageHint"));
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
            int num = 8;
            string s = LSRequest.qq("phaseid");
            string str5 = LSRequest.qq("playpage");
            string str6 = LSRequest.qq("oddsid");
            string str7 = LSRequest.qq("uPI_P");
            string str8 = LSRequest.qq("uPI_M");
            string[] strArray = LSRequest.qq("i_index").Split(new char[] { ',' });
            string str9 = LSRequest.qq("JeuValidate");
            string str10 = Utils.GetPKBJL_NumTable(str5);
            string str11 = LSRequest.qq("playtype");
            if (!string.IsNullOrEmpty(str11))
            {
                if ((str11 != "0") && (str11 != "1"))
                {
                    base.DeleteCreditLock(str);
                    base.Response.End();
                }
            }
            else
            {
                str11 = "1";
            }
            if (str10 == null)
            {
                base.DeleteCreditLock(str);
                base.Response.End();
            }
            if ((int.Parse(str10) < 1) || (int.Parse(str10) > 5))
            {
                base.DeleteCreditLock(str);
                base.Response.End();
            }
            string[] source = str6.Split(new char[] { ',' });
            if (source.Distinct<string>().ToList<string>().Count != source.Length)
            {
                base.DeleteCreditLock(str);
                context.Response.End();
            }
            if (source.Length > FileCacheHelper.get_GetPKBJLMaxGroup())
            {
                base.DeleteCreditLock(str);
                result.set_success(400);
                result.set_tipinfo(string.Format("您選擇的號碼超過了最大{0}組，請重新選擇號碼!", FileCacheHelper.get_GetPKBJLMaxGroup()));
                dictionary.Add("JeuValidate", base.get_JeuValidate());
                result.set_data(dictionary);
                strResult = JsonHandle.ObjectToJson(result);
                return;
            }
            if (((context.Session["JeuValidate"] == null) || (context.Session["JeuValidate"].ToString().Trim() != str9)) || (context.Session["JeuValidate"].ToString().Trim() == ""))
            {
                base.DeleteCreditLock(str);
                result.set_tipinfo(PageBase.GetMessageByCache("u100012", "MessageHint"));
                result.set_success(400);
                dictionary.Add("JeuValidate", base.get_JeuValidate());
                result.set_data(dictionary);
                strResult = JsonHandle.ObjectToJson(result);
                return;
            }
            context.Session["JeuValidate"] = "";
            int num3 = 1;
            if (!getUserModelInfo.get_su_type().ToString().Trim().Equals("dl"))
            {
                num3 = -1;
            }
            int index = 0;
        Label_05F1:;
            if (index < str8.Split(new char[] { ',' }).Length)
            {
                if (!base.IsNumber(str8.Split(new char[] { ',' })[index]))
                {
                    base.DeleteCreditLock(str);
                    result.set_tipinfo(PageBase.GetMessageByCache("u100013", "MessageHint"));
                    result.set_success(500);
                    dictionary.Add("index", strArray[index]);
                    dictionary.Add("JeuValidate", base.get_JeuValidate());
                    result.set_data(dictionary);
                    strResult = JsonHandle.ObjectToJson(result);
                    return;
                }
                index++;
                goto Label_05F1;
            }
            if (getUserModelInfo.get_begin_kc().Trim() != "yes")
            {
                getUserModelInfo = base.GetRateByUserObject(2, rate);
            }
            string str13 = "";
            string str14 = "";
            if (!Utils.IsInteger(s))
            {
                base.Response.End();
            }
            DataTable table2 = CallBLL.cz_phase_pkbjl_bll.GetIsClosedByTime(int.Parse(s), str10).Tables[0];
            if ((table2 != null) && (table2.Rows.Count > 0))
            {
                if (table2.Rows[0]["is_closed"].ToString().Trim() == "1")
                {
                    base.DeleteCreditLock(str);
                    result.set_tipinfo(string.Format(PageBase.GetMessageByCache("u100015", "MessageHint"), table2.Rows[0]["phase"].ToString()));
                    result.set_success(400);
                    dictionary.Add("JeuValidate", base.get_JeuValidate());
                    result.set_data(dictionary);
                    strResult = JsonHandle.ObjectToJson(result);
                    return;
                }
                str13 = table2.Rows[0]["phase"].ToString();
                str14 = table2.Rows[0]["p_id"].ToString();
                time = Convert.ToDateTime(table2.Rows[0]["stop_date"].ToString());
                num29 = 8;
                string str15 = base.BetReceiveEnd(num29.ToString());
                if (!string.IsNullOrEmpty(str15))
                {
                    time = Convert.ToDateTime(table2.Rows[0]["play_open_date"].ToString()).AddSeconds((double) -int.Parse(str15));
                }
            }
            else
            {
                base.DeleteCreditLock(str);
                result.set_tipinfo(PageBase.GetMessageByCache("u100014", "MessageHint"));
                result.set_success(400);
                dictionary.Add("JeuValidate", base.get_JeuValidate());
                result.set_data(dictionary);
                strResult = JsonHandle.ObjectToJson(result);
                return;
            }
            string str16 = "";
            string str17 = "";
            double num5 = 0.0;
            DataTable table3 = CallBLL.cz_users_bll.GetUserRate(str, 2).Tables[0];
            str16 = table3.Rows[0]["kc_kind"].ToString().Trim().ToUpper();
            str17 = table3.Rows[0]["su_type"].ToString().Trim();
            num5 = double.Parse(table3.Rows[0]["kc_usable_credit"].ToString().Trim());
            getUserModelInfo.set_kc_kind(str16.ToUpper());
            double num6 = 0.0;
            double num7 = 0.0;
            double num8 = 0.0;
            double num9 = 0.0;
            double num10 = 0.0;
            double num11 = 0.0;
            string str18 = "";
            string str19 = "";
            string str20 = "";
            string str21 = "";
            string str22 = "";
            string[] strArray3 = str6.Split(new char[] { ',' });
            string[] strArray4 = str7.Split(new char[] { ',' });
            string[] strArray5 = str8.Split(new char[] { ',' });
            DataTable plDT = CallBLL.cz_odds_pkbjl_bll.GetPlayOddsByID(str6).Tables[0];
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
                DataTable table5 = CallBLL.cz_bet_kc_bll.GetSinglePhaseByBet(str13, str, num.ToString(), str6, str10).Tables[0];
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
                    double num12 = 0.0;
                    string str23 = "";
                    List<string> list = new List<string>();
                    List<string> list2 = new List<string>();
                    int num13 = 0;
                    foreach (string str24 in strArray3)
                    {
                        num6 = 0.0;
                        foreach (DataRow row in plDT.Rows)
                        {
                            if (row["odds_id"].ToString().Trim() == str24.Trim())
                            {
                                str23 = row["is_open"].ToString().Trim();
                                str19 = row["current_odds"].ToString().Trim();
                                str20 = row[str16 + "_diff"].ToString().Trim();
                                str18 = row["play_id"].ToString().Trim();
                                str21 = row["play_name"].ToString().Trim();
                                str22 = row["put_amount"].ToString().Trim();
                                num11 = double.Parse(row["allow_min_amount"].ToString().Trim());
                                num8 = double.Parse(row["allow_max_amount"].ToString().Trim());
                                num10 = double.Parse(row["allow_max_put_amount"].ToString().Trim());
                                break;
                            }
                        }
                        if (table5.Rows.Count > 0)
                        {
                            foreach (DataRow row2 in table5.Rows)
                            {
                                if (row2["odds_id"].ToString().Trim() == str24.Trim())
                                {
                                    num6 = double.Parse(row2["sumbet"].ToString().Trim());
                                    break;
                                }
                            }
                        }
                        DataTable table6 = null;
                        if (FileCacheHelper.get_GetKCPutMoneyCache() == "1")
                        {
                            if (CacheHelper.GetCache("kc_drawback_FileCacheKey_pkbjl" + str18 + this.Session["user_name"].ToString()) == null)
                            {
                                table6 = CallBLL.cz_drawback_pkbjl_bll.GetDrawback(str18, str).Tables[0];
                            }
                            else
                            {
                                table6 = base.GetUserDrawback_pkbjl(rate, getUserModelInfo.get_kc_kind(), str18);
                            }
                        }
                        else if (CacheHelper.GetCache("kc_drawback_FileCacheKey_pkbjl" + this.Session["user_name"].ToString()) == null)
                        {
                            table6 = CallBLL.cz_drawback_pkbjl_bll.GetDrawback(str18, str).Tables[0];
                        }
                        else
                        {
                            table6 = base.GetUserDrawback_pkbjl(rate, getUserModelInfo.get_kc_kind());
                        }
                        DataRow[] rowArray = table6.Select(string.Format(" play_id={0} and u_name='{1}' ", str18, str));
                        num7 = double.Parse(rowArray[0]["single_max_amount"].ToString().Trim());
                        double num14 = double.Parse(rowArray[0]["single_min_amount"].ToString().Trim());
                        num9 = double.Parse(rowArray[0]["single_phase_amount"].ToString().Trim());
                        if (num7 > num8)
                        {
                            num7 = num8;
                        }
                        if (num9 > num10)
                        {
                            num9 = num10;
                        }
                        if (num14 > num11)
                        {
                            num11 = num14;
                        }
                        if (str23 != "1")
                        {
                            base.DeleteCreditLock(str);
                            result.set_tipinfo(string.Format("{0}<{1}>已經停止投注！", str21, str22));
                            result.set_success(400);
                            dictionary.Add("JeuValidate", base.get_JeuValidate());
                            result.set_data(dictionary);
                            strResult = JsonHandle.ObjectToJson(result);
                            return;
                        }
                        num12 = double.Parse(str19) + double.Parse(str20);
                        if (str11.Equals("0"))
                        {
                            num12 = Utils.GetPKBJLPlaytypeOdds(str24, num12, base.GetPlayTypeWTValue_PKBJL(str24));
                        }
                        string pl = num12.ToString();
                        base.GetOdds_KC(8, str24, ref pl);
                        num12 = Convert.ToDouble(pl);
                        if (!(double.Parse(strArray4[num13]) == double.Parse(num12.ToString())))
                        {
                            list.Add(num13.ToString());
                            list2.Add(num12.ToString());
                        }
                        if (double.Parse(strArray5[num13]) > num7)
                        {
                            base.DeleteCreditLock(str);
                            result.set_tipinfo(string.Format("下注金額超出單注最大金額！", new object[0]));
                            result.set_success(500);
                            dictionary.Add("JeuValidate", base.get_JeuValidate());
                            result.set_data(dictionary);
                            strResult = JsonHandle.ObjectToJson(result);
                            return;
                        }
                        if (double.Parse(strArray5[num13]) > num5)
                        {
                            base.DeleteCreditLock(str);
                            result.set_tipinfo(string.Format("下注金額超出可用金額！", new object[0]));
                            result.set_success(500);
                            dictionary.Add("JeuValidate", base.get_JeuValidate());
                            result.set_data(dictionary);
                            strResult = JsonHandle.ObjectToJson(result);
                            return;
                        }
                        if (double.Parse(strArray5[num13]) < num11)
                        {
                            base.DeleteCreditLock(str);
                            result.set_tipinfo(string.Format("下注金額低過最低金額！", new object[0]));
                            result.set_success(500);
                            dictionary.Add("JeuValidate", base.get_JeuValidate());
                            result.set_data(dictionary);
                            strResult = JsonHandle.ObjectToJson(result);
                            return;
                        }
                        if ((double.Parse(strArray5[num13]) + num6) > num9)
                        {
                            base.DeleteCreditLock(str);
                            result.set_tipinfo(string.Format("{0}<{1}>下注單期金額超出單期最大金額！", str21, str22));
                            result.set_success(500);
                            dictionary.Add("JeuValidate", base.get_JeuValidate());
                            result.set_data(dictionary);
                            strResult = JsonHandle.ObjectToJson(result);
                            return;
                        }
                        num13++;
                    }
                    if (list.Count > 0)
                    {
                        base.DeleteCreditLock(str);
                        result.set_tipinfo(string.Format("賠率有變動,請確認後再提交!", new object[0]));
                        result.set_success(600);
                        dictionary.Add("JeuValidate", base.get_JeuValidate());
                        dictionary.Add("index", list);
                        dictionary.Add("newpl", list2);
                        result.set_data(dictionary);
                        strResult = JsonHandle.ObjectToJson(result);
                    }
                    else
                    {
                        double num15 = 0.0;
                        int num16 = 0;
                        foreach (string str26 in strArray3)
                        {
                            num15 += double.Parse(strArray5[num16].ToString().Trim());
                            num16++;
                        }
                        if (num5 < num15)
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
                            DataTable dataTable = null;
                            bool flag2 = false;
                            List<string> successBetList = new List<string>();
                            num13 = 0;
                            DateTime? nullable = null;
                            foreach (string str24 in strArray3)
                            {
                                double num19;
                                if (FileCacheHelper.get_AddBetLockUType_KC().Equals("zj"))
                                {
                                    num29 = 8;
                                    base.Add_Bet_Lock(num29.ToString(), getUserModelInfo.get_zjname(), str24);
                                }
                                DataTable table8 = CallBLL.cz_odds_pkbjl_bll.GetOddsByID(str24).Tables[0];
                                string str27 = table8.Rows[0]["play_id"].ToString();
                                string str28 = table8.Rows[0]["play_name"].ToString();
                                string str29 = table8.Rows[0]["put_amount"].ToString();
                                string ratio = table8.Rows[0]["ratio"].ToString();
                                if (FileCacheHelper.get_GetKCPutMoneyCache() == "1")
                                {
                                    dataTable = base.GetUserDrawback_pkbjl(rate, getUserModelInfo.get_kc_kind(), str27.ToString());
                                }
                                else
                                {
                                    dataTable = base.GetUserDrawback_pkbjl(rate, getUserModelInfo.get_kc_kind());
                                }
                                if (dataTable == null)
                                {
                                    base.DeleteCreditLock(str);
                                    if (FileCacheHelper.get_AddBetLockUType_KC().Equals("zj"))
                                    {
                                        num29 = 8;
                                        base.Un_Bet_Lock(num29.ToString(), getUserModelInfo.get_zjname(), str24);
                                    }
                                    else
                                    {
                                        num29 = 8;
                                        base.Un_Bet_Lock(num29.ToString(), rate.get_fgsname(), str24);
                                    }
                                    result.set_tipinfo(string.Format("系統錯誤！", new object[0]));
                                    result.set_success(400);
                                    dictionary.Add("JeuValidate", base.get_JeuValidate());
                                    result.set_data(dictionary);
                                    strResult = JsonHandle.ObjectToJson(result);
                                    return;
                                }
                                DataRow[] rowArray2 = dataTable.Select(string.Format(" play_id={0} ", str27));
                                string str31 = "0";
                                string str32 = "0";
                                string str33 = "0";
                                string str34 = "0";
                                string str35 = "0";
                                string str36 = "0";
                                double num17 = 0.0;
                                double num18 = 0.0;
                                if (rate.get_zcyg().Equals("1"))
                                {
                                    num19 = (((100.0 - double.Parse(rate.get_fgszc())) - double.Parse(rate.get_gdzc())) - double.Parse(rate.get_zdzc())) - double.Parse(rate.get_dlzc());
                                    num17 = num19;
                                    num18 = double.Parse(rate.get_fgszc());
                                }
                                else
                                {
                                    num19 = (((100.0 - double.Parse(rate.get_zjzc())) - double.Parse(rate.get_gdzc())) - double.Parse(rate.get_zdzc())) - double.Parse(rate.get_dlzc());
                                    num18 = num19;
                                    num17 = double.Parse(rate.get_zjzc());
                                }
                                string str37 = "A";
                                if (getUserModelInfo.get_kc_kind().Trim().Equals("0") || string.IsNullOrEmpty(getUserModelInfo.get_kc_kind()))
                                {
                                    str37 = "A";
                                }
                                else
                                {
                                    str37 = getUserModelInfo.get_kc_kind().Trim();
                                }
                                str37 = str16;
                                foreach (DataRow row in rowArray2)
                                {
                                    string str41 = row["u_type"].ToString().Trim();
                                    if (str41 != null)
                                    {
                                        if (!(str41 == "zj"))
                                        {
                                            if (str41 == "fgs")
                                            {
                                                goto Label_16FD;
                                            }
                                            if (str41 == "gd")
                                            {
                                                goto Label_1721;
                                            }
                                            if (str41 == "zd")
                                            {
                                                goto Label_1745;
                                            }
                                            if (str41 == "dl")
                                            {
                                                goto Label_1769;
                                            }
                                            if (str41 == "hy")
                                            {
                                                goto Label_178D;
                                            }
                                        }
                                        else
                                        {
                                            str31 = row[str16 + "_drawback"].ToString().Trim();
                                        }
                                    }
                                    continue;
                                Label_16FD:
                                    str32 = row[str16 + "_drawback"].ToString().Trim();
                                    continue;
                                Label_1721:
                                    str33 = row[str16 + "_drawback"].ToString().Trim();
                                    continue;
                                Label_1745:
                                    str34 = row[str16 + "_drawback"].ToString().Trim();
                                    continue;
                                Label_1769:
                                    str35 = row[str16 + "_drawback"].ToString().Trim();
                                    continue;
                                Label_178D:
                                    if (getUserModelInfo.get_su_type() == "dl")
                                    {
                                        str36 = row[str16 + "_drawback"].ToString().Trim();
                                    }
                                    else if (getUserModelInfo.get_su_type() == "zd")
                                    {
                                        str35 = row[str16 + "_drawback"].ToString().Trim();
                                        str36 = row[str16 + "_drawback"].ToString().Trim();
                                    }
                                    else if (getUserModelInfo.get_su_type() == "gd")
                                    {
                                        str34 = row[str16 + "_drawback"].ToString().Trim();
                                        str36 = row[str16 + "_drawback"].ToString().Trim();
                                    }
                                    else if (getUserModelInfo.get_su_type() == "fgs")
                                    {
                                        str33 = row[str16 + "_drawback"].ToString().Trim();
                                        str36 = row[str16 + "_drawback"].ToString().Trim();
                                    }
                                }
                                if (DateTime.Now >= time.AddSeconds(0.0))
                                {
                                    flag2 = true;
                                    break;
                                }
                                if (!FileCacheHelper.get_AddBetLockUType_KC().Equals("zj"))
                                {
                                    num29 = 8;
                                    base.Add_Bet_Lock(num29.ToString(), rate.get_fgsname(), str24);
                                }
                                double num32 = double.Parse(table8.Rows[0]["current_odds"].ToString()) + Convert.ToDouble(table8.Rows[0][str16 + "_diff"].ToString().Trim());
                                string str38 = num32.ToString();
                                if (str11.Equals("0"))
                                {
                                    str38 = Utils.GetPKBJLPlaytypeOdds(str24, double.Parse(str38), base.GetPlayTypeWTValue_PKBJL(str24)).ToString();
                                }
                                string str39 = str38.ToString();
                                base.GetOdds_KC(8, str24, ref str39);
                                double playDrawbackValue = base.GetPlayDrawbackValue(str32, ratio);
                                if ((playDrawbackValue != 0.0) && (double.Parse(str38) > playDrawbackValue))
                                {
                                    base.DeleteCreditLock(str);
                                    if (FileCacheHelper.get_AddBetLockUType_KC().Equals("zj"))
                                    {
                                        num29 = 8;
                                        base.Un_Bet_Lock(num29.ToString(), getUserModelInfo.get_zjname(), str24);
                                    }
                                    else
                                    {
                                        num29 = 8;
                                        base.Un_Bet_Lock(num29.ToString(), rate.get_fgsname(), str24);
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
                                cz_bet_kc _kc = new cz_bet_kc();
                                _kc.set_order_num(Utils.GetOrderNumber());
                                _kc.set_checkcode(str9);
                                _kc.set_u_name(getUserModelInfo.get_u_name());
                                _kc.set_u_nicker(getUserModelInfo.get_u_nicker());
                                _kc.set_phase_id(new int?(int.Parse(str14)));
                                _kc.set_phase(str13);
                                _kc.set_bet_time(nullable);
                                _kc.set_odds_id(new int?(int.Parse(str24)));
                                _kc.set_category(table8.Rows[0]["category"].ToString());
                                _kc.set_play_id(new int?(int.Parse(table8.Rows[0]["play_id"].ToString())));
                                _kc.set_play_name(str28);
                                _kc.set_bet_val(str29);
                                _kc.set_odds(str39);
                                _kc.set_amount(new decimal?(decimal.Parse(strArray5[num13])));
                                _kc.set_profit(0);
                                _kc.set_hy_drawback(new decimal?(decimal.Parse(str36)));
                                _kc.set_dl_drawback(new decimal?(decimal.Parse(str35)));
                                _kc.set_zd_drawback(new decimal?(decimal.Parse(str34)));
                                _kc.set_gd_drawback(new decimal?(decimal.Parse(str33)));
                                _kc.set_fgs_drawback(new decimal?(decimal.Parse(str32)));
                                _kc.set_zj_drawback(new decimal?(decimal.Parse(str31)));
                                _kc.set_dl_rate((float) int.Parse(rate.get_dlzc()));
                                _kc.set_zd_rate((float) int.Parse(rate.get_zdzc()));
                                _kc.set_gd_rate((float) int.Parse(rate.get_gdzc()));
                                _kc.set_fgs_rate(float.Parse(num18.ToString()));
                                _kc.set_zj_rate(float.Parse(num17.ToString()));
                                _kc.set_is_payment(0);
                                _kc.set_dl_name(rate.get_dlname());
                                _kc.set_zd_name(rate.get_zdname());
                                _kc.set_gd_name(rate.get_gdname());
                                _kc.set_fgs_name(rate.get_fgsname());
                                _kc.set_m_type(new int?(num3));
                                _kc.set_kind(str16);
                                _kc.set_ip(LSRequest.GetIP());
                                _kc.set_lottery_type(new int?(num));
                                _kc.set_lottery_name(base.GetGameNameByID(_kc.get_lottery_type().ToString()));
                                _kc.set_ordervalidcode(Utils.GetOrderValidCode(_kc.get_u_name(), _kc.get_order_num(), _kc.get_bet_val(), _kc.get_odds(), _kc.get_kind(), Convert.ToInt32(_kc.get_phase_id()), Convert.ToInt32(_kc.get_odds_id()), Convert.ToDouble(_kc.get_amount())));
                                _kc.set_odds_zj(str38);
                                _kc.set_table_type(new int?(int.Parse(str10)));
                                _kc.set_play_type(new int?(int.Parse(str11)));
                                int num21 = 0;
                                if (!CallBLL.cz_bet_kc_bll.AddBet(_kc, decimal.Parse(strArray5[num13]), str, ref num21) || (num21 <= 0))
                                {
                                    base.DeleteCreditLock(str);
                                    if (FileCacheHelper.get_AddBetLockUType_KC().Equals("zj"))
                                    {
                                        num29 = 8;
                                        base.Un_Bet_Lock(num29.ToString(), getUserModelInfo.get_zjname(), str24);
                                    }
                                    else
                                    {
                                        num29 = 8;
                                        base.Un_Bet_Lock(num29.ToString(), rate.get_fgsname(), str24);
                                    }
                                    result.set_tipinfo(string.Format("系統錯誤！", new object[0]));
                                    result.set_success(400);
                                    dictionary.Add("JeuValidate", base.get_JeuValidate());
                                    result.set_data(dictionary);
                                    strResult = JsonHandle.ObjectToJson(result);
                                    return;
                                }
                                successBetList.Add(string.Concat(new object[] { _kc.get_odds_id(), ",", _kc.get_order_num(), ",", _kc.get_play_name(), ",", _kc.get_bet_val(), ",", _kc.get_odds(), ",", _kc.get_amount() }));
                                double num22 = (double.Parse(strArray5[num13]) * num17) / 100.0;
                                CallBLL.cz_odds_pkbjl_bll.UpdateGrandTotal(Convert.ToDecimal(num22), int.Parse(str24));
                                double num23 = double.Parse(table8.Rows[0]["grand_total"].ToString()) + num22;
                                double num24 = double.Parse(table8.Rows[0]["downbase"].ToString());
                                double num25 = Math.Floor((double) (num23 / num24));
                                if ((num25 >= 1.0) && (num24 >= 1.0))
                                {
                                    double num26 = double.Parse(table8.Rows[0]["down_odds_rate"].ToString()) * num25;
                                    int num28 = CallBLL.cz_odds_pkbjl_bll.UpdateGrandTotalCurrentOdds(num26.ToString(), (num23 - (num24 * num25)).ToString(), str24.ToString());
                                    cz_system_log _log = new cz_system_log();
                                    _log.set_user_name("系統");
                                    _log.set_children_name("");
                                    _log.set_category(table8.Rows[0]["category"].ToString());
                                    _log.set_play_name(str28);
                                    _log.set_put_amount(str29);
                                    num29 = 8;
                                    _log.set_l_name(base.GetGameNameByID(num29.ToString()));
                                    _log.set_l_phase(str13);
                                    _log.set_action("降賠率");
                                    _log.set_odds_id(int.Parse(str24));
                                    string str40 = table8.Rows[0]["current_odds"].ToString();
                                    if (str11.Equals("0"))
                                    {
                                        str40 = Utils.GetPKBJLPlaytypeOdds(str24, double.Parse(str40), base.GetPlayTypeWTValue_PKBJL(str24)).ToString();
                                    }
                                    _log.set_old_val(str40);
                                    num32 = double.Parse(str40) - num26;
                                    _log.set_new_val(num32.ToString());
                                    _log.set_ip(LSRequest.GetIP());
                                    _log.set_add_time(DateTime.Now);
                                    _log.set_note("系統自動降賠");
                                    _log.set_type_id(Convert.ToInt32((LSEnums.LogTypeID) 0));
                                    _log.set_lottery_type(8);
                                    CallBLL.cz_system_log_bll.Add(_log);
                                    cz_jp_odds _odds = new cz_jp_odds();
                                    _odds.set_add_time(DateTime.Now);
                                    _odds.set_odds_id(int.Parse(str24));
                                    _odds.set_phase_id(int.Parse(str14));
                                    _odds.set_play_name(str28);
                                    _odds.set_put_amount(str29);
                                    _odds.set_odds(num26.ToString());
                                    _odds.set_lottery_type(8);
                                    _odds.set_phase(str13);
                                    _odds.set_old_odds(str40);
                                    _odds.set_new_odds((double.Parse(str40) - num26).ToString());
                                    CallBLL.cz_jp_odds_bll.Add(_odds);
                                }
                                DataTable fgsWTTable = null;
                                if (getUserModelInfo.get_kc_op_odds().Equals(1))
                                {
                                    fgsWTTable = base.GetFgsWTTable(8);
                                }
                                CallBLL.cz_autosale_pkbjl_bll.DLAutoSale(_kc.get_order_num(), str9, getUserModelInfo.get_u_nicker(), getUserModelInfo.get_u_name(), str16, table8.Rows[0]["play_id"].ToString(), str24.Trim(), str31, str32, str33, str34, str35, str36, str14, str13, _kc.get_ip(), num, _kc.get_lottery_name(), getUserModelInfo, rate, fgsWTTable, str10, str11);
                                if (getUserModelInfo.get_kc_op_odds().Equals(1))
                                {
                                    fgsWTTable = base.GetFgsWTTable(8);
                                }
                                CallBLL.cz_autosale_pkbjl_bll.ZDAutoSale(_kc.get_order_num(), str9, getUserModelInfo.get_u_nicker(), getUserModelInfo.get_u_name(), str16, table8.Rows[0]["play_id"].ToString(), str24.Trim(), str31, str32, str33, str34, str35, str36, str14, str13, _kc.get_ip(), num, _kc.get_lottery_name(), getUserModelInfo, rate, fgsWTTable, str10, str11);
                                if (getUserModelInfo.get_kc_op_odds().Equals(1))
                                {
                                    fgsWTTable = base.GetFgsWTTable(8);
                                }
                                CallBLL.cz_autosale_pkbjl_bll.GDAutoSale(_kc.get_order_num(), str9, getUserModelInfo.get_u_nicker(), getUserModelInfo.get_u_name(), str16, table8.Rows[0]["play_id"].ToString(), str24.Trim(), str31, str32, str33, str34, str35, str36, str14, str13, _kc.get_ip(), num, _kc.get_lottery_name(), getUserModelInfo, rate, fgsWTTable, str10, str11);
                                if (getUserModelInfo.get_kc_op_odds().Equals(1))
                                {
                                    fgsWTTable = base.GetFgsWTTable(8);
                                }
                                CallBLL.cz_autosale_pkbjl_bll.FGSAutoSale(_kc.get_order_num(), str9, getUserModelInfo.get_u_nicker(), getUserModelInfo.get_u_name(), str16, table8.Rows[0]["play_id"].ToString(), str24.Trim(), str31, str32, str33, str34, str35, str36, str14, str13, _kc.get_ip(), num, _kc.get_lottery_name(), getUserModelInfo, rate, fgsWTTable, str10, str11);
                                if (FileCacheHelper.get_AddBetLockUType_KC().Equals("zj"))
                                {
                                    num29 = 8;
                                    base.Un_Bet_Lock(num29.ToString(), getUserModelInfo.get_zjname(), str24);
                                }
                                else
                                {
                                    base.Un_Bet_Lock(8.ToString(), rate.get_fgsname(), str24);
                                }
                                num13++;
                                if (FileCacheHelper.get_GetKCPutMoneyCache() == "1")
                                {
                                    base.SetUserDrawback_pkbjl(dataTable, str27.ToString());
                                }
                            }
                            base.SetUserRate_kc(rate);
                            if (FileCacheHelper.get_GetKCPutMoneyCache() != "1")
                            {
                                base.SetUserDrawback_pkbjl(dataTable);
                            }
                            getUserModelInfo.set_begin_kc("yes");
                            if (flag2)
                            {
                                base.DeleteCreditLock(str);
                                List<FastSale> list4 = base.UserPutBet(plDT, successBetList, strArray3, strArray4, strArray5);
                                result.set_tipinfo(string.Format("下注提示！", new object[0]));
                                result.set_success(210);
                                dictionary.Add("JeuValidate", base.get_JeuValidate());
                                dictionary.Add("returnlist", list4);
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
                    }
                }
            }
        }

        public void get_oddsinfo(HttpContext context, ref string strResult)
        {
            string str = LSRequest.qq("playid");
            string str2 = LSRequest.qq("playpage");
            int num = Convert.ToInt32(LSRequest.qq("playtype"));
            string numTable = Utils.GetPKBJL_NumTable(str2);
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
                CookieHelper.SetCookie("PKBJL_TableType_Record_Current_User", str2);
                string str4 = context.Session["user_name"].ToString();
                cz_userinfo_session getUserModelInfo = base.GetUserModelInfo;
                string str5 = getUserModelInfo.get_su_type().ToString();
                string str6 = getUserModelInfo.get_u_name().ToString();
                string str7 = getUserModelInfo.get_kc_kind().Trim();
                ReturnResult result = new ReturnResult();
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                dictionary.Add("type", "get_oddsinfo");
                dictionary.Add("playpage", str2);
                DataTable table = CallBLL.cz_users_bll.GetCredit(str6, str5).Tables[0];
                string str8 = table.Rows[0]["kc_credit"].ToString();
                string s = table.Rows[0]["kc_usable_credit"].ToString();
                dictionary.Add("credit", Convert.ToDouble(str8).ToString());
                dictionary.Add("usable_credit", (int) double.Parse(s));
                DataSet set2 = CallBLL.cz_play_pkbjl_bll.GetPlay(str, str6, str5, numTable);
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
                        string str10 = row["openning"].ToString();
                        string str11 = row["isopen"].ToString();
                        string str12 = row["opendate"].ToString();
                        string str13 = row["endtime"].ToString();
                        string str14 = row["nn"].ToString();
                        string str15 = row["p_id"].ToString();
                        if (str10.Equals("n"))
                        {
                            string str16 = string.Concat(new object[] { "Poker_", 8, "_", str15, "_", numTable });
                            if (string.IsNullOrEmpty(CacheHelper.GetCache(str16)))
                            {
                                int num4 = 8;
                                base.Add_TenPoker_Lock(num4.ToString(), str15, numTable);
                                cz_phase_pkbjl tenPoker = CallBLL.cz_phase_pkbjl_bll.GetTenPoker(str15, numTable);
                                if ((tenPoker == null) || string.IsNullOrEmpty(tenPoker.get_ten_poker()))
                                {
                                    CallBLL.cz_poker_pkbjl_bll.InitToPhase(numTable, str15);
                                }
                                base.Un_TenPoker_Lock(8.ToString(), str15, numTable);
                                CacheHelper.SetCache(str16, DateTime.Now, DateTime.MaxValue, TimeSpan.FromSeconds(600.0));
                            }
                        }
                        dictionary.Add("p_id", row["p_id"].ToString());
                        dictionary.Add("openning", row["openning"].ToString());
                        dictionary.Add("isOpen", row["isopen"].ToString());
                        DataTable table5 = CallBLL.cz_phase_pkbjl_bll.GetCurrentPhase(numTable).Tables[0];
                        string str17 = table5.Rows[0]["sold"].ToString().Trim();
                        string str18 = table5.Rows[0]["surplus"].ToString().Trim();
                        dictionary.Add("quantity", string.Format("{0},{1}", str17, str18));
                        DataTable newOpenPhaseByNumTable = CallBLL.cz_phase_pkbjl_bll.GetNewOpenPhaseByNumTable(numTable);
                        dictionary.Add("porkList", newOpenPhaseByNumTable.Rows[0]["ten_poker"].ToString());
                        if (!newOpenPhaseByNumTable.Rows[0]["currentphase"].ToString().Equals(newOpenPhaseByNumTable.Rows[0]["previousphase"].ToString()))
                        {
                            dictionary.Add("openNumber", "");
                        }
                        else
                        {
                            dictionary.Add("openNumber", newOpenPhaseByNumTable.Rows[0]["nns"].ToString());
                        }
                        dictionary.Add("openNumberPorkIndex", Utils.GetPKBJLOpenIndex(newOpenPhaseByNumTable.Rows[0]["nns"].ToString(), newOpenPhaseByNumTable.Rows[0]["zhuang_nn"].ToString(), newOpenPhaseByNumTable.Rows[0]["xian_nn"].ToString()));
                        dictionary.Add("jq", newOpenPhaseByNumTable.Rows[0]["currentphase"].ToString());
                        dictionary.Add("lastJq", newOpenPhaseByNumTable.Rows[0]["previousphase"].ToString());
                        dictionary.Add("stock", CallBLL.cz_poker_pkbjl_bll.GetSurplusPoker(numTable).ToString());
                        dictionary.Add("noticeIndex", Utils.GetPKBJLBalanceWinOddsID(newOpenPhaseByNumTable.Rows[0]["zhuang_nn"].ToString(), newOpenPhaseByNumTable.Rows[0]["xian_nn"].ToString()));
                        Dictionary<string, string> dictionary2 = new Dictionary<string, string>();
                        dictionary2.Add("player", newOpenPhaseByNumTable.Rows[0]["xian_nn"].ToString());
                        dictionary2.Add("banker", newOpenPhaseByNumTable.Rows[0]["zhuang_nn"].ToString());
                        dictionary.Add("lastResult", dictionary2);
                        dictionary.Add("history", CallBLL.cz_phase_pkbjl_bll.GetHistoryString(numTable));
                        dictionary.Add("time", str13);
                        dictionary.Add("closeTime", str12);
                        dictionary.Add("profit", base.GetKCProfit());
                        Dictionary<string, List<string>> pAILU = base.GetPAILU(numTable);
                        dictionary.Add("road", pAILU);
                        DataTable drawbackByPlayIds = null;
                        if (FileCacheHelper.get_GetKCPutMoneyCache() == "1")
                        {
                            foreach (string str19 in str.Split(new char[] { ',' }))
                            {
                                DataTable table9;
                                if (CacheHelper.GetCache("kc_drawback_FileCacheKey_pkbjl" + str19 + context.Session["user_name"].ToString()) != null)
                                {
                                    DataTable cache = CacheHelper.GetCache("kc_drawback_FileCacheKey_pkbjl" + str19 + context.Session["user_name"].ToString()) as DataTable;
                                    if (drawbackByPlayIds == null)
                                    {
                                        drawbackByPlayIds = cache;
                                    }
                                    else
                                    {
                                        table9 = drawbackByPlayIds.Clone();
                                        table9 = cache;
                                        if (table9 != null)
                                        {
                                            drawbackByPlayIds.Merge(table9);
                                        }
                                    }
                                }
                                else if (drawbackByPlayIds == null)
                                {
                                    drawbackByPlayIds = CallBLL.cz_drawback_pkbjl_bll.GetDrawbackByPlayIds(str19, getUserModelInfo.get_u_name());
                                }
                                else
                                {
                                    table9 = drawbackByPlayIds.Clone();
                                    table9 = CallBLL.cz_drawback_pkbjl_bll.GetDrawbackByPlayIds(str19, getUserModelInfo.get_u_name());
                                    if (table9 != null)
                                    {
                                        drawbackByPlayIds.Merge(table9);
                                    }
                                }
                            }
                        }
                        else if (CacheHelper.GetCache("kc_drawback_FileCacheKey_pkbjl" + this.Session["user_name"].ToString()) != null)
                        {
                            drawbackByPlayIds = CacheHelper.GetCache("kc_drawback_FileCacheKey_pkbjl" + this.Session["user_name"].ToString()) as DataTable;
                        }
                        else
                        {
                            drawbackByPlayIds = CallBLL.cz_drawback_pkbjl_bll.GetDrawbackByPlayIds(str, getUserModelInfo.get_u_name());
                        }
                        Dictionary<string, object> dictionary4 = new Dictionary<string, object>();
                        Dictionary<string, object> dictionary5 = new Dictionary<string, object>();
                        List<double> collection = new List<double>();
                        foreach (DataRow row2 in table3.Rows)
                        {
                            double num3;
                            string key = "";
                            string pl = "";
                            string str22 = "";
                            string str23 = "";
                            string str24 = "";
                            string str25 = "";
                            string str26 = "";
                            key = row2["play_id"].ToString() + "_" + row2["odds_id"].ToString();
                            string str27 = row2["current_odds"].ToString();
                            if (str10 == "n")
                            {
                                str27 = "-";
                            }
                            string str28 = row2[str7 + "_diff"].ToString().Trim();
                            if (str27 != "-")
                            {
                                num3 = double.Parse(str27) + double.Parse(str28);
                                pl = num3.ToString();
                            }
                            else
                            {
                                pl = str27;
                            }
                            if (str27 != "-")
                            {
                                base.GetOdds_KC(8, row2["odds_id"].ToString(), ref pl);
                            }
                            DataRow[] rowArray = drawbackByPlayIds.Select(string.Format(" play_id={0} and u_name='{1}' ", row2["play_id"].ToString(), getUserModelInfo.get_u_name()));
                            string str29 = rowArray[0]["single_phase_amount"].ToString();
                            string str30 = rowArray[0]["single_max_amount"].ToString();
                            string str31 = rowArray[0]["single_min_amount"].ToString();
                            str22 = row2["allow_min_amount"].ToString();
                            str23 = row2["allow_max_amount"].ToString();
                            str24 = row2["max_appoint"].ToString();
                            str25 = row2["total_amount"].ToString();
                            str26 = row2["allow_max_put_amount"].ToString();
                            if (Convert.ToDecimal(str23) > Convert.ToDecimal(str26))
                            {
                                str23 = row2["allow_max_put_amount"].ToString();
                            }
                            if (Convert.ToDecimal(str23) > Convert.ToDecimal(str25))
                            {
                                str23 = row2["total_amount"].ToString();
                            }
                            if (double.Parse(str22) < double.Parse(str31))
                            {
                                str22 = str31;
                            }
                            if (double.Parse(str23) > double.Parse(str30))
                            {
                                str23 = str30;
                            }
                            if (num.Equals(0))
                            {
                                if (!pl.Equals("-"))
                                {
                                    if (key.Equals("81004_82005"))
                                    {
                                        num3 = Convert.ToDouble(pl) + base.GetPlayTypeWTValue_PKBJL("82005");
                                        pl = num3.ToString();
                                    }
                                    if (key.Equals("81004_82006"))
                                    {
                                        pl = (Convert.ToDouble(pl) + base.GetPlayTypeWTValue_PKBJL("82006")).ToString();
                                    }
                                }
                                dictionary5.Add("pl", pl);
                            }
                            else
                            {
                                dictionary5.Add("pl", pl);
                            }
                            dictionary5.Add("plx", new List<double>(collection));
                            dictionary5.Add("min_amount", Convert.ToDouble(str22).ToString());
                            dictionary5.Add("max_amount", Convert.ToDouble(str23).ToString());
                            dictionary5.Add("top_amount", Convert.ToDouble(str24).ToString());
                            dictionary5.Add("dq_max_amount", Convert.ToDouble(str25).ToString());
                            dictionary5.Add("dh_max_amount", Convert.ToDouble(str26).ToString());
                            dictionary4.Add(key, new Dictionary<string, object>(dictionary5));
                            dictionary5.Clear();
                            collection.Clear();
                        }
                        dictionary.Add("play_odds", dictionary4);
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
                string str = LSRequest.qq("playpage");
                string numTable = "";
                numTable = Utils.GetPKBJL_NumTable(str);
                DataTable kCOpenBall = base.GetKCOpenBall(8, numTable);
                if (kCOpenBall != null)
                {
                    DataRow row = kCOpenBall.Rows[0];
                    dictionary.Add("jq", row["currentphase"].ToString());
                    dictionary.Add("porkList", row["ten_poker"].ToString());
                    if (!row["currentphase"].ToString().Equals(row["previousphase"].ToString()))
                    {
                        dictionary.Add("openNumber", "");
                    }
                    else
                    {
                        dictionary.Add("openNumber", row["nns"].ToString());
                    }
                    dictionary.Add("openNumberPorkIndex", Utils.GetPKBJLOpenIndex(row["nns"].ToString(), row["zhuang_nn"].ToString(), row["xian_nn"].ToString()));
                    DataTable table2 = CallBLL.cz_phase_pkbjl_bll.GetCurrentPhase(numTable).Tables[0];
                    string str3 = table2.Rows[0]["sold"].ToString().Trim();
                    string str4 = table2.Rows[0]["surplus"].ToString().Trim();
                    dictionary.Add("quantity", string.Format("{0},{1}", str3, str4));
                    dictionary.Add("lastJq", row["previousphase"].ToString());
                    dictionary.Add("stock", CallBLL.cz_poker_pkbjl_bll.GetSurplusPoker(numTable).ToString());
                    dictionary.Add("noticeIndex", Utils.GetPKBJLBalanceWinOddsID(row["zhuang_nn"].ToString(), row["xian_nn"].ToString()));
                    Dictionary<string, string> dictionary2 = new Dictionary<string, string>();
                    dictionary2.Add("player", row["xian_nn"].ToString());
                    dictionary2.Add("banker", row["zhuang_nn"].ToString());
                    dictionary.Add("lastResult", dictionary2);
                    dictionary.Add("profit", base.GetKCProfit());
                    dictionary.Add("history", CallBLL.cz_phase_pkbjl_bll.GetHistoryString(numTable));
                    dictionary.Add("road", base.GetPAILU(numTable));
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

        public override void ProcessRequest(HttpContext context)
        {
            base.checkLogin();
            base.IsLotteryExist(8.ToString());
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
                        goto Label_00C4;
                    }
                    if (str3 == "get_putinfo")
                    {
                        this.get_putinfo(context, ref strResult);
                        goto Label_00C4;
                    }
                    if (str3 == "put_money")
                    {
                        this.put_money(context, ref strResult);
                        goto Label_00C4;
                    }
                }
                else
                {
                    this.get_oddsinfo(context, ref strResult);
                    goto Label_00C4;
                }
            }
            this.do_default(context, ref strResult);
        Label_00C4:
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
    }
}

