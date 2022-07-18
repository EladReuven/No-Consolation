using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace No_Consolation
{
    public class BeepMusic
    {
        public static int[] DO = new int[] { 131, 262, 523, 1046 };
        public static int[] RE = new int[] { 147, 294, 587, 1174 };
        public static int[] MI = new int[] { 165, 330, 659, 1318 };
        public static int[] FA = new int[] { 175, 349, 698, 1396 };
        public static int[] SO = new int[] { 196, 392, 784, 1568 };
        public static int[] LA = new int[] { 220, 440, 880, 1760 };
        public static int[] TI = new int[] { 247, 494, 988, 1976 };
         
        public static int oct1 = 0, oct2 = 1, oct3 = 2, oct4 = 3;

        public static void DoReMi()
        {
            Console.Beep(DO[oct3], 200);
            Console.Beep(RE[oct3], 200);
            Console.Beep(MI[oct3], 200);
            Console.Beep(FA[oct3], 200);
            Console.Beep(SO[oct3], 200);
            Console.Beep(LA[oct3], 200);
            Console.Beep(TI[oct3], 200);
            Console.Beep(DO[oct4], 200);
        }

        public static void PokemonBattle()
        {
            Console.Beep(TI[oct2], 50);
            Console.Beep(SO[oct4], 100);
            Console.Beep(FA[oct4], 100);
            Console.Beep(MI[oct4], 100);
            Console.Beep(DO[oct3], 50);
            Console.Beep(SO[oct4], 100);
            Console.Beep(FA[oct4], 100);
            Console.Beep(MI[oct4], 100);
            Console.Beep(550, 50);
            Console.Beep(SO[oct4], 100);
            Console.Beep(FA[oct4], 100);
            Console.Beep(MI[oct4], 100);
            Console.Beep(RE[oct3], 50);
            Console.Beep(SO[oct4], 100);
            Console.Beep(FA[oct4], 100);
            Console.Beep(MI[oct4], 100);
            Console.Beep(620, 50);
            Console.Beep(SO[oct4], 100);
            Console.Beep(FA[oct4], 100);
            Console.Beep(MI[oct4], 100);
            Console.Beep(MI[oct3], 50);
            Console.Beep(SO[oct4], 100);
            Console.Beep(FA[oct4], 100);
            Console.Beep(MI[oct4], 100);
            Console.Beep(FA[oct3], 50);
            Console.Beep(SO[oct4], 100);
            Console.Beep(FA[oct4], 100);
            Console.Beep(MI[oct4], 100);
            Console.Beep(740, 50);
            Console.Beep(SO[oct4], 100);
            Console.Beep(FA[oct4], 100);
            Console.Beep(MI[oct4], 100);
            Console.Beep(SO[oct3], 50);
            Console.Beep(SO[oct4], 50);

        }

        

        public static void MortalKombat()
        {
            Console.Beep(LA[oct3], 200);
            Console.Beep(LA[oct3], 200);
            Console.Beep(LA[oct3], 200);
            Console.Beep(LA[oct3], 200);
            Console.Beep(SO[oct3], 200);
            Console.Beep(DO[oct4], 200);
            Console.Beep(LA[oct3], 200);
            Console.Beep(LA[oct3], 200);
            Console.Beep(LA[oct3], 200);
            Console.Beep(LA[oct3], 200);
            Console.Beep(SO[oct3], 200);
            Console.Beep(MI[oct3], 200);
        }

    }
}
