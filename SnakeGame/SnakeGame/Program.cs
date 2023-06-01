using System;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Reflection.Metadata.Ecma335;
using static System.Reflection.Metadata.BlobBuilder;

namespace SnakeGame
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> listGameOver = new List<string>();
            listGameOver.Add("Game Over!");
            listGameOver.Add("");
            listGameOver.Add("Your Score:");
            
            Console.CursorVisible = false;

            Display display = new();
            Obstacles obstacles = new(display);
            Snake snake = new(display);
            Fruits fruits = new(display, 5);

            while (true)
            {
                Console.Clear();

                // äußeren Rahmen zeichnen
                display.DrawRectangle(0, 0, display.Width() - 1, display.Heigth() - 1);

                // Schlange bewegen
                snake.Direction();

                //Schlange körper
                snake.DrawSnakeBody();

                // Obst zeichnen
                fruits.DrawFood();

                //Obstacles zeichen
                obstacles.DrawAcle();

                //fruits.CreateNewFood();
                snake.CheckObst(fruits);
                
                //Game Over
                if (snake.IsGameOver() || snake.IsOnacles(obstacles))
                {
                    bool exitGame = false;
                    display.DrawText(listGameOver);
                    snake.Score();
                    snake.NewStart(ref exitGame);

                    if (exitGame)
                    {   
                        return;
                    }    
                }

                Thread.Sleep(100); 
            }          
        }
    }
}
    
