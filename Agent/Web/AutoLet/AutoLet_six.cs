namespace Agent.Web.AutoLet
{
    using Agent.Web.WebBase;
    using LotterySystem.Common;
    using LotterySystem.Model;
    using System;
    using System.Data;

    public class AutoLet_six : MemberPageBase
    {
        protected DataTable Autosale_DT;
        protected DataTable lotteryDT;
        protected string lotteryId = "";
        protected DataRow[] play_Row;
        protected string skin = "";
        protected string sltString = "";

        private bool IsIntMoney(string max_money)
        {
            bool flag = true;
            if (!string.IsNullOrEmpty(max_money))
            {
                try
                {
                    if (int.Parse(max_money) < 0)
                    {
                        flag = false;
                    }
                }
                catch
                {
                    flag = false;
                }
            }
            return flag;
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
            this.lotteryDT = base.GetLotteryList();
            this.lotteryId = LSRequest.qq("lid");
            int num = 100;
            if (!this.lotteryId.Equals(num.ToString()))
            {
                base.Response.Redirect("/AutoLet/AutoLet_kc.aspx?lid=" + this.lotteryId, true);
            }
            if (!model.get_allow_sale().Equals(1))
            {
                base.Response.Redirect("/MessagePage.aspx?code=u100037&url=&issuccess=1&isback=0");
                base.Response.End();
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
            DataTable play = null;
            DataSet autosaleByUserName = null;
            if (!FileCacheHelper.get_IsShowLM_B())
            {
                play = CallBLL.cz_play_six_bll.GetPlay("91060,91061,91062,91063,91064,91065");
                autosaleByUserName = CallBLL.cz_autosale_six_bll.GetAutosaleByUserName(str, "91060,91061,91062,91063,91064,91065");
            }
            else
            {
                play = CallBLL.cz_play_six_bll.GetPlay();
                autosaleByUserName = CallBLL.cz_autosale_six_bll.GetAutosaleByUserName(str);
            }
            if (play != null)
            {
                this.play_Row = play.Select(" play_id <>91002 and play_id <>91015 ", " sort asc");
            }
            if (((autosaleByUserName != null) && (autosaleByUserName.Tables.Count > 0)) && (autosaleByUserName.Tables[0].Rows.Count > 0))
            {
                this.Autosale_DT = autosaleByUserName.Tables[0];
            }
            if (LSRequest.qq("hdnSubmit").Equals("submit"))
            {
                this.SaveData();
            }
        }

        private void SaveData()
        {
            this.ValidInput();
            bool flag = true;
            DataTable table = null;
            if (!FileCacheHelper.get_IsShowLM_B())
            {
                table = CallBLL.cz_autosale_six_bll.GetAutosaleByUserName(this.Session["user_name"].ToString(), "91060,91061,91062,91063,91064,91065").Tables[0];
            }
            else
            {
                table = CallBLL.cz_autosale_six_bll.GetAutosaleByUserName(this.Session["user_name"].ToString()).Tables[0];
            }
            for (int i = 0; i < this.play_Row.Length; i++)
            {
                string str = this.play_Row[i]["play_id"].ToString();
                string str2 = this.play_Row[i]["play_name"].ToString();
                string str3 = LSRequest.qq("money_" + str);
                cz_autosale_six _six = new cz_autosale_six();
                _six.set_play_id(new int?(Convert.ToInt32(str)));
                _six.set_u_name(this.Session["user_name"].ToString());
                _six.set_max_money(new decimal?(!string.IsNullOrEmpty(str3) ? Convert.ToDecimal(str3) : 0M));
                _six.set_play_name(str2);
                _six.set_sale_type(0);
                _six.set_set_type(0);
                string isInsert = LSRequest.qq("chk_" + str);
                try
                {
                    if (str.Equals("91001"))
                    {
                        _six.set_play_name("特碼A+B");
                    }
                    CallBLL.cz_autosale_six_bll.SetAutosale(_six, isInsert);
                }
                catch
                {
                    flag = false;
                    break;
                }
                if ((table == null) && (isInsert == "1"))
                {
                    base.autosale_set_six_log(null, _six, isInsert, 100);
                }
                else
                {
                    DataRow[] rowArray = table.Select(string.Format(" play_id={0} ", _six.get_play_id()));
                    if ((rowArray.Length <= 0) && (isInsert == "1"))
                    {
                        base.autosale_set_six_log(rowArray, _six, isInsert, 100);
                    }
                    else if (rowArray.Length == 1)
                    {
                        base.autosale_set_six_log(rowArray, _six, isInsert, 100);
                    }
                }
            }
            if (flag)
            {
                string url = "/AutoLet/AutoLet_six.aspx?lid=" + this.lotteryId;
                base.Response.Write(base.ShowDialogBox("設置自動補貨成功！", url, 0));
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
            for (int i = 0; i < this.play_Row.Length; i++)
            {
                string str = this.play_Row[i]["play_id"].ToString();
                string str2 = LSRequest.qq("money_" + str);
                if (LSRequest.qq("chk_" + str).Equals("1"))
                {
                    bool flag2 = Utils.IsInteger(str2);
                    bool flag3 = this.IsIntMoney(str2);
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
    }
}

