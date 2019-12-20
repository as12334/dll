namespace Agent.Web
{
    using Agent.Web.WebBase;
    using LotterySystem.Common;
    using LotterySystem.Model;
    using LotterySystem.WebPageBase;
    using System;
    using System.Data;

    public class OddsWT : MemberPageBase
    {
        protected DataTable car168DataTable;
        protected DataTable cqscDataTable;
        protected DataTable happycarDataTable;
        protected DataTable jscarDataTable;
        protected DataTable jscqscDataTable;
        protected DataTable jsft2DataTable;
        protected DataTable jsk3DataTable;
        protected DataTable jspk10DataTable;
        protected DataTable jssfcDataTable;
        protected DataTable k8scDataTable;
        protected DataTable kl10DataTable;
        protected DataTable kl8DataTable;
        protected string lid = "";
        private DataTable lotteryDT;
        protected DataTable pcddDataTable;
        protected DataTable pk10DataTable;
        protected DataTable pkbjlDataTable;
        protected DataTable sixDataTable;
        protected string sltString = "";
        protected DataTable speed5DataTable;
        protected DataTable ssc168DataTable;
        protected agent_userinfo_session uModel;
        protected DataTable vrcarDataTable;
        protected DataTable vrsscDataTable;
        protected DataTable xyft5DataTable;
        protected DataTable xyftoaDataTable;
        protected DataTable xyftsgDataTable;
        protected DataTable xyncDataTable;

        private void InitDataTable()
        {
            if (this.uModel.get_six_op_odds().Equals(1) && !this.uModel.get_kc_op_odds().Equals(1))
            {
                if (!FileCacheHelper.get_IsShowLM_B())
                {
                    this.sixDataTable = CallBLL.cz_odds_wt_six_bll.GetOddsWT(this.uModel.get_u_name(), "91060,91061,91062,91063,91064,91065");
                }
                else
                {
                    this.sixDataTable = CallBLL.cz_odds_wt_six_bll.GetOddsWT(this.uModel.get_u_name());
                }
                this.lid = 100.ToString();
            }
            if (!this.uModel.get_six_op_odds().Equals(1) && this.uModel.get_kc_op_odds().Equals(1))
            {
                switch (int.Parse(this.lid))
                {
                    case 0:
                        this.kl10DataTable = CallBLL.cz_odds_wt_kl10_bll.GetOddsWT(this.uModel.get_u_name());
                        break;

                    case 1:
                        this.cqscDataTable = CallBLL.cz_odds_wt_cqsc_bll.GetOddsWT(this.uModel.get_u_name());
                        break;

                    case 2:
                        this.pk10DataTable = CallBLL.cz_odds_wt_pk10_bll.GetOddsWT(this.uModel.get_u_name());
                        break;

                    case 3:
                        this.xyncDataTable = CallBLL.cz_odds_wt_xync_bll.GetOddsWT(this.uModel.get_u_name());
                        break;

                    case 4:
                        this.jsk3DataTable = CallBLL.cz_odds_wt_jsk3_bll.GetOddsWT(this.uModel.get_u_name());
                        break;

                    case 5:
                        this.kl8DataTable = CallBLL.cz_odds_wt_kl8_bll.GetOddsWT(this.uModel.get_u_name());
                        break;

                    case 6:
                        this.k8scDataTable = CallBLL.cz_odds_wt_k8sc_bll.GetOddsWT(this.uModel.get_u_name());
                        break;

                    case 7:
                        this.pcddDataTable = CallBLL.cz_odds_wt_pcdd_bll.GetOddsWT(this.uModel.get_u_name());
                        break;

                    case 8:
                        this.pkbjlDataTable = CallBLL.cz_odds_wt_pkbjl_bll.GetOddsWT(this.uModel.get_u_name());
                        break;

                    case 9:
                        this.xyft5DataTable = CallBLL.cz_odds_wt_xyft5_bll.GetOddsWT(this.uModel.get_u_name());
                        break;

                    case 10:
                        this.jscarDataTable = CallBLL.cz_odds_wt_jscar_bll.GetOddsWT(this.uModel.get_u_name());
                        break;

                    case 11:
                        this.speed5DataTable = CallBLL.cz_odds_wt_speed5_bll.GetOddsWT(this.uModel.get_u_name());
                        break;

                    case 12:
                        this.jspk10DataTable = CallBLL.cz_odds_wt_jspk10_bll.GetOddsWT(this.uModel.get_u_name());
                        break;

                    case 13:
                        this.jscqscDataTable = CallBLL.cz_odds_wt_jscqsc_bll.GetOddsWT(this.uModel.get_u_name());
                        break;

                    case 14:
                        this.jssfcDataTable = CallBLL.cz_odds_wt_jssfc_bll.GetOddsWT(this.uModel.get_u_name());
                        break;

                    case 15:
                        this.jsft2DataTable = CallBLL.cz_odds_wt_jsft2_bll.GetOddsWT(this.uModel.get_u_name());
                        break;

                    case 0x10:
                        this.jscarDataTable = CallBLL.cz_odds_wt_car168_bll.GetOddsWT(this.uModel.get_u_name());
                        break;

                    case 0x11:
                        this.speed5DataTable = CallBLL.cz_odds_wt_ssc168_bll.GetOddsWT(this.uModel.get_u_name());
                        break;

                    case 0x12:
                        this.jscarDataTable = CallBLL.cz_odds_wt_vrcar_bll.GetOddsWT(this.uModel.get_u_name());
                        break;

                    case 0x13:
                        this.vrsscDataTable = CallBLL.cz_odds_wt_vrssc_bll.GetOddsWT(this.uModel.get_u_name());
                        break;

                    case 20:
                        this.xyftoaDataTable = CallBLL.cz_odds_wt_xyftoa_bll.GetOddsWT(this.uModel.get_u_name());
                        break;

                    case 0x15:
                        this.xyftsgDataTable = CallBLL.cz_odds_wt_xyftsg_bll.GetOddsWT(this.uModel.get_u_name());
                        break;

                    case 0x16:
                        this.happycarDataTable = CallBLL.cz_odds_wt_happycar_bll.GetOddsWT(this.uModel.get_u_name());
                        break;
                }
            }
            if (this.uModel.get_six_op_odds().Equals(1) && this.uModel.get_kc_op_odds().Equals(1))
            {
                switch (int.Parse(this.lid))
                {
                    case 0:
                        this.kl10DataTable = CallBLL.cz_odds_wt_kl10_bll.GetOddsWT(this.uModel.get_u_name());
                        return;

                    case 1:
                        this.cqscDataTable = CallBLL.cz_odds_wt_cqsc_bll.GetOddsWT(this.uModel.get_u_name());
                        return;

                    case 2:
                        this.pk10DataTable = CallBLL.cz_odds_wt_pk10_bll.GetOddsWT(this.uModel.get_u_name());
                        return;

                    case 3:
                        this.xyncDataTable = CallBLL.cz_odds_wt_xync_bll.GetOddsWT(this.uModel.get_u_name());
                        return;

                    case 4:
                        this.jsk3DataTable = CallBLL.cz_odds_wt_jsk3_bll.GetOddsWT(this.uModel.get_u_name());
                        return;

                    case 5:
                        this.kl8DataTable = CallBLL.cz_odds_wt_kl8_bll.GetOddsWT(this.uModel.get_u_name());
                        return;

                    case 6:
                        this.k8scDataTable = CallBLL.cz_odds_wt_k8sc_bll.GetOddsWT(this.uModel.get_u_name());
                        return;

                    case 7:
                        this.pcddDataTable = CallBLL.cz_odds_wt_pcdd_bll.GetOddsWT(this.uModel.get_u_name());
                        return;

                    case 8:
                        this.pkbjlDataTable = CallBLL.cz_odds_wt_pkbjl_bll.GetOddsWT(this.uModel.get_u_name());
                        return;

                    case 9:
                        this.xyft5DataTable = CallBLL.cz_odds_wt_xyft5_bll.GetOddsWT(this.uModel.get_u_name());
                        return;

                    case 10:
                        this.jscarDataTable = CallBLL.cz_odds_wt_jscar_bll.GetOddsWT(this.uModel.get_u_name());
                        return;

                    case 11:
                        this.speed5DataTable = CallBLL.cz_odds_wt_speed5_bll.GetOddsWT(this.uModel.get_u_name());
                        return;

                    case 12:
                        this.jspk10DataTable = CallBLL.cz_odds_wt_jspk10_bll.GetOddsWT(this.uModel.get_u_name());
                        return;

                    case 13:
                        this.jscqscDataTable = CallBLL.cz_odds_wt_jscqsc_bll.GetOddsWT(this.uModel.get_u_name());
                        return;

                    case 14:
                        this.jssfcDataTable = CallBLL.cz_odds_wt_jssfc_bll.GetOddsWT(this.uModel.get_u_name());
                        return;

                    case 15:
                        this.jsft2DataTable = CallBLL.cz_odds_wt_jsft2_bll.GetOddsWT(this.uModel.get_u_name());
                        return;

                    case 0x10:
                        this.car168DataTable = CallBLL.cz_odds_wt_car168_bll.GetOddsWT(this.uModel.get_u_name());
                        return;

                    case 0x11:
                        this.ssc168DataTable = CallBLL.cz_odds_wt_ssc168_bll.GetOddsWT(this.uModel.get_u_name());
                        return;

                    case 0x12:
                        this.vrcarDataTable = CallBLL.cz_odds_wt_vrcar_bll.GetOddsWT(this.uModel.get_u_name());
                        return;

                    case 0x13:
                        this.vrsscDataTable = CallBLL.cz_odds_wt_vrssc_bll.GetOddsWT(this.uModel.get_u_name());
                        return;

                    case 20:
                        this.xyftoaDataTable = CallBLL.cz_odds_wt_xyftoa_bll.GetOddsWT(this.uModel.get_u_name());
                        return;

                    case 0x15:
                        this.xyftsgDataTable = CallBLL.cz_odds_wt_xyftsg_bll.GetOddsWT(this.uModel.get_u_name());
                        return;

                    case 0x16:
                        this.happycarDataTable = CallBLL.cz_odds_wt_happycar_bll.GetOddsWT(this.uModel.get_u_name());
                        break;

                    case 100:
                        if (!FileCacheHelper.get_IsShowLM_B())
                        {
                            this.sixDataTable = CallBLL.cz_odds_wt_six_bll.GetOddsWT(this.uModel.get_u_name(), "91060,91061,91062,91063,91064,91065");
                            return;
                        }
                        this.sixDataTable = CallBLL.cz_odds_wt_six_bll.GetOddsWT(this.uModel.get_u_name());
                        return;

                    default:
                        return;
                }
            }
        }

        private void InitSelect(string lid)
        {
            foreach (DataRow row in this.lotteryDT.Rows)
            {
                string str = row["lottery_name"].ToString();
                string str2 = row["id"].ToString();
                row["dir_name"].ToString();
                if (this.uModel.get_six_op_odds().Equals(1) && this.uModel.get_kc_op_odds().Equals(1))
                {
                    if (row["id"].ToString().Equals(this.lid))
                    {
                        this.sltString = this.sltString + string.Format("<option value='{0}' {1}>{2}</option>", str2, "selected=selected", str);
                    }
                    else
                    {
                        this.sltString = this.sltString + string.Format("<option value='{0}' {1}>{2}</option>", str2, "", str);
                    }
                }
                if (this.uModel.get_six_op_odds().Equals(1) && !this.uModel.get_kc_op_odds().Equals(1))
                {
                    int num = 100;
                    if (row["id"].ToString().Equals(num.ToString()))
                    {
                        this.sltString = this.sltString + string.Format("<option value='{0}' {1}>{2}</option>", str2, "selected=selected", str);
                    }
                }
                if (!this.uModel.get_six_op_odds().Equals(1) && this.uModel.get_kc_op_odds().Equals(1))
                {
                    if (row["id"].ToString().Equals(lid))
                    {
                        int num2 = 100;
                        if (!row["id"].ToString().Equals(num2.ToString()))
                        {
                            this.sltString = this.sltString + string.Format("<option value='{0}' {1}>{2}</option>", str2, "selected=selected", str);
                        }
                    }
                    else
                    {
                        int num3 = 100;
                        if (!row["id"].ToString().Equals(num3.ToString()))
                        {
                            this.sltString = this.sltString + string.Format("<option value='{0}' {1}>{2}</option>", str2, "", str);
                        }
                    }
                }
            }
        }

        protected bool IsTableExists()
        {
            if ((((((this.sixDataTable == null) && (this.kl10DataTable == null)) && ((this.cqscDataTable == null) && (this.pk10DataTable == null))) && (((this.xyncDataTable == null) && (this.jsk3DataTable == null)) && ((this.kl8DataTable == null) && (this.k8scDataTable == null)))) && ((((this.pcddDataTable == null) && (this.xyft5DataTable == null)) && ((this.pkbjlDataTable == null) && (this.jscarDataTable == null))) && (((this.speed5DataTable == null) && (this.jspk10DataTable == null)) && ((this.jscqscDataTable == null) && (this.jssfcDataTable == null))))) && ((((this.jsft2DataTable == null) && (this.car168DataTable == null)) && ((this.ssc168DataTable == null) && (this.vrcarDataTable == null))) && (((this.vrsscDataTable == null) && (this.xyftoaDataTable == null)) && ((this.xyftsgDataTable == null) && (this.happycarDataTable == null)))))
            {
                return false;
            }
            return true;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string str = this.Session["user_name"].ToString();
            this.uModel = this.Session[str + "lottery_session_user_info"] as agent_userinfo_session;
            if (!this.uModel.get_u_type().Equals("fgs"))
            {
                base.Response.Redirect("/MessagePage.aspx?code=u100057&url=&issuccess=1&isback=0");
                base.Response.End();
            }
            if (this.uModel.get_u_type().Equals("fgs") && (this.uModel.get_six_op_odds().Equals(1) || this.uModel.get_kc_op_odds().Equals(1)))
            {
                base.Permission_Aspx_DL(this.uModel, "po_5_3");
            }
            else
            {
                base.Response.Redirect("/MessagePage.aspx?code=u100057&url=&issuccess=1&isback=0");
                base.Response.End();
            }
            this.lid = LSRequest.qq("lid");
            this.lotteryDT = base.GetLotteryList();
            base.IsLotteryExist(this.lid, "u100032", "1", "");
            if (!this.uModel.get_six_op_odds().Equals(1))
            {
                int num2 = 100;
                if (this.lid.Equals(num2.ToString()))
                {
                    this.lid = this.lotteryDT.Select(string.Format(" id<>{0} ", 100))[0]["id"].ToString();
                }
            }
            if (!this.uModel.get_kc_op_odds().Equals(1))
            {
                int num3 = 100;
                if (!this.lid.Equals(num3.ToString()))
                {
                    this.lid = 100.ToString();
                }
            }
            this.InitSelect(this.lid);
            this.InitDataTable();
            if (!LSRequest.qq("submitHdn").Equals("submit"))
            {
                return;
            }
            int num = 0;
            if (this.uModel.get_six_op_odds().Equals(1))
            {
                int num5 = 100;
                if (this.lid.Equals(num5.ToString()))
                {
                    num = CallBLL.cz_odds_wt_six_bll.RestOddsWT(this.uModel.get_u_name());
                    FileCacheHelper.UpdateFGSWTFile(100);
                }
            }
            if (this.uModel.get_kc_op_odds().Equals(1))
            {
                int num6 = 100;
                if (!this.lid.Equals(num6.ToString()) && (this.lotteryDT.Select(string.Format(" id={0} ", this.lid)).Length > 0))
                {
                    switch (int.Parse(this.lid))
                    {
                        case 0:
                            num = CallBLL.cz_odds_wt_kl10_bll.RestOddsWT(this.uModel.get_u_name());
                            FileCacheHelper.UpdateFGSWTFile(0);
                            goto Label_0645;

                        case 1:
                            num = CallBLL.cz_odds_wt_cqsc_bll.RestOddsWT(this.uModel.get_u_name());
                            FileCacheHelper.UpdateFGSWTFile(1);
                            goto Label_0645;

                        case 2:
                            num = CallBLL.cz_odds_wt_pk10_bll.RestOddsWT(this.uModel.get_u_name());
                            FileCacheHelper.UpdateFGSWTFile(2);
                            goto Label_0645;

                        case 3:
                            num = CallBLL.cz_odds_wt_xync_bll.RestOddsWT(this.uModel.get_u_name());
                            FileCacheHelper.UpdateFGSWTFile(3);
                            goto Label_0645;

                        case 4:
                            num = CallBLL.cz_odds_wt_jsk3_bll.RestOddsWT(this.uModel.get_u_name());
                            FileCacheHelper.UpdateFGSWTFile(4);
                            goto Label_0645;

                        case 5:
                            num = CallBLL.cz_odds_wt_kl8_bll.RestOddsWT(this.uModel.get_u_name());
                            FileCacheHelper.UpdateFGSWTFile(5);
                            goto Label_0645;

                        case 6:
                            num = CallBLL.cz_odds_wt_k8sc_bll.RestOddsWT(this.uModel.get_u_name());
                            FileCacheHelper.UpdateFGSWTFile(6);
                            goto Label_0645;

                        case 7:
                            num = CallBLL.cz_odds_wt_pcdd_bll.RestOddsWT(this.uModel.get_u_name());
                            FileCacheHelper.UpdateFGSWTFile(7);
                            goto Label_0645;

                        case 8:
                            num = CallBLL.cz_odds_wt_pkbjl_bll.RestOddsWT(this.uModel.get_u_name());
                            FileCacheHelper.UpdateFGSWTFile(8);
                            goto Label_0645;

                        case 9:
                            num = CallBLL.cz_odds_wt_xyft5_bll.RestOddsWT(this.uModel.get_u_name());
                            FileCacheHelper.UpdateFGSWTFile(9);
                            goto Label_0645;

                        case 10:
                            num = CallBLL.cz_odds_wt_jscar_bll.RestOddsWT(this.uModel.get_u_name());
                            FileCacheHelper.UpdateFGSWTFile(10);
                            goto Label_0645;

                        case 11:
                            num = CallBLL.cz_odds_wt_speed5_bll.RestOddsWT(this.uModel.get_u_name());
                            FileCacheHelper.UpdateFGSWTFile(11);
                            goto Label_0645;

                        case 12:
                            num = CallBLL.cz_odds_wt_jspk10_bll.RestOddsWT(this.uModel.get_u_name());
                            FileCacheHelper.UpdateFGSWTFile(12);
                            goto Label_0645;

                        case 13:
                            num = CallBLL.cz_odds_wt_jscqsc_bll.RestOddsWT(this.uModel.get_u_name());
                            FileCacheHelper.UpdateFGSWTFile(13);
                            goto Label_0645;

                        case 14:
                            num = CallBLL.cz_odds_wt_jssfc_bll.RestOddsWT(this.uModel.get_u_name());
                            FileCacheHelper.UpdateFGSWTFile(14);
                            goto Label_0645;

                        case 15:
                            num = CallBLL.cz_odds_wt_jsft2_bll.RestOddsWT(this.uModel.get_u_name());
                            FileCacheHelper.UpdateFGSWTFile(15);
                            goto Label_0645;

                        case 0x10:
                            num = CallBLL.cz_odds_wt_car168_bll.RestOddsWT(this.uModel.get_u_name());
                            FileCacheHelper.UpdateFGSWTFile(0x10);
                            goto Label_0645;

                        case 0x11:
                            num = CallBLL.cz_odds_wt_ssc168_bll.RestOddsWT(this.uModel.get_u_name());
                            FileCacheHelper.UpdateFGSWTFile(0x11);
                            goto Label_0645;

                        case 0x12:
                            num = CallBLL.cz_odds_wt_vrcar_bll.RestOddsWT(this.uModel.get_u_name());
                            FileCacheHelper.UpdateFGSWTFile(0x12);
                            goto Label_0645;

                        case 0x13:
                            num = CallBLL.cz_odds_wt_vrssc_bll.RestOddsWT(this.uModel.get_u_name());
                            FileCacheHelper.UpdateFGSWTFile(0x13);
                            goto Label_0645;

                        case 20:
                            num = CallBLL.cz_odds_wt_xyftoa_bll.RestOddsWT(this.uModel.get_u_name());
                            FileCacheHelper.UpdateFGSWTFile(20);
                            goto Label_0645;

                        case 0x15:
                            num = CallBLL.cz_odds_wt_xyftsg_bll.RestOddsWT(this.uModel.get_u_name());
                            FileCacheHelper.UpdateFGSWTFile(0x15);
                            goto Label_0645;

                        case 0x16:
                            num = CallBLL.cz_odds_wt_happycar_bll.RestOddsWT(this.uModel.get_u_name());
                            FileCacheHelper.UpdateFGSWTFile(0x16);
                            goto Label_0645;
                    }
                    base.Response.End();
                }
            }
        Label_0645:
            if (num <= 0)
            {
                string url = "/OddsSet/OddsWT.aspx?lid=" + this.lid;
                base.Response.Write(base.ShowDialogBox(string.Format(PageBase.GetMessageByCache("u100073", "MessageHint"), base.GetGameNameByID(this.lid)), url, 0));
                base.Response.End();
            }
            else
            {
                switch (int.Parse(this.lid))
                {
                    case 0:
                        base.fgs_reset_zero_wt_log(0);
                        break;

                    case 1:
                        base.fgs_reset_zero_wt_log(1);
                        break;

                    case 2:
                        base.fgs_reset_zero_wt_log(2);
                        break;

                    case 3:
                        base.fgs_reset_zero_wt_log(3);
                        break;

                    case 4:
                        base.fgs_reset_zero_wt_log(4);
                        break;

                    case 5:
                        base.fgs_reset_zero_wt_log(5);
                        break;

                    case 6:
                        base.fgs_reset_zero_wt_log(6);
                        break;

                    case 7:
                        base.fgs_reset_zero_wt_log(7);
                        break;

                    case 8:
                        base.fgs_reset_zero_wt_log(8);
                        break;

                    case 9:
                        base.fgs_reset_zero_wt_log(9);
                        break;

                    case 10:
                        base.fgs_reset_zero_wt_log(10);
                        break;

                    case 11:
                        base.fgs_reset_zero_wt_log(11);
                        break;

                    case 12:
                        base.fgs_reset_zero_wt_log(12);
                        break;

                    case 13:
                        base.fgs_reset_zero_wt_log(13);
                        break;

                    case 14:
                        base.fgs_reset_zero_wt_log(14);
                        break;

                    case 15:
                        base.fgs_reset_zero_wt_log(15);
                        break;

                    case 0x10:
                        base.fgs_reset_zero_wt_log(0x10);
                        break;

                    case 0x11:
                        base.fgs_reset_zero_wt_log(0x11);
                        break;

                    case 0x12:
                        base.fgs_reset_zero_wt_log(0x12);
                        break;

                    case 0x13:
                        base.fgs_reset_zero_wt_log(0x13);
                        break;

                    case 20:
                        base.fgs_reset_zero_wt_log(20);
                        break;

                    case 0x15:
                        base.fgs_reset_zero_wt_log(0x15);
                        break;

                    case 0x16:
                        base.fgs_reset_zero_wt_log(0x16);
                        break;

                    case 100:
                        base.fgs_reset_zero_wt_log(100);
                        break;

                    default:
                        base.Response.End();
                        break;
                }
                string str3 = string.Format("/OddsSet/OddsWT.aspx?lid={0}&rest=1", this.lid);
                base.Response.Write(base.ShowDialogBox(string.Format(PageBase.GetMessageByCache("u100072", "MessageHint"), base.GetGameNameByID(this.lid)), str3, 0));
                base.Response.End();
            }
        }
    }
}

