using LotterySystem.Common;
using LotterySystem.Model;
using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using User.Web.WebBase;

public class m_GroupShow_kc : MemberPageBase_Mobile
{
    protected string groupString = "";
    protected string is_payment = "0";
    protected string masterId = 1.ToString();

    public string GetBetGroupStringPaymentFalse(string play_id, string odds_id, string play_name, string bet_group, string bet_wt, string odds, string amount)
    {
        int length;
        string str = "";
        string[] strArray = bet_group.Split(new char[] { '~' });
        for (int i = 0; i < strArray.Length; i++)
        {
            string str2 = strArray[i];
            string s = "0";
            s = (double.Parse(odds) * double.Parse(amount)).ToString();
            string str4 = string.Format("{0:F1}", double.Parse(amount));
            string str5 = string.Format("{0:F1}", double.Parse(s) - double.Parse(amount));
            string plstr = string.Format("<span  class='lan'>{0}</span>@<b class='hong'>{1}</b>", play_name, odds);
            if (!odds_id.Equals("330") && !odds_id.Equals("1200"))
            {
                if (odds_id.Equals("72055"))
                {
                    str2 = this.px_pcdd(str2);
                }
                else
                {
                    str2 = this.px(str2);
                }
            }
            length = i + 1;
            str = str + this.GetGroupHtml(length.ToString(), str2.Replace(',', '、'), plstr, str4, str5, null);
        }
        string str7 = string.Format("{0:F1}", ((double.Parse(amount) * double.Parse(odds)) * strArray.Length) - (double.Parse(amount) * strArray.Length));
        length = strArray.Length;
        return (str + this.GetGroupHtmlTotal(length.ToString(), string.Format("{0:F1}", double.Parse(amount) * strArray.Length), string.Format("{0:F1}", str7), null));
    }

