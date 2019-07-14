using GoogleDinoAIBot.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleDinoAIBot.Core.Services
{
    public class BitmapHasherService
        : IBitmapHasherService
    {
        public List<bool> GetHash(Bitmap bmpSource)
        {
            List<bool> lResult = new List<bool>();
            //create new image with 16x16 pixel
            Bitmap bmpMin = new Bitmap(bmpSource, new Size(16, 16));
            for (int j = 0; j < bmpMin.Height; j++)
            {
                for (int i = 0; i < bmpMin.Width; i++)
                {
                    //reduce colors to true / false                
                    lResult.Add(bmpMin.GetPixel(i, j).GetBrightness() < 0.5f);
                }
            }
            return lResult;
        }

        public bool HashCompare(List<bool> hash1, List<bool> hash2)
            => hash1.Zip(hash2, (i, j) => i == j).Count(eq => eq) == 256;
    }
}
