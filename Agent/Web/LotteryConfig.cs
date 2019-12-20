namespace Agent.Web
{
    using Agent.Web.WebBase;
    using LotterySystem.Common;
    using LotterySystem.Model;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class LotteryConfig : MemberPageBase
    {
        protected bool childModify = true;
        protected DataTable lotteryDT;
        protected DataTable lotteryDT_ZJ;

        private void InsertLottery(string lotteryIds)
        {
            if (string.IsNullOrEmpty(lotteryIds))
            {
                base.Response.Write(base.ShowDialogBox("彩種最少選擇一項！", null, 400));
                base.Response.End();
            }
            string str = LSRequest.qq("chk_kl8_night");
            string str2 = LSRequest.qq("chk_k8sc_night");
            string str3 = LSRequest.qq("chk_pcdd_night");
            if (string.IsNullOrEmpty(str))
            {
                str = "";
            }
            if (string.IsNullOrEmpty(str2))
            {
                str2 = "";
            }
            if (string.IsNullOrEmpty(str3))
            {
                str3 = "";
            }
            DataTable oldDT = CallBLL.cz_lottery_config_bll.GetList();
            lotteryIds.Split(new char[] { ',' });
            List<string> list = new List<string>();
            string str4 = "";
            string str5 = "";
            string str6 = "";
            foreach (DataRow row in oldDT.Rows)
            {
                string item = row["id"].ToString();
                string str8 = row["is_display"].ToString();
                string str9 = row["night_flag"].ToString();
                if (str8 == "1")
                {
                    list.Add(item);
                    int num2 = 5;
                    if (item == num2.ToString())
                    {
                        str4 = str9;
                    }
                    int num3 = 6;
                    if (item == num3.ToString())
                    {
                        str5 = str9;
                    }
                    int num4 = 7;
                    if (item == num4.ToString())
                    {
                        str6 = str9;
                    }
                }
            }
            string str10 = string.Join(",", list.ToArray());
            bool flag = true;
            bool flag2 = true;
            bool flag3 = true;
            if (str10 == lotteryIds)
            {
                if (this.is_in_time())
                {
                    if (!string.IsNullOrEmpty(str4))
                    {
                        if (str4 == str)
                        {
                            flag = false;
                        }
                        else if ((str == "") && (str4 == "0"))
                        {
                            flag = false;
                        }
                    }
                    else
                    {
                        flag = false;
                    }
                    if (!string.IsNullOrEmpty(str5))
                    {
                        if (str5 == str2)
                        {
                            flag2 = false;
                        }
                        else if ((str2 == "") && (str5 == "0"))
                        {
                            flag2 = false;
                        }
                    }
                    else
                    {
                        flag2 = false;
                    }
                    if (!string.IsNullOrEmpty(str6))
                    {
                        if (str6 == str3)
                        {
                            flag3 = false;
                        }
                        else if ((str3 == "") && (str6 == "0"))
                        {
                            flag3 = false;
                        }
                    }
                    else
                    {
                        flag3 = false;
                    }
                }
                else
                {
                    flag = false;
                    flag2 = false;
                    flag3 = false;
                }
            }
            if ((!flag && !flag2) && !flag3)
            {
                base.Response.Write(base.ShowDialogBox("無變動彩種！", "", 400));
                base.Response.End();
            }
            Dictionary<int, string> dictionary = new Dictionary<int, string>();
            if (this.is_in_time())
            {
                string str11 = "";
                switch (str)
                {
                    case "1":
                        str11 = "1";
                        break;

                    case "":
                        str11 = "0";
                        break;
                }
                dictionary.Add(5, str11);
                string str12 = "";
                if (str2 == "1")
                {
                    str12 = "1";
                }
                else if (str2 == "")
                {
                    str12 = "0";
                }
                dictionary.Add(6, str12);
                string str13 = "";
                if (str3 == "1")
                {
                    str13 = "1";
                }
                else if (str3 == "")
                {
                    str13 = "0";
                }
                dictionary.Add(7, str13);
            }
            if (CallBLL.cz_lottery_config_bll.InsertLottery(lotteryIds, dictionary) > 0)
            {
                base.czsd_log(oldDT, CallBLL.cz_lottery_config_bll.GetList());
                FileCacheHelper.UpdateWebconfig(0);
                string url = "lotteryconfig.aspx";
                base.Response.Write(base.ShowDialogBox("配置彩種成功！,系統正在退出，請重新登錄！！", url, 0));
                base.Response.End();
            }
            else
            {
                base.Response.Write(base.ShowDialogBox("配置彩種失敗！", "", 400));
                base.Response.End();
            }
        }

        protected bool is_in_time()
        {
            bool flag = false;
            DateTime time = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 06:10:00");
            DateTime time2 = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 08:00:00");
            DateTime now = DateTime.Now;
            if ((DateTime.Compare(time, now) <= 0) && (DateTime.Compare(now, time2) <= 0))
            {
                flag = true;
            }
            return flag;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string str = this.Session["user_name"].ToString();
            agent_userinfo_session model = this.Session[str + "lottery_session_user_info"] as agent_userinfo_session;
            if (!model.get_u_type().Equals("zj"))
            {
                base.Response.Redirect("/MessagePage.aspx?code=u100057&url=&issuccess=1&isback=0");
                base.Response.End();
            }
            base.Permission_Aspx_ZJ(model, "po_2_2");
            this.lotteryDT = CallBLL.cz_lottery_bll.GetListByConfig().Tables[0];
            this.lotteryDT_ZJ = CallBLL.cz_lottery_config_bll.GetAdminList();
            if (LSRequest.qq("savelottery").Equals("savelottery"))
            {
                if (base.IsChildSync())
                {
                    base.Response.Redirect("/MessagePage.aspx?code=u100080&url=&issuccess=1&isback=0");
                }
                string lotteryIds = LSRequest.qq("chk_lottery").Trim();
                this.InsertLottery(lotteryIds);
            }
        }
    }
}

