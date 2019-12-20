namespace User.Web.ReportBill
{
    using LotterySystem.Common;
    using System;
    using System.Data;
    using System.Web;
    using User.Web.WebBase;

    public class GroupShow_kc : MemberPageBase
    {
        protected string groupString = "";
        protected string is_payment = "0";
        protected bool isDoubleOdds = false;
        protected string masterId = 2.ToString();

        public string GetBetGroupStringPaymentFalse(string play_id, string odds_id, string play_name, string bet_group, string odds, string amount)
        {
            string str = "";
            string[] strArray = bet_group.Split(new char[] { '~' });
            for (int i = 0; i < strArray.Length; i++)
            {
                str = str + "<tr onMouseOut=\"this.style.backgroundColor=''\" onMouseOver=\"this.style.backgroundColor='#FFFFA2'\">";
                string str2 = strArray[i];
                string s = "0";
                s = (double.Parse(odds) * double.Parse(amount)).ToString();
                string str4 = string.Format("{0:F1}", double.Parse(amount));
                string str5 = string.Format("{0:F1}", double.Parse(s) - double.Parse(amount));
                string str6 = string.Format("<span  class='blue'>{0}</span>@<b class='orange'>{1}</b>", play_name, odds);
                if (odds_id.Equals("330") || odds_id.Equals("1200"))
                {
                    goto Label_01A5;
                }
                if (!odds_id.Equals("72055"))
                {
                    goto Label_019B;
                }
                str2 = this.px_pcdd(str2);
                string str7 = "";
                int index = 0;
                goto Label_0170;
            Label_00F7:
                if (index == 0)
                {
                    str7 = str7 + int.Parse(str2.Split(new char[] { ',' })[index]);
                }
                else
                {
                    str7 = str7 + "," + int.Parse(str2.Split(new char[] { ',' })[index]);
                }
                index++;
            Label_0170:;
                if (index < str2.Split(new char[] { ',' }).Length)
                {
                    goto Label_00F7;
                }
                str2 = str7;
                goto Label_01A5;
            Label_019B:
                str2 = this.px(str2);
            Label_01A5:;
                str = str + string.Format("<td>{0}</td><td>{1}</td><td>{2}</td><td align='right'>{3}</td><td align='right'>{4}</td>", new object[] { i + 1, str2.Replace(',', '、'), str6, str4, str5 }) + "</tr>";
            }
            str = str + "<tr  style='font-weight:bold'>";
            string str8 = string.Format("{0:F1}", double.Parse(amount) * strArray.Length);
            string str9 = string.Format("{0:F1}", ((double.Parse(amount) * double.Parse(odds)) * strArray.Length) - (double.Parse(amount) * strArray.Length));
            return (str + string.Format("<th>{0}</th><th>{1}</th><th>{2}</th><th align='right'>{3}&nbsp;</th><th align='right'>{4}&nbsp;</th>", new object[] { "合計：", "共" + strArray.Length + "組", "", str8, str9 }) + "</tr>");
        }

        public string GetBetGroupStringPaymentTrue(string phase_id, string play_id, string odds_id, string play_name, string bet_group, string odds, string amount, string lottery_type, double drawback)
        {
            string str = "";
            string str2 = "";
            string str3 = "";
            string str4 = "";
            string str5 = "";
            string str6 = "";
            string str7 = "";
            string str8 = "";
            string zqh = "";
            string str10 = "";
            string str11 = "";
            string str12 = "";
            string str14 = "";
            DataTable phaseByPID = null;
            int num5 = 0;
            if (lottery_type.Equals(num5.ToString()))
            {
                phaseByPID = CallBLL.cz_phase_kl10_bll.GetPhaseByPID(phase_id);
            }
            else
            {
                num5 = 3;
                if (lottery_type.Equals(num5.ToString()))
                {
                    phaseByPID = CallBLL.cz_phase_xync_bll.GetPhaseByPID(phase_id);
                }
                else
                {
                    num5 = 7;
                    if (lottery_type.Equals(num5.ToString()))
                    {
                        phaseByPID = CallBLL.cz_phase_pcdd_bll.GetPhaseByPID(phase_id);
                    }
                    else
                    {
                        num5 = 14;
                        if (lottery_type.Equals(num5.ToString()))
                        {
                            phaseByPID = CallBLL.cz_phase_jssfc_bll.GetPhaseByPID(phase_id);
                        }
                        else
                        {
                            return "";
                        }
                    }
                }
            }
            if (phaseByPID == null)
            {
                return "";
            }
            string str15 = phaseByPID.Rows[0]["phase"].ToString().Trim();
            num5 = 7;
            if (lottery_type.Equals(num5.ToString()))
            {
                str = phaseByPID.Rows[0]["n1"].ToString().Trim();
                str2 = phaseByPID.Rows[0]["n2"].ToString().Trim();
                str3 = phaseByPID.Rows[0]["n3"].ToString().Trim();
                zqh = phaseByPID.Rows[0]["sn"].ToString().Trim();
            }
            else
            {
                str = phaseByPID.Rows[0]["n1"].ToString().Trim();
                str2 = phaseByPID.Rows[0]["n2"].ToString().Trim();
                str3 = phaseByPID.Rows[0]["n3"].ToString().Trim();
                str4 = phaseByPID.Rows[0]["n4"].ToString().Trim();
                str5 = phaseByPID.Rows[0]["n5"].ToString().Trim();
                str6 = phaseByPID.Rows[0]["n6"].ToString().Trim();
                str7 = phaseByPID.Rows[0]["n7"].ToString().Trim();
                str8 = phaseByPID.Rows[0]["n8"].ToString().Trim();
                str10 = str + "," + str2 + "," + str3 + "," + str4 + "," + str5 + "," + str6 + "," + str7 + "," + str8;
                str11 = str + "," + str2;
                str12 = str + "," + str2 + "," + str3;
                str14 = str + "," + str2 + "," + str3 + "," + str4 + "," + str5 + "," + str6 + "," + str7 + "," + str8;
            }
            string str16 = "";
            string[] strArray = bet_group.Split(new char[] { '~' });
            double num = 0.0;
            double num2 = 0.0;
            for (int i = 0; i < strArray.Length; i++)
            {
                string str25;
                string xzh = strArray[i];
                string s = "0";
                num5 = 0;
                if (lottery_type.Equals(num5.ToString()))
                {
                    str25 = play_id;
                    switch (str25)
                    {
                        case "72":
                            s = base.count_rqz_kl10(xzh, str10, double.Parse(amount), odds).ToString();
                            break;

                        case "74":
                            s = base.count_rlz_kl10(xzh, str10, double.Parse(amount), odds).ToString();
                            break;

                        case "73":
                            s = base.count_rsxz_kl10(xzh, str10, double.Parse(amount), odds).ToString();
                            break;

                        case "75":
                            s = base.count_sqz_kl10(xzh, str10, double.Parse(amount), odds).ToString();
                            break;

                        case "77":
                            s = base.count_sqz_kl10(xzh, str12, double.Parse(amount), odds).ToString();
                            break;

                        case "76":
                            s = base.count_rsxz_kl10(xzh, str12, double.Parse(amount), odds).ToString();
                            break;

                        case "78":
                            s = base.count_sqz4_kl10(xzh, str10, double.Parse(amount), odds).ToString();
                            break;

                        case "79":
                            s = base.count_wqz5_kl10(xzh, str10, double.Parse(amount), odds).ToString();
                            break;
                    }
                }
                num5 = 3;
                if (lottery_type.Equals(num5.ToString()))
                {
                    str25 = play_id;
                    if (str25 != null)
                    {
                        if (!(str25 == "72"))
                        {
                            if (str25 == "74")
                            {
                                goto Label_075C;
                            }
                            if (str25 == "73")
                            {
                                goto Label_077F;
                            }
                            if (str25 == "75")
                            {
                                goto Label_079F;
                            }
                            if (str25 == "78")
                            {
                                goto Label_07BF;
                            }
                            if (str25 == "79")
                            {
                                goto Label_07DF;
                            }
                        }
                        else
                        {
                            s = base.count_rqz_xync(xzh, str10, double.Parse(amount), odds).ToString();
                        }
                    }
                }
                goto Label_0800;
            Label_075C:
                s = base.count_rlz_xync(xzh, str10, double.Parse(amount), odds).ToString();
                goto Label_0800;
            Label_077F:
                s = base.count_rsxz_xync(xzh, str10, double.Parse(amount), odds).ToString();
                goto Label_0800;
            Label_079F:
                s = base.count_sqz_xync(xzh, str10, double.Parse(amount), odds).ToString();
                goto Label_0800;
            Label_07BF:
                s = base.count_sqz4_xync(xzh, str10, double.Parse(amount), odds).ToString();
                goto Label_0800;
            Label_07DF:
                s = base.count_wqz5_xync(xzh, str10, double.Parse(amount), odds).ToString();
            Label_0800:
                num5 = 7;
                if (lottery_type.Equals(num5.ToString()))
                {
                    str25 = play_id;
                    if ((str25 != null) && (str25 == "71014"))
                    {
                        s = base.count_tmbs(xzh, zqh, double.Parse(amount), odds, "").ToString();
                    }
                }
                num5 = 14;
                if (lottery_type.Equals(num5.ToString()))
                {
                    switch (play_id)
                    {
                        case "72":
                            s = base.count_rqz_jssfc(xzh, str10, double.Parse(amount), odds).ToString();
                            break;

                        case "74":
                            s = base.count_rlz_jssfc(xzh, str10, double.Parse(amount), odds).ToString();
                            break;

                        case "73":
                            s = base.count_rsxz_jssfc(xzh, str10, double.Parse(amount), odds).ToString();
                            break;

                        case "75":
                            s = base.count_sqz_jssfc(xzh, str10, double.Parse(amount), odds).ToString();
                            break;

                        case "77":
                            s = base.count_sqz_jssfc(xzh, str12, double.Parse(amount), odds).ToString();
                            break;

                        case "76":
                            s = base.count_rsxz_jssfc(xzh, str12, double.Parse(amount), odds).ToString();
                            break;

                        case "78":
                            s = base.count_sqz4_jssfc(xzh, str10, double.Parse(amount), odds).ToString();
                            break;

                        case "79":
                            s = base.count_wqz5_jssfc(xzh, str10, double.Parse(amount), odds).ToString();
                            break;
                    }
                }
                if (double.Parse(s) > 0.0)
                {
                    num += double.Parse(s);
                    num2++;
                    str16 = str16 + "<tr class='tdbg' onMouseOut=\"this.style.backgroundColor=''\" onMouseOver=\"this.style.backgroundColor='#FFFFA2'\">";
                }
                else
                {
                    str16 = str16 + "<tr onMouseOut=\"this.style.backgroundColor=''\" onMouseOver=\"this.style.backgroundColor='#FFFFA2'\">";
                }
                string str19 = string.Format("<span  class='blue'>{0}</span>@<b class='orange'>{1}</b>", play_name, odds);
                if (odds_id.Equals("330") || odds_id.Equals("1200"))
                {
                    goto Label_0BCC;
                }
                if (!odds_id.Equals("72055"))
                {
                    goto Label_0BC0;
                }
                xzh = this.px_pcdd(xzh);
                string str20 = "";
                int index = 0;
                goto Label_0B93;
            Label_0B18:
                if (index == 0)
                {
                    str20 = str20 + int.Parse(xzh.Split(new char[] { ',' })[index]);
                }
                else
                {
                    str20 = str20 + "," + int.Parse(xzh.Split(new char[] { ',' })[index]);
                }
                index++;
            Label_0B93:;
                if (index < xzh.Split(new char[] { ',' }).Length)
                {
                    goto Label_0B18;
                }
                xzh = str20;
                goto Label_0BCC;
            Label_0BC0:
                xzh = this.px(xzh);
            Label_0BCC:;
                str16 = str16 + string.Format("<td>{0}</td><td>{1}</td><td>{2}</td><td align='right'>{3}</td><td align='right'>{4}</td>", new object[] { i + 1, (double.Parse(s) > 0.0) ? ("<span style='font-weight:bold'>" + xzh.Replace(',', '、') + "</span>") : xzh.Replace(',', '、'), str19, string.Format("{0:F1}", double.Parse(amount)), string.Format("{0:F1}", double.Parse(s)) }) + "</tr>";
            }
            string str21 = string.Format("{0:F1}", double.Parse(amount) * strArray.Length);
            string str22 = string.Format("{0:F1}", num - (double.Parse(amount) * (strArray.Length - num2)));
            string str23 = string.Format("{0:F1}", drawback);
            return ((str16 + "<tr  style='font-weight:bold'>") + string.Format("<th>{0}</th><th>{1}</th><th>{2}</th><th align='right'>{3}&nbsp;</th><th align='right'>{4}&nbsp;</th>", new object[] { "合計：", "共" + strArray.Length + "組", "", str21, str22 }) + "</tr>");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string str = HttpContext.Current.Session["user_name"].ToString();
            string str2 = LSRequest.qq("ispay");
            string str3 = LSRequest.qq("orderid");
            DataTable betByOrdernum = CallBLL.cz_bet_kc_bll.GetBetByOrdernum(str3, str2);
            if (betByOrdernum != null)
            {
                string str4 = betByOrdernum.Rows[0]["play_id"].ToString();
                string str5 = betByOrdernum.Rows[0]["odds_id"].ToString();
                string str6 = betByOrdernum.Rows[0]["play_name"].ToString();
                string str7 = betByOrdernum.Rows[0]["bet_group"].ToString();
                string odds = betByOrdernum.Rows[0]["odds"].ToString();
                string str9 = betByOrdernum.Rows[0]["phase_id"].ToString();
                this.is_payment = betByOrdernum.Rows[0]["is_payment"].ToString();
                string str10 = betByOrdernum.Rows[0]["lottery_type"].ToString();
                string s = betByOrdernum.Rows[0]["amount"].ToString();
                string str12 = betByOrdernum.Rows[0]["unit_cnt"].ToString();
                s = (double.Parse(s) / double.Parse(str12)).ToString();
                double drawback = 0.0;
                string str13 = base.GetUserModelInfo.get_su_type();
                if (str13 != null)
                {
                    if (!(str13 == "dl"))
                    {
                        if (str13 == "zd")
                        {
                            drawback = double.Parse(betByOrdernum.Rows[0]["dl_drawback"].ToString());
                        }
                        else if (str13 == "gd")
                        {
                            drawback = double.Parse(betByOrdernum.Rows[0]["zd_drawback"].ToString());
                        }
                        else if (str13 == "fgs")
                        {
                            drawback = double.Parse(betByOrdernum.Rows[0]["gd_drawback"].ToString());
                        }
                    }
                    else
                    {
                        drawback = double.Parse(betByOrdernum.Rows[0]["hy_drawback"].ToString());
                    }
                }
                drawback = double.Parse(betByOrdernum.Rows[0]["amount"].ToString()) * (drawback / 100.0);
                if (this.is_payment.Equals("1"))
                {
                    this.groupString = this.GetBetGroupStringPaymentTrue(str9, str4, str5, str6, str7, odds, s, str10, drawback);
                }
                else
                {
                    this.groupString = this.GetBetGroupStringPaymentFalse(str4, str5, str6, str7, odds, s);
                }
            }
        }

        public string px(string p_str)
        {
            string[] array = p_str.Split(new char[] { ',' });
            Array.Sort<string>(array);
            return string.Join(",", array);
        }

        private string px_pcdd(string str)
        {
            int num;
            string str2 = "";
            string[] strArray = str.Split(new char[] { ',' });
            int[] array = new int[strArray.Length];
            for (num = 0; num < strArray.Length; num++)
            {
                array[num] = Convert.ToInt32(strArray[num]);
            }
            Array.Sort<int>(array);
            for (num = 0; num < array.Length; num++)
            {
                str2 = str2 + array[num] + ",";
            }
            return str2.Substring(0, str2.Length - 1);
        }
    }
}

