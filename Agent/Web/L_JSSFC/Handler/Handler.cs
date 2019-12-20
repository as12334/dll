namespace Agent.Web.L_JSSFC.Handler
{
    using Agent.Web.Handler;
    using Agent.Web.WebBase;
    using LotterySystem.Common;
    using LotterySystem.Model;
    using LotterySystem.WebPageBase;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Web;

    public class Handler : BaseHandler
    {
        private void FGSBH(agent_userinfo_session userModel, agent_kc_rate kc_rate, string kc_kind, HttpContext context, string phase_id, string phase, string play_id, string odds_id, string zj_ds, string fgs_ds, string gd_ds, string zd_ds, string dl_ds, string hy_ds, string checkcode)
        {
            if (!string.IsNullOrEmpty(kc_rate.get_fgsname()))
            {
                DataTable isAllowSale = CallBLL.cz_users_bll.GetIsAllowSale(kc_rate.get_fgsname());
                if ((isAllowSale.Rows[0]["kc_allow_sale"] != null) && (Convert.ToInt32(isAllowSale.Rows[0]["kc_allow_sale"]) != 0))
                {
                    double num = 1.0;
                    DataTable autosale = CallBLL.cz_autosale_jssfc_bll.GetAutosale(kc_rate.get_fgsname(), play_id);
                    if (((autosale != null) && (autosale.Rows.Count > 0)) && autosale.Rows[0]["sale_type"].ToString().Equals("0"))
                    {
                        DataTable table3 = CallBLL.cz_bet_kc_bll.GetSZZTBHByOddsid("fgs", kc_rate.get_fgsname(), odds_id, phase_id, 14);
                        if ((double.Parse(table3.Rows[0]["szzt"].ToString()) - num) > double.Parse(autosale.Rows[0]["max_money"].ToString()))
                        {
                            double num2 = double.Parse(table3.Rows[0]["szzt"].ToString());
                            double num3 = double.Parse(autosale.Rows[0]["max_money"].ToString());
                            double num4 = Math.Floor((double) (num2 - num3));
                            DataSet playOddsInfo = CallBLL.cz_play_jssfc_bll.GetPlayOddsInfo(odds_id);
                            string s = playOddsInfo.Tables[0].Rows[0]["current_odds"].ToString().Trim();
                            string str2 = playOddsInfo.Tables[0].Rows[0]["play_name"].ToString();
                            string str3 = playOddsInfo.Tables[0].Rows[0]["put_amount"].ToString();
                            string str4 = playOddsInfo.Tables[0].Rows[0][kc_kind.ToUpper() + "_diff"].ToString().Trim();
                            string str5 = (double.Parse(s) + double.Parse(str4)).ToString();
                            base.GetOdds_KC_EX(14, playOddsInfo.Tables[0].Rows[0]["current_odds"].ToString().Trim(), str4, ref s, odds_id);
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
                            _kc.set_lottery_type(14);
                            _kc.set_lottery_name(base.GetGameNameByID(_kc.get_lottery_type().ToString()));
                            _kc.set_isLM(0);
                            _kc.set_ordervalidcode(Utils.GetOrderValidCode(_kc.get_fgs_name(), _kc.get_order_num(), _kc.get_bet_val(), _kc.get_odds(), _kc.get_kind(), Convert.ToInt32(_kc.get_phase_id()), Convert.ToInt32(_kc.get_odds_id()), Convert.ToDouble(_kc.get_amount())));
                            _kc.set_odds_zj(str5);
                            int num5 = 0;
                            CallBLL.cz_bet_kc_bll.AddBetByBH(_kc, ref num5);
                            double num6 = (num4 * double.Parse(str6)) / 100.0;
                            CallBLL.cz_odds_jssfc_bll.Agent_UpdateGrandTotal(num6.ToString(), odds_id);
                            double num7 = double.Parse(playOddsInfo.Tables[0].Rows[0]["grand_total"].ToString()) + num6;
                            double num8 = double.Parse(playOddsInfo.Tables[0].Rows[0]["downbase"].ToString());
                            double num9 = Math.Floor((double) (num7 / num8));
                            if ((num9 > 1.0) && (num8 >= 1.0))
                            {
                                double num10 = double.Parse(playOddsInfo.Tables[0].Rows[0]["down_odds_rate"].ToString()) * num9;
                                CallBLL.cz_odds_jssfc_bll.UpdateGrandTotalCurrentOdds(num10.ToString(), (num7 - (num8 * num9)).ToString(), odds_id.ToString());
                                cz_system_log _log = new cz_system_log();
                                _log.set_user_name("系統");
                                _log.set_children_name("");
                                _log.set_category(playOddsInfo.Tables[0].Rows[0]["category"].ToString());
                                _log.set_play_name(str2);
                                _log.set_put_amount(str3);
                                _log.set_l_name(LSKeys.get_JSSFC_Name());
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
                                _log.set_lottery_type(14);
                                CallBLL.cz_system_log_bll.Add(_log);
                                cz_jp_odds _odds = new cz_jp_odds();
                                _odds.set_add_time(DateTime.Now);
                                _odds.set_odds_id(int.Parse(odds_id));
                                _odds.set_phase_id(int.Parse(phase_id));
                                _odds.set_play_name(str2);
                                _odds.set_put_amount(str3);
                                _odds.set_odds(num10.ToString());
                                _odds.set_lottery_type(14);
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

        protected void FGSBH_LM(agent_userinfo_session userModel, agent_kc_rate kc_rate, string kc_kind, HttpContext context, string phase_id, string phase, string play_id, string odds_id, string zj_ds, string fgs_ds, string gd_ds, string zd_ds, string dl_ds, string hy_ds, string checkcode)
        {
            if (!string.IsNullOrEmpty(kc_rate.get_fgsname()))
            {
                DataTable isAllowSale = CallBLL.cz_users_bll.GetIsAllowSale(kc_rate.get_fgsname());
                if ((isAllowSale.Rows[0]["kc_allow_sale"] != null) && (Convert.ToInt32(isAllowSale.Rows[0]["kc_allow_sale"]) != 0))
                {
                    double num = 1.0;
                    DataTable autosale = CallBLL.cz_autosale_jssfc_bll.GetAutosale(kc_rate.get_fgsname(), play_id);
                    if (((autosale != null) && (autosale.Rows.Count > 0)) && autosale.Rows[0]["sale_type"].ToString().Equals("0"))
                    {
                        DataTable table3 = CallBLL.cz_bet_kc_bll.GetBHGroup("fgs", kc_rate.get_fgsname(), odds_id, phase_id, autosale.Rows[0]["max_money"].ToString(), num.ToString(), 14);
                        if ((table3 != null) && (table3.Rows.Count > 0))
                        {
                            gd_ds = fgs_ds;
                            string s = kc_rate.get_zjzc();
                            kc_rate.get_fgszc();
                            if (kc_rate.get_zcyg() == "1")
                            {
                                s = 100.0.ToString();
                            }
                            else
                            {
                                100.0.ToString();
                            }
                            DataSet playOddsInfo = CallBLL.cz_play_jssfc_bll.GetPlayOddsInfo(odds_id);
                            string str2 = playOddsInfo.Tables[0].Rows[0]["current_odds"].ToString().Trim();
                            string str3 = playOddsInfo.Tables[0].Rows[0]["play_name"].ToString();
                            string str4 = playOddsInfo.Tables[0].Rows[0]["put_amount"].ToString();
                            string str5 = playOddsInfo.Tables[0].Rows[0][kc_kind.ToUpper() + "_diff"].ToString().Trim();
                            string str6 = (double.Parse(str2) + double.Parse(str5)).ToString();
                            base.GetOdds_KC_EX(14, playOddsInfo.Tables[0].Rows[0]["current_odds"].ToString().Trim(), str5, ref str2, odds_id);
                            if (userModel.get_kc_op_odds().Equals(1))
                            {
                                str2 = str6;
                            }
                            string nos = "";
                            foreach (DataRow row in table3.Rows)
                            {
                                double num4 = Math.Floor(double.Parse(row["bhl"].ToString().Trim()));
                                nos = row["item"].ToString().Trim();
                                string str8 = this.get_ball_pl(play_id, double.Parse(str2), nos);
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
                                _kc.set_play_name(str3);
                                _kc.set_bet_val(nos);
                                _kc.set_bet_wt(str8);
                                _kc.set_odds(str2);
                                _kc.set_amount(new decimal?(Convert.ToDecimal(num4.ToString())));
                                _kc.set_profit(0);
                                _kc.set_unit_cnt(1);
                                _kc.set_lm_type(0);
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
                                _kc.set_zj_rate((float) int.Parse(s));
                                _kc.set_dl_name("");
                                _kc.set_zd_name("");
                                _kc.set_gd_name("");
                                _kc.set_fgs_name(kc_rate.get_fgsname());
                                _kc.set_sale_type(1);
                                _kc.set_is_payment(0);
                                _kc.set_m_type(-2);
                                _kc.set_kind(kc_kind);
                                _kc.set_ip(LSRequest.GetIP());
                                _kc.set_lottery_type(14);
                                _kc.set_lottery_name(base.GetGameNameByID(_kc.get_lottery_type().ToString()));
                                _kc.set_isLM(1);
                                _kc.set_bet_group(nos);
                                _kc.set_ordervalidcode(Utils.GetOrderValidCode(_kc.get_fgs_name(), _kc.get_order_num(), _kc.get_bet_val(), _kc.get_odds(), _kc.get_kind(), Convert.ToInt32(_kc.get_phase_id()), Convert.ToInt32(_kc.get_odds_id()), Convert.ToDouble(_kc.get_amount())));
                                _kc.set_odds_zj(str6);
                                int num5 = 0;
                                CallBLL.cz_bet_kc_bll.AddBetByBH(_kc, ref num5);
                                double num6 = (double.Parse(num4.ToString()) * double.Parse(s)) / 100.0;
                                CallBLL.cz_odds_jssfc_bll.Agent_UpdateGrandTotal(num6.ToString(), odds_id);
                                double num7 = double.Parse(playOddsInfo.Tables[0].Rows[0]["grand_total"].ToString()) + num6;
                                double num8 = double.Parse(playOddsInfo.Tables[0].Rows[0]["downbase"].ToString());
                                double num9 = Math.Floor((double) (num7 / num8));
                                if ((num9 >= 1.0) && (num8 >= 1.0))
                                {
                                    double num10 = double.Parse(playOddsInfo.Tables[0].Rows[0]["down_odds_rate"].ToString()) * num9;
                                    CallBLL.cz_odds_jssfc_bll.UpdateGrandTotalCurrentOdds(num10.ToString(), (num7 - (num8 * num9)).ToString(), odds_id.ToString());
                                    cz_system_log _log = new cz_system_log();
                                    _log.set_user_name("系統");
                                    _log.set_children_name("");
                                    _log.set_category(playOddsInfo.Tables[0].Rows[0]["category"].ToString());
                                    _log.set_play_name(str3);
                                    _log.set_put_amount(str4);
                                    _log.set_l_name(LSKeys.get_JSSFC_Name());
                                    _log.set_l_phase(phase);
                                    _log.set_action("降賠率");
                                    _log.set_odds_id(int.Parse(odds_id));
                                    string str9 = playOddsInfo.Tables[0].Rows[0]["current_odds"].ToString();
                                    _log.set_old_val(str9);
                                    _log.set_new_val((double.Parse(str9) - num10).ToString());
                                    _log.set_ip(LSRequest.GetIP());
                                    _log.set_add_time(DateTime.Now);
                                    _log.set_note("系統自動降賠");
                                    _log.set_type_id(Convert.ToInt32((LSEnums.LogTypeID) 0));
                                    _log.set_lottery_type(14);
                                    CallBLL.cz_system_log_bll.Add(_log);
                                    cz_jp_odds _odds = new cz_jp_odds();
                                    _odds.set_add_time(DateTime.Now);
                                    _odds.set_odds_id(int.Parse(odds_id));
                                    _odds.set_phase_id(int.Parse(phase_id));
                                    _odds.set_play_name(str3);
                                    _odds.set_put_amount(str4);
                                    _odds.set_odds(num10.ToString());
                                    _odds.set_lottery_type(14);
                                    _odds.set_phase(phase);
                                    _odds.set_old_odds(str9);
                                    _odds.set_new_odds((double.Parse(str9) - num10).ToString());
                                    CallBLL.cz_jp_odds_bll.Add(_odds);
                                }
                                double num12 = 0.0;
                                double num13 = 0.0;
                                string[] strArray = str2.Split(new char[] { ',' });
                                num12 = double.Parse(strArray[0].ToString());
                                if (strArray.Length > 1)
                                {
                                    num13 = double.Parse(strArray[1].ToString());
                                }
                                double num14 = 0.0;
                                double num15 = 0.0;
                                string[] strArray2 = str6.Split(new char[] { ',' });
                                num14 = double.Parse(strArray2[0].ToString());
                                if (strArray2.Length > 1)
                                {
                                    num15 = double.Parse(strArray2[1].ToString());
                                }
                                cz_splitgroup_jssfc _jssfc = new cz_splitgroup_jssfc();
                                _jssfc.set_odds_id(new int?(int.Parse(odds_id)));
                                _jssfc.set_item_money(new decimal?(Convert.ToDecimal(num4)));
                                _jssfc.set_odds1(new decimal?(Convert.ToDecimal(num12.ToString())));
                                _jssfc.set_odds2(new decimal?(Convert.ToDecimal(num13.ToString())));
                                _jssfc.set_bet_id(new int?(num5));
                                _jssfc.set_item(nos);
                                _jssfc.set_odds1_zj(new decimal?(Convert.ToDecimal(num14.ToString())));
                                _jssfc.set_odds2_zj(new decimal?(Convert.ToDecimal(num15.ToString())));
                                CallBLL.cz_splitgroup_jssfc_bll.Agent_AddSplitGroup(_jssfc);
                            }
                        }
                    }
                }
            }
        }

        private void GDBH(agent_userinfo_session userModel, agent_kc_rate kc_rate, string kc_kind, HttpContext context, string phase_id, string phase, string play_id, string odds_id, string zj_ds, string fgs_ds, string gd_ds, string zd_ds, string dl_ds, string hy_ds, string checkcode)
        {
            if (!string.IsNullOrEmpty(kc_rate.get_gdname()))
            {
                DataTable isAllowSale = CallBLL.cz_users_bll.GetIsAllowSale(kc_rate.get_gdname());
                if ((isAllowSale.Rows[0]["kc_allow_sale"] != null) && (Convert.ToInt32(isAllowSale.Rows[0]["kc_allow_sale"]) != 0))
                {
                    double num = 1.0;
                    DataTable autosale = CallBLL.cz_autosale_jssfc_bll.GetAutosale(kc_rate.get_gdname(), play_id);
                    if (((autosale != null) && (autosale.Rows.Count > 0)) && autosale.Rows[0]["sale_type"].ToString().Equals("0"))
                    {
                        DataTable table3 = CallBLL.cz_bet_kc_bll.GetSZZTBHByOddsid("gd", kc_rate.get_gdname(), odds_id, phase_id, 14);
                        if ((double.Parse(table3.Rows[0]["szzt"].ToString()) - num) > double.Parse(autosale.Rows[0]["max_money"].ToString()))
                        {
                            double num2 = double.Parse(table3.Rows[0]["szzt"].ToString());
                            double num3 = double.Parse(autosale.Rows[0]["max_money"].ToString());
                            double num4 = Math.Floor((double) (num2 - num3));
                            DataSet playOddsInfo = CallBLL.cz_play_jssfc_bll.GetPlayOddsInfo(odds_id);
                            string s = playOddsInfo.Tables[0].Rows[0]["current_odds"].ToString().Trim();
                            string str2 = playOddsInfo.Tables[0].Rows[0]["play_name"].ToString();
                            string str3 = playOddsInfo.Tables[0].Rows[0]["put_amount"].ToString();
                            string str4 = playOddsInfo.Tables[0].Rows[0][kc_kind.ToUpper() + "_diff"].ToString().Trim();
                            string str5 = (double.Parse(s) + double.Parse(str4)).ToString();
                            base.GetOdds_KC_EX(14, playOddsInfo.Tables[0].Rows[0]["current_odds"].ToString().Trim(), str4, ref s, odds_id);
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
                            _kc.set_lottery_type(14);
                            _kc.set_lottery_name(base.GetGameNameByID(_kc.get_lottery_type().ToString()));
                            _kc.set_isLM(0);
                            _kc.set_ordervalidcode(Utils.GetOrderValidCode(_kc.get_gd_name(), _kc.get_order_num(), _kc.get_bet_val(), _kc.get_odds(), _kc.get_kind(), Convert.ToInt32(_kc.get_phase_id()), Convert.ToInt32(_kc.get_odds_id()), Convert.ToDouble(_kc.get_amount())));
                            _kc.set_odds_zj(str5);
                            int num7 = 0;
                            CallBLL.cz_bet_kc_bll.AddBetByBH(_kc, ref num7);
                            double num8 = (num4 * double.Parse(str6)) / 100.0;
                            CallBLL.cz_odds_jssfc_bll.Agent_UpdateGrandTotal(num8.ToString(), odds_id);
                            double num9 = double.Parse(playOddsInfo.Tables[0].Rows[0]["grand_total"].ToString()) + num8;
                            double num10 = double.Parse(playOddsInfo.Tables[0].Rows[0]["downbase"].ToString());
                            double num11 = Math.Floor((double) (num9 / num10));
                            if ((num11 > 1.0) && (num10 >= 1.0))
                            {
                                double num12 = double.Parse(playOddsInfo.Tables[0].Rows[0]["down_odds_rate"].ToString()) * num11;
                                CallBLL.cz_odds_jssfc_bll.UpdateGrandTotalCurrentOdds(num12.ToString(), (num9 - (num10 * num11)).ToString(), odds_id.ToString());
                                cz_system_log _log = new cz_system_log();
                                _log.set_user_name("系統");
                                _log.set_children_name("");
                                _log.set_category(playOddsInfo.Tables[0].Rows[0]["category"].ToString());
                                _log.set_play_name(str2);
                                _log.set_put_amount(str3);
                                _log.set_l_name(LSKeys.get_JSSFC_Name());
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
                                _log.set_lottery_type(14);
                                CallBLL.cz_system_log_bll.Add(_log);
                                cz_jp_odds _odds = new cz_jp_odds();
                                _odds.set_add_time(DateTime.Now);
                                _odds.set_odds_id(int.Parse(odds_id));
                                _odds.set_phase_id(int.Parse(phase_id));
                                _odds.set_play_name(str2);
                                _odds.set_put_amount(str3);
                                _odds.set_odds(num12.ToString());
                                _odds.set_lottery_type(14);
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

        protected void GDBH_LM(agent_userinfo_session userModel, agent_kc_rate kc_rate, string kc_kind, HttpContext context, string phase_id, string phase, string play_id, string odds_id, string zj_ds, string fgs_ds, string gd_ds, string zd_ds, string dl_ds, string hy_ds, string checkcode)
        {
            if (!string.IsNullOrEmpty(kc_rate.get_gdname()))
            {
                DataTable isAllowSale = CallBLL.cz_users_bll.GetIsAllowSale(kc_rate.get_gdname());
                if ((isAllowSale.Rows[0]["kc_allow_sale"] != null) && (Convert.ToInt32(isAllowSale.Rows[0]["kc_allow_sale"]) != 0))
                {
                    double num = 1.0;
                    DataTable autosale = CallBLL.cz_autosale_jssfc_bll.GetAutosale(kc_rate.get_gdname(), play_id);
                    if (((autosale != null) && (autosale.Rows.Count > 0)) && autosale.Rows[0]["sale_type"].ToString().Equals("0"))
                    {
                        DataTable table3 = CallBLL.cz_bet_kc_bll.GetBHGroup("gd", kc_rate.get_gdname(), odds_id, phase_id, autosale.Rows[0]["max_money"].ToString(), num.ToString(), 14);
                        if ((table3 != null) && (table3.Rows.Count > 0))
                        {
                            zd_ds = gd_ds;
                            string s = kc_rate.get_zjzc();
                            string str2 = kc_rate.get_fgszc();
                            if (kc_rate.get_zcyg() == "1")
                            {
                                s = (100.0 - double.Parse(kc_rate.get_fgszc())).ToString();
                            }
                            else
                            {
                                str2 = (100.0 - double.Parse(kc_rate.get_zjzc())).ToString();
                            }
                            DataSet playOddsInfo = CallBLL.cz_play_jssfc_bll.GetPlayOddsInfo(odds_id);
                            string str3 = playOddsInfo.Tables[0].Rows[0]["current_odds"].ToString().Trim();
                            string str4 = playOddsInfo.Tables[0].Rows[0]["play_name"].ToString();
                            string str5 = playOddsInfo.Tables[0].Rows[0]["put_amount"].ToString();
                            string str6 = playOddsInfo.Tables[0].Rows[0][kc_kind.ToUpper() + "_diff"].ToString().Trim();
                            string str7 = (double.Parse(str3) + double.Parse(str6)).ToString();
                            base.GetOdds_KC_EX(14, playOddsInfo.Tables[0].Rows[0]["current_odds"].ToString().Trim(), str6, ref str3, odds_id);
                            string nos = "";
                            foreach (DataRow row in table3.Rows)
                            {
                                double num4 = Math.Floor(double.Parse(row["bhl"].ToString().Trim()));
                                nos = row["item"].ToString().Trim();
                                string str9 = this.get_ball_pl(play_id, double.Parse(str3), nos);
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
                                _kc.set_play_name(playOddsInfo.Tables[0].Rows[0]["play_name"].ToString());
                                _kc.set_bet_val(nos);
                                _kc.set_bet_wt(str9);
                                _kc.set_odds(str3);
                                _kc.set_amount(new decimal?(Convert.ToDecimal(num4.ToString())));
                                _kc.set_profit(0);
                                _kc.set_unit_cnt(1);
                                _kc.set_lm_type(0);
                                _kc.set_hy_drawback(0);
                                _kc.set_dl_drawback(0);
                                _kc.set_zd_drawback(new decimal?(decimal.Parse(zd_ds)));
                                _kc.set_gd_drawback(new decimal?(decimal.Parse(gd_ds)));
                                _kc.set_fgs_drawback(new decimal?(decimal.Parse(fgs_ds)));
                                _kc.set_zj_drawback(new decimal?(decimal.Parse(zj_ds)));
                                _kc.set_dl_rate(0f);
                                _kc.set_zd_rate(0f);
                                _kc.set_gd_rate(0f);
                                _kc.set_fgs_rate((float) int.Parse(str2));
                                _kc.set_zj_rate((float) int.Parse(s));
                                _kc.set_dl_name("");
                                _kc.set_zd_name("");
                                _kc.set_gd_name(kc_rate.get_gdname());
                                _kc.set_fgs_name(kc_rate.get_fgsname());
                                _kc.set_is_payment(0);
                                _kc.set_sale_type(1);
                                _kc.set_m_type(-2);
                                _kc.set_kind(kc_kind);
                                _kc.set_ip(LSRequest.GetIP());
                                _kc.set_lottery_type(14);
                                _kc.set_lottery_name(base.GetGameNameByID(_kc.get_lottery_type().ToString()));
                                _kc.set_isLM(1);
                                _kc.set_bet_group(nos);
                                _kc.set_ordervalidcode(Utils.GetOrderValidCode(_kc.get_gd_name(), _kc.get_order_num(), _kc.get_bet_val(), _kc.get_odds(), _kc.get_kind(), Convert.ToInt32(_kc.get_phase_id()), Convert.ToInt32(_kc.get_odds_id()), Convert.ToDouble(_kc.get_amount())));
                                _kc.set_odds_zj(str7);
                                int num5 = 0;
                                CallBLL.cz_bet_kc_bll.AddBetByBH(_kc, ref num5);
                                double num6 = (double.Parse(num4.ToString()) * double.Parse(s)) / 100.0;
                                CallBLL.cz_odds_jssfc_bll.Agent_UpdateGrandTotal(num6.ToString(), odds_id);
                                double num7 = double.Parse(playOddsInfo.Tables[0].Rows[0]["grand_total"].ToString()) + num6;
                                double num8 = double.Parse(playOddsInfo.Tables[0].Rows[0]["downbase"].ToString());
                                double num9 = Math.Floor((double) (num7 / num8));
                                if ((num9 >= 1.0) && (num8 >= 1.0))
                                {
                                    double num10 = double.Parse(playOddsInfo.Tables[0].Rows[0]["down_odds_rate"].ToString()) * num9;
                                    CallBLL.cz_odds_jssfc_bll.UpdateGrandTotalCurrentOdds(num10.ToString(), (num7 - (num8 * num9)).ToString(), odds_id.ToString());
                                    cz_system_log _log = new cz_system_log();
                                    _log.set_user_name("系統");
                                    _log.set_children_name("");
                                    _log.set_category(playOddsInfo.Tables[0].Rows[0]["category"].ToString());
                                    _log.set_play_name(str4);
                                    _log.set_put_amount(str5);
                                    _log.set_l_name(LSKeys.get_JSSFC_Name());
                                    _log.set_l_phase(phase);
                                    _log.set_action("降賠率");
                                    _log.set_odds_id(int.Parse(odds_id));
                                    string str10 = playOddsInfo.Tables[0].Rows[0]["current_odds"].ToString();
                                    _log.set_old_val(str10);
                                    _log.set_new_val((double.Parse(str10) - num10).ToString());
                                    _log.set_ip(LSRequest.GetIP());
                                    _log.set_add_time(DateTime.Now);
                                    _log.set_note("系統自動降賠");
                                    _log.set_type_id(Convert.ToInt32((LSEnums.LogTypeID) 0));
                                    _log.set_lottery_type(14);
                                    CallBLL.cz_system_log_bll.Add(_log);
                                    cz_jp_odds _odds = new cz_jp_odds();
                                    _odds.set_add_time(DateTime.Now);
                                    _odds.set_odds_id(int.Parse(odds_id));
                                    _odds.set_phase_id(int.Parse(phase_id));
                                    _odds.set_play_name(str4);
                                    _odds.set_put_amount(str5);
                                    _odds.set_odds(num10.ToString());
                                    _odds.set_lottery_type(14);
                                    _odds.set_phase(phase);
                                    _odds.set_old_odds(str10);
                                    _odds.set_new_odds((double.Parse(str10) - num10).ToString());
                                    CallBLL.cz_jp_odds_bll.Add(_odds);
                                }
                                double num12 = 0.0;
                                double num13 = 0.0;
                                string[] strArray = str3.Split(new char[] { ',' });
                                num12 = double.Parse(strArray[0].ToString());
                                if (strArray.Length > 1)
                                {
                                    num13 = double.Parse(strArray[1].ToString());
                                }
                                double num14 = 0.0;
                                double num15 = 0.0;
                                string[] strArray2 = str7.Split(new char[] { ',' });
                                num14 = double.Parse(strArray2[0].ToString());
                                if (strArray2.Length > 1)
                                {
                                    num15 = double.Parse(strArray2[1].ToString());
                                }
                                cz_splitgroup_jssfc _jssfc = new cz_splitgroup_jssfc();
                                _jssfc.set_odds_id(new int?(int.Parse(odds_id)));
                                _jssfc.set_item_money(new decimal?(Convert.ToDecimal(num4)));
                                _jssfc.set_odds1(new decimal?(Convert.ToDecimal(num12.ToString())));
                                _jssfc.set_odds2(new decimal?(Convert.ToDecimal(num13.ToString())));
                                _jssfc.set_bet_id(new int?(num5));
                                _jssfc.set_item(nos);
                                _jssfc.set_odds1_zj(new decimal?(Convert.ToDecimal(num14.ToString())));
                                _jssfc.set_odds2_zj(new decimal?(Convert.ToDecimal(num15.ToString())));
                                CallBLL.cz_splitgroup_jssfc_bll.Agent_AddSplitGroup(_jssfc);
                            }
                        }
                    }
                }
            }
        }

        public string get_ball_pl(string playid, double pl, string nos)
        {
            List<string> list = new List<string>();
            DataTable table = CallBLL.cz_wt_jssfc_bll.GetWtByPlayID(int.Parse(playid), nos).Tables[0];
            foreach (string str in nos.Split(new char[] { ',' }))
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

        public void get_clyl(HttpContext context, ref string strResult)
        {
            ReturnResult result = new ReturnResult();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("type", "get_clyl");
            string str = "_jssfc_lmcl";
            string str2 = "_jssfc_lryl";
            Dictionary<string, object> source = new Dictionary<string, object>();
            if (CacheHelper.GetCache("balance_kc_FileCacheKey" + str2) != null)
            {
                source = CacheHelper.GetCache("balance_kc_FileCacheKey" + str2) as Dictionary<string, object>;
            }
            else
            {
                DataSet set = CallBLL.cz_phase_jssfc_bll.Agent_GetTopPhase(40);
                int index = 0;
                string[] strArray = new string[40];
                string[] strArray2 = new string[] { 
                    "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12", "13", "14", "15", "16", 
                    "17", "18", "19", "20"
                 };
                string[] strArray3 = new string[20];
                int num2 = 0;
                if ((set != null) && (set.Tables.Count > 0))
                {
                    DataTable table = set.Tables[0];
                    foreach (DataRow row in table.Rows)
                    {
                        strArray[index] = row["n1"].ToString() + "," + row["n2"].ToString() + "," + row["n3"].ToString() + "," + row["n4"].ToString() + "," + row["n5"].ToString() + "," + row["n6"].ToString() + "," + row["n7"].ToString() + "," + row["n8"].ToString();
                        index++;
                    }
                    for (int i = 0; i < 20; i++)
                    {
                        num2 = 0;
                        foreach (string str3 in strArray)
                        {
                            if (string.IsNullOrEmpty(str3) || (str3.IndexOf(strArray2[i].ToString()) > -1))
                            {
                                break;
                            }
                            num2++;
                        }
                        strArray3[i] = num2.ToString();
                    }
                    int num4 = 1;
                    foreach (string str4 in strArray3)
                    {
                        if (num4 < 10)
                        {
                            source.Add("0" + num4, str4);
                        }
                        else
                        {
                            source.Add(num4.ToString(), str4);
                        }
                        num4++;
                    }
                }
                CacheHelper.SetCache("balance_kc_FileCacheKey" + str2, source);
                CacheHelper.SetPublicFileCache("balance_kc_FileCacheKey" + str2, source, PageBase.GetPublicForderPath(base.get_KC_BalanceFileName()));
            }
            dictionary.Add("dropball", source.ToArray<KeyValuePair<string, object>>());
            List<object> cache = new List<object>();
            if (CacheHelper.GetCache("balance_kc_FileCacheKey" + str) != null)
            {
                cache = CacheHelper.GetCache("balance_kc_FileCacheKey" + str) as List<object>;
            }
            else
            {
                DataTable table2 = CallBLL.cz_phase_jssfc_bll.GetChangLong().Tables[0];
                if (table2.Rows.Count > 0)
                {
                    Dictionary<string, object> dictionary3 = new Dictionary<string, object>();
                    foreach (DataRow row2 in table2.Rows)
                    {
                        string str5 = row2["c_name"].ToString();
                        string str6 = row2["c_qs"].ToString();
                        dictionary3.Add("cl_name", str5);
                        dictionary3.Add("cl_num", str6);
                        cache.Add(new Dictionary<string, object>(dictionary3));
                        dictionary3.Clear();
                    }
                }
                CacheHelper.SetCache("balance_kc_FileCacheKey" + str, cache);
                CacheHelper.SetPublicFileCache("balance_kc_FileCacheKey" + str, cache, PageBase.GetPublicForderPath(base.get_KC_BalanceFileName()));
            }
            dictionary.Add("lmcl", cache);
            result.set_data(dictionary);
            strResult = JsonHandle.ObjectToJson(result);
        }

        public void get_lmbhlist(HttpContext context, ref string strResult)
        {
            string str = context.Session["user_name"].ToString();
            agent_userinfo_session model = context.Session[str + "lottery_session_user_info"] as agent_userinfo_session;
            string str2 = base.Permission_Ashx_ZJ(model, "po_1_1");
            string str3 = base.Permission_Ashx_DL(model, "po_5_1");
            if (!string.IsNullOrEmpty(str2) || !string.IsNullOrEmpty(str3))
            {
                context.Response.Write(PageBase.GetMessageByCache("u100027", "MessageHint"));
                context.Response.End();
            }
            ReturnResult result = new ReturnResult();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("type", "get_lmbhlist");
            result.set_success(400);
            LSRequest.qq("playid");
            string str4 = LSRequest.qq("pk");
            string s = LSRequest.qq("szsz");
            string str6 = LSRequest.qq("phaseid");
            string str7 = LSRequest.qq("oddsid");
            string str8 = str4;
            if (string.IsNullOrEmpty(str8))
            {
                str8 = "A";
            }
            else
            {
                str8 = str8.ToUpper();
            }
            if (!string.IsNullOrEmpty(str7))
            {
                string str9 = "";
                string str10 = model.get_u_type();
                string str11 = model.get_u_name();
                if ((model.get_u_type() == "fgs") && ((model.get_allow_view_report() == 1) && int.Parse(s).Equals(2)))
                {
                    str10 = "zj";
                    str11 = model.get_zjname().ToString();
                    s = "1";
                }
                DataSet set = CallBLL.cz_bet_kc_bll.GetLMBHList(str10, str11, int.Parse(s), Convert.ToInt32(str6), str9, model.get_zjname().ToString(), Convert.ToInt32(str7), 14, "jssfc", str4);
                if (((set != null) && (set.Tables.Count > 0)) && (set.Tables[0].Rows.Count > 0))
                {
                    Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
                    foreach (DataRow row in set.Tables[0].Rows)
                    {
                        string str12 = string.Concat(new object[] { Math.Round(double.Parse(row["hl"].ToString()), 3).ToString(), ",", Utils.GetMathRound(double.Parse(row["ky"].ToString()), 3), ",", Math.Round(double.Parse(row["ds"].ToString()), 3) });
                        dictionary2.Add(row["item"].ToString(), str12);
                    }
                    dictionary.Add("lmbhlist", dictionary2);
                }
                result.set_success(200);
            }
            else
            {
                result.set_tipinfo(PageBase.GetMessageByCache("u100008", "MessageHint"));
            }
            result.set_data(dictionary);
            strResult = JsonHandle.ObjectToJson(result);
        }

        public void get_oddsinfo(HttpContext context, ref string strResult)
        {
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
            string str8 = LSRequest.qq("isbh");
            LSRequest.qq("oddsid");
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
                string str9 = model.get_u_type().Trim().ToString();
                string str10 = model.get_u_name().ToString();
                string str11 = str6;
                if (string.IsNullOrEmpty(str6))
                {
                    str11 = "A";
                }
                else
                {
                    str11 = str11.ToUpper();
                }
                if ((!str11.Equals("A") && !str11.Equals("B")) && !str11.Equals("C"))
                {
                    context.Response.End();
                }
                ReturnResult result = new ReturnResult();
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                dictionary.Add("type", "get_oddsinfo");
                dictionary.Add("playpage", str7);
                DataSet set = CallBLL.cz_play_jssfc_bll.Agent_GetPlay(str4, str10, str9, str7);
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
                        string str12 = row["openning"].ToString();
                        string str13 = row["isopen"].ToString();
                        string str14 = row["opendate"].ToString();
                        string str15 = (row["endtime"].ToString() != "") ? Convert.ToDateTime(row["endtime"].ToString()).ToString("HH:mm:ss") : row["endtime"].ToString();
                        string str16 = row["nn"].ToString();
                        string str17 = row["p_id"].ToString();
                        string str18 = row["upopenphase"].ToString();
                        string str19 = row["upopennumber"].ToString();
                        dictionary.Add("openning", str12);
                        dictionary.Add("isopen", str13);
                        dictionary.Add("drawopen_time", str14);
                        dictionary.Add("stop_time", str15);
                        dictionary.Add("nn", str16);
                        dictionary.Add("p_id", str17);
                        dictionary.Add("profit", base.GetKCProfit());
                        dictionary.Add("upopenphase", str18);
                        dictionary.Add("upopennumber", str19);
                        dictionary.Add("jeucode", base.get_JeuValidate());
                        Dictionary<string, object> dictionary2 = base.GetOdds_KC(14, oddsDT, str11, str12, null);
                        dictionary.Add("play_odds", dictionary2);
                        Dictionary<string, object> dictionary3 = new Dictionary<string, object>();
                        string str20 = "";
                        new Dictionary<string, object>();
                        string str21 = "";
                        if (model.get_u_type() == "fgs")
                        {
                            nullable = model.get_allow_view_report();
                            if (((nullable.GetValueOrDefault() == 1) && nullable.HasValue) && int.Parse(s).Equals(2))
                            {
                                str9 = "zj";
                                str10 = model.get_zjname().ToString();
                                s = "1";
                            }
                        }
                        DataSet set2 = CallBLL.cz_bet_kc_bll.GetSZSZ(str9, str10, int.Parse(s), Convert.ToInt32(str17), 14, str4, str21, model.get_zjname().ToString(), num, "jssfc", str6, null, null);
                        DataTable table4 = null;
                        if (str8.Equals("1"))
                        {
                            int num24 = 14;
                            table4 = CallBLL.cz_bet_kc_bll.GetBHSZZT(str10, str9, str17, str21, str4, model.get_zjname().ToString(), num, num24.ToString(), "jssfc", str6, null, null).Tables[0];
                        }
                        if ((set2 != null) && (set2.Tables.Count > 0))
                        {
                            DataTable table5 = set2.Tables[0];
                            foreach (DataRow row2 in table5.Rows)
                            {
                                string str22 = "0";
                                if (!row2["bs"].ToString().Equals("0"))
                                {
                                    if (str8.Equals("1"))
                                    {
                                        foreach (DataRow row3 in table4.Select(string.Format("odds_id={0}", Convert.ToInt32(row2["odds_id"].ToString()))))
                                        {
                                            str22 = row3["szzt"].ToString();
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
                                    strArray[8] = Utils.GetMathRound(double.Parse(str22), nullable);
                                    str20 = string.Concat(strArray);
                                    dictionary3.Add(key, str20);
                                    if (Convert.ToInt32(row2["max_id"]) > num)
                                    {
                                        num = Convert.ToInt32(row2["max_id"]);
                                    }
                                }
                            }
                        }
                        dictionary.Add("szsz_amount", dictionary3);
                        Dictionary<string, object> dictionary4 = new Dictionary<string, object>();
                        DataSet set4 = CallBLL.cz_bet_kc_bll.GetSZSZCount(str9, str10, int.Parse(s), Convert.ToInt32(str17), 14, "jssfc", str6, null, null);
                        if ((set4 != null) && (set4.Tables.Count > 0))
                        {
                            DataTable table6 = set4.Tables[0];
                            double d = 0.0;
                            double num4 = 0.0;
                            double num5 = 0.0;
                            double num6 = 0.0;
                            double num7 = 0.0;
                            double num8 = 0.0;
                            double num9 = 0.0;
                            double num10 = 0.0;
                            double num11 = 0.0;
                            double num12 = 0.0;
                            double num13 = 0.0;
                            double num14 = 0.0;
                            double num15 = 0.0;
                            double num16 = 0.0;
                            double num17 = 0.0;
                            double num18 = 0.0;
                            double num19 = 0.0;
                            double num20 = 0.0;
                            double num21 = 0.0;
                            double num22 = 0.0;
                            for (int i = 0; i < table6.Rows.Count; i++)
                            {
                                string str24 = table6.Rows[i]["play_name"].ToString();
                                if (str24.IndexOf("第一球") > -1)
                                {
                                    d += double.Parse(table6.Rows[i]["szzt"].ToString());
                                }
                                if (str24.IndexOf("第二球") > -1)
                                {
                                    num4 += double.Parse(table6.Rows[i]["szzt"].ToString());
                                }
                                if (str24.IndexOf("第三球") > -1)
                                {
                                    num5 += double.Parse(table6.Rows[i]["szzt"].ToString());
                                }
                                if (str24.IndexOf("第四球") > -1)
                                {
                                    num6 += double.Parse(table6.Rows[i]["szzt"].ToString());
                                }
                                if (str24.IndexOf("第五球") > -1)
                                {
                                    num7 += double.Parse(table6.Rows[i]["szzt"].ToString());
                                }
                                if (str24.IndexOf("第六球") > -1)
                                {
                                    num8 += double.Parse(table6.Rows[i]["szzt"].ToString());
                                }
                                if (str24.IndexOf("第七球") > -1)
                                {
                                    num9 += double.Parse(table6.Rows[i]["szzt"].ToString());
                                }
                                if (str24.IndexOf("第八球") > -1)
                                {
                                    num10 += double.Parse(table6.Rows[i]["szzt"].ToString());
                                }
                                if (str24.IndexOf("總和大小") > -1)
                                {
                                    num11 += double.Parse(table6.Rows[i]["szzt"].ToString());
                                }
                                if (str24.IndexOf("總和單雙") > -1)
                                {
                                    num12 += double.Parse(table6.Rows[i]["szzt"].ToString());
                                }
                                if (str24.IndexOf("總和尾數大小") > -1)
                                {
                                    num13 += double.Parse(table6.Rows[i]["szzt"].ToString());
                                }
                                if (str24.IndexOf("任選二") > -1)
                                {
                                    num14 += double.Parse(table6.Rows[i]["szzt"].ToString());
                                }
                                if (str24.IndexOf("選二連直") > -1)
                                {
                                    num15 += double.Parse(table6.Rows[i]["szzt"].ToString());
                                }
                                if (str24.IndexOf("選二連組") > -1)
                                {
                                    num16 += double.Parse(table6.Rows[i]["szzt"].ToString());
                                }
                                if (str24.IndexOf("任選三") > -1)
                                {
                                    num17 += double.Parse(table6.Rows[i]["szzt"].ToString());
                                }
                                if (str24.IndexOf("選三前直") > -1)
                                {
                                    num18 += double.Parse(table6.Rows[i]["szzt"].ToString());
                                }
                                if (str24.IndexOf("選三前組") > -1)
                                {
                                    num19 += double.Parse(table6.Rows[i]["szzt"].ToString());
                                }
                                if (str24.IndexOf("任選四") > -1)
                                {
                                    num20 += double.Parse(table6.Rows[i]["szzt"].ToString());
                                }
                                if (str24.IndexOf("任選五") > -1)
                                {
                                    num21 += double.Parse(table6.Rows[i]["szzt"].ToString());
                                }
                                if (str24.IndexOf("龍虎") > -1)
                                {
                                    num22 += double.Parse(table6.Rows[i]["szzt"].ToString());
                                }
                            }
                            dictionary4.Add("81", Math.Floor(d));
                            dictionary4.Add("86", Math.Floor(num4));
                            dictionary4.Add("91", Math.Floor(num5));
                            dictionary4.Add("96", Math.Floor(num6));
                            dictionary4.Add("101", Math.Floor(num7));
                            dictionary4.Add("106", Math.Floor(num8));
                            dictionary4.Add("111", Math.Floor(num9));
                            dictionary4.Add("116", Math.Floor(num10));
                            dictionary4.Add("1011", Math.Floor(num11));
                            dictionary4.Add("1012", Math.Floor(num12));
                            dictionary4.Add("1013", Math.Floor(num13));
                            dictionary4.Add("72", Math.Floor(num14));
                            dictionary4.Add("73", Math.Floor(num15));
                            dictionary4.Add("74", Math.Floor(num16));
                            dictionary4.Add("75", Math.Floor(num17));
                            dictionary4.Add("76", Math.Floor(num18));
                            dictionary4.Add("77", Math.Floor(num19));
                            dictionary4.Add("78", Math.Floor(num20));
                            dictionary4.Add("79", Math.Floor(num21));
                            dictionary4.Add("80", Math.Floor(num22));
                        }
                        dictionary.Add("szsz_amount_count", dictionary4);
                        Dictionary<string, object> dictionary5 = new Dictionary<string, object>();
                        if (table3 != null)
                        {
                            foreach (DataRow row4 in table3.Rows)
                            {
                                dictionary5.Add(row4["play_id"].ToString(), "-" + row4["above_quota"].ToString());
                            }
                        }
                        dictionary.Add("abovevalid", dictionary5);
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
                new List<object>();
                DataTable openNumber = base.GetOpenNumber(14, null);
                Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
                if ((openNumber != null) && (openNumber.Rows.Count > 0))
                {
                    string keepDecimalNumber = Utils.GetKeepDecimalNumber(1, base.GetKCProfit());
                    string str3 = openNumber.Rows[0]["upopenphase"].ToString();
                    string str4 = openNumber.Rows[0]["upopennumber"].ToString();
                    dictionary2.Add("profit", keepDecimalNumber);
                    dictionary2.Add("upopenphase", str3);
                    dictionary2.Add("upopennumber", str4);
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
                string str12;
                double num39;
                cz_odds_jssfc oldModel = null;
                if ((str3.Equals("1") || str3.Equals("2")) || (str3.Equals("3") || str3.Equals("4")))
                {
                    oldModel = CallBLL.cz_odds_jssfc_bll.GetModel(Convert.ToInt32(str4));
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
                        double num11 = Convert.ToDouble(oldModel.get_max_odds());
                        double num12 = Convert.ToDouble(oldModel.get_min_odds());
                        double num13 = 0.0;
                        if (str5.Equals("A"))
                        {
                            num13 = Convert.ToDouble(oldModel.get_a_diff());
                        }
                        else if (str5.Equals("B"))
                        {
                            num13 = Convert.ToDouble(oldModel.get_b_diff());
                        }
                        else if (str5.Equals("C"))
                        {
                            num13 = Convert.ToDouble(oldModel.get_c_diff());
                        }
                        num39 = double.Parse(s) - num13;
                        s = num39.ToString();
                        if (Convert.ToDouble(s) < num12)
                        {
                            result.set_success(400);
                            num39 = num12 + num13;
                            result.set_tipinfo(string.Format(PageBase.GetMessageByCache("u100012", "MessageHint"), num39.ToString()));
                            strResult = JsonHandle.ObjectToJson(result);
                        }
                        else if (Convert.ToDouble(s) > num11)
                        {
                            result.set_success(400);
                            num39 = num11 + num13;
                            result.set_tipinfo(string.Format(PageBase.GetMessageByCache("u100010", "MessageHint"), num39.ToString()));
                            strResult = JsonHandle.ObjectToJson(result);
                        }
                        else if (CallBLL.cz_odds_jssfc_bll.UpdateCurrentOdds(Convert.ToInt32(str4), Convert.ToDouble(s), str3) > 0)
                        {
                            cz_odds_jssfc newModel = CallBLL.cz_odds_jssfc_bll.GetModel(Convert.ToInt32(str4));
                            base.jssfc_log(oldModel, newModel, str3);
                            double num15 = Convert.ToDouble(newModel.get_current_odds());
                            double num16 = Convert.ToDouble(newModel.get_max_odds());
                            double num17 = Convert.ToDouble(newModel.get_min_odds());
                            double num18 = 0.0;
                            if (str5.Equals("A"))
                            {
                                num18 = Convert.ToDouble(newModel.get_a_diff());
                            }
                            else if (str5.Equals("B"))
                            {
                                num18 = Convert.ToDouble(newModel.get_b_diff());
                            }
                            else if (str5.Equals("C"))
                            {
                                num18 = Convert.ToDouble(newModel.get_c_diff());
                            }
                            num39 = num15 + num18;
                            num15 = double.Parse(num39.ToString());
                            string str9 = newModel.get_is_open().ToString();
                            Dictionary<string, object> dictionary4 = new Dictionary<string, object>();
                            Dictionary<string, object> dictionary5 = new Dictionary<string, object>();
                            dictionary5.Add("pl", num15);
                            num39 = num16 + num18;
                            dictionary5.Add("maxpl", num39.ToString());
                            num39 = num17 + num18;
                            dictionary5.Add("minpl", num39.ToString());
                            dictionary5.Add("plx", new List<double>());
                            DataTable table2 = CallBLL.cz_phase_jssfc_bll.IsPhaseClose();
                            if (table2.Rows[0]["isopen"].ToString().Equals("0") && string.IsNullOrEmpty(table2.Rows[0]["endtime"].ToString()))
                            {
                                str9 = "0";
                            }
                            if (table2.Rows[0]["openning"].ToString().Equals("n"))
                            {
                                str9 = "0";
                            }
                            dictionary5.Add("is_open", str9);
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
                    else
                    {
                        DataTable oldTable = null;
                        if ((str3.Equals("5") || str3.Equals("6")) || str3.Equals("7"))
                        {
                            oldTable = CallBLL.cz_odds_jssfc_bll.GetOddsInfoByID(str4.ToString());
                        }
                        if (str3.Equals("5") || str3.Equals("6"))
                        {
                            if (string.IsNullOrEmpty(str6))
                            {
                                str6 = "0.01";
                            }
                            new Dictionary<string, string>();
                            new Dictionary<string, string>();
                            int num19 = 0;
                            foreach (DataRow row in oldTable.Rows)
                            {
                                double num21 = Convert.ToDouble(row["current_odds"]);
                                double num22 = Convert.ToDouble(row["max_odds"]);
                                double num23 = Convert.ToDouble(row["min_odds"]);
                                double num24 = double.Parse(str6);
                                str12 = str3;
                                if (str12 != null)
                                {
                                    if (!(str12 == "5"))
                                    {
                                        if (str12 == "6")
                                        {
                                            goto Label_0AEB;
                                        }
                                    }
                                    else
                                    {
                                        num39 = num21 + num24;
                                        num21 = double.Parse(num39.ToString());
                                    }
                                }
                                goto Label_0B01;
                            Label_0AEB:
                                num39 = num21 + -num24;
                                num21 = double.Parse(num39.ToString());
                            Label_0B01:
                                if (num21 < num23)
                                {
                                    num24 = 0.0;
                                }
                                if (num21 > num22)
                                {
                                    num24 = 0.0;
                                }
                                if (CallBLL.cz_odds_jssfc_bll.UpdateCurrentOddsList(row["odds_id"].ToString(), str3.Equals("5") ? num24 : -num24, "5") > 0)
                                {
                                    num19++;
                                }
                            }
                            if (num19 > 0)
                            {
                                DataTable oddsInfoByID = CallBLL.cz_odds_jssfc_bll.GetOddsInfoByID(str4);
                                base.jssfc_kjj_log(oldTable, oddsInfoByID, str3);
                                Dictionary<string, object> dictionary6 = new Dictionary<string, object>();
                                foreach (DataRow row2 in oddsInfoByID.Rows)
                                {
                                    double num26 = Convert.ToDouble(row2["current_odds"]);
                                    double num27 = Convert.ToDouble(row2["max_odds"]);
                                    double num28 = Convert.ToDouble(row2["min_odds"]);
                                    num39 = num26 + Convert.ToDouble(row2[str5 + "_diff"]);
                                    num26 = double.Parse(num39.ToString());
                                    string str10 = row2["is_open"].ToString();
                                    Dictionary<string, object> dictionary7 = new Dictionary<string, object>();
                                    dictionary7.Add("pl", num26.ToString());
                                    num39 = num27 + Convert.ToDouble(row2[str5 + "_diff"]);
                                    dictionary7.Add("maxpl", num39.ToString());
                                    num39 = num28 + Convert.ToDouble(row2[str5 + "_diff"]);
                                    dictionary7.Add("minpl", num39.ToString());
                                    dictionary7.Add("plx", new List<double>());
                                    DataTable table5 = CallBLL.cz_phase_jssfc_bll.IsPhaseClose();
                                    if (table5.Rows[0]["isopen"].ToString().Equals("0") && string.IsNullOrEmpty(table5.Rows[0]["endtime"].ToString()))
                                    {
                                        str10 = "0";
                                    }
                                    if (table5.Rows[0]["openning"].ToString().Equals("n"))
                                    {
                                        str10 = "0";
                                    }
                                    dictionary7.Add("is_open", str10);
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
                            new Dictionary<string, string>();
                            new Dictionary<string, string>();
                            int num29 = 0;
                            foreach (DataRow row3 in oldTable.Rows)
                            {
                                double num30 = Convert.ToDouble(row3["current_odds"]);
                                double num31 = Convert.ToDouble(row3["max_odds"]);
                                double num32 = Convert.ToDouble(row3["min_odds"]);
                                double num33 = double.Parse(row3[str5 + "_diff"].ToString());
                                num39 = double.Parse(s) - num33;
                                double num34 = double.Parse(num39.ToString());
                                if (Convert.ToDouble(s) < num32)
                                {
                                    num34 = num32;
                                }
                                if (Convert.ToDouble(s) > num31)
                                {
                                    num34 = num31;
                                }
                                if (CallBLL.cz_odds_jssfc_bll.UpdateCurrentOddsList(row3["odds_id"].ToString(), num34, "7") > 0)
                                {
                                    num29++;
                                }
                            }
                            if (num29 > 0)
                            {
                                DataTable newTable = CallBLL.cz_odds_jssfc_bll.GetOddsInfoByID(str4);
                                base.jssfc_kjj_log(oldTable, newTable, str3);
                                Dictionary<string, object> dictionary8 = new Dictionary<string, object>();
                                foreach (DataRow row4 in newTable.Rows)
                                {
                                    double num36 = Convert.ToDouble(row4["current_odds"]);
                                    double num37 = Convert.ToDouble(row4["max_odds"]);
                                    double num38 = Convert.ToDouble(row4["min_odds"]);
                                    num39 = num36 + Convert.ToDouble(row4[str5 + "_diff"]);
                                    num36 = double.Parse(num39.ToString());
                                    string str11 = row4["is_open"].ToString();
                                    Dictionary<string, object> dictionary9 = new Dictionary<string, object>();
                                    dictionary9.Add("pl", num36.ToString());
                                    num39 = num37 + Convert.ToDouble(row4[str5 + "_diff"]);
                                    dictionary9.Add("maxpl", num39.ToString());
                                    num39 = num38 + Convert.ToDouble(row4[str5 + "_diff"]);
                                    dictionary9.Add("minpl", num39.ToString());
                                    dictionary9.Add("plx", new List<double>());
                                    DataTable table7 = CallBLL.cz_phase_jssfc_bll.IsPhaseClose();
                                    if (table7.Rows[0]["isopen"].ToString().Equals("0") && string.IsNullOrEmpty(table7.Rows[0]["endtime"].ToString()))
                                    {
                                        str11 = "0";
                                    }
                                    if (table7.Rows[0]["openning"].ToString().Equals("n"))
                                    {
                                        str11 = "0";
                                    }
                                    dictionary9.Add("is_open", str11);
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
                    str12 = str3;
                    if (str12 != null)
                    {
                        if (!(str12 == "1"))
                        {
                            if (str12 == "2")
                            {
                                num39 = num2 + -Convert.ToDouble(str6);
                                num2 = double.Parse(num39.ToString());
                            }
                        }
                        else
                        {
                            num39 = num2 + Convert.ToDouble(str6);
                            num2 = double.Parse(num39.ToString());
                        }
                    }
                    if (num2 < num4)
                    {
                        result.set_success(400);
                        num39 = num4 + num5;
                        result.set_tipinfo(string.Format(PageBase.GetMessageByCache("u100012", "MessageHint"), num39.ToString()));
                        strResult = JsonHandle.ObjectToJson(result);
                    }
                    else if (num2 > num3)
                    {
                        result.set_success(400);
                        num39 = num3 + num5;
                        result.set_tipinfo(string.Format(PageBase.GetMessageByCache("u100010", "MessageHint"), num39.ToString()));
                        strResult = JsonHandle.ObjectToJson(result);
                    }
                    else if (CallBLL.cz_odds_jssfc_bll.UpdateCurrentOdds(Convert.ToInt32(str4), str3.Equals("1") ? Convert.ToDouble(str6) : -Convert.ToDouble(str6), str3) > 0)
                    {
                        cz_odds_jssfc _jssfc2 = CallBLL.cz_odds_jssfc_bll.GetModel(Convert.ToInt32(str4));
                        base.jssfc_log(oldModel, _jssfc2, str3);
                        double num7 = Convert.ToDouble(_jssfc2.get_current_odds());
                        double num8 = Convert.ToDouble(_jssfc2.get_max_odds());
                        double num9 = Convert.ToDouble(_jssfc2.get_min_odds());
                        double num10 = 0.0;
                        if (str5.Equals("A"))
                        {
                            num10 = Convert.ToDouble(_jssfc2.get_a_diff());
                        }
                        else if (str5.Equals("B"))
                        {
                            num10 = Convert.ToDouble(_jssfc2.get_b_diff());
                        }
                        else if (str5.Equals("C"))
                        {
                            num10 = Convert.ToDouble(_jssfc2.get_c_diff());
                        }
                        num39 = num7 + num10;
                        num7 = double.Parse(num39.ToString());
                        string str8 = _jssfc2.get_is_open().ToString();
                        Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
                        Dictionary<string, object> dictionary3 = new Dictionary<string, object>();
                        dictionary3.Add("pl", num7.ToString());
                        num39 = num8 + num10;
                        dictionary3.Add("maxpl", num39.ToString());
                        dictionary3.Add("minpl", (num9 + num10).ToString());
                        dictionary3.Add("plx", new List<double>());
                        DataTable table = CallBLL.cz_phase_jssfc_bll.IsPhaseClose();
                        if (table.Rows[0]["isopen"].ToString().Equals("0") && string.IsNullOrEmpty(table.Rows[0]["endtime"].ToString()))
                        {
                            str8 = "0";
                        }
                        if (table.Rows[0]["openning"].ToString().Equals("n"))
                        {
                            str8 = "0";
                        }
                        dictionary3.Add("is_open", str8);
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
            ReturnResult result = new ReturnResult();
            string str3 = LSRequest.qq("optype");
            string str4 = LSRequest.qq("oddsid");
            LSRequest.qq("phaseid");
            LSRequest.qq("pk");
            LSRequest.qq("wtvalue");
            LSRequest.qq("inputvalue");
            string str5 = LSRequest.qq("isopen");
            string str6 = LSRequest.qq("playid");
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
                    DataTable table = CallBLL.cz_phase_jssfc_bll.IsPhaseClose();
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
                        int num = CallBLL.cz_odds_jssfc_bll.UpdateIsOpen(Convert.ToInt32(str4));
                        result.set_success(200);
                        if (num > 0)
                        {
                            base.kc_openclose_log(str4, str3, str5, 14, LSRequest.qq("playpage"));
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
                    DataTable table2 = CallBLL.cz_phase_jssfc_bll.IsPhaseClose();
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
                        int num2 = CallBLL.cz_odds_jssfc_bll.UpdateIsOpenList(str4, Convert.ToInt32(str5));
                        result.set_success(200);
                        if (num2 > 0)
                        {
                            base.kc_openclose_log(str4, str3, str5, 14, LSRequest.qq("playpage"));
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
                    int num3 = CallBLL.cz_odds_jssfc_bll.UpdateIsOpenList(str3, str6, str4, str5);
                    result.set_success(200);
                    if (num3 > 0)
                    {
                        base.kc_openclose_log(str4, str3, str5, 14, LSRequest.qq("playpage"));
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
                double num60;
                string str13;
                int num61;
                cz_odds_jssfc _jssfc = null;
                if ((str3.Equals("1") || str3.Equals("2")) || str3.Equals("3"))
                {
                    _jssfc = CallBLL.cz_odds_jssfc_bll.GetModel(Convert.ToInt32(str4));
                    if (_jssfc == null)
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
                        double num16 = double.Parse(_jssfc.get_current_odds());
                        double num17 = Convert.ToDouble(_jssfc.get_max_odds());
                        double num18 = Convert.ToDouble(_jssfc.get_min_odds());
                        double num19 = 0.0;
                        if (str8.Equals("A"))
                        {
                            num19 = Convert.ToDouble(_jssfc.get_a_diff());
                        }
                        else if (str8.Equals("B"))
                        {
                            num19 = Convert.ToDouble(_jssfc.get_b_diff());
                        }
                        else if (str8.Equals("C"))
                        {
                            num19 = Convert.ToDouble(_jssfc.get_c_diff());
                        }
                        base.GetFgsWTTable(14);
                        double wtvalue = 0.0;
                        base.GetOperate_KC(14, str4, ref wtvalue);
                        double num21 = num16;
                        double num22 = num18;
                        num60 = num16 + wtvalue;
                        double num23 = double.Parse(num60.ToString());
                        num60 = num16 + num19;
                        double num24 = double.Parse(num60.ToString());
                        num60 = (num21 - num22) + 1.0;
                        double num25 = double.Parse(num60.ToString());
                        num60 = double.Parse(s) - num19;
                        s = num60.ToString();
                        if (Convert.ToDouble(s) < num22)
                        {
                            result.set_success(400);
                            num60 = num22 + num19;
                            result.set_tipinfo(string.Format(PageBase.GetMessageByCache("u100023", "MessageHint"), _jssfc.get_play_name() + "【" + _jssfc.get_put_amount() + "】", double.Parse(num60.ToString())));
                            strResult = JsonHandle.ObjectToJson(result);
                        }
                        else if (Convert.ToDouble(s) > num21)
                        {
                            result.set_success(400);
                            num60 = num21 + num19;
                            result.set_tipinfo(string.Format(PageBase.GetMessageByCache("u100055", "MessageHint"), _jssfc.get_play_name() + "【" + _jssfc.get_put_amount() + "】", double.Parse(num60.ToString())));
                            strResult = JsonHandle.ObjectToJson(result);
                        }
                        else if (Convert.ToDouble(s) < num25)
                        {
                            result.set_success(400);
                            num60 = num25 + num19;
                            result.set_tipinfo(string.Format(PageBase.GetMessageByCache("u100023", "MessageHint"), _jssfc.get_play_name() + "【" + _jssfc.get_put_amount() + "】", double.Parse(num60.ToString())));
                            strResult = JsonHandle.ObjectToJson(result);
                        }
                        else
                        {
                            decimal num26 = decimal.Parse((decimal.Parse(s) - decimal.Parse(num21.ToString())).ToString());
                            DataTable oddsWTbyFGS = CallBLL.cz_odds_wt_jssfc_bll.GetOddsWTbyFGS(str4, str);
                            num61 = 14;
                            base.Add_FgsOddsWT_Lock(num61.ToString(), str, str4);
                            int num27 = CallBLL.cz_odds_wt_jssfc_bll.UpdateFgsWTOdds(Convert.ToInt32(str4), num26, str3, str);
                            num61 = 14;
                            base.Del_FgsOddsWT_Lock(num61.ToString(), str, str4);
                            if (num27 > 0)
                            {
                                wtvalue = 0.0;
                                CacheHelper.RemoveAllCache("fgs_wt_FileCacheKey" + context.Session["user_name"].ToString() + 14);
                                FileCacheHelper.UpdateFGSWTFile(14);
                                base.GetOperate_KC(14, str4, ref wtvalue);
                                cz_odds_jssfc _jssfc3 = CallBLL.cz_odds_jssfc_bll.GetModel(Convert.ToInt32(str4));
                                base.fgs_opt_kc_log(oddsWTbyFGS, CallBLL.cz_odds_wt_jssfc_bll.GetOddsWTbyFGS(str4, str), str3, str, str4, 14);
                                double num28 = Convert.ToDouble(_jssfc3.get_current_odds());
                                Convert.ToDouble(_jssfc3.get_max_odds());
                                double num29 = Convert.ToDouble(_jssfc3.get_min_odds());
                                double num30 = 0.0;
                                if (str8.Equals("A"))
                                {
                                    num30 = Convert.ToDouble(_jssfc3.get_a_diff());
                                }
                                else if (str8.Equals("B"))
                                {
                                    num30 = Convert.ToDouble(_jssfc3.get_b_diff());
                                }
                                else if (str8.Equals("C"))
                                {
                                    num30 = Convert.ToDouble(_jssfc3.get_c_diff());
                                }
                                num60 = (num28 + num30) + wtvalue;
                                num23 = double.Parse(num60.ToString());
                                num60 = num28 + num30;
                                num24 = double.Parse(num60.ToString());
                                num60 = ((num28 - num29) + num30) + 1.0;
                                num25 = double.Parse(num60.ToString());
                                string str10 = _jssfc3.get_is_open().ToString();
                                Dictionary<string, object> dictionary4 = new Dictionary<string, object>();
                                Dictionary<string, object> dictionary5 = new Dictionary<string, object>();
                                dictionary5.Add("pl", Convert.ToDecimal(num23.ToString()).ToString());
                                dictionary5.Add("maxpl", Convert.ToDecimal(num24.ToString()).ToString());
                                dictionary5.Add("minpl", Convert.ToDecimal(num25.ToString()).ToString());
                                dictionary5.Add("plx", new List<double>());
                                DataTable table4 = CallBLL.cz_phase_jssfc_bll.IsPhaseClose();
                                if (table4.Rows[0]["isopen"].ToString().Equals("0") && string.IsNullOrEmpty(table4.Rows[0]["endtime"].ToString()))
                                {
                                    str10 = "0";
                                }
                                if (table4.Rows[0]["openning"].ToString().Equals("n"))
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
                        DataTable oddsInfoByID = null;
                        if ((str3.Equals("5") || str3.Equals("6")) || str3.Equals("7"))
                        {
                            oddsInfoByID = CallBLL.cz_odds_jssfc_bll.GetOddsInfoByID(str4.ToString());
                        }
                        if (str3.Equals("5") || str3.Equals("6"))
                        {
                            if (string.IsNullOrEmpty(str6))
                            {
                                str6 = "0.01";
                            }
                            new Dictionary<string, string>();
                            new Dictionary<string, string>();
                            new Dictionary<string, double>();
                            int num31 = 0;
                            foreach (DataRow row in oddsInfoByID.Rows)
                            {
                                double num32 = double.Parse(row["current_odds"].ToString());
                                Convert.ToDouble(row["max_odds"].ToString());
                                double num33 = Convert.ToDouble(row["min_odds"].ToString());
                                double num34 = Convert.ToDouble(row[str8 + "_diff"].ToString());
                                base.GetFgsWTTable(14);
                                double num35 = 0.0;
                                base.GetOperate_KC(14, row["odds_id"].ToString(), ref num35);
                                num60 = num32 + num35;
                                double num36 = double.Parse(num60.ToString());
                                num60 = num32 + num34;
                                double.Parse(num60.ToString());
                                num60 = (num32 - num33) + 1.0;
                                double num37 = double.Parse(num60.ToString());
                                double num38 = 0.0;
                                double num39 = double.Parse(str6);
                                str13 = str3;
                                if (str13 != null)
                                {
                                    if (!(str13 == "5"))
                                    {
                                        if (str13 == "6")
                                        {
                                            goto Label_100E;
                                        }
                                    }
                                    else
                                    {
                                        num60 = num36 + Convert.ToDouble(num39);
                                        num38 = double.Parse(num60.ToString());
                                    }
                                }
                                goto Label_1029;
                            Label_100E:
                                num60 = num36 + -Convert.ToDouble(num39);
                                num38 = double.Parse(num60.ToString());
                            Label_1029:
                                if (num38 < num33)
                                {
                                    num39 = 0.0;
                                }
                                if (num38 > num32)
                                {
                                    num39 = 0.0;
                                }
                                if (num38 < num37)
                                {
                                    num39 = 0.0;
                                }
                                DataTable table6 = CallBLL.cz_odds_wt_jssfc_bll.GetOddsWTbyFGS(str4, str);
                                num61 = 14;
                                base.Add_FgsOddsWT_Lock(num61.ToString(), str, row["odds_id"].ToString());
                                int num40 = CallBLL.cz_odds_wt_jssfc_bll.UpdateFgsWTOddsList(row["odds_id"].ToString(), str3.Equals("5") ? Convert.ToDecimal(num39) : -Convert.ToDecimal(num39), "5", str);
                                num61 = 14;
                                base.Del_FgsOddsWT_Lock(num61.ToString(), str, row["odds_id"].ToString());
                                if (num40 > 0)
                                {
                                    base.fgs_opt_kc_log(table6, CallBLL.cz_odds_wt_jssfc_bll.GetOddsWTbyFGS(str4, str), str3, str, str4, 14);
                                    num31++;
                                }
                            }
                            if (num31 > 0)
                            {
                                CacheHelper.RemoveAllCache("fgs_wt_FileCacheKey" + context.Session["user_name"].ToString() + 14);
                                FileCacheHelper.UpdateFGSWTFile(14);
                                DataTable table7 = CallBLL.cz_odds_jssfc_bll.GetOddsInfoByID(str4);
                                Dictionary<string, object> dictionary6 = new Dictionary<string, object>();
                                foreach (DataRow row2 in table7.Rows)
                                {
                                    double num41 = 0.0;
                                    base.GetOperate_KC(14, row2["odds_id"].ToString(), ref num41);
                                    cz_odds_jssfc _jssfc4 = CallBLL.cz_odds_jssfc_bll.GetModel(Convert.ToInt32(row2["odds_id"].ToString()));
                                    double num42 = Convert.ToDouble(_jssfc4.get_current_odds());
                                    double num43 = Convert.ToDouble(_jssfc4.get_max_odds());
                                    double num44 = Convert.ToDouble(_jssfc4.get_min_odds());
                                    double num45 = 0.0;
                                    if (str8.Equals("A"))
                                    {
                                        num45 = Convert.ToDouble(_jssfc4.get_a_diff());
                                    }
                                    else if (str8.Equals("B"))
                                    {
                                        num45 = Convert.ToDouble(_jssfc4.get_b_diff());
                                    }
                                    else if (str8.Equals("C"))
                                    {
                                        num45 = Convert.ToDouble(_jssfc4.get_c_diff());
                                    }
                                    num60 = num42 + num45;
                                    num43 = double.Parse(num60.ToString());
                                    num60 = (num42 + num45) + num41;
                                    num42 = double.Parse(num60.ToString());
                                    num60 = ((num42 - num44) + num45) + 1.0;
                                    num44 = double.Parse(num60.ToString());
                                    string str11 = row2["is_open"].ToString();
                                    Dictionary<string, object> dictionary7 = new Dictionary<string, object>();
                                    dictionary7.Add("pl", Convert.ToDecimal(num42.ToString()).ToString());
                                    dictionary7.Add("maxpl", Convert.ToDecimal(num43.ToString()).ToString());
                                    dictionary7.Add("minpl", Convert.ToDecimal(num44.ToString()).ToString());
                                    dictionary7.Add("plx", new List<double>());
                                    DataTable table8 = CallBLL.cz_phase_jssfc_bll.IsPhaseClose();
                                    if (table8.Rows[0]["isopen"].ToString().Equals("0") && string.IsNullOrEmpty(table8.Rows[0]["endtime"].ToString()))
                                    {
                                        str11 = "0";
                                    }
                                    if (table8.Rows[0]["openning"].ToString().Equals("n"))
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
                            new Dictionary<string, string>();
                            new Dictionary<string, string>();
                            Dictionary<string, double> dictionary8 = new Dictionary<string, double>();
                            foreach (DataRow row3 in oddsInfoByID.Rows)
                            {
                                double num46 = double.Parse(row3["current_odds"].ToString());
                                Convert.ToDouble(row3["max_odds"].ToString());
                                double num47 = Convert.ToDouble(row3["min_odds"].ToString());
                                double num48 = Convert.ToDouble(row3[str8 + "_diff"].ToString());
                                base.GetFgsWTTable(14);
                                double num49 = 0.0;
                                base.GetOperate_KC(14, row3["odds_id"].ToString(), ref num49);
                                num60 = num46 + num49;
                                double.Parse(num60.ToString());
                                num60 = num46 + num48;
                                double.Parse(num60.ToString());
                                num60 = (num46 - num47) + 1.0;
                                double num50 = double.Parse(num60.ToString());
                                num60 = double.Parse(s) - num48;
                                double num51 = double.Parse(num60.ToString());
                                if (num51 > num46)
                                {
                                    dictionary8.Add(row3["odds_id"].ToString(), 0.0);
                                }
                                else if (num51 < num50)
                                {
                                    num60 = num46 - double.Parse(num50.ToString());
                                    double num52 = -double.Parse(num60.ToString());
                                    dictionary8.Add(row3["odds_id"].ToString(), num52);
                                }
                                else
                                {
                                    num60 = num51 - double.Parse(num46.ToString());
                                    double num53 = double.Parse(num60.ToString());
                                    dictionary8.Add(row3["odds_id"].ToString(), num53);
                                }
                            }
                            DataTable table9 = CallBLL.cz_odds_wt_jssfc_bll.GetOddsWTbyFGS(str4, str);
                            num61 = 14;
                            base.Add_FgsOddsWT_Lock(num61.ToString(), str, str4);
                            int num54 = CallBLL.cz_odds_wt_jssfc_bll.UpdateFgsWTOddsInputList(str4, dictionary8, str);
                            num61 = 14;
                            base.Del_FgsOddsWT_Lock(num61.ToString(), str, str4);
                            if (num54 > 0)
                            {
                                CacheHelper.RemoveAllCache("fgs_wt_FileCacheKey" + context.Session["user_name"].ToString() + 14);
                                FileCacheHelper.UpdateFGSWTFile(14);
                                DataTable table10 = CallBLL.cz_odds_jssfc_bll.GetOddsInfoByID(str4);
                                base.fgs_opt_kc_log(table9, CallBLL.cz_odds_wt_jssfc_bll.GetOddsWTbyFGS(str4, str), str3, str, str4, 14);
                                Dictionary<string, object> dictionary9 = new Dictionary<string, object>();
                                foreach (DataRow row4 in table10.Rows)
                                {
                                    double num55 = 0.0;
                                    base.GetOperate_KC(14, row4["odds_id"].ToString(), ref num55);
                                    double num56 = Convert.ToDouble(row4["current_odds"]);
                                    double num57 = Convert.ToDouble(row4["max_odds"]);
                                    double num58 = Convert.ToDouble(row4["min_odds"]);
                                    double num59 = Convert.ToDouble(row4[str8 + "_diff"]);
                                    num60 = (num56 + num59) + num55;
                                    num56 = double.Parse(num60.ToString());
                                    num60 = num56 + num59;
                                    num57 = double.Parse(num60.ToString());
                                    num60 = ((num56 - num58) + num59) + 1.0;
                                    num58 = double.Parse(num60.ToString());
                                    string str12 = row4["is_open"].ToString();
                                    Dictionary<string, object> dictionary10 = new Dictionary<string, object>();
                                    dictionary10.Add("pl", Convert.ToDecimal(num56.ToString()).ToString());
                                    dictionary10.Add("maxpl", Convert.ToDecimal(num57.ToString()).ToString());
                                    dictionary10.Add("minpl", Convert.ToDecimal(num58.ToString()).ToString());
                                    dictionary10.Add("plx", new List<double>());
                                    DataTable table11 = CallBLL.cz_phase_jssfc_bll.IsPhaseClose();
                                    if (table11.Rows[0]["isopen"].ToString().Equals("0") && string.IsNullOrEmpty(table11.Rows[0]["endtime"].ToString()))
                                    {
                                        str12 = "0";
                                    }
                                    if (table11.Rows[0]["openning"].ToString().Equals("n"))
                                    {
                                        str12 = "0";
                                    }
                                    dictionary10.Add("is_open", str12);
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
                    double num = double.Parse(_jssfc.get_current_odds());
                    double num2 = Convert.ToDouble(_jssfc.get_max_odds());
                    double num3 = Convert.ToDouble(_jssfc.get_min_odds());
                    double num4 = 0.0;
                    if (str8.Equals("A"))
                    {
                        num4 = Convert.ToDouble(_jssfc.get_a_diff());
                    }
                    else if (str8.Equals("B"))
                    {
                        num4 = Convert.ToDouble(_jssfc.get_b_diff());
                    }
                    else if (str8.Equals("C"))
                    {
                        num4 = Convert.ToDouble(_jssfc.get_c_diff());
                    }
                    base.GetFgsWTTable(14);
                    double num5 = 0.0;
                    base.GetOperate_KC(14, str4, ref num5);
                    double num6 = num;
                    double num7 = num3;
                    num60 = num + num5;
                    double num8 = double.Parse(num60.ToString());
                    num60 = num + num4;
                    double num9 = double.Parse(num60.ToString());
                    num60 = (num6 - num7) + 1.0;
                    double num10 = double.Parse(num60.ToString());
                    double num11 = 0.0;
                    str13 = str3;
                    if (str13 != null)
                    {
                        if (!(str13 == "1"))
                        {
                            if (str13 == "2")
                            {
                                num60 = num8 + -Convert.ToDouble(str6);
                                num11 = double.Parse(num60.ToString());
                            }
                        }
                        else
                        {
                            num60 = num8 + Convert.ToDouble(str6);
                            num11 = double.Parse(num60.ToString());
                        }
                    }
                    if (!str3.Equals("1") && (num11 < num7))
                    {
                        result.set_success(400);
                        num60 = num7 + num4;
                        result.set_tipinfo(string.Format(PageBase.GetMessageByCache("u100023", "MessageHint"), _jssfc.get_play_name() + "【" + _jssfc.get_put_amount() + "】", double.Parse(num60.ToString())));
                        strResult = JsonHandle.ObjectToJson(result);
                    }
                    else if (!str3.Equals("2") && (num11 > num6))
                    {
                        result.set_success(400);
                        num60 = num6 + num4;
                        result.set_tipinfo(string.Format(PageBase.GetMessageByCache("u100055", "MessageHint"), _jssfc.get_play_name() + "【" + _jssfc.get_put_amount() + "】", double.Parse(num60.ToString())));
                        strResult = JsonHandle.ObjectToJson(result);
                    }
                    else if (num11 < num10)
                    {
                        result.set_success(400);
                        num60 = num10 + num4;
                        result.set_tipinfo(string.Format(PageBase.GetMessageByCache("u100023", "MessageHint"), _jssfc.get_play_name() + "【" + _jssfc.get_put_amount() + "】", double.Parse(num60.ToString())));
                        strResult = JsonHandle.ObjectToJson(result);
                    }
                    else
                    {
                        DataTable table = CallBLL.cz_odds_wt_jssfc_bll.GetOddsWTbyFGS(str4, str);
                        num61 = 14;
                        base.Add_FgsOddsWT_Lock(num61.ToString(), str, str4);
                        int num12 = CallBLL.cz_odds_wt_jssfc_bll.UpdateFgsWTOdds(Convert.ToInt32(str4), str3.Equals("1") ? decimal.Parse(str6) : -decimal.Parse(str6), str3, str);
                        base.Del_FgsOddsWT_Lock(14.ToString(), str, str4);
                        if (num12 > 0)
                        {
                            num5 = 0.0;
                            CacheHelper.RemoveAllCache("fgs_wt_FileCacheKey" + context.Session["user_name"].ToString() + 14);
                            FileCacheHelper.UpdateFGSWTFile(14);
                            base.GetOperate_KC(14, str4, ref num5);
                            cz_odds_jssfc _jssfc2 = CallBLL.cz_odds_jssfc_bll.GetModel(Convert.ToInt32(str4));
                            base.fgs_opt_kc_log(table, CallBLL.cz_odds_wt_jssfc_bll.GetOddsWTbyFGS(str4, str), str3, str, str4, 14);
                            double num13 = Convert.ToDouble(_jssfc2.get_current_odds());
                            Convert.ToDouble(_jssfc2.get_max_odds());
                            double num14 = Convert.ToDouble(_jssfc2.get_min_odds());
                            double num15 = 0.0;
                            if (str8.Equals("A"))
                            {
                                num15 = Convert.ToDouble(_jssfc2.get_a_diff());
                            }
                            else if (str8.Equals("B"))
                            {
                                num15 = Convert.ToDouble(_jssfc2.get_b_diff());
                            }
                            else if (str8.Equals("C"))
                            {
                                num15 = Convert.ToDouble(_jssfc2.get_c_diff());
                            }
                            num60 = (num13 + num15) + num5;
                            num8 = double.Parse(num60.ToString());
                            num60 = num13 + num15;
                            num9 = double.Parse(num60.ToString());
                            num60 = ((num13 - num14) + num15) + 1.0;
                            num10 = num10 = double.Parse(num60.ToString());
                            string str9 = _jssfc2.get_is_open().ToString();
                            Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
                            Dictionary<string, object> dictionary3 = new Dictionary<string, object>();
                            dictionary3.Add("pl", Convert.ToDecimal(num8.ToString()).ToString());
                            dictionary3.Add("maxpl", Convert.ToDecimal(num9.ToString()).ToString());
                            dictionary3.Add("minpl", Convert.ToDecimal(num10.ToString()).ToString());
                            dictionary3.Add("plx", new List<double>());
                            DataTable table2 = CallBLL.cz_phase_jssfc_bll.IsPhaseClose();
                            if (table2.Rows[0]["isopen"].ToString().Equals("0") && string.IsNullOrEmpty(table2.Rows[0]["endtime"].ToString()))
                            {
                                str9 = "0";
                            }
                            if (table2.Rows[0]["openning"].ToString().Equals("n"))
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

        public override void ProcessRequest(HttpContext context)
        {
            base.checkLoginByHandler(0);
            context.Session["user_name"].ToString();
            string str = context.Session["user_type"].ToString();
            string str2 = LSRequest.qq("action");
            string strResult = "";
            if (!string.IsNullOrEmpty(str2))
            {
                switch (str2)
                {
                    case "get_oddsinfo":
                        this.get_oddsinfo(context, ref strResult);
                        break;

                    case "get_clyl":
                        this.get_clyl(context, ref strResult);
                        break;

                    case "get_lmbhlist":
                        this.get_lmbhlist(context, ref strResult);
                        break;

                    case "operate_adds":
                    {
                        if (!str.ToLower().Equals("fgs"))
                        {
                            this.operate_adds(context, ref strResult);
                            break;
                        }
                        if (base.IsFGSWT_Opt(14))
                        {
                            this.operate_odds_wt(context, ref strResult);
                            break;
                        }
                        ReturnResult result = new ReturnResult();
                        result.set_success(400);
                        result.set_tipinfo(PageBase.GetMessageByCache("u100095", "MessageHint"));
                        strResult = JsonHandle.ObjectToJson(result);
                        break;
                    }
                    case "operate_closeopen":
                        this.operate_CloseOpen(context, ref strResult);
                        break;

                    case "get_opennumber":
                        this.get_opennumber(context, ref strResult);
                        break;

                    case "set_allowsale":
                        this.user_allow_sale(context, ref strResult);
                        break;
                }
                context.Response.ContentType = "text/json";
                context.Response.Write(strResult);
            }
        }

        public void user_allow_sale(HttpContext context, ref string strResult)
        {
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
            string nos = LSRequest.qq("number");
            LSRequest.qq("fast");
            string str8 = LSRequest.qq("pk");
            string str9 = LSRequest.qq("jeucode");
            if ((context.Session["JeuValidate"] == null) || !context.Session["JeuValidate"].ToString().Equals(str9))
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
                DataTable phaseByPID = CallBLL.cz_phase_jssfc_bll.GetPhaseByPID(s);
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
                    string[] strArray8 = strArray;
                    int num28 = 0;
                    while (num28 < strArray8.Length)
                    {
                        string str29;
                        cz_bet_kc _kc2;
                        string str38;
                        string str11 = strArray8[num28];
                        string str12 = strArray2[index];
                        bool flag = false;
                        string[] strArray3 = "329,330,331,1181,1200,1201,1202,1203".Split(new char[] { ',' });
                        for (int i = 0; i < strArray3.Length; i++)
                        {
                            if (strArray3[i].Equals(str4))
                            {
                                flag = true;
                                break;
                            }
                        }
                        DataTable table2 = null;
                        if (flag)
                        {
                            string[] strArray4 = nos.Split(new char[] { ',' });
                            string str13 = "";
                            string str14 = "";
                            foreach (string str15 in strArray4)
                            {
                                if (int.Parse(str15) < 10)
                                {
                                    str14 = "0" + int.Parse(str15).ToString();
                                }
                                else
                                {
                                    str14 = str15.ToString();
                                }
                                str13 = str13 + str14.Trim() + ",";
                            }
                            nos = str13.Substring(0, str13.Length - 1);
                            int num30 = 14;
                            table2 = CallBLL.cz_bet_kc_bll.GetSZZT(model.get_u_name(), model.get_u_type().Trim(), str4, s, true, nos, num30.ToString(), "jssfc");
                        }
                        else
                        {
                            table2 = CallBLL.cz_bet_kc_bll.GetSZZT(model.get_u_name(), model.get_u_type().Trim(), str4, s, false, "", 14.ToString(), "jssfc");
                        }
                        if (double.Parse(table2.Rows[0][0].ToString()) < double.Parse(str6))
                        {
                            result.set_success(400);
                            result.set_tipinfo(PageBase.GetMessageByCache("u100029", "MessageHint"));
                            strResult = JsonHandle.ObjectToJson(result);
                            break;
                        }
                        if (double.Parse(str6) < 10.0)
                        {
                            result.set_success(400);
                            result.set_tipinfo(string.Format(PageBase.GetMessageByCache("u100096", "MessageHint"), 10));
                            strResult = JsonHandle.ObjectToJson(result);
                            break;
                        }
                        string str16 = "A";
                        if (!Utils.Agent_AllowSaleKind(str8, model.get_kc_kind(), ref str16))
                        {
                            result.set_success(400);
                            result.set_tipinfo(PageBase.GetMessageByCache("u100056", "MessageHint"));
                            strResult = JsonHandle.ObjectToJson(result);
                            break;
                        }
                        agent_kc_rate _rate = base.GetUserRate_kc(model.get_zjname());
                        DataTable table3 = base.GetUserDrawback_jssfc();
                        DataSet playOddsInfo = CallBLL.cz_play_jssfc_bll.GetPlayOddsInfo(str11);
                        string str17 = playOddsInfo.Tables[0].Rows[0]["play_id"].ToString();
                        string str18 = playOddsInfo.Tables[0].Rows[0]["play_name"].ToString();
                        string str19 = playOddsInfo.Tables[0].Rows[0]["current_odds"].ToString().Trim();
                        string str20 = playOddsInfo.Tables[0].Rows[0]["put_amount"].ToString().Trim();
                        playOddsInfo.Tables[0].Rows[0]["min_odds"].ToString().Trim();
                        string str21 = playOddsInfo.Tables[0].Rows[0][str16.ToUpper() + "_diff"].ToString().Trim();
                        if (string.IsNullOrEmpty(str21))
                        {
                            str21 = "0";
                        }
                        double num31 = double.Parse(str19) + double.Parse(str21);
                        string str22 = num31.ToString();
                        base.GetOdds_KC_EX(14, playOddsInfo.Tables[0].Rows[0]["current_odds"].ToString().Trim(), str21, ref str19, str11);
                        if (model.get_u_type().Equals("fgs") && model.get_kc_op_odds().Equals(1))
                        {
                            str19 = str22;
                            str5 = str19;
                        }
                        if (!str19.Equals(str5))
                        {
                            result.set_success(600);
                            new List<object>();
                            Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
                            dictionary2.Add("oldodds", str5);
                            dictionary2.Add("newodds", str19);
                            dictionary.Add("jeucode", base.get_JeuValidate());
                            dictionary.Add("oddschange", dictionary2);
                            result.set_data(dictionary);
                            result.set_tipinfo(string.Format(PageBase.GetMessageByCache("u100028", "MessageHint"), str5, str19));
                            strResult = JsonHandle.ObjectToJson(result);
                            break;
                        }
                        DataRow[] rowArray = table3.Select(string.Format(" play_id={0} ", str17));
                        string str23 = "0";
                        string str24 = "0";
                        string str25 = "0";
                        string str26 = "0";
                        string str27 = "0";
                        string str28 = "0";
                        double num3 = 0.0;
                        double num4 = 0.0;
                        if (_rate.get_zcyg().Equals("1"))
                        {
                            num3 = ((100.0 - double.Parse(_rate.get_fgszc())) - double.Parse(_rate.get_gdzc())) - double.Parse(_rate.get_zdzc());
                            num4 = double.Parse(_rate.get_fgszc());
                        }
                        else if (model.get_u_type() == "fgs")
                        {
                            num3 = 100.0;
                        }
                        else
                        {
                            num4 = ((100.0 - double.Parse(_rate.get_zjzc())) - double.Parse(_rate.get_gdzc())) - double.Parse(_rate.get_zdzc());
                            num3 = double.Parse(_rate.get_zjzc());
                        }
                        foreach (DataRow row in rowArray)
                        {
                            str38 = row["u_type"].ToString().Trim();
                            if (str38 != null)
                            {
                                if (!(str38 == "zj"))
                                {
                                    if (str38 == "fgs")
                                    {
                                        goto Label_0A70;
                                    }
                                    if (str38 == "gd")
                                    {
                                        goto Label_0A94;
                                    }
                                    if (str38 == "zd")
                                    {
                                        goto Label_0AB5;
                                    }
                                    if (str38 == "dl")
                                    {
                                        goto Label_0AD6;
                                    }
                                    if (str38 == "hy")
                                    {
                                        goto Label_0AF7;
                                    }
                                }
                                else
                                {
                                    str23 = row[str16 + "_drawback"].ToString().Trim();
                                }
                            }
                            continue;
                        Label_0A70:
                            str24 = row[str16 + "_drawback"].ToString().Trim();
                            continue;
                        Label_0A94:
                            str25 = row[str16 + "_drawback"].ToString().Trim();
                            continue;
                        Label_0AB5:
                            str26 = row[str16 + "_drawback"].ToString().Trim();
                            continue;
                        Label_0AD6:
                            str27 = row[str16 + "_drawback"].ToString().Trim();
                            continue;
                        Label_0AF7:
                            str28 = row[str16 + "_drawback"].ToString().Trim();
                        }
                        str38 = model.get_u_type().ToString().Trim();
                        if (str38 != null)
                        {
                            if (!(str38 == "fgs"))
                            {
                                if (str38 == "gd")
                                {
                                    goto Label_0B95;
                                }
                                if (str38 == "zd")
                                {
                                    goto Label_0BB1;
                                }
                                if (str38 == "dl")
                                {
                                    goto Label_0BCD;
                                }
                            }
                            else
                            {
                                context.Session["user_name"].ToString();
                                str25 = str24;
                            }
                        }
                        goto Label_0BE7;
                    Label_0B95:
                        context.Session["user_name"].ToString();
                        str26 = str25;
                        goto Label_0BE7;
                    Label_0BB1:
                        context.Session["user_name"].ToString();
                        str27 = str26;
                        goto Label_0BE7;
                    Label_0BCD:
                        context.Session["user_name"].ToString();
                        str28 = str27;
                    Label_0BE7:
                        str29 = Utils.GetOrderNumber();
                        if (!flag)
                        {
                            goto Label_175D;
                        }
                        string[] source = Utils.longnumbersmallten(nos.Replace("-", "")).Split(new char[] { ',' });
                        if (source.Distinct<string>().ToList<string>().Count != source.Length)
                        {
                            context.Response.End();
                        }
                        string str31 = this.get_ball_pl(str17, double.Parse(str19), nos);
                        cz_bet_kc _kc = new cz_bet_kc();
                        _kc.set_order_num(str29);
                        _kc.set_checkcode(str9);
                        _kc.set_u_name(model.get_u_name());
                        _kc.set_u_nicker(model.get_u_nicker());
                        _kc.set_phase_id(new int?(int.Parse(s)));
                        _kc.set_phase(phase);
                        _kc.set_bet_time(new DateTime?(DateTime.Now));
                        _kc.set_odds_id(new int?(int.Parse(str11)));
                        _kc.set_category(playOddsInfo.Tables[0].Rows[0]["category"].ToString());
                        _kc.set_play_id(new int?(int.Parse(playOddsInfo.Tables[0].Rows[0]["play_id"].ToString())));
                        _kc.set_play_name(str18);
                        _kc.set_bet_val(nos);
                        _kc.set_bet_wt(str31);
                        _kc.set_odds(str19);
                        _kc.set_amount(new decimal?(decimal.Parse(str6)));
                        _kc.set_profit(0);
                        _kc.set_unit_cnt(1);
                        _kc.set_lm_type(0);
                        str38 = model.get_u_type().Trim();
                        if (str38 != null)
                        {
                            if (!(str38 == "dl"))
                            {
                                if (str38 == "zd")
                                {
                                    goto Label_0EFD;
                                }
                                if (str38 == "gd")
                                {
                                    goto Label_0FFB;
                                }
                                if (str38 == "fgs")
                                {
                                    goto Label_10EE;
                                }
                            }
                            else
                            {
                                _kc.set_hy_drawback(new decimal?(decimal.Parse(str28)));
                                _kc.set_dl_drawback(new decimal?(decimal.Parse(str27)));
                                _kc.set_zd_drawback(new decimal?(decimal.Parse(str26)));
                                _kc.set_gd_drawback(new decimal?(decimal.Parse(str25)));
                                _kc.set_fgs_drawback(new decimal?(decimal.Parse(str24)));
                                _kc.set_zj_drawback(new decimal?(decimal.Parse(str23)));
                                _kc.set_dl_rate(0f);
                                _kc.set_zd_rate((float) int.Parse(_rate.get_zdzc()));
                                _kc.set_gd_rate((float) int.Parse(_rate.get_gdzc()));
                                _kc.set_fgs_rate(float.Parse(num4.ToString()));
                                _kc.set_zj_rate(float.Parse(num3.ToString()));
                                _kc.set_dl_name(_rate.get_dlname());
                                _kc.set_zd_name(_rate.get_zdname());
                                _kc.set_gd_name(_rate.get_gdname());
                                _kc.set_fgs_name(_rate.get_fgsname());
                            }
                        }
                        goto Label_11D2;
                    Label_0EFD:
                        _kc.set_hy_drawback(0);
                        _kc.set_dl_drawback(new decimal?(decimal.Parse(str27)));
                        _kc.set_zd_drawback(new decimal?(decimal.Parse(str26)));
                        _kc.set_gd_drawback(new decimal?(decimal.Parse(str25)));
                        _kc.set_fgs_drawback(new decimal?(decimal.Parse(str24)));
                        _kc.set_zj_drawback(new decimal?(decimal.Parse(str23)));
                        _kc.set_dl_rate(0f);
                        _kc.set_zd_rate(0f);
                        _kc.set_gd_rate((float) int.Parse(_rate.get_gdzc()));
                        _kc.set_fgs_rate(float.Parse(num4.ToString()));
                        _kc.set_zj_rate(float.Parse(num3.ToString()));
                        _kc.set_dl_name("");
                        _kc.set_zd_name(_rate.get_zdname());
                        _kc.set_gd_name(_rate.get_gdname());
                        _kc.set_fgs_name(_rate.get_fgsname());
                        goto Label_11D2;
                    Label_0FFB:
                        _kc.set_hy_drawback(0);
                        _kc.set_dl_drawback(0);
                        _kc.set_zd_drawback(new decimal?(decimal.Parse(str26)));
                        _kc.set_gd_drawback(new decimal?(decimal.Parse(str25)));
                        _kc.set_fgs_drawback(new decimal?(decimal.Parse(str24)));
                        _kc.set_zj_drawback(new decimal?(decimal.Parse(str23)));
                        _kc.set_dl_rate(0f);
                        _kc.set_zd_rate(0f);
                        _kc.set_gd_rate(0f);
                        _kc.set_fgs_rate(float.Parse(num4.ToString()));
                        _kc.set_zj_rate(float.Parse(num3.ToString()));
                        _kc.set_dl_name("");
                        _kc.set_zd_name("");
                        _kc.set_gd_name(_rate.get_gdname());
                        _kc.set_fgs_name(_rate.get_fgsname());
                        goto Label_11D2;
                    Label_10EE:
                        _kc.set_hy_drawback(0);
                        _kc.set_dl_drawback(0);
                        _kc.set_zd_drawback(0);
                        _kc.set_gd_drawback(new decimal?(decimal.Parse(str25)));
                        _kc.set_fgs_drawback(new decimal?(decimal.Parse(str24)));
                        _kc.set_zj_drawback(new decimal?(decimal.Parse(str23)));
                        _kc.set_dl_rate(0f);
                        _kc.set_zd_rate(0f);
                        _kc.set_gd_rate(0f);
                        _kc.set_fgs_rate(0f);
                        _kc.set_zj_rate(float.Parse(num3.ToString()));
                        _kc.set_dl_name("");
                        _kc.set_zd_name("");
                        _kc.set_gd_name("");
                        _kc.set_fgs_name(_rate.get_fgsname());
                    Label_11D2:
                        _kc.set_is_payment(0);
                        _kc.set_sale_type(1);
                        _kc.set_m_type(-2);
                        _kc.set_kind(str16);
                        _kc.set_ip(LSRequest.GetIP());
                        _kc.set_lottery_type(14);
                        _kc.set_lottery_name(base.GetGameNameByID(_kc.get_lottery_type().ToString()));
                        _kc.set_isLM(1);
                        _kc.set_bet_group(nos);
                        _kc.set_ordervalidcode(Utils.GetOrderValidCode(_kc.get_u_name(), _kc.get_order_num(), _kc.get_bet_val(), _kc.get_odds(), _kc.get_kind(), Convert.ToInt32(_kc.get_phase_id()), Convert.ToInt32(_kc.get_odds_id()), Convert.ToDouble(_kc.get_amount())));
                        _kc.set_odds_zj(str22);
                        int num8 = 0;
                        CallBLL.cz_bet_kc_bll.AddBetByBH(_kc, ref num8);
                        double num9 = (double.Parse(str12) * num3) / 100.0;
                        CallBLL.cz_odds_jssfc_bll.Agent_UpdateGrandTotal(num9.ToString(), str11);
                        double num10 = double.Parse(playOddsInfo.Tables[0].Rows[0]["grand_total"].ToString()) + num9;
                        double num11 = double.Parse(playOddsInfo.Tables[0].Rows[0]["downbase"].ToString());
                        double num12 = Math.Floor((double) (num10 / num11));
                        if ((num12 >= 1.0) && (num11 >= 1.0))
                        {
                            double num13 = double.Parse(playOddsInfo.Tables[0].Rows[0]["down_odds_rate"].ToString()) * num12;
                            CallBLL.cz_odds_jssfc_bll.UpdateGrandTotalCurrentOdds(num13.ToString(), (num10 - (num11 * num12)).ToString(), str11.ToString());
                            cz_system_log _log = new cz_system_log();
                            _log.set_user_name("系統");
                            _log.set_children_name("");
                            _log.set_category(playOddsInfo.Tables[0].Rows[0]["category"].ToString());
                            _log.set_play_name(str18);
                            _log.set_put_amount(str20);
                            _log.set_l_name(LSKeys.get_JSSFC_Name());
                            _log.set_l_phase(phase);
                            _log.set_action("降賠率");
                            _log.set_odds_id(int.Parse(str4));
                            string str32 = playOddsInfo.Tables[0].Rows[0]["current_odds"].ToString();
                            _log.set_old_val(str32);
                            num31 = double.Parse(str32) - num13;
                            _log.set_new_val(num31.ToString());
                            _log.set_ip(LSRequest.GetIP());
                            _log.set_add_time(DateTime.Now);
                            _log.set_note("系統自動降賠");
                            _log.set_type_id(Convert.ToInt32((LSEnums.LogTypeID) 0));
                            _log.set_lottery_type(14);
                            CallBLL.cz_system_log_bll.Add(_log);
                            cz_jp_odds _odds = new cz_jp_odds();
                            _odds.set_add_time(DateTime.Now);
                            _odds.set_odds_id(int.Parse(str11));
                            _odds.set_phase_id(int.Parse(s));
                            _odds.set_play_name(str18);
                            _odds.set_put_amount(str20);
                            _odds.set_odds(num13.ToString());
                            _odds.set_lottery_type(14);
                            _odds.set_phase(phase);
                            _odds.set_old_odds(str32);
                            num31 = double.Parse(str32) - num13;
                            _odds.set_new_odds(num31.ToString());
                            CallBLL.cz_jp_odds_bll.Add(_odds);
                        }
                        double num15 = 0.0;
                        double num16 = 0.0;
                        string[] strArray6 = str19.Split(new char[] { ',' });
                        num15 = double.Parse(strArray6[0].ToString());
                        if (strArray6.Length > 1)
                        {
                            num16 = double.Parse(strArray6[1].ToString());
                        }
                        double num17 = 0.0;
                        double num18 = 0.0;
                        string[] strArray7 = str22.Split(new char[] { ',' });
                        num17 = double.Parse(strArray7[0].ToString());
                        if (strArray7.Length > 1)
                        {
                            num18 = double.Parse(strArray7[1].ToString());
                        }
                        cz_splitgroup_jssfc _jssfc = new cz_splitgroup_jssfc();
                        _jssfc.set_odds_id(new int?(int.Parse(str11)));
                        _jssfc.set_item_money(new decimal?(Convert.ToDecimal(str12)));
                        _jssfc.set_odds1(new decimal?(Convert.ToDecimal(num15.ToString())));
                        _jssfc.set_odds2(new decimal?(Convert.ToDecimal(num16.ToString())));
                        _jssfc.set_bet_id(new int?(num8));
                        _jssfc.set_item(nos);
                        _jssfc.set_odds1_zj(new decimal?(Convert.ToDecimal(num17.ToString())));
                        _jssfc.set_odds2_zj(new decimal?(Convert.ToDecimal(num18.ToString())));
                        CallBLL.cz_splitgroup_jssfc_bll.Agent_AddSplitGroup(_jssfc);
                        this.ZDBH_LM(model, _rate, str16, context, s, phase, str17, str11, str23, str24, str25, str26, str27, str28, str9);
                        this.GDBH_LM(model, _rate, str16, context, s, phase, str17, str11, str23, str24, str25, str26, str27, str28, str9);
                        this.FGSBH_LM(model, _rate, str16, context, s, phase, str17, str11, str23, str24, str25, str26, str27, str28, str9);
                        goto Label_20D9;
                    Label_175D:
                        _kc2 = new cz_bet_kc();
                        _kc2.set_order_num(str29);
                        _kc2.set_checkcode(str9);
                        _kc2.set_u_name(model.get_u_name());
                        _kc2.set_u_nicker(model.get_u_nicker());
                        _kc2.set_phase_id(new int?(int.Parse(s)));
                        _kc2.set_phase(phase);
                        _kc2.set_bet_time(new DateTime?(DateTime.Now));
                        _kc2.set_odds_id(new int?(int.Parse(str11)));
                        _kc2.set_category(playOddsInfo.Tables[0].Rows[0]["category"].ToString());
                        _kc2.set_play_name(str18);
                        _kc2.set_play_id(new int?(int.Parse(playOddsInfo.Tables[0].Rows[0]["play_id"].ToString())));
                        _kc2.set_bet_val(str20);
                        _kc2.set_bet_wt("");
                        _kc2.set_odds(str19);
                        _kc2.set_amount(new decimal?(decimal.Parse(str6)));
                        _kc2.set_profit(0);
                        str38 = model.get_u_type().Trim();
                        if (str38 != null)
                        {
                            if (!(str38 == "dl"))
                            {
                                if (str38 == "zd")
                                {
                                    goto Label_19E4;
                                }
                                if (str38 == "gd")
                                {
                                    goto Label_1AE2;
                                }
                                if (str38 == "fgs")
                                {
                                    goto Label_1BD5;
                                }
                            }
                            else
                            {
                                _kc2.set_hy_drawback(new decimal?(decimal.Parse(str28)));
                                _kc2.set_dl_drawback(new decimal?(decimal.Parse(str27)));
                                _kc2.set_zd_drawback(new decimal?(decimal.Parse(str26)));
                                _kc2.set_gd_drawback(new decimal?(decimal.Parse(str25)));
                                _kc2.set_fgs_drawback(new decimal?(decimal.Parse(str24)));
                                _kc2.set_zj_drawback(new decimal?(decimal.Parse(str23)));
                                _kc2.set_dl_rate(0f);
                                _kc2.set_zd_rate((float) int.Parse(_rate.get_zdzc()));
                                _kc2.set_gd_rate((float) int.Parse(_rate.get_gdzc()));
                                _kc2.set_fgs_rate(float.Parse(num4.ToString()));
                                _kc2.set_zj_rate(float.Parse(num3.ToString()));
                                _kc2.set_dl_name(_rate.get_dlname());
                                _kc2.set_zd_name(_rate.get_zdname());
                                _kc2.set_gd_name(_rate.get_gdname());
                                _kc2.set_fgs_name(_rate.get_fgsname());
                            }
                        }
                        goto Label_1CB9;
                    Label_19E4:
                        _kc2.set_hy_drawback(0);
                        _kc2.set_dl_drawback(new decimal?(decimal.Parse(str27)));
                        _kc2.set_zd_drawback(new decimal?(decimal.Parse(str26)));
                        _kc2.set_gd_drawback(new decimal?(decimal.Parse(str25)));
                        _kc2.set_fgs_drawback(new decimal?(decimal.Parse(str24)));
                        _kc2.set_zj_drawback(new decimal?(decimal.Parse(str23)));
                        _kc2.set_dl_rate(0f);
                        _kc2.set_zd_rate(0f);
                        _kc2.set_gd_rate((float) int.Parse(_rate.get_gdzc()));
                        _kc2.set_fgs_rate(float.Parse(num4.ToString()));
                        _kc2.set_zj_rate(float.Parse(num3.ToString()));
                        _kc2.set_dl_name("");
                        _kc2.set_zd_name(_rate.get_zdname());
                        _kc2.set_gd_name(_rate.get_gdname());
                        _kc2.set_fgs_name(_rate.get_fgsname());
                        goto Label_1CB9;
                    Label_1AE2:
                        _kc2.set_hy_drawback(0);
                        _kc2.set_dl_drawback(0);
                        _kc2.set_zd_drawback(new decimal?(decimal.Parse(str26)));
                        _kc2.set_gd_drawback(new decimal?(decimal.Parse(str25)));
                        _kc2.set_fgs_drawback(new decimal?(decimal.Parse(str24)));
                        _kc2.set_zj_drawback(new decimal?(decimal.Parse(str23)));
                        _kc2.set_dl_rate(0f);
                        _kc2.set_zd_rate(0f);
                        _kc2.set_gd_rate(0f);
                        _kc2.set_fgs_rate(float.Parse(num4.ToString()));
                        _kc2.set_zj_rate(float.Parse(num3.ToString()));
                        _kc2.set_dl_name("");
                        _kc2.set_zd_name("");
                        _kc2.set_gd_name(_rate.get_gdname());
                        _kc2.set_fgs_name(_rate.get_fgsname());
                        goto Label_1CB9;
                    Label_1BD5:
                        _kc2.set_hy_drawback(0);
                        _kc2.set_dl_drawback(0);
                        _kc2.set_zd_drawback(0);
                        _kc2.set_gd_drawback(new decimal?(decimal.Parse(str25)));
                        _kc2.set_fgs_drawback(new decimal?(decimal.Parse(str24)));
                        _kc2.set_zj_drawback(new decimal?(decimal.Parse(str23)));
                        _kc2.set_dl_rate(0f);
                        _kc2.set_zd_rate(0f);
                        _kc2.set_gd_rate(0f);
                        _kc2.set_fgs_rate(0f);
                        _kc2.set_zj_rate(float.Parse(num3.ToString()));
                        _kc2.set_dl_name("");
                        _kc2.set_zd_name("");
                        _kc2.set_gd_name("");
                        _kc2.set_fgs_name(_rate.get_fgsname());
                    Label_1CB9:
                        _kc2.set_is_payment(0);
                        _kc2.set_sale_type(1);
                        _kc2.set_m_type(-2);
                        _kc2.set_kind(str16);
                        _kc2.set_ip(LSRequest.GetIP());
                        _kc2.set_lottery_type(14);
                        _kc2.set_lottery_name(base.GetGameNameByID(_kc2.get_lottery_type().ToString()));
                        _kc2.set_isLM(0);
                        _kc2.set_ordervalidcode(Utils.GetOrderValidCode(_kc2.get_u_name(), _kc2.get_order_num(), _kc2.get_bet_val(), _kc2.get_odds(), _kc2.get_kind(), Convert.ToInt32(_kc2.get_phase_id()), Convert.ToInt32(_kc2.get_odds_id()), Convert.ToDouble(_kc2.get_amount())));
                        _kc2.set_odds_zj(str22);
                        int num19 = 0;
                        CallBLL.cz_bet_kc_bll.AddBetByBH(_kc2, ref num19);
                        double num20 = (double.Parse(str12) * num3) / 100.0;
                        CallBLL.cz_odds_jssfc_bll.Agent_UpdateGrandTotal(num20.ToString(), str11);
                        double num21 = double.Parse(playOddsInfo.Tables[0].Rows[0]["grand_total"].ToString()) + num20;
                        double num22 = double.Parse(playOddsInfo.Tables[0].Rows[0]["downbase"].ToString());
                        double num23 = Math.Floor((double) (num21 / num22));
                        if ((num23 >= 1.0) && (num22 >= 1.0))
                        {
                            double num24 = double.Parse(playOddsInfo.Tables[0].Rows[0]["down_odds_rate"].ToString()) * num23;
                            CallBLL.cz_odds_jssfc_bll.UpdateGrandTotalCurrentOdds(num24.ToString(), (num21 - (num22 * num23)).ToString(), str11.ToString());
                            cz_system_log _log2 = new cz_system_log();
                            _log2.set_user_name("系統");
                            _log2.set_children_name("");
                            _log2.set_category(playOddsInfo.Tables[0].Rows[0]["category"].ToString());
                            _log2.set_play_name(str18);
                            _log2.set_put_amount(str20);
                            _log2.set_l_name(LSKeys.get_JSSFC_Name());
                            _log2.set_l_phase(phase);
                            _log2.set_action("降賠率");
                            _log2.set_odds_id(int.Parse(str4));
                            string str33 = playOddsInfo.Tables[0].Rows[0]["current_odds"].ToString();
                            _log2.set_old_val(str33);
                            num31 = double.Parse(str33) - num24;
                            _log2.set_new_val(num31.ToString());
                            _log2.set_ip(LSRequest.GetIP());
                            _log2.set_add_time(DateTime.Now);
                            _log2.set_note("系統自動降賠");
                            _log2.set_type_id(Convert.ToInt32((LSEnums.LogTypeID) 0));
                            _log2.set_lottery_type(14);
                            CallBLL.cz_system_log_bll.Add(_log2);
                            cz_jp_odds _odds2 = new cz_jp_odds();
                            _odds2.set_add_time(DateTime.Now);
                            _odds2.set_odds_id(int.Parse(str11));
                            _odds2.set_phase_id(int.Parse(s));
                            _odds2.set_play_name(str18);
                            _odds2.set_put_amount(str20);
                            _odds2.set_odds(num24.ToString());
                            _odds2.set_lottery_type(14);
                            _odds2.set_phase(phase);
                            _odds2.set_old_odds(str33);
                            _odds2.set_new_odds((double.Parse(str33) - num24).ToString());
                            CallBLL.cz_jp_odds_bll.Add(_odds2);
                        }
                        this.ZDBH(model, _rate, str16, context, s, phase, str17, str11, str23, str24, str25, str26, str27, str28, str9);
                        this.GDBH(model, _rate, str16, context, s, phase, str17, str11, str23, str24, str25, str26, str27, str28, str9);
                        this.FGSBH(model, _rate, str16, context, s, phase, str17, str11, str23, str24, str25, str26, str27, str28, str9);
                    Label_20D9:
                        if (flag)
                        {
                            string str34 = "";
                            str34 = (double.Parse(str12) * (double.Parse(str19) - 1.0)).ToString();
                            string str35 = "";
                            if (nos.Trim() != "")
                            {
                                str35 = nos;
                            }
                            else
                            {
                                str35 = str20;
                            }
                            index++;
                            new Dictionary<string, object>();
                            Dictionary<string, object> dictionary3 = new Dictionary<string, object>();
                            dictionary3.Add("ordernum", str29);
                            dictionary3.Add("playname", str18);
                            dictionary3.Add("ky", str34);
                            dictionary3.Add("putamount", str35);
                            dictionary3.Add("pl", str19.ToString());
                            dictionary3.Add("amount", str12);
                            if (flag)
                            {
                                dictionary3.Add("islm", "1");
                            }
                            else
                            {
                                dictionary3.Add("islm", "0");
                            }
                            dictionary.Add("bhdata", dictionary3);
                        }
                        else
                        {
                            string str36 = "";
                            double num27 = double.Parse(str6) * (double.Parse(str19) - 1.0);
                            str36 = num27.ToString();
                            string str37 = str20;
                            List<FastSale> list = new List<FastSale>();
                            FastSale item = new FastSale();
                            item.set_success("1");
                            item.set_ordernum(str29);
                            item.set_playname(str18);
                            item.set_putamount(str37);
                            str36 = num27.ToString();
                            item.set_ky(str36);
                            item.set_islm("0");
                            item.set_pl(str19.ToString());
                            item.set_message("");
                            item.set_amount(str6);
                            list.Add(item);
                            dictionary.Add("bhdata", list);
                        }
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

        private void ZDBH(agent_userinfo_session userModel, agent_kc_rate kc_rate, string kc_kind, HttpContext context, string phase_id, string phase, string play_id, string odds_id, string zj_ds, string fgs_ds, string gd_ds, string zd_ds, string dl_ds, string hy_ds, string checkcode)
        {
            if (!string.IsNullOrEmpty(kc_rate.get_zdname()))
            {
                DataTable isAllowSale = CallBLL.cz_users_bll.GetIsAllowSale(kc_rate.get_zdname());
                if ((isAllowSale.Rows[0]["kc_allow_sale"] != null) && (Convert.ToInt32(isAllowSale.Rows[0]["kc_allow_sale"]) != 0))
                {
                    double num = 1.0;
                    DataTable autosale = CallBLL.cz_autosale_jssfc_bll.GetAutosale(kc_rate.get_zdname(), play_id);
                    if (((autosale != null) && (autosale.Rows.Count > 0)) && autosale.Rows[0]["sale_type"].ToString().Equals("0"))
                    {
                        DataTable table3 = CallBLL.cz_bet_kc_bll.GetSZZTBHByOddsid("zd", kc_rate.get_zdname(), odds_id, phase_id, 14);
                        if ((double.Parse(table3.Rows[0]["szzt"].ToString()) - num) > double.Parse(autosale.Rows[0]["max_money"].ToString()))
                        {
                            double num2 = double.Parse(table3.Rows[0]["szzt"].ToString());
                            double num3 = double.Parse(autosale.Rows[0]["max_money"].ToString());
                            double num4 = Math.Floor((double) (num2 - num3));
                            DataSet playOddsInfo = CallBLL.cz_play_jssfc_bll.GetPlayOddsInfo(odds_id);
                            string s = playOddsInfo.Tables[0].Rows[0]["current_odds"].ToString().Trim();
                            string str2 = playOddsInfo.Tables[0].Rows[0]["play_name"].ToString();
                            string str3 = playOddsInfo.Tables[0].Rows[0]["put_amount"].ToString();
                            string str4 = playOddsInfo.Tables[0].Rows[0][kc_kind.ToUpper() + "_diff"].ToString().Trim();
                            string str5 = (double.Parse(s) + double.Parse(str4)).ToString();
                            base.GetOdds_KC_EX(14, playOddsInfo.Tables[0].Rows[0]["current_odds"].ToString().Trim(), str4, ref s, odds_id);
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
                            _kc.set_lottery_type(14);
                            _kc.set_lottery_name(base.GetGameNameByID(_kc.get_lottery_type().ToString()));
                            _kc.set_isLM(0);
                            _kc.set_ordervalidcode(Utils.GetOrderValidCode(_kc.get_zd_name(), _kc.get_order_num(), _kc.get_bet_val(), _kc.get_odds(), _kc.get_kind(), Convert.ToInt32(_kc.get_phase_id()), Convert.ToInt32(_kc.get_odds_id()), Convert.ToDouble(_kc.get_amount())));
                            _kc.set_odds_zj(str5);
                            int num7 = 0;
                            CallBLL.cz_bet_kc_bll.AddBetByBH(_kc, ref num7);
                            double num8 = (num4 * double.Parse(str6)) / 100.0;
                            CallBLL.cz_odds_jssfc_bll.Agent_UpdateGrandTotal(num8.ToString(), odds_id);
                            double num9 = double.Parse(playOddsInfo.Tables[0].Rows[0]["grand_total"].ToString()) + num8;
                            double num10 = double.Parse(playOddsInfo.Tables[0].Rows[0]["downbase"].ToString());
                            double num11 = Math.Floor((double) (num9 / num10));
                            if ((num11 > 1.0) && (num10 >= 1.0))
                            {
                                double num12 = double.Parse(playOddsInfo.Tables[0].Rows[0]["down_odds_rate"].ToString()) * num11;
                                CallBLL.cz_odds_jssfc_bll.UpdateGrandTotalCurrentOdds(num12.ToString(), (num9 - (num10 * num11)).ToString(), odds_id.ToString());
                                cz_system_log _log = new cz_system_log();
                                _log.set_user_name("系統");
                                _log.set_children_name("");
                                _log.set_category(playOddsInfo.Tables[0].Rows[0]["category"].ToString());
                                _log.set_play_name(str2);
                                _log.set_put_amount(str3);
                                _log.set_l_name(LSKeys.get_JSSFC_Name());
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
                                _log.set_lottery_type(14);
                                CallBLL.cz_system_log_bll.Add(_log);
                                cz_jp_odds _odds = new cz_jp_odds();
                                _odds.set_add_time(DateTime.Now);
                                _odds.set_odds_id(int.Parse(odds_id));
                                _odds.set_phase_id(int.Parse(phase_id));
                                _odds.set_play_name(str2);
                                _odds.set_put_amount(str3);
                                _odds.set_odds(num12.ToString());
                                _odds.set_lottery_type(14);
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

        protected void ZDBH_LM(agent_userinfo_session userModel, agent_kc_rate kc_rate, string kc_kind, HttpContext context, string phase_id, string phase, string play_id, string odds_id, string zj_ds, string fgs_ds, string gd_ds, string zd_ds, string dl_ds, string hy_ds, string checkcode)
        {
            if (!string.IsNullOrEmpty(kc_rate.get_zdname()))
            {
                DataTable isAllowSale = CallBLL.cz_users_bll.GetIsAllowSale(kc_rate.get_zdname());
                if ((isAllowSale.Rows[0]["kc_allow_sale"] != null) && (Convert.ToInt32(isAllowSale.Rows[0]["kc_allow_sale"]) != 0))
                {
                    double num = 1.0;
                    DataTable autosale = CallBLL.cz_autosale_jssfc_bll.GetAutosale(kc_rate.get_zdname(), play_id);
                    if (((autosale != null) && (autosale.Rows.Count > 0)) && autosale.Rows[0]["sale_type"].ToString().Equals("0"))
                    {
                        DataTable table3 = CallBLL.cz_bet_kc_bll.GetBHGroup("zd", kc_rate.get_zdname(), odds_id, phase_id, autosale.Rows[0]["max_money"].ToString(), num.ToString(), 14);
                        if ((table3 != null) && (table3.Rows.Count > 0))
                        {
                            dl_ds = zd_ds;
                            string s = kc_rate.get_zjzc();
                            string str2 = kc_rate.get_fgszc();
                            if (kc_rate.get_zcyg() == "1")
                            {
                                s = ((100.0 - double.Parse(kc_rate.get_fgszc())) - double.Parse(kc_rate.get_gdzc())).ToString();
                            }
                            else
                            {
                                str2 = ((100.0 - double.Parse(kc_rate.get_zjzc())) - double.Parse(kc_rate.get_gdzc())).ToString();
                            }
                            DataSet playOddsInfo = CallBLL.cz_play_jssfc_bll.GetPlayOddsInfo(odds_id);
                            string str3 = playOddsInfo.Tables[0].Rows[0]["current_odds"].ToString().Trim();
                            string str4 = playOddsInfo.Tables[0].Rows[0]["play_name"].ToString();
                            string str5 = playOddsInfo.Tables[0].Rows[0]["put_amount"].ToString();
                            string str6 = playOddsInfo.Tables[0].Rows[0][kc_kind.ToUpper() + "_diff"].ToString().Trim();
                            string str7 = (double.Parse(str3) + double.Parse(str6)).ToString();
                            base.GetOdds_KC_EX(14, playOddsInfo.Tables[0].Rows[0]["current_odds"].ToString().Trim(), str6, ref str3, odds_id);
                            string nos = "";
                            foreach (DataRow row in table3.Rows)
                            {
                                double num4 = Math.Floor(double.Parse(row["bhl"].ToString().Trim()));
                                nos = row["item"].ToString().Trim();
                                string str9 = this.get_ball_pl(play_id, double.Parse(str3), nos);
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
                                _kc.set_play_name(str4);
                                _kc.set_bet_val(nos);
                                _kc.set_bet_wt(str9);
                                _kc.set_odds(str3);
                                _kc.set_amount(new decimal?(Convert.ToDecimal(num4.ToString())));
                                _kc.set_profit(0);
                                _kc.set_unit_cnt(1);
                                _kc.set_lm_type(0);
                                _kc.set_hy_drawback(0);
                                _kc.set_dl_drawback(new decimal?(decimal.Parse(dl_ds)));
                                _kc.set_zd_drawback(new decimal?(decimal.Parse(zd_ds)));
                                _kc.set_gd_drawback(new decimal?(decimal.Parse(gd_ds)));
                                _kc.set_fgs_drawback(new decimal?(decimal.Parse(fgs_ds)));
                                _kc.set_zj_drawback(new decimal?(decimal.Parse(zj_ds)));
                                _kc.set_dl_rate(0f);
                                _kc.set_zd_rate(0f);
                                _kc.set_gd_rate((float) int.Parse(kc_rate.get_gdzc()));
                                _kc.set_fgs_rate((float) int.Parse(str2));
                                _kc.set_zj_rate((float) int.Parse(s));
                                _kc.set_dl_name("");
                                _kc.set_zd_name(kc_rate.get_zdname());
                                _kc.set_gd_name(kc_rate.get_gdname());
                                _kc.set_fgs_name(kc_rate.get_fgsname());
                                _kc.set_is_payment(0);
                                _kc.set_sale_type(1);
                                _kc.set_m_type(-2);
                                _kc.set_kind(kc_kind);
                                _kc.set_ip(LSRequest.GetIP());
                                _kc.set_lottery_type(14);
                                _kc.set_lottery_name(base.GetGameNameByID(_kc.get_lottery_type().ToString()));
                                _kc.set_isLM(1);
                                _kc.set_bet_group(nos);
                                _kc.set_ordervalidcode(Utils.GetOrderValidCode(_kc.get_zd_name(), _kc.get_order_num(), _kc.get_bet_val(), _kc.get_odds(), _kc.get_kind(), Convert.ToInt32(_kc.get_phase_id()), Convert.ToInt32(_kc.get_odds_id()), Convert.ToDouble(_kc.get_amount())));
                                _kc.set_odds_zj(str7);
                                int num5 = 0;
                                CallBLL.cz_bet_kc_bll.AddBetByBH(_kc, ref num5);
                                double num6 = (double.Parse(num4.ToString()) * double.Parse(s)) / 100.0;
                                CallBLL.cz_odds_jssfc_bll.Agent_UpdateGrandTotal(num6.ToString(), odds_id);
                                double num7 = double.Parse(playOddsInfo.Tables[0].Rows[0]["grand_total"].ToString()) + num6;
                                double num8 = double.Parse(playOddsInfo.Tables[0].Rows[0]["downbase"].ToString());
                                double num9 = Math.Floor((double) (num7 / num8));
                                if ((num9 >= 1.0) && (num8 >= 1.0))
                                {
                                    double num10 = double.Parse(playOddsInfo.Tables[0].Rows[0]["down_odds_rate"].ToString()) * num9;
                                    CallBLL.cz_odds_jssfc_bll.UpdateGrandTotalCurrentOdds(num10.ToString(), (num7 - (num8 * num9)).ToString(), odds_id.ToString());
                                    cz_system_log _log = new cz_system_log();
                                    _log.set_user_name("系統");
                                    _log.set_children_name("");
                                    _log.set_category(playOddsInfo.Tables[0].Rows[0]["category"].ToString());
                                    _log.set_play_name(str4);
                                    _log.set_put_amount(str5);
                                    _log.set_l_name(LSKeys.get_JSSFC_Name());
                                    _log.set_l_phase(phase);
                                    _log.set_action("降賠率");
                                    _log.set_odds_id(int.Parse(odds_id));
                                    string str10 = playOddsInfo.Tables[0].Rows[0]["current_odds"].ToString();
                                    _log.set_old_val(str10);
                                    _log.set_new_val((double.Parse(str10) - num10).ToString());
                                    _log.set_ip(LSRequest.GetIP());
                                    _log.set_add_time(DateTime.Now);
                                    _log.set_note("系統自動降賠");
                                    _log.set_type_id(Convert.ToInt32((LSEnums.LogTypeID) 0));
                                    _log.set_lottery_type(14);
                                    CallBLL.cz_system_log_bll.Add(_log);
                                    cz_jp_odds _odds = new cz_jp_odds();
                                    _odds.set_add_time(DateTime.Now);
                                    _odds.set_odds_id(int.Parse(odds_id));
                                    _odds.set_phase_id(int.Parse(phase_id));
                                    _odds.set_play_name(str4);
                                    _odds.set_put_amount(str5);
                                    _odds.set_odds(num10.ToString());
                                    _odds.set_lottery_type(14);
                                    _odds.set_phase(phase);
                                    _odds.set_old_odds(str10);
                                    _odds.set_new_odds((double.Parse(str10) - num10).ToString());
                                    CallBLL.cz_jp_odds_bll.Add(_odds);
                                }
                                double num12 = 0.0;
                                double num13 = 0.0;
                                string[] strArray = str3.Split(new char[] { ',' });
                                num12 = double.Parse(strArray[0].ToString());
                                if (strArray.Length > 1)
                                {
                                    num13 = double.Parse(strArray[1].ToString());
                                }
                                double num14 = 0.0;
                                double num15 = 0.0;
                                string[] strArray2 = str7.Split(new char[] { ',' });
                                num14 = double.Parse(strArray2[0].ToString());
                                if (strArray2.Length > 1)
                                {
                                    num15 = double.Parse(strArray2[1].ToString());
                                }
                                cz_splitgroup_jssfc _jssfc = new cz_splitgroup_jssfc();
                                _jssfc.set_odds_id(new int?(int.Parse(odds_id)));
                                _jssfc.set_item_money(new decimal?(Convert.ToDecimal(num4)));
                                _jssfc.set_odds1(new decimal?(Convert.ToDecimal(num12.ToString())));
                                _jssfc.set_odds2(new decimal?(Convert.ToDecimal(num13.ToString())));
                                _jssfc.set_bet_id(new int?(num5));
                                _jssfc.set_item(nos);
                                _jssfc.set_odds1_zj(new decimal?(Convert.ToDecimal(num14.ToString())));
                                _jssfc.set_odds2_zj(new decimal?(Convert.ToDecimal(num15.ToString())));
                                CallBLL.cz_splitgroup_jssfc_bll.Agent_AddSplitGroup(_jssfc);
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

