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
            Console.OutputEncoding = Encoding.UTF8;
            Oefeningen = new List<string>();
            Oefeningen.Add("Exit");
            Oefeningen.Add("Figures with interfaces");
            Oefeningen.Add("Game");
            bool bExit = false;
            while (!bExit)
            {
                Console.Clear();
                Console.WriteLine("Interfaces oefeningen:\n");
                switch (SelectMenu(false,menu:Oefeningen.ToArray()) - 1)
                {
                    case 0: bExit = true; break;
                    case 1: FiguresWithInterfaces(); break;
                    case 2: Game(); break;
                    default: break;
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
                Rnd rnd = new Rnd(1, 15);
                Rnd rnd_5 = new Rnd(1, 6);
                Rnd rnd_4 = new Rnd(1, 4);
                const int maxY = 20;
                const int maxX = 20;
                const int aantalMonsters = 20;
                const int aantalRock = 60;
                const int aantalRockDestroyers = 30;
                MapElement[,] playground = new MapElement[maxX,maxY];
                List<MapElement> mapElements = new List<MapElement>();
                int iRocks = 0;
                int iMonsters = 0;
                int iDestroyers = 0;
                bool bGameExit = false;
                bool bGewonnen = false;
                string sExitMsg = null;
                int cursX;
                int cursY;
                int iMenu = 1;
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
                                            iMonsters++;
                                            Monster monster = new Monster('M');
                                            monster.Location = new Point(i, j);
                                            monster.MaxX = maxX;
                                            monster.MaxY = maxY;
                                            if (mapElements == null) mapElements = new List<MapElement>();
                                            mapElements.Add(monster);
                                        }
                                    }break;
                                case 5:
                                    {
                                        if (iRocks < aantalRock)
                                        {
                                            iRocks++;
                                            Rock rock = new Rock('O');
                                            rock.Location = new Point(i, j);
                                            rock.MaxX = maxX;
                                            rock.MaxY = maxY;
                                            if (mapElements == null) mapElements = new List<MapElement>();
                                            mapElements.Add(rock);
                                        }
                                    }break;
                                case 10:
                                    {
                                        if (iDestroyers < aantalRockDestroyers)
                                        {
                                            iDestroyers++;
                                            RockDestroyer rock = new RockDestroyer('D');
                                            rock.Location = new Point(i, j);
                                            rock.MaxX = maxX;
                                            rock.MaxY = maxY;
                                            if (mapElements == null) mapElements = new List<MapElement>();
                                            mapElements.Add(rock);
                                        }
                                    }
                                    break;
                            }
                        }
                    }
                }
                Player newPlayer = new Player('X');
                newPlayer.Location = new Point(maxX / 2, 0);
                newPlayer.MaxX = maxX;
                newPlayer.MaxY = maxY;
                newPlayer.Levens = 3;
                mapElements.Add(newPlayer);
                while (!bGameExit)
                {
                    Console.Clear();
                    playground = new MapElement[maxX, maxY];
                    foreach (var mapElement in mapElements)
                    {
                        switch (mapElement)
                        {
                            case IMoveable moveable:
                                {
                                    playground[moveable.Location.X, moveable.Location.Y] = mapElement;
                                }
                                break;
                            case Rock rock:
                                {
                                    playground[rock.Location.X, rock.Location.Y] = mapElement;
                                    //if ((rock.Location.X == i) && (rock.Location.Y == j))
                                    //    playground[i, j] = rock;
                                }
                                break;
                        }

                    }
                    cursX = Console.CursorTop;
                    cursY = Console.CursorLeft;
                    for (int i = 0; i < maxX; i++)
                    {
                        for (int j = 0; j < maxY; j++)
                        {
                            
                            
                            if (playground[i, j] != null)
                            {
                                playground[i, j].Draw();
                            }
                        }
                    }
                    Console.SetCursorPosition(Console.WindowWidth / 2 - 46/2, maxX + 1);
                    iMenu = SelectMenu(false, iMenu, "Up", "Down", "Right", "Left", "Shoot", "Exit");
                    if (iMenu == 6) bGameExit = true;
                    else
                    {
                        foreach (object obj in playground)
                        {
                            switch (obj)
                            {
                                case Player player:
                                    {
                                        switch (iMenu)
                                        {
                                            case 1:
                                                {
                                                    if (player.Location.X > 0)
                                                        if (playground[(player.Location.X - 1), player.Location.Y] == null)
                                                            player.MoveUp();
                                                }
                                                break;
                                            case 2:
                                                {
                                                    if (player.Location.X < maxX - 1)
                                                        if (playground[player.Location.X + 1, player.Location.Y] == null)
                                                            player.MoveDown();
                                                }
                                                break;
                                            case 3:
                                                {
                                                    if (player.Location.Y < maxY - 1)
                                                        if (playground[player.Location.X, player.Location.Y + 1] == null)
                                                        {
                                                            player.MoveRight();
                                                            if (player.Location.Y == maxY - 1)
                                                            {
                                                                bGameExit = true;
                                                                bGewonnen = true;
                                                                sExitMsg = "Je hebt gewonnen!";
                                                            }
                                                        }
                                                }
                                                break;
                                            case 4:
                                                {
                                                    if (player.Location.Y > 0)
                                                        if (playground[player.Location.X, player.Location.Y - 1] == null)
                                                            player.MoveLeft();
                                                }
                                                break;
                                            case 5:
                                                {
                                                    player.Shoot(ConsoleColor.Red);

                                                    int iDelete = -1;
                                                    bool bDestroyed = false;
                                                    iDelete = mapElements.IndexOf(playground[player.Location.X, player.Location.Y + 1]);
                                                    switch (playground[player.Location.X, player.Location.Y + 1])
                                                    {
                                                        case Monster monster:
                                                            {
                                                                if (monster.Levens > 0)
                                                                    monster.Levens--;
                                                                else
                                                                    bDestroyed = true;
                                                            }
                                                            break;
                                                        case Rock rock:
                                                            {
                                                                bDestroyed = true;
                                                            }
                                                            break;
                                                    }
                                                    if (bDestroyed)
                                                    {
                                                        iDelete = -1;
                                                        iDelete = mapElements.IndexOf(playground[player.Location.X, player.Location.Y + 1]);
                                                        if (iDelete > 0)
                                                        {
                                                            mapElements.RemoveAt(iDelete);
                                                        }
                                                        playground[player.Location.X, player.Location.Y + 1] = null;
                                                    }

                                                    //foreach (MapElement mapElement in mapElements)
                                                    //{
                                                    //    if ((mapElement.Location.X == player.Location.X) && (mapElement.Location.Y == player.Location.Y + 1))
                                                    //    {
                                                    //        if (mapElement is Rock) iDelete = mapElements.IndexOf(mapElement);
                                                    //        if (mapElement is Monster monster)
                                                    //        {
                                                    //            if (monster.Levens > 0) monster.Levens--;
                                                    //            else iDelete = mapElements.IndexOf(monster);
                                                    //        }
                                                    //    }
                                                    //    if (iDelete > 0) break;
                                                    //}
                                                    //playground[player.Location.X, player.Location.Y + 1] = null;
                                                    //if (iDelete > 0)
                                                    //{
                                                    //    mapElements.RemoveAt(iDelete);
                                                    //}
                                                }
                                                break;
                                        }
                                    }
                                    break;
                                case RockDestroyer rockDestroyer:
                                    {
                                        bool bAlGeschoten = false;
                                        for (int i = (rockDestroyer.Location.X - 1) < 0 ? 0 : rockDestroyer.Location.X - 1; i < ((rockDestroyer.Location.X + 1) > maxX ? maxX : rockDestroyer.Location.X + 1); i++)
                                        {
                                            for (int j = (rockDestroyer.Location.Y - 1) < 0 ? 0 : rockDestroyer.Location.Y - 1; j < ((rockDestroyer.Location.Y + 1) > maxY ? maxY : rockDestroyer.Location.Y + 1); j++)
                                            {

                                                bool bGeschoten = false;
                                                if (playground[i, j] is Rock rockIsTarget)
                                                {
                                                    bGeschoten = true;
                                                    int iIndex = mapElements.IndexOf(rockIsTarget);
                                                    mapElements.RemoveAt(mapElements.IndexOf(rockIsTarget));
                                                }
                                                if (playground[i, j] is Player playerIsTarget)
                                                {
                                                    bGeschoten = true;
                                                    if (playerIsTarget.Levens > 1)
                                                    {
                                                        playerIsTarget.Levens--;
                                                        MsgXY(Console.WindowHeight / 2, Console.WindowWidth / 2 - $"1 leven verloren, nog {playerIsTarget.Levens} te gaan".Length / 2, ConsoleColor.Red, msg: $"1 leven verloren, nog {playerIsTarget.Levens} te gaan");
                                                        Console.ReadKey(true);
                                                    }
                                                    else
                                                    {
                                                        playerIsTarget = null;
                                                        sExitMsg = "Je hebt verloren!";
                                                        bGameExit = true;
                                                    }
                                                }
                                                if (bGeschoten && !bGameExit)
                                                {
                                                    rockDestroyer.Shoot(ConsoleColor.Red);
                                                    bAlGeschoten = true;
                                                }
                                            }
                                        }
                                        if (!bAlGeschoten)
                                        {
                                            switch (rnd_5.RandomNumber())
                                            {
                                                case 1:
                                                    {
                                                        if (rockDestroyer.Location.X > 0)
                                                            if (playground[rockDestroyer.Location.X - 1, rockDestroyer.Location.Y] == null)
                                                                rockDestroyer.MoveUp();
                                                    }
                                                    break;
                                                case 2:
                                                    {
                                                        if (rockDestroyer.Location.X < maxX - 1)
                                                            if (playground[rockDestroyer.Location.X + 1, rockDestroyer.Location.Y] == null)
                                                                rockDestroyer.MoveDown();
                                                    }
                                                    break;
                                                case 3:
                                                    {
                                                        if (rockDestroyer.Location.Y > 0)
                                                            if (playground[rockDestroyer.Location.X, rockDestroyer.Location.Y - 1] == null)
                                                                rockDestroyer.MoveLeft();
                                                    }
                                                    break;
                                                case 4:
                                                    {
                                                        if (rockDestroyer.Location.Y < maxY - 1)
                                                            if (playground[rockDestroyer.Location.X, rockDestroyer.Location.Y + 1] == null)
                                                                rockDestroyer.MoveRight();
                                                    }
                                                    break;
                                            }
                                        }
                                    }
                                    break;
                                case Monster monster:
                                    {
                                        switch (rnd_4.RandomNumber())
                                        {
                                            case 1:
                                                {
                                                    if (monster.Location.X > 0)
                                                        if (playground[monster.Location.X - 1, monster.Location.Y] == null)
                                                            monster.MoveUp();
                                                }
                                                break;
                                            case 2:
                                                {
                                                    if (monster.Location.X < maxX - 1)
                                                        if (playground[monster.Location.X + 1, monster.Location.Y] == null)
                                                            monster.MoveDown();
                                                }
                                                break;
                                            case 3:
                                                {
                                                    if (monster.Location.Y > 0)
                                                        if (playground[monster.Location.X, monster.Location.Y - 1] == null)
                                                            monster.MoveLeft();
                                                }
                                                break;
                                            case 4:
                                                {
                                                    if (monster.Location.Y < maxY - 1)
                                                        if (playground[monster.Location.X, monster.Location.Y + 1] == null)
                                                            monster.MoveRight();
                                                }
                                                break;
                                        }
                                    }
                                    break;
                            }
                        }
                    }
                    if (sExitMsg != null)
                    {
                        MsgXY(Console.WindowHeight / 2, Console.WindowWidth / 2 - sExitMsg.Length / 2, bGewonnen ? ConsoleColor.Yellow : ConsoleColor.Red, msg: sExitMsg);
                        Console.ReadKey();
                    }
                }
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
