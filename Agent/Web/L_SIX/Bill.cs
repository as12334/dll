namespace Agent.Web.L_SIX
{
    using Agent.Web.WebBase;
    using LotterySystem.Common;
    using LotterySystem.Model;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using System.Text;

    public class Bill : MemberPageBase
    {
        private string drawback = "";
        private string levelName = "";
        private string rdoDetailCount = "";
        private string rdoDown = "";
        private string rdoType = "";
        private string u_type = "";
        protected agent_userinfo_session userModel;
        private string userName = "";

        private void CreateCountTM()
        {
            string str = "";
            string str2 = "";
            string str3 = "";
            DataTable table = CallBLL.cz_bet_six_bll.GetBillCountList_TM(this.rdoType, this.u_type, this.userName, this.levelName, this.drawback, "91001,91002,91003,91004,91005,91006,91007,91038,91039", ref str, ref str2, ref str3);
            if (str.Equals("0"))
            {
                base.Response.Write(base.ShowDialogBox("已經開獎，不允許賬單備份！", null, 2));
                base.Response.End();
            }
            else if (table == null)
            {
                base.Response.Write(base.ShowDialogBox("無賬單備份！", null, 2));
                base.Response.End();
            }
            else
            {
                StringWriter writer = new StringWriter();
                writer.WriteLine("類型,明細,筆數,實占下注金額,輸贏結果,退水");
                string s = "";
                string str5 = "";
                string str6 = "";
                double num = 0.0;
                double num2 = 0.0;
                double num3 = 0.0;
                double num4 = 0.0;
                double num5 = 0.0;
                double num6 = 0.0;
                int num7 = 0;
                foreach (DataRow row in table.Rows)
                {
                    s = row["bs"].ToString().Trim();
                    str5 = row["play_name"].ToString();
                    str6 = row["put_amount"].ToString();
                    num2 = double.Parse(Utils.GetMathRound(double.Parse(row["szjg"].ToString()), 1));
                    num3 = double.Parse(Utils.GetMathRound(double.Parse(row["szds"].ToString()), 1));
                    num = double.Parse(Utils.GetMathRound(double.Parse(row["sum_m"].ToString()), 1));
                    num4 += double.Parse(row["sum_m"].ToString());
                    num5 += double.Parse(row["szjg"].ToString());
                    num6 += double.Parse(row["szds"].ToString());
                    num7 += int.Parse(s);
                    writer.WriteLine(string.Concat(new object[] { str5, ",", str6, ",", s, ",", num, ",", num2, ",", num3 }));
                }
                writer.WriteLine(string.Concat(new object[] { "合計, ,", num7, ",", Utils.GetMathRound(num4, 1), ",", Utils.GetMathRound(num5, 1), ",", Utils.GetMathRound(num6, 1) }));
                writer.WriteLine(string.Format("【特碼統計】生成時間:{0},,,,,", Convert.ToDateTime(str2).ToString("yyyy-MM-dd HH:mm:ss")));
                writer.Close();
                string str7 = "LHC";
                base.Response.AddHeader("Content-Disposition", "attachment; filename=" + str7 + "_" + str3 + "_" + DateTime.Now.Date.ToString("yyyy-MM-dd") + "_" + DateTime.Now.ToLongTimeString().Replace(":", "-") + ".csv");
                base.Response.ContentType = "application/ms-excel";
                base.Response.ContentEncoding = Encoding.GetEncoding("GB2312");
                base.Response.Write(writer);
                base.Response.End();
            }
        }

        private void CreateFTM()
        {
            string str = "";
            string str2 = "";
            DataTable table = CallBLL.cz_bet_six_bll.GetBillList_F_TM_LM(this.rdoType, this.u_type, this.userName, this.levelName, this.drawback, "91016,91017,91018,91019,91020,91040,91030,91031,91032,91033,91034,91035,91036,91037,91047,91048,91049,91050,91051,91015,91058,91059,91060,91061,91062,91063,91064,91065", "91001,91002,91003,91004,91005,91006,91007,91038,91039", ref str, ref str2);
            if (str.Equals("0"))
            {
                base.Response.Write(base.ShowDialogBox("已經開獎，不允許賬單備份！", null, 2));
                base.Response.End();
            }
            else if (table == null)
            {
                base.Response.Write(base.ShowDialogBox("無賬單備份！", null, 2));
                base.Response.End();
            }
            else
            {
                StringWriter writer = new StringWriter();
                writer.WriteLine("期號,註單號,時間,會員,成数(%),類型,明細,金額,賠率,");
                string str3 = "";
                string str4 = "";
                foreach (DataRow row in table.Rows)
                {
                    str3 = row["bet_val"].ToString().Trim();
                    str4 = row["phase"].ToString().Trim();
                    string str5 = row["odds"].ToString().Trim();
                    if (this.u_type.Equals("zj"))
                    {
                        str5 = row["odds_zj"].ToString().Trim();
                    }
                    string str6 = "";
                    if ((row["isDelete"]).Equals("1"))
                    {
                        str6 = "單已註銷";
                    }
                    writer.WriteLine(row["phase"].ToString().Trim() + "," + row["order_num"].ToString().Trim() + "\t," + Convert.ToDateTime(row["bet_time"].ToString()).ToString("yyyy-MM-dd HH:mm:ss") + "\t," + row["u_name"].ToString().Trim() + "," + row[this.u_type + "_rate"].ToString().Trim() + "," + row["play_name"].ToString().Trim() + "," + str3 + "," + row["amount"].ToString().Trim() + "," + str5.Trim().Replace(",", "&") + "," + str6);
                }
                writer.WriteLine(string.Format("【非特碼統計】生成時間:{0},,,,,,,,,,", Convert.ToDateTime(str2).ToString("yyyy-MM-dd HH:mm:ss")));
                writer.Close();
                string str7 = "LHC";
                base.Response.AddHeader("Content-Disposition", "attachment; filename=" + str7 + "_" + str4 + "_" + DateTime.Now.Date.ToString("yyyy-MM-dd") + "_" + DateTime.Now.ToLongTimeString().Replace(":", "-") + ".csv");
                base.Response.ContentType = "application/ms-excel";
                base.Response.ContentEncoding = Encoding.GetEncoding("GB2312");
                base.Response.Write(writer);
                base.Response.End();
            }
        }

        private void CreateLM()
        {
            string str = "";
            string str2 = "";
            DataTable table = CallBLL.cz_bet_six_bll.GetBillList_TM_LM(this.rdoType, this.u_type, this.userName, this.levelName, this.drawback, "91016,91017,91018,91019,91020,91040,91030,91031,91032,91033,91034,91035,91036,91037,91047,91048,91049,91050,91051,91015,91058,91059,91060,91061,91062,91063,91064,91065", ref str, ref str2);
            if (str.Equals("0"))
            {
                base.Response.Write(base.ShowDialogBox("已經開獎，不允許賬單備份！", null, 2));
                base.Response.End();
            }
            else if (table == null)
            {
                base.Response.Write(base.ShowDialogBox("無賬單備份！", null, 2));
                base.Response.End();
            }
            else
            {
                StringWriter writer = new StringWriter();
                writer.WriteLine("期號,註單號,時間,會員,成数(%),項目,明細,组数,金額,賠率,");
                string str3 = "";
                string str4 = "";
                string[] strArray = new string[1];
                foreach (DataRow row in table.Rows)
                {
                    str3 = "";
                    if (((("92285,92286,92287,92288,92289,92575".IndexOf(row["odds_id"].ToString()) <= -1) && ("92638,92639,92640,92641,92642,92643".IndexOf(row["odds_id"].ToString()) <= -1)) && (("92572,92588,92589,92590,92591,92592".IndexOf(row["odds_id"].ToString()) <= -1) && !"92566".Equals(row["odds_id"].ToString()))) && (((!"92567".Equals(row["odds_id"].ToString()) && !"92568".Equals(row["odds_id"].ToString())) && (!"92569".Equals(row["odds_id"].ToString()) && !"92570".Equals(row["odds_id"].ToString()))) && ((!"92571".Equals(row["odds_id"].ToString()) && !"92636".Equals(row["odds_id"].ToString())) && !"92637".Equals(row["odds_id"].ToString()))))
                    {
                        goto Label_07E5;
                    }
                    if (Convert.ToInt32(row["unit_cnt"].ToString()) > 0)
                    {
                        switch (Convert.ToInt32(row["lm_type"].ToString().Trim()))
                        {
                            case 0:
                                goto Label_0294;

                            case 1:
                                str3 = str3 + "膽拖『" + row["unit_cnt"].ToString() + " 組 』";
                                strArray = row["bet_val"].ToString().Trim().Split(new char[] { '|' });
                                str3 = (str3 + strArray[0].Replace(',', '、')) + "拖" + strArray[1].Replace(',', '、');
                                break;

                            case 2:
                                str3 = str3 + "生肖對碰『" + row["unit_cnt"].ToString() + " 組 』";
                                strArray = row["bet_val"].ToString().Trim().Split(new char[] { '|' });
                                str3 = (str3 + strArray[0].Replace(',', '、')) + "對碰" + strArray[1].Replace(',', '、');
                                break;

                            case 3:
                                str3 = str3 + "尾數對碰『" + row["unit_cnt"].ToString() + " 組 』";
                                strArray = row["bet_val"].ToString().Trim().Split(new char[] { '|' });
                                str3 = (str3 + strArray[0].Replace(',', '、')) + "對碰" + strArray[1].Replace(',', '、');
                                break;

                            case 4:
                                str3 = str3 + "混合對碰『" + row["unit_cnt"].ToString() + " 組 』";
                                strArray = row["bet_val"].ToString().Trim().Split(new char[] { '|' });
                                str3 = (str3 + strArray[0].Replace(',', '、')) + "對碰" + strArray[1].Replace(',', '、');
                                break;
                        }
                    }
                    goto Label_0897;
                Label_0294:
                    if ((row["odds_id"].ToString().Trim().Equals("92566") || row["odds_id"].ToString().Trim().Equals("92567")) || (row["odds_id"].ToString().Trim().Equals("92568") || row["odds_id"].ToString().Trim().Equals("92636")))
                    {
                        if (double.Parse(row["unit_cnt"].ToString()) > 1.0)
                        {
                            str3 = str3 + "復式『 " + row["unit_cnt"].ToString() + " 組』";
                        }
                        string[] strArray2 = row["bet_value"].ToString().Split(new char[] { ',' });
                        ArrayList list = new ArrayList();
                        foreach (string str5 in strArray2)
                        {
                            list.Add(FunctionSix.GetSXNameBySxidText(int.Parse(str5)));
                        }
                        str3 = str3 + string.Join("、", list.ToArray());
                    }
                    else if ((row["odds_id"].ToString().Trim().Equals("92569") || row["odds_id"].ToString().Trim().Equals("92570")) || (row["odds_id"].ToString().Trim().Equals("92571") || row["odds_id"].ToString().Trim().Equals("92637")))
                    {
                        if (double.Parse(row["unit_cnt"].ToString()) > 1.0)
                        {
                            str3 = str3 + "復式『 " + row["unit_cnt"].ToString() + " 組』";
                        }
                        str3 = str3 + row["bet_val"].ToString().Replace(",", "尾、").Replace("10", "xx").Replace("0", "").Replace("xx", "0") + "尾";
                    }
                    else
                    {
                        if (double.Parse(row["unit_cnt"].ToString()) > 1.0)
                        {
                            str3 = str3 + "復式『 " + row["unit_cnt"].ToString() + " 組』";
                        }
                        str3 = str3 + row["bet_val"].ToString().Replace(',', '、');
                    }
                    goto Label_0897;
                Label_07E5:
                    if (row["odds_id"].ToString().Equals("92565"))
                    {
                        string[] strArray3 = row["bet_val"].ToString().Split(new char[] { ',' });
                        ArrayList list2 = new ArrayList();
                        foreach (string str6 in strArray3)
                        {
                            list2.Add(FunctionSix.GetSXNameBySxidText(int.Parse(str6)));
                        }
                        str3 = str3 + string.Join("、", list2.ToArray());
                    }
                    else
                    {
                        str3 = row["bet_val"].ToString().Trim();
                    }
                Label_0897:
                    str4 = row["phase"].ToString().Trim();
                    string odds = row["odds"].ToString().Trim();
                    if (this.u_type.Equals("zj"))
                    {
                        odds = row["odds_zj"].ToString().Trim();
                    }
                    if (!string.IsNullOrEmpty(row["bet_wt_zj"]))
                    {
                        odds = odds + "\t" + GetLMWT(row["odds_id"].ToString(), odds, row["bet_wt_zj"].ToString());
                    }
                    string str8 = "";
                    if ((row["isDelete"]).Equals("1"))
                    {
                        str8 = "單已註銷";
                    }
                    writer.WriteLine(row["phase"].ToString().Trim() + "," + row["order_num"].ToString().Trim() + "\t," + Convert.ToDateTime(row["bet_time"].ToString()).ToString("yyyy-MM-dd HH:mm:ss") + "\t," + row["u_name"].ToString().Trim() + "," + row[this.u_type + "_rate"].ToString().Trim() + "," + row["play_name"].ToString().Trim() + "," + str3 + "," + row["unit_cnt"].ToString().Trim() + "," + row["amount"].ToString().Trim() + "," + odds.Replace(",", "&") + "," + str8);
                }
                writer.WriteLine(string.Format("【連碼明細】生成時間:{0},,,,,,,,,,", Convert.ToDateTime(str2).ToString("yyyy-MM-dd HH:mm:ss")));
                writer.Close();
                string str9 = "LHC";
                base.Response.AddHeader("Content-Disposition", "attachment; filename=" + str9 + "_" + str4 + "_" + DateTime.Now.Date.ToString("yyyy-MM-dd") + "_" + DateTime.Now.ToLongTimeString().Replace(":", "-") + ".csv");
                base.Response.ContentType = "application/ms-excel";
                base.Response.ContentEncoding = Encoding.GetEncoding("GB2312");
                base.Response.Write(writer);
                base.Response.End();
            }
        }

        private void CreateTM()
        {
            string str = "";
            string str2 = "";
            DataTable table = CallBLL.cz_bet_six_bll.GetBillList_TM_LM(this.rdoType, this.u_type, this.userName, this.levelName, this.drawback, "91001,91002,91003,91004,91005,91006,91007,91038,91039", ref str, ref str2);
            if (str.Equals("0"))
            {
                base.Response.Write(base.ShowDialogBox("已經開獎，不允許賬單備份！", null, 2));
                base.Response.End();
            }
            else if (table == null)
            {
                base.Response.Write(base.ShowDialogBox("無賬單備份！", null, 2));
                base.Response.End();
            }
            else
            {
                StringWriter writer = new StringWriter();
                writer.WriteLine("期號,註單號,時間,會員,成数(%),類型,明細,金額,賠率,");
                string str3 = "";
                string str4 = "";
                foreach (DataRow row in table.Rows)
                {
                    str3 = row["bet_val"].ToString().Trim();
                    str4 = row["phase"].ToString().Trim();
                    string str5 = row["odds"].ToString().Trim();
                    if (this.u_type.Equals("zj"))
                    {
                        str5 = row["odds_zj"].ToString().Trim();
                    }
                    string str6 = "";
                    if ((row["isDelete"]).Equals("1"))
                    {
                        str6 = "單已註銷";
                    }
                    writer.WriteLine(row["phase"].ToString().Trim() + "," + row["order_num"].ToString().Trim() + "\t," + Convert.ToDateTime(row["bet_time"].ToString()).ToString("yyyy-MM-dd HH:mm:ss") + "\t," + row["u_name"].ToString().Trim() + "," + row[this.u_type + "_rate"].ToString().Trim() + "," + row["play_name"].ToString().Trim() + "," + str3 + "," + row["amount"].ToString().Trim() + "," + str5.Replace(",", "&") + "," + str6);
                }
                writer.WriteLine(string.Format("【特碼明細】生成時間:{0},,,,,,,,,,", Convert.ToDateTime(str2).ToString("yyyy-MM-dd HH:mm:ss")));
                writer.Close();
                string str7 = "LHC";
                base.Response.AddHeader("Content-Disposition", "attachment; filename=" + str7 + "_" + str4 + "_" + DateTime.Now.Date.ToString("yyyy-MM-dd") + "_" + DateTime.Now.ToLongTimeString().Replace(":", "-") + ".csv");
                base.Response.ContentType = "application/ms-excel";
                base.Response.ContentEncoding = Encoding.GetEncoding("GB2312");
                base.Response.Write(writer);
                base.Response.End();
            }
        }

        public static string GetLMWT(string odds_id, string odds, string bet_wt_zj)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            if (string.IsNullOrEmpty(bet_wt_zj))
            {
                return "";
            }
            string[] strArray = bet_wt_zj.Split(new char[] { '~' });
            for (int i = 0; i < strArray.Length; i++)
            {
                if (!string.IsNullOrEmpty(strArray[i]))
                {
                    string key = strArray[i].Split(new char[] { '|' })[0];
                    string str3 = strArray[i].Split(new char[] { '|' })[1];
                    if (!dictionary.ContainsKey(key))
                    {
                        dictionary.Add(key, str3);
                    }
                }
            }
            string str4 = "";
            if (dictionary.Count > 0)
            {
                foreach (KeyValuePair<string, string> pair in dictionary)
                {
                    str4 = str4 + string.Format("【含 『{0}』 的號碼賠率下調為『{1}』】", pair.Key, pair.Value.Replace(",", "&"));
                }
            }
            return str4.ToString();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.userName = this.Session["user_name"].ToString();
            this.userModel = this.Session[this.userName + "lottery_session_user_info"] as agent_userinfo_session;
            base.Permission_Aspx_ZJ(this.userModel, "po_1_1");
            base.Permission_Aspx_DL(this.userModel, "po_5_1");
            if (LSRequest.qq("hdnCreate").Equals("create"))
            {
                this.rdoType = LSRequest.qq("rdoType");
                this.rdoDown = LSRequest.qq("rdoDown");
                this.rdoDetailCount = LSRequest.qq("rdoDetailCount");
                this.u_type = this.userModel.get_u_type().Trim();
                string str2 = this.u_type;
                if (str2 != null)
                {
                    if (!(str2 == "zj"))
                    {
                        if (str2 == "fgs")
                        {
                            this.levelName = "fgs_name";
                            this.drawback = "gd_drawback";
                        }
                        else if (str2 == "gd")
                        {
                            this.levelName = "gd_name";
                            this.drawback = "zd_drawback";
                        }
                        else if (str2 == "zd")
                        {
                            this.levelName = "zd_name";
                            this.drawback = "dl_drawback";
                        }
                        else if (str2 == "dl")
                        {
                            this.levelName = "dl_name";
                            this.drawback = "hy_drawback";
                        }
                    }
                    else
                    {
                        this.drawback = "fgs_drawback";
                    }
                }
                if (this.rdoDown.Equals("1"))
                {
                    if (this.userModel.get_u_type().Equals("zj") || this.userModel.get_u_type().Equals("fgs"))
                    {
                        if (this.rdoDetailCount.Equals("1"))
                        {
                            this.CreateTM();
                        }
                        else
                        {
                            this.CreateCountTM();
                        }
                    }
                    else
                    {
                        this.CreateCountTM();
                    }
                }
                else if (this.rdoDown.Equals("2"))
                {
                    this.CreateFTM();
                }
                else if (this.rdoDown.Equals("3"))
                {
                    this.CreateLM();
                }
                else
                {
                    base.Response.Write(base.ShowDialogBox("下載項错误！", null, 400));
                    base.Response.End();
                }
            }
        }
    }
}

