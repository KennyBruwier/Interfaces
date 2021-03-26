using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    interface IDestroyer
    {
        void Shoot();
    }
    interface IMoveable
    {
        Point Location { get; set; }
        void MoveDown();
        void MoveLeft();
        void MoveRight();
        void MoveUp();
    }
    public class Point : MapElement
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Point(char drawChar, int x, int y) : base(drawChar)
        {
            drawChar = drawChar;
            X = x;
            Y = y;
        }
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }
        public override void Draw()
        {
            Draw(ConsoleColor.White);
        }
    }
    abstract public class MapElement
    {
        public char drawChar = ' ';
        public Point Location { get; set; }
        public int MaxX { get; set; }
        public int MaxY { get; set; }
        public int MinX { get; set; }
        public int MinY { get; set; }
        abstract public void Draw();   
        protected private void Draw(ConsoleColor kleur)
        {
            ConsoleColor fKleur = Console.ForegroundColor;
            Console.ForegroundColor = kleur;
            Console.SetCursorPosition(Location.Y,Location.X);
            Console.Write($"{drawChar}");
            Console.ForegroundColor = fKleur;
        }

        public MapElement(char drawChar)
        {
            this.drawChar = drawChar;
        }
        public MapElement()
        {

        }
        public void Shoot(ConsoleColor kleur)
        {
            ConsoleColor fKleur = Console.ForegroundColor;
            Console.ForegroundColor = kleur;
            Console.SetCursorPosition(Location.Y+1, Location.X);
            Console.Write('▒');
            System.Threading.Thread.Sleep(10);
            Console.ForegroundColor = fKleur;
        }
        public void MoveUp()
        {
            if (Location.X > MinX) Location.X--; 
        }
        public void MoveDown()
        {
            if (Location.X < MaxX-1) Location.X++;
        }
        public void MoveLeft()
        {
            if (Location.Y > MinY) Location.Y--;
        }
        public void MoveRight()
        {
            if (Location.Y < MaxY-1) Location.Y++;
        }
    }
    class Monster : MapElement, IMoveable
    {
        public byte Levens = 0;
        public Monster(char drawChar) : base(drawChar)
        {
            this.drawChar = drawChar;
        }

        public override void Draw()
        {
            Draw(ConsoleColor.DarkRed);
        }

        void IMoveable.MoveDown()
        {
            MoveDown();
        }

        void IMoveable.MoveLeft()
        {
            MoveLeft();
        }

        void IMoveable.MoveRight()
        {
            MoveRight();
        }

        void IMoveable.MoveUp()
        {
            MoveUp();
        }
    }
    class RockDestroyer : Monster, IDestroyer
    {
        public RockDestroyer(char drawChar) : base(drawChar)
        {
            this.drawChar = drawChar;
        }

        void IDestroyer.Shoot()
        {
            Shoot(ConsoleColor.Red);
        }
    }
    class Player : MapElement, IMoveable, IDestroyer
    {
        public byte Levens = 0;
        public Player(char drawChar) : base(drawChar)
        {
            this.drawChar = drawChar;
        }

        void IMoveable.MoveDown()
        {
            MoveDown();
        }

        void IMoveable.MoveLeft()
        {
            MoveLeft();
        }

        void IMoveable.MoveRight()
        {
            MoveRight();
        }

        void IMoveable.MoveUp()
        {
            MoveUp();
        }

        void IDestroyer.Shoot()
        {
            Shoot(ConsoleColor.Blue);
        }
        public override void Draw()
        {
            Draw(ConsoleColor.DarkBlue);
        }
    }
    class Rock : MapElement
    {
        public Rock(char drawChar) : base(drawChar)
        {
            this.drawChar = drawChar;
        }
        public override void Draw()
        {
            Draw(ConsoleColor.DarkGray);
        }
    }
}
