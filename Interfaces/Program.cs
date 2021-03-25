using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> Oefeningen = new List<string>();

            //string[] Oefeningen = ne
            Oefeningen = new List<string>();
            Oefeningen.Add("Exit");
            Oefeningen.Add("Figures with interfaces");
            Oefeningen.Add("Game");
            //Oefeningen.Add("Bank");
            //Oefeningen.Add("Pokemon (advanced)");
            //Oefeningen.Add("Bookmark Manager (advanced)");
            //Oefeningen.Add("Book (advanced)");
            //Oefeningen.Add("MoneyMoney (advanced)");
            //Oefeningen.Add("GeometricFigures (advanced)");
            //Oefeningen.Add("Dierentuin (advanced)");
            bool bExit = false;
            while (!bExit)
            {
                Console.Clear();
                Console.WriteLine("Overerving oefeningen:\n");
                switch (SelectMenu(false,menu:Oefeningen.ToArray()) - 1)
                {
                    case 0: bExit = true; break;
                    case 1: FiguresWithInterfaces(); break;
                    case 2: Game(); break;
                    //case 3: Bank(); break;
                    //case 4: Pokemon(); break;
                    //case 5: BookMarkExtra(); break;
                    //case 6: Book(); break;
                    //case 7: MoneyMoney(); break;
                    //case 8: GeometricFigures(); break;
                    //case 9: Dierentuin(); break;
                    //case 5: Pokemon(); break;
                    //case 5: Pokemon(); break;
                    //case 5: Pokemon(); break;
                    default:
                        break;
                }
            }
            void FiguresWithInterfaces()
            {
                Console.Clear();
                Rnd rnd = new Rnd(1, 10);

                List<GeometricFigure> figuren = new List<GeometricFigure>();
                for (int i = 0; i < 10; i++)
                {
                    figuren.Add(new Rechthoek(rnd.RandomNumber() * (i + 1), rnd.RandomNumber() * (i + 1)));
                }
                Console.WriteLine(string.Format("{0,-30}{1,-30}\n","Ongesorteerd","Gesorteerd op H daarna op B"));
                int cursX = Console.CursorTop;
                int cursY = Console.CursorLeft;

                foreach (GeometricFigure figuur in figuren)
                {
                    Console.WriteLine(string.Format("{0,-10}: H={1,-2} B={2,-2}", "Figuur " + (figuren.IndexOf(figuur) + 1), figuur.Hoogte, figuur.Breedte));
                }
                figuren.Sort();
                foreach (GeometricFigure figuur in figuren)
                {
                    Console.CursorLeft = cursY + 30;
                    Console.CursorTop = cursX + figuren.IndexOf(figuur);
                    Console.WriteLine(string.Format("{0,-10}: H={1,-2} B={2,-2}", "Figuur " + (figuren.IndexOf(figuur) + 1), figuur.Hoogte, figuur.Breedte));
                }
                Console.ReadKey(true);
            }
            void Game()
            {
                Console.Clear();
                Rnd rnd = new Rnd(1, 30);
                Rnd rnd_5 = new Rnd(1, 5);
                Rnd rnd_4 = new Rnd(1, 4);
                const int maxY = 20;
                const int maxX = 20;
                const int aantalMonsters = 10;
                const int aantalRock = 10;
                const int aantalRockDestroyers = 10;
                MapElement[,] playground = new MapElement[maxX,maxY];

                int iRocks = 0;
                int iMonsters = 0;
                int iDestroyers = 0;
                for (int i = 0; i < maxX; i++)
                {
                    for (int j = 0; j < maxY; j++)
                    {
                        if (j != 0)
                        {
                            switch(rnd.RandomNumber())
                            {
                                case 1:
                                    {
                                        if (iMonsters < aantalMonsters)
                                        {
                                            playground[i, j] = new Monster('M');
                                            playground[i, j].Location = new Point('M', i, j);
                                            playground[i, j].MaxX = maxX;
                                            playground[i, j].MaxY = maxY;
                                            iMonsters++;
                                        }
                                    }break;
                                case 5:
                                    {
                                        if (iRocks < aantalRock)
                                        {
                                            playground[i, j] = new Rock('O');
                                            playground[i, j].Location = new Point('O', i, j);
                                            playground[i, j].MaxX = maxX;
                                            playground[i, j].MaxY = maxY;
                                            iRocks++;
                                        }
                                    }break;
                                case 10:
                                    {
                                        if (iDestroyers < aantalRockDestroyers)
                                        {
                                            playground[i, j] = new RockDestroyer('D');
                                            playground[i, j].Location = new Point('D', i, j);
                                            playground[i, j].MaxX = maxX;
                                            playground[i, j].MaxY = maxY;
                                            iDestroyers++;
                                        }
                                    }
                                    break;
                            }
                        }
                    }
                }
                bool bGameExit = false;
                bool bGewonnen = false;
                string sExitMsg = "";
                int cursX;
                int cursY;
                int iMenu = 1;
                playground[maxX / 2, 0] = new Player('X');
                playground[maxX / 2, 0].Location = new Point('X', maxX / 2,0);
                playground[maxX / 2, 0].MaxX = maxX;
                playground[maxX / 2, 0].MaxY = maxY;
                playground[maxX / 2, 0].Draw();
                while (!bGameExit)
                {
                    Console.Clear();
                    cursX = Console.CursorTop;
                    cursY = Console.CursorLeft;
                    for (int i = 0; i < maxX; i++)
                    {
                        for (int j = 0; j < maxY; j++)
                        {
                            if (playground[i, j] != null) 
                                playground[i, j].Draw();
                        }
                    }
                    Console.SetCursorPosition(Console.WindowWidth / 2 - 46/2, maxX + 1);
                    iMenu = SelectMenu(false, iMenu, "Up", "Down", "Right", "Left", "Shoot", "Exit");
                    if (iMenu == 6) bGameExit = true;
                    else
                    {
                        foreach (object obj in playground)
                        {
                            if (obj is Player player)
                            {
                                switch(iMenu)
                                {
                                    case 1: player.MoveUp(); break;
                                    case 2: player.MoveDown(); break;
                                    case 3:
                                        {
                                            player.MoveRight();
                                            if (player.Location.Y == maxY)
                                            {
                                                bGameExit = true;
                                                bGewonnen = true;
                                                sExitMsg = "Je hebt gewonnen!";
                                            }
                                            break;
                                            
                                        }
                                    case 4: player.MoveLeft(); break;
                                    case 5:
                                        {
                                            player.Shoot(ConsoleColor.Red);
                                            playground[player.Location.X, player.Location.Y + 1] = null;
                                            //playground[player.Location.X, player.Location.Y + 1].Location.drawChar = ' ';
                                        }
                                        break;
                                }
                            }else
                            if (obj is RockDestroyer rockDestroyer)
                            {
                                switch(rnd_5.RandomNumber())
                                {
                                    case 1: rockDestroyer.MoveUp(); break;
                                    case 2: rockDestroyer.MoveDown(); break;
                                    case 3: rockDestroyer.MoveLeft(); break;
                                    case 4: rockDestroyer.MoveRight(); break;
                                    case 5:
                                        {
                                            rockDestroyer.Shoot(ConsoleColor.Red);

                                            if (playground[rockDestroyer.Location.X, rockDestroyer.Location.Y + 1] is Player playerIsTarget)
                                            {
                                                if (playerIsTarget.Levens > 1)
                                                {
                                                    playerIsTarget.Levens--;
                                                    MsgXY(Console.WindowHeight / 2, Console.WindowWidth / 2 - sExitMsg.Length / 2, ConsoleColor.Red, msg: $"1 leven verloren, nog {playerIsTarget.Levens} te gaan");
                                                    Console.ReadKey(true);
                                                }
                                                else
                                                {
                                                    playerIsTarget = null;
                                                    sExitMsg = "Je hebt verloren!";
                                                    bGameExit = true;
                                                }
                                            }
                                            playground[rockDestroyer.Location.X, rockDestroyer.Location.Y + 1] = null;
                                        }
                                        break;
                                }
                            }else
                                if (obj is Monster monster)
                                {
                                    switch (rnd_4.RandomNumber())
                                    {
                                        case 1: monster.MoveUp(); break;
                                        case 2: monster.MoveDown(); break;
                                        case 3: monster.MoveLeft(); break;
                                        case 4: monster.MoveRight(); break;
                                    }
                            }
                        }
                    }
                }
                MsgXY(Console.WindowHeight / 2, Console.WindowWidth / 2 - sExitMsg.Length / 2, bGewonnen?ConsoleColor.Yellow:ConsoleColor.Red, msg: sExitMsg);
                Console.ReadKey();
            }
            void MsgXY(int x, int y, ConsoleColor fg = ConsoleColor.White, ConsoleColor bg = ConsoleColor.Black, params string[] msg)
            {
                int cursX = Console.CursorTop;
                int cursY = Console.CursorLeft;
                ConsoleColor Fg = Console.ForegroundColor;
                ConsoleColor Bg = Console.BackgroundColor;
                Console.ForegroundColor = fg;
                Console.BackgroundColor = bg;
                byte c = 0;
                foreach (var text in msg)
                {
                    Console.SetCursorPosition(y, x+c++);
                    Console.Write(text);
                }
                Console.CursorTop = cursX;
                Console.CursorLeft = cursY;
                Console.ForegroundColor = Fg;
                Console.BackgroundColor = Bg;
            }
            int SelectMenu(bool clearScreen = true, int select = 1, params string[] menu)
            {
                int selection = select;
                int cursTop = Console.CursorTop;
                int cursLeft = Console.CursorLeft;
                bool selected = false;
                ConsoleColor selectionForeground = Console.BackgroundColor;
                ConsoleColor selectionBackground = Console.ForegroundColor;

                if (clearScreen)
                {
                    Console.SetCursorPosition(0, 0);
                    Console.Clear();
                }
                else
                {
                    cursTop = Console.CursorTop;
                    cursLeft = Console.CursorLeft;
                    Console.SetCursorPosition(cursLeft, cursTop);
                }
                Console.CursorVisible = false;

                while (!selected)
                {
                    for (int i = 0; i < menu.Length; i++)
                    {
                        if (selection == i + 1)
                        {
                            Console.ForegroundColor = selectionForeground;
                            Console.BackgroundColor = selectionBackground;
                        }
                        Console.SetCursorPosition(cursLeft, cursTop + i);
                        Console.Write(string.Format("{0,5}:{1,-40}", i + 1, menu[i]));
                        Console.ResetColor();
                    }
                    switch (Console.ReadKey(true).Key)
                    {
                        case ConsoleKey.UpArrow:
                            selection--;
                            break;
                        case ConsoleKey.DownArrow:
                            selection++;
                            break;
                        case ConsoleKey.Enter:
                            selected = true;
                            break;
                        case ConsoleKey.D1:
                        case ConsoleKey.NumPad1: selection = 1; break;
                        case ConsoleKey.D2:
                        case ConsoleKey.NumPad2: selection = 2; break;
                        case ConsoleKey.D3:
                        case ConsoleKey.NumPad3: selection = 3 <= menu.Length ? 3 : menu.Length; break;
                        case ConsoleKey.D4:
                        case ConsoleKey.NumPad4: selection = 4 <= menu.Length ? 4 : menu.Length; break;
                    }
                    selection = Math.Min(Math.Max(selection, 1), menu.Length);
                    if (clearScreen)
                        Console.SetCursorPosition(0, 0);
                    else Console.SetCursorPosition(cursLeft, cursTop);
                }
                Console.Clear();
                Console.CursorVisible = true;
                return selection;
            }
            string InputStrFormat(string inputFormat = "  :    :    :", int fixedLength = 14, char charStart = '0', char charEnd = '9')
            {
                string toReturn = inputFormat;
                bool exit = false;
                int cursX = Console.CursorLeft;
                int cursY = Console.CursorTop;
                int count = 0;

                foreach (char c in toReturn)
                {
                    if (c == ' ')
                    {

                    }
                }
                while ((toReturn.Length < fixedLength) && (!exit))
                {
                    Console.CursorLeft = cursX;
                    Console.CursorTop = cursY;
                    Console.WriteLine(toReturn, fixedLength);
                    char input = Console.ReadKey(true).KeyChar;
                    if ((input >= charStart) && (input <= charEnd))
                    {
                        //toReturn[0] = input;
                    }
                }

                return toReturn;
            }
            char InputChr(params string[] tekst)
            {
                for (int i = 0; i < tekst.GetLength(0); i++)
                    Console.WriteLine(tekst[i]);
                return Console.ReadKey(true).KeyChar;
            }
            string InputStr(params string[] tekst)
            {
                for (int i = 0; i < tekst.GetLength(0); i++)
                    if (tekst.GetLength(0) == 1) Console.Write(tekst[i]);
                    else Console.WriteLine(tekst[i]);
                return Console.ReadLine();
            }
            bool InputBool(string tekst = "j/n", bool Cyes = true, bool Cno = false)
            {
                Console.WriteLine(tekst);
                switch (Char.ToLower(Console.ReadKey(true).KeyChar))
                {
                    case 'y':
                    case 'j': return Cyes;
                    case 'n': return Cno;
                }
                return false;
            }
            int InputInt(string tekst = "Getal: ")
            {
                Console.Write(tekst);
                return int.Parse(Console.ReadLine());
            }
            double InputDbl(string tekst = "Getal: ")
            {
                Console.Write(tekst);
                return double.Parse(Console.ReadLine());
            }
        }
    }
}
