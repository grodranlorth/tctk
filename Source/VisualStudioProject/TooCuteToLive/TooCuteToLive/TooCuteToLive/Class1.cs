using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace TooCuteToLive
{
    class Class1
    {
        public static GraphicsDeviceManager graph;

        public static Texture2D CreateCircle(int radius, Color color)
        {
            Texture2D circle = new Texture2D(graph.GraphicsDevice, radius * 2, radius * 2);
            Color[] c = new Color[circle.Width * circle.Height];
            circle.GetData<Color>(c);
            DrawCircle(circle.Width / 2, circle.Height / 2, radius, color, ref c, circle.Width, circle.Height);
            circle.SetData<Color>(c);
            return circle;
        }
        static void DrawCircle(int x, int y, int r, Color c, ref Color[] z, int width, int height)
        {
            int i, j;
            for (i = 0; i < 2 * r; i++)
            {
                if ((y - r + i) >= 0 && (y - r + i) < height)
                {
                    int len = (int)(Math.Sqrt(Math.Cos(0.5f * Math.PI * (i - r) / r)) * r * 2);
                    int xofs = x - len / 2;
                    if (xofs < 0)
                    {
                        len += xofs;
                        xofs = 0;
                    }
                    if (xofs + len >= width)
                    {
                        len -= (xofs + len) - width;
                    }
                    int ofs = (y - r + i) * width + xofs;
                    for (j = 0; j < len; j++)
                        z[ofs + j] = c;
                }
            }
        } 
    }
}
