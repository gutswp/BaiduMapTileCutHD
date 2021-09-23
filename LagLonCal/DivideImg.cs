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
    public class DivideImg
    {

        public void ShowImage(string path)
        {
            int smallImageWidth = 6600 / 20;
            int width = 6600;
            int height = 6600;
            int ImageNumber = width / smallImageWidth;
            Bitmap[] bitmaps = new Bitmap[ImageNumber];
            for (int i = 0; i < ImageNumber; i++)
            {
                bitmaps[i] = new Bitmap(smallImageWidth, height, PixelFormat.Format8bppIndexed);
                ReadBitmapValue(i * smallImageWidth, ref bitmaps[i], path, width);
                bitmaps[i].Save(@"D:\1 BI\切片" + i + ".png", ImageFormat.Png);
            }
        }


        public bool ReadBitmapValue(int startX, ref Bitmap bitmap, string path, int SrcImageWidth)
        {
            byte[] value = new byte[bitmap.Width * bitmap.Height];
            StreamReader reader;
            BitmapData bitmapData = null;
            try
            {
                bitmapData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite, bitmap.PixelFormat);
                reader = new StreamReader(path);
                int datastart = 1078;
                reader.BaseStream.Seek(datastart + startX, SeekOrigin.Begin);
                long ret = 0;
                for (int i = 0; i < bitmap.Height; i++)
                {
                    if (reader.BaseStream.Read(value, value.Length - ((i + 1) * bitmap.Width), bitmap.Width) != bitmap.Width)
                    {
                        return false;
                    }
                    if (i != bitmap.Height - 1)
                    {
                        ret = reader.BaseStream.Seek(SrcImageWidth - bitmap.Width, SeekOrigin.Current);
                    }
                }
                Marshal.Copy(value, 0, bitmapData.Scan0, value.Length);
            }
            catch
            {
                return false;
            }
            finally
            {
                bitmap.UnlockBits(bitmapData);
            }
            reader.Close();
            SetColorTable(ref bitmap);
            return true;
        }

        public void SetColorTable(ref Bitmap bitmap)
        {
            ColorPalette colorPalette = bitmap.Palette;
            for (int i = 0; i < 256; i++)
            {
                colorPalette.Entries[i] = Color.FromArgb(i, i, i);
            }
            bitmap.Palette = colorPalette;
        }
    }
}
