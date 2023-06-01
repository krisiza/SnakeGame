using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SnakeGame
{
    internal class Fruits
    {
        private Display _display;
        private char _cObst = '*';
        int _numFruits = 3;
        private List<(int x,int y)> ListObst = new List<(int,int)>();

        public Fruits(Display display, int numFruits)
        {
            _display = display;
            _numFruits = numFruits;

            CreateNewFood();
        }
        public void CreateNewFood()
        {
            Random rnd = new Random();
            
            for (int i = 0; i < _numFruits; i++)
            {    
                int x = rnd.Next(5, _display.Width() - 3);
                int y = rnd.Next(5, _display.Heigth() - 3);

                ListObst.Add((x, y));
            }
        }
        public int NumberOfFruits()
        {
            return ListObst.Count();
        }
        public (int x,int y) FruitAt(int index)
        {
            return ListObst[index];
        }
        public void RemoveFruit(int index)
        {
            ListObst.RemoveAt(index);

            if (ListObst.Count == 0)
                CreateNewFood();
        }
        public void DrawFood()
        {
            for (int i = 0; i < ListObst.Count; i++)
            {
                _display.DrawPixel(ListObst[i].x, ListObst[i].y, _cObst);
            }
        }
    }
}
