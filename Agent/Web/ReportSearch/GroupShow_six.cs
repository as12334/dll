namespace Agent.Web.ReportSearch
{
    using Agent.Web.WebBase;
    using LotterySystem.Common;
    using LotterySystem.Model;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;

    public class GroupShow_six : MemberPageBase
    {
        protected string atz = "1";
        protected string groupString = "";
        protected string is_payment = "0";
        protected bool isDoubleOdds;
        protected string masterId = 1.ToString();
        protected string OutWindow = "0";

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
                        s = strArray2[index];
                    }
                    num2 += num4 * double.Parse(s);
                }
                index++;
            }
            return num2;
        }

        protected double count_rzt(string xzh, string zqh_zm, string zqh_tm, double m_money, string pl, string pl_list, ref int plIndex)
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
                        s = strArray2[index].Split(new char[] { ',' })[0].ToString().Trim();
                        plIndex = 1;
                    }
                    num6 += num5 * double.Parse(s);
                }
                else if (num2 == 2)
                {
                    s = strArray3[1].ToString();
                    if ((length > index) && (strArray2[index].ToString().Trim() != ""))
                    {
                        s = strArray2[index].Split(new char[] { ',' })[1].ToString().Trim();
                        plIndex = 2;
                    }
                    num6 += num5 * double.Parse(s);
                }
                index++;
            }
            return num6;
        }

        protected double count_sqr(string xzh, string zqh, double m_money, string pl, string pl_list, ref int plIndex)
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
                        s = strArray2[index].Split(new char[] { ',' })[1].ToString().Trim();
                        plIndex = 2;
                    }
                    num2 += num4 * double.Parse(s);
                }
                else if (source.Count<string>() == 2)
                {
                    s = strArray4[0].ToString();
                    if (strArray2[index].ToString().Trim() != "")
                    {
                        s = strArray2[index].Split(new char[] { ',' })[0].ToString().Trim();
                        plIndex = 1;
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
                        s = strArray2[index].ToString();
                    }
                    num2 += num4 * double.Parse(s);
                }
                index++;
            }
            return num2;
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
                        s = strArray2[index];
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
                        s = strArray2[index];
                    }
                    num4 += num3 * double.Parse(s);
                }
                index++;
            }
            return num4;
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
                        s = strArray2[index];
                    }
                    num4 += num3 * double.Parse(s);
                }
                index++;
            }
            return num4;
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

        public string GetBetGroupStringPaymentFalse(string play_id, string odds_id, string play_name, string bet_group, string bet_wt, string odds, string amount, double rate, string hyAmount)
        {
            string str = "";
            string[] strArray = bet_group.Split(new char[] { '~' });
            string[] strArray2 = bet_wt.Split(new char[] { '~' });
            string[] strArray3 = odds.Split(new char[] { ',' });
            string str2 = play_name;
            string str3 = "";
            double num = 0.0;
            double num2 = 0.0;
            amount = (double.Parse(amount) * rate).ToString();
            switch (play_id)
            {
                case "91017":
                case "91061":
                    str3 = "中三";
                    this.isDoubleOdds = true;
                    break;

                case "91019":
                case "91063":
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
            for (int i = 0; i < strArray.Length; i++)
            {
                str = str + "<tr onMouseOut=\"this.style.backgroundColor=''\" onMouseOver=\"this.style.backgroundColor='#FFFFA2'\">";
                string str4 = strArray[i];
                string str5 = strArray2[i];
                string s = "0";
                string str7 = "0";
                if ((play_id.Equals("91031") || play_id.Equals("91032")) || (play_id.Equals("91033") || play_id.Equals("91058")))
                {
                    string str8 = "";
                    string str9 = "";
                    if (string.IsNullOrEmpty(str5))
                    {
                        str8 = strArray3[0];
                        str9 = strArray3[1];
                        s = ((double.Parse(str8) * double.Parse(amount)) - double.Parse(amount)).ToString();
                    }
                    else if (str5.Split(new char[] { '|' })[0].Equals(base.get_YearLianID()))
                    {
                        str8 = strArray3[0];
                        str9 = strArray3[1];
                        s = ((double.Parse(str9) * double.Parse(amount)) - double.Parse(amount)).ToString();
                    }
                    else
                    {
                        str8 = str5.Split(new char[] { '|' })[1];
                        str9 = strArray3[1];
                        s = ((double.Parse(str8) * double.Parse(amount)) - double.Parse(amount)).ToString();
                    }
                    num += double.Parse(s);
                    string str10 = FunctionSix.GetSXLWT(odds_id, odds, str5, base.get_YearLianID());
                    str5 = string.Format("<span  class='blue'>{0}</span>@<b class='red'>{1}</b><br /><span  class='blue'>{2}</span>@<b class='red'>{3}</b>", new object[] { str2, str8, str3, str9 });
                    str = str + string.Format("<td>{0}</td><td>{1}</td><td>{2}</td><td align='right'>{3}&nbsp;</td><td align='right'>{4}&nbsp;</td><td align='right'>{5}&nbsp;</td>", new object[] { i + 1, FunctionSix.GetSXLNameBySXid(this.px(str4)) + str10, str5, string.Format("{0:F1}", double.Parse(hyAmount)), string.Format("{0:F1}", double.Parse(amount)), "-" + string.Format("{0:F1}", double.Parse(s)) });
                }
                else if ((play_id.Equals("91034") || play_id.Equals("91035")) || (play_id.Equals("91036") || play_id.Equals("91059")))
                {
                    string str11 = "";
                    string str12 = "";
                    if (string.IsNullOrEmpty(str5))
                    {
                        str11 = strArray3[0];
                        str12 = strArray3[1];
                        s = ((double.Parse(str11) * double.Parse(amount)) - double.Parse(amount)).ToString();
                    }
                    else if (str5.Split(new char[] { '|' })[0].Equals("10"))
                    {
                        str11 = strArray3[0];
                        str12 = strArray3[1];
                        s = ((double.Parse(str12) * double.Parse(amount)) - double.Parse(amount)).ToString();
                    }
                    else
                    {
                        str11 = str5.Split(new char[] { '|' })[1];
                        str12 = strArray3[1];
                        s = ((double.Parse(str11) * double.Parse(amount)) - double.Parse(amount)).ToString();
                    }
                    string str13 = FunctionSix.GetWSLWT(odds_id, odds, str5);
                    num += double.Parse(s);
                    str5 = string.Format("<span  class='blue'>{0}</span>@<b class='red'>{1}</b><br /><span  class='blue'>{2}</span>@<b class='red'>{3}</b>", new object[] { str2, str11, str3, str12 });
                    str = str + string.Format("<td>{0}</td><td>{1}</td><td>{2}</td><td align='right'>{3}&nbsp;</td><td align='right'>{4}&nbsp;</td><td align='right'>{5}&nbsp;</td>", new object[] { i + 1, FunctionSix.GetWSLNameBySXid(this.px(str4)) + str13, str5, string.Format("{0:F1}", double.Parse(hyAmount)), string.Format("{0:F1}", double.Parse(amount)), "-" + string.Format("{0:F1}", double.Parse(s)) });
                }
                else
                {
                    string str14 = FunctionSix.GetLMWT(odds_id, odds, str5);
                    string str15 = "";
                    "92638,92639,92640,92641,92642,92643".IndexOf(odds_id);
                    if (string.IsNullOrEmpty(str5))
                    {
                        str5 = odds;
                    }
                    else
                    {
                        str5 = str5.Split(new char[] { '|' })[1];
                    }
                    s = ((double.Parse(str5.Split(new char[] { ',' })[0]) * double.Parse(amount)) - double.Parse(amount)).ToString();
                    num += double.Parse(s);
                    if (strArray3.Length > 1)
                    {
                        str7 = ((double.Parse(str5.Split(new char[] { ',' })[1]) * double.Parse(amount)) - double.Parse(amount)).ToString();
                        num2 += double.Parse(str7);
                        str5 = string.Format(str15 + "<span  class='blue'>{0}</span>@<b class='red'>{1}</b><br /><span  class='blue'>{2}</span>@<b class='red'>{3}</b>", new object[] { str2, str5.Split(new char[] { ',' })[0], str3, str5.Split(new char[] { ',' })[1] });
                    }
                    else
                    {
                        str5 = string.Format(str15 + "<span  class='blue'>{0}</span>@<b class='red'>{1}</b>", str2, str5.Split(new char[] { ',' })[0]);
                    }
                    str = str + string.Format("<td>{0}</td><td>{1}</td><td>{2}</td><td align='right'>{3}&nbsp;</td><td align='right'>{4}&nbsp;</td><td align='right'>{5}&nbsp;</td>", new object[] { i + 1, this.px(str4).Replace(',', '、') + str14, str5, string.Format("{0:F1}", double.Parse(hyAmount)), string.Format("{0:F1}", double.Parse(amount)), "-" + string.Format("{0:F1}", double.Parse(s)) });
                    if (strArray3.Length > 1)
                    {
                        str = str + string.Format("<td align='right'>{0}&nbsp;</td>", "-" + string.Format("{0:F1}", double.Parse(str7)));
                    }
                }
                str = str + "</tr>";
            }
            str = str + "<tr  style='font-weight:bold'>" + string.Format("<th>{0}</th><th>{1}</th><th>{2}</th><th align='right'>{3}&nbsp;</th><th align='right'>{4}&nbsp;</th><th align='right'>{5}&nbsp;</th>", new object[] { "合計", "共 " + strArray.Length + " 組", "", string.Format("{0:F1}", double.Parse(hyAmount) * strArray.Length), string.Format("{0:F1}", double.Parse(amount) * strArray.Length), "-" + string.Format("{0:F1}", num) });
            if (this.isDoubleOdds)
            {
                str = str + string.Format("<th align='right'>{0}&nbsp;</th>", "-" + string.Format("{0:F1}", num2));
            }
            return (str + "</tr>");
        }

        public string GetBetGroupStringPaymentTrue(string phase_id, string play_id, string odds_id, string play_name, string bet_group, string bet_wt, string odds, string amount, double rate, double drawback, string hyAmount)
        {
            cz_phase_six phaseModel = CallBLL.cz_phase_six_bll.GetPhaseModel(int.Parse(phase_id));
            string str = phaseModel.get_n1();
            string str2 = phaseModel.get_n2();
            string str3 = phaseModel.get_n3();
            string str4 = phaseModel.get_n4();
            string str5 = phaseModel.get_n5();
            string str6 = phaseModel.get_n6();
            string str7 = phaseModel.get_sn();
            string text1 = str + "," + str2 + "," + str3 + "," + str4 + "," + str5 + "," + str6;
            string zqh = str + "," + str2 + "," + str3 + "," + str4 + "," + str5 + "," + str6 + "," + str7;
            string str9 = str + "," + str2 + "," + str3 + "," + str4 + "," + str5 + "," + str6;
            string str10 = "";
            string str11 = "";
            string[] strArray = zqh.Split(new char[] { ',' });
            string str12 = "";
            foreach (string str13 in strArray)
            {
                str12 = base.GetZodiacNum(str13.Trim()).Trim();
                str11 = str11 + str12 + ",";
                str12 = base.GetZodiacName(str13.Trim()).Trim();
                str10 = str10 + str12 + ",";
            }
            string str14 = "";
            string[] strArray2 = zqh.Split(new char[] { ',' });
            int num = 0;
            string str15 = "";
            foreach (string str16 in strArray2)
            {
                num = int.Parse(str16) % 10;
                if (num == 0)
                {
                    str15 = "10";
                }
                else
                {
                    str15 = "0" + num.ToString();
                }
                str14 = str14 + str15 + ",";
            }
            string str17 = "";
            string[] strArray3 = bet_group.Split(new char[] { '~' });
            string[] strArray4 = bet_wt.Split(new char[] { '~' });
            string[] strArray5 = odds.Split(new char[] { ',' });
            string str18 = play_name;
            string str19 = "";
            switch (play_id)
            {
                case "91017":
                case "91061":
                    str19 = "中三";
                    this.isDoubleOdds = true;
                    break;

                case "91019":
                case "91063":
                    str19 = "中二";
                    this.isDoubleOdds = true;
                    break;

                case "91031":
                case "91032":
                case "91033":
                case "91058":
                    str19 = "連" + base.get_YearLian();
                    this.isDoubleOdds = false;
                    break;

                case "91034":
                case "91035":
                case "91036":
                case "91059":
                    str19 = "連0";
                    this.isDoubleOdds = false;
                    break;
            }
            this.isDoubleOdds = false;
            double num2 = 0.0;
            double num3 = 0.0;
            amount = (double.Parse(amount) * rate).ToString();
            for (int i = 0; i < strArray3.Length; i++)
            {
                string xzh = strArray3[i];
                string str21 = strArray4[i];
                string s = "0";
                if ((play_id.Equals("91031") || play_id.Equals("91032")) || (play_id.Equals("91033") || play_id.Equals("91058")))
                {
                    string str23 = "";
                    string str24 = "";
                    int num5 = 1;
                    if (string.IsNullOrEmpty(str21))
                    {
                        str23 = strArray5[0];
                        str24 = strArray5[1];
                        num5 = 1;
                    }
                    else if (str21.Split(new char[] { '|' })[0].Equals(base.get_YearLianID()))
                    {
                        str23 = strArray5[0];
                        str24 = strArray5[1];
                        num5 = 2;
                    }
                    else
                    {
                        str23 = str21.Split(new char[] { '|' })[1];
                        str24 = strArray5[1];
                        num5 = 1;
                    }
                    s = this.count_sxl(xzh, str11, double.Parse(amount), odds, str21).ToString();
                    if (double.Parse(s) > 0.0)
                    {
                        num2 += double.Parse(s);
                        num3++;
                        str17 = str17 + "<tr  class='tdbg' onMouseOut=\"this.style.backgroundColor=''\" onMouseOver=\"this.style.backgroundColor='#FFFFA2'\">";
                    }
                    else
                    {
                        str17 = str17 + "<tr onMouseOut=\"this.style.backgroundColor=''\" onMouseOver=\"this.style.backgroundColor='#FFFFA2'\">";
                    }
                    string str25 = FunctionSix.GetSXLWT(odds_id, odds, str21, base.get_YearLianID());
                    string str26 = (num5.Equals(1) && (double.Parse(s) > 0.0)) ? ("<span>▶ </span>" + str18) : str18;
                    string str27 = (num5.Equals(2) && (double.Parse(s) > 0.0)) ? ("<span>▶ </span>" + str19) : str19;
                    str21 = string.Format("<span  class='blue'>{0}</span>@<b class='red'>{1}</b><br /><span  class='blue'>{2}</span>@<b class='red'>{3}</b>", new object[] { str26, str23, str27, str24 });
                    string str28 = string.Format("{0:F1}", double.Parse(amount));
                    string str29 = string.Format("{0:F1}", double.Parse(s));
                    string str30 = "";
                    if (double.Parse(s) > 0.0)
                    {
                        str30 = " class='winner'";
                    }
                    str17 = str17 + string.Format("<td>{0}</td><td>{1}</td><td>{2}</td><td align='right'>{3}&nbsp;</td><td align='right'>{4}&nbsp;</td><td align='right' " + str30 + ">{5}&nbsp;</td>", new object[] { i + 1, ((double.Parse(s) > 0.0) ? ("<span style='font-weight:bold'>" + FunctionSix.GetSXLNameBySXid(this.px(xzh)) + "</span>") : FunctionSix.GetSXLNameBySXid(this.px(xzh))) + str25, str21, string.Format("{0:F1}", double.Parse(hyAmount)), str28, str29 });
                }
                else if ((play_id.Equals("91034") || play_id.Equals("91035")) || (play_id.Equals("91036") || play_id.Equals("91059")))
                {
                    string str31 = "";
                    string str32 = "";
                    int num6 = 1;
                    if (string.IsNullOrEmpty(str21))
                    {
                        str31 = strArray5[0];
                        str32 = strArray5[1];
                        num6 = 1;
                    }
                    else if (str21.Split(new char[] { '|' })[0].Equals("10"))
                    {
                        str31 = strArray5[0];
                        str32 = strArray5[1];
                        num6 = 2;
                    }
                    else
                    {
                        str31 = str21.Split(new char[] { '|' })[1];
                        str32 = strArray5[1];
                        num6 = 1;
                    }
                    s = this.count_wsl(xzh, str14, double.Parse(amount), odds, str21).ToString();
                    if (double.Parse(s) > 0.0)
                    {
                        num2 += double.Parse(s);
                        num3++;
                        str17 = str17 + "<tr  class='tdbg' onMouseOut=\"this.style.backgroundColor=''\" onMouseOver=\"this.style.backgroundColor='#FFFFA2'\">";
                    }
                    else
                    {
                        str17 = str17 + "<tr onMouseOut=\"this.style.backgroundColor=''\" onMouseOver=\"this.style.backgroundColor='#FFFFA2'\">";
                    }
                    string str33 = string.Format("{0:F1}", double.Parse(amount));
                    string str34 = string.Format("{0:F1}", double.Parse(s));
                    string str35 = (num6.Equals(1) && (double.Parse(s) > 0.0)) ? ("<span>▶ </span>" + str18) : str18;
                    string str36 = (num6.Equals(2) && (double.Parse(s) > 0.0)) ? ("<span>▶ </span>" + str19) : str19;
                    string str37 = FunctionSix.GetWSLWT(odds_id, odds, str21);
                    str21 = string.Format("<span  class='blue'>{0}</span>@<b class='red'>{1}</b><br /><span  class='blue'>{2}</span>@<b class='red'>{3}</b>", new object[] { str35, str31, str36, str32 });
                    string str38 = "";
                    if (double.Parse(s) > 0.0)
                    {
                        str38 = " class='winner'";
                    }
                    str17 = str17 + string.Format("<td>{0}</td><td>{1}</td><td>{2}</td><td align='right'>{3}&nbsp;</td><td align='right'>{4}&nbsp;</td><td align='right' " + str38 + ">{5}&nbsp;</td>", new object[] { i + 1, ((double.Parse(s) > 0.0) ? ("<span style='font-weight:bold'>" + FunctionSix.GetWSLNameBySXid(this.px(xzh)) + "</span>") : FunctionSix.GetWSLNameBySXid(this.px(xzh))) + str37, str21, string.Format("{0:F1}", double.Parse(hyAmount)), str33, str34 });
                }
                else
                {
                    int plIndex = 1;
                    string str39 = FunctionSix.GetLMWT(odds_id, odds, str21);
                    if (string.IsNullOrEmpty(str21))
                    {
                        str21 = odds;
                    }
                    else
                    {
                        str21 = str21.Split(new char[] { '|' })[1];
                    }
                    switch (play_id)
                    {
                        case "91016":
                        case "91060":
                            s = this.count_sqz(xzh, str9, double.Parse(amount), odds, str21).ToString();
                            break;

                        case "91017":
                        case "91061":
                            s = this.count_sqr(xzh, str9, double.Parse(amount), odds, str21, ref plIndex).ToString();
                            break;

                        case "91018":
                        case "91062":
                            s = this.count_rqz(xzh, str9, double.Parse(amount), odds, str21).ToString();
                            break;

                        case "91019":
                        case "91063":
                            s = this.count_rzt(xzh, str9, str7, double.Parse(amount), odds, str21, ref plIndex).ToString();
                            break;

                        case "91020":
                        case "91064":
                            s = this.count_tc(xzh, str9, str7, double.Parse(amount), odds, str21).ToString();
                            break;

                        case "91040":
                        case "91065":
                            s = this.count_szy(xzh, zqh, double.Parse(amount), odds, str21).ToString();
                            break;

                        case "91037":
                        case "91047":
                        case "91048":
                        case "91049":
                        case "91050":
                        case "91051":
                            s = this.count_wbz(xzh, zqh, double.Parse(amount), odds, str21).ToString();
                            break;
                    }
                    string str40 = string.Format("{0:F1}", double.Parse(amount));
                    string str41 = string.Format("{0:F1}", double.Parse(s));
                    string str42 = "";
                    "92638,92639,92640,92641,92642,92643".IndexOf(odds_id);
                    if (double.Parse(s) > 0.0)
                    {
                        num2 += double.Parse(s);
                        num3++;
                        str17 = str17 + "<tr  class='tdbg' onMouseOut=\"this.style.backgroundColor=''\" onMouseOver=\"this.style.backgroundColor='#FFFFA2'\">";
                    }
                    else
                    {
                        str17 = str17 + "<tr onMouseOut=\"this.style.backgroundColor=''\" onMouseOver=\"this.style.backgroundColor='#FFFFA2'\">";
                    }
                    if (strArray5.Length > 1)
                    {
                        string str43 = (plIndex.Equals(1) && (double.Parse(s) > 0.0)) ? ("<span>▶ </span>" + str18) : str18;
                        string str44 = (plIndex.Equals(2) && (double.Parse(s) > 0.0)) ? ("<span>▶ </span>" + str19) : str19;
                        str21 = string.Format(str42 + "<span  class='blue'>{0}</span>@<b class='red'>{1}</b><br /><span  class='blue'>{2}</span>@<b class='red'>{3}</b>", new object[] { str43, str21.Split(new char[] { ',' })[0], str44, str21.Split(new char[] { ',' })[1] });
                    }
                    else
                    {
                        str21 = string.Format(str42 + "<span  class='blue'>{0}</span>@<b class='red'>{1}</b>", str18, str21.Split(new char[] { ',' })[0]);
                    }
                    string str45 = "";
                    if (double.Parse(s) > 0.0)
                    {
                        str45 = " class='winner'";
                    }
                    str17 = str17 + string.Format("<td>{0}</td><td>{1}</td><td>{2}</td><td align='right'>{3}&nbsp;</td><td align='right'>{4}&nbsp;</td><td align='right' " + str45 + ">{5}&nbsp;</td>", new object[] { i + 1, ((double.Parse(s) > 0.0) ? ("<span style='font-weight:bold'>" + this.px(xzh).Replace(',', '、') + "</span>") : this.px(xzh).Replace(',', '、')) + str39, str21, string.Format("{0:F1}", double.Parse(hyAmount)), str40, str41 });
                }
                str17 = str17 + "</tr>";
            }
            str17 = str17 + "<tr  style='font-weight:bold'>";
            num2 = (num2 - (double.Parse(amount) * strArray3.Length)) + (double.Parse(amount) * num3);
            double.Parse(amount);
            double num1 = drawback / 100.0;
            return (str17 + string.Format("<th>{0}</th><th>{1}</th><th>{2}</th><th align='right'>{3}&nbsp;</th><th align='right'>{4}&nbsp;</th><th align='right'>{5}&nbsp;</th>", new object[] { "合計：", "共 " + strArray3.Length + " 組", "", string.Format("{0:F1}", double.Parse(hyAmount) * strArray3.Length), string.Format("{0:F1}", double.Parse(amount) * strArray3.Length), string.Format("{0:F1}", num2) }) + "</tr>");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            agent_userinfo_session model = this.Session[this.Session["user_name"] + "lottery_session_user_info"] as agent_userinfo_session;
            string str = base.Request.UrlReferrer.LocalPath.ToLower();
            if (model.get_u_type().Equals("zj"))
            {
                if (str.IndexOf("billsearchlist") > -1)
                {
                    base.Permission_Aspx_ZJ(model, "po_1_1");
                }
                if (str.IndexOf("bill_six") > -1)
                {
                    base.Permission_Aspx_ZJ(model, "po_1_1");
                }
                if (str.IndexOf("newbet_six") > -1)
                {
                    base.Permission_Aspx_ZJ(model, "po_3_5");
                }
                if (str.IndexOf("reportdetail_b_six") > -1)
                {
                    base.Permission_Aspx_ZJ(model, "po_4_1");
                }
                if (str.IndexOf("reportdetail_t_six") > -1)
                {
                    base.Permission_Aspx_ZJ(model, "po_4_2");
                }
            }
            else
            {
                if ((str.IndexOf("billsearchlist") > -1) || (str.IndexOf("newbet_six") > -1))
                {
                    base.Response.Redirect("/MessagePage.aspx?code=u100057&url=&issuccess=1&isback=0&isopen=1");
                    base.Response.End();
                }
                if (str.IndexOf("bill_six") > -1)
                {
                    base.Permission_Aspx_DL(model, "po_5_1");
                }
                if (str.IndexOf("reportdetail_b_six") > -1)
                {
                    base.Permission_Aspx_ZJ(model, "po_7_1");
                }
                if (str.IndexOf("reportdetail_t_six") > -1)
                {
                    base.Permission_Aspx_ZJ(model, "po_7_2");
                }
            }
            string str2 = LSRequest.qq("ispay");
            string str3 = LSRequest.qq("orderid");
            this.OutWindow = LSRequest.qq("ow");
            this.atz = LSRequest.qq("atz");
            DataTable betByOrdernum = CallBLL.cz_bet_six_bll.GetBetByOrdernum(str3, str2);
            if (betByOrdernum != null)
            {
                string str4 = betByOrdernum.Rows[0]["play_id"].ToString();
                string str5 = betByOrdernum.Rows[0]["odds_id"].ToString();
                string str6 = betByOrdernum.Rows[0]["play_name"].ToString();
                string str7 = betByOrdernum.Rows[0]["bet_group"].ToString();
                string str8 = betByOrdernum.Rows[0]["bet_wt"].ToString();
                string odds = betByOrdernum.Rows[0]["odds"].ToString();
                if (model.get_u_type().Equals("zj") || model.get_u_type().Equals("fgs"))
                {
                    odds = betByOrdernum.Rows[0]["odds_zj"].ToString();
                    str8 = betByOrdernum.Rows[0]["bet_wt_zj"].ToString();
                }
                double rate = 0.0;
                double drawback = 0.0;
                string str15 = model.get_u_type();
                if (str15 != null)
                {
                    if (!(str15 == "zj"))
                    {
                        if (str15 == "fgs")
                        {
                            rate = double.Parse(betByOrdernum.Rows[0]["fgs_rate"].ToString());
                            drawback = double.Parse(betByOrdernum.Rows[0]["gd_drawback"].ToString());
                        }
                        else if (str15 == "gd")
                        {
                            rate = double.Parse(betByOrdernum.Rows[0]["gd_rate"].ToString());
                            drawback = double.Parse(betByOrdernum.Rows[0]["zd_drawback"].ToString());
                        }
                        else if (str15 == "zd")
                        {
                            rate = double.Parse(betByOrdernum.Rows[0]["zd_rate"].ToString());
                            drawback = double.Parse(betByOrdernum.Rows[0]["dl_drawback"].ToString());
                        }
                        else if (str15 == "dl")
                        {
                            rate = double.Parse(betByOrdernum.Rows[0]["dl_rate"].ToString());
                            drawback = double.Parse(betByOrdernum.Rows[0]["hy_drawback"].ToString());
                        }
                    }
                    else
                    {
                        rate = double.Parse(betByOrdernum.Rows[0]["zj_rate"].ToString());
                        drawback = double.Parse(betByOrdernum.Rows[0]["fgs_drawback"].ToString());
                    }
                }
                rate /= 100.0;
                string str11 = betByOrdernum.Rows[0]["phase_id"].ToString();
                this.is_payment = betByOrdernum.Rows[0]["is_payment"].ToString();
                string s = betByOrdernum.Rows[0]["amount"].ToString();
                string str13 = betByOrdernum.Rows[0]["unit_cnt"].ToString();
                string hyAmount = (double.Parse(s) / double.Parse(str13)).ToString();
                s = (double.Parse(s) / double.Parse(str13)).ToString();
                if (this.is_payment.Equals("1"))
                {
                    this.groupString = this.GetBetGroupStringPaymentTrue(str11, str4, str5, str6, str7, str8, odds, s, rate, drawback, hyAmount);
                }
                else
                {
                    this.groupString = this.GetBetGroupStringPaymentFalse(str4, str5, str6, str7, str8, odds, s, rate, hyAmount);
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

