namespace User.Web.L_K8SC.Handler
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
            ReturnResult result = new ReturnResult();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("type", "put_money");
            int num = 6;
            string s = LSRequest.qq("phaseid");
            string str3 = LSRequest.qq("playpage");
            string str4 = LSRequest.qq("oddsid");
            string str5 = LSRequest.qq("uPI_P");
            string str6 = LSRequest.qq("uPI_M");
            string[] strArray = LSRequest.qq("i_index").Split(new char[] { ',' });
            string str7 = LSRequest.qq("JeuValidate");
            if (((context.Session["JeuValidate"] == null) || (context.Session["JeuValidate"].ToString().Trim() != str7)) || (context.Session["JeuValidate"].ToString().Trim() == ""))
            {
                result.set_tipinfo(PageBase.GetMessageByCache("u100012", "MessageHint"));
                result.set_success(400);
                dictionary.Add("JeuValidate", base.get_JeuValidate());
                result.set_data(dictionary);
                strResult = JsonHandle.ObjectToJson(result);
                return;
            }
            context.Session["JeuValidate"] = "";
            if (!base.BetCreditLock(str))
            {
                result.set_success(400);
                result.set_tipinfo("系統繁忙，請稍後！");
                dictionary.Add("JeuValidate", base.get_JeuValidate());
                result.set_data(dictionary);
                strResult = JsonHandle.ObjectToJson(result);
                return;
            }
            user_kc_rate _rate = base.GetUserRate_kc(getUserModelInfo.get_zjname());
            DataTable table = null;
            DataSet set = CallBLL.cz_users_bll.AccountIsDisabled(_rate.get_fgsname(), _rate.get_gdname(), _rate.get_zdname(), _rate.get_dlname(), getUserModelInfo.get_u_name());
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
                    string str9 = base.get_JeuValidate();
                    dictionary.Add("JeuValidate", str9);
                    result.set_data(dictionary);
                    strResult = JsonHandle.ObjectToJson(result);
                    return;
                }
            }
            string[] source = str4.Split(new char[] { ',' });
            if (source.Distinct<string>().ToList<string>().Count != source.Length)
            {
                base.DeleteCreditLock(str);
                context.Response.End();
            }
            if (source.Length > FileCacheHelper.get_GetK8SCMaxGroup())
            {
                base.DeleteCreditLock(str);
                result.set_success(400);
                result.set_tipinfo(string.Format("您選擇的號碼超過了最大{0}組，請重新選擇號碼!", FileCacheHelper.get_GetK8SCMaxGroup()));
                dictionary.Add("JeuValidate", base.get_JeuValidate());
                result.set_data(dictionary);
                strResult = JsonHandle.ObjectToJson(result);
                return;
            }
            int index = 0;
        Label_04F6:;
            if (index < str6.Split(new char[] { ',' }).Length)
            {
                if (!base.IsNumber(str6.Split(new char[] { ',' })[index]))
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
                goto Label_04F6;
            }
            string str10 = "";
            string str11 = "";
            if (!Utils.IsInteger(s))
            {
                base.Response.End();
            }
            DataTable table2 = CallBLL.cz_phase_k8sc_bll.GetIsClosedByTime(int.Parse(s)).Tables[0];
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
                str10 = table2.Rows[0]["phase"].ToString();
                str11 = table2.Rows[0]["p_id"].ToString();
                time = Convert.ToDateTime(table2.Rows[0]["stop_date"].ToString());
                num29 = 6;
                string str12 = base.BetReceiveEnd(num29.ToString());
                if (!string.IsNullOrEmpty(str12))
                {
                    time = Convert.ToDateTime(table2.Rows[0]["play_open_date"].ToString()).AddSeconds((double) -int.Parse(str12));
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
            int num4 = 1;
            if (!getUserModelInfo.get_su_type().ToString().Trim().Equals("dl"))
            {
                num4 = -1;
            }
            string str14 = "";
            string str15 = "";
            double num5 = 0.0;
            DataTable table3 = CallBLL.cz_users_bll.GetUserRate(str, 2).Tables[0];
            str14 = table3.Rows[0]["kc_kind"].ToString().Trim().ToUpper();
            str15 = table3.Rows[0]["su_type"].ToString().Trim();
            num5 = double.Parse(table3.Rows[0]["kc_usable_credit"].ToString().Trim());
            getUserModelInfo.set_kc_kind(str14.ToUpper());
            double num6 = 0.0;
            double num7 = 0.0;
            double num8 = 0.0;
            double num9 = 0.0;
            double num10 = 0.0;
            double num11 = 0.0;
            string str16 = "";
            string str17 = "";
            string str18 = "";
            string str19 = "";
            string str20 = "";
            string[] strArray3 = str4.Split(new char[] { ',' });
            string[] strArray4 = str5.Split(new char[] { ',' });
            string[] strArray5 = str6.Split(new char[] { ',' });
            DataTable plDT = CallBLL.cz_odds_k8sc_bll.GetPlayOddsByID(str4).Tables[0];
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
                DataTable table5 = CallBLL.cz_bet_kc_bll.GetSinglePhaseByBet(str10, str, num.ToString(), str4, null).Tables[0];
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
                    string str21 = "";
                    List<string> list = new List<string>();
                    List<string> list2 = new List<string>();
                    int num13 = 0;
                    foreach (string str22 in strArray3)
                    {
                        num6 = 0.0;
                        foreach (DataRow row in plDT.Rows)
                        {
                            if (row["odds_id"].ToString().Trim() == str22.Trim())
                            {
                                str21 = row["is_open"].ToString().Trim();
                                str17 = row["current_odds"].ToString().Trim();
                                str18 = row[str14 + "_diff"].ToString().Trim();
                                str16 = row["play_id"].ToString().Trim();
                                str19 = row["play_name"].ToString().Trim();
                                str20 = row["put_amount"].ToString().Trim();
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
                                if (row2["odds_id"].ToString().Trim() == str22.Trim())
                                {
                                    num6 = double.Parse(row2["sumbet"].ToString().Trim());
                                    break;
                                }
                            }
                        }
                        DataTable table6 = null;
                        if (FileCacheHelper.get_GetKCPutMoneyCache() == "1")
                        {
                            if (CacheHelper.GetCache("kc_drawback_FileCacheKey_k8sc" + str16 + this.Session["user_name"].ToString()) == null)
                            {
                                table6 = CallBLL.cz_drawback_k8sc_bll.GetDrawback(str16, str).Tables[0];
                            }
                            else
                            {
                                table6 = base.GetUserDrawback_k8sc(_rate, getUserModelInfo.get_kc_kind(), str16);
                            }
                        }
                        else if (CacheHelper.GetCache("kc_drawback_FileCacheKey_k8sc" + this.Session["user_name"].ToString()) == null)
                        {
                            table6 = CallBLL.cz_drawback_k8sc_bll.GetDrawback(str16, str).Tables[0];
                        }
                        else
                        {
                            table6 = base.GetUserDrawback_k8sc(_rate, getUserModelInfo.get_kc_kind());
                        }
                        DataRow[] rowArray = table6.Select(string.Format(" play_id={0} and u_name='{1}' ", str16, str));
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
                        if (str21 != "1")
                        {
                            base.DeleteCreditLock(str);
                            result.set_tipinfo(string.Format("{0}<{1}>已經停止投注！", str19, str20));
                            result.set_success(400);
                            dictionary.Add("JeuValidate", base.get_JeuValidate());
                            result.set_data(dictionary);
                            strResult = JsonHandle.ObjectToJson(result);
                            return;
                        }
                        num12 = double.Parse(str17) + double.Parse(str18);
                        string pl = num12.ToString();
                        base.GetOdds_KC(6, str22, ref pl);
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
                            result.set_tipinfo(string.Format("{0}<{1}>下注單期金額超出單期最大金額！", str19, str20));
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
                        foreach (string str24 in strArray3)
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
                            foreach (string str22 in strArray3)
                            {
                                double num19;
                                if (FileCacheHelper.get_AddBetLockUType_KC().Equals("zj"))
                                {
                                    num29 = 6;
                                    base.Add_Bet_Lock(num29.ToString(), getUserModelInfo.get_zjname(), str22);
                                }
                                DataTable table8 = CallBLL.cz_odds_k8sc_bll.GetOddsByID(str22).Tables[0];
                                string str25 = table8.Rows[0]["play_id"].ToString();
                                string str26 = table8.Rows[0]["play_name"].ToString();
                                string str27 = table8.Rows[0]["put_amount"].ToString();
                                string ratio = table8.Rows[0]["ratio"].ToString();
                                if (FileCacheHelper.get_GetKCPutMoneyCache() == "1")
                                {
                                    dataTable = base.GetUserDrawback_k8sc(_rate, getUserModelInfo.get_kc_kind(), str25.ToString());
                                }
                                else
                                {
                                    dataTable = base.GetUserDrawback_k8sc(_rate, getUserModelInfo.get_kc_kind());
                                }
                                if (dataTable == null)
                                {
                                    base.DeleteCreditLock(str);
                                    if (FileCacheHelper.get_AddBetLockUType_KC().Equals("zj"))
                                    {
                                        num29 = 6;
                                        base.Un_Bet_Lock(num29.ToString(), getUserModelInfo.get_zjname(), str22);
                                    }
                                    else
                                    {
                                        num29 = 6;
                                        base.Un_Bet_Lock(num29.ToString(), _rate.get_fgsname(), str22);
                                    }
                                    result.set_tipinfo(string.Format("系統錯誤！", new object[0]));
                                    result.set_success(400);
                                    dictionary.Add("JeuValidate", base.get_JeuValidate());
                                    result.set_data(dictionary);
                                    strResult = JsonHandle.ObjectToJson(result);
                                    return;
                                }
                                DataRow[] rowArray2 = dataTable.Select(string.Format(" play_id={0} ", str25));
                                string str29 = "0";
                                string str30 = "0";
                                string str31 = "0";
                                string str32 = "0";
                                string str33 = "0";
                                string str34 = "0";
                                double num17 = 0.0;
                                double num18 = 0.0;
                                if (_rate.get_zcyg().Equals("1"))
                                {
                                    num19 = (((100.0 - double.Parse(_rate.get_fgszc())) - double.Parse(_rate.get_gdzc())) - double.Parse(_rate.get_zdzc())) - double.Parse(_rate.get_dlzc());
                                    num17 = num19;
                                    num18 = double.Parse(_rate.get_fgszc());
                                }
                                else
                                {
                                    num19 = (((100.0 - double.Parse(_rate.get_zjzc())) - double.Parse(_rate.get_gdzc())) - double.Parse(_rate.get_zdzc())) - double.Parse(_rate.get_dlzc());
                                    num18 = num19;
                                    num17 = double.Parse(_rate.get_zjzc());
                                }
                                string str35 = "A";
                                if (getUserModelInfo.get_kc_kind().Trim().Equals("0") || string.IsNullOrEmpty(getUserModelInfo.get_kc_kind()))
                                {
                                    str35 = "A";
                                }
                                else
                                {
                                    str35 = getUserModelInfo.get_kc_kind().Trim();
                                }
                                str35 = str14;
                                foreach (DataRow row in rowArray2)
                                {
                                    string str39 = row["u_type"].ToString().Trim();
                                    if (str39 != null)
                                    {
                                        if (!(str39 == "zj"))
                                        {
                                            if (str39 == "fgs")
                                            {
                                                goto Label_15D7;
                                            }
                                            if (str39 == "gd")
                                            {
                                                goto Label_15FB;
                                            }
                                            if (str39 == "zd")
                                            {
                                                goto Label_161F;
                                            }
                                            if (str39 == "dl")
                                            {
                                                goto Label_1643;
                                            }
                                            if (str39 == "hy")
                                            {
                                                goto Label_1667;
                                            }
                                        }
                                        else
                                        {
                                            str29 = row[str14 + "_drawback"].ToString().Trim();
                                        }
                                    }
                                    continue;
                                Label_15D7:
                                    str30 = row[str14 + "_drawback"].ToString().Trim();
                                    continue;
                                Label_15FB:
                                    str31 = row[str14 + "_drawback"].ToString().Trim();
                                    continue;
                                Label_161F:
                                    str32 = row[str14 + "_drawback"].ToString().Trim();
                                    continue;
                                Label_1643:
                                    str33 = row[str14 + "_drawback"].ToString().Trim();
                                    continue;
                                Label_1667:
                                    if (getUserModelInfo.get_su_type() == "dl")
                                    {
                                        str34 = row[str14 + "_drawback"].ToString().Trim();
                                    }
                                    else if (getUserModelInfo.get_su_type() == "zd")
                                    {
                                        str33 = row[str14 + "_drawback"].ToString().Trim();
                                        str34 = row[str14 + "_drawback"].ToString().Trim();
                                    }
                                    else if (getUserModelInfo.get_su_type() == "gd")
                                    {
                                        str32 = row[str14 + "_drawback"].ToString().Trim();
                                        str34 = row[str14 + "_drawback"].ToString().Trim();
                                    }
                                    else if (getUserModelInfo.get_su_type() == "fgs")
                                    {
                                        str31 = row[str14 + "_drawback"].ToString().Trim();
                                        str34 = row[str14 + "_drawback"].ToString().Trim();
                                    }
                                }
                                if (DateTime.Now >= time.AddSeconds(0.0))
                                {
                                    flag2 = true;
                                    break;
                                }
                                if (!FileCacheHelper.get_AddBetLockUType_KC().Equals("zj"))
                                {
                                    num29 = 6;
                                    base.Add_Bet_Lock(num29.ToString(), _rate.get_fgsname(), str22);
                                }
                                double num32 = double.Parse(table8.Rows[0]["current_odds"].ToString()) + Convert.ToDouble(table8.Rows[0][str14 + "_diff"].ToString().Trim());
                                string str36 = num32.ToString();
                                string str37 = str36.ToString();
                                base.GetOdds_KC(6, str22, ref str37);
                                double playDrawbackValue = base.GetPlayDrawbackValue(str30, ratio);
                                if ((playDrawbackValue != 0.0) && (double.Parse(str36) > playDrawbackValue))
                                {
                                    base.DeleteCreditLock(str);
                                    if (FileCacheHelper.get_AddBetLockUType_KC().Equals("zj"))
                                    {
                                        num29 = 6;
                                        base.Un_Bet_Lock(num29.ToString(), getUserModelInfo.get_zjname(), str22);
                                    }
                                    else
                                    {
                                        num29 = 6;
                                        base.Un_Bet_Lock(num29.ToString(), _rate.get_fgsname(), str22);
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
                                _kc.set_checkcode(str7);
                                _kc.set_u_name(getUserModelInfo.get_u_name());
                                _kc.set_u_nicker(getUserModelInfo.get_u_nicker());
                                _kc.set_phase_id(new int?(int.Parse(str11)));
                                _kc.set_phase(str10);
                                _kc.set_bet_time(nullable);
                                _kc.set_odds_id(new int?(int.Parse(str22)));
                                _kc.set_category(table8.Rows[0]["category"].ToString());
                                _kc.set_play_id(new int?(int.Parse(table8.Rows[0]["play_id"].ToString())));
                                _kc.set_play_name(str26);
                                _kc.set_bet_val(str27);
                                _kc.set_odds(str37);
                                _kc.set_amount(new decimal?(decimal.Parse(strArray5[num13])));
                                _kc.set_profit(0);
                                _kc.set_hy_drawback(new decimal?(decimal.Parse(str34)));
                                _kc.set_dl_drawback(new decimal?(decimal.Parse(str33)));
                                _kc.set_zd_drawback(new decimal?(decimal.Parse(str32)));
                                _kc.set_gd_drawback(new decimal?(decimal.Parse(str31)));
                                _kc.set_fgs_drawback(new decimal?(decimal.Parse(str30)));
                                _kc.set_zj_drawback(new decimal?(decimal.Parse(str29)));
                                _kc.set_dl_rate((float) int.Parse(_rate.get_dlzc()));
                                _kc.set_zd_rate((float) int.Parse(_rate.get_zdzc()));
                                _kc.set_gd_rate((float) int.Parse(_rate.get_gdzc()));
                                _kc.set_fgs_rate(float.Parse(num18.ToString()));
                                _kc.set_zj_rate(float.Parse(num17.ToString()));
                                _kc.set_dl_name(_rate.get_dlname());
                                _kc.set_zd_name(_rate.get_zdname());
                                _kc.set_gd_name(_rate.get_gdname());
                                _kc.set_fgs_name(_rate.get_fgsname());
                                _kc.set_is_payment(0);
                                _kc.set_m_type(new int?(num4));
                                _kc.set_kind(str14);
                                _kc.set_ip(LSRequest.GetIP());
                                _kc.set_lottery_type(new int?(num));
                                _kc.set_lottery_name(base.GetGameNameByID(_kc.get_lottery_type().ToString()));
                                _kc.set_ordervalidcode(Utils.GetOrderValidCode(_kc.get_u_name(), _kc.get_order_num(), _kc.get_bet_val(), _kc.get_odds(), _kc.get_kind(), Convert.ToInt32(_kc.get_phase_id()), Convert.ToInt32(_kc.get_odds_id()), Convert.ToDouble(_kc.get_amount())));
                                _kc.set_odds_zj(str36);
                                int num21 = 0;
                                if (!CallBLL.cz_bet_kc_bll.AddBet(_kc, decimal.Parse(strArray5[num13]), str, ref num21) || (num21 <= 0))
                                {
                                    base.DeleteCreditLock(str);
                                    if (FileCacheHelper.get_AddBetLockUType_KC().Equals("zj"))
                                    {
                                        num29 = 6;
                                        base.Un_Bet_Lock(num29.ToString(), getUserModelInfo.get_zjname(), str22);
                                    }
                                    else
                                    {
                                        num29 = 6;
                                        base.Un_Bet_Lock(num29.ToString(), _rate.get_fgsname(), str22);
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
                                CallBLL.cz_odds_k8sc_bll.UpdateGrandTotal(Convert.ToDecimal(num22), int.Parse(str22));
                                double num23 = double.Parse(table8.Rows[0]["grand_total"].ToString()) + num22;
                                double num24 = double.Parse(table8.Rows[0]["downbase"].ToString());
                                double num25 = Math.Floor((double) (num23 / num24));
                                if ((num25 >= 1.0) && (num24 >= 1.0))
                                {
                                    double num26 = double.Parse(table8.Rows[0]["down_odds_rate"].ToString()) * num25;
                                    int num28 = CallBLL.cz_odds_k8sc_bll.UpdateGrandTotalCurrentOdds(num26.ToString(), (num23 - (num24 * num25)).ToString(), str22.ToString());
                                    cz_system_log _log = new cz_system_log();
                                    _log.set_user_name("系統");
                                    _log.set_children_name("");
                                    _log.set_category(table8.Rows[0]["category"].ToString());
                                    _log.set_play_name(str26);
                                    _log.set_put_amount(str27);
                                    num29 = 6;
                                    _log.set_l_name(base.GetGameNameByID(num29.ToString()));
                                    _log.set_l_phase(str10);
                                    _log.set_action("降賠率");
                                    _log.set_odds_id(int.Parse(str22));
                                    string str38 = table8.Rows[0]["current_odds"].ToString();
                                    _log.set_old_val(str38);
                                    num32 = double.Parse(str38) - num26;
                                    _log.set_new_val(num32.ToString());
                                    _log.set_ip(LSRequest.GetIP());
                                    _log.set_add_time(DateTime.Now);
                                    _log.set_note("系統自動降賠");
                                    _log.set_type_id(Convert.ToInt32((LSEnums.LogTypeID) 0));
                                    _log.set_lottery_type(6);
                                    CallBLL.cz_system_log_bll.Add(_log);
                                    cz_jp_odds _odds = new cz_jp_odds();
                                    _odds.set_add_time(DateTime.Now);
                                    _odds.set_odds_id(int.Parse(str22));
                                    _odds.set_phase_id(int.Parse(str11));
                                    _odds.set_play_name(str26);
                                    _odds.set_put_amount(str27);
                                    _odds.set_odds(num26.ToString());
                                    _odds.set_lottery_type(6);
                                    _odds.set_phase(str10);
                                    _odds.set_old_odds(str38);
                                    _odds.set_new_odds((double.Parse(str38) - num26).ToString());
                                    CallBLL.cz_jp_odds_bll.Add(_odds);
                                }
                                DataTable fgsWTTable = null;
                                if (getUserModelInfo.get_kc_op_odds().Equals(1))
                                {
                                    fgsWTTable = base.GetFgsWTTable(6);
                                }
                                CallBLL.cz_autosale_k8sc_bll.DLAutoSale(_kc.get_order_num(), str7, getUserModelInfo.get_u_nicker(), getUserModelInfo.get_u_name(), str14, table8.Rows[0]["play_id"].ToString(), str22.Trim(), str29, str30, str31, str32, str33, str34, str11, str10, _kc.get_ip(), num, _kc.get_lottery_name(), getUserModelInfo, _rate, fgsWTTable);
                                if (getUserModelInfo.get_kc_op_odds().Equals(1))
                                {
                                    fgsWTTable = base.GetFgsWTTable(6);
                                }
                                CallBLL.cz_autosale_k8sc_bll.ZDAutoSale(_kc.get_order_num(), str7, getUserModelInfo.get_u_nicker(), getUserModelInfo.get_u_name(), str14, table8.Rows[0]["play_id"].ToString(), str22.Trim(), str29, str30, str31, str32, str33, str34, str11, str10, _kc.get_ip(), num, _kc.get_lottery_name(), getUserModelInfo, _rate, fgsWTTable);
                                if (getUserModelInfo.get_kc_op_odds().Equals(1))
                                {
                                    fgsWTTable = base.GetFgsWTTable(6);
                                }
                                CallBLL.cz_autosale_k8sc_bll.GDAutoSale(_kc.get_order_num(), str7, getUserModelInfo.get_u_nicker(), getUserModelInfo.get_u_name(), str14, table8.Rows[0]["play_id"].ToString(), str22.Trim(), str29, str30, str31, str32, str33, str34, str11, str10, _kc.get_ip(), num, _kc.get_lottery_name(), getUserModelInfo, _rate, fgsWTTable);
                                if (getUserModelInfo.get_kc_op_odds().Equals(1))
                                {
                                    fgsWTTable = base.GetFgsWTTable(6);
                                }
                                CallBLL.cz_autosale_k8sc_bll.FGSAutoSale(_kc.get_order_num(), str7, getUserModelInfo.get_u_nicker(), getUserModelInfo.get_u_name(), str14, table8.Rows[0]["play_id"].ToString(), str22.Trim(), str29, str30, str31, str32, str33, str34, str11, str10, _kc.get_ip(), num, _kc.get_lottery_name(), getUserModelInfo, _rate, fgsWTTable);
                                if (FileCacheHelper.get_AddBetLockUType_KC().Equals("zj"))
                                {
                                    num29 = 6;
                                    base.Un_Bet_Lock(num29.ToString(), getUserModelInfo.get_zjname(), str22);
                                }
                                else
                                {
                                    base.Un_Bet_Lock(6.ToString(), _rate.get_fgsname(), str22);
                                }
                                num13++;
                                if (FileCacheHelper.get_GetKCPutMoneyCache() == "1")
                                {
                                    base.SetUserDrawback_k8sc(dataTable, str25.ToString());
                                }
                            }
                            base.SetUserRate_kc(_rate);
                            if (FileCacheHelper.get_GetKCPutMoneyCache() != "1")
                            {
                                base.SetUserDrawback_k8sc(dataTable);
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
                DataSet set2 = CallBLL.cz_play_k8sc_bll.GetPlay(str, str5, str4);
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
                                if (CacheHelper.GetCache("kc_drawback_FileCacheKey_k8sc" + str15 + context.Session["user_name"].ToString()) != null)
                                {
                                    DataTable cache = CacheHelper.GetCache("kc_drawback_FileCacheKey_k8sc" + str15 + context.Session["user_name"].ToString()) as DataTable;
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
                                    drawbackByPlayIds = CallBLL.cz_drawback_k8sc_bll.GetDrawbackByPlayIds(str15, getUserModelInfo.get_u_name());
                                }
                                else
                                {
                                    table7 = drawbackByPlayIds.Clone();
                                    table7 = CallBLL.cz_drawback_k8sc_bll.GetDrawbackByPlayIds(str15, getUserModelInfo.get_u_name());
                                    if (table7 != null)
                                    {
                                        drawbackByPlayIds.Merge(table7);
                                    }
                                }
                            }
                        }
                        else if (CacheHelper.GetCache("kc_drawback_FileCacheKey_k8sc" + this.Session["user_name"].ToString()) != null)
                        {
                            drawbackByPlayIds = CacheHelper.GetCache("kc_drawback_FileCacheKey_k8sc" + this.Session["user_name"].ToString()) as DataTable;
                        }
                        else
                        {
                            drawbackByPlayIds = CallBLL.cz_drawback_k8sc_bll.GetDrawbackByPlayIds(str, getUserModelInfo.get_u_name());
                        }
                        Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
                        Dictionary<string, object> dictionary3 = new Dictionary<string, object>();
                        List<double> collection = new List<double>();
                        foreach (DataRow row2 in table3.Rows)
                        {
                            string key = "";
                            string pl = "";
                            string str18 = "";
                            string str19 = "";
                            string str20 = "";
                            string str21 = "";
                            string str22 = "";
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
                                base.GetOdds_KC(6, row2["odds_id"].ToString(), ref pl);
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
                            dictionary3.Add("plx", new List<double>(collection));
                            dictionary3.Add("min_amount", Convert.ToDouble(str18).ToString());
                            dictionary3.Add("max_amount", Convert.ToDouble(str19).ToString());
                            dictionary3.Add("top_amount", Convert.ToDouble(str20).ToString());
                            dictionary3.Add("dq_max_amount", Convert.ToDouble(str21).ToString());
                            dictionary3.Add("dh_max_amount", Convert.ToDouble(str22).ToString());
                            dictionary2.Add(key, new Dictionary<string, object>(dictionary3));
                            dictionary3.Clear();
                            collection.Clear();
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
                DataTable kCOpenBall = base.GetKCOpenBall(6);
                if (kCOpenBall != null)
                {
                    string str = kCOpenBall.Rows[0]["previousphase"].ToString().Trim();
                    string str2 = kCOpenBall.Rows[0]["sold"].ToString().Trim();
                    string str3 = kCOpenBall.Rows[0]["surplus"].ToString().Trim();
                    string str4 = kCOpenBall.Rows[0]["open_date"].ToString().Trim();
                    string str5 = kCOpenBall.Rows[0]["play_open_date"].ToString().Trim();
                    string dateDiff = LSKeys.get_K8SC_Phase_Interval().ToString() + "分鐘";
                    if (!(string.IsNullOrEmpty(str4) || string.IsNullOrEmpty(str5)))
                    {
                        dateDiff = Utils.GetDateDiff(str4, str5);
                    }
                    Dictionary<string, string> dictionary2 = new Dictionary<string, string>();
                    DataRow[] rowArray = base.GetLotteryList().Select(string.Format(" id={0} ", 6));
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
                Dictionary<string, object> dictionary3;
                List<string> list5;
                List<string> list6;
                int num6;
                string str17;
                string str18;
                ReturnResult result = new ReturnResult();
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                dictionary.Add("type", "get_ranklist");
                dictionary.Add("playpage", str);
                string str2 = "_k8sc_jqkj";
                string str3 = "_k8sc_lmcl";
                string str4 = "_k8sc_cql";
                string str5 = "_k8sc_lryl";
                List<object> cache = new List<object>();
                if (CacheHelper.GetCache("balance_kc_FileCacheKey" + str2) != null)
                {
                    cache = CacheHelper.GetCache("balance_kc_FileCacheKey" + str2) as List<object>;
                }
                else
                {
                    List<string> collection = new List<string>();
                    DataSet topPhase = CallBLL.cz_phase_k8sc_bll.GetTopPhase(5);
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
                            string str13 = "";
                            string str14 = "";
                            str6 = row["phase"].ToString();
                            str7 = row["play_open_date"].ToString();
                            int num = Convert.ToInt32(row["n1"].ToString());
                            int num2 = Convert.ToInt32(row["n2"].ToString());
                            int num3 = Convert.ToInt32(row["n3"].ToString());
                            int num4 = Convert.ToInt32(row["n4"].ToString());
                            int num5 = Convert.ToInt32(row["n5"].ToString());
                            collection.Add(num.ToString());
                            collection.Add(num2.ToString());
                            collection.Add(num3.ToString());
                            collection.Add(num4.ToString());
                            collection.Add(num5.ToString());
                            str8 = ((((num + num2) + num3) + num4) + num5).ToString();
                            str9 = K8SCPhase.get_cl_zhdx(str8);
                            str9 = base.SetWordColor(str9.Replace("總和", ""));
                            str10 = K8SCPhase.get_cl_zhds(str8);
                            str10 = base.SetWordColor(str10.Replace("總和", ""));
                            str11 = K8SCPhase.get_cl_lh(num.ToString(), num5.ToString());
                            str11 = base.SetWordColor(str11);
                            str12 = K8SCPhase.get_k8sc_str(string.Concat(new object[] { num, ",", num2, ",", num3 }));
                            str13 = K8SCPhase.get_k8sc_str(string.Concat(new object[] { num2, ",", num3, ",", num4 }));
                            str14 = K8SCPhase.get_k8sc_str(string.Concat(new object[] { num3, ",", num4, ",", num5 }));
                            dictionary2.Add("phase", str6);
                            dictionary2.Add("play_open_date", str7);
                            dictionary2.Add("draw_num", new List<string>(collection));
                            dictionary2.Add("total", new List<string> { str8, str9, str10, str11, str12, str13, str14 });
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
                    DataTable table2 = CallBLL.cz_phase_k8sc_bll.GetChangLong().Tables[0];
                    if (table2.Rows.Count > 0)
                    {
                        dictionary2 = new Dictionary<string, object>();
                        foreach (DataRow row in table2.Rows)
                        {
                            string str15 = row["c_name"].ToString();
                            string str16 = row["c_qs"].ToString();
                            dictionary2.Add("cl_name", str15);
                            dictionary2.Add("cl_num", str16);
                            list4.Add(new Dictionary<string, object>(dictionary2));
                            dictionary2.Clear();
                        }
                    }
                    CacheHelper.SetCache("balance_kc_FileCacheKey" + str3, list4);
                    CacheHelper.SetPublicFileCache("balance_kc_FileCacheKey" + str3, list4, PageBase.GetPublicForderPath(base.get_KC_BalanceFileName()));
                }
                dictionary.Add("lmcl", list4);
                if (str.Equals("k8sc_lmp") || str.Equals("k8sc_d1_5"))
                {
                    dictionary3 = new Dictionary<string, object>();
                    if (CacheHelper.GetCache("balance_kc_FileCacheKey" + str4 + str) != null)
                    {
                        dictionary3 = CacheHelper.GetCache("balance_kc_FileCacheKey" + str4 + str) as Dictionary<string, object>;
                    }
                    else
                    {
                        list5 = CallBLL.cz_phase_k8sc_bll.PaihangList_LMP(str, 0x19);
                        list6 = new List<string>();
                        for (num6 = 1; num6 <= 5; num6++)
                        {
                            list6.Add(string.Format("第{0}球大小", num6));
                            list6.Add(string.Format("第{0}球單雙", num6));
                        }
                        list6.Add("總和大小");
                        list6.Add("總和單雙");
                        list6.Add("龍虎");
                        dictionary3.Add("title", list6);
                        dictionary3.Add("content", list5);
                        CacheHelper.SetCache("balance_kc_FileCacheKey" + str4 + str, dictionary3);
                        CacheHelper.SetPublicFileCache("balance_kc_FileCacheKey" + str4 + str, dictionary3, PageBase.GetPublicForderPath(base.get_KC_BalanceFileName()));
                    }
                    dictionary.Add("cql", dictionary3);
                }
                else
                {
                    dictionary3 = new Dictionary<string, object>();
                    if (CacheHelper.GetCache("balance_kc_FileCacheKey" + str4 + str) != null)
                    {
                        dictionary3 = CacheHelper.GetCache("balance_kc_FileCacheKey" + str4 + str) as Dictionary<string, object>;
                    }
                    else
                    {
                        str17 = "1";
                        str18 = str;
                        if (str18 != null)
                        {
                            if (!(str18 == "k8sc_d1"))
                            {
                                if (str18 == "k8sc_d2")
                                {
                                    str17 = "2";
                                }
                                else if (str18 == "k8sc_d3")
                                {
                                    str17 = "3";
                                }
                                else if (str18 == "k8sc_d4")
                                {
                                    str17 = "4";
                                }
                                else if (str18 == "k8sc_d5")
                                {
                                    str17 = "5";
                                }
                            }
                            else
                            {
                                str17 = "1";
                            }
                        }
                        list5 = CallBLL.cz_phase_k8sc_bll.PaihangListDQ(str17, 0x19);
                        list6 = new List<string> {
                            string.Format("第{0}球", str17),
                            "大小",
                            "單雙",
                            "總和大小",
                            "總和單雙",
                            "龍虎"
                        };
                        dictionary3.Add("title", list6);
                        dictionary3.Add("content", list5);
                        CacheHelper.SetCache("balance_kc_FileCacheKey" + str4 + str, dictionary3);
                        CacheHelper.SetPublicFileCache("balance_kc_FileCacheKey" + str4 + str, dictionary3, PageBase.GetPublicForderPath(base.get_KC_BalanceFileName()));
                    }
                    dictionary.Add("cql", dictionary3);
                }
                if (!str.Equals("k8sc_lmp") && !str.Equals("k8sc_d1_5"))
                {
                    str17 = "1";
                    str18 = str;
                    if (str18 != null)
                    {
                        if (!(str18 == "k8sc_d1"))
                        {
                            if (str18 == "k8sc_d2")
                            {
                                str17 = "2";
                            }
                            else if (str18 == "k8sc_d3")
                            {
                                str17 = "3";
                            }
                            else if (str18 == "k8sc_d4")
                            {
                                str17 = "4";
                            }
                            else if (str18 == "k8sc_d5")
                            {
                                str17 = "5";
                            }
                        }
                        else
                        {
                            str17 = "1";
                        }
                    }
                    Dictionary<string, object> dictionary4 = new Dictionary<string, object>();
                    DataSet lRYL = CallBLL.cz_phase_k8sc_bll.GetLRYL(str17);
                    if (CacheHelper.GetCache("balance_kc_FileCacheKey" + str5 + str) != null)
                    {
                        dictionary4 = CacheHelper.GetCache("balance_kc_FileCacheKey" + str5 + str) as Dictionary<string, object>;
                    }
                    else
                    {
                        DataTable table3 = null;
                        if (lRYL != null)
                        {
                            table3 = lRYL.Tables[0];
                            ArrayList list7 = new ArrayList();
                            if ((table3 != null) && (table3.Rows.Count > 0))
                            {
                                ArrayList list8 = new ArrayList();
                                for (num6 = 0; num6 < table3.Columns.Count; num6++)
                                {
                                    list8.Add(table3.Columns[num6].ColumnName);
                                }
                                dictionary4.Add("num", list8);
                                dictionary4.Add("lr", table3.Rows[0].ItemArray);
                                dictionary4.Add("yl", table3.Rows[1].ItemArray);
                            }
                        }
                        CacheHelper.SetCache("balance_kc_FileCacheKey" + str5 + str, dictionary4);
                        CacheHelper.SetPublicFileCache("balance_kc_FileCacheKey" + str5 + str, dictionary4, PageBase.GetPublicForderPath(base.get_KC_BalanceFileName()));
                    }
                    dictionary.Add("lryl", dictionary4);
                }
                result.set_data(dictionary);
                strResult = JsonHandle.ObjectToJson(result);
            }
        }

        public override void ProcessRequest(HttpContext context)
        {
            base.checkLogin();
            base.IsLotteryExist(6.ToString());
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
    }
}

