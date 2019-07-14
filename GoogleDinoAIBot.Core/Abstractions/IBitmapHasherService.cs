using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleDinoAIBot.Core.Abstractions
{
    public interface IBitmapHasherService
    {
        List<bool> GetHash(Bitmap bmpSource);
        bool HashCompare(List<bool> hash1, List<bool> hash2);
    }
}
