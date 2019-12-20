namespace Agent.Web.L_SIX.Handler
{
    using Agent.Web.Handler;
    using Agent.Web.WebBase;
    using LotterySystem.Common;
    using LotterySystem.DBUtility;
    using LotterySystem.Model;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Web;

    public class HandlerBalance : BaseHandler
    {
        private agent_userinfo_session uModel;

        private void Balance(HttpContext context, string p_id)
        {
            int num = FileCacheHelper.get_GetCommandTimeoutCustom();
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            DateTime now = DateTime.Now;
            StreamReader reader = new StreamReader(Path.Combine(base.Server.MapPath("."), "../ProgressBar.htm"), Encoding.GetEncoding("gb2312"));
            string s = reader.ReadToEnd();
            reader.Close();
            context.Response.Write(s);
            context.Response.Flush();
            Thread.Sleep(100);
            cz_phase_six phaseModel = CallBLL.cz_phase_six_bll.GetPhaseModel(int.Parse(p_id));
            if (phaseModel == null)
            {
                base.Un_Balance_Lock();
                context.Response.End();
            }
            else if (phaseModel.get_is_opendata() == 1)
            {
                base.Un_Balance_Lock();
                context.Response.End();
            }
            string message = "";
            string zqh = phaseModel.get_n1();
            string str5 = phaseModel.get_n2();
            string str6 = phaseModel.get_n3();
            string str7 = phaseModel.get_n4();
            string str8 = phaseModel.get_n5();
            string str9 = phaseModel.get_n6();
            string na = phaseModel.get_sn();
            string str11 = "";
            string str12 = "";
            string str13 = zqh + "," + str5 + "," + str6 + "," + str7 + "," + str8 + "," + str9;
            if (!this.ValidParam(str13, na, ref message))
            {
                context.Response.Write("<script>alert('" + message + "');history.go(-1);</script>");
                base.Un_Balance_Lock();
                context.Response.End();
            }
            else
            {
                string str27;
                string str38;
                for (int i = 1; i <= 6; i++)
                {
                    if (string.IsNullOrEmpty("n" + i) || ("n" + i).Equals("00"))
                    {
                        context.Response.Write("<script>alert('碼沒填完或沒有送出結果！！');history.go(-1);</script>");
                        base.Un_Balance_Lock();
                        context.Response.End();
                    }
                }
                if (string.IsNullOrEmpty(na) || na.Equals("00"))
                {
                    context.Response.Write("<script>alert('碼沒填完或沒有送出結果！！');history.go(-1);</script>");
                    base.Un_Balance_Lock();
                    context.Response.End();
                }
                CallBLL.cz_phase_six_bll.UpdateClosed("1", p_id);
                CallBLL.cz_odds_six_bll.UpdateIsOpen(0, "");
                str11 = zqh + "," + str5 + "," + str6 + "," + str7 + "," + str8 + "," + str9 + "," + na;
                str12 = zqh + "," + str5 + "," + str6 + "," + str7 + "," + str8 + "," + str9;
                string[] strArray = str12.Split(new char[] { ',' });
                string str14 = "";
                string str15 = "";
                string[] strArray2 = str11.Split(new char[] { ',' });
                string str16 = "";
                foreach (string str17 in strArray2)
                {
                    str16 = base.GetZodiacNum(str17.Trim()).Trim();
                    str15 = str15 + str16 + ",";
                    str16 = base.GetZodiacName(str17.Trim()).Trim();
                    str14 = str14 + str16 + ",";
                }
                string str18 = "";
                string[] strArray3 = str11.Split(new char[] { ',' });
                int num3 = 0;
                string str19 = "";
                foreach (string str20 in strArray3)
                {
                    num3 = int.Parse(str20) % 10;
                    if (num3 == 0)
                    {
                        str19 = "10";
                    }
                    else
                    {
                        str19 = "0" + num3.ToString();
                    }
                    str18 = str18 + str19 + ",";
                }
                DbHelperSQL_ADMIN.ExecuteSql("delete from cz_banlance_log where   DATEDIFF(day ,add_time,getdate())>10", num, null);
                DataTable table = null;
                string str21 = string.Format("select  *  from cz_balance_operation_log where phase_id=@phase_id ", new object[0]);
                SqlParameter[] parameterArray11 = new SqlParameter[1];
                SqlParameter parameter5 = new SqlParameter("@phase_id", SqlDbType.NVarChar) {
                    Value = p_id
                };
                parameterArray11[0] = parameter5;
                SqlParameter[] parameterArray = parameterArray11;
                DataSet set = DbHelperSQL.Query(str21, num, parameterArray);
                if ((set != null) && (set.Tables.Count > 0))
                {
                    str21 = string.Format("select *  from  cz_banlance_log  where banlance_id in (select  max(banlance_id)  from cz_banlance_log where lottery_type={0} and  phase=@phase    group by u_name)  and profit<>0 ", 100);
                    parameterArray11 = new SqlParameter[1];
                    SqlParameter parameter = new SqlParameter("@phase", SqlDbType.NVarChar) {
                        Value = p_id
                    };
                    parameterArray11[0] = parameter;
                    SqlParameter[] parameterArray2 = parameterArray11;
                    set = DbHelperSQL.Query(str21, num, parameterArray2);
                    if ((set != null) && (set.Tables.Count > 0))
                    {
                        table = set.Tables[0];
                    }
                    foreach (DataRow row in table.Rows)
                    {
                        DbHelperSQL_ADMIN.ExecuteSql(string.Format("update cz_users set six_usable_credit=six_usable_credit-{0}  where u_name='{1}'", row["profit"].ToString().Trim(), row["u_name"].ToString().Trim()), num, null);
                    }
                    string str22 = string.Format(" update cz_phase_six set is_opendata=0,is_payment=0 where p_id=@p_id ", new object[0]);
                    parameterArray11 = new SqlParameter[1];
                    SqlParameter parameter2 = new SqlParameter("@p_id", SqlDbType.NVarChar) {
                        Value = p_id
                    };
                    parameterArray11[0] = parameter2;
                    SqlParameter[] parameterArray3 = parameterArray11;
                    DbHelperSQL_ADMIN.ExecuteSql(str22, num, parameterArray3);
                    string str23 = string.Format(" update cz_bet_six set is_payment=0 where phase_id=@phase_id ", new object[0]);
                    parameterArray11 = new SqlParameter[1];
                    SqlParameter parameter3 = new SqlParameter("@phase_id", SqlDbType.NVarChar) {
                        Value = p_id
                    };
                    parameterArray11[0] = parameter3;
                    SqlParameter[] parameterArray4 = parameterArray11;
                    DbHelperSQL_ADMIN.ExecuteSql(str23, num, parameterArray4);
                }
                string str24 = string.Format(" update cz_bet_six set profit=-amount,profit_zj=-amount,is_payment=1,paymentvalidcode='{0}' where phase_id=@phase_id and (play_id={1} or play_id={2}) and bet_val<>@bet_val  ", "", 0x16379, 0x1637a);
                parameterArray11 = new SqlParameter[2];
                SqlParameter parameter6 = new SqlParameter("@phase_id", SqlDbType.NVarChar) {
                    Value = p_id
                };
                parameterArray11[0] = parameter6;
                SqlParameter parameter7 = new SqlParameter("@bet_val", SqlDbType.NVarChar) {
                    Value = na
                };
                parameterArray11[1] = parameter7;
                SqlParameter[] parameterArray5 = parameterArray11;
                DbHelperSQL_ADMIN.ExecuteSql(str24, num, parameterArray5);
                str24 = string.Format(" update cz_bet_six set profit=amount*odds-amount,profit_zj=amount*odds_zj-amount,is_payment=1,paymentvalidcode='{0}' where phase_id=@phase_id and (play_id={1} or play_id={2}) and bet_val=@bet_val  ", "", 0x16379, 0x1637a);
                parameterArray11 = new SqlParameter[2];
                SqlParameter parameter8 = new SqlParameter("@phase_id", SqlDbType.NVarChar) {
                    Value = p_id
                };
                parameterArray11[0] = parameter8;
                SqlParameter parameter9 = new SqlParameter("@bet_val", SqlDbType.NVarChar) {
                    Value = na
                };
                parameterArray11[1] = parameter9;
                SqlParameter[] parameterArray6 = parameterArray11;
                DbHelperSQL_ADMIN.ExecuteSql(str24, num, parameterArray6);
                string str25 = string.Format(" select bet_id,play_id,play_name,odds_id,category,bet_val,bet_group,odds,odds_zj,amount,unit_cnt,bet_wt,order_num,bet_wt_zj from cz_bet_six with(NOLOCK)  where phase_id=@phase_id  and play_id<>{0} and play_id<>{1} ", 0x16379, 0x1637a);
                parameterArray11 = new SqlParameter[1];
                SqlParameter parameter10 = new SqlParameter("@phase_id", SqlDbType.NVarChar) {
                    Value = p_id
                };
                parameterArray11[0] = parameter10;
                SqlParameter[] parameterArray7 = parameterArray11;
                DataSet set2 = DbHelperSQL.Query(str25, num, parameterArray7);
                if ((set2 != null) && (set2.Tables.Count > 0))
                {
                    table = set2.Tables[0];
                }
                int index = 0;
                double num5 = 0.0;
                double num6 = 0.0;
                string str26 = "";
                int num7 = 0;
                double num8 = 0.0;
                double num9 = (table == null) ? ((double) 0) : ((double) table.Rows.Count);
                double num10 = 0.0;
                string str28 = "";
                foreach (DataRow row2 in table.Rows)
                {
                    string str30;
                    num8++;
                    if ((((num8 / num9) * 100.0) >= 1.0) && (Math.Floor((double) ((num8 / num9) * 100.0)) > num10))
                    {
                        Thread.Sleep(1);
                        num10 = Math.Floor((double) ((num8 / num9) * 100.0));
                        str27 = "<script>SetPorgressBar('已結算" + num8.ToString() + "','" + num10.ToString() + "'); </script>";
                        context.Response.Write(str27);
                        context.Response.Flush();
                    }
                    if (num7 > 0x7d0)
                    {
                        num7 = 0;
                    }
                    num7++;
                    num5 = 0.0;
                    num6 = 0.0;
                    str26 = "";
                    index = 0;
                    str28 = row2["odds_id"].ToString();
                    string str29 = "";
                    switch (row2["play_id"].ToString().Trim())
                    {
                        case "91003":
                            num5 = this.count_dx(row2["bet_val"].ToString().Trim(), na, double.Parse(row2["amount"].ToString()), row2["odds"].ToString().Trim());
                            num6 = this.count_dx(row2["bet_val"].ToString().Trim(), na, double.Parse(row2["amount"].ToString()), row2["odds_zj"].ToString().Trim());
                            if (num5 != -double.Parse(row2["amount"].ToString()))
                            {
                                str29 = string.Format(" update cz_bet_six set profit={0},profit_zj={1},is_payment=1,isTie={2},paymentvalidcode='{3}' where bet_id={4} ", new object[] { num5, num6, (num5 == 0.0) ? 1 : 0, str26, row2["bet_id"].ToString() });
                            }
                            goto Label_408A;

                        case "91004":
                            num5 = this.count_ds(row2["bet_val"].ToString().Trim(), na, double.Parse(row2["amount"].ToString()), row2["odds"].ToString().Trim());
                            num6 = this.count_ds(row2["bet_val"].ToString().Trim(), na, double.Parse(row2["amount"].ToString()), row2["odds_zj"].ToString().Trim());
                            if (num5 != -double.Parse(row2["amount"].ToString()))
                            {
                                str29 = string.Format(" update cz_bet_six set profit={0},profit_zj={1},is_payment=1,isTie={2},paymentvalidcode='{3}' where bet_id={4} ", new object[] { num5, num6, (num5 == 0.0) ? 1 : 0, str26, row2["bet_id"].ToString() });
                            }
                            goto Label_408A;

                        case "91005":
                            num5 = this.count_hsds(row2["bet_val"].ToString().Trim(), na, double.Parse(row2["amount"].ToString()), row2["odds"].ToString().Trim());
                            num6 = this.count_hsds(row2["bet_val"].ToString().Trim(), na, double.Parse(row2["amount"].ToString()), row2["odds_zj"].ToString().Trim());
                            if (num5 != -double.Parse(row2["amount"].ToString()))
                            {
                                str29 = string.Format(" update cz_bet_six set profit={0},profit_zj={1},is_payment=1,isTie={2},paymentvalidcode='{3}' where bet_id={4} ", new object[] { num5, num6, (num5 == 0.0) ? 1 : 0, str26, row2["bet_id"].ToString() });
                            }
                            goto Label_408A;

                        case "91006":
                            num5 = this.count_tmsx(row2["bet_val"].ToString().Trim(), na, double.Parse(row2["amount"].ToString()), row2["odds"].ToString().Trim());
                            num6 = this.count_tmsx(row2["bet_val"].ToString().Trim(), na, double.Parse(row2["amount"].ToString()), row2["odds_zj"].ToString().Trim());
                            if (num5 != -double.Parse(row2["amount"].ToString()))
                            {
                                str29 = string.Format(" update cz_bet_six set profit={0},profit_zj={1},is_payment=1,isTie={2},paymentvalidcode='{3}' where bet_id={4} ", new object[] { num5, num6, (num5 == 0.0) ? 1 : 0, str26, row2["bet_id"].ToString() });
                            }
                            goto Label_408A;

                        case "91007":
                            num5 = this.count_sb(row2["bet_val"].ToString().Trim(), na, double.Parse(row2["amount"].ToString()), row2["odds"].ToString().Trim());
                            num6 = this.count_sb(row2["bet_val"].ToString().Trim(), na, double.Parse(row2["amount"].ToString()), row2["odds_zj"].ToString().Trim());
                            if (num5 != -double.Parse(row2["amount"].ToString()))
                            {
                                str29 = string.Format(" update cz_bet_six set profit={0},profit_zj={1},is_payment=1,isTie={2},paymentvalidcode='{3}' where bet_id={4} ", new object[] { num5, num6, (num5 == 0.0) ? 1 : 0, str26, row2["bet_id"].ToString() });
                            }
                            goto Label_408A;

                        case "91008":
                        case "91057":
                            num5 = this.count_bb(row2["bet_val"].ToString().Trim(), na, double.Parse(row2["amount"].ToString()), row2["odds"].ToString().Trim());
                            num6 = this.count_bb(row2["bet_val"].ToString().Trim(), na, double.Parse(row2["amount"].ToString()), row2["odds_zj"].ToString().Trim());
                            if (num5 != -double.Parse(row2["amount"].ToString()))
                            {
                                str29 = string.Format(" update cz_bet_six set profit={0},profit_zj={1},is_payment=1,isTie={2},paymentvalidcode='{3}' where bet_id={4} ", new object[] { num5, num6, (num5 == 0.0) ? 1 : 0, str26, row2["bet_id"].ToString() });
                            }
                            goto Label_408A;

                        case "91009":
                            num5 = this.count_zm(row2["bet_val"].ToString().Trim(), str12, double.Parse(row2["amount"].ToString()), row2["odds"].ToString().Trim());
                            num6 = this.count_zm(row2["bet_val"].ToString().Trim(), str12, double.Parse(row2["amount"].ToString()), row2["odds_zj"].ToString().Trim());
                            if (num5 != -double.Parse(row2["amount"].ToString()))
                            {
                                str29 = string.Format(" update cz_bet_six set profit={0},profit_zj={1},is_payment=1,isTie={2},paymentvalidcode='{3}' where bet_id={4} ", new object[] { num5, num6, (num5 == 0.0) ? 1 : 0, str26, row2["bet_id"].ToString() });
                            }
                            goto Label_408A;

                        case "91010":
                            num5 = this.count_tm(row2["bet_val"].ToString().Trim(), zqh, double.Parse(row2["amount"].ToString()), row2["odds"].ToString().Trim());
                            num6 = this.count_tm(row2["bet_val"].ToString().Trim(), zqh, double.Parse(row2["amount"].ToString()), row2["odds_zj"].ToString().Trim());
                            if (num5 != -double.Parse(row2["amount"].ToString()))
                            {
                                str29 = string.Format(" update cz_bet_six set profit={0},profit_zj={1},is_payment=1,isTie={2},paymentvalidcode='{3}' where bet_id={4} ", new object[] { num5, num6, (num5 == 0.0) ? 1 : 0, str26, row2["bet_id"].ToString() });
                            }
                            goto Label_408A;

                        case "91025":
                            num5 = this.count_tm(row2["bet_val"].ToString().Trim(), str5, double.Parse(row2["amount"].ToString()), row2["odds"].ToString().Trim());
                            num6 = this.count_tm(row2["bet_val"].ToString().Trim(), str5, double.Parse(row2["amount"].ToString()), row2["odds_zj"].ToString().Trim());
                            if (num5 != -double.Parse(row2["amount"].ToString()))
                            {
                                str29 = string.Format(" update cz_bet_six set profit={0},profit_zj={1},is_payment=1,isTie={2},paymentvalidcode='{3}' where bet_id={4} ", new object[] { num5, num6, (num5 == 0.0) ? 1 : 0, str26, row2["bet_id"].ToString() });
                            }
                            goto Label_408A;

                        case "91026":
                            num5 = this.count_tm(row2["bet_val"].ToString().Trim(), str6, double.Parse(row2["amount"].ToString()), row2["odds"].ToString().Trim());
                            num6 = this.count_tm(row2["bet_val"].ToString().Trim(), str6, double.Parse(row2["amount"].ToString()), row2["odds_zj"].ToString().Trim());
                            if (num5 != -double.Parse(row2["amount"].ToString()))
                            {
                                str29 = string.Format(" update cz_bet_six set profit={0},profit_zj={1},is_payment=1,isTie={2},paymentvalidcode='{3}' where bet_id={4} ", new object[] { num5, num6, (num5 == 0.0) ? 1 : 0, str26, row2["bet_id"].ToString() });
                            }
                            goto Label_408A;

                        case "91027":
                            num5 = this.count_tm(row2["bet_val"].ToString().Trim(), str7, double.Parse(row2["amount"].ToString()), row2["odds"].ToString().Trim());
                            num6 = this.count_tm(row2["bet_val"].ToString().Trim(), str7, double.Parse(row2["amount"].ToString()), row2["odds_zj"].ToString().Trim());
                            if (num5 != -double.Parse(row2["amount"].ToString()))
                            {
                                str29 = string.Format(" update cz_bet_six set profit={0},profit_zj={1},is_payment=1,isTie={2},paymentvalidcode='{3}' where bet_id={4} ", new object[] { num5, num6, (num5 == 0.0) ? 1 : 0, str26, row2["bet_id"].ToString() });
                            }
                            goto Label_408A;

                        case "91028":
                            num5 = this.count_tm(row2["bet_val"].ToString().Trim(), str8, double.Parse(row2["amount"].ToString()), row2["odds"].ToString().Trim());
                            num6 = this.count_tm(row2["bet_val"].ToString().Trim(), str8, double.Parse(row2["amount"].ToString()), row2["odds_zj"].ToString().Trim());
                            if (num5 != -double.Parse(row2["amount"].ToString()))
                            {
                                str29 = string.Format(" update cz_bet_six set profit={0},profit_zj={1},is_payment=1,isTie={2},paymentvalidcode='{3}' where bet_id={4} ", new object[] { num5, num6, (num5 == 0.0) ? 1 : 0, str26, row2["bet_id"].ToString() });
                            }
                            goto Label_408A;

                        case "91029":
                            num5 = this.count_tm(row2["bet_val"].ToString().Trim(), str9, double.Parse(row2["amount"].ToString()), row2["odds"].ToString().Trim());
                            num6 = this.count_tm(row2["bet_val"].ToString().Trim(), str9, double.Parse(row2["amount"].ToString()), row2["odds_zj"].ToString().Trim());
                            if (num5 != -double.Parse(row2["amount"].ToString()))
                            {
                                str29 = string.Format(" update cz_bet_six set profit={0},profit_zj={1},is_payment=1,isTie={2},paymentvalidcode='{3}' where bet_id={4} ", new object[] { num5, num6, (num5 == 0.0) ? 1 : 0, str26, row2["bet_id"].ToString() });
                            }
                            goto Label_408A;

                        case "91011":
                            switch (str28)
                            {
                                case "92231":
                                case "92237":
                                    goto Label_1D3F;

                                case "92232":
                                case "92238":
                                    goto Label_1D44;

                                case "92233":
                                case "92239":
                                    goto Label_1D49;

                                case "92234":
                                case "92240":
                                    goto Label_1D4E;

                                case "92235":
                                case "92241":
                                    goto Label_1D53;
                            }
                            goto Label_1D56;

                        case "91012":
                            switch (str28)
                            {
                                case "92242":
                                case "92248":
                                    goto Label_1F7A;

                                case "92243":
                                case "92249":
                                    goto Label_1F7F;

                                case "92244":
                                case "92250":
                                    goto Label_1F84;

                                case "92245":
                                case "92251":
                                    goto Label_1F89;

                                case "92246":
                                case "92252":
                                    goto Label_1F8E;

                                case "92247":
                                case "92253":
                                    goto Label_1F93;
                            }
                            goto Label_1F96;

                        case "91013":
                            switch (str28)
                            {
                                case "92254":
                                case "92260":
                                case "92266":
                                    goto Label_2220;

                                case "92255":
                                case "92261":
                                case "92267":
                                    goto Label_2225;

                                case "92256":
                                case "92262":
                                case "92268":
                                    goto Label_222A;

                                case "92257":
                                case "92263":
                                case "92269":
                                    goto Label_222F;

                                case "92258":
                                case "92264":
                                case "92270":
                                    goto Label_2234;

                                case "92259":
                                case "92265":
                                case "92271":
                                    goto Label_2239;
                            }
                            goto Label_223C;

                        case "91014":
                            switch (str28)
                            {
                                case "92272":
                                case "92278":
                                    goto Label_2460;

                                case "92273":
                                case "92279":
                                    goto Label_2465;

                                case "92274":
                                case "92280":
                                    goto Label_246A;

                                case "92275":
                                case "92281":
                                    goto Label_246F;

                                case "92276":
                                case "92282":
                                    goto Label_2474;

                                case "92277":
                                case "92283":
                                    goto Label_2479;
                            }
                            goto Label_247C;

                        case "91016":
                        case "91060":
                            num5 = this.count_sqz(row2["bet_group"].ToString().Trim(), str12, double.Parse(row2["amount"].ToString()), row2["odds"].ToString().Trim(), row2["bet_wt"].ToString().Trim());
                            num6 = this.count_sqz(row2["bet_group"].ToString().Trim(), str12, double.Parse(row2["amount"].ToString()), row2["odds_zj"].ToString().Trim(), row2["bet_wt_zj"].ToString().Trim());
                            if (num5 != -double.Parse(row2["amount"].ToString()))
                            {
                                str29 = string.Format(" update cz_bet_six set profit={0},profit_zj={1},is_payment=1,isTie={2},paymentvalidcode='{3}' where bet_id={4} ", new object[] { num5, num6, 0, str26, row2["bet_id"].ToString() });
                            }
                            goto Label_408A;

                        case "91017":
                        case "91061":
                            num5 = this.count_sqr(row2["bet_group"].ToString().Trim(), str12, double.Parse(row2["amount"].ToString()), row2["odds"].ToString().Trim(), row2["bet_wt"].ToString().Trim());
                            num6 = this.count_sqr(row2["bet_group"].ToString().Trim(), str12, double.Parse(row2["amount"].ToString()), row2["odds_zj"].ToString().Trim(), row2["bet_wt_zj"].ToString().Trim());
                            if (num5 != -double.Parse(row2["amount"].ToString()))
                            {
                                str29 = string.Format(" update cz_bet_six set profit={0},profit_zj={1},is_payment=1,isTie={2},paymentvalidcode='{3}' where bet_id={4} ", new object[] { num5, num6, 0, str26, row2["bet_id"].ToString() });
                            }
                            goto Label_408A;

                        case "91018":
                        case "91062":
                            num5 = this.count_rqz(row2["bet_group"].ToString().Trim(), str12, double.Parse(row2["amount"].ToString()), row2["odds"].ToString().Trim(), row2["bet_wt"].ToString().Trim());
                            num6 = this.count_rqz(row2["bet_group"].ToString().Trim(), str12, double.Parse(row2["amount"].ToString()), row2["odds_zj"].ToString().Trim(), row2["bet_wt_zj"].ToString().Trim());
                            if (num5 != -double.Parse(row2["amount"].ToString()))
                            {
                                str29 = string.Format(" update cz_bet_six set profit={0},profit_zj={1},is_payment=1,isTie={2},paymentvalidcode='{3}' where bet_id={4} ", new object[] { num5, num6, 0, str26, row2["bet_id"].ToString() });
                            }
                            goto Label_408A;

                        case "91019":
                        case "91063":
                            num5 = this.count_rzt(row2["bet_group"].ToString().Trim(), str12, na, double.Parse(row2["amount"].ToString()), row2["odds"].ToString().Trim(), row2["bet_wt"].ToString().Trim());
                            num6 = this.count_rzt(row2["bet_group"].ToString().Trim(), str12, na, double.Parse(row2["amount"].ToString()), row2["odds_zj"].ToString().Trim(), row2["bet_wt_zj"].ToString().Trim());
                            if (num5 != -double.Parse(row2["amount"].ToString()))
                            {
                                str29 = string.Format(" update cz_bet_six set profit={0},profit_zj={1},is_payment=1,isTie={2},paymentvalidcode='{3}' where bet_id={4} ", new object[] { num5, num6, 0, str26, row2["bet_id"].ToString() });
                            }
                            goto Label_408A;

                        case "91020":
                        case "91064":
                            num5 = this.count_tc(row2["bet_group"].ToString().Trim(), str12, na, double.Parse(row2["amount"].ToString()), row2["odds"].ToString().Trim(), row2["bet_wt"].ToString().Trim());
                            num6 = this.count_tc(row2["bet_group"].ToString().Trim(), str12, na, double.Parse(row2["amount"].ToString()), row2["odds_zj"].ToString().Trim(), row2["bet_wt_zj"].ToString().Trim());
                            if (num5 != -double.Parse(row2["amount"].ToString()))
                            {
                                str29 = string.Format(" update cz_bet_six set profit={0},profit_zj={1},is_payment=1,isTie={2},paymentvalidcode='{3}' where bet_id={4} ", new object[] { num5, num6, 0, str26, row2["bet_id"].ToString() });
                            }
                            goto Label_408A;

                        case "91037":
                        case "91047":
                        case "91048":
                        case "91049":
                        case "91050":
                        case "91051":
                            num5 = this.count_wbz(row2["bet_group"].ToString().Trim(), str11, double.Parse(row2["amount"].ToString()), row2["odds"].ToString().Trim(), row2["bet_wt"].ToString().Trim());
                            num6 = this.count_wbz(row2["bet_group"].ToString().Trim(), str11, double.Parse(row2["amount"].ToString()), row2["odds_zj"].ToString().Trim(), row2["bet_wt_zj"].ToString().Trim());
                            if (num5 != -double.Parse(row2["amount"].ToString()))
                            {
                                str29 = string.Format(" update cz_bet_six set profit={0},profit_zj={1},is_payment=1,isTie={2},paymentvalidcode='{3}' where bet_id={4} ", new object[] { num5, num6, 0, str26, row2["bet_id"].ToString() });
                            }
                            goto Label_408A;

                        case "91040":
                        case "91065":
                            num5 = this.count_szy(row2["bet_group"].ToString().Trim(), str11, double.Parse(row2["amount"].ToString()), row2["odds"].ToString().Trim(), row2["bet_wt"].ToString().Trim());
                            num6 = this.count_szy(row2["bet_group"].ToString().Trim(), str11, double.Parse(row2["amount"].ToString()), row2["odds_zj"].ToString().Trim(), row2["bet_wt_zj"].ToString().Trim());
                            if (num5 != -double.Parse(row2["amount"].ToString()))
                            {
                                str29 = string.Format(" update cz_bet_six set profit={0},profit_zj={1},is_payment=1,isTie={2},paymentvalidcode='{3}' where bet_id={4} ", new object[] { num5, num6, 0, str26, row2["bet_id"].ToString() });
                            }
                            goto Label_408A;

                        case "91021":
                            num5 = this.count_sx(row2["bet_val"].ToString().Trim(), str11, double.Parse(row2["amount"].ToString()), row2["odds"].ToString().Trim());
                            num6 = this.count_sx(row2["bet_val"].ToString().Trim(), str11, double.Parse(row2["amount"].ToString()), row2["odds_zj"].ToString().Trim());
                            if (num5 != -double.Parse(row2["amount"].ToString()))
                            {
                                str29 = string.Format(" update cz_bet_six set profit={0},profit_zj={1},is_payment=1,isTie={2},paymentvalidcode='{3}' where bet_id={4} ", new object[] { num5, num6, 0, str26, row2["bet_id"].ToString() });
                            }
                            goto Label_408A;

                        case "91022":
                            num5 = this.count_ws(row2["bet_val"].ToString().Trim(), str11, double.Parse(row2["amount"].ToString()), row2["odds"].ToString().Trim());
                            num6 = this.count_ws(row2["bet_val"].ToString().Trim(), str11, double.Parse(row2["amount"].ToString()), row2["odds_zj"].ToString().Trim());
                            if (num5 != -double.Parse(row2["amount"].ToString()))
                            {
                                str29 = string.Format(" update cz_bet_six set profit={0},profit_zj={1},is_payment=1,isTie={2},paymentvalidcode='{3}' where bet_id={4} ", new object[] { num5, num6, 0, str26, row2["bet_id"].ToString() });
                            }
                            goto Label_408A;

                        case "91023":
                            num5 = this.count_zhdx(row2["bet_val"].ToString().Trim(), str11, double.Parse(row2["amount"].ToString()), row2["odds"].ToString().Trim());
                            num6 = this.count_zhdx(row2["bet_val"].ToString().Trim(), str11, double.Parse(row2["amount"].ToString()), row2["odds_zj"].ToString().Trim());
                            if (num5 != -double.Parse(row2["amount"].ToString()))
                            {
                                str29 = string.Format(" update cz_bet_six set profit={0},profit_zj={1},is_payment=1,isTie={2},paymentvalidcode='{3}' where bet_id={4} ", new object[] { num5, num6, 0, str26, row2["bet_id"].ToString() });
                            }
                            goto Label_408A;

                        case "91024":
                            num5 = this.count_zhds(row2["bet_val"].ToString().Trim(), str11, double.Parse(row2["amount"].ToString()), row2["odds"].ToString().Trim());
                            num6 = this.count_zhds(row2["bet_val"].ToString().Trim(), str11, double.Parse(row2["amount"].ToString()), row2["odds_zj"].ToString().Trim());
                            if (num5 != -double.Parse(row2["amount"].ToString()))
                            {
                                str29 = string.Format(" update cz_bet_six set profit={0},profit_zj={1},is_payment=1,isTie={2},paymentvalidcode='{3}' where bet_id={4} ", new object[] { num5, num6, 0, str26, row2["bet_id"].ToString() });
                            }
                            goto Label_408A;

                        case "91038":
                            num5 = this.count_wsdx(row2["bet_val"].ToString().Trim(), na, double.Parse(row2["amount"].ToString()), row2["odds"].ToString().Trim());
                            num6 = this.count_wsdx(row2["bet_val"].ToString().Trim(), na, double.Parse(row2["amount"].ToString()), row2["odds_zj"].ToString().Trim());
                            if (num5 != -double.Parse(row2["amount"].ToString()))
                            {
                                str29 = string.Format(" update cz_bet_six set profit={0},profit_zj={1},is_payment=1,isTie={2},paymentvalidcode='{3}' where bet_id={4} ", new object[] { num5, num6, (num5 == 0.0) ? 1 : 0, str26, row2["bet_id"].ToString() });
                            }
                            goto Label_408A;

                        case "91039":
                            num5 = this.count_tmtz(row2["bet_val"].ToString().Trim(), na, double.Parse(row2["amount"].ToString()), row2["odds"].ToString().Trim());
                            num5 = this.count_tmtz(row2["bet_val"].ToString().Trim(), na, double.Parse(row2["amount"].ToString()), row2["odds_zj"].ToString().Trim());
                            if (num5 != -double.Parse(row2["amount"].ToString()))
                            {
                                str29 = string.Format(" update cz_bet_six set profit={0},profit_zj={1},is_payment=1,isTie={2},paymentvalidcode='{3}' where bet_id={4} ", new object[] { num5, num6, (num5 == 0.0) ? 1 : 0, str26, row2["bet_id"].ToString() });
                            }
                            goto Label_408A;

                        case "91015":
                            num5 = this.count_zmgg(row2["bet_val"].ToString().Trim(), str12, double.Parse(row2["amount"].ToString()), row2["odds"].ToString().Trim());
                            num6 = this.count_zmgg(row2["bet_val"].ToString().Trim(), str12, double.Parse(row2["amount"].ToString()), row2["odds_zj"].ToString().Trim());
                            if (num5 != -double.Parse(row2["amount"].ToString()))
                            {
                                str29 = string.Format(" update cz_bet_six set profit={0},profit_zj={1},is_payment=1,isTie={2},paymentvalidcode='{3}' where bet_id={4} ", new object[] { num5, num6, (num5 == 0.0) ? 1 : 0, str26, row2["bet_id"].ToString() });
                            }
                            goto Label_408A;

                        case "91030":
                            num5 = this.count_6x(row2["bet_val"].ToString().Trim(), na, double.Parse(row2["amount"].ToString()), row2["odds"].ToString().Trim(), row2["bet_wt"].ToString().Trim());
                            num6 = this.count_6x(row2["bet_val"].ToString().Trim(), na, double.Parse(row2["amount"].ToString()), row2["odds_zj"].ToString().Trim(), row2["bet_wt_zj"].ToString().Trim());
                            if (num5 != -double.Parse(row2["amount"].ToString()))
                            {
                                str29 = string.Format(" update cz_bet_six set profit={0},profit_zj={1},is_payment=1,isTie={2},paymentvalidcode='{3}' where bet_id={4} ", new object[] { num5, num6, (num5 == 0.0) ? 1 : 0, str26, row2["bet_id"].ToString() });
                            }
                            goto Label_408A;

                        case "91031":
                        case "91032":
                        case "91033":
                        case "91058":
                            num5 = this.count_sxl(row2["bet_group"].ToString().Trim(), str15, double.Parse(row2["amount"].ToString()), row2["odds"].ToString().Trim(), row2["bet_wt"].ToString().Trim());
                            num6 = this.count_sxl(row2["bet_group"].ToString().Trim(), str15, double.Parse(row2["amount"].ToString()), row2["odds_zj"].ToString().Trim(), row2["bet_wt_zj"].ToString().Trim());
                            if (num5 != -double.Parse(row2["amount"].ToString()))
                            {
                                str29 = string.Format(" update cz_bet_six set profit={0},profit_zj={1},is_payment=1,isTie={2},paymentvalidcode='{3}' where bet_id={4} ", new object[] { num5, num6, 0, str26, row2["bet_id"].ToString() });
                            }
                            goto Label_408A;

                        case "91034":
                        case "91035":
                        case "91036":
                        case "91059":
                            num5 = this.count_wsl(row2["bet_group"].ToString().Trim(), str18, double.Parse(row2["amount"].ToString()), row2["odds"].ToString().Trim(), row2["bet_wt"].ToString().Trim());
                            num6 = this.count_wsl(row2["bet_group"].ToString().Trim(), str18, double.Parse(row2["amount"].ToString()), row2["odds_zj"].ToString().Trim(), row2["bet_wt_zj"].ToString().Trim());
                            if (num5 != -double.Parse(row2["amount"].ToString()))
                            {
                                str29 = string.Format(" update cz_bet_six set profit={0},profit_zj={1},is_payment=1,isTie={2},paymentvalidcode='{3}' where bet_id={4} ", new object[] { num5, num6, 0, str26, row2["bet_id"].ToString() });
                            }
                            goto Label_408A;

                        case "91041":
                        case "91042":
                        case "91043":
                        case "91044":
                        case "91045":
                        case "91046":
                            str30 = "";
                            str38 = row2["play_id"].ToString();
                            if (str38 != null)
                            {
                                if (str38 == "91041")
                                {
                                    goto Label_39E0;
                                }
                                if (str38 == "91042")
                                {
                                    goto Label_39F2;
                                }
                                if (str38 == "91043")
                                {
                                    goto Label_3A04;
                                }
                                if (str38 == "91044")
                                {
                                    goto Label_3A16;
                                }
                                if (str38 == "91045")
                                {
                                    goto Label_3A28;
                                }
                                if (str38 == "91046")
                                {
                                    goto Label_3A3A;
                                }
                            }
                            goto Label_3A4A;

                        case "91054":
                            num5 = this.count_sxbz(row2["bet_val"].ToString().Trim(), str14, double.Parse(row2["amount"].ToString()), row2["odds"].ToString().Trim());
                            num6 = this.count_sxbz(row2["bet_val"].ToString().Trim(), str14, double.Parse(row2["amount"].ToString()), row2["odds_zj"].ToString().Trim());
                            if (num5 != -double.Parse(row2["amount"].ToString()))
                            {
                                str29 = string.Format(" update cz_bet_six set profit={0},profit_zj={1},is_payment=1,isTie={2},paymentvalidcode='{3}' where bet_id={4} ", new object[] { num5, num6, 0, str26, row2["bet_id"].ToString() });
                            }
                            goto Label_408A;

                        case "91055":
                            num5 = this.count_wsbz(row2["bet_val"].ToString().Trim(), str18, double.Parse(row2["amount"].ToString()), row2["odds"].ToString().Trim());
                            num6 = this.count_wsbz(row2["bet_val"].ToString().Trim(), str18, double.Parse(row2["amount"].ToString()), row2["odds_zj"].ToString().Trim());
                            if (num5 != -double.Parse(row2["amount"].ToString()))
                            {
                                str29 = string.Format(" update cz_bet_six set profit={0},profit_zj={1},is_payment=1,isTie={2},paymentvalidcode='{3}' where bet_id={4} ", new object[] { num5, num6, 0, str26, row2["bet_id"].ToString() });
                            }
                            goto Label_408A;

                        case "91052":
                            num5 = this.count_qmds(row2["bet_val"].ToString().Trim(), str11, double.Parse(row2["amount"].ToString()), row2["odds"].ToString().Trim());
                            num6 = this.count_qmds(row2["bet_val"].ToString().Trim(), str11, double.Parse(row2["amount"].ToString()), row2["odds_zj"].ToString().Trim());
                            if (num5 != -double.Parse(row2["amount"].ToString()))
                            {
                                str29 = string.Format(" update cz_bet_six set profit={0},profit_zj={1},is_payment=1,isTie={2},paymentvalidcode='{3}' where bet_id={4} ", new object[] { num5, num6, 0, str26, row2["bet_id"].ToString() });
                            }
                            goto Label_408A;

                        case "91056":
                            num5 = this.count_qmdx(row2["bet_val"].ToString().Trim(), str11, double.Parse(row2["amount"].ToString()), row2["odds"].ToString().Trim());
                            num6 = this.count_qmdx(row2["bet_val"].ToString().Trim(), str11, double.Parse(row2["amount"].ToString()), row2["odds_zj"].ToString().Trim());
                            if (num5 != -double.Parse(row2["amount"].ToString()))
                            {
                                str29 = string.Format(" update cz_bet_six set profit={0},profit_zj={1},is_payment=1,isTie={2},paymentvalidcode='{3}' where bet_id={4} ", new object[] { num5, num6, 0, str26, row2["bet_id"].ToString() });
                            }
                            goto Label_408A;

                        case "91053":
                            num5 = this.count_wx(row2["bet_val"].ToString().Trim(), na, double.Parse(row2["amount"].ToString()), row2["odds"].ToString().Trim());
                            num6 = this.count_wx(row2["bet_val"].ToString().Trim(), na, double.Parse(row2["amount"].ToString()), row2["odds_zj"].ToString().Trim());
                            if (num5 != -double.Parse(row2["amount"].ToString()))
                            {
                                str29 = string.Format(" update cz_bet_six set profit={0},profit_zj={1},is_payment=1,isTie={2},paymentvalidcode='{3}' where bet_id={4} ", new object[] { num5, num6, 0, str26, row2["bet_id"].ToString() });
                            }
                            goto Label_408A;

                        default:
                            goto Label_408A;
                    }
                    index = 0;
                    goto Label_1D56;
                Label_1D3F:
                    index = 1;
                    goto Label_1D56;
                Label_1D44:
                    index = 2;
                    goto Label_1D56;
                Label_1D49:
                    index = 3;
                    goto Label_1D56;
                Label_1D4E:
                    index = 4;
                    goto Label_1D56;
                Label_1D53:
                    index = 5;
                Label_1D56:
                    num5 = this.count_dx(row2["bet_val"].ToString().Trim(), strArray[index], double.Parse(row2["amount"].ToString()), row2["odds"].ToString().Trim());
                    num6 = this.count_dx(row2["bet_val"].ToString().Trim(), strArray[index], double.Parse(row2["amount"].ToString()), row2["odds_zj"].ToString().Trim());
                    if (num5 != -double.Parse(row2["amount"].ToString()))
                    {
                        str29 = string.Format(" update cz_bet_six set profit={0},profit_zj={1},is_payment=1,isTie={2},paymentvalidcode='{3}' where bet_id={4} ", new object[] { num5, num6, (num5 == 0.0) ? 1 : 0, str26, row2["bet_id"].ToString() });
                    }
                    goto Label_408A;
                Label_1F7A:
                    index = 0;
                    goto Label_1F96;
                Label_1F7F:
                    index = 1;
                    goto Label_1F96;
                Label_1F84:
                    index = 2;
                    goto Label_1F96;
                Label_1F89:
                    index = 3;
                    goto Label_1F96;
                Label_1F8E:
                    index = 4;
                    goto Label_1F96;
                Label_1F93:
                    index = 5;
                Label_1F96:
                    num5 = this.count_ds(row2["bet_val"].ToString().Trim(), strArray[index], double.Parse(row2["amount"].ToString()), row2["odds"].ToString().Trim());
                    num6 = this.count_ds(row2["bet_val"].ToString().Trim(), strArray[index], double.Parse(row2["amount"].ToString()), row2["odds_zj"].ToString().Trim());
                    if (num5 != -double.Parse(row2["amount"].ToString()))
                    {
                        str29 = string.Format(" update cz_bet_six set profit={0},profit_zj={1},is_payment=1,isTie={2},paymentvalidcode='{3}' where bet_id={4} ", new object[] { num5, num6, (num5 == 0.0) ? 1 : 0, str26, row2["bet_id"].ToString() });
                    }
                    goto Label_408A;
                Label_2220:
                    index = 0;
                    goto Label_223C;
                Label_2225:
                    index = 1;
                    goto Label_223C;
                Label_222A:
                    index = 2;
                    goto Label_223C;
                Label_222F:
                    index = 3;
                    goto Label_223C;
                Label_2234:
                    index = 4;
                    goto Label_223C;
                Label_2239:
                    index = 5;
                Label_223C:
                    num5 = this.count_sb(row2["bet_val"].ToString().Trim(), strArray[index], double.Parse(row2["amount"].ToString()), row2["odds"].ToString().Trim());
                    num6 = this.count_sb(row2["bet_val"].ToString().Trim(), strArray[index], double.Parse(row2["amount"].ToString()), row2["odds_zj"].ToString().Trim());
                    if (num5 != -double.Parse(row2["amount"].ToString()))
                    {
                        str29 = string.Format(" update cz_bet_six set profit={0},profit_zj={1},is_payment=1,isTie={2},paymentvalidcode='{3}' where bet_id={4} ", new object[] { num5, num6, (num5 == 0.0) ? 1 : 0, str26, row2["bet_id"].ToString() });
                    }
                    goto Label_408A;
                Label_2460:
                    index = 0;
                    goto Label_247C;
                Label_2465:
                    index = 1;
                    goto Label_247C;
                Label_246A:
                    index = 2;
                    goto Label_247C;
                Label_246F:
                    index = 3;
                    goto Label_247C;
                Label_2474:
                    index = 4;
                    goto Label_247C;
                Label_2479:
                    index = 5;
                Label_247C:
                    num5 = this.count_hsds(row2["bet_val"].ToString().Trim(), strArray[index], double.Parse(row2["amount"].ToString()), row2["odds"].ToString().Trim());
                    num6 = this.count_hsds(row2["bet_val"].ToString().Trim(), strArray[index], double.Parse(row2["amount"].ToString()), row2["odds_zj"].ToString().Trim());
                    if (num5 != -double.Parse(row2["amount"].ToString()))
                    {
                        str29 = string.Format(" update cz_bet_six set profit={0},profit_zj={1},is_payment=1,isTie={2},paymentvalidcode='{3}' where bet_id={4} ", new object[] { num5, num6, (num5 == 0.0) ? 1 : 0, str26, row2["bet_id"].ToString() });
                    }
                    goto Label_408A;
                Label_39E0:
                    str30 = zqh + "," + na;
                    goto Label_3A4A;
                Label_39F2:
                    str30 = str5 + "," + na;
                    goto Label_3A4A;
                Label_3A04:
                    str30 = str6 + "," + na;
                    goto Label_3A4A;
                Label_3A16:
                    str30 = str7 + "," + na;
                    goto Label_3A4A;
                Label_3A28:
                    str30 = str8 + "," + na;
                    goto Label_3A4A;
                Label_3A3A:
                    str30 = str9 + "," + na;
                Label_3A4A:
                    num5 = this.count_longhu(row2["bet_val"].ToString().Trim(), str30, double.Parse(row2["amount"].ToString()), row2["odds"].ToString().Trim());
                    num6 = this.count_longhu(row2["bet_val"].ToString().Trim(), str30, double.Parse(row2["amount"].ToString()), row2["odds_zj"].ToString().Trim());
                    if (num5 != -double.Parse(row2["amount"].ToString()))
                    {
                        str29 = string.Format(" update cz_bet_six set profit={0},profit_zj={1},is_payment=1,isTie={2},paymentvalidcode='{3}' where bet_id={4} ", new object[] { num5, num6, 0, str26, row2["bet_id"].ToString() });
                    }
                Label_408A:
                    if (!string.IsNullOrEmpty(str29))
                    {
                        DbHelperSQL_ADMIN.ExecuteSql(str29, num, null);
                    }
                }
                DbHelperSQL_ADMIN.ExecuteSql(string.Format("update cz_bet_six set profit=-amount,is_payment=1,profit_zj=-amount,paymentvalidcode='{0}' where is_payment=0", ""), num, null);
                string str31 = string.Format("update cz_phase_six set  is_payment=1 where p_id=@p_id", new object[0]);
                parameterArray11 = new SqlParameter[1];
                SqlParameter parameter11 = new SqlParameter("@p_id", SqlDbType.NVarChar) {
                    Value = p_id
                };
                parameterArray11[0] = parameter11;
                SqlParameter[] parameterArray8 = parameterArray11;
                DbHelperSQL_ADMIN.ExecuteSql(str31, num, parameterArray8);
                DbHelperSQL_ADMIN.ExecuteSql("update cz_users set six_usable_credit=six_credit where u_type='hy' and six_isCash =0 ", num, null);
                string str32 = string.Format("select  u_name,su_type, sum(amount) as zt,sum(profit) as jg,sum(CASE WHEN isTie = 1 THEN 0 ELSE amount  * gd_drawback/ 100 END) as  gd_ts,sum(CASE WHEN isTie = 1 THEN 0 ELSE amount  * zd_drawback/ 100 END) as  zd_ts,sum(CASE WHEN isTie = 1 THEN 0 ELSE amount  * dl_drawback/ 100 END) as  dl_ts,sum(CASE WHEN isTie = 1 THEN 0 ELSE amount  * hy_drawback/ 100 END) as  hy_ts  from  (select  aa.*,cz_users.su_type from (select * from cz_bet_six with(NOLOCK)  where phase_id=@phase_id  and sale_type=0 and is_payment=1) aa inner join (select  *  from cz_users where u_type='hy' and  six_isCash=1 )cz_users on aa.u_name=cz_users.u_name  where  sale_type=0) as tab_a  group by u_name,su_type  ", new object[0]);
                parameterArray11 = new SqlParameter[1];
                SqlParameter parameter12 = new SqlParameter("@phase_id", SqlDbType.NVarChar) {
                    Value = p_id
                };
                parameterArray11[0] = parameter12;
                SqlParameter[] parameterArray9 = parameterArray11;
                set = DbHelperSQL.Query(str32, num, parameterArray9);
                if ((set != null) && (set.Tables.Count > 0))
                {
                    table = set.Tables[0];
                }
                foreach (DataRow row3 in table.Rows)
                {
                    double num11 = 0.0;
                    string str33 = "0";
                    str38 = row3["su_type"].ToString().Trim();
                    if (str38 != null)
                    {
                        if (!(str38 == "fgs"))
                        {
                            if (str38 == "gd")
                            {
                                goto Label_4236;
                            }
                            if (str38 == "zd")
                            {
                                goto Label_4250;
                            }
                            if (str38 == "dl")
                            {
                                goto Label_426A;
                            }
                        }
                        else
                        {
                            str33 = row3["gd_ts"].ToString().Trim();
                        }
                    }
                    goto Label_4282;
                Label_4236:
                    str33 = row3["zd_ts"].ToString().Trim();
                    goto Label_4282;
                Label_4250:
                    str33 = row3["dl_ts"].ToString().Trim();
                    goto Label_4282;
                Label_426A:
                    str33 = row3["hy_ts"].ToString().Trim();
                Label_4282:
                    num11 = double.Parse(row3["zt"].ToString().Trim()) + (double.Parse(row3["jg"].ToString().Trim()) + double.Parse(str33));
                    DbHelperSQL_ADMIN.ExecuteSql(string.Format("update cz_users set six_usable_credit=six_usable_credit+{0}  where u_name='{1}'", num11.ToString(), row3["u_name"].ToString().Trim()), num, null);
                    string str34 = string.Format("insert into cz_banlance_log ([u_name],[lottery_type],[phase],[profit],[ip])  values('{0}','{1}',@p_id,{2},'{3}')", new object[] { row3["u_name"].ToString().Trim(), 100, num11.ToString(), LSRequest.GetIP() });
                    parameterArray11 = new SqlParameter[1];
                    SqlParameter parameter4 = new SqlParameter("@p_id", SqlDbType.NVarChar) {
                        Value = p_id
                    };
                    parameterArray11[0] = parameter4;
                    SqlParameter[] parameterArray10 = parameterArray11;
                    DbHelperSQL_ADMIN.ExecuteSql(str34, num, parameterArray10);
                }
                stopwatch.Stop();
                double num12 = stopwatch.ElapsedMilliseconds / 0x3e8L;
                cz_balance_operation_log _log = new cz_balance_operation_log();
                _log.set_lottery_type(100);
                _log.set_lottery_name(LSKeys.get_SIX_Name());
                _log.set_phase_id(new int?(phaseModel.get_p_id()));
                _log.set_phase(phaseModel.get_phase());
                _log.set_code(str11);
                _log.set_u_name(this.uModel.get_u_name());
                if (this.uModel.get_users_child_session() != null)
                {
                    _log.set_child_u_name(this.uModel.get_users_child_session().get_u_name());
                }
                _log.set_ip(LSRequest.GetIP());
                _log.set_consumtime(new double?(num12));
                _log.set_addtime(new DateTime?(DateTime.Now));
                _log.set_isZK(0);
                CallBLL.cz_balance_operation_log_bll.Add(_log);
                string str35 = null;
                if (this.uModel.get_users_child_session() != null)
                {
                    str35 = this.uModel.get_users_child_session().get_u_name();
                }
                string str36 = "";
                string str37 = "";
                cz_lotteryopen_log _log2 = new cz_lotteryopen_log();
                _log2.set_phase_id(phaseModel.get_p_id());
                _log2.set_phase(phaseModel.get_phase());
                _log2.set_u_name(this.uModel.get_u_name());
                _log2.set_children_name(str35);
                _log2.set_action("開獎結算");
                _log2.set_old_val(str36);
                _log2.set_new_val(str37);
                _log2.set_ip(LSRequest.GetIP());
                _log2.set_add_time(DateTime.Now);
                _log2.set_note(string.Format("【本期編號:{0}】開獎結算", _log2.get_phase()));
                _log2.set_type_id(0);
                _log2.set_lottery_id(100);
                CallBLL.cz_lotteryopen_log_bll.Insert(_log2);
                base.Un_Balance_Lock();
                str27 = "<script>hText(); </script>";
                context.Response.Write(str27);
                context.Response.Flush();
                context.Response.Write(string.Format("<script>alert('結算完成！耗時" + num12.ToString() + "秒');window.location=\"/LotteryPeriod/AwardPeriod.aspx?lid={0}\";</script>", 100));
            }
        }

        protected double count_6x(string xzh, string zqh, double m_money, string pl, string pl_list)
        {
            string s = "";
            double num = 0.0;
            if (int.Parse(zqh) == 0x31)
            {
                return num;
            }
            num = -m_money;
            string str2 = base.GetZodiacNum(zqh).Trim();
            if (int.Parse(str2) < 10)
            {
                str2 = "0" + int.Parse(str2).ToString();
            }
            if (xzh.IndexOf(str2) < 0)
            {
                return num;
            }
            s = pl;
            if (pl_list.Trim() != "")
            {
                s = pl_list.Split(new char[] { '|' })[1];
            }
            return (num + (m_money * double.Parse(s)));
        }

        protected double count_bb(string xzh, string zqh, double m_money, string pl)
        {
            double num = 0.0;
            int num2 = int.Parse(zqh);
            if (num2 != 0x31)
            {
                num = -m_money;
                switch (num2)
                {
                    case 1:
                    case 2:
                    case 7:
                    case 8:
                    case 12:
                    case 13:
                    case 0x12:
                    case 0x13:
                    case 0x17:
                    case 0x18:
                    case 0x1d:
                    case 30:
                    case 0x22:
                    case 0x23:
                    case 40:
                    case 0x2d:
                    case 0x2e:
                        if (((num2 % 2) != 1) || (!(xzh.Trim() == "紅波單") && !(xzh.Trim() == "單")))
                        {
                            if (((num2 % 2) != 0) || ((xzh.Trim() != "紅波雙") && (xzh.Trim() != "雙")))
                            {
                                if ((num2 > 0x18) && ((xzh.Trim() == "紅波大") || (xzh.Trim() == "大")))
                                {
                                    return (num + (m_money * double.Parse(pl)));
                                }
                                if ((num2 >= 0x19) || (!(xzh.Trim() == "紅波小") && !(xzh.Trim() == "小")))
                                {
                                    return num;
                                }
                            }
                            return (num + (m_money * double.Parse(pl)));
                        }
                        return (num + (m_money * double.Parse(pl)));

                    case 3:
                    case 4:
                    case 9:
                    case 10:
                    case 14:
                    case 15:
                    case 20:
                    case 0x19:
                    case 0x1a:
                    case 0x1f:
                    case 0x24:
                    case 0x25:
                    case 0x29:
                    case 0x2a:
                    case 0x2f:
                    case 0x30:
                        if (((num2 % 2) != 1) || (!(xzh.Trim() == "藍波單") && !(xzh.Trim() == "單")))
                        {
                            if (((num2 % 2) != 0) || ((xzh.Trim() != "藍波雙") && (xzh.Trim() != "雙")))
                            {
                                if ((num2 > 0x18) && ((xzh.Trim() == "藍波大") || (xzh.Trim() == "大")))
                                {
                                    return (num + (m_money * double.Parse(pl)));
                                }
                                if ((num2 >= 0x19) || (!(xzh.Trim() == "藍波小") && !(xzh.Trim() == "小")))
                                {
                                    return num;
                                }
                            }
                            return (num + (m_money * double.Parse(pl)));
                        }
                        return (num + (m_money * double.Parse(pl)));

                    case 5:
                    case 6:
                    case 11:
                    case 0x10:
                    case 0x11:
                    case 0x15:
                    case 0x16:
                    case 0x1b:
                    case 0x1c:
                    case 0x20:
                    case 0x21:
                    case 0x26:
                    case 0x27:
                    case 0x2b:
                    case 0x2c:
                    case 0x31:
                        if (((num2 % 2) != 1) || (!(xzh.Trim() == "綠波單") && !(xzh.Trim() == "單")))
                        {
                            if (((num2 % 2) != 0) || ((xzh.Trim() != "綠波雙") && (xzh.Trim() != "雙")))
                            {
                                if ((num2 > 0x18) && ((xzh.Trim() == "綠波大") || (xzh.Trim() == "大")))
                                {
                                    return (num + (m_money * double.Parse(pl)));
                                }
                                if ((num2 >= 0x19) || (!(xzh.Trim() == "綠波小") && !(xzh.Trim() == "小")))
                                {
                                    return num;
                                }
                            }
                            return (num + (m_money * double.Parse(pl)));
                        }
                        return (num + (m_money * double.Parse(pl)));
                }
            }
            return num;
        }

        protected double count_ds(string xzh, string zqh, double m_money, string pl)
        {
            if (int.Parse(zqh) == 0x31)
            {
                return 0.0;
            }
            if ((int.Parse(zqh) % 2) == 0)
            {
                if (xzh.Trim() == "雙")
                {
                    return ((m_money * double.Parse(pl)) - m_money);
                }
                return -m_money;
            }
            if (xzh.Trim() == "單")
            {
                return ((m_money * double.Parse(pl)) - m_money);
            }
            return -m_money;
        }

        protected double count_dx(string xzh, string zqh, double m_money, string pl)
        {
            double num = 0.0;
            if (int.Parse(zqh) == 0x31)
            {
                return 0.0;
            }
            if (int.Parse(zqh) < 0x19)
            {
                if (xzh.Trim() == "大")
                {
                    return -m_money;
                }
                return ((m_money * double.Parse(pl)) - m_money);
            }
            if (int.Parse(zqh) <= 0x18)
            {
                return num;
            }
            if (xzh.Trim() == "大")
            {
                return ((m_money * double.Parse(pl)) - m_money);
            }
            return -m_money;
        }

        protected double count_hsds(string xzh, string zqh, double m_money, string pl)
        {
            int num2 = int.Parse(zqh) / 10;
            int num3 = int.Parse(zqh) % 10;
            int num4 = num2 + num3;
            if (int.Parse(zqh) == 0x31)
            {
                return 0.0;
            }
            if ((num4 % 2) == 0)
            {
                if ((xzh.Trim() == "雙") || (xzh.Trim() == "合雙"))
                {
                    return ((m_money * double.Parse(pl)) - m_money);
                }
                return -m_money;
            }
            if ((xzh.Trim() == "單") || (xzh.Trim() == "合單"))
            {
                return ((m_money * double.Parse(pl)) - m_money);
            }
            return -m_money;
        }

        protected double count_longhu(string xzh, string zqh, double m_money, string pl)
        {
            double num = 0.0;
            string[] strArray = zqh.Split(new char[] { ',' });
            int num2 = int.Parse(strArray[0]);
            int num3 = int.Parse(strArray[1]);
            if (num2 > num3)
            {
                if ((((xzh.Trim() == "龍") || (xzh.Trim() == "天")) || ((xzh.Trim() == "莊") || (xzh.Trim() == "雷"))) || ((xzh.Trim() == "神") || (xzh.Trim() == "黑")))
                {
                    return ((m_money * double.Parse(pl)) - m_money);
                }
                return -m_money;
            }
            if (num2 >= num3)
            {
                return num;
            }
            if ((((xzh.Trim() == "虎") || (xzh.Trim() == "地")) || ((xzh.Trim() == "閑") || (xzh.Trim() == "電"))) || ((xzh.Trim() == "奇") || (xzh.Trim() == "紅")))
            {
                return ((m_money * double.Parse(pl)) - m_money);
            }
            return -m_money;
        }

        protected double count_qmds(string xzh, string nm, double m_money, string pl)
        {
            double num = 0.0;
            num = -m_money;
            int num2 = 0;
            int num3 = 0;
            string[] strArray = nm.Split(new char[] { ',' });
            for (int i = 0; i < strArray.Length; i++)
            {
                if ((int.Parse(strArray[i]) % 2) != 0)
                {
                    num2++;
                }
                else
                {
                    num3++;
                }
            }
            string str = string.Format("單{0}、雙{1}", num2, num3);
            if (xzh.Equals(str))
            {
                num += m_money * double.Parse(pl);
            }
            return num;
        }

        protected double count_qmdx(string xzh, string nm, double m_money, string pl)
        {
            double num = 0.0;
            num = -m_money;
            int num2 = 0;
            int num3 = 0;
            string[] strArray = nm.Split(new char[] { ',' });
            for (int i = 0; i < strArray.Length; i++)
            {
                if (int.Parse(strArray[i]) >= 0x19)
                {
                    num2++;
                }
                else
                {
                    num3++;
                }
            }
            string str = string.Format("大{0}、小{1}", num2, num3);
            if (xzh.Equals(str))
            {
                num += m_money * double.Parse(pl);
            }
            return num;
        }

        protected double count_rqz(string xzh, string zqh, double m_money, string pl, string pl_list)
        {
            string[] strArray = xzh.Split(new char[] { '~' });
            string[] strArray2 = pl_list.Split(new char[] { '~' });
            string[] first = zqh.Split(new char[] { ',' });
            int length = strArray2.Length;
            double num2 = 0.0;
            num2 = -m_money;
            int num3 = strArray.Length;
            double num4 = m_money / ((double) num3);
            int index = 0;
            string s = "";
            foreach (string str2 in strArray)
            {
                string[] second = str2.Split(new char[] { ',' });
                if (first.Intersect<string>(second).Count<string>() == 2)
                {
                    s = pl;
                    if ((length > index) && (strArray2[index].ToString().Trim() != ""))
                    {
                        s = strArray2[index].Split(new char[] { '|' })[1];
                    }
                    num2 += num4 * double.Parse(s);
                }
                index++;
            }
            return num2;
        }

        protected double count_rzt(string xzh, string zqh_zm, string zqh_tm, double m_money, string pl, string pl_list)
        {
            string[] strArray = xzh.Split(new char[] { '~' });
            string[] strArray2 = pl_list.Split(new char[] { '~' });
            int length = strArray2.Length;
            int num2 = 0;
            int num3 = 0;
            string[] strArray3 = pl.Split(new char[] { ',' });
            int num4 = strArray.Length;
            double num5 = m_money / ((double) num4);
            double num6 = 0.0;
            num6 = -m_money;
            int index = 0;
            string s = "";
            foreach (string str2 in strArray)
            {
                num3 = 0;
                if (str2.IndexOf(zqh_tm.Trim()) > -1)
                {
                    num3 = 1;
                }
                string[] strArray4 = str2.Split(new char[] { ',' });
                num2 = 0;
                foreach (string str3 in strArray4)
                {
                    if (zqh_zm.IndexOf(str3) > -1)
                    {
                        num2++;
                    }
                }
                if ((num2 == 1) && (num3 == 1))
                {
                    s = strArray3[0].ToString();
                    if ((length > index) && (strArray2[index].ToString().Trim() != ""))
                    {
                        s = strArray2[index].Split(new char[] { '|' })[1].Split(new char[] { ',' })[0].ToString().Trim();
                    }
                    num6 += num5 * double.Parse(s);
                }
                else if (num2 == 2)
                {
                    s = strArray3[1].ToString();
                    if ((length > index) && (strArray2[index].ToString().Trim() != ""))
                    {
                        s = strArray2[index].Split(new char[] { '|' })[1].Split(new char[] { ',' })[1].ToString().Trim();
                    }
                    num6 += num5 * double.Parse(s);
                }
                index++;
            }
            return num6;
        }

        protected double count_sb(string xzh, string zqh, double m_money, string pl)
        {
            double num = 0.0;
            num = -m_money;
            int num2 = int.Parse(zqh);
            xzh = xzh.Replace("波", "");
            switch (num2)
            {
                case 1:
                case 2:
                case 7:
                case 8:
                case 12:
                case 13:
                case 0x12:
                case 0x13:
                case 0x17:
                case 0x18:
                case 0x1d:
                case 30:
                case 0x22:
                case 0x23:
                case 40:
                case 0x2d:
                case 0x2e:
                    if (xzh.Trim() == "紅")
                    {
                        num += m_money * double.Parse(pl);
                    }
                    return num;

                case 3:
                case 4:
                case 9:
                case 10:
                case 14:
                case 15:
                case 20:
                case 0x19:
                case 0x1a:
                case 0x1f:
                case 0x24:
                case 0x25:
                case 0x29:
                case 0x2a:
                case 0x2f:
                case 0x30:
                    if (xzh.Trim() == "藍")
                    {
                        num += m_money * double.Parse(pl);
                    }
                    return num;

                case 5:
                case 6:
                case 11:
                case 0x10:
                case 0x11:
                case 0x15:
                case 0x16:
                case 0x1b:
                case 0x1c:
                case 0x20:
                case 0x21:
                case 0x26:
                case 0x27:
                case 0x2b:
                case 0x2c:
                case 0x31:
                    if (xzh.Trim() == "綠")
                    {
                        num += m_money * double.Parse(pl);
                    }
                    return num;
            }
            return num;
        }

        protected double count_sqr(string xzh, string zqh, double m_money, string pl, string pl_list)
        {
            string[] strArray = xzh.Split(new char[] { '~' });
            string[] strArray2 = pl_list.Split(new char[] { '~' });
            string[] first = zqh.Split(new char[] { ',' });
            int length = strArray2.Length;
            double num2 = 0.0;
            num2 = -m_money;
            int num3 = strArray.Length;
            double num4 = m_money / ((double) num3);
            string[] strArray4 = pl.Split(new char[] { ',' });
            string s = "";
            int index = 0;
            foreach (string str2 in strArray)
            {
                string[] second = str2.Split(new char[] { ',' });
                IEnumerable<string> source = first.Intersect<string>(second);
                if (source.Count<string>() == 3)
                {
                    s = strArray4[1].ToString();
                    if ((length > index) && (strArray2[index].ToString().Trim() != ""))
                    {
                        s = strArray2[index].Split(new char[] { '|' })[1].Split(new char[] { ',' })[1].ToString().Trim();
                    }
                    num2 += num4 * double.Parse(s);
                }
                else if (source.Count<string>() == 2)
                {
                    s = strArray4[0].ToString();
                    if (strArray2[index].ToString().Trim() != "")
                    {
                        s = strArray2[index].Split(new char[] { '|' })[1].Split(new char[] { ',' })[0].ToString().Trim();
                    }
                    num2 += num4 * double.Parse(s);
                }
                index++;
            }
            return num2;
        }

        protected double count_sqz(string xzh, string zqh, double m_money, string pl, string pl_list)
        {
            string[] strArray = xzh.Split(new char[] { '~' });
            string[] strArray2 = pl_list.Split(new char[] { '~' });
            string[] first = zqh.Split(new char[] { ',' });
            int length = strArray2.Length;
            double num2 = 0.0;
            num2 = -m_money;
            int num3 = strArray.Length;
            double num4 = m_money / ((double) num3);
            string s = "";
            int index = 0;
            foreach (string str2 in strArray)
            {
                string[] second = str2.Split(new char[] { ',' });
                if (first.Intersect<string>(second).Count<string>() == 3)
                {
                    s = pl;
                    if ((length > index) && (strArray2[index].ToString().Trim() != ""))
                    {
                        s = strArray2[index].Split(new char[] { '|' })[1].ToString();
                    }
                    num2 += num4 * double.Parse(s);
                }
                index++;
            }
            return num2;
        }

        protected double count_sx(string xzh, string zqh, double m_money, string pl)
        {
            double num = 0.0;
            num = -m_money;
            foreach (string str in zqh.Split(new char[] { ',' }))
            {
                if (base.GetZodiacName(str.Trim()).Trim() == xzh.Trim())
                {
                    return (num + (m_money * double.Parse(pl)));
                }
            }
            return num;
        }

        protected double count_sxbz(string xzh, string zq_sx_str, double m_money, string pl)
        {
            double num = 0.0;
            num = -m_money;
            if (zq_sx_str.IndexOf(xzh) <= -1)
            {
                num += m_money * double.Parse(pl);
            }
            return num;
        }

        protected double count_sxl(string xzh, string zqh1, double m_money, string pl, string pl_list)
        {
            string[] strArray = xzh.Split(new char[] { '~' });
            string[] strArray2 = pl_list.Split(new char[] { '~' });
            int length = strArray2.Length;
            string[] strArray3 = pl.Split(new char[] { ',' });
            int num2 = 0;
            int num3 = strArray.Length;
            double num4 = m_money / ((double) num3);
            double num5 = 0.0;
            num5 = -m_money;
            int num6 = 0;
            int index = 0;
            string s = "";
            foreach (string str2 in strArray)
            {
                string[] strArray4 = str2.Split(new char[] { ',' });
                num2 = 0;
                num6 = 0;
                foreach (string str3 in strArray4)
                {
                    if (zqh1.IndexOf(str3) < 0)
                    {
                        num2 = -1;
                        break;
                    }
                    if (str3.Trim() == base.get_YearLianID())
                    {
                        num6 = 1;
                    }
                }
                if (num2 == 0)
                {
                    if (num6 < 1)
                    {
                        s = strArray3[0].ToString();
                        if ((length > index) && (strArray2[index].ToString().Trim() != ""))
                        {
                            s = strArray2[index].Split(new char[] { '|' })[1].ToString();
                        }
                        num5 += num4 * double.Parse(s);
                    }
                    else
                    {
                        s = strArray3[1].ToString();
                        if ((length > index) && (strArray2[index].ToString().Trim() != ""))
                        {
                            s = strArray2[index].Split(new char[] { '|' })[1].ToString();
                        }
                        num5 += num4 * double.Parse(s);
                    }
                }
                index++;
            }
            return num5;
        }

        protected double count_szy(string xzh, string zqh, double m_money, string pl, string pl_list)
        {
            string[] strArray = xzh.Split(new char[] { '~' });
            string[] strArray2 = pl_list.Split(new char[] { '~' });
            string[] first = zqh.Split(new char[] { ',' });
            int length = strArray2.Length;
            int num2 = strArray.Length;
            double num3 = m_money / ((double) num2);
            double num4 = 0.0;
            num4 = -m_money;
            int index = 0;
            string s = "";
            foreach (string str2 in strArray)
            {
                string[] second = str2.Split(new char[] { ',' });
                if (first.Intersect<string>(second).Count<string>() > 0)
                {
                    s = pl;
                    if ((length > index) && (strArray2[index].ToString().Trim() != ""))
                    {
                        s = strArray2[index].Split(new char[] { '|' })[1];
                    }
                    num4 += num3 * double.Parse(s);
                }
                index++;
            }
            return num4;
        }

        protected double count_tc(string xzh, string zqh_zm, string zqh_tm, double m_money, string pl, string pl_list)
        {
            string[] strArray = xzh.Split(new char[] { '~' });
            string[] strArray2 = pl_list.Split(new char[] { '~' });
            int length = strArray2.Length;
            int num2 = strArray.Length;
            double num3 = m_money / ((double) num2);
            double num4 = 0.0;
            num4 = -m_money;
            int num5 = 0;
            int num6 = 0;
            int index = 0;
            string s = "";
            foreach (string str2 in strArray)
            {
                num6 = 0;
                if (str2.IndexOf(zqh_tm.Trim()) > -1)
                {
                    num6 = 1;
                }
                string[] strArray3 = str2.Split(new char[] { ',' });
                num5 = 0;
                foreach (string str3 in strArray3)
                {
                    if (zqh_zm.IndexOf(str3) > -1)
                    {
                        num5++;
                    }
                }
                if ((num5 == 1) && (num6 == 1))
                {
                    s = pl;
                    if ((length > index) && (strArray2[index].ToString().Trim() != ""))
                    {
                        s = strArray2[index].Split(new char[] { '|' })[1];
                    }
                    num4 += num3 * double.Parse(s);
                }
                index++;
            }
            return num4;
        }

        protected double count_tm(string xzh, string zqh, double m_money, string pl)
        {
            if (xzh.Trim() == zqh)
            {
                return ((m_money * double.Parse(pl)) - m_money);
            }
            return -m_money;
        }

        protected double count_tmsx(string xzh, string zqh, double m_money, string pl)
        {
            double num = 0.0;
            num = -m_money;
            if (base.GetZodiacName(zqh).Trim() == xzh.Trim())
            {
                num += m_money * double.Parse(pl);
            }
            return num;
        }

        protected double count_tmtz(string xzh, string zqh, double m_money, string pl)
        {
            if (int.Parse(zqh) == 0x31)
            {
                return 0.0;
            }
            if (base.get_tz(zqh).Trim() == xzh.Trim())
            {
                return ((m_money * double.Parse(pl)) - m_money);
            }
            return -m_money;
        }

        protected double count_wbz(string xzh, string zqh, double m_money, string pl, string pl_list)
        {
            string[] strArray = xzh.Split(new char[] { '~' });
            string[] strArray2 = pl_list.Split(new char[] { '~' });
            string[] first = zqh.Split(new char[] { ',' });
            int length = strArray2.Length;
            int num2 = strArray.Length;
            double num3 = m_money / ((double) num2);
            double num4 = 0.0;
            num4 = -m_money;
            int index = 0;
            string s = "";
            foreach (string str2 in strArray)
            {
                string[] second = str2.Split(new char[] { ',' });
                if (first.Intersect<string>(second).Count<string>() == 0)
                {
                    s = pl;
                    if ((length > index) && (strArray2[index].ToString().Trim() != ""))
                    {
                        s = strArray2[index].Split(new char[] { '|' })[1];
                    }
                    num4 += num3 * double.Parse(s);
                }
                index++;
            }
            return num4;
        }

        protected double count_ws(string xzh, string zqh, double m_money, string pl)
        {
            double num = 0.0;
            num = -m_money;
            string[] strArray = zqh.Split(new char[] { ',' });
            int num2 = int.Parse(xzh);
            foreach (string str in strArray)
            {
                if (num2 == (int.Parse(str) % 10))
                {
                    return (num + (m_money * double.Parse(pl)));
                }
            }
            return num;
        }

        protected double count_wsbz(string xzh, string zq_ws, double m_money, string pl)
        {
            double num = 0.0;
            num = -m_money;
            string str = "";
            int num2 = int.Parse(xzh);
            if (num2 == 0)
            {
                str = "10";
            }
            else
            {
                str = "0" + num2.ToString();
            }
            if (zq_ws.IndexOf(str) <= -1)
            {
                num += m_money * double.Parse(pl);
            }
            return num;
        }

        protected double count_wsdx(string xzh, string zqh, double m_money, string pl)
        {
            int num2 = int.Parse(zqh) % 10;
            if (int.Parse(zqh) == 0x31)
            {
                return 0.0;
            }
            if (num2 < 5)
            {
                if (xzh.Trim() == "小")
                {
                    return ((m_money * double.Parse(pl)) - m_money);
                }
                return -m_money;
            }
            if (xzh.Trim() == "大")
            {
                return ((m_money * double.Parse(pl)) - m_money);
            }
            return -m_money;
        }

        protected double count_wsl(string xzh, string zqh, double m_money, string pl, string pl_list)
        {
            string[] strArray = xzh.Split(new char[] { '~' });
            string[] strArray2 = pl_list.Split(new char[] { '~' });
            int length = strArray2.Length;
            string[] strArray3 = pl.Split(new char[] { ',' });
            int num2 = 0;
            int num3 = strArray.Length;
            double num4 = m_money / ((double) num3);
            double num5 = 0.0;
            num5 = -m_money;
            int num6 = 0;
            int index = 0;
            string s = "";
            foreach (string str2 in strArray)
            {
                string[] strArray4 = str2.Split(new char[] { ',' });
                num2 = 0;
                num6 = 0;
                foreach (string str3 in strArray4)
                {
                    if (zqh.IndexOf(str3.Trim()) < 0)
                    {
                        num2 = -1;
                        break;
                    }
                    if (str3.Trim() == "10")
                    {
                        num6 = 1;
                    }
                }
                if (num2 == 0)
                {
                    if (num6 == 1)
                    {
                        s = strArray3[1].ToString();
                        if ((length > index) && (strArray2[index].ToString().Trim() != ""))
                        {
                            s = strArray2[index].Split(new char[] { '|' })[1].ToString();
                        }
                        num5 += num4 * double.Parse(s);
                    }
                    else
                    {
                        s = strArray3[0].ToString();
                        if ((length > index) && (strArray2[index].ToString().Trim() != ""))
                        {
                            s = strArray2[index].Split(new char[] { '|' })[1].ToString();
                        }
                        num5 += num4 * double.Parse(s);
                    }
                }
                index++;
            }
            return num5;
        }

        protected double count_wx(string xzh, string sn, double m_money, string pl)
        {
            string str = this.get_count_wx(sn);
            double num = 0.0;
            num = -m_money;
            if (xzh.Trim().Equals(str))
            {
                num += m_money * double.Parse(pl);
            }
            return num;
        }

        protected double count_zhds(string xzh, string zqh, double m_money, string pl)
        {
            double num = 0.0;
            num = -m_money;
            string[] strArray = zqh.Split(new char[] { ',' });
            int num2 = 0;
            foreach (string str in strArray)
            {
                num2 += int.Parse(str);
            }
            if ((num2 % 2) == 0)
            {
                if (xzh.Trim() == "雙")
                {
                    num += m_money * double.Parse(pl);
                }
                return num;
            }
            if (xzh.Trim() == "單")
            {
                num += m_money * double.Parse(pl);
            }
            return num;
        }

        protected double count_zhdx(string xzh, string zqh, double m_money, string pl)
        {
            string[] strArray = zqh.Split(new char[] { ',' });
            int num2 = 0;
            foreach (string str in strArray)
            {
                num2 += int.Parse(str);
            }
            if (num2 < 0xaf)
            {
                if (xzh.Trim() == "小")
                {
                    return ((m_money * double.Parse(pl)) - m_money);
                }
                return -m_money;
            }
            if (xzh.Trim() == "大")
            {
                return ((m_money * double.Parse(pl)) - m_money);
            }
            return -m_money;
        }

        protected double count_zm(string xzh, string zqh, double m_money, string pl)
        {
            string[] strArray = zqh.Split(new char[] { ',' });
            double num = 0.0;
            num = -m_money;
            foreach (string str in strArray)
            {
                if (xzh.Trim() == str.Trim())
                {
                    return ((m_money * double.Parse(pl)) - m_money);
                }
            }
            return num;
        }

        protected double count_zmgg(string xzh, string zqh, double m_money, string pl)
        {
            string[] strArray = xzh.Split(new char[] { ',' });
            string[] strArray2 = pl.Split(new char[] { ',' });
            string[] strArray3 = zqh.Split(new char[] { ',' });
            int index = 0;
            string str = "";
            string str2 = "";
            double num2 = m_money;
            double num3 = 0.0;
            int num4 = 0;
            foreach (string str3 in strArray)
            {
                str = "select * from cz_odds_six where odds_id=@odds_id ";
                SqlParameter[] parameterArray2 = new SqlParameter[1];
                SqlParameter parameter = new SqlParameter("@odds_id", SqlDbType.NVarChar) {
                    Value = str3.Trim()
                };
                parameterArray2[0] = parameter;
                SqlParameter[] parameterArray = parameterArray2;
                DataTable table = DbHelperSQL.Query(str, parameterArray).Tables[0];
                str2 = table.Rows[0]["put_amount"].ToString().Trim();
                switch (str2)
                {
                    case "大":
                    case "小":
                        num4 = int.Parse(table.Rows[0]["play_name"].ToString().Trim().Replace("正碼", "")) - 1;
                        num3 = this.count_dx(str2, strArray3[num4], num2, strArray2[index]);
                        break;
                }
                if ((str2 == "單") || (str2 == "雙"))
                {
                    num4 = int.Parse(table.Rows[0]["play_name"].ToString().Trim().Replace("正碼", "")) - 1;
                    num3 = this.count_ds(str2, strArray3[num4], num2, strArray2[index]);
                }
                if (((str2 == "藍") || (str2 == "綠")) || (str2 == "紅"))
                {
                    num4 = int.Parse(table.Rows[0]["play_name"].ToString().Trim().Replace("正碼", "")) - 1;
                    num3 = this.count_sb(str2, strArray3[num4], num2, strArray2[index]);
                }
                if (num3 < 0.0)
                {
                    num2 = 0.0;
                    break;
                }
                num2 = num3 + num2;
                index++;
            }
            return (num2 - m_money);
        }

        protected string get_count_wx(string sn)
        {
            Dictionary<string, string> dictionary = base.PlayWXList_six();
            if (int.Parse(sn) < 10)
            {
                sn = "0" + int.Parse(sn);
            }
            foreach (KeyValuePair<string, string> pair in dictionary)
            {
                if (pair.Value.IndexOf(sn) > -1)
                {
                    return pair.Key;
                }
            }
            return "";
        }

        public override void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/html";
            string str = context.Session["user_name"].ToString();
            this.uModel = context.Session[str + "lottery_session_user_info"] as agent_userinfo_session;
            if (!this.uModel.get_u_type().Equals("zj"))
            {
                context.Response.End();
            }
            else if ((this.uModel.get_users_child_session() != null) && (this.uModel.get_users_child_session().get_permissions_name().IndexOf("po_3_1") < 0))
            {
                context.Response.End();
            }
            if (base.IsChildSync())
            {
                context.Response.End();
            }
            string str2 = LSRequest.qq("pid");
            if (string.IsNullOrEmpty(str2))
            {
                context.Response.End();
            }
            else if (!Utils.IsPureNumeric(str2))
            {
                context.Response.End();
            }
            else
            {
                if (base.En_Balance_Lock(true).Equals("300"))
                {
                    context.Response.Redirect("/MessagePage.aspx?code=u100082&url=&issuccess=1&isback=0&isopen=0");
                    context.Response.End();
                }
                this.Balance(context, str2);
            }
        }

        private bool ValidParam(string num, string na, ref string message)
        {
            List<string> list = new List<string>();
            string[] strArray = num.Split(new char[] { ',' });
            for (int i = 0; i < strArray.Length; i++)
            {
                list.Add(strArray[i]);
            }
            list.Add(na);
            for (int j = 0; j < list.Count; j++)
            {
                if (list[j].Equals("00") || string.IsNullOrEmpty(list[j]))
                {
                    message = "碼沒有填完或沒有送出結果！！";
                    return false;
                }
                if (!base.IsNumber(list[j]))
                {
                    message = "開獎號碼格式錯誤！！";
                    return false;
                }
                if ((list[j].Length == 1) || (list[j].Length > 2))
                {
                    message = "開獎號碼格式錯誤！！";
                    return false;
                }
                if (int.Parse(list[j]) > 0x31)
                {
                    message = "開獎號碼格式錯誤！！";
                    return false;
                }
            }
            for (int k = 0; k < list.Count; k++)
            {
                string str = list[k];
                int num5 = 0;
                for (int m = 0; m < list.Count; m++)
                {
                    if (list[m].Equals(str))
                    {
                        num5++;
                    }
                }
                if (num5 > 1)
                {
                    message = "開獎號碼重複錯誤!";
                    return false;
                }
            }
            return true;
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

