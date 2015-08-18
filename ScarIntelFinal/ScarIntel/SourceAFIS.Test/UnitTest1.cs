using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SourceAFIS.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var image = TestUtils.LoadImage(@"D:\ScarIntelFinal\ScarIntel\BD\filipe_dir_pol\filipe_dir_pol_1.bmp");
            var image1 = TestUtils.LoadImage(@"D:\ScarIntelFinal\ScarIntel\BD\filipe_dir_pol\filipe_dir_pol_2.bmp");
            var image2 = TestUtils.LoadImage(@"D:\ScarIntelFinal\ScarIntel\BD\filipe_dir_pol\filipe_dir_pol_3.bmp");

            var imagedif = TestUtils.LoadImage(@"D:\ScarIntelFinal\ScarIntel\BD\nuno_dir_pol\nuno_dir_pol_1.bmp");

            var fp = new FingerprintTemplate(image);
            var fp1 = new FingerprintTemplate(image1);
            var fp2 = new FingerprintTemplate(image2);
            var fpdif = new FingerprintTemplate(imagedif);
            
            


            FingerprintMatcher fm = new FingerprintMatcher(fp);

            double aux= fm.Match(fp);
            double aux1 = fm.Match(fp1);
            double aux2 = fm.Match(fp2);
            double aux3 = fm.Match(fpdif);
            
            Assert.AreEqual( aux , aux );

        }
    }
}
