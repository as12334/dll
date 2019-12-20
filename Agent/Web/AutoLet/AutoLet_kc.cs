namespace Agent.Web.AutoLet
{
    using Agent.Web.WebBase;
    using LotterySystem.Common;
    using LotterySystem.Model;
    using System;
    using System.Data;

    public class AutoLet_kc : MemberPageBase
    {
        protected DataTable lotteryDT;
        protected string lotteryId = "";
        protected DataTable play_car168DT;
        protected DataTable play_cqscDT;
        protected DataTable play_happycarDT;
        protected DataTable play_jscarDT;
        protected DataTable play_jscqscDT;
        protected DataTable play_jsft2DT;
        protected DataTable play_jsk3DT;
        protected DataTable play_jspk10DT;
        protected DataTable play_jssfcDT;
        protected DataTable play_k8scDT;
        protected DataTable play_kl10DT;
        protected DataTable play_kl8DT;
        protected DataTable play_pcddDT;
        protected DataTable play_pk10DT;
        protected DataTable play_pkbjlDT;
        protected DataTable play_speed5DT;
        protected DataTable play_ssc168DT;
        protected DataTable play_vrcarDT;
        protected DataTable play_vrsscDT;
        protected DataTable play_xyft5DT;
        protected DataTable play_xyftoaDT;
        protected DataTable play_xyftsgDT;
        protected DataTable play_xyncDT;
        protected DataTable sale_car168DT;
        protected DataTable sale_cqscDT;
        protected DataTable sale_happycarDT;
        protected DataTable sale_jscarDT;
        protected DataTable sale_jscqscDT;
        protected DataTable sale_jsft2DT;
        protected DataTable sale_jsk3DT;
        protected DataTable sale_jspk10DT;
        protected DataTable sale_jssfcDT;
        protected DataTable sale_k8scDT;
        protected DataTable sale_kl10DT;
        protected DataTable sale_kl8DT;
        protected DataTable sale_pcddDT;
        protected DataTable sale_pk10DT;
        protected DataTable sale_pkbjlDT;
        protected DataTable sale_speed5DT;
        protected DataTable sale_ssc168DT;
        protected DataTable sale_vrcarDT;
        protected DataTable sale_vrsscDT;
        protected DataTable sale_xyft5DT;
        protected DataTable sale_xyftoaDT;
        protected DataTable sale_xyftsgDT;
        protected DataTable sale_xyncDT;
        protected string skin = "";
        protected string sltString = "";

        private void InitData()
        {
            for (int i = 0; i < this.lotteryDT.Rows.Count; i++)
            {
                switch (Convert.ToInt32(this.lotteryDT.Rows[i]["id"].ToString()))
                {
                    case 0:
                    {
                        DataSet autosaleByUserName = CallBLL.cz_autosale_kl10_bll.GetAutosaleByUserName(this.Session["user_name"].ToString());
                        if (((autosaleByUserName != null) && (autosaleByUserName.Tables.Count > 0)) && (autosaleByUserName.Tables[0].Rows.Count > 0))
                        {
                            this.sale_kl10DT = autosaleByUserName.Tables[0];
                        }
                        string str = "86,87,88,89,90,91,92,93,94,95,96,97,98,99,100,101,102,103,104,105,106,107,108,109,110,111,112,113,114,115,116,117,118,119,120,123,124,125,126,127,128,129,130,131,132,133,134,135,136";
                        this.play_kl10DT = CallBLL.cz_drawback_kl10_bll.GetDrawbackByPlayId(this.Session["user_name"].ToString(), str).Tables[0];
                        break;
                    }
                    case 1:
                    {
                        DataSet set2 = CallBLL.cz_autosale_cqsc_bll.GetAutosaleByUserName(this.Session["user_name"].ToString());
                        if (((set2 != null) && (set2.Tables.Count > 0)) && (set2.Tables[0].Rows.Count > 0))
                        {
                            this.sale_cqscDT = set2.Tables[0];
                        }
                        this.play_cqscDT = CallBLL.cz_drawback_cqsc_bll.GetDrawbackList(this.Session["user_name"].ToString(), "4,5,6,7,8,9,10,11,12,13,14,15").Tables[0];
                        break;
                    }
                    case 2:
                    {
                        DataSet set3 = CallBLL.cz_autosale_pk10_bll.GetAutosaleByUserName(this.Session["user_name"].ToString());
                        if (((set3 != null) && (set3.Tables.Count > 0)) && (set3.Tables[0].Rows.Count > 0))
                        {
                            this.sale_pk10DT = set3.Tables[0];
                        }
                        this.play_pk10DT = CallBLL.cz_drawback_pk10_bll.GetDrawbackList(this.Session["user_name"].ToString(), "1,2,3,4,36,37,38").Tables[0];
                        break;
                    }
                    case 3:
                    {
                        DataSet set4 = CallBLL.cz_autosale_xync_bll.GetAutosaleByUserName(this.Session["user_name"].ToString());
                        if (((set4 != null) && (set4.Tables.Count > 0)) && (set4.Tables[0].Rows.Count > 0))
                        {
                            this.sale_xyncDT = set4.Tables[0];
                        }
                        this.play_xyncDT = CallBLL.cz_drawback_xync_bll.GetDrawbackList(this.Session["user_name"].ToString(), "86,87,88,89,90,91,92,93,94,95,96,97,98,99,100,101,102,103,104,105,106,107,108,109,110,111,112,113,114,115,116,117,118,119,120,123,124,125,126,127,128,129,130,131,132,133,134,135,136").Tables[0];
                        break;
                    }
                    case 4:
                    {
                        DataSet set5 = CallBLL.cz_autosale_jsk3_bll.GetAutosaleByUserName(this.Session["user_name"].ToString());
                        if (((set5 != null) && (set5.Tables.Count > 0)) && (set5.Tables[0].Rows.Count > 0))
                        {
                            this.sale_jsk3DT = set5.Tables[0];
                        }
                        this.play_jsk3DT = CallBLL.cz_drawback_jsk3_bll.GetDrawbackList(this.Session["user_name"].ToString()).Tables[0];
                        break;
                    }
                    case 5:
                    {
                        DataSet set6 = CallBLL.cz_autosale_kl8_bll.GetAutosaleByUserName(this.Session["user_name"].ToString());
                        if (((set6 != null) && (set6.Tables.Count > 0)) && (set6.Tables[0].Rows.Count > 0))
                        {
                            this.sale_kl8DT = set6.Tables[0];
                        }
                        this.play_kl8DT = CallBLL.cz_drawback_kl8_bll.GetDrawbackList(this.Session["user_name"].ToString()).Tables[0];
                        break;
                    }
                    case 6:
                    {
                        DataSet set7 = CallBLL.cz_autosale_k8sc_bll.GetAutosaleByUserName(this.Session["user_name"].ToString());
                        if (((set7 != null) && (set7.Tables.Count > 0)) && (set7.Tables[0].Rows.Count > 0))
                        {
                            this.sale_k8scDT = set7.Tables[0];
                        }
                        this.play_k8scDT = CallBLL.cz_drawback_k8sc_bll.GetDrawbackList(this.Session["user_name"].ToString(), "4,5,6,7,8,9,10,11,12,13,14,15").Tables[0];
                        break;
                    }
                    case 7:
                    {
                        DataSet set8 = CallBLL.cz_autosale_pcdd_bll.GetAutosaleByUserName(this.Session["user_name"].ToString());
                        if (((set8 != null) && (set8.Tables.Count > 0)) && (set8.Tables[0].Rows.Count > 0))
                        {
                            this.sale_pcddDT = set8.Tables[0];
                        }
                        this.play_pcddDT = CallBLL.cz_drawback_pcdd_bll.GetDrawbackList(this.Session["user_name"].ToString(), "71009,71010,71011,71012").Tables[0];
                        break;
                    }
                    case 8:
                    {
                        DataSet set10 = CallBLL.cz_autosale_pkbjl_bll.GetAutosaleByUserName(this.Session["user_name"].ToString());
                        if (((set10 != null) && (set10.Tables.Count > 0)) && (set10.Tables[0].Rows.Count > 0))
                        {
                            this.sale_pkbjlDT = set10.Tables[0];
                        }
                        this.play_pkbjlDT = CallBLL.cz_drawback_pkbjl_bll.GetDrawbackList(this.Session["user_name"].ToString(), "81001,81002,81003,81004").Tables[0];
                        break;
                    }
                    case 9:
                    {
                        DataSet set9 = CallBLL.cz_autosale_xyft5_bll.GetAutosaleByUserName(this.Session["user_name"].ToString());
                        if (((set9 != null) && (set9.Tables.Count > 0)) && (set9.Tables[0].Rows.Count > 0))
                        {
                            this.sale_xyft5DT = set9.Tables[0];
                        }
                        this.play_xyft5DT = CallBLL.cz_drawback_xyft5_bll.GetDrawbackList(this.Session["user_name"].ToString(), "1,2,3,4,36,37,38").Tables[0];
                        break;
                    }
                    case 10:
                    {
                        DataSet set11 = CallBLL.cz_autosale_jscar_bll.GetAutosaleByUserName(this.Session["user_name"].ToString());
                        if (((set11 != null) && (set11.Tables.Count > 0)) && (set11.Tables[0].Rows.Count > 0))
                        {
                            this.sale_jscarDT = set11.Tables[0];
                        }
                        this.play_jscarDT = CallBLL.cz_drawback_jscar_bll.GetDrawbackList(this.Session["user_name"].ToString(), "1,2,3,4,36,37,38").Tables[0];
                        break;
                    }
                    case 11:
                    {
                        DataSet set12 = CallBLL.cz_autosale_speed5_bll.GetAutosaleByUserName(this.Session["user_name"].ToString());
                        if (((set12 != null) && (set12.Tables.Count > 0)) && (set12.Tables[0].Rows.Count > 0))
                        {
                            this.sale_speed5DT = set12.Tables[0];
                        }
                        this.play_speed5DT = CallBLL.cz_drawback_speed5_bll.GetDrawbackList(this.Session["user_name"].ToString(), "4,5,6,7,8,9,10,11,12,13,14,15").Tables[0];
                        break;
                    }
                    case 12:
                    {
                        DataSet set14 = CallBLL.cz_autosale_jspk10_bll.GetAutosaleByUserName(this.Session["user_name"].ToString());
                        if (((set14 != null) && (set14.Tables.Count > 0)) && (set14.Tables[0].Rows.Count > 0))
                        {
                            this.sale_jspk10DT = set14.Tables[0];
                        }
                        this.play_jspk10DT = CallBLL.cz_drawback_jspk10_bll.GetDrawbackList(this.Session["user_name"].ToString(), "1,2,3,4,36,37,38").Tables[0];
                        break;
                    }
                    case 13:
                    {
                        DataSet set13 = CallBLL.cz_autosale_jscqsc_bll.GetAutosaleByUserName(this.Session["user_name"].ToString());
                        if (((set13 != null) && (set13.Tables.Count > 0)) && (set13.Tables[0].Rows.Count > 0))
                        {
                            this.sale_jscqscDT = set13.Tables[0];
                        }
                        this.play_jscqscDT = CallBLL.cz_drawback_jscqsc_bll.GetDrawbackList(this.Session["user_name"].ToString(), "4,5,6,7,8,9,10,11,12,13,14,15").Tables[0];
                        break;
                    }
                    case 14:
                    {
                        DataSet set15 = CallBLL.cz_autosale_jssfc_bll.GetAutosaleByUserName(this.Session["user_name"].ToString());
                        if (((set15 != null) && (set15.Tables.Count > 0)) && (set15.Tables[0].Rows.Count > 0))
                        {
                            this.sale_jssfcDT = set15.Tables[0];
                        }
                        string str2 = "86,87,88,89,90,91,92,93,94,95,96,97,98,99,100,101,102,103,104,105,106,107,108,109,110,111,112,113,114,115,116,117,118,119,120,123,124,125,126,127,128,129,130,131,132,133,134,135,136";
                        this.play_jssfcDT = CallBLL.cz_drawback_jssfc_bll.GetDrawbackByPlayId(this.Session["user_name"].ToString(), str2).Tables[0];
                        break;
                    }
                    case 15:
                    {
                        DataSet set16 = CallBLL.cz_autosale_jsft2_bll.GetAutosaleByUserName(this.Session["user_name"].ToString());
                        if (((set16 != null) && (set16.Tables.Count > 0)) && (set16.Tables[0].Rows.Count > 0))
                        {
                            this.sale_jsft2DT = set16.Tables[0];
                        }
                        this.play_jsft2DT = CallBLL.cz_drawback_jsft2_bll.GetDrawbackList(this.Session["user_name"].ToString(), "1,2,3,4,36,37,38").Tables[0];
                        break;
                    }
                    case 0x10:
                    {
                        DataSet set17 = CallBLL.cz_autosale_car168_bll.GetAutosaleByUserName(this.Session["user_name"].ToString());
                        if (((set17 != null) && (set17.Tables.Count > 0)) && (set17.Tables[0].Rows.Count > 0))
                        {
                            this.sale_car168DT = set17.Tables[0];
                        }
                        this.play_car168DT = CallBLL.cz_drawback_car168_bll.GetDrawbackList(this.Session["user_name"].ToString(), "1,2,3,4,36,37,38").Tables[0];
                        break;
                    }
                    case 0x11:
                    {
                        DataSet set18 = CallBLL.cz_autosale_ssc168_bll.GetAutosaleByUserName(this.Session["user_name"].ToString());
                        if (((set18 != null) && (set18.Tables.Count > 0)) && (set18.Tables[0].Rows.Count > 0))
                        {
                            this.sale_ssc168DT = set18.Tables[0];
                        }
                        this.play_ssc168DT = CallBLL.cz_drawback_ssc168_bll.GetDrawbackList(this.Session["user_name"].ToString(), "4,5,6,7,8,9,10,11,12,13,14,15").Tables[0];
                        break;
                    }
                    case 0x12:
                    {
                        DataSet set19 = CallBLL.cz_autosale_vrcar_bll.GetAutosaleByUserName(this.Session["user_name"].ToString());
                        if (((set19 != null) && (set19.Tables.Count > 0)) && (set19.Tables[0].Rows.Count > 0))
                        {
                            this.sale_vrcarDT = set19.Tables[0];
                        }
                        this.play_vrcarDT = CallBLL.cz_drawback_vrcar_bll.GetDrawbackList(this.Session["user_name"].ToString(), "1,2,3,4,36,37,38").Tables[0];
                        break;
                    }
                    case 0x13:
                    {
                        DataSet set20 = CallBLL.cz_autosale_vrssc_bll.GetAutosaleByUserName(this.Session["user_name"].ToString());
                        if (((set20 != null) && (set20.Tables.Count > 0)) && (set20.Tables[0].Rows.Count > 0))
                        {
                            this.sale_vrsscDT = set20.Tables[0];
                        }
                        this.play_vrsscDT = CallBLL.cz_drawback_vrssc_bll.GetDrawbackList(this.Session["user_name"].ToString(), "4,5,6,7,8,9,10,11,12,13,14,15").Tables[0];
                        break;
                    }
                    case 20:
                    {
                        DataSet set21 = CallBLL.cz_autosale_xyftoa_bll.GetAutosaleByUserName(this.Session["user_name"].ToString());
                        if (((set21 != null) && (set21.Tables.Count > 0)) && (set21.Tables[0].Rows.Count > 0))
                        {
                            this.sale_xyftoaDT = set21.Tables[0];
                        }
                        this.play_xyftoaDT = CallBLL.cz_drawback_xyftoa_bll.GetDrawbackList(this.Session["user_name"].ToString(), "1,2,3,4,36,37,38").Tables[0];
                        break;
                    }
                    case 0x15:
                    {
                        DataSet set22 = CallBLL.cz_autosale_xyftsg_bll.GetAutosaleByUserName(this.Session["user_name"].ToString());
                        if (((set22 != null) && (set22.Tables.Count > 0)) && (set22.Tables[0].Rows.Count > 0))
                        {
                            this.sale_xyftsgDT = set22.Tables[0];
                        }
                        this.play_xyftsgDT = CallBLL.cz_drawback_xyftsg_bll.GetDrawbackList(this.Session["user_name"].ToString(), "1,2,3,4,36,37,38").Tables[0];
                        break;
                    }
                    case 0x16:
                    {
                        DataSet set23 = CallBLL.cz_autosale_happycar_bll.GetAutosaleByUserName(this.Session["user_name"].ToString());
                        if (((set23 != null) && (set23.Tables.Count > 0)) && (set23.Tables[0].Rows.Count > 0))
                        {
                            this.sale_happycarDT = set23.Tables[0];
                        }
                        this.play_happycarDT = CallBLL.cz_drawback_happycar_bll.GetDrawbackList(this.Session["user_name"].ToString(), "1,2,3,4,36,37,38").Tables[0];
                        break;
                    }
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string str = this.Session["user_name"].ToString();
            agent_userinfo_session model = this.Session[str + "lottery_session_user_info"] as agent_userinfo_session;
            this.skin = model.get_u_skin();
            if (model.get_u_type().Equals("zj".ToString()))
            {
                base.Response.Redirect("/MessagePage.aspx?code=u100057&url=&issuccess=1&isback=0");
                base.Response.End();
            }
            base.Permission_Aspx_DL(model, "po_5_2");
            this.lotteryId = LSRequest.qq("lid");
            this.lotteryDT = base.GetLotteryList();
            int num = 100;
            if (this.lotteryId.Equals(num.ToString()))
            {
                base.Response.Redirect("/AutoLet/AutoLet_six.aspx?lid=" + this.lotteryId, true);
            }
            DataRow[] rowArray = this.lotteryDT.Select(string.Format(" id={0} ", 100));
            DataRow[] rowArray2 = this.lotteryDT.Select(string.Format(" id<>{0} ", 100));
            int num2 = 100;
            if (this.lotteryId.Equals(num2.ToString()))
            {
                if (rowArray.Length > 0)
                {
                    this.sltString = this.sltString + string.Format("<option value='{0}' selected=selected>{1}</option>", 100, "香港⑥合彩");
                }
                if (rowArray2.Length > 0)
                {
                    this.sltString = this.sltString + string.Format("<option value='{0}'>{1}</option>", 0, "快彩");
                }
            }
            else
            {
                if (rowArray.Length > 0)
                {
                    this.sltString = this.sltString + string.Format("<option value='{0}'>{1}</option>", 100, "香港⑥合彩");
                }
                if (rowArray2.Length > 0)
                {
                    this.sltString = this.sltString + string.Format("<option value='{0}' selected=selected>{1}</option>", 0, "快彩");
                }
            }
            if (!model.get_kc_allow_sale().Equals(1))
            {
                base.Response.Redirect("/MessagePage.aspx?code=u100036&url=&issuccess=1&isback=0");
                base.Response.End();
            }
            this.InitData();
            if (LSRequest.qq("hdnSubmit").Equals("submit"))
            {
                this.SaveData();
            }
        }

        private void SaveData()
        {
            this.ValidInput();
            bool flag = true;
            bool flag2 = true;
            bool flag3 = true;
            bool flag4 = true;
            bool flag5 = true;
            bool flag6 = true;
            bool flag7 = true;
            bool flag8 = true;
            bool flag9 = true;
            bool flag10 = true;
            bool flag11 = true;
            bool flag12 = true;
            bool flag13 = true;
            bool flag14 = true;
            bool flag15 = true;
            bool flag16 = true;
            bool flag17 = true;
            bool flag18 = true;
            bool flag19 = true;
            bool flag20 = true;
            bool flag21 = true;
            bool flag22 = true;
            bool flag23 = true;
            DataTable table = CallBLL.cz_autosale_kl10_bll.GetAutosaleByUserName(this.Session["user_name"].ToString()).Tables[0];
            if (this.play_kl10DT != null)
            {
                bool flag24 = true;
                for (int i = 0; i < this.play_kl10DT.Rows.Count; i++)
                {
                    string str = this.play_kl10DT.Rows[i]["play_id"].ToString();
                    string str2 = this.play_kl10DT.Rows[i]["play_name"].ToString();
                    string str3 = LSRequest.qq("kl10_money_" + str);
                    cz_autosale_kl10 model = new cz_autosale_kl10();
                    model.set_play_id(new int?(Convert.ToInt32(str)));
                    model.set_u_name(this.Session["user_name"].ToString());
                    model.set_max_money(new decimal?(!string.IsNullOrEmpty(str3) ? Convert.ToDecimal(str3) : 0M));
                    model.set_play_name(str2);
                    model.set_sale_type(0);
                    model.set_set_type(0);
                    string isInsert = LSRequest.qq("kl10_chk_" + str);
                    try
                    {
                        CallBLL.cz_autosale_kl10_bll.SetAutosale(model, isInsert);
                    }
                    catch
                    {
                        flag24 = false;
                        break;
                    }
                    if ((table == null) && (isInsert == "1"))
                    {
                        base.autosale_set_kc_log(null, model, isInsert, 0);
                    }
                    else
                    {
                        DataRow[] rowArray = table.Select(string.Format(" play_id={0} ", model.get_play_id()));
                        if ((rowArray.Length <= 0) && (isInsert == "1"))
                        {
                            base.autosale_set_kc_log(rowArray, model, isInsert, 0);
                        }
                        else if (rowArray.Length == 1)
                        {
                            base.autosale_set_kc_log(rowArray, model, isInsert, 0);
                        }
                    }
                }
                if (!flag24)
                {
                    flag = false;
                }
            }
            if (this.play_cqscDT != null)
            {
                table = CallBLL.cz_autosale_cqsc_bll.GetAutosaleByUserName(this.Session["user_name"].ToString()).Tables[0];
                bool flag25 = true;
                for (int j = 0; j < this.play_cqscDT.Rows.Count; j++)
                {
                    string str5 = this.play_cqscDT.Rows[j]["play_id"].ToString();
                    string str6 = this.play_cqscDT.Rows[j]["play_name"].ToString();
                    string str7 = LSRequest.qq("cqsc_money_" + str5);
                    cz_autosale_cqsc _cqsc = new cz_autosale_cqsc();
                    _cqsc.set_play_id(new int?(Convert.ToInt32(str5)));
                    _cqsc.set_u_name(this.Session["user_name"].ToString());
                    _cqsc.set_max_money(new decimal?(!string.IsNullOrEmpty(str7) ? Convert.ToDecimal(str7) : 0M));
                    _cqsc.set_play_name(str6);
                    _cqsc.set_sale_type(0);
                    _cqsc.set_set_type(0);
                    string str8 = LSRequest.qq("cqsc_chk_" + str5);
                    try
                    {
                        CallBLL.cz_autosale_cqsc_bll.SetAutosale(_cqsc, str8);
                    }
                    catch
                    {
                        flag25 = false;
                        break;
                    }
                    if ((table == null) && (str8 == "1"))
                    {
                        base.autosale_set_kc_log(null, _cqsc, str8, 1);
                    }
                    else
                    {
                        DataRow[] rowArray2 = table.Select(string.Format(" play_id={0} ", _cqsc.get_play_id()));
                        if ((rowArray2.Length <= 0) && (str8 == "1"))
                        {
                            base.autosale_set_kc_log(rowArray2, _cqsc, str8, 1);
                        }
                        else if (rowArray2.Length == 1)
                        {
                            base.autosale_set_kc_log(rowArray2, _cqsc, str8, 1);
                        }
                    }
                }
                if (!flag25)
                {
                    flag2 = false;
                }
            }
            if (this.play_pk10DT != null)
            {
                table = CallBLL.cz_autosale_pk10_bll.GetAutosaleByUserName(this.Session["user_name"].ToString()).Tables[0];
                bool flag26 = true;
                for (int k = 0; k < this.play_pk10DT.Rows.Count; k++)
                {
                    string str9 = this.play_pk10DT.Rows[k]["play_id"].ToString();
                    string str10 = this.play_pk10DT.Rows[k]["play_name"].ToString();
                    string str11 = LSRequest.qq("pk10_money_" + str9);
                    cz_autosale_pk10 _pk = new cz_autosale_pk10();
                    _pk.set_play_id(new int?(Convert.ToInt32(str9)));
                    _pk.set_u_name(this.Session["user_name"].ToString());
                    _pk.set_max_money(new decimal?(!string.IsNullOrEmpty(str11) ? Convert.ToDecimal(str11) : 0M));
                    _pk.set_play_name(str10);
                    _pk.set_sale_type(0);
                    _pk.set_set_type(0);
                    string str12 = LSRequest.qq("pk10_chk_" + str9);
                    try
                    {
                        CallBLL.cz_autosale_pk10_bll.SetAutosale(_pk, str12);
                    }
                    catch
                    {
                        flag26 = false;
                        break;
                    }
                    if ((table == null) && (str12 == "1"))
                    {
                        base.autosale_set_kc_log(null, _pk, str12, 2);
                    }
                    else
                    {
                        DataRow[] rowArray3 = table.Select(string.Format(" play_id={0} ", _pk.get_play_id()));
                        if ((rowArray3.Length <= 0) && (str12 == "1"))
                        {
                            base.autosale_set_kc_log(rowArray3, _pk, str12, 2);
                        }
                        else if (rowArray3.Length == 1)
                        {
                            base.autosale_set_kc_log(rowArray3, _pk, str12, 2);
                        }
                    }
                }
                if (!flag26)
                {
                    flag3 = false;
                }
            }
            if (this.play_xyncDT != null)
            {
                table = CallBLL.cz_autosale_xync_bll.GetAutosaleByUserName(this.Session["user_name"].ToString()).Tables[0];
                bool flag27 = true;
                for (int m = 0; m < this.play_xyncDT.Rows.Count; m++)
                {
                    string str13 = this.play_xyncDT.Rows[m]["play_id"].ToString();
                    string str14 = this.play_xyncDT.Rows[m]["play_name"].ToString();
                    string str15 = LSRequest.qq("xync_money_" + str13);
                    cz_autosale_xync _xync = new cz_autosale_xync();
                    _xync.set_play_id(new int?(Convert.ToInt32(str13)));
                    _xync.set_u_name(this.Session["user_name"].ToString());
                    _xync.set_max_money(new decimal?(!string.IsNullOrEmpty(str15) ? Convert.ToDecimal(str15) : 0M));
                    _xync.set_play_name(str14);
                    _xync.set_sale_type(0);
                    _xync.set_set_type(0);
                    string str16 = LSRequest.qq("xync_chk_" + str13);
                    try
                    {
                        CallBLL.cz_autosale_xync_bll.SetAutosale(_xync, str16);
                    }
                    catch
                    {
                        flag27 = false;
                        break;
                    }
                    if ((table == null) && (str16 == "1"))
                    {
                        base.autosale_set_kc_log(null, _xync, str16, 3);
                    }
                    else
                    {
                        DataRow[] rowArray4 = table.Select(string.Format(" play_id={0} ", _xync.get_play_id()));
                        if ((rowArray4.Length <= 0) && (str16 == "1"))
                        {
                            base.autosale_set_kc_log(rowArray4, _xync, str16, 3);
                        }
                        else if (rowArray4.Length == 1)
                        {
                            base.autosale_set_kc_log(rowArray4, _xync, str16, 3);
                        }
                    }
                }
                if (!flag27)
                {
                    flag4 = false;
                }
            }
            if (this.play_jsk3DT != null)
            {
                table = CallBLL.cz_autosale_jsk3_bll.GetAutosaleByUserName(this.Session["user_name"].ToString()).Tables[0];
                bool flag28 = true;
                for (int n = 0; n < this.play_jsk3DT.Rows.Count; n++)
                {
                    string str17 = this.play_jsk3DT.Rows[n]["play_id"].ToString();
                    string str18 = this.play_jsk3DT.Rows[n]["play_name"].ToString();
                    string str19 = LSRequest.qq("jsk3_money_" + str17);
                    cz_autosale_jsk3 _jsk = new cz_autosale_jsk3();
                    _jsk.set_play_id(new int?(Convert.ToInt32(str17)));
                    _jsk.set_u_name(this.Session["user_name"].ToString());
                    _jsk.set_max_money(new decimal?(!string.IsNullOrEmpty(str19) ? Convert.ToDecimal(str19) : 0M));
                    _jsk.set_play_name(str18);
                    _jsk.set_sale_type(0);
                    _jsk.set_set_type(0);
                    string str20 = LSRequest.qq("jsk3_chk_" + str17);
                    try
                    {
                        CallBLL.cz_autosale_jsk3_bll.SetAutosale(_jsk, str20);
                    }
                    catch
                    {
                        flag28 = false;
                        break;
                    }
                    if ((table == null) && (str20 == "1"))
                    {
                        base.autosale_set_kc_log(null, _jsk, str20, 4);
                    }
                    else
                    {
                        DataRow[] rowArray5 = table.Select(string.Format(" play_id={0} ", _jsk.get_play_id()));
                        if ((rowArray5.Length <= 0) && (str20 == "1"))
                        {
                            base.autosale_set_kc_log(rowArray5, _jsk, str20, 4);
                        }
                        else if (rowArray5.Length == 1)
                        {
                            base.autosale_set_kc_log(rowArray5, _jsk, str20, 4);
                        }
                    }
                }
                if (!flag28)
                {
                    flag6 = false;
                }
            }
            if (this.play_kl8DT != null)
            {
                table = CallBLL.cz_autosale_kl8_bll.GetAutosaleByUserName(this.Session["user_name"].ToString()).Tables[0];
                bool flag29 = true;
                for (int num6 = 0; num6 < this.play_kl8DT.Rows.Count; num6++)
                {
                    string str21 = this.play_kl8DT.Rows[num6]["play_id"].ToString();
                    string str22 = this.play_kl8DT.Rows[num6]["play_name"].ToString();
                    string str23 = LSRequest.qq("kl8_money_" + str21);
                    cz_autosale_kl8 _kl2 = new cz_autosale_kl8();
                    _kl2.set_play_id(new int?(Convert.ToInt32(str21)));
                    _kl2.set_u_name(this.Session["user_name"].ToString());
                    _kl2.set_max_money(new decimal?(!string.IsNullOrEmpty(str23) ? Convert.ToDecimal(str23) : 0M));
                    _kl2.set_play_name(str22);
                    _kl2.set_sale_type(0);
                    _kl2.set_set_type(0);
                    string str24 = LSRequest.qq("kl8_chk_" + str21);
                    try
                    {
                        CallBLL.cz_autosale_kl8_bll.SetAutosale(_kl2, str24);
                    }
                    catch
                    {
                        flag29 = false;
                        break;
                    }
                    if ((table == null) && (str24 == "1"))
                    {
                        base.autosale_set_kc_log(null, _kl2, str24, 5);
                    }
                    else
                    {
                        DataRow[] rowArray6 = table.Select(string.Format(" play_id={0} ", _kl2.get_play_id()));
                        if ((rowArray6.Length <= 0) && (str24 == "1"))
                        {
                            base.autosale_set_kc_log(rowArray6, _kl2, str24, 5);
                        }
                        else if (rowArray6.Length == 1)
                        {
                            base.autosale_set_kc_log(rowArray6, _kl2, str24, 5);
                        }
                    }
                }
                if (!flag29)
                {
                    flag5 = false;
                }
            }
            if (this.play_k8scDT != null)
            {
                table = CallBLL.cz_autosale_k8sc_bll.GetAutosaleByUserName(this.Session["user_name"].ToString()).Tables[0];
                bool flag30 = true;
                for (int num7 = 0; num7 < this.play_k8scDT.Rows.Count; num7++)
                {
                    string str25 = this.play_k8scDT.Rows[num7]["play_id"].ToString();
                    string str26 = this.play_k8scDT.Rows[num7]["play_name"].ToString();
                    string str27 = LSRequest.qq("k8sc_money_" + str25);
                    cz_autosale_k8sc _ksc = new cz_autosale_k8sc();
                    _ksc.set_play_id(new int?(Convert.ToInt32(str25)));
                    _ksc.set_u_name(this.Session["user_name"].ToString());
                    _ksc.set_max_money(new decimal?(!string.IsNullOrEmpty(str27) ? Convert.ToDecimal(str27) : 0M));
                    _ksc.set_play_name(str26);
                    _ksc.set_sale_type(0);
                    _ksc.set_set_type(0);
                    string str28 = LSRequest.qq("k8sc_chk_" + str25);
                    try
                    {
                        CallBLL.cz_autosale_k8sc_bll.SetAutosale(_ksc, str28);
                    }
                    catch
                    {
                        flag30 = false;
                        break;
                    }
                    if ((table == null) && (str28 == "1"))
                    {
                        base.autosale_set_kc_log(null, _ksc, str28, 6);
                    }
                    else
                    {
                        DataRow[] rowArray7 = table.Select(string.Format(" play_id={0} ", _ksc.get_play_id()));
                        if ((rowArray7.Length <= 0) && (str28 == "1"))
                        {
                            base.autosale_set_kc_log(rowArray7, _ksc, str28, 6);
                        }
                        else if (rowArray7.Length == 1)
                        {
                            base.autosale_set_kc_log(rowArray7, _ksc, str28, 6);
                        }
                    }
                }
                if (!flag30)
                {
                    flag7 = false;
                }
            }
            if (this.play_pcddDT != null)
            {
                table = CallBLL.cz_autosale_pcdd_bll.GetAutosaleByUserName(this.Session["user_name"].ToString()).Tables[0];
                bool flag31 = true;
                for (int num8 = 0; num8 < this.play_pcddDT.Rows.Count; num8++)
                {
                    string str29 = this.play_pcddDT.Rows[num8]["play_id"].ToString();
                    string str30 = this.play_pcddDT.Rows[num8]["play_name"].ToString();
                    string str31 = LSRequest.qq("pcdd_money_" + str29);
                    cz_autosale_pcdd _pcdd = new cz_autosale_pcdd();
                    _pcdd.set_play_id(new int?(Convert.ToInt32(str29)));
                    _pcdd.set_u_name(this.Session["user_name"].ToString());
                    _pcdd.set_max_money(new decimal?(!string.IsNullOrEmpty(str31) ? Convert.ToDecimal(str31) : 0M));
                    _pcdd.set_play_name(str30);
                    _pcdd.set_sale_type(0);
                    _pcdd.set_set_type(0);
                    string str32 = LSRequest.qq("pcdd_chk_" + str29);
                    try
                    {
                        CallBLL.cz_autosale_pcdd_bll.SetAutosale(_pcdd, str32);
                    }
                    catch
                    {
                        flag31 = false;
                        break;
                    }
                    if ((table == null) && (str32 == "1"))
                    {
                        base.autosale_set_kc_log(null, _pcdd, str32, 7);
                    }
                    else
                    {
                        DataRow[] rowArray8 = table.Select(string.Format(" play_id={0} ", _pcdd.get_play_id()));
                        if ((rowArray8.Length <= 0) && (str32 == "1"))
                        {
                            base.autosale_set_kc_log(rowArray8, _pcdd, str32, 7);
                        }
                        else if (rowArray8.Length == 1)
                        {
                            base.autosale_set_kc_log(rowArray8, _pcdd, str32, 7);
                        }
                    }
                }
                if (!flag31)
                {
                    flag8 = false;
                }
            }
            if (this.play_xyft5DT != null)
            {
                table = CallBLL.cz_autosale_xyft5_bll.GetAutosaleByUserName(this.Session["user_name"].ToString()).Tables[0];
                bool flag32 = true;
                for (int num9 = 0; num9 < this.play_xyft5DT.Rows.Count; num9++)
                {
                    string str33 = this.play_xyft5DT.Rows[num9]["play_id"].ToString();
                    string str34 = this.play_xyft5DT.Rows[num9]["play_name"].ToString();
                    string str35 = LSRequest.qq("xyft5_money_" + str33);
                    cz_autosale_xyft5 _xyft = new cz_autosale_xyft5();
                    _xyft.set_play_id(new int?(Convert.ToInt32(str33)));
                    _xyft.set_u_name(this.Session["user_name"].ToString());
                    _xyft.set_max_money(new decimal?(!string.IsNullOrEmpty(str35) ? Convert.ToDecimal(str35) : 0M));
                    _xyft.set_play_name(str34);
                    _xyft.set_sale_type(0);
                    _xyft.set_set_type(0);
                    string str36 = LSRequest.qq("xyft5_chk_" + str33);
                    try
                    {
                        CallBLL.cz_autosale_xyft5_bll.SetAutosale(_xyft, str36);
                    }
                    catch
                    {
                        flag32 = false;
                        break;
                    }
                    if ((table == null) && (str36 == "1"))
                    {
                        base.autosale_set_kc_log(null, _xyft, str36, 9);
                    }
                    else
                    {
                        DataRow[] rowArray9 = table.Select(string.Format(" play_id={0} ", _xyft.get_play_id()));
                        if ((rowArray9.Length <= 0) && (str36 == "1"))
                        {
                            base.autosale_set_kc_log(rowArray9, _xyft, str36, 9);
                        }
                        else if (rowArray9.Length == 1)
                        {
                            base.autosale_set_kc_log(rowArray9, _xyft, str36, 9);
                        }
                    }
                }
                if (!flag32)
                {
                    flag9 = false;
                }
            }
            if (this.play_pkbjlDT != null)
            {
                table = CallBLL.cz_autosale_pkbjl_bll.GetAutosaleByUserName(this.Session["user_name"].ToString()).Tables[0];
                bool flag33 = true;
                for (int num10 = 0; num10 < this.play_pkbjlDT.Rows.Count; num10++)
                {
                    string str37 = this.play_pkbjlDT.Rows[num10]["play_id"].ToString();
                    string str38 = this.play_pkbjlDT.Rows[num10]["play_name"].ToString();
                    string str39 = LSRequest.qq("pkbjl_money_" + str37);
                    cz_autosale_pkbjl _pkbjl = new cz_autosale_pkbjl();
                    _pkbjl.set_play_id(new int?(Convert.ToInt32(str37)));
                    _pkbjl.set_u_name(this.Session["user_name"].ToString());
                    _pkbjl.set_max_money(new decimal?(!string.IsNullOrEmpty(str39) ? Convert.ToDecimal(str39) : 0M));
                    _pkbjl.set_play_name(str38);
                    _pkbjl.set_sale_type(0);
                    _pkbjl.set_set_type(0);
                    string str40 = LSRequest.qq("pkbjl_chk_" + str37);
                    try
                    {
                        CallBLL.cz_autosale_pkbjl_bll.SetAutosale(_pkbjl, str40);
                    }
                    catch
                    {
                        flag33 = false;
                        break;
                    }
                    if ((table == null) && (str40 == "1"))
                    {
                        base.autosale_set_kc_log(null, _pkbjl, str40, 8);
                    }
                    else
                    {
                        DataRow[] rowArray10 = table.Select(string.Format(" play_id={0} ", _pkbjl.get_play_id()));
                        if ((rowArray10.Length <= 0) && (str40 == "1"))
                        {
                            base.autosale_set_kc_log(rowArray10, _pkbjl, str40, 8);
                        }
                        else if (rowArray10.Length == 1)
                        {
                            base.autosale_set_kc_log(rowArray10, _pkbjl, str40, 8);
                        }
                    }
                }
                if (!flag33)
                {
                    flag10 = false;
                }
            }
            if (this.play_jscarDT != null)
            {
                table = CallBLL.cz_autosale_jscar_bll.GetAutosaleByUserName(this.Session["user_name"].ToString()).Tables[0];
                bool flag34 = true;
                for (int num11 = 0; num11 < this.play_jscarDT.Rows.Count; num11++)
                {
                    string str41 = this.play_jscarDT.Rows[num11]["play_id"].ToString();
                    string str42 = this.play_jscarDT.Rows[num11]["play_name"].ToString();
                    string str43 = LSRequest.qq("jscar_money_" + str41);
                    cz_autosale_jscar _jscar = new cz_autosale_jscar();
                    _jscar.set_play_id(new int?(Convert.ToInt32(str41)));
                    _jscar.set_u_name(this.Session["user_name"].ToString());
                    _jscar.set_max_money(new decimal?(!string.IsNullOrEmpty(str43) ? Convert.ToDecimal(str43) : 0M));
                    _jscar.set_play_name(str42);
                    _jscar.set_sale_type(0);
                    _jscar.set_set_type(0);
                    string str44 = LSRequest.qq("jscar_chk_" + str41);
                    try
                    {
                        CallBLL.cz_autosale_jscar_bll.SetAutosale(_jscar, str44);
                    }
                    catch
                    {
                        flag34 = false;
                        break;
                    }
                    if ((table == null) && (str44 == "1"))
                    {
                        base.autosale_set_kc_log(null, _jscar, str44, 10);
                    }
                    else
                    {
                        DataRow[] rowArray11 = table.Select(string.Format(" play_id={0} ", _jscar.get_play_id()));
                        if ((rowArray11.Length <= 0) && (str44 == "1"))
                        {
                            base.autosale_set_kc_log(rowArray11, _jscar, str44, 10);
                        }
                        else if (rowArray11.Length == 1)
                        {
                            base.autosale_set_kc_log(rowArray11, _jscar, str44, 10);
                        }
                    }
                }
                if (!flag34)
                {
                    flag11 = false;
                }
            }
            if (this.play_speed5DT != null)
            {
                table = CallBLL.cz_autosale_speed5_bll.GetAutosaleByUserName(this.Session["user_name"].ToString()).Tables[0];
                bool flag35 = true;
                for (int num12 = 0; num12 < this.play_speed5DT.Rows.Count; num12++)
                {
                    string str45 = this.play_speed5DT.Rows[num12]["play_id"].ToString();
                    string str46 = this.play_speed5DT.Rows[num12]["play_name"].ToString();
                    string str47 = LSRequest.qq("speed5_money_" + str45);
                    cz_autosale_speed5 _speed = new cz_autosale_speed5();
                    _speed.set_play_id(new int?(Convert.ToInt32(str45)));
                    _speed.set_u_name(this.Session["user_name"].ToString());
                    _speed.set_max_money(new decimal?(!string.IsNullOrEmpty(str47) ? Convert.ToDecimal(str47) : 0M));
                    _speed.set_play_name(str46);
                    _speed.set_sale_type(0);
                    _speed.set_set_type(0);
                    string str48 = LSRequest.qq("speed5_chk_" + str45);
                    try
                    {
                        CallBLL.cz_autosale_speed5_bll.SetAutosale(_speed, str48);
                    }
                    catch
                    {
                        flag35 = false;
                        break;
                    }
                    if ((table == null) && (str48 == "1"))
                    {
                        base.autosale_set_kc_log(null, _speed, str48, 11);
                    }
                    else
                    {
                        DataRow[] rowArray12 = table.Select(string.Format(" play_id={0} ", _speed.get_play_id()));
                        if ((rowArray12.Length <= 0) && (str48 == "1"))
                        {
                            base.autosale_set_kc_log(rowArray12, _speed, str48, 11);
                        }
                        else if (rowArray12.Length == 1)
                        {
                            base.autosale_set_kc_log(rowArray12, _speed, str48, 11);
                        }
                    }
                }
                if (!flag35)
                {
                    flag12 = false;
                }
            }
            if (this.play_jscqscDT != null)
            {
                table = CallBLL.cz_autosale_jscqsc_bll.GetAutosaleByUserName(this.Session["user_name"].ToString()).Tables[0];
                bool flag36 = true;
                for (int num13 = 0; num13 < this.play_jscqscDT.Rows.Count; num13++)
                {
                    string str49 = this.play_jscqscDT.Rows[num13]["play_id"].ToString();
                    string str50 = this.play_jscqscDT.Rows[num13]["play_name"].ToString();
                    string str51 = LSRequest.qq("jscqsc_money_" + str49);
                    cz_autosale_jscqsc _jscqsc = new cz_autosale_jscqsc();
                    _jscqsc.set_play_id(new int?(Convert.ToInt32(str49)));
                    _jscqsc.set_u_name(this.Session["user_name"].ToString());
                    _jscqsc.set_max_money(new decimal?(!string.IsNullOrEmpty(str51) ? Convert.ToDecimal(str51) : 0M));
                    _jscqsc.set_play_name(str50);
                    _jscqsc.set_sale_type(0);
                    _jscqsc.set_set_type(0);
                    string str52 = LSRequest.qq("jscqsc_chk_" + str49);
                    try
                    {
                        CallBLL.cz_autosale_jscqsc_bll.SetAutosale(_jscqsc, str52);
                    }
                    catch
                    {
                        flag36 = false;
                        break;
                    }
                    if ((table == null) && (str52 == "1"))
                    {
                        base.autosale_set_kc_log(null, _jscqsc, str52, 13);
                    }
                    else
                    {
                        DataRow[] rowArray13 = table.Select(string.Format(" play_id={0} ", _jscqsc.get_play_id()));
                        if ((rowArray13.Length <= 0) && (str52 == "1"))
                        {
                            base.autosale_set_kc_log(rowArray13, _jscqsc, str52, 13);
                        }
                        else if (rowArray13.Length == 1)
                        {
                            base.autosale_set_kc_log(rowArray13, _jscqsc, str52, 13);
                        }
                    }
                }
                if (!flag36)
                {
                    flag13 = false;
                }
            }
            if (this.play_jspk10DT != null)
            {
                table = CallBLL.cz_autosale_jspk10_bll.GetAutosaleByUserName(this.Session["user_name"].ToString()).Tables[0];
                bool flag37 = true;
                for (int num14 = 0; num14 < this.play_jspk10DT.Rows.Count; num14++)
                {
                    string str53 = this.play_jspk10DT.Rows[num14]["play_id"].ToString();
                    string str54 = this.play_jspk10DT.Rows[num14]["play_name"].ToString();
                    string str55 = LSRequest.qq("jspk10_money_" + str53);
                    cz_autosale_jspk10 _jspk = new cz_autosale_jspk10();
                    _jspk.set_play_id(new int?(Convert.ToInt32(str53)));
                    _jspk.set_u_name(this.Session["user_name"].ToString());
                    _jspk.set_max_money(new decimal?(!string.IsNullOrEmpty(str55) ? Convert.ToDecimal(str55) : 0M));
                    _jspk.set_play_name(str54);
                    _jspk.set_sale_type(0);
                    _jspk.set_set_type(0);
                    string str56 = LSRequest.qq("jspk10_chk_" + str53);
                    try
                    {
                        CallBLL.cz_autosale_jspk10_bll.SetAutosale(_jspk, str56);
                    }
                    catch
                    {
                        flag37 = false;
                        break;
                    }
                    if ((table == null) && (str56 == "1"))
                    {
                        base.autosale_set_kc_log(null, _jspk, str56, 12);
                    }
                    else
                    {
                        DataRow[] rowArray14 = table.Select(string.Format(" play_id={0} ", _jspk.get_play_id()));
                        if ((rowArray14.Length <= 0) && (str56 == "1"))
                        {
                            base.autosale_set_kc_log(rowArray14, _jspk, str56, 12);
                        }
                        else if (rowArray14.Length == 1)
                        {
                            base.autosale_set_kc_log(rowArray14, _jspk, str56, 12);
                        }
                    }
                }
                if (!flag37)
                {
                    flag14 = false;
                }
            }
            table = CallBLL.cz_autosale_jssfc_bll.GetAutosaleByUserName(this.Session["user_name"].ToString()).Tables[0];
            if (this.play_jssfcDT != null)
            {
                bool flag38 = true;
                for (int num15 = 0; num15 < this.play_jssfcDT.Rows.Count; num15++)
                {
                    string str57 = this.play_jssfcDT.Rows[num15]["play_id"].ToString();
                    string str58 = this.play_jssfcDT.Rows[num15]["play_name"].ToString();
                    string str59 = LSRequest.qq("jssfc_money_" + str57);
                    cz_autosale_jssfc _jssfc = new cz_autosale_jssfc();
                    _jssfc.set_play_id(new int?(Convert.ToInt32(str57)));
                    _jssfc.set_u_name(this.Session["user_name"].ToString());
                    _jssfc.set_max_money(new decimal?(!string.IsNullOrEmpty(str59) ? Convert.ToDecimal(str59) : 0M));
                    _jssfc.set_play_name(str58);
                    _jssfc.set_sale_type(0);
                    _jssfc.set_set_type(0);
                    string str60 = LSRequest.qq("jssfc_chk_" + str57);
                    try
                    {
                        CallBLL.cz_autosale_jssfc_bll.SetAutosale(_jssfc, str60);
                    }
                    catch
                    {
                        flag38 = false;
                        break;
                    }
                    if ((table == null) && (str60 == "1"))
                    {
                        base.autosale_set_kc_log(null, _jssfc, str60, 14);
                    }
                    else
                    {
                        DataRow[] rowArray15 = table.Select(string.Format(" play_id={0} ", _jssfc.get_play_id()));
                        if ((rowArray15.Length <= 0) && (str60 == "1"))
                        {
                            base.autosale_set_kc_log(rowArray15, _jssfc, str60, 14);
                        }
                        else if (rowArray15.Length == 1)
                        {
                            base.autosale_set_kc_log(rowArray15, _jssfc, str60, 14);
                        }
                    }
                }
                if (!flag38)
                {
                    flag15 = false;
                }
            }
            if (this.play_jsft2DT != null)
            {
                table = CallBLL.cz_autosale_jsft2_bll.GetAutosaleByUserName(this.Session["user_name"].ToString()).Tables[0];
                bool flag39 = true;
                for (int num16 = 0; num16 < this.play_jsft2DT.Rows.Count; num16++)
                {
                    string str61 = this.play_jsft2DT.Rows[num16]["play_id"].ToString();
                    string str62 = this.play_jsft2DT.Rows[num16]["play_name"].ToString();
                    string str63 = LSRequest.qq("jsft2_money_" + str61);
                    cz_autosale_jsft2 _jsft = new cz_autosale_jsft2();
                    _jsft.set_play_id(new int?(Convert.ToInt32(str61)));
                    _jsft.set_u_name(this.Session["user_name"].ToString());
                    _jsft.set_max_money(new decimal?(!string.IsNullOrEmpty(str63) ? Convert.ToDecimal(str63) : 0M));
                    _jsft.set_play_name(str62);
                    _jsft.set_sale_type(0);
                    _jsft.set_set_type(0);
                    string str64 = LSRequest.qq("jsft2_chk_" + str61);
                    try
                    {
                        CallBLL.cz_autosale_jsft2_bll.SetAutosale(_jsft, str64);
                    }
                    catch
                    {
                        flag39 = false;
                        break;
                    }
                    if ((table == null) && (str64 == "1"))
                    {
                        base.autosale_set_kc_log(null, _jsft, str64, 15);
                    }
                    else
                    {
                        DataRow[] rowArray16 = table.Select(string.Format(" play_id={0} ", _jsft.get_play_id()));
                        if ((rowArray16.Length <= 0) && (str64 == "1"))
                        {
                            base.autosale_set_kc_log(rowArray16, _jsft, str64, 15);
                        }
                        else if (rowArray16.Length == 1)
                        {
                            base.autosale_set_kc_log(rowArray16, _jsft, str64, 15);
                        }
                    }
                }
                if (!flag39)
                {
                    flag16 = false;
                }
            }
            if (this.play_car168DT != null)
            {
                table = CallBLL.cz_autosale_car168_bll.GetAutosaleByUserName(this.Session["user_name"].ToString()).Tables[0];
                bool flag40 = true;
                for (int num17 = 0; num17 < this.play_car168DT.Rows.Count; num17++)
                {
                    string str65 = this.play_car168DT.Rows[num17]["play_id"].ToString();
                    string str66 = this.play_car168DT.Rows[num17]["play_name"].ToString();
                    string str67 = LSRequest.qq("car168_money_" + str65);
                    cz_autosale_car168 _car = new cz_autosale_car168();
                    _car.set_play_id(new int?(Convert.ToInt32(str65)));
                    _car.set_u_name(this.Session["user_name"].ToString());
                    _car.set_max_money(new decimal?(!string.IsNullOrEmpty(str67) ? Convert.ToDecimal(str67) : 0M));
                    _car.set_play_name(str66);
                    _car.set_sale_type(0);
                    _car.set_set_type(0);
                    string str68 = LSRequest.qq("car168_chk_" + str65);
                    try
                    {
                        CallBLL.cz_autosale_car168_bll.SetAutosale(_car, str68);
                    }
                    catch
                    {
                        flag40 = false;
                        break;
                    }
                    if ((table == null) && (str68 == "1"))
                    {
                        base.autosale_set_kc_log(null, _car, str68, 0x10);
                    }
                    else
                    {
                        DataRow[] rowArray17 = table.Select(string.Format(" play_id={0} ", _car.get_play_id()));
                        if ((rowArray17.Length <= 0) && (str68 == "1"))
                        {
                            base.autosale_set_kc_log(rowArray17, _car, str68, 0x10);
                        }
                        else if (rowArray17.Length == 1)
                        {
                            base.autosale_set_kc_log(rowArray17, _car, str68, 0x10);
                        }
                    }
                }
                if (!flag40)
                {
                    flag17 = false;
                }
            }
            if (this.play_ssc168DT != null)
            {
                table = CallBLL.cz_autosale_ssc168_bll.GetAutosaleByUserName(this.Session["user_name"].ToString()).Tables[0];
                bool flag41 = true;
                for (int num18 = 0; num18 < this.play_ssc168DT.Rows.Count; num18++)
                {
                    string str69 = this.play_ssc168DT.Rows[num18]["play_id"].ToString();
                    string str70 = this.play_ssc168DT.Rows[num18]["play_name"].ToString();
                    string str71 = LSRequest.qq("ssc168_money_" + str69);
                    cz_autosale_ssc168 _ssc = new cz_autosale_ssc168();
                    _ssc.set_play_id(new int?(Convert.ToInt32(str69)));
                    _ssc.set_u_name(this.Session["user_name"].ToString());
                    _ssc.set_max_money(new decimal?(!string.IsNullOrEmpty(str71) ? Convert.ToDecimal(str71) : 0M));
                    _ssc.set_play_name(str70);
                    _ssc.set_sale_type(0);
                    _ssc.set_set_type(0);
                    string str72 = LSRequest.qq("ssc168_chk_" + str69);
                    try
                    {
                        CallBLL.cz_autosale_ssc168_bll.SetAutosale(_ssc, str72);
                    }
                    catch
                    {
                        flag41 = false;
                        break;
                    }
                    if ((table == null) && (str72 == "1"))
                    {
                        base.autosale_set_kc_log(null, _ssc, str72, 0x11);
                    }
                    else
                    {
                        DataRow[] rowArray18 = table.Select(string.Format(" play_id={0} ", _ssc.get_play_id()));
                        if ((rowArray18.Length <= 0) && (str72 == "1"))
                        {
                            base.autosale_set_kc_log(rowArray18, _ssc, str72, 0x11);
                        }
                        else if (rowArray18.Length == 1)
                        {
                            base.autosale_set_kc_log(rowArray18, _ssc, str72, 0x11);
                        }
                    }
                }
                if (!flag41)
                {
                    flag18 = false;
                }
            }
            if (this.play_vrcarDT != null)
            {
                table = CallBLL.cz_autosale_vrcar_bll.GetAutosaleByUserName(this.Session["user_name"].ToString()).Tables[0];
                bool flag42 = true;
                for (int num19 = 0; num19 < this.play_vrcarDT.Rows.Count; num19++)
                {
                    string str73 = this.play_vrcarDT.Rows[num19]["play_id"].ToString();
                    string str74 = this.play_vrcarDT.Rows[num19]["play_name"].ToString();
                    string str75 = LSRequest.qq("vrcar_money_" + str73);
                    cz_autosale_vrcar _vrcar = new cz_autosale_vrcar();
                    _vrcar.set_play_id(new int?(Convert.ToInt32(str73)));
                    _vrcar.set_u_name(this.Session["user_name"].ToString());
                    _vrcar.set_max_money(new decimal?(!string.IsNullOrEmpty(str75) ? Convert.ToDecimal(str75) : 0M));
                    _vrcar.set_play_name(str74);
                    _vrcar.set_sale_type(0);
                    _vrcar.set_set_type(0);
                    string str76 = LSRequest.qq("vrcar_chk_" + str73);
                    try
                    {
                        CallBLL.cz_autosale_vrcar_bll.SetAutosale(_vrcar, str76);
                    }
                    catch
                    {
                        flag42 = false;
                        break;
                    }
                    if ((table == null) && (str76 == "1"))
                    {
                        base.autosale_set_kc_log(null, _vrcar, str76, 0x12);
                    }
                    else
                    {
                        DataRow[] rowArray19 = table.Select(string.Format(" play_id={0} ", _vrcar.get_play_id()));
                        if ((rowArray19.Length <= 0) && (str76 == "1"))
                        {
                            base.autosale_set_kc_log(rowArray19, _vrcar, str76, 0x12);
                        }
                        else if (rowArray19.Length == 1)
                        {
                            base.autosale_set_kc_log(rowArray19, _vrcar, str76, 0x12);
                        }
                    }
                }
                if (!flag42)
                {
                    flag19 = false;
                }
            }
            if (this.play_vrsscDT != null)
            {
                table = CallBLL.cz_autosale_vrssc_bll.GetAutosaleByUserName(this.Session["user_name"].ToString()).Tables[0];
                bool flag43 = true;
                for (int num20 = 0; num20 < this.play_vrsscDT.Rows.Count; num20++)
                {
                    string str77 = this.play_vrsscDT.Rows[num20]["play_id"].ToString();
                    string str78 = this.play_vrsscDT.Rows[num20]["play_name"].ToString();
                    string str79 = LSRequest.qq("vrssc_money_" + str77);
                    cz_autosale_vrssc _vrssc = new cz_autosale_vrssc();
                    _vrssc.set_play_id(new int?(Convert.ToInt32(str77)));
                    _vrssc.set_u_name(this.Session["user_name"].ToString());
                    _vrssc.set_max_money(new decimal?(!string.IsNullOrEmpty(str79) ? Convert.ToDecimal(str79) : 0M));
                    _vrssc.set_play_name(str78);
                    _vrssc.set_sale_type(0);
                    _vrssc.set_set_type(0);
                    string str80 = LSRequest.qq("vrssc_chk_" + str77);
                    try
                    {
                        CallBLL.cz_autosale_vrssc_bll.SetAutosale(_vrssc, str80);
                    }
                    catch
                    {
                        flag43 = false;
                        break;
                    }
                    if ((table == null) && (str80 == "1"))
                    {
                        base.autosale_set_kc_log(null, _vrssc, str80, 0x13);
                    }
                    else
                    {
                        DataRow[] rowArray20 = table.Select(string.Format(" play_id={0} ", _vrssc.get_play_id()));
                        if ((rowArray20.Length <= 0) && (str80 == "1"))
                        {
                            base.autosale_set_kc_log(rowArray20, _vrssc, str80, 0x13);
                        }
                        else if (rowArray20.Length == 1)
                        {
                            base.autosale_set_kc_log(rowArray20, _vrssc, str80, 0x13);
                        }
                    }
                }
                if (!flag43)
                {
                    flag20 = false;
                }
            }
            if (this.play_xyftoaDT != null)
            {
                table = CallBLL.cz_autosale_xyftoa_bll.GetAutosaleByUserName(this.Session["user_name"].ToString()).Tables[0];
                bool flag44 = true;
                for (int num21 = 0; num21 < this.play_xyftoaDT.Rows.Count; num21++)
                {
                    string str81 = this.play_xyftoaDT.Rows[num21]["play_id"].ToString();
                    string str82 = this.play_xyftoaDT.Rows[num21]["play_name"].ToString();
                    string str83 = LSRequest.qq("xyftoa_money_" + str81);
                    cz_autosale_xyftoa _xyftoa = new cz_autosale_xyftoa();
                    _xyftoa.set_play_id(new int?(Convert.ToInt32(str81)));
                    _xyftoa.set_u_name(this.Session["user_name"].ToString());
                    _xyftoa.set_max_money(new decimal?(!string.IsNullOrEmpty(str83) ? Convert.ToDecimal(str83) : 0M));
                    _xyftoa.set_play_name(str82);
                    _xyftoa.set_sale_type(0);
                    _xyftoa.set_set_type(0);
                    string str84 = LSRequest.qq("xyftoa_chk_" + str81);
                    try
                    {
                        CallBLL.cz_autosale_xyftoa_bll.SetAutosale(_xyftoa, str84);
                    }
                    catch
                    {
                        flag44 = false;
                        break;
                    }
                    if ((table == null) && (str84 == "1"))
                    {
                        base.autosale_set_kc_log(null, _xyftoa, str84, 20);
                    }
                    else
                    {
                        DataRow[] rowArray21 = table.Select(string.Format(" play_id={0} ", _xyftoa.get_play_id()));
                        if ((rowArray21.Length <= 0) && (str84 == "1"))
                        {
                            base.autosale_set_kc_log(rowArray21, _xyftoa, str84, 20);
                        }
                        else if (rowArray21.Length == 1)
                        {
                            base.autosale_set_kc_log(rowArray21, _xyftoa, str84, 20);
                        }
                    }
                }
                if (!flag44)
                {
                    flag21 = false;
                }
            }
            if (this.play_xyftsgDT != null)
            {
                table = CallBLL.cz_autosale_xyftsg_bll.GetAutosaleByUserName(this.Session["user_name"].ToString()).Tables[0];
                bool flag45 = true;
                for (int num22 = 0; num22 < this.play_xyftsgDT.Rows.Count; num22++)
                {
                    string str85 = this.play_xyftsgDT.Rows[num22]["play_id"].ToString();
                    string str86 = this.play_xyftsgDT.Rows[num22]["play_name"].ToString();
                    string str87 = LSRequest.qq("xyftsg_money_" + str85);
                    cz_autosale_xyftsg _xyftsg = new cz_autosale_xyftsg();
                    _xyftsg.set_play_id(new int?(Convert.ToInt32(str85)));
                    _xyftsg.set_u_name(this.Session["user_name"].ToString());
                    _xyftsg.set_max_money(new decimal?(!string.IsNullOrEmpty(str87) ? Convert.ToDecimal(str87) : 0M));
                    _xyftsg.set_play_name(str86);
                    _xyftsg.set_sale_type(0);
                    _xyftsg.set_set_type(0);
                    string str88 = LSRequest.qq("xyftsg_chk_" + str85);
                    try
                    {
                        CallBLL.cz_autosale_xyftsg_bll.SetAutosale(_xyftsg, str88);
                    }
                    catch
                    {
                        flag45 = false;
                        break;
                    }
                    if ((table == null) && (str88 == "1"))
                    {
                        base.autosale_set_kc_log(null, _xyftsg, str88, 0x15);
                    }
                    else
                    {
                        DataRow[] rowArray22 = table.Select(string.Format(" play_id={0} ", _xyftsg.get_play_id()));
                        if ((rowArray22.Length <= 0) && (str88 == "1"))
                        {
                            base.autosale_set_kc_log(rowArray22, _xyftsg, str88, 0x15);
                        }
                        else if (rowArray22.Length == 1)
                        {
                            base.autosale_set_kc_log(rowArray22, _xyftsg, str88, 0x15);
                        }
                    }
                }
                if (!flag45)
                {
                    flag22 = false;
                }
            }
            if (this.play_happycarDT != null)
            {
                table = CallBLL.cz_autosale_happycar_bll.GetAutosaleByUserName(this.Session["user_name"].ToString()).Tables[0];
                bool flag46 = true;
                for (int num23 = 0; num23 < this.play_happycarDT.Rows.Count; num23++)
                {
                    string str89 = this.play_happycarDT.Rows[num23]["play_id"].ToString();
                    string str90 = this.play_happycarDT.Rows[num23]["play_name"].ToString();
                    string str91 = LSRequest.qq("happycar_money_" + str89);
                    cz_autosale_happycar _happycar = new cz_autosale_happycar();
                    _happycar.set_play_id(new int?(Convert.ToInt32(str89)));
                    _happycar.set_u_name(this.Session["user_name"].ToString());
                    _happycar.set_max_money(new decimal?(!string.IsNullOrEmpty(str91) ? Convert.ToDecimal(str91) : 0M));
                    _happycar.set_play_name(str90);
                    _happycar.set_sale_type(0);
                    _happycar.set_set_type(0);
                    string str92 = LSRequest.qq("happycar_chk_" + str89);
                    try
                    {
                        CallBLL.cz_autosale_happycar_bll.SetAutosale(_happycar, str92);
                    }
                    catch
                    {
                        flag46 = false;
                        break;
                    }
                    if ((table == null) && (str92 == "1"))
                    {
                        base.autosale_set_kc_log(null, _happycar, str92, 0x16);
                    }
                    else
                    {
                        DataRow[] rowArray23 = table.Select(string.Format(" play_id={0} ", _happycar.get_play_id()));
                        if ((rowArray23.Length <= 0) && (str92 == "1"))
                        {
                            base.autosale_set_kc_log(rowArray23, _happycar, str92, 0x16);
                        }
                        else if (rowArray23.Length == 1)
                        {
                            base.autosale_set_kc_log(rowArray23, _happycar, str92, 0x16);
                        }
                    }
                }
                if (!flag46)
                {
                    flag23 = false;
                }
            }
            if ((((flag || flag2) || (flag3 || flag4)) || ((flag6 || flag5) || (flag7 || flag8))) || ((((flag9 || flag10) || (flag11 || flag12)) || ((flag13 || flag14) || (flag15 || flag16))) || (((flag17 || flag18) || (flag19 || flag20)) || ((flag21 || flag22) || flag23))))
            {
                string url = "/AutoLet/AutoLet_kc.aspx?lid=" + this.lotteryId;
                base.Response.Write(base.ShowDialogBox("設置自動補貨成功！", url, 0));
                base.Response.End();
            }
            else
            {
                base.Response.Write(base.ShowDialogBox("設置自動補貨失敗！", "", 400));
                base.Response.End();
            }
        }

        private void ValidInput()
        {
            bool flag = true;
            if (this.play_kl10DT != null)
            {
                for (int i = 0; i < this.play_kl10DT.Rows.Count; i++)
                {
                    string str = this.play_kl10DT.Rows[i]["play_id"].ToString();
                    string str2 = LSRequest.qq("kl10_money_" + str);
                    if (LSRequest.qq("kl10_chk_" + str).Equals("1"))
                    {
                        bool flag2 = Utils.IsInteger(str2);
                        bool flag3 = Utils.IsIntMoney(str2);
                        if (!flag2 || !flag3)
                        {
                            flag = false;
                            break;
                        }
                    }
                }
                if (!flag)
                {
                    base.Response.Write(base.ShowDialogBox("請輸入有效的”控製額度“！", "", 400));
                    base.Response.End();
                }
            }
            if (this.play_cqscDT != null)
            {
                for (int j = 0; j < this.play_cqscDT.Rows.Count; j++)
                {
                    string str4 = this.play_cqscDT.Rows[j]["play_id"].ToString();
                    string str5 = LSRequest.qq("cqsc_money_" + str4);
                    if (LSRequest.qq("cqsc_chk_" + str4).Equals("1"))
                    {
                        bool flag4 = Utils.IsInteger(str5);
                        bool flag5 = Utils.IsIntMoney(str5);
                        if (!flag4 || !flag5)
                        {
                            flag = false;
                            break;
                        }
                    }
                }
                if (!flag)
                {
                    base.Response.Write(base.ShowDialogBox("請輸入有效的”控製額度“！", "", 400));
                    base.Response.End();
                }
            }
            if (this.play_pk10DT != null)
            {
                for (int k = 0; k < this.play_pk10DT.Rows.Count; k++)
                {
                    string str7 = this.play_pk10DT.Rows[k]["play_id"].ToString();
                    string str8 = LSRequest.qq("pk10_money_" + str7);
                    if (LSRequest.qq("pk10_chk_" + str7).Equals("1"))
                    {
                        bool flag6 = Utils.IsInteger(str8);
                        bool flag7 = Utils.IsIntMoney(str8);
                        if (!flag6 || !flag7)
                        {
                            flag = false;
                            break;
                        }
                    }
                }
                if (!flag)
                {
                    base.Response.Write(base.ShowDialogBox("請輸入有效的”控製額度“！", "", 400));
                    base.Response.End();
                }
            }
            if (this.play_xyncDT != null)
            {
                for (int m = 0; m < this.play_xyncDT.Rows.Count; m++)
                {
                    string str10 = this.play_xyncDT.Rows[m]["play_id"].ToString();
                    string str11 = LSRequest.qq("xync_money_" + str10);
                    if (LSRequest.qq("xync_chk_" + str10).Equals("1"))
                    {
                        bool flag8 = Utils.IsInteger(str11);
                        bool flag9 = Utils.IsIntMoney(str11);
                        if (!flag8 || !flag9)
                        {
                            flag = false;
                            break;
                        }
                    }
                }
                if (!flag)
                {
                    base.Response.Write(base.ShowDialogBox("請輸入有效的”控製額度“！", "", 400));
                    base.Response.End();
                }
            }
            if (this.play_jsk3DT != null)
            {
                for (int n = 0; n < this.play_jsk3DT.Rows.Count; n++)
                {
                    string str13 = this.play_jsk3DT.Rows[n]["play_id"].ToString();
                    string str14 = LSRequest.qq("jsk3_money_" + str13);
                    if (LSRequest.qq("jsk3_chk_" + str13).Equals("1"))
                    {
                        bool flag10 = Utils.IsInteger(str14);
                        bool flag11 = Utils.IsIntMoney(str14);
                        if (!flag10 || !flag11)
                        {
                            flag = false;
                            break;
                        }
                    }
                }
                if (!flag)
                {
                    base.Response.Write(base.ShowDialogBox("請輸入有效的”控製額度“！", "", 400));
                    base.Response.End();
                }
            }
            if (this.play_kl8DT != null)
            {
                for (int num6 = 0; num6 < this.play_kl8DT.Rows.Count; num6++)
                {
                    string str16 = this.play_kl8DT.Rows[num6]["play_id"].ToString();
                    string str17 = LSRequest.qq("kl8_money_" + str16);
                    if (LSRequest.qq("kl8_chk_" + str16).Equals("1"))
                    {
                        bool flag12 = Utils.IsInteger(str17);
                        bool flag13 = Utils.IsIntMoney(str17);
                        if (!flag12 || !flag13)
                        {
                            flag = false;
                            break;
                        }
                    }
                }
                if (!flag)
                {
                    base.Response.Write(base.ShowDialogBox("請輸入有效的”控製額度“！", "", 400));
                    base.Response.End();
                }
            }
            if (this.play_k8scDT != null)
            {
                for (int num7 = 0; num7 < this.play_k8scDT.Rows.Count; num7++)
                {
                    string str19 = this.play_k8scDT.Rows[num7]["play_id"].ToString();
                    string str20 = LSRequest.qq("k8sc_money_" + str19);
                    if (LSRequest.qq("k8sc_chk_" + str19).Equals("1"))
                    {
                        bool flag14 = Utils.IsInteger(str20);
                        bool flag15 = Utils.IsIntMoney(str20);
                        if (!flag14 || !flag15)
                        {
                            flag = false;
                            break;
                        }
                    }
                }
                if (!flag)
                {
                    base.Response.Write(base.ShowDialogBox("請輸入有效的”控製額度“！", "", 400));
                    base.Response.End();
                }
            }
            if (this.play_pcddDT != null)
            {
                for (int num8 = 0; num8 < this.play_pcddDT.Rows.Count; num8++)
                {
                    string str22 = this.play_pcddDT.Rows[num8]["play_id"].ToString();
                    string str23 = LSRequest.qq("pcdd_money_" + str22);
                    if (LSRequest.qq("pcdd_chk_" + str22).Equals("1"))
                    {
                        bool flag16 = Utils.IsInteger(str23);
                        bool flag17 = Utils.IsIntMoney(str23);
                        if (!flag16 || !flag17)
                        {
                            flag = false;
                            break;
                        }
                    }
                }
                if (!flag)
                {
                    base.Response.Write(base.ShowDialogBox("請輸入有效的”控製額度“！", "", 400));
                    base.Response.End();
                }
            }
            if (this.play_xyft5DT != null)
            {
                for (int num9 = 0; num9 < this.play_xyft5DT.Rows.Count; num9++)
                {
                    string str25 = this.play_xyft5DT.Rows[num9]["play_id"].ToString();
                    string str26 = LSRequest.qq("xyft5_money_" + str25);
                    if (LSRequest.qq("xyft5_chk_" + str25).Equals("1"))
                    {
                        bool flag18 = Utils.IsInteger(str26);
                        bool flag19 = Utils.IsIntMoney(str26);
                        if (!flag18 || !flag19)
                        {
                            flag = false;
                            break;
                        }
                    }
                }
                if (!flag)
                {
                    base.Response.Write(base.ShowDialogBox("請輸入有效的”控製額度“！", "", 400));
                    base.Response.End();
                }
            }
            if (this.play_pkbjlDT != null)
            {
                for (int num10 = 0; num10 < this.play_pkbjlDT.Rows.Count; num10++)
                {
                    string str28 = this.play_pkbjlDT.Rows[num10]["play_id"].ToString();
                    string str29 = LSRequest.qq("pkbjl_money_" + str28);
                    if (LSRequest.qq("pkbjl_chk_" + str28).Equals("1"))
                    {
                        bool flag20 = Utils.IsInteger(str29);
                        bool flag21 = Utils.IsIntMoney(str29);
                        if (!flag20 || !flag21)
                        {
                            flag = false;
                            break;
                        }
                    }
                }
                if (!flag)
                {
                    base.Response.Write(base.ShowDialogBox("請輸入有效的”控製額度“！", "", 400));
                    base.Response.End();
                }
            }
            if (this.play_jscarDT != null)
            {
                for (int num11 = 0; num11 < this.play_jscarDT.Rows.Count; num11++)
                {
                    string str31 = this.play_jscarDT.Rows[num11]["play_id"].ToString();
                    string str32 = LSRequest.qq("jscar_money_" + str31);
                    if (LSRequest.qq("jscar_chk_" + str31).Equals("1"))
                    {
                        bool flag22 = Utils.IsInteger(str32);
                        bool flag23 = Utils.IsIntMoney(str32);
                        if (!flag22 || !flag23)
                        {
                            flag = false;
                            break;
                        }
                    }
                }
                if (!flag)
                {
                    base.Response.Write(base.ShowDialogBox("請輸入有效的”控製額度“！", "", 400));
                    base.Response.End();
                }
            }
            if (this.play_speed5DT != null)
            {
                for (int num12 = 0; num12 < this.play_speed5DT.Rows.Count; num12++)
                {
                    string str34 = this.play_speed5DT.Rows[num12]["play_id"].ToString();
                    string str35 = LSRequest.qq("speed5_money_" + str34);
                    if (LSRequest.qq("speed5_chk_" + str34).Equals("1"))
                    {
                        bool flag24 = Utils.IsInteger(str35);
                        bool flag25 = Utils.IsIntMoney(str35);
                        if (!flag24 || !flag25)
                        {
                            flag = false;
                            break;
                        }
                    }
                }
                if (!flag)
                {
                    base.Response.Write(base.ShowDialogBox("請輸入有效的”控製額度“！", "", 400));
                    base.Response.End();
                }
            }
            if (this.play_jscqscDT != null)
            {
                for (int num13 = 0; num13 < this.play_jscqscDT.Rows.Count; num13++)
                {
                    string str37 = this.play_jscqscDT.Rows[num13]["play_id"].ToString();
                    string str38 = LSRequest.qq("jscqsc_money_" + str37);
                    if (LSRequest.qq("jscqsc_chk_" + str37).Equals("1"))
                    {
                        bool flag26 = Utils.IsInteger(str38);
                        bool flag27 = Utils.IsIntMoney(str38);
                        if (!flag26 || !flag27)
                        {
                            flag = false;
                            break;
                        }
                    }
                }
                if (!flag)
                {
                    base.Response.Write(base.ShowDialogBox("請輸入有效的”控製額度“！", "", 400));
                    base.Response.End();
                }
            }
            if (this.play_jspk10DT != null)
            {
                for (int num14 = 0; num14 < this.play_jspk10DT.Rows.Count; num14++)
                {
                    string str40 = this.play_jspk10DT.Rows[num14]["play_id"].ToString();
                    string str41 = LSRequest.qq("jspk10_money_" + str40);
                    if (LSRequest.qq("jspk10_chk_" + str40).Equals("1"))
                    {
                        bool flag28 = Utils.IsInteger(str41);
                        bool flag29 = Utils.IsIntMoney(str41);
                        if (!flag28 || !flag29)
                        {
                            flag = false;
                            break;
                        }
                    }
                }
                if (!flag)
                {
                    base.Response.Write(base.ShowDialogBox("請輸入有效的”控製額度“！", "", 400));
                    base.Response.End();
                }
            }
            if (this.play_jssfcDT != null)
            {
                for (int num15 = 0; num15 < this.play_jssfcDT.Rows.Count; num15++)
                {
                    string str43 = this.play_jssfcDT.Rows[num15]["play_id"].ToString();
                    string str44 = LSRequest.qq("jssfc_money_" + str43);
                    if (LSRequest.qq("jssfc_chk_" + str43).Equals("1"))
                    {
                        bool flag30 = Utils.IsInteger(str44);
                        bool flag31 = Utils.IsIntMoney(str44);
                        if (!flag30 || !flag31)
                        {
                            flag = false;
                            break;
                        }
                    }
                }
                if (!flag)
                {
                    base.Response.Write(base.ShowDialogBox("請輸入有效的”控製額度“！", "", 400));
                    base.Response.End();
                }
            }
            if (this.play_jsft2DT != null)
            {
                for (int num16 = 0; num16 < this.play_jsft2DT.Rows.Count; num16++)
                {
                    string str46 = this.play_jsft2DT.Rows[num16]["play_id"].ToString();
                    string str47 = LSRequest.qq("jsft2_money_" + str46);
                    if (LSRequest.qq("jsft2_chk_" + str46).Equals("1"))
                    {
                        bool flag32 = Utils.IsInteger(str47);
                        bool flag33 = Utils.IsIntMoney(str47);
                        if (!flag32 || !flag33)
                        {
                            flag = false;
                            break;
                        }
                    }
                }
                if (!flag)
                {
                    base.Response.Write(base.ShowDialogBox("請輸入有效的”控製額度“！", "", 400));
                    base.Response.End();
                }
            }
            if (this.play_car168DT != null)
            {
                for (int num17 = 0; num17 < this.play_car168DT.Rows.Count; num17++)
                {
                    string str49 = this.play_car168DT.Rows[num17]["play_id"].ToString();
                    string str50 = LSRequest.qq("car168_money_" + str49);
                    if (LSRequest.qq("car168_chk_" + str49).Equals("1"))
                    {
                        bool flag34 = Utils.IsInteger(str50);
                        bool flag35 = Utils.IsIntMoney(str50);
                        if (!flag34 || !flag35)
                        {
                            flag = false;
                            break;
                        }
                    }
                }
                if (!flag)
                {
                    base.Response.Write(base.ShowDialogBox("請輸入有效的”控製額度“！", "", 400));
                    base.Response.End();
                }
            }
            if (this.play_ssc168DT != null)
            {
                for (int num18 = 0; num18 < this.play_ssc168DT.Rows.Count; num18++)
                {
                    string str52 = this.play_ssc168DT.Rows[num18]["play_id"].ToString();
                    string str53 = LSRequest.qq("ssc168_money_" + str52);
                    if (LSRequest.qq("ssc168_chk_" + str52).Equals("1"))
                    {
                        bool flag36 = Utils.IsInteger(str53);
                        bool flag37 = Utils.IsIntMoney(str53);
                        if (!flag36 || !flag37)
                        {
                            flag = false;
                            break;
                        }
                    }
                }
                if (!flag)
                {
                    base.Response.Write(base.ShowDialogBox("請輸入有效的”控製額度“！", "", 400));
                    base.Response.End();
                }
            }
            if (this.play_vrcarDT != null)
            {
                for (int num19 = 0; num19 < this.play_vrcarDT.Rows.Count; num19++)
                {
                    string str55 = this.play_vrcarDT.Rows[num19]["play_id"].ToString();
                    string str56 = LSRequest.qq("vrcar_money_" + str55);
                    if (LSRequest.qq("vrcar_chk_" + str55).Equals("1"))
                    {
                        bool flag38 = Utils.IsInteger(str56);
                        bool flag39 = Utils.IsIntMoney(str56);
                        if (!flag38 || !flag39)
                        {
                            flag = false;
                            break;
                        }
                    }
                }
                if (!flag)
                {
                    base.Response.Write(base.ShowDialogBox("請輸入有效的”控製額度“！", "", 400));
                    base.Response.End();
                }
            }
            if (this.play_vrsscDT != null)
            {
                for (int num20 = 0; num20 < this.play_vrsscDT.Rows.Count; num20++)
                {
                    string str58 = this.play_vrsscDT.Rows[num20]["play_id"].ToString();
                    string str59 = LSRequest.qq("vrssc_money_" + str58);
                    if (LSRequest.qq("vrssc_chk_" + str58).Equals("1"))
                    {
                        bool flag40 = Utils.IsInteger(str59);
                        bool flag41 = Utils.IsIntMoney(str59);
                        if (!flag40 || !flag41)
                        {
                            flag = false;
                            break;
                        }
                    }
                }
                if (!flag)
                {
                    base.Response.Write(base.ShowDialogBox("請輸入有效的”控製額度“！", "", 400));
                    base.Response.End();
                }
            }
            if (this.play_xyftoaDT != null)
            {
                for (int num21 = 0; num21 < this.play_xyftoaDT.Rows.Count; num21++)
                {
                    string str61 = this.play_xyftoaDT.Rows[num21]["play_id"].ToString();
                    string str62 = LSRequest.qq("xyftoa_money_" + str61);
                    if (LSRequest.qq("xyftoa_chk_" + str61).Equals("1"))
                    {
                        bool flag42 = Utils.IsInteger(str62);
                        bool flag43 = Utils.IsIntMoney(str62);
                        if (!flag42 || !flag43)
                        {
                            flag = false;
                            break;
                        }
                    }
                }
                if (!flag)
                {
                    base.Response.Write(base.ShowDialogBox("請輸入有效的”控製額度“！", "", 400));
                    base.Response.End();
                }
            }
            if (this.play_xyftsgDT != null)
            {
                for (int num22 = 0; num22 < this.play_xyftsgDT.Rows.Count; num22++)
                {
                    string str64 = this.play_xyftsgDT.Rows[num22]["play_id"].ToString();
                    string str65 = LSRequest.qq("xyftsg_money_" + str64);
                    if (LSRequest.qq("xyftsg_chk_" + str64).Equals("1"))
                    {
                        bool flag44 = Utils.IsInteger(str65);
                        bool flag45 = Utils.IsIntMoney(str65);
                        if (!flag44 || !flag45)
                        {
                            flag = false;
                            break;
                        }
                    }
                }
                if (!flag)
                {
                    base.Response.Write(base.ShowDialogBox("請輸入有效的”控製額度“！", "", 400));
                    base.Response.End();
                }
            }
            if (this.play_happycarDT != null)
            {
                for (int num23 = 0; num23 < this.play_happycarDT.Rows.Count; num23++)
                {
                    string str67 = this.play_happycarDT.Rows[num23]["play_id"].ToString();
                    string str68 = LSRequest.qq("happycar_money_" + str67);
                    if (LSRequest.qq("happycar_chk_" + str67).Equals("1"))
                    {
                        bool flag46 = Utils.IsInteger(str68);
                        bool flag47 = Utils.IsIntMoney(str68);
                        if (!flag46 || !flag47)
                        {
                            flag = false;
                            break;
                        }
                    }
                }
                if (!flag)
                {
                    base.Response.Write(base.ShowDialogBox("請輸入有效的”控製額度“！", "", 400));
                    base.Response.End();
                }
            }
        }
    }
}

