namespace Agent.Web.L_PKBJL.Handler
{
    using Agent.Web.Handler;
    using Agent.Web.WebBase;
    using log4net;
    using LotterySystem.Common;
    using LotterySystem.Model;
    using LotterySystem.WebPageBase;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Web;

    public class Handler : BaseHandler
    {
        private static ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private void FGSBH(agent_userinfo_session userModel, agent_kc_rate kc_rate, string kc_kind, HttpContext context, string phase_id, string phase, string play_id, string odds_id, string zj_ds, string fgs_ds, string gd_ds, string zd_ds, string dl_ds, string hy_ds, string checkcode, string numTable, string playtype)
        {
            if (!string.IsNullOrEmpty(kc_rate.get_fgsname()))
            {
                DataTable isAllowSale = CallBLL.cz_users_bll.GetIsAllowSale(kc_rate.get_fgsname());
                if ((isAllowSale.Rows[0]["kc_allow_sale"] != null) && (Convert.ToInt32(isAllowSale.Rows[0]["kc_allow_sale"]) != 0))
                {
                    double num = 1.0;
                    DataTable autosale = CallBLL.cz_autosale_pkbjl_bll.GetAutosale(kc_rate.get_fgsname(), play_id);
                    if (((autosale != null) && (autosale.Rows.Count > 0)) && autosale.Rows[0]["sale_type"].ToString().Equals("0"))
                    {
                        DataTable table3 = CallBLL.cz_bet_kc_bll.GetSZZTBHByOddsid("fgs", kc_rate.get_fgsname(), odds_id, phase_id, 8);
                        if ((double.Parse(table3.Rows[0]["szzt"].ToString()) - num) > double.Parse(autosale.Rows[0]["max_money"].ToString()))
                        {
                            double num2 = double.Parse(table3.Rows[0]["szzt"].ToString());
                            double num3 = double.Parse(autosale.Rows[0]["max_money"].ToString());
                            double num4 = Math.Floor((double) (num2 - num3));
                            DataSet playOddsInfo = CallBLL.cz_play_pkbjl_bll.GetPlayOddsInfo(odds_id);
                            string s = playOddsInfo.Tables[0].Rows[0]["current_odds"].ToString().Trim();
                            string str2 = playOddsInfo.Tables[0].Rows[0]["play_name"].ToString();
                            string str3 = playOddsInfo.Tables[0].Rows[0]["put_amount"].ToString();
                            string str4 = playOddsInfo.Tables[0].Rows[0][kc_kind.ToUpper() + "_diff"].ToString().Trim();
                            string str5 = (double.Parse(s) + double.Parse(str4)).ToString();
                            base.GetOdds_KC_EX(8, playOddsInfo.Tables[0].Rows[0]["current_odds"].ToString().Trim(), str4, ref s, odds_id);
                            int num13 = 0;
                            if (playtype.Equals(num13.ToString()))
                            {
                                str5 = Utils.GetPKBJLPlaytypeOdds(odds_id, double.Parse(str5), base.GetPlayTypeWTValue_PKBJL(odds_id)).ToString();
                                s = Utils.GetPKBJLPlaytypeOdds(odds_id, double.Parse(s), base.GetPlayTypeWTValue_PKBJL(odds_id)).ToString();
                            }
                            if (userModel.get_kc_op_odds().Equals(1))
                            {
                                s = str5;
                            }
                            gd_ds = fgs_ds;
                            string str6 = kc_rate.get_zjzc();
                            str6 = "100";
                            cz_bet_kc _kc = new cz_bet_kc();
                            _kc.set_order_num(Utils.GetOrderNumber());
                            _kc.set_checkcode(checkcode);
                            _kc.set_u_name(kc_rate.get_fgsname());
                            _kc.set_u_nicker("");
                            _kc.set_phase_id(new int?(int.Parse(phase_id)));
                            _kc.set_phase(phase);
                            _kc.set_bet_time(new DateTime?(DateTime.Now));
                            _kc.set_odds_id(new int?(int.Parse(odds_id)));
                            _kc.set_category(playOddsInfo.Tables[0].Rows[0]["category"].ToString());
                            _kc.set_play_id(new int?(int.Parse(play_id)));
                            _kc.set_play_name(str2);
                            _kc.set_bet_val(str3);
                            _kc.set_bet_wt("");
                            _kc.set_odds(s);
                            _kc.set_amount(new decimal?(decimal.Parse(num4.ToString())));
                            _kc.set_profit(0);
                            _kc.set_hy_drawback(0);
                            _kc.set_dl_drawback(0);
                            _kc.set_zd_drawback(0);
                            _kc.set_gd_drawback(new decimal?(decimal.Parse(gd_ds)));
                            _kc.set_fgs_drawback(new decimal?(decimal.Parse(fgs_ds)));
                            _kc.set_zj_drawback(new decimal?(decimal.Parse(zj_ds)));
                            _kc.set_dl_rate(0f);
                            _kc.set_zd_rate(0f);
                            _kc.set_gd_rate(0f);
                            _kc.set_fgs_rate(0f);
                            _kc.set_zj_rate((float) int.Parse(str6));
                            _kc.set_dl_name("");
                            _kc.set_zd_name("");
                            _kc.set_gd_name("");
                            _kc.set_fgs_name(kc_rate.get_fgsname());
                            _kc.set_is_payment(0);
                            _kc.set_sale_type(1);
                            _kc.set_m_type(-2);
                            _kc.set_kind(kc_kind);
                            _kc.set_ip(LSRequest.GetIP());
                            _kc.set_lottery_type(8);
                            _kc.set_lottery_name(base.GetGameNameByID(_kc.get_lottery_type().ToString()));
                            _kc.set_ordervalidcode(Utils.GetOrderValidCode(_kc.get_fgs_name(), _kc.get_order_num(), _kc.get_bet_val(), _kc.get_odds(), _kc.get_kind(), Convert.ToInt32(_kc.get_phase_id()), Convert.ToInt32(_kc.get_odds_id()), Convert.ToDouble(_kc.get_amount())));
                            _kc.set_odds_zj(str5);
                            _kc.set_table_type(new int?(int.Parse(numTable)));
                            _kc.set_play_type(new int?(int.Parse(playtype)));
                            int num5 = 0;
                            CallBLL.cz_bet_kc_bll.AddBetByBH(_kc, ref num5);
                            double num6 = (num4 * double.Parse(str6)) / 100.0;
                            CallBLL.cz_odds_pkbjl_bll.Agent_UpdateGrandTotal(num6.ToString(), odds_id);
                            double num7 = double.Parse(playOddsInfo.Tables[0].Rows[0]["grand_total"].ToString()) + num6;
                            double num8 = double.Parse(playOddsInfo.Tables[0].Rows[0]["downbase"].ToString());
                            double num9 = Math.Floor((double) (num7 / num8));
                            if ((num9 > 1.0) && (num8 >= 1.0))
                            {
                                double num10 = double.Parse(playOddsInfo.Tables[0].Rows[0]["down_odds_rate"].ToString()) * num9;
                                CallBLL.cz_odds_pkbjl_bll.UpdateGrandTotalCurrentOdds(num10.ToString(), (num7 - (num8 * num9)).ToString(), odds_id.ToString());
                                cz_system_log _log = new cz_system_log();
                                _log.set_user_name("系統");
                                _log.set_children_name("");
                                _log.set_category(playOddsInfo.Tables[0].Rows[0]["category"].ToString());
                                _log.set_play_name(str2);
                                _log.set_put_amount(str3);
                                _log.set_l_name(LSKeys.get_PKBJL_Name());
                                _log.set_l_phase(phase);
                                _log.set_action("降賠率");
                                _log.set_odds_id(int.Parse(odds_id));
                                string str7 = playOddsInfo.Tables[0].Rows[0]["current_odds"].ToString();
                                _log.set_old_val(str7);
                                _log.set_new_val((double.Parse(str7) - num10).ToString());
                                _log.set_ip(LSRequest.GetIP());
                                _log.set_add_time(DateTime.Now);
                                _log.set_note("系統自動降賠");
                                _log.set_type_id(Convert.ToInt32((LSEnums.LogTypeID) 0));
                                _log.set_lottery_type(8);
                                CallBLL.cz_system_log_bll.Add(_log);
                                cz_jp_odds _odds = new cz_jp_odds();
                                _odds.set_add_time(DateTime.Now);
                                _odds.set_odds_id(int.Parse(odds_id));
                                _odds.set_phase_id(int.Parse(phase_id));
                                _odds.set_play_name(str2);
                                _odds.set_put_amount(str3);
                                _odds.set_odds(num10.ToString());
                                _odds.set_lottery_type(8);
                                _odds.set_phase(phase);
                                _odds.set_old_odds(str7);
                                _odds.set_new_odds((double.Parse(str7) - num10).ToString());
                                CallBLL.cz_jp_odds_bll.Add(_odds);
                            }
                        }
                    }
                }
            }
        }

        private void GDBH(agent_userinfo_session userModel, agent_kc_rate kc_rate, string kc_kind, HttpContext context, string phase_id, string phase, string play_id, string odds_id, string zj_ds, string fgs_ds, string gd_ds, string zd_ds, string dl_ds, string hy_ds, string checkcode, string numTable, string playtype)
        {
            if (!string.IsNullOrEmpty(kc_rate.get_gdname()))
            {
                DataTable isAllowSale = CallBLL.cz_users_bll.GetIsAllowSale(kc_rate.get_gdname());
                if ((isAllowSale.Rows[0]["kc_allow_sale"] != null) && (Convert.ToInt32(isAllowSale.Rows[0]["kc_allow_sale"]) != 0))
                {
                    double num = 1.0;
                    DataTable autosale = CallBLL.cz_autosale_pkbjl_bll.GetAutosale(kc_rate.get_gdname(), play_id);
                    if (((autosale != null) && (autosale.Rows.Count > 0)) && autosale.Rows[0]["sale_type"].ToString().Equals("0"))
                    {
                        DataTable table3 = CallBLL.cz_bet_kc_bll.GetSZZTBHByOddsid("gd", kc_rate.get_gdname(), odds_id, phase_id, 8);
                        if ((double.Parse(table3.Rows[0]["szzt"].ToString()) - num) > double.Parse(autosale.Rows[0]["max_money"].ToString()))
                        {
                            double num2 = double.Parse(table3.Rows[0]["szzt"].ToString());
                            double num3 = double.Parse(autosale.Rows[0]["max_money"].ToString());
                            double num4 = Math.Floor((double) (num2 - num3));
                            DataSet playOddsInfo = CallBLL.cz_play_pkbjl_bll.GetPlayOddsInfo(odds_id);
                            string s = playOddsInfo.Tables[0].Rows[0]["current_odds"].ToString().Trim();
                            string str2 = playOddsInfo.Tables[0].Rows[0]["play_name"].ToString();
                            string str3 = playOddsInfo.Tables[0].Rows[0]["put_amount"].ToString();
                            string str4 = playOddsInfo.Tables[0].Rows[0][kc_kind.ToUpper() + "_diff"].ToString().Trim();
                            string str5 = (double.Parse(s) + double.Parse(str4)).ToString();
                            base.GetOdds_KC_EX(8, playOddsInfo.Tables[0].Rows[0]["current_odds"].ToString().Trim(), str4, ref s, odds_id);
                            int num15 = 0;
                            if (playtype.Equals(num15.ToString()))
                            {
                                str5 = Utils.GetPKBJLPlaytypeOdds(odds_id, double.Parse(str5), base.GetPlayTypeWTValue_PKBJL(odds_id)).ToString();
                                s = Utils.GetPKBJLPlaytypeOdds(odds_id, double.Parse(s), base.GetPlayTypeWTValue_PKBJL(odds_id)).ToString();
                            }
                            zd_ds = gd_ds;
                            string str6 = kc_rate.get_zjzc();
                            string str7 = kc_rate.get_fgszc();
                            if (kc_rate.get_zcyg() == "1")
                            {
                                str6 = (100.0 - double.Parse(kc_rate.get_fgszc())).ToString();
                            }
                            else
                            {
                                str7 = (100.0 - double.Parse(kc_rate.get_zjzc())).ToString();
                            }
                            cz_bet_kc _kc = new cz_bet_kc();
                            _kc.set_order_num(Utils.GetOrderNumber());
                            _kc.set_checkcode(checkcode);
                            _kc.set_u_name(kc_rate.get_gdname());
                            _kc.set_u_nicker("");
                            _kc.set_phase_id(new int?(int.Parse(phase_id)));
                            _kc.set_phase(phase);
                            _kc.set_bet_time(new DateTime?(DateTime.Now));
                            _kc.set_odds_id(new int?(int.Parse(odds_id)));
                            _kc.set_category(playOddsInfo.Tables[0].Rows[0]["category"].ToString());
                            _kc.set_play_id(new int?(int.Parse(play_id)));
                            _kc.set_play_name(str2);
                            _kc.set_bet_val(str3);
                            _kc.set_bet_wt("");
                            _kc.set_odds(s);
                            _kc.set_amount(new decimal?(decimal.Parse(num4.ToString())));
                            _kc.set_profit(0);
                            _kc.set_hy_drawback(0);
                            _kc.set_dl_drawback(0);
                            _kc.set_zd_drawback(new decimal?(decimal.Parse(zd_ds)));
                            _kc.set_gd_drawback(new decimal?(decimal.Parse(gd_ds)));
                            _kc.set_fgs_drawback(new decimal?(decimal.Parse(fgs_ds)));
                            _kc.set_zj_drawback(new decimal?(decimal.Parse(zj_ds)));
                            _kc.set_dl_rate(0f);
                            _kc.set_zd_rate(0f);
                            _kc.set_gd_rate(0f);
                            _kc.set_fgs_rate((float) int.Parse(str7));
                            _kc.set_zj_rate((float) int.Parse(str6));
                            _kc.set_dl_name("");
                            _kc.set_zd_name("");
                            _kc.set_gd_name(kc_rate.get_gdname());
                            _kc.set_fgs_name(kc_rate.get_fgsname());
                            _kc.set_is_payment(0);
                            _kc.set_sale_type(1);
                            _kc.set_m_type(-2);
                            _kc.set_kind(kc_kind);
                            _kc.set_ip(LSRequest.GetIP());
                            _kc.set_lottery_type(8);
                            _kc.set_lottery_name(base.GetGameNameByID(_kc.get_lottery_type().ToString()));
                            _kc.set_ordervalidcode(Utils.GetOrderValidCode(_kc.get_gd_name(), _kc.get_order_num(), _kc.get_bet_val(), _kc.get_odds(), _kc.get_kind(), Convert.ToInt32(_kc.get_phase_id()), Convert.ToInt32(_kc.get_odds_id()), Convert.ToDouble(_kc.get_amount())));
                            _kc.set_odds_zj(str5);
                            _kc.set_table_type(new int?(int.Parse(numTable)));
                            _kc.set_play_type(new int?(int.Parse(playtype)));
                            int num7 = 0;
                            CallBLL.cz_bet_kc_bll.AddBetByBH(_kc, ref num7);
                            double num8 = (num4 * double.Parse(str6)) / 100.0;
                            CallBLL.cz_odds_pkbjl_bll.Agent_UpdateGrandTotal(num8.ToString(), odds_id);
                            double num9 = double.Parse(playOddsInfo.Tables[0].Rows[0]["grand_total"].ToString()) + num8;
                            double num10 = double.Parse(playOddsInfo.Tables[0].Rows[0]["downbase"].ToString());
                            double num11 = Math.Floor((double) (num9 / num10));
                            if ((num11 > 1.0) && (num10 >= 1.0))
                            {
                                double num12 = double.Parse(playOddsInfo.Tables[0].Rows[0]["down_odds_rate"].ToString()) * num11;
                                CallBLL.cz_odds_pkbjl_bll.UpdateGrandTotalCurrentOdds(num12.ToString(), (num9 - (num10 * num11)).ToString(), odds_id.ToString());
                                cz_system_log _log = new cz_system_log();
                                _log.set_user_name("系統");
                                _log.set_children_name("");
                                _log.set_category(playOddsInfo.Tables[0].Rows[0]["category"].ToString());
                                _log.set_play_name(str2);
                                _log.set_put_amount(str3);
                                _log.set_l_name(LSKeys.get_PKBJL_Name());
                                _log.set_l_phase(phase);
                                _log.set_action("降賠率");
                                _log.set_odds_id(int.Parse(odds_id));
                                string str8 = playOddsInfo.Tables[0].Rows[0]["current_odds"].ToString();
                                _log.set_old_val(str8);
                                _log.set_new_val((double.Parse(str8) - num12).ToString());
                                _log.set_ip(LSRequest.GetIP());
                                _log.set_add_time(DateTime.Now);
                                _log.set_note("系統自動降賠");
                                _log.set_type_id(Convert.ToInt32((LSEnums.LogTypeID) 0));
                                _log.set_lottery_type(8);
                                CallBLL.cz_system_log_bll.Add(_log);
                                cz_jp_odds _odds = new cz_jp_odds();
                                _odds.set_add_time(DateTime.Now);
                                _odds.set_odds_id(int.Parse(odds_id));
                                _odds.set_phase_id(int.Parse(phase_id));
                                _odds.set_play_name(str2);
                                _odds.set_put_amount(str3);
                                _odds.set_odds(num12.ToString());
                                _odds.set_lottery_type(8);
                                _odds.set_phase(phase);
                                _odds.set_old_odds(str8);
                                _odds.set_new_odds((double.Parse(str8) - num12).ToString());
                                CallBLL.cz_jp_odds_bll.Add(_odds);
                            }
                        }
                    }
                }
            }
        }

