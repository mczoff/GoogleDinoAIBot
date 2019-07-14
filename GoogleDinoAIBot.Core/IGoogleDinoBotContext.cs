using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleDinoAIBot.Core
{
    public interface IGoogleDinoBotContext
    {
        Task StartAsync();
        Task StopAsync();
    }
}
