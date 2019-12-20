namespace Agent.Web
{
    using Agent.Web.WebBase;
    using LotterySystem.Common;
    using LotterySystem.Model;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Text.RegularExpressions;

    public class SystemSet_kc : MemberPageBase
    {
        protected SystemSetControl CAR168Control;
        protected bool childModify = true;
        protected SystemSetControl CQSCControl;
        protected cz_system_set_kc_ex cz_system_set_kc_exModel;
        protected DataRow[] down_car168Rows;
        protected DataRow[] down_cqscRows;
        protected DataRow[] down_happycarRows;
        protected DataRow[] down_jscarRows;
        protected DataRow[] down_jscqscRows;
        protected DataRow[] down_jsft2Rows;
        protected DataRow[] down_jsk3Rows;
        protected DataRow[] down_jspk10Rows;
        protected DataRow[] down_jssfcRows;
        protected DataRow[] down_k8scRows;
        protected DataRow[] down_kl10Rows;
        protected DataRow[] down_kl8Rows;
        protected DataRow[] down_pcddRows;
        protected DataRow[] down_pk10Rows;
        protected DataRow[] down_pkbjlRows;
        protected DataRow[] down_sixRows;
        protected DataRow[] down_speed5Rows;
        protected DataRow[] down_ssc168Rows;
        protected DataRow[] down_vrcarRows;
        protected DataRow[] down_vrsscRows;
        protected DataRow[] down_xyft5Rows;
        protected DataRow[] down_xyftoaRows;
        protected DataRow[] down_xyftsgRows;
        protected DataRow[] down_xyncRows;
        protected DataRow[] down_yl_jssfcRows;
        protected DataRow[] down_yl_kl10Rows;
        protected DataRow[] down_yl_xyncRows;
        protected string drawbackAutoSet = "0";
        protected SystemSetControl HAPPYCARControl;
        protected string isNight = "0";
        protected string isNightStr = "display:none;";
        private bool isRestUserAgent;
        protected SystemSetControl JSCARControl;
        protected SystemSetControl JSCQSCControl;
        protected SystemSetControl JSFT2Control;
        protected SystemSetControl JSK3Control;
        protected SystemSetControl JSPK10Control;
        protected SystemSetControl JSSFCControl;
        protected SystemSetControl K8SCControl;
        protected SystemSetControl KL10Control;
        protected SystemSetControl KL8Control;
        private string l_ids = "";
        protected DataTable lmGroupTable;
        protected DataTable lmGroupTable_jssfc;
        protected DataTable lmGroupTable_kl10;
        protected DataTable lmGroupTable_pcdd;
        protected DataTable lmGroupTable_xync;
        protected DataTable lotteryDT;
        protected string lotteryId = "";
        protected string lotterys = "";
        private DataTable old_down_dt;
        private DataTable old_param_dt;
        protected SystemSetControl PCDDControl;
        protected SystemSetControl PK10Control;
        protected SystemSetControl PKBJLControl;
        protected string skin = "";
        protected string sltString = "";
        protected SystemSetControl SPEED5Control;
        protected SystemSetControl SSC168Control;
        protected DataRow[] system_car168Rows;
        protected DataRow[] system_cqscRows;
        protected DataRow[] system_happycarRows;
        protected DataRow[] system_jscarRows;
        protected DataRow[] system_jscqscRows;
        protected DataRow[] system_jsft2Rows;
        protected DataRow[] system_jsk3Rows;
        protected DataRow[] system_jspk10Rows;
        protected DataRow[] system_jssfcRows;
        protected DataRow[] system_k8scRows;
        protected DataRow[] system_kl10Rows;
        protected DataRow[] system_kl8Rows;
        protected DataRow[] system_pcddRows;
        protected DataRow[] system_pk10Rows;
        protected DataRow[] system_pkbjlRows;
        protected DataRow[] system_sixRows;
        protected DataRow[] system_speed5Rows;
        protected DataRow[] system_ssc168Rows;
        protected DataRow[] system_vrcarRows;
        protected DataRow[] system_vrsscRows;
        protected DataRow[] system_xyft5Rows;
        protected DataRow[] system_xyftoaRows;
        protected DataRow[] system_xyftsgRows;
        protected DataRow[] system_xyncRows;
        protected DataRow[] system_yl_jssfcRows;
        protected DataRow[] system_yl_kl10Rows;
        protected DataRow[] system_yl_xyncRows;
        protected SystemSetControl VRCARControl;
        protected SystemSetControl VRSSCControl;
        protected SystemSetControl XYFT5Control;
        protected SystemSetControl XYFTOAControl;
        protected SystemSetControl XYFTSGControl;
        protected SystemSetControl XYNCControl;
        protected SystemSetControl ylJSSFCControl;
        protected SystemSetControl ylKL10Control;
        protected SystemSetControl ylXYNCControl;

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

        private void InitData()
        {
            string str = "";
            for (int i = 0; i < this.lotteryDT.Rows.Count; i++)
            {
                if (i == 0)
                {
                    str = str + this.lotteryDT.Rows[i]["id"].ToString();
                }
                else
                {
                    str = str + "," + this.lotteryDT.Rows[i]["id"].ToString();
                }
            }
            DataTable systemSet = CallBLL.cz_system_set_kc_bll.GetSystemSet(str);
            this.old_param_dt = systemSet;
            this.l_ids = str;
            DataTable downoddsSet = CallBLL.cz_downodds_set_kc_bll.GetDownoddsSet(str);
            this.old_down_dt = downoddsSet;
            for (int j = 0; j < this.lotteryDT.Rows.Count; j++)
            {
                switch (Convert.ToInt32(this.lotteryDT.Rows[j]["id"].ToString()))
                {
                    case 0:
                    {
                        int num5 = 0;
                        this.system_kl10Rows = systemSet.Select(string.Format("lottery_type={0} ", num5.ToString()));
                        int num6 = 0;
                        this.down_kl10Rows = downoddsSet.Select(string.Format("lottery_type={0} ", num6.ToString()));
                        int num7 = 0;
                        this.system_yl_kl10Rows = systemSet.Select(string.Format("lottery_type={0} ", num7.ToString()));
                        int num8 = 0;
                        this.down_yl_kl10Rows = downoddsSet.Select(string.Format("lottery_type={0} ", num8.ToString()));
                        break;
                    }
                    case 1:
                    {
                        int num9 = 1;
                        this.system_cqscRows = systemSet.Select(string.Format("lottery_type={0}", num9.ToString()));
                        int num10 = 1;
                        this.down_cqscRows = downoddsSet.Select(string.Format("lottery_type={0}", num10.ToString()));
                        break;
                    }
                    case 2:
                    {
                        int num11 = 2;
                        this.system_pk10Rows = systemSet.Select(string.Format("lottery_type={0}", num11.ToString()));
                        int num12 = 2;
                        this.down_pk10Rows = downoddsSet.Select(string.Format("lottery_type={0}", num12.ToString()));
                        break;
                    }
                    case 3:
                    {
                        int num13 = 3;
                        this.system_xyncRows = systemSet.Select(string.Format("lottery_type={0}", num13.ToString()));
                        int num14 = 3;
                        this.down_xyncRows = downoddsSet.Select(string.Format("lottery_type={0}", num14.ToString()));
                        int num15 = 3;
                        this.system_yl_xyncRows = systemSet.Select(string.Format("lottery_type={0} ", num15.ToString()));
                        int num16 = 3;
                        this.down_yl_xyncRows = downoddsSet.Select(string.Format("lottery_type={0} ", num16.ToString()));
                        break;
                    }
                    case 4:
                    {
                        int num17 = 4;
                        this.system_jsk3Rows = systemSet.Select(string.Format("lottery_type={0}", num17.ToString()));
                        int num18 = 4;
                        this.down_jsk3Rows = downoddsSet.Select(string.Format("lottery_type={0}", num18.ToString()));
                        break;
                    }
                    case 5:
                    {
                        int num19 = 5;
                        this.system_kl8Rows = systemSet.Select(string.Format("lottery_type={0}", num19.ToString()));
                        int num20 = 5;
                        this.down_kl8Rows = downoddsSet.Select(string.Format("lottery_type={0}", num20.ToString()));
                        if ((this.lotteryDT.Rows[j]["isnight"] != null) && this.lotteryDT.Rows[j]["isnight"].ToString().Equals("1"))
                        {
                            this.isNight = "1";
                            this.isNightStr = "display:";
                        }
                        break;
                    }
                    case 6:
                    {
                        int num21 = 6;
                        this.system_k8scRows = systemSet.Select(string.Format("lottery_type={0}", num21.ToString()));
                        int num22 = 6;
                        this.down_k8scRows = downoddsSet.Select(string.Format("lottery_type={0}", num22.ToString()));
                        break;
                    }
                    case 7:
                    {
                        int num23 = 7;
                        this.system_pcddRows = systemSet.Select(string.Format("lottery_type={0}", num23.ToString()));
                        int num24 = 7;
                        this.down_pcddRows = downoddsSet.Select(string.Format("lottery_type={0}", num24.ToString()));
                        break;
                    }
                    case 9:
                    {
                        int num25 = 9;
                        this.system_xyft5Rows = systemSet.Select(string.Format("lottery_type={0}", num25.ToString()));
                        int num26 = 9;
                        this.down_xyft5Rows = downoddsSet.Select(string.Format("lottery_type={0}", num26.ToString()));
                        break;
                    }
                    case 10:
                    {
                        int num27 = 10;
                        this.system_jscarRows = systemSet.Select(string.Format("lottery_type={0}", num27.ToString()));
                        int num28 = 10;
                        this.down_jscarRows = downoddsSet.Select(string.Format("lottery_type={0}", num28.ToString()));
                        break;
                    }
                    case 11:
                    {
                        int num29 = 11;
                        this.system_speed5Rows = systemSet.Select(string.Format("lottery_type={0}", num29.ToString()));
                        int num30 = 11;
                        this.down_speed5Rows = downoddsSet.Select(string.Format("lottery_type={0}", num30.ToString()));
                        break;
                    }
                    case 12:
                    {
                        int num33 = 12;
                        this.system_jspk10Rows = systemSet.Select(string.Format("lottery_type={0}", num33.ToString()));
                        int num34 = 12;
                        this.down_jspk10Rows = downoddsSet.Select(string.Format("lottery_type={0}", num34.ToString()));
                        break;
                    }
                    case 13:
                    {
                        int num31 = 13;
                        this.system_jscqscRows = systemSet.Select(string.Format("lottery_type={0}", num31.ToString()));
                        int num32 = 13;
                        this.down_jscqscRows = downoddsSet.Select(string.Format("lottery_type={0}", num32.ToString()));
                        break;
                    }
                    case 14:
                    {
                        int num35 = 14;
                        this.system_jssfcRows = systemSet.Select(string.Format("lottery_type={0} ", num35.ToString()));
                        int num36 = 14;
                        this.down_jssfcRows = downoddsSet.Select(string.Format("lottery_type={0} ", num36.ToString()));
                        int num37 = 14;
                        this.system_yl_jssfcRows = systemSet.Select(string.Format("lottery_type={0} ", num37.ToString()));
                        int num38 = 14;
                        this.down_yl_jssfcRows = downoddsSet.Select(string.Format("lottery_type={0} ", num38.ToString()));
                        break;
                    }
                    case 15:
                    {
                        int num39 = 15;
                        this.system_jsft2Rows = systemSet.Select(string.Format("lottery_type={0}", num39.ToString()));
                        int num40 = 15;
                        this.down_jsft2Rows = downoddsSet.Select(string.Format("lottery_type={0}", num40.ToString()));
                        break;
                    }
                    case 0x10:
                    {
                        int num41 = 0x10;
                        this.system_car168Rows = systemSet.Select(string.Format("lottery_type={0}", num41.ToString()));
                        int num42 = 0x10;
                        this.down_car168Rows = downoddsSet.Select(string.Format("lottery_type={0}", num42.ToString()));
                        break;
                    }
                    case 0x11:
                    {
                        int num43 = 0x11;
                        this.system_ssc168Rows = systemSet.Select(string.Format("lottery_type={0}", num43.ToString()));
                        int num44 = 0x11;
                        this.down_ssc168Rows = downoddsSet.Select(string.Format("lottery_type={0}", num44.ToString()));
                        break;
                    }
                    case 0x12:
                    {
                        int num45 = 0x12;
                        this.system_vrcarRows = systemSet.Select(string.Format("lottery_type={0}", num45.ToString()));
                        int num46 = 0x12;
                        this.down_vrcarRows = downoddsSet.Select(string.Format("lottery_type={0}", num46.ToString()));
                        break;
                    }
                    case 0x13:
                    {
                        int num47 = 0x13;
                        this.system_vrsscRows = systemSet.Select(string.Format("lottery_type={0}", num47.ToString()));
                        int num48 = 0x13;
                        this.down_vrsscRows = downoddsSet.Select(string.Format("lottery_type={0}", num48.ToString()));
                        break;
                    }
                    case 20:
                    {
                        int num49 = 20;
                        this.system_xyftoaRows = systemSet.Select(string.Format("lottery_type={0}", num49.ToString()));
                        int num50 = 20;
                        this.down_xyftoaRows = downoddsSet.Select(string.Format("lottery_type={0}", num50.ToString()));
                        break;
                    }
                    case 0x15:
                    {
                        int num51 = 0x15;
                        this.system_xyftsgRows = systemSet.Select(string.Format("lottery_type={0}", num51.ToString()));
                        int num52 = 0x15;
                        this.down_xyftsgRows = downoddsSet.Select(string.Format("lottery_type={0}", num52.ToString()));
                        break;
                    }
                    case 0x16:
                    {
                        int num53 = 0x16;
                        this.system_happycarRows = systemSet.Select(string.Format("lottery_type={0}", num53.ToString()));
                        int num54 = 0x16;
                        this.down_happycarRows = downoddsSet.Select(string.Format("lottery_type={0}", num54.ToString()));
                        break;
                    }
                }
            }
        }

        private void InitDateEX()
        {
            this.cz_system_set_kc_exModel = CallBLL.cz_system_set_kc_ex_bll.GetModel(1);
        }

        private void InitLmGroup()
        {
            Dictionary<int, DataTable> dictionary = new Dictionary<int, DataTable>();
            DataRow[] rowArray = this.lotteryDT.Select(string.Format(" id={0}", 0));
            DataRow[] rowArray2 = this.lotteryDT.Select(string.Format(" id={0}", 3));
            DataRow[] rowArray3 = this.lotteryDT.Select(string.Format(" id={0}", 7));
            DataRow[] rowArray4 = this.lotteryDT.Select(string.Format(" id={0}", 14));
            if (rowArray.Length > 0)
            {
                this.lmGroupTable_kl10 = CallBLL.cz_lm_group_set_bll.GetListKC(0);
                dictionary.Add(0, this.lmGroupTable_kl10);
            }
            if (rowArray2.Length > 0)
            {
                this.lmGroupTable_xync = CallBLL.cz_lm_group_set_bll.GetListKC(3);
                dictionary.Add(3, this.lmGroupTable_xync);
            }
            if (rowArray3.Length > 0)
            {
                this.lmGroupTable_pcdd = CallBLL.cz_lm_group_set_bll.GetListKC(7);
                dictionary.Add(7, this.lmGroupTable_pcdd);
            }
            if (rowArray4.Length > 0)
            {
                this.lmGroupTable_jssfc = CallBLL.cz_lm_group_set_bll.GetListKC(14);
                dictionary.Add(14, this.lmGroupTable_jssfc);
            }
        }

        protected bool IsLotteryKCLmExists()
        {
            int num = 0;
            if (!base.IsLotteryExist(num.ToString()))
            {
                int num2 = 3;
                if (!base.IsLotteryExist(num2.ToString()))
                {
                    int num3 = 7;
                    if (!base.IsLotteryExist(num3.ToString()))
                    {
                        int num4 = 14;
                        if (!base.IsLotteryExist(num4.ToString()))
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            agent_userinfo_session model = this.Session[this.Session["user_name"] + "lottery_session_user_info"] as agent_userinfo_session;
            this.skin = model.get_u_skin();
            if (!model.get_u_type().Equals("zj"))
            {
                base.Response.Redirect("/MessagePage.aspx?code=u100057&url=&issuccess=1&isback=0");
                base.Response.End();
            }
            base.Permission_Aspx_ZJ(model, "po_3_2");
            this.lotteryId = LSRequest.qq("lid");
            this.lotteryDT = base.GetLotteryList();
            base.IsLotteryExist(this.lotteryId, "u100032", "1", "");
            int num = 100;
            if (this.lotteryId.Equals(num.ToString()))
            {
                base.Response.Redirect("/SystemSet/SystemSet_six.aspx?lid=" + this.lotteryId, true);
            }
            foreach (DataRow row in this.lotteryDT.Rows)
            {
                if ((((Convert.ToInt32(row["id"].ToString()).Equals(0) || Convert.ToInt32(row["id"].ToString()).Equals(1)) || (Convert.ToInt32(row["id"].ToString()).Equals(2) || Convert.ToInt32(row["id"].ToString()).Equals(3))) || ((Convert.ToInt32(row["id"].ToString()).Equals(4) || Convert.ToInt32(row["id"].ToString()).Equals(5)) || (Convert.ToInt32(row["id"].ToString()).Equals(6) || Convert.ToInt32(row["id"].ToString()).Equals(7)))) || ((((Convert.ToInt32(row["id"].ToString()).Equals(8) || Convert.ToInt32(row["id"].ToString()).Equals(9)) || (Convert.ToInt32(row["id"].ToString()).Equals(10) || Convert.ToInt32(row["id"].ToString()).Equals(11))) || ((Convert.ToInt32(row["id"].ToString()).Equals(13) || Convert.ToInt32(row["id"].ToString()).Equals(12)) || (Convert.ToInt32(row["id"].ToString()).Equals(14) || Convert.ToInt32(row["id"].ToString()).Equals(15)))) || (((Convert.ToInt32(row["id"].ToString()).Equals(0x10) || Convert.ToInt32(row["id"].ToString()).Equals(0x11)) || (Convert.ToInt32(row["id"].ToString()).Equals(0x12) || Convert.ToInt32(row["id"].ToString()).Equals(0x13))) || ((Convert.ToInt32(row["id"].ToString()).Equals(20) || Convert.ToInt32(row["id"].ToString()).Equals(0x15)) || Convert.ToInt32(row["id"].ToString()).Equals(0x16)))))
                {
                    this.sltString = this.sltString + string.Format("<option value='{0}' selected=selected>{1}</option>", row["id"].ToString(), "快彩");
                    break;
                }
            }
            foreach (DataRow row2 in this.lotteryDT.Rows)
            {
                if (Convert.ToInt32(row2["id"].ToString()).Equals(100))
                {
                    this.sltString = this.sltString + string.Format("<option value='{0}'>{1}</option>", 100, "香港⑥合彩");
                    break;
                }
            }
            this.InitData();
            this.InitDateEX();
            int num26 = 2;
            this.drawbackAutoSet = CallBLL.cz_drawback_auto_set_bll.GetDrawbackAutoSet(num26.ToString()).Rows[0]["flag"].ToString();
            if ((this.down_kl10Rows != null) && (this.down_kl10Rows.Count<DataRow>() > 0))
            {
                this.KL10Control.dataRow = this.down_kl10Rows;
                this.KL10Control.lotteryId = 0;
                this.KL10Control.flagtype = 0;
                this.ylKL10Control.ylSytemDataRow = this.system_yl_kl10Rows;
                this.ylKL10Control.ylDownDataRow = this.down_yl_kl10Rows;
                this.ylKL10Control.lotteryId = 0;
                this.ylKL10Control.flagtype = 1;
                this.ylKL10Control.gameName = base.GetGameNameByID(0.ToString());
            }
            if ((this.down_cqscRows != null) && (this.down_cqscRows.Count<DataRow>() > 0))
            {
                this.CQSCControl.dataRow = this.down_cqscRows;
                this.CQSCControl.lotteryId = 1;
                this.CQSCControl.flagtype = 0;
            }
            if ((this.down_pk10Rows != null) && (this.down_pk10Rows.Count<DataRow>() > 0))
            {
                this.PK10Control.dataRow = this.down_pk10Rows;
                this.PK10Control.lotteryId = 2;
                this.PK10Control.flagtype = 0;
            }
            if ((this.down_xyncRows != null) && (this.down_xyncRows.Count<DataRow>() > 0))
            {
                this.XYNCControl.dataRow = this.down_xyncRows;
                this.XYNCControl.lotteryId = 3;
                this.XYNCControl.flagtype = 0;
                this.ylXYNCControl.ylSytemDataRow = this.system_yl_xyncRows;
                this.ylXYNCControl.ylDownDataRow = this.down_yl_xyncRows;
                this.ylXYNCControl.lotteryId = 3;
                this.ylXYNCControl.flagtype = 1;
                this.ylXYNCControl.gameName = base.GetGameNameByID(3.ToString());
            }
            if ((this.down_jsk3Rows != null) && (this.down_jsk3Rows.Count<DataRow>() > 0))
            {
                this.JSK3Control.dataRow = this.down_jsk3Rows;
                this.JSK3Control.lotteryId = 4;
                this.JSK3Control.flagtype = 0;
            }
            if ((this.down_kl8Rows != null) && (this.down_kl8Rows.Count<DataRow>() > 0))
            {
                this.KL8Control.dataRow = this.down_kl8Rows;
                this.KL8Control.lotteryId = 5;
                this.KL8Control.flagtype = 0;
            }
            if ((this.down_k8scRows != null) && (this.down_k8scRows.Count<DataRow>() > 0))
            {
                this.K8SCControl.dataRow = this.down_k8scRows;
                this.K8SCControl.lotteryId = 6;
                this.K8SCControl.flagtype = 0;
            }
            if ((this.down_pcddRows != null) && (this.down_pcddRows.Count<DataRow>() > 0))
            {
                this.PCDDControl.dataRow = this.down_pcddRows;
                this.PCDDControl.lotteryId = 7;
                this.PCDDControl.flagtype = 0;
            }
            if ((this.down_xyft5Rows != null) && (this.down_xyft5Rows.Count<DataRow>() > 0))
            {
                this.XYFT5Control.dataRow = this.down_xyft5Rows;
                this.XYFT5Control.lotteryId = 9;
                this.XYFT5Control.flagtype = 0;
            }
            if ((this.down_pkbjlRows != null) && (this.down_pkbjlRows.Count<DataRow>() > 0))
            {
                this.PKBJLControl.dataRow = this.down_pkbjlRows;
                this.PKBJLControl.lotteryId = 8;
                this.PKBJLControl.flagtype = 0;
            }
            if ((this.down_jscarRows != null) && (this.down_jscarRows.Count<DataRow>() > 0))
            {
                this.JSCARControl.dataRow = this.down_jscarRows;
                this.JSCARControl.lotteryId = 10;
                this.JSCARControl.flagtype = 0;
            }
            if ((this.down_speed5Rows != null) && (this.down_speed5Rows.Count<DataRow>() > 0))
            {
                this.SPEED5Control.dataRow = this.down_speed5Rows;
                this.SPEED5Control.lotteryId = 11;
                this.SPEED5Control.flagtype = 0;
            }
            if ((this.down_jscqscRows != null) && (this.down_jscqscRows.Count<DataRow>() > 0))
            {
                this.JSCQSCControl.dataRow = this.down_jscqscRows;
                this.JSCQSCControl.lotteryId = 13;
                this.JSCQSCControl.flagtype = 0;
            }
            if ((this.down_jspk10Rows != null) && (this.down_jspk10Rows.Count<DataRow>() > 0))
            {
                this.JSPK10Control.dataRow = this.down_jspk10Rows;
                this.JSPK10Control.lotteryId = 12;
                this.JSPK10Control.flagtype = 0;
            }
            if ((this.down_jssfcRows != null) && (this.down_jssfcRows.Count<DataRow>() > 0))
            {
                this.JSSFCControl.dataRow = this.down_jssfcRows;
                this.JSSFCControl.lotteryId = 14;
                this.JSSFCControl.flagtype = 0;
                this.ylJSSFCControl.ylSytemDataRow = this.system_yl_jssfcRows;
                this.ylJSSFCControl.ylDownDataRow = this.down_yl_jssfcRows;
                this.ylJSSFCControl.lotteryId = 14;
                this.ylJSSFCControl.flagtype = 1;
                this.ylJSSFCControl.gameName = base.GetGameNameByID(14.ToString());
            }
            if ((this.down_jsft2Rows != null) && (this.down_jsft2Rows.Count<DataRow>() > 0))
            {
                this.JSFT2Control.dataRow = this.down_jsft2Rows;
                this.JSFT2Control.lotteryId = 15;
                this.JSFT2Control.flagtype = 0;
            }
            if ((this.down_car168Rows != null) && (this.down_car168Rows.Count<DataRow>() > 0))
            {
                this.CAR168Control.dataRow = this.down_car168Rows;
                this.CAR168Control.lotteryId = 0x10;
                this.CAR168Control.flagtype = 0;
            }
            if ((this.down_ssc168Rows != null) && (this.down_ssc168Rows.Count<DataRow>() > 0))
            {
                this.SSC168Control.dataRow = this.down_ssc168Rows;
                this.SSC168Control.lotteryId = 0x11;
                this.SSC168Control.flagtype = 0;
            }
            if ((this.down_vrcarRows != null) && (this.down_vrcarRows.Count<DataRow>() > 0))
            {
                this.VRCARControl.dataRow = this.down_vrcarRows;
                this.VRCARControl.lotteryId = 0x12;
                this.VRCARControl.flagtype = 0;
            }
            if ((this.down_vrsscRows != null) && (this.down_vrsscRows.Count<DataRow>() > 0))
            {
                this.VRSSCControl.dataRow = this.down_vrsscRows;
                this.VRSSCControl.lotteryId = 0x13;
                this.VRSSCControl.flagtype = 0;
            }
            if ((this.down_xyftoaRows != null) && (this.down_xyftoaRows.Count<DataRow>() > 0))
            {
                this.XYFTOAControl.dataRow = this.down_xyftoaRows;
                this.XYFTOAControl.lotteryId = 20;
                this.XYFTOAControl.flagtype = 0;
            }
            if ((this.down_xyftsgRows != null) && (this.down_xyftsgRows.Count<DataRow>() > 0))
            {
                this.XYFTSGControl.dataRow = this.down_xyftsgRows;
                this.XYFTSGControl.lotteryId = 0x15;
                this.XYFTSGControl.flagtype = 0;
            }
            if ((this.down_happycarRows != null) && (this.down_happycarRows.Count<DataRow>() > 0))
            {
                this.HAPPYCARControl.dataRow = this.down_happycarRows;
                this.HAPPYCARControl.lotteryId = 0x16;
                this.HAPPYCARControl.flagtype = 0;
            }
            if (LSRequest.qq("submitHdn").Equals("submit"))
            {
                this.UpdateDataEX();
                this.UpdateDrawbackAutoSet();
                this.UpdateData();
            }
        }

        private void UpdateData()
        {
            this.ValidInput();
            this.ValidSecondInput();
            DataTable systemSet = CallBLL.cz_system_set_kc_bll.GetSystemSet(this.l_ids);
            this.old_param_dt = systemSet;
            DataTable downoddsSet = CallBLL.cz_downodds_set_kc_bll.GetDownoddsSet(this.l_ids);
            this.old_down_dt = downoddsSet;
            foreach (DataRow row in this.lotteryDT.Rows)
            {
                if (!Convert.ToInt32(row["id"]).Equals(100) && !Convert.ToInt32(row["id"]).Equals(8))
                {
                    cz_system_set_kc _kc = new cz_system_set_kc();
                    _kc.set_lotterytype(new int?(Convert.ToInt32(row["id"])));
                    _kc.set_close_day_advance(new int?(Convert.ToInt32(LSRequest.qq("second_" + row["id"]))));
                    _kc.set_close_night_advance(0);
                    _kc.set_isopen(new int?(Convert.ToInt32(LSRequest.qq("isopen_" + row["id"]))));
                    _kc.set_odds_ratio(new int?(Convert.ToInt32(LSRequest.qq("odds_ratio_" + row["id"]))));
                    if (Convert.ToInt32(row["id"]).Equals(4) || Convert.ToInt32(row["id"]).Equals(5))
                    {
                        _kc.set_odds_ratio(0);
                    }
                    _kc.set_yl_isopen(0);
                    if ((Convert.ToInt32(row["id"]).Equals(0) || Convert.ToInt32(row["id"]).Equals(3)) || Convert.ToInt32(row["id"]).Equals(14))
                    {
                        _kc.set_yl_isopen(new int?(Convert.ToInt32(LSRequest.qq("yl_isopen_" + row["id"]))));
                    }
                    if ((Convert.ToInt32(row["id"]).Equals(1) || Convert.ToInt32(row["id"]).Equals(5)) || (Convert.ToInt32(row["id"]).Equals(6) || Convert.ToInt32(row["id"]).Equals(7)))
                    {
                        _kc.set_close_night_advance(new int?(Convert.ToInt32(LSRequest.qq("second_" + row["id"].ToString() + "_1"))));
                    }
                    CallBLL.cz_system_set_kc_bll.SetSystemSet(_kc);
                    for (int i = 1; i <= 0x19; i++)
                    {
                        cz_downodds_set_kc _kc2 = new cz_downodds_set_kc();
                        _kc2.set_lottery_type(new int?(Convert.ToInt32(row["id"])));
                        _kc2.set_phase(new int?(i));
                        _kc2.set_downbase1(new decimal?(Convert.ToDecimal(LSRequest.qq(string.Concat(new object[] { "MC_", row["id"].ToString(), "_", i })))));
                        _kc2.set_downbase2(new decimal?(Convert.ToDecimal(LSRequest.qq(string.Concat(new object[] { "LC_", row["id"].ToString(), "_", i })))));
                        _kc2.set_yl_downbase(0);
                        CallBLL.cz_downodds_set_kc_bll.SetDownoddsSet(_kc2);
                    }
                }
            }
            base.sys_param_set_kc_log(this.old_param_dt, CallBLL.cz_system_set_kc_bll.GetSystemSet(this.l_ids));
            base.sys_jp_set_kc_log(this.old_down_dt, CallBLL.cz_downodds_set_kc_bll.GetDownoddsSet(this.l_ids), false);
            foreach (DataRow row2 in this.lotteryDT.Rows)
            {
                if (!Convert.ToInt32(row2["id"]).Equals(100) && ((Convert.ToInt32(row2["id"]).Equals(0) || Convert.ToInt32(row2["id"]).Equals(3)) || Convert.ToInt32(row2["id"]).Equals(14)))
                {
                    for (int j = 3; j <= 0x19; j++)
                    {
                        cz_downodds_set_kc _kc3 = new cz_downodds_set_kc();
                        _kc3.set_lottery_type(new int?(Convert.ToInt32(row2["id"])));
                        _kc3.set_phase(new int?(j));
                        _kc3.set_yl_downbase(new decimal?(Convert.ToDecimal(LSRequest.qq(string.Concat(new object[] { "yl_", row2["id"].ToString(), "_", j })))));
                        CallBLL.cz_downodds_set_kc_bll.SetDownoddsSetEx(_kc3);
                    }
                }
            }
            base.sys_jp_set_kc_log(this.old_down_dt, CallBLL.cz_downodds_set_kc_bll.GetDownoddsSet(this.l_ids), true);
            string url = "/SystemSet/SystemSet_kc.aspx?lid=" + this.lotteryId;
            base.Response.Write(base.ShowDialogBox("系統初始化參數保存成功！", url, 0));
            base.Response.End();
        }

        private void UpdateDataEX()
        {
            cz_system_set_kc_ex _ex = new cz_system_set_kc_ex();
            _ex.set_s_id(this.cz_system_set_kc_exModel.get_s_id());
            _ex.set_RecoveryMode(1);
            _ex.set_DecimalNumber(3);
            _ex.set_MaxNumber(10);
            _ex.set_ReportOpenDate(LSRequest.qq("ReportOpenDate"));
            if (CallBLL.cz_system_set_kc_ex_bll.Update(_ex) && !this.cz_system_set_kc_exModel.get_ReportOpenDate().Equals(_ex.get_ReportOpenDate()))
            {
                string str = base.get_master_name();
                string str2 = base.get_children_name();
                string str3 = "";
                string category = "";
                string str5 = "";
                string str6 = "";
                string str7 = "";
                string str8 = "";
                int num = 0;
                string note = "系統參數設定";
                string act = "系統參數設定";
                int num2 = Convert.ToInt32((LSEnums.LogTypeID) 1);
                string str11 = "";
                str3 = "快彩";
                str7 = str7 + " 報表查詢起始日期: " + this.cz_system_set_kc_exModel.get_ReportOpenDate();
                str8 = str8 + " 報表查詢起始日期: " + _ex.get_ReportOpenDate();
                List<SqlParameter> paramList = new List<SqlParameter>();
                string str12 = base.add_sys_log(str, str2, category, str5, str6, str3, str11, act, num, str7, str8, note, num2, 0, ref paramList);
                CallBLL.cz_system_log_bll.executte_sql(str12.Replace(",0)", ",null)"), paramList.ToArray());
            }
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
                _set.set_master_id(2);
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
                    str4 = "快彩";
                    str8 = str8 + " 現金代理賺水返還模式: " + this.drawbackAutoSet;
                    str9 = str9 + " 現金代理賺水返還模式: " + str;
                    List<SqlParameter> paramList = new List<SqlParameter>();
                    string str13 = base.add_sys_log(str2, str3, category, str6, str7, str4, str12, act, num, str8, str9, note, num2, 0, ref paramList);
                    CallBLL.cz_system_log_bll.executte_sql(str13.Replace(",0)", ",null)"), paramList.ToArray());
                }
            }
        }

        private int UpdateLmGroup()
        {
            this.ValidInputGroup();
            if ((this.lmGroupTable == null) && (this.lmGroupTable_pcdd == null))
            {
                return 1;
            }
            int num = 0;
            if (this.lmGroupTable != null)
            {
                List<cz_lm_group_set> list = new List<cz_lm_group_set>();
                for (int i = 0; i < this.lmGroupTable.Rows.Count; i++)
                {
                    int num3 = 0;
                    string s = this.lmGroupTable.Rows[i]["play_id"].ToString();
                    string str2 = this.lmGroupTable.Rows[i]["min_num_count"].ToString();
                    if (!string.IsNullOrEmpty(LSRequest.qq(s + "_currentnumcount")))
                    {
                        int b = int.Parse(LSRequest.qq(s + "_currentnumcount"));
                        num3 = this.combination(b, int.Parse(str2));
                        cz_lm_group_set item = new cz_lm_group_set();
                        item.set_play_id(int.Parse(s));
                        item.set_max_num_group(num3);
                        item.set_current_num_count(b);
                        list.Add(item);
                    }
                }
                num += CallBLL.cz_lm_group_set_bll.UpdateKC(list, 0 + "," + 3);
                if (list.Count > 0)
                {
                    int num8 = 3;
                    base.lm_group_set_log_kc(this.lmGroupTable, (this.lotterys == num8.ToString()) ? CallBLL.cz_lm_group_set_bll.GetListKC(3) : CallBLL.cz_lm_group_set_bll.GetListKC(0), ref this.isRestUserAgent);
                }
            }
            if (this.lmGroupTable_pcdd != null)
            {
                List<cz_lm_group_set> list2 = new List<cz_lm_group_set>();
                for (int j = 0; j < this.lmGroupTable_pcdd.Rows.Count; j++)
                {
                    int num6 = 0;
                    string str3 = this.lmGroupTable_pcdd.Rows[j]["play_id"].ToString();
                    string str4 = this.lmGroupTable_pcdd.Rows[j]["min_num_count"].ToString();
                    if (!string.IsNullOrEmpty(LSRequest.qq(str3 + "_currentnumcount_pcdd")))
                    {
                        int num7 = int.Parse(LSRequest.qq(str3 + "_currentnumcount_pcdd"));
                        num6 = this.combination(num7, int.Parse(str4));
                        cz_lm_group_set _set2 = new cz_lm_group_set();
                        _set2.set_play_id(int.Parse(str3));
                        _set2.set_max_num_group(num6);
                        _set2.set_current_num_count(num7);
                        list2.Add(_set2);
                    }
                }
                int num9 = 7;
                num += CallBLL.cz_lm_group_set_bll.UpdateKC(list2, num9.ToString());
                if (list2.Count > 0)
                {
                    base.lm_group_set_log_kc(this.lmGroupTable_pcdd, CallBLL.cz_lm_group_set_bll.GetListKC(7), ref this.isRestUserAgent);
                }
            }
            return num;
        }

        private void ValidInput()
        {
            foreach (DataRow row in this.lotteryDT.Rows)
            {
                if (!Convert.ToInt32(row["id"]).Equals(100) && !Convert.ToInt32(row["id"]).Equals(8))
                {
                    if ((string.IsNullOrEmpty(LSRequest.qq("second_" + row["id"].ToString())) || string.IsNullOrEmpty(LSRequest.qq("isopen_" + row["id"].ToString()))) || string.IsNullOrEmpty(LSRequest.qq("odds_ratio_" + row["id"].ToString())))
                    {
                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 繫統初始設定不能為空！", base.GetGameNameByID(row["id"].ToString())), null, 400));
                        base.Response.End();
                    }
                    if (((Convert.ToInt32(row["id"]).Equals(1) || Convert.ToInt32(row["id"]).Equals(5)) || Convert.ToInt32(row["id"]).Equals(6)) && string.IsNullOrEmpty(LSRequest.qq("second_" + row["id"].ToString() + "_1")))
                    {
                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 繫統初始設定不能為空！", base.GetGameNameByID(row["id"].ToString())), null, 400));
                        base.Response.End();
                    }
                    for (int i = 1; i <= 0x19; i++)
                    {
                        if (string.IsNullOrEmpty(LSRequest.qq(string.Concat(new object[] { "MC_", row["id"].ToString(), "_", i }))) || string.IsNullOrEmpty(LSRequest.qq(string.Concat(new object[] { "LC_", row["id"].ToString(), "_", i }))))
                        {
                            base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 繫統初始設定不能為空！", base.GetGameNameByID(row["id"].ToString())), null, 400));
                            base.Response.End();
                        }
                    }
                    Regex regex = new Regex(@"^[+-]?\d+(\.\d+)?$");
                    if ((!regex.IsMatch(LSRequest.qq("second_" + row["id"].ToString())) || !regex.IsMatch(LSRequest.qq("isopen_" + row["id"].ToString()))) || !regex.IsMatch(LSRequest.qq("odds_ratio_" + row["id"].ToString())))
                    {
                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 繫統初始設定數據格式不正確！", base.GetGameNameByID(row["id"].ToString())), null, 400));
                        base.Response.End();
                    }
                    if (((Convert.ToInt32(row["id"]).Equals(1) || Convert.ToInt32(row["id"]).Equals(5)) || (Convert.ToInt32(row["id"]).Equals(6) || Convert.ToInt32(row["id"]).Equals(7))) && !regex.IsMatch(LSRequest.qq("second_" + row["id"].ToString() + "_1")))
                    {
                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 繫統初始設定數據格式不正確！", base.GetGameNameByID(row["id"].ToString())), null, 400));
                        base.Response.End();
                    }
                    for (int j = 1; j <= 0x19; j++)
                    {
                        if (!regex.IsMatch(LSRequest.qq(string.Concat(new object[] { "MC_", row["id"].ToString(), "_", j }))) || !regex.IsMatch(LSRequest.qq(string.Concat(new object[] { "LC_", row["id"].ToString(), "_", j }))))
                        {
                            base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 繫統初始設定數據格式不正確！", base.GetGameNameByID(row["id"].ToString())), null, 400));
                            base.Response.End();
                        }
                    }
                    for (int k = 1; k <= 0x19; k++)
                    {
                        if (Convert.ToDouble(LSRequest.qq(string.Concat(new object[] { "MC_", row["id"].ToString(), "_", k }))) < 0.0)
                        {
                            base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 繫統初始設定不能小於0！", base.GetGameNameByID(row["id"].ToString())), null, 400));
                            base.Response.End();
                        }
                    }
                    if ((Convert.ToInt32(row["id"]).Equals(0) || Convert.ToInt32(row["id"]).Equals(3)) || Convert.ToInt32(row["id"]).Equals(14))
                    {
                        if (string.IsNullOrEmpty(LSRequest.qq("yl_isopen_" + row["id"].ToString())))
                        {
                            base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 繫統初始設定不能為空！", base.GetGameNameByID(row["id"].ToString())), null, 400));
                            base.Response.End();
                        }
                        for (int m = 3; m <= 0x19; m++)
                        {
                            if (string.IsNullOrEmpty(LSRequest.qq(string.Concat(new object[] { "yl_", row["id"].ToString(), "_", m }))))
                            {
                                base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 繫統初始設定不能為空！", base.GetGameNameByID(row["id"].ToString())), null, 400));
                                base.Response.End();
                            }
                        }
                        if (!regex.IsMatch(LSRequest.qq("yl_isopen_" + row["id"].ToString())))
                        {
                            base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 繫統初始設定數據格式不正確！", base.GetGameNameByID(row["id"].ToString())), null, 400));
                            base.Response.End();
                        }
                        for (int n = 3; n <= 0x19; n++)
                        {
                            if (!regex.IsMatch(LSRequest.qq(string.Concat(new object[] { "yl_", row["id"].ToString(), "_", n }))))
                            {
                                base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 繫統初始設定數據格式不正確！", base.GetGameNameByID(row["id"].ToString())), null, 400));
                                base.Response.End();
                            }
                        }
                        for (int num6 = 3; num6 <= 0x19; num6++)
                        {
                            if (Convert.ToDouble(LSRequest.qq(string.Concat(new object[] { "yl_", row["id"].ToString(), "_", num6 }))) < 0.0)
                            {
                                base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 繫統初始設定遗漏不能小於0！", base.GetGameNameByID(row["id"].ToString())), null, 400));
                                base.Response.End();
                            }
                        }
                    }
                }
            }
        }

        private void ValidInputGroup()
        {
            if (string.IsNullOrEmpty(LSRequest.qq("ReportOpenDate")))
            {
                base.Response.Write(base.ShowDialogBox(string.Format("繫統初始設定‘{0}’不能為空！", "報表查詢起始日期"), null, 400));
                base.Response.End();
            }
            else
            {
                try
                {
                    Convert.ToDateTime(LSRequest.qq("ReportOpenDate"));
                }
                catch
                {
                    base.Response.Write(base.ShowDialogBox(string.Format("繫統初始設定‘{0}’格式錯誤！", "報表查詢起始日期"), null, 400));
                    base.Response.End();
                }
            }
            if ((this.lmGroupTable != null) || (this.lmGroupTable_pcdd != null))
            {
                if (this.lmGroupTable != null)
                {
                    for (int i = 0; i < this.lmGroupTable.Rows.Count; i++)
                    {
                        string str = this.lmGroupTable.Rows[i]["play_id"].ToString();
                        string s = this.lmGroupTable.Rows[i]["min_num_count"].ToString();
                        string str3 = this.lmGroupTable.Rows[i]["max_num_count"].ToString();
                        string str4 = this.lmGroupTable.Rows[i]["play_name_kl10"].ToString();
                        string str5 = this.lmGroupTable.Rows[i]["play_name_xync"].ToString();
                        string str6 = "";
                        if (this.lotterys.Split(new char[] { ',' }).Length > 1)
                        {
                            if (!string.IsNullOrEmpty(str5))
                            {
                                int num3 = 0;
                                int num4 = 3;
                                str6 = string.Format("({0})" + str4 + "，({1})" + str5, base.GetGameNameByID(num3.ToString()), base.GetGameNameByID(num4.ToString()));
                            }
                            else
                            {
                                int num5 = 0;
                                str6 = string.Format("({0})" + str4, base.GetGameNameByID(num5.ToString()));
                            }
                        }
                        else
                        {
                            int num6 = 0;
                            if (this.lotterys.Equals(num6.ToString()))
                            {
                                int num7 = 0;
                                str6 = string.Format("({0})" + str4, base.GetGameNameByID(num7.ToString()));
                            }
                            else
                            {
                                int num8 = 3;
                                if (this.lotterys.Equals(num8.ToString()))
                                {
                                    int num9 = 3;
                                    str6 = string.Format("({0})" + str5, base.GetGameNameByID(num9.ToString()));
                                }
                            }
                        }
                        if (!string.IsNullOrEmpty(LSRequest.qq(str + "_currentnumcount")))
                        {
                            string str7 = LSRequest.qq(str + "_currentnumcount");
                            if (!Utils.IsPureNumeric(str7.ToString()))
                            {
                                base.Response.Write(base.ShowDialogBox(string.Format("繫統初始設定‘{0}’格式不正確！", str6), null, 400));
                                base.Response.End();
                            }
                            if (int.Parse(str7) < int.Parse(s))
                            {
                                base.Response.Write(base.ShowDialogBox(string.Format("繫統初始設定‘{0}’設置當前連碼不能小於{1}！", str6, s), null, 400));
                                base.Response.End();
                            }
                            if (int.Parse(str7) > int.Parse(str3))
                            {
                                base.Response.Write(base.ShowDialogBox(string.Format("繫統初始設定‘{0}’設置當前連碼不能大於{1}！", str6, str3), null, 400));
                                base.Response.End();
                            }
                        }
                    }
                }
                if (this.lmGroupTable_pcdd != null)
                {
                    for (int j = 0; j < this.lmGroupTable_pcdd.Rows.Count; j++)
                    {
                        string str8 = this.lmGroupTable_pcdd.Rows[j]["play_id"].ToString();
                        string str9 = this.lmGroupTable_pcdd.Rows[j]["min_num_count"].ToString();
                        string str10 = this.lmGroupTable_pcdd.Rows[j]["max_num_count"].ToString();
                        string str11 = this.lmGroupTable_pcdd.Rows[j]["play_name"].ToString();
                        string str12 = "";
                        int num10 = 7;
                        str12 = string.Format("({0})" + str11, base.GetGameNameByID(num10.ToString()));
                        if (!string.IsNullOrEmpty(LSRequest.qq(str8 + "_currentnumcount_pcdd")))
                        {
                            string str13 = LSRequest.qq(str8 + "_currentnumcount_pcdd");
                            if (!Utils.IsPureNumeric(str13.ToString()))
                            {
                                base.Response.Write(base.ShowDialogBox(string.Format("繫統初始設定‘{0}’格式不正確！", str12), null, 400));
                                base.Response.End();
                            }
                            if (int.Parse(str13) < int.Parse(str9))
                            {
                                base.Response.Write(base.ShowDialogBox(string.Format("繫統初始設定‘{0}’設置當前連碼不能小於{1}！", str12, str9), null, 400));
                                base.Response.End();
                            }
                            if (int.Parse(str13) > int.Parse(str10))
                            {
                                base.Response.Write(base.ShowDialogBox(string.Format("繫統初始設定‘{0}’設置當前連碼不能大於{1}！", str12, str10), null, 400));
                                base.Response.End();
                            }
                        }
                    }
                }
            }
        }

        private void ValidSecondInput()
        {
            for (int i = 0; i < this.lotteryDT.Rows.Count; i++)
            {
                int num2 = Convert.ToInt32(this.lotteryDT.Rows[i]["id"].ToString());
                switch (num2)
                {
                    case 0:
                        if (Convert.ToInt32(LSRequest.qq("second_" + num2)) < base.GetLotterySecondsEnd(0, 0))
                        {
                            base.Response.Write(base.ShowDialogBox(string.Concat(new object[] { base.GetGameNameByID(num2.ToString()), " -（封盤提前） 不低於", base.GetLotterySecondsEnd(0, 0), "秒！" }), null, 400));
                            base.Response.End();
                        }
                        break;

                    case 1:
                        if (Convert.ToInt32(LSRequest.qq("second_" + num2)) < base.GetLotterySecondsEnd(1, 0))
                        {
                            base.Response.Write(base.ShowDialogBox(string.Concat(new object[] { base.GetGameNameByID(num2.ToString()), " -（封盤提前） 不低於", base.GetLotterySecondsEnd(1, 0), "秒！" }), null, 400));
                            base.Response.End();
                        }
                        if (Convert.ToInt32(LSRequest.qq("second_" + num2 + "_1")) < base.GetLotterySecondsEnd(1, 1))
                        {
                            base.Response.Write(base.ShowDialogBox(string.Concat(new object[] { base.GetGameNameByID(num2.ToString()), " -（封盤提前） 不低於", base.GetLotterySecondsEnd(1, 1), "秒！" }), null, 400));
                            base.Response.End();
                        }
                        break;

                    case 2:
                        if (Convert.ToInt32(LSRequest.qq("second_" + num2)) < base.GetLotterySecondsEnd(2, 0))
                        {
                            base.Response.Write(base.ShowDialogBox(string.Concat(new object[] { base.GetGameNameByID(num2.ToString()), " -（封盤提前）不低於", base.GetLotterySecondsEnd(2, 0), "秒！" }), null, 400));
                            base.Response.End();
                        }
                        break;

                    case 3:
                        if (Convert.ToInt32(LSRequest.qq("second_" + num2)) < base.GetLotterySecondsEnd(3, 0))
                        {
                            base.Response.Write(base.ShowDialogBox(string.Concat(new object[] { base.GetGameNameByID(num2.ToString()), " -（封盤提前）不低於", base.GetLotterySecondsEnd(3, 0), "秒！" }), null, 400));
                            base.Response.End();
                        }
                        break;

                    case 4:
                        if (Convert.ToInt32(LSRequest.qq("second_" + num2)) < base.GetLotterySecondsEnd(4, 0))
                        {
                            base.Response.Write(base.ShowDialogBox(string.Concat(new object[] { base.GetGameNameByID(num2.ToString()), " -（封盤提前）不低於", base.GetLotterySecondsEnd(4, 0), "秒！" }), null, 400));
                            base.Response.End();
                        }
                        break;

                    case 5:
                        if (Convert.ToInt32(LSRequest.qq("second_" + num2)) < base.GetLotterySecondsEnd(5, 0))
                        {
                            base.Response.Write(base.ShowDialogBox(string.Concat(new object[] { base.GetGameNameByID(num2.ToString()), " -（封盤提前）不低於", base.GetLotterySecondsEnd(5, 0), "秒！" }), null, 400));
                            base.Response.End();
                        }
                        if (Convert.ToInt32(LSRequest.qq("second_" + num2 + "_1")) < base.GetLotterySecondsEnd(5, 1))
                        {
                            base.Response.Write(base.ShowDialogBox(string.Concat(new object[] { base.GetGameNameByID(num2.ToString()), " -（封盤提前）不低於", base.GetLotterySecondsEnd(5, 1), "秒！" }), null, 400));
                            base.Response.End();
                        }
                        break;

                    case 6:
                        if (Convert.ToInt32(LSRequest.qq("second_" + num2)) < base.GetLotterySecondsEnd(6, 0))
                        {
                            base.Response.Write(base.ShowDialogBox(string.Concat(new object[] { base.GetGameNameByID(num2.ToString()), " -（封盤提前）不低於", base.GetLotterySecondsEnd(6, 0), "秒！" }), null, 400));
                            base.Response.End();
                        }
                        if (Convert.ToInt32(LSRequest.qq("second_" + num2 + "_1")) < base.GetLotterySecondsEnd(6, 1))
                        {
                            base.Response.Write(base.ShowDialogBox(string.Concat(new object[] { base.GetGameNameByID(num2.ToString()), " -（封盤提前）不低於", base.GetLotterySecondsEnd(6, 1), "秒！" }), null, 400));
                            base.Response.End();
                        }
                        break;

                    case 7:
                        if (Convert.ToInt32(LSRequest.qq("second_" + num2)) < base.GetLotterySecondsEnd(7, 0))
                        {
                            base.Response.Write(base.ShowDialogBox(string.Concat(new object[] { base.GetGameNameByID(num2.ToString()), " -（封盤提前）不低於", base.GetLotterySecondsEnd(7, 0), "秒！" }), null, 400));
                            base.Response.End();
                        }
                        if (Convert.ToInt32(LSRequest.qq("second_" + num2 + "_1")) < base.GetLotterySecondsEnd(7, 1))
                        {
                            base.Response.Write(base.ShowDialogBox(string.Concat(new object[] { base.GetGameNameByID(num2.ToString()), " -（封盤提前）不低於", base.GetLotterySecondsEnd(7, 1), "秒！" }), null, 400));
                            base.Response.End();
                        }
                        break;

                    case 9:
                        if (Convert.ToInt32(LSRequest.qq("second_" + num2)) < base.GetLotterySecondsEnd(9, 0))
                        {
                            base.Response.Write(base.ShowDialogBox(string.Concat(new object[] { base.GetGameNameByID(num2.ToString()), " -（封盤提前）不低於", base.GetLotterySecondsEnd(9, 0), "秒！" }), null, 400));
                            base.Response.End();
                        }
                        break;

                    case 10:
                        if (Convert.ToInt32(LSRequest.qq("second_" + num2)) < base.GetLotterySecondsEnd(10, 0))
                        {
                            base.Response.Write(base.ShowDialogBox(string.Concat(new object[] { base.GetGameNameByID(num2.ToString()), " -（封盤提前）不低於", base.GetLotterySecondsEnd(10, 0), "秒！" }), null, 400));
                            base.Response.End();
                        }
                        break;

                    case 11:
                        if (Convert.ToInt32(LSRequest.qq("second_" + num2)) < base.GetLotterySecondsEnd(11, 0))
                        {
                            base.Response.Write(base.ShowDialogBox(string.Concat(new object[] { base.GetGameNameByID(num2.ToString()), " -（封盤提前） 不低於", base.GetLotterySecondsEnd(11, 0), "秒！" }), null, 400));
                            base.Response.End();
                        }
                        break;

                    case 12:
                        if (Convert.ToInt32(LSRequest.qq("second_" + num2)) < base.GetLotterySecondsEnd(12, 0))
                        {
                            base.Response.Write(base.ShowDialogBox(string.Concat(new object[] { base.GetGameNameByID(num2.ToString()), " -（封盤提前）不低於", base.GetLotterySecondsEnd(12, 0), "秒！" }), null, 400));
                            base.Response.End();
                        }
                        break;

                    case 13:
                        if (Convert.ToInt32(LSRequest.qq("second_" + num2)) < base.GetLotterySecondsEnd(13, 0))
                        {
                            base.Response.Write(base.ShowDialogBox(string.Concat(new object[] { base.GetGameNameByID(num2.ToString()), " -（封盤提前） 不低於", base.GetLotterySecondsEnd(13, 0), "秒！" }), null, 400));
                            base.Response.End();
                        }
                        break;

                    case 14:
                        if (Convert.ToInt32(LSRequest.qq("second_" + num2)) < base.GetLotterySecondsEnd(14, 0))
                        {
                            base.Response.Write(base.ShowDialogBox(string.Concat(new object[] { base.GetGameNameByID(num2.ToString()), " -（封盤提前） 不低於", base.GetLotterySecondsEnd(14, 0), "秒！" }), null, 400));
                            base.Response.End();
                        }
                        break;

                    case 15:
                        if (Convert.ToInt32(LSRequest.qq("second_" + num2)) < base.GetLotterySecondsEnd(15, 0))
                        {
                            base.Response.Write(base.ShowDialogBox(string.Concat(new object[] { base.GetGameNameByID(num2.ToString()), " -（封盤提前）不低於", base.GetLotterySecondsEnd(15, 0), "秒！" }), null, 400));
                            base.Response.End();
                        }
                        break;

                    case 0x10:
                        if (Convert.ToInt32(LSRequest.qq("second_" + num2)) < base.GetLotterySecondsEnd(0x10, 0))
                        {
                            base.Response.Write(base.ShowDialogBox(string.Concat(new object[] { base.GetGameNameByID(num2.ToString()), " -（封盤提前）不低於", base.GetLotterySecondsEnd(0x10, 0), "秒！" }), null, 400));
                            base.Response.End();
                        }
                        break;

                    case 0x11:
                        if (Convert.ToInt32(LSRequest.qq("second_" + num2)) < base.GetLotterySecondsEnd(0x11, 0))
                        {
                            base.Response.Write(base.ShowDialogBox(string.Concat(new object[] { base.GetGameNameByID(num2.ToString()), " -（封盤提前） 不低於", base.GetLotterySecondsEnd(0x11, 0), "秒！" }), null, 400));
                            base.Response.End();
                        }
                        break;

                    case 0x12:
                        if (Convert.ToInt32(LSRequest.qq("second_" + num2)) < base.GetLotterySecondsEnd(0x12, 0))
                        {
                            base.Response.Write(base.ShowDialogBox(string.Concat(new object[] { base.GetGameNameByID(num2.ToString()), " -（封盤提前）不低於", base.GetLotterySecondsEnd(0x12, 0), "秒！" }), null, 400));
                            base.Response.End();
                        }
                        break;

                    case 0x13:
                        if (Convert.ToInt32(LSRequest.qq("second_" + num2)) < base.GetLotterySecondsEnd(0x13, 0))
                        {
                            base.Response.Write(base.ShowDialogBox(string.Concat(new object[] { base.GetGameNameByID(num2.ToString()), " -（封盤提前） 不低於", base.GetLotterySecondsEnd(0x13, 0), "秒！" }), null, 400));
                            base.Response.End();
                        }
                        break;

                    case 20:
                        if (Convert.ToInt32(LSRequest.qq("second_" + num2)) < base.GetLotterySecondsEnd(20, 0))
                        {
                            base.Response.Write(base.ShowDialogBox(string.Concat(new object[] { base.GetGameNameByID(num2.ToString()), " -（封盤提前）不低於", base.GetLotterySecondsEnd(20, 0), "秒！" }), null, 400));
                            base.Response.End();
                        }
                        break;

                    case 0x15:
                        if (Convert.ToInt32(LSRequest.qq("second_" + num2)) < base.GetLotterySecondsEnd(0x15, 0))
                        {
                            base.Response.Write(base.ShowDialogBox(string.Concat(new object[] { base.GetGameNameByID(num2.ToString()), " -（封盤提前）不低於", base.GetLotterySecondsEnd(0x15, 0), "秒！" }), null, 400));
                            base.Response.End();
                        }
                        break;
                }
            }
        }
    }
}