        private void get_HeadParam(HttpContext context, string p_id, ref string porkList, ref string player, ref string banker, ref string porkIndex, ref string noticeIndex)
        {
            if (context.Session["user_name"] == null)
            {
                context.Response.End();
            }
            string str2 = Utils.GetPKBJL_NumTable(LSRequest.qq("playpage"));
            cz_phase_pkbjl tenPoker = CallBLL.cz_phase_pkbjl_bll.GetTenPoker(p_id, str2);
            porkList = (tenPoker != null) ? tenPoker.get_ten_poker() : "";
            cz_phase_pkbjl openPhaseByNumTable = CallBLL.cz_phase_pkbjl_bll.GetOpenPhaseByNumTable(str2);
            player = (openPhaseByNumTable != null) ? openPhaseByNumTable.get_xian_nn() : "";
            banker = (openPhaseByNumTable != null) ? openPhaseByNumTable.get_zhuang_nn() : "";
        }

        public void get_oddsinfo(HttpContext context, ref string strResult)
        {
            string str10;
            int num8;
            string str = context.Session["user_name"].ToString();
            agent_userinfo_session model = context.Session[str + "lottery_session_user_info"] as agent_userinfo_session;
            string str2 = base.Permission_Ashx_ZJ(model, "po_1_1");
            string str3 = base.Permission_Ashx_DL(model, "po_5_1");
            if (!string.IsNullOrEmpty(str2) || !string.IsNullOrEmpty(str3))
            {
                context.Response.Write(PageBase.GetMessageByCache("u100027", "MessageHint"));
                context.Response.End();
            }
            string str4 = LSRequest.qq("playid");
            LSRequest.qq("numsort");
            string s = LSRequest.qq("szsz");
            string str6 = LSRequest.qq("pk");
            string str7 = LSRequest.qq("playpage");
            string str8 = Utils.GetPKBJL_NumTable(str7);
            if (string.IsNullOrEmpty(str8))
            {
                context.Response.End();
            }
            else
            {
                this.Session["PKBJL_TableType_Record_Current_Agent"] = str7;
            }
            string str9 = LSRequest.qq("playtype");
            if (!string.IsNullOrEmpty(str9))
            {
                num8 = 0;
                if (str9 != num8.ToString())
                {
                    num8 = 1;
                    if (str9 != num8.ToString())
                    {
                        num8 = 2;
                        if (str9 != num8.ToString())
                        {
                            context.Response.End();
                            goto Label_0168;
                        }
                    }
                }
                this.Session["PKBJL_PlayType_Record_Current_Agent"] = str9;
            }
            else
            {
                num8 = 2;
                str9 = num8.ToString();
            }
        Label_0168:
            str10 = LSRequest.qq("isbh");
            int num = 0;
            if (!string.IsNullOrEmpty(LSRequest.qq("oldId")))
            {
                num = Convert.ToInt32(LSRequest.qq("oldId"));
            }
            int num2 = num;
            if (string.IsNullOrEmpty(str4))
            {
                context.Response.End();
            }
            else if (context.Session["user_name"] == null)
            {
                context.Response.End();
            }
            else
            {
                string str11 = model.get_u_type().Trim().ToString();
                string str12 = model.get_u_name().ToString();
                string str13 = str6;
                if (string.IsNullOrEmpty(str13))
                {
                    str13 = "A";
                }
                ReturnResult result = new ReturnResult();
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                dictionary.Add("type", "get_oddsinfo");
                dictionary.Add("playpage", str7);
                DataSet set = CallBLL.cz_play_pkbjl_bll.Agent_GetPlay(str4, str12, str11, str8);
                if (set == null)
                {
                    context.Response.End();
                }
                else
                {
                    DataTable table = set.Tables["phase"];
                    DataTable oddsDT = set.Tables["odds"];
                    DataTable table1 = set.Tables["wtTable"];
                    DataTable table3 = set.Tables["aboveTable"];
                    if ((table == null) || (oddsDT == null))
                    {
                        context.Response.End();
                    }
                    else
                    {
                        int? nullable;
                        DataRow row = table.Rows[0];
                        string str14 = row["openning"].ToString();
                        string str15 = row["isopen"].ToString();
                        string str16 = row["opendate"].ToString();
                        string str17 = (row["endtime"].ToString() != "") ? Convert.ToDateTime(row["endtime"].ToString()).ToString("HH:mm:ss") : row["endtime"].ToString();
                        string str18 = row["nn"].ToString();
                        string str19 = row["p_id"].ToString();
                        string str20 = row["upopenphase"].ToString();
                        string str21 = row["upopennumber"].ToString();
                        string str22 = row["upopentennumber"].ToString();
                        dictionary.Add("openning", str14);
                        dictionary.Add("isopen", str15);
                        dictionary.Add("drawopen_time", str16);
                        dictionary.Add("stop_time", str17);
                        dictionary.Add("nn", str18);
                        dictionary.Add("p_id", str19);
                        dictionary.Add("profit", base.GetKCProfit());
                        dictionary.Add("upopenphase", str20);
                        dictionary.Add("upopennumber", str21);
                        dictionary.Add("jeucode", base.get_JeuValidate());
                        string porkList = "";
                        string player = "";
                        string banker = "";
                        string porkIndex = "";
                        string noticeIndex = "";
                        this.get_HeadParam(context, str19, ref porkList, ref player, ref banker, ref porkIndex, ref noticeIndex);
                        dictionary.Add("porkList", str22);
                        dictionary.Add("openNumberPorkIndex", porkIndex);
                        dictionary.Add("noticeIndex", noticeIndex);
                        Dictionary<string, string> dictionary2 = new Dictionary<string, string>();
                        dictionary2.Add("player", player);
                        dictionary2.Add("banker", banker);
                        dictionary.Add("lastResult", dictionary2);
                        Dictionary<string, object> dictionary3 = base.GetOdds_KC(8, oddsDT, str13, str14, str9);
                        dictionary.Add("play_odds", dictionary3);
                        Dictionary<string, object> dictionary4 = new Dictionary<string, object>();
                        string str28 = "";
                        new Dictionary<string, object>();
                        string str29 = "";
                        if (model.get_u_type() == "fgs")
                        {
                            nullable = model.get_allow_view_report();
                            if (((nullable.GetValueOrDefault() == 1) && nullable.HasValue) && int.Parse(s).Equals(2))
                            {
                                str11 = "zj";
                                str12 = model.get_zjname().ToString();
                                s = "1";
                            }
                        }
                        DataSet set2 = CallBLL.cz_bet_kc_bll.GetSZSZ(str11, str12, int.Parse(s), Convert.ToInt32(str19), 8, str4, str29, model.get_zjname().ToString(), num, "pkbjl", str6, str8, str9);
                        DataTable table4 = null;
                        if (str10.Equals("1"))
                        {
                            num8 = 8;
                            table4 = CallBLL.cz_bet_kc_bll.GetBHSZZT(str12, str11, str19, str29, str4, model.get_zjname().ToString(), num, num8.ToString(), "pkbjl", str6, str8, str9).Tables[0];
                        }
                        if ((set2 != null) && (set2.Tables.Count > 0))
                        {
                            DataTable table5 = set2.Tables[0];
                            foreach (DataRow row2 in table5.Rows)
                            {
                                string str30 = "0";
                                if (!row2["bs"].ToString().Equals("0"))
                                {
                                    if (str10.Equals("1"))
                                    {
                                        foreach (DataRow row3 in table4.Select(string.Format("odds_id={0}", Convert.ToInt32(row2["odds_id"].ToString()))))
                                        {
                                            str30 = row3["szzt"].ToString();
                                        }
                                    }
                                    string key = (row2["odds_id"].ToString().Length == 0) ? ("0" + row2["odds_id"].ToString()) : row2["odds_id"].ToString();
                                    string[] strArray = new string[9];
                                    strArray[0] = row2["bs"].ToString();
                                    strArray[1] = ",";
                                    nullable = null;
                                    strArray[2] = Utils.GetMathRound(double.Parse(row2["szzt"].ToString()), nullable);
                                    strArray[3] = ",";
                                    nullable = null;
                                    strArray[4] = Utils.GetMathRound(double.Parse(row2["ky"].ToString()), nullable);
                                    strArray[5] = ",";
                                    nullable = null;
                                    strArray[6] = Utils.GetMathRound(double.Parse(row2["ds"].ToString()), nullable);
                                    strArray[7] = ",";
                                    nullable = null;
                                    strArray[8] = Utils.GetMathRound(double.Parse(str30), nullable);
                                    str28 = string.Concat(strArray);
                                    dictionary4.Add(key, str28);
                                    if (Convert.ToInt32(row2["max_id"]) > num)
                                    {
                                        num = Convert.ToInt32(row2["max_id"]);
                                    }
                                }
                            }
                        }
                        dictionary.Add("szsz_amount", dictionary4);
                        Dictionary<string, object> dictionary5 = new Dictionary<string, object>();
                        DataSet set4 = CallBLL.cz_bet_kc_bll.GetSZSZCount(str11, str12, int.Parse(s), Convert.ToInt32(str19), 8, "pkbjl", str6, str8, str9);
                        if ((set4 != null) && (set4.Tables.Count > 0))
                        {
                            DataTable table6 = set4.Tables[0];
                            double d = 0.0;
                            double num4 = 0.0;
                            double num5 = 0.0;
                            double num6 = 0.0;
                            for (int i = 0; i < table6.Rows.Count; i++)
                            {
                                string str32 = table6.Rows[i]["play_id"].ToString();
                                string str33 = table6.Rows[i]["play_name"].ToString();
                                if (str32.Equals("81001") && str33.Equals("大小"))
                                {
                                    d += double.Parse(table6.Rows[i]["szzt"].ToString());
                                }
                                if (str32.Equals("81002") && str33.Equals("莊對"))
                                {
                                    num4 += double.Parse(table6.Rows[i]["szzt"].ToString());
                                }
                                if (str32.Equals("81003") && str33.Equals("閑對"))
                                {
                                    num5 += double.Parse(table6.Rows[i]["szzt"].ToString());
                                }
                                if (str32.Equals("81004") && str33.Equals("莊閑和"))
                                {
                                    num6 += double.Parse(table6.Rows[i]["szzt"].ToString());
                                }
                            }
                            dictionary5.Add("81001", Math.Floor(d));
                            dictionary5.Add("81002", Math.Floor(num4));
                            dictionary5.Add("81003", Math.Floor(num5));
                            dictionary5.Add("81004", Math.Floor(num6));
                        }
                        dictionary.Add("szsz_amount_count", dictionary5);
                        Dictionary<string, object> dictionary6 = new Dictionary<string, object>();
                        if (table3 != null)
                        {
                            foreach (DataRow row4 in table3.Rows)
                            {
                                dictionary6.Add(row4["play_id"].ToString(), "-" + row4["above_quota"].ToString());
                            }
                        }
                        dictionary.Add("abovevalid", dictionary6);
                        new Dictionary<string, object>();
                        dictionary.Add("maxidvalid", num);
                        if (num2.Equals(0))
                        {
                            dictionary.Add("cleardata", "0");
                        }
                        else
                        {
                            dictionary.Add("cleardata", "1");
                        }
                        result.set_data(dictionary);
                        strResult = JsonHandle.ObjectToJson(result);
                    }
                }
            }
        }

