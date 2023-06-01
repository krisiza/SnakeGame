using System;
using System.Security.Cryptography.X509Certificates;
using System.Collections.Generic;

namespace SnakeGame
{
    internal class Display
    {
        private int _width = 0;
        private int _height = 0;

        private List<(int x, int y)> ListObstacles = new List<(int, int)>();
        public int Width()
        {
            return _width;
        }
        public int Heigth()
        {
            return _height;
        }
        public Display()
        {
            _width = Console.WindowWidth;
            _height = Console.WindowHeight;
        }
        public Display(int width, int height)
        {
            _width = width;
            _height = height;
        }
        public void DrawPixel(int x, int y, char pixel = '█')         
        {
            if (x < 0 || x > _width)
            {
                return;
            }
            if (y < 0 || y > _height)
            {
                return;
            }

            Console.SetCursorPosition(x, y);
            Console.Write(pixel);     
        }

        public void DrawLine(int x0, int y0, int x1, int y1, char pixel = '*')
        {
            if (y0 == y1)
            {
                int y = y0;

                // sicherstellen dass x0 kleiner x1 ist, wegen schleife
                if (x1 < x0)
                {
                    // x0 und x1 tauschen
                    (x0, x1) = (x1, x0);
                }

                for (int x = x0; x <= x1; x++)
                {
                    DrawPixel(x, y);
                }
            }
            else if (x0 == x1)
            {
                int x = x0;

                // sicherstellen dass y0 kleiner y1 ist, wegen schleife
                if (y1 < y0)
                {
                    // y0 und y1 tauschen
                    (y0, y1) = (y1, y0);
                }

                for (int y = y0; y <= y1; y++)
                {
                    DrawPixel(x, y);
                }
            }
        }
        public void DrawRectangle(int x0,int y0, int x1, int y1)
        {
            DrawLine(x0,y0, x1,y0);
            DrawLine(x1,y0, x1,y1);
            DrawLine(x1,y1, x0,y1);
            DrawLine(x0,y1, x0,y0);
        }
        public void DrawText(List<string> textList)
        {
            int xm = _width/2;
            int ym = _height / 2;
            int ofsY = textList.Count / 2;
            int ys = ym - ofsY;
            int endY =ym + ofsY;
            
            int maxStringLength=0;
            for (int i = 0; i < textList.Count; i++)
            {
                maxStringLength  = Math.Max(maxStringLength, textList[i].Length);
                
            }
      
            int ofsX = maxStringLength / 2;
            int xs = xm - ofsX;
            int endX = xm + ofsX;
            
            DrawRectangle(xs - 2, ys - 2, endX + 3, endY + 2);

            for (int y = 0; y < textList.Count; y++)
            {
                int ofsZ = (maxStringLength - textList[y].Length) / 2;

                Console.SetCursorPosition(xs + ofsZ, ys + y);
                Console.Write(textList[y]);  
            }
        }
    } 
}
