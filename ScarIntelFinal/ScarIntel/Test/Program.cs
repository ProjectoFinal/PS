using System;
using System.Drawing;
using AForge.Imaging;
using AForge.Imaging.Filters;
using AForge.Math;
using SourceAFIS;
using SourceAFIS.Test;
using Test.BrokerService;

namespace Test
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var image = TestUtils.LoadImage(@"D:\ScarIntelFinal\ScarIntel\BD\filipe_dir_an\filipe_dir_an_2.bmp");
            var fp = new FingerprintTemplate(image);
            
            Bitmap img = (Bitmap) Bitmap.FromFile(@"D:\ScarIntelFinal\ScarIntel\BD\filipe_dir_an\filipe_dir_an_2.bmp");
            
            HistogramEqualization equalization = new HistogramEqualization();
            equalization.ApplyInPlace(img);


            
            img.Save("Aforge.bmp");
             
        }
    }
}