        public void get_opennumber(HttpContext context, ref string strResult)
        {
            ReturnResult result = new ReturnResult();
            result.set_success(400);
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("type", "get_opennumber");
            string str = context.Session["user_name"].ToString();
            agent_userinfo_session sessionInfo = context.Session[str + "lottery_session_user_info"] as agent_userinfo_session;
            if (RuleJudge.ChildOperateValid(sessionInfo, "po_1_1", context))
            {
                string numTable = Utils.GetPKBJL_NumTable(LSRequest.qq("playpage"));
                switch (numTable)
                {
                    case null:
                    case "":
                        context.Response.End();
                        break;
                }
                new List<object>();
                DataTable openNumber = base.GetOpenNumber(8, numTable);
                Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
                if ((openNumber != null) && (openNumber.Rows.Count > 0))
                {
                    string keepDecimalNumber = Utils.GetKeepDecimalNumber(1, base.GetKCProfit());
                    openNumber.Rows[0]["p_id"].ToString();
                    string str5 = openNumber.Rows[0]["upopenphase"].ToString();
                    string str6 = openNumber.Rows[0]["upopennumber"].ToString();
                    string str7 = openNumber.Rows[0]["upopentennumber"].ToString();
                    string str8 = openNumber.Rows[0]["upopenzhuangnumber"].ToString();
                    string str9 = openNumber.Rows[0]["upopenxiannumber"].ToString();
                    dictionary2.Add("profit", keepDecimalNumber);
                    dictionary2.Add("upopenphase", str5);
                    dictionary2.Add("upopennumber", str6);
                    dictionary2.Add("porkList", str7);
                    Dictionary<string, string> dictionary3 = new Dictionary<string, string>();
                    dictionary3.Add("player", str9);
                    dictionary3.Add("banker", str8);
                    dictionary2.Add("lastResult", dictionary3);
                    result.set_success(200);
                }
                else
                {
                    result.set_success(300);
                    result.set_tipinfo(PageBase.GetMessageByCache("u100016", "MessageHint"));
                }
                dictionary.Add("opennumber", dictionary2);
                result.set_data(dictionary);
                strResult = JsonHandle.ObjectToJson(result);
            }
        }

        public void operate_adds(HttpContext context, ref string strResult)
        {
            string str = context.Session["user_name"].ToString();
            agent_userinfo_session model = context.Session[str + "lottery_session_user_info"] as agent_userinfo_session;
            if (!model.get_u_type().Trim().Equals("zj"))
            {
                context.Response.Write(PageBase.GetMessageByCache("u100027", "MessageHint"));
                context.Response.End();
            }
            if (!string.IsNullOrEmpty(base.Permission_Ashx_ZJ(model, "po_1_2")))
            {
                context.Response.Write(PageBase.GetMessageByCache("u100027", "MessageHint"));
                context.Response.End();
            }
            string str3 = LSRequest.qq("optype");
            string str4 = LSRequest.qq("oddsid");
            LSRequest.qq("phaseid");
            string str5 = LSRequest.qq("pk");
            string str6 = LSRequest.qq("wtvalue");
            string s = LSRequest.qq("inputvalue");
            LSRequest.qq("isopen");
            if (string.IsNullOrEmpty(str5))
            {
                str5 = "A";
            }
            else
            {
                str5 = str5.ToUpper();
            }
            if ((!str5.Equals("A") && !str5.Equals("B")) && !str5.Equals("C"))
            {
                context.Response.End();
            }
            ReturnResult result = new ReturnResult();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("type", "operate_adds");
            result.set_data(dictionary);
            if (string.IsNullOrEmpty(str4))
            {
                result.set_success(400);
                result.set_tipinfo(PageBase.GetMessageByCache("u100008", "MessageHint"));
                strResult = JsonHandle.ObjectToJson(result);
            }
            else
            {
                string str13;
                double num43;
                int num44;
                string str8 = this.Session["PKBJL_PlayType_Record_Current_Agent"];
                cz_odds_pkbjl oldModel = null;
                if ((str3.Equals("1") || str3.Equals("2")) || (str3.Equals("3") || str3.Equals("4")))
                {
                    oldModel = CallBLL.cz_odds_pkbjl_bll.GetModel(Convert.ToInt32(str4));
                    if (oldModel == null)
                    {
                        result.set_success(400);
                        result.set_tipinfo(PageBase.GetMessageByCache("u100008", "MessageHint"));
                        strResult = JsonHandle.ObjectToJson(result);
                        return;
                    }
                }
                if (!str3.Equals("1") && !str3.Equals("2"))
                {
                    if (str3.Equals("3"))
                    {
                        double num12 = Convert.ToDouble(oldModel.get_max_odds());
                        double num13 = Convert.ToDouble(oldModel.get_min_odds());
                        double num14 = 0.0;
                        if (str5.Equals("A"))
                        {
                            num14 = Convert.ToDouble(oldModel.get_a_diff());
                        }
                        else if (str5.Equals("B"))
                        {
                            num14 = Convert.ToDouble(oldModel.get_b_diff());
                        }
                        else if (str5.Equals("C"))
                        {
                            num14 = Convert.ToDouble(oldModel.get_c_diff());
                        }
                        num43 = double.Parse(s) - num14;
                        s = num43.ToString();
                        if (Convert.ToDouble(s) < num13)
                        {
                            result.set_success(400);
                            num43 = num13 + num14;
                            result.set_tipinfo(string.Format(PageBase.GetMessageByCache("u100012", "MessageHint"), num43.ToString()));
                            strResult = JsonHandle.ObjectToJson(result);
                        }
                        else if (Convert.ToDouble(s) > num12)
                        {
                            result.set_success(400);
                            num43 = num12 + num14;
                            result.set_tipinfo(string.Format(PageBase.GetMessageByCache("u100010", "MessageHint"), num43.ToString()));
                            strResult = JsonHandle.ObjectToJson(result);
                        }
                        else
                        {
                            double num15 = base.GetPlayTypeWTValue_PKBJL(str4);
                            double num16 = 0.0;
                            num44 = 0;
                            if (str8.Equals(num44.ToString()))
                            {
                                if (num15 > 0.0)
                                {
                                    num16 = num15;
                                }
                                else
                                {
                                    num16 = Math.Abs(num15);
                                }
                                if ((Convert.ToDouble(s) + num16) > num12)
                                {
                                    result.set_success(400);
                                    num43 = num12 + num14;
                                    result.set_tipinfo(string.Format(PageBase.GetMessageByCache("u100097", "MessageHint"), num43.ToString()));
                                    strResult = JsonHandle.ObjectToJson(result);
                                    return;
                                }
                            }
                            else
                            {
                                if (num15 > 0.0)
                                {
                                    num16 = num15;
                                }
                                else
                                {
                                    num16 = -Math.Abs(num15);
                                }
                                if ((Convert.ToDouble(s) + num16) > num12)
                                {
                                    result.set_success(400);
                                    num43 = num12 + num14;
                                    result.set_tipinfo(string.Format(PageBase.GetMessageByCache("u100097", "MessageHint"), num43.ToString()));
                                    strResult = JsonHandle.ObjectToJson(result);
                                    return;
                                }
                            }
                            int num17 = 0;
                            num44 = 0;
                            if (str8.Equals(num44.ToString()))
                            {
                                double num18 = num15;
                                if (num15 < 0.0)
                                {
                                    num18 = -Math.Abs(num15);
                                }
                                num17 = CallBLL.cz_odds_pkbjl_bll.UpdateCurrentOdds(Convert.ToInt32(str4), Convert.ToDouble(s) - num18, str3);
                            }
                            else
                            {
                                num17 = CallBLL.cz_odds_pkbjl_bll.UpdateCurrentOdds(Convert.ToInt32(str4), Convert.ToDouble(s), str3);
                            }
                            if (num17 > 0)
                            {
                                cz_odds_pkbjl newModel = CallBLL.cz_odds_pkbjl_bll.GetModel(Convert.ToInt32(str4));
                                base.pkbjl_log(oldModel, newModel, str3);
                                double num19 = Convert.ToDouble(newModel.get_current_odds());
                                double num20 = Convert.ToDouble(newModel.get_max_odds());
                                double num21 = Convert.ToDouble(newModel.get_min_odds());
                                double num22 = 0.0;
                                if (str5.Equals("A"))
                                {
                                    num22 = Convert.ToDouble(newModel.get_a_diff());
                                }
                                else if (str5.Equals("B"))
                                {
                                    num22 = Convert.ToDouble(newModel.get_b_diff());
                                }
                                else if (str5.Equals("C"))
                                {
                                    num22 = Convert.ToDouble(newModel.get_c_diff());
                                }
                                num43 = num19 + num22;
                                num19 = double.Parse(num43.ToString());
                                num44 = 0;
                                if (str8.Equals(num44.ToString()))
                                {
                                    num19 = Utils.GetPKBJLPlaytypeOdds(str4, num19, base.GetPlayTypeWTValue_PKBJL(str4));
                                }
                                string str10 = newModel.get_is_open().ToString();
                                Dictionary<string, object> dictionary4 = new Dictionary<string, object>();
                                Dictionary<string, object> dictionary5 = new Dictionary<string, object>();
                                dictionary5.Add("pl", num19);
                                num43 = num20 + num22;
                                dictionary5.Add("maxpl", num43.ToString());
                                num43 = num21 + num22;
                                dictionary5.Add("minpl", num43.ToString());
                                dictionary5.Add("plx", new List<double>());
                                DataTable table2 = CallBLL.cz_phase_pkbjl_bll.IsPhaseClose();
                                if (table2.Rows[0]["isopen"].ToString().Equals("0") && string.IsNullOrEmpty(table2.Rows[0]["endtime"].ToString()))
                                {
                                    str10 = "0";
                                }
                                if (table2.Rows[0]["openning"].ToString().Equals("n"))
                                {
                                    str10 = "0";
                                }
                                dictionary5.Add("is_open", str10);
                                dictionary4.Add(str4, new Dictionary<string, object>(dictionary5));
                                dictionary.Add("play_odds", dictionary4);
                                result.set_data(dictionary);
                                result.set_success(200);
                                strResult = JsonHandle.ObjectToJson(result);
                            }
                            else
                            {
                                result.set_success(400);
                                result.set_tipinfo(PageBase.GetMessageByCache("u100011", "MessageHint"));
                                strResult = JsonHandle.ObjectToJson(result);
                            }
                        }
                    }
                    else
                    {
                        DataTable oldTable = null;
                        if ((str3.Equals("5") || str3.Equals("6")) || str3.Equals("7"))
                        {
                            oldTable = CallBLL.cz_odds_pkbjl_bll.GetOddsInfoByID(str4.ToString());
                        }
                        if (str3.Equals("5") || str3.Equals("6"))
                        {
                            base.Response.End();
                            if (string.IsNullOrEmpty(str6))
                            {
                                str6 = "0.01";
                            }
                            new Dictionary<string, string>();
                            new Dictionary<string, string>();
                            int num23 = 0;
                            foreach (DataRow row in oldTable.Rows)
                            {
                                double num25 = Convert.ToDouble(row["current_odds"]);
                                double num26 = Convert.ToDouble(row["max_odds"]);
                                double num27 = Convert.ToDouble(row["min_odds"]);
                                double num28 = double.Parse(str6);
                                str13 = str3;
                                if (str13 != null)
                                {
                                    if (!(str13 == "5"))
                                    {
                                        if (str13 == "6")
                                        {
                                            goto Label_0D6A;
                                        }
                                    }
                                    else
                                    {
                                        num43 = num25 + num28;
                                        num25 = double.Parse(num43.ToString());
                                    }
                                }
                                goto Label_0D80;
                            Label_0D6A:
                                num43 = num25 + -num28;
                                num25 = double.Parse(num43.ToString());
                            Label_0D80:
                                if (num25 < num27)
                                {
                                    num28 = 0.0;
                                }
                                if (num25 > num26)
                                {
                                    num28 = 0.0;
                                }
                                if (CallBLL.cz_odds_pkbjl_bll.UpdateCurrentOddsList(row["odds_id"].ToString(), str3.Equals("5") ? num28 : -num28, "5") > 0)
                                {
                                    num23++;
                                }
                            }
                            if (num23 > 0)
                            {
                                DataTable oddsInfoByID = CallBLL.cz_odds_pkbjl_bll.GetOddsInfoByID(str4);
                                base.pkbjl_kjj_log(oldTable, oddsInfoByID, str3);
                                Dictionary<string, object> dictionary6 = new Dictionary<string, object>();
                                foreach (DataRow row2 in oddsInfoByID.Rows)
                                {
                                    double num30 = Convert.ToDouble(row2["current_odds"]);
                                    double num31 = Convert.ToDouble(row2["max_odds"]);
                                    double num32 = Convert.ToDouble(row2["min_odds"]);
                                    num43 = num30 + Convert.ToDouble(row2[str5 + "_diff"]);
                                    num30 = double.Parse(num43.ToString());
                                    string str11 = row2["is_open"].ToString();
                                    Dictionary<string, object> dictionary7 = new Dictionary<string, object>();
                                    dictionary7.Add("pl", num30.ToString());
                                    num43 = num31 + Convert.ToDouble(row2[str5 + "_diff"]);
                                    dictionary7.Add("maxpl", num43.ToString());
                                    num43 = num32 + Convert.ToDouble(row2[str5 + "_diff"]);
                                    dictionary7.Add("minpl", num43.ToString());
                                    dictionary7.Add("plx", new List<double>());
                                    DataTable table5 = CallBLL.cz_phase_pkbjl_bll.IsPhaseClose();
                                    if (table5.Rows[0]["isopen"].ToString().Equals("0") && string.IsNullOrEmpty(table5.Rows[0]["endtime"].ToString()))
                                    {
                                        str11 = "0";
                                    }
                                    if (table5.Rows[0]["openning"].ToString().Equals("n"))
                                    {
                                        str11 = "0";
                                    }
                                    dictionary7.Add("is_open", str11);
                                    dictionary6.Add(row2["odds_id"].ToString(), new Dictionary<string, object>(dictionary7));
                                }
                                dictionary.Add("play_odds", dictionary6);
                                result.set_data(dictionary);
                                result.set_success(200);
                                strResult = JsonHandle.ObjectToJson(result);
                            }
                            else
                            {
                                result.set_success(400);
                                result.set_tipinfo(PageBase.GetMessageByCache("u100011", "MessageHint"));
                                strResult = JsonHandle.ObjectToJson(result);
                            }
                        }
                        else if (str3.Equals("7"))
                        {
                            base.Response.End();
                            new Dictionary<string, string>();
                            new Dictionary<string, string>();
                            int num33 = 0;
                            foreach (DataRow row3 in oldTable.Rows)
                            {
                                double num34 = Convert.ToDouble(row3["current_odds"]);
                                double num35 = Convert.ToDouble(row3["max_odds"]);
                                double num36 = Convert.ToDouble(row3["min_odds"]);
                                double num37 = double.Parse(row3[str5 + "_diff"].ToString());
                                num43 = double.Parse(s) - num37;
                                double num38 = double.Parse(num43.ToString());
                                if (Convert.ToDouble(s) < num36)
                                {
                                    num38 = num36;
                                }
                                if (Convert.ToDouble(s) > num35)
                                {
                                    num38 = num35;
                                }
                                if (CallBLL.cz_odds_pkbjl_bll.UpdateCurrentOddsList(row3["odds_id"].ToString(), num38, "7") > 0)
                                {
                                    num33++;
                                }
                            }
                            if (num33 > 0)
                            {
                                DataTable newTable = CallBLL.cz_odds_pkbjl_bll.GetOddsInfoByID(str4);
                                base.pkbjl_kjj_log(oldTable, newTable, str3);
                                Dictionary<string, object> dictionary8 = new Dictionary<string, object>();
                                foreach (DataRow row4 in newTable.Rows)
                                {
                                    double num40 = Convert.ToDouble(row4["current_odds"]);
                                    double num41 = Convert.ToDouble(row4["max_odds"]);
                                    double num42 = Convert.ToDouble(row4["min_odds"]);
                                    num43 = num40 + Convert.ToDouble(row4[str5 + "_diff"]);
                                    num40 = double.Parse(num43.ToString());
                                    string str12 = row4["is_open"].ToString();
                                    Dictionary<string, object> dictionary9 = new Dictionary<string, object>();
                                    dictionary9.Add("pl", num40.ToString());
                                    num43 = num41 + Convert.ToDouble(row4[str5 + "_diff"]);
                                    dictionary9.Add("maxpl", num43.ToString());
                                    num43 = num42 + Convert.ToDouble(row4[str5 + "_diff"]);
                                    dictionary9.Add("minpl", num43.ToString());
                                    dictionary9.Add("plx", new List<double>());
                                    DataTable table7 = CallBLL.cz_phase_pkbjl_bll.IsPhaseClose();
                                    if (table7.Rows[0]["isopen"].ToString().Equals("0") && string.IsNullOrEmpty(table7.Rows[0]["endtime"].ToString()))
                                    {
                                        str12 = "0";
                                    }
                                    if (table7.Rows[0]["openning"].ToString().Equals("n"))
                                    {
                                        str12 = "0";
                                    }
                                    dictionary9.Add("is_open", str12);
                                    dictionary8.Add(row4["odds_id"].ToString(), new Dictionary<string, object>(dictionary9));
                                }
                                dictionary.Add("play_odds", dictionary8);
                                result.set_data(dictionary);
                                result.set_success(200);
                                strResult = JsonHandle.ObjectToJson(result);
                            }
                            else
                            {
                                result.set_success(400);
                                result.set_tipinfo(PageBase.GetMessageByCache("u100011", "MessageHint"));
                                strResult = JsonHandle.ObjectToJson(result);
                            }
                        }
                    }
                }
                else
                {
                    double num2 = Convert.ToDouble(oldModel.get_current_odds());
                    double num3 = Convert.ToDouble(oldModel.get_max_odds());
                    double num4 = Convert.ToDouble(oldModel.get_min_odds());
                    double num5 = 0.0;
                    if (str5.Equals("A"))
                    {
                        num5 = Convert.ToDouble(oldModel.get_a_diff());
                    }
                    else if (str5.Equals("B"))
                    {
                        num5 = Convert.ToDouble(oldModel.get_b_diff());
                    }
                    else if (str5.Equals("C"))
                    {
                        num5 = Convert.ToDouble(oldModel.get_c_diff());
                    }
                    if (string.IsNullOrEmpty(str6))
                    {
                        str6 = "0.01";
                    }
                    str13 = str3;
                    if (str13 != null)
                    {
                        if (!(str13 == "1"))
                        {
                            if (str13 == "2")
                            {
                                num43 = num2 + -Convert.ToDouble(str6);
                                num2 = double.Parse(num43.ToString());
                            }
                        }
                        else
                        {
                            num43 = num2 + Convert.ToDouble(str6);
                            num2 = double.Parse(num43.ToString());
                        }
                    }
                    if (num2 < num4)
                    {
                        result.set_success(400);
                        num43 = num4 + num5;
                        result.set_tipinfo(string.Format(PageBase.GetMessageByCache("u100012", "MessageHint"), num43.ToString()));
                        strResult = JsonHandle.ObjectToJson(result);
                    }
                    else if (num2 > num3)
                    {
                        result.set_success(400);
                        num43 = num3 + num5;
                        result.set_tipinfo(string.Format(PageBase.GetMessageByCache("u100010", "MessageHint"), num43.ToString()));
                        strResult = JsonHandle.ObjectToJson(result);
                    }
                    else
                    {
                        double num6 = base.GetPlayTypeWTValue_PKBJL(str4);
                        if ((num2 + num6) > num3)
                        {
                            result.set_success(400);
                            num43 = num3 + num5;
                            result.set_tipinfo(string.Format(PageBase.GetMessageByCache("u100097", "MessageHint"), num43.ToString()));
                            strResult = JsonHandle.ObjectToJson(result);
                        }
                        else if (CallBLL.cz_odds_pkbjl_bll.UpdateCurrentOdds(Convert.ToInt32(str4), str3.Equals("1") ? Convert.ToDouble(str6) : -Convert.ToDouble(str6), str3) > 0)
                        {
                            cz_odds_pkbjl _pkbjl2 = CallBLL.cz_odds_pkbjl_bll.GetModel(Convert.ToInt32(str4));
                            base.pkbjl_log(oldModel, _pkbjl2, str3);
                            double num8 = Convert.ToDouble(_pkbjl2.get_current_odds());
                            double num9 = Convert.ToDouble(_pkbjl2.get_max_odds());
                            double num10 = Convert.ToDouble(_pkbjl2.get_min_odds());
                            double num11 = 0.0;
                            if (str5.Equals("A"))
                            {
                                num11 = Convert.ToDouble(_pkbjl2.get_a_diff());
                            }
                            else if (str5.Equals("B"))
                            {
                                num11 = Convert.ToDouble(_pkbjl2.get_b_diff());
                            }
                            else if (str5.Equals("C"))
                            {
                                num11 = Convert.ToDouble(_pkbjl2.get_c_diff());
                            }
                            num43 = num8 + num11;
                            num8 = double.Parse(num43.ToString());
                            num44 = 0;
                            if (str8.Equals(num44.ToString()))
                            {
                                num8 = Utils.GetPKBJLPlaytypeOdds(str4, num8, base.GetPlayTypeWTValue_PKBJL(str4));
                            }
                            string str9 = _pkbjl2.get_is_open().ToString();
                            Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
                            Dictionary<string, object> dictionary3 = new Dictionary<string, object>();
                            dictionary3.Add("pl", num8.ToString());
                            num43 = num9 + num11;
                            dictionary3.Add("maxpl", num43.ToString());
                            dictionary3.Add("minpl", (num10 + num11).ToString());
                            dictionary3.Add("plx", new List<double>());
                            DataTable table = CallBLL.cz_phase_pkbjl_bll.IsPhaseClose();
                            if (table.Rows[0]["isopen"].ToString().Equals("0") && string.IsNullOrEmpty(table.Rows[0]["endtime"].ToString()))
                            {
                                str9 = "0";
                            }
                            if (table.Rows[0]["openning"].ToString().Equals("n"))
                            {
                                str9 = "0";
                            }
                            dictionary3.Add("is_open", str9);
                            dictionary2.Add(str4, new Dictionary<string, object>(dictionary3));
                            dictionary.Add("play_odds", dictionary2);
                            result.set_data(dictionary);
                            result.set_success(200);
                            strResult = JsonHandle.ObjectToJson(result);
                        }
                        else
                        {
                            result.set_success(400);
                            result.set_tipinfo(PageBase.GetMessageByCache("u100011", "MessageHint"));
                            strResult = JsonHandle.ObjectToJson(result);
                        }
                    }
                }
            }
        }

