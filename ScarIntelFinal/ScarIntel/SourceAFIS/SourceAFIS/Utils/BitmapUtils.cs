using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SourceAFIS.Utils
{
    class BitmapUtils
    {
        public static Bitmap CreateBitmap(byte[,] image)
        {
            int xsize = image.GetLength(0);
            int ysize = image.GetLength(1);

            Bitmap b = new Bitmap( xsize , ysize );
            //b.PixelFormat = PixelFormat.Format8bppIndexed;

            for (int x = 0; x < xsize; x++)
            {
                for (int y = 0; y < ysize; y++)
                {
                    Color c = Color.FromArgb(image[x, y], image[x, y], image[x, y]);
                    b.SetPixel( x , y , c );
                }
            }
            return b; 
        }

        public static void ShowImage(byte[,] image)
        {
            Bitmap b = CreateBitmap(image);
            b.Save("image.bmp");
            /*PictureBox pb = new PictureBox();
            pb.Image = b;
            pb.Visible = true; 
            pb.Show();*/

            
        }

        internal static void ShowImage(bool[,] image)
        {
            int xsize = image.GetLength(0);
            int ysize = image.GetLength(1);

            byte[,] img = new byte[xsize, ysize];


            for (int x = 0; x < xsize; x++)
            {
                for (int y = 0; y < ysize; y++)
                {
                    if (image[x, y])
                    {
                        img[x, y] = 255;
                    }
                    else
                    {
                        img[x, y] = 0 ;
                    }
                }
            }
            ShowImage(img);
        }
    }
}
