namespace User.Web.OldWeb.ReportBill
{
    using LotterySystem.Common;
    using LotterySystem.Model;
    using System;
    using System.Data;
    using System.Linq;
    using System.Web;
    using User.Web.WebBase;

    public class GroupShow_six : MemberPageBase
    {
        protected string groupString = "";
        protected string is_payment = "0";
        protected bool isDoubleOdds = false;
        protected string masterId = 1.ToString();

        public string GetBetGroupStringPaymentFalse(string play_id, string odds_id, string play_name, string bet_group, string bet_wt, string odds, string amount)
        {
            string str = "";
            string[] strArray = bet_group.Split(new char[] { '~' });
            string[] strArray2 = bet_wt.Split(new char[] { '~' });
            string[] strArray3 = odds.Split(new char[] { ',' });
            string str2 = play_name;
            string str3 = "";
            switch (play_id)
            {
                case "91017":
                    str3 = "中三";
                    this.isDoubleOdds = true;
                    break;

                case "91019":
                    str3 = "中二";
                    this.isDoubleOdds = true;
                    break;

                case "91031":
                case "91032":
                case "91033":
                case "91058":
                    str3 = "連" + base.get_YearLian();
                    this.isDoubleOdds = false;
                    break;

                case "91034":
                case "91035":
                case "91036":
                case "91059":
                    str3 = "連0";
                    this.isDoubleOdds = false;
                    break;
            }
            double num = 0.0;
            double num2 = 0.0;
            for (int i = 0; i < strArray.Length; i++)
            {
                string str8;
                string str9;
                string str10;
                string str11;
                string str12;
                double num5;
                str = str + "<tr onMouseOut=\"this.style.backgroundColor=''\" onMouseOver=\"this.style.backgroundColor='#FFFFA2'\">";
                string str4 = strArray[i];
                string str5 = strArray2[i];
                string s = "0";
                string str7 = "0";
                if (((play_id.Equals("91031") || play_id.Equals("91032")) || play_id.Equals("91033")) || play_id.Equals("91058"))
                {
                    str8 = "";
                    str9 = "";
                    if (string.IsNullOrEmpty(str5))
                    {
                        str8 = strArray3[0];
                        str9 = strArray3[1];
                        num5 = double.Parse(str8) * double.Parse(amount);
                        s = num5.ToString();
                    }
                    else if (str5.Split(new char[] { '|' })[0].Equals(base.get_YearLianID()))
                    {
                        str8 = strArray3[0];
                        str9 = strArray3[1];
                        num5 = double.Parse(str9) * double.Parse(amount);
                        s = num5.ToString();
                    }
                    else
                    {
                        str8 = str5.Split(new char[] { '|' })[1];
                        str9 = strArray3[1];
                        num5 = double.Parse(str8) * double.Parse(amount);
                        s = num5.ToString();
                    }
                    num5 = double.Parse(s) - double.Parse(amount);
                    s = num5.ToString();
                    str10 = FunctionSix.GetSXLWT(odds_id, odds, str5, base.get_YearLianID());
                    str11 = string.Format("{0:F1}", double.Parse(amount));
                    str12 = string.Format("{0:F1}", double.Parse(s));
                    num += double.Parse(str12);
                    str5 = string.Format("<span  class='blue'>{0}</span>@<b class='orange'>{1}</b><br /><span  class='blue'>{2}</span>@<b class='orange'>{3}</b>", new object[] { str2, str8, str3, str9 });
                    str = str + string.Format("<td>{0}</td><td>{1}</td><td>{2}</td><td align='right'>{3}</td><td align='right'>{4}</td>", new object[] { i + 1, FunctionSix.GetSXLNameBySXid(this.px(str4)) + str10, str5, str11, str12 });
                }
                else if (((play_id.Equals("91034") || play_id.Equals("91035")) || play_id.Equals("91036")) || play_id.Equals("91059"))
                {
                    str8 = "";
                    str9 = "";
                    if (string.IsNullOrEmpty(str5))
                    {
                        str8 = strArray3[0];
                        str9 = strArray3[1];
                        num5 = double.Parse(str8) * double.Parse(amount);
                        s = num5.ToString();
                    }
                    else if (str5.Split(new char[] { '|' })[0].Equals("10"))
                    {
                        str8 = strArray3[0];
                        str9 = strArray3[1];
                        num5 = double.Parse(str9) * double.Parse(amount);
                        s = num5.ToString();
                    }
                    else
                    {
                        str8 = str5.Split(new char[] { '|' })[1];
                        str9 = strArray3[1];
                        num5 = double.Parse(str8) * double.Parse(amount);
                        s = num5.ToString();
                    }
                    num5 = double.Parse(s) - double.Parse(amount);
                    s = num5.ToString();
                    str10 = FunctionSix.GetWSLWT(odds_id, odds, str5);
                    str11 = string.Format("{0:F1}", double.Parse(amount));
                    str12 = string.Format("{0:F1}", double.Parse(s));
                    num += double.Parse(str12);
                    str5 = string.Format("<span  class='blue'>{0}</span>@<b class='orange'>{1}</b><br /><span  class='blue'>{2}</span>@<b class='orange'>{3}</b>", new object[] { str2, str8, str3, str9 });
                    str = str + string.Format("<td>{0}</td><td>{1}</td><td>{2}</td><td align='right'>{3}</td><td align='right'>{4}</td>", new object[] { i + 1, FunctionSix.GetWSLNameBySXid(this.px(str4)) + str10, str5, str11, str12 });
                }
                else
                {
                    str10 = FunctionSix.GetLMWT(odds_id, odds, str5);
                    if (string.IsNullOrEmpty(str5))
                    {
                        str5 = odds;
                    }
                    else
                    {
                        str5 = str5.Split(new char[] { '|' })[1];
                    }
                    num5 = double.Parse(str5.Split(new char[] { ',' })[0]) * double.Parse(amount);
                    num5 = double.Parse(num5.ToString()) - double.Parse(amount);
                    s = num5.ToString();
                    if (strArray3.Length > 1)
                    {
                        num5 = double.Parse(str5.Split(new char[] { ',' })[1]) * double.Parse(amount);
                        str7 = (double.Parse(num5.ToString()) - double.Parse(amount)).ToString();
                        str5 = string.Format("<span  class='blue'>{0}</span>@<b class='orange'>{1}</b><br /><span  class='blue'>{2}</span>@<b class='orange'>{3}</b>", new object[] { str2, str5.Split(new char[] { ',' })[0], str3, str5.Split(new char[] { ',' })[1] });
                    }
                    else
                    {
                        str5 = string.Format("<span  class='blue'>{0}</span>@<b class='orange'>{1}</b>", str2, str5.Split(new char[] { ',' })[0]);
                    }
                    str11 = string.Format("{0:F1}", double.Parse(amount));
                    str12 = string.Format("{0:F1}", double.Parse(s));
                    num += double.Parse(str12);
                    str = str + string.Format("<td>{0}</td><td>{1}</td><td>{2}</td><td align='right'>{3}</td><td align='right'>{4}</td>", new object[] { i + 1, this.px(str4).Replace(',', '、') + str10, str5, str11, str12 });
                    if (strArray3.Length > 1)
                    {
                        string str13 = string.Format("{0:F1}", double.Parse(str7));
                        num2 += double.Parse(str13);
                        str = str + string.Format("<td align='right'>{0}&nbsp;</td>", str13);
                    }
                }
                str = str + "</tr>";
            }
            str = str + "<tr  style='font-weight:bold'>" + string.Format("<th>{0}</th><th>{1}</th><th>{2}</th><th align='right'>{3}&nbsp;</th><th align='right'>{4}&nbsp;</th>", new object[] { "合計：", "共 " + strArray.Length + " 組", "", string.Format("{0:F1}", double.Parse(amount) * strArray.Length), string.Format("{0:F1}", num) });
            if (num2 > 0.0)
            {
                str = str + string.Format("<th align='right'>{0}&nbsp;</th>", string.Format("{0:F1}", num2));
            }
            return (str + "</tr>");
        }