        public void operate_CloseOpen(HttpContext context, ref string strResult)
        {
            string str = context.Session["user_name"].ToString();
            agent_userinfo_session model = context.Session[str + "lottery_session_user_info"] as agent_userinfo_session;
            if (!model.get_u_type().Trim().Equals("zj"))
            {
                context.Response.Write(PageBase.GetMessageByCache("u100027", "MessageHint"));
                context.Response.End();
            }
            if (!string.IsNullOrEmpty(base.Permission_Ashx_ZJ(model, "po_1_2")))
            {
                context.Response.Write(PageBase.GetMessageByCache("u100027", "MessageHint"));
                context.Response.End();
            }
            string str3 = LSRequest.qq("optype");
            string str4 = LSRequest.qq("oddsid");
            LSRequest.qq("phaseid");
            LSRequest.qq("pk");
            LSRequest.qq("wtvalue");
            LSRequest.qq("inputvalue");
            string str5 = LSRequest.qq("isopen");
            string str6 = LSRequest.qq("playid");
            ReturnResult result = new ReturnResult();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("type", "operate_closeopen");
            result.set_data(dictionary);
            if (str3.Equals("4"))
            {
                if (string.IsNullOrEmpty(str4))
                {
                    result.set_success(400);
                    result.set_tipinfo(PageBase.GetMessageByCache("u100008", "MessageHint"));
                    strResult = JsonHandle.ObjectToJson(result);
                }
                else
                {
                    DataTable table = CallBLL.cz_phase_pkbjl_bll.IsPhaseClose();
                    if (table.Rows[0]["isopen"].ToString().Equals("0") && string.IsNullOrEmpty(table.Rows[0]["endtime"].ToString()))
                    {
                        result.set_success(400);
                        result.set_tipinfo(PageBase.GetMessageByCache("u100015", "MessageHint"));
                        strResult = JsonHandle.ObjectToJson(result);
                    }
                    else if (table.Rows[0]["openning"].ToString().Equals("n"))
                    {
                        result.set_success(400);
                        result.set_tipinfo(PageBase.GetMessageByCache("u100015", "MessageHint"));
                        strResult = JsonHandle.ObjectToJson(result);
                    }
                    else
                    {
                        int num = CallBLL.cz_odds_pkbjl_bll.UpdateIsOpen(Convert.ToInt32(str4));
                        result.set_success(200);
                        if (num > 0)
                        {
                            base.kc_openclose_log(str4, str3, str5, 8, LSRequest.qq("playpage"));
                            strResult = JsonHandle.ObjectToJson(result);
                        }
                        else
                        {
                            result.set_success(400);
                            result.set_tipinfo(PageBase.GetMessageByCache("u100009", "MessageHint"));
                            strResult = JsonHandle.ObjectToJson(result);
                        }
                    }
                }
            }
            else if (str3.Equals("8"))
            {
                if (string.IsNullOrEmpty(str5) || string.IsNullOrEmpty(str4))
                {
                    result.set_success(400);
                    result.set_tipinfo(PageBase.GetMessageByCache("u100008", "MessageHint"));
                    strResult = JsonHandle.ObjectToJson(result);
                }
                else
                {
                    DataTable table2 = CallBLL.cz_phase_pkbjl_bll.IsPhaseClose();
                    if (table2.Rows[0]["isopen"].ToString().Equals("0") && string.IsNullOrEmpty(table2.Rows[0]["endtime"].ToString()))
                    {
                        result.set_success(400);
                        result.set_tipinfo(PageBase.GetMessageByCache("u100015", "MessageHint"));
                        strResult = JsonHandle.ObjectToJson(result);
                    }
                    else if (table2.Rows[0]["openning"].ToString().Equals("n"))
                    {
                        result.set_success(400);
                        result.set_tipinfo(PageBase.GetMessageByCache("u100015", "MessageHint"));
                        strResult = JsonHandle.ObjectToJson(result);
                    }
                    else
                    {
                        int num2 = CallBLL.cz_odds_pkbjl_bll.UpdateIsOpenList(str4, Convert.ToInt32(str5));
                        result.set_success(200);
                        if (num2 > 0)
                        {
                            base.kc_openclose_log(str4, str3, str5, 8, LSRequest.qq("playpage"));
                            strResult = JsonHandle.ObjectToJson(result);
                        }
                        else
                        {
                            result.set_success(400);
                            result.set_tipinfo(string.Format(PageBase.GetMessageByCache("u100025", "MessageHint"), str5.Equals("0") ? "禁用" : "啟用"));
                            strResult = JsonHandle.ObjectToJson(result);
                        }
                    }
                }
            }
            else if (str3.Equals("61") || str3.Equals("62"))
            {
                if (string.IsNullOrEmpty(str5))
                {
                    result.set_success(400);
                    result.set_tipinfo(PageBase.GetMessageByCache("u100008", "MessageHint"));
                    strResult = JsonHandle.ObjectToJson(result);
                }
                else
                {
                    int num3 = CallBLL.cz_odds_pkbjl_bll.UpdateIsOpenList(str3, str6, str4, str5);
                    result.set_success(200);
                    if (num3 > 0)
                    {
                        base.kc_openclose_log(str4, str3, str5, 8, LSRequest.qq("playpage"));
                        strResult = JsonHandle.ObjectToJson(result);
                    }
                    else
                    {
                        result.set_success(400);
                        result.set_tipinfo(string.Format(PageBase.GetMessageByCache("u100025", "MessageHint"), str5.Equals("0") ? "禁用" : "啟用"));
                        strResult = JsonHandle.ObjectToJson(result);
                    }
                }
            }
        }

