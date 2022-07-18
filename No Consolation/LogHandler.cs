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
        private static Queue<String> log = new Queue<String>(5);
        private static List<string> logList;

        public static Queue<String> GetLog()
        {
            return log;
        }

        public static void Add(String str)
        {
            if (log.Count == 5)
            {
                log.Dequeue();
            }
            log.Enqueue(str);
        }
        public static void Reset()
        {
            log.Clear();
        }

        public static void PrintLog()
        {
            Console.WriteLine("Log (5 last interactions):");
            Console.WriteLine("----------------------------");
            
            foreach (String line in GetLog().Reverse())
            {
                Console.WriteLine(line);
                Console.WriteLine("----------------------------");
            }
        }

    }
    }
