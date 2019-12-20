namespace Agent.Web
{
    using Agent.Web.WebBase;
    using LotterySystem.Common;
    using LotterySystem.Model;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Text.RegularExpressions;

    public class SystemSet_six : MemberPageBase
    {
        protected string abAcountSet = 0.ToString();
        protected bool childModify = true;
        protected string drawbackAutoSet = "0";
        private bool isOut;
        private bool isRestUserAgent;
        protected DataTable lmGroupTable;
        protected DataTable lotteryDT;
        protected string lotteryId = "";
        protected cz_system_set_six model;
        protected string negativeSale = "0";
        protected string singleNumberSet = 0.ToString();
        protected string skin = "";
        protected string sltString = "";
        protected agent_userinfo_session userModel;

        private int GetLmGroupCount()
        {
            List<cz_lm_group_set> list = new List<cz_lm_group_set>();
            for (int i = 0; i < this.lmGroupTable.Rows.Count; i++)
            {
                int num2 = 0;
                string s = this.lmGroupTable.Rows[i]["play_id"].ToString();
                int num3 = int.Parse(LSRequest.qq(s + "_currentnumcount"));
                if ((s.Equals("91016") || s.Equals("91017")) || (s.Equals("91060") || s.Equals("91061")))
                {
                    num2 = ((num3 * (num3 - 1)) * (num3 - 2)) / 6;
                }
                if (((s.Equals("91018") || s.Equals("91019")) || (s.Equals("91020") || s.Equals("91062"))) || (s.Equals("91063") || s.Equals("91064")))
                {
                    num2 = (num3 * (num3 - 1)) / 2;
                }
                if (s.Equals("91040") || s.Equals("91065"))
                {
                    num2 = (((num3 * (num3 - 1)) * (num3 - 2)) * (num3 - 3)) / 0x18;
                }
                if (s.Equals("91037"))
                {
                    num2 = ((((num3 * (num3 - 1)) * (num3 - 2)) * (num3 - 3)) * (num3 - 4)) / 120;
                }
                if (s.Equals("91047"))
                {
                    num2 = (((((num3 * (num3 - 1)) * (num3 - 2)) * (num3 - 3)) * (num3 - 4)) * (num3 - 5)) / 720;
                }
                if (s.Equals("91048"))
                {
                    num2 = ((((((num3 * (num3 - 1)) * (num3 - 2)) * (num3 - 3)) * (num3 - 4)) * (num3 - 5)) * (num3 - 6)) / 0x13b0;
                }
                if (s.Equals("91049"))
                {
                    num2 = (((((((num3 * (num3 - 1)) * (num3 - 2)) * (num3 - 3)) * (num3 - 4)) * (num3 - 5)) * (num3 - 6)) * (num3 - 7)) / 0x9d80;
                }
                if (s.Equals("91050"))
                {
                    num2 = ((((((((num3 * (num3 - 1)) * (num3 - 2)) * (num3 - 3)) * (num3 - 4)) * (num3 - 5)) * (num3 - 6)) * (num3 - 7)) * (num3 - 8)) / 0x58980;
                }
                if (s.Equals("91051"))
                {
                    num3 = 10;
                    num2 = 1;
                }
                if (s.Equals("91030"))
                {
                    num3 = 6;
                    num2 = 1;
                }
                if (s.Equals("91031") || s.Equals("91034"))
                {
                    num2 = (num3 * (num3 - 1)) / 2;
                }
                if (s.Equals("91032") || s.Equals("91035"))
                {
                    num2 = ((num3 * (num3 - 1)) * (num3 - 2)) / 6;
                }
                if (s.Equals("91033") || s.Equals("91036"))
                {
                    num2 = (((num3 * (num3 - 1)) * (num3 - 2)) * (num3 - 3)) / 0x18;
                }
                if (s.Equals("91058") || s.Equals("91059"))
                {
                    num2 = ((((num3 * (num3 - 1)) * (num3 - 2)) * (num3 - 3)) * (num3 - 4)) / 120;
                }
                cz_lm_group_set item = new cz_lm_group_set();
                item.set_lottery_id(100);
                item.set_play_id(int.Parse(s));
                item.set_max_num_group(num2);
                item.set_current_num_count(num3);
                list.Add(item);
                string playChange = this.GetPlayChange(s);
                if (!string.IsNullOrEmpty(playChange))
                {
                    cz_lm_group_set _set2 = new cz_lm_group_set();
                    _set2.set_lottery_id(100);
                    _set2.set_play_id(int.Parse(playChange));
                    _set2.set_max_num_group(num2);
                    _set2.set_current_num_count(num3);
                    list.Add(_set2);
                }
            }
            int num4 = CallBLL.cz_lm_group_set_bll.Update(list);
            if (list.Count > 0)
            {
                base.lm_group_set_log_six(this.lmGroupTable, CallBLL.cz_lm_group_set_bll.GetList(100), ref this.isRestUserAgent);
            }
            return num4;
        }

        private string GetPlayChange(string playId)
        {
            switch (playId)
            {
                case "91016":
                    return "91060";

                case "91017":
                    return "91061";

                case "91018":
                    return "91062";

                case "91019":
                    return "91063";

                case "91020":
                    return "91064";

                case "91040":
                    return "91065";
            }
            return "";
        }

        private void OutZJ()
        {
            DataTable listByParentName = CallBLL.cz_users_child_bll.GetListByParentName(this.userModel.get_u_name());
            if (listByParentName == null)
            {
                if (FileCacheHelper.get_RedisStatOnline().Equals(1))
                {
                    base.UpdateIsOutOpt(this.userModel.get_u_name());
                }
                else if (FileCacheHelper.get_RedisStatOnline().Equals(2))
                {
                    base.UpdateIsOutOptStack(this.userModel.get_u_name());
                }
                else
                {
                    CallBLL.cz_stat_online_bll.UpdateIsOut(this.userModel.get_u_name());
                }
            }
            else
            {
                foreach (DataRow row in listByParentName.Rows)
                {
                    string str = row["u_name"].ToString();
                    if (FileCacheHelper.get_RedisStatOnline().Equals(1))
                    {
                        base.UpdateIsOutOpt(str);
                    }
                    else if (FileCacheHelper.get_RedisStatOnline().Equals(2))
                    {
                        base.UpdateIsOutOptStack(str);
                    }
                    else
                    {
                        CallBLL.cz_stat_online_bll.UpdateIsOut(str);
                    }
                }
                if (FileCacheHelper.get_RedisStatOnline().Equals(1))
                {
                    base.UpdateIsOutOpt(this.userModel.get_u_name());
                }
                else if (FileCacheHelper.get_RedisStatOnline().Equals(2))
                {
                    base.UpdateIsOutOptStack(this.userModel.get_u_name());
                }
                else
                {
                    CallBLL.cz_stat_online_bll.UpdateIsOut(this.userModel.get_u_name());
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string str = this.Session["user_name"].ToString();
            this.userModel = this.Session[str + "lottery_session_user_info"] as agent_userinfo_session;
            this.skin = this.userModel.get_u_skin();
            if (!this.userModel.get_u_type().Equals("zj"))
            {
                base.Response.Redirect("/MessagePage.aspx?code=u100057&url=&issuccess=1&isback=0");
                base.Response.End();
            }
            base.Permission_Aspx_ZJ(this.userModel, "po_3_2");
            this.lotteryId = LSRequest.qq("lid");
            this.lotteryDT = base.GetLotteryList();
            base.IsLotteryExist(this.lotteryId, "u100032", "1", "");
            int num = 100;
            if (!this.lotteryId.Equals(num.ToString()))
            {
                base.Response.Redirect("/SystemSet/SystemSet_kc.aspx?lid=" + this.lotteryId, true);
            }
            foreach (DataRow row in this.lotteryDT.Rows)
            {
                if ((((Convert.ToInt32(row["id"].ToString()).Equals(0) || Convert.ToInt32(row["id"].ToString()).Equals(1)) || (Convert.ToInt32(row["id"].ToString()).Equals(2) || Convert.ToInt32(row["id"].ToString()).Equals(3))) || ((Convert.ToInt32(row["id"].ToString()).Equals(4) || Convert.ToInt32(row["id"].ToString()).Equals(5)) || (Convert.ToInt32(row["id"].ToString()).Equals(6) || Convert.ToInt32(row["id"].ToString()).Equals(7)))) || (((Convert.ToInt32(row["id"].ToString()).Equals(8) || Convert.ToInt32(row["id"].ToString()).Equals(9)) || (Convert.ToInt32(row["id"].ToString()).Equals(10) || Convert.ToInt32(row["id"].ToString()).Equals(11))) || ((Convert.ToInt32(row["id"].ToString()).Equals(12) || Convert.ToInt32(row["id"].ToString()).Equals(13)) || Convert.ToInt32(row["id"].ToString()).Equals(14))))
                {
                    this.sltString = this.sltString + string.Format("<option value='{0}'>{1}</option>", row["id"].ToString(), "快彩");
                    break;
                }
            }
            foreach (DataRow row2 in this.lotteryDT.Rows)
            {
                if (Convert.ToInt32(row2["id"].ToString()).Equals(100))
                {
                    this.sltString = this.sltString + string.Format("<option value='{0}' selected=selected>{1}</option>", 100, "香港⑥合彩");
                    break;
                }
            }
            this.model = CallBLL.cz_system_set_six_bll.GetSystemSet(100);
            this.singleNumberSet = this.model.get_single_number_isdown().ToString();
            this.abAcountSet = this.model.get_is_tmab().ToString();
            this.lmGroupTable = CallBLL.cz_lm_group_set_bll.GetList(100, "91060,91061,91062,91063,91064,91065");
            int num18 = 1;
            this.drawbackAutoSet = CallBLL.cz_drawback_auto_set_bll.GetDrawbackAutoSet(num18.ToString()).Rows[0]["flag"].ToString();
            this.negativeSale = CallBLL.cz_users_bll.GetZJInfo().Rows[0]["negative_sale"].ToString();
            if (LSRequest.qq("submitHdn").Equals("submit"))
            {
                if (base.IsChildSync())
                {
                    base.Response.Redirect("/MessagePage.aspx?code=u100080&url=&issuccess=1&isback=0");
                }
                this.UpdateDrawbackAutoSet();
                this.UpdateZJNegativeSale(ref this.isOut);
                this.UpdateData();
            }
        }

        private void UpdateData()
        {
            this.ValidInput();
            cz_system_set_six _six = new cz_system_set_six();
            _six.set_lottery_type(100);
            _six.set_add_time(new DateTime?(DateTime.Now));
            _six.set_ev_0(LSRequest.qq("ev_0").Trim());
            _six.set_ev_1(LSRequest.qq("ev_1").Trim());
            _six.set_ev_2(LSRequest.qq("ev_2").Trim());
            _six.set_ev_3(LSRequest.qq("ev_3").Trim());
            _six.set_ev_4(LSRequest.qq("ev_4").Trim());
            _six.set_ev_5(LSRequest.qq("ev_5").Trim());
            _six.set_ev_6(LSRequest.qq("ev_6").Trim());
            _six.set_ev_7(LSRequest.qq("ev_7").Trim());
            _six.set_ev_8(LSRequest.qq("ev_8").Trim());
            _six.set_ev_9(LSRequest.qq("ev_9").Trim());
            _six.set_ev_10(LSRequest.qq("ev_10").Trim());
            _six.set_ev_11(LSRequest.qq("ev_11").Trim());
            _six.set_ev_12(LSRequest.qq("ev_12").Trim());
            _six.set_ev_13(LSRequest.qq("ev_13").Trim());
            _six.set_ev_14(LSRequest.qq("ev_14").Trim());
            _six.set_ev_15(LSRequest.qq("ev_15").Trim());
            _six.set_ev_16(LSRequest.qq("ev_16").Trim());
            _six.set_ev_17(LSRequest.qq("ev_17").Trim());
            _six.set_ev_18(LSRequest.qq("ev_18").Trim());
            _six.set_single_number_isdown(new int?(int.Parse(LSRequest.qq("singleNumberSet"))));
            _six.set_is_tmab(new int?(int.Parse(LSRequest.qq("abamountSet"))));
            CallBLL.cz_system_set_six_bll.SetSystemSet(_six);
            base.sys_set_log(this.model, CallBLL.cz_system_set_six_bll.GetSystemSet(100));
            this.GetLmGroupCount();
            string url = "/SystemSet/SystemSet_six.aspx?lid=" + this.lotteryId;
            base.Response.Write(base.ShowDialogBox("系統初始化參數保存成功！", url, 0));
            if (this.isRestUserAgent)
            {
                FileCacheHelper.UpdateWebconfig(0);
            }
            if (this.isOut)
            {
                this.OutZJ();
            }
            base.Response.End();
        }

        private void UpdateDrawbackAutoSet()
        {
            string str = LSRequest.qq("drawbackAutoSet");
            if ((str != "0") && (str != "1"))
            {
                base.Response.Write(base.ShowDialogBox(string.Format("繫統初始設定‘{0}’错误！", "現金代理賺水返還模式"), null, 400));
                base.Response.End();
            }
            if (!str.Equals(this.drawbackAutoSet))
            {
                cz_drawback_auto_set _set = new cz_drawback_auto_set();
                _set.set_flag(Convert.ToInt32(str));
                _set.set_master_id(1);
                if (CallBLL.cz_drawback_auto_set_bll.Update(_set))
                {
                    string str2 = base.get_master_name();
                    string str3 = base.get_children_name();
                    string str4 = "";
                    string category = "";
                    string str6 = "";
                    string str7 = "";
                    string str8 = "";
                    string str9 = "";
                    int num = 0;
                    string note = "系統參數設定";
                    string act = "系統參數設定";
                    int num2 = Convert.ToInt32((LSEnums.LogTypeID) 1);
                    string str12 = "";
                    str4 = "香港⑥合彩";
                    str8 = str8 + " 現金代理賺水返還模式: " + this.drawbackAutoSet;
                    str9 = str9 + " 現金代理賺水返還模式: " + str;
                    List<SqlParameter> paramList = new List<SqlParameter>();
                    string str13 = base.add_sys_log(str2, str3, category, str6, str7, str4, str12, act, num, str8, str9, note, num2, 100, ref paramList);
                    CallBLL.cz_system_log_bll.executte_sql(str13.Replace(",0)", ",null)"), paramList.ToArray());
                }
            }
        }

        private void UpdateZJNegativeSale(ref bool isOut)
        {
            if (this.userModel.get_u_type().Equals("zj") && (this.userModel.get_users_child_session() == null))
            {
                string str = LSRequest.qq("negativeSale");
                if (!str.Equals("0") && !str.Equals("1"))
                {
                    base.Response.End();
                }
                else if (!str.Equals(this.negativeSale) && (CallBLL.cz_users_bll.UpdateZJNegativeSale(str) > 0))
                {
                    base.sys_set_log(this.userModel.get_u_name(), this.negativeSale, str);
                    isOut = true;
                }
            }
        }

        private void ValidInput()
        {
            for (int i = 0; i < 0x12; i++)
            {
                if (string.IsNullOrEmpty(LSRequest.qq("ev_" + i.ToString())))
                {
                    base.Response.Write(base.ShowDialogBox("繫統初始設定不能為空！", null, 400));
                    base.Response.End();
                }
            }
            Regex regex = new Regex(@"^[+-]?\d+(\.\d+)?$");
            Regex regex2 = new Regex(@"^((20|21|22|23|[0-1]?\d):[0-5]?\d:[0-5]?\d)$");
            for (int j = 0; j < 0x12; j++)
            {
                switch (j)
                {
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                        if (!regex2.IsMatch(LSRequest.qq("ev_" + j.ToString())))
                        {
                            base.Response.Write(base.ShowDialogBox("繫統初始設定‘封盤時間’格式不正確！", null, 400));
                            base.Response.End();
                        }
                        break;

                    default:
                        if (!regex.IsMatch(LSRequest.qq("ev_" + j.ToString())))
                        {
                            base.Response.Write(base.ShowDialogBox("繫統初始設定數據格式不正確！", null, 400));
                            base.Response.End();
                        }
                        break;
                }
            }
            string str = LSRequest.qq("ev_18");
            if (!string.IsNullOrEmpty(str))
            {
                DateTime time;
                if (!DateTime.TryParse(str, out time))
                {
                    base.Response.Write(base.ShowDialogBox("繫統初始設定‘報表查詢起始日期’格式不正確！", null, 400));
                    base.Response.End();
                }
            }
            else
            {
                base.Response.Write(base.ShowDialogBox("繫統初始設定‘報表查詢起始日期’不能為空！", null, 400));
                base.Response.End();
            }
            string str2 = LSRequest.qq("singleNumberSet");
            int num5 = 0;
            if (!str2.Equals(num5.ToString()))
            {
                int num6 = 1;
                if (!str2.Equals(num6.ToString()))
                {
                    base.Response.Write(base.ShowDialogBox("連碼項降賠方式錯誤！", null, 400));
                    base.Response.End();
                }
            }
            string str3 = LSRequest.qq("abamountSet");
            int num7 = 0;
            if (!str3.Equals(num7.ToString()))
            {
                int num8 = 1;
                if (!str3.Equals(num8.ToString()))
                {
                    base.Response.Write(base.ShowDialogBox("特碼項降賠方式(貨量)錯誤！", null, 400));
                    base.Response.End();
                }
            }
            for (int k = 0; k < this.lmGroupTable.Rows.Count; k++)
            {
                string str4 = this.lmGroupTable.Rows[k]["play_id"].ToString();
                string str5 = this.lmGroupTable.Rows[k]["play_name"].ToString();
                string s = LSRequest.qq(str4 + "_currentnumcount");
                string str7 = this.lmGroupTable.Rows[k]["max_num_count"].ToString();
                string str8 = this.lmGroupTable.Rows[k]["min_num_count"].ToString();
                if (!Utils.IsPureNumeric(s))
                {
                    base.Response.Write(base.ShowDialogBox(string.Format("繫統初始設定‘{0}’格式不正確！", str5), null, 400));
                    base.Response.End();
                }
                int num4 = int.Parse(s);
                if (num4 < int.Parse(str8))
                {
                    base.Response.Write(base.ShowDialogBox(string.Format("繫統初始設定‘{0}’設置當前號碼數量不能小於{1}！", str5, str8), null, 400));
                    base.Response.End();
                }
                if (num4 > int.Parse(str7))
                {
                    base.Response.Write(base.ShowDialogBox(string.Format("繫統初始設定‘{0}’設置當前號碼數量不能大於{1}！", str5, str7), null, 400));
                    base.Response.End();
                }
            }
        }
    }
}

