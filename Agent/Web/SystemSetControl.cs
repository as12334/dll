namespace Agent.Web
{
    using System;
    using System.Data;
    using System.Runtime.CompilerServices;
    using System.Web.UI;

    public class SystemSetControl : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        public DataRow[] dataRow { get; set; }

        public int flagtype { get; set; }

        public string gameName { get; set; }

        public int lotteryId { get; set; }

        public DataRow[] ylDownDataRow { get; set; }

        public DataRow[] ylSytemDataRow { get; set; }
    }
}

