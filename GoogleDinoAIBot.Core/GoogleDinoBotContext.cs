using GoogleDinoAIBot.Core.Abstractions;
using GoogleDinoAIBot.Core.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GoogleDinoAIBot.Core
{
    public class GoogleDinoBotContext
        : IGoogleDinoBotContext
    {
        private readonly Rectangle _gameFrameRectangle;

        private readonly IBitmapHasherService _bitmapHasherService;

        [DllImport("kernel32.dll", EntryPoint = "GetConsoleWindow", SetLastError = true)]
        private static extern IntPtr GetConsoleHandle();

        public GoogleDinoBotContext(Rectangle gameFrameRectangle)
        {
            _gameFrameRectangle = gameFrameRectangle;
            _bitmapHasherService = new BitmapHasherService();
        }


        public async Task StartAsync()
        {
            List<bool> lastHash = default;

            while (true)
            {
                Bitmap bitmap = GetGameRectangleBitMap();
                Bitmap alertBitmap = CropAlertImage(bitmap, new Rectangle(100, 135, 30, 10));

                if (lastHash == null)
                {
                    lastHash = _bitmapHasherService.GetHash(alertBitmap);
                }
                else
                {
                    var hashCode = _bitmapHasherService.GetHash(alertBitmap);

                    if (!_bitmapHasherService.HashCompare(lastHash, hashCode))
                    {
                        SendKeys.SendWait(" ");
                        DrawAlertRectangle(bitmap, new Rectangle(100, 135, 15, 10), Pens.Green);
                    }
                    else
                    {
                        DrawAlertRectangle(bitmap, new Rectangle(100, 135, 15, 10), Pens.Red);
                    }
                }

                
                Task.Run(() =>
                {
                    Console.Clear();

                    var handler = GetConsoleHandle();

                    using (var graphics = Graphics.FromHwnd(handler))
                    using (var image = bitmap)
                        graphics.DrawImage(image, 0, 0, 640, 300);
                });
            }
        }

        private void DrawAlertRectangle(Bitmap bitmap, Rectangle rectangle, Pen pen)
        {
            Graphics graphics = Graphics.FromImage(bitmap);
            graphics.DrawRectangle(pen, rectangle);
        }

        public Bitmap CropAlertImage(Bitmap source, Rectangle section)
        {
            Bitmap bitmap = new Bitmap(section.Width, section.Height);

            Graphics g = Graphics.FromImage(bitmap);
            g.DrawImage(source, 0, 0, section, GraphicsUnit.Pixel);

            return bitmap;
        }

        private Bitmap GetGameRectangleBitMap()
        {
            Bitmap bitmap = new Bitmap(_gameFrameRectangle.Width, _gameFrameRectangle.Height);
            Graphics graphics = Graphics.FromImage(bitmap as System.Drawing.Image);

            graphics.CopyFromScreen(
                _gameFrameRectangle.Left,
                _gameFrameRectangle.Top,
                0,
                0, bitmap.Size);
            
            return bitmap;
        }

        public Task StopAsync()
        {
            throw new NotImplementedException();
        }
    }
}
