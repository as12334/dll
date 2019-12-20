namespace Agent.Web.WebBase
{
    using LotterySystem.Common;
    using LotterySystem.Common.ServiceStackRedis;
    using LotterySystem.DBUtility;
    using LotterySystem.Model;
    using LotterySystem.WebPageBase;
    using ServiceStack.Redis;
    using System;
    using System.Collections;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.Data.SqlClient;
    using System.IO;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Threading;
    using System.Web;

    public class MemberPageBase : PageBase
    {
        public const int Log_All = 300;
        public const int Log_KC = 200;
        public const int Log_Six = 100;

        public MemberPageBase()
        {
            base.Load += new EventHandler(this.MemberPageBase_Load);
        }

        public MemberPageBase(string mobile)
        {
        }

        protected string add_autosale_log(string changed_user, int lottery_id, string edit_master_name, string edit_child_name, string note, string old_val, string new_val, ref List<SqlParameter> paramList)
        {
            string iP = LSRequest.GetIP();
            DateTime now = DateTime.Now;
            string str2 = string.Format("insert into cz_autosale_log ([u_name],[lottery_id],[old_val],[new_val],[note],[edit_time],[edit_user],[edit_children_user],[ip]) values (@u_name,@lottery_id,@old_val,@new_val,@note,@edit_time,@edit_user,@edit_children_user,@ip)", new object[] { changed_user, lottery_id, old_val, new_val, note, now, edit_master_name, edit_child_name, iP });
            SqlParameter item = new SqlParameter("@u_name", SqlDbType.NVarChar) {
                Value = changed_user
            };
            paramList.Add(item);
            SqlParameter parameter2 = new SqlParameter("@lottery_id", SqlDbType.Int) {
                Value = lottery_id
            };
            paramList.Add(parameter2);
            SqlParameter parameter3 = new SqlParameter("@old_val", SqlDbType.NVarChar) {
                Value = old_val
            };
            paramList.Add(parameter3);
            SqlParameter parameter4 = new SqlParameter("@new_val", SqlDbType.NVarChar) {
                Value = new_val
            };
            paramList.Add(parameter4);
            SqlParameter parameter5 = new SqlParameter("@note", SqlDbType.NVarChar) {
                Value = note
            };
            paramList.Add(parameter5);
            SqlParameter parameter6 = new SqlParameter("@edit_time", SqlDbType.DateTime) {
                Value = now
            };
            paramList.Add(parameter6);
            SqlParameter parameter7 = new SqlParameter("@edit_user", SqlDbType.NVarChar) {
                Value = edit_master_name
            };
            paramList.Add(parameter7);
            SqlParameter parameter8 = new SqlParameter("@edit_children_user", SqlDbType.NVarChar) {
                Value = edit_child_name
            };
            paramList.Add(parameter8);
            SqlParameter parameter9 = new SqlParameter("@ip", SqlDbType.NVarChar) {
                Value = iP
            };
            paramList.Add(parameter9);
            return str2;
        }

        protected string add_fgs_log(string user_name, string children_name, string category, string play_name, string put_val, string lottery_name, string phase_number, string act, int odds_id, string old_val, string new_val, string note, int type_id, int lottery_type, ref List<SqlParameter> paramList)
        {
            string iP = LSRequest.GetIP();
            DateTime now = DateTime.Now;
            string str2 = string.Format("insert into cz_fgs_opt_log (user_name,children_name,category,play_name,put_amount,l_name,l_phase,action,odds_id,old_val,new_val,ip,add_time,note,type_id,lottery_type) values(@user_name,@children_name,@category,@play_name,@put_amount,@l_name,@l_phase,@action,@odds_id,@old_val,@new_val,@ip,@add_time,@note,@type_id,@lottery_type)", new object[0]);
            SqlParameter item = new SqlParameter("@user_name", SqlDbType.NVarChar) {
                Value = user_name
            };
            paramList.Add(item);
            SqlParameter parameter2 = new SqlParameter("@children_name", SqlDbType.NVarChar) {
                Value = children_name
            };
            paramList.Add(parameter2);
            SqlParameter parameter3 = new SqlParameter("@category", SqlDbType.NVarChar) {
                Value = category
            };
            paramList.Add(parameter3);
            SqlParameter parameter4 = new SqlParameter("@play_name", SqlDbType.NVarChar) {
                Value = play_name
            };
            paramList.Add(parameter4);
            SqlParameter parameter5 = new SqlParameter("@put_amount", SqlDbType.NVarChar) {
                Value = put_val
            };
            paramList.Add(parameter5);
            SqlParameter parameter6 = new SqlParameter("@l_name", SqlDbType.NVarChar) {
                Value = lottery_name
            };
            paramList.Add(parameter6);
            SqlParameter parameter7 = new SqlParameter("@l_phase", SqlDbType.NVarChar) {
                Value = phase_number
            };
            paramList.Add(parameter7);
            SqlParameter parameter8 = new SqlParameter("@action", SqlDbType.NVarChar) {
                Value = act
            };
            paramList.Add(parameter8);
            SqlParameter parameter9 = new SqlParameter("@odds_id", SqlDbType.Int) {
                Value = odds_id
            };
            paramList.Add(parameter9);
            SqlParameter parameter10 = new SqlParameter("@old_val", SqlDbType.NVarChar) {
                Value = old_val
            };
            paramList.Add(parameter10);
            SqlParameter parameter11 = new SqlParameter("@new_val", SqlDbType.NVarChar) {
                Value = new_val
            };
            paramList.Add(parameter11);
            SqlParameter parameter12 = new SqlParameter("@ip", SqlDbType.NVarChar) {
                Value = iP
            };
            paramList.Add(parameter12);
            SqlParameter parameter13 = new SqlParameter("@add_time", SqlDbType.DateTime) {
                Value = now
            };
            paramList.Add(parameter13);
            SqlParameter parameter14 = new SqlParameter("@note", SqlDbType.NVarChar) {
                Value = note
            };
            paramList.Add(parameter14);
            SqlParameter parameter15 = new SqlParameter("@type_id", SqlDbType.Int) {
                Value = type_id
            };
            paramList.Add(parameter15);
            SqlParameter parameter16 = new SqlParameter("@lottery_type", SqlDbType.Int) {
                Value = lottery_type
            };
            paramList.Add(parameter16);
            return str2;
        }

        public void Add_FgsOddsWT_Lock(string lottery_type, string fgs_name, string odds_id)
        {
            while (true)
            {
                if (HttpContext.Current.Application[fgs_name + "_FgsOddsWT_" + lottery_type + "_" + odds_id] == null)
                {
                    break;
                }
                int num = (int) Math.Floor(Utils.DateDiff(Convert.ToDateTime(HttpContext.Current.Application["FgsOddsWT_" + lottery_type + "_" + odds_id].ToString()), DateTime.Now).TotalMilliseconds);
                if (num > 0x1f40)
                {
                    break;
                }
                Thread.Sleep(100);
            }
            HttpContext.Current.Application.Lock();
            HttpContext.Current.Application[fgs_name + "_FgsOddsWT_" + lottery_type + "_" + odds_id] = DateTime.Now.ToString();
            HttpContext.Current.Application.UnLock();
        }

        protected string add_sys_log(string user_name, string children_name, string category, string play_name, string put_val, string lottery_name, string phase_number, string act, int odds_id, string old_val, string new_val, string note, int type_id, int lottery_type, ref List<SqlParameter> paramList)
        {
            string iP = LSRequest.GetIP();
            DateTime now = DateTime.Now;
            string str2 = string.Format("insert into cz_system_log (user_name,children_name,category,play_name,put_amount,l_name,l_phase,action,odds_id,old_val,new_val,ip,add_time,note,type_id,lottery_type) values (@user_name,@children_name,@category,@play_name,@put_amount,@l_name,@l_phase,@action,@odds_id,@old_val,@new_val,@ip,@add_time,@note,@type_id,@lottery_type)", new object[0]);
            SqlParameter item = new SqlParameter("@user_name", SqlDbType.NVarChar) {
                Value = user_name
            };
            paramList.Add(item);
            SqlParameter parameter2 = new SqlParameter("@children_name", SqlDbType.NVarChar) {
                Value = children_name
            };
            paramList.Add(parameter2);
            SqlParameter parameter3 = new SqlParameter("@category", SqlDbType.NVarChar) {
                Value = category
            };
            paramList.Add(parameter3);
            SqlParameter parameter4 = new SqlParameter("@play_name", SqlDbType.NVarChar) {
                Value = play_name
            };
            paramList.Add(parameter4);
            SqlParameter parameter5 = new SqlParameter("@put_amount", SqlDbType.NVarChar) {
                Value = put_val
            };
            paramList.Add(parameter5);
            SqlParameter parameter6 = new SqlParameter("@l_name", SqlDbType.NVarChar) {
                Value = lottery_name
            };
            paramList.Add(parameter6);
            SqlParameter parameter7 = new SqlParameter("@l_phase", SqlDbType.NVarChar) {
                Value = phase_number
            };
            paramList.Add(parameter7);
            SqlParameter parameter8 = new SqlParameter("@action", SqlDbType.NVarChar) {
                Value = act
            };
            paramList.Add(parameter8);
            SqlParameter parameter9 = new SqlParameter("@odds_id", SqlDbType.Int) {
                Value = odds_id
            };
            paramList.Add(parameter9);
            SqlParameter parameter10 = new SqlParameter("@old_val", SqlDbType.NVarChar) {
                Value = old_val
            };
            paramList.Add(parameter10);
            SqlParameter parameter11 = new SqlParameter("@new_val", SqlDbType.NVarChar) {
                Value = new_val
            };
            paramList.Add(parameter11);
            SqlParameter parameter12 = new SqlParameter("@ip", SqlDbType.NVarChar) {
                Value = iP
            };
            paramList.Add(parameter12);
            SqlParameter parameter13 = new SqlParameter("@add_time", SqlDbType.DateTime) {
                Value = now
            };
            paramList.Add(parameter13);
            SqlParameter parameter14 = new SqlParameter("@note", SqlDbType.NVarChar) {
                Value = note
            };
            paramList.Add(parameter14);
            SqlParameter parameter15 = new SqlParameter("@type_id", SqlDbType.Int) {
                Value = type_id
            };
            paramList.Add(parameter15);
            SqlParameter parameter16 = new SqlParameter("@lottery_type", SqlDbType.Int) {
                Value = lottery_type
            };
            paramList.Add(parameter16);
            return str2;
        }

        protected string add_sys_set_log(string user_name, string children_name, string act, string old_val, string new_val, string note, int type_id, ref List<SqlParameter> paramList)
        {
            string iP = LSRequest.GetIP();
            DateTime now = DateTime.Now;
            string str2 = string.Format("insert into cz_system_set_log (user_name,children_name,action,old_val,new_val,ip,add_time,note,type_id) values(@user_name,@children_name,@action,@old_val,@new_val,@ip,@add_time,@note,@type_id)", new object[0]);
            SqlParameter item = new SqlParameter("@user_name", SqlDbType.NVarChar) {
                Value = user_name
            };
            paramList.Add(item);
            SqlParameter parameter2 = new SqlParameter("@children_name", SqlDbType.NVarChar) {
                Value = children_name
            };
            paramList.Add(parameter2);
            SqlParameter parameter3 = new SqlParameter("@action", SqlDbType.NVarChar) {
                Value = act
            };
            paramList.Add(parameter3);
            SqlParameter parameter4 = new SqlParameter("@old_val", SqlDbType.NVarChar) {
                Value = old_val
            };
            paramList.Add(parameter4);
            SqlParameter parameter5 = new SqlParameter("@new_val", SqlDbType.NVarChar) {
                Value = new_val
            };
            paramList.Add(parameter5);
            SqlParameter parameter6 = new SqlParameter("@ip", SqlDbType.NVarChar) {
                Value = iP
            };
            paramList.Add(parameter6);
            SqlParameter parameter7 = new SqlParameter("@add_time", SqlDbType.DateTime) {
                Value = now
            };
            paramList.Add(parameter7);
            SqlParameter parameter8 = new SqlParameter("@note", SqlDbType.NVarChar) {
                Value = note
            };
            paramList.Add(parameter8);
            SqlParameter parameter9 = new SqlParameter("@type_id", SqlDbType.Int) {
                Value = type_id
            };
            paramList.Add(parameter9);
            return str2;
        }

        protected string add_user_change_log(string changed_user, int lottery_id, string edit_master_name, string edit_child_name, string note, string old_val, string new_val, ref List<SqlParameter> paramList)
        {
            string iP = LSRequest.GetIP();
            DateTime now = DateTime.Now;
            string str2 = string.Format("insert into cz_user_change_log (u_name,lottery_id,old_val,new_val,note,edit_time,edit_user,edit_children_user,ip) values (@u_name,@lottery_id,@old_val,@new_val,@note,@edit_time,@edit_user,@edit_children_user,@ip)", new object[0]);
            SqlParameter item = new SqlParameter("@u_name", SqlDbType.NVarChar) {
                Value = changed_user
            };
            paramList.Add(item);
            SqlParameter parameter2 = new SqlParameter("@lottery_id", SqlDbType.Int) {
                Value = lottery_id
            };
            paramList.Add(parameter2);
            SqlParameter parameter3 = new SqlParameter("@old_val", SqlDbType.NVarChar) {
                Value = old_val
            };
            paramList.Add(parameter3);
            SqlParameter parameter4 = new SqlParameter("@new_val", SqlDbType.NVarChar) {
                Value = new_val
            };
            paramList.Add(parameter4);
            SqlParameter parameter5 = new SqlParameter("@note", SqlDbType.NVarChar) {
                Value = note
            };
            paramList.Add(parameter5);
            SqlParameter parameter6 = new SqlParameter("@edit_time", SqlDbType.DateTime) {
                Value = now
            };
            paramList.Add(parameter6);
            SqlParameter parameter7 = new SqlParameter("@edit_user", SqlDbType.NVarChar) {
                Value = edit_master_name
            };
            paramList.Add(parameter7);
            SqlParameter parameter8 = new SqlParameter("@edit_children_user", SqlDbType.NVarChar) {
                Value = edit_child_name
            };
            paramList.Add(parameter8);
            SqlParameter parameter9 = new SqlParameter("@ip", SqlDbType.NVarChar) {
                Value = iP
            };
            paramList.Add(parameter9);
            return str2;
        }

        public void AgentCurrentLottery()
        {
            this.Context.Request.Path.ToLower();
            if (this.Context.Request.Path.ToLower().IndexOf("l_six") > -1)
            {
                CacheHelper.SetCache("cachecurrentmlid", 1.ToString());
                CacheHelper.SetCache("cachecurrentlid", 100.ToString());
            }
            else if (this.Context.Request.Path.ToLower().IndexOf("l_kl10") > -1)
            {
                CacheHelper.SetCache("cachecurrentmlid", 2.ToString());
                CacheHelper.SetCache("cachecurrentlid", 0.ToString());
            }
            else if (this.Context.Request.Path.ToLower().IndexOf("l_cqsc") > -1)
            {
                CacheHelper.SetCache("cachecurrentmlid", 2.ToString());
                CacheHelper.SetCache("cachecurrentlid", 1.ToString());
            }
            else if (this.Context.Request.Path.ToLower().IndexOf("l_pk10") > -1)
            {
                CacheHelper.SetCache("cachecurrentmlid", 2.ToString());
                CacheHelper.SetCache("cachecurrentlid", 2.ToString());
            }
            else if (this.Context.Request.Path.ToLower().IndexOf("l_xync") > -1)
            {
                CacheHelper.SetCache("cachecurrentmlid", 2.ToString());
                CacheHelper.SetCache("cachecurrentlid", 3.ToString());
            }
            else if (this.Context.Request.Path.ToLower().IndexOf("l_k3") > -1)
            {
                CacheHelper.SetCache("cachecurrentmlid", 2.ToString());
                CacheHelper.SetCache("cachecurrentlid", 4.ToString());
            }
            else if (this.Context.Request.Path.ToLower().IndexOf("l_kl8") > -1)
            {
                CacheHelper.SetCache("cachecurrentmlid", 2.ToString());
                CacheHelper.SetCache("cachecurrentlid", 5.ToString());
            }
            else if (this.Context.Request.Path.ToLower().IndexOf("l_k8sc") > -1)
            {
                CacheHelper.SetCache("cachecurrentmlid", 2.ToString());
                CacheHelper.SetCache("cachecurrentlid", 6.ToString());
            }
            else if (this.Context.Request.Path.ToLower().IndexOf("l_pcdd") > -1)
            {
                CacheHelper.SetCache("cachecurrentmlid", 2.ToString());
                CacheHelper.SetCache("cachecurrentlid", 7.ToString());
            }
            else if (this.Context.Request.Path.ToLower().IndexOf("l_xyft5") > -1)
            {
                CacheHelper.SetCache("cachecurrentmlid", 2.ToString());
                CacheHelper.SetCache("cachecurrentlid", 9.ToString());
            }
            else if (this.Context.Request.Path.ToLower().IndexOf("l_pkbjl") > -1)
            {
                CacheHelper.SetCache("cachecurrentmlid", 2.ToString());
                CacheHelper.SetCache("cachecurrentlid", 8.ToString());
            }
            else if (this.Context.Request.Path.ToLower().IndexOf("l_jscar") > -1)
            {
                CacheHelper.SetCache("cachecurrentmlid", 2.ToString());
                CacheHelper.SetCache("cachecurrentlid", 10.ToString());
            }
            else if (this.Context.Request.Path.ToLower().IndexOf("l_speed5") > -1)
            {
                CacheHelper.SetCache("cachecurrentmlid", 2.ToString());
                CacheHelper.SetCache("cachecurrentlid", 11.ToString());
            }
            else if (this.Context.Request.Path.ToLower().IndexOf("l_jscqsc") > -1)
            {
                CacheHelper.SetCache("cachecurrentmlid", 2.ToString());
                CacheHelper.SetCache("cachecurrentlid", 13.ToString());
            }
            else if (this.Context.Request.Path.ToLower().IndexOf("l_jspk10") > -1)
            {
                CacheHelper.SetCache("cachecurrentmlid", 2.ToString());
                CacheHelper.SetCache("cachecurrentlid", 12.ToString());
            }
            else if (this.Context.Request.Path.ToLower().IndexOf("l_jssfc") > -1)
            {
                CacheHelper.SetCache("cachecurrentmlid", 2.ToString());
                CacheHelper.SetCache("cachecurrentlid", 14.ToString());
            }
            else if (this.Context.Request.Path.ToLower().IndexOf("l_jsft2") > -1)
            {
                CacheHelper.SetCache("cachecurrentmlid", 2.ToString());
                CacheHelper.SetCache("cachecurrentlid", 15.ToString());
            }
            else if (this.Context.Request.Path.ToLower().IndexOf("l_car168") > -1)
            {
                CacheHelper.SetCache("cachecurrentmlid", 2.ToString());
                CacheHelper.SetCache("cachecurrentlid", 0x10.ToString());
            }
            else if (this.Context.Request.Path.ToLower().IndexOf("l_ssc168") > -1)
            {
                CacheHelper.SetCache("cachecurrentmlid", 2.ToString());
                CacheHelper.SetCache("cachecurrentlid", 0x11.ToString());
            }
            else if (this.Context.Request.Path.ToLower().IndexOf("l_vrcar") > -1)
            {
                CacheHelper.SetCache("cachecurrentmlid", 2.ToString());
                CacheHelper.SetCache("cachecurrentlid", 0x12.ToString());
            }
            else if (this.Context.Request.Path.ToLower().IndexOf("l_vrssc") > -1)
            {
                CacheHelper.SetCache("cachecurrentmlid", 2.ToString());
                CacheHelper.SetCache("cachecurrentlid", 0x13.ToString());
            }
            else if (this.Context.Request.Path.ToLower().IndexOf("l_xyftoa") > -1)
            {
                CacheHelper.SetCache("cachecurrentmlid", 2.ToString());
                CacheHelper.SetCache("cachecurrentlid", 20.ToString());
            }
            else if (this.Context.Request.Path.ToLower().IndexOf("l_xyftsg") > -1)
            {
                CacheHelper.SetCache("cachecurrentmlid", 2.ToString());
                CacheHelper.SetCache("cachecurrentlid", 0x15.ToString());
            }
            else if (this.Context.Request.Path.ToLower().IndexOf("l_happycar") > -1)
            {
                CacheHelper.SetCache("cachecurrentmlid", 2.ToString());
                CacheHelper.SetCache("cachecurrentlid", 0x16.ToString());
            }
        }

        public bool AgentOperateValid(string permissions_name)
        {
            return false;
        }

        public string AlertString(string skin)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("<link href=\"../Plus/artDialog/css/ui-dialog.css\" rel=\"stylesheet\" type=\"text/css\" />");
            builder.Append("<script src=\"../Scripts/sea.js\" type=\"text/javascript\"></script>");
            builder.Append("<script src=\"../Scripts/otherConfig.js\" type=\"text/javascript\"></script>");
            return builder.ToString();
        }

        public void autosale_set_kc_log(DataRow[] old_rows, object model, string isInsert, int lottery_type)
        {
            string str = "";
            string str2 = "";
            decimal? nullable = 0M;
            if (lottery_type == 0)
            {
                cz_autosale_kl10 _kl = model as cz_autosale_kl10;
                str = _kl.get_u_name();
                nullable = _kl.get_max_money();
                if (_kl.get_play_id().Equals(0x51))
                {
                    str2 = "1-8球";
                }
                else if (_kl.get_play_id().Equals(0x52))
                {
                    str2 = "1-8球大小";
                }
                else if (_kl.get_play_id().Equals(0x53))
                {
                    str2 = "1-8球單雙";
                }
                else if (_kl.get_play_id().Equals(0x54))
                {
                    str2 = "1-8球尾數大小";
                }
                else if (_kl.get_play_id().Equals(0x55))
                {
                    str2 = "1-8球合數單雙";
                }
                else if (_kl.get_play_id().Equals(0x79))
                {
                    str2 = "1-8球方位";
                }
                else if (_kl.get_play_id().Equals(0x7a))
                {
                    str2 = "1-8球中發白";
                }
                else
                {
                    str2 = _kl.get_play_name();
                }
            }
            else if (lottery_type == 5)
            {
                cz_autosale_kl8 _kl2 = model as cz_autosale_kl8;
                str = _kl2.get_u_name();
                str2 = _kl2.get_play_name();
                nullable = _kl2.get_max_money();
            }
            else if (lottery_type == 4)
            {
                cz_autosale_jsk3 _jsk = model as cz_autosale_jsk3;
                str = _jsk.get_u_name();
                str2 = _jsk.get_play_name();
                nullable = _jsk.get_max_money();
            }
            else if (lottery_type == 3)
            {
                cz_autosale_xync _xync = model as cz_autosale_xync;
                str = _xync.get_u_name();
                nullable = _xync.get_max_money();
                if (_xync.get_play_id().Equals(0x51))
                {
                    str2 = "1-8球";
                }
                else if (_xync.get_play_id().Equals(0x52))
                {
                    str2 = "1-8球大小";
                }
                else if (_xync.get_play_id().Equals(0x53))
                {
                    str2 = "1-8球單雙";
                }
                else if (_xync.get_play_id().Equals(0x54))
                {
                    str2 = "1-8球尾數大小";
                }
                else if (_xync.get_play_id().Equals(0x55))
                {
                    str2 = "1-8球合數單雙";
                }
                else if (_xync.get_play_id().Equals(0x79))
                {
                    str2 = "1-8球梅蘭竹菊";
                }
                else if (_xync.get_play_id().Equals(0x7a))
                {
                    str2 = "1-8球中发白";
                }
                else
                {
                    str2 = _xync.get_play_name();
                }
            }
            else if (lottery_type == 2)
            {
                cz_autosale_pk10 _pk = model as cz_autosale_pk10;
                str = _pk.get_u_name();
                nullable = _pk.get_max_money();
                if (_pk.get_play_id().Equals(1))
                {
                    str2 = "1-10名";
                }
                else if (_pk.get_play_id().Equals(2))
                {
                    str2 = "1-10名大小";
                }
                else if (_pk.get_play_id().Equals(3))
                {
                    str2 = "1-10名單雙";
                }
                else if (_pk.get_play_id().Equals(4))
                {
                    str2 = "1-5名龍虎";
                }
                else
                {
                    str2 = _pk.get_play_name();
                }
            }
            else if (lottery_type == 1)
            {
                cz_autosale_cqsc _cqsc = model as cz_autosale_cqsc;
                str = _cqsc.get_u_name();
                nullable = _cqsc.get_max_money();
                if (_cqsc.get_play_id().Equals(1))
                {
                    str2 = "1-5球";
                }
                else if (_cqsc.get_play_id().Equals(2))
                {
                    str2 = "1-5球大小";
                }
                else if (_cqsc.get_play_id().Equals(3))
                {
                    str2 = "1-5球單雙";
                }
                else
                {
                    str2 = _cqsc.get_play_name();
                }
            }
            else if (lottery_type == 6)
            {
                cz_autosale_k8sc _ksc = model as cz_autosale_k8sc;
                str = _ksc.get_u_name();
                nullable = _ksc.get_max_money();
                if (_ksc.get_play_id().Equals(1))
                {
                    str2 = "1-5球";
                }
                else if (_ksc.get_play_id().Equals(2))
                {
                    str2 = "1-5球大小";
                }
                else if (_ksc.get_play_id().Equals(3))
                {
                    str2 = "1-5球單雙";
                }
                else
                {
                    str2 = _ksc.get_play_name();
                }
            }
            else if (lottery_type == 7)
            {
                cz_autosale_pcdd _pcdd = model as cz_autosale_pcdd;
                str = _pcdd.get_u_name();
                nullable = _pcdd.get_max_money();
                if (_pcdd.get_play_id().Equals(0x1155f))
                {
                    str2 = "1-3區大小";
                }
                else if (_pcdd.get_play_id().Equals(0x11560))
                {
                    str2 = "1-3區單雙";
                }
                else
                {
                    str2 = _pcdd.get_play_name();
                }
            }
            else if (lottery_type == 9)
            {
                cz_autosale_xyft5 _xyft = model as cz_autosale_xyft5;
                str = _xyft.get_u_name();
                nullable = _xyft.get_max_money();
                if (_xyft.get_play_id().Equals(1))
                {
                    str2 = "1-10名";
                }
                else if (_xyft.get_play_id().Equals(2))
                {
                    str2 = "1-10名大小";
                }
                else if (_xyft.get_play_id().Equals(3))
                {
                    str2 = "1-10名單雙";
                }
                else if (_xyft.get_play_id().Equals(4))
                {
                    str2 = "1-5名龍虎";
                }
                else
                {
                    str2 = _xyft.get_play_name();
                }
            }
            else if (lottery_type == 8)
            {
                cz_autosale_pkbjl _pkbjl = model as cz_autosale_pkbjl;
                str = _pkbjl.get_u_name();
                nullable = _pkbjl.get_max_money();
                str2 = _pkbjl.get_play_name();
            }
            else if (lottery_type == 10)
            {
                cz_autosale_jscar _jscar = model as cz_autosale_jscar;
                str = _jscar.get_u_name();
                nullable = _jscar.get_max_money();
                if (_jscar.get_play_id().Equals(1))
                {
                    str2 = "1-10名";
                }
                else if (_jscar.get_play_id().Equals(2))
                {
                    str2 = "1-10名大小";
                }
                else if (_jscar.get_play_id().Equals(3))
                {
                    str2 = "1-10名單雙";
                }
                else if (_jscar.get_play_id().Equals(4))
                {
                    str2 = "1-5名龍虎";
                }
                else
                {
                    str2 = _jscar.get_play_name();
                }
            }
            else if (lottery_type == 11)
            {
                cz_autosale_speed5 _speed = model as cz_autosale_speed5;
                str = _speed.get_u_name();
                nullable = _speed.get_max_money();
                if (_speed.get_play_id().Equals(1))
                {
                    str2 = "1-5球";
                }
                else if (_speed.get_play_id().Equals(2))
                {
                    str2 = "1-5球大小";
                }
                else if (_speed.get_play_id().Equals(3))
                {
                    str2 = "1-5球單雙";
                }
                else
                {
                    str2 = _speed.get_play_name();
                }
            }
            else if (lottery_type == 12)
            {
                cz_autosale_jspk10 _jspk = model as cz_autosale_jspk10;
                str = _jspk.get_u_name();
                nullable = _jspk.get_max_money();
                if (_jspk.get_play_id().Equals(1))
                {
                    str2 = "1-10名";
                }
                else if (_jspk.get_play_id().Equals(2))
                {
                    str2 = "1-10名大小";
                }
                else if (_jspk.get_play_id().Equals(3))
                {
                    str2 = "1-10名單雙";
                }
                else if (_jspk.get_play_id().Equals(4))
                {
                    str2 = "1-5名龍虎";
                }
                else
                {
                    str2 = _jspk.get_play_name();
                }
            }
            else if (lottery_type == 13)
            {
                cz_autosale_jscqsc _jscqsc = model as cz_autosale_jscqsc;
                str = _jscqsc.get_u_name();
                nullable = _jscqsc.get_max_money();
                if (_jscqsc.get_play_id().Equals(1))
                {
                    str2 = "1-5球";
                }
                else if (_jscqsc.get_play_id().Equals(2))
                {
                    str2 = "1-5球大小";
                }
                else if (_jscqsc.get_play_id().Equals(3))
                {
                    str2 = "1-5球單雙";
                }
                else
                {
                    str2 = _jscqsc.get_play_name();
                }
            }
            else if (lottery_type == 14)
            {
                cz_autosale_jssfc _jssfc = model as cz_autosale_jssfc;
                str = _jssfc.get_u_name();
                nullable = _jssfc.get_max_money();
                if (_jssfc.get_play_id().Equals(0x51))
                {
                    str2 = "1-8球";
                }
                else if (_jssfc.get_play_id().Equals(0x52))
                {
                    str2 = "1-8球大小";
                }
                else if (_jssfc.get_play_id().Equals(0x53))
                {
                    str2 = "1-8球單雙";
                }
                else if (_jssfc.get_play_id().Equals(0x54))
                {
                    str2 = "1-8球尾數大小";
                }
                else if (_jssfc.get_play_id().Equals(0x55))
                {
                    str2 = "1-8球合數單雙";
                }
                else if (_jssfc.get_play_id().Equals(0x79))
                {
                    str2 = "1-8球方位";
                }
                else if (_jssfc.get_play_id().Equals(0x7a))
                {
                    str2 = "1-8球中發白";
                }
                else
                {
                    str2 = _jssfc.get_play_name();
                }
            }
            else if (lottery_type == 15)
            {
                cz_autosale_jsft2 _jsft = model as cz_autosale_jsft2;
                str = _jsft.get_u_name();
                nullable = _jsft.get_max_money();
                if (_jsft.get_play_id().Equals(1))
                {
                    str2 = "1-10名";
                }
                else if (_jsft.get_play_id().Equals(2))
                {
                    str2 = "1-10名大小";
                }
                else if (_jsft.get_play_id().Equals(3))
                {
                    str2 = "1-10名單雙";
                }
                else if (_jsft.get_play_id().Equals(4))
                {
                    str2 = "1-5名龍虎";
                }
                else
                {
                    str2 = _jsft.get_play_name();
                }
            }
            else if (lottery_type == 0x10)
            {
                cz_autosale_car168 _car = model as cz_autosale_car168;
                str = _car.get_u_name();
                nullable = _car.get_max_money();
                if (_car.get_play_id().Equals(1))
                {
                    str2 = "1-10名";
                }
                else if (_car.get_play_id().Equals(2))
                {
                    str2 = "1-10名大小";
                }
                else if (_car.get_play_id().Equals(3))
                {
                    str2 = "1-10名單雙";
                }
                else if (_car.get_play_id().Equals(4))
                {
                    str2 = "1-5名龍虎";
                }
                else
                {
                    str2 = _car.get_play_name();
                }
            }
            else if (lottery_type == 0x11)
            {
                cz_autosale_ssc168 _ssc = model as cz_autosale_ssc168;
                str = _ssc.get_u_name();
                nullable = _ssc.get_max_money();
                if (_ssc.get_play_id().Equals(1))
                {
                    str2 = "1-5球";
                }
                else if (_ssc.get_play_id().Equals(2))
                {
                    str2 = "1-5球大小";
                }
                else if (_ssc.get_play_id().Equals(3))
                {
                    str2 = "1-5球單雙";
                }
                else
                {
                    str2 = _ssc.get_play_name();
                }
            }
            else if (lottery_type == 0x12)
            {
                cz_autosale_vrcar _vrcar = model as cz_autosale_vrcar;
                str = _vrcar.get_u_name();
                nullable = _vrcar.get_max_money();
                if (_vrcar.get_play_id().Equals(1))
                {
                    str2 = "1-10名";
                }
                else if (_vrcar.get_play_id().Equals(2))
                {
                    str2 = "1-10名大小";
                }
                else if (_vrcar.get_play_id().Equals(3))
                {
                    str2 = "1-10名單雙";
                }
                else if (_vrcar.get_play_id().Equals(4))
                {
                    str2 = "1-5名龍虎";
                }
                else
                {
                    str2 = _vrcar.get_play_name();
                }
            }
            else if (lottery_type == 0x13)
            {
                cz_autosale_vrssc _vrssc = model as cz_autosale_vrssc;
                str = _vrssc.get_u_name();
                nullable = _vrssc.get_max_money();
                if (_vrssc.get_play_id().Equals(1))
                {
                    str2 = "1-5球";
                }
                else if (_vrssc.get_play_id().Equals(2))
                {
                    str2 = "1-5球大小";
                }
                else if (_vrssc.get_play_id().Equals(3))
                {
                    str2 = "1-5球單雙";
                }
                else
                {
                    str2 = _vrssc.get_play_name();
                }
            }
            else if (lottery_type == 20)
            {
                cz_autosale_xyftoa _xyftoa = model as cz_autosale_xyftoa;
                str = _xyftoa.get_u_name();
                nullable = _xyftoa.get_max_money();
                if (_xyftoa.get_play_id().Equals(1))
                {
                    str2 = "1-10名";
                }
                else if (_xyftoa.get_play_id().Equals(2))
                {
                    str2 = "1-10名大小";
                }
                else if (_xyftoa.get_play_id().Equals(3))
                {
                    str2 = "1-10名單雙";
                }
                else if (_xyftoa.get_play_id().Equals(4))
                {
                    str2 = "1-5名龍虎";
                }
                else
                {
                    str2 = _xyftoa.get_play_name();
                }
            }
            else if (lottery_type == 0x15)
            {
                cz_autosale_xyftsg _xyftsg = model as cz_autosale_xyftsg;
                str = _xyftsg.get_u_name();
                nullable = _xyftsg.get_max_money();
                if (_xyftsg.get_play_id().Equals(1))
                {
                    str2 = "1-10名";
                }
                else if (_xyftsg.get_play_id().Equals(2))
                {
                    str2 = "1-10名大小";
                }
                else if (_xyftsg.get_play_id().Equals(3))
                {
                    str2 = "1-10名單雙";
                }
                else if (_xyftsg.get_play_id().Equals(4))
                {
                    str2 = "1-5名龍虎";
                }
                else
                {
                    str2 = _xyftsg.get_play_name();
                }
            }
            else
            {
                if (lottery_type != 0x16)
                {
                    return;
                }
                cz_autosale_happycar _happycar = model as cz_autosale_happycar;
                str = _happycar.get_u_name();
                nullable = _happycar.get_max_money();
                if (_happycar.get_play_id().Equals(1))
                {
                    str2 = "1-10名";
                }
                else if (_happycar.get_play_id().Equals(2))
                {
                    str2 = "1-10名大小";
                }
                else if (_happycar.get_play_id().Equals(3))
                {
                    str2 = "1-10名單雙";
                }
                else if (_happycar.get_play_id().Equals(4))
                {
                    str2 = "1-5名龍虎";
                }
                else
                {
                    str2 = _happycar.get_play_name();
                }
            }
            string str3 = this.get_master_name();
            string str4 = this.get_children_name();
            string str5 = "";
            string str6 = "";
            string note = "";
            int num = lottery_type;
            if ((old_rows == null) || (old_rows.Length <= 0))
            {
                if (isInsert.Equals("1"))
                {
                    note = "新增記錄";
                    str6 = nullable.ToString();
                }
            }
            else
            {
                decimal? nullable2 = new decimal?(Convert.ToDecimal(old_rows[0]["max_money"].ToString()));
                decimal? nullable3 = nullable;
                if (isInsert == "1")
                {
                    decimal? nullable29 = nullable2;
                    decimal? nullable30 = nullable3;
                    if ((nullable29.GetValueOrDefault() == nullable30.GetValueOrDefault()) && (nullable29.HasValue == nullable30.HasValue))
                    {
                        return;
                    }
                    note = "修改記錄";
                    str5 = nullable2.ToString();
                    str6 = nullable3.ToString();
                }
                else
                {
                    note = "刪除記錄";
                    str5 = nullable2.ToString();
                }
            }
            note = note + ":" + str2;
            List<SqlParameter> paramList = new List<SqlParameter>();
            string str8 = this.add_autosale_log(str, num, str3, str4, note, str5, str6, ref paramList);
            CallBLL.cz_autosale_log_bll.executte_sql(str8, paramList.ToArray());
        }

        public void autosale_set_six_log(DataRow[] old_rows, cz_autosale_six new_model, string isInsert, int lottery_type)
        {
            string str = this.get_master_name();
            string str2 = this.get_children_name();
            string str3 = "";
            string str4 = "";
            string note = "";
            string str6 = new_model.get_u_name();
            int num = lottery_type;
            string str7 = new_model.get_play_name();
            "91060,91061,91062,91063,91064,91065".IndexOf(new_model.get_play_id().ToString());
            if ((old_rows == null) || (old_rows.Length <= 0))
            {
                if (isInsert.Equals("1"))
                {
                    note = "新增記錄";
                    str4 = new_model.get_max_money().ToString();
                }
            }
            else
            {
                decimal? nullable = new decimal?(Convert.ToDecimal(old_rows[0]["max_money"].ToString()));
                decimal? nullable2 = new_model.get_max_money();
                if (isInsert == "1")
                {
                    decimal? nullable5 = nullable;
                    decimal? nullable6 = nullable2;
                    if ((nullable5.GetValueOrDefault() == nullable6.GetValueOrDefault()) && (nullable5.HasValue == nullable6.HasValue))
                    {
                        return;
                    }
                    note = "修改記錄";
                    str3 = nullable.ToString();
                    str4 = nullable2.ToString();
                }
                else
                {
                    note = "刪除記錄";
                    str3 = nullable.ToString();
                }
            }
            note = note + ":" + str7;
            List<SqlParameter> paramList = new List<SqlParameter>();
            string str8 = this.add_autosale_log(str6, num, str, str2, note, str3, str4, ref paramList);
            CallBLL.cz_autosale_log_bll.executte_sql(str8, paramList.ToArray());
        }

        public string Banlance(string I_id, string nm, string zodiacList)
        {
            string str = "";
            string[] strArray = nm.Split(new char[] { ',' });
            string str2 = "";
            SqlParameter[] parameterArray = Utils.GetParams(I_id, ref str2, SqlDbType.Int);
            string str3 = string.Format("   select  A.play_id as i_id , B.play_name as item_name  , A.play_name as item_type ,A.put_amount as item_value ,A.odds_id as r_ID from cz_odds_six as A inner join cz_play_six  as B on A.play_id = B.play_id where A.play_id not in (91015,91016,91017,91018,91019,91020,91030,91031,91032,91033,91034,91035,91036,91037,91047,91048,91049,91050,91051,91058,91059) and A.play_id  in ({0})  ", str2);
            DataTable oddsTableByPlayIds = CallBLL.cz_odds_six_bll.GetOddsTableByPlayIds(str3, parameterArray);
            if (oddsTableByPlayIds.Rows.Count != 0)
            {
                DataView view = new DataView(oddsTableByPlayIds);
                foreach (DataRow row in view.ToTable(true, new string[] { "item_name" }).Rows)
                {
                    str = str + this.Gget_zq(row["item_name"].ToString(), oddsTableByPlayIds, strArray, zodiacList);
                }
                if (str != "")
                {
                    str = str.Substring(0, str.Length - 1);
                }
            }
            return str;
        }

        public void bz_group_log(cz_odds_six oldModel, cz_odds_six newModel, string operate_type)
        {
            this.tm_log(oldModel, newModel, operate_type);
        }

        public void bz_number_log(cz_odds_six oldModel, double oldWT, double newWT, string number, string operate_type)
        {
            string str = this.get_master_name();
            string str2 = this.get_children_name();
            string gameNameByID = base.GetGameNameByID(100.ToString());
            string category = oldModel.get_category();
            string str5 = oldModel.get_play_name();
            string str6 = (int.Parse(number) < 10) ? ("0" + int.Parse(number)) : number.ToString();
            double num = oldWT;
            double num2 = newWT;
            int num3 = oldModel.get_odds_id();
            string note = "微調號碼";
            string act = "";
            int num4 = Convert.ToInt32((LSEnums.LogTypeID) 0);
            if (operate_type.Equals("3"))
            {
                note = "手工输入微調值,微調號碼";
            }
            string str9 = "";
            cz_phase_six currentPhase = CallBLL.cz_phase_six_bll.GetCurrentPhase();
            if (currentPhase != null)
            {
                str9 = currentPhase.get_phase().ToString();
            }
            if (num != num2)
            {
                if (num > num2)
                {
                    act = "降賠率";
                }
                else
                {
                    act = "升賠率";
                }
                List<SqlParameter> paramList = new List<SqlParameter>();
                string str10 = this.add_sys_log(str, str2, category, str5, str6, gameNameByID, str9, act, num3, num.ToString(), num2.ToString(), note, num4, 100, ref paramList);
                CallBLL.cz_system_log_bll.executte_sql(str10, paramList.ToArray());
            }
        }

        public void car168_kjj_log(DataTable oldTable, DataTable newTable, string operate_type)
        {
            string str = this.get_master_name();
            string str2 = this.get_children_name();
            string gameNameByID = base.GetGameNameByID(0x10.ToString());
            string category = "";
            string str5 = "";
            string str6 = "";
            string s = "";
            string str8 = "";
            int num = 0;
            string note = "快捷鍵微调" + str6;
            string act = "";
            int num2 = Convert.ToInt32((LSEnums.LogTypeID) 0);
            if (operate_type.Equals("7"))
            {
                note = "快捷鍵手工输入赔率值,微調" + str6;
            }
            string str11 = "";
            DataTable table = CallBLL.cz_phase_car168_bll.IsOpenPhaseByTime(DateTime.Now);
            if (table != null)
            {
                str11 = table.Rows[0]["phase"].ToString();
            }
            List<CommandInfo> list = new List<CommandInfo>();
            int num3 = 0;
            foreach (DataRow row in oldTable.Rows)
            {
                category = row["category"].ToString();
                str5 = row["play_name"].ToString();
                str6 = row["put_amount"].ToString();
                s = row["current_odds"].ToString();
                str8 = newTable.Rows[num3]["current_odds"].ToString();
                num = int.Parse(row["odds_id"].ToString());
                if (s != str8)
                {
                    if (double.Parse(s) > double.Parse(str8))
                    {
                        act = "降賠率";
                    }
                    else
                    {
                        act = "升賠率";
                    }
                    List<SqlParameter> paramList = new List<SqlParameter>();
                    string str12 = this.add_sys_log(str, str2, category, str5, str6, gameNameByID, str11, act, num, s, str8, note, num2, 0x10, ref paramList);
                    CommandInfo item = new CommandInfo {
                        CommandText = str12,
                        Parameters = paramList.ToArray()
                    };
                    list.Add(item);
                }
                num3++;
            }
            if (list.Count > 0)
            {
                CallBLL.cz_system_log_bll.execute_sql_tran(list);
            }
        }

        public void car168_log(cz_odds_car168 oldModel, cz_odds_car168 newModel, string operate_type)
        {
            string str = this.get_master_name();
            string str2 = this.get_children_name();
            string gameNameByID = base.GetGameNameByID(0x10.ToString());
            string category = oldModel.get_category();
            string str5 = oldModel.get_play_name();
            string str6 = oldModel.get_put_amount();
            string s = oldModel.get_current_odds();
            string str8 = newModel.get_current_odds();
            int num = oldModel.get_odds_id();
            string note = "微調" + str6;
            string act = "";
            int num2 = Convert.ToInt32((LSEnums.LogTypeID) 0);
            if (operate_type.Equals("3"))
            {
                note = "手工输入赔率值,微調" + str6;
            }
            string str11 = "";
            DataTable table = CallBLL.cz_phase_car168_bll.IsOpenPhaseByTime(DateTime.Now);
            if (table != null)
            {
                str11 = table.Rows[0]["phase"].ToString();
            }
            if (s != str8)
            {
                if (double.Parse(s) > double.Parse(str8))
                {
                    act = "降賠率";
                }
                else
                {
                    act = "升賠率";
                }
                List<SqlParameter> paramList = new List<SqlParameter>();
                string str12 = this.add_sys_log(str, str2, category, str5, str6, gameNameByID, str11, act, num, s, str8, note, num2, 0x10, ref paramList);
                CallBLL.cz_system_log_bll.executte_sql(str12, paramList.ToArray());
            }
        }

        protected void checkLoginByHandler(int ha)
        {
            string str = base.qq("browserCode");
            bool flag = false;
            if ((HttpContext.Current.Session["lottery_session_img_code_brower"] != null) && !string.IsNullOrEmpty(str))
            {
                if (HttpContext.Current.Session["lottery_session_img_code_brower"].ToString().ToLower() != str.ToLower())
                {
                    flag = true;
                }
                else
                {
                    flag = false;
                }
            }
            if ((HttpContext.Current.Session["user_name"] == null) || flag)
            {
                this.Session.Abandon();
                ReturnResult result = new ReturnResult();
                result.set_success(300);
                result.set_tipinfo("請重新登錄！");
                string s = JsonHandle.ObjectToJson(result);
                HttpContext.Current.Response.ContentType = "application/json";
                HttpContext.Current.Response.Write(s);
                HttpContext.Current.Response.End();
            }
            string str3 = this.get_children_name();
            if (PageBase.IsNeedPopBrower((str3 == "") ? HttpContext.Current.Session["user_name"].ToString() : str3))
            {
                this.Session.Abandon();
                ReturnResult result2 = new ReturnResult();
                result2.set_success(300);
                result2.set_tipinfo("請重新登錄！");
                string str4 = JsonHandle.ObjectToJson(result2);
                HttpContext.Current.Response.ContentType = "application/json";
                HttpContext.Current.Response.Write(str4);
                HttpContext.Current.Response.End();
            }
            string str5 = HttpContext.Current.Session["user_name"].ToString();
            agent_userinfo_session _session = HttpContext.Current.Session[str5 + "lottery_session_user_info"] as agent_userinfo_session;
            if (FileCacheHelper.get_RedisStatOnline().Equals(1) || FileCacheHelper.get_RedisStatOnline().Equals(2))
            {
                bool flag2 = false;
                if ((_session.get_users_child_session() != null) && _session.get_users_child_session().get_is_admin().Equals(1))
                {
                    flag2 = true;
                }
                if (!flag2)
                {
                    if (FileCacheHelper.get_RedisStatOnline().Equals(1))
                    {
                        base.CheckIsOut((str3 == "") ? str5 : str3);
                    }
                    else if (FileCacheHelper.get_RedisStatOnline().Equals(2))
                    {
                        base.CheckIsOutStack((str3 == "") ? str5 : str3);
                    }
                }
            }
            else if (base.IsUserOut((str3 == "") ? HttpContext.Current.Session["user_name"].ToString() : str3))
            {
                this.Session.Abandon();
                ReturnResult result3 = new ReturnResult();
                result3.set_success(100);
                result3.set_tipinfo("");
                string str6 = JsonHandle.ObjectToJson(result3);
                HttpContext.Current.Response.ContentType = "application/json";
                HttpContext.Current.Response.Write(str6);
                HttpContext.Current.Response.End();
            }
        }

        public void CheckZKChildWT(string oddsid)
        {
            if (this.IsChildSync())
            {
                double num25;
                cz_admin_subsystem _subsystem = this.IsChildSystem();
                if ("92572,92588,92589,92590,92591,92592".IndexOf(oddsid) > -1)
                {
                    DataTable oddsTableByOddsIds = CallBLL.cz_odds_six_bll.GetOddsTableByOddsIds(oddsid);
                    DataTable table2 = CallBLL.cz_odds_six_bll.ZK_GetOddsTableByOddsIds(oddsid, _subsystem.get_conn());
                    DataRow[] rowArray = oddsTableByOddsIds.Select(string.Format(" odds_id={0} ", oddsid));
                    DataRow[] rowArray2 = table2.Select(string.Format(" odds_id={0} ", oddsid));
                    double num = double.Parse(double.Parse(rowArray[0]["current_odds"].ToString()).ToString("N6"));
                    double num2 = double.Parse(double.Parse(rowArray2[0]["current_odds"].ToString()).ToString("N6"));
                    if (num > num2)
                    {
                        CallBLL.cz_odds_six_bll.UpdateCurrentOdds(int.Parse(oddsid), num2, "");
                    }
                    DataTable wT = null;
                    DataTable table4 = null;
                    string str6 = oddsid;
                    if (str6 != null)
                    {
                        if (!(str6 == "92572"))
                        {
                            if (str6 == "92588")
                            {
                                wT = CallBLL.cz_wt_6bz_six_bll.GetWT();
                                table4 = CallBLL.cz_wt_6bz_six_bll.ZK_GetWT(_subsystem.get_conn());
                            }
                            else if (str6 == "92589")
                            {
                                wT = CallBLL.cz_wt_7bz_six_bll.GetWT();
                                table4 = CallBLL.cz_wt_7bz_six_bll.ZK_GetWT(_subsystem.get_conn());
                            }
                            else if (str6 == "92590")
                            {
                                wT = CallBLL.cz_wt_8bz_six_bll.GetWT();
                                table4 = CallBLL.cz_wt_8bz_six_bll.ZK_GetWT(_subsystem.get_conn());
                            }
                            else if (str6 == "92591")
                            {
                                wT = CallBLL.cz_wt_9bz_six_bll.GetWT();
                                table4 = CallBLL.cz_wt_9bz_six_bll.ZK_GetWT(_subsystem.get_conn());
                            }
                            else if (str6 == "92592")
                            {
                                wT = CallBLL.cz_wt_10bz_six_bll.GetWT();
                                table4 = CallBLL.cz_wt_10bz_six_bll.ZK_GetWT(_subsystem.get_conn());
                            }
                        }
                        else
                        {
                            wT = CallBLL.cz_wt_5bz_six_bll.GetWT();
                            table4 = CallBLL.cz_wt_5bz_six_bll.ZK_GetWT(_subsystem.get_conn());
                        }
                    }
                    foreach (DataRow row in wT.Rows)
                    {
                        string str7;
                        string str = row["number"].ToString();
                        double num3 = double.Parse(row["wt_value"].ToString());
                        double num4 = double.Parse(table4.Select(string.Format(" number={0} ", str))[0]["wt_value"].ToString());
                        double num5 = double.Parse((num + num3).ToString("N6"));
                        double num6 = double.Parse((num2 + num4).ToString("N6"));
                        if (((num5 > num6) || (num3 > 0.0)) && ((str7 = oddsid) != null))
                        {
                            if (!(str7 == "92572"))
                            {
                                if (str7 == "92588")
                                {
                                    goto Label_034A;
                                }
                                if (str7 == "92589")
                                {
                                    goto Label_0360;
                                }
                                if (str7 == "92590")
                                {
                                    goto Label_0376;
                                }
                                if (str7 == "92591")
                                {
                                    goto Label_038C;
                                }
                                if (str7 == "92592")
                                {
                                    goto Label_03A2;
                                }
                            }
                            else
                            {
                                CallBLL.cz_wt_5bz_six_bll.SetWT(int.Parse(str), num4);
                            }
                        }
                        continue;
                    Label_034A:
                        CallBLL.cz_wt_6bz_six_bll.SetWT(int.Parse(str), num4);
                        continue;
                    Label_0360:
                        CallBLL.cz_wt_7bz_six_bll.SetWT(int.Parse(str), num4);
                        continue;
                    Label_0376:
                        CallBLL.cz_wt_8bz_six_bll.SetWT(int.Parse(str), num4);
                        continue;
                    Label_038C:
                        CallBLL.cz_wt_9bz_six_bll.SetWT(int.Parse(str), num4);
                        continue;
                    Label_03A2:
                        CallBLL.cz_wt_10bz_six_bll.SetWT(int.Parse(str), num4);
                    }
                }
                if (("92285,92286,92287,92288,92289,92575".IndexOf(oddsid) > -1) || ("92638,92639,92640,92641,92642,92643".IndexOf(oddsid) > -1))
                {
                    DataTable table5 = CallBLL.cz_odds_six_bll.GetOddsTableByOddsIds(oddsid);
                    DataTable table6 = CallBLL.cz_odds_six_bll.ZK_GetOddsTableByOddsIds(oddsid, _subsystem.get_conn());
                    DataRow[] rowArray4 = table5.Select(string.Format(" odds_id={0} ", oddsid));
                    DataRow[] rowArray5 = table6.Select(string.Format(" odds_id={0} ", oddsid));
                    double num7 = double.Parse(double.Parse(rowArray4[0]["current_odds"].ToString().Split(new char[] { ',' })[0]).ToString("N6"));
                    double num8 = num7;
                    if (rowArray4[0]["current_odds"].ToString().Split(new char[] { ',' }).Length > 1)
                    {
                        num8 = double.Parse(double.Parse(rowArray4[0]["current_odds"].ToString().Split(new char[] { ',' })[1]).ToString("N6"));
                    }
                    double num9 = double.Parse(double.Parse(rowArray5[0]["current_odds"].ToString().Split(new char[] { ',' })[0]).ToString("N6"));
                    double num10 = num9;
                    if (rowArray5[0]["current_odds"].ToString().Split(new char[] { ',' }).Length > 1)
                    {
                        num10 = double.Parse(double.Parse(rowArray5[0]["current_odds"].ToString().Split(new char[] { ',' })[1]).ToString("N6"));
                    }
                    string str2 = "";
                    if ((num7 > num9) && (num8 > num10))
                    {
                        str2 = num9.ToString() + "," + num10.ToString();
                    }
                    else if ((num7 > num9) && (num8 <= num10))
                    {
                        str2 = num9.ToString() + "," + num8.ToString();
                    }
                    else if ((num7 <= num9) && (num8 > num10))
                    {
                        str2 = num7.ToString() + "," + num10.ToString();
                    }
                    if (!string.IsNullOrEmpty(str2))
                    {
                        CallBLL.cz_odds_six_bll.UpdateCurrentOddsLM(int.Parse(oddsid), str2, "", null);
                    }
                    DataTable table7 = null;
                    DataTable table8 = null;
                    switch (oddsid)
                    {
                        case "92285":
                            table7 = CallBLL.cz_wt_3qz_six_bll.GetWT();
                            table8 = CallBLL.cz_wt_3qz_six_bll.ZK_GetWT(_subsystem.get_conn());
                            break;

                        case "92286":
                            table7 = CallBLL.cz_wt_3z2_six_bll.GetWT();
                            table8 = CallBLL.cz_wt_3z2_six_bll.ZK_GetWT(_subsystem.get_conn());
                            break;

                        case "92287":
                            table7 = CallBLL.cz_wt_2qz_six_bll.GetWT();
                            table8 = CallBLL.cz_wt_2qz_six_bll.ZK_GetWT(_subsystem.get_conn());
                            break;

                        case "92288":
                            table7 = CallBLL.cz_wt_2zt_six_bll.GetWT();
                            table8 = CallBLL.cz_wt_2zt_six_bll.ZK_GetWT(_subsystem.get_conn());
                            break;

                        case "92289":
                            table7 = CallBLL.cz_wt_tc_six_bll.GetWT();
                            table8 = CallBLL.cz_wt_tc_six_bll.ZK_GetWT(_subsystem.get_conn());
                            break;

                        case "92575":
                            table7 = CallBLL.cz_wt_4z1_six_bll.GetWT();
                            table8 = CallBLL.cz_wt_4z1_six_bll.ZK_GetWT(_subsystem.get_conn());
                            break;

                        case "92638":
                            table7 = CallBLL.cz_wt_3qz_b_six_bll.GetWT();
                            table8 = CallBLL.cz_wt_3qz_b_six_bll.ZK_GetWT(_subsystem.get_conn());
                            break;

                        case "92639":
                            table7 = CallBLL.cz_wt_3z2_b_six_bll.GetWT();
                            table8 = CallBLL.cz_wt_3z2_b_six_bll.ZK_GetWT(_subsystem.get_conn());
                            break;

                        case "92640":
                            table7 = CallBLL.cz_wt_2qz_b_six_bll.GetWT();
                            table8 = CallBLL.cz_wt_2qz_b_six_bll.ZK_GetWT(_subsystem.get_conn());
                            break;

                        case "92641":
                            table7 = CallBLL.cz_wt_2zt_b_six_bll.GetWT();
                            table8 = CallBLL.cz_wt_2zt_b_six_bll.ZK_GetWT(_subsystem.get_conn());
                            break;

                        case "92642":
                            table7 = CallBLL.cz_wt_tc_b_six_bll.GetWT();
                            table8 = CallBLL.cz_wt_tc_b_six_bll.ZK_GetWT(_subsystem.get_conn());
                            break;

                        case "92643":
                            table7 = CallBLL.cz_wt_4z1_six_bll.GetWT();
                            table8 = CallBLL.cz_wt_4z1_b_six_bll.ZK_GetWT(_subsystem.get_conn());
                            break;
                    }
                    foreach (DataRow row2 in table7.Rows)
                    {
                        string str3 = row2["number"].ToString();
                        double num11 = double.Parse(row2["wt_value"].ToString());
                        double num12 = double.Parse(table8.Select(string.Format(" number={0} ", str3))[0]["wt_value"].ToString());
                        num25 = num7 + num11;
                        double num13 = double.Parse(num25.ToString("N6"));
                        num25 = num9 + num12;
                        double num14 = double.Parse(num25.ToString("N6"));
                        num25 = num8 + num11;
                        double num15 = double.Parse(num25.ToString("N6"));
                        num25 = num10 + num12;
                        double num16 = double.Parse(num25.ToString("N6"));
                        if (((num13 > num14) || (num15 > num16)) || (num11 > 0.0))
                        {
                            switch (oddsid)
                            {
                                case "92285":
                                    CallBLL.cz_wt_3qz_six_bll.SetWT(int.Parse(str3), num12);
                                    break;

                                case "92286":
                                    CallBLL.cz_wt_3z2_six_bll.SetWT(int.Parse(str3), num12);
                                    break;

                                case "92287":
                                    CallBLL.cz_wt_2qz_six_bll.SetWT(int.Parse(str3), num12);
                                    break;

                                case "92288":
                                    CallBLL.cz_wt_2zt_six_bll.SetWT(int.Parse(str3), num12);
                                    break;

                                case "92289":
                                    CallBLL.cz_wt_tc_six_bll.SetWT(int.Parse(str3), num12);
                                    break;

                                case "92575":
                                    CallBLL.cz_wt_4z1_six_bll.SetWT(int.Parse(str3), num12);
                                    break;

                                case "92638":
                                    CallBLL.cz_wt_3qz_b_six_bll.SetWT(int.Parse(str3), num12);
                                    break;

                                case "92639":
                                    CallBLL.cz_wt_3z2_b_six_bll.SetWT(int.Parse(str3), num12);
                                    break;

                                case "92640":
                                    CallBLL.cz_wt_2qz_b_six_bll.SetWT(int.Parse(str3), num12);
                                    break;

                                case "92641":
                                    CallBLL.cz_wt_2zt_b_six_bll.SetWT(int.Parse(str3), num12);
                                    break;

                                case "92642":
                                    CallBLL.cz_wt_tc_b_six_bll.SetWT(int.Parse(str3), num12);
                                    break;

                                case "92643":
                                    CallBLL.cz_wt_4z1_b_six_bll.SetWT(int.Parse(str3), num12);
                                    break;
                            }
                        }
                    }
                }
                if ("92565,92566,92567,92568,92569,92570,92571,92636,92637".IndexOf(oddsid) > -1)
                {
                    DataTable table9 = CallBLL.cz_odds_six_bll.GetOddsTableByOddsIds(oddsid);
                    DataTable table10 = CallBLL.cz_odds_six_bll.ZK_GetOddsTableByOddsIds(oddsid, _subsystem.get_conn());
                    DataRow[] rowArray7 = table9.Select(string.Format(" odds_id={0} ", oddsid));
                    DataRow[] rowArray8 = table10.Select(string.Format(" odds_id={0} ", oddsid));
                    double num17 = double.Parse(double.Parse(rowArray7[0]["current_odds"].ToString().Split(new char[] { ',' })[0]).ToString("N6"));
                    double num18 = num17;
                    if (rowArray7[0]["current_odds"].ToString().Split(new char[] { ',' }).Length > 1)
                    {
                        num18 = double.Parse(double.Parse(rowArray7[0]["current_odds"].ToString().Split(new char[] { ',' })[1]).ToString("N6"));
                    }
                    double num19 = double.Parse(double.Parse(rowArray8[0]["current_odds"].ToString().Split(new char[] { ',' })[0]).ToString("N6"));
                    double num20 = num19;
                    if (rowArray8[0]["current_odds"].ToString().Split(new char[] { ',' }).Length > 1)
                    {
                        num20 = double.Parse(double.Parse(rowArray8[0]["current_odds"].ToString().Split(new char[] { ',' })[1]).ToString("N6"));
                    }
                    string str4 = "";
                    if ((num17 > num19) && (num18 > num20))
                    {
                        str4 = num19.ToString() + "," + num20.ToString();
                    }
                    else if ((num17 > num19) && (num18 <= num20))
                    {
                        str4 = num19.ToString() + "," + num18.ToString();
                    }
                    else if ((num17 <= num19) && (num18 > num20))
                    {
                        str4 = num17.ToString() + "," + num20.ToString();
                    }
                    if (!string.IsNullOrEmpty(str4))
                    {
                        CallBLL.cz_odds_six_bll.UpdateCurrentOddsLXLWSL(int.Parse(oddsid), str4, null);
                    }
                    DataTable table11 = null;
                    DataTable table12 = null;
                    if ("92565".Equals(oddsid))
                    {
                        table11 = CallBLL.cz_wt_6xyz_six_bll.GetWT();
                        table12 = CallBLL.cz_wt_6xyz_six_bll.ZK_GetWT(_subsystem.get_conn());
                    }
                    else
                    {
                        table11 = CallBLL.cz_wt_sxlwsl_six_bll.GetWT(oddsid);
                        table12 = CallBLL.cz_wt_sxlwsl_six_bll.ZK_GetWT(int.Parse(oddsid), _subsystem.get_conn());
                    }
                    foreach (DataRow row3 in table11.Rows)
                    {
                        string str5 = row3["number"].ToString();
                        double num21 = double.Parse(row3["wt_value"].ToString());
                        double num22 = double.Parse(table12.Select(string.Format(" number={0} ", str5))[0]["wt_value"].ToString());
                        num25 = num17 + num21;
                        double num23 = double.Parse(num25.ToString("N6"));
                        num25 = num19 + num22;
                        double num24 = double.Parse(num25.ToString("N6"));
                        num25 = num18 + num21;
                        double.Parse(num25.ToString("N6"));
                        double.Parse((num20 + num22).ToString("N6"));
                        if ("92565".Equals(oddsid))
                        {
                            if ((num23 > num24) || (num21 > 0.0))
                            {
                                CallBLL.cz_wt_6xyz_six_bll.SetWT(int.Parse(str5), num22);
                            }
                        }
                        else if (((num23 > num24) || (num21 > 0.0)) && !str5.Equals(base.get_YearLianID()))
                        {
                            CallBLL.cz_wt_sxlwsl_six_bll.SetWT(int.Parse(str5), int.Parse(oddsid), num22);
                        }
                    }
                }
            }
        }

        public bool ChildOperateValid(string permissions_name)
        {
            agent_userinfo_session _session = new agent_userinfo_session();
            string str = HttpContext.Current.Session["user_name"].ToString();
            _session = HttpContext.Current.Session[str + "lottery_session_user_info"] as agent_userinfo_session;
            if (!_session.get_u_type().Trim().Equals("zj"))
            {
                return false;
            }
            if ((_session.get_users_child_session() != null) && (_session.get_users_child_session().get_permissions_name().IndexOf(permissions_name) < 0))
            {
                return false;
            }
            return true;
        }

        protected void ChildUserSession(int ha)
        {
            if (CacheHelper.GetCache("user_FileCacheKey" + HttpContext.Current.Session["child_user_name"].ToString()) == null)
            {
                agent_userinfo_session _session = HttpContext.Current.Session[HttpContext.Current.Session["user_name"] + "lottery_session_user_info"] as agent_userinfo_session;
                agent_userinfo_session sessionLogin = this.GetSessionLogin();
                HttpContext.Current.Session[HttpContext.Current.Session["user_name"] + "lottery_session_user_info"] = sessionLogin;
                HttpContext current = HttpContext.Current;
                new ReturnResult();
                if (sessionLogin.get_users_child_session().get_u_psw() != _session.get_users_child_session().get_u_psw())
                {
                    string messageByCache = PageBase.GetMessageByCache("u100017", "MessageHint");
                    this.ShowReturnResultQuit(current, 700, ha, messageByCache, "/Quit.aspx");
                    current.Response.End();
                }
                else if (sessionLogin.get_users_child_session().get_status().Equals(2))
                {
                    string msg = PageBase.GetMessageByCache("u100018", "MessageHint");
                    this.ShowReturnResultQuit(current, 800, ha, msg, "/Quit.aspx");
                    current.Response.End();
                }
                else if (sessionLogin.get_a_state().Equals(2))
                {
                    string str3 = PageBase.GetMessageByCache("u100020", "MessageHint");
                    this.ShowReturnResultQuit(current, 800, ha, str3, "/Quit.aspx");
                    current.Response.End();
                }
                else
                {
                    switch (CallBLL.cz_users_bll.UpUserStatus(HttpContext.Current.Session["user_name"].ToString()))
                    {
                        case 2:
                        {
                            string str4 = PageBase.GetMessageByCache("u100020", "MessageHint");
                            this.ShowReturnResultQuit(current, 800, ha, str4, "/Quit.aspx");
                            current.Response.End();
                            return;
                        }
                        case 1:
                        {
                            string str5 = PageBase.GetMessageByCache("u100021", "MessageHint");
                            HttpContext.Current.Session["user_state"] = 1;
                            this.ShowReturnResultQuit(current, 900, ha, str5, "/Report.aspx");
                            CacheHelper.SetPublicFileCache("user_FileCacheKey" + HttpContext.Current.Session["child_user_name"].ToString(), sessionLogin, PageBase.GetPublicForderPath(ConfigurationManager.AppSettings["UserInfoCachesFileName"]));
                            current.Response.End();
                            return;
                        }
                    }
                    HttpContext.Current.Session["user_state"] = 0;
                    if (sessionLogin.get_users_child_session().get_status().Equals(1))
                    {
                        string str6 = PageBase.GetMessageByCache("u100019", "MessageHint");
                        HttpContext.Current.Session["user_state"] = 1;
                        this.ShowReturnResultQuit(current, 900, ha, str6, "/Report.aspx");
                        CacheHelper.SetPublicFileCache("user_FileCacheKey" + HttpContext.Current.Session["child_user_name"].ToString(), sessionLogin, PageBase.GetPublicForderPath(ConfigurationManager.AppSettings["UserInfoCachesFileName"]));
                        current.Response.End();
                    }
                    else if (sessionLogin.get_a_state().Equals(1))
                    {
                        string str7 = PageBase.GetMessageByCache("u100021", "MessageHint");
                        HttpContext.Current.Session["user_state"] = 1;
                        this.ShowReturnResultQuit(current, 900, ha, str7, "/Report.aspx");
                        CacheHelper.SetPublicFileCache("user_FileCacheKey" + HttpContext.Current.Session["child_user_name"].ToString(), sessionLogin, PageBase.GetPublicForderPath(ConfigurationManager.AppSettings["UserInfoCachesFileName"]));
                        current.Response.End();
                    }
                }
            }
        }

        protected string count_ds(string zqh)
        {
            if (int.Parse(zqh) == 0x31)
            {
                return "和";
            }
            if ((int.Parse(zqh) % 2) == 0)
            {
                return "雙";
            }
            return "單";
        }

        protected string count_dx(string zqh)
        {
            string str = "";
            if (int.Parse(zqh) == 0x31)
            {
                return "和";
            }
            if (int.Parse(zqh) < 0x19)
            {
                return "小";
            }
            if ((int.Parse(zqh) > 0x18) && (int.Parse(zqh) < 0x31))
            {
                str = "大";
            }
            return str;
        }

        protected string count_hsds(string zqh)
        {
            int num = int.Parse(zqh) / 10;
            int num2 = int.Parse(zqh) % 10;
            int num3 = num + num2;
            if (int.Parse(zqh) == 0x31)
            {
                return "和";
            }
            if ((num3 % 2) == 0)
            {
                return "雙";
            }
            return "單";
        }

        protected string count_longhu(string zqh, int I_type)
        {
            string str = "";
            string[] strArray = zqh.Split(new char[] { ',' });
            int num = int.Parse(strArray[0]);
            int num2 = int.Parse(strArray[1]);
            if (num > num2)
            {
                switch (I_type)
                {
                    case 1:
                        return "龍";

                    case 2:
                        return "天";

                    case 3:
                        return "莊";

                    case 4:
                        return "雷";

                    case 5:
                        return "神";

                    case 6:
                        return "黑";
                }
                return str;
            }
            if (num < num2)
            {
                switch (I_type)
                {
                    case 1:
                        return "虎";

                    case 2:
                        return "地";

                    case 3:
                        return "閑";

                    case 4:
                        return "電";

                    case 5:
                        return "奇";

                    case 6:
                        return "紅";
                }
            }
            return str;
        }

        protected string count_qmds(string nm)
        {
            int num = 0;
            int num2 = 0;
            string[] strArray = nm.Split(new char[] { ',' });
            for (int i = 0; i < strArray.Length; i++)
            {
                if ((int.Parse(strArray[i]) % 2) != 0)
                {
                    num++;
                }
                else
                {
                    num2++;
                }
            }
            return string.Format("單{0}、雙{1}", num, num2);
        }

        protected string count_qmdx(string nm)
        {
            int num = 0;
            int num2 = 0;
            string[] strArray = nm.Split(new char[] { ',' });
            for (int i = 0; i < strArray.Length; i++)
            {
                if (int.Parse(strArray[i]) >= 0x19)
                {
                    num++;
                }
                else
                {
                    num2++;
                }
            }
            return string.Format("大{0}、小{1}", num, num2);
        }

        protected string count_sb(string zqh)
        {
            switch (int.Parse(zqh))
            {
                case 1:
                case 2:
                case 7:
                case 8:
                case 12:
                case 13:
                case 0x12:
                case 0x13:
                case 0x17:
                case 0x18:
                case 0x1d:
                case 30:
                case 0x22:
                case 0x23:
                case 40:
                case 0x2d:
                case 0x2e:
                    return "紅波";

                case 3:
                case 4:
                case 9:
                case 10:
                case 14:
                case 15:
                case 20:
                case 0x19:
                case 0x1a:
                case 0x1f:
                case 0x24:
                case 0x25:
                case 0x29:
                case 0x2a:
                case 0x2f:
                case 0x30:
                    return "藍波";

                case 5:
                case 6:
                case 11:
                case 0x10:
                case 0x11:
                case 0x15:
                case 0x16:
                case 0x1b:
                case 0x1c:
                case 0x20:
                case 0x21:
                case 0x26:
                case 0x27:
                case 0x2b:
                case 0x2c:
                case 0x31:
                    return "綠波";
            }
            return "";
        }

        protected string count_ws(string zqh)
        {
            int num = int.Parse(zqh) % 10;
            return num.ToString();
        }

        protected string count_wsdx(string zqh)
        {
            int num = int.Parse(zqh) % 10;
            if (int.Parse(zqh) == 0x31)
            {
                return "和";
            }
            if (num < 5)
            {
                return "小";
            }
            return "大";
        }

        protected string count_wx(string sn)
        {
            Dictionary<string, string> dictionary = base.PlayWXList_six();
            if (int.Parse(sn) < 10)
            {
                sn = "0" + int.Parse(sn);
            }
            foreach (KeyValuePair<string, string> pair in dictionary)
            {
                if (pair.Value.IndexOf(sn) > -1)
                {
                    return pair.Key;
                }
            }
            return "";
        }

        protected string count_zhds(string[] zqh)
        {
            int num = 0;
            foreach (string str2 in zqh)
            {
                num += int.Parse(str2);
            }
            if ((num % 2) == 0)
            {
                return "雙";
            }
            return "單";
        }

        protected string count_zhdx(string[] zqh)
        {
            int num = 0;
            foreach (string str2 in zqh)
            {
                num += int.Parse(str2);
            }
            if (num < 0xaf)
            {
                return "小";
            }
            return "大";
        }

        public void cqsc_kjj_log(DataTable oldTable, DataTable newTable, string operate_type)
        {
            string str = this.get_master_name();
            string str2 = this.get_children_name();
            string gameNameByID = base.GetGameNameByID(1.ToString());
            string category = "";
            string str5 = "";
            string str6 = "";
            string s = "";
            string str8 = "";
            int num = 0;
            string note = "快捷鍵微调" + str6;
            string act = "";
            int num2 = Convert.ToInt32((LSEnums.LogTypeID) 0);
            if (operate_type.Equals("7"))
            {
                note = "快捷鍵手工输入赔率值,微調" + str6;
            }
            string str11 = "";
            DataTable table = CallBLL.cz_phase_cqsc_bll.IsOpenPhaseByTime(DateTime.Now);
            if (table != null)
            {
                str11 = table.Rows[0]["phase"].ToString();
            }
            List<CommandInfo> list = new List<CommandInfo>();
            int num3 = 0;
            foreach (DataRow row in oldTable.Rows)
            {
                category = row["category"].ToString();
                str5 = row["play_name"].ToString();
                str6 = row["put_amount"].ToString();
                s = row["current_odds"].ToString();
                str8 = newTable.Rows[num3]["current_odds"].ToString();
                num = int.Parse(row["odds_id"].ToString());
                if (s != str8)
                {
                    if (double.Parse(s) > double.Parse(str8))
                    {
                        act = "降賠率";
                    }
                    else
                    {
                        act = "升賠率";
                    }
                    List<SqlParameter> paramList = new List<SqlParameter>();
                    string str12 = this.add_sys_log(str, str2, category, str5, str6, gameNameByID, str11, act, num, s, str8, note, num2, 1, ref paramList);
                    CommandInfo item = new CommandInfo {
                        CommandText = str12,
                        Parameters = paramList.ToArray()
                    };
                    list.Add(item);
                }
                num3++;
            }
            if (list.Count > 0)
            {
                CallBLL.cz_system_log_bll.execute_sql_tran(list);
            }
        }

        public void cqsc_log(cz_odds_cqsc oldModel, cz_odds_cqsc newModel, string operate_type)
        {
            string str = this.get_master_name();
            string str2 = this.get_children_name();
            string gameNameByID = base.GetGameNameByID(1.ToString());
            string category = oldModel.get_category();
            string str5 = oldModel.get_play_name();
            string str6 = oldModel.get_put_amount();
            string s = oldModel.get_current_odds();
            string str8 = newModel.get_current_odds();
            int num = oldModel.get_odds_id();
            string note = "微調" + str6;
            string act = "";
            int num2 = Convert.ToInt32((LSEnums.LogTypeID) 0);
            if (operate_type.Equals("3"))
            {
                note = "手工输入赔率值,微調" + str6;
            }
            string str11 = "";
            DataTable table = CallBLL.cz_phase_cqsc_bll.IsOpenPhaseByTime(DateTime.Now);
            if (table != null)
            {
                str11 = table.Rows[0]["phase"].ToString();
            }
            if (s != str8)
            {
                if (double.Parse(s) > double.Parse(str8))
                {
                    act = "降賠率";
                }
                else
                {
                    act = "升賠率";
                }
                List<SqlParameter> paramList = new List<SqlParameter>();
                string str12 = this.add_sys_log(str, str2, category, str5, str6, gameNameByID, str11, act, num, s, str8, note, num2, 1, ref paramList);
                CallBLL.cz_system_log_bll.executte_sql(str12, paramList.ToArray());
            }
        }

        public void czsd_log(DataTable oldDT, DataTable newDT)
        {
            string str = this.get_master_name();
            string str2 = this.get_children_name();
            string str3 = "";
            string str4 = "";
            string note = "彩種配置";
            string act = "彩種配置";
            int num = 0;
            List<string> list = new List<string>();
            List<string> list2 = new List<string>();
            foreach (DataRow row in oldDT.Rows)
            {
                string item = row["lottery_name"].ToString();
                string str8 = row["id"].ToString();
                string str9 = row["night_flag"].ToString();
                int num2 = 5;
                if (str8 == num2.ToString())
                {
                    if (str9 == "1")
                    {
                        item = item + ",午夜盘开启中";
                    }
                    else
                    {
                        item = item + ",午夜盘关闭中";
                    }
                }
                int num3 = 6;
                if (str8 == num3.ToString())
                {
                    if (str9 == "1")
                    {
                        item = item + ",午夜盘开启中";
                    }
                    else
                    {
                        item = item + ",午夜盘关闭中";
                    }
                }
                int num4 = 7;
                if (str8 == num4.ToString())
                {
                    if (str9 == "1")
                    {
                        item = item + ",午夜盘开启中";
                    }
                    else
                    {
                        item = item + ",午夜盘关闭中";
                    }
                }
                list.Add(item);
            }
            str3 = string.Join(",<br />", list.ToArray());
            foreach (DataRow row2 in newDT.Rows)
            {
                string str10 = row2["lottery_name"].ToString();
                string str11 = row2["id"].ToString();
                string str12 = row2["night_flag"].ToString();
                int num5 = 5;
                if (str11 == num5.ToString())
                {
                    if (str12 == "1")
                    {
                        str10 = str10 + ",午夜盘开启中";
                    }
                    else
                    {
                        str10 = str10 + ",午夜盘关闭中";
                    }
                }
                int num6 = 6;
                if (str11 == num6.ToString())
                {
                    if (str12 == "1")
                    {
                        str10 = str10 + ",午夜盘开启中";
                    }
                    else
                    {
                        str10 = str10 + ",午夜盘关闭中";
                    }
                }
                int num7 = 7;
                if (str11 == num7.ToString())
                {
                    if (str12 == "1")
                    {
                        str10 = str10 + ",午夜盘开启中";
                    }
                    else
                    {
                        str10 = str10 + ",午夜盘关闭中";
                    }
                }
                list2.Add(str10);
            }
            str4 = string.Join(",<br />", list2.ToArray());
            List<SqlParameter> paramList = new List<SqlParameter>();
            string str13 = this.add_sys_set_log(str, str2, act, str3, str4, note, num, ref paramList);
            CallBLL.cz_system_set_log_bll.executte_sql(str13, paramList.ToArray());
        }

        public void Del_FgsOddsWT_Lock(string lottery_type, string fgs_name, string odds_id)
        {
            HttpContext.Current.Application.Lock();
            HttpContext.Current.Application[fgs_name + "_FgsOddsWT_" + lottery_type + "_" + odds_id] = null;
            HttpContext.Current.Application.UnLock();
        }

        public bool DeleteCreditLock(string u_name)
        {
            if (FileCacheHelper.get_IsRedisCreditLock().Equals("0"))
            {
                CallBLL.cz_credit_lock_bll.Delete(u_name);
                return true;
            }
            return (FileCacheHelper.get_IsRedisCreditLock().Equals("1") || (FileCacheHelper.get_IsRedisCreditLock().Equals("2") || true));
        }

        public void En_User_Lock(string fgs_name)
        {
            if (base.Application["lock_" + fgs_name] != null)
            {
                int num = (int) Math.Floor(Utils.DateDiff(Convert.ToDateTime(base.Application["lock_" + fgs_name].ToString()), DateTime.Now).TotalMilliseconds);
                if (num <= 0x7d0)
                {
                    base.Response.Write(this.GetAlert("系統繁忙,請稍后提交！"));
                    base.Response.End();
                }
            }
            base.Application.Lock();
            base.Application["lock_" + fgs_name] = DateTime.Now.ToString();
            base.Application.UnLock();
        }

        public void fgs_opt_kc_log(DataTable old_dt, DataTable new_dt, string operate_type, string fgsName, string odds_ids, int lottery_type)
        {
            if ((old_dt != null) && (new_dt != null))
            {
                DataTable dt = null;
                DataTable table2 = null;
                string str = "";
                if (lottery_type == 0)
                {
                    dt = CallBLL.cz_odds_kl10_bll.GetOddsInfoByID(odds_ids);
                    table2 = CallBLL.cz_phase_kl10_bll.IsOpenPhaseByTime(DateTime.Now);
                    if (table2 != null)
                    {
                        str = table2.Rows[0]["phase"].ToString();
                    }
                }
                else if (lottery_type == 5)
                {
                    dt = CallBLL.cz_odds_kl8_bll.GetOddsInfoByID(odds_ids);
                    table2 = CallBLL.cz_phase_kl8_bll.IsOpenPhaseByTime(DateTime.Now);
                    if (table2 != null)
                    {
                        str = table2.Rows[0]["phase"].ToString();
                    }
                }
                else if (lottery_type == 1)
                {
                    dt = CallBLL.cz_odds_cqsc_bll.GetOddsInfoByID(odds_ids);
                    table2 = CallBLL.cz_phase_cqsc_bll.IsOpenPhaseByTime(DateTime.Now);
                    if (table2 != null)
                    {
                        str = table2.Rows[0]["phase"].ToString();
                    }
                }
                else if (lottery_type == 4)
                {
                    dt = CallBLL.cz_odds_jsk3_bll.GetOddsInfoByID(odds_ids);
                    table2 = CallBLL.cz_phase_jsk3_bll.IsOpenPhaseByTime(DateTime.Now);
                    if (table2 != null)
                    {
                        str = table2.Rows[0]["phase"].ToString();
                    }
                }
                else if (lottery_type == 2)
                {
                    dt = CallBLL.cz_odds_pk10_bll.GetOddsInfoByID(odds_ids);
                    table2 = CallBLL.cz_phase_pk10_bll.IsOpenPhaseByTime(DateTime.Now);
                    if (table2 != null)
                    {
                        str = table2.Rows[0]["phase"].ToString();
                    }
                }
                else if (lottery_type == 3)
                {
                    dt = CallBLL.cz_odds_xync_bll.GetOddsInfoByID(odds_ids);
                    table2 = CallBLL.cz_phase_xync_bll.IsOpenPhaseByTime(DateTime.Now);
                    if (table2 != null)
                    {
                        str = table2.Rows[0]["phase"].ToString();
                    }
                }
                else if (lottery_type == 6)
                {
                    dt = CallBLL.cz_odds_k8sc_bll.GetOddsInfoByID(odds_ids);
                    table2 = CallBLL.cz_phase_k8sc_bll.IsOpenPhaseByTime(DateTime.Now);
                    if (table2 != null)
                    {
                        str = table2.Rows[0]["phase"].ToString();
                    }
                }
                else if (lottery_type == 7)
                {
                    dt = CallBLL.cz_odds_pcdd_bll.GetOddsInfoByID(odds_ids);
                    table2 = CallBLL.cz_phase_pcdd_bll.IsOpenPhaseByTime(DateTime.Now);
                    if (table2 != null)
                    {
                        str = table2.Rows[0]["phase"].ToString();
                    }
                }
                else if (lottery_type == 9)
                {
                    dt = CallBLL.cz_odds_xyft5_bll.GetOddsInfoByID(odds_ids);
                    table2 = CallBLL.cz_phase_xyft5_bll.IsOpenPhaseByTime(DateTime.Now);
                    if (table2 != null)
                    {
                        str = table2.Rows[0]["phase"].ToString();
                    }
                }
                else if (lottery_type == 8)
                {
                    dt = CallBLL.cz_odds_pkbjl_bll.GetOddsInfoByID(odds_ids);
                    table2 = CallBLL.cz_phase_pkbjl_bll.IsOpenPhaseByTime(DateTime.Now);
                    if (table2 != null)
                    {
                        str = table2.Rows[0]["phase"].ToString();
                    }
                }
                else if (lottery_type == 10)
                {
                    dt = CallBLL.cz_odds_jscar_bll.GetOddsInfoByID(odds_ids);
                    table2 = CallBLL.cz_phase_jscar_bll.IsOpenPhaseByTime(DateTime.Now);
                    if (table2 != null)
                    {
                        str = table2.Rows[0]["phase"].ToString();
                    }
                }
                else if (lottery_type == 11)
                {
                    dt = CallBLL.cz_odds_speed5_bll.GetOddsInfoByID(odds_ids);
                    table2 = CallBLL.cz_phase_speed5_bll.IsOpenPhaseByTime(DateTime.Now);
                    if (table2 != null)
                    {
                        str = table2.Rows[0]["phase"].ToString();
                    }
                }
                else if (lottery_type == 12)
                {
                    dt = CallBLL.cz_odds_jspk10_bll.GetOddsInfoByID(odds_ids);
                    table2 = CallBLL.cz_phase_jspk10_bll.IsOpenPhaseByTime(DateTime.Now);
                    if (table2 != null)
                    {
                        str = table2.Rows[0]["phase"].ToString();
                    }
                }
                else if (lottery_type == 13)
                {
                    dt = CallBLL.cz_odds_jscqsc_bll.GetOddsInfoByID(odds_ids);
                    table2 = CallBLL.cz_phase_jscqsc_bll.IsOpenPhaseByTime(DateTime.Now);
                    if (table2 != null)
                    {
                        str = table2.Rows[0]["phase"].ToString();
                    }
                }
                else if (lottery_type == 14)
                {
                    dt = CallBLL.cz_odds_jssfc_bll.GetOddsInfoByID(odds_ids);
                    table2 = CallBLL.cz_phase_jssfc_bll.IsOpenPhaseByTime(DateTime.Now);
                    if (table2 != null)
                    {
                        str = table2.Rows[0]["phase"].ToString();
                    }
                }
                else if (lottery_type == 15)
                {
                    dt = CallBLL.cz_odds_jsft2_bll.GetOddsInfoByID(odds_ids);
                    table2 = CallBLL.cz_phase_jsft2_bll.IsOpenPhaseByTime(DateTime.Now);
                    if (table2 != null)
                    {
                        str = table2.Rows[0]["phase"].ToString();
                    }
                }
                else if (lottery_type == 0x10)
                {
                    dt = CallBLL.cz_odds_car168_bll.GetOddsInfoByID(odds_ids);
                    table2 = CallBLL.cz_phase_car168_bll.IsOpenPhaseByTime(DateTime.Now);
                    if (table2 != null)
                    {
                        str = table2.Rows[0]["phase"].ToString();
                    }
                }
                else if (lottery_type == 0x11)
                {
                    dt = CallBLL.cz_odds_ssc168_bll.GetOddsInfoByID(odds_ids);
                    table2 = CallBLL.cz_phase_ssc168_bll.IsOpenPhaseByTime(DateTime.Now);
                    if (table2 != null)
                    {
                        str = table2.Rows[0]["phase"].ToString();
                    }
                }
                else if (lottery_type == 0x12)
                {
                    dt = CallBLL.cz_odds_vrcar_bll.GetOddsInfoByID(odds_ids);
                    table2 = CallBLL.cz_phase_vrcar_bll.IsOpenPhaseByTime(DateTime.Now);
                    if (table2 != null)
                    {
                        str = table2.Rows[0]["phase"].ToString();
                    }
                }
                else if (lottery_type == 0x13)
                {
                    dt = CallBLL.cz_odds_vrssc_bll.GetOddsInfoByID(odds_ids);
                    table2 = CallBLL.cz_phase_vrssc_bll.IsOpenPhaseByTime(DateTime.Now);
                    if (table2 != null)
                    {
                        str = table2.Rows[0]["phase"].ToString();
                    }
                }
                else if (lottery_type == 20)
                {
                    dt = CallBLL.cz_odds_xyftoa_bll.GetOddsInfoByID(odds_ids);
                    table2 = CallBLL.cz_phase_xyftoa_bll.IsOpenPhaseByTime(DateTime.Now);
                    if (table2 != null)
                    {
                        str = table2.Rows[0]["phase"].ToString();
                    }
                }
                else if (lottery_type == 0x15)
                {
                    dt = CallBLL.cz_odds_xyftsg_bll.GetOddsInfoByID(odds_ids);
                    table2 = CallBLL.cz_phase_xyftsg_bll.IsOpenPhaseByTime(DateTime.Now);
                    if (table2 != null)
                    {
                        str = table2.Rows[0]["phase"].ToString();
                    }
                }
                else if (lottery_type == 0x16)
                {
                    dt = CallBLL.cz_odds_happycar_bll.GetOddsInfoByID(odds_ids);
                    table2 = CallBLL.cz_phase_happycar_bll.IsOpenPhaseByTime(DateTime.Now);
                    if (table2 != null)
                    {
                        str = table2.Rows[0]["phase"].ToString();
                    }
                }
                string str2 = this.get_master_name();
                string str3 = this.get_children_name();
                string gameNameByID = base.GetGameNameByID(lottery_type.ToString());
                int num = Convert.ToInt32((LSEnums.LogTypeID) 0);
                string cotegory = "";
                string str6 = "";
                string str7 = "";
                List<CommandInfo> list = new List<CommandInfo>();
                int num2 = 0;
                foreach (DataRow row in old_dt.Rows)
                {
                    string s = row["wt_value"].ToString();
                    string str9 = new_dt.Rows[num2]["wt_value"].ToString();
                    int num3 = int.Parse(row["odds_id"].ToString());
                    this.get_playinfo(dt, num3.ToString(), ref cotegory, ref str6, ref str7);
                    string note = "分公司微調";
                    string act = "";
                    if (operate_type.Equals("3"))
                    {
                        note = "手工输入赔率值,微調" + str7;
                    }
                    else if (operate_type.Equals("5") || operate_type.Equals("6"))
                    {
                        note = "快捷欄微調" + str7;
                    }
                    else if (operate_type.Equals("7"))
                    {
                        note = "快捷欄手工输入赔率值,微調" + str7;
                    }
                    if (s != str9)
                    {
                        if (double.Parse(s) > double.Parse(str9))
                        {
                            act = "降賠率";
                        }
                        else
                        {
                            act = "升賠率";
                        }
                        List<SqlParameter> paramList = new List<SqlParameter>();
                        string str12 = this.add_fgs_log(str2, str3, cotegory, str6, str7, gameNameByID, str, act, num3, s, str9, note, num, lottery_type, ref paramList);
                        CommandInfo item = new CommandInfo {
                            CommandText = str12,
                            Parameters = paramList.ToArray()
                        };
                        list.Add(item);
                    }
                    num2++;
                }
                if (list.Count > 0)
                {
                    CallBLL.cz_fgs_opt_log_bll.execute_sql_tran(list);
                }
            }
        }

        public void fgs_opt_six_log(DataTable old_dt, DataTable new_dt, string operate_type, string fgsName, string odds_ids)
        {
            if ((old_dt != null) && (new_dt != null))
            {
                DataTable oddsTableByOddsIds = CallBLL.cz_odds_six_bll.GetOddsTableByOddsIds(odds_ids);
                string str = this.get_master_name();
                string str2 = this.get_children_name();
                string gameNameByID = base.GetGameNameByID(100.ToString());
                int num = Convert.ToInt32((LSEnums.LogTypeID) 0);
                string cotegory = "";
                string str5 = "";
                string str6 = "";
                string str7 = "";
                cz_phase_six currentPhase = CallBLL.cz_phase_six_bll.GetCurrentPhase();
                if (currentPhase != null)
                {
                    str7 = currentPhase.get_phase().ToString();
                }
                List<CommandInfo> list = new List<CommandInfo>();
                int num2 = 0;
                foreach (DataRow row in old_dt.Rows)
                {
                    string s = row["wt_value"].ToString();
                    string str9 = row["wt_value2"].ToString();
                    string str10 = new_dt.Rows[num2]["wt_value"].ToString();
                    string str11 = new_dt.Rows[num2]["wt_value2"].ToString();
                    int num3 = int.Parse(row["odds_id"].ToString());
                    this.get_playinfo(oddsTableByOddsIds, num3.ToString(), ref cotegory, ref str5, ref str6);
                    string note = "分公司微調";
                    string act = "";
                    if (operate_type.Equals("3"))
                    {
                        note = "手工输入赔率值,微調" + str6;
                    }
                    else if (operate_type.Equals("5") || operate_type.Equals("6"))
                    {
                        note = "快捷欄微調" + str6;
                    }
                    else if (operate_type.Equals("7"))
                    {
                        note = "快捷欄手工输入赔率值,微調" + str6;
                    }
                    if (s != str10)
                    {
                        if (double.Parse(s) > double.Parse(str10))
                        {
                            act = "降賠率";
                        }
                        else
                        {
                            act = "升賠率";
                        }
                        if ((num3.Equals(0x16996) || num3.Equals(0x16997)) || (num3.Equals(0x16998) || num3.Equals(0x169dc)))
                        {
                            s = "不連" + base.get_YearLian() + s;
                            str10 = "不連" + base.get_YearLian() + str10;
                        }
                        if ((num3.Equals(0x16999) || num3.Equals(0x1699a)) || (num3.Equals(0x1699b) || num3.Equals(0x169dd)))
                        {
                            s = "不連0尾" + s;
                            str10 = "不連0尾" + str10;
                        }
                        if (num3.Equals(0x1687e))
                        {
                            s = "中二" + s;
                            str10 = "中二" + str10;
                        }
                        if (num3.Equals(0x16880))
                        {
                            s = "中特" + s;
                            str10 = "中特" + str10;
                        }
                        List<SqlParameter> paramList = new List<SqlParameter>();
                        string str14 = this.add_fgs_log(str, str2, cotegory, str5, str6, gameNameByID, str7, act, num3, s, str10, note, num, 100, ref paramList);
                        CommandInfo item = new CommandInfo {
                            CommandText = str14,
                            Parameters = paramList.ToArray()
                        };
                        list.Add(item);
                    }
                    else if (str9 != str11)
                    {
                        if (double.Parse(str9) > double.Parse(str11))
                        {
                            act = "降賠率";
                        }
                        else
                        {
                            act = "升賠率";
                        }
                        if ((num3.Equals(0x16996) || num3.Equals(0x16997)) || (num3.Equals(0x16998) || num3.Equals(0x169dc)))
                        {
                            str9 = "連" + base.get_YearLian() + str9;
                            str11 = "連" + base.get_YearLian() + str11;
                        }
                        if ((num3.Equals(0x16999) || num3.Equals(0x1699a)) || (num3.Equals(0x1699b) || num3.Equals(0x169dd)))
                        {
                            str9 = "連0尾" + str9;
                            str11 = "連0尾" + str11;
                        }
                        if (num3.Equals(0x1687e))
                        {
                            str9 = "中三" + str9;
                            str11 = "中三" + str11;
                        }
                        if (num3.Equals(0x16880))
                        {
                            str9 = "中二" + str9;
                            str11 = "中二" + str11;
                        }
                        List<SqlParameter> list3 = new List<SqlParameter>();
                        string str15 = this.add_fgs_log(str, str2, cotegory, str5, str6, gameNameByID, str7, act, num3, str9, str11, note, num, 100, ref list3);
                        CommandInfo info2 = new CommandInfo {
                            CommandText = str15,
                            Parameters = list3.ToArray()
                        };
                        list.Add(info2);
                    }
                    num2++;
                }
                if (list.Count > 0)
                {
                    CallBLL.cz_fgs_opt_log_bll.execute_sql_tran(list);
                }
            }
        }

        public void fgs_reset_zero_wt_log(int lottery_id)
        {
            string str = this.get_master_name();
            string str2 = this.get_children_name();
            string gameNameByID = base.GetGameNameByID(lottery_id.ToString());
            int num = Convert.ToInt32((LSEnums.LogTypeID) 0);
            string category = "";
            string str5 = "";
            string str6 = "";
            string str7 = "";
            string act = "微调重置零";
            int num2 = 0;
            string str9 = "";
            string str10 = "";
            string note = string.Format("【{0}】微调全部重置零", gameNameByID);
            List<SqlParameter> paramList = new List<SqlParameter>();
            string str12 = this.add_fgs_log(str, str2, category, str5, str6, gameNameByID, str7, act, num2, str9, str10, note, num, lottery_id, ref paramList);
            CallBLL.cz_fgs_opt_log_bll.executte_sql(str12, paramList.ToArray());
        }

        public DataTable FGS_WTChche(int? lotteryID)
        {
            if (lotteryID.HasValue)
            {
                string str = HttpContext.Current.Session["user_name"].ToString();
                agent_userinfo_session _session = HttpContext.Current.Session[str + "lottery_session_user_info"] as agent_userinfo_session;
                int valueOrDefault = lotteryID.GetValueOrDefault();
                if (lotteryID.HasValue)
                {
                    switch (valueOrDefault)
                    {
                        case 0:
                        {
                            DataTable cache = CacheHelper.GetCache("fgs_wt_FileCacheKey" + HttpContext.Current.Session["user_name"].ToString() + 0) as DataTable;
                            if (cache != null)
                            {
                                return cache;
                            }
                            if (!_session.get_kc_op_odds().Equals(1))
                            {
                                goto Label_10E4;
                            }
                            DataTable oddsWTs = CallBLL.cz_odds_wt_kl10_bll.GetOddsWTs(_session.get_fgs_name());
                            CacheHelper.SetPublicFileCache("fgs_wt_FileCacheKey" + HttpContext.Current.Session["user_name"].ToString() + 0, oddsWTs, PageBase.GetPublicForderPath(FileCacheHelper.get_FGS_WTCaches_KL10()));
                            return oddsWTs;
                        }
                        case 1:
                        {
                            DataTable table5 = CacheHelper.GetCache("fgs_wt_FileCacheKey" + HttpContext.Current.Session["user_name"].ToString() + 1) as DataTable;
                            if (table5 != null)
                            {
                                return table5;
                            }
                            if (!_session.get_kc_op_odds().Equals(1))
                            {
                                goto Label_10E4;
                            }
                            DataTable table6 = CallBLL.cz_odds_wt_cqsc_bll.GetOddsWTs(_session.get_fgs_name());
                            CacheHelper.SetPublicFileCache("fgs_wt_FileCacheKey" + HttpContext.Current.Session["user_name"].ToString() + 1, table6, PageBase.GetPublicForderPath(FileCacheHelper.get_FGS_WTCaches_CQSC()));
                            return table6;
                        }
                        case 2:
                        {
                            DataTable table7 = CacheHelper.GetCache("fgs_wt_FileCacheKey" + HttpContext.Current.Session["user_name"].ToString() + 2) as DataTable;
                            if (table7 != null)
                            {
                                return table7;
                            }
                            if (!_session.get_kc_op_odds().Equals(1))
                            {
                                goto Label_10E4;
                            }
                            DataTable table8 = CallBLL.cz_odds_wt_pk10_bll.GetOddsWTs(_session.get_fgs_name());
                            CacheHelper.SetPublicFileCache("fgs_wt_FileCacheKey" + HttpContext.Current.Session["user_name"].ToString() + 2, table8, PageBase.GetPublicForderPath(FileCacheHelper.get_FGS_WTCaches_PK10()));
                            return table8;
                        }
                        case 3:
                        {
                            DataTable table9 = CacheHelper.GetCache("fgs_wt_FileCacheKey" + HttpContext.Current.Session["user_name"].ToString() + 3) as DataTable;
                            if (table9 != null)
                            {
                                return table9;
                            }
                            if (!_session.get_kc_op_odds().Equals(1))
                            {
                                goto Label_10E4;
                            }
                            DataTable table10 = CallBLL.cz_odds_wt_xync_bll.GetOddsWTs(_session.get_fgs_name());
                            CacheHelper.SetPublicFileCache("fgs_wt_FileCacheKey" + HttpContext.Current.Session["user_name"].ToString() + 3, table10, PageBase.GetPublicForderPath(FileCacheHelper.get_FGS_WTCaches_XYNC()));
                            return table10;
                        }
                        case 4:
                        {
                            DataTable table11 = CacheHelper.GetCache("fgs_wt_FileCacheKey" + HttpContext.Current.Session["user_name"].ToString() + 4) as DataTable;
                            if (table11 != null)
                            {
                                return table11;
                            }
                            if (!_session.get_kc_op_odds().Equals(1))
                            {
                                goto Label_10E4;
                            }
                            DataTable table12 = CallBLL.cz_odds_wt_jsk3_bll.GetOddsWTs(_session.get_fgs_name());
                            CacheHelper.SetPublicFileCache("fgs_wt_FileCacheKey" + HttpContext.Current.Session["user_name"].ToString() + 4, table12, PageBase.GetPublicForderPath(FileCacheHelper.get_FGS_WTCaches_JSK3()));
                            return table12;
                        }
                        case 5:
                        {
                            DataTable table13 = CacheHelper.GetCache("fgs_wt_FileCacheKey" + HttpContext.Current.Session["user_name"].ToString() + 5) as DataTable;
                            if (table13 != null)
                            {
                                return table13;
                            }
                            if (!_session.get_kc_op_odds().Equals(1))
                            {
                                goto Label_10E4;
                            }
                            DataTable table14 = CallBLL.cz_odds_wt_kl8_bll.GetOddsWTs(_session.get_fgs_name());
                            CacheHelper.SetPublicFileCache("fgs_wt_FileCacheKey" + HttpContext.Current.Session["user_name"].ToString() + 5, table14, PageBase.GetPublicForderPath(FileCacheHelper.get_FGS_WTCaches_KL8()));
                            return table14;
                        }
                        case 6:
                        {
                            DataTable table15 = CacheHelper.GetCache("fgs_wt_FileCacheKey" + HttpContext.Current.Session["user_name"].ToString() + 6) as DataTable;
                            if (table15 != null)
                            {
                                return table15;
                            }
                            if (!_session.get_kc_op_odds().Equals(1))
                            {
                                goto Label_10E4;
                            }
                            DataTable table16 = CallBLL.cz_odds_wt_k8sc_bll.GetOddsWTs(_session.get_fgs_name());
                            CacheHelper.SetPublicFileCache("fgs_wt_FileCacheKey" + HttpContext.Current.Session["user_name"].ToString() + 6, table16, PageBase.GetPublicForderPath(FileCacheHelper.get_FGS_WTCaches_K8SC()));
                            return table16;
                        }
                        case 7:
                        {
                            DataTable table17 = CacheHelper.GetCache("fgs_wt_FileCacheKey" + HttpContext.Current.Session["user_name"].ToString() + 7) as DataTable;
                            if (table17 != null)
                            {
                                return table17;
                            }
                            if (!_session.get_kc_op_odds().Equals(1))
                            {
                                goto Label_10E4;
                            }
                            DataTable table18 = CallBLL.cz_odds_wt_pcdd_bll.GetOddsWTs(_session.get_fgs_name());
                            CacheHelper.SetPublicFileCache("fgs_wt_FileCacheKey" + HttpContext.Current.Session["user_name"].ToString() + 7, table18, PageBase.GetPublicForderPath(FileCacheHelper.get_FGS_WTCaches_PCDD()));
                            return table18;
                        }
                        case 8:
                        {
                            DataTable table21 = CacheHelper.GetCache("fgs_wt_FileCacheKey" + HttpContext.Current.Session["user_name"].ToString() + 8) as DataTable;
                            if (table21 != null)
                            {
                                return table21;
                            }
                            if (!_session.get_kc_op_odds().Equals(1))
                            {
                                goto Label_10E4;
                            }
                            DataTable table22 = CallBLL.cz_odds_wt_pkbjl_bll.GetOddsWTs(_session.get_fgs_name());
                            CacheHelper.SetPublicFileCache("fgs_wt_FileCacheKey" + HttpContext.Current.Session["user_name"].ToString() + 8, table22, PageBase.GetPublicForderPath(FileCacheHelper.get_FGS_WTCaches_PKBJL()));
                            return table22;
                        }
                        case 9:
                        {
                            DataTable table19 = CacheHelper.GetCache("fgs_wt_FileCacheKey" + HttpContext.Current.Session["user_name"].ToString() + 9) as DataTable;
                            if (table19 != null)
                            {
                                return table19;
                            }
                            if (!_session.get_kc_op_odds().Equals(1))
                            {
                                goto Label_10E4;
                            }
                            DataTable table20 = CallBLL.cz_odds_wt_xyft5_bll.GetOddsWTs(_session.get_fgs_name());
                            CacheHelper.SetPublicFileCache("fgs_wt_FileCacheKey" + HttpContext.Current.Session["user_name"].ToString() + 9, table20, PageBase.GetPublicForderPath(FileCacheHelper.get_FGS_WTCaches_XYFT5()));
                            return table20;
                        }
                        case 10:
                        {
                            DataTable table23 = CacheHelper.GetCache("fgs_wt_FileCacheKey" + HttpContext.Current.Session["user_name"].ToString() + 10) as DataTable;
                            if (table23 != null)
                            {
                                return table23;
                            }
                            if (!_session.get_kc_op_odds().Equals(1))
                            {
                                goto Label_10E4;
                            }
                            DataTable table24 = CallBLL.cz_odds_wt_jscar_bll.GetOddsWTs(_session.get_fgs_name());
                            CacheHelper.SetPublicFileCache("fgs_wt_FileCacheKey" + HttpContext.Current.Session["user_name"].ToString() + 10, table24, PageBase.GetPublicForderPath(FileCacheHelper.get_FGS_WTCaches_JSCAR()));
                            return table24;
                        }
                        case 11:
                        {
                            DataTable table25 = CacheHelper.GetCache("fgs_wt_FileCacheKey" + HttpContext.Current.Session["user_name"].ToString() + 11) as DataTable;
                            if (table25 != null)
                            {
                                return table25;
                            }
                            if (!_session.get_kc_op_odds().Equals(1))
                            {
                                goto Label_10E4;
                            }
                            DataTable table26 = CallBLL.cz_odds_wt_speed5_bll.GetOddsWTs(_session.get_fgs_name());
                            CacheHelper.SetPublicFileCache("fgs_wt_FileCacheKey" + HttpContext.Current.Session["user_name"].ToString() + 11, table26, PageBase.GetPublicForderPath(FileCacheHelper.get_FGS_WTCaches_SPEED5()));
                            return table26;
                        }
                        case 12:
                        {
                            DataTable table29 = CacheHelper.GetCache("fgs_wt_FileCacheKey" + HttpContext.Current.Session["user_name"].ToString() + 12) as DataTable;
                            if (table29 != null)
                            {
                                return table29;
                            }
                            if (!_session.get_kc_op_odds().Equals(1))
                            {
                                goto Label_10E4;
                            }
                            DataTable table30 = CallBLL.cz_odds_wt_jspk10_bll.GetOddsWTs(_session.get_fgs_name());
                            CacheHelper.SetPublicFileCache("fgs_wt_FileCacheKey" + HttpContext.Current.Session["user_name"].ToString() + 12, table30, PageBase.GetPublicForderPath(FileCacheHelper.get_FGS_WTCaches_JSPK10()));
                            return table30;
                        }
                        case 13:
                        {
                            DataTable table27 = CacheHelper.GetCache("fgs_wt_FileCacheKey" + HttpContext.Current.Session["user_name"].ToString() + 13) as DataTable;
                            if (table27 != null)
                            {
                                return table27;
                            }
                            if (!_session.get_kc_op_odds().Equals(1))
                            {
                                goto Label_10E4;
                            }
                            DataTable table28 = CallBLL.cz_odds_wt_jscqsc_bll.GetOddsWTs(_session.get_fgs_name());
                            CacheHelper.SetPublicFileCache("fgs_wt_FileCacheKey" + HttpContext.Current.Session["user_name"].ToString() + 13, table28, PageBase.GetPublicForderPath(FileCacheHelper.get_FGS_WTCaches_JSCQSC()));
                            return table28;
                        }
                        case 14:
                        {
                            DataTable table31 = CacheHelper.GetCache("fgs_wt_FileCacheKey" + HttpContext.Current.Session["user_name"].ToString() + 14) as DataTable;
                            if (table31 != null)
                            {
                                return table31;
                            }
                            if (!_session.get_kc_op_odds().Equals(1))
                            {
                                goto Label_10E4;
                            }
                            DataTable table32 = CallBLL.cz_odds_wt_jssfc_bll.GetOddsWTs(_session.get_fgs_name());
                            CacheHelper.SetPublicFileCache("fgs_wt_FileCacheKey" + HttpContext.Current.Session["user_name"].ToString() + 14, table32, PageBase.GetPublicForderPath(FileCacheHelper.get_FGS_WTCaches_JSSFC()));
                            return table32;
                        }
                        case 15:
                        {
                            DataTable table33 = CacheHelper.GetCache("fgs_wt_FileCacheKey" + HttpContext.Current.Session["user_name"].ToString() + 15) as DataTable;
                            if (table33 != null)
                            {
                                return table33;
                            }
                            if (!_session.get_kc_op_odds().Equals(1))
                            {
                                goto Label_10E4;
                            }
                            DataTable table34 = CallBLL.cz_odds_wt_jsft2_bll.GetOddsWTs(_session.get_fgs_name());
                            CacheHelper.SetPublicFileCache("fgs_wt_FileCacheKey" + HttpContext.Current.Session["user_name"].ToString() + 15, table34, PageBase.GetPublicForderPath(FileCacheHelper.get_FGS_WTCaches_JSFT2()));
                            return table34;
                        }
                        case 0x10:
                        {
                            DataTable table35 = CacheHelper.GetCache("fgs_wt_FileCacheKey" + HttpContext.Current.Session["user_name"].ToString() + 0x10) as DataTable;
                            if (table35 != null)
                            {
                                return table35;
                            }
                            if (!_session.get_kc_op_odds().Equals(1))
                            {
                                goto Label_10E4;
                            }
                            DataTable table36 = CallBLL.cz_odds_wt_car168_bll.GetOddsWTs(_session.get_fgs_name());
                            CacheHelper.SetPublicFileCache("fgs_wt_FileCacheKey" + HttpContext.Current.Session["user_name"].ToString() + 0x10, table36, PageBase.GetPublicForderPath(FileCacheHelper.get_FGS_WTCaches_CAR168()));
                            return table36;
                        }
                        case 0x11:
                        {
                            DataTable table37 = CacheHelper.GetCache("fgs_wt_FileCacheKey" + HttpContext.Current.Session["user_name"].ToString() + 0x11) as DataTable;
                            if (table37 != null)
                            {
                                return table37;
                            }
                            if (!_session.get_kc_op_odds().Equals(1))
                            {
                                goto Label_10E4;
                            }
                            DataTable table38 = CallBLL.cz_odds_wt_ssc168_bll.GetOddsWTs(_session.get_fgs_name());
                            CacheHelper.SetPublicFileCache("fgs_wt_FileCacheKey" + HttpContext.Current.Session["user_name"].ToString() + 0x11, table38, PageBase.GetPublicForderPath(FileCacheHelper.get_FGS_WTCaches_SSC168()));
                            return table38;
                        }
                        case 0x12:
                        {
                            DataTable table39 = CacheHelper.GetCache("fgs_wt_FileCacheKey" + HttpContext.Current.Session["user_name"].ToString() + 0x12) as DataTable;
                            if (table39 != null)
                            {
                                return table39;
                            }
                            if (!_session.get_kc_op_odds().Equals(1))
                            {
                                goto Label_10E4;
                            }
                            DataTable table40 = CallBLL.cz_odds_wt_vrcar_bll.GetOddsWTs(_session.get_fgs_name());
                            CacheHelper.SetPublicFileCache("fgs_wt_FileCacheKey" + HttpContext.Current.Session["user_name"].ToString() + 0x12, table40, PageBase.GetPublicForderPath(FileCacheHelper.get_FGS_WTCaches_VRCAR()));
                            return table40;
                        }
                        case 0x13:
                        {
                            DataTable table41 = CacheHelper.GetCache("fgs_wt_FileCacheKey" + HttpContext.Current.Session["user_name"].ToString() + 0x13) as DataTable;
                            if (table41 != null)
                            {
                                return table41;
                            }
                            if (!_session.get_kc_op_odds().Equals(1))
                            {
                                goto Label_10E4;
                            }
                            DataTable table42 = CallBLL.cz_odds_wt_vrssc_bll.GetOddsWTs(_session.get_fgs_name());
                            CacheHelper.SetPublicFileCache("fgs_wt_FileCacheKey" + HttpContext.Current.Session["user_name"].ToString() + 0x13, table42, PageBase.GetPublicForderPath(FileCacheHelper.get_FGS_WTCaches_VRSSC()));
                            return table42;
                        }
                        case 20:
                        {
                            DataTable table43 = CacheHelper.GetCache("fgs_wt_FileCacheKey" + HttpContext.Current.Session["user_name"].ToString() + 20) as DataTable;
                            if (table43 != null)
                            {
                                return table43;
                            }
                            if (!_session.get_kc_op_odds().Equals(1))
                            {
                                goto Label_10E4;
                            }
                            DataTable table44 = CallBLL.cz_odds_wt_xyftoa_bll.GetOddsWTs(_session.get_fgs_name());
                            CacheHelper.SetPublicFileCache("fgs_wt_FileCacheKey" + HttpContext.Current.Session["user_name"].ToString() + 20, table44, PageBase.GetPublicForderPath(FileCacheHelper.get_FGS_WTCaches_XYFTOA()));
                            return table44;
                        }
                        case 0x15:
                        {
                            DataTable table45 = CacheHelper.GetCache("fgs_wt_FileCacheKey" + HttpContext.Current.Session["user_name"].ToString() + 0x15) as DataTable;
                            if (table45 != null)
                            {
                                return table45;
                            }
                            if (!_session.get_kc_op_odds().Equals(1))
                            {
                                goto Label_10E4;
                            }
                            DataTable table46 = CallBLL.cz_odds_wt_xyftsg_bll.GetOddsWTs(_session.get_fgs_name());
                            CacheHelper.SetPublicFileCache("fgs_wt_FileCacheKey" + HttpContext.Current.Session["user_name"].ToString() + 0x15, table46, PageBase.GetPublicForderPath(FileCacheHelper.get_FGS_WTCaches_XYFTSG()));
                            return table46;
                        }
                        case 0x16:
                        {
                            DataTable table47 = CacheHelper.GetCache("fgs_wt_FileCacheKey" + HttpContext.Current.Session["user_name"].ToString() + 0x16) as DataTable;
                            if (table47 != null)
                            {
                                return table47;
                            }
                            if (!_session.get_kc_op_odds().Equals(1))
                            {
                                goto Label_10E4;
                            }
                            DataTable table48 = CallBLL.cz_odds_wt_happycar_bll.GetOddsWTs(_session.get_fgs_name());
                            CacheHelper.SetPublicFileCache("fgs_wt_FileCacheKey" + HttpContext.Current.Session["user_name"].ToString() + 0x16, table48, PageBase.GetPublicForderPath(FileCacheHelper.get_FGS_WTCaches_HAPPYCAR()));
                            return table48;
                        }
                        case 100:
                        {
                            DataTable table = CacheHelper.GetCache("fgs_wt_FileCacheKey" + HttpContext.Current.Session["user_name"].ToString() + 100) as DataTable;
                            if (table == null)
                            {
                                if (_session.get_six_op_odds().Equals(1))
                                {
                                    DataTable table2 = CallBLL.cz_odds_wt_six_bll.GetOddsWTs(_session.get_fgs_name());
                                    CacheHelper.SetPublicFileCache("fgs_wt_FileCacheKey" + HttpContext.Current.Session["user_name"].ToString() + 100, table2, PageBase.GetPublicForderPath(FileCacheHelper.get_FGS_WTCaches_SIX()));
                                    return table2;
                                }
                                goto Label_10E4;
                            }
                            return table;
                        }
                    }
                }
            }
            return null;
        Label_10E4:
            return null;
        }

        private void fixed_list(ref List<object> obj, DataRow[] rows)
        {
            int num = 5;
            int num2 = 0;
            foreach (DataRow row in rows)
            {
                if (num2 == num)
                {
                    return;
                }
                obj.Add(row);
                num2++;
            }
        }

        protected void ForcedModifyPassword()
        {
            string str = this.Context.Request.Path.ToLower();
            if (!string.IsNullOrEmpty(this.Session["modifypassword"]) && (str.IndexOf("resetpasswd.aspx") < 0))
            {
                base.Response.Redirect(string.Format("ResetPasswd.aspx", new object[0]), true);
                base.Response.End();
            }
        }

        public string get_children_name()
        {
            if (HttpContext.Current.Session["child_user_name"] != null)
            {
                return HttpContext.Current.Session["child_user_name"].ToString();
            }
            return "";
        }

        private ArrayList get_current_lottery()
        {
            ArrayList list = new ArrayList();
            DataTable lotteryList = this.GetLotteryList();
            if (lotteryList != null)
            {
                foreach (DataRow row in lotteryList.Rows)
                {
                    list.Add(row["id"].ToString());
                }
            }
            return list;
        }

        public int get_current_master_id()
        {
            int num = 0;
            ArrayList list = this.get_current_lottery();
            int count = list.Count;
            int num3 = 100;
            if (list.Contains(num3.ToString()))
            {
                if (count == 1)
                {
                    num = 1;
                }
                return num;
            }
            return 2;
        }

        private ArrayList get_current_phase_id()
        {
            ArrayList list = new ArrayList();
            string str = "";
            string str2 = "";
            string str3 = "";
            string str4 = "";
            string str5 = "";
            string str6 = "";
            string str7 = "";
            string str8 = "";
            string str9 = "";
            string str10 = "";
            string str11 = "";
            string str12 = "";
            string str13 = "";
            string str14 = "";
            string str15 = "";
            string str16 = "";
            string str17 = "";
            string str18 = "";
            string str19 = "";
            string str20 = "";
            string str21 = "";
            string str22 = "";
            string str23 = "";
            string str24 = "";
            DataTable table = CallBLL.cz_phase_kl10_bll.IsOpenPhaseByTime(DateTime.Now);
            if (table != null)
            {
                str2 = table.Rows[0]["p_id"].ToString();
                list.Add(str2);
            }
            table = CallBLL.cz_phase_cqsc_bll.IsOpenPhaseByTime(DateTime.Now);
            if (table != null)
            {
                str3 = table.Rows[0]["p_id"].ToString();
                list.Add(str3);
            }
            table = CallBLL.cz_phase_kl8_bll.IsOpenPhaseByTime(DateTime.Now);
            if (table != null)
            {
                str4 = table.Rows[0]["p_id"].ToString();
                list.Add(str4);
            }
            table = CallBLL.cz_phase_jsk3_bll.IsOpenPhaseByTime(DateTime.Now);
            if (table != null)
            {
                str5 = table.Rows[0]["p_id"].ToString();
                list.Add(str5);
            }
            table = CallBLL.cz_phase_xync_bll.IsOpenPhaseByTime(DateTime.Now);
            if (table != null)
            {
                str6 = table.Rows[0]["p_id"].ToString();
                list.Add(str6);
            }
            table = CallBLL.cz_phase_pk10_bll.IsOpenPhaseByTime(DateTime.Now);
            if (table != null)
            {
                str7 = table.Rows[0]["p_id"].ToString();
                list.Add(str7);
            }
            table = CallBLL.cz_phase_k8sc_bll.IsOpenPhaseByTime(DateTime.Now);
            if (table != null)
            {
                str8 = table.Rows[0]["p_id"].ToString();
                list.Add(str8);
            }
            table = CallBLL.cz_phase_pcdd_bll.IsOpenPhaseByTime(DateTime.Now);
            if (table != null)
            {
                str9 = table.Rows[0]["p_id"].ToString();
                list.Add(str9);
            }
            table = CallBLL.cz_phase_pkbjl_bll.IsOpenPhaseByTime(DateTime.Now);
            if (table != null)
            {
                str10 = table.Rows[0]["p_id"].ToString();
                list.Add(str10);
            }
            table = CallBLL.cz_phase_xyft5_bll.IsOpenPhaseByTime(DateTime.Now);
            if (table != null)
            {
                str11 = table.Rows[0]["p_id"].ToString();
                list.Add(str11);
            }
            table = CallBLL.cz_phase_jscar_bll.IsOpenPhaseByTime(DateTime.Now);
            if (table != null)
            {
                str12 = table.Rows[0]["p_id"].ToString();
                list.Add(str12);
            }
            cz_phase_six currentPhase = CallBLL.cz_phase_six_bll.GetCurrentPhase();
            if (currentPhase != null)
            {
                str = currentPhase.get_p_id().ToString();
                list.Add(str);
            }
            table = CallBLL.cz_phase_speed5_bll.IsOpenPhaseByTime(DateTime.Now);
            if (table != null)
            {
                str13 = table.Rows[0]["p_id"].ToString();
                list.Add(str13);
            }
            table = CallBLL.cz_phase_jscqsc_bll.IsOpenPhaseByTime(DateTime.Now);
            if (table != null)
            {
                str15 = table.Rows[0]["p_id"].ToString();
                list.Add(str15);
            }
            table = CallBLL.cz_phase_jspk10_bll.IsOpenPhaseByTime(DateTime.Now);
            if (table != null)
            {
                str14 = table.Rows[0]["p_id"].ToString();
                list.Add(str14);
            }
            table = CallBLL.cz_phase_jssfc_bll.IsOpenPhaseByTime(DateTime.Now);
            if (table != null)
            {
                str16 = table.Rows[0]["p_id"].ToString();
                list.Add(str16);
            }
            table = CallBLL.cz_phase_jsft2_bll.IsOpenPhaseByTime(DateTime.Now);
            if (table != null)
            {
                str17 = table.Rows[0]["p_id"].ToString();
                list.Add(str17);
            }
            table = CallBLL.cz_phase_car168_bll.IsOpenPhaseByTime(DateTime.Now);
            if (table != null)
            {
                str18 = table.Rows[0]["p_id"].ToString();
                list.Add(str18);
            }
            table = CallBLL.cz_phase_ssc168_bll.IsOpenPhaseByTime(DateTime.Now);
            if (table != null)
            {
                str19 = table.Rows[0]["p_id"].ToString();
                list.Add(str19);
            }
            table = CallBLL.cz_phase_vrcar_bll.IsOpenPhaseByTime(DateTime.Now);
            if (table != null)
            {
                str20 = table.Rows[0]["p_id"].ToString();
                list.Add(str20);
            }
            table = CallBLL.cz_phase_vrssc_bll.IsOpenPhaseByTime(DateTime.Now);
            if (table != null)
            {
                str21 = table.Rows[0]["p_id"].ToString();
                list.Add(str21);
            }
            table = CallBLL.cz_phase_xyftoa_bll.IsOpenPhaseByTime(DateTime.Now);
            if (table != null)
            {
                str22 = table.Rows[0]["p_id"].ToString();
                list.Add(str22);
            }
            table = CallBLL.cz_phase_xyftsg_bll.IsOpenPhaseByTime(DateTime.Now);
            if (table != null)
            {
                str23 = table.Rows[0]["p_id"].ToString();
                list.Add(str23);
            }
            table = CallBLL.cz_phase_happycar_bll.IsOpenPhaseByTime(DateTime.Now);
            if (table != null)
            {
                str24 = table.Rows[0]["p_id"].ToString();
                list.Add(str24);
            }
            return list;
        }

        private string get_drawback(string flag)
        {
            if (flag == "0")
            {
                return "水全退到底";
            }
            if (flag == "NO")
            {
                return "賺取所有退水";
            }
            return ("賺取" + flag + "退水");
        }

        private DataTable get_jp_talbe()
        {
            string str = string.Join(",", this.get_current_lottery().ToArray());
            string str2 = string.Join(",", this.get_current_phase_id().ToArray());
            return CallBLL.cz_jp_odds_bll.GetTableInfo(str2, str);
        }

        protected string get_kc_change_val(string lotteryID, string play_id, string o_v, string n_v)
        {
            string[] strArray;
            string[] strArray2;
            int length;
            string str2;
            string str3;
            int num2;
            string str = "";
            switch (Convert.ToInt32(lotteryID))
            {
                case 0:
                case 14:
                    if (!(play_id == "122"))
                    {
                        return str;
                    }
                    strArray = o_v.Split(new char[] { '|' });
                    strArray2 = n_v.Split(new char[] { '|' });
                    length = strArray.Length;
                    str2 = "";
                    str3 = "";
                    num2 = 0;
                    goto Label_012E;

                case 1:
                case 11:
                case 13:
                case 0x11:
                case 0x13:
                {
                    if (!(play_id == "18"))
                    {
                        if (!(play_id == "19"))
                        {
                            return str;
                        }
                        string[] strArray5 = o_v.Split(new char[] { '|' });
                        string[] strArray6 = n_v.Split(new char[] { '|' });
                        int num5 = strArray5.Length;
                        string str6 = "";
                        string str7 = "";
                        for (int j = 0; j < num5; j++)
                        {
                            if (!strArray5[j].Equals(strArray6[j]))
                            {
                                if (j == 0)
                                {
                                    str6 = "豹子:" + strArray5[j];
                                    str7 = "豹子:" + strArray6[j];
                                }
                                if (j == 1)
                                {
                                    str6 = str6 + " 順子:" + strArray5[j];
                                    str7 = str7 + " 順子:" + strArray6[j];
                                }
                                if (j == 2)
                                {
                                    str6 = " 對子:" + strArray5[j];
                                    str7 = " 對子:" + strArray6[j];
                                }
                                if (j == 3)
                                {
                                    str6 = str6 + " 半順:" + strArray5[j];
                                    str7 = str7 + " 半順:" + strArray6[j];
                                }
                                if (j == 4)
                                {
                                    str6 = " 雜六:" + strArray5[j];
                                    str7 = " 雜六:" + strArray6[j];
                                }
                            }
                        }
                        return (str = str6 + "|" + str7);
                    }
                    string[] strArray3 = o_v.Split(new char[] { '|' });
                    string[] strArray4 = n_v.Split(new char[] { '|' });
                    int num3 = strArray3.Length;
                    string str4 = "";
                    string str5 = "";
                    for (int i = 0; i < num3; i++)
                    {
                        if (!strArray3[i].Equals(strArray4[i]))
                        {
                            if (i == 0)
                            {
                                str4 = "龍虎:" + strArray3[i];
                                str5 = "龍虎:" + strArray4[i];
                            }
                            if (i == 1)
                            {
                                str4 = str4 + " 和:" + strArray3[i];
                                str5 = str5 + " 和:" + strArray4[i];
                            }
                        }
                    }
                    return (str = str4 + "|" + str5);
                }
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
                {
                    if (!(play_id == "38"))
                    {
                        if (play_id == "37")
                        {
                            string[] strArray9 = o_v.Split(new char[] { '|' });
                            string[] strArray10 = n_v.Split(new char[] { '|' });
                            int num9 = strArray9.Length;
                            string str10 = "";
                            string str11 = "";
                            for (int n = 0; n < num9; n++)
                            {
                                if (!strArray9[n].Equals(strArray10[n]))
                                {
                                    if (n == 0)
                                    {
                                        str10 = "大:" + strArray9[n];
                                        str11 = "大:" + strArray10[n];
                                    }
                                    if (n == 1)
                                    {
                                        str10 = str10 + " 小:" + strArray9[n];
                                        str11 = str11 + " 小:" + strArray10[n];
                                    }
                                }
                            }
                            return (str = str10 + "|" + str11);
                        }
                        if (!(play_id == "36"))
                        {
                            return str;
                        }
                        string[] strArray11 = o_v.Split(new char[] { '|' });
                        string[] strArray12 = n_v.Split(new char[] { '|' });
                        int num11 = strArray11.Length;
                        string str12 = "";
                        string str13 = "";
                        for (int m = 0; m < num11; m++)
                        {
                            if (!strArray11[m].Equals(strArray12[m]))
                            {
                                if (m == 0)
                                {
                                    str12 = "3、4、18、19:" + strArray11[m];
                                    str13 = "3、4、18、19:" + strArray12[m];
                                }
                                if (m == 1)
                                {
                                    str12 = str12 + " 5、6、16、17:" + strArray11[m];
                                    str13 = str13 + " 5、6、16、17:" + strArray12[m];
                                }
                                if (m == 2)
                                {
                                    str12 = " 7、8、14、15:" + strArray11[m];
                                    str13 = " 7、8、14、15:" + strArray12[m];
                                }
                                if (m == 3)
                                {
                                    str12 = str12 + " 9、10、12、13:" + strArray11[m];
                                    str13 = str13 + " 9、10、12、13:" + strArray12[m];
                                }
                                if (m == 4)
                                {
                                    str12 = " 11:" + strArray11[m];
                                    str13 = " 11:" + strArray12[m];
                                }
                            }
                        }
                        return (str = str12 + "|" + str13);
                    }
                    string[] strArray7 = o_v.Split(new char[] { '|' });
                    string[] strArray8 = n_v.Split(new char[] { '|' });
                    int num7 = strArray7.Length;
                    string str8 = "";
                    string str9 = "";
                    for (int k = 0; k < num7; k++)
                    {
                        if (!strArray7[k].Equals(strArray8[k]))
                        {
                            if (k == 0)
                            {
                                str8 = "單:" + strArray7[k];
                                str9 = "單:" + strArray8[k];
                            }
                            if (k == 1)
                            {
                                str8 = str8 + " 雙:" + strArray7[k];
                                str9 = str9 + " 雙:" + strArray8[k];
                            }
                        }
                    }
                    return (str = str8 + "|" + str9);
                }
                case 3:
                {
                    if (!(play_id == "122"))
                    {
                        return str;
                    }
                    string[] strArray13 = o_v.Split(new char[] { '|' });
                    string[] strArray14 = n_v.Split(new char[] { '|' });
                    int num13 = strArray13.Length;
                    string str14 = "";
                    string str15 = "";
                    for (int num14 = 0; num14 < num13; num14++)
                    {
                        if (!strArray13[num14].Equals(strArray14[num14]))
                        {
                            if (num14 == 0)
                            {
                                str14 = "1-8球中發白:" + strArray13[num14];
                                str15 = "1-8球中發白:" + strArray14[num14];
                            }
                            if (num14 == 1)
                            {
                                str14 = str14 + " 白:" + strArray13[num14];
                                str15 = str15 + " 白:" + strArray14[num14];
                            }
                        }
                    }
                    return (str = str14 + "|" + str15);
                }
                case 4:
                {
                    if (!(play_id == "62"))
                    {
                        return str;
                    }
                    string[] strArray15 = o_v.Split(new char[] { '|' });
                    string[] strArray16 = n_v.Split(new char[] { '|' });
                    int num15 = strArray15.Length;
                    string str16 = "";
                    string str17 = "";
                    for (int num16 = 0; num16 < num15; num16++)
                    {
                        if (!strArray15[num16].Equals(strArray16[num16]))
                        {
                            if (num16 == 0)
                            {
                                str16 = "4點:" + strArray15[num16];
                                str17 = "4點:" + strArray16[num16];
                            }
                            if (num16 == 1)
                            {
                                str16 = str16 + " 5點:" + strArray15[num16];
                                str17 = str17 + " 5點:" + strArray16[num16];
                            }
                            if (num16 == 2)
                            {
                                str16 = " 6點:" + strArray15[num16];
                                str17 = " 6點:" + strArray16[num16];
                            }
                            if (num16 == 3)
                            {
                                str16 = str16 + " 7點:" + strArray15[num16];
                                str17 = str17 + " 7點:" + strArray16[num16];
                            }
                            if (num16 == 4)
                            {
                                str16 = " 8點:" + strArray15[num16];
                                str17 = " 8點:" + strArray16[num16];
                            }
                            if (num16 == 5)
                            {
                                str16 = str16 + " 9點:" + strArray15[num16];
                                str17 = str17 + " 9點:" + strArray16[num16];
                            }
                            if (num16 == 6)
                            {
                                str16 = " 10點:" + strArray15[num16];
                                str17 = " 10點:" + strArray16[num16];
                            }
                            if (num16 == 7)
                            {
                                str16 = str16 + " 11點:" + strArray15[num16];
                                str17 = str17 + " 11點:" + strArray16[num16];
                            }
                            if (num16 == 8)
                            {
                                str16 = " 12點:" + strArray15[num16];
                                str17 = " 12點:" + strArray16[num16];
                            }
                            if (num16 == 9)
                            {
                                str16 = str16 + " 13點:" + strArray15[num16];
                                str17 = str17 + " 13點:" + strArray16[num16];
                            }
                            if (num16 == 10)
                            {
                                str16 = str16 + " 14點:" + strArray15[num16];
                                str17 = str17 + " 14點:" + strArray16[num16];
                            }
                            if (num16 == 11)
                            {
                                str16 = str16 + " 15點:" + strArray15[num16];
                                str17 = str17 + " 15點:" + strArray16[num16];
                            }
                            if (num16 == 12)
                            {
                                str16 = str16 + " 16點:" + strArray15[num16];
                                str17 = str17 + " 16點:" + strArray16[num16];
                            }
                            if (num16 == 13)
                            {
                                str16 = str16 + " 17點:" + strArray15[num16];
                                str17 = str17 + " 17點:" + strArray16[num16];
                            }
                        }
                    }
                    return (str = str16 + "|" + str17);
                }
                case 5:
                {
                    if (!(play_id == "70"))
                    {
                        if (play_id == "71")
                        {
                            string[] strArray19 = o_v.Split(new char[] { '|' });
                            string[] strArray20 = n_v.Split(new char[] { '|' });
                            int num19 = strArray19.Length;
                            string str20 = "";
                            string str21 = "";
                            for (int num20 = 0; num20 < num19; num20++)
                            {
                                if (!strArray19[num20].Equals(strArray20[num20]))
                                {
                                    if (num20 == 0)
                                    {
                                        str20 = "單雙:" + strArray19[num20];
                                        str21 = "單雙:" + strArray20[num20];
                                    }
                                    if (num20 == 1)
                                    {
                                        str20 = str20 + " 和:" + strArray19[num20];
                                        str21 = str21 + " 和:" + strArray20[num20];
                                    }
                                }
                            }
                            return (str = str20 + "|" + str21);
                        }
                        if (!(play_id == "72"))
                        {
                            return str;
                        }
                        string[] strArray21 = o_v.Split(new char[] { '|' });
                        string[] strArray22 = n_v.Split(new char[] { '|' });
                        int num21 = strArray21.Length;
                        string str22 = "";
                        string str23 = "";
                        for (int num22 = 0; num22 < num21; num22++)
                        {
                            if (!strArray21[num22].Equals(strArray22[num22]))
                            {
                                if (num22 == 0)
                                {
                                    str22 = "金:" + strArray21[num22];
                                    str23 = "金:" + strArray22[num22];
                                }
                                if (num22 == 1)
                                {
                                    str22 = str22 + " 木:" + strArray21[num22];
                                    str23 = str23 + " 木:" + strArray22[num22];
                                }
                                if (num22 == 2)
                                {
                                    str22 = " 水:" + strArray21[num22];
                                    str23 = " 水:" + strArray22[num22];
                                }
                                if (num22 == 3)
                                {
                                    str22 = str22 + " 火:" + strArray21[num22];
                                    str23 = str23 + " 火:" + strArray22[num22];
                                }
                                if (num22 == 4)
                                {
                                    str22 = " 土:" + strArray21[num22];
                                    str23 = " 土:" + strArray22[num22];
                                }
                            }
                        }
                        return (str = str22 + "|" + str23);
                    }
                    string[] strArray17 = o_v.Split(new char[] { '|' });
                    string[] strArray18 = n_v.Split(new char[] { '|' });
                    int num17 = strArray17.Length;
                    string str18 = "";
                    string str19 = "";
                    for (int num18 = 0; num18 < num17; num18++)
                    {
                        if (!strArray17[num18].Equals(strArray18[num18]))
                        {
                            if (num18 == 0)
                            {
                                str18 = "前後:" + strArray17[num18];
                                str19 = "前後:" + strArray18[num18];
                            }
                            if (num18 == 1)
                            {
                                str18 = str18 + " 和:" + strArray17[num18];
                                str19 = str19 + " 和:" + strArray18[num18];
                            }
                        }
                    }
                    return (str = str18 + "|" + str19);
                }
                case 6:
                {
                    if (!(play_id == "18"))
                    {
                        if (!(play_id == "19"))
                        {
                            return str;
                        }
                        string[] strArray25 = o_v.Split(new char[] { '|' });
                        string[] strArray26 = n_v.Split(new char[] { '|' });
                        int num25 = strArray25.Length;
                        string str26 = "";
                        string str27 = "";
                        for (int num26 = 0; num26 < num25; num26++)
                        {
                            if (!strArray25[num26].Equals(strArray26[num26]))
                            {
                                if (num26 == 0)
                                {
                                    str26 = "豹子:" + strArray25[num26];
                                    str27 = "豹子:" + strArray26[num26];
                                }
                                if (num26 == 1)
                                {
                                    str26 = str26 + " 順子:" + strArray25[num26];
                                    str27 = str27 + " 順子:" + strArray26[num26];
                                }
                                if (num26 == 2)
                                {
                                    str26 = " 對子:" + strArray25[num26];
                                    str27 = " 對子:" + strArray26[num26];
                                }
                                if (num26 == 3)
                                {
                                    str26 = str26 + " 半順:" + strArray25[num26];
                                    str27 = str27 + " 半順:" + strArray26[num26];
                                }
                                if (num26 == 4)
                                {
                                    str26 = " 雜六:" + strArray25[num26];
                                    str27 = " 雜六:" + strArray26[num26];
                                }
                            }
                        }
                        return (str = str26 + "|" + str27);
                    }
                    string[] strArray23 = o_v.Split(new char[] { '|' });
                    string[] strArray24 = n_v.Split(new char[] { '|' });
                    int num23 = strArray23.Length;
                    string str24 = "";
                    string str25 = "";
                    for (int num24 = 0; num24 < num23; num24++)
                    {
                        if (!strArray23[num24].Equals(strArray24[num24]))
                        {
                            if (num24 == 0)
                            {
                                str24 = "龍虎:" + strArray23[num24];
                                str25 = "龍虎:" + strArray24[num24];
                            }
                            if (num24 == 1)
                            {
                                str24 = str24 + " 和:" + strArray23[num24];
                                str25 = str25 + " 和:" + strArray24[num24];
                            }
                        }
                    }
                    return (str = str24 + "|" + str25);
                }
                case 7:
                case 8:
                    return str;

                default:
                    return str;
            }
        Label_0128:
            num2++;
        Label_012E:
            if (num2 < length)
            {
                if (!strArray[num2].Equals(strArray2[num2]))
                {
                    switch (num2)
                    {
                        case 0:
                            str2 = "1-8球中發白:" + strArray[num2];
                            str3 = "1-8球中發白:" + strArray2[num2];
                            break;

                        case 1:
                            str2 = str2 + " 白:" + strArray[num2];
                            str3 = str3 + " 白:" + strArray2[num2];
                            break;
                    }
                }
                goto Label_0128;
            }
            return (str = str2 + "|" + str3);
        }

        protected string get_master_name()
        {
            return HttpContext.Current.Session["user_name"].ToString();
        }

        public string get_online_cnt()
        {
            int num = 0;
            int num2 = 0;
            string str2 = string.Format("select u_type,count(u_name) from cz_stat_online  with(NOLOCK)  where last_time > '{0}' and u_name not in (select u_name from cz_users_child where is_admin=1)  group by u_type", DateTime.Now.AddMinutes(-3.0));
            foreach (DataRow row in CallBLL.cz_stat_online_bll.query_sql(str2).Rows)
            {
                if (row[0].ToString().Trim() != "hy")
                {
                    num += int.Parse(row[1].ToString());
                }
                else
                {
                    num2 = int.Parse(row[1].ToString());
                }
            }
            return (num.ToString() + "|" + num2.ToString());
        }

        public DataTable get_online_detail()
        {
            string str = string.Format(" select * from cz_stat_online with(NOLOCK) where last_time >'{0}' order by last_time desc", DateTime.Now.AddMinutes(-3.0));
            return CallBLL.cz_stat_online_bll.query_sql(str);
        }

        private string get_pk(string type)
        {
            string str = type;
            if (type == "0")
            {
                str = "不限";
            }
            return str;
        }

        private void get_playinfo(DataTable dt, string odds_id, ref string cotegory, ref string play_name, ref string put_val)
        {
            if (dt != null)
            {
                DataRow[] source = dt.Select(string.Format(" odds_id={0} ", odds_id));
                if (source.Count<DataRow>() > 0)
                {
                    cotegory = source[0]["category"].ToString();
                    play_name = source[0]["play_name"].ToString();
                    put_val = source[0]["put_amount"].ToString();
                }
            }
        }

        private string get_playpage_name(string playpage)
        {
            string str = "";
            if (playpage == "tma")
            {
                return "特碼A";
            }
            if (playpage == "tmb")
            {
                return "特碼B";
            }
            if (playpage == "tmab")
            {
                return "特碼AB-A";
            }
            if (playpage == "tmzx1")
            {
                return "總項1";
            }
            if (playpage == "tmzx2")
            {
                return "總項2";
            }
            if (playpage == "zm")
            {
                return "正碼";
            }
            if (playpage == "zmt1")
            {
                return "正1特";
            }
            if (playpage == "zmt2")
            {
                return "正2特";
            }
            if (playpage == "zmt3")
            {
                return "正3特";
            }
            if (playpage == "zmt4")
            {
                return "正4特";
            }
            if (playpage == "zmt5")
            {
                return "正5特";
            }
            if (playpage == "zmt6")
            {
                return "正6特";
            }
            if (playpage == "lm")
            {
                return "連碼";
            }
            if (playpage == "bz")
            {
                return "不中";
            }
            if (playpage == "zm1-6")
            {
                return "正碼1-6";
            }
            if (playpage == "tmsxsb")
            {
                return "特碼生肖色波";
            }
            if (playpage == "sxws")
            {
                return "生肖尾數(中)";
            }
            if (playpage == "sxwsbz")
            {
                return "生肖尾數(不中)";
            }
            if (playpage == "bb")
            {
                return "半波";
            }
            if (playpage == "lxl")
            {
                return "六肖...連";
            }
            if (playpage == "lhtmtz")
            {
                return "龍虎-特碼攤子";
            }
            if (playpage == "qmwx")
            {
                str = "七碼五行 ";
            }
            return str;
        }

        private string get_playpage_name_kc(int lotteryID, string playpage)
        {
            string str = "";
            switch (lotteryID)
            {
                case 0:
                case 14:
                    if (!(playpage == "p1"))
                    {
                        if (playpage == "p2")
                        {
                            return "第二球";
                        }
                        if (playpage == "p3")
                        {
                            return "第三球";
                        }
                        if (playpage == "p4")
                        {
                            return "第四球";
                        }
                        if (playpage == "p5")
                        {
                            return "第五球";
                        }
                        if (playpage == "p6")
                        {
                            return "第六球";
                        }
                        if (playpage == "p7")
                        {
                            return "第七球";
                        }
                        if (playpage == "p8")
                        {
                            return "第八球";
                        }
                        if (playpage == "lh")
                        {
                            return "總和、龍虎";
                        }
                        if (playpage == "lm")
                        {
                            str = "連碼";
                        }
                        return str;
                    }
                    return "第一球";

                case 1:
                case 6:
                case 7:
                case 11:
                case 13:
                case 0x11:
                case 0x13:
                    if (playpage == "zx")
                    {
                        str = "總項盤口";
                    }
                    return str;

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
                    if (!(playpage == "p1"))
                    {
                        if (playpage == "p2")
                        {
                            return "三、四、伍、六名";
                        }
                        if (playpage == "p3")
                        {
                            str = "七、八、九、十名";
                        }
                        return str;
                    }
                    return "冠、亞軍 組合";

                case 3:
                    if (!(playpage == "p1"))
                    {
                        if (playpage == "p2")
                        {
                            return "第二球";
                        }
                        if (playpage == "p3")
                        {
                            return "第三球";
                        }
                        if (playpage == "p4")
                        {
                            return "第四球";
                        }
                        if (playpage == "p5")
                        {
                            return "第五球";
                        }
                        if (playpage == "p6")
                        {
                            return "第六球";
                        }
                        if (playpage == "p7")
                        {
                            return "第七球";
                        }
                        if (playpage == "p8")
                        {
                            return "第八球";
                        }
                        if (playpage == "zh")
                        {
                            return "總和、家禽野獸";
                        }
                        if (playpage == "lm")
                        {
                            str = "連碼";
                        }
                        return str;
                    }
                    return "第一球";

                case 4:
                    if (playpage == "zx")
                    {
                        str = "總項盤口";
                    }
                    return str;

                case 5:
                    if (!(playpage == "zh"))
                    {
                        if (playpage == "zm")
                        {
                            str = "正碼";
                        }
                        return str;
                    }
                    return "總和、比數、五行";

                case 8:
                    return "總項盤口";
            }
            return str;
        }

        protected string get_rename_play_name(string lotteryID, int i)
        {
            string str = "";
            switch (Convert.ToInt32(lotteryID))
            {
                case 0:
                case 14:
                    if (i != 0)
                    {
                        if (i == 1)
                        {
                            return "1-8球大小";
                        }
                        if (i == 2)
                        {
                            return "1-8球單雙";
                        }
                        if (i == 3)
                        {
                            return "1-8球尾數大小";
                        }
                        if (i == 4)
                        {
                            return "1-8球合數單雙";
                        }
                        if (i == 5)
                        {
                            return "1-8球方位";
                        }
                        if (i == 6)
                        {
                            str = "1-8球中發白";
                        }
                        return str;
                    }
                    return "1-8球";

                case 1:
                case 11:
                case 13:
                case 0x11:
                case 0x13:
                    if (i != 0)
                    {
                        if (i == 1)
                        {
                            return "1-5球大小";
                        }
                        if (i == 2)
                        {
                            str = "1-5球單雙";
                        }
                        return str;
                    }
                    return "1-5球";

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
                    if (i != 0)
                    {
                        if (i == 1)
                        {
                            return "1-10名大小";
                        }
                        if (i == 2)
                        {
                            return "1-10名單雙";
                        }
                        if (i == 3)
                        {
                            str = "1-5名龍虎";
                        }
                        return str;
                    }
                    return "1-10名";

                case 3:
                    if (i != 0)
                    {
                        if (i == 1)
                        {
                            return "1-8球大小";
                        }
                        if (i == 2)
                        {
                            return "1-8球單雙";
                        }
                        if (i == 3)
                        {
                            return "1-8球尾數大小";
                        }
                        if (i == 4)
                        {
                            return "1-8球合數單雙";
                        }
                        if (i == 5)
                        {
                            return "1-8球梅蘭竹菊";
                        }
                        if (i == 6)
                        {
                            str = "1-8球中發白";
                        }
                        return str;
                    }
                    return "1-8球";

                case 4:
                case 5:
                    return str;

                case 6:
                    if (i != 0)
                    {
                        if (i == 1)
                        {
                            return "1-5球大小";
                        }
                        if (i == 2)
                        {
                            str = "1-5球單雙";
                        }
                        return str;
                    }
                    return "1-5球";

                case 7:
                    if (i == 6)
                    {
                        str = "1-3區大小";
                    }
                    if (i == 7)
                    {
                        str = "1-3區單雙";
                    }
                    return str;

                case 8:
                    if (i != 0)
                    {
                        if (i == 1)
                        {
                            return "莊對";
                        }
                        if (i == 2)
                        {
                            return "閑對";
                        }
                        if (i == 3)
                        {
                            str = "莊閑和";
                        }
                        return str;
                    }
                    return "大小";
            }
            return str;
        }

        public DataTable get_top_online(int days)
        {
            string str = string.Format(" select * from cz_stat_top_online with(NOLOCK) where update_time >'{0}' order by update_time desc", DateTime.Now.AddDays((double) -days));
            return CallBLL.cz_stat_top_online_bll.query_sql(str);
        }

        protected string get_tz(string hm)
        {
            if (hm.Trim() == "49")
            {
                return "和";
            }
            if ((int.Parse(hm) % 4) == 0)
            {
                return "4";
            }
            int num2 = int.Parse(hm) % 4;
            return num2.ToString();
        }

        private string get_user_status(string u_status)
        {
            string str = "";
            if (u_status == "0")
            {
                return "啟用";
            }
            if (u_status == "1")
            {
                return "凍結";
            }
            if (u_status == "2")
            {
                str = "停用";
            }
            return str;
        }

        private string get_zhsj(string type)
        {
            string str = "";
            type = type.Trim().ToLower();
            if (type == "fgs")
            {
                str = "分公司";
            }
            if (type == "gd")
            {
                str = "股東";
            }
            if (type == "zd")
            {
                str = "總代";
            }
            if (type == "dl")
            {
                str = "代理";
            }
            return str;
        }

        public string GetAlert(string message)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("<script>");
            builder.AppendFormat(" alert('{0}');", message);
            builder.Append("</script>");
            return builder.ToString();
        }

        public string GetAlert(string message, string okStr, string closeStr, string openStr)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("<script>");
            builder.Append("seajs.use('alert',function(myAlert){");
            builder.Append("myAlert({");
            builder.AppendFormat("content: '{0}',", message);
            builder.Append("okCallBack: function () { " + okStr + "},");
            builder.Append("closeCallBack:function () { " + closeStr + "},");
            builder.Append("openCallBack: function () { " + openStr + "}");
            builder.Append("})");
            builder.Append("});");
            builder.Append("</script>");
            return builder.ToString();
        }

        public SqlParameter[] GetAllDayPhaseByPid(string p_id, string numTable, ref string reAllDayPhaseStr)
        {
            string str = "";
            DataTable allDayPhaseByPid = CallBLL.cz_phase_pkbjl_bll.GetAllDayPhaseByPid(p_id, numTable);
            for (int i = 0; i < allDayPhaseByPid.Rows.Count; i++)
            {
                if (string.IsNullOrEmpty(str))
                {
                    str = str + allDayPhaseByPid.Rows[i]["p_id"].ToString();
                }
                else
                {
                    str = str + "," + allDayPhaseByPid.Rows[i]["p_id"].ToString();
                }
            }
            return Utils.GetParams(str, ref reAllDayPhaseStr, SqlDbType.NVarChar);
        }

        public string getAnimal(int i)
        {
            switch (i)
            {
                case 1:
                    return "鼠";

                case 2:
                    return "牛";

                case 3:
                    return "虎";

                case 4:
                    return "兔";

                case 5:
                    return "龍";

                case 6:
                    return "蛇";

                case 7:
                    return "馬";

                case 8:
                    return "羊";

                case 9:
                    return "猴";

                case 10:
                    return "雞";

                case 11:
                    return "狗";

                case 12:
                    return "豬";
            }
            return "";
        }

        public List<object> GetAutoJPForAd(string compareTime, ref DateTime dt)
        {
            dt = DateTime.Now;
            int num = 5;
            DataTable cache = null;
            if (CacheHelper.GetCache("all_auto_jp_FileCacheKey") == null)
            {
                cache = this.get_jp_talbe();
                CacheHelper.SetPublicFileCacheDependency("all_auto_jp_FileCacheKey", cache, PageBase.GetPublicForderPath(ConfigurationManager.AppSettings["AutoJPCachesFileName"]));
            }
            else
            {
                cache = CacheHelper.GetCache("all_auto_jp_FileCacheKey") as DataTable;
            }
            if (cache == null)
            {
                return null;
            }
            List<object> list = new List<object>();
            int num2 = 0;
            foreach (DataRow row in cache.Rows)
            {
                if (num2 == num)
                {
                    return list;
                }
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                string str = "";
                "92638,92639,92640,92641,92642,92643".IndexOf(row["odds_id"].ToString());
                dictionary.Add("category", str);
                dictionary.Add("lottery_id", row["lottery_type"].ToString());
                dictionary.Add("lottery_name", base.GetGameNameByID(row["lottery_type"].ToString()));
                dictionary.Add("play_name", row["play_name"].ToString());
                dictionary.Add("put_val", row["put_amount"].ToString());
                dictionary.Add("odds", row["odds"].ToString());
                dictionary.Add("number", row["number"].ToString());
                dictionary.Add("phase", row["phase"].ToString());
                dictionary.Add("add_time", Convert.ToDateTime(row["add_time"].ToString()).ToString("HH:mm:ss"));
                list.Add(new Dictionary<string, object>(dictionary));
                num2++;
            }
            return list;
        }

        public List<object> GetAutoJPForTable(string lottery_ids, string compareTime, ref DateTime dt)
        {
            if (string.IsNullOrEmpty(lottery_ids))
            {
                return null;
            }
            DataTable cache = null;
            if (CacheHelper.GetCache("all_auto_jp_FileCacheKey") == null)
            {
                cache = this.get_jp_talbe();
                CacheHelper.SetPublicFileCacheDependency("all_auto_jp_FileCacheKey", cache, PageBase.GetPublicForderPath(ConfigurationManager.AppSettings["AutoJPCachesFileName"]));
            }
            else
            {
                cache = CacheHelper.GetCache("all_auto_jp_FileCacheKey") as DataTable;
            }
            if (cache == null)
            {
                return null;
            }
            string[] strArray = lottery_ids.Split(new char[] { ',' });
            List<object> list = new List<object>();
            foreach (string str in strArray)
            {
                if (string.IsNullOrEmpty(compareTime))
                {
                    DataRow[] rows = cache.Select(string.Format(" lottery_type={0} ", str), " add_time desc");
                    this.fixed_list(ref list, rows);
                }
                else
                {
                    DateTime time = Utils.StampToDateTime(compareTime);
                    foreach (DataRow row in cache.Select(string.Format(" lottery_type={0} and add_time >= #{1}# ", str, time), " add_time desc "))
                    {
                        list.Add(row);
                    }
                }
            }
            List<object> list2 = new List<object>();
            if (list.Count <= 0)
            {
                return null;
            }
            foreach (DataRow row2 in list)
            {
                string str2 = "";
                "92638,92639,92640,92641,92642,92643".IndexOf(row2["odds_id"].ToString());
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                dictionary.Add("category", str2);
                dictionary.Add("lottery_id", row2["lottery_type"].ToString());
                dictionary.Add("lottery_name", base.GetGameNameByID(row2["lottery_type"].ToString()));
                dictionary.Add("play_name", row2["play_name"].ToString());
                dictionary.Add("put_val", row2["put_amount"].ToString());
                dictionary.Add("odds", row2["odds"].ToString());
                dictionary.Add("old_odds", row2["old_odds"].ToString());
                dictionary.Add("new_odds", row2["new_odds"].ToString());
                dictionary.Add("phase", row2["phase"].ToString());
                dictionary.Add("add_time", row2["add_time"].ToString());
                list2.Add(new Dictionary<string, object>(dictionary));
            }
            return list2;
        }

        protected string GetCAR168_PlayIDChange(string playType)
        {
            string str = "";
            string str2 = playType;
            if (str2 == null)
            {
                return str;
            }
            if (!(str2 == "q1"))
            {
                if (str2 != "q2")
                {
                    if (str2 != "q3")
                    {
                        return str;
                    }
                    return "24,25,26,27,28,29,30,31,32,33,34,35";
                }
            }
            else
            {
                return "1,2,3,4,5,6,7,8,36,37,38";
            }
            return "9,10,11,12,13,14,15,16,17,18,19,20,21,22,23";
        }

        protected string GetCQSC_PlayIDChange(string playType)
        {
            return "1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21";
        }

        public string GetCreditHref(string uid, int masterId)
        {
            string str = "";
            if (masterId.Equals(1))
            {
                str = "six";
            }
            else
            {
                str = "kc";
            }
            string str2 = string.Format("/credit_flow_list.aspx?uid={0}&mlid={1}", uid, masterId);
            string str3 = string.Format("/Account/credit_recharge_{0}.aspx?uid={1}&mlid={2}", str, uid, masterId);
            string str4 = string.Format("/Account/credit_withdraw_{0}.aspx?uid={1}&mlid={2}", str, uid, masterId);
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("<a class=\"editBtn black btn\" href=\"{0}\">充值</a> <a class=\"editBtn black btn\" href=\"{1}\">提現</a> <a class=\"editBtn black btn\" href=\"{2}\">现金流</a>", str3, str4, str2);
            return builder.ToString();
        }

        protected void GetDataTipPhase(string lid, string phase_id, string play_id, ref string phase)
        {
            switch (int.Parse(lid))
            {
                case 0:
                {
                    if (string.IsNullOrEmpty(phase_id) || phase_id.Equals("0"))
                    {
                        break;
                    }
                    DataTable phaseByPID = CallBLL.cz_phase_kl10_bll.GetPhaseByPID(phase_id);
                    if ((phaseByPID == null) || (phaseByPID.Rows.Count <= 0))
                    {
                        break;
                    }
                    phase = phaseByPID.Rows[0]["phase"].ToString();
                    return;
                }
                case 1:
                {
                    if (string.IsNullOrEmpty(phase_id) || phase_id.Equals("0"))
                    {
                        break;
                    }
                    DataTable table2 = CallBLL.cz_phase_cqsc_bll.GetPhaseByPID(phase_id);
                    if ((table2 == null) || (table2.Rows.Count <= 0))
                    {
                        break;
                    }
                    phase = table2.Rows[0]["phase"].ToString();
                    return;
                }
                case 2:
                {
                    if (string.IsNullOrEmpty(phase_id) || phase_id.Equals("0"))
                    {
                        break;
                    }
                    DataTable table3 = CallBLL.cz_phase_pk10_bll.GetPhaseByPID(phase_id);
                    if ((table3 == null) || (table3.Rows.Count <= 0))
                    {
                        break;
                    }
                    phase = table3.Rows[0]["phase"].ToString();
                    return;
                }
                case 3:
                {
                    if (string.IsNullOrEmpty(phase_id) || phase_id.Equals("0"))
                    {
                        break;
                    }
                    DataTable table4 = CallBLL.cz_phase_xync_bll.GetPhaseByPID(phase_id);
                    if ((table4 == null) || (table4.Rows.Count <= 0))
                    {
                        break;
                    }
                    phase = table4.Rows[0]["phase"].ToString();
                    return;
                }
                case 4:
                {
                    if (string.IsNullOrEmpty(phase_id) || phase_id.Equals("0"))
                    {
                        break;
                    }
                    DataTable table5 = CallBLL.cz_phase_jsk3_bll.GetPhaseByPID(phase_id);
                    if ((table5 == null) || (table5.Rows.Count <= 0))
                    {
                        break;
                    }
                    phase = table5.Rows[0]["phase"].ToString();
                    return;
                }
                case 5:
                {
                    if (string.IsNullOrEmpty(phase_id) || phase_id.Equals("0"))
                    {
                        break;
                    }
                    DataTable table6 = CallBLL.cz_phase_kl8_bll.GetPhaseByPID(phase_id);
                    if ((table6 == null) || (table6.Rows.Count <= 0))
                    {
                        break;
                    }
                    phase = table6.Rows[0]["phase"].ToString();
                    return;
                }
                case 6:
                {
                    if (string.IsNullOrEmpty(phase_id) || phase_id.Equals("0"))
                    {
                        break;
                    }
                    DataTable table7 = CallBLL.cz_phase_k8sc_bll.GetPhaseByPID(phase_id);
                    if ((table7 == null) || (table7.Rows.Count <= 0))
                    {
                        break;
                    }
                    phase = table7.Rows[0]["phase"].ToString();
                    return;
                }
                case 7:
                {
                    if (string.IsNullOrEmpty(phase_id) || phase_id.Equals("0"))
                    {
                        break;
                    }
                    DataTable table8 = CallBLL.cz_phase_pcdd_bll.GetPhaseByPID(phase_id);
                    if ((table8 == null) || (table8.Rows.Count <= 0))
                    {
                        break;
                    }
                    phase = table8.Rows[0]["phase"].ToString();
                    return;
                }
                case 8:
                {
                    if (string.IsNullOrEmpty(phase_id) || phase_id.Equals("0"))
                    {
                        break;
                    }
                    DataTable table10 = CallBLL.cz_phase_pkbjl_bll.GetPhaseByPID(phase_id);
                    if ((table10 == null) || (table10.Rows.Count <= 0))
                    {
                        break;
                    }
                    phase = table10.Rows[0]["phase"].ToString();
                    return;
                }
                case 9:
                {
                    if (string.IsNullOrEmpty(phase_id) || phase_id.Equals("0"))
                    {
                        break;
                    }
                    DataTable table9 = CallBLL.cz_phase_xyft5_bll.GetPhaseByPID(phase_id);
                    if ((table9 == null) || (table9.Rows.Count <= 0))
                    {
                        break;
                    }
                    phase = table9.Rows[0]["phase"].ToString();
                    return;
                }
                case 10:
                {
                    if (string.IsNullOrEmpty(phase_id) || phase_id.Equals("0"))
                    {
                        break;
                    }
                    DataTable table11 = CallBLL.cz_phase_jscar_bll.GetPhaseByPID(phase_id);
                    if ((table11 == null) || (table11.Rows.Count <= 0))
                    {
                        break;
                    }
                    phase = table11.Rows[0]["phase"].ToString();
                    return;
                }
                case 11:
                {
                    if (string.IsNullOrEmpty(phase_id) || phase_id.Equals("0"))
                    {
                        break;
                    }
                    DataTable table12 = CallBLL.cz_phase_speed5_bll.GetPhaseByPID(phase_id);
                    if ((table12 == null) || (table12.Rows.Count <= 0))
                    {
                        break;
                    }
                    phase = table12.Rows[0]["phase"].ToString();
                    return;
                }
                case 12:
                {
                    if (string.IsNullOrEmpty(phase_id) || phase_id.Equals("0"))
                    {
                        break;
                    }
                    DataTable table14 = CallBLL.cz_phase_jspk10_bll.GetPhaseByPID(phase_id);
                    if ((table14 == null) || (table14.Rows.Count <= 0))
                    {
                        break;
                    }
                    phase = table14.Rows[0]["phase"].ToString();
                    return;
                }
                case 13:
                {
                    if (string.IsNullOrEmpty(phase_id) || phase_id.Equals("0"))
                    {
                        break;
                    }
                    DataTable table13 = CallBLL.cz_phase_jscqsc_bll.GetPhaseByPID(phase_id);
                    if ((table13 == null) || (table13.Rows.Count <= 0))
                    {
                        break;
                    }
                    phase = table13.Rows[0]["phase"].ToString();
                    return;
                }
                case 14:
                {
                    if (string.IsNullOrEmpty(phase_id) || phase_id.Equals("0"))
                    {
                        break;
                    }
                    DataTable table15 = CallBLL.cz_phase_jssfc_bll.GetPhaseByPID(phase_id);
                    if ((table15 == null) || (table15.Rows.Count <= 0))
                    {
                        break;
                    }
                    phase = table15.Rows[0]["phase"].ToString();
                    return;
                }
                case 15:
                {
                    if (string.IsNullOrEmpty(phase_id) || phase_id.Equals("0"))
                    {
                        break;
                    }
                    DataTable table16 = CallBLL.cz_phase_jsft2_bll.GetPhaseByPID(phase_id);
                    if ((table16 == null) || (table16.Rows.Count <= 0))
                    {
                        break;
                    }
                    phase = table16.Rows[0]["phase"].ToString();
                    return;
                }
                case 0x10:
                {
                    if (string.IsNullOrEmpty(phase_id) || phase_id.Equals("0"))
                    {
                        break;
                    }
                    DataTable table17 = CallBLL.cz_phase_car168_bll.GetPhaseByPID(phase_id);
                    if ((table17 == null) || (table17.Rows.Count <= 0))
                    {
                        break;
                    }
                    phase = table17.Rows[0]["phase"].ToString();
                    return;
                }
                case 0x11:
                {
                    if (string.IsNullOrEmpty(phase_id) || phase_id.Equals("0"))
                    {
                        break;
                    }
                    DataTable table18 = CallBLL.cz_phase_ssc168_bll.GetPhaseByPID(phase_id);
                    if ((table18 == null) || (table18.Rows.Count <= 0))
                    {
                        break;
                    }
                    phase = table18.Rows[0]["phase"].ToString();
                    return;
                }
                case 0x12:
                {
                    if (string.IsNullOrEmpty(phase_id) || phase_id.Equals("0"))
                    {
                        break;
                    }
                    DataTable table19 = CallBLL.cz_phase_vrcar_bll.GetPhaseByPID(phase_id);
                    if ((table19 == null) || (table19.Rows.Count <= 0))
                    {
                        break;
                    }
                    phase = table19.Rows[0]["phase"].ToString();
                    return;
                }
                case 0x13:
                {
                    if (string.IsNullOrEmpty(phase_id) || phase_id.Equals("0"))
                    {
                        break;
                    }
                    DataTable table20 = CallBLL.cz_phase_vrssc_bll.GetPhaseByPID(phase_id);
                    if ((table20 == null) || (table20.Rows.Count <= 0))
                    {
                        break;
                    }
                    phase = table20.Rows[0]["phase"].ToString();
                    return;
                }
                case 20:
                {
                    if (string.IsNullOrEmpty(phase_id) || phase_id.Equals("0"))
                    {
                        break;
                    }
                    DataTable table21 = CallBLL.cz_phase_xyftoa_bll.GetPhaseByPID(phase_id);
                    if ((table21 == null) || (table21.Rows.Count <= 0))
                    {
                        break;
                    }
                    phase = table21.Rows[0]["phase"].ToString();
                    return;
                }
                case 0x15:
                {
                    if (string.IsNullOrEmpty(phase_id) || phase_id.Equals("0"))
                    {
                        break;
                    }
                    DataTable table22 = CallBLL.cz_phase_xyftsg_bll.GetPhaseByPID(phase_id);
                    if ((table22 == null) || (table22.Rows.Count <= 0))
                    {
                        break;
                    }
                    phase = table22.Rows[0]["phase"].ToString();
                    return;
                }
                case 0x16:
                    if (!string.IsNullOrEmpty(phase_id) && !phase_id.Equals("0"))
                    {
                        DataTable table23 = CallBLL.cz_phase_happycar_bll.GetPhaseByPID(phase_id);
                        if ((table23 != null) && (table23.Rows.Count > 0))
                        {
                            phase = table23.Rows[0]["phase"].ToString();
                        }
                    }
                    break;

                default:
                    return;
            }
        }

        protected void GetDataTipPlayname(string lid, string phase_id, string play_id, ref string play_name)
        {
            switch (int.Parse(lid))
            {
                case 0:
                    play_name = CallBLL.cz_play_kl10_bll.GetModel(int.Parse(play_id)).get_play_name();
                    return;

                case 1:
                    play_name = CallBLL.cz_play_cqsc_bll.GetModel(int.Parse(play_id)).get_play_name();
                    return;

                case 2:
                    play_name = CallBLL.cz_play_pk10_bll.GetModel(int.Parse(play_id)).get_play_name();
                    return;

                case 3:
                    play_name = CallBLL.cz_play_xync_bll.GetModel(int.Parse(play_id)).get_play_name();
                    return;

                case 4:
                    play_name = CallBLL.cz_play_jsk3_bll.GetModel(int.Parse(play_id)).get_play_name();
                    return;

                case 5:
                    play_name = CallBLL.cz_play_kl8_bll.GetModel(int.Parse(play_id)).get_play_name();
                    return;

                case 6:
                    play_name = CallBLL.cz_play_k8sc_bll.GetModel(int.Parse(play_id)).get_play_name();
                    return;

                case 7:
                    play_name = CallBLL.cz_play_pcdd_bll.GetModel(int.Parse(play_id)).get_play_name();
                    return;

                case 8:
                    play_name = CallBLL.cz_play_pkbjl_bll.GetModel(int.Parse(play_id)).get_play_name();
                    return;

                case 9:
                    play_name = CallBLL.cz_play_xyft5_bll.GetModel(int.Parse(play_id)).get_play_name();
                    return;

                case 10:
                    play_name = CallBLL.cz_play_jscar_bll.GetModel(int.Parse(play_id)).get_play_name();
                    return;

                case 11:
                    play_name = CallBLL.cz_play_speed5_bll.GetModel(int.Parse(play_id)).get_play_name();
                    return;

                case 12:
                    play_name = CallBLL.cz_play_jspk10_bll.GetModel(int.Parse(play_id)).get_play_name();
                    return;

                case 13:
                    play_name = CallBLL.cz_play_jscqsc_bll.GetModel(int.Parse(play_id)).get_play_name();
                    return;

                case 14:
                    play_name = CallBLL.cz_play_jssfc_bll.GetModel(int.Parse(play_id)).get_play_name();
                    return;

                case 15:
                    play_name = CallBLL.cz_play_jsft2_bll.GetModel(int.Parse(play_id)).get_play_name();
                    return;

                case 0x10:
                    play_name = CallBLL.cz_play_car168_bll.GetModel(int.Parse(play_id)).get_play_name();
                    return;

                case 0x11:
                    play_name = CallBLL.cz_play_ssc168_bll.GetModel(int.Parse(play_id)).get_play_name();
                    return;

                case 0x12:
                    play_name = CallBLL.cz_play_vrcar_bll.GetModel(int.Parse(play_id)).get_play_name();
                    return;

                case 0x13:
                    play_name = CallBLL.cz_play_vrssc_bll.GetModel(int.Parse(play_id)).get_play_name();
                    return;

                case 20:
                    play_name = CallBLL.cz_play_xyftoa_bll.GetModel(int.Parse(play_id)).get_play_name();
                    return;

                case 0x15:
                    play_name = CallBLL.cz_play_xyftsg_bll.GetModel(int.Parse(play_id)).get_play_name();
                    return;

                case 0x16:
                    play_name = CallBLL.cz_play_happycar_bll.GetModel(int.Parse(play_id)).get_play_name();
                    return;
            }
        }

        public DataTable GetFgsWTTable(int lotteryID)
        {
            DataTable table = null;
            string str = HttpContext.Current.Session["user_name"].ToString();
            agent_userinfo_session _session = HttpContext.Current.Session[str + "lottery_session_user_info"] as agent_userinfo_session;
            if (lotteryID.Equals(100))
            {
                if (_session.get_six_op_odds().Equals(1))
                {
                    table = this.FGS_WTChche(100);
                }
                return table;
            }
            if (!_session.get_kc_op_odds().Equals(1))
            {
                return table;
            }
            int? nullable = null;
            switch (lotteryID)
            {
                case 0:
                    nullable = 0;
                    break;

                case 1:
                    nullable = 1;
                    break;

                case 2:
                    nullable = 2;
                    break;

                case 3:
                    nullable = 3;
                    break;

                case 4:
                    nullable = 4;
                    break;

                case 5:
                    nullable = 5;
                    break;

                case 6:
                    nullable = 6;
                    break;

                case 7:
                    nullable = 7;
                    break;

                case 8:
                    nullable = 8;
                    break;

                case 9:
                    nullable = 9;
                    break;

                case 10:
                    nullable = 10;
                    break;

                case 11:
                    nullable = 11;
                    break;

                case 12:
                    nullable = 12;
                    break;

                case 13:
                    nullable = 13;
                    break;

                case 14:
                    nullable = 14;
                    break;

                case 15:
                    nullable = 15;
                    break;

                case 0x10:
                    nullable = 0x10;
                    break;

                case 0x11:
                    nullable = 0x11;
                    break;

                case 0x12:
                    nullable = 0x12;
                    break;

                case 0x13:
                    nullable = 0x13;
                    break;

                case 20:
                    nullable = 20;
                    break;

                case 0x15:
                    nullable = 0x15;
                    break;

                case 0x16:
                    nullable = 0x16;
                    break;
            }
            return this.FGS_WTChche(nullable);
        }

        protected string GetHAPPYCAR_PlayIDChange(string playType)
        {
            string str = "";
            string str2 = playType;
            if (str2 == null)
            {
                return str;
            }
            if (!(str2 == "q1"))
            {
                if (str2 != "q2")
                {
                    if (str2 != "q3")
                    {
                        return str;
                    }
                    return "24,25,26,27,28,29,30,31,32,33,34,35";
                }
            }
            else
            {
                return "1,2,3,4,5,6,7,8,36,37,38";
            }
            return "9,10,11,12,13,14,15,16,17,18,19,20,21,22,23";
        }

        private string GetHtml_BBCX(agent_userinfo_session uModel)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("        \"ut\": [");
            if (FileCacheHelper.get_IsViewNewReportMenu().Equals("1"))
            {
                builder.Append("            \"(新)報表查詢|ReportSearch/ReportNew.aspx\",");
            }
            builder.Append("            \"報表查詢|ReportSearch/Report.aspx\"");
            builder.Append("        ]");
            return builder.ToString();
        }

        private string GetHtml_GRGL(agent_userinfo_session uModel)
        {
            StringBuilder builder = new StringBuilder();
            if (uModel.get_u_type().Equals("zj"))
            {
                builder.Append("        \"ut\": [");
                builder.Append("            \"登陸日誌|ViewLog/LoginLog.aspx\",");
                builder.Append("            \"變更密碼|EditPwd.aspx|0\"");
                builder.Append("        ]");
            }
            else
            {
                builder.Append("        \"ut\": [");
                builder.Append("            \"信用資料|CreditInfo.aspx\",");
                if (uModel.get_users_child_session() == null)
                {
                    builder.Append("            \"登陸日誌|ViewLog/LoginLog.aspx\",");
                }
                else if (uModel.get_users_child_session().get_permissions_name().IndexOf("po_6_1") > -1)
                {
                    builder.Append("            \"登陸日誌|ViewLog/LoginLog.aspx\",");
                }
                if (uModel.get_u_type().Equals("fgs"))
                {
                    if ((uModel.get_users_child_session() == null) && (uModel.get_six_op_odds().Equals(1) || uModel.get_kc_op_odds().Equals(1)))
                    {
                        builder.Append("            \"操盤日誌|ViewLog/ViewFgsOptOddsLog.aspx\",");
                    }
                    else if (((uModel.get_users_child_session() != null) && (uModel.get_six_op_odds().Equals(1) || uModel.get_kc_op_odds().Equals(1))) && (uModel.get_users_child_session().get_permissions_name().IndexOf("po_5_3") > -1))
                    {
                        builder.Append("            \"操盤日誌|ViewLog/ViewFgsOptOddsLog.aspx\",");
                    }
                }
                builder.Append("            \"變更密碼|EditPwd.aspx|0\",");
                if (HttpContext.Current.Session["user_state"].ToString().Equals("0"))
                {
                    if (uModel.get_users_child_session() == null)
                    {
                        builder.Append("            \"自動補貨設定|/AutoLet/AutoLet_kc.aspx\",");
                    }
                    else if (uModel.get_users_child_session().get_permissions_name().IndexOf("po_5_2") > -1)
                    {
                        builder.Append("            \"自動補貨設定|/AutoLet/AutoLet_kc.aspx\",");
                    }
                }
                builder.Append("            \"自動補貨變更記錄|/ViewLog/ViewAutoSaleLog.aspx\"");
                if ((uModel.get_u_type().Equals("fgs") && (uModel.get_six_op_odds().Equals(1) || uModel.get_kc_op_odds().Equals(1))) && uModel.get_a_state().Equals(0))
                {
                    if (uModel.get_users_child_session() == null)
                    {
                        builder.Append("            ,\"微調列表|/OddsSet/OddsWT.aspx\"");
                    }
                    else if (uModel.get_users_child_session().get_permissions_name().IndexOf("po_5_3") > -1)
                    {
                        builder.Append("            ,\"微調列表|/OddsSet/OddsWT.aspx\"");
                    }
                }
                builder.Append("        ]");
            }
            return builder.ToString();
        }

        private string GetHtml_JSZD(agent_userinfo_session uModel)
        {
            StringBuilder builder = new StringBuilder();
            if (!string.IsNullOrEmpty(builder.ToString()))
            {
                builder.Append("        ,\"L_SIX\": [");
            }
            else
            {
                builder.Append("        \"L_SIX\": [");
            }
            if (uModel.get_u_type().Equals("zj"))
            {
                builder.Append("            \"特碼|Betimes_tmZX2.aspx\",");
            }
            else
            {
                builder.Append("            \"特碼|Betimes_tmZX2.aspx\",");
            }
            builder.Append("            \"正碼|Betimes_zm.aspx\",");
            builder.Append("            \"正碼特|Betimes_zmt1.aspx\",");
            builder.Append("            \"連碼|Betimes_lm.aspx\",");
            builder.Append("            \"不中|Betimes_bz.aspx\",");
            builder.Append("            \"正碼1-6|Betimes_zm1-6.aspx\",");
            builder.Append("            \"特碼生肖色波|Betimes_tmsxsb.aspx\",");
            builder.Append("            \"生肖尾數|Betimes_sxws.aspx\",");
            builder.Append("            \"半波|Betimes_bb.aspx\",");
            builder.Append("            \"六肖...連|Betimes_lxl.aspx\",");
            builder.Append("            \"龍虎-特碼攤子|Betimes_lhtmtz.aspx\",");
            builder.Append("            \"七碼五行|Betimes_qmwx.aspx\",");
            builder.Append("            \"帳單|../L_SIX/Bill.aspx|1\"");
            if (uModel.get_u_type().Equals("zj"))
            {
                builder.AppendFormat("            ,\"實時滾單|NewBet_six.aspx|1\"", 100);
            }
            builder.Append("        ]");
            if (!string.IsNullOrEmpty(builder.ToString()))
            {
                builder.Append("        ,\"L_KL10\": [");
            }
            else
            {
                builder.Append("        \"L_KL10\": [");
            }
            builder.Append("            \"第一球|Betimes_1.aspx\",");
            builder.Append("            \"第二球|Betimes_2.aspx\",");
            builder.Append("            \"第三球|Betimes_3.aspx\",");
            builder.Append("            \"第四球|Betimes_4.aspx\",");
            builder.Append("            \"第五球|Betimes_5.aspx\",");
            builder.Append("            \"第六球|Betimes_6.aspx\",");
            builder.Append("            \"第七球|Betimes_7.aspx\",");
            builder.Append("            \"第八球|Betimes_8.aspx\",");
            builder.Append("            \"總和、龍虎|Betimes_lh.aspx\",");
            builder.Append("            \"連碼|Betimes_lm.aspx\",");
            builder.Append("            \"帳單|../Bill_kc.aspx|1\",");
            builder.Append("            \"備份|../BillBackup_kc.aspx|1\"");
            if (uModel.get_u_type().Equals("zj"))
            {
                builder.AppendFormat("            ,\"實時滾單|../NewBet_kc.aspx|1\"", 0);
            }
            builder.Append("        ]");
            if (!string.IsNullOrEmpty(builder.ToString()))
            {
                builder.Append("        ,\"L_CQSC\": [");
            }
            else
            {
                builder.Append("        \"L_CQSC\": [");
            }
            builder.Append("            \"總項盤口|Betimes_zx.aspx\",");
            builder.Append("            \"帳單|../Bill_kc.aspx|1\",");
            builder.Append("            \"備份|../BillBackup_kc.aspx|1\"");
            if (uModel.get_u_type().Equals("zj"))
            {
                builder.AppendFormat("            ,\"實時滾單|../NewBet_kc.aspx|1\"", 1);
            }
            builder.Append("        ]");
            if (!string.IsNullOrEmpty(builder.ToString()))
            {
                builder.Append("        ,\"L_PK10\": [");
            }
            else
            {
                builder.Append("        \"L_PK10\": [");
            }
            builder.Append("            \"冠、亞軍 組合|Betimes_1.aspx\",");
            builder.Append("            \"三、四、伍、六名|Betimes_2.aspx\",");
            builder.Append("            \"七、八、九、十名|Betimes_3.aspx\",");
            builder.Append("            \"帳單|../Bill_kc.aspx|1\",");
            builder.Append("            \"備份|../BillBackup_kc.aspx|1\"");
            if (uModel.get_u_type().Equals("zj"))
            {
                builder.AppendFormat("            ,\"實時滾單|../NewBet_kc.aspx|1\"", 2);
            }
            builder.Append("        ]");
            if (!string.IsNullOrEmpty(builder.ToString()))
            {
                builder.Append("        ,\"L_XYNC\": [");
            }
            else
            {
                builder.Append("        \"L_XYNC\": [");
            }
            builder.Append("            \"第一球|Betimes_1.aspx\",");
            builder.Append("            \"第二球|Betimes_2.aspx\",");
            builder.Append("            \"第三球|Betimes_3.aspx\",");
            builder.Append("            \"第四球|Betimes_4.aspx\",");
            builder.Append("            \"第五球|Betimes_5.aspx\",");
            builder.Append("            \"第六球|Betimes_6.aspx\",");
            builder.Append("            \"第七球|Betimes_7.aspx\",");
            builder.Append("            \"第八球|Betimes_8.aspx\",");
            builder.Append("            \"總和、家禽野獸|Betimes_zh.aspx\",");
            builder.Append("            \"連碼|Betimes_lm.aspx\",");
            builder.Append("            \"帳單|../Bill_kc.aspx|1\",");
            builder.Append("            \"備份|../BillBackup_kc.aspx|1\"");
            if (uModel.get_u_type().Equals("zj"))
            {
                builder.AppendFormat("            ,\"實時滾單|../NewBet_kc.aspx|1\"", 3);
            }
            builder.Append("        ]");
            if (!string.IsNullOrEmpty(builder.ToString()))
            {
                builder.Append("        ,\"L_K3\": [");
            }
            else
            {
                builder.Append("        \"L_K3\": [");
            }
            builder.Append("            \"總項盤口|Betimes_zx.aspx\",");
            builder.Append("            \"帳單|../Bill_kc.aspx|1\",");
            builder.Append("            \"備份|../BillBackup_kc.aspx|1\"");
            if (uModel.get_u_type().Equals("zj"))
            {
                builder.AppendFormat("            ,\"實時滾單|../NewBet_kc.aspx|1\"", 4);
            }
            builder.Append("        ]");
            if (!string.IsNullOrEmpty(builder.ToString()))
            {
                builder.Append("        ,\"L_KL8\": [");
            }
            else
            {
                builder.Append("        \"L_KL8\": [");
            }
            builder.Append("            \"總和、比數、五行|Betimes_zh.aspx\",");
            builder.Append("            \"正碼|Betimes_zm.aspx\",");
            builder.Append("            \"帳單|../Bill_kc.aspx|1\",");
            builder.Append("            \"備份|../BillBackup_kc.aspx|1\"");
            if (uModel.get_u_type().Equals("zj"))
            {
                builder.AppendFormat("            ,\"實時滾單|../NewBet_kc.aspx|1\"", 5);
            }
            builder.Append("        ]");
            if (!string.IsNullOrEmpty(builder.ToString()))
            {
                builder.Append("        ,\"L_K8SC\": [");
            }
            else
            {
                builder.Append("        \"L_K8SC\": [");
            }
            builder.Append("            \"總項盤口|Betimes_zx.aspx\",");
            builder.Append("            \"帳單|../Bill_kc.aspx|1\",");
            builder.Append("            \"備份|../BillBackup_kc.aspx|1\"");
            if (uModel.get_u_type().Equals("zj"))
            {
                builder.AppendFormat("            ,\"實時滾單|../NewBet_kc.aspx|1\"", 6);
            }
            builder.Append("        ]");
            if (!string.IsNullOrEmpty(builder.ToString()))
            {
                builder.Append("        ,\"L_PCDD\": [");
            }
            else
            {
                builder.Append("        \"L_PCDD\": [");
            }
            builder.Append("            \"總項盤口|Betimes_zx.aspx\",");
            builder.Append("            \"特碼包三|Betimes_lm.aspx\",");
            builder.Append("            \"帳單|../Bill_kc.aspx|1\",");
            builder.Append("            \"備份|../BillBackup_kc.aspx|1\"");
            if (uModel.get_u_type().Equals("zj"))
            {
                builder.AppendFormat("            ,\"實時滾單|../NewBet_kc.aspx|1\"", 7);
            }
            builder.Append("        ]");
            if (!string.IsNullOrEmpty(builder.ToString()))
            {
                builder.Append("        ,\"L_XYFT5\": [");
            }
            else
            {
                builder.Append("        \"L_XYFT5\": [");
            }
            builder.Append("            \"冠、亞軍 組合|Betimes_1.aspx\",");
            builder.Append("            \"三、四、伍、六名|Betimes_2.aspx\",");
            builder.Append("            \"七、八、九、十名|Betimes_3.aspx\",");
            builder.Append("            \"帳單|../Bill_kc.aspx|1\",");
            builder.Append("            \"備份|../BillBackup_kc.aspx|1\"");
            if (uModel.get_u_type().Equals("zj"))
            {
                builder.AppendFormat("            ,\"實時滾單|../NewBet_kc.aspx|1\"", 9);
            }
            builder.Append("        ]");
            if (!string.IsNullOrEmpty(builder.ToString()))
            {
                builder.Append("        ,\"L_PKBJL\": [");
            }
            else
            {
                builder.Append("        \"L_PKBJL\": [");
            }
            builder.Append("            \"總項盤口|Betimes_1.aspx\",");
            builder.Append("            \"帳單|../Bill_kc.aspx|1\",");
            builder.Append("            \"備份|../BillBackup_kc.aspx|1\"");
            if (uModel.get_u_type().Equals("zj"))
            {
                builder.AppendFormat("            ,\"實時滾單|../NewBet_kc.aspx|1\"", 8);
            }
            builder.Append("        ]");
            if (!string.IsNullOrEmpty(builder.ToString()))
            {
                builder.Append("        ,\"L_JSCAR\": [");
            }
            else
            {
                builder.Append("        \"L_JSCAR\": [");
            }
            builder.Append("            \"冠、亞軍 組合|Betimes_1.aspx\",");
            builder.Append("            \"三、四、伍、六名|Betimes_2.aspx\",");
            builder.Append("            \"七、八、九、十名|Betimes_3.aspx\",");
            builder.Append("            \"帳單|../Bill_kc.aspx|1\",");
            builder.Append("            \"備份|../BillBackup_kc.aspx|1\"");
            if (uModel.get_u_type().Equals("zj"))
            {
                builder.AppendFormat("            ,\"實時滾單|../NewBet_kc.aspx|1\"", 10);
            }
            builder.Append("        ]");
            if (!string.IsNullOrEmpty(builder.ToString()))
            {
                builder.Append("        ,\"L_SPEED5\": [");
            }
            else
            {
                builder.Append("        \"L_SPEED5\": [");
            }
            builder.Append("            \"總項盤口|Betimes_zx.aspx\",");
            builder.Append("            \"帳單|../Bill_kc.aspx|1\",");
            builder.Append("            \"備份|../BillBackup_kc.aspx|1\"");
            if (uModel.get_u_type().Equals("zj"))
            {
                builder.AppendFormat("            ,\"實時滾單|../NewBet_kc.aspx|1\"", 11);
            }
            builder.Append("        ]");
            if (!string.IsNullOrEmpty(builder.ToString()))
            {
                builder.Append("        ,\"L_JSCQSC\": [");
            }
            else
            {
                builder.Append("        \"L_JSCQSC\": [");
            }
            builder.Append("            \"總項盤口|Betimes_zx.aspx\",");
            builder.Append("            \"帳單|../Bill_kc.aspx|1\",");
            builder.Append("            \"備份|../BillBackup_kc.aspx|1\"");
            if (uModel.get_u_type().Equals("zj"))
            {
                builder.AppendFormat("            ,\"實時滾單|../NewBet_kc.aspx|1\"", 13);
            }
            builder.Append("        ]");
            if (!string.IsNullOrEmpty(builder.ToString()))
            {
                builder.Append("        ,\"L_JSPK10\": [");
            }
            else
            {
                builder.Append("        \"L_JSPK10\": [");
            }
            builder.Append("            \"冠、亞軍 組合|Betimes_1.aspx\",");
            builder.Append("            \"三、四、伍、六名|Betimes_2.aspx\",");
            builder.Append("            \"七、八、九、十名|Betimes_3.aspx\",");
            builder.Append("            \"帳單|../Bill_kc.aspx|1\",");
            builder.Append("            \"備份|../BillBackup_kc.aspx|1\"");
            if (uModel.get_u_type().Equals("zj"))
            {
                builder.AppendFormat("            ,\"實時滾單|../NewBet_kc.aspx|1\"", 12);
            }
            builder.Append("        ]");
            if (!string.IsNullOrEmpty(builder.ToString()))
            {
                builder.Append("        ,\"L_JSSFC\": [");
            }
            else
            {
                builder.Append("        \"L_JSSFC\": [");
            }
            builder.Append("            \"第一球|Betimes_1.aspx\",");
            builder.Append("            \"第二球|Betimes_2.aspx\",");
            builder.Append("            \"第三球|Betimes_3.aspx\",");
            builder.Append("            \"第四球|Betimes_4.aspx\",");
            builder.Append("            \"第五球|Betimes_5.aspx\",");
            builder.Append("            \"第六球|Betimes_6.aspx\",");
            builder.Append("            \"第七球|Betimes_7.aspx\",");
            builder.Append("            \"第八球|Betimes_8.aspx\",");
            builder.Append("            \"總和、龍虎|Betimes_lh.aspx\",");
            builder.Append("            \"連碼|Betimes_lm.aspx\",");
            builder.Append("            \"帳單|../Bill_kc.aspx|1\",");
            builder.Append("            \"備份|../BillBackup_kc.aspx|1\"");
            if (uModel.get_u_type().Equals("zj"))
            {
                builder.AppendFormat("            ,\"實時滾單|../NewBet_kc.aspx|1\"", 14);
            }
            builder.Append("        ]");
            if (!string.IsNullOrEmpty(builder.ToString()))
            {
                builder.Append("        ,\"L_JSFT2\": [");
            }
            else
            {
                builder.Append("        \"L_JSFT2\": [");
            }
            builder.Append("            \"冠、亞軍 組合|Betimes_1.aspx\",");
            builder.Append("            \"三、四、伍、六名|Betimes_2.aspx\",");
            builder.Append("            \"七、八、九、十名|Betimes_3.aspx\",");
            builder.Append("            \"帳單|../Bill_kc.aspx|1\",");
            builder.Append("            \"備份|../BillBackup_kc.aspx|1\"");
            if (uModel.get_u_type().Equals("zj"))
            {
                builder.AppendFormat("            ,\"實時滾單|../NewBet_kc.aspx|1\"", 15);
            }
            builder.Append("        ]");
            if (!string.IsNullOrEmpty(builder.ToString()))
            {
                builder.Append("        ,\"L_CAR168\": [");
            }
            else
            {
                builder.Append("        \"L_CAR168\": [");
            }
            builder.Append("            \"冠、亞軍 組合|Betimes_1.aspx\",");
            builder.Append("            \"三、四、伍、六名|Betimes_2.aspx\",");
            builder.Append("            \"七、八、九、十名|Betimes_3.aspx\",");
            builder.Append("            \"帳單|../Bill_kc.aspx|1\",");
            builder.Append("            \"備份|../BillBackup_kc.aspx|1\"");
            if (uModel.get_u_type().Equals("zj"))
            {
                builder.AppendFormat("            ,\"實時滾單|../NewBet_kc.aspx|1\"", 0x10);
            }
            builder.Append("        ]");
            if (!string.IsNullOrEmpty(builder.ToString()))
            {
                builder.Append("        ,\"L_SSC168\": [");
            }
            else
            {
                builder.Append("        \"L_SSC168\": [");
            }
            builder.Append("            \"總項盤口|Betimes_zx.aspx\",");
            builder.Append("            \"帳單|../Bill_kc.aspx|1\",");
            builder.Append("            \"備份|../BillBackup_kc.aspx|1\"");
            if (uModel.get_u_type().Equals("zj"))
            {
                builder.AppendFormat("            ,\"實時滾單|../NewBet_kc.aspx|1\"", 0x11);
            }
            builder.Append("        ]");
            if (!string.IsNullOrEmpty(builder.ToString()))
            {
                builder.Append("        ,\"L_VRCAR\": [");
            }
            else
            {
                builder.Append("        \"L_VRCAR\": [");
            }
            builder.Append("            \"冠、亞軍 組合|Betimes_1.aspx\",");
            builder.Append("            \"三、四、伍、六名|Betimes_2.aspx\",");
            builder.Append("            \"七、八、九、十名|Betimes_3.aspx\",");
            builder.Append("            \"帳單|../Bill_kc.aspx|1\",");
            builder.Append("            \"備份|../BillBackup_kc.aspx|1\"");
            if (uModel.get_u_type().Equals("zj"))
            {
                builder.AppendFormat("            ,\"實時滾單|../NewBet_kc.aspx|1\"", 0x12);
            }
            builder.Append("        ]");
            if (!string.IsNullOrEmpty(builder.ToString()))
            {
                builder.Append("        ,\"L_VRSSC\": [");
            }
            else
            {
                builder.Append("        \"L_VRSSC\": [");
            }
            builder.Append("            \"總項盤口|Betimes_zx.aspx\",");
            builder.Append("            \"帳單|../Bill_kc.aspx|1\",");
            builder.Append("            \"備份|../BillBackup_kc.aspx|1\"");
            if (uModel.get_u_type().Equals("zj"))
            {
                builder.AppendFormat("            ,\"實時滾單|../NewBet_kc.aspx|1\"", 0x13);
            }
            builder.Append("        ]");
            if (!string.IsNullOrEmpty(builder.ToString()))
            {
                builder.Append("        ,\"L_XYFTOA\": [");
            }
            else
            {
                builder.Append("        \"L_XYFTOA\": [");
            }
            builder.Append("            \"冠、亞軍 組合|Betimes_1.aspx\",");
            builder.Append("            \"三、四、伍、六名|Betimes_2.aspx\",");
            builder.Append("            \"七、八、九、十名|Betimes_3.aspx\",");
            builder.Append("            \"帳單|../Bill_kc.aspx|1\",");
            builder.Append("            \"備份|../BillBackup_kc.aspx|1\"");
            if (uModel.get_u_type().Equals("zj"))
            {
                builder.AppendFormat("            ,\"實時滾單|../NewBet_kc.aspx|1\"", 20);
            }
            builder.Append("        ]");
            if (!string.IsNullOrEmpty(builder.ToString()))
            {
                builder.Append("        ,\"L_XYFTSG\": [");
            }
            else
            {
                builder.Append("        \"L_XYFTSG\": [");
            }
            builder.Append("            \"冠、亞軍 組合|Betimes_1.aspx\",");
            builder.Append("            \"三、四、伍、六名|Betimes_2.aspx\",");
            builder.Append("            \"七、八、九、十名|Betimes_3.aspx\",");
            builder.Append("            \"帳單|../Bill_kc.aspx|1\",");
            builder.Append("            \"備份|../BillBackup_kc.aspx|1\"");
            if (uModel.get_u_type().Equals("zj"))
            {
                builder.AppendFormat("            ,\"實時滾單|../NewBet_kc.aspx|1\"", 0x15);
            }
            builder.Append("        ]");
            if (!string.IsNullOrEmpty(builder.ToString()))
            {
                builder.Append("        ,\"L_HAPPYCAR\": [");
            }
            else
            {
                builder.Append("        \"L_HAPPYCAR\": [");
            }
            builder.Append("            \"冠、亞軍 組合|Betimes_1.aspx\",");
            builder.Append("            \"三、四、伍、六名|Betimes_2.aspx\",");
            builder.Append("            \"七、八、九、十名|Betimes_3.aspx\",");
            builder.Append("            \"帳單|../Bill_kc.aspx|1\",");
            builder.Append("            \"備份|../BillBackup_kc.aspx|1\"");
            if (uModel.get_u_type().Equals("zj"))
            {
                builder.AppendFormat("            ,\"實時滾單|../NewBet_kc.aspx|1\"", 0x16);
            }
            builder.Append("        ]");
            return builder.ToString();
        }

        private string GetHtml_NBGL(agent_userinfo_session uModel)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("        \"ut\": [");
            if (HttpContext.Current.Session["child_user_name"] == null)
            {
                builder.Append("            \"注單搜索|BillSearch.aspx\",");
            }
            else if (uModel.get_users_child_session().get_permissions_name().IndexOf("po_3_5") > -1)
            {
                builder.Append("            \"注單搜索|BillSearch.aspx\",");
            }
            int num = 0;
            if (HttpContext.Current.Session["user_state"].ToString().Equals(num.ToString()))
            {
                if (uModel.get_users_child_session() == null)
                {
                    if (!this.IsChildSync())
                    {
                        builder.Append("            \"彩種配置|LotteryConfig.aspx\",");
                    }
                    builder.Append("            \"系統初始設定|/SystemSet/SystemSet_kc.aspx\",");
                }
                else
                {
                    if ((uModel.get_users_child_session().get_permissions_name().IndexOf("po_2_2") > -1) && !this.IsChildSync())
                    {
                        builder.Append("            \"彩種配置|LotteryConfig.aspx\",");
                    }
                    if (uModel.get_users_child_session().get_permissions_name().IndexOf("po_3_2") > -1)
                    {
                        builder.Append("            \"系統初始設定|/SystemSet/SystemSet_kc.aspx\",");
                    }
                }
                if (FileCacheHelper.get_ManageZJProfit().Equals("1") && ((uModel.get_users_child_session() == null) || uModel.get_users_child_session().get_is_admin().Equals(1)))
                {
                    builder.Append("            \"總監盈利設置|/ManageZJProfit/Manage_ZJ_Profit.aspx\",");
                }
                if (uModel.get_users_child_session() == null)
                {
                    builder.Append("            \"交易設定|TradingSet.aspx\",");
                    builder.Append("            \"賠率設定|/OddsSet/OddsSet_kc.aspx\",");
                }
                else if (uModel.get_users_child_session().get_permissions_name().IndexOf("po_3_3") > -1)
                {
                    builder.Append("            \"交易設定|TradingSet.aspx\",");
                    builder.Append("            \"賠率設定|/OddsSet/OddsSet_kc.aspx\",");
                }
                if (uModel.get_u_type().Equals("fgs") && (uModel.get_six_op_odds().Equals(1) || uModel.get_six_op_odds().Equals(1)))
                {
                    if (uModel.get_users_child_session() == null)
                    {
                        builder.Append("            \"微調列表|/OddsSet/OddsWT.aspx\",");
                    }
                    else if (uModel.get_users_child_session().get_permissions_name().IndexOf("po_5_3") > -1)
                    {
                        builder.Append("            \"微調列表|/OddsSet/OddsWT.aspx\",");
                    }
                }
                if (uModel.get_users_child_session() == null)
                {
                    builder.Append("            \"站內消息管理|/NewsManage/news_list.aspx\",");
                }
                else if (uModel.get_users_child_session().get_permissions_name().IndexOf("po_3_4") > -1)
                {
                    builder.Append("            \"站內消息管理|/NewsManage/news_list.aspx\",");
                }
                if (uModel.get_users_child_session() == null)
                {
                    builder.Append("            \"獎期管理|/LotteryPeriod/AwardPeriod.aspx\",");
                }
                else if (uModel.get_users_child_session().get_permissions_name().IndexOf("po_3_1") > -1)
                {
                    builder.Append("            \"獎期管理|/LotteryPeriod/AwardPeriod.aspx\",");
                }
                builder.Append("            \"報表備份|/ReportBackupManage/ReportBackup.aspx|1\",");
                builder.Append("            \"注單備份|/BillBackupManage/BillBackup.aspx|1\",");
            }
            if (uModel.get_users_child_session() == null)
            {
                builder.Append("            \"操盤日誌|/ViewLog/LogOddsChange.aspx\",");
            }
            else if (uModel.get_users_child_session().get_permissions_name().IndexOf("po_3_1") > -1)
            {
                builder.Append("            \"操盤日誌|/ViewLog/LogOddsChange.aspx\",");
            }
            builder.Append("            \"系統日誌|/ViewLog/LogSystem.aspx\"");
            builder.Append("        ]");
            return builder.ToString();
        }

        private string GetHtml_YHGL(agent_userinfo_session uModel)
        {
            StringBuilder builder = new StringBuilder();
            if (!HttpContext.Current.Session["user_type"].ToString().Equals("zj"))
            {
                if (uModel.get_u_type().Equals("fgs"))
                {
                    builder.Append("        \"ut\": [");
                    builder.Append("            \"股东|account/gd_list.aspx\",");
                    builder.Append("            \"總代理|account/zd_list.aspx\",");
                    builder.Append("            \"代理|account/dl_list.aspx\",");
                    builder.Append("            \"會員|account/hy_list.aspx\"");
                    if (uModel.get_users_child_session() == null)
                    {
                        builder.Append("            ,\"子賬號|account/child_list.aspx\"");
                    }
                    builder.Append("        ]");
                }
                else if (uModel.get_u_type().Equals("gd"))
                {
                    builder.Append("        \"ut\": [");
                    builder.Append("            \"總代理|account/zd_list.aspx\",");
                    builder.Append("            \"代理|account/dl_list.aspx\",");
                    builder.Append("            \"會員|account/hy_list.aspx\"");
                    if (uModel.get_users_child_session() == null)
                    {
                        builder.Append("            ,\"子賬號|account/child_list.aspx\"");
                    }
                    builder.Append("        ]");
                }
                else if (uModel.get_u_type().Equals("zd"))
                {
                    builder.Append("        \"ut\": [");
                    builder.Append("            \"代理|account/dl_list.aspx\",");
                    builder.Append("            \"會員|account/hy_list.aspx\"");
                    if (uModel.get_users_child_session() == null)
                    {
                        builder.Append("            ,\"子賬號|account/child_list.aspx\"");
                    }
                    builder.Append("        ]");
                }
                else if (uModel.get_u_type().Equals("dl"))
                {
                    builder.Append("        \"ut\": [");
                    builder.Append("            \"會員|account/hy_list.aspx\"");
                    if (uModel.get_users_child_session() == null)
                    {
                        builder.Append("            ,\"子賬號|account/child_list.aspx\"");
                    }
                    builder.Append("        ]");
                }
                else
                {
                    builder.Append("        \"ut\": [ ]");
                }
            }
            else
            {
                builder.Append("        \"ut\": [");
                builder.Append("            \"分公司|account/fgs_list.aspx\",");
                builder.Append("            \"股东|account/gd_list.aspx\",");
                builder.Append("            \"總代理|account/zd_list.aspx\",");
                builder.Append("            \"代理|account/dl_list.aspx\",");
                builder.Append("            \"會員|account/hy_list.aspx\"");
                if (uModel.get_users_child_session() == null)
                {
                    builder.Append("            ,\"子賬號|account/child_list.aspx\"");
                }
                DataTable table = this.GetLotteryList().DefaultView.ToTable(true, new string[] { "master_id" });
                string str = "";
                foreach (DataRow row in table.Rows)
                {
                    int num = 1;
                    if (row["master_id"].ToString().Equals(num.ToString()))
                    {
                        str = "            ,\"出貨會員|account/filluser_list.aspx\"";
                        break;
                    }
                }
                if (uModel.get_users_child_session() != null)
                {
                    if (uModel.get_users_child_session().get_permissions_name().IndexOf("po_2_3") > -1)
                    {
                        builder.Append(str);
                    }
                }
                else
                {
                    builder.Append(str);
                }
                builder.Append("        ]");
            }
            return builder.ToString();
        }

        protected string GetIframeHeader(string id, string skin)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat(PageBase.GetHeaderByCache(id, "HtmlHeaderHint"), "title", skin);
            return builder.ToString().Replace("ЁJSVersionЁ", base.get_GetJSVersion()).Replace("ЁisOpenUpperЁ", base.get_GetPasswordLU());
        }

        protected string GetIframeHeader(string id, string skin, string title)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat(PageBase.GetHeaderByCache(id, "HtmlHeaderHint"), title, skin);
            return builder.ToString().Replace("ЁJSVersionЁ", base.get_GetJSVersion()).Replace("ЁisOpenUpperЁ", base.get_GetPasswordLU());
        }

        protected string GetIframeHeaderByShortcutData(string id)
        {
            return PageBase.GetHeaderByCache(id, "HtmlHeaderHint");
        }

        public string GetIsOperation(agent_userinfo_session uModel, int masterID)
        {
            string str = "0";
            if (uModel.get_u_type().Equals("fgs"))
            {
                string str2 = uModel.get_six_op_odds();
                string str3 = uModel.get_kc_op_odds();
                if (HttpContext.Current.Session["child_user_name"] == null)
                {
                    if (masterID.Equals(1))
                    {
                        str = str2;
                    }
                    if (masterID.Equals(2))
                    {
                        str = str3;
                    }
                    return str;
                }
                if (uModel.get_users_child_session().get_permissions_name().ToString().IndexOf("po_5_3") < 0)
                {
                    return "0";
                }
                if (masterID.Equals(1))
                {
                    str = str2;
                }
                if (masterID.Equals(2))
                {
                    str = str3;
                }
                return str;
            }
            if (uModel.get_u_type().Equals("zj"))
            {
                str = "1";
                if ((HttpContext.Current.Session["child_user_name"] != null) && (uModel.get_users_child_session().get_permissions_name().ToString().IndexOf("po_1_2") < 0))
                {
                    str = "0";
                }
                return str;
            }
            return "0";
        }

        protected string GetJSCAR_PlayIDChange(string playType)
        {
            string str = "";
            string str2 = playType;
            if (str2 == null)
            {
                return str;
            }
            if (!(str2 == "q1"))
            {
                if (str2 != "q2")
                {
                    if (str2 != "q3")
                    {
                        return str;
                    }
                    return "24,25,26,27,28,29,30,31,32,33,34,35";
                }
            }
            else
            {
                return "1,2,3,4,5,6,7,8,36,37,38";
            }
            return "9,10,11,12,13,14,15,16,17,18,19,20,21,22,23";
        }

        protected string GetJSCQSC_PlayIDChange(string playType)
        {
            return "1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21";
        }

        protected string GetJSFT2_PlayIDChange(string playType)
        {
            string str = "";
            string str2 = playType;
            if (str2 == null)
            {
                return str;
            }
            if (!(str2 == "q1"))
            {
                if (str2 != "q2")
                {
                    if (str2 != "q3")
                    {
                        return str;
                    }
                    return "24,25,26,27,28,29,30,31,32,33,34,35";
                }
            }
            else
            {
                return "1,2,3,4,5,6,7,8,36,37,38";
            }
            return "9,10,11,12,13,14,15,16,17,18,19,20,21,22,23";
        }

        protected string GetJSK3_PlayIDChange(string playType)
        {
            string str2;
            string str = "";
            if (((str2 = playType) != null) && (str2 == "zx"))
            {
                str = "58,59,60,61,62,63,64";
            }
            return str;
        }

        protected string GetJSPK10_PlayIDChange(string playType)
        {
            string str = "";
            string str2 = playType;
            if (str2 == null)
            {
                return str;
            }
            if (!(str2 == "q1"))
            {
                if (str2 != "q2")
                {
                    if (str2 != "q3")
                    {
                        return str;
                    }
                    return "24,25,26,27,28,29,30,31,32,33,34,35";
                }
            }
            else
            {
                return "1,2,3,4,5,6,7,8,36,37,38";
            }
            return "9,10,11,12,13,14,15,16,17,18,19,20,21,22,23";
        }

        protected string GetJSSFC_PlayIDChange(string playType)
        {
            switch (playType)
            {
                case "q1":
                    return "81,82,83,84,85,121,122";

                case "q2":
                    return "86,87,88,89,90,123,124";

                case "q3":
                    return "91,92,93,94,95,125,126";

                case "q4":
                    return "96,97,98,99,100,127,128";

                case "q5":
                    return "101,102,103,104,105,129,130";

                case "q6":
                    return "106,107,108,109,110,131,132";

                case "q7":
                    return "111,112,113,114,115,133,134";

                case "q8":
                    return "116,117,118,119,120,135,136";

                case "lh":
                    return "11,12,13,80";

                case "lm":
                    return "70,71,72,73,74,75,76,77,78,79";
            }
            return "";
        }

        protected string GetK8SC_PlayIDChange(string playType)
        {
            return "1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21";
        }

        protected string GetKCProfit()
        {
            string str = HttpContext.Current.Session["user_name"].ToString();
            agent_userinfo_session _session = HttpContext.Current.Session[str + "lottery_session_user_info"] as agent_userinfo_session;
            double num = 0.0;
            if (CacheHelper.GetCache("balance_kc_FileCacheKey" + this.Session["user_name"].ToString()) == null)
            {
                string str2 = "";
                string str3 = "";
                str2 = FileCacheHelper.ReadBalance_kc();
                num = CallBLL.cz_bet_kc_bll.Agent_Profit(str, _session.get_u_type());
                str3 = FileCacheHelper.ReadBalance_kc();
                if (str2.Trim() != str3.Trim())
                {
                    this.GetKCProfit();
                }
                CacheHelper.SetPublicFileCache("balance_kc_FileCacheKey" + this.Session["user_name"].ToString(), num, PageBase.GetPublicForderPath(base.get_KC_BalanceFileName()));
            }
            else
            {
                num = double.Parse(CacheHelper.GetCache("balance_kc_FileCacheKey" + this.Session["user_name"]).ToString());
            }
            return Utils.GetKeepDecimalNumber(1, num.ToString());
        }

        protected string GetKL10_PlayIDChange(string playType)
        {
            switch (playType)
            {
                case "q1":
                    return "81,82,83,84,85,121,122";

                case "q2":
                    return "86,87,88,89,90,123,124";

                case "q3":
                    return "91,92,93,94,95,125,126";

                case "q4":
                    return "96,97,98,99,100,127,128";

                case "q5":
                    return "101,102,103,104,105,129,130";

                case "q6":
                    return "106,107,108,109,110,131,132";

                case "q7":
                    return "111,112,113,114,115,133,134";

                case "q8":
                    return "116,117,118,119,120,135,136";

                case "lh":
                    return "11,12,13,80";

                case "lm":
                    return "70,71,72,73,74,75,76,77,78,79";
            }
            return "";
        }

        protected string GetKL8_PlayIDChange(string playType)
        {
            string str = "";
            string str2 = playType;
            if (str2 == null)
            {
                return str;
            }
            if (!(str2 == "zh"))
            {
                if (str2 != "zm")
                {
                    return str;
                }
            }
            else
            {
                return "66,67,68,69,70,71,72";
            }
            return "65";
        }

        public DataTable GetLotteryList()
        {
            if (CacheHelper.GetCache("cz_lottery_FileCacheKey") != null)
            {
                return (CacheHelper.GetCache("cz_lottery_FileCacheKey") as DataTable);
            }
            DataTable table = CallBLL.cz_lottery_bll.GetList().Tables[0];
            CacheHelper.SetCache("cz_lottery_FileCacheKey", table);
            CacheHelper.SetPublicFileCache("cz_lottery_FileCacheKey", table, PageBase.GetPublicForderPath(FileCacheHelper.get_LotteryCachesFileName()));
            return table;
        }

        protected string GetLotteryLogoImgHtml(string lottery_id, string flag)
        {
            string str = "<img id=\"game_logo\" src=\"\" alt=\"\">";
            string lotteryLogoImgUrl = this.GetLotteryLogoImgUrl(lottery_id);
            string str3 = string.Format("/images/logo/logo{0}.png", lottery_id);
            if (string.IsNullOrEmpty(lotteryLogoImgUrl))
            {
                return str;
            }
            int num = 100;
            if (lottery_id.Equals(num.ToString()))
            {
                return string.Format("<a href=\"javascript:;\" id=\"kjBtn\" title=\"開獎日期表\"><img id=\"game_logo\" src=\"{0}\" alt=\"{1}\" title=\"{2}\"></a>", str3, base.GetGameNameByID(lottery_id), base.GetGameNameByID(lottery_id));
            }
            return string.Format("<a href=\"{0}\" ><img id=\"game_logo\" src=\"{1}\" alt=\"{2}\" title=\"{3}\"></a>", new object[] { lotteryLogoImgUrl, str3, base.GetGameNameByID(lottery_id), base.GetGameNameByID(lottery_id) });
        }

        protected string GetLotteryLogoImgUrl(string lottery_id)
        {
            return string.Format(base.GetGameFolderFileByID(lottery_id) + "?lid={0}", lottery_id);
        }

        public string GetLotteryMasterSeleteString(string mid)
        {
            string str = "";
            DataTable lotteryList = this.GetLotteryList();
            DataRow[] rowArray = lotteryList.Select(string.Format(" id={0} ", 100));
            DataRow[] rowArray2 = lotteryList.Select(string.Format(" id<>{0} ", 100));
            int num = 1;
            if (mid.Equals(num.ToString()))
            {
                if (rowArray.Length > 0)
                {
                    str = str + string.Format("<option value='{0}' selected=selected>{1}</option>", 1, "香港⑥合彩");
                }
                if (rowArray2.Length > 0)
                {
                    str = str + string.Format("<option value='{0}'>{1}</option>", 2, "快彩");
                }
                return str;
            }
            if (rowArray.Length > 0)
            {
                str = str + string.Format("<option value='{0}'>{1}</option>", 1, "香港⑥合彩");
            }
            if (rowArray2.Length > 0)
            {
                str = str + string.Format("<option value='{0}' selected=selected>{1}</option>", 2, "快彩");
            }
            return str;
        }

        protected string GetNav()
        {
            string str = HttpContext.Current.Session["user_name"].ToString();
            agent_userinfo_session uModel = HttpContext.Current.Session[str + "lottery_session_user_info"] as agent_userinfo_session;
            StringBuilder builder = new StringBuilder();
            builder.Append("            {");
            if (HttpContext.Current.Session["user_state"].ToString().Equals("0"))
            {
                if (HttpContext.Current.Session["child_user_name"] != null)
                {
                    if (HttpContext.Current.Session["user_type"].ToString().Equals("zj"))
                    {
                        if (CallBLL.cz_users_child_bll.GetPermissionsName(HttpContext.Current.Session["child_user_name"].ToString()).IndexOf("po_1_1") > -1)
                        {
                            builder.Append("    \"即時注單\": {");
                            builder.Append(this.GetHtml_JSZD(uModel));
                            builder.Append("    },");
                        }
                    }
                    else if (CallBLL.cz_users_child_bll.GetPermissionsName(HttpContext.Current.Session["child_user_name"].ToString()).IndexOf("po_5_1") > -1)
                    {
                        builder.Append("    \"即時注單\": {");
                        builder.Append(this.GetHtml_JSZD(uModel));
                        builder.Append("    },");
                    }
                }
                else
                {
                    builder.Append("    \"即時注單\": {");
                    builder.Append(this.GetHtml_JSZD(uModel));
                    builder.Append("    },");
                }
            }
            if (HttpContext.Current.Session["user_state"].ToString().Equals("0"))
            {
                if (HttpContext.Current.Session["child_user_name"] != null)
                {
                    if (HttpContext.Current.Session["user_type"].ToString().Equals("zj"))
                    {
                        if (CallBLL.cz_users_child_bll.GetPermissionsName(HttpContext.Current.Session["child_user_name"].ToString()).IndexOf("po_2_1") > -1)
                        {
                            builder.Append("    \"用戶管理\": {");
                            builder.Append(this.GetHtml_YHGL(uModel));
                            builder.Append("    },");
                        }
                    }
                    else if (CallBLL.cz_users_child_bll.GetPermissionsName(HttpContext.Current.Session["child_user_name"].ToString()).IndexOf("po_6_1") > -1)
                    {
                        builder.Append("    \"用戶管理\": {");
                        builder.Append(this.GetHtml_YHGL(uModel));
                        builder.Append("    },");
                    }
                }
                else
                {
                    builder.Append("    \"用戶管理\": {");
                    builder.Append(this.GetHtml_YHGL(uModel));
                    builder.Append("    },");
                }
            }
            if (HttpContext.Current.Session["user_type"].ToString().Equals("zj"))
            {
                builder.Append("    \"内部管理\": {");
                builder.Append(this.GetHtml_NBGL(uModel));
                builder.Append("    },");
            }
            builder.Append("    \"個人管理\": {");
            builder.Append(this.GetHtml_GRGL(uModel));
            builder.Append("    },");
            builder.Append("    \"報表查詢\": {");
            builder.Append(this.GetHtml_BBCX(uModel));
            builder.Append("    },");
            builder.Append("    \"歷史開獎\": {");
            builder.Append("        \"ut\": [");
            builder.Append("            \"歷史開獎|/LotteryPeriod/HistoryLottery.aspx\"");
            builder.Append("        ]");
            builder.Append("    },");
            builder.Append("    \"站内消息\": {");
            builder.Append("        \"ut\": [");
            builder.Append("            \"站内消息|/NewsManage/NewsList.aspx\"");
            builder.Append("        ]");
            builder.Append("    },");
            builder.Append("    \"安全退出\": {");
            builder.Append("        \"ut\": [");
            builder.Append("            \"Quit.aspx\"");
            builder.Append("        ]");
            builder.Append("    }");
            builder.Append("}");
            return builder.ToString();
        }

        public Dictionary<string, object> GetOdds_KC(int lotteryID, DataTable oddsDT, string pk_kind, string openning, string playtype)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
            string str = HttpContext.Current.Session["user_name"].ToString();
            agent_userinfo_session _session = HttpContext.Current.Session[str + "lottery_session_user_info"] as agent_userinfo_session;
            if (string.IsNullOrEmpty(pk_kind))
            {
                pk_kind = "A";
            }
            else
            {
                pk_kind = pk_kind.ToUpper();
            }
            DataTable fgsWTTable = null;
            if (!_session.get_u_type().Equals("zj") && _session.get_kc_op_odds().Equals(1))
            {
                fgsWTTable = this.GetFgsWTTable(lotteryID);
            }
            string str2 = "";
            string s = "";
            string str4 = "";
            string str5 = "";
            string str6 = "";
            string str7 = "";
            List<double> collection = new List<double>();
            foreach (DataRow row in oddsDT.Rows)
            {
                str2 = row["odds_id"].ToString();
                string str8 = row["current_odds"].ToString();
                string str9 = row["max_odds"].ToString();
                string str10 = row["min_odds"].ToString();
                string str11 = row[pk_kind + "_diff"].ToString().Trim();
                if (str8 != "-")
                {
                    if (!_session.get_u_type().Equals("zj") && _session.get_kc_op_odds().Equals(1))
                    {
                        s = (double.Parse(str8) + double.Parse(str11)).ToString();
                        str4 = (double.Parse(str9) + double.Parse(str11)).ToString();
                        str5 = (double.Parse(str10) + double.Parse(str11)).ToString();
                        str6 = (double.Parse(str8) + double.Parse(str11)).ToString();
                        double num = double.Parse(fgsWTTable.Select(string.Format(" u_name='{0}' and odds_id={1} ", _session.get_fgs_name(), str2)).FirstOrDefault<DataRow>()["wt_value"].ToString());
                        s = (double.Parse(s) + num).ToString();
                        str4 = str6;
                        str5 = (double.Parse(str5) + num).ToString();
                        if (lotteryID.Equals(8))
                        {
                            int num10 = 0;
                            if (playtype.Equals(num10.ToString()))
                            {
                                if (str2.Equals("82005"))
                                {
                                    s = (double.Parse(s) + this.GetPlayTypeWTValue_PKBJL("82005")).ToString();
                                    str4 = (double.Parse(str4) + this.GetPlayTypeWTValue_PKBJL("82005")).ToString();
                                    double num2 = double.Parse(str9) + double.Parse(str11);
                                    if (double.Parse(str4) >= num2)
                                    {
                                        str4 = num2.ToString();
                                    }
                                }
                                if (str2.Equals("82006"))
                                {
                                    s = (double.Parse(s) + this.GetPlayTypeWTValue_PKBJL("82006")).ToString();
                                    str4 = (double.Parse(str4) + this.GetPlayTypeWTValue_PKBJL("82006")).ToString();
                                    double num3 = double.Parse(str9) + double.Parse(str11);
                                    if (double.Parse(str4) >= num3)
                                    {
                                        str4 = num3.ToString();
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        s = (double.Parse(str8) + double.Parse(str11)).ToString();
                        str4 = (double.Parse(str9) + double.Parse(str11)).ToString();
                        str5 = (double.Parse(str10) + double.Parse(str11)).ToString();
                        if (lotteryID.Equals(8))
                        {
                            int num18 = 0;
                            if (playtype.Equals(num18.ToString()))
                            {
                                if (str2.Equals("82005"))
                                {
                                    s = (double.Parse(s) + this.GetPlayTypeWTValue_PKBJL("82005")).ToString();
                                }
                                if (str2.Equals("82006"))
                                {
                                    s = (double.Parse(s) + this.GetPlayTypeWTValue_PKBJL("82006")).ToString();
                                }
                            }
                        }
                    }
                }
                else
                {
                    s = str8;
                    str4 = str9;
                    str5 = str10;
                }
                str7 = row["is_open"].ToString();
                dictionary2.Add("pl", s);
                if (!_session.get_u_type().Equals("zj") && _session.get_kc_op_odds().Equals("1"))
                {
                    dictionary2.Add("maxpl", str6);
                }
                else
                {
                    dictionary2.Add("maxpl", str4);
                }
                dictionary2.Add("minpl", str5);
                dictionary2.Add("plx", new List<double>(collection));
                if (openning.Equals("n"))
                {
                    dictionary2.Add("is_open", "0");
                }
                else
                {
                    dictionary2.Add("is_open", str7);
                }
                dictionary.Add(str2, new Dictionary<string, object>(dictionary2));
                dictionary2.Clear();
                collection.Clear();
            }
            return dictionary;
        }

        public void GetOdds_KC_EX(int lotteryID, string current_odds, string diff, ref string pl, string odds_id)
        {
            string str = HttpContext.Current.Session["user_name"].ToString();
            agent_userinfo_session _session = HttpContext.Current.Session[str + "lottery_session_user_info"] as agent_userinfo_session;
            if (_session.get_kc_op_odds().Equals(1))
            {
                double num = double.Parse(this.GetFgsWTTable(lotteryID).Select(string.Format(" u_name='{0}' and odds_id={1} ", _session.get_fgs_name(), odds_id)).FirstOrDefault<DataRow>()["wt_value"].ToString());
                pl = ((double.Parse(current_odds) + double.Parse(diff)) + num).ToString();
            }
            else
            {
                pl = (double.Parse(current_odds) + double.Parse(diff)).ToString();
            }
        }

        public void GetOdds_SIX(string odds_id, ref string pl, ref string maxpl, ref string minpl)
        {
            string str = pl;
            string str2 = maxpl;
            string str3 = minpl;
            string str4 = HttpContext.Current.Session["user_name"].ToString();
            agent_userinfo_session _session = HttpContext.Current.Session[str4 + "lottery_session_user_info"] as agent_userinfo_session;
            if (!_session.get_six_op_odds().Equals(1))
            {
                goto Label_022B;
            }
            DataRow row = this.GetFgsWTTable(100).Select(string.Format(" u_name='{0}' and odds_id={1} ", _session.get_fgs_name(), odds_id)).FirstOrDefault<DataRow>();
            double num = double.Parse(row["wt_value"].ToString());
            double num2 = double.Parse(row["wt_value2"].ToString());
            int index = 0;
        Label_020B:;
            if (index < pl.Split(new char[] { ',' }).Length)
            {
                if (index == 0)
                {
                    str = (double.Parse(pl.Split(new char[] { ',' })[index]) + num).ToString();
                    str2 = (double.Parse(maxpl.Split(new char[] { ',' })[index]) + num).ToString();
                    str3 = (double.Parse(minpl.Split(new char[] { ',' })[index]) + num).ToString();
                }
                else
                {
                    str = str + "," + ((double.Parse(pl.Split(new char[] { ',' })[index]) + num2)).ToString();
                    str2 = str2 + "," + ((double.Parse(maxpl.Split(new char[] { ',' })[index]) + num2)).ToString();
                    str3 = str3 + "," + ((double.Parse(minpl.Split(new char[] { ',' })[index]) + num2)).ToString();
                }
                index++;
                goto Label_020B;
            }
        Label_022B:
            pl = str;
            maxpl = str2;
            minpl = str3;
        }

        public void GetOdds_SIX_EX(string current_odds, string max_odds, string min_odds, string diff, ref string pl, ref string maxpl, ref string minpl)
        {
            string[] strArray = current_odds.Split(new char[] { ',' });
            string[] strArray2 = max_odds.Split(new char[] { ',' });
            string[] strArray3 = min_odds.Split(new char[] { ',' });
            string[] strArray4 = diff.Split(new char[] { ',' });
            if (strArray.Length > 1)
            {
                if (strArray4.Length > 1)
                {
                    pl = ((double.Parse(strArray[0]) + double.Parse(strArray4[0]))).ToString() + "," + ((double.Parse(strArray[1]) + double.Parse(strArray4[1]))).ToString();
                    maxpl = ((double.Parse(strArray2[0]) + double.Parse(strArray4[0]))).ToString() + "," + ((double.Parse(strArray2[1]) + double.Parse(strArray4[1]))).ToString();
                    minpl = ((double.Parse(strArray3[0]) + double.Parse(strArray4[0]))).ToString() + "," + ((double.Parse(strArray3[1]) + double.Parse(strArray4[1]))).ToString();
                }
                else
                {
                    pl = ((double.Parse(strArray[0]) + double.Parse(strArray4[0]))).ToString() + "," + ((double.Parse(strArray[1]) + double.Parse(strArray4[0]))).ToString();
                    maxpl = ((double.Parse(strArray2[0]) + double.Parse(strArray4[0]))).ToString() + "," + ((double.Parse(strArray2[1]) + double.Parse(strArray4[0]))).ToString();
                    minpl = ((double.Parse(strArray3[0]) + double.Parse(strArray4[0]))).ToString() + "," + ((double.Parse(strArray3[1]) + double.Parse(strArray4[0]))).ToString();
                }
            }
            else
            {
                pl = (double.Parse(current_odds) + double.Parse(diff)).ToString();
                maxpl = (double.Parse(max_odds) + double.Parse(diff)).ToString();
                minpl = (double.Parse(min_odds) + double.Parse(diff)).ToString();
            }
        }

        public void GetOdds_SIX_Sale(string odds_id, string current_odds, string diff, string isDoubleOdds, ref string pl1, ref string pl2, ref string pl1_2, ref string odds_zj)
        {
            string str = HttpContext.Current.Session["user_name"].ToString();
            agent_userinfo_session _session = HttpContext.Current.Session[str + "lottery_session_user_info"] as agent_userinfo_session;
            if (_session.get_six_op_odds().Equals(1))
            {
                DataRow row = this.GetFgsWTTable(100).Select(string.Format(" u_name='{0}' and odds_id={1} ", _session.get_fgs_name(), odds_id)).FirstOrDefault<DataRow>();
                double num = double.Parse(row["wt_value"].ToString());
                double num2 = double.Parse(row["wt_value2"].ToString());
                if (isDoubleOdds.Equals("1"))
                {
                    string[] strArray = current_odds.Split(new char[] { ',' });
                    double num3 = Convert.ToDouble(strArray[0]);
                    double num4 = Convert.ToDouble(strArray[0]);
                    if (strArray.Length > 1)
                    {
                        num4 = Convert.ToDouble(strArray[1]);
                    }
                    string[] strArray2 = diff.Split(new char[] { ',' });
                    double num5 = Convert.ToDouble(strArray2[0]);
                    double num6 = Convert.ToDouble(strArray2[0]);
                    if (strArray2.Length > 1)
                    {
                        num6 = Convert.ToDouble(strArray2[1]);
                    }
                    pl1 = ((num3 + num5) + num).ToString();
                    pl2 = ((num4 + num6) + num2).ToString();
                    pl1_2 = pl1 + "," + pl2;
                    odds_zj = ((num3 + num5)).ToString() + "," + ((num4 + num6)).ToString();
                }
                else
                {
                    pl1 = ((Convert.ToDouble(current_odds) + Convert.ToDouble(diff)) + num).ToString();
                    odds_zj = (Convert.ToDouble(current_odds) + Convert.ToDouble(diff)).ToString();
                }
            }
            else if (isDoubleOdds.Equals("1"))
            {
                string[] strArray3 = current_odds.Split(new char[] { ',' });
                double num7 = Convert.ToDouble(strArray3[0]);
                double num8 = Convert.ToDouble(strArray3[0]);
                if (strArray3.Length > 1)
                {
                    num8 = Convert.ToDouble(strArray3[1]);
                }
                string[] strArray4 = diff.Split(new char[] { ',' });
                double num9 = Convert.ToDouble(strArray4[0]);
                double num10 = Convert.ToDouble(strArray4[0]);
                if (strArray4.Length > 1)
                {
                    num10 = Convert.ToDouble(strArray4[1]);
                }
                pl1 = (num7 + num9).ToString();
                pl2 = (num8 + num10).ToString();
                pl1_2 = pl1 + "," + pl2;
                odds_zj = pl1_2;
            }
            else
            {
                pl1 = (Convert.ToDouble(current_odds) + Convert.ToDouble(diff)).ToString();
                odds_zj = pl1;
            }
        }

        public void GetOdds_SIX_SXWSL(string odds_id, ref string pl, ref string maxpl, ref string minpl, int zodiacID, string lxlwslType)
        {
            string str = pl;
            string str2 = maxpl;
            string str3 = minpl;
            string str4 = HttpContext.Current.Session["user_name"].ToString();
            agent_userinfo_session _session = HttpContext.Current.Session[str4 + "lottery_session_user_info"] as agent_userinfo_session;
            if (!_session.get_six_op_odds().Equals(1))
            {
                goto Label_045A;
            }
            DataRow row = this.GetFgsWTTable(100).Select(string.Format(" u_name='{0}' and odds_id={1} ", _session.get_fgs_name(), odds_id)).FirstOrDefault<DataRow>();
            double num = double.Parse(row["wt_value"].ToString());
            double num2 = double.Parse(row["wt_value2"].ToString());
            if (!lxlwslType.Equals("lxltype"))
            {
                int num4 = 0;
            Label_043A:;
                if (num4 < pl.Split(new char[] { ',' }).Length)
                {
                    if (num4 == 0)
                    {
                        str = (double.Parse(pl.Split(new char[] { ',' })[num4]) + num2).ToString();
                        str2 = (double.Parse(maxpl.Split(new char[] { ',' })[num4]) + num2).ToString();
                        str3 = (double.Parse(minpl.Split(new char[] { ',' })[num4]) + num2).ToString();
                    }
                    else
                    {
                        str = str + "," + ((double.Parse(pl.Split(new char[] { ',' })[num4]) + num)).ToString();
                        str2 = str2 + "," + ((double.Parse(maxpl.Split(new char[] { ',' })[num4]) + num)).ToString();
                        str3 = str3 + "," + ((double.Parse(minpl.Split(new char[] { ',' })[num4]) + num)).ToString();
                    }
                    num4++;
                    goto Label_043A;
                }
                goto Label_045A;
            }
            int index = 0;
        Label_02D0:;
            if (index < pl.Split(new char[] { ',' }).Length)
            {
                if (index == 0)
                {
                    str = (double.Parse(pl.Split(new char[] { ',' })[index]) + num).ToString();
                    str2 = (double.Parse(maxpl.Split(new char[] { ',' })[index]) + num).ToString();
                    str3 = (double.Parse(minpl.Split(new char[] { ',' })[index]) + num).ToString();
                }
                else if (index == zodiacID)
                {
                    str = str + "," + ((double.Parse(pl.Split(new char[] { ',' })[index]) + num2)).ToString();
                    str2 = str2 + "," + ((double.Parse(maxpl.Split(new char[] { ',' })[index]) + num2)).ToString();
                    str3 = str3 + "," + ((double.Parse(minpl.Split(new char[] { ',' })[index]) + num2)).ToString();
                }
                else
                {
                    str = str + "," + ((double.Parse(pl.Split(new char[] { ',' })[index]) + num)).ToString();
                    str2 = str2 + "," + ((double.Parse(maxpl.Split(new char[] { ',' })[index]) + num)).ToString();
                    str3 = str3 + "," + ((double.Parse(minpl.Split(new char[] { ',' })[index]) + num)).ToString();
                }
                index++;
                goto Label_02D0;
            }
        Label_045A:
            pl = str;
            maxpl = str2;
            minpl = str3;
        }

        public void GetOdds_SIX_WT(string odds_id, ref string pl, ref string maxpl, ref string minpl, string oddsindex)
        {
            string str = pl;
            string str2 = maxpl;
            string str3 = minpl;
            string str4 = HttpContext.Current.Session["user_name"].ToString();
            agent_userinfo_session _session = HttpContext.Current.Session[str4 + "lottery_session_user_info"] as agent_userinfo_session;
            if (_session.get_six_op_odds().Equals(1))
            {
                DataRow row = this.GetFgsWTTable(100).Select(string.Format(" u_name='{0}' and odds_id={1} ", _session.get_fgs_name(), odds_id)).FirstOrDefault<DataRow>();
                double num = 0.0;
                if (oddsindex.Equals("0"))
                {
                    num = double.Parse(row["wt_value"].ToString());
                }
                else
                {
                    num = double.Parse(row["wt_value2"].ToString());
                }
                str = (double.Parse(pl) + num).ToString();
                str2 = (double.Parse(maxpl) + num).ToString();
                str3 = (double.Parse(minpl) + num).ToString();
            }
            pl = str;
            maxpl = str2;
            minpl = str3;
        }

        public DataTable GetOpenNumber(int lotteryID, string numTable)
        {
            agent_userinfo_session _session = new agent_userinfo_session();
            string str = HttpContext.Current.Session["user_name"].ToString();
            _session = HttpContext.Current.Session[str + "lottery_session_user_info"] as agent_userinfo_session;
            DataTable table = null;
            string str2 = "";
            if (lotteryID.Equals(8))
            {
                str2 = "kc_agent_openball_FileCacheKey" + lotteryID.ToString() + numTable;
            }
            else
            {
                str2 = "kc_agent_openball_FileCacheKey" + lotteryID.ToString();
            }
            if (CacheHelper.GetCache(str2) != null)
            {
                return (CacheHelper.GetCache(str2) as DataTable);
            }
            switch (lotteryID)
            {
                case 0:
                    table = CallBLL.cz_phase_kl10_bll.NewOpenPhase(str, _session.get_u_type().Trim());
                    break;

                case 1:
                    table = CallBLL.cz_phase_cqsc_bll.NewOpenPhase(str, _session.get_u_type().Trim());
                    break;

                case 2:
                    table = CallBLL.cz_phase_pk10_bll.NewOpenPhase(str, _session.get_u_type().Trim());
                    break;

                case 3:
                    table = CallBLL.cz_phase_xync_bll.NewOpenPhase(str, _session.get_u_type().Trim());
                    break;

                case 4:
                    table = CallBLL.cz_phase_jsk3_bll.NewOpenPhase(str, _session.get_u_type().Trim());
                    break;

                case 5:
                    table = CallBLL.cz_phase_kl8_bll.NewOpenPhase(str, _session.get_u_type().Trim());
                    break;

                case 6:
                    table = CallBLL.cz_phase_k8sc_bll.NewOpenPhase(str, _session.get_u_type().Trim());
                    break;

                case 7:
                    table = CallBLL.cz_phase_pcdd_bll.NewOpenPhase(str, _session.get_u_type().Trim());
                    break;

                case 8:
                    table = CallBLL.cz_phase_pkbjl_bll.NewOpenPhase(str, _session.get_u_type().Trim(), numTable);
                    break;

                case 9:
                    table = CallBLL.cz_phase_xyft5_bll.NewOpenPhase(str, _session.get_u_type().Trim());
                    break;

                case 10:
                    table = CallBLL.cz_phase_jscar_bll.NewOpenPhase(str, _session.get_u_type().Trim());
                    break;

                case 11:
                    table = CallBLL.cz_phase_speed5_bll.NewOpenPhase(str, _session.get_u_type().Trim());
                    break;

                case 12:
                    table = CallBLL.cz_phase_jspk10_bll.NewOpenPhase(str, _session.get_u_type().Trim());
                    break;

                case 13:
                    table = CallBLL.cz_phase_jscqsc_bll.NewOpenPhase(str, _session.get_u_type().Trim());
                    break;

                case 14:
                    table = CallBLL.cz_phase_jssfc_bll.NewOpenPhase(str, _session.get_u_type().Trim());
                    break;

                case 15:
                    table = CallBLL.cz_phase_jsft2_bll.NewOpenPhase(str, _session.get_u_type().Trim());
                    break;

                case 0x10:
                    table = CallBLL.cz_phase_car168_bll.NewOpenPhase(str, _session.get_u_type().Trim());
                    break;

                case 0x11:
                    table = CallBLL.cz_phase_ssc168_bll.NewOpenPhase(str, _session.get_u_type().Trim());
                    break;

                case 0x12:
                    table = CallBLL.cz_phase_vrcar_bll.NewOpenPhase(str, _session.get_u_type().Trim());
                    break;

                case 0x13:
                    table = CallBLL.cz_phase_vrssc_bll.NewOpenPhase(str, _session.get_u_type().Trim());
                    break;

                case 20:
                    table = CallBLL.cz_phase_xyftoa_bll.NewOpenPhase(str, _session.get_u_type().Trim());
                    break;

                case 0x15:
                    table = CallBLL.cz_phase_xyftsg_bll.NewOpenPhase(str, _session.get_u_type().Trim());
                    break;

                case 0x16:
                    table = CallBLL.cz_phase_happycar_bll.NewOpenPhase(str, _session.get_u_type().Trim());
                    break;
            }
            CacheHelper.SetPublicFileCacheDependency(str2, table, PageBase.GetPublicForderPath(base.get_KC_BalanceFileName()));
            return table;
        }

        public void GetOperate_KC(int lotteryID, string odds_id, ref double wtvalue)
        {
            double num = 0.0;
            string str = HttpContext.Current.Session["user_name"].ToString();
            agent_userinfo_session _session = HttpContext.Current.Session[str + "lottery_session_user_info"] as agent_userinfo_session;
            if (_session.get_kc_op_odds().Equals(1))
            {
                num = double.Parse(this.GetFgsWTTable(lotteryID).Select(string.Format(" u_name='{0}' and odds_id={1} ", _session.get_fgs_name(), odds_id)).FirstOrDefault<DataRow>()["wt_value"].ToString());
            }
            else
            {
                num = 0.0;
            }
            wtvalue = num;
        }

        public void GetOperate_SIX(string odds_id, ref double wtvalue)
        {
            double num = 0.0;
            string str = HttpContext.Current.Session["user_name"].ToString();
            agent_userinfo_session _session = HttpContext.Current.Session[str + "lottery_session_user_info"] as agent_userinfo_session;
            if (_session.get_six_op_odds().Equals(1))
            {
                num = double.Parse(this.GetFgsWTTable(100).Select(string.Format(" u_name='{0}' and odds_id={1} ", _session.get_fgs_name(), odds_id)).FirstOrDefault<DataRow>()["wt_value"].ToString());
            }
            else
            {
                num = 0.0;
            }
            wtvalue = num;
        }

        public void GetOperate_SIX(string odds_id, ref double wtvalue1, ref double wtvalue2)
        {
            string str = HttpContext.Current.Session["user_name"].ToString();
            agent_userinfo_session _session = HttpContext.Current.Session[str + "lottery_session_user_info"] as agent_userinfo_session;
            double num = 0.0;
            double num2 = 0.0;
            if (_session.get_u_type().Equals("fgs") && _session.get_six_op_odds().Equals(1))
            {
                DataRow row = this.GetFgsWTTable(100).Select(string.Format(" u_name='{0}' and odds_id={1} ", _session.get_fgs_name(), odds_id)).FirstOrDefault<DataRow>();
                num = double.Parse(row["wt_value"].ToString());
                num2 = double.Parse(row["wt_value2"].ToString());
            }
            wtvalue1 = num;
            wtvalue2 = num2;
        }

        protected string GetOutUserNameList(string u_name)
        {
            string str = "";
            DataTable outUserNameList = CallBLL.cz_users_bll.GetOutUserNameList(u_name);
            if (outUserNameList != null)
            {
                for (int i = 0; i < outUserNameList.Rows.Count; i++)
                {
                    string str2 = outUserNameList.Rows[i]["u_name"].ToString();
                    if (i == 0)
                    {
                        str = str + str2;
                    }
                    else
                    {
                        str = str + "," + str2;
                    }
                }
            }
            return str;
        }

        public string GetParentChildStr(string u_name, string u_type, int flag)
        {
            string str = u_name;
            string str2 = HttpContext.Current.Session["user_name"].ToString();
            agent_userinfo_session _session = HttpContext.Current.Session[str2 + "lottery_session_user_info"] as agent_userinfo_session;
            if (flag.Equals(0))
            {
                if (_session.get_u_name().Equals(u_name) && (_session.get_users_child_session() != null))
                {
                    return "-";
                }
                return u_name;
            }
            if (flag.Equals(1))
            {
                if (_session.get_users_child_session() != null)
                {
                    if (u_type.Equals("zj"))
                    {
                        return "總監(-)";
                    }
                    return "(-)";
                }
                if (u_type.Equals("zj"))
                {
                    return ("總監(" + u_name + ")");
                }
                return ("(" + u_name + ")");
            }
            if (flag.Equals(2))
            {
                if (_session.get_u_type().Equals(u_type) && (_session.get_users_child_session() != null))
                {
                    return "-";
                }
                return u_name;
            }
            if (flag.Equals(3))
            {
                if (u_name.Equals(_session.get_u_name()) && (_session.get_users_child_session() != null))
                {
                    return "-";
                }
                return u_name;
            }
            return u_name;
        }

        protected string GetPCDD_PlayIDChange(string playType)
        {
            string str = "";
            if (playType.Equals("zx"))
            {
                str = "71001,71002,71003,71004,71005,71006,71007,71008,71009,71010,71011,71012,71013";
            }
            if (playType.Equals("lm"))
            {
                str = "71014";
            }
            return str;
        }

        protected string GetPK10_PlayIDChange(string playType)
        {
            string str = "";
            string str2 = playType;
            if (str2 == null)
            {
                return str;
            }
            if (!(str2 == "q1"))
            {
                if (str2 != "q2")
                {
                    if (str2 != "q3")
                    {
                        return str;
                    }
                    return "24,25,26,27,28,29,30,31,32,33,34,35";
                }
            }
            else
            {
                return "1,2,3,4,5,6,7,8,36,37,38";
            }
            return "9,10,11,12,13,14,15,16,17,18,19,20,21,22,23";
        }

        protected string GetPKBJL_PlayIDChange(string playType)
        {
            return "81001,81002,81003,81004";
        }

        public string Getpkpl(string a_pk, string a_pl, string abcpl)
        {
            double num = 0.0;
            num = double.Parse(a_pl) + double.Parse(abcpl);
            return num.ToString();
        }

        public double GetPlayTypeWTValue_PKBJL(string odds_id)
        {
            DataTable playTypeValue = CallBLL.cz_play_pkbjl_bll.GetPlayTypeValue(odds_id);
            if ((playTypeValue != null) && (playTypeValue.Rows.Count > 0))
            {
                return double.Parse(playTypeValue.Rows[0]["wt_value"].ToString());
            }
            return 0.0;
        }

        private agent_userinfo_session GetSessionLogin()
        {
            cz_users _users = CallBLL.cz_users_bll.AgentLogin(HttpContext.Current.Session["user_name"].ToString());
            cz_users_child _child = null;
            if (HttpContext.Current.Session["child_user_name"] != null)
            {
                _child = CallBLL.cz_users_child_bll.AgentLogin(HttpContext.Current.Session["child_user_name"].ToString());
            }
            agent_userinfo_session _session = new agent_userinfo_session();
            _session.set_u_id(_users.get_u_id());
            _session.set_u_name(_users.get_u_name().Trim());
            _session.set_u_psw(_users.get_u_psw().Trim());
            _session.set_u_nicker(_users.get_u_nicker().Trim());
            _session.set_u_skin(_users.get_u_skin().Trim());
            _session.set_sup_name(_users.get_sup_name().Trim());
            _session.set_u_type(_users.get_u_type().Trim());
            _session.set_su_type(_users.get_su_type().Trim());
            _session.set_a_state(_users.get_a_state());
            _session.set_six_kind(_users.get_six_kind().Trim().ToUpper());
            _session.set_kc_kind(_users.get_kc_kind().Trim().ToUpper());
            _session.set_allow_sale(_users.get_allow_sale());
            _session.set_kc_allow_sale(_users.get_kc_allow_sale());
            _session.set_negative_sale(_users.get_negative_sale());
            _session.set_users_child_session(_child);
            if (!_session.get_allow_view_report().HasValue)
            {
                _session.set_allow_view_report(0);
            }
            else
            {
                _session.set_allow_view_report(_users.get_allow_view_report());
            }
            if (_child != null)
            {
                _child.set_salt("");
            }
            DataTable zJInfo = CallBLL.cz_users_bll.GetZJInfo();
            if (zJInfo != null)
            {
                _session.set_zjname(zJInfo.Rows[0]["u_name"].ToString().Trim());
            }
            if (!_session.get_u_type().ToLower().Equals("zj"))
            {
                cz_rate_kc rateKCByUserName = CallBLL.cz_rate_kc_bll.GetRateKCByUserName(_session.get_u_name());
                _session.set_fgs_name(rateKCByUserName.get_fgs_name());
                _session.set_gd_name(rateKCByUserName.get_gd_name());
                _session.set_zd_name(rateKCByUserName.get_zd_name());
                _session.set_dl_name(rateKCByUserName.get_dl_name());
                DataTable userOpOdds = CallBLL.cz_rate_kc_bll.GetUserOpOdds(_session.get_u_name());
                if (userOpOdds == null)
                {
                    return _session;
                }
                if ((userOpOdds.Rows[0]["six_op_odds"] != null) && (userOpOdds.Rows[0]["six_op_odds"].ToString() != ""))
                {
                    _session.set_six_op_odds(new int?(int.Parse(userOpOdds.Rows[0]["six_op_odds"].ToString())));
                }
                if ((userOpOdds.Rows[0]["kc_op_odds"] != null) && (userOpOdds.Rows[0]["kc_op_odds"].ToString() != ""))
                {
                    _session.set_kc_op_odds(new int?(int.Parse(userOpOdds.Rows[0]["kc_op_odds"].ToString())));
                }
            }
            return _session;
        }

        protected string GetSIX_PlayIDChange(string playType)
        {
            switch (playType)
            {
                case "tma":
                    return "91001,91003,91004,91005,91007,91038";

                case "tmb":
                    return "91002,91003,91004,91005,91007,91038";

                case "tmab":
                    return "91001,91002,91003,91004,91005,91007,91038";

                case "tmzx1":
                    return "91001,91002,91003,91004,91005,91006,91007,91008,91057,91030,91038,91039";

                case "tmzx2":
                    return "91001,91002,91003,91004,91005,91006,91007,91008,91057,91030,91038,91039";

                case "tmzx2_2":
                    return "91001,91002,91003,91004,91005,91006,91007,91008,91057,91030,91038,91039";

                case "tmzx3":
                    return "91001,91039";

                case "zm":
                    return "91009,91023,91024";

                case "zmt1":
                    return "91010";

                case "zmt2":
                    return "91025";

                case "zmt3":
                    return "91026";

                case "zmt4":
                    return "91027";

                case "zmt5":
                    return "91028";

                case "zmt6":
                    return "91029";

                case "lm":
                    return "91016,91017,91018,91019,91020,91040";

                case "lm_b":
                    return "91060,91061,91062,91063,91064,91065";

                case "bz":
                    return "91037,91047,91048,91049,91050,91051";

                case "zm1-6":
                    return "91011,91012,91013,91014";

                case "tmsxsb":
                    return "91006,91007";

                case "sxws":
                    return "91021,91022";

                case "sxwsbz":
                    return "91054,91055";

                case "bb":
                    return "91008,91057";

                case "lxl":
                    return "91030,91031,91032,91033,91034,91035,91036,91058,91059";

                case "lhtmtz":
                    return "91039,91041,91042,91043,91044,91045,91046";

                case "qmwx":
                    return "91052,91053,91056";
            }
            return "";
        }

        protected string GetSkinList(string currentSkin)
        {
            ReturnResult result = new ReturnResult();
            result.set_data(null);
            result.set_success(0);
            try
            {
                string[] directories = Directory.GetDirectories(base.Server.MapPath("") + @"\Styles");
                Dictionary<string, string> dictionary = new Dictionary<string, string>();
                for (int i = 0; i < directories.Length; i++)
                {
                    string name = new DirectoryInfo(directories[i]).Name;
                    if (name.ToLower().Equals(currentSkin.ToLower()))
                    {
                        dictionary.Add(name, "1");
                    }
                    else
                    {
                        dictionary.Add(name, "0");
                    }
                }
                result.set_success(200);
                result.set_tipinfo(PageBase.GetMessageByCache("u100002", "MessageHint"));
                result.set_data(dictionary);
            }
            catch
            {
                result.set_tipinfo(PageBase.GetMessageByCache("u100003", "MessageHint"));
            }
            return JsonHandle.ObjectToJson(result);
        }

        protected string GetSPEED5_PlayIDChange(string playType)
        {
            return "1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21";
        }

        protected string GetSSC168_PlayIDChange(string playType)
        {
            return "1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21";
        }

        protected string GetSUType(string u_name)
        {
            cz_users userInfoByUName = CallBLL.cz_users_bll.GetUserInfoByUName(u_name);
            if (userInfoByUName != null)
            {
                return userInfoByUName.get_su_type();
            }
            return "";
        }

        public DataTable GetUserDrawback_car168()
        {
            DataTable cache = CacheHelper.GetCache("kc_drawback_FileCacheKey_car168" + this.Session["user_name"].ToString()) as DataTable;
            if (cache == null)
            {
                this.UserDrawback_car168();
                cache = CacheHelper.GetCache("kc_drawback_FileCacheKey_car168" + this.Session["user_name"].ToString()) as DataTable;
            }
            return cache;
        }

        public DataTable GetUserDrawback_cqsc()
        {
            DataTable cache = CacheHelper.GetCache("kc_drawback_FileCacheKey_cqsc" + this.Session["user_name"].ToString()) as DataTable;
            if (cache == null)
            {
                this.UserDrawback_cqsc();
                cache = CacheHelper.GetCache("kc_drawback_FileCacheKey_cqsc" + this.Session["user_name"].ToString()) as DataTable;
            }
            return cache;
        }

        public DataTable GetUserDrawback_happycar()
        {
            DataTable cache = CacheHelper.GetCache("kc_drawback_FileCacheKey_happycar" + this.Session["user_name"].ToString()) as DataTable;
            if (cache == null)
            {
                this.UserDrawback_happycar();
                cache = CacheHelper.GetCache("kc_drawback_FileCacheKey_happycar" + this.Session["user_name"].ToString()) as DataTable;
            }
            return cache;
        }

        public DataTable GetUserDrawback_jscar()
        {
            DataTable cache = CacheHelper.GetCache("kc_drawback_FileCacheKey_jscar" + this.Session["user_name"].ToString()) as DataTable;
            if (cache == null)
            {
                this.UserDrawback_jscar();
                cache = CacheHelper.GetCache("kc_drawback_FileCacheKey_jscar" + this.Session["user_name"].ToString()) as DataTable;
            }
            return cache;
        }

        public DataTable GetUserDrawback_jscqsc()
        {
            DataTable cache = CacheHelper.GetCache("kc_drawback_FileCacheKey_jscqsc" + this.Session["user_name"].ToString()) as DataTable;
            if (cache == null)
            {
                this.UserDrawback_jscqsc();
                cache = CacheHelper.GetCache("kc_drawback_FileCacheKey_jscqsc" + this.Session["user_name"].ToString()) as DataTable;
            }
            return cache;
        }

        public DataTable GetUserDrawback_jsft2()
        {
            DataTable cache = CacheHelper.GetCache("kc_drawback_FileCacheKey_jsft2" + this.Session["user_name"].ToString()) as DataTable;
            if (cache == null)
            {
                this.UserDrawback_jsft2();
                cache = CacheHelper.GetCache("kc_drawback_FileCacheKey_jsft2" + this.Session["user_name"].ToString()) as DataTable;
            }
            return cache;
        }

        public DataTable GetUserDrawback_jsk3()
        {
            DataTable cache = CacheHelper.GetCache("kc_drawback_FileCacheKey_jsk3" + this.Session["user_name"].ToString()) as DataTable;
            if (cache == null)
            {
                this.UserDrawback_jsk3();
                cache = CacheHelper.GetCache("kc_drawback_FileCacheKey_jsk3" + this.Session["user_name"].ToString()) as DataTable;
            }
            return cache;
        }

        public DataTable GetUserDrawback_jspk10()
        {
            DataTable cache = CacheHelper.GetCache("kc_drawback_FileCacheKey_jspk10" + this.Session["user_name"].ToString()) as DataTable;
            if (cache == null)
            {
                this.UserDrawback_jspk10();
                cache = CacheHelper.GetCache("kc_drawback_FileCacheKey_jspk10" + this.Session["user_name"].ToString()) as DataTable;
            }
            return cache;
        }

        public DataTable GetUserDrawback_jssfc()
        {
            DataTable cache = CacheHelper.GetCache("kc_drawback_FileCacheKey_jssfc" + this.Session["user_name"].ToString()) as DataTable;
            if (cache == null)
            {
                this.UserDrawback_jssfc();
                cache = CacheHelper.GetCache("kc_drawback_FileCacheKey_jssfc" + this.Session["user_name"].ToString()) as DataTable;
            }
            return cache;
        }

        public DataTable GetUserDrawback_k8sc()
        {
            DataTable cache = CacheHelper.GetCache("kc_drawback_FileCacheKey_k8sc" + this.Session["user_name"].ToString()) as DataTable;
            if (cache == null)
            {
                this.UserDrawback_k8sc();
                cache = CacheHelper.GetCache("kc_drawback_FileCacheKey_k8sc" + this.Session["user_name"].ToString()) as DataTable;
            }
            return cache;
        }

        public DataTable GetUserDrawback_kl10()
        {
            DataTable cache = CacheHelper.GetCache("kc_drawback_FileCacheKey_kl10" + this.Session["user_name"].ToString()) as DataTable;
            if (cache == null)
            {
                this.UserDrawback_kl10();
                cache = CacheHelper.GetCache("kc_drawback_FileCacheKey_kl10" + this.Session["user_name"].ToString()) as DataTable;
            }
            return cache;
        }

        public DataTable GetUserDrawback_kl8()
        {
            DataTable cache = CacheHelper.GetCache("kc_drawback_FileCacheKey_kl8" + this.Session["user_name"].ToString()) as DataTable;
            if (cache == null)
            {
                this.UserDrawback_kl8();
                cache = CacheHelper.GetCache("kc_drawback_FileCacheKey_kl8" + this.Session["user_name"].ToString()) as DataTable;
            }
            return cache;
        }

        public DataTable GetUserDrawback_pcdd()
        {
            DataTable cache = CacheHelper.GetCache("kc_drawback_FileCacheKey_pcdd" + this.Session["user_name"].ToString()) as DataTable;
            if (cache == null)
            {
                this.UserDrawback_pcdd();
                cache = CacheHelper.GetCache("kc_drawback_FileCacheKey_pcdd" + this.Session["user_name"].ToString()) as DataTable;
            }
            return cache;
        }

        public DataTable GetUserDrawback_pk10()
        {
            DataTable cache = CacheHelper.GetCache("kc_drawback_FileCacheKey_pk10" + this.Session["user_name"].ToString()) as DataTable;
            if (cache == null)
            {
                this.UserDrawback_pk10();
                cache = CacheHelper.GetCache("kc_drawback_FileCacheKey_pk10" + this.Session["user_name"].ToString()) as DataTable;
            }
            return cache;
        }

        public DataTable GetUserDrawback_pkbjl()
        {
            DataTable cache = CacheHelper.GetCache("kc_drawback_FileCacheKey_pkbjl" + this.Session["user_name"].ToString()) as DataTable;
            if (cache == null)
            {
                this.UserDrawback_pkbjl();
                cache = CacheHelper.GetCache("kc_drawback_FileCacheKey_pkbjl" + this.Session["user_name"].ToString()) as DataTable;
            }
            return cache;
        }

        public DataSet GetUserDrawback_six()
        {
            DataSet cache = CacheHelper.GetCache("six_drawback_FileCacheKey" + this.Session["user_name"].ToString()) as DataSet;
            if (cache == null)
            {
                this.UserDrawback_six();
                cache = CacheHelper.GetCache("six_drawback_FileCacheKey" + this.Session["user_name"].ToString()) as DataSet;
            }
            return cache;
        }

        public DataTable GetUserDrawback_speed5()
        {
            DataTable cache = CacheHelper.GetCache("kc_drawback_FileCacheKey_speed5" + this.Session["user_name"].ToString()) as DataTable;
            if (cache == null)
            {
                this.UserDrawback_speed5();
                cache = CacheHelper.GetCache("kc_drawback_FileCacheKey_speed5" + this.Session["user_name"].ToString()) as DataTable;
            }
            return cache;
        }

        public DataTable GetUserDrawback_ssc168()
        {
            DataTable cache = CacheHelper.GetCache("kc_drawback_FileCacheKey_ssc168" + this.Session["user_name"].ToString()) as DataTable;
            if (cache == null)
            {
                this.UserDrawback_ssc168();
                cache = CacheHelper.GetCache("kc_drawback_FileCacheKey_ssc168" + this.Session["user_name"].ToString()) as DataTable;
            }
            return cache;
        }

        public DataTable GetUserDrawback_vrcar()
        {
            DataTable cache = CacheHelper.GetCache("kc_drawback_FileCacheKey_vrcar" + this.Session["user_name"].ToString()) as DataTable;
            if (cache == null)
            {
                this.UserDrawback_vrcar();
                cache = CacheHelper.GetCache("kc_drawback_FileCacheKey_vrcar" + this.Session["user_name"].ToString()) as DataTable;
            }
            return cache;
        }

        public DataTable GetUserDrawback_vrssc()
        {
            DataTable cache = CacheHelper.GetCache("kc_drawback_FileCacheKey_vrssc" + this.Session["user_name"].ToString()) as DataTable;
            if (cache == null)
            {
                this.UserDrawback_vrssc();
                cache = CacheHelper.GetCache("kc_drawback_FileCacheKey_vrssc" + this.Session["user_name"].ToString()) as DataTable;
            }
            return cache;
        }

        public DataTable GetUserDrawback_xyft5()
        {
            DataTable cache = CacheHelper.GetCache("kc_drawback_FileCacheKey_xyft5" + this.Session["user_name"].ToString()) as DataTable;
            if (cache == null)
            {
                this.UserDrawback_xyft5();
                cache = CacheHelper.GetCache("kc_drawback_FileCacheKey_xyft5" + this.Session["user_name"].ToString()) as DataTable;
            }
            return cache;
        }

        public DataTable GetUserDrawback_xyftoa()
        {
            DataTable cache = CacheHelper.GetCache("kc_drawback_FileCacheKey_xyftoa" + this.Session["user_name"].ToString()) as DataTable;
            if (cache == null)
            {
                this.UserDrawback_xyftoa();
                cache = CacheHelper.GetCache("kc_drawback_FileCacheKey_xyftoa" + this.Session["user_name"].ToString()) as DataTable;
            }
            return cache;
        }

        public DataTable GetUserDrawback_xyftsg()
        {
            DataTable cache = CacheHelper.GetCache("kc_drawback_FileCacheKey_xyftsg" + this.Session["user_name"].ToString()) as DataTable;
            if (cache == null)
            {
                this.UserDrawback_xyftsg();
                cache = CacheHelper.GetCache("kc_drawback_FileCacheKey_xyftsg" + this.Session["user_name"].ToString()) as DataTable;
            }
            return cache;
        }

        public DataTable GetUserDrawback_xync()
        {
            DataTable cache = CacheHelper.GetCache("kc_drawback_FileCacheKey_xync" + this.Session["user_name"].ToString()) as DataTable;
            if (cache == null)
            {
                this.UserDrawback_xync();
                cache = CacheHelper.GetCache("kc_drawback_FileCacheKey_xync" + this.Session["user_name"].ToString()) as DataTable;
            }
            return cache;
        }

        public agent_kc_rate GetUserRate_kc(string zjName)
        {
            agent_kc_rate _rate = HttpContext.Current.Session["kc_rate_FileCacheKey" + HttpContext.Current.Session["user_name"]] as agent_kc_rate;
            if (_rate == null)
            {
                this.UserRate_kc(zjName);
                _rate = HttpContext.Current.Session["kc_rate_FileCacheKey" + HttpContext.Current.Session["user_name"]] as agent_kc_rate;
            }
            return _rate;
        }

        public agent_six_rate GetUserRate_six(string zjName)
        {
            agent_six_rate _rate = HttpContext.Current.Session["six_rate_FileCacheKey" + HttpContext.Current.Session["user_name"]] as agent_six_rate;
            if (_rate == null)
            {
                this.UserRate_six(zjName);
                _rate = HttpContext.Current.Session["six_rate_FileCacheKey" + HttpContext.Current.Session["user_name"]] as agent_six_rate;
            }
            return _rate;
        }

        public string GetUserSkin(string sysType)
        {
            string str = "Blue";
            DataRow item = CallBLL.cz_admin_sysconfig_bll.GetItem();
            if (item == null)
            {
                return str;
            }
            if (sysType.Equals("hy"))
            {
                return item["hy_skin"].ToString().Split(new char[] { '|' })[0];
            }
            return item["agent_skin"].ToString().Split(new char[] { '|' })[0];
        }

        protected string GetUserTypeByName(string u_type)
        {
            switch (u_type)
            {
                case "zj":
                    return "總監";

                case "fgs":
                    return "分公司";

                case "gd":
                    return "股東";

                case "zd":
                    return "總代理";

                case "dl":
                    return "代理";

                case "hy":
                    return "會員";
            }
            return "";
        }

        public string GetValueByKey(string wKey, string fTable, string wStr, SqlParameter[] cmdParms)
        {
            try
            {
                return DbHelperSQL.Query("select " + wKey + " from " + fTable + " where " + wStr, cmdParms).Tables[0].Rows[0][0].ToString();
            }
            catch (Exception)
            {
                return "0";
            }
        }

        protected string GetVRCAR_PlayIDChange(string playType)
        {
            string str = "";
            string str2 = playType;
            if (str2 == null)
            {
                return str;
            }
            if (!(str2 == "q1"))
            {
                if (str2 != "q2")
                {
                    if (str2 != "q3")
                    {
                        return str;
                    }
                    return "24,25,26,27,28,29,30,31,32,33,34,35";
                }
            }
            else
            {
                return "1,2,3,4,5,6,7,8,36,37,38";
            }
            return "9,10,11,12,13,14,15,16,17,18,19,20,21,22,23";
        }

        protected string GetVRSSC_PlayIDChange(string playType)
        {
            return "1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21";
        }

        protected string GetXYFT5_PlayIDChange(string playType)
        {
            string str = "";
            string str2 = playType;
            if (str2 == null)
            {
                return str;
            }
            if (!(str2 == "q1"))
            {
                if (str2 != "q2")
                {
                    if (str2 != "q3")
                    {
                        return str;
                    }
                    return "24,25,26,27,28,29,30,31,32,33,34,35";
                }
            }
            else
            {
                return "1,2,3,4,5,6,7,8,36,37,38";
            }
            return "9,10,11,12,13,14,15,16,17,18,19,20,21,22,23";
        }

        protected string GetXYFTOA_PlayIDChange(string playType)
        {
            string str = "";
            string str2 = playType;
            if (str2 == null)
            {
                return str;
            }
            if (!(str2 == "q1"))
            {
                if (str2 != "q2")
                {
                    if (str2 != "q3")
                    {
                        return str;
                    }
                    return "24,25,26,27,28,29,30,31,32,33,34,35";
                }
            }
            else
            {
                return "1,2,3,4,5,6,7,8,36,37,38";
            }
            return "9,10,11,12,13,14,15,16,17,18,19,20,21,22,23";
        }

        protected string GetXYFTSG_PlayIDChange(string playType)
        {
            string str = "";
            string str2 = playType;
            if (str2 == null)
            {
                return str;
            }
            if (!(str2 == "q1"))
            {
                if (str2 != "q2")
                {
                    if (str2 != "q3")
                    {
                        return str;
                    }
                    return "24,25,26,27,28,29,30,31,32,33,34,35";
                }
            }
            else
            {
                return "1,2,3,4,5,6,7,8,36,37,38";
            }
            return "9,10,11,12,13,14,15,16,17,18,19,20,21,22,23";
        }

        protected string GetXYNC_PlayIDChange(string playType)
        {
            switch (playType)
            {
                case "q1":
                    return "81,82,83,84,85,121,122";

                case "q2":
                    return "86,87,88,89,90,123,124";

                case "q3":
                    return "91,92,93,94,95,125,126";

                case "q4":
                    return "96,97,98,99,100,127,128";

                case "q5":
                    return "101,102,103,104,105,129,130";

                case "q6":
                    return "106,107,108,109,110,131,132";

                case "q7":
                    return "111,112,113,114,115,133,134";

                case "q8":
                    return "116,117,118,119,120,135,136";

                case "zh":
                    return "11,12,13,80";

                case "lm":
                    return "70,71,72,73,74,75,76,77,78,79";
            }
            return "";
        }

        public string GetZodiacId(string numstr)
        {
            if (string.IsNullOrEmpty(numstr))
            {
                return "";
            }
            switch (numstr)
            {
                case "鼠":
                    return "01";

                case "牛":
                    return "02";

                case "虎":
                    return "03";

                case "兔":
                    return "04";

                case "龍":
                    return "05";

                case "蛇":
                    return "06";

                case "馬":
                    return "07";

                case "羊":
                    return "08";

                case "猴":
                    return "09";

                case "雞":
                    return "10";

                case "狗":
                    return "11";

                case "豬":
                    return "12";
            }
            return "";
        }

        public string GetZodiacName(string numstr)
        {
            if (string.IsNullOrEmpty(numstr))
            {
                return "";
            }
            numstr = (numstr.Length == 1) ? ("0" + numstr) : numstr;
            string str = "";
            string[] strArray = base.get_ZodiacNumberArray().Split(new char[] { ',' });
            if (strArray[0].IndexOf(numstr) > -1)
            {
                return "鼠";
            }
            if (strArray[1].IndexOf(numstr) > -1)
            {
                return "牛";
            }
            if (strArray[2].IndexOf(numstr) > -1)
            {
                return "虎";
            }
            if (strArray[3].IndexOf(numstr) > -1)
            {
                return "兔";
            }
            if (strArray[4].IndexOf(numstr) > -1)
            {
                return "龍";
            }
            if (strArray[5].IndexOf(numstr) > -1)
            {
                return "蛇";
            }
            if (strArray[6].IndexOf(numstr) > -1)
            {
                return "馬";
            }
            if (strArray[7].IndexOf(numstr) > -1)
            {
                return "羊";
            }
            if (strArray[8].IndexOf(numstr) > -1)
            {
                return "猴";
            }
            if (strArray[9].IndexOf(numstr) > -1)
            {
                return "雞";
            }
            if (strArray[10].IndexOf(numstr) > -1)
            {
                return "狗";
            }
            if (strArray[11].IndexOf(numstr) > -1)
            {
                str = "豬";
            }
            return str;
        }

        public string GetZodiacNum(string numstr)
        {
            numstr = (numstr.Length == 1) ? ("0" + numstr) : numstr;
            string str = "";
            string[] strArray = base.get_ZodiacNumberArray().Split(new char[] { ',' });
            if (strArray[0].IndexOf(numstr) > -1)
            {
                return "01";
            }
            if (strArray[1].IndexOf(numstr) > -1)
            {
                return "02";
            }
            if (strArray[2].IndexOf(numstr) > -1)
            {
                return "03";
            }
            if (strArray[3].IndexOf(numstr) > -1)
            {
                return "04";
            }
            if (strArray[4].IndexOf(numstr) > -1)
            {
                return "05";
            }
            if (strArray[5].IndexOf(numstr) > -1)
            {
                return "06";
            }
            if (strArray[6].IndexOf(numstr) > -1)
            {
                return "07";
            }
            if (strArray[7].IndexOf(numstr) > -1)
            {
                return "08";
            }
            if (strArray[8].IndexOf(numstr) > -1)
            {
                return "09";
            }
            if (strArray[9].IndexOf(numstr) > -1)
            {
                return "10";
            }
            if (strArray[10].IndexOf(numstr) > -1)
            {
                return "11";
            }
            if (strArray[11].IndexOf(numstr) > -1)
            {
                str = "12";
            }
            return str;
        }

        protected string Gget_zq(string item_name, DataTable DS, string[] z_num, string zodiacList)
        {
            string str = "";
            string str2 = "";
            string str3 = "";
            string str4 = "";
            switch (item_name.ToString().Trim())
            {
                case "特碼A":
                case "特碼B":
                    foreach (DataRow row in DS.Select(string.Format("item_name='{0}' and item_value='{1}'", item_name, z_num[6].ToString().Trim())))
                    {
                        str = str + row["r_ID"].ToString().Trim() + ",";
                    }
                    return str;

                case "特碼大小":
                    str2 = this.count_dx(z_num[6].ToString().Trim());
                    foreach (DataRow row2 in DS.Select(string.Format("item_name='{0}' and item_value='{1}'", item_name, str2)))
                    {
                        str = str + row2["r_ID"].ToString().Trim() + ",";
                    }
                    return str;

                case "特碼單雙":
                    str2 = this.count_ds(z_num[6].ToString().Trim());
                    foreach (DataRow row3 in DS.Select(string.Format("item_name='{0}' and item_value='{1}'", item_name, str2)))
                    {
                        str = str + row3["r_ID"].ToString().Trim() + ",";
                    }
                    return str;

                case "特碼合數單雙":
                    str2 = this.count_hsds(z_num[6].ToString().Trim());
                    foreach (DataRow row4 in DS.Select(string.Format("item_name='{0}' and item_value='{1}'", item_name, str2)))
                    {
                        str = str + row4["r_ID"].ToString().Trim() + ",";
                    }
                    return str;

                case "特碼生肖":
                    str2 = zodiacList.Split(new char[] { ',' })[6];
                    foreach (DataRow row5 in DS.Select(string.Format("item_name='{0}' and item_value='{1}'", item_name, str2)))
                    {
                        str = str + row5["r_ID"].ToString().Trim() + ",";
                    }
                    return str;

                case "特碼色波":
                    str2 = this.count_sb(z_num[6].ToString().Trim());
                    foreach (DataRow row6 in DS.Select(string.Format("item_name='{0}' and item_value='{1}'", item_name, str2)))
                    {
                        str = str + row6["r_ID"].ToString().Trim() + ",";
                    }
                    return str;

                case "半波【單雙】":
                case "半波【大小】":
                    str2 = this.count_sb(z_num[6].ToString().Trim());
                    str3 = this.count_dx(z_num[6].ToString().Trim());
                    str4 = this.count_ds(z_num[6].ToString().Trim());
                    foreach (DataRow row7 in DS.Select(string.Format("item_name='{0}' and item_value in ('{1}','{2}')", item_name, str2 + str3, str2 + str4)))
                    {
                        str = str + row7["r_ID"].ToString().Trim() + ",";
                    }
                    return str;

                case "正碼":
                    foreach (DataRow row8 in DS.Select(string.Format("item_name='{0}' and item_value in ('{1}','{2}','{3}','{4}','{5}','{6}')", new object[] { item_name, z_num[0].ToString().Trim(), z_num[1].ToString().Trim(), z_num[2].ToString().Trim(), z_num[3].ToString().Trim(), z_num[4].ToString().Trim(), z_num[5].ToString().Trim() })))
                    {
                        str = str + row8["r_ID"].ToString().Trim() + ",";
                    }
                    return str;

                case "正1特":
                    foreach (DataRow row9 in DS.Select(string.Format("item_name='{0}' and item_value='{1}'", item_name, z_num[0].ToString().Trim())))
                    {
                        str = str + row9["r_ID"].ToString().Trim() + ",";
                    }
                    return str;

                case "正2特":
                    foreach (DataRow row10 in DS.Select(string.Format("item_name='{0}' and item_value='{1}'", item_name, z_num[1].ToString().Trim())))
                    {
                        str = str + row10["r_ID"].ToString().Trim() + ",";
                    }
                    return str;

                case "正3特":
                    foreach (DataRow row11 in DS.Select(string.Format("item_name='{0}' and item_value='{1}'", item_name, z_num[2].ToString().Trim())))
                    {
                        str = str + row11["r_ID"].ToString().Trim() + ",";
                    }
                    return str;

                case "正4特":
                    foreach (DataRow row12 in DS.Select(string.Format("item_name='{0}' and item_value='{1}'", item_name, z_num[3].ToString().Trim())))
                    {
                        str = str + row12["r_ID"].ToString().Trim() + ",";
                    }
                    return str;

                case "正5特":
                    foreach (DataRow row13 in DS.Select(string.Format("item_name='{0}' and item_value='{1}'", item_name, z_num[4].ToString().Trim())))
                    {
                        str = str + row13["r_ID"].ToString().Trim() + ",";
                    }
                    return str;

                case "正6特":
                    foreach (DataRow row14 in DS.Select(string.Format("item_name='{0}' and item_value='{1}'", item_name, z_num[5].ToString().Trim())))
                    {
                        str = str + row14["r_ID"].ToString().Trim() + ",";
                    }
                    return str;

                case "正碼1-6大小":
                    str2 = this.count_dx(z_num[0].ToString().Trim());
                    str3 = "正碼1";
                    foreach (DataRow row15 in DS.Select(string.Format("item_name='{0}' and item_type='{1}' and item_value ='{2}'", item_name, str3, str2)))
                    {
                        str = str + row15["r_ID"].ToString().Trim() + ",";
                    }
                    str2 = this.count_dx(z_num[1].ToString().Trim());
                    str3 = "正碼2";
                    foreach (DataRow row16 in DS.Select(string.Format("item_name='{0}' and item_type='{1}' and item_value ='{2}'", item_name, str3, str2)))
                    {
                        str = str + row16["r_ID"].ToString().Trim() + ",";
                    }
                    str2 = this.count_dx(z_num[2].ToString().Trim());
                    str3 = "正碼3";
                    foreach (DataRow row17 in DS.Select(string.Format("item_name='{0}' and item_type='{1}' and item_value ='{2}'", item_name, str3, str2)))
                    {
                        str = str + row17["r_ID"].ToString().Trim() + ",";
                    }
                    str2 = this.count_dx(z_num[3].ToString().Trim());
                    str3 = "正碼4";
                    foreach (DataRow row18 in DS.Select(string.Format("item_name='{0}' and item_type='{1}' and item_value ='{2}'", item_name, str3, str2)))
                    {
                        str = str + row18["r_ID"].ToString().Trim() + ",";
                    }
                    str2 = this.count_dx(z_num[4].ToString().Trim());
                    str3 = "正碼5";
                    foreach (DataRow row19 in DS.Select(string.Format("item_name='{0}' and item_type='{1}' and item_value ='{2}'", item_name, str3, str2)))
                    {
                        str = str + row19["r_ID"].ToString().Trim() + ",";
                    }
                    str2 = this.count_dx(z_num[5].ToString().Trim());
                    str3 = "正碼6";
                    foreach (DataRow row20 in DS.Select(string.Format("item_name='{0}' and item_type='{1}' and item_value ='{2}'", item_name, str3, str2)))
                    {
                        str = str + row20["r_ID"].ToString().Trim() + ",";
                    }
                    return str;

                case "正碼1-6單雙":
                    str2 = this.count_ds(z_num[0].ToString().Trim());
                    str3 = "正碼1";
                    foreach (DataRow row21 in DS.Select(string.Format("item_name='{0}' and item_type='{1}' and item_value ='{2}'", item_name, str3, str2)))
                    {
                        str = str + row21["r_ID"].ToString().Trim() + ",";
                    }
                    str2 = this.count_ds(z_num[1].ToString().Trim());
                    str3 = "正碼2";
                    foreach (DataRow row22 in DS.Select(string.Format("item_name='{0}' and item_type='{1}' and item_value ='{2}'", item_name, str3, str2)))
                    {
                        str = str + row22["r_ID"].ToString().Trim() + ",";
                    }
                    str2 = this.count_ds(z_num[2].ToString().Trim());
                    str3 = "正碼3";
                    foreach (DataRow row23 in DS.Select(string.Format("item_name='{0}' and item_type='{1}' and item_value ='{2}'", item_name, str3, str2)))
                    {
                        str = str + row23["r_ID"].ToString().Trim() + ",";
                    }
                    str2 = this.count_ds(z_num[3].ToString().Trim());
                    str3 = "正碼4";
                    foreach (DataRow row24 in DS.Select(string.Format("item_name='{0}' and item_type='{1}' and item_value ='{2}'", item_name, str3, str2)))
                    {
                        str = str + row24["r_ID"].ToString().Trim() + ",";
                    }
                    str2 = this.count_ds(z_num[4].ToString().Trim());
                    str3 = "正碼5";
                    foreach (DataRow row25 in DS.Select(string.Format("item_name='{0}' and item_type='{1}' and item_value ='{2}'", item_name, str3, str2)))
                    {
                        str = str + row25["r_ID"].ToString().Trim() + ",";
                    }
                    str2 = this.count_ds(z_num[5].ToString().Trim());
                    str3 = "正碼6";
                    foreach (DataRow row26 in DS.Select(string.Format("item_name='{0}' and item_type='{1}' and item_value ='{2}'", item_name, str3, str2)))
                    {
                        str = str + row26["r_ID"].ToString().Trim() + ",";
                    }
                    return str;

                case "正碼1-6色波":
                    str2 = this.count_sb(z_num[0].ToString().Trim()).Replace("波", "");
                    str3 = "正碼1";
                    foreach (DataRow row27 in DS.Select(string.Format("item_name='{0}' and item_type='{1}' and item_value ='{2}'", item_name, str3, str2)))
                    {
                        str = str + row27["r_ID"].ToString().Trim() + ",";
                    }
                    str2 = this.count_sb(z_num[1].ToString().Trim()).Replace("波", "");
                    str3 = "正碼2";
                    foreach (DataRow row28 in DS.Select(string.Format("item_name='{0}' and item_type='{1}' and item_value ='{2}'", item_name, str3, str2)))
                    {
                        str = str + row28["r_ID"].ToString().Trim() + ",";
                    }
                    str2 = this.count_sb(z_num[2].ToString().Trim()).Replace("波", "");
                    str3 = "正碼3";
                    foreach (DataRow row29 in DS.Select(string.Format("item_name='{0}' and item_type='{1}' and item_value ='{2}'", item_name, str3, str2)))
                    {
                        str = str + row29["r_ID"].ToString().Trim() + ",";
                    }
                    str2 = this.count_sb(z_num[3].ToString().Trim()).Replace("波", "");
                    str3 = "正碼4";
                    foreach (DataRow row30 in DS.Select(string.Format("item_name='{0}' and item_type='{1}' and item_value ='{2}'", item_name, str3, str2)))
                    {
                        str = str + row30["r_ID"].ToString().Trim() + ",";
                    }
                    str2 = this.count_sb(z_num[4].ToString().Trim()).Replace("波", "");
                    str3 = "正碼5";
                    foreach (DataRow row31 in DS.Select(string.Format("item_name='{0}' and item_type='{1}' and item_value ='{2}'", item_name, str3, str2)))
                    {
                        str = str + row31["r_ID"].ToString().Trim() + ",";
                    }
                    str2 = this.count_sb(z_num[5].ToString().Trim()).Replace("波", "");
                    str3 = "正碼6";
                    foreach (DataRow row32 in DS.Select(string.Format("item_name='{0}' and item_type='{1}' and item_value ='{2}'", item_name, str3, str2)))
                    {
                        str = str + row32["r_ID"].ToString().Trim() + ",";
                    }
                    return str;

                case "正碼1-6合數單雙":
                    str2 = this.count_hsds(z_num[0].ToString().Trim());
                    str3 = "正碼1";
                    foreach (DataRow row33 in DS.Select(string.Format("item_name='{0}' and item_type='{1}' and item_value ='合{2}'", item_name, str3, str2)))
                    {
                        str = str + row33["r_ID"].ToString().Trim() + ",";
                    }
                    str2 = this.count_hsds(z_num[1].ToString().Trim());
                    str3 = "正碼2";
                    foreach (DataRow row34 in DS.Select(string.Format("item_name='{0}' and item_type='{1}' and item_value ='合{2}'", item_name, str3, str2)))
                    {
                        str = str + row34["r_ID"].ToString().Trim() + ",";
                    }
                    str2 = this.count_hsds(z_num[2].ToString().Trim());
                    str3 = "正碼3";
                    foreach (DataRow row35 in DS.Select(string.Format("item_name='{0}' and item_type='{1}' and item_value ='合{2}'", item_name, str3, str2)))
                    {
                        str = str + row35["r_ID"].ToString().Trim() + ",";
                    }
                    str2 = this.count_hsds(z_num[3].ToString().Trim());
                    str3 = "正碼4";
                    foreach (DataRow row36 in DS.Select(string.Format("item_name='{0}' and item_type='{1}' and item_value ='合{2}'", item_name, str3, str2)))
                    {
                        str = str + row36["r_ID"].ToString().Trim() + ",";
                    }
                    str2 = this.count_hsds(z_num[4].ToString().Trim());
                    str3 = "正碼5";
                    foreach (DataRow row37 in DS.Select(string.Format("item_name='{0}' and item_type='{1}' and item_value ='合{2}'", item_name, str3, str2)))
                    {
                        str = str + row37["r_ID"].ToString().Trim() + ",";
                    }
                    str2 = this.count_hsds(z_num[5].ToString().Trim());
                    str3 = "正碼6";
                    foreach (DataRow row38 in DS.Select(string.Format("item_name='{0}' and item_type='{1}' and item_value ='合{2}'", item_name, str3, str2)))
                    {
                        str = str + row38["r_ID"].ToString().Trim() + ",";
                    }
                    return str;

                case "生肖":
                {
                    string str5 = zodiacList.Split(new char[] { ',' })[0];
                    string str6 = zodiacList.Split(new char[] { ',' })[1];
                    string str7 = zodiacList.Split(new char[] { ',' })[2];
                    string str8 = zodiacList.Split(new char[] { ',' })[3];
                    string str9 = zodiacList.Split(new char[] { ',' })[4];
                    string str10 = zodiacList.Split(new char[] { ',' })[5];
                    string str11 = zodiacList.Split(new char[] { ',' })[6];
                    foreach (DataRow row39 in DS.Select(string.Format("item_name='{0}' and  item_value in ('{1}','{2}','{3}','{4}','{5}','{6}','{7}')", new object[] { item_name, str5, str6, str7, str8, str9, str10, str11 })))
                    {
                        str = str + row39["r_ID"].ToString().Trim() + ",";
                    }
                    return str;
                }
                case "尾數":
                {
                    string str12 = this.count_ws(z_num[0].ToString().Trim());
                    string str13 = this.count_ws(z_num[1].ToString().Trim());
                    string str14 = this.count_ws(z_num[2].ToString().Trim());
                    string str15 = this.count_ws(z_num[3].ToString().Trim());
                    string str16 = this.count_ws(z_num[4].ToString().Trim());
                    string str17 = this.count_ws(z_num[5].ToString().Trim());
                    string str18 = this.count_ws(z_num[6].ToString().Trim());
                    foreach (DataRow row40 in DS.Select(string.Format("item_name='{0}' and  item_value in ('{1}','{2}','{3}','{4}','{5}','{6}','{7}')", new object[] { item_name, str12, str13, str14, str15, str16, str17, str18 })))
                    {
                        str = str + row40["r_ID"].ToString().Trim() + ",";
                    }
                    return str;
                }
                case "總和大小":
                    str2 = this.count_zhdx(z_num);
                    foreach (DataRow row41 in DS.Select(string.Format("item_name='{0}' and item_value='{1}'", item_name, str2)))
                    {
                        str = str + row41["r_ID"].ToString().Trim() + ",";
                    }
                    return str;

                case "總和單雙":
                    str2 = this.count_zhds(z_num);
                    foreach (DataRow row42 in DS.Select(string.Format("item_name='{0}' and item_value='{1}'", item_name, str2)))
                    {
                        str = str + row42["r_ID"].ToString().Trim() + ",";
                    }
                    return str;

                case "特碼尾數大小":
                    str2 = this.count_wsdx(z_num[6].ToString().Trim());
                    foreach (DataRow row43 in DS.Select(string.Format("item_name='{0}' and item_value='{1}'", item_name, str2)))
                    {
                        str = str + row43["r_ID"].ToString().Trim() + ",";
                    }
                    return str;

                case "特碼攤子":
                    str2 = this.get_tz(z_num[6].ToString().Trim());
                    foreach (DataRow row44 in DS.Select(string.Format("item_name='{0}' and item_value='{1}'", item_name, str2)))
                    {
                        str = str + row44["r_ID"].ToString().Trim() + ",";
                    }
                    return str;

                case "龍虎":
                    str2 = this.count_longhu(z_num[0].ToString().Trim() + "," + z_num[6].ToString().Trim(), 1);
                    foreach (DataRow row45 in DS.Select(string.Format("item_name='{0}' and item_value='{1}'", item_name, str2)))
                    {
                        str = str + row45["r_ID"].ToString().Trim() + ",";
                    }
                    return str;

                case "天地":
                    str2 = this.count_longhu(z_num[1].ToString().Trim() + "," + z_num[6].ToString().Trim(), 2);
                    foreach (DataRow row46 in DS.Select(string.Format("item_name='{0}' and item_value='{1}'", item_name, str2)))
                    {
                        str = str + row46["r_ID"].ToString().Trim() + ",";
                    }
                    return str;

                case "莊閑":
                    str2 = this.count_longhu(z_num[2].ToString().Trim() + "," + z_num[6].ToString().Trim(), 3);
                    foreach (DataRow row47 in DS.Select(string.Format("item_name='{0}' and item_value='{1}'", item_name, str2)))
                    {
                        str = str + row47["r_ID"].ToString().Trim() + ",";
                    }
                    return str;

                case "雷電":
                    str2 = this.count_longhu(z_num[3].ToString().Trim() + "," + z_num[6].ToString().Trim(), 4);
                    foreach (DataRow row48 in DS.Select(string.Format("item_name='{0}' and item_value='{1}'", item_name, str2)))
                    {
                        str = str + row48["r_ID"].ToString().Trim() + ",";
                    }
                    return str;

                case "神奇":
                    str2 = this.count_longhu(z_num[4].ToString().Trim() + "," + z_num[6].ToString().Trim(), 5);
                    foreach (DataRow row49 in DS.Select(string.Format("item_name='{0}' and item_value='{1}'", item_name, str2)))
                    {
                        str = str + row49["r_ID"].ToString().Trim() + ",";
                    }
                    return str;

                case "黑紅":
                    str2 = this.count_longhu(z_num[5].ToString().Trim() + "," + z_num[6].ToString().Trim(), 6);
                    foreach (DataRow row50 in DS.Select(string.Format("item_name='{0}' and item_value='{1}'", item_name, str2)))
                    {
                        str = str + row50["r_ID"].ToString().Trim() + ",";
                    }
                    return str;

                case "七碼單雙":
                {
                    string str19 = z_num[0].ToString().Trim();
                    string str20 = z_num[1].ToString().Trim();
                    string str21 = z_num[2].ToString().Trim();
                    string str22 = z_num[3].ToString().Trim();
                    string str23 = z_num[4].ToString().Trim();
                    string str24 = z_num[5].ToString().Trim();
                    string str25 = z_num[6].ToString().Trim();
                    str2 = this.count_qmds(string.Format("{0},{1},{2},{3},{4},{5},{6}", new object[] { str19, str20, str21, str22, str23, str24, str25 }));
                    foreach (DataRow row51 in DS.Select(string.Format("item_name='{0}' and item_value='{1}'", item_name, str2)))
                    {
                        str = str + row51["r_ID"].ToString().Trim() + ",";
                    }
                    return str;
                }
                case "七碼大小":
                {
                    string str26 = z_num[0].ToString().Trim();
                    string str27 = z_num[1].ToString().Trim();
                    string str28 = z_num[2].ToString().Trim();
                    string str29 = z_num[3].ToString().Trim();
                    string str30 = z_num[4].ToString().Trim();
                    string str31 = z_num[5].ToString().Trim();
                    string str32 = z_num[6].ToString().Trim();
                    str2 = this.count_qmdx(string.Format("{0},{1},{2},{3},{4},{5},{6}", new object[] { str26, str27, str28, str29, str30, str31, str32 }));
                    foreach (DataRow row52 in DS.Select(string.Format("item_name='{0}' and item_value='{1}'", item_name, str2)))
                    {
                        str = str + row52["r_ID"].ToString().Trim() + ",";
                    }
                    return str;
                }
                case "五行":
                {
                    string sn = z_num[6].ToString().Trim();
                    str2 = this.count_wx(sn);
                    foreach (DataRow row53 in DS.Select(string.Format("item_name='{0}' and item_value='{1}'", item_name, str2)))
                    {
                        str = str + row53["r_ID"].ToString().Trim() + ",";
                    }
                    return str;
                }
                case "生肖【不中】":
                {
                    string str34 = zodiacList.Split(new char[] { ',' })[0];
                    string str35 = zodiacList.Split(new char[] { ',' })[1];
                    string str36 = zodiacList.Split(new char[] { ',' })[2];
                    string str37 = zodiacList.Split(new char[] { ',' })[3];
                    string str38 = zodiacList.Split(new char[] { ',' })[4];
                    string str39 = zodiacList.Split(new char[] { ',' })[5];
                    string str40 = zodiacList.Split(new char[] { ',' })[6];
                    foreach (DataRow row54 in DS.Select(string.Format("item_name='{0}' and  item_value not in ('{1}','{2}','{3}','{4}','{5}','{6}','{7}')", new object[] { item_name, str34, str35, str36, str37, str38, str39, str40 })))
                    {
                        str = str + row54["r_ID"].ToString().Trim() + ",";
                    }
                    return str;
                }
                case "尾數【不中】":
                {
                    string str41 = this.count_ws(z_num[0].ToString().Trim());
                    string str42 = this.count_ws(z_num[1].ToString().Trim());
                    string str43 = this.count_ws(z_num[2].ToString().Trim());
                    string str44 = this.count_ws(z_num[3].ToString().Trim());
                    string str45 = this.count_ws(z_num[4].ToString().Trim());
                    string str46 = this.count_ws(z_num[5].ToString().Trim());
                    string str47 = this.count_ws(z_num[6].ToString().Trim());
                    foreach (DataRow row55 in DS.Select(string.Format("item_name='{0}' and  item_value not in ('{1}','{2}','{3}','{4}','{5}','{6}','{7}')", new object[] { item_name, str41, str42, str43, str44, str45, str46, str47 })))
                    {
                        str = str + row55["r_ID"].ToString().Trim() + ",";
                    }
                    return str;
                }
            }
            return str;
        }

        public string GroupShowHrefString(int master_id, string order_num, string is_payment, string optwindow, string atz)
        {
            string str = "";
            if (master_id.Equals(1))
            {
                str = "six";
            }
            else
            {
                str = "kc";
            }
            return string.Format("<a class='green groupshow' style='text-decoration:underline;' href='javascript:;' data-href='/ReportSearch/GroupShow_{0}.aspx?orderid={1}&ispay={2}&ow={3}&atz={4}' >詳細</a>", new object[] { str, order_num, is_payment, optwindow, atz });
        }

        public void happycar_kjj_log(DataTable oldTable, DataTable newTable, string operate_type)
        {
            string str = this.get_master_name();
            string str2 = this.get_children_name();
            string gameNameByID = base.GetGameNameByID(0x16.ToString());
            string category = "";
            string str5 = "";
            string str6 = "";
            string s = "";
            string str8 = "";
            int num = 0;
            string note = "快捷鍵微调" + str6;
            string act = "";
            int num2 = Convert.ToInt32((LSEnums.LogTypeID) 0);
            if (operate_type.Equals("7"))
            {
                note = "快捷鍵手工输入赔率值,微調" + str6;
            }
            string str11 = "";
            DataTable table = CallBLL.cz_phase_happycar_bll.IsOpenPhaseByTime(DateTime.Now);
            if (table != null)
            {
                str11 = table.Rows[0]["phase"].ToString();
            }
            List<CommandInfo> list = new List<CommandInfo>();
            int num3 = 0;
            foreach (DataRow row in oldTable.Rows)
            {
                category = row["category"].ToString();
                str5 = row["play_name"].ToString();
                str6 = row["put_amount"].ToString();
                s = row["current_odds"].ToString();
                str8 = newTable.Rows[num3]["current_odds"].ToString();
                num = int.Parse(row["odds_id"].ToString());
                if (s != str8)
                {
                    if (double.Parse(s) > double.Parse(str8))
                    {
                        act = "降賠率";
                    }
                    else
                    {
                        act = "升賠率";
                    }
                    List<SqlParameter> paramList = new List<SqlParameter>();
                    string str12 = this.add_sys_log(str, str2, category, str5, str6, gameNameByID, str11, act, num, s, str8, note, num2, 0x16, ref paramList);
                    CommandInfo item = new CommandInfo {
                        CommandText = str12,
                        Parameters = paramList.ToArray()
                    };
                    list.Add(item);
                }
                num3++;
            }
            if (list.Count > 0)
            {
                CallBLL.cz_system_log_bll.execute_sql_tran(list);
            }
        }

        public void happycar_log(cz_odds_happycar oldModel, cz_odds_happycar newModel, string operate_type)
        {
            string str = this.get_master_name();
            string str2 = this.get_children_name();
            string gameNameByID = base.GetGameNameByID(0x16.ToString());
            string category = oldModel.get_category();
            string str5 = oldModel.get_play_name();
            string str6 = oldModel.get_put_amount();
            string s = oldModel.get_current_odds();
            string str8 = newModel.get_current_odds();
            int num = oldModel.get_odds_id();
            string note = "微調" + str6;
            string act = "";
            int num2 = Convert.ToInt32((LSEnums.LogTypeID) 0);
            if (operate_type.Equals("3"))
            {
                note = "手工输入赔率值,微調" + str6;
            }
            string str11 = "";
            DataTable table = CallBLL.cz_phase_happycar_bll.IsOpenPhaseByTime(DateTime.Now);
            if (table != null)
            {
                str11 = table.Rows[0]["phase"].ToString();
            }
            if (s != str8)
            {
                if (double.Parse(s) > double.Parse(str8))
                {
                    act = "降賠率";
                }
                else
                {
                    act = "升賠率";
                }
                List<SqlParameter> paramList = new List<SqlParameter>();
                string str12 = this.add_sys_log(str, str2, category, str5, str6, gameNameByID, str11, act, num, s, str8, note, num2, 0x16, ref paramList);
                CallBLL.cz_system_log_bll.executte_sql(str12, paramList.ToArray());
            }
        }

        protected static void insert_online(string userIP, string user, string user_type, DateTime first_time, DateTime last_time)
        {
            string str = " select u_name from cz_stat_online with(NOLOCK) where u_name =@u_name ";
            SqlParameter[] parameterArray = new SqlParameter[] { new SqlParameter("@u_name", SqlDbType.NVarChar) };
            parameterArray[0].Value = user.Trim();
            if (CallBLL.cz_stat_online_bll.query_sql(str, parameterArray).Rows.Count > 0)
            {
                str = "update cz_stat_online set ip=@ip, first_time=@first_time, last_time=@last_time where u_name =@u_name ";
                SqlParameter[] parameterArray2 = new SqlParameter[] { new SqlParameter("@ip", SqlDbType.NVarChar), new SqlParameter("@first_time", SqlDbType.DateTime), new SqlParameter("@last_time", SqlDbType.DateTime), new SqlParameter("@u_name", SqlDbType.NVarChar) };
                parameterArray2[0].Value = userIP;
                parameterArray2[1].Value = first_time;
                parameterArray2[2].Value = last_time;
                parameterArray2[3].Value = user.Trim();
                CallBLL.cz_stat_online_bll.executte_sql(str, parameterArray2);
            }
            else
            {
                string str2 = string.Format("insert into cz_stat_online (u_name,u_type,first_time,last_time,ip) values(@u_name,@user_type,@first_time,@last_time,@userIP)", new object[0]);
                SqlParameter[] parameterArray3 = new SqlParameter[] { new SqlParameter("@u_name", SqlDbType.NVarChar), new SqlParameter("@user_type", SqlDbType.NVarChar), new SqlParameter("@first_time", SqlDbType.DateTime), new SqlParameter("@last_time", SqlDbType.DateTime), new SqlParameter("@userIP", SqlDbType.NVarChar) };
                parameterArray3[0].Value = user.Trim();
                parameterArray3[1].Value = user_type;
                parameterArray3[2].Value = first_time;
                parameterArray3[3].Value = last_time;
                parameterArray3[4].Value = userIP;
                try
                {
                    CallBLL.cz_stat_online_bll.executte_sql(str2, parameterArray3);
                }
                catch (SqlException exception)
                {
                    if ((exception.Number != 0xa29) && (exception.Number != 0xa43))
                    {
                        throw exception;
                    }
                }
            }
        }

        public bool IsAutoUpdate(string userName, string compareTime)
        {
            if (string.IsNullOrEmpty(compareTime))
            {
                return true;
            }
            bool flag = false;
            string str = userName + "_all_auto_jp_FileCacheKey";
            if (CacheHelper.GetCache(str) == null)
            {
                flag = true;
                CacheHelper.SetPublicFileCacheDependency(str, DateTime.Now, PageBase.GetPublicForderPath(ConfigurationManager.AppSettings["AutoJPCachesFileName"]));
                return flag;
            }
            return false;
        }

        public bool IsChildSync()
        {
            cz_admin_subsystem _subsystem = this.IsChildSystem();
            if (_subsystem == null)
            {
                return false;
            }
            if (!_subsystem.get_flag().Equals(1))
            {
                return false;
            }
            DbHelperSQL_Ex.connectionString = string.Format(PubConstant.get_ConnectionStringExtend(), _subsystem.get_conn());
            DataSet set = DbHelperSQL_Ex.Query(string.Format("select top 1 * from zk_subsys where sys_id='{0}'", _subsystem.get_sys_id()).ToString(), null);
            return (((set != null) && (set.Tables.Count > 0)) && ((set.Tables[0].Rows.Count > 0) && set.Tables[0].Rows[0]["sync"].ToString().Equals("1")));
        }

        public cz_admin_subsystem IsChildSystem()
        {
            return CallBLL.cz_admin_subsystem_bll.GetModel();
        }

        public bool IsCreditLock(string u_name)
        {
            if (FileCacheHelper.get_IsRedisCreditLock().Equals("0"))
            {
                return CallBLL.cz_credit_lock_bll.IsBusy(u_name);
            }
            return (FileCacheHelper.get_IsRedisCreditLock().Equals("1") || (FileCacheHelper.get_IsRedisCreditLock().Equals("2") || true));
        }

        protected bool IsLotteryExist(string lotteryId)
        {
            foreach (DataRow row in this.GetLotteryList().Rows)
            {
                if (row["id"].ToString().Equals(lotteryId))
                {
                    return true;
                }
            }
            return false;
        }

        protected void IsLotteryExist(string lotteryId, string msgCode, string isSuccess, string url)
        {
            bool flag = false;
            DataTable cache = CacheHelper.GetCache("cz_lottery_FileCacheKey") as DataTable;
            if (cache == null)
            {
                cache = CallBLL.cz_lottery_bll.GetList().Tables[0];
                CacheHelper.SetCache("cz_lottery_FileCacheKey", cache);
                CacheHelper.SetPublicFileCache("cz_lottery_FileCacheKey", cache, PageBase.GetPublicForderPath(FileCacheHelper.get_LotteryCachesFileName()));
            }
            foreach (DataRow row in cache.Rows)
            {
                if (row["id"].ToString().Equals(lotteryId))
                {
                    flag = true;
                    break;
                }
            }
            if (!flag)
            {
                base.Response.Redirect(string.Format("/MessagePage.aspx?code={0}&issuccess={1}&url={2}", msgCode, isSuccess, base.Server.UrlEncode(url)));
                base.Response.End();
            }
        }

        protected void IsLotteryExistForSysLog(string lotteryId, string msgCode, string isSuccess, string url)
        {
            if (!string.IsNullOrEmpty(lotteryId))
            {
                this.IsLotteryExist(lotteryId, msgCode, isSuccess, url);
            }
        }

        protected bool IsMasterLotteryExist(string master_id)
        {
            foreach (DataRow row in this.GetLotteryList().Rows)
            {
                if (row["master_id"].ToString().Equals(master_id))
                {
                    return true;
                }
            }
            return false;
        }

        protected void IsOpenLottery()
        {
            if (((this.Context.Request.Path.ToLower().IndexOf("betimes_") >= 0) && (this.Context.Request.Path.ToLower().IndexOf("l_six/betimes_") <= -1)) && (this.Context.Request.Path.ToLower().IndexOf("index.aspx") <= -1))
            {
                string lotteryId = LSRequest.qq("lid");
                this.IsLotteryExist(lotteryId, "u100032", "1", "");
                int num = Convert.ToInt32(lotteryId);
                DataTable table = null;
                switch (num)
                {
                    case 0:
                        table = CallBLL.cz_phase_kl10_bll.IsPhaseClose();
                        break;

                    case 1:
                        table = CallBLL.cz_phase_cqsc_bll.IsPhaseClose();
                        break;

                    case 2:
                        table = CallBLL.cz_phase_pk10_bll.IsPhaseClose();
                        break;

                    case 3:
                        table = CallBLL.cz_phase_xync_bll.IsPhaseClose();
                        break;

                    case 4:
                        table = CallBLL.cz_phase_jsk3_bll.IsPhaseClose();
                        break;

                    case 5:
                        table = CallBLL.cz_phase_kl8_bll.IsPhaseClose();
                        break;

                    case 6:
                        table = CallBLL.cz_phase_k8sc_bll.IsPhaseClose();
                        break;

                    case 7:
                        table = CallBLL.cz_phase_pcdd_bll.IsPhaseClose();
                        break;

                    case 8:
                        table = CallBLL.cz_phase_pkbjl_bll.IsPhaseClose();
                        break;

                    case 9:
                        table = CallBLL.cz_phase_xyft5_bll.IsPhaseClose();
                        break;

                    case 10:
                        table = CallBLL.cz_phase_jscar_bll.IsPhaseClose();
                        break;

                    case 11:
                        table = CallBLL.cz_phase_speed5_bll.IsPhaseClose();
                        break;

                    case 12:
                        table = CallBLL.cz_phase_jspk10_bll.IsPhaseClose();
                        break;

                    case 13:
                        table = CallBLL.cz_phase_jscqsc_bll.IsPhaseClose();
                        break;

                    case 14:
                        table = CallBLL.cz_phase_jssfc_bll.IsPhaseClose();
                        break;

                    case 15:
                        table = CallBLL.cz_phase_jsft2_bll.IsPhaseClose();
                        break;

                    case 0x10:
                        table = CallBLL.cz_phase_car168_bll.IsPhaseClose();
                        break;

                    case 0x11:
                        table = CallBLL.cz_phase_ssc168_bll.IsPhaseClose();
                        break;

                    case 0x12:
                        table = CallBLL.cz_phase_vrcar_bll.IsPhaseClose();
                        break;

                    case 0x13:
                        table = CallBLL.cz_phase_vrssc_bll.IsPhaseClose();
                        break;

                    case 20:
                        table = CallBLL.cz_phase_xyftoa_bll.IsPhaseClose();
                        break;

                    case 0x15:
                        table = CallBLL.cz_phase_xyftsg_bll.IsPhaseClose();
                        break;

                    case 0x16:
                        table = CallBLL.cz_phase_happycar_bll.IsPhaseClose();
                        break;
                }
                this.GetLotteryList().Select(string.Format("id={0}", num));
                if (table.Rows[0]["isopen"].ToString().Equals("0"))
                {
                    string str2 = HttpContext.Current.Request.FilePath.ToLower();
                    FileInfo info = new FileInfo(HttpContext.Current.Request.FilePath);
                    string str3 = info.Extension.ToLower();
                    if ((str2.Substring(str2.LastIndexOf("/") + 1).Trim().IndexOf("betimes_") > -1) && (str3.IndexOf("aspx") > -1))
                    {
                        string str5 = base.Server.UrlEncode(HttpContext.Current.Request.Url.AbsolutePath);
                        base.Response.Redirect(string.Format("/noopen.aspx?lid={0}&path={1}", num, str5), true);
                        base.Response.End();
                    }
                }
            }
        }

        protected bool IsOpenPhase_KC()
        {
            bool flag = false;
            bool flag2 = CallBLL.cz_phase_kl10_bll.IsOpenPhase();
            bool flag3 = CallBLL.cz_phase_cqsc_bll.IsOpenPhase();
            bool flag4 = CallBLL.cz_phase_pk10_bll.IsOpenPhase();
            bool flag5 = CallBLL.cz_phase_xync_bll.IsOpenPhase();
            bool flag6 = CallBLL.cz_phase_jsk3_bll.IsOpenPhase();
            bool flag7 = CallBLL.cz_phase_kl8_bll.IsOpenPhase();
            bool flag8 = CallBLL.cz_phase_k8sc_bll.IsOpenPhase();
            bool flag9 = CallBLL.cz_phase_pcdd_bll.IsOpenPhase();
            bool flag10 = CallBLL.cz_phase_xyft5_bll.IsOpenPhase();
            bool flag11 = CallBLL.cz_phase_pkbjl_bll.IsOpenPhase();
            bool flag12 = CallBLL.cz_phase_jscar_bll.IsOpenPhase();
            bool flag13 = CallBLL.cz_phase_speed5_bll.IsOpenPhase();
            bool flag14 = CallBLL.cz_phase_jscqsc_bll.IsOpenPhase();
            bool flag15 = CallBLL.cz_phase_jspk10_bll.IsOpenPhase();
            bool flag16 = CallBLL.cz_phase_jssfc_bll.IsOpenPhase();
            bool flag17 = CallBLL.cz_phase_jsft2_bll.IsOpenPhase();
            bool flag18 = CallBLL.cz_phase_car168_bll.IsOpenPhase();
            bool flag19 = CallBLL.cz_phase_ssc168_bll.IsOpenPhase();
            bool flag20 = CallBLL.cz_phase_vrcar_bll.IsOpenPhase();
            bool flag21 = CallBLL.cz_phase_vrssc_bll.IsOpenPhase();
            bool flag22 = CallBLL.cz_phase_xyftoa_bll.IsOpenPhase();
            bool flag23 = CallBLL.cz_phase_xyftsg_bll.IsOpenPhase();
            bool flag24 = CallBLL.cz_phase_happycar_bll.IsOpenPhase();
            if ((((!flag2 && !flag3) && (!flag4 && !flag5)) && ((!flag6 && !flag7) && (!flag8 && !flag9))) && ((((!flag10 && !flag11) && (!flag12 && !flag13)) && ((!flag14 && !flag15) && (!flag16 && !flag17))) && (((!flag18 && !flag19) && (!flag20 && !flag21)) && ((!flag22 && !flag23) && !flag24))))
            {
                return flag;
            }
            return true;
        }

        protected bool IsUnderLing(string u_name, string rateName, string u_type)
        {
            bool flag = false;
            if (u_type == "zj")
            {
                return true;
            }
            DataSet set = CallBLL.cz_rate_kc_bll.GetRateIsValid(u_name, rateName, u_type);
            if (((set != null) && (set.Tables.Count > 0)) && (set.Tables[0].Rows.Count > 0))
            {
                flag = true;
            }
            return flag;
        }

        public bool IsUpperLowerLevels(string u_name, string s_utype, string s_uname)
        {
            if (!s_utype.Equals("zj") && (CallBLL.cz_rate_kc_bll.UpperLowerLevels(u_name, s_utype, s_uname) == null))
            {
                return false;
            }
            return true;
        }

        public void jscar_kjj_log(DataTable oldTable, DataTable newTable, string operate_type)
        {
            string str = this.get_master_name();
            string str2 = this.get_children_name();
            string gameNameByID = base.GetGameNameByID(10.ToString());
            string category = "";
            string str5 = "";
            string str6 = "";
            string s = "";
            string str8 = "";
            int num = 0;
            string note = "快捷鍵微调" + str6;
            string act = "";
            int num2 = Convert.ToInt32((LSEnums.LogTypeID) 0);
            if (operate_type.Equals("7"))
            {
                note = "快捷鍵手工输入赔率值,微調" + str6;
            }
            string str11 = "";
            DataTable table = CallBLL.cz_phase_jscar_bll.IsOpenPhaseByTime(DateTime.Now);
            if (table != null)
            {
                str11 = table.Rows[0]["phase"].ToString();
            }
            List<CommandInfo> list = new List<CommandInfo>();
            int num3 = 0;
            foreach (DataRow row in oldTable.Rows)
            {
                category = row["category"].ToString();
                str5 = row["play_name"].ToString();
                str6 = row["put_amount"].ToString();
                s = row["current_odds"].ToString();
                str8 = newTable.Rows[num3]["current_odds"].ToString();
                num = int.Parse(row["odds_id"].ToString());
                if (s != str8)
                {
                    if (double.Parse(s) > double.Parse(str8))
                    {
                        act = "降賠率";
                    }
                    else
                    {
                        act = "升賠率";
                    }
                    List<SqlParameter> paramList = new List<SqlParameter>();
                    string str12 = this.add_sys_log(str, str2, category, str5, str6, gameNameByID, str11, act, num, s, str8, note, num2, 10, ref paramList);
                    CommandInfo item = new CommandInfo {
                        CommandText = str12,
                        Parameters = paramList.ToArray()
                    };
                    list.Add(item);
                }
                num3++;
            }
            if (list.Count > 0)
            {
                CallBLL.cz_system_log_bll.execute_sql_tran(list);
            }
        }

        public void jscar_log(cz_odds_jscar oldModel, cz_odds_jscar newModel, string operate_type)
        {
            string str = this.get_master_name();
            string str2 = this.get_children_name();
            string gameNameByID = base.GetGameNameByID(10.ToString());
            string category = oldModel.get_category();
            string str5 = oldModel.get_play_name();
            string str6 = oldModel.get_put_amount();
            string s = oldModel.get_current_odds();
            string str8 = newModel.get_current_odds();
            int num = oldModel.get_odds_id();
            string note = "微調" + str6;
            string act = "";
            int num2 = Convert.ToInt32((LSEnums.LogTypeID) 0);
            if (operate_type.Equals("3"))
            {
                note = "手工输入赔率值,微調" + str6;
            }
            string str11 = "";
            DataTable table = CallBLL.cz_phase_jscar_bll.IsOpenPhaseByTime(DateTime.Now);
            if (table != null)
            {
                str11 = table.Rows[0]["phase"].ToString();
            }
            if (s != str8)
            {
                if (double.Parse(s) > double.Parse(str8))
                {
                    act = "降賠率";
                }
                else
                {
                    act = "升賠率";
                }
                List<SqlParameter> paramList = new List<SqlParameter>();
                string str12 = this.add_sys_log(str, str2, category, str5, str6, gameNameByID, str11, act, num, s, str8, note, num2, 10, ref paramList);
                CallBLL.cz_system_log_bll.executte_sql(str12, paramList.ToArray());
            }
        }

        public void jscqsc_kjj_log(DataTable oldTable, DataTable newTable, string operate_type)
        {
            string str = this.get_master_name();
            string str2 = this.get_children_name();
            string gameNameByID = base.GetGameNameByID(13.ToString());
            string category = "";
            string str5 = "";
            string str6 = "";
            string s = "";
            string str8 = "";
            int num = 0;
            string note = "快捷鍵微调" + str6;
            string act = "";
            int num2 = Convert.ToInt32((LSEnums.LogTypeID) 0);
            if (operate_type.Equals("7"))
            {
                note = "快捷鍵手工输入赔率值,微調" + str6;
            }
            string str11 = "";
            DataTable table = CallBLL.cz_phase_jscqsc_bll.IsOpenPhaseByTime(DateTime.Now);
            if (table != null)
            {
                str11 = table.Rows[0]["phase"].ToString();
            }
            List<CommandInfo> list = new List<CommandInfo>();
            int num3 = 0;
            foreach (DataRow row in oldTable.Rows)
            {
                category = row["category"].ToString();
                str5 = row["play_name"].ToString();
                str6 = row["put_amount"].ToString();
                s = row["current_odds"].ToString();
                str8 = newTable.Rows[num3]["current_odds"].ToString();
                num = int.Parse(row["odds_id"].ToString());
                if (s != str8)
                {
                    if (double.Parse(s) > double.Parse(str8))
                    {
                        act = "降賠率";
                    }
                    else
                    {
                        act = "升賠率";
                    }
                    List<SqlParameter> paramList = new List<SqlParameter>();
                    string str12 = this.add_sys_log(str, str2, category, str5, str6, gameNameByID, str11, act, num, s, str8, note, num2, 13, ref paramList);
                    CommandInfo item = new CommandInfo {
                        CommandText = str12,
                        Parameters = paramList.ToArray()
                    };
                    list.Add(item);
                }
                num3++;
            }
            if (list.Count > 0)
            {
                CallBLL.cz_system_log_bll.execute_sql_tran(list);
            }
        }

        public void jscqsc_log(cz_odds_jscqsc oldModel, cz_odds_jscqsc newModel, string operate_type)
        {
            string str = this.get_master_name();
            string str2 = this.get_children_name();
            string gameNameByID = base.GetGameNameByID(13.ToString());
            string category = oldModel.get_category();
            string str5 = oldModel.get_play_name();
            string str6 = oldModel.get_put_amount();
            string s = oldModel.get_current_odds();
            string str8 = newModel.get_current_odds();
            int num = oldModel.get_odds_id();
            string note = "微調" + str6;
            string act = "";
            int num2 = Convert.ToInt32((LSEnums.LogTypeID) 0);
            if (operate_type.Equals("3"))
            {
                note = "手工输入赔率值,微調" + str6;
            }
            string str11 = "";
            DataTable table = CallBLL.cz_phase_jscqsc_bll.IsOpenPhaseByTime(DateTime.Now);
            if (table != null)
            {
                str11 = table.Rows[0]["phase"].ToString();
            }
            if (s != str8)
            {
                if (double.Parse(s) > double.Parse(str8))
                {
                    act = "降賠率";
                }
                else
                {
                    act = "升賠率";
                }
                List<SqlParameter> paramList = new List<SqlParameter>();
                string str12 = this.add_sys_log(str, str2, category, str5, str6, gameNameByID, str11, act, num, s, str8, note, num2, 13, ref paramList);
                CallBLL.cz_system_log_bll.executte_sql(str12, paramList.ToArray());
            }
        }

        public void jsft2_kjj_log(DataTable oldTable, DataTable newTable, string operate_type)
        {
            string str = this.get_master_name();
            string str2 = this.get_children_name();
            string gameNameByID = base.GetGameNameByID(15.ToString());
            string category = "";
            string str5 = "";
            string str6 = "";
            string s = "";
            string str8 = "";
            int num = 0;
            string note = "快捷鍵微调" + str6;
            string act = "";
            int num2 = Convert.ToInt32((LSEnums.LogTypeID) 0);
            if (operate_type.Equals("7"))
            {
                note = "快捷鍵手工输入赔率值,微調" + str6;
            }
            string str11 = "";
            DataTable table = CallBLL.cz_phase_jsft2_bll.IsOpenPhaseByTime(DateTime.Now);
            if (table != null)
            {
                str11 = table.Rows[0]["phase"].ToString();
            }
            List<CommandInfo> list = new List<CommandInfo>();
            int num3 = 0;
            foreach (DataRow row in oldTable.Rows)
            {
                category = row["category"].ToString();
                str5 = row["play_name"].ToString();
                str6 = row["put_amount"].ToString();
                s = row["current_odds"].ToString();
                str8 = newTable.Rows[num3]["current_odds"].ToString();
                num = int.Parse(row["odds_id"].ToString());
                if (s != str8)
                {
                    if (double.Parse(s) > double.Parse(str8))
                    {
                        act = "降賠率";
                    }
                    else
                    {
                        act = "升賠率";
                    }
                    List<SqlParameter> paramList = new List<SqlParameter>();
                    string str12 = this.add_sys_log(str, str2, category, str5, str6, gameNameByID, str11, act, num, s, str8, note, num2, 15, ref paramList);
                    CommandInfo item = new CommandInfo {
                        CommandText = str12,
                        Parameters = paramList.ToArray()
                    };
                    list.Add(item);
                }
                num3++;
            }
            if (list.Count > 0)
            {
                CallBLL.cz_system_log_bll.execute_sql_tran(list);
            }
        }

        public void jsft2_log(cz_odds_jsft2 oldModel, cz_odds_jsft2 newModel, string operate_type)
        {
            string str = this.get_master_name();
            string str2 = this.get_children_name();
            string gameNameByID = base.GetGameNameByID(15.ToString());
            string category = oldModel.get_category();
            string str5 = oldModel.get_play_name();
            string str6 = oldModel.get_put_amount();
            string s = oldModel.get_current_odds();
            string str8 = newModel.get_current_odds();
            int num = oldModel.get_odds_id();
            string note = "微調" + str6;
            string act = "";
            int num2 = Convert.ToInt32((LSEnums.LogTypeID) 0);
            if (operate_type.Equals("3"))
            {
                note = "手工输入赔率值,微調" + str6;
            }
            string str11 = "";
            DataTable table = CallBLL.cz_phase_jsft2_bll.IsOpenPhaseByTime(DateTime.Now);
            if (table != null)
            {
                str11 = table.Rows[0]["phase"].ToString();
            }
            if (s != str8)
            {
                if (double.Parse(s) > double.Parse(str8))
                {
                    act = "降賠率";
                }
                else
                {
                    act = "升賠率";
                }
                List<SqlParameter> paramList = new List<SqlParameter>();
                string str12 = this.add_sys_log(str, str2, category, str5, str6, gameNameByID, str11, act, num, s, str8, note, num2, 15, ref paramList);
                CallBLL.cz_system_log_bll.executte_sql(str12, paramList.ToArray());
            }
        }

        public void jsk3_kjj_log(DataTable oldTable, DataTable newTable, string operate_type)
        {
            string str = this.get_master_name();
            string str2 = this.get_children_name();
            string gameNameByID = base.GetGameNameByID(4.ToString());
            string category = "";
            string str5 = "";
            string str6 = "";
            string s = "";
            string str8 = "";
            int num = 0;
            string note = "快捷鍵微调" + str6;
            string act = "";
            int num2 = Convert.ToInt32((LSEnums.LogTypeID) 0);
            if (operate_type.Equals("7"))
            {
                note = "快捷鍵手工输入赔率值,微調" + str6;
            }
            string str11 = "";
            DataTable table = CallBLL.cz_phase_jsk3_bll.IsOpenPhaseByTime(DateTime.Now);
            if (table != null)
            {
                str11 = table.Rows[0]["phase"].ToString();
            }
            List<CommandInfo> list = new List<CommandInfo>();
            int num3 = 0;
            foreach (DataRow row in oldTable.Rows)
            {
                category = row["category"].ToString();
                str5 = row["play_name"].ToString();
                str6 = row["put_amount"].ToString();
                s = row["current_odds"].ToString();
                str8 = newTable.Rows[num3]["current_odds"].ToString();
                num = int.Parse(row["odds_id"].ToString());
                if (s != str8)
                {
                    if (double.Parse(s) > double.Parse(str8))
                    {
                        act = "降賠率";
                    }
                    else
                    {
                        act = "升賠率";
                    }
                    List<SqlParameter> paramList = new List<SqlParameter>();
                    string str12 = this.add_sys_log(str, str2, category, str5, str6, gameNameByID, str11, act, num, s, str8, note, num2, 4, ref paramList);
                    CommandInfo item = new CommandInfo {
                        CommandText = str12,
                        Parameters = paramList.ToArray()
                    };
                    list.Add(item);
                }
                num3++;
            }
            if (list.Count > 0)
            {
                CallBLL.cz_system_log_bll.execute_sql_tran(list);
            }
        }

        public void jsk3_log(cz_odds_jsk3 oldModel, cz_odds_jsk3 newModel, string operate_type)
        {
            string str = this.get_master_name();
            string str2 = this.get_children_name();
            string gameNameByID = base.GetGameNameByID(4.ToString());
            string category = oldModel.get_category();
            string str5 = oldModel.get_play_name();
            string str6 = oldModel.get_put_amount();
            string s = oldModel.get_current_odds();
            string str8 = newModel.get_current_odds();
            int num = oldModel.get_odds_id();
            string note = "微調" + str6;
            string act = "";
            int num2 = Convert.ToInt32((LSEnums.LogTypeID) 0);
            if (operate_type.Equals("3"))
            {
                note = "手工输入赔率值,微調" + str6;
            }
            string str11 = "";
            DataTable table = CallBLL.cz_phase_jsk3_bll.IsOpenPhaseByTime(DateTime.Now);
            if (table != null)
            {
                str11 = table.Rows[0]["phase"].ToString();
            }
            if (s != str8)
            {
                if (double.Parse(s) > double.Parse(str8))
                {
                    act = "降賠率";
                }
                else
                {
                    act = "升賠率";
                }
                List<SqlParameter> paramList = new List<SqlParameter>();
                string str12 = this.add_sys_log(str, str2, category, str5, str6, gameNameByID, str11, act, num, s, str8, note, num2, 4, ref paramList);
                CallBLL.cz_system_log_bll.executte_sql(str12, paramList.ToArray());
            }
        }

        public void jspk10_kjj_log(DataTable oldTable, DataTable newTable, string operate_type)
        {
            string str = this.get_master_name();
            string str2 = this.get_children_name();
            string gameNameByID = base.GetGameNameByID(12.ToString());
            string category = "";
            string str5 = "";
            string str6 = "";
            string s = "";
            string str8 = "";
            int num = 0;
            string note = "快捷鍵微调" + str6;
            string act = "";
            int num2 = Convert.ToInt32((LSEnums.LogTypeID) 0);
            if (operate_type.Equals("7"))
            {
                note = "快捷鍵手工输入赔率值,微調" + str6;
            }
            string str11 = "";
            DataTable table = CallBLL.cz_phase_jspk10_bll.IsOpenPhaseByTime(DateTime.Now);
            if (table != null)
            {
                str11 = table.Rows[0]["phase"].ToString();
            }
            List<CommandInfo> list = new List<CommandInfo>();
            int num3 = 0;
            foreach (DataRow row in oldTable.Rows)
            {
                category = row["category"].ToString();
                str5 = row["play_name"].ToString();
                str6 = row["put_amount"].ToString();
                s = row["current_odds"].ToString();
                str8 = newTable.Rows[num3]["current_odds"].ToString();
                num = int.Parse(row["odds_id"].ToString());
                if (s != str8)
                {
                    if (double.Parse(s) > double.Parse(str8))
                    {
                        act = "降賠率";
                    }
                    else
                    {
                        act = "升賠率";
                    }
                    List<SqlParameter> paramList = new List<SqlParameter>();
                    string str12 = this.add_sys_log(str, str2, category, str5, str6, gameNameByID, str11, act, num, s, str8, note, num2, 12, ref paramList);
                    CommandInfo item = new CommandInfo {
                        CommandText = str12,
                        Parameters = paramList.ToArray()
                    };
                    list.Add(item);
                }
                num3++;
            }
            if (list.Count > 0)
            {
                CallBLL.cz_system_log_bll.execute_sql_tran(list);
            }
        }

        public void jspk10_log(cz_odds_jspk10 oldModel, cz_odds_jspk10 newModel, string operate_type)
        {
            string str = this.get_master_name();
            string str2 = this.get_children_name();
            string gameNameByID = base.GetGameNameByID(12.ToString());
            string category = oldModel.get_category();
            string str5 = oldModel.get_play_name();
            string str6 = oldModel.get_put_amount();
            string s = oldModel.get_current_odds();
            string str8 = newModel.get_current_odds();
            int num = oldModel.get_odds_id();
            string note = "微調" + str6;
            string act = "";
            int num2 = Convert.ToInt32((LSEnums.LogTypeID) 0);
            if (operate_type.Equals("3"))
            {
                note = "手工输入赔率值,微調" + str6;
            }
            string str11 = "";
            DataTable table = CallBLL.cz_phase_jspk10_bll.IsOpenPhaseByTime(DateTime.Now);
            if (table != null)
            {
                str11 = table.Rows[0]["phase"].ToString();
            }
            if (s != str8)
            {
                if (double.Parse(s) > double.Parse(str8))
                {
                    act = "降賠率";
                }
                else
                {
                    act = "升賠率";
                }
                List<SqlParameter> paramList = new List<SqlParameter>();
                string str12 = this.add_sys_log(str, str2, category, str5, str6, gameNameByID, str11, act, num, s, str8, note, num2, 12, ref paramList);
                CallBLL.cz_system_log_bll.executte_sql(str12, paramList.ToArray());
            }
        }

        public void jssfc_kjj_log(DataTable oldTable, DataTable newTable, string operate_type)
        {
            string str = this.get_master_name();
            string str2 = this.get_children_name();
            string gameNameByID = base.GetGameNameByID(14.ToString());
            string category = "";
            string str5 = "";
            string str6 = "";
            string s = "";
            string str8 = "";
            int num = 0;
            string note = "快捷鍵微调" + str6;
            string act = "";
            int num2 = Convert.ToInt32((LSEnums.LogTypeID) 0);
            if (operate_type.Equals("7"))
            {
                note = "快捷鍵手工输入赔率值,微調" + str6;
            }
            string str11 = "";
            DataTable table = CallBLL.cz_phase_jssfc_bll.IsOpenPhaseByTime(DateTime.Now);
            if (table != null)
            {
                str11 = table.Rows[0]["phase"].ToString();
            }
            List<CommandInfo> list = new List<CommandInfo>();
            int num3 = 0;
            foreach (DataRow row in oldTable.Rows)
            {
                category = row["category"].ToString();
                str5 = row["play_name"].ToString();
                str6 = row["put_amount"].ToString();
                s = row["current_odds"].ToString();
                str8 = newTable.Rows[num3]["current_odds"].ToString();
                num = int.Parse(row["odds_id"].ToString());
                if (s != str8)
                {
                    if (double.Parse(s) > double.Parse(str8))
                    {
                        act = "降賠率";
                    }
                    else
                    {
                        act = "升賠率";
                    }
                    List<SqlParameter> paramList = new List<SqlParameter>();
                    string str12 = this.add_sys_log(str, str2, category, str5, str6, gameNameByID, str11, act, num, s, str8, note, num2, 14, ref paramList);
                    CommandInfo item = new CommandInfo {
                        CommandText = str12,
                        Parameters = paramList.ToArray()
                    };
                    list.Add(item);
                }
                num3++;
            }
            if (list.Count > 0)
            {
                CallBLL.cz_system_log_bll.execute_sql_tran(list);
            }
        }

        public void jssfc_log(cz_odds_jssfc oldModel, cz_odds_jssfc newModel, string operate_type)
        {
            string str = this.get_master_name();
            string str2 = this.get_children_name();
            string gameNameByID = base.GetGameNameByID(14.ToString());
            string category = oldModel.get_category();
            string str5 = oldModel.get_play_name();
            string str6 = oldModel.get_put_amount();
            string s = oldModel.get_current_odds();
            string str8 = newModel.get_current_odds();
            int num = oldModel.get_odds_id();
            string note = "微調" + str6;
            string act = "";
            int num2 = Convert.ToInt32((LSEnums.LogTypeID) 0);
            if (operate_type.Equals("3"))
            {
                note = "手工输入赔率值,微調" + str6;
            }
            string str11 = "";
            DataTable table = CallBLL.cz_phase_jssfc_bll.IsOpenPhaseByTime(DateTime.Now);
            if (table != null)
            {
                str11 = table.Rows[0]["phase"].ToString();
            }
            if (s != str8)
            {
                if (double.Parse(s) > double.Parse(str8))
                {
                    act = "降賠率";
                }
                else
                {
                    act = "升賠率";
                }
                List<SqlParameter> paramList = new List<SqlParameter>();
                string str12 = this.add_sys_log(str, str2, category, str5, str6, gameNameByID, str11, act, num, s, str8, note, num2, 14, ref paramList);
                CallBLL.cz_system_log_bll.executte_sql(str12, paramList.ToArray());
            }
        }

        public void jysd_kc_log(DataTable oldDT, DataTable newDT, string lotteryID)
        {
            string str = this.get_master_name();
            string str2 = this.get_children_name();
            string gameNameByID = base.GetGameNameByID(lotteryID);
            string category = "";
            string str5 = "";
            string str6 = "";
            string str7 = "";
            string str8 = "";
            int num = 0;
            string note = "交易設定";
            string act = "交易設定";
            int num2 = Convert.ToInt32((LSEnums.LogTypeID) 2);
            string str11 = "";
            int count = oldDT.Rows.Count;
            IEqualityComparer<DataRow> comparer = DataRowComparer.Default;
            for (int i = 0; i < count; i++)
            {
                str7 = "";
                str8 = "";
                DataRow x = oldDT.Rows[i];
                DataRow y = newDT.Rows[i];
                if (!comparer.Equals(x, y))
                {
                    str5 = x["play_name"].ToString();
                    x["play_id"].ToString();
                    string str12 = this.get_rename_play_name(lotteryID, i);
                    if (str12 != "")
                    {
                        str5 = str12;
                    }
                    string s = x["total_amount"].ToString();
                    string str14 = x["allow_min_amount"].ToString();
                    string str15 = x["allow_max_amount"].ToString();
                    string str16 = x["allow_max_put_amount"].ToString();
                    string str17 = x["max_appoint"].ToString();
                    string str18 = x["above_quota"].ToString();
                    string str19 = y["total_amount"].ToString();
                    string str20 = y["allow_min_amount"].ToString();
                    string str21 = y["allow_max_amount"].ToString();
                    string str22 = y["allow_max_put_amount"].ToString();
                    string str23 = y["max_appoint"].ToString();
                    string str24 = y["above_quota"].ToString();
                    if (!s.Equals(str19))
                    {
                        str7 = "公司總受註額(實貨):" + double.Parse(s).ToString("F0");
                        str8 = "公司總受註額(實貨):" + double.Parse(str19).ToString("F0");
                    }
                    if (!str14.Equals(str20))
                    {
                        str7 = str7 + " 單註最低:" + double.Parse(str14).ToString("F0");
                        str8 = str8 + " 單註最低:" + double.Parse(str20).ToString("F0");
                    }
                    if (!str15.Equals(str21))
                    {
                        str7 = str7 + " 單註最高:" + double.Parse(str15).ToString("F0");
                        str8 = str8 + " 單註最高:" + double.Parse(str21).ToString("F0");
                    }
                    if (!str16.Equals(str22))
                    {
                        str7 = str7 + " 單期限額:" + double.Parse(str16).ToString("F0");
                        str8 = str8 + " 單期限額:" + double.Parse(str22).ToString("F0");
                    }
                    if (!str17.Equals(str23))
                    {
                        str7 = str7 + " 最高派彩:" + double.Parse(str17).ToString("F0");
                        str8 = str8 + " 最高派彩:" + double.Parse(str23).ToString("F0");
                    }
                    if (!str18.Equals(str24))
                    {
                        str7 = str7 + " 負值超額警示:" + double.Parse(str18).ToString("F0");
                        str8 = str8 + " 負值超額警示:" + double.Parse(str24).ToString("F0");
                    }
                    List<SqlParameter> paramList = new List<SqlParameter>();
                    string str25 = this.add_sys_log(str, str2, category, str5, str6, gameNameByID, str11, act, num, str7, str8, note, num2, int.Parse(lotteryID), ref paramList);
                    CallBLL.cz_system_log_bll.executte_sql(str25, paramList.ToArray());
                }
            }
        }

        public void jysd_six_log(DataTable oldDT, DataTable newDT)
        {
            string str = this.get_master_name();
            string str2 = this.get_children_name();
            string gameNameByID = base.GetGameNameByID(100.ToString());
            string category = "";
            string str5 = "";
            string str6 = "";
            string str7 = "";
            string str8 = "";
            int num = 0;
            string note = "交易設定";
            string act = "交易設定";
            int num2 = Convert.ToInt32((LSEnums.LogTypeID) 2);
            string str11 = "";
            int count = oldDT.Rows.Count;
            IEqualityComparer<DataRow> comparer = DataRowComparer.Default;
            for (int i = 0; i < count; i++)
            {
                str7 = "";
                str8 = "";
                DataRow x = oldDT.Rows[i];
                DataRow y = newDT.Rows[i];
                if (!comparer.Equals(x, y))
                {
                    str5 = x["play_name"].ToString();
                    string str12 = x["play_id"].ToString();
                    if (str12 == "91010")
                    {
                        str5 = "正碼特";
                    }
                    "91060,91061,91062,91063,91064,91065".IndexOf(str12);
                    string s = x["total_amount"].ToString();
                    string str14 = x["allow_min_amount"].ToString();
                    string str15 = x["allow_max_amount"].ToString();
                    string str16 = x["allow_max_put_amount"].ToString();
                    string str17 = x["max_appoint"].ToString();
                    string str18 = x["above_quota"].ToString();
                    string str19 = y["total_amount"].ToString();
                    string str20 = y["allow_min_amount"].ToString();
                    string str21 = y["allow_max_amount"].ToString();
                    string str22 = y["allow_max_put_amount"].ToString();
                    string str23 = y["max_appoint"].ToString();
                    string str24 = y["above_quota"].ToString();
                    if (!s.Equals(str19))
                    {
                        str7 = "公司總受註額(實貨):" + double.Parse(s).ToString("F0");
                        str8 = "公司總受註額(實貨):" + double.Parse(str19).ToString("F0");
                    }
                    if (!str14.Equals(str20))
                    {
                        str7 = str7 + " 單註最低:" + double.Parse(str14).ToString("F0");
                        str8 = str8 + " 單註最低:" + double.Parse(str20).ToString("F0");
                    }
                    if (!str15.Equals(str21))
                    {
                        str7 = str7 + " 單註最高:" + double.Parse(str15).ToString("F0");
                        str8 = str8 + " 單註最高:" + double.Parse(str21).ToString("F0");
                    }
                    if (!str16.Equals(str22))
                    {
                        str7 = str7 + " 單期限額:" + double.Parse(str16).ToString("F0");
                        str8 = str8 + " 單期限額:" + double.Parse(str22).ToString("F0");
                    }
                    if (!str17.Equals(str23))
                    {
                        str7 = str7 + " 最高派彩:" + double.Parse(str17).ToString("F0");
                        str8 = str8 + " 最高派彩:" + double.Parse(str23).ToString("F0");
                    }
                    if (!str18.Equals(str24))
                    {
                        str7 = str7 + " 負值超額警示:" + double.Parse(str18).ToString("F0");
                        str8 = str8 + " 負值超額警示:" + double.Parse(str24).ToString("F0");
                    }
                    List<SqlParameter> paramList = new List<SqlParameter>();
                    string str25 = this.add_sys_log(str, str2, category, str5, str6, gameNameByID, str11, act, num, str7, str8, note, num2, 100, ref paramList);
                    CallBLL.cz_system_log_bll.executte_sql(str25, paramList.ToArray());
                }
            }
        }

        public void k8sc_kjj_log(DataTable oldTable, DataTable newTable, string operate_type)
        {
            string str = this.get_master_name();
            string str2 = this.get_children_name();
            string gameNameByID = base.GetGameNameByID(6.ToString());
            string category = "";
            string str5 = "";
            string str6 = "";
            string s = "";
            string str8 = "";
            int num = 0;
            string note = "快捷鍵微调" + str6;
            string act = "";
            int num2 = Convert.ToInt32((LSEnums.LogTypeID) 0);
            if (operate_type.Equals("7"))
            {
                note = "快捷鍵手工输入赔率值,微調" + str6;
            }
            string str11 = "";
            DataTable table = CallBLL.cz_phase_k8sc_bll.IsOpenPhaseByTime(DateTime.Now);
            if (table != null)
            {
                str11 = table.Rows[0]["phase"].ToString();
            }
            List<CommandInfo> list = new List<CommandInfo>();
            int num3 = 0;
            foreach (DataRow row in oldTable.Rows)
            {
                category = row["category"].ToString();
                str5 = row["play_name"].ToString();
                str6 = row["put_amount"].ToString();
                s = row["current_odds"].ToString();
                str8 = newTable.Rows[num3]["current_odds"].ToString();
                num = int.Parse(row["odds_id"].ToString());
                if (s != str8)
                {
                    if (double.Parse(s) > double.Parse(str8))
                    {
                        act = "降賠率";
                    }
                    else
                    {
                        act = "升賠率";
                    }
                    List<SqlParameter> paramList = new List<SqlParameter>();
                    string str12 = this.add_sys_log(str, str2, category, str5, str6, gameNameByID, str11, act, num, s, str8, note, num2, 6, ref paramList);
                    CommandInfo item = new CommandInfo {
                        CommandText = str12,
                        Parameters = paramList.ToArray()
                    };
                    list.Add(item);
                }
                num3++;
            }
            if (list.Count > 0)
            {
                CallBLL.cz_system_log_bll.execute_sql_tran(list);
            }
        }

        public void k8sc_log(cz_odds_k8sc oldModel, cz_odds_k8sc newModel, string operate_type)
        {
            string str = this.get_master_name();
            string str2 = this.get_children_name();
            string gameNameByID = base.GetGameNameByID(6.ToString());
            string category = oldModel.get_category();
            string str5 = oldModel.get_play_name();
            string str6 = oldModel.get_put_amount();
            string s = oldModel.get_current_odds();
            string str8 = newModel.get_current_odds();
            int num = oldModel.get_odds_id();
            string note = "微調" + str6;
            string act = "";
            int num2 = Convert.ToInt32((LSEnums.LogTypeID) 0);
            if (operate_type.Equals("3"))
            {
                note = "手工输入赔率值,微調" + str6;
            }
            string str11 = "";
            DataTable table = CallBLL.cz_phase_k8sc_bll.IsOpenPhaseByTime(DateTime.Now);
            if (table != null)
            {
                str11 = table.Rows[0]["phase"].ToString();
            }
            if (s != str8)
            {
                if (double.Parse(s) > double.Parse(str8))
                {
                    act = "降賠率";
                }
                else
                {
                    act = "升賠率";
                }
                List<SqlParameter> paramList = new List<SqlParameter>();
                string str12 = this.add_sys_log(str, str2, category, str5, str6, gameNameByID, str11, act, num, s, str8, note, num2, 6, ref paramList);
                CallBLL.cz_system_log_bll.executte_sql(str12, paramList.ToArray());
            }
        }

        public void kc_openclose_log(string oddsid, string operate_type, string is_open, int lotteryID, string playpage)
        {
            List<SqlParameter> list;
            string str = this.get_master_name();
            string str2 = this.get_children_name();
            string gameNameByID = base.GetGameNameByID(lotteryID.ToString());
            string category = "";
            string str5 = "";
            string str6 = "";
            string str7 = "";
            string str8 = "";
            int num = 0;
            string note = "單項開停盤操作";
            string act = "";
            int num2 = Convert.ToInt32((LSEnums.LogTypeID) 0);
            string str11 = "";
            switch (lotteryID)
            {
                case 0:
                {
                    cz_odds_kl10 model = null;
                    if (!string.IsNullOrEmpty(oddsid) && (oddsid.IndexOf(',') < 0))
                    {
                        model = CallBLL.cz_odds_kl10_bll.GetModel(Convert.ToInt32(oddsid));
                    }
                    if (model == null)
                    {
                        if (is_open.Equals("0"))
                        {
                            act = "停押";
                        }
                        else
                        {
                            act = "開放";
                        }
                        if (operate_type.Equals("61"))
                        {
                            note = "全部開停盤操作";
                        }
                        if (operate_type.Equals("62"))
                        {
                            note = "當前頁開停盤操作";
                            string str12 = "";
                            str12 = this.get_playpage_name_kc(lotteryID, playpage);
                            if (str12 != "")
                            {
                                note = note + "=>" + str12;
                            }
                        }
                        if (operate_type.Equals("8"))
                        {
                            note = "快捷鍵開停盤操作";
                        }
                        break;
                    }
                    category = model.get_category();
                    str5 = model.get_play_name();
                    str6 = model.get_put_amount();
                    num = int.Parse(oddsid);
                    note = note + "," + str6;
                    int? nullable = new int?(model.get_is_open());
                    if (nullable == 0)
                    {
                        str7 = "開盤";
                        str8 = "停盤";
                        act = "停押";
                    }
                    else
                    {
                        str7 = "停盤";
                        str8 = "開盤";
                        act = "開放";
                    }
                    break;
                }
                case 1:
                {
                    cz_odds_cqsc _cqsc = null;
                    if (!string.IsNullOrEmpty(oddsid) && (oddsid.IndexOf(',') < 0))
                    {
                        _cqsc = CallBLL.cz_odds_cqsc_bll.GetModel(Convert.ToInt32(oddsid));
                    }
                    if (_cqsc == null)
                    {
                        if (is_open.Equals("0"))
                        {
                            act = "停押";
                        }
                        else
                        {
                            act = "開放";
                        }
                        if (operate_type.Equals("61"))
                        {
                            note = "全部開停盤操作";
                        }
                        if (operate_type.Equals("62"))
                        {
                            note = "當前頁開停盤操作";
                            string str13 = "";
                            str13 = this.get_playpage_name_kc(lotteryID, playpage);
                            if (str13 != "")
                            {
                                note = note + "=>" + str13;
                            }
                        }
                        if (operate_type.Equals("8"))
                        {
                            note = "快捷鍵開停盤操作";
                        }
                    }
                    else
                    {
                        category = _cqsc.get_category();
                        str5 = _cqsc.get_play_name();
                        str6 = _cqsc.get_put_amount();
                        num = int.Parse(oddsid);
                        note = note + "," + str6;
                        if (_cqsc.get_is_open() == 0)
                        {
                            str7 = "開盤";
                            str8 = "停盤";
                            act = "停押";
                        }
                        else
                        {
                            str7 = "停盤";
                            str8 = "開盤";
                            act = "開放";
                        }
                    }
                    DataTable table2 = CallBLL.cz_phase_cqsc_bll.IsOpenPhaseByTime(DateTime.Now);
                    if (table2 != null)
                    {
                        str11 = table2.Rows[0]["phase"].ToString();
                    }
                    goto Label_22F5;
                }
                case 2:
                {
                    cz_odds_pk10 _pk = null;
                    if (!string.IsNullOrEmpty(oddsid) && (oddsid.IndexOf(',') < 0))
                    {
                        _pk = CallBLL.cz_odds_pk10_bll.GetModel(Convert.ToInt32(oddsid));
                    }
                    if (_pk == null)
                    {
                        if (is_open.Equals("0"))
                        {
                            act = "停押";
                        }
                        else
                        {
                            act = "開放";
                        }
                        if (operate_type.Equals("61"))
                        {
                            note = "全部開停盤操作";
                        }
                        if (operate_type.Equals("62"))
                        {
                            note = "當前頁開停盤操作";
                            string str14 = "";
                            str14 = this.get_playpage_name_kc(lotteryID, playpage);
                            if (str14 != "")
                            {
                                note = note + "=>" + str14;
                            }
                        }
                        if (operate_type.Equals("8"))
                        {
                            note = "快捷鍵開停盤操作";
                        }
                    }
                    else
                    {
                        category = _pk.get_category();
                        str5 = _pk.get_play_name();
                        str6 = _pk.get_put_amount();
                        num = int.Parse(oddsid);
                        note = note + "," + str6;
                        if (_pk.get_is_open() == 0)
                        {
                            str7 = "開盤";
                            str8 = "停盤";
                            act = "停押";
                        }
                        else
                        {
                            str7 = "停盤";
                            str8 = "開盤";
                            act = "開放";
                        }
                    }
                    DataTable table3 = CallBLL.cz_phase_pk10_bll.IsOpenPhaseByTime(DateTime.Now);
                    if (table3 != null)
                    {
                        str11 = table3.Rows[0]["phase"].ToString();
                    }
                    goto Label_22F5;
                }
                case 3:
                {
                    cz_odds_xync _xync = null;
                    if (!string.IsNullOrEmpty(oddsid) && (oddsid.IndexOf(',') < 0))
                    {
                        _xync = CallBLL.cz_odds_xync_bll.GetModel(Convert.ToInt32(oddsid));
                    }
                    if (_xync == null)
                    {
                        if (is_open.Equals("0"))
                        {
                            act = "停押";
                        }
                        else
                        {
                            act = "開放";
                        }
                        if (operate_type.Equals("61"))
                        {
                            note = "全部開停盤操作";
                        }
                        if (operate_type.Equals("62"))
                        {
                            note = "當前頁開停盤操作";
                            string str15 = "";
                            str15 = this.get_playpage_name_kc(lotteryID, playpage);
                            if (str15 != "")
                            {
                                note = note + "=>" + str15;
                            }
                        }
                        if (operate_type.Equals("8"))
                        {
                            note = "快捷鍵開停盤操作";
                        }
                    }
                    else
                    {
                        category = _xync.get_category();
                        str5 = _xync.get_play_name();
                        str6 = _xync.get_put_amount();
                        num = int.Parse(oddsid);
                        note = note + "," + str6;
                        if (_xync.get_is_open() == 0)
                        {
                            str7 = "開盤";
                            str8 = "停盤";
                            act = "停押";
                        }
                        else
                        {
                            str7 = "停盤";
                            str8 = "開盤";
                            act = "開放";
                        }
                    }
                    DataTable table4 = CallBLL.cz_phase_xync_bll.IsOpenPhaseByTime(DateTime.Now);
                    if (table4 != null)
                    {
                        str11 = table4.Rows[0]["phase"].ToString();
                    }
                    goto Label_22F5;
                }
                case 4:
                {
                    cz_odds_jsk3 _jsk = null;
                    if (!string.IsNullOrEmpty(oddsid) && (oddsid.IndexOf(',') < 0))
                    {
                        _jsk = CallBLL.cz_odds_jsk3_bll.GetModel(Convert.ToInt32(oddsid));
                    }
                    if (_jsk == null)
                    {
                        if (is_open.Equals("0"))
                        {
                            act = "停押";
                        }
                        else
                        {
                            act = "開放";
                        }
                        if (operate_type.Equals("61"))
                        {
                            note = "全部開停盤操作";
                        }
                        if (operate_type.Equals("62"))
                        {
                            note = "當前頁開停盤操作";
                            string str16 = "";
                            str16 = this.get_playpage_name_kc(lotteryID, playpage);
                            if (str16 != "")
                            {
                                note = note + "=>" + str16;
                            }
                        }
                        if (operate_type.Equals("8"))
                        {
                            note = "快捷鍵開停盤操作";
                        }
                    }
                    else
                    {
                        category = _jsk.get_category();
                        str5 = _jsk.get_play_name();
                        str6 = _jsk.get_put_amount();
                        num = int.Parse(oddsid);
                        note = note + "," + str6;
                        if (_jsk.get_is_open() == 0)
                        {
                            str7 = "開盤";
                            str8 = "停盤";
                            act = "停押";
                        }
                        else
                        {
                            str7 = "停盤";
                            str8 = "開盤";
                            act = "開放";
                        }
                    }
                    DataTable table5 = CallBLL.cz_phase_jsk3_bll.IsOpenPhaseByTime(DateTime.Now);
                    if (table5 != null)
                    {
                        str11 = table5.Rows[0]["phase"].ToString();
                    }
                    goto Label_22F5;
                }
                case 5:
                {
                    cz_odds_kl8 _kl2 = null;
                    if (!string.IsNullOrEmpty(oddsid) && (oddsid.IndexOf(',') < 0))
                    {
                        _kl2 = CallBLL.cz_odds_kl8_bll.GetModel(Convert.ToInt32(oddsid));
                    }
                    if (_kl2 == null)
                    {
                        if (is_open.Equals("0"))
                        {
                            act = "停押";
                        }
                        else
                        {
                            act = "開放";
                        }
                        if (operate_type.Equals("61"))
                        {
                            note = "全部開停盤操作";
                        }
                        if (operate_type.Equals("62"))
                        {
                            note = "當前頁開停盤操作";
                            string str17 = "";
                            str17 = this.get_playpage_name_kc(lotteryID, playpage);
                            if (str17 != "")
                            {
                                note = note + "=>" + str17;
                            }
                        }
                        if (operate_type.Equals("8"))
                        {
                            note = "快捷鍵開停盤操作";
                        }
                    }
                    else
                    {
                        category = _kl2.get_category();
                        str5 = _kl2.get_play_name();
                        str6 = _kl2.get_put_amount();
                        num = int.Parse(oddsid);
                        note = note + "," + str6;
                        if (_kl2.get_is_open() == 0)
                        {
                            str7 = "開盤";
                            str8 = "停盤";
                            act = "停押";
                        }
                        else
                        {
                            str7 = "停盤";
                            str8 = "開盤";
                            act = "開放";
                        }
                    }
                    DataTable table6 = CallBLL.cz_phase_kl8_bll.IsOpenPhaseByTime(DateTime.Now);
                    if (table6 != null)
                    {
                        str11 = table6.Rows[0]["phase"].ToString();
                    }
                    goto Label_22F5;
                }
                case 6:
                {
                    cz_odds_k8sc _ksc = null;
                    if (!string.IsNullOrEmpty(oddsid) && (oddsid.IndexOf(',') < 0))
                    {
                        _ksc = CallBLL.cz_odds_k8sc_bll.GetModel(Convert.ToInt32(oddsid));
                    }
                    if (_ksc == null)
                    {
                        if (is_open.Equals("0"))
                        {
                            act = "停押";
                        }
                        else
                        {
                            act = "開放";
                        }
                        if (operate_type.Equals("61"))
                        {
                            note = "全部開停盤操作";
                        }
                        if (operate_type.Equals("62"))
                        {
                            note = "當前頁開停盤操作";
                            string str18 = "";
                            str18 = this.get_playpage_name_kc(lotteryID, playpage);
                            if (str18 != "")
                            {
                                note = note + "=>" + str18;
                            }
                        }
                        if (operate_type.Equals("8"))
                        {
                            note = "快捷鍵開停盤操作";
                        }
                    }
                    else
                    {
                        category = _ksc.get_category();
                        str5 = _ksc.get_play_name();
                        str6 = _ksc.get_put_amount();
                        num = int.Parse(oddsid);
                        note = note + "," + str6;
                        if (_ksc.get_is_open() == 0)
                        {
                            str7 = "開盤";
                            str8 = "停盤";
                            act = "停押";
                        }
                        else
                        {
                            str7 = "停盤";
                            str8 = "開盤";
                            act = "開放";
                        }
                    }
                    DataTable table7 = CallBLL.cz_phase_k8sc_bll.IsOpenPhaseByTime(DateTime.Now);
                    if (table7 != null)
                    {
                        str11 = table7.Rows[0]["phase"].ToString();
                    }
                    goto Label_22F5;
                }
                case 7:
                {
                    cz_odds_pcdd _pcdd = null;
                    if (!string.IsNullOrEmpty(oddsid) && (oddsid.IndexOf(',') < 0))
                    {
                        _pcdd = CallBLL.cz_odds_pcdd_bll.GetModel(Convert.ToInt32(oddsid));
                    }
                    if (_pcdd == null)
                    {
                        if (is_open.Equals("0"))
                        {
                            act = "停押";
                        }
                        else
                        {
                            act = "開放";
                        }
                        if (operate_type.Equals("61"))
                        {
                            note = "全部開停盤操作";
                        }
                        if (operate_type.Equals("62"))
                        {
                            note = "當前頁開停盤操作";
                            string str19 = "";
                            str19 = this.get_playpage_name_kc(lotteryID, playpage);
                            if (str19 != "")
                            {
                                note = note + "=>" + str19;
                            }
                        }
                        if (operate_type.Equals("8"))
                        {
                            note = "快捷鍵開停盤操作";
                        }
                    }
                    else
                    {
                        category = _pcdd.get_category();
                        str5 = _pcdd.get_play_name();
                        str6 = _pcdd.get_put_amount();
                        num = int.Parse(oddsid);
                        note = note + "," + str6;
                        if (_pcdd.get_is_open() == 0)
                        {
                            str7 = "開盤";
                            str8 = "停盤";
                            act = "停押";
                        }
                        else
                        {
                            str7 = "停盤";
                            str8 = "開盤";
                            act = "開放";
                        }
                    }
                    DataTable table8 = CallBLL.cz_phase_pcdd_bll.IsOpenPhaseByTime(DateTime.Now);
                    if (table8 != null)
                    {
                        str11 = table8.Rows[0]["phase"].ToString();
                    }
                    goto Label_22F5;
                }
                case 8:
                {
                    cz_odds_pkbjl _pkbjl = null;
                    if (!string.IsNullOrEmpty(oddsid) && (oddsid.IndexOf(',') < 0))
                    {
                        _pkbjl = CallBLL.cz_odds_pkbjl_bll.GetModel(Convert.ToInt32(oddsid));
                    }
                    if (_pkbjl == null)
                    {
                        if (is_open.Equals("0"))
                        {
                            act = "停押";
                        }
                        else
                        {
                            act = "開放";
                        }
                        if (operate_type.Equals("61"))
                        {
                            note = "全部開停盤操作";
                        }
                        if (operate_type.Equals("62"))
                        {
                            note = "當前頁開停盤操作";
                            string str20 = "";
                            str20 = this.get_playpage_name_kc(lotteryID, playpage);
                            if (str20 != "")
                            {
                                note = note + "=>" + str20;
                            }
                        }
                        if (operate_type.Equals("8"))
                        {
                            note = "快捷鍵開停盤操作";
                        }
                    }
                    else
                    {
                        category = _pkbjl.get_category();
                        str5 = _pkbjl.get_play_name();
                        str6 = _pkbjl.get_put_amount();
                        num = int.Parse(oddsid);
                        note = note + "," + str6;
                        if (_pkbjl.get_is_open() == 0)
                        {
                            str7 = "開盤";
                            str8 = "停盤";
                            act = "停押";
                        }
                        else
                        {
                            str7 = "停盤";
                            str8 = "開盤";
                            act = "開放";
                        }
                    }
                    DataTable table9 = CallBLL.cz_phase_pkbjl_bll.IsOpenPhaseByTime(DateTime.Now);
                    if (table9 != null)
                    {
                        str11 = table9.Rows[0]["phase"].ToString();
                    }
                    goto Label_22F5;
                }
                case 9:
                {
                    cz_odds_xyft5 _xyft = null;
                    if (!string.IsNullOrEmpty(oddsid) && (oddsid.IndexOf(',') < 0))
                    {
                        _xyft = CallBLL.cz_odds_xyft5_bll.GetModel(Convert.ToInt32(oddsid));
                    }
                    if (_xyft == null)
                    {
                        if (is_open.Equals("0"))
                        {
                            act = "停押";
                        }
                        else
                        {
                            act = "開放";
                        }
                        if (operate_type.Equals("61"))
                        {
                            note = "全部開停盤操作";
                        }
                        if (operate_type.Equals("62"))
                        {
                            note = "當前頁開停盤操作";
                            string str21 = "";
                            str21 = this.get_playpage_name_kc(lotteryID, playpage);
                            if (str21 != "")
                            {
                                note = note + "=>" + str21;
                            }
                        }
                        if (operate_type.Equals("8"))
                        {
                            note = "快捷鍵開停盤操作";
                        }
                    }
                    else
                    {
                        category = _xyft.get_category();
                        str5 = _xyft.get_play_name();
                        str6 = _xyft.get_put_amount();
                        num = int.Parse(oddsid);
                        note = note + "," + str6;
                        if (_xyft.get_is_open() == 0)
                        {
                            str7 = "開盤";
                            str8 = "停盤";
                            act = "停押";
                        }
                        else
                        {
                            str7 = "停盤";
                            str8 = "開盤";
                            act = "開放";
                        }
                    }
                    DataTable table10 = CallBLL.cz_phase_xyft5_bll.IsOpenPhaseByTime(DateTime.Now);
                    if (table10 != null)
                    {
                        str11 = table10.Rows[0]["phase"].ToString();
                    }
                    goto Label_22F5;
                }
                case 10:
                {
                    cz_odds_jscar _jscar = null;
                    if (!string.IsNullOrEmpty(oddsid) && (oddsid.IndexOf(',') < 0))
                    {
                        _jscar = CallBLL.cz_odds_jscar_bll.GetModel(Convert.ToInt32(oddsid));
                    }
                    if (_jscar == null)
                    {
                        if (is_open.Equals("0"))
                        {
                            act = "停押";
                        }
                        else
                        {
                            act = "開放";
                        }
                        if (operate_type.Equals("61"))
                        {
                            note = "全部開停盤操作";
                        }
                        if (operate_type.Equals("62"))
                        {
                            note = "當前頁開停盤操作";
                            string str22 = "";
                            str22 = this.get_playpage_name_kc(lotteryID, playpage);
                            if (str22 != "")
                            {
                                note = note + "=>" + str22;
                            }
                        }
                        if (operate_type.Equals("8"))
                        {
                            note = "快捷鍵開停盤操作";
                        }
                    }
                    else
                    {
                        category = _jscar.get_category();
                        str5 = _jscar.get_play_name();
                        str6 = _jscar.get_put_amount();
                        num = int.Parse(oddsid);
                        note = note + "," + str6;
                        if (_jscar.get_is_open() == 0)
                        {
                            str7 = "開盤";
                            str8 = "停盤";
                            act = "停押";
                        }
                        else
                        {
                            str7 = "停盤";
                            str8 = "開盤";
                            act = "開放";
                        }
                    }
                    DataTable table11 = CallBLL.cz_phase_jscar_bll.IsOpenPhaseByTime(DateTime.Now);
                    if (table11 != null)
                    {
                        str11 = table11.Rows[0]["phase"].ToString();
                    }
                    goto Label_22F5;
                }
                case 11:
                {
                    cz_odds_speed5 _speed = null;
                    if (!string.IsNullOrEmpty(oddsid) && (oddsid.IndexOf(',') < 0))
                    {
                        _speed = CallBLL.cz_odds_speed5_bll.GetModel(Convert.ToInt32(oddsid));
                    }
                    if (_speed == null)
                    {
                        if (is_open.Equals("0"))
                        {
                            act = "停押";
                        }
                        else
                        {
                            act = "開放";
                        }
                        if (operate_type.Equals("61"))
                        {
                            note = "全部開停盤操作";
                        }
                        if (operate_type.Equals("62"))
                        {
                            note = "當前頁開停盤操作";
                            string str23 = "";
                            str23 = this.get_playpage_name_kc(lotteryID, playpage);
                            if (str23 != "")
                            {
                                note = note + "=>" + str23;
                            }
                        }
                        if (operate_type.Equals("8"))
                        {
                            note = "快捷鍵開停盤操作";
                        }
                    }
                    else
                    {
                        category = _speed.get_category();
                        str5 = _speed.get_play_name();
                        str6 = _speed.get_put_amount();
                        num = int.Parse(oddsid);
                        note = note + "," + str6;
                        if (_speed.get_is_open() == 0)
                        {
                            str7 = "開盤";
                            str8 = "停盤";
                            act = "停押";
                        }
                        else
                        {
                            str7 = "停盤";
                            str8 = "開盤";
                            act = "開放";
                        }
                    }
                    DataTable table12 = CallBLL.cz_phase_speed5_bll.IsOpenPhaseByTime(DateTime.Now);
                    if (table12 != null)
                    {
                        str11 = table12.Rows[0]["phase"].ToString();
                    }
                    goto Label_22F5;
                }
                case 12:
                {
                    cz_odds_jspk10 _jspk = null;
                    if (!string.IsNullOrEmpty(oddsid) && (oddsid.IndexOf(',') < 0))
                    {
                        _jspk = CallBLL.cz_odds_jspk10_bll.GetModel(Convert.ToInt32(oddsid));
                    }
                    if (_jspk == null)
                    {
                        if (is_open.Equals("0"))
                        {
                            act = "停押";
                        }
                        else
                        {
                            act = "開放";
                        }
                        if (operate_type.Equals("61"))
                        {
                            note = "全部開停盤操作";
                        }
                        if (operate_type.Equals("62"))
                        {
                            note = "當前頁開停盤操作";
                            string str24 = "";
                            str24 = this.get_playpage_name_kc(lotteryID, playpage);
                            if (str24 != "")
                            {
                                note = note + "=>" + str24;
                            }
                        }
                        if (operate_type.Equals("8"))
                        {
                            note = "快捷鍵開停盤操作";
                        }
                    }
                    else
                    {
                        category = _jspk.get_category();
                        str5 = _jspk.get_play_name();
                        str6 = _jspk.get_put_amount();
                        num = int.Parse(oddsid);
                        note = note + "," + str6;
                        if (_jspk.get_is_open() == 0)
                        {
                            str7 = "開盤";
                            str8 = "停盤";
                            act = "停押";
                        }
                        else
                        {
                            str7 = "停盤";
                            str8 = "開盤";
                            act = "開放";
                        }
                    }
                    DataTable table13 = CallBLL.cz_phase_jspk10_bll.IsOpenPhaseByTime(DateTime.Now);
                    if (table13 != null)
                    {
                        str11 = table13.Rows[0]["phase"].ToString();
                    }
                    goto Label_22F5;
                }
                case 13:
                {
                    cz_odds_jscqsc _jscqsc = null;
                    if (!string.IsNullOrEmpty(oddsid) && (oddsid.IndexOf(',') < 0))
                    {
                        _jscqsc = CallBLL.cz_odds_jscqsc_bll.GetModel(Convert.ToInt32(oddsid));
                    }
                    if (_jscqsc == null)
                    {
                        if (is_open.Equals("0"))
                        {
                            act = "停押";
                        }
                        else
                        {
                            act = "開放";
                        }
                        if (operate_type.Equals("61"))
                        {
                            note = "全部開停盤操作";
                        }
                        if (operate_type.Equals("62"))
                        {
                            note = "當前頁開停盤操作";
                            string str25 = "";
                            str25 = this.get_playpage_name_kc(lotteryID, playpage);
                            if (str25 != "")
                            {
                                note = note + "=>" + str25;
                            }
                        }
                        if (operate_type.Equals("8"))
                        {
                            note = "快捷鍵開停盤操作";
                        }
                    }
                    else
                    {
                        category = _jscqsc.get_category();
                        str5 = _jscqsc.get_play_name();
                        str6 = _jscqsc.get_put_amount();
                        num = int.Parse(oddsid);
                        note = note + "," + str6;
                        if (_jscqsc.get_is_open() == 0)
                        {
                            str7 = "開盤";
                            str8 = "停盤";
                            act = "停押";
                        }
                        else
                        {
                            str7 = "停盤";
                            str8 = "開盤";
                            act = "開放";
                        }
                    }
                    DataTable table14 = CallBLL.cz_phase_jscqsc_bll.IsOpenPhaseByTime(DateTime.Now);
                    if (table14 != null)
                    {
                        str11 = table14.Rows[0]["phase"].ToString();
                    }
                    goto Label_22F5;
                }
                case 14:
                {
                    cz_odds_jssfc _jssfc = null;
                    if (!string.IsNullOrEmpty(oddsid) && (oddsid.IndexOf(',') < 0))
                    {
                        _jssfc = CallBLL.cz_odds_jssfc_bll.GetModel(Convert.ToInt32(oddsid));
                    }
                    if (_jssfc == null)
                    {
                        if (is_open.Equals("0"))
                        {
                            act = "停押";
                        }
                        else
                        {
                            act = "開放";
                        }
                        if (operate_type.Equals("61"))
                        {
                            note = "全部開停盤操作";
                        }
                        if (operate_type.Equals("62"))
                        {
                            note = "當前頁開停盤操作";
                            string str26 = "";
                            str26 = this.get_playpage_name_kc(lotteryID, playpage);
                            if (str26 != "")
                            {
                                note = note + "=>" + str26;
                            }
                        }
                        if (operate_type.Equals("8"))
                        {
                            note = "快捷鍵開停盤操作";
                        }
                    }
                    else
                    {
                        category = _jssfc.get_category();
                        str5 = _jssfc.get_play_name();
                        str6 = _jssfc.get_put_amount();
                        num = int.Parse(oddsid);
                        note = note + "," + str6;
                        int? nullable15 = new int?(_jssfc.get_is_open());
                        if (nullable15 == 0)
                        {
                            str7 = "開盤";
                            str8 = "停盤";
                            act = "停押";
                        }
                        else
                        {
                            str7 = "停盤";
                            str8 = "開盤";
                            act = "開放";
                        }
                    }
                    DataTable table15 = CallBLL.cz_phase_jssfc_bll.IsOpenPhaseByTime(DateTime.Now);
                    if (table15 != null)
                    {
                        str11 = table15.Rows[0]["phase"].ToString();
                    }
                    goto Label_22F5;
                }
                case 15:
                {
                    cz_odds_jsft2 _jsft = null;
                    if (!string.IsNullOrEmpty(oddsid) && (oddsid.IndexOf(',') < 0))
                    {
                        _jsft = CallBLL.cz_odds_jsft2_bll.GetModel(Convert.ToInt32(oddsid));
                    }
                    if (_jsft == null)
                    {
                        if (is_open.Equals("0"))
                        {
                            act = "停押";
                        }
                        else
                        {
                            act = "開放";
                        }
                        if (operate_type.Equals("61"))
                        {
                            note = "全部開停盤操作";
                        }
                        if (operate_type.Equals("62"))
                        {
                            note = "當前頁開停盤操作";
                            string str27 = "";
                            str27 = this.get_playpage_name_kc(lotteryID, playpage);
                            if (str27 != "")
                            {
                                note = note + "=>" + str27;
                            }
                        }
                        if (operate_type.Equals("8"))
                        {
                            note = "快捷鍵開停盤操作";
                        }
                    }
                    else
                    {
                        category = _jsft.get_category();
                        str5 = _jsft.get_play_name();
                        str6 = _jsft.get_put_amount();
                        num = int.Parse(oddsid);
                        note = note + "," + str6;
                        if (_jsft.get_is_open() == 0)
                        {
                            str7 = "開盤";
                            str8 = "停盤";
                            act = "停押";
                        }
                        else
                        {
                            str7 = "停盤";
                            str8 = "開盤";
                            act = "開放";
                        }
                    }
                    DataTable table16 = CallBLL.cz_phase_jsft2_bll.IsOpenPhaseByTime(DateTime.Now);
                    if (table16 != null)
                    {
                        str11 = table16.Rows[0]["phase"].ToString();
                    }
                    goto Label_22F5;
                }
                case 0x10:
                {
                    cz_odds_car168 _car = null;
                    if (!string.IsNullOrEmpty(oddsid) && (oddsid.IndexOf(',') < 0))
                    {
                        _car = CallBLL.cz_odds_car168_bll.GetModel(Convert.ToInt32(oddsid));
                    }
                    if (_car == null)
                    {
                        if (is_open.Equals("0"))
                        {
                            act = "停押";
                        }
                        else
                        {
                            act = "開放";
                        }
                        if (operate_type.Equals("61"))
                        {
                            note = "全部開停盤操作";
                        }
                        if (operate_type.Equals("62"))
                        {
                            note = "當前頁開停盤操作";
                            string str28 = "";
                            str28 = this.get_playpage_name_kc(lotteryID, playpage);
                            if (str28 != "")
                            {
                                note = note + "=>" + str28;
                            }
                        }
                        if (operate_type.Equals("8"))
                        {
                            note = "快捷鍵開停盤操作";
                        }
                    }
                    else
                    {
                        category = _car.get_category();
                        str5 = _car.get_play_name();
                        str6 = _car.get_put_amount();
                        num = int.Parse(oddsid);
                        note = note + "," + str6;
                        if (_car.get_is_open() == 0)
                        {
                            str7 = "開盤";
                            str8 = "停盤";
                            act = "停押";
                        }
                        else
                        {
                            str7 = "停盤";
                            str8 = "開盤";
                            act = "開放";
                        }
                    }
                    DataTable table17 = CallBLL.cz_phase_car168_bll.IsOpenPhaseByTime(DateTime.Now);
                    if (table17 != null)
                    {
                        str11 = table17.Rows[0]["phase"].ToString();
                    }
                    goto Label_22F5;
                }
                case 0x11:
                {
                    cz_odds_ssc168 _ssc = null;
                    if (!string.IsNullOrEmpty(oddsid) && (oddsid.IndexOf(',') < 0))
                    {
                        _ssc = CallBLL.cz_odds_ssc168_bll.GetModel(Convert.ToInt32(oddsid));
                    }
                    if (_ssc == null)
                    {
                        if (is_open.Equals("0"))
                        {
                            act = "停押";
                        }
                        else
                        {
                            act = "開放";
                        }
                        if (operate_type.Equals("61"))
                        {
                            note = "全部開停盤操作";
                        }
                        if (operate_type.Equals("62"))
                        {
                            note = "當前頁開停盤操作";
                            string str29 = "";
                            str29 = this.get_playpage_name_kc(lotteryID, playpage);
                            if (str29 != "")
                            {
                                note = note + "=>" + str29;
                            }
                        }
                        if (operate_type.Equals("8"))
                        {
                            note = "快捷鍵開停盤操作";
                        }
                    }
                    else
                    {
                        category = _ssc.get_category();
                        str5 = _ssc.get_play_name();
                        str6 = _ssc.get_put_amount();
                        num = int.Parse(oddsid);
                        note = note + "," + str6;
                        if (_ssc.get_is_open() == 0)
                        {
                            str7 = "開盤";
                            str8 = "停盤";
                            act = "停押";
                        }
                        else
                        {
                            str7 = "停盤";
                            str8 = "開盤";
                            act = "開放";
                        }
                    }
                    DataTable table18 = CallBLL.cz_phase_ssc168_bll.IsOpenPhaseByTime(DateTime.Now);
                    if (table18 != null)
                    {
                        str11 = table18.Rows[0]["phase"].ToString();
                    }
                    goto Label_22F5;
                }
                case 0x12:
                {
                    cz_odds_vrcar _vrcar = null;
                    if (!string.IsNullOrEmpty(oddsid) && (oddsid.IndexOf(',') < 0))
                    {
                        _vrcar = CallBLL.cz_odds_vrcar_bll.GetModel(Convert.ToInt32(oddsid));
                    }
                    if (_vrcar == null)
                    {
                        if (is_open.Equals("0"))
                        {
                            act = "停押";
                        }
                        else
                        {
                            act = "開放";
                        }
                        if (operate_type.Equals("61"))
                        {
                            note = "全部開停盤操作";
                        }
                        if (operate_type.Equals("62"))
                        {
                            note = "當前頁開停盤操作";
                            string str30 = "";
                            str30 = this.get_playpage_name_kc(lotteryID, playpage);
                            if (str30 != "")
                            {
                                note = note + "=>" + str30;
                            }
                        }
                        if (operate_type.Equals("8"))
                        {
                            note = "快捷鍵開停盤操作";
                        }
                    }
                    else
                    {
                        category = _vrcar.get_category();
                        str5 = _vrcar.get_play_name();
                        str6 = _vrcar.get_put_amount();
                        num = int.Parse(oddsid);
                        note = note + "," + str6;
                        if (_vrcar.get_is_open() == 0)
                        {
                            str7 = "開盤";
                            str8 = "停盤";
                            act = "停押";
                        }
                        else
                        {
                            str7 = "停盤";
                            str8 = "開盤";
                            act = "開放";
                        }
                    }
                    DataTable table19 = CallBLL.cz_phase_vrcar_bll.IsOpenPhaseByTime(DateTime.Now);
                    if (table19 != null)
                    {
                        str11 = table19.Rows[0]["phase"].ToString();
                    }
                    goto Label_22F5;
                }
                case 0x13:
                {
                    cz_odds_vrssc _vrssc = null;
                    if (!string.IsNullOrEmpty(oddsid) && (oddsid.IndexOf(',') < 0))
                    {
                        _vrssc = CallBLL.cz_odds_vrssc_bll.GetModel(Convert.ToInt32(oddsid));
                    }
                    if (_vrssc == null)
                    {
                        if (is_open.Equals("0"))
                        {
                            act = "停押";
                        }
                        else
                        {
                            act = "開放";
                        }
                        if (operate_type.Equals("61"))
                        {
                            note = "全部開停盤操作";
                        }
                        if (operate_type.Equals("62"))
                        {
                            note = "當前頁開停盤操作";
                            string str31 = "";
                            str31 = this.get_playpage_name_kc(lotteryID, playpage);
                            if (str31 != "")
                            {
                                note = note + "=>" + str31;
                            }
                        }
                        if (operate_type.Equals("8"))
                        {
                            note = "快捷鍵開停盤操作";
                        }
                    }
                    else
                    {
                        category = _vrssc.get_category();
                        str5 = _vrssc.get_play_name();
                        str6 = _vrssc.get_put_amount();
                        num = int.Parse(oddsid);
                        note = note + "," + str6;
                        if (_vrssc.get_is_open() == 0)
                        {
                            str7 = "開盤";
                            str8 = "停盤";
                            act = "停押";
                        }
                        else
                        {
                            str7 = "停盤";
                            str8 = "開盤";
                            act = "開放";
                        }
                    }
                    DataTable table20 = CallBLL.cz_phase_vrssc_bll.IsOpenPhaseByTime(DateTime.Now);
                    if (table20 != null)
                    {
                        str11 = table20.Rows[0]["phase"].ToString();
                    }
                    goto Label_22F5;
                }
                case 20:
                {
                    cz_odds_xyftoa _xyftoa = null;
                    if (!string.IsNullOrEmpty(oddsid) && (oddsid.IndexOf(',') < 0))
                    {
                        _xyftoa = CallBLL.cz_odds_xyftoa_bll.GetModel(Convert.ToInt32(oddsid));
                    }
                    if (_xyftoa == null)
                    {
                        if (is_open.Equals("0"))
                        {
                            act = "停押";
                        }
                        else
                        {
                            act = "開放";
                        }
                        if (operate_type.Equals("61"))
                        {
                            note = "全部開停盤操作";
                        }
                        if (operate_type.Equals("62"))
                        {
                            note = "當前頁開停盤操作";
                            string str32 = "";
                            str32 = this.get_playpage_name_kc(lotteryID, playpage);
                            if (str32 != "")
                            {
                                note = note + "=>" + str32;
                            }
                        }
                        if (operate_type.Equals("8"))
                        {
                            note = "快捷鍵開停盤操作";
                        }
                    }
                    else
                    {
                        category = _xyftoa.get_category();
                        str5 = _xyftoa.get_play_name();
                        str6 = _xyftoa.get_put_amount();
                        num = int.Parse(oddsid);
                        note = note + "," + str6;
                        if (_xyftoa.get_is_open() == 0)
                        {
                            str7 = "開盤";
                            str8 = "停盤";
                            act = "停押";
                        }
                        else
                        {
                            str7 = "停盤";
                            str8 = "開盤";
                            act = "開放";
                        }
                    }
                    DataTable table21 = CallBLL.cz_phase_xyftoa_bll.IsOpenPhaseByTime(DateTime.Now);
                    if (table21 != null)
                    {
                        str11 = table21.Rows[0]["phase"].ToString();
                    }
                    goto Label_22F5;
                }
                case 0x15:
                {
                    cz_odds_xyftsg _xyftsg = null;
                    if (!string.IsNullOrEmpty(oddsid) && (oddsid.IndexOf(',') < 0))
                    {
                        _xyftsg = CallBLL.cz_odds_xyftsg_bll.GetModel(Convert.ToInt32(oddsid));
                    }
                    if (_xyftsg == null)
                    {
                        if (is_open.Equals("0"))
                        {
                            act = "停押";
                        }
                        else
                        {
                            act = "開放";
                        }
                        if (operate_type.Equals("61"))
                        {
                            note = "全部開停盤操作";
                        }
                        if (operate_type.Equals("62"))
                        {
                            note = "當前頁開停盤操作";
                            string str33 = "";
                            str33 = this.get_playpage_name_kc(lotteryID, playpage);
                            if (str33 != "")
                            {
                                note = note + "=>" + str33;
                            }
                        }
                        if (operate_type.Equals("8"))
                        {
                            note = "快捷鍵開停盤操作";
                        }
                    }
                    else
                    {
                        category = _xyftsg.get_category();
                        str5 = _xyftsg.get_play_name();
                        str6 = _xyftsg.get_put_amount();
                        num = int.Parse(oddsid);
                        note = note + "," + str6;
                        if (_xyftsg.get_is_open() == 0)
                        {
                            str7 = "開盤";
                            str8 = "停盤";
                            act = "停押";
                        }
                        else
                        {
                            str7 = "停盤";
                            str8 = "開盤";
                            act = "開放";
                        }
                    }
                    DataTable table22 = CallBLL.cz_phase_xyftsg_bll.IsOpenPhaseByTime(DateTime.Now);
                    if (table22 != null)
                    {
                        str11 = table22.Rows[0]["phase"].ToString();
                    }
                    goto Label_22F5;
                }
                case 0x16:
                {
                    cz_odds_happycar _happycar = null;
                    if (!string.IsNullOrEmpty(oddsid) && (oddsid.IndexOf(',') < 0))
                    {
                        _happycar = CallBLL.cz_odds_happycar_bll.GetModel(Convert.ToInt32(oddsid));
                    }
                    if (_happycar == null)
                    {
                        if (is_open.Equals("0"))
                        {
                            act = "停押";
                        }
                        else
                        {
                            act = "開放";
                        }
                        if (operate_type.Equals("61"))
                        {
                            note = "全部開停盤操作";
                        }
                        if (operate_type.Equals("62"))
                        {
                            note = "當前頁開停盤操作";
                            string str34 = "";
                            str34 = this.get_playpage_name_kc(lotteryID, playpage);
                            if (str34 != "")
                            {
                                note = note + "=>" + str34;
                            }
                        }
                        if (operate_type.Equals("8"))
                        {
                            note = "快捷鍵開停盤操作";
                        }
                    }
                    else
                    {
                        category = _happycar.get_category();
                        str5 = _happycar.get_play_name();
                        str6 = _happycar.get_put_amount();
                        num = int.Parse(oddsid);
                        note = note + "," + str6;
                        if (_happycar.get_is_open() == 0)
                        {
                            str7 = "開盤";
                            str8 = "停盤";
                            act = "停押";
                        }
                        else
                        {
                            str7 = "停盤";
                            str8 = "開盤";
                            act = "開放";
                        }
                    }
                    DataTable table23 = CallBLL.cz_phase_happycar_bll.IsOpenPhaseByTime(DateTime.Now);
                    if (table23 != null)
                    {
                        str11 = table23.Rows[0]["phase"].ToString();
                    }
                    goto Label_22F5;
                }
                default:
                    goto Label_22F5;
            }
            DataTable table = CallBLL.cz_phase_kl10_bll.IsOpenPhaseByTime(DateTime.Now);
            if (table != null)
            {
                str11 = table.Rows[0]["phase"].ToString();
            }
        Label_22F5:
            list = new List<SqlParameter>();
            string str35 = this.add_sys_log(str, str2, category, str5, str6, gameNameByID, str11, act, num, str7, str8, note, num2, lotteryID, ref list);
            CallBLL.cz_system_log_bll.executte_sql(str35, list.ToArray());
        }

        public void kjj_log(DataTable oldTable, DataTable newTable, string operate_type)
        {
            string str = this.get_master_name();
            string str2 = this.get_children_name();
            string gameNameByID = base.GetGameNameByID(100.ToString());
            string category = "";
            string str5 = "";
            string str6 = "";
            string s = "";
            string str8 = "";
            int num = 0;
            string note = "快捷鍵微调" + str6;
            string act = "";
            int num2 = Convert.ToInt32((LSEnums.LogTypeID) 0);
            if (operate_type.Equals("7"))
            {
                note = "快捷鍵手工输入赔率值,微調" + str6;
            }
            string str11 = "";
            cz_phase_six currentPhase = CallBLL.cz_phase_six_bll.GetCurrentPhase();
            if (currentPhase != null)
            {
                str11 = currentPhase.get_phase().ToString();
            }
            List<CommandInfo> list = new List<CommandInfo>();
            int num3 = 0;
            foreach (DataRow row in oldTable.Rows)
            {
                category = row["category"].ToString();
                str5 = row["play_name"].ToString();
                str6 = row["put_amount"].ToString();
                s = row["current_odds"].ToString();
                str8 = newTable.Rows[num3]["current_odds"].ToString();
                num = int.Parse(row["odds_id"].ToString());
                if (s != str8)
                {
                    if (double.Parse(s) > double.Parse(str8))
                    {
                        act = "降賠率";
                    }
                    else
                    {
                        act = "升賠率";
                    }
                    List<SqlParameter> paramList = new List<SqlParameter>();
                    string str12 = this.add_sys_log(str, str2, category, str5, str6, gameNameByID, str11, act, num, s, str8, note, num2, 100, ref paramList);
                    CommandInfo item = new CommandInfo {
                        CommandText = str12,
                        Parameters = paramList.ToArray()
                    };
                    list.Add(item);
                }
                num3++;
            }
            if (list.Count > 0)
            {
                CallBLL.cz_system_log_bll.execute_sql_tran(list);
            }
        }

        public void kl10_kjj_log(DataTable oldTable, DataTable newTable, string operate_type)
        {
            string str = this.get_master_name();
            string str2 = this.get_children_name();
            string gameNameByID = base.GetGameNameByID(0.ToString());
            string category = "";
            string str5 = "";
            string str6 = "";
            string s = "";
            string str8 = "";
            int num = 0;
            string note = "快捷鍵微调" + str6;
            string act = "";
            int num2 = Convert.ToInt32((LSEnums.LogTypeID) 0);
            if (operate_type.Equals("7"))
            {
                note = "快捷鍵手工输入赔率值,微調" + str6;
            }
            string str11 = "";
            DataTable table = CallBLL.cz_phase_kl10_bll.IsOpenPhaseByTime(DateTime.Now);
            if (table != null)
            {
                str11 = table.Rows[0]["phase"].ToString();
            }
            List<CommandInfo> list = new List<CommandInfo>();
            int num3 = 0;
            foreach (DataRow row in oldTable.Rows)
            {
                category = row["category"].ToString();
                str5 = row["play_name"].ToString();
                str6 = row["put_amount"].ToString();
                s = row["current_odds"].ToString();
                str8 = newTable.Rows[num3]["current_odds"].ToString();
                num = int.Parse(row["odds_id"].ToString());
                if (s != str8)
                {
                    if (double.Parse(s) > double.Parse(str8))
                    {
                        act = "降賠率";
                    }
                    else
                    {
                        act = "升賠率";
                    }
                    List<SqlParameter> paramList = new List<SqlParameter>();
                    string str12 = this.add_sys_log(str, str2, category, str5, str6, gameNameByID, str11, act, num, s, str8, note, num2, 0, ref paramList);
                    CommandInfo item = new CommandInfo {
                        CommandText = str12,
                        Parameters = paramList.ToArray()
                    };
                    list.Add(item);
                }
                num3++;
            }
            if (list.Count > 0)
            {
                CallBLL.cz_system_log_bll.execute_sql_tran(list);
            }
        }

        public void kl10_log(cz_odds_kl10 oldModel, cz_odds_kl10 newModel, string operate_type)
        {
            string str = this.get_master_name();
            string str2 = this.get_children_name();
            string gameNameByID = base.GetGameNameByID(0.ToString());
            string category = oldModel.get_category();
            string str5 = oldModel.get_play_name();
            string str6 = oldModel.get_put_amount();
            string s = oldModel.get_current_odds();
            string str8 = newModel.get_current_odds();
            int num = oldModel.get_odds_id();
            string note = "微調" + str6;
            string act = "";
            int num2 = Convert.ToInt32((LSEnums.LogTypeID) 0);
            if (operate_type.Equals("3"))
            {
                note = "手工输入赔率值,微調" + str6;
            }
            string str11 = "";
            DataTable table = CallBLL.cz_phase_kl10_bll.IsOpenPhaseByTime(DateTime.Now);
            if (table != null)
            {
                str11 = table.Rows[0]["phase"].ToString();
            }
            if (s != str8)
            {
                if (double.Parse(s) > double.Parse(str8))
                {
                    act = "降賠率";
                }
                else
                {
                    act = "升賠率";
                }
                List<SqlParameter> paramList = new List<SqlParameter>();
                string str12 = this.add_sys_log(str, str2, category, str5, str6, gameNameByID, str11, act, num, s, str8, note, num2, 0, ref paramList);
                CallBLL.cz_system_log_bll.executte_sql(str12, paramList.ToArray());
            }
        }

        public void kl8_kjj_log(DataTable oldTable, DataTable newTable, string operate_type)
        {
            string str = this.get_master_name();
            string str2 = this.get_children_name();
            string gameNameByID = base.GetGameNameByID(5.ToString());
            string category = "";
            string str5 = "";
            string str6 = "";
            string s = "";
            string str8 = "";
            int num = 0;
            string note = "快捷鍵微调" + str6;
            string act = "";
            int num2 = Convert.ToInt32((LSEnums.LogTypeID) 0);
            if (operate_type.Equals("7"))
            {
                note = "快捷鍵手工输入赔率值,微調" + str6;
            }
            string str11 = "";
            DataTable table = CallBLL.cz_phase_kl8_bll.IsOpenPhaseByTime(DateTime.Now);
            if (table != null)
            {
                str11 = table.Rows[0]["phase"].ToString();
            }
            List<CommandInfo> list = new List<CommandInfo>();
            int num3 = 0;
            foreach (DataRow row in oldTable.Rows)
            {
                category = row["category"].ToString();
                str5 = row["play_name"].ToString();
                str6 = row["put_amount"].ToString();
                s = row["current_odds"].ToString();
                str8 = newTable.Rows[num3]["current_odds"].ToString();
                num = int.Parse(row["odds_id"].ToString());
                if (s != str8)
                {
                    if (double.Parse(s) > double.Parse(str8))
                    {
                        act = "降賠率";
                    }
                    else
                    {
                        act = "升賠率";
                    }
                    List<SqlParameter> paramList = new List<SqlParameter>();
                    string str12 = this.add_sys_log(str, str2, category, str5, str6, gameNameByID, str11, act, num, s, str8, note, num2, 5, ref paramList);
                    CommandInfo item = new CommandInfo {
                        CommandText = str12,
                        Parameters = paramList.ToArray()
                    };
                    list.Add(item);
                }
                num3++;
            }
            if (list.Count > 0)
            {
                CallBLL.cz_system_log_bll.execute_sql_tran(list);
            }
        }

        public void kl8_log(cz_odds_kl8 oldModel, cz_odds_kl8 newModel, string operate_type)
        {
            string str = this.get_master_name();
            string str2 = this.get_children_name();
            string gameNameByID = base.GetGameNameByID(5.ToString());
            string category = oldModel.get_category();
            string str5 = oldModel.get_play_name();
            string str6 = oldModel.get_put_amount();
            string s = oldModel.get_current_odds();
            string str8 = newModel.get_current_odds();
            int num = oldModel.get_odds_id();
            string note = "微調" + str6;
            string act = "";
            int num2 = Convert.ToInt32((LSEnums.LogTypeID) 0);
            if (operate_type.Equals("3"))
            {
                note = "手工输入赔率值,微調" + str6;
            }
            string str11 = "";
            DataTable table = CallBLL.cz_phase_kl8_bll.IsOpenPhaseByTime(DateTime.Now);
            if (table != null)
            {
                str11 = table.Rows[0]["phase"].ToString();
            }
            if (s != str8)
            {
                if (double.Parse(s) > double.Parse(str8))
                {
                    act = "降賠率";
                }
                else
                {
                    act = "升賠率";
                }
                List<SqlParameter> paramList = new List<SqlParameter>();
                string str12 = this.add_sys_log(str, str2, category, str5, str6, gameNameByID, str11, act, num, s, str8, note, num2, 5, ref paramList);
                CallBLL.cz_system_log_bll.executte_sql(str12, paramList.ToArray());
            }
        }

        public string LayerOperate(int isReload, string strUrl, int isClose)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("<script>");
            builder.AppendFormat("top.saveReload({0}, '{1}', {2});", isReload, strUrl, isClose);
            builder.Append("</script>");
            return builder.ToString();
        }

        public void lm_group_log(cz_odds_six oldModel, string old_pl, string new_pl, string operate_type)
        {
            if (!old_pl.Equals(new_pl))
            {
                string str = this.get_master_name();
                string str2 = this.get_children_name();
                string gameNameByID = base.GetGameNameByID(100.ToString());
                string category = oldModel.get_category();
                string str5 = oldModel.get_play_name();
                string str6 = oldModel.get_put_amount();
                double num = 0.0;
                double num2 = 0.0;
                int num3 = oldModel.get_odds_id();
                string str7 = "微調";
                string act = "";
                int num4 = Convert.ToInt32((LSEnums.LogTypeID) 0);
                if (operate_type.Equals("3"))
                {
                    str7 = "手工输入微調值,微調";
                }
                string str9 = "";
                cz_phase_six currentPhase = CallBLL.cz_phase_six_bll.GetCurrentPhase();
                if (currentPhase != null)
                {
                    str9 = currentPhase.get_phase().ToString();
                }
                if (old_pl.Split(new char[] { ',' }).Length > 1)
                {
                    string[] strArray = old_pl.Split(new char[] { ',' });
                    string[] strArray2 = new_pl.Split(new char[] { ',' });
                    string[] strArray3 = str6.Split(new char[] { ',' });
                    if (strArray[0].Equals(strArray2[0]))
                    {
                        num = double.Parse(strArray[1]);
                        num2 = double.Parse(strArray2[1]);
                        str7 = string.Format("{0}{1}賠率", str7, strArray3[1]);
                    }
                    else
                    {
                        num = double.Parse(strArray[0]);
                        num2 = double.Parse(strArray2[0]);
                        str7 = string.Format("{0}{1}賠率", str7, strArray3[0]);
                    }
                }
                else
                {
                    num = double.Parse(old_pl);
                    num2 = double.Parse(new_pl);
                }
                if (num > num2)
                {
                    act = "降賠率";
                }
                else
                {
                    act = "升賠率";
                }
                if (num != num2)
                {
                    List<SqlParameter> paramList = new List<SqlParameter>();
                    string str10 = this.add_sys_log(str, str2, category, str5, str6, gameNameByID, str9, act, num3, old_pl, new_pl, str7, num4, 100, ref paramList);
                    CallBLL.cz_system_log_bll.executte_sql(str10, paramList.ToArray());
                }
            }
        }

        public void lm_group_log_ab(cz_odds_six oldModel, string old_pl, string new_pl, string operate_type)
        {
            if (!old_pl.Equals(new_pl))
            {
                string str = this.get_master_name();
                string str2 = this.get_children_name();
                string gameNameByID = base.GetGameNameByID(100.ToString());
                string category = oldModel.get_category();
                string str5 = oldModel.get_play_name();
                string str6 = oldModel.get_put_amount();
                double num = 0.0;
                double num2 = 0.0;
                int num3 = oldModel.get_odds_id();
                string str7 = "微調(聯動)";
                string act = "";
                int num4 = Convert.ToInt32((LSEnums.LogTypeID) 0);
                if (operate_type.Equals("3"))
                {
                    str7 = "手工输入微調值,微調";
                }
                string str9 = "";
                cz_phase_six currentPhase = CallBLL.cz_phase_six_bll.GetCurrentPhase();
                if (currentPhase != null)
                {
                    str9 = currentPhase.get_phase().ToString();
                }
                if (old_pl.Split(new char[] { ',' }).Length > 1)
                {
                    string[] strArray = old_pl.Split(new char[] { ',' });
                    string[] strArray2 = new_pl.Split(new char[] { ',' });
                    string[] strArray3 = str6.Split(new char[] { ',' });
                    if (strArray[0].Equals(strArray2[0]))
                    {
                        num = double.Parse(strArray[1]);
                        num2 = double.Parse(strArray2[1]);
                        str7 = string.Format("{0}{1}賠率", str7, strArray3[1]);
                    }
                    else
                    {
                        num = double.Parse(strArray[0]);
                        num2 = double.Parse(strArray2[0]);
                        str7 = string.Format("{0}{1}賠率", str7, strArray3[0]);
                    }
                }
                else
                {
                    num = double.Parse(old_pl);
                    num2 = double.Parse(new_pl);
                }
                if (num > num2)
                {
                    act = "降賠率";
                }
                else
                {
                    act = "升賠率";
                }
                if (num != num2)
                {
                    List<SqlParameter> paramList = new List<SqlParameter>();
                    string str10 = this.add_sys_log(str, str2, category, str5, str6, gameNameByID, str9, act, num3, old_pl, new_pl, str7, num4, 100, ref paramList);
                    CallBLL.cz_system_log_bll.executte_sql(str10, paramList.ToArray());
                }
            }
        }

        public void lm_group_set_log_kc(DataTable oldDT, DataTable newDT, ref bool isRestUserAgent)
        {
            string str = this.get_master_name();
            string str2 = this.get_children_name();
            string category = "";
            string str4 = "";
            string str5 = "";
            string str6 = "";
            int num = 0;
            string note = "系統參數設定";
            string act = "系統參數設定";
            int num2 = Convert.ToInt32((LSEnums.LogTypeID) 1);
            string str9 = "";
            int count = oldDT.Rows.Count;
            IEqualityComparer<DataRow> comparer = DataRowComparer.Default;
            for (int i = 0; i < count; i++)
            {
                str5 = "";
                str6 = "";
                DataRow x = oldDT.Rows[i];
                DataRow y = newDT.Rows[i];
                if (!comparer.Equals(x, y))
                {
                    string str10 = x["play_id"].ToString();
                    string str11 = x["current_num_count"].ToString();
                    string str12 = y["current_num_count"].ToString();
                    string str13 = x["max_num_group"].ToString();
                    string str14 = y["max_num_group"].ToString();
                    if (!str11.Equals(str12))
                    {
                        isRestUserAgent = true;
                        str5 = "連碼可選擇號碼數量:" + str11;
                        str6 = "連碼可選擇號碼數量:" + str12;
                    }
                    if (!str13.Equals(str14))
                    {
                        isRestUserAgent = true;
                        str5 = str5 + "  連碼可選擇號碼組數:" + str13;
                        str6 = str6 + "  連碼可選擇號碼組數:" + str14;
                    }
                    if (str10.Equals("71014"))
                    {
                        List<SqlParameter> paramList = new List<SqlParameter>();
                        int num5 = 7;
                        string str15 = this.add_sys_log(str, str2, category, x["play_name"].ToString(), str4, base.GetGameNameByID(num5.ToString()), str9, act, num, str5, str6, note, num2, 7, ref paramList);
                        CallBLL.cz_system_log_bll.executte_sql(str15, paramList.ToArray());
                    }
                    else if (str5 != "")
                    {
                        List<SqlParameter> list2 = new List<SqlParameter>();
                        int num6 = 0;
                        string str16 = this.add_sys_log(str, str2, category, x["play_name_kl10"].ToString(), str4, base.GetGameNameByID(num6.ToString()), str9, act, num, str5, str6, note, num2, 0, ref list2);
                        CallBLL.cz_system_log_bll.executte_sql(str16, list2.ToArray());
                        List<SqlParameter> list3 = new List<SqlParameter>();
                        int num7 = 3;
                        str16 = this.add_sys_log(str, str2, category, x["play_name_xync"].ToString(), str4, base.GetGameNameByID(num7.ToString()), str9, act, num, str5, str6, note, num2, 3, ref list3);
                        CallBLL.cz_system_log_bll.executte_sql(str16, list3.ToArray());
                    }
                }
            }
        }

        public void lm_group_set_log_kc(DataTable oldDT, DataTable newDT, int lottery_type)
        {
            string str = this.get_master_name();
            string str2 = this.get_children_name();
            string category = "";
            string str4 = "";
            string str5 = "";
            string str6 = "";
            int num = 0;
            string note = "系統參數設定";
            string act = "系統參數設定";
            int num2 = Convert.ToInt32((LSEnums.LogTypeID) 1);
            string str9 = "";
            int count = oldDT.Rows.Count;
            IEqualityComparer<DataRow> comparer = DataRowComparer.Default;
            for (int i = 0; i < count; i++)
            {
                str5 = "";
                str6 = "";
                DataRow x = oldDT.Rows[i];
                DataRow y = newDT.Rows[i];
                if (!comparer.Equals(x, y))
                {
                    x["play_id"].ToString();
                    string str10 = x["current_num_count"].ToString();
                    string str11 = y["current_num_count"].ToString();
                    string str12 = x["max_num_group"].ToString();
                    string str13 = y["max_num_group"].ToString();
                    if (!str10.Equals(str11))
                    {
                        str5 = "連碼可選擇號碼數量:" + str10;
                        str6 = "連碼可選擇號碼數量:" + str11;
                    }
                    if (!str12.Equals(str13))
                    {
                        str5 = str5 + "  連碼可選擇號碼組數:" + str12;
                        str6 = str6 + "  連碼可選擇號碼組數:" + str13;
                    }
                    List<SqlParameter> paramList = new List<SqlParameter>();
                    string str14 = this.add_sys_log(str, str2, category, x["play_name"].ToString(), str4, base.GetGameNameByID(lottery_type.ToString()), str9, act, num, str5, str6, note, num2, lottery_type, ref paramList);
                    CallBLL.cz_system_log_bll.executte_sql(str14, paramList.ToArray());
                }
            }
        }

        public void lm_group_set_log_six(DataTable oldDT, DataTable newDT, ref bool isRestUserAgent)
        {
            string str = this.get_master_name();
            string str2 = this.get_children_name();
            string gameNameByID = base.GetGameNameByID(100.ToString());
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
            int count = oldDT.Rows.Count;
            IEqualityComparer<DataRow> comparer = DataRowComparer.Default;
            for (int i = 0; i < count; i++)
            {
                str7 = "";
                str8 = "";
                DataRow x = oldDT.Rows[i];
                DataRow y = newDT.Rows[i];
                if (!comparer.Equals(x, y))
                {
                    str5 = x["play_name"].ToString();
                    x["play_id"].ToString();
                    string str12 = x["current_num_count"].ToString();
                    string str13 = y["current_num_count"].ToString();
                    string str14 = x["max_num_group"].ToString();
                    string str15 = y["max_num_group"].ToString();
                    if (!str12.Equals(str13))
                    {
                        isRestUserAgent = true;
                        str7 = "連碼可選擇號碼數量:" + str12;
                        str8 = "連碼可選擇號碼數量:" + str13;
                    }
                    if (!str14.Equals(str15))
                    {
                        isRestUserAgent = true;
                        str7 = str7 + "  連碼可選擇號碼組數:" + str14;
                        str8 = str8 + "  連碼可選擇號碼組數:" + str15;
                    }
                    if (str7 != "")
                    {
                        List<SqlParameter> paramList = new List<SqlParameter>();
                        string str16 = this.add_sys_log(str, str2, category, str5, str6, gameNameByID, str11, act, num, str7, str8, note, num2, 100, ref paramList);
                        CallBLL.cz_system_log_bll.executte_sql(str16, paramList.ToArray());
                    }
                }
            }
        }

        public void lm_number_log(cz_odds_six oldModel, double oldWT, double newWT, string number, string operate_type)
        {
            this.bz_number_log(oldModel, oldWT, newWT, number, operate_type);
        }

        public void lm_number_log_ab(cz_odds_six oldModel, double oldWT, double newWT, string number, string operate_type)
        {
            string str = this.get_master_name();
            string str2 = this.get_children_name();
            string gameNameByID = base.GetGameNameByID(100.ToString());
            string category = oldModel.get_category();
            string str5 = oldModel.get_play_name();
            string str6 = (int.Parse(number) < 10) ? ("0" + int.Parse(number)) : number.ToString();
            double num = oldWT;
            double num2 = newWT;
            int num3 = oldModel.get_odds_id();
            string note = "微調號碼(聯動)";
            string act = "";
            int num4 = Convert.ToInt32((LSEnums.LogTypeID) 0);
            if (operate_type.Equals("3"))
            {
                note = "手工输入微調值,微調號碼";
            }
            string str9 = "";
            cz_phase_six currentPhase = CallBLL.cz_phase_six_bll.GetCurrentPhase();
            if (currentPhase != null)
            {
                str9 = currentPhase.get_phase().ToString();
            }
            if (num != num2)
            {
                if (num > num2)
                {
                    act = "降賠率";
                }
                else
                {
                    act = "升賠率";
                }
                List<SqlParameter> paramList = new List<SqlParameter>();
                string str10 = this.add_sys_log(str, str2, category, str5, str6, gameNameByID, str9, act, num3, num.ToString(), num2.ToString(), note, num4, 100, ref paramList);
                CallBLL.cz_system_log_bll.executte_sql(str10, paramList.ToArray());
            }
        }

        public string LocationHref(string url)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("<script>");
            builder.AppendFormat(" location.href='{0}' ", url);
            builder.Append("</script>");
            return builder.ToString();
        }

        protected void log_user_reset_password(string u_name, string edit_master_name, string edit_child_name, string flagStr)
        {
            List<SqlParameter> paramList = new List<SqlParameter>();
            string str = this.add_user_change_log(u_name, 300, edit_master_name, edit_child_name, "密碼變動" + flagStr, "", "重置新密碼", ref paramList);
            CallBLL.cz_user_change_log_bll.executte_sql(str, paramList.ToArray());
        }

        public void lxyz_number_log(cz_odds_six oldModel, double oldWT, double newWT, string number, string operate_type)
        {
            string str = this.get_master_name();
            string str2 = this.get_children_name();
            string gameNameByID = base.GetGameNameByID(100.ToString());
            string category = oldModel.get_category();
            string str5 = oldModel.get_play_name();
            string sXNameBySxidText = FunctionSix.GetSXNameBySxidText(int.Parse(number));
            double num = oldWT;
            double num2 = newWT;
            int num3 = oldModel.get_odds_id();
            string note = "微調號碼";
            string act = "";
            int num4 = Convert.ToInt32((LSEnums.LogTypeID) 0);
            if (operate_type.Equals("3"))
            {
                note = "手工输入微調值,微調號碼";
            }
            string str9 = "";
            cz_phase_six currentPhase = CallBLL.cz_phase_six_bll.GetCurrentPhase();
            if (currentPhase != null)
            {
                str9 = currentPhase.get_phase().ToString();
            }
            if (num != num2)
            {
                if (num > num2)
                {
                    act = "降賠率";
                }
                else
                {
                    act = "升賠率";
                }
                List<SqlParameter> paramList = new List<SqlParameter>();
                string str10 = this.add_sys_log(str, str2, category, str5, sXNameBySxidText, gameNameByID, str9, act, num3, num.ToString(), num2.ToString(), note, num4, 100, ref paramList);
                CallBLL.cz_system_log_bll.executte_sql(str10, paramList.ToArray());
            }
        }

        protected void MasterUserSession(int ha)
        {
            if (CacheHelper.GetCache("user_FileCacheKey" + HttpContext.Current.Session["user_name"].ToString()) == null)
            {
                agent_userinfo_session _session = HttpContext.Current.Session[HttpContext.Current.Session["user_name"] + "lottery_session_user_info"] as agent_userinfo_session;
                agent_userinfo_session sessionLogin = this.GetSessionLogin();
                HttpContext.Current.Session[HttpContext.Current.Session["user_name"] + "lottery_session_user_info"] = sessionLogin;
                if (sessionLogin.get_u_psw() != _session.get_u_psw())
                {
                    string messageByCache = PageBase.GetMessageByCache("u100017", "MessageHint");
                    base.Response.Write("<script>alert('" + messageByCache + "');window.location=\"/Quit.aspx\"</script>");
                }
                if (sessionLogin.get_a_state().Equals(2))
                {
                    string str2 = PageBase.GetMessageByCache("u100020", "MessageHint");
                    base.Response.Write("<script>alert('" + str2 + "');window.location=\"/Quit.aspx\"</script>");
                }
                else
                {
                    switch (CallBLL.cz_users_bll.UpUserStatus(HttpContext.Current.Session["user_name"].ToString()))
                    {
                        case 2:
                        {
                            string str3 = PageBase.GetMessageByCache("u100020", "MessageHint");
                            base.Response.Write("<script>alert('" + str3 + "');window.location=\"/Quit.aspx\"</script>");
                            return;
                        }
                        case 1:
                        {
                            string str4 = PageBase.GetMessageByCache("u100021", "MessageHint");
                            HttpContext.Current.Session["user_state"] = 1;
                            base.Response.Write("<script>alert('" + str4 + "');window.location=\"/Report.aspx\"</script>");
                            break;
                        }
                        default:
                            HttpContext.Current.Session["user_state"] = 0;
                            if (sessionLogin.get_a_state().Equals(1))
                            {
                                string str5 = PageBase.GetMessageByCache("u100021", "MessageHint");
                                HttpContext.Current.Session["user_state"] = 1;
                                base.Response.Write("<script>alert('" + str5 + "');window.location=\"/Report.aspx\"</script>");
                            }
                            break;
                    }
                    CacheHelper.SetPublicFileCache("user_FileCacheKey" + HttpContext.Current.Session["user_name"].ToString(), sessionLogin, PageBase.GetPublicForderPath(FileCacheHelper.get_UserInfoCachesFileName()));
                }
            }
        }

        public void MemberPageBase_Load(object sender, EventArgs e)
        {
            if (HttpContext.Current.Session["user_name"] == null)
            {
                base.Response.Write("<script>top.location.href='/'</script>");
                base.Response.End();
            }
            string str = this.get_children_name();
            if (FileCacheHelper.get_RedisStatOnline().Equals(1))
            {
                if (this.Context.Request.Path.ToLower().IndexOf("resetpasswd.aspx") > 0)
                {
                    DateTime now = DateTime.Now;
                    cz_stat_online _online = new cz_stat_online();
                    _online.set_u_name((str == "") ? HttpContext.Current.Session["user_name"].ToString() : str);
                    _online.set_is_out(0);
                    _online.set_u_type(HttpContext.Current.Session["user_type"].ToString());
                    _online.set_ip(LSRequest.GetIP());
                    _online.set_first_time(new DateTime?(now));
                    _online.set_last_time(new DateTime?(now));
                    CallBLL.redisHelper.HashSet<cz_stat_online>("useronline:list", (str == "") ? HttpContext.Current.Session["user_name"].ToString() : str, _online);
                }
                if (CallBLL.redisHelper.HashExists("useronline:list", (str == "") ? HttpContext.Current.Session["user_name"].ToString() : str))
                {
                    cz_stat_online _online2 = CallBLL.redisHelper.HashGet<cz_stat_online>("useronline:list", (str == "") ? HttpContext.Current.Session["user_name"].ToString() : str);
                    if ((_online2 != null) && _online2.get_is_out().Equals(1))
                    {
                        this.Session.Abandon();
                        base.Response.Write("<script>top.location.href='/'</script>");
                        base.Response.End();
                    }
                }
                if (PageBase.IsNeedPopBrower((str == "") ? HttpContext.Current.Session["user_name"].ToString() : str))
                {
                    this.Session.Abandon();
                    base.Response.Write("<script>top.location.href='/'</script>");
                    base.Response.End();
                }
            }
            else if (FileCacheHelper.get_RedisStatOnline().Equals(2))
            {
                if (this.Context.Request.Path.ToLower().IndexOf("resetpasswd.aspx") > 0)
                {
                    DateTime time2 = DateTime.Now;
                    cz_stat_online _online3 = new cz_stat_online();
                    _online3.set_u_name((str == "") ? HttpContext.Current.Session["user_name"].ToString() : str);
                    _online3.set_is_out(0);
                    _online3.set_u_type(HttpContext.Current.Session["user_type"].ToString());
                    _online3.set_ip(LSRequest.GetIP());
                    _online3.set_first_time(new DateTime?(time2));
                    _online3.set_last_time(new DateTime?(time2));
                    using (RedisClient client = new RedisClient(RedisConnectSplit.get_RedisIP(), RedisConnectSplit.get_RedisPort(), RedisConnectSplit.get_RedisPassword(), (long) FileCacheHelper.get_GetRedisDBIndex()))
                    {
                        client.set_ConnectTimeout(int.Parse(RedisConnectSplit.get_RedisConnectTimeout()));
                        client.SetEntryInHash("useronline:list", (str == "") ? HttpContext.Current.Session["user_name"].ToString() : str, JsonHandle.ObjectToJson(_online3));
                    }
                }
                using (RedisClient client2 = new RedisClient(RedisConnectSplit.get_RedisIP(), RedisConnectSplit.get_RedisPort(), RedisConnectSplit.get_RedisPassword(), (long) FileCacheHelper.get_GetRedisDBIndex()))
                {
                    client2.set_ConnectTimeout(int.Parse(RedisConnectSplit.get_RedisConnectTimeout()));
                    if (client2.HashContainsEntry("useronline:list", (str == "") ? HttpContext.Current.Session["user_name"].ToString() : str))
                    {
                        cz_stat_online _online4 = JsonHandle.JsonToObject<cz_stat_online>(client2.GetValueFromHash("useronline:list", (str == "") ? HttpContext.Current.Session["user_name"].ToString() : str)) as cz_stat_online;
                        if ((_online4 != null) && _online4.get_is_out().Equals(1))
                        {
                            this.Session.Abandon();
                            base.Response.Write("<script>top.location.href='/'</script>");
                            base.Response.End();
                        }
                    }
                }
                if (PageBase.IsNeedPopBrower((str == "") ? HttpContext.Current.Session["user_name"].ToString() : str))
                {
                    this.Session.Abandon();
                    base.Response.Write("<script>top.location.href='/'</script>");
                    base.Response.End();
                }
            }
            else if (base.IsUserOut((str == "") ? HttpContext.Current.Session["user_name"].ToString() : str) || PageBase.IsNeedPopBrower((str == "") ? HttpContext.Current.Session["user_name"].ToString() : str))
            {
                this.Session.Abandon();
                base.Response.Write("<script>top.location.href='/'</script>");
                base.Response.End();
            }
            string str3 = HttpContext.Current.Session["user_name"].ToString();
            this.UserCurrentSkin = (HttpContext.Current.Session[str3 + "lottery_session_user_info"] as agent_userinfo_session).get_u_skin();
            this.ForcedModifyPassword();
            this.AgentCurrentLottery();
            this.RedirectReport();
            this.IsOpenLottery();
            if ((((this.Context.Request.Path.ToLower().IndexOf("fgs_list") > -1) || (this.Context.Request.Path.ToLower().IndexOf("gd_list") > -1)) || ((this.Context.Request.Path.ToLower().IndexOf("zd_list") > -1) || (this.Context.Request.Path.ToLower().IndexOf("dl_list") > -1))) || (((this.Context.Request.Path.ToLower().IndexOf("hy_list") > -1) || (this.Context.Request.Path.ToLower().IndexOf("child_list") > -1)) || (this.Context.Request.Path.ToLower().IndexOf("filluser_list") > -1)))
            {
                CookieHelper.SetCookie("userreturnbackurl", base.Request.ServerVariables["Path_Info"] + "?" + base.Request.ServerVariables["Query_String"]);
            }
        }

        public void openclose_log(string oddsid, string operate_type, string is_open, string playpage, string playid)
        {
            cz_odds_six model = null;
            if (!string.IsNullOrEmpty(oddsid) && (oddsid.IndexOf(',') < 0))
            {
                model = CallBLL.cz_odds_six_bll.GetModel(Convert.ToInt32(oddsid));
            }
            string str = this.get_master_name();
            string str2 = this.get_children_name();
            string gameNameByID = base.GetGameNameByID(100.ToString());
            string category = "";
            string str5 = "";
            string str6 = "";
            string str7 = "";
            string str8 = "";
            int num = 0;
            string note = "單項開停盤操作";
            string act = "";
            int num2 = Convert.ToInt32((LSEnums.LogTypeID) 0);
            if (model != null)
            {
                category = model.get_category();
                str5 = model.get_play_name();
                str6 = model.get_put_amount();
                num = int.Parse(oddsid);
                note = note + "," + str6;
                if (model.get_is_open() == 0)
                {
                    str7 = "開盤";
                    str8 = "停盤";
                    act = "停押";
                }
                else
                {
                    str7 = "停盤";
                    str8 = "開盤";
                    act = "開放";
                }
            }
            else
            {
                if (is_open.Equals("0"))
                {
                    act = "停押";
                }
                else
                {
                    act = "開放";
                }
                if (operate_type.Equals("61"))
                {
                    note = "全部開停盤操作";
                }
                if (operate_type.Equals("62"))
                {
                    note = "當前頁開停盤操作";
                    string str11 = this.get_playpage_name(playpage);
                    if ((playpage.IndexOf("zmt") == 0) && (playid == "91010,91025,91026,91027,91028,91029"))
                    {
                        str11 = "正馬特(1-6)";
                    }
                    if (playpage == "lhtmtz")
                    {
                        if (playid == "91046")
                        {
                            str11 = "龍虎";
                        }
                        else if (playid == "91039")
                        {
                            str11 = "特碼攤子";
                        }
                    }
                    if (str11 != "")
                    {
                        note = note + "=>" + str11;
                    }
                }
                if (operate_type.Equals("65"))
                {
                    note = "非特開停盤操作";
                }
                if (operate_type.Equals("64"))
                {
                    note = "快捷鍵開停盤操作";
                }
            }
            string str12 = "";
            cz_phase_six currentPhase = CallBLL.cz_phase_six_bll.GetCurrentPhase();
            if (currentPhase != null)
            {
                str12 = currentPhase.get_phase().ToString();
            }
            List<SqlParameter> paramList = new List<SqlParameter>();
            string str13 = this.add_sys_log(str, str2, category, str5, str6, gameNameByID, str12, act, num, str7, str8, note, num2, 100, ref paramList);
            CallBLL.cz_system_log_bll.executte_sql(str13, paramList.ToArray());
        }

        public string PageList(int size, int pageCount, int currentPage, string[] FieldName, string[] FieldValue, int rcount)
        {
            string str = "";
            for (int i = 0; i < FieldName.Length; i++)
            {
                string str5 = str;
                str = str5 + "&" + FieldName[i].ToString() + "=" + FieldValue[i].ToString();
            }
            int num2 = 5;
            int num3 = 1;
            string str2 = "";
            pageCount = (pageCount == 0) ? 1 : pageCount;
            currentPage = (currentPage == 0) ? 1 : currentPage;
            if (rcount == 0)
            {
                currentPage = 0;
                pageCount = 0;
            }
            if (currentPage > pageCount)
            {
                currentPage = pageCount;
            }
            str2 = string.Concat(new object[] { "<span id='pager'>共 ", rcount, " 條記錄  分頁：", currentPage.ToString(), "/", pageCount.ToString(), "頁&nbsp;&nbsp;&nbsp;" });
            if ((currentPage - num2) < 2)
            {
                num3 = 1;
            }
            else
            {
                num3 = currentPage - num2;
            }
            int num4 = pageCount;
            if ((currentPage + num2) >= pageCount)
            {
                num4 = pageCount;
            }
            else
            {
                num4 = currentPage + num2;
            }
            if (num3 == 1)
            {
                if ((currentPage == 1) || (currentPage == 0))
                {
                    str2 = str2 + "上一頁";
                }
                else
                {
                    string str6 = str2 + "<a class='redLink' href='?page=1" + str + "' title='首頁'>首頁</a>&nbsp;&nbsp;";
                    str2 = str6 + "<a class='redLink' href='?page=" + Convert.ToString((int) (currentPage - 1)) + str + "' title='上頁'>上一頁</a>";
                }
            }
            else
            {
                string str7 = str2 + "<a class='redLink' href='?page=1" + str + "' title='首頁'>首頁</a>&nbsp;&nbsp;";
                str2 = str7 + "<a class='redLink' href='?page=" + Convert.ToString((int) (currentPage - 1)) + str + "' title='上頁'>上一頁</a>...";
            }
            str2 = str2 + "『";
            for (int j = num3; j <= num4; j++)
            {
                if (j == currentPage)
                {
                    str2 = str2 + "<span class='font_c'>" + j.ToString() + "</span>&nbsp;";
                }
                else
                {
                    string str8 = str2;
                    str2 = str8 + "<a class='redLink' href='?page=" + j.ToString() + str + "' title='第" + j.ToString() + "頁'>" + j.ToString() + "</a>&nbsp;";
                }
                if (j == pageCount)
                {
                    break;
                }
            }
            str2 = str2 + "』";
            if (num4 == pageCount)
            {
                if (pageCount == currentPage)
                {
                    str2 = str2 + "下一頁";
                }
                else
                {
                    string str9 = str2;
                    string str10 = str9 + "<a class='redLink' href='?page=" + Convert.ToString((int) (currentPage + 1)) + str + "' title='下頁'>下一頁</a>";
                    str2 = str10 + "&nbsp;&nbsp;<a class='redLink' href='?page=" + pageCount.ToString() + str + "' title='尾頁'>尾頁</a>";
                }
            }
            else
            {
                string str11 = str2;
                string str12 = str11 + "...<a class='redLink' href='?page=" + Convert.ToString((int) (currentPage + 1)) + str + "' title='下頁'>下一頁</a>";
                str2 = str12 + "&nbsp;&nbsp;<a class='redLink' href='?page=" + pageCount.ToString() + str + "' title='最後一頁'>尾頁</a>";
            }
            str2 = (str2 + "<input type=\"hidden\" value=\"" + currentPage.ToString() + "\" id=\"page\"></span>") + "<input type=\"hidden\" value=\"" + str + "\" id=\"hdnPString\"></span>";
            StringBuilder builder = new StringBuilder();
            string str3 = @"javascript:  var txtPagerValue=document.getElementById('txtPager').value; var regs = /^\d+$/; if(!regs.test(txtPagerValue)){alert('輸入錯誤');document.getElementById('txtPager').focus();return false;};var hdnPStringValue=document.getElementById('hdnPString').value; location.href='?page='+txtPagerValue+hdnPStringValue;";
            string str4 = @"javascript: if (event.keyCode ==13){ var txtPagerValue=document.getElementById('txtPager').value; var regs = /^\d+$/; if(!regs.test(txtPagerValue)){alert('輸入錯誤');document.getElementById('txtPager').focus();return false;};var hdnPStringValue=document.getElementById('hdnPString').value; location.href='?page='+txtPagerValue+hdnPStringValue;};";
            builder.AppendFormat("&nbsp;&nbsp;<input type='text' value='{0}' name='txtPager' id='txtPager' class='GOtext' onkeydown=\"{1}\" style=\"display:inline-block;vertical-align:middle;margin-bottom:2px;\" />", currentPage, str4);
            builder.AppendFormat("<input type='button' class='GObtn' onclick=\"{0}\" id=\"btnPager\" style=\"display:inline-block;vertical-align:middle;margin-bottom:2px;\" />", str3);
            return (str2 + builder.ToString());
        }

        public string PageSelectKind_kc()
        {
            string str = HttpContext.Current.Session["user_name"].ToString();
            agent_userinfo_session _session = HttpContext.Current.Session[str + "lottery_session_user_info"] as agent_userinfo_session;
            StringBuilder builder = new StringBuilder();
            if (string.IsNullOrEmpty(_session.get_kc_kind()) || _session.get_kc_kind().ToString().Equals("0"))
            {
                builder.Append("<option value=\"\" selected>全部</option>");
                builder.Append("<option value=\"a\">A盤</option>");
                builder.Append("<option value=\"b\">B盤</option>");
                builder.Append("<option value=\"c\">C盤</option>");
            }
            else
            {
                builder.AppendFormat("<option value=\"{0}\">{1}盤</option>", _session.get_kc_kind().ToLower(), _session.get_kc_kind().ToUpper());
            }
            return builder.ToString();
        }

        public string PageSelectKind_six()
        {
            string str = HttpContext.Current.Session["user_name"].ToString();
            agent_userinfo_session _session = HttpContext.Current.Session[str + "lottery_session_user_info"] as agent_userinfo_session;
            StringBuilder builder = new StringBuilder();
            if (string.IsNullOrEmpty(_session.get_six_kind()) || _session.get_six_kind().ToString().Equals("0"))
            {
                builder.Append("<option value=\"\" selected>全部</option>");
                builder.Append("<option value=\"a\">A盤</option>");
                builder.Append("<option value=\"b\">B盤</option>");
                builder.Append("<option value=\"c\">C盤</option>");
            }
            else
            {
                builder.AppendFormat("<option value=\"{0}\">{1}盤</option>", _session.get_six_kind().ToLower(), _session.get_six_kind().ToUpper());
            }
            return builder.ToString();
        }

        public string PageSelectPlaytype_PKBJL()
        {
            string str = HttpContext.Current.Session["user_name"].ToString();
            object obj1 = HttpContext.Current.Session[str + "lottery_session_user_info"];
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("<option value=\"{0}\" selected>全部</option>", 2);
            builder.AppendFormat("<option value=\"{0}\">一般</option>", 1);
            builder.AppendFormat("<option value=\"{0}\">免傭</option>", 0);
            return builder.ToString();
        }

        public void pcdd_kjj_log(DataTable oldTable, DataTable newTable, string operate_type)
        {
            string str = this.get_master_name();
            string str2 = this.get_children_name();
            string gameNameByID = base.GetGameNameByID(7.ToString());
            string category = "";
            string str5 = "";
            string str6 = "";
            string s = "";
            string str8 = "";
            int num = 0;
            string note = "快捷鍵微调" + str6;
            string act = "";
            int num2 = Convert.ToInt32((LSEnums.LogTypeID) 0);
            if (operate_type.Equals("7"))
            {
                note = "快捷鍵手工输入赔率值,微調" + str6;
            }
            string str11 = "";
            DataTable table = CallBLL.cz_phase_pcdd_bll.IsOpenPhaseByTime(DateTime.Now);
            if (table != null)
            {
                str11 = table.Rows[0]["phase"].ToString();
            }
            List<CommandInfo> list = new List<CommandInfo>();
            int num3 = 0;
            foreach (DataRow row in oldTable.Rows)
            {
                category = row["category"].ToString();
                str5 = row["play_name"].ToString();
                str6 = row["put_amount"].ToString();
                s = row["current_odds"].ToString();
                str8 = newTable.Rows[num3]["current_odds"].ToString();
                num = int.Parse(row["odds_id"].ToString());
                if (s != str8)
                {
                    if (double.Parse(s) > double.Parse(str8))
                    {
                        act = "降賠率";
                    }
                    else
                    {
                        act = "升賠率";
                    }
                    List<SqlParameter> paramList = new List<SqlParameter>();
                    string str12 = this.add_sys_log(str, str2, category, str5, str6, gameNameByID, str11, act, num, s, str8, note, num2, 7, ref paramList);
                    CommandInfo item = new CommandInfo {
                        CommandText = str12,
                        Parameters = paramList.ToArray()
                    };
                    list.Add(item);
                }
                num3++;
            }
            if (list.Count > 0)
            {
                CallBLL.cz_system_log_bll.execute_sql_tran(list);
            }
        }

        public void pcdd_log(cz_odds_pcdd oldModel, cz_odds_pcdd newModel, string operate_type)
        {
            string str = this.get_master_name();
            string str2 = this.get_children_name();
            string gameNameByID = base.GetGameNameByID(7.ToString());
            string category = oldModel.get_category();
            string str5 = oldModel.get_play_name();
            string str6 = oldModel.get_put_amount();
            string s = oldModel.get_current_odds();
            string str8 = newModel.get_current_odds();
            int num = oldModel.get_odds_id();
            string note = "微調" + str6;
            string act = "";
            int num2 = Convert.ToInt32((LSEnums.LogTypeID) 0);
            if (operate_type.Equals("3"))
            {
                note = "手工输入赔率值,微調" + str6;
            }
            string str11 = "";
            DataTable table = CallBLL.cz_phase_pcdd_bll.IsOpenPhaseByTime(DateTime.Now);
            if (table != null)
            {
                str11 = table.Rows[0]["phase"].ToString();
            }
            if (s != str8)
            {
                if (double.Parse(s) > double.Parse(str8))
                {
                    act = "降賠率";
                }
                else
                {
                    act = "升賠率";
                }
                List<SqlParameter> paramList = new List<SqlParameter>();
                string str12 = this.add_sys_log(str, str2, category, str5, str6, gameNameByID, str11, act, num, s, str8, note, num2, 7, ref paramList);
                CallBLL.cz_system_log_bll.executte_sql(str12, paramList.ToArray());
            }
        }

        public string Permission_Ashx_DL(agent_userinfo_session model, string perName)
        {
            string str = "";
            if ((!model.get_u_type().ToLower().Equals("zj") && (model.get_users_child_session() != null)) && (model.get_users_child_session().get_permissions_name().IndexOf(perName) < 0))
            {
                ReturnResult result = new ReturnResult();
                result.set_success(400);
                result.set_tipinfo(PageBase.GetMessageByCache("u100013", "MessageHint"));
                str = JsonHandle.ObjectToJson(result);
            }
            return str;
        }

        public string Permission_Ashx_ZJ(agent_userinfo_session model, string perName)
        {
            string str = "";
            if ((model.get_u_type().ToLower().Equals("zj") && (model.get_users_child_session() != null)) && (model.get_users_child_session().get_permissions_name().IndexOf(perName) < 0))
            {
                ReturnResult result = new ReturnResult();
                result.set_success(400);
                result.set_tipinfo(PageBase.GetMessageByCache("u100013", "MessageHint"));
                str = JsonHandle.ObjectToJson(result);
            }
            return str;
        }

        public void Permission_Aspx_DL(agent_userinfo_session model, string perName)
        {
            if ((!model.get_u_type().ToLower().Equals("zj") && (model.get_users_child_session() != null)) && (model.get_users_child_session().get_permissions_name().IndexOf(perName) < 0))
            {
                base.Response.Redirect("/MessagePage.aspx?code=u100057&url=&issuccess=1&isback=0");
            }
        }

        public void Permission_Aspx_ZJ(agent_userinfo_session model, string perName)
        {
            if ((model.get_u_type().ToLower().Equals("zj") && (model.get_users_child_session() != null)) && (model.get_users_child_session().get_permissions_name().IndexOf(perName) < 0))
            {
                base.Response.Redirect("/MessagePage.aspx?code=u100057&url=&issuccess=1&isback=0");
            }
        }

        public void pk10_kjj_log(DataTable oldTable, DataTable newTable, string operate_type)
        {
            string str = this.get_master_name();
            string str2 = this.get_children_name();
            string gameNameByID = base.GetGameNameByID(2.ToString());
            string category = "";
            string str5 = "";
            string str6 = "";
            string s = "";
            string str8 = "";
            int num = 0;
            string note = "快捷鍵微调" + str6;
            string act = "";
            int num2 = Convert.ToInt32((LSEnums.LogTypeID) 0);
            if (operate_type.Equals("7"))
            {
                note = "快捷鍵手工输入赔率值,微調" + str6;
            }
            string str11 = "";
            DataTable table = CallBLL.cz_phase_pk10_bll.IsOpenPhaseByTime(DateTime.Now);
            if (table != null)
            {
                str11 = table.Rows[0]["phase"].ToString();
            }
            List<CommandInfo> list = new List<CommandInfo>();
            int num3 = 0;
            foreach (DataRow row in oldTable.Rows)
            {
                category = row["category"].ToString();
                str5 = row["play_name"].ToString();
                str6 = row["put_amount"].ToString();
                s = row["current_odds"].ToString();
                str8 = newTable.Rows[num3]["current_odds"].ToString();
                num = int.Parse(row["odds_id"].ToString());
                if (s != str8)
                {
                    if (double.Parse(s) > double.Parse(str8))
                    {
                        act = "降賠率";
                    }
                    else
                    {
                        act = "升賠率";
                    }
                    List<SqlParameter> paramList = new List<SqlParameter>();
                    string str12 = this.add_sys_log(str, str2, category, str5, str6, gameNameByID, str11, act, num, s, str8, note, num2, 2, ref paramList);
                    CommandInfo item = new CommandInfo {
                        CommandText = str12,
                        Parameters = paramList.ToArray()
                    };
                    list.Add(item);
                }
                num3++;
            }
            if (list.Count > 0)
            {
                CallBLL.cz_system_log_bll.execute_sql_tran(list);
            }
        }

        public void pk10_log(cz_odds_pk10 oldModel, cz_odds_pk10 newModel, string operate_type)
        {
            string str = this.get_master_name();
            string str2 = this.get_children_name();
            string gameNameByID = base.GetGameNameByID(2.ToString());
            string category = oldModel.get_category();
            string str5 = oldModel.get_play_name();
            string str6 = oldModel.get_put_amount();
            string s = oldModel.get_current_odds();
            string str8 = newModel.get_current_odds();
            int num = oldModel.get_odds_id();
            string note = "微調" + str6;
            string act = "";
            int num2 = Convert.ToInt32((LSEnums.LogTypeID) 0);
            if (operate_type.Equals("3"))
            {
                note = "手工输入赔率值,微調" + str6;
            }
            string str11 = "";
            DataTable table = CallBLL.cz_phase_pk10_bll.IsOpenPhaseByTime(DateTime.Now);
            if (table != null)
            {
                str11 = table.Rows[0]["phase"].ToString();
            }
            if (s != str8)
            {
                if (double.Parse(s) > double.Parse(str8))
                {
                    act = "降賠率";
                }
                else
                {
                    act = "升賠率";
                }
                List<SqlParameter> paramList = new List<SqlParameter>();
                string str12 = this.add_sys_log(str, str2, category, str5, str6, gameNameByID, str11, act, num, s, str8, note, num2, 2, ref paramList);
                CallBLL.cz_system_log_bll.executte_sql(str12, paramList.ToArray());
            }
        }

        public void pkbjl_kjj_log(DataTable oldTable, DataTable newTable, string operate_type)
        {
            string str = this.get_master_name();
            string str2 = this.get_children_name();
            string gameNameByID = base.GetGameNameByID(8.ToString());
            string category = "";
            string str5 = "";
            string str6 = "";
            string s = "";
            string str8 = "";
            int num = 0;
            string note = "快捷鍵微调" + str6;
            string act = "";
            int num2 = Convert.ToInt32((LSEnums.LogTypeID) 0);
            if (operate_type.Equals("7"))
            {
                note = "快捷鍵手工输入赔率值,微調" + str6;
            }
            string str11 = "";
            DataTable table = CallBLL.cz_phase_pkbjl_bll.IsOpenPhaseByTime(DateTime.Now);
            if (table != null)
            {
                str11 = table.Rows[0]["phase"].ToString();
            }
            List<CommandInfo> list = new List<CommandInfo>();
            int num3 = 0;
            foreach (DataRow row in oldTable.Rows)
            {
                category = row["category"].ToString();
                str5 = row["play_name"].ToString();
                str6 = row["put_amount"].ToString();
                s = row["current_odds"].ToString();
                str8 = newTable.Rows[num3]["current_odds"].ToString();
                num = int.Parse(row["odds_id"].ToString());
                if (s != str8)
                {
                    if (double.Parse(s) > double.Parse(str8))
                    {
                        act = "降賠率";
                    }
                    else
                    {
                        act = "升賠率";
                    }
                    List<SqlParameter> paramList = new List<SqlParameter>();
                    string str12 = this.add_sys_log(str, str2, category, str5, str6, gameNameByID, str11, act, num, s, str8, note, num2, 8, ref paramList);
                    CommandInfo item = new CommandInfo {
                        CommandText = str12,
                        Parameters = paramList.ToArray()
                    };
                    list.Add(item);
                }
                num3++;
            }
            if (list.Count > 0)
            {
                CallBLL.cz_system_log_bll.execute_sql_tran(list);
            }
        }

        public void pkbjl_log(cz_odds_pkbjl oldModel, cz_odds_pkbjl newModel, string operate_type)
        {
            string str = this.get_master_name();
            string str2 = this.get_children_name();
            string gameNameByID = base.GetGameNameByID(8.ToString());
            string category = oldModel.get_category();
            string str5 = oldModel.get_play_name();
            string str6 = oldModel.get_put_amount();
            string s = oldModel.get_current_odds();
            string str8 = newModel.get_current_odds();
            int num = oldModel.get_odds_id();
            string note = "微調" + str6;
            string act = "";
            int num2 = Convert.ToInt32((LSEnums.LogTypeID) 0);
            if (operate_type.Equals("3"))
            {
                note = "手工输入赔率值,微調" + str6;
            }
            string str11 = "";
            DataTable table = CallBLL.cz_phase_pkbjl_bll.IsOpenPhaseByTime(DateTime.Now);
            if (table != null)
            {
                str11 = table.Rows[0]["phase"].ToString();
            }
            if (s != str8)
            {
                if (double.Parse(s) > double.Parse(str8))
                {
                    act = "降賠率";
                }
                else
                {
                    act = "升賠率";
                }
                List<SqlParameter> paramList = new List<SqlParameter>();
                string str12 = this.add_sys_log(str, str2, category, str5, str6, gameNameByID, str11, act, num, s, str8, note, num2, 8, ref paramList);
                CallBLL.cz_system_log_bll.executte_sql(str12, paramList.ToArray());
            }
        }

        public void plsd_kc_log(DataTable oldDT, DataTable newDT, string lotteryID)
        {
            string str = this.get_master_name();
            string str2 = this.get_children_name();
            string gameNameByID = base.GetGameNameByID(lotteryID);
            string category = "";
            string str5 = "";
            string str6 = "";
            string str7 = "";
            string str8 = "";
            int num = 0;
            string note = "賠率設定";
            string act = "賠率設定";
            int num2 = Convert.ToInt32((LSEnums.LogTypeID) 3);
            string str11 = "";
            int count = oldDT.Rows.Count;
            IEqualityComparer<DataRow> comparer = DataRowComparer.Default;
            for (int i = 0; i < count; i++)
            {
                str7 = "";
                str8 = "";
                DataRow x = oldDT.Rows[i];
                DataRow y = newDT.Rows[i];
                if (!comparer.Equals(x, y))
                {
                    str5 = x["play_name"].ToString();
                    string str12 = x["play_id"].ToString();
                    string str13 = this.get_rename_play_name(lotteryID, i);
                    if (str13 != "")
                    {
                        str5 = str13;
                    }
                    if (((((int.Parse(lotteryID) == 1) || (int.Parse(lotteryID) == 6)) || ((int.Parse(lotteryID) == 11) || (int.Parse(lotteryID) == 13))) || ((int.Parse(lotteryID) == 0x11) || (int.Parse(lotteryID) == 0x13))) && (str12 == "19"))
                    {
                        str5 = "前三 中三 后三";
                    }
                    string str14 = x["default_odds"].ToString();
                    string str15 = x["max_odds"].ToString();
                    string str16 = x["min_odds"].ToString();
                    string str17 = x["b_diff"].ToString();
                    string str18 = x["c_diff"].ToString();
                    string str19 = x["downbase"].ToString();
                    string str20 = x["down_odds_rate"].ToString();
                    string str21 = y["default_odds"].ToString();
                    string str22 = y["max_odds"].ToString();
                    string str23 = y["min_odds"].ToString();
                    string str24 = y["b_diff"].ToString();
                    string str25 = y["c_diff"].ToString();
                    string str26 = y["downbase"].ToString();
                    string str27 = y["down_odds_rate"].ToString();
                    string str28 = "";
                    if (!str14.Equals(str21))
                    {
                        str28 = this.get_kc_change_val(lotteryID, str12, str14, str21);
                        str7 = "開盤賠率(A盤) :" + ((str28 != "") ? str28.Split(new char[] { '|' })[0] : str14);
                        str8 = "開盤賠率(A盤) :" + ((str28 != "") ? str28.Split(new char[] { '|' })[1] : str21);
                    }
                    if (!str15.Equals(str22))
                    {
                        str28 = this.get_kc_change_val(lotteryID, str12, str15, str22);
                        str7 = str7 + " A盤上限:" + ((str28 != "") ? str28.Split(new char[] { '|' })[0] : str15);
                        str8 = str8 + " A盤上限:" + ((str28 != "") ? str28.Split(new char[] { '|' })[1] : str22);
                    }
                    if (!str16.Equals(str23))
                    {
                        str28 = this.get_kc_change_val(lotteryID, str12, str16, str23);
                        str7 = str7 + " A盤下限:" + ((str28 != "") ? str28.Split(new char[] { '|' })[0] : str16);
                        str8 = str8 + " A盤下限:" + ((str28 != "") ? str28.Split(new char[] { '|' })[1] : str23);
                    }
                    if (!str17.Equals(str24))
                    {
                        str7 = str7 + " B盤賠率(降):" + str17;
                        str8 = str8 + " B盤賠率(降):" + str24;
                    }
                    if (!str18.Equals(str25))
                    {
                        str7 = str7 + " C盤賠率(降):" + str18;
                        str8 = str8 + " C盤賠率(降):" + str25;
                    }
                    if (!str19.Equals(str26))
                    {
                        str7 = str7 + " 自動降賠率額度(實貨):" + str19;
                        str8 = str8 + " 自動降賠率額度(實貨):" + str26;
                    }
                    if (!str20.Equals(str27))
                    {
                        str7 = str7 + " 每次降賠率:" + str20;
                        str8 = str8 + " 每次降賠率:" + str27;
                    }
                    List<SqlParameter> paramList = new List<SqlParameter>();
                    string str29 = this.add_sys_log(str, str2, category, str5, str6, gameNameByID, str11, act, num, str7, str8, note, num2, int.Parse(lotteryID), ref paramList);
                    CallBLL.cz_system_log_bll.executte_sql(str29, paramList.ToArray());
                }
            }
        }

        public void plsd_kc_log_pkbjl(DataTable oldDT, DataTable newDT)
        {
            string s = 8.ToString();
            string str2 = this.get_master_name();
            string str3 = this.get_children_name();
            string gameNameByID = base.GetGameNameByID(s);
            string category = "";
            string str6 = "";
            string str7 = "";
            string str8 = "";
            string str9 = "";
            string note = "賠率設定";
            string act = "賠率設定";
            int num = Convert.ToInt32((LSEnums.LogTypeID) 3);
            string str12 = "";
            int count = oldDT.Rows.Count;
            IEqualityComparer<DataRow> comparer = DataRowComparer.Default;
            for (int i = 0; i < count; i++)
            {
                str6 = "";
                str8 = "";
                str9 = "";
                DataRow x = oldDT.Rows[i];
                DataRow y = newDT.Rows[i];
                if (!comparer.Equals(x, y))
                {
                    string str13 = x["odds_id"].ToString();
                    x["wt_value"].ToString();
                    switch (str13)
                    {
                        case "82005":
                            str6 = "莊閑和【莊】";
                            break;

                        case "82006":
                            str6 = "莊閑和【閑】";
                            break;
                    }
                    string str14 = Convert.ToDouble(x["wt_value"].ToString()).ToString();
                    string str15 = Convert.ToDouble(y["wt_value"].ToString()).ToString();
                    if (!str14.Equals(str15))
                    {
                        str8 = "免傭賠率差 :" + str14;
                        str9 = "免傭賠率差 :" + str15;
                    }
                    List<SqlParameter> paramList = new List<SqlParameter>();
                    string str16 = this.add_sys_log(str2, str3, category, str6, str7, gameNameByID, str12, act, int.Parse(str13), str8, str9, note, num, int.Parse(s), ref paramList);
                    CallBLL.cz_system_log_bll.executte_sql(str16, paramList.ToArray());
                }
            }
        }

        public void plsd_six_log(DataTable oldDT, DataTable newDT)
        {
            string str = this.get_master_name();
            string str2 = this.get_children_name();
            string gameNameByID = base.GetGameNameByID(100.ToString());
            string category = "";
            string str5 = "";
            string str6 = "";
            string str7 = "";
            string str8 = "";
            int num = 0;
            string note = "賠率設定";
            string act = "賠率設定";
            int num2 = Convert.ToInt32((LSEnums.LogTypeID) 3);
            string str11 = "";
            int count = oldDT.Rows.Count;
            IEqualityComparer<DataRow> comparer = DataRowComparer.Default;
            List<string> list = new List<string> { "91013", "91006", "91007", "91021", "91022", "91008", "91052", "91053", "91054", "91055", "91056", "91057" };
            for (int i = 0; i < count; i++)
            {
                str7 = "";
                str8 = "";
                DataRow x = oldDT.Rows[i];
                DataRow y = newDT.Rows[i];
                if (!comparer.Equals(x, y))
                {
                    str5 = x["play_name"].ToString();
                    string item = x["play_id"].ToString();
                    string str13 = x["isDoubleOdds"].ToString();
                    if (list.Contains(item))
                    {
                        str13 = "1";
                    }
                    string str14 = this.rename_play_six(item);
                    if (str14 != "")
                    {
                        str5 = str14;
                    }
                    "91060,91061,91062,91063,91064,91065".IndexOf(item);
                    string str15 = x["default_odds"].ToString();
                    string str16 = x["max_odds"].ToString();
                    string str17 = x["min_odds"].ToString();
                    string str18 = x["b_diff"].ToString();
                    string str19 = x["c_diff"].ToString();
                    string str20 = x["downbase"].ToString();
                    string str21 = x["down_odds_rate"].ToString();
                    string str22 = y["default_odds"].ToString();
                    string str23 = y["max_odds"].ToString();
                    string str24 = y["min_odds"].ToString();
                    string str25 = y["b_diff"].ToString();
                    string str26 = y["c_diff"].ToString();
                    string str27 = y["downbase"].ToString();
                    string str28 = y["down_odds_rate"].ToString();
                    string str29 = "";
                    if (!str15.Equals(str22))
                    {
                        if (str13 == "1")
                        {
                            str29 = this.spl_name(item, str15, str22);
                        }
                        str7 = "開盤賠率(A盤) :" + ((str13 == "1") ? str29.Split(new char[] { '|' })[0] : str15);
                        str8 = "開盤賠率(A盤) :" + ((str13 == "1") ? str29.Split(new char[] { '|' })[1] : str22);
                    }
                    if (!str16.Equals(str23))
                    {
                        if (str13 == "1")
                        {
                            str29 = this.spl_name(item, str16, str23);
                        }
                        str7 = str7 + " A盤上限:" + ((str13 == "1") ? str29.Split(new char[] { '|' })[0] : str16);
                        str8 = str8 + " A盤上限:" + ((str13 == "1") ? str29.Split(new char[] { '|' })[1] : str23);
                    }
                    if (!str17.Equals(str24))
                    {
                        if (str13 == "1")
                        {
                            str29 = this.spl_name(item, str17, str24);
                        }
                        str7 = str7 + " A盤下限:" + ((str13 == "1") ? str29.Split(new char[] { '|' })[0] : str17);
                        str8 = str8 + " A盤下限:" + ((str13 == "1") ? str29.Split(new char[] { '|' })[1] : str24);
                    }
                    if (!str18.Equals(str25))
                    {
                        if (str13 == "1")
                        {
                            str29 = this.spl_name(item, str18, str25);
                        }
                        str7 = str7 + " B盤賠率(降):" + ((str13 == "1") ? str29.Split(new char[] { '|' })[0] : str18);
                        str8 = str8 + " B盤賠率(降):" + ((str13 == "1") ? str29.Split(new char[] { '|' })[1] : str25);
                    }
                    if (!str19.Equals(str26))
                    {
                        if (str13 == "1")
                        {
                            str29 = this.spl_name(item, str19, str26);
                        }
                        str7 = str7 + " C盤賠率(降):" + ((str13 == "1") ? str29.Split(new char[] { '|' })[0] : str19);
                        str8 = str8 + " C盤賠率(降):" + ((str13 == "1") ? str29.Split(new char[] { '|' })[1] : str26);
                    }
                    if (!str20.Equals(str27))
                    {
                        str7 = str7 + " 自動降賠率額度(實貨):" + str20;
                        str8 = str8 + " 自動降賠率額度(實貨):" + str27;
                    }
                    if (!str21.Equals(str28))
                    {
                        str7 = str7 + " 每次降賠率:" + str21;
                        str8 = str8 + " 每次降賠率:" + str28;
                    }
                    List<SqlParameter> paramList = new List<SqlParameter>();
                    string str30 = this.add_sys_log(str, str2, category, str5, str6, gameNameByID, str11, act, num, str7, str8, note, num2, 100, ref paramList);
                    CallBLL.cz_system_log_bll.executte_sql(str30, paramList.ToArray());
                }
            }
        }

        protected void RedirectReport()
        {
            int num = 1;
            if (HttpContext.Current.Session["user_state"].ToString().Equals(num.ToString()) && ((((((this.Context.Request.Path.ToLower().IndexOf("l_six") > 0) || (this.Context.Request.Path.ToLower().IndexOf("l_cqsc") > 0)) || ((this.Context.Request.Path.ToLower().IndexOf("l_k3") > 0) || (this.Context.Request.Path.ToLower().IndexOf("l_kl10") > 0))) || (((this.Context.Request.Path.ToLower().IndexOf("l_kl8") > 0) || (this.Context.Request.Path.ToLower().IndexOf("l_pk10") > 0)) || ((this.Context.Request.Path.ToLower().IndexOf("l_xync") > 0) || (this.Context.Request.Path.ToLower().IndexOf("l_k8sc") > 0)))) || ((((this.Context.Request.Path.ToLower().IndexOf("l_pcdd") > 0) || (this.Context.Request.Path.ToLower().IndexOf("l_xyft5") > 0)) || ((this.Context.Request.Path.ToLower().IndexOf("l_pkbjl") > 0) || (this.Context.Request.Path.ToLower().IndexOf("l_jscar") > 0))) || (((this.Context.Request.Path.ToLower().IndexOf("l_speed5") > 0) || (this.Context.Request.Path.ToLower().IndexOf("l_jspk10") > 0)) || ((this.Context.Request.Path.ToLower().IndexOf("l_jscqsc") > 0) || (this.Context.Request.Path.ToLower().IndexOf("l_jssfc") > 0))))) || ((((((this.Context.Request.Path.ToLower().IndexOf("l_jsft2") > 0) || (this.Context.Request.Path.ToLower().IndexOf("l_car168") > 0)) || ((this.Context.Request.Path.ToLower().IndexOf("l_ssc168") > 0) || (this.Context.Request.Path.ToLower().IndexOf("l_vrcar") > 0))) || (((this.Context.Request.Path.ToLower().IndexOf("l_vrssc") > 0) || (this.Context.Request.Path.ToLower().IndexOf("l_xyftoa") > 0)) || ((this.Context.Request.Path.ToLower().IndexOf("l_xyftsg") > 0) || (this.Context.Request.Path.ToLower().IndexOf("l_happycar") > 0)))) || ((((this.Context.Request.Path.ToLower().IndexOf("account") > 0) || (this.Context.Request.Path.ToLower().IndexOf("viewbill") > 0)) || ((this.Context.Request.Path.ToLower().IndexOf("autolet") > 0) || (this.Context.Request.Path.ToLower().IndexOf("autolet_kc") > 0))) || (((this.Context.Request.Path.ToLower().IndexOf("autolet_six") > 0) || (this.Context.Request.Path.ToLower().IndexOf("oddswt.aspx") > 0)) || ((this.Context.Request.Path.ToLower().IndexOf("systemset_kc") > 0) || (this.Context.Request.Path.ToLower().IndexOf("systemset_six") > 0))))) || ((((this.Context.Request.Path.ToLower().IndexOf("tradingset") > 0) || (this.Context.Request.Path.ToLower().IndexOf("tradingset") > 0)) || ((this.Context.Request.Path.ToLower().IndexOf("oddsset_kc") > 0) || (this.Context.Request.Path.ToLower().IndexOf("oddsset_six") > 0))) || ((((this.Context.Request.Path.ToLower().IndexOf("news_add") > 0) || (this.Context.Request.Path.ToLower().IndexOf("news_edit") > 0)) || ((this.Context.Request.Path.ToLower().IndexOf("news_list") > 0) || (this.Context.Request.Path.ToLower().IndexOf("awardperiod") > 0))) || (((this.Context.Request.Path.ToLower().IndexOf("reportbackupmanage") > 0) || (this.Context.Request.Path.ToLower().IndexOf("billbackupmanage") > 0)) || (this.Context.Request.Path.ToLower().IndexOf("viewonlineuser") > 0)))))))
            {
                string str = LSRequest.qq("lid");
                base.Response.Redirect(string.Format("/ReportSearch/Report.aspx?lid={0}", str), true);
                base.Response.End();
            }
        }

        public void RedirectUrl(int code, string msg, string url)
        {
            ReturnResult result = new ReturnResult();
            result.set_success(code);
            result.set_tipinfo(msg);
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("type", "uservalid");
            Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
            dictionary2.Add("url", url);
            dictionary.Add("turl", dictionary2);
            result.set_data(dictionary);
            JsonHandle.ObjectToJson(result);
        }

        protected string rename_play_six(string play_id)
        {
            string str = "";
            if (play_id == "91010")
            {
                str = "正特1-6";
            }
            return str;
        }

        protected void RestOddsWT(int? lid, int mlid, string fgs_name)
        {
            if (mlid.Equals(1))
            {
                CallBLL.cz_odds_wt_six_bll.RestOddsWT(fgs_name);
            }
            else if (!lid.HasValue)
            {
                CallBLL.cz_odds_wt_kl10_bll.RestOddsWT(fgs_name);
                CallBLL.cz_odds_wt_cqsc_bll.RestOddsWT(fgs_name);
                CallBLL.cz_odds_wt_pk10_bll.RestOddsWT(fgs_name);
                CallBLL.cz_odds_wt_xync_bll.RestOddsWT(fgs_name);
                CallBLL.cz_odds_wt_jsk3_bll.RestOddsWT(fgs_name);
                CallBLL.cz_odds_wt_kl8_bll.RestOddsWT(fgs_name);
                CallBLL.cz_odds_wt_k8sc_bll.RestOddsWT(fgs_name);
                CallBLL.cz_odds_wt_pcdd_bll.RestOddsWT(fgs_name);
                CallBLL.cz_odds_wt_xyft5_bll.RestOddsWT(fgs_name);
                CallBLL.cz_odds_wt_pkbjl_bll.RestOddsWT(fgs_name);
                CallBLL.cz_odds_wt_jscar_bll.RestOddsWT(fgs_name);
                CallBLL.cz_odds_wt_speed5_bll.RestOddsWT(fgs_name);
                CallBLL.cz_odds_wt_jscqsc_bll.RestOddsWT(fgs_name);
                CallBLL.cz_odds_wt_jspk10_bll.RestOddsWT(fgs_name);
                CallBLL.cz_odds_wt_jssfc_bll.RestOddsWT(fgs_name);
                CallBLL.cz_odds_wt_jsft2_bll.RestOddsWT(fgs_name);
                CallBLL.cz_odds_wt_car168_bll.RestOddsWT(fgs_name);
                CallBLL.cz_odds_wt_ssc168_bll.RestOddsWT(fgs_name);
                CallBLL.cz_odds_wt_vrcar_bll.RestOddsWT(fgs_name);
                CallBLL.cz_odds_wt_vrssc_bll.RestOddsWT(fgs_name);
                CallBLL.cz_odds_wt_xyftoa_bll.RestOddsWT(fgs_name);
                CallBLL.cz_odds_wt_xyftsg_bll.RestOddsWT(fgs_name);
                CallBLL.cz_odds_wt_happycar_bll.RestOddsWT(fgs_name);
            }
            else
            {
                int valueOrDefault = lid.GetValueOrDefault();
                if (lid.HasValue)
                {
                    switch (valueOrDefault)
                    {
                        case 0:
                            CallBLL.cz_odds_wt_kl10_bll.RestOddsWT(fgs_name);
                            return;

                        case 1:
                            CallBLL.cz_odds_wt_cqsc_bll.RestOddsWT(fgs_name);
                            return;

                        case 2:
                            CallBLL.cz_odds_wt_pk10_bll.RestOddsWT(fgs_name);
                            return;

                        case 3:
                            CallBLL.cz_odds_wt_xync_bll.RestOddsWT(fgs_name);
                            return;

                        case 4:
                            CallBLL.cz_odds_wt_jsk3_bll.RestOddsWT(fgs_name);
                            return;

                        case 5:
                            CallBLL.cz_odds_wt_kl8_bll.RestOddsWT(fgs_name);
                            return;

                        case 6:
                            CallBLL.cz_odds_wt_k8sc_bll.RestOddsWT(fgs_name);
                            return;

                        case 7:
                            CallBLL.cz_odds_wt_pcdd_bll.RestOddsWT(fgs_name);
                            return;

                        case 8:
                            CallBLL.cz_odds_wt_pkbjl_bll.RestOddsWT(fgs_name);
                            return;

                        case 9:
                            CallBLL.cz_odds_wt_xyft5_bll.RestOddsWT(fgs_name);
                            return;

                        case 10:
                            CallBLL.cz_odds_wt_jscar_bll.RestOddsWT(fgs_name);
                            return;

                        case 11:
                            CallBLL.cz_odds_wt_speed5_bll.RestOddsWT(fgs_name);
                            return;

                        case 12:
                            CallBLL.cz_odds_wt_jspk10_bll.RestOddsWT(fgs_name);
                            return;

                        case 13:
                            CallBLL.cz_odds_wt_jscqsc_bll.RestOddsWT(fgs_name);
                            return;

                        case 14:
                            CallBLL.cz_odds_wt_jssfc_bll.RestOddsWT(fgs_name);
                            return;

                        case 15:
                            CallBLL.cz_odds_wt_jsft2_bll.RestOddsWT(fgs_name);
                            return;

                        case 0x10:
                            CallBLL.cz_odds_wt_car168_bll.RestOddsWT(fgs_name);
                            return;

                        case 0x11:
                            CallBLL.cz_odds_wt_ssc168_bll.RestOddsWT(fgs_name);
                            return;

                        case 0x12:
                            CallBLL.cz_odds_wt_vrcar_bll.RestOddsWT(fgs_name);
                            return;

                        case 0x13:
                            CallBLL.cz_odds_wt_vrssc_bll.RestOddsWT(fgs_name);
                            return;

                        case 20:
                            CallBLL.cz_odds_wt_xyftoa_bll.RestOddsWT(fgs_name);
                            return;

                        case 0x15:
                            CallBLL.cz_odds_wt_xyftsg_bll.RestOddsWT(fgs_name);
                            return;

                        case 0x16:
                            CallBLL.cz_odds_wt_happycar_bll.RestOddsWT(fgs_name);
                            break;

                        default:
                            return;
                    }
                }
            }
        }

        protected void SetUserInfoSession(int ha)
        {
            if (HttpContext.Current.Session["child_user_name"] != null)
            {
                this.ChildUserSession(ha);
            }
            else
            {
                this.MasterUserSession(ha);
            }
        }

        public string ShowDialogBox(string mess, string url, int flag)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("<script>");
            if (flag.Equals(400))
            {
                builder.Append("top.setDialogBox('" + mess + "', function () {");
                builder.Append("});");
            }
            else if (flag.Equals(0))
            {
                builder.Append("top.setDialogBox('" + mess + "', function () {");
                builder.Append(" location.href='" + url + "'; ");
                builder.Append("});");
            }
            else if (flag.Equals(1))
            {
                builder.Append("top.setDialogBox('" + mess + "', function () {");
                builder.AppendFormat(" top.saveReload({0}, '{1}', {2});", 1, url, 1);
                builder.Append("});");
            }
            else if (flag.Equals(2))
            {
                builder.Append("top.setDialogBox('" + mess + "', function () {");
                builder.Append(" history.go(-1); ");
                builder.Append("});");
            }
            else if (flag.Equals(3))
            {
                builder.Append("top.setDialogBox('" + mess + "', function () {");
                builder.AppendFormat(" top.saveReload({0}, '{1}', {2});", 2, url, 1);
                builder.Append("});");
            }
            else
            {
                builder.Append(" top.setDialogBox('" + mess + "', function () { ");
                builder.Append(" }, function () {");
                builder.Append(" location.href='" + url + "'; ");
                builder.Append(" }, function () {");
                builder.Append(" }, function () {");
                builder.Append(" });");
            }
            builder.Append("</script>");
            return builder.ToString();
        }

        public void ShowReturnResultQuit(HttpContext context, int code, int ha, string msg, string url)
        {
            ReturnResult result = new ReturnResult();
            if (ha.Equals(0))
            {
                result.set_success(code);
                result.set_tipinfo(msg);
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                dictionary.Add("type", "uservalid");
                Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
                dictionary2.Add("url", url);
                dictionary.Add("turl", dictionary2);
                result.set_data(dictionary);
                string s = JsonHandle.ObjectToJson(result);
                context.Response.ContentType = "text/json";
                context.Response.Write(s);
            }
        }

        public void speed5_kjj_log(DataTable oldTable, DataTable newTable, string operate_type)
        {
            string str = this.get_master_name();
            string str2 = this.get_children_name();
            string gameNameByID = base.GetGameNameByID(11.ToString());
            string category = "";
            string str5 = "";
            string str6 = "";
            string s = "";
            string str8 = "";
            int num = 0;
            string note = "快捷鍵微调" + str6;
            string act = "";
            int num2 = Convert.ToInt32((LSEnums.LogTypeID) 0);
            if (operate_type.Equals("7"))
            {
                note = "快捷鍵手工输入赔率值,微調" + str6;
            }
            string str11 = "";
            DataTable table = CallBLL.cz_phase_speed5_bll.IsOpenPhaseByTime(DateTime.Now);
            if (table != null)
            {
                str11 = table.Rows[0]["phase"].ToString();
            }
            List<CommandInfo> list = new List<CommandInfo>();
            int num3 = 0;
            foreach (DataRow row in oldTable.Rows)
            {
                category = row["category"].ToString();
                str5 = row["play_name"].ToString();
                str6 = row["put_amount"].ToString();
                s = row["current_odds"].ToString();
                str8 = newTable.Rows[num3]["current_odds"].ToString();
                num = int.Parse(row["odds_id"].ToString());
                if (s != str8)
                {
                    if (double.Parse(s) > double.Parse(str8))
                    {
                        act = "降賠率";
                    }
                    else
                    {
                        act = "升賠率";
                    }
                    List<SqlParameter> paramList = new List<SqlParameter>();
                    string str12 = this.add_sys_log(str, str2, category, str5, str6, gameNameByID, str11, act, num, s, str8, note, num2, 11, ref paramList);
                    CommandInfo item = new CommandInfo {
                        CommandText = str12,
                        Parameters = paramList.ToArray()
                    };
                    list.Add(item);
                }
                num3++;
            }
            if (list.Count > 0)
            {
                CallBLL.cz_system_log_bll.execute_sql_tran(list);
            }
        }

        public void speed5_log(cz_odds_speed5 oldModel, cz_odds_speed5 newModel, string operate_type)
        {
            string str = this.get_master_name();
            string str2 = this.get_children_name();
            string gameNameByID = base.GetGameNameByID(11.ToString());
            string category = oldModel.get_category();
            string str5 = oldModel.get_play_name();
            string str6 = oldModel.get_put_amount();
            string s = oldModel.get_current_odds();
            string str8 = newModel.get_current_odds();
            int num = oldModel.get_odds_id();
            string note = "微調" + str6;
            string act = "";
            int num2 = Convert.ToInt32((LSEnums.LogTypeID) 0);
            if (operate_type.Equals("3"))
            {
                note = "手工输入赔率值,微調" + str6;
            }
            string str11 = "";
            DataTable table = CallBLL.cz_phase_speed5_bll.IsOpenPhaseByTime(DateTime.Now);
            if (table != null)
            {
                str11 = table.Rows[0]["phase"].ToString();
            }
            if (s != str8)
            {
                if (double.Parse(s) > double.Parse(str8))
                {
                    act = "降賠率";
                }
                else
                {
                    act = "升賠率";
                }
                List<SqlParameter> paramList = new List<SqlParameter>();
                string str12 = this.add_sys_log(str, str2, category, str5, str6, gameNameByID, str11, act, num, s, str8, note, num2, 11, ref paramList);
                CallBLL.cz_system_log_bll.executte_sql(str12, paramList.ToArray());
            }
        }

        protected string spl_name(string play_id, string o_v, string n_v)
        {
            string str46;
            string str = "";
            if ((play_id == "91017") || (play_id == "91061"))
            {
                string[] strArray = o_v.Split(new char[] { ',' });
                string[] strArray2 = n_v.Split(new char[] { ',' });
                int num = strArray.Length;
                string str2 = "";
                string str3 = "";
                for (int j = 0; j < num; j++)
                {
                    if (!strArray[j].Equals(strArray2[j]))
                    {
                        if (j == 0)
                        {
                            str2 = "中二:" + strArray[j];
                            str3 = "中二:" + strArray2[j];
                        }
                        if (j == 1)
                        {
                            str2 = str2 + " 中三:" + strArray[j];
                            str3 = str3 + " 中三:" + strArray2[j];
                        }
                    }
                }
                return (str = str2 + "|" + str3);
            }
            if ((play_id == "91019") || (play_id == "91063"))
            {
                string[] strArray3 = o_v.Split(new char[] { ',' });
                string[] strArray4 = n_v.Split(new char[] { ',' });
                int num3 = strArray3.Length;
                string str4 = "";
                string str5 = "";
                for (int k = 0; k < num3; k++)
                {
                    if (!strArray3[k].Equals(strArray4[k]))
                    {
                        if (k == 0)
                        {
                            str4 = "中特:" + strArray3[k];
                            str5 = "中特:" + strArray4[k];
                        }
                        if (k == 1)
                        {
                            str4 = str4 + " 中二:" + strArray3[k];
                            str5 = str5 + " 中二:" + strArray4[k];
                        }
                    }
                }
                return (str = str4 + "|" + str5);
            }
            if (play_id == "91013")
            {
                string[] strArray5 = o_v.Split(new char[] { '|' });
                string[] strArray6 = n_v.Split(new char[] { '|' });
                int num5 = strArray5.Length;
                string str6 = "";
                string str7 = "";
                for (int m = 0; m < num5; m++)
                {
                    if (!strArray5[m].Equals(strArray6[m]))
                    {
                        if (m == 0)
                        {
                            str6 = "正碼1-6色波:" + strArray5[m];
                            str7 = "正碼1-6色波:" + strArray6[m];
                        }
                        if (m == 1)
                        {
                            str6 = str6 + " 紅波:" + strArray5[m];
                            str7 = str7 + " 紅波:" + strArray6[m];
                        }
                    }
                }
                return (str = str6 + "|" + str7);
            }
            if (play_id == "91006")
            {
                string[] strArray7 = o_v.Split(new char[] { '|' });
                string[] strArray8 = n_v.Split(new char[] { '|' });
                int num7 = strArray7.Length;
                string str8 = "";
                string str9 = "";
                for (int n = 0; n < num7; n++)
                {
                    if (!strArray7[n].Equals(strArray8[n]))
                    {
                        if (n == 0)
                        {
                            str8 = "特碼生肖:" + strArray7[n];
                            str9 = "特碼生肖:" + strArray8[n];
                        }
                        if (n == 1)
                        {
                            str46 = str8;
                            str8 = str46 + " " + base.get_YearLian() + ":" + strArray7[n];
                            str46 = str9;
                            str9 = str46 + " " + base.get_YearLian() + ":" + strArray8[n];
                        }
                    }
                }
                return (str = str8 + "|" + str9);
            }
            if (play_id == "91007")
            {
                string[] strArray9 = o_v.Split(new char[] { '|' });
                string[] strArray10 = n_v.Split(new char[] { '|' });
                int num9 = strArray9.Length;
                string str10 = "";
                string str11 = "";
                for (int num10 = 0; num10 < num9; num10++)
                {
                    if (!strArray9[num10].Equals(strArray10[num10]))
                    {
                        if (num10 == 0)
                        {
                            str10 = "特碼色波:" + strArray9[num10];
                            str11 = "特碼色波:" + strArray10[num10];
                        }
                        if (num10 == 1)
                        {
                            str10 = str10 + " 紅波:" + strArray9[num10];
                            str11 = str11 + " 紅波:" + strArray10[num10];
                        }
                    }
                }
                return (str = str10 + "|" + str11);
            }
            if (play_id == "91021")
            {
                string[] strArray11 = o_v.Split(new char[] { '|' });
                string[] strArray12 = n_v.Split(new char[] { '|' });
                int num11 = strArray11.Length;
                string str12 = "";
                string str13 = "";
                for (int num12 = 0; num12 < num11; num12++)
                {
                    if (!strArray11[num12].Equals(strArray12[num12]))
                    {
                        if (num12 == 0)
                        {
                            str12 = "生肖:" + strArray11[num12];
                            str13 = "生肖:" + strArray12[num12];
                        }
                        if (num12 == 1)
                        {
                            str46 = str12;
                            str12 = str46 + " " + base.get_YearLian() + ":" + strArray11[num12];
                            str46 = str13;
                            str13 = str46 + " " + base.get_YearLian() + ":" + strArray12[num12];
                        }
                    }
                }
                return (str = str12 + "|" + str13);
            }
            if (play_id == "91022")
            {
                string[] strArray13 = o_v.Split(new char[] { '|' });
                string[] strArray14 = n_v.Split(new char[] { '|' });
                int num13 = strArray13.Length;
                string str14 = "";
                string str15 = "";
                for (int num14 = 0; num14 < num13; num14++)
                {
                    if (!strArray13[num14].Equals(strArray14[num14]))
                    {
                        if (num14 == 0)
                        {
                            str14 = "尾數:" + strArray13[num14];
                            str15 = "尾數:" + strArray14[num14];
                        }
                        if (num14 == 1)
                        {
                            str14 = str14 + " 0尾:" + strArray13[num14];
                            str15 = str15 + " 0尾:" + strArray14[num14];
                        }
                    }
                }
                return (str = str14 + "|" + str15);
            }
            if (play_id == "91008")
            {
                string[] strArray15 = o_v.Split(new char[] { '|' });
                string[] strArray16 = n_v.Split(new char[] { '|' });
                int num15 = strArray15.Length;
                string str16 = "";
                string str17 = "";
                for (int num16 = 0; num16 < num15; num16++)
                {
                    if (!strArray15[num16].Equals(strArray16[num16]))
                    {
                        if (num16 == 0)
                        {
                            str16 = "紅單:" + strArray15[num16];
                            str17 = "紅單:" + strArray16[num16];
                        }
                        if (num16 == 1)
                        {
                            str16 = str16 + " 紅雙:" + strArray15[num16];
                            str17 = str17 + " 紅雙:" + strArray16[num16];
                        }
                        if (num16 == 2)
                        {
                            str16 = " 藍單:" + strArray15[num16];
                            str17 = " 藍單:" + strArray16[num16];
                        }
                        if (num16 == 3)
                        {
                            str16 = str16 + " 藍雙:" + strArray15[num16];
                            str17 = str17 + " 藍雙:" + strArray16[num16];
                        }
                        if (num16 == 4)
                        {
                            str16 = " 綠單:" + strArray15[num16];
                            str17 = " 綠單:" + strArray16[num16];
                        }
                        if (num16 == 5)
                        {
                            str16 = str16 + " 綠雙:" + strArray15[num16];
                            str17 = str17 + " 綠雙:" + strArray16[num16];
                        }
                    }
                }
                return (str = str16 + "|" + str17);
            }
            if (play_id == "91057")
            {
                string[] strArray17 = o_v.Split(new char[] { '|' });
                string[] strArray18 = n_v.Split(new char[] { '|' });
                int num17 = strArray17.Length;
                string str18 = "";
                string str19 = "";
                for (int num18 = 0; num18 < num17; num18++)
                {
                    if (!strArray17[num18].Equals(strArray18[num18]))
                    {
                        if (num18 == 0)
                        {
                            str18 = " 紅大:" + strArray17[num18];
                            str19 = " 紅大:" + strArray18[num18];
                        }
                        if (num18 == 1)
                        {
                            str18 = str18 + " 紅小:" + strArray17[num18];
                            str19 = str19 + " 紅小:" + strArray18[num18];
                        }
                        if (num18 == 2)
                        {
                            str18 = " 藍大:" + strArray17[num18];
                            str19 = " 藍大:" + strArray18[num18];
                        }
                        if (num18 == 3)
                        {
                            str18 = str18 + " 藍小:" + strArray17[num18];
                            str19 = str19 + " 藍小:" + strArray18[num18];
                        }
                        if (num18 == 4)
                        {
                            str18 = str18 + " 綠大:" + strArray17[num18];
                            str19 = str19 + " 綠大:" + strArray18[num18];
                        }
                        if (num18 == 5)
                        {
                            str18 = str18 + " 綠小:" + strArray17[num18];
                            str19 = str19 + " 綠小:" + strArray18[num18];
                        }
                    }
                }
                return (str = str18 + "|" + str19);
            }
            if (play_id == "91031")
            {
                string[] strArray19 = o_v.Split(new char[] { ',' });
                string[] strArray20 = n_v.Split(new char[] { ',' });
                int num19 = strArray19.Length;
                string str20 = "";
                string str21 = "";
                for (int num20 = 0; num20 < num19; num20++)
                {
                    if (!strArray19[num20].Equals(strArray20[num20]))
                    {
                        if (num20 == 0)
                        {
                            str20 = "二肖連:" + strArray19[num20];
                            str21 = "二肖連:" + strArray20[num20];
                        }
                        if (num20 == 1)
                        {
                            str46 = str20;
                            str20 = str46 + " 連" + base.get_YearLian() + ":" + strArray19[num20];
                            str46 = str21;
                            str21 = str46 + " 連" + base.get_YearLian() + ":" + strArray20[num20];
                        }
                    }
                }
                return (str = str20 + "|" + str21);
            }
            if (play_id == "91032")
            {
                string[] strArray21 = o_v.Split(new char[] { ',' });
                string[] strArray22 = n_v.Split(new char[] { ',' });
                int num21 = strArray21.Length;
                string str22 = "";
                string str23 = "";
                for (int num22 = 0; num22 < num21; num22++)
                {
                    if (!strArray21[num22].Equals(strArray22[num22]))
                    {
                        if (num22 == 0)
                        {
                            str22 = "三肖連:" + strArray21[num22];
                            str23 = "三肖連:" + strArray22[num22];
                        }
                        if (num22 == 1)
                        {
                            str46 = str22;
                            str22 = str46 + " 連" + base.get_YearLian() + ":" + strArray21[num22];
                            str46 = str23;
                            str23 = str46 + " 連" + base.get_YearLian() + ":" + strArray22[num22];
                        }
                    }
                }
                return (str = str22 + "|" + str23);
            }
            if (play_id == "91033")
            {
                string[] strArray23 = o_v.Split(new char[] { ',' });
                string[] strArray24 = n_v.Split(new char[] { ',' });
                int num23 = strArray23.Length;
                string str24 = "";
                string str25 = "";
                for (int num24 = 0; num24 < num23; num24++)
                {
                    if (!strArray23[num24].Equals(strArray24[num24]))
                    {
                        if (num24 == 0)
                        {
                            str24 = "四肖連:" + strArray23[num24];
                            str25 = "四肖連:" + strArray24[num24];
                        }
                        if (num24 == 1)
                        {
                            str46 = str24;
                            str24 = str46 + " 連" + base.get_YearLian() + ":" + strArray23[num24];
                            str46 = str25;
                            str25 = str46 + " 連" + base.get_YearLian() + ":" + strArray24[num24];
                        }
                    }
                }
                return (str = str24 + "|" + str25);
            }
            if (play_id == "91058")
            {
                string[] strArray25 = o_v.Split(new char[] { ',' });
                string[] strArray26 = n_v.Split(new char[] { ',' });
                int num25 = strArray25.Length;
                string str26 = "";
                string str27 = "";
                for (int num26 = 0; num26 < num25; num26++)
                {
                    if (!strArray25[num26].Equals(strArray26[num26]))
                    {
                        if (num26 == 0)
                        {
                            str26 = "五肖連:" + strArray25[num26];
                            str27 = "五肖連:" + strArray26[num26];
                        }
                        if (num26 == 1)
                        {
                            str46 = str26;
                            str26 = str46 + " 連" + base.get_YearLian() + ":" + strArray25[num26];
                            str46 = str27;
                            str27 = str46 + " 連" + base.get_YearLian() + ":" + strArray26[num26];
                        }
                    }
                }
                return (str = str26 + "|" + str27);
            }
            if (play_id == "91034")
            {
                string[] strArray27 = o_v.Split(new char[] { ',' });
                string[] strArray28 = n_v.Split(new char[] { ',' });
                int num27 = strArray27.Length;
                string str28 = "";
                string str29 = "";
                for (int num28 = 0; num28 < num27; num28++)
                {
                    if (!strArray27[num28].Equals(strArray28[num28]))
                    {
                        if (num28 == 0)
                        {
                            str28 = "二尾連:" + strArray27[num28];
                            str29 = "二尾連:" + strArray28[num28];
                        }
                        if (num28 == 1)
                        {
                            str28 = str28 + " 連0:" + strArray27[num28];
                            str29 = str29 + " 連0:" + strArray28[num28];
                        }
                    }
                }
                return (str = str28 + "|" + str29);
            }
            if (play_id == "91035")
            {
                string[] strArray29 = o_v.Split(new char[] { ',' });
                string[] strArray30 = n_v.Split(new char[] { ',' });
                int num29 = strArray29.Length;
                string str30 = "";
                string str31 = "";
                for (int num30 = 0; num30 < num29; num30++)
                {
                    if (!strArray29[num30].Equals(strArray30[num30]))
                    {
                        if (num30 == 0)
                        {
                            str30 = "三尾連:" + strArray29[num30];
                            str31 = "三尾連:" + strArray30[num30];
                        }
                        if (num30 == 1)
                        {
                            str30 = str30 + " 連0:" + strArray29[num30];
                            str31 = str31 + " 連0:" + strArray30[num30];
                        }
                    }
                }
                return (str = str30 + "|" + str31);
            }
            if (play_id == "91036")
            {
                string[] strArray31 = o_v.Split(new char[] { ',' });
                string[] strArray32 = n_v.Split(new char[] { ',' });
                int num31 = strArray31.Length;
                string str32 = "";
                string str33 = "";
                for (int num32 = 0; num32 < num31; num32++)
                {
                    if (!strArray31[num32].Equals(strArray32[num32]))
                    {
                        if (num32 == 0)
                        {
                            str32 = "四尾連:" + strArray31[num32];
                            str33 = "四尾連:" + strArray32[num32];
                        }
                        if (num32 == 1)
                        {
                            str32 = str32 + " 連0:" + strArray31[num32];
                            str33 = str33 + " 連0:" + strArray32[num32];
                        }
                    }
                }
                return (str = str32 + "|" + str33);
            }
            if (play_id == "91059")
            {
                string[] strArray33 = o_v.Split(new char[] { ',' });
                string[] strArray34 = n_v.Split(new char[] { ',' });
                int num33 = strArray33.Length;
                string str34 = "";
                string str35 = "";
                for (int num34 = 0; num34 < num33; num34++)
                {
                    if (!strArray33[num34].Equals(strArray34[num34]))
                    {
                        if (num34 == 0)
                        {
                            str34 = "五尾連:" + strArray33[num34];
                            str35 = "五尾連:" + strArray34[num34];
                        }
                        if (num34 == 1)
                        {
                            str34 = str34 + " 連0:" + strArray33[num34];
                            str35 = str35 + " 連0:" + strArray34[num34];
                        }
                    }
                }
                return (str = str34 + "|" + str35);
            }
            if (play_id == "91052")
            {
                string[] strArray35 = o_v.Split(new char[] { '|' });
                string[] strArray36 = n_v.Split(new char[] { '|' });
                int num35 = strArray35.Length;
                string str36 = "";
                string str37 = "";
                for (int num36 = 0; num36 < num35; num36++)
                {
                    if (!strArray35[num36].Equals(strArray36[num36]))
                    {
                        if (num36 == 0)
                        {
                            str36 = "單0、雙7:" + strArray35[num36];
                            str37 = "單0、雙7:" + strArray36[num36];
                        }
                        if (num36 == 1)
                        {
                            str36 = str36 + " 單1、雙6:" + strArray35[num36];
                            str37 = str37 + " 單1、雙6:" + strArray36[num36];
                        }
                        if (num36 == 2)
                        {
                            str36 = " 單2、雙5:" + strArray35[num36];
                            str37 = " 單2、雙5:" + strArray36[num36];
                        }
                        if (num36 == 3)
                        {
                            str36 = str36 + "  單3、雙4:" + strArray35[num36];
                            str37 = str37 + "  單3、雙4:" + strArray36[num36];
                        }
                        if (num36 == 4)
                        {
                            str36 = " 單4、雙3:" + strArray35[num36];
                            str37 = " 單4、雙3:" + strArray36[num36];
                        }
                        if (num36 == 5)
                        {
                            str36 = str36 + " 單5、雙2:" + strArray35[num36];
                            str37 = str37 + " 單5、雙2:" + strArray36[num36];
                        }
                        if (num36 == 6)
                        {
                            str36 = " 單6、雙1:" + strArray35[num36];
                            str37 = " 單6、雙1:" + strArray36[num36];
                        }
                        if (num36 == 7)
                        {
                            str36 = str36 + " 單7、雙0:" + strArray35[num36];
                            str37 = str37 + " 單7、雙0:" + strArray36[num36];
                        }
                    }
                }
                return (str = str36 + "|" + str37);
            }
            if (play_id == "91056")
            {
                string[] strArray37 = o_v.Split(new char[] { '|' });
                string[] strArray38 = n_v.Split(new char[] { '|' });
                int num37 = strArray37.Length;
                string str38 = "";
                string str39 = "";
                for (int num38 = 0; num38 < num37; num38++)
                {
                    if (!strArray37[num38].Equals(strArray38[num38]))
                    {
                        if (num38 == 0)
                        {
                            str38 = " 大0、小7:" + strArray37[num38];
                            str39 = " 大0、小7:" + strArray38[num38];
                        }
                        if (num38 == 1)
                        {
                            str38 = str38 + " 大1、小6:" + strArray37[num38];
                            str39 = str39 + " 大1、小6:" + strArray38[num38];
                        }
                        if (num38 == 2)
                        {
                            str38 = str38 + " 大2、小5:" + strArray37[num38];
                            str39 = str39 + " 大2、小5:" + strArray38[num38];
                        }
                        if (num38 == 3)
                        {
                            str38 = str38 + " 大3、小4:" + strArray37[num38];
                            str39 = str39 + " 大3、小4:" + strArray38[num38];
                        }
                        if (num38 == 4)
                        {
                            str38 = str38 + " 大4、小3:" + strArray37[num38];
                            str39 = str39 + " 大4、小3:" + strArray38[num38];
                        }
                        if (num38 == 5)
                        {
                            str38 = str38 + " 大5、小2:" + strArray37[num38];
                            str39 = str39 + " 大5、小2:" + strArray38[num38];
                        }
                        if (num38 == 6)
                        {
                            str38 = str38 + " 大6、小1:" + strArray37[num38];
                            str39 = str39 + " 大6、小1:" + strArray38[num38];
                        }
                        if (num38 == 7)
                        {
                            str38 = str38 + " 大7、小0:" + strArray37[num38];
                            str39 = str39 + " 大7、小0:" + strArray38[num38];
                        }
                    }
                }
                return (str = str38 + "|" + str39);
            }
            if (play_id == "91053")
            {
                string[] strArray39 = o_v.Split(new char[] { '|' });
                string[] strArray40 = n_v.Split(new char[] { '|' });
                int num39 = strArray39.Length;
                string str40 = "";
                string str41 = "";
                for (int num40 = 0; num40 < num39; num40++)
                {
                    if (!strArray39[num40].Equals(strArray40[num40]))
                    {
                        if (num40 == 0)
                        {
                            str40 = "金:" + strArray39[num40];
                            str41 = "金:" + strArray40[num40];
                        }
                        if (num40 == 1)
                        {
                            str40 = str40 + " 木:" + strArray39[num40];
                            str41 = str41 + " 木:" + strArray40[num40];
                        }
                        if (num40 == 2)
                        {
                            str40 = " 水:" + strArray39[num40];
                            str41 = " 水:" + strArray40[num40];
                        }
                        if (num40 == 3)
                        {
                            str40 = str40 + " 火:" + strArray39[num40];
                            str41 = str41 + " 火:" + strArray40[num40];
                        }
                        if (num40 == 4)
                        {
                            str40 = " 土:" + strArray39[num40];
                            str41 = " 土:" + strArray40[num40];
                        }
                    }
                }
                return (str = str40 + "|" + str41);
            }
            if (play_id == "91054")
            {
                string[] strArray41 = o_v.Split(new char[] { '|' });
                string[] strArray42 = n_v.Split(new char[] { '|' });
                int num41 = strArray41.Length;
                string str42 = "";
                string str43 = "";
                for (int num42 = 0; num42 < num41; num42++)
                {
                    if (!strArray41[num42].Equals(strArray42[num42]))
                    {
                        if (num42 == 0)
                        {
                            str42 = "不連" + base.get_YearLian() + ":" + strArray41[num42];
                            str43 = "不連" + base.get_YearLian() + ":" + strArray42[num42];
                        }
                        if (num42 == 1)
                        {
                            str46 = str42;
                            str42 = str46 + " 連" + base.get_YearLian() + ":" + strArray41[num42];
                            str46 = str43;
                            str43 = str46 + " 連" + base.get_YearLian() + ":" + strArray42[num42];
                        }
                    }
                }
                return (str = str42 + "|" + str43);
            }
            if (!(play_id == "91055"))
            {
                return str;
            }
            string[] strArray43 = o_v.Split(new char[] { ',' });
            string[] strArray44 = n_v.Split(new char[] { ',' });
            int length = strArray43.Length;
            string str44 = "";
            string str45 = "";
            for (int i = 0; i < length; i++)
            {
                if (!strArray43[i].Equals(strArray44[i]))
                {
                    if (i == 0)
                    {
                        str44 = "不連0尾:" + strArray43[i];
                        str45 = "不連0尾:" + strArray44[i];
                    }
                    if (i == 1)
                    {
                        str44 = str44 + " 連0尾:" + strArray43[i];
                        str45 = str45 + " 連0尾:" + strArray44[i];
                    }
                }
            }
            return (str = str44 + "|" + str45);
        }

        public void ssc168_kjj_log(DataTable oldTable, DataTable newTable, string operate_type)
        {
            string str = this.get_master_name();
            string str2 = this.get_children_name();
            string gameNameByID = base.GetGameNameByID(0x11.ToString());
            string category = "";
            string str5 = "";
            string str6 = "";
            string s = "";
            string str8 = "";
            int num = 0;
            string note = "快捷鍵微调" + str6;
            string act = "";
            int num2 = Convert.ToInt32((LSEnums.LogTypeID) 0);
            if (operate_type.Equals("7"))
            {
                note = "快捷鍵手工输入赔率值,微調" + str6;
            }
            string str11 = "";
            DataTable table = CallBLL.cz_phase_ssc168_bll.IsOpenPhaseByTime(DateTime.Now);
            if (table != null)
            {
                str11 = table.Rows[0]["phase"].ToString();
            }
            List<CommandInfo> list = new List<CommandInfo>();
            int num3 = 0;
            foreach (DataRow row in oldTable.Rows)
            {
                category = row["category"].ToString();
                str5 = row["play_name"].ToString();
                str6 = row["put_amount"].ToString();
                s = row["current_odds"].ToString();
                str8 = newTable.Rows[num3]["current_odds"].ToString();
                num = int.Parse(row["odds_id"].ToString());
                if (s != str8)
                {
                    if (double.Parse(s) > double.Parse(str8))
                    {
                        act = "降賠率";
                    }
                    else
                    {
                        act = "升賠率";
                    }
                    List<SqlParameter> paramList = new List<SqlParameter>();
                    string str12 = this.add_sys_log(str, str2, category, str5, str6, gameNameByID, str11, act, num, s, str8, note, num2, 0x11, ref paramList);
                    CommandInfo item = new CommandInfo {
                        CommandText = str12,
                        Parameters = paramList.ToArray()
                    };
                    list.Add(item);
                }
                num3++;
            }
            if (list.Count > 0)
            {
                CallBLL.cz_system_log_bll.execute_sql_tran(list);
            }
        }

        public void ssc168_log(cz_odds_ssc168 oldModel, cz_odds_ssc168 newModel, string operate_type)
        {
            string str = this.get_master_name();
            string str2 = this.get_children_name();
            string gameNameByID = base.GetGameNameByID(0x11.ToString());
            string category = oldModel.get_category();
            string str5 = oldModel.get_play_name();
            string str6 = oldModel.get_put_amount();
            string s = oldModel.get_current_odds();
            string str8 = newModel.get_current_odds();
            int num = oldModel.get_odds_id();
            string note = "微調" + str6;
            string act = "";
            int num2 = Convert.ToInt32((LSEnums.LogTypeID) 0);
            if (operate_type.Equals("3"))
            {
                note = "手工输入赔率值,微調" + str6;
            }
            string str11 = "";
            DataTable table = CallBLL.cz_phase_ssc168_bll.IsOpenPhaseByTime(DateTime.Now);
            if (table != null)
            {
                str11 = table.Rows[0]["phase"].ToString();
            }
            if (s != str8)
            {
                if (double.Parse(s) > double.Parse(str8))
                {
                    act = "降賠率";
                }
                else
                {
                    act = "升賠率";
                }
                List<SqlParameter> paramList = new List<SqlParameter>();
                string str12 = this.add_sys_log(str, str2, category, str5, str6, gameNameByID, str11, act, num, s, str8, note, num2, 0x11, ref paramList);
                CallBLL.cz_system_log_bll.executte_sql(str12, paramList.ToArray());
            }
        }

        public static void stat_online(string user, string user_type)
        {
            string str = "Online_Stat";
            string str2 = "Online_Stat_CheckTime";
            string str3 = "online_User_Key";
            string iP = LSRequest.GetIP();
            DateTime now = DateTime.Now;
            if (HttpContext.Current.Application[str2] == null)
            {
                HttpContext.Current.Application.Lock();
                HttpContext.Current.Application[str2] = now;
                HttpContext.Current.Application.UnLock();
            }
            if (HttpContext.Current.Application[str] == null)
            {
                ConcurrentDictionary<string, object> dictionary = new ConcurrentDictionary<string, object>();
                ArrayList list = new ArrayList();
                list.Add(iP);
                list.Add(now);
                dictionary.GetOrAdd(user, list);
                HttpContext.Current.Application.Lock();
                HttpContext.Current.Application[str] = dictionary;
                HttpContext.Current.Application.UnLock();
                insert_online(iP, user, user_type, now, now);
            }
            else
            {
                ConcurrentDictionary<string, object> dictionary2 = HttpContext.Current.Application[str] as ConcurrentDictionary<string, object>;
                if (dictionary2.ContainsKey(user))
                {
                    if (HttpContext.Current.Application[str3] == null)
                    {
                        ConcurrentDictionary<string, object> dictionary3 = new ConcurrentDictionary<string, object>();
                        HttpContext.Current.Application[str3] = dictionary3;
                    }
                    ConcurrentDictionary<string, object> dictionary4 = HttpContext.Current.Application[str3] as ConcurrentDictionary<string, object>;
                    ArrayList infoList = new ArrayList();
                    infoList.Add(iP);
                    infoList.Add(DateTime.Now);
                    dictionary4.AddOrUpdate(user, infoList, (key, oldValue) => infoList);
                    dictionary2.AddOrUpdate(user, infoList, (key, oldValue) => infoList);
                }
                else
                {
                    ArrayList list2 = new ArrayList();
                    list2.Add(iP);
                    list2.Add(now);
                    dictionary2.GetOrAdd(user, list2);
                    insert_online(iP, user, user_type, now, now);
                }
                if (DateTime.Compare(Convert.ToDateTime(HttpContext.Current.Application[str2].ToString()).AddMinutes(1.0), DateTime.Now) < 0)
                {
                    HttpContext.Current.Application.Lock();
                    HttpContext.Current.Application[str2] = DateTime.Now;
                    HttpContext.Current.Application.UnLock();
                    update_online();
                }
            }
        }

        public static void stat_top_online(string loginname)
        {
            string str = "Online_Stat";
            string str2 = "Online_Stat_CheckTime";
            DateTime now = DateTime.Now;
            string str3 = string.Format("update  cz_stat_online  set  first_time = '{0}',last_time= '{1}'  where u_name =@u_name ", now, now);
            SqlParameter[] parameterArray = new SqlParameter[] { new SqlParameter("@u_name", SqlDbType.NVarChar) };
            parameterArray[0].Value = loginname;
            CallBLL.cz_stat_online_bll.executte_sql(str3, parameterArray);
            string str4 = "";
            if (HttpContext.Current.Application[str] == null)
            {
                HttpContext.Current.Application.Lock();
                HttpContext.Current.Application[str2] = now;
                HttpContext.Current.Application.UnLock();
                str4 = string.Format(" select * from cz_stat_top_online  with(NOLOCK) where update_time > '{0}' and update_time < '{1}' ", DateTime.Today, DateTime.Today.AddHours(24.0));
                if (CallBLL.cz_stat_top_online_bll.query_sql(str4).Rows.Count <= 0)
                {
                    str4 = string.Format("insert into cz_stat_top_online values({0},'{1}') ", 1, DateTime.Now);
                    CallBLL.cz_stat_top_online_bll.executte_sql(str4);
                }
            }
            else
            {
                int num = 1;
                string str5 = string.Format(" select count(1)  from cz_stat_online  with(NOLOCK) where last_time > '{0}' ", now.AddMinutes(-3.0));
                DataTable table2 = CallBLL.cz_stat_online_bll.query_sql(str5, parameterArray);
                if (table2.Rows.Count > 0)
                {
                    num = int.Parse(table2.Rows[0][0].ToString());
                }
                str4 = string.Format("select * from cz_stat_top_online with(NOLOCK) where update_time > '{0}' and update_time < '{1}' ", DateTime.Today, DateTime.Today.AddHours(24.0));
                DataTable table3 = CallBLL.cz_stat_top_online_bll.query_sql(str4);
                if (table3.Rows.Count > 0)
                {
                    string s = table3.Rows[0]["top_cnt"].ToString();
                    if (num > int.Parse(s))
                    {
                        str4 = string.Format("update cz_stat_top_online set top_cnt = {0}, update_time = '{1}' where update_time > '{2}' and update_time < '{3}' ", new object[] { num, now, DateTime.Today, DateTime.Today.AddHours(24.0) });
                        CallBLL.cz_stat_top_online_bll.executte_sql(str4);
                    }
                }
                else
                {
                    str4 = string.Format("insert into cz_stat_top_online values({0},'{1}') ", num, now);
                    CallBLL.cz_stat_top_online_bll.executte_sql(str4);
                }
            }
        }

        public void sxlws_group_log(cz_odds_six oldModel, string old_spl, string new_spl, string operate_type, bool isSXL)
        {
            if (!old_spl.Equals(new_spl))
            {
                string str = this.get_master_name();
                string str2 = this.get_children_name();
                string gameNameByID = base.GetGameNameByID(100.ToString());
                string category = oldModel.get_category();
                string str5 = oldModel.get_play_name();
                string str6 = oldModel.get_put_amount();
                double num = 0.0;
                double num2 = 0.0;
                int num3 = oldModel.get_odds_id();
                string str7 = "微調";
                string act = "";
                int num4 = Convert.ToInt32((LSEnums.LogTypeID) 0);
                string[] strArray = old_spl.Split(new char[] { ',' });
                string[] strArray2 = new_spl.Split(new char[] { ',' });
                if (operate_type.Equals("3"))
                {
                    str7 = "手工输入微調值,微調";
                }
                string str9 = "";
                cz_phase_six currentPhase = CallBLL.cz_phase_six_bll.GetCurrentPhase();
                if (currentPhase != null)
                {
                    str9 = currentPhase.get_phase().ToString();
                }
                if (strArray[0].Equals(strArray2[0]))
                {
                    num = double.Parse(strArray[1]);
                    num2 = double.Parse(strArray2[1]);
                    str7 = string.Format("{0}連{1}賠率", str7, isSXL ? base.get_YearLian() : "0尾");
                }
                else
                {
                    num = double.Parse(strArray[0]);
                    num2 = double.Parse(strArray2[0]);
                    str7 = string.Format("{0}不連{1}賠率", str7, isSXL ? base.get_YearLian() : "0尾");
                }
                if (num > num2)
                {
                    act = "降賠率";
                }
                else
                {
                    act = "升賠率";
                }
                if (num != num2)
                {
                    List<SqlParameter> paramList = new List<SqlParameter>();
                    string str10 = this.add_sys_log(str, str2, category, str5, str6, gameNameByID, str9, act, num3, old_spl, new_spl, str7, num4, 100, ref paramList);
                    CallBLL.cz_system_log_bll.executte_sql(str10, paramList.ToArray());
                }
            }
        }

        public void sxlws_number_log(cz_odds_six oldModel, double oldWT, double newWT, string number, string operate_type, bool isSXL)
        {
            string str = this.get_master_name();
            string str2 = this.get_children_name();
            string gameNameByID = base.GetGameNameByID(100.ToString());
            string category = oldModel.get_category();
            string str5 = oldModel.get_play_name();
            string str6 = oldModel.get_put_amount();
            double num = oldWT;
            double num2 = newWT;
            int num3 = oldModel.get_odds_id();
            string note = "微調號碼";
            string act = "";
            int num4 = Convert.ToInt32((LSEnums.LogTypeID) 0);
            if (operate_type.Equals("3"))
            {
                note = "手工输入微調值,微調號碼";
            }
            if (isSXL)
            {
                str6 = this.getAnimal(int.Parse(number));
            }
            else
            {
                str6 = (int.Parse(number) < 10) ? ("0" + int.Parse(number)) : number.ToString();
            }
            string str9 = "";
            cz_phase_six currentPhase = CallBLL.cz_phase_six_bll.GetCurrentPhase();
            if (currentPhase != null)
            {
                str9 = currentPhase.get_phase().ToString();
            }
            if (num != num2)
            {
                if (num > num2)
                {
                    act = "降賠率";
                }
                else
                {
                    act = "升賠率";
                }
                List<SqlParameter> paramList = new List<SqlParameter>();
                string str10 = this.add_sys_log(str, str2, category, str5, str6, gameNameByID, str9, act, num3, num.ToString(), num2.ToString(), note, num4, 100, ref paramList);
                CallBLL.cz_system_log_bll.executte_sql(str10, paramList.ToArray());
            }
        }

        public void sys_jp_set_kc_log(DataTable oldDT, DataTable newDT, bool isYL)
        {
            string str = this.get_master_name();
            string str2 = this.get_children_name();
            string gameNameByID = "";
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
            int count = oldDT.Rows.Count;
            IEqualityComparer<DataRow> comparer = DataRowComparer.Default;
            for (int i = 0; i < count; i++)
            {
                str7 = "";
                str8 = "";
                DataRow x = oldDT.Rows[i];
                DataRow y = newDT.Rows[i];
                if (!comparer.Equals(x, y))
                {
                    string str12 = x["phase"].ToString();
                    string str13 = x["downbase1"].ToString();
                    if (isYL)
                    {
                        str13 = x["yl_downbase"].ToString();
                    }
                    string str14 = x["downbase2"].ToString();
                    string s = x["lottery_type"].ToString();
                    x["yl_downbase"].ToString();
                    y["phase"].ToString();
                    string str16 = y["downbase1"].ToString();
                    if (isYL)
                    {
                        str16 = y["yl_downbase"].ToString();
                    }
                    string str17 = y["downbase2"].ToString();
                    y["lottery_type"].ToString();
                    y["yl_downbase"].ToString();
                    gameNameByID = base.GetGameNameByID(s);
                    if (!str13.Equals(str16))
                    {
                        if (isYL)
                        {
                            str7 = " 遺漏自動降賠率: " + str12 + "期遺漏降:" + str13;
                            str8 = " 遺漏自動降賠率: " + str12 + "期遺漏降:" + str16;
                        }
                        else
                        {
                            str7 = " 兩面自動降賠率: " + str12 + "期沒出降:" + str13;
                            str8 = " 兩面自動降賠率: " + str12 + "期沒出降:" + str16;
                        }
                    }
                    if (!str14.Equals(str17) && !isYL)
                    {
                        string str20 = str7;
                        str7 = str20 + " 兩面自動降賠率: " + str12 + "期連出降:" + str14;
                        string str21 = str8;
                        str8 = str21 + " 兩面自動降賠率: " + str12 + "期連出降:" + str17;
                    }
                    if (!isYL)
                    {
                        List<SqlParameter> paramList = new List<SqlParameter>();
                        string str18 = this.add_sys_log(str, str2, category, str5, str6, gameNameByID, str11, act, num, str7, str8, note, num2, int.Parse(s), ref paramList);
                        CallBLL.cz_system_log_bll.executte_sql(str18, paramList.ToArray());
                    }
                    else
                    {
                        int num5 = 0;
                        if (!s.Equals(num5.ToString()))
                        {
                            int num6 = 3;
                            if (!s.Equals(num6.ToString()))
                            {
                                int num7 = 14;
                                if (!s.Equals(num7.ToString()))
                                {
                                    continue;
                                }
                            }
                        }
                        if (!str13.Equals(str16))
                        {
                            List<SqlParameter> list2 = new List<SqlParameter>();
                            string str19 = this.add_sys_log(str, str2, category, str5, str6, gameNameByID, str11, act, num, str7, str8, note, num2, int.Parse(s), ref list2);
                            CallBLL.cz_system_log_bll.executte_sql(str19, list2.ToArray());
                        }
                    }
                }
            }
        }

        public void sys_param_set_kc_log(DataTable oldDT, DataTable newDT)
        {
            string str = this.get_master_name();
            string str2 = this.get_children_name();
            string gameNameByID = "";
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
            int count = oldDT.Rows.Count;
            IEqualityComparer<DataRow> comparer = DataRowComparer.Default;
            for (int i = 0; i < count; i++)
            {
                str7 = "";
                str8 = "";
                DataRow x = oldDT.Rows[i];
                DataRow y = newDT.Rows[i];
                if (!comparer.Equals(x, y))
                {
                    string str12 = x["close_day_advance"].ToString();
                    string str13 = x["close_night_advance"].ToString();
                    string str14 = x["isopen"].ToString();
                    string str15 = x["odds_ratio"].ToString();
                    string s = x["lottery_type"].ToString();
                    string str17 = x["yl_isopen"].ToString();
                    string str18 = y["close_day_advance"].ToString();
                    string str19 = y["close_night_advance"].ToString();
                    string str20 = y["isopen"].ToString();
                    string str21 = y["odds_ratio"].ToString();
                    y["lottery_type"].ToString();
                    y["yl_isopen"].ToString();
                    gameNameByID = base.GetGameNameByID(s);
                    if (!str12.Equals(str18) && (str17 == "0"))
                    {
                        str7 = " 日場 -（封盤提前）: " + str12 + "秒";
                        str8 = " 日場 -（封盤提前）: " + str18 + "秒";
                    }
                    if (!str13.Equals(str19) && (str17 == "0"))
                    {
                        str7 = str7 + " 夜場 -（封盤提前）: " + str13 + "秒";
                        str8 = str8 + " 夜場 -（封盤提前）: " + str19 + "秒";
                    }
                    if (!str14.Equals(str20))
                    {
                        if (str17 == "1")
                        {
                            str7 = str7 + " 遺漏自動降賠率: " + ((str14 == "0") ? "禁用" : "啟用");
                            str8 = str8 + " 遺漏自動降賠率: " + ((str20 == "0") ? "禁用" : "啟用");
                        }
                        else
                        {
                            str7 = str7 + " 同路號碼隨大路降賠率: " + ((str14 == "0") ? "禁用" : "啟用");
                            str8 = str8 + " 同路號碼隨大路降賠率: " + ((str20 == "0") ? "禁用" : "啟用");
                        }
                    }
                    if (!str15.Equals(str21) && (str17 == "0"))
                    {
                        str7 = str7 + "  同路號碼隨大路降賠率比例: " + str15;
                        str8 = str8 + "  同路號碼隨大路降賠率比例: " + str21;
                    }
                    List<SqlParameter> paramList = new List<SqlParameter>();
                    string str22 = this.add_sys_log(str, str2, category, str5, str6, gameNameByID, str11, act, num, str7, str8, note, num2, int.Parse(s), ref paramList);
                    CallBLL.cz_system_log_bll.executte_sql(str22, paramList.ToArray());
                }
            }
        }

        public void sys_set_log(cz_system_set_six old_model, cz_system_set_six new_model)
        {
            string str = this.get_master_name();
            string str2 = this.get_children_name();
            string gameNameByID = base.GetGameNameByID(100.ToString());
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
            if (!old_model.get_single_number_isdown().Equals(new_model.get_single_number_isdown()))
            {
                List<SqlParameter> paramList = new List<SqlParameter>();
                string str14 = this.add_sys_log(str, str2, category, str5, str6, gameNameByID, str11, act, num, "連碼項降賠方式:" + (old_model.get_single_number_isdown().Equals(0) ? "玩法賠率" : "號碼微調賠率"), "連碼項降賠方式:" + (new_model.get_single_number_isdown().Equals(0) ? "玩法賠率" : "號碼微調賠率"), note, num2, 100, ref paramList);
                CallBLL.cz_system_log_bll.executte_sql(str14, paramList.ToArray());
            }
            if (!old_model.get_is_tmab().Equals(new_model.get_is_tmab()))
            {
                List<SqlParameter> list2 = new List<SqlParameter>();
                string str17 = this.add_sys_log(str, str2, category, str5, str6, gameNameByID, str11, act, num, "特碼項降賠方式(貨量):" + (old_model.get_is_tmab().Equals(0) ? "特碼A、B獨立" : "特碼A、B合併"), "特碼項降賠方式(貨量):" + (new_model.get_is_tmab().Equals(0) ? "特碼A、B獨立" : "特碼A、B合併"), note, num2, 100, ref list2);
                CallBLL.cz_system_log_bll.executte_sql(str17, list2.ToArray());
            }
            Type type = old_model.GetType();
            for (int i = 0; i < 0x13; i++)
            {
                string name = "ev_" + i.ToString();
                object obj2 = type.GetProperty(name).GetValue(old_model, null);
                object obj3 = type.GetProperty(name).GetValue(new_model, null);
                str7 = Convert.ToString(obj2);
                str8 = Convert.ToString(obj3);
                string str19 = "";
                if (!str7.Equals(str8))
                {
                    switch (i)
                    {
                        case 0:
                            str19 = "特碼A<—>特碼B 保持賠率差 ";
                            break;

                        case 1:
                            str19 = "特碼C是否開放（1=開放、0=不開放） ";
                            break;

                        case 2:
                            str19 = "（九點）“正碼”默認封盤時間 hh:mm:ss ";
                            break;

                        case 3:
                            str19 = "（九點）“特碼”默認封盤時間 hh:mm:ss ";
                            break;

                        case 4:
                            str19 = "（九點半）“正碼”默認封盤時間 hh:mm:ss ";
                            break;

                        case 5:
                            str19 = "（九點半）“特碼”默認封盤時間 hh:mm:ss ";
                            break;

                        case 6:
                            str19 = "代理補貨，總代理是否占成(1=有、0=沒 ";
                            break;

                        case 7:
                            str19 = "正碼過關 默認開盤（1=開盤、0=不開盤） ";
                            break;

                        case 8:
                            str19 = "生肖連 尾數連 默認開盤（1=開盤、0=不開盤） ";
                            break;

                        case 9:
                            str19 = "四中一 默認開盤（1=開盤、0=不開盤） ";
                            break;

                        case 10:
                            str19 = "連碼‘復式’最多號碼數‘五不中’減2個(建議不超過10個) ";
                            break;

                        case 11:
                            str19 = "五不中 默認開盤（1=開盤、0=不開盤） ";
                            break;

                        case 12:
                            str19 = "生肖連 尾數連‘復式’最多號碼數(建議不超過8個) ";
                            break;

                        case 13:
                            str19 = "二全中按正碼賠率換算（正碼@7、二全中@56）比例微調值【即：微調值*二全中@56】 ";
                            break;

                        case 14:
                            str19 = "賠率浮點后最大位數 ";
                            break;

                        case 15:
                            str19 = "新增賬戶【股東、總代理、代理、會員】時默認設為最高可占成比例(1=啟用、0=停用) ";
                            break;

                        case 0x10:
                            str19 = "新增會員時默認退水設定（0=全不退、1=退到底） ";
                            break;

                        case 0x11:
                            str19 = "生肖降賠率時四中一所對應號碼降賠率比例微調值【即：微調值*生肖所降賠率】 ";
                            break;

                        case 0x12:
                            str19 = "報表查詢起始日期 ";
                            break;
                    }
                    List<SqlParameter> list3 = new List<SqlParameter>();
                    string str20 = this.add_sys_log(str, str2, category, str5, str6, gameNameByID, str11, act, num, str19 + ":" + str7, str19 + ":" + str8, note, num2, 100, ref list3);
                    CallBLL.cz_system_log_bll.executte_sql(str20, list3.ToArray());
                }
            }
        }

        public void sys_set_log(string u_name, string old_negative_sale, string new_negative_sale)
        {
            string str = u_name;
            string str2 = this.get_children_name();
            string gameNameByID = base.GetGameNameByID(100.ToString());
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
            if (old_negative_sale.Equals("0"))
            {
                str7 = "總監超額走飛 : 不允許";
            }
            else
            {
                str7 = "總監超額走飛 : 允許";
            }
            if (new_negative_sale.Equals("0"))
            {
                str8 = "總監超額走飛 : 不允許";
            }
            else
            {
                str8 = "總監超額走飛 : 允許";
            }
            List<SqlParameter> paramList = new List<SqlParameter>();
            string str12 = this.add_sys_log(str, str2, category, str5, str6, gameNameByID, str11, act, num, str7, str8, note, num2, 100, ref paramList);
            CallBLL.cz_system_log_bll.executte_sql(str12, paramList.ToArray());
        }

        public void tm_log(cz_odds_six oldModel, cz_odds_six newModel, string operate_type)
        {
            string str = this.get_master_name();
            string str2 = this.get_children_name();
            string gameNameByID = base.GetGameNameByID(100.ToString());
            string category = oldModel.get_category();
            string str5 = oldModel.get_play_name();
            string str6 = oldModel.get_put_amount();
            string s = oldModel.get_current_odds();
            string str8 = newModel.get_current_odds();
            int num = oldModel.get_odds_id();
            string note = "微調" + str6;
            string act = "";
            int num2 = Convert.ToInt32((LSEnums.LogTypeID) 0);
            if (operate_type.Equals("3"))
            {
                note = "手工输入赔率值,微調" + str6;
            }
            string str11 = "";
            cz_phase_six currentPhase = CallBLL.cz_phase_six_bll.GetCurrentPhase();
            if (currentPhase != null)
            {
                str11 = currentPhase.get_phase().ToString();
            }
            if (s != str8)
            {
                if (double.Parse(s) > double.Parse(str8))
                {
                    act = "降賠率";
                }
                else
                {
                    act = "升賠率";
                }
                List<SqlParameter> paramList = new List<SqlParameter>();
                string str12 = this.add_sys_log(str, str2, category, str5, str6, gameNameByID, str11, act, num, s, str8, note, num2, 100, ref paramList);
                CallBLL.cz_system_log_bll.executte_sql(str12, paramList.ToArray());
            }
        }

        public void Un_User_Lock(string fgs_name)
        {
            if (base.Application["lock_" + fgs_name] != null)
            {
                base.Application.Lock();
                base.Application["lock_" + fgs_name] = null;
                base.Application.UnLock();
            }
        }

        protected static void update_online()
        {
            string str = "online_User_Key";
            HttpContext.Current.Application.Lock();
            ConcurrentDictionary<string, object> dictionary = HttpContext.Current.Application[str] as ConcurrentDictionary<string, object>;
            HttpContext.Current.Application.UnLock();
            List<CommandInfo> list = new List<CommandInfo>();
            foreach (KeyValuePair<string, object> pair in dictionary)
            {
                object obj2;
                ArrayList list2 = pair.Value as ArrayList;
                string str2 = string.Format("update  cz_stat_online set ip=@ip, last_time=@last_time where u_name =@u_name ", new object[0]);
                SqlParameter[] parameterArray2 = new SqlParameter[3];
                SqlParameter parameter = new SqlParameter("@ip", SqlDbType.NVarChar) {
                    Value = list2[0].ToString()
                };
                parameterArray2[0] = parameter;
                SqlParameter parameter2 = new SqlParameter("@last_time", SqlDbType.DateTime) {
                    Value = list2[1].ToString()
                };
                parameterArray2[1] = parameter2;
                SqlParameter parameter3 = new SqlParameter("@u_name", SqlDbType.NVarChar) {
                    Value = pair.Key
                };
                parameterArray2[2] = parameter3;
                SqlParameter[] parameterArray = parameterArray2;
                CommandInfo item = new CommandInfo {
                    CommandText = str2,
                    Parameters = parameterArray
                };
                list.Add(item);
                dictionary.TryRemove(pair.Key, out obj2);
            }
            if (list.Count > 0)
            {
                CallBLL.cz_stat_online_bll.executte_sql(list);
            }
        }

        public static void update_online_user(string u_name)
        {
            string str = string.Format("update  cz_stat_online set  last_time=last_time-0.1 where u_name=@u_name ", new object[0]);
            SqlParameter[] parameterArray = new SqlParameter[] { new SqlParameter("@u_name", SqlDbType.NVarChar) };
            parameterArray[0].Value = u_name;
            CallBLL.cz_stat_online_bll.executte_sql(str, parameterArray);
        }

        public void user_add_agent_log(cz_users model, int lottery_type)
        {
            string str = this.get_master_name();
            string str2 = this.get_children_name();
            string str3 = "";
            string str4 = "";
            string note = "";
            string str6 = model.get_u_name();
            int num = 300;
            int? nullable = model.get_allow_sale();
            int? nullable2 = model.get_kc_allow_sale();
            int? nullable3 = model.get_six_allow_maxrate();
            int? nullable4 = model.get_kc_allow_maxrate();
            int? nullable5 = model.get_six_low_maxrate();
            int? nullable6 = model.get_kc_low_maxrate();
            string str7 = (nullable == 0) ? "禁用" : "啟用";
            string str8 = (nullable2 == 0) ? "禁用" : "啟用";
            string str9 = "";
            string str10 = "";
            if (nullable3 == 0)
            {
                str9 = "占餘成數下綫任占";
            }
            else
            {
                str9 = "限製下綫可占成數:" + nullable5.ToString();
            }
            if (nullable4 == 0)
            {
                str10 = "占餘成數下綫任占";
            }
            else
            {
                str10 = "限製下綫可占成數:" + nullable6.ToString();
            }
            str4 = (("上級名稱:" + model.get_sup_name()) + "<br />帳號:" + model.get_u_name()) + "<br />狀態:" + this.get_user_status(model.get_a_state().ToString().Trim());
            if (model.get_u_type().Equals("dl"))
            {
                if (model.get_limit_hy().Equals(0))
                {
                    str4 = str4 + "<br />代理限製下綫:【不限製】";
                }
                else
                {
                    object obj2 = str4;
                    str4 = string.Concat(new object[] { obj2, "<br />代理限製下綫:【限製", model.get_limit_hy(), "人】" });
                }
            }
            if (lottery_type == 2)
            {
                if (model.get_six_iscash().Equals(int.Parse("1")))
                {
                    str4 = str4 + "<br />⑥合彩現金额度:" + model.get_six_credit();
                }
                else
                {
                    str4 = str4 + "<br />⑥合彩信用额度:" + model.get_six_credit();
                }
                str4 = (((str4 + "<br />⑥合彩上級占成:" + model.get_six_rate()) + "<br />⑥合彩盤口:" + this.get_pk(model.get_six_kind())) + "<br />(⑥合彩)補貨功能:" + str7) + "<br />(⑥合彩)下綫占成上限:" + str9;
                if (model.get_kc_iscash().Equals(int.Parse("1")))
                {
                    str4 = str4 + "<br />快彩現金额度:" + model.get_kc_credit();
                }
                else
                {
                    str4 = str4 + "<br />快彩信用额度:" + model.get_kc_credit();
                }
                str4 = (((str4 + "<br />快彩上級占成:" + model.get_kc_rate()) + "<br />快彩盤口:" + this.get_pk(model.get_kc_kind())) + "<br />快彩補貨功能:" + str8) + "<br />快彩下綫占成上限:" + str10;
            }
            else if (lottery_type == 0)
            {
                num = 100;
                if (model.get_six_iscash().Equals(int.Parse("1")))
                {
                    str4 = str4 + "<br />⑥合彩現金额度:" + model.get_six_credit();
                }
                else
                {
                    str4 = str4 + "<br />⑥合彩信用额度:" + model.get_six_credit();
                }
                str4 = (((str4 + "<br />⑥合彩上級占成:" + model.get_six_rate()) + "<br />⑥合彩盤口:" + this.get_pk(model.get_six_kind())) + "<br />(⑥合彩)補貨功能:" + str7) + "<br />(⑥合彩)下綫占成上限:" + str9;
            }
            else if (lottery_type == 1)
            {
                num = 200;
                if (model.get_kc_iscash().Equals(int.Parse("1")))
                {
                    str4 = str4 + "<br />快彩現金额度:" + model.get_kc_credit();
                }
                else
                {
                    str4 = str4 + "<br />快彩信用额度:" + model.get_kc_credit();
                }
                str4 = (((str4 + "<br />快彩上級占成:" + model.get_kc_rate()) + "<br />快彩盤口:" + this.get_pk(model.get_kc_kind())) + "<br />快彩補貨功能:" + str8) + "<br />快彩下綫占成上限:" + str10;
            }
            if (str4 != "")
            {
                note = "新增" + this.get_zhsj(model.get_u_type());
                List<SqlParameter> paramList = new List<SqlParameter>();
                string str11 = this.add_user_change_log(str6, num, str, str2, note, str3, str4, ref paramList);
                CallBLL.cz_user_change_log_bll.executte_sql(str11, paramList.ToArray());
            }
        }

        public void user_add_children_log(cz_users_child model)
        {
            string str = this.get_master_name();
            string str2 = this.get_children_name();
            string str3 = "";
            string str4 = "";
            string note = "";
            string str6 = model.get_u_name();
            int num = 300;
            string str7 = Utils.StrToQuotStr(model.get_permissions_name());
            DataTable permissions = CallBLL.cz_permissions_bll.GetPermissions(str7);
            string str8 = "";
            if (permissions != null)
            {
                ArrayList list = new ArrayList();
                foreach (DataRow row in permissions.Rows)
                {
                    list.Add(row["name_remark"].ToString());
                }
                str8 = string.Join(",", list.ToArray());
            }
            str4 = ("子帳號:" + model.get_u_name()) + " 子帳號名稱:" + model.get_u_nicker();
            if (str8 != "")
            {
                str4 = str4 + " 子帳號權限:" + str8;
            }
            if (str4 != "")
            {
                note = "新增子帳號";
                List<SqlParameter> paramList = new List<SqlParameter>();
                string str9 = this.add_user_change_log(str6, num, str, str2, note, str3, str4, ref paramList);
                CallBLL.cz_user_change_log_bll.executte_sql(str9, paramList.ToArray());
            }
        }

        public void user_add_fgs_log(cz_users model, int lottery_type)
        {
            string str = this.get_master_name();
            string str2 = this.get_children_name();
            string str3 = "";
            string str4 = "";
            string note = "";
            string str6 = model.get_u_name();
            int num = 300;
            int? nullable = model.get_allow_sale();
            int? nullable2 = model.get_kc_allow_sale();
            model.get_six_allow_maxrate();
            model.get_kc_allow_maxrate();
            model.get_six_low_maxrate();
            model.get_kc_low_maxrate();
            string str7 = (nullable == 0) ? "禁用" : "啟用";
            string str8 = (nullable2 == 0) ? "禁用" : "啟用";
            str4 = (("帳號:" + model.get_u_name()) + "<br />狀態:" + this.get_user_status(model.get_a_state().ToString().Trim())) + "<br />開放公司報表:" + ((model.get_allow_view_report() == 0) ? "禁看" : "顯示");
            if (lottery_type == 2)
            {
                if (model.get_six_iscash().Equals(int.Parse("1")))
                {
                    str4 = str4 + "<br />⑥合彩現金额度:" + model.get_six_credit();
                }
                else
                {
                    str4 = str4 + "<br />⑥合彩信用额度:" + model.get_six_credit();
                }
                str4 = (((((str4 + "<br />⑥合彩上級占成:" + model.get_six_rate()) + "<br />⑥合彩盤口:" + this.get_pk(model.get_six_kind())) + "<br />⑥合彩補貨功能:" + str7) + "<br />⑥合彩占餘成數歸:" + ((model.get_six_rate_owner() == 1) ? "總監" : "分公司")) + "<br />⑥合彩操盤:" + ((model.get_six_op_odds() == 0) ? "禁止" : "允許")) + "<br />⑥合彩信用/現金:" + ((model.get_six_iscash() == 0) ? "信用" : "現金");
                if (model.get_kc_iscash().Equals(int.Parse("1")))
                {
                    str4 = str4 + "<br />快彩現金额度:" + model.get_kc_credit();
                }
                else
                {
                    str4 = str4 + "<br />快彩信用额度:" + model.get_kc_credit();
                }
                str4 = (((((str4 + "<br />快彩上級占成:" + model.get_kc_rate()) + "<br />快彩盤口:" + this.get_pk(model.get_kc_kind())) + "<br />快彩補貨功能:" + str8) + "<br />快彩占餘成數歸:" + ((model.get_kc_rate_owner() == 1) ? "總監" : "分公司")) + "<br />快彩信用/現金:" + ((model.get_kc_iscash() == 0) ? "信用" : "現金")) + "<br />快彩現金虧損自動回收:" + ((model.get_kc_isauto_back() == 0) ? "自動回收" : "不自動回收");
            }
            else if (lottery_type == 0)
            {
                num = 100;
                if (model.get_six_iscash().Equals(int.Parse("1")))
                {
                    str4 = str4 + "<br />⑥合彩現金额度:" + model.get_six_credit();
                }
                else
                {
                    str4 = str4 + "<br />⑥合彩信用额度:" + model.get_six_credit();
                }
                str4 = (((((str4 + "<br />⑥合彩上級占成:" + model.get_six_rate()) + "<br />⑥合彩盤口:" + this.get_pk(model.get_six_kind())) + "<br />⑥合彩補貨功能:" + str7) + "<br />⑥合彩占餘成數歸:" + ((model.get_six_rate_owner() == 1) ? "總監" : "分公司")) + "<br />⑥合彩操盤:" + ((model.get_six_op_odds() == 0) ? "禁止" : "允許")) + "<br />⑥合彩信用/現金:" + ((model.get_six_iscash() == 0) ? "信用" : "現金");
            }
            else if (lottery_type == 1)
            {
                num = 200;
                if (model.get_kc_iscash().Equals(int.Parse("1")))
                {
                    str4 = str4 + "<br />快彩現金额度:" + model.get_kc_credit();
                }
                else
                {
                    str4 = str4 + "<br />快彩信用额度:" + model.get_kc_credit();
                }
                str4 = (((((str4 + "<br />快彩上級占成:" + model.get_kc_rate()) + "<br />快彩盤口:" + this.get_pk(model.get_kc_kind())) + "<br />快彩補貨功能:" + str8) + "<br />快彩占餘成數歸:" + ((model.get_kc_rate_owner() == 1) ? "總監" : "分公司")) + "<br />快彩信用/現金:" + ((model.get_kc_iscash() == 0) ? "信用" : "現金")) + "<br />快彩現金虧損自動回收:" + ((model.get_kc_isauto_back() == 0) ? "自動回收" : "不自動回收");
            }
            if (str4 != "")
            {
                note = "新增分公司";
                List<SqlParameter> paramList = new List<SqlParameter>();
                string str9 = this.add_user_change_log(str6, num, str, str2, note, str3, str4, ref paramList);
                CallBLL.cz_user_change_log_bll.executte_sql(str9, paramList.ToArray());
            }
        }

        public void user_add_fill_log(cz_saleset_six model)
        {
            string str = this.get_master_name();
            string str2 = this.get_children_name();
            string str3 = "";
            string str4 = "";
            string note = "";
            string str6 = model.get_u_name();
            int num = 100;
            str4 = (("帳號:" + model.get_u_name()) + " 帳號名稱:" + model.get_u_nicker()) + " 盤口:" + model.get_six_kind();
            if (str4 != "")
            {
                note = "新增出貨會員";
                List<SqlParameter> paramList = new List<SqlParameter>();
                string str7 = this.add_user_change_log(str6, num, str, str2, note, str3, str4, ref paramList);
                CallBLL.cz_user_change_log_bll.executte_sql(str7, paramList.ToArray());
            }
        }

        public void user_add_hy_log(cz_users model, bool isZSHY, int lottery_type, string sltDrawback_six, string sltDrawback_kc)
        {
            string str = this.get_master_name();
            string str2 = this.get_children_name();
            string str3 = "";
            string str4 = "";
            string note = "";
            string str6 = model.get_u_name();
            int num = 300;
            str4 = "上級名稱:" + model.get_sup_name();
            if (isZSHY)
            {
                str4 = str4 + "<br />直屬上級:" + this.get_zhsj(model.get_su_type().Trim());
            }
            str4 = (str4 + "<br />帳號:" + model.get_u_name()) + "<br />狀態:" + this.get_user_status(model.get_a_state().ToString().Trim());
            if (lottery_type == 0)
            {
                num = 100;
                if (model.get_six_iscash().Equals(int.Parse("1")))
                {
                    str4 = str4 + "<br />⑥合彩現金额度:" + model.get_six_credit();
                }
                else
                {
                    str4 = str4 + "<br />⑥合彩信用额度:" + model.get_six_credit();
                }
                str4 = ((str4 + "<br />⑥合彩代理占成:" + model.get_six_rate()) + "<br />⑥合彩盤口:" + model.get_six_kind()) + "<br />⑥合彩退水設定:" + this.get_drawback(sltDrawback_six.ToUpper());
            }
            else if (lottery_type == 1)
            {
                num = 200;
                if (model.get_kc_iscash().Equals(int.Parse("1")))
                {
                    str4 = str4 + "<br />快彩現金额度:" + model.get_kc_credit();
                }
                else
                {
                    str4 = str4 + "<br />快彩信用额度:" + model.get_kc_credit();
                }
                str4 = ((str4 + "<br />快彩代理占成:" + model.get_kc_rate()) + "<br />快彩盤口:" + model.get_kc_kind()) + "<br />快彩退水設定:" + this.get_drawback(sltDrawback_kc.ToUpper());
            }
            else
            {
                if (model.get_six_iscash().Equals(int.Parse("1")))
                {
                    str4 = str4 + "<br />⑥合彩現金额度:" + model.get_six_credit();
                }
                else
                {
                    str4 = str4 + "<br />⑥合彩信用额度:" + model.get_six_credit();
                }
                str4 = ((str4 + "<br />⑥合彩代理占成:" + model.get_six_rate()) + "<br />⑥合彩盤口:" + model.get_six_kind()) + "<br />⑥合彩退水設定:" + this.get_drawback(sltDrawback_six.ToUpper());
                if (model.get_kc_iscash().Equals(int.Parse("1")))
                {
                    str4 = str4 + "<br />快彩現金额度:" + model.get_kc_credit();
                }
                else
                {
                    str4 = str4 + "<br />快彩信用额度:" + model.get_kc_credit();
                }
                str4 = ((str4 + "<br />快彩代理占成:" + model.get_kc_rate()) + "<br />快彩盤口:" + model.get_kc_kind()) + "<br />快彩退水設定:" + this.get_drawback(sltDrawback_kc.ToUpper());
            }
            if (str4 != "")
            {
                note = "新增普通用户";
                if (isZSHY)
                {
                    note = "新增直屬會員";
                }
                List<SqlParameter> paramList = new List<SqlParameter>();
                string str7 = this.add_user_change_log(str6, num, str, str2, note, str3, str4, ref paramList);
                CallBLL.cz_user_change_log_bll.executte_sql(str7, paramList.ToArray());
            }
        }

        public void user_change_status_log(string uid, string old_status, bool is_children)
        {
            string str = this.get_master_name();
            string str2 = this.get_children_name();
            string str3 = "";
            string str4 = "";
            string note = "";
            string str6 = "";
            string str7 = "";
            string str8 = "";
            str3 = this.get_user_status(old_status);
            if (!is_children)
            {
                cz_users userInfoByUID = CallBLL.cz_users_bll.GetUserInfoByUID(uid);
                if (userInfoByUID == null)
                {
                    return;
                }
                str6 = userInfoByUID.get_u_name();
                str8 = userInfoByUID.get_u_type();
                str7 = userInfoByUID.get_a_state().ToString();
                str4 = this.get_user_status(userInfoByUID.get_a_state().ToString());
            }
            else
            {
                cz_users_child userByUID = CallBLL.cz_users_child_bll.GetUserByUID(uid);
                if (userByUID == null)
                {
                    return;
                }
                str6 = userByUID.get_u_name();
                str7 = userByUID.get_status().ToString();
                str4 = this.get_user_status(userByUID.get_status().ToString());
            }
            int num = 300;
            if ((str4 != "") && (str4 != str3))
            {
                switch (str7)
                {
                    case "0":
                    case "1":
                    case "2":
                        if (!is_children)
                        {
                            if (str8 != "hy")
                            {
                                if (FileCacheHelper.get_RedisStatOnline().Equals(1))
                                {
                                    base.UpdateIsOutOpts(str6);
                                }
                                else if (FileCacheHelper.get_RedisStatOnline().Equals(2))
                                {
                                    base.UpdateIsOutOptsStack(str6);
                                }
                                else
                                {
                                    CallBLL.cz_stat_online_bll.UpdateIsOutList(str6);
                                }
                            }
                            if (str8 == "hy")
                            {
                                if (FileCacheHelper.get_RedisStatOnline().Equals(1))
                                {
                                    base.UpdateIsOutOpts(str6);
                                }
                                else if (FileCacheHelper.get_RedisStatOnline().Equals(2))
                                {
                                    base.UpdateIsOutOptsStack(str6);
                                }
                                else
                                {
                                    CallBLL.cz_stat_online_bll.UpdateIsOut(str6);
                                }
                            }
                        }
                        else if (FileCacheHelper.get_RedisStatOnline().Equals(1))
                        {
                            CallBLL.cz_users_bll.GetUserInfoByUName(str);
                            base.UpdateIsOutOpts(str6);
                        }
                        else if (FileCacheHelper.get_RedisStatOnline().Equals(2))
                        {
                            CallBLL.cz_users_bll.GetUserInfoByUName(str);
                            base.UpdateIsOutOptsStack(str6);
                        }
                        else
                        {
                            CallBLL.cz_stat_online_bll.UpdateIsOut(str6);
                        }
                        if (FileCacheHelper.get_RedisStatOnline().Equals(0))
                        {
                            FileCacheHelper.UpdateUserOutFile();
                        }
                        break;
                }
                note = "修改狀態";
                List<SqlParameter> paramList = new List<SqlParameter>();
                string str9 = this.add_user_change_log(str6, num, str, str2, note, str3, str4, ref paramList);
                CallBLL.cz_user_change_log_bll.executte_sql(str9, paramList.ToArray());
            }
        }

        public void user_del_fill_log(string uid)
        {
            cz_saleset_six model = CallBLL.cz_saleset_six_bll.GetModel(uid.ToUpper());
            if (model != null)
            {
                string str = this.get_master_name();
                string str2 = this.get_children_name();
                string str3 = "";
                string str4 = "";
                string note = "";
                string str6 = model.get_u_name();
                int num = 100;
                str4 = (("帳號:" + model.get_u_name()) + " 帳號名稱:" + model.get_u_nicker()) + " 盤口:" + model.get_six_kind();
                if (str4 != "")
                {
                    note = "刪除出貨會員";
                    List<SqlParameter> paramList = new List<SqlParameter>();
                    string str7 = this.add_user_change_log(str6, num, str, str2, note, str3, str4, ref paramList);
                    CallBLL.cz_user_change_log_bll.executte_sql(str7, paramList.ToArray());
                }
            }
        }

        public void user_drawback_kc_log(DataTable oldDT, DataTable newDT, string changed_user, int lottery_id)
        {
            string str = this.get_master_name();
            string str2 = this.get_children_name();
            string str3 = "";
            string str4 = "";
            string note = "";
            string str6 = "";
            string gameNameByID = base.GetGameNameByID(lottery_id.ToString());
            int count = oldDT.Rows.Count;
            IEqualityComparer<DataRow> comparer = DataRowComparer.Default;
            for (int i = 0; i < count; i++)
            {
                str3 = "";
                str4 = "";
                DataRow x = oldDT.Rows[i];
                DataRow y = newDT.Rows[i];
                if (comparer.Equals(x, y))
                {
                    continue;
                }
                string str8 = x["play_id"].ToString();
                string str9 = x["play_name"].ToString();
                string str10 = x["a_drawback"].ToString();
                string str11 = x["b_drawback"].ToString();
                string str12 = x["c_drawback"].ToString();
                string s = x["single_phase_amount"].ToString();
                string str14 = x["single_max_amount"].ToString();
                string str15 = x["single_min_amount"].ToString().Trim();
                y["play_name"].ToString();
                string str16 = y["a_drawback"].ToString();
                string str17 = y["b_drawback"].ToString();
                string str18 = y["c_drawback"].ToString();
                string str19 = y["single_phase_amount"].ToString();
                string str20 = y["single_max_amount"].ToString();
                string str21 = y["single_min_amount"].ToString().Trim();
                if (!str10.Equals(str16))
                {
                    str3 = " A盤退水值:" + str10;
                    str4 = " A盤退水值:" + str16;
                }
                if (!str11.Equals(str17))
                {
                    str3 = str3 + " B盤退水值:" + str11;
                    str4 = str4 + " B盤退水值:" + str17;
                }
                if (!str12.Equals(str18))
                {
                    str3 = str3 + " C盤退水值:" + str12;
                    str4 = str4 + " C盤退水值:" + str18;
                }
                if (!s.Equals(str19))
                {
                    str3 = str3 + " 單期限額:" + double.Parse(s).ToString("F0");
                    str4 = str4 + " 單期限額:" + double.Parse(str19).ToString("F0");
                }
                if (!str14.Equals(str20))
                {
                    str3 = str3 + " 單注限額:" + double.Parse(str14).ToString("F0");
                    str4 = str4 + " 單注限額:" + double.Parse(str20).ToString("F0");
                }
                if (!str15.Equals(str21))
                {
                    str3 = str3 + " 最小單注:" + double.Parse(str15).ToString("F0");
                    str4 = str4 + " 最小單注:" + double.Parse(str21).ToString("F0");
                }
                if (!(str3 != ""))
                {
                    continue;
                }
                str6 = str9;
                if (lottery_id.Equals(0))
                {
                    switch (str8)
                    {
                        case "81":
                            str6 = "1-8球";
                            break;

                        case "82":
                            str6 = "1-8球大小";
                            break;

                        case "83":
                            str6 = "1-8球單雙";
                            break;

                        case "84":
                            str6 = "1-8球尾數大小";
                            break;

                        case "85":
                            str6 = "1-8球合數單雙";
                            break;

                        case "121":
                            str6 = "1-8球方位";
                            break;

                        case "122":
                            str6 = "1-8球中發白";
                            break;
                    }
                }
                else
                {
                    if (!lottery_id.Equals(1))
                    {
                        goto Label_049A;
                    }
                    string str24 = str8;
                    if (str24 != null)
                    {
                        if (!(str24 == "1"))
                        {
                            if (str24 == "2")
                            {
                                goto Label_0482;
                            }
                            if (str24 == "3")
                            {
                                goto Label_048E;
                            }
                        }
                        else
                        {
                            str6 = "1-5球";
                        }
                    }
                }
                goto Label_0AC1;
            Label_0482:
                str6 = "1-5球大小";
                goto Label_0AC1;
            Label_048E:
                str6 = "1-5球單雙";
                goto Label_0AC1;
            Label_049A:
                if (!lottery_id.Equals(2))
                {
                    goto Label_051B;
                }
                string str25 = str8;
                if (str25 != null)
                {
                    if (!(str25 == "1"))
                    {
                        if (str25 == "2")
                        {
                            goto Label_04F7;
                        }
                        if (str25 == "3")
                        {
                            goto Label_0503;
                        }
                        if (str25 == "4")
                        {
                            goto Label_050F;
                        }
                    }
                    else
                    {
                        str6 = "1-10名";
                    }
                }
                goto Label_0AC1;
            Label_04F7:
                str6 = "1-10名大小";
                goto Label_0AC1;
            Label_0503:
                str6 = "1-10名單雙";
                goto Label_0AC1;
            Label_050F:
                str6 = "1-5名龍虎";
                goto Label_0AC1;
            Label_051B:
                if (lottery_id.Equals(3))
                {
                    switch (str8)
                    {
                        case "81":
                            str6 = "1-8球";
                            break;

                        case "82":
                            str6 = "1-8球大小";
                            break;

                        case "83":
                            str6 = "1-8球單雙";
                            break;

                        case "84":
                            str6 = "1-8球尾數大小";
                            break;

                        case "85":
                            str6 = "1-8球合數單雙";
                            break;

                        case "121":
                            str6 = "1-8球梅蘭竹菊";
                            break;

                        case "122":
                            str6 = "1-8球中發白";
                            break;
                    }
                }
                else if (lottery_id.Equals(4))
                {
                    str6 = str9;
                }
                else if (lottery_id.Equals(5))
                {
                    str6 = str9;
                }
                else
                {
                    if (!lottery_id.Equals(6))
                    {
                        goto Label_06BA;
                    }
                    string str27 = str8;
                    if (str27 != null)
                    {
                        if (!(str27 == "1"))
                        {
                            if (str27 == "2")
                            {
                                goto Label_06A2;
                            }
                            if (str27 == "3")
                            {
                                goto Label_06AE;
                            }
                        }
                        else
                        {
                            str6 = "1-5球";
                        }
                    }
                }
                goto Label_0AC1;
            Label_06A2:
                str6 = "1-5球大小";
                goto Label_0AC1;
            Label_06AE:
                str6 = "1-5球單雙";
                goto Label_0AC1;
            Label_06BA:
                if (!lottery_id.Equals(7))
                {
                    goto Label_0707;
                }
                string str28 = str8;
                if (str28 != null)
                {
                    if (!(str28 == "71007"))
                    {
                        if (str28 == "71008")
                        {
                            goto Label_06FB;
                        }
                    }
                    else
                    {
                        str6 = "1-3區大小";
                    }
                }
                goto Label_0AC1;
            Label_06FB:
                str6 = "1-3區單雙";
                goto Label_0AC1;
            Label_0707:
                if (lottery_id.Equals(8))
                {
                    str6 = str9;
                }
                else
                {
                    if (((!lottery_id.Equals(9) && !lottery_id.Equals(10)) && (!lottery_id.Equals(0x10) && !lottery_id.Equals(0x12))) && ((!lottery_id.Equals(20) && !lottery_id.Equals(0x15)) && !lottery_id.Equals(0x16)))
                    {
                        goto Label_07DE;
                    }
                    string str29 = str8;
                    if (str29 != null)
                    {
                        if (!(str29 == "1"))
                        {
                            if (str29 == "2")
                            {
                                goto Label_07BA;
                            }
                            if (str29 == "3")
                            {
                                goto Label_07C6;
                            }
                            if (str29 == "4")
                            {
                                goto Label_07D2;
                            }
                        }
                        else
                        {
                            str6 = "1-10名";
                        }
                    }
                }
                goto Label_0AC1;
            Label_07BA:
                str6 = "1-10名大小";
                goto Label_0AC1;
            Label_07C6:
                str6 = "1-10名單雙";
                goto Label_0AC1;
            Label_07D2:
                str6 = "1-5名龍虎";
                goto Label_0AC1;
            Label_07DE:
                if ((!lottery_id.Equals(11) && !lottery_id.Equals(0x11)) && !lottery_id.Equals(0x13))
                {
                    goto Label_085C;
                }
                string str30 = str8;
                if (str30 != null)
                {
                    if (!(str30 == "1"))
                    {
                        if (str30 == "2")
                        {
                            goto Label_0844;
                        }
                        if (str30 == "3")
                        {
                            goto Label_0850;
                        }
                    }
                    else
                    {
                        str6 = "1-5球";
                    }
                }
                goto Label_0AC1;
            Label_0844:
                str6 = "1-5球大小";
                goto Label_0AC1;
            Label_0850:
                str6 = "1-5球單雙";
                goto Label_0AC1;
            Label_085C:
                if (!lottery_id.Equals(13))
                {
                    goto Label_08C4;
                }
                string str31 = str8;
                if (str31 != null)
                {
                    if (!(str31 == "1"))
                    {
                        if (str31 == "2")
                        {
                            goto Label_08AC;
                        }
                        if (str31 == "3")
                        {
                            goto Label_08B8;
                        }
                    }
                    else
                    {
                        str6 = "1-5球";
                    }
                }
                goto Label_0AC1;
            Label_08AC:
                str6 = "1-5球大小";
                goto Label_0AC1;
            Label_08B8:
                str6 = "1-5球單雙";
                goto Label_0AC1;
            Label_08C4:
                if (!lottery_id.Equals(12))
                {
                    goto Label_0946;
                }
                string str32 = str8;
                if (str32 != null)
                {
                    if (!(str32 == "1"))
                    {
                        if (str32 == "2")
                        {
                            goto Label_0922;
                        }
                        if (str32 == "3")
                        {
                            goto Label_092E;
                        }
                        if (str32 == "4")
                        {
                            goto Label_093A;
                        }
                    }
                    else
                    {
                        str6 = "1-10名";
                    }
                }
                goto Label_0AC1;
            Label_0922:
                str6 = "1-10名大小";
                goto Label_0AC1;
            Label_092E:
                str6 = "1-10名單雙";
                goto Label_0AC1;
            Label_093A:
                str6 = "1-5名龍虎";
                goto Label_0AC1;
            Label_0946:
                if (lottery_id.Equals(14))
                {
                    switch (str8)
                    {
                        case "81":
                            str6 = "1-8球";
                            break;

                        case "82":
                            str6 = "1-8球大小";
                            break;

                        case "83":
                            str6 = "1-8球單雙";
                            break;

                        case "84":
                            str6 = "1-8球尾數大小";
                            break;

                        case "85":
                            str6 = "1-8球合數單雙";
                            break;

                        case "121":
                            str6 = "1-8球方位";
                            break;

                        case "122":
                            str6 = "1-8球中發白";
                            break;
                    }
                }
                else
                {
                    string str34;
                    if (lottery_id.Equals(15) && ((str34 = str8) != null))
                    {
                        if (!(str34 == "1"))
                        {
                            if (str34 == "2")
                            {
                                goto Label_0AA8;
                            }
                            if (str34 == "3")
                            {
                                goto Label_0AB1;
                            }
                            if (str34 == "4")
                            {
                                goto Label_0ABA;
                            }
                        }
                        else
                        {
                            str6 = "1-10名";
                        }
                    }
                }
                goto Label_0AC1;
            Label_0AA8:
                str6 = "1-10名大小";
                goto Label_0AC1;
            Label_0AB1:
                str6 = "1-10名單雙";
                goto Label_0AC1;
            Label_0ABA:
                str6 = "1-5名龍虎";
            Label_0AC1:
                if (str6 == "")
                {
                    str6 = str9;
                }
                str3 = gameNameByID + "," + str6 + str3;
                str4 = gameNameByID + "," + str6 + str4;
                List<SqlParameter> paramList = new List<SqlParameter>();
                string str22 = this.add_user_change_log(changed_user, 200, str, str2, note, str3, str4, ref paramList);
                CallBLL.cz_user_change_log_bll.executte_sql(str22, paramList.ToArray());
            }
        }

        public void user_drawback_six_log(DataTable oldDT, DataTable newDT, string changed_user)
        {
            string str = this.get_master_name();
            string str2 = this.get_children_name();
            string str3 = "";
            string str4 = "";
            string note = "";
            string str6 = "";
            int num = 100;
            string gameNameByID = base.GetGameNameByID(100.ToString());
            int count = oldDT.Rows.Count;
            IEqualityComparer<DataRow> comparer = DataRowComparer.Default;
            for (int i = 0; i < count; i++)
            {
                str3 = "";
                str4 = "";
                DataRow x = oldDT.Rows[i];
                DataRow y = newDT.Rows[i];
                if (!comparer.Equals(x, y))
                {
                    string str8 = x["play_name"].ToString().Trim();
                    string str9 = x["a_drawback"].ToString().Trim();
                    string str10 = x["b_drawback"].ToString().Trim();
                    string str11 = x["c_drawback"].ToString().Trim();
                    string s = x["single_phase_amount"].ToString().Trim();
                    string str13 = x["single_max_amount"].ToString().Trim();
                    string str14 = x["single_min_amount"].ToString().Trim();
                    y["play_name"].ToString().Trim();
                    string str15 = y["a_drawback"].ToString().Trim();
                    string str16 = y["b_drawback"].ToString().Trim();
                    string str17 = y["c_drawback"].ToString().Trim();
                    string str18 = y["single_phase_amount"].ToString().Trim();
                    string str19 = y["single_max_amount"].ToString().Trim();
                    string str20 = y["single_min_amount"].ToString().Trim();
                    if (!str9.Equals(str15))
                    {
                        str3 = " A盤退水值:" + str9;
                        str4 = " A盤退水值:" + str15;
                    }
                    if (!str10.Equals(str16))
                    {
                        str3 = str3 + " B盤退水值:" + str10;
                        str4 = str4 + " B盤退水值:" + str16;
                    }
                    if (!str11.Equals(str17))
                    {
                        str3 = str3 + " C盤退水值:" + str11;
                        str4 = str4 + " C盤退水值:" + str17;
                    }
                    if (!s.Equals(str18))
                    {
                        str3 = str3 + " 單期限額:" + double.Parse(s).ToString("F0");
                        str4 = str4 + " 單期限額:" + double.Parse(str18).ToString("F0");
                    }
                    if (!str13.Equals(str19))
                    {
                        str3 = str3 + " 單注限額:" + double.Parse(str13).ToString("F0");
                        str4 = str4 + " 單注限額:" + double.Parse(str19).ToString("F0");
                    }
                    if (!str14.Equals(str20))
                    {
                        str3 = str3 + " 最小單注:" + double.Parse(str14).ToString("F0");
                        str4 = str4 + " 最小單注:" + double.Parse(str20).ToString("F0");
                    }
                    if (str3 != "")
                    {
                        str6 = str8;
                        str3 = gameNameByID + "," + str6 + str3;
                        str4 = gameNameByID + "," + str6 + str4;
                        List<SqlParameter> paramList = new List<SqlParameter>();
                        string str21 = this.add_user_change_log(changed_user, num, str, str2, note, str3, str4, ref paramList);
                        CallBLL.cz_user_change_log_bll.executte_sql(str21, paramList.ToArray());
                    }
                }
            }
        }

        public void user_edit_agent_log(DataTable oldDT, DataTable newDT, string changed_user, string u_type)
        {
            string str = this.get_master_name();
            string str2 = this.get_children_name();
            string str3 = "";
            string str4 = "";
            string note = "";
            bool flag = false;
            int num = 0;
            int num2 = 300;
            int count = oldDT.Rows.Count;
            IEqualityComparer<DataRow> comparer = DataRowComparer.Default;
            for (int i = 0; i < count; i++)
            {
                str3 = "";
                str4 = "";
                DataRow x = oldDT.Rows[i];
                DataRow y = newDT.Rows[i];
                if (!comparer.Equals(x, y))
                {
                    string str6 = x["u_nicker"].ToString().Trim();
                    string str7 = x["u_psw"].ToString().Trim();
                    string str8 = x["a_state"].ToString().Trim();
                    string str9 = x["six_rate"].ToString().Trim();
                    string s = x["six_credit"].ToString().Trim();
                    string str11 = x["six_usable_credit"].ToString().Trim();
                    string str12 = x["six_kind"].ToString().Trim();
                    string str13 = x["kc_rate"].ToString().Trim();
                    string str14 = x["kc_credit"].ToString().Trim();
                    string str15 = x["kc_usable_credit"].ToString().Trim();
                    string str16 = x["allow_sale"].ToString().Trim();
                    string str17 = x["kc_allow_sale"].ToString().Trim();
                    string str18 = x["six_allow_maxrate"].ToString().Trim();
                    string str19 = x["kc_allow_maxrate"].ToString().Trim();
                    string str20 = x["six_low_maxrate"].ToString().Trim();
                    string str21 = x["kc_low_maxrate"].ToString().Trim();
                    string str22 = x["retry_times"].ToString().Trim();
                    string str23 = x["limit_hy"].ToString().Trim();
                    string str24 = y["u_nicker"].ToString().Trim();
                    string str25 = y["u_psw"].ToString().Trim();
                    string str26 = y["a_state"].ToString().Trim();
                    string str27 = y["six_rate"].ToString().Trim();
                    string str28 = y["six_credit"].ToString().Trim();
                    string str29 = y["six_usable_credit"].ToString().Trim();
                    string str30 = y["six_kind"].ToString().Trim();
                    string str31 = y["kc_rate"].ToString().Trim();
                    string str32 = y["kc_credit"].ToString().Trim();
                    string str33 = y["kc_usable_credit"].ToString().Trim();
                    string str34 = y["allow_sale"].ToString().Trim();
                    string str35 = y["kc_allow_sale"].ToString().Trim();
                    string str36 = y["six_allow_maxrate"].ToString().Trim();
                    string str37 = y["kc_allow_maxrate"].ToString().Trim();
                    string str38 = y["six_low_maxrate"].ToString().Trim();
                    string str39 = y["kc_low_maxrate"].ToString().Trim();
                    string str40 = y["retry_times"].ToString().Trim();
                    string str41 = y["limit_hy"].ToString().Trim();
                    if (!str6.Equals(str24))
                    {
                        str3 = "名稱:" + str6 + "<br />";
                        str4 = "名稱:" + str24 + "<br />";
                    }
                    if (!str7.Equals(str25))
                    {
                        str3 = str3 + "密碼有變動<br />";
                        str4 = str4 + "密碼有變動<br />";
                        flag = true;
                        num |= 1;
                        CallBLL.cz_users_bll.ZeroIsChanged(changed_user);
                    }
                    if (!str8.Equals(str26))
                    {
                        str3 = str3 + "狀態:" + this.get_user_status(str8) + "<br />";
                        str4 = str4 + "狀態:" + this.get_user_status(str26) + "<br />";
                        flag = true;
                        num |= 2;
                    }
                    if (!str9.Equals(str27))
                    {
                        str3 = str3 + "⑥合彩占成:" + str9 + "<br />";
                        str4 = str4 + "⑥合彩占成:" + str27 + "<br />";
                    }
                    if (!s.Equals(str28))
                    {
                        str3 = str3 + "⑥合彩信用额度:" + double.Parse(s).ToString("F0") + "<br />";
                        str4 = str4 + "⑥合彩信用额度:" + double.Parse(str28).ToString("F0") + "<br />";
                    }
                    if (!str11.Equals(str29))
                    {
                        str3 = str3 + "⑥合彩可用额度:" + double.Parse(str11).ToString("F1") + "<br />";
                        str4 = str4 + "⑥合彩可用额度:" + double.Parse(str29).ToString("F1") + "<br />";
                    }
                    if (!str12.Equals(str30))
                    {
                        str3 = str3 + "⑥合彩盘口:" + str12 + "<br />";
                        str4 = str4 + "⑥合彩盘口:" + str30 + "<br />";
                    }
                    if (!str13.Equals(str31))
                    {
                        str3 = str3 + "快彩占成:" + str13 + "<br />";
                        str4 = str4 + "快彩占成:" + str31 + "<br />";
                    }
                    if (!str14.Equals(str32))
                    {
                        str3 = str3 + "快彩信用额度:" + double.Parse(str14).ToString("F0") + "<br />";
                        str4 = str4 + "快彩信用额度:" + double.Parse(str32).ToString("F0") + "<br />";
                    }
                    if (!str15.Equals(str33))
                    {
                        str3 = str3 + "快彩可用额度:" + double.Parse(str15).ToString("F1") + "<br />";
                        str4 = str4 + "快彩可用额度:" + double.Parse(str33).ToString("F1") + "<br />";
                    }
                    if (!str16.Equals(str34))
                    {
                        str3 = str3 + "⑥合彩補貨功能:" + ((str16 == "1") ? "啟用" : "禁用") + "<br />";
                        str4 = str4 + "⑥合彩補貨功能:" + ((str34 == "1") ? "啟用" : "禁用") + "<br />";
                    }
                    if (!str17.Equals(str35))
                    {
                        str3 = str3 + "快彩補貨功能:" + ((str17 == "1") ? "啟用" : "禁用") + "<br />";
                        str4 = str4 + "快彩補貨功能:" + ((str35 == "1") ? "啟用" : "禁用") + "<br />";
                    }
                    if (!str18.Equals(str36))
                    {
                        str3 = str3 + "⑥合彩下線占成上限功能:" + ((str18 == "1") ? "限製下綫可占成數" : "占餘成數下綫任占") + "<br />";
                        str4 = str4 + "⑥合彩下線占成上限功能:" + ((str36 == "1") ? "限製下綫可占成數" : "占餘成數下綫任占") + "<br />";
                    }
                    if (!str19.Equals(str37))
                    {
                        str3 = str3 + "快彩下線占成上限功能:" + ((str19 == "1") ? "限製下綫可占成數" : "占餘成數下綫任占") + "<br />";
                        str4 = str4 + "快彩下線占成上限功能:" + ((str37 == "1") ? "限製下綫可占成數" : "占餘成數下綫任占") + "<br />";
                    }
                    if ((!str20.Equals(str38) && (str18 == "1")) && (str36 == "1"))
                    {
                        str3 = str3 + "⑥合彩限製下綫可占成數:" + str20 + "<br />";
                        str4 = str4 + "⑥合彩限製下綫可占成數:" + str38 + "<br />";
                    }
                    if ((!str21.Equals(str39) && (str19 == "1")) && (str37 == "1"))
                    {
                        str3 = str3 + "快彩限製下綫可占成數:" + str21 + "<br />";
                        str4 = str4 + "快彩限製下綫可占成數:" + str39 + "<br />";
                    }
                    if ((str22 != str40) && (str40 == "0"))
                    {
                        str3 = str3 + "帳號被鎖<br />";
                        str4 = str4 + "帳號被解鎖<br />";
                    }
                    if (u_type.Equals("dl") && (str23 != str41))
                    {
                        if (str23.Equals("0"))
                        {
                            str3 = str3 + "代理限製下綫【不限製】<br />";
                            str4 = str4 + "代理限製下綫【限製" + str41 + "人】<br />";
                        }
                        else if (!str23.Equals("0") && !str41.Equals("0"))
                        {
                            str3 = str3 + "代理限製下綫【限製" + str23 + "人】<br />";
                            str4 = str4 + "代理限製下綫【限製" + str41 + "人】<br />";
                        }
                        else
                        {
                            str3 = str3 + "代理限製下綫【限製" + str23 + "人】<br />";
                            str4 = str4 + "代理限製下綫【不限製】<br />";
                        }
                    }
                    if (flag)
                    {
                        if ((num & 1) == 1)
                        {
                            if (FileCacheHelper.get_RedisStatOnline().Equals(1))
                            {
                                CallBLL.cz_users_bll.GetUserInfoByUName(changed_user);
                                base.UpdateIsOutOpts(changed_user);
                            }
                            else if (FileCacheHelper.get_RedisStatOnline().Equals(2))
                            {
                                CallBLL.cz_users_bll.GetUserInfoByUName(changed_user);
                                base.UpdateIsOutOptsStack(changed_user);
                            }
                            else
                            {
                                CallBLL.cz_stat_online_bll.UpdateIsOut(changed_user);
                            }
                        }
                        if ((num & 2) == 2)
                        {
                            if (FileCacheHelper.get_RedisStatOnline().Equals(1))
                            {
                                base.UpdateIsOutOpts(changed_user);
                            }
                            else if (FileCacheHelper.get_RedisStatOnline().Equals(2))
                            {
                                base.UpdateIsOutOptsStack(changed_user);
                            }
                            else
                            {
                                CallBLL.cz_stat_online_bll.UpdateIsOutList(changed_user);
                            }
                        }
                        if (FileCacheHelper.get_RedisStatOnline().Equals(0))
                        {
                            FileCacheHelper.UpdateUserOutFile();
                        }
                    }
                    if (str3 != "")
                    {
                        note = "修改" + this.get_zhsj(u_type);
                        List<SqlParameter> paramList = new List<SqlParameter>();
                        string str42 = this.add_user_change_log(changed_user, num2, str, str2, note, str3, str4, ref paramList);
                        CallBLL.cz_user_change_log_bll.executte_sql(str42, paramList.ToArray());
                    }
                }
            }
        }

        public void user_edit_children_log(cz_users_child old_model, cz_users_child new_model)
        {
            string str = this.get_master_name();
            string str2 = this.get_children_name();
            string str3 = "";
            string str4 = "";
            string note = "";
            string str6 = old_model.get_u_name();
            int num = 300;
            string str7 = old_model.get_permissions_name().Trim();
            string str8 = new_model.get_permissions_name().Trim();
            string str9 = old_model.get_u_psw();
            string str10 = new_model.get_u_psw();
            string str11 = old_model.get_retry_times().ToString();
            string str12 = new_model.get_retry_times().ToString();
            bool flag = false;
            if (str7 != str8)
            {
                DataTable permissions = CallBLL.cz_permissions_bll.GetPermissions(Utils.StrToQuotStr(str7));
                string str13 = "";
                if (permissions != null)
                {
                    ArrayList list = new ArrayList();
                    foreach (DataRow row in permissions.Rows)
                    {
                        list.Add(row["name_remark"].ToString());
                    }
                    str13 = string.Join(",", list.ToArray());
                }
                DataTable table2 = CallBLL.cz_permissions_bll.GetPermissions(Utils.StrToQuotStr(str8));
                string str14 = "";
                if (table2 != null)
                {
                    ArrayList list2 = new ArrayList();
                    foreach (DataRow row2 in table2.Rows)
                    {
                        list2.Add(row2["name_remark"].ToString());
                    }
                    str14 = string.Join(",", list2.ToArray());
                }
                if (str13 != "")
                {
                    str3 = "子帳號權限:" + str13;
                }
                if (str14 != "")
                {
                    str4 = "子帳號權限:" + str14;
                }
                note = " 修改子帳號";
                if (str13 != str14)
                {
                    flag = true;
                }
            }
            if ((str9 != str10) && !string.IsNullOrEmpty(str10))
            {
                note = note + ", 密碼有變動";
                CallBLL.cz_users_child_bll.ZeroIsChanged(str6);
                flag = true;
            }
            if ((str11 != str12) && (str12 == "0"))
            {
                note = note + ",帳號被解鎖";
            }
            if (note != "")
            {
                if (flag)
                {
                    if (FileCacheHelper.get_RedisStatOnline().Equals(1))
                    {
                        CallBLL.cz_users_bll.GetUserInfoByUName(old_model.get_parent_u_name());
                        base.UpdateIsOutOpt(str6);
                    }
                    else if (FileCacheHelper.get_RedisStatOnline().Equals(2))
                    {
                        CallBLL.cz_users_bll.GetUserInfoByUName(old_model.get_parent_u_name());
                        base.UpdateIsOutOptStack(str6);
                    }
                    else
                    {
                        CallBLL.cz_stat_online_bll.UpdateIsOut(str6);
                        FileCacheHelper.UpdateUserOutFile();
                    }
                }
                if (note.IndexOf(',') == 0)
                {
                    note = note.Substring(1);
                }
                List<SqlParameter> paramList = new List<SqlParameter>();
                string str15 = this.add_user_change_log(str6, num, str, str2, note, str3, str4, ref paramList);
                CallBLL.cz_user_change_log_bll.executte_sql(str15, paramList.ToArray());
            }
        }

        public void user_edit_fgs_log(DataTable oldDT, DataTable newDT, string changed_user)
        {
            string str = this.get_master_name();
            string str2 = this.get_children_name();
            string str3 = "";
            string str4 = "";
            string note = "";
            bool flag = false;
            int num = 0;
            int num2 = 300;
            int count = oldDT.Rows.Count;
            IEqualityComparer<DataRow> comparer = DataRowComparer.Default;
            for (int i = 0; i < count; i++)
            {
                str3 = "";
                str4 = "";
                DataRow x = oldDT.Rows[i];
                DataRow y = newDT.Rows[i];
                if (!comparer.Equals(x, y))
                {
                    string str6 = x["u_nicker"].ToString().Trim();
                    string str7 = x["u_psw"].ToString().Trim();
                    string str8 = x["a_state"].ToString().Trim();
                    string str9 = x["six_rate"].ToString().Trim();
                    string s = x["six_credit"].ToString().Trim();
                    string str11 = x["six_usable_credit"].ToString().Trim();
                    string str12 = x["six_kind"].ToString().Trim();
                    string str13 = x["kc_rate"].ToString().Trim();
                    string str14 = x["kc_credit"].ToString().Trim();
                    string str15 = x["kc_usable_credit"].ToString().Trim();
                    string str16 = x["allow_sale"].ToString().Trim();
                    string str17 = x["kc_allow_sale"].ToString().Trim();
                    string str18 = x["allow_view_report"].ToString().Trim();
                    string str19 = x["six_rate_owner"].ToString().Trim();
                    string str20 = x["kc_rate_owner"].ToString().Trim();
                    string str21 = x["retry_times"].ToString().Trim();
                    string str22 = x["six_op_odds"].ToString().Trim();
                    string str23 = x["kc_isAuto_back"].ToString().Trim();
                    string str24 = y["u_nicker"].ToString().Trim();
                    string str25 = y["u_psw"].ToString().Trim();
                    string str26 = y["a_state"].ToString().Trim();
                    string str27 = y["six_rate"].ToString().Trim();
                    string str28 = y["six_credit"].ToString().Trim();
                    string str29 = y["six_usable_credit"].ToString().Trim();
                    string str30 = y["six_kind"].ToString().Trim();
                    string str31 = y["kc_rate"].ToString().Trim();
                    string str32 = y["kc_credit"].ToString().Trim();
                    string str33 = y["kc_usable_credit"].ToString().Trim();
                    string str34 = y["allow_sale"].ToString().Trim();
                    string str35 = y["kc_allow_sale"].ToString().Trim();
                    string str36 = y["allow_view_report"].ToString().Trim();
                    string str37 = y["six_rate_owner"].ToString().Trim();
                    string str38 = y["kc_rate_owner"].ToString().Trim();
                    string str39 = y["retry_times"].ToString().Trim();
                    string str40 = y["six_op_odds"].ToString().Trim();
                    string str41 = y["kc_isAuto_back"].ToString().Trim();
                    if (!str6.Equals(str24))
                    {
                        str3 = "名稱:" + str6 + "<br />";
                        str4 = "名稱:" + str24 + "<br />";
                    }
                    if (!str7.Equals(str25))
                    {
                        str3 = str3 + "密碼有變動<br />";
                        str4 = str4 + "密碼有變動<br />";
                        flag = true;
                        num |= 1;
                        CallBLL.cz_users_bll.ZeroIsChanged(changed_user);
                    }
                    if (!str8.Equals(str26))
                    {
                        str3 = str3 + "狀態:" + this.get_user_status(str8) + "<br />";
                        str4 = str4 + "狀態:" + this.get_user_status(str26) + "<br />";
                        flag = true;
                        num |= 2;
                    }
                    if (!str9.Equals(str27))
                    {
                        str3 = str3 + "⑥合彩占成:" + str9 + "<br />";
                        str4 = str4 + "⑥合彩占成:" + str27 + "<br />";
                    }
                    if (!s.Equals(str28))
                    {
                        str3 = str3 + "⑥合彩信用额度:" + double.Parse(s).ToString("F0") + "<br />";
                        str4 = str4 + "⑥合彩信用额度:" + double.Parse(str28).ToString("F0") + "<br />";
                    }
                    if (!str11.Equals(str29))
                    {
                        str3 = str3 + "⑥合彩可用额度:" + double.Parse(str11).ToString("F1") + "<br />";
                        str4 = str4 + "⑥合彩可用额度:" + double.Parse(str29).ToString("F1") + "<br />";
                    }
                    if (!str12.Equals(str30))
                    {
                        str3 = str3 + "⑥合彩盘口:" + str12 + "<br />";
                        str4 = str4 + "⑥合彩盘口:" + str30 + "<br />";
                    }
                    if (!str13.Equals(str31))
                    {
                        str3 = str3 + "快彩占成:" + str13 + "<br />";
                        str4 = str4 + "快彩占成:" + str31 + "<br />";
                    }
                    if (!str14.Equals(str32))
                    {
                        str3 = str3 + "快彩信用额度:" + double.Parse(str14).ToString("F0") + "<br />";
                        str4 = str4 + "快彩信用额度:" + double.Parse(str32).ToString("F0") + "<br />";
                    }
                    if (!str15.Equals(str33))
                    {
                        str3 = str3 + "快彩可用额度:" + double.Parse(str15).ToString("F1") + "<br />";
                        str4 = str4 + "快彩可用额度:" + double.Parse(str33).ToString("F1") + "<br />";
                    }
                    if (!str16.Equals(str34))
                    {
                        str3 = str3 + "⑥合彩補貨功能:" + ((str16 == "1") ? "啟用" : "禁用") + "<br />";
                        str4 = str4 + "⑥合彩補貨功能:" + ((str34 == "1") ? "啟用" : "禁用") + "<br />";
                    }
                    if (!str17.Equals(str35))
                    {
                        str3 = str3 + "快彩補貨功能:" + ((str17 == "1") ? "啟用" : "禁用") + "<br />";
                        str4 = str4 + "快彩補貨功能:" + ((str35 == "1") ? "啟用" : "禁用") + "<br />";
                    }
                    if (!str18.Equals(str36))
                    {
                        str3 = str3 + "開放公司報錶功能:" + ((str18 == "1") ? "開放" : "禁看") + "<br />";
                        str4 = str4 + "開放公司報錶功能:" + ((str36 == "1") ? "開放" : "禁看") + "<br />";
                    }
                    if (!str19.Equals(str37))
                    {
                        str3 = str3 + "⑥合彩占餘成數歸:" + ((str19 == "1") ? "總監" : "分公司") + "<br />";
                        str4 = str4 + "⑥合彩占餘成數歸:" + ((str37 == "1") ? "總監" : "分公司") + "<br />";
                    }
                    if (!str20.Equals(str38))
                    {
                        str3 = str3 + "快彩占餘成數歸:" + ((str20 == "1") ? "總監" : "分公司") + "<br />";
                        str4 = str4 + "快彩占餘成數歸:" + ((str38 == "1") ? "總監" : "分公司") + "<br />";
                    }
                    if ((str21 != str39) && (str39 == "0"))
                    {
                        str3 = str3 + "帳號被鎖<br />";
                        str4 = str4 + "帳號被解鎖<br />";
                    }
                    if (!str22.Equals(str40))
                    {
                        str3 = str3 + "⑥合彩操盤:" + ((str22 == "0") ? "禁止" : "允許") + "<br />";
                        str4 = str4 + "⑥合彩操盤:" + ((str40 == "0") ? "禁止" : "允許") + "<br />";
                    }
                    if (!str23.Equals(str41))
                    {
                        str3 = str3 + "快彩會員現金自動回收:" + ((str23 == "0") ? "自動" : "不自動") + "<br />";
                        str4 = str4 + "快彩會員現金自動回收:" + ((str41 == "0") ? "自動" : "不自動") + "<br />";
                    }
                    if (flag)
                    {
                        if ((num & 1) == 1)
                        {
                            if (FileCacheHelper.get_RedisStatOnline().Equals(1))
                            {
                                CallBLL.cz_users_bll.GetUserInfoByUName(changed_user);
                                base.UpdateIsOutOpts(changed_user);
                            }
                            else if (FileCacheHelper.get_RedisStatOnline().Equals(2))
                            {
                                CallBLL.cz_users_bll.GetUserInfoByUName(changed_user);
                                base.UpdateIsOutOptsStack(changed_user);
                            }
                            else
                            {
                                CallBLL.cz_stat_online_bll.UpdateIsOut(changed_user);
                            }
                        }
                        if ((num & 2) == 2)
                        {
                            if (FileCacheHelper.get_RedisStatOnline().Equals(1))
                            {
                                base.UpdateIsOutOpts(changed_user);
                            }
                            else if (FileCacheHelper.get_RedisStatOnline().Equals(2))
                            {
                                base.UpdateIsOutOptsStack(changed_user);
                            }
                            else
                            {
                                CallBLL.cz_stat_online_bll.UpdateIsOutList(changed_user);
                            }
                        }
                        if (FileCacheHelper.get_RedisStatOnline().Equals(0))
                        {
                            FileCacheHelper.UpdateUserOutFile();
                        }
                    }
                    if (str3 != "")
                    {
                        note = "修改分公司";
                        List<SqlParameter> paramList = new List<SqlParameter>();
                        string str42 = this.add_user_change_log(changed_user, num2, str, str2, note, str3, str4, ref paramList);
                        CallBLL.cz_user_change_log_bll.executte_sql(str42, paramList.ToArray());
                    }
                }
            }
        }

        public void user_edit_hy_log(DataTable oldDT, DataTable newDT, string changed_user)
        {
            string str = this.get_master_name();
            string str2 = this.get_children_name();
            string str3 = "";
            string str4 = "";
            string note = "";
            bool flag = false;
            int num = 0;
            int num2 = 300;
            int count = oldDT.Rows.Count;
            IEqualityComparer<DataRow> comparer = DataRowComparer.Default;
            for (int i = 0; i < count; i++)
            {
                str3 = "";
                str4 = "";
                DataRow x = oldDT.Rows[i];
                DataRow y = newDT.Rows[i];
                if (!comparer.Equals(x, y))
                {
                    string str6 = x["u_nicker"].ToString().Trim();
                    string str7 = x["u_psw"].ToString().Trim();
                    string str8 = x["a_state"].ToString().Trim();
                    string str9 = x["six_rate"].ToString().Trim();
                    string s = x["six_credit"].ToString().Trim();
                    string str11 = x["six_usable_credit"].ToString().Trim();
                    string str12 = x["six_kind"].ToString().Trim();
                    string str13 = x["kc_rate"].ToString().Trim();
                    string str14 = x["kc_credit"].ToString().Trim();
                    string str15 = x["kc_usable_credit"].ToString().Trim();
                    string str16 = x["kc_kind"].ToString().Trim();
                    string str17 = x["retry_times"].ToString().Trim();
                    string str18 = y["u_nicker"].ToString().Trim();
                    string str19 = y["u_psw"].ToString().Trim();
                    string str20 = y["a_state"].ToString().Trim();
                    string str21 = y["six_rate"].ToString().Trim();
                    string str22 = y["six_credit"].ToString().Trim();
                    string str23 = y["six_usable_credit"].ToString().Trim();
                    string str24 = y["six_kind"].ToString().Trim();
                    string str25 = y["kc_rate"].ToString().Trim();
                    string str26 = y["kc_credit"].ToString().Trim();
                    string str27 = y["kc_usable_credit"].ToString().Trim();
                    string str28 = y["kc_kind"].ToString().Trim();
                    string str29 = y["retry_times"].ToString().Trim();
                    if (!str6.Equals(str18))
                    {
                        str3 = " 會員名稱:" + str6 + "<br />";
                        str4 = " 會員名稱:" + str18 + "<br />";
                    }
                    if (!str7.Equals(str19))
                    {
                        str3 = str3 + " 密碼有變動<br />";
                        str4 = str4 + " 密碼有變動<br />";
                        flag = true;
                        num |= 1;
                        CallBLL.cz_users_bll.ZeroIsChanged(changed_user);
                    }
                    if (!str8.Equals(str20))
                    {
                        str3 = str3 + " 會員狀態:" + this.get_user_status(str8) + "<br />";
                        str4 = str4 + " 會員狀態:" + this.get_user_status(str20) + "<br />";
                        flag = true;
                        num |= 2;
                    }
                    if (!str9.Equals(str21))
                    {
                        str3 = str3 + " ⑥合彩占成:" + str9 + "<br />";
                        str4 = str4 + " ⑥合彩占成:" + str21 + "<br />";
                    }
                    if (!s.Equals(str22))
                    {
                        str3 = str3 + " ⑥合彩信用额度:" + double.Parse(s).ToString("F0") + "<br />";
                        str4 = str4 + " ⑥合彩信用额度:" + double.Parse(str22).ToString("F0") + "<br />";
                    }
                    if (!str11.Equals(str23))
                    {
                        str3 = str3 + " ⑥合彩可用额度:" + double.Parse(str11).ToString("F1") + "<br />";
                        str4 = str4 + " ⑥合彩可用额度:" + double.Parse(str23).ToString("F1") + "<br />";
                    }
                    if (!str12.Equals(str24))
                    {
                        str3 = str3 + " ⑥合彩盘口:" + str12 + "<br />";
                        str4 = str4 + " ⑥合彩盘口:" + str24 + "<br />";
                    }
                    if (!str13.Equals(str25))
                    {
                        str3 = str3 + " 快彩占成:" + str13 + "<br />";
                        str4 = str4 + " 快彩占成:" + str25 + "<br />";
                    }
                    if (!str14.Equals(str26))
                    {
                        str3 = str3 + " 快彩信用额度:" + double.Parse(str14).ToString("F0") + "<br />";
                        str4 = str4 + " 快彩信用额度:" + double.Parse(str26).ToString("F0") + "<br />";
                    }
                    if (!str15.Equals(str27))
                    {
                        str3 = str3 + " 快彩可用额度:" + double.Parse(str15).ToString("F1") + "<br />";
                        str4 = str4 + " 快彩可用额度:" + double.Parse(str27).ToString("F1") + "<br />";
                    }
                    if (!str16.Equals(str28))
                    {
                        str3 = str3 + " 快彩盘口:" + str16 + "<br />";
                        str4 = str4 + " 快彩盘口:" + str28 + "<br />";
                    }
                    if ((str17 != str29) && (str29 == "0"))
                    {
                        str3 = str3 + " 帳號被鎖<br />";
                        str4 = str4 + " 帳號被解鎖<br />";
                    }
                    if (flag)
                    {
                        if ((num & 1) == 1)
                        {
                            if (FileCacheHelper.get_RedisStatOnline().Equals(1))
                            {
                                CallBLL.cz_users_bll.GetUserInfoByUName(changed_user);
                                base.UpdateIsOutOpts(changed_user);
                            }
                            else if (FileCacheHelper.get_RedisStatOnline().Equals(2))
                            {
                                CallBLL.cz_users_bll.GetUserInfoByUName(changed_user);
                                base.UpdateIsOutOptsStack(changed_user);
                            }
                            else
                            {
                                CallBLL.cz_stat_online_bll.UpdateIsOut(changed_user);
                            }
                        }
                        if ((num & 2) == 2)
                        {
                            if (FileCacheHelper.get_RedisStatOnline().Equals(1))
                            {
                                CallBLL.cz_users_bll.GetUserInfoByUName(changed_user);
                                base.UpdateIsOutOpts(changed_user);
                            }
                            else if (FileCacheHelper.get_RedisStatOnline().Equals(2))
                            {
                                CallBLL.cz_users_bll.GetUserInfoByUName(changed_user);
                                base.UpdateIsOutOptsStack(changed_user);
                            }
                            else
                            {
                                CallBLL.cz_stat_online_bll.UpdateIsOut(changed_user);
                            }
                        }
                        if (FileCacheHelper.get_RedisStatOnline().Equals(0))
                        {
                            FileCacheHelper.UpdateUserOutFile();
                        }
                    }
                    if (str3 != "")
                    {
                        note = "修改用户";
                        List<SqlParameter> paramList = new List<SqlParameter>();
                        string str30 = this.add_user_change_log(changed_user, num2, str, str2, note, str3, str4, ref paramList);
                        CallBLL.cz_user_change_log_bll.executte_sql(str30, paramList.ToArray());
                    }
                }
            }
        }

        public string user_hy_profit_log(cz_users model, bool kcShow, double kcProfit, double kc_usable_credit, bool sixShow, double sixProfit, double six_usable_credit, ref List<SqlParameter> paramList)
        {
            string str = this.get_master_name();
            string str2 = this.get_children_name();
            string str3 = "";
            string str4 = "";
            string note = "";
            string str6 = model.get_u_name();
            int num = 300;
            string str7 = "";
            if (kcShow && sixShow)
            {
                if (sixProfit > 0.0)
                {
                    object obj2 = str3 + " ⑥合彩信用额度:" + double.Parse(model.get_six_credit().ToString()).ToString() + "; ";
                    str3 = string.Concat(new object[] { obj2, " ⑥合彩可用额度:", six_usable_credit, "<br />" });
                    object obj3 = str4;
                    str4 = string.Concat(new object[] { obj3, " ⑥合彩可用额度變動:", six_usable_credit - sixProfit, "(回收盈利額度:-", sixProfit, ")<br />" });
                }
                if (kcProfit > 0.0)
                {
                    str3 = (str3 + " 快彩信用额度:" + double.Parse(model.get_kc_credit().ToString()).ToString() + "; ") + " 快彩可用额度:" + kc_usable_credit;
                    object obj4 = str4;
                    str4 = string.Concat(new object[] { obj4, " 快彩可用额度變動:", kc_usable_credit - kcProfit, "(回收盈利額度:-", kcProfit, ")" });
                }
                num = 300;
            }
            if ((sixShow && !kcShow) && (sixProfit > 0.0))
            {
                str3 = (str3 + " ⑥合彩信用额度:" + double.Parse(model.get_six_credit().ToString()).ToString() + "; ") + " ⑥合彩可用额度:" + six_usable_credit;
                object obj5 = str4;
                str4 = string.Concat(new object[] { obj5, " ⑥合彩可用额度變動:", six_usable_credit - sixProfit, "(回收盈利額度:-", sixProfit, ")" });
                num = 100;
            }
            if ((!sixShow && kcShow) && (kcProfit > 0.0))
            {
                str3 = (str3 + " 快彩信用额度:" + double.Parse(model.get_kc_credit().ToString()).ToString() + "; ") + " 快彩可用额度:" + kc_usable_credit;
                object obj6 = str4;
                str4 = string.Concat(new object[] { obj6, " 快彩可用额度變動:", kc_usable_credit - kcProfit, "(回收盈利額度:-", kcProfit, ")" });
                num = 200;
            }
            if (str4 != "")
            {
                note = "盈利回收";
                str7 = this.add_user_change_log(str6, num, str, str2, note, str3, str4, ref paramList);
            }
            return str7;
        }

        public void UserDrawback_car168()
        {
            if (CacheHelper.GetCache("kc_drawback_FileCacheKey_car168" + this.Session["user_name"].ToString()) == null)
            {
                string str = (this.Session["kc_rate_FileCacheKey" + this.Session["user_name"].ToString()] as agent_kc_rate).get_mastername();
                DataTable drawback = CallBLL.cz_drawback_car168_bll.GetDrawback(str);
                CacheHelper.SetPublicFileCache("kc_drawback_FileCacheKey_car168" + this.Session["user_name"].ToString(), drawback, PageBase.GetPublicForderPath(FileCacheHelper.get_Drawback_Caches_CAR168()));
            }
        }

        public void UserDrawback_cqsc()
        {
            if (CacheHelper.GetCache("kc_drawback_FileCacheKey_cqsc" + this.Session["user_name"].ToString()) == null)
            {
                string str = (this.Session["kc_rate_FileCacheKey" + this.Session["user_name"].ToString()] as agent_kc_rate).get_mastername();
                DataTable drawback = CallBLL.cz_drawback_cqsc_bll.GetDrawback(str);
                CacheHelper.SetPublicFileCache("kc_drawback_FileCacheKey_cqsc" + this.Session["user_name"].ToString(), drawback, PageBase.GetPublicForderPath(FileCacheHelper.get_Drawback_Caches_CQSC()));
            }
        }

        public void UserDrawback_happycar()
        {
            if (CacheHelper.GetCache("kc_drawback_FileCacheKey_happycar" + this.Session["user_name"].ToString()) == null)
            {
                string str = (this.Session["kc_rate_FileCacheKey" + this.Session["user_name"].ToString()] as agent_kc_rate).get_mastername();
                DataTable drawback = CallBLL.cz_drawback_happycar_bll.GetDrawback(str);
                CacheHelper.SetPublicFileCache("kc_drawback_FileCacheKey_happycar" + this.Session["user_name"].ToString(), drawback, PageBase.GetPublicForderPath(FileCacheHelper.get_Drawback_Caches_HAPPYCAR()));
            }
        }

        public void UserDrawback_jscar()
        {
            if (CacheHelper.GetCache("kc_drawback_FileCacheKey_jscar" + this.Session["user_name"].ToString()) == null)
            {
                string str = (this.Session["kc_rate_FileCacheKey" + this.Session["user_name"].ToString()] as agent_kc_rate).get_mastername();
                DataTable drawback = CallBLL.cz_drawback_jscar_bll.GetDrawback(str);
                CacheHelper.SetPublicFileCache("kc_drawback_FileCacheKey_jscar" + this.Session["user_name"].ToString(), drawback, PageBase.GetPublicForderPath(FileCacheHelper.get_Drawback_Caches_JSCAR()));
            }
        }

        public void UserDrawback_jscqsc()
        {
            if (CacheHelper.GetCache("kc_drawback_FileCacheKey_jscqsc" + this.Session["user_name"].ToString()) == null)
            {
                string str = (this.Session["kc_rate_FileCacheKey" + this.Session["user_name"].ToString()] as agent_kc_rate).get_mastername();
                DataTable drawback = CallBLL.cz_drawback_jscqsc_bll.GetDrawback(str);
                CacheHelper.SetPublicFileCache("kc_drawback_FileCacheKey_jscqsc" + this.Session["user_name"].ToString(), drawback, PageBase.GetPublicForderPath(FileCacheHelper.get_Drawback_Caches_JSCQSC()));
            }
        }

        public void UserDrawback_jsft2()
        {
            if (CacheHelper.GetCache("kc_drawback_FileCacheKey_jsft2" + this.Session["user_name"].ToString()) == null)
            {
                string str = (this.Session["kc_rate_FileCacheKey" + this.Session["user_name"].ToString()] as agent_kc_rate).get_mastername();
                DataTable drawback = CallBLL.cz_drawback_jsft2_bll.GetDrawback(str);
                CacheHelper.SetPublicFileCache("kc_drawback_FileCacheKey_jsft2" + this.Session["user_name"].ToString(), drawback, PageBase.GetPublicForderPath(FileCacheHelper.get_Drawback_Caches_JSFT2()));
            }
        }

        public void UserDrawback_jsk3()
        {
            if (CacheHelper.GetCache("kc_drawback_FileCacheKey_jsk3" + this.Session["user_name"].ToString()) == null)
            {
                string str = (this.Session["kc_rate_FileCacheKey" + this.Session["user_name"].ToString()] as agent_kc_rate).get_mastername();
                DataTable drawback = CallBLL.cz_drawback_jsk3_bll.GetDrawback(str);
                CacheHelper.SetPublicFileCache("kc_drawback_FileCacheKey_jsk3" + this.Session["user_name"].ToString(), drawback, PageBase.GetPublicForderPath(FileCacheHelper.get_Drawback_Caches_JSK3()));
            }
        }

        public void UserDrawback_jspk10()
        {
            if (CacheHelper.GetCache("kc_drawback_FileCacheKey_jspk10" + this.Session["user_name"].ToString()) == null)
            {
                string str = (this.Session["kc_rate_FileCacheKey" + this.Session["user_name"].ToString()] as agent_kc_rate).get_mastername();
                DataTable drawback = CallBLL.cz_drawback_jspk10_bll.GetDrawback(str);
                CacheHelper.SetPublicFileCache("kc_drawback_FileCacheKey_jspk10" + this.Session["user_name"].ToString(), drawback, PageBase.GetPublicForderPath(FileCacheHelper.get_Drawback_Caches_JSPK10()));
            }
        }

        public void UserDrawback_jssfc()
        {
            if (CacheHelper.GetCache("kc_drawback_FileCacheKey_jssfc" + this.Session["user_name"].ToString()) == null)
            {
                string str = (this.Session["kc_rate_FileCacheKey" + this.Session["user_name"].ToString()] as agent_kc_rate).get_mastername();
                DataTable drawback = CallBLL.cz_drawback_jssfc_bll.GetDrawback(str);
                CacheHelper.SetPublicFileCache("kc_drawback_FileCacheKey_jssfc" + this.Session["user_name"].ToString(), drawback, PageBase.GetPublicForderPath(FileCacheHelper.get_Drawback_Caches_JSSFC()));
            }
        }

        public void UserDrawback_k8sc()
        {
            if (CacheHelper.GetCache("kc_drawback_FileCacheKey_k8sc" + this.Session["user_name"].ToString()) == null)
            {
                string str = (this.Session["kc_rate_FileCacheKey" + this.Session["user_name"].ToString()] as agent_kc_rate).get_mastername();
                DataTable drawback = CallBLL.cz_drawback_k8sc_bll.GetDrawback(str);
                CacheHelper.SetPublicFileCache("kc_drawback_FileCacheKey_k8sc" + this.Session["user_name"].ToString(), drawback, PageBase.GetPublicForderPath(FileCacheHelper.get_Drawback_Caches_K8SC()));
            }
        }

        public void UserDrawback_kl10()
        {
            if (CacheHelper.GetCache("kc_drawback_FileCacheKey_kl10" + this.Session["user_name"].ToString()) == null)
            {
                string str = (this.Session["kc_rate_FileCacheKey" + this.Session["user_name"].ToString()] as agent_kc_rate).get_mastername();
                DataTable drawback = CallBLL.cz_drawback_kl10_bll.GetDrawback(str);
                CacheHelper.SetPublicFileCache("kc_drawback_FileCacheKey_kl10" + this.Session["user_name"].ToString(), drawback, PageBase.GetPublicForderPath(FileCacheHelper.get_Drawback_Caches_KL10()));
            }
        }

        public void UserDrawback_kl8()
        {
            if (CacheHelper.GetCache("kc_drawback_FileCacheKey_kl8" + this.Session["user_name"].ToString()) == null)
            {
                string str = (this.Session["kc_rate_FileCacheKey" + this.Session["user_name"].ToString()] as agent_kc_rate).get_mastername();
                DataTable drawback = CallBLL.cz_drawback_kl8_bll.GetDrawback(str);
                CacheHelper.SetPublicFileCache("kc_drawback_FileCacheKey_kl8" + this.Session["user_name"].ToString(), drawback, PageBase.GetPublicForderPath(FileCacheHelper.get_Drawback_Caches_KL8()));
            }
        }

        public void UserDrawback_pcdd()
        {
            if (CacheHelper.GetCache("kc_drawback_FileCacheKey_pcdd" + this.Session["user_name"].ToString()) == null)
            {
                string str = (this.Session["kc_rate_FileCacheKey" + this.Session["user_name"].ToString()] as agent_kc_rate).get_mastername();
                DataTable drawback = CallBLL.cz_drawback_pcdd_bll.GetDrawback(str);
                CacheHelper.SetPublicFileCache("kc_drawback_FileCacheKey_pcdd" + this.Session["user_name"].ToString(), drawback, PageBase.GetPublicForderPath(FileCacheHelper.get_Drawback_Caches_PCDD()));
            }
        }

        public void UserDrawback_pk10()
        {
            if (CacheHelper.GetCache("kc_drawback_FileCacheKey_pk10" + this.Session["user_name"].ToString()) == null)
            {
                string str = (this.Session["kc_rate_FileCacheKey" + this.Session["user_name"].ToString()] as agent_kc_rate).get_mastername();
                DataTable drawback = CallBLL.cz_drawback_pk10_bll.GetDrawback(str);
                CacheHelper.SetPublicFileCache("kc_drawback_FileCacheKey_pk10" + this.Session["user_name"].ToString(), drawback, PageBase.GetPublicForderPath(FileCacheHelper.get_Drawback_Caches_PK10()));
            }
        }

        public void UserDrawback_pkbjl()
        {
            if (CacheHelper.GetCache("kc_drawback_FileCacheKey_pkbjl" + this.Session["user_name"].ToString()) == null)
            {
                string str = (this.Session["kc_rate_FileCacheKey" + this.Session["user_name"].ToString()] as agent_kc_rate).get_mastername();
                DataTable drawback = CallBLL.cz_drawback_pkbjl_bll.GetDrawback(str);
                CacheHelper.SetPublicFileCache("kc_drawback_FileCacheKey_pkbjl" + this.Session["user_name"].ToString(), drawback, PageBase.GetPublicForderPath(FileCacheHelper.get_Drawback_Caches_PKBJL()));
            }
        }

        public void UserDrawback_six()
        {
            if (CacheHelper.GetCache("six_drawback_FileCacheKey" + HttpContext.Current.Session["user_name"].ToString()) == null)
            {
                string str = (HttpContext.Current.Session["six_rate_FileCacheKey" + HttpContext.Current.Session["user_name"].ToString()] as agent_six_rate).get_mastername();
                DataTable drawback = CallBLL.cz_drawback_six_bll.GetDrawback(str);
                DataSet set = new DataSet();
                if (drawback != null)
                {
                    set.Tables.Add(drawback.Copy());
                }
                CacheHelper.SetPublicFileCache("six_drawback_FileCacheKey" + HttpContext.Current.Session["user_name"].ToString(), set, PageBase.GetPublicForderPath(FileCacheHelper.get_Drawback_Caches_SIX()));
            }
        }

        public void UserDrawback_speed5()
        {
            if (CacheHelper.GetCache("kc_drawback_FileCacheKey_speed5" + this.Session["user_name"].ToString()) == null)
            {
                string str = (this.Session["kc_rate_FileCacheKey" + this.Session["user_name"].ToString()] as agent_kc_rate).get_mastername();
                DataTable drawback = CallBLL.cz_drawback_speed5_bll.GetDrawback(str);
                CacheHelper.SetPublicFileCache("kc_drawback_FileCacheKey_speed5" + this.Session["user_name"].ToString(), drawback, PageBase.GetPublicForderPath(FileCacheHelper.get_Drawback_Caches_SPEED5()));
            }
        }

        public void UserDrawback_ssc168()
        {
            if (CacheHelper.GetCache("kc_drawback_FileCacheKey_ssc168" + this.Session["user_name"].ToString()) == null)
            {
                string str = (this.Session["kc_rate_FileCacheKey" + this.Session["user_name"].ToString()] as agent_kc_rate).get_mastername();
                DataTable drawback = CallBLL.cz_drawback_ssc168_bll.GetDrawback(str);
                CacheHelper.SetPublicFileCache("kc_drawback_FileCacheKey_ssc168" + this.Session["user_name"].ToString(), drawback, PageBase.GetPublicForderPath(FileCacheHelper.get_Drawback_Caches_SSC168()));
            }
        }

        public void UserDrawback_vrcar()
        {
            if (CacheHelper.GetCache("kc_drawback_FileCacheKey_vrcar" + this.Session["user_name"].ToString()) == null)
            {
                string str = (this.Session["kc_rate_FileCacheKey" + this.Session["user_name"].ToString()] as agent_kc_rate).get_mastername();
                DataTable drawback = CallBLL.cz_drawback_vrcar_bll.GetDrawback(str);
                CacheHelper.SetPublicFileCache("kc_drawback_FileCacheKey_vrcar" + this.Session["user_name"].ToString(), drawback, PageBase.GetPublicForderPath(FileCacheHelper.get_Drawback_Caches_VRCAR()));
            }
        }

        public void UserDrawback_vrssc()
        {
            if (CacheHelper.GetCache("kc_drawback_FileCacheKey_vrssc" + this.Session["user_name"].ToString()) == null)
            {
                string str = (this.Session["kc_rate_FileCacheKey" + this.Session["user_name"].ToString()] as agent_kc_rate).get_mastername();
                DataTable drawback = CallBLL.cz_drawback_vrssc_bll.GetDrawback(str);
                CacheHelper.SetPublicFileCache("kc_drawback_FileCacheKey_vrssc" + this.Session["user_name"].ToString(), drawback, PageBase.GetPublicForderPath(FileCacheHelper.get_Drawback_Caches_VRSSC()));
            }
        }

        public void UserDrawback_xyft5()
        {
            if (CacheHelper.GetCache("kc_drawback_FileCacheKey_xyft5" + this.Session["user_name"].ToString()) == null)
            {
                string str = (this.Session["kc_rate_FileCacheKey" + this.Session["user_name"].ToString()] as agent_kc_rate).get_mastername();
                DataTable drawback = CallBLL.cz_drawback_xyft5_bll.GetDrawback(str);
                CacheHelper.SetPublicFileCache("kc_drawback_FileCacheKey_xyft5" + this.Session["user_name"].ToString(), drawback, PageBase.GetPublicForderPath(FileCacheHelper.get_Drawback_Caches_XYFT5()));
            }
        }

        public void UserDrawback_xyftoa()
        {
            if (CacheHelper.GetCache("kc_drawback_FileCacheKey_xyftoa" + this.Session["user_name"].ToString()) == null)
            {
                string str = (this.Session["kc_rate_FileCacheKey" + this.Session["user_name"].ToString()] as agent_kc_rate).get_mastername();
                DataTable drawback = CallBLL.cz_drawback_xyftoa_bll.GetDrawback(str);
                CacheHelper.SetPublicFileCache("kc_drawback_FileCacheKey_xyftoa" + this.Session["user_name"].ToString(), drawback, PageBase.GetPublicForderPath(FileCacheHelper.get_Drawback_Caches_XYFTOA()));
            }
        }

        public void UserDrawback_xyftsg()
        {
            if (CacheHelper.GetCache("kc_drawback_FileCacheKey_xyftsg" + this.Session["user_name"].ToString()) == null)
            {
                string str = (this.Session["kc_rate_FileCacheKey" + this.Session["user_name"].ToString()] as agent_kc_rate).get_mastername();
                DataTable drawback = CallBLL.cz_drawback_xyftsg_bll.GetDrawback(str);
                CacheHelper.SetPublicFileCache("kc_drawback_FileCacheKey_xyftsg" + this.Session["user_name"].ToString(), drawback, PageBase.GetPublicForderPath(FileCacheHelper.get_Drawback_Caches_XYFTSG()));
            }
        }

        public void UserDrawback_xync()
        {
            if (CacheHelper.GetCache("kc_drawback_FileCacheKey_xync" + this.Session["user_name"].ToString()) == null)
            {
                string str = (this.Session["kc_rate_FileCacheKey" + this.Session["user_name"].ToString()] as agent_kc_rate).get_mastername();
                DataTable drawback = CallBLL.cz_drawback_xync_bll.GetDrawback(str);
                CacheHelper.SetPublicFileCache("kc_drawback_FileCacheKey_xync" + this.Session["user_name"].ToString(), drawback, PageBase.GetPublicForderPath(FileCacheHelper.get_Drawback_Caches_XYNC()));
            }
        }

        public void UserRate_kc(string zjName)
        {
            if (this.Session[this.Session["user_name"] + "_begin_kc_date"] != null)
            {
                string str = DateTime.Now.AddHours(-7.0).ToString("yyyy-MM-dd");
                if (this.Session[HttpContext.Current.Session["user_name"] + "_begin_kc_date"].ToString().Trim() != str)
                {
                    base.Response.Write("<script>top.location.href='/'</script>");
                    base.Response.End();
                }
            }
            else
            {
                string str2 = DateTime.Now.AddHours(-7.0).ToString("yyyy-MM-dd");
                this.Session[this.Session["user_name"] + "_begin_kc_date"] = str2;
            }
            if (HttpContext.Current.Session["kc_rate_FileCacheKey" + HttpContext.Current.Session["user_name"].ToString()] == null)
            {
                DataTable table = CallBLL.cz_rate_kc_bll.Agent_GetRateByAccount(HttpContext.Current.Session["user_name"].ToString()).Tables[0];
                if (table.Rows.Count > 0)
                {
                    agent_kc_rate _rate = new agent_kc_rate();
                    _rate.set_fgsname(table.Rows[0]["fgs_name"].ToString().Trim());
                    _rate.set_gdname(table.Rows[0]["gd_name"].ToString().Trim());
                    _rate.set_zdname(table.Rows[0]["zd_name"].ToString().Trim());
                    _rate.set_dlname(table.Rows[0]["dl_name"].ToString().Trim());
                    _rate.set_zjzc(table.Rows[0]["zj_rate"].ToString().Trim());
                    _rate.set_fgszc(table.Rows[0]["fgs_rate"].ToString().Trim());
                    _rate.set_gdzc(table.Rows[0]["gd_rate"].ToString().Trim());
                    _rate.set_zdzc(table.Rows[0]["zd_rate"].ToString().Trim());
                    _rate.set_dlzc(table.Rows[0]["dl_rate"].ToString().Trim());
                    _rate.set_zcyg(table.Rows[0]["kc_rate_owner"].ToString().Trim());
                    table.Rows[0]["kc_rate_owner"].ToString().Trim();
                    double.Parse(table.Rows[0]["zj_rate"].ToString().Trim());
                    double.Parse(table.Rows[0]["fgs_rate"].ToString().Trim());
                    double.Parse(table.Rows[0]["gd_rate"].ToString().Trim());
                    double.Parse(table.Rows[0]["zd_rate"].ToString().Trim());
                    _rate.set_mastername(string.Format("'{0}','{1}','{2}','{3}','{4}'", new object[] { zjName, _rate.get_fgsname(), _rate.get_gdname(), _rate.get_zdname(), _rate.get_dlname() }));
                    HttpContext.Current.Session["kc_rate_FileCacheKey" + HttpContext.Current.Session["user_name"].ToString()] = _rate;
                    CacheHelper.SetPublicFileCache("kc_rate_FileCacheKey" + HttpContext.Current.Session["user_name"].ToString(), _rate, PageBase.GetPublicForderPath(FileCacheHelper.get_KC_RateCachesFileName()));
                }
            }
        }

        public void UserRate_six(string zjName)
        {
            if ((CacheHelper.GetCache("six_rate_FileCacheKey" + HttpContext.Current.Session["user_name"].ToString()) == null) || (HttpContext.Current.Session["six_rate_FileCacheKey" + HttpContext.Current.Session["user_name"].ToString()] == null))
            {
                DataTable table = CallBLL.cz_rate_six_bll.GetRateByAccount(HttpContext.Current.Session["user_name"].ToString()).Tables[0];
                if (table.Rows.Count > 0)
                {
                    agent_six_rate _rate = new agent_six_rate();
                    _rate.set_fgsname(table.Rows[0]["fgs_name"].ToString().Trim());
                    _rate.set_gdname(table.Rows[0]["gd_name"].ToString().Trim());
                    _rate.set_zdname(table.Rows[0]["zd_name"].ToString().Trim());
                    _rate.set_dlname(table.Rows[0]["dl_name"].ToString().Trim());
                    _rate.set_zjzc(table.Rows[0]["zj_rate"].ToString().Trim());
                    _rate.set_fgszc(table.Rows[0]["fgs_rate"].ToString().Trim());
                    _rate.set_gdzc(table.Rows[0]["gd_rate"].ToString().Trim());
                    _rate.set_zdzc(table.Rows[0]["zd_rate"].ToString().Trim());
                    _rate.set_dlzc(table.Rows[0]["dl_rate"].ToString().Trim());
                    _rate.set_zcyg(table.Rows[0]["six_rate_owner"].ToString().Trim());
                    string str = table.Rows[0]["six_rate_owner"].ToString().Trim();
                    double num = ((double.Parse(table.Rows[0]["zj_rate"].ToString().Trim()) + double.Parse(table.Rows[0]["fgs_rate"].ToString().Trim())) + double.Parse(table.Rows[0]["gd_rate"].ToString().Trim())) + double.Parse(table.Rows[0]["zd_rate"].ToString().Trim());
                    if (str == "1")
                    {
                        double num2 = 100.0 - num;
                        _rate.set_zjzc((double.Parse(table.Rows[0]["zj_rate"].ToString().Trim()) + num2).ToString());
                    }
                    else if (HttpContext.Current.Session["user_type"].ToString().Equals("fgs"))
                    {
                        _rate.set_zjzc("100");
                    }
                    else
                    {
                        double num3 = 100.0 - num;
                        _rate.set_fgszc((double.Parse(table.Rows[0]["fgs_rate"].ToString().Trim()) + num3).ToString());
                    }
                    _rate.set_mastername(string.Format("'{0}','{1}','{2}','{3}','{4}'", new object[] { zjName, _rate.get_fgsname(), _rate.get_gdname(), _rate.get_zdname(), _rate.get_dlname() }));
                    HttpContext.Current.Session["six_rate_FileCacheKey" + HttpContext.Current.Session["user_name"].ToString()] = _rate;
                    CacheHelper.SetPublicFileCache("six_rate_FileCacheKey" + HttpContext.Current.Session["user_name"].ToString(), _rate, PageBase.GetPublicForderPath(FileCacheHelper.get_SIX_RateCachesFileName()));
                }
            }
        }

        protected void UserRecharge_kc(cz_users edit_model, cz_users up_edit_model, string txt_recharge)
        {
            string str = HttpContext.Current.Session["user_name"].ToString();
            agent_userinfo_session _session = HttpContext.Current.Session[str + "lottery_session_user_info"] as agent_userinfo_session;
            string random = Utils.GetRandom();
            cz_credit_flow _flow = new cz_credit_flow();
            _flow.set_master_id(2);
            _flow.set_credit_old(edit_model.get_kc_usable_credit());
            _flow.set_credit_new(new decimal?(decimal.Parse(txt_recharge)));
            _flow.set_credit_change(new decimal?(decimal.Parse(txt_recharge)));
            _flow.set_u_name(edit_model.get_u_name());
            _flow.set_iscash(new int?(int.Parse("1")));
            _flow.set_operator_name(_session.get_u_name());
            if (_session.get_users_child_session() != null)
            {
                _flow.set_operator_child_name(_session.get_users_child_session().get_u_name());
            }
            _flow.set_operator_time(DateTime.Now);
            _flow.set_flag(0);
            _flow.set_checkcode(random);
            _flow.set_isphone(0);
            _flow.set_note("開賬戶");
            _flow.set_ip(LSRequest.GetIP());
            cz_credit_flow _flow2 = new cz_credit_flow();
            _flow2.set_master_id(2);
            _flow2.set_credit_old(up_edit_model.get_kc_usable_credit());
            decimal? nullable = up_edit_model.get_kc_usable_credit();
            decimal num = decimal.Parse(txt_recharge);
            _flow2.set_credit_new(nullable.HasValue ? new decimal?(nullable.GetValueOrDefault() - num) : null);
            _flow2.set_credit_change(new decimal?(-decimal.Parse(txt_recharge)));
            _flow2.set_u_name(up_edit_model.get_u_name());
            _flow2.set_iscash(new int?(int.Parse("1")));
            _flow2.set_operator_name(_session.get_u_name());
            if (_session.get_users_child_session() != null)
            {
                _flow2.set_operator_child_name(_session.get_users_child_session().get_u_name());
            }
            _flow2.set_operator_time(DateTime.Now);
            _flow2.set_flag(1);
            _flow2.set_checkcode(random);
            _flow2.set_note("開賬戶 -> " + edit_model.get_u_name());
            _flow2.set_isphone(0);
            _flow2.set_ip(LSRequest.GetIP());
            CallBLL.cz_users_bll.UserRecharge(double.Parse(txt_recharge), _flow, _flow2);
        }

        protected void UserRecharge_six(cz_users edit_model, cz_users up_edit_model, string txt_recharge)
        {
            string str = HttpContext.Current.Session["user_name"].ToString();
            agent_userinfo_session _session = HttpContext.Current.Session[str + "lottery_session_user_info"] as agent_userinfo_session;
            string random = Utils.GetRandom();
            cz_credit_flow _flow = new cz_credit_flow();
            _flow.set_master_id(1);
            _flow.set_credit_old(edit_model.get_six_usable_credit());
            _flow.set_credit_new(new decimal?(decimal.Parse(txt_recharge)));
            _flow.set_credit_change(new decimal?(decimal.Parse(txt_recharge)));
            _flow.set_u_name(edit_model.get_u_name());
            _flow.set_iscash(new int?(int.Parse("1")));
            _flow.set_operator_name(_session.get_u_name());
            if (_session.get_users_child_session() != null)
            {
                _flow.set_operator_child_name(_session.get_users_child_session().get_u_name());
            }
            _flow.set_operator_time(DateTime.Now);
            _flow.set_flag(0);
            _flow.set_checkcode(random);
            _flow.set_isphone(0);
            _flow.set_note("開賬戶");
            _flow.set_ip(LSRequest.GetIP());
            cz_credit_flow _flow2 = new cz_credit_flow();
            _flow2.set_master_id(1);
            _flow2.set_credit_old(up_edit_model.get_six_usable_credit());
            decimal? nullable = up_edit_model.get_six_usable_credit();
            decimal num = decimal.Parse(txt_recharge);
            _flow2.set_credit_new(nullable.HasValue ? new decimal?(nullable.GetValueOrDefault() - num) : null);
            _flow2.set_credit_change(new decimal?(-decimal.Parse(txt_recharge)));
            _flow2.set_u_name(up_edit_model.get_u_name());
            _flow2.set_iscash(new int?(int.Parse("1")));
            _flow2.set_operator_name(_session.get_u_name());
            if (_session.get_users_child_session() != null)
            {
                _flow2.set_operator_child_name(_session.get_users_child_session().get_u_name());
            }
            _flow2.set_operator_time(DateTime.Now);
            _flow2.set_flag(1);
            _flow2.set_checkcode(random);
            _flow2.set_note("開賬戶 -> " + edit_model.get_u_name());
            _flow2.set_isphone(0);
            _flow2.set_ip(LSRequest.GetIP());
            CallBLL.cz_users_bll.UserRecharge(double.Parse(txt_recharge), _flow, _flow2);
        }

        protected bool ValidParamByUserAdd(string utype, ref string message, object[] obj, string six, string kc)
        {
            string str = utype.ToLower();
            if (string.IsNullOrEmpty(str))
            {
                message = "用戶類型不正確！";
                return false;
            }
            bool flag = true;
            if (str.Equals("fgs"))
            {
                if (string.IsNullOrEmpty(LSRequest.qq("userName").Trim()))
                {
                    message = "帳號 不能為空！";
                    return false;
                }
                if ((base.q("userName").Trim().Length < 6) || (base.q("userName").Trim().Length > 12))
                {
                    message = "帳號 最小長度必須6-12位！";
                    return false;
                }
                if (!Utils.UserNameRegex(base.q("userName")))
                {
                    message = "帳號必須包含字母和數字，支持‘_’，但開頭和結尾不能用‘_’！";
                    return false;
                }
                if (string.IsNullOrEmpty(base.q("userPassword").Trim()))
                {
                    message = "密碼 不能為空！";
                    return false;
                }
                if ((base.q("userPassword").Trim().Length < 8) || (base.q("userPassword").Trim().Length > 20))
                {
                    message = "密碼 最小長度必須 8位 或以上（最長20位）！";
                    return false;
                }
                flag = true;
                if (!string.IsNullOrEmpty(six))
                {
                    if (string.IsNullOrEmpty(base.q("userCredit_six").Trim()))
                    {
                        message = "⑥合彩: 信用額度 請務必輸入！";
                        return false;
                    }
                    try
                    {
                        if (int.Parse(base.q("userCredit_six").Trim()) < 0)
                        {
                            message = "⑥合彩:信用額度 必須大於等於0";
                            return false;
                        }
                    }
                    catch
                    {
                        message = "⑥合彩:信用額度 只能為數字！";
                        return false;
                    }
                    if (string.IsNullOrEmpty(base.q("userRate_six").Trim()))
                    {
                        message = "⑥合彩:總監成數 不能為空！";
                        return false;
                    }
                    try
                    {
                        int num2 = int.Parse(base.q("userRate_six").Trim());
                        if (num2 < int.Parse(base.get_ZJMinRate_SIX()))
                        {
                            message = string.Format("⑥合彩:總監成數 不能小於{0}%", base.get_ZJMinRate_SIX());
                            return false;
                        }
                        if (num2 > 100)
                        {
                            message = "⑥合彩:總監占成 不能大于100%！";
                            return false;
                        }
                        flag = true;
                    }
                    catch
                    {
                        message = "⑥合彩:總監成數 只能數字！";
                        return false;
                    }
                }
                if (string.IsNullOrEmpty(kc))
                {
                    goto Label_1262;
                }
                if (string.IsNullOrEmpty(base.q("userCredit_kc").Trim()))
                {
                    message = "快彩:信用額度 請務必輸入！";
                    return false;
                }
                try
                {
                    if (int.Parse(base.q("userCredit_kc").Trim()) < 0)
                    {
                        message = "快彩:信用額度 必須大於等於0";
                        return false;
                    }
                }
                catch
                {
                    message = "快彩:信用額度 只能為數字！";
                    return false;
                }
                if (string.IsNullOrEmpty(base.q("userRate_kc").Trim()))
                {
                    message = "快彩:總監成數 不能為空！";
                    return false;
                }
                try
                {
                    int num4 = int.Parse(base.q("userRate_kc").Trim());
                    if (num4 < int.Parse(base.get_ZJMinRate_KC()))
                    {
                        message = string.Format("快彩:總監成數 不能小於{0}%", base.get_ZJMinRate_KC());
                        return false;
                    }
                    if (num4 > 100)
                    {
                        message = "快彩:總監占成 不能大于100%！";
                        return false;
                    }
                    flag = true;
                    goto Label_1262;
                }
                catch
                {
                    message = "快彩:總監成數 只能數字！";
                    return false;
                }
            }
            if (str.Equals("gd"))
            {
                if (string.IsNullOrEmpty(base.q("sltupuser").Trim()))
                {
                    message = "上級分公司 不能為空！";
                    return false;
                }
                if (string.IsNullOrEmpty(base.q("userName").Trim()))
                {
                    message = "帳號 不能為空！";
                    return false;
                }
                if ((base.q("userName").Trim().Length < 6) || (base.q("userName").Trim().Length > 12))
                {
                    message = "帳號 最小長度必須6-12位！";
                    return false;
                }
                if (!Utils.UserNameRegex(base.q("userName")))
                {
                    message = "帳號必須包含字母和數字，支持‘_’，但開頭和結尾不能用‘_’！";
                    return false;
                }
                if (string.IsNullOrEmpty(base.q("userPassword").Trim()))
                {
                    message = "密碼 不能為空！";
                    return false;
                }
                if ((base.q("userPassword").Trim().Length < 8) || (base.q("userPassword").Trim().Length > 20))
                {
                    message = "密碼 最小長度必須 8位 或以上（最長20位）！";
                    return false;
                }
                flag = true;
                if (!string.IsNullOrEmpty(six))
                {
                    if (string.IsNullOrEmpty(base.q("userCredit_six").Trim()))
                    {
                        message = "⑥合彩: 信用額度 請務必輸入！";
                        return false;
                    }
                    try
                    {
                        if (int.Parse(base.q("userCredit_six").Trim()) < 0)
                        {
                            message = "⑥合彩:信用額度 必須大於等於0";
                            return false;
                        }
                    }
                    catch
                    {
                        message = "⑥合彩: 信用額度 只能為數字！";
                        return false;
                    }
                    if (string.IsNullOrEmpty(base.q("userRate_six").Trim()))
                    {
                        message = "⑥合彩: 分公司成數 不能為空！";
                        return false;
                    }
                    try
                    {
                        if (int.Parse(base.q("userRate_six").Trim()) > 100)
                        {
                            message = "⑥合彩: 分公司占成 不能大于100%！";
                            return false;
                        }
                        flag = true;
                    }
                    catch
                    {
                        message = "⑥合彩: 分公司成數 只能為數字！";
                        return false;
                    }
                }
                if (string.IsNullOrEmpty(kc))
                {
                    goto Label_1262;
                }
                if (string.IsNullOrEmpty(base.q("userCredit_kc").Trim()))
                {
                    message = "快彩: 信用額度 請務必輸入！";
                    return false;
                }
                try
                {
                    if (int.Parse(base.q("userCredit_kc").Trim()) < 0)
                    {
                        message = "快彩:信用額度 必須大於等於0";
                        return false;
                    }
                }
                catch
                {
                    message = "快彩: 信用額度 只能為數字！";
                    return false;
                }
                if (string.IsNullOrEmpty(base.q("userRate_kc").Trim()))
                {
                    message = "快彩: 分公司成數 不能為空！";
                    return false;
                }
                try
                {
                    if (int.Parse(base.q("userRate_kc").Trim()) > 100)
                    {
                        message = "快彩: 分公司占成 不能大于100%！";
                        return false;
                    }
                    flag = true;
                    goto Label_1262;
                }
                catch
                {
                    message = "快彩: 分公司成數 只能為數字！";
                    return false;
                }
            }
            if (str.Equals("zd"))
            {
                if (string.IsNullOrEmpty(base.q("sltupuser").Trim()))
                {
                    message = "上級分股東 不能為空！";
                    return false;
                }
                if (string.IsNullOrEmpty(base.q("userName").Trim()))
                {
                    message = "帳號 不能為空！";
                    return false;
                }
                if ((base.q("userName").Trim().Length < 6) || (base.q("userName").Trim().Length > 12))
                {
                    message = "帳號 最小長度必須6-12位！";
                    return false;
                }
                if (!Utils.UserNameRegex(base.q("userName")))
                {
                    message = "帳號必須包含字母和數字，支持‘_’，但開頭和結尾不能用‘_’！";
                    return false;
                }
                if (string.IsNullOrEmpty(base.q("userPassword").Trim()))
                {
                    message = "密碼 不能為空！";
                    return false;
                }
                if ((base.q("userPassword").Trim().Length < 8) || (base.q("userPassword").Trim().Length > 20))
                {
                    message = "密碼 最小長度必須 8位 或以上（最長20位）！";
                    return false;
                }
                flag = true;
                if (!string.IsNullOrEmpty(six))
                {
                    if (string.IsNullOrEmpty(base.q("userCredit_six").Trim()))
                    {
                        message = "⑥合彩: 信用額度 請務必輸入！";
                        return false;
                    }
                    try
                    {
                        if (int.Parse(base.q("userCredit_six").Trim()) < 0)
                        {
                            message = "⑥合彩:信用額度 必須大於等於0";
                            return false;
                        }
                    }
                    catch
                    {
                        message = "⑥合彩: 信用額度 只能為數字！";
                        return false;
                    }
                    if (string.IsNullOrEmpty(base.q("userRate_six").Trim()))
                    {
                        message = "⑥合彩: 股東成數 不能為空！";
                        return false;
                    }
                    try
                    {
                        if (int.Parse(base.q("userRate_six").Trim()) > 100)
                        {
                            message = "⑥合彩: 股東占成 不能大于100%！";
                            return false;
                        }
                        flag = true;
                    }
                    catch
                    {
                        message = "⑥合彩: 股東成數 只能為數字！";
                        return false;
                    }
                }
                if (string.IsNullOrEmpty(kc))
                {
                    goto Label_1262;
                }
                if (string.IsNullOrEmpty(base.q("userCredit_kc").Trim()))
                {
                    message = "快彩: 信用額度 請務必輸入！";
                    return false;
                }
                try
                {
                    if (int.Parse(base.q("userCredit_kc").Trim()) < 0)
                    {
                        message = "快彩:信用額度 必須大於等於0";
                        return false;
                    }
                }
                catch
                {
                    message = "快彩: 信用額度 只能為數字！";
                    return false;
                }
                if (string.IsNullOrEmpty(base.q("userRate_kc").Trim()))
                {
                    message = "快彩: 股東成數 不能為空！";
                    return false;
                }
                try
                {
                    if (int.Parse(base.q("userRate_kc").Trim()) > 100)
                    {
                        message = "快彩: 股東占成 不能大于100%！";
                        return false;
                    }
                    flag = true;
                    goto Label_1262;
                }
                catch
                {
                    message = "快彩: 股東成數 只能為數字！";
                    return false;
                }
            }
            if (!str.Equals("dl"))
            {
                goto Label_0B6C;
            }
            if (string.IsNullOrEmpty(base.q("sltupuser").Trim()))
            {
                message = "上級總代 不能為空！";
                return false;
            }
            if (string.IsNullOrEmpty(base.q("userName").Trim()))
            {
                message = "帳號 不能為空！";
                return false;
            }
            if ((base.q("userName").Trim().Length < 6) || (base.q("userName").Trim().Length > 12))
            {
                message = "帳號 最小長度必須6-12位！";
                return false;
            }
            if (!Utils.UserNameRegex(base.q("userName")))
            {
                message = "帳號必須包含字母和數字，支持‘_’，但開頭和結尾不能用‘_’！";
                return false;
            }
            if (string.IsNullOrEmpty(base.q("userPassword").Trim()))
            {
                message = "密碼 不能為空！";
                return false;
            }
            if ((base.q("userPassword").Trim().Length < 8) || (base.q("userPassword").Trim().Length > 20))
            {
                message = "密碼 最小長度必須 8位 或以上（最長20位）！";
                return false;
            }
            if (!LSRequest.qq("sltlimithy").ToLower().Trim().Equals("0") && !LSRequest.qq("sltlimithy").ToLower().Trim().Equals("1"))
            {
                message = "代理下綫人數上限 錯誤！";
                return false;
            }
            if (LSRequest.qq("sltlimithy").ToLower().Trim().Equals("1"))
            {
                if (string.IsNullOrEmpty(LSRequest.qq("txtlimithy").ToLower().Trim()))
                {
                    message = "代理下綫人數 不能為空！";
                    return false;
                }
                try
                {
                    if (int.Parse(LSRequest.qq("txtlimithy").ToLower().Trim()) <= 0)
                    {
                        message = "代理下綫人數 必須大於\"0\"！";
                        return false;
                    }
                    goto Label_09D7;
                }
                catch
                {
                    message = "代理下綫人數 只能為數字！";
                    return false;
                }
            }
            flag = true;
        Label_09D7:
            if (!string.IsNullOrEmpty(six))
            {
                if (string.IsNullOrEmpty(base.q("userCredit_six").Trim()))
                {
                    message = "⑥合彩: 信用額度 請務必輸入！";
                    return false;
                }
                try
                {
                    if (int.Parse(base.q("userCredit_six").Trim()) < 0)
                    {
                        message = "⑥合彩:信用額度 必須大於等於0";
                        return false;
                    }
                }
                catch
                {
                    message = "⑥合彩: 信用額度 只能為數字！";
                    return false;
                }
                if (string.IsNullOrEmpty(base.q("userRate_six").Trim()))
                {
                    message = "⑥合彩: 總代成數 不能為空！";
                    return false;
                }
                try
                {
                    if (int.Parse(base.q("userRate_six").Trim()) > 100)
                    {
                        message = "⑥合彩: 總代占成 不能大于100%！";
                        return false;
                    }
                    flag = true;
                }
                catch
                {
                    message = "⑥合彩: 總代成數 只能為數字！";
                    return false;
                }
            }
            if (string.IsNullOrEmpty(kc))
            {
                goto Label_1262;
            }
            if (string.IsNullOrEmpty(base.q("userCredit_kc").Trim()))
            {
                message = "快彩: 信用額度 請務必輸入！";
                return false;
            }
            try
            {
                if (int.Parse(base.q("userCredit_kc").Trim()) < 0)
                {
                    message = "快彩:信用額度 必須大於等於0";
                    return false;
                }
            }
            catch
            {
                message = "快彩: 信用額度 只能為數字！";
                return false;
            }
            if (string.IsNullOrEmpty(base.q("userRate_kc").Trim()))
            {
                message = "快彩: 總代成數 不能為空！";
                return false;
            }
            try
            {
                if (int.Parse(base.q("userRate_kc").Trim()) > 100)
                {
                    message = "快彩: 總代占成 不能大于100%！";
                    return false;
                }
                flag = true;
                goto Label_1262;
            }
            catch
            {
                message = "快彩: 總代成數 只能為數字！";
                return false;
            }
        Label_0B6C:
            if (str.Equals("hy"))
            {
                if (string.IsNullOrEmpty(base.q("sltupuser").Trim()))
                {
                    message = "上級代理 不能為空！";
                    return false;
                }
                if (string.IsNullOrEmpty(base.q("userName").Trim()))
                {
                    message = "帳號 不能為空！";
                    return false;
                }
                if ((base.q("userName").Trim().Length < 6) || (base.q("userName").Trim().Length > 12))
                {
                    message = "帳號 最小長度必須6-12位！";
                    return false;
                }
                if (!Utils.UserNameRegex(base.q("userName")))
                {
                    message = "帳號必須包含字母和數字，支持‘_’，但開頭和結尾不能用‘_’！";
                    return false;
                }
                if (string.IsNullOrEmpty(base.q("userPassword").Trim()))
                {
                    message = "密碼 不能為空！";
                    return false;
                }
                if ((base.q("userPassword").Trim().Length < 8) || (base.q("userPassword").Trim().Length > 20))
                {
                    message = "密碼 最小長度必須 8位 或以上（最長20位）！";
                    return false;
                }
                flag = true;
                if (!string.IsNullOrEmpty(six))
                {
                    if (string.IsNullOrEmpty(base.q("userCredit_six").Trim()))
                    {
                        message = "⑥合彩: 信用額度 請務必輸入！";
                        return false;
                    }
                    try
                    {
                        if (int.Parse(base.q("userCredit_six").Trim()) < 0)
                        {
                            message = "⑥合彩:信用額度 必須大於等於0";
                            return false;
                        }
                    }
                    catch
                    {
                        message = "⑥合彩: 信用額度 只能為數字！";
                        return false;
                    }
                    if (string.IsNullOrEmpty(base.q("userRate_six").Trim()))
                    {
                        message = "⑥合彩: 代理成數 不能為空！";
                        return false;
                    }
                    try
                    {
                        if (int.Parse(base.q("userRate_six").Trim()) > 100)
                        {
                            message = "⑥合彩: 代理占成 不能大于100%！";
                            return false;
                        }
                        flag = true;
                    }
                    catch
                    {
                        message = "⑥合彩: 代理成數 只能為數字！";
                        return false;
                    }
                }
                if (string.IsNullOrEmpty(kc))
                {
                    goto Label_1262;
                }
                if (string.IsNullOrEmpty(base.q("userCredit_kc").Trim()))
                {
                    message = "快彩: 信用額度 請務必輸入！";
                    return false;
                }
                try
                {
                    if (int.Parse(base.q("userCredit_kc").Trim()) < 0)
                    {
                        message = "快彩:信用額度 必須大於等於0";
                        return false;
                    }
                }
                catch
                {
                    message = "快彩: 信用額度 只能為數字！";
                    return false;
                }
                if (string.IsNullOrEmpty(base.q("userRate_kc").Trim()))
                {
                    message = "快彩: 代理成數 不能為空！";
                    return false;
                }
                try
                {
                    if (int.Parse(base.q("userRate_kc").Trim()) > 100)
                    {
                        message = "快彩: 代理占成 不能大于100%！";
                        return false;
                    }
                    flag = true;
                    goto Label_1262;
                }
                catch
                {
                    message = "快彩: 代理成數 只能為數字！";
                    return false;
                }
            }
            if (str.Equals("hydu"))
            {
                if (string.IsNullOrEmpty(base.q("rdoutype").Trim()))
                {
                    message = "直屬上級 不能為空！";
                    return false;
                }
                string str2 = "";
                string str3 = LSRequest.qq("rdoutype").Trim();
                if (str3 != null)
                {
                    if (!(str3 == "fgs"))
                    {
                        if (str3 == "gd")
                        {
                            str2 = "股東";
                        }
                        else if (str3 == "zd")
                        {
                            str2 = "總代";
                        }
                    }
                    else
                    {
                        str2 = "分公司";
                    }
                }
                if (string.IsNullOrEmpty(base.q("sltupuser").Trim()))
                {
                    message = string.Format("上級{0} 不能為空！", str2);
                    return false;
                }
                if (string.IsNullOrEmpty(base.q("userName").Trim()))
                {
                    message = "帳號 不能為空！";
                    return false;
                }
                if ((base.q("userName").Trim().Length < 6) || (base.q("userName").Trim().Length > 12))
                {
                    message = "帳號 最小長度必須6-12位！";
                    return false;
                }
                if (!Utils.UserNameRegex(base.q("userName")))
                {
                    message = "帳號必須包含字母和數字，支持‘_’，但開頭和結尾不能用‘_’！";
                    return false;
                }
                if (string.IsNullOrEmpty(base.q("userPassword").Trim()))
                {
                    message = "密碼 不能為空！";
                    return false;
                }
                if ((base.q("userPassword").Trim().Length < 8) || (base.q("userPassword").Trim().Length > 20))
                {
                    message = "密碼 最小長度必須 8位 或以上（最長20位）！";
                    return false;
                }
                flag = true;
                if (!string.IsNullOrEmpty(six))
                {
                    if (string.IsNullOrEmpty(base.q("userCredit_six").Trim()))
                    {
                        message = "⑥合彩: 信用額度 請務必輸入！";
                        return false;
                    }
                    try
                    {
                        if (int.Parse(base.q("userCredit_six").Trim()) < 0)
                        {
                            message = "⑥合彩:信用額度 必須大於等於0";
                            return false;
                        }
                    }
                    catch
                    {
                        message = "⑥合彩: 信用額度 只能為數字！";
                        return false;
                    }
                    if (string.IsNullOrEmpty(base.q("userRate_six").Trim()))
                    {
                        message = string.Format("⑥合彩: {0}成數 不能為空！", str2);
                        return false;
                    }
                    try
                    {
                        if (int.Parse(base.q("userRate_six").Trim()) > 100)
                        {
                            message = string.Format("⑥合彩: {0}占成 不能大于100%！", str2);
                            return false;
                        }
                        flag = true;
                    }
                    catch
                    {
                        message = string.Format("⑥合彩: {0}成數 只能為數字！", str2);
                        return false;
                    }
                }
                if (string.IsNullOrEmpty(kc))
                {
                    goto Label_1262;
                }
                if (string.IsNullOrEmpty(base.q("userCredit_kc").Trim()))
                {
                    message = "快彩: 信用額度 請務必輸入！";
                    return false;
                }
                try
                {
                    if (int.Parse(base.q("userCredit_kc").Trim()) < 0)
                    {
                        message = "快彩:信用額度 必須大於等於0";
                        return false;
                    }
                }
                catch
                {
                    message = "快彩: 信用額度 只能為數字！";
                    return false;
                }
                if (string.IsNullOrEmpty(base.q("userRate_kc").Trim()))
                {
                    message = string.Format("快彩: {0}成數 不能為空！", str2);
                    return false;
                }
                try
                {
                    if (int.Parse(base.q("userRate_kc").Trim()) > 100)
                    {
                        message = string.Format("快彩: {0}占成 不能大于100%！", str2);
                        return false;
                    }
                    flag = true;
                    goto Label_1262;
                }
                catch
                {
                    message = string.Format("快彩: {0}成數 只能為數字！", str2);
                    return false;
                }
            }
            if (str.Equals("child"))
            {
                if (string.IsNullOrEmpty(base.q("userName").Trim()))
                {
                    message = "帳號 不能為空！";
                    return false;
                }
                if ((base.q("userName").Trim().Length < 6) || (base.q("userName").Trim().Length > 12))
                {
                    message = "帳號 最小長度必須6-12位！";
                    return false;
                }
                if (!Utils.UserNameRegex(base.q("userName")))
                {
                    message = "帳號必須包含字母和數字，支持‘_’，但開頭和結尾不能用‘_’！";
                    return false;
                }
                if (string.IsNullOrEmpty(base.q("userPassword").Trim()))
                {
                    message = "密碼 不能為空！";
                    return false;
                }
                if ((base.q("userPassword").Trim().Length < 8) || (base.q("userPassword").Trim().Length > 20))
                {
                    message = "密碼 最小長度必須 8位 或以上（最長20位）！";
                    return false;
                }
                flag = true;
            }
            else if (str.Equals("filluser"))
            {
                if (string.IsNullOrEmpty(base.q("userName").Trim()))
                {
                    message = "帳號 不能為空！";
                    return false;
                }
            }
            else
            {
                message = "用戶類型不正確！";
                return false;
            }
        Label_1262:
            if (CallBLL.cz_users_bll.ExistName(LSRequest.qq("userName").Trim()))
            {
                message = "該用戶名已經存在！";
                return false;
            }
            return flag;
        }

        protected bool ValidParamByUserEdit(string utype, ref string message, object[] obj, string six, string kc)
        {
            string str = utype.ToLower();
            if (string.IsNullOrEmpty(str))
            {
                message = "用戶類型不正確！";
                return false;
            }
            bool flag = true;
            if (str.Equals("fgs"))
            {
                if (!string.IsNullOrEmpty(base.q("userPassword").Trim()))
                {
                    if ((base.q("userPassword").Trim().Length < 8) || (base.q("userPassword").Trim().Length > 20))
                    {
                        message = "密碼 最小長度必須 8位 或以上（最長20位）！";
                        return false;
                    }
                }
                else
                {
                    flag = true;
                }
                if (!string.IsNullOrEmpty(six))
                {
                    if (string.IsNullOrEmpty(base.q("userCredit_six").Trim()))
                    {
                        message = "⑥合彩: 信用額度 請務必輸入！";
                        return false;
                    }
                    try
                    {
                        if (int.Parse(base.q("userCredit_six").Trim()) < 0)
                        {
                            message = "⑥合彩:信用額度 必須大於等於0！";
                            return false;
                        }
                    }
                    catch
                    {
                        message = "⑥合彩: 信用額度 只能為數字！";
                        return false;
                    }
                    if (string.IsNullOrEmpty(base.q("userRate_six").Trim()))
                    {
                        message = "⑥合彩: 總監成數 不能為空！";
                        return false;
                    }
                    try
                    {
                        int num2 = int.Parse(base.q("userRate_six").Trim());
                        if (num2 < int.Parse(base.get_ZJMinRate_SIX()))
                        {
                            message = string.Format("⑥合彩:總監成數 不能小於{0}%！", base.get_ZJMinRate_SIX());
                            return false;
                        }
                        if (num2 > 100)
                        {
                            message = "⑥合彩: 總監占成 不能大于100%！";
                            return false;
                        }
                        flag = true;
                    }
                    catch
                    {
                        message = "⑥合彩: 總監成數 只能數字！";
                        return false;
                    }
                }
                if (string.IsNullOrEmpty(kc))
                {
                    goto Label_0B81;
                }
                if (string.IsNullOrEmpty(base.q("userCredit_kc").Trim()))
                {
                    message = "快彩: 信用額度 請務必輸入！";
                    return false;
                }
                try
                {
                    if (int.Parse(base.q("userCredit_kc").Trim()) < 0)
                    {
                        message = "快彩:信用額度 必須大於等於0！";
                        return false;
                    }
                }
                catch
                {
                    message = "快彩: 信用額度 只能為數字！";
                    return false;
                }
                if (string.IsNullOrEmpty(base.q("userRate_kc").Trim()))
                {
                    message = "快彩: 總監成數 不能為空！";
                    return false;
                }
                try
                {
                    int num4 = int.Parse(base.q("userRate_kc").Trim());
                    if (num4 < int.Parse(base.get_ZJMinRate_KC()))
                    {
                        message = string.Format("快彩:總監成數 不能小於{0}%！", base.get_ZJMinRate_KC());
                        return false;
                    }
                    if (num4 > 100)
                    {
                        message = "快彩: 總監占成 不能大于100%！";
                        return false;
                    }
                    flag = true;
                    goto Label_0B81;
                }
                catch
                {
                    message = "快彩: 總監成數 只能數字！";
                    return false;
                }
            }
            if (str.Equals("gd"))
            {
                if (!string.IsNullOrEmpty(base.q("userPassword").Trim()))
                {
                    if ((base.q("userPassword").Trim().Length < 8) || (base.q("userPassword").Trim().Length > 20))
                    {
                        message = "密碼 最小長度必須 8位 或以上（最長20位）！";
                        return false;
                    }
                }
                else
                {
                    flag = true;
                }
                if (!string.IsNullOrEmpty(six))
                {
                    if (string.IsNullOrEmpty(base.q("userCredit_six").Trim()))
                    {
                        message = "⑥合彩: 信用額度 請務必輸入！";
                        return false;
                    }
                    try
                    {
                        if (int.Parse(base.q("userCredit_six").Trim()) < 0)
                        {
                            message = "⑥合彩:信用額度 必須大於等於0！";
                            return false;
                        }
                    }
                    catch
                    {
                        message = "⑥合彩: 信用額度 只能為數字！";
                        return false;
                    }
                    if (string.IsNullOrEmpty(base.q("userRate_six").Trim()))
                    {
                        message = "⑥合彩: 分公司成數 不能為空！";
                        return false;
                    }
                    try
                    {
                        if (int.Parse(base.q("userRate_six").Trim()) > 100)
                        {
                            message = "⑥合彩: 分公司占成 不能大于100%！";
                            return false;
                        }
                        flag = true;
                    }
                    catch
                    {
                        message = "⑥合彩: 分公司成數 只能為數字！";
                        return false;
                    }
                }
                if (string.IsNullOrEmpty(kc))
                {
                    goto Label_0B81;
                }
                if (string.IsNullOrEmpty(base.q("userCredit_kc").Trim()))
                {
                    message = "快彩: 信用額度 請務必輸入！";
                    return false;
                }
                try
                {
                    if (int.Parse(base.q("userCredit_kc").Trim()) < 0)
                    {
                        message = "快彩:信用額度 必須大於等於0！";
                        return false;
                    }
                }
                catch
                {
                    message = "快彩: 信用額度 只能為數字！";
                    return false;
                }
                if (string.IsNullOrEmpty(base.q("userRate_kc").Trim()))
                {
                    message = "快彩: 分公司成數 不能為空！";
                    return false;
                }
                try
                {
                    if (int.Parse(base.q("userRate_kc").Trim()) > 100)
                    {
                        message = "快彩: 分公司占成 不能大于100%！";
                        return false;
                    }
                    flag = true;
                    goto Label_0B81;
                }
                catch
                {
                    message = "快彩: 分公司成數 只能為數字！";
                    return false;
                }
            }
            if (str.Equals("zd"))
            {
                if (!string.IsNullOrEmpty(base.q("userPassword").Trim()))
                {
                    if ((base.q("userPassword").Trim().Length < 8) || (base.q("userPassword").Trim().Length > 20))
                    {
                        message = "密碼 最小長度必須 8位 或以上（最長20位）！";
                        return false;
                    }
                }
                else
                {
                    flag = true;
                }
                if (!string.IsNullOrEmpty(six))
                {
                    if (string.IsNullOrEmpty(base.q("userCredit_six").Trim()))
                    {
                        message = "⑥合彩: 信用額度 請務必輸入！";
                        return false;
                    }
                    try
                    {
                        if (int.Parse(base.q("userCredit_six").Trim()) < 0)
                        {
                            message = "⑥合彩:信用額度 必須大於等於0！";
                            return false;
                        }
                    }
                    catch
                    {
                        message = "⑥合彩: 信用額度 只能為數字！";
                        return false;
                    }
                    if (string.IsNullOrEmpty(base.q("userRate_six").Trim()))
                    {
                        message = "⑥合彩: 股東成數 不能為空！";
                        return false;
                    }
                    try
                    {
                        if (int.Parse(base.q("userRate_six").Trim()) > 100)
                        {
                            message = "⑥合彩: 股東占成 不能大于100%！";
                            return false;
                        }
                        flag = true;
                    }
                    catch
                    {
                        message = "⑥合彩: 股東成數 只能為數字！";
                        return false;
                    }
                }
                if (string.IsNullOrEmpty(kc))
                {
                    goto Label_0B81;
                }
                if (string.IsNullOrEmpty(base.q("userCredit_kc").Trim()))
                {
                    message = "快彩: 信用額度 請務必輸入！";
                    return false;
                }
                try
                {
                    if (int.Parse(base.q("userCredit_kc").Trim()) < 0)
                    {
                        message = "快彩:信用額度 必須大於等於0！";
                        return false;
                    }
                }
                catch
                {
                    message = "快彩: 信用額度 只能為數字！";
                    return false;
                }
                if (string.IsNullOrEmpty(base.q("userRate_kc").Trim()))
                {
                    message = "快彩: 股東成數 不能為空！";
                    return false;
                }
                try
                {
                    if (int.Parse(base.q("userRate_kc").Trim()) > 100)
                    {
                        message = "快彩: 股東占成 不能大于100%！";
                        return false;
                    }
                    flag = true;
                    goto Label_0B81;
                }
                catch
                {
                    message = "快彩: 股東成數 只能為數字！";
                    return false;
                }
            }
            if (!str.Equals("dl"))
            {
                goto Label_0915;
            }
            if (!string.IsNullOrEmpty(base.q("userPassword").Trim()) && ((base.q("userPassword").Trim().Length < 8) || (base.q("userPassword").Trim().Length > 20)))
            {
                message = "密碼 最小長度必須 8位 或以上（最長20位）！";
                return false;
            }
            if (!LSRequest.qq("sltlimithy").ToLower().Trim().Equals("0") && !LSRequest.qq("sltlimithy").ToLower().Trim().Equals("1"))
            {
                message = "代理下綫人數上限 錯誤！";
                return false;
            }
            if (LSRequest.qq("sltlimithy").ToLower().Trim().Equals("1"))
            {
                if (string.IsNullOrEmpty(LSRequest.qq("txtlimithy").ToLower().Trim()))
                {
                    message = "代理下綫人數 不能為空！";
                    return false;
                }
                try
                {
                    if (int.Parse(LSRequest.qq("txtlimithy").ToLower().Trim()) <= 0)
                    {
                        message = "代理下綫人數 必須大於\"0\"！";
                        return false;
                    }
                    goto Label_0780;
                }
                catch
                {
                    message = "代理下綫人數 只能為數字！";
                    return false;
                }
            }
            flag = true;
        Label_0780:
            if (!string.IsNullOrEmpty(six))
            {
                if (string.IsNullOrEmpty(base.q("userCredit_six").Trim()))
                {
                    message = "⑥合彩: 信用額度 請務必輸入！";
                    return false;
                }
                try
                {
                    if (int.Parse(base.q("userCredit_six").Trim()) < 0)
                    {
                        message = "⑥合彩:信用額度 必須大於等於0！";
                        return false;
                    }
                }
                catch
                {
                    message = "⑥合彩: 信用額度 只能為數字！";
                    return false;
                }
                if (string.IsNullOrEmpty(base.q("userRate_six").Trim()))
                {
                    message = "⑥合彩: 總代成數 不能為空！";
                    return false;
                }
                try
                {
                    if (int.Parse(base.q("userRate_six").Trim()) > 100)
                    {
                        message = "⑥合彩: 總代占成 不能大于100%！";
                        return false;
                    }
                    flag = true;
                }
                catch
                {
                    message = "⑥合彩: 總代成數 只能為數字！";
                    return false;
                }
            }
            if (string.IsNullOrEmpty(kc))
            {
                goto Label_0B81;
            }
            if (string.IsNullOrEmpty(base.q("userCredit_kc").Trim()))
            {
                message = "快彩: 信用額度 請務必輸入！";
                return false;
            }
            try
            {
                if (int.Parse(base.q("userCredit_kc").Trim()) < 0)
                {
                    message = "快彩:信用額度 必須大於等於0！";
                    return false;
                }
            }
            catch
            {
                message = "快彩: 信用額度 只能為數字！";
                return false;
            }
            if (string.IsNullOrEmpty(base.q("userRate_kc").Trim()))
            {
                message = "快彩: 總代成數 不能為空！";
                return false;
            }
            try
            {
                if (int.Parse(base.q("userRate_kc").Trim()) > 100)
                {
                    message = "快彩: 總代占成 不能大于100%！";
                    return false;
                }
                flag = true;
                goto Label_0B81;
            }
            catch
            {
                message = "快彩: 總代成數 只能為數字！";
                return false;
            }
        Label_0915:
            if (str.Equals("hy"))
            {
                if (!string.IsNullOrEmpty(base.q("userPassword").Trim()))
                {
                    if ((base.q("userPassword").Trim().Length < 8) || (base.q("userPassword").Trim().Length > 20))
                    {
                        message = "密碼 最小長度必須 8位 或以上（最長20位）！";
                        return false;
                    }
                }
                else
                {
                    flag = true;
                }
                if (!string.IsNullOrEmpty(six))
                {
                    if (string.IsNullOrEmpty(base.q("userCredit_six").Trim()))
                    {
                        message = "⑥合彩: 信用額度 請務必輸入！";
                        return false;
                    }
                    try
                    {
                        if (int.Parse(base.q("userCredit_six").Trim()) < 0)
                        {
                            message = "⑥合彩:信用額度 必須大於等於0！";
                            return false;
                        }
                    }
                    catch
                    {
                        message = "⑥合彩: 信用額度 只能為數字！";
                        return false;
                    }
                    if (string.IsNullOrEmpty(base.q("userRate_six").Trim()))
                    {
                        message = "⑥合彩: 代理成數 不能為空！";
                        return false;
                    }
                    try
                    {
                        if (int.Parse(base.q("userRate_six").Trim()) > 100)
                        {
                            message = "⑥合彩: 代理占成 不能大于100%！";
                            return false;
                        }
                        flag = true;
                    }
                    catch
                    {
                        message = "⑥合彩: 代理成數 只能為數字！";
                        return false;
                    }
                }
                if (string.IsNullOrEmpty(kc))
                {
                    goto Label_0B81;
                }
                if (string.IsNullOrEmpty(base.q("userCredit_kc").Trim()))
                {
                    message = "快彩: 信用額度 請務必輸入！";
                    return false;
                }
                try
                {
                    if (int.Parse(base.q("userCredit_kc").Trim()) < 0)
                    {
                        message = "快彩:信用額度 必須大於等於0！";
                        return false;
                    }
                }
                catch
                {
                    message = "快彩: 信用額度 只能為數字！";
                    return false;
                }
                if (string.IsNullOrEmpty(base.q("userRate_kc").Trim()))
                {
                    message = "快彩: 代理成數 不能為空！";
                    return false;
                }
                try
                {
                    if (int.Parse(base.q("userRate_kc").Trim()) > 100)
                    {
                        message = "快彩: 代理占成 不能大于100%！";
                        return false;
                    }
                    flag = true;
                    goto Label_0B81;
                }
                catch
                {
                    message = "快彩: 代理成數 只能為數字！";
                    return false;
                }
            }
            if (str.Equals("child"))
            {
                if (!string.IsNullOrEmpty(base.q("userPassword").Trim()) && ((base.q("userPassword").Trim().Length < 8) || (base.q("userPassword").Trim().Length > 20)))
                {
                    message = "密碼 最小長度必須 8位 或以上（最長20位）！";
                    return false;
                }
            }
            else if (!str.Equals("filluser"))
            {
                message = "用戶類型不正確！";
                return false;
            }
        Label_0B81:
            if (CallBLL.cz_users_bll.ExistName(LSRequest.qq("userName").Trim()))
            {
                message = "已經存該用戶名！";
                return false;
            }
            return flag;
        }

        protected bool ValidParamByUserEditPhone(string utype, ref string message, object[] obj, string six, string kc)
        {
            string str = utype.ToLower();
            if (string.IsNullOrEmpty(str))
            {
                message = "用戶類型不正確！";
                return false;
            }
            bool flag = true;
            if (str.Equals("fgs"))
            {
                if (!string.IsNullOrEmpty(base.q("password").Trim()))
                {
                    if ((base.q("password").Trim().Length < 8) || (base.q("password").Trim().Length > 20))
                    {
                        message = "密碼 最小長度必須 8位 或以上（最長20位）！";
                        return false;
                    }
                }
                else
                {
                    flag = true;
                }
                if (!string.IsNullOrEmpty(six))
                {
                    if (string.IsNullOrEmpty(base.q("userCreditSix").Trim()))
                    {
                        message = "⑥合彩: 信用額度 請務必輸入！";
                        return false;
                    }
                    try
                    {
                        if (int.Parse(base.q("userCreditSix").Trim()) < 0)
                        {
                            message = "⑥合彩:信用額度 必須大於等於0！";
                            return false;
                        }
                    }
                    catch
                    {
                        message = "⑥合彩: 信用額度 只能為數字！";
                        return false;
                    }
                    if (string.IsNullOrEmpty(base.q("userRateSix").Trim()))
                    {
                        message = "⑥合彩: 總監成數 不能為空！";
                        return false;
                    }
                    try
                    {
                        int num2 = int.Parse(base.q("userRateSix").Trim());
                        if (num2 < int.Parse(base.get_ZJMinRate_SIX()))
                        {
                            message = string.Format("⑥合彩:總監成數 不能小於{0}%！", base.get_ZJMinRate_SIX());
                            return false;
                        }
                        if (num2 > 100)
                        {
                            message = "⑥合彩: 總監占成 不能大于100%！";
                            return false;
                        }
                        flag = true;
                    }
                    catch
                    {
                        message = "⑥合彩: 總監成數 只能數字！";
                        return false;
                    }
                }
                if (string.IsNullOrEmpty(kc))
                {
                    goto Label_0B79;
                }
                if (string.IsNullOrEmpty(base.q("userCreditKc").Trim()))
                {
                    message = "快彩: 信用額度 請務必輸入！";
                    return false;
                }
                try
                {
                    if (int.Parse(base.q("userCreditKc").Trim()) < 0)
                    {
                        message = "快彩:信用額度 必須大於等於0！";
                        return false;
                    }
                }
                catch
                {
                    message = "快彩: 信用額度 只能為數字！";
                    return false;
                }
                if (string.IsNullOrEmpty(base.q("userRateKc").Trim()))
                {
                    message = "快彩: 總監成數 不能為空！";
                    return false;
                }
                try
                {
                    int num4 = int.Parse(base.q("userRateKc").Trim());
                    if (num4 < int.Parse(base.get_ZJMinRate_KC()))
                    {
                        message = string.Format("快彩:總監成數 不能小於{0}%！", base.get_ZJMinRate_KC());
                        return false;
                    }
                    if (num4 > 100)
                    {
                        message = "快彩: 總監占成 不能大于100%！";
                        return false;
                    }
                    flag = true;
                    goto Label_0B79;
                }
                catch
                {
                    message = "快彩: 總監成數 只能數字！";
                    return false;
                }
            }
            if (str.Equals("gd"))
            {
                if (!string.IsNullOrEmpty(base.q("password").Trim()))
                {
                    if ((base.q("password").Trim().Length < 8) || (base.q("password").Trim().Length > 20))
                    {
                        message = "密碼 最小長度必須 8位 或以上（最長20位）！";
                        return false;
                    }
                }
                else
                {
                    flag = true;
                }
                if (!string.IsNullOrEmpty(six))
                {
                    if (string.IsNullOrEmpty(base.q("userCreditSix").Trim()))
                    {
                        message = "⑥合彩: 信用額度 請務必輸入！";
                        return false;
                    }
                    try
                    {
                        if (int.Parse(base.q("userCreditSix").Trim()) < 0)
                        {
                            message = "⑥合彩:信用額度 必須大於等於0！";
                            return false;
                        }
                    }
                    catch
                    {
                        message = "⑥合彩: 信用額度 只能為數字！";
                        return false;
                    }
                    if (string.IsNullOrEmpty(base.q("userRateSix").Trim()))
                    {
                        message = "⑥合彩: 分公司成數 不能為空！";
                        return false;
                    }
                    try
                    {
                        if (int.Parse(base.q("userRateSix").Trim()) > 100)
                        {
                            message = "⑥合彩: 分公司占成 不能大于100%！";
                            return false;
                        }
                        flag = true;
                    }
                    catch
                    {
                        message = "⑥合彩: 分公司成數 只能為數字！";
                        return false;
                    }
                }
                if (string.IsNullOrEmpty(kc))
                {
                    goto Label_0B79;
                }
                if (string.IsNullOrEmpty(base.q("userCreditKc").Trim()))
                {
                    message = "快彩: 信用額度 請務必輸入！";
                    return false;
                }
                try
                {
                    if (int.Parse(base.q("userCreditKc").Trim()) < 0)
                    {
                        message = "快彩:信用額度 必須大於等於0！";
                        return false;
                    }
                }
                catch
                {
                    message = "快彩: 信用額度 只能為數字！";
                    return false;
                }
                if (string.IsNullOrEmpty(base.q("userRateKc").Trim()))
                {
                    message = "快彩: 分公司成數 不能為空！";
                    return false;
                }
                try
                {
                    if (int.Parse(base.q("userRateKc").Trim()) > 100)
                    {
                        message = "快彩: 分公司占成 不能大于100%！";
                        return false;
                    }
                    flag = true;
                    goto Label_0B79;
                }
                catch
                {
                    message = "快彩: 分公司成數 只能為數字！";
                    return false;
                }
            }
            if (str.Equals("zd"))
            {
                if (!string.IsNullOrEmpty(base.q("password").Trim()))
                {
                    if ((base.q("password").Trim().Length < 8) || (base.q("password").Trim().Length > 20))
                    {
                        message = "密碼 最小長度必須 8位 或以上（最長20位）！";
                        return false;
                    }
                }
                else
                {
                    flag = true;
                }
                if (!string.IsNullOrEmpty(six))
                {
                    if (string.IsNullOrEmpty(base.q("userCreditSix").Trim()))
                    {
                        message = "⑥合彩: 信用額度 請務必輸入！";
                        return false;
                    }
                    try
                    {
                        if (int.Parse(base.q("userCreditSix").Trim()) < 0)
                        {
                            message = "⑥合彩:信用額度 必須大於等於0！";
                            return false;
                        }
                    }
                    catch
                    {
                        message = "⑥合彩: 信用額度 只能為數字！";
                        return false;
                    }
                    if (string.IsNullOrEmpty(base.q("userRateSix").Trim()))
                    {
                        message = "⑥合彩: 股東成數 不能為空！";
                        return false;
                    }
                    try
                    {
                        if (int.Parse(base.q("userRateSix").Trim()) > 100)
                        {
                            message = "⑥合彩: 股東占成 不能大于100%！";
                            return false;
                        }
                        flag = true;
                    }
                    catch
                    {
                        message = "⑥合彩: 股東成數 只能為數字！";
                        return false;
                    }
                }
                if (string.IsNullOrEmpty(kc))
                {
                    goto Label_0B79;
                }
                if (string.IsNullOrEmpty(base.q("userCreditKc").Trim()))
                {
                    message = "快彩: 信用額度 請務必輸入！";
                    return false;
                }
                try
                {
                    if (int.Parse(base.q("userCreditKc").Trim()) < 0)
                    {
                        message = "快彩:信用額度 必須大於等於0！";
                        return false;
                    }
                }
                catch
                {
                    message = "快彩: 信用額度 只能為數字！";
                    return false;
                }
                if (string.IsNullOrEmpty(base.q("userRateKc").Trim()))
                {
                    message = "快彩: 股東成數 不能為空！";
                    return false;
                }
                try
                {
                    if (int.Parse(base.q("userRateKc").Trim()) > 100)
                    {
                        message = "快彩: 股東占成 不能大于100%！";
                        return false;
                    }
                    flag = true;
                    goto Label_0B79;
                }
                catch
                {
                    message = "快彩: 股東成數 只能為數字！";
                    return false;
                }
            }
            if (!str.Equals("dl"))
            {
                goto Label_090D;
            }
            if (!string.IsNullOrEmpty(base.q("password").Trim()) && ((base.q("password").Trim().Length < 8) || (base.q("password").Trim().Length > 20)))
            {
                message = "密碼 最小長度必須 8位 或以上（最長20位）！";
                return false;
            }
            if (!LSRequest.qq("sltlimithy").ToLower().Trim().Equals("0") && !LSRequest.qq("sltlimithy").ToLower().Trim().Equals("1"))
            {
                message = "代理下綫人數上限 錯誤！";
                return false;
            }
            if (LSRequest.qq("sltlimithy").ToLower().Trim().Equals("1"))
            {
                if (string.IsNullOrEmpty(LSRequest.qq("txtlimithy").ToLower().Trim()))
                {
                    message = "代理下綫人數 不能為空！";
                    return false;
                }
                try
                {
                    if (int.Parse(LSRequest.qq("txtlimithy").ToLower().Trim()) <= 0)
                    {
                        message = "代理下綫人數 必須大於\"0\"！";
                    }
                    goto Label_0778;
                }
                catch
                {
                    message = "代理下綫人數 只能為數字！";
                    return false;
                }
            }
            flag = true;
        Label_0778:
            if (!string.IsNullOrEmpty(six))
            {
                if (string.IsNullOrEmpty(base.q("userCreditSix").Trim()))
                {
                    message = "⑥合彩: 信用額度 請務必輸入！";
                    return false;
                }
                try
                {
                    if (int.Parse(base.q("userCreditSix").Trim()) < 0)
                    {
                        message = "⑥合彩:信用額度 必須大於等於0！";
                        return false;
                    }
                }
                catch
                {
                    message = "⑥合彩: 信用額度 只能為數字！";
                    return false;
                }
                if (string.IsNullOrEmpty(base.q("userRateSix").Trim()))
                {
                    message = "⑥合彩: 總代成數 不能為空！";
                    return false;
                }
                try
                {
                    if (int.Parse(base.q("userRateSix").Trim()) > 100)
                    {
                        message = "⑥合彩: 總代占成 不能大于100%！";
                        return false;
                    }
                    flag = true;
                }
                catch
                {
                    message = "⑥合彩: 總代成數 只能為數字！";
                    return false;
                }
            }
            if (string.IsNullOrEmpty(kc))
            {
                goto Label_0B79;
            }
            if (string.IsNullOrEmpty(base.q("userCreditKc").Trim()))
            {
                message = "快彩: 信用額度 請務必輸入！";
                return false;
            }
            try
            {
                if (int.Parse(base.q("userCreditKc").Trim()) < 0)
                {
                    message = "快彩:信用額度 必須大於等於0！";
                    return false;
                }
            }
            catch
            {
                message = "快彩: 信用額度 只能為數字！";
                return false;
            }
            if (string.IsNullOrEmpty(base.q("userRateKc").Trim()))
            {
                message = "快彩: 總代成數 不能為空！";
                return false;
            }
            try
            {
                if (int.Parse(base.q("userRateKc").Trim()) > 100)
                {
                    message = "快彩: 總代占成 不能大于100%！";
                    return false;
                }
                flag = true;
                goto Label_0B79;
            }
            catch
            {
                message = "快彩: 總代成數 只能為數字！";
                return false;
            }
        Label_090D:
            if (str.Equals("hy"))
            {
                if (!string.IsNullOrEmpty(base.q("password").Trim()))
                {
                    if ((base.q("password").Trim().Length < 8) || (base.q("password").Trim().Length > 20))
                    {
                        message = "密碼 最小長度必須 8位 或以上（最長20位）！";
                        return false;
                    }
                }
                else
                {
                    flag = true;
                }
                if (!string.IsNullOrEmpty(six))
                {
                    if (string.IsNullOrEmpty(base.q("userCreditSix").Trim()))
                    {
                        message = "⑥合彩: 信用額度 請務必輸入！";
                        return false;
                    }
                    try
                    {
                        if (int.Parse(base.q("userCreditSix").Trim()) < 0)
                        {
                            message = "⑥合彩:信用額度 必須大於等於0！";
                            return false;
                        }
                    }
                    catch
                    {
                        message = "⑥合彩: 信用額度 只能為數字！";
                        return false;
                    }
                    if (string.IsNullOrEmpty(base.q("userRateSix").Trim()))
                    {
                        message = "⑥合彩: 代理成數 不能為空！";
                        return false;
                    }
                    try
                    {
                        if (int.Parse(base.q("userRateSix").Trim()) > 100)
                        {
                            message = "⑥合彩: 代理占成 不能大于100%！";
                            return false;
                        }
                        flag = true;
                    }
                    catch
                    {
                        message = "⑥合彩: 代理成數 只能為數字！";
                        return false;
                    }
                }
                if (string.IsNullOrEmpty(kc))
                {
                    goto Label_0B79;
                }
                if (string.IsNullOrEmpty(base.q("userCreditKc").Trim()))
                {
                    message = "快彩: 信用額度 請務必輸入！";
                    return false;
                }
                try
                {
                    if (int.Parse(base.q("userCreditKc").Trim()) < 0)
                    {
                        message = "快彩:信用額度 必須大於等於0！";
                        return false;
                    }
                }
                catch
                {
                    message = "快彩: 信用額度 只能為數字！";
                    return false;
                }
                if (string.IsNullOrEmpty(base.q("userRateKc").Trim()))
                {
                    message = "快彩: 代理成數 不能為空！";
                    return false;
                }
                try
                {
                    if (int.Parse(base.q("userRateKc").Trim()) > 100)
                    {
                        message = "快彩: 代理占成 不能大于100%！";
                        return false;
                    }
                    flag = true;
                    goto Label_0B79;
                }
                catch
                {
                    message = "快彩: 代理成數 只能為數字！";
                    return false;
                }
            }
            if (str.Equals("child"))
            {
                if (!string.IsNullOrEmpty(base.q("password").Trim()) && ((base.q("password").Trim().Length < 8) || (base.q("password").Trim().Length > 20)))
                {
                    message = "密碼 最小長度必須 8位 或以上（最長20位）！";
                    return false;
                }
            }
            else if (!str.Equals("filluser"))
            {
                message = "用戶類型不正確！";
                return false;
            }
        Label_0B79:
            if (CallBLL.cz_users_bll.ExistName(LSRequest.qq("userName").Trim()))
            {
                message = "已經存該用戶名！";
                return false;
            }
            return flag;
        }

        public void vrcar_kjj_log(DataTable oldTable, DataTable newTable, string operate_type)
        {
            string str = this.get_master_name();
            string str2 = this.get_children_name();
            string gameNameByID = base.GetGameNameByID(0x12.ToString());
            string category = "";
            string str5 = "";
            string str6 = "";
            string s = "";
            string str8 = "";
            int num = 0;
            string note = "快捷鍵微调" + str6;
            string act = "";
            int num2 = Convert.ToInt32((LSEnums.LogTypeID) 0);
            if (operate_type.Equals("7"))
            {
                note = "快捷鍵手工输入赔率值,微調" + str6;
            }
            string str11 = "";
            DataTable table = CallBLL.cz_phase_vrcar_bll.IsOpenPhaseByTime(DateTime.Now);
            if (table != null)
            {
                str11 = table.Rows[0]["phase"].ToString();
            }
            List<CommandInfo> list = new List<CommandInfo>();
            int num3 = 0;
            foreach (DataRow row in oldTable.Rows)
            {
                category = row["category"].ToString();
                str5 = row["play_name"].ToString();
                str6 = row["put_amount"].ToString();
                s = row["current_odds"].ToString();
                str8 = newTable.Rows[num3]["current_odds"].ToString();
                num = int.Parse(row["odds_id"].ToString());
                if (s != str8)
                {
                    if (double.Parse(s) > double.Parse(str8))
                    {
                        act = "降賠率";
                    }
                    else
                    {
                        act = "升賠率";
                    }
                    List<SqlParameter> paramList = new List<SqlParameter>();
                    string str12 = this.add_sys_log(str, str2, category, str5, str6, gameNameByID, str11, act, num, s, str8, note, num2, 0x12, ref paramList);
                    CommandInfo item = new CommandInfo {
                        CommandText = str12,
                        Parameters = paramList.ToArray()
                    };
                    list.Add(item);
                }
                num3++;
            }
            if (list.Count > 0)
            {
                CallBLL.cz_system_log_bll.execute_sql_tran(list);
            }
        }

        public void vrcar_log(cz_odds_vrcar oldModel, cz_odds_vrcar newModel, string operate_type)
        {
            string str = this.get_master_name();
            string str2 = this.get_children_name();
            string gameNameByID = base.GetGameNameByID(0x12.ToString());
            string category = oldModel.get_category();
            string str5 = oldModel.get_play_name();
            string str6 = oldModel.get_put_amount();
            string s = oldModel.get_current_odds();
            string str8 = newModel.get_current_odds();
            int num = oldModel.get_odds_id();
            string note = "微調" + str6;
            string act = "";
            int num2 = Convert.ToInt32((LSEnums.LogTypeID) 0);
            if (operate_type.Equals("3"))
            {
                note = "手工输入赔率值,微調" + str6;
            }
            string str11 = "";
            DataTable table = CallBLL.cz_phase_vrcar_bll.IsOpenPhaseByTime(DateTime.Now);
            if (table != null)
            {
                str11 = table.Rows[0]["phase"].ToString();
            }
            if (s != str8)
            {
                if (double.Parse(s) > double.Parse(str8))
                {
                    act = "降賠率";
                }
                else
                {
                    act = "升賠率";
                }
                List<SqlParameter> paramList = new List<SqlParameter>();
                string str12 = this.add_sys_log(str, str2, category, str5, str6, gameNameByID, str11, act, num, s, str8, note, num2, 0x12, ref paramList);
                CallBLL.cz_system_log_bll.executte_sql(str12, paramList.ToArray());
            }
        }

        public void vrssc_kjj_log(DataTable oldTable, DataTable newTable, string operate_type)
        {
            string str = this.get_master_name();
            string str2 = this.get_children_name();
            string gameNameByID = base.GetGameNameByID(0x13.ToString());
            string category = "";
            string str5 = "";
            string str6 = "";
            string s = "";
            string str8 = "";
            int num = 0;
            string note = "快捷鍵微调" + str6;
            string act = "";
            int num2 = Convert.ToInt32((LSEnums.LogTypeID) 0);
            if (operate_type.Equals("7"))
            {
                note = "快捷鍵手工输入赔率值,微調" + str6;
            }
            string str11 = "";
            DataTable table = CallBLL.cz_phase_vrssc_bll.IsOpenPhaseByTime(DateTime.Now);
            if (table != null)
            {
                str11 = table.Rows[0]["phase"].ToString();
            }
            List<CommandInfo> list = new List<CommandInfo>();
            int num3 = 0;
            foreach (DataRow row in oldTable.Rows)
            {
                category = row["category"].ToString();
                str5 = row["play_name"].ToString();
                str6 = row["put_amount"].ToString();
                s = row["current_odds"].ToString();
                str8 = newTable.Rows[num3]["current_odds"].ToString();
                num = int.Parse(row["odds_id"].ToString());
                if (s != str8)
                {
                    if (double.Parse(s) > double.Parse(str8))
                    {
                        act = "降賠率";
                    }
                    else
                    {
                        act = "升賠率";
                    }
                    List<SqlParameter> paramList = new List<SqlParameter>();
                    string str12 = this.add_sys_log(str, str2, category, str5, str6, gameNameByID, str11, act, num, s, str8, note, num2, 0x13, ref paramList);
                    CommandInfo item = new CommandInfo {
                        CommandText = str12,
                        Parameters = paramList.ToArray()
                    };
                    list.Add(item);
                }
                num3++;
            }
            if (list.Count > 0)
            {
                CallBLL.cz_system_log_bll.execute_sql_tran(list);
            }
        }

        public void vrssc_log(cz_odds_vrssc oldModel, cz_odds_vrssc newModel, string operate_type)
        {
            string str = this.get_master_name();
            string str2 = this.get_children_name();
            string gameNameByID = base.GetGameNameByID(0x13.ToString());
            string category = oldModel.get_category();
            string str5 = oldModel.get_play_name();
            string str6 = oldModel.get_put_amount();
            string s = oldModel.get_current_odds();
            string str8 = newModel.get_current_odds();
            int num = oldModel.get_odds_id();
            string note = "微調" + str6;
            string act = "";
            int num2 = Convert.ToInt32((LSEnums.LogTypeID) 0);
            if (operate_type.Equals("3"))
            {
                note = "手工输入赔率值,微調" + str6;
            }
            string str11 = "";
            DataTable table = CallBLL.cz_phase_vrssc_bll.IsOpenPhaseByTime(DateTime.Now);
            if (table != null)
            {
                str11 = table.Rows[0]["phase"].ToString();
            }
            if (s != str8)
            {
                if (double.Parse(s) > double.Parse(str8))
                {
                    act = "降賠率";
                }
                else
                {
                    act = "升賠率";
                }
                List<SqlParameter> paramList = new List<SqlParameter>();
                string str12 = this.add_sys_log(str, str2, category, str5, str6, gameNameByID, str11, act, num, s, str8, note, num2, 0x13, ref paramList);
                CallBLL.cz_system_log_bll.executte_sql(str12, paramList.ToArray());
            }
        }

        public void xyft5_kjj_log(DataTable oldTable, DataTable newTable, string operate_type)
        {
            string str = this.get_master_name();
            string str2 = this.get_children_name();
            string gameNameByID = base.GetGameNameByID(9.ToString());
            string category = "";
            string str5 = "";
            string str6 = "";
            string s = "";
            string str8 = "";
            int num = 0;
            string note = "快捷鍵微调" + str6;
            string act = "";
            int num2 = Convert.ToInt32((LSEnums.LogTypeID) 0);
            if (operate_type.Equals("7"))
            {
                note = "快捷鍵手工输入赔率值,微調" + str6;
            }
            string str11 = "";
            DataTable table = CallBLL.cz_phase_xyft5_bll.IsOpenPhaseByTime(DateTime.Now);
            if (table != null)
            {
                str11 = table.Rows[0]["phase"].ToString();
            }
            List<CommandInfo> list = new List<CommandInfo>();
            int num3 = 0;
            foreach (DataRow row in oldTable.Rows)
            {
                category = row["category"].ToString();
                str5 = row["play_name"].ToString();
                str6 = row["put_amount"].ToString();
                s = row["current_odds"].ToString();
                str8 = newTable.Rows[num3]["current_odds"].ToString();
                num = int.Parse(row["odds_id"].ToString());
                if (s != str8)
                {
                    if (double.Parse(s) > double.Parse(str8))
                    {
                        act = "降賠率";
                    }
                    else
                    {
                        act = "升賠率";
                    }
                    List<SqlParameter> paramList = new List<SqlParameter>();
                    string str12 = this.add_sys_log(str, str2, category, str5, str6, gameNameByID, str11, act, num, s, str8, note, num2, 9, ref paramList);
                    CommandInfo item = new CommandInfo {
                        CommandText = str12,
                        Parameters = paramList.ToArray()
                    };
                    list.Add(item);
                }
                num3++;
            }
            if (list.Count > 0)
            {
                CallBLL.cz_system_log_bll.execute_sql_tran(list);
            }
        }

        public void xyft5_log(cz_odds_xyft5 oldModel, cz_odds_xyft5 newModel, string operate_type)
        {
            string str = this.get_master_name();
            string str2 = this.get_children_name();
            string gameNameByID = base.GetGameNameByID(9.ToString());
            string category = oldModel.get_category();
            string str5 = oldModel.get_play_name();
            string str6 = oldModel.get_put_amount();
            string s = oldModel.get_current_odds();
            string str8 = newModel.get_current_odds();
            int num = oldModel.get_odds_id();
            string note = "微調" + str6;
            string act = "";
            int num2 = Convert.ToInt32((LSEnums.LogTypeID) 0);
            if (operate_type.Equals("3"))
            {
                note = "手工输入赔率值,微調" + str6;
            }
            string str11 = "";
            DataTable table = CallBLL.cz_phase_xyft5_bll.IsOpenPhaseByTime(DateTime.Now);
            if (table != null)
            {
                str11 = table.Rows[0]["phase"].ToString();
            }
            if (s != str8)
            {
                if (double.Parse(s) > double.Parse(str8))
                {
                    act = "降賠率";
                }
                else
                {
                    act = "升賠率";
                }
                List<SqlParameter> paramList = new List<SqlParameter>();
                string str12 = this.add_sys_log(str, str2, category, str5, str6, gameNameByID, str11, act, num, s, str8, note, num2, 9, ref paramList);
                CallBLL.cz_system_log_bll.executte_sql(str12, paramList.ToArray());
            }
        }

        public void xyftoa_kjj_log(DataTable oldTable, DataTable newTable, string operate_type)
        {
            string str = this.get_master_name();
            string str2 = this.get_children_name();
            string gameNameByID = base.GetGameNameByID(20.ToString());
            string category = "";
            string str5 = "";
            string str6 = "";
            string s = "";
            string str8 = "";
            int num = 0;
            string note = "快捷鍵微调" + str6;
            string act = "";
            int num2 = Convert.ToInt32((LSEnums.LogTypeID) 0);
            if (operate_type.Equals("7"))
            {
                note = "快捷鍵手工输入赔率值,微調" + str6;
            }
            string str11 = "";
            DataTable table = CallBLL.cz_phase_xyftoa_bll.IsOpenPhaseByTime(DateTime.Now);
            if (table != null)
            {
                str11 = table.Rows[0]["phase"].ToString();
            }
            List<CommandInfo> list = new List<CommandInfo>();
            int num3 = 0;
            foreach (DataRow row in oldTable.Rows)
            {
                category = row["category"].ToString();
                str5 = row["play_name"].ToString();
                str6 = row["put_amount"].ToString();
                s = row["current_odds"].ToString();
                str8 = newTable.Rows[num3]["current_odds"].ToString();
                num = int.Parse(row["odds_id"].ToString());
                if (s != str8)
                {
                    if (double.Parse(s) > double.Parse(str8))
                    {
                        act = "降賠率";
                    }
                    else
                    {
                        act = "升賠率";
                    }
                    List<SqlParameter> paramList = new List<SqlParameter>();
                    string str12 = this.add_sys_log(str, str2, category, str5, str6, gameNameByID, str11, act, num, s, str8, note, num2, 20, ref paramList);
                    CommandInfo item = new CommandInfo {
                        CommandText = str12,
                        Parameters = paramList.ToArray()
                    };
                    list.Add(item);
                }
                num3++;
            }
            if (list.Count > 0)
            {
                CallBLL.cz_system_log_bll.execute_sql_tran(list);
            }
        }

        public void xyftoa_log(cz_odds_xyftoa oldModel, cz_odds_xyftoa newModel, string operate_type)
        {
            string str = this.get_master_name();
            string str2 = this.get_children_name();
            string gameNameByID = base.GetGameNameByID(20.ToString());
            string category = oldModel.get_category();
            string str5 = oldModel.get_play_name();
            string str6 = oldModel.get_put_amount();
            string s = oldModel.get_current_odds();
            string str8 = newModel.get_current_odds();
            int num = oldModel.get_odds_id();
            string note = "微調" + str6;
            string act = "";
            int num2 = Convert.ToInt32((LSEnums.LogTypeID) 0);
            if (operate_type.Equals("3"))
            {
                note = "手工输入赔率值,微調" + str6;
            }
            string str11 = "";
            DataTable table = CallBLL.cz_phase_xyftoa_bll.IsOpenPhaseByTime(DateTime.Now);
            if (table != null)
            {
                str11 = table.Rows[0]["phase"].ToString();
            }
            if (s != str8)
            {
                if (double.Parse(s) > double.Parse(str8))
                {
                    act = "降賠率";
                }
                else
                {
                    act = "升賠率";
                }
                List<SqlParameter> paramList = new List<SqlParameter>();
                string str12 = this.add_sys_log(str, str2, category, str5, str6, gameNameByID, str11, act, num, s, str8, note, num2, 20, ref paramList);
                CallBLL.cz_system_log_bll.executte_sql(str12, paramList.ToArray());
            }
        }

        public void xyftsg_kjj_log(DataTable oldTable, DataTable newTable, string operate_type)
        {
            string str = this.get_master_name();
            string str2 = this.get_children_name();
            string gameNameByID = base.GetGameNameByID(0x15.ToString());
            string category = "";
            string str5 = "";
            string str6 = "";
            string s = "";
            string str8 = "";
            int num = 0;
            string note = "快捷鍵微调" + str6;
            string act = "";
            int num2 = Convert.ToInt32((LSEnums.LogTypeID) 0);
            if (operate_type.Equals("7"))
            {
                note = "快捷鍵手工输入赔率值,微調" + str6;
            }
            string str11 = "";
            DataTable table = CallBLL.cz_phase_xyftsg_bll.IsOpenPhaseByTime(DateTime.Now);
            if (table != null)
            {
                str11 = table.Rows[0]["phase"].ToString();
            }
            List<CommandInfo> list = new List<CommandInfo>();
            int num3 = 0;
            foreach (DataRow row in oldTable.Rows)
            {
                category = row["category"].ToString();
                str5 = row["play_name"].ToString();
                str6 = row["put_amount"].ToString();
                s = row["current_odds"].ToString();
                str8 = newTable.Rows[num3]["current_odds"].ToString();
                num = int.Parse(row["odds_id"].ToString());
                if (s != str8)
                {
                    if (double.Parse(s) > double.Parse(str8))
                    {
                        act = "降賠率";
                    }
                    else
                    {
                        act = "升賠率";
                    }
                    List<SqlParameter> paramList = new List<SqlParameter>();
                    string str12 = this.add_sys_log(str, str2, category, str5, str6, gameNameByID, str11, act, num, s, str8, note, num2, 0x15, ref paramList);
                    CommandInfo item = new CommandInfo {
                        CommandText = str12,
                        Parameters = paramList.ToArray()
                    };
                    list.Add(item);
                }
                num3++;
            }
            if (list.Count > 0)
            {
                CallBLL.cz_system_log_bll.execute_sql_tran(list);
            }
        }

        public void xyftsg_log(cz_odds_xyftsg oldModel, cz_odds_xyftsg newModel, string operate_type)
        {
            string str = this.get_master_name();
            string str2 = this.get_children_name();
            string gameNameByID = base.GetGameNameByID(0x15.ToString());
            string category = oldModel.get_category();
            string str5 = oldModel.get_play_name();
            string str6 = oldModel.get_put_amount();
            string s = oldModel.get_current_odds();
            string str8 = newModel.get_current_odds();
            int num = oldModel.get_odds_id();
            string note = "微調" + str6;
            string act = "";
            int num2 = Convert.ToInt32((LSEnums.LogTypeID) 0);
            if (operate_type.Equals("3"))
            {
                note = "手工输入赔率值,微調" + str6;
            }
            string str11 = "";
            DataTable table = CallBLL.cz_phase_xyftsg_bll.IsOpenPhaseByTime(DateTime.Now);
            if (table != null)
            {
                str11 = table.Rows[0]["phase"].ToString();
            }
            if (s != str8)
            {
                if (double.Parse(s) > double.Parse(str8))
                {
                    act = "降賠率";
                }
                else
                {
                    act = "升賠率";
                }
                List<SqlParameter> paramList = new List<SqlParameter>();
                string str12 = this.add_sys_log(str, str2, category, str5, str6, gameNameByID, str11, act, num, s, str8, note, num2, 0x15, ref paramList);
                CallBLL.cz_system_log_bll.executte_sql(str12, paramList.ToArray());
            }
        }

        public void xync_kjj_log(DataTable oldTable, DataTable newTable, string operate_type)
        {
            string str = this.get_master_name();
            string str2 = this.get_children_name();
            string gameNameByID = base.GetGameNameByID(3.ToString());
            string category = "";
            string str5 = "";
            string str6 = "";
            string s = "";
            string str8 = "";
            int num = 0;
            string note = "快捷鍵微调" + str6;
            string act = "";
            int num2 = Convert.ToInt32((LSEnums.LogTypeID) 0);
            if (operate_type.Equals("7"))
            {
                note = "快捷鍵手工输入赔率值,微調" + str6;
            }
            string str11 = "";
            DataTable table = CallBLL.cz_phase_xync_bll.IsOpenPhaseByTime(DateTime.Now);
            if (table != null)
            {
                str11 = table.Rows[0]["phase"].ToString();
            }
            List<CommandInfo> list = new List<CommandInfo>();
            int num3 = 0;
            foreach (DataRow row in oldTable.Rows)
            {
                category = row["category"].ToString();
                str5 = row["play_name"].ToString();
                str6 = row["put_amount"].ToString();
                s = row["current_odds"].ToString();
                str8 = newTable.Rows[num3]["current_odds"].ToString();
                num = int.Parse(row["odds_id"].ToString());
                if (s != str8)
                {
                    if (double.Parse(s) > double.Parse(str8))
                    {
                        act = "降賠率";
                    }
                    else
                    {
                        act = "升賠率";
                    }
                    List<SqlParameter> paramList = new List<SqlParameter>();
                    string str12 = this.add_sys_log(str, str2, category, str5, str6, gameNameByID, str11, act, num, s, str8, note, num2, 3, ref paramList);
                    CommandInfo item = new CommandInfo {
                        CommandText = str12,
                        Parameters = paramList.ToArray()
                    };
                    list.Add(item);
                }
                num3++;
            }
            if (list.Count > 0)
            {
                CallBLL.cz_system_log_bll.execute_sql_tran(list);
            }
        }

        public void xync_log(cz_odds_xync oldModel, cz_odds_xync newModel, string operate_type)
        {
            string str = this.get_master_name();
            string str2 = this.get_children_name();
            string gameNameByID = base.GetGameNameByID(3.ToString());
            string category = oldModel.get_category();
            string str5 = oldModel.get_play_name();
            string str6 = oldModel.get_put_amount();
            string s = oldModel.get_current_odds();
            string str8 = newModel.get_current_odds();
            int num = oldModel.get_odds_id();
            string note = "微調" + str6;
            string act = "";
            int num2 = Convert.ToInt32((LSEnums.LogTypeID) 0);
            if (operate_type.Equals("3"))
            {
                note = "手工输入赔率值,微調" + str6;
            }
            string str11 = "";
            DataTable table = CallBLL.cz_phase_xync_bll.IsOpenPhaseByTime(DateTime.Now);
            if (table != null)
            {
                str11 = table.Rows[0]["phase"].ToString();
            }
            if (s != str8)
            {
                if (double.Parse(s) > double.Parse(str8))
                {
                    act = "降賠率";
                }
                else
                {
                    act = "升賠率";
                }
                List<SqlParameter> paramList = new List<SqlParameter>();
                string str12 = this.add_sys_log(str, str2, category, str5, str6, gameNameByID, str11, act, num, s, str8, note, num2, 3, ref paramList);
                CallBLL.cz_system_log_bll.executte_sql(str12, paramList.ToArray());
            }
        }

        public string AgentCurrentLotteryID
        {
            get
            {
                string str = CacheHelper.GetCache("cachecurrentlid");
                if (string.IsNullOrEmpty(str))
                {
                    string str2 = this.GetLotteryList().Rows[0]["id"].ToString();
                    CacheHelper.SetCache("cachecurrentlid", str2);
                }
                return str;
            }
        }

        public string AgentCurrentMasterLotteryID
        {
            get
            {
                string str = CacheHelper.GetCache("cachecurrentmlid");
                if (string.IsNullOrEmpty(str))
                {
                    string str2 = this.GetLotteryList().Rows[0]["master_id"].ToString();
                    CacheHelper.SetCache("cachecurrentmlid", str2);
                }
                return str;
            }
        }

        protected string GetUserName
        {
            get
            {
                if (HttpContext.Current.Session["child_user_name"] != null)
                {
                    return HttpContext.Current.Session["child_user_name"].ToString();
                }
                return HttpContext.Current.Session["user_name"].ToString();
            }
        }

        protected string GetUserTypeName
        {
            get
            {
                switch (HttpContext.Current.Session["user_type"].ToString())
                {
                    case "zj":
                        return "總監";

                    case "fgs":
                        return "分公司";

                    case "gd":
                        return "股東";

                    case "zd":
                        return "總代理";

                    case "dl":
                        return "代理";

                    case "hy":
                        return "會員";
                }
                return "";
            }
        }

        public string UserCurrentSkin { get; set; }

        public string UserReturnBackUrl
        {
            get
            {
                return CookieHelper.GetCookie("userreturnbackurl");
            }
        }
    }
}

