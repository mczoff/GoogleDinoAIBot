using GoogleDinoAIBot.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GoogleDinoAIBot
{
    class Program
    {
        static async Task Main(string[] args)
        {
            GoogleDinoBotContext googleDinoBotContext = new GoogleDinoBotContext(new Rectangle(50,380,600,200));
            await googleDinoBotContext.StartAsync();

            while (true)
            {
                Console.WriteLine(Cursor.Position);
            }

            Console.ReadKey();
        }
    }
}
