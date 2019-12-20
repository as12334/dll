namespace Agent.Web
{
    using Agent.Web.WebBase;
    using LotterySystem.Common;
    using LotterySystem.Model;
    using LotterySystem.WebPageBase;
    using System;
    using System.Data;

    public class TradingSet : MemberPageBase
    {
        protected DataTable dataTable;
        protected DataTable kl10DT;
        protected DataTable lotteryDT;
        protected string lotteryId = "";
        protected string skin = "";

        protected string GetPlayName(string playName, int lottery_type, string play_id)
        {
            string str = "";
            int num = lottery_type;
            switch (num)
            {
                case 0:
                case 14:
                    return playName.Replace("第一球", "1-8球");

                case 1:
                case 11:
                case 13:
                case 0x11:
                case 0x13:
                    return playName.Replace("第一球", "1-5球");

                case 2:
                case 9:
                case 10:
                case 12:
                case 15:
                case 0x10:
                case 0x12:
                case 20:
                case 0x15:
                case 0x16:
                    return playName.Replace("冠軍龍虎", "1-5名龍虎").Replace("冠軍", "1-10名");

                case 3:
                    return playName.Replace("第一球", "1-8球");

                case 4:
                    return playName;

                case 5:
                    return playName;

                case 6:
                    return playName.Replace("第一球", "1-5球");

                case 7:
                    return playName.Replace("1區大小", "1-3區大小").Replace("1區單雙", "1-3區單雙");

                case 8:
                    return playName;
            }
            if (num == 100)
            {
                str = playName.Replace("正1特", "正特1-6");
            }
            return str;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string str = this.Session["user_name"].ToString();
            agent_userinfo_session model = this.Session[str + "lottery_session_user_info"] as agent_userinfo_session;
            this.skin = model.get_u_skin();
            if (!model.get_u_type().Equals("zj"))
            {
                base.Response.Redirect("/MessagePage.aspx?code=u100014&url=&issuccess=1&isback=0");
                base.Response.End();
            }
            base.Permission_Aspx_ZJ(model, "po_3_3");
            this.lotteryId = LSRequest.qq("lid");
            this.lotteryDT = base.GetLotteryList();
            switch (Convert.ToInt32(this.lotteryId))
            {
                case 0:
                {
                    DataSet playByTradingSet = CallBLL.cz_play_kl10_bll.GetPlayByTradingSet("86,87,88,89,90,91,92,93,94,95,96,97,98,99,100,101,102,103,104,105,106,107,108,109,110,111,112,113,114,115,116,117,118,119,120,123,124,125,126,127,128,129,130,131,132,133,134,135,136");
                    if (((playByTradingSet != null) && (playByTradingSet.Tables.Count > 0)) && (playByTradingSet.Tables[0].Rows.Count > 0))
                    {
                        this.dataTable = playByTradingSet.Tables[0];
                    }
                    break;
                }
                case 1:
                {
                    DataSet set2 = CallBLL.cz_play_cqsc_bll.GetPlayByTradingSet("4,5,6,7,8,9,10,11,12,13,14,15");
                    if (((set2 != null) && (set2.Tables.Count > 0)) && (set2.Tables[0].Rows.Count > 0))
                    {
                        this.dataTable = set2.Tables[0];
                    }
                    break;
                }
                case 2:
                {
                    DataSet set3 = CallBLL.cz_play_pk10_bll.GetPlayByTradingSet("1,2,3,4,36,37,38");
                    if (((set3 != null) && (set3.Tables.Count > 0)) && (set3.Tables[0].Rows.Count > 0))
                    {
                        this.dataTable = set3.Tables[0];
                    }
                    break;
                }
                case 3:
                {
                    DataSet set4 = CallBLL.cz_play_xync_bll.GetPlayByTradingSet("86,87,88,89,90,91,92,93,94,95,96,97,98,99,100,101,102,103,104,105,106,107,108,109,110,111,112,113,114,115,116,117,118,119,120,123,124,125,126,127,128,129,130,131,132,133,134,135,136");
                    if (((set4 != null) && (set4.Tables.Count > 0)) && (set4.Tables[0].Rows.Count > 0))
                    {
                        this.dataTable = set4.Tables[0];
                    }
                    break;
                }
                case 4:
                {
                    DataSet set5 = CallBLL.cz_play_jsk3_bll.GetPlayByTradingSet("");
                    if (((set5 != null) && (set5.Tables.Count > 0)) && (set5.Tables[0].Rows.Count > 0))
                    {
                        this.dataTable = set5.Tables[0];
                    }
                    break;
                }
                case 5:
                {
                    DataSet set6 = CallBLL.cz_play_kl8_bll.GetPlayByTradingSet("");
                    if (((set6 != null) && (set6.Tables.Count > 0)) && (set6.Tables[0].Rows.Count > 0))
                    {
                        this.dataTable = set6.Tables[0];
                    }
                    break;
                }
                case 6:
                {
                    DataSet set8 = CallBLL.cz_play_k8sc_bll.GetPlayByTradingSet("4,5,6,7,8,9,10,11,12,13,14,15");
                    if (((set8 != null) && (set8.Tables.Count > 0)) && (set8.Tables[0].Rows.Count > 0))
                    {
                        this.dataTable = set8.Tables[0];
                    }
                    break;
                }
                case 7:
                {
                    DataSet set9 = CallBLL.cz_play_pcdd_bll.GetPlayByTradingSet("71009,71010,71011,71012");
                    if (((set9 != null) && (set9.Tables.Count > 0)) && (set9.Tables[0].Rows.Count > 0))
                    {
                        this.dataTable = set9.Tables[0];
                    }
                    break;
                }
                case 8:
                {
                    DataSet set11 = CallBLL.cz_play_pkbjl_bll.GetPlayByTradingSet("");
                    if (((set11 != null) && (set11.Tables.Count > 0)) && (set11.Tables[0].Rows.Count > 0))
                    {
                        this.dataTable = set11.Tables[0];
                    }
                    break;
                }
                case 9:
                {
                    DataSet set10 = CallBLL.cz_play_xyft5_bll.GetPlayByTradingSet("1,2,3,4,36,37,38");
                    if (((set10 != null) && (set10.Tables.Count > 0)) && (set10.Tables[0].Rows.Count > 0))
                    {
                        this.dataTable = set10.Tables[0];
                    }
                    break;
                }
                case 10:
                {
                    DataSet set12 = CallBLL.cz_play_jscar_bll.GetPlayByTradingSet("1,2,3,4,36,37,38");
                    if (((set12 != null) && (set12.Tables.Count > 0)) && (set12.Tables[0].Rows.Count > 0))
                    {
                        this.dataTable = set12.Tables[0];
                    }
                    break;
                }
                case 11:
                {
                    DataSet set13 = CallBLL.cz_play_speed5_bll.GetPlayByTradingSet("4,5,6,7,8,9,10,11,12,13,14,15");
                    if (((set13 != null) && (set13.Tables.Count > 0)) && (set13.Tables[0].Rows.Count > 0))
                    {
                        this.dataTable = set13.Tables[0];
                    }
                    break;
                }
                case 12:
                {
                    DataSet set15 = CallBLL.cz_play_jspk10_bll.GetPlayByTradingSet("1,2,3,4,36,37,38");
                    if (((set15 != null) && (set15.Tables.Count > 0)) && (set15.Tables[0].Rows.Count > 0))
                    {
                        this.dataTable = set15.Tables[0];
                    }
                    break;
                }
                case 13:
                {
                    DataSet set14 = CallBLL.cz_play_jscqsc_bll.GetPlayByTradingSet("4,5,6,7,8,9,10,11,12,13,14,15");
                    if (((set14 != null) && (set14.Tables.Count > 0)) && (set14.Tables[0].Rows.Count > 0))
                    {
                        this.dataTable = set14.Tables[0];
                    }
                    break;
                }
                case 14:
                {
                    DataSet set16 = CallBLL.cz_play_jssfc_bll.GetPlayByTradingSet("86,87,88,89,90,91,92,93,94,95,96,97,98,99,100,101,102,103,104,105,106,107,108,109,110,111,112,113,114,115,116,117,118,119,120,123,124,125,126,127,128,129,130,131,132,133,134,135,136");
                    if (((set16 != null) && (set16.Tables.Count > 0)) && (set16.Tables[0].Rows.Count > 0))
                    {
                        this.dataTable = set16.Tables[0];
                    }
                    break;
                }
                case 15:
                {
                    DataSet set17 = CallBLL.cz_play_jsft2_bll.GetPlayByTradingSet("1,2,3,4,36,37,38");
                    if (((set17 != null) && (set17.Tables.Count > 0)) && (set17.Tables[0].Rows.Count > 0))
                    {
                        this.dataTable = set17.Tables[0];
                    }
                    break;
                }
                case 0x10:
                {
                    DataSet set18 = CallBLL.cz_play_car168_bll.GetPlayByTradingSet("1,2,3,4,36,37,38");
                    if (((set18 != null) && (set18.Tables.Count > 0)) && (set18.Tables[0].Rows.Count > 0))
                    {
                        this.dataTable = set18.Tables[0];
                    }
                    break;
                }
                case 0x11:
                {
                    DataSet set19 = CallBLL.cz_play_ssc168_bll.GetPlayByTradingSet("4,5,6,7,8,9,10,11,12,13,14,15");
                    if (((set19 != null) && (set19.Tables.Count > 0)) && (set19.Tables[0].Rows.Count > 0))
                    {
                        this.dataTable = set19.Tables[0];
                    }
                    break;
                }
                case 0x12:
                {
                    DataSet set20 = CallBLL.cz_play_vrcar_bll.GetPlayByTradingSet("1,2,3,4,36,37,38");
                    if (((set20 != null) && (set20.Tables.Count > 0)) && (set20.Tables[0].Rows.Count > 0))
                    {
                        this.dataTable = set20.Tables[0];
                    }
                    break;
                }
                case 0x13:
                {
                    DataSet set21 = CallBLL.cz_play_vrssc_bll.GetPlayByTradingSet("4,5,6,7,8,9,10,11,12,13,14,15");
                    if (((set21 != null) && (set21.Tables.Count > 0)) && (set21.Tables[0].Rows.Count > 0))
                    {
                        this.dataTable = set21.Tables[0];
                    }
                    break;
                }
                case 20:
                {
                    DataSet set22 = CallBLL.cz_play_xyftoa_bll.GetPlayByTradingSet("1,2,3,4,36,37,38");
                    if (((set22 != null) && (set22.Tables.Count > 0)) && (set22.Tables[0].Rows.Count > 0))
                    {
                        this.dataTable = set22.Tables[0];
                    }
                    break;
                }
                case 0x15:
                {
                    DataSet set23 = CallBLL.cz_play_xyftsg_bll.GetPlayByTradingSet("1,2,3,4,36,37,38");
                    if (((set23 != null) && (set23.Tables.Count > 0)) && (set23.Tables[0].Rows.Count > 0))
                    {
                        this.dataTable = set23.Tables[0];
                    }
                    break;
                }
                case 0x16:
                {
                    DataSet set24 = CallBLL.cz_play_happycar_bll.GetPlayByTradingSet("1,2,3,4,36,37,38");
                    if (((set24 != null) && (set24.Tables.Count > 0)) && (set24.Tables[0].Rows.Count > 0))
                    {
                        this.dataTable = set24.Tables[0];
                    }
                    break;
                }
                case 100:
                {
                    DataSet set7 = CallBLL.cz_play_six_bll.GetPlayByTradingSet("91025,91026,91027,91028,91029");
                    if (((set7 != null) && (set7.Tables.Count > 0)) && (set7.Tables[0].Rows.Count > 0))
                    {
                        this.dataTable = set7.Tables[0];
                    }
                    break;
                }
            }
            if (LSRequest.qq("submitHdn").Equals("submit"))
            {
                DataTable rs = null;
                int num25 = Convert.ToInt32(this.lotteryId);
                switch (num25)
                {
                    case 0:
                    {
                        DataSet set25 = CallBLL.cz_play_kl10_bll.GetPlayByTradingSet("86,87,88,89,90,91,92,93,94,95,96,97,98,99,100,101,102,103,104,105,106,107,108,109,110,111,112,113,114,115,116,117,118,119,120,123,124,125,126,127,128,129,130,131,132,133,134,135,136");
                        if (((set25 != null) && (set25.Tables.Count > 0)) && (set25.Tables[0].Rows.Count > 0))
                        {
                            rs = set25.Tables[0];
                            this.ValidInput(rs);
                            int num = 0;
                            foreach (DataRow row in rs.Rows)
                            {
                                string str2 = "";
                                switch (num)
                                {
                                    case 0:
                                        str2 = "81,86,91,96,101,106,111,116";
                                        break;

                                    case 1:
                                        str2 = "82,87,92,97,102,107,112,117";
                                        break;

                                    case 2:
                                        str2 = "83,88,93,98,103,108,113,118";
                                        break;

                                    case 3:
                                        str2 = "84,89,94,99,104,109,114,119";
                                        break;

                                    case 4:
                                        str2 = "85,90,95,100,105,110,115,120";
                                        break;

                                    case 5:
                                        str2 = "121,123,125,127,129,131,133,135";
                                        break;

                                    case 6:
                                        str2 = "122,124,126,128,130,132,134,136";
                                        break;

                                    default:
                                        str2 = row["play_id"].ToString();
                                        break;
                                }
                                cz_play_kl10 _kl = new cz_play_kl10();
                                _kl.set_total_amount(new decimal?(Convert.ToDecimal(LSRequest.qq("t_" + num.ToString() + "_total_amount"))));
                                _kl.set_allow_min_amount(new decimal?(Convert.ToDecimal(LSRequest.qq("t_" + num.ToString() + "_allow_min_amount"))));
                                _kl.set_allow_max_amount(new decimal?(Convert.ToDecimal(LSRequest.qq("t_" + num.ToString() + "_allow_max_amount"))));
                                _kl.set_allow_max_put_amount(new decimal?(Convert.ToDecimal(LSRequest.qq("t_" + num.ToString() + "_allow_max_put_amount"))));
                                _kl.set_max_appoint(new decimal?(Convert.ToDecimal(LSRequest.qq("t_" + num.ToString() + "_max_appoint"))));
                                _kl.set_above_quota(new decimal?(Convert.ToDecimal(LSRequest.qq("t_" + num.ToString() + "_above_quota"))));
                                CallBLL.cz_play_kl10_bll.SetPlayByTradingSet(str2, _kl);
                                num++;
                            }
                            base.jysd_kc_log(rs, CallBLL.cz_play_kl10_bll.GetPlayByTradingSet("86,87,88,89,90,91,92,93,94,95,96,97,98,99,100,101,102,103,104,105,106,107,108,109,110,111,112,113,114,115,116,117,118,119,120,123,124,125,126,127,128,129,130,131,132,133,134,135,136").Tables[0], this.lotteryId);
                        }
                        break;
                    }
                    case 1:
                    {
                        DataSet set26 = CallBLL.cz_play_cqsc_bll.GetPlayByTradingSet("4,5,6,7,8,9,10,11,12,13,14,15");
                        if (((set26 != null) && (set26.Tables.Count > 0)) && (set26.Tables[0].Rows.Count > 0))
                        {
                            rs = set26.Tables[0];
                            this.ValidInput(rs);
                            int num2 = 0;
                            foreach (DataRow row2 in rs.Rows)
                            {
                                string str3 = "";
                                switch (num2)
                                {
                                    case 0:
                                        str3 = "1,4,7,10,13";
                                        break;

                                    case 1:
                                        str3 = "2,5,8,11,14";
                                        break;

                                    case 2:
                                        str3 = "3,6,9,12,15";
                                        break;

                                    default:
                                        str3 = row2["play_id"].ToString();
                                        break;
                                }
                                cz_play_cqsc _cqsc = new cz_play_cqsc();
                                _cqsc.set_total_amount(new decimal?(Convert.ToDecimal(LSRequest.qq("t_" + num2.ToString() + "_total_amount"))));
                                _cqsc.set_allow_min_amount(new decimal?(Convert.ToDecimal(LSRequest.qq("t_" + num2.ToString() + "_allow_min_amount"))));
                                _cqsc.set_allow_max_amount(new decimal?(Convert.ToDecimal(LSRequest.qq("t_" + num2.ToString() + "_allow_max_amount"))));
                                _cqsc.set_allow_max_put_amount(new decimal?(Convert.ToDecimal(LSRequest.qq("t_" + num2.ToString() + "_allow_max_put_amount"))));
                                _cqsc.set_max_appoint(new decimal?(Convert.ToDecimal(LSRequest.qq("t_" + num2.ToString() + "_max_appoint"))));
                                _cqsc.set_above_quota(new decimal?(Convert.ToDecimal(LSRequest.qq("t_" + num2.ToString() + "_above_quota"))));
                                CallBLL.cz_play_cqsc_bll.SetPlayByTradingSet(str3, _cqsc);
                                num2++;
                            }
                            base.jysd_kc_log(rs, CallBLL.cz_play_cqsc_bll.GetPlayByTradingSet("4,5,6,7,8,9,10,11,12,13,14,15").Tables[0], this.lotteryId);
                        }
                        break;
                    }
                    case 2:
                    {
                        DataSet set27 = CallBLL.cz_play_pk10_bll.GetPlayByTradingSet("1,2,3,4,36,37,38");
                        if (((set27 != null) && (set27.Tables.Count > 0)) && (set27.Tables[0].Rows.Count > 0))
                        {
                            rs = set27.Tables[0];
                            this.ValidInput(rs);
                            int num3 = 0;
                            foreach (DataRow row3 in rs.Rows)
                            {
                                string str4 = "";
                                switch (num3)
                                {
                                    case 0:
                                        str4 = "1,5,9,13,17,21,24,27,30,33";
                                        break;

                                    case 1:
                                        str4 = "2,6,10,14,18,22,25,28,31,34";
                                        break;

                                    case 2:
                                        str4 = "3,7,11,15,19,23,26,29,32,35";
                                        break;

                                    case 3:
                                        str4 = "4,8,12,16,20";
                                        break;

                                    default:
                                        str4 = row3["play_id"].ToString();
                                        break;
                                }
                                cz_play_pk10 _pk = new cz_play_pk10();
                                _pk.set_total_amount(new decimal?(Convert.ToDecimal(LSRequest.qq("t_" + num3.ToString() + "_total_amount"))));
                                _pk.set_allow_min_amount(new decimal?(Convert.ToDecimal(LSRequest.qq("t_" + num3.ToString() + "_allow_min_amount"))));
                                _pk.set_allow_max_amount(new decimal?(Convert.ToDecimal(LSRequest.qq("t_" + num3.ToString() + "_allow_max_amount"))));
                                _pk.set_allow_max_put_amount(new decimal?(Convert.ToDecimal(LSRequest.qq("t_" + num3.ToString() + "_allow_max_put_amount"))));
                                _pk.set_max_appoint(new decimal?(Convert.ToDecimal(LSRequest.qq("t_" + num3.ToString() + "_max_appoint"))));
                                _pk.set_above_quota(new decimal?(Convert.ToDecimal(LSRequest.qq("t_" + num3.ToString() + "_above_quota"))));
                                CallBLL.cz_play_pk10_bll.SetPlayByTradingSet(str4, _pk);
                                num3++;
                            }
                            base.jysd_kc_log(rs, CallBLL.cz_play_pk10_bll.GetPlayByTradingSet("1,2,3,4,36,37,38").Tables[0], this.lotteryId);
                        }
                        break;
                    }
                    case 3:
                    {
                        DataSet set28 = CallBLL.cz_play_xync_bll.GetPlayByTradingSet("86,87,88,89,90,91,92,93,94,95,96,97,98,99,100,101,102,103,104,105,106,107,108,109,110,111,112,113,114,115,116,117,118,119,120,123,124,125,126,127,128,129,130,131,132,133,134,135,136");
                        if (((set28 != null) && (set28.Tables.Count > 0)) && (set28.Tables[0].Rows.Count > 0))
                        {
                            rs = set28.Tables[0];
                            this.ValidInput(rs);
                            int num4 = 0;
                            foreach (DataRow row4 in rs.Rows)
                            {
                                string str5 = "";
                                switch (num4)
                                {
                                    case 0:
                                        str5 = "81,86,91,96,101,106,111,116";
                                        break;

                                    case 1:
                                        str5 = "82,87,92,97,102,107,112,117";
                                        break;

                                    case 2:
                                        str5 = "83,88,93,98,103,108,113,118";
                                        break;

                                    case 3:
                                        str5 = "84,89,94,99,104,109,114,119";
                                        break;

                                    case 4:
                                        str5 = "85,90,95,100,105,110,115,120";
                                        break;

                                    case 5:
                                        str5 = "121,123,125,127,129,131,133,135";
                                        break;

                                    case 6:
                                        str5 = "122,124,126,128,130,132,134,136";
                                        break;

                                    default:
                                        str5 = row4["play_id"].ToString();
                                        break;
                                }
                                cz_play_xync _xync = new cz_play_xync();
                                _xync.set_total_amount(new decimal?(Convert.ToDecimal(LSRequest.qq("t_" + num4.ToString() + "_total_amount"))));
                                _xync.set_allow_min_amount(new decimal?(Convert.ToDecimal(LSRequest.qq("t_" + num4.ToString() + "_allow_min_amount"))));
                                _xync.set_allow_max_amount(new decimal?(Convert.ToDecimal(LSRequest.qq("t_" + num4.ToString() + "_allow_max_amount"))));
                                _xync.set_allow_max_put_amount(new decimal?(Convert.ToDecimal(LSRequest.qq("t_" + num4.ToString() + "_allow_max_put_amount"))));
                                _xync.set_max_appoint(new decimal?(Convert.ToDecimal(LSRequest.qq("t_" + num4.ToString() + "_max_appoint"))));
                                _xync.set_above_quota(new decimal?(Convert.ToDecimal(LSRequest.qq("t_" + num4.ToString() + "_above_quota"))));
                                CallBLL.cz_play_xync_bll.SetPlayByTradingSet(str5, _xync);
                                num4++;
                            }
                            base.jysd_kc_log(rs, CallBLL.cz_play_xync_bll.GetPlayByTradingSet("86,87,88,89,90,91,92,93,94,95,96,97,98,99,100,101,102,103,104,105,106,107,108,109,110,111,112,113,114,115,116,117,118,119,120,123,124,125,126,127,128,129,130,131,132,133,134,135,136").Tables[0], this.lotteryId);
                        }
                        break;
                    }
                    case 4:
                    {
                        DataSet set29 = CallBLL.cz_play_jsk3_bll.GetPlayByTradingSet("");
                        if (((set29 != null) && (set29.Tables.Count > 0)) && (set29.Tables[0].Rows.Count > 0))
                        {
                            rs = set29.Tables[0];
                            this.ValidInput(rs);
                            int num5 = 0;
                            foreach (DataRow row5 in rs.Rows)
                            {
                                string str6 = row5["play_id"].ToString();
                                cz_play_jsk3 _jsk = new cz_play_jsk3();
                                _jsk.set_total_amount(new decimal?(Convert.ToDecimal(LSRequest.qq("t_" + num5.ToString() + "_total_amount"))));
                                _jsk.set_allow_min_amount(new decimal?(Convert.ToDecimal(LSRequest.qq("t_" + num5.ToString() + "_allow_min_amount"))));
                                _jsk.set_allow_max_amount(new decimal?(Convert.ToDecimal(LSRequest.qq("t_" + num5.ToString() + "_allow_max_amount"))));
                                _jsk.set_allow_max_put_amount(new decimal?(Convert.ToDecimal(LSRequest.qq("t_" + num5.ToString() + "_allow_max_put_amount"))));
                                _jsk.set_max_appoint(new decimal?(Convert.ToDecimal(LSRequest.qq("t_" + num5.ToString() + "_max_appoint"))));
                                _jsk.set_above_quota(new decimal?(Convert.ToDecimal(LSRequest.qq("t_" + num5.ToString() + "_above_quota"))));
                                CallBLL.cz_play_jsk3_bll.SetPlayByTradingSet(str6, _jsk);
                                num5++;
                            }
                            base.jysd_kc_log(rs, CallBLL.cz_play_jsk3_bll.GetPlayByTradingSet("").Tables[0], this.lotteryId);
                        }
                        break;
                    }
                    case 5:
                    {
                        DataSet set30 = CallBLL.cz_play_kl8_bll.GetPlayByTradingSet("");
                        if (((set30 != null) && (set30.Tables.Count > 0)) && (set30.Tables[0].Rows.Count > 0))
                        {
                            rs = set30.Tables[0];
                            this.ValidInput(rs);
                            int num6 = 0;
                            foreach (DataRow row6 in rs.Rows)
                            {
                                string str7 = row6["play_id"].ToString();
                                cz_play_kl8 _kl2 = new cz_play_kl8();
                                _kl2.set_total_amount(new decimal?(Convert.ToDecimal(LSRequest.qq("t_" + num6.ToString() + "_total_amount"))));
                                _kl2.set_allow_min_amount(new decimal?(Convert.ToDecimal(LSRequest.qq("t_" + num6.ToString() + "_allow_min_amount"))));
                                _kl2.set_allow_max_amount(new decimal?(Convert.ToDecimal(LSRequest.qq("t_" + num6.ToString() + "_allow_max_amount"))));
                                _kl2.set_allow_max_put_amount(new decimal?(Convert.ToDecimal(LSRequest.qq("t_" + num6.ToString() + "_allow_max_put_amount"))));
                                _kl2.set_max_appoint(new decimal?(Convert.ToDecimal(LSRequest.qq("t_" + num6.ToString() + "_max_appoint"))));
                                _kl2.set_above_quota(new decimal?(Convert.ToDecimal(LSRequest.qq("t_" + num6.ToString() + "_above_quota"))));
                                CallBLL.cz_play_kl8_bll.SetPlayByTradingSet(str7, _kl2);
                                num6++;
                            }
                            base.jysd_kc_log(rs, CallBLL.cz_play_kl8_bll.GetPlayByTradingSet("").Tables[0], this.lotteryId);
                        }
                        break;
                    }
                    case 6:
                    {
                        DataSet set32 = CallBLL.cz_play_k8sc_bll.GetPlayByTradingSet("4,5,6,7,8,9,10,11,12,13,14,15");
                        if (((set32 != null) && (set32.Tables.Count > 0)) && (set32.Tables[0].Rows.Count > 0))
                        {
                            rs = set32.Tables[0];
                            this.ValidInput(rs);
                            int num8 = 0;
                            foreach (DataRow row8 in rs.Rows)
                            {
                                string str9 = "";
                                switch (num8)
                                {
                                    case 0:
                                        str9 = "1,4,7,10,13";
                                        break;

                                    case 1:
                                        str9 = "2,5,8,11,14";
                                        break;

                                    case 2:
                                        str9 = "3,6,9,12,15";
                                        break;

                                    default:
                                        str9 = row8["play_id"].ToString();
                                        break;
                                }
                                cz_play_k8sc _ksc = new cz_play_k8sc();
                                _ksc.set_total_amount(new decimal?(Convert.ToDecimal(LSRequest.qq("t_" + num8.ToString() + "_total_amount"))));
                                _ksc.set_allow_min_amount(new decimal?(Convert.ToDecimal(LSRequest.qq("t_" + num8.ToString() + "_allow_min_amount"))));
                                _ksc.set_allow_max_amount(new decimal?(Convert.ToDecimal(LSRequest.qq("t_" + num8.ToString() + "_allow_max_amount"))));
                                _ksc.set_allow_max_put_amount(new decimal?(Convert.ToDecimal(LSRequest.qq("t_" + num8.ToString() + "_allow_max_put_amount"))));
                                _ksc.set_max_appoint(new decimal?(Convert.ToDecimal(LSRequest.qq("t_" + num8.ToString() + "_max_appoint"))));
                                _ksc.set_above_quota(new decimal?(Convert.ToDecimal(LSRequest.qq("t_" + num8.ToString() + "_above_quota"))));
                                CallBLL.cz_play_k8sc_bll.SetPlayByTradingSet(str9, _ksc);
                                num8++;
                            }
                            base.jysd_kc_log(rs, CallBLL.cz_play_k8sc_bll.GetPlayByTradingSet("4,5,6,7,8,9,10,11,12,13,14,15").Tables[0], this.lotteryId);
                        }
                        break;
                    }
                    case 7:
                    {
                        DataSet set33 = CallBLL.cz_play_pcdd_bll.GetPlayByTradingSet("71009,71010,71011,71012");
                        if (((set33 != null) && (set33.Tables.Count > 0)) && (set33.Tables[0].Rows.Count > 0))
                        {
                            rs = set33.Tables[0];
                            this.ValidInput(rs);
                            int num9 = 0;
                            foreach (DataRow row9 in rs.Rows)
                            {
                                string str10 = "";
                                switch (num9)
                                {
                                    case 6:
                                        str10 = "71007,71009,71011";
                                        break;

                                    case 7:
                                        str10 = "71008,71010,71012";
                                        break;

                                    default:
                                        str10 = row9["play_id"].ToString();
                                        break;
                                }
                                cz_play_pcdd _pcdd = new cz_play_pcdd();
                                _pcdd.set_total_amount(new decimal?(Convert.ToDecimal(LSRequest.qq("t_" + num9.ToString() + "_total_amount"))));
                                _pcdd.set_allow_min_amount(new decimal?(Convert.ToDecimal(LSRequest.qq("t_" + num9.ToString() + "_allow_min_amount"))));
                                _pcdd.set_allow_max_amount(new decimal?(Convert.ToDecimal(LSRequest.qq("t_" + num9.ToString() + "_allow_max_amount"))));
                                _pcdd.set_allow_max_put_amount(new decimal?(Convert.ToDecimal(LSRequest.qq("t_" + num9.ToString() + "_allow_max_put_amount"))));
                                _pcdd.set_max_appoint(new decimal?(Convert.ToDecimal(LSRequest.qq("t_" + num9.ToString() + "_max_appoint"))));
                                _pcdd.set_above_quota(new decimal?(Convert.ToDecimal(LSRequest.qq("t_" + num9.ToString() + "_above_quota"))));
                                CallBLL.cz_play_pcdd_bll.SetPlayByTradingSet(str10, _pcdd);
                                num9++;
                            }
                            base.jysd_kc_log(rs, CallBLL.cz_play_pcdd_bll.GetPlayByTradingSet("71009,71010,71011,71012").Tables[0], this.lotteryId);
                        }
                        break;
                    }
                    case 8:
                    {
                        DataSet set35 = CallBLL.cz_play_pkbjl_bll.GetPlayByTradingSet("");
                        if (((set35 != null) && (set35.Tables.Count > 0)) && (set35.Tables[0].Rows.Count > 0))
                        {
                            rs = set35.Tables[0];
                            this.ValidInput(rs);
                            int num11 = 0;
                            foreach (DataRow row11 in rs.Rows)
                            {
                                string str12;
                                str12 = str12 = row11["play_id"].ToString();
                                cz_play_pkbjl _pkbjl = new cz_play_pkbjl();
                                _pkbjl.set_total_amount(new decimal?(Convert.ToDecimal(LSRequest.qq("t_" + num11.ToString() + "_total_amount"))));
                                _pkbjl.set_allow_min_amount(new decimal?(Convert.ToDecimal(LSRequest.qq("t_" + num11.ToString() + "_allow_min_amount"))));
                                _pkbjl.set_allow_max_amount(new decimal?(Convert.ToDecimal(LSRequest.qq("t_" + num11.ToString() + "_allow_max_amount"))));
                                _pkbjl.set_allow_max_put_amount(new decimal?(Convert.ToDecimal(LSRequest.qq("t_" + num11.ToString() + "_allow_max_put_amount"))));
                                _pkbjl.set_max_appoint(new decimal?(Convert.ToDecimal(LSRequest.qq("t_" + num11.ToString() + "_max_appoint"))));
                                _pkbjl.set_above_quota(new decimal?(Convert.ToDecimal(LSRequest.qq("t_" + num11.ToString() + "_above_quota"))));
                                CallBLL.cz_play_pkbjl_bll.SetPlayByTradingSet(str12, _pkbjl);
                                num11++;
                            }
                            base.jysd_kc_log(rs, CallBLL.cz_play_pkbjl_bll.GetPlayByTradingSet("").Tables[0], this.lotteryId);
                        }
                        break;
                    }
                    case 9:
                    {
                        DataSet set34 = CallBLL.cz_play_xyft5_bll.GetPlayByTradingSet("1,2,3,4,36,37,38");
                        if (((set34 != null) && (set34.Tables.Count > 0)) && (set34.Tables[0].Rows.Count > 0))
                        {
                            rs = set34.Tables[0];
                            this.ValidInput(rs);
                            int num10 = 0;
                            foreach (DataRow row10 in rs.Rows)
                            {
                                string str11 = "";
                                switch (num10)
                                {
                                    case 0:
                                        str11 = "1,5,9,13,17,21,24,27,30,33";
                                        break;

                                    case 1:
                                        str11 = "2,6,10,14,18,22,25,28,31,34";
                                        break;

                                    case 2:
                                        str11 = "3,7,11,15,19,23,26,29,32,35";
                                        break;

                                    case 3:
                                        str11 = "4,8,12,16,20";
                                        break;

                                    default:
                                        str11 = row10["play_id"].ToString();
                                        break;
                                }
                                cz_play_xyft5 _xyft = new cz_play_xyft5();
                                _xyft.set_total_amount(new decimal?(Convert.ToDecimal(LSRequest.qq("t_" + num10.ToString() + "_total_amount"))));
                                _xyft.set_allow_min_amount(new decimal?(Convert.ToDecimal(LSRequest.qq("t_" + num10.ToString() + "_allow_min_amount"))));
                                _xyft.set_allow_max_amount(new decimal?(Convert.ToDecimal(LSRequest.qq("t_" + num10.ToString() + "_allow_max_amount"))));
                                _xyft.set_allow_max_put_amount(new decimal?(Convert.ToDecimal(LSRequest.qq("t_" + num10.ToString() + "_allow_max_put_amount"))));
                                _xyft.set_max_appoint(new decimal?(Convert.ToDecimal(LSRequest.qq("t_" + num10.ToString() + "_max_appoint"))));
                                _xyft.set_above_quota(new decimal?(Convert.ToDecimal(LSRequest.qq("t_" + num10.ToString() + "_above_quota"))));
                                CallBLL.cz_play_xyft5_bll.SetPlayByTradingSet(str11, _xyft);
                                num10++;
                            }
                            base.jysd_kc_log(rs, CallBLL.cz_play_xyft5_bll.GetPlayByTradingSet("1,2,3,4,36,37,38").Tables[0], this.lotteryId);
                        }
                        break;
                    }
                    case 10:
                    {
                        DataSet set36 = CallBLL.cz_play_jscar_bll.GetPlayByTradingSet("1,2,3,4,36,37,38");
                        if (((set36 != null) && (set36.Tables.Count > 0)) && (set36.Tables[0].Rows.Count > 0))
                        {
                            rs = set36.Tables[0];
                            this.ValidInput(rs);
                            int num12 = 0;
                            foreach (DataRow row12 in rs.Rows)
                            {
                                string str13 = "";
                                switch (num12)
                                {
                                    case 0:
                                        str13 = "1,5,9,13,17,21,24,27,30,33";
                                        break;

                                    case 1:
                                        str13 = "2,6,10,14,18,22,25,28,31,34";
                                        break;

                                    case 2:
                                        str13 = "3,7,11,15,19,23,26,29,32,35";
                                        break;

                                    case 3:
                                        str13 = "4,8,12,16,20";
                                        break;

                                    default:
                                        str13 = row12["play_id"].ToString();
                                        break;
                                }
                                cz_play_jscar _jscar = new cz_play_jscar();
                                _jscar.set_total_amount(new decimal?(Convert.ToDecimal(LSRequest.qq("t_" + num12.ToString() + "_total_amount"))));
                                _jscar.set_allow_min_amount(new decimal?(Convert.ToDecimal(LSRequest.qq("t_" + num12.ToString() + "_allow_min_amount"))));
                                _jscar.set_allow_max_amount(new decimal?(Convert.ToDecimal(LSRequest.qq("t_" + num12.ToString() + "_allow_max_amount"))));
                                _jscar.set_allow_max_put_amount(new decimal?(Convert.ToDecimal(LSRequest.qq("t_" + num12.ToString() + "_allow_max_put_amount"))));
                                _jscar.set_max_appoint(new decimal?(Convert.ToDecimal(LSRequest.qq("t_" + num12.ToString() + "_max_appoint"))));
                                _jscar.set_above_quota(new decimal?(Convert.ToDecimal(LSRequest.qq("t_" + num12.ToString() + "_above_quota"))));
                                CallBLL.cz_play_jscar_bll.SetPlayByTradingSet(str13, _jscar);
                                num12++;
                            }
                            base.jysd_kc_log(rs, CallBLL.cz_play_jscar_bll.GetPlayByTradingSet("1,2,3,4,36,37,38").Tables[0], this.lotteryId);
                        }
                        break;
                    }
                    case 11:
                    {
                        DataSet set37 = CallBLL.cz_play_speed5_bll.GetPlayByTradingSet("4,5,6,7,8,9,10,11,12,13,14,15");
                        if (((set37 != null) && (set37.Tables.Count > 0)) && (set37.Tables[0].Rows.Count > 0))
                        {
                            rs = set37.Tables[0];
                            this.ValidInput(rs);
                            int num13 = 0;
                            foreach (DataRow row13 in rs.Rows)
                            {
                                string str14 = "";
                                switch (num13)
                                {
                                    case 0:
                                        str14 = "1,4,7,10,13";
                                        break;

                                    case 1:
                                        str14 = "2,5,8,11,14";
                                        break;

                                    case 2:
                                        str14 = "3,6,9,12,15";
                                        break;

                                    default:
                                        str14 = row13["play_id"].ToString();
                                        break;
                                }
                                cz_play_speed5 _speed = new cz_play_speed5();
                                _speed.set_total_amount(new decimal?(Convert.ToDecimal(LSRequest.qq("t_" + num13.ToString() + "_total_amount"))));
                                _speed.set_allow_min_amount(new decimal?(Convert.ToDecimal(LSRequest.qq("t_" + num13.ToString() + "_allow_min_amount"))));
                                _speed.set_allow_max_amount(new decimal?(Convert.ToDecimal(LSRequest.qq("t_" + num13.ToString() + "_allow_max_amount"))));
                                _speed.set_allow_max_put_amount(new decimal?(Convert.ToDecimal(LSRequest.qq("t_" + num13.ToString() + "_allow_max_put_amount"))));
                                _speed.set_max_appoint(new decimal?(Convert.ToDecimal(LSRequest.qq("t_" + num13.ToString() + "_max_appoint"))));
                                _speed.set_above_quota(new decimal?(Convert.ToDecimal(LSRequest.qq("t_" + num13.ToString() + "_above_quota"))));
                                CallBLL.cz_play_speed5_bll.SetPlayByTradingSet(str14, _speed);
                                num13++;
                            }
                            base.jysd_kc_log(rs, CallBLL.cz_play_speed5_bll.GetPlayByTradingSet("4,5,6,7,8,9,10,11,12,13,14,15").Tables[0], this.lotteryId);
                        }
                        break;
                    }
                    case 12:
                    {
                        DataSet set39 = CallBLL.cz_play_jspk10_bll.GetPlayByTradingSet("1,2,3,4,36,37,38");
                        if (((set39 != null) && (set39.Tables.Count > 0)) && (set39.Tables[0].Rows.Count > 0))
                        {
                            rs = set39.Tables[0];
                            this.ValidInput(rs);
                            int num15 = 0;
                            foreach (DataRow row15 in rs.Rows)
                            {
                                string str16 = "";
                                switch (num15)
                                {
                                    case 0:
                                        str16 = "1,5,9,13,17,21,24,27,30,33";
                                        break;

                                    case 1:
                                        str16 = "2,6,10,14,18,22,25,28,31,34";
                                        break;

                                    case 2:
                                        str16 = "3,7,11,15,19,23,26,29,32,35";
                                        break;

                                    case 3:
                                        str16 = "4,8,12,16,20";
                                        break;

                                    default:
                                        str16 = row15["play_id"].ToString();
                                        break;
                                }
                                cz_play_jspk10 _jspk = new cz_play_jspk10();
                                _jspk.set_total_amount(new decimal?(Convert.ToDecimal(LSRequest.qq("t_" + num15.ToString() + "_total_amount"))));
                                _jspk.set_allow_min_amount(new decimal?(Convert.ToDecimal(LSRequest.qq("t_" + num15.ToString() + "_allow_min_amount"))));
                                _jspk.set_allow_max_amount(new decimal?(Convert.ToDecimal(LSRequest.qq("t_" + num15.ToString() + "_allow_max_amount"))));
                                _jspk.set_allow_max_put_amount(new decimal?(Convert.ToDecimal(LSRequest.qq("t_" + num15.ToString() + "_allow_max_put_amount"))));
                                _jspk.set_max_appoint(new decimal?(Convert.ToDecimal(LSRequest.qq("t_" + num15.ToString() + "_max_appoint"))));
                                _jspk.set_above_quota(new decimal?(Convert.ToDecimal(LSRequest.qq("t_" + num15.ToString() + "_above_quota"))));
                                CallBLL.cz_play_jspk10_bll.SetPlayByTradingSet(str16, _jspk);
                                num15++;
                            }
                            base.jysd_kc_log(rs, CallBLL.cz_play_jspk10_bll.GetPlayByTradingSet("1,2,3,4,36,37,38").Tables[0], this.lotteryId);
                        }
                        break;
                    }
                    case 13:
                    {
                        DataSet set38 = CallBLL.cz_play_jscqsc_bll.GetPlayByTradingSet("4,5,6,7,8,9,10,11,12,13,14,15");
                        if (((set38 != null) && (set38.Tables.Count > 0)) && (set38.Tables[0].Rows.Count > 0))
                        {
                            rs = set38.Tables[0];
                            this.ValidInput(rs);
                            int num14 = 0;
                            foreach (DataRow row14 in rs.Rows)
                            {
                                string str15 = "";
                                switch (num14)
                                {
                                    case 0:
                                        str15 = "1,4,7,10,13";
                                        break;

                                    case 1:
                                        str15 = "2,5,8,11,14";
                                        break;

                                    case 2:
                                        str15 = "3,6,9,12,15";
                                        break;

                                    default:
                                        str15 = row14["play_id"].ToString();
                                        break;
                                }
                                cz_play_jscqsc _jscqsc = new cz_play_jscqsc();
                                _jscqsc.set_total_amount(new decimal?(Convert.ToDecimal(LSRequest.qq("t_" + num14.ToString() + "_total_amount"))));
                                _jscqsc.set_allow_min_amount(new decimal?(Convert.ToDecimal(LSRequest.qq("t_" + num14.ToString() + "_allow_min_amount"))));
                                _jscqsc.set_allow_max_amount(new decimal?(Convert.ToDecimal(LSRequest.qq("t_" + num14.ToString() + "_allow_max_amount"))));
                                _jscqsc.set_allow_max_put_amount(new decimal?(Convert.ToDecimal(LSRequest.qq("t_" + num14.ToString() + "_allow_max_put_amount"))));
                                _jscqsc.set_max_appoint(new decimal?(Convert.ToDecimal(LSRequest.qq("t_" + num14.ToString() + "_max_appoint"))));
                                _jscqsc.set_above_quota(new decimal?(Convert.ToDecimal(LSRequest.qq("t_" + num14.ToString() + "_above_quota"))));
                                CallBLL.cz_play_jscqsc_bll.SetPlayByTradingSet(str15, _jscqsc);
                                num14++;
                            }
                            base.jysd_kc_log(rs, CallBLL.cz_play_jscqsc_bll.GetPlayByTradingSet("4,5,6,7,8,9,10,11,12,13,14,15").Tables[0], this.lotteryId);
                        }
                        break;
                    }
                    case 14:
                    {
                        DataSet set40 = CallBLL.cz_play_jssfc_bll.GetPlayByTradingSet("86,87,88,89,90,91,92,93,94,95,96,97,98,99,100,101,102,103,104,105,106,107,108,109,110,111,112,113,114,115,116,117,118,119,120,123,124,125,126,127,128,129,130,131,132,133,134,135,136");
                        if (((set40 != null) && (set40.Tables.Count > 0)) && (set40.Tables[0].Rows.Count > 0))
                        {
                            rs = set40.Tables[0];
                            this.ValidInput(rs);
                            int num16 = 0;
                            foreach (DataRow row16 in rs.Rows)
                            {
                                string str17 = "";
                                switch (num16)
                                {
                                    case 0:
                                        str17 = "81,86,91,96,101,106,111,116";
                                        break;

                                    case 1:
                                        str17 = "82,87,92,97,102,107,112,117";
                                        break;

                                    case 2:
                                        str17 = "83,88,93,98,103,108,113,118";
                                        break;

                                    case 3:
                                        str17 = "84,89,94,99,104,109,114,119";
                                        break;

                                    case 4:
                                        str17 = "85,90,95,100,105,110,115,120";
                                        break;

                                    case 5:
                                        str17 = "121,123,125,127,129,131,133,135";
                                        break;

                                    case 6:
                                        str17 = "122,124,126,128,130,132,134,136";
                                        break;

                                    default:
                                        str17 = row16["play_id"].ToString();
                                        break;
                                }
                                cz_play_jssfc _jssfc = new cz_play_jssfc();
                                _jssfc.set_total_amount(new decimal?(Convert.ToDecimal(LSRequest.qq("t_" + num16.ToString() + "_total_amount"))));
                                _jssfc.set_allow_min_amount(new decimal?(Convert.ToDecimal(LSRequest.qq("t_" + num16.ToString() + "_allow_min_amount"))));
                                _jssfc.set_allow_max_amount(new decimal?(Convert.ToDecimal(LSRequest.qq("t_" + num16.ToString() + "_allow_max_amount"))));
                                _jssfc.set_allow_max_put_amount(new decimal?(Convert.ToDecimal(LSRequest.qq("t_" + num16.ToString() + "_allow_max_put_amount"))));
                                _jssfc.set_max_appoint(new decimal?(Convert.ToDecimal(LSRequest.qq("t_" + num16.ToString() + "_max_appoint"))));
                                _jssfc.set_above_quota(new decimal?(Convert.ToDecimal(LSRequest.qq("t_" + num16.ToString() + "_above_quota"))));
                                CallBLL.cz_play_jssfc_bll.SetPlayByTradingSet(str17, _jssfc);
                                num16++;
                            }
                            base.jysd_kc_log(rs, CallBLL.cz_play_jssfc_bll.GetPlayByTradingSet("86,87,88,89,90,91,92,93,94,95,96,97,98,99,100,101,102,103,104,105,106,107,108,109,110,111,112,113,114,115,116,117,118,119,120,123,124,125,126,127,128,129,130,131,132,133,134,135,136").Tables[0], this.lotteryId);
                        }
                        break;
                    }
                    case 15:
                    {
                        DataSet set41 = CallBLL.cz_play_jsft2_bll.GetPlayByTradingSet("1,2,3,4,36,37,38");
                        if (((set41 != null) && (set41.Tables.Count > 0)) && (set41.Tables[0].Rows.Count > 0))
                        {
                            rs = set41.Tables[0];
                            this.ValidInput(rs);
                            int num17 = 0;
                            foreach (DataRow row17 in rs.Rows)
                            {
                                string str18 = "";
                                switch (num17)
                                {
                                    case 0:
                                        str18 = "1,5,9,13,17,21,24,27,30,33";
                                        break;

                                    case 1:
                                        str18 = "2,6,10,14,18,22,25,28,31,34";
                                        break;

                                    case 2:
                                        str18 = "3,7,11,15,19,23,26,29,32,35";
                                        break;

                                    case 3:
                                        str18 = "4,8,12,16,20";
                                        break;

                                    default:
                                        str18 = row17["play_id"].ToString();
                                        break;
                                }
                                cz_play_jsft2 _jsft = new cz_play_jsft2();
                                _jsft.set_total_amount(new decimal?(Convert.ToDecimal(LSRequest.qq("t_" + num17.ToString() + "_total_amount"))));
                                _jsft.set_allow_min_amount(new decimal?(Convert.ToDecimal(LSRequest.qq("t_" + num17.ToString() + "_allow_min_amount"))));
                                _jsft.set_allow_max_amount(new decimal?(Convert.ToDecimal(LSRequest.qq("t_" + num17.ToString() + "_allow_max_amount"))));
                                _jsft.set_allow_max_put_amount(new decimal?(Convert.ToDecimal(LSRequest.qq("t_" + num17.ToString() + "_allow_max_put_amount"))));
                                _jsft.set_max_appoint(new decimal?(Convert.ToDecimal(LSRequest.qq("t_" + num17.ToString() + "_max_appoint"))));
                                _jsft.set_above_quota(new decimal?(Convert.ToDecimal(LSRequest.qq("t_" + num17.ToString() + "_above_quota"))));
                                CallBLL.cz_play_jsft2_bll.SetPlayByTradingSet(str18, _jsft);
                                num17++;
                            }
                            base.jysd_kc_log(rs, CallBLL.cz_play_jsft2_bll.GetPlayByTradingSet("1,2,3,4,36,37,38").Tables[0], this.lotteryId);
                        }
                        break;
                    }
                    case 0x10:
                    {
                        DataSet set42 = CallBLL.cz_play_car168_bll.GetPlayByTradingSet("1,2,3,4,36,37,38");
                        if (((set42 != null) && (set42.Tables.Count > 0)) && (set42.Tables[0].Rows.Count > 0))
                        {
                            rs = set42.Tables[0];
                            this.ValidInput(rs);
                            int num18 = 0;
                            foreach (DataRow row18 in rs.Rows)
                            {
                                string str19 = "";
                                switch (num18)
                                {
                                    case 0:
                                        str19 = "1,5,9,13,17,21,24,27,30,33";
                                        break;

                                    case 1:
                                        str19 = "2,6,10,14,18,22,25,28,31,34";
                                        break;

                                    case 2:
                                        str19 = "3,7,11,15,19,23,26,29,32,35";
                                        break;

                                    case 3:
                                        str19 = "4,8,12,16,20";
                                        break;

                                    default:
                                        str19 = row18["play_id"].ToString();
                                        break;
                                }
                                cz_play_car168 _car = new cz_play_car168();
                                _car.set_total_amount(new decimal?(Convert.ToDecimal(LSRequest.qq("t_" + num18.ToString() + "_total_amount"))));
                                _car.set_allow_min_amount(new decimal?(Convert.ToDecimal(LSRequest.qq("t_" + num18.ToString() + "_allow_min_amount"))));
                                _car.set_allow_max_amount(new decimal?(Convert.ToDecimal(LSRequest.qq("t_" + num18.ToString() + "_allow_max_amount"))));
                                _car.set_allow_max_put_amount(new decimal?(Convert.ToDecimal(LSRequest.qq("t_" + num18.ToString() + "_allow_max_put_amount"))));
                                _car.set_max_appoint(new decimal?(Convert.ToDecimal(LSRequest.qq("t_" + num18.ToString() + "_max_appoint"))));
                                _car.set_above_quota(new decimal?(Convert.ToDecimal(LSRequest.qq("t_" + num18.ToString() + "_above_quota"))));
                                CallBLL.cz_play_car168_bll.SetPlayByTradingSet(str19, _car);
                                num18++;
                            }
                            base.jysd_kc_log(rs, CallBLL.cz_play_car168_bll.GetPlayByTradingSet("1,2,3,4,36,37,38").Tables[0], this.lotteryId);
                        }
                        break;
                    }
                    case 0x11:
                    {
                        DataSet set43 = CallBLL.cz_play_ssc168_bll.GetPlayByTradingSet("4,5,6,7,8,9,10,11,12,13,14,15");
                        if (((set43 != null) && (set43.Tables.Count > 0)) && (set43.Tables[0].Rows.Count > 0))
                        {
                            rs = set43.Tables[0];
                            this.ValidInput(rs);
                            int num19 = 0;
                            foreach (DataRow row19 in rs.Rows)
                            {
                                string str20 = "";
                                switch (num19)
                                {
                                    case 0:
                                        str20 = "1,4,7,10,13";
                                        break;

                                    case 1:
                                        str20 = "2,5,8,11,14";
                                        break;

                                    case 2:
                                        str20 = "3,6,9,12,15";
                                        break;

                                    default:
                                        str20 = row19["play_id"].ToString();
                                        break;
                                }
                                cz_play_ssc168 _ssc = new cz_play_ssc168();
                                _ssc.set_total_amount(new decimal?(Convert.ToDecimal(LSRequest.qq("t_" + num19.ToString() + "_total_amount"))));
                                _ssc.set_allow_min_amount(new decimal?(Convert.ToDecimal(LSRequest.qq("t_" + num19.ToString() + "_allow_min_amount"))));
                                _ssc.set_allow_max_amount(new decimal?(Convert.ToDecimal(LSRequest.qq("t_" + num19.ToString() + "_allow_max_amount"))));
                                _ssc.set_allow_max_put_amount(new decimal?(Convert.ToDecimal(LSRequest.qq("t_" + num19.ToString() + "_allow_max_put_amount"))));
                                _ssc.set_max_appoint(new decimal?(Convert.ToDecimal(LSRequest.qq("t_" + num19.ToString() + "_max_appoint"))));
                                _ssc.set_above_quota(new decimal?(Convert.ToDecimal(LSRequest.qq("t_" + num19.ToString() + "_above_quota"))));
                                CallBLL.cz_play_ssc168_bll.SetPlayByTradingSet(str20, _ssc);
                                num19++;
                            }
                            base.jysd_kc_log(rs, CallBLL.cz_play_ssc168_bll.GetPlayByTradingSet("4,5,6,7,8,9,10,11,12,13,14,15").Tables[0], this.lotteryId);
                        }
                        break;
                    }
                    case 0x12:
                    {
                        DataSet set44 = CallBLL.cz_play_vrcar_bll.GetPlayByTradingSet("1,2,3,4,36,37,38");
                        if (((set44 != null) && (set44.Tables.Count > 0)) && (set44.Tables[0].Rows.Count > 0))
                        {
                            rs = set44.Tables[0];
                            this.ValidInput(rs);
                            int num20 = 0;
                            foreach (DataRow row20 in rs.Rows)
                            {
                                string str21 = "";
                                switch (num20)
                                {
                                    case 0:
                                        str21 = "1,5,9,13,17,21,24,27,30,33";
                                        break;

                                    case 1:
                                        str21 = "2,6,10,14,18,22,25,28,31,34";
                                        break;

                                    case 2:
                                        str21 = "3,7,11,15,19,23,26,29,32,35";
                                        break;

                                    case 3:
                                        str21 = "4,8,12,16,20";
                                        break;

                                    default:
                                        str21 = row20["play_id"].ToString();
                                        break;
                                }
                                cz_play_vrcar _vrcar = new cz_play_vrcar();
                                _vrcar.set_total_amount(new decimal?(Convert.ToDecimal(LSRequest.qq("t_" + num20.ToString() + "_total_amount"))));
                                _vrcar.set_allow_min_amount(new decimal?(Convert.ToDecimal(LSRequest.qq("t_" + num20.ToString() + "_allow_min_amount"))));
                                _vrcar.set_allow_max_amount(new decimal?(Convert.ToDecimal(LSRequest.qq("t_" + num20.ToString() + "_allow_max_amount"))));
                                _vrcar.set_allow_max_put_amount(new decimal?(Convert.ToDecimal(LSRequest.qq("t_" + num20.ToString() + "_allow_max_put_amount"))));
                                _vrcar.set_max_appoint(new decimal?(Convert.ToDecimal(LSRequest.qq("t_" + num20.ToString() + "_max_appoint"))));
                                _vrcar.set_above_quota(new decimal?(Convert.ToDecimal(LSRequest.qq("t_" + num20.ToString() + "_above_quota"))));
                                CallBLL.cz_play_vrcar_bll.SetPlayByTradingSet(str21, _vrcar);
                                num20++;
                            }
                            base.jysd_kc_log(rs, CallBLL.cz_play_vrcar_bll.GetPlayByTradingSet("1,2,3,4,36,37,38").Tables[0], this.lotteryId);
                        }
                        break;
                    }
                    case 0x13:
                    {
                        DataSet set45 = CallBLL.cz_play_vrssc_bll.GetPlayByTradingSet("4,5,6,7,8,9,10,11,12,13,14,15");
                        if (((set45 != null) && (set45.Tables.Count > 0)) && (set45.Tables[0].Rows.Count > 0))
                        {
                            rs = set45.Tables[0];
                            this.ValidInput(rs);
                            int num21 = 0;
                            foreach (DataRow row21 in rs.Rows)
                            {
                                string str22 = "";
                                switch (num21)
                                {
                                    case 0:
                                        str22 = "1,4,7,10,13";
                                        break;

                                    case 1:
                                        str22 = "2,5,8,11,14";
                                        break;

                                    case 2:
                                        str22 = "3,6,9,12,15";
                                        break;

                                    default:
                                        str22 = row21["play_id"].ToString();
                                        break;
                                }
                                cz_play_vrssc _vrssc = new cz_play_vrssc();
                                _vrssc.set_total_amount(new decimal?(Convert.ToDecimal(LSRequest.qq("t_" + num21.ToString() + "_total_amount"))));
                                _vrssc.set_allow_min_amount(new decimal?(Convert.ToDecimal(LSRequest.qq("t_" + num21.ToString() + "_allow_min_amount"))));
                                _vrssc.set_allow_max_amount(new decimal?(Convert.ToDecimal(LSRequest.qq("t_" + num21.ToString() + "_allow_max_amount"))));
                                _vrssc.set_allow_max_put_amount(new decimal?(Convert.ToDecimal(LSRequest.qq("t_" + num21.ToString() + "_allow_max_put_amount"))));
                                _vrssc.set_max_appoint(new decimal?(Convert.ToDecimal(LSRequest.qq("t_" + num21.ToString() + "_max_appoint"))));
                                _vrssc.set_above_quota(new decimal?(Convert.ToDecimal(LSRequest.qq("t_" + num21.ToString() + "_above_quota"))));
                                CallBLL.cz_play_vrssc_bll.SetPlayByTradingSet(str22, _vrssc);
                                num21++;
                            }
                            base.jysd_kc_log(rs, CallBLL.cz_play_vrssc_bll.GetPlayByTradingSet("4,5,6,7,8,9,10,11,12,13,14,15").Tables[0], this.lotteryId);
                        }
                        break;
                    }
                    case 20:
                    {
                        DataSet set46 = CallBLL.cz_play_xyftoa_bll.GetPlayByTradingSet("1,2,3,4,36,37,38");
                        if (((set46 != null) && (set46.Tables.Count > 0)) && (set46.Tables[0].Rows.Count > 0))
                        {
                            rs = set46.Tables[0];
                            this.ValidInput(rs);
                            int num22 = 0;
                            foreach (DataRow row22 in rs.Rows)
                            {
                                string str23 = "";
                                switch (num22)
                                {
                                    case 0:
                                        str23 = "1,5,9,13,17,21,24,27,30,33";
                                        break;

                                    case 1:
                                        str23 = "2,6,10,14,18,22,25,28,31,34";
                                        break;

                                    case 2:
                                        str23 = "3,7,11,15,19,23,26,29,32,35";
                                        break;

                                    case 3:
                                        str23 = "4,8,12,16,20";
                                        break;

                                    default:
                                        str23 = row22["play_id"].ToString();
                                        break;
                                }
                                cz_play_xyftoa _xyftoa = new cz_play_xyftoa();
                                _xyftoa.set_total_amount(new decimal?(Convert.ToDecimal(LSRequest.qq("t_" + num22.ToString() + "_total_amount"))));
                                _xyftoa.set_allow_min_amount(new decimal?(Convert.ToDecimal(LSRequest.qq("t_" + num22.ToString() + "_allow_min_amount"))));
                                _xyftoa.set_allow_max_amount(new decimal?(Convert.ToDecimal(LSRequest.qq("t_" + num22.ToString() + "_allow_max_amount"))));
                                _xyftoa.set_allow_max_put_amount(new decimal?(Convert.ToDecimal(LSRequest.qq("t_" + num22.ToString() + "_allow_max_put_amount"))));
                                _xyftoa.set_max_appoint(new decimal?(Convert.ToDecimal(LSRequest.qq("t_" + num22.ToString() + "_max_appoint"))));
                                _xyftoa.set_above_quota(new decimal?(Convert.ToDecimal(LSRequest.qq("t_" + num22.ToString() + "_above_quota"))));
                                CallBLL.cz_play_xyftoa_bll.SetPlayByTradingSet(str23, _xyftoa);
                                num22++;
                            }
                            base.jysd_kc_log(rs, CallBLL.cz_play_xyftoa_bll.GetPlayByTradingSet("1,2,3,4,36,37,38").Tables[0], this.lotteryId);
                        }
                        break;
                    }
                    case 0x15:
                    {
                        DataSet set47 = CallBLL.cz_play_xyftsg_bll.GetPlayByTradingSet("1,2,3,4,36,37,38");
                        if (((set47 != null) && (set47.Tables.Count > 0)) && (set47.Tables[0].Rows.Count > 0))
                        {
                            rs = set47.Tables[0];
                            this.ValidInput(rs);
                            int num23 = 0;
                            foreach (DataRow row23 in rs.Rows)
                            {
                                string str24 = "";
                                switch (num23)
                                {
                                    case 0:
                                        str24 = "1,5,9,13,17,21,24,27,30,33";
                                        break;

                                    case 1:
                                        str24 = "2,6,10,14,18,22,25,28,31,34";
                                        break;

                                    case 2:
                                        str24 = "3,7,11,15,19,23,26,29,32,35";
                                        break;

                                    case 3:
                                        str24 = "4,8,12,16,20";
                                        break;

                                    default:
                                        str24 = row23["play_id"].ToString();
                                        break;
                                }
                                cz_play_xyftsg _xyftsg = new cz_play_xyftsg();
                                _xyftsg.set_total_amount(new decimal?(Convert.ToDecimal(LSRequest.qq("t_" + num23.ToString() + "_total_amount"))));
                                _xyftsg.set_allow_min_amount(new decimal?(Convert.ToDecimal(LSRequest.qq("t_" + num23.ToString() + "_allow_min_amount"))));
                                _xyftsg.set_allow_max_amount(new decimal?(Convert.ToDecimal(LSRequest.qq("t_" + num23.ToString() + "_allow_max_amount"))));
                                _xyftsg.set_allow_max_put_amount(new decimal?(Convert.ToDecimal(LSRequest.qq("t_" + num23.ToString() + "_allow_max_put_amount"))));
                                _xyftsg.set_max_appoint(new decimal?(Convert.ToDecimal(LSRequest.qq("t_" + num23.ToString() + "_max_appoint"))));
                                _xyftsg.set_above_quota(new decimal?(Convert.ToDecimal(LSRequest.qq("t_" + num23.ToString() + "_above_quota"))));
                                CallBLL.cz_play_xyftsg_bll.SetPlayByTradingSet(str24, _xyftsg);
                                num23++;
                            }
                            base.jysd_kc_log(rs, CallBLL.cz_play_xyftsg_bll.GetPlayByTradingSet("1,2,3,4,36,37,38").Tables[0], this.lotteryId);
                        }
                        break;
                    }
                    case 0x16:
                    {
                        DataSet set48 = CallBLL.cz_play_happycar_bll.GetPlayByTradingSet("1,2,3,4,36,37,38");
                        if (((set48 != null) && (set48.Tables.Count > 0)) && (set48.Tables[0].Rows.Count > 0))
                        {
                            rs = set48.Tables[0];
                            this.ValidInput(rs);
                            int num24 = 0;
                            foreach (DataRow row24 in rs.Rows)
                            {
                                string str25 = "";
                                switch (num24)
                                {
                                    case 0:
                                        str25 = "1,5,9,13,17,21,24,27,30,33";
                                        break;

                                    case 1:
                                        str25 = "2,6,10,14,18,22,25,28,31,34";
                                        break;

                                    case 2:
                                        str25 = "3,7,11,15,19,23,26,29,32,35";
                                        break;

                                    case 3:
                                        str25 = "4,8,12,16,20";
                                        break;

                                    default:
                                        str25 = row24["play_id"].ToString();
                                        break;
                                }
                                cz_play_happycar _happycar = new cz_play_happycar();
                                _happycar.set_total_amount(new decimal?(Convert.ToDecimal(LSRequest.qq("t_" + num24.ToString() + "_total_amount"))));
                                _happycar.set_allow_min_amount(new decimal?(Convert.ToDecimal(LSRequest.qq("t_" + num24.ToString() + "_allow_min_amount"))));
                                _happycar.set_allow_max_amount(new decimal?(Convert.ToDecimal(LSRequest.qq("t_" + num24.ToString() + "_allow_max_amount"))));
                                _happycar.set_allow_max_put_amount(new decimal?(Convert.ToDecimal(LSRequest.qq("t_" + num24.ToString() + "_allow_max_put_amount"))));
                                _happycar.set_max_appoint(new decimal?(Convert.ToDecimal(LSRequest.qq("t_" + num24.ToString() + "_max_appoint"))));
                                _happycar.set_above_quota(new decimal?(Convert.ToDecimal(LSRequest.qq("t_" + num24.ToString() + "_above_quota"))));
                                CallBLL.cz_play_happycar_bll.SetPlayByTradingSet(str25, _happycar);
                                num24++;
                            }
                            base.jysd_kc_log(rs, CallBLL.cz_play_happycar_bll.GetPlayByTradingSet("1,2,3,4,36,37,38").Tables[0], this.lotteryId);
                        }
                        break;
                    }
                    case 100:
                    {
                        DataSet set31 = CallBLL.cz_play_six_bll.GetPlayByTradingSet("91025,91026,91027,91028,91029");
                        if (((set31 != null) && (set31.Tables.Count > 0)) && (set31.Tables[0].Rows.Count > 0))
                        {
                            rs = set31.Tables[0];
                            this.ValidInput(rs);
                            int num7 = 0;
                            foreach (DataRow row7 in rs.Rows)
                            {
                                string str8 = "";
                                num25 = num7;
                                if (num25 == 8)
                                {
                                    str8 = "91010,91025,91026,91027,91028,91029";
                                }
                                else
                                {
                                    str8 = row7["play_id"].ToString();
                                }
                                cz_play_six _six = new cz_play_six();
                                _six.set_total_amount(new decimal?(Convert.ToDecimal(LSRequest.qq("t_" + num7.ToString() + "_total_amount"))));
                                _six.set_allow_min_amount(new decimal?(Convert.ToDecimal(LSRequest.qq("t_" + num7.ToString() + "_allow_min_amount"))));
                                _six.set_allow_max_amount(new decimal?(Convert.ToDecimal(LSRequest.qq("t_" + num7.ToString() + "_allow_max_amount"))));
                                _six.set_allow_max_put_amount(new decimal?(Convert.ToDecimal(LSRequest.qq("t_" + num7.ToString() + "_allow_max_put_amount"))));
                                _six.set_max_appoint(new decimal?(Convert.ToDecimal(LSRequest.qq("t_" + num7.ToString() + "_max_appoint"))));
                                _six.set_above_quota(new decimal?(Convert.ToDecimal(LSRequest.qq("t_" + num7.ToString() + "_above_quota"))));
                                CallBLL.cz_play_six_bll.SetPlayByTradingSet(str8, _six);
                                num7++;
                            }
                            base.jysd_six_log(rs, CallBLL.cz_play_six_bll.GetPlayByTradingSet("91025,91026,91027,91028,91029").Tables[0]);
                        }
                        break;
                    }
                }
                string url = "TradingSet.aspx?lid=" + this.lotteryId;
                base.Response.Write(base.ShowDialogBox(PageBase.GetMessageByCache("u100031", "MessageHint"), url, 0));
                base.Response.End();
            }
        }

        private void ValidInput(DataTable rs)
        {
            if (!string.IsNullOrEmpty(this.lotteryId))
            {
                for (int i = 0; i < rs.Rows.Count; i++)
                {
                    string str = this.GetPlayName(rs.Rows[i]["play_name"].ToString().Trim(), int.Parse(this.lotteryId), rs.Rows[i]["play_id"].ToString());
                    if (((string.IsNullOrEmpty(base.q("t_" + i.ToString() + "_total_amount")) || string.IsNullOrEmpty(base.q("t_" + i.ToString() + "_allow_min_amount"))) || (string.IsNullOrEmpty(base.q("t_" + i.ToString() + "_allow_max_amount")) || string.IsNullOrEmpty(base.q("t_" + i.ToString() + "_allow_max_put_amount")))) || (string.IsNullOrEmpty(base.q("t_" + i.ToString() + "_max_appoint")) || string.IsNullOrEmpty(base.q("t_" + i.ToString() + "_above_quota"))))
                    {
                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 交易設定不能為空！", str), null, 400));
                        base.Response.End();
                    }
                }
                for (int j = 0; j < rs.Rows.Count; j++)
                {
                    string str2 = this.GetPlayName(rs.Rows[j]["play_name"].ToString().Trim(), int.Parse(this.lotteryId), rs.Rows[j]["play_id"].ToString());
                    if (!base.IsNumber(base.q("t_" + j.ToString() + "_total_amount")))
                    {
                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 交易設定必須為數字！", str2), null, 400));
                        base.Response.End();
                    }
                    if (!base.IsNumber(base.q("t_" + j.ToString() + "_allow_min_amount")))
                    {
                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 交易設定必須為數字！", str2), null, 400));
                        base.Response.End();
                    }
                    if (!base.IsNumber(base.q("t_" + j.ToString() + "_allow_max_amount")))
                    {
                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 交易設定必須為數字！", str2), null, 400));
                        base.Response.End();
                    }
                    if (!base.IsNumber(base.q("t_" + j.ToString() + "_allow_max_put_amount")))
                    {
                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 交易設定必須為數字！", str2), null, 400));
                        base.Response.End();
                    }
                    if (!base.IsNumber(base.q("t_" + j.ToString() + "_max_appoint")))
                    {
                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 交易設定必須為數字！", str2), null, 400));
                        base.Response.End();
                    }
                    if (!base.IsNumber(base.q("t_" + j.ToString() + "_above_quota")))
                    {
                        base.Response.Write(base.ShowDialogBox(string.Format("‘{0}’ 交易設定必須為數字！", str2), null, 400));
                        base.Response.End();
                    }
                }
            }
        }
    }
}

