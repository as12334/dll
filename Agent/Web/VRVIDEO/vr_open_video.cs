namespace Agent.Web.VRVIDEO
{
    using Agent.Web.WebBase;
    using LotterySystem.Common;
    using Newtonsoft.Json.Linq;
    using System;

    public class vr_open_video : MemberPageBase
    {
        protected string flvUrl = "";
        protected string h5Url = "";
        protected string lotteryId = "";

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
                int num2 = 0x13;
                if (this.lotteryId.Equals(num2.ToString()))
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
                this.h5Url = (string) ((dynamic) ((JObject) obj2)).data;
                this.flvUrl = this.h5Url.Replace("/playlist.m3u8", ".flv");
            }
        }
    }
}

