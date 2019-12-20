namespace User.Web.m.VRVIDEO
{
    using LotterySystem.Common;
    using LotterySystem.WebPageBase;
    using Newtonsoft.Json.Linq;
    using System;

    public class vr_open_video_w : PageBase
    {
        private string lotteryId = "";
        protected string url = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            this.lotteryId = LSRequest.qq("lid");
            object obj2 = null;
            int num = 0x12;
            if (this.lotteryId.Equals(num.ToString()))
            {
                obj2 = base.VR_Open_Video(0x12);
            }
            else
            {
                num = 0x13;
                if (this.lotteryId.Equals(num.ToString()))
                {
                    obj2 = base.VR_Open_Video(0x13);
                }
                else
                {
                    base.Response.End();
                }
            }
            if (obj2 != null)
            {
                this.url = (string) ((dynamic) ((JObject) obj2)).data;
            }
        }
    }
}

