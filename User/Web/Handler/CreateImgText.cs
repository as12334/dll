namespace User.Web.Handler
{
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.Drawing.Text;
    using System.IO;
    using System.Windows.Forms;

    public class CreateImgText
    {
        private int _Height;
        private int _Width;
        private string filePath = "";
        private string Text = "";
        private Color TextColor = Color.White;
        private FontStyle TextStyle = FontStyle.Regular;

        public CreateImgText(string text, string strPath)
        {
            this.Text = text;
            this.filePath = strPath;
        }

        public byte[] CreateTextByte()
        {
            Font font = new Font("微软雅黑", 20f, this.TextStyle);
            Brush brush = new SolidBrush(this.TextColor);
            Size size = TextRenderer.MeasureText(this.Text, font);
            this._Width = size.Width;
            this._Height = size.Height;
            Bitmap image = new Bitmap(size.Width, size.Height);
            Graphics graphics = Graphics.FromImage(image);
            graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
            graphics.Clear(Color.Transparent);
            graphics.DrawString(this.Text, font, brush, (float) 0f, (float) 0f);
            MemoryStream stream = new MemoryStream();
            image.Save(this.filePath, ImageFormat.Png);
            byte[] buffer = new byte[stream.Length];
            buffer = stream.ToArray();
            image.Dispose();
            graphics.Dispose();
            stream.Close();
            return buffer;
        }
    }
}

