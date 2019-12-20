namespace Agent.Web
{
    using Agent.Web.WebBase;
    using LotterySystem.Common;
    using LotterySystem.Model;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Text.RegularExpressions;

    public class OddsSet_kc : MemberPageBase
    {
        protected DataTable dataTable;
        protected string downbase_str = " readonly=\"readonly\" style=\"background:#EBE9E9\" ";
        protected DataTable kl10DT;
        protected DataTable lotteryDT;
        protected string lotteryId = "";
        protected string max_odds_isopen = "0";
        protected string max_odds_isopen_str = " readonly=\"readonly\" style=\"background:#EBE9E9\" ";
        protected double playTypeBanker;
        protected double playTypePlayer;
        protected string skin = "";

        private void InitData()
        {
            switch (Convert.ToInt32(this.lotteryId))
            {
                case 0:
                {
                    DataSet playByOddsSet = CallBLL.cz_play_kl10_bll.GetPlayByOddsSet("86,87,88,89,90,91,92,93,94,95,96,97,98,99,100,101,102,103,104,105,106,107,108,109,110,111,112,113,114,115,116,117,118,119,120,123,124,125,126,127,128,129,130,131,132,133,134,135,136");
                    if (((playByOddsSet == null) || (playByOddsSet.Tables.Count <= 0)) || (playByOddsSet.Tables[0].Rows.Count <= 0))
                    {
                        break;
                    }
                    this.dataTable = playByOddsSet.Tables[0];
                    return;
                }
                case 1:
                {
                    DataSet set2 = CallBLL.cz_play_cqsc_bll.GetPlayByOddsSet("1,2,3,16,17,18,19");
                    if (((set2 == null) || (set2.Tables.Count <= 0)) || (set2.Tables[0].Rows.Count <= 0))
                    {
                        break;
                    }
                    this.dataTable = set2.Tables[0];
                    return;
                }
                case 2:
                {
                    DataSet set3 = CallBLL.cz_play_pk10_bll.GetPlayByOddsSet("1,2,3,4,36,37,38");
                    if (((set3 == null) || (set3.Tables.Count <= 0)) || (set3.Tables[0].Rows.Count <= 0))
                    {
                        break;
                    }
                    this.dataTable = set3.Tables[0];
                    return;
                }
                case 3:
                {
                    DataSet set4 = CallBLL.cz_play_xync_bll.GetPlayByOddsSet("86,87,88,89,90,91,92,93,94,95,96,97,98,99,100,101,102,103,104,105,106,107,108,109,110,111,112,113,114,115,116,117,118,119,120,123,124,125,126,127,128,129,130,131,132,133,134,135,136");
                    if (((set4 == null) || (set4.Tables.Count <= 0)) || (set4.Tables[0].Rows.Count <= 0))
                    {
                        break;
                    }
                    this.dataTable = set4.Tables[0];
                    return;
                }
                case 4:
                {
                    DataSet set5 = CallBLL.cz_play_jsk3_bll.GetPlayByOddsSet("");
                    if (((set5 == null) || (set5.Tables.Count <= 0)) || (set5.Tables[0].Rows.Count <= 0))
                    {
                        break;
                    }
                    this.dataTable = set5.Tables[0];
                    return;
                }
                case 5:
                {
                    DataSet set6 = CallBLL.cz_play_kl8_bll.GetPlayByOddsSet("");
                    if (((set6 == null) || (set6.Tables.Count <= 0)) || (set6.Tables[0].Rows.Count <= 0))
                    {
                        break;
                    }
                    this.dataTable = set6.Tables[0];
                    return;
                }
                case 6:
                {
                    DataSet set7 = CallBLL.cz_play_k8sc_bll.GetPlayByOddsSet("1,2,3,16,17,18,19");
                    if (((set7 == null) || (set7.Tables.Count <= 0)) || (set7.Tables[0].Rows.Count <= 0))
                    {
                        break;
                    }
                    this.dataTable = set7.Tables[0];
                    return;
                }
                case 7:
                {
                    DataSet set8 = CallBLL.cz_play_pcdd_bll.GetPlayByOddsSet("71001,71002,71003,71004,71005,71006,71007,71008,71013,71014");
                    if (((set8 == null) || (set8.Tables.Count <= 0)) || (set8.Tables[0].Rows.Count <= 0))
                    {
                        break;
                    }
                    this.dataTable = set8.Tables[0];
                    return;
                }
                case 8:
                {
                    DataSet set10 = CallBLL.cz_play_pkbjl_bll.GetPlayByOddsSet("81001,81002,81003,81004");
                    if (((set10 != null) && (set10.Tables.Count > 0)) && (set10.Tables[0].Rows.Count > 0))
                    {
                        this.dataTable = set10.Tables[0];
                    }
                    if (((set10 == null) || (set10.Tables.Count <= 0)) || (set10.Tables[1].Rows.Count <= 0))
                    {
                        break;
                    }
                    DataTable table1 = set10.Tables[1];
                    DataRow[] rowArray = set10.Tables[1].Select(" odds_id=82005 ");
                    DataRow[] rowArray2 = set10.Tables[1].Select(" odds_id=82006 ");
                    if (rowArray.Length > 0)
                    {
                        this.playTypeBanker = Convert.ToDouble(rowArray[0]["wt_value"].ToString());
                    }
                    if (rowArray2.Length <= 0)
                    {
                        break;
                    }
                    this.playTypePlayer = Convert.ToDouble(rowArray2[0]["wt_value"].ToString());
                    return;
                }
                case 9:
                {
                    DataSet set9 = CallBLL.cz_play_xyft5_bll.GetPlayByOddsSet("1,2,3,4,36,37,38");
                    if (((set9 == null) || (set9.Tables.Count <= 0)) || (set9.Tables[0].Rows.Count <= 0))
                    {
                        break;
                    }
                    this.dataTable = set9.Tables[0];
                    return;
                }
                case 10:
                {
                    DataSet set11 = CallBLL.cz_play_jscar_bll.GetPlayByOddsSet("1,2,3,4,36,37,38");
                    if (((set11 == null) || (set11.Tables.Count <= 0)) || (set11.Tables[0].Rows.Count <= 0))
                    {
                        break;
                    }
                    this.dataTable = set11.Tables[0];
                    return;
                }
                case 11:
                {
                    DataSet set12 = CallBLL.cz_play_speed5_bll.GetPlayByOddsSet("1,2,3,16,17,18,19");
                    if (((set12 == null) || (set12.Tables.Count <= 0)) || (set12.Tables[0].Rows.Count <= 0))
                    {
                        break;
                    }
                    this.dataTable = set12.Tables[0];
                    return;
                }
                case 12:
                {
                    DataSet set14 = CallBLL.cz_play_jspk10_bll.GetPlayByOddsSet("1,2,3,4,36,37,38");
                    if (((set14 == null) || (set14.Tables.Count <= 0)) || (set14.Tables[0].Rows.Count <= 0))
                    {
                        break;
                    }
                    this.dataTable = set14.Tables[0];
                    return;
                }
                case 13:
                {
                    DataSet set13 = CallBLL.cz_play_jscqsc_bll.GetPlayByOddsSet("1,2,3,16,17,18,19");
                    if (((set13 == null) || (set13.Tables.Count <= 0)) || (set13.Tables[0].Rows.Count <= 0))
                    {
                        break;
                    }
                    this.dataTable = set13.Tables[0];
                    return;
                }
                case 14:
                {
                    DataSet set15 = CallBLL.cz_play_jssfc_bll.GetPlayByOddsSet("86,87,88,89,90,91,92,93,94,95,96,97,98,99,100,101,102,103,104,105,106,107,108,109,110,111,112,113,114,115,116,117,118,119,120,123,124,125,126,127,128,129,130,131,132,133,134,135,136");
                    if (((set15 == null) || (set15.Tables.Count <= 0)) || (set15.Tables[0].Rows.Count <= 0))
                    {
                        break;
                    }
                    this.dataTable = set15.Tables[0];
                    return;
                }
                case 15:
                {
                    DataSet set16 = CallBLL.cz_play_jsft2_bll.GetPlayByOddsSet("1,2,3,4,36,37,38");
                    if (((set16 == null) || (set16.Tables.Count <= 0)) || (set16.Tables[0].Rows.Count <= 0))
                    {
                        break;
                    }
                    this.dataTable = set16.Tables[0];
                    return;
                }
                case 0x10:
                {
                    DataSet set17 = CallBLL.cz_play_car168_bll.GetPlayByOddsSet("1,2,3,4,36,37,38");
                    if (((set17 == null) || (set17.Tables.Count <= 0)) || (set17.Tables[0].Rows.Count <= 0))
                    {
                        break;
                    }
                    this.dataTable = set17.Tables[0];
                    return;
                }
                case 0x11:
                {
                    DataSet set18 = CallBLL.cz_play_ssc168_bll.GetPlayByOddsSet("1,2,3,16,17,18,19");
                    if (((set18 == null) || (set18.Tables.Count <= 0)) || (set18.Tables[0].Rows.Count <= 0))
                    {
                        break;
                    }
                    this.dataTable = set18.Tables[0];
                    return;
                }
                case 0x12:
                {
                    DataSet set19 = CallBLL.cz_play_vrcar_bll.GetPlayByOddsSet("1,2,3,4,36,37,38");
                    if (((set19 == null) || (set19.Tables.Count <= 0)) || (set19.Tables[0].Rows.Count <= 0))
                    {
                        break;
                    }
                    this.dataTable = set19.Tables[0];
                    return;
                }
                case 0x13:
                {
                    DataSet set20 = CallBLL.cz_play_vrssc_bll.GetPlayByOddsSet("1,2,3,16,17,18,19");
                    if (((set20 == null) || (set20.Tables.Count <= 0)) || (set20.Tables[0].Rows.Count <= 0))
                    {
                        break;
                    }
                    this.dataTable = set20.Tables[0];
                    return;
                }
                case 20:
                {
                    DataSet set21 = CallBLL.cz_play_xyftoa_bll.GetPlayByOddsSet("1,2,3,4,36,37,38");
                    if (((set21 == null) || (set21.Tables.Count <= 0)) || (set21.Tables[0].Rows.Count <= 0))
                    {
                        break;
                    }
                    this.dataTable = set21.Tables[0];
                    return;
                }
                case 0x15:
                {
                    DataSet set22 = CallBLL.cz_play_xyftsg_bll.GetPlayByOddsSet("1,2,3,4,36,37,38");
                    if (((set22 == null) || (set22.Tables.Count <= 0)) || (set22.Tables[0].Rows.Count <= 0))
                    {
                        break;
                    }
                    this.dataTable = set22.Tables[0];
                    return;
                }
                case 0x16:
                {
                    DataSet set23 = CallBLL.cz_play_happycar_bll.GetPlayByOddsSet("1,2,3,4,36,37,38");
                    if (((set23 != null) && (set23.Tables.Count > 0)) && (set23.Tables[0].Rows.Count > 0))
                    {
                        this.dataTable = set23.Tables[0];
                    }
                    break;
                }
                default:
                    return;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            base.Response.Buffer = true;
            base.Response.ExpiresAbsolute = DateTime.Now - new TimeSpan(1, 0, 0);
            base.Response.Expires = 0;
            base.Response.CacheControl = "no-cache";
            agent_userinfo_session model = this.Session[this.Session["user_name"] + "lottery_session_user_info"] as agent_userinfo_session;
            this.skin = model.get_u_skin();
            this.lotteryId = LSRequest.qq("lid");
            this.lotteryDT = base.GetLotteryList();
            base.IsLotteryExist(this.lotteryId, "u100032", "1", "");
            if (!model.get_u_type().Equals("zj"))
            {
                base.Response.Redirect("/MessagePage.aspx?code=u100057&url=&issuccess=1&isback=0");
                base.Response.End();
            }
            base.Permission_Aspx_ZJ(model, "po_3_3");
            int num = 100;
            if (this.lotteryId.Equals(num.ToString()))
            {
                base.Response.Redirect("/OddsSet/OddsSet_six.aspx?lid=" + this.lotteryId, true);
            }
            this.InitData();
            this.max_odds_isopen = CallBLL.cz_system_set_kc_bll.GetSystemSet(this.lotteryId).Rows[0]["max_odds_isopen"];
            if (this.max_odds_isopen.Equals("1"))
            {
                this.max_odds_isopen_str = "";
            }
            if (LSRequest.qq("submitHdn").Equals("submit"))
            {
                this.UpdateData();
            }
        }

        private void UpdateData()
        {
            DataTable rs = null;
            switch (Convert.ToInt32(this.lotteryId))
            {
                case 0:
                {
                    DataSet playByTradingSet = CallBLL.cz_play_kl10_bll.GetPlayByTradingSet("86,87,88,89,90,91,92,93,94,95,96,97,98,99,100,101,102,103,104,105,106,107,108,109,110,111,112,113,114,115,116,117,118,119,120,123,124,125,126,127,128,129,130,131,132,133,134,135,136");
                    if (((playByTradingSet != null) && (playByTradingSet.Tables.Count > 0)) && (playByTradingSet.Tables[0].Rows.Count > 0))
                    {
                        rs = playByTradingSet.Tables[0];
                        this.ValidInput_KL10(rs);
                        int num = 0;
                        foreach (DataRow row in rs.Rows)
                        {
                            string str = row["play_name"].ToString().Trim().Replace("第一球", "1-8球");
                            cz_play_kl10 _kl = new cz_play_kl10();
                            double num2 = 0.0;
                            double num3 = 0.0;
                            double num4 = 0.0;
                            double num5 = 0.0;
                            double num6 = 0.0;
                            double num7 = 0.0;
                            num2 = double.Parse(LSRequest.qq("t" + num.ToString() + "_default_odds_1"));
                            num4 = double.Parse(LSRequest.qq("t" + num.ToString() + "_max_odds_1"));
                            if (!this.max_odds_isopen.Equals("1"))
                            {
                                num4 = Convert.ToDouble(row["max_odds"].ToString().Split(new char[] { '|' })[0]);
                            }
                            num6 = double.Parse(LSRequest.qq("t" + num.ToString() + "_min_odds_1"));
                            if (num2 > num4)
                            {
                                base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能大於賠率上限！", str), null, 400));
                                base.Response.End();
                            }
                            if (num2 < num6)
                            {
                                base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能小於賠率下限！", str), null, 400));
                                base.Response.End();
                            }
                            if (num4 < num6)
                            {
                                base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率上限不能小於賠率下限！", str), null, 400));
                                base.Response.End();
                            }
                            if (num == 6)
                            {
                                num3 = double.Parse(LSRequest.qq("t" + num.ToString() + "_default_odds_2"));
                                num5 = double.Parse(LSRequest.qq("t" + num.ToString() + "_max_odds_2"));
                                if (!this.max_odds_isopen.Equals("1"))
                                {
                                    num5 = Convert.ToDouble(row["max_odds"].ToString().Split(new char[] { '|' })[1]);
                                }
                                num7 = double.Parse(LSRequest.qq("t" + num.ToString() + "_min_odds_2"));
                                if (num3 > num5)
                                {
                                    base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能大於賠率上限！", str), null, 400));
                                    base.Response.End();
                                }
                                if (num3 < num7)
                                {
                                    base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能小於賠率下限！", str), null, 400));
                                    base.Response.End();
                                }
                                if (num5 < num7)
                                {
                                    base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率上限不能小於賠率下限！", str), null, 400));
                                    base.Response.End();
                                }
                                _kl.set_default_odds(num2 + "|" + num3);
                                _kl.set_max_odds(num4 + "|" + num5);
                                _kl.set_min_odds(num6 + "|" + num7);
                            }
                            else
                            {
                                _kl.set_default_odds(num2.ToString());
                                _kl.set_max_odds(num4.ToString());
                                _kl.set_min_odds(num6.ToString());
                            }
                            _kl.set_b_diff(new decimal?(Convert.ToDecimal(LSRequest.qq("t" + num.ToString() + "_b_diff"))));
                            _kl.set_c_diff(new decimal?(Convert.ToDecimal(LSRequest.qq("t" + num.ToString() + "_c_diff"))));
                            if (!this.max_odds_isopen.Equals("1"))
                            {
                                _kl.set_b_diff(new decimal?(Convert.ToDecimal(row["b_diff"].ToString())));
                                _kl.set_c_diff(new decimal?(Convert.ToDecimal(row["c_diff"].ToString())));
                            }
                            _kl.set_downbase(new decimal?(Convert.ToDecimal(LSRequest.qq("t" + num.ToString() + "_downbase"))));
                            _kl.set_down_odds_rate(new decimal?(Convert.ToDecimal(LSRequest.qq("t" + num.ToString() + "_down_odds_rate"))));
                            _kl.set_allow_min_amount(new decimal?(decimal.Parse(row["allow_min_amount"].ToString())));
                            string str2 = "";
                            switch (num)
                            {
                                case 0:
                                    str2 = "81,86,91,96,101,106,111,116";
                                    break;

                                case 1:
                                    str2 = "82,87,92,97,102,107,112,117";
                                    break;

                                case 2:
                                    str2 = "83,88,93,98,103,108,113,118";
                                    break;

                                case 3:
                                    str2 = "84,89,94,99,104,109,114,119";
                                    break;

                                case 4:
                                    str2 = "85,90,95,100,105,110,115,120";
                                    break;

                                case 5:
                                    str2 = "121,123,125,127,129,131,133,135";
                                    break;

                                case 6:
                                    str2 = "122,124,126,128,130,132,134,136";
                                    break;

                                default:
                                    str2 = row["play_id"].ToString();
                                    break;
                            }
                            CallBLL.cz_play_kl10_bll.SetPlayByOddsSet(str2, _kl);
                            num++;
                        }
                        base.plsd_kc_log(rs, CallBLL.cz_play_kl10_bll.GetPlayByTradingSet("86,87,88,89,90,91,92,93,94,95,96,97,98,99,100,101,102,103,104,105,106,107,108,109,110,111,112,113,114,115,116,117,118,119,120,123,124,125,126,127,128,129,130,131,132,133,134,135,136").Tables[0], this.lotteryId);
                    }
                    break;
                }
                case 1:
                {
                    DataSet playByOddsSet = CallBLL.cz_play_cqsc_bll.GetPlayByOddsSet("1,2,3,16,17,18,19");
                    if (((playByOddsSet != null) && (playByOddsSet.Tables.Count > 0)) && (playByOddsSet.Tables[0].Rows.Count > 0))
                    {
                        rs = playByOddsSet.Tables[0];
                        this.ValidInput_CQSC(rs);
                        int num8 = 0;
                        foreach (DataRow row2 in rs.Rows)
                        {
                            string str3 = row2["play_name"].ToString().Trim().Replace("第一球", "1-5球");
                            cz_play_cqsc _cqsc = new cz_play_cqsc();
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
                            double num23 = 0.0;
                            num9 = double.Parse(LSRequest.qq("t" + num8.ToString() + "_default_odds_1"));
                            num14 = double.Parse(LSRequest.qq("t" + num8.ToString() + "_max_odds_1"));
                            if (!this.max_odds_isopen.Equals("1"))
                            {
                                num14 = Convert.ToDouble(row2["max_odds"].ToString().Split(new char[] { '|' })[0]);
                            }
                            num19 = double.Parse(LSRequest.qq("t" + num8.ToString() + "_min_odds_1"));
                            if (num9 > num14)
                            {
                                base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能大於賠率上限！", str3), null, 400));
                                base.Response.End();
                            }
                            if (num9 < num19)
                            {
                                base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能小於賠率下限！", str3), null, 400));
                                base.Response.End();
                            }
                            if (num14 < num19)
                            {
                                base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率上限不能小於賠率下限！", str3), null, 400));
                                base.Response.End();
                            }
                            switch (num8)
                            {
                                case 5:
                                    num10 = double.Parse(LSRequest.qq("t" + num8.ToString() + "_default_odds_2"));
                                    num15 = double.Parse(LSRequest.qq("t" + num8.ToString() + "_max_odds_2"));
                                    if (!this.max_odds_isopen.Equals("1"))
                                    {
                                        num15 = Convert.ToDouble(row2["max_odds"].ToString().Split(new char[] { '|' })[1]);
                                    }
                                    num20 = double.Parse(LSRequest.qq("t" + num8.ToString() + "_min_odds_2"));
                                    if (num10 > num15)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能大於賠率上限！", str3), null, 400));
                                        base.Response.End();
                                    }
                                    if (num10 < num20)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能小於賠率下限！", str3), null, 400));
                                        base.Response.End();
                                    }
                                    if (num15 < num20)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率上限不能小於賠率下限！", str3), null, 400));
                                        base.Response.End();
                                    }
                                    _cqsc.set_default_odds(num9 + "|" + num10);
                                    _cqsc.set_max_odds(num14 + "|" + num15);
                                    _cqsc.set_min_odds(num19 + "|" + num20);
                                    break;

                                case 6:
                                    num10 = double.Parse(LSRequest.qq("t" + num8.ToString() + "_default_odds_2"));
                                    num15 = double.Parse(LSRequest.qq("t" + num8.ToString() + "_max_odds_2"));
                                    if (!this.max_odds_isopen.Equals("1"))
                                    {
                                        num15 = Convert.ToDouble(row2["max_odds"].ToString().Split(new char[] { '|' })[1]);
                                    }
                                    num20 = double.Parse(LSRequest.qq("t" + num8.ToString() + "_min_odds_2"));
                                    num11 = double.Parse(LSRequest.qq("t" + num8.ToString() + "_default_odds_3"));
                                    num16 = double.Parse(LSRequest.qq("t" + num8.ToString() + "_max_odds_3"));
                                    if (!this.max_odds_isopen.Equals("1"))
                                    {
                                        num16 = Convert.ToDouble(row2["max_odds"].ToString().Split(new char[] { '|' })[2]);
                                    }
                                    num21 = double.Parse(LSRequest.qq("t" + num8.ToString() + "_min_odds_3"));
                                    num12 = double.Parse(LSRequest.qq("t" + num8.ToString() + "_default_odds_4"));
                                    num17 = double.Parse(LSRequest.qq("t" + num8.ToString() + "_max_odds_4"));
                                    if (!this.max_odds_isopen.Equals("1"))
                                    {
                                        num17 = Convert.ToDouble(row2["max_odds"].ToString().Split(new char[] { '|' })[3]);
                                    }
                                    num22 = double.Parse(LSRequest.qq("t" + num8.ToString() + "_min_odds_4"));
                                    num13 = double.Parse(LSRequest.qq("t" + num8.ToString() + "_default_odds_5"));
                                    num18 = double.Parse(LSRequest.qq("t" + num8.ToString() + "_max_odds_5"));
                                    if (!this.max_odds_isopen.Equals("1"))
                                    {
                                        num18 = Convert.ToDouble(row2["max_odds"].ToString().Split(new char[] { '|' })[4]);
                                    }
                                    num23 = double.Parse(LSRequest.qq("t" + num8.ToString() + "_min_odds_5"));
                                    if (num10 > num15)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能大於賠率上限！", str3), null, 400));
                                        base.Response.End();
                                    }
                                    if (num10 < num20)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能小於賠率下限！", str3), null, 400));
                                        base.Response.End();
                                    }
                                    if (num15 < num20)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率上限不能小於賠率下限！", str3), null, 400));
                                        base.Response.End();
                                    }
                                    if (num11 > num16)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能大於賠率上限！", str3), null, 400));
                                        base.Response.End();
                                    }
                                    if (num11 < num21)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能小於賠率下限！", str3), null, 400));
                                        base.Response.End();
                                    }
                                    if (num16 < num21)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率上限不能小於賠率下限！", str3), null, 400));
                                        base.Response.End();
                                    }
                                    if (num12 > num17)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能大於賠率上限！", str3), null, 400));
                                        base.Response.End();
                                    }
                                    if (num12 < num22)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能小於賠率下限！", str3), null, 400));
                                        base.Response.End();
                                    }
                                    if (num17 < num22)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率上限不能小於賠率下限！", str3), null, 400));
                                        base.Response.End();
                                    }
                                    if (num13 > num18)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能大於賠率上限！", str3), null, 400));
                                        base.Response.End();
                                    }
                                    if (num13 < num23)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能小於賠率下限！", str3), null, 400));
                                        base.Response.End();
                                    }
                                    if (num18 < num23)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率上限不能小於賠率下限！", str3), null, 400));
                                        base.Response.End();
                                    }
                                    _cqsc.set_default_odds(string.Concat(new object[] { num9, "|", num10, "|", num11, "|", num12, "|", num13 }));
                                    _cqsc.set_max_odds(string.Concat(new object[] { num14, "|", num15, "|", num16, "|", num17, "|", num18 }));
                                    _cqsc.set_min_odds(string.Concat(new object[] { num19, "|", num20, "|", num21, "|", num22, "|", num23 }));
                                    break;

                                default:
                                    _cqsc.set_default_odds(num9.ToString());
                                    _cqsc.set_max_odds(num14.ToString());
                                    _cqsc.set_min_odds(num19.ToString());
                                    break;
                            }
                            _cqsc.set_b_diff(new decimal?(Convert.ToDecimal(LSRequest.qq("t" + num8.ToString() + "_b_diff"))));
                            _cqsc.set_c_diff(new decimal?(Convert.ToDecimal(LSRequest.qq("t" + num8.ToString() + "_c_diff"))));
                            if (!this.max_odds_isopen.Equals("1"))
                            {
                                _cqsc.set_b_diff(new decimal?(Convert.ToDecimal(row2["b_diff"].ToString())));
                                _cqsc.set_c_diff(new decimal?(Convert.ToDecimal(row2["c_diff"].ToString())));
                            }
                            _cqsc.set_downbase(new decimal?(Convert.ToDecimal(LSRequest.qq("t" + num8.ToString() + "_downbase"))));
                            _cqsc.set_down_odds_rate(new decimal?(Convert.ToDecimal(LSRequest.qq("t" + num8.ToString() + "_down_odds_rate"))));
                            _cqsc.set_allow_min_amount(new decimal?(decimal.Parse(row2["allow_min_amount"].ToString())));
                            string str4 = "";
                            switch (num8)
                            {
                                case 0:
                                    str4 = "1,4,7,10,13";
                                    break;

                                case 1:
                                    str4 = "2,5,8,11,14";
                                    break;

                                case 2:
                                    str4 = "3,6,9,12,15";
                                    break;

                                case 5:
                                    str4 = row2["play_id"].ToString();
                                    break;

                                case 6:
                                    str4 = "19,20,21";
                                    break;

                                default:
                                    str4 = row2["play_id"].ToString();
                                    break;
                            }
                            CallBLL.cz_play_cqsc_bll.SetPlayByOddsSet(str4, _cqsc);
                            num8++;
                        }
                        base.plsd_kc_log(rs, CallBLL.cz_play_cqsc_bll.GetPlayByOddsSet("1,2,3,16,17,18,19").Tables[0], this.lotteryId);
                    }
                    break;
                }
                case 2:
                {
                    DataSet set3 = CallBLL.cz_play_pk10_bll.GetPlayByOddsSet("1,2,3,4,36,37,38");
                    if (((set3 != null) && (set3.Tables.Count > 0)) && (set3.Tables[0].Rows.Count > 0))
                    {
                        rs = set3.Tables[0];
                        this.ValidInput_PK10(rs);
                        int num24 = 0;
                        foreach (DataRow row3 in rs.Rows)
                        {
                            string str5 = row3["play_name"].ToString().Trim().Replace("冠軍龍虎", "1-5名龍虎").Replace("冠軍", "1-10名");
                            cz_play_pk10 _pk = new cz_play_pk10();
                            double num25 = 0.0;
                            double num26 = 0.0;
                            double num27 = 0.0;
                            double num28 = 0.0;
                            double num29 = 0.0;
                            double num30 = 0.0;
                            double num31 = 0.0;
                            double num32 = 0.0;
                            double num33 = 0.0;
                            double num34 = 0.0;
                            double num35 = 0.0;
                            double num36 = 0.0;
                            double num37 = 0.0;
                            double num38 = 0.0;
                            double num39 = 0.0;
                            num25 = double.Parse(LSRequest.qq("t" + num24.ToString() + "_default_odds_1"));
                            num30 = double.Parse(LSRequest.qq("t" + num24.ToString() + "_max_odds_1"));
                            if (!this.max_odds_isopen.Equals("1"))
                            {
                                num30 = Convert.ToDouble(row3["max_odds"].ToString().Split(new char[] { '|' })[0]);
                            }
                            num35 = double.Parse(LSRequest.qq("t" + num24.ToString() + "_min_odds_1"));
                            if (num25 > num30)
                            {
                                base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能大於賠率上限！", str5), null, 400));
                                base.Response.End();
                            }
                            if (num25 < num35)
                            {
                                base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能小於賠率下限！", str5), null, 400));
                                base.Response.End();
                            }
                            if (num30 < num35)
                            {
                                base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率上限不能小於賠率下限！", str5), null, 400));
                                base.Response.End();
                            }
                            switch (num24)
                            {
                                case 4:
                                    num26 = double.Parse(LSRequest.qq("t" + num24.ToString() + "_default_odds_2"));
                                    num31 = double.Parse(LSRequest.qq("t" + num24.ToString() + "_max_odds_2"));
                                    if (!this.max_odds_isopen.Equals("1"))
                                    {
                                        num31 = Convert.ToDouble(row3["max_odds"].ToString().Split(new char[] { '|' })[1]);
                                    }
                                    num36 = double.Parse(LSRequest.qq("t" + num24.ToString() + "_min_odds_2"));
                                    num27 = double.Parse(LSRequest.qq("t" + num24.ToString() + "_default_odds_3"));
                                    num32 = double.Parse(LSRequest.qq("t" + num24.ToString() + "_max_odds_3"));
                                    if (!this.max_odds_isopen.Equals("1"))
                                    {
                                        num32 = Convert.ToDouble(row3["max_odds"].ToString().Split(new char[] { '|' })[2]);
                                    }
                                    num37 = double.Parse(LSRequest.qq("t" + num24.ToString() + "_min_odds_3"));
                                    num28 = double.Parse(LSRequest.qq("t" + num24.ToString() + "_default_odds_4"));
                                    num33 = double.Parse(LSRequest.qq("t" + num24.ToString() + "_max_odds_4"));
                                    if (!this.max_odds_isopen.Equals("1"))
                                    {
                                        num33 = Convert.ToDouble(row3["max_odds"].ToString().Split(new char[] { '|' })[3]);
                                    }
                                    num38 = double.Parse(LSRequest.qq("t" + num24.ToString() + "_min_odds_4"));
                                    num29 = double.Parse(LSRequest.qq("t" + num24.ToString() + "_default_odds_5"));
                                    num34 = double.Parse(LSRequest.qq("t" + num24.ToString() + "_max_odds_5"));
                                    if (!this.max_odds_isopen.Equals("1"))
                                    {
                                        num34 = Convert.ToDouble(row3["max_odds"].ToString().Split(new char[] { '|' })[4]);
                                    }
                                    num39 = double.Parse(LSRequest.qq("t" + num24.ToString() + "_min_odds_5"));
                                    if (num26 > num31)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能大於賠率上限！", str5), null, 400));
                                        base.Response.End();
                                    }
                                    if (num26 < num36)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能小於賠率下限！", str5), null, 400));
                                        base.Response.End();
                                    }
                                    if (num31 < num36)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率上限不能小於賠率下限！", str5), null, 400));
                                        base.Response.End();
                                    }
                                    if (num27 > num32)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能大於賠率上限！", str5), null, 400));
                                        base.Response.End();
                                    }
                                    if (num27 < num37)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能小於賠率下限！", str5), null, 400));
                                        base.Response.End();
                                    }
                                    if (num32 < num37)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率上限不能小於賠率下限！", str5), null, 400));
                                        base.Response.End();
                                    }
                                    if (num28 > num33)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能大於賠率上限！", str5), null, 400));
                                        base.Response.End();
                                    }
                                    if (num28 < num38)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能小於賠率下限！", str5), null, 400));
                                        base.Response.End();
                                    }
                                    if (num33 < num38)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率上限不能小於賠率下限！", str5), null, 400));
                                        base.Response.End();
                                    }
                                    if (num29 > num34)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能大於賠率上限！", str5), null, 400));
                                        base.Response.End();
                                    }
                                    if (num29 < num39)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能小於賠率下限！", str5), null, 400));
                                        base.Response.End();
                                    }
                                    if (num34 < num39)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率上限不能小於賠率下限！", str5), null, 400));
                                        base.Response.End();
                                    }
                                    _pk.set_default_odds(string.Concat(new object[] { num25, "|", num26, "|", num27, "|", num28, "|", num29 }));
                                    _pk.set_max_odds(string.Concat(new object[] { num30, "|", num31, "|", num32, "|", num33, "|", num34 }));
                                    _pk.set_min_odds(string.Concat(new object[] { num35, "|", num36, "|", num37, "|", num38, "|", num39 }));
                                    break;

                                case 5:
                                case 6:
                                    num26 = double.Parse(LSRequest.qq("t" + num24.ToString() + "_default_odds_2"));
                                    num31 = double.Parse(LSRequest.qq("t" + num24.ToString() + "_max_odds_2"));
                                    if (!this.max_odds_isopen.Equals("1"))
                                    {
                                        num31 = Convert.ToDouble(row3["max_odds"].ToString().Split(new char[] { '|' })[1]);
                                    }
                                    num36 = double.Parse(LSRequest.qq("t" + num24.ToString() + "_min_odds_2"));
                                    if (num26 > num31)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能大於賠率上限！", str5), null, 400));
                                        base.Response.End();
                                    }
                                    if (num26 < num36)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能小於賠率下限！", str5), null, 400));
                                        base.Response.End();
                                    }
                                    if (num31 < num36)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率上限不能小於賠率下限！", str5), null, 400));
                                        base.Response.End();
                                    }
                                    _pk.set_default_odds(num25 + "|" + num26);
                                    _pk.set_max_odds(num30 + "|" + num31);
                                    _pk.set_min_odds(num35 + "|" + num36);
                                    break;

                                default:
                                    _pk.set_default_odds(num25.ToString());
                                    _pk.set_max_odds(num30.ToString());
                                    _pk.set_min_odds(num35.ToString());
                                    break;
                            }
                            _pk.set_b_diff(new decimal?(Convert.ToDecimal(LSRequest.qq("t" + num24.ToString() + "_b_diff"))));
                            _pk.set_c_diff(new decimal?(Convert.ToDecimal(LSRequest.qq("t" + num24.ToString() + "_c_diff"))));
                            if (!this.max_odds_isopen.Equals("1"))
                            {
                                _pk.set_b_diff(new decimal?(Convert.ToDecimal(row3["b_diff"].ToString())));
                                _pk.set_c_diff(new decimal?(Convert.ToDecimal(row3["c_diff"].ToString())));
                            }
                            _pk.set_downbase(new decimal?(Convert.ToDecimal(LSRequest.qq("t" + num24.ToString() + "_downbase"))));
                            _pk.set_down_odds_rate(new decimal?(Convert.ToDecimal(LSRequest.qq("t" + num24.ToString() + "_down_odds_rate"))));
                            _pk.set_allow_min_amount(new decimal?(decimal.Parse(row3["allow_min_amount"].ToString())));
                            string str6 = "";
                            switch (num24)
                            {
                                case 0:
                                    str6 = "1,5,9,13,17,21,24,27,30,33";
                                    break;

                                case 1:
                                    str6 = "2,6,10,14,18,22,25,28,31,34";
                                    break;

                                case 2:
                                    str6 = "3,7,11,15,19,23,26,29,32,35";
                                    break;

                                case 3:
                                    str6 = "4,8,12,16,20";
                                    break;

                                case 4:
                                    str6 = row3["play_id"].ToString();
                                    break;

                                case 5:
                                    str6 = row3["play_id"].ToString();
                                    break;

                                case 6:
                                    str6 = row3["play_id"].ToString();
                                    break;

                                default:
                                    str6 = row3["play_id"].ToString();
                                    break;
                            }
                            CallBLL.cz_play_pk10_bll.SetPlayByOddsSet(str6, _pk);
                            num24++;
                        }
                        base.plsd_kc_log(rs, CallBLL.cz_play_pk10_bll.GetPlayByOddsSet("1,2,3,4,36,37,38").Tables[0], this.lotteryId);
                    }
                    break;
                }
                case 3:
                {
                    DataSet set4 = CallBLL.cz_play_xync_bll.GetPlayByOddsSet("86,87,88,89,90,91,92,93,94,95,96,97,98,99,100,101,102,103,104,105,106,107,108,109,110,111,112,113,114,115,116,117,118,119,120,123,124,125,126,127,128,129,130,131,132,133,134,135,136");
                    if (((set4 != null) && (set4.Tables.Count > 0)) && (set4.Tables[0].Rows.Count > 0))
                    {
                        rs = set4.Tables[0];
                        this.ValidInput_XYNC(rs);
                        int num40 = 0;
                        foreach (DataRow row4 in rs.Rows)
                        {
                            string str7 = row4["play_name"].ToString().Trim().Replace("第一球", "1-8球");
                            cz_play_xync _xync = new cz_play_xync();
                            double num41 = 0.0;
                            double num42 = 0.0;
                            double num43 = 0.0;
                            double num44 = 0.0;
                            double num45 = 0.0;
                            double num46 = 0.0;
                            num41 = double.Parse(LSRequest.qq("t" + num40.ToString() + "_default_odds_1"));
                            num43 = double.Parse(LSRequest.qq("t" + num40.ToString() + "_max_odds_1"));
                            if (!this.max_odds_isopen.Equals("1"))
                            {
                                num43 = Convert.ToDouble(row4["max_odds"].ToString().Split(new char[] { '|' })[0]);
                            }
                            num45 = double.Parse(LSRequest.qq("t" + num40.ToString() + "_min_odds_1"));
                            if (num41 > num43)
                            {
                                base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能大於賠率上限！", str7), null, 400));
                                base.Response.End();
                            }
                            if (num41 < num45)
                            {
                                base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能小於賠率下限！", str7), null, 400));
                                base.Response.End();
                            }
                            if (num43 < num45)
                            {
                                base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率上限不能小於賠率下限！", str7), null, 400));
                                base.Response.End();
                            }
                            if (num40 == 6)
                            {
                                num42 = double.Parse(LSRequest.qq("t" + num40.ToString() + "_default_odds_2"));
                                num44 = double.Parse(LSRequest.qq("t" + num40.ToString() + "_max_odds_2"));
                                if (!this.max_odds_isopen.Equals("1"))
                                {
                                    num44 = Convert.ToDouble(row4["max_odds"].ToString().Split(new char[] { '|' })[1]);
                                }
                                num46 = double.Parse(LSRequest.qq("t" + num40.ToString() + "_min_odds_2"));
                                if (num42 > num44)
                                {
                                    base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能大於賠率上限！", str7), null, 400));
                                    base.Response.End();
                                }
                                if (num42 < num46)
                                {
                                    base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能小於賠率下限！", str7), null, 400));
                                    base.Response.End();
                                }
                                if (num44 < num46)
                                {
                                    base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率上限不能小於賠率下限！", str7), null, 400));
                                    base.Response.End();
                                }
                                _xync.set_default_odds(num41 + "|" + num42);
                                _xync.set_max_odds(num43 + "|" + num44);
                                _xync.set_min_odds(num45 + "|" + num46);
                            }
                            else
                            {
                                _xync.set_default_odds(num41.ToString());
                                _xync.set_max_odds(num43.ToString());
                                _xync.set_min_odds(num45.ToString());
                            }
                            _xync.set_b_diff(new decimal?(Convert.ToDecimal(LSRequest.qq("t" + num40.ToString() + "_b_diff"))));
                            _xync.set_c_diff(new decimal?(Convert.ToDecimal(LSRequest.qq("t" + num40.ToString() + "_c_diff"))));
                            if (!this.max_odds_isopen.Equals("1"))
                            {
                                _xync.set_b_diff(new decimal?(Convert.ToDecimal(row4["b_diff"].ToString())));
                                _xync.set_c_diff(new decimal?(Convert.ToDecimal(row4["c_diff"].ToString())));
                            }
                            _xync.set_downbase(new decimal?(Convert.ToDecimal(LSRequest.qq("t" + num40.ToString() + "_downbase"))));
                            _xync.set_down_odds_rate(new decimal?(Convert.ToDecimal(LSRequest.qq("t" + num40.ToString() + "_down_odds_rate"))));
                            _xync.set_allow_min_amount(new decimal?(decimal.Parse(row4["allow_min_amount"].ToString())));
                            string str8 = "";
                            switch (num40)
                            {
                                case 0:
                                    str8 = "81,86,91,96,101,106,111,116";
                                    break;

                                case 1:
                                    str8 = "82,87,92,97,102,107,112,117";
                                    break;

                                case 2:
                                    str8 = "83,88,93,98,103,108,113,118";
                                    break;

                                case 3:
                                    str8 = "84,89,94,99,104,109,114,119";
                                    break;

                                case 4:
                                    str8 = "85,90,95,100,105,110,115,120";
                                    break;

                                case 5:
                                    str8 = "121,123,125,127,129,131,133,135";
                                    break;

                                case 6:
                                    str8 = "122,124,126,128,130,132,134,136";
                                    break;

                                default:
                                    str8 = row4["play_id"].ToString();
                                    break;
                            }
                            CallBLL.cz_play_xync_bll.SetPlayByOddsSet(str8, _xync);
                            num40++;
                        }
                        base.plsd_kc_log(rs, CallBLL.cz_play_xync_bll.GetPlayByOddsSet("86,87,88,89,90,91,92,93,94,95,96,97,98,99,100,101,102,103,104,105,106,107,108,109,110,111,112,113,114,115,116,117,118,119,120,123,124,125,126,127,128,129,130,131,132,133,134,135,136").Tables[0], this.lotteryId);
                    }
                    break;
                }
                case 4:
                {
                    DataSet set5 = CallBLL.cz_play_jsk3_bll.GetPlayByOddsSet("");
                    if (((set5 != null) && (set5.Tables.Count > 0)) && (set5.Tables[0].Rows.Count > 0))
                    {
                        rs = set5.Tables[0];
                        this.ValidInput_JSK3(rs);
                        int num47 = 0;
                        foreach (DataRow row5 in rs.Rows)
                        {
                            string str9 = row5["play_name"].ToString().Trim();
                            cz_play_jsk3 _jsk = new cz_play_jsk3();
                            double num48 = 0.0;
                            double num49 = 0.0;
                            double num50 = 0.0;
                            num48 = double.Parse(LSRequest.qq("t" + num47.ToString() + "_default_odds_1"));
                            num49 = double.Parse(LSRequest.qq("t" + num47.ToString() + "_max_odds_1"));
                            if (!this.max_odds_isopen.Equals("1"))
                            {
                                num49 = Convert.ToDouble(row5["max_odds"].ToString().Split(new char[] { '|' })[0]);
                            }
                            num50 = double.Parse(LSRequest.qq("t" + num47.ToString() + "_min_odds_1"));
                            if (num48 > num49)
                            {
                                base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能大於賠率上限！", str9), null, 400));
                                base.Response.End();
                            }
                            if (num48 < num50)
                            {
                                base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能小於賠率下限！", str9), null, 400));
                                base.Response.End();
                            }
                            if (num49 < num50)
                            {
                                base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率上限不能小於賠率下限！", str9), null, 400));
                                base.Response.End();
                            }
                            if (num47 == 4)
                            {
                                string str10 = "";
                                string str11 = "";
                                string str12 = "";
                                for (int i = 0; i < 14; i++)
                                {
                                    double num52 = double.Parse(LSRequest.qq(string.Concat(new object[] { "t", num47.ToString(), "_default_odds_", i + 1 })));
                                    double num53 = double.Parse(LSRequest.qq(string.Concat(new object[] { "t", num47.ToString(), "_max_odds_", i + 1 })));
                                    if (!this.max_odds_isopen.Equals("1"))
                                    {
                                        num53 = Convert.ToDouble(row5["max_odds"].ToString().Split(new char[] { '|' })[i]);
                                    }
                                    double num54 = double.Parse(LSRequest.qq(string.Concat(new object[] { "t", num47.ToString(), "_min_odds_", i + 1 })));
                                    if (num52 > num53)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能大於賠率上限！", str9), null, 400));
                                        base.Response.End();
                                    }
                                    if (num52 < num54)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能小於賠率下限！", str9), null, 400));
                                        base.Response.End();
                                    }
                                    if (num53 < num54)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率上限不能小於賠率下限！", str9), null, 400));
                                        base.Response.End();
                                    }
                                    if (i.Equals(0))
                                    {
                                        str10 = str10 + num52;
                                        str11 = str11 + num53;
                                        str12 = str12 + num54;
                                    }
                                    else
                                    {
                                        str10 = str10 + "|" + num52;
                                        str11 = str11 + "|" + num53;
                                        str12 = str12 + "|" + num54;
                                    }
                                }
                                _jsk.set_default_odds(str10);
                                _jsk.set_max_odds(str11);
                                _jsk.set_min_odds(str12);
                            }
                            else
                            {
                                _jsk.set_default_odds(num48.ToString());
                                _jsk.set_max_odds(num49.ToString());
                                _jsk.set_min_odds(num50.ToString());
                            }
                            _jsk.set_b_diff(new decimal?(Convert.ToDecimal(LSRequest.qq("t" + num47.ToString() + "_b_diff"))));
                            _jsk.set_c_diff(new decimal?(Convert.ToDecimal(LSRequest.qq("t" + num47.ToString() + "_c_diff"))));
                            if (!this.max_odds_isopen.Equals("1"))
                            {
                                _jsk.set_b_diff(new decimal?(Convert.ToDecimal(row5["b_diff"].ToString())));
                                _jsk.set_c_diff(new decimal?(Convert.ToDecimal(row5["c_diff"].ToString())));
                            }
                            _jsk.set_downbase(new decimal?(Convert.ToDecimal(LSRequest.qq("t" + num47.ToString() + "_downbase"))));
                            _jsk.set_down_odds_rate(new decimal?(Convert.ToDecimal(LSRequest.qq("t" + num47.ToString() + "_down_odds_rate"))));
                            _jsk.set_allow_min_amount(new decimal?(decimal.Parse(row5["allow_min_amount"].ToString())));
                            string str13 = row5["play_id"].ToString();
                            CallBLL.cz_play_jsk3_bll.SetPlayByOddsSet(str13, _jsk);
                            num47++;
                        }
                        base.plsd_kc_log(rs, CallBLL.cz_play_jsk3_bll.GetPlayByOddsSet("").Tables[0], this.lotteryId);
                    }
                    break;
                }
                case 5:
                {
                    DataSet set6 = CallBLL.cz_play_kl8_bll.GetPlayByOddsSet("");
                    if (((set6 != null) && (set6.Tables.Count > 0)) && (set6.Tables[0].Rows.Count > 0))
                    {
                        rs = set6.Tables[0];
                        this.ValidInput_KL8(rs);
                        int num55 = 0;
                        foreach (DataRow row6 in rs.Rows)
                        {
                            string str14 = row6["play_name"].ToString().Trim();
                            cz_play_kl8 _kl2 = new cz_play_kl8();
                            double num56 = 0.0;
                            double num57 = 0.0;
                            double num58 = 0.0;
                            double num59 = 0.0;
                            double num60 = 0.0;
                            double num61 = 0.0;
                            double num62 = 0.0;
                            double num63 = 0.0;
                            double num64 = 0.0;
                            double num65 = 0.0;
                            double num66 = 0.0;
                            double num67 = 0.0;
                            double num68 = 0.0;
                            double num69 = 0.0;
                            double num70 = 0.0;
                            num56 = double.Parse(LSRequest.qq("t" + num55.ToString() + "_default_odds_1"));
                            num61 = double.Parse(LSRequest.qq("t" + num55.ToString() + "_max_odds_1"));
                            if (!this.max_odds_isopen.Equals("1"))
                            {
                                num61 = Convert.ToDouble(row6["max_odds"].ToString().Split(new char[] { '|' })[0]);
                            }
                            num66 = double.Parse(LSRequest.qq("t" + num55.ToString() + "_min_odds_1"));
                            if (num56 > num61)
                            {
                                base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能大於賠率上限！", str14), null, 400));
                                base.Response.End();
                            }
                            if (num56 < num66)
                            {
                                base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能小於賠率下限！", str14), null, 400));
                                base.Response.End();
                            }
                            if (num61 < num66)
                            {
                                base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率上限不能小於賠率下限！", str14), null, 400));
                                base.Response.End();
                            }
                            switch (num55)
                            {
                                case 5:
                                case 6:
                                    num57 = double.Parse(LSRequest.qq("t" + num55.ToString() + "_default_odds_2"));
                                    num62 = double.Parse(LSRequest.qq("t" + num55.ToString() + "_max_odds_2"));
                                    if (!this.max_odds_isopen.Equals("1"))
                                    {
                                        num62 = Convert.ToDouble(row6["max_odds"].ToString().Split(new char[] { '|' })[1]);
                                    }
                                    num67 = double.Parse(LSRequest.qq("t" + num55.ToString() + "_min_odds_2"));
                                    if (num57 > num62)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能大於賠率上限！", str14), null, 400));
                                        base.Response.End();
                                    }
                                    if (num57 < num67)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能小於賠率下限！", str14), null, 400));
                                        base.Response.End();
                                    }
                                    if (num62 < num67)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率上限不能小於賠率下限！", str14), null, 400));
                                        base.Response.End();
                                    }
                                    _kl2.set_default_odds(num56 + "|" + num57);
                                    _kl2.set_max_odds(num61 + "|" + num62);
                                    _kl2.set_min_odds(num66 + "|" + num67);
                                    break;

                                case 7:
                                    num57 = double.Parse(LSRequest.qq("t" + num55.ToString() + "_default_odds_2"));
                                    num62 = double.Parse(LSRequest.qq("t" + num55.ToString() + "_max_odds_2"));
                                    if (!this.max_odds_isopen.Equals("1"))
                                    {
                                        num62 = Convert.ToDouble(row6["max_odds"].ToString().Split(new char[] { '|' })[1]);
                                    }
                                    num67 = double.Parse(LSRequest.qq("t" + num55.ToString() + "_min_odds_2"));
                                    num58 = double.Parse(LSRequest.qq("t" + num55.ToString() + "_default_odds_3"));
                                    num63 = double.Parse(LSRequest.qq("t" + num55.ToString() + "_max_odds_3"));
                                    if (!this.max_odds_isopen.Equals("1"))
                                    {
                                        num63 = Convert.ToDouble(row6["max_odds"].ToString().Split(new char[] { '|' })[2]);
                                    }
                                    num68 = double.Parse(LSRequest.qq("t" + num55.ToString() + "_min_odds_3"));
                                    num59 = double.Parse(LSRequest.qq("t" + num55.ToString() + "_default_odds_4"));
                                    num64 = double.Parse(LSRequest.qq("t" + num55.ToString() + "_max_odds_4"));
                                    if (!this.max_odds_isopen.Equals("1"))
                                    {
                                        num64 = Convert.ToDouble(row6["max_odds"].ToString().Split(new char[] { '|' })[3]);
                                    }
                                    num69 = double.Parse(LSRequest.qq("t" + num55.ToString() + "_min_odds_4"));
                                    num60 = double.Parse(LSRequest.qq("t" + num55.ToString() + "_default_odds_5"));
                                    num65 = double.Parse(LSRequest.qq("t" + num55.ToString() + "_max_odds_5"));
                                    if (!this.max_odds_isopen.Equals("1"))
                                    {
                                        num65 = Convert.ToDouble(row6["max_odds"].ToString().Split(new char[] { '|' })[4]);
                                    }
                                    num70 = double.Parse(LSRequest.qq("t" + num55.ToString() + "_min_odds_5"));
                                    if (num57 > num62)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能大於賠率上限！", str14), null, 400));
                                        base.Response.End();
                                    }
                                    if (num57 < num67)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能小於賠率下限！", str14), null, 400));
                                        base.Response.End();
                                    }
                                    if (num62 < num67)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率上限不能小於賠率下限！", str14), null, 400));
                                        base.Response.End();
                                    }
                                    if (num58 > num63)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能大於賠率上限！", str14), null, 400));
                                        base.Response.End();
                                    }
                                    if (num58 < num68)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能小於賠率下限！", str14), null, 400));
                                        base.Response.End();
                                    }
                                    if (num63 < num68)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率上限不能小於賠率下限！", str14), null, 400));
                                        base.Response.End();
                                    }
                                    if (num59 > num64)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能大於賠率上限！", str14), null, 400));
                                        base.Response.End();
                                    }
                                    if (num59 < num69)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能小於賠率下限！", str14), null, 400));
                                        base.Response.End();
                                    }
                                    if (num64 < num69)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率上限不能小於賠率下限！", str14), null, 400));
                                        base.Response.End();
                                    }
                                    if (num60 > num65)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能大於賠率上限！", str14), null, 400));
                                        base.Response.End();
                                    }
                                    if (num60 < num70)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能小於賠率下限！", str14), null, 400));
                                        base.Response.End();
                                    }
                                    if (num65 < num70)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率上限不能小於賠率下限！", str14), null, 400));
                                        base.Response.End();
                                    }
                                    _kl2.set_default_odds(string.Concat(new object[] { num56, "|", num57, "|", num58, "|", num59, "|", num60 }));
                                    _kl2.set_max_odds(string.Concat(new object[] { num61, "|", num62, "|", num63, "|", num64, "|", num65 }));
                                    _kl2.set_min_odds(string.Concat(new object[] { num66, "|", num67, "|", num68, "|", num69, "|", num70 }));
                                    break;

                                default:
                                    _kl2.set_default_odds(num56.ToString());
                                    _kl2.set_max_odds(num61.ToString());
                                    _kl2.set_min_odds(num66.ToString());
                                    break;
                            }
                            _kl2.set_b_diff(new decimal?(Convert.ToDecimal(LSRequest.qq("t" + num55.ToString() + "_b_diff"))));
                            _kl2.set_c_diff(new decimal?(Convert.ToDecimal(LSRequest.qq("t" + num55.ToString() + "_c_diff"))));
                            if (!this.max_odds_isopen.Equals("1"))
                            {
                                _kl2.set_b_diff(new decimal?(Convert.ToDecimal(row6["b_diff"].ToString())));
                                _kl2.set_c_diff(new decimal?(Convert.ToDecimal(row6["c_diff"].ToString())));
                            }
                            _kl2.set_downbase(new decimal?(Convert.ToDecimal(LSRequest.qq("t" + num55.ToString() + "_downbase"))));
                            _kl2.set_down_odds_rate(new decimal?(Convert.ToDecimal(LSRequest.qq("t" + num55.ToString() + "_down_odds_rate"))));
                            _kl2.set_allow_min_amount(new decimal?(decimal.Parse(row6["allow_min_amount"].ToString())));
                            string str15 = row6["play_id"].ToString();
                            CallBLL.cz_play_kl8_bll.SetPlayByOddsSet(str15, _kl2);
                            num55++;
                        }
                        base.plsd_kc_log(rs, CallBLL.cz_play_kl8_bll.GetPlayByOddsSet("").Tables[0], this.lotteryId);
                    }
                    break;
                }
                case 6:
                {
                    DataSet set7 = CallBLL.cz_play_k8sc_bll.GetPlayByOddsSet("1,2,3,16,17,18,19");
                    if (((set7 != null) && (set7.Tables.Count > 0)) && (set7.Tables[0].Rows.Count > 0))
                    {
                        rs = set7.Tables[0];
                        this.ValidInput_K8SC(rs);
                        int num71 = 0;
                        foreach (DataRow row7 in rs.Rows)
                        {
                            string str16 = row7["play_name"].ToString().Trim().Replace("第一球", "1-5球");
                            cz_play_k8sc _ksc = new cz_play_k8sc();
                            double num72 = 0.0;
                            double num73 = 0.0;
                            double num74 = 0.0;
                            double num75 = 0.0;
                            double num76 = 0.0;
                            double num77 = 0.0;
                            double num78 = 0.0;
                            double num79 = 0.0;
                            double num80 = 0.0;
                            double num81 = 0.0;
                            double num82 = 0.0;
                            double num83 = 0.0;
                            double num84 = 0.0;
                            double num85 = 0.0;
                            double num86 = 0.0;
                            num72 = double.Parse(LSRequest.qq("t" + num71.ToString() + "_default_odds_1"));
                            num77 = double.Parse(LSRequest.qq("t" + num71.ToString() + "_max_odds_1"));
                            if (!this.max_odds_isopen.Equals("1"))
                            {
                                num77 = Convert.ToDouble(row7["max_odds"].ToString().Split(new char[] { '|' })[0]);
                            }
                            num82 = double.Parse(LSRequest.qq("t" + num71.ToString() + "_min_odds_1"));
                            if (num72 > num77)
                            {
                                base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能大於賠率上限！", str16), null, 400));
                                base.Response.End();
                            }
                            if (num72 < num82)
                            {
                                base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能小於賠率下限！", str16), null, 400));
                                base.Response.End();
                            }
                            if (num77 < num82)
                            {
                                base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率上限不能小於賠率下限！", str16), null, 400));
                                base.Response.End();
                            }
                            switch (num71)
                            {
                                case 5:
                                    num73 = double.Parse(LSRequest.qq("t" + num71.ToString() + "_default_odds_2"));
                                    num78 = double.Parse(LSRequest.qq("t" + num71.ToString() + "_max_odds_2"));
                                    if (!this.max_odds_isopen.Equals("1"))
                                    {
                                        num78 = Convert.ToDouble(row7["max_odds"].ToString().Split(new char[] { '|' })[1]);
                                    }
                                    num83 = double.Parse(LSRequest.qq("t" + num71.ToString() + "_min_odds_2"));
                                    if (num73 > num78)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能大於賠率上限！", str16), null, 400));
                                        base.Response.End();
                                    }
                                    if (num73 < num83)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能小於賠率下限！", str16), null, 400));
                                        base.Response.End();
                                    }
                                    if (num78 < num83)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率上限不能小於賠率下限！", str16), null, 400));
                                        base.Response.End();
                                    }
                                    _ksc.set_default_odds(num72 + "|" + num73);
                                    _ksc.set_max_odds(num77 + "|" + num78);
                                    _ksc.set_min_odds(num82 + "|" + num83);
                                    break;

                                case 6:
                                    num73 = double.Parse(LSRequest.qq("t" + num71.ToString() + "_default_odds_2"));
                                    num78 = double.Parse(LSRequest.qq("t" + num71.ToString() + "_max_odds_2"));
                                    if (!this.max_odds_isopen.Equals("1"))
                                    {
                                        num78 = Convert.ToDouble(row7["max_odds"].ToString().Split(new char[] { '|' })[1]);
                                    }
                                    num83 = double.Parse(LSRequest.qq("t" + num71.ToString() + "_min_odds_2"));
                                    num74 = double.Parse(LSRequest.qq("t" + num71.ToString() + "_default_odds_3"));
                                    num79 = double.Parse(LSRequest.qq("t" + num71.ToString() + "_max_odds_3"));
                                    if (!this.max_odds_isopen.Equals("1"))
                                    {
                                        num79 = Convert.ToDouble(row7["max_odds"].ToString().Split(new char[] { '|' })[2]);
                                    }
                                    num84 = double.Parse(LSRequest.qq("t" + num71.ToString() + "_min_odds_3"));
                                    num75 = double.Parse(LSRequest.qq("t" + num71.ToString() + "_default_odds_4"));
                                    num80 = double.Parse(LSRequest.qq("t" + num71.ToString() + "_max_odds_4"));
                                    if (!this.max_odds_isopen.Equals("1"))
                                    {
                                        num80 = Convert.ToDouble(row7["max_odds"].ToString().Split(new char[] { '|' })[3]);
                                    }
                                    num85 = double.Parse(LSRequest.qq("t" + num71.ToString() + "_min_odds_4"));
                                    num76 = double.Parse(LSRequest.qq("t" + num71.ToString() + "_default_odds_5"));
                                    num81 = double.Parse(LSRequest.qq("t" + num71.ToString() + "_max_odds_5"));
                                    if (!this.max_odds_isopen.Equals("1"))
                                    {
                                        num81 = Convert.ToDouble(row7["max_odds"].ToString().Split(new char[] { '|' })[4]);
                                    }
                                    num86 = double.Parse(LSRequest.qq("t" + num71.ToString() + "_min_odds_5"));
                                    if (num73 > num78)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能大於賠率上限！", str16), null, 400));
                                        base.Response.End();
                                    }
                                    if (num73 < num83)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能小於賠率下限！", str16), null, 400));
                                        base.Response.End();
                                    }
                                    if (num78 < num83)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率上限不能小於賠率下限！", str16), null, 400));
                                        base.Response.End();
                                    }
                                    if (num74 > num79)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能大於賠率上限！", str16), null, 400));
                                        base.Response.End();
                                    }
                                    if (num74 < num84)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能小於賠率下限！", str16), null, 400));
                                        base.Response.End();
                                    }
                                    if (num79 < num84)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率上限不能小於賠率下限！", str16), null, 400));
                                        base.Response.End();
                                    }
                                    if (num75 > num80)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能大於賠率上限！", str16), null, 400));
                                        base.Response.End();
                                    }
                                    if (num75 < num85)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能小於賠率下限！", str16), null, 400));
                                        base.Response.End();
                                    }
                                    if (num80 < num85)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率上限不能小於賠率下限！", str16), null, 400));
                                        base.Response.End();
                                    }
                                    if (num76 > num81)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能大於賠率上限！", str16), null, 400));
                                        base.Response.End();
                                    }
                                    if (num76 < num86)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能小於賠率下限！", str16), null, 400));
                                        base.Response.End();
                                    }
                                    if (num81 < num86)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率上限不能小於賠率下限！", str16), null, 400));
                                        base.Response.End();
                                    }
                                    _ksc.set_default_odds(string.Concat(new object[] { num72, "|", num73, "|", num74, "|", num75, "|", num76 }));
                                    _ksc.set_max_odds(string.Concat(new object[] { num77, "|", num78, "|", num79, "|", num80, "|", num81 }));
                                    _ksc.set_min_odds(string.Concat(new object[] { num82, "|", num83, "|", num84, "|", num85, "|", num86 }));
                                    break;

                                default:
                                    _ksc.set_default_odds(num72.ToString());
                                    _ksc.set_max_odds(num77.ToString());
                                    _ksc.set_min_odds(num82.ToString());
                                    break;
                            }
                            _ksc.set_b_diff(new decimal?(Convert.ToDecimal(LSRequest.qq("t" + num71.ToString() + "_b_diff"))));
                            _ksc.set_c_diff(new decimal?(Convert.ToDecimal(LSRequest.qq("t" + num71.ToString() + "_c_diff"))));
                            if (!this.max_odds_isopen.Equals("1"))
                            {
                                _ksc.set_b_diff(new decimal?(Convert.ToDecimal(row7["b_diff"].ToString())));
                                _ksc.set_c_diff(new decimal?(Convert.ToDecimal(row7["c_diff"].ToString())));
                            }
                            _ksc.set_downbase(new decimal?(Convert.ToDecimal(LSRequest.qq("t" + num71.ToString() + "_downbase"))));
                            _ksc.set_down_odds_rate(new decimal?(Convert.ToDecimal(LSRequest.qq("t" + num71.ToString() + "_down_odds_rate"))));
                            _ksc.set_allow_min_amount(new decimal?(decimal.Parse(row7["allow_min_amount"].ToString())));
                            string str17 = "";
                            switch (num71)
                            {
                                case 0:
                                    str17 = "1,4,7,10,13";
                                    break;

                                case 1:
                                    str17 = "2,5,8,11,14";
                                    break;

                                case 2:
                                    str17 = "3,6,9,12,15";
                                    break;

                                case 5:
                                    str17 = row7["play_id"].ToString();
                                    break;

                                case 6:
                                    str17 = "19,20,21";
                                    break;

                                default:
                                    str17 = row7["play_id"].ToString();
                                    break;
                            }
                            CallBLL.cz_play_k8sc_bll.SetPlayByOddsSet(str17, _ksc);
                            num71++;
                        }
                        base.plsd_kc_log(rs, CallBLL.cz_play_k8sc_bll.GetPlayByOddsSet("1,2,3,16,17,18,19").Tables[0], this.lotteryId);
                    }
                    break;
                }
                case 7:
                {
                    DataSet set8 = CallBLL.cz_play_pcdd_bll.GetPlayByOddsSet("71001,71002,71003,71004,71005,71006,71007,71008,71013,71014");
                    if (((set8 != null) && (set8.Tables.Count > 0)) && (set8.Tables[0].Rows.Count > 0))
                    {
                        rs = set8.Tables[0];
                        this.ValidInput_PCDD(rs);
                        int num87 = 0;
                        foreach (DataRow row8 in rs.Rows)
                        {
                            string str18 = row8["play_name"].ToString().Trim().Replace("1區大小", "1-3區大小").Replace("1區單雙", "1-3區單雙");
                            cz_play_pcdd _pcdd = new cz_play_pcdd();
                            double num88 = 0.0;
                            double num89 = 0.0;
                            double num90 = 0.0;
                            num88 = double.Parse(LSRequest.qq("t" + num87.ToString() + "_default_odds_1"));
                            num89 = double.Parse(LSRequest.qq("t" + num87.ToString() + "_max_odds_1"));
                            if (!this.max_odds_isopen.Equals("1"))
                            {
                                num89 = Convert.ToDouble(row8["max_odds"].ToString().Split(new char[] { '|' })[0]);
                            }
                            num90 = double.Parse(LSRequest.qq("t" + num87.ToString() + "_min_odds_1"));
                            if (num88 > num89)
                            {
                                base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能大於賠率上限！", str18), null, 400));
                                base.Response.End();
                            }
                            if (num88 < num90)
                            {
                                base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能小於賠率下限！", str18), null, 400));
                                base.Response.End();
                            }
                            if (num89 < num90)
                            {
                                base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率上限不能小於賠率下限！", str18), null, 400));
                                base.Response.End();
                            }
                            switch (num87)
                            {
                                case 0:
                                {
                                    string str19 = "";
                                    string str20 = "";
                                    string str21 = "";
                                    for (int j = 1; j < 14; j++)
                                    {
                                        double num92 = double.Parse(LSRequest.qq(string.Concat(new object[] { "t", num87.ToString(), "_default_odds_", j + 1 })));
                                        double num93 = double.Parse(LSRequest.qq(string.Concat(new object[] { "t", num87.ToString(), "_max_odds_", j + 1 })));
                                        if (!this.max_odds_isopen.Equals("1"))
                                        {
                                            num93 = Convert.ToDouble(row8["max_odds"].ToString().Split(new char[] { '|' })[j]);
                                        }
                                        double num94 = double.Parse(LSRequest.qq(string.Concat(new object[] { "t", num87.ToString(), "_min_odds_", j + 1 })));
                                        if (num92 > num93)
                                        {
                                            base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能大於賠率上限！", str18), null, 400));
                                            base.Response.End();
                                        }
                                        if (num92 < num94)
                                        {
                                            base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能小於賠率下限！", str18), null, 400));
                                            base.Response.End();
                                        }
                                        if (num93 < num94)
                                        {
                                            base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率上限不能小於賠率下限！", str18), null, 400));
                                            base.Response.End();
                                        }
                                        str19 = str19 + "|" + num92;
                                        str20 = str20 + "|" + num93;
                                        str21 = str21 + "|" + num94;
                                    }
                                    _pcdd.set_default_odds(num88 + str19);
                                    _pcdd.set_max_odds(num89 + str20);
                                    _pcdd.set_min_odds(num90 + str21);
                                    break;
                                }
                                case 5:
                                {
                                    double num95 = double.Parse(LSRequest.qq("t" + num87.ToString() + "_default_odds_2"));
                                    double num96 = double.Parse(LSRequest.qq("t" + num87.ToString() + "_max_odds_2"));
                                    if (!this.max_odds_isopen.Equals("1"))
                                    {
                                        num96 = Convert.ToDouble(row8["max_odds"].ToString().Split(new char[] { '|' })[1]);
                                    }
                                    double num97 = double.Parse(LSRequest.qq("t" + num87.ToString() + "_min_odds_2"));
                                    double num98 = double.Parse(LSRequest.qq("t" + num87.ToString() + "_default_odds_3"));
                                    double num99 = double.Parse(LSRequest.qq("t" + num87.ToString() + "_max_odds_3"));
                                    if (!this.max_odds_isopen.Equals("1"))
                                    {
                                        num99 = Convert.ToDouble(row8["max_odds"].ToString().Split(new char[] { '|' })[2]);
                                    }
                                    double num100 = double.Parse(LSRequest.qq("t" + num87.ToString() + "_min_odds_3"));
                                    if (num95 > num96)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能大於賠率上限！", str18), null, 400));
                                        base.Response.End();
                                    }
                                    if (num95 < num97)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能小於賠率下限！", str18), null, 400));
                                        base.Response.End();
                                    }
                                    if (num96 < num97)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率上限不能小於賠率下限！", str18), null, 400));
                                        base.Response.End();
                                    }
                                    if (num98 > num99)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能大於賠率上限！", str18), null, 400));
                                        base.Response.End();
                                    }
                                    if (num98 < num100)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能小於賠率下限！", str18), null, 400));
                                        base.Response.End();
                                    }
                                    if (num99 < num100)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率上限不能小於賠率下限！", str18), null, 400));
                                        base.Response.End();
                                    }
                                    _pcdd.set_default_odds(string.Concat(new object[] { num88, "|", num95, "|", num98 }));
                                    _pcdd.set_max_odds(string.Concat(new object[] { num89, "|", num96, "|", num99 }));
                                    _pcdd.set_min_odds(string.Concat(new object[] { num90, "|", num97, "|", num100 }));
                                    break;
                                }
                                default:
                                    _pcdd.set_default_odds(num88.ToString());
                                    _pcdd.set_max_odds(num89.ToString());
                                    _pcdd.set_min_odds(num90.ToString());
                                    break;
                            }
                            _pcdd.set_b_diff(new decimal?(Convert.ToDecimal(LSRequest.qq("t" + num87.ToString() + "_b_diff"))));
                            _pcdd.set_c_diff(new decimal?(Convert.ToDecimal(LSRequest.qq("t" + num87.ToString() + "_c_diff"))));
                            if (!this.max_odds_isopen.Equals("1"))
                            {
                                _pcdd.set_b_diff(new decimal?(Convert.ToDecimal(row8["b_diff"].ToString())));
                                _pcdd.set_c_diff(new decimal?(Convert.ToDecimal(row8["c_diff"].ToString())));
                            }
                            _pcdd.set_downbase(new decimal?(Convert.ToDecimal(LSRequest.qq("t" + num87.ToString() + "_downbase"))));
                            _pcdd.set_down_odds_rate(new decimal?(Convert.ToDecimal(LSRequest.qq("t" + num87.ToString() + "_down_odds_rate"))));
                            _pcdd.set_allow_min_amount(new decimal?(decimal.Parse(row8["allow_min_amount"].ToString())));
                            string str22 = "";
                            switch (num87)
                            {
                                case 6:
                                    str22 = "71007,71009,71011";
                                    break;

                                case 7:
                                    str22 = "71008,71010,71012";
                                    break;

                                default:
                                    str22 = row8["play_id"].ToString();
                                    break;
                            }
                            CallBLL.cz_play_pcdd_bll.SetPlayByOddsSet(str22, _pcdd);
                            num87++;
                        }
                        base.plsd_kc_log(rs, CallBLL.cz_play_pcdd_bll.GetPlayByOddsSet("71001,71002,71003,71004,71005,71006,71007,71008,71013,71014").Tables[0], this.lotteryId);
                    }
                    break;
                }
                case 8:
                {
                    DataSet set10 = CallBLL.cz_play_pkbjl_bll.GetPlayByOddsSet("81001,81002,81003,81004");
                    if (((set10 != null) && (set10.Tables.Count > 0)) && (set10.Tables[0].Rows.Count > 0))
                    {
                        rs = set10.Tables[0];
                        DataTable playTypeValue = CallBLL.cz_play_pkbjl_bll.GetPlayTypeValue("82005,82006");
                        this.ValidInput_PKBJL(rs);
                        int num117 = 0;
                        foreach (DataRow row10 in rs.Rows)
                        {
                            string str26;
                            double num118 = 0.0;
                            double num119 = 0.0;
                            string str25 = row10["play_name"].ToString().Trim();
                            cz_play_pkbjl _pkbjl = new cz_play_pkbjl();
                            double num120 = 0.0;
                            double num121 = 0.0;
                            double num122 = 0.0;
                            double num123 = 0.0;
                            double num124 = 0.0;
                            double num125 = 0.0;
                            double num126 = 0.0;
                            double num127 = 0.0;
                            double num128 = 0.0;
                            num120 = double.Parse(LSRequest.qq("t" + num117.ToString() + "_default_odds_1"));
                            num123 = double.Parse(LSRequest.qq("t" + num117.ToString() + "_max_odds_1"));
                            if (!this.max_odds_isopen.Equals("1"))
                            {
                                num123 = Convert.ToDouble(row10["max_odds"].ToString().Split(new char[] { '|' })[0]);
                            }
                            num126 = double.Parse(LSRequest.qq("t" + num117.ToString() + "_min_odds_1"));
                            if (num120 > num123)
                            {
                                base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能大於賠率上限！", str25), null, 400));
                                base.Response.End();
                            }
                            if (num120 < num126)
                            {
                                base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能小於賠率下限！", str25), null, 400));
                                base.Response.End();
                            }
                            if (num123 < num126)
                            {
                                base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率上限不能小於賠率下限！", str25), null, 400));
                                base.Response.End();
                            }
                            else
                            {
                                switch (num117)
                                {
                                    case 0:
                                        num121 = double.Parse(LSRequest.qq("t" + num117.ToString() + "_default_odds_2"));
                                        num124 = double.Parse(LSRequest.qq("t" + num117.ToString() + "_max_odds_2"));
                                        if (!this.max_odds_isopen.Equals("1"))
                                        {
                                            num124 = Convert.ToDouble(row10["max_odds"].ToString().Split(new char[] { '|' })[1]);
                                        }
                                        num127 = double.Parse(LSRequest.qq("t" + num117.ToString() + "_min_odds_2"));
                                        if (num121 > num124)
                                        {
                                            base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能大於賠率上限！", str25), null, 400));
                                            base.Response.End();
                                        }
                                        if (num121 < num127)
                                        {
                                            base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能小於賠率下限！", str25), null, 400));
                                            base.Response.End();
                                        }
                                        if (num124 < num127)
                                        {
                                            base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率上限不能小於賠率下限！", str25), null, 400));
                                            base.Response.End();
                                        }
                                        _pkbjl.set_default_odds(num120 + "|" + num121);
                                        _pkbjl.set_max_odds(num123 + "|" + num124);
                                        _pkbjl.set_min_odds(num126 + "|" + num127);
                                        goto Label_6912;

                                    case 3:
                                        num121 = double.Parse(LSRequest.qq("t" + num117.ToString() + "_default_odds_2"));
                                        num124 = double.Parse(LSRequest.qq("t" + num117.ToString() + "_max_odds_2"));
                                        if (!this.max_odds_isopen.Equals("1"))
                                        {
                                            num124 = Convert.ToDouble(row10["max_odds"].ToString().Split(new char[] { '|' })[1]);
                                        }
                                        num127 = double.Parse(LSRequest.qq("t" + num117.ToString() + "_min_odds_2"));
                                        num122 = double.Parse(LSRequest.qq("t" + num117.ToString() + "_default_odds_3"));
                                        num125 = double.Parse(LSRequest.qq("t" + num117.ToString() + "_max_odds_3"));
                                        if (!this.max_odds_isopen.Equals("1"))
                                        {
                                            num125 = Convert.ToDouble(row10["max_odds"].ToString().Split(new char[] { '|' })[2]);
                                        }
                                        num128 = double.Parse(LSRequest.qq("t" + num117.ToString() + "_min_odds_3"));
                                        try
                                        {
                                            num118 = double.Parse(LSRequest.qq("t" + num117.ToString() + "_playtype_odds_82005"));
                                        }
                                        catch
                                        {
                                            base.Response.Write(base.ShowDialogBox(string.Format("‘{0} 免傭差’ 賠率錯誤！", "莊"), null, 400));
                                            base.Response.End();
                                        }
                                        try
                                        {
                                            num119 = double.Parse(LSRequest.qq("t" + num117.ToString() + "_playtype_odds_82006"));
                                        }
                                        catch
                                        {
                                            base.Response.Write(base.ShowDialogBox(string.Format("‘{0} 免傭差’ 賠率錯誤！", "閑"), null, 400));
                                            base.Response.End();
                                        }
                                        if (num121 > num124)
                                        {
                                            base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能大於賠率上限！", str25), null, 400));
                                            base.Response.End();
                                        }
                                        if (num121 < num127)
                                        {
                                            base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能小於賠率下限！", str25), null, 400));
                                            base.Response.End();
                                        }
                                        if (num124 < num127)
                                        {
                                            base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率上限不能小於賠率下限！", str25), null, 400));
                                            base.Response.End();
                                        }
                                        if (num122 > num125)
                                        {
                                            base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能大於賠率上限！", str25), null, 400));
                                            base.Response.End();
                                        }
                                        if (num122 < num128)
                                        {
                                            base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能小於賠率下限！", str25), null, 400));
                                            base.Response.End();
                                        }
                                        if (num125 < num128)
                                        {
                                            base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率上限不能小於賠率下限！", str25), null, 400));
                                            base.Response.End();
                                        }
                                        _pkbjl.set_default_odds(string.Concat(new object[] { num120, "|", num121, "|", num122 }));
                                        _pkbjl.set_max_odds(string.Concat(new object[] { num123, "|", num124, "|", num125 }));
                                        _pkbjl.set_min_odds(string.Concat(new object[] { num126, "|", num127, "|", num128 }));
                                        goto Label_6912;
                                }
                                _pkbjl.set_default_odds(num120.ToString());
                                _pkbjl.set_max_odds(num123.ToString());
                                _pkbjl.set_min_odds(num126.ToString());
                            }
                        Label_6912:
                            _pkbjl.set_b_diff(new decimal?(Convert.ToDecimal(LSRequest.qq("t" + num117.ToString() + "_b_diff"))));
                            _pkbjl.set_c_diff(new decimal?(Convert.ToDecimal(LSRequest.qq("t" + num117.ToString() + "_c_diff"))));
                            if (!this.max_odds_isopen.Equals("1"))
                            {
                                _pkbjl.set_b_diff(new decimal?(Convert.ToDecimal(row10["b_diff"].ToString())));
                                _pkbjl.set_c_diff(new decimal?(Convert.ToDecimal(row10["c_diff"].ToString())));
                            }
                            _pkbjl.set_downbase(new decimal?(Convert.ToDecimal(row10["downbase"].ToString())));
                            _pkbjl.set_down_odds_rate(new decimal?(Convert.ToDecimal(row10["down_odds_rate"].ToString())));
                            _pkbjl.set_allow_min_amount(new decimal?(decimal.Parse(row10["allow_min_amount"].ToString())));
                            str26 = str26 = row10["play_id"].ToString();
                            CallBLL.cz_play_pkbjl_bll.SetPlayByOddsSet(str26, _pkbjl);
                            List<cz_odds_pkbjl_playtype> list = new List<cz_odds_pkbjl_playtype>();
                            cz_odds_pkbjl_playtype item = new cz_odds_pkbjl_playtype();
                            item.set_odds_id(0x14055);
                            item.set_wt_value(num118);
                            list.Add(item);
                            cz_odds_pkbjl_playtype _playtype2 = new cz_odds_pkbjl_playtype();
                            _playtype2.set_odds_id(0x14056);
                            _playtype2.set_wt_value(num119);
                            list.Add(_playtype2);
                            if (num117.Equals(3))
                            {
                                CallBLL.cz_play_pkbjl_bll.UpdatePlayTypeValue(list);
                            }
                            num117++;
                        }
                        base.plsd_kc_log(rs, CallBLL.cz_play_pkbjl_bll.GetPlayByOddsSet("81001,81002,81003,81004").Tables[0], this.lotteryId);
                        base.plsd_kc_log_pkbjl(playTypeValue, CallBLL.cz_play_pkbjl_bll.GetPlayTypeValue("82005,82006"));
                    }
                    break;
                }
                case 9:
                {
                    DataSet set9 = CallBLL.cz_play_xyft5_bll.GetPlayByOddsSet("1,2,3,4,36,37,38");
                    if (((set9 != null) && (set9.Tables.Count > 0)) && (set9.Tables[0].Rows.Count > 0))
                    {
                        rs = set9.Tables[0];
                        this.ValidInput_XYFT5(rs);
                        int num101 = 0;
                        foreach (DataRow row9 in rs.Rows)
                        {
                            string str23 = row9["play_name"].ToString().Trim().Replace("冠軍龍虎", "1-5名龍虎").Replace("冠軍", "1-10名");
                            cz_play_xyft5 _xyft = new cz_play_xyft5();
                            double num102 = 0.0;
                            double num103 = 0.0;
                            double num104 = 0.0;
                            double num105 = 0.0;
                            double num106 = 0.0;
                            double num107 = 0.0;
                            double num108 = 0.0;
                            double num109 = 0.0;
                            double num110 = 0.0;
                            double num111 = 0.0;
                            double num112 = 0.0;
                            double num113 = 0.0;
                            double num114 = 0.0;
                            double num115 = 0.0;
                            double num116 = 0.0;
                            num102 = double.Parse(LSRequest.qq("t" + num101.ToString() + "_default_odds_1"));
                            num107 = double.Parse(LSRequest.qq("t" + num101.ToString() + "_max_odds_1"));
                            if (!this.max_odds_isopen.Equals("1"))
                            {
                                num107 = Convert.ToDouble(row9["max_odds"].ToString().Split(new char[] { '|' })[0]);
                            }
                            num112 = double.Parse(LSRequest.qq("t" + num101.ToString() + "_min_odds_1"));
                            if (num102 > num107)
                            {
                                base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能大於賠率上限！", str23), null, 400));
                                base.Response.End();
                            }
                            if (num102 < num112)
                            {
                                base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能小於賠率下限！", str23), null, 400));
                                base.Response.End();
                            }
                            if (num107 < num112)
                            {
                                base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率上限不能小於賠率下限！", str23), null, 400));
                                base.Response.End();
                            }
                            switch (num101)
                            {
                                case 4:
                                    num103 = double.Parse(LSRequest.qq("t" + num101.ToString() + "_default_odds_2"));
                                    num108 = double.Parse(LSRequest.qq("t" + num101.ToString() + "_max_odds_2"));
                                    if (!this.max_odds_isopen.Equals("1"))
                                    {
                                        num108 = Convert.ToDouble(row9["max_odds"].ToString().Split(new char[] { '|' })[1]);
                                    }
                                    num113 = double.Parse(LSRequest.qq("t" + num101.ToString() + "_min_odds_2"));
                                    num104 = double.Parse(LSRequest.qq("t" + num101.ToString() + "_default_odds_3"));
                                    num109 = double.Parse(LSRequest.qq("t" + num101.ToString() + "_max_odds_3"));
                                    if (!this.max_odds_isopen.Equals("1"))
                                    {
                                        num109 = Convert.ToDouble(row9["max_odds"].ToString().Split(new char[] { '|' })[2]);
                                    }
                                    num114 = double.Parse(LSRequest.qq("t" + num101.ToString() + "_min_odds_3"));
                                    num105 = double.Parse(LSRequest.qq("t" + num101.ToString() + "_default_odds_4"));
                                    num110 = double.Parse(LSRequest.qq("t" + num101.ToString() + "_max_odds_4"));
                                    if (!this.max_odds_isopen.Equals("1"))
                                    {
                                        num110 = Convert.ToDouble(row9["max_odds"].ToString().Split(new char[] { '|' })[3]);
                                    }
                                    num115 = double.Parse(LSRequest.qq("t" + num101.ToString() + "_min_odds_4"));
                                    num106 = double.Parse(LSRequest.qq("t" + num101.ToString() + "_default_odds_5"));
                                    num111 = double.Parse(LSRequest.qq("t" + num101.ToString() + "_max_odds_5"));
                                    if (!this.max_odds_isopen.Equals("1"))
                                    {
                                        num111 = Convert.ToDouble(row9["max_odds"].ToString().Split(new char[] { '|' })[4]);
                                    }
                                    num116 = double.Parse(LSRequest.qq("t" + num101.ToString() + "_min_odds_5"));
                                    if (num103 > num108)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能大於賠率上限！", str23), null, 400));
                                        base.Response.End();
                                    }
                                    if (num103 < num113)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能小於賠率下限！", str23), null, 400));
                                        base.Response.End();
                                    }
                                    if (num108 < num113)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率上限不能小於賠率下限！", str23), null, 400));
                                        base.Response.End();
                                    }
                                    if (num104 > num109)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能大於賠率上限！", str23), null, 400));
                                        base.Response.End();
                                    }
                                    if (num104 < num114)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能小於賠率下限！", str23), null, 400));
                                        base.Response.End();
                                    }
                                    if (num109 < num114)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率上限不能小於賠率下限！", str23), null, 400));
                                        base.Response.End();
                                    }
                                    if (num105 > num110)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能大於賠率上限！", str23), null, 400));
                                        base.Response.End();
                                    }
                                    if (num105 < num115)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能小於賠率下限！", str23), null, 400));
                                        base.Response.End();
                                    }
                                    if (num110 < num115)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率上限不能小於賠率下限！", str23), null, 400));
                                        base.Response.End();
                                    }
                                    if (num106 > num111)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能大於賠率上限！", str23), null, 400));
                                        base.Response.End();
                                    }
                                    if (num106 < num116)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能小於賠率下限！", str23), null, 400));
                                        base.Response.End();
                                    }
                                    if (num111 < num116)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率上限不能小於賠率下限！", str23), null, 400));
                                        base.Response.End();
                                    }
                                    _xyft.set_default_odds(string.Concat(new object[] { num102, "|", num103, "|", num104, "|", num105, "|", num106 }));
                                    _xyft.set_max_odds(string.Concat(new object[] { num107, "|", num108, "|", num109, "|", num110, "|", num111 }));
                                    _xyft.set_min_odds(string.Concat(new object[] { num112, "|", num113, "|", num114, "|", num115, "|", num116 }));
                                    break;

                                case 5:
                                case 6:
                                    num103 = double.Parse(LSRequest.qq("t" + num101.ToString() + "_default_odds_2"));
                                    num108 = double.Parse(LSRequest.qq("t" + num101.ToString() + "_max_odds_2"));
                                    if (!this.max_odds_isopen.Equals("1"))
                                    {
                                        num108 = Convert.ToDouble(row9["max_odds"].ToString().Split(new char[] { '|' })[1]);
                                    }
                                    num113 = double.Parse(LSRequest.qq("t" + num101.ToString() + "_min_odds_2"));
                                    if (num103 > num108)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能大於賠率上限！", str23), null, 400));
                                        base.Response.End();
                                    }
                                    if (num103 < num113)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能小於賠率下限！", str23), null, 400));
                                        base.Response.End();
                                    }
                                    if (num108 < num113)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率上限不能小於賠率下限！", str23), null, 400));
                                        base.Response.End();
                                    }
                                    _xyft.set_default_odds(num102 + "|" + num103);
                                    _xyft.set_max_odds(num107 + "|" + num108);
                                    _xyft.set_min_odds(num112 + "|" + num113);
                                    break;

                                default:
                                    _xyft.set_default_odds(num102.ToString());
                                    _xyft.set_max_odds(num107.ToString());
                                    _xyft.set_min_odds(num112.ToString());
                                    break;
                            }
                            _xyft.set_b_diff(new decimal?(Convert.ToDecimal(LSRequest.qq("t" + num101.ToString() + "_b_diff"))));
                            _xyft.set_c_diff(new decimal?(Convert.ToDecimal(LSRequest.qq("t" + num101.ToString() + "_c_diff"))));
                            if (!this.max_odds_isopen.Equals("1"))
                            {
                                _xyft.set_b_diff(new decimal?(Convert.ToDecimal(row9["b_diff"].ToString())));
                                _xyft.set_c_diff(new decimal?(Convert.ToDecimal(row9["c_diff"].ToString())));
                            }
                            _xyft.set_downbase(new decimal?(Convert.ToDecimal(LSRequest.qq("t" + num101.ToString() + "_downbase"))));
                            _xyft.set_down_odds_rate(new decimal?(Convert.ToDecimal(LSRequest.qq("t" + num101.ToString() + "_down_odds_rate"))));
                            _xyft.set_allow_min_amount(new decimal?(decimal.Parse(row9["allow_min_amount"].ToString())));
                            string str24 = "";
                            switch (num101)
                            {
                                case 0:
                                    str24 = "1,5,9,13,17,21,24,27,30,33";
                                    break;

                                case 1:
                                    str24 = "2,6,10,14,18,22,25,28,31,34";
                                    break;

                                case 2:
                                    str24 = "3,7,11,15,19,23,26,29,32,35";
                                    break;

                                case 3:
                                    str24 = "4,8,12,16,20";
                                    break;

                                case 4:
                                    str24 = row9["play_id"].ToString();
                                    break;

                                case 5:
                                    str24 = row9["play_id"].ToString();
                                    break;

                                case 6:
                                    str24 = row9["play_id"].ToString();
                                    break;

                                default:
                                    str24 = row9["play_id"].ToString();
                                    break;
                            }
                            CallBLL.cz_play_xyft5_bll.SetPlayByOddsSet(str24, _xyft);
                            num101++;
                        }
                        base.plsd_kc_log(rs, CallBLL.cz_play_xyft5_bll.GetPlayByOddsSet("1,2,3,4,36,37,38").Tables[0], this.lotteryId);
                    }
                    break;
                }
                case 10:
                {
                    DataSet set11 = CallBLL.cz_play_jscar_bll.GetPlayByOddsSet("1,2,3,4,36,37,38");
                    if (((set11 != null) && (set11.Tables.Count > 0)) && (set11.Tables[0].Rows.Count > 0))
                    {
                        rs = set11.Tables[0];
                        this.ValidInput_JSCAR(rs);
                        int num129 = 0;
                        foreach (DataRow row11 in rs.Rows)
                        {
                            string str27 = row11["play_name"].ToString().Trim().Replace("冠軍龍虎", "1-5名龍虎").Replace("冠軍", "1-10名");
                            cz_play_jscar _jscar = new cz_play_jscar();
                            double num130 = 0.0;
                            double num131 = 0.0;
                            double num132 = 0.0;
                            double num133 = 0.0;
                            double num134 = 0.0;
                            double num135 = 0.0;
                            double num136 = 0.0;
                            double num137 = 0.0;
                            double num138 = 0.0;
                            double num139 = 0.0;
                            double num140 = 0.0;
                            double num141 = 0.0;
                            double num142 = 0.0;
                            double num143 = 0.0;
                            double num144 = 0.0;
                            num130 = double.Parse(LSRequest.qq("t" + num129.ToString() + "_default_odds_1"));
                            num135 = double.Parse(LSRequest.qq("t" + num129.ToString() + "_max_odds_1"));
                            if (!this.max_odds_isopen.Equals("1"))
                            {
                                num135 = Convert.ToDouble(row11["max_odds"].ToString().Split(new char[] { '|' })[0]);
                            }
                            num140 = double.Parse(LSRequest.qq("t" + num129.ToString() + "_min_odds_1"));
                            if (num130 > num135)
                            {
                                base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能大於賠率上限！", str27), null, 400));
                                base.Response.End();
                            }
                            if (num130 < num140)
                            {
                                base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能小於賠率下限！", str27), null, 400));
                                base.Response.End();
                            }
                            if (num135 < num140)
                            {
                                base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率上限不能小於賠率下限！", str27), null, 400));
                                base.Response.End();
                            }
                            switch (num129)
                            {
                                case 4:
                                    num131 = double.Parse(LSRequest.qq("t" + num129.ToString() + "_default_odds_2"));
                                    num136 = double.Parse(LSRequest.qq("t" + num129.ToString() + "_max_odds_2"));
                                    if (!this.max_odds_isopen.Equals("1"))
                                    {
                                        num136 = Convert.ToDouble(row11["max_odds"].ToString().Split(new char[] { '|' })[1]);
                                    }
                                    num141 = double.Parse(LSRequest.qq("t" + num129.ToString() + "_min_odds_2"));
                                    num132 = double.Parse(LSRequest.qq("t" + num129.ToString() + "_default_odds_3"));
                                    num137 = double.Parse(LSRequest.qq("t" + num129.ToString() + "_max_odds_3"));
                                    if (!this.max_odds_isopen.Equals("1"))
                                    {
                                        num137 = Convert.ToDouble(row11["max_odds"].ToString().Split(new char[] { '|' })[2]);
                                    }
                                    num142 = double.Parse(LSRequest.qq("t" + num129.ToString() + "_min_odds_3"));
                                    num133 = double.Parse(LSRequest.qq("t" + num129.ToString() + "_default_odds_4"));
                                    num138 = double.Parse(LSRequest.qq("t" + num129.ToString() + "_max_odds_4"));
                                    if (!this.max_odds_isopen.Equals("1"))
                                    {
                                        num138 = Convert.ToDouble(row11["max_odds"].ToString().Split(new char[] { '|' })[3]);
                                    }
                                    num143 = double.Parse(LSRequest.qq("t" + num129.ToString() + "_min_odds_4"));
                                    num134 = double.Parse(LSRequest.qq("t" + num129.ToString() + "_default_odds_5"));
                                    num139 = double.Parse(LSRequest.qq("t" + num129.ToString() + "_max_odds_5"));
                                    if (!this.max_odds_isopen.Equals("1"))
                                    {
                                        num139 = Convert.ToDouble(row11["max_odds"].ToString().Split(new char[] { '|' })[4]);
                                    }
                                    num144 = double.Parse(LSRequest.qq("t" + num129.ToString() + "_min_odds_5"));
                                    if (num131 > num136)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能大於賠率上限！", str27), null, 400));
                                        base.Response.End();
                                    }
                                    if (num131 < num141)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能小於賠率下限！", str27), null, 400));
                                        base.Response.End();
                                    }
                                    if (num136 < num141)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率上限不能小於賠率下限！", str27), null, 400));
                                        base.Response.End();
                                    }
                                    if (num132 > num137)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能大於賠率上限！", str27), null, 400));
                                        base.Response.End();
                                    }
                                    if (num132 < num142)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能小於賠率下限！", str27), null, 400));
                                        base.Response.End();
                                    }
                                    if (num137 < num142)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率上限不能小於賠率下限！", str27), null, 400));
                                        base.Response.End();
                                    }
                                    if (num133 > num138)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能大於賠率上限！", str27), null, 400));
                                        base.Response.End();
                                    }
                                    if (num133 < num143)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能小於賠率下限！", str27), null, 400));
                                        base.Response.End();
                                    }
                                    if (num138 < num143)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率上限不能小於賠率下限！", str27), null, 400));
                                        base.Response.End();
                                    }
                                    if (num134 > num139)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能大於賠率上限！", str27), null, 400));
                                        base.Response.End();
                                    }
                                    if (num134 < num144)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能小於賠率下限！", str27), null, 400));
                                        base.Response.End();
                                    }
                                    if (num139 < num144)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率上限不能小於賠率下限！", str27), null, 400));
                                        base.Response.End();
                                    }
                                    _jscar.set_default_odds(string.Concat(new object[] { num130, "|", num131, "|", num132, "|", num133, "|", num134 }));
                                    _jscar.set_max_odds(string.Concat(new object[] { num135, "|", num136, "|", num137, "|", num138, "|", num139 }));
                                    _jscar.set_min_odds(string.Concat(new object[] { num140, "|", num141, "|", num142, "|", num143, "|", num144 }));
                                    break;

                                case 5:
                                case 6:
                                    num131 = double.Parse(LSRequest.qq("t" + num129.ToString() + "_default_odds_2"));
                                    num136 = double.Parse(LSRequest.qq("t" + num129.ToString() + "_max_odds_2"));
                                    if (!this.max_odds_isopen.Equals("1"))
                                    {
                                        num136 = Convert.ToDouble(row11["max_odds"].ToString().Split(new char[] { '|' })[1]);
                                    }
                                    num141 = double.Parse(LSRequest.qq("t" + num129.ToString() + "_min_odds_2"));
                                    if (num131 > num136)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能大於賠率上限！", str27), null, 400));
                                        base.Response.End();
                                    }
                                    if (num131 < num141)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能小於賠率下限！", str27), null, 400));
                                        base.Response.End();
                                    }
                                    if (num136 < num141)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率上限不能小於賠率下限！", str27), null, 400));
                                        base.Response.End();
                                    }
                                    _jscar.set_default_odds(num130 + "|" + num131);
                                    _jscar.set_max_odds(num135 + "|" + num136);
                                    _jscar.set_min_odds(num140 + "|" + num141);
                                    break;

                                default:
                                    _jscar.set_default_odds(num130.ToString());
                                    _jscar.set_max_odds(num135.ToString());
                                    _jscar.set_min_odds(num140.ToString());
                                    break;
                            }
                            _jscar.set_b_diff(new decimal?(Convert.ToDecimal(LSRequest.qq("t" + num129.ToString() + "_b_diff"))));
                            _jscar.set_c_diff(new decimal?(Convert.ToDecimal(LSRequest.qq("t" + num129.ToString() + "_c_diff"))));
                            if (!this.max_odds_isopen.Equals("1"))
                            {
                                _jscar.set_b_diff(new decimal?(Convert.ToDecimal(row11["b_diff"].ToString())));
                                _jscar.set_c_diff(new decimal?(Convert.ToDecimal(row11["c_diff"].ToString())));
                            }
                            _jscar.set_downbase(new decimal?(Convert.ToDecimal(LSRequest.qq("t" + num129.ToString() + "_downbase"))));
                            _jscar.set_down_odds_rate(new decimal?(Convert.ToDecimal(LSRequest.qq("t" + num129.ToString() + "_down_odds_rate"))));
                            _jscar.set_allow_min_amount(new decimal?(decimal.Parse(row11["allow_min_amount"].ToString())));
                            string str28 = "";
                            switch (num129)
                            {
                                case 0:
                                    str28 = "1,5,9,13,17,21,24,27,30,33";
                                    break;

                                case 1:
                                    str28 = "2,6,10,14,18,22,25,28,31,34";
                                    break;

                                case 2:
                                    str28 = "3,7,11,15,19,23,26,29,32,35";
                                    break;

                                case 3:
                                    str28 = "4,8,12,16,20";
                                    break;

                                case 4:
                                    str28 = row11["play_id"].ToString();
                                    break;

                                case 5:
                                    str28 = row11["play_id"].ToString();
                                    break;

                                case 6:
                                    str28 = row11["play_id"].ToString();
                                    break;

                                default:
                                    str28 = row11["play_id"].ToString();
                                    break;
                            }
                            CallBLL.cz_play_jscar_bll.SetPlayByOddsSet(str28, _jscar);
                            num129++;
                        }
                        base.plsd_kc_log(rs, CallBLL.cz_play_jscar_bll.GetPlayByOddsSet("1,2,3,4,36,37,38").Tables[0], this.lotteryId);
                    }
                    break;
                }
                case 11:
                {
                    DataSet set12 = CallBLL.cz_play_speed5_bll.GetPlayByOddsSet("1,2,3,16,17,18,19");
                    if (((set12 != null) && (set12.Tables.Count > 0)) && (set12.Tables[0].Rows.Count > 0))
                    {
                        rs = set12.Tables[0];
                        this.ValidInput_SPEED5(rs);
                        int num145 = 0;
                        foreach (DataRow row12 in rs.Rows)
                        {
                            string str29 = row12["play_name"].ToString().Trim().Replace("第一球", "1-5球");
                            cz_play_speed5 _speed = new cz_play_speed5();
                            double num146 = 0.0;
                            double num147 = 0.0;
                            double num148 = 0.0;
                            double num149 = 0.0;
                            double num150 = 0.0;
                            double num151 = 0.0;
                            double num152 = 0.0;
                            double num153 = 0.0;
                            double num154 = 0.0;
                            double num155 = 0.0;
                            double num156 = 0.0;
                            double num157 = 0.0;
                            double num158 = 0.0;
                            double num159 = 0.0;
                            double num160 = 0.0;
                            num146 = double.Parse(LSRequest.qq("t" + num145.ToString() + "_default_odds_1"));
                            num151 = double.Parse(LSRequest.qq("t" + num145.ToString() + "_max_odds_1"));
                            if (!this.max_odds_isopen.Equals("1"))
                            {
                                num151 = Convert.ToDouble(row12["max_odds"].ToString().Split(new char[] { '|' })[0]);
                            }
                            num156 = double.Parse(LSRequest.qq("t" + num145.ToString() + "_min_odds_1"));
                            if (num146 > num151)
                            {
                                base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能大於賠率上限！", str29), null, 400));
                                base.Response.End();
                            }
                            if (num146 < num156)
                            {
                                base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能小於賠率下限！", str29), null, 400));
                                base.Response.End();
                            }
                            if (num151 < num156)
                            {
                                base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率上限不能小於賠率下限！", str29), null, 400));
                                base.Response.End();
                            }
                            switch (num145)
                            {
                                case 5:
                                    num147 = double.Parse(LSRequest.qq("t" + num145.ToString() + "_default_odds_2"));
                                    num152 = double.Parse(LSRequest.qq("t" + num145.ToString() + "_max_odds_2"));
                                    if (!this.max_odds_isopen.Equals("1"))
                                    {
                                        num152 = Convert.ToDouble(row12["max_odds"].ToString().Split(new char[] { '|' })[1]);
                                    }
                                    num157 = double.Parse(LSRequest.qq("t" + num145.ToString() + "_min_odds_2"));
                                    if (num147 > num152)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能大於賠率上限！", str29), null, 400));
                                        base.Response.End();
                                    }
                                    if (num147 < num157)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能小於賠率下限！", str29), null, 400));
                                        base.Response.End();
                                    }
                                    if (num152 < num157)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率上限不能小於賠率下限！", str29), null, 400));
                                        base.Response.End();
                                    }
                                    _speed.set_default_odds(num146 + "|" + num147);
                                    _speed.set_max_odds(num151 + "|" + num152);
                                    _speed.set_min_odds(num156 + "|" + num157);
                                    break;

                                case 6:
                                    num147 = double.Parse(LSRequest.qq("t" + num145.ToString() + "_default_odds_2"));
                                    num152 = double.Parse(LSRequest.qq("t" + num145.ToString() + "_max_odds_2"));
                                    if (!this.max_odds_isopen.Equals("1"))
                                    {
                                        num152 = Convert.ToDouble(row12["max_odds"].ToString().Split(new char[] { '|' })[1]);
                                    }
                                    num157 = double.Parse(LSRequest.qq("t" + num145.ToString() + "_min_odds_2"));
                                    num148 = double.Parse(LSRequest.qq("t" + num145.ToString() + "_default_odds_3"));
                                    num153 = double.Parse(LSRequest.qq("t" + num145.ToString() + "_max_odds_3"));
                                    if (!this.max_odds_isopen.Equals("1"))
                                    {
                                        num153 = Convert.ToDouble(row12["max_odds"].ToString().Split(new char[] { '|' })[2]);
                                    }
                                    num158 = double.Parse(LSRequest.qq("t" + num145.ToString() + "_min_odds_3"));
                                    num149 = double.Parse(LSRequest.qq("t" + num145.ToString() + "_default_odds_4"));
                                    num154 = double.Parse(LSRequest.qq("t" + num145.ToString() + "_max_odds_4"));
                                    if (!this.max_odds_isopen.Equals("1"))
                                    {
                                        num154 = Convert.ToDouble(row12["max_odds"].ToString().Split(new char[] { '|' })[3]);
                                    }
                                    num159 = double.Parse(LSRequest.qq("t" + num145.ToString() + "_min_odds_4"));
                                    num150 = double.Parse(LSRequest.qq("t" + num145.ToString() + "_default_odds_5"));
                                    num155 = double.Parse(LSRequest.qq("t" + num145.ToString() + "_max_odds_5"));
                                    if (!this.max_odds_isopen.Equals("1"))
                                    {
                                        num155 = Convert.ToDouble(row12["max_odds"].ToString().Split(new char[] { '|' })[4]);
                                    }
                                    num160 = double.Parse(LSRequest.qq("t" + num145.ToString() + "_min_odds_5"));
                                    if (num147 > num152)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能大於賠率上限！", str29), null, 400));
                                        base.Response.End();
                                    }
                                    if (num147 < num157)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能小於賠率下限！", str29), null, 400));
                                        base.Response.End();
                                    }
                                    if (num152 < num157)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率上限不能小於賠率下限！", str29), null, 400));
                                        base.Response.End();
                                    }
                                    if (num148 > num153)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能大於賠率上限！", str29), null, 400));
                                        base.Response.End();
                                    }
                                    if (num148 < num158)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能小於賠率下限！", str29), null, 400));
                                        base.Response.End();
                                    }
                                    if (num153 < num158)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率上限不能小於賠率下限！", str29), null, 400));
                                        base.Response.End();
                                    }
                                    if (num149 > num154)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能大於賠率上限！", str29), null, 400));
                                        base.Response.End();
                                    }
                                    if (num149 < num159)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能小於賠率下限！", str29), null, 400));
                                        base.Response.End();
                                    }
                                    if (num154 < num159)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率上限不能小於賠率下限！", str29), null, 400));
                                        base.Response.End();
                                    }
                                    if (num150 > num155)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能大於賠率上限！", str29), null, 400));
                                        base.Response.End();
                                    }
                                    if (num150 < num160)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能小於賠率下限！", str29), null, 400));
                                        base.Response.End();
                                    }
                                    if (num155 < num160)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率上限不能小於賠率下限！", str29), null, 400));
                                        base.Response.End();
                                    }
                                    _speed.set_default_odds(string.Concat(new object[] { num146, "|", num147, "|", num148, "|", num149, "|", num150 }));
                                    _speed.set_max_odds(string.Concat(new object[] { num151, "|", num152, "|", num153, "|", num154, "|", num155 }));
                                    _speed.set_min_odds(string.Concat(new object[] { num156, "|", num157, "|", num158, "|", num159, "|", num160 }));
                                    break;

                                default:
                                    _speed.set_default_odds(num146.ToString());
                                    _speed.set_max_odds(num151.ToString());
                                    _speed.set_min_odds(num156.ToString());
                                    break;
                            }
                            _speed.set_b_diff(new decimal?(Convert.ToDecimal(LSRequest.qq("t" + num145.ToString() + "_b_diff"))));
                            _speed.set_c_diff(new decimal?(Convert.ToDecimal(LSRequest.qq("t" + num145.ToString() + "_c_diff"))));
                            if (!this.max_odds_isopen.Equals("1"))
                            {
                                _speed.set_b_diff(new decimal?(Convert.ToDecimal(row12["b_diff"].ToString())));
                                _speed.set_c_diff(new decimal?(Convert.ToDecimal(row12["c_diff"].ToString())));
                            }
                            _speed.set_downbase(new decimal?(Convert.ToDecimal(LSRequest.qq("t" + num145.ToString() + "_downbase"))));
                            _speed.set_down_odds_rate(new decimal?(Convert.ToDecimal(LSRequest.qq("t" + num145.ToString() + "_down_odds_rate"))));
                            _speed.set_allow_min_amount(new decimal?(decimal.Parse(row12["allow_min_amount"].ToString())));
                            string str30 = "";
                            switch (num145)
                            {
                                case 0:
                                    str30 = "1,4,7,10,13";
                                    break;

                                case 1:
                                    str30 = "2,5,8,11,14";
                                    break;

                                case 2:
                                    str30 = "3,6,9,12,15";
                                    break;

                                case 5:
                                    str30 = row12["play_id"].ToString();
                                    break;

                                case 6:
                                    str30 = "19,20,21";
                                    break;

                                default:
                                    str30 = row12["play_id"].ToString();
                                    break;
                            }
                            CallBLL.cz_play_speed5_bll.SetPlayByOddsSet(str30, _speed);
                            num145++;
                        }
                        base.plsd_kc_log(rs, CallBLL.cz_play_speed5_bll.GetPlayByOddsSet("1,2,3,16,17,18,19").Tables[0], this.lotteryId);
                    }
                    break;
                }
                case 12:
                {
                    DataSet set14 = CallBLL.cz_play_jspk10_bll.GetPlayByOddsSet("1,2,3,4,36,37,38");
                    if (((set14 != null) && (set14.Tables.Count > 0)) && (set14.Tables[0].Rows.Count > 0))
                    {
                        rs = set14.Tables[0];
                        this.ValidInput_JSPK10(rs);
                        int num177 = 0;
                        foreach (DataRow row14 in rs.Rows)
                        {
                            string str33 = row14["play_name"].ToString().Trim().Replace("冠軍龍虎", "1-5名龍虎").Replace("冠軍", "1-10名");
                            cz_play_jspk10 _jspk = new cz_play_jspk10();
                            double num178 = 0.0;
                            double num179 = 0.0;
                            double num180 = 0.0;
                            double num181 = 0.0;
                            double num182 = 0.0;
                            double num183 = 0.0;
                            double num184 = 0.0;
                            double num185 = 0.0;
                            double num186 = 0.0;
                            double num187 = 0.0;
                            double num188 = 0.0;
                            double num189 = 0.0;
                            double num190 = 0.0;
                            double num191 = 0.0;
                            double num192 = 0.0;
                            num178 = double.Parse(LSRequest.qq("t" + num177.ToString() + "_default_odds_1"));
                            num183 = double.Parse(LSRequest.qq("t" + num177.ToString() + "_max_odds_1"));
                            if (!this.max_odds_isopen.Equals("1"))
                            {
                                num183 = Convert.ToDouble(row14["max_odds"].ToString().Split(new char[] { '|' })[0]);
                            }
                            num188 = double.Parse(LSRequest.qq("t" + num177.ToString() + "_min_odds_1"));
                            if (num178 > num183)
                            {
                                base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能大於賠率上限！", str33), null, 400));
                                base.Response.End();
                            }
                            if (num178 < num188)
                            {
                                base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能小於賠率下限！", str33), null, 400));
                                base.Response.End();
                            }
                            if (num183 < num188)
                            {
                                base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率上限不能小於賠率下限！", str33), null, 400));
                                base.Response.End();
                            }
                            switch (num177)
                            {
                                case 4:
                                    num179 = double.Parse(LSRequest.qq("t" + num177.ToString() + "_default_odds_2"));
                                    num184 = double.Parse(LSRequest.qq("t" + num177.ToString() + "_max_odds_2"));
                                    if (!this.max_odds_isopen.Equals("1"))
                                    {
                                        num184 = Convert.ToDouble(row14["max_odds"].ToString().Split(new char[] { '|' })[1]);
                                    }
                                    num189 = double.Parse(LSRequest.qq("t" + num177.ToString() + "_min_odds_2"));
                                    num180 = double.Parse(LSRequest.qq("t" + num177.ToString() + "_default_odds_3"));
                                    num185 = double.Parse(LSRequest.qq("t" + num177.ToString() + "_max_odds_3"));
                                    if (!this.max_odds_isopen.Equals("1"))
                                    {
                                        num185 = Convert.ToDouble(row14["max_odds"].ToString().Split(new char[] { '|' })[2]);
                                    }
                                    num190 = double.Parse(LSRequest.qq("t" + num177.ToString() + "_min_odds_3"));
                                    num181 = double.Parse(LSRequest.qq("t" + num177.ToString() + "_default_odds_4"));
                                    num186 = double.Parse(LSRequest.qq("t" + num177.ToString() + "_max_odds_4"));
                                    if (!this.max_odds_isopen.Equals("1"))
                                    {
                                        num186 = Convert.ToDouble(row14["max_odds"].ToString().Split(new char[] { '|' })[3]);
                                    }
                                    num191 = double.Parse(LSRequest.qq("t" + num177.ToString() + "_min_odds_4"));
                                    num182 = double.Parse(LSRequest.qq("t" + num177.ToString() + "_default_odds_5"));
                                    num187 = double.Parse(LSRequest.qq("t" + num177.ToString() + "_max_odds_5"));
                                    if (!this.max_odds_isopen.Equals("1"))
                                    {
                                        num187 = Convert.ToDouble(row14["max_odds"].ToString().Split(new char[] { '|' })[4]);
                                    }
                                    num192 = double.Parse(LSRequest.qq("t" + num177.ToString() + "_min_odds_5"));
                                    if (num179 > num184)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能大於賠率上限！", str33), null, 400));
                                        base.Response.End();
                                    }
                                    if (num179 < num189)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能小於賠率下限！", str33), null, 400));
                                        base.Response.End();
                                    }
                                    if (num184 < num189)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率上限不能小於賠率下限！", str33), null, 400));
                                        base.Response.End();
                                    }
                                    if (num180 > num185)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能大於賠率上限！", str33), null, 400));
                                        base.Response.End();
                                    }
                                    if (num180 < num190)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能小於賠率下限！", str33), null, 400));
                                        base.Response.End();
                                    }
                                    if (num185 < num190)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率上限不能小於賠率下限！", str33), null, 400));
                                        base.Response.End();
                                    }
                                    if (num181 > num186)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能大於賠率上限！", str33), null, 400));
                                        base.Response.End();
                                    }
                                    if (num181 < num191)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能小於賠率下限！", str33), null, 400));
                                        base.Response.End();
                                    }
                                    if (num186 < num191)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率上限不能小於賠率下限！", str33), null, 400));
                                        base.Response.End();
                                    }
                                    if (num182 > num187)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能大於賠率上限！", str33), null, 400));
                                        base.Response.End();
                                    }
                                    if (num182 < num192)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能小於賠率下限！", str33), null, 400));
                                        base.Response.End();
                                    }
                                    if (num187 < num192)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率上限不能小於賠率下限！", str33), null, 400));
                                        base.Response.End();
                                    }
                                    _jspk.set_default_odds(string.Concat(new object[] { num178, "|", num179, "|", num180, "|", num181, "|", num182 }));
                                    _jspk.set_max_odds(string.Concat(new object[] { num183, "|", num184, "|", num185, "|", num186, "|", num187 }));
                                    _jspk.set_min_odds(string.Concat(new object[] { num188, "|", num189, "|", num190, "|", num191, "|", num192 }));
                                    break;

                                case 5:
                                case 6:
                                    num179 = double.Parse(LSRequest.qq("t" + num177.ToString() + "_default_odds_2"));
                                    num184 = double.Parse(LSRequest.qq("t" + num177.ToString() + "_max_odds_2"));
                                    if (!this.max_odds_isopen.Equals("1"))
                                    {
                                        num184 = Convert.ToDouble(row14["max_odds"].ToString().Split(new char[] { '|' })[1]);
                                    }
                                    num189 = double.Parse(LSRequest.qq("t" + num177.ToString() + "_min_odds_2"));
                                    if (num179 > num184)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能大於賠率上限！", str33), null, 400));
                                        base.Response.End();
                                    }
                                    if (num179 < num189)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能小於賠率下限！", str33), null, 400));
                                        base.Response.End();
                                    }
                                    if (num184 < num189)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率上限不能小於賠率下限！", str33), null, 400));
                                        base.Response.End();
                                    }
                                    _jspk.set_default_odds(num178 + "|" + num179);
                                    _jspk.set_max_odds(num183 + "|" + num184);
                                    _jspk.set_min_odds(num188 + "|" + num189);
                                    break;

                                default:
                                    _jspk.set_default_odds(num178.ToString());
                                    _jspk.set_max_odds(num183.ToString());
                                    _jspk.set_min_odds(num188.ToString());
                                    break;
                            }
                            _jspk.set_b_diff(new decimal?(Convert.ToDecimal(LSRequest.qq("t" + num177.ToString() + "_b_diff"))));
                            _jspk.set_c_diff(new decimal?(Convert.ToDecimal(LSRequest.qq("t" + num177.ToString() + "_c_diff"))));
                            if (!this.max_odds_isopen.Equals("1"))
                            {
                                _jspk.set_b_diff(new decimal?(Convert.ToDecimal(row14["b_diff"].ToString())));
                                _jspk.set_c_diff(new decimal?(Convert.ToDecimal(row14["c_diff"].ToString())));
                            }
                            _jspk.set_downbase(new decimal?(Convert.ToDecimal(LSRequest.qq("t" + num177.ToString() + "_downbase"))));
                            _jspk.set_down_odds_rate(new decimal?(Convert.ToDecimal(LSRequest.qq("t" + num177.ToString() + "_down_odds_rate"))));
                            _jspk.set_allow_min_amount(new decimal?(decimal.Parse(row14["allow_min_amount"].ToString())));
                            string str34 = "";
                            switch (num177)
                            {
                                case 0:
                                    str34 = "1,5,9,13,17,21,24,27,30,33";
                                    break;

                                case 1:
                                    str34 = "2,6,10,14,18,22,25,28,31,34";
                                    break;

                                case 2:
                                    str34 = "3,7,11,15,19,23,26,29,32,35";
                                    break;

                                case 3:
                                    str34 = "4,8,12,16,20";
                                    break;

                                case 4:
                                    str34 = row14["play_id"].ToString();
                                    break;

                                case 5:
                                    str34 = row14["play_id"].ToString();
                                    break;

                                case 6:
                                    str34 = row14["play_id"].ToString();
                                    break;

                                default:
                                    str34 = row14["play_id"].ToString();
                                    break;
                            }
                            CallBLL.cz_play_jspk10_bll.SetPlayByOddsSet(str34, _jspk);
                            num177++;
                        }
                        base.plsd_kc_log(rs, CallBLL.cz_play_jspk10_bll.GetPlayByOddsSet("1,2,3,4,36,37,38").Tables[0], this.lotteryId);
                    }
                    break;
                }
                case 13:
                {
                    DataSet set13 = CallBLL.cz_play_jscqsc_bll.GetPlayByOddsSet("1,2,3,16,17,18,19");
                    if (((set13 != null) && (set13.Tables.Count > 0)) && (set13.Tables[0].Rows.Count > 0))
                    {
                        rs = set13.Tables[0];
                        this.ValidInput_JSCQSC(rs);
                        int num161 = 0;
                        foreach (DataRow row13 in rs.Rows)
                        {
                            string str31 = row13["play_name"].ToString().Trim().Replace("第一球", "1-5球");
                            cz_play_jscqsc _jscqsc = new cz_play_jscqsc();
                            double num162 = 0.0;
                            double num163 = 0.0;
                            double num164 = 0.0;
                            double num165 = 0.0;
                            double num166 = 0.0;
                            double num167 = 0.0;
                            double num168 = 0.0;
                            double num169 = 0.0;
                            double num170 = 0.0;
                            double num171 = 0.0;
                            double num172 = 0.0;
                            double num173 = 0.0;
                            double num174 = 0.0;
                            double num175 = 0.0;
                            double num176 = 0.0;
                            num162 = double.Parse(LSRequest.qq("t" + num161.ToString() + "_default_odds_1"));
                            num167 = double.Parse(LSRequest.qq("t" + num161.ToString() + "_max_odds_1"));
                            if (!this.max_odds_isopen.Equals("1"))
                            {
                                num167 = Convert.ToDouble(row13["max_odds"].ToString().Split(new char[] { '|' })[0]);
                            }
                            num172 = double.Parse(LSRequest.qq("t" + num161.ToString() + "_min_odds_1"));
                            if (num162 > num167)
                            {
                                base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能大於賠率上限！", str31), null, 400));
                                base.Response.End();
                            }
                            if (num162 < num172)
                            {
                                base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能小於賠率下限！", str31), null, 400));
                                base.Response.End();
                            }
                            if (num167 < num172)
                            {
                                base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率上限不能小於賠率下限！", str31), null, 400));
                                base.Response.End();
                            }
                            switch (num161)
                            {
                                case 5:
                                    num163 = double.Parse(LSRequest.qq("t" + num161.ToString() + "_default_odds_2"));
                                    num168 = double.Parse(LSRequest.qq("t" + num161.ToString() + "_max_odds_2"));
                                    if (!this.max_odds_isopen.Equals("1"))
                                    {
                                        num168 = Convert.ToDouble(row13["max_odds"].ToString().Split(new char[] { '|' })[1]);
                                    }
                                    num173 = double.Parse(LSRequest.qq("t" + num161.ToString() + "_min_odds_2"));
                                    if (num163 > num168)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能大於賠率上限！", str31), null, 400));
                                        base.Response.End();
                                    }
                                    if (num163 < num173)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能小於賠率下限！", str31), null, 400));
                                        base.Response.End();
                                    }
                                    if (num168 < num173)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率上限不能小於賠率下限！", str31), null, 400));
                                        base.Response.End();
                                    }
                                    _jscqsc.set_default_odds(num162 + "|" + num163);
                                    _jscqsc.set_max_odds(num167 + "|" + num168);
                                    _jscqsc.set_min_odds(num172 + "|" + num173);
                                    break;

                                case 6:
                                    num163 = double.Parse(LSRequest.qq("t" + num161.ToString() + "_default_odds_2"));
                                    num168 = double.Parse(LSRequest.qq("t" + num161.ToString() + "_max_odds_2"));
                                    if (!this.max_odds_isopen.Equals("1"))
                                    {
                                        num168 = Convert.ToDouble(row13["max_odds"].ToString().Split(new char[] { '|' })[1]);
                                    }
                                    num173 = double.Parse(LSRequest.qq("t" + num161.ToString() + "_min_odds_2"));
                                    num164 = double.Parse(LSRequest.qq("t" + num161.ToString() + "_default_odds_3"));
                                    num169 = double.Parse(LSRequest.qq("t" + num161.ToString() + "_max_odds_3"));
                                    if (!this.max_odds_isopen.Equals("1"))
                                    {
                                        num169 = Convert.ToDouble(row13["max_odds"].ToString().Split(new char[] { '|' })[2]);
                                    }
                                    num174 = double.Parse(LSRequest.qq("t" + num161.ToString() + "_min_odds_3"));
                                    num165 = double.Parse(LSRequest.qq("t" + num161.ToString() + "_default_odds_4"));
                                    num170 = double.Parse(LSRequest.qq("t" + num161.ToString() + "_max_odds_4"));
                                    if (!this.max_odds_isopen.Equals("1"))
                                    {
                                        num170 = Convert.ToDouble(row13["max_odds"].ToString().Split(new char[] { '|' })[3]);
                                    }
                                    num175 = double.Parse(LSRequest.qq("t" + num161.ToString() + "_min_odds_4"));
                                    num166 = double.Parse(LSRequest.qq("t" + num161.ToString() + "_default_odds_5"));
                                    num171 = double.Parse(LSRequest.qq("t" + num161.ToString() + "_max_odds_5"));
                                    if (!this.max_odds_isopen.Equals("1"))
                                    {
                                        num171 = Convert.ToDouble(row13["max_odds"].ToString().Split(new char[] { '|' })[4]);
                                    }
                                    num176 = double.Parse(LSRequest.qq("t" + num161.ToString() + "_min_odds_5"));
                                    if (num163 > num168)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能大於賠率上限！", str31), null, 400));
                                        base.Response.End();
                                    }
                                    if (num163 < num173)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能小於賠率下限！", str31), null, 400));
                                        base.Response.End();
                                    }
                                    if (num168 < num173)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率上限不能小於賠率下限！", str31), null, 400));
                                        base.Response.End();
                                    }
                                    if (num164 > num169)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能大於賠率上限！", str31), null, 400));
                                        base.Response.End();
                                    }
                                    if (num164 < num174)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能小於賠率下限！", str31), null, 400));
                                        base.Response.End();
                                    }
                                    if (num169 < num174)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率上限不能小於賠率下限！", str31), null, 400));
                                        base.Response.End();
                                    }
                                    if (num165 > num170)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能大於賠率上限！", str31), null, 400));
                                        base.Response.End();
                                    }
                                    if (num165 < num175)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能小於賠率下限！", str31), null, 400));
                                        base.Response.End();
                                    }
                                    if (num170 < num175)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率上限不能小於賠率下限！", str31), null, 400));
                                        base.Response.End();
                                    }
                                    if (num166 > num171)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能大於賠率上限！", str31), null, 400));
                                        base.Response.End();
                                    }
                                    if (num166 < num176)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能小於賠率下限！", str31), null, 400));
                                        base.Response.End();
                                    }
                                    if (num171 < num176)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率上限不能小於賠率下限！", str31), null, 400));
                                        base.Response.End();
                                    }
                                    _jscqsc.set_default_odds(string.Concat(new object[] { num162, "|", num163, "|", num164, "|", num165, "|", num166 }));
                                    _jscqsc.set_max_odds(string.Concat(new object[] { num167, "|", num168, "|", num169, "|", num170, "|", num171 }));
                                    _jscqsc.set_min_odds(string.Concat(new object[] { num172, "|", num173, "|", num174, "|", num175, "|", num176 }));
                                    break;

                                default:
                                    _jscqsc.set_default_odds(num162.ToString());
                                    _jscqsc.set_max_odds(num167.ToString());
                                    _jscqsc.set_min_odds(num172.ToString());
                                    break;
                            }
                            _jscqsc.set_b_diff(new decimal?(Convert.ToDecimal(LSRequest.qq("t" + num161.ToString() + "_b_diff"))));
                            _jscqsc.set_c_diff(new decimal?(Convert.ToDecimal(LSRequest.qq("t" + num161.ToString() + "_c_diff"))));
                            if (!this.max_odds_isopen.Equals("1"))
                            {
                                _jscqsc.set_b_diff(new decimal?(Convert.ToDecimal(row13["b_diff"].ToString())));
                                _jscqsc.set_c_diff(new decimal?(Convert.ToDecimal(row13["c_diff"].ToString())));
                            }
                            _jscqsc.set_downbase(new decimal?(Convert.ToDecimal(LSRequest.qq("t" + num161.ToString() + "_downbase"))));
                            _jscqsc.set_down_odds_rate(new decimal?(Convert.ToDecimal(LSRequest.qq("t" + num161.ToString() + "_down_odds_rate"))));
                            _jscqsc.set_allow_min_amount(new decimal?(decimal.Parse(row13["allow_min_amount"].ToString())));
                            string str32 = "";
                            switch (num161)
                            {
                                case 0:
                                    str32 = "1,4,7,10,13";
                                    break;

                                case 1:
                                    str32 = "2,5,8,11,14";
                                    break;

                                case 2:
                                    str32 = "3,6,9,12,15";
                                    break;

                                case 5:
                                    str32 = row13["play_id"].ToString();
                                    break;

                                case 6:
                                    str32 = "19,20,21";
                                    break;

                                default:
                                    str32 = row13["play_id"].ToString();
                                    break;
                            }
                            CallBLL.cz_play_jscqsc_bll.SetPlayByOddsSet(str32, _jscqsc);
                            num161++;
                        }
                        base.plsd_kc_log(rs, CallBLL.cz_play_jscqsc_bll.GetPlayByOddsSet("1,2,3,16,17,18,19").Tables[0], this.lotteryId);
                    }
                    break;
                }
                case 14:
                {
                    DataSet set15 = CallBLL.cz_play_jssfc_bll.GetPlayByTradingSet("86,87,88,89,90,91,92,93,94,95,96,97,98,99,100,101,102,103,104,105,106,107,108,109,110,111,112,113,114,115,116,117,118,119,120,123,124,125,126,127,128,129,130,131,132,133,134,135,136");
                    if (((set15 != null) && (set15.Tables.Count > 0)) && (set15.Tables[0].Rows.Count > 0))
                    {
                        rs = set15.Tables[0];
                        this.ValidInput_JSSFC(rs);
                        int num193 = 0;
                        foreach (DataRow row15 in rs.Rows)
                        {
                            string str35 = row15["play_name"].ToString().Trim().Replace("第一球", "1-8球");
                            cz_play_jssfc _jssfc = new cz_play_jssfc();
                            double num194 = 0.0;
                            double num195 = 0.0;
                            double num196 = 0.0;
                            double num197 = 0.0;
                            double num198 = 0.0;
                            double num199 = 0.0;
                            num194 = double.Parse(LSRequest.qq("t" + num193.ToString() + "_default_odds_1"));
                            num196 = double.Parse(LSRequest.qq("t" + num193.ToString() + "_max_odds_1"));
                            if (!this.max_odds_isopen.Equals("1"))
                            {
                                num196 = Convert.ToDouble(row15["max_odds"].ToString().Split(new char[] { '|' })[0]);
                            }
                            num198 = double.Parse(LSRequest.qq("t" + num193.ToString() + "_min_odds_1"));
                            if (num194 > num196)
                            {
                                base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能大於賠率上限！", str35), null, 400));
                                base.Response.End();
                            }
                            if (num194 < num198)
                            {
                                base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能小於賠率下限！", str35), null, 400));
                                base.Response.End();
                            }
                            if (num196 < num198)
                            {
                                base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率上限不能小於賠率下限！", str35), null, 400));
                                base.Response.End();
                            }
                            if (num193 == 6)
                            {
                                num195 = double.Parse(LSRequest.qq("t" + num193.ToString() + "_default_odds_2"));
                                num197 = double.Parse(LSRequest.qq("t" + num193.ToString() + "_max_odds_2"));
                                if (!this.max_odds_isopen.Equals("1"))
                                {
                                    num197 = Convert.ToDouble(row15["max_odds"].ToString().Split(new char[] { '|' })[1]);
                                }
                                num199 = double.Parse(LSRequest.qq("t" + num193.ToString() + "_min_odds_2"));
                                if (num195 > num197)
                                {
                                    base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能大於賠率上限！", str35), null, 400));
                                    base.Response.End();
                                }
                                if (num195 < num199)
                                {
                                    base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能小於賠率下限！", str35), null, 400));
                                    base.Response.End();
                                }
                                if (num197 < num199)
                                {
                                    base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率上限不能小於賠率下限！", str35), null, 400));
                                    base.Response.End();
                                }
                                _jssfc.set_default_odds(num194 + "|" + num195);
                                _jssfc.set_max_odds(num196 + "|" + num197);
                                _jssfc.set_min_odds(num198 + "|" + num199);
                            }
                            else
                            {
                                _jssfc.set_default_odds(num194.ToString());
                                _jssfc.set_max_odds(num196.ToString());
                                _jssfc.set_min_odds(num198.ToString());
                            }
                            _jssfc.set_b_diff(new decimal?(Convert.ToDecimal(LSRequest.qq("t" + num193.ToString() + "_b_diff"))));
                            _jssfc.set_c_diff(new decimal?(Convert.ToDecimal(LSRequest.qq("t" + num193.ToString() + "_c_diff"))));
                            if (!this.max_odds_isopen.Equals("1"))
                            {
                                _jssfc.set_b_diff(new decimal?(Convert.ToDecimal(row15["b_diff"].ToString())));
                                _jssfc.set_c_diff(new decimal?(Convert.ToDecimal(row15["c_diff"].ToString())));
                            }
                            _jssfc.set_downbase(new decimal?(Convert.ToDecimal(LSRequest.qq("t" + num193.ToString() + "_downbase"))));
                            _jssfc.set_down_odds_rate(new decimal?(Convert.ToDecimal(LSRequest.qq("t" + num193.ToString() + "_down_odds_rate"))));
                            _jssfc.set_allow_min_amount(new decimal?(decimal.Parse(row15["allow_min_amount"].ToString())));
                            string str36 = "";
                            switch (num193)
                            {
                                case 0:
                                    str36 = "81,86,91,96,101,106,111,116";
                                    break;

                                case 1:
                                    str36 = "82,87,92,97,102,107,112,117";
                                    break;

                                case 2:
                                    str36 = "83,88,93,98,103,108,113,118";
                                    break;

                                case 3:
                                    str36 = "84,89,94,99,104,109,114,119";
                                    break;

                                case 4:
                                    str36 = "85,90,95,100,105,110,115,120";
                                    break;

                                case 5:
                                    str36 = "121,123,125,127,129,131,133,135";
                                    break;

                                case 6:
                                    str36 = "122,124,126,128,130,132,134,136";
                                    break;

                                default:
                                    str36 = row15["play_id"].ToString();
                                    break;
                            }
                            CallBLL.cz_play_jssfc_bll.SetPlayByOddsSet(str36, _jssfc);
                            num193++;
                        }
                        base.plsd_kc_log(rs, CallBLL.cz_play_jssfc_bll.GetPlayByTradingSet("86,87,88,89,90,91,92,93,94,95,96,97,98,99,100,101,102,103,104,105,106,107,108,109,110,111,112,113,114,115,116,117,118,119,120,123,124,125,126,127,128,129,130,131,132,133,134,135,136").Tables[0], this.lotteryId);
                    }
                    break;
                }
                case 15:
                {
                    DataSet set16 = CallBLL.cz_play_jsft2_bll.GetPlayByOddsSet("1,2,3,4,36,37,38");
                    if (((set16 != null) && (set16.Tables.Count > 0)) && (set16.Tables[0].Rows.Count > 0))
                    {
                        rs = set16.Tables[0];
                        this.ValidInput_JSFT2(rs);
                        int num200 = 0;
                        foreach (DataRow row16 in rs.Rows)
                        {
                            string str37 = row16["play_name"].ToString().Trim().Replace("冠軍龍虎", "1-5名龍虎").Replace("冠軍", "1-10名");
                            cz_play_jsft2 _jsft = new cz_play_jsft2();
                            double num201 = 0.0;
                            double num202 = 0.0;
                            double num203 = 0.0;
                            double num204 = 0.0;
                            double num205 = 0.0;
                            double num206 = 0.0;
                            double num207 = 0.0;
                            double num208 = 0.0;
                            double num209 = 0.0;
                            double num210 = 0.0;
                            double num211 = 0.0;
                            double num212 = 0.0;
                            double num213 = 0.0;
                            double num214 = 0.0;
                            double num215 = 0.0;
                            num201 = double.Parse(LSRequest.qq("t" + num200.ToString() + "_default_odds_1"));
                            num206 = double.Parse(LSRequest.qq("t" + num200.ToString() + "_max_odds_1"));
                            if (!this.max_odds_isopen.Equals("1"))
                            {
                                num206 = Convert.ToDouble(row16["max_odds"].ToString().Split(new char[] { '|' })[0]);
                            }
                            num211 = double.Parse(LSRequest.qq("t" + num200.ToString() + "_min_odds_1"));
                            if (num201 > num206)
                            {
                                base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能大於賠率上限！", str37), null, 400));
                                base.Response.End();
                            }
                            if (num201 < num211)
                            {
                                base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能小於賠率下限！", str37), null, 400));
                                base.Response.End();
                            }
                            if (num206 < num211)
                            {
                                base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率上限不能小於賠率下限！", str37), null, 400));
                                base.Response.End();
                            }
                            switch (num200)
                            {
                                case 4:
                                    num202 = double.Parse(LSRequest.qq("t" + num200.ToString() + "_default_odds_2"));
                                    num207 = double.Parse(LSRequest.qq("t" + num200.ToString() + "_max_odds_2"));
                                    if (!this.max_odds_isopen.Equals("1"))
                                    {
                                        num207 = Convert.ToDouble(row16["max_odds"].ToString().Split(new char[] { '|' })[1]);
                                    }
                                    num212 = double.Parse(LSRequest.qq("t" + num200.ToString() + "_min_odds_2"));
                                    num203 = double.Parse(LSRequest.qq("t" + num200.ToString() + "_default_odds_3"));
                                    num208 = double.Parse(LSRequest.qq("t" + num200.ToString() + "_max_odds_3"));
                                    if (!this.max_odds_isopen.Equals("1"))
                                    {
                                        num208 = Convert.ToDouble(row16["max_odds"].ToString().Split(new char[] { '|' })[2]);
                                    }
                                    num213 = double.Parse(LSRequest.qq("t" + num200.ToString() + "_min_odds_3"));
                                    num204 = double.Parse(LSRequest.qq("t" + num200.ToString() + "_default_odds_4"));
                                    num209 = double.Parse(LSRequest.qq("t" + num200.ToString() + "_max_odds_4"));
                                    if (!this.max_odds_isopen.Equals("1"))
                                    {
                                        num209 = Convert.ToDouble(row16["max_odds"].ToString().Split(new char[] { '|' })[3]);
                                    }
                                    num214 = double.Parse(LSRequest.qq("t" + num200.ToString() + "_min_odds_4"));
                                    num205 = double.Parse(LSRequest.qq("t" + num200.ToString() + "_default_odds_5"));
                                    num210 = double.Parse(LSRequest.qq("t" + num200.ToString() + "_max_odds_5"));
                                    if (!this.max_odds_isopen.Equals("1"))
                                    {
                                        num210 = Convert.ToDouble(row16["max_odds"].ToString().Split(new char[] { '|' })[4]);
                                    }
                                    num215 = double.Parse(LSRequest.qq("t" + num200.ToString() + "_min_odds_5"));
                                    if (num202 > num207)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能大於賠率上限！", str37), null, 400));
                                        base.Response.End();
                                    }
                                    if (num202 < num212)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能小於賠率下限！", str37), null, 400));
                                        base.Response.End();
                                    }
                                    if (num207 < num212)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率上限不能小於賠率下限！", str37), null, 400));
                                        base.Response.End();
                                    }
                                    if (num203 > num208)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能大於賠率上限！", str37), null, 400));
                                        base.Response.End();
                                    }
                                    if (num203 < num213)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能小於賠率下限！", str37), null, 400));
                                        base.Response.End();
                                    }
                                    if (num208 < num213)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率上限不能小於賠率下限！", str37), null, 400));
                                        base.Response.End();
                                    }
                                    if (num204 > num209)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能大於賠率上限！", str37), null, 400));
                                        base.Response.End();
                                    }
                                    if (num204 < num214)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能小於賠率下限！", str37), null, 400));
                                        base.Response.End();
                                    }
                                    if (num209 < num214)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率上限不能小於賠率下限！", str37), null, 400));
                                        base.Response.End();
                                    }
                                    if (num205 > num210)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能大於賠率上限！", str37), null, 400));
                                        base.Response.End();
                                    }
                                    if (num205 < num215)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能小於賠率下限！", str37), null, 400));
                                        base.Response.End();
                                    }
                                    if (num210 < num215)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率上限不能小於賠率下限！", str37), null, 400));
                                        base.Response.End();
                                    }
                                    _jsft.set_default_odds(string.Concat(new object[] { num201, "|", num202, "|", num203, "|", num204, "|", num205 }));
                                    _jsft.set_max_odds(string.Concat(new object[] { num206, "|", num207, "|", num208, "|", num209, "|", num210 }));
                                    _jsft.set_min_odds(string.Concat(new object[] { num211, "|", num212, "|", num213, "|", num214, "|", num215 }));
                                    break;

                                case 5:
                                case 6:
                                    num202 = double.Parse(LSRequest.qq("t" + num200.ToString() + "_default_odds_2"));
                                    num207 = double.Parse(LSRequest.qq("t" + num200.ToString() + "_max_odds_2"));
                                    if (!this.max_odds_isopen.Equals("1"))
                                    {
                                        num207 = Convert.ToDouble(row16["max_odds"].ToString().Split(new char[] { '|' })[1]);
                                    }
                                    num212 = double.Parse(LSRequest.qq("t" + num200.ToString() + "_min_odds_2"));
                                    if (num202 > num207)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能大於賠率上限！", str37), null, 400));
                                        base.Response.End();
                                    }
                                    if (num202 < num212)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能小於賠率下限！", str37), null, 400));
                                        base.Response.End();
                                    }
                                    if (num207 < num212)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率上限不能小於賠率下限！", str37), null, 400));
                                        base.Response.End();
                                    }
                                    _jsft.set_default_odds(num201 + "|" + num202);
                                    _jsft.set_max_odds(num206 + "|" + num207);
                                    _jsft.set_min_odds(num211 + "|" + num212);
                                    break;

                                default:
                                    _jsft.set_default_odds(num201.ToString());
                                    _jsft.set_max_odds(num206.ToString());
                                    _jsft.set_min_odds(num211.ToString());
                                    break;
                            }
                            _jsft.set_b_diff(new decimal?(Convert.ToDecimal(LSRequest.qq("t" + num200.ToString() + "_b_diff"))));
                            _jsft.set_c_diff(new decimal?(Convert.ToDecimal(LSRequest.qq("t" + num200.ToString() + "_c_diff"))));
                            if (!this.max_odds_isopen.Equals("1"))
                            {
                                _jsft.set_b_diff(new decimal?(Convert.ToDecimal(row16["b_diff"].ToString())));
                                _jsft.set_c_diff(new decimal?(Convert.ToDecimal(row16["c_diff"].ToString())));
                            }
                            _jsft.set_downbase(new decimal?(Convert.ToDecimal(LSRequest.qq("t" + num200.ToString() + "_downbase"))));
                            _jsft.set_down_odds_rate(new decimal?(Convert.ToDecimal(LSRequest.qq("t" + num200.ToString() + "_down_odds_rate"))));
                            _jsft.set_allow_min_amount(new decimal?(decimal.Parse(row16["allow_min_amount"].ToString())));
                            string str38 = "";
                            switch (num200)
                            {
                                case 0:
                                    str38 = "1,5,9,13,17,21,24,27,30,33";
                                    break;

                                case 1:
                                    str38 = "2,6,10,14,18,22,25,28,31,34";
                                    break;

                                case 2:
                                    str38 = "3,7,11,15,19,23,26,29,32,35";
                                    break;

                                case 3:
                                    str38 = "4,8,12,16,20";
                                    break;

                                case 4:
                                    str38 = row16["play_id"].ToString();
                                    break;

                                case 5:
                                    str38 = row16["play_id"].ToString();
                                    break;

                                case 6:
                                    str38 = row16["play_id"].ToString();
                                    break;

                                default:
                                    str38 = row16["play_id"].ToString();
                                    break;
                            }
                            CallBLL.cz_play_jsft2_bll.SetPlayByOddsSet(str38, _jsft);
                            num200++;
                        }
                        base.plsd_kc_log(rs, CallBLL.cz_play_jsft2_bll.GetPlayByOddsSet("1,2,3,4,36,37,38").Tables[0], this.lotteryId);
                    }
                    break;
                }
                case 0x10:
                {
                    DataSet set17 = CallBLL.cz_play_car168_bll.GetPlayByOddsSet("1,2,3,4,36,37,38");
                    if (((set17 != null) && (set17.Tables.Count > 0)) && (set17.Tables[0].Rows.Count > 0))
                    {
                        rs = set17.Tables[0];
                        this.ValidInput_CAR168(rs);
                        int num216 = 0;
                        foreach (DataRow row17 in rs.Rows)
                        {
                            string str39 = row17["play_name"].ToString().Trim().Replace("冠軍龍虎", "1-5名龍虎").Replace("冠軍", "1-10名");
                            cz_play_car168 _car = new cz_play_car168();
                            double num217 = 0.0;
                            double num218 = 0.0;
                            double num219 = 0.0;
                            double num220 = 0.0;
                            double num221 = 0.0;
                            double num222 = 0.0;
                            double num223 = 0.0;
                            double num224 = 0.0;
                            double num225 = 0.0;
                            double num226 = 0.0;
                            double num227 = 0.0;
                            double num228 = 0.0;
                            double num229 = 0.0;
                            double num230 = 0.0;
                            double num231 = 0.0;
                            num217 = double.Parse(LSRequest.qq("t" + num216.ToString() + "_default_odds_1"));
                            num222 = double.Parse(LSRequest.qq("t" + num216.ToString() + "_max_odds_1"));
                            if (!this.max_odds_isopen.Equals("1"))
                            {
                                num222 = Convert.ToDouble(row17["max_odds"].ToString().Split(new char[] { '|' })[0]);
                            }
                            num227 = double.Parse(LSRequest.qq("t" + num216.ToString() + "_min_odds_1"));
                            if (num217 > num222)
                            {
                                base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能大於賠率上限！", str39), null, 400));
                                base.Response.End();
                            }
                            if (num217 < num227)
                            {
                                base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能小於賠率下限！", str39), null, 400));
                                base.Response.End();
                            }
                            if (num222 < num227)
                            {
                                base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率上限不能小於賠率下限！", str39), null, 400));
                                base.Response.End();
                            }
                            switch (num216)
                            {
                                case 4:
                                    num218 = double.Parse(LSRequest.qq("t" + num216.ToString() + "_default_odds_2"));
                                    num223 = double.Parse(LSRequest.qq("t" + num216.ToString() + "_max_odds_2"));
                                    if (!this.max_odds_isopen.Equals("1"))
                                    {
                                        num223 = Convert.ToDouble(row17["max_odds"].ToString().Split(new char[] { '|' })[1]);
                                    }
                                    num228 = double.Parse(LSRequest.qq("t" + num216.ToString() + "_min_odds_2"));
                                    num219 = double.Parse(LSRequest.qq("t" + num216.ToString() + "_default_odds_3"));
                                    num224 = double.Parse(LSRequest.qq("t" + num216.ToString() + "_max_odds_3"));
                                    if (!this.max_odds_isopen.Equals("1"))
                                    {
                                        num224 = Convert.ToDouble(row17["max_odds"].ToString().Split(new char[] { '|' })[2]);
                                    }
                                    num229 = double.Parse(LSRequest.qq("t" + num216.ToString() + "_min_odds_3"));
                                    num220 = double.Parse(LSRequest.qq("t" + num216.ToString() + "_default_odds_4"));
                                    num225 = double.Parse(LSRequest.qq("t" + num216.ToString() + "_max_odds_4"));
                                    if (!this.max_odds_isopen.Equals("1"))
                                    {
                                        num225 = Convert.ToDouble(row17["max_odds"].ToString().Split(new char[] { '|' })[3]);
                                    }
                                    num230 = double.Parse(LSRequest.qq("t" + num216.ToString() + "_min_odds_4"));
                                    num221 = double.Parse(LSRequest.qq("t" + num216.ToString() + "_default_odds_5"));
                                    num226 = double.Parse(LSRequest.qq("t" + num216.ToString() + "_max_odds_5"));
                                    if (!this.max_odds_isopen.Equals("1"))
                                    {
                                        num226 = Convert.ToDouble(row17["max_odds"].ToString().Split(new char[] { '|' })[4]);
                                    }
                                    num231 = double.Parse(LSRequest.qq("t" + num216.ToString() + "_min_odds_5"));
                                    if (num218 > num223)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能大於賠率上限！", str39), null, 400));
                                        base.Response.End();
                                    }
                                    if (num218 < num228)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能小於賠率下限！", str39), null, 400));
                                        base.Response.End();
                                    }
                                    if (num223 < num228)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率上限不能小於賠率下限！", str39), null, 400));
                                        base.Response.End();
                                    }
                                    if (num219 > num224)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能大於賠率上限！", str39), null, 400));
                                        base.Response.End();
                                    }
                                    if (num219 < num229)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能小於賠率下限！", str39), null, 400));
                                        base.Response.End();
                                    }
                                    if (num224 < num229)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率上限不能小於賠率下限！", str39), null, 400));
                                        base.Response.End();
                                    }
                                    if (num220 > num225)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能大於賠率上限！", str39), null, 400));
                                        base.Response.End();
                                    }
                                    if (num220 < num230)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能小於賠率下限！", str39), null, 400));
                                        base.Response.End();
                                    }
                                    if (num225 < num230)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率上限不能小於賠率下限！", str39), null, 400));
                                        base.Response.End();
                                    }
                                    if (num221 > num226)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能大於賠率上限！", str39), null, 400));
                                        base.Response.End();
                                    }
                                    if (num221 < num231)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能小於賠率下限！", str39), null, 400));
                                        base.Response.End();
                                    }
                                    if (num226 < num231)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率上限不能小於賠率下限！", str39), null, 400));
                                        base.Response.End();
                                    }
                                    _car.set_default_odds(string.Concat(new object[] { num217, "|", num218, "|", num219, "|", num220, "|", num221 }));
                                    _car.set_max_odds(string.Concat(new object[] { num222, "|", num223, "|", num224, "|", num225, "|", num226 }));
                                    _car.set_min_odds(string.Concat(new object[] { num227, "|", num228, "|", num229, "|", num230, "|", num231 }));
                                    break;

                                case 5:
                                case 6:
                                    num218 = double.Parse(LSRequest.qq("t" + num216.ToString() + "_default_odds_2"));
                                    num223 = double.Parse(LSRequest.qq("t" + num216.ToString() + "_max_odds_2"));
                                    if (!this.max_odds_isopen.Equals("1"))
                                    {
                                        num223 = Convert.ToDouble(row17["max_odds"].ToString().Split(new char[] { '|' })[1]);
                                    }
                                    num228 = double.Parse(LSRequest.qq("t" + num216.ToString() + "_min_odds_2"));
                                    if (num218 > num223)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能大於賠率上限！", str39), null, 400));
                                        base.Response.End();
                                    }
                                    if (num218 < num228)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能小於賠率下限！", str39), null, 400));
                                        base.Response.End();
                                    }
                                    if (num223 < num228)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率上限不能小於賠率下限！", str39), null, 400));
                                        base.Response.End();
                                    }
                                    _car.set_default_odds(num217 + "|" + num218);
                                    _car.set_max_odds(num222 + "|" + num223);
                                    _car.set_min_odds(num227 + "|" + num228);
                                    break;

                                default:
                                    _car.set_default_odds(num217.ToString());
                                    _car.set_max_odds(num222.ToString());
                                    _car.set_min_odds(num227.ToString());
                                    break;
                            }
                            _car.set_b_diff(new decimal?(Convert.ToDecimal(LSRequest.qq("t" + num216.ToString() + "_b_diff"))));
                            _car.set_c_diff(new decimal?(Convert.ToDecimal(LSRequest.qq("t" + num216.ToString() + "_c_diff"))));
                            if (!this.max_odds_isopen.Equals("1"))
                            {
                                _car.set_b_diff(new decimal?(Convert.ToDecimal(row17["b_diff"].ToString())));
                                _car.set_c_diff(new decimal?(Convert.ToDecimal(row17["c_diff"].ToString())));
                            }
                            _car.set_downbase(new decimal?(Convert.ToDecimal(LSRequest.qq("t" + num216.ToString() + "_downbase"))));
                            _car.set_down_odds_rate(new decimal?(Convert.ToDecimal(LSRequest.qq("t" + num216.ToString() + "_down_odds_rate"))));
                            _car.set_allow_min_amount(new decimal?(decimal.Parse(row17["allow_min_amount"].ToString())));
                            string str40 = "";
                            switch (num216)
                            {
                                case 0:
                                    str40 = "1,5,9,13,17,21,24,27,30,33";
                                    break;

                                case 1:
                                    str40 = "2,6,10,14,18,22,25,28,31,34";
                                    break;

                                case 2:
                                    str40 = "3,7,11,15,19,23,26,29,32,35";
                                    break;

                                case 3:
                                    str40 = "4,8,12,16,20";
                                    break;

                                case 4:
                                    str40 = row17["play_id"].ToString();
                                    break;

                                case 5:
                                    str40 = row17["play_id"].ToString();
                                    break;

                                case 6:
                                    str40 = row17["play_id"].ToString();
                                    break;

                                default:
                                    str40 = row17["play_id"].ToString();
                                    break;
                            }
                            CallBLL.cz_play_car168_bll.SetPlayByOddsSet(str40, _car);
                            num216++;
                        }
                        base.plsd_kc_log(rs, CallBLL.cz_play_car168_bll.GetPlayByOddsSet("1,2,3,4,36,37,38").Tables[0], this.lotteryId);
                    }
                    break;
                }
                case 0x11:
                {
                    DataSet set18 = CallBLL.cz_play_ssc168_bll.GetPlayByOddsSet("1,2,3,16,17,18,19");
                    if (((set18 != null) && (set18.Tables.Count > 0)) && (set18.Tables[0].Rows.Count > 0))
                    {
                        rs = set18.Tables[0];
                        this.ValidInput_SSC168(rs);
                        int num232 = 0;
                        foreach (DataRow row18 in rs.Rows)
                        {
                            string str41 = row18["play_name"].ToString().Trim().Replace("第一球", "1-5球");
                            cz_play_ssc168 _ssc = new cz_play_ssc168();
                            double num233 = 0.0;
                            double num234 = 0.0;
                            double num235 = 0.0;
                            double num236 = 0.0;
                            double num237 = 0.0;
                            double num238 = 0.0;
                            double num239 = 0.0;
                            double num240 = 0.0;
                            double num241 = 0.0;
                            double num242 = 0.0;
                            double num243 = 0.0;
                            double num244 = 0.0;
                            double num245 = 0.0;
                            double num246 = 0.0;
                            double num247 = 0.0;
                            num233 = double.Parse(LSRequest.qq("t" + num232.ToString() + "_default_odds_1"));
                            num238 = double.Parse(LSRequest.qq("t" + num232.ToString() + "_max_odds_1"));
                            if (!this.max_odds_isopen.Equals("1"))
                            {
                                num238 = Convert.ToDouble(row18["max_odds"].ToString().Split(new char[] { '|' })[0]);
                            }
                            num243 = double.Parse(LSRequest.qq("t" + num232.ToString() + "_min_odds_1"));
                            if (num233 > num238)
                            {
                                base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能大於賠率上限！", str41), null, 400));
                                base.Response.End();
                            }
                            if (num233 < num243)
                            {
                                base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能小於賠率下限！", str41), null, 400));
                                base.Response.End();
                            }
                            if (num238 < num243)
                            {
                                base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率上限不能小於賠率下限！", str41), null, 400));
                                base.Response.End();
                            }
                            switch (num232)
                            {
                                case 5:
                                    num234 = double.Parse(LSRequest.qq("t" + num232.ToString() + "_default_odds_2"));
                                    num239 = double.Parse(LSRequest.qq("t" + num232.ToString() + "_max_odds_2"));
                                    if (!this.max_odds_isopen.Equals("1"))
                                    {
                                        num239 = Convert.ToDouble(row18["max_odds"].ToString().Split(new char[] { '|' })[1]);
                                    }
                                    num244 = double.Parse(LSRequest.qq("t" + num232.ToString() + "_min_odds_2"));
                                    if (num234 > num239)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能大於賠率上限！", str41), null, 400));
                                        base.Response.End();
                                    }
                                    if (num234 < num244)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能小於賠率下限！", str41), null, 400));
                                        base.Response.End();
                                    }
                                    if (num239 < num244)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率上限不能小於賠率下限！", str41), null, 400));
                                        base.Response.End();
                                    }
                                    _ssc.set_default_odds(num233 + "|" + num234);
                                    _ssc.set_max_odds(num238 + "|" + num239);
                                    _ssc.set_min_odds(num243 + "|" + num244);
                                    break;

                                case 6:
                                    num234 = double.Parse(LSRequest.qq("t" + num232.ToString() + "_default_odds_2"));
                                    num239 = double.Parse(LSRequest.qq("t" + num232.ToString() + "_max_odds_2"));
                                    if (!this.max_odds_isopen.Equals("1"))
                                    {
                                        num239 = Convert.ToDouble(row18["max_odds"].ToString().Split(new char[] { '|' })[1]);
                                    }
                                    num244 = double.Parse(LSRequest.qq("t" + num232.ToString() + "_min_odds_2"));
                                    num235 = double.Parse(LSRequest.qq("t" + num232.ToString() + "_default_odds_3"));
                                    num240 = double.Parse(LSRequest.qq("t" + num232.ToString() + "_max_odds_3"));
                                    if (!this.max_odds_isopen.Equals("1"))
                                    {
                                        num240 = Convert.ToDouble(row18["max_odds"].ToString().Split(new char[] { '|' })[2]);
                                    }
                                    num245 = double.Parse(LSRequest.qq("t" + num232.ToString() + "_min_odds_3"));
                                    num236 = double.Parse(LSRequest.qq("t" + num232.ToString() + "_default_odds_4"));
                                    num241 = double.Parse(LSRequest.qq("t" + num232.ToString() + "_max_odds_4"));
                                    if (!this.max_odds_isopen.Equals("1"))
                                    {
                                        num241 = Convert.ToDouble(row18["max_odds"].ToString().Split(new char[] { '|' })[3]);
                                    }
                                    num246 = double.Parse(LSRequest.qq("t" + num232.ToString() + "_min_odds_4"));
                                    num237 = double.Parse(LSRequest.qq("t" + num232.ToString() + "_default_odds_5"));
                                    num242 = double.Parse(LSRequest.qq("t" + num232.ToString() + "_max_odds_5"));
                                    if (!this.max_odds_isopen.Equals("1"))
                                    {
                                        num242 = Convert.ToDouble(row18["max_odds"].ToString().Split(new char[] { '|' })[4]);
                                    }
                                    num247 = double.Parse(LSRequest.qq("t" + num232.ToString() + "_min_odds_5"));
                                    if (num234 > num239)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能大於賠率上限！", str41), null, 400));
                                        base.Response.End();
                                    }
                                    if (num234 < num244)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能小於賠率下限！", str41), null, 400));
                                        base.Response.End();
                                    }
                                    if (num239 < num244)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率上限不能小於賠率下限！", str41), null, 400));
                                        base.Response.End();
                                    }
                                    if (num235 > num240)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能大於賠率上限！", str41), null, 400));
                                        base.Response.End();
                                    }
                                    if (num235 < num245)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能小於賠率下限！", str41), null, 400));
                                        base.Response.End();
                                    }
                                    if (num240 < num245)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率上限不能小於賠率下限！", str41), null, 400));
                                        base.Response.End();
                                    }
                                    if (num236 > num241)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能大於賠率上限！", str41), null, 400));
                                        base.Response.End();
                                    }
                                    if (num236 < num246)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能小於賠率下限！", str41), null, 400));
                                        base.Response.End();
                                    }
                                    if (num241 < num246)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率上限不能小於賠率下限！", str41), null, 400));
                                        base.Response.End();
                                    }
                                    if (num237 > num242)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能大於賠率上限！", str41), null, 400));
                                        base.Response.End();
                                    }
                                    if (num237 < num247)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能小於賠率下限！", str41), null, 400));
                                        base.Response.End();
                                    }
                                    if (num242 < num247)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率上限不能小於賠率下限！", str41), null, 400));
                                        base.Response.End();
                                    }
                                    _ssc.set_default_odds(string.Concat(new object[] { num233, "|", num234, "|", num235, "|", num236, "|", num237 }));
                                    _ssc.set_max_odds(string.Concat(new object[] { num238, "|", num239, "|", num240, "|", num241, "|", num242 }));
                                    _ssc.set_min_odds(string.Concat(new object[] { num243, "|", num244, "|", num245, "|", num246, "|", num247 }));
                                    break;

                                default:
                                    _ssc.set_default_odds(num233.ToString());
                                    _ssc.set_max_odds(num238.ToString());
                                    _ssc.set_min_odds(num243.ToString());
                                    break;
                            }
                            _ssc.set_b_diff(new decimal?(Convert.ToDecimal(LSRequest.qq("t" + num232.ToString() + "_b_diff"))));
                            _ssc.set_c_diff(new decimal?(Convert.ToDecimal(LSRequest.qq("t" + num232.ToString() + "_c_diff"))));
                            if (!this.max_odds_isopen.Equals("1"))
                            {
                                _ssc.set_b_diff(new decimal?(Convert.ToDecimal(row18["b_diff"].ToString())));
                                _ssc.set_c_diff(new decimal?(Convert.ToDecimal(row18["c_diff"].ToString())));
                            }
                            _ssc.set_downbase(new decimal?(Convert.ToDecimal(LSRequest.qq("t" + num232.ToString() + "_downbase"))));
                            _ssc.set_down_odds_rate(new decimal?(Convert.ToDecimal(LSRequest.qq("t" + num232.ToString() + "_down_odds_rate"))));
                            _ssc.set_allow_min_amount(new decimal?(decimal.Parse(row18["allow_min_amount"].ToString())));
                            string str42 = "";
                            switch (num232)
                            {
                                case 0:
                                    str42 = "1,4,7,10,13";
                                    break;

                                case 1:
                                    str42 = "2,5,8,11,14";
                                    break;

                                case 2:
                                    str42 = "3,6,9,12,15";
                                    break;

                                case 5:
                                    str42 = row18["play_id"].ToString();
                                    break;

                                case 6:
                                    str42 = "19,20,21";
                                    break;

                                default:
                                    str42 = row18["play_id"].ToString();
                                    break;
                            }
                            CallBLL.cz_play_ssc168_bll.SetPlayByOddsSet(str42, _ssc);
                            num232++;
                        }
                        base.plsd_kc_log(rs, CallBLL.cz_play_ssc168_bll.GetPlayByOddsSet("1,2,3,16,17,18,19").Tables[0], this.lotteryId);
                    }
                    break;
                }
                case 0x12:
                {
                    DataSet set19 = CallBLL.cz_play_vrcar_bll.GetPlayByOddsSet("1,2,3,4,36,37,38");
                    if (((set19 != null) && (set19.Tables.Count > 0)) && (set19.Tables[0].Rows.Count > 0))
                    {
                        rs = set19.Tables[0];
                        this.ValidInput_VRCAR(rs);
                        int num248 = 0;
                        foreach (DataRow row19 in rs.Rows)
                        {
                            string str43 = row19["play_name"].ToString().Trim().Replace("冠軍龍虎", "1-5名龍虎").Replace("冠軍", "1-10名");
                            cz_play_vrcar _vrcar = new cz_play_vrcar();
                            double num249 = 0.0;
                            double num250 = 0.0;
                            double num251 = 0.0;
                            double num252 = 0.0;
                            double num253 = 0.0;
                            double num254 = 0.0;
                            double num255 = 0.0;
                            double num256 = 0.0;
                            double num257 = 0.0;
                            double num258 = 0.0;
                            double num259 = 0.0;
                            double num260 = 0.0;
                            double num261 = 0.0;
                            double num262 = 0.0;
                            double num263 = 0.0;
                            num249 = double.Parse(LSRequest.qq("t" + num248.ToString() + "_default_odds_1"));
                            num254 = double.Parse(LSRequest.qq("t" + num248.ToString() + "_max_odds_1"));
                            if (!this.max_odds_isopen.Equals("1"))
                            {
                                num254 = Convert.ToDouble(row19["max_odds"].ToString().Split(new char[] { '|' })[0]);
                            }
                            num259 = double.Parse(LSRequest.qq("t" + num248.ToString() + "_min_odds_1"));
                            if (num249 > num254)
                            {
                                base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能大於賠率上限！", str43), null, 400));
                                base.Response.End();
                            }
                            if (num249 < num259)
                            {
                                base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能小於賠率下限！", str43), null, 400));
                                base.Response.End();
                            }
                            if (num254 < num259)
                            {
                                base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率上限不能小於賠率下限！", str43), null, 400));
                                base.Response.End();
                            }
                            switch (num248)
                            {
                                case 4:
                                    num250 = double.Parse(LSRequest.qq("t" + num248.ToString() + "_default_odds_2"));
                                    num255 = double.Parse(LSRequest.qq("t" + num248.ToString() + "_max_odds_2"));
                                    if (!this.max_odds_isopen.Equals("1"))
                                    {
                                        num255 = Convert.ToDouble(row19["max_odds"].ToString().Split(new char[] { '|' })[1]);
                                    }
                                    num260 = double.Parse(LSRequest.qq("t" + num248.ToString() + "_min_odds_2"));
                                    num251 = double.Parse(LSRequest.qq("t" + num248.ToString() + "_default_odds_3"));
                                    num256 = double.Parse(LSRequest.qq("t" + num248.ToString() + "_max_odds_3"));
                                    if (!this.max_odds_isopen.Equals("1"))
                                    {
                                        num256 = Convert.ToDouble(row19["max_odds"].ToString().Split(new char[] { '|' })[2]);
                                    }
                                    num261 = double.Parse(LSRequest.qq("t" + num248.ToString() + "_min_odds_3"));
                                    num252 = double.Parse(LSRequest.qq("t" + num248.ToString() + "_default_odds_4"));
                                    num257 = double.Parse(LSRequest.qq("t" + num248.ToString() + "_max_odds_4"));
                                    if (!this.max_odds_isopen.Equals("1"))
                                    {
                                        num257 = Convert.ToDouble(row19["max_odds"].ToString().Split(new char[] { '|' })[3]);
                                    }
                                    num262 = double.Parse(LSRequest.qq("t" + num248.ToString() + "_min_odds_4"));
                                    num253 = double.Parse(LSRequest.qq("t" + num248.ToString() + "_default_odds_5"));
                                    num258 = double.Parse(LSRequest.qq("t" + num248.ToString() + "_max_odds_5"));
                                    if (!this.max_odds_isopen.Equals("1"))
                                    {
                                        num258 = Convert.ToDouble(row19["max_odds"].ToString().Split(new char[] { '|' })[4]);
                                    }
                                    num263 = double.Parse(LSRequest.qq("t" + num248.ToString() + "_min_odds_5"));
                                    if (num250 > num255)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能大於賠率上限！", str43), null, 400));
                                        base.Response.End();
                                    }
                                    if (num250 < num260)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能小於賠率下限！", str43), null, 400));
                                        base.Response.End();
                                    }
                                    if (num255 < num260)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率上限不能小於賠率下限！", str43), null, 400));
                                        base.Response.End();
                                    }
                                    if (num251 > num256)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能大於賠率上限！", str43), null, 400));
                                        base.Response.End();
                                    }
                                    if (num251 < num261)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能小於賠率下限！", str43), null, 400));
                                        base.Response.End();
                                    }
                                    if (num256 < num261)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率上限不能小於賠率下限！", str43), null, 400));
                                        base.Response.End();
                                    }
                                    if (num252 > num257)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能大於賠率上限！", str43), null, 400));
                                        base.Response.End();
                                    }
                                    if (num252 < num262)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能小於賠率下限！", str43), null, 400));
                                        base.Response.End();
                                    }
                                    if (num257 < num262)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率上限不能小於賠率下限！", str43), null, 400));
                                        base.Response.End();
                                    }
                                    if (num253 > num258)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能大於賠率上限！", str43), null, 400));
                                        base.Response.End();
                                    }
                                    if (num253 < num263)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能小於賠率下限！", str43), null, 400));
                                        base.Response.End();
                                    }
                                    if (num258 < num263)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率上限不能小於賠率下限！", str43), null, 400));
                                        base.Response.End();
                                    }
                                    _vrcar.set_default_odds(string.Concat(new object[] { num249, "|", num250, "|", num251, "|", num252, "|", num253 }));
                                    _vrcar.set_max_odds(string.Concat(new object[] { num254, "|", num255, "|", num256, "|", num257, "|", num258 }));
                                    _vrcar.set_min_odds(string.Concat(new object[] { num259, "|", num260, "|", num261, "|", num262, "|", num263 }));
                                    break;

                                case 5:
                                case 6:
                                    num250 = double.Parse(LSRequest.qq("t" + num248.ToString() + "_default_odds_2"));
                                    num255 = double.Parse(LSRequest.qq("t" + num248.ToString() + "_max_odds_2"));
                                    if (!this.max_odds_isopen.Equals("1"))
                                    {
                                        num255 = Convert.ToDouble(row19["max_odds"].ToString().Split(new char[] { '|' })[1]);
                                    }
                                    num260 = double.Parse(LSRequest.qq("t" + num248.ToString() + "_min_odds_2"));
                                    if (num250 > num255)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能大於賠率上限！", str43), null, 400));
                                        base.Response.End();
                                    }
                                    if (num250 < num260)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能小於賠率下限！", str43), null, 400));
                                        base.Response.End();
                                    }
                                    if (num255 < num260)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率上限不能小於賠率下限！", str43), null, 400));
                                        base.Response.End();
                                    }
                                    _vrcar.set_default_odds(num249 + "|" + num250);
                                    _vrcar.set_max_odds(num254 + "|" + num255);
                                    _vrcar.set_min_odds(num259 + "|" + num260);
                                    break;

                                default:
                                    _vrcar.set_default_odds(num249.ToString());
                                    _vrcar.set_max_odds(num254.ToString());
                                    _vrcar.set_min_odds(num259.ToString());
                                    break;
                            }
                            _vrcar.set_b_diff(new decimal?(Convert.ToDecimal(LSRequest.qq("t" + num248.ToString() + "_b_diff"))));
                            _vrcar.set_c_diff(new decimal?(Convert.ToDecimal(LSRequest.qq("t" + num248.ToString() + "_c_diff"))));
                            if (!this.max_odds_isopen.Equals("1"))
                            {
                                _vrcar.set_b_diff(new decimal?(Convert.ToDecimal(row19["b_diff"].ToString())));
                                _vrcar.set_c_diff(new decimal?(Convert.ToDecimal(row19["c_diff"].ToString())));
                            }
                            _vrcar.set_downbase(new decimal?(Convert.ToDecimal(LSRequest.qq("t" + num248.ToString() + "_downbase"))));
                            _vrcar.set_down_odds_rate(new decimal?(Convert.ToDecimal(LSRequest.qq("t" + num248.ToString() + "_down_odds_rate"))));
                            _vrcar.set_allow_min_amount(new decimal?(decimal.Parse(row19["allow_min_amount"].ToString())));
                            string str44 = "";
                            switch (num248)
                            {
                                case 0:
                                    str44 = "1,5,9,13,17,21,24,27,30,33";
                                    break;

                                case 1:
                                    str44 = "2,6,10,14,18,22,25,28,31,34";
                                    break;

                                case 2:
                                    str44 = "3,7,11,15,19,23,26,29,32,35";
                                    break;

                                case 3:
                                    str44 = "4,8,12,16,20";
                                    break;

                                case 4:
                                    str44 = row19["play_id"].ToString();
                                    break;

                                case 5:
                                    str44 = row19["play_id"].ToString();
                                    break;

                                case 6:
                                    str44 = row19["play_id"].ToString();
                                    break;

                                default:
                                    str44 = row19["play_id"].ToString();
                                    break;
                            }
                            CallBLL.cz_play_vrcar_bll.SetPlayByOddsSet(str44, _vrcar);
                            num248++;
                        }
                        base.plsd_kc_log(rs, CallBLL.cz_play_vrcar_bll.GetPlayByOddsSet("1,2,3,4,36,37,38").Tables[0], this.lotteryId);
                    }
                    break;
                }
                case 0x13:
                {
                    DataSet set20 = CallBLL.cz_play_vrssc_bll.GetPlayByOddsSet("1,2,3,16,17,18,19");
                    if (((set20 != null) && (set20.Tables.Count > 0)) && (set20.Tables[0].Rows.Count > 0))
                    {
                        rs = set20.Tables[0];
                        this.ValidInput_VRSSC(rs);
                        int num264 = 0;
                        foreach (DataRow row20 in rs.Rows)
                        {
                            string str45 = row20["play_name"].ToString().Trim().Replace("第一球", "1-5球");
                            cz_play_vrssc _vrssc = new cz_play_vrssc();
                            double num265 = 0.0;
                            double num266 = 0.0;
                            double num267 = 0.0;
                            double num268 = 0.0;
                            double num269 = 0.0;
                            double num270 = 0.0;
                            double num271 = 0.0;
                            double num272 = 0.0;
                            double num273 = 0.0;
                            double num274 = 0.0;
                            double num275 = 0.0;
                            double num276 = 0.0;
                            double num277 = 0.0;
                            double num278 = 0.0;
                            double num279 = 0.0;
                            num265 = double.Parse(LSRequest.qq("t" + num264.ToString() + "_default_odds_1"));
                            num270 = double.Parse(LSRequest.qq("t" + num264.ToString() + "_max_odds_1"));
                            if (!this.max_odds_isopen.Equals("1"))
                            {
                                num270 = Convert.ToDouble(row20["max_odds"].ToString().Split(new char[] { '|' })[0]);
                            }
                            num275 = double.Parse(LSRequest.qq("t" + num264.ToString() + "_min_odds_1"));
                            if (num265 > num270)
                            {
                                base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能大於賠率上限！", str45), null, 400));
                                base.Response.End();
                            }
                            if (num265 < num275)
                            {
                                base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能小於賠率下限！", str45), null, 400));
                                base.Response.End();
                            }
                            if (num270 < num275)
                            {
                                base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率上限不能小於賠率下限！", str45), null, 400));
                                base.Response.End();
                            }
                            switch (num264)
                            {
                                case 5:
                                    num266 = double.Parse(LSRequest.qq("t" + num264.ToString() + "_default_odds_2"));
                                    num271 = double.Parse(LSRequest.qq("t" + num264.ToString() + "_max_odds_2"));
                                    if (!this.max_odds_isopen.Equals("1"))
                                    {
                                        num271 = Convert.ToDouble(row20["max_odds"].ToString().Split(new char[] { '|' })[1]);
                                    }
                                    num276 = double.Parse(LSRequest.qq("t" + num264.ToString() + "_min_odds_2"));
                                    if (num266 > num271)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能大於賠率上限！", str45), null, 400));
                                        base.Response.End();
                                    }
                                    if (num266 < num276)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能小於賠率下限！", str45), null, 400));
                                        base.Response.End();
                                    }
                                    if (num271 < num276)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率上限不能小於賠率下限！", str45), null, 400));
                                        base.Response.End();
                                    }
                                    _vrssc.set_default_odds(num265 + "|" + num266);
                                    _vrssc.set_max_odds(num270 + "|" + num271);
                                    _vrssc.set_min_odds(num275 + "|" + num276);
                                    break;

                                case 6:
                                    num266 = double.Parse(LSRequest.qq("t" + num264.ToString() + "_default_odds_2"));
                                    num271 = double.Parse(LSRequest.qq("t" + num264.ToString() + "_max_odds_2"));
                                    if (!this.max_odds_isopen.Equals("1"))
                                    {
                                        num271 = Convert.ToDouble(row20["max_odds"].ToString().Split(new char[] { '|' })[1]);
                                    }
                                    num276 = double.Parse(LSRequest.qq("t" + num264.ToString() + "_min_odds_2"));
                                    num267 = double.Parse(LSRequest.qq("t" + num264.ToString() + "_default_odds_3"));
                                    num272 = double.Parse(LSRequest.qq("t" + num264.ToString() + "_max_odds_3"));
                                    if (!this.max_odds_isopen.Equals("1"))
                                    {
                                        num272 = Convert.ToDouble(row20["max_odds"].ToString().Split(new char[] { '|' })[2]);
                                    }
                                    num277 = double.Parse(LSRequest.qq("t" + num264.ToString() + "_min_odds_3"));
                                    num268 = double.Parse(LSRequest.qq("t" + num264.ToString() + "_default_odds_4"));
                                    num273 = double.Parse(LSRequest.qq("t" + num264.ToString() + "_max_odds_4"));
                                    if (!this.max_odds_isopen.Equals("1"))
                                    {
                                        num273 = Convert.ToDouble(row20["max_odds"].ToString().Split(new char[] { '|' })[3]);
                                    }
                                    num278 = double.Parse(LSRequest.qq("t" + num264.ToString() + "_min_odds_4"));
                                    num269 = double.Parse(LSRequest.qq("t" + num264.ToString() + "_default_odds_5"));
                                    num274 = double.Parse(LSRequest.qq("t" + num264.ToString() + "_max_odds_5"));
                                    if (!this.max_odds_isopen.Equals("1"))
                                    {
                                        num274 = Convert.ToDouble(row20["max_odds"].ToString().Split(new char[] { '|' })[4]);
                                    }
                                    num279 = double.Parse(LSRequest.qq("t" + num264.ToString() + "_min_odds_5"));
                                    if (num266 > num271)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能大於賠率上限！", str45), null, 400));
                                        base.Response.End();
                                    }
                                    if (num266 < num276)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能小於賠率下限！", str45), null, 400));
                                        base.Response.End();
                                    }
                                    if (num271 < num276)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率上限不能小於賠率下限！", str45), null, 400));
                                        base.Response.End();
                                    }
                                    if (num267 > num272)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能大於賠率上限！", str45), null, 400));
                                        base.Response.End();
                                    }
                                    if (num267 < num277)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能小於賠率下限！", str45), null, 400));
                                        base.Response.End();
                                    }
                                    if (num272 < num277)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率上限不能小於賠率下限！", str45), null, 400));
                                        base.Response.End();
                                    }
                                    if (num268 > num273)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能大於賠率上限！", str45), null, 400));
                                        base.Response.End();
                                    }
                                    if (num268 < num278)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能小於賠率下限！", str45), null, 400));
                                        base.Response.End();
                                    }
                                    if (num273 < num278)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率上限不能小於賠率下限！", str45), null, 400));
                                        base.Response.End();
                                    }
                                    if (num269 > num274)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能大於賠率上限！", str45), null, 400));
                                        base.Response.End();
                                    }
                                    if (num269 < num279)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能小於賠率下限！", str45), null, 400));
                                        base.Response.End();
                                    }
                                    if (num274 < num279)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率上限不能小於賠率下限！", str45), null, 400));
                                        base.Response.End();
                                    }
                                    _vrssc.set_default_odds(string.Concat(new object[] { num265, "|", num266, "|", num267, "|", num268, "|", num269 }));
                                    _vrssc.set_max_odds(string.Concat(new object[] { num270, "|", num271, "|", num272, "|", num273, "|", num274 }));
                                    _vrssc.set_min_odds(string.Concat(new object[] { num275, "|", num276, "|", num277, "|", num278, "|", num279 }));
                                    break;

                                default:
                                    _vrssc.set_default_odds(num265.ToString());
                                    _vrssc.set_max_odds(num270.ToString());
                                    _vrssc.set_min_odds(num275.ToString());
                                    break;
                            }
                            _vrssc.set_b_diff(new decimal?(Convert.ToDecimal(LSRequest.qq("t" + num264.ToString() + "_b_diff"))));
                            _vrssc.set_c_diff(new decimal?(Convert.ToDecimal(LSRequest.qq("t" + num264.ToString() + "_c_diff"))));
                            if (!this.max_odds_isopen.Equals("1"))
                            {
                                _vrssc.set_b_diff(new decimal?(Convert.ToDecimal(row20["b_diff"].ToString())));
                                _vrssc.set_c_diff(new decimal?(Convert.ToDecimal(row20["c_diff"].ToString())));
                            }
                            _vrssc.set_downbase(new decimal?(Convert.ToDecimal(LSRequest.qq("t" + num264.ToString() + "_downbase"))));
                            _vrssc.set_down_odds_rate(new decimal?(Convert.ToDecimal(LSRequest.qq("t" + num264.ToString() + "_down_odds_rate"))));
                            _vrssc.set_allow_min_amount(new decimal?(decimal.Parse(row20["allow_min_amount"].ToString())));
                            string str46 = "";
                            switch (num264)
                            {
                                case 0:
                                    str46 = "1,4,7,10,13";
                                    break;

                                case 1:
                                    str46 = "2,5,8,11,14";
                                    break;

                                case 2:
                                    str46 = "3,6,9,12,15";
                                    break;

                                case 5:
                                    str46 = row20["play_id"].ToString();
                                    break;

                                case 6:
                                    str46 = "19,20,21";
                                    break;

                                default:
                                    str46 = row20["play_id"].ToString();
                                    break;
                            }
                            CallBLL.cz_play_vrssc_bll.SetPlayByOddsSet(str46, _vrssc);
                            num264++;
                        }
                        base.plsd_kc_log(rs, CallBLL.cz_play_vrssc_bll.GetPlayByOddsSet("1,2,3,16,17,18,19").Tables[0], this.lotteryId);
                    }
                    break;
                }
                case 20:
                {
                    DataSet set21 = CallBLL.cz_play_xyftoa_bll.GetPlayByOddsSet("1,2,3,4,36,37,38");
                    if (((set21 != null) && (set21.Tables.Count > 0)) && (set21.Tables[0].Rows.Count > 0))
                    {
                        rs = set21.Tables[0];
                        this.ValidInput_XYFTOA(rs);
                        int num280 = 0;
                        foreach (DataRow row21 in rs.Rows)
                        {
                            string str47 = row21["play_name"].ToString().Trim().Replace("冠軍龍虎", "1-5名龍虎").Replace("冠軍", "1-10名");
                            cz_play_xyftoa _xyftoa = new cz_play_xyftoa();
                            double num281 = 0.0;
                            double num282 = 0.0;
                            double num283 = 0.0;
                            double num284 = 0.0;
                            double num285 = 0.0;
                            double num286 = 0.0;
                            double num287 = 0.0;
                            double num288 = 0.0;
                            double num289 = 0.0;
                            double num290 = 0.0;
                            double num291 = 0.0;
                            double num292 = 0.0;
                            double num293 = 0.0;
                            double num294 = 0.0;
                            double num295 = 0.0;
                            num281 = double.Parse(LSRequest.qq("t" + num280.ToString() + "_default_odds_1"));
                            num286 = double.Parse(LSRequest.qq("t" + num280.ToString() + "_max_odds_1"));
                            if (!this.max_odds_isopen.Equals("1"))
                            {
                                num286 = Convert.ToDouble(row21["max_odds"].ToString().Split(new char[] { '|' })[0]);
                            }
                            num291 = double.Parse(LSRequest.qq("t" + num280.ToString() + "_min_odds_1"));
                            if (num281 > num286)
                            {
                                base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能大於賠率上限！", str47), null, 400));
                                base.Response.End();
                            }
                            if (num281 < num291)
                            {
                                base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能小於賠率下限！", str47), null, 400));
                                base.Response.End();
                            }
                            if (num286 < num291)
                            {
                                base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率上限不能小於賠率下限！", str47), null, 400));
                                base.Response.End();
                            }
                            switch (num280)
                            {
                                case 4:
                                    num282 = double.Parse(LSRequest.qq("t" + num280.ToString() + "_default_odds_2"));
                                    num287 = double.Parse(LSRequest.qq("t" + num280.ToString() + "_max_odds_2"));
                                    if (!this.max_odds_isopen.Equals("1"))
                                    {
                                        num287 = Convert.ToDouble(row21["max_odds"].ToString().Split(new char[] { '|' })[1]);
                                    }
                                    num292 = double.Parse(LSRequest.qq("t" + num280.ToString() + "_min_odds_2"));
                                    num283 = double.Parse(LSRequest.qq("t" + num280.ToString() + "_default_odds_3"));
                                    num288 = double.Parse(LSRequest.qq("t" + num280.ToString() + "_max_odds_3"));
                                    if (!this.max_odds_isopen.Equals("1"))
                                    {
                                        num288 = Convert.ToDouble(row21["max_odds"].ToString().Split(new char[] { '|' })[2]);
                                    }
                                    num293 = double.Parse(LSRequest.qq("t" + num280.ToString() + "_min_odds_3"));
                                    num284 = double.Parse(LSRequest.qq("t" + num280.ToString() + "_default_odds_4"));
                                    num289 = double.Parse(LSRequest.qq("t" + num280.ToString() + "_max_odds_4"));
                                    if (!this.max_odds_isopen.Equals("1"))
                                    {
                                        num289 = Convert.ToDouble(row21["max_odds"].ToString().Split(new char[] { '|' })[3]);
                                    }
                                    num294 = double.Parse(LSRequest.qq("t" + num280.ToString() + "_min_odds_4"));
                                    num285 = double.Parse(LSRequest.qq("t" + num280.ToString() + "_default_odds_5"));
                                    num290 = double.Parse(LSRequest.qq("t" + num280.ToString() + "_max_odds_5"));
                                    if (!this.max_odds_isopen.Equals("1"))
                                    {
                                        num290 = Convert.ToDouble(row21["max_odds"].ToString().Split(new char[] { '|' })[4]);
                                    }
                                    num295 = double.Parse(LSRequest.qq("t" + num280.ToString() + "_min_odds_5"));
                                    if (num282 > num287)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能大於賠率上限！", str47), null, 400));
                                        base.Response.End();
                                    }
                                    if (num282 < num292)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能小於賠率下限！", str47), null, 400));
                                        base.Response.End();
                                    }
                                    if (num287 < num292)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率上限不能小於賠率下限！", str47), null, 400));
                                        base.Response.End();
                                    }
                                    if (num283 > num288)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能大於賠率上限！", str47), null, 400));
                                        base.Response.End();
                                    }
                                    if (num283 < num293)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能小於賠率下限！", str47), null, 400));
                                        base.Response.End();
                                    }
                                    if (num288 < num293)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率上限不能小於賠率下限！", str47), null, 400));
                                        base.Response.End();
                                    }
                                    if (num284 > num289)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能大於賠率上限！", str47), null, 400));
                                        base.Response.End();
                                    }
                                    if (num284 < num294)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能小於賠率下限！", str47), null, 400));
                                        base.Response.End();
                                    }
                                    if (num289 < num294)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率上限不能小於賠率下限！", str47), null, 400));
                                        base.Response.End();
                                    }
                                    if (num285 > num290)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能大於賠率上限！", str47), null, 400));
                                        base.Response.End();
                                    }
                                    if (num285 < num295)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能小於賠率下限！", str47), null, 400));
                                        base.Response.End();
                                    }
                                    if (num290 < num295)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率上限不能小於賠率下限！", str47), null, 400));
                                        base.Response.End();
                                    }
                                    _xyftoa.set_default_odds(string.Concat(new object[] { num281, "|", num282, "|", num283, "|", num284, "|", num285 }));
                                    _xyftoa.set_max_odds(string.Concat(new object[] { num286, "|", num287, "|", num288, "|", num289, "|", num290 }));
                                    _xyftoa.set_min_odds(string.Concat(new object[] { num291, "|", num292, "|", num293, "|", num294, "|", num295 }));
                                    break;

                                case 5:
                                case 6:
                                    num282 = double.Parse(LSRequest.qq("t" + num280.ToString() + "_default_odds_2"));
                                    num287 = double.Parse(LSRequest.qq("t" + num280.ToString() + "_max_odds_2"));
                                    if (!this.max_odds_isopen.Equals("1"))
                                    {
                                        num287 = Convert.ToDouble(row21["max_odds"].ToString().Split(new char[] { '|' })[1]);
                                    }
                                    num292 = double.Parse(LSRequest.qq("t" + num280.ToString() + "_min_odds_2"));
                                    if (num282 > num287)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能大於賠率上限！", str47), null, 400));
                                        base.Response.End();
                                    }
                                    if (num282 < num292)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能小於賠率下限！", str47), null, 400));
                                        base.Response.End();
                                    }
                                    if (num287 < num292)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率上限不能小於賠率下限！", str47), null, 400));
                                        base.Response.End();
                                    }
                                    _xyftoa.set_default_odds(num281 + "|" + num282);
                                    _xyftoa.set_max_odds(num286 + "|" + num287);
                                    _xyftoa.set_min_odds(num291 + "|" + num292);
                                    break;

                                default:
                                    _xyftoa.set_default_odds(num281.ToString());
                                    _xyftoa.set_max_odds(num286.ToString());
                                    _xyftoa.set_min_odds(num291.ToString());
                                    break;
                            }
                            _xyftoa.set_b_diff(new decimal?(Convert.ToDecimal(LSRequest.qq("t" + num280.ToString() + "_b_diff"))));
                            _xyftoa.set_c_diff(new decimal?(Convert.ToDecimal(LSRequest.qq("t" + num280.ToString() + "_c_diff"))));
                            if (!this.max_odds_isopen.Equals("1"))
                            {
                                _xyftoa.set_b_diff(new decimal?(Convert.ToDecimal(row21["b_diff"].ToString())));
                                _xyftoa.set_c_diff(new decimal?(Convert.ToDecimal(row21["c_diff"].ToString())));
                            }
                            _xyftoa.set_downbase(new decimal?(Convert.ToDecimal(LSRequest.qq("t" + num280.ToString() + "_downbase"))));
                            _xyftoa.set_down_odds_rate(new decimal?(Convert.ToDecimal(LSRequest.qq("t" + num280.ToString() + "_down_odds_rate"))));
                            _xyftoa.set_allow_min_amount(new decimal?(decimal.Parse(row21["allow_min_amount"].ToString())));
                            string str48 = "";
                            switch (num280)
                            {
                                case 0:
                                    str48 = "1,5,9,13,17,21,24,27,30,33";
                                    break;

                                case 1:
                                    str48 = "2,6,10,14,18,22,25,28,31,34";
                                    break;

                                case 2:
                                    str48 = "3,7,11,15,19,23,26,29,32,35";
                                    break;

                                case 3:
                                    str48 = "4,8,12,16,20";
                                    break;

                                case 4:
                                    str48 = row21["play_id"].ToString();
                                    break;

                                case 5:
                                    str48 = row21["play_id"].ToString();
                                    break;

                                case 6:
                                    str48 = row21["play_id"].ToString();
                                    break;

                                default:
                                    str48 = row21["play_id"].ToString();
                                    break;
                            }
                            CallBLL.cz_play_xyftoa_bll.SetPlayByOddsSet(str48, _xyftoa);
                            num280++;
                        }
                        base.plsd_kc_log(rs, CallBLL.cz_play_xyftoa_bll.GetPlayByOddsSet("1,2,3,4,36,37,38").Tables[0], this.lotteryId);
                    }
                    break;
                }
                case 0x15:
                {
                    DataSet set22 = CallBLL.cz_play_xyftsg_bll.GetPlayByOddsSet("1,2,3,4,36,37,38");
                    if (((set22 != null) && (set22.Tables.Count > 0)) && (set22.Tables[0].Rows.Count > 0))
                    {
                        rs = set22.Tables[0];
                        this.ValidInput_XYFTSG(rs);
                        int num296 = 0;
                        foreach (DataRow row22 in rs.Rows)
                        {
                            string str49 = row22["play_name"].ToString().Trim().Replace("冠軍龍虎", "1-5名龍虎").Replace("冠軍", "1-10名");
                            cz_play_xyftsg _xyftsg = new cz_play_xyftsg();
                            double num297 = 0.0;
                            double num298 = 0.0;
                            double num299 = 0.0;
                            double num300 = 0.0;
                            double num301 = 0.0;
                            double num302 = 0.0;
                            double num303 = 0.0;
                            double num304 = 0.0;
                            double num305 = 0.0;
                            double num306 = 0.0;
                            double num307 = 0.0;
                            double num308 = 0.0;
                            double num309 = 0.0;
                            double num310 = 0.0;
                            double num311 = 0.0;
                            num297 = double.Parse(LSRequest.qq("t" + num296.ToString() + "_default_odds_1"));
                            num302 = double.Parse(LSRequest.qq("t" + num296.ToString() + "_max_odds_1"));
                            if (!this.max_odds_isopen.Equals("1"))
                            {
                                num302 = Convert.ToDouble(row22["max_odds"].ToString().Split(new char[] { '|' })[0]);
                            }
                            num307 = double.Parse(LSRequest.qq("t" + num296.ToString() + "_min_odds_1"));
                            if (num297 > num302)
                            {
                                base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能大於賠率上限！", str49), null, 400));
                                base.Response.End();
                            }
                            if (num297 < num307)
                            {
                                base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能小於賠率下限！", str49), null, 400));
                                base.Response.End();
                            }
                            if (num302 < num307)
                            {
                                base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率上限不能小於賠率下限！", str49), null, 400));
                                base.Response.End();
                            }
                            switch (num296)
                            {
                                case 4:
                                    num298 = double.Parse(LSRequest.qq("t" + num296.ToString() + "_default_odds_2"));
                                    num303 = double.Parse(LSRequest.qq("t" + num296.ToString() + "_max_odds_2"));
                                    if (!this.max_odds_isopen.Equals("1"))
                                    {
                                        num303 = Convert.ToDouble(row22["max_odds"].ToString().Split(new char[] { '|' })[1]);
                                    }
                                    num308 = double.Parse(LSRequest.qq("t" + num296.ToString() + "_min_odds_2"));
                                    num299 = double.Parse(LSRequest.qq("t" + num296.ToString() + "_default_odds_3"));
                                    num304 = double.Parse(LSRequest.qq("t" + num296.ToString() + "_max_odds_3"));
                                    if (!this.max_odds_isopen.Equals("1"))
                                    {
                                        num304 = Convert.ToDouble(row22["max_odds"].ToString().Split(new char[] { '|' })[2]);
                                    }
                                    num309 = double.Parse(LSRequest.qq("t" + num296.ToString() + "_min_odds_3"));
                                    num300 = double.Parse(LSRequest.qq("t" + num296.ToString() + "_default_odds_4"));
                                    num305 = double.Parse(LSRequest.qq("t" + num296.ToString() + "_max_odds_4"));
                                    if (!this.max_odds_isopen.Equals("1"))
                                    {
                                        num305 = Convert.ToDouble(row22["max_odds"].ToString().Split(new char[] { '|' })[3]);
                                    }
                                    num310 = double.Parse(LSRequest.qq("t" + num296.ToString() + "_min_odds_4"));
                                    num301 = double.Parse(LSRequest.qq("t" + num296.ToString() + "_default_odds_5"));
                                    num306 = double.Parse(LSRequest.qq("t" + num296.ToString() + "_max_odds_5"));
                                    if (!this.max_odds_isopen.Equals("1"))
                                    {
                                        num306 = Convert.ToDouble(row22["max_odds"].ToString().Split(new char[] { '|' })[4]);
                                    }
                                    num311 = double.Parse(LSRequest.qq("t" + num296.ToString() + "_min_odds_5"));
                                    if (num298 > num303)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能大於賠率上限！", str49), null, 400));
                                        base.Response.End();
                                    }
                                    if (num298 < num308)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能小於賠率下限！", str49), null, 400));
                                        base.Response.End();
                                    }
                                    if (num303 < num308)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率上限不能小於賠率下限！", str49), null, 400));
                                        base.Response.End();
                                    }
                                    if (num299 > num304)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能大於賠率上限！", str49), null, 400));
                                        base.Response.End();
                                    }
                                    if (num299 < num309)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能小於賠率下限！", str49), null, 400));
                                        base.Response.End();
                                    }
                                    if (num304 < num309)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率上限不能小於賠率下限！", str49), null, 400));
                                        base.Response.End();
                                    }
                                    if (num300 > num305)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能大於賠率上限！", str49), null, 400));
                                        base.Response.End();
                                    }
                                    if (num300 < num310)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能小於賠率下限！", str49), null, 400));
                                        base.Response.End();
                                    }
                                    if (num305 < num310)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率上限不能小於賠率下限！", str49), null, 400));
                                        base.Response.End();
                                    }
                                    if (num301 > num306)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能大於賠率上限！", str49), null, 400));
                                        base.Response.End();
                                    }
                                    if (num301 < num311)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能小於賠率下限！", str49), null, 400));
                                        base.Response.End();
                                    }
                                    if (num306 < num311)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率上限不能小於賠率下限！", str49), null, 400));
                                        base.Response.End();
                                    }
                                    _xyftsg.set_default_odds(string.Concat(new object[] { num297, "|", num298, "|", num299, "|", num300, "|", num301 }));
                                    _xyftsg.set_max_odds(string.Concat(new object[] { num302, "|", num303, "|", num304, "|", num305, "|", num306 }));
                                    _xyftsg.set_min_odds(string.Concat(new object[] { num307, "|", num308, "|", num309, "|", num310, "|", num311 }));
                                    break;

                                case 5:
                                case 6:
                                    num298 = double.Parse(LSRequest.qq("t" + num296.ToString() + "_default_odds_2"));
                                    num303 = double.Parse(LSRequest.qq("t" + num296.ToString() + "_max_odds_2"));
                                    if (!this.max_odds_isopen.Equals("1"))
                                    {
                                        num303 = Convert.ToDouble(row22["max_odds"].ToString().Split(new char[] { '|' })[1]);
                                    }
                                    num308 = double.Parse(LSRequest.qq("t" + num296.ToString() + "_min_odds_2"));
                                    if (num298 > num303)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能大於賠率上限！", str49), null, 400));
                                        base.Response.End();
                                    }
                                    if (num298 < num308)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能小於賠率下限！", str49), null, 400));
                                        base.Response.End();
                                    }
                                    if (num303 < num308)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率上限不能小於賠率下限！", str49), null, 400));
                                        base.Response.End();
                                    }
                                    _xyftsg.set_default_odds(num297 + "|" + num298);
                                    _xyftsg.set_max_odds(num302 + "|" + num303);
                                    _xyftsg.set_min_odds(num307 + "|" + num308);
                                    break;

                                default:
                                    _xyftsg.set_default_odds(num297.ToString());
                                    _xyftsg.set_max_odds(num302.ToString());
                                    _xyftsg.set_min_odds(num307.ToString());
                                    break;
                            }
                            _xyftsg.set_b_diff(new decimal?(Convert.ToDecimal(LSRequest.qq("t" + num296.ToString() + "_b_diff"))));
                            _xyftsg.set_c_diff(new decimal?(Convert.ToDecimal(LSRequest.qq("t" + num296.ToString() + "_c_diff"))));
                            if (!this.max_odds_isopen.Equals("1"))
                            {
                                _xyftsg.set_b_diff(new decimal?(Convert.ToDecimal(row22["b_diff"].ToString())));
                                _xyftsg.set_c_diff(new decimal?(Convert.ToDecimal(row22["c_diff"].ToString())));
                            }
                            _xyftsg.set_downbase(new decimal?(Convert.ToDecimal(LSRequest.qq("t" + num296.ToString() + "_downbase"))));
                            _xyftsg.set_down_odds_rate(new decimal?(Convert.ToDecimal(LSRequest.qq("t" + num296.ToString() + "_down_odds_rate"))));
                            _xyftsg.set_allow_min_amount(new decimal?(decimal.Parse(row22["allow_min_amount"].ToString())));
                            string str50 = "";
                            switch (num296)
                            {
                                case 0:
                                    str50 = "1,5,9,13,17,21,24,27,30,33";
                                    break;

                                case 1:
                                    str50 = "2,6,10,14,18,22,25,28,31,34";
                                    break;

                                case 2:
                                    str50 = "3,7,11,15,19,23,26,29,32,35";
                                    break;

                                case 3:
                                    str50 = "4,8,12,16,20";
                                    break;

                                case 4:
                                    str50 = row22["play_id"].ToString();
                                    break;

                                case 5:
                                    str50 = row22["play_id"].ToString();
                                    break;

                                case 6:
                                    str50 = row22["play_id"].ToString();
                                    break;

                                default:
                                    str50 = row22["play_id"].ToString();
                                    break;
                            }
                            CallBLL.cz_play_xyftsg_bll.SetPlayByOddsSet(str50, _xyftsg);
                            num296++;
                        }
                        base.plsd_kc_log(rs, CallBLL.cz_play_xyftsg_bll.GetPlayByOddsSet("1,2,3,4,36,37,38").Tables[0], this.lotteryId);
                    }
                    break;
                }
                case 0x16:
                {
                    DataSet set23 = CallBLL.cz_play_happycar_bll.GetPlayByOddsSet("1,2,3,4,36,37,38");
                    if (((set23 != null) && (set23.Tables.Count > 0)) && (set23.Tables[0].Rows.Count > 0))
                    {
                        rs = set23.Tables[0];
                        this.ValidInput_HAPPYCAR(rs);
                        int num312 = 0;
                        foreach (DataRow row23 in rs.Rows)
                        {
                            string str51 = row23["play_name"].ToString().Trim().Replace("冠軍龍虎", "1-5名龍虎").Replace("冠軍", "1-10名");
                            cz_play_happycar _happycar = new cz_play_happycar();
                            double num313 = 0.0;
                            double num314 = 0.0;
                            double num315 = 0.0;
                            double num316 = 0.0;
                            double num317 = 0.0;
                            double num318 = 0.0;
                            double num319 = 0.0;
                            double num320 = 0.0;
                            double num321 = 0.0;
                            double num322 = 0.0;
                            double num323 = 0.0;
                            double num324 = 0.0;
                            double num325 = 0.0;
                            double num326 = 0.0;
                            double num327 = 0.0;
                            num313 = double.Parse(LSRequest.qq("t" + num312.ToString() + "_default_odds_1"));
                            num318 = double.Parse(LSRequest.qq("t" + num312.ToString() + "_max_odds_1"));
                            if (!this.max_odds_isopen.Equals("1"))
                            {
                                num318 = Convert.ToDouble(row23["max_odds"].ToString().Split(new char[] { '|' })[0]);
                            }
                            num323 = double.Parse(LSRequest.qq("t" + num312.ToString() + "_min_odds_1"));
                            if (num313 > num318)
                            {
                                base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能大於賠率上限！", str51), null, 400));
                                base.Response.End();
                            }
                            if (num313 < num323)
                            {
                                base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能小於賠率下限！", str51), null, 400));
                                base.Response.End();
                            }
                            if (num318 < num323)
                            {
                                base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率上限不能小於賠率下限！", str51), null, 400));
                                base.Response.End();
                            }
                            switch (num312)
                            {
                                case 4:
                                    num314 = double.Parse(LSRequest.qq("t" + num312.ToString() + "_default_odds_2"));
                                    num319 = double.Parse(LSRequest.qq("t" + num312.ToString() + "_max_odds_2"));
                                    if (!this.max_odds_isopen.Equals("1"))
                                    {
                                        num319 = Convert.ToDouble(row23["max_odds"].ToString().Split(new char[] { '|' })[1]);
                                    }
                                    num324 = double.Parse(LSRequest.qq("t" + num312.ToString() + "_min_odds_2"));
                                    num315 = double.Parse(LSRequest.qq("t" + num312.ToString() + "_default_odds_3"));
                                    num320 = double.Parse(LSRequest.qq("t" + num312.ToString() + "_max_odds_3"));
                                    if (!this.max_odds_isopen.Equals("1"))
                                    {
                                        num320 = Convert.ToDouble(row23["max_odds"].ToString().Split(new char[] { '|' })[2]);
                                    }
                                    num325 = double.Parse(LSRequest.qq("t" + num312.ToString() + "_min_odds_3"));
                                    num316 = double.Parse(LSRequest.qq("t" + num312.ToString() + "_default_odds_4"));
                                    num321 = double.Parse(LSRequest.qq("t" + num312.ToString() + "_max_odds_4"));
                                    if (!this.max_odds_isopen.Equals("1"))
                                    {
                                        num321 = Convert.ToDouble(row23["max_odds"].ToString().Split(new char[] { '|' })[3]);
                                    }
                                    num326 = double.Parse(LSRequest.qq("t" + num312.ToString() + "_min_odds_4"));
                                    num317 = double.Parse(LSRequest.qq("t" + num312.ToString() + "_default_odds_5"));
                                    num322 = double.Parse(LSRequest.qq("t" + num312.ToString() + "_max_odds_5"));
                                    if (!this.max_odds_isopen.Equals("1"))
                                    {
                                        num322 = Convert.ToDouble(row23["max_odds"].ToString().Split(new char[] { '|' })[4]);
                                    }
                                    num327 = double.Parse(LSRequest.qq("t" + num312.ToString() + "_min_odds_5"));
                                    if (num314 > num319)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能大於賠率上限！", str51), null, 400));
                                        base.Response.End();
                                    }
                                    if (num314 < num324)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能小於賠率下限！", str51), null, 400));
                                        base.Response.End();
                                    }
                                    if (num319 < num324)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率上限不能小於賠率下限！", str51), null, 400));
                                        base.Response.End();
                                    }
                                    if (num315 > num320)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能大於賠率上限！", str51), null, 400));
                                        base.Response.End();
                                    }
                                    if (num315 < num325)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能小於賠率下限！", str51), null, 400));
                                        base.Response.End();
                                    }
                                    if (num320 < num325)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率上限不能小於賠率下限！", str51), null, 400));
                                        base.Response.End();
                                    }
                                    if (num316 > num321)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能大於賠率上限！", str51), null, 400));
                                        base.Response.End();
                                    }
                                    if (num316 < num326)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能小於賠率下限！", str51), null, 400));
                                        base.Response.End();
                                    }
                                    if (num321 < num326)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率上限不能小於賠率下限！", str51), null, 400));
                                        base.Response.End();
                                    }
                                    if (num317 > num322)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能大於賠率上限！", str51), null, 400));
                                        base.Response.End();
                                    }
                                    if (num317 < num327)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能小於賠率下限！", str51), null, 400));
                                        base.Response.End();
                                    }
                                    if (num322 < num327)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率上限不能小於賠率下限！", str51), null, 400));
                                        base.Response.End();
                                    }
                                    _happycar.set_default_odds(string.Concat(new object[] { num313, "|", num314, "|", num315, "|", num316, "|", num317 }));
                                    _happycar.set_max_odds(string.Concat(new object[] { num318, "|", num319, "|", num320, "|", num321, "|", num322 }));
                                    _happycar.set_min_odds(string.Concat(new object[] { num323, "|", num324, "|", num325, "|", num326, "|", num327 }));
                                    break;

                                case 5:
                                case 6:
                                    num314 = double.Parse(LSRequest.qq("t" + num312.ToString() + "_default_odds_2"));
                                    num319 = double.Parse(LSRequest.qq("t" + num312.ToString() + "_max_odds_2"));
                                    if (!this.max_odds_isopen.Equals("1"))
                                    {
                                        num319 = Convert.ToDouble(row23["max_odds"].ToString().Split(new char[] { '|' })[1]);
                                    }
                                    num324 = double.Parse(LSRequest.qq("t" + num312.ToString() + "_min_odds_2"));
                                    if (num314 > num319)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能大於賠率上限！", str51), null, 400));
                                        base.Response.End();
                                    }
                                    if (num314 < num324)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能小於賠率下限！", str51), null, 400));
                                        base.Response.End();
                                    }
                                    if (num319 < num324)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率上限不能小於賠率下限！", str51), null, 400));
                                        base.Response.End();
                                    }
                                    _happycar.set_default_odds(num313 + "|" + num314);
                                    _happycar.set_max_odds(num318 + "|" + num319);
                                    _happycar.set_min_odds(num323 + "|" + num324);
                                    break;

                                default:
                                    _happycar.set_default_odds(num313.ToString());
                                    _happycar.set_max_odds(num318.ToString());
                                    _happycar.set_min_odds(num323.ToString());
                                    break;
                            }
                            _happycar.set_b_diff(new decimal?(Convert.ToDecimal(LSRequest.qq("t" + num312.ToString() + "_b_diff"))));
                            _happycar.set_c_diff(new decimal?(Convert.ToDecimal(LSRequest.qq("t" + num312.ToString() + "_c_diff"))));
                            if (!this.max_odds_isopen.Equals("1"))
                            {
                                _happycar.set_b_diff(new decimal?(Convert.ToDecimal(row23["b_diff"].ToString())));
                                _happycar.set_c_diff(new decimal?(Convert.ToDecimal(row23["c_diff"].ToString())));
                            }
                            _happycar.set_downbase(new decimal?(Convert.ToDecimal(LSRequest.qq("t" + num312.ToString() + "_downbase"))));
                            _happycar.set_down_odds_rate(new decimal?(Convert.ToDecimal(LSRequest.qq("t" + num312.ToString() + "_down_odds_rate"))));
                            _happycar.set_allow_min_amount(new decimal?(decimal.Parse(row23["allow_min_amount"].ToString())));
                            string str52 = "";
                            switch (num312)
                            {
                                case 0:
                                    str52 = "1,5,9,13,17,21,24,27,30,33";
                                    break;

                                case 1:
                                    str52 = "2,6,10,14,18,22,25,28,31,34";
                                    break;

                                case 2:
                                    str52 = "3,7,11,15,19,23,26,29,32,35";
                                    break;

                                case 3:
                                    str52 = "4,8,12,16,20";
                                    break;

                                case 4:
                                    str52 = row23["play_id"].ToString();
                                    break;

                                case 5:
                                    str52 = row23["play_id"].ToString();
                                    break;

                                case 6:
                                    str52 = row23["play_id"].ToString();
                                    break;

                                default:
                                    str52 = row23["play_id"].ToString();
                                    break;
                            }
                            CallBLL.cz_play_happycar_bll.SetPlayByOddsSet(str52, _happycar);
                            num312++;
                        }
                        base.plsd_kc_log(rs, CallBLL.cz_play_happycar_bll.GetPlayByOddsSet("1,2,3,4,36,37,38").Tables[0], this.lotteryId);
                    }
                    break;
                }
            }
            string url = "/OddsSet/OddsSet_kc.aspx?lid=" + this.lotteryId;
            base.Response.Write(base.ShowDialogBox("賠率設定參數已經成功保存！", url, 0));
            base.Response.End();
        }

        private void ValidInput_CAR168(DataTable rs)
        {
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string str = rs.Rows[i]["play_name"].ToString().Trim().Replace("冠軍龍虎", "1-5名龍虎").Replace("冠軍", "1-10名");
                if (((string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_b_diff")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_c_diff"))) || (string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_downbase")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_down_odds_rate")))) || ((string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_default_odds_1")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_max_odds_1"))) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_min_odds_1"))))
                {
                    base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率設定不能為空！", str), null, 400));
                    base.Response.End();
                }
                if ((i == 4) && ((((string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_default_odds_2")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_default_odds_3"))) || (string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_default_odds_4")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_default_odds_5")))) || ((string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_max_odds_2")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_max_odds_3"))) || (string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_max_odds_4")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_max_odds_5"))))) || ((string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_min_odds_2")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_min_odds_3"))) || (string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_min_odds_4")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_min_odds_5"))))))
                {
                    base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率設定不能為空！", str), null, 400));
                    base.Response.End();
                }
                if (((i == 5) || (i == 6)) && ((string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_default_odds_2")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_max_odds_2"))) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_min_odds_2"))))
                {
                    base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率設定不能為空！", str), null, 400));
                    base.Response.End();
                }
            }
            Regex regex = new Regex(@"^[+-]?\d+(\.\d+)?$");
            for (int j = 0; j < rs.Rows.Count; j++)
            {
                string str2 = rs.Rows[j]["play_name"].ToString().Trim().Replace("冠軍龍虎", "1-5名龍虎").Replace("冠軍", "1-10名");
                if (((!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_b_diff")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_c_diff"))) || (!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_downbase")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_down_odds_rate")))) || ((!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_default_odds_1")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_max_odds_1"))) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_min_odds_1"))))
                {
                    base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率設定必須為數字或小數！", str2), null, 400));
                    base.Response.End();
                }
                if ((j == 4) && ((((!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_default_odds_2")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_default_odds_3"))) || (!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_default_odds_4")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_default_odds_5")))) || ((!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_max_odds_2")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_max_odds_3"))) || (!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_max_odds_4")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_max_odds_5"))))) || ((!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_min_odds_2")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_min_odds_3"))) || (!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_min_odds_4")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_min_odds_5"))))))
                {
                    base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率設定必須為數字或小數！", str2), null, 400));
                    base.Response.End();
                }
                if (((j == 5) || (j == 6)) && ((!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_default_odds_2")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_max_odds_2"))) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_min_odds_2"))))
                {
                    base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率設定必須為數字或小數！", str2), null, 400));
                    base.Response.End();
                }
            }
        }

        private void ValidInput_CQSC(DataTable rs)
        {
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string str = rs.Rows[i]["play_name"].ToString().Trim().Replace("第一球", "1-5球");
                if (((string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_b_diff")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_c_diff"))) || (string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_downbase")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_down_odds_rate")))) || ((string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_default_odds_1")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_max_odds_1"))) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_min_odds_1"))))
                {
                    base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率設定不能為空！", str), null, 400));
                    base.Response.End();
                }
                if ((i == 5) && ((string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_default_odds_2")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_max_odds_2"))) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_min_odds_2"))))
                {
                    base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率設定不能為空！", str), null, 400));
                    base.Response.End();
                }
                if ((i == 6) && ((((string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_default_odds_2")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_default_odds_3"))) || (string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_default_odds_4")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_default_odds_5")))) || ((string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_max_odds_2")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_max_odds_3"))) || (string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_max_odds_4")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_max_odds_5"))))) || ((string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_min_odds_2")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_min_odds_3"))) || (string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_min_odds_4")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_min_odds_5"))))))
                {
                    base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率設定不能為空！", str), null, 400));
                    base.Response.End();
                }
            }
            Regex regex = new Regex(@"^[+-]?\d+(\.\d+)?$");
            for (int j = 0; j < rs.Rows.Count; j++)
            {
                string str2 = rs.Rows[j]["play_name"].ToString().Trim().Replace("第一球", "1-5球");
                if (((!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_b_diff")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_c_diff"))) || (!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_downbase")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_down_odds_rate")))) || ((!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_default_odds_1")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_max_odds_1"))) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_min_odds_1"))))
                {
                    base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率設定必須為數字或小數！", str2), null, 400));
                    base.Response.End();
                }
                if ((j == 5) && ((!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_default_odds_2")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_max_odds_2"))) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_min_odds_2"))))
                {
                    base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率設定必須為數字或小數！", str2), null, 400));
                    base.Response.End();
                }
                if ((j == 6) && ((((!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_default_odds_2")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_default_odds_3"))) || (!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_default_odds_4")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_default_odds_5")))) || ((!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_max_odds_2")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_max_odds_3"))) || (!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_max_odds_4")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_max_odds_5"))))) || ((!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_min_odds_2")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_min_odds_3"))) || (!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_min_odds_4")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_min_odds_5"))))))
                {
                    base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率設定必須為數字或小數！", str2), null, 400));
                    base.Response.End();
                }
            }
        }

        private void ValidInput_HAPPYCAR(DataTable rs)
        {
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string str = rs.Rows[i]["play_name"].ToString().Trim().Replace("冠軍龍虎", "1-5名龍虎").Replace("冠軍", "1-10名");
                if (((string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_b_diff")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_c_diff"))) || (string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_downbase")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_down_odds_rate")))) || ((string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_default_odds_1")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_max_odds_1"))) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_min_odds_1"))))
                {
                    base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率設定不能為空！", str), null, 400));
                    base.Response.End();
                }
                if ((i == 4) && ((((string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_default_odds_2")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_default_odds_3"))) || (string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_default_odds_4")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_default_odds_5")))) || ((string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_max_odds_2")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_max_odds_3"))) || (string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_max_odds_4")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_max_odds_5"))))) || ((string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_min_odds_2")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_min_odds_3"))) || (string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_min_odds_4")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_min_odds_5"))))))
                {
                    base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率設定不能為空！", str), null, 400));
                    base.Response.End();
                }
                if (((i == 5) || (i == 6)) && ((string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_default_odds_2")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_max_odds_2"))) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_min_odds_2"))))
                {
                    base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率設定不能為空！", str), null, 400));
                    base.Response.End();
                }
            }
            Regex regex = new Regex(@"^[+-]?\d+(\.\d+)?$");
            for (int j = 0; j < rs.Rows.Count; j++)
            {
                string str2 = rs.Rows[j]["play_name"].ToString().Trim().Replace("冠軍龍虎", "1-5名龍虎").Replace("冠軍", "1-10名");
                if (((!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_b_diff")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_c_diff"))) || (!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_downbase")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_down_odds_rate")))) || ((!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_default_odds_1")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_max_odds_1"))) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_min_odds_1"))))
                {
                    base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率設定必須為數字或小數！", str2), null, 400));
                    base.Response.End();
                }
                if ((j == 4) && ((((!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_default_odds_2")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_default_odds_3"))) || (!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_default_odds_4")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_default_odds_5")))) || ((!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_max_odds_2")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_max_odds_3"))) || (!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_max_odds_4")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_max_odds_5"))))) || ((!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_min_odds_2")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_min_odds_3"))) || (!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_min_odds_4")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_min_odds_5"))))))
                {
                    base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率設定必須為數字或小數！", str2), null, 400));
                    base.Response.End();
                }
                if (((j == 5) || (j == 6)) && ((!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_default_odds_2")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_max_odds_2"))) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_min_odds_2"))))
                {
                    base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率設定必須為數字或小數！", str2), null, 400));
                    base.Response.End();
                }
            }
        }

        private void ValidInput_JSCAR(DataTable rs)
        {
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string str = rs.Rows[i]["play_name"].ToString().Trim().Replace("冠軍龍虎", "1-5名龍虎").Replace("冠軍", "1-10名");
                if (((string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_b_diff")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_c_diff"))) || (string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_downbase")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_down_odds_rate")))) || ((string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_default_odds_1")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_max_odds_1"))) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_min_odds_1"))))
                {
                    base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率設定不能為空！", str), null, 400));
                    base.Response.End();
                }
                if ((i == 4) && ((((string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_default_odds_2")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_default_odds_3"))) || (string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_default_odds_4")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_default_odds_5")))) || ((string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_max_odds_2")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_max_odds_3"))) || (string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_max_odds_4")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_max_odds_5"))))) || ((string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_min_odds_2")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_min_odds_3"))) || (string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_min_odds_4")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_min_odds_5"))))))
                {
                    base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率設定不能為空！", str), null, 400));
                    base.Response.End();
                }
                if (((i == 5) || (i == 6)) && ((string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_default_odds_2")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_max_odds_2"))) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_min_odds_2"))))
                {
                    base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率設定不能為空！", str), null, 400));
                    base.Response.End();
                }
            }
            Regex regex = new Regex(@"^[+-]?\d+(\.\d+)?$");
            for (int j = 0; j < rs.Rows.Count; j++)
            {
                string str2 = rs.Rows[j]["play_name"].ToString().Trim().Replace("冠軍龍虎", "1-5名龍虎").Replace("冠軍", "1-10名");
                if (((!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_b_diff")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_c_diff"))) || (!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_downbase")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_down_odds_rate")))) || ((!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_default_odds_1")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_max_odds_1"))) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_min_odds_1"))))
                {
                    base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率設定必須為數字或小數！", str2), null, 400));
                    base.Response.End();
                }
                if ((j == 4) && ((((!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_default_odds_2")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_default_odds_3"))) || (!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_default_odds_4")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_default_odds_5")))) || ((!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_max_odds_2")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_max_odds_3"))) || (!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_max_odds_4")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_max_odds_5"))))) || ((!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_min_odds_2")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_min_odds_3"))) || (!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_min_odds_4")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_min_odds_5"))))))
                {
                    base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率設定必須為數字或小數！", str2), null, 400));
                    base.Response.End();
                }
                if (((j == 5) || (j == 6)) && ((!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_default_odds_2")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_max_odds_2"))) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_min_odds_2"))))
                {
                    base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率設定必須為數字或小數！", str2), null, 400));
                    base.Response.End();
                }
            }
        }

        private void ValidInput_JSCQSC(DataTable rs)
        {
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string str = rs.Rows[i]["play_name"].ToString().Trim().Replace("第一球", "1-5球");
                if (((string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_b_diff")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_c_diff"))) || (string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_downbase")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_down_odds_rate")))) || ((string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_default_odds_1")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_max_odds_1"))) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_min_odds_1"))))
                {
                    base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率設定不能為空！", str), null, 400));
                    base.Response.End();
                }
                if ((i == 5) && ((string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_default_odds_2")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_max_odds_2"))) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_min_odds_2"))))
                {
                    base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率設定不能為空！", str), null, 400));
                    base.Response.End();
                }
                if ((i == 6) && ((((string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_default_odds_2")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_default_odds_3"))) || (string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_default_odds_4")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_default_odds_5")))) || ((string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_max_odds_2")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_max_odds_3"))) || (string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_max_odds_4")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_max_odds_5"))))) || ((string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_min_odds_2")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_min_odds_3"))) || (string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_min_odds_4")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_min_odds_5"))))))
                {
                    base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率設定不能為空！", str), null, 400));
                    base.Response.End();
                }
            }
            Regex regex = new Regex(@"^[+-]?\d+(\.\d+)?$");
            for (int j = 0; j < rs.Rows.Count; j++)
            {
                string str2 = rs.Rows[j]["play_name"].ToString().Trim().Replace("第一球", "1-5球");
                if (((!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_b_diff")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_c_diff"))) || (!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_downbase")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_down_odds_rate")))) || ((!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_default_odds_1")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_max_odds_1"))) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_min_odds_1"))))
                {
                    base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率設定必須為數字或小數！", str2), null, 400));
                    base.Response.End();
                }
                if ((j == 5) && ((!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_default_odds_2")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_max_odds_2"))) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_min_odds_2"))))
                {
                    base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率設定必須為數字或小數！", str2), null, 400));
                    base.Response.End();
                }
                if ((j == 6) && ((((!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_default_odds_2")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_default_odds_3"))) || (!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_default_odds_4")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_default_odds_5")))) || ((!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_max_odds_2")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_max_odds_3"))) || (!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_max_odds_4")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_max_odds_5"))))) || ((!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_min_odds_2")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_min_odds_3"))) || (!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_min_odds_4")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_min_odds_5"))))))
                {
                    base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率設定必須為數字或小數！", str2), null, 400));
                    base.Response.End();
                }
            }
        }

        private void ValidInput_JSFT2(DataTable rs)
        {
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string str = rs.Rows[i]["play_name"].ToString().Trim().Replace("冠軍龍虎", "1-5名龍虎").Replace("冠軍", "1-10名");
                if (((string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_b_diff")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_c_diff"))) || (string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_downbase")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_down_odds_rate")))) || ((string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_default_odds_1")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_max_odds_1"))) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_min_odds_1"))))
                {
                    base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率設定不能為空！", str), null, 400));
                    base.Response.End();
                }
                if ((i == 4) && ((((string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_default_odds_2")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_default_odds_3"))) || (string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_default_odds_4")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_default_odds_5")))) || ((string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_max_odds_2")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_max_odds_3"))) || (string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_max_odds_4")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_max_odds_5"))))) || ((string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_min_odds_2")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_min_odds_3"))) || (string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_min_odds_4")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_min_odds_5"))))))
                {
                    base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率設定不能為空！", str), null, 400));
                    base.Response.End();
                }
                if (((i == 5) || (i == 6)) && ((string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_default_odds_2")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_max_odds_2"))) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_min_odds_2"))))
                {
                    base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率設定不能為空！", str), null, 400));
                    base.Response.End();
                }
            }
            Regex regex = new Regex(@"^[+-]?\d+(\.\d+)?$");
            for (int j = 0; j < rs.Rows.Count; j++)
            {
                string str2 = rs.Rows[j]["play_name"].ToString().Trim().Replace("冠軍龍虎", "1-5名龍虎").Replace("冠軍", "1-10名");
                if (((!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_b_diff")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_c_diff"))) || (!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_downbase")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_down_odds_rate")))) || ((!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_default_odds_1")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_max_odds_1"))) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_min_odds_1"))))
                {
                    base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率設定必須為數字或小數！", str2), null, 400));
                    base.Response.End();
                }
                if ((j == 4) && ((((!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_default_odds_2")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_default_odds_3"))) || (!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_default_odds_4")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_default_odds_5")))) || ((!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_max_odds_2")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_max_odds_3"))) || (!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_max_odds_4")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_max_odds_5"))))) || ((!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_min_odds_2")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_min_odds_3"))) || (!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_min_odds_4")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_min_odds_5"))))))
                {
                    base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率設定必須為數字或小數！", str2), null, 400));
                    base.Response.End();
                }
                if (((j == 5) || (j == 6)) && ((!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_default_odds_2")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_max_odds_2"))) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_min_odds_2"))))
                {
                    base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率設定必須為數字或小數！", str2), null, 400));
                    base.Response.End();
                }
            }
        }

        private void ValidInput_JSK3(DataTable rs)
        {
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string str = rs.Rows[i]["play_name"].ToString().Trim();
                if (((string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_b_diff")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_c_diff"))) || (string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_downbase")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_down_odds_rate")))) || ((string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_default_odds_1")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_max_odds_1"))) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_min_odds_1"))))
                {
                    base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率設定不能為空！", str), null, 400));
                    base.Response.End();
                }
                if (i == 4)
                {
                    for (int k = 1; k < 14; k++)
                    {
                        if ((string.IsNullOrEmpty(LSRequest.qq(string.Concat(new object[] { "t", i.ToString(), "_default_odds_", k + 1 }))) || string.IsNullOrEmpty(LSRequest.qq(string.Concat(new object[] { "t", i.ToString(), "_max_odds_", k + 1 })))) || string.IsNullOrEmpty(LSRequest.qq(string.Concat(new object[] { "t", i.ToString(), "_min_odds_", k + 1 }))))
                        {
                            base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率設定不能為空！", str), null, 400));
                            base.Response.End();
                        }
                    }
                }
            }
            Regex regex = new Regex(@"^[+-]?\d+(\.\d+)?$");
            for (int j = 0; j < rs.Rows.Count; j++)
            {
                string str2 = rs.Rows[j]["play_name"].ToString().Trim();
                if (((!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_b_diff")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_c_diff"))) || (!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_downbase")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_down_odds_rate")))) || ((!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_default_odds_1")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_max_odds_1"))) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_min_odds_1"))))
                {
                    base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率設定必須為數字或小數！", str2), null, 400));
                    base.Response.End();
                }
                if (j == 4)
                {
                    for (int m = 1; m < 14; m++)
                    {
                        if ((!regex.IsMatch(LSRequest.qq(string.Concat(new object[] { "t", j.ToString(), "_default_odds_", m + 1 }))) || !regex.IsMatch(LSRequest.qq(string.Concat(new object[] { "t", j.ToString(), "_max_odds_", m + 1 })))) || !regex.IsMatch(LSRequest.qq(string.Concat(new object[] { "t", j.ToString(), "_min_odds_", m + 1 }))))
                        {
                            base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率設定必須為數字或小數！", str2), null, 400));
                            base.Response.End();
                        }
                    }
                }
            }
        }

        private void ValidInput_JSPK10(DataTable rs)
        {
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string str = rs.Rows[i]["play_name"].ToString().Trim().Replace("冠軍龍虎", "1-5名龍虎").Replace("冠軍", "1-10名");
                if (((string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_b_diff")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_c_diff"))) || (string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_downbase")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_down_odds_rate")))) || ((string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_default_odds_1")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_max_odds_1"))) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_min_odds_1"))))
                {
                    base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率設定不能為空！", str), null, 400));
                    base.Response.End();
                }
                if ((i == 4) && ((((string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_default_odds_2")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_default_odds_3"))) || (string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_default_odds_4")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_default_odds_5")))) || ((string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_max_odds_2")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_max_odds_3"))) || (string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_max_odds_4")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_max_odds_5"))))) || ((string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_min_odds_2")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_min_odds_3"))) || (string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_min_odds_4")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_min_odds_5"))))))
                {
                    base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率設定不能為空！", str), null, 400));
                    base.Response.End();
                }
                if (((i == 5) || (i == 6)) && ((string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_default_odds_2")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_max_odds_2"))) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_min_odds_2"))))
                {
                    base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率設定不能為空！", str), null, 400));
                    base.Response.End();
                }
            }
            Regex regex = new Regex(@"^[+-]?\d+(\.\d+)?$");
            for (int j = 0; j < rs.Rows.Count; j++)
            {
                string str2 = rs.Rows[j]["play_name"].ToString().Trim().Replace("冠軍龍虎", "1-5名龍虎").Replace("冠軍", "1-10名");
                if (((!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_b_diff")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_c_diff"))) || (!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_downbase")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_down_odds_rate")))) || ((!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_default_odds_1")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_max_odds_1"))) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_min_odds_1"))))
                {
                    base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率設定必須為數字或小數！", str2), null, 400));
                    base.Response.End();
                }
                if ((j == 4) && ((((!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_default_odds_2")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_default_odds_3"))) || (!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_default_odds_4")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_default_odds_5")))) || ((!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_max_odds_2")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_max_odds_3"))) || (!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_max_odds_4")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_max_odds_5"))))) || ((!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_min_odds_2")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_min_odds_3"))) || (!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_min_odds_4")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_min_odds_5"))))))
                {
                    base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率設定必須為數字或小數！", str2), null, 400));
                    base.Response.End();
                }
                if (((j == 5) || (j == 6)) && ((!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_default_odds_2")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_max_odds_2"))) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_min_odds_2"))))
                {
                    base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率設定必須為數字或小數！", str2), null, 400));
                    base.Response.End();
                }
            }
        }

        private void ValidInput_JSSFC(DataTable rs)
        {
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string str = rs.Rows[i]["play_name"].ToString().Trim().Replace("第一球", "1-8球");
                if (((string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_b_diff")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_c_diff"))) || (string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_downbase")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_down_odds_rate")))) || ((string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_default_odds_1")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_max_odds_1"))) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_min_odds_1"))))
                {
                    base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率設定不能為空！", str), null, 400));
                    base.Response.End();
                }
                if ((i == 6) && ((string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_default_odds_2")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_max_odds_2"))) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_min_odds_2"))))
                {
                    base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率設定不能為空！", str), null, 400));
                    base.Response.End();
                }
            }
            Regex regex = new Regex(@"^[+-]?\d+(\.\d+)?$");
            for (int j = 0; j < rs.Rows.Count; j++)
            {
                string str2 = rs.Rows[j]["play_name"].ToString().Trim().Replace("第一球", "1-8球");
                if (((!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_b_diff")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_c_diff"))) || (!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_downbase")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_down_odds_rate")))) || ((!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_default_odds_1")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_max_odds_1"))) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_min_odds_1"))))
                {
                    base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率設定必須為數字或小數！", str2), null, 400));
                    base.Response.End();
                }
                if ((j == 6) && ((!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_default_odds_2")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_max_odds_2"))) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_min_odds_2"))))
                {
                    base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率設定必須為數字或小數！", str2), null, 400));
                    base.Response.End();
                }
            }
        }

        private void ValidInput_K8SC(DataTable rs)
        {
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string str = rs.Rows[i]["play_name"].ToString().Trim().Replace("第一球", "1-5球");
                if (((string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_b_diff")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_c_diff"))) || (string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_downbase")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_down_odds_rate")))) || ((string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_default_odds_1")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_max_odds_1"))) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_min_odds_1"))))
                {
                    base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率設定不能為空！", str), null, 400));
                    base.Response.End();
                }
                if ((i == 5) && ((string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_default_odds_2")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_max_odds_2"))) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_min_odds_2"))))
                {
                    base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率設定不能為空！", str), null, 400));
                    base.Response.End();
                }
                if ((i == 6) && ((((string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_default_odds_2")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_default_odds_3"))) || (string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_default_odds_4")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_default_odds_5")))) || ((string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_max_odds_2")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_max_odds_3"))) || (string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_max_odds_4")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_max_odds_5"))))) || ((string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_min_odds_2")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_min_odds_3"))) || (string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_min_odds_4")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_min_odds_5"))))))
                {
                    base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率設定不能為空！", str), null, 400));
                    base.Response.End();
                }
            }
            Regex regex = new Regex(@"^[+-]?\d+(\.\d+)?$");
            for (int j = 0; j < rs.Rows.Count; j++)
            {
                string str2 = rs.Rows[j]["play_name"].ToString().Trim().Replace("第一球", "1-5球");
                if (((!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_b_diff")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_c_diff"))) || (!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_downbase")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_down_odds_rate")))) || ((!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_default_odds_1")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_max_odds_1"))) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_min_odds_1"))))
                {
                    base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率設定必須為數字或小數！", str2), null, 400));
                    base.Response.End();
                }
                if ((j == 5) && ((!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_default_odds_2")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_max_odds_2"))) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_min_odds_2"))))
                {
                    base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率設定必須為數字或小數！", str2), null, 400));
                    base.Response.End();
                }
                if ((j == 6) && ((((!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_default_odds_2")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_default_odds_3"))) || (!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_default_odds_4")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_default_odds_5")))) || ((!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_max_odds_2")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_max_odds_3"))) || (!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_max_odds_4")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_max_odds_5"))))) || ((!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_min_odds_2")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_min_odds_3"))) || (!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_min_odds_4")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_min_odds_5"))))))
                {
                    base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率設定必須為數字或小數！", str2), null, 400));
                    base.Response.End();
                }
            }
        }

        private void ValidInput_KL10(DataTable rs)
        {
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string str = rs.Rows[i]["play_name"].ToString().Trim().Replace("第一球", "1-8球");
                if (((string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_b_diff")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_c_diff"))) || (string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_downbase")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_down_odds_rate")))) || ((string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_default_odds_1")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_max_odds_1"))) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_min_odds_1"))))
                {
                    base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率設定不能為空！", str), null, 400));
                    base.Response.End();
                }
                if ((i == 6) && ((string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_default_odds_2")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_max_odds_2"))) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_min_odds_2"))))
                {
                    base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率設定不能為空！", str), null, 400));
                    base.Response.End();
                }
            }
            Regex regex = new Regex(@"^[+-]?\d+(\.\d+)?$");
            for (int j = 0; j < rs.Rows.Count; j++)
            {
                string str2 = rs.Rows[j]["play_name"].ToString().Trim().Replace("第一球", "1-8球");
                if (((!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_b_diff")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_c_diff"))) || (!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_downbase")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_down_odds_rate")))) || ((!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_default_odds_1")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_max_odds_1"))) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_min_odds_1"))))
                {
                    base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率設定必須為數字或小數！", str2), null, 400));
                    base.Response.End();
                }
                if ((j == 6) && ((!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_default_odds_2")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_max_odds_2"))) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_min_odds_2"))))
                {
                    base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率設定必須為數字或小數！", str2), null, 400));
                    base.Response.End();
                }
            }
        }

        private void ValidInput_KL8(DataTable rs)
        {
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string str = rs.Rows[i]["play_name"].ToString().Trim();
                if (((string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_b_diff")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_c_diff"))) || (string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_downbase")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_down_odds_rate")))) || ((string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_default_odds_1")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_max_odds_1"))) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_min_odds_1"))))
                {
                    base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率設定不能為空！", str), null, 400));
                    base.Response.End();
                }
                if (((i == 5) || (i == 6)) && ((string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_default_odds_2")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_max_odds_2"))) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_min_odds_2"))))
                {
                    base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率設定不能為空！", str), null, 400));
                    base.Response.End();
                }
                if ((i == 7) && ((((string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_default_odds_2")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_default_odds_3"))) || (string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_default_odds_4")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_default_odds_5")))) || ((string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_max_odds_2")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_max_odds_3"))) || (string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_max_odds_4")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_max_odds_5"))))) || ((string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_min_odds_2")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_min_odds_3"))) || (string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_min_odds_4")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_min_odds_5"))))))
                {
                    base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率設定不能為空！", str), null, 400));
                    base.Response.End();
                }
            }
            Regex regex = new Regex(@"^[+-]?\d+(\.\d+)?$");
            for (int j = 0; j < rs.Rows.Count; j++)
            {
                string str2 = rs.Rows[j]["play_name"].ToString().Trim();
                if (((!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_b_diff")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_c_diff"))) || (!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_downbase")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_down_odds_rate")))) || ((!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_default_odds_1")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_max_odds_1"))) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_min_odds_1"))))
                {
                    base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率設定必須為數字或小數！", str2), null, 400));
                    base.Response.End();
                }
                if (((j == 5) || (j == 6)) && ((!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_default_odds_2")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_max_odds_2"))) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_min_odds_2"))))
                {
                    base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率設定必須為數字或小數！", str2), null, 400));
                    base.Response.End();
                }
                if ((j == 7) && ((((!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_default_odds_2")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_default_odds_3"))) || (!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_default_odds_4")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_default_odds_5")))) || ((!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_max_odds_2")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_max_odds_3"))) || (!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_max_odds_4")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_max_odds_5"))))) || ((!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_min_odds_2")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_min_odds_3"))) || (!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_min_odds_4")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_min_odds_5"))))))
                {
                    base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率設定必須為數字或小數！", str2), null, 400));
                    base.Response.End();
                }
            }
        }

        private void ValidInput_PCDD(DataTable rs)
        {
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string str = rs.Rows[i]["play_name"].ToString().Trim().Replace("1區大小", "1-3區大小").Replace("1區單雙", "1-3區單雙");
                if (((string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_b_diff")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_c_diff"))) || (string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_downbase")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_down_odds_rate")))) || ((string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_default_odds_1")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_max_odds_1"))) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_min_odds_1"))))
                {
                    base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率設定不能為空！", str), null, 400));
                    base.Response.End();
                }
                if (i == 0)
                {
                    int num2 = 0x1a;
                    for (int k = 1; k < 14; k++)
                    {
                        if ((string.IsNullOrEmpty(LSRequest.qq(string.Concat(new object[] { "t", i.ToString(), "_default_odds_", k + 1 }))) || string.IsNullOrEmpty(LSRequest.qq(string.Concat(new object[] { "t", i.ToString(), "_max_odds_", k + 1 })))) || string.IsNullOrEmpty(LSRequest.qq(string.Concat(new object[] { "t", i.ToString(), "_min_odds_", k + 1 }))))
                        {
                            base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率設定不能為空！", str), null, 400));
                            base.Response.End();
                        }
                        num2--;
                    }
                }
                if ((i == 5) && (((string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_default_odds_2")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_max_odds_2"))) || (string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_min_odds_2")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_default_odds_3")))) || (string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_max_odds_3")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_min_odds_3")))))
                {
                    base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率設定不能為空！", str), null, 400));
                    base.Response.End();
                }
            }
            Regex regex = new Regex(@"^[+-]?\d+(\.\d+)?$");
            for (int j = 0; j < rs.Rows.Count; j++)
            {
                string str2 = rs.Rows[j]["play_name"].ToString().Trim().Replace("1區大小", "1-3區大小").Replace("1區單雙", "1-3區單雙");
                if (((!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_b_diff")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_c_diff"))) || (!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_downbase")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_down_odds_rate")))) || ((!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_default_odds_1")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_max_odds_1"))) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_min_odds_1"))))
                {
                    base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率設定必須為數字或小數！", str2), null, 400));
                    base.Response.End();
                }
                if (j == 0)
                {
                    int num5 = 0x1a;
                    for (int m = 1; m < 14; m++)
                    {
                        if ((!regex.IsMatch(LSRequest.qq(string.Concat(new object[] { "t", j.ToString(), "_default_odds_", m + 1 }))) || !regex.IsMatch(LSRequest.qq(string.Concat(new object[] { "t", j.ToString(), "_default_odds_", m + 1 })))) || !regex.IsMatch(LSRequest.qq(string.Concat(new object[] { "t", j.ToString(), "_default_odds_", m + 1 }))))
                        {
                            base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率設定必須為數字或小數！", str2), null, 400));
                            base.Response.End();
                        }
                        num5--;
                    }
                }
                if ((j == 5) && (((!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_default_odds_2")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_max_odds_2"))) || (!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_min_odds_2")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_default_odds_3")))) || (!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_max_odds_3")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_min_odds_3")))))
                {
                    base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率設定必須為數字或小數！", str2), null, 400));
                    base.Response.End();
                }
            }
        }

        private void ValidInput_PK10(DataTable rs)
        {
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string str = rs.Rows[i]["play_name"].ToString().Trim().Replace("冠軍龍虎", "1-5名龍虎").Replace("冠軍", "1-10名");
                if (((string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_b_diff")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_c_diff"))) || (string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_downbase")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_down_odds_rate")))) || ((string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_default_odds_1")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_max_odds_1"))) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_min_odds_1"))))
                {
                    base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率設定不能為空！", str), null, 400));
                    base.Response.End();
                }
                if ((i == 4) && ((((string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_default_odds_2")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_default_odds_3"))) || (string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_default_odds_4")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_default_odds_5")))) || ((string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_max_odds_2")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_max_odds_3"))) || (string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_max_odds_4")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_max_odds_5"))))) || ((string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_min_odds_2")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_min_odds_3"))) || (string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_min_odds_4")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_min_odds_5"))))))
                {
                    base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率設定不能為空！", str), null, 400));
                    base.Response.End();
                }
                if (((i == 5) || (i == 6)) && ((string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_default_odds_2")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_max_odds_2"))) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_min_odds_2"))))
                {
                    base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率設定不能為空！", str), null, 400));
                    base.Response.End();
                }
            }
            Regex regex = new Regex(@"^[+-]?\d+(\.\d+)?$");
            for (int j = 0; j < rs.Rows.Count; j++)
            {
                string str2 = rs.Rows[j]["play_name"].ToString().Trim().Replace("冠軍龍虎", "1-5名龍虎").Replace("冠軍", "1-10名");
                if (((!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_b_diff")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_c_diff"))) || (!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_downbase")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_down_odds_rate")))) || ((!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_default_odds_1")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_max_odds_1"))) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_min_odds_1"))))
                {
                    base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率設定必須為數字或小數！", str2), null, 400));
                    base.Response.End();
                }
                if ((j == 4) && ((((!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_default_odds_2")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_default_odds_3"))) || (!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_default_odds_4")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_default_odds_5")))) || ((!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_max_odds_2")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_max_odds_3"))) || (!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_max_odds_4")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_max_odds_5"))))) || ((!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_min_odds_2")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_min_odds_3"))) || (!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_min_odds_4")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_min_odds_5"))))))
                {
                    base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率設定必須為數字或小數！", str2), null, 400));
                    base.Response.End();
                }
                if (((j == 5) || (j == 6)) && ((!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_default_odds_2")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_max_odds_2"))) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_min_odds_2"))))
                {
                    base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率設定必須為數字或小數！", str2), null, 400));
                    base.Response.End();
                }
            }
        }

        private void ValidInput_PKBJL(DataTable rs)
        {
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string str = rs.Rows[i]["play_name"].ToString().Trim();
                if (((string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_b_diff")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_c_diff"))) || (string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_downbase")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_down_odds_rate")))) || ((string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_default_odds_1")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_max_odds_1"))) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_min_odds_1"))))
                {
                    base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率設定不能為空！", str), null, 400));
                    base.Response.End();
                }
                if ((i == 6) && (((string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_default_odds_2")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_default_odds_3"))) || (string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_max_odds_2")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_max_odds_3")))) || (string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_min_odds_2")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_min_odds_3")))))
                {
                    base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率設定不能為空！", str), null, 400));
                    base.Response.End();
                }
            }
            Regex regex = new Regex(@"^[+-]?\d+(\.\d+)?$");
            for (int j = 0; j < rs.Rows.Count; j++)
            {
                string str2 = rs.Rows[j]["play_name"].ToString().Trim();
                if (((!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_b_diff")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_c_diff"))) || (!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_downbase")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_down_odds_rate")))) || ((!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_default_odds_1")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_max_odds_1"))) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_min_odds_1"))))
                {
                    base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率設定必須為數字或小數！", str2), null, 400));
                    base.Response.End();
                }
                if ((j == 6) && (((!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_default_odds_2")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_default_odds_3"))) || (!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_max_odds_2")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_max_odds_3")))) || (!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_min_odds_2")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_min_odds_3")))))
                {
                    base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率設定必須為數字或小數！", str2), null, 400));
                    base.Response.End();
                }
            }
        }

        private void ValidInput_SPEED5(DataTable rs)
        {
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string str = rs.Rows[i]["play_name"].ToString().Trim().Replace("第一球", "1-5球");
                if (((string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_b_diff")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_c_diff"))) || (string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_downbase")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_down_odds_rate")))) || ((string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_default_odds_1")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_max_odds_1"))) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_min_odds_1"))))
                {
                    base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率設定不能為空！", str), null, 400));
                    base.Response.End();
                }
                if ((i == 5) && ((string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_default_odds_2")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_max_odds_2"))) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_min_odds_2"))))
                {
                    base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率設定不能為空！", str), null, 400));
                    base.Response.End();
                }
                if ((i == 6) && ((((string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_default_odds_2")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_default_odds_3"))) || (string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_default_odds_4")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_default_odds_5")))) || ((string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_max_odds_2")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_max_odds_3"))) || (string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_max_odds_4")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_max_odds_5"))))) || ((string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_min_odds_2")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_min_odds_3"))) || (string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_min_odds_4")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_min_odds_5"))))))
                {
                    base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率設定不能為空！", str), null, 400));
                    base.Response.End();
                }
            }
            Regex regex = new Regex(@"^[+-]?\d+(\.\d+)?$");
            for (int j = 0; j < rs.Rows.Count; j++)
            {
                string str2 = rs.Rows[j]["play_name"].ToString().Trim().Replace("第一球", "1-5球");
                if (((!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_b_diff")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_c_diff"))) || (!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_downbase")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_down_odds_rate")))) || ((!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_default_odds_1")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_max_odds_1"))) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_min_odds_1"))))
                {
                    base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率設定必須為數字或小數！", str2), null, 400));
                    base.Response.End();
                }
                if ((j == 5) && ((!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_default_odds_2")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_max_odds_2"))) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_min_odds_2"))))
                {
                    base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率設定必須為數字或小數！", str2), null, 400));
                    base.Response.End();
                }
                if ((j == 6) && ((((!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_default_odds_2")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_default_odds_3"))) || (!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_default_odds_4")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_default_odds_5")))) || ((!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_max_odds_2")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_max_odds_3"))) || (!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_max_odds_4")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_max_odds_5"))))) || ((!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_min_odds_2")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_min_odds_3"))) || (!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_min_odds_4")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_min_odds_5"))))))
                {
                    base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率設定必須為數字或小數！", str2), null, 400));
                    base.Response.End();
                }
            }
        }

        private void ValidInput_SSC168(DataTable rs)
        {
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string str = rs.Rows[i]["play_name"].ToString().Trim().Replace("第一球", "1-5球");
                if (((string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_b_diff")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_c_diff"))) || (string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_downbase")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_down_odds_rate")))) || ((string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_default_odds_1")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_max_odds_1"))) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_min_odds_1"))))
                {
                    base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率設定不能為空！", str), null, 400));
                    base.Response.End();
                }
                if ((i == 5) && ((string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_default_odds_2")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_max_odds_2"))) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_min_odds_2"))))
                {
                    base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率設定不能為空！", str), null, 400));
                    base.Response.End();
                }
                if ((i == 6) && ((((string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_default_odds_2")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_default_odds_3"))) || (string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_default_odds_4")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_default_odds_5")))) || ((string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_max_odds_2")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_max_odds_3"))) || (string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_max_odds_4")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_max_odds_5"))))) || ((string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_min_odds_2")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_min_odds_3"))) || (string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_min_odds_4")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_min_odds_5"))))))
                {
                    base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率設定不能為空！", str), null, 400));
                    base.Response.End();
                }
            }
            Regex regex = new Regex(@"^[+-]?\d+(\.\d+)?$");
            for (int j = 0; j < rs.Rows.Count; j++)
            {
                string str2 = rs.Rows[j]["play_name"].ToString().Trim().Replace("第一球", "1-5球");
                if (((!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_b_diff")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_c_diff"))) || (!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_downbase")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_down_odds_rate")))) || ((!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_default_odds_1")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_max_odds_1"))) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_min_odds_1"))))
                {
                    base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率設定必須為數字或小數！", str2), null, 400));
                    base.Response.End();
                }
                if ((j == 5) && ((!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_default_odds_2")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_max_odds_2"))) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_min_odds_2"))))
                {
                    base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率設定必須為數字或小數！", str2), null, 400));
                    base.Response.End();
                }
                if ((j == 6) && ((((!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_default_odds_2")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_default_odds_3"))) || (!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_default_odds_4")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_default_odds_5")))) || ((!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_max_odds_2")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_max_odds_3"))) || (!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_max_odds_4")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_max_odds_5"))))) || ((!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_min_odds_2")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_min_odds_3"))) || (!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_min_odds_4")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_min_odds_5"))))))
                {
                    base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率設定必須為數字或小數！", str2), null, 400));
                    base.Response.End();
                }
            }
        }

        private void ValidInput_VRCAR(DataTable rs)
        {
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string str = rs.Rows[i]["play_name"].ToString().Trim().Replace("冠軍龍虎", "1-5名龍虎").Replace("冠軍", "1-10名");
                if (((string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_b_diff")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_c_diff"))) || (string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_downbase")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_down_odds_rate")))) || ((string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_default_odds_1")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_max_odds_1"))) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_min_odds_1"))))
                {
                    base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率設定不能為空！", str), null, 400));
                    base.Response.End();
                }
                if ((i == 4) && ((((string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_default_odds_2")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_default_odds_3"))) || (string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_default_odds_4")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_default_odds_5")))) || ((string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_max_odds_2")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_max_odds_3"))) || (string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_max_odds_4")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_max_odds_5"))))) || ((string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_min_odds_2")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_min_odds_3"))) || (string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_min_odds_4")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_min_odds_5"))))))
                {
                    base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率設定不能為空！", str), null, 400));
                    base.Response.End();
                }
                if (((i == 5) || (i == 6)) && ((string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_default_odds_2")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_max_odds_2"))) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_min_odds_2"))))
                {
                    base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率設定不能為空！", str), null, 400));
                    base.Response.End();
                }
            }
            Regex regex = new Regex(@"^[+-]?\d+(\.\d+)?$");
            for (int j = 0; j < rs.Rows.Count; j++)
            {
                string str2 = rs.Rows[j]["play_name"].ToString().Trim().Replace("冠軍龍虎", "1-5名龍虎").Replace("冠軍", "1-10名");
                if (((!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_b_diff")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_c_diff"))) || (!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_downbase")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_down_odds_rate")))) || ((!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_default_odds_1")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_max_odds_1"))) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_min_odds_1"))))
                {
                    base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率設定必須為數字或小數！", str2), null, 400));
                    base.Response.End();
                }
                if ((j == 4) && ((((!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_default_odds_2")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_default_odds_3"))) || (!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_default_odds_4")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_default_odds_5")))) || ((!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_max_odds_2")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_max_odds_3"))) || (!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_max_odds_4")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_max_odds_5"))))) || ((!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_min_odds_2")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_min_odds_3"))) || (!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_min_odds_4")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_min_odds_5"))))))
                {
                    base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率設定必須為數字或小數！", str2), null, 400));
                    base.Response.End();
                }
                if (((j == 5) || (j == 6)) && ((!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_default_odds_2")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_max_odds_2"))) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_min_odds_2"))))
                {
                    base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率設定必須為數字或小數！", str2), null, 400));
                    base.Response.End();
                }
            }
        }

        private void ValidInput_VRSSC(DataTable rs)
        {
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string str = rs.Rows[i]["play_name"].ToString().Trim().Replace("第一球", "1-5球");
                if (((string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_b_diff")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_c_diff"))) || (string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_downbase")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_down_odds_rate")))) || ((string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_default_odds_1")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_max_odds_1"))) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_min_odds_1"))))
                {
                    base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率設定不能為空！", str), null, 400));
                    base.Response.End();
                }
                if ((i == 5) && ((string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_default_odds_2")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_max_odds_2"))) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_min_odds_2"))))
                {
                    base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率設定不能為空！", str), null, 400));
                    base.Response.End();
                }
                if ((i == 6) && ((((string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_default_odds_2")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_default_odds_3"))) || (string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_default_odds_4")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_default_odds_5")))) || ((string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_max_odds_2")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_max_odds_3"))) || (string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_max_odds_4")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_max_odds_5"))))) || ((string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_min_odds_2")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_min_odds_3"))) || (string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_min_odds_4")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_min_odds_5"))))))
                {
                    base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率設定不能為空！", str), null, 400));
                    base.Response.End();
                }
            }
            Regex regex = new Regex(@"^[+-]?\d+(\.\d+)?$");
            for (int j = 0; j < rs.Rows.Count; j++)
            {
                string str2 = rs.Rows[j]["play_name"].ToString().Trim().Replace("第一球", "1-5球");
                if (((!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_b_diff")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_c_diff"))) || (!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_downbase")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_down_odds_rate")))) || ((!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_default_odds_1")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_max_odds_1"))) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_min_odds_1"))))
                {
                    base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率設定必須為數字或小數！", str2), null, 400));
                    base.Response.End();
                }
                if ((j == 5) && ((!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_default_odds_2")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_max_odds_2"))) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_min_odds_2"))))
                {
                    base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率設定必須為數字或小數！", str2), null, 400));
                    base.Response.End();
                }
                if ((j == 6) && ((((!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_default_odds_2")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_default_odds_3"))) || (!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_default_odds_4")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_default_odds_5")))) || ((!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_max_odds_2")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_max_odds_3"))) || (!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_max_odds_4")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_max_odds_5"))))) || ((!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_min_odds_2")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_min_odds_3"))) || (!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_min_odds_4")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_min_odds_5"))))))
                {
                    base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率設定必須為數字或小數！", str2), null, 400));
                    base.Response.End();
                }
            }
        }

        private void ValidInput_XYFT5(DataTable rs)
        {
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string str = rs.Rows[i]["play_name"].ToString().Trim().Replace("冠軍龍虎", "1-5名龍虎").Replace("冠軍", "1-10名");
                if (((string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_b_diff")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_c_diff"))) || (string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_downbase")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_down_odds_rate")))) || ((string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_default_odds_1")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_max_odds_1"))) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_min_odds_1"))))
                {
                    base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率設定不能為空！", str), null, 400));
                    base.Response.End();
                }
                if ((i == 4) && ((((string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_default_odds_2")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_default_odds_3"))) || (string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_default_odds_4")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_default_odds_5")))) || ((string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_max_odds_2")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_max_odds_3"))) || (string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_max_odds_4")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_max_odds_5"))))) || ((string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_min_odds_2")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_min_odds_3"))) || (string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_min_odds_4")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_min_odds_5"))))))
                {
                    base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率設定不能為空！", str), null, 400));
                    base.Response.End();
                }
                if (((i == 5) || (i == 6)) && ((string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_default_odds_2")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_max_odds_2"))) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_min_odds_2"))))
                {
                    base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率設定不能為空！", str), null, 400));
                    base.Response.End();
                }
            }
            Regex regex = new Regex(@"^[+-]?\d+(\.\d+)?$");
            for (int j = 0; j < rs.Rows.Count; j++)
            {
                string str2 = rs.Rows[j]["play_name"].ToString().Trim().Replace("冠軍龍虎", "1-5名龍虎").Replace("冠軍", "1-10名");
                if (((!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_b_diff")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_c_diff"))) || (!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_downbase")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_down_odds_rate")))) || ((!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_default_odds_1")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_max_odds_1"))) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_min_odds_1"))))
                {
                    base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率設定必須為數字或小數！", str2), null, 400));
                    base.Response.End();
                }
                if ((j == 4) && ((((!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_default_odds_2")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_default_odds_3"))) || (!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_default_odds_4")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_default_odds_5")))) || ((!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_max_odds_2")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_max_odds_3"))) || (!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_max_odds_4")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_max_odds_5"))))) || ((!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_min_odds_2")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_min_odds_3"))) || (!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_min_odds_4")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_min_odds_5"))))))
                {
                    base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率設定必須為數字或小數！", str2), null, 400));
                    base.Response.End();
                }
                if (((j == 5) || (j == 6)) && ((!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_default_odds_2")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_max_odds_2"))) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_min_odds_2"))))
                {
                    base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率設定必須為數字或小數！", str2), null, 400));
                    base.Response.End();
                }
            }
        }

        private void ValidInput_XYFTOA(DataTable rs)
        {
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string str = rs.Rows[i]["play_name"].ToString().Trim().Replace("冠軍龍虎", "1-5名龍虎").Replace("冠軍", "1-10名");
                if (((string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_b_diff")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_c_diff"))) || (string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_downbase")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_down_odds_rate")))) || ((string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_default_odds_1")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_max_odds_1"))) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_min_odds_1"))))
                {
                    base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率設定不能為空！", str), null, 400));
                    base.Response.End();
                }
                if ((i == 4) && ((((string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_default_odds_2")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_default_odds_3"))) || (string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_default_odds_4")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_default_odds_5")))) || ((string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_max_odds_2")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_max_odds_3"))) || (string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_max_odds_4")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_max_odds_5"))))) || ((string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_min_odds_2")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_min_odds_3"))) || (string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_min_odds_4")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_min_odds_5"))))))
                {
                    base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率設定不能為空！", str), null, 400));
                    base.Response.End();
                }
                if (((i == 5) || (i == 6)) && ((string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_default_odds_2")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_max_odds_2"))) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_min_odds_2"))))
                {
                    base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率設定不能為空！", str), null, 400));
                    base.Response.End();
                }
            }
            Regex regex = new Regex(@"^[+-]?\d+(\.\d+)?$");
            for (int j = 0; j < rs.Rows.Count; j++)
            {
                string str2 = rs.Rows[j]["play_name"].ToString().Trim().Replace("冠軍龍虎", "1-5名龍虎").Replace("冠軍", "1-10名");
                if (((!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_b_diff")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_c_diff"))) || (!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_downbase")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_down_odds_rate")))) || ((!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_default_odds_1")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_max_odds_1"))) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_min_odds_1"))))
                {
                    base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率設定必須為數字或小數！", str2), null, 400));
                    base.Response.End();
                }
                if ((j == 4) && ((((!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_default_odds_2")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_default_odds_3"))) || (!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_default_odds_4")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_default_odds_5")))) || ((!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_max_odds_2")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_max_odds_3"))) || (!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_max_odds_4")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_max_odds_5"))))) || ((!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_min_odds_2")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_min_odds_3"))) || (!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_min_odds_4")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_min_odds_5"))))))
                {
                    base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率設定必須為數字或小數！", str2), null, 400));
                    base.Response.End();
                }
                if (((j == 5) || (j == 6)) && ((!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_default_odds_2")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_max_odds_2"))) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_min_odds_2"))))
                {
                    base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率設定必須為數字或小數！", str2), null, 400));
                    base.Response.End();
                }
            }
        }

        private void ValidInput_XYFTSG(DataTable rs)
        {
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string str = rs.Rows[i]["play_name"].ToString().Trim().Replace("冠軍龍虎", "1-5名龍虎").Replace("冠軍", "1-10名");
                if (((string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_b_diff")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_c_diff"))) || (string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_downbase")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_down_odds_rate")))) || ((string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_default_odds_1")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_max_odds_1"))) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_min_odds_1"))))
                {
                    base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率設定不能為空！", str), null, 400));
                    base.Response.End();
                }
                if ((i == 4) && ((((string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_default_odds_2")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_default_odds_3"))) || (string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_default_odds_4")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_default_odds_5")))) || ((string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_max_odds_2")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_max_odds_3"))) || (string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_max_odds_4")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_max_odds_5"))))) || ((string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_min_odds_2")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_min_odds_3"))) || (string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_min_odds_4")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_min_odds_5"))))))
                {
                    base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率設定不能為空！", str), null, 400));
                    base.Response.End();
                }
                if (((i == 5) || (i == 6)) && ((string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_default_odds_2")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_max_odds_2"))) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_min_odds_2"))))
                {
                    base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率設定不能為空！", str), null, 400));
                    base.Response.End();
                }
            }
            Regex regex = new Regex(@"^[+-]?\d+(\.\d+)?$");
            for (int j = 0; j < rs.Rows.Count; j++)
            {
                string str2 = rs.Rows[j]["play_name"].ToString().Trim().Replace("冠軍龍虎", "1-5名龍虎").Replace("冠軍", "1-10名");
                if (((!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_b_diff")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_c_diff"))) || (!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_downbase")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_down_odds_rate")))) || ((!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_default_odds_1")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_max_odds_1"))) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_min_odds_1"))))
                {
                    base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率設定必須為數字或小數！", str2), null, 400));
                    base.Response.End();
                }
                if ((j == 4) && ((((!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_default_odds_2")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_default_odds_3"))) || (!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_default_odds_4")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_default_odds_5")))) || ((!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_max_odds_2")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_max_odds_3"))) || (!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_max_odds_4")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_max_odds_5"))))) || ((!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_min_odds_2")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_min_odds_3"))) || (!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_min_odds_4")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_min_odds_5"))))))
                {
                    base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率設定必須為數字或小數！", str2), null, 400));
                    base.Response.End();
                }
                if (((j == 5) || (j == 6)) && ((!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_default_odds_2")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_max_odds_2"))) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_min_odds_2"))))
                {
                    base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率設定必須為數字或小數！", str2), null, 400));
                    base.Response.End();
                }
            }
        }

        private void ValidInput_XYNC(DataTable rs)
        {
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string str = rs.Rows[i]["play_name"].ToString().Trim().Replace("第一球", "1-8球");
                if (((string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_b_diff")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_c_diff"))) || (string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_downbase")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_down_odds_rate")))) || ((string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_default_odds_1")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_max_odds_1"))) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_min_odds_1"))))
                {
                    base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率設定不能為空！", str), null, 400));
                    base.Response.End();
                }
                if ((i == 6) && ((string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_default_odds_2")) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_max_odds_2"))) || string.IsNullOrEmpty(LSRequest.qq("t" + i.ToString() + "_min_odds_2"))))
                {
                    base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率設定不能為空！", str), null, 400));
                    base.Response.End();
                }
            }
            Regex regex = new Regex(@"^[+-]?\d+(\.\d+)?$");
            for (int j = 0; j < rs.Rows.Count; j++)
            {
                string str2 = rs.Rows[j]["play_name"].ToString().Trim().Replace("第一球", "1-8球");
                if (((!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_b_diff")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_c_diff"))) || (!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_downbase")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_down_odds_rate")))) || ((!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_default_odds_1")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_max_odds_1"))) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_min_odds_1"))))
                {
                    base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率設定必須為數字或小數！", str2), null, 400));
                    base.Response.End();
                }
                if ((j == 6) && ((!regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_default_odds_2")) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_max_odds_2"))) || !regex.IsMatch(LSRequest.qq("t" + j.ToString() + "_min_odds_2"))))
                {
                    base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率設定必須為數字或小數！", str2), null, 400));
                    base.Response.End();
                }
            }
        }
    }
}

