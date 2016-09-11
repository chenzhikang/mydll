using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZXing;
using ZXing.Common;

namespace MyDllCollection
{
    /// <summary>
    /// 
    /// </summary>
    public class QRCodeHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="info">带编码字符串</param>
        /// <param name="savepath">c:\a\b\c.jpg</param>
        public static void ConvertToImage(string info, string savepath)
        {
            Bitmap bitmap = GenerateBitmap(info);
            bitmap.Save(savepath, ImageFormat.Jpeg);
        }
        public static string ConvertToBase64Img(string info)
        {
            Bitmap bitmap = GenerateBitmap(info);
            MemoryStream mstream = new MemoryStream();
            bitmap.Save(mstream, ImageFormat.Jpeg);
            byte[] picarray = mstream.GetBuffer();

            mstream.Close();
            string picbase64 = "data:image/png;base64," + Convert.ToBase64String(picarray);//将8位无符号的整数
            return picbase64;
        }
        private static Bitmap GenerateBitmap(string info)
        {
            BarcodeWriter writer = new BarcodeWriter();
            //使用ITF 格式，不能被现在常用的支付宝、微信扫出来
            //如果想生成可识别的可以使用 CODE_128 格式
            //writer.Format = BarcodeFormat.ITF;
            writer.Format = BarcodeFormat.QR_CODE;
            EncodingOptions options = new EncodingOptions()
            {
                Width = 250,
                Height = 250,
                Margin = 2
            };
            writer.Options = options;
            Bitmap bitmap = writer.Write(info);
            return bitmap;

        }
    }
}