    public string GetBetGroupStringPaymentTrue(string lottery_type, string phase_id, string play_id, string odds_id, string play_name, string bet_group, string bet_wt, string odds, string amount, double drawback)
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
        int length = 0;
        if (lottery_type.Equals(length.ToString()))
        {
            phaseByPID = CallBLL.cz_phase_kl10_bll.GetPhaseByPID(phase_id);
        }
        else
        {
            length = 3;
            if (lottery_type.Equals(length.ToString()))
            {
                phaseByPID = CallBLL.cz_phase_xync_bll.GetPhaseByPID(phase_id);
            }
            else
            {
                length = 7;
                if (lottery_type.Equals(length.ToString()))
                {
                    phaseByPID = CallBLL.cz_phase_pcdd_bll.GetPhaseByPID(phase_id);
                }
                else
                {
                    return "";
                }
            }
        }
        if (phaseByPID == null)
        {
            return "";
        }
        string str15 = phaseByPID.Rows[0]["phase"].ToString().Trim();
        length = 7;
        if (lottery_type.Equals(length.ToString()))
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
            string str24;
            string xzh = strArray[i];
            string s = "0";
            length = 0;
            if (lottery_type.Equals(length.ToString()))
            {
                str24 = play_id;
                switch (str24)
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
            length = 3;
            if (lottery_type.Equals(length.ToString()))
            {
                str24 = play_id;
                if (str24 != null)
                {
                    if (!(str24 == "72"))
                    {
                        if (str24 == "74")
                        {
                            goto Label_0727;
                        }
                        if (str24 == "73")
                        {
                            goto Label_074A;
                        }
                        if (str24 == "75")
                        {
                            goto Label_076A;
                        }
                        if (str24 == "78")
                        {
                            goto Label_078A;
                        }
                        if (str24 == "79")
                        {
                            goto Label_07AA;
                        }
                    }
                    else
                    {
                        s = base.count_rqz_xync(xzh, str10, double.Parse(amount), odds).ToString();
                    }
                }
            }
            goto Label_07CB;
        Label_0727:
            s = base.count_rlz_xync(xzh, str10, double.Parse(amount), odds).ToString();
            goto Label_07CB;
        Label_074A:
            s = base.count_rsxz_xync(xzh, str10, double.Parse(amount), odds).ToString();
            goto Label_07CB;
        Label_076A:
            s = base.count_sqz_xync(xzh, str10, double.Parse(amount), odds).ToString();
            goto Label_07CB;
        Label_078A:
            s = base.count_sqz4_xync(xzh, str10, double.Parse(amount), odds).ToString();
            goto Label_07CB;
        Label_07AA:
            s = base.count_wqz5_xync(xzh, str10, double.Parse(amount), odds).ToString();
        Label_07CB:
            length = 7;
            if (lottery_type.Equals(length.ToString()))
            {
                str24 = play_id;
                if ((str24 != null) && (str24 == "71014"))
                {
                    s = base.count_tmbs(xzh, zqh, double.Parse(amount), odds, "").ToString();
                }
            }
            if (double.Parse(s) > 0.0)
            {
                num += double.Parse(s);
                num2++;
            }
            string plstr = string.Format("<span  class='lan'>{0}</span>@<b class='hong'>{1}</b>", play_name, odds);
            if (!odds_id.Equals("330") && !odds_id.Equals("1200"))
            {
                if (odds_id.Equals("72055"))
                {
                    xzh = this.px_pcdd(xzh);
                }
                else
                {
                    xzh = this.px(xzh);
                }
            }
            length = i + 1;
            str16 = str16 + this.GetGroupHtmlTrue(length.ToString(), xzh.Replace(',', '、'), plstr, string.Format("{0:F1}", double.Parse(amount)), string.Format("{0:F1}", double.Parse(s)), null);
        }
        string str20 = string.Format("{0:F1}", double.Parse(amount) * strArray.Length);
        string str21 = string.Format("{0:F1}", num - (double.Parse(amount) * (strArray.Length - num2)));
        string str22 = string.Format("{0:F1}", drawback);
        length = strArray.Length;
        return (str16 + this.GetGroupHtmlTotalTrue(length.ToString(), str20, string.Format("{0:F1}", str21), null));
    }

    private string GetGroupHtml(string index, string group, string plstr, string amount, string ky1, string ky2)
    {
        StringBuilder builder = new StringBuilder();
        string str = "tableBox";
        if ((int.Parse(index) % 2) != 0)
        {
            str = "tableBox1";
        }
        builder.AppendFormat("<table class='{0}'>", str);
        builder.Append("<tr class=''>");
        builder.Append("<th align='right' width='30%'>號碼組合&nbsp;</th>");
        builder.AppendFormat("<td align='left'>&nbsp;{0}</td>", group);
        builder.Append("</tr>");
        builder.Append("<tr class=''>");
        builder.Append("<th align='right'>賠率&nbsp;</th>");
        builder.AppendFormat("<td align='left'>&nbsp;{0}</td>", plstr);
        builder.Append("</tr>");
        builder.Append("<tr class=''>");
        builder.Append("<th align='right'>下註額&nbsp;</th>");
        builder.AppendFormat("<td align='left'>&nbsp;{0}</td>", amount);
        builder.Append("</tr>");
        if (!string.IsNullOrEmpty(ky2))
        {
            builder.Append("<tr class=''>");
            builder.Append("<th align='right'>可贏①&nbsp;</th>");
            builder.AppendFormat("<td align='left'>&nbsp;{0}</td>", ky1);
            builder.Append("</tr>");
            builder.Append("<tr class=''>");
            builder.Append("<th align='right'>可贏②&nbsp;</th>");
            builder.AppendFormat("<td align='left'>&nbsp;{0}</td>", ky2);
            builder.Append("</tr>");
        }
        else
        {
            builder.Append("<tr class=''>");
            builder.Append("<th align='right'>可贏&nbsp;</th>");
            builder.AppendFormat("<td align='left'>&nbsp;{0}</td>", ky1);
            builder.Append("</tr>");
        }
        builder.Append("</table>");
        return builder.ToString();
    }

    private string GetGroupHtmlTotal(string num, string amount, string ky1, string ky2)
    {
        StringBuilder builder = new StringBuilder();
        builder.Append("<table class='tableBox'>");
        builder.Append("<tr class=''>");
        builder.Append("<th align='right' width='30%' class='groupshowtotalbg'>合計&nbsp;</th>");
        builder.AppendFormat("<td align='left'>&nbsp;共 {0} 組</td>", num);
        builder.Append("</tr>");
        builder.Append("<tr class=''>");
        builder.Append("<th align='right' class='groupshowtotalbg'>下註額&nbsp;</th>");
        builder.AppendFormat("<td align='left'>&nbsp;{0}</td>", amount);
        builder.Append("</tr>");
        if (!string.IsNullOrEmpty(ky2))
        {
            builder.Append("<tr class=''>");
            builder.Append("<th align='right' class='groupshowtotalbg'>可贏①&nbsp;</th>");
            builder.AppendFormat("<td align='left'>&nbsp;{0}</td>", ky1);
            builder.Append("</tr>");
            builder.Append("<tr class=''>");
            builder.Append("<th align='right' class='groupshowtotalbg'>可贏②&nbsp;</th>");
            builder.AppendFormat("<td align='left'>&nbsp;{0}</td>", ky2);
            builder.Append("</tr>");
        }
        else
        {
            builder.Append("<tr class=''>");
            builder.Append("<th align='right' class='groupshowtotalbg'>可贏&nbsp;</th>");
            builder.AppendFormat("<td align='left'>&nbsp;{0}</td>", ky1);
            builder.Append("</tr>");
        }
        builder.Append("</table>");
        return builder.ToString();
    }

    private string GetGroupHtmlTotalTrue(string num, string amount, string ky1, string ky2)
    {
        StringBuilder builder = new StringBuilder();
        builder.Append("<table class='tableBox'>");
        builder.Append("<tr class=''>");
        builder.Append("<th align='right' width='30%' class='groupshowtotalbg'>合計&nbsp;</th>");
        builder.AppendFormat("<td align='left'>&nbsp;共 {0} 組</td>", num);
        builder.Append("</tr>");
        builder.Append("<tr class=''>");
        builder.Append("<th align='right' class='groupshowtotalbg'>下註額&nbsp;</th>");
        builder.AppendFormat("<td align='left'>&nbsp;{0}</td>", amount);
        builder.Append("</tr>");
        builder.Append("<tr class=''>");
        builder.Append("<th align='right' class='groupshowtotalbg'>結果&nbsp;</th>");
        builder.AppendFormat("<td align='left'>&nbsp;{0}</td>", ky1);
        builder.Append("</tr>");
        builder.Append("</table>");
        return builder.ToString();
    }

    private string GetGroupHtmlTrue(string index, string group, string plstr, string amount, string ky1, string ky2)
    {
        StringBuilder builder = new StringBuilder();
        string str = "tableBox";
        if ((int.Parse(index) % 2) != 0)
        {
            str = "tableBox1";
        }
        builder.AppendFormat("<table class='{0}'>", str);
        builder.Append("<tr class=''>");
        builder.Append("<th align='right' width='30%'>號碼組合&nbsp;</th>");
        builder.AppendFormat("<td align='left'>&nbsp;{0}</td>", group);
        builder.Append("</tr>");
        builder.Append("<tr class=''>");
        builder.Append("<th align='right'>賠率&nbsp;</th>");
        builder.AppendFormat("<td align='left'>&nbsp;{0}</td>", plstr);
        builder.Append("</tr>");
        builder.Append("<tr class=''>");
        builder.Append("<th align='right'>下註額&nbsp;</th>");
        builder.AppendFormat("<td align='left'>&nbsp;{0}</td>", amount);
        builder.Append("</tr>");
        builder.Append("<tr class=''>");
        builder.Append("<th align='right'>結果&nbsp;</th>");
        builder.AppendFormat("<td align='left'>&nbsp;{0}</td>", ky1);
        builder.Append("</tr>");
        builder.Append("</table>");
        return builder.ToString();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        string str = HttpContext.Current.Session["user_name"].ToString();
        cz_userinfo_session getUserModelInfo = base.GetUserModelInfo;
        string str2 = LSRequest.qq("ispay");
        string str3 = LSRequest.qq("orderid");
        DataTable betByOrdernum = CallBLL.cz_bet_kc_bll.GetBetByOrdernum(str3, str2);
        if (betByOrdernum != null)
        {
            string str4 = betByOrdernum.Rows[0]["play_id"].ToString();
            string str5 = betByOrdernum.Rows[0]["odds_id"].ToString();
            string str6 = betByOrdernum.Rows[0]["play_name"].ToString();
            string str7 = betByOrdernum.Rows[0]["bet_group"].ToString();
            string str8 = betByOrdernum.Rows[0]["bet_wt"].ToString();
            string odds = betByOrdernum.Rows[0]["odds"].ToString();
            string str10 = betByOrdernum.Rows[0]["phase_id"].ToString();
            string str11 = betByOrdernum.Rows[0]["lottery_type"].ToString();
            this.is_payment = betByOrdernum.Rows[0]["is_payment"].ToString();
            string s = betByOrdernum.Rows[0]["amount"].ToString();
            string str13 = betByOrdernum.Rows[0]["unit_cnt"].ToString();
            s = (double.Parse(s) / double.Parse(str13)).ToString();
            double drawback = 0.0;
            string str14 = getUserModelInfo.get_su_type();
            if (str14 != null)
            {
                if (!(str14 == "dl"))
                {
                    if (str14 == "zd")
                    {
                        drawback = double.Parse(betByOrdernum.Rows[0]["dl_drawback"].ToString());
                    }
                    else if (str14 == "gd")
                    {
                        drawback = double.Parse(betByOrdernum.Rows[0]["zd_drawback"].ToString());
                    }
                    else if (str14 == "fgs")
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
                this.groupString = this.GetBetGroupStringPaymentTrue(str11, str10, str4, str5, str6, str7, str8, odds, s, drawback);
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

