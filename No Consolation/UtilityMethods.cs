using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace No_Consolation
{
    public static class UtilityMethods
    {
        public static bool shouldClear = false;
        public static bool sadDog = false;

        public static void ShouldClear()
        {
            if (shouldClear)
            {
                Console.Clear();
                shouldClear = false;
            }
        }

        public static void ClearLine()
        {
            Console.Write("                                                                                                    ");
        }

        public static void ClearBlock(int x, int y)
        {
            for(int i = 0; i < y; i++)
            {
                for(int j = 0; j < x; j++)
                {
                    Console.Write(" ");
                }
                Console.WriteLine();
            }
        }

        public static void YouDied()
        {
            Console.WriteLine(@"__     ______  _    _     _____ _____ ______ _____  ");
            Console.WriteLine(@"\ \   / / __ \| |  | |   |  __ \_   _|  ____|  __ \ ");
            Console.WriteLine(@" \ \_/ / |  | | |  | |   | |  | || | | |__  | |  | |");
            Console.WriteLine(@"  \   /| |  | | |  | |   | |  | || | |  __| | |  | |");
            Console.WriteLine(@"   | | | |__| | |__| |   | |__| || |_| |____| |__| |");
            Console.WriteLine(@"   |_|  \____/ \____/    |_____/_____|______|_____/ ");
        }

        public static void HappyShopDog()
        {
            Console.WriteLine(@"░░░░░░░░░░░░░░░░░░░░");
            Console.WriteLine(@"░▄▀▄▀▀▀▀▄▀▄░░░░░░░░░");
            Console.WriteLine(@"░█░░░░░░░░▀▄░░░░░░▄░");
            Console.WriteLine(@"█░░▀░░▀░░░░░▀▄▄░░█░█");
            Console.WriteLine(@"█░▄░█▀░▄░░░░░░░▀▀░░█");
            Console.WriteLine(@"█░░▀▀▀▀░░░░░░░░░░░░█");
            Console.WriteLine(@"█░░░░░░░░░░░░░░░░░░█");
            Console.WriteLine(@"█░░░░░░░░░░░░░░░░░░█");
            Console.WriteLine(@"░█░░▄▄░░▄▄▄▄░░▄▄░░█░");
            Console.WriteLine(@"░█░▄▀█░▄▀░░█░▄▀█░▄▀░");
            Console.WriteLine(@"░░▀░░░▀░░░░░▀░░░▀░░░");
        }

        public static void SadShopDog()
        {
            Console.WriteLine(@"░░░░░░░░░░░░░░░░░░░░");
            Console.WriteLine(@"░▄▀▄▀▀▀▀▄▀▄░░░░░░░░░");
            Console.WriteLine(@"░█░░░░░░░░▀▄░░░░░░▄░");
            Console.WriteLine(@"█░░▀░░▀░░░░░▀▄▄░░█░█");
            Console.WriteLine(@"█░░░█▀░░░░░░░░░▀▀░░█");
            Console.WriteLine(@"█░▄▀▀▀▀▄░░░░░░░░░░░█");
            Console.WriteLine(@"█░░░░░░░░░░░░░░░░░░█");
            Console.WriteLine(@"█░░░░░░░░░░░░░░░░░░█");
            Console.WriteLine(@"░█░░▄▄░░▄▄▄▄░░▄▄░░█░");
            Console.WriteLine(@"░█░▄▀█░▄▀░░█░▄▀█░▄▀░");
            Console.WriteLine(@"░░▀░░░▀░░░░░▀░░░▀░░░");
        }
    }
}
