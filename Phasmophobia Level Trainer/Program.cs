using Memory;
using System.Runtime.InteropServices;
using System.Security;
using System.Timers;

namespace PhasmoTrainer
{
    [SecurityCritical]
    internal class Program
    {
        [DllImport("user32.dll")]
        static extern short GetAsyncKeyState(int vkey);

        private static protected readonly string Level_Adress = "GameAssembly.dll+055C73A0,B98,50,B8,0,78,28";
        private static protected int Player_Level = 0;

        private static protected Mem meme = new Mem();

        private static protected void Main()
        {
            Console.CursorVisible = false;
            Setup();
        }


        private static protected void Setup()
        {
            int PID = meme.GetProcIdFromName("Phasmophobia");

            if (PID > 0)
            {
                meme.OpenProcess(PID);
                System.Timers.Timer timer = new System.Timers.Timer();
                timer.Elapsed += new ElapsedEventHandler(MainTimer);
                timer.Interval = 100;
                timer.Enabled = true;
                Thread worked = new Thread(Draw);
                worked.Start();
                Thread.Sleep(100000);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Game Not Found,  Please Start the Game!");
                Console.ForegroundColor = ConsoleColor.White;
                Thread.Sleep(5000); Console.Clear(); Setup();
            }
        }

        private static protected void MainTimer(object source, ElapsedEventArgs e)
        {
            bool WriteLevelOnce = false;
            if (GetAsyncKeyState(0x70) < 0)
            {
                Player_Level = meme.ReadInt(Level_Adress) + 10;
                string NewPlayerLevel = Player_Level.ToString();
                meme.WriteMemory(Level_Adress, "int", NewPlayerLevel);
                WriteLevelOnce = true;
            }
        }

        private static protected void Draw()
        {
            char spacer = '\n';
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(
                @"  _____  _                                 _______        _                 " + spacer +
                @" |  __ \| |                               |__   __|      (_)                " + spacer +
                @" | |__) | |__   __ _ ___ _ __ ___   ___      | |_ __ __ _ _ _ __   ___ _ __ " + spacer +
                @" |  ___/| '_ \ / _` / __| '_ ` _ \ / _ \     | | '__/ _` | | '_ \ / _ \ '__|" + spacer +
                @" | |    | | | | (_| \__ \ | | | | | (_) |    | | | | (_| | | | | |  __/ |   " + spacer +
                @" |_|    |_| |_|\__,_|___/_| |_| |_|\___/     |_|_|  \__,_|_|_| |_|\___|_|   " + spacer +
                spacer + spacer + spacer + spacer
                );
            State();
        }

        private static protected void State()
        {
            Player_Level = 0;
            Player_Level = meme.ReadInt(Level_Adress);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"F1 - Add 10 Phasmo Level            Current: {Player_Level}");
            Console.CursorVisible = false;
            Thread.Sleep(200);
            Console.SetCursorPosition(0, Console.CursorTop - 0);
            Console.Write(new string(' ', Console.BufferWidth));
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            State();
        }
    }
}