        public void operate_odds_wt(HttpContext context, ref string strResult)
        {
            string str = context.Session["user_name"].ToString();
            agent_userinfo_session model = context.Session[str + "lottery_session_user_info"] as agent_userinfo_session;
            if (!string.IsNullOrEmpty(base.Permission_Ashx_DL(model, "po_5_3")))
            {
                context.Response.Write(PageBase.GetMessageByCache("u100027", "MessageHint"));
                context.Response.End();
            }
            if (!model.get_kc_op_odds().Equals(1))
            {
                context.Response.Write(PageBase.GetMessageByCache("u100027", "MessageHint"));
                context.Response.End();
            }
            string str3 = LSRequest.qq("optype");
            string str4 = LSRequest.qq("oddsid");
            LSRequest.qq("phaseid");
            string str5 = LSRequest.qq("pk");
            string str6 = LSRequest.qq("wtvalue");
            string s = LSRequest.qq("inputvalue");
            LSRequest.qq("isopen");
            string str8 = str5;
            if (string.IsNullOrEmpty(str8))
            {
                str8 = "A";
            }
            else
            {
                str8 = str8.ToUpper();
            }
            if ((!str8.Equals("A") && !str8.Equals("B")) && !str8.Equals("C"))
            {
                context.Response.End();
            }
            ReturnResult result = new ReturnResult();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("type", "operate_adds");
            result.set_data(dictionary);
            if (string.IsNullOrEmpty(str4))
            {
                result.set_success(400);
                result.set_tipinfo(PageBase.GetMessageByCache("u100008", "MessageHint"));
                strResult = JsonHandle.ObjectToJson(result);
            }
            else
            {
                string str9 = this.Session["PKBJL_PlayType_Record_Current_Agent"];
                int num61 = 0;
                if (str9.Equals(num61.ToString()))
                {
                    result.set_success(400);
                    result.set_tipinfo(PageBase.GetMessageByCache("u100098", "MessageHint"));
                    strResult = JsonHandle.ObjectToJson(result);
                }
                else
                {
                    double num62;
                    string str14;
                    cz_odds_pkbjl _pkbjl = null;
                    if ((str3.Equals("1") || str3.Equals("2")) || str3.Equals("3"))
                    {
                        _pkbjl = CallBLL.cz_odds_pkbjl_bll.GetModel(Convert.ToInt32(str4));
                        if (_pkbjl == null)
                        {
                            result.set_success(400);
                            result.set_tipinfo(PageBase.GetMessageByCache("u100008", "MessageHint"));
                            strResult = JsonHandle.ObjectToJson(result);
                            return;
                        }
                    }
                    if (!str3.Equals("1") && !str3.Equals("2"))
                    {
                        if (str3.Equals("3"))
                        {
                            double num16 = double.Parse(_pkbjl.get_current_odds());
                            double num17 = Convert.ToDouble(_pkbjl.get_max_odds());
                            double num18 = Convert.ToDouble(_pkbjl.get_min_odds());
                            double num19 = 0.0;
                            if (str8.Equals("A"))
                            {
                                num19 = Convert.ToDouble(_pkbjl.get_a_diff());
                            }
                            else if (str8.Equals("B"))
                            {
                                num19 = Convert.ToDouble(_pkbjl.get_b_diff());
                            }
                            else if (str8.Equals("C"))
                            {
                                num19 = Convert.ToDouble(_pkbjl.get_c_diff());
                            }
                            base.GetFgsWTTable(8);
                            double wtvalue = 0.0;
                            base.GetOperate_KC(8, str4, ref wtvalue);
                            double num21 = num16;
                            double num22 = num18;
                            num62 = num16 + wtvalue;
                            double num23 = double.Parse(num62.ToString());
                            num62 = num16 + num19;
                            double num24 = double.Parse(num62.ToString());
                            num62 = (num21 - num22) + 1.0;
                            double num25 = double.Parse(num62.ToString());
                            num62 = double.Parse(s) - num19;
                            s = num62.ToString();
                            double num26 = base.GetPlayTypeWTValue_PKBJL(str4);
                            if (Convert.ToDouble(s) < num22)
                            {
                                result.set_success(400);
                                num62 = num22 + num19;
                                result.set_tipinfo(string.Format(PageBase.GetMessageByCache("u100023", "MessageHint"), _pkbjl.get_play_name() + "【" + _pkbjl.get_put_amount() + "】", double.Parse(num62.ToString())));
                                strResult = JsonHandle.ObjectToJson(result);
                            }
                            else
                            {
                                num61 = 0;
                                if (str9.Equals(num61.ToString()))
                                {
                                    if (Convert.ToDouble(s) > num21)
                                    {
                                        result.set_success(400);
                                        num62 = (num21 + num19) + num26;
                                        result.set_tipinfo(string.Format(PageBase.GetMessageByCache("u100055", "MessageHint"), _pkbjl.get_play_name() + "【" + _pkbjl.get_put_amount() + "】", double.Parse(num62.ToString())));
                                        strResult = JsonHandle.ObjectToJson(result);
                                        return;
                                    }
                                }
                                else if (Convert.ToDouble(s) > num21)
                                {
                                    result.set_success(400);
                                    num62 = num21 + num19;
                                    result.set_tipinfo(string.Format(PageBase.GetMessageByCache("u100055", "MessageHint"), _pkbjl.get_play_name() + "【" + _pkbjl.get_put_amount() + "】", double.Parse(num62.ToString())));
                                    strResult = JsonHandle.ObjectToJson(result);
                                    return;
                                }
                                if (Convert.ToDouble(s) < num25)
                                {
                                    result.set_success(400);
                                    num62 = num25 + num19;
                                    result.set_tipinfo(string.Format(PageBase.GetMessageByCache("u100023", "MessageHint"), _pkbjl.get_play_name() + "【" + _pkbjl.get_put_amount() + "】", double.Parse(num62.ToString())));
                                    strResult = JsonHandle.ObjectToJson(result);
                                }
                                else
                                {
                                    decimal num27 = decimal.Parse((decimal.Parse(s) - decimal.Parse(num21.ToString())).ToString());
                                    DataTable oddsWTbyFGS = CallBLL.cz_odds_wt_pkbjl_bll.GetOddsWTbyFGS(str4, str);
                                    num61 = 8;
                                    base.Add_FgsOddsWT_Lock(num61.ToString(), str, str4);
                                    int num28 = CallBLL.cz_odds_wt_pkbjl_bll.UpdateFgsWTOdds(Convert.ToInt32(str4), num27, str3, str);
                                    num61 = 8;
                                    base.Del_FgsOddsWT_Lock(num61.ToString(), str, str4);
                                    if (num28 > 0)
                                    {
                                        wtvalue = 0.0;
                                        CacheHelper.RemoveAllCache("fgs_wt_FileCacheKey" + context.Session["user_name"].ToString() + 8);
                                        FileCacheHelper.UpdateFGSWTFile(8);
                                        base.GetOperate_KC(8, str4, ref wtvalue);
                                        cz_odds_pkbjl _pkbjl3 = CallBLL.cz_odds_pkbjl_bll.GetModel(Convert.ToInt32(str4));
                                        base.fgs_opt_kc_log(oddsWTbyFGS, CallBLL.cz_odds_wt_pkbjl_bll.GetOddsWTbyFGS(str4, str), str3, str, str4, 8);
                                        double num29 = Convert.ToDouble(_pkbjl3.get_current_odds());
                                        Convert.ToDouble(_pkbjl3.get_max_odds());
                                        double num30 = Convert.ToDouble(_pkbjl3.get_min_odds());
                                        double num31 = 0.0;
                                        if (str8.Equals("A"))
                                        {
                                            num31 = Convert.ToDouble(_pkbjl3.get_a_diff());
                                        }
                                        else if (str8.Equals("B"))
                                        {
                                            num31 = Convert.ToDouble(_pkbjl3.get_b_diff());
                                        }
                                        else if (str8.Equals("C"))
                                        {
                                            num31 = Convert.ToDouble(_pkbjl3.get_c_diff());
                                        }
                                        num62 = (num29 + num31) + wtvalue;
                                        num23 = double.Parse(num62.ToString());
                                        num61 = 0;
                                        if (str9.Equals(num61.ToString()))
                                        {
                                            num23 = Utils.GetPKBJLPlaytypeOdds(str4, num23, base.GetPlayTypeWTValue_PKBJL(str4));
                                        }
                                        num62 = num29 + num31;
                                        num24 = double.Parse(num62.ToString());
                                        num62 = ((num29 - num30) + num31) + 1.0;
                                        num25 = double.Parse(num62.ToString());
                                        string str11 = _pkbjl3.get_is_open().ToString();
                                        Dictionary<string, object> dictionary4 = new Dictionary<string, object>();
                                        Dictionary<string, object> dictionary5 = new Dictionary<string, object>();
                                        dictionary5.Add("pl", Convert.ToDecimal(num23.ToString()).ToString());
                                        dictionary5.Add("maxpl", Convert.ToDecimal(num24.ToString()).ToString());
                                        dictionary5.Add("minpl", Convert.ToDecimal(num25.ToString()).ToString());
                                        dictionary5.Add("plx", new List<double>());
                                        DataTable table4 = CallBLL.cz_phase_pkbjl_bll.IsPhaseClose();
                                        if (table4.Rows[0]["isopen"].ToString().Equals("0") && string.IsNullOrEmpty(table4.Rows[0]["endtime"].ToString()))
                                        {
                                            str11 = "0";
                                        }
                                        if (table4.Rows[0]["openning"].ToString().Equals("n"))
                                        {
                                            str11 = "0";
                                        }
                                        dictionary5.Add("is_open", str11);
                                        dictionary4.Add(str4, new Dictionary<string, object>(dictionary5));
                                        dictionary.Add("play_odds", dictionary4);
                                        result.set_data(dictionary);
                                        result.set_success(200);
                                        strResult = JsonHandle.ObjectToJson(result);
                                    }
                                    else
                                    {
                                        result.set_success(400);
                                        result.set_tipinfo(PageBase.GetMessageByCache("u100011", "MessageHint"));
                                        strResult = JsonHandle.ObjectToJson(result);
                                    }
                                }
                            }
                        }
                        else
                        {
                            DataTable oddsInfoByID = null;
                            if ((str3.Equals("5") || str3.Equals("6")) || str3.Equals("7"))
                            {
                                oddsInfoByID = CallBLL.cz_odds_pkbjl_bll.GetOddsInfoByID(str4.ToString());
                            }
                            if (str3.Equals("5") || str3.Equals("6"))
                            {
                                base.Response.End();
                                if (string.IsNullOrEmpty(str6))
                                {
                                    str6 = "0.01";
                                }
                                new Dictionary<string, string>();
                                new Dictionary<string, string>();
                                new Dictionary<string, double>();
                                int num32 = 0;
                                foreach (DataRow row in oddsInfoByID.Rows)
                                {
                                    double num33 = double.Parse(row["current_odds"].ToString());
                                    Convert.ToDouble(row["max_odds"].ToString());
                                    double num34 = Convert.ToDouble(row["min_odds"].ToString());
                                    double num35 = Convert.ToDouble(row[str8 + "_diff"].ToString());
                                    base.GetFgsWTTable(8);
                                    double num36 = 0.0;
                                    base.GetOperate_KC(8, row["odds_id"].ToString(), ref num36);
                                    num62 = num33 + num36;
                                    double num37 = double.Parse(num62.ToString());
                                    num62 = num33 + num35;
                                    double.Parse(num62.ToString());
                                    num62 = (num33 - num34) + 1.0;
                                    double num38 = double.Parse(num62.ToString());
                                    double num39 = 0.0;
                                    double num40 = double.Parse(str6);
                                    str14 = str3;
                                    if (str14 != null)
                                    {
                                        if (!(str14 == "5"))
                                        {
                                            if (str14 == "6")
                                            {
                                                goto Label_11AD;
                                            }
                                        }
                                        else
                                        {
                                            num62 = num37 + Convert.ToDouble(num40);
                                            num39 = double.Parse(num62.ToString());
                                        }
                                    }
                                    goto Label_11C8;
                                Label_11AD:
                                    num62 = num37 + -Convert.ToDouble(num40);
                                    num39 = double.Parse(num62.ToString());
                                Label_11C8:
                                    if (num39 < num34)
                                    {
                                        num40 = 0.0;
                                    }
                                    if (num39 > num33)
                                    {
                                        num40 = 0.0;
                                    }
                                    if (num39 < num38)
                                    {
                                        num40 = 0.0;
                                    }
                                    DataTable table6 = CallBLL.cz_odds_wt_pkbjl_bll.GetOddsWTbyFGS(str4, str);
                                    num61 = 8;
                                    base.Add_FgsOddsWT_Lock(num61.ToString(), str, row["odds_id"].ToString());
                                    int num41 = CallBLL.cz_odds_wt_pkbjl_bll.UpdateFgsWTOddsList(row["odds_id"].ToString(), str3.Equals("5") ? Convert.ToDecimal(num40) : -Convert.ToDecimal(num40), "5", str);
                                    num61 = 8;
                                    base.Del_FgsOddsWT_Lock(num61.ToString(), str, row["odds_id"].ToString());
                                    if (num41 > 0)
                                    {
                                        base.fgs_opt_kc_log(table6, CallBLL.cz_odds_wt_pkbjl_bll.GetOddsWTbyFGS(str4, str), str3, str, str4, 8);
                                        num32++;
                                    }
                                }
                                if (num32 > 0)
                                {
                                    CacheHelper.RemoveAllCache("fgs_wt_FileCacheKey" + context.Session["user_name"].ToString() + 8);
                                    FileCacheHelper.UpdateFGSWTFile(8);
                                    DataTable table7 = CallBLL.cz_odds_pkbjl_bll.GetOddsInfoByID(str4);
                                    Dictionary<string, object> dictionary6 = new Dictionary<string, object>();
                                    foreach (DataRow row2 in table7.Rows)
                                    {
                                        double num42 = 0.0;
                                        base.GetOperate_KC(8, row2["odds_id"].ToString(), ref num42);
                                        cz_odds_pkbjl _pkbjl4 = CallBLL.cz_odds_pkbjl_bll.GetModel(Convert.ToInt32(row2["odds_id"].ToString()));
                                        double num43 = Convert.ToDouble(_pkbjl4.get_current_odds());
                                        double num44 = Convert.ToDouble(_pkbjl4.get_max_odds());
                                        double num45 = Convert.ToDouble(_pkbjl4.get_min_odds());
                                        double num46 = 0.0;
                                        if (str8.Equals("A"))
                                        {
                                            num46 = Convert.ToDouble(_pkbjl4.get_a_diff());
                                        }
                                        else if (str8.Equals("B"))
                                        {
                                            num46 = Convert.ToDouble(_pkbjl4.get_b_diff());
                                        }
                                        else if (str8.Equals("C"))
                                        {
                                            num46 = Convert.ToDouble(_pkbjl4.get_c_diff());
                                        }
                                        num62 = num43 + num46;
                                        num44 = double.Parse(num62.ToString());
                                        num62 = (num43 + num46) + num42;
                                        num43 = double.Parse(num62.ToString());
                                        num62 = ((num43 - num45) + num46) + 1.0;
                                        num45 = double.Parse(num62.ToString());
                                        string str12 = row2["is_open"].ToString();
                                        Dictionary<string, object> dictionary7 = new Dictionary<string, object>();
                                        dictionary7.Add("pl", Convert.ToDecimal(num43.ToString()).ToString());
                                        dictionary7.Add("maxpl", Convert.ToDecimal(num44.ToString()).ToString());
                                        dictionary7.Add("minpl", Convert.ToDecimal(num45.ToString()).ToString());
                                        dictionary7.Add("plx", new List<double>());
                                        DataTable table8 = CallBLL.cz_phase_pkbjl_bll.IsPhaseClose();
                                        if (table8.Rows[0]["isopen"].ToString().Equals("0") && string.IsNullOrEmpty(table8.Rows[0]["endtime"].ToString()))
                                        {
                                            str12 = "0";
                                        }
                                        if (table8.Rows[0]["openning"].ToString().Equals("n"))
                                        {
                                            str12 = "0";
                                        }
                                        dictionary7.Add("is_open", str12);
                                        dictionary6.Add(row2["odds_id"].ToString(), new Dictionary<string, object>(dictionary7));
                                    }
                                    dictionary.Add("play_odds", dictionary6);
                                    result.set_data(dictionary);
                                    result.set_success(200);
                                    strResult = JsonHandle.ObjectToJson(result);
                                }
                                else
                                {
                                    result.set_success(400);
                                    result.set_tipinfo(PageBase.GetMessageByCache("u100011", "MessageHint"));
                                    strResult = JsonHandle.ObjectToJson(result);
                                }
                            }
                            else if (str3.Equals("7"))
                            {
                                base.Response.End();
                                new Dictionary<string, string>();
                                new Dictionary<string, string>();
                                Dictionary<string, double> dictionary8 = new Dictionary<string, double>();
                                foreach (DataRow row3 in oddsInfoByID.Rows)
                                {
                                    double num47 = double.Parse(row3["current_odds"].ToString());
                                    Convert.ToDouble(row3["max_odds"].ToString());
                                    double num48 = Convert.ToDouble(row3["min_odds"].ToString());
                                    double num49 = Convert.ToDouble(row3[str8 + "_diff"].ToString());
                                    base.GetFgsWTTable(8);
                                    double num50 = 0.0;
                                    base.GetOperate_KC(8, row3["odds_id"].ToString(), ref num50);
                                    num62 = num47 + num50;
                                    double.Parse(num62.ToString());
                                    num62 = num47 + num49;
                                    double.Parse(num62.ToString());
                                    num62 = (num47 - num48) + 1.0;
                                    double num51 = double.Parse(num62.ToString());
                                    num62 = double.Parse(s) - num49;
                                    double num52 = double.Parse(num62.ToString());
                                    if (num52 > num47)
                                    {
                                        dictionary8.Add(row3["odds_id"].ToString(), 0.0);
                                    }
                                    else if (num52 < num51)
                                    {
                                        num62 = num47 - double.Parse(num51.ToString());
                                        double num53 = -double.Parse(num62.ToString());
                                        dictionary8.Add(row3["odds_id"].ToString(), num53);
                                    }
                                    else
                                    {
                                        num62 = num52 - double.Parse(num47.ToString());
                                        double num54 = double.Parse(num62.ToString());
                                        dictionary8.Add(row3["odds_id"].ToString(), num54);
                                    }
                                }
                                DataTable table9 = CallBLL.cz_odds_wt_pkbjl_bll.GetOddsWTbyFGS(str4, str);
                                num61 = 8;
                                base.Add_FgsOddsWT_Lock(num61.ToString(), str, str4);
                                int num55 = CallBLL.cz_odds_wt_pkbjl_bll.UpdateFgsWTOddsInputList(str4, dictionary8, str);
                                num61 = 8;
                                base.Del_FgsOddsWT_Lock(num61.ToString(), str, str4);
                                if (num55 > 0)
                                {
                                    CacheHelper.RemoveAllCache("fgs_wt_FileCacheKey" + context.Session["user_name"].ToString() + 8);
                                    FileCacheHelper.UpdateFGSWTFile(8);
                                    DataTable table10 = CallBLL.cz_odds_pkbjl_bll.GetOddsInfoByID(str4);
                                    base.fgs_opt_kc_log(table9, CallBLL.cz_odds_wt_pkbjl_bll.GetOddsWTbyFGS(str4, str), str3, str, str4, 8);
                                    Dictionary<string, object> dictionary9 = new Dictionary<string, object>();
                                    foreach (DataRow row4 in table10.Rows)
                                    {
                                        double num56 = 0.0;
                                        base.GetOperate_KC(8, row4["odds_id"].ToString(), ref num56);
                                        double num57 = Convert.ToDouble(row4["current_odds"]);
                                        double num58 = Convert.ToDouble(row4["max_odds"]);
                                        double num59 = Convert.ToDouble(row4["min_odds"]);
                                        double num60 = Convert.ToDouble(row4[str8 + "_diff"]);
                                        num62 = (num57 + num60) + num56;
                                        num57 = double.Parse(num62.ToString());
                                        num62 = num57 + num60;
                                        num58 = double.Parse(num62.ToString());
                                        num62 = ((num57 - num59) + num60) + 1.0;
                                        num59 = double.Parse(num62.ToString());
                                        string str13 = row4["is_open"].ToString();
                                        Dictionary<string, object> dictionary10 = new Dictionary<string, object>();
                                        dictionary10.Add("pl", Convert.ToDecimal(num57.ToString()).ToString());
                                        dictionary10.Add("maxpl", Convert.ToDecimal(num58.ToString()).ToString());
                                        dictionary10.Add("minpl", Convert.ToDecimal(num59.ToString()).ToString());
                                        dictionary10.Add("plx", new List<double>());
                                        DataTable table11 = CallBLL.cz_phase_pkbjl_bll.IsPhaseClose();
                                        if (table11.Rows[0]["isopen"].ToString().Equals("0") && string.IsNullOrEmpty(table11.Rows[0]["endtime"].ToString()))
                                        {
                                            str13 = "0";
                                        }
                                        if (table11.Rows[0]["openning"].ToString().Equals("n"))
                                        {
                                            str13 = "0";
                                        }
                                        dictionary10.Add("is_open", str13);
                                        dictionary9.Add(row4["odds_id"].ToString(), new Dictionary<string, object>(dictionary10));
                                    }
                                    dictionary.Add("play_odds", dictionary9);
                                    result.set_data(dictionary);
                                    result.set_success(200);
                                    strResult = JsonHandle.ObjectToJson(result);
                                }
                                else
                                {
                                    result.set_success(400);
                                    result.set_tipinfo(PageBase.GetMessageByCache("u100011", "MessageHint"));
                                    strResult = JsonHandle.ObjectToJson(result);
                                }
                            }
                        }
                    }
                    else
                    {
                        double num = double.Parse(_pkbjl.get_current_odds());
                        double num2 = Convert.ToDouble(_pkbjl.get_max_odds());
                        double num3 = Convert.ToDouble(_pkbjl.get_min_odds());
                        double num4 = 0.0;
                        if (str8.Equals("A"))
                        {
                            num4 = Convert.ToDouble(_pkbjl.get_a_diff());
                        }
                        else if (str8.Equals("B"))
                        {
                            num4 = Convert.ToDouble(_pkbjl.get_b_diff());
                        }
                        else if (str8.Equals("C"))
                        {
                            num4 = Convert.ToDouble(_pkbjl.get_c_diff());
                        }
                        base.GetFgsWTTable(8);
                        double num5 = 0.0;
                        base.GetOperate_KC(8, str4, ref num5);
                        double num6 = num;
                        double num7 = num3;
                        num62 = num + num5;
                        double num8 = double.Parse(num62.ToString());
                        num62 = num + num4;
                        double num9 = double.Parse(num62.ToString());
                        num62 = (num6 - num7) + 1.0;
                        double num10 = double.Parse(num62.ToString());
                        double num11 = 0.0;
                        str14 = str3;
                        if (str14 != null)
                        {
                            if (!(str14 == "1"))
                            {
                                if (str14 == "2")
                                {
                                    num62 = num8 + -Convert.ToDouble(str6);
                                    num11 = double.Parse(num62.ToString());
                                }
                            }
                            else
                            {
                                num62 = num8 + Convert.ToDouble(str6);
                                num11 = double.Parse(num62.ToString());
                            }
                        }
                        if (!str3.Equals("1") && (num11 < num7))
                        {
                            result.set_success(400);
                            num62 = num7 + num4;
                            result.set_tipinfo(string.Format(PageBase.GetMessageByCache("u100023", "MessageHint"), _pkbjl.get_play_name() + "【" + _pkbjl.get_put_amount() + "】", double.Parse(num62.ToString())));
                            strResult = JsonHandle.ObjectToJson(result);
                        }
                        else if (!str3.Equals("2") && (num11 > num6))
                        {
                            result.set_success(400);
                            num62 = num6 + num4;
                            result.set_tipinfo(string.Format(PageBase.GetMessageByCache("u100055", "MessageHint"), _pkbjl.get_play_name() + "【" + _pkbjl.get_put_amount() + "】", double.Parse(num62.ToString())));
                            strResult = JsonHandle.ObjectToJson(result);
                        }
                        else if (num11 < num10)
                        {
                            result.set_success(400);
                            num62 = num10 + num4;
                            result.set_tipinfo(string.Format(PageBase.GetMessageByCache("u100023", "MessageHint"), _pkbjl.get_play_name() + "【" + _pkbjl.get_put_amount() + "】", double.Parse(num62.ToString())));
                            strResult = JsonHandle.ObjectToJson(result);
                        }
                        else
                        {
                            DataTable table = CallBLL.cz_odds_wt_pkbjl_bll.GetOddsWTbyFGS(str4, str);
                            num61 = 8;
                            base.Add_FgsOddsWT_Lock(num61.ToString(), str, str4);
                            int num12 = CallBLL.cz_odds_wt_pkbjl_bll.UpdateFgsWTOdds(Convert.ToInt32(str4), str3.Equals("1") ? decimal.Parse(str6) : -decimal.Parse(str6), str3, str);
                            num61 = 8;
                            base.Del_FgsOddsWT_Lock(num61.ToString(), str, str4);
                            if (num12 > 0)
                            {
                                num5 = 0.0;
                                CacheHelper.RemoveAllCache("fgs_wt_FileCacheKey" + context.Session["user_name"].ToString() + 8);
                                FileCacheHelper.UpdateFGSWTFile(8);
                                base.GetOperate_KC(8, str4, ref num5);
                                cz_odds_pkbjl _pkbjl2 = CallBLL.cz_odds_pkbjl_bll.GetModel(Convert.ToInt32(str4));
                                base.fgs_opt_kc_log(table, CallBLL.cz_odds_wt_pkbjl_bll.GetOddsWTbyFGS(str4, str), str3, str, str4, 8);
                                double num13 = Convert.ToDouble(_pkbjl2.get_current_odds());
                                Convert.ToDouble(_pkbjl2.get_max_odds());
                                double num14 = Convert.ToDouble(_pkbjl2.get_min_odds());
                                double num15 = 0.0;
                                if (str8.Equals("A"))
                                {
                                    num15 = Convert.ToDouble(_pkbjl2.get_a_diff());
                                }
                                else if (str8.Equals("B"))
                                {
                                    num15 = Convert.ToDouble(_pkbjl2.get_b_diff());
                                }
                                else if (str8.Equals("C"))
                                {
                                    num15 = Convert.ToDouble(_pkbjl2.get_c_diff());
                                }
                                num62 = (num13 + num15) + num5;
                                num8 = double.Parse(num62.ToString());
                                num62 = num13 + num15;
                                num9 = double.Parse(num62.ToString());
                                num62 = ((num13 - num14) + num15) + 1.0;
                                num10 = num10 = double.Parse(num62.ToString());
                                string str10 = _pkbjl2.get_is_open().ToString();
                                Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
                                Dictionary<string, object> dictionary3 = new Dictionary<string, object>();
                                num61 = 0;
                                if (str9.Equals(num61.ToString()))
                                {
                                    num8 = Utils.GetPKBJLPlaytypeOdds(str4, num8, base.GetPlayTypeWTValue_PKBJL(str4));
                                }
                                dictionary3.Add("pl", Convert.ToDecimal(num8.ToString()).ToString());
                                dictionary3.Add("maxpl", Convert.ToDecimal(num9.ToString()).ToString());
                                dictionary3.Add("minpl", Convert.ToDecimal(num10.ToString()).ToString());
                                dictionary3.Add("plx", new List<double>());
                                DataTable table2 = CallBLL.cz_phase_pkbjl_bll.IsPhaseClose();
                                if (table2.Rows[0]["isopen"].ToString().Equals("0") && string.IsNullOrEmpty(table2.Rows[0]["endtime"].ToString()))
                                {
                                    str10 = "0";
                                }
                                if (table2.Rows[0]["openning"].ToString().Equals("n"))
                                {
                                    str10 = "0";
                                }
                                dictionary3.Add("is_open", str10);
                                dictionary2.Add(str4, new Dictionary<string, object>(dictionary3));
                                dictionary.Add("play_odds", dictionary2);
                                result.set_data(dictionary);
                                result.set_success(200);
                                strResult = JsonHandle.ObjectToJson(result);
                            }
                            else
                            {
                                result.set_success(400);
                                result.set_tipinfo(PageBase.GetMessageByCache("u100011", "MessageHint"));
                                strResult = JsonHandle.ObjectToJson(result);
                            }
                        }
                    }
                }
            }
        }

