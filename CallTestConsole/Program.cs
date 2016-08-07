using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mydll;

namespace CallTestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            string sourcePicturePath = @"C:\Users\ljj\Desktop\20160805_035209_584.jpg";
            string targetPath = PictureHelper.CreateSpecSizePic(sourcePicturePath, 400, 0);

            Console.WriteLine(targetPath);
            Console.Read();
        }
    }
}
