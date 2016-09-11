using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

namespace MyDllCollection
{
    public class imageHelper
    {
        public string MakeWaterImg(string sourceimagepath, string waterpath, int toumingdu, bool createnew = false)
        {
            //把水印图画到目标图，保存图片
            Image waterimg = Image.FromFile(waterpath);
            Image sourceimg = Image.FromFile(sourceimagepath);
            Graphics g = Graphics.FromImage(sourceimg);
            //  g.DrawImage(waterimg,);
            return "";
        }
        private static ImageCodecInfo GetImageCodeinfo(string extension)
        {
            var allinfos = ImageCodecInfo.GetImageEncoders();
            foreach (var info in allinfos)
            {
                if (info.FilenameExtension.Contains(extension.ToUpper()))
                {
                    return info;
                }
            }
            return null;
        }
        public static string ConvertImageSize(string sourcePath, int maxWidth, int maxHeight, int zhiliang = 100)
        {
            if (!File.Exists(sourcePath))
                return "";

            Image sourceImage = Image.FromFile(sourcePath);
            Rectangle fromrec = new Rectangle(0, 0, sourceImage.Width, sourceImage.Height);
            Rectangle torec;
            int destWidth = maxWidth, destHeight = maxHeight;
            Image destImage;
            if (maxWidth > sourceImage.Width && maxWidth > sourceImage.Height)
            {
                return sourcePath;//返回原图
            }
            else
            {
                string saveFilePath = "";
                string extension = Path.GetExtension(sourcePath);
                string front = sourcePath.Substring(0, sourcePath.LastIndexOf("."));
                saveFilePath = front + "_" + maxWidth + "_" + maxHeight + "_" + zhiliang + extension;

                if (File.Exists(saveFilePath))
                {
                    return saveFilePath;
                }

                if (maxHeight == 0)
                {
                    double scaleRate = (double)maxWidth / sourceImage.Width;
                    destHeight = (int)(scaleRate * sourceImage.Height);
                }
                else if (maxWidth == 0)
                {
                    double scaleRate = (double)maxHeight / sourceImage.Height;
                    destWidth = (int)(scaleRate * sourceImage.Width);
                }
                else
                {
                    destWidth = maxWidth;
                    destHeight = maxHeight;
                }
                torec = new Rectangle(0, 0, destWidth, destHeight);
                destImage = new Bitmap(torec.Width, torec.Height);
                Graphics g = Graphics.FromImage(destImage);
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.SmoothingMode = SmoothingMode.HighQuality;

                g.DrawImage(sourceImage, torec, fromrec, GraphicsUnit.Pixel);

                ImageCodecInfo thisimageInfo = GetImageCodeinfo(extension);
                EncoderParameters paramss = new EncoderParameters(1);
                paramss.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, zhiliang);
                destImage.Save(saveFilePath, thisimageInfo, paramss);

                sourceImage.Dispose();
                destImage.Dispose();
                g.Dispose();
                return saveFilePath;
            }
            //
        }
    }
}