        public override void ProcessRequest(HttpContext context)
        {
            base.checkLoginByHandler(0);
            context.Session["user_name"].ToString();
            string str = context.Session["user_type"].ToString();
            string str2 = LSRequest.qq("action");
            string strResult = "";
            if (!string.IsNullOrEmpty(str2))
            {
                string str4 = str2;
                if (str4 != null)
                {
                    if (!(str4 == "get_oddsinfo"))
                    {
                        if (!(str4 == "get_clyl"))
                        {
                            if (str4 == "operate_adds")
                            {
                                if (str.ToLower().Equals("fgs"))
                                {
                                    if (!base.IsFGSWT_Opt(8))
                                    {
                                        ReturnResult result = new ReturnResult();
                                        result.set_success(400);
                                        result.set_tipinfo(PageBase.GetMessageByCache("u100095", "MessageHint"));
                                        strResult = JsonHandle.ObjectToJson(result);
                                    }
                                    else
                                    {
                                        this.operate_odds_wt(context, ref strResult);
                                    }
                                }
                                else
                                {
                                    this.operate_adds(context, ref strResult);
                                }
                            }
                            else if (str4 == "operate_closeopen")
                            {
                                this.operate_CloseOpen(context, ref strResult);
                            }
                            else if (str4 == "get_opennumber")
                            {
                                this.get_opennumber(context, ref strResult);
                            }
                            else if (str4 == "set_allowsale")
                            {
                                this.user_allow_sale(context, ref strResult);
                            }
                        }
                    }
                    else
                    {
                        this.get_oddsinfo(context, ref strResult);
                    }
                }
                context.Response.ContentType = "text/json";
                context.Response.Write(strResult);
            }
        }

