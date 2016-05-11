using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Common
{
    public static class Drawing
    {
        public static Color BlendColor(Color color1, Color color2, byte ratio)
        {
            var color2Ratio = 255 - ratio;
            var newA = (byte)((color2.A * ratio + color1.A * color2Ratio) / 255);
            var newR = (byte)((color2.R * ratio + color1.R * color2Ratio) / 255);
            var newG = (byte)((color2.G * ratio + color1.G * color2Ratio) / 255);
            var newB = (byte)((color2.B * ratio + color1.B * color2Ratio) / 255);

            return Color.FromArgb(newA, newR, newG, newB);
        }
    }
}
