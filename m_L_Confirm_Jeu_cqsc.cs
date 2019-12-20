using LotterySystem.Common;
using LotterySystem.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using User.Web.WebBase;

public class m_L_Confirm_Jeu_cqsc : MemberPageBase_Mobile
{
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

    public static void get_drawback(ref cz_userinfo_session uModel, string play_id, ref string zj_ds, ref string fgs_ds, ref string gd_ds, ref string zd_ds, ref string dl_ds, ref string hy_ds, string a_m_type, string hy_pk)
    {
        string str = "'" + uModel.get_kc_session().get_fgsname() + "','" + uModel.get_kc_session().get_gdname() + "','" + uModel.get_kc_session().get_zdname() + "','" + uModel.get_kc_session().get_dlname() + "','" + uModel.get_u_name() + "'";
        DataTable table = CallBLL.cz_drawback_kl10_bll.GetDrawbacks(play_id.Trim(), str).Tables[0];
        foreach (DataRow row in table.Rows)
        {
            string str2 = row["u_type"].ToString().Trim();
            if (str2 != null)
            {
                if (!(str2 == "zj"))
                {
                    if (str2 == "fgs")
                    {
                        goto Label_0178;
                    }
                    if (str2 == "gd")
                    {
                        goto Label_019B;
                    }
                    if (str2 == "zd")
                    {
                        goto Label_01BF;
                    }
                    if (str2 == "dl")
                    {
                        goto Label_01E3;
                    }
                    if (str2 == "hy")
                    {
                        goto Label_0207;
                    }
                }
                else
                {
                    zj_ds = row[hy_pk + "_drawback"].ToString().Trim();
                }
            }
            continue;
        Label_0178:
            fgs_ds = row[hy_pk + "_drawback"].ToString().Trim();
            continue;
        Label_019B:
            gd_ds = row[hy_pk + "_drawback"].ToString().Trim();
            continue;
        Label_01BF:
            zd_ds = row[hy_pk + "_drawback"].ToString().Trim();
            continue;
        Label_01E3:
            dl_ds = row[hy_pk + "_drawback"].ToString().Trim();
            continue;
        Label_0207:
            if (a_m_type == "dl")
            {
                hy_ds = row[hy_pk + "_drawback"].ToString().Trim();
            }
            else if (a_m_type == "zd")
            {
                dl_ds = row[hy_pk + "_drawback"].ToString().Trim();
                hy_ds = row[hy_pk + "_drawback"].ToString().Trim();
            }
            else if (a_m_type == "gd")
            {
                zd_ds = row[hy_pk + "_drawback"].ToString().Trim();
                hy_ds = row[hy_pk + "_drawback"].ToString().Trim();
            }
            else if (a_m_type == "fgs")
            {
                gd_ds = row[hy_pk + "_drawback"].ToString().Trim();
                hy_ds = row[hy_pk + "_drawback"].ToString().Trim();
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        DateTime time;
        base.Response.Expires = 0;
        if (!base.IsUserLoginByMobileForAjax())
        {
            this.Session.Abandon();
            base.Response.Write("<script>top.location.href='/m'</script>");
            base.Response.End();
        }
        int num30 = 1;
        if (!base.IsLotteryExistByPhone(num30.ToString()))
        {
            this.Session.Abandon();
            base.Response.Write("<script>top.location.href='/m'</script>");
            base.Response.End();
        }
        string str = this.Session["user_name"].ToString();
        string str2 = "";
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
            str2 = "系統繁忙，請稍後！";
            str2 = string.Format("<script>alert('{0}');</script>", str2);
            base.Response.Write(str2);
            base.Response.End();
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
                    base.DeleteCreditLock(str);
                    str2 = "會員或上級已經被凍結,請與上級聯系！";
                    str2 = string.Format("<script>alert('{0}');</script>", str2);
                    base.Response.Write(str2);
                    base.Response.End();
                }
                else
                {
                    base.DeleteCreditLock(str);
                    str2 = "會員或上級已經被停用,請與上級聯系！";
                    str2 = string.Format("<script>alert('{0}');</script>", str2);
                    base.Response.Write(str2);
                    base.Response.End();
                }
            }
        }
        else
        {
            base.DeleteCreditLock(str);
            base.Response.End();
        }
        string str4 = base.qq("caizhong");
        string str5 = base.qq("wanfa");
        string str6 = base.qq("jiangqi");
        string str7 = "CQSC_" + str5 + ".aspx?lottery_type=" + str4 + "&player_type=" + str5;
        string str8 = "";
        string str9 = "";
        string str10 = "";
        string str11 = "";
        string str12 = "";
        str9 = base.qq("uPI_ID");
        str10 = base.qq("uPI_P");
        str11 = base.qq("uPI_M");
        string[] strArray = base.qq("i_index").Split(new char[] { ',' });
        str12 = base.qq("JeuValidate");
        str8 = base.qq("shortcut");
        if ((((string.IsNullOrEmpty(str4) || string.IsNullOrEmpty(str5)) || (string.IsNullOrEmpty(str6) || string.IsNullOrEmpty(str9))) || string.IsNullOrEmpty(str10)) || string.IsNullOrEmpty(str11))
        {
            base.DeleteCreditLock(str);
            base.Response.End();
        }
        string[] source = str9.Split(new char[] { ',' });
        if (source.Distinct<string>().ToList<string>().Count != source.Length)
        {
            base.DeleteCreditLock(str);
            base.Response.End();
        }
        if (source.Length > FileCacheHelper.get_GetCQSCMaxGroup())
        {
            base.DeleteCreditLock(str);
            base.Response.End();
        }
        string str13 = str5;
        string s = str6;
        int num2 = 1;
        if ((this.Session["JeuValidate"].ToString().Trim() != str12) || (this.Session["JeuValidate"].ToString().Trim() == ""))
        {
            base.DeleteCreditLock(str);
            base.Response.Write("<script>alert('下注規則有誤,請重新下注,謝謝合作!');$('#JeuValidate').val('" + base.get_JeuValidate() + "');</script>");
            base.Response.End();
        }
        else
        {
            this.Session["JeuValidate"] = "";
        }
        int num3 = 1;
        if (getUserModelInfo.get_su_type().ToString().Trim() != "dl")
        {
            num3 = -1;
        }
        int index = 0;
    Label_05F3:;
        if (index < str11.Split(new char[] { ',' }).Length)
        {
            if (!base.IsNumber(str11.Split(new char[] { ',' })[index]))
            {
                base.DeleteCreditLock(str);
                base.Response.Write("<script>alert($(\"#v_" + strArray[index] + "\").text()+' 下注金額有誤！');$('#m_" + strArray[index] + "').focus();$('#JeuValidate').val('" + base.get_JeuValidate() + "');</script>");
                base.Response.End();
                return;
            }
            index++;
            goto Label_05F3;
        }
        if (getUserModelInfo.get_begin_kc().Trim() != "yes")
        {
            getUserModelInfo = base.GetRateByUserObject(2, rate);
        }
        string str16 = "";
        string str17 = "";
        if (!Utils.IsInteger(s))
        {
            base.Response.End();
        }
        DataTable table2 = CallBLL.cz_phase_cqsc_bll.GetIsClosedByTime(int.Parse(s)).Tables[0];
        if ((table2 != null) && (table2.Rows.Count > 0))
        {
            if (table2.Rows[0]["is_closed"].ToString().Trim() == "1")
            {
                base.DeleteCreditLock(str);
                base.Response.Write("<script>alert('該獎期已經截止下注！');window.location=\"" + str7 + "\";</script>");
                base.Response.End();
                return;
            }
            str16 = table2.Rows[0]["phase"].ToString();
            str17 = table2.Rows[0]["p_id"].ToString();
            time = Convert.ToDateTime(table2.Rows[0]["stop_date"].ToString());
            num30 = 1;
            string str18 = base.BetReceiveEnd(num30.ToString());
            if (!string.IsNullOrEmpty(str18))
            {
                time = Convert.ToDateTime(table2.Rows[0]["play_open_date"].ToString()).AddSeconds((double) -int.Parse(str18));
            }
        }
        else
        {
            base.DeleteCreditLock(str);
            base.Response.Write("<script>alert('該獎期已經截止下注！');window.location=\"" + str7 + "\";</script>");
            base.Response.End();
            return;
        }
        string str19 = "";
        string str20 = "";
        double num5 = 0.0;
        DataTable table3 = CallBLL.cz_users_bll.GetUserRate(str, 2).Tables[0];
        str19 = table3.Rows[0]["kc_kind"].ToString().Trim().ToUpper();
        str20 = table3.Rows[0]["su_type"].ToString().Trim();
        num5 = double.Parse(table3.Rows[0]["kc_usable_credit"].ToString().Trim());
        getUserModelInfo.set_kc_kind(str19.ToUpper());
        string str21 = "0";
        string str22 = "0";
        string str23 = "0";
        string str24 = "0";
        string str25 = "0";
        string str26 = "0";
        double num6 = 0.0;
        double num7 = 0.0;
        double num8 = 0.0;
        double num9 = 0.0;
        double num10 = 0.0;
        double num11 = 0.0;
        string str27 = "";
        string str28 = "";
        string str29 = "";
        string str30 = "";
        string str31 = "";
        string[] strArray3 = str9.Split(new char[] { ',' });
        string[] strArray4 = str10.Split(new char[] { ',' });
        string[] strArray5 = str11.Split(new char[] { ',' });
        DataTable plDT = CallBLL.cz_odds_cqsc_bll.GetPlayOddsByID(str9).Tables[0];
        if (plDT.Rows.Count == 0)
        {
            base.DeleteCreditLock(str);
            base.Response.End();
        }
        else
        {
            DataTable table5 = CallBLL.cz_bet_kc_bll.GetSinglePhaseByBet(str16, str, num2.ToString(), str9, null).Tables[0];
            if ((plDT.Rows.Count == 0) || (plDT.Rows.Count != strArray3.Count<string>()))
            {
                base.DeleteCreditLock(str);
                base.Response.End();
            }
            else
            {
                double num12 = 0.0;
                string str32 = "";
                List<string> list = new List<string>();
                List<string> list2 = new List<string>();
                int num13 = 0;
                foreach (string str33 in strArray3)
                {
                    num6 = 0.0;
                    row = plDT.Select(string.Format(" odds_id= {0} ", str33.Trim()))[0];
                    str32 = row["is_open"].ToString().Trim();
                    str28 = row["current_odds"].ToString().Trim();
                    str29 = row[str19 + "_diff"].ToString().Trim();
                    str27 = row["play_id"].ToString().Trim();
                    str30 = row["play_name"].ToString().Trim();
                    str31 = row["put_amount"].ToString().Trim();
                    num11 = double.Parse(row["allow_min_amount"].ToString().Trim());
                    num8 = double.Parse(row["allow_max_amount"].ToString().Trim());
                    num10 = double.Parse(row["allow_max_put_amount"].ToString().Trim());
                    if ((table5 != null) && (table5.Rows.Count > 0))
                    {
                        DataRow[] rowArray2 = table5.Select(string.Format(" odds_id = {0} ", str33.Trim()));
                        if (rowArray2.Count<DataRow>() > 0)
                        {
                            num6 = double.Parse(rowArray2[0]["sumbet"].ToString().Trim());
                        }
                    }
                    DataTable table6 = null;
                    if (FileCacheHelper.get_GetKCPutMoneyCache() == "1")
                    {
                        if (CacheHelper.GetCache("kc_drawback_FileCacheKey_cqsc" + str27 + this.Session["user_name"].ToString()) == null)
                        {
                            table6 = CallBLL.cz_drawback_cqsc_bll.GetDrawback(str27, str).Tables[0];
                        }
                        else
                        {
                            table6 = base.GetUserDrawback_cqsc(rate, getUserModelInfo.get_kc_kind(), str27);
                        }
                    }
                    else if (CacheHelper.GetCache("kc_drawback_FileCacheKey_cqsc" + this.Session["user_name"].ToString()) == null)
                    {
                        table6 = CallBLL.cz_drawback_cqsc_bll.GetDrawback(str27, str).Tables[0];
                    }
                    else
                    {
                        table6 = base.GetUserDrawback_cqsc(rate, getUserModelInfo.get_kc_kind());
                    }
                    DataRow[] rowArray3 = table6.Select(string.Format(" play_id={0} and u_name='{1}' ", str27, str));
                    num7 = double.Parse(rowArray3[0]["single_max_amount"].ToString().Trim());
                    double num14 = double.Parse(rowArray3[0]["single_min_amount"].ToString().Trim());
                    num9 = double.Parse(rowArray3[0]["single_phase_amount"].ToString().Trim());
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
                    if (str32 != "1")
                    {
                        base.DeleteCreditLock(str);
                        base.Response.Write("<script>alert('" + str30 + "【" + str31 + "】已經停止投注！！');$('#JeuValidate').val('" + base.get_JeuValidate() + "');</script>");
                        base.Response.End();
                    }
                    num12 = double.Parse(str28) + double.Parse(str29);
                    string pl = num12.ToString();
                    base.GetOdds_KC(1, str33, ref pl);
                    num12 = Convert.ToDouble(pl);
                    if (!(double.Parse(strArray4[num13]) == double.Parse(num12.ToString())))
                    {
                        list.Add(num13.ToString());
                        list2.Add(num12.ToString());
                    }
                    if (list.Count > 0)
                    {
                        base.DeleteCreditLock(str);
                        base.Response.Write("<script>alert('" + str30 + "【" + str31 + "】賠率已經由 " + strArray4[num13] + " 變為 " + num12.ToString() + " 請確認再投註！');$('#v_" + strArray[num13] + " ~ span.hong').text('" + num12.ToString() + "');$('#p_" + strArray[num13] + "').val('" + num12.ToString() + "');$('#JeuValidate').val('" + base.get_JeuValidate() + "');</script>");
                        base.Response.End();
                        return;
                    }
                    if (double.Parse(strArray5[num13]) > num7)
                    {
                        base.DeleteCreditLock(str);
                        base.Response.Write("<script>alert($(\"#v_" + strArray[num13] + "\").text()+' 下注金額超出單注最大金額！');$('#m_" + strArray[num13] + "').focus();$('#JeuValidate').val('" + base.get_JeuValidate() + "');</script>");
                        base.Response.End();
                        return;
                    }
                    if (double.Parse(strArray5[num13]) < num11)
                    {
                        base.DeleteCreditLock(str);
                        base.Response.Write("<script>alert($(\"#v_" + strArray[num13] + "\").text()+' 下注金額低過最低金額！');$('#m_" + strArray[num13] + "').focus();$('#JeuValidate').val('" + base.get_JeuValidate() + "');</script>");
                        base.Response.End();
                        return;
                    }
                    if (double.Parse(strArray5[num13]) > num5)
                    {
                        base.DeleteCreditLock(str);
                        base.Response.Write("<script>alert($(\"#v_" + strArray[num13] + "\").text()+' 下注金額超出可用金額！');$('#m_" + strArray[num13] + "').focus();$('#JeuValidate').val('" + base.get_JeuValidate() + "');</script>");
                        base.Response.End();
                        return;
                    }
                    if ((double.Parse(strArray5[num13]) + num6) > num9)
                    {
                        base.DeleteCreditLock(str);
                        base.Response.Write("<script>alert('" + str30 + "【" + str31 + "】下註單期金額超出單期最大金額！');</script>");
                        base.Response.End();
                        return;
                    }
                    num13++;
                }
                double num15 = 0.0;
                int num16 = 0;
                foreach (string str35 in strArray3)
                {
                    num15 += double.Parse(strArray5[num16].ToString().Trim());
                    num16++;
                }
                if (num5 < num15)
                {
                    base.DeleteCreditLock(str);
                    base.Response.Write("<script>alert('可用餘額不足！');$('#JeuValidate').val('" + base.get_JeuValidate() + "');</script>");
                    base.Response.End();
                }
                else
                {
                    DataTable dataTable = null;
                    bool flag2 = false;
                    List<string> successBetList = new List<string>();
                    DateTime? nullable = null;
                    num13 = 0;
                    foreach (string str33 in strArray3)
                    {
                        double num20;
                        if (FileCacheHelper.get_AddBetLockUType_KC().Equals("zj"))
                        {
                            num30 = 1;
                            base.Add_Bet_Lock(num30.ToString(), getUserModelInfo.get_zjname(), str33);
                        }
                        DataTable table8 = CallBLL.cz_odds_cqsc_bll.GetOddsByID(str33).Tables[0];
                        int num17 = int.Parse(table8.Rows[0]["play_id"].ToString());
                        string str36 = table8.Rows[0]["play_name"].ToString();
                        string str37 = table8.Rows[0]["put_amount"].ToString();
                        string ratio = table8.Rows[0]["ratio"].ToString();
                        if (FileCacheHelper.get_GetKCPutMoneyCache() == "1")
                        {
                            dataTable = base.GetUserDrawback_cqsc(rate, getUserModelInfo.get_kc_kind(), num17.ToString());
                        }
                        else
                        {
                            dataTable = base.GetUserDrawback_cqsc(rate, getUserModelInfo.get_kc_kind());
                        }
                        if (dataTable == null)
                        {
                            base.DeleteCreditLock(str);
                            if (FileCacheHelper.get_AddBetLockUType_KC().Equals("zj"))
                            {
                                num30 = 1;
                                base.Un_Bet_Lock(num30.ToString(), getUserModelInfo.get_zjname(), str33);
                            }
                            else
                            {
                                num30 = 1;
                                base.Un_Bet_Lock(num30.ToString(), rate.get_fgsname(), str33);
                            }
                            base.Response.Write("<script>alert('系統錯誤,請重試！');$('#JeuValidate').val('" + base.get_JeuValidate() + "');</script>");
                            base.Response.End();
                            return;
                        }
                        DataRow[] rowArray4 = dataTable.Select(string.Format(" play_id={0} ", num17));
                        foreach (DataRow row in rowArray4)
                        {
                            string str42 = row["u_type"].ToString().Trim();
                            if (str42 != null)
                            {
                                if (!(str42 == "zj"))
                                {
                                    if (str42 == "fgs")
                                    {
                                        goto Label_152D;
                                    }
                                    if (str42 == "gd")
                                    {
                                        goto Label_1551;
                                    }
                                    if (str42 == "zd")
                                    {
                                        goto Label_1575;
                                    }
                                    if (str42 == "dl")
                                    {
                                        goto Label_1599;
                                    }
                                    if (str42 == "hy")
                                    {
                                        goto Label_15BD;
                                    }
                                }
                                else
                                {
                                    str21 = row[str19 + "_drawback"].ToString().Trim();
                                }
                            }
                            continue;
                        Label_152D:
                            str22 = row[str19 + "_drawback"].ToString().Trim();
                            continue;
                        Label_1551:
                            str23 = row[str19 + "_drawback"].ToString().Trim();
                            continue;
                        Label_1575:
                            str24 = row[str19 + "_drawback"].ToString().Trim();
                            continue;
                        Label_1599:
                            str25 = row[str19 + "_drawback"].ToString().Trim();
                            continue;
                        Label_15BD:
                            if (getUserModelInfo.get_su_type() == "dl")
                            {
                                str26 = row[str19 + "_drawback"].ToString().Trim();
                            }
                            else if (getUserModelInfo.get_su_type() == "zd")
                            {
                                str25 = row[str19 + "_drawback"].ToString().Trim();
                                str26 = row[str19 + "_drawback"].ToString().Trim();
                            }
                            else if (getUserModelInfo.get_su_type() == "gd")
                            {
                                str24 = row[str19 + "_drawback"].ToString().Trim();
                                str26 = row[str19 + "_drawback"].ToString().Trim();
                            }
                            else if (getUserModelInfo.get_su_type() == "fgs")
                            {
                                str23 = row[str19 + "_drawback"].ToString().Trim();
                                str26 = row[str19 + "_drawback"].ToString().Trim();
                            }
                        }
                        double num18 = 0.0;
                        double num19 = 0.0;
                        if (rate.get_zcyg().Equals("1"))
                        {
                            num20 = (((100.0 - double.Parse(rate.get_fgszc())) - double.Parse(rate.get_gdzc())) - double.Parse(rate.get_zdzc())) - double.Parse(rate.get_dlzc());
                            num18 = num20;
                            num19 = double.Parse(rate.get_fgszc());
                        }
                        else
                        {
                            num20 = (((100.0 - double.Parse(rate.get_zjzc())) - double.Parse(rate.get_gdzc())) - double.Parse(rate.get_zdzc())) - double.Parse(rate.get_dlzc());
                            num19 = num20;
                            num18 = double.Parse(rate.get_zjzc());
                        }
                        if (DateTime.Now >= time.AddSeconds(0.0))
                        {
                            flag2 = true;
                            break;
                        }
                        if (!FileCacheHelper.get_AddBetLockUType_KC().Equals("zj"))
                        {
                            num30 = 1;
                            base.Add_Bet_Lock(num30.ToString(), rate.get_fgsname(), str33);
                        }
                        double num33 = double.Parse(table8.Rows[0]["current_odds"].ToString()) + Convert.ToDouble(table8.Rows[0][str19 + "_diff"].ToString().Trim());
                        string str39 = num33.ToString();
                        string str40 = str39.ToString();
                        base.GetOdds_KC(1, str33, ref str40);
                        double playDrawbackValue = base.GetPlayDrawbackValue(str22, ratio);
                        if ((playDrawbackValue != 0.0) && (double.Parse(str39) > playDrawbackValue))
                        {
                            base.DeleteCreditLock(str);
                            if (FileCacheHelper.get_AddBetLockUType_KC().Equals("zj"))
                            {
                                num30 = 1;
                                base.Un_Bet_Lock(num30.ToString(), getUserModelInfo.get_zjname(), str33);
                            }
                            else
                            {
                                num30 = 1;
                                base.Un_Bet_Lock(num30.ToString(), rate.get_fgsname(), str33);
                            }
                            base.Response.Write("<script>alert('賠率錯誤！');$('#JeuValidate').val('" + base.get_JeuValidate() + "');</script>");
                            base.Response.End();
                        }
                        if (!nullable.HasValue)
                        {
                            nullable = new DateTime?(DateTime.Now);
                        }
                        cz_bet_kc _kc = new cz_bet_kc();
                        _kc.set_order_num(Utils.GetOrderNumber());
                        _kc.set_checkcode(str12);
                        _kc.set_u_name(getUserModelInfo.get_u_name());
                        _kc.set_u_nicker(getUserModelInfo.get_u_nicker());
                        _kc.set_phase_id(new int?(int.Parse(str17)));
                        _kc.set_phase(str16);
                        _kc.set_bet_time(nullable);
                        _kc.set_odds_id(new int?(int.Parse(str33)));
                        _kc.set_category(table8.Rows[0]["category"].ToString());
                        _kc.set_play_id(new int?(int.Parse(table8.Rows[0]["play_id"].ToString())));
                        _kc.set_play_name(str36);
                        _kc.set_bet_val(str37);
                        _kc.set_odds(str40);
                        _kc.set_amount(new decimal?(decimal.Parse(strArray5[num13])));
                        _kc.set_profit(0);
                        _kc.set_hy_drawback(new decimal?(decimal.Parse(str26)));
                        _kc.set_dl_drawback(new decimal?(decimal.Parse(str25)));
                        _kc.set_zd_drawback(new decimal?(decimal.Parse(str24)));
                        _kc.set_gd_drawback(new decimal?(decimal.Parse(str23)));
                        _kc.set_fgs_drawback(new decimal?(decimal.Parse(str22)));
                        _kc.set_zj_drawback(new decimal?(decimal.Parse(str21)));
                        _kc.set_dl_rate((float) int.Parse(rate.get_dlzc()));
                        _kc.set_zd_rate((float) int.Parse(rate.get_zdzc()));
                        _kc.set_gd_rate((float) int.Parse(rate.get_gdzc()));
                        _kc.set_fgs_rate(float.Parse(num19.ToString()));
                        _kc.set_zj_rate(float.Parse(num18.ToString()));
                        _kc.set_dl_name(rate.get_dlname());
                        _kc.set_zd_name(rate.get_zdname());
                        _kc.set_gd_name(rate.get_gdname());
                        _kc.set_fgs_name(rate.get_fgsname());
                        _kc.set_is_payment(0);
                        _kc.set_m_type(new int?(num3));
                        _kc.set_kind(str19);
                        _kc.set_ip(LSRequest.GetIP());
                        _kc.set_lottery_type(new int?(num2));
                        _kc.set_lottery_name(base.GetGameNameByID(_kc.get_lottery_type().ToString()));
                        _kc.set_ordervalidcode(Utils.GetOrderValidCode(_kc.get_u_name(), _kc.get_order_num(), _kc.get_bet_val(), _kc.get_odds(), _kc.get_kind(), Convert.ToInt32(_kc.get_phase_id()), Convert.ToInt32(_kc.get_odds_id()), Convert.ToDouble(_kc.get_amount())));
                        _kc.set_odds_zj(str39);
                        _kc.set_isPhone(1);
                        int num22 = 0;
                        if (!CallBLL.cz_bet_kc_bll.AddBet(_kc, decimal.Parse(strArray5[num13]), str, ref num22) || (num22 <= 0))
                        {
                            base.DeleteCreditLock(str);
                            if (FileCacheHelper.get_AddBetLockUType_KC().Equals("zj"))
                            {
                                num30 = 1;
                                base.Un_Bet_Lock(num30.ToString(), getUserModelInfo.get_zjname(), str33);
                            }
                            else
                            {
                                num30 = 1;
                                base.Un_Bet_Lock(num30.ToString(), rate.get_fgsname(), str33);
                            }
                            base.Response.Write("<script>alert('系統錯誤,請重試！');$('#JeuValidate').val('" + base.get_JeuValidate() + "');</script>");
                            base.Response.End();
                            return;
                        }
                        successBetList.Add(string.Concat(new object[] { _kc.get_odds_id(), ",", _kc.get_order_num(), ",", _kc.get_play_name(), ",", _kc.get_bet_val(), ",", _kc.get_odds(), ",", _kc.get_amount() }));
                        double num23 = (double.Parse(strArray5[num13]) * num18) / 100.0;
                        CallBLL.cz_odds_cqsc_bll.UpdateGrandTotal(Convert.ToDecimal(num23), int.Parse(str33));
                        double num24 = double.Parse(table8.Rows[0]["grand_total"].ToString()) + num23;
                        double num25 = double.Parse(table8.Rows[0]["downbase"].ToString());
                        double num26 = Math.Floor((double) (num24 / num25));
                        if ((num26 >= 1.0) && (num25 >= 1.0))
                        {
                            double num27 = double.Parse(table8.Rows[0]["down_odds_rate"].ToString()) * num26;
                            int num29 = CallBLL.cz_odds_cqsc_bll.UpdateGrandTotalCurrentOdds(num27.ToString(), (num23 - (num25 * num26)).ToString(), str33);
                            cz_system_log _log = new cz_system_log();
                            _log.set_user_name("系統");
                            _log.set_children_name("");
                            _log.set_category(table8.Rows[0]["category"].ToString());
                            _log.set_play_name(str36);
                            _log.set_put_amount(str37);
                            num30 = 1;
                            _log.set_l_name(base.GetGameNameByID(num30.ToString()));
                            _log.set_l_phase(str16);
                            _log.set_action("降賠率");
                            _log.set_odds_id(int.Parse(str33));
                            string str41 = table8.Rows[0]["current_odds"].ToString();
                            _log.set_old_val(str41);
                            num33 = double.Parse(str41) - num27;
                            _log.set_new_val(num33.ToString());
                            _log.set_ip(LSRequest.GetIP());
                            _log.set_add_time(DateTime.Now);
                            _log.set_note("系統自動降賠");
                            _log.set_type_id(Convert.ToInt32((LSEnums.LogTypeID) 0));
                            _log.set_lottery_type(1);
                            CallBLL.cz_system_log_bll.Add(_log);
                            cz_jp_odds _odds = new cz_jp_odds();
                            _odds.set_add_time(DateTime.Now);
                            _odds.set_odds_id(int.Parse(str33));
                            _odds.set_phase_id(int.Parse(str17));
                            _odds.set_play_name(str36);
                            _odds.set_put_amount(str37);
                            _odds.set_odds(num27.ToString());
                            _odds.set_lottery_type(1);
                            _odds.set_phase(str16);
                            _odds.set_old_odds(str41);
                            _odds.set_new_odds((double.Parse(str41) - num27).ToString());
                            CallBLL.cz_jp_odds_bll.Add(_odds);
                        }
                        DataTable fgsWTTable = null;
                        if (getUserModelInfo.get_kc_op_odds().Equals(1))
                        {
                            fgsWTTable = base.GetFgsWTTable(1);
                        }
                        CallBLL.cz_autosale_cqsc_bll.DLAutoSale(_kc.get_order_num(), str12, getUserModelInfo.get_u_nicker(), getUserModelInfo.get_u_name(), str19, table8.Rows[0]["play_id"].ToString(), str33.Trim(), str21, str22, str23, str24, str25, str26, str17, str16, _kc.get_ip(), num2, _kc.get_lottery_name(), getUserModelInfo, rate, fgsWTTable);
                        if (getUserModelInfo.get_kc_op_odds().Equals(1))
                        {
                            fgsWTTable = base.GetFgsWTTable(1);
                        }
                        CallBLL.cz_autosale_cqsc_bll.ZDAutoSale(_kc.get_order_num(), str12, getUserModelInfo.get_u_nicker(), getUserModelInfo.get_u_name(), str19, table8.Rows[0]["play_id"].ToString(), str33.Trim(), str21, str22, str23, str24, str25, str26, str17, str16, _kc.get_ip(), num2, _kc.get_lottery_name(), getUserModelInfo, rate, fgsWTTable);
                        if (getUserModelInfo.get_kc_op_odds().Equals(1))
                        {
                            fgsWTTable = base.GetFgsWTTable(1);
                        }
                        CallBLL.cz_autosale_cqsc_bll.GDAutoSale(_kc.get_order_num(), str12, getUserModelInfo.get_u_nicker(), getUserModelInfo.get_u_name(), str19, table8.Rows[0]["play_id"].ToString(), str33.Trim(), str21, str22, str23, str24, str25, str26, str17, str16, _kc.get_ip(), num2, _kc.get_lottery_name(), getUserModelInfo, rate, fgsWTTable);
                        if (getUserModelInfo.get_kc_op_odds().Equals(1))
                        {
                            fgsWTTable = base.GetFgsWTTable(1);
                        }
                        CallBLL.cz_autosale_cqsc_bll.FGSAutoSale(_kc.get_order_num(), str12, getUserModelInfo.get_u_nicker(), getUserModelInfo.get_u_name(), str19, table8.Rows[0]["play_id"].ToString(), str33.Trim(), str21, str22, str23, str24, str25, str26, str17, str16, _kc.get_ip(), num2, _kc.get_lottery_name(), getUserModelInfo, rate, fgsWTTable);
                        if (FileCacheHelper.get_AddBetLockUType_KC().Equals("zj"))
                        {
                            num30 = 1;
                            base.Un_Bet_Lock(num30.ToString(), getUserModelInfo.get_zjname(), str33);
                        }
                        else
                        {
                            base.Un_Bet_Lock(1.ToString(), rate.get_fgsname(), str33);
                        }
                        num13++;
                        if (FileCacheHelper.get_GetKCPutMoneyCache() == "1")
                        {
                            base.SetUserDrawback_cqsc(dataTable, num17.ToString());
                        }
                    }
                    base.SetUserRate_kc(rate);
                    if (FileCacheHelper.get_GetKCPutMoneyCache() != "1")
                    {
                        base.SetUserDrawback_cqsc(dataTable);
                    }
                    getUserModelInfo.set_begin_kc("yes");
                    base.UserPutBetByPhone(plDT, successBetList, strArray3, strArray4, strArray5);
                    base.DeleteCreditLock(str);
                    base.Response.Write(string.Format("<script>window.location=\"tj_ok.aspx?lottery_type={0}&player_type={1}\";</script>", str4, str5));
                    base.Response.End();
                }
            }
        }
    }

    public static string px(string p_str)
    {
        string[] array = p_str.Split(new char[] { ',' });
        Array.Sort<string>(array);
        return string.Join(",", array);
    }
}

