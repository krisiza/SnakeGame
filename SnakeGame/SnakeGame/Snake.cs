using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;
using static System.Reflection.Metadata.BlobBuilder;

namespace SnakeGame
{
    internal class Snake
    {
        private char _body = '■';
        private char _head = '^';
        private int _iScore = 0;
        private Display _display;
        public List<(int x, int y)> SnakeBody = new List<(int, int)>();
        enum DirectionEnum
        {
            UP,
            DOWN,
            LEFT,
            RIGHT
        }

        private DirectionEnum _direction;
        public Snake(Display display)
        {
            _display = display;

            SnakeBody.Add((_display.Width() / 2, _display.Heigth() / 2));
        }
        public void NewStart(ref bool exitGame)
        {
            ConsoleKey key = Console.ReadKey().Key;

            if (key == ConsoleKey.Enter)
            {
                SnakeBody.Clear();
                SnakeBody.Add((_display.Width() / 2, _display.Heigth() / 2));
            }
            else
            {
                exitGame = true;
                Console.Clear();
                return;
            }
        }
        public void Score()
        {
            _iScore = SnakeBody.Count - 1;

            Console.Write(_iScore);
        }
        public void DrawSnakeBody()
        {
            for (int i = 0; i < SnakeBody.Count; i++)
            {
                char pixel = ' ';

                (int x, int y) = SnakeBody[i];
                if (i == 0)
                    pixel = _head;
                else
                    pixel = _body;

                _display.DrawPixel(x, y, pixel);
            }
        }

        // Figur bewegen
        public void Direction()
        {
            (int xpHead, int ypHead) = SnakeBody[0];

            if (Console.KeyAvailable)
            {
                ConsoleKey key1 = Console.ReadKey().Key;

                if (key1 == ConsoleKey.UpArrow)
                {
                    _direction = DirectionEnum.UP;
                }
                else if (key1 == ConsoleKey.DownArrow)
                {
                    _direction = DirectionEnum.DOWN;
                }
                else if (key1 == ConsoleKey.LeftArrow)
                {
                    _direction = DirectionEnum.LEFT;
                }
                else if (key1 == ConsoleKey.RightArrow)
                {
                    _direction = DirectionEnum.RIGHT;
                }
                else
                {

                }
            }

            if (_direction == DirectionEnum.UP && ypHead > 1)
            {
                ypHead--;
                _head = '^';
            }
            else if (_direction == DirectionEnum.DOWN && ypHead < _display.Heigth() - 2)
            {
                ypHead++;
                _head = 'v';
            }
            else if (_direction == DirectionEnum.LEFT && xpHead > 1)
            {
                xpHead--;
                _head = '<';
            }
            else if (_direction == DirectionEnum.RIGHT && xpHead < _display.Width() - 2)
            {
                xpHead++;
                _head = '>';
            }
            else
            {
                xpHead--;
                _head = '<';
            }

            SnakeBody.Insert(0, (xpHead, ypHead));
        }
        public void CheckObst(Fruits fruits)
        {
            bool obstErreicht = false;

            // prüfen ob der Schlangenkopf ein Obst erreicht hat
            for (int i = 0; i < fruits.NumberOfFruits(); i++)
            {
                if (SnakeBody[0].x == fruits.FruitAt(i).x && SnakeBody[0].y == fruits.FruitAt(i).y)
                {
                    fruits.RemoveFruit(i);
                    obstErreicht = true;
                    break;
                }
            }

            if (obstErreicht == false)
                SnakeBody.RemoveAt(SnakeBody.Count - 1);
        }
        public bool IsOnacles(Obstacles obstacles)
        {
            for (int i = 0; i < obstacles.NumberOfAcles(); i++)
            {
                if (SnakeBody[0].x == obstacles.AclesAt(i).x && SnakeBody[0].y == obstacles.AclesAt(i).y)
                {
                    return true;
                }  
            }
            return false;
        }

        public bool IsGameOver()
        {
            if (SnakeBody[0].x >= _display.Width() - 2)
            {
                return true;
            }
            if (SnakeBody[0].y >= _display.Heigth() - 2)
            {
                return true;
            }
            if (SnakeBody[0].x <= 1)
            {
                return true;
            }
            if (SnakeBody[0].y <= 1)
            {
                return true;
            }

            // prüfen ob schlange mit sich selbst kollidiert
            for (int i = 0; i < SnakeBody.Count; i++)
            {
                if (i > 1 && SnakeBody[0] == SnakeBody[i])
                {
                    return true;
                }
            }
            return false;
        }
    }
}