        public void user_allow_sale(HttpContext context, ref string strResult)
        {
            string str12;
            int num14;
            ReturnResult result = new ReturnResult();
            result.set_success(400);
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("type", "user_allow_sale");
            string str = context.Session["user_name"].ToString();
            agent_userinfo_session model = context.Session[str + "lottery_session_user_info"] as agent_userinfo_session;
            if (!string.IsNullOrEmpty(base.Permission_Ashx_DL(model, "po_5_2")))
            {
                context.Response.Write(PageBase.GetMessageByCache("u100027", "MessageHint"));
                context.Response.End();
            }
            if (!model.get_kc_allow_sale().Equals(1))
            {
                result.set_success(400);
                result.set_tipinfo(PageBase.GetMessageByCache("u100022", "MessageHint"));
                strResult = JsonHandle.ObjectToJson(result);
                context.Response.End();
            }
            else if ((model.get_users_child_session() != null) && (model.get_users_child_session().get_permissions_name().IndexOf("po_5_2") < 0))
            {
                result.set_success(400);
                result.set_tipinfo(PageBase.GetMessageByCache("u100022", "MessageHint"));
                strResult = JsonHandle.ObjectToJson(result);
                context.Response.End();
            }
            if (model.get_u_type().Trim().Equals("zj"))
            {
                result.set_success(400);
                result.set_tipinfo(PageBase.GetMessageByCache("u100026", "MessageHint"));
                strResult = JsonHandle.ObjectToJson(result);
                context.Response.End();
            }
            string s = LSRequest.qq("phaseid");
            string str4 = LSRequest.qq("oddsid");
            string str5 = LSRequest.qq("currentodds");
            string str6 = LSRequest.qq("amount");
            string str7 = LSRequest.qq("number");
            LSRequest.qq("fast");
            string str8 = LSRequest.qq("pk");
            string str9 = LSRequest.qq("playpage");
            string str10 = LSRequest.qq("playtype");
            string str11 = Utils.GetPKBJL_NumTable(str9, null);
            if (string.IsNullOrEmpty(str11))
            {
                result.set_success(400);
                result.set_tipinfo(PageBase.GetMessageByCache("u100008", "MessageHint"));
                strResult = JsonHandle.ObjectToJson(result);
                return;
            }
            if (!(this.Session["PKBJL_TableType_Record_Current_Agent"]).Equals(str9))
            {
                result.set_success(400);
                result.set_tipinfo(PageBase.GetMessageByCache("u100008", "MessageHint"));
                strResult = JsonHandle.ObjectToJson(result);
                return;
            }
            if (!string.IsNullOrEmpty(str10))
            {
                num14 = 0;
                if (str10 != num14.ToString())
                {
                    num14 = 1;
                    if (str10 != num14.ToString())
                    {
                        context.Response.End();
                        goto Label_0300;
                    }
                }
                if (!(this.Session["PKBJL_PlayType_Record_Current_Agent"]).Equals(str10))
                {
                    result.set_success(400);
                    result.set_tipinfo(PageBase.GetMessageByCache("u100008", "MessageHint"));
                    strResult = JsonHandle.ObjectToJson(result);
                    return;
                }
            }
            else
            {
                context.Response.End();
            }
        Label_0300:
            str12 = LSRequest.qq("jeucode");
            if ((context.Session["JeuValidate"] == null) || !context.Session["JeuValidate"].ToString().Equals(str12))
            {
                context.Session["JeuValidate"] = null;
                result.set_success(400);
                new Dictionary<string, object>();
                context.Session["JeuValidate"] = base.get_JeuValidate();
                dictionary.Add("jeucode", context.Session["JeuValidate"].ToString());
                result.set_data(dictionary);
                result.set_tipinfo(PageBase.GetMessageByCache("u100030", "MessageHint"));
                strResult = JsonHandle.ObjectToJson(result);
            }
            else
            {
                context.Session["JeuValidate"] = null;
                string[] strArray = str4.Split(new char[] { ',' });
                string[] strArray2 = str6.Split(new char[] { ',' });
                if (strArray2.Length > 100)
                {
                    context.Response.End();
                }
                DataTable phaseByPID = CallBLL.cz_phase_pkbjl_bll.GetPhaseByPID(s);
                Convert.ToDateTime(phaseByPID.Rows[0]["open_date"].ToString().Trim()).ToString("HH:mm:ss");
                DateTime time2 = Convert.ToDateTime(phaseByPID.Rows[0]["stop_date"].ToString().Trim());
                DateTime time3 = Convert.ToDateTime(DateTime.Now.ToString());
                Utils.DateDiff(time2, time3).ToString();
                if (time2 < time3)
                {
                    result.set_success(400);
                    result.set_tipinfo(PageBase.GetMessageByCache("u100015", "MessageHint"));
                    strResult = JsonHandle.ObjectToJson(result);
                }
                else
                {
                    string phase = phaseByPID.Rows[0]["phase"].ToString().Trim();
                    int index = 0;
                    string[] strArray4 = strArray;
                    int num15 = 0;
                    while (num15 < strArray4.Length)
                    {
                        string str34;
                        string str36;
                        string str14 = strArray4[num15];
                        string str15 = strArray2[index];
                        bool flag = false;
                        DataTable table2 = null;
                        if (flag)
                        {
                            string[] strArray3 = str7.Split(new char[] { ',' });
                            string str16 = "";
                            string str17 = "";
                            foreach (string str18 in strArray3)
                            {
                                if (int.Parse(str18) < 10)
                                {
                                    str17 = "0" + int.Parse(str18).ToString();
                                }
                                else
                                {
                                    str17 = str18.ToString();
                                }
                                str16 = str16 + str17.Trim() + ",";
                            }
                            str7 = str16.Substring(0, str16.Length - 1);
                            num14 = 8;
                            table2 = CallBLL.cz_bet_kc_bll.GetSZZT_PKBJL(model.get_u_name(), model.get_u_type().Trim(), str4, s, true, str7, num14.ToString(), "pkbjl", str10);
                        }
                        else
                        {
                            num14 = 8;
                            table2 = CallBLL.cz_bet_kc_bll.GetSZZT_PKBJL(model.get_u_name(), model.get_u_type().Trim(), str4, s, false, "", num14.ToString(), "pkbjl", str10);
                        }
                        if (double.Parse(table2.Rows[0][0].ToString()) < double.Parse(str6))
                        {
                            context.Response.End();
                        }
                        if (double.Parse(str6) < 10.0)
                        {
                            result.set_success(400);
                            result.set_tipinfo(string.Format(PageBase.GetMessageByCache("u100096", "MessageHint"), 10));
                            strResult = JsonHandle.ObjectToJson(result);
                            break;
                        }
                        string str19 = "A";
                        if (!Utils.Agent_AllowSaleKind(str8, model.get_kc_kind(), ref str19))
                        {
                            result.set_success(400);
                            result.set_tipinfo(PageBase.GetMessageByCache("u100056", "MessageHint"));
                            strResult = JsonHandle.ObjectToJson(result);
                            break;
                        }
                        agent_kc_rate _rate = base.GetUserRate_kc(model.get_zjname());
                        DataTable table3 = base.GetUserDrawback_pkbjl();
                        DataSet playOddsInfo = CallBLL.cz_play_pkbjl_bll.GetPlayOddsInfo(str14);
                        string str20 = playOddsInfo.Tables[0].Rows[0]["play_id"].ToString();
                        string str21 = playOddsInfo.Tables[0].Rows[0]["current_odds"].ToString().Trim();
                        string str22 = playOddsInfo.Tables[0].Rows[0]["put_amount"].ToString().Trim();
                        string str23 = playOddsInfo.Tables[0].Rows[0]["play_name"].ToString().Trim();
                        playOddsInfo.Tables[0].Rows[0]["min_odds"].ToString().Trim();
                        string str24 = playOddsInfo.Tables[0].Rows[0][str19.ToUpper() + "_diff"].ToString().Trim();
                        double num17 = double.Parse(str21) + double.Parse(str24);
                        string str25 = num17.ToString();
                        base.GetOdds_KC_EX(8, playOddsInfo.Tables[0].Rows[0]["current_odds"].ToString().Trim(), str24, ref str21, str14);
                        num14 = 0;
                        if (str10.Equals(num14.ToString()))
                        {
                            str25 = Utils.GetPKBJLPlaytypeOdds(str14, double.Parse(str25), base.GetPlayTypeWTValue_PKBJL(str14)).ToString();
                            str21 = Utils.GetPKBJLPlaytypeOdds(str14, double.Parse(str21), base.GetPlayTypeWTValue_PKBJL(str14)).ToString();
                        }
                        if (model.get_u_type().Equals("fgs") && model.get_kc_op_odds().Equals(1))
                        {
                            str21 = str25;
                            str5 = str21;
                        }
                        string orderNumber = Utils.GetOrderNumber();
                        if (!str21.Equals(str5))
                        {
                            result.set_success(600);
                            new List<object>();
                            Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
                            dictionary2.Add("oldodds", str5);
                            dictionary2.Add("newodds", str21);
                            dictionary.Add("oddschange", dictionary2);
                            dictionary.Add("jeucode", base.get_JeuValidate());
                            result.set_data(dictionary);
                            result.set_tipinfo(string.Format(PageBase.GetMessageByCache("u100028", "MessageHint"), str5, str21));
                            strResult = JsonHandle.ObjectToJson(result);
                            break;
                        }
                        DataRow[] rowArray = table3.Select(string.Format(" play_id={0} ", str20));
                        string str27 = "0";
                        string str28 = "0";
                        string str29 = "0";
                        string str30 = "0";
                        string str31 = "0";
                        string str32 = "0";
                        double num2 = 0.0;
                        double num3 = 0.0;
                        if (_rate.get_zcyg().Equals("1"))
                        {
                            num2 = ((100.0 - double.Parse(_rate.get_fgszc())) - double.Parse(_rate.get_gdzc())) - double.Parse(_rate.get_zdzc());
                            num3 = double.Parse(_rate.get_fgszc());
                        }
                        else if (model.get_u_type() == "fgs")
                        {
                            num2 = 100.0;
                        }
                        else
                        {
                            num3 = ((100.0 - double.Parse(_rate.get_zjzc())) - double.Parse(_rate.get_gdzc())) - double.Parse(_rate.get_zdzc());
                            num2 = double.Parse(_rate.get_zjzc());
                        }
                        foreach (DataRow row in rowArray)
                        {
                            str36 = row["u_type"].ToString().Trim();
                            if (str36 != null)
                            {
                                if (!(str36 == "zj"))
                                {
                                    if (str36 == "fgs")
                                    {
                                        goto Label_0B87;
                                    }
                                    if (str36 == "gd")
                                    {
                                        goto Label_0BAB;
                                    }
                                    if (str36 == "zd")
                                    {
                                        goto Label_0BCC;
                                    }
                                    if (str36 == "dl")
                                    {
                                        goto Label_0BED;
                                    }
                                    if (str36 == "hy")
                                    {
                                        goto Label_0C0E;
                                    }
                                }
                                else
                                {
                                    str27 = row[str19 + "_drawback"].ToString().Trim();
                                }
                            }
                            continue;
                        Label_0B87:
                            str28 = row[str19 + "_drawback"].ToString().Trim();
                            continue;
                        Label_0BAB:
                            str29 = row[str19 + "_drawback"].ToString().Trim();
                            continue;
                        Label_0BCC:
                            str30 = row[str19 + "_drawback"].ToString().Trim();
                            continue;
                        Label_0BED:
                            str31 = row[str19 + "_drawback"].ToString().Trim();
                            continue;
                        Label_0C0E:
                            str32 = row[str19 + "_drawback"].ToString().Trim();
                        }
                        str36 = model.get_u_type().ToString().Trim();
                        if (str36 != null)
                        {
                            if (!(str36 == "fgs"))
                            {
                                if (str36 == "gd")
                                {
                                    goto Label_0CAC;
                                }
                                if (str36 == "zd")
                                {
                                    goto Label_0CC8;
                                }
                                if (str36 == "dl")
                                {
                                    goto Label_0CE4;
                                }
                            }
                            else
                            {
                                context.Session["user_name"].ToString();
                                str29 = str28;
                            }
                        }
                        goto Label_0CFE;
                    Label_0CAC:
                        context.Session["user_name"].ToString();
                        str30 = str29;
                        goto Label_0CFE;
                    Label_0CC8:
                        context.Session["user_name"].ToString();
                        str31 = str30;
                        goto Label_0CFE;
                    Label_0CE4:
                        context.Session["user_name"].ToString();
                        str32 = str31;
                    Label_0CFE:
                        if (flag)
                        {
                            goto Label_167E;
                        }
                        cz_bet_kc _kc = new cz_bet_kc();
                        _kc.set_order_num(orderNumber);
                        _kc.set_checkcode(str12);
                        _kc.set_u_name(model.get_u_name());
                        _kc.set_u_nicker(model.get_u_nicker());
                        _kc.set_phase_id(new int?(int.Parse(s)));
                        _kc.set_phase(phase);
                        _kc.set_bet_time(new DateTime?(DateTime.Now));
                        _kc.set_odds_id(new int?(int.Parse(str14)));
                        _kc.set_category(playOddsInfo.Tables[0].Rows[0]["category"].ToString());
                        _kc.set_play_name(str23);
                        _kc.set_play_id(new int?(int.Parse(str20)));
                        _kc.set_bet_val(str22);
                        _kc.set_bet_wt("");
                        _kc.set_odds(str21);
                        _kc.set_amount(new decimal?(decimal.Parse(str6)));
                        _kc.set_profit(0);
                        str36 = model.get_u_type().Trim();
                        if (str36 != null)
                        {
                            if (!(str36 == "dl"))
                            {
                                if (str36 == "zd")
                                {
                                    goto Label_0F67;
                                }
                                if (str36 == "gd")
                                {
                                    goto Label_1065;
                                }
                                if (str36 == "fgs")
                                {
                                    goto Label_1158;
                                }
                            }
                            else
                            {
                                _kc.set_hy_drawback(new decimal?(decimal.Parse(str32)));
                                _kc.set_dl_drawback(new decimal?(decimal.Parse(str31)));
                                _kc.set_zd_drawback(new decimal?(decimal.Parse(str30)));
                                _kc.set_gd_drawback(new decimal?(decimal.Parse(str29)));
                                _kc.set_fgs_drawback(new decimal?(decimal.Parse(str28)));
                                _kc.set_zj_drawback(new decimal?(decimal.Parse(str27)));
                                _kc.set_dl_rate(0f);
                                _kc.set_zd_rate((float) int.Parse(_rate.get_zdzc()));
                                _kc.set_gd_rate((float) int.Parse(_rate.get_gdzc()));
                                _kc.set_fgs_rate(float.Parse(num3.ToString()));
                                _kc.set_zj_rate(float.Parse(num2.ToString()));
                                _kc.set_dl_name(_rate.get_dlname());
                                _kc.set_zd_name(_rate.get_zdname());
                                _kc.set_gd_name(_rate.get_gdname());
                                _kc.set_fgs_name(_rate.get_fgsname());
                            }
                        }
                        goto Label_123C;
                    Label_0F67:
                        _kc.set_hy_drawback(0);
                        _kc.set_dl_drawback(new decimal?(decimal.Parse(str31)));
                        _kc.set_zd_drawback(new decimal?(decimal.Parse(str30)));
                        _kc.set_gd_drawback(new decimal?(decimal.Parse(str29)));
                        _kc.set_fgs_drawback(new decimal?(decimal.Parse(str28)));
                        _kc.set_zj_drawback(new decimal?(decimal.Parse(str27)));
                        _kc.set_dl_rate(0f);
                        _kc.set_zd_rate(0f);
                        _kc.set_gd_rate((float) int.Parse(_rate.get_gdzc()));
                        _kc.set_fgs_rate(float.Parse(num3.ToString()));
                        _kc.set_zj_rate(float.Parse(num2.ToString()));
                        _kc.set_dl_name("");
                        _kc.set_zd_name(_rate.get_zdname());
                        _kc.set_gd_name(_rate.get_gdname());
                        _kc.set_fgs_name(_rate.get_fgsname());
                        goto Label_123C;
                    Label_1065:
                        _kc.set_hy_drawback(0);
                        _kc.set_dl_drawback(0);
                        _kc.set_zd_drawback(new decimal?(decimal.Parse(str30)));
                        _kc.set_gd_drawback(new decimal?(decimal.Parse(str29)));
                        _kc.set_fgs_drawback(new decimal?(decimal.Parse(str28)));
                        _kc.set_zj_drawback(new decimal?(decimal.Parse(str27)));
                        _kc.set_dl_rate(0f);
                        _kc.set_zd_rate(0f);
                        _kc.set_gd_rate(0f);
                        _kc.set_fgs_rate(float.Parse(num3.ToString()));
                        _kc.set_zj_rate(float.Parse(num2.ToString()));
                        _kc.set_dl_name("");
                        _kc.set_zd_name("");
                        _kc.set_gd_name(_rate.get_gdname());
                        _kc.set_fgs_name(_rate.get_fgsname());
                        goto Label_123C;
                    Label_1158:
                        _kc.set_hy_drawback(0);
                        _kc.set_dl_drawback(0);
                        _kc.set_zd_drawback(0);
                        _kc.set_gd_drawback(new decimal?(decimal.Parse(str29)));
                        _kc.set_fgs_drawback(new decimal?(decimal.Parse(str28)));
                        _kc.set_zj_drawback(new decimal?(decimal.Parse(str27)));
                        _kc.set_dl_rate(0f);
                        _kc.set_zd_rate(0f);
                        _kc.set_gd_rate(0f);
                        _kc.set_fgs_rate(0f);
                        _kc.set_zj_rate(float.Parse(num2.ToString()));
                        _kc.set_dl_name("");
                        _kc.set_zd_name("");
                        _kc.set_gd_name("");
                        _kc.set_fgs_name(_rate.get_fgsname());
                    Label_123C:
                        _kc.set_is_payment(0);
                        _kc.set_sale_type(1);
                        _kc.set_m_type(-2);
                        _kc.set_kind(str19);
                        _kc.set_ip(LSRequest.GetIP());
                        _kc.set_lottery_type(8);
                        _kc.set_lottery_name(base.GetGameNameByID(_kc.get_lottery_type().ToString()));
                        _kc.set_ordervalidcode(Utils.GetOrderValidCode(_kc.get_u_name(), _kc.get_order_num(), _kc.get_bet_val(), _kc.get_odds(), _kc.get_kind(), Convert.ToInt32(_kc.get_phase_id()), Convert.ToInt32(_kc.get_odds_id()), Convert.ToDouble(_kc.get_amount())));
                        _kc.set_odds_zj(str25);
                        _kc.set_table_type(new int?(int.Parse(str11)));
                        _kc.set_play_type(new int?(int.Parse(str10)));
                        int num6 = 0;
                        CallBLL.cz_bet_kc_bll.AddBetByBH(_kc, ref num6);
                        double num7 = (double.Parse(str15) * num2) / 100.0;
                        CallBLL.cz_odds_pkbjl_bll.Agent_UpdateGrandTotal(num7.ToString(), str14);
                        double num8 = double.Parse(playOddsInfo.Tables[0].Rows[0]["grand_total"].ToString()) + num7;
                        double num9 = double.Parse(playOddsInfo.Tables[0].Rows[0]["downbase"].ToString());
                        double num10 = Math.Floor((double) (num8 / num9));
                        if ((num10 >= 1.0) && (num9 >= 1.0))
                        {
                            double num11 = double.Parse(playOddsInfo.Tables[0].Rows[0]["down_odds_rate"].ToString()) * num10;
                            CallBLL.cz_odds_pkbjl_bll.UpdateGrandTotalCurrentOdds(num11.ToString(), (num8 - (num9 * num10)).ToString(), str14.ToString());
                            cz_system_log _log = new cz_system_log();
                            _log.set_user_name("系統");
                            _log.set_children_name("");
                            _log.set_category(playOddsInfo.Tables[0].Rows[0]["category"].ToString());
                            _log.set_play_name(str23);
                            _log.set_put_amount(str22);
                            _log.set_l_name(LSKeys.get_PKBJL_Name());
                            _log.set_l_phase(phase);
                            _log.set_action("降賠率");
                            _log.set_odds_id(int.Parse(str4));
                            string str33 = playOddsInfo.Tables[0].Rows[0]["current_odds"].ToString();
                            _log.set_old_val(str33);
                            num17 = double.Parse(str33) - num11;
                            _log.set_new_val(num17.ToString());
                            _log.set_ip(LSRequest.GetIP());
                            _log.set_add_time(DateTime.Now);
                            _log.set_note("系統自動降賠");
                            _log.set_type_id(Convert.ToInt32((LSEnums.LogTypeID) 0));
                            _log.set_lottery_type(8);
                            CallBLL.cz_system_log_bll.Add(_log);
                            cz_jp_odds _odds = new cz_jp_odds();
                            _odds.set_add_time(DateTime.Now);
                            _odds.set_odds_id(int.Parse(str14));
                            _odds.set_phase_id(int.Parse(s));
                            _odds.set_play_name(str23);
                            _odds.set_put_amount(str22);
                            _odds.set_odds(num11.ToString());
                            _odds.set_lottery_type(8);
                            _odds.set_phase(phase);
                            _odds.set_old_odds(str33);
                            _odds.set_new_odds((double.Parse(str33) - num11).ToString());
                            CallBLL.cz_jp_odds_bll.Add(_odds);
                        }
                        this.ZDBH(model, _rate, str19, context, s, phase, str20, str14, str27, str28, str29, str30, str31, str32, str12, str11, str10);
                        this.GDBH(model, _rate, str19, context, s, phase, str20, str14, str27, str28, str29, str30, str31, str32, str12, str11, str10);
                        this.FGSBH(model, _rate, str19, context, s, phase, str20, str14, str27, str28, str29, str30, str31, str32, str12, str11, str10);
                    Label_167E:
                        str34 = "";
                        double num13 = double.Parse(str6) * (double.Parse(str21) - 1.0);
                        str34 = num13.ToString();
                        string str35 = str22;
                        List<FastSale> list = new List<FastSale>();
                        FastSale item = new FastSale();
                        item.set_success("1");
                        item.set_ordernum(orderNumber);
                        item.set_playname(str23);
                        item.set_putamount(str35);
                        str34 = num13.ToString();
                        item.set_ky(str34);
                        item.set_islm("0");
                        item.set_pl(str21.ToString());
                        item.set_message("");
                        item.set_amount(str6);
                        list.Add(item);
                        dictionary.Add("bhdata", list);
                        new Dictionary<string, object>();
                        dictionary.Add("jeucode", base.get_JeuValidate());
                        result.set_data(dictionary);
                        result.set_success(200);
                        strResult = JsonHandle.ObjectToJson(result);
                        break;
                    }
                }
            }
        }

