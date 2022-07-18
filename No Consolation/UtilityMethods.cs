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
    }
}
