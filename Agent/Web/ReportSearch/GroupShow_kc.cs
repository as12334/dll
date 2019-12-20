namespace Agent.Web.ReportSearch
{
    using Agent.Web.WebBase;
    using LotterySystem.Common;
    using LotterySystem.Model;
    using System;
    using System.Data;
    using System.Linq;

    public class GroupShow_kc : MemberPageBase
    {
        protected string atz = "1";
        protected string groupString = "";
        protected string is_payment = "0";
        protected string masterId = 2.ToString();
        protected string OutWindow = "0";

        protected double count_rlz_jssfc(string xzh, string zqh, double m_money, string pl)
        {
            string[] strArray = xzh.Split(new char[] { '~' });
            int num = 0;
            foreach (string str in strArray)
            {
                string[] strArray2 = str.Split(new char[] { ',' });
                if (zqh.IndexOf(strArray2[0] + "," + strArray2[1]) > -1)
                {
                    num++;
                }
                else if (zqh.IndexOf(strArray2[1] + "," + strArray2[0]) > -1)
                {
                    num++;
                }
            }
            int length = strArray.Length;
            double num3 = m_money / ((double) length);
            double num4 = 0.0;
            num4 = -m_money;
            return (num4 + ((num3 * num) * double.Parse(pl)));
        }

        protected double count_rlz_kl10(string xzh, string zqh, double m_money, string pl)
        {
            string[] strArray = xzh.Split(new char[] { '~' });
            int num = 0;
            foreach (string str in strArray)
            {
                string[] strArray2 = str.Split(new char[] { ',' });
                if (zqh.IndexOf(strArray2[0] + "," + strArray2[1]) > -1)
                {
                    num++;
                }
                else if (zqh.IndexOf(strArray2[1] + "," + strArray2[0]) > -1)
                {
                    num++;
                }
            }
            int length = strArray.Length;
            double num3 = m_money / ((double) length);
            double num4 = 0.0;
            num4 = -m_money;
            return (num4 + ((num3 * num) * double.Parse(pl)));
        }

        protected double count_rlz_xync(string xzh, string zqh, double m_money, string pl)
        {
            string[] strArray = xzh.Split(new char[] { '~' });
            int num = 0;
            foreach (string str in strArray)
            {
                string[] strArray2 = str.Split(new char[] { ',' });
                if (zqh.IndexOf(strArray2[0] + "," + strArray2[1]) > -1)
                {
                    num++;
                }
                else if (zqh.IndexOf(strArray2[1] + "," + strArray2[0]) > -1)
                {
                    num++;
                }
            }
            int length = strArray.Length;
            double num3 = m_money / ((double) length);
            double num4 = 0.0;
            num4 = -m_money;
            return (num4 + ((num3 * num) * double.Parse(pl)));
        }

        protected double count_rqz_jssfc(string xzh, string zqh, double m_money, string pl)
        {
            string[] strArray = xzh.Split(new char[] { '~' });
            int num = 0;
            int num2 = 0;
            foreach (string str in strArray)
            {
                string[] strArray2 = str.Split(new char[] { ',' });
                num2 = 0;
                foreach (string str2 in strArray2)
                {
                    if (zqh.IndexOf(str2) > -1)
                    {
                        num2++;
                    }
                }
                if (num2 == 2)
                {
                    num++;
                }
            }
            int length = strArray.Length;
            double num4 = m_money / ((double) length);
            double num5 = 0.0;
            num5 = -m_money;
            return (num5 + ((num4 * num) * double.Parse(pl)));
        }

        protected double count_rqz_kl10(string xzh, string zqh, double m_money, string pl)
        {
            string[] strArray = xzh.Split(new char[] { '~' });
            int num = 0;
            int num2 = 0;
            foreach (string str in strArray)
            {
                string[] strArray2 = str.Split(new char[] { ',' });
                num2 = 0;
                foreach (string str2 in strArray2)
                {
                    if (zqh.IndexOf(str2) > -1)
                    {
                        num2++;
                    }
                }
                if (num2 == 2)
                {
                    num++;
                }
            }
            int length = strArray.Length;
            double num4 = m_money / ((double) length);
            double num5 = 0.0;
            num5 = -m_money;
            return (num5 + ((num4 * num) * double.Parse(pl)));
        }

        protected double count_rqz_xync(string xzh, string zqh, double m_money, string pl)
        {
            string[] strArray = xzh.Split(new char[] { '~' });
            int num = 0;
            int num2 = 0;
            foreach (string str in strArray)
            {
                string[] strArray2 = str.Split(new char[] { ',' });
                num2 = 0;
                foreach (string str2 in strArray2)
                {
                    if (zqh.IndexOf(str2) > -1)
                    {
                        num2++;
                    }
                }
                if (num2 == 2)
                {
                    num++;
                }
            }
            int length = strArray.Length;
            double num4 = m_money / ((double) length);
            double num5 = 0.0;
            num5 = -m_money;
            return (num5 + ((num4 * num) * double.Parse(pl)));
        }

        protected double count_rsxz_jssfc(string xzh, string zqh, double m_money, string pl)
        {
            string[] strArray = xzh.Split(new char[] { '~' });
            int num = 0;
            foreach (string str in strArray)
            {
                if (zqh.IndexOf(str) > -1)
                {
                    num++;
                }
            }
            int length = strArray.Length;
            double num3 = m_money / ((double) length);
            double num4 = 0.0;
            num4 = -m_money;
            return (num4 + ((num3 * num) * double.Parse(pl)));
        }

        protected double count_rsxz_kl10(string xzh, string zqh, double m_money, string pl)
        {
            string[] strArray = xzh.Split(new char[] { '~' });
            int num = 0;
            foreach (string str in strArray)
            {
                if (zqh.IndexOf(str) > -1)
                {
                    num++;
                }
            }
            int length = strArray.Length;
            double num3 = m_money / ((double) length);
            double num4 = 0.0;
            num4 = -m_money;
            return (num4 + ((num3 * num) * double.Parse(pl)));
        }

        protected double count_rsxz_xync(string xzh, string zqh, double m_money, string pl)
        {
            string[] strArray = xzh.Split(new char[] { '~' });
            int num = 0;
            foreach (string str in strArray)
            {
                if (zqh.IndexOf(str) > -1)
                {
                    num++;
                }
            }
            int length = strArray.Length;
            double num3 = m_money / ((double) length);
            double num4 = 0.0;
            num4 = -m_money;
            return (num4 + ((num3 * num) * double.Parse(pl)));
        }

        protected double count_sqz_jssfc(string xzh, string zqh, double m_money, string pl)
        {
            string[] strArray = xzh.Split(new char[] { '~' });
            int num = 0;
            int num2 = 0;
            foreach (string str in strArray)
            {
                string[] strArray2 = str.Split(new char[] { ',' });
                num2 = 0;
                foreach (string str2 in strArray2)
                {
                    if (zqh.IndexOf(str2) > -1)
                    {
                        num2++;
                    }
                }
                if (num2 == 3)
                {
                    num++;
                }
            }
            int length = strArray.Length;
            double num4 = m_money / ((double) length);
            double num5 = 0.0;
            num5 = -m_money;
            return (num5 + ((num4 * num) * double.Parse(pl)));
        }

        protected double count_sqz_kl10(string xzh, string zqh, double m_money, string pl)
        {
            string[] strArray = xzh.Split(new char[] { '~' });
            int num = 0;
            int num2 = 0;
            foreach (string str in strArray)
            {
                string[] strArray2 = str.Split(new char[] { ',' });
                num2 = 0;
                foreach (string str2 in strArray2)
                {
                    if (zqh.IndexOf(str2) > -1)
                    {
                        num2++;
                    }
                }
                if (num2 == 3)
                {
                    num++;
                }
            }
            int length = strArray.Length;
            double num4 = m_money / ((double) length);
            double num5 = 0.0;
            num5 = -m_money;
            return (num5 + ((num4 * num) * double.Parse(pl)));
        }

        protected double count_sqz_xync(string xzh, string zqh, double m_money, string pl)
        {
            string[] strArray = xzh.Split(new char[] { '~' });
            int num = 0;
            int num2 = 0;
            foreach (string str in strArray)
            {
                string[] strArray2 = str.Split(new char[] { ',' });
                num2 = 0;
                foreach (string str2 in strArray2)
                {
                    if (zqh.IndexOf(str2) > -1)
                    {
                        num2++;
                    }
                }
                if (num2 == 3)
                {
                    num++;
                }
            }
            int length = strArray.Length;
            double num4 = m_money / ((double) length);
            double num5 = 0.0;
            num5 = -m_money;
            return (num5 + ((num4 * num) * double.Parse(pl)));
        }

        protected double count_sqz4_jssfc(string xzh, string zqh, double m_money, string pl)
        {
            string[] strArray = xzh.Split(new char[] { '~' });
            int num = 0;
            int num2 = 0;
            foreach (string str in strArray)
            {
                string[] strArray2 = str.Split(new char[] { ',' });
                num2 = 0;
                foreach (string str2 in strArray2)
                {
                    if (zqh.IndexOf(str2) > -1)
                    {
                        num2++;
                    }
                }
                if (num2 == 4)
                {
                    num++;
                }
            }
            int length = strArray.Length;
            double num4 = m_money / ((double) length);
            double num5 = 0.0;
            num5 = -m_money;
            return (num5 + ((num4 * num) * double.Parse(pl)));
        }

        protected double count_sqz4_kl10(string xzh, string zqh, double m_money, string pl)
        {
            string[] strArray = xzh.Split(new char[] { '~' });
            int num = 0;
            int num2 = 0;
            foreach (string str in strArray)
            {
                string[] strArray2 = str.Split(new char[] { ',' });
                num2 = 0;
                foreach (string str2 in strArray2)
                {
                    if (zqh.IndexOf(str2) > -1)
                    {
                        num2++;
                    }
                }
                if (num2 == 4)
                {
                    num++;
                }
            }
            int length = strArray.Length;
            double num4 = m_money / ((double) length);
            double num5 = 0.0;
            num5 = -m_money;
            return (num5 + ((num4 * num) * double.Parse(pl)));
        }

        protected double count_sqz4_xync(string xzh, string zqh, double m_money, string pl)
        {
            string[] strArray = xzh.Split(new char[] { '~' });
            int num = 0;
            int num2 = 0;
            foreach (string str in strArray)
            {
                string[] strArray2 = str.Split(new char[] { ',' });
                num2 = 0;
                foreach (string str2 in strArray2)
                {
                    if (zqh.IndexOf(str2) > -1)
                    {
                        num2++;
                    }
                }
                if (num2 == 4)
                {
                    num++;
                }
            }
            int length = strArray.Length;
            double num4 = m_money / ((double) length);
            double num5 = 0.0;
            num5 = -m_money;
            return (num5 + ((num4 * num) * double.Parse(pl)));
        }

        protected double count_tmbs(string xzh, string zqh, double m_money, string pl, string pl_list)
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

        protected double count_wqz5_jssfc(string xzh, string zqh, double m_money, string pl)
        {
            string[] strArray = xzh.Split(new char[] { '~' });
            int num = 0;
            int num2 = 0;
            foreach (string str in strArray)
            {
                string[] strArray2 = str.Split(new char[] { ',' });
                num2 = 0;
                foreach (string str2 in strArray2)
                {
                    if (zqh.IndexOf(str2) > -1)
                    {
                        num2++;
                    }
                }
                if (num2 == 5)
                {
                    num++;
                }
            }
            int length = strArray.Length;
            double num4 = m_money / ((double) length);
            double num5 = 0.0;
            num5 = -m_money;
            return (num5 + ((num4 * num) * double.Parse(pl)));
        }

        protected double count_wqz5_kl10(string xzh, string zqh, double m_money, string pl)
        {
            string[] strArray = xzh.Split(new char[] { '~' });
            int num = 0;
            int num2 = 0;
            foreach (string str in strArray)
            {
                string[] strArray2 = str.Split(new char[] { ',' });
                num2 = 0;
                foreach (string str2 in strArray2)
                {
                    if (zqh.IndexOf(str2) > -1)
                    {
                        num2++;
                    }
                }
                if (num2 == 5)
                {
                    num++;
                }
            }
            int length = strArray.Length;
            double num4 = m_money / ((double) length);
            double num5 = 0.0;
            num5 = -m_money;
            return (num5 + ((num4 * num) * double.Parse(pl)));
        }

        protected double count_wqz5_xync(string xzh, string zqh, double m_money, string pl)
        {
            string[] strArray = xzh.Split(new char[] { '~' });
            int num = 0;
            int num2 = 0;
            foreach (string str in strArray)
            {
                string[] strArray2 = str.Split(new char[] { ',' });
                num2 = 0;
                foreach (string str2 in strArray2)
                {
                    if (zqh.IndexOf(str2) > -1)
                    {
                        num2++;
                    }
                }
                if (num2 == 5)
                {
                    num++;
                }
            }
            int length = strArray.Length;
            double num4 = m_money / ((double) length);
            double num5 = 0.0;
            num5 = -m_money;
            return (num5 + ((num4 * num) * double.Parse(pl)));
        }

        public string GetBetGroupStringPaymentFalse(string play_id, string odds_id, string play_name, string bet_group, string odds, string amount, double rate, string hyAmount)
        {
            string str = "";
            string[] strArray = bet_group.Split(new char[] { '~' });
            amount = (double.Parse(amount) * rate).ToString();
            for (int i = 0; i < strArray.Length; i++)
            {
                str = str + "<tr onMouseOut=\"this.style.backgroundColor=''\" onMouseOver=\"this.style.backgroundColor='#FFFFA2'\">";
                string str2 = strArray[i];
                string s = "0";
                s = (double.Parse(odds) * double.Parse(amount)).ToString();
                string str4 = string.Format("{0:F1}", double.Parse(amount));
                string str5 = string.Format("{0:F1}", double.Parse(s) - double.Parse(amount));
                string str6 = string.Format("{0:F1}", double.Parse(hyAmount));
                string str7 = string.Format("<span  class='blue'>{0}</span>@<b class='red'>{1}</b>", play_name, odds);
                if (!odds_id.Equals("330") && !odds_id.Equals("1200"))
                {
                    if (odds_id.Equals("72055"))
                    {
                        str2 = this.px_pcdd(str2);
                    }
                    else
                    {
                        str2 = px(str2);
                    }
                }
                str = str + string.Format("<td>{0}</td><td>{1}</td><td>{2}</td><td align='right'>{3}&nbsp;</td><td align='right'>{4}&nbsp;</td><td align='right'>{5}&nbsp;</td>", new object[] { i + 1, str2.Replace(',', '、'), str7, str6, str4, "-" + str5 }) + "</tr>";
            }
            str = str + "<tr  style='font-weight:bold'>";
            string str8 = string.Format("{0:F1}", double.Parse(amount) * strArray.Length);
            string str9 = string.Format("{0:F1}", ((double.Parse(amount) * double.Parse(odds)) * strArray.Length) - (double.Parse(amount) * strArray.Length));
            string str10 = string.Format("{0:F1}", double.Parse(hyAmount) * strArray.Length);
            return (str + string.Format("<th>{0}</th><th>{1}</th><th>{2}</th><th align='right'>{3}&nbsp;</th><th align='right'>{4}&nbsp;</th><th align='right'>{5}&nbsp;</th>", new object[] { "合計：", "共" + strArray.Length + "組", "", str10, str8, "-" + str9 }) + "</tr>");
        }

        public string GetBetGroupStringPaymentTrue(string phase_id, string play_id, string odds_id, string play_name, string bet_group, string odds, string amount, string lottery_type, double rate, double drawback, string hyAmount)
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
            DataTable phaseByPID = null;
            int num4 = 0;
            if (lottery_type.Equals(num4.ToString()))
            {
                phaseByPID = CallBLL.cz_phase_kl10_bll.GetPhaseByPID(phase_id);
            }
            else
            {
                int num5 = 3;
                if (lottery_type.Equals(num5.ToString()))
                {
                    phaseByPID = CallBLL.cz_phase_xync_bll.GetPhaseByPID(phase_id);
                }
                else
                {
                    int num6 = 7;
                    if (lottery_type.Equals(num6.ToString()))
                    {
                        phaseByPID = CallBLL.cz_phase_pcdd_bll.GetPhaseByPID(phase_id);
                    }
                    else
                    {
                        int num7 = 14;
                        if (lottery_type.Equals(num7.ToString()))
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
            phaseByPID.Rows[0]["phase"].ToString().Trim();
            int num8 = 7;
            if (lottery_type.Equals(num8.ToString()))
            {
                str11 = phaseByPID.Rows[0]["sn"].ToString().Trim();
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
                zqh = str + "," + str2 + "," + str3 + "," + str4 + "," + str5 + "," + str6 + "," + str7 + "," + str8;
                string text1 = str + "," + str2;
                str10 = str + "," + str2 + "," + str3;
                string text2 = str + "," + str2 + "," + str3 + "," + str4 + "," + str5 + "," + str6 + "," + str7 + "," + str8;
            }
            string str12 = "";
            string[] strArray = bet_group.Split(new char[] { '~' });
            double num = 0.0;
            double num2 = 0.0;
            amount = (double.Parse(amount) * rate).ToString();
            for (int i = 0; i < strArray.Length; i++)
            {
                string str20;
                int num11;
                string str21;
                int num27;
                string str22;
                string str23;
                int num30;
                string xzh = strArray[i];
                string s = "0";
                int num10 = 0;
                if ((lottery_type.Equals(num10.ToString()) && ((str20 = play_id) != null)) && <PrivateImplementationDetails>{F4B22813-5AF5-4276-8E58-35C912C332C7}.$$method0x6000450-1.TryGetValue(str20, out num11))
                {
                    switch (num11)
                    {
                        case 0:
                            s = this.count_rqz_kl10(xzh, zqh, double.Parse(amount), odds).ToString();
                            break;

                        case 1:
                            s = this.count_rlz_kl10(xzh, zqh, double.Parse(amount), odds).ToString();
                            break;

                        case 2:
                            s = this.count_rsxz_kl10(xzh, zqh, double.Parse(amount), odds).ToString();
                            break;

                        case 3:
                            s = this.count_sqz_kl10(xzh, zqh, double.Parse(amount), odds).ToString();
                            break;

                        case 4:
                            s = this.count_sqz_kl10(xzh, str10, double.Parse(amount), odds).ToString();
                            break;

                        case 5:
                            s = this.count_rsxz_kl10(xzh, str10, double.Parse(amount), odds).ToString();
                            break;

                        case 6:
                            s = this.count_sqz4_kl10(xzh, zqh, double.Parse(amount), odds).ToString();
                            break;

                        case 7:
                            s = this.count_wqz5_kl10(xzh, zqh, double.Parse(amount), odds).ToString();
                            break;
                    }
                }
                int num20 = 3;
                if (lottery_type.Equals(num20.ToString()) && ((str21 = play_id) != null))
                {
                    if (!(str21 == "72"))
                    {
                        if (str21 == "74")
                        {
                            goto Label_067C;
                        }
                        if (str21 == "73")
                        {
                            goto Label_069C;
                        }
                        if (str21 == "75")
                        {
                            goto Label_06BC;
                        }
                        if (str21 == "78")
                        {
                            goto Label_06DC;
                        }
                        if (str21 == "79")
                        {
                            goto Label_06FC;
                        }
                    }
                    else
                    {
                        s = this.count_rqz_xync(xzh, zqh, double.Parse(amount), odds).ToString();
                    }
                }
                goto Label_071A;
            Label_067C:
                s = this.count_rlz_xync(xzh, zqh, double.Parse(amount), odds).ToString();
                goto Label_071A;
            Label_069C:
                s = this.count_rsxz_xync(xzh, zqh, double.Parse(amount), odds).ToString();
                goto Label_071A;
            Label_06BC:
                s = this.count_sqz_xync(xzh, zqh, double.Parse(amount), odds).ToString();
                goto Label_071A;
            Label_06DC:
                s = this.count_sqz4_xync(xzh, zqh, double.Parse(amount), odds).ToString();
                goto Label_071A;
            Label_06FC:
                s = this.count_wqz5_xync(xzh, zqh, double.Parse(amount), odds).ToString();
            Label_071A:
                num27 = 7;
                if ((lottery_type.Equals(num27.ToString()) && ((str22 = play_id) != null)) && (str22 == "71014"))
                {
                    s = this.count_tmbs(xzh, str11, double.Parse(amount), odds, "").ToString();
                }
                int num29 = 0;
                if ((lottery_type.Equals(num29.ToString()) && ((str23 = play_id) != null)) && <PrivateImplementationDetails>{F4B22813-5AF5-4276-8E58-35C912C332C7}.$$method0x6000450-2.TryGetValue(str23, out num30))
                {
                    switch (num30)
                    {
                        case 0:
                            s = this.count_rqz_jssfc(xzh, zqh, double.Parse(amount), odds).ToString();
                            break;

                        case 1:
                            s = this.count_rlz_jssfc(xzh, zqh, double.Parse(amount), odds).ToString();
                            break;

                        case 2:
                            s = this.count_rsxz_jssfc(xzh, zqh, double.Parse(amount), odds).ToString();
                            break;

                        case 3:
                            s = this.count_sqz_jssfc(xzh, zqh, double.Parse(amount), odds).ToString();
                            break;

                        case 4:
                            s = this.count_sqz_jssfc(xzh, str10, double.Parse(amount), odds).ToString();
                            break;

                        case 5:
                            s = this.count_rsxz_jssfc(xzh, str10, double.Parse(amount), odds).ToString();
                            break;

                        case 6:
                            s = this.count_sqz4_jssfc(xzh, zqh, double.Parse(amount), odds).ToString();
                            break;

                        case 7:
                            s = this.count_wqz5_jssfc(xzh, zqh, double.Parse(amount), odds).ToString();
                            break;
                    }
                }
                if (double.Parse(s) > 0.0)
                {
                    num += double.Parse(s);
                    num2++;
                    str12 = str12 + "<tr class='tdbg' onMouseOut=\"this.style.backgroundColor=''\" onMouseOver=\"this.style.backgroundColor='#FFFFA2'\">";
                }
                else
                {
                    str12 = str12 + "<tr onMouseOut=\"this.style.backgroundColor=''\" onMouseOver=\"this.style.backgroundColor='#FFFFA2'\">";
                }
                string str15 = string.Format("<span  class='blue'>{0}</span>@<b class='red'>{1}</b>", play_name, odds);
                if (!odds_id.Equals("330") && !odds_id.Equals("1200"))
                {
                    if (odds_id.Equals("72055"))
                    {
                        xzh = this.px_pcdd(xzh);
                    }
                    else
                    {
                        xzh = px(xzh);
                    }
                }
                string str16 = "";
                if (double.Parse(s) > 0.0)
                {
                    str16 = " class='winner'";
                }
                str12 = str12 + string.Format("<td>{0}</td><td>{1}</td><td>{2}</td><td align='right'>{3}&nbsp;</td><td align='right'>{4}&nbsp;</td><td align='right' " + str16 + ">{5}&nbsp;</td>", new object[] { i + 1, (double.Parse(s) > 0.0) ? ("<span style='font-weight:bold'>" + xzh.Replace(',', '、') + "</span>") : xzh.Replace(',', '、'), str15, string.Format("{0:F1}", double.Parse(hyAmount)), string.Format("{0:F1}", double.Parse(amount)), string.Format("{0:F1}", double.Parse(s)) }) + "</tr>";
            }
            string str17 = string.Format("{0:F1}", double.Parse(amount) * strArray.Length);
            string str18 = string.Format("{0:F1}", num - (double.Parse(amount) * (strArray.Length - num2)));
            double.Parse(amount);
            double num1 = drawback / 100.0;
            string str19 = string.Format("{0:F1}", double.Parse(hyAmount) * strArray.Length);
            return ((str12 + "<tr  style='font-weight:bold'>") + string.Format("<th>{0}</th><th>{1}</th><th>{2}</th><th align='right'>{3}&nbsp;</th><th align='right'>{4}&nbsp;</th><th align='right'>{5}&nbsp;</th>", new object[] { "合計：", "共" + strArray.Length + "組", "", str19, str17, str18 }) + "</tr>");
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
                if (str.IndexOf("bill_kc") > -1)
                {
                    base.Permission_Aspx_ZJ(model, "po_1_1");
                }
                if (str.IndexOf("newbet_kc") > -1)
                {
                    base.Permission_Aspx_ZJ(model, "po_3_5");
                }
                if (str.IndexOf("reportdetail_b_kc") > -1)
                {
                    base.Permission_Aspx_ZJ(model, "po_4_1");
                }
                if (str.IndexOf("reportdetail_t_kc") > -1)
                {
                    base.Permission_Aspx_ZJ(model, "po_4_2");
                }
            }
            else
            {
                if ((str.IndexOf("billsearchlist") > -1) || (str.IndexOf("newbet_kc") > -1))
                {
                    base.Response.Redirect("/MessagePage.aspx?code=u100057&url=&issuccess=1&isback=0&isopen=1");
                    base.Response.End();
                }
                if (str.IndexOf("bill_kc") > -1)
                {
                    base.Permission_Aspx_DL(model, "po_5_1");
                }
                if (str.IndexOf("reportdetail_b_kc") > -1)
                {
                    base.Permission_Aspx_ZJ(model, "po_7_1");
                }
                if (str.IndexOf("reportdetail_t_kc") > -1)
                {
                    base.Permission_Aspx_ZJ(model, "po_7_2");
                }
            }
            string str2 = LSRequest.qq("ispay");
            string str3 = LSRequest.qq("orderid");
            this.OutWindow = LSRequest.qq("ow");
            this.atz = LSRequest.qq("atz");
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
                string hyAmount = (double.Parse(s) / double.Parse(str12)).ToString();
                s = (double.Parse(s) / double.Parse(str12)).ToString();
                if (model.get_u_type().Equals("zj"))
                {
                    odds = betByOrdernum.Rows[0]["odds_zj"].ToString();
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
                if (this.is_payment.Equals("1"))
                {
                    this.groupString = this.GetBetGroupStringPaymentTrue(str9, str4, str5, str6, str7, odds, s, str10, rate, drawback, hyAmount);
                }
                else
                {
                    this.groupString = this.GetBetGroupStringPaymentFalse(str4, str5, str6, str7, odds, s, rate, hyAmount);
                }
            }
        }

        public static string px(string p_str)
        {
            string[] array = p_str.Split(new char[] { ',' });
            Array.Sort<string>(array);
            return string.Join(",", array);
        }

        private string px_pcdd(string str)
        {
            string str2 = "";
            string[] strArray = str.Split(new char[] { ',' });
            int[] array = new int[strArray.Length];
            for (int i = 0; i < strArray.Length; i++)
            {
                array[i] = Convert.ToInt32(strArray[i]);
            }
            Array.Sort<int>(array);
            for (int j = 0; j < array.Length; j++)
            {
                str2 = str2 + array[j] + ",";
            }
            return str2.Substring(0, str2.Length - 1);
        }
    }
}

