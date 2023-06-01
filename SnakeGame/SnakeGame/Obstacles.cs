using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame
{
    internal class Obstacles
    {
        private Display _display;
        private char _obstacle = 'X';
        int _size = 3;
        int counterspiel = 3;
        int x = 0;
        int y = 0;
        private List<(int x, int y)> ListObstacles = new List<(int, int)>();


        public Obstacles(Display display)
        {
            _display = display;
            CreateObstacles();
        }
        public void CreateObstacles()
        {
            int counter = 0;
 
            while (counter < counterspiel)
            {
                Random rnd = new Random();

                x = rnd.Next(5, _display.Width() - 2 * _size);
                y = rnd.Next(5, _display.Heigth() - 2 * _size);

                ListObstacles.Add((x, y));
                counter++;
            }
        }
        public void DrawAcle()
        {
            for (int i = 0; i < ListObstacles.Count; i++)
            {
                _display.DrawPixel(ListObstacles[i].x, ListObstacles[i].y, _obstacle);
            }

            for (int j = 0; j < _size; j++)
            {
                for (int k = 0; k < _size; k++)
                {
                    ListObstacles.Add((x+j, y+k));
                }
            }
        }
        public void OnFruits(Fruits fruits)
        {
           for(int i = 0; i <= fruits.NumberOfFruits(); i++)
           {
                if (AclesAt(i).x == fruits.FruitAt(i).x && AclesAt(i).y == fruits.FruitAt(i).y)
                {
                    CreateObstacles();
                }
           }
        }

        public void OnAcles()
        {
            for(int i = 0; i <= NumberOfAcles(); i++)
            {
                if (AclesAt(i).x == AclesAt(i).x && AclesAt(i).y == AclesAt(i).y)
                {
                    CreateObstacles();
                }
            }
        }

        public int NumberOfAcles()
        {
            return ListObstacles.Count();
        }
        public (int x, int y) AclesAt(int index)
        {
            return ListObstacles[index];
        }

    }
}
