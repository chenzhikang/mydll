using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
namespace MyDllCollection
{


    /// <summary>
    /// 
    /// </summary>
    public class PictureHelper
    {

        /// <summary>
        /// 调用 压缩图片的方法
        /// </summary>
        /// <param name="sourcepath"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static string GetSpecSizePic(string sourcepath, int width, int height)
        {
            string destPath = ChangedestFilePath(sourcepath, width, height);
            if (File.Exists(destPath))
            {
                return destPath;
            }
            else {
                return CreateSpecSizePic(sourcepath, width, height);
            }

        }

        /// <summary>
        /// 生成制定尺寸的图片
        /// </summary>
        /// <param name="sourcepath">原图路径（绝对路径）</param>
        /// <param name="maxwidth">最大宽度</param>
        /// <param name="maxheight">最大高度</param>
        /// <param name="Quality">图片质量</param>
        /// <returns></returns>
        public static string CreateSpecSizePic(string sourcepath, int maxwidth, int maxheight, int Quality = 100)
        {
            bool IsExist = File.Exists(sourcepath);
            if (!IsExist) return "";

            string fileSaveUrl = "";//xxxxxxxxx_width.jpg


            Image sourceImg = Image.FromFile(sourcepath);

            if (maxheight > sourceImg.Height && maxwidth > sourceImg.Width)
                return sourcepath;

            double sourceRate = (double)sourceImg.Width / sourceImg.Height;
            double targetRat = (double)maxwidth / maxheight;

            fileSaveUrl = ChangedestFilePath(sourcepath, maxwidth, maxheight);

            Image destImage;
            Graphics templateG;

            Rectangle fromrec, toRec;
            fromrec = new Rectangle(0, 0, sourceImg.Width, sourceImg.Height);//原图的大小
            if (maxwidth != 0 && maxheight != 0)//同时指定了宽和高
            {
                toRec = new Rectangle(0, 0, maxwidth, maxheight);

                destImage = new Bitmap(maxheight, maxheight);
            }
            else {
                double scaleRate = 0;
                if (maxheight == 0)//以宽度为准进行缩放
                {
                    scaleRate = (double)maxwidth / sourceImg.Width;
                    int targetHeight = (int)(scaleRate * sourceImg.Height);//目标高度
                    toRec = new Rectangle(0, 0, maxwidth, targetHeight);

                    destImage = new Bitmap(maxwidth, targetHeight);
                }
                else {
                    scaleRate = (double)maxheight / sourceImg.Height;
                    int targetWidth = (int)(scaleRate * sourceImg.Width);
                    toRec = new Rectangle(0, 0, targetWidth, maxheight);

                    destImage = new Bitmap(targetWidth, maxheight);
                }
            }
            templateG = Graphics.FromImage(destImage);
            templateG.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
            templateG.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            templateG.Clear(Color.White);

            templateG.DrawImage(sourceImg, toRec, fromrec, GraphicsUnit.Pixel);//用画板将img划到制定的区域。

            EncoderParameters paramss = new EncoderParameters(1);//一个参数
            paramss.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, Quality);
            string extension = Path.GetExtension(sourcepath);
            destImage.Save(fileSaveUrl, GetCodeInfo(extension), paramss);

            templateG.Dispose();
            sourceImg.Dispose();
            destImage.Dispose();
            return fileSaveUrl;
        }
        /// <summary>
        /// 生成压缩之后的文件路径名
        /// </summary>
        /// <param name="sourcepath"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        private static string ChangedestFilePath(string sourcepath, int width, int height)
        {
            string frontpath = sourcepath.Substring(0, sourcepath.LastIndexOf('.')),
                extensions = Path.GetExtension(sourcepath);
            return frontpath + "_" + width + "_" + height + extensions;
        }



        /// <summary>
        /// 传入后缀名，返回此后缀格式对应的图片编码信息
        /// </summary>
        /// <param name="imageformat"></param>
        /// <returns></returns>
        private static ImageCodecInfo GetCodeInfo(string fileExtension)
        {
            ImageCodecInfo[] allcodeinfos = ImageCodecInfo.GetImageEncoders();
            foreach (var info in allcodeinfos)
            {
                if (info.FilenameExtension.Contains(fileExtension.ToUpper()))
                    return info;
            }
            return null;
        }


    }
}
