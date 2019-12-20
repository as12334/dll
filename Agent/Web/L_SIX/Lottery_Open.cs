namespace Agent.Web.L_SIX
{
    using Agent.Web.WebBase;
    using LotterySystem.Common;
    using LotterySystem.Model;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Text;

    public class Lottery_Open : MemberPageBase
    {
        protected string again = "0";
        protected string lid = "";
        protected cz_phase_six phaseModel;
        protected string pid = "";

        private string CoverString(string num)
        {
            if (string.IsNullOrEmpty(num))
            {
                return "";
            }
            if (num.Length == 1)
            {
                return ("0" + num);
            }
            return num;
        }

        protected string GetZodiacNameString(string n)
        {
            string zodiacName = base.GetZodiacName(n);
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("{0}", zodiacName);
            return builder.ToString();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string str = this.Session["user_name"].ToString();
            agent_userinfo_session model = this.Session[str + "lottery_session_user_info"] as agent_userinfo_session;
            if (!model.get_u_type().Trim().Equals("zj"))
            {
                base.Response.Redirect("../MessagePage.aspx?code=u100014&url=&issuccess=1&isback=0");
            }
            base.Permission_Aspx_ZJ(model, "po_3_1");
            if (base.IsChildSync())
            {
                base.Response.Redirect("../MessagePage.aspx?code=u100080&url=&issuccess=1&isback=0");
            }
            if (base.En_Balance_Lock(false).Equals("300"))
            {
                base.Response.Redirect("/MessagePage.aspx?code=u100082&url=&issuccess=1&isback=0&isopen=0");
                base.Response.End();
            }
            DataTable maxPhase = CallBLL.cz_phase_six_bll.GetMaxPhase();
            if (maxPhase == null)
            {
                base.Response.Write(base.ShowDialogBox("獎期錯誤！", "", 400));
                base.Response.End();
            }
            this.lid = LSRequest.qq("lid");
            this.pid = LSRequest.qq("pid");
            this.again = maxPhase.Rows[0]["is_opendata"].ToString();
            if (LSRequest.qq("hdnadd").Equals("add"))
            {
                this.pid = LSRequest.qq("hdnpid");
                this.lid = LSRequest.qq("hdnlid");
                string message = "";
                if (!this.ValidParam(ref message))
                {
                    base.Response.Write(base.ShowDialogBox(message, "", 400));
                    base.Response.End();
                }
                else
                {
                    if (maxPhase.Rows[0]["p_id"].ToString() != this.pid)
                    {
                        base.Response.Write(base.ShowDialogBox("獎期錯誤！", "", 400));
                        base.Response.End();
                    }
                    cz_phase_six _six = new cz_phase_six();
                    _six.set_p_id(Convert.ToInt32(this.pid));
                    _six.set_n1((this.CoverString(LSRequest.qq("zm1")).Equals("00") || this.CoverString(LSRequest.qq("zm1")).Equals("0")) ? "" : this.CoverString(LSRequest.qq("zm1")));
                    _six.set_n2((this.CoverString(LSRequest.qq("zm2")).Equals("00") || this.CoverString(LSRequest.qq("zm2")).Equals("0")) ? "" : this.CoverString(LSRequest.qq("zm2")));
                    _six.set_n3((this.CoverString(LSRequest.qq("zm3")).Equals("00") || this.CoverString(LSRequest.qq("zm3")).Equals("0")) ? "" : this.CoverString(LSRequest.qq("zm3")));
                    _six.set_n4((this.CoverString(LSRequest.qq("zm4")).Equals("00") || this.CoverString(LSRequest.qq("zm4")).Equals("0")) ? "" : this.CoverString(LSRequest.qq("zm4")));
                    _six.set_n5((this.CoverString(LSRequest.qq("zm5")).Equals("00") || this.CoverString(LSRequest.qq("zm5")).Equals("0")) ? "" : this.CoverString(LSRequest.qq("zm5")));
                    _six.set_n6((this.CoverString(LSRequest.qq("zm6")).Equals("00") || this.CoverString(LSRequest.qq("zm6")).Equals("0")) ? "" : this.CoverString(LSRequest.qq("zm6")));
                    _six.set_sn((this.CoverString(LSRequest.qq("tm")).Equals("00") || this.CoverString(LSRequest.qq("tm")).Equals("0")) ? "" : this.CoverString(LSRequest.qq("tm")));
                    if (!string.IsNullOrEmpty(_six.get_sn()))
                    {
                        _six.set_zodiac_sn(this.GetZodiacNameString(_six.get_sn()));
                    }
                    if (!string.IsNullOrEmpty(_six.get_n1()))
                    {
                        _six.set_zodiac_n1(this.GetZodiacNameString(_six.get_n1()));
                    }
                    if (!string.IsNullOrEmpty(_six.get_n2()))
                    {
                        _six.set_zodiac_n2(this.GetZodiacNameString(_six.get_n2()));
                    }
                    if (!string.IsNullOrEmpty(_six.get_n3()))
                    {
                        _six.set_zodiac_n3(this.GetZodiacNameString(_six.get_n3()));
                    }
                    if (!string.IsNullOrEmpty(_six.get_n4()))
                    {
                        _six.set_zodiac_n4(this.GetZodiacNameString(_six.get_n4()));
                    }
                    if (!string.IsNullOrEmpty(_six.get_n5()))
                    {
                        _six.set_zodiac_n5(this.GetZodiacNameString(_six.get_n5()));
                    }
                    if (!string.IsNullOrEmpty(_six.get_n6()))
                    {
                        _six.set_zodiac_n6(this.GetZodiacNameString(_six.get_n6()));
                    }
                    if (!CallBLL.cz_phase_six_bll.UpdatePhaseCode(_six))
                    {
                        base.Response.Write(base.ShowDialogBox("更新開獎碼時錯誤", "", 400));
                        base.Response.End();
                    }
                    else
                    {
                        string str5 = null;
                        if (model.get_users_child_session() != null)
                        {
                            str5 = model.get_users_child_session().get_u_name();
                        }
                        string str6 = string.Concat(new object[] { maxPhase.Rows[0]["n1"], " , ", maxPhase.Rows[0]["n2"], " , ", maxPhase.Rows[0]["n3"], " , ", maxPhase.Rows[0]["n4"], " , ", maxPhase.Rows[0]["n5"], " , ", maxPhase.Rows[0]["n6"], " + ", maxPhase.Rows[0]["sn"] });
                        string str7 = _six.get_n1() + " , " + _six.get_n2() + " , " + _six.get_n3() + " , " + _six.get_n4() + " , " + _six.get_n5() + " , " + _six.get_n6() + " + " + _six.get_sn();
                        cz_lotteryopen_log _log = new cz_lotteryopen_log();
                        _log.set_phase_id(int.Parse(this.pid));
                        _log.set_phase(maxPhase.Rows[0]["phase"].ToString());
                        _log.set_u_name(str);
                        _log.set_children_name(str5);
                        _log.set_action("送出號碼");
                        _log.set_old_val(str6);
                        _log.set_new_val(str7);
                        _log.set_ip(LSRequest.GetIP());
                        _log.set_add_time(DateTime.Now);
                        if (maxPhase.Rows[0]["is_opendata"].ToString().Equals("1"))
                        {
                            _log.set_note(string.Format("【本期編號:{0}】重新開獎", _log.get_phase()));
                        }
                        else
                        {
                            _log.set_note(string.Format("【本期編號:{0}】開獎", _log.get_phase()));
                        }
                        _log.set_type_id(0);
                        _log.set_lottery_id(100);
                        CallBLL.cz_lotteryopen_log_bll.Insert(_log);
                        base.Response.Write(string.Format("<script>location.href='Lottery_Open.aspx?pid={0}&lid={1}';</script>", this.pid, this.lid));
                        base.Response.End();
                    }
                }
            }
            this.phaseModel = CallBLL.cz_phase_six_bll.GetPhaseModel(Convert.ToInt32(this.pid));
            if (this.phaseModel == null)
            {
                base.Response.Redirect("../MessagePage.aspx?code=u100008&url=&issuccess=1&isback=0");
            }
        }

        private bool ValidParam(ref string message)
        {
            List<string> list = new List<string>();
            List<int> list2 = new List<int>();
            for (int i = 1; i < 7; i++)
            {
                string str = LSRequest.qq("zm" + i);
                if (!string.IsNullOrEmpty(str))
                {
                    list.Add(str);
                }
                else
                {
                    list2.Add(i);
                }
            }
            string str2 = LSRequest.qq("tm");
            if (!string.IsNullOrEmpty(str2))
            {
                list.Add(str2);
            }
            else
            {
                list2.Add(7);
            }
            if (list2.Count.Equals(7))
            {
                message = "請輸入開獎號碼！";
                return false;
            }
            for (int j = 0; j < list.Count; j++)
            {
                if (!base.IsNumber(list[j]))
                {
                    message = "開獎號碼格式錯誤!";
                    return false;
                }
                if ((list[j].Length == 1) || (list[j].Length > 2))
                {
                    message = "開獎號碼格式錯誤!";
                    return false;
                }
                if (int.Parse(list[j]) > 0x31)
                {
                    message = "開獎號碼格式錯誤!";
                    return false;
                }
                if (int.Parse(list[j]) < 1)
                {
                    message = "開獎號碼格式錯誤!";
                    return false;
                }
            }
            for (int k = 0; k < list.Count; k++)
            {
                string str3 = list[k];
                int num4 = 0;
                for (int m = 0; m < list.Count; m++)
                {
                    if (list[m].Equals(str3))
                    {
                        num4++;
                    }
                }
                if (num4 > 1)
                {
                    message = "開獎號碼重複錯誤!";
                    return false;
                }
            }
            return true;
        }
    }
}