        public string GetBetGroupStringPaymentTrue(string phase_id, string play_id, string odds_id, string play_name, string bet_group, string bet_wt, string odds, string amount, double drawback)
        {
            cz_phase_six phaseModel = CallBLL.cz_phase_six_bll.GetPhaseModel(int.Parse(phase_id));
            string str = phaseModel.get_n1();
            string str2 = phaseModel.get_n2();
            string str3 = phaseModel.get_n3();
            string str4 = phaseModel.get_n4();
            string str5 = phaseModel.get_n5();
            string str6 = phaseModel.get_n6();
            string str7 = phaseModel.get_sn();
            string str8 = str + "," + str2 + "," + str3 + "," + str4 + "," + str5 + "," + str6;
            string zqh = str + "," + str2 + "," + str3 + "," + str4 + "," + str5 + "," + str6 + "," + str7;
            string str10 = str + "," + str2 + "," + str3 + "," + str4 + "," + str5 + "," + str6;
            string str11 = "";
            string str12 = "";
            string[] strArray = zqh.Split(new char[] { ',' });
            string str13 = "";
            foreach (string str14 in strArray)
            {
                str13 = base.GetZodiacNum(str14.Trim()).Trim();
                str12 = str12 + str13 + ",";
                str13 = base.GetZodiacName(str14.Trim()).Trim();
                str11 = str11 + str13 + ",";
            }
            string str15 = "";
            string[] strArray2 = zqh.Split(new char[] { ',' });
            int num = 0;
            string str16 = "";
            foreach (string str17 in strArray2)
            {
                num = int.Parse(str17) % 10;
                if (num == 0)
                {
                    str16 = "10";
                }
                else
                {
                    str16 = "0" + num.ToString();
                }
                str15 = str15 + str16 + ",";
            }
            string str18 = "";
            string[] strArray3 = bet_group.Split(new char[] { '~' });
            string[] strArray4 = bet_wt.Split(new char[] { '~' });
            string[] strArray5 = odds.Split(new char[] { ',' });
            string str19 = play_name;
            string str20 = "";
            switch (play_id)
            {
                case "91017":
                    str20 = "中三";
                    this.isDoubleOdds = true;
                    break;

                case "91019":
                    str20 = "中二";
                    this.isDoubleOdds = true;
                    break;

                case "91031":
                case "91032":
                case "91033":
                case "91058":
                    str20 = "連" + base.get_YearLian();
                    this.isDoubleOdds = false;
                    break;

                case "91034":
                case "91035":
                case "91036":
                case "91059":
                    str20 = "連0";
                    this.isDoubleOdds = false;
                    break;
            }
            this.isDoubleOdds = false;
            double num2 = 0.0;
            double num3 = 0.0;
            for (int i = 0; i < strArray3.Length; i++)
            {
                string str25;
                string str26;
                int num5;
                string str27;
                string str28;
                string str29;
                string str30;
                string str31;
                string xzh = strArray3[i];
                string str22 = strArray4[i];
                string s = "0";
                if (((play_id.Equals("91031") || play_id.Equals("91032")) || play_id.Equals("91033")) || play_id.Equals("91058"))
                {
                    str25 = "";
                    str26 = "";
                    num5 = 1;
                    if (string.IsNullOrEmpty(str22))
                    {
                        str25 = strArray5[0];
                        str26 = strArray5[1];
                        num5 = 1;
                    }
                    else if (str22.Split(new char[] { '|' })[0].Equals(base.get_YearLianID()))
                    {
                        str25 = strArray5[0];
                        str26 = strArray5[1];
                        num5 = 2;
                    }
                    else
                    {
                        str25 = str22.Split(new char[] { '|' })[1];
                        str26 = strArray5[1];
                        num5 = 1;
                    }
                    s = base.count_sxl(xzh, str12, double.Parse(amount), odds, str22).ToString();
                    if (double.Parse(s) > 0.0)
                    {
                        num2 += double.Parse(s);
                        num3++;
                        str18 = str18 + "<tr  class='tdbg' onMouseOut=\"this.style.backgroundColor=''\" onMouseOver=\"this.style.backgroundColor='#FFFFA2'\">";
                    }
                    else
                    {
                        str18 = str18 + "<tr onMouseOut=\"this.style.backgroundColor=''\" onMouseOver=\"this.style.backgroundColor='#FFFFA2'\">";
                    }
                    str27 = FunctionSix.GetSXLWT(odds_id, odds, str22, base.get_YearLianID());
                    str28 = (num5.Equals(1) && (double.Parse(s) > 0.0)) ? ("<span>▶ </span>" + str19) : str19;
                    str29 = (num5.Equals(2) && (double.Parse(s) > 0.0)) ? ("<span>▶ </span>" + str20) : str20;
                    str22 = string.Format("<span  class='blue'>{0}</span>@<b class='orange'>{1}</b><br /><span  class='blue'>{2}</span>@<b class='orange'>{3}</b>", new object[] { str28, str25, str29, str26 });
                    str30 = string.Format("{0:F1}", double.Parse(amount));
                    str31 = string.Format("{0:F1}", double.Parse(s));
                    str18 = str18 + string.Format("<td>{0}</td><td>{1}</td><td>{2}</td><td align='right'>{3}</td><td align='right'>{4}</td>", new object[] { i + 1, ((double.Parse(s) > 0.0) ? ("<span style='font-weight:bold'>" + FunctionSix.GetSXLNameBySXid(this.px(xzh)) + "</span>") : FunctionSix.GetSXLNameBySXid(this.px(xzh))) + str27, str22, str30, str31 });
                }
                else if (((play_id.Equals("91034") || play_id.Equals("91035")) || play_id.Equals("91036")) || play_id.Equals("91059"))
                {
                    str25 = "";
                    str26 = "";
                    num5 = 1;
                    if (string.IsNullOrEmpty(str22))
                    {
                        str25 = strArray5[0];
                        str26 = strArray5[1];
                        num5 = 1;
                    }
                    else if (str22.Split(new char[] { '|' })[0].Equals("10"))
                    {
                        str25 = strArray5[0];
                        str26 = strArray5[1];
                        num5 = 2;
                    }
                    else
                    {
                        str25 = str22.Split(new char[] { '|' })[1];
                        str26 = strArray5[1];
                        num5 = 1;
                    }
                    s = base.count_wsl(xzh, str15, double.Parse(amount), odds, str22).ToString();
                    if (double.Parse(s) > 0.0)
                    {
                        num2 += double.Parse(s);
                        num3++;
                        str18 = str18 + "<tr  class='tdbg' onMouseOut=\"this.style.backgroundColor=''\" onMouseOver=\"this.style.backgroundColor='#FFFFA2'\">";
                    }
                    else
                    {
                        str18 = str18 + "<tr onMouseOut=\"this.style.backgroundColor=''\" onMouseOver=\"this.style.backgroundColor='#FFFFA2'\">";
                    }
                    str30 = string.Format("{0:F1}", double.Parse(amount));
                    str31 = string.Format("{0:F1}", double.Parse(s));
                    str28 = (num5.Equals(1) && (double.Parse(s) > 0.0)) ? ("<span>▶ </span>" + str19) : str19;
                    str29 = (num5.Equals(2) && (double.Parse(s) > 0.0)) ? ("<span>▶ </span>" + str20) : str20;
                    str27 = FunctionSix.GetWSLWT(odds_id, odds, str22);
                    str22 = string.Format("<span  class='blue'>{0}</span>@<b class='orange'>{1}</b><br /><span  class='blue'>{2}</span>@<b class='orange'>{3}</b>", new object[] { str28, str25, str29, str26 });
                    str18 = str18 + string.Format("<td>{0}</td><td>{1}</td><td>{2}</td><td align='right'>{3}</td><td align='right'>{4}</td>", new object[] { i + 1, ((double.Parse(s) > 0.0) ? ("<span style='font-weight:bold'>" + FunctionSix.GetWSLNameBySXid(this.px(xzh)) + "</span>") : FunctionSix.GetWSLNameBySXid(this.px(xzh))) + str27, str22, str30, str31 });
                }
                else
                {
                    num5 = 1;
                    str27 = FunctionSix.GetLMWT(odds_id, odds, str22);
                    if (string.IsNullOrEmpty(str22))
                    {
                        str22 = odds;
                    }
                    else
                    {
                        str22 = str22.Split(new char[] { '|' })[1];
                    }
                    switch (play_id)
                    {
                        case "91016":
                            s = base.count_sqz(xzh, str10, double.Parse(amount), odds, str22).ToString();
                            break;

                        case "91017":
                            s = base.count_sqr(xzh, str10, double.Parse(amount), odds, str22, ref num5).ToString();
                            break;

                        case "91018":
                            s = base.count_rqz(xzh, str10, double.Parse(amount), odds, str22).ToString();
                            break;

                        case "91019":
                            s = base.count_rzt(xzh, str10, str7, double.Parse(amount), odds, str22, ref num5).ToString();
                            break;

                        case "91020":
                            s = base.count_tc(xzh, str10, str7, double.Parse(amount), odds, str22).ToString();
                            break;

                        case "91040":
                            s = base.count_szy(xzh, zqh, double.Parse(amount), odds, str22).ToString();
                            break;

                        case "91037":
                        case "91047":
                        case "91048":
                        case "91049":
                        case "91050":
                        case "91051":
                            s = base.count_wbz(xzh, zqh, double.Parse(amount), odds, str22).ToString();
                            break;
                    }
                    str30 = string.Format("{0:F1}", double.Parse(amount));
                    str31 = string.Format("{0:F1}", double.Parse(s));
                    if (double.Parse(s) > 0.0)
                    {
                        num2 += double.Parse(s);
                        num3++;
                        str18 = str18 + "<tr  class='tdbg' onMouseOut=\"this.style.backgroundColor=''\" onMouseOver=\"this.style.backgroundColor='#FFFFA2'\">";
                    }
                    else
                    {
                        str18 = str18 + "<tr onMouseOut=\"this.style.backgroundColor=''\" onMouseOver=\"this.style.backgroundColor='#FFFFA2'\">";
                    }
                    if (strArray5.Length > 1)
                    {
                        str28 = (num5.Equals(1) && (double.Parse(s) > 0.0)) ? ("<span>▶ </span>" + str19) : str19;
                        str29 = (num5.Equals(2) && (double.Parse(s) > 0.0)) ? ("<span>▶ </span>" + str20) : str20;
                        str22 = string.Format("<span  class='blue'>{0}</span>@<b class='orange'>{1}</b><br /><span  class='blue'>{2}</span>@<b class='orange'>{3}</b>", new object[] { str28, str22.Split(new char[] { ',' })[0], str29, str22.Split(new char[] { ',' })[1] });
                    }
                    else
                    {
                        str22 = string.Format("<span  class='blue'>{0}</span>@<b class='orange'>{1}</b>", str19, str22.Split(new char[] { ',' })[0]);
                    }
                    str18 = str18 + string.Format("<td>{0}</td><td>{1}</td><td>{2}</td><td align='right'>{3}</td><td align='right'>{4}</td>", new object[] { i + 1, ((double.Parse(s) > 0.0) ? ("<span style='font-weight:bold'>" + this.px(xzh).Replace(',', '、') + "</span>") : this.px(xzh).Replace(',', '、')) + str27, str22, str30, str31 });
                }
                str18 = str18 + "</tr>";
            }
            str18 = str18 + "<tr  style='font-weight:bold'>";
            num2 = (num2 - (double.Parse(amount) * strArray3.Length)) + (double.Parse(amount) * num3);
            return (str18 + string.Format("<th>{0}</th><th>{1}</th><th>{2}</th><th align='right'>{3}&nbsp;</th><th align='right'>{4}&nbsp;</th>", new object[] { "合計：", "共 " + strArray3.Length + " 組", "", string.Format("{0:F1}", double.Parse(amount) * strArray3.Length), string.Format("{0:F1}", num2) }) + "</tr>");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string str = HttpContext.Current.Session["user_name"].ToString();
            cz_userinfo_session _session = this.Session[str + "lottery_session_user_info"] as cz_userinfo_session;
            string str2 = LSRequest.qq("ispay");
            string str3 = LSRequest.qq("orderid");
            DataTable betByOrdernum = CallBLL.cz_bet_six_bll.GetBetByOrdernum(str3, str2);
            if (betByOrdernum != null)
            {
                string str4 = betByOrdernum.Rows[0]["play_id"].ToString();
                string str5 = betByOrdernum.Rows[0]["odds_id"].ToString();
                string str6 = betByOrdernum.Rows[0]["play_name"].ToString();
                string str7 = betByOrdernum.Rows[0]["bet_group"].ToString();
                string str8 = betByOrdernum.Rows[0]["bet_wt"].ToString();
                string odds = betByOrdernum.Rows[0]["odds"].ToString();
                string str10 = betByOrdernum.Rows[0]["phase_id"].ToString();
                this.is_payment = betByOrdernum.Rows[0]["is_payment"].ToString();
                string s = betByOrdernum.Rows[0]["amount"].ToString();
                string str12 = betByOrdernum.Rows[0]["unit_cnt"].ToString();
                s = (double.Parse(s) / double.Parse(str12)).ToString();
                double drawback = 0.0;
                string str13 = _session.get_su_type();
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
                    this.groupString = this.GetBetGroupStringPaymentTrue(str10, str4, str5, str6, str7, str8, odds, s, drawback);
                }
                else
                {
                    this.groupString = this.GetBetGroupStringPaymentFalse(str4, str5, str6, str7, str8, odds, s);
                }
            }
        }

        private string px(string p_str)
        {
            string[] array = p_str.Split(new char[] { ',' });
            Array.Sort<string>(array);
            return string.Join(",", array.ToArray<string>());
        }
    }
}