        private void ZDBH(agent_userinfo_session userModel, agent_kc_rate kc_rate, string kc_kind, HttpContext context, string phase_id, string phase, string play_id, string odds_id, string zj_ds, string fgs_ds, string gd_ds, string zd_ds, string dl_ds, string hy_ds, string checkcode, string numTable, string playtype)
        {
            if (!string.IsNullOrEmpty(kc_rate.get_zdname()))
            {
                DataTable isAllowSale = CallBLL.cz_users_bll.GetIsAllowSale(kc_rate.get_zdname());
                if ((isAllowSale.Rows[0]["kc_allow_sale"] != null) && (Convert.ToInt32(isAllowSale.Rows[0]["kc_allow_sale"]) != 0))
                {
                    double num = 1.0;
                    DataTable autosale = CallBLL.cz_autosale_pkbjl_bll.GetAutosale(kc_rate.get_zdname(), play_id);
                    if (((autosale != null) && (autosale.Rows.Count > 0)) && autosale.Rows[0]["sale_type"].ToString().Equals("0"))
                    {
                        DataTable table3 = CallBLL.cz_bet_kc_bll.GetSZZTBHByOddsid("zd", kc_rate.get_zdname(), odds_id, phase_id, 8);
                        if ((double.Parse(table3.Rows[0]["szzt"].ToString()) - num) > double.Parse(autosale.Rows[0]["max_money"].ToString()))
                        {
                            double num2 = double.Parse(table3.Rows[0]["szzt"].ToString());
                            double num3 = double.Parse(autosale.Rows[0]["max_money"].ToString());
                            double num4 = Math.Floor((double) (num2 - num3));
                            DataSet playOddsInfo = CallBLL.cz_play_pkbjl_bll.GetPlayOddsInfo(odds_id);
                            string s = playOddsInfo.Tables[0].Rows[0]["current_odds"].ToString().Trim();
                            string str2 = playOddsInfo.Tables[0].Rows[0]["play_name"].ToString();
                            string str3 = playOddsInfo.Tables[0].Rows[0]["put_amount"].ToString();
                            string str4 = playOddsInfo.Tables[0].Rows[0][kc_kind.ToUpper() + "_diff"].ToString().Trim();
                            string str5 = (double.Parse(s) + double.Parse(str4)).ToString();
                            base.GetOdds_KC_EX(8, playOddsInfo.Tables[0].Rows[0]["current_odds"].ToString().Trim(), str4, ref s, odds_id);
                            int num15 = 0;
                            if (playtype.Equals(num15.ToString()))
                            {
                                str5 = Utils.GetPKBJLPlaytypeOdds(odds_id, double.Parse(str5), base.GetPlayTypeWTValue_PKBJL(odds_id)).ToString();
                                s = Utils.GetPKBJLPlaytypeOdds(odds_id, double.Parse(s), base.GetPlayTypeWTValue_PKBJL(odds_id)).ToString();
                            }
                            dl_ds = zd_ds;
                            string str6 = kc_rate.get_zjzc();
                            string str7 = kc_rate.get_fgszc();
                            if (kc_rate.get_zcyg() == "1")
                            {
                                str6 = ((100.0 - double.Parse(kc_rate.get_fgszc())) - double.Parse(kc_rate.get_gdzc())).ToString();
                            }
                            else
                            {
                                str7 = ((100.0 - double.Parse(kc_rate.get_zjzc())) - double.Parse(kc_rate.get_gdzc())).ToString();
                            }
                            cz_bet_kc _kc = new cz_bet_kc();
                            _kc.set_order_num(Utils.GetOrderNumber());
                            _kc.set_checkcode(checkcode);
                            _kc.set_u_name(kc_rate.get_zdname());
                            _kc.set_u_nicker("");
                            _kc.set_phase_id(new int?(int.Parse(phase_id)));
                            _kc.set_phase(phase);
                            _kc.set_bet_time(new DateTime?(DateTime.Now));
                            _kc.set_odds_id(new int?(int.Parse(odds_id)));
                            _kc.set_category(playOddsInfo.Tables[0].Rows[0]["category"].ToString());
                            _kc.set_play_id(new int?(int.Parse(play_id)));
                            _kc.set_play_name(str2);
                            _kc.set_bet_val(str3);
                            _kc.set_bet_wt("");
                            _kc.set_odds(s);
                            _kc.set_amount(new decimal?(decimal.Parse(num4.ToString())));
                            _kc.set_profit(0);
                            _kc.set_hy_drawback(0);
                            _kc.set_dl_drawback(new decimal?(decimal.Parse(dl_ds)));
                            _kc.set_zd_drawback(new decimal?(decimal.Parse(zd_ds)));
                            _kc.set_gd_drawback(new decimal?(decimal.Parse(gd_ds)));
                            _kc.set_fgs_drawback(new decimal?(decimal.Parse(fgs_ds)));
                            _kc.set_zj_drawback(new decimal?(decimal.Parse(zj_ds)));
                            _kc.set_dl_rate(0f);
                            _kc.set_zd_rate(0f);
                            _kc.set_gd_rate((float) int.Parse(kc_rate.get_gdzc()));
                            _kc.set_fgs_rate((float) int.Parse(str7));
                            _kc.set_zj_rate((float) int.Parse(str6));
                            _kc.set_dl_name("");
                            _kc.set_zd_name(kc_rate.get_zdname());
                            _kc.set_gd_name(kc_rate.get_gdname());
                            _kc.set_fgs_name(kc_rate.get_fgsname());
                            _kc.set_is_payment(0);
                            _kc.set_sale_type(1);
                            _kc.set_m_type(-2);
                            _kc.set_kind(kc_kind);
                            _kc.set_ip(LSRequest.GetIP());
                            _kc.set_lottery_type(8);
                            _kc.set_lottery_name(base.GetGameNameByID(_kc.get_lottery_type().ToString()));
                            _kc.set_ordervalidcode(Utils.GetOrderValidCode(_kc.get_zd_name(), _kc.get_order_num(), _kc.get_bet_val(), _kc.get_odds(), _kc.get_kind(), Convert.ToInt32(_kc.get_phase_id()), Convert.ToInt32(_kc.get_odds_id()), Convert.ToDouble(_kc.get_amount())));
                            _kc.set_odds_zj(str5);
                            _kc.set_table_type(new int?(int.Parse(numTable)));
                            _kc.set_play_type(new int?(int.Parse(playtype)));
                            int num7 = 0;
                            CallBLL.cz_bet_kc_bll.AddBetByBH(_kc, ref num7);
                            double num8 = (num4 * double.Parse(str6)) / 100.0;
                            CallBLL.cz_odds_pkbjl_bll.Agent_UpdateGrandTotal(num8.ToString(), odds_id);
                            double num9 = double.Parse(playOddsInfo.Tables[0].Rows[0]["grand_total"].ToString()) + num8;
                            double num10 = double.Parse(playOddsInfo.Tables[0].Rows[0]["downbase"].ToString());
                            double num11 = Math.Floor((double) (num9 / num10));
                            if ((num11 > 1.0) && (num10 >= 1.0))
                            {
                                double num12 = double.Parse(playOddsInfo.Tables[0].Rows[0]["down_odds_rate"].ToString()) * num11;
                                CallBLL.cz_odds_pkbjl_bll.UpdateGrandTotalCurrentOdds(num12.ToString(), (num9 - (num10 * num11)).ToString(), odds_id.ToString());
                                cz_system_log _log = new cz_system_log();
                                _log.set_user_name("系統");
                                _log.set_children_name("");
                                _log.set_category(playOddsInfo.Tables[0].Rows[0]["category"].ToString());
                                _log.set_play_name(str2);
                                _log.set_put_amount(str3);
                                _log.set_l_name(LSKeys.get_PKBJL_Name());
                                _log.set_l_phase(phase);
                                _log.set_action("降賠率");
                                _log.set_odds_id(int.Parse(odds_id));
                                string str8 = playOddsInfo.Tables[0].Rows[0]["current_odds"].ToString();
                                _log.set_old_val(str8);
                                _log.set_new_val((double.Parse(str8) - num12).ToString());
                                _log.set_ip(LSRequest.GetIP());
                                _log.set_add_time(DateTime.Now);
                                _log.set_note("系統自動降賠");
                                _log.set_type_id(Convert.ToInt32((LSEnums.LogTypeID) 0));
                                _log.set_lottery_type(8);
                                CallBLL.cz_system_log_bll.Add(_log);
                                cz_jp_odds _odds = new cz_jp_odds();
                                _odds.set_add_time(DateTime.Now);
                                _odds.set_odds_id(int.Parse(odds_id));
                                _odds.set_phase_id(int.Parse(phase_id));
                                _odds.set_play_name(str2);
                                _odds.set_put_amount(str3);
                                _odds.set_odds(num12.ToString());
                                _odds.set_lottery_type(8);
                                _odds.set_phase(phase);
                                _odds.set_old_odds(str8);
                                _odds.set_new_odds((double.Parse(str8) - num12).ToString());
                                CallBLL.cz_jp_odds_bll.Add(_odds);
                            }
                        }
                    }
                }
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

