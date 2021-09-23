using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Device.Location;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using ImageProcessor;

namespace LagLonCal
{
    public class Program
    {
        static void Main(string[] args)
        {
            string imgPath = @"D:\0 产品相关\天津石化高清---20级覆盖图.png";
            //SliceUp(@"D:\1 BI\TestWP.png", 256, 256);

            //new edit by wp 
            new DivideImg().ShowImage(imgPath);
            //ChangeFileExtToNone(imgPath);
            Console.WriteLine("Done ! ! !");

        }


        //更改图片后缀名，离线地图将.jpg/.png修改为无后缀名的文件
        public static void ChangeFileExtToNone(string imgPath)
        {
            imgPath = @"D:\0 产品相关\地图测试\地图测试\瓦片21\tiles";
            DirectoryInfo dirPar = new DirectoryInfo(imgPath);
            foreach (var dir in dirPar.GetDirectories())
            {
                foreach (var item in dir.GetDirectories())
                {
                    foreach (var file in item.GetFiles())
                    {
                        file.MoveTo(Path.ChangeExtension(file.FullName, ""));

                    }
                }
            }
        }


    }
}
