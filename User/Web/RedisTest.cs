namespace User.Web
{
    using LotterySystem.Common;
    using LotterySystem.DBUtility;
    using LotterySystem.Model;
    using StackExchange.Redis;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using User.Web.WebBase;

    public class RedisTest : Page
    {
        protected HtmlForm form1;

        public List<string> GetFolder(string reIndex)
        {
            List<string> list = new List<string> { "useronline:list:hy:hy_1300:u_type", "useronline:list:hy:hy_1200:u_type", "useronline:list:zj:yt668833:last_time", "useronline:list:hy:hy_1300:first_time", "useronline:list:hy:hy_1200:first_time", "useronline:list:zj:yt668833:is_out", "useronline:list:hy:hy_1200:ip", "useronline:list:zj:yt668833:u_type", "useronline:list:zj:yt668833:first_time", "useronline:list:zj:yt668833:ip", "useronline:list:hy:hy_1200:last_time", "useronline:list:hy:hy_1200:last_time", "useronline:list:hy:hy_1200:is_out", "useronline:list:hy:hy_1300:ip", "useronline:list:hy:hy_1300:last_time" };
            List<string> source = new List<string>();
            for (int i = 0; i < list.Count; i++)
            {
                string[] strArray = list[i].Split(new char[] { ':' });
                string str = "";
                string[] strArray2 = reIndex.Split(new char[] { ',' });
                for (int j = 0; j < strArray2.Length; j++)
                {
                    if (string.IsNullOrEmpty(str))
                    {
                        str = str + strArray[int.Parse(strArray2[j])];
                    }
                    else
                    {
                        str = str + ":" + strArray[int.Parse(strArray2[j])];
                    }
                }
                source.Add(str);
            }
            if (source.Count > 0)
            {
                return source.Distinct<string>().ToList<string>();
            }
            return null;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            base.Response.End();
            base.Response.End();
            ITransaction transaction = CallBLL.redisHelper.CreateTransaction();
            CallBLL.redisHelper.GetDatabase().CreateBatch(null).Execute();
            List<string> stringKeysByPattern = CallBLL.redisHelper.GetStringKeysByPattern("*online*");
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary.Add("A1", "A11111");
            dictionary.Add("B1", "B11111");
            CallBLL.redisHelper.StringSet("DictTest:dict", JsonHandle.ObjectToJson(dictionary), FileCacheHelper.get_RedisExpiryDay());
            RedisDemoObject obj2 = new RedisDemoObject {
                Id = 1,
                Name = "123"
            };
            CallBLL.redisHelper.StringSet<RedisDemoObject>("test1:RedisDemoObject", obj2, FileCacheHelper.get_RedisExpiryDay());
            RedisDemoObject obj3 = CallBLL.redisHelper.StringGet<RedisDemoObject>("test1:RedisDemoObject");
            string str = "select * from cz_stat_online";
            Dictionary<string, cz_stat_online> dictionary2 = new Dictionary<string, cz_stat_online>();
            DataTable table = DbHelperSQL.Query(str, null).Tables[0];
            foreach (DataRow row in table.Rows)
            {
                cz_stat_online _online = new cz_stat_online();
                _online.set_id(int.Parse(row["id"].ToString()));
                _online.set_u_name(row["u_name"].ToString());
                _online.set_u_type(row["u_type"].ToString());
                _online.set_u_flag(row["u_flag"].ToString());
                _online.set_first_time(new DateTime?(DateTime.Parse(row["first_time"].ToString())));
                _online.set_last_time(new DateTime?(DateTime.Parse(row["last_time"].ToString())));
                _online.set_ip(row["ip"].ToString());
                _online.set_is_out(new int?(int.Parse(row["is_out"].ToString())));
                CallBLL.redisHelper.HashSet<cz_stat_online>("useronline:list", _online.get_u_name(), _online);
            }
            long num = CallBLL.redisHelper.HashLength("useronline:list");
            cz_stat_online _online2 = CallBLL.redisHelper.HashGet<cz_stat_online>("useronline:list", "yt668833");
            cz_stat_online _online3 = new cz_stat_online();
            _online3.set_u_name("fgs_1111111111111111111111");
            if (CallBLL.redisHelper.HashExists("useronline:list", "fgs_1111111111111111111111"))
            {
                CallBLL.redisHelper.HashDelete("list", "useronlinefgs_1111111111111111111111");
            }
            _online3.set_u_flag("zj");
            CallBLL.redisHelper.HashSet<cz_stat_online>("useronline:list", _online3.get_u_name(), _online3);
            List<cz_stat_online> list2 = CallBLL.redisHelper.HashValues<cz_stat_online>("useronline:list");
            bool flag2 = transaction.Execute(0);
        }
    }
}

