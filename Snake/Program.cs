﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Snake
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.SetBufferSize(120, 120);

            Walls walls = new Walls(80, 25);
            walls.Draw();

            Point p = new Point(1, 3, 'O');
            /*p1.x = 1;
            p1.y = 3;
            p1.sym = '*';*/
            Snake snake = new Snake(p, 4, Direction.RIGHT);
            snake.Draw();

            Score score = new Score(75, 1);
            score.UpdateScore();

            FoodCreator foodCreator = new FoodCreator(70, 25, '#');
            Point food = foodCreator.CreateFood();
            food.Draw(ConsoleColor.Green);

            FoodCreator badfoodCreator = new FoodCreator(50, 10, '¤');
            Point badfood = badfoodCreator.CreateFood();
            badfood.Draw(ConsoleColor.Red);


            while (true)
            {
                if (walls.IsHit(snake) || snake.IsHitTail())
                {
                    break;
                }
                if (snake.Eat(food))
                {
                    Score.score += 5;
                    score.UpdateScore();
                    food = foodCreator.CreateFood();
                    food.Draw(ConsoleColor.Green);
                }
                if (snake.NOEat(badfood))
                {
                    Score.score -= 5;
                    score.UpdateScore();
                    badfood = badfoodCreator.CreateFood();
                    badfood.Draw(ConsoleColor.Red);
                }
                if(Score.score <=-1)
                {
                    Console.Clear();
                    Console.SetCursorPosition(50, 10);
                    WriteGameOver();
                }
                else
                {
                    snake.Move();
                }
                Thread.Sleep(100);
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo key = Console.ReadKey();
                    snake.HandleKey(key.Key);
                }
            }
            WriteGameOver();
            Console.ReadLine();
        }
        static void WriteGameOver()
        {
            int xOffset = 25;
            int yOffset = 8;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(xOffset, yOffset++);
            WriteText("============================", xOffset, yOffset++);
            WriteText("         GAME OVER", xOffset + 1, yOffset++);
            WriteText("Your points " + Score.score, xOffset - 1, yOffset++);
            WriteText("============================", xOffset, yOffset++);
            Console.SetCursorPosition(27, 15);
            Console.WriteLine("Please write your name: ");
            Console.SetCursorPosition(27, 16);
            string name = Console.ReadLine();
            Console.SetCursorPosition(27, 15);
            Console.WriteLine(name + " , your score is " + Score.score);
        }

        static void WriteText(String text, int xOffset, int yOffset)
        {
            Console.SetCursorPosition(xOffset, yOffset);
            Console.WriteLine(text);
        }

    }
}
