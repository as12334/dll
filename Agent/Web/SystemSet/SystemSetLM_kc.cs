namespace Agent.Web.SystemSet
{
    using Agent.Web.WebBase;
    using LotterySystem.Common;
    using LotterySystem.Model;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class SystemSetLM_kc : MemberPageBase
    {
        protected bool childModify = true;
        protected Dictionary<int, DataTable> lmGroupDict = new Dictionary<int, DataTable>();
        protected DataTable lmGroupTable_jssfc;
        protected DataTable lmGroupTable_kl10;
        protected DataTable lmGroupTable_pcdd;
        protected DataTable lmGroupTable_xync;
        protected DataTable lotteryDT;

        public int combination(int b, int a)
        {
            return (this.factorial(b) / (this.factorial(a) * this.factorial(b - a)));
        }

        public int factorial(int a)
        {
            if (a < 2)
            {
                return 1;
            }
            return (a * this.factorial(a - 1));
        }

        private List<cz_lm_group_set> GetSqlModel(int lottery_type, DataTable lmGroupTable)
        {
            List<cz_lm_group_set> list = new List<cz_lm_group_set>();
            foreach (DataRow row in lmGroupTable.Rows)
            {
                int num = 0;
                string s = row["play_id"].ToString();
                string str2 = row["min_num_count"].ToString();
                string str3 = LSRequest.qq(string.Concat(new object[] { lottery_type, "_", s, "_currentnumcount" }));
                if (!string.IsNullOrEmpty(str3))
                {
                    int b = int.Parse(str3);
                    num = this.combination(b, int.Parse(str2));
                    cz_lm_group_set item = new cz_lm_group_set();
                    item.set_play_id(int.Parse(s));
                    item.set_max_num_group(num);
                    item.set_current_num_count(b);
                    item.set_lottery_id(lottery_type);
                    list.Add(item);
                }
            }
            return list;
        }

        private void InitLmGroup()
        {
            DataRow[] rowArray = this.lotteryDT.Select(string.Format(" id={0}", 0));
            DataRow[] rowArray2 = this.lotteryDT.Select(string.Format(" id={0}", 3));
            DataRow[] rowArray3 = this.lotteryDT.Select(string.Format(" id={0}", 7));
            DataRow[] rowArray4 = this.lotteryDT.Select(string.Format(" id={0}", 14));
            if (rowArray.Length > 0)
            {
                this.lmGroupTable_kl10 = CallBLL.cz_lm_group_set_bll.GetListKC(0);
                this.lmGroupDict.Add(0, this.lmGroupTable_kl10);
            }
            if (rowArray2.Length > 0)
            {
                this.lmGroupTable_xync = CallBLL.cz_lm_group_set_bll.GetListKC(3);
                this.lmGroupDict.Add(3, this.lmGroupTable_xync);
            }
            if (rowArray3.Length > 0)
            {
                this.lmGroupTable_pcdd = CallBLL.cz_lm_group_set_bll.GetListKC(7);
                this.lmGroupDict.Add(7, this.lmGroupTable_pcdd);
            }
            if (rowArray4.Length > 0)
            {
                this.lmGroupTable_jssfc = CallBLL.cz_lm_group_set_bll.GetListKC(14);
                this.lmGroupDict.Add(14, this.lmGroupTable_jssfc);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.lotteryDT = base.GetLotteryList();
            this.InitLmGroup();
            if (LSRequest.qq("submitHdn").Equals("submit"))
            {
                this.UpdateLmGroup();
            }
        }

        private void UpdateLmGroup()
        {
            if (((this.lmGroupTable_kl10 != null) || (this.lmGroupTable_xync != null)) || ((this.lmGroupTable_pcdd != null) || (this.lmGroupTable_jssfc != null)))
            {
                if (this.lmGroupTable_kl10 != null)
                {
                    this.ValidLmGroup(0, this.lmGroupTable_kl10);
                }
                if (this.lmGroupTable_xync != null)
                {
                    this.ValidLmGroup(3, this.lmGroupTable_xync);
                }
                if (this.lmGroupTable_pcdd != null)
                {
                    this.ValidLmGroup(7, this.lmGroupTable_pcdd);
                }
                if (this.lmGroupTable_jssfc != null)
                {
                    this.ValidLmGroup(14, this.lmGroupTable_jssfc);
                }
                List<cz_lm_group_set> list = new List<cz_lm_group_set>();
                if (this.lmGroupTable_kl10 != null)
                {
                    list.AddRange(this.GetSqlModel(0, this.lmGroupTable_kl10));
                }
                if (this.lmGroupTable_xync != null)
                {
                    list.AddRange(this.GetSqlModel(3, this.lmGroupTable_xync));
                }
                if (this.lmGroupTable_pcdd != null)
                {
                    list.AddRange(this.GetSqlModel(7, this.lmGroupTable_pcdd));
                }
                if (this.lmGroupTable_jssfc != null)
                {
                    list.AddRange(this.GetSqlModel(14, this.lmGroupTable_jssfc));
                }
                if (list.Count > 0)
                {
                    if (CallBLL.cz_lm_group_set_bll.Update(list) > 0)
                    {
                        bool flag = true;
                        if (this.lmGroupTable_kl10 != null)
                        {
                            DataTable listKC = CallBLL.cz_lm_group_set_bll.GetListKC(0);
                            if (flag)
                            {
                                flag = Utils.CompareDataTable(this.lmGroupTable_kl10, listKC);
                            }
                            base.lm_group_set_log_kc(this.lmGroupTable_kl10, listKC, 0);
                        }
                        if (this.lmGroupTable_xync != null)
                        {
                            DataTable newDT = CallBLL.cz_lm_group_set_bll.GetListKC(3);
                            if (flag)
                            {
                                flag = Utils.CompareDataTable(this.lmGroupTable_xync, newDT);
                            }
                            base.lm_group_set_log_kc(this.lmGroupTable_xync, newDT, 3);
                        }
                        if (this.lmGroupTable_pcdd != null)
                        {
                            DataTable table3 = CallBLL.cz_lm_group_set_bll.GetListKC(7);
                            if (flag)
                            {
                                flag = Utils.CompareDataTable(this.lmGroupTable_pcdd, table3);
                            }
                            base.lm_group_set_log_kc(this.lmGroupTable_pcdd, table3, 7);
                        }
                        if (this.lmGroupTable_jssfc != null)
                        {
                            DataTable table4 = CallBLL.cz_lm_group_set_bll.GetListKC(14);
                            if (flag)
                            {
                                flag = Utils.CompareDataTable(this.lmGroupTable_jssfc, table4);
                            }
                            base.lm_group_set_log_kc(this.lmGroupTable_jssfc, table4, 14);
                        }
                        if (!flag)
                        {
                            FileCacheHelper.UpdateWebconfig(0);
                        }
                        base.Response.Write(base.ShowDialogBox("快彩連碼設置成功！", null, 400));
                        base.Response.End();
                    }
                    else
                    {
                        base.Response.Write(base.ShowDialogBox("快彩連碼設置失敗！", null, 400));
                        base.Response.End();
                    }
                }
            }
        }

        private void ValidLmGroup(int lottery_type, DataTable lmGroupTable)
        {
            foreach (DataRow row in lmGroupTable.Rows)
            {
                string str = row["play_id"].ToString();
                string s = row["min_num_count"].ToString();
                string str3 = row["max_num_count"].ToString();
                string str5 = string.Format("({0})" + row["play_name"].ToString(), base.GetGameNameByID(lottery_type.ToString()));
                string str6 = LSRequest.qq(string.Concat(new object[] { lottery_type, "_", str, "_currentnumcount" }));
                if (!string.IsNullOrEmpty(str6))
                {
                    if (!Utils.IsPureNumeric(str6.ToString()))
                    {
                        base.Response.Write(base.ShowDialogBox(string.Format("繫統初始設定‘{0}’格式不正確！", str5), null, 400));
                        base.Response.End();
                    }
                    if (int.Parse(str6) < int.Parse(s))
                    {
                        base.Response.Write(base.ShowDialogBox(string.Format("繫統初始設定‘{0}’設置當前連碼不能小於{1}！", str5, s), null, 400));
                        base.Response.End();
                    }
                    if (int.Parse(str6) > int.Parse(str3))
                    {
                        base.Response.Write(base.ShowDialogBox(string.Format("繫統初始設定‘{0}’設置當前連碼不能大於{1}！", str5, str3), null, 400));
                        base.Response.End();
                    }
                }
            }
        }
    }
}

