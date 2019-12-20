namespace Agent.Web
{
    using Agent.Web.WebBase;
    using LotterySystem.Common;
    using LotterySystem.Model;
    using System;
    using System.Data;
    using System.Text.RegularExpressions;

    public class OddsSet_six : MemberPageBase
    {
        protected bool childModify = true;
        protected DataTable dataTable;
        protected string is_abamount = "0";
        protected DataTable kl10DT;
        protected DataTable lotteryDT;
        protected string lotteryId = "";
        protected string max_odds_isopen = "0";
        protected string max_odds_isopen_str = " readonly=\"readonly\" style=\"background:#EBE9E9;\" ";
        protected string skin = "";
        protected cz_system_set_six systemModel_six;

        private void InitData()
        {
            if (Convert.ToInt32(this.lotteryId) == 100)
            {
                DataSet playByOddsSet = CallBLL.cz_play_six_bll.GetPlayByOddsSet("91025,91026,91027,91028,91029");
                if (((playByOddsSet != null) && (playByOddsSet.Tables.Count > 0)) && (playByOddsSet.Tables[0].Rows.Count > 0))
                {
                    this.dataTable = playByOddsSet.Tables[0];
                }
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
            if (!this.lotteryId.Equals(num.ToString()))
            {
                base.Response.Redirect("/OddsSet/OddsSet_kc.aspx?lid=" + this.lotteryId, true);
            }
            this.InitData();
            this.systemModel_six = CallBLL.cz_system_set_six_bll.GetSystemSet(100);
            this.max_odds_isopen = this.systemModel_six.get_max_odds_isopen();
            this.is_abamount = this.systemModel_six.get_is_tmab();
            if (this.max_odds_isopen.Equals("1"))
            {
                this.max_odds_isopen_str = "";
            }
            if (LSRequest.qq("submitHdn").Equals("submit"))
            {
                if (base.IsChildSync())
                {
                    base.Response.Redirect("/MessagePage.aspx?code=u100080&url=&issuccess=1&isback=0");
                }
                this.UpdateData();
            }
        }

        private void UpdateData()
        {
            DataTable rs = null;
            if (Convert.ToInt32(this.lotteryId) == 100)
            {
                DataSet playByOddsSet = CallBLL.cz_play_six_bll.GetPlayByOddsSet("91025,91026,91027,91028,91029");
                if (((playByOddsSet != null) && (playByOddsSet.Tables.Count > 0)) && (playByOddsSet.Tables[0].Rows.Count > 0))
                {
                    rs = playByOddsSet.Tables[0];
                    this.ValidInput_SIX(rs);
                    foreach (DataRow row in rs.Rows)
                    {
                        int num = int.Parse(row["play_id"].ToString());
                        string str = row["play_name"].ToString().Trim().Replace("正1特", "正特1-6");
                        int num2 = int.Parse(row["isDoubleOdds"].ToString());
                        cz_play_six _six = new cz_play_six();
                        double num3 = 0.0;
                        double num4 = 0.0;
                        double num5 = 0.0;
                        double num6 = 0.0;
                        double num7 = 0.0;
                        double num8 = 0.0;
                        double num9 = 0.0;
                        double num10 = 0.0;
                        double num11 = 0.0;
                        double num12 = 0.0;
                        decimal num13 = Convert.ToDecimal(LSRequest.qq("t" + num.ToString() + "_downbase"));
                        decimal num14 = Convert.ToDecimal(LSRequest.qq("t" + num.ToString() + "_down_odds_rate"));
                        decimal num15 = decimal.Parse(row["allow_min_amount"].ToString());
                        num3 = double.Parse(LSRequest.qq("t" + num.ToString() + "_default_odds_1"));
                        num5 = double.Parse(LSRequest.qq("t" + num.ToString() + "_max_odds_1"));
                        if (!this.max_odds_isopen.Equals("1"))
                        {
                            if (num2.Equals(1))
                            {
                                num5 = Convert.ToDouble(row["max_odds"].ToString().Split(new char[] { ',' })[0]);
                            }
                            else
                            {
                                num5 = Convert.ToDouble(row["max_odds"].ToString().Split(new char[] { '|' })[0]);
                            }
                        }
                        num7 = double.Parse(LSRequest.qq("t" + num.ToString() + "_min_odds_1"));
                        num9 = double.Parse(LSRequest.qq("t" + num.ToString() + "_b_diff_1"));
                        num11 = double.Parse(LSRequest.qq("t" + num.ToString() + "_c_diff_1"));
                        if (!this.max_odds_isopen.Equals("1"))
                        {
                            if (num2.Equals(1))
                            {
                                num9 = Convert.ToDouble(row["b_diff"].ToString().Split(new char[] { ',' })[0]);
                                num11 = Convert.ToDouble(row["c_diff"].ToString().Split(new char[] { ',' })[0]);
                            }
                            else
                            {
                                num9 = Convert.ToDouble(row["b_diff"].ToString().Split(new char[] { '|' })[0]);
                                num11 = Convert.ToDouble(row["c_diff"].ToString().Split(new char[] { '|' })[0]);
                            }
                        }
                        _six.set_play_id(Convert.ToInt32(row["play_id"].ToString()));
                        if (_six.get_play_id().ToString().Equals("91002") && this.systemModel_six.get_is_tmab().Equals(1))
                        {
                            _six.set_downbase(new decimal?(decimal.Parse(row["downbase"].ToString())));
                            _six.set_down_odds_rate(new decimal?(decimal.Parse(row["down_odds_rate"].ToString())));
                        }
                        else
                        {
                            _six.set_downbase(new decimal?(num13));
                            _six.set_down_odds_rate(new decimal?(num14));
                        }
                        _six.set_allow_min_amount(new decimal?(num15));
                        _six.set_default_odds(num3.ToString());
                        _six.set_max_odds(num5.ToString());
                        _six.set_min_odds(num7.ToString());
                        _six.set_b_diff(num9.ToString());
                        _six.set_c_diff(num11.ToString());
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
                        switch (num)
                        {
                            case 0x16389:
                            case 0x1638b:
                            case 0x163b5:
                            case 0x163b7:
                                num4 = double.Parse(LSRequest.qq("t" + num.ToString() + "_default_odds_2"));
                                num6 = double.Parse(LSRequest.qq("t" + num.ToString() + "_max_odds_2"));
                                if (!this.max_odds_isopen.Equals("1"))
                                {
                                    if (num2.Equals(1))
                                    {
                                        num6 = Convert.ToDouble(row["max_odds"].ToString().Split(new char[] { ',' })[1]);
                                    }
                                    else
                                    {
                                        num6 = Convert.ToDouble(row["max_odds"].ToString().Split(new char[] { '|' })[1]);
                                    }
                                }
                                num8 = double.Parse(LSRequest.qq("t" + num.ToString() + "_min_odds_2"));
                                num10 = double.Parse(LSRequest.qq("t" + num.ToString() + "_b_diff_2"));
                                num12 = double.Parse(LSRequest.qq("t" + num.ToString() + "_c_diff_2"));
                                if (!this.max_odds_isopen.Equals("1"))
                                {
                                    if (num2.Equals(1))
                                    {
                                        num10 = Convert.ToDouble(row["b_diff"].ToString().Split(new char[] { ',' })[1]);
                                        num12 = Convert.ToDouble(row["c_diff"].ToString().Split(new char[] { ',' })[1]);
                                    }
                                    else
                                    {
                                        num10 = Convert.ToDouble(row["b_diff"].ToString().Split(new char[] { '|' })[1]);
                                        num12 = Convert.ToDouble(row["c_diff"].ToString().Split(new char[] { '|' })[1]);
                                    }
                                }
                                if (num4 > num6)
                                {
                                    base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能大於賠率上限！", str), null, 400));
                                    base.Response.End();
                                }
                                if (num4 < num8)
                                {
                                    base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能小於賠率下限！", str), null, 400));
                                    base.Response.End();
                                }
                                if (num6 < num8)
                                {
                                    base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率上限不能小於賠率下限！", str), null, 400));
                                    base.Response.End();
                                }
                                _six.set_default_odds(num3 + "," + num4);
                                _six.set_max_odds(num5 + "," + num6);
                                _six.set_min_odds(num7 + "," + num8);
                                _six.set_b_diff(num9 + "," + num10);
                                _six.set_c_diff(num11 + "," + num12);
                                break;

                            case 0x1637e:
                            case 0x1637f:
                            case 0x1638d:
                            case 0x1638e:
                            case 0x163ae:
                            case 0x163af:
                                num4 = double.Parse(LSRequest.qq("t" + num.ToString() + "_default_odds_2"));
                                num6 = double.Parse(LSRequest.qq("t" + num.ToString() + "_max_odds_2"));
                                if (!this.max_odds_isopen.Equals("1"))
                                {
                                    if (num2.Equals(1))
                                    {
                                        num6 = Convert.ToDouble(row["max_odds"].ToString().Split(new char[] { ',' })[1]);
                                    }
                                    else
                                    {
                                        num6 = Convert.ToDouble(row["max_odds"].ToString().Split(new char[] { '|' })[1]);
                                    }
                                }
                                num8 = double.Parse(LSRequest.qq("t" + num.ToString() + "_min_odds_2"));
                                num10 = double.Parse(LSRequest.qq("t" + num.ToString() + "_b_diff_2"));
                                num12 = double.Parse(LSRequest.qq("t" + num.ToString() + "_c_diff_2"));
                                if (!this.max_odds_isopen.Equals("1"))
                                {
                                    if (num2.Equals(1))
                                    {
                                        num10 = Convert.ToDouble(row["b_diff"].ToString().Split(new char[] { ',' })[1]);
                                        num12 = Convert.ToDouble(row["c_diff"].ToString().Split(new char[] { ',' })[1]);
                                    }
                                    else
                                    {
                                        num10 = Convert.ToDouble(row["b_diff"].ToString().Split(new char[] { '|' })[1]);
                                        num12 = Convert.ToDouble(row["c_diff"].ToString().Split(new char[] { '|' })[1]);
                                    }
                                }
                                if (num4 > num6)
                                {
                                    base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能大於賠率上限！", str), null, 400));
                                    base.Response.End();
                                }
                                if (num4 < num8)
                                {
                                    base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能小於賠率下限！", str), null, 400));
                                    base.Response.End();
                                }
                                if (num6 < num8)
                                {
                                    base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率上限不能小於賠率下限！", str), null, 400));
                                    base.Response.End();
                                }
                                _six.set_default_odds(num3 + "|" + num4);
                                _six.set_max_odds(num5 + "|" + num6);
                                _six.set_min_odds(num7 + "|" + num8);
                                _six.set_b_diff(num9 + "|" + num10);
                                _six.set_c_diff(num11 + "|" + num12);
                                break;

                            case 0x16385:
                                num4 = double.Parse(LSRequest.qq("t" + num.ToString() + "_default_odds_2"));
                                num6 = double.Parse(LSRequest.qq("t" + num.ToString() + "_max_odds_2"));
                                if (!this.max_odds_isopen.Equals("1"))
                                {
                                    if (num2.Equals(1))
                                    {
                                        num6 = Convert.ToDouble(row["max_odds"].ToString().Split(new char[] { ',' })[1]);
                                    }
                                    else
                                    {
                                        num6 = Convert.ToDouble(row["max_odds"].ToString().Split(new char[] { '|' })[1]);
                                    }
                                }
                                num8 = double.Parse(LSRequest.qq("t" + num.ToString() + "_min_odds_2"));
                                num10 = double.Parse(LSRequest.qq("t" + num.ToString() + "_b_diff_2"));
                                num12 = double.Parse(LSRequest.qq("t" + num.ToString() + "_c_diff_2"));
                                if (!this.max_odds_isopen.Equals("1"))
                                {
                                    if (num2.Equals(1))
                                    {
                                        num10 = Convert.ToDouble(row["b_diff"].ToString().Split(new char[] { ',' })[1]);
                                        num12 = Convert.ToDouble(row["c_diff"].ToString().Split(new char[] { ',' })[1]);
                                    }
                                    else
                                    {
                                        num10 = Convert.ToDouble(row["b_diff"].ToString().Split(new char[] { '|' })[1]);
                                        num12 = Convert.ToDouble(row["c_diff"].ToString().Split(new char[] { '|' })[1]);
                                    }
                                }
                                if (num4 > num6)
                                {
                                    base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能大於賠率上限！", str), null, 400));
                                    base.Response.End();
                                }
                                if (num4 < num8)
                                {
                                    base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能小於賠率下限！", str), null, 400));
                                    base.Response.End();
                                }
                                if (num6 < num8)
                                {
                                    base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率上限不能小於賠率下限！", str), null, 400));
                                    base.Response.End();
                                }
                                _six.set_default_odds(num3 + "|" + num4);
                                _six.set_max_odds(num5 + "|" + num6);
                                _six.set_min_odds(num7 + "|" + num8);
                                _six.set_b_diff(num9 + "|" + num10);
                                _six.set_c_diff(num11 + "|" + num12);
                                break;

                            case 0x16380:
                            case 0x163b1:
                            {
                                for (int i = 1; i < 6; i++)
                                {
                                    if ((string.IsNullOrEmpty(LSRequest.qq(string.Concat(new object[] { "t", num.ToString(), "_default_odds_", i + 1 }))) || string.IsNullOrEmpty(LSRequest.qq(string.Concat(new object[] { "t", num.ToString(), "_max_odds_", i + 1 })))) || string.IsNullOrEmpty(LSRequest.qq(string.Concat(new object[] { "t", num.ToString(), "_min_odds_", i + 1 }))))
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率設定不能為空！", str), null, 400));
                                        base.Response.End();
                                    }
                                }
                                string str2 = "";
                                string str3 = "";
                                string str4 = "";
                                string str5 = "";
                                string str6 = "";
                                for (int j = 1; j < 6; j++)
                                {
                                    double num18 = double.Parse(LSRequest.qq(string.Concat(new object[] { "t", num.ToString(), "_default_odds_", j + 1 })));
                                    double num19 = double.Parse(LSRequest.qq(string.Concat(new object[] { "t", num.ToString(), "_max_odds_", j + 1 })));
                                    if (!this.max_odds_isopen.Equals("1"))
                                    {
                                        num19 = Convert.ToDouble(row["max_odds"].ToString().Split(new char[] { '|' })[j]);
                                    }
                                    double num20 = double.Parse(LSRequest.qq(string.Concat(new object[] { "t", num.ToString(), "_min_odds_", j + 1 })));
                                    double num21 = double.Parse(LSRequest.qq(string.Concat(new object[] { "t", num.ToString(), "_b_diff_", j + 1 })));
                                    double num22 = double.Parse(LSRequest.qq(string.Concat(new object[] { "t", num.ToString(), "_c_diff_", j + 1 })));
                                    if (!this.max_odds_isopen.Equals("1"))
                                    {
                                        num21 = Convert.ToDouble(row["b_diff"].ToString().Split(new char[] { '|' })[j]);
                                        num22 = Convert.ToDouble(row["c_diff"].ToString().Split(new char[] { '|' })[j]);
                                    }
                                    if (num18 > num19)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能大於賠率上限！", str), null, 400));
                                        base.Response.End();
                                    }
                                    if (num18 < num20)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能小於賠率下限！", str), null, 400));
                                        base.Response.End();
                                    }
                                    if (num19 < num20)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率上限不能小於賠率下限！", str), null, 400));
                                        base.Response.End();
                                    }
                                    str2 = str2 + "|" + num18;
                                    str3 = str3 + "|" + num19;
                                    str4 = str4 + "|" + num20;
                                    str5 = str5 + "|" + num21;
                                    str6 = str6 + "|" + num22;
                                }
                                _six.set_default_odds(num3 + str2);
                                _six.set_max_odds(num5 + str3);
                                _six.set_min_odds(num7 + str4);
                                _six.set_b_diff(num9 + str5);
                                _six.set_c_diff(num11 + str6);
                                break;
                            }
                            case 0x16397:
                            case 0x16398:
                            case 0x16399:
                            case 0x1639a:
                            case 0x1639b:
                            case 0x1639c:
                            case 0x163b2:
                            case 0x163b3:
                                num4 = double.Parse(LSRequest.qq("t" + num.ToString() + "_default_odds_2"));
                                num6 = double.Parse(LSRequest.qq("t" + num.ToString() + "_max_odds_2"));
                                if (!this.max_odds_isopen.Equals("1"))
                                {
                                    num6 = Convert.ToDouble(row["max_odds"].ToString().Split(new char[] { ',' })[1]);
                                }
                                num8 = double.Parse(LSRequest.qq("t" + num.ToString() + "_min_odds_2"));
                                num10 = double.Parse(LSRequest.qq("t" + num.ToString() + "_b_diff_2"));
                                num12 = double.Parse(LSRequest.qq("t" + num.ToString() + "_c_diff_2"));
                                if (!this.max_odds_isopen.Equals("1"))
                                {
                                    num10 = Convert.ToDouble(row["b_diff"].ToString().Split(new char[] { ',' })[1]);
                                    num12 = Convert.ToDouble(row["c_diff"].ToString().Split(new char[] { ',' })[1]);
                                }
                                if (num4 > num6)
                                {
                                    base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能大於賠率上限！", str), null, 400));
                                    base.Response.End();
                                }
                                if (num4 < num8)
                                {
                                    base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能小於賠率下限！", str), null, 400));
                                    base.Response.End();
                                }
                                if (num6 < num8)
                                {
                                    base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率上限不能小於賠率下限！", str), null, 400));
                                    base.Response.End();
                                }
                                _six.set_default_odds(num3 + "," + num4);
                                _six.set_max_odds(num5 + "," + num6);
                                _six.set_min_odds(num7 + "," + num8);
                                _six.set_b_diff(num9 + "," + num10);
                                _six.set_c_diff(num11 + "," + num12);
                                break;

                            case 0x163ac:
                            case 0x163b0:
                            {
                                for (int k = 1; k < 8; k++)
                                {
                                    if ((string.IsNullOrEmpty(LSRequest.qq(string.Concat(new object[] { "t", num.ToString(), "_default_odds_", k + 1 }))) || string.IsNullOrEmpty(LSRequest.qq(string.Concat(new object[] { "t", num.ToString(), "_max_odds_", k + 1 })))) || string.IsNullOrEmpty(LSRequest.qq(string.Concat(new object[] { "t", num.ToString(), "_min_odds_", k + 1 }))))
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率設定不能為空！", str), null, 400));
                                        base.Response.End();
                                    }
                                }
                                string str7 = "";
                                string str8 = "";
                                string str9 = "";
                                string str10 = "";
                                string str11 = "";
                                for (int m = 1; m < 8; m++)
                                {
                                    double num25 = double.Parse(LSRequest.qq(string.Concat(new object[] { "t", num.ToString(), "_default_odds_", m + 1 })));
                                    double num26 = double.Parse(LSRequest.qq(string.Concat(new object[] { "t", num.ToString(), "_max_odds_", m + 1 })));
                                    if (!this.max_odds_isopen.Equals("1"))
                                    {
                                        num26 = Convert.ToDouble(row["max_odds"].ToString().Split(new char[] { '|' })[m]);
                                    }
                                    double num27 = double.Parse(LSRequest.qq(string.Concat(new object[] { "t", num.ToString(), "_min_odds_", m + 1 })));
                                    double num28 = double.Parse(LSRequest.qq(string.Concat(new object[] { "t", num.ToString(), "_b_diff_", m + 1 })));
                                    double num29 = double.Parse(LSRequest.qq(string.Concat(new object[] { "t", num.ToString(), "_c_diff_", m + 1 })));
                                    if (!this.max_odds_isopen.Equals("1"))
                                    {
                                        num28 = Convert.ToDouble(row["b_diff"].ToString().Split(new char[] { '|' })[m]);
                                        num29 = Convert.ToDouble(row["c_diff"].ToString().Split(new char[] { '|' })[m]);
                                    }
                                    if (num25 > num26)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能大於賠率上限！", str), null, 400));
                                        base.Response.End();
                                    }
                                    if (num25 < num27)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能小於賠率下限！", str), null, 400));
                                        base.Response.End();
                                    }
                                    if (num26 < num27)
                                    {
                                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率上限不能小於賠率下限！", str), null, 400));
                                        base.Response.End();
                                    }
                                    str7 = str7 + "|" + num25;
                                    str8 = str8 + "|" + num26;
                                    str9 = str9 + "|" + num27;
                                    str10 = str10 + "|" + num28;
                                    str11 = str11 + "|" + num29;
                                }
                                _six.set_default_odds(num3 + str7);
                                _six.set_max_odds(num5 + str8);
                                _six.set_min_odds(num7 + str9);
                                _six.set_b_diff(num9 + str10);
                                _six.set_c_diff(num11 + str11);
                                break;
                            }
                            default:
                                if (num == 0x163ad)
                                {
                                    for (int n = 1; n < 5; n++)
                                    {
                                        if ((string.IsNullOrEmpty(LSRequest.qq(string.Concat(new object[] { "t", num.ToString(), "_default_odds_", n + 1 }))) || string.IsNullOrEmpty(LSRequest.qq(string.Concat(new object[] { "t", num.ToString(), "_max_odds_", n + 1 })))) || string.IsNullOrEmpty(LSRequest.qq(string.Concat(new object[] { "t", num.ToString(), "_min_odds_", n + 1 }))))
                                        {
                                            base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率設定不能為空！", str), null, 400));
                                            base.Response.End();
                                        }
                                    }
                                    string str12 = "";
                                    string str13 = "";
                                    string str14 = "";
                                    string str15 = "";
                                    string str16 = "";
                                    for (int num31 = 1; num31 < 5; num31++)
                                    {
                                        double num32 = double.Parse(LSRequest.qq(string.Concat(new object[] { "t", num.ToString(), "_default_odds_", num31 + 1 })));
                                        double num33 = double.Parse(LSRequest.qq(string.Concat(new object[] { "t", num.ToString(), "_max_odds_", num31 + 1 })));
                                        if (!this.max_odds_isopen.Equals("1"))
                                        {
                                            num33 = Convert.ToDouble(row["max_odds"].ToString().Split(new char[] { '|' })[num31]);
                                        }
                                        double num34 = double.Parse(LSRequest.qq(string.Concat(new object[] { "t", num.ToString(), "_min_odds_", num31 + 1 })));
                                        double num35 = double.Parse(LSRequest.qq(string.Concat(new object[] { "t", num.ToString(), "_b_diff_", num31 + 1 })));
                                        double num36 = double.Parse(LSRequest.qq(string.Concat(new object[] { "t", num.ToString(), "_c_diff_", num31 + 1 })));
                                        if (!this.max_odds_isopen.Equals("1"))
                                        {
                                            num35 = Convert.ToDouble(row["b_diff"].ToString().Split(new char[] { '|' })[num31]);
                                            num36 = Convert.ToDouble(row["c_diff"].ToString().Split(new char[] { '|' })[num31]);
                                        }
                                        if (num32 > num33)
                                        {
                                            base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能大於賠率上限！", str), null, 400));
                                            base.Response.End();
                                        }
                                        if (num32 < num34)
                                        {
                                            base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 開盤賠率不能小於賠率下限！", str), null, 400));
                                            base.Response.End();
                                        }
                                        if (num33 < num34)
                                        {
                                            base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率上限不能小於賠率下限！", str), null, 400));
                                            base.Response.End();
                                        }
                                        str12 = str12 + "|" + num32;
                                        str13 = str13 + "|" + num33;
                                        str14 = str14 + "|" + num34;
                                        str15 = str15 + "|" + num35;
                                        str16 = str16 + "|" + num36;
                                    }
                                    _six.set_default_odds(num3 + str12);
                                    _six.set_max_odds(num5 + str13);
                                    _six.set_min_odds(num7 + str14);
                                    _six.set_b_diff(num9 + str15);
                                    _six.set_c_diff(num11 + str16);
                                }
                                break;
                        }
                        try
                        {
                            if (_six.get_play_id().Equals(0x16382))
                            {
                                CallBLL.cz_play_six_bll.SetPlayByOddsSet(_six, "91010,91025,91026,91027,91028,91029");
                            }
                            else
                            {
                                CallBLL.cz_play_six_bll.SetPlayByOddsSet(_six);
                            }
                            bool flag = CallBLL.cz_phase_six_bll.IsOpenPhase();
                            int num37 = 0;
                            if (flag)
                            {
                                num37 = 1;
                            }
                            else
                            {
                                num37 = 0;
                            }
                            if (_six.get_play_id().Equals(0x16385))
                            {
                                CallBLL.cz_odds_six_bll.SetOddsByOddsSet_SB(_six, num37.ToString());
                            }
                            else if (_six.get_play_id().Equals(0x16382))
                            {
                                CallBLL.cz_odds_six_bll.SetOddsByOddsSet_ZT(_six, "91010,91025,91026,91027,91028,91029", num37.ToString());
                            }
                            else if (_six.get_play_id().Equals(0x1637e))
                            {
                                CallBLL.cz_odds_six_bll.SetOddsByOddsSet_TMSX(_six, base.get_YearLian(), num37.ToString());
                            }
                            else if (_six.get_play_id().Equals(0x1637f))
                            {
                                CallBLL.cz_odds_six_bll.SetOddsByOddsSet_TMSB(_six, num37.ToString());
                            }
                            else if (_six.get_play_id().Equals(0x1638d))
                            {
                                CallBLL.cz_odds_six_bll.SetOddsByOddsSet_SXWS(_six, "sx", base.get_YearLian(), num37.ToString());
                            }
                            else if (_six.get_play_id().Equals(0x1638e))
                            {
                                CallBLL.cz_odds_six_bll.SetOddsByOddsSet_SXWS(_six, "ws", "0", num37.ToString());
                            }
                            else if (_six.get_play_id().Equals(0x163ae))
                            {
                                CallBLL.cz_odds_six_bll.SetOddsByOddsSet_SXWS(_six, "sx", base.get_YearLian(), num37.ToString());
                            }
                            else if (_six.get_play_id().Equals(0x163af))
                            {
                                CallBLL.cz_odds_six_bll.SetOddsByOddsSet_SXWS(_six, "ws", "0", num37.ToString());
                            }
                            else if (_six.get_play_id().Equals(0x16380))
                            {
                                string[] strArray = _six.get_default_odds().Split(new char[] { '|' });
                                string[] strArray2 = _six.get_max_odds().Split(new char[] { '|' });
                                string[] strArray3 = _six.get_min_odds().Split(new char[] { '|' });
                                string[] strArray4 = _six.get_b_diff().Split(new char[] { '|' });
                                string[] strArray5 = _six.get_c_diff().Split(new char[] { '|' });
                                cz_odds_six _six2 = new cz_odds_six();
                                _six2.set_play_id(new int?(_six.get_play_id()));
                                _six2.set_play_name("半波【單雙】");
                                _six2.set_put_amount("紅波單");
                                _six2.set_current_odds(strArray[0]);
                                _six2.set_max_odds(strArray2[0]);
                                _six2.set_min_odds(strArray3[0]);
                                _six2.set_b_diff(strArray4[0]);
                                _six2.set_c_diff(strArray5[0]);
                                CallBLL.cz_odds_six_bll.SetOddsByOddsSet_BB(_six2, num37.ToString());
                                _six2.set_play_name("半波【單雙】");
                                _six2.set_put_amount("紅波雙");
                                _six2.set_current_odds(strArray[1]);
                                _six2.set_max_odds(strArray2[1]);
                                _six2.set_min_odds(strArray3[1]);
                                _six2.set_b_diff(strArray4[1]);
                                _six2.set_c_diff(strArray5[1]);
                                CallBLL.cz_odds_six_bll.SetOddsByOddsSet_BB(_six2, num37.ToString());
                                _six2.set_play_name("半波【單雙】");
                                _six2.set_put_amount("藍波單");
                                _six2.set_current_odds(strArray[2]);
                                _six2.set_max_odds(strArray2[2]);
                                _six2.set_min_odds(strArray3[2]);
                                _six2.set_b_diff(strArray4[2]);
                                _six2.set_c_diff(strArray5[2]);
                                CallBLL.cz_odds_six_bll.SetOddsByOddsSet_BB(_six2, num37.ToString());
                                _six2.set_play_name("半波【單雙】");
                                _six2.set_put_amount("藍波雙");
                                _six2.set_current_odds(strArray[3]);
                                _six2.set_max_odds(strArray2[3]);
                                _six2.set_min_odds(strArray3[3]);
                                _six2.set_b_diff(strArray4[3]);
                                _six2.set_c_diff(strArray5[3]);
                                CallBLL.cz_odds_six_bll.SetOddsByOddsSet_BB(_six2, num37.ToString());
                                _six2.set_play_name("半波【單雙】");
                                _six2.set_put_amount("綠波單");
                                _six2.set_current_odds(strArray[4]);
                                _six2.set_max_odds(strArray2[4]);
                                _six2.set_min_odds(strArray3[4]);
                                _six2.set_b_diff(strArray4[4]);
                                _six2.set_c_diff(strArray5[4]);
                                CallBLL.cz_odds_six_bll.SetOddsByOddsSet_BB(_six2, num37.ToString());
                                _six2.set_play_name("半波【單雙】");
                                _six2.set_put_amount("綠波雙");
                                _six2.set_current_odds(strArray[5]);
                                _six2.set_max_odds(strArray2[5]);
                                _six2.set_min_odds(strArray3[5]);
                                _six2.set_b_diff(strArray4[5]);
                                _six2.set_c_diff(strArray5[5]);
                                CallBLL.cz_odds_six_bll.SetOddsByOddsSet_BB(_six2, num37.ToString());
                            }
                            else if (_six.get_play_id().Equals(0x163b1))
                            {
                                string[] strArray6 = _six.get_default_odds().Split(new char[] { '|' });
                                string[] strArray7 = _six.get_max_odds().Split(new char[] { '|' });
                                string[] strArray8 = _six.get_min_odds().Split(new char[] { '|' });
                                string[] strArray9 = _six.get_b_diff().Split(new char[] { '|' });
                                string[] strArray10 = _six.get_c_diff().Split(new char[] { '|' });
                                cz_odds_six _six3 = new cz_odds_six();
                                _six3.set_play_id(new int?(_six.get_play_id()));
                                _six3.set_play_name("半波【大小】");
                                _six3.set_put_amount("紅波大");
                                _six3.set_current_odds(strArray6[0]);
                                _six3.set_max_odds(strArray7[0]);
                                _six3.set_min_odds(strArray8[0]);
                                _six3.set_b_diff(strArray9[0]);
                                _six3.set_c_diff(strArray10[0]);
                                CallBLL.cz_odds_six_bll.SetOddsByOddsSet_BB(_six3, num37.ToString());
                                _six3.set_play_name("半波【大小】");
                                _six3.set_put_amount("紅波小");
                                _six3.set_current_odds(strArray6[1]);
                                _six3.set_max_odds(strArray7[1]);
                                _six3.set_min_odds(strArray8[1]);
                                _six3.set_b_diff(strArray9[1]);
                                _six3.set_c_diff(strArray10[1]);
                                CallBLL.cz_odds_six_bll.SetOddsByOddsSet_BB(_six3, num37.ToString());
                                _six3.set_play_name("半波【大小】");
                                _six3.set_put_amount("藍波大");
                                _six3.set_current_odds(strArray6[2]);
                                _six3.set_max_odds(strArray7[2]);
                                _six3.set_min_odds(strArray8[2]);
                                _six3.set_b_diff(strArray9[2]);
                                _six3.set_c_diff(strArray10[2]);
                                CallBLL.cz_odds_six_bll.SetOddsByOddsSet_BB(_six3, num37.ToString());
                                _six3.set_play_name("半波【大小】");
                                _six3.set_put_amount("藍波小");
                                _six3.set_current_odds(strArray6[3]);
                                _six3.set_max_odds(strArray7[3]);
                                _six3.set_min_odds(strArray8[3]);
                                _six3.set_b_diff(strArray9[3]);
                                _six3.set_c_diff(strArray10[3]);
                                CallBLL.cz_odds_six_bll.SetOddsByOddsSet_BB(_six3, num37.ToString());
                                _six3.set_play_name("半波【大小】");
                                _six3.set_put_amount("綠波大");
                                _six3.set_current_odds(strArray6[4]);
                                _six3.set_max_odds(strArray7[4]);
                                _six3.set_min_odds(strArray8[4]);
                                _six3.set_b_diff(strArray9[4]);
                                _six3.set_c_diff(strArray10[4]);
                                CallBLL.cz_odds_six_bll.SetOddsByOddsSet_BB(_six3, num37.ToString());
                                _six3.set_play_name("半波【大小】");
                                _six3.set_put_amount("綠波小");
                                _six3.set_current_odds(strArray6[5]);
                                _six3.set_max_odds(strArray7[5]);
                                _six3.set_min_odds(strArray8[5]);
                                _six3.set_b_diff(strArray9[5]);
                                _six3.set_c_diff(strArray10[5]);
                                CallBLL.cz_odds_six_bll.SetOddsByOddsSet_BB(_six3, num37.ToString());
                            }
                            else if (_six.get_play_id().Equals(0x163ac))
                            {
                                string[] strArray11 = _six.get_default_odds().Split(new char[] { '|' });
                                string[] strArray12 = _six.get_max_odds().Split(new char[] { '|' });
                                string[] strArray13 = _six.get_min_odds().Split(new char[] { '|' });
                                string[] strArray14 = _six.get_b_diff().Split(new char[] { '|' });
                                string[] strArray15 = _six.get_c_diff().Split(new char[] { '|' });
                                cz_odds_six _six4 = new cz_odds_six();
                                _six4.set_play_id(new int?(_six.get_play_id()));
                                _six4.set_play_name("七碼單雙");
                                _six4.set_put_amount("單0、雙7");
                                _six4.set_current_odds(strArray11[0]);
                                _six4.set_max_odds(strArray12[0]);
                                _six4.set_min_odds(strArray13[0]);
                                _six4.set_b_diff(strArray14[0]);
                                _six4.set_c_diff(strArray15[0]);
                                CallBLL.cz_odds_six_bll.SetOddsByOddsSet_QM(_six4, num37.ToString());
                                _six4.set_play_id(new int?(_six.get_play_id()));
                                _six4.set_play_name("七碼單雙");
                                _six4.set_put_amount("單1、雙6");
                                _six4.set_current_odds(strArray11[1]);
                                _six4.set_max_odds(strArray12[1]);
                                _six4.set_min_odds(strArray13[1]);
                                _six4.set_b_diff(strArray14[1]);
                                _six4.set_c_diff(strArray15[1]);
                                CallBLL.cz_odds_six_bll.SetOddsByOddsSet_QM(_six4, num37.ToString());
                                _six4.set_play_id(new int?(_six.get_play_id()));
                                _six4.set_play_name("七碼單雙");
                                _six4.set_put_amount("單2、雙5");
                                _six4.set_current_odds(strArray11[2]);
                                _six4.set_max_odds(strArray12[2]);
                                _six4.set_min_odds(strArray13[2]);
                                _six4.set_b_diff(strArray14[2]);
                                _six4.set_c_diff(strArray15[2]);
                                CallBLL.cz_odds_six_bll.SetOddsByOddsSet_QM(_six4, num37.ToString());
                                _six4.set_play_id(new int?(_six.get_play_id()));
                                _six4.set_play_name("七碼單雙");
                                _six4.set_put_amount("單3、雙4");
                                _six4.set_current_odds(strArray11[3]);
                                _six4.set_max_odds(strArray12[3]);
                                _six4.set_min_odds(strArray13[3]);
                                _six4.set_b_diff(strArray14[3]);
                                _six4.set_c_diff(strArray15[3]);
                                CallBLL.cz_odds_six_bll.SetOddsByOddsSet_QM(_six4, num37.ToString());
                                _six4.set_play_id(new int?(_six.get_play_id()));
                                _six4.set_play_name("七碼單雙");
                                _six4.set_put_amount("單4、雙3");
                                _six4.set_current_odds(strArray11[4]);
                                _six4.set_max_odds(strArray12[4]);
                                _six4.set_min_odds(strArray13[4]);
                                _six4.set_b_diff(strArray14[4]);
                                _six4.set_c_diff(strArray15[4]);
                                CallBLL.cz_odds_six_bll.SetOddsByOddsSet_QM(_six4, num37.ToString());
                                _six4.set_play_id(new int?(_six.get_play_id()));
                                _six4.set_play_name("七碼單雙");
                                _six4.set_put_amount("單5、雙2");
                                _six4.set_current_odds(strArray11[5]);
                                _six4.set_max_odds(strArray12[5]);
                                _six4.set_min_odds(strArray13[5]);
                                _six4.set_b_diff(strArray14[5]);
                                _six4.set_c_diff(strArray15[5]);
                                CallBLL.cz_odds_six_bll.SetOddsByOddsSet_QM(_six4, num37.ToString());
                                _six4.set_play_id(new int?(_six.get_play_id()));
                                _six4.set_play_name("七碼單雙");
                                _six4.set_put_amount("單6、雙1");
                                _six4.set_current_odds(strArray11[6]);
                                _six4.set_max_odds(strArray12[6]);
                                _six4.set_min_odds(strArray13[6]);
                                _six4.set_b_diff(strArray14[6]);
                                _six4.set_c_diff(strArray15[6]);
                                CallBLL.cz_odds_six_bll.SetOddsByOddsSet_QM(_six4, num37.ToString());
                                _six4.set_play_id(new int?(_six.get_play_id()));
                                _six4.set_play_name("七碼單雙");
                                _six4.set_put_amount("單7、雙0");
                                _six4.set_current_odds(strArray11[7]);
                                _six4.set_max_odds(strArray12[7]);
                                _six4.set_min_odds(strArray13[7]);
                                _six4.set_b_diff(strArray14[7]);
                                _six4.set_c_diff(strArray15[7]);
                                CallBLL.cz_odds_six_bll.SetOddsByOddsSet_QM(_six4, num37.ToString());
                            }
                            else if (_six.get_play_id().Equals(0x163b0))
                            {
                                string[] strArray16 = _six.get_default_odds().Split(new char[] { '|' });
                                string[] strArray17 = _six.get_max_odds().Split(new char[] { '|' });
                                string[] strArray18 = _six.get_min_odds().Split(new char[] { '|' });
                                string[] strArray19 = _six.get_b_diff().Split(new char[] { '|' });
                                string[] strArray20 = _six.get_c_diff().Split(new char[] { '|' });
                                cz_odds_six _six5 = new cz_odds_six();
                                _six5.set_play_id(new int?(_six.get_play_id()));
                                _six5.set_play_name("七碼大小");
                                _six5.set_put_amount("大0、小7");
                                _six5.set_current_odds(strArray16[0]);
                                _six5.set_max_odds(strArray17[0]);
                                _six5.set_min_odds(strArray18[0]);
                                _six5.set_b_diff(strArray19[0]);
                                _six5.set_c_diff(strArray20[0]);
                                CallBLL.cz_odds_six_bll.SetOddsByOddsSet_QM(_six5, num37.ToString());
                                _six5.set_play_id(new int?(_six.get_play_id()));
                                _six5.set_play_name("七碼大小");
                                _six5.set_put_amount("大1、小6");
                                _six5.set_current_odds(strArray16[1]);
                                _six5.set_max_odds(strArray17[1]);
                                _six5.set_min_odds(strArray18[1]);
                                _six5.set_b_diff(strArray19[1]);
                                _six5.set_c_diff(strArray20[1]);
                                CallBLL.cz_odds_six_bll.SetOddsByOddsSet_QM(_six5, num37.ToString());
                                _six5.set_play_id(new int?(_six.get_play_id()));
                                _six5.set_play_name("七碼大小");
                                _six5.set_put_amount("大2、小5");
                                _six5.set_current_odds(strArray16[2]);
                                _six5.set_max_odds(strArray17[2]);
                                _six5.set_min_odds(strArray18[2]);
                                _six5.set_b_diff(strArray19[2]);
                                _six5.set_c_diff(strArray20[2]);
                                CallBLL.cz_odds_six_bll.SetOddsByOddsSet_QM(_six5, num37.ToString());
                                _six5.set_play_id(new int?(_six.get_play_id()));
                                _six5.set_play_name("七碼大小");
                                _six5.set_put_amount("大3、小4");
                                _six5.set_current_odds(strArray16[3]);
                                _six5.set_max_odds(strArray17[3]);
                                _six5.set_min_odds(strArray18[3]);
                                _six5.set_b_diff(strArray19[3]);
                                _six5.set_c_diff(strArray20[3]);
                                CallBLL.cz_odds_six_bll.SetOddsByOddsSet_QM(_six5, num37.ToString());
                                _six5.set_play_id(new int?(_six.get_play_id()));
                                _six5.set_play_name("七碼大小");
                                _six5.set_put_amount("大4、小3");
                                _six5.set_current_odds(strArray16[4]);
                                _six5.set_max_odds(strArray17[4]);
                                _six5.set_min_odds(strArray18[4]);
                                _six5.set_b_diff(strArray19[4]);
                                _six5.set_c_diff(strArray20[4]);
                                CallBLL.cz_odds_six_bll.SetOddsByOddsSet_QM(_six5, num37.ToString());
                                _six5.set_play_id(new int?(_six.get_play_id()));
                                _six5.set_play_name("七碼大小");
                                _six5.set_put_amount("大5、小2");
                                _six5.set_current_odds(strArray16[5]);
                                _six5.set_max_odds(strArray17[5]);
                                _six5.set_min_odds(strArray18[5]);
                                _six5.set_b_diff(strArray19[5]);
                                _six5.set_c_diff(strArray20[5]);
                                CallBLL.cz_odds_six_bll.SetOddsByOddsSet_QM(_six5, num37.ToString());
                                _six5.set_play_id(new int?(_six.get_play_id()));
                                _six5.set_play_name("七碼大小");
                                _six5.set_put_amount("大6、小1");
                                _six5.set_current_odds(strArray16[6]);
                                _six5.set_max_odds(strArray17[6]);
                                _six5.set_min_odds(strArray18[6]);
                                _six5.set_b_diff(strArray19[6]);
                                _six5.set_c_diff(strArray20[6]);
                                CallBLL.cz_odds_six_bll.SetOddsByOddsSet_QM(_six5, num37.ToString());
                                _six5.set_play_id(new int?(_six.get_play_id()));
                                _six5.set_play_name("七碼大小");
                                _six5.set_put_amount("大7、小0");
                                _six5.set_current_odds(strArray16[7]);
                                _six5.set_max_odds(strArray17[7]);
                                _six5.set_min_odds(strArray18[7]);
                                _six5.set_b_diff(strArray19[7]);
                                _six5.set_c_diff(strArray20[7]);
                                CallBLL.cz_odds_six_bll.SetOddsByOddsSet_QM(_six5, num37.ToString());
                            }
                            else if (_six.get_play_id().Equals(0x163ad))
                            {
                                string[] strArray21 = _six.get_default_odds().Split(new char[] { '|' });
                                string[] strArray22 = _six.get_max_odds().Split(new char[] { '|' });
                                string[] strArray23 = _six.get_min_odds().Split(new char[] { '|' });
                                string[] strArray24 = _six.get_b_diff().Split(new char[] { '|' });
                                string[] strArray25 = _six.get_c_diff().Split(new char[] { '|' });
                                cz_odds_six _six6 = new cz_odds_six();
                                _six6.set_play_id(new int?(_six.get_play_id()));
                                _six6.set_play_name("五行");
                                _six6.set_put_amount("金");
                                _six6.set_current_odds(strArray21[0]);
                                _six6.set_max_odds(strArray22[0]);
                                _six6.set_min_odds(strArray23[0]);
                                _six6.set_b_diff(strArray24[0]);
                                _six6.set_c_diff(strArray25[0]);
                                CallBLL.cz_odds_six_bll.SetOddsByOddsSet_WX(_six6, num37.ToString());
                                _six6.set_play_id(new int?(_six.get_play_id()));
                                _six6.set_play_name("五行");
                                _six6.set_put_amount("木");
                                _six6.set_current_odds(strArray21[1]);
                                _six6.set_max_odds(strArray22[1]);
                                _six6.set_min_odds(strArray23[1]);
                                _six6.set_b_diff(strArray24[1]);
                                _six6.set_c_diff(strArray25[1]);
                                CallBLL.cz_odds_six_bll.SetOddsByOddsSet_WX(_six6, num37.ToString());
                                _six6.set_play_id(new int?(_six.get_play_id()));
                                _six6.set_play_name("五行");
                                _six6.set_put_amount("水");
                                _six6.set_current_odds(strArray21[2]);
                                _six6.set_max_odds(strArray22[2]);
                                _six6.set_min_odds(strArray23[2]);
                                _six6.set_b_diff(strArray24[2]);
                                _six6.set_c_diff(strArray25[2]);
                                CallBLL.cz_odds_six_bll.SetOddsByOddsSet_WX(_six6, num37.ToString());
                                _six6.set_play_id(new int?(_six.get_play_id()));
                                _six6.set_play_name("五行");
                                _six6.set_put_amount("火");
                                _six6.set_current_odds(strArray21[3]);
                                _six6.set_max_odds(strArray22[3]);
                                _six6.set_min_odds(strArray23[3]);
                                _six6.set_b_diff(strArray24[3]);
                                _six6.set_c_diff(strArray25[3]);
                                CallBLL.cz_odds_six_bll.SetOddsByOddsSet_WX(_six6, num37.ToString());
                                _six6.set_play_id(new int?(_six.get_play_id()));
                                _six6.set_play_name("五行");
                                _six6.set_put_amount("土");
                                _six6.set_current_odds(strArray21[4]);
                                _six6.set_max_odds(strArray22[4]);
                                _six6.set_min_odds(strArray23[4]);
                                _six6.set_b_diff(strArray24[4]);
                                _six6.set_c_diff(strArray25[4]);
                                CallBLL.cz_odds_six_bll.SetOddsByOddsSet_WX(_six6, num37.ToString());
                            }
                            else
                            {
                                CallBLL.cz_odds_six_bll.SetOddsByOddsSet(_six, num37.ToString());
                            }
                        }
                        catch (Exception exception)
                        {
                            string message = exception.Message;
                        }
                    }
                    base.plsd_six_log(rs, CallBLL.cz_play_six_bll.GetPlayByOddsSet("91025,91026,91027,91028,91029").Tables[0]);
                }
            }
            string url = "/OddsSet/OddsSet_six.aspx?lid=" + this.lotteryId;
            base.Response.Write(base.ShowDialogBox("賠率設定參數已經成功保存！", url, 0));
            base.Response.End();
        }

        private void ValidInput_SIX(DataTable rs)
        {
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                int num2 = int.Parse(rs.Rows[i]["play_id"].ToString());
                string str = rs.Rows[i]["play_name"].ToString().Trim().Replace("正1特", "正特1-6");
                if (((string.IsNullOrEmpty(LSRequest.qq("t" + num2.ToString() + "_default_odds_1")) || string.IsNullOrEmpty(LSRequest.qq("t" + num2.ToString() + "_max_odds_1"))) || (string.IsNullOrEmpty(LSRequest.qq("t" + num2.ToString() + "_min_odds_1")) || string.IsNullOrEmpty(LSRequest.qq("t" + num2.ToString() + "_b_diff_1")))) || ((string.IsNullOrEmpty(LSRequest.qq("t" + num2.ToString() + "_c_diff_1")) || string.IsNullOrEmpty(LSRequest.qq("t" + num2.ToString() + "_downbase"))) || string.IsNullOrEmpty(LSRequest.qq("t" + num2.ToString() + "_down_odds_rate"))))
                {
                    base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率設定不能為空！", str), null, 400));
                    base.Response.End();
                }
                if ((((num2 == 0x16389) || (num2 == 0x1638b)) || ((num2 == 0x16385) || (num2 == 0x1637e))) || ((((num2 == 0x1637f) || (num2 == 0x1638d)) || ((num2 == 0x1638e) || (num2 == 0x163ae))) || (((num2 == 0x163af) || (num2 == 0x163b5)) || (num2 == 0x163b7))))
                {
                    if ((string.IsNullOrEmpty(LSRequest.qq("t" + num2.ToString() + "_default_odds_2")) || string.IsNullOrEmpty(LSRequest.qq("t" + num2.ToString() + "_max_odds_2"))) || ((string.IsNullOrEmpty(LSRequest.qq("t" + num2.ToString() + "_min_odds_2")) || string.IsNullOrEmpty(LSRequest.qq("t" + num2.ToString() + "_b_diff_2"))) || string.IsNullOrEmpty(LSRequest.qq("t" + num2.ToString() + "_c_diff_2"))))
                    {
                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率設定不能為空！", str), null, 400));
                        base.Response.End();
                    }
                }
                else if ((num2 == 0x16380) || (num2 == 0x163b1))
                {
                    for (int k = 1; k < 6; k++)
                    {
                        if ((string.IsNullOrEmpty(LSRequest.qq(string.Concat(new object[] { "t", num2.ToString(), "_default_odds_", k + 1 }))) || string.IsNullOrEmpty(LSRequest.qq(string.Concat(new object[] { "t", num2.ToString(), "_max_odds_", k + 1 })))) || ((string.IsNullOrEmpty(LSRequest.qq(string.Concat(new object[] { "t", num2.ToString(), "_min_odds_", k + 1 }))) || string.IsNullOrEmpty(LSRequest.qq(string.Concat(new object[] { "t", num2.ToString(), "_b_diff_", k + 1 })))) || string.IsNullOrEmpty(LSRequest.qq(string.Concat(new object[] { "t", num2.ToString(), "_c_diff_", k + 1 })))))
                        {
                            base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率設定不能為空！", str), null, 400));
                            base.Response.End();
                        }
                    }
                }
                else if ((((num2 == 0x16397) || (num2 == 0x16398)) || ((num2 == 0x16399) || (num2 == 0x163b2))) || (((num2 == 0x1639a) || (num2 == 0x1639b)) || ((num2 == 0x1639c) || (num2 == 0x163b3))))
                {
                    if ((string.IsNullOrEmpty(LSRequest.qq("t" + num2.ToString() + "_default_odds_2")) || string.IsNullOrEmpty(LSRequest.qq("t" + num2.ToString() + "_max_odds_2"))) || string.IsNullOrEmpty(LSRequest.qq("t" + num2.ToString() + "_min_odds_2")))
                    {
                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率設定不能為空！", str), null, 400));
                        base.Response.End();
                    }
                }
                else if (num2 == 0x163ac)
                {
                    for (int m = 1; m < 8; m++)
                    {
                        if ((string.IsNullOrEmpty(LSRequest.qq(string.Concat(new object[] { "t", num2.ToString(), "_default_odds_", m + 1 }))) || string.IsNullOrEmpty(LSRequest.qq(string.Concat(new object[] { "t", num2.ToString(), "_max_odds_", m + 1 })))) || ((string.IsNullOrEmpty(LSRequest.qq(string.Concat(new object[] { "t", num2.ToString(), "_min_odds_", m + 1 }))) || string.IsNullOrEmpty(LSRequest.qq(string.Concat(new object[] { "t", num2.ToString(), "_b_diff_", m + 1 })))) || string.IsNullOrEmpty(LSRequest.qq(string.Concat(new object[] { "t", num2.ToString(), "_c_diff_", m + 1 })))))
                        {
                            base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率設定不能為空！", str), null, 400));
                            base.Response.End();
                        }
                    }
                }
                else if (num2 == 0x163b0)
                {
                    for (int n = 1; n < 8; n++)
                    {
                        if ((string.IsNullOrEmpty(LSRequest.qq(string.Concat(new object[] { "t", num2.ToString(), "_default_odds_", n + 1 }))) || string.IsNullOrEmpty(LSRequest.qq(string.Concat(new object[] { "t", num2.ToString(), "_max_odds_", n + 1 })))) || ((string.IsNullOrEmpty(LSRequest.qq(string.Concat(new object[] { "t", num2.ToString(), "_min_odds_", n + 1 }))) || string.IsNullOrEmpty(LSRequest.qq(string.Concat(new object[] { "t", num2.ToString(), "_b_diff_", n + 1 })))) || string.IsNullOrEmpty(LSRequest.qq(string.Concat(new object[] { "t", num2.ToString(), "_c_diff_", n + 1 })))))
                        {
                            base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率設定不能為空！", str), null, 400));
                            base.Response.End();
                        }
                    }
                }
                else if (num2 == 0x163ad)
                {
                    for (int num6 = 1; num6 < 5; num6++)
                    {
                        if ((string.IsNullOrEmpty(LSRequest.qq(string.Concat(new object[] { "t", num2.ToString(), "_default_odds_", num6 + 1 }))) || string.IsNullOrEmpty(LSRequest.qq(string.Concat(new object[] { "t", num2.ToString(), "_max_odds_", num6 + 1 })))) || ((string.IsNullOrEmpty(LSRequest.qq(string.Concat(new object[] { "t", num2.ToString(), "_min_odds_", num6 + 1 }))) || string.IsNullOrEmpty(LSRequest.qq(string.Concat(new object[] { "t", num2.ToString(), "_b_diff_", num6 + 1 })))) || string.IsNullOrEmpty(LSRequest.qq(string.Concat(new object[] { "t", num2.ToString(), "_c_diff_", num6 + 1 })))))
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
                int num8 = int.Parse(rs.Rows[j]["play_id"].ToString());
                string str2 = rs.Rows[j]["play_name"].ToString().Trim().Replace("正1特", "正特1-6");
                if (((!regex.IsMatch(LSRequest.qq("t" + num8.ToString() + "_default_odds_1")) || !regex.IsMatch(LSRequest.qq("t" + num8.ToString() + "_max_odds_1"))) || (!regex.IsMatch(LSRequest.qq("t" + num8.ToString() + "_min_odds_1")) || !regex.IsMatch(LSRequest.qq("t" + num8.ToString() + "_b_diff_1")))) || ((!regex.IsMatch(LSRequest.qq("t" + num8.ToString() + "_c_diff_1")) || !regex.IsMatch(LSRequest.qq("t" + num8.ToString() + "_downbase"))) || !regex.IsMatch(LSRequest.qq("t" + num8.ToString() + "_down_odds_rate"))))
                {
                    base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率設定必須為數字或小數！", str2), null, 400));
                    base.Response.End();
                }
                if ((((num8 == 0x16389) || (num8 == 0x1638b)) || ((num8 == 0x16385) || (num8 == 0x1637e))) || ((((num8 == 0x1637f) || (num8 == 0x1638d)) || ((num8 == 0x1638e) || (num8 == 0x163ae))) || (((num8 == 0x163af) || (num8 == 0x163b5)) || (num8 == 0x163b7))))
                {
                    if ((!regex.IsMatch(LSRequest.qq("t" + num8.ToString() + "_default_odds_2")) || !regex.IsMatch(LSRequest.qq("t" + num8.ToString() + "_max_odds_2"))) || ((!regex.IsMatch(LSRequest.qq("t" + num8.ToString() + "_min_odds_2")) || !regex.IsMatch(LSRequest.qq("t" + num8.ToString() + "_b_diff_2"))) || !regex.IsMatch(LSRequest.qq("t" + num8.ToString() + "_c_diff_2"))))
                    {
                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率設定必須為數字或小數！", str2), null, 400));
                        base.Response.End();
                    }
                }
                else if ((num8 == 0x16380) || (num8 == 0x163b1))
                {
                    for (int num9 = 1; num9 < 6; num9++)
                    {
                        if ((!regex.IsMatch(LSRequest.qq(string.Concat(new object[] { "t", num8.ToString(), "_default_odds_", num9 + 1 }))) || !regex.IsMatch(LSRequest.qq(string.Concat(new object[] { "t", num8.ToString(), "_max_odds_", num9 + 1 })))) || ((!regex.IsMatch(LSRequest.qq(string.Concat(new object[] { "t", num8.ToString(), "_min_odds_", num9 + 1 }))) || !regex.IsMatch(LSRequest.qq(string.Concat(new object[] { "t", num8.ToString(), "_b_diff_", num9 + 1 })))) || !regex.IsMatch(LSRequest.qq(string.Concat(new object[] { "t", num8.ToString(), "_c_diff_", num9 + 1 })))))
                        {
                            base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率設定必須為數字或小數！", str2), null, 400));
                            base.Response.End();
                        }
                    }
                }
                else if ((((num8 == 0x16397) || (num8 == 0x16398)) || ((num8 == 0x16399) || (num8 == 0x163b2))) || (((num8 == 0x1639a) || (num8 == 0x1639b)) || ((num8 == 0x1639c) || (num8 == 0x163b3))))
                {
                    if ((!regex.IsMatch(LSRequest.qq("t" + num8.ToString() + "_default_odds_2")) || !regex.IsMatch(LSRequest.qq("t" + num8.ToString() + "_max_odds_2"))) || !regex.IsMatch(LSRequest.qq("t" + num8.ToString() + "_min_odds_2")))
                    {
                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率設定必須為數字或小數！", str2), null, 400));
                        base.Response.End();
                    }
                }
                else if (num8 == 0x163ac)
                {
                    for (int num10 = 1; num10 < 8; num10++)
                    {
                        if ((!regex.IsMatch(LSRequest.qq(string.Concat(new object[] { "t", num8.ToString(), "_default_odds_", num10 + 1 }))) || !regex.IsMatch(LSRequest.qq(string.Concat(new object[] { "t", num8.ToString(), "_max_odds_", num10 + 1 })))) || ((!regex.IsMatch(LSRequest.qq(string.Concat(new object[] { "t", num8.ToString(), "_min_odds_", num10 + 1 }))) || !regex.IsMatch(LSRequest.qq(string.Concat(new object[] { "t", num8.ToString(), "_b_diff_", num10 + 1 })))) || !regex.IsMatch(LSRequest.qq(string.Concat(new object[] { "t", num8.ToString(), "_c_diff_", num10 + 1 })))))
                        {
                            base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率設定必須為數字或小數！", str2), null, 400));
                            base.Response.End();
                        }
                    }
                }
                else if (num8 == 0x163b0)
                {
                    for (int num11 = 1; num11 < 8; num11++)
                    {
                        if ((!regex.IsMatch(LSRequest.qq(string.Concat(new object[] { "t", num8.ToString(), "_default_odds_", num11 + 1 }))) || !regex.IsMatch(LSRequest.qq(string.Concat(new object[] { "t", num8.ToString(), "_max_odds_", num11 + 1 })))) || ((!regex.IsMatch(LSRequest.qq(string.Concat(new object[] { "t", num8.ToString(), "_min_odds_", num11 + 1 }))) || !regex.IsMatch(LSRequest.qq(string.Concat(new object[] { "t", num8.ToString(), "_b_diff_", num11 + 1 })))) || !regex.IsMatch(LSRequest.qq(string.Concat(new object[] { "t", num8.ToString(), "_c_diff_", num11 + 1 })))))
                        {
                            base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率設定必須為數字或小數！", str2), null, 400));
                            base.Response.End();
                        }
                    }
                }
                else if (num8 == 0x163ad)
                {
                    for (int num12 = 1; num12 < 5; num12++)
                    {
                        if ((!regex.IsMatch(LSRequest.qq(string.Concat(new object[] { "t", num8.ToString(), "_default_odds_", num12 + 1 }))) || !regex.IsMatch(LSRequest.qq(string.Concat(new object[] { "t", num8.ToString(), "_max_odds_", num12 + 1 })))) || ((!regex.IsMatch(LSRequest.qq(string.Concat(new object[] { "t", num8.ToString(), "_min_odds_", num12 + 1 }))) || !regex.IsMatch(LSRequest.qq(string.Concat(new object[] { "t", num8.ToString(), "_b_diff_", num12 + 1 })))) || !regex.IsMatch(LSRequest.qq(string.Concat(new object[] { "t", num8.ToString(), "_c_diff_", num12 + 1 })))))
                        {
                            base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 賠率設定必須為數字或小數！", str2), null, 400));
                            base.Response.End();
                        }
                    }
                }
            }
        }
    }
}

