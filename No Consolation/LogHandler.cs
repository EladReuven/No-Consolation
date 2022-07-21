using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace No_Consolation
{
    public static class LogHandler
    {
        private static Queue<String> shortLog = new Queue<String>(5);
        private static Queue<String> longLog = new Queue<String>();
        private static List<string> logList;

        public static Queue<String> GetLog(bool trueShortFalseLong)
        {
            if(trueShortFalseLong)
            {
                return shortLog;
            }
            else
            {
                return longLog;
            }
        }

        public static void Add(String str)
        {
            if (shortLog.Count == 5)
            {
                shortLog.Dequeue();
            }
            shortLog.Enqueue(str);
            longLog.Enqueue(str);
        }
        public static void Reset()
        {
            shortLog.Clear();
        }

        public static void PrintShortLog()
        {
            Console.WriteLine("Log (5 last interactions):");
            Console.WriteLine("----------------------------");
            
            foreach (String line in GetLog(true).Reverse())
            {
                Console.WriteLine(line);
                Console.WriteLine("----------------------------");
            }
        }
        public static void PrintLongLog()
        {
            Console.WriteLine("Full Log Interactions: ");
            Console.WriteLine("----------------------------");

            foreach (String line in GetLog(false).Reverse())
            {
                Console.WriteLine(line);
                Console.WriteLine("----------------------------");
            }
        }
    }
    }
