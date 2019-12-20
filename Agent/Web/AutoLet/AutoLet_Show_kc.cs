namespace Agent.Web.AutoLet
{
    using Agent.Web.WebBase;
    using LotterySystem.Common;
    using LotterySystem.Model;
    using System;
    using System.Data;

    public class AutoLet_Show_kc : MemberPageBase
    {
        protected cz_users cz_users_model;
        protected DataTable lotteryDT;
        protected string lotteryId = "";
        protected string mId = "";
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
        protected string uid = "";

        private void InitData()
        {
            for (int i = 0; i < this.lotteryDT.Rows.Count; i++)
            {
                switch (Convert.ToInt32(this.lotteryDT.Rows[i]["id"].ToString()))
                {
                    case 0:
                    {
                        string str = "86,87,88,89,90,91,92,93,94,95,96,97,98,99,100,101,102,103,104,105,106,107,108,109,110,111,112,113,114,115,116,117,118,119,120,123,124,125,126,127,128,129,130,131,132,133,134,135,136";
                        DataSet autosaleShowByUserName = CallBLL.cz_autosale_kl10_bll.GetAutosaleShowByUserName(this.cz_users_model.get_u_name(), str);
                        if (((autosaleShowByUserName != null) && (autosaleShowByUserName.Tables.Count > 0)) && (autosaleShowByUserName.Tables[0].Rows.Count > 0))
                        {
                            this.sale_kl10DT = autosaleShowByUserName.Tables[0];
                        }
                        this.play_kl10DT = new DataTable();
                        break;
                    }
                    case 1:
                    {
                        DataSet set2 = CallBLL.cz_autosale_cqsc_bll.GetAutosaleShowByUserName(this.cz_users_model.get_u_name(), "4,5,6,7,8,9,10,11,12,13,14,15");
                        if (((set2 != null) && (set2.Tables.Count > 0)) && (set2.Tables[0].Rows.Count > 0))
                        {
                            this.sale_cqscDT = set2.Tables[0];
                        }
                        this.play_cqscDT = new DataTable();
                        break;
                    }
                    case 2:
                    {
                        DataSet set3 = CallBLL.cz_autosale_pk10_bll.GetAutosaleShowByUserName(this.cz_users_model.get_u_name(), "1,2,3,4,36,37,38");
                        if (((set3 != null) && (set3.Tables.Count > 0)) && (set3.Tables[0].Rows.Count > 0))
                        {
                            this.sale_pk10DT = set3.Tables[0];
                        }
                        this.play_pk10DT = new DataTable();
                        break;
                    }
                    case 3:
                    {
                        DataSet set4 = CallBLL.cz_autosale_xync_bll.GetAutosaleShowByUserName(this.cz_users_model.get_u_name(), "86,87,88,89,90,91,92,93,94,95,96,97,98,99,100,101,102,103,104,105,106,107,108,109,110,111,112,113,114,115,116,117,118,119,120,123,124,125,126,127,128,129,130,131,132,133,134,135,136");
                        if (((set4 != null) && (set4.Tables.Count > 0)) && (set4.Tables[0].Rows.Count > 0))
                        {
                            this.sale_xyncDT = set4.Tables[0];
                        }
                        this.play_xyncDT = new DataTable();
                        break;
                    }
                    case 4:
                    {
                        DataSet autosaleByUserName = CallBLL.cz_autosale_jsk3_bll.GetAutosaleByUserName(this.cz_users_model.get_u_name());
                        if (((autosaleByUserName != null) && (autosaleByUserName.Tables.Count > 0)) && (autosaleByUserName.Tables[0].Rows.Count > 0))
                        {
                            this.sale_jsk3DT = autosaleByUserName.Tables[0];
                        }
                        this.play_jsk3DT = new DataTable();
                        break;
                    }
                    case 5:
                    {
                        DataSet set6 = CallBLL.cz_autosale_kl8_bll.GetAutosaleByUserName(this.cz_users_model.get_u_name());
                        if (((set6 != null) && (set6.Tables.Count > 0)) && (set6.Tables[0].Rows.Count > 0))
                        {
                            this.sale_kl8DT = set6.Tables[0];
                        }
                        this.play_kl8DT = new DataTable();
                        break;
                    }
                    case 6:
                    {
                        DataSet set7 = CallBLL.cz_autosale_k8sc_bll.GetAutosaleShowByUserName(this.cz_users_model.get_u_name(), "4,5,6,7,8,9,10,11,12,13,14,15");
                        if (((set7 != null) && (set7.Tables.Count > 0)) && (set7.Tables[0].Rows.Count > 0))
                        {
                            this.sale_k8scDT = set7.Tables[0];
                        }
                        this.play_k8scDT = new DataTable();
                        break;
                    }
                    case 7:
                    {
                        DataSet set8 = CallBLL.cz_autosale_pcdd_bll.GetAutosaleShowByUserName(this.cz_users_model.get_u_name(), "71009,71010,71011,71012");
                        if (((set8 != null) && (set8.Tables.Count > 0)) && (set8.Tables[0].Rows.Count > 0))
                        {
                            this.sale_pcddDT = set8.Tables[0];
                        }
                        this.play_pcddDT = new DataTable();
                        break;
                    }
                    case 8:
                    {
                        DataSet set10 = CallBLL.cz_autosale_pkbjl_bll.GetAutosaleShowByUserName(this.cz_users_model.get_u_name(), "81001,81002,81003,81004");
                        if (((set10 != null) && (set10.Tables.Count > 0)) && (set10.Tables[0].Rows.Count > 0))
                        {
                            this.sale_pkbjlDT = set10.Tables[0];
                        }
                        this.play_pkbjlDT = new DataTable();
                        break;
                    }
                    case 9:
                    {
                        DataSet set9 = CallBLL.cz_autosale_xyft5_bll.GetAutosaleShowByUserName(this.cz_users_model.get_u_name(), "1,2,3,4,36,37,38");
                        if (((set9 != null) && (set9.Tables.Count > 0)) && (set9.Tables[0].Rows.Count > 0))
                        {
                            this.sale_xyft5DT = set9.Tables[0];
                        }
                        this.play_xyft5DT = new DataTable();
                        break;
                    }
                    case 10:
                    {
                        DataSet set11 = CallBLL.cz_autosale_jscar_bll.GetAutosaleShowByUserName(this.cz_users_model.get_u_name(), "1,2,3,4,36,37,38");
                        if (((set11 != null) && (set11.Tables.Count > 0)) && (set11.Tables[0].Rows.Count > 0))
                        {
                            this.sale_jscarDT = set11.Tables[0];
                        }
                        this.play_jscarDT = new DataTable();
                        break;
                    }
                    case 11:
                    {
                        DataSet set12 = CallBLL.cz_autosale_speed5_bll.GetAutosaleShowByUserName(this.cz_users_model.get_u_name(), "4,5,6,7,8,9,10,11,12,13,14,15");
                        if (((set12 != null) && (set12.Tables.Count > 0)) && (set12.Tables[0].Rows.Count > 0))
                        {
                            this.sale_speed5DT = set12.Tables[0];
                        }
                        this.play_speed5DT = new DataTable();
                        break;
                    }
                    case 12:
                    {
                        DataSet set14 = CallBLL.cz_autosale_jspk10_bll.GetAutosaleShowByUserName(this.cz_users_model.get_u_name(), "1,2,3,4,36,37,38");
                        if (((set14 != null) && (set14.Tables.Count > 0)) && (set14.Tables[0].Rows.Count > 0))
                        {
                            this.sale_jspk10DT = set14.Tables[0];
                        }
                        this.play_jspk10DT = new DataTable();
                        break;
                    }
                    case 13:
                    {
                        DataSet set13 = CallBLL.cz_autosale_jscqsc_bll.GetAutosaleShowByUserName(this.cz_users_model.get_u_name(), "4,5,6,7,8,9,10,11,12,13,14,15");
                        if (((set13 != null) && (set13.Tables.Count > 0)) && (set13.Tables[0].Rows.Count > 0))
                        {
                            this.sale_jscqscDT = set13.Tables[0];
                        }
                        this.play_jscqscDT = new DataTable();
                        break;
                    }
                    case 14:
                    {
                        string str2 = "86,87,88,89,90,91,92,93,94,95,96,97,98,99,100,101,102,103,104,105,106,107,108,109,110,111,112,113,114,115,116,117,118,119,120,123,124,125,126,127,128,129,130,131,132,133,134,135,136";
                        DataSet set15 = CallBLL.cz_autosale_jssfc_bll.GetAutosaleShowByUserName(this.cz_users_model.get_u_name(), str2);
                        if (((set15 != null) && (set15.Tables.Count > 0)) && (set15.Tables[0].Rows.Count > 0))
                        {
                            this.sale_jssfcDT = set15.Tables[0];
                        }
                        this.play_jssfcDT = new DataTable();
                        break;
                    }
                    case 15:
                    {
                        DataSet set16 = CallBLL.cz_autosale_jsft2_bll.GetAutosaleShowByUserName(this.cz_users_model.get_u_name(), "1,2,3,4,36,37,38");
                        if (((set16 != null) && (set16.Tables.Count > 0)) && (set16.Tables[0].Rows.Count > 0))
                        {
                            this.sale_jsft2DT = set16.Tables[0];
                        }
                        this.play_jsft2DT = new DataTable();
                        break;
                    }
                    case 0x10:
                    {
                        DataSet set17 = CallBLL.cz_autosale_car168_bll.GetAutosaleShowByUserName(this.cz_users_model.get_u_name(), "1,2,3,4,36,37,38");
                        if (((set17 != null) && (set17.Tables.Count > 0)) && (set17.Tables[0].Rows.Count > 0))
                        {
                            this.sale_car168DT = set17.Tables[0];
                        }
                        this.play_car168DT = new DataTable();
                        break;
                    }
                    case 0x11:
                    {
                        DataSet set18 = CallBLL.cz_autosale_ssc168_bll.GetAutosaleShowByUserName(this.cz_users_model.get_u_name(), "4,5,6,7,8,9,10,11,12,13,14,15");
                        if (((set18 != null) && (set18.Tables.Count > 0)) && (set18.Tables[0].Rows.Count > 0))
                        {
                            this.sale_ssc168DT = set18.Tables[0];
                        }
                        this.play_ssc168DT = new DataTable();
                        break;
                    }
                    case 0x12:
                    {
                        DataSet set19 = CallBLL.cz_autosale_vrcar_bll.GetAutosaleShowByUserName(this.cz_users_model.get_u_name(), "1,2,3,4,36,37,38");
                        if (((set19 != null) && (set19.Tables.Count > 0)) && (set19.Tables[0].Rows.Count > 0))
                        {
                            this.sale_vrcarDT = set19.Tables[0];
                        }
                        this.play_vrcarDT = new DataTable();
                        break;
                    }
                    case 0x13:
                    {
                        DataSet set20 = CallBLL.cz_autosale_vrssc_bll.GetAutosaleShowByUserName(this.cz_users_model.get_u_name(), "4,5,6,7,8,9,10,11,12,13,14,15");
                        if (((set20 != null) && (set20.Tables.Count > 0)) && (set20.Tables[0].Rows.Count > 0))
                        {
                            this.sale_vrsscDT = set20.Tables[0];
                        }
                        this.play_vrsscDT = new DataTable();
                        break;
                    }
                    case 20:
                    {
                        DataSet set21 = CallBLL.cz_autosale_xyftoa_bll.GetAutosaleShowByUserName(this.cz_users_model.get_u_name(), "1,2,3,4,36,37,38");
                        if (((set21 != null) && (set21.Tables.Count > 0)) && (set21.Tables[0].Rows.Count > 0))
                        {
                            this.sale_xyftoaDT = set21.Tables[0];
                        }
                        this.play_xyftoaDT = new DataTable();
                        break;
                    }
                    case 0x15:
                    {
                        DataSet set22 = CallBLL.cz_autosale_xyftsg_bll.GetAutosaleShowByUserName(this.cz_users_model.get_u_name(), "1,2,3,4,36,37,38");
                        if (((set22 != null) && (set22.Tables.Count > 0)) && (set22.Tables[0].Rows.Count > 0))
                        {
                            this.sale_xyftsgDT = set22.Tables[0];
                        }
                        this.play_xyftsgDT = new DataTable();
                        break;
                    }
                    case 0x16:
                    {
                        DataSet set23 = CallBLL.cz_autosale_happycar_bll.GetAutosaleShowByUserName(this.cz_users_model.get_u_name(), "1,2,3,4,36,37,38");
                        if (((set23 != null) && (set23.Tables.Count > 0)) && (set23.Tables[0].Rows.Count > 0))
                        {
                            this.sale_happycarDT = set23.Tables[0];
                        }
                        this.play_happycarDT = new DataTable();
                        break;
                    }
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string str = this.Session["user_name"].ToString();
            agent_userinfo_session _session = this.Session[str + "lottery_session_user_info"] as agent_userinfo_session;
            this.skin = _session.get_u_skin();
            this.lotteryId = base.AgentCurrentLotteryID;
            this.uid = LSRequest.qq("uid");
            this.mId = LSRequest.qq("mid");
            this.lotteryDT = base.GetLotteryList();
            int num = 1;
            if (this.mId.Equals(num.ToString()))
            {
                base.Response.Redirect(string.Format("/AutoLet/AutoLet_Show_six.aspx?uid={0}&mid={1}", this.uid, this.mId), true);
            }
            this.cz_users_model = CallBLL.cz_users_bll.GetUserInfoByUID(this.uid);
            if (this.cz_users_model == null)
            {
                base.Response.Redirect("/MessagePage.aspx?code=u100038&url=&issuccess=1&isback=0&isopen=1");
                base.Response.End();
            }
            if (!this.cz_users_model.get_kc_allow_sale().Equals(1))
            {
                base.Response.Redirect("/MessagePage.aspx?code=u100036&url=&issuccess=1&isback=0&isopen=1");
                base.Response.End();
            }
            if (!_session.get_u_type().Equals("zj") && !base.IsUpperLowerLevels(this.cz_users_model.get_u_name(), _session.get_u_type(), _session.get_u_name()))
            {
                base.Response.Redirect("/MessagePage.aspx?code=u100057&url=&issuccess=1&isback=0&isopen=1");
                base.Response.End();
            }
            this.InitData();
        }
    }
}

