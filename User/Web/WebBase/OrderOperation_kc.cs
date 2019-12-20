namespace User.Web.WebBase
{
    using LotterySystem.Common;
    using LotterySystem.Model;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Web;

    public class OrderOperation_kc : MemberPageBase
    {
        private double get_ball_min_pl(DataTable myDT, string nos, double pl)
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

        private string get_ball_pl(int lid, int playid, double pl, string nos)
        {
            List<string> list = new List<string>();
            DataTable table = null;
            switch (lid)
            {
                case 0:
                    table = CallBLL.cz_wt_kl10_bll.GetWtByPlayID(playid, nos).Tables[0];
                    break;

                case 3:
                    table = CallBLL.cz_wt_xync_bll.GetWtByPlayID(playid, nos).Tables[0];
                    break;

                case 7:
                    table = CallBLL.cz_wt_pcdd_bll.GetWtByPlayID(playid, nos).Tables[0];
                    break;
            }
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

        private DataTable GetDrawbackByLotteryID(int lid, int playid, user_kc_rate kc_rate, string kc_kind)
        {
            DataTable table = null;
            switch (lid)
            {
                case 0:
                    if (CacheHelper.GetCache("kc_drawback_FileCacheKey_kl10" + this.Session["user_name"].ToString()) != null)
                    {
                        return base.GetUserDrawback_kl10(kc_rate, kc_kind);
                    }
                    return CallBLL.cz_drawback_kl10_bll.GetDrawback(playid.ToString(), this.Session["user_name"].ToString()).Tables[0];

                case 1:
                    if (CacheHelper.GetCache("kc_drawback_FileCacheKey_cqsc" + this.Session["user_name"].ToString()) != null)
                    {
                        return base.GetUserDrawback_cqsc(kc_rate, kc_kind);
                    }
                    return CallBLL.cz_drawback_cqsc_bll.GetDrawback(playid.ToString(), this.Session["user_name"].ToString()).Tables[0];

                case 2:
                    if (CacheHelper.GetCache("kc_drawback_FileCacheKey_pk10" + this.Session["user_name"].ToString()) != null)
                    {
                        return base.GetUserDrawback_pk10(kc_rate, kc_kind);
                    }
                    return CallBLL.cz_drawback_pk10_bll.GetDrawback(playid.ToString(), this.Session["user_name"].ToString()).Tables[0];

                case 3:
                    if (CacheHelper.GetCache("kc_drawback_FileCacheKey_xync" + this.Session["user_name"].ToString()) != null)
                    {
                        return base.GetUserDrawback_xync(kc_rate, kc_kind);
                    }
                    return CallBLL.cz_drawback_xync_bll.GetDrawback(playid.ToString(), this.Session["user_name"].ToString()).Tables[0];

                case 4:
                    if (CacheHelper.GetCache("kc_drawback_FileCacheKey_jsk3" + this.Session["user_name"].ToString()) != null)
                    {
                        return base.GetUserDrawback_jsk3(kc_rate, kc_kind);
                    }
                    return CallBLL.cz_drawback_jsk3_bll.GetDrawback(playid.ToString(), this.Session["user_name"].ToString()).Tables[0];

                case 5:
                    if (CacheHelper.GetCache("kc_drawback_FileCacheKey_kl8" + this.Session["user_name"].ToString()) != null)
                    {
                        return base.GetUserDrawback_kl8(kc_rate, kc_kind);
                    }
                    return CallBLL.cz_drawback_kl8_bll.GetDrawback(playid.ToString(), this.Session["user_name"].ToString()).Tables[0];

                case 6:
                    if (CacheHelper.GetCache("kc_drawback_FileCacheKey_k8sc" + this.Session["user_name"].ToString()) != null)
                    {
                        return base.GetUserDrawback_k8sc(kc_rate, kc_kind);
                    }
                    return CallBLL.cz_drawback_k8sc_bll.GetDrawback(playid.ToString(), this.Session["user_name"].ToString()).Tables[0];

                case 7:
                    if (CacheHelper.GetCache("kc_drawback_FileCacheKey_pcdd" + this.Session["user_name"].ToString()) != null)
                    {
                        return base.GetUserDrawback_pcdd(kc_rate, kc_kind);
                    }
                    return CallBLL.cz_drawback_pcdd_bll.GetDrawback(playid.ToString(), this.Session["user_name"].ToString()).Tables[0];

                case 8:
                    return table;

                case 9:
                    if (CacheHelper.GetCache("kc_drawback_FileCacheKey_xyft5" + this.Session["user_name"].ToString()) != null)
                    {
                        return base.GetUserDrawback_xyft5(kc_rate, kc_kind);
                    }
                    return CallBLL.cz_drawback_xyft5_bll.GetDrawback(playid.ToString(), this.Session["user_name"].ToString()).Tables[0];
            }
            return table;
        }

        private List<string> GetLmList(int lid)
        {
            List<string> list = null;
            if (lid.Equals(0))
            {
                return "329,330,331,1181,1200,1201,1202,1203".Split(new char[] { ',' }).ToList<string>();
            }
            if (lid.Equals(3))
            {
                return "329,330,331,1181,1202,1203".Split(new char[] { ',' }).ToList<string>();
            }
            if (lid.Equals(7))
            {
                list = "72055".Split(new char[] { ',' }).ToList<string>();
            }
            return list;
        }

        private string GetLotteryStringFlag(int lid)
        {
            string str = "";
            switch (lid)
            {
                case 0:
                    return "kl10";

                case 1:
                    return "cqsc";

                case 2:
                    return "pk10";

                case 3:
                    return "xync";

                case 4:
                    return "jsk3";

                case 5:
                    return "kl8";

                case 6:
                    return "k8sc";

                case 7:
                    return "pcdd";

                case 8:
                    return str;

                case 9:
                    return "xyft";
            }
            return str;
        }

        private DataSet GetOddsByLotteryID(int lid, string odds_ids)
        {
            DataSet set = null;
            switch (lid)
            {
                case 0:
                    return CallBLL.cz_odds_kl10_bll.GetOddsByID(odds_ids);

                case 1:
                    return CallBLL.cz_odds_cqsc_bll.GetOddsByID(odds_ids);

                case 2:
                    return CallBLL.cz_odds_pk10_bll.GetOddsByID(odds_ids);

                case 3:
                    return CallBLL.cz_odds_xync_bll.GetOddsByID(odds_ids);

                case 4:
                    return CallBLL.cz_odds_jsk3_bll.GetOddsByID(odds_ids);

                case 5:
                    return CallBLL.cz_odds_kl8_bll.GetOddsByID(odds_ids);

                case 6:
                    return CallBLL.cz_odds_k8sc_bll.GetOddsByID(odds_ids);

                case 7:
                    return CallBLL.cz_odds_pcdd_bll.GetOddsByID(odds_ids);

                case 8:
                    return set;

                case 9:
                    return CallBLL.cz_odds_xyft5_bll.GetOddsByID(odds_ids);
            }
            return set;
        }

        private DataSet GetPhaseByLotteryID(int lid, int phaseID)
        {
            DataSet set = null;
            switch (lid)
            {
                case 0:
                    return CallBLL.cz_phase_kl10_bll.GetIsClosedByTime(phaseID);

                case 1:
                    return CallBLL.cz_phase_cqsc_bll.GetIsClosedByTime(phaseID);

                case 2:
                    return CallBLL.cz_phase_pk10_bll.GetIsClosedByTime(phaseID);

                case 3:
                    return CallBLL.cz_phase_xync_bll.GetIsClosedByTime(phaseID);

                case 4:
                    return CallBLL.cz_phase_jsk3_bll.GetIsClosedByTime(phaseID);

                case 5:
                    return CallBLL.cz_phase_kl8_bll.GetIsClosedByTime(phaseID);

                case 6:
                    return CallBLL.cz_phase_k8sc_bll.GetIsClosedByTime(phaseID);

                case 7:
                    return CallBLL.cz_phase_pcdd_bll.GetIsClosedByTime(phaseID);

                case 8:
                    return set;

                case 9:
                    return CallBLL.cz_phase_xyft5_bll.GetIsClosedByTime(phaseID);
            }
            return set;
        }

        private DataSet GetPlayOddsByLotteryID(int lid, string odds_ids)
        {
            DataSet set = null;
            switch (lid)
            {
                case 0:
                    return CallBLL.cz_odds_kl10_bll.GetPlayOddsByID(odds_ids);

                case 1:
                    return CallBLL.cz_odds_cqsc_bll.GetPlayOddsByID(odds_ids);

                case 2:
                    return CallBLL.cz_odds_pk10_bll.GetPlayOddsByID(odds_ids);

                case 3:
                    return CallBLL.cz_odds_xync_bll.GetPlayOddsByID(odds_ids);

                case 4:
                    return CallBLL.cz_odds_jsk3_bll.GetPlayOddsByID(odds_ids);

                case 5:
                    return CallBLL.cz_odds_kl8_bll.GetPlayOddsByID(odds_ids);

                case 6:
                    return CallBLL.cz_odds_k8sc_bll.GetPlayOddsByID(odds_ids);

                case 7:
                    return CallBLL.cz_odds_pcdd_bll.GetPlayOddsByID(odds_ids);

                case 8:
                    return set;

                case 9:
                    return CallBLL.cz_odds_xyft5_bll.GetPlayOddsByID(odds_ids);
            }
            return set;
        }

        public void OrderOpt(int lid, ref string strResult)
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
            double num22;
            DataTable table9;
            string str47;
            string str48;
            DataRow[] rowArray2;
            string str50;
            string str51;
            cz_bet_kc _kc;
            int num26;
            double num33;
            double num34;
            double num35;
            double num36;
            double num37;
            int num39;
            cz_system_log _log;
            string str54;
            cz_jp_odds _odds;
            DataTable fgsWTTable;
            string str55;
            double num42;
            int num43;
            base.Response.End();
            HttpContext current = HttpContext.Current;
            if (current.Session["user_name"] == null)
            {
                current.Response.End();
                return;
            }
            string str = current.Session["user_name"].ToString();
            ReturnResult result = new ReturnResult();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("type", "put_money");
            cz_userinfo_session rateByUserObject = current.Session[current.Session["user_name"] + "lottery_session_user_info"] as cz_userinfo_session;
            base.CheckIsOut(rateByUserObject.get_u_name());
            base.stat_online_redis(rateByUserObject.get_u_name(), "hy");
            user_kc_rate rate = base.GetUserRate_kc(rateByUserObject.get_zjname());
            DataTable table = null;
            DataSet set = CallBLL.cz_users_bll.AccountIsDisabled(rate.get_fgsname(), rate.get_gdname(), rate.get_zdname(), rate.get_dlname(), rateByUserObject.get_u_name());
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
            string str6 = "";
            string str7 = "";
            string str8 = "";
            int num = lid;
            s = LSRequest.qq("phaseid");
            str4 = LSRequest.qq("playpage");
            str6 = LSRequest.qq("oddsid");
            str7 = LSRequest.qq("uPI_P");
            str8 = LSRequest.qq("uPI_M");
            string str9 = LSRequest.qq("uPI_TM");
            string[] source = str6.Split(new char[] { ',' });
            if (source.Distinct<string>().ToList<string>().Count != source.Length)
            {
                current.Response.End();
            }
            if (lid.Equals(0) && (source.Length > FileCacheHelper.get_GetKL10MaxGroup()))
            {
                result.set_success(400);
                result.set_tipinfo(string.Format("您選擇的號碼超過了最大{0}組，請重新選擇號碼!", FileCacheHelper.get_GetKL10MaxGroup()));
                dictionary.Add("JeuValidate", base.get_JeuValidate());
                result.set_data(dictionary);
                strResult = JsonHandle.ObjectToJson(result);
                return;
            }
            if (lid.Equals(3) && (source.Length > FileCacheHelper.get_GetXYNCMaxGroup()))
            {
                result.set_success(400);
                result.set_tipinfo(string.Format("您選擇的號碼超過了最大{0}組，請重新選擇號碼!", FileCacheHelper.get_GetXYNCMaxGroup()));
                dictionary.Add("JeuValidate", base.get_JeuValidate());
                result.set_data(dictionary);
                strResult = JsonHandle.ObjectToJson(result);
                return;
            }
            if (lid.Equals(7) && (source.Length > FileCacheHelper.get_GetPCDDMaxGroup()))
            {
                result.set_success(400);
                result.set_tipinfo(string.Format("您選擇的號碼超過了最大{0}組，請重新選擇號碼!", FileCacheHelper.get_GetPCDDMaxGroup()));
                dictionary.Add("JeuValidate", base.get_JeuValidate());
                result.set_data(dictionary);
                strResult = JsonHandle.ObjectToJson(result);
                return;
            }
            string[] strArray2 = LSRequest.qq("i_index").Split(new char[] { ',' });
            string str10 = LSRequest.qq("JeuValidate");
            if (((current.Session["JeuValidate"] == null) || (current.Session["JeuValidate"].ToString().Trim() != str10)) || (current.Session["JeuValidate"].ToString().Trim() == ""))
            {
                result.set_tipinfo("下注規則有誤,請重新下注,謝謝合作!");
                result.set_success(400);
                dictionary.Add("JeuValidate", base.get_JeuValidate());
                result.set_data(dictionary);
                strResult = JsonHandle.ObjectToJson(result);
                return;
            }
            current.Session["JeuValidate"] = "";
            int num3 = 1;
            if (rateByUserObject.get_su_type().ToString().Trim() != "dl")
            {
                num3 = -1;
            }
            int index = 0;
        Label_0589:;
            if (index < str8.Split(new char[] { ',' }).Length)
            {
                if (!base.IsNumber(str8.Split(new char[] { ',' })[index]))
                {
                    result.set_tipinfo("下注金額有誤！");
                    result.set_success(500);
                    dictionary.Add("index", strArray2[index]);
                    dictionary.Add("JeuValidate", base.get_JeuValidate());
                    result.set_data(dictionary);
                    strResult = JsonHandle.ObjectToJson(result);
                    return;
                }
                index++;
                goto Label_0589;
            }
            if (rateByUserObject.get_begin_kc().Trim() != "yes")
            {
                rateByUserObject = base.GetRateByUserObject(2, rate);
            }
            string str12 = "";
            string str13 = "";
            DataTable table2 = this.GetPhaseByLotteryID(lid, int.Parse(s)).Tables[0];
            if ((table2 != null) && (table2.Rows.Count > 0))
            {
                if (table2.Rows[0]["is_closed"].ToString().Trim() == "1")
                {
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
                string str14 = base.BetReceiveEnd(lid.ToString());
                if (!string.IsNullOrEmpty(str14))
                {
                    time = Convert.ToDateTime(table2.Rows[0]["play_open_date"].ToString()).AddSeconds((double) -int.Parse(str14));
                }
            }
            else
            {
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
            rateByUserObject.set_kc_kind(str15.ToUpper());
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
            else if (rateByUserObject.get_u_type() == "fgs")
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
            string[] strArray3 = str6.Split(new char[] { ',' });
            string[] strArray4 = str7.Split(new char[] { ',' });
            string[] strArray5 = str8.Split(new char[] { ',' });
            DataTable plDT = this.GetPlayOddsByLotteryID(lid, str6).Tables[0];
            if (plDT.Rows.Count == 0)
            {
                result.set_success(400);
                result.set_tipinfo("賠率ID設置错误!");
                dictionary.Add("JeuValidate", base.get_JeuValidate());
                result.set_data(dictionary);
                strResult = JsonHandle.ObjectToJson(result);
                return;
            }
            DataTable table5 = CallBLL.cz_bet_kc_bll.GetSinglePhaseByBet(str12, str, lid.ToString(), str6, null).Tables[0];
            if ((plDT.Rows.Count == 0) || (plDT.Rows.Count != strArray3.Count<string>()))
            {
                result.set_success(400);
                result.set_tipinfo("系统错误!");
                dictionary.Add("JeuValidate", base.get_JeuValidate());
                result.set_data(dictionary);
                strResult = JsonHandle.ObjectToJson(result);
                return;
            }
            List<string> lmList = this.GetLmList(lid);
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
                DataRow[] rowArray = this.GetDrawbackByLotteryID(lid, int.Parse(str23), rate, rateByUserObject.get_kc_kind()).Select(string.Format(" play_id={0} and u_name='{1}' ", str23, str));
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
                    result.set_tipinfo(string.Format("{0}<{1}>已經停止投注！", str26, str27));
                    result.set_success(400);
                    dictionary.Add("JeuValidate", base.get_JeuValidate());
                    result.set_data(dictionary);
                    strResult = JsonHandle.ObjectToJson(result);
                    return;
                }
                num15 = double.Parse(str24) + double.Parse(str25);
                string pl = num15.ToString();
                base.GetOdds_KC(lid, str29, ref pl);
                num15 = Convert.ToDouble(pl);
                if (!lmList.Contains(str29))
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
                if (!lmList.Contains(str29) && (double.Parse(strArray5[num16]) > num10))
                {
                    result.set_tipinfo(string.Format("下注金額超出單注最大金額！", new object[0]));
                    result.set_success(500);
                    dictionary.Add("JeuValidate", base.get_JeuValidate());
                    result.set_data(dictionary);
                    strResult = JsonHandle.ObjectToJson(result);
                    return;
                }
                if (double.Parse(strArray5[num16]) > num5)
                {
                    result.set_tipinfo(string.Format("下注金額超出可用金額！", new object[0]));
                    result.set_success(500);
                    dictionary.Add("JeuValidate", base.get_JeuValidate());
                    result.set_data(dictionary);
                    strResult = JsonHandle.ObjectToJson(result);
                    return;
                }
                if (double.Parse(strArray5[num16]) < num14)
                {
                    result.set_tipinfo(string.Format("下注金額低過最低金額！", new object[0]));
                    result.set_success(500);
                    dictionary.Add("JeuValidate", base.get_JeuValidate());
                    result.set_data(dictionary);
                    strResult = JsonHandle.ObjectToJson(result);
                    return;
                }
                if (lmList.Contains(str29))
                {
                    if (double.Parse(str8) > num12)
                    {
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
                result.set_tipinfo(string.Format("可用餘額不足！", new object[0]));
                result.set_success(500);
                dictionary.Add("JeuValidate", base.get_JeuValidate());
                result.set_data(dictionary);
                strResult = JsonHandle.ObjectToJson(result);
                return;
            }
            string str32 = "";
            string str33 = "";
            if (!lmList.Contains(str6))
            {
                bool flag2 = false;
                List<string> successBetList = new List<string>();
                num16 = 0;
                nullable = null;
                foreach (string str29 in strArray3)
                {
                    table9 = CallBLL.cz_odds_kl10_bll.GetOddsByID(str29).Tables[0];
                    int num40 = int.Parse(table9.Rows[0]["play_id"].ToString());
                    str47 = table9.Rows[0]["play_name"].ToString();
                    str48 = table9.Rows[0]["put_amount"].ToString();
                    dataTable = base.GetUserDrawback_kl10(rate, rateByUserObject.get_kc_kind());
                    if (dataTable == null)
                    {
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
                        str55 = row["u_type"].ToString().Trim();
                        if (str55 != null)
                        {
                            if (!(str55 == "zj"))
                            {
                                if (str55 == "fgs")
                                {
                                    goto Label_31AC;
                                }
                                if (str55 == "gd")
                                {
                                    goto Label_31D0;
                                }
                                if (str55 == "zd")
                                {
                                    goto Label_31F4;
                                }
                                if (str55 == "dl")
                                {
                                    goto Label_3218;
                                }
                                if (str55 == "hy")
                                {
                                    goto Label_323C;
                                }
                            }
                            else
                            {
                                str17 = row[str15 + "_drawback"].ToString().Trim();
                            }
                        }
                        continue;
                    Label_31AC:
                        str18 = row[str15 + "_drawback"].ToString().Trim();
                        continue;
                    Label_31D0:
                        str19 = row[str15 + "_drawback"].ToString().Trim();
                        continue;
                    Label_31F4:
                        str20 = row[str15 + "_drawback"].ToString().Trim();
                        continue;
                    Label_3218:
                        str21 = row[str15 + "_drawback"].ToString().Trim();
                        continue;
                    Label_323C:
                        if (rateByUserObject.get_su_type() == "dl")
                        {
                            str22 = row[str15 + "_drawback"].ToString().Trim();
                        }
                        else if (rateByUserObject.get_su_type() == "zd")
                        {
                            str21 = row[str15 + "_drawback"].ToString().Trim();
                            str22 = row[str15 + "_drawback"].ToString().Trim();
                        }
                        else if (rateByUserObject.get_su_type() == "gd")
                        {
                            str20 = row[str15 + "_drawback"].ToString().Trim();
                            str22 = row[str15 + "_drawback"].ToString().Trim();
                        }
                        else if (rateByUserObject.get_su_type() == "fgs")
                        {
                            str19 = row[str15 + "_drawback"].ToString().Trim();
                            str22 = row[str15 + "_drawback"].ToString().Trim();
                        }
                    }
                    if (DateTime.Now >= time.AddSeconds(0.0))
                    {
                        flag2 = true;
                        break;
                    }
                    num42 = double.Parse(table9.Rows[0]["current_odds"].ToString()) + Convert.ToDouble(table9.Rows[0][str15 + "_diff"].ToString().Trim());
                    str50 = num42.ToString();
                    str51 = str50.ToString();
                    base.GetOdds_KC(0, str29, ref str51);
                    if (!nullable.HasValue)
                    {
                        nullable = new DateTime?(DateTime.Now);
                    }
                    num43 = 0;
                    base.Add_Bet_Lock(num43.ToString(), rate.get_fgsname(), str29);
                    _kc = new cz_bet_kc();
                    _kc.set_order_num(Utils.GetOrderNumber());
                    _kc.set_checkcode(str10);
                    _kc.set_u_name(rateByUserObject.get_u_name());
                    _kc.set_u_nicker(rateByUserObject.get_u_nicker());
                    _kc.set_phase_id(new int?(int.Parse(str13)));
                    _kc.set_phase(str12);
                    _kc.set_bet_time(nullable);
                    _kc.set_odds_id(new int?(int.Parse(str29)));
                    _kc.set_category(table9.Rows[0]["category"].ToString());
                    _kc.set_play_id(new int?(int.Parse(table9.Rows[0]["play_id"].ToString())));
                    _kc.set_play_name(str47);
                    _kc.set_bet_val(str48);
                    _kc.set_odds(str51);
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
                    _kc.set_odds_zj(str50);
                    num26 = 0;
                    if (!(CallBLL.cz_bet_kc_bll.AddBet(_kc, decimal.Parse(strArray5[num16]), str, ref num26) && (num26 > 0)))
                    {
                        result.set_tipinfo(string.Format("系統錯誤！", new object[0]));
                        result.set_success(400);
                        dictionary.Add("JeuValidate", base.get_JeuValidate());
                        result.set_data(dictionary);
                        strResult = JsonHandle.ObjectToJson(result);
                        return;
                    }
                    successBetList.Add(string.Concat(new object[] { _kc.get_odds_id(), ",", _kc.get_order_num(), ",", _kc.get_play_name(), ",", _kc.get_bet_val(), ",", _kc.get_odds(), ",", _kc.get_amount() }));
                    num33 = (double.Parse(strArray5[num16]) * num6) / 100.0;
                    CallBLL.cz_odds_kl10_bll.UpdateGrandTotal(Convert.ToDecimal(num33), int.Parse(str29));
                    num34 = double.Parse(table9.Rows[0]["grand_total"].ToString()) + num33;
                    num35 = double.Parse(table9.Rows[0]["downbase"].ToString());
                    num36 = Math.Floor((double) (num34 / num35));
                    if (num36 >= 1.0)
                    {
                        num37 = double.Parse(table9.Rows[0]["down_odds_rate"].ToString()) * num36;
                        double num38 = num34 - (num35 * num36);
                        num39 = CallBLL.cz_odds_kl10_bll.UpdateGrandTotalCurrentOdds(num37.ToString(), num38.ToString(), str29);
                        _log = new cz_system_log();
                        _log.set_user_name("系統");
                        _log.set_children_name("");
                        _log.set_category(table9.Rows[0]["category"].ToString());
                        _log.set_play_name(str47);
                        _log.set_put_amount(str48);
                        num43 = 0;
                        _log.set_l_name(base.GetGameNameByID(num43.ToString()));
                        _log.set_l_phase(str12);
                        _log.set_action("降賠率");
                        _log.set_odds_id(int.Parse(str29));
                        str54 = table9.Rows[0]["current_odds"].ToString();
                        _log.set_old_val(str54);
                        num42 = double.Parse(str54) - num37;
                        _log.set_new_val(num42.ToString());
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
                        _odds.set_odds(num37.ToString());
                        _odds.set_lottery_type(0);
                        _odds.set_phase(str12);
                        _odds.set_old_odds(str54);
                        num42 = double.Parse(str54) - num37;
                        _odds.set_new_odds(num42.ToString());
                        CallBLL.cz_jp_odds_bll.Add(_odds);
                    }
                    fgsWTTable = null;
                    if (rateByUserObject.get_kc_op_odds().Equals(1))
                    {
                        fgsWTTable = base.GetFgsWTTable(0);
                    }
                    CallBLL.cz_autosale_kl10_bll.DLAutoSale(_kc.get_order_num(), str10, rateByUserObject.get_u_nicker(), rateByUserObject.get_u_name(), str15, table9.Rows[0]["play_id"].ToString(), str29.Trim(), str17, str18, str19, str20, str21, str22, str13, str12, _kc.get_ip(), num, _kc.get_lottery_name(), rateByUserObject, rate, 0, fgsWTTable);
                    if (rateByUserObject.get_kc_op_odds().Equals(1))
                    {
                        fgsWTTable = base.GetFgsWTTable(0);
                    }
                    CallBLL.cz_autosale_kl10_bll.ZDAutoSale(_kc.get_order_num(), str10, rateByUserObject.get_u_nicker(), rateByUserObject.get_u_name(), str15, table9.Rows[0]["play_id"].ToString(), str29.Trim(), str17, str18, str19, str20, str21, str22, str13, str12, _kc.get_ip(), num, _kc.get_lottery_name(), rateByUserObject, rate, 0, fgsWTTable);
                    if (rateByUserObject.get_kc_op_odds().Equals(1))
                    {
                        fgsWTTable = base.GetFgsWTTable(0);
                    }
                    CallBLL.cz_autosale_kl10_bll.GDAutoSale(_kc.get_order_num(), str10, rateByUserObject.get_u_nicker(), rateByUserObject.get_u_name(), str15, table9.Rows[0]["play_id"].ToString(), str29.Trim(), str17, str18, str19, str20, str21, str22, str13, str12, _kc.get_ip(), num, _kc.get_lottery_name(), rateByUserObject, rate, 0, fgsWTTable);
                    if (rateByUserObject.get_kc_op_odds().Equals(1))
                    {
                        fgsWTTable = base.GetFgsWTTable(0);
                    }
                    CallBLL.cz_autosale_kl10_bll.FGSAutoSale(_kc.get_order_num(), str10, rateByUserObject.get_u_nicker(), rateByUserObject.get_u_name(), str15, table9.Rows[0]["play_id"].ToString(), str29.Trim(), str17, str18, str19, str20, str21, str22, str13, str12, _kc.get_ip(), num, _kc.get_lottery_name(), rateByUserObject, rate, 0, fgsWTTable);
                    num43 = 0;
                    base.Un_Bet_Lock(num43.ToString(), rate.get_fgsname(), str29);
                    num16++;
                }
                base.SetUserRate_kc(rate);
                base.SetUserDrawback_kl10(dataTable);
                rateByUserObject.set_begin_kc("yes");
                if (flag2)
                {
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
            if ((((str6 != "330") && (str6 != "1200")) && (str6 != "72055")) && !base.checkVal(lid, ref str35))
            {
                result.set_tipinfo(string.Format("[{0}]你選擇的號碼不能構成有效注單，請重新選擇!", str26));
                result.set_success(400);
                dictionary.Add("JeuValidate", base.get_JeuValidate());
                result.set_data(dictionary);
                strResult = JsonHandle.ObjectToJson(result);
                return;
            }
            int length = 0;
            str55 = str34;
            if ((str55 == null) || !(str55 == "0"))
            {
                goto Label_1AAD;
            }
            if (!(str6 == "330"))
            {
                string str41;
                string str42;
                switch (str6)
                {
                    case "329":
                    case "331":
                        str32 = base.getfz(str35, 2);
                        str33 = this.get_ball_pl(lid, int.Parse(str23), double.Parse(str7), str35);
                        goto Label_19DA;

                    default:
                        switch (str6)
                        {
                            case "1181":
                            case "1201":
                                str32 = base.getfz(str35, 3);
                                str33 = this.get_ball_pl(lid, int.Parse(str23), double.Parse(str7), str35);
                                goto Label_19DA;
                        }
                        if (!(str6 == "1200"))
                        {
                            if (str6 == "1202")
                            {
                                str32 = base.getfz(str35, 4);
                                str33 = this.get_ball_pl(lid, int.Parse(str23), double.Parse(str7), str35);
                            }
                            else if (str6 == "1203")
                            {
                                str32 = base.getfz(str35, 5);
                                str33 = this.get_ball_pl(lid, int.Parse(str23), double.Parse(str7), str35);
                            }
                            else if (str6 == "72055")
                            {
                                str32 = base.getfz(str35, 3);
                                str33 = this.get_ball_pl(lid, int.Parse(str23), double.Parse(str7), str35);
                            }
                            goto Label_19DA;
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
            Label_174E:;
                if (num21 < str39.Split(new char[] { ',' }).Length)
                {
                    str37 = str37 + str39.Split(new char[] { ',' })[num21] + ",";
                    num21++;
                    goto Label_174E;
                }
                num21 = 0;
            Label_17A6:;
                if (num21 < str40.Split(new char[] { ',' }).Length)
                {
                    str38 = str38 + str40.Split(new char[] { ',' })[num21] + ",";
                    num21++;
                    goto Label_17A6;
                }
                num21 = 0;
            Label_17FE:;
                if (num21 < str42.Split(new char[] { ',' }).Length)
                {
                    str41 = str41 + str42.Split(new char[] { ',' })[num21] + ",";
                    num21++;
                    goto Label_17FE;
                }
                str37 = str37.Substring(0, str37.Length - 1);
                str38 = str38.Substring(0, str38.Length - 1);
                str41 = str41.Substring(0, str41.Length - 1);
                if (!((base.checkVal(lid, ref str37) && base.checkVal(lid, ref str38)) && base.checkVal(lid, ref str41)))
                {
                    result.set_tipinfo(string.Format("[{0}]你選擇的號碼不能構成有效注單，請重新選擇!", str26));
                    result.set_success(400);
                    dictionary.Add("JeuValidate", base.get_JeuValidate());
                    result.set_data(dictionary);
                    strResult = JsonHandle.ObjectToJson(result);
                    return;
                }
                str32 = base.getxsqz(str37, str38, str41);
                str33 = str32;
                string str56 = str36;
                str35 = str56 + str37 + "|" + str38 + "|" + str41;
                goto Label_19DA;
            }
            str36 = "";
            str37 = "";
            str38 = "";
            str39 = base.getlongnum(str35.Split(new char[] { '|' })[0]);
            str40 = base.getlongnum(str35.Split(new char[] { '|' })[1]);
            num21 = 0;
        Label_14A7:;
            if (num21 < str39.Split(new char[] { ',' }).Length)
            {
                str37 = str37 + str39.Split(new char[] { ',' })[num21] + ",";
                num21++;
                goto Label_14A7;
            }
            num21 = 0;
        Label_14FF:;
            if (num21 < str40.Split(new char[] { ',' }).Length)
            {
                str38 = str38 + str40.Split(new char[] { ',' })[num21] + ",";
                num21++;
                goto Label_14FF;
            }
            str37 = str37.Substring(0, str37.Length - 1);
            str38 = str38.Substring(0, str38.Length - 1);
            if (!(base.checkVal(lid, ref str37) && base.checkVal(lid, ref str38)))
            {
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
        Label_19DA:;
            length = str32.Split(new char[] { '~' }).Length;
            if (length == 0)
            {
                result.set_tipinfo(string.Format("[{0}]你選擇的號碼不能構成有效注單，請重新選擇!", str26));
                result.set_success(400);
                dictionary.Add("JeuValidate", base.get_JeuValidate());
                result.set_data(dictionary);
                strResult = JsonHandle.ObjectToJson(result);
                return;
            }
            if (!((length * double.Parse(str8)) == double.Parse(str9)))
            {
                result.set_tipinfo(string.Format("[{0}]你下注金额不能構成有效注單，請确认!", str26));
                result.set_success(500);
                dictionary.Add("JeuValidate", base.get_JeuValidate());
                result.set_data(dictionary);
                strResult = JsonHandle.ObjectToJson(result);
                return;
            }
        Label_1AAD:
            num22 = num12 - double.Parse(str8);
            string str43 = "";
            string str44 = "";
            string[] strArray6 = str32.Split(new char[] { '~' });
            int num23 = 0;
            foreach (string str45 in strArray6)
            {
                if (num23 == 0)
                {
                    if (str6.Equals("330") || str6.Equals("1200"))
                    {
                        str43 = str43 + "'" + str45 + "'";
                        str44 = str44 + str45;
                    }
                    else if (str6.Equals("72055"))
                    {
                        str43 = str43 + "'" + this.px_pcdd(str45) + "'";
                        str44 = str44 + this.px_pcdd(str45);
                    }
                    else
                    {
                        str43 = str43 + "'" + this.px(str45) + "'";
                        str44 = str44 + this.px(str45);
                    }
                }
                else if (str6.Equals("330") || str6.Equals("1200"))
                {
                    str43 = str43 + ",'" + str45 + "'";
                    str44 = str44 + "|" + str45;
                }
                else if (str6.Equals("72055"))
                {
                    str43 = str43 + ",'" + this.px_pcdd(str45) + "'";
                    str44 = str44 + "|" + this.px_pcdd(str45);
                }
                else
                {
                    str43 = str43 + ",'" + this.px(str45) + "'";
                    str44 = str44 + "|" + this.px(str45);
                }
                num23++;
            }
            DataTable table8 = CallBLL.cz_bet_kc_bll.DQLMValid(int.Parse(str6), rateByUserObject.get_u_name(), str44, num22, this.GetLotteryStringFlag(lid), str13);
            if (table8 != null)
            {
                result.set_tipinfo(string.Format("單組【" + table8.Rows[0][0].ToString().Trim() + "】單期下註金額超出單期金額[" + num12.ToString() + "]的限制！", new object[0]));
                result.set_success(400);
                dictionary.Add("JeuValidate", base.get_JeuValidate());
                result.set_data(dictionary);
                strResult = JsonHandle.ObjectToJson(result);
            }
            else
            {
                double num24 = double.Parse(str8) * length;
                if (double.Parse(str8) > num10)
                {
                    result.set_tipinfo(string.Format("{0}下注金額超出單注最大金額！", str26));
                    result.set_success(500);
                    dictionary.Add("JeuValidate", base.get_JeuValidate());
                    result.set_data(dictionary);
                    strResult = JsonHandle.ObjectToJson(result);
                }
                else
                {
                    table9 = this.GetOddsByLotteryID(lid, str6).Tables[0];
                    string str46 = table9.Rows[0]["play_id"].ToString();
                    str47 = table9.Rows[0]["play_name"].ToString();
                    str48 = table9.Rows[0]["put_amount"].ToString();
                    if (!str46.Equals("73") && !str46.Equals("76"))
                    {
                        string str49 = base.GetLmGroup()[str46 + "_" + num];
                        int num25 = int.Parse(str49.Split(new char[] { ',' })[1]);
                        if (str32.Split(new char[] { '~' }).Length > num25)
                        {
                            result.set_tipinfo(string.Format("您選擇的號碼組合超過了最大{0}組，請重新選擇號碼！", num25));
                            result.set_success(400);
                            result.set_data(dictionary);
                            strResult = JsonHandle.ObjectToJson(result);
                            return;
                        }
                    }
                    dataTable = base.GetUserDrawback_kl10(rate, rateByUserObject.get_kc_kind());
                    if (dataTable == null)
                    {
                        result.set_tipinfo(string.Format("系統錯誤！", new object[0]));
                        result.set_success(400);
                        dictionary.Add("JeuValidate", base.get_JeuValidate());
                        result.set_data(dictionary);
                        strResult = JsonHandle.ObjectToJson(result);
                    }
                    else
                    {
                        rowArray2 = dataTable.Select(string.Format(" play_id={0} ", str46));
                        foreach (DataRow row in rowArray2)
                        {
                            str55 = row["u_type"].ToString().Trim();
                            if (str55 != null)
                            {
                                if (!(str55 == "zj"))
                                {
                                    if (str55 == "fgs")
                                    {
                                        goto Label_2078;
                                    }
                                    if (str55 == "gd")
                                    {
                                        goto Label_209C;
                                    }
                                    if (str55 == "zd")
                                    {
                                        goto Label_20C0;
                                    }
                                    if (str55 == "dl")
                                    {
                                        goto Label_20E4;
                                    }
                                    if (str55 == "hy")
                                    {
                                        goto Label_2108;
                                    }
                                }
                                else
                                {
                                    str17 = row[str15 + "_drawback"].ToString().Trim();
                                }
                            }
                            continue;
                        Label_2078:
                            str18 = row[str15 + "_drawback"].ToString().Trim();
                            continue;
                        Label_209C:
                            str19 = row[str15 + "_drawback"].ToString().Trim();
                            continue;
                        Label_20C0:
                            str20 = row[str15 + "_drawback"].ToString().Trim();
                            continue;
                        Label_20E4:
                            str21 = row[str15 + "_drawback"].ToString().Trim();
                            continue;
                        Label_2108:
                            if (rateByUserObject.get_su_type() == "dl")
                            {
                                str22 = row[str15 + "_drawback"].ToString().Trim();
                            }
                            else if (rateByUserObject.get_su_type() == "zd")
                            {
                                str21 = row[str15 + "_drawback"].ToString().Trim();
                                str22 = row[str15 + "_drawback"].ToString().Trim();
                            }
                            else if (rateByUserObject.get_su_type() == "gd")
                            {
                                str20 = row[str15 + "_drawback"].ToString().Trim();
                                str22 = row[str15 + "_drawback"].ToString().Trim();
                            }
                            else if (rateByUserObject.get_su_type() == "fgs")
                            {
                                str19 = row[str15 + "_drawback"].ToString().Trim();
                                str22 = row[str15 + "_drawback"].ToString().Trim();
                            }
                        }
                        num42 = double.Parse(table9.Rows[0]["current_odds"].ToString()) + Convert.ToDouble(table9.Rows[0][str15 + "_diff"].ToString().Trim());
                        str50 = num42.ToString();
                        str51 = str50.ToString();
                        base.GetOdds_KC(0, str6, ref str51);
                        if (!nullable.HasValue)
                        {
                            nullable = new DateTime?(DateTime.Now);
                        }
                        num43 = 0;
                        base.Add_Bet_Lock(num43.ToString(), rate.get_fgsname(), str6);
                        _kc = new cz_bet_kc();
                        _kc.set_order_num(Utils.GetOrderNumber());
                        _kc.set_checkcode(str10);
                        _kc.set_u_name(rateByUserObject.get_u_name());
                        _kc.set_u_nicker(rateByUserObject.get_u_nicker());
                        _kc.set_phase_id(new int?(int.Parse(str13)));
                        _kc.set_phase(str12);
                        _kc.set_bet_time(nullable);
                        _kc.set_odds_id(new int?(int.Parse(str6)));
                        _kc.set_category(table9.Rows[0]["category"].ToString());
                        _kc.set_play_id(new int?(int.Parse(str46)));
                        _kc.set_play_name(str47);
                        _kc.set_bet_val(str35);
                        _kc.set_bet_wt(str33);
                        _kc.set_bet_group(str32);
                        _kc.set_odds(str51);
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
                        _kc.set_odds_zj(str50);
                        _kc.set_ordervalidcode(Utils.GetOrderValidCode(_kc.get_u_name(), _kc.get_order_num(), _kc.get_bet_val(), _kc.get_odds(), _kc.get_kind(), Convert.ToInt32(_kc.get_phase_id()), Convert.ToInt32(_kc.get_odds_id()), Convert.ToDouble(_kc.get_amount())));
                        num26 = 0;
                        if (!(CallBLL.cz_bet_kc_bll.AddBet(_kc, decimal.Parse(num24.ToString()), str, ref num26) && (num26 > 0)))
                        {
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
                            string[] strArray7 = str51.Split(new char[] { ',' });
                            num28 = double.Parse(strArray7[0].ToString());
                            if (strArray7.Length > 1)
                            {
                                num29 = double.Parse(strArray7[1].ToString());
                            }
                            double num30 = 0.0;
                            double num31 = 0.0;
                            string[] strArray8 = str50.Split(new char[] { ',' });
                            num30 = double.Parse(strArray8[0].ToString());
                            if (strArray8.Length > 1)
                            {
                                num31 = double.Parse(strArray8[1].ToString());
                            }
                            string[] strArray9 = str32.Split(new char[] { '~' });
                            string str52 = "";
                            cz_splitgroup_kl10 _kl = new cz_splitgroup_kl10();
                            _kl.set_odds_id(new int?(int.Parse(str6)));
                            _kl.set_item_money(new decimal?((decimal) num27));
                            _kl.set_odds1(new decimal?(Convert.ToDecimal(num28.ToString())));
                            _kl.set_odds2(new decimal?(Convert.ToDecimal(num29.ToString())));
                            _kl.set_bet_id(new int?(num26));
                            _kl.set_odds1_zj(new decimal?(Convert.ToDecimal(num30.ToString())));
                            _kl.set_odds2_zj(new decimal?(Convert.ToDecimal(num31.ToString())));
                            switch (str6)
                            {
                                case "330":
                                case "1200":
                                    foreach (string str53 in strArray9)
                                    {
                                        str52 = str53;
                                        _kl.set_item(str52);
                                        if (CallBLL.cz_splitgroup_kl10_bll.AddSplitGroup(_kl) == 0)
                                        {
                                            result.set_tipinfo(string.Format("系統錯誤！", new object[0]));
                                            result.set_success(400);
                                            dictionary.Add("JeuValidate", base.get_JeuValidate());
                                            result.set_data(dictionary);
                                            strResult = JsonHandle.ObjectToJson(result);
                                            return;
                                        }
                                    }
                                    break;

                                default:
                                    foreach (string str53 in strArray9)
                                    {
                                        str52 = this.px(str53);
                                        _kl.set_item(str52);
                                        if (CallBLL.cz_splitgroup_kl10_bll.AddSplitGroup(_kl) == 0)
                                        {
                                            result.set_tipinfo(string.Format("系統錯誤！", new object[0]));
                                            result.set_success(400);
                                            dictionary.Add("JeuValidate", base.get_JeuValidate());
                                            result.set_data(dictionary);
                                            strResult = JsonHandle.ObjectToJson(result);
                                            return;
                                        }
                                    }
                                    break;
                            }
                            num33 = (double.Parse(num24.ToString()) * num6) / 100.0;
                            CallBLL.cz_odds_kl10_bll.UpdateGrandTotal(Convert.ToDecimal(num33), int.Parse(str6.Trim()));
                            num34 = double.Parse(table9.Rows[0]["grand_total"].ToString()) + num33;
                            num35 = double.Parse(table9.Rows[0]["downbase"].ToString());
                            num36 = Math.Floor((double) (num34 / num35));
                            if (num36 >= 1.0)
                            {
                                num37 = double.Parse(table9.Rows[0]["down_odds_rate"].ToString()) * num36;
                                num39 = CallBLL.cz_odds_kl10_bll.UpdateGrandTotalCurrentOdds(num37.ToString(), (num34 - (num35 * num36)).ToString(), str6);
                                _log = new cz_system_log();
                                _log.set_user_name("系統");
                                _log.set_children_name("");
                                _log.set_category(table9.Rows[0]["category"].ToString());
                                _log.set_play_name(str47);
                                _log.set_put_amount(str48);
                                num43 = 0;
                                _log.set_l_name(base.GetGameNameByID(num43.ToString()));
                                _log.set_l_phase(str12);
                                _log.set_action("降賠率");
                                _log.set_odds_id(int.Parse(str6));
                                str54 = table9.Rows[0]["current_odds"].ToString();
                                _log.set_old_val(str54);
                                num42 = double.Parse(str54) - num37;
                                _log.set_new_val(num42.ToString());
                                _log.set_ip(LSRequest.GetIP());
                                _log.set_add_time(DateTime.Now);
                                _log.set_note("系統自動降賠");
                                _log.set_type_id(Convert.ToInt32((LSEnums.LogTypeID) 0));
                                _log.set_lottery_type(0);
                                CallBLL.cz_system_log_bll.Add(_log);
                                _odds = new cz_jp_odds();
                                _odds.set_add_time(DateTime.Now);
                                _odds.set_odds_id(int.Parse(str6));
                                _odds.set_phase_id(int.Parse(str13));
                                _odds.set_play_name(str47);
                                _odds.set_put_amount(str48);
                                _odds.set_odds(num37.ToString());
                                _odds.set_lottery_type(0);
                                _odds.set_phase(str12);
                                _odds.set_old_odds(str54);
                                _odds.set_new_odds((double.Parse(str54) - num37).ToString());
                                CallBLL.cz_jp_odds_bll.Add(_odds);
                            }
                            fgsWTTable = null;
                            if (rateByUserObject.get_kc_op_odds().Equals(1))
                            {
                                fgsWTTable = base.GetFgsWTTable(0);
                            }
                            CallBLL.cz_autosale_kl10_bll.DLAutoSaleLM(_kc.get_order_num(), str10, int.Parse(table9.Rows[0]["play_id"].ToString()), int.Parse(str6.Trim()), double.Parse(_kc.get_odds()), num, _kc.get_lottery_name(), int.Parse(str13), str12, str17, str18, str19, str20, str21, str22, _kc.get_ip(), rateByUserObject, rate, 1, str15, fgsWTTable);
                            if (rateByUserObject.get_kc_op_odds().Equals(1))
                            {
                                fgsWTTable = base.GetFgsWTTable(0);
                            }
                            CallBLL.cz_autosale_kl10_bll.ZDAutoSaleLM(_kc.get_order_num(), str10, int.Parse(table9.Rows[0]["play_id"].ToString()), int.Parse(str6.Trim()), double.Parse(_kc.get_odds()), num, _kc.get_lottery_name(), int.Parse(str13), str12, str17, str18, str19, str20, str21, str22, _kc.get_ip(), rateByUserObject, rate, 1, str15, fgsWTTable);
                            if (rateByUserObject.get_kc_op_odds().Equals(1))
                            {
                                fgsWTTable = base.GetFgsWTTable(0);
                            }
                            CallBLL.cz_autosale_kl10_bll.GDAutoSaleLM(_kc.get_order_num(), str10, int.Parse(table9.Rows[0]["play_id"].ToString()), int.Parse(str6.Trim()), double.Parse(_kc.get_odds()), num, _kc.get_lottery_name(), int.Parse(str13), str12, str17, str18, str19, str20, str21, str22, _kc.get_ip(), rateByUserObject, rate, 1, str15, fgsWTTable);
                            if (rateByUserObject.get_kc_op_odds().Equals(1))
                            {
                                fgsWTTable = base.GetFgsWTTable(0);
                            }
                            CallBLL.cz_autosale_kl10_bll.FGSAutoSaleLM(_kc.get_order_num(), str10, int.Parse(table9.Rows[0]["play_id"].ToString()), int.Parse(str6.Trim()), double.Parse(_kc.get_odds()), num, _kc.get_lottery_name(), int.Parse(str13), str12, str17, str18, str19, str20, str21, str22, _kc.get_ip(), rateByUserObject, rate, 1, str15, fgsWTTable);
                            base.Un_Bet_Lock(0.ToString(), rate.get_fgsname(), str6);
                            result.set_tipinfo(string.Format("下注成功！", new object[0]));
                            result.set_success(200);
                            dictionary.Add("JeuValidate", base.get_JeuValidate());
                            result.set_data(dictionary);
                            strResult = JsonHandle.ObjectToJson(result);
                            base.SetUserRate_kc(rate);
                            base.SetUserDrawback_kl10(dataTable);
                            rateByUserObject.set_begin_kc("yes");
                        }
                    }
                }
            }
        }

        private string px(string p_str)
        {
            string[] array = p_str.Split(new char[] { ',' });
            Array.Sort<string>(array);
            return string.Join(",", array);
        }

        private string px_pcdd(string p_str)
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